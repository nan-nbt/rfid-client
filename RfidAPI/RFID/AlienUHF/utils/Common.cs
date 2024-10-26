using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace UHFAPP
{
    public class Common
    {
        public static bool isEnglish = false;
        public static string tag = "";

        public static int time = 2000;

        static Hashtable hash = new Hashtable();

        public static Form getForm(string FormName, Form mainForm)
        {
            if (hash.Contains(FormName))
            {
                Form frm = (Form)hash[FormName];
                return frm;
            }
            else
            {

                Form form = (Form)Assembly.Load("UHFAPP").CreateInstance("UHFAPP." + FormName); //注意: 窗体命  名空间后面一定要加一个点
                //form.MdiParent = mainForm; //如果是Mdi窗体的话请加这一行
                hash.Add(FormName, form);
                //form.Show();
                return form;
            }

        }

    }



}
