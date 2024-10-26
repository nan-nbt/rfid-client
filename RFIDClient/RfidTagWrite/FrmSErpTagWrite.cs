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
using System.ServiceModel;
using System.Threading;
using System.Transactions;
//Ref Others
using Newtonsoft.Json;
using Generic;
using RFID.Command;
using RFID.Adapter;
using RFIDClient.Properties;
using RFIDModel.Model.POJO;
using RFIDModel.Interface.POJO;
using RFIDModel.Common;
using RFIDModel.Interface.DTO;
using RFIDModel.Model.DTO;
using System.Net;
using System.IO;

namespace RFIDClient
{
    public partial class FrmSErpTagWrite : Form
    {
        /// <summary>是否忽略讀寫器</summary>
        bool isIngoreAR = false;
        int iOkNum = 0, iTagNum = 0;
        string rfidSerialPortNm = "";
        string make_no = "";
        string msg = "";
        string sqlMapperId = Common.SqlMapperId;
        const string INIT_CODE = "000000000000000000000000";
        ReaderAdapter oReader = null;
        ReadWriteJson oReadWriteJson = new ReadWriteJson();
        private string RFAttenuation = AppConfig.RFAttenuation;
        private string ComTimeOutInterval = AppConfig.ComTimeOutInterval;
        private string NetworkTimeout = AppConfig.NetworkTimeout;
        string dbName = AppConfig.CurrentDBName;
        string dbConn = AppConfig.CurrentDBConn;

