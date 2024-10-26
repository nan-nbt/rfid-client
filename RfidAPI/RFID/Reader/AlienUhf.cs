using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RFID.Command;
using System.Threading;
using System.Windows.Forms;
using UR4RFID;

namespace RFID.Reader
{
    public class AlienUhf : IReader
    {
        public int qtySucc = 0;
        private int iTryTime = 3, iMinPower = 11, iMaxPower = 22;
        private string _readerPortNm;
        public static ur4Reader uhf = new ur4Reader();
        private Dictionary<string, string> arrDict = new Dictionary<string, string>();
        List<TagStruct> lstRfidTag = new List<TagStruct>();
        string tagList = "", oldTag = "", tagNew = "", tagVal = "";
        /// <summary>定义委托</summary>
        public delegate void MessageReceivedEventHandler(string msg);
        /// <summary>此委托类型的事件</summary>
        public event MessageReceivedEventHandler MessageReceived;
        Thread oThread = null;
        /// <summary> 设置输出功率为10（Max=30） </summary>
        public bool SetPower() { return uhf != null && uhf.SetPower(1, 10).IndexOf("Success") > -1 ? true : false; }
        public bool StopGet() { try { uhf.StopEPC(); return true; } catch (Exception) { return false; } }
        /// <summary> UHF 讀寫功率及重試次數 </summary>
        public int WriteTime(int iMinOutP, int iMaxOutP, int iTime)
        {
            iMinPower = iMinOutP == 0 ? 11 : iMinOutP;
            iMaxPower = iMaxOutP == 0 ? 22 : iMaxOutP;
            iTryTime = iTime == 0 ? 3 : iTime;
            return iTryTime;
        }
        /// <summary>自动执行</summary>
        public void AutoRun()
        {
            bool bLoop = false;
            do
            {
                if (MessageReceived != null)
                {
                    oldTag = getTag();
                    if (!string.IsNullOrEmpty(oldTag))
                    {
                        tagVal = "00 00 00 00 00 00 00 00 00 00 00 00";
                        ProgTagBySingle(1, tagVal);
                    }
                    tagNew = getTag();
                    if (tagVal != "" && tagNew != "" && !tagVal.Equals(tagNew))
                    {
                        MessageReceived("AUTO MODE EVALUATES TRUE");
                    }
                }
                if (oThread != null)
                    oThread.Join(100);
            } while (!bLoop);
        }

        /// <summary>開啟 Reader 連線</summary>
        public bool OpenConnect(int iType)
        {
            bool bolOk = false;
            if (uhf != null) { Disconnect(); uhf = null; }
            uhf = new ur4Reader();
            switch (iType)
            {
                case 1:
                    bolOk = ConnectBySerial();
                    break;
                case 2:
                    bolOk = ConnectBySerial();
                    break;
                case 3:
                    bolOk = ConnectBySocket();
                    break;
            }
            return bolOk;
        }
        /// <summary>斷開 Reader 連線</summary>
        public bool Disconnect()
        {
            bool bClose = false;
            uhf.Disconnect();
            bClose = !uhf.IsConnected ? true : false;
            if (oThread != null)
            {
                oThread.Abort();
                oThread = null;
            }
            return bClose;
        }
        /// <summary>是否連線</summary>
        public bool IsConnected()
        {
            bool bConn = uhf.IsConnected;
            return bConn;
        }
        /// <summary>成功數量</summary>
        public int GetSuccQty()
        {
            return qtySucc;
        }
        /// <summary>設置參數</summary>
        public bool SetReaderBySingle(ParameterNm commmandNm, Parameters classAttr)
        {
            bool bResult = false;
            if (IsConnected())
            {
                switch (commmandNm)
                {
                    case ParameterNm.SetAcqG2Mask:
                        // bResult = WriteData(classAttr.HEX_BYTES);
                        break;
                }
            }
            return bResult;
        }
        /// <summary>設置參數</summary>
        public bool SetReaderBySingle(ParameterNm commmandNm, string strAttr)
        {
            if (IsConnected())
            {
                switch (commmandNm)
                {
                    case ParameterNm.AcqG2MaskAction:
                        //oReader.AcqG2MaskAction = strAttr;
                        break;
                    case ParameterNm.TagListFormat:
                        //oReader.TagListFormat = strAttr;
                        break;
                    case ParameterNm.AcqG2Mask:
                        //oReader.AcqG2Mask = strAttr;
                        break;
                    case ParameterNm.AutoAction:
                        //oReader.AutoAction = strAttr;
                        break;
                    case ParameterNm.AutoMode:
                        //oReader.AutoMode = strAttr;
                        break;
                    case ParameterNm.ProgDataUnit:
                        //oReader.ProgDataUnit = strAttr;
                        break;
                    case ParameterNm.NotifyTime:
                        //oReader.NotifyTime = strAttr;
                        break;
                    case ParameterNm.NotifyFormat:
                        //oReader.NotifyFormat = strAttr;
                        break;
                    case ParameterNm.NotifyTrigger:
                        //oReader.NotifyTrigger = strAttr;
                        break;
                    case ParameterNm.PersistTime:
                        //oReader.PersistTime = strAttr;
                        break;
                    case ParameterNm.NotifyMode:
                        //oReader.NotifyMode = strAttr;
                        break;
                    case ParameterNm.RSSIFilter:
                        //oReader.RSSIFilter = strAttr;
                        break;
                    case ParameterNm.ComTimeOutInterval:
                        //oReader.ComTimeOutInterval = string.IsNullOrEmpty(strAttr) ? 5000 : Convert.ToInt32(strAttr);
                        break;
                    case ParameterNm.NetworkTimeout:
                        //oReader.NetworkTimeout = strAttr;
                        break;
                    case ParameterNm.RFLevel:
                        //oReader.RFLevel = Convert.ToInt32(strAttr);
                        break;
                    case ParameterNm.RFAttenuation:
                        //oReader.RFAttenuation = string.IsNullOrEmpty(strAttr) ? 90 : Convert.ToInt32(strAttr);
                        break;
                    case ParameterNm.ProgEPCData:
                        //oReader.ProgEPCData = strAttr;
                        break;
                    case ParameterNm.ProgEPCDataInc:
                        //oReader.ProgEPCDataInc = strAttr;
                        break;
                    case ParameterNm.NotifyAddress:
                        //oReader.NotifyAddress = strAttr;
                        break;
                    case ParameterNm.NotifyHeader:
                        //oReader.NotifyHeader = strAttr;
                        break;
                    case ParameterNm.Mask:
                        //oReader.Mask = strAttr;
                        break;
                }
                return true;
            }
            return false;
        }

