using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UHFAPP.interfaces;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Threading;

namespace UHFAPP.Receive
{
    class UdpReceive:IAutoReceive
    {
        public delegate void ReceiveData(byte[] data);
        public ReceiveData ReceiveDataDelegate = null; 

        private IPEndPoint remote = null;
        private UdpClient ReceiveUdpClient = null;
        private bool isOpen = false;
        private string MyIPAddress = "";
        private int PortName = 0;

        private Thread threadReceiveOriginalData=null;

        public void SetIP(string MyIPAddress, int PortName)
        {
            this.MyIPAddress = MyIPAddress;
            this.PortName = PortName;
        }

        public bool Connect()
        {
            if (MyIPAddress == "" || PortName == 0)
                return false;

            try
            {
                IPEndPoint local = new IPEndPoint(IPAddress.Parse(MyIPAddress), PortName);
                ReceiveUdpClient = new UdpClient(local);
                remote = new IPEndPoint(IPAddress.Any, 0);
                isOpen = true;
                if (threadReceiveOriginalData == null)
                {
                    threadReceiveOriginalData = new Thread(new ThreadStart(ReceiveOriginalData));
                    threadReceiveOriginalData.IsBackground = true;
                    threadReceiveOriginalData.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public void DisConnect()
        {
            if (ReceiveUdpClient != null)
            {
                try
                {
                    isOpen = false;
                    threadReceiveOriginalData.Interrupt();
                    threadReceiveOriginalData = null;
                    ReceiveUdpClient.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }

        

        //获取epc
        private void ReceiveOriginalData()
        {
            try
            {
                while (isOpen)
                {
                    if (isOpen)
                    {
                        try
                        {
                            // 接收
                            byte[] receiveBytes = ReceiveUdpClient.Receive(ref remote);//Receive the original 
                            if (receiveBytes != null)
                            {
                                if (ReceiveDataDelegate != null)
                                {
                                    ReceiveDataDelegate(receiveBytes);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        
        public IPEndPoint GetRemoteIP() {
            return remote;
        }

    }
}
