using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;
using System.Drawing;
using System.Threading;
// Ref Other
using DPUruNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Generic;
using RFIDModel.Model.POJO;
using RFIDModel.Interface.POJO;
using RFIDModel.Common;
using System.Management;
using System.Text.RegularExpressions;

namespace RFIDClient
{
    public partial class FrmLogin : Form, IMessageFilter
    {
        private string connNm = "", connStr = "", comConnNm = "";
        //private string login = "", pwd = "";
        private string ssouid = ""; // anan add this variable 20240902
        private string langType = "", country = "", factNoIdx = "";
        ReadWriteJson oReadWriteJson = null;
        internal static FrmLogin oFrmLogin = null;
        int iMatchFp = 0;   //指紋匹配數量
        string verifyUser = ""; //驗證成功用戶信息
        bool bEnableFp = false; //是否啟用指紋登錄【True:指紋登錄，False:密碼登錄】
        DigitalPersona oDp = null;  //指紋機類對象
        RfidUserFinger oUfMatch = null; //匹配的用戶指紋對象
        IList<RfidUserFinger> userFpList = null;    //用戶指紋集
        public static Fmd[] fmdList = new Fmd[] { };    //用戶特征值集
        private const int DPFJ_PROBABILITY_ONE = 0x7fffffff;    //指紋比較檢驗時閾值常量
        System.Timers.Timer oTimer = null;  //定時檢測指紋讀取器有無連線
        internal static string CT = "2024-09-02 15:25 Compiled";

        public FrmLogin()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            lblMsg.Visible = false;
            oFrmLogin = this;
        }

