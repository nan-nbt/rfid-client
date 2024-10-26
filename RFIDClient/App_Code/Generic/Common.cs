using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ServiceModel;
using RFIDModel.Common;

namespace Generic
{
    public class Common
    {
        /// <summary>当前應用程序路径</summary>
        public static string appPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>当前 sqlMapperId</summary>
        public static string SqlMapperId = "";
        /// <summary>當前通道錯誤信息</summary>
        public static string CurStatusMsg = "";
        /// <summary>通道中斷提示信息</summary>
        public const string CHANNEL_FAULT_INFO = "Channel fault has been detected. \n\nPlease check the network and server. \nIf check ok, please try again later.";
        /// <summary>服務器通訊地址</summary>
        public static string ServerURI = "";
        /// <summary>當前登錄廠別</summary>
        public static string FactNo = "";
        /// <summary>關閉通道服務</summary>
        public static void CloseService(dynamic oService)
        {
            if (oService != null)
            {
                (oService as ICommunicationObject).Abort();
                (oService as ICommunicationObject).Close();
            }
            oService = null;
        }

        /// <summary>除去半角或全角后是否为空 (true:空; false:非空)</summary>
        public static bool isEmpty(string sPara)
        {
            return Regex.Replace(sPara, @"\s", "") == "" ? true : false;
        }

        /// <summary>截去前后空格</summary>
        public static string Trim(object oPara)
        {
            return oPara != null ? oPara.ToString().Trim() : "";
        }

        public static string GetLoginInfoLog()
        {
            string strLoginInfo = "";
            try
            {
                string logPath = AppConfig.LogPath;
                string fileName = logPath + "\\" + "LoginInfo.log";
                if (!Directory.Exists(logPath))
                    return "";
                strLoginInfo = File.ReadAllText(fileName);
            }
            catch
            {

            }
            return strLoginInfo.Trim();
        }

        public static void SetLoginInfoLog(string loginInfo)
        {
            try
            {
                string logPath = AppConfig.LogPath;
                string fileName = logPath + "\\" + "LoginInfo.log";
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);
                if (!File.Exists(fileName))
                    using (File.Create(fileName)) { }
                FileInfo oFi = new FileInfo(fileName);
                using (StreamWriter oSw = new StreamWriter(fileName, false))
                {
                    oSw.Write(loginInfo);
                    oSw.Close();
                };
            }
            catch
            {

            }
        }

