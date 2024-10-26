using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//Ref Other
using Newtonsoft.Json;
using Generic;
using RFID.Command;
using RFID.Adapter;
using RFIDModel.Model.POJO;
using RFIDModel.Model.DTO;
using RFIDModel.Interface.POJO;
using RFIDModel.Interface.DTO;
using RFIDModel.Common;

namespace RFIDClient
{
    public partial class FrmShipmentInOut : Form
    {
        /// <summary>In User必選标识: true:必填, false:选填 </summary>
        bool bInUser = false;
        bool bManual = false;
        const int CODE_LENGTH = 20;
        const string MAGNETIZE = "0000";
        const string DEMAGNETIZE = "1111";
        string dbName = AppConfig.CurrentDBName;
        string dbConn = AppConfig.CurrentDBConn;
        string sqlMapperId = Common.SqlMapperId;
        string vFactNo, vBarNo, vAutoDemMk, vDemMk, vReasonNo, vInOutKind, stockNoIn;
        ServiceType usedServiceType;
        ReadWriteJson oReadWriteJson = new ReadWriteJson();
        List<string> inUserList = null;
        List<string> outUserList = null;
        IList<PctStockUser> lstPctStockUser;
        // 用于新设备， 2023-08-22
        string oldCode = "", newCode = "";
        bool isRun = false;
        Thread oThread = null;
        delegate bool ReadCallback(string paraVal);
        ReadCallback readCallback;

        string InOutKind
        {
            get { return vInOutKind; }
            set
            {
                vInOutKind = value;
                switch (vInOutKind)
                {
                    case "0":
                        vInOutKind = "A";
                        vDemMk = MAGNETIZE;
                        break;
                    case "1":
                        vInOutKind = "B";
                        vDemMk = DEMAGNETIZE;
                        break;
                    default:
                        vInOutKind = "";
                        break;
                }
            }
        }

        ReaderAdapter oRfidControl = null;
        static TagPool oTagPool = null;
        ContextMenu oMenu = null;
        public FrmShipmentInOut()
        {
            oTagPool = new TagPool();
            oMenu = new ContextMenu();
            InitializeComponent();
            this.Text = Lang.Dict("C0009", "樣品倉入出庫");
            List<PropName> paraList = oReadWriteJson.ReadJson();
            PropName oPropName = paraList != null && paraList.Count > 0 ? paraList[0] : new PropName();
            SrvStatic.comPort = oPropName.ComPort == null ? "" : oPropName.ComPort;
            SrvStatic.readerType = oPropName.ReaderType == null ? "" : oPropName.ReaderType;
            SrvStatic.MinPower = string.IsNullOrEmpty(oPropName.ReadPower) ? 0 : Convert.ToInt32(oPropName.ReadPower);
            SrvStatic.MaxPower = string.IsNullOrEmpty(oPropName.WritePower) ? 0 : Convert.ToInt32(oPropName.WritePower);
            SrvStatic.TryTime = string.IsNullOrEmpty(oPropName.WriteTime) ? 0 : Convert.ToInt32(oPropName.WriteTime);
            cbxComPort.Text = SrvStatic.comPort;
            oMenu_AddItem("Clear Table", new System.EventHandler(oMenuItem_ClearDatagrid_Click));
            oMenu_AddItem("Read Tags", new System.EventHandler(oMenuItem_ReadRfidTags_Click));
            oMenu_AddItem("Run StockInOut", new System.EventHandler(oMenuItem_RunStockInOut_Click));
            oMenu_AddItem("Filter History", new System.EventHandler(oMenuItem_FilterStockInOut_Click));
            oMenu.MenuItems[1].Enabled = false;
            oMenu.MenuItems[2].Enabled = false;
            tbxBarCode.Enabled = false;
        }

        #region "FORM EVEN"
        private delegate void mathAction();
        private void FrmShipmentInOut_Load(object sender, EventArgs e)
        {
            readCallback = new ReadCallback(ShipmentInOutByTagId);
            btnChkUser.Enabled = false;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oService = ServiceFactory<IPctUserDAO>.GetService();
            try
            {
                IList<PctUser> lstPctUser;
                string usrId = SrvStatic.usrId;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelPctUserAll, dbName, new PctUser()
                {
                    UsrId = usrId
                }, sqlMapperId);
                lstPctUser = oService.GetData();
                Common.CloseService(oService);  //關閉通道
                if (lstPctUser == null || lstPctUser.Count == 0)
                    throw new Exception("No user data");
                vFactNo = lstPctUser[0].FactNo;
                vFactNo = vFactNo != null ? vFactNo.Trim() : vFactNo;
                InOutKind = Convert.ToString(tabShipment.SelectedIndex);
                LoadCbxBrandItems(new DevBrand()
                {
                    FactNo = vFactNo
                });
                oTagPool.RegisterAddEven(new TagPool.ActionEven(TagPoolActionAdd));
                oTagPool.RegisterModifyEven(new TagPool.ActionEven(TagPoolActionModify));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            bInUser = CheckInUser();
            BindInUser();
            CheckStatus();
        }

        private void FrmShipmentInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            oTagPool = null;
            if (oRfidControl != null && oRfidControl.IsConnected())
            {
                RfidCommandGroup(RfidCommandGroupNm.InitReader);
                oRfidControl.Disconnect();
            }
        }

        private void oMenu_AddItem(string itemNm, System.EventHandler oEvenHandler)
        {
            MenuItem miAdd = new MenuItem(itemNm);
            miAdd.Click += oEvenHandler;
            oMenu.MenuItems.Add(miAdd);
        }

