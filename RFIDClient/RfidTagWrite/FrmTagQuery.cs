using System;
using System.Text;
using Microsoft.Win32;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.ServiceModel;
using System.Threading;
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

namespace RFIDClient
{
    public partial class FrmTagQuery : Form
    {
        int iOkNum = 0, iTagNum = 0;
        string rfidSerialPortNm = "";
        const string INIT_CODE = "000000000000000000000000";
        string sqlMapperId = Common.SqlMapperId;
        ReaderAdapter oReader = null;
        ReadWriteJson oReadWriteJson = new ReadWriteJson();
        private string RFAttenuation = AppConfig.RFAttenuation;
        private string ComTimeOutInterval = AppConfig.ComTimeOutInterval;
        private string NetworkTimeout = AppConfig.NetworkTimeout;
        string dbName = AppConfig.CurrentDBName;

        public FrmTagQuery()
        {
            InitializeComponent();
            this.Text = Lang.Dict("C0011", "Barcode 與 RFID Code 驗証查詢");
        }

        /// <summary> 窗口加載時 </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            BindComPort();
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

        /// <summary> 連接 / 斷開  </summary>
        private void picConn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (oReader == null || !oReader.IsConnected())
                SetBaseConfig();
            else
                DisconnectReader();
            cbPortName.Enabled = (oReader == null || !oReader.IsConnected()) ? true : false;
            this.Cursor = Cursors.Default;
            if (oReader != null && oReader.IsConnected())
            {
                oReadWriteJson.WriteJson(new PropName() { ComPort = cbPortName.Text.Trim() });
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
            bool bVerify = true;
            try
            {
                if (e.KeyCode == Keys.Enter && barcode != "")
                {
                    txtBarcode.Enabled = false;
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
                    for (var r = dgvHistory.Rows.Count - 1; r >= 0; r--)
                    {
                        dgvHistory.Rows.RemoveAt(r);
                    }
                    groupBox3.Text = "History";
                    // 讀取器是否連線
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
                    lblShoeCode.ForeColor = Color.FromArgb(51, 153, 255);
                    lblSystemCode.ForeColor = Color.FromArgb(51, 153, 255);
                    //驗證資料
                    bVerify = VerifyQuery();
                    if (bVerify)
                    {
                        lblShoeCode.ForeColor = Color.Green;
                        lblSystemCode.ForeColor = Color.Green;
                        SetStatus("【OK】The RFID code of shoe is equal to system, the data is correct.");
                        iOkNum++;
                        picStatus.Image = Properties.Resources.Success;
                        Common.InvokeBeep(50000, 500);
                    }
                    else
                    {
                        if (lblShoeCode.Text.Trim() != "")
                            SetStatus("【ERR】The RFID code of shoe is not equal to system, the data is wrong.");
                        picStatus.Image = Properties.Resources.Error;
                        lblSystemCode.ForeColor = Color.Red;
                        Common.InvokeBeep(50000, 500, 2);
                    }
                    txtBarcode.Text = "";
                    this.Cursor = Cursors.Default;
                }
            }
            catch { }
            finally { txtBarcode.Enabled = true; txtBarcode.Focus(); }
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
        }

