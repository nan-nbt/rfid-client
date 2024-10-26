using System;
using System.Collections.Generic;
using RFIDModel.Common;
using RFIDClient;

namespace Generic
{
    class CallbackHandler : ICallbackClient
    {
        public void ServerCallback(string serverReplyMsg)
        {
            //FrmTagWrite.oFrmTagWrite.richTextBox1.Text = "";
            //FrmTagWrite.oFrmTagWrite.richTextBox1.Text += string.Format("{0} 收到服務器的回復:{1}", DateTime.Now, serverReplyMsg);
            //FrmTagWrite.oFrmTagWrite.richTextBox1.SelectAll();
            Common.CurStatusMsg = serverReplyMsg;
            Console.WriteLine("{0} 收到服務器的回復:{1}", DateTime.Now, serverReplyMsg);
        }

    }
}
