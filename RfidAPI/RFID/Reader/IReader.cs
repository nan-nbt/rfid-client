using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RFID.Command;

namespace RFID.Reader
{
    public interface IReader
    {
        bool IsConnected();
        bool OpenConnect(int iType);
        bool Disconnect();
        bool SetReaderBySingle(ParameterNm commmandNm, Parameters classAttr);
        bool SetReaderBySingle(ParameterNm commmandNm, string strAttr);
        bool SetReaderBySingle(ParameterNm commmandNm, int iAttr);
        bool SetReaderBySingle(ParameterNm commmandNm);
        void AddMessageReceived(ReceiveMsg oReceiveMsg);
        void RemoveMessageReceived(ReceiveMsg oReceiveMsg);
        string ReadTagByBank(int iBank, string wordPtr, string wordLen);
        string TagFormatByBank(int iBank, string tag);
        List<TagStruct> GetTagList();
        void ClearTagList();
        string ProgTagBySingle(int iBank, string progCode);
        string SearchSerialPort();
        void SetSerialPort(string portNm);
        bool SetPower();
        bool StopGet();
        /// <summary> UHF Reader讀寫功率及重試次數 </summary>
        int WriteTime(int iMinOutP, int iMaxOutP, int iTime);
        int GetSuccQty();
    }
}