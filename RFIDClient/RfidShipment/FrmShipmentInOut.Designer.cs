namespace RFIDClient
{
    partial class FrmShipmentInOut
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxBrand = new System.Windows.Forms.ComboBox();
            this.cbxStock = new System.Windows.Forms.ComboBox();
            this.cbxInOut = new System.Windows.Forms.ComboBox();
            this.cbxOutReason = new System.Windows.Forms.ComboBox();
            this.cbxOutSelection = new System.Windows.Forms.ComboBox();
            this.cbxOutUser = new System.Windows.Forms.ComboBox();
            this.cbxComPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxPostNo = new System.Windows.Forms.ComboBox();
            this.tabShipment = new System.Windows.Forms.TabControl();
            this.tabShipmentIn = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxInUser = new System.Windows.Forms.ComboBox();
            this.tabShipmentOut = new System.Windows.Forms.TabPage();
            this.btnChkUser = new System.Windows.Forms.Button();
            this.tabProgramTag = new System.Windows.Forms.TabPage();
            this.btnProgTag = new System.Windows.Forms.Button();
            this.tbxProgramCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxOrginCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxBarCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lbxMsg = new System.Windows.Forms.ListBox();
            this.chxAutomatic = new System.Windows.Forms.CheckBox();
            this.chxSemiautomatic = new System.Windows.Forms.CheckBox();
            this.lblOldBarCode = new System.Windows.Forms.Label();
            this.dgHistory = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Article = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RfidCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DemMk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chxManual = new System.Windows.Forms.CheckBox();
            this.lblCounted = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabShipment.SuspendLayout();
            this.tabShipmentIn.SuspendLayout();
            this.tabShipmentOut.SuspendLayout();
            this.tabProgramTag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brand No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stock No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "InOut No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Reason No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "Out Selection";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(458, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Out User";
            // 
            // cbxBrand
            // 
            this.cbxBrand.BackColor = System.Drawing.Color.White;
            this.cbxBrand.FormattingEnabled = true;
            this.cbxBrand.Location = new System.Drawing.Point(85, 40);
            this.cbxBrand.Name = "cbxBrand";
            this.cbxBrand.Size = new System.Drawing.Size(121, 20);
            this.cbxBrand.TabIndex = 7;
            this.cbxBrand.TextChanged += new System.EventHandler(this.cbxBrand_TextChanged);
            // 
            // cbxStock
            // 
            this.cbxStock.BackColor = System.Drawing.Color.White;
            this.cbxStock.FormattingEnabled = true;
            this.cbxStock.Location = new System.Drawing.Point(266, 40);
            this.cbxStock.Name = "cbxStock";
            this.cbxStock.Size = new System.Drawing.Size(121, 20);
            this.cbxStock.TabIndex = 8;
            this.cbxStock.SelectedIndexChanged += new System.EventHandler(this.cbxStock_SelectedIndexChanged);
            this.cbxStock.TextChanged += new System.EventHandler(this.cbxStock_TextChanged);
            // 
            // cbxInOut
            // 
            this.cbxInOut.BackColor = System.Drawing.Color.White;
            this.cbxInOut.FormattingEnabled = true;
            this.cbxInOut.Location = new System.Drawing.Point(448, 40);
            this.cbxInOut.Name = "cbxInOut";
            this.cbxInOut.Size = new System.Drawing.Size(121, 20);
            this.cbxInOut.TabIndex = 9;
            this.cbxInOut.TextChanged += new System.EventHandler(this.cbxInOut_TextChanged);
            // 
            // cbxOutReason
            // 
            this.cbxOutReason.FormattingEnabled = true;
            this.cbxOutReason.Location = new System.Drawing.Point(65, 6);
            this.cbxOutReason.Name = "cbxOutReason";
            this.cbxOutReason.Size = new System.Drawing.Size(133, 20);
            this.cbxOutReason.TabIndex = 10;
            this.cbxOutReason.TextChanged += new System.EventHandler(this.cbxOutReason_TextChanged);
            // 
            // cbxOutSelection
            // 
            this.cbxOutSelection.FormattingEnabled = true;
            this.cbxOutSelection.Location = new System.Drawing.Point(284, 7);
            this.cbxOutSelection.Name = "cbxOutSelection";
            this.cbxOutSelection.Size = new System.Drawing.Size(150, 20);
            this.cbxOutSelection.TabIndex = 11;
            this.cbxOutSelection.TextChanged += new System.EventHandler(this.cbxOutSelection_TextChanged);
            // 
            // cbxOutUser
            // 
            this.cbxOutUser.FormattingEnabled = true;
            this.cbxOutUser.Location = new System.Drawing.Point(510, 7);
            this.cbxOutUser.Name = "cbxOutUser";
            this.cbxOutUser.Size = new System.Drawing.Size(313, 20);
            this.cbxOutUser.TabIndex = 12;
            this.cbxOutUser.SelectedIndexChanged += new System.EventHandler(this.cbxOutUser_SelectedIndexChanged);
            this.cbxOutUser.TextUpdate += new System.EventHandler(this.cbxOutUser_TextUpdate);
            // 
            // cbxComPort
            // 
            this.cbxComPort.Enabled = false;
            this.cbxComPort.FormattingEnabled = true;
            this.cbxComPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.cbxComPort.Location = new System.Drawing.Point(85, 10);
            this.cbxComPort.Name = "cbxComPort";
            this.cbxComPort.Size = new System.Drawing.Size(121, 20);
            this.cbxComPort.TabIndex = 22;
            this.cbxComPort.TextChanged += new System.EventHandler(this.cbxComPort_TextChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(4, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 20);
            this.btnConnect.TabIndex = 21;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "Pos No";
            // 
            // cbxPostNo
            // 
            this.cbxPostNo.FormattingEnabled = true;
            this.cbxPostNo.Location = new System.Drawing.Point(48, 6);
            this.cbxPostNo.Name = "cbxPostNo";
            this.cbxPostNo.Size = new System.Drawing.Size(230, 20);
            this.cbxPostNo.TabIndex = 26;
            this.cbxPostNo.SelectedIndexChanged += new System.EventHandler(this.cbxPostNo_SelectedIndexChanged);
            // 
            // tabShipment
            // 
            this.tabShipment.Controls.Add(this.tabShipmentIn);
            this.tabShipment.Controls.Add(this.tabShipmentOut);
            this.tabShipment.Controls.Add(this.tabProgramTag);
            this.tabShipment.Location = new System.Drawing.Point(4, 63);
            this.tabShipment.Margin = new System.Windows.Forms.Padding(0);
            this.tabShipment.Name = "tabShipment";
            this.tabShipment.SelectedIndex = 0;
            this.tabShipment.Size = new System.Drawing.Size(1066, 61);
            this.tabShipment.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabShipment.TabIndex = 27;
            this.tabShipment.SelectedIndexChanged += new System.EventHandler(this.tabShipment_SelectedIndexChanged);
            this.tabShipment.DoubleClick += new System.EventHandler(this.tabShipment_DoubleClick);
            // 
            // tabShipmentIn
            // 
            this.tabShipmentIn.Controls.Add(this.label10);
            this.tabShipmentIn.Controls.Add(this.cbxInUser);
            this.tabShipmentIn.Controls.Add(this.label7);
            this.tabShipmentIn.Controls.Add(this.cbxPostNo);
            this.tabShipmentIn.Location = new System.Drawing.Point(4, 22);
            this.tabShipmentIn.Name = "tabShipmentIn";
            this.tabShipmentIn.Padding = new System.Windows.Forms.Padding(3);
            this.tabShipmentIn.Size = new System.Drawing.Size(1058, 35);
            this.tabShipmentIn.TabIndex = 1;
            this.tabShipmentIn.Text = "Stock In";
            this.tabShipmentIn.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "In User";
            // 
            // cbxInUser
            // 
            this.cbxInUser.FormattingEnabled = true;
            this.cbxInUser.Location = new System.Drawing.Point(360, 6);
            this.cbxInUser.Name = "cbxInUser";
            this.cbxInUser.Size = new System.Drawing.Size(313, 20);
            this.cbxInUser.TabIndex = 28;
            this.cbxInUser.SelectedIndexChanged += new System.EventHandler(this.cbxInUser_SelectedIndexChanged);
            this.cbxInUser.TextUpdate += new System.EventHandler(this.cbxInUser_TextUpdate);
            // 
            // tabShipmentOut
            // 
            this.tabShipmentOut.Controls.Add(this.btnChkUser);
            this.tabShipmentOut.Controls.Add(this.label4);
            this.tabShipmentOut.Controls.Add(this.cbxOutSelection);
            this.tabShipmentOut.Controls.Add(this.cbxOutReason);
            this.tabShipmentOut.Controls.Add(this.label5);
            this.tabShipmentOut.Controls.Add(this.label6);
            this.tabShipmentOut.Controls.Add(this.cbxOutUser);
            this.tabShipmentOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabShipmentOut.Location = new System.Drawing.Point(4, 22);
            this.tabShipmentOut.Name = "tabShipmentOut";
            this.tabShipmentOut.Padding = new System.Windows.Forms.Padding(3);
            this.tabShipmentOut.Size = new System.Drawing.Size(1058, 35);
            this.tabShipmentOut.TabIndex = 0;
            this.tabShipmentOut.Text = "Stock Out";
            this.tabShipmentOut.UseVisualStyleBackColor = true;
            // 
            // btnChkUser
            // 
            this.btnChkUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkUser.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnChkUser.ForeColor = System.Drawing.Color.Navy;
            this.btnChkUser.Location = new System.Drawing.Point(848, 7);
            this.btnChkUser.Name = "btnChkUser";
            this.btnChkUser.Size = new System.Drawing.Size(81, 20);
            this.btnChkUser.TabIndex = 22;
            this.btnChkUser.Text = "Check User";
            this.btnChkUser.UseVisualStyleBackColor = true;
            this.btnChkUser.Click += new System.EventHandler(this.btnChkUser_Click);
            // 
            // tabProgramTag
            // 
            this.tabProgramTag.Controls.Add(this.btnProgTag);
            this.tabProgramTag.Controls.Add(this.tbxProgramCode);
            this.tabProgramTag.Controls.Add(this.label9);
            this.tabProgramTag.Controls.Add(this.tbxOrginCode);
            this.tabProgramTag.Controls.Add(this.label8);
            this.tabProgramTag.Location = new System.Drawing.Point(4, 22);
            this.tabProgramTag.Name = "tabProgramTag";
            this.tabProgramTag.Padding = new System.Windows.Forms.Padding(3);
            this.tabProgramTag.Size = new System.Drawing.Size(1058, 35);
            this.tabProgramTag.TabIndex = 2;
            this.tabProgramTag.Text = "Program Tag";
            this.tabProgramTag.UseVisualStyleBackColor = true;
            // 
            // btnProgTag
            // 
            this.btnProgTag.Location = new System.Drawing.Point(523, 5);
            this.btnProgTag.Name = "btnProgTag";
            this.btnProgTag.Size = new System.Drawing.Size(75, 23);
            this.btnProgTag.TabIndex = 40;
            this.btnProgTag.Text = "Program Tag";
            this.btnProgTag.UseVisualStyleBackColor = true;
            this.btnProgTag.Click += new System.EventHandler(this.btnProgTag_Click);
            // 
            // tbxProgramCode
            // 
            this.tbxProgramCode.Location = new System.Drawing.Point(337, 6);
            this.tbxProgramCode.MaxLength = 24;
            this.tbxProgramCode.Name = "tbxProgramCode";
            this.tbxProgramCode.Size = new System.Drawing.Size(180, 21);
            this.tbxProgramCode.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(258, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "Program Code";
            // 
            // tbxOrginCode
            // 
            this.tbxOrginCode.Location = new System.Drawing.Point(72, 6);
            this.tbxOrginCode.MaxLength = 24;
            this.tbxOrginCode.Name = "tbxOrginCode";
            this.tbxOrginCode.Size = new System.Drawing.Size(180, 21);
            this.tbxOrginCode.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Orgin Code";
            // 
            // tbxBarCode
            // 
            this.tbxBarCode.Enabled = false;
            this.tbxBarCode.Location = new System.Drawing.Point(624, 38);
            this.tbxBarCode.Name = "tbxBarCode";
            this.tbxBarCode.Size = new System.Drawing.Size(121, 21);
            this.tbxBarCode.TabIndex = 33;
            this.tbxBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxBarCode_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(582, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 32;
            this.label13.Text = "BarNo";
            // 
            // lbxMsg
            // 
            this.lbxMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbxMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbxMsg.FormattingEnabled = true;
            this.lbxMsg.ItemHeight = 12;
            this.lbxMsg.Location = new System.Drawing.Point(0, 588);
            this.lbxMsg.Name = "lbxMsg";
            this.lbxMsg.Size = new System.Drawing.Size(1070, 64);
            this.lbxMsg.TabIndex = 30;
            this.lbxMsg.DoubleClick += new System.EventHandler(this.lbxMsg_DoubleClick);
            // 
            // chxAutomatic
            // 
            this.chxAutomatic.AutoSize = true;
            this.chxAutomatic.Enabled = false;
            this.chxAutomatic.Location = new System.Drawing.Point(214, 12);
            this.chxAutomatic.Name = "chxAutomatic";
            this.chxAutomatic.Size = new System.Drawing.Size(78, 16);
            this.chxAutomatic.TabIndex = 32;
            this.chxAutomatic.Text = "Automatic";
            this.chxAutomatic.UseVisualStyleBackColor = true;
            this.chxAutomatic.CheckedChanged += new System.EventHandler(this.chxAutomatic_CheckedChanged);
            // 
            // chxSemiautomatic
            // 
            this.chxSemiautomatic.AutoSize = true;
            this.chxSemiautomatic.Enabled = false;
            this.chxSemiautomatic.Location = new System.Drawing.Point(292, 12);
            this.chxSemiautomatic.Name = "chxSemiautomatic";
            this.chxSemiautomatic.Size = new System.Drawing.Size(102, 16);
            this.chxSemiautomatic.TabIndex = 34;
            this.chxSemiautomatic.Text = "Semiautomatic";
            this.chxSemiautomatic.UseVisualStyleBackColor = true;
            this.chxSemiautomatic.CheckedChanged += new System.EventHandler(this.chxSemiautomatic_CheckedChanged);
            // 
            // lblOldBarCode
            // 
            this.lblOldBarCode.AutoSize = true;
            this.lblOldBarCode.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOldBarCode.ForeColor = System.Drawing.Color.Red;
            this.lblOldBarCode.Location = new System.Drawing.Point(621, 11);
            this.lblOldBarCode.Name = "lblOldBarCode";
            this.lblOldBarCode.Size = new System.Drawing.Size(106, 16);
            this.lblOldBarCode.TabIndex = 35;
            this.lblOldBarCode.Text = "Old Bar Code";
            this.lblOldBarCode.Visible = false;
            // 
            // dgHistory
            // 
            this.dgHistory.AllowUserToAddRows = false;
            this.dgHistory.AllowUserToDeleteRows = false;
            this.dgHistory.AllowUserToResizeRows = false;
            this.dgHistory.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.PosNo,
            this.Model,
            this.Article,
            this.SpecNo,
            this.Size,
            this.LR,
            this.BarcodeNo,
            this.RfidCode,
            this.DemMk});
            this.dgHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHistory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgHistory.Location = new System.Drawing.Point(0, 129);
            this.dgHistory.Name = "dgHistory";
            this.dgHistory.ReadOnly = true;
            this.dgHistory.RowHeadersVisible = false;
            this.dgHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgHistory.RowTemplate.Height = 24;
            this.dgHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistory.ShowCellToolTips = false;
            this.dgHistory.Size = new System.Drawing.Size(856, 459);
            this.dgHistory.StandardTab = true;
            this.dgHistory.TabIndex = 36;
            this.dgHistory.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgHistory_RowsAdded);
            this.dgHistory.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgHistory_RowsRemoved);
            this.dgHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgHistory_KeyDown);
            this.dgHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgHistory_MouseDown);
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 50;
            // 
            // PosNo
            // 
            this.PosNo.HeaderText = "Pos No";
            this.PosNo.Name = "PosNo";
            this.PosNo.ReadOnly = true;
            this.PosNo.Width = 60;
            // 
            // Model
            // 
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // Article
            // 
            this.Article.HeaderText = "Article";
            this.Article.Name = "Article";
            this.Article.ReadOnly = true;
            this.Article.Width = 150;
            // 
            // SpecNo
            // 
            this.SpecNo.HeaderText = "Spec. No. Ver";
            this.SpecNo.Name = "SpecNo";
            this.SpecNo.ReadOnly = true;
            this.SpecNo.Width = 110;
            // 
            // Size
            // 
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.Width = 30;
            // 
            // LR
            // 
            this.LR.HeaderText = "L/R";
            this.LR.Name = "LR";
            this.LR.ReadOnly = true;
            this.LR.Width = 30;
            // 
            // BarcodeNo
            // 
            this.BarcodeNo.HeaderText = "Barcode No.";
            this.BarcodeNo.Name = "BarcodeNo";
            this.BarcodeNo.ReadOnly = true;
            // 
            // RfidCode
            // 
            this.RfidCode.HeaderText = "RFID Code";
            this.RfidCode.Name = "RfidCode";
            this.RfidCode.ReadOnly = true;
            this.RfidCode.Width = 150;
            // 
            // DemMk
            // 
            this.DemMk.HeaderText = "DemMk";
            this.DemMk.Name = "DemMk";
            this.DemMk.ReadOnly = true;
            this.DemMk.Width = 50;
            // 
            // chxManual
            // 
            this.chxManual.AutoSize = true;
            this.chxManual.Enabled = false;
            this.chxManual.Location = new System.Drawing.Point(390, 12);
            this.chxManual.Name = "chxManual";
            this.chxManual.Size = new System.Drawing.Size(60, 16);
            this.chxManual.TabIndex = 37;
            this.chxManual.Text = "Manual";
            this.chxManual.UseVisualStyleBackColor = true;
            this.chxManual.CheckedChanged += new System.EventHandler(this.chxManual_CheckedChanged);
            // 
            // lblCounted
            // 
            this.lblCounted.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblCounted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCounted.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCounted.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCounted.Font = new System.Drawing.Font("微軟正黑體", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCounted.ForeColor = System.Drawing.Color.Blue;
            this.lblCounted.Location = new System.Drawing.Point(856, 129);
            this.lblCounted.Name = "lblCounted";
            this.lblCounted.Size = new System.Drawing.Size(214, 459);
            this.lblCounted.TabIndex = 38;
            this.lblCounted.Tag = "";
            this.lblCounted.Text = "0";
            this.lblCounted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 129);
            this.panel1.TabIndex = 39;
            // 
            // FrmShipmentInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 652);
            this.Controls.Add(this.dgHistory);
            this.Controls.Add(this.lblCounted);
            this.Controls.Add(this.chxAutomatic);
            this.Controls.Add(this.chxManual);
            this.Controls.Add(this.chxSemiautomatic);
            this.Controls.Add(this.lblOldBarCode);
            this.Controls.Add(this.tbxBarCode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lbxMsg);
            this.Controls.Add(this.tabShipment);
            this.Controls.Add(this.cbxBrand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxInOut);
            this.Controls.Add(this.cbxComPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxStock);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmShipmentInOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Step Scan";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmShipmentInOut_FormClosed);
            this.Load += new System.EventHandler(this.FrmShipmentInOut_Load);
            this.tabShipment.ResumeLayout(false);
            this.tabShipmentIn.ResumeLayout(false);
            this.tabShipmentIn.PerformLayout();
            this.tabShipmentOut.ResumeLayout(false);
            this.tabShipmentOut.PerformLayout();
            this.tabProgramTag.ResumeLayout(false);
            this.tabProgramTag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxBrand;
        private System.Windows.Forms.ComboBox cbxStock;
        private System.Windows.Forms.ComboBox cbxInOut;
        private System.Windows.Forms.ComboBox cbxOutReason;
        private System.Windows.Forms.ComboBox cbxOutSelection;
        private System.Windows.Forms.ComboBox cbxOutUser;
        private System.Windows.Forms.ComboBox cbxComPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxPostNo;
        private System.Windows.Forms.TabControl tabShipment;
        private System.Windows.Forms.TabPage tabShipmentOut;
        private System.Windows.Forms.TabPage tabShipmentIn;
        private System.Windows.Forms.ListBox lbxMsg;
        private System.Windows.Forms.TextBox tbxBarCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chxAutomatic;
        private System.Windows.Forms.CheckBox chxSemiautomatic;
        private System.Windows.Forms.Label lblOldBarCode;
        private System.Windows.Forms.DataGridView dgHistory;
        private System.Windows.Forms.CheckBox chxManual;
        private System.Windows.Forms.Label lblCounted;
        private System.Windows.Forms.TabPage tabProgramTag;
        private System.Windows.Forms.TextBox tbxOrginCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxProgramCode;
        private System.Windows.Forms.Button btnProgTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Article;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn LR;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RfidCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DemMk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxInUser;
        private System.Windows.Forms.Button btnChkUser;
    }
}