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
using DPCtlUruNet;
using DPUruNet;
using System.Drawing.Imaging;
using System.Threading;

namespace RFIDClient
{
    public partial class FrmFingerprint : Form
    {
        int iTime = 0;
        bool bFirstTime = true;
        List<Fmd> fmdList;
        Fmd comFinger;
        DigitalPersona oDp = new DigitalPersona();
        IList<PctUser> listUser = null;
        const int PROBABILITY_ONE = 0x7fffffff;
        string sqlMapperId = Common.SqlMapperId;
        string dbName = AppConfig.CurrentDBName;
        string prevFinger = "";

        public FrmFingerprint()
        {
            InitializeComponent();
        }

        private void FrmFingerprint_Load(object sender, EventArgs e)
        {
            // For users
            toolTip1.SetToolTip(txtUser, "Support【UsrId, UsrName, UserEmail, SsoUserNo, Pccuid】fuzzy query");
            try
            {
                var oSvrPctUser = ServiceFactory<IPctUserDAO>.GetService();
                var oSvrDeptGroup = ServiceFactory<IDeptGroupDAO>.GetService();
                oSvrPctUser.SetConnection(dbName, AppConfig.CurrentDBConn);
                oSvrPctUser.SetPara(SqlConst.SelPctUserAll, dbName, new PctUser(), sqlMapperId);
                listUser = oSvrPctUser.GetData();
                Common.CloseService(oSvrPctUser);
                BindUser();
                bFirstTime = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            txtUser.Clear();
            // Fingerprint Reader
            txtMessage.Text = string.Empty;
            fmdList = new List<Fmd>();
            iTime = 0;
            chkFilter.Checked = true;
            BindFinger();
            if (!oDp.OpenReader()) { return; }
            if (!oDp.StartCaptureAsync(this.OnCaptured)) { return; }
            pbFingerprint.Image = null;
        }

        private void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                if (!oDp.CheckCaptureResult(captureResult)) return;
                iTime++;
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
                if (comFinger == null) { comFinger = resultConversion.Data; }
                if (iTime > 1)
                {
                    CompareResult compareResult = Comparison.Compare(comFinger, 0, resultConversion.Data, 0);
                    if (compareResult.Score >= (PROBABILITY_ONE / 100000))
                    {
                        iTime = 0;
                        comFinger = null;
                        fmdList = null;
                        fmdList = new List<Fmd>();
                        SendMessage(Action.SendMessage, "【Place the same finger on the reader.】");
                    }
                    else { comFinger = resultConversion.Data; }
                    if (compareResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                        throw new Exception(compareResult.ResultCode.ToString());
                }
                SendMessage(Action.SendMessage, "A finger was captured.  Times:  " + (iTime));
                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    throw new Exception(resultConversion.ResultCode.ToString());
                }
                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    pbFingerprint.Image = oDp.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                }
                fmdList.Add(resultConversion.Data);
                if (iTime >= 4)
                {
                    DataResult<Fmd> resultEnrollment = Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, fmdList);
                    if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                    {
                        SendMessage(Action.SendMessage, "An enrollment FMD was successfully created.\r\n");
                        //SendMessage(Action.SendMessage, "Place the selected finger on the reader.\r\n{$$}");
                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                        SendMessage(Action.SendMessage, "Enrollment was unsuccessful.  Please try again.");
                        SendMessage(Action.SendMessage, "{$$}Place the selected finger on the reader.\r\n");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SendMessage(Action.SendMessage, "Error:  Please try again.\r\n" + ex.Message);
            }
        }

