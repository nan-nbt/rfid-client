using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLEDeviceAPI;

namespace UHFAPP
{
    public interface IUHF
    {


        #region 协议
        /// <summary>
        /// 设置协议类型
        /// </summary>
        /// <param name="type">type  0x00 表示 ISO18000-6C 协议,0x01 表示 GB/T 29768 国标协议,0x02 表示 GJB 7377.1 国军标协议</param>
        /// <returns></returns>
        bool SetProtocol(byte type);
        /// <summary>
        /// 获取协议类型
        /// </summary>
        /// <returns></returns>
        int GetProtocol();
        #endregion


        #region  国标标签Lock
        /// 
        /// <summary>
        /// 国标标签Lock
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：bit</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="memory"></param>
        /// <param name="config"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool GBTagLock(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte memory, byte config, byte action);

        #endregion



        #region TCPIP



        //设置蜂鸣器 工作模式：0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器
        bool UHFSetBuzzer(byte mode);
        //获取蜂鸣器 工作模式：0x00表示关闭蜂鸣器；0x01表示打开蜂鸣器
        bool UHFGetBuzzer(byte[] mode);



        bool SetLocalIP(string ip, int port, string mask, string gate);

        bool GetLocalIP(StringBuilder ip, StringBuilder port, StringBuilder mask, StringBuilder gate);


        bool SetDestIP(string ip, int port);

        bool GetDestIP(StringBuilder ip, StringBuilder port);


        #endregion

        #region 串口、版本号、ID
        /// <summary>
        /// 打开串口，或者usb
        /// </summary>
        /// <returns></returns>
        bool Open();
        /// <summary>
        /// 关闭串口，或者usb
        /// </summary>
        /// <returns></returns>
        bool Close();
        /// <summary>
        /// 获取硬件版本
        /// </summary>
        /// <returns></returns>
        string GetHardwareVersion();
        /// <summary>
        /// 获取软件版本
        /// </summary>
        /// <returns></returns>
        string GetSoftwareVersion();
        /// <summary>
        /// 获取设备ID
        /// </summary>
        /// <returns>id--整型ID号</returns>
        int GetUHFGetDeviceID();
        #endregion

        #region 频率、功率
        /// <summary>
        /// 设置功率
        /// </summary>
        /// <param name="save">1:保存设置   0：不保存</param>
        /// <param name="uPower">功率（DB）</param>
        /// <returns></returns>
        bool SetPower(byte save, byte uPower);
        /// <summary>
        /// 获取功率
        /// </summary>
        /// <param name="uPower">功率（DB）</param>
        /// <returns></returns>
        bool GetPower(ref byte uPower);
        /// <summary>
        /// 获取跳频
        /// </summary>
        /// <param name="Freqbuf">Freqbuf[0]--频点个数， Freqbuf[1]..[x]--频点数组（整型）</param>
        /// <returns></returns>
        bool GetJumpFrequency(ref int[] Freqbuf);
        /// <summary>
        /// 设置跳频
        /// </summary>
        /// <param name="nums">跳频个数</param>
        /// <param name="Freqbuf">Freqbuf--频点数组（整型） ，如920125，921250.....</param>
        /// <returns></returns>
        bool SetJumpFrequency(byte nums, int[] Freqbuf);
        #endregion

