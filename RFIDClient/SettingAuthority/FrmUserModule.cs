using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Generic;
using RFIDModel.Common;
using RFIDModel.Model.POJO;
using RFIDModel.Model.DTO;
using RFIDModel.Interface.POJO;
using RFIDModel.Interface.DTO;

namespace RFIDClient
{
    public partial class FrmUserModule : Form
    {
        string sqlMapperId = Common.SqlMapperId;
        string dbName = AppConfig.CurrentDBName;
        IList<PctUser> listUser = null;
        IList<DeptGroup> listDeptGroup = null;

        public FrmUserModule()
        {
            InitializeComponent();
        }

        private void FrmUserModule_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtUser, "Support【UsrId, UsrName, UserEmail, SsoUserNo, Pccuid】fuzzy query");
            toolTip1.SetToolTip(txtDept, "Support【DeptNo, DeptGroup】fuzzy query");
            BindModule();
            try
            {
                var oSvrPctUser = ServiceFactory<IPctUserDAO>.GetService();
                var oSvrDeptGroup = ServiceFactory<IDeptGroupDAO>.GetService();
                // User
                oSvrPctUser.SetConnection(dbName, AppConfig.CurrentDBConn);
                oSvrPctUser.SetPara(SqlConst.SelPctUserAll, dbName, new PctUser(), sqlMapperId);
                listUser = oSvrPctUser.GetData();
                Common.CloseService(oSvrPctUser);
                // DeptGroup
                oSvrDeptGroup.SetPara(SqlConst.SelDeptGroupAll, dbName, new DeptGroup(), sqlMapperId);
                listDeptGroup = oSvrDeptGroup.GetData();
                Common.CloseService(oSvrDeptGroup);
                BindUser();
                BindDept();
            }
            catch { }
            txtUser.Clear();
            txtDept.Clear();
        }

        /// <summary>綁定模塊 </summary>
        private void BindModule()
        {
            IList<RfidModule> listModule = FrmModule.SelectModule();
            dgvModule.DataSource = Common.ToDataTable(listModule);
            foreach (DataGridViewRow row in dgvModule.Rows)
            {
                row.Cells["ColModuleName"].Value = Lang.Dict(Convert.ToString(row.Cells["ColModuleDict"].Value), Convert.ToString(row.Cells["ColModuleName"].Value));
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count == 0) { MessageBox.Show("Please select a row", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string usrId = Convert.ToString(dgvUser.SelectedRows[0].Cells["ColUsrId"].Value);
            bool bRet = SaveAuth(usrId, "");
            string retMsg = bRet ? "Save success" : "Save fail";
            MessageBox.Show(retMsg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>保存設定</summary>
        private bool SaveAuth(string usrId, string deptId)
        {
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var oDMService = ServiceFactory<IDataManipulationDAO>.GetService();
            oDMService.SetConnection(dbName, AppConfig.CurrentDBConn);
            Dictionary<string, string> arrPara = new Dictionary<string, string>();
            RfidModuleAuth oRfidModuleAuth = new RfidModuleAuth();
            oRfidModuleAuth.UsrId = usrId;
            oRfidModuleAuth.DepartId = deptId == "" ? 0 : Convert.ToInt32(deptId);
            oRfidModuleAuth.CreateUid = SrvStatic.usrId;
            oRfidModuleAuth.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            arrPara.Add(SqlConst.DelRfidModuleAuth + "#" + oRfidModuleAuth.GetType().FullName + "#3", JsonConvert.SerializeObject(oRfidModuleAuth));
            for (var i = 0; i < dgvModule.Rows.Count; i++)
            {
                if (Convert.ToString(dgvModule.Rows[i].Cells["ColPermit"].Value).ToLower() == "true")
                {
                    oRfidModuleAuth.ModuleNo = Convert.ToString(dgvModule.Rows[i].Cells["ColModuleNo"].Value);
                    arrPara.Add(SqlConst.InsRfidModuleAuth + "#" + oRfidModuleAuth.GetType().FullName + "#1#" + i.ToString(), JsonConvert.SerializeObject(oRfidModuleAuth));
                }
            }
            DataManipulation dataManipulation = new DataManipulation() { Data = arrPara };
            bool bResult = oDMService.ExecSave(dbName, dataManipulation, sqlMapperId);
            Common.CloseService(oDMService);
            return bResult;
        }

        private void btnDept_Click(object sender, EventArgs e)
        {
            if (dgvDept.SelectedRows.Count == 0) { MessageBox.Show("Please select a row", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string deptId = Convert.ToString(dgvDept.SelectedRows[0].Cells["DepartId"].Value);
            bool bRet = SaveAuth("", deptId);
            string retMsg = bRet ? "Save success" : "Save fail";
            MessageBox.Show(retMsg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>全選</summary>
        private void chkAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < dgvModule.Rows.Count; i++)
            {
                dgvModule.Rows[i].Cells["ColPermit"].Value = chkAll.Checked;
            }
        }

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = txtUser.Text.Trim().ToUpper();
            BindUser(keyword);
            //if (e.KeyCode == Keys.Enter) { BindUser(keyword); }
        }

        /// <summary>綁定用戶</summary>
        private void BindUser(string keyword = "")
        {
            IList<PctUser> userList = listUser;
            if (keyword != "")
                userList = (from d in listUser
                            where
                                (string.IsNullOrEmpty(d.UsrId) ? "-" : d.UsrId).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.UsrName) ? "-" : d.UsrName).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.UserEmail) ? "-" : d.UserEmail).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.SsoUserNo) ? "-" : d.SsoUserNo).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.Pccuid) ? "-" : d.Pccuid).ToUpper().Contains(keyword)
                            select d).ToList<PctUser>();
            dgvUser.DataSource = Common.ToDataTable(userList);
            for (var i = 0; i < dgvUser.Columns.Count; i++)
            {
                dgvUser.Columns[i].Visible = dgvUser.Columns[i].Name.Contains("ColUsrId") || dgvUser.Columns[i].Name.Contains("ColUsrName") ? true : false;
            }
            if (dgvUser.Rows.Count > 0) { dgvUser.Rows[0].Selected = false; }
            lblUserNum.Text = dgvUser.Rows.Count.ToString();
            SetButtonStatus(new bool[] { false, false });
        }

        private void txtDept_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = txtDept.Text.Trim().ToUpper();
            BindDept(keyword);
            //if (e.KeyCode == Keys.Enter) { BindDept(keyword); }
        }

        /// <summary>綁定群組</summary>
        private void BindDept(string keyword = "")
        {
            IList<DeptGroup> deptList = listDeptGroup;
            if (keyword != "")
                deptList = (from d in listDeptGroup
                            where
                                (string.IsNullOrEmpty(d.DeptNo) ? "-" : d.DeptNo).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.DeptGroupName) ? "-" : d.DeptGroupName).ToUpper().Contains(keyword)
                            select d).ToList<DeptGroup>();
            dgvDept.DataSource = Common.ToDataTable(deptList);
            for (var i = 0; i < dgvDept.Columns.Count; i++)
            {
                dgvDept.Columns[i].Visible = dgvDept.Columns[i].Name.Contains("ColDeptNo") || dgvDept.Columns[i].Name.Contains("ColDeptName") ? true : false;
            }
            if (dgvDept.Rows.Count > 0) { dgvDept.Rows[0].Selected = false; }
            lblDeptNum.Text = dgvDept.Rows.Count.ToString();
            SetButtonStatus(new bool[] { false, false });
        }

        private void dgvModule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                dgvModule.Rows[e.RowIndex].Cells["ColPermit"].Value = Convert.ToString(dgvModule.Rows[e.RowIndex].Cells["ColPermit"].Value) == "true" ? "false" : "true";
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            ShowUserAuth();
        }

        /// <summary>顯示用戶權限設定</summary>
        private void ShowUserAuth()
        {
            if (dgvUser.SelectedRows.Count == 0) { return; }
            foreach (DataGridViewRow row in dgvDept.SelectedRows) { row.Selected = false; }
            SetButtonStatus(new bool[] { true, false });
            string usrId = Convert.ToString(dgvUser.SelectedRows[0].Cells["ColUsrId"].Value);
            SelRfidAuth(usrId, "");
        }

        /// <summary>查詢設定 </summary>
        private void SelRfidAuth(string usrId, string deptId)
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
                deptId = deptId == "" || deptId == null ? "0" : deptId;
                oSvrRfidAuth.SetPara(SqlConst.SelRfidModuleAuthAll, dbName, new RfidModuleAuth() { UsrId = usrId, DepartId = Convert.ToInt32(deptId) }, sqlMapperId);
                IList<RfidModuleAuth> listModule = oSvrRfidAuth.GetData();
                Common.CloseService(oSvrRfidAuth);
                chkAll.Checked = false;
                foreach (DataGridViewRow row in dgvModule.Rows)
                {
                    row.Selected = false;
                    row.Cells["ColPermit"].Value = "false";
                    var tmpList = (from d in listModule where d.ModuleNo == Convert.ToString(row.Cells["ColModuleNo"].Value) select d).FirstOrDefault();
                    if (tmpList != null && tmpList.ModuleNo != null)
                    {
                        row.Cells["ColPermit"].Value = "true";
                        chkAll.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDept_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            ShowDeptAuth();
        }

        /// <summary>顯示群組權限設定 </summary>
        private void ShowDeptAuth()
        {
            if (dgvDept.SelectedRows.Count == 0) { return; }
            foreach (DataGridViewRow row in dgvUser.SelectedRows) { row.Selected = false; }
            SetButtonStatus(new bool[] { false, true });
            string deptId = Convert.ToString(dgvDept.SelectedRows[0].Cells["DepartId"].Value);
            SelRfidAuth("", deptId);
        }

        private void dgvModule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            CheckPermit(true);
        }

        /// <summary>是否勾選</summary>
        private void CheckPermit(bool isAutoSel = false)
        {
            bool isChk = false;
            foreach (DataGridViewRow row in dgvModule.Rows)
            {
                if (!isAutoSel) { row.Selected = false; row.Cells["ColPermit"].Value = isAutoSel; }
                if (Convert.ToString(row.Cells["ColPermit"].Value) == "true") { isChk = true; }
            }
            chkAll.Checked = isChk;
        }

        /// <summary>設置按鈕狀態: arrBool[0]:btnUser, arrBool[0]:btnDept</summary>
        private void SetButtonStatus(bool[] arrBool)
        {
            btnUser.Enabled = arrBool[0];
            btnDept.Enabled = arrBool[1];
        }

        private void dgvUser_SelectionChanged(object sender, EventArgs e)
        {
            ShowUserAuth();
        }

        private void dgvDept_SelectionChanged(object sender, EventArgs e)
        {
            ShowDeptAuth();
        }

    }
}
