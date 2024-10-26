namespace RFIDClient
{
    partial class FrmAccessControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccessControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.picComPort = new System.Windows.Forms.PictureBox();
            this.picConn = new System.Windows.Forms.PictureBox();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCurrCode = new System.Windows.Forms.Label();
            this.lblOrigCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.menuStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelHistoryR = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelTag = new System.Windows.Forms.Panel();
            this.panelTagR = new System.Windows.Forms.Panel();
            this.panelCount = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblOkNum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDemagnetize = new System.Windows.Forms.Button();
            this.btnMagnetize = new System.Windows.Forms.Button();
            this.panelScan = new System.Windows.Forms.Panel();
            this.col_barno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_article = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_spec_ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_left_right = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_rfid_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dem_mk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.menuStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelTag.SuspendLayout();
            this.panelTagR.SuspendLayout();
            this.panelCount.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelScan.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.panel4);
            this.panelTop.Controls.Add(this.picConn);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(991, 47);
            this.panelTop.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbPortName);
            this.panel4.Controls.Add(this.picComPort);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(858, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(133, 47);
            this.panel4.TabIndex = 23;
            // 
            // cbPortName
            // 
            this.cbPortName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(47, 12);
            this.cbPortName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(79, 24);
            this.cbPortName.TabIndex = 12;
            // 
            // picComPort
            // 
            this.picComPort.Dock = System.Windows.Forms.DockStyle.Left;
            this.picComPort.Image = ((System.Drawing.Image)(resources.GetObject("picComPort.Image")));
            this.picComPort.InitialImage = null;
            this.picComPort.Location = new System.Drawing.Point(0, 0);
            this.picComPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picComPort.Name = "picComPort";
            this.picComPort.Size = new System.Drawing.Size(47, 47);
            this.picComPort.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picComPort.TabIndex = 21;
            this.picComPort.TabStop = false;
            // 
            // picConn
            // 
            this.picConn.BackColor = System.Drawing.Color.Transparent;
            this.picConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picConn.Dock = System.Windows.Forms.DockStyle.Left;
            this.picConn.Image = global::RFIDClient.Properties.Resources.disconnect;
            this.picConn.InitialImage = null;
            this.picConn.Location = new System.Drawing.Point(0, 0);
            this.picConn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picConn.Name = "picConn";
            this.picConn.Size = new System.Drawing.Size(47, 47);
            this.picConn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picConn.TabIndex = 22;
            this.picConn.TabStop = false;
            this.picConn.Click += new System.EventHandler(this.picConn_Click);
            // 
            // picStatus
            // 
            this.picStatus.BackColor = System.Drawing.Color.Transparent;
            this.picStatus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.picStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picStatus.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picStatus.ErrorImage")));
            this.picStatus.Image = global::RFIDClient.Properties.Resources.Success;
            this.picStatus.InitialImage = null;
            this.picStatus.Location = new System.Drawing.Point(0, 0);
            this.picStatus.Margin = new System.Windows.Forms.Padding(0);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(133, 135);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStatus.TabIndex = 23;
            this.picStatus.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCurrCode);
            this.groupBox2.Controls.Add(this.lblOrigCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblCurrent);
            this.groupBox2.Controls.Add(this.lblOriginal);
            this.groupBox2.Controls.Add(this.lblTag);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(858, 135);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tag Field";
            // 
            // lblCurrCode
            // 
            this.lblCurrCode.AutoSize = true;
            this.lblCurrCode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblCurrCode.Location = new System.Drawing.Point(219, 73);
            this.lblCurrCode.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrCode.Name = "lblCurrCode";
            this.lblCurrCode.Size = new System.Drawing.Size(279, 22);
            this.lblCurrCode.TabIndex = 8;
            this.lblCurrCode.Text = "0090000000000002D9A70000";
            // 
            // lblOrigCode
            // 
            this.lblOrigCode.AutoSize = true;
            this.lblOrigCode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigCode.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblOrigCode.Location = new System.Drawing.Point(219, 35);
            this.lblOrigCode.Margin = new System.Windows.Forms.Padding(0);
            this.lblOrigCode.Name = "lblOrigCode";
            this.lblOrigCode.Size = new System.Drawing.Size(274, 22);
            this.lblOrigCode.TabIndex = 7;
            this.lblOrigCode.Text = "000000000000000000000000";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(223, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "..............................................................................";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(223, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "................................................................................." +
    "..........";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.Location = new System.Drawing.Point(128, 75);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(88, 22);
            this.lblCurrent.TabIndex = 2;
            this.lblCurrent.Text = "Current:";
            // 
            // lblOriginal
            // 
            this.lblOriginal.AutoSize = true;
            this.lblOriginal.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOriginal.Location = new System.Drawing.Point(128, 36);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(90, 22);
            this.lblOriginal.TabIndex = 1;
            this.lblOriginal.Text = "Original:";
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.Location = new System.Drawing.Point(12, 35);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(97, 70);
            this.lblTag.TabIndex = 0;
            this.lblTag.Text = "EPC Code";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToOrderColumns = true;
            this.dgvHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.dgvHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_barno,
            this.col_article,
            this.col_spec_ver,
            this.col_size,
            this.col_left_right,
            this.col_rfid_code,
            this.col_dem_mk});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistory.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvHistory.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvHistory.Location = new System.Drawing.Point(3, 18);
            this.dgvHistory.Margin = new System.Windows.Forms.Padding(0);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvHistory.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvHistory.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvHistory.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            this.dgvHistory.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHistory.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHistory.Size = new System.Drawing.Size(852, 209);
            this.dgvHistory.TabIndex = 1;
            // 
            // lstStatus
            // 
            this.lstStatus.BackColor = System.Drawing.Color.White;
            this.lstStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstStatus.ContextMenuStrip = this.menuStatus;
            this.lstStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.ItemHeight = 18;
            this.lstStatus.Location = new System.Drawing.Point(3, 18);
            this.lstStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstStatus.Size = new System.Drawing.Size(985, 61);
            this.lstStatus.TabIndex = 2;
            this.lstStatus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstStatus_MouseMove);
            // 
            // menuStatus
            // 
            this.menuStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearStatusToolStripMenuItem});
            this.menuStatus.Name = "contextMenuStrip1";
            this.menuStatus.Size = new System.Drawing.Size(143, 26);
            // 
            // clearStatusToolStripMenuItem
            // 
            this.clearStatusToolStripMenuItem.Image = global::RFIDClient.Properties.Resources.clear;
            this.clearStatusToolStripMenuItem.Name = "clearStatusToolStripMenuItem";
            this.clearStatusToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.clearStatusToolStripMenuItem.Text = "Clear Status";
            this.clearStatusToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 315);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.panelHistoryR);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(991, 313);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            this.groupBox3.Controls.Add(this.dgvHistory);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(858, 230);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "History";
            // 
            // panelHistoryR
            // 
            this.panelHistoryR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            this.panelHistoryR.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelHistoryR.Location = new System.Drawing.Point(858, 0);
            this.panelHistoryR.Margin = new System.Windows.Forms.Padding(0);
            this.panelHistoryR.Name = "panelHistoryR";
            this.panelHistoryR.Size = new System.Drawing.Size(133, 230);
            this.panelHistoryR.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.lstStatus);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(991, 82);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Realtime Status";
            // 
            // panelTag
            // 
            this.panelTag.AutoScroll = true;
            this.panelTag.BackColor = System.Drawing.Color.Azure;
            this.panelTag.Controls.Add(this.groupBox2);
            this.panelTag.Controls.Add(this.panelTagR);
            this.panelTag.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTag.Location = new System.Drawing.Point(0, 180);
            this.panelTag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(991, 135);
            this.panelTag.TabIndex = 5;
            // 
            // panelTagR
            // 
            this.panelTagR.Controls.Add(this.picStatus);
            this.panelTagR.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTagR.Location = new System.Drawing.Point(858, 0);
            this.panelTagR.Margin = new System.Windows.Forms.Padding(0);
            this.panelTagR.Name = "panelTagR";
            this.panelTagR.Size = new System.Drawing.Size(133, 135);
            this.panelTagR.TabIndex = 0;
            // 
            // panelCount
            // 
            this.panelCount.Controls.Add(this.groupBox5);
            this.panelCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCount.Location = new System.Drawing.Point(858, 0);
            this.panelCount.Margin = new System.Windows.Forms.Padding(0);
            this.panelCount.Name = "panelCount";
            this.panelCount.Size = new System.Drawing.Size(133, 133);
            this.panelCount.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblOkNum);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(133, 133);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Count";
            // 
            // lblOkNum
            // 
            this.lblOkNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOkNum.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOkNum.ForeColor = System.Drawing.Color.Black;
            this.lblOkNum.Location = new System.Drawing.Point(3, 18);
            this.lblOkNum.Margin = new System.Windows.Forms.Padding(0);
            this.lblOkNum.Name = "lblOkNum";
            this.lblOkNum.Size = new System.Drawing.Size(127, 112);
            this.lblOkNum.TabIndex = 0;
            this.lblOkNum.Text = "0";
            this.lblOkNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDemagnetize);
            this.groupBox1.Controls.Add(this.btnMagnetize);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(858, 133);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scan Field";
            // 
            // btnDemagnetize
            // 
            this.btnDemagnetize.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDemagnetize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDemagnetize.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDemagnetize.Location = new System.Drawing.Point(542, 32);
            this.btnDemagnetize.Name = "btnDemagnetize";
            this.btnDemagnetize.Size = new System.Drawing.Size(236, 76);
            this.btnDemagnetize.TabIndex = 1;
            this.btnDemagnetize.Text = "Demagnetize";
            this.btnDemagnetize.UseVisualStyleBackColor = true;
            this.btnDemagnetize.Click += new System.EventHandler(this.btnDemagnetize_Click);
            // 
            // btnMagnetize
            // 
            this.btnMagnetize.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMagnetize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMagnetize.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMagnetize.Location = new System.Drawing.Point(77, 32);
            this.btnMagnetize.Name = "btnMagnetize";
            this.btnMagnetize.Size = new System.Drawing.Size(244, 76);
            this.btnMagnetize.TabIndex = 0;
            this.btnMagnetize.Text = "Magnetize";
            this.btnMagnetize.UseVisualStyleBackColor = true;
            this.btnMagnetize.Click += new System.EventHandler(this.btnMagnetize_Click);
            // 
            // panelScan
            // 
            this.panelScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.panelScan.Controls.Add(this.groupBox1);
            this.panelScan.Controls.Add(this.panelCount);
            this.panelScan.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelScan.Location = new System.Drawing.Point(0, 47);
            this.panelScan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelScan.Name = "panelScan";
            this.panelScan.Size = new System.Drawing.Size(991, 133);
            this.panelScan.TabIndex = 4;
            // 
            // col_barno
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_barno.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_barno.HeaderText = "Barcode No.";
            this.col_barno.Name = "col_barno";
            this.col_barno.ReadOnly = true;
            this.col_barno.Width = 150;
            // 
            // col_article
            // 
            this.col_article.HeaderText = "Article";
            this.col_article.Name = "col_article";
            this.col_article.ReadOnly = true;
            // 
            // col_spec_ver
            // 
            this.col_spec_ver.HeaderText = "Spec. No. Ver";
            this.col_spec_ver.Name = "col_spec_ver";
            this.col_spec_ver.ReadOnly = true;
            this.col_spec_ver.Width = 200;
            // 
            // col_size
            // 
            this.col_size.HeaderText = "Size";
            this.col_size.Name = "col_size";
            this.col_size.ReadOnly = true;
            this.col_size.Width = 65;
            // 
            // col_left_right
            // 
            this.col_left_right.HeaderText = "L / R";
            this.col_left_right.Name = "col_left_right";
            this.col_left_right.ReadOnly = true;
            this.col_left_right.Width = 50;
            // 
            // col_rfid_code
            // 
            this.col_rfid_code.HeaderText = "RFID Code";
            this.col_rfid_code.Name = "col_rfid_code";
            this.col_rfid_code.ReadOnly = true;
            this.col_rfid_code.Width = 200;
            // 
            // col_dem_mk
            // 
            this.col_dem_mk.HeaderText = "Dem Mk";
            this.col_dem_mk.Name = "col_dem_mk";
            this.col_dem_mk.ReadOnly = true;
            this.col_dem_mk.Width = 70;
            // 
            // FrmAccessControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 628);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelTag);
            this.Controls.Add(this.panelScan);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmAccessControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Access Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panelTop.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picComPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.menuStatus.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panelTag.ResumeLayout(false);
            this.panelTagR.ResumeLayout(false);
            this.panelCount.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panelScan.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.PictureBox picComPort;
        private System.Windows.Forms.PictureBox picConn;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrCode;
        private System.Windows.Forms.Label lblOrigCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ContextMenuStrip menuStatus;
        private System.Windows.Forms.ToolStripMenuItem clearStatusToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelHistoryR;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Panel panelTagR;
        private System.Windows.Forms.Panel panelCount;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblOkNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelScan;
        private System.Windows.Forms.Button btnDemagnetize;
        private System.Windows.Forms.Button btnMagnetize;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_barno;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_article;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_spec_ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_size;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_left_right;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_rfid_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dem_mk;

    }
}

