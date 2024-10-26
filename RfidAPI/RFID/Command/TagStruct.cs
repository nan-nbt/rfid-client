using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFID.Command
{
    public class TagStruct
    {
        public string TagID { get; set; }
        public string DiscoveryTime { get; set; }
        public string LastSeenTime { get; set; }
        public string Antenna { get; set; }
        public string ReadCount { get; set; }
        public string Protocol { get; set; }
        public string OtherUse { get; set; }
    }
}
