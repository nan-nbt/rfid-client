using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Generic;
using System.Text.RegularExpressions;

namespace RFIDClient
{
    public partial class FrmSetAlien : Form
    {
        bool bInit = true;
        ReadWriteJson oReadWriteJson = null;

        public FrmSetAlien()
        {
            InitializeComponent();
        }

        private void FrmSetAlien_Load(object sender, EventArgs e)
        {
            oReadWriteJson = new ReadWriteJson();
            List<PropName> paraList = oReadWriteJson.ReadJson();
            PropName oPropName = paraList != null && paraList.Count > 0 ? paraList[0] : new PropName();
            string rfAnts = oPropName.AntennaSequence;
            string rfAtten = oPropName.RfAttenuation;
            string rssiFilter = oPropName.RssiFilter;
            string rfLevel = oPropName.RfLevel;
            string readPower = oPropName.ReadPower;
            string writePower = oPropName.WritePower;
            string writeTime = oPropName.WriteTime;
            rfAnts = string.IsNullOrEmpty(rfAnts) ? AppConfig.AntennaSequence : rfAnts;
            rfAtten = string.IsNullOrEmpty(rfAtten) ? AppConfig.RFAttenuation : rfAtten;
            if (!string.IsNullOrEmpty(rfAnts))
            {
                string[] arrAnt = rfAnts.Split(' ');
                for (var i = 0; i < arrAnt.Length; i++)
                {
                    if (arrAnt[i] == "0") { radAnt0.Checked = true; }
                    if (arrAnt[i] == "1") { radAnt1.Checked = true; }
                }
            }
            radAnt0.Checked = rfAnts == "0 1" || rfAnts == "0" ? true : false;
            txtRfAtten.Text = rfAtten;
            trackBar1.Value = Convert.ToInt32(rfAtten);
            if (rssiFilter != null && rssiFilter.Contains(" "))
            {
                txtRssiMin.Text = rssiFilter.Split(' ')[0];
                txtRssiMax.Text = rssiFilter.Split(' ')[1];
            }
            txtRfLevel.Text = rfLevel;
            txtUhfRead.Text = readPower;
            txtUhfWrite.Text = writePower;
            txtUhfTime.Text = writeTime;
            // btnSave_Click(sender, e);
            bInit = false;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            txtRfAtten.Text = trackBar1.Value.ToString();
        }

        private void txtRfAtten_TextChanged(object sender, EventArgs e)
        {
            string rfAtten = txtRfAtten.Text.Trim();
            bool isNum = CheckValue(txtRfAtten, rfAtten);
            if (!isNum) { return; }
            trackBar1.Value = rfAtten == "" ? 0 : Convert.ToInt32(rfAtten) > 120 ? 120 : Convert.ToInt32(rfAtten);
            txtRfAtten.Text = trackBar1.Value.ToString();
        }

        private void txtRssiMin_TextChanged(object sender, EventArgs e)
        {
            string rfRssiMin = txtRssiMin.Text.Trim();
            bool isNum = CheckValue(txtRssiMin, rfRssiMin);
            if (!isNum) { return; }
            txtRssiMin.Text = rfRssiMin == "" ? "0" : Convert.ToInt32(rfRssiMin) < 0 ? "0" : (Convert.ToInt32(rfRssiMin) > 60000 ? "60000" : Convert.ToInt32(rfRssiMin).ToString());
        }

        private void txtRssiMax_TextChanged(object sender, EventArgs e)
        {
            string rfRssiMax = txtRssiMax.Text.Trim();
            bool isNum = CheckValue(txtRssiMax, rfRssiMax);
            if (!isNum) { return; }
            txtRssiMax.Text = rfRssiMax == "" ? "0" : Convert.ToInt32(rfRssiMax) > 60000 ? "60000" : Convert.ToInt32(rfRssiMax).ToString();
        }

        private void txtRfLevel_TextChanged(object sender, EventArgs e)
        {
            string rfLevel = txtRfLevel.Text.Trim();
            bool isNum = CheckValue(txtRfLevel, rfLevel);
            if (!isNum) { return; }
            txtRfLevel.Text = rfLevel != "" && rfLevel != "0" ? (Convert.ToInt32(rfLevel) > 316 ? "316" : Convert.ToInt32(rfLevel).ToString()) : rfLevel;
        }