        /// <summary>綁定手指</summary>
        private void BindFinger()
        {
            string selFinger = "";
            BindingSource oBs = new BindingSource();
            Dictionary<string, string> fingerList = AppConfig.FingerList;
            if (chkFilter.Checked)
            {
                foreach (DataGridViewRow row in dgvFingerprint.Rows)
                {
                    string key = row.Cells["ColFingerPosition"].Value == null ? "" : row.Cells["ColFingerPosition"].Value.ToString();
                    if (dgvFingerprint.CurrentRow.Index == row.Index && dgvFingerprint.SelectedRows.Count > 0) { selFinger = key; continue; }
                    if (fingerList.Keys.Contains(key)) { fingerList.Remove(key); }
                }
            }
            oBs.DataSource = null;
            cbxFinger.DataSource = null;
            if (fingerList.Count > 0) { oBs.DataSource = fingerList; }
            cbxFinger.DataSource = oBs;
            cbxFinger.DisplayMember = "Value";
            cbxFinger.ValueMember = "Key";
            if (prevFinger != "" && chkFilter.Checked)
            {
                if (Convert.ToInt32(prevFinger) > 0 && Convert.ToInt32(prevFinger) < 9)
                {
                    for (var i = Convert.ToInt32(prevFinger) + 1; i < 9 - Convert.ToInt32(prevFinger); i++)
                    {
                        selFinger = i.ToString();
                        cbxFinger.SelectedValue = selFinger;
                        if (string.IsNullOrEmpty(cbxFinger.Text))
                            continue;
                        else
                            break;
                    }
                }
                if (Convert.ToInt32(prevFinger) == 0 || Convert.ToInt32(prevFinger) == 9)
                    selFinger = cbxFinger.Items[0].ToString().Split(',')[0].Replace("[", "");
            }
            if (!chkFilter.Checked) { selFinger = "1"; }
            cbxFinger.SelectedValue = dgvFingerprint.Rows.Count == 0 ? "1" : (selFinger == "" ? cbxFinger.Items[0].ToString().Split(',')[0].Replace("[", "") : selFinger);  //預設右手食指
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
                dgvUser.Columns[i].Visible = dgvUser.Columns[i].Name.Contains("ColPccuid") || dgvUser.Columns[i].Name.Contains("ColUsrId") || dgvUser.Columns[i].Name.Contains("ColUsrName") ? true : false;
            }
            if (dgvUser.Rows.Count > 0 && bFirstTime) { dgvUser.Rows[0].Selected = false; }
            lblUserNum.Text = dgvUser.Rows.Count.ToString();
            btnDelFp.Enabled = false;
        }

        #region SendMessage
        private enum Action
        {
            SendMessage
        }
        private delegate void SendMessageCallback(Action action, string payload);
        private void SendMessage(Action action, string payload)
        {
            try
            {
                if (this.txtMessage.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            txtMessage.Text += payload.Replace("{$$}", "【" + cbxFinger.Text + "】") + "\r\n";
                            SetFontColor();
                            txtMessage.SelectionStart = txtMessage.TextLength;
                            txtMessage.ScrollToCaret();
                            if (iTime == 1 && (dgvUser.SelectedRows.Count == 0 || cbxFinger.Items.Count == 0))
                            {
                                iTime = 0;
                                fmdList.Clear();
                                proBarTimes.Value = 0;
                                txtMessage.Text = "【" + cbxFinger.Text + "】\r\nPlace the selected finger on the reader.\r\n";
                                SetFontColor();
                                if (dgvUser.SelectedRows.Count == 0)
                                    MessageBox.Show("Please select a User row", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (cbxFinger.Items.Count == 0)
                                    MessageBox.Show("Please select a finger in【Finger Position】", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (iTime > 0 && iTime <= 4 || iTime == 0 && comFinger == null)
                            {
                                lblTimes.Text = iTime + " / 4";
                                proBarTimes.Value = iTime * 25;
                            }
                            if (iTime >= 4) { SaveData(); comFinger = null; }
                            if (iTime == 1) { picStatus.Image = null; }
                            break;
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            bFirstTime = false;
            string keyword = txtUser.Text.Trim().ToUpper();
            BindUser(keyword);
        }

        /// <summary>保存</summary>
        private bool SaveData()
        {
            bool bResult = false;
            try
            {
                if (dgvUser.SelectedRows.Count == 0) { MessageBox.Show("Please select a User row", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
                if (iTime < 4) { MessageBox.Show("Please enrol at least four times.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
                if (fmdList.Count >= 4 && iTime >= 4)
                {
                    string pccuid = Convert.ToString(dgvUser.SelectedRows[0].Cells["ColPccuid"].Value);
                    DataResult<Fmd> resultEnrollment = Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, fmdList);
                    var fingerFmd = Fmd.SerializeXml(resultEnrollment.Data);
                    RfidUserFinger oRfidUserFinger = new RfidUserFinger();
                    oRfidUserFinger.Pccuid = pccuid.Trim();
                    oRfidUserFinger.FingerData = fingerFmd;
                    oRfidUserFinger.FingerPosition = cbxFinger.SelectedValue.ToString();
                    prevFinger = oRfidUserFinger.FingerPosition;
                    oRfidUserFinger.ReaderVendor = oDp.oReader.Description.Id.VendorName.Trim();
                    if (!Common.CheckChannel())
                    {
                        MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    var oDMService = ServiceFactory<IDataManipulationDAO>.GetService();
                    oDMService.SetConnection(dbName, AppConfig.CurrentDBConn);
                    Dictionary<string, string> arrPara = new Dictionary<string, string>();
                    arrPara.Add(SqlConst.DelRfidUserFingerPk + "#" + oRfidUserFinger.GetType().FullName + "#3", JsonConvert.SerializeObject(oRfidUserFinger));
                    arrPara.Add(SqlConst.InsRfidUserFinger + "#" + oRfidUserFinger.GetType().FullName + "#1", JsonConvert.SerializeObject(oRfidUserFinger));
                    DataManipulation dataManipulation = new DataManipulation() { Data = arrPara };
                    bResult = oDMService.ExecSave(dbName, dataManipulation, sqlMapperId);
                    Common.CloseService(oDMService);
                    BindFingerprint(pccuid);
                    string retMsg = bResult ? "Save successful" : "Save fail";
                    if (bResult) { picStatus.Image = Properties.Resources.Success; fmdList.Clear(); iTime = 0; }
                    else { picStatus.Image = Properties.Resources.Error; MessageBox.Show(retMsg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception) { }
            return bResult;
        }
        /// <summary>綁定指紋</summary>
        private void BindFingerprint(string pccuid)
        {
            try
            {
                if (!Common.CheckChannel())
                {
                    MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var oSvrRfidUserFinger = ServiceFactory<IRfidUserFingerDAO>.GetService();
                oSvrRfidUserFinger.SetConnection(dbName, AppConfig.CurrentDBConn);
                oSvrRfidUserFinger.SetPara(SqlConst.SelRfidUserFingerAll, dbName, new RfidUserFinger() { Pccuid = pccuid }, sqlMapperId);
                IList<RfidUserFinger> fingerList = oSvrRfidUserFinger.GetData();
                Common.CloseService(oSvrRfidUserFinger);
                dgvFingerprint.DataSource = Common.ToDataTable(fingerList);
                if (dgvFingerprint.Rows.Count > 0) { dgvFingerprint.Rows[0].Selected = false; }
                btnDelFp.Enabled = false;
                for (var i = 0; i < dgvFingerprint.Columns.Count; i++)
                {
                    dgvFingerprint.Columns[i].Visible = dgvFingerprint.Columns[i].Name.Contains("ColUserFingerId") || dgvFingerprint.Columns[i].Name.Contains("ColFingerPositionDesc") || dgvFingerprint.Columns[i].Name.Contains("ColReaderVendor") ? true : false;
                }
                if (chkFilter.Checked)
                    BindFinger();
            }
            catch (Exception) { }
        }

        //刪除指紋
        private void btnDelFp_Click(object sender, EventArgs e)
        {
            if (dgvFingerprint.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select Fingerprint row(s)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult oDr = MessageBox.Show("Are you sure to delete？", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (oDr == DialogResult.OK)
            {
                try
                {
                    if (!Common.CheckChannel())
                    {
                        MessageBox.Show(Common.CHANNEL_FAULT_INFO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var oDMService = ServiceFactory<IDataManipulationDAO>.GetService();
                    oDMService.SetConnection(dbName, AppConfig.CurrentDBConn);
                    Dictionary<string, string> arrPara = new Dictionary<string, string>();
                    for (var i = 0; i < dgvFingerprint.SelectedRows.Count; i++)
                    {
                        string tmpId = Convert.ToString(dgvFingerprint.SelectedRows[i].Cells["ColUserFingerId"].Value);
                        RfidUserFinger oRfidUserFinger = new RfidUserFinger()
                        {
                            UserFingerId = Convert.ToInt32(string.IsNullOrEmpty(tmpId) ? "0" : tmpId)
                        };
                        arrPara.Add(SqlConst.DelRfidUserFingerPk + "#" + oRfidUserFinger.GetType().FullName + "#3#" + i, JsonConvert.SerializeObject(oRfidUserFinger));
                    }
                    DataManipulation dataManipulation = new DataManipulation() { Data = arrPara };
                    bool bResult = oDMService.ExecSave(dbName, dataManipulation, sqlMapperId);
                    Common.CloseService(oDMService);
                    string retMsg = bResult ? "Delete successful" : "Delete fail";
                    if (bResult)
                    {
                        string pccuid = Convert.ToString(dgvUser.SelectedRows[0].Cells["ColPccuid"].Value);
                        BindFingerprint(pccuid);
                    }
                    else { MessageBox.Show(retMsg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch (Exception) { }
            }
        }

        private void FrmFingerprint_FormClosed(object sender, FormClosedEventArgs e)
        {
            oDp.CancelCaptureAndCloseReader(this.OnCaptured);
            oDp = null;
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (dgvUser.SelectedRows.Count == 0) { MessageBox.Show("Please select a User row", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string pccuid = Convert.ToString(dgvUser.SelectedRows[0].Cells["ColPccuid"].Value);
            BindFingerprint(pccuid);
            picStatus.Image = null;
            pbFingerprint.Image = null;
            txtMessage.ResetText();
            txtMessage.Text = "【" + cbxFinger.Text + "】\r\nPlace the selected finger on the reader.\r\n";
            SetFontColor();
            iTime = 0;
            lblTimes.Text = iTime + " / 4";
            proBarTimes.Value = iTime;
            prevFinger = "";
        }

        private void dgvFingerprint_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (dgvFingerprint.SelectedRows.Count == 0) { MessageBox.Show("Please select a Fingerprint row", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            btnDelFp.Enabled = true;
            prevFinger = "";
            if (chkFilter.Checked && dgvFingerprint.Rows.Count > 0)
                BindFinger();
        }

        private void dgvUser_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count == 0 || bFirstTime) { return; }
            string pccuid = Convert.ToString(dgvUser.SelectedRows[0].Cells["ColPccuid"].Value);
            BindFingerprint(pccuid);
        }

        private void chkLock_Click(object sender, EventArgs e)
        {
            BindFinger();
        }

        private void cbxFinger_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMessage.ResetText();
            txtMessage.Text = "【" + cbxFinger.Text + "】\r\nPlace the selected finger on the reader.\r\n";
            SetFontColor();
            iTime = 0;
            //proBarTimes.Value = iTime;
            //lblTimes.Text = iTime + " / 4";
            //picStatus.Image = null;
        }

        /// <summary>設置狀態字體顏色 </summary>
        private void SetFontColor()
        {
            int iSC = txtMessage.Text.LastIndexOf("【");
            int iEC = txtMessage.Text.LastIndexOf("】");
            txtMessage.Select(iSC + 1, iEC - iSC - 1);
            txtMessage.SelectionColor = Color.Red;
            txtMessage.Select(0, 0);
        }

    }
}
