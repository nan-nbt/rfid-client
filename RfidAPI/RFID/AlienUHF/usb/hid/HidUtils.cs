using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HID_SIMPLE.HID
{
    public class HidUtils
    {
        #region parameter Define
        private HIDInterface hid = new HIDInterface();


        struct connectStatusStruct
        {
            public bool preStatus;
            public bool curStatus;
        }

        private connectStatusStruct connectStatus = new connectStatusStruct();

        //推送连接状态信息
        public delegate void isConnectedDelegate(bool isConnected);
        public isConnectedDelegate isConnectedFunc;


        //推送接收数据信息
        public delegate void PushReceiveDataDele(byte[] datas);
        public PushReceiveDataDele pushReceiveData;

        #endregion

        //第一步需要初始化，传入vid、pid，并开启自动连接
        public bool Initial(int vID, int pID)
        {
            hid.StatusConnected = StatusConnected;
            hid.DataReceived = DataReceived;

            HIDInterface.HidDevice hidDevice = new HIDInterface.HidDevice();
            hidDevice.vID = (UInt16)vID;
            hidDevice.pID = (UInt16)pID;
            hidDevice.serial = "";
            return hid.Connect(hidDevice);
        }

        //不使用则关闭
        public void Close()
        {
            hid.StopAutoConnect();
        }

        //发送数据
        public bool SendBytes(byte[] data)
        {
            
            return hid.Send(data);
            
        }

        //接受到数据
        private void DataReceived(object sender, byte[] e)
        {
            if (pushReceiveData != null)
                pushReceiveData(e);
        }

        //状态改变接收
        private void StatusConnected(object sender, bool isConnect)
        {
            connectStatus.curStatus = isConnect;
            if (connectStatus.curStatus == connectStatus.preStatus)  //connect
                return;
            connectStatus.preStatus = connectStatus.curStatus;

            if(connectStatus.curStatus)
            {
               // isConnectedFunc(true);
                //ReportMessage(MessagesType.Message, "连接成功");
            }
            else //disconnect
            {
               // isConnectedFunc(false);
                //ReportMessage(MessagesType.Error, "无法连接");
            }
        }

    }
}