        #region session
        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="Action"></param>
        /// <param name="T"></param>
        /// <param name="Q"></param>
        /// <param name="StartQ"></param>
        /// <param name="MinQ"></param>
        /// <param name="MaxQ"></param>
        /// <param name="D"></param>
        /// <param name="C"></param>
        /// <param name="P"></param>
        /// <param name="Sel"></param>
        /// <param name="Session"></param>
        /// <param name="G"></param>
        /// <param name="LF"></param>
        /// <returns></returns>
        bool SetGen2(byte Target, byte Action, byte T, byte Q,
                             byte StartQ, byte MinQ,
                             byte MaxQ, byte D, byte C, byte P,
                             byte Sel, byte Session, byte G, byte LF);
        /// <summary>
        /// 获取session
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="Action"></param>
        /// <param name="T"></param>
        /// <param name="Q"></param>
        /// <param name="StartQ"></param>
        /// <param name="MinQ"></param>
        /// <param name="MaxQ"></param>
        /// <param name="D"></param>
        /// <param name="Coding"></param>
        /// <param name="P"></param>
        /// <param name="Sel"></param>
        /// <param name="Session"></param>
        /// <param name="G"></param>
        /// <param name="LF"></param>
        /// <returns></returns>
        bool GetGen2(ref byte Target, ref byte Action, ref byte T, ref byte Q,
                  ref byte StartQ, ref byte MinQ,
                  ref byte MaxQ, ref byte D, ref byte Coding, ref byte P,
                  ref byte Sel, ref byte Session, ref byte G, ref byte LF);
        #endregion

