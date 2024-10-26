using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFID.Command
{
    public delegate void ReceiveMsg(string msg);
    public class Parameters
    {
        public string BITPTR;
        public string BITLEN;
        public string HEX_BYTES;
        public string MEMORY_BANK;
    }

    public enum ParameterNm
    {
        AutoAction = 0,
        AutoModeReset = 1,
        AcqG2Mask = 2,
        AcqG2MaskAction = 3,
        TagListFormat = 4,
        SetAcqG2Mask = 5,
        AutoMode = 6,
        ProgDataUnit = 7,
        NotifyTime = 8,
        NotifyFormat = 9,
        NotifyTrigger = 10,
        PersistTime = 11,
        NotifyMode = 12,
        RSSIFilter = 13,
        ComTimeOutInterval = 14,
        NetworkTimeout = 15,
        RFAttenuation = 16,
        ProgEPCData = 17,
        ProgEPCDataInc = 18,
        NotifyAddress = 19,
        NotifyHeader = 20,
        Mask = 21,
        AntennaSequence = 22,
        MaxAntenna = 23,
        RFLevel = 24
    };
}