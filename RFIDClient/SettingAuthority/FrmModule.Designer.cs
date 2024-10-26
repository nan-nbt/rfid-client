namespace RFIDClient
{
    partial class FrmModule
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSort = new System.Windows.Forms.Button();
            this.lblModuleNum = new System.Windows.Forms.Label();
            this.txtModule = new System.Windows.Forms.TextBox();
            this.lblModule = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvModule = new System.Windows.Forms.DataGridView();
            this.gbxModule = new System.Windows.Forms.GroupBox();
            this.txtSortSeq = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkEnableMk = new System.Windows.Forms.CheckBox();
            this.txtModuleDict = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtModuleNo = new System.Windows.Forms.TextBox();
            this.ColModuleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModuleDict = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEnableMk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSortSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTempId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).BeginInit();
            this.gbxModule.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Controls.Add(this.lblModuleNum);
            this.panel1.Controls.Add(this.txtModule);
            this.panel1.Controls.Add(this.lblModule);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(952, 37);
            this.panel1.TabIndex = 0;
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSort.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSort.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSort.Location = new System.Drawing.Point(713, 5);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(97, 27);
            this.btnSort.TabIndex = 19;
            this.btnSort.Text = "Save Sort";
            this.btnSort.UseVisualStyleBackColor = false;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // lblModuleNum
            // 
            this.lblModuleNum.AutoSize = true;
            this.lblModuleNum.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuleNum.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblModuleNum.Location = new System.Drawing.Point(257, 10);
            this.lblModuleNum.Name = "lblModuleNum";
            this.lblModuleNum.Size = new System.Drawing.Size(18, 19);
            this.lblModuleNum.TabIndex = 15;
            this.lblModuleNum.Text = "0";
            // 
            // txtModule
            // 
            this.txtModule.BackColor = System.Drawing.Color.White;
            this.txtModule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModule.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModule.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtModule.Location = new System.Drawing.Point(79, 6);
            this.txtModule.Margin = new System.Windows.Forms.Padding(0);
            this.txtModule.Name = "txtModule";
            this.txtModule.Size = new System.Drawing.Size(175, 26);
            this.txtModule.TabIndex = 13;
            this.txtModule.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtModule_KeyUp);
            // 
            // lblModule
            // 
            this.lblModule.AutoSize = true;
            this.lblModule.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModule.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblModule.Location = new System.Drawing.Point(3, 9);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(73, 18);
            this.lblModule.TabIndex = 14;
            this.lblModule.Text = "Keyword:";
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.ForeColor = System.Drawing.Color.Red;
            this.btnDel.Location = new System.Drawing.Point(434, 25);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(69, 27);
            this.btnDel.TabIndex = 18;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnAdd.Location = new System.Drawing.Point(132, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 27);
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.Location = new System.Drawing.Point(203, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 27);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvModule);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbxModule);
            this.splitContainer1.Size = new System.Drawing.Size(954, 646);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvModule
            // 
            this.dgvModule.AllowUserToAddRows = false;
            this.dgvModule.AllowUserToDeleteRows = false;
            this.dgvModule.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvModule.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvModule.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvModule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvModule.ColumnHeadersHeight = 24;
            this.dgvModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColModuleNo,
            this.ColModuleName,
            this.ColModuleDict,
            this.ColEnableMk,
            this.ColSortSeq,
            this.ColTempId});
            this.dgvModule.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvModule.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvModule.EnableHeadersVisualStyles = false;
            this.dgvModule.GridColor = System.Drawing.SystemColors.Highlight;
            this.dgvModule.Location = new System.Drawing.Point(0, 37);
            this.dgvModule.Margin = new System.Windows.Forms.Padding(0);
            this.dgvModule.MultiSelect = false;
            this.dgvModule.Name = "dgvModule";
            this.dgvModule.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModule.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvModule.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvModule.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvModule.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvModule.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvModule.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.dgvModule.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.dgvModule.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvModule.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModule.Size = new System.Drawing.Size(952, 304);
            this.dgvModule.TabIndex = 7;
            this.dgvModule.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModule_CellClick);
            this.dgvModule.SelectionChanged += new System.EventHandler(this.dgvModule_SelectionChanged);
            // 
            // gbxModule
            // 
            this.gbxModule.Controls.Add(this.txtSortSeq);
            this.gbxModule.Controls.Add(this.btnDel);
            this.gbxModule.Controls.Add(this.label5);
            this.gbxModule.Controls.Add(this.btnAdd);
            this.gbxModule.Controls.Add(this.btnSave);
            this.gbxModule.Controls.Add(this.chkEnableMk);
            this.gbxModule.Controls.Add(this.txtModuleDict);
            this.gbxModule.Controls.Add(this.label2);
            this.gbxModule.Controls.Add(this.label4);
            this.gbxModule.Controls.Add(this.txtModuleName);
            this.gbxModule.Controls.Add(this.label1);
            this.gbxModule.Controls.Add(this.label3);
            this.gbxModule.Controls.Add(this.txtModuleNo);
            this.gbxModule.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxModule.ForeColor = System.Drawing.Color.Gray;
            this.gbxModule.Location = new System.Drawing.Point(202, 8);
            this.gbxModule.Name = "gbxModule";
            this.gbxModule.Size = new System.Drawing.Size(524, 277);
            this.gbxModule.TabIndex = 23;
            this.gbxModule.TabStop = false;
            this.gbxModule.Text = "Module Detail";
            // 
            // txtSortSeq
            // 
            this.txtSortSeq.BackColor = System.Drawing.Color.White;
            this.txtSortSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSortSeq.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSortSeq.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtSortSeq.Location = new System.Drawing.Point(131, 236);
            this.txtSortSeq.Margin = new System.Windows.Forms.Padding(0);
            this.txtSortSeq.Name = "txtSortSeq";
            this.txtSortSeq.Size = new System.Drawing.Size(372, 26);
            this.txtSortSeq.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(19, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 18);
            this.label5.TabIndex = 25;
            this.label5.Text = "Sort Seq.:";
            // 
            // chkEnableMk
            // 
            this.chkEnableMk.AutoSize = true;
            this.chkEnableMk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEnableMk.ForeColor = System.Drawing.Color.DarkBlue;
            this.chkEnableMk.Location = new System.Drawing.Point(131, 205);
            this.chkEnableMk.Margin = new System.Windows.Forms.Padding(0);
            this.chkEnableMk.Name = "chkEnableMk";
            this.chkEnableMk.Size = new System.Drawing.Size(12, 11);
            this.chkEnableMk.TabIndex = 23;
            this.chkEnableMk.UseVisualStyleBackColor = true;
            // 
            // txtModuleDict
            // 
            this.txtModuleDict.BackColor = System.Drawing.Color.White;
            this.txtModuleDict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModuleDict.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModuleDict.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtModuleDict.Location = new System.Drawing.Point(131, 157);
            this.txtModuleDict.Margin = new System.Windows.Forms.Padding(0);
            this.txtModuleDict.Name = "txtModuleDict";
            this.txtModuleDict.Size = new System.Drawing.Size(372, 26);
            this.txtModuleDict.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(19, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Module Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(19, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "Enable Mark:";
            // 
            // txtModuleName
            // 
            this.txtModuleName.BackColor = System.Drawing.Color.White;
            this.txtModuleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModuleName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModuleName.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtModuleName.Location = new System.Drawing.Point(131, 112);
            this.txtModuleName.Margin = new System.Windows.Forms.Padding(0);
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(372, 26);
            this.txtModuleName.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(19, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "Module No.:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(19, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "Module Dict:";
            // 
            // txtModuleNo
            // 
            this.txtModuleNo.BackColor = System.Drawing.Color.White;
            this.txtModuleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModuleNo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModuleNo.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtModuleNo.Location = new System.Drawing.Point(131, 67);
            this.txtModuleNo.Margin = new System.Windows.Forms.Padding(0);
            this.txtModuleNo.Name = "txtModuleNo";
            this.txtModuleNo.Size = new System.Drawing.Size(372, 26);
            this.txtModuleNo.TabIndex = 15;
            // 
            // ColModuleNo
            // 
            this.ColModuleNo.DataPropertyName = "ModuleNo";
            this.ColModuleNo.HeaderText = "Module No.";
            this.ColModuleNo.Name = "ColModuleNo";
            this.ColModuleNo.ReadOnly = true;
            this.ColModuleNo.Width = 280;
            // 
            // ColModuleName
            // 
            this.ColModuleName.DataPropertyName = "ModuleName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColModuleName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColModuleName.HeaderText = "Module Name";
            this.ColModuleName.Name = "ColModuleName";
            this.ColModuleName.ReadOnly = true;
            this.ColModuleName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColModuleName.Width = 300;
            // 
            // ColModuleDict
            // 
            this.ColModuleDict.DataPropertyName = "ModuleDict";
            this.ColModuleDict.HeaderText = "Module Dict";
            this.ColModuleDict.Name = "ColModuleDict";
            this.ColModuleDict.ReadOnly = true;
            this.ColModuleDict.Width = 120;
            // 
            // ColEnableMk
            // 
            this.ColEnableMk.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColEnableMk.DataPropertyName = "EnableMk";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColEnableMk.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColEnableMk.HeaderText = "Enable Mark";
            this.ColEnableMk.Name = "ColEnableMk";
            this.ColEnableMk.ReadOnly = true;
            this.ColEnableMk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColEnableMk.Width = 120;
            // 
            // ColSortSeq
            // 
            this.ColSortSeq.DataPropertyName = "SortSeq";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.ColSortSeq.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColSortSeq.HeaderText = "Sort Seq";
            this.ColSortSeq.Name = "ColSortSeq";
            this.ColSortSeq.Width = 110;
            // 
            // ColTempId
            // 
            this.ColTempId.DataPropertyName = "TempId";
            this.ColTempId.HeaderText = "Temp ID";
            this.ColTempId.Name = "ColTempId";
            this.ColTempId.Visible = false;
            // 
            // FrmModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(954, 646);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Maintain Module";
            this.Load += new System.EventHandler(this.FrmModule_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).EndInit();
            this.gbxModule.ResumeLayout(false);
            this.gbxModule.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblModuleNum;
        private System.Windows.Forms.TextBox txtModule;
        private System.Windows.Forms.Label lblModule;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvModule;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModuleDict;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModuleNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxModule;
        private System.Windows.Forms.CheckBox chkEnableMk;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.TextBox txtSortSeq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModuleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModuleDict;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEnableMk;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSortSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTempId;

    }
}