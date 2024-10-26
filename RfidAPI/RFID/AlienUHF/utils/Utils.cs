using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace BLEDeviceAPI
{
   public class Utils
    {
        /// <summary>
        /// 复制数组
        /// </summary>
        /// <typeparam name="T">泛型数组类型</typeparam>
        /// <param name="sourceArray">原数组</param>
        /// <param name="copyLen">要复制的数组长度，也就是新数组的长度</param>
        /// <returns>返回新数组</returns>
        public static T[] CopyArray<T>(T[] sourceArray, int copyLen)
        {
            T[] outData = new T[copyLen];
            Array.Copy(sourceArray, outData, copyLen);
            return outData;
        }
        /// <summary>
        /// 复制数组
        /// </summary>
        /// <typeparam name="T">泛型数组类型</typeparam>
        /// <param name="sourceArray">原数组</param>
        /// <param name="copyLen">要复制的数组长度，也就是新数组的长度</param>
        /// <returns>返回新数组</returns>
        public static T[] CopyArray<T>(T[] sourceArray, int start,int copyLen)
        {
            T[] outData = new T[copyLen];
            Array.Copy(sourceArray, start,outData, 0,copyLen);
            return outData;
        }
    }
}
