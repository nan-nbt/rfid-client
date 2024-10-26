using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RFID.Command;
using UHFAPP;
using UHFAPP.interfaces;
using BLEDeviceAPI;
using UHFAPP.Receive;
using System.Threading;
using System.Windows.Forms;

namespace RFID.Reader
{
    public class AlienUhf : IReader
    {
        public int qtySucc = 0;
        private string _readerPortNm;
        public static IUHF uhf = null;
        public static IAutoReceive iAutoReceive = null;
        List<TagStruct> lstRfidTag = new List<TagStruct>();
        string tagList = "", oldTag = "", newTag = "", tagVal = "";
        /// <summary>定义委托</summary>
        public delegate void MessageReceivedEventHandler(string msg);
        /// <summary>此委托类型的事件</summary>
        public event MessageReceivedEventHandler MessageReceived;
        Thread oThread = null;
        /// <summary> 设置输出功率为10（Max=30） </summary>
        public bool SetPower() { return uhf != null ? uhf.SetPower((byte)1, (byte)10) : true; }
        public bool StopGet() { return uhf != null ? uhf.StopGet() : true; }
        /// <summary>自动执行</summary>
        public void AutoRun()
        {
            bool bLoop = false;
            do
            {
                if (MessageReceived != null)
                {
                    UHFTAGInfo oTagO = getTag();
                    if (oTagO != null)
                    {
                        tagVal = "";
                        oldTag = oTagO.Epc;
                        tagVal = "00 00 00 00 00 00 00 00 00 00 00 00";
                        ProgTagBySingle(1, tagVal);
                    }
                    UHFTAGInfo oTagN = getTag();
                    if (oTagN != null)
                        newTag = oTagN.Epc;
                    if (oldTag != "" && newTag != "" && !oldTag.Equals(newTag))
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
            uhf = UHFAPI.getInstance();
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
            if (iAutoReceive != null)
                iAutoReceive.DisConnect();
            iAutoReceive = null;
            if (uhf != null)
            {
                uhf.StopGet();
                bClose = uhf.Close();
                uhf = null;
            }
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
            bool bConn = uhf != null && uhf.Inventory() ? true : false;
            return bConn;
        }
        /// <summary>成功數量</summary>
        public int getQty()
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
                UHFTAGInfo info = getTag();
                tag = info != null ? info.Epc : "";
            }
            return tag;
        }

        /// <summary> 取得Tag信息 </summary>
        private UHFTAGInfo getTag()
        {
            UHFTAGInfo info = uhf.uhfGetReceived();
            if (info == null)
            {
                SpinWait.SpinUntil(() => false, 200);
                info = uhf.uhfGetReceived();
            }
            return info;
        }

        /// <summary>读取Tag</summary>
        private void ReadEPC()
        {
            UHFTAGInfo info = getTag();
            if (info != null)
            {
                tagList = info.Epc;
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
            bool bResult = false;
            string comVal = epcCode.Replace(" ", "");
            byte[] pwd = DataConvert.HexStringToByteArray("00000000");
            byte[] dataBuff = DataConvert.HexStringToByteArray(comVal);
            byte[] filterBuff = null;
            UHFTAGInfo info = getTag();
            if (info != null)
            {
                filterBuff = DataConvert.HexStringToByteArray(info.Epc);
            }
            filterBuff = filterBuff == null ? dataBuff : filterBuff;
            uhf.SetPower((byte)1, (byte)26);    // 写前将输出功率设成26 才能写入成功
            bResult = uhf.WriteData(pwd, (byte)1, 32, 96, filterBuff, (byte)1, 2, (byte)6, dataBuff);
            uhf.SetPower((byte)1, (byte)10);    // 写完后将输出功率复原为预设 10
            SpinWait.SpinUntil(() => false, 200);
            UHFTAGInfo tagInfo = getTag();
            string newVal = tagInfo != null ? tagInfo.Epc : "";
            qtySucc = bResult || comVal.Equals(newVal) ? (qtySucc + 1) : qtySucc;
            bResult = comVal.Equals(newVal) ? true : bResult;
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
            bool result = false;
            if (String.IsNullOrEmpty(_readerPortNm))
                _readerPortNm = SearchSerialPort();
            result = (uhf as UHFAPI).Open(getComPort());
            iAutoReceive = new SerialPortReceive();
            iAutoReceive.Connect();
            return result;
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
