namespace RFIDClient
{
    partial class FrmFingerprint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFingerprint));
            this.splitContainerBase = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.gbxUser = new System.Windows.Forms.GroupBox();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.ColPccuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUsrId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUsrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblUser = new System.Windows.Forms.Label();
            this.panelUser = new System.Windows.Forms.Panel();
            this.lblUserNum = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.gbxFinger = new System.Windows.Forms.GroupBox();
            this.dgvFingerprint = new System.Windows.Forms.DataGridView();
            this.ColUserFingerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFingerPositionDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColReaderVendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFingerPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFp = new System.Windows.Forms.Panel();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.cbxFinger = new System.Windows.Forms.ComboBox();
            this.lblFinger = new System.Windows.Forms.Label();
            this.btnDelFp = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblTimes = new System.Windows.Forms.Label();
            this.lblTimeInfo = new System.Windows.Forms.Label();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.proBarTimes = new System.Windows.Forms.ProgressBar();
            this.pbFingerprint = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbxStatus = new System.Windows.Forms.GroupBox();
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
            this.panelUser.SuspendLayout();
            this.gbxFinger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFingerprint)).BeginInit();
            this.panelFp.SuspendLayout();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).BeginInit();
            this.gbxStatus.SuspendLayout();
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
            this.splitContainerBase.Panel2.Controls.Add(this.gbxStatus);
            this.splitContainerBase.Size = new System.Drawing.Size(902, 638);
            this.splitContainerBase.SplitterDistance = 567;
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
            this.splitContainerLeft.Panel2.Controls.Add(this.gbxFinger);
            this.splitContainerLeft.Size = new System.Drawing.Size(567, 638);
            this.splitContainerLeft.SplitterDistance = 332;
            this.splitContainerLeft.SplitterWidth = 2;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // gbxUser
            // 
            this.gbxUser.Controls.Add(this.dgvUser);
            this.gbxUser.Controls.Add(this.lblUser);
            this.gbxUser.Controls.Add(this.panelUser);
            this.gbxUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxUser.ForeColor = System.Drawing.Color.Silver;
            this.gbxUser.Location = new System.Drawing.Point(0, 0);
            this.gbxUser.Name = "gbxUser";
            this.gbxUser.Size = new System.Drawing.Size(567, 332);
            this.gbxUser.TabIndex = 1;
            this.gbxUser.TabStop = false;
            this.gbxUser.Text = "User List";
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
            this.ColPccuid,
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
            this.dgvUser.Location = new System.Drawing.Point(3, 52);
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
            this.dgvUser.Size = new System.Drawing.Size(561, 277);
            this.dgvUser.TabIndex = 3;
            this.dgvUser.Tag = "AAA";
            this.dgvUser.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellClick);
            this.dgvUser.SelectionChanged += new System.EventHandler(this.dgvUser_SelectionChanged);
            // 
            // ColPccuid
            // 
            this.ColPccuid.DataPropertyName = "Pccuid";
            this.ColPccuid.HeaderText = "PccUid";
            this.ColPccuid.Name = "ColPccuid";
            this.ColPccuid.ReadOnly = true;
            this.ColPccuid.Width = 160;
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
            this.ColUsrName.Width = 200;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUser.Location = new System.Drawing.Point(8, 28);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(73, 18);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "Keyword:";
            // 
            // panelUser
            // 
            this.panelUser.Controls.Add(this.lblUserNum);
            this.panelUser.Controls.Add(this.txtUser);
            this.panelUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUser.Location = new System.Drawing.Point(3, 22);
            this.panelUser.Margin = new System.Windows.Forms.Padding(0);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(561, 30);
            this.panelUser.TabIndex = 13;
            // 
            // lblUserNum
            // 
            this.lblUserNum.AutoSize = true;
            this.lblUserNum.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNum.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUserNum.Location = new System.Drawing.Point(369, 6);
            this.lblUserNum.Name = "lblUserNum";
            this.lblUserNum.Size = new System.Drawing.Size(18, 19);
            this.lblUserNum.TabIndex = 12;
            this.lblUserNum.Text = "0";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtUser.Location = new System.Drawing.Point(81, 2);
            this.txtUser.Margin = new System.Windows.Forms.Padding(0);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(275, 26);
            this.txtUser.TabIndex = 9;
            this.txtUser.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUser_KeyUp);
            // 
            // gbxFinger
            // 
            this.gbxFinger.Controls.Add(this.dgvFingerprint);
            this.gbxFinger.Controls.Add(this.panelFp);
            this.gbxFinger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxFinger.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxFinger.ForeColor = System.Drawing.Color.Silver;
            this.gbxFinger.Location = new System.Drawing.Point(0, 0);
            this.gbxFinger.Name = "gbxFinger";
            this.gbxFinger.Size = new System.Drawing.Size(567, 304);
            this.gbxFinger.TabIndex = 4;
            this.gbxFinger.TabStop = false;
            this.gbxFinger.Text = "User Fingerprint";
            // 
            // dgvFingerprint
            // 
            this.dgvFingerprint.AllowUserToAddRows = false;
            this.dgvFingerprint.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvFingerprint.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvFingerprint.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvFingerprint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFingerprint.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFingerprint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvFingerprint.ColumnHeadersHeight = 24;
            this.dgvFingerprint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFingerprint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColUserFingerId,
            this.ColFingerPositionDesc,
            this.ColReaderVendor,
            this.ColFingerPosition});
            this.dgvFingerprint.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFingerprint.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvFingerprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFingerprint.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvFingerprint.EnableHeadersVisualStyles = false;
            this.dgvFingerprint.GridColor = System.Drawing.SystemColors.Highlight;
            this.dgvFingerprint.Location = new System.Drawing.Point(3, 54);
            this.dgvFingerprint.Margin = new System.Windows.Forms.Padding(0);
            this.dgvFingerprint.Name = "dgvFingerprint";
            this.dgvFingerprint.ReadOnly = true;
            this.dgvFingerprint.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFingerprint.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvFingerprint.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvFingerprint.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvFingerprint.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvFingerprint.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFingerprint.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.dgvFingerprint.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            this.dgvFingerprint.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.dgvFingerprint.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFingerprint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFingerprint.Size = new System.Drawing.Size(561, 247);
            this.dgvFingerprint.TabIndex = 4;
            this.dgvFingerprint.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFingerprint_CellClick);
            // 
            // ColUserFingerId
            // 
            this.ColUserFingerId.DataPropertyName = "UserFingerId";
            this.ColUserFingerId.HeaderText = "User Finger Id";
            this.ColUserFingerId.Name = "ColUserFingerId";
            this.ColUserFingerId.ReadOnly = true;
            this.ColUserFingerId.Width = 120;
            // 
            // ColFingerPositionDesc
            // 
            this.ColFingerPositionDesc.DataPropertyName = "FingerPositionDesc";
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColFingerPositionDesc.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColFingerPositionDesc.HeaderText = "Finger Position";
            this.ColFingerPositionDesc.Name = "ColFingerPositionDesc";
            this.ColFingerPositionDesc.ReadOnly = true;
            this.ColFingerPositionDesc.Width = 280;
            // 
            // ColReaderVendor
            // 
            this.ColReaderVendor.DataPropertyName = "ReaderVendor";
            this.ColReaderVendor.HeaderText = "Reader Vendor";
            this.ColReaderVendor.Name = "ColReaderVendor";
            this.ColReaderVendor.ReadOnly = true;
            this.ColReaderVendor.Width = 140;
            // 
            // ColFingerPosition
            // 
            this.ColFingerPosition.DataPropertyName = "FingerPosition";
            this.ColFingerPosition.HeaderText = "Finger Position";
            this.ColFingerPosition.Name = "ColFingerPosition";
            this.ColFingerPosition.ReadOnly = true;
            this.ColFingerPosition.Visible = false;
            // 
            // panelFp
            // 
            this.panelFp.Controls.Add(this.chkFilter);
            this.panelFp.Controls.Add(this.cbxFinger);
            this.panelFp.Controls.Add(this.lblFinger);
            this.panelFp.Controls.Add(this.btnDelFp);
            this.panelFp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFp.Location = new System.Drawing.Point(3, 22);
            this.panelFp.Name = "panelFp";
            this.panelFp.Size = new System.Drawing.Size(561, 32);
            this.panelFp.TabIndex = 5;
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkFilter.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.chkFilter.Location = new System.Drawing.Point(408, 5);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(60, 22);
            this.chkFilter.TabIndex = 35;
            this.chkFilter.Text = "Filter";
            this.chkFilter.UseVisualStyleBackColor = true;
            this.chkFilter.Click += new System.EventHandler(this.chkLock_Click);
            // 
            // cbxFinger
            // 
            this.cbxFinger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFinger.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxFinger.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFinger.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbxFinger.FormattingEnabled = true;
            this.cbxFinger.Location = new System.Drawing.Point(129, 4);
            this.cbxFinger.Name = "cbxFinger";
            this.cbxFinger.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbxFinger.Size = new System.Drawing.Size(275, 24);
            this.cbxFinger.TabIndex = 34;
            this.cbxFinger.SelectedIndexChanged += new System.EventHandler(this.cbxFinger_SelectedIndexChanged);
            // 
            // lblFinger
            // 
            this.lblFinger.AutoSize = true;
            this.lblFinger.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinger.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblFinger.Location = new System.Drawing.Point(3, 7);
            this.lblFinger.Name = "lblFinger";
            this.lblFinger.Size = new System.Drawing.Size(118, 18);
            this.lblFinger.TabIndex = 33;
            this.lblFinger.Text = "Finger Position:";
            // 
            // btnDelFp
            // 
            this.btnDelFp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnDelFp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelFp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelFp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelFp.ForeColor = System.Drawing.Color.Red;
            this.btnDelFp.Location = new System.Drawing.Point(472, 2);
            this.btnDelFp.Name = "btnDelFp";
            this.btnDelFp.Size = new System.Drawing.Size(68, 27);
            this.btnDelFp.TabIndex = 32;
            this.btnDelFp.Text = "Delete";
            this.btnDelFp.UseVisualStyleBackColor = false;
            this.btnDelFp.Click += new System.EventHandler(this.btnDelFp_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.Azure;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtMessage.Location = new System.Drawing.Point(3, 410);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(0);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(325, 225);
            this.txtMessage.TabIndex = 14;
            this.txtMessage.Text = "";
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.lblTimes);
            this.panelInfo.Controls.Add(this.lblTimeInfo);
            this.panelInfo.Controls.Add(this.picStatus);
            this.panelInfo.Controls.Add(this.proBarTimes);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelInfo.Location = new System.Drawing.Point(3, 352);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(325, 58);
            this.panelInfo.TabIndex = 18;
            // 
            // lblTimes
            // 
            this.lblTimes.AutoSize = true;
            this.lblTimes.BackColor = System.Drawing.Color.Transparent;
            this.lblTimes.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimes.ForeColor = System.Drawing.Color.Green;
            this.lblTimes.Location = new System.Drawing.Point(203, 26);
            this.lblTimes.Margin = new System.Windows.Forms.Padding(0);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(60, 29);
            this.lblTimes.TabIndex = 35;
            this.lblTimes.Text = "0 / 4";
            this.lblTimes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTimeInfo
            // 
            this.lblTimeInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTimeInfo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeInfo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTimeInfo.Location = new System.Drawing.Point(0, 0);
            this.lblTimeInfo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTimeInfo.Name = "lblTimeInfo";
            this.lblTimeInfo.Size = new System.Drawing.Size(267, 25);
            this.lblTimeInfo.TabIndex = 34;
            this.lblTimeInfo.Text = "Success times / Enrollment times";
            this.lblTimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picStatus
            // 
            this.picStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.picStatus.Location = new System.Drawing.Point(267, 0);
            this.picStatus.Margin = new System.Windows.Forms.Padding(0);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(58, 58);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStatus.TabIndex = 33;
            this.picStatus.TabStop = false;
            // 
            // proBarTimes
            // 
            this.proBarTimes.Location = new System.Drawing.Point(0, 28);
            this.proBarTimes.Margin = new System.Windows.Forms.Padding(0);
            this.proBarTimes.Name = "proBarTimes";
            this.proBarTimes.Size = new System.Drawing.Size(200, 25);
            this.proBarTimes.TabIndex = 36;
            // 
            // pbFingerprint
            // 
            this.pbFingerprint.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbFingerprint.Location = new System.Drawing.Point(3, 22);
            this.pbFingerprint.Margin = new System.Windows.Forms.Padding(0);
            this.pbFingerprint.Name = "pbFingerprint";
            this.pbFingerprint.Size = new System.Drawing.Size(325, 330);
            this.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFingerprint.TabIndex = 5;
            this.pbFingerprint.TabStop = false;
            // 
            // gbxStatus
            // 
            this.gbxStatus.Controls.Add(this.txtMessage);
            this.gbxStatus.Controls.Add(this.panelInfo);
            this.gbxStatus.Controls.Add(this.pbFingerprint);
            this.gbxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxStatus.Font = new System.Drawing.Font("Arial", 12F);
            this.gbxStatus.ForeColor = System.Drawing.Color.Silver;
            this.gbxStatus.Location = new System.Drawing.Point(0, 0);
            this.gbxStatus.Name = "gbxStatus";
            this.gbxStatus.Size = new System.Drawing.Size(331, 638);
            this.gbxStatus.TabIndex = 19;
            this.gbxStatus.TabStop = false;
            this.gbxStatus.Text = "Realtime Infomation";
            // 
            // FrmFingerprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(902, 638);
            this.Controls.Add(this.splitContainerBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFingerprint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fingerprint Enrollment";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFingerprint_FormClosed);
            this.Load += new System.EventHandler(this.FrmFingerprint_Load);
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
            this.panelUser.ResumeLayout(false);
            this.panelUser.PerformLayout();
            this.gbxFinger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFingerprint)).EndInit();
            this.panelFp.ResumeLayout(false);
            this.panelFp.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).EndInit();
            this.gbxStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerBase;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.GroupBox gbxUser;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.Label lblUserNum;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.GroupBox gbxFinger;
        private System.Windows.Forms.DataGridView dgvFingerprint;
        private System.Windows.Forms.PictureBox pbFingerprint;
        private System.Windows.Forms.Button btnDelFp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPccuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUsrId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUsrName;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.Panel panelFp;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblTimeInfo;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.ProgressBar proBarTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUserFingerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFingerPositionDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColReaderVendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFingerPosition;
        private System.Windows.Forms.ComboBox cbxFinger;
        private System.Windows.Forms.Label lblFinger;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.GroupBox gbxStatus;
    }
}