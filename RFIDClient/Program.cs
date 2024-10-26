using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RFIDModel.Interface.DTO;

namespace RFIDClient
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogin oFrm = new FrmLogin();
            //FrmMain oFrm = new FrmMain();
            Application.Run(oFrm);
        }
    }
}
