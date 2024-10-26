using System;
using System.Data;
using System.Text;
using Microsoft.Win32;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
//Ref Others
using Generic;
using RFID.Command;
using RFID.Adapter;
using RFIDModel.Model.POJO;
using RFIDModel.Interface.POJO;
using RFIDModel.Common;
using System.Transactions;
using RFIDModel.Interface.DTO;
using RFIDModel.Model.DTO;
using Newtonsoft.Json;
using System.ServiceModel;
using System.Threading;

namespace RFIDClient
{
    public partial class FrmTagWrite : Form
    {
        int iOkNum = 0, iTagNum = 0;
        string rfidSerialPortNm = "";
        const string INIT_CODE = "000000000000000000000000";
        string make_no = "";
        string sqlMapperId = Common.SqlMapperId;
        ReaderAdapter oReader = null;
        ReadWriteJson oReadWriteJson = new ReadWriteJson();
        private string RFAttenuation = AppConfig.RFAttenuation;
        private string ComTimeOutInterval = AppConfig.ComTimeOutInterval;
        private string NetworkTimeout = AppConfig.NetworkTimeout;
        string dbName = AppConfig.CurrentDBName;
        string dbConn = AppConfig.CurrentDBConn;

        public FrmTagWrite()
        {
            InitializeComponent();
            this.Text = Lang.Dict("C0008", "制程掃描");
        }

        /// <summary> 窗口加載時 </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            BindComPort();
            BindCbxStep();
            ResetAll();
        }

        /// <summary>綁定COM</summary>
        private void BindComPort()
        {
            //cbPortName.SelectedIndexChanged -= new EventHandler(cbPortName_SelectedIndexChanged);
            BindingSource oBs = new BindingSource();
            oBs.DataSource = AppConfig.ComPortList;
            cbPortName.DataSource = oBs;
            cbPortName.DisplayMember = "Value";
            cbPortName.ValueMember = "Key";
            //cbPortName.SelectedIndexChanged += new EventHandler(cbPortName_SelectedIndexChanged);
            List<PropName> paraList = oReadWriteJson.ReadJson();
            PropName oPropName = paraList != null && paraList.Count > 0 ? paraList[0] : new PropName();
            SrvStatic.comPort = oPropName.ComPort == null ? "" : oPropName.ComPort;
            SrvStatic.readerType = oPropName.ReaderType == null ? "" : oPropName.ReaderType;
            SrvStatic.MinPower = string.IsNullOrEmpty(oPropName.ReadPower) ? 0 : Convert.ToInt32(oPropName.ReadPower);
            SrvStatic.MaxPower = string.IsNullOrEmpty(oPropName.WritePower) ? 0 : Convert.ToInt32(oPropName.WritePower);
            SrvStatic.TryTime = string.IsNullOrEmpty(oPropName.WriteTime) ? 0 : Convert.ToInt32(oPropName.WriteTime);
            cbPortName.SelectedValue = SrvStatic.comPort;
        }

        private void cbxStep_Leave(object sender, EventArgs e)
        {
            if (cbxStep.SelectedItem != null)
            {
                PropName propName = new PropName();
                propName.StepNo = ((DevMake)cbxStep.SelectedItem).MakeNo;
                oReadWriteJson.WriteJson(propName);
            }
        }

