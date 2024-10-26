using System;
using System.Xml;
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
using System.Threading;
using System.Threading.Tasks;
//Ref custom
using Generic;
using RFID.Command;
using RFID.Adapter;
using RFIDModel.Model.POJO;
using RFIDModel.Interface.POJO;
using RFIDModel.Common;
using RFIDModel.Model.DTO;
using Newtonsoft.Json;

namespace RFIDClient
{
    public partial class FrmAccessControl : Form
    {
        const int CODE_LENGTH = 20;
        const string MAGNETIZE = "00 00";
        const string DEMAGNETIZE = "11 11";
        int iOkNum = 0, iTagNum = 0;
        string rfidSerialPortNm = "";
        ReaderAdapter oReader = null;
        ReadWriteJson oReadWriteJson = new ReadWriteJson();
        private string RFAttenuation = AppConfig.RFAttenuation;
        private string ComTimeOutInterval = AppConfig.ComTimeOutInterval;
        private string NetworkTimeout = AppConfig.NetworkTimeout;
        string tagState = "";
        string sqlMapperId = Common.SqlMapperId;
        string dbName = AppConfig.CurrentDBName;
        string dbConn = AppConfig.CurrentDBConn;
        // 用于新设备， 2023-08-22
        string oldCode = "", newCode = "";
        bool isRun = false;
        Thread oThread = null;
        delegate bool ReadCallback(string paraVal);
        ReadCallback readCallback;

        public FrmAccessControl()
        {
            InitializeComponent();
            this.Text = Lang.Dict("C0010", "上消磁門禁管控");
        }

        /// <summary> 窗口加載時 </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            readCallback = new ReadCallback(ExecSaveData);
            BindComPort();
            ResetAll();
            // SetBaseConfig();
            btnMagnetize.Enabled = true;
            btnDemagnetize.Enabled = true;
            if (oReader == null || oReader != null && !oReader.IsConnected())
            {
                btnMagnetize.Enabled = false;
                btnDemagnetize.Enabled = false;
            }
            cbPortName.Enabled = (oReader == null || oReader != null && !oReader.IsConnected()) ? true : false;
        }

