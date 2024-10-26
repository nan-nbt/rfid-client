using System;
using System.IO;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Newtonsoft.Json;
using Generic;
using RFIDModel.Common;
using RFIDModel.Model.POJO;
using RFIDModel.Model.DTO;
using RFIDModel.Interface.POJO;
using RFIDModel.Interface.DTO;

namespace RFIDClient
{
    public partial class MainForm : Form
    {
        Panel[] arrPanel = null;
        TextBox oTxtBox = null, oPreText = null;
        string sqlMapperId = Common.SqlMapperId;
        string dbName = AppConfig.CurrentDBName;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>窗體初始化 </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            string userDate = "Welcome【" + SrvStatic.usrName + "】" + DateTime.Now.ToString("yyyy-MM-dd dddd"); //new System.Globalization.CultureInfo("zh-cn")
            this.Text = "RFID Client" + userDate.PadLeft(60, ' ') + FrmLogin.CT.PadLeft(60, ' ');
            CreateMenuNode(false);
            GetUserModule();
            SrvStatic.rfidAdmin = IsRfidAdmin();
            tabPageAuth.Parent = SrvStatic.rfidAdmin ? tabControl1 : null;
            txtDesc.Select();
        }

        #region 函數功能
        /// <summary>是否 RFID 管理員</summary>
        private bool IsRfidAdmin()
        {
            bool bAdmin = false;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                var oSvrPctUser = ServiceFactory<IPctUserDAO>.GetService();
                oSvrPctUser.SetConnection(dbName, AppConfig.CurrentDBConn);
                oSvrPctUser.SetPara(SqlConst.SelRfidAdminUser, dbName, new PctUser() { UsrId = Convert.ToString(SrvStatic.usrId).ToUpper() }, sqlMapperId);
                IList<PctUser> listUser = oSvrPctUser.GetData();
                Common.CloseService(oSvrPctUser);
                bAdmin = listUser != null && listUser.Count > 0 ? true : false;
            }
            catch { bAdmin = false; }
            return bAdmin;
        }
        /// <summary>創建節點菜單 </summary>
        private void CreateMenuNode(bool isEnable = true)
        {
            if (panelBasic.HasChildren) { panelBasic.Controls.Clear(); }
            IList<RfidModule> listModule = FrmModule.SelectModule("Y");
            if (listModule == null || listModule.Count == 0)
            {
                TextBox oTextBox = new TextBox();
                oTextBox.Text = "No modules data in the database\r\nPlease contact administrator";
                oTextBox.Dock = DockStyle.Fill;
                oTextBox.ForeColor = Color.Red;
                oTextBox.BackColor = Color.Azure;
                oTextBox.Font = new Font("Arial", 22, FontStyle.Regular);
                oTextBox.Multiline = true;
                oTextBox.ReadOnly = true;
                oTextBox.TextAlign = HorizontalAlignment.Center;
                panelBasic.Controls.Add(oTextBox);
                return;
            }
            int rowQty = 5;//每行節點數
            int iCnt = listModule.Count;
            arrPanel = new Panel[iCnt];
            for (var i = 0; i < listModule.Count; i++)
            {
                string moduleNo = listModule[i].ModuleNo;
                string tmp = moduleNo;
                tmp = moduleNo != null && moduleNo.IndexOf(".") > 0 ? moduleNo.Substring(moduleNo.LastIndexOf(".") + 1) : moduleNo;
                if (tmp.ToUpper() == "EXE")
                {
                    tmp = moduleNo != null && moduleNo.IndexOf("/") > 0 ? moduleNo.Substring(moduleNo.LastIndexOf("/") + 1) : moduleNo;
                    tmp = moduleNo != null && moduleNo.IndexOf("\\") > 0 ? moduleNo.Substring(moduleNo.LastIndexOf("\\") + 1) : moduleNo;
                    tmp = tmp.Replace(".exe", "").Replace(".EXE", "");
                }
                Panel oPanel = new Panel();
                oPanel.BorderStyle = BorderStyle.FixedSingle;
                oPanel.Width = 160;
                oPanel.Height = 180;
                oPanel.Top = i / rowQty * 200 + 10;
                oPanel.Left = i % rowQty * 190 + 10;
                oPanel.Name = "oPanel" + tmp;
                oPanel.Tag = moduleNo;
                PictureBox oPicBox = new PictureBox();
                oPicBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(tmp, null);
                if (oPicBox.Image == null)
                    oPicBox.Image = (Image)Properties.Resources.ResourceManager.GetObject("FrmDefault", null);
                oPicBox.Name = "pic" + tmp;
                oPicBox.Height = 136;
                oPicBox.Dock = DockStyle.Top;
                oPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                oPicBox.Tag = moduleNo;
                oPicBox.Cursor = Cursors.Hand;
                TextBox oTextBox = new TextBox();
                oTextBox.Name = "txt" + tmp;
                oTextBox.Dock = DockStyle.Bottom;
                oTextBox.BorderStyle = BorderStyle.None;
                oTextBox.ForeColor = Color.DarkBlue;
                oTextBox.BackColor = Color.White;
                oTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
                oTextBox.Height = oPanel.Height - oPicBox.Height;
                oTextBox.Multiline = true;
                oTextBox.ReadOnly = true;
                oTextBox.Cursor = Cursors.Hand;
                oTextBox.Tag = moduleNo;
                oTextBox.TextAlign = HorizontalAlignment.Center;
                oTextBox.Text = Lang.Dict(listModule[i].ModuleDict, listModule[i].ModuleName);
                oPicBox.Click += delegate(object sender1, EventArgs e1) { panel_Click(sender1, e1); };
                oTextBox.Click += delegate(object sender1, EventArgs e1) { panel_Click(sender1, e1); };
                oPicBox.MouseHover += delegate(object sender1, EventArgs e1) { ShowDesc(oTextBox, e1, "over"); };
                oTextBox.MouseHover += delegate(object sender1, EventArgs e1) { ShowDesc(sender1, e1, "over"); };
                oPicBox.MouseLeave += delegate(object sender1, EventArgs e1) { ShowDesc(oTextBox, e1, "out"); };
                oTextBox.MouseLeave += delegate(object sender1, EventArgs e1) { ShowDesc(sender1, e1, "out"); };
                oPanel.Controls.Add(oPicBox);
                oPanel.Controls.Add(oTextBox);
                oPanel.Enabled = isEnable;
                arrPanel[i] = oPanel;
                panelBasic.Controls.Add(oPanel);
            }
        }
        /// <summary>取得有權限的模組</summary>
        private void GetUserModule()
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var oSvrRfidAuth = ServiceFactory<IRfidModuleAuthDAO>.GetService();
                oSvrRfidAuth.SetConnection(dbName, AppConfig.CurrentDBConn);
                oSvrRfidAuth.SetPara(SqlConst.SelUserModuleAuth, dbName, new RfidModuleAuth() { UsrId = SrvStatic.usrId }, sqlMapperId);
                IList<RfidModuleAuth> listModule = oSvrRfidAuth.GetData();
                Common.CloseService(oSvrRfidAuth);
                foreach (Panel node in arrPanel)
                {
                    node.Cursor = Cursors.Arrow;
                    node.Enabled = false;
                    var tmpList = (from d in listModule where d.ModuleNo == Convert.ToString(node.Tag) select d).FirstOrDefault();
                    if (tmpList != null && tmpList.ModuleNo != null)
                    {
                        node.Enabled = true;
                        node.Cursor = Cursors.Hand;
                    }
                }
            }
            catch { }
        }
        #endregion 函數功能

        private void panel_Click(object sender, EventArgs e)
        {
            string winName = "", extNm = "";
            string t = sender.GetType().Name;
            if (sender == null) { return; }
            try
            {
                if (t == "PictureBox")
                    winName = (sender as PictureBox).Tag.ToString();
                if (t == "TextBox")
                    winName = (sender as TextBox).Tag.ToString();
                extNm = winName.Substring(winName.LastIndexOf(".") + 1);
                if (!string.IsNullOrEmpty(winName))
                {
                    if (oPreText != null)
                        oPreText.ForeColor = Color.DarkBlue;
                    if (extNm.ToUpper() == "EXE")
                    {
                        string exeFile = Common.appPath + winName;
                        if (!File.Exists(exeFile))
                            MessageBox.Show("未找到此路徑下的程序文件\nNot found the program file in the path.\n\n" + exeFile, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            System.Diagnostics.Process.Start(exeFile);
                    }
                    else
                    {
                        Type type = this.GetType();
                        Assembly assembly = type.Assembly;
                        Form oForm = assembly.CreateInstance(winName) as Form;
                        oForm.ShowInTaskbar = false;
                        oForm.ShowDialog();
                    }
                    oTxtBox.ForeColor = Color.DarkOrange;
                    oPreText = oTxtBox;
                }
            }
            catch (Exception ex)
            {
                if (extNm.ToUpper() != "EXE")
                    MessageBox.Show("Cann't instantiate a WinFom\nPlease contact administrator\n\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>用戶模組設定 </summary>
        private void panelUserModule_Click(object sender, EventArgs e)
        {
            FrmUserModule frmUserModule = new FrmUserModule();
            frmUserModule.ShowDialog();
        }

        /// <summary>模組設定 </summary>
        private void panelModule_Click(object sender, EventArgs e)
        {
            FrmModule frmModule = new FrmModule();
            frmModule.ShowDialog();
        }

        /// <summary>指紋登記 </summary>
        private void panelFingerprint_Click(object sender, EventArgs e)
        {
            FrmFingerprint frmFingerprint = new FrmFingerprint();
            frmFingerprint.ShowDialog();
        }

        //顯示功能描述
        private void ShowDesc(object sender, EventArgs e, string mt)
        {
            if (sender != null)
            {
                oTxtBox = sender as TextBox;
                oTxtBox.ForeColor = mt == "over" ? Color.DarkCyan : Color.DarkBlue;
                groupBox1.Text = "【" + oTxtBox.Text + "】 Functional Description";
                txtDesc.Text = oTxtBox.Text + "\r\n.......";
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // call logout method for logout from OAuth 2.0 | anan add 20240902
            FrmOAuth2Login oFrm = new FrmOAuth2Login();
            oFrm.Logout();

            Environment.Exit(Environment.ExitCode);
            this.Close();
            this.Dispose();
            Application.Exit();
        }

        /// <summary>頁簽切換</summary>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex + 1)
            {
                case 1:
                    CreateMenuNode(false);
                    GetUserModule();
                    break;
                case 2:
                    break;
            }
        }

        //重繪Tab, OwnerDrawFixed
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            for (var t = 0; t < tabControl1.TabPages.Count; t++)
            {
                Rectangle oRec = tabControl1.GetTabRect(t);
                e.Graphics.FillRectangle(new SolidBrush(Color.Wheat), oRec);
                e.Graphics.DrawString(tabControl1.TabPages[t].Text, new Font("Arial", 14, FontStyle.Bold), new SolidBrush(Color.Black), oRec, new StringFormat() { Alignment = StringAlignment.Center });
            }
        }

    }
}
