using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLEDeviceAPI.interfaces
{
    interface IUHFProtocolParse
    {
        /**
         * 
         * 解析设置蜂鸣器返回的数据
         * 
         * @param inData 设置蜂鸣器返回的原始数据
         * @return true: 设置成功  ,flase:设置设备
         */
        bool parseBeepData(byte[] inData);

        /**
         * 解析扫描条码返回的数据
         * @param inData  蓝牙返回的条码原始数据
         * @return 返回解析后的条码数据
         */
        byte[] parseBarcodeData(byte[] inData);

        /**
         * 解析获取电量返回的数据
         * @param inData  蓝牙返回的原始数据
         * @return 返回解析后的电量
         */
        int parseBatteryData(byte[] inData);

        /**
         * 解析写标签返回的数据
         * @param inData 蓝牙返回的原始数据
         * @return true:写标签成功    false:写标签失败
         */
        bool parseWriteData(byte[] inData);

        /**
         * 解析读标签返回的数据
         * @param inData 蓝牙返回的原始数据
         * @return 返回解析后的标签数据
         */
        string parseReadData(byte[] inData);

        /**
         *  解析锁标签返回的数据
         * @param inData 蓝牙返回的原始数据
         * @return true:锁成功， false:锁失败
         */
        bool parseLockData(byte[] inData);

        /**
         * 解析销毁标签返回的数据
         * 
         * @param inData 蓝牙返回的原始数据
         * @return true:销毁标签成功， false:销毁标签失败
         */
        bool parseKillData(byte[] inData);


        /**
         * 解析单次盘点标签返回的数据
         * @return 返回单次盘点标签的数据
         */
        UHFTAGInfo parseInventorySingleTagData(byte[] data);

        /**
        * 解析盘点标签返回的数据
        * @return 返回单次盘点标签的数据
        */
        List<UHFTAGInfo> parseInventoryTagData(byte[] data);
        /**
         * 解析设置功率返回的数据
         * @param inData 蓝牙返回的原始数据
         * @return true:设置功率成功, flase:设置功率失败
         */
        bool parseSetPowerData(byte[] iData);

        /**
         * 解析获取功率返回的数据
         * @param inData 蓝牙返回的原始数据
         * @return 返回功率
         */
        int parseGetPowerData(byte[] iData);

        /**
         * 解析设置协议返回的数据
         * @param inData 蓝牙返回的原始数据
         * @return true:设置协议成功， false:设置协议失败
         */
        bool parseSetProtocolData(byte[] inData);

        /**
         * 解析获取协议返回的数据
         * @param inData 蓝牙返回的原始数据
         * @return true:获取协议成功， false:获取协议失败
         */
        int parseGetProtocolData(byte[] inData);

        /**
         * 解析开始盘点标签返回的数据
         * 
         * @return true:开始盘点成功，false:开始盘点失败
         */
        bool parseStartInventoryTagData();



        /**
         * 解析停止盘点标签返回的数据
         * 
         * @return true:停止盘点成功，false:停止盘点失败
         */
        bool parseStopInventoryData(byte[] inData);

        //************************************send begin*********************************************************************
        /**
         * 
         * 获取设置蜂鸣器的发送数据
         * 
         * @param isOpen  true:表示打开蜂鸣器， false:表示关闭蜂鸣器
         * 
         * @return 发送的数据
         */
        byte[] getBeepSendData(bool isOpen);

        /**
         * 
         * 获取扫描条码的发送数据
         * 
         * @return 发送的数据
         */
        byte[] getScanBarcodeSendData();

        /**
         * 
         * 获取电量的发送数据
         * 
         * @return 发送的数据
         */
        byte[] getBatterySendData();

        /**
         * 获取写标签的发送数据<br>
         * 
         * @param accessPwd
         *            标签的ACCESS PASSWORD（4字 节）<br>
         * @param filterBank
         *            过滤的数据块<br>
         * @param filterPtr
         *            过滤的起始地址(单位:bit)<br>
         * @param filterCnt
         *            过滤的数据长度(单位:bit)<br>
         * @param filterData
         *            过滤的数据<br>
         * @param bank
         *            写入的数据块<br>
         * @param ptr
         *            写入的起始地址(单位:字)<br>
         * @param cnt
         *            写入的数据长度(单位:字)<br>
         * @param writeData
         *            写入的数据
         *            
         * @return    发送的数据
         * 
         */
        byte[] getWriteSendData(string accessPwd,
               int filterBank, int filterPtr, int filterCnt,
               string filterData, int bank, int ptr, int cnt, string writeData);

        /**
         * 获取读标签的发送数据
         * 
         * @param accessPwd
         *            访问密码<br>
         * @param filterBank
         *            过滤的数据块<br>
         * @param filterPtr
         *            过滤的起始地址(单位:bit)<br>
         * @param filterCnt
         *            过滤的数据长度(单位:bit)<br>
         * @param filterData
         *            过滤的数据<br>
         * @param bank
         *            读取的数据块<br>
         * @param ptr
         *            读取的起始地址(单位:字)<br>
         * @param cnt
         *            读取的数据长度(单位:字)<br>
         * 
         * @return 发送的数据<br>
         */
        byte[] getReadSendData(string accessPwd, int filterBank,
               int filterPtr, int filterCnt, string filterData, int bank,
               int ptr, int cnt);

        /**
         * 获取锁标签需要发送的数据 <br>
         * 
         * @param accessPwd
         *            标签的ACCESS PASSWORD（4字 节）<br>
         * @param filterBank
         *            标签的存储区<br>
         * @param filterPtr
         *            过滤起始地址(单位:bit)<br>
         * @param filterCnt
         *            过滤数据长度(单位:bit)<br>
         * @param filterData
         *            过滤数据<br>
         * @param lockCode
         *            锁定码<br>
         * @return 发送的数据<br>
         * 
         */
        byte[] getLockSendData(string accessPwd, int filterBank,
               int filterPtr, int filterCnt, string filterData, string lockCode);

        /**
         * 获取锁标签的锁定码
         * @param lockBank 要锁定的区域
         * @param lockMode 锁定的模式
         * @return 返回null 表示失败
         */
        string generateLockCode(List<int> lockBank, int lockMode);

        /**
         * 获取销毁标签的发送数据
         * 
         * @param accessPwd
         *            标签的ACCESS PASSWORD（4字 节）<br>
         * @param filterBank
         *            标签的存储区<br>
         * @param filterPtr
         *            过滤起始地址(单位:bit)<br>
         * @param filterCnt
         *            过滤数据长度(单位:bit)<br>
         * @param filterData
         *            过滤数据<br>
         *            
         * @return  发送的数据<br>
         */
        byte[] getKillSendData(string accessPwd, int filterBank, int filterPtr, int filterCnt, string filterData);


        /**
         * 
         * 获取协议需要发送数据<br>
         * 
         * @return  发送的数据 
         * 
         */
        byte[] getProtocolSendData();

        /**
         * 
         * 获取设置协议的发送数据<br>
         * @param protocol
         *            0x00 表示 ISO18000-6C 协议,  0x01 表示 GB/T 29768 国标协议,  0x02 表示 GJB 7377.1 国军标协议
         * @return  发送的数据 
         * 
         */
        byte[] setProtocolSendData(int protocol);

        /**
         * 
         * 获取功率的发送数据<br>
         * 
         * @return 发送的数据 
         */
        byte[] getPowerSendData();

        /**
         * 获取设置功率的发送数据<br>
         * 
         * @param power 功率
         * @return  发送的数据 
         */
        byte[] setPowerSendData(int power,bool sava);

        /**
         * 获取开启单次盘点标签的发送数据
         * 
         * @return 发送的数据 
         */
        byte[] getInventorySingleTagSendData();

        /**
         * 获取循环盘点标签的发送数据
         * 
         * @return 发送的数据 
         */
        byte[] getStartInventoryTagSendData();

        /**
         * 获取停止循环盘点标签的发送数据
         * 
         * @return 发送的数据 
         */
        byte[] getStopInventorySendData();

        /**
         *获取在循环盘点标签的模式中,读取缓存标签的发送数据
         * 
         * @return 发送的数据 
         */
        byte[] getReadTagSendData();

        /// <summary>
        /// 获取R2、R6缓存数据数量
        /// </summary>
        /// <returns></returns>
        byte[] getAllTagNumFromFlashSendData();

        /// <summary>
        /// 解析R2 R6获取数量返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        int parseTagNumFromFlashData(byte[] inData);

        /// <summary>
        /// 删除flash缓存的标签
        /// </summary>
        /// <returns></returns>      
        byte[] getDeleteAllTagToFlashSendData();

        /// <summary>
        /// 解析删除flash,标签返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        bool parseDeleteAllTagToFlashData(byte[] inData);

        /// <summary>
        /// 获取flash缓存的标签在发送数据
        /// </summary>
        /// <returns></returns>
        byte[] getTagDataFromFlashSendData();

        /// <summary>
        /// 解析读取flash 缓存返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        List<UHFTAGInfo> parseTagDataFromFlashData(byte[] inData);

        byte[] setFrequencyModeSendData(byte save, int freMode);
        bool parseSetFrequencyModeData(byte[] inData);

        byte[] getFrequencyModeSendData();
        int parseGetFrequencyModeData(byte[] inData);

        List<UHFTAGInfo> parserUhfTagBuff_EpcTidUser(byte[] tagsBuff);


        /// <summary>
        /// 获取蜂鸣器状态发送的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        byte[] getBeepStatusSendData();
        /// <summary>
        /// 解析获取蜂鸣器返回的数据
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        int parseGetBeepStatusData(byte[] inData);

        /// <summary>
        /// 获取模块温度
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        int parseTemperatureData(byte[] inData);
        byte[] getTemperatureSendData();

        /// <summary>
        /// 硬件版本
        /// </summary>
        /// <returns></returns>
        byte[] getHardwareVersionSendData();
        string parseHardwareVersionData(byte[] inData);

        /// <summary>
        /// 软件版本
        /// </summary>
        /// <returns></returns>
        byte[] getSoftwareVersionSendData();
        string parseSoftwareVersionData(byte[] inData);

        /// <summary>
        /// 模块id
        /// </summary>
        /// <returns></returns>
        byte[] getDeviceIDSendData();
        int parseDeviceIDData(byte[] inData);

        //盘点过滤
        byte[] setFilterSendData(byte saveflag, byte bank, int startaddr, int datalen, byte[] databuf);
        bool parseFilterData(byte[] inData);

        //Gen2
        byte[] setGen2SendData(byte Target, byte Action, byte T, byte Q, byte StartQ, byte MinQ, byte MaxQ, byte D, byte C, byte P, byte Sel, byte Session, byte G, byte LF);
        bool parseSetGen2Data(byte[] inData);

        byte[] getGen2SendData();
        bool parseGetGen2Data(byte[] inData, int inLen, ref byte Target, ref byte Action, ref byte T, ref byte Q,
              ref byte StartQ, ref byte MinQ,
              ref byte MaxQ, ref byte D, ref byte Coding, ref byte P,
              ref byte Sel, ref byte Session, ref byte G, ref byte LF);

        #region 天线
        byte[] setANTSendData(byte saveflag, byte[] buf);
        bool parseSetANTDData(byte[] inData);

        byte[] getANTSendData();
        byte[] parseGetANTDData(byte[] inData);
        //--------
        byte[] getANTWorkTimeSendData(byte antnum);
        int parseGetANTWorkTimeData(byte[] inData);

        byte[] setANTWorkTimeSendData(byte antnum, byte saveflag, int WorkTime);
        bool parseSetANTWorkTimeData(byte[] inData);
        #endregion

        #region 链路
        byte[] setRFLinkSendData(byte saveflag, byte mode);
        byte[] getRFLinkSendData();
        int parseGetRFLinkData(byte[] inData);
        bool parseSetRFLinkData(byte[] inData);

        #endregion

        #region EPC+TID
        //byte[] setEPCTIDModeSendData(byte saveflag, byte mode);
        //bool parseSetEPCTIDModeData(byte[] inData);
        //byte[] getEPCTIDModeSendData();
        //int parseGetEPCTIDModeData(byte[] inData);
        #endregion

        #region IP
        byte[] setDestIpSendData(byte[] ipbuf, byte[] postbuff);
        byte[] getIpSendData();
        bool parseGetIpData(byte[] inData, byte[] ipbuf, byte[] postbuff);
        bool parseSetIpData(byte[] inData);
        byte[] setIpSendData(byte[] ipbuf, byte[] postbuff);
        byte[] getDestIpSendData();
        bool parseGetDestIpData(byte[] inData, byte[] ipbuf, byte[] postbuff);
        bool parseSetDestIpData(byte[] inData);
        #endregion

        #region focus
        byte[] setTagfocusSendData(byte flag);
        bool parseSetTagfocusData(byte[] inData);
        byte[] getTagfocusSendData();
        int parseGetTagfocusData(byte[] inData);
        #endregion


        #region  FastID
        byte[] setFastIDSendData(byte flag);
        bool parseSetFastIDData(byte[] inData);

        byte[] getFastIDSendData();
        int parseGetFastIDData(byte[] inData);
        #endregion

        #region cw 连续波
        byte[] setCWSendData(byte flag);
        bool parseSetCWData(byte[] inData);

        byte[] getCWSendData();
        int parseGetCWData(byte[] inData);
        #endregion

        #region  工作模式 (命令工作模式、自动模式)

        byte[] setSetWorkModeSendData(byte mode);
        bool parseSetWorkModeData(byte[] inData);

        byte[] getWorkModeSendData();
        int parseGetWorkModeData(byte[] inData);

        #endregion

        #region 复位

        byte[] setSoftResetSendData();
        bool parseSetSoftResetData(byte[] inData);

        #endregion


        #region gpio、继电器

         byte[] setIOControlSendData(byte output1, byte output2, byte outStatus);
         bool parseSetIOControlData(byte[] inData);
 
         byte[] getIOControlSendData();
         byte[] UHFGetIOControl_RecvData(byte[] inData);
        #endregion

        #region BlockPermalock
         byte[] getBlockPermalockSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte ReadLock, byte uBank, int uPtr, byte uRange, byte[] uMaskbuf);
         bool parseBlockPermalocData(byte uRange, byte[] inData, byte[] uMaskbuf);
        #endregion

        #region 擦除
         byte[] blockEraseDataSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte  uBank, int uPtr, byte uCnt);
         bool parseBlockEraseData(byte[] inData);

         byte[] blockWriteDataSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, int uCnt, byte[] uWriteDatabuf);
         bool parseBlockWriteData(byte[] inData); 
        #endregion

         #region QT

         byte[] setQTSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData,byte QTData);
         bool parseSetQTData(byte[] inData);


         byte[] getQTSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData);
         int parseGetQTData(byte[] inData);


        #endregion

         byte[] getGBTagLockSendData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte memory, byte config, byte action);
         bool parseGBTagLockData(byte[] inData);


       
        byte[] setJumpFrequencySendData(byte nums, int[] Freqbuf);
        bool parseSetJumpFrequencyData(byte[] inData);
        byte[] getJumpFrequencySendData();
        int[] parseGetJumpFrequencyData(byte[] inData);

        byte[] setEpcTidUserModeSendData(byte saveFlag, byte memory, byte userAddress, byte useLen);
        bool parseSetEpcTidUserModeyData(byte[] inData);

        byte[] getEpcTidUserModeSendData();
        bool parseGetEpcTidUserModeyData(byte[] inData, ref int mode, ref byte userAddress, ref byte useLen);


        #region
        byte[] jump2BootSendData(int flag);
        bool parseJump2BootData(byte[] inData);

        byte[] startUpdSendData();
        bool parseStartUpdData(byte[] inData);

        byte[] updatingSendData(byte[] data);
        bool parseUpdatingData(byte[] inData);
        
        byte[] stopUpdateSendData();
        bool parseStopUpdateData(byte[] inData);
        #endregion
    }
}