        public bool SetReaderBySingle(ParameterNm commmandNm, int iAttr)
        {
            return true;
        }
        /// <summary>執行命令 </summary>
        public bool SetReaderBySingle(ParameterNm commmandNm)
        {
            if (IsConnected())
            {
                switch (commmandNm)
                {
                    case ParameterNm.AutoModeReset:
                        //    oReader.AutoModeReset();
                        break;
                }
                return true;
            }
            return false;
        }
        /// <summary>讀取 Tag 通過 Bank</summary>
        public string ReadTagByBank(int iBank, string wordPtr, string wordLen)
        {
            string tag = "";
            if (IsConnected())
            {
                string info = getTag();
                tag = !string.IsNullOrEmpty(info) ? info : "";
            }
            return tag;
        }

        /// <summary> 取得Tag信息 </summary>
        private string getTag()
        {
            string info = uhf.Read(0, "");
            if (string.IsNullOrEmpty(info))
            {
                SpinWait.SpinUntil(() => false, 100);
                info = uhf.Read(0, "");
            }
            return info;
        }

        /// <summary>读取Tag</summary>
        private void ReadEPC()
        {
            string info = getTag();
            if (!string.IsNullOrEmpty(info))
            {
                tagList = info;
                Console.Out.Write("Refresh");
            }
            else
            {
                tagList = "";
                lstRfidTag.Clear();
            }
        }

        /// <summary>獲取 TagList 數據</summary>
        public List<TagStruct> GetTagList()
        {
            string[] arrTag;
            string[] arrTagAttr;
            lstRfidTag = new List<TagStruct>();
            if (IsConnected())
            {
                new Thread(new ThreadStart(delegate { ReadEPC(); })).Start();
                if (!String.IsNullOrEmpty(tagList) && tagList.IndexOf(',') != -1)
                {
                    arrTag = new string[1];
                    arrTag[0] = tagList;
                    if (tagList.IndexOf("\r\n") != -1)
                    {
                        tagList = tagList.Replace("\r\n", "_");
                        arrTag = tagList.Split('_');
                    }

                    for (int i = 0; i < arrTag.Length; i++)
                    {
                        if (arrTag[i].IndexOf(',') != -1)
                        {
                            arrTagAttr = arrTag[i].Split(',');
                            lstRfidTag.Add(new TagStruct()
                            {
                                TagID = arrTagAttr[0].Replace(" ", "")
                            });
                        }
                    }
                }
                else
                {
                    lstRfidTag.Add(new TagStruct() { TagID = tagList });
                }
            }
            return lstRfidTag;
        }

