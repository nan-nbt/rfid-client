using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Xml;
//Ref Others
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Generic;
using RFID.Adapter;
using RFID.Command;

namespace RFIDClient
{
    public partial class FrmTagInitial : Form
    {
        private ReaderAdapter oReader;
        private ReaderType readerType;
        private bool bolListenReceived = false;
        private string MASK_BANK_IDX = "1";
        private string MASK_BITPTR = "32";
        private string MASK_BITLEN = "96";
        private string INIT_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 00 00";
        private string MASK_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 00 00";
        private string LAST_INIT_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 00 00";
        private string RFAttenuation = AppConfig.RFAttenuation;
        private string ComTimeOutInterval = AppConfig.ComTimeOutInterval;
        private string NetworkTimeout = AppConfig.NetworkTimeout;
        ReceiveMsg oReceiveMsg = null;
        ReadWriteJson oReadWriteJson = null;

        #region "FORM EVEN"
        public FrmTagInitial()
        {
            InitializeComponent();
            this.Text = Lang.Dict("C0007", "批量初始化標籤");
            Form.CheckForIllegalCrossThreadCalls = false;
        }

        private void TagInitial_Load(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
                if (!Common.IsEnabledUSB())
                {
                    MessageBox.Show("Please Enabled USB");
                    return;
                }
                BindingSource oBs = new BindingSource();
                oBs.DataSource = AppConfig.GetReaderList;
                cbxTagType.DataSource = oBs;
                cbxTagType.DisplayMember = "Key";
                cbxTagType.ValueMember = "Value";
                cbxTagType.SelectedIndex = 0;
                SetProgMsg("Auto Searching RFID Comport");
                oBs = new BindingSource();
                oBs.DataSource = AppConfig.ComPortList;
                cbxComPort.DataSource = oBs;
                cbxComPort.DisplayMember = "Key";
                cbxComPort.ValueMember = "Value";
                cbxComPort.Text = GetReaderSerialPortNm();
                if (String.IsNullOrEmpty(cbxComPort.Text))
                {
                    SetProgMsg("Not Found RFID Comport");
                }
                else
                {
                    SetProgMsg("Found RFID Comport");
                }
                oReadWriteJson = new ReadWriteJson();
                List<PropName> paraList = oReadWriteJson.ReadJson();
                PropName oPropName = paraList != null && paraList.Count > 0 ? paraList[0] : new PropName();
                SrvStatic.comPort = oPropName.ComPort == null ? "" : oPropName.ComPort;
                SrvStatic.MinPower = string.IsNullOrEmpty(oPropName.ReadPower) ? 0 : Convert.ToInt32(oPropName.ReadPower);
                SrvStatic.MaxPower = string.IsNullOrEmpty(oPropName.WritePower) ? 0 : Convert.ToInt32(oPropName.WritePower);
                SrvStatic.TryTime = string.IsNullOrEmpty(oPropName.WriteTime) ? 0 : Convert.ToInt32(oPropName.WriteTime);
                cbxTagType.SelectedValue = oPropName != null && oPropName.ReaderType != null ? oPropName.ReaderType : "0";
                cbxComPort.SelectedValue = SrvStatic.comPort;
                oReceiveMsg = new ReceiveMsg(ReceiveMsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TagInitial_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                oReader = SetReaderAttr(oReader, -1);
                DisconnectReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void imgReaderStatus_Click(object sender, EventArgs e)
        {
            string strComport = cbxComPort.Text;
            if (!Common.IsEnabledUSB())
            {
                MessageBox.Show("Please Enabled USB");
                return;
            }

            if (String.IsNullOrEmpty(strComport))
            {
                strComport = GetReaderSerialPortNm();
                if (String.IsNullOrEmpty(strComport))
                {
                    MessageBox.Show("Please Choice Comport");
                    return;
                }
                cbxComPort.Text = strComport;
            }

            try
            {
                SetCounted(0);
                oReader = SetReaderAttr(oReader, -1);
                if (DisconnectReader())
                    return;

                cbxComPort.Enabled = false;
                cbxTagType.Enabled = false;
                readerType = (ReaderType)Convert.ToInt32(cbxTagType.SelectedValue);
                oReader = new ReaderAdapter(readerType);//ReaderType.ALIEN
                oReader.SetSerialPort(strComport);
                oReader.OpenConnect();
                oReader.iReader.SetPower();
                oReader.iReader.WriteTime(SrvStatic.MinPower, SrvStatic.MaxPower, SrvStatic.TryTime);
                if (!oReader.IsConnected())
                {
                    SetProgMsg("Can Not Connect RFID Reader");
                    oReader.Disconnect();
                    oReader = null;
                    cbxComPort.Enabled = true;
                    cbxTagType.Enabled = true;
                    return;
                }
                SetProgMsg("Connect Reader");
                txtAutoEPC.Text = INIT_EPC_DATA;
                txtManualEPC.Text = INIT_EPC_DATA;
                if (oReadWriteJson == null) { oReadWriteJson = new ReadWriteJson(); }
                oReadWriteJson.WriteJson(new PropName() { ReaderType = Convert.ToString(cbxTagType.SelectedValue), ComPort = Convert.ToString(cbxComPort.SelectedValue) });
                oReadWriteJson = null;
                SetTabControl();
                imgReaderStatus.Image = Properties.Resources.connect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                DisconnectReader();
            }
        }

        private void btnRecount_Click(object sender, EventArgs e)
        {
            try
            {
                if (oReader != null)
                {
                    SetCounted(0);
                    if (!oReader.IsConnected() && DisconnectReader())
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnReadTag_Click(object sender, EventArgs e)
        {
            try
            {
                List<TagStruct> lstRfidTag;
                if (oReader != null)
                {
                    if (!oReader.IsConnected() && DisconnectReader())
                        return;
                    SetProgMsg("Read Tags");
                    lstRfidTag = oReader.GetTagList();
                    lstBoxTags.Items.Clear();
                    lstBoxTags.Items.AddRange(lstRfidTag.Select(a => a.TagID).ToArray<string>());
                    if (lstRfidTag.Count == 0)
                    {
                        SetProgMsg("No Tags Can Read");
                        lstBoxTags.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnWriteTag_Click(object sender, EventArgs e)
        {
            List<TagStruct> lstRfidTag;
            string tagId = "";
            string status = "";
            try
            {
                if (oReader != null)
                {
                    if (!oReader.IsConnected() && DisconnectReader())
                        return;
                    tagId = FormatStr(Convert.ToString(lstBoxTags.SelectedItem));
                    if (String.IsNullOrEmpty(tagId)) return;
                    //oReader.SetAcqG2Mask(MASK_BANK_IDX, MASK_BITPTR, MASK_BITLEN, tagId);
                    oReader.SetReaderBySingle(ParameterNm.SetAcqG2Mask, new Parameters()
                    {
                        MEMORY_BANK = MASK_BANK_IDX,
                        BITLEN = MASK_BITLEN,
                        BITPTR = MASK_BITPTR,
                        HEX_BYTES = tagId
                    });
                    //oReader.AcqG2MaskAction = "Include";
                    oReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Include");
                    //oReader.G2Read(eG2Bank.EPC, "2", "6")
                    string curTag = oReader.ReadTagByBank(1, "2", "6");
                    if (curTag.Replace(" ", "") == tagId.Replace(" ", ""))
                        status = oReader.ProgTagBySingle(1, INIT_EPC_DATA);  //oReader.ProgramEPC(INIT_EPC_DATA);
                    if (status.Replace(" ", "") == INIT_EPC_DATA.Replace(" ", ""))
                        SetProgMsg("Write Tag Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                oReader = SetReaderAttr(oReader, 1);
                if (oReader != null && oReader.IsConnected())
                {
                    lstRfidTag = oReader.GetTagList();
                    lstBoxTags.Items.Clear();
                    lstBoxTags.Items.AddRange(lstRfidTag.Select(a => a.TagID).ToArray<string>());
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetTabControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbxTagType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbxTagType = sender as ComboBox;
            LAST_INIT_EPC_DATA = INIT_EPC_DATA;
            switch (cbxTagType.Text)
            {
                case "Alien":
                    MASK_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 00 00";
                    INIT_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 00 00";
                    break;
                case "NPX":
                    MASK_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 11 11";
                    INIT_EPC_DATA = "00 00 00 00 00 00 00 00 00 00 11 11";
                    break;
            }
        }

        private void btnClearProgMsg_Click(object sender, EventArgs e)
        {
            txtProgMsg.Text = "";
        }
        #endregion

        #region "FUNCTION"
        private string GetReaderSerialPortNm()
        {
            string portNm = cbxComPort.Text;
            string[] arrPortName;
            if (String.IsNullOrEmpty(portNm))
            {
                arrPortName = SerialPort.GetPortNames();
                foreach (var item in arrPortName)
                {
                    if (item != "COM1")
                    {
                        portNm = item;
                    }
                }
            }
            return portNm;
        }

        private void ReceiveMsg(string msg)
        {
            int oNowCount;
            try
            {
                if (oReader != null && oReader.IsConnected() && !String.IsNullOrEmpty(msg))
                {
                    oNowCount = Convert.ToInt32(lblCounted.Text);
                    msg = msg.Replace("\0", "").Replace("\r\n", "");
                    if (msg.IndexOf("AUTO MODE EVALUATES TRUE") != -1)
                    {
                        oNowCount = readerType.Equals(ReaderType.ALIEN_UHF) ? oReader.GetSuccQty() : oNowCount + 1;
                        SetCounted(oNowCount);
                    }
                }
            }
            catch
            {

            }
        }

        private void SetTabControl()
        {
            int tabIdx = tabControl.SelectedIndex;
            List<TagStruct> lstRfidTag;
            if (oReader != null)
            {
                if (!oReader.IsConnected() && DisconnectReader())
                    return;
                switch (tabIdx)
                {
                    case 0:
                        SetProgMsg("Automatic Init Tags");
                        if (!bolListenReceived)
                        {
                            oReader.AddMessageReceived(oReceiveMsg);
                            bolListenReceived = true;
                        }
                        oReader = SetReaderAttr(oReader, tabIdx);
                        break;
                    case 1:
                        SetProgMsg("Read Tags");
                        if (bolListenReceived)
                        {
                            oReader.RemoveMessageReceived(oReceiveMsg);
                            bolListenReceived = false;
                        }
                        oReader = SetReaderAttr(oReader, tabIdx);
                        lstRfidTag = oReader.GetTagList();
                        lstBoxTags.Items.Clear();
                        lstBoxTags.Items.AddRange(lstRfidTag.Select(a => a.TagID).ToArray<string>());
                        if (lstRfidTag.Count == 0) SetProgMsg("No Tags Can Read");
                        break;
                }
            }
        }

        private void SetProgMsg(string msg)
        {
            if (String.IsNullOrEmpty(msg))
                return;
            txtProgMsg.Text = DateTime.Now.ToString("HH:mm:ss") + " " + msg + "\r\n" + txtProgMsg.Text;
        }

        private void SetCounted(int iCounted)
        {
            if (iCounted == 0 && lblCounted.Text != "0")
            {
                SetProgMsg("Reset Counted Tag");
            }
            lblCounted.Text = Convert.ToString(iCounted);
        }

        private bool DisconnectReader()
        {
            bool bolOk = false;
            if (oReader != null)
            {
                oReader.iReader.SetPower();
                if (oReader.IsConnected()) { oReader.Disconnect(); }
                imgReaderStatus.Image = Properties.Resources.disconnect;
                if (bolListenReceived)
                {
                    oReader.RemoveMessageReceived(oReceiveMsg);
                    bolListenReceived = false;
                }
                oReader.Disconnect();
                oReader = null;
                cbxComPort.Enabled = true;
                cbxTagType.Enabled = true;
                SetCounted(0);
                lstBoxTags.Items.Clear();
                SetProgMsg("Disconnect Reader");
                bolOk = true;
            }
            return bolOk;
        }

        public string FormatStr(string vPara, int iChar = 2)
        {
            string newStr = "";
            char[] arrChar = vPara.Replace(" ", "").ToCharArray();
            for (int i = 0; i < arrChar.Length; i++)
            {
                newStr += arrChar[i];
                if ((i + 1) % iChar == 0)
                    newStr += " ";
            }
            return newStr.Trim();
        }

        private ReaderAdapter SetReaderAttr(ReaderAdapter vReader, int type)
        {
            if (vReader != null && vReader.IsConnected())
            {
                switch (type)
                {
                    case -1:
                        vReader.SetReaderBySingle(ParameterNm.ComTimeOutInterval, ComTimeOutInterval);
                        vReader.SetReaderBySingle(ParameterNm.NetworkTimeout, NetworkTimeout);
                        vReader.SetReaderBySingle(ParameterNm.RFAttenuation, RFAttenuation);
                        vReader.SetReaderBySingle(ParameterNm.AcqG2Mask, "0");
                        vReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Include");
                        vReader.SetReaderBySingle(ParameterNm.ProgEPCData, INIT_EPC_DATA);
                        vReader.SetReaderBySingle(ParameterNm.ProgEPCDataInc, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.NotifyMode, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.NotifyTrigger, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.AutoMode, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.AutoAction, "Acquire");
                        break;
                    case 0://Automatic
                        vReader.SetReaderBySingle(ParameterNm.PersistTime, "-1");
                        vReader.SetReaderBySingle(ParameterNm.SetAcqG2Mask, new Parameters()
                        {
                            MEMORY_BANK = MASK_BANK_IDX,
                            BITLEN = MASK_BITLEN,
                            BITPTR = MASK_BITPTR,
                            HEX_BYTES = MASK_EPC_DATA
                        });
                        vReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Exclude");
                        vReader.SetReaderBySingle(ParameterNm.ProgEPCData, INIT_EPC_DATA);
                        vReader.SetReaderBySingle(ParameterNm.ProgEPCDataInc, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.ProgDataUnit, "WORD");
                        vReader.SetReaderBySingle(ParameterNm.NotifyMode, "ON");
                        vReader.SetReaderBySingle(ParameterNm.NotifyTime, "0");
                        vReader.SetReaderBySingle(ParameterNm.NotifyAddress, "SERIAL");
                        vReader.SetReaderBySingle(ParameterNm.NotifyHeader, "ON");
                        vReader.SetReaderBySingle(ParameterNm.NotifyFormat, "Terse");
                        vReader.SetReaderBySingle(ParameterNm.NotifyTrigger, "True");
                        vReader.SetReaderBySingle(ParameterNm.AutoMode, "ON");
                        vReader.SetReaderBySingle(ParameterNm.AutoAction, "ProgramEPC");
                        break;
                    case 1://Manual
                        vReader.SetReaderBySingle(ParameterNm.PersistTime, "-1");
                        vReader.SetReaderBySingle(ParameterNm.TagListFormat, "Terse");
                        vReader.SetReaderBySingle(ParameterNm.SetAcqG2Mask, new Parameters()
                        {
                            MEMORY_BANK = MASK_BANK_IDX,
                            BITLEN = MASK_BITLEN,
                            BITPTR = MASK_BITPTR,
                            HEX_BYTES = MASK_EPC_DATA
                        });
                        vReader.SetReaderBySingle(ParameterNm.AcqG2MaskAction, "Exclude");
                        vReader.SetReaderBySingle(ParameterNm.ProgEPCData, INIT_EPC_DATA);
                        vReader.SetReaderBySingle(ParameterNm.ProgEPCDataInc, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.ProgDataUnit, "WORD");
                        vReader.SetReaderBySingle(ParameterNm.NotifyMode, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.NotifyAddress, "SERIAL");
                        vReader.SetReaderBySingle(ParameterNm.NotifyHeader, "ON");
                        vReader.SetReaderBySingle(ParameterNm.NotifyFormat, "Terse");
                        vReader.SetReaderBySingle(ParameterNm.NotifyTrigger, "True");
                        vReader.SetReaderBySingle(ParameterNm.AutoMode, "OFF");
                        vReader.SetReaderBySingle(ParameterNm.AutoAction, "Acquire");
                        break;
                }
            }
            return vReader;
        }
        #endregion
    }
}