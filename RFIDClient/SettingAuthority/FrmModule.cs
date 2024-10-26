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
    public partial class FrmModule : Form
    {
        int iRow = 0;
        static string sqlMapperId = Common.SqlMapperId;
        static string dbName = AppConfig.CurrentDBName;
        IList<RfidModule> listModule = null;

        public FrmModule()
        {
            InitializeComponent();
        }

        private void FrmModule_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            listModule = SelectModule();
            BindModule();
        }

        /// <summary>查詢模組</summary>
        public static IList<RfidModule> SelectModule(string enableMk = "")
        {
            IList<RfidModule> listData = null;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return listData;
            }
            try
            {
                var oSvrRfidModule = ServiceFactory<IRfidModuleDAO>.GetService();
                oSvrRfidModule.SetConnection(dbName, AppConfig.CurrentDBConn);
                oSvrRfidModule.SetPara(SqlConst.SelRfidModuleAll, dbName, new RfidModule(), sqlMapperId);
                IList<RfidModule> tmpList = oSvrRfidModule.GetData();
                listData = tmpList;
                Common.CloseService(oSvrRfidModule);
                if (!string.IsNullOrEmpty(enableMk))
                    listData = (from d in listData
                                where (string.IsNullOrEmpty(d.EnableMk) ? "-" : d.EnableMk).ToUpper().Trim() == Convert.ToString(enableMk).ToUpper().Trim()
                                select d).ToList<RfidModule>();
            }
            catch { return listData; }
            return listData;
        }

        /// <summary>綁定</summary>
        private void BindModule(string keyword = "")
        {
            dynamic bindList = listModule;
            if (keyword != "")
                bindList = (from d in listModule
                            where
                                (string.IsNullOrEmpty(d.ModuleNo) ? "-" : d.ModuleNo).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.ModuleName) ? "-" : d.ModuleName).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.ModuleDict) ? "-" : d.ModuleDict).ToUpper().Contains(keyword) ||
                                (string.IsNullOrEmpty(d.SortSeq) ? "-" : d.SortSeq).ToUpper().Contains(keyword) ||
                                ("@" + (string.IsNullOrEmpty(d.EnableMk) ? "-" : d.EnableMk) + "@").ToUpper().Contains("@" + keyword + "@")
                            select d).ToList<RfidModule>();
            dgvModule.DataSource = Common.ToDataTable(bindList);
            foreach (DataGridViewRow row in dgvModule.Rows)
            {
                row.Cells["ColModuleName"].Value = Lang.Dict(Convert.ToString(row.Cells["ColModuleDict"].Value), Convert.ToString(row.Cells["ColModuleName"].Value));
            }
            lblModuleNum.Text = dgvModule.Rows.Count.ToString();
            if (dgvModule.Rows.Count == 0)
                btnDel.Enabled = false;
            else
                dgvModule.Rows[iRow].Selected = true;
        }

        private void txtModule_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = txtModule.Text.Trim().ToUpper();
            BindModule(keyword);
            ShowDetail(dgvModule.CurrentRow);
        }

        private void dgvModule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            ShowDetail(dgvModule.CurrentRow);
        }

        private void ShowDetail(DataGridViewRow row = null)
        {
            btnSave.Enabled = true;
            txtModuleNo.Text = "";
            txtModuleName.Text = "";
            txtModuleDict.Text = "";
            txtSortSeq.Text = "M" + (Convert.ToInt32(lblModuleNum.Text) + 1).ToString().PadLeft(4, '0');
            chkEnableMk.Checked = true;
            txtModuleNo.ReadOnly = false;
            txtModuleNo.BackColor = Color.White;
            if (row != null)
            {
                iRow = row.Index;
                btnDel.Enabled = true;
                txtModuleNo.ReadOnly = true;
                txtModuleNo.BackColor = this.BackColor;
                txtModuleNo.Text = Convert.ToString(row.Cells["ColModuleNo"].Value);
                txtModuleName.Text = Convert.ToString(row.Cells["ColModuleName"].Value);
                txtModuleDict.Text = Convert.ToString(row.Cells["ColModuleDict"].Value);
                txtSortSeq.Text = Convert.ToString(row.Cells["ColSortSeq"].Value);
                chkEnableMk.Checked = Convert.ToString(row.Cells["ColEnableMk"].Value) == "Y" ? true : false;
            }
        }

        private void dgvModule_SelectionChanged(object sender, EventArgs e)
        {
            ShowDetail(dgvModule.CurrentRow);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvModule.SelectedRows) { row.Selected = false; }
            ShowDetail();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string moduleNo = txtModuleNo.Text.Trim();
            string moduleNm = txtModuleName.Text.Trim();
            string moduleDict = txtModuleDict.Text.Trim();
            string sortSeq = txtSortSeq.Text.Trim();
            int sqlType = dgvModule.SelectedRows.Count > 0 ? 2 : 1;
            var tmpList = (from d in listModule
                           where (string.IsNullOrEmpty(d.ModuleNo) ? "-" : d.ModuleNo).ToUpper().Contains((string.IsNullOrEmpty(moduleNo) ? "-" : moduleNo).ToUpper())
                           select d).ToList<RfidModule>();
            if (tmpList != null && tmpList.Count > 0 && sqlType == 1)
            {
                MessageBox.Show("You input【Module No.】has existed", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            }
            if (moduleNo == "" || moduleNo.Length > 100)
            {
                MessageBox.Show("Please input 【Module No.】\n Or input length too long (no more than 100)", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information); txtModuleNo.Select(); return;
            }
            if (moduleNm == "" || moduleNm.Length > 200)
            {
                MessageBox.Show("Please input 【Module Name】\n Or input length too long (no more than 200)", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information); txtModuleName.Select(); return;
            }
            if (moduleDict == "" || moduleDict.Length > 5)
            {
                MessageBox.Show("Please input 【Module Dict】\n Or input length too long (no more than 5)", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information); txtModuleDict.Select(); return;
            }
            if (sortSeq == "" || sortSeq.Length > 5)
            {
                MessageBox.Show("Please input 【Sort Seq.】\n Or input length too long (no more than 5)", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSortSeq.Select(); return;
            }
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var oSvrRfidModule = ServiceFactory<IRfidModuleDAO>.GetService();
                oSvrRfidModule.SetConnection(dbName, AppConfig.CurrentDBConn);
                RfidModule rfidModule = new RfidModule();
                rfidModule.ModuleNo = moduleNo;
                rfidModule.ModuleName = moduleNm;
                rfidModule.ModuleDict = moduleDict.Trim().Replace(" ", "").ToUpper();
                rfidModule.EnableMk = chkEnableMk.Checked ? "Y" : "N";
                rfidModule.SortSeq = sortSeq;
                string sqlName = dgvModule.SelectedRows.Count > 0 ? SqlConst.UpdRfidModulePk : SqlConst.InsRfidModule;
                oSvrRfidModule.SetPara(sqlName, dbName, rfidModule, sqlMapperId);
                int iRetVal = oSvrRfidModule.GetChange(sqlType);
                Common.CloseService(oSvrRfidModule);
                string retMsg = iRetVal > 0 ? "Save success" : "Save fail";
                if (iRetVal > 0)
                {
                    listModule = SelectModule();
                    BindModule();
                }
                MessageBox.Show(retMsg, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        //排序
        private void btnSort_Click(object sender, EventArgs e)
        {
            bool bValidLen = false;
            if (!Common.CheckChannel())
            {
                MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var oDMService = ServiceFactory<IDataManipulationDAO>.GetService();
                oDMService.SetConnection(dbName, AppConfig.CurrentDBConn);
                Dictionary<string, string> arrPara = new Dictionary<string, string>();
                RfidModule oRfidModule = new RfidModule();
                for (var i = 0; i < dgvModule.Rows.Count; i++)
                {
                    dgvModule.Rows[i].Cells["ColSortSeq"].Style.ForeColor = Color.DarkBlue;
                    oRfidModule.SortSeq = Convert.ToString(dgvModule.Rows[i].Cells["ColSortSeq"].Value);
                    if (oRfidModule.SortSeq.Length > 5)
                    {
                        dgvModule.Rows[i].Cells["ColSortSeq"].Style.ForeColor = Color.Red;
                        dgvModule.Rows[i].Cells["ColSortSeq"].Selected = true;
                        bValidLen = true;
                        break;
                    }
                    oRfidModule.ModuleNo = Convert.ToString(dgvModule.Rows[i].Cells["ColModuleNo"].Value);
                    arrPara.Add(SqlConst.UpdRfidModulePk + "#" + oRfidModule.GetType().FullName + "#2#" + i.ToString(), JsonConvert.SerializeObject(oRfidModule));
                }
                if (bValidLen)
                {
                    MessageBox.Show("【Sort Seq.】input length too long (no more than 5)", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataManipulation dataManipulation = new DataManipulation() { Data = arrPara };
                bool bResult = oDMService.ExecSave(dbName, dataManipulation, sqlMapperId);
                Common.CloseService(oDMService);
                string retMsg = bResult ? "Save success" : "Save fail";
                MessageBox.Show(retMsg, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvModule.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult oDr = MessageBox.Show("Are you sure to delete it？", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (oDr == DialogResult.OK)
            {
                try
                {
                    var oSvrRfidModule = ServiceFactory<IRfidModuleDAO>.GetService();
                    oSvrRfidModule.SetConnection(dbName, AppConfig.CurrentDBConn);
                    RfidModule rfidModule = new RfidModule();
                    rfidModule.ModuleNo = txtModuleNo.Text.Trim();
                    oSvrRfidModule.SetPara(SqlConst.DelRfidModulePk, dbName, rfidModule, sqlMapperId);
                    int iRetVal = oSvrRfidModule.GetChange(3);
                    Common.CloseService(oSvrRfidModule);
                    if (iRetVal > 0)
                    {
                        listModule = SelectModule();
                        BindModule();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
