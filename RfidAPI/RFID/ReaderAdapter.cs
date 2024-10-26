using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using Microsoft.Win32;
using RFID.Command;
using RFID.Reader;

namespace RFID.Adapter
{
    public class ReaderAdapter
    {
        private int _readerType;
        public IReader iReader;
        public ReaderAdapter(ReaderType type)
        {
            _readerType = Convert.ToInt32(type);
            switch (type)
            {
                case ReaderType.ALIEN:
                    iReader = new Alien();
                    break;
                case ReaderType.ALIEN_UHF:
                    iReader = new AlienUhf();
                    break;
            }
        }

        public bool OpenConnect()
        {
            return iReader.OpenConnect(_readerType);
        }

        public bool Disconnect()
        {
            return iReader.Disconnect();
        }

        public int GetSuccQty() { return iReader.GetSuccQty(); }

        public bool IsConnected()
        {
            return iReader.IsConnected();
        }

        public bool SetReaderBySingle(ParameterNm commmandNm, Parameters classAttr)
        {
            try
            {
                return iReader.SetReaderBySingle(commmandNm, classAttr);
            }
            catch
            {
                throw;
            }
        }

        public bool SetReaderBySingle(ParameterNm commmandNm, string strAttr)
        {
            try
            {
                return iReader.SetReaderBySingle(commmandNm, strAttr);
            }
            catch
            {
                throw;
            }
        }

        public bool SetReaderBySingle(ParameterNm commmandNm, int iAttr)
        {
            try
            {
                return iReader.SetReaderBySingle(commmandNm, iAttr);
            }
            catch
            {
                throw;
            }
        }

        public bool SetReaderBySingle(ParameterNm commmandNm)
        {
            try
            {
                return iReader.SetReaderBySingle(commmandNm);
            }
            catch
            {
                throw;
            }
        }

        public String ReadTagByBank(int iBank, string wordPtr, string wordLen)
        {
            try
            {
                return iReader.ReadTagByBank(iBank, wordPtr, wordLen);
            }
            catch
            {
                throw;
            }
        }

        public List<TagStruct> GetTagList()
        {
            try
            {
                return iReader.GetTagList();
            }
            catch
            {
                throw;
            }
        }

        public void ClearTagList()
        {
            try
            {
                iReader.ClearTagList();
            }
            catch { throw; }
        }

        public void AddMessageReceived(ReceiveMsg oReceiveMsg)
        {
            try
            {
                iReader.AddMessageReceived(oReceiveMsg);
            }
            catch
            {
                throw;
            }
        }

        public void RemoveMessageReceived(ReceiveMsg oReceiveMsg)
        {
            try
            {
                iReader.RemoveMessageReceived(oReceiveMsg);
            }
            catch
            {
                throw;
            }
        }

        public string ProgTagBySingle(int iBank, string progCode)
        {
            try
            {
                return iReader.ProgTagBySingle(iBank, progCode);
            }
            catch
            {
                throw;
            }
        }

        public string SearchSerialPort()
        {
            try
            {
                return iReader.SearchSerialPort();
            }
            catch
            {
                throw;
            }
        }

        public List<String> GetSerialPortNm()
        {
            List<String> lstPorNm;
            SerialPort sPort;
            lstPorNm = new List<string>();
            foreach (string portNm in SerialPort.GetPortNames())
            {
                sPort = new SerialPort(portNm);
                sPort.Dispose();
                if (!sPort.IsOpen)
                    lstPorNm.Add(portNm);
            }

            return lstPorNm;
        }

        public void SetSerialPort(string portNm)
        {
            iReader.SetSerialPort(portNm);
        }

        public bool IsEnabledUSB()
        {
            string keyPath = "", keyValue = "";
            RegistryKey regKey = Registry.LocalMachine;
            keyPath = @"SYSTEM\CurrentControlSet\Services\USBSTOR";
            RegistryKey openKey = regKey.OpenSubKey(keyPath);
            keyValue = openKey.GetValue("Start").ToString();
            openKey.Close();
            return keyValue == "3" ? true : false;
        }
    }

    public enum ReaderType
    {
        ALIEN = 1,
        ALIEN_UHF = 2,
        NPX = 3
    };

    public enum RfidCommandGroupNm
    {
        InitReader = 0,
        ListeninTagList = 1
    };
}