        private void BindCbxStep()
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oServiceDevMake = ServiceFactory<IDevMakeDAO>.GetService();
            oServiceDevMake.SetConnection(dbName, dbConn);
            oServiceDevMake.SetPara(SqlConst.SelDevMakeAll, dbName, new DevMake() { StopMk = "N" }, sqlMapperId);
            IList<DevMake> listDevMake = oServiceDevMake.GetData();
            Common.CloseService(oServiceDevMake);
            cbxStep.DataSource = null;
            cbxStep.DataSource = listDevMake;
            cbxStep.DisplayMember = "MakeName";
            cbxStep.ValueMember = "MakeNo";
            List<PropName> paraList = oReadWriteJson.ReadJson();
            PropName oPropName = paraList != null && paraList.Count > 0 ? paraList[0] : new PropName();
            cbxStep.SelectedValue = oPropName.StepNo;
        }

        /// <summary> 連接 / 斷開  </summary>
        private void picConn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (oReader == null || !oReader.IsConnected())
                SetBaseConfig();
            else
                DisconnectReader();
            cbPortName.Enabled = (oReader == null || !oReader.IsConnected()) ? true : false;
            cbxStep.Enabled = cbPortName.Enabled;
            this.Cursor = Cursors.Default;
            if (oReader != null && oReader.IsConnected())
            {
                oReadWriteJson.WriteJson(new PropName() { ComPort = cbPortName.Text.Trim(), StepNo = Convert.ToString(cbxStep.SelectedValue) });
            }
        }

        /// <summary> 連接 Reader </summary>
        public bool ConnectReader(string portNm = null, string hostBaudRate = null)
        {
            try
            {
                if (!Common.IsEnabledUSB()) { SetStatus("【INFO】USB has been locked."); Common.InvokeBeep(50000, 500, 2); return false; }
                if (oReader == null)
                {
                    ReaderType readerType = (ReaderType)Convert.ToInt32(SrvStatic.readerType);
                    oReader = new ReaderAdapter(readerType);
                    oReader.iReader.WriteTime(SrvStatic.MinPower, SrvStatic.MaxPower, SrvStatic.TryTime);
                }
                oReader.SetReaderBySingle(ParameterNm.RFAttenuation, RFAttenuation);
                oReader.SetReaderBySingle(ParameterNm.RSSIFilter, AppConfig.RssiFilter);
                oReader.SetReaderBySingle(ParameterNm.RFLevel, AppConfig.RFLevel);
                if (string.IsNullOrWhiteSpace(portNm) || rfidSerialPortNm == "") { GetPortName(); }
                if (oReader != null && !oReader.IsConnected()) { oReader.OpenConnect(); }
                SetReaderRequire();
                if (oReader == null || !oReader.IsConnected()) { return false; }
                txtBarcode.Select();
            }
            catch
            {
                Common.InvokeBeep(50000, 500, 2);
                return false;
            }
            return true;
        }

        /// <summary> 關閉窗口時 </summary>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisconnectReader();
            this.Close();
            this.Dispose();
            oReadWriteJson = null;
            GC.Collect();
        }

        /// <summary> 選擇所有 </summary>
        private void txtBarcode_Click(object sender, EventArgs e)
        {
            txtBarcode.SelectAll();
            picStatus.Image = null;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //偵測有無斷線過
                if (oReader != null && !oReader.IsConnected())
                {
                    DisconnectReader();
                }
            }
            catch
            {
                picStatus.Image = Properties.Resources.Error;
                SetStatus("【ERR】Happen bug, please contact administrator.");
                Common.InvokeBeep(50000, 500, 2);
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary> 回車寫Tag </summary>
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            string barcode = txtBarcode.Text.Trim();
            bool bolSave = true;
            try
            {
                if (e.KeyCode == Keys.Enter && barcode != "")
                {
                    txtBarcode.Enabled = false;
                    if (cbxStep.SelectedValue == null) { SetStatus("【INFO】Please select a step."); return; }
                    make_no = cbxStep.SelectedValue.ToString().Trim();
                    txtBarcode.SelectAll();
                    picStatus.Image = null;
                    dgvBarcode.Rows[0].Cells[0].Value = "";
                    dgvBarcode.Rows[0].Cells[1].Value = "";
                    dgvBarcode.Rows[0].Cells[2].Value = "";
                    dgvBarcode.Rows[0].Cells[3].Value = "";
                    dgvBarcode.Rows[0].Cells[4].Value = "";
                    dgvBarcode.Rows[0].Cells[5].Value = "";
                    dgvBarcode.Rows[0].Cells[0].Value = barcode;
                    Application.DoEvents();
                    this.Cursor = Cursors.WaitCursor;

                    if (oReader == null || !oReader.IsConnected())
                    {
                        SetStatus("【INFO】Reader has been disconnected, please try connect again.");
                        this.Cursor = Cursors.Default;
                        picStatus.Image = Properties.Resources.Error;
                        picConn.Image = Properties.Resources.disconnect;
                        txtBarcode.Text = "";
                        Common.InvokeBeep(50000, 500, 2);
                        return;
                    }

                    bolSave = SaveData();
                    if (bolSave)
                    {
                        SetStatus("【OK】Successful");
                        Common.InvokeBeep(50000, 500);
                        iOkNum++;
                        picStatus.Image = Properties.Resources.Success;
                        lblOkNum.Text = iOkNum.ToString();
                    }

                    txtBarcode.Text = "";
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtBarcode.Enabled = true;
                txtBarcode.Focus();
            }
        }

        /// <summary> 初始化基本配置 </summary>
        private void SetBaseConfig()
        {
            bool bConn = ConnectReader(cbPortName.Text.Trim(), "");
            if (!bConn)
            {
                picConn.Image = Properties.Resources.disconnect;
                SetStatus("【INFO】Reader can not connect.");
            }
            else
            {
                picConn.Image = Properties.Resources.connect;
                SetStatus("【OK】Connect success.");
                txtBarcode.Select();
            }
            cbPortName.Enabled = (oReader == null || !oReader.IsConnected()) ? true : false;
            cbxStep.Enabled = cbPortName.Enabled;
        }

        /// <summary>保存資料</summary>
        private bool SaveData()
        {
            string progCode = "", rfidCode = "", tagEpc = "";
            DevBarno oDevBarno;
            DevMakePro oDevMakePro;
            DevMake oDevMake;
            IList<DevBarno> dataList;
            IList<DevMake> tmpList;
            DateTime st, et;
            st = DateTime.Now;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oCallbackProxy = ServiceFactory<IClientSendMsg>.GetService();
            var oDMService = ServiceFactory<IDataManipulationDAO>.GetService();
            var oServiceDevBarno = ServiceFactory<IDevBarnoDAO>.GetService();
            var oServiceDevMake = ServiceFactory<IDevMakeDAO>.GetService();
            var oServicePctUser = ServiceFactory<IPctUserDAO>.GetService();
            try
            {
                picStatus.Image = null;
                lblOrigCode.Text = "";
                lblCurrCode.Text = "";
                lblCurrCode.ForeColor = Color.DarkBlue;
                if (dbName == null) { SetStatus("【ERR】CurrentDBName is NULL."); return false; }
                //查詢 DEV_BARNO
                oDevBarno = new DevBarno();
                oDevBarno.BarNo = txtBarcode.Text.Trim().ToUpper();
                oServiceDevBarno.SetConnection(dbName, dbConn);
                oServiceDevBarno.SetPara(SqlConst.SelDevBarnoAll, dbName, oDevBarno, sqlMapperId);
                dataList = oServiceDevBarno.GetData();
                if (dataList == null || dataList.Count <= 0)
                {
                    picStatus.Image = Properties.Resources.Error;
                    SetStatus("【ERR】The barcode [" + oDevBarno.BarNo + "] not exist in DEV_BARNO OR DEV_BARNO.RFID_CODE is null.");
                    IList<TagStruct> tagList = oReader.GetTagList();
                    int iTagCnt = tagList.Count;
                    if (iTagCnt != 1)
                    {
                        lblOrigCode.Text = "";
                        if (iTagCnt == 0)
                            SetStatus("【ERR】No tag found or Unknown tag error.");
                        else
                            SetStatus("【ERR】Write tag fail, because more than one tag in the Field.");
                    }
                    else
                    {
                        tagEpc = GetTagEPC();
                        lblOrigCode.Text = tagEpc;
                    }
                    Common.InvokeBeep(50000, 500, 2);
                    return false;
                }

                dgvBarcode.Rows[0].Cells[0].Value = dataList[0].BarNo;
                dgvBarcode.Rows[0].Cells[1].Value = dataList[0].ArticNo;
                dgvBarcode.Rows[0].Cells[2].Value = dataList[0].SpecNo + "-" + dataList[0].SpecVersion;
                dgvBarcode.Rows[0].Cells[3].Value = dataList[0].SizeNo;
                dgvBarcode.Rows[0].Cells[4].Value = dataList[0].LrMk;
                dgvBarcode.Rows[0].Cells[5].Value = dataList[0].RfidCode;
                rfidCode = dataList[0].RfidCode.Trim();
                rfidCode = rfidCode.Length == 20 ? rfidCode.PadRight(24, '0') : rfidCode;

                //新增 DEV_MAKE_PRO
                oDevMakePro = new DevMakePro();
                oDevMakePro.FactNo = dataList[0].FactNo;
                oDevMakePro.BrandNo = dataList[0].BrandNo;
                oDevMakePro.BarNo = dataList[0].BarNo;
                oDevMakePro.MakeNo = make_no;
                oDevMakePro.MakeDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                oDevMakePro.ModifyUser = SrvStatic.usrId;
                oDevMakePro.ModifyDate = DateTime.Now.ToString("yyyyMMddHHmmss");

                string inOutDate = dataList[0].InoutDate;
                string intOUtNo = dataList[0].InoutNo;
                oDevBarno.StockNo = dataList[0].StockNo;
                oDevBarno.PosNo = dataList[0].PosNo;
                oDevBarno.InoutNo = intOUtNo;
                oDevBarno.InoutDate = inOutDate;
                //更新 DEV_BARNO
                oDevMake = new DevMake();
                oServiceDevMake.SetPara(SqlConst.SelDevMakeAuto, dbName, oDevMake, sqlMapperId);
                tmpList = oServiceDevMake.GetData();
                if (tmpList != null)
                {
                    // 1. 如果條碼有入出庫且 dev_barno.inout_no=B 開頭，並且dev_make的自動設定儲位等為空， 2020-07-01
                    // 2. 如果條碼未入出庫，且dev_make的自動設定儲位等為空
                    if (!string.IsNullOrEmpty(inOutDate) && tmpList.Count == 0 && (!string.IsNullOrEmpty(intOUtNo) && intOUtNo.Substring(0, 1) == "B")
                        || string.IsNullOrEmpty(inOutDate) && tmpList.Count == 0)
                    {
                        oDevBarno.StockNo = "";
                        oDevBarno.PosNo = "";
                        oDevBarno.InoutNo = "";
                        oDevBarno.InoutDate = "";
                        oDevBarno.ModifyUser = SrvStatic.usrId;
                        oDevBarno.ModifyDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                    else if (string.IsNullOrEmpty(inOutDate) && tmpList.Count > 0)  // 如果條碼未入出庫，但 dev_make設定的有值
                    {
                        oDevBarno.StockNo = tmpList[0].AutoInStockNo;
                        oDevBarno.PosNo = tmpList[0].AutoInPosNo;
                        oDevBarno.InoutNo = tmpList[0].AutoInNo;
                        oDevBarno.InoutDate = DateTime.Now.ToString("yyyyMMdd");
                        oDevBarno.ModifyUser = SrvStatic.usrId;
                        oDevBarno.ModifyDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
                oDevBarno.RfidUser = SrvStatic.usrId;
                oDevBarno.ScanTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                //編寫標簽
                progCode = SetTagEpc(rfidCode, INIT_CODE);
                progCode = progCode.Replace(" ", "");
                rfidCode = rfidCode.Replace(" ", "");

                // 是否啟用【有收貨單號的樣品，不列入產量】
                oServicePctUser.SetPara(SqlConst.SelRfidRecNo, dbName, new PctUser(), sqlMapperId);
                IList<PctUser> listData = oServicePctUser.GetData();
                bool bRecNo = (listData != null && listData.Count > 0 && dataList[0].RecNo != null && !string.IsNullOrEmpty(dataList[0].RecNo)) ? true : false;
                //處理資料 dataList[0].BarNo
                Dictionary<string, string> arrPara = new Dictionary<string, string>();
                if (bRecNo)
                    SetStatus("【INFO】" + oDevBarno.BarNo + " 有收貨單號的樣品，不列入產量計算");
                else
                    arrPara.Add(SqlConst.InsDevMakePro + "#" + oDevMakePro.GetType().FullName + "#1", JsonConvert.SerializeObject(oDevMakePro));
                arrPara.Add(SqlConst.UpdDevBarno + "#" + oDevBarno.GetType().FullName + "#2", JsonConvert.SerializeObject(oDevBarno));
                DataManipulation dataManipulation = new DataManipulation() { Data = arrPara };
                if (VerifyTag(rfidCode, progCode))
                {
                    bool bResult = oDMService.ExecSave(dbName, dataManipulation, sqlMapperId);
                    if (!bResult)
                    {
                        oCallbackProxy.ClientSendMsg(System.Net.Dns.GetHostName() + "【" + rfidCode + "】資料異動失敗.");
                        //復原 Tag 值
                        SetTagEpc(INIT_CODE, rfidCode);
                        Common.InvokeBeep(50000, 500, 2);
                        SetStatus("【ERR】Save data fail. ");
                        lblCurrCode.Text = INIT_CODE;
                        lblCurrCode.ForeColor = Color.Red;
                        picStatus.Image = Properties.Resources.Error;
                        return false;
                    }
                    lblCurrCode.ForeColor = Color.FromArgb(0, 192, 0);
                    oCallbackProxy.ClientSendMsg(System.Net.Dns.GetHostName() + "【" + rfidCode + "】Tag 寫入成功.");
                }
                else
                {
                    oCallbackProxy.ClientSendMsg(System.Net.Dns.GetHostName() + "【" + rfidCode + "】Tag 寫入失敗.");
                    return false;
                };

                //顯示資料
                string[] arrCell = { dataList[0].BarNo, dataList[0].ArticNo, dataList[0].SpecNo + "-" + dataList[0].SpecVersion, dataList[0].SizeNo, dataList[0].LrMk, dataList[0].RfidCode };
                int r = dgvHistory.Rows.Count;
                if (r == 0)
                {
                    dgvHistory.Rows.Add();
                    for (var k = 0; k < arrCell.Length; k++) { dgvHistory.Rows[r].Cells[k].Value = arrCell[k]; }
                }
                else
                {
                    DataGridViewRow oNewRow = new DataGridViewRow();
                    for (var k = 0; k < arrCell.Length; k++)
                    {
                        DataGridViewTextBoxCell oCell = new DataGridViewTextBoxCell();
                        oCell.Value = arrCell[k];
                        oNewRow.Cells.Add(oCell);
                    }
                    oNewRow.DefaultCellStyle = dgvHistory.Rows[0].DefaultCellStyle;
                    dgvHistory.Rows.Insert(0, oNewRow);
                }
                dgvHistory.ClearSelection();
                dgvHistory.Rows[0].Cells[0].Selected = true;
                return true;
            }
            catch (Exception ex)
            {
                Common.CurStatusMsg = ex.Message;
                Common.InvokeBeep(50000, 500, 2);
                SetStatus("【ERR】Exception Error " + ex.Message);
                lblCurrCode.Text = INIT_CODE;
                lblCurrCode.ForeColor = Color.Red;
                picStatus.Image = Properties.Resources.Error;
                return false;
            }
            finally
            {
                et = DateTime.Now;
                TimeSpan ts = et.Subtract(st);
                //關閉通道
                Common.CloseService(oCallbackProxy);
                Common.CloseService(oDMService);
                Common.CloseService(oServiceDevBarno);
                Common.CloseService(oServiceDevMake);
                Common.CloseService(oServicePctUser);
                //MessageBox.Show(string.Format("運行時間: {0}", ts.TotalMilliseconds));
            }
        }

        /// <summary>設置Reader必需參數 </summary>
        private void SetReaderRequire()
        {
            if (oReader != null && oReader.IsConnected())
            {
                oReader.SetReaderBySingle(ParameterNm.ProgEPCData, Common.FormatStr(INIT_CODE));
                oReader.SetReaderBySingle(ParameterNm.ComTimeOutInterval, ComTimeOutInterval);
                oReader.SetReaderBySingle(ParameterNm.NetworkTimeout, NetworkTimeout);
                oReader.SetReaderBySingle(ParameterNm.RFAttenuation, RFAttenuation);
                oReader.SetReaderBySingle(ParameterNm.AcqG2Mask, "0");
                oReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Include");
                oReader.SetReaderBySingle(ParameterNm.ProgDataUnit, "Word");
                oReader.SetReaderBySingle(ParameterNm.ProgEPCDataInc, "OFF");
                oReader.SetReaderBySingle(ParameterNm.NotifyMode, "OFF");
                oReader.SetReaderBySingle(ParameterNm.NotifyTrigger, "OFF");
                oReader.SetReaderBySingle(ParameterNm.AutoAction, "Acquire");
                oReader.SetReaderBySingle(ParameterNm.TagListFormat, "Terse");
                oReader.SetReaderBySingle(ParameterNm.AutoMode, "ON");
            }
        }

        /// <summary>讀取標簽</summary>
        private string GetTagEPC()
        {
            try
            {
                string tagData = "", retTag = "";
                oReader.SetReaderBySingle(ParameterNm.RFAttenuation, RFAttenuation);
                oReader.SetReaderBySingle(ParameterNm.RSSIFilter, AppConfig.RssiFilter);
                oReader.SetReaderBySingle(ParameterNm.AcqG2Mask, "0");
                tagData = oReader.ReadTagByBank(1, "2", "6");
                retTag = tagData.Replace(" ", "").Trim();
                return retTag;
            }
            catch (Exception ex)
            {
                SetStatus("【WARN】GetTagEPC Exception " + ex.Message);
                return "";
            }
        }

        /// <summary>寫入標簽</summary>
        private string SetTagEpc(string epcCode, string maskCode = null)
        {
            string maskLen, progCode = "";
            try
            {
                epcCode = Common.FormatStr(epcCode);
                maskCode = Common.FormatStr(maskCode);
                maskLen = Convert.ToString(maskCode.Replace(" ", "").Length * 4);
                oReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Include");
                //oReader.SetAcqG2Mask("1", "32", maskLen, maskCode);
                oReader.SetReaderBySingle(ParameterNm.SetAcqG2Mask, new Parameters()
                {
                    MEMORY_BANK = "1",
                    BITPTR = "32",
                    BITLEN = maskLen,
                    HEX_BYTES = maskCode
                });
                oReader.ClearTagList();
                progCode = oReader.ProgTagBySingle(1, epcCode);
            }
            catch (Exception ex)
            {
                SetStatus("【WARN】SetTagEpc Exception " + ex.Message);
            }
            progCode = progCode.Replace(" ", "");
            return progCode;
        }

        /// <summary>驗證標簽</summary>
        private bool VerifyTag(string oldCode, string newCode)
        {
            string epcCode, orgEpc;
            IList<TagStruct> tagList;
            try
            {
                if (!String.IsNullOrEmpty(oldCode) && !String.IsNullOrEmpty(newCode) && oldCode == newCode)
                {
                    lblOrigCode.ForeColor = Color.FromArgb(51, 153, 255);
                    lblOrigCode.Text = INIT_CODE.Replace(" ", "");
                    lblCurrCode.Text = newCode.PadRight(24, '0');
                    return true;
                }
                else
                {
                    picStatus.Image = Properties.Resources.Error;
                    lblOrigCode.ForeColor = Color.Red;
                    Common.InvokeBeep(50000, 500, 2);
                    tagList = oReader.GetTagList();
                    iTagNum = tagList.Count;
                    if (iTagNum != 1)
                    {
                        lblOrigCode.Text = "";
                        orgEpc = GetTagEPC();
                        lblOrigCode.Text = orgEpc;
                        if (iTagNum == 0 || orgEpc == "")
                            SetStatus("【ERR】No tag found or Unknown tag error.");
                        else if (iTagNum > 1)
                            SetStatus("【ERR】Write tag fail, because more than one tag in the Field.");
                        else if (!orgEpc.Replace(" ", "").Trim().Equals(INIT_CODE))
                            SetStatus("【ERR】The tag not initial.");
                    }
                    else
                    {
                        epcCode = GetTagEPC();
                        if (!epcCode.Equals("") && !epcCode.Replace(" ", "").Trim().Equals(INIT_CODE))
                        {
                            lblOrigCode.Text = epcCode;
                            SetStatus("【ERR】The tag not initial.");
                        }
                        else if (epcCode.Equals(""))
                        {
                            SetStatus("【ERR】No tag found or Unknown tag error.");
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                SetStatus("【ERR】Verify Tag Error." + ex.Message);
                return false;
            }
        }

        /// <summary> 獲取 Portname </summary>
        private void GetPortName()
        {
            if (oReader == null) { oReader = new ReaderAdapter(ReaderType.ALIEN); }
            if (!string.IsNullOrWhiteSpace(cbPortName.Text.Trim()))
                oReader.SetSerialPort(cbPortName.Text.Trim());
            bool bConn = false;
            if (rfidSerialPortNm != "" || !string.IsNullOrWhiteSpace(cbPortName.Text.Trim()))
                bConn = oReader.OpenConnect();
            if (!bConn)
            {
                List<string> arrPortName = oReader.GetSerialPortNm();
                foreach (var item in arrPortName)
                {
                    try
                    {
                        oReader.SetSerialPort(item);
                        SetStatus("【INFO】Try use " + item + " to connect reader...");
                        bConn = oReader.OpenConnect();
                        if (bConn)
                        {
                            rfidSerialPortNm = item;
                            cbPortName.SelectedValue = rfidSerialPortNm;
                            break;
                        }
                    }
                    catch { continue; }
                }
            }
        }

        /// <summary> 斷開 Reader </summary>
        public void DisconnectReader()
        {
            if (oReader != null && oReader.IsConnected())
            {
                oReader.iReader.SetPower();
                oReader.SetReaderBySingle(ParameterNm.RFAttenuation, RFAttenuation);
                oReader.SetReaderBySingle(ParameterNm.RSSIFilter, AppConfig.RssiFilter);
                oReader.SetReaderBySingle(ParameterNm.ProgEPCData, Common.FormatStr(INIT_CODE));
                oReader.SetReaderBySingle(ParameterNm.AcqG2Mask, "0");
                oReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Include");
                oReader.SetReaderBySingle(ParameterNm.ProgDataUnit, "Word");
                oReader.SetReaderBySingle(ParameterNm.ProgEPCDataInc, "OFF");
                oReader.SetReaderBySingle(ParameterNm.NotifyMode, "OFF");
                oReader.SetReaderBySingle(ParameterNm.NotifyTrigger, "OFF");
                oReader.SetReaderBySingle(ParameterNm.AutoAction, "Acquire");
                oReader.SetReaderBySingle(ParameterNm.TagListFormat, "Terse");
                oReader.SetReaderBySingle(ParameterNm.AutoMode, "OFF");
                oReader.Disconnect();
            }
            oReader = null;
            picStatus.Image = null;
            SetStatus("【INFO】Reader has been disconnected.");
            picConn.Image = Properties.Resources.disconnect;
            ResetAll();
        }

        /// <summary> 重設所有 </summary>
        private void ResetAll()
        {
            iOkNum = 0;
            txtBarcode.Text = "";
            lblOkNum.Text = "0";
            picStatus.Image = null;
            dgvHistory.Rows.Clear();
            lblOrigCode.Text = "";
            lblCurrCode.Text = "";
            dgvBarcode.Rows[0].Cells[0].Value = "";
            dgvBarcode.Rows[0].Cells[1].Value = "";
            dgvBarcode.Rows[0].Cells[2].Value = "";
            dgvBarcode.Rows[0].Cells[3].Value = "";
            dgvBarcode.Rows[0].Cells[4].Value = "";
            dgvBarcode.Rows[0].Cells[5].Value = "";
            txtBarcode.Select();
        }

        /// <summary> 狀態信息</summary>
        private void SetStatus(string infoStatus)
        {
            lstStatus.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + infoStatus);
            lstStatus.SelectedIndex = 0;
        }

        /// <summary>清除狀態信息 </summary>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstStatus.Items.Clear();
        }

        /// <summary> 自動提示 </summary>
        private void lstStatus_MouseMove(object sender, MouseEventArgs e)
        {
            int index = lstStatus.IndexFromPoint(e.Location);
            if (index != -1 && index < lstStatus.Items.Count)
            {
                if (toolTip1.GetToolTip(lstStatus) != lstStatus.Items[index].ToString())
                {
                    toolTip1.SetToolTip(lstStatus, lstStatus.Items[index].ToString());
                }
            }
        }

    }
}