        /// <summary> 判斷l输入框是否輸入數字 </summary>
        private bool CheckValue(TextBox txtBox, string txtVal)
        {
            bool bRet = true;
            if (txtVal != "" && !Common.ChkNumber(txtVal))
            {
                MessageBox.Show("Please input a number.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBox.Text = Regex.Replace(txtVal, @"\D", "");
                txtBox.Focus();
                bRet = false;
            }
            return bRet;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string rfAnts = "";
            string rfAtten = txtRfAtten.Text.Trim();
            string rfRssiMin = txtRssiMin.Text.Trim();
            string rfRssiMax = txtRssiMax.Text.Trim();
            string rfLevel = txtRfLevel.Text.Trim();
            string readPower = txtUhfRead.Text.Trim();
            string writePower = txtUhfWrite.Text.Trim();
            string writeTime = txtUhfTime.Text.Trim();
            rfRssiMin = rfRssiMin == "" ? "0" : rfRssiMin;
            rfRssiMax = rfRssiMax == "" ? "0" : rfRssiMax;
            try
            {
                if (rfRssiMin != "" && rfRssiMax != "" && Common.ChkNumber(rfRssiMin) && Common.ChkNumber(rfRssiMax))
                {
                    if (Convert.ToInt32(rfRssiMin) > Convert.ToInt32(rfRssiMax))
                    {
                        MessageBox.Show("Fore number no more than back number.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRssiMin.Focus();
                        return;
                    }
                }
                if (radAnt0.Checked) { rfAnts += radAnt0.Tag + " "; }
                if (radAnt1.Checked) { rfAnts += radAnt1.Tag + " "; }
                rfRssiMin = rfRssiMin == "0" ? "" : rfRssiMin;
                rfRssiMax = rfRssiMax == "0" ? "" : rfRssiMax;
                PropName propName = new PropName();
                propName.AntennaSequence = rfAnts.Trim();
                propName.RfAttenuation = rfAtten;
                propName.RssiFilter = (rfRssiMin + " " + rfRssiMax).Trim();
                propName.RfLevel = rfLevel;
                propName.ReadPower = readPower;
                propName.WritePower = writePower;
                propName.WriteTime = writeTime;
                oReadWriteJson.WriteJson(propName);
                if (!bInit)
                    MessageBox.Show("Save successful.", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Common.SetAlienPara(propName);
            }
            catch (Exception ex)
            {
                if (!bInit)
                    MessageBox.Show("Save fail.\n\n" + ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDefult_Click(object sender, EventArgs e)
        {
            int iTab = tabControl1.SelectedIndex;
            if (iTab == 0)
            {
                string rfAnts = ConfigurationManager.AppSettings["AntennaSequence"].Trim();
                string rfAtten = ConfigurationManager.AppSettings["RFAttenuation"].Trim();
                string rssiFilter = ConfigurationManager.AppSettings["RssiFilter"].Trim();
                string rfLevel = ConfigurationManager.AppSettings["RFLevel"].Trim();
                if (!string.IsNullOrEmpty(rfAnts))
                {
                    string[] arrAnt = rfAnts.Split(' ');
                    for (var i = 0; i < arrAnt.Length; i++)
                    {
                        if (arrAnt[i] == "0") { radAnt0.Checked = true; }
                        if (arrAnt[i] == "1") { radAnt1.Checked = true; }
                    }
                }
                txtRfAtten.Text = rfAtten;
                if (rssiFilter.Contains(" "))
                {
                    txtRssiMin.Text = rssiFilter.Split(' ')[0];
                    txtRssiMax.Text = rssiFilter.Split(' ')[1];
                }
                txtRfLevel.Text = rfLevel;
            }
            else
            {
                txtUhfRead.Text = ConfigurationManager.AppSettings["ReadPower"].Trim();
                txtUhfWrite.Text = ConfigurationManager.AppSettings["WritePower"].Trim();
                txtUhfTime.Text = ConfigurationManager.AppSettings["WriteTime"].Trim();
            }
        }

        private void txtUhfRead_TextChanged(object sender, EventArgs e)
        {
            string txtVal = txtUhfRead.Text.Trim();
            bool isNum = CheckValue(txtUhfRead, txtVal);
            if (!isNum) { return; }
            txtUhfRead.Text = txtVal != "" && txtVal != "0" ? (Convert.ToInt32(txtVal) > 30 ? "30" : Convert.ToInt32(txtVal).ToString()) : txtVal;
        }

        private void txtUhfWrite_TextChanged(object sender, EventArgs e)
        {
            string txtVal = txtUhfWrite.Text.Trim();
            bool isNum = CheckValue(txtUhfWrite, txtVal);
            if (!isNum) { return; }
            txtUhfWrite.Text = txtVal != "" && txtVal != "0" ? (Convert.ToInt32(txtVal) > 30 ? "30" : Convert.ToInt32(txtVal).ToString()) : txtVal;
        }

        private void txtUhfTime_TextChanged(object sender, EventArgs e)
        {
            string txtVal = txtUhfTime.Text.Trim();
            bool isNum = CheckValue(txtUhfTime, txtVal);
            if (!isNum) { return; }
        }


    }
}
