using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UHFAPP
{
    public class StringUtils
    {
        /**
         * 判断是否是十六进制
         *
         * @param str
         * @return
         */
        public static bool IsHexNumber(string str)
        {
            if (string.IsNullOrEmpty(str)) {
                return false;
            }
            str = str.Trim();
            if (str.Length == 0  || (str.Length % 2 != 0)) {
                return false;
            }
            if (Regex.IsMatch(str, "^[0-9A-Fa-f]+$"))
            {
                return true;
            }
            return false;
        }
        public static bool IsHexNumber2(char strChar)
        {
            if (strChar == '0' || strChar == '1' || strChar == '2' || strChar == '3' || strChar == '4' || strChar == '5' || strChar == '6' || strChar == '7' || strChar == '8' || strChar == '9'
                 || strChar == 'a' || strChar == 'b' || strChar == 'c' || strChar == 'd' || strChar == 'e' || strChar == 'f'
                 || strChar == 'A' || strChar == 'B' || strChar == 'C' || strChar == 'D' || strChar == 'E' || strChar == 'F')
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 检测是不是数字
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumber(string strNumber)
        {
            if (string.IsNullOrEmpty(strNumber))
            {
                return false;
            }
            Regex regex = new Regex(@"^\d+(\.\d)?$");
            return regex.IsMatch(strNumber);

        }

        public static bool isIP(string ip)
        {
            if (ip == null || ip.Length == 0)
                return false;

            int flag0 = 0;
            int flag1 = 0;
            int flag2 = 0;//统计点好出现的次数
            char[] cIP = ip.ToCharArray();
            for (int k = 0; k < cIP.Length; k++)
            {
                if (cIP[0] == '.' || cIP[cIP.Length - 1] == '.')
                    return false;

                if (cIP[k] != '0' && cIP[k] != '1' && cIP[k] != '2' && cIP[k] != '3' && cIP[k] != '4' &&
                    cIP[k] != '5' && cIP[k] != '6' && cIP[k] != '7' && cIP[k] != '8' && cIP[k] != '9' && cIP[k] != '.')
                {
                    return false;
                }

                if (cIP[k] == '.')
                {
                    flag1 = flag1 + 1;
                    flag2 = flag2 + 1;
                    if (flag1 > 1)
                    {
                        return false;
                    }
                    if (flag2 > 3)
                    {
                        return false;
                    }
                    flag0 = 0;
                }
                else
                {
                    flag0 = flag0 + 1;

                    if (flag0 > 3)
                    {
                        return false;
                    }
                    flag1 = 0;
                }
            }
            return true;
        }
    
    }
}