        /// <summary>
        /// 设置CW
        /// </summary>
        /// <param name="flag">flag -- 1:开CW，  0：关CW</param>
        /// <returns></returns>
        bool SetCW(byte flag);
        /// <summary>
        /// 功能：获取CW
        /// </summary>
        /// <param name="flag">flag -- 1:开CW，  0：关CW</param>
        /// <returns></returns>
        bool GetCW(ref byte flag);
        /// <summary>
        /// 天线设置
        /// </summary>
        /// <param name="saveflag">saveflag -- 1:掉电保存，  0：不保存</param>
        /// <param name="buf">buf--2bytes, 共16bits, 每bit 置1选择对应天线</param>
        /// <returns></returns>
        bool SetANT(byte saveflag, byte[] buf);
        /// <summary>
        /// 获取天线设置
        /// </summary>
        /// <param name="buf">buf--2bytes, 共16bits</param>
        /// <returns></returns>
        bool GetANT(byte[] buf);
        /// <summary>
        /// 区域设置
        /// </summary>
        /// <param name="saveflag">1:掉电保存，  0：不保存</param>
        /// <param name="region">0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)</param>
        /// <returns></returns>
        bool SetRegion(byte saveflag, byte region);
        /// <summary>
        /// 获取区域设置
        /// </summary>
        /// <param name="region"> 0x01(China1),0x02(China2),0x04(Europe),0x08(USA),0x16(Korea),0x32(Japan)</param>
        /// <returns></returns>
        bool GetRegion(ref byte region);
        /// <summary>
        /// 获取模块温度
        /// </summary>
        /// <param name="temperature">回传的温度</param>
        /// <returns>返回true表示获取成功，temperature参数可以使用。返回false表示获取失败，temperature参数不可以使用</returns>
        string GetTemperature();
        /// <summary>
        /// 设置温度保护
        /// </summary>
        /// <param name="flag">1:温度保护， 0：无温度保护</param>
        /// <returns></returns>
        bool SetTemperatureProtect(byte flag);
        /// <summary>
        /// 获取温度保护
        /// </summary>
        /// <param name="flag">1:温度保护， 0：无温度保护</param>
        /// <returns></returns>
        bool GetTemperatureProtect(ref byte flag);
        /// <summary>
        /// 设置天线工作时间
        /// </summary>
        /// <param name="antnum">天线号</param>
        /// <param name="saveflag">1:掉电保存， 0：不保存</param>
        /// <param name="WorkTime">工作时间 ，单位ms, 范围 10-65535ms</param>
        /// <returns></returns>
        bool SetANTWorkTime(byte antnum, byte saveflag, int WorkTime);
        /// <summary>
        /// 获取天线工作时间
        /// </summary>
        /// <param name="antnum">天线号</param>
        /// <param name="WorkTime">工作时间 ，单位ms, 范围 10-65535ms</param>
        /// <returns></returns>
        bool GetANTWorkTime(byte antnum, ref int WorkTime);
        /// <summary>
        /// 设置链路组合
        /// </summary>
        /// <param name="saveflag">1:掉电保存， 0：不保存</param>
        /// <param name="mode">0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ</param>
        /// <returns></returns>
        bool SetRFLink(byte saveflag, byte mode);
        /// <summary>
        /// 获取链路组合
        /// </summary>
        /// <param name="uMode">0:DSB_ASK/FM0/40KHZ , 1:PR_ASK/Miller4/250KHZ , 2:PR_ASK/Miller4/300KHZ, 3:DSB_ASK/FM0/400KHZ</param>
        /// <returns></returns>
        bool GetRFLink(ref byte uMode);
        /// <summary>
        /// 设置FastID功能
        /// </summary>
        /// <param name="flag">1:开启， 0：关闭</param>
        /// <returns></returns>
        bool SetFastID(byte flag);
        /// <summary>
        /// 获取FastID功能
        /// </summary>
        /// <param name="flag">1:开启， 0：关闭</param>
        /// <returns></returns>
        bool GetFastID(ref byte flag);
        /// <summary>
        /// 设置Tagfocus功能
        /// </summary>
        /// <param name="flag">1:开启， 0：关闭</param>
        /// <returns></returns>
        bool SetTagfocus(byte flag);
        /// <summary>
        /// 获取Tagfocus功能
        /// </summary>
        /// <param name="flag">1:开启， 0：关闭</param>
        /// <returns></returns>
        bool GetTagfocus(ref byte flag);
        /// <summary>
        /// 设置软件复位
        /// </summary>
        /// <returns></returns>
        bool SetSoftReset();
        /// <summary>
        /// 设置Dual和Singel模式
        /// </summary>
        /// <param name="saveflag">1:掉电保存， 0：不保存</param>
        /// <param name="mode">1:设置Singel模式， 0：设置Dual模式</param>
        /// <returns></returns>
        bool SetDualSingelMode(byte saveflag, byte mode);
        /// <summary>
        /// 获取Dual和Singel模式
        /// </summary>
        /// <param name="mode">1:设置Singel模式， 0：设置Dual模式</param>
        /// <returns></returns>
        bool GetDualSingelMode(ref byte mode);
        /// <summary>
        /// 设置寻标签过滤设置
        /// </summary>
        /// <param name="saveflag">1:掉电保存， 0：不保存</param>
        /// <param name="bank">0x01:EPC , 0x02:TID, 0x03:USR</param>
        /// <param name="startaddr">起始地址，单位：字节</param>
        /// <param name="datalen">数据长度， 单位:字节</param>
        /// <param name="databuf">数据</param>
        /// <returns></returns>
        bool SetFilter(byte saveflag, byte bank, int startaddr, int datalen, byte[] databuf);
        /// <summary>
        /// 设置EPC和TID模式
        /// </summary>
        /// <param name="saveflag">1:掉电保存， 0：不保存</param>
        /// <param name="mode">1：开启EPC和TID， 0:关闭</param>
        /// <returns></returns>
        // bool SetEPCTIDMode(byte saveflag, byte mode);
        /// <summary>
        /// 获取EPC和TID模式
        /// </summary>
        /// <param name="mode">1：开启EPC和TID， 0:关闭</param>
        /// <returns></returns>
        // bool GetEPCTIDMode(ref byte mode);
        /// <summary>
        /// 恢复出厂设置
        /// </summary>
        /// <returns></returns>
        bool SetDefaultMode();
        /// <summary>
        /// 单次盘存标签
        /// </summary>
        /// <param name="uLenUii">UII长度</param>
        /// <param name="uUii">UII数据</param>
        /// <returns></returns>
        bool InventorySingle(ref byte uLenUii, ref byte[] uUii);
        /// <summary>
        /// 连续盘存标签
        /// </summary>
        /// <returns></returns>
        bool Inventory();
        /// <summary>
        /// 停止盘存标签
        /// </summary>
        /// <returns></returns>
        bool StopGet();
        /// <summary>
        /// 获取连续盘存标签数据
        /// </summary>
        /// <param name="uLenUii">UII长度</param>
        /// <param name="uUii">UII数据</param>
        /// <returns></returns>
        bool GetReceived_EX(ref int uLenUii, ref byte[] uUii);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="uBank">读取数据的bank</param>
        /// <param name="uPtr">读取数据的起始地址， 单位：字</param>
        /// <param name="uCnt">读取数据的长度， 单位：字</param>
        /// <returns>返回十六进制数据，读取失败返回""</returns>
        string ReadData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, int uCnt);


