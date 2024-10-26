namespace RFIDClient
{
    partial class FrmTagQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTagQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.picComPort = new System.Windows.Forms.PictureBox();
            this.picConn = new System.Windows.Forms.PictureBox();
            this.panelScan = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblScan = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.dgvBarcode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelCount = new System.Windows.Forms.Panel();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblSystemCode = new System.Windows.Forms.Label();
            this.lblShoeCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSystem = new System.Windows.Forms.Label();
            this.lblShoe = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelTag = new System.Windows.Forms.Panel();
            this.panelTagR = new System.Windows.Forms.Panel();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.col_barno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_rfid_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stock_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_inout_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_modify_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_modify_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menuStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConn)).BeginInit();
            this.panelScan.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelTag.SuspendLayout();
            this.panelTagR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.menuStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Honeydew;
            this.panelTop.Controls.Add(this.panel4);
            this.panelTop.Controls.Add(this.picConn);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(985, 47);
            this.panelTop.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbPortName);
            this.panel4.Controls.Add(this.picComPort);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(852, 0);
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
            // panelScan
            // 
            this.panelScan.BackColor = System.Drawing.Color.AliceBlue;
            this.panelScan.Controls.Add(this.groupBox1);
            this.panelScan.Controls.Add(this.panelCount);
            this.panelScan.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelScan.Location = new System.Drawing.Point(0, 47);
            this.panelScan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelScan.Name = "panelScan";
            this.panelScan.Size = new System.Drawing.Size(985, 120);
            this.panelScan.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Silver;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(885, 120);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scan Field";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 18);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblScan);
            this.splitContainer2.Panel1.Controls.Add(this.txtBarcode);
            this.splitContainer2.Panel1MinSize = 30;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvBarcode);
            this.splitContainer2.Size = new System.Drawing.Size(879, 99);
            this.splitContainer2.SplitterDistance = 35;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // lblScan
            // 
            this.lblScan.AutoSize = true;
            this.lblScan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScan.ForeColor = System.Drawing.Color.Black;
            this.lblScan.Location = new System.Drawing.Point(4, 10);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(123, 19);
            this.lblScan.TabIndex = 8;
            this.lblScan.Text = "Scan Barcode:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.White;
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtBarcode.Location = new System.Drawing.Point(130, 3);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(0);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(167, 29);
            this.txtBarcode.TabIndex = 7;
            this.txtBarcode.Text = "PRSCG3087004R";
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcode.Click += new System.EventHandler(this.txtBarcode_Click);
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // dgvBarcode
            // 
            this.dgvBarcode.AllowUserToDeleteRows = false;
            this.dgvBarcode.AllowUserToResizeRows = false;
            dataGridViewCellStyle49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvBarcode.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle49;
            this.dgvBarcode.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgvBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBarcode.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle50.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle50.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBarcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle50;
            this.dgvBarcode.ColumnHeadersHeight = 25;
            this.dgvBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBarcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle52.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle52.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle52.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle52.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBarcode.DefaultCellStyle = dataGridViewCellStyle52;
            this.dgvBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBarcode.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBarcode.EnableHeadersVisualStyles = false;
            this.dgvBarcode.GridColor = System.Drawing.SystemColors.Highlight;
            this.dgvBarcode.Location = new System.Drawing.Point(0, 0);
            this.dgvBarcode.Margin = new System.Windows.Forms.Padding(0);
            this.dgvBarcode.Name = "dgvBarcode";
            this.dgvBarcode.ReadOnly = true;
            this.dgvBarcode.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle53.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle53.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle53.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle53.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle53.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBarcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle53;
            this.dgvBarcode.RowHeadersVisible = false;
            dataGridViewCellStyle54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle54.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle54.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle54.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle54.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvBarcode.RowsDefaultCellStyle = dataGridViewCellStyle54;
            this.dgvBarcode.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvBarcode.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBarcode.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvBarcode.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvBarcode.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvBarcode.RowTemplate.Height = 24;
            this.dgvBarcode.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvBarcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBarcode.Size = new System.Drawing.Size(879, 63);
            this.dgvBarcode.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle51.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle51;
            this.dataGridViewTextBoxColumn1.HeaderText = "Barcode No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Article";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Spec. No. -Ver";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Size";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 55;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "L / R";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 45;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "RFID Code";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 260;
            // 
            // panelCount
            // 
            this.panelCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCount.Location = new System.Drawing.Point(885, 0);
            this.panelCount.Margin = new System.Windows.Forms.Padding(0);
            this.panelCount.Name = "panelCount";
            this.panelCount.Size = new System.Drawing.Size(100, 120);
            this.panelCount.TabIndex = 1;
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
            this.picStatus.Size = new System.Drawing.Size(100, 100);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStatus.TabIndex = 23;
            this.picStatus.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSystemCode);
            this.groupBox2.Controls.Add(this.lblShoeCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblSystem);
            this.groupBox2.Controls.Add(this.lblShoe);
            this.groupBox2.Controls.Add(this.lblTag);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Silver;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(885, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tag Field";
            // 
            // lblSystemCode
            // 
            this.lblSystemCode.AutoSize = true;
            this.lblSystemCode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblSystemCode.Location = new System.Drawing.Point(219, 60);
            this.lblSystemCode.Margin = new System.Windows.Forms.Padding(0);
            this.lblSystemCode.Name = "lblSystemCode";
            this.lblSystemCode.Size = new System.Drawing.Size(279, 22);
            this.lblSystemCode.TabIndex = 8;
            this.lblSystemCode.Text = "0090000000000002D9A70000";
            // 
            // lblShoeCode
            // 
            this.lblShoeCode.AutoSize = true;
            this.lblShoeCode.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShoeCode.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblShoeCode.Location = new System.Drawing.Point(219, 22);
            this.lblShoeCode.Margin = new System.Windows.Forms.Padding(0);
            this.lblShoeCode.Name = "lblShoeCode";
            this.lblShoeCode.Size = new System.Drawing.Size(274, 22);
            this.lblShoeCode.TabIndex = 7;
            this.lblShoeCode.Text = "000000000000000000000000";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(223, 74);
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
            this.label1.Location = new System.Drawing.Point(223, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "................................................................................." +
    "..........";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSystem
            // 
            this.lblSystem.AutoSize = true;
            this.lblSystem.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystem.ForeColor = System.Drawing.Color.Black;
            this.lblSystem.Location = new System.Drawing.Point(128, 62);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(86, 22);
            this.lblSystem.TabIndex = 2;
            this.lblSystem.Text = "System:";
            // 
            // lblShoe
            // 
            this.lblShoe.AutoSize = true;
            this.lblShoe.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShoe.ForeColor = System.Drawing.Color.Black;
            this.lblShoe.Location = new System.Drawing.Point(128, 23);
            this.lblShoe.Name = "lblShoe";
            this.lblShoe.Size = new System.Drawing.Size(65, 22);
            this.lblShoe.TabIndex = 1;
            this.lblShoe.Text = "Shoe:";
            // 
            // lblTag
            // 
            this.lblTag.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTag.ForeColor = System.Drawing.Color.Black;
            this.lblTag.Location = new System.Drawing.Point(12, 22);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(97, 70);
            this.lblTag.TabIndex = 0;
            this.lblTag.Text = "EPC Code";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstStatus
            // 
            this.lstStatus.BackColor = System.Drawing.SystemColors.Info;
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
            this.lstStatus.Size = new System.Drawing.Size(979, 66);
            this.lstStatus.TabIndex = 2;
            this.lstStatus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstStatus_MouseMove);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.lstStatus);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Silver;
            this.groupBox4.Location = new System.Drawing.Point(0, 535);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(985, 87);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Realtime Status";
            // 
            // panelTag
            // 
            this.panelTag.AutoScroll = true;
            this.panelTag.BackColor = System.Drawing.Color.Lavender;
            this.panelTag.Controls.Add(this.groupBox2);
            this.panelTag.Controls.Add(this.panelTagR);
            this.panelTag.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTag.Location = new System.Drawing.Point(0, 167);
            this.panelTag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(985, 100);
            this.panelTag.TabIndex = 5;
            // 
            // panelTagR
            // 
            this.panelTagR.Controls.Add(this.picStatus);
            this.panelTagR.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTagR.Location = new System.Drawing.Point(885, 0);
            this.panelTagR.Margin = new System.Windows.Forms.Padding(0);
            this.panelTagR.Name = "panelTagR";
            this.panelTagR.Size = new System.Drawing.Size(100, 100);
            this.panelTagR.TabIndex = 0;
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToOrderColumns = true;
            this.dgvHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.dgvHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle55;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle56.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle56.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle56.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle56.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle56.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle56;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_barno,
            this.col_rfid_code,
            this.col_stock_name,
            this.col_inout_name,
            this.col_modify_user,
            this.col_modify_date});
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle58.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle58.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle58.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle58.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle58.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistory.DefaultCellStyle = dataGridViewCellStyle58;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvHistory.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvHistory.Location = new System.Drawing.Point(3, 18);
            this.dgvHistory.Margin = new System.Windows.Forms.Padding(0);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle59.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle59.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle59.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle59;
            this.dgvHistory.RowHeadersVisible = false;
            dataGridViewCellStyle60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle60.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle60.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvHistory.RowsDefaultCellStyle = dataGridViewCellStyle60;
            this.dgvHistory.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            this.dgvHistory.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHistory.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHistory.Size = new System.Drawing.Size(979, 247);
            this.dgvHistory.TabIndex = 6;
            // 
            // col_barno
            // 
            this.col_barno.DataPropertyName = "BarNo";
            dataGridViewCellStyle57.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col_barno.DefaultCellStyle = dataGridViewCellStyle57;
            this.col_barno.HeaderText = "Barcode No.";
            this.col_barno.Name = "col_barno";
            this.col_barno.ReadOnly = true;
            this.col_barno.Width = 150;
            // 
            // col_rfid_code
            // 
            this.col_rfid_code.DataPropertyName = "RfidCode";
            this.col_rfid_code.HeaderText = "RFID Code";
            this.col_rfid_code.Name = "col_rfid_code";
            this.col_rfid_code.ReadOnly = true;
            this.col_rfid_code.Width = 240;
            // 
            // col_stock_name
            // 
            this.col_stock_name.DataPropertyName = "StockName";
            this.col_stock_name.FillWeight = 150F;
            this.col_stock_name.HeaderText = "Stock";
            this.col_stock_name.Name = "col_stock_name";
            this.col_stock_name.ReadOnly = true;
            this.col_stock_name.Width = 150;
            // 
            // col_inout_name
            // 
            this.col_inout_name.DataPropertyName = "InoutName";
            this.col_inout_name.FillWeight = 150F;
            this.col_inout_name.HeaderText = "IN / OUT";
            this.col_inout_name.Name = "col_inout_name";
            this.col_inout_name.ReadOnly = true;
            this.col_inout_name.Width = 150;
            // 
            // col_modify_user
            // 
            this.col_modify_user.DataPropertyName = "ModifyUser";
            this.col_modify_user.HeaderText = "Modify User";
            this.col_modify_user.Name = "col_modify_user";
            this.col_modify_user.ReadOnly = true;
            this.col_modify_user.Width = 110;
            // 
            // col_modify_date
            // 
            this.col_modify_date.DataPropertyName = "ModifyDate";
            this.col_modify_date.HeaderText = "Modify Date";
            this.col_modify_date.Name = "col_modify_date";
            this.col_modify_date.ReadOnly = true;
            this.col_modify_date.Width = 160;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Azure;
            this.groupBox3.Controls.Add(this.dgvHistory);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Silver;
            this.groupBox3.Location = new System.Drawing.Point(0, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(985, 268);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "History";
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
            this.clearStatusToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearStatusToolStripMenuItem.Text = "Clear Status";
            this.clearStatusToolStripMenuItem.Click += new System.EventHandler(this.clearStatusToolStripMenuItem_Click);
            // 
            // FrmTagQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 622);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panelTag);
            this.Controls.Add(this.panelScan);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTagQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scan Barcode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panelTop.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picComPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConn)).EndInit();
            this.panelScan.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panelTag.ResumeLayout(false);
            this.panelTagR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.menuStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.PictureBox picComPort;
        private System.Windows.Forms.PictureBox picConn;
        private System.Windows.Forms.Panel panelScan;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblSystem;
        private System.Windows.Forms.Label lblShoe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblSystemCode;
        private System.Windows.Forms.Label lblShoeCode;
        private System.Windows.Forms.DataGridView dgvBarcode;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lblScan;
        private System.Windows.Forms.Panel panelCount;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Panel panelTagR;
        protected internal System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_barno;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_rfid_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stock_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_inout_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_modify_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_modify_date;
        private System.Windows.Forms.ContextMenuStrip menuStatus;
        private System.Windows.Forms.ToolStripMenuItem clearStatusToolStripMenuItem;

    }
}

