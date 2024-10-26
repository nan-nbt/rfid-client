using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFAPP.usb.hid_new
{
    class Debug
    {
        public static bool isDebug = false;
   
        public static void PrintLog(string msg)
        {
            if (isDebug)
            {
                Console.WriteLine("msg=" + msg);
                FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now + " " + msg + "\r\n");
            }
        }
        public static void PrintLog(string tag, string msg)
        {
            if (isDebug)
            {
                Console.WriteLine(tag + "=" + msg);
                FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now + " " + tag + " " + msg + "\r\n");
            }
        }
        public static void PrintLog(string tag, byte[] msg)
        {
            if (isDebug)
            {
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < msg.Length; k++)
                {
                    sb.Append(String.Format("{0:X2} ", msg[k]));
                }
                Console.WriteLine(tag + "=" + sb.ToString());
                FileManage.WriterLog(FileManage.LogType.Debug, DateTime.Now + " " + tag + " " + sb.ToString() + "\r\n");
            }
        }
    }
}