        /// <summary>綁定COM</summary>
        private void BindComPort()
        {
            BindingSource oBs = new BindingSource();
            oBs.DataSource = AppConfig.ComPortList;
            cbPortName.DataSource = oBs;
            cbPortName.DisplayMember = "Key";
            cbPortName.ValueMember = "Value";
            List<PropName> paraList = oReadWriteJson.ReadJson();
            PropName oPropName = paraList != null && paraList.Count > 0 ? paraList[0] : new PropName();
            SrvStatic.comPort = oPropName.ComPort == null ? "" : oPropName.ComPort;
            SrvStatic.readerType = oPropName.ReaderType == null ? "" : oPropName.ReaderType;
            SrvStatic.MinPower = oPropName.ReadPower == null ? 0 : Convert.ToInt32(oPropName.ReadPower);
            SrvStatic.MaxPower = oPropName.WritePower == null ? 0 : Convert.ToInt32(oPropName.WritePower);
            SrvStatic.TryTime = oPropName.WriteTime == null ? 0 : Convert.ToInt32(oPropName.WriteTime);
            cbPortName.SelectedValue = SrvStatic.comPort;
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
                        }
                    }
                    catch { continue; }
                }
            }
        }

        /// <summary> 重設所有 </summary>
        private void ResetAll()
        {
            iOkNum = 0;
            lblOkNum.Text = "0";
            picStatus.Image = null;
            dgvHistory.Rows.Clear();
            lblOrigCode.Text = "";
            lblCurrCode.Text = "";
        }

        /// <summary> 連接 / 斷開 圖標 </summary>
        private void picConn_Click(object sender, EventArgs e)
        {
            if (oReader == null || !oReader.IsConnected())
            {
                SetBaseConfig();
                btnMagnetize.Enabled = true;
                btnDemagnetize.Enabled = true;
            }
            else
            {
                DisconnectReader();
                btnMagnetize.Enabled = false;
                btnDemagnetize.Enabled = false;
            }
            cbPortName.Enabled = (oReader == null || oReader != null && !oReader.IsConnected()) ? true : false;
        }

        /// <summary> 初始化基本配置 </summary>
        private void SetBaseConfig()
        {
            this.Cursor = Cursors.WaitCursor;
            bool bConn = ConnectReader(cbPortName.Text.Trim(), "");
            if (!bConn)
            {
                picConn.Image = Properties.Resources.disconnect;
                SetStatus("【INFO】Reader can not connect.");
            }
            else
            {
                SetStatus("【OK】Connect success.");
                picConn.Image = Properties.Resources.connect;

            }
            this.Cursor = Cursors.Default;
        }

        /// <summary> 斷開 Reader </summary>
        public void DisconnectReader()
        {
            if (oReader != null && oReader.IsConnected())
            {
                oReader.iReader.SetPower();
                oReader.SetReaderBySingle(ParameterNm.AutoModeReset);
                oReader.SetReaderBySingle(ParameterNm.PersistTime, "-1");
                oReader.Disconnect();
            }
            oReader = null;
            picStatus.Image = null;
            SetStatus("【INFO】Reader has been disconnected.");
            picConn.Image = Properties.Resources.disconnect;
            ResetAll();
            if (oThread != null)
            {
                oThread.Abort();
                oThread = null;
            }
        }

        /// <summary> 狀態信息</summary>
        private void SetStatus(string infoStatus)
        {
            if (lstStatus.Items.Count > 50) { lstStatus.Items.RemoveAt(lstStatus.Items.Count - 1); }
            lstStatus.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + infoStatus);
            lstStatus.SelectedIndex = 0;
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
                if (string.IsNullOrWhiteSpace(portNm) || rfidSerialPortNm == "") { GetPortName(); }
                if (oReader != null && !oReader.IsConnected()) { oReader.OpenConnect(); }
                SetReaderRequire();
                if (oReader == null || !oReader.IsConnected()) { return false; }
            }
            catch
            {
                Common.InvokeBeep(50000, 500, 2);
                return false;
            }
            return true;
        }

        /// <summary>設置Reader必需參數 </summary>
        private void SetReaderRequire()
        {
            if (oReader != null && oReader.IsConnected())
            {
                oReader.SetReaderBySingle(ParameterNm.AutoModeReset);
                oReader.SetReaderBySingle(ParameterNm.RFAttenuation, RFAttenuation);
                oReader.SetReaderBySingle(ParameterNm.RSSIFilter, AppConfig.RssiFilter);
                oReader.SetReaderBySingle(ParameterNm.AcqG2Mask, "0");
                oReader.SetReaderBySingle(ParameterNm.TagListFormat, "Terse");
                oReader.SetReaderBySingle(ParameterNm.NotifyFormat, "Terse");
                oReader.SetReaderBySingle(ParameterNm.ProgEPCDataInc, "OFF");
                oReader.SetReaderBySingle(ParameterNm.ProgDataUnit, "WORD");
                oReader.SetReaderBySingle(ParameterNm.NotifyTrigger, "True");   //Change, AddRemove, TrueFalse
                oReader.SetReaderBySingle(ParameterNm.PersistTime, "-1");
                oReader.SetReaderBySingle(ParameterNm.NotifyMode, "OFF");
                oReader.SetReaderBySingle(ParameterNm.AutoMode, "OFF");
                oReader.SetReaderBySingle(ParameterNm.AutoAction, "Acquire");
            }
        }

        /// <summary>關閉窗口時</summary>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisconnectReader();
            oReadWriteJson = null;
            this.Close();
            this.Dispose();
        }

        /// <summary> 選擇所有 </summary>
        private void txtBarcode_Click(object sender, EventArgs e)
        {

            picStatus.Image = null;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //偵測有無斷線過
                if (oReader != null && !oReader.IsConnected())
                {
                    GetPortName();
                    picConn.Image = Properties.Resources.disconnect;
                    oReader.Disconnect();
                    oReader = null;
                    SetBaseConfig();
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

        /// <summary> 上磁 </summary>
        private void btnMagnetize_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            iOkNum = tagState != MAGNETIZE ? 0 : iOkNum;
            lblOkNum.Text = iOkNum.ToString();
            tagState = MAGNETIZE;
            if (oReader != null && oReader.IsConnected())
            {
                SetReaderRequire();
                oReader.SetReaderBySingle(ParameterNm.AcqG2Mask, "0");
                oReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Exclude");
                oReader.SetReaderBySingle(ParameterNm.SetAcqG2Mask, new Parameters()
                {
                    MEMORY_BANK = "1",
                    BITPTR = "12",
                    BITLEN = "16",
                    HEX_BYTES = tagState //遮罩有處理過的
                });
                // 如是新设备 ReaderType.ALIEN_UHF
                if ("2".Equals(SrvStatic.readerType))
                    StartExecute();
                else
                    ExecSaveData(tagState);
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>启用线程执行</summary>
        private void StartExecute()
        {
            if (oThread != null)
            {
                bool bStop = oReader.iReader.StopGet();
                isRun = false;
                DisconnectReader();
                SetBaseConfig();
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
                newCode = GetTagEPC();
                if (!oldCode.Replace(" ", "").Equals(newCode.Replace(" ", "")) && !string.IsNullOrEmpty(newCode))
                    this.Invoke(readCallback, new object[] { tagState });
            };
        }

        /// <summary> 消磁 </summary>
        private void btnDemagnetize_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            iOkNum = tagState != DEMAGNETIZE ? 0 : iOkNum;
            lblOkNum.Text = iOkNum.ToString();
            tagState = DEMAGNETIZE;
            if (oReader != null && oReader.IsConnected())
            {
                SetReaderRequire();
                oReader.SetReaderBySingle(ParameterNm.AcqG2Mask, "0");
                oReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Exclude");
                oReader.SetReaderBySingle(ParameterNm.SetAcqG2Mask, new Parameters()
                {
                    MEMORY_BANK = "1",
                    BITPTR = "12",
                    BITLEN = "16",
                    HEX_BYTES = tagState
                });   //遮罩有處理過的
                // 如是新设备 ReaderType.ALIEN_UHF
                if ("2".Equals(SrvStatic.readerType))
                    StartExecute();
                else
                    ExecSaveData(tagState);
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary> 寫入資料庫 (Para1-上磁或消磁) </summary>
        private bool ExecSaveData(string processCode)
        {
            string epcCode = "", progCode = "";
            picStatus.Image = null;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oCallbackProxy = ServiceFactory<IClientSendMsg>.GetService();
            var oService = ServiceFactory<IDevBarnoDAO>.GetService();
            try
            {
                if (!isRun)
                    Application.DoEvents();
                if (oReader == null || !oReader.IsConnected())
                {
                    SetStatus("【INFO】Reader has been disconnected, please try connect again.");
                    picConn.Image = Properties.Resources.disconnect;
                    Common.InvokeBeep(50000, 500, 2);
                    return false;
                }
                epcCode = GetTagEPC();
                // 消磁時，檢查是否出庫Tag, A: 入庫，B:出庫
                if (epcCode != "" && tagState == DEMAGNETIZE)
                {
                    string tmpEpc = epcCode.Replace(" ", "");
                    tmpEpc = tmpEpc.Substring(0, tmpEpc.Length - 4);
                    oService.SetPara(SqlConst.SelDevBarnoAll, dbName, new DevBarno()
                    {
                        RfidCode = tmpEpc,
                        InoutNo = "A"
                    }, sqlMapperId);
                    IList<DevBarno> outList = oService.GetData();
                    if (outList != null && outList.Count > 0)
                    {
                        Common.CloseService(oService);
                        Common.CloseService(oCallbackProxy);
                        MessageBox.Show("未掃瞄出庫，不允許消磁", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                progCode = SetTagEPC(iTagNum, epcCode, processCode);
                newCode = progCode;
                lblOrigCode.Text = epcCode.Replace(" ", "").Trim();
                lblCurrCode.Text = progCode.Replace(" ", "").Trim();
                if (String.IsNullOrEmpty(progCode.Replace(" ", "").Trim()))
                {
                    picStatus.Image = Properties.Resources.Error;
                    lblOrigCode.ForeColor = Color.Red;
                    oCallbackProxy.ClientSendMsg(System.Net.Dns.GetHostName() + "【" + epcCode + "】" + (tagState == MAGNETIZE ? "上磁失敗" : "消磁失敗"));
                    return false;
                }
                bool bSave = SaveData(epcCode, processCode);
                if (!bSave)
                {
                    picStatus.Image = Properties.Resources.Error;
                    lblOrigCode.ForeColor = Color.Red;
                    if (progCode == epcCode)
                        SetTagEPC(iTagNum, epcCode, tagState == MAGNETIZE ? DEMAGNETIZE : MAGNETIZE);
                    oCallbackProxy.ClientSendMsg(System.Net.Dns.GetHostName() + "【" + epcCode + "】" + (tagState == MAGNETIZE ? "上磁失敗" : "消磁失敗"));
                    return false;
                }
                else
                {
                    oldCode = progCode;
                }
                oCallbackProxy.ClientSendMsg(System.Net.Dns.GetHostName() + "【" + progCode + "】" + (tagState == MAGNETIZE ? "上磁成功" : "消磁成功"));
                SetStatus("【OK】Successful");
                lblOrigCode.ForeColor = Color.FromArgb(51, 153, 255);
                iOkNum++;
                lblOkNum.Text = iOkNum.ToString();
                picStatus.Image = Properties.Resources.Success;

                epcCode = epcCode.Replace(" ", "").Trim().Substring(0, CODE_LENGTH);
                //oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.SelDevBarnoAll, dbName, new DevBarno()
                {
                    RfidCode = epcCode
                }, sqlMapperId);
                IList<DevBarno> dataList = oService.GetData();
                string[] arrCell = { 
                                           dataList[0].BarNo, dataList[0].ArticNo, dataList[0].SpecNo + "-" + dataList[0].SpecVersion, 
                                           dataList[0].SizeNo, dataList[0].LrMk, dataList[0].RfidCode.Substring(0, CODE_LENGTH), dataList[0].DemMk
                                       };
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
                Common.InvokeBeep(50000, 500);
                return true;
            }
            catch (Exception ex)
            {
                picStatus.Image = Properties.Resources.Error;
                lblOrigCode.ForeColor = Color.Red;
                return false;
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                Common.CloseService(oService);
                Common.CloseService(oCallbackProxy);
            }
        }

        /// <summary> 設定 EPC </summary>
        private string SetTagEPC(int tagNum, string epcCode, string demMk)
        {
            string progCode = "";
            string setCode = "";
            if (tagNum != 1)
            {
                switch (tagNum)
                {
                    case 0:
                        SetStatus("【ERR】No tag found or Unknown tag error.");
                        break;
                    default:
                        SetStatus("【ERR】Write tag fail, because more than one tag in the Field.");
                        break;
                }
                Common.InvokeBeep(50000, 500, 2);
                return "";
            }

            if (epcCode.Replace(" ", "").Trim().Length < 24)
            {
                SetStatus("【ERR】Tag EPC less than 24 digits,EPC:" + epcCode);
                return "";
            }

            try
            {
                setCode = epcCode.Substring(0, (CODE_LENGTH + CODE_LENGTH) / 2) + demMk;
                progCode = oReader.ProgTagBySingle(1, setCode);
            }
            catch (Exception ex)
            {
                progCode = "";
            }

            if (progCode.Replace(" ", "").Trim() != setCode.Replace(" ", "").Trim())
            {
                SetStatus("【ERR】Program RFID EPC CODE fail,because more than one tag in the field or no tag found");
                return "";
            }

            return progCode;
        }

        /// <summary>保存資料</summary>
        private bool SaveData(string rfidCode, string demMk)
        {
            bool bolOk = false;
            string dbErr = "";
            IList<DevBarno> lstDevBarno;
            rfidCode = rfidCode.Replace(" ", "").Trim();
            demMk = demMk.Replace(" ", "").Trim();
            if (String.IsNullOrEmpty(demMk))
                return false;
            if (rfidCode.Length < CODE_LENGTH)
                return false;
            rfidCode = rfidCode.Substring(0, CODE_LENGTH);
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oService = ServiceFactory<IDevBarnoDAO>.GetService();
            try
            {
                oService.SetConnection(dbName, dbConn);
                oService.SetPara(SqlConst.UpdDevMkByRfidCode, dbName, new DevBarno()
                {
                    RfidCode = rfidCode,
                    DemMk = demMk,
                    DemUser = SrvStatic.usrId,
                    DemIp = SrvStatic.localIP
                }, sqlMapperId);

                if (oService.GetChange(2) <= 0)
                {
                    dbErr = "【ERR】Update database fail.EPC:" + rfidCode;
                    oService.SetPara(SqlConst.SelDevBarnoAll, dbName, new DevBarno()
                    {
                        RfidCode = rfidCode
                    }, sqlMapperId);
                    lstDevBarno = oService.GetData();
                    if (lstDevBarno == null || lstDevBarno.Count == 0)
                        dbErr = "【INFO】This tag not in database.EPC:" + rfidCode;
                    SetStatus(dbErr);
                }
                else
                {
                    bolOk = true;
                }
            }
            catch (Exception ex)
            {
                SetStatus("【ERR】Update TABLE [DEV_BARNO] fail,please contact administrators,check column length." + dbErr);
                if (oService != null)
                {
                    Common.CloseService(oService);
                }
                MessageBox.Show(ex.ToString());
            }
            return bolOk;
        }

        /// <summary>獲取 EPC</summary>
        private string GetTagEPC()
        {
            string tagData, retTag;
            retTag = "";
            iTagNum = 0;
            if (oReader != null && oReader.IsConnected())
            {
                tagData = "";
                List<TagStruct> tagList = oReader.GetTagList(); //oReader.TagList;
                iTagNum = tagList.Count;
                try
                {
                    tagData = oReader.ReadTagByBank(1, "2", "6"); //oReader.G2Read((eG2Bank)(1), "2", "6");
                    int len = tagData.Length;
                    if (len >= 35)
                        retTag = tagData.Substring(0, 35);//2*12 (字) + 11 (空白)
                    else
                        retTag = tagData.Substring(0, 24);//2*12 (字)
                }
                catch
                {
                    iTagNum = 0;
                }
            }
            return retTag;
        }

        /// <summary>频率高低，响时長短  </summary>
        [DllImport("kernel32.dll", EntryPoint = "Beep")]
        public static extern int Beep(int dwFreq, int dwDuration);
        [DllImport("winmm.dll")]
        public static extern bool PlaySound(String Filename, int Mod, int Flags);

        //清除狀態信息
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstStatus.Items.Clear();
        }

        //自動提示
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
