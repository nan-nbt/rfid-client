using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
namespace Generic
{
    public class AppConfig
    {
        /// <summary>日志保留天数</summary>
        public static string LogKeepDay { get { return ConfigurationManager.AppSettings["LogKeepDay"].Trim(); } }
        /// <summary>自動分割日志文件大小(單位:MB) </summary>
        public static string LogSplitSize { get { return ConfigurationManager.AppSettings["LogSplitSize"].Trim(); } }
        /// <summary>Reader 有效天線 </summary>
        public static string AntennaSequence { get; set; }
        /// <summary>Reader 衰減功率</summary>
        public static string RFAttenuation { get; set; }
        /// <summary>Reader 接收信號強度 </summary>
        public static string RssiFilter { get; set; }
        /// <summary>Reader 天線功率層級</summary>
        public static string RFLevel { get; set; }
        /// <summary>UHF Read Power</summary>
        public static string ReadPower { get; set; }
        /// <summary>UHF Write Power </summary>
        public static string WritePower { get; set; }
        /// <summary>UHF write try times</summary>
        public static string WriteTime { get; set; }
        /// <summary>多國語言類型</summary>
        public static string LangType { get; set; }
        /// <summary>多國語言翻譯</summary>
        public static string LangData
        {
            get
            {
                string retLang = "";
                string langPath = AppDomain.CurrentDomain.BaseDirectory + "Lang.json";
                if (File.Exists(langPath)) { retLang = File.ReadAllText(langPath); }
                return retLang;
            }
        }
        /// <summary>Com Timeout </summary>
        public static string ComTimeOutInterval
        {
            get
            {
                string strComTimeOutInterval = ConfigurationManager.AppSettings["ComTimeOutInterval"].Trim();
                if (String.IsNullOrEmpty(strComTimeOutInterval))
                    strComTimeOutInterval = "60000";
                return strComTimeOutInterval;
            }
        }
        /// <summary>Network Timeout </summary>
        public static string NetworkTimeout
        {
            get
            {
                string strNetworkTimeout = ConfigurationManager.AppSettings["NetworkTimeout"].Trim();
                if (String.IsNullOrEmpty(strNetworkTimeout))
                    strNetworkTimeout = "90";
                return strNetworkTimeout;
            }
        }
        /// <summary>Log 位置 </summary>
        public static string LogPath { get { return AppDomain.CurrentDomain.BaseDirectory + "Log"; } }
        /// <summary>Log 名稱 </summary>
        public static string LogName { get { return "AppLog.log"; } }
        /// <summary>目前資料庫名稱</summary>
        public static string CurrentDBName { get; set; }
        /// <summary>目前資料庫連線資訊</summary>
        public static string CurrentDBConn { set; get; }
        /// <summary>中心資料庫名稱</summary>
        public static string CenterDBName { get; set; }
        /// <summary>中心資料庫連線資訊</summary>
        public static string CenterDBConnection { set; get; }
        /// <summary>語言類別</summary>
        public static Dictionary<string, string> LangList
        {
            get
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                oDic.Add("EN", "English");
                oDic.Add("CN", "简体中文");
                oDic.Add("TW", "繁體中文");
                oDic.Add("VN", "Việt nam");
                oDic.Add("ID", "Indonesia");
                return oDic;
            }
        }
        /// <summary>應用模塊集</summary>
        public static Dictionary<string, string> ModuleList
        {
            get
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                oDic.Add("M0001", Lang.Dict("C0007", "批量初始化標籤"));
                oDic.Add("M0002", Lang.Dict("C0008", "制程掃描"));
                oDic.Add("M0003", Lang.Dict("C0009", "樣品倉入出庫"));
                oDic.Add("M0004", Lang.Dict("C0010", "上消磁門禁管控"));
                return oDic;
            }
        }
        /// <summary>國家地區類別</summary>
        public static Dictionary<string, string> CountryList
        {
            get
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                oDic.Add("", "");
                if (RFIDClient.FrmLogin.oFrmLogin.chkBoxTest.Checked)
                {
                    oDic.Add("DEV_TEST", "開發測試 (DevTest)");
                    oDic.Add("USER_TEST", "用戶測試 (UserTest)");
                }
                oDic.Add("CN", "中國 (China)");
                oDic.Add("TW", "臺灣 (Taiwan)");
                oDic.Add("VN", "越南 (Vietnam)");
                oDic.Add("ID", "印尼 (Indonesia)");
                return oDic;
            }
        }
        /// <summary>取得服務端地址</summary>
        public static string CurrentCountry
        {
            get
            {
                switch (CenterDBName)
                {
                    case "CN":
                        return "dmscn.pouchen.com:8108";
                    case "VN":
                        return "dmsvn.pouchen.com:8108";
                    case "ID":
                        return "dmsid.pouchen.com:8108";
                    case "TW":
                        return "dms.pouchen.com:8108";
                    case "USER_TEST":
                        bool bPing = Ping("dgepptest.cn.pouchen.com");
                        if (bPing)
                            return "dgepptest.cn.pouchen.com:8108";//172.17.2.191
                        else
                            return "172.17.11.26:8108";
                    case "DEV_TEST":
                        return "localhost:8108";
                    default:
                        return "localhost:8108";
                }
            }
        }

        /// <summary></summary>
        public static bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);
            int timeout = 1000; // Timeout 时间，单位：毫秒
            try
            {
                System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception) { return false; }
        }

        /// <summary>十個手指名稱</summary>
        public static Dictionary<string, string> FingerList
        {
            get
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                oDic.Add("0", "右手拇指 (thumb of the right hand)");
                oDic.Add("1", "右手食指 (index finger of the right hand)");
                oDic.Add("2", "右手中指 (middle finger of the right hand)");
                oDic.Add("3", "右手無名指 (ring finger of the right hand)");
                oDic.Add("4", "右手小指 (little finger of the right hand)");
                oDic.Add("5", "左手拇指 (thumb of the left hand)");
                oDic.Add("6", "左手食指 (index finger of the left hand)");
                oDic.Add("7", "左手中指 (middle finger of the left hand)");
                oDic.Add("8", "左手無名指 (ring finger of the left hand)");
                oDic.Add("9", "左手小指 (little finger of the left hand)");
                return oDic;
            }
        }
        /// <summary>COM端口</summary>
        public static Dictionary<string, string> ComPortList
        {
            get
            {
                string[] ArryPort = System.IO.Ports.SerialPort.GetPortNames();
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                for (int i = 0; i < ArryPort.Length; i++)
                {
                    oDic.Add(ArryPort[i], ArryPort[i]);
                }
                return oDic;
            }
        }
        /// <summary>選讀寫器類型</summary>
        public static Dictionary<string, string> GetReaderList
        {
            get
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                oDic.Add("Alien", "1");
                oDic.Add("Alien UHF", "2");
                oDic.Add("NPX", "3");
                return oDic;
            }
        }

        /// <summary>SSO Check User Wizard Uri</summary>
        public static string SsoCheckUserWizard
        {
            get
            {
                switch (CenterDBName)
                {
                    case "CN":
                        return "ssocn.pouchen.com/cas/signon/checkUserWizard.jsp";
                    case "VN":
                        return "ssovn.pouchen.com/cas/signon/checkUserWizard.jsp";
                    case "ID":
                        return "ssoid.pouchen.com/cas/signon/checkUserWizard.jsp";
                    case "TW":
                        return "sso.pouchen.com/cas/signon/checkUserWizard.jsp";
                    case "USER_TEST":
                        return "ssocn.pouchen.com/cas/signon/checkUserWizard.jsp";
                    case "DEV_TEST":
                        return "ssocn.pouchen.com/cas/signon/checkUserWizard.jsp";
                    default:
                        return "ssocn.pouchen.com/cas/signon/checkUserWizard.jsp";
                }
            }
        }

    }

}