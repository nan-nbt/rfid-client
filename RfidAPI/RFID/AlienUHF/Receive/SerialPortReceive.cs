using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UHFAPP.interfaces;
using System.IO.Ports;

namespace UHFAPP.Receive
{
    class SerialPortReceive : IAutoReceive
    {
        public delegate void ReceiveData(byte[] data);
        public ReceiveData ReceiveDataDelegate = null; 

        private SerialPort serialPort = null;
        public SerialPortReceive()
            : this("COM1", 115200)
        {
        
        }
        public SerialPortReceive(string PortName)
            : this(PortName, 115200)
        {
      
        }
        public SerialPortReceive(string PortName, int BaudRate)
        {
            if (serialPort == null)
            {
                serialPort = new SerialPort(PortName, BaudRate);
                serialPort.DataReceived += SerialDataReceivedEvent;
            }
        }

        public void SetPortName(string PortName)
        {
            if (serialPort != null)
                serialPort.PortName = PortName;
        }
        public void SetBaudRate(int BaudRate)
        {
            if (serialPort != null)
                serialPort.BaudRate = BaudRate;
        }

        public void SetPortNameAndBaudRate(string PortName, int BaudRate)
        {
            if (serialPort != null)
            {
                serialPort.PortName = PortName;
                serialPort.BaudRate = BaudRate;
            }
        }
        public   bool Connect()
        {
            try
            {
                if (serialPort != null && !serialPort.IsOpen)
                {
                    serialPort.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }

        public   void DisConnect()
        {
            if (serialPort != null)
                serialPort.Close();
        }

        public void SerialDataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort=sender as SerialPort;
            if (serialPort != null)
            {
               int len= serialPort.BytesToRead;
               if (len > 0)
               {
                   byte[] data = new byte[len];
                   len = serialPort.Read(data, 0, len);
                   if (len > 0)
                   {
                       if (ReceiveDataDelegate != null) 
                       {
                           if (len >= data.Length)
                           {
                               ReceiveDataDelegate(data);
                           }
                           else
                           {
                               byte[] temp = new byte[len];
                               Array.Copy(data, 0, temp,0,len);
                               ReceiveDataDelegate(temp);
                           }

                       }
                   }
               }
            }
        }

        
    }
}
