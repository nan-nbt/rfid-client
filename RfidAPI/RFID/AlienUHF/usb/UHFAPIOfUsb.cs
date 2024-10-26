using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLEDeviceAPI;
using System.Threading;
using BLEDeviceAPI.interfaces;

using System.Collections;
using UHFAPP.usb;

using UHFAPP.usb.hid_new;
using HID_SIMPLE.HID;

namespace UHFAPP
{
    public class UHFAPIOfUsb : IUHF
    {
        bool isDebug = false;
        HidUtils hidUtils = null;
        //AHidApi ahid = new AHidApi();
        UsbUHFProtocolParse uhfProtocolParse = new UsbUHFProtocolParse();
        private static UHFAPIOfUsb uhf = null;
        private object objLock = new object();


        // Queue  queueReceiveData = Queue.Synchronized( new Queue(1024));
        //接收的缓存数据
        Queue<byte> queueReceiveData = new Queue<byte>(1024);
        byte receiveLastData = 0; //最后接收到的数据
        bool isCompleteReceive = false; //接收完成

        List<UHFTAGInfo> uhfinfoTemp = new List<UHFTAGInfo>();
        int timeOut = 2000;



        private UHFAPIOfUsb() { }
        public static UHFAPIOfUsb getInstance()
        {
            if (uhf == null)
                uhf = new UHFAPIOfUsb();
            return uhf;
        }
        #region IUHF 成员

        #region uhf协议
        public bool SetProtocol(byte type)
        {
            byte[] sendData = uhfProtocolParse.setProtocolSendData((int)type);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetProtocol 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetProtocolData(receiveData);
            }
        }