        /// <summary>禁用鼠標滾輪 </summary>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 522) { return true; } else { return false; }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            oReadWriteJson = new ReadWriteJson();
            List<PropName> paraList = oReadWriteJson.ReadJson();
            PropName oPropName = paraList != null && paraList.Count > 0 ? paraList[0] : new PropName();
            Common.SetAlienPara(oPropName);
            BindLanguage();
            BindCountry();
            langType = oPropName.LangType == null ? "" : oPropName.LangType;
            country = oPropName.Country == null ? "" : oPropName.Country;
            factNoIdx = oPropName.Vendor == null ? "" : oPropName.Vendor;
            if (langType != "")
            {
                if (langType.Contains(":"))
                {
                    chkBoxTest.Checked = true;
                    cbxLang.SelectedValue = langType.Split(':')[1];
                }
                else { cbxLang.SelectedValue = langType; }
            }
            if (country != "" && AppConfig.CountryList.Keys.Contains(country)) { cbxCountry.SelectedValue = country; }
            txtLogin.Text = oPropName.UsrId == null ? "" : oPropName.UsrId;
            this.Text = bntOK.Text + CT.PadLeft(50 - bntOK.Text.Length, ' ');
        }

        private void ChkFpReader(object source, System.Timers.ElapsedEventArgs e)
        {
            CheckReader(Action.CheckReader, "");
        }

        /// <summary> 初始指紋讀取器 </summary>
        private void InitDpReader()
        {
            if (panelMask.Visible)
            {
                try
                {
                    if (oDp != null && oDp.oReader != null && oDp.CaptureFingerAsync()) { return; }
                    oDp = null;
                    oDp = new DigitalPersona();
                    if (!oDp.OpenReader()) { return; }
                    if (!oDp.StartCaptureAsync(this.OnCaptured)) { return; }
                    lblFinger.ForeColor = Color.DarkBlue;
                    lblFinger.Text = "Fingerprint reader has been connected.\r\nPlace your a finger on the reader.";
                }
                catch (Exception)
                {
                    lblFinger.ForeColor = Color.Red;
                    lblFinger.Text = "The DigitalPersona fingerprint reader driver\r\n not installed in your computer.";
                    if (fmdList.Length > 0)
                    {
                        lblFinger.ForeColor = Color.Red;
                        lblFinger.Text = "Fingerprint reader can not connect.\r\nPlease connect it and restart the program.";
                        return;
                    }
                }
            }
        }

        /// <summary>綁定語言 </summary>
        private void BindLanguage()
        {
            cbxLang.SelectedIndexChanged -= new EventHandler(cbxLang_SelectedIndexChanged);
            BindingSource oBs = new BindingSource();
            oBs.DataSource = AppConfig.LangList;
            cbxLang.DataSource = oBs;
            cbxLang.DisplayMember = "Value";
            cbxLang.ValueMember = "Key";
            cbxLang.SelectedIndexChanged += new EventHandler(cbxLang_SelectedIndexChanged);
        }

        /// <summary>綁定國家地區選項 </summary>
        private void BindCountry()
        {
            cbxCountry.SelectedIndexChanged -= new EventHandler(cbxCountry_SelectedIndexChanged);
            BindingSource oBs = new BindingSource();
            oBs.DataSource = AppConfig.CountryList;
            cbxCountry.DataSource = oBs;
            cbxCountry.DisplayMember = "Value";
            cbxCountry.ValueMember = "Key";
            cbxCountry.SelectedIndexChanged += new EventHandler(cbxCountry_SelectedIndexChanged);
            if (country != "" && AppConfig.CountryList.Keys.Contains(country)) { cbxCountry.SelectedValue = country; }
        }

        //語言
        private void cbxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppConfig.LangType = cbxLang.SelectedValue != null ? cbxLang.SelectedValue.ToString().Replace("[", "").Split(',')[0] : "EN";
            Translate();
        }

        //地區
        private void cbxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbxVendor.DataSource = null;
                if (string.IsNullOrEmpty(cbxCountry.SelectedValue.ToString())) { return; }
                string centerDBName = cbxCountry.SelectedValue.ToString().Replace("[", "").Split(',')[0];
                AppConfig.CenterDBName = centerDBName;
                var oService = ServiceFactory<IStkIpMapSiteDAO>.GetService();
                StkIpMapSite oStkIpMapSite = new StkIpMapSite();
                oService.SetConnection(centerDBName);
                oService.SetPara(SqlConst.SelStkIpMapSiteAll, centerDBName, oStkIpMapSite);
                IList<StkIpMapSite> dataList = oService.GetData();
                cbxVendor.DisplayMember = "FactName";
                cbxVendor.ValueMember = "ConnectString";
                cbxVendor.DataSource = dataList;
                oStkIpMapSite = null;
                Common.CloseService(oService);
                if (factNoIdx != "") { cbxVendor.Text = factNoIdx; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary> 翻譯 </summary>
        private void Translate()
        {
            lblLang.Text = Lang.Dict("C0000", "語言");
            lblArea.Text = Lang.Dict("C0001", "地區");
            lblFact.Text = Lang.Dict("C0002", "廠別");
            lblAcct.Text = Lang.Dict("C0003", "帳號");
            lblPwd.Text = Lang.Dict("C0004", "密碼");
            bntOK.Text = Lang.Dict("C0005", "登錄");
            lblMsg.Text = Lang.Dict("C0006", "正在檢查SSO賬號");
            this.Text = bntOK.Text + CT.PadLeft(50 - bntOK.Text.Length, ' ');
        }

        private void cbxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var oFact = cbxVendor.SelectedItem as StkIpMapSite;
            connNm = oFact != null ? oFact.SchemaName : "";
            connStr = oFact != null ? oFact.ConnectString : "";
            Common.FactNo = oFact != null ? oFact.FactNo : "";
            if (connNm != "" && connStr != null)
            {
                AppConfig.CurrentDBName = connNm;
                AppConfig.CurrentDBConn = connStr;
            }
            txtPsw.Enabled = true;
            txtLogin.Focus();
            txtLogin.SelectAll();
            iFailTime = 0;
            if (comConnNm != AppConfig.CurrentDBName)
            {
                txtLogin.Enabled = false;
                txtPsw.Enabled = false;
                GetFingerprint();
                // 普通登錄--測試
                //bEnableFp = false;
                //panelMask.Visible = false;
                //bntOK.Enabled = true;
                //txtPsw.Enabled = true;
                if (bEnableFp)
                {
                    //自動偵測指紋機連線與否
                    if (oTimer == null)
                    {
                        oTimer = new System.Timers.Timer();
                        oTimer.Interval = 1000;
                        oTimer.Elapsed += new System.Timers.ElapsedEventHandler(ChkFpReader);
                        oTimer.AutoReset = true;
                        oTimer.Enabled = true;
                    }
                    oTimer.Enabled = true;
                    InitDpReader();
                }
                else
                {
                    txtLogin.Enabled = true;
                    txtPsw.Enabled = true;
                    if (oTimer != null) { oTimer.Enabled = false; }
                    if (oDp != null && oDp.CaptureFingerAsync())
                        oDp.CancelCaptureAndCloseReader(this.OnCaptured);
                }
                comConnNm = connNm;
            }
        }

        private void bntOK_Click(object sender, EventArgs e)
        {
            // call new window form SSO 2.0 login page
            FrmOAuth2Login oFrm = new FrmOAuth2Login();
            oFrm.StartPosition = FormStartPosition.CenterScreen;
            oFrm.ShowDialog();

            if (oFrm.ssouid != null)
            {
                Application.DoEvents();
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    ssouid = oFrm.ssouid.Trim().ToUpper(); // set pccuid for check in PCT_USER database
                    //login = txtLogin.Text.Trim();
                    //pwd = txtPsw.Text.Trim();
                    //For Test
                    // login = "hanxing.zhong";
                    // pwd = "Bell_921";
                    //if (string.IsNullOrWhiteSpace(login)) { this.Cursor = Cursors.Default; MessageBox.Show("Please input login"); return; }
                    //if (string.IsNullOrWhiteSpace(pwd) && !panelMask.Visible) { this.Cursor = Cursors.Default; MessageBox.Show("Please input password"); return; }
                    //string ssoCheckUserWizard = AppConfig.SsoCheckUserWizard;
                    //if (String.IsNullOrEmpty(ssoCheckUserWizard)) { this.Cursor = Cursors.Default; return; }
                    if (oDp != null && oDp.IsConnect && iMatchFp == 0 && fmdList.Length > 0 && panelMask.Visible) { this.Cursor = Cursors.Default; MessageBox.Show("Place your a finger on the reader."); return; }
                    if (!String.IsNullOrEmpty(ssouid) || iMatchFp > 0)
                    //if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(pwd) || iMatchFp > 0)
                    {
                        if (connNm != "" && connStr != null)
                        {
                            if (!Common.CheckChannel())
                            {
                                picFinger.Image = Properties.Resources.finger_init;
                                lblFinger.ForeColor = Color.Red;
                                lblFinger.Text = "Connection has been disconnected. \nPlease check the network and server program.";
                                return;
                            }
                            var oService = ServiceFactory<IPctUserDAO>.GetService();
                            PctUser oPctUser = new PctUser();
                            oPctUser.SsoUserNo = ssouid.Trim().ToUpper();
                            //oPctUser.SsoUserNo = login.Trim().ToUpper();
                            oService.SetConnection(connNm, connStr);
                            oService.SetPara(SqlConst.SelPctUserAll, connNm, oPctUser, null);
                            string sqlMapperId = oService.GetSqlMapperId();
                            Common.SqlMapperId = sqlMapperId;
                            IList<PctUser> dataList = oService.GetData();
                            oPctUser = null;
                            Common.CloseService(oService);
                            if (dataList == null || dataList.Count == 0) { this.Cursor = Cursors.Default; MessageBox.Show("The account not exist in DMS system.", "Info"); return; }
                            verifyUser = "【" + (dataList.Count > 0 && dataList[0].UsrName != null ? dataList[0].UsrName : dataList[0].UsrEname) + " 】fingerprint verification successful.";
                            SrvStatic.superYn = dataList.Count > 0 ? (dataList[0].SuperYn == null ? "N" : dataList[0].SuperYn) : "";
                            SrvStatic.usrId = dataList.Count > 0 ? dataList[0].UsrId : "TEST_USER" + DateTime.Now.ToString("yyyyMMddHHmmss");
                            SrvStatic.localIP = GetLocalIP();
                            SrvStatic.usrName = dataList.Count > 0 ? dataList[0].UsrName : "";
                            //保存JSON
                            PropName propName = new PropName();
                            AppConfig.LangType = string.IsNullOrEmpty(AppConfig.LangType) ? "EN" : AppConfig.LangType;
                            propName.LangType = chkBoxTest.Checked ? "Y:" + AppConfig.LangType : AppConfig.LangType;
                            propName.Country = AppConfig.CenterDBName;
                            propName.Vendor = cbxVendor.Text.Trim();
                            propName.UsrId = ssouid;
                            //propName.UsrId = login;
                            propName.LocalIP = SrvStatic.localIP;
                            if (oReadWriteJson == null) { oReadWriteJson = new ReadWriteJson(); }
                            oReadWriteJson.WriteJson(propName);
                            oReadWriteJson = null;

                            // open new window MainForm
                            this.Hide();
                            new MainForm().ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Database can not connect.", "Info");
                            return;
                        }
                        if (iMatchFp > 0 && panelMask.Visible)
                        {
                            lblFinger.Text = verifyUser;
                            oDp.CancelCaptureAndCloseReader(this.OnCaptured);
                            oDp = null;
                            Thread.Sleep(1000);
                            Application.RemoveMessageFilter(this);
                            this.Hide();
                            new MainForm().ShowDialog();
                            return;
                        }
                        lblFinger.ResetText();
                        //lblMsg.Text = "Checking the SSO";
                        //帳密編碼
                        //login = System.Web.HttpUtility.UrlEncode(login, Encoding.UTF8);
                        //pwd = System.Web.HttpUtility.UrlEncode(pwd, Encoding.UTF8);
                        //string strUtl = "https://" + ssoCheckUserWizard + "?username=" + login + "&password=" + pwd;
                        // strUtl = "https://iamlab.pouchen.com/auth/realms/pcg/";
                        //Uri target = new Uri(strUtl);
                        //webBrowser.Url = target;
                        //lblMsg.Visible = true;
                        oDp.CancelCaptureAndCloseReader(this.OnCaptured);
                        oDp = null;
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    if (ex.Message.IndexOf("DataSource") > 0)
                        MessageBox.Show("Can not connect Database." + ex.Message);
                    lblMsg.Visible = false;
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                string userWizardJson = webBrowser.DocumentText.Replace("\n\r", "").Trim();
                bool bolPassed = false;
                string loginMsg = "";
                if (!String.IsNullOrEmpty(userWizardJson))
                {
                    // JObject jsonObj = JObject.Parse(userWizardJson);
                    // bolPassed = Convert.ToBoolean(jsonObj["passed"]);
                    // loginMsg = Convert.ToString(jsonObj["message"]);
                    bolPassed = true;
                }
                lblMsg.Visible = false;
                if (!bolPassed)
                {
                    MessageBox.Show("Login fail");
                    return;
                }
                txtLogin.Text = "";
                txtPsw.Text = "";
                Application.RemoveMessageFilter(this);
                this.Hide();
                new MainForm().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                lblMsg.Visible = false;
            }
        }

        public string GetLocalIP()
        {
            try
            {
                bool isEthernet = false;
                bool isDHCP = false;
                IPInterfaceProperties ipProperties;
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    ipProperties = ni.GetIPProperties();
                    isEthernet = ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet;
                    isDHCP = ipProperties.DhcpServerAddresses.Count != 0;
                    if (isEthernet && isDHCP)
                    {
                        foreach (UnicastIPAddressInformation ip in ipProperties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                return ip.Address.ToString();
                            }
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("獲取本機 IP 出錯" + ex.Message);
                return "";
            }
        }

        //用于顯示測試選項
        private void chkBoxTest_CheckedChanged(object sender, EventArgs e)
        {
            BindCountry();
        }

        /// <summary>獲取用戶指紋</summary>
        private void GetFingerprint()
        {
            string clientIP = "", clientMac = "";
            //Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                bEnableFp = false;
                string login = ssouid;
                //login = txtLogin.Text.Trim();
                var oUserService = ServiceFactory<IPctUserDAO>.GetService();
                oUserService.SetConnection(connNm, connStr);
                // 启用开关时， 一律用指纹
                oUserService.SetPara(SqlConst.SelRfidFingerprint, connNm, new PctUser(), null);
                string sqlMaperId = oUserService.GetSqlMapperId();
                IList<PctUser> statusList = oUserService.GetData();
                bEnableFp = (statusList != null && statusList.Count > 0 && Convert.ToString(statusList[0].StopYn).Trim() == "N") ? true : bEnableFp;
                // 未启用开关时， 如果有在设定的IP或Mac范围之内用指紋登錄
                if (!bEnableFp)
                {
                    // IP
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        clientIP = endPoint.Address.ToString();
                        // MessageBox.Show(clientIP);
                    }
                    // MAC
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection mocList = mc.GetInstances();
                    foreach (ManagementObject m in mocList)
                    {
                        if ((bool)m["IPEnabled"] == true)
                        {
                            string mac = Regex.Replace(m["MacAddress"].ToString(), "-|:", "");
                            clientMac = clientMac == "" ? mac : clientMac + "','" + mac;
                        }
                    }
                    mocList = null;
                    mc = null;
                    oUserService.SetPara(SqlConst.SelClientIP, connNm, new PctUser() { LoginIp = clientIP, UsrId = clientMac }, null);
                    IList<PctUser> ipList = oUserService.GetData();
                    bEnableFp = (ipList != null && ipList.Count > 0) ? true : bEnableFp;
                }
                Common.CloseService(oUserService);
                // User Fingerprint
                fmdList = null;
                fmdList = new Fmd[] { };
                if (bEnableFp)
                {
                    var oService = ServiceFactory<IRfidUserFingerDAO>.GetService();
                    RfidUserFinger oRfidUserFinger = new RfidUserFinger();
                    //oRfidUserFinger.SsoUserNo = login.Trim().ToUpper();
                    oService.SetConnection(connNm, connStr);
                    oService.SetPara(SqlConst.SelRfidUserFingerAll, connNm, oRfidUserFinger, sqlMaperId);
                    userFpList = oService.GetData();
                    oRfidUserFinger = null;
                    Common.CloseService(oService);
                    if (userFpList != null && userFpList.Count > 0)
                    {
                        fmdList = new Fmd[userFpList.Count];
                        for (var i = 0; i < userFpList.Count; i++)
                        {
                            try
                            {
                                //未安裝好指紋驅動時無法使用 Fmd.DeserializeXml 方法，故在此用 try 語法
                                Fmd oFmd = Fmd.DeserializeXml(userFpList[i].FingerData);
                                fmdList[i] = oFmd;
                            }
                            catch { continue; }
                        }
                    }
                    if (fmdList.Length > 0)
                    {
                        txtLogin.Enabled = false;
                        txtPsw.Enabled = false;
                        bntOK.Enabled = false;
                        panelMask.Visible = true;
                        lblFinger.Visible = true;
                        picFinger.Image = Properties.Resources.finger_init;
                        panelMask.BringToFront();
                    }
                }
                //throw new Exception("Customize error");
            }
            catch (Exception)
            {
                bEnableFp = false;
            }
            finally
            {
                //無指紋或未啟用指紋登錄
                if (!bEnableFp || fmdList.Length == 0)
                {
                    txtLogin.Enabled = true;
                    txtPsw.Enabled = true;
                    bntOK.Enabled = true;
                    panelMask.Visible = false;
                    lblFinger.Visible = false;
                    picFinger.Image = null;
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                if (!oDp.CheckCaptureResult(captureResult)) return;
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
                Fmd anyFinger = resultConversion.Data;
                int thresholdScore = DPFJ_PROBABILITY_ONE * 1 / 100000;
                IdentifyResult identifyResult = Comparison.Identify(anyFinger, 0, fmdList, thresholdScore, 2);
                iMatchFp = identifyResult.Indexes.Length;
                oUfMatch = null;
                for (var i = 0; i < userFpList.Count; i++)
                {
                    if (oUfMatch != null) { break; }
                    for (var j = 0; j < iMatchFp; j++)
                    {
                        if (identifyResult.Indexes[j][0] == i) { oUfMatch = userFpList[i]; break; }
                    }
                }
                SendMessage(Action.SendMessage, "Place your a finger on the reader.");
            }
            catch (Exception) { }
        }

        #region SendMessage
        private enum Action
        {
            SendMessage,
            CheckReader
        }
        int iFailTime = 0;
        private delegate void SendMessageCallback(Action action, string payload);
        private void SendMessage(Action action, string payload)
        {
            try
            {
                if (this.lblMsg.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            Application.DoEvents();
                            this.Cursor = Cursors.WaitCursor;
                            lblMsg.Text = payload;
                            picFinger.Image = null;
                            if (oDp != null && oDp.IsConnect)
                            {
                                if (iMatchFp > 0)
                                {
                                    if (oTimer != null)
                                    {
                                        oTimer.Enabled = false;
                                        oTimer.AutoReset = false;
                                        oTimer.Close();
                                        oTimer.Dispose();
                                    }
                                    SrvStatic.usrName = oUfMatch.UsrName;
                                    txtLogin.Text = oUfMatch.SsoUserNo;
                                    lblFinger.ForeColor = Color.Green;
                                    lblFinger.Text = "【" + SrvStatic.usrName + "】fingerprint verification successful.\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    picFinger.Image = Properties.Resources.finger_ok;
                                    bntOK_Click(null, null);
                                }
                                else
                                {
                                    iFailTime++;
                                    lblFinger.ForeColor = Color.Red;
                                    picFinger.Image = Properties.Resources.finger_no;
                                    lblFinger.Text = "Fingerprint verification fail.\r\n【" + iFailTime + "】" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }
                            this.Cursor = Cursors.Default;
                            break;
                    }
                }
            }
            catch (Exception) { }
        }
        /// <summary>偵測指紋讀取器委托方法</summary>
        private delegate void CheckReaderCallback(Action action, string payload);
        private void CheckReader(Action action, string payload)
        {
            try
            {
                if (this.lblMsg.InvokeRequired)
                {
                    CheckReaderCallback d = new CheckReaderCallback(CheckReader);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    InitDpReader();
                    if (oDp != null && oDp.oReader != null && oDp.CaptureFingerAsync())
                    {
                        if (iFailTime == 0)
                        {
                            lblFinger.ForeColor = Color.DarkBlue;
                            lblFinger.Text = "Fingerprint reader has been connected.\r\nPlace your a finger on the reader.";
                        }
                    }
                    else
                    {
                        lblFinger.ForeColor = Color.Red;
                        picFinger.Image = Properties.Resources.finger_init;
                        lblFinger.Text = "Fingerprint reader has been disconnected.\r\nPlease connect it.";
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            // call logout method for logout from OAuth 2.0 | anan add 20240902
            FrmOAuth2Login oFrm = new FrmOAuth2Login();
            oFrm.Logout();
            if (oDp != null && oDp.IsConnect)
            {
                oDp.CancelCaptureAndCloseReader(this.OnCaptured);
                oDp = null;
            }
            Environment.Exit(0);
            this.Close();
        }

        private void picAlien_Click(object sender, EventArgs e)
        {
            new FrmSetAlien().ShowDialog();
        }
    }
}
