using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLEDeviceAPI;
using UHFAPP.usb.hid_new;

namespace UHFAPP.usb
{
    class UsbUHFProtocolParse : UHFProtocolParse
    {
        const int R_START = 0;
        const int R_5A = 1;
        const int R_LEN_H = 2;//数据长度高位
        const int R_LEN_L = 3;//数据长度低位
        const int R_CMD = 4;//命令字节
        const int R_DATA = 5;//数据
        const int R_XOR = 6;//校验位
        const int R_END_0D = 7;//结束贞
        const int R_END_0A = 8;//结束贞
        const int Head1 = 0xA5;
        const int Head2 = 0x5A;
        const int Tail1 = 0x0D;
        const int Tail2 = 0x0A;
        byte[] rbuf = new byte[2048];
 
        public List<UHFTAGInfo> parsingTAGUsb(byte[] inData)
        {
            return parseReadTagDataEPC_TID_USER(inData);
        }

        private List<UHFTAGInfo> parseReadTagDataEPC_TID_USER(byte[] inData)
        {
            if (inData == null || inData.Length == 0)
            {
                return null;
            }
           // Console.WriteLine("parseReadTagDataEPC_TID_USER hex=" + DataConvert.ByteArrayToHexString(inData));
            byte[] data = parseData(0xEB, inData);//0xE1
      
            if (data != null && data.Length > 0)
            {
                //Console.WriteLine("parseReadTagDataEPC_TID_USER hex=" + DataConvert.ByteArrayToHexString(data));
                return parseReadTagDataEPCTIDUSER(data);
            }
            Console.WriteLine("parseReadTagDataEPC_TID_USER  fail");
            return null;

        }

   
        //多张标签的解析，蓝牙解析，包含 EPC、TID、USER的任意数据
        private List<UHFTAGInfo> parseReadTagDataEPCTIDUSER(byte[] data)
        {
            List<UHFTAGInfo> list = null;
            int index = 0;
            if (data == null)
            {
                return null;
            }
            else if (data.Length <= 1)
            {
                return null;
            }
            else if (data.Length < 5)
            {
                index = ((data[0] & 0xFF) << 8) | (data[1] & 0xFF);
                list = new List<UHFTAGInfo>();
                UHFTAGInfo tag = new UHFTAGInfo();
                list.Add(tag);
                return list;
            }

            list = new List<UHFTAGInfo>();




            int count = data[0] & 0xFF;// data[2] & 0xFF;// 标签个数
            int epc_lenIndex = 1;// ;// epc长度索引
            int epc_startIndex = 2;// ; // 截取epc数据的起始索引
            int epc_endIndex = 1;// 截取epc数据的结束索引


            for (int k = 0; k < count; k++)
            {
                epc_startIndex = epc_lenIndex + 1;
                epc_endIndex = epc_startIndex + (data[epc_lenIndex] & 0xFF);// epc的起始索引加长度得到结束索引
                if (epc_endIndex > data.Length)
                    break;
                else
                {
                    int leng = epc_endIndex - epc_startIndex;
                    byte[] epcBuff = new byte[leng];
                    Array.Copy(data, epc_startIndex, epcBuff, 0, leng);
                    UHFTAGInfo info = parserUhfTagBuff_EPC_TID_USER(epcBuff, false);
                    if (info != null)
                    {
                        list.Add(info);
                    }
                }
                epc_lenIndex = epc_endIndex;
                if (epc_lenIndex >= data.Length)
                    break;
            }

            if (list.Count() > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
      
        private byte[] parseData(int cmd, byte[] inDataBuf)
        {

            if (inDataBuf == null || inDataBuf.Length == 0)
            {
                return null;
            }
            Array.Clear(rbuf, 0, rbuf.Length);
            int rxsta = R_START;
            int rlen = 0;//数据长度
            int ridx = 0; //数据
            int rxor = 0; //校验字节
            int rcmd = 0; //命令字节
            int rflag = 0;//是否正确的完成了数据解析

            for (int i = 0; i < inDataBuf.Length; i++)
            {
                int tmpdata = inDataBuf[i] & 0xFF;
                switch (rxsta)
                {
                    case R_START:
                        if (tmpdata == Head1)
                        {
                            rxsta = R_5A;
                        }
                        else
                        {
                            rxsta = R_START;
                        }
                        rxor = 0;
                        ridx = 0;
                        rlen = 0;
                        rflag = 0;
                        break;
                    case R_5A:
                        if (tmpdata == Head2)
                        {
                            rxsta = R_LEN_H;
                        }
                        else
                        {
                            rxsta = R_START;
                        }
                        break;
                    case R_LEN_H:
                        rxor = rxor ^ tmpdata;
                        rlen = tmpdata * 256;
                        rxsta = R_LEN_L;
                        break;
                    case R_LEN_L:
                        rxor = rxor ^ tmpdata;
                        rlen = rlen + tmpdata;
                        if ((rlen < 8) || (rlen > 2048))
                        {
                            rxsta = R_START;
                        }
                        else
                        {
                            rlen = rlen - 8;
                            rxsta = R_CMD;
                        }
                        break;
                    case R_CMD:
                        rxor = rxor ^ tmpdata;
                        rcmd = tmpdata;
                        if (rlen > 0)
                        {
                            rxsta = R_DATA;
                        }
                        else
                        {
                            rxsta = R_XOR;
                        }
                        break;
                    case R_DATA:
                        if (rlen == 0)
                        {
                            rxsta = R_START;
                            break;
                        }
                        if (ridx < rlen)
                        {
                            rxor = rxor ^ tmpdata;
                            rbuf[ridx++] = (byte)tmpdata;
                            if (ridx >= rlen)
                            {
                                rxsta = R_XOR;
                            }
                        }
                        break;
                    case R_XOR:
                        {
                            if (rxor == tmpdata)
                            {
                                rxsta = R_END_0D;
                            }
                            else
                            {
                                rxsta = R_START;
                            }
                        }
                        break;
                    case R_END_0D:
                        if (tmpdata == Tail1)
                        {
                            rxsta = R_END_0A;
                        }
                        else
                        {
                            rxsta = R_START;
                        }
                        break;
                    case R_END_0A:
                        rxsta = R_START;
                        if (tmpdata == Tail2)
                        {
                            rflag = 1;
                        }
                        break;
                    default:
                        rxor = 0;
                        ridx = 0;
                        rlen = 0;
                        rflag = 0;
                        break;
                }
                if (rflag == 1)
                    break;
            }
            if (rflag == 1)
            {
                if (rcmd != cmd)
                {

                 //  return null;
                }
                byte[] data=new byte[rlen];
                Array.Copy(rbuf, 0, data,0, rlen);
               // rbuf.CopyTo(data,rlen);
                return data;
            }
            else
            {
                return null;
            }
        }

         

        //只有一张标签的解析  30 00 E2 00 00 17 01 0B 01 37 19 10 50 CD FD B9
        UHFTAGInfo parserUhfTagBuff_EPC_TID_USER(byte[] tagsBuff, bool isContainAnt)
        {
            if (tagsBuff == null || tagsBuff.Length == 0)
            {
                return null;
            }
            if (tagsBuff.Length < 3)
            {
                return null;
            }
            UHFTAGInfo info = new UHFTAGInfo();
            int len = tagsBuff.Length;
            byte[] pcBuff = new byte[2];
            Array.Copy(tagsBuff, 0, pcBuff,0,2);
            int pc = (pcBuff[0] & 0xFF) | ((pcBuff[1] & 0xFF) << 8);
            info.Pc = DataConvert.ByteArrayToHexString(pcBuff);

            // -----获取epc---------
            int epclen = ((pcBuff[0] & 0xFF) >> 3) * 2;//(pc >> 3) * 2;
            byte[] epc = new byte[epclen]; 
            Array.Copy(tagsBuff, 2, epc, 0, epclen);
            info.Epc = DataConvert.ByteArrayToHexString(epc);


            if (info.Epc == null || info.Epc=="")
            {
                return null;
            }
            // -----------------------
            int uiiLen = epclen + 2;

            byte[] rssi = null;
            byte[] ant_data = null;
            String strAnt = "";
            if (len >= uiiLen + 12)
            {
                // -----获取tid---------
                int tidStart = uiiLen;
                int tidEnd = tidStart + 12;
                byte[] tidBuff =new byte[12]; 
                Array.Copy(tagsBuff, tidStart, tidBuff, 0, 12);
                info.Tid = DataConvert.ByteArrayToHexString(tidBuff);
                if (len - 3 > tidEnd)
                {
                    // -----获取user---------
                    //包含天线号，所以长度是3; 连续盘点只有Rssi的两个字节
                    int rssiAndAntLen = isContainAnt ? 3 : 2;
                    int userStart = tidEnd;
                    int userEnd = len - rssiAndAntLen;
                    byte[] userBuff = new byte[userEnd - userStart];
                    Array.Copy(tagsBuff, userStart, userBuff, 0, userBuff.Length);
                    info.User = DataConvert.ByteArrayToHexString(userBuff);

                    if (len >= userEnd + rssiAndAntLen)
                    {
                        int rssiStart = userEnd;
                        int rssiEnd = userEnd + 2;
                        rssi = new byte[2]; 
                        Array.Copy(tagsBuff, rssiStart, rssi, 0, rssi.Length);
                    }
                }
                else
                {
                    if (len >= tidEnd + 2)
                    {
                        int rssiStart = tidEnd;
                        int rssiEnd = tidEnd + 2;
                        rssi = new byte[2];
                        Array.Copy(tagsBuff, rssiStart, rssi, 0, rssi.Length);
                    }
                }
            }
            else
            {
                if (len >= uiiLen + 2)
                {
                    int rssiStart = uiiLen;
                    int rssiEnd = 2 + uiiLen;
                    rssi = new byte[2];
                    Array.Copy(tagsBuff, rssiStart, rssi, 0, rssi.Length);
                }
            }
            // -------rssi-------------
            if (rssi != null)
            {

                float dBm = (65535 - ((rssi[0] << 8) | (rssi[1]))) / 10f;
                String strdBm = "N/A";
                if (dBm < 200 && dBm > 0)
                {
                    strdBm = "-" + dBm.ToString("#0.00 ");
                }
                info.Rssi = strdBm;
            }
            // --------------------
            // -----Ant------------
            if (ant_data != null)
            {
         
            }
            // ----------
            if (info == null || string.IsNullOrEmpty(info.Epc))
                info = null;

            return info;
        }
    }
}
