using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Generic;
using RFIDModel.Interface.DTO;
using RFIDModel.Model.DTO;
using RFIDModel.Common;

namespace RFIDClient
{
    public partial class FrmCheckUser : Form
    {
        string dbName = AppConfig.CurrentDBName;
        string dbConn = AppConfig.CurrentDBConn;
        string sqlMapperId = Common.SqlMapperId;
        ComboBox oCbxOutSelection, oCbxOutUser;
        DateTime dtStart = DateTime.Now;
        IList<DeptGroupUser> listStockUser;

        public FrmCheckUser()
        {
            InitializeComponent();
        }

        public FrmCheckUser(ComboBox cbxOutSelection, ComboBox cbxOutUser)
        {
            InitializeComponent();
            oCbxOutSelection = cbxOutSelection;
            oCbxOutUser = cbxOutUser;
        }

        private void FrmCheckUser_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (listStockUser != null && listStockUser.Count > 0)
            {
                oCbxOutSelection.Text = "";
                oCbxOutUser.Text = "";
                oCbxOutSelection.Text = listStockUser[0].DepartId + "-" + listStockUser[0].DeptGroupName;
                oCbxOutUser.Text = listStockUser[0].UsrId + "-" + listStockUser[0].UsrName;
                oCbxOutUser.Focus();
            }
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsrID.Clear();
            txtUsrName.Clear();
            txtSsoUserNo.Clear();
            txtMessage.Clear();
            txtUsrID.Focus();
        }

        private void txtUsrID_KeyDown(object sender, KeyEventArgs e)
        {
            string usrID = txtUsrID.Text.Trim();
            if (string.IsNullOrEmpty(txtUsrID.Text))
                dtStart = DateTime.Now;
            if (e.KeyCode == Keys.Enter)
            {
                if (usrID != "")
                {
                    txtUsrName.Clear();
                    txtSsoUserNo.Clear();
                    txtMessage.Clear();
                    if (!Common.CheckChannel())
                    {
                        MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var oService = ServiceFactory<IDeptGroupUserDAO>.GetService();
                    DeptGroupUser oDeptGroupUser = new DeptGroupUser() { UsrId = usrID };
                    oService.SetConnection(dbName, dbConn);
                    oService.SetPara(SqlConst.SelDeptGroupUserAll, dbName, oDeptGroupUser, sqlMapperId);
                    listStockUser = oService.GetData() as IList<DeptGroupUser>;
                    Common.CloseService(oService);
                    if (listStockUser != null && listStockUser.Count > 0)
                    {
                        txtUsrName.Text = listStockUser[0].UsrName;
                        txtSsoUserNo.Text = listStockUser[0].SsoUserNo;
                    }
                    txtMessage.Text = string.IsNullOrEmpty(txtSsoUserNo.Text) ? "此帳號不存在，請重新掃瞄! (Scan fail，please try again)" : "掃描成功 (Scan success)";
                    txtMessage.ForeColor = string.IsNullOrEmpty(txtSsoUserNo.Text) ? Color.Red : Color.Green;
                }
            }
            else
            {
                if ((dtStart.Ticks + 5000000) < DateTime.Now.Ticks)
                    txtUsrID.Text = "";
            }
        }

        private void txtUsrID_Click(object sender, EventArgs e)
        {
            txtUsrID.SelectAll();
        }
    }
}