        public int GetProtocol()
        {
            byte[] sendData = uhfProtocolParse.getProtocolSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetProtocol 获取返回数据失败!");
                return -1;
            }
            else
            {
                return uhfProtocolParse.parseGetProtocolData(receiveData);
            }
        }
        #endregion

        #region IP
        public bool SetLocalIP(string ip, int port, string mask, string gate)
        {
            if (ip == null || ip == "")
            {
                return false;
            }
            ip = ip.Trim();

            if (!StringUtils.isIP(ip))
            {
                return false;
            }
            byte[] bPort = new byte[2];
            byte[] bIP = new byte[4];

            string hexPort = DataConvert.DecimalToHexString(port);
            bPort = DataConvert.HexStringToByteArray("0000".Substring(0, 4 - hexPort.Length) + hexPort);

            string[] strIp = ip.Split('.');
            for (int k = 0; k < strIp.Length; k++)
            {
                bIP[k] = byte.Parse(strIp[k]);
            }

            byte[] sendData = uhfProtocolParse.setIpSendData(bIP, bPort);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetLocalIP 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetIpData(receiveData);
            }
        }

        public bool GetLocalIP(StringBuilder ip, StringBuilder port, StringBuilder mask, StringBuilder gate)
        {
            byte[] sendData = uhfProtocolParse.getIpSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetLocalIP 获取返回数据失败!");
                return false;
            }
            else
            {
                byte[] sIP = new byte[4];
                byte[] sPort = new byte[2];

                if (uhfProtocolParse.parseGetIpData(receiveData, sIP, sPort))
                {
                    if (ip != null)
                    {
                        ip.Append(sIP[0]);
                        ip.Append(".");
                        ip.Append(sIP[1]);
                        ip.Append(".");
                        ip.Append(sIP[2]);
                        ip.Append(".");
                        ip.Append(sIP[3]);
                    }
                    if (port != null)
                    {
                        string hexPort = DataConvert.ByteArrayToHexString(sPort).Replace(" ", "");
                        int iPort = Convert.ToInt32(hexPort, 16);
                        port.Append(iPort);
                    }

                    return true;
                }
            }
            return false;
        }
        public bool SetDestIP(string ip, int port)
        {

            if (ip == null || ip == "")
            {
                return false;
            }
            ip = ip.Trim();

            if (!StringUtils.isIP(ip))
            {
                return false;
            }
            byte[] bPort = new byte[2];
            byte[] bIP = new byte[4];

            string hexPort = DataConvert.DecimalToHexString(port);
            bPort = DataConvert.HexStringToByteArray("0000".Substring(0, 4 - hexPort.Length) + hexPort);

            string[] strIp = ip.Split('.');
            for (int k = 0; k < strIp.Length; k++)
            {
                bIP[k] = byte.Parse(strIp[k]);
            }

            byte[] sendData = uhfProtocolParse.setDestIpSendData(bIP, bPort);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetLocalIP 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetDestIpData(receiveData);
            }
        }

        public bool GetDestIP(StringBuilder ip, StringBuilder port)
        {
            byte[] sendData = uhfProtocolParse.getDestIpSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetDestIP 获取返回数据失败!");
                return false;
            }
            else
            {
                byte[] sIP = new byte[4];
                byte[] sPort = new byte[2];

                if (uhfProtocolParse.parseGetDestIpData(receiveData, sIP, sPort))
                {
                    if (ip != null)
                    {
                        ip.Append(sIP[0]);
                        ip.Append(".");
                        ip.Append(sIP[1]);
                        ip.Append(".");
                        ip.Append(sIP[2]);
                        ip.Append(".");
                        ip.Append(sIP[3]);
                    }
                    if (port != null)
                    {
                        string hexPort = DataConvert.ByteArrayToHexString(sPort).Replace(" ", "");
                        int iPort = Convert.ToInt32(hexPort, 16);
                        port.Append(iPort);
                    }

                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 打开、关闭
        public bool Open()
        {
            if (hidUtils == null)
            {
                hidUtils = new HidUtils();
                hidUtils.pushReceiveData = Received;
            }
            int vid = 0x2047;
            int pid = 0x301;
            bool result = hidUtils.Initial(vid, pid);
            if (result)
            {
                PrintLog("usb打開成功!");
                return true;
            }
            else
            {
                PrintLog("usb打開失敗!");
            }
            return false;
        }

        public bool Close()
        {
            if (hidUtils == null)
                return false;
            hidUtils.Close();
            PrintLog("usb關閉成功!");
            return true;
        }
        #endregion

        #region Gen2

        public bool SetGen2(byte Target, byte Action, byte T, byte Q, byte StartQ, byte MinQ, byte MaxQ, byte D, byte C, byte P, byte Sel, byte Session, byte G, byte LF)
        {
            byte[] sendData = uhfProtocolParse.setGen2SendData(Target, Action, T, Q, StartQ, MinQ, MaxQ, D, C, P, Sel, Session, G, LF);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetGen2 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetGen2Data(receiveData);

            }
        }

        public bool GetGen2(ref byte Target, ref byte Action, ref byte T, ref byte Q, ref byte StartQ, ref byte MinQ, ref byte MaxQ, ref byte D, ref byte Coding, ref byte P, ref byte Sel, ref byte Session, ref byte G, ref byte LF)
        {
            byte[] sendData = uhfProtocolParse.getGen2SendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetGen2 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseGetGen2Data(receiveData, receiveData.Length, ref   Target, ref   Action, ref   T, ref   Q,
                                                                                              ref   StartQ, ref   MinQ, ref   MaxQ, ref   D,
                                                                                              ref   Coding, ref   P, ref   Sel, ref   Session,
                                                                                              ref   G, ref   LF);
            }
        }

        #endregion

        #region cw 连续波
        public bool SetCW(byte flag)
        {
            byte[] sendData = uhfProtocolParse.setCWSendData(flag);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetCW 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetCWData(receiveData);
            }
        }

        public bool GetCW(ref byte flag)
        {
            byte[] sendData = uhfProtocolParse.getCWSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetCW 获取返回数据失败!");
                return false;
            }
            else
            {
                int data = uhfProtocolParse.parseGetCWData(receiveData);
                if (data != -1)
                {
                    flag = (byte)data;
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region 天线
        public bool SetANT(byte saveflag, byte[] buf)
        {
            byte[] sendData = uhfProtocolParse.setANTSendData(saveflag, buf);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetANT 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetANTDData(receiveData);
            }
        }

        public bool GetANT(byte[] buf)
        {
            byte[] sendData = uhfProtocolParse.getANTSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetANT 获取返回数据失败!");
                return false;
            }
            else
            {
                byte[] data = uhfProtocolParse.parseGetANTDData(receiveData);
                if (data != null && data.Length > 0)
                {
                    buf[0] = data[0];
                    buf[1] = data[1];
                    return true;
                }
                return false;
            }
        }

        public bool SetANTWorkTime(byte antnum, byte saveflag, int WorkTime)
        {
            byte[] sendData = uhfProtocolParse.setANTWorkTimeSendData(antnum, saveflag, WorkTime);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetANTWorkTime 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetANTWorkTimeData(receiveData);
            }
        }

        public bool GetANTWorkTime(byte antnum, ref int WorkTime)
        {
            byte[] sendData = uhfProtocolParse.getANTWorkTimeSendData(antnum);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetANTWorkTime 获取返回数据失败!");
                return false;
            }
            else
            {
                int time = uhfProtocolParse.parseGetANTWorkTimeData(receiveData);
                if (time >= 0)
                {
                    WorkTime = time;
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region 温度
        public string GetTemperature()
        {
            byte[] sendData = uhfProtocolParse.getTemperatureSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetTemperature 获取返回数据失败!");
                return "";
            }
            else
            {
                int data = uhfProtocolParse.parseTemperatureData(receiveData);
                return data + "";
            }
        }

        #endregion

        #region 硬件版本，软件版本，id

        public string GetHardwareVersion()
        {
            byte[] sendData = uhfProtocolParse.getHardwareVersionSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetHardwareVersion 获取返回数据失败!");
                return "";
            }
            else
            {
                return uhfProtocolParse.parseHardwareVersionData(receiveData);
            }
        }

        public string GetSoftwareVersion()
        {
            byte[] sendData = uhfProtocolParse.getSoftwareVersionSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("ReadData 获取返回数据失败!");
                return "";
            }
            else
            {
                string data = uhfProtocolParse.parseSoftwareVersionData(receiveData);
                return data + "";
            }
        }

        public int GetUHFGetDeviceID()
        {
            byte[] sendData = uhfProtocolParse.getDeviceIDSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetUHFGetDeviceID 获取返回数据失败!");
                return -1;
            }
            else
            {
                return uhfProtocolParse.parseDeviceIDData(receiveData);
            }
        }

        #endregion

        #region RFLink、Fast ID、Tagfocus
        public bool SetRFLink(byte saveflag, byte mode)
        {
            byte[] sendData = uhfProtocolParse.setRFLinkSendData(saveflag, mode);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetFastID 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetRFLinkData(receiveData);
            }
        }
        public bool GetRFLink(ref byte uMode)
        {
            byte[] sendData = uhfProtocolParse.getRFLinkSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetFastID 获取返回数据失败!");
                return false;
            }
            else
            {
                int result = uhfProtocolParse.parseGetRFLinkData(receiveData);
                if (result >= 0)
                {
                    uMode = (byte)result;
                    return true;
                }
                return false;
            }
        }

        public bool SetFastID(byte flag)
        {
            byte[] sendData = uhfProtocolParse.setFastIDSendData(flag);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetFastID 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetFastIDData(receiveData);
            }
        }

        public bool GetFastID(ref byte flag)
        {
            byte[] sendData = uhfProtocolParse.getFastIDSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetFastID 获取返回数据失败!");
                return false;
            }
            else
            {
                int f = uhfProtocolParse.parseGetFastIDData(receiveData);
                if (f != -1)
                {
                    flag = (byte)f;
                    return true;
                }
            }
            return false;
        }

        public bool SetTagfocus(byte flag)
        {
            byte[] sendData = uhfProtocolParse.setTagfocusSendData(flag);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetFastID 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetTagfocusData(receiveData);
            }
        }

        public bool GetTagfocus(ref byte flag)
        {
            byte[] sendData = uhfProtocolParse.getTagfocusSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetFastID 获取返回数据失败!");
                return false;
            }
            else
            {
                int f = uhfProtocolParse.parseGetTagfocusData(receiveData);
                if (f != -1)
                {
                    flag = (byte)f;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 复位
        public bool SetSoftReset()
        {
            byte[] sendData = uhfProtocolParse.setSoftResetSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetFastID 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseSetSoftResetData(receiveData);
            }
            return false;
        }
        #endregion

        #region 盘点过滤
        public bool SetFilter(byte saveflag, byte bank, int startaddr, int datalen, byte[] databuf)
        {
            byte[] sendData = uhfProtocolParse.setFilterSendData(saveflag, bank, startaddr, datalen, databuf);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetFilter 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseFilterData(receiveData);
            }
        }
        #endregion



        #region 锁、销毁
        public bool LockTag(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte[] lockbuf)
        {
            byte[] sendData = uhfProtocolParse.getLockSendData(DataConvert.ByteArrayToHexString(uAccessPwd), (int)FilterBank, FilterStartaddr, FilterLen, DataConvert.ByteArrayToHexString(FilterData), DataConvert.ByteArrayToHexString(lockbuf));
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("ReadData 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseLockData(receiveData);
            }
        }

        public bool KillTag(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData)
        {
            byte[] sendData = uhfProtocolParse.getKillSendData(DataConvert.ByteArrayToHexString(uAccessPwd), (int)FilterBank, FilterStartaddr, FilterLen, DataConvert.ByteArrayToHexString(FilterData));
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("ReadData 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseKillData(receiveData);
            }
        }

        public bool GBTagLock(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte memory, byte config, byte action)
        {
            byte[] sendData = uhfProtocolParse.getGBTagLockSendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, memory, config, action);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("ReadData 获取返回数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseGBTagLockData(receiveData);
            }
        }
        #endregion

        #region 擦除
        public bool BlockWriteData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, int uCnt, byte[] uDatabuf)
        {
            byte[] sendData = uhfProtocolParse.blockWriteDataSendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt, uDatabuf);

            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseBlockWriteData(receiveData);
                return result;
            }
        }

        public bool BlockEraseData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, byte uCnt)
        {
            byte[] sendData = uhfProtocolParse.blockEraseDataSendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, uBank, uPtr, uCnt);

            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseBlockEraseData(receiveData);
                return result;
            }
        }
        #endregion

        #region QT
        public bool SetQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte QTData)
        {
            byte[] sendData = uhfProtocolParse.setQTSendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData, QTData);

            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetQTData(receiveData);
                return result;
            }
        }

        public bool GetQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, ref byte QTData)
        {
            byte[] sendData = uhfProtocolParse.getQTSendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData);

            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                int result = uhfProtocolParse.parseGetQTData(receiveData);
                if (result != -1)
                {
                    QTData = (byte)result;
                }
                return false;
            }
        }
        #endregion

        #region BlockPermalock
        public bool BlockPermalock(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte ReadLock, byte uBank, int uPtr, byte uRange, byte[] uMaskbuf)
        {

            byte[] sendData = uhfProtocolParse.getBlockPermalockSendData(uAccessPwd, FilterBank, FilterStartaddr, FilterLen, FilterData,
                                      ReadLock, uBank, uPtr, uRange, uMaskbuf);

            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseBlockPermalocData(uRange, receiveData, uMaskbuf);
                return result;
            }
        }
        #endregion

        #region 蜂鸣器

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器</param>
        /// <returns></returns>
        public bool UHFSetBuzzer(byte mode)
        {
            bool isOpen = mode == 1 ? true : false;
            byte[] sendData = uhfProtocolParse.getBeepSendData(isOpen);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("UHFSetBuzzer 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseBeepData(receiveData);
                return result;
            }
        }

        public bool UHFGetBuzzer(byte[] mode)
        {
            byte[] sendData = uhfProtocolParse.getBeepStatusSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("UHFGetBuzzer 获取发送数据失败!");
                return false;
            }
            else
            {
                int result = uhfProtocolParse.parseGetBeepStatusData(receiveData);
                if (result == 0 || result == 1)
                {
                    mode[0] = (byte)result;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 工作模式 (命令工作模式、自动模式)
        public bool SetWorkMode(byte mode)
        {
            byte[] sendData = uhfProtocolParse.setSetWorkModeSendData(mode);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetWorkMode 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetWorkModeData(receiveData);
                return result;
            }
        }

        public bool GetWorkMode(byte[] mode)
        {
            byte[] sendData = uhfProtocolParse.getWorkModeSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                int result = uhfProtocolParse.parseGetWorkModeData(receiveData);
                if (result != -1)
                {
                    mode[0] = (byte)result;
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region 功率、频段
        public bool SetPower(byte save, byte uPower)
        {
            byte[] sendData = uhfProtocolParse.setPowerSendData(uPower, save == 1 ? true : false);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetPowerData(receiveData);
                return result;
            }
        }

        public bool GetPower(ref byte uPower)
        {
            byte[] sendData = uhfProtocolParse.getPowerSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetPower 获取发送数据失败!");
                return false;
            }
            else
            {
                int result = uhfProtocolParse.parseGetPowerData(receiveData);
                if (result > 0)
                {
                    uPower = (byte)result;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 设置区域
        /// </summary>
        /// <param name="saveflag"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public bool SetRegion(byte saveflag, byte region)
        {
            byte[] sendData = uhfProtocolParse.setFrequencyModeSendData(saveflag, region);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetRegion 获取返回数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetFrequencyModeData(receiveData);
                return result;
            }
        }
        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public bool GetRegion(ref byte region)
        {
            byte[] sendData = uhfProtocolParse.getFrequencyModeSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetFrequency 获取返回数据失败!");
                return false;
            }
            else
            {
                int result = uhfProtocolParse.parseGetFrequencyModeData(receiveData);
                region = (byte)result;
                return true;
            }
        }

        /// <summary>
        /// 获取频点
        /// </summary>
        /// <param name="Freqbuf"></param>
        /// <returns></returns>
        public bool GetJumpFrequency(ref int[] Freqbuf)
        {
            byte[] sendData = uhfProtocolParse.getJumpFrequencySendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetFrequency 获取返回数据失败!");
                return false;
            }
            else
            {
                int[] result = uhfProtocolParse.parseGetJumpFrequencyData(receiveData);
                if (result != null)
                {
                    Freqbuf = result;
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 设置频点
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="Freqbuf"></param>
        /// <returns></returns>
        public bool SetJumpFrequency(byte nums, int[] Freqbuf)
        {
            byte[] sendData = uhfProtocolParse.setJumpFrequencySendData(nums, Freqbuf);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetRegion 获取返回数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetJumpFrequencyData(receiveData);
                return result;
            }
        }

        #endregion

        #region 盘点标签
        public bool InventorySingle(ref string epc)
        {
            UHFTAGInfo uhfinfo = inventorySingleTag();
            if (uhfinfo != null)
            {
                epc = uhfinfo.Epc;
                return true;
            }
            return false;
        }
        public bool Inventory()
        {
            if (uhfinfoTemp != null) uhfinfoTemp.Clear();
            byte[] sendData = uhfProtocolParse.getStartInventoryTagSendData();
            byte[] receiveData = SendAndReceive(sendData, 500);
            // 开始盘点标签没有数据返回所以不需要解析
            return true;
        }
        public bool StopGet()
        {
            bool result = false;
            if (Debug.isDebug) Debug.PrintLog("==========Stop being==============");
            byte[] sendData = uhfProtocolParse.getStopInventorySendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (Debug.isDebug) Debug.PrintLog("==========Stop end==============");
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("stopInventoryTag 失败!");
                // return false;
            }
            else
            {
                result = uhfProtocolParse.parseStopInventoryData(receiveData);

            }

            return true; //return result;
        }
        public bool uhfGetReceived(ref string epc, ref string tid, ref string rssi, ref string ant)
        {
            if (uhfinfoTemp != null && uhfinfoTemp.Count > 0)
            {
                UHFTAGInfo info = uhfinfoTemp[0];
                epc = info.Epc;
                tid = info.Tid;
                rssi = info.Rssi;
                ant = info.Ant;
                uhfinfoTemp.RemoveAt(0);
                return true;
            }
            Thread.Sleep(2);

            byte[] sendData = uhfProtocolParse.getReadTagSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("inventorySingleTag 获取返回数据失败!");
                return false;
            }
            else
            {
                uhfinfoTemp = uhfProtocolParse.parsingTAGUsb(receiveData);
                if (uhfinfoTemp != null)
                {
                    UHFTAGInfo info = uhfinfoTemp[0];
                    epc = info.Epc;
                    tid = info.Tid;
                    rssi = info.Rssi;
                    ant = info.Ant;
                    uhfinfoTemp.RemoveAt(0);
                    return true;
                }
                return false;
            }
        }


        public UHFTAGInfo uhfGetReceived()
        {
            if (uhfinfoTemp != null && uhfinfoTemp.Count > 0)
            {
                UHFTAGInfo info = uhfinfoTemp[0];
                uhfinfoTemp.RemoveAt(0);
                return info;
            }
            Thread.Sleep(2);

            byte[] sendData = uhfProtocolParse.getReadTagSendData();
            //byte[] receiveData = DataConvert.HexStringToByteArray("A5 5A 00 16 EC 01 0C 20 FF A0 90 32 32 32 32 30 1F FD 0E C4 0D 0A 00 00 00 00");//
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("inventorySingleTag 获取返回数据失败!");
                return null;
            }
            else
            {
                uhfinfoTemp = uhfProtocolParse.parsingTAGUsb(receiveData);
                if (uhfinfoTemp != null)
                {
                    UHFTAGInfo info = uhfinfoTemp[0];
                    uhfinfoTemp.RemoveAt(0);
                    return info;
                }
                return null;
            }
        }
        /// <summary>
        /// 单次盘点标签
        /// </summary>
        /// <returns></returns>
        private UHFTAGInfo inventorySingleTag()
        {
            byte[] sendData = uhfProtocolParse.getInventorySingleTagSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("inventorySingleTag 获取返回数据失败!");
                return null;
            }
            else
            {
                UHFTAGInfo uhfinfo = uhfProtocolParse.parseInventorySingleTagData(receiveData);
                return uhfinfo;
            }
        }
        #endregion

        #region 读、写
        public string ReadData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, int uCnt)
        {
            byte[] sendData = uhfProtocolParse.getReadSendData(DataConvert.ByteArrayToHexString(uAccessPwd), (int)FilterBank, FilterStartaddr, FilterLen, DataConvert.ByteArrayToHexString(FilterData), (int)uBank, uPtr, uCnt);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("ReadData 获取返回数据失败!");
                return "";
            }
            else
            {
                string data = uhfProtocolParse.parseReadData(receiveData);
                if (data != null && data.Length > 0)
                    return data;
            }
            return "";
        }

        public bool WriteData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, byte uCnt, byte[] uDatabuf)
        {
            //getWriteSendData(string accessPwd, int filterBank, int filterPtr, int filterCnt, string filterData, int bank, int ptr, int cnt, string writeData)
            byte[] sendData = uhfProtocolParse.getWriteSendData(DataConvert.ByteArrayToHexString(uAccessPwd), (int)FilterBank, FilterStartaddr, FilterLen, DataConvert.ByteArrayToHexString(FilterData), (int)uBank, uPtr, uCnt, DataConvert.ByteArrayToHexString(uDatabuf));
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("WriteData 写入数据失败!");
                return false;
            }
            else
            {
                return uhfProtocolParse.parseWriteData(receiveData);
            }
        }
        #endregion

        #region gpio、继电器
        public bool getIOControl(byte[] outData)
        {
            byte[] sendData = uhfProtocolParse.getIOControlSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetRegion 获取返回数据失败!");
                return false;
            }
            else
            {
                byte[] result = uhfProtocolParse.UHFGetIOControl_RecvData(receiveData);
                if (result != null)
                {
                    for (int k = 0; k < result.Length; k++)
                    {
                        outData[k] = result[k];
                    }
                    return true;
                }
                return false;
            }
        }

        public bool setIOControl(byte ouput1, byte ouput2, byte outStatus)
        {
            byte[] sendData = uhfProtocolParse.setIOControlSendData(ouput1, ouput2, outStatus);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("SetRegion 获取返回数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetIOControlData(receiveData);
                return result;
            }
        }
        #endregion
        public bool Deactivate(int cmd, byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData)
        {
            throw new NotImplementedException();
        }

        public bool SetTemperatureVal(byte tempVal)
        {
            throw new NotImplementedException();
        }

        public int GetTemperatureVal()
        {
            throw new NotImplementedException();
        }

        public bool SetDualSingelMode(byte saveflag, byte mode)
        {
            throw new NotImplementedException();
        }

        public bool GetDualSingelMode(ref byte mode)
        {
            throw new NotImplementedException();
        }

        public bool SetTemperatureProtect(byte flag)
        {
            throw new NotImplementedException();
        }

        public bool GetTemperatureProtect(ref byte flag)
        {
            throw new NotImplementedException();
        }

        public bool SetDefaultMode()
        {
            throw new NotImplementedException();
        }

        public bool InventorySingle(ref byte uLenUii, ref byte[] uUii)
        {
            throw new NotImplementedException();
        }

        public bool GetReceived_EX(ref int uLenUii, ref byte[] uUii)
        {
            throw new NotImplementedException();
        }
        public string ReadQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte QTData, byte uBank, int uPtr, byte uCnt)
        {
            throw new NotImplementedException();
        }

        public bool WriteQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte QTData, byte uBank, int uPtr, byte uCnt, byte[] uDatabuf)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置工作时间和等待時間
        /// </summary>
        /// <param name="workTime">工作時間</param>
        /// <param name="waitTime">等待時間</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public bool setWorkAndWaitTime(int workTime, int waitTime, bool isSave)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 獲取工作时间和等待時間
        /// </summary>
        /// <param name="workTime">工作時間</param>
        /// <param name="waitTime">等待時間</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public bool getWorkAndWaitTime(out int workTime, out int waitTime)
        {
            throw new NotImplementedException();
        }

        #endregion



        private bool send(byte[] data)
        {
            PrintLog("sendData", data);
            if (hidUtils != null && hidUtils.SendBytes(data))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Received(byte[] data)
        {
            PrintLog("SendAndReceive", "接收的数据长度=" + data.Length);
            for (int k = 0; k < data.Length; k++)
            {
                if (receiveLastData == 0x0D && data[k] == 0x0A)
                    isCompleteReceive = true;

                receiveLastData = data[k];
                queueReceiveData.Enqueue(data[k]);
            }
            PrintLog("SendAndReceive", data);
        }
        private byte[] SendAndReceive(byte[] datas, int timeOut)
        {
            Console.Write("\r\n");
            Console.Write("\r\n");
            lock (objLock)
            {
                isCompleteReceive = false;
                CleanReceiveData();//清空之前接收到的数据
                //-----------------发送数据-----------------------------------
                PrintLog("SendAndReceive ==>send", datas);
                if (send(datas))
                {
                    //PrintLog("SendAndReceive","写数据");
                    //-------------------接收数据----------------------------------
                    int startTime = Environment.TickCount;
                    for (int k = 0; k < timeOut; k++)
                    {
                        if (isCompleteReceive)
                        {
                            break;
                        }
                        if (Environment.TickCount - startTime > timeOut)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }
                    int count = queueReceiveData.Count;
                    PrintLog("SendAndReceive", "接收的数据长度=" + count);
                    if (count == 0)
                    {
                        return null;
                    }
                    //-----------------
                    byte[] data = new byte[count];
                    for (int k = 0; k < data.Length; k++)
                    {
                        //返回并且移除隊列的中的數據
                        data[k] = (byte)queueReceiveData.Dequeue();
                    }
                    //-----------------
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
        private void CleanReceiveData()
        {
            queueReceiveData.Clear();
        }
        private void PrintLog(string msg)
        {
            if (isDebug)
            {
                Console.Write("\r\n");
                Console.Write("msg=" + msg);
                FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now + " " + msg + "\r\n");
            }
        }
        private void PrintLog(string tag, string msg)
        {
            if (isDebug)
            {
                Console.Write("\r\n");
                Console.Write(tag + "=" + msg);
                FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now + " " + tag + " " + msg + "\r\n");
            }
        }
        private void PrintLog(string tag, byte[] msg)
        {
            if (isDebug)
            {
                Console.Write("\r\n");
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < msg.Length; k++)
                {
                    sb.Append(String.Format("{0:X2} ", msg[k]));
                }
                Console.Write(tag + "=" + sb.ToString());
                FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now + " " + tag + " " + sb.ToString() + "\r\n");
            }
        }

        #region 设置盘点模式




        public bool setEPCMode()
        {
            byte[] sendData = uhfProtocolParse.setEpcTidUserModeSendData(0, 0, 0, 0);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("UHFSetBuzzer 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetEpcTidUserModeyData(receiveData);
                return result;
            }
        }

        public bool setEPCAndTIDMode()
        {
            byte[] sendData = uhfProtocolParse.setEpcTidUserModeSendData(0, 1, 0, 0);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("UHFSetBuzzer 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetEpcTidUserModeyData(receiveData);
                return result;
            }
        }

        public bool setEPCAndTIDUSERMode(byte userAddress, byte userLenth)
        {
            byte[] sendData = uhfProtocolParse.setEpcTidUserModeSendData(0, 2, userAddress, userLenth);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("UHFSetBuzzer 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseSetEpcTidUserModeyData(receiveData);
                return result;
            }
        }

        public int getEPCTIDUSERMode(ref byte userAddress, ref byte userLenth)
        {

            //  byte[] getEpcTidUserModeSendData();
            //  bool parseGetEpcTidUserModeyData(byte[] inData, ref int mode, ref int userAddress, ref int useLen);

            byte[] sendData = uhfProtocolParse.getEpcTidUserModeSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("UHFSetBuzzer 获取发送数据失败!");
                return -1;
            }
            else
            {
                int mode = -1;
                bool result = uhfProtocolParse.parseGetEpcTidUserModeyData(receiveData, ref mode, ref userAddress, ref userLenth);
                if (!result)
                    return -1;

                return mode;
            }

        }

        #endregion



        #region 升级


        public bool jump2Boot(byte flag)
        {

            byte[] sendData = uhfProtocolParse.jump2BootSendData(flag);
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                PrintLog("jump2Boot 获取发送数据失败!");
                return false;
            }
            else
            {
                if (flag == 0)
                {
                    //stm32升级
                    Thread.Sleep(3000);
                    Close();
                    Thread.Sleep(1000);
                    return Open();

                }

                bool result = uhfProtocolParse.parseJump2BootData(receiveData);
                return result;
            }
        }

        public bool startUpd()
        {
            byte[] sendData = uhfProtocolParse.startUpdSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("startUpd 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseStartUpdData(receiveData);
                return result;
            }
        }

        public bool updating(byte[] buf, int len)
        {
            if (buf != null && buf.Length > 0)
            {
                buf = uhfProtocolParse.updatingSendData(buf);

                if (buf.Length > 64)
                {
                    byte[] sendData = new byte[64];
                    Array.Copy(buf, 0, sendData, 0, 64);
                    bool result = send(sendData);
                    if (result)
                    {
                        Array.Clear(sendData, 0, sendData.Length);
                        Array.Copy(buf, 64, sendData, 0, buf.Length - 64);
                        byte[] receiveData = SendAndReceive(sendData, timeOut);
                        if (receiveData == null || receiveData.Length == 0)
                        {
                            if (Debug.isDebug) Debug.PrintLog("updating  发送数据失败!");
                            return false;
                        }
                        else
                        {
                            bool result3 = uhfProtocolParse.parseUpdatingData(receiveData);
                            return result3;
                        }
                    }
                    else
                    {
                        if (Debug.isDebug) Debug.PrintLog("updating 发送数据失败!");
                    }


                }
                else
                {
                    // byte[] sendData = uhfProtocolParse.updatingSendData(buf);
                    byte[] receiveData = SendAndReceive(buf, timeOut);
                    if (receiveData == null || receiveData.Length == 0)
                    {
                        if (Debug.isDebug) Debug.PrintLog("updating 获取发送数据失败!");
                        return false;
                    }
                    else
                    {
                        bool result = uhfProtocolParse.parseUpdatingData(receiveData);
                        return result;
                    }
                }

            }
            return false;
        }

        public bool stopUpdate()
        {
            byte[] sendData = uhfProtocolParse.stopUpdateSendData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("stopUpdate 获取发送数据失败!");
                return false;
            }
            else
            {
                bool result = uhfProtocolParse.parseStopUpdateData(receiveData);
                return result;
            }
        }

        #endregion

        public string GetSTM32Version()
        {
            byte[] version = new byte[50];

            byte[] sendData = uhfProtocolParse.getSTM32VersionData();
            byte[] receiveData = SendAndReceive(sendData, timeOut);
            if (receiveData == null || receiveData.Length == 0)
            {
                if (Debug.isDebug) Debug.PrintLog("GetSTM32Version 发送数据失败!");
                return "";
            }
            else
            {
                byte[] result = uhfProtocolParse.parseSTM32VersionData(receiveData);
                if (result == null || result.Length == 0)
                    return "";
                else
                    return System.Text.ASCIIEncoding.ASCII.GetString(result);

            }

            return string.Empty;
        }
    }
}
