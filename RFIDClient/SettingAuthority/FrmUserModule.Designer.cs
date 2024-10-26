namespace RFIDClient
{
    partial class FrmUserModule
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserModule));
            this.splitContainerBase = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.gbxUser = new System.Windows.Forms.GroupBox();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.ColUsrId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUsrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblUserNum = new System.Windows.Forms.Label();
            this.btnUser = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.panelUser = new System.Windows.Forms.Panel();
            this.gbxDept = new System.Windows.Forms.GroupBox();
            this.dgvDept = new System.Windows.Forms.DataGridView();
            this.ColDeptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDeptNum = new System.Windows.Forms.Label();
            this.btnDept = new System.Windows.Forms.Button();
            this.lblDept = new System.Windows.Forms.Label();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxAuth = new System.Windows.Forms.GroupBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.dgvModule = new System.Windows.Forms.DataGridView();
            this.ColPermit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModuleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTempId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModuleDict = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEnableMk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).BeginInit();
            this.splitContainerBase.Panel1.SuspendLayout();
            this.splitContainerBase.Panel2.SuspendLayout();
            this.splitContainerBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            this.gbxUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.gbxDept.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDept)).BeginInit();
            this.gbxAuth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerBase
            // 
            this.splitContainerBase.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBase.Location = new System.Drawing.Point(0, 0);
            this.splitContainerBase.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerBase.Name = "splitContainerBase";
            // 
            // splitContainerBase.Panel1
            // 
            this.splitContainerBase.Panel1.Controls.Add(this.splitContainerLeft);
            // 
            // splitContainerBase.Panel2
            // 
            this.splitContainerBase.Panel2.Controls.Add(this.gbxAuth);
            this.splitContainerBase.Size = new System.Drawing.Size(781, 553);
            this.splitContainerBase.SplitterDistance = 388;
            this.splitContainerBase.TabIndex = 1;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.gbxUser);
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.gbxDept);
            this.splitContainerLeft.Size = new System.Drawing.Size(388, 553);
            this.splitContainerLeft.SplitterDistance = 286;
            this.splitContainerLeft.SplitterWidth = 2;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // gbxUser
            // 
            this.gbxUser.Controls.Add(this.dgvUser);
            this.gbxUser.Controls.Add(this.lblUserNum);
            this.gbxUser.Controls.Add(this.btnUser);
            this.gbxUser.Controls.Add(this.txtUser);
            this.gbxUser.Controls.Add(this.lblUser);
            this.gbxUser.Controls.Add(this.panelUser);
            this.gbxUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxUser.ForeColor = System.Drawing.Color.Silver;
            this.gbxUser.Location = new System.Drawing.Point(0, 0);
            this.gbxUser.Name = "gbxUser";
            this.gbxUser.Size = new System.Drawing.Size(388, 286);
            this.gbxUser.TabIndex = 0;
            this.gbxUser.TabStop = false;
            this.gbxUser.Text = "User";
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUser.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUser.ColumnHeadersHeight = 24;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColUsrId,
            this.ColUsrName});
            this.dgvUser.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUser.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.GridColor = System.Drawing.SystemColors.Highlight;
            this.dgvUser.Location = new System.Drawing.Point(3, 62);
            this.dgvUser.Margin = new System.Windows.Forms.Padding(0);
            this.dgvUser.MultiSelect = false;
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUser.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvUser.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvUser.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvUser.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvUser.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.dgvUser.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.dgvUser.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvUser.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(382, 221);
            this.dgvUser.TabIndex = 3;
            this.dgvUser.Tag = "AAA";
            this.dgvUser.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellClick);
            this.dgvUser.SelectionChanged += new System.EventHandler(this.dgvUser_SelectionChanged);
            // 
            // ColUsrId
            // 
            this.ColUsrId.DataPropertyName = "UsrId";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColUsrId.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColUsrId.HeaderText = "User ID";
            this.ColUsrId.Name = "ColUsrId";
            this.ColUsrId.ReadOnly = true;
            this.ColUsrId.Width = 180;
            // 
            // ColUsrName
            // 
            this.ColUsrName.DataPropertyName = "UsrName";
            this.ColUsrName.HeaderText = "User Name";
            this.ColUsrName.Name = "ColUsrName";
            this.ColUsrName.ReadOnly = true;
            this.ColUsrName.Width = 180;
            // 
            // lblUserNum
            // 
            this.lblUserNum.AutoSize = true;
            this.lblUserNum.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNum.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUserNum.Location = new System.Drawing.Point(262, 34);
            this.lblUserNum.Name = "lblUserNum";
            this.lblUserNum.Size = new System.Drawing.Size(18, 19);
            this.lblUserNum.TabIndex = 12;
            this.lblUserNum.Text = "0";
            // 
            // btnUser
            // 
            this.btnUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUser.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnUser.Location = new System.Drawing.Point(302, 29);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(62, 27);
            this.btnUser.TabIndex = 11;
            this.btnUser.Text = "Save";
            this.btnUser.UseVisualStyleBackColor = false;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtUser.Location = new System.Drawing.Point(84, 30);
            this.txtUser.Margin = new System.Windows.Forms.Padding(0);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(175, 26);
            this.txtUser.TabIndex = 9;
            this.txtUser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUser_KeyUp);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUser.Location = new System.Drawing.Point(8, 33);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(73, 18);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "Keyword:";
            // 
            // panelUser
            // 
            this.panelUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUser.Location = new System.Drawing.Point(3, 22);
            this.panelUser.Margin = new System.Windows.Forms.Padding(0);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(382, 40);
            this.panelUser.TabIndex = 13;
            // 
            // gbxDept
            // 
            this.gbxDept.Controls.Add(this.dgvDept);
            this.gbxDept.Controls.Add(this.lblDeptNum);
            this.gbxDept.Controls.Add(this.btnDept);
            this.gbxDept.Controls.Add(this.lblDept);
            this.gbxDept.Controls.Add(this.txtDept);
            this.gbxDept.Controls.Add(this.panel1);
            this.gbxDept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDept.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDept.ForeColor = System.Drawing.Color.Silver;
            this.gbxDept.Location = new System.Drawing.Point(0, 0);
            this.gbxDept.Name = "gbxDept";
            this.gbxDept.Size = new System.Drawing.Size(388, 265);
            this.gbxDept.TabIndex = 1;
            this.gbxDept.TabStop = false;
            this.gbxDept.Text = "Group";
            // 
            // dgvDept
            // 
            this.dgvDept.AllowUserToAddRows = false;
            this.dgvDept.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvDept.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDept.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvDept.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDept.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDept.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDept.ColumnHeadersHeight = 24;
            this.dgvDept.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDept.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDeptNo,
            this.ColDeptName});
            this.dgvDept.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDept.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvDept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDept.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDept.EnableHeadersVisualStyles = false;
            this.dgvDept.GridColor = System.Drawing.SystemColors.Highlight;
            this.dgvDept.Location = new System.Drawing.Point(3, 62);
            this.dgvDept.Margin = new System.Windows.Forms.Padding(0);
            this.dgvDept.MultiSelect = false;
            this.dgvDept.Name = "dgvDept";
            this.dgvDept.ReadOnly = true;
            this.dgvDept.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDept.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvDept.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvDept.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvDept.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvDept.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDept.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.dgvDept.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.dgvDept.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvDept.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDept.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDept.Size = new System.Drawing.Size(382, 200);
            this.dgvDept.TabIndex = 4;
            this.dgvDept.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDept_CellClick);
            this.dgvDept.SelectionChanged += new System.EventHandler(this.dgvDept_SelectionChanged);
            // 
            // ColDeptNo
            // 
            this.ColDeptNo.DataPropertyName = "DeptNo";
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColDeptNo.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColDeptNo.HeaderText = "Dept. No.";
            this.ColDeptNo.Name = "ColDeptNo";
            this.ColDeptNo.ReadOnly = true;
            // 
            // ColDeptName
            // 
            this.ColDeptName.DataPropertyName = "DeptGroupName";
            this.ColDeptName.HeaderText = "Dept. Name";
            this.ColDeptName.Name = "ColDeptName";
            this.ColDeptName.ReadOnly = true;
            this.ColDeptName.Width = 260;
            // 
            // lblDeptNum
            // 
            this.lblDeptNum.AutoSize = true;
            this.lblDeptNum.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeptNum.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDeptNum.Location = new System.Drawing.Point(262, 34);
            this.lblDeptNum.Name = "lblDeptNum";
            this.lblDeptNum.Size = new System.Drawing.Size(18, 19);
            this.lblDeptNum.TabIndex = 15;
            this.lblDeptNum.Text = "0";
            // 
            // btnDept
            // 
            this.btnDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDept.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDept.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnDept.Location = new System.Drawing.Point(302, 30);
            this.btnDept.Name = "btnDept";
            this.btnDept.Size = new System.Drawing.Size(62, 27);
            this.btnDept.TabIndex = 14;
            this.btnDept.Text = "Save";
            this.btnDept.UseVisualStyleBackColor = false;
            this.btnDept.Click += new System.EventHandler(this.btnDept_Click);
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDept.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDept.Location = new System.Drawing.Point(8, 34);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(73, 18);
            this.lblDept.TabIndex = 13;
            this.lblDept.Text = "Keyword:";
            // 
            // txtDept
            // 
            this.txtDept.BackColor = System.Drawing.Color.White;
            this.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDept.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDept.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtDept.Location = new System.Drawing.Point(84, 30);
            this.txtDept.Margin = new System.Windows.Forms.Padding(0);
            this.txtDept.Name = "txtDept";
            this.txtDept.Size = new System.Drawing.Size(175, 26);
            this.txtDept.TabIndex = 12;
            this.txtDept.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDept_KeyUp);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 40);
            this.panel1.TabIndex = 16;
            // 
            // gbxAuth
            // 
            this.gbxAuth.Controls.Add(this.chkAll);
            this.gbxAuth.Controls.Add(this.dgvModule);
            this.gbxAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxAuth.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxAuth.ForeColor = System.Drawing.Color.Silver;
            this.gbxAuth.Location = new System.Drawing.Point(0, 0);
            this.gbxAuth.Name = "gbxAuth";
            this.gbxAuth.Size = new System.Drawing.Size(389, 553);
            this.gbxAuth.TabIndex = 0;
            this.gbxAuth.TabStop = false;
            this.gbxAuth.Text = "Authority";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAll.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.chkAll.Location = new System.Drawing.Point(60, 30);
            this.chkAll.Margin = new System.Windows.Forms.Padding(0);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(12, 11);
            this.chkAll.TabIndex = 6;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Click += new System.EventHandler(this.chkAll_Click);
            // 
            // dgvModule
            // 
            this.dgvModule.AllowUserToAddRows = false;
            this.dgvModule.AllowUserToDeleteRows = false;
            this.dgvModule.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvModule.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvModule.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvModule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvModule.ColumnHeadersHeight = 24;
            this.dgvModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColPermit,
            this.ColModuleName,
            this.ColModuleNo,
            this.ColTempId,
            this.ColModuleDict,
            this.ColEnableMk});
            this.dgvModule.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvModule.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvModule.EnableHeadersVisualStyles = false;
            this.dgvModule.GridColor = System.Drawing.SystemColors.Highlight;
            this.dgvModule.Location = new System.Drawing.Point(3, 22);
            this.dgvModule.Margin = new System.Windows.Forms.Padding(0);
            this.dgvModule.Name = "dgvModule";
            this.dgvModule.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModule.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvModule.RowHeadersVisible = false;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvModule.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvModule.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvModule.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvModule.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.dgvModule.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.dgvModule.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvModule.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModule.Size = new System.Drawing.Size(383, 528);
            this.dgvModule.TabIndex = 5;
            this.dgvModule.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModule_CellClick);
            this.dgvModule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModule_CellDoubleClick);
            // 
            // ColPermit
            // 
            this.ColPermit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColPermit.FalseValue = "false";
            this.ColPermit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColPermit.HeaderText = "Permit";
            this.ColPermit.Name = "ColPermit";
            this.ColPermit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColPermit.TrueValue = "true";
            this.ColPermit.Width = 80;
            // 
            // ColModuleName
            // 
            this.ColModuleName.DataPropertyName = "ModuleName";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColModuleName.DefaultCellStyle = dataGridViewCellStyle15;
            this.ColModuleName.HeaderText = "Module Name";
            this.ColModuleName.Name = "ColModuleName";
            this.ColModuleName.ReadOnly = true;
            this.ColModuleName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColModuleName.Width = 300;
            // 
            // ColModuleNo
            // 
            this.ColModuleNo.DataPropertyName = "ModuleNo";
            this.ColModuleNo.HeaderText = "Module No.";
            this.ColModuleNo.Name = "ColModuleNo";
            this.ColModuleNo.ReadOnly = true;
            this.ColModuleNo.Visible = false;
            // 
            // ColTempId
            // 
            this.ColTempId.DataPropertyName = "TempId";
            this.ColTempId.HeaderText = "TempId";
            this.ColTempId.Name = "ColTempId";
            this.ColTempId.ReadOnly = true;
            this.ColTempId.Visible = false;
            // 
            // ColModuleDict
            // 
            this.ColModuleDict.DataPropertyName = "ModuleDict";
            this.ColModuleDict.HeaderText = "ModuleDict";
            this.ColModuleDict.Name = "ColModuleDict";
            this.ColModuleDict.ReadOnly = true;
            this.ColModuleDict.Visible = false;
            // 
            // ColEnableMk
            // 
            this.ColEnableMk.DataPropertyName = "EnableMk";
            this.ColEnableMk.HeaderText = "EnableMk";
            this.ColEnableMk.Name = "ColEnableMk";
            this.ColEnableMk.ReadOnly = true;
            this.ColEnableMk.Visible = false;
            // 
            // FrmUserModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(781, 553);
            this.Controls.Add(this.splitContainerBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUserModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting User Module";
            this.Load += new System.EventHandler(this.FrmUserModule_Load);
            this.splitContainerBase.Panel1.ResumeLayout(false);
            this.splitContainerBase.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).EndInit();
            this.splitContainerBase.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
            this.splitContainerLeft.ResumeLayout(false);
            this.gbxUser.ResumeLayout(false);
            this.gbxUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.gbxDept.ResumeLayout(false);
            this.gbxDept.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDept)).EndInit();
            this.gbxAuth.ResumeLayout(false);
            this.gbxAuth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerBase;
        private System.Windows.Forms.GroupBox gbxDept;
        private System.Windows.Forms.Label lblDeptNum;
        private System.Windows.Forms.Button btnDept;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.TextBox txtDept;
        private System.Windows.Forms.DataGridView dgvDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDeptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDeptName;
        private System.Windows.Forms.GroupBox gbxUser;
        private System.Windows.Forms.Label lblUserNum;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUsrId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUsrName;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.GroupBox gbxAuth;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.DataGridView dgvModule;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColPermit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModuleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTempId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModuleDict;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEnableMk;
    }
}