        private void oMenuItem_ReadRfidTags_Click(object sender, System.EventArgs e)
        {
            try
            {
                ReadTagByManual();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void oMenuItem_ClearDatagrid_Click(object sender, System.EventArgs e)
        {
            oTagPool.ClearPool();
            dgHistory.Rows.Clear();
            if (oRfidControl != null && oRfidControl.IsConnected())
                oRfidControl.ClearTagList();
        }

        private void oMenuItem_RunStockInOut_Click(object sender, System.EventArgs e)
        {
            int iRows = dgHistory.Rows.Count;
            string rfidCode, demMk;
            if (iRows == 0) return;
            try
            {
                for (int i = 0; i < iRows; i++)
                {
                    rfidCode = Convert.ToString(dgHistory.Rows[i].Cells["RfidCode"].Value);
                    demMk = Convert.ToString(dgHistory.Rows[i].Cells["DemMk"].Value);
                    oTagPool.AddTag(new TagStruct()
                    {
                        TagID = rfidCode + demMk,
                        DiscoveryTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        OtherUse = Convert.ToString(TagPoolInType.ManualReadIn)
                    });
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void oMenuItem_FilterStockInOut_Click(object sender, System.EventArgs e)
        {
            int iRows = dgHistory.Rows.Count;
            string stockStatus = "";
            List<int> lstIdx = new List<int>();
            try
            {
                switch (InOutKind)
                {
                    case "A":
                        stockStatus = Convert.ToString(StockStatus.StockIn);
                        break;
                    case "B":
                        stockStatus = Convert.ToString(StockStatus.StockOut);
                        break;
                }

                for (int i = 0; i < iRows; i++)
                {
                    dgHistory.Rows[i].Selected = false;
                    if (dgHistory.Rows[i].Cells["Status"].Value.ToString() == stockStatus)
                    {
                        dgHistory.Rows[i].Selected = true;
                    }
                }

                foreach (DataGridViewRow item in dgHistory.SelectedRows)
                {
                    dgHistory.Rows.RemoveAt(item.Index);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string btnConnectText = btnConnect.Text;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                switch (btnConnectText)
                {
                    case "CONNECT":
                        ConnectRfidReader();
                        break;
                    case "DISCONNECT":
                        DisconnectRfidReader();
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnProgTag_Click(object sender, EventArgs e)
        {
            string progCode;
            bool bolRfid = false;
            DevBarno oDevBarno = null;
            string orginCode = tbxOrginCode.Text.ToUpper().Trim();
            string setCode = tbxProgramCode.Text.ToUpper().Trim();
            try
            {
                if (oRfidControl != null && oRfidControl.IsConnected())
                    bolRfid = true;
                if (!bolRfid)
                    throw new Exception("RFID Not Connected");
                if (orginCode.Length != 24)
                    throw new Exception("OrginCode length must equal 24");
                if (setCode.Length != 20)
                    throw new Exception("ProgramCode length must equal 20");
                oDevBarno = SetDevBarnoByShipment(setCode, setCode.Replace(setCode.Substring(0, CODE_LENGTH), ""));
                if (oDevBarno == null || String.IsNullOrEmpty(oDevBarno.BarNo))
                    throw new Exception("ProgramCode not in DMS database");
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                RfidCommandGroup(RfidCommandGroupNm.InitReader);
                SetTagMask("Include", "96", orginCode);
                setCode = setCode + GetStockStatusDemMk(StockStatus.StockIn);
                progCode = oRfidControl.ProgTagBySingle(1, setCode);
                oRfidControl.SetReaderBySingle(ParameterNm.SetAcqG2Mask, "0");
                oRfidControl.SetReaderBySingle(ParameterNm.Mask, "0");
                if (progCode.Replace(" ", "").Trim() != setCode.Replace(" ", "").Trim())
                {
                    throw new Exception("Program Tag Error [" + orginCode + "]");
                }
                oDevBarno.DemMk = GetStockStatusDemMk(StockStatus.StockIn);
                ModifyDgHistory(oDevBarno);
                WriteMsg("Program Tag Success[" + progCode + "]");
                tbxOrginCode.Text = "";
                tbxProgramCode.Text = "";
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tabShipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckStatus();
            string stockNo = cbxStock.Text.Trim();
            string brandNo = cbxBrand.Text.Trim();
            vBarNo = null;
            cbxInOut.Items.Clear();
            cbxOutReason.Items.Clear();
            cbxOutSelection.Items.Clear();
            cbxOutUser.Items.Clear();
            cbxPostNo.Items.Clear();
            cbxInUser.Text = "";
            cbxOutUser.Text = "";
            InOutKind = Convert.ToString(tabShipment.SelectedIndex);
            //切換頁簽時重新清除舊值并加入緩存列表中
            if (oRfidControl != null)
            {
                oMenuItem_ClearDatagrid_Click(sender, e);
                oMenuItem_RunStockInOut_Click(sender, e);
            }
            //EnableServerTypeControl(false);
            if (String.IsNullOrEmpty(InOutKind))
            {
                cbxBrand.Enabled = false;
                cbxStock.Enabled = false;
                cbxInOut.Enabled = false;
            }
            else
            {
                cbxBrand.Enabled = true;
                cbxStock.Enabled = true;
                cbxInOut.Enabled = true;
                if (InOutKind.Equals("A"))
                {
                    bInUser = CheckInUser();
                    BindInUser();
                }
            }

            if (!String.IsNullOrEmpty(stockNo) && !String.IsNullOrEmpty(brandNo))
            {
                if (brandNo.IndexOf('-') != -1)
                    brandNo = brandNo.Split('-')[0];
                if (stockNo.IndexOf('-') != -1)
                    stockNo = stockNo.Split('-')[0];
                string kindType = GetStockIn("T2");   //查詢有無啓用過濾InOut No.資料需權限抓取的開關 2023-1-11
                LoadCbxInOutNoItems(new DevStockInOutNo()
                {
                    FactNo = vFactNo,
                    InOutKind = InOutKind,
                    BrandNo = brandNo,
                    StockNo = stockNo,
                    RightUser = string.IsNullOrEmpty(kindType) ? "" : SrvStatic.usrId
                });
            }
            CheckStatus();
        }

        /// <summary>检查 In User是否必填</summary>
        private bool CheckInUser()
        {
            bool bRet = false;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IPctUserDAO>.GetService();
            try
            {
                IList<PctUser> listUser;
                string usrId = SrvStatic.usrId;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelRfidInUser, dbName, new PctUser(), sqlMapperId);
                listUser = oService.GetData();
                Common.CloseService(oService);
                bRet = (listUser == null || listUser.Count == 0) ? false : true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
            return bRet;
        }

        /// <summary>綁定 In user 選項</summary>
        private void BindInUser()
        {
            if (!Common.CheckChannel())
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            var oService = ServiceFactory<IPctUserDAO>.GetService();
            try
            {
                IList<PctUser> listUser;
                string usrId = SrvStatic.usrId;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelPctUserAll, dbName, new PctUser() { StopYn = "N" }, sqlMapperId);
                listUser = oService.GetData();
                Common.CloseService(oService);
                cbxInUser.Items.Clear();
                if (inUserList != null) { inUserList.Clear(); }
                inUserList = new List<string>();
                foreach (PctUser user in listUser)
                {
                    cbxInUser.Items.Add(user.UsrId + "-" + user.UsrName);
                    inUserList.Add(user.UsrId + "-" + user.UsrName);
                }
                cbxInUser.AutoCompleteCustomSource.AddRange(inUserList.ToArray());
                cbxInUser.AutoCompleteSource = AutoCompleteSource.None;
                cbxInUser.AutoCompleteMode = AutoCompleteMode.None;
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        /// <summary>是否可選擇【Automatic, SemiAutomatic, Manual】</summary>
        private void CheckStatus()
        {
            oTagPool.ClearPool();
            dgHistory.Rows.Clear();
            if (oRfidControl != null && oRfidControl.IsConnected())
                oRfidControl.ClearTagList();
            chxAutomatic.Enabled = false;
            chxSemiautomatic.Enabled = false;
            chxManual.Enabled = false;
            chxAutomatic.Checked = false;
            chxSemiautomatic.Checked = false;
            chxManual.Checked = false;
            bool bInUserMk = bInUser ? (cbxInUser.SelectedIndex >= 0 ? true : false) : true;
            switch (InOutKind)
            {
                case "A":
                    if (cbxBrand.SelectedIndex >= 0 && cbxStock.SelectedIndex >= 0 && cbxInOut.SelectedIndex >= 0 && cbxPostNo.SelectedIndex >= 0 && bInUserMk
                        && !string.IsNullOrWhiteSpace(cbxInUser.Text))
                    {
                        chxAutomatic.Enabled = true;
                        chxSemiautomatic.Enabled = true;
                        chxManual.Enabled = bManual;
                    }
                    break;
                case "B":
                    if (cbxBrand.SelectedIndex >= 0 && cbxStock.SelectedIndex >= 0 && cbxInOut.SelectedIndex >= 0 && cbxOutReason.SelectedIndex >= 0
                        && cbxOutSelection.SelectedIndex >= 0 && cbxOutUser.SelectedIndex >= 0 && !string.IsNullOrWhiteSpace(cbxOutUser.Text))
                    {
                        chxAutomatic.Enabled = true;
                        chxSemiautomatic.Enabled = true;
                        chxManual.Enabled = bManual;
                    }
                    break;
            }
        }

        private void cbxComPort_TextChanged(object sender, EventArgs e)
        {
            if (oRfidControl == null)
                return;
            string comPort = cbxComPort.Text.Trim();
            if (String.IsNullOrEmpty(comPort))
                return;
            oRfidControl.SetSerialPort(comPort);
        }

        private void cbxBrand_TextChanged(object sender, EventArgs e)
        {
            string brandNo = cbxBrand.Text.Trim();
            if (String.IsNullOrEmpty(brandNo))
                return;
            if (brandNo.IndexOf('-') != -1)
                brandNo = brandNo.Split('-')[0];
            this.Cursor = Cursors.WaitCursor;
            LoadCbxStockNoItems(new PctStockUser()
            {
                FactNo = vFactNo,
                BrandNo = brandNo,
                UsrId = SrvStatic.usrId
            });
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 是否禁用
            bool bFrozen = CheckFrozen(brandNo);
            if (bFrozen)
            {
                MessageBox.Show("During the audit inventory period, Scan In/out is not allowed! \n\n 倉庫停用，不允許入出庫掃描！", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.Cursor = Cursors.Default;
            CheckStatus();
        }
        /// <summary> 检查是否禁用 dev_inventory_frozen </summary>
        private bool CheckFrozen(string brandNo)
        {
            bool bRet = false;
            var oServiceFrozen = ServiceFactory<IDevInventoryFrozenDAO>.GetService();
            oServiceFrozen.SetConnection(dbName, AppConfig.CurrentDBConn);
            oServiceFrozen.SetPara(SqlConst.SelDevInventoryFrozen, dbName, new DevInventoryFrozen() { FrozenYn = "Y", BrandNo = brandNo.ToUpper() }, sqlMapperId);
            IList<DevInventoryFrozen> listFrozen = oServiceFrozen.GetData();
            Common.CloseService(oServiceFrozen);
            btnConnect.Enabled = true;
            tabShipment.Enabled = true;
            cbxStock.Enabled = true;
            cbxInOut.Enabled = true;
            if (listFrozen != null && listFrozen.Count > 0)
            {
                if (oRfidControl != null && oRfidControl.IsConnected())
                    oRfidControl.Disconnect();
                this.Cursor = Cursors.Default;
                btnConnect.Enabled = false;
                chxAutomatic.Enabled = false;
                chxSemiautomatic.Enabled = false;
                chxManual.Enabled = false;
                tabShipment.Enabled = false;
                cbxStock.Enabled = false;
                cbxInOut.Enabled = false;
                bRet = true;
            }
            return bRet;
        }

        private void cbxStock_TextChanged(object sender, EventArgs e)
        {
            string stockNo = cbxStock.Text.Trim();
            string brandNo = cbxBrand.Text.Trim();
            if (String.IsNullOrEmpty(stockNo) || String.IsNullOrEmpty(brandNo))
                return;
            if (stockNo.IndexOf('-') != -1)
                stockNo = stockNo.Split('-')[0];
            if (brandNo.IndexOf('-') != -1)
                brandNo = brandNo.Split('-')[0];
            this.Cursor = Cursors.WaitCursor;
            string kindType = GetStockIn("T2");   //查詢有無啓用過濾InOut No.資料需權限抓取的開關 2023-1-11
            LoadCbxInOutNoItems(new DevStockInOutNo()
            {
                FactNo = vFactNo,
                InOutKind = InOutKind,
                BrandNo = brandNo,
                StockNo = stockNo,
                RightUser = string.IsNullOrEmpty(kindType) ? "" : SrvStatic.usrId
            });
            this.Cursor = Cursors.Default;
            CheckStatus();
        }

        private void cbxInOut_TextChanged(object sender, EventArgs e)
        {
            bool bolEnable = false;
            string brandNo = cbxBrand.Text.Trim();
            string stockNo = cbxStock.Text.Trim();
            string inOutNo = cbxInOut.Text.Trim();
            if (String.IsNullOrEmpty(brandNo) || String.IsNullOrEmpty(stockNo) || String.IsNullOrEmpty(inOutNo))
                return;
            if (brandNo.IndexOf('-') != -1)
                brandNo = brandNo.Split('-')[0];
            if (stockNo.IndexOf('-') != -1)
                stockNo = stockNo.Split('-')[0];
            if (inOutNo.IndexOf('-') != -1)
                inOutNo = inOutNo.Split('-')[0];
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (InOutKind)
                {
                    case "A":
                        bolEnable = LoadCbxPostNoItems(new DevStockidd()
                        {
                            FactNo = vFactNo,
                            BrandNo = brandNo,
                            StockNo = stockNo,
                            StopMk = "N"
                        });
                        break;
                    case "B":
                        cbxOutReason.Text = "";
                        cbxOutReason.Items.Clear();
                        bolEnable = LoadCbxOutReasonItems(new DevOutReasonStock()
                        {
                            FactNo = vFactNo,
                            BrandNo = brandNo,
                            StockNo = stockNo,
                            InoutNo = inOutNo
                        });

                        if (cbxOutSelection.Items.Count == 0)
                            LoadCbxOutSelectionItems(new DeptGroup());
                        break;
                }
                EnableServerTypeControl(bolEnable);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            CheckStatus();
        }

        private void cbxOutReason_TextChanged(object sender, EventArgs e)
        {
            string reasonNo = cbxOutReason.Text.Trim();
            if (String.IsNullOrEmpty(reasonNo))
            {
                vReasonNo = "";
                return;
            }
            if (reasonNo.IndexOf('-') != -1)
                reasonNo = reasonNo.Split('-')[0];
            vReasonNo = reasonNo;
            this.Cursor = Cursors.WaitCursor;
            SetAutoDemMk(reasonNo);
            this.Cursor = Cursors.Default;
            CheckStatus();
        }

        private void cbxOutSelection_TextChanged(object sender, EventArgs e)
        {
            string outSelection = cbxOutSelection.Text.Trim();
            if (String.IsNullOrEmpty(outSelection))
                return;
            if (outSelection.IndexOf('-') != -1)
                outSelection = outSelection.Split('-')[0];
            this.Cursor = Cursors.WaitCursor;
            LoadCbxOutUserItems(new DeptGroupUser()
            {
                DepartId = Convert.ToInt32(outSelection)
            });
            this.Cursor = Cursors.Default;
            CheckStatus();
        }

        private void chxAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            bool bolRfidConneced = false;
            oMenuItem_RunStockInOut_Click(sender, e);
            if (oRfidControl != null && oRfidControl.IsConnected())
                bolRfidConneced = true;
            if (!bolRfidConneced)
            {
                if (chxAutomatic.Checked)
                {
                    chxAutomatic.Checked = false;
                    MessageBox.Show("RFID Not Connected");
                }
                return;
            }

            vBarNo = null;
            tbxBarCode.Text = "";
            lblOldBarCode.Text = "";
            if (chxAutomatic.Checked)
            {
                ReceiveMsg oReceiveMsg = new ReceiveMsg(ReceiveMsg);
                RfidCommandGroup(RfidCommandGroupNm.ListeninTagList);
                if (chxSemiautomatic.Checked)
                    chxSemiautomatic.Checked = false;
                if (chxManual.Checked)
                    chxManual.Checked = false;
                tbxBarCode.Enabled = false;
                oMenu.MenuItems[1].Enabled = false;
                oMenu.MenuItems[2].Enabled = false;
                oMenu.MenuItems[3].Enabled = false;
                // 如是新设备 ReaderType.ALIEN_UHF
                if ("2".Equals(SrvStatic.readerType))
                    StartExecute();
                else
                    oRfidControl.AddMessageReceived(oReceiveMsg);
                usedServiceType = ServiceType.Automatic;
            }
            else if (!chxAutomatic.Checked && !chxSemiautomatic.Checked && !chxManual.Checked)
            {
                oMenu.MenuItems[1].Enabled = false;
                oMenu.MenuItems[2].Enabled = false;
                oMenu.MenuItems[3].Enabled = true;
                tbxBarCode.Enabled = false;
                RfidCommandGroup(RfidCommandGroupNm.InitReader);
            }
            oRfidControl.ClearTagList();
        }

        /// <summary>启用线程执行</summary>
        private void StartExecute()
        {
            if (oThread != null)
            {
                bool bStop = oRfidControl.iReader.StopGet();
                isRun = false;
                DisconnectRfidReader();
                ConnectRfidReader();
            }
            oldCode = "";
            isRun = true;
            oThread = new Thread(new ThreadStart(delegate { AutoRun(); }));
            oThread.Start();
        }
        /// <summary>自动执行</summary>
        private void AutoRun()
        {
            while (isRun)
            {
                newCode = oRfidControl.ReadTagByBank(1, "2", "6");
                if (!oldCode.Replace(" ", "").Equals(newCode.Replace(" ", "")) && !string.IsNullOrEmpty(newCode))
                    this.Invoke(readCallback, new object[] { chxAutomatic.Checked ? newCode : "" });
            };
        }

        private void chxSemiautomatic_CheckedChanged(object sender, EventArgs e)
        {
            bool bolRfidConneced = false;
            oMenuItem_ClearDatagrid_Click(sender, e);
            oMenuItem_RunStockInOut_Click(sender, e);
            if (oRfidControl != null && oRfidControl.IsConnected())
                bolRfidConneced = true;
            if (!bolRfidConneced)
            {
                if (chxSemiautomatic.Checked)
                {
                    chxSemiautomatic.Checked = false;
                    MessageBox.Show("RFID Not Connected");
                }
                return;
            }

            vBarNo = null;
            tbxBarCode.Text = "";
            lblOldBarCode.Text = "";
            if (chxSemiautomatic.Checked)
            {
                ReceiveMsg oReceiveMsg = new ReceiveMsg(ReceiveMsg);
                if (chxAutomatic.Checked)
                    chxAutomatic.Checked = false;
                if (chxManual.Checked)
                    chxManual.Checked = false;
                tbxBarCode.Enabled = true;
                oMenu.MenuItems[1].Enabled = false;
                oMenu.MenuItems[2].Enabled = true;
                oMenu.MenuItems[3].Enabled = true;
                RfidCommandGroup(RfidCommandGroupNm.ListeninTagList);
                // 如是新设备 ReaderType.ALIEN_UHF
                if ("2".Equals(SrvStatic.readerType))
                    StartExecute();
                else
                    oRfidControl.AddMessageReceived(oReceiveMsg);
                usedServiceType = ServiceType.Semiautomatic;
            }
            else if (!chxAutomatic.Checked && !chxSemiautomatic.Checked && !chxManual.Checked)
            {
                oMenu.MenuItems[1].Enabled = false;
                oMenu.MenuItems[2].Enabled = false;
                oMenu.MenuItems[3].Enabled = true;
                tbxBarCode.Enabled = false;
                RfidCommandGroup(RfidCommandGroupNm.InitReader);
            }
            oRfidControl.ClearTagList();
        }

        private void chxManual_CheckedChanged(object sender, EventArgs e)
        {
            vBarNo = null;
            tbxBarCode.Text = "";
            lblOldBarCode.Text = "";
            oMenuItem_ClearDatagrid_Click(sender, e);
            oMenuItem_RunStockInOut_Click(sender, e);
            if (chxManual.Checked)
            {
                tbxBarCode.Enabled = true;
                chxAutomatic.Checked = false;
                chxSemiautomatic.Checked = false;
                oMenu.MenuItems[1].Enabled = true;
                oMenu.MenuItems[2].Enabled = true;
                oMenu.MenuItems[3].Enabled = true;
                if (oRfidControl != null && oRfidControl.IsConnected())
                {
                    ReceiveMsg oReceiveMsg = new ReceiveMsg(ReceiveMsg);
                    RfidCommandGroup(RfidCommandGroupNm.InitReader);
                    oRfidControl.RemoveMessageReceived(oReceiveMsg);
                    // 如是新设备 ReaderType.ALIEN_UHF
                    if ("2".Equals(SrvStatic.readerType))
                        StartExecute();
                }
                usedServiceType = ServiceType.Manual;
            }
            else if (!chxAutomatic.Checked && !chxSemiautomatic.Checked && !chxManual.Checked)
            {
                oMenu.MenuItems[1].Enabled = false;
                oMenu.MenuItems[2].Enabled = false;
                oMenu.MenuItems[3].Enabled = true;
                tbxBarCode.Enabled = false;
                RfidCommandGroup(RfidCommandGroupNm.InitReader);
            }
        }

        private void tbxBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            string rfidCode, demMk;
            IList<DevBarno> lstDevBarno;
            if (e.KeyCode != Keys.Enter) return;
            if (String.IsNullOrEmpty(tbxBarCode.Text.Trim())) return;
            if (String.IsNullOrEmpty(cbxBrand.Text.Trim())) return;
            if (String.IsNullOrEmpty(cbxStock.Text.Trim())) return;
            switch (InOutKind)
            {
                case "A":
                    if (String.IsNullOrEmpty(cbxPostNo.Text.Trim())) return;
                    break;
                case "B":
                    if (String.IsNullOrEmpty(cbxOutReason.Text.Trim())) return;
                    if (String.IsNullOrEmpty(cbxOutSelection.Text.Trim())) return;
                    if (String.IsNullOrEmpty(cbxOutUser.Text.Trim())) return;
                    break;
                default:
                    return;
            }
            vBarNo = tbxBarCode.Text.Trim();
            lblOldBarCode.Text = vBarNo;
            tbxBarCode.Text = "";
            lblOldBarCode.Visible = true;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oService = ServiceFactory<IDevBarnoDAO>.GetService();
            oService.SetConnection(dbName, dbConn);
            oService.SetPara(SqlConst.SelDevBarnoAll, dbName, new DevBarno()
            {
                BarNo = vBarNo
            }, sqlMapperId);
            lstDevBarno = oService.GetData() as IList<DevBarno>;
            Common.CloseService(oService);
            if (lstDevBarno == null || lstDevBarno.Count == 0) return;
            if (String.IsNullOrEmpty(lstDevBarno[0].RfidCode)) return;
            rfidCode = lstDevBarno[0].RfidCode;
            demMk = lstDevBarno[0].DemMk;
            if (String.IsNullOrEmpty(demMk))
                demMk = GetStockStatusDemMk(StockStatus.StockIn);
            rfidCode = rfidCode + demMk;
            oTagPool.AddTag(new TagStruct()
            {
                TagID = rfidCode,
                DiscoveryTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                OtherUse = Convert.ToString(TagPoolInType.ManualReadIn)
            });
        }

        private void dgHistory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                oMenu.Show(dgHistory, new Point(e.X, e.Y));
            }
        }

        private void dgHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    string rfidCode = "";
                    foreach (DataGridViewRow item in dgHistory.SelectedRows)
                    {
                        rfidCode = Convert.ToString(dgHistory.Rows[item.Index].Cells["RfidCode"].Value);
                        rfidCode += Convert.ToString(dgHistory.Rows[item.Index].Cells["DemMk"].Value);
                        dgHistory.Rows.RemoveAt(item.Index);
                        if (usedServiceType == ServiceType.Automatic)
                            oTagPool.RemoveTag(rfidCode);
                    }

                    if (dgHistory.Rows.Count == 0)
                    {
                        oTagPool.ClearPool();
                        if (oRfidControl != null && oRfidControl.IsConnected())
                            oRfidControl.ClearTagList();
                    }
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        private void dgHistory_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblCounted.Text = dgHistory.Rows.Count.ToString();
        }

        private void dgHistory_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblCounted.Text = dgHistory.Rows.Count.ToString();
        }

        private void lbxMsg_DoubleClick(object sender, EventArgs e)
        {
            lbxMsg.Items.Clear();
        }
        #endregion

        #region "FUNCTION"
        private bool LoadCbxBrandItems(DevBrand oDevBrand)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDevBrandDAO>.GetService();
            try
            {
                if (oDevBrand == null) return false;
                IList<DevBrand> lstDevBrand;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelDevBrandAll, dbName, oDevBrand, sqlMapperId);
                lstDevBrand = oService.GetData() as IList<DevBrand>;
                Common.CloseService(oService);
                if (lstDevBrand == null || lstDevBrand.Count == 0)
                    return false;
                cbxBrand.Text = "";
                cbxBrand.Items.Clear();
                foreach (DevBrand vDevBrand in lstDevBrand)
                {
                    cbxBrand.Items.Add(vDevBrand.BrandNo + "-" + vDevBrand.BrandName);
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        private bool LoadCbxStockNoItems(PctStockUser oPctStockUser)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IPctStockUserDAO>.GetService();
            try
            {
                if (oPctStockUser == null) return false;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelPctStockUserAll, dbName, oPctStockUser, sqlMapperId);
                lstPctStockUser = oService.GetData() as IList<PctStockUser>;
                Common.CloseService(oService);
                cbxStock.Text = "";
                cbxStock.Items.Clear();
                cbxInOut.Items.Clear();
                if (lstPctStockUser == null || lstPctStockUser.Count == 0)
                    return false;
                foreach (PctStockUser vPctStockUser in lstPctStockUser)
                {
                    cbxStock.Items.Add(vPctStockUser.StockNo + "-" + vPctStockUser.StockName);
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        private bool LoadCbxInOutNoItems(DevStockInOutNo oDevStockInOutNo)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDevStockInOutNoDAO>.GetService();
            try
            {
                if (oDevStockInOutNo == null) return false;
                cbxInOut.Text = "";
                cbxInOut.Items.Clear();
                cbxPostNo.Text = "";
                cbxPostNo.Items.Clear();
                cbxOutReason.Text = "";
                cbxOutReason.Items.Clear();
                IList<DevStockInOutNo> lstDevStockInOutNo;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelDevStockInOutNoAll, dbName, oDevStockInOutNo, sqlMapperId);
                lstDevStockInOutNo = oService.GetData() as IList<DevStockInOutNo>;
                Common.CloseService(oService);
                if (lstDevStockInOutNo == null || lstDevStockInOutNo.Count == 0)
                    return false;
                foreach (DevStockInOutNo vDevStockInOutNo in lstDevStockInOutNo)
                {
                    cbxInOut.Items.Add(vDevStockInOutNo.InOutNo + "-" + vDevStockInOutNo.InOutName);
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        private bool LoadCbxOutReasonItems(DevOutReasonStock oDevOutReasonStock)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDevOutReasonStockDAO>.GetService();
            try
            {
                if (oDevOutReasonStock == null) return false;
                IList<DevOutReasonStock> lstDevOutReasonStock;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelDevOutReasonStockAll, dbName, oDevOutReasonStock, sqlMapperId);
                lstDevOutReasonStock = oService.GetData() as IList<DevOutReasonStock>;
                Common.CloseService(oService);
                if (lstDevOutReasonStock == null || lstDevOutReasonStock.Count == 0)
                    return false;
                cbxOutReason.Text = "";
                cbxOutReason.Items.Clear();
                foreach (DevOutReasonStock vDevOutReasonStock in lstDevOutReasonStock)
                {
                    cbxOutReason.Items.Add(vDevOutReasonStock.ReasonNo + "-" + vDevOutReasonStock.ReasonName);
                }
                if (cbxOutReason.Items.Count != 0)
                    cbxOutReason.SelectedIndex = 0;
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        private bool LoadCbxPostNoItems(DevStockidd oDevStockidd)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDevStockiddDAO>.GetService();
            try
            {
                if (oDevStockidd == null) return false;
                IList<DevStockidd> lstDevStockidd;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelDevStockiddAll, dbName, oDevStockidd, sqlMapperId);
                lstDevStockidd = oService.GetData() as IList<DevStockidd>;
                Common.CloseService(oService);
                if (lstDevStockidd == null || lstDevStockidd.Count == 0)
                    return false;
                cbxPostNo.Text = "";
                cbxPostNo.Items.Clear();
                foreach (DevStockidd vDevStockidd in lstDevStockidd)
                {
                    cbxPostNo.Items.Add(vDevStockidd.PosNo + "-" + vDevStockidd.PosName);
                }
                if (cbxPostNo.Items.Count != 0)
                    cbxPostNo.SelectedIndex = 0;
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        private bool LoadCbxOutSelectionItems(DeptGroup oDeptGroup)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDeptGroupDAO>.GetService();
            try
            {
                if (oDeptGroup == null) return false;
                IList<DeptGroup> lstDeptGroup;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelDeptGroupAll, dbName, oDeptGroup, sqlMapperId);
                lstDeptGroup = oService.GetData() as IList<DeptGroup>;
                Common.CloseService(oService);
                if (lstDeptGroup == null || lstDeptGroup.Count == 0)
                    return false;
                cbxOutSelection.Text = "";
                cbxOutSelection.Items.Clear();
                foreach (DeptGroup vDeptGroup in lstDeptGroup)
                {
                    cbxOutSelection.Items.Add(vDeptGroup.DepartId + "-" + vDeptGroup.DeptGroupName);
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        private bool LoadCbxOutUserItems(DeptGroupUser oDeptGroupUser)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDeptGroupUserDAO>.GetService();
            try
            {
                if (oDeptGroupUser == null) return false;
                IList<DeptGroupUser> lstDeptGroupUser;
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelDeptGroupUserAll, dbName, oDeptGroupUser, sqlMapperId);
                lstDeptGroupUser = oService.GetData() as IList<DeptGroupUser>;
                Common.CloseService(oService);
                if (lstDeptGroupUser == null || lstDeptGroupUser.Count == 0)
                    return false;
                cbxOutUser.Text = "";
                cbxOutUser.Items.Clear();
                if (outUserList != null) { outUserList.Clear(); }
                outUserList = new List<string>();
                foreach (DeptGroupUser vDeptGroupUser in lstDeptGroupUser)
                {
                    cbxOutUser.Items.Add(vDeptGroupUser.UsrId + "-" + vDeptGroupUser.UsrName);
                    outUserList.Add(vDeptGroupUser.UsrId + "-" + vDeptGroupUser.UsrName);
                }
                cbxOutUser.AutoCompleteCustomSource.AddRange(outUserList.ToArray());
                cbxOutUser.AutoCompleteSource = AutoCompleteSource.None;   // ListItems模糊查找时會閃爍，用CustomSource
                cbxOutUser.AutoCompleteMode = AutoCompleteMode.None;
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        private void EnableServerTypeControl(bool bolEnable)
        {
            chxAutomatic.Enabled = bolEnable;
            chxSemiautomatic.Enabled = bolEnable;
            chxManual.Enabled = tbxBarCode.Enabled;
            if (!bolEnable)
            {
                vBarNo = null;
                lblOldBarCode.Text = "";
                tbxBarCode.Text = "";
                chxAutomatic.Checked = bolEnable;
                chxSemiautomatic.Checked = bolEnable;
                chxManual.Checked = bolEnable;
                tbxBarCode.Enabled = bolEnable;
                oTagPool.ClearPool();
                dgHistory.Rows.Clear();
                if (oRfidControl != null && oRfidControl.IsConnected())
                {
                    ReceiveMsg oReceiveMsg = new ReceiveMsg(ReceiveMsg);
                    oRfidControl.RemoveMessageReceived(oReceiveMsg);
                    oRfidControl.ClearTagList();
                }
            }
        }

        private void ModifyDgHistory(DevBarno oDevBarno)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oDevStockIoBasic = ServiceFactory<IDevStockIoBasicDAO>.GetService();
            var oDevModelArticle = ServiceFactory<IDevModelArticleDAO>.GetService();
            try
            {
                if (oDevBarno == null)
                    return;
                int iRows = dgHistory.Rows.Count;
                string shipmentStatus = "";
                string modelName = "";
                object[] oRow = new object[10];
                oDevStockIoBasic.SetConnection(dbName, dbConn);
                oDevStockIoBasic.SetPara(SqlConst.SelDevStockIoBasicAll, dbName, new DevStockIoBasic()
                {
                    FactNo = oDevBarno.FactNo,
                    BrandNo = oDevBarno.BrandNo,
                    InoutNo = oDevBarno.InoutNo
                }, sqlMapperId);
                IList<DevStockIoBasic> lstDevStockIoBasic = oDevStockIoBasic.GetData() as IList<DevStockIoBasic>;
                Common.CloseService(oDevStockIoBasic);
                if (lstDevStockIoBasic != null && lstDevStockIoBasic.Count > 0)
                {
                    if (lstDevStockIoBasic[0].InoutKind == "A")
                    {
                        shipmentStatus = Convert.ToString(StockStatus.StockIn);
                    }
                    else if (lstDevStockIoBasic[0].InoutKind == "B")
                    {
                        shipmentStatus = Convert.ToString(StockStatus.StockOut);
                    }
                }

                if (!String.IsNullOrEmpty(oDevBarno.BrandNo) && oDevBarno.ArticId != null)
                {
                    oDevModelArticle.SetPara(SqlConst.SelDevModelArticleAll, dbName, new DevModelArticle()
                    {
                        BrandCode = oDevBarno.BrandNo,
                        ArticId = oDevBarno.ArticId
                    }, sqlMapperId);
                    IList<DevModelArticle> lstDevModelArticle = oDevModelArticle.GetData() as IList<DevModelArticle>;
                    Common.CloseService(oDevModelArticle);
                    if (lstDevModelArticle != null && lstDevModelArticle.Count > 0)
                        modelName = lstDevModelArticle[0].ProdName;
                }

                oRow[0] = shipmentStatus;
                oRow[1] = oDevBarno.PosNo;
                oRow[2] = modelName;
                oRow[3] = oDevBarno.ArticNo;
                oRow[4] = oDevBarno.SpecNo;
                oRow[5] = oDevBarno.SizeNo;
                oRow[6] = oDevBarno.LrMk;
                oRow[7] = oDevBarno.BarNo;
                oRow[8] = oDevBarno.RfidCode;
                oRow[9] = oDevBarno.DemMk;

                for (int i = 0; i < iRows; i++)
                {
                    if (Convert.ToString(dgHistory.Rows[i].Cells["RfidCode"].Value) == oDevBarno.RfidCode)
                    {
                        for (int j = 0; j < oRow.Length; j++)
                        {
                            dgHistory.Rows[i].Cells[j].Value = oRow[j];
                        }
                        return;
                    }
                }
                dgHistory.Rows.Add(oRow);
            }
            catch
            {
                throw;
            }
        }

        private string GetStockStatusDemMk(StockStatus oStockStatus)
        {
            switch (oStockStatus)
            {
                case StockStatus.StockOut:
                    return DEMAGNETIZE;
                default:
                    return MAGNETIZE;
            }
        }

        private bool SetAutoDemMk(string reasonNo)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDevOutReasonDAO>.GetService();
            IList<DevOutReason> lstDevOutReason;
            if (String.IsNullOrEmpty(reasonNo)) return false;
            oService.SetConnection(dbName, dbConn);
            oService.SetPara(SqlConst.SelDevOutReasonAll, dbName, new DevOutReason()
            {
                ReasonNo = reasonNo
            }, sqlMapperId);
            lstDevOutReason = oService.GetData() as IList<DevOutReason>;
            Common.CloseService(oService);
            if (lstDevOutReason == null || lstDevOutReason.Count == 0)
                return false;
            switch (lstDevOutReason[0].AutoDemMk)
            {
                case "Y":
                    vAutoDemMk = "Y";
                    vDemMk = lstDevOutReason[0].RfidLevel;
                    break;
                case "N":
                    vAutoDemMk = "N";
                    vDemMk = "";
                    break;
                default:
                    vAutoDemMk = "";
                    vDemMk = "";
                    break;
            }
            WriteMsg("ReasonNo=" + reasonNo + ";AutoDemMk=" + vAutoDemMk + ";DemMk=" + vDemMk);
            return true;
        }

        private bool ShipmentInOutByTagId(string epcCode)
        {
            string usrId, demIp, brandNo, stockNo, inoutNo, posNo, outSelect, inUser, outUser;
            bool bolProgTag = false;
            DevBarno oDevBarno, oDevBarnoOld;
            oDevBarno = SetDevBarnoByShipment(epcCode);
            if (oDevBarno == null)
                return false;
            oDevBarnoOld = new DevBarno()
            {
                BrandNo = oDevBarno.BrandNo,
                StockNo = oDevBarno.StockNo,
                InoutNo = oDevBarno.InoutNo,
                PosNo = oDevBarno.PosNo,
                ReasonNo = oDevBarno.ReasonNo,
                DepartId = oDevBarno.DepartId,
                OutUser = oDevBarno.OutUser
            };
            usrId = SrvStatic.usrId;
            demIp = SrvStatic.localIP;
            brandNo = cbxBrand.Text.Trim();
            stockNo = cbxStock.Text.Trim();
            inoutNo = cbxInOut.Text.Trim();
            if (brandNo.IndexOf('-') != -1)
                brandNo = brandNo.Split('-')[0];
            if (stockNo.IndexOf('-') != -1)
                stockNo = stockNo.Split('-')[0];
            if (inoutNo.IndexOf('-') != -1)
                inoutNo = inoutNo.Split('-')[0];
            oDevBarno.FactNo = vFactNo;
            oDevBarno.BrandNo = brandNo;
            oDevBarno.StockNo = stockNo;
            oDevBarno.InoutNo = inoutNo;
            oDevBarno.ModifyUser = usrId;
            oDevBarno.InoutDate = DateTime.Now.ToString("yyyyMMdd");
            oDevBarno.ScanTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            oDevBarno.ModifyDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            if ((InOutKind == "B" && vAutoDemMk == "Y") || InOutKind == "A")
                bolProgTag = true;
            if (bolProgTag && oRfidControl != null && oRfidControl.IsConnected())
            {
                oDevBarno.DemMk = vDemMk;
                oDevBarno.DemUser = usrId;
                oDevBarno.DemIp = demIp;
            }

            switch (InOutKind)
            {
                case "A":
                    posNo = cbxPostNo.Text.Trim();
                    inUser = cbxInUser.Text.Trim();
                    if (inUser.IndexOf('-') != -1)
                        inUser = inUser.Split('-')[0];
                    if (posNo.IndexOf('-') != -1)
                        posNo = posNo.Split('-')[0];
                    if (bolProgTag && String.IsNullOrEmpty(oDevBarno.DemMk))
                        oDevBarno.DemMk = GetStockStatusDemMk(StockStatus.StockIn);
                    oDevBarno.InUser = inUser;
                    oDevBarno.PosNo = posNo;
                    if (("," + stockNoIn + ",").Contains("," + stockNo + ","))
                        oDevBarno.DemMk = DEMAGNETIZE;
                    // 不允許重復入庫，2020-10-16
                    if (oDevBarnoOld.InoutNo != null && oDevBarnoOld.InoutNo.Length > 0 && oDevBarnoOld.InoutNo.Substring(0, 1) == "A")
                    {
                        WriteMsg(epcCode + " tag in warehouse already (" + oDevBarnoOld.StockNo + ")，not allow repeat operation");
                        return false;
                    }
                    break;
                case "B":
                    outSelect = cbxOutSelection.Text.Trim();
                    outUser = cbxOutUser.Text.Trim();
                    if (outSelect.IndexOf('-') != -1)
                        outSelect = outSelect.Split('-')[0];
                    if (outUser.IndexOf('-') != -1)
                        outUser = outUser.Split('-')[0];
                    if (bolProgTag && String.IsNullOrEmpty(oDevBarno.DemMk))
                        oDevBarno.DemMk = GetStockStatusDemMk(StockStatus.StockOut);
                    oDevBarno.DepartId = Convert.ToInt32(outSelect);
                    oDevBarno.OutUser = outUser;
                    oDevBarno.ReasonNo = vReasonNo;
                    // 不允許重復出庫，2020-10-16
                    if (oDevBarnoOld.InoutNo != null && oDevBarnoOld.InoutNo.Length > 0 && oDevBarnoOld.InoutNo.Substring(0, 1) == "B")
                    {
                        WriteMsg(epcCode + " has been sent out from warehouse (" + oDevBarnoOld.StockNo + ") already，not allow repeat operation");
                        return false;
                    }
                    // 出庫必需在鞋子所在倉庫，2020-10-16
                    if (oDevBarnoOld.StockNo != stockNo && oDevBarnoOld.InoutNo != null && oDevBarnoOld.InoutNo.Length > 0 && oDevBarnoOld.InoutNo.Substring(0, 1) == "A")
                    {
                        WriteMsg("Please send " + epcCode + " out from the corresponding warehouse " + oDevBarnoOld.StockNo);
                        return false;
                    }
                    // 必須先入庫才能出庫，2020-10-16
                    if (oDevBarnoOld.InoutNo == null || oDevBarnoOld.InoutNo.Length == 0)
                    {
                        WriteMsg(epcCode + " the shoe hasn't been put in a warehouse. ");
                        return false;
                    }
                    break;
            }

            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDevBarnoDAO>.GetService();
            var oSvrDevStockIo = ServiceFactory<IDevStockIoDAO>.GetService();
            //oService.BeginTransaction();
            try
            {
                // 檢查有無報廢, 2019-10-9
                if (vInOutKind == "A")
                {
                    DevStockIo oDevStockIo = new DevStockIo()
                    {
                        FactNo = vFactNo,
                        BrandNo = brandNo.ToUpper(),
                        StockNo = stockNo.ToUpper(),
                        InoutNo = inoutNo.ToUpper(),
                        BarNo = oDevBarno.BarNo
                    };
                    oSvrDevStockIo.SetPara(SqlConst.SelDevStockIoScrap, dbName, oDevStockIo, sqlMapperId);
                    IList<DevStockIo> listDevStockIo = oSvrDevStockIo.GetData() as IList<DevStockIo>;
                    Common.CloseService(oSvrDevStockIo);
                    if (listDevStockIo.Count > 0)
                    {
                        WriteMsg("Barcode No.: " + oDevBarno.BarNo + " 最後一筆的出庫掃瞄是報廢，不允許再入庫！");
                        return false;
                    }
                }
                if ((InOutKind == "B" && vAutoDemMk == "Y") || InOutKind == "A")
                    bolProgTag = true;
                // 是否禁用
                bool bFrozen = CheckFrozen(brandNo);
                if (bFrozen)
                {
                    MessageBox.Show("During the audit inventory period, Scan In/out is not allowed! \n\n 倉庫停用，不允許入出庫掃描！", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (bolProgTag && oRfidControl != null && oRfidControl.IsConnected())
                {
                    if (!ProgTagByShipment(epcCode, oDevBarno.DemMk))
                        throw new Exception("Program Tag Error:[" + epcCode + "_" + oDevBarno.DemMk + "]");
                }
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.UpdDevBarnoPk, dbName, oDevBarno, sqlMapperId);
                if (oService.GetChange(2) <= 0)
                {
                    //oService.RollBackTransaction();
                    throw new Exception("No data,update dev_barno error");
                }
                //oService.CommitTransaction();
                ModifyDgHistory(oDevBarno);
                return true;
            }
            catch
            {
                //oService.RollBackTransaction();
                throw;
            }
        }

        private DevBarno SetDevBarnoByShipment(string epcCode, string demMk = null)
        {
            string rfidCode;
            DevBarno oDevBarno = null;
            IList<DevBarno> lstDevBarno = null;
            if (epcCode.Length < CODE_LENGTH)
                return oDevBarno;
            rfidCode = epcCode.Substring(0, CODE_LENGTH);
            oDevBarno = new DevBarno();
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            var oService = ServiceFactory<IDevBarnoDAO>.GetService();
            oService.SetConnection(dbName, dbConn);
            oService.SetPara(SqlConst.SelDevBarnoAll, dbName, new DevBarno()
            {
                RfidCode = rfidCode
            }, sqlMapperId);
            lstDevBarno = oService.GetData() as IList<DevBarno>;
            Common.CloseService(oService);
            if (lstDevBarno == null || lstDevBarno.Count == 0)
                return null;
            oDevBarno.BarNo = lstDevBarno[0].BarNo;
            oDevBarno.BrandNo = lstDevBarno[0].BrandNo;
            oDevBarno.ArticNo = lstDevBarno[0].ArticNo;
            oDevBarno.ArticId = lstDevBarno[0].ArticId;
            oDevBarno.SpecNo = lstDevBarno[0].SpecNo + "-" + lstDevBarno[0].SpecVersion;
            oDevBarno.SizeNo = lstDevBarno[0].SizeNo;
            oDevBarno.LrMk = lstDevBarno[0].LrMk;
            oDevBarno.DepartId = lstDevBarno[0].DepartId;
            oDevBarno.InUser = lstDevBarno[0].InUser;
            oDevBarno.OutUser = lstDevBarno[0].OutUser;
            oDevBarno.InoutNo = lstDevBarno[0].InoutNo;
            oDevBarno.ReasonNo = lstDevBarno[0].ReasonNo;
            oDevBarno.PosNo = lstDevBarno[0].PosNo;
            oDevBarno.DemUser = lstDevBarno[0].DemUser;
            oDevBarno.DemIp = lstDevBarno[0].DemIp;
            oDevBarno.StockNo = lstDevBarno[0].StockNo;
            if (String.IsNullOrEmpty(demMk))
                demMk = lstDevBarno[0].DemMk;
            oDevBarno.RfidCode = rfidCode;
            oDevBarno.DemMk = demMk;
            return oDevBarno;
        }

        private bool ReadTagByManual()
        {
            if (oRfidControl != null && oRfidControl.IsConnected())
            {
                List<TagStruct> lstTagStruct;
                DevBarno oDevBarno;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    oTagPool.ClearPool();
                    lstTagStruct = oRfidControl.GetTagList();
                    foreach (TagStruct oTagStruct in lstTagStruct)
                    {
                        if (oTagStruct.TagID.Length < CODE_LENGTH)
                            continue;
                        oDevBarno = SetDevBarnoByShipment(oTagStruct.TagID, oTagStruct.TagID.Replace(oTagStruct.TagID.Substring(0, CODE_LENGTH), ""));
                        ModifyDgHistory(oDevBarno);
                        if (oDevBarno == null)
                            WriteMsg("TagID [" + oTagStruct.TagID + "] is not in database");
                    }
                    return true;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                throw new Exception("RFID Not Connected");
            }
        }

        private bool ProgTagByShipment(string epcCode, string demMk)
        {
            string progCode = "";
            string setCode = "";
            string maskCode = "";
            int maskLen = 0;
            if (oRfidControl == null || String.IsNullOrEmpty(demMk))
                return false;
            epcCode = epcCode.Replace(" ", "").Trim();
            if (epcCode.Length < CODE_LENGTH)
            {
                throw new Exception("Tag EPC less than 24 digits,EPC:" + epcCode);
            }

            try
            {
                if (epcCode.Length > CODE_LENGTH)
                    setCode = epcCode.Substring(0, CODE_LENGTH);
                setCode = setCode + demMk;
                if (setCode == epcCode)
                {
                    return true;
                }
                else
                {
                    maskLen = (epcCode.Replace(" ", "").Length - 4) * 4;
                    maskCode = epcCode.Substring(0, maskLen / 4);
                    SetTagMask("Include", Convert.ToString(maskLen), maskCode);
                    progCode = oRfidControl.ProgTagBySingle(1, setCode);
                }
            }
            catch (Exception ex)
            {
                progCode = "";
            }
            oRfidControl.SetReaderBySingle(ParameterNm.SetAcqG2Mask, "0");
            oRfidControl.SetReaderBySingle(ParameterNm.Mask, "0");
            if (progCode.Replace(" ", "").Trim() != setCode.Replace(" ", "").Trim())
            {
                throw new Exception("Program Tag Error [" + epcCode + "]");
            }
            oTagPool.AddTag(new TagStruct()
            {
                TagID = progCode,
                DiscoveryTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                OtherUse = Convert.ToString(TagPoolInType.ProgReadIn)
            });
            return true;
        }

        private void ReceiveMsg(string msg)
        {
            lock (this)
            {
                mathAction method = delegate
                {
                    string[] tagAttr;
                    TagStruct oTagStruct;
                    msg = msg.Replace("\0", "").Replace("\r\n", "!");
                    tagAttr = msg.Split('!');
                    try
                    {
                        for (int i = 0; i < tagAttr.Length; i++)
                        {
                            if (tagAttr[i].IndexOf('#') == -1 && tagAttr[i].IndexOf(',') != -1)
                            {
                                oTagStruct = new TagStruct()
                                {
                                    TagID = tagAttr[i].Split(',')[0].Replace(" ", ""),
                                    DiscoveryTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                    OtherUse = Convert.ToString(TagPoolInType.AutoReadIn)
                                };
                                if (oTagPool != null)
                                    oTagPool.AddTag(oTagStruct);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                        if (ex.InnerException != null)
                            err = ex.InnerException.Message;
                        WriteMsg("Receive Error:" + err);
                    }
                };
                this.BeginInvoke(method);
            }
        }

        private void TagPoolActionAdd(TagStruct oTagStruct)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if ((InOutKind == "A" || InOutKind == "B"))
                {
                    if (oTagStruct.OtherUse == Convert.ToString(TagPoolInType.ProgReadIn))
                        return;
                    if (usedServiceType == ServiceType.Semiautomatic)
                    {
                        DevBarno oDevBarno = SetDevBarnoByShipment(oTagStruct.TagID, oTagStruct.TagID.Replace(oTagStruct.TagID.Substring(0, CODE_LENGTH), ""));
                        ModifyDgHistory(oDevBarno);
                        return;
                    }
                    if (!ShipmentInOutByTagId(oTagStruct.TagID))
                        WriteMsg("1-New Tag Error [" + oTagStruct.TagID + "]");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                    err = ex.InnerException.Message;
                WriteMsg("1.1-New Tag Error:" + err);
                if (usedServiceType == ServiceType.Automatic)
                    oTagPool.RemoveTag(oTagStruct.TagID);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void TagPoolActionModify(TagStruct oTagStruct)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if ((InOutKind == "A" || InOutKind == "B"))
                {
                    if (oTagStruct.OtherUse != Convert.ToString(TagPoolInType.ManualReadIn))
                        return;
                    if (!ShipmentInOutByTagId(oTagStruct.TagID))
                        WriteMsg("2-Modify Tag Error [" + oTagStruct.TagID + "]");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                    err = ex.InnerException.Message;
                WriteMsg("2.1-Modify Tag Error:" + err);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SetTagMask(string maskAction, string maskLen, string maskCode)
        {
            oRfidControl.SetReaderBySingle(ParameterNm.AcqG2MaskAction, maskAction);
            oRfidControl.SetReaderBySingle(ParameterNm.SetAcqG2Mask, new Parameters()
            {
                MEMORY_BANK = "1",
                BITLEN = maskLen,
                BITPTR = "32",
                HEX_BYTES = maskCode
            });
        }

        private bool ConnectRfidReader()
        {
            string comPort = cbxComPort.Text;
            List<String> lstPorNm;
            try
            {
                ReaderType readerType = (ReaderType)Convert.ToInt32(SrvStatic.readerType);
                oRfidControl = new ReaderAdapter(readerType);
                oRfidControl.iReader.WriteTime(SrvStatic.MinPower, SrvStatic.MaxPower, SrvStatic.TryTime);
                oRfidControl.SetSerialPort(comPort);
                if (String.IsNullOrEmpty(comPort) || !oRfidControl.OpenConnect())
                {
                    comPort = "";
                    lstPorNm = oRfidControl.GetSerialPortNm();
                    for (int i = 0; i < lstPorNm.Count; i++)
                    {
                        oRfidControl.SetSerialPort(lstPorNm[i]);
                        WriteMsg("Try use " + lstPorNm[i] + " to connect reader...");
                        if (oRfidControl.OpenConnect())
                        {
                            comPort = lstPorNm[i];
                            break;
                        }
                    }

                    if (String.IsNullOrEmpty(comPort))
                        throw new Exception("Can not connect RFID Reader");
                }
                cbxComPort.Text = comPort;
                RfidCommandGroup(RfidCommandGroupNm.InitReader);
                btnConnect.Text = "DISCONNECT";
                WriteMsg("CONNECT SUCCESS");
                SrvStatic.comPort = comPort;
                oReadWriteJson.WriteJson(new PropName() { ComPort = comPort });
                return true;
            }
            catch
            {
                cbxComPort.Enabled = true;
                throw;
            }
        }

        private bool DisconnectRfidReader()
        {
            if (oRfidControl != null && !oRfidControl.Disconnect())
            {
                MessageBox.Show("Can not disconnect RFID Reader");
            }
            btnConnect.Text = "CONNECT";
            WriteMsg("DISCONNECT SUCCESS");
            chxManual.Checked = false;
            chxSemiautomatic.Checked = false;
            chxAutomatic.Checked = false;
            return true;
        }

        private bool RfidCommandGroup(RfidCommandGroupNm groupNm)
        {
            try
            {
                if (oRfidControl == null)
                    return false;
                switch (groupNm)
                {
                    case RfidCommandGroupNm.InitReader:
                        oRfidControl.SetReaderBySingle(ParameterNm.NotifyMode, "OFF");
                        oRfidControl.SetReaderBySingle(ParameterNm.AutoMode, "OFF");
                        oRfidControl.SetReaderBySingle(ParameterNm.AntennaSequence, AppConfig.AntennaSequence);
                        oRfidControl.SetReaderBySingle(ParameterNm.RFLevel, AppConfig.RFLevel);
                        oRfidControl.SetReaderBySingle(ParameterNm.RFAttenuation, AppConfig.RFAttenuation);
                        oRfidControl.SetReaderBySingle(ParameterNm.ComTimeOutInterval, AppConfig.ComTimeOutInterval);
                        oRfidControl.SetReaderBySingle(ParameterNm.NetworkTimeout, AppConfig.NetworkTimeout);
                        oRfidControl.SetReaderBySingle(ParameterNm.RSSIFilter, AppConfig.RssiFilter);
                        break;
                    case RfidCommandGroupNm.ListeninTagList:
                        oRfidControl.SetReaderBySingle(ParameterNm.TagListFormat, "Terse");
                        oRfidControl.SetReaderBySingle(ParameterNm.NotifyTime, "0");
                        oRfidControl.SetReaderBySingle(ParameterNm.NotifyFormat, "Terse");
                        oRfidControl.SetReaderBySingle(ParameterNm.NotifyTrigger, "Add");
                        oRfidControl.SetReaderBySingle(ParameterNm.PersistTime, "1");
                        oRfidControl.SetReaderBySingle(ParameterNm.NotifyAddress, "Serial");
                        oRfidControl.SetReaderBySingle(ParameterNm.NotifyMode, "ON");
                        oRfidControl.SetReaderBySingle(ParameterNm.AutoMode, "ON");
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        public string FormatStr(string vPara, int iChar = 2)
        {
            string newStr = "";
            char[] arrChar = vPara.Replace(" ", "").ToCharArray();
            for (int i = 0; i < arrChar.Length; i++)
            {
                newStr += arrChar[i];
                if ((i + 1) % iChar == 0)
                    newStr += " ";
            }
            return newStr.Trim();
        }

        private void WriteMsg(string msg)
        {
            if (lbxMsg.Items.Count > 50) { lbxMsg.Items.RemoveAt(lbxMsg.Items.Count - 1); }
            lbxMsg.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + msg);
            lbxMsg.SelectedIndex = 0;
        }

        private void ShowException(Exception ex)
        {
            string err = ex.ToString();
            if (ex.InnerException != null)
            {
                err = ex.InnerException.ToString();
            }
            MessageBox.Show(err);
        }
        #endregion

        private void cbxPostNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckStatus();
        }

        private void cbxOutUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckStatus();
        }

        private void cbxInUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckStatus();
        }

        private void cbxInUser_TextUpdate(object sender, EventArgs e)
        {
            if (inUserList == null) { return; }
            var data = inUserList.ToArray();
            var input = cbxInUser.Text.ToUpper();
            cbxInUser.Items.Clear();
            if (string.IsNullOrWhiteSpace(input)) cbxInUser.Items.AddRange(data);
            else
            {
                var newList = data.Where(x => x.IndexOf(input, StringComparison.CurrentCultureIgnoreCase) != -1).ToArray();
                if (newList.Length > 0)
                    cbxInUser.Items.AddRange(newList);
                else
                    cbxInUser.Items.Add("");
            }
            cbxInUser.Select(input.Length, 0);
            cbxInUser.DroppedDown = true;
            Cursor = Cursors.Default;
            CheckStatus();
        }

        private void cbxOutUser_TextUpdate(object sender, EventArgs e)
        {
            if (outUserList == null) { return; }
            var data = outUserList.ToArray();
            var input = cbxOutUser.Text.ToUpper();
            cbxOutUser.Items.Clear();
            if (string.IsNullOrWhiteSpace(input)) cbxOutUser.Items.AddRange(data);
            else
            {
                var newList = data.Where(x => x.IndexOf(input, StringComparison.CurrentCultureIgnoreCase) != -1).ToArray();
                if (newList.Length > 0)
                    cbxOutUser.Items.AddRange(newList);
                else
                    cbxOutUser.Items.Add("");
            }
            cbxOutUser.Select(input.Length, 0);
            cbxOutUser.DroppedDown = true;
            Cursor = Cursors.Default;
            CheckStatus();
        }

        private void tabShipment_DoubleClick(object sender, EventArgs e)
        {
            //  For Test
            string s1 = cbxOutSelection.Text;
            string s2 = cbxOutUser.Text;
            MessageBox.Show("s1:" + s1 + "\n\ns2:" + s2);
        }

        private void cbxStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxOutSelection.Text = "";
            cbxOutUser.Text = "";
            cbxOutSelection.Items.Clear();
            cbxOutUser.Items.Clear();
            cbxOutSelection.Enabled = true;
            cbxOutUser.Enabled = true;
            bool bOutIdscan = false;
            string stockNo = cbxStock.Text.Trim();
            if (stockNo.IndexOf('-') != -1 && lstPctStockUser.Count > 0)
            {
                stockNo = stockNo.Split('-')[0];
                List<PctStockUser> listStock = lstPctStockUser.Where(p => p.StockNo == stockNo).ToList();
                bOutIdscan = listStock != null && listStock.Count > 0 && listStock[0].OutIdscan == "Y" ? true : false;
                bManual = isManualScan("T4", stockNo);
                tbxBarCode.Enabled = bManual;
            }
            cbxOutSelection.Enabled = !bOutIdscan;
            cbxOutUser.Enabled = !bOutIdscan;
            btnChkUser.Enabled = bOutIdscan;
            stockNoIn = GetStockIn("DZ");
        }

        /// <summary>是否可用手動掃描輸入框 </summary>
        private bool isManualScan(string kindNo, string stockNo)
        {
            bool bRet = false;
            var oService = ServiceFactory<IPctUserDAO>.GetService();
            oService.SetConnection(dbName, dbConn);
            oService.SetPara(SqlConst.SelManualScan, dbName, new PctUser() { Alais = kindNo, GroupId = stockNo }, sqlMapperId);
            IList<PctUser> listData = oService.GetData();
            Common.CloseService(oService);
            bRet = (listData != null && listData.Count > 0) ? true : false;
            return bRet;
        }

        /// <summary> 取得入库时需要消磁的仓别 20220923  </summary>
        private string GetStockIn(string kindNo)
        {
            string retVal = "";
            var oService = ServiceFactory<IPctUserDAO>.GetService();
            oService.SetConnection(dbName, dbConn);
            oService.SetPara(SqlConst.SelStockNo, dbName, new PctUser() { DeptNo = kindNo }, sqlMapperId);
            IList<PctUser> listData = oService.GetData();
            Common.CloseService(oService);
            if (listData != null && listData.Count > 0)
                retVal = listData[0].DeptNo;    // 在映射栏位时用 dept_no 代 stock_no
            return retVal;
        }

        private void btnChkUser_Click(object sender, EventArgs e)
        {
            FrmCheckUser oFrmCheckUser = new FrmCheckUser(cbxOutSelection, cbxOutUser);
            oFrmCheckUser.ShowDialog();
        }
    }

    public enum TagPoolInType
    {
        ManualReadIn = 0,
        AutoReadIn = 1,
        ProgReadIn = 2
    };

    public enum StockStatus
    {
        StockIn = 0,
        StockOut = 1
    };

    public enum ServiceType
    {
        Manual = 0,
        Automatic = 1,
        Semiautomatic = 2
    };
}