        ///<summary>記錄日志</summary>
        public static void WriteLog(string logMsg)
        {
            string dtTmp = "yyyyMMddHHmmss", prefix = "AppLog_";
            string logPath = AppConfig.LogPath;
            string fileName = logPath + "\\" + AppConfig.LogName;
            int iLogSplitSize = string.IsNullOrEmpty(AppConfig.LogSplitSize) ? 5 : Convert.ToInt32(AppConfig.LogSplitSize);
            double dLogKeepDay = string.IsNullOrEmpty(AppConfig.LogKeepDay) ? 30 : Convert.ToInt32(AppConfig.LogKeepDay);
            if (!Directory.Exists(logPath)) { Directory.CreateDirectory(logPath); }
            if (!File.Exists(fileName)) { using (File.Create(fileName)) { } }
            FileInfo oFi = new FileInfo(fileName);
            //每 5MB 分割一個日志文件
            if (oFi.Length > 1024 * 1024 * iLogSplitSize)
            {
                File.Move(fileName, appPath + prefix + DateTime.Now.ToString(dtTmp) + ".log");
                if (!File.Exists(fileName)) { using (File.Create(fileName)) { } }
            }
            using (StreamWriter oSw = File.AppendText(fileName))
            {
                oSw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " => " + logMsg);
                oSw.Close();
            }
            //自動刪除舊日志
            int iCurDay = DateTime.Now.Day;
            if (Directory.Exists(logPath) && iCurDay < 8)
            {
                DateTime oDtOld = DateTime.Now.AddDays(-dLogKeepDay);
                DirectoryInfo oLogDI = new DirectoryInfo(logPath);
                FileInfo[] arrFile = oLogDI.GetFiles(prefix + "*", SearchOption.TopDirectoryOnly);
                string comTime = oDtOld.ToString(dtTmp);
                foreach (FileInfo file in arrFile)
                {
                    string fileTime = file.Name.Substring(prefix.Length, dtTmp.Length);
                    if (Convert.ToDecimal(fileTime) < Convert.ToDecimal(comTime))
                    {
                        if (file.Exists) { file.Delete(); }
                    }
                }
                oLogDI = null;
                arrFile = null;
            }
        }

        /// <summary> 檢測 USB 是否啟用 </summary>
        public static bool IsEnabledUSB()
        {
            string keyPath = "", keyValue = "";
            RegistryKey regKey = Registry.LocalMachine;
            keyPath = @"SYSTEM\CurrentControlSet\Services\USBSTOR";
            RegistryKey openKey = regKey.OpenSubKey(keyPath);
            keyValue = openKey.GetValue("Start").ToString(); //键值: 3-开启, 4-关闭  
            openKey.Close();
            return keyValue == "3" ? true : false;
        }

        /// <summary>格式字串</summary>
        public static string FormatStr(string vPara, int iChar = 2)
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

        /// <summary>調用蜂鳴器: dwFreq-频率高低，dwDuration-响时長短  </summary>
        public static void InvokeBeep(int dwFreq, int dwDuration, int iTime = 1) { for (var i = 1; i <= iTime; i++) { Beep(dwFreq, dwDuration); } }
        [DllImport("kernel32.dll", EntryPoint = "Beep")]
        public static extern int Beep(int dwFreq, int dwDuration);
        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref   int dwFlag, int dwReserved);
        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;
        /// <summary>檢查網絡</summary>
        public static bool CheckNetwork()
        {
            bool bStatus = true;
            int dwFlag = 0;
            if (!InternetGetConnectedState(ref dwFlag, 0)) { bStatus = false; }
            if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0) { bStatus = true; }  //MessageBox.Show("采用调制解调器上网。");                
            if ((dwFlag & INTERNET_CONNECTION_LAN) != 0) { bStatus = true; }        //MessageBox.Show("采用网卡上网。");
            return bStatus;
        }

        /// <summary>設置狀態欄提示信息</summary>
        public static void setStatusInfo(ListBox lstStatus, string infoStatus, PictureBox picStatus, System.Drawing.Bitmap picSrc, int iTime = 1)
        {
            picStatus.Image = picSrc;
            lstStatus.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + infoStatus);
            Common.InvokeBeep(50000, 500, iTime);
            lstStatus.SelectedIndex = 0;
        }

        /// <summary>檢測通道是否連通</summary>
        public static bool CheckChannel(dynamic oProxyCallback = null)
        {
            bool bConnOk = false;
            var oProxy = string.IsNullOrWhiteSpace(Convert.ToString(oProxyCallback)) ? ServiceFactory<IClientSendMsg>.GetService() : oProxyCallback;
            try { oProxy.ClientSendMsg(""); Common.CurStatusMsg = ""; bConnOk = true; }
            catch (Exception ex) { Common.CurStatusMsg = ex.Message; }
            if (Common.CurStatusMsg.Contains("10061") && Common.CurStatusMsg.ToLower().Contains(Common.ServerURI))
                bConnOk = false;
            CloseService(oProxy);
            return bConnOk;
        }

        /// <summary>檢查是否數字</summary>
        public static bool ChkNumber(string vPara)
        {
            const string REGEX_COM = @"^[0-9]*$";
            Match m = Regex.Match(vPara, REGEX_COM);
            return m.Success;
        }

        /// <summary>設置 Reader 參數</summary>
        public static void SetAlienPara(PropName propName)
        {
            AppConfig.AntennaSequence = ConfigurationManager.AppSettings["AntennaSequence"].Trim();
            AppConfig.RFAttenuation = ConfigurationManager.AppSettings["RFAttenuation"].Trim();
            AppConfig.RssiFilter = ConfigurationManager.AppSettings["RssiFilter"].Trim();
            AppConfig.RFLevel = ConfigurationManager.AppSettings["RFLevel"].Trim();
            if (!string.IsNullOrEmpty(propName.AntennaSequence))
                AppConfig.AntennaSequence = propName.AntennaSequence;
            if (!string.IsNullOrEmpty(propName.RfAttenuation))
                AppConfig.RFAttenuation = propName.RfAttenuation;
            if (!string.IsNullOrEmpty(propName.RssiFilter))
                AppConfig.RssiFilter = propName.RssiFilter;
            if (!string.IsNullOrEmpty(propName.RfLevel))
                AppConfig.RFLevel = propName.RfLevel;
            AppConfig.AntennaSequence = AppConfig.AntennaSequence == "0 1" ? "0" : AppConfig.AntennaSequence;
        }

        /// <summary>轉成DataTable便于排序</summary>
        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            var oDt = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                Type t = GetPropType(prop.PropertyType);
                oDt.Columns.Add(prop.Name, t);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                oDt.Rows.Add(values);
            }
            return oDt;
        }
        public static Type GetPropType(Type t)
        {
            if (t != null && IsNullable(t))
                return !t.IsValueType ? t : Nullable.GetUnderlyingType(t);
            else
                return t;
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

    }
}