        public FrmSErpTagWrite()
        {
            InitializeComponent();
            this.Text = Lang.Dict("C0013", "制程掃描 Sample ERP");
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
            BindingSource oBs = new BindingSource();
            oBs.DataSource = AppConfig.ComPortList;
            cbPortName.DataSource = oBs;
            cbPortName.DisplayMember = "Value";
            cbPortName.ValueMember = "Key";
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
            dynamic oJson = null;
            try
            {
                if (e.KeyCode == Keys.Enter && barcode != "")
                {
                    dgvBarcode.Rows[0].Cells[0].Value = barcode;
                    txtJson.Text = GetJsonData(barcode);
                    string barnoJson = txtJson.Text.Trim();
                    try
                    {
                        oJson = JsonConvert.DeserializeObject<dynamic>(barnoJson);
                        txtJson.Text = Convert.ToString(oJson);
                    }
                    catch (JsonException je)
                    {
                        ResetPart();
                        msg = "【ERR】Invalid JSON Format！ => " + je.Message;
                        Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                        return;
                    }
                    if (string.IsNullOrEmpty(barnoJson))
                    {
                        ResetPart();
                        msg = "【INFO】The JSON data is empty！";
                        Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                        return;
                    }
                    string isSucc = Convert.ToString(oJson["IsSucc"]);
                    if (!string.IsNullOrEmpty(isSucc) && !isSucc.ToUpper().Equals("Y") || string.IsNullOrEmpty(isSucc))
                    {
                        ResetPart();
                        if (oJson["ErrorMessage"] == null || !isSucc.ToUpper().Equals("N"))
                            msg = "【INFO】Unknown JSON format！";
                        else
                            msg = "【INFO】" + oJson["ErrorMessage"] + " (" + oJson["ErrorMessageZhTW"] + ")";
                        Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                        return;
                    }
                    txtBarcode.Enabled = false;
                    if (cbxStep.SelectedValue == null) { SetStatus("【INFO】Please select a step."); return; }
                    make_no = cbxStep.SelectedValue.ToString().Trim();
                    txtBarcode.SelectAll();
                    picStatus.Image = null;
                    // dgvBarcode.Rows[0].Cells[0].Value = "";
                    dgvBarcode.Rows[0].Cells[1].Value = "";
                    dgvBarcode.Rows[0].Cells[2].Value = "";
                    dgvBarcode.Rows[0].Cells[3].Value = "";
                    dgvBarcode.Rows[0].Cells[4].Value = "";
                    dgvBarcode.Rows[0].Cells[5].Value = "";
                    Application.DoEvents();
                    this.Cursor = Cursors.WaitCursor;
                    if ((oReader == null || !oReader.IsConnected()) && !isIngoreAR)
                    {
                        ResetPart();
                        this.Cursor = Cursors.Default;
                        picConn.Image = Properties.Resources.disconnect;
                        msg = "【INFO】Reader has been disconnected, please try connect again.";
                        Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                        return;
                    }
                    // 數據處理過程
                    bolSave = SaveData(oJson);
                    if (bolSave)
                    {
                        msg = "【OK】Successful";
                        Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Success);
                        iOkNum++;
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

        /// <summary>請求SERP提供的ESB服務</summary>
        private string GetJsonData(string barcode)
        {
            string retJson = "", factNo = "", esbHost = "", esbAcct = "", esbPwd = "";
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Cann't connect to RFIDServer";
            }
            var oSvrPctUser = ServiceFactory<IPctUserDAO>.GetService();
            oSvrPctUser.SetConnection(dbName, dbConn);
            oSvrPctUser.SetPara(SqlConst.SelEsbAcct, dbName, new PctUser() { FactNo = Common.FactNo }, sqlMapperId);
            IList<PctUser> listData = oSvrPctUser.GetData() as IList<PctUser>;
            Common.CloseService(oSvrPctUser);
            if (listData != null && listData.Count > 0)
            {
                factNo = Common.Trim(listData[0].FactNo) == "" ? Common.FactNo : listData[0].FactNo;
                esbAcct = listData[0].UsrId;
                esbPwd = listData[0].UsrPwd;
                esbHost = listData[0].LoginIp;
            }
            string url = esbHost + "?FactNo=" + factNo + "&Barcode=" + barcode;

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(esbAcct + ":" + esbPwd));
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Accept = "*/*";
            request.Method = "GET";
            request.Headers.Set("Pragma", "no-cache");
            request.ContentType = "application/json; charset=utf-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream oSteam = response.GetResponseStream();
            StreamReader oSReader = new StreamReader(oSteam, Encoding.UTF8);
            retJson = oSReader.ReadToEnd();
            oSteam.Close();
            oSteam.Dispose();
            oSReader.Close();
            oSReader.Dispose();
            request.Abort();
            return retJson;
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
        private bool SaveData(dynamic oJson)
        {
            int colMax = 0;
            string progCode = "", rfidCode = "", tagEpc = "", fieldNm = "";
            DevBarno oDevBarno;
            DevMakePro oDevMakePro;
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
            var oServiceDevMakePro = ServiceFactory<IDevMakeProDAO>.GetService();
            var oServicePctUser = ServiceFactory<IPctUserDAO>.GetService();
            var oServiceBrand = ServiceFactory<IDevBrandDAO>.GetService();
            try
            {
                picStatus.Image = null;
                lblOrigCode.Text = "";
                lblCurrCode.Text = "";
                lblCurrCode.ForeColor = Color.DarkBlue;
                if (dbName == null) { SetStatus("【ERR】CurrentDBName is NULL."); return false; }
                if (oJson["FactNo"] != null && Convert.ToString(oJson["FactNo"]).Length > 4) { colMax = 4; fieldNm = "FACT_NO"; }
                if (oJson["Barcode"] != null && Convert.ToString(oJson["Barcode"]).Length > 15) { colMax = 15; fieldNm = "BAR_NO"; }
                if (oJson["OdrNo"] != null && Convert.ToString(oJson["OdrNo"]).Length > 15) { colMax = 15; fieldNm = "SPEC_NO"; }
                if (oJson["IsFromOdr"] != null && Convert.ToString(oJson["IsFromOdr"]).Length > 1) { colMax = 1; fieldNm = "BAR_KIND"; }
                if (oJson["AtcNo"] != null && Convert.ToString(oJson["AtcNo"]).Length > 30) { colMax = 30; fieldNm = "ARTIC_NO"; }
                if (oJson["Size"] != null && Convert.ToString(oJson["Size"]).Length > 10) { colMax = 10; fieldNm = "SIZE_NO"; }
                if (!string.IsNullOrEmpty(fieldNm))
                {
                    msg = "【INFO】The field [" + fieldNm + "] is more than " + colMax + " bytes.";
                    Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                    return false;
                }
                // 取得品牌代号
                oServiceBrand.SetConnection(dbName, dbConn);
                oServiceBrand.SetPara(SqlConst.SelDevBrandAll, dbName, new DevBrand() { FactNo = Common.FactNo }, sqlMapperId);
                IList<DevBrand> listBrand = oServiceBrand.GetData() as IList<DevBrand>;
                Common.CloseService(oServiceBrand);
                // 判斷是否爲空
                if (oJson["FactNo"] == null || oJson["FactNo"] != null && Convert.ToString(oJson["FactNo"]).Trim().Length == 0) { fieldNm = "FACT_NO"; }
                if (oJson["Barcode"] == null || oJson["Barcode"] != null && Convert.ToString(oJson["Barcode"]).Trim().Length == 0) { fieldNm = "BAR_NO"; }
                if (oJson["OdrNo"] == null || oJson["OdrNo"] != null && Convert.ToString(oJson["OdrNo"]).Trim().Length == 0) { fieldNm = "SPEC_NO"; }
                if (oJson["IsFromOdr"] == null || oJson["IsFromOdr"] != null && Convert.ToString(oJson["IsFromOdr"]).Trim().Length == 0) { fieldNm = "BAR_KIND"; }
                if (oJson["AtcNo"] == null || oJson["AtcNo"] != null && Convert.ToString(oJson["AtcNo"]).Trim().Length == 0) { fieldNm = "ARTIC_NO"; }
                if (oJson["Size"] == null || oJson["Size"] != null && Convert.ToString(oJson["Size"]).Trim().Length == 0) { fieldNm = "SIZE_NO"; }
                if (oJson["CreateDate"] == null || oJson["CreateDate"] != null && Convert.ToString(oJson["CreateDate"]).Trim().Length == 0) { fieldNm = "PRO_DATE"; }
                if (!string.IsNullOrEmpty(fieldNm))
                {
                    msg = "【INFO】The field [" + fieldNm + "] is required, not allow empty.";
                    Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                    return false;
                }
                // 抓取 SeasonId, TeamId, StageId, ArticId欄位
                DevBarno devBarno = new DevBarno() { SpecNo = oJson["OdrNo"], SpecVersion = oJson["OdrVer"] };
                oServiceDevBarno.SetPara(SqlConst.SelSeasonId, dbName, devBarno, sqlMapperId);
                IList<DevBarno> listTmp = oServiceDevBarno.GetData() as IList<DevBarno>;
                Common.CloseService(oServiceDevBarno);
                // 條碼檔 DEV_BARNO
                string barcode = Convert.ToString(oJson["Barcode"]);
                string shoeKind = Convert.ToString(oJson["ShoeKind"]);
                string lrMk = !string.IsNullOrEmpty(barcode) ? barcode.Substring(barcode.Length - 1, 1) : "";
                oDevBarno = new DevBarno();
                oDevBarno.FactNo = oJson["FactNo"];
                oDevBarno.BrandNo = listBrand != null && listBrand.Count > 0 ? listBrand[0].BrandNo : "";
                oDevBarno.BarNo = oJson["Barcode"];
                oDevBarno.BarSeq = !string.IsNullOrWhiteSpace(barcode) ? barcode.Substring(barcode.Length - 4, 3) : "";
                oDevBarno.ProDate = oJson["CreateDate"];
                oDevBarno.SpecNo = oJson["OdrNo"];
                oDevBarno.SpecVersion = oJson["OdrVer"];
                oDevBarno.BarKind = oJson["IsFromOdr"];
                oDevBarno.SeasonId = listTmp != null && listTmp.Count > 0 ? listTmp[0].SeasonId : null;
                oDevBarno.TeamId = listTmp != null && listTmp.Count > 0 ? listTmp[0].TeamId : null;
                oDevBarno.StageId = listTmp != null && listTmp.Count > 0 ? listTmp[0].StageId : null;
                oDevBarno.ArticId = listTmp != null && listTmp.Count > 0 ? listTmp[0].ArticId : null;
                oDevBarno.ArticNo = oJson["AtcNo"];
                oDevBarno.SizeNo = oJson["Size"];
                oDevBarno.LrMk = lrMk;
                oDevBarno.ModifyUser = oJson["ModifyUser"];
                oDevBarno.ModifyDate = oJson["ModifyTime"];
                oDevBarno.ShoeKind = !string.IsNullOrWhiteSpace(shoeKind) ? shoeKind.Substring(0, 1) : "";
                oDevBarno.RfidCode = oJson["RFID"];
                oDevBarno.RfidUser = SrvStatic.usrId;
                oDevBarno.ScanTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                int iDec = Convert.ToInt32(oDevBarno.SeasonId);
                if (Convert.ToInt32(oDevBarno.SeasonId) <= 0 || oDevBarno.StageId == null || oDevBarno.StageId == null || oDevBarno.ArticId == null)
                {
                    msg = "【INFO】Cann't get these require fields（season_id,team_id, stage_id, artic_id）by SPEC NO. and SPEC VER.";
                    Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                    return false;
                }
                // 忽略連接讀取器
                if (!isIngoreAR)
                {
                    IList<TagStruct> tagList = oReader.GetTagList();
                    int iTagCnt = tagList.Count;
                    if (iTagCnt != 1)
                    {
                        lblOrigCode.Text = "";
                        msg = iTagCnt == 0 ? "【ERR】No tag found or Unknown tag error." : "【ERR】Write tag fail, because more than one tag (" + iTagCnt + ") in the Field.";
                        Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                        return false;
                    }
                    else
                    {
                        tagEpc = GetTagEPC();
                        lblOrigCode.Text = tagEpc;
                    }
                }
                dgvBarcode.Rows[0].Cells[0].Value = oJson["Barcode"];
                dgvBarcode.Rows[0].Cells[1].Value = oJson["AtcNo"];
                dgvBarcode.Rows[0].Cells[2].Value = oJson["OdrNo"] + "-" + oJson["OdrVer"];
                dgvBarcode.Rows[0].Cells[3].Value = oJson["Size"];
                dgvBarcode.Rows[0].Cells[4].Value = lrMk;
                dgvBarcode.Rows[0].Cells[5].Value = oJson["RFID"];
                rfidCode = oJson["RFID"];
                rfidCode = rfidCode != null && rfidCode.Length == 20 ? rfidCode.PadRight(24, '0') : rfidCode;
                //新增 DEV_MAKE_PRO
                oDevMakePro = new DevMakePro();
                oDevMakePro.FactNo = oJson["FactNo"];
                oDevMakePro.BrandNo = oDevBarno.BrandNo;
                oDevMakePro.BarNo = oJson["Barcode"];
                oDevMakePro.MakeNo = make_no;
                oDevMakePro.MakeDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                oDevMakePro.ModifyUser = SrvStatic.usrId;
                oDevMakePro.ModifyDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                // 檢查有無寫入產量檔                 
                oServiceDevMakePro.SetPara(SqlConst.SelDevMakeProAll, dbName, new DevMakePro()
                {
                    FactNo = oJson["FactNo"],
                    BrandNo = oDevBarno.BrandNo,
                    BarNo = oJson["Barcode"],
                    MakeNo = make_no
                }, sqlMapperId);
                IList<DevMakePro> listDmp = oServiceDevMakePro.GetData() as IList<DevMakePro>;
                Common.CloseService(oServiceDevMakePro);
                if (listDmp != null && listDmp.Count > 0)
                {
                    msg = "【ERR】This shoe has the same process produce recode, Please check！";
                    Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
                    return false;
                }
                //編寫標簽
                if (!isIngoreAR)
                    progCode = SetTagEpc(rfidCode, INIT_CODE);
                progCode = progCode.Replace(" ", "");
                rfidCode = rfidCode != null ? rfidCode.Replace(" ", "") : rfidCode;
                //處理資料 dataList[0].BarNo
                Dictionary<string, string> arrPara = new Dictionary<string, string>();
                arrPara.Add(SqlConst.InsDevBarno + "#" + oDevBarno.GetType().FullName + "#1", JsonConvert.SerializeObject(oDevBarno));
                arrPara.Add(SqlConst.InsDevMakePro + "#" + oDevMakePro.GetType().FullName + "#1", JsonConvert.SerializeObject(oDevMakePro));
                DataManipulation dataManipulation = new DataManipulation() { Data = arrPara };
                bool isVerifyTag = true;
                if (!isIngoreAR)
                    isVerifyTag = VerifyTag(rfidCode, progCode);
                if (isVerifyTag)
                {
                    bool bResult = oDMService.ExecSave(dbName, dataManipulation, sqlMapperId);
                    if (!bResult)
                    {
                        oCallbackProxy.ClientSendMsg(System.Net.Dns.GetHostName() + "【" + rfidCode + "】資料異動失敗.");
                        //復原 Tag 值
                        if (!isIngoreAR)
                        {
                            SetTagEpc(INIT_CODE, rfidCode);
                            lblCurrCode.Text = INIT_CODE;
                        }
                        lblCurrCode.ForeColor = Color.Red;
                        msg = "【ERR】Save data fail. ";
                        Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
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
                string[] arrCell = { oJson["Barcode"], oJson["AtcNo"], oJson["OdrNo"] + "-" + oJson["OdrVer"], oJson["Size"], lrMk, oJson["RFID"] };
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
                lblCurrCode.Text = INIT_CODE;
                lblCurrCode.ForeColor = Color.Red;
                msg = "【ERR】Exception Error " + ex.Message;
                Common.setStatusInfo(lstStatus, msg, picStatus, Resources.Error, 2);
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
                        if (iTagNum == 0 && orgEpc == "")
                            SetStatus("【ERR】No tag found or Unknown tag error.");
                        else if (iTagNum > 1)
                            SetStatus("【ERR】Write tag fail, because more than one tag in the Field.");
                        else if (!orgEpc.Replace(" ", "").Trim().Equals(INIT_CODE))
                            SetStatus("【ERR】The tag not initial.");
                    }
                    else
                    {
                        epcCode = GetTagEPC();
                        if (!epcCode.Replace(" ", "").Trim().Equals(INIT_CODE))
                        {
                            lblOrigCode.Text = epcCode;
                            SetStatus("【ERR】The tag not initial.");
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

        /// <summary>清空部分</summary>
        private void ResetPart()
        {
            txtBarcode.Clear();
            lblOrigCode.Text = "";
            lblCurrCode.Text = "";
            // txtBarcode.Select();
        }

        /// <summary> 重設所有 </summary>
        private void ResetAll()
        {
            iOkNum = 0;
            txtBarcode.Text = "";
            lblOkNum.Text = "0";
            picStatus.Image = null;
            dgvBarcode.Rows.Clear();
            dgvHistory.Rows.Clear();
            ResetPart();
        }

        /// <summary> 狀態信息</summary>
        private void SetStatus(string infoStatus)
        {
            lstStatus.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + infoStatus);
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

        private void txtJson_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtJson, txtJson.Text);
        }

        private void chkReader_CheckedChanged(object sender, EventArgs e)
        {
            isIngoreAR = chkReader.Checked;
            picConn.Visible = !isIngoreAR;
            panelPort.Visible = !isIngoreAR;
        }

    }
}
