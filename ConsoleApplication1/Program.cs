using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ConsoleApp
{
    /// <summary>
    /// 定义事件
    /// </summary>
    class AlienUhf
    {
        /// <summary>定义委托</summary>
        public delegate void MessageReceivedEventHandler(string msg);
        /// <summary>此委托类型的事件</summary>
        public event MessageReceivedEventHandler MessageReceived;
        public AlienUhf()
        {
            ReaderEventMonitor oReaderMonitor = new ReaderEventMonitor(this);
        }
        public void DoRun()
        {
            bool flag = false;
            do
            {
                //Console.WriteLine();
                //Console.WriteLine("请输入：");
                //string result = Console.ReadLine();
                ////if (result == "1")
                ////{
                //if (MessageReceived != null)
                    MessageReceived("中華人民萬歲");
                //}
            } while (!flag);
        }
    }
    /// <summary>事件监听</summary>
    class ReaderEventMonitor
    {
        public ReaderEventMonitor(AlienUhf oReader)
        {
            oReader.MessageReceived += ShowMessage;
            oReader.MessageReceived += delegate
            {
                Console.WriteLine("第三 hello word!!用户测试");
            };
        }
        public void ShowMessage(string msg)
        {
            Console.WriteLine("第二 hello word!!显示提示信息"+ msg);
        }
    }
    /// <summary>
    /// 调用类
    /// </summary>
    public class Run
    {
        public delegate void ReceiveMsg(string msg);

        public static void updData(string msg)
        {
            Console.WriteLine("请输入：");
            string result = Console.ReadLine();
            Console.WriteLine("第一 更新資料, 用户测试 Test" + result);
        }

        static void Main(string[] args)
        {
            AlienUhf oReader = new AlienUhf();
            ReceiveMsg oReceiveMsg = new ReceiveMsg(updData);
            oReader.MessageReceived += new AlienUhf.MessageReceivedEventHandler(oReceiveMsg);
            // oReader.MessageReceived -= new AlienUhf.MessageReceivedEventHandler(oReceiveMsg);
            oReader.DoRun();
            Console.ReadLine();
        }

    }
}