        /// <summary>清除 TagList 數據</summary>
        public void ClearTagList()
        {
            if (IsConnected()) { lstRfidTag.Clear(); }
        }
        /// <summary>增加綁定方法 </summary>
        public void AddMessageReceived(ReceiveMsg oReceiveMsg)
        {
            if (IsConnected())
                this.MessageReceived += new AlienUhf.MessageReceivedEventHandler(oReceiveMsg);
            oThread = new Thread(new ThreadStart(delegate { AutoRun(); }));
            oThread.Start();
            return;
        }
        /// <summary>移除綁定方法 </summary>
        public void RemoveMessageReceived(ReceiveMsg oReceiveMsg)
        {
            if (IsConnected())
                this.MessageReceived -= new AlienUhf.MessageReceivedEventHandler(oReceiveMsg);
            if (oThread != null)
            {
                oThread.Abort();
                oThread = null;
            }
            return;
        }
        /// <summary>寫入Tag命令</summary>
        public string ProgTagBySingle(int iBank, string progCode)
        {
            switch (iBank)
            {
                case 1:
                    bool bOk = WriteData(progCode);
                    return bOk ? FormatStr(progCode) : "";
                default:
                    return "";
            }
        }

        /// <summary>寫數據</summary>
        private bool WriteData(string epcCode)
        {
            int iTime = 0;
            bool bResult = false;
            bool bLoop = false;
            string newVal = "", retMsg = "";
            string comVal = epcCode.Replace(" ", "");
            string oldVal = getTag();
            if (!comVal.Equals(oldVal) && !string.IsNullOrEmpty(oldVal))
            {
                do
                {
                    iTime++;
                    newVal = "";
                    retMsg = "";
                    oldVal = getTag();
                    if (!comVal.Equals(oldVal))
                    {
                        uhf.SetPower(1, iMaxPower);
                        retMsg = uhf.ProgramEPC(comVal);
                        uhf.SetPower(1, iMinPower);
                        newVal = getTag();
                        // qtySucc = comVal.Equals(newVal) ? (qtySucc + 1) : qtySucc;
                        if ((comVal.Equals(newVal) || retMsg.IndexOf("True") > 0) && !arrDict.ContainsKey(oldVal))
                        {
                            qtySucc++;
                            arrDict.Add(oldVal, oldVal);
                        }
                    }
                    bResult = comVal.Equals(newVal) ? true : bResult;
                    bLoop = bResult || (iTryTime > 0 && iTime >= iTryTime) ? true : false;

                } while (!bLoop);
            }
            return bResult;
        }

        /// <summary>設置 Port</summary>
        public void SetSerialPort(string portNm)
        {
            _readerPortNm = portNm;
        }
        /// <summary>取得所有 Port </summary>
        public string SearchSerialPort()
        {
            int iPort = 0;
            // IReaderInfo[] rs;
            // oReadMonitor = new clsReaderMonitor();
            // if (String.IsNullOrEmpty(oReadMonitor.CheckComPorts()))
            // {
            //     iPort = oReadMonitor.GetReaderListOnSerial(out rs);
            //     if (iPort > 0)
            //     {
            //         _readerPortNm = rs[0].IPAddress;
            //         return _readerPortNm;
            //     }
            // }
            return "";
        }
        /// <summary>截取并格式化 Tag</summary>
        public string TagFormatByBank(int iBank, string tag)
        {
            switch (iBank)
            {
                case 1:
                    if (tag.Length >= 35)
                        tag = tag.Substring(0, 35);//2*12 (字) + 11 (空白)
                    else
                        tag = tag.Substring(0, 23);//2*12 (字)
                    tag = FormatStr(tag);
                    return tag;
                default:
                    return "";
            }
        }
        /// <summary>連線 Reader </summary>
        private bool ConnectBySerial()
        {
            bool bResult = false;
            if (String.IsNullOrEmpty(_readerPortNm))
                _readerPortNm = SearchSerialPort();
            uhf.Connect(_readerPortNm);
            bResult = uhf.IsConnected ? true : false;
            return bResult;
        }

        private int getComPort()
        {
            int com = 0;//串口号
            string tmp = _readerPortNm.Replace("COM", "");
            com = int.Parse(_readerPortNm.Replace("COM", ""));
            return com;
        }

        private bool ConnectBySocket()
        {
            return false;
        }

        private eG2BankUhf GetTagBankByIdx(int iBank)
        {
            switch (iBank)
            {
                case 1:
                    return eG2BankUhf.EPC;
                case 2:
                    return eG2BankUhf.RESERVED;
                case 3:
                    return eG2BankUhf.TID;
                case 4:
                    return eG2BankUhf.USER;
                default:
                    return eG2BankUhf.EPC;
            }
        }

        private string FormatStr(string vPara, int iChar = 2)
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
    }

    enum eG2BankUhf
    {
        RESERVED = 0,
        EPC = 1,
        TID = 2,
        USER = 3,
    }
}
