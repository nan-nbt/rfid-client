using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
 

namespace BLEDeviceAPI
{
    class UsbNative
    {
        #region R2、R6缓存
        //**************删除R2、R6缓存标签***********************
        [DllImport("UHFAPI.dll", EntryPoint = "UHFBTDeleteAllTagToFlash_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBTDeleteAllTagToFlash_SendData(byte[] outSendData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFBTDeleteAllTagToFlash_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBTDeleteAllTagToFlash_RecvData(byte[] inData, int inLen);
        //********************************************************

        //**************获取R2、R6缓存的数量***********************
        [DllImport("UHFAPI.dll", EntryPoint = "UHFBTGetAllTagNumFromFlash_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBTGetAllTagNumFromFlash_SendData(byte[] outSendData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFBTGetAllTagNumFromFlash_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBTGetAllTagNumFromFlash_RecvData(byte[] inData, int inLen);
        //********************************************************

        //**************获取2、R6缓存标签***********************
        [DllImport("UHFAPI.dll", EntryPoint = "UHFBTGetTagDataFromFlash_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBTGetTagDataFromFlash_SendData(byte[] outSendData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFBTGetTagDataFromFlash_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBTGetTagDataFromFlash_RecvData(byte[] inData, int inLen, byte[] outTagData);
        //********************************************************
        #endregion

        #region  盘点标签
   
        [DllImport("UHFAPI.dll", EntryPoint = "UHFInventory_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFInventory_SendData(byte[] outData);
        //单次盘点标签
        [DllImport("UHFAPI.dll", EntryPoint = "UHFInventorySingle_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFInventorySingle_RecvData(byte[] inData, int inLen, byte[] uLenUii, byte[] uiiData);
        //停止盤點標簽
        [DllImport("UHFAPI.dll", EntryPoint = "UHFStopInventory_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFStopInventory_RecvData(byte[] inData, int inLen);
        //获取标签的发送数据
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetTagsData_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetTagsData_SendData(byte[] outSendData);
        //解析标签在返回数据
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetTagsData_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetTagsData_RecvData(byte[] inData, int inLen, byte[] uiiLen, byte[] uii);

        //解析标签返回的数据-网络
        [DllImport("UHFAPI.dll", EntryPoint = "UHF_TCP_TagsDataParse_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHF_TCP_TagsDataParse_RecvData(byte[] inData, int inLen, byte[] uiiLen, byte[] uii);
 
        #endregion

        #region 扫描条码
        [DllImport("UHFAPI.dll", EntryPoint = "Open2DScan_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int Open2DScan_RecvData(byte[] inData, int inLen, byte[] outData, byte[] outLen);
        #endregion

        #region 功率
        /**********************************************************************************************************
        * 功能：设置功率
        * 输入：saveflag  -- 1:保存设置   0：不保存
        * 输入：uPower -- 功率（DB）
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetPower_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetPower_SendData(byte saveflag, byte uPower, byte[] outSendData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetPower_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetPower_RecvData(byte[] inData, int inLen);

        /**********************************************************************************************************
        * 功能：获取功率
        * 输出：uPower -- 功率（DB）
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetPower_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetPower_SendData(byte[] outSendData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetPower_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetPower_RecvData(byte[] inData, int inLen, byte[] outData);

        #endregion

        #region 蜂鸣器
        ///**********************************************************************************************************
        //* 功能：设置蜂鸣器工作模式
        //* 输入：mode   1:打开  0：关闭
        //*********************************************************************************************************/

        //[DllImport("UHFAPI.dll", EntryPoint = "UHFSetBeep_SendData", CallingConvention = CallingConvention.Cdecl)]
        //public extern static int UHFSetBeep_SendData(byte mode, byte[] outSendData);


        //[DllImport("UHFAPI.dll", EntryPoint = "UHFSetBeep_RecvData", CallingConvention = CallingConvention.Cdecl)]
        //public extern static int UHFSetBeep_RecvData(byte[] inData, int len);


        ///**********************************************************************************************************
        //* 功能：获取蜂鸣器工作模式
        //* 返回值： 1:打开  0：关闭
        //*********************************************************************************************************/

        //[DllImport("UHFAPI.dll", EntryPoint = "UHFGetBeep_SendData", CallingConvention = CallingConvention.Cdecl)]
        //public extern static int UHFGetBeep_SendData(byte[] outSendData);


        //[DllImport("UHFAPI.dll", EntryPoint = "UHFGetBeep_RecvData", CallingConvention = CallingConvention.Cdecl)]
        //public extern static int UHFGetBeep_RecvData(byte[] inData, int len);

        /**********************************************************************************************************
        * 功能：设置读写器蜂鸣器工作模式
        * 输入：mode   1:打开  0：关闭

        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetReaderBeep_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetReaderBeep_SendData(byte mode, byte[] outSendData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetReaderBeep_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetReaderBeep_RecvData(byte[] inData, int inLen);

        /**********************************************************************************************************
        * 功能：获取读写器蜂鸣器工作模式
        * 返回值： 1:打开  0：关闭
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetReaderBeep_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetReaderBeep_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetReaderBeep_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetReaderBeep_RecvData(byte[] inData, int inLen);




        #endregion

        #region 读数据

        /**********************************************************************************************************
        * 功能：读标签数据区
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：bit
        * 输入：FilterLen -- 启动过滤的长度， 单位：bit
        * 输入：FilterData -- 启动过滤的数据
        * 输入：uBank -- 读取数据的bank
        * 输入：uPtr --  读取数据的起始地址， 单位：字
        * 输入：uCnt --  读取数据的长度， 单位：字
        * 输出：uReadDatabuf --  读取到的数据
        * 输出：uReadDataLen --  读取到的数据长度
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFReadData_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFReadData_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,
                                                                         int uBank, int uPtr, int uCnt, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFReadData_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFReadData_RecvData(byte[] inData, int inLen, byte[] uReadDatabufOut, ref int uReadDataLenOut);

        #endregion

        #region 写数据
        /**********************************************************************************************************
        * 功能：写标签数据区
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：bit
        * 输入：FilterLen -- 启动过滤的长度， 单位：bit
        * 输入：FilterData -- 启动过滤的数据  单位：字节
        * 输入：uBank -- 读取数据的bank
        * 输入：uPtr --  读取数据的起始地址， 单位：字
        * 输入：uCnt --  读取数据的长度， 单位：字
        * 输入：uWriteDatabuf --  写入的数据
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFWriteData_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFWriteData_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,
                                                                          byte uBank, int uPtr, byte uCnt, byte[] uDatabuf, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFWriteData_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFWriteData_RecvData(byte[] inData, int inLen);

        #endregion

        #region 锁

        /**********************************************************************************************************
        * 功能：LOCK标签
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
        * 输入：FilterData -- 启动过滤的数据
        * 输入：lockbuf --  3字节，第0-9位为Action位， 第10-19位为Mask位
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFLockTag_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFLockTag_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,
                                                                          byte[] lockbuf, byte[] outData);
        [DllImport("UHFAPI.dll", EntryPoint = "UHFLockTag_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFLockTag_RecvData(byte[] inData, int inLena);

        #endregion

        #region 销毁

        /**********************************************************************************************************
        * 功能：KILL标签
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
        * 输入：FilterData -- 启动过滤的数据
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFKillTag_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFKillTag_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFKillTag_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFKillTag_RecvData(byte[] inData, int inLen);

        #endregion

        #region 频段设置
        /**********************************************************************************************************
        * 功能：区域设置
        * 输入：saveflag -- 1:掉电保存，  0：不保存
        * 输入：region -- 0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetRegion_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetRegion_SendData(byte saveflag, byte region, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetRegion_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetRegion_RecvData(byte[] inData, int inLen);

        /**********************************************************************************************************
        * 功能：获取区域设置
        * 输出：region -- 0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetRegion_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetRegion_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetRegion_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetRegion_RecvData(byte[] inData, int inLen, byte[] region);

        #endregion

        #region 模块温度

        /**********************************************************************************************************
        * 功能：获取当前温度
        * 输出：temperature -- 整型
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetTemperature_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetTemperature_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetTemperature_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetTemperature_RecvData(byte[] inData, int inLen, byte[] temperature);


        /**********************************************************************************************************
        * 功能：获取软件版本号
        * 输出：version[0]--版本号长度 ,  version[1--x]--版本号
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetSoftwareVersion_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetSoftwareVersion_SendData(byte[] outData);


        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetSoftwareVersion_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetSoftwareVersion_RecvData(byte[] inData, int inLen, byte[] version);


        /**********************************************************************************************************
        * 功能：获取硬件版本号
        * 输出：version[0]--版本号长度 ,  version[1--x]--版本号
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetHardwareVersion_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetHardwareVersion_SendData(byte[] outData);


        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetHardwareVersion_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetHardwareVersion_RecvData(byte[] inData, int inLen, byte[] version);


        /**********************************************************************************************************
         * 功能：获取ID号
         * 输出：id--整型ID号
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetDeviceID_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetDeviceID_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetDeviceID_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetDeviceID_RecvData(byte[] inData, int inLen, byte[] id);

        #endregion

        #region 协议

        /**********************************************************************************************************
            * 功能：设置协议类型
            * 输入：type  0x00 表示 ISO18000-6C 协议,0x01 表示 GB/T 29768 国标协议,0x02 表示 GJB 7377.1 国军标协议
            * 
            *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetProtocolType_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetProtocolType_SendData(byte type, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetProtocolType_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetProtocolType_RecvData(byte[] inData, int inLen);



        /**********************************************************************************************************
        * 功能：获取协议类型
        * 输出：type  0x00 表示 ISO18000-6C 协议,0x01 表示 GB/T 29768 国标协议,0x02 表示 GJB 7377.1 国军标协议*/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetProtocolType_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetProtocolType_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetProtocolType_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetProtocolType_RecvData(byte[] inData, int inLen, byte[] type);

        #endregion

        #region 盘点过滤

        /**********************************************************************************************************
                * 功能：设置寻标签过滤设置
                * 输入：saveflag -- 1:掉电保存， 0：不保存
                * 输入：bank --  0x01:EPC , 0x02:TID, 0x03:USR
                * 输入：startaddr 起始地址，单位：字节
                * 输入：datalen 数据长度， 单位:字节
                * 输入：databuf 数据
                *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetFilter_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetFilter_SendData(byte saveflag, byte bank, int startaddr, int datalen, byte[] databuf, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetFilter_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetFilter_RecvData(byte[] inData, int inLen);


        #endregion

        #region 天线号
        /**********************************************************************************************************
            * 功能：天线设置
            * 输入：saveflag -- 1:掉电保存，  0：不保存
            * 输入：buf--2bytes, 共16bits, 每bit 置1选择对应天线
            *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetANT_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetANT_RecvData(byte[] inData, int inLen);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetANT_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetANT_SendData(byte saveflag, byte[] buf, byte[] outData);



        /**********************************************************************************************************
        * 功能：获取天线设置
        * 输出：buf--2bytes, 共16bits,
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetANT_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetANT_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetANT_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetANT_RecvData(byte[] intData, int inLen, byte[] outData);
        #endregion



        #region Gen2

        /**********************************************************************************************************
            * 功能：设置Gen2参数
            * 输入：
            **********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetGen2_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetGen2_RecvData(byte[] intData, int inLen);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetGen2_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetGen2_SendData(byte Target, byte Action, byte T, byte Q,
                            byte StartQ, byte MinQ,
                            byte MaxQ, byte D, byte C, byte P,
                            byte Sel, byte Session, byte G, byte LF, byte[] outData);
        /**********************************************************************************************************
        * 功能：获取Gen2参数
        * 输入：
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetGen2_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetGen2_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetGen2_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetGen2_RecvData(byte[] inData, int inLen, ref byte Target, ref byte Action, ref byte T, ref byte Q,
            ref byte StartQ, ref byte MinQ,
                ref byte MaxQ, ref byte D, ref byte Coding, ref byte P,
            ref byte Sel, ref byte Session, ref byte G, ref byte LF);

        #endregion





        /**********************************************************************************************************
        * 功能：设置链路组合
        * 输入：saveflag -- 1:掉电保存， 0：不保存
        * 输入：mode --  0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetRFLink_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetRFLink_SendData(byte saveflag, byte mode, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetRFLink_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetRFLink_RecvData(byte[] inData, int inDataLen);
        /**********************************************************************************************************
        * 功能：获取链路组合
        * 输出：mode --  0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetRFLink_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetRFLink_RecvData(byte[] inData, int inDataLen, byte[] mode);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetRFLink_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetRFLink_SendData(byte[] outData);


        //--------设置IP-------------
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetIp_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetIp_SendData(byte[] ipbuf, byte[] postbuff, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetIp_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetIp_RecvData(byte[] inData, int inLen);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetIp_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetIp_SendData(byte[] ouData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetIp_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetIp_RecvData(byte[] inData, int inLen, byte[] ipbuf, byte[] postbuff);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetDestIp_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetDestIp_SendData(byte[] ipbuf, byte[] postbuff, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetDestIp_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetDestIp_RecvData(byte[] inData, int inLen);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetDestIp_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetDestIp_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetDestIp_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetDestIp_RecvData(byte[] inData, int inLen, byte[] ipbuf, byte[] postbuf);


   
        /**********************************************************************************************************
        * 功能：设置EPC TID USER模式

        * 输入：saveflag -- 1:掉电保存， 0：不保存

        * 输入：Memory 0x00，表示关闭； 0x01，表示开启EPC+TID模式（默认 地址为 0x00, 长 度 为 6 个 字 ） ； 0x02, 表 示 开 启EPC+TID+USER模式

        * 输入：address 为USER区的起始地址（单位为字）
        * 输入：为USER区的长度（单位为字）
        *********************************************************************************************************/
          [DllImport("UHFAPI.dll", EntryPoint = "UHFSetEPCTIDMode_SendData", CallingConvention = CallingConvention.Cdecl)]
          public extern static int UHFSetEPCTIDMode_SendData(byte saveFlag,byte memory,byte address,byte length,byte[] outData);

          [DllImport("UHFAPI.dll", EntryPoint = "UHFSetEPCTIDMode_RecvData", CallingConvention = CallingConvention.Cdecl)]
          public extern static int UHFSetEPCTIDMode_RecvData(byte[] inData, int inLen);
  
 
        /**********************************************************************************************************
        * 功能：获取EPC TID USER 模式

        * 输入：rev1 :保留数据，传入0
        * 输入：rev2 :保留数据，传入0

        * 输出：data[0]，Memory 0x00，表示关闭； 0x01，表示开启EPC+TID模式（默认 地址为 0x00, 长 度 为 6 个 字 ） ； 0x02, 表 示 开 启EPC+TID+USER模式

        * 输出：data[1]address 为USER区的起始地址（单位为字）
        * 输出：data[2]为USER区的长度（单位为字）
        *
        * 返回值：3：正确，其它：错误
        *********************************************************************************************************/
          [DllImport("UHFAPI.dll", EntryPoint = "UHFGetEPCTIDMode_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetEPCTIDMode_SendData(byte rev1, byte rev2, byte[] outData);


          [DllImport("UHFAPI.dll", EntryPoint = "UHFGetEPCTIDMode_RecvData", CallingConvention = CallingConvention.Cdecl)]
          public extern static int UHFGetEPCTIDMode_RecvData(byte[] inData, int inLen, byte[] mode);


        /**********************************************************************************************************
        * 功能：设置Tagfocus功能
        * 输入：flag -- 1:开启， 0：关闭
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetTagfocus_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetTagfocus_SendData(byte flag, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetTagfocus_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetTagfocus_RecvData(byte[] inData, int inLen);
        /**********************************************************************************************************
        * 功能：获取Tagfocus功能
        * 输出：flag -- 1:开启， 0：关闭
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetTagfocus_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetTagfocus_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetTagfocus_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetTagfocus_RecvData(byte[] inData, int inLen, byte[] flag);




        /**********************************************************************************************************
        * 功能：设置FastID功能
        * 输入：flag -- 1:开启， 0：关闭
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetFastID_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetFastID_SendData(byte flag, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetFastID_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetFastID_RecvData(byte[] inData, int inLen);

        /**********************************************************************************************************
        * 功能：获取FastID功能
        * 输出：flag -- 1:开启， 0：关闭
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetFastID_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetFastID_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetFastID_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetFastID_RecvData(byte[] inData, int inLen, byte[] flag);

        /**********************************************************************************************************
        * 功能：设置CW
        * 输入：flag -- 1:开CW，  0：关CW
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetCW_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetCW_SendData(byte flag, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetCW_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetCW_RecvData(byte[] inData, int inLen);


        /**********************************************************************************************************
        * 功能：获取CW
        * 输出：flag -- 1:开CW，  0：关CW
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetCW_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetCW_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetCW_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetCW_RecvData(byte[] inData, int inLen, byte[] flag);

        /**********************************************************************************************************
        * 功能：设置软件复位
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetSoftReset_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetSoftReset_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetSoftReset_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetSoftReset_RecvData(byte[] inData, int inLen);

        /**********************************************************************************************************
           * 功能：继电器和 IO 控制输出设置
           * 输入：output1:    0:低电平   1：高电平

                   output2:    0:低电平   1：高电平

                   outStatus： 0：断开    1：闭合

             返回值：0：设置成功     -1：设置失败

           * 
           *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetIOControl_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetIOControl_SendData(byte output1, byte output2, byte outStatus, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetIOControl_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetIOControl_RecvData(byte[] inData, int inLen);

        /**********************************************************************************************************
        * 功能：获取继电器和 IO 控制输出设置状态
        * 输出：statusData[0]:    0:低电平   1：高电平

                statusData[1]:    0:低电平   1：高电平


          返回值：2：数据长度    -1：获取失败

        * 
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetIOControl_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetIOControl_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetIOControl_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetIOControl_RecvData(byte[] inData, int inLen, byte[] statusData);

        /**********************************************************************************************************
        * 功能：设置跳频
        * 输入：nums -- 跳频个数
        * 输入：Freqbuf--频点数组（整型） ，如920125，921250.....
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetJumpFrequency_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetJumpFrequency_SendData(byte nums, int[] Freqbuf, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetJumpFrequency_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetJumpFrequency_RecvData(byte[] inData, int inLen);

        /**********************************************************************************************************
        * 功能：获取跳频
        * 输出：Freqbuf[0]--频点个数， Freqbuf[1]..[x]--频点数组（整型）
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetJumpFrequency_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetJumpFrequency_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetJumpFrequency_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetJumpFrequency_RecvData(byte[] inData, int inLen, byte[] Freqbuf);


        /**********************************************************************************************************
         * 功能：获取天线工作时间
         * 输入：antnum -- 天线号
         * 输出：WorkTime -- 工作时间 ，单位ms, 范围 10-65535ms
         *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetANTWorkTime_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetANTWorkTime_SendData(byte antnum, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetANTWorkTime_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetANTWorkTime_RecvData(byte[] inData, int inLen, int[] WorkTime);


        /**********************************************************************************************************
        * 功能：设置天线工作时间
        * 输入：antnum -- 天线号
        * 输入：saveflag -- 1:掉电保存， 0：不保存
        * 输入：WorkTime -- 工作时间 ，单位ms, 范围 10-65535ms
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetANTWorkTime_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetANTWorkTime_SendData(byte antnum, byte saveflag, int WorkTime, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetANTWorkTime_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetANTWorkTime_RecvData(byte[] inData, int inLen);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="outData"></param>
        /// <returns></returns>
        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetWorkMode_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetWorkMode_SendData(byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetWorkMode_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetWorkMode_RecvData(byte[] inData, int inLen, byte[] mode);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetWorkMode_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetWorkMode_SendData(byte mode, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetWorkMode_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetWorkMode_RecvData(byte[] inData, int inLen);



        /**********************************************************************************************************
        * 功能：Block Permalock操作
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
        * 输入：FilterData -- 启动过滤的数据
        * 输入：ReadLock --  bit0：（0：表示Read ， 1：表示Permalock）  
        * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
        * 输入：uPtr --  Block起始地址 ，单位为16个块
        * 输入：uRange --  Block范围，单位为16个块
        * 输入：uMaskbuf -- 块掩码数据，2个字节，16bit 对应16个块
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFBlockPermalock_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBlockPermalock_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,
                                   byte ReadLock, byte uBank, int uPtr, byte uRange, byte[] uMaskbuf, byte[] outData);

        /**********************************************************************************************************
        * 输入：uRange --  Block范围，单位为16个块
        * 输出：uMaskbuf -- 块掩码数据，2个字节，16bit 对应16个块
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFBlockPermalock_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBlockPermalock_RecvData(byte uRange, byte[] inData, int inLen, byte[] uMaskbuf);



        /**********************************************************************************************************
        * 功能：设置QT命令参数
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
        * 输入：FilterData -- 启动过滤的数据
        * 输入：QTData --  bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用public memory map)
        *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetQT_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetQT_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,
                                                                          byte QTData, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFSetQT_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFSetQT_RecvData(byte[] inData, int inLen);


        /**********************************************************************************************************
        * 功能：获取QT命令参数
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
        * 输入：FilterData -- 启动过滤的数据
        * 输出：QTData --  bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用public memory map)
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetQT_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetQT_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,
                                                                           byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFGetQT_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFGetQT_RecvData(byte[] inData, int inLen, byte[] QTData);





        /**********************************************************************************************************
        * 功能：BlockWrite 特定长度的数据到标签的特定地址
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
        * 输入：FilterData -- 启动过滤的数据
        * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
        * 输入：uPtr --  读取数据的起始地址， 单位：字
        * 输入：uCnt --  读取数据的长度， 单位：字
        * 输入：uWriteDatabuf --  写入的数据
        *********************************************************************************************************/

        [DllImport("UHFAPI.dll", EntryPoint = "UHFBlockWriteData_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBlockWriteData_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, int uCnt, byte[] uWriteDatabuf, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFBlockWriteData_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBlockWriteData_RecvData(byte[] inData, int inLen);


        /**********************************************************************************************************
       * 功能：BlockErase 特定长度的数据到标签的特定地址
       * 输入：uAccessPwd -- 4字节密码
       * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
       * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
       * 输入：FilterLen -- 启动过滤的长度， 单位：字节
       * 输入：FilterData -- 启动过滤的数据
       * 输入：uBank -- 块号  1：EPC, 2:TID, 3:USR
       * 输入：uPtr --  读取数据的起始地址， 单位：字
       * 输入：uCnt --  读取数据的长度， 单位：字
       *********************************************************************************************************/
        [DllImport("UHFAPI.dll", EntryPoint = "UHFBlockEraseData_SendData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBlockEraseData_SendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, byte uCnt, byte[] outData);

        [DllImport("UHFAPI.dll", EntryPoint = "UHFBlockEraseData_RecvData", CallingConvention = CallingConvention.Cdecl)]
        public extern static int UHFBlockEraseData_RecvData(byte[] inData, int inLen);


        /**********************************************************************************************************
        * 功能：国标LOCK标签
        * 输入：uAccessPwd -- 4字节密码
        * 输入：FilterBank -- 启动过滤的bank号， 1：EPC, 2:TID, 3:USR
        * 输入：FilterStartaddr -- 启动过滤的起始地址， 单位：字节
        * 输入：FilterLen -- 启动过滤的长度， 单位：字节
        * 输入：FilterData -- 启动过滤的数据

        * 输入：memory 存储区：  0x00 表示标签信息区,   0x10 表示编码区,   0x20 表示安全区,   0x3x 表示用户区 0x30-0x3F 表示用户区编号 0 到编号 15
                config 配置：    0x00 表示配置存储区属性,    0x01 表示配置安全模式


          action:  
          配置存储区属性:  0x00:可读可写,  0x01:可读不可写,  0x02:不可读可写,  0x03:不可读不可写
	      配置安全模式:    0x00:保留,   0x01:不需要鉴别,   0x02:需要鉴别,不需要安全通信,   0x03:需要鉴别,需要安全通信

         *********************************************************************************************************/
         [DllImport("UHFAPI.dll", EntryPoint = "UHFGBTagLock_SendData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int UHFGBTagLock_SendData(byte[] uAccessPwd,byte FilterBank,int FilterStartaddr,int FilterLen,byte[] FilterData,byte memory,byte config,byte action,byte[] outData);
									  
                            
         [DllImport("UHFAPI.dll", EntryPoint = "UHFGBTagLock_RecvData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int UHFGBTagLock_RecvData(byte[] inData,int inLen);
 
        //-----------------------------------
         [DllImport("UHFAPI.dll", EntryPoint = "UHFJump2Boot_SendData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int UHFJump2Boot_SendData(int flag,byte[] outData);

         [DllImport("UHFAPI.dll", EntryPoint = "UHFJump2Boot_RecvData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFJump2Boot_RecvData(byte[] inData,int inLen);


         [DllImport("UHFAPI.dll", EntryPoint = "UHFStartUpd_SendData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFStartUpd_SendData(byte[] outData);

         [DllImport("UHFAPI.dll", EntryPoint = "UHFStartUpd_RecvData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFStartUpd_RecvData(byte[] inData,int inLen);


         [DllImport("UHFAPI.dll", EntryPoint = "UHFUpdating_SendData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int UHFUpdating_SendData(byte[] buf, byte[] outData);

         [DllImport("UHFAPI.dll", EntryPoint = "UHFUpdating_RecvData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFUpdating_RecvData(byte[] inData,int inLen);


         [DllImport("UHFAPI.dll", EntryPoint = "UHFStopUpdate_SendData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFStopUpdate_SendData(byte[] outData);

         [DllImport("UHFAPI.dll", EntryPoint = "UHFStopUpdate_RecvData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFStopUpdate_RecvData(byte[] inData,int inLen);
 


        /**********************************************************************************************************
            * 功能：获取STM32软件版本号
            * 输出：version[0-4] --- Vx.xx
         *********************************************************************************************************/
        
         [DllImport("UHFAPI.dll", EntryPoint = "UHFGetSTM32Version_SendData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFGetSTM32Version_SendData(byte[] outData);
    
  
         [DllImport("UHFAPI.dll", EntryPoint = "UHFGetSTM32Version_RecvData", CallingConvention = CallingConvention.Cdecl)]
         public extern static int  UHFGetSTM32Version_RecvData(byte[] inData,int inLen,byte[] version);
    }
}


 