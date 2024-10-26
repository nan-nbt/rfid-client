using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFAPP
{
    public class IPConfig
    {
        static string path = System.Environment.CurrentDirectory+"\\ipConfig.txt";

        public static void setIPConfig(IPEntity ipEntity)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("ip=");
                sb.Append(ipEntity.Ip[0]);
                sb.Append(".");
                sb.Append(ipEntity.Ip[1]);
                sb.Append(".");
                sb.Append(ipEntity.Ip[2]);
                sb.Append(".");
                sb.Append(ipEntity.Ip[3]);
                sb.Append("\r\n");
                sb.Append("port=");
                sb.Append(ipEntity.Port);
                FileManage.WriterFile(path, sb.ToString(), false);
            }
            catch (Exception ex) { 
            
            }
        }
        public static IPEntity getIPConfig()
        {
            try
            {
                string data = FileManage.ReadFile(path);
                if (data == "") return null;

                string[] ip = data.Split('\n')[0].Replace("ip=", "").Replace(" ", "").Replace("\r", "").Split('.');
                if (ip.Length != 4) return null;

                int port = int.Parse(data.Split('\n')[1].Replace("port=", "").Replace("\r", "").Replace(" ", ""));
                IPEntity ipEntity = new IPEntity();
                ipEntity.Ip = ip;
                ipEntity.Port = port;
                return ipEntity;
            }
            catch (Exception ex)
            {

            }

            return null;
        }


        public class IPEntity
        {
            private string[] ip;

            private string strIp;

            public string StrIp
            {
                get { return strIp; }
                set { strIp = value; }
            }

            public string[] Ip
            {
                get { return ip; }
                set { ip = value; }
            }
            private int port;

            public int Port
            {
                get { return port; }
                set { port = value; }
            }

        }
    }
}
