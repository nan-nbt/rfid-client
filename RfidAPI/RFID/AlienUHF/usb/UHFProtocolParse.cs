using BLEDeviceAPI.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UHFAPP;
 

namespace BLEDeviceAPI
{
    public class UHFProtocolParse : IUHFProtocolParse
    {
        internal byte[] tagBuff = new byte[1024];
        internal byte[] GetTAGData = null;
        internal byte[] FlashSendData = null;
        internal bool isDebug = false;
        internal static UHFProtocolParse protocolParse = null;
        internal static object objLock = new object();

        internal UHFProtocolParse()
        {

        }
        public static UHFProtocolParse GetInstance()
        {
            if (protocolParse == null)
            {
                lock (objLock)
                {
                    if (protocolParse == null)
                        protocolParse = new UHFProtocolParse();
                }
            }
            return protocolParse;
        }



        #region 盘点标签
        public bool parseStartInventoryTagData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取单次盘点标签的发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getInventorySingleTagSendData()
        {
            string UHFInventorySingle_SendData = "A5 5A 00 0A 80 00 64 EE 0D 0A";
            return DataConvert.HexStringToByteArray(UHFInventorySingle_SendData);
        }
        /// <summary>
        /// 获取开始盘点的发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getStartInventoryTagSendData()
        {
            byte[] outData = new byte[50];
            int len = UsbNative.UHFInventory_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        /// <summary>
        /// 获取停止盘点标签的发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getStopInventorySendData()
        {
            string StopInventory_SendData = "A5 5A 00 08 8C 84 0D 0A";
            return DataConvert.HexStringToByteArray(StopInventory_SendData);
        }
        /// <summary>
        /// 获取读取标签的发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getReadTagSendData()
        {
            byte[] send = new byte[9];
            send[0] = 0xA5;
            send[1] = 0x5A;
            send[2] = 0x00;
            send[3] = 0x08;
            send[4] = 0xEB;
            send[5] = 0xE3;
            send[6] = 0x0D;
            send[7] = 0x0A;

            return send;
            /*
            if (GetTAGData == null)
            {
                byte[] outData = new byte[50];
                int len =DeviceAPI.UHFGetTagsData_SendData(outData);
                GetTAGData = Utils.CopyArray(outData, len);
            }
            return GetTAGData;
             */
        }
        /// <summary>
        /// 解析单次盘点标签返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public UHFTAGInfo parseInventorySingleTagData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            byte[] outUII = new byte[512];
            byte[] outUIILen = new byte[1];

            if(isDebug) PrintLog("单次盘点解析之前的数据:" + DataConvert.ByteArrayToHexString(inData, inData.Length));
            if (UsbNative.UHFInventorySingle_RecvData(inData, inData.Length, outUIILen, outUII) == 0)
            {
                return parserUhfTagBuffSingle(Utils.CopyArray(outUII, outUIILen[0]), false);
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// 解析盘点标签返回的数据，网口通讯
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public UHFTAGInfo parseInventoryTagDataNetwork(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            byte[] outUII = new byte[512];
            byte[] outUIILen = new byte[1];

            if (isDebug) PrintLog("盘点标签获取的原始数据 tagsBuff=" + DataConvert.ByteArrayToHexString(inData));

            if (UsbNative.UHF_TCP_TagsDataParse_RecvData(inData, inData.Length, outUIILen, outUII) == 0)
            {
                if (outUIILen[0] > 3)
                {
                    return parserUhfTagBuffSingle(Utils.CopyArray(outUII, outUIILen[0]),false);
                }
            }
            return null;
        }
        /// <summary>
        /// 解析盘点标签返回的数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<UHFTAGInfo> parseInventoryTagData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            byte[] outUII = new byte[512];
            byte[] outUIILen = new byte[1];

            if (isDebug) PrintLog("盘点标签获取的原始数据 tagsBuff=" + DataConvert.ByteArrayToHexString(inData));

           if (UsbNative.UHFGetTagsData_RecvData(inData, inData.Length, outUIILen, outUII) == 0)
            {
              if (outUIILen[0] > 3)
                {
                    return parserUhfTagBuff(Utils.CopyArray(outUII, outUIILen[0]));
                }
            }
            return null;
        }
        /// <summary>
        /// 停止盘点标签发送在数据
        /// </summary> 
        /// <returns></returns>
        public bool parseStopInventoryData(byte[] inData)
        {
            if (UsbNative.UHFStopInventory_RecvData(inData, inData.Length) == 0)
            {
                return true;
            }
            return false;
        }


        private UHFTAGInfo parserUhfTagBuffSingle(byte[] tagsBuff,bool isContainsAnt)
        {
            UHFTAGInfo info = new UHFTAGInfo();
            int len = tagsBuff.Length;
            //-------获取pc值------
            byte[] pcBuff = Utils.CopyArray(tagsBuff, 0, 2);// Arrays.copyOfRange(tagsBuff, 0, 2);
            int pc = (pcBuff[0] & 0xFF) | ((pcBuff[1] & 0xFF) << 8); 
            string strPc = DataConvert.ByteArrayToHexString(pcBuff, pcBuff.Length);
            info.Pc = strPc; //  info.setPc(StringUtility.bytes2HexString2(pcBuff, pcBuff.length));
            //-----获取epc---------
            int epclen = (pcBuff[0] >> 3) * 2;
            byte[] epc = Utils.CopyArray(tagsBuff, 2, epclen); //byte[] uii = Arrays.copyOfRange(tagsBuff, 0, 2 + epclen);
            string strEPC = DataConvert.ByteArrayToHexString(epc, epc.Length);// String strUii = StringUtility.bytes2HexString2(uii, uii.length);
            info.Epc = strEPC; //(convertUiiToEPC(strUii));
            //-----------------------
            int uiiLen = epclen + 2;
            byte[] rssi = null;
            byte[] ant_data = null;
            string strAnt = "";
            if (len >= uiiLen + 12)
            {
                //-----获取tid---------
                int tidStart = uiiLen;
                int tidEnd = tidStart + 12;
                byte[] tidBuff = Utils.CopyArray(tagsBuff, tidStart, 12);// Arrays.copyOfRange(tagsBuff, tidStart, tidEnd);
                info.Tid = DataConvert.ByteArrayToHexString(tidBuff, tidBuff.Length); //setTid(StringUtility.bytes2HexString2(tidBuff, tidBuff.length));

                if (len - 3 > tidEnd)
                {
                    //-----获取user---------
                    int userStart = tidEnd;
                    int userEnd = len - 2;
                    int userLen = userEnd - userStart;
                    byte[] userBuff = Utils.CopyArray(tagsBuff, userStart, userLen);// Arrays.copyOfRange(tagsBuff, userStart, userEnd);
                    info.User = DataConvert.ByteArrayToHexString(userBuff, userBuff.Length);// info.setUser(StringUtility.bytes2HexString2(userBuff, userBuff.length));
                    if (len >= userEnd + 2)
                    {
                        int rssiStart = userEnd;
                        int rssiEnd = userEnd + 2;
                        rssi = Utils.CopyArray(tagsBuff, rssiStart, 2); //Arrays.copyOfRange(tagsBuff, rssiStart, rssiEnd);
                       if(isContainsAnt) ant_data = Utils.CopyArray(tagsBuff, rssiEnd, 1); //Arrays.copyOfRange(tagsBuff, rssiEnd, rssiEnd+1); 
                    }
                }
                else
                {
                    if (len >= tidEnd + 2)
                    {
                        int rssiStart = tidEnd;
                        int rssiEnd = tidEnd + 2;
                        rssi = Utils.CopyArray(tagsBuff, rssiStart, 2); // Arrays.copyOfRange(tagsBuff, rssiStart, rssiEnd);
                        if(isContainsAnt)ant_data = Utils.CopyArray(tagsBuff, rssiEnd, 1); // ant_data=Arrays.copyOfRange(tagsBuff, rssiEnd, rssiEnd+1); 
                    }
                }
            }
            else
            {
                if (len >= uiiLen + 2)
                {
                    int rssiStart = uiiLen;
                    int rssiEnd = 2 + uiiLen;
                    rssi = Utils.CopyArray(tagsBuff, rssiStart, 2);// Arrays.copyOfRange(tagsBuff, rssiStart, rssiEnd);
                   if(isContainsAnt) ant_data = Utils.CopyArray(tagsBuff, rssiEnd, 1);// ant_data=Arrays.copyOfRange(tagsBuff, rssiEnd, rssiEnd+1); 
                }
            }
            //-------rssi-------------
            if (rssi != null)
            {
                string strRssi = DataConvert.ByteArrayToHexString(rssi, rssi.Length);
                int irssi = ((rssi[0] & 0xFF)<<8) | (rssi[1] & 0xFF);//FE 91
                float dBm = (65535 - irssi) / 10f;
                string strdBm = "N/A";
                if (dBm < 200 && dBm > 0)
                {
                    strdBm = "-" + dBm.ToString("f2");
                }
                info.Rssi = strdBm;
            }
            //--------------------
            //-----Ant------------
            if (ant_data != null)
            {
                strAnt = ((int)ant_data[0] & 0xff) + "";
                info.Ant = strAnt;//info.setAnt(strAnt);
            }
            //---------- 
            return info;
        }

        private List<UHFTAGInfo> parserUhfTagBuff(byte[] tagsBuff)
        {
            if (tagsBuff != null)
            {
                List<UHFTAGInfo> list = new List<UHFTAGInfo>();
                int count = tagsBuff[2];// 标签个数 
                PrintLog("parserUhfTagBuff count=" + count);
                int begin = 3;
                for (int k = 0; k < count; k++)
                {
                    int epcLen = tagsBuff[begin];
                    if (tagBuff.Length > begin + epcLen)
                    {
                        UHFTAGInfo tag = new UHFTAGInfo();
                        begin = begin + 1;//epc长度后面是具体数据所以要加1
                        byte[] epcBuff = Utils.CopyArray(tagsBuff, begin, epcLen);
                        tag.Epc = DataConvert.ByteArrayToHexString(epcBuff);
                        list.Add(tag);
                        begin = begin + epcLen;
                        PrintLog("parserUhfTagBuff epc=" + tag.Epc);
                    }
                    else
                    {
                        break;
                    }

                }
                if (list.Count > 0)
                    return list;
            }
            return null;
        }

        /// <summary>
        /// 读取标签数据，同时返回EPC、TID、USER数据 （需要uhf固件支持）
        /// </summary>
        /// <returns></returns>
        public List<UHFTAGInfo> parserUhfTagBuff_EpcTidUser(byte[] tagsBuff)
        {
            if (tagsBuff != null)
            {
                List<UHFTAGInfo> list = new List<UHFTAGInfo>();
                int count = tagsBuff[2];// 标签个数
                int epc_lenIndex = 3;// epc长度索引
                int epc_startIndex = 4; //截取epc数据的起始索引
                int epc_endIndex = 0;//截取epc数据的结束索引
                PrintLog("readTagFromBuffer_EpcTidUser count=" + count);
                for (int k = 0; k < count; k++)
                {
                    epc_startIndex = epc_lenIndex + 1;
                    epc_endIndex = epc_startIndex + (tagsBuff[epc_lenIndex] & 0xFF);//epc的起始索引加长度得到结束索引

                    if (epc_endIndex > tagsBuff.Length)
                        break;
                    else
                    {
                        byte[] epcBuff = Utils.CopyArray(tagsBuff, epc_startIndex, epc_endIndex - epc_startIndex);
                        UHFTAGInfo info = parserUhfTagBuffSingle(epcBuff,false);
                        PrintLog("parserUhfTagBuff epc=" + info.Epc);
                        if (info != null)
                        {
                            list.Add(info);
                        }
                    }
                    epc_lenIndex = epc_endIndex;
                    if (epc_lenIndex >= tagsBuff.Length)
                        break;
                }
                PrintLog("readTagFromBuffer_EpcTidUser end");


                if (list.Count > 0)
                {
                    return list;
                }
            }
            return null;
        }

        #endregion

        #region 获取R2、R6缓存的数据
        /// <summary>
        /// 获取R2R6缓存标签数量的发送指令
        /// </summary>
        /// <returns></returns>
        public byte[] getAllTagNumFromFlashSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFBTGetAllTagNumFromFlash_SendData(outData);
            return Utils.CopyArray(outData, len);

        }
        /// <summary>
        /// 解析R2 R6获取数量返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public int parseTagNumFromFlashData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            return UsbNative.UHFBTGetAllTagNumFromFlash_RecvData(inData, inData.Length);
        }
        /// <summary>
        /// 获取删除flash数据的发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getDeleteAllTagToFlashSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFBTDeleteAllTagToFlash_SendData(outData);
            return Utils.CopyArray(outData, len);
        }

        /// <summary>
        /// 解析删除flash返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public bool parseDeleteAllTagToFlashData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            int result = UsbNative.UHFBTDeleteAllTagToFlash_RecvData(inData, inData.Length);

            if (result == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 解析读取flash 缓存返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public List<UHFTAGInfo> parseTagDataFromFlashData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            Array.Clear(tagBuff, 0, tagBuff.Length);
            int leng = UsbNative.UHFBTGetTagDataFromFlash_RecvData(inData, inData.Length, tagBuff);
            if (leng <= 0)
            {
                return null;
            }

            List<UHFTAGInfo> list = new List<UHFTAGInfo>();
            int tagCount = tagBuff[0] & 0xff;//当前这次上传的标签数量

            int start = 0;
            int end = 1;
            for (int k = 0; k < tagCount; k++)
            {
                UHFTAGInfo info = new UHFTAGInfo();
                int epcLen = tagBuff[end];
                start = end + 1;
                end = start + epcLen;
                byte[] epcBuff = new byte[epcLen]; // Array..copyOfRange(tagBuff, start, end);
                Buffer.BlockCopy(tagBuff, start, epcBuff, 0, epcBuff.Length);
                info.Epc = DataConvert.ByteArrayToHexString(epcBuff);
                list.Add(info);
            }
            return list;
        }
        /// <summary>
        /// 获取flash缓存的标签在发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getTagDataFromFlashSendData()
        {
            if (FlashSendData == null)
            {
                byte[] outData = new byte[100];
                int len = UsbNative.UHFBTGetTagDataFromFlash_SendData(outData);
                FlashSendData = Utils.CopyArray(outData, len);
            }
            return FlashSendData;
        }
        #endregion

        #region 扫描
        public byte[] getScanBarcodeSendData()
        {
            string send = "A5 5A 00 09 E4 02 EF 0D 0A";
            return DataConvert.HexStringToByteArray(send);
        }
        public byte[] parseBarcodeData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            byte[] outData = new byte[512];
            byte[] outDataLen = new byte[1];

            if (UsbNative.Open2DScan_RecvData(inData, inData.Length, outData, outDataLen) == 0)
            {
                if (outDataLen[0] >0)
                {
                    return Utils.CopyArray(outData, outDataLen[0]);
                }
            }
            return null;
        }
        #endregion

        #region 功率 
        /// <summary>
        /// 获取设置功率的发送数据
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public byte[] setPowerSendData(int power,bool save)
        {
            if (power <1 || power>30)
            {
                throw new ArgumentException();
            }
            byte[] outData = new byte[100];
            int saveFlag = (save ? 1 : 0);
            int len = UsbNative.UHFSetPower_SendData((byte)saveFlag,(byte)power,outData);
            return Utils.CopyArray(outData, len);

        }
        /// <summary>
        /// 解析设置功率返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public bool parseSetPowerData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            if (UsbNative.UHFSetPower_RecvData(inData, inData.Length) == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取当前功率的发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getPowerSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetPower_SendData(outData);
            return Utils.CopyArray(outData, len);

        }
        /// <summary>
        /// 解析获取功率返回的数据
        /// </summary>
        /// <param name="iData"></param>
        /// <returns></returns>
        public int parseGetPowerData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            byte[] outData = new byte[512];
            if (UsbNative.UHFGetPower_RecvData(inData, inData.Length, outData) == 0)
            {
                return outData[0];
            }
            return -1;
        }
        #endregion

        #region 蜂鸣器

 
 
 

        /// <summary>
        /// 解析设置蜂鸣器返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public bool parseBeepData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            int result = UsbNative.UHFSetReaderBeep_RecvData(inData, inData.Length);
            if  (result == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///  设置蜂鸣器的发送数据
        /// </summary>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public byte[] getBeepSendData(bool isOpen)
        {


            byte[] outData = new byte[100];
            int mode = isOpen ? 1 : 0;
            int len = UsbNative.UHFSetReaderBeep_SendData((byte)mode, outData);
            return Utils.CopyArray(outData, len);

           // return getBeepSendData1(isOpen);
        }
        public byte[] getBeepSendData1(bool isOpen)
        {
            if (isOpen)
            {
 
              return  DataConvert.HexStringToByteArray("A55A000AE40301EC0D0A");
            }
            else
            {

                return DataConvert.HexStringToByteArray("A55A000AE40300ED0D0A");
            }
        }


        /// <summary>
        /// 获取蜂鸣器状态发送的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public byte[] getBeepStatusSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetReaderBeep_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        /// <summary>
        /// 解析获取蜂鸣器返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns>  1:打开  0：关闭 </returns>
        public int parseGetBeepStatusData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException();
            }
            int result = UsbNative.UHFGetReaderBeep_RecvData(inData, inData.Length);
            return result;
        }
        #endregion

        #region 读写

        public byte[] getWriteSendData(string accessPwd, int filterBank, int filterPtr, int filterCnt, string filterData, int bank, int ptr, int cnt, string writeData)
        {
            byte[] uAccessPwd = DataConvert.HexStringToByteArray(accessPwd);
            byte[] ufilterData = null;

            if (uAccessPwd == null || uAccessPwd.Length != 4)
            {
                throw new ArgumentException("parameter error 1!");
            }
            if (bank < 0 || ptr < 0 || cnt < 0)
            {
                throw new ArgumentException("parameter error 2!");
            }

            byte[] uDatabuf = DataConvert.HexStringToByteArray(writeData);
            int tempLen = cnt / 8 + (cnt % 8 == 0 ? 0 : 1);
            if (uDatabuf.Length < tempLen)
            {
                throw new ArgumentException("parameter error 3!");
            }

            if (filterCnt == 0)
            {
                filterData = "00";
                filterBank = 1;
                filterPtr = 0;
                ufilterData = new byte[1];
            }
            else
            {
                if (filterBank < 0 || filterPtr < 0 || filterCnt < 0)
                {
                    throw new ArgumentException("parameter error 4!");
                }

                ufilterData = DataConvert.HexStringToByteArray(filterData);
                if (ufilterData == null || ufilterData.Length == 0)
                {
                    throw new ArgumentException("writeData cannot be empty!");
                }
                 tempLen = filterCnt / 8 + (filterCnt % 8 == 0 ? 0 : 1);
                if (ufilterData.Length < tempLen)
                {
                    throw new ArgumentException("parameter error 5!");
                }

            }
            byte[] outData = new byte[1024];
            int len = UsbNative.UHFWriteData_SendData(uAccessPwd, (byte)filterBank,   filterPtr, filterCnt, ufilterData, (byte)bank, ptr, (byte)cnt,  uDatabuf,  outData);
            return Utils.CopyArray(outData, len);
        }

        public byte[] getReadSendData(string accessPwd, int filterBank, int filterPtr, int filterCnt, string filterData, int bank, int ptr, int cnt)
        {
            byte[] uAccessPwd = DataConvert.HexStringToByteArray(accessPwd);
            byte[] ufilterData = null;

            if (uAccessPwd == null || uAccessPwd.Length != 4)
            {
                throw new ArgumentException("parameter error 1!");
            }
            if (bank < 0 || ptr < 0 || cnt < 0)
            {
                throw new ArgumentException("parameter error 2!");
            }
            if (filterCnt == 0)
            {
                filterData = "00";
                filterBank = 1;
                filterPtr = 0;
                ufilterData = new byte[1];
            }
            else
            {
                if (filterBank < 0 || filterPtr < 0 || filterCnt < 0)
                {
                    throw new ArgumentException("parameter error 3!");
                }
                ufilterData = DataConvert.HexStringToByteArray(filterData);
                if (ufilterData == null || ufilterData.Length == 0)
                {
                    throw new ArgumentException("writeData cannot be empty!");
                }

                int tempLen = filterCnt / 8 + (filterCnt % 8 == 0 ? 0 : 1);
                if (ufilterData.Length < tempLen)
                {
                    throw new ArgumentException("parameter error 4!");
                }

            }
            byte[] outData = new byte[1024];
            int len = UsbNative.UHFReadData_SendData(uAccessPwd, (byte)filterBank, filterPtr, filterCnt, ufilterData, bank, ptr, cnt, outData);
            return Utils.CopyArray(outData, len);
        }

     
        public bool parseWriteData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
 
            int result = UsbNative.UHFWriteData_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public string parseReadData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            byte[] data = new byte[inData.Length];
            for (int k = 0; k < data.Length; k++)
            {
                data[k]=inData[k];
            }

            byte[] outData = new byte[4096];
            int outLen = 0;
            int result = UsbNative.UHFReadData_RecvData(data, data.Length, outData, ref outLen);
            if (result == 0)
            {
                int len = outLen;

                return DataConvert.ByteArrayToHexString(Utils.CopyArray(outData, len));
            }
            return null;
 
        }

        #endregion

        #region 锁、销毁
        public string generateLockCode(List<int> lockBank, int lockMode)
        {
            throw new NotImplementedException();
        }
        public byte[] getLockSendData(string accessPwd, int filterBank, int filterPtr, int filterCnt, string filterData, string lockCode)
        {
            byte[] uAccessPwd = DataConvert.HexStringToByteArray(accessPwd);
            byte[] ufilterData = null;
            byte[] bCode = DataConvert.HexStringToByteArray(lockCode);

            if (uAccessPwd == null || uAccessPwd.Length != 4)
            {
                throw new ArgumentException("parameter error to pwd!");
            }
            if (bCode == null || bCode.Length != 3)
            {
                throw new ArgumentException("parameter error to lockCode!");
            }

            if (filterCnt == 0)
            {
                filterData = "00";
                filterBank = 1;
                filterPtr = 0;
                ufilterData = new byte[1];
            }
            else
            {
                if (filterBank < 0 || filterPtr < 0 || filterCnt < 0)
                {
                    throw new ArgumentException("parameter error 3!");
                }
                ufilterData = DataConvert.HexStringToByteArray(filterData);
                if (ufilterData == null || ufilterData.Length == 0)
                {
                    throw new ArgumentException("writeData cannot be empty!");
                }

                int tempLen = filterCnt / 8 + (filterCnt % 8 == 0 ? 0 : 1);
                if (ufilterData.Length < tempLen)
                {
                    throw new ArgumentException("parameter error 4!");
                }

            }
            byte[] outData = new byte[100];
            int len = UsbNative.UHFLockTag_SendData(uAccessPwd, (byte)filterBank, filterPtr, filterCnt, ufilterData, bCode, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseLockData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFLockTag_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getKillSendData(string accessPwd, int filterBank, int filterPtr, int filterCnt, string filterData)
        {
            byte[] uAccessPwd = DataConvert.HexStringToByteArray(accessPwd);
            byte[] ufilterData = null;

            if (uAccessPwd == null || uAccessPwd.Length != 4)
            {
                throw new ArgumentException("parameter error 1!");
            }
            if (filterCnt == 0)
            {
                filterData = "00";
                filterBank = 1;
                filterPtr = 0;
                ufilterData = new byte[1];
            }
            else
            {
                if (filterBank < 0 || filterPtr < 0 || filterCnt < 0)
                {
                    throw new ArgumentException("parameter error 3!");
                }
                ufilterData = DataConvert.HexStringToByteArray(filterData);
                if (ufilterData == null || ufilterData.Length == 0)
                {
                    throw new ArgumentException("writeData cannot be empty!");
                }

                int tempLen = filterCnt / 8 + (filterCnt % 8 == 0 ? 0 : 1);
                if (ufilterData.Length < tempLen)
                {
                    throw new ArgumentException("parameter error 4!");
                }

            }
            byte[] outData = new byte[100];
            int len = UsbNative.UHFKillTag_SendData(uAccessPwd, (byte)filterBank, filterPtr, filterCnt, ufilterData, outData);
            return Utils.CopyArray(outData, len);
        }

        public bool parseKillData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFKillTag_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }
        #endregion
 
        #region 电量、温度、版本
        public int parseBatteryData(byte[] inData)
        {
            throw new NotImplementedException();
        }
        public byte[] getBatterySendData()
        {
            throw new NotImplementedException();
        }
        public int parseTemperatureData(byte[] inData)
        {
            byte[] temperature = new byte[5];
            int reuslt = UsbNative.UHFGetTemperature_RecvData(inData, inData.Length, temperature);
            if (reuslt == 0)
            {
               return temperature[0];
            }
            return -1;
        }
        public byte[] getTemperatureSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetTemperature_SendData(outData);
            return Utils.CopyArray(outData, len);
        }

        /// <summary>
        /// 硬件版本
        /// </summary>
        /// <returns></returns>
        public byte[] getHardwareVersionSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetHardwareVersion_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public string parseHardwareVersionData(byte[] inData)
        {
            byte[] version = new byte[100];
            int reuslt = UsbNative.UHFGetHardwareVersion_RecvData(inData, inData.Length, version);
            if (reuslt == 0)
            {
                int len = version[0];
                byte[] versionTemp = new byte[len];
                Array.Copy(version, 1, versionTemp, 0, len);
                return System.Text.Encoding.ASCII.GetString(versionTemp);// DataConvert.ByteArrayToHexString(versionTemp);
            }
            return "";
        }

        /// <summary>
        /// 软件版本
        /// </summary>
        /// <returns></returns>
        public byte[] getSoftwareVersionSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetSoftwareVersion_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public string parseSoftwareVersionData(byte[] inData) {

            byte[] version = new byte[100];
            int reuslt = UsbNative.UHFGetSoftwareVersion_RecvData(inData, inData.Length, version);
            if (reuslt == 0)
            {
                int len = version[0];
                byte[] versionTemp = new byte[len];
                Array.Copy(version, 1, versionTemp, 0, len);
                return System.Text.Encoding.ASCII.GetString(versionTemp); 
            }
            return "";

        }

        /// <summary>
        /// 设备id
        /// </summary>
        /// <returns></returns>
        public byte[] getDeviceIDSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetDeviceID_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public int parseDeviceIDData(byte[] inData)
        {
            byte[] id = new byte[1];
            int reuslt = UsbNative.UHFGetSoftwareVersion_RecvData(inData, inData.Length, id);
            if (reuslt == 0)
            {
                return id[0];
            }
            return -1;
        }

        #endregion 

        #region 协议
        public byte[] getProtocolSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetProtocolType_SendData(outData);
            return Utils.CopyArray(outData, len);
        }

        public byte[] setProtocolSendData(int protocol)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetProtocolType_SendData((byte)protocol,outData);
            return Utils.CopyArray(outData, len);
        }

        public bool parseSetProtocolData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFSetProtocolType_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public int parseGetProtocolData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData=new byte[100];
            int result = UsbNative.UHFGetProtocolType_RecvData(inData, inData.Length, outData);
            if (result == 0)
            {
                return outData[0];
            }
            return -1;
        }
        #endregion

        #region 频段设置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveflag">1:表示掉电保存，0:表示掉电不保存</param>
        /// <param name="freMode"></param>
        /// <returns></returns>
        public byte[] setFrequencyModeSendData(byte saveflag, int freMode)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetRegion_SendData(saveflag, (byte)freMode, outData);
            return Utils.CopyArray(outData, len);
        }
        /// <summary>
        /// 解析设置频段模式返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public bool parseSetFrequencyModeData(byte[] inData) {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFSetRegion_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false ;
        }
        /// <summary>
        /// 获取当前频段模式的发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getFrequencyModeSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetRegion_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        /// <summary>
        /// 解析获取当前频段模式的发送数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public int parseGetFrequencyModeData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] region = new byte[5];
            int result = UsbNative.UHFGetRegion_RecvData(inData, inData.Length, region);
            if (result == 0)
            {
                return region[0];
            }
            return -1;
        }

        /**********************************************************************************************************
        * 功能：设置跳频
        * 输入：nums -- 跳频个数
        * 输入：Freqbuf--频点数组（整型） ，如920125，921250.....
        *********************************************************************************************************/
        public byte[] setJumpFrequencySendData(byte nums, int[] Freqbuf)
        {

            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetJumpFrequency_SendData(nums,  Freqbuf, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetJumpFrequencyData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetJumpFrequency_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getJumpFrequencySendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetJumpFrequency_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        /**********************************************************************************************************
            * 功能：获取跳频
            * 输出：Freqbuf[0]--频点个数， Freqbuf[1]..[x]--频点数组（整型）
         *********************************************************************************************************/
        public int[] parseGetJumpFrequencyData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] Freqbuf=new byte[100];
            int result = UsbNative.UHFGetJumpFrequency_RecvData(inData, inData.Length, Freqbuf);
            if (result == 0)
            {
                int len = 1;// Freqbuf[0];
                int[] freqData = new int[len];
                Array.Copy(Freqbuf, 1, freqData, 0, len);
                if (freqData[0].ToString().Length < 3)
                {
                    return null;
                }
                else
                {
                    return freqData;
                }
            }
            return null;
        }
 

        #endregion

        #region 盘点过滤
        public byte[] setFilterSendData(byte saveflag, byte bank, int startaddr, int datalen, byte[] databuf) {
            byte[] outData = new byte[100];
            int len = UsbNative. UHFSetFilter_SendData(  saveflag,   bank,   startaddr,   datalen,  databuf,  outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseFilterData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetFilter_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }
        #endregion
    
        #region Gen2
        public byte[] setGen2SendData(byte Target, byte Action, byte T, byte Q, byte StartQ, byte MinQ, byte MaxQ, byte D, byte C,
            byte P, byte Sel, byte Session, byte G, byte LF)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetGen2_SendData(Target, Action, T, Q,
                              StartQ, MinQ,
                              MaxQ, D, C, P,
                              Sel, Session, G, LF, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetGen2Data(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetGen2_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[]  getGen2SendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetGen2_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseGetGen2Data(byte[] inData, int inLen, ref byte Target, ref byte Action, ref byte T, ref byte Q,
              ref byte StartQ, ref byte MinQ,
              ref byte MaxQ, ref byte D, ref byte Coding, ref byte P,
              ref byte Sel, ref byte Session, ref byte G, ref byte LF)
        {

            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFGetGen2_RecvData(inData, inLen, ref Target, ref Action, ref T, ref Q,
                ref  StartQ, ref  MinQ,
                ref  MaxQ, ref  D, ref  Coding, ref  P,
                ref  Sel, ref  Session, ref  G, ref  LF);
            if (result == 0)
            {
                return true;
            }
            return false;
        }


        #endregion

        #region 天线
        public byte[] setANTSendData(byte saveflag, byte[] buf)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetANT_SendData(saveflag, buf, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetANTDData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetANT_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getANTSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetANT_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public byte[] parseGetANTDData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetANT_RecvData(inData, inData.Length, outData);
            if (len == 0)
            {
                return Utils.CopyArray(outData, 2);
            }
            return null;
        }

        public byte[] getANTWorkTimeSendData(byte antnum)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetANTWorkTime_SendData(antnum,outData);
            return Utils.CopyArray(outData, len);
        }
        public int parseGetANTWorkTimeData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int[] time=new int[1];
            int result = UsbNative.UHFGetANTWorkTime_RecvData(inData, inData.Length, time);
            if (result == 0)
            {
                return time[0];
            }
            return -1;
        }
        public byte[] setANTWorkTimeSendData(byte antnum, byte saveflag, int WorkTime)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetANTWorkTime_SendData(antnum, saveflag, WorkTime, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetANTWorkTimeData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetANTWorkTime_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region 链路
        public byte[] setRFLinkSendData(byte saveflag, byte mode)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetRFLink_SendData(saveflag, mode, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetRFLinkData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int len = UsbNative.UHFSetRFLink_RecvData(inData, inData.Length);
            if (len == 0)
            {
                return true;
            }
            return false;
        }
        public byte[] getRFLinkSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetRFLink_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        
        public int parseGetRFLinkData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFGetRFLink_RecvData(inData, inData.Length, outData);
            if (result == 0)
            {
                return outData[0];
            }
            return -1;
        }

        #endregion

        #region IP

        //--------设置IP-------------

        public byte[] setIpSendData(byte[] ipbuf, byte[] postbuff)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetIp_SendData(ipbuf,postbuff,outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetIpData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFSetIp_RecvData(inData, inData.Length);
            if (result==0)
            {
                return true;
            }
            return false;
        }

        //-------获取ip---------
        public byte[] getIpSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetIp_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseGetIpData(byte[] inData, byte[] ipbuf, byte[] postbuff)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
 
            int result = UsbNative.UHFGetIp_RecvData(inData, inData.Length, ipbuf, postbuff);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        //--------设置目标IP-------------

        public byte[] setDestIpSendData(byte[] ipbuf, byte[] postbuff)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetDestIp_SendData(ipbuf, postbuff, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetDestIpData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFSetDestIp_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        //--------获取目标IP-------------
        public byte[] getDestIpSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetDestIp_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseGetDestIpData(byte[] inData, byte[] ipbuf, byte[] postbuff)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFGetDestIp_RecvData(inData, inData.Length, ipbuf, postbuff);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        
 
        #endregion

    


        #region Tagfocus 功能
        public byte[] setTagfocusSendData(byte flag)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetTagfocus_SendData(flag,outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetTagfocusData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFSetTagfocus_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getTagfocusSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetTagfocus_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
         /**********************************************************************************************************
          * 功能：获取Tagfocus功能
          * 输出：flag -- 1:开启， 0：关闭
          *********************************************************************************************************/
        public int parseGetTagfocusData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFGetTagfocus_RecvData(inData, inData.Length, outData);
            if (result == 0)
            {
                return outData[0];
            }
            return -1;
        }
        #endregion
 
        #region FastID

        public byte[] setFastIDSendData(byte flag)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetFastID_SendData(flag, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetFastIDData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetFastID_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getFastIDSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetFastID_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public int parseGetFastIDData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFGetFastID_RecvData(inData, inData.Length, outData);
            if (result == 0)
            {
                return outData[0];
            }
            return -1;
        }

        #endregion


        #region cw连续波

        public byte[] setCWSendData(byte flag)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetCW_SendData(flag,outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetCWData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetCW_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getCWSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetCW_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public int parseGetCWData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] outData = new byte[100];
            int result = UsbNative.UHFGetCW_RecvData(inData, inData.Length, outData);
            if (result == 0)
            {
                return outData[0];
            }
            return -1;
        }

        #endregion

        #region  工作模式 (命令工作模式、自动模式)

        public byte[] setSetWorkModeSendData(byte mode)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetWorkMode_SendData(mode, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetWorkModeData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            int result = UsbNative.UHFSetWorkMode_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getWorkModeSendData() {

            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetWorkMode_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public int parseGetWorkModeData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] mode=new byte[1];
            int result = UsbNative.UHFGetWorkMode_RecvData(inData, inData.Length, mode);
            if (result == 0)
            {
                return mode[0];
            }
            return -1;
        }

        #endregion

        #region 复位

        public byte[] setSoftResetSendData() {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetSoftReset_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetSoftResetData(byte[] inData) {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
 
            int result = UsbNative.UHFSetSoftReset_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region GPIO 、继电器
        public byte[] setIOControlSendData(byte output1, byte output2, byte outStatus) {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetIOControl_SendData(output1,output2 ,outStatus,outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetIOControlData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFSetIOControl_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getIOControlSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetIOControl_SendData(outData);
            return Utils.CopyArray(outData, len);
        }
        public byte[] UHFGetIOControl_RecvData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            byte[] outData = new byte[100];
            int result = UsbNative.UHFGetIOControl_RecvData(inData, inData.Length,outData);
            if (result == 0)
            {
                if (outData != null && outData.Length >= 2)
                {
                    return new byte[]{ outData[0], outData[1]};
                }
                return null;
            }
            return null;
        }
        #endregion

 
        #region BlockPermalock
        public byte[] getBlockPermalockSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,
                                    byte ReadLock, byte uBank, int uPtr, byte uRange, byte[] uMaskbuf) {

                byte[] outData = new byte[100];
                int len = UsbNative.UHFBlockPermalock_SendData(uAccessPwd,FilterBank,FilterStartaddr,FilterLen,FilterData,ReadLock,uBank,uPtr,uRange,uMaskbuf,outData);
                return Utils.CopyArray(outData, len);
        }
        public bool parseBlockPermalocData(byte uRange, byte[] inData, byte[] uMaskbuf)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFBlockPermalock_RecvData(uRange, inData, inData.Length, uMaskbuf);
            if (result == 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 擦除
        public byte[] blockWriteDataSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, int uCnt, byte[] uWriteDatabuf)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFBlockWriteData_SendData(uAccessPwd,FilterBank,FilterStartaddr, FilterLen,FilterData, uBank, uPtr, uCnt,uWriteDatabuf,outData); 
            return Utils.CopyArray(outData, len);
        }

        public bool parseBlockWriteData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFBlockWriteData_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] blockEraseDataSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, byte uCnt) {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFBlockEraseData_SendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseBlockEraseData(byte[] inData) {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFBlockEraseData_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 国军标

        public byte[] getGBTagLockSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte memory, byte config, byte action)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGBTagLock_SendData(uAccessPwd,FilterBank,FilterStartaddr,FilterLen,FilterData,memory,config,action,outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseGBTagLockData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFGBTagLock_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region QT

        public byte[] setQTSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte QTData)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetQT_SendData( uAccessPwd,  FilterBank,  FilterStartaddr,  FilterLen,  FilterData, QTData,  outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetQTData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFSetQT_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }


        public byte[] getQTSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetQT_SendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, outData);
            return Utils.CopyArray(outData, len);
        }
        public int parseGetQTData(byte[] inData)
        {
             if (inData == null || inData.Length == 0)
             {
                throw new ArgumentException("inData is null!");
             }
             byte[] QTData=new byte[100];
             int result = UsbNative.UHFGetQT_RecvData(inData, inData.Length, QTData);
            if (result == 0)
            {
                return (int)QTData[0];
            }
            return  -1 ;
        }


        #endregion

        #region EPC+TID+USER 模式

        public byte[] setEpcTidUserModeSendData(byte saveFlag, byte memory, byte userAddress, byte useLen)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFSetEPCTIDMode_SendData(  saveFlag,  memory,  userAddress,  useLen, outData);
            return Utils.CopyArray(outData, len);
        }
        public bool parseSetEpcTidUserModeyData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFSetEPCTIDMode_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] getEpcTidUserModeSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetEPCTIDMode_SendData(0, 0, outData);
            return Utils.CopyArray(outData, len);
        }

        public bool parseGetEpcTidUserModeyData(byte[] inData, ref int mode, ref byte userAddress, ref byte useLen)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }
            byte[] data = new byte[10];
            int result = UsbNative.UHFGetEPCTIDMode_RecvData(inData, inData.Length, data);
            if (result> 0)
            {
                mode = data[0];
                userAddress = data[1];
                useLen = data[2];
                return true;
            }
            return false;
        }

        #endregion

        internal void PrintLog(string msg)
        {
            if (isDebug)
            {
                Console.WriteLine("\r\n");
       
                Console.WriteLine(DateTime.Now + "  UHFProtocolParse" + "===>" + msg);
            }

        }


        #region IUHFProtocolParse 成员

 

        #endregion


        #region 升级

   
        public byte[] jump2BootSendData(int flag)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFJump2Boot_SendData(flag, outData);
            return Utils.CopyArray(outData, len);
        }

        public bool parseJump2BootData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFJump2Boot_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] startUpdSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFStartUpd_SendData(outData);
            return Utils.CopyArray(outData, len);
        }

        public bool parseStartUpdData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFStartUpd_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] updatingSendData(byte[] data)
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFUpdating_SendData(data, outData);
            return Utils.CopyArray(outData, len);
        }

        public bool parseUpdatingData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFUpdating_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] stopUpdateSendData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFStopUpdate_SendData(outData);
            return Utils.CopyArray(outData, len);
        }

        public bool parseStopUpdateData(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                throw new ArgumentException("inData is null!");
            }

            int result = UsbNative.UHFStopUpdate_RecvData(inData, inData.Length);
            if (result == 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        public byte[] getSTM32VersionData()
        {
            byte[] outData = new byte[100];
            int len = UsbNative.UHFGetSTM32Version_SendData(outData);
            return Utils.CopyArray(outData, len);
        }

        public byte[] parseSTM32VersionData(byte[] inData)
        {
            byte[] version = new byte[100];
            int len = UsbNative.UHFGetSTM32Version_RecvData(inData, inData.Length, version);
            return Utils.CopyArray(version, len);
        }
    
    }
}

 