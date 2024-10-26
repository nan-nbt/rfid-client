using UHFAPP;
using UR4RFID;
using static UR4RFID.ur4Reader;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using static WinFormsApp1.Form1;
using System.Collections;
using System.Windows.Forms;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        static ur4Reader ur4 = new ur4Reader();
        private static List<Thread> thrList = new List<Thread>();
        private static object oLock = new object();
        ArrayList arraySysPort = new ArrayList();

        int mask_flag = 0;
        internal delegate void ShowUICallBack(string Info, bool uiIsShow);

        public Form1()
        {
            InitializeComponent();
            btn_FunEnabled(false);

            string[] myPorts = SerialPort.GetPortNames(); //���o�Ҧ�port���W�r����k
            foreach (string port in myPorts)  //�ϥΰj��覡���o�Ҧ�port���W�r
            {
                cb_Port.Items.Add(port);
            }
            if (cb_Port.Items.Count > 0)
                cb_Port.SelectedIndex = cb_Port.Items.Count - 1;
            //cb_Port.SelectedIndex = 0;
            ur4.CallAlert += new ur4Reader.MessageReceivedEventHandler(oReceiveMsg);
        }

        /// <summary>
        /// �s�u ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (btn_Connect.Text.Equals("�s�u"))
            {
                ur4.Connect(cb_Port.SelectedItem.ToString());
                if (ur4.IsConnected)
                {
                    ShowUI("�s�u���\!!", true);
                    btn_Connect.Text = "�_�u";
                    btn_FunEnabled(true);

                    Thread.Sleep(1000);

                    byte power = 0;
                    if (uhf.GetPower(ref power))
                    {
                        cb_Power.SelectedIndex = power - 1;
                    }
                }
                else
                {
                    ShowUI("�s�u����!!", true);
                    btn_FunEnabled(false);
                }
            }
            else
            {
                ur4.Disconnect();
                if (!ur4.IsConnected)
                {
                    ShowUI("�_�u���\!!", true);
                    btn_Connect.Text = "�s�u";
                    btn_FunEnabled(false);
                }
                else
                {
                    ShowUI("�_�u����!!", true);
                }
            }
        }

        /// <summary>
        /// Mask ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Mask_Click(object sender, EventArgs e)
        {
            mask_flag = 0;
            if (!txt_mask.Text.Equals("") && txt_mask.Text.Length == 24)
            {
                string MaskResutl = ur4.SetAcqG2Mask("1", "32", "96", txt_mask.Text);
                if (MaskResutl.IndexOf("Success") > -1)
                {
                    mask_flag = 1;
                    btn_unMask.Enabled = true;
                    ShowUI("Mask " + txt_mask.Text + " ����", true);
                }
                else
                {
                    ShowUI("Mask ����/n��]:" + MaskResutl, true);
                }
            }
            else
            {
                ShowUI("�п�J Mask ��r(24 �X) ", true);
            }
        }

        /// <summary>
        /// �Ѱ� Mask 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_unMask_Click(object sender, EventArgs e)
        {
            int save = 1;
            if (ur4.SetAcqG2unMask().IndexOf("Success") > -1)
            {
                mask_flag = 0;
                txt_mask.Text = "";
                ShowUI("�Ѱ� Mask ���\", true);
            }
            else
            {
                ShowUI("�Ѱ� Mask ����!!", true);
            }
        }

        /// <summary>
        /// �g�X ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Write_Click(object sender, EventArgs e)
        {
            string result = ur4.ProgramEPC(txt_write.Text, txt_mask.Text);
            txt_write.Text = "";
            ShowUI(result, true);
        }

        /// <summary>
        /// Ū���浧 ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadOne_Click(object sender, EventArgs e)
        {
            string result = "";
            result = ur4.Read(mask_flag, txt_mask.Text);

            if (result != string.Empty)
            {
                ShowUI(result, true);
                //txt_Message.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "  " + result + "\r\n\r\n");
            }
        }

        /// <summary>
        /// Ū���h�� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadMulti_Click(object sender, EventArgs e)
        {
            if (btn_ReadMulti.Text.Equals("�h��Ū��"))
            {
                btn_ReadMulti.Text = "����Ū��";
                ur4.MessageReceived();
                btn_MultiReadEnabled(false);

            }
            else
            {
                btn_ReadMulti.Text = "�h��Ū��";
                ur4.StopEPC();
                btn_MultiReadEnabled(true);
            }

        }

        /// <summary>
        /// �M���O��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearMessage_Click(object sender, EventArgs e)
        {
            txt_Message.Text = "";
        }

        private void DoSomething(object oThrNum)
        {
            lock (oLock)
            {
                Thread thrCurrentThread = Thread.CurrentThread;
                string result = ur4.Read(mask_flag, txt_mask.Text);
                ShowUI("EPC:" + result, true);
            }
        }

        /// <summary>
        /// Reecive event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void oReceiveMsg(object sender, AlertEventArgs e)
        {
            ShowUI(e.uuiData, true);
        }

        /// <summary>
        /// ��ܦb�e���W
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="uiIsShow"></param>
        public void ShowUI(string Info, bool uiIsShow)
        {
            try
            {
                if (txt_Message.InvokeRequired)
                {
                    // call back on this same method, but in a different thread.
                    txt_Message.Invoke(
                         new ShowUICallBack(ShowUI), // the method to call back on
                         new object[] { Info, uiIsShow });
                }
                else
                {
                    // you are in this method on the correct thread.
                    Info = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "  " + Info + "\r\n\r\n";
                    if (uiIsShow)
                        txt_Message.AppendText(Info);
                }
            }
            catch (Exception ex)
            {
                // you are in this method on the correct thread.
                Info = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "  " + "�o�Ϳ��~:" + ex.Message + "\r\n\r\n";
            }
        }

        /// <summary>
        /// �s�u��i�Ϋ���
        /// </summary>
        /// <param name="flag"></param>
        private void btn_FunEnabled(bool flag)
        {
            btn_Write.Enabled = flag;
            btn_ReadOne.Enabled = flag;
            btn_ReadMulti.Enabled = flag;
            btn_SetPower.Enabled = flag;
            btn_Mask.Enabled = flag;
            btn_unMask.Enabled = flag;
        }

        private void btn_MultiReadEnabled(bool flag)
        {
            btn_Connect.Enabled = flag;
            btn_Write.Enabled = flag;
            btn_ReadOne.Enabled = flag;
            btn_SetPower.Enabled = flag;
            btn_Mask.Enabled = flag;
            btn_unMask.Enabled = flag;
        }

        private void btn_SetPower_Click(object sender, EventArgs e)
        {
            if (ur4.SetPower(1, int.Parse(cb_Power.SelectedItem.ToString())).IndexOf("Success") > -1)
            {
                Thread.Sleep(1000);
                ShowUI("Reader ���m�� ..", true);
                Thread.Sleep(1000);
                ShowUI("�\�v�]�m���\", true);
            }
            else
            {
                ShowUI("�\�v�]�m����!!", true);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ur4.IsConnected)
            {
                ur4.Disconnect();
            }
        }
    }

}