        /// <summary>
        /// 写标签数据区
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：bit</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：bit</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="uBank">写入数据的bank</param>
        /// <param name="uPtr">写入数据的起始地址， 单位：字</param>
        /// <param name="uCnt">写入数据的长度， 单位：字</param>
        /// <param name="uDatabuf">写入的数据</param>
        /// <returns></returns>
        bool WriteData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, byte uCnt, byte[] uDatabuf)
     ;
        /// <summary>
        /// LOCK标签
        /// </summary>
        /// <param name="uAccessPwd"> 4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="lockbuf">3字节，第0-9位为Action位， 第10-19位为Mask位</param>
        /// <returns></returns>
        bool LockTag(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte[] lockbuf);
        /// <summary>
        ///  KILL标签
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <returns></returns>
        bool KillTag(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData);
        /// <summary>
        /// BlockWrite 特定长度的数据到标签的特定地址
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
        /// <param name="uPtr">读取数据的起始地址， 单位：字</param>
        /// <param name="uCnt">读取数据的长度， 单位：字</param>
        /// <param name="uDatabuf">写入的数据</param>
        /// <returns></returns>
        bool BlockWriteData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, int uCnt, byte[] uDatabuf);
        /// <summary>
        /// BlockErase 特定长度的数据到标签的特定地址
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
        /// <param name="uPtr">读取数据的起始地址， 单位：字</param>
        /// <param name="uCnt">读取数据的长度， 单位：字</param>
        /// <returns></returns>
        bool BlockEraseData(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte uBank, int uPtr, byte uCnt);
        #region QT 相关
        /// <summary>
        /// 设置QT命令参数
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用 memory map)</param>
        /// <returns></returns>
        bool SetQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte QTData)
      ;
        /// <summary>
        /// 获取QT命令参数
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  bit1：(0:表示启用private Memory map, 1：启用 memory map)</param>
        /// <returns></returns>
        bool GetQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, ref byte QTData);
        /// <summary>
        /// QT标签读操作
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制）  </param>
        /// <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
        /// <param name="uPtr">读取数据的起始地址， 单位：字</param>
        /// <param name="uCnt">读取数据的长度， 单位：字</param>
        /// <param name="uReadDatabuf">读出的数据</param>
        /// <param name="uReadDataLen">读出的数据长度</param>
        /// <returns></returns>
        string ReadQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte QTData, byte uBank, int uPtr, byte uCnt)
    ;
        /// <summary>
        ///   QT标签写操作
        /// </summary>
        /// <param name="uAccessPwd"> 4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr"> 启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="QTData">bit0：（0：表示无近距离控制 ， 1：表示启用近距离控制） </param>
        /// <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
        /// <param name="uPtr">读取数据的起始地址， 单位：字</param>
        /// <param name="uCnt">读取数据的长度， 单位：字</param>
        /// <param name="uDatabuf">写入的数据</param>
        /// <returns></returns>
        bool WriteQT(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte QTData, byte uBank, int uPtr, byte uCnt, byte[] uDatabuf)
       ;
        #endregion
        /// <summary>
        /// Block Permalock操作
        /// </summary>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：字节</param>
        /// <param name="FilterLen">启动过滤的长度， 单位：字节</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <param name="ReadLock">bit0：（0：表示Read ， 1：表示Permalock）  </param>
        /// <param name="uBank">块号  1：EPC, 2:TID, 3:USR</param>
        /// <param name="uPtr">Block起始地址 ，单位为16个块</param>
        /// <param name="uRange">Block范围，单位为16个块</param>
        /// <param name="uMaskbuf">块掩码数据，2个字节，16bit 对应16个块</param>
        /// <returns></returns>
        bool BlockPermalock(byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData, byte ReadLock, byte uBank, int uPtr, byte uRange, byte[] uMaskbuf)
      ;

        //读取epc
        bool uhfGetReceived(ref string epc, ref string tid, ref string rssi, ref string ant);
        UHFTAGInfo uhfGetReceived();
        bool InventorySingle(ref string epc);


        /// <summary>
        /// 激活或失效EM4124标签
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="uAccessPwd">4字节密码</param>
        /// <param name="FilterBank">启动过滤的bank号， 1：EPC, 2:TID, 3:USR</param>
        /// <param name="FilterStartaddr">启动过滤的起始地址， 单位：bit</param>
        /// <param name="FilterLen">启动过滤的数据长度</param>
        /// <param name="FilterData">启动过滤的数据</param>
        /// <returns></returns>
        bool Deactivate(int cmd, byte[] uAccessPwd, byte FilterBank, int FilterStartaddr, int FilterLen, byte[] FilterData);

        bool SetWorkMode(byte mode);
        bool GetWorkMode(byte[] mode);

        /// <summary>
        /// 设置温度过热保护
        /// </summary>
        /// <param name="tempVal">50-75</param>
        /// <returns></returns>
        bool SetTemperatureVal(byte tempVal);

        /// <summary>
        /// 获取温度过热保护值
        /// </summary>
        /// <returns></returns>
        int GetTemperatureVal();

        /// <summary>
        /// 获取gpio输入
        /// </summary>
        /// <param name="outData">
        ///       outData[0]:    0:低电平   1：高电平
        ///       outData[1]:    0:低电平   1：高电平
        /// 
        /// </param>
        /// <returns></returns>
        bool getIOControl(byte[] outData);
        /// <summary>
        /// 设置gpio输出
        /// </summary>
        /// <param name="ouput1">0:低电平   1：高电平</param>
        /// <param name="ouput2">0:低电平   1：高电平</param>
        /// <param name="outStatus">继电器 0：断开    1：闭合</param>
        /// <returns></returns>
        bool setIOControl(byte ouput1, byte ouput2, byte outStatus);


        /// <summary>
        /// 设置工作时间和等待時間
        /// </summary>
        /// <param name="workTime">工作時間</param>
        /// <param name="waitTime">等待時間</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        bool setWorkAndWaitTime(int workTime, int waitTime, bool isSave);
        /// <summary>
        /// 獲取工作时间和等待時間
        /// </summary>
        /// <param name="workTime">工作時間</param>
        /// <param name="waitTime">等待時間</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        bool getWorkAndWaitTime(out int workTime, out int waitTime);


        /// <summary>
        /// 设置EPC模式
        /// </summary>
        /// <returns></returns>
        bool setEPCMode();
        /// <summary>
        /// 设置EPC 和TID 模式
        /// </summary>
        /// <returns></returns>
        bool setEPCAndTIDMode();
        /// <summary>
        /// 设置EPC+TID+User模式
        /// </summary>
        /// <param name="userAddress">user区域起始地址</param>
        /// <param name="userLenth">user区长度</param>
        /// <returns></returns>
        bool setEPCAndTIDUSERMode(byte userAddress, byte userLenth);
        /// <summary>
        /// 获取盘点模式
        /// </summary>
        /// <param name="userAddress">user区域起始地址</param>
        /// <param name="userLenth">user区长度</param>
        /// <returns>  0x00:EPC模式;  0x01:EPC+TID模式;  0x02:EPC+TID+USER模式</returns>
        int getEPCTIDUSERMode(ref byte userAddress, ref byte userLenth);

        /// <summary>
        /// 升级固件
        /// </summary>
        /// <param name="flag">1: R2000， 0:STM32  </param>
        /// <returns></returns>
        bool jump2Boot(byte flag);
        bool startUpd();
        bool updating(byte[] buf, int len);
        bool stopUpdate();

        string GetSTM32Version();

    }
}
