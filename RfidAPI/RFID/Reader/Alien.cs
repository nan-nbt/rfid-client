using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RFID.Command;
using nsAlienRFID2;

namespace RFID.Reader
{
    public class Alien : IReader
    {
        public int qtySucc = 0;
        private int iTryTime = 0, iMinPower = 11, iMaxPower = 22;
        private string _readerPortNm;
        private clsReader oReader = null;
        private clsReaderMonitor oReadMonitor = null;
        public bool SetPower() { return true; }
        public bool StopGet() { return true; }
        /// <summary> UHF 讀寫功率及重試次數 </summary>
        public int WriteTime(int iMinOutP, int iMaxOutP, int iTime)
        {
            iMinPower = iMinOutP == 0 ? 11 : iMinOutP;
            iMaxPower = iMaxOutP == 0 ? 22 : iMaxOutP;
            iTryTime = iTime == 0 ? 3 : iTime;
            return iTryTime;
        }
        /// <summary>開啟 Reader 連線</summary>
        public bool OpenConnect(int iType)
        {
            bool bolOk = false;
            if (oReader != null)
                Disconnect();
            oReader = new clsReader();
            switch (iType)
            {
                case 1:
                    bolOk = ConnectBySerial();
                    break;
                case 2:
                    bolOk = ConnectBySocket();
                    break;
            }
            return bolOk;
        }
        /// <summary>斷開 Reader 連線</summary>
        public bool Disconnect()
        {
            if (oReadMonitor != null)
                oReadMonitor.Dispose();
            if (oReader != null && oReader.IsConnected)
            {
                oReader.AutoModeReset();
                oReader.TagListFormat = "Terse";
                oReader.AcqG2Mask = "0";
                oReader.AutoAction = "Acquire";
                oReader.Disconnect();
                oReader.Dispose();
                oReader = null;
            }
            return true;
        }
        /// <summary>是否連線</summary>
        public bool IsConnected()
        {
            if (oReader != null)
                return oReader.IsConnected;
            return false;
        }
        /// <summary>成功數量</summary>
        public int GetSuccQty()
        {
            return qtySucc;
        }
        /// <summary>設置參數</summary>
        public bool SetReaderBySingle(ParameterNm commmandNm, Parameters classAttr)
        {
            if (oReader != null && oReader.IsConnected)
            {
                switch (commmandNm)
                {
                    case ParameterNm.SetAcqG2Mask:
                        oReader.SetAcqG2Mask(classAttr.MEMORY_BANK, classAttr.BITPTR, classAttr.BITLEN, FormatStr(classAttr.HEX_BYTES));
                        break;
                }
                return true;
            }
            return false;
        }
        /// <summary>設置參數</summary>
        public bool SetReaderBySingle(ParameterNm commmandNm, string strAttr)
        {
            if (oReader != null && oReader.IsConnected)
            {
                switch (commmandNm)
                {
                    case ParameterNm.AcqG2MaskAction:
                        oReader.AcqG2MaskAction = strAttr;
                        break;
                    case ParameterNm.TagListFormat:
                        oReader.TagListFormat = strAttr;
                        break;
                    case ParameterNm.AcqG2Mask:
                        oReader.AcqG2Mask = strAttr;
                        break;
                    case ParameterNm.AutoAction:
                        oReader.AutoAction = strAttr;
                        break;
                    case ParameterNm.AutoMode:
                        oReader.AutoMode = strAttr;
                        break;
                    case ParameterNm.ProgDataUnit:
                        oReader.ProgDataUnit = strAttr;
                        break;
                    case ParameterNm.NotifyTime:
                        oReader.NotifyTime = strAttr;
                        break;
                    case ParameterNm.NotifyFormat:
                        oReader.NotifyFormat = strAttr;
                        break;
                    case ParameterNm.NotifyTrigger:
                        oReader.NotifyTrigger = strAttr;
                        break;
                    case ParameterNm.PersistTime:
                        oReader.PersistTime = strAttr;
                        break;
                    case ParameterNm.NotifyMode:
                        oReader.NotifyMode = strAttr;
                        break;
                    case ParameterNm.RSSIFilter:
                        oReader.RSSIFilter = strAttr;
                        break;
                    case ParameterNm.ComTimeOutInterval:
                        oReader.ComTimeOutInterval = string.IsNullOrEmpty(strAttr) ? 5000 : Convert.ToInt32(strAttr);
                        break;
                    case ParameterNm.NetworkTimeout:
                        oReader.NetworkTimeout = strAttr;
                        break;
                    case ParameterNm.RFLevel:
                        oReader.RFLevel = Convert.ToInt32(strAttr);
                        break;
                    case ParameterNm.RFAttenuation:
                        oReader.RFAttenuation = string.IsNullOrEmpty(strAttr) ? 90 : Convert.ToInt32(strAttr);
                        break;
                    case ParameterNm.ProgEPCData:
                        oReader.ProgEPCData = strAttr;
                        break;
                    case ParameterNm.ProgEPCDataInc:
                        oReader.ProgEPCDataInc = strAttr;
                        break;
                    case ParameterNm.NotifyAddress:
                        oReader.NotifyAddress = strAttr;
                        break;
                    case ParameterNm.NotifyHeader:
                        oReader.NotifyHeader = strAttr;
                        break;
                    case ParameterNm.Mask:
                        oReader.Mask = strAttr;
                        break;
                    case ParameterNm.AntennaSequence:
                        int iMaxAntenna = oReader.MaxAntenna;
                        string tempAntenna;
                        string[] arrAntenna;
                        strAttr = strAttr.Trim();
                        tempAntenna = strAttr.Replace(" ", ",");
                        if (tempAntenna.IndexOf(',') != -1)
                        {
                            strAttr = "";
                            arrAntenna = tempAntenna.Split(',');
                            for (int i = 0; i < arrAntenna.Length; i++)
                            {
                                if (Convert.ToInt32(arrAntenna[i]) <= iMaxAntenna)
                                {
                                    strAttr += arrAntenna[i] + " ";
                                }
                            }
                            strAttr = strAttr.Trim();
                        }
                        else if (Convert.ToInt32(strAttr) > iMaxAntenna)
                        {
                            strAttr = Convert.ToString(iMaxAntenna);
                        }

                        if (!String.IsNullOrEmpty(strAttr))
                        {
                            oReader.AntennaSequence = strAttr;
                            oReader.ProgAntenna = strAttr;
                        }
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
            if (oReader != null && oReader.IsConnected)
            {
                switch (commmandNm)
                {
                    case ParameterNm.AutoModeReset:
                        oReader.AutoModeReset();
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
            if (oReader != null && oReader.IsConnected)
            {
                if (oReader.AutoMode != "ON")
                    oReader.AutoMode = "ON";
                tag = TagFormatByBank(iBank, oReader.G2Read(GetTagBankByIdx(iBank), wordPtr, wordLen));
            }
            return tag;
        }
        /// <summary>獲取 TagList 數據</summary>
        public List<TagStruct> GetTagList()
        {
            string tagList;
            string[] arrTag;
            string[] arrTagAttr;
            List<TagStruct> lstRfidTag = new List<TagStruct>();
            if (oReader != null && oReader.IsConnected)
            {
                oReader.ClearTagList(); //放在此處才合適，清楚舊的偵聽新的
                oReader.TagListFormat = "Terse";
                oReader.AcqG2Mask = "0";
                oReader.AutoAction = "Acquire";
                //oReader.ClearTagList();   //而若放此處很難偵聽到，因已清除
                tagList = oReader.TagList.Trim();
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
            }
            return lstRfidTag;
        }
        /// <summary>清除 TagList 數據</summary>
        public void ClearTagList()
        {
            if (oReader != null && oReader.IsConnected) { oReader.ClearTagList(); }
        }
        /// <summary>增加綁定方法 </summary>
        public void AddMessageReceived(ReceiveMsg oReceiveMsg)
        {
            if (oReader != null && oReader.IsConnected)
                oReader.MessageReceived += new CBaseReader.MessageReceivedEventHandler(oReceiveMsg);
        }
        /// <summary>移除綁定方法 </summary>
        public void RemoveMessageReceived(ReceiveMsg oReceiveMsg)
        {
            if (oReader != null && oReader.IsConnected)
                oReader.MessageReceived -= new CBaseReader.MessageReceivedEventHandler(oReceiveMsg);
        }
        /// <summary>寫入Tag命令</summary>
        public string ProgTagBySingle(int iBank, string progCode)
        {
            switch (iBank)
            {
                case 1:
                    oReader.ProgDataUnit = "WORD";
                    progCode = oReader.ProgramEPC(FormatStr(progCode));
                    return FormatStr(progCode);
                default:
                    return "";
            }
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
            IReaderInfo[] rs;
            oReadMonitor = new clsReaderMonitor();
            if (String.IsNullOrEmpty(oReadMonitor.CheckComPorts()))
            {
                iPort = oReadMonitor.GetReaderListOnSerial(out rs);
                if (iPort > 0)
                {
                    _readerPortNm = rs[0].IPAddress;
                    return _readerPortNm;
                }
            }
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
            if (oReader == null || !oReader.IsConnected)
            {
                if (String.IsNullOrEmpty(_readerPortNm))
                    _readerPortNm = SearchSerialPort();
                if (String.IsNullOrEmpty(_readerPortNm))
                    return false;
                oReader.SerialPort = _readerPortNm;
                oReader.InitOnCom();
                oReader.Connect();
                if (oReader == null || !oReader.IsConnected)
                {
                    _readerPortNm = null;
                    return false;
                }
            }
            return true;
        }

        private bool ConnectBySocket()
        {
            return false;
        }

        private eG2Bank GetTagBankByIdx(int iBank)
        {
            switch (iBank)
            {
                case 1:
                    return eG2Bank.EPC;
                case 2:
                    return eG2Bank.RESERVED;
                case 3:
                    return eG2Bank.TID;
                case 4:
                    return eG2Bank.USER;
                default:
                    return eG2Bank.EPC;
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
}