        /// <summary>驗證資料</summary>
        private bool VerifyQuery()
        {
            string epcCode = "", rfidCode = "", tagEpc = "";
            DevBarno oDevBarno;
            DevHisBarnoInOut oDevHisBarnoInOut;
            IList<DevBarno> dataList;
            IList<DevHisBarnoInOut> hisList;
            DateTime st, et;
            st = DateTime.Now;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oCallbackProxy = ServiceFactory<IClientSendMsg>.GetService();
            var oServiceDevBarno = ServiceFactory<IDevBarnoDAO>.GetService();
            var oServiceDevHisBarno = ServiceFactory<IDevHisBarnoInOutDAO>.GetService();
            try
            {
                picStatus.Image = null;
                lblShoeCode.Text = "";
                lblSystemCode.Text = "";
                if (dbName == null) { SetStatus("【ERR】CurrentDBName is NULL."); return false; }
                oServiceDevBarno.SetConnection(dbName, AppConfig.CurrentDBConn);
                //查詢 dev_barno_his
                oDevHisBarnoInOut = new DevHisBarnoInOut();
                oDevHisBarnoInOut.BarNo = txtBarcode.Text.Trim().ToUpper();
                oServiceDevHisBarno.SetPara(SqlConst.SelDevHisBarnoInOut, dbName, oDevHisBarnoInOut, sqlMapperId);
                hisList = oServiceDevHisBarno.GetData();
                if (hisList == null || hisList.Count <= 0)
                {
                    //查詢 DEV_BARNO
                    oDevBarno = new DevBarno();
                    oDevBarno.BarNo = txtBarcode.Text.Trim().ToUpper();
                    oServiceDevBarno.SetPara(SqlConst.SelDevBarnoAll, dbName, oDevBarno, sqlMapperId);
                    dataList = oServiceDevBarno.GetData();
                    oReader.ClearTagList();
                    if (dataList == null || dataList.Count <= 0)
                    {
                        picStatus.Image = Properties.Resources.Error;
                        SetStatus("【ERR】The barcode [" + oDevBarno.BarNo + "] not exist in DEV_BARNO OR DEV_BARNO.RFID_CODE is null.");
                        IList<TagStruct> tagsList = oReader.GetTagList();
                        int iTagCnt = tagsList.Count;
                        if (iTagCnt != 1)
                        {
                            lblShoeCode.Text = "";
                            if (iTagCnt == 0)
                                SetStatus("【ERR】No tag found or Unknown tag error.");
                            else
                                SetStatus("【ERR】More than one tag in the Field.");
                        }
                        else
                        {
                            tagEpc = GetTagEPC();
                            lblShoeCode.Text = tagEpc;
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
                }
                else
                {
                    dgvBarcode.Rows[0].Cells[0].Value = hisList[0].BarNo;
                    dgvBarcode.Rows[0].Cells[1].Value = hisList[0].ArticNo;
                    dgvBarcode.Rows[0].Cells[2].Value = hisList[0].SpecNo + "-" + hisList[0].SpecVersion;
                    dgvBarcode.Rows[0].Cells[3].Value = hisList[0].SizeNo;
                    dgvBarcode.Rows[0].Cells[4].Value = hisList[0].LrMk;
                    dgvBarcode.Rows[0].Cells[5].Value = hisList[0].RfidCode;
                    rfidCode = hisList[0].RfidCode.Trim();
                    //綁定History
                    dgvHistory.DataSource = Common.ToDataTable(hisList);
                    foreach (DataGridViewColumn col in dgvHistory.Columns)
                    {
                        if (col.Index >= 6) { col.Visible = false; }
                    }
                    dgvHistory.Sort(dgvHistory.Columns[5], ListSortDirection.Descending);
                    groupBox3.Text = "History【" + dgvHistory.Rows.Count.ToString() + "】";
                }
                rfidCode = rfidCode.Length == 20 ? rfidCode.PadRight(24, '0') : rfidCode;
                lblSystemCode.Text = rfidCode;
                //取得Tag數量
                IList<TagStruct> tagList = oReader.GetTagList();
                iTagNum = tagList.Count;
                if (iTagNum != 1)
                {
                    lblShoeCode.Text = "";
                    epcCode = GetTagEPC();
                    lblShoeCode.Text = epcCode;
                    if (iTagNum == 0 && epcCode == "")
                        SetStatus("【ERR】No tag found or Unknown tag error.");
                    else if (iTagNum > 1)
                        SetStatus("【ERR】Write tag fail, because more than one tag in the Field.");
                }
                else
                {
                    epcCode = GetTagEPC();
                    epcCode = epcCode.Length == 20 ? epcCode.PadRight(24, '0') : epcCode;
                    lblShoeCode.Text = epcCode;
                }
                //關閉通道
                Common.CloseService(oCallbackProxy);
                Common.CloseService(oServiceDevBarno);
                return rfidCode == epcCode ? true : false;
            }
            catch (Exception ex)
            {
                Common.CurStatusMsg = ex.Message;
                Common.InvokeBeep(50000, 500, 2);
                SetStatus("【ERR】Exception Error " + ex.Message);
                lblSystemCode.Text = INIT_CODE;
                lblSystemCode.ForeColor = Color.Red;
                picStatus.Image = Properties.Resources.Error;
                return false;
            }
            finally
            {
                et = DateTime.Now;
                TimeSpan ts = et.Subtract(st);
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
            picStatus.Image = null;
            lblShoeCode.Text = "";
            lblSystemCode.Text = "";
            dgvBarcode.Rows[0].Cells[0].Value = "";
            dgvBarcode.Rows[0].Cells[1].Value = "";
            dgvBarcode.Rows[0].Cells[2].Value = "";
            dgvBarcode.Rows[0].Cells[3].Value = "";
            dgvBarcode.Rows[0].Cells[4].Value = "";
            dgvBarcode.Rows[0].Cells[5].Value = "";
            txtBarcode.Select();
            for (var r = dgvHistory.Rows.Count - 1; r >= 0; r--)
            {
                dgvHistory.Rows.RemoveAt(r);
            }
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

        private void clearStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstStatus.Items.Clear();
        }

    }
}
