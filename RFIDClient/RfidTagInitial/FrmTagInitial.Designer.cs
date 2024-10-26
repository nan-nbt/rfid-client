namespace RFIDClient
{
    partial class FrmTagInitial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTagInitial));
            this.txtProgMsg = new System.Windows.Forms.TextBox();
            this.imgReaderStatus = new System.Windows.Forms.PictureBox();
            this.imgComPort = new System.Windows.Forms.PictureBox();
            this.cbxComPort = new System.Windows.Forms.ComboBox();
            this.lblCounted = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbxTagType = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtAutoEPC = new System.Windows.Forms.TextBox();
            this.btnRecount = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtManualEPC = new System.Windows.Forms.TextBox();
            this.btnReadTag = new System.Windows.Forms.Button();
            this.btnWriteTag = new System.Windows.Forms.Button();
            this.lstBoxTags = new System.Windows.Forms.ListBox();
            this.btnClearProgMsg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgReaderStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgComPort)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProgMsg
            // 
            this.txtProgMsg.BackColor = System.Drawing.SystemColors.Info;
            this.txtProgMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProgMsg.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtProgMsg.Location = new System.Drawing.Point(10, 416);
            this.txtProgMsg.Multiline = true;
            this.txtProgMsg.Name = "txtProgMsg";
            this.txtProgMsg.ReadOnly = true;
            this.txtProgMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProgMsg.Size = new System.Drawing.Size(551, 62);
            this.txtProgMsg.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtProgMsg, "Program Message");
            // 
            // imgReaderStatus
            // 
            this.imgReaderStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgReaderStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgReaderStatus.Image = ((System.Drawing.Image)(resources.GetObject("imgReaderStatus.Image")));
            this.imgReaderStatus.Location = new System.Drawing.Point(10, 6);
            this.imgReaderStatus.Name = "imgReaderStatus";
            this.imgReaderStatus.Size = new System.Drawing.Size(56, 47);
            this.imgReaderStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgReaderStatus.TabIndex = 2;
            this.imgReaderStatus.TabStop = false;
            this.toolTip1.SetToolTip(this.imgReaderStatus, "Connect / Disconnect Reader");
            this.imgReaderStatus.Click += new System.EventHandler(this.imgReaderStatus_Click);
            // 
            // imgComPort
            // 
            this.imgComPort.Image = ((System.Drawing.Image)(resources.GetObject("imgComPort.Image")));
            this.imgComPort.Location = new System.Drawing.Point(310, 6);
            this.imgComPort.Name = "imgComPort";
            this.imgComPort.Size = new System.Drawing.Size(45, 47);
            this.imgComPort.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgComPort.TabIndex = 3;
            this.imgComPort.TabStop = false;
            // 
            // cbxComPort
            // 
            this.cbxComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cbxComPort.Location = new System.Drawing.Point(361, 35);
            this.cbxComPort.Name = "cbxComPort";
            this.cbxComPort.Size = new System.Drawing.Size(105, 20);
            this.cbxComPort.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cbxComPort, "Comeport");
            // 
            // lblCounted
            // 
            this.lblCounted.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblCounted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCounted.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCounted.Font = new System.Drawing.Font("微軟正黑體", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCounted.ForeColor = System.Drawing.Color.Blue;
            this.lblCounted.Location = new System.Drawing.Point(-2, 39);
            this.lblCounted.Name = "lblCounted";
            this.lblCounted.Size = new System.Drawing.Size(545, 284);
            this.lblCounted.TabIndex = 5;
            this.lblCounted.Tag = "";
            this.lblCounted.Text = "0";
            this.lblCounted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxTagType
            // 
            this.cbxTagType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTagType.FormattingEnabled = true;
            this.cbxTagType.Items.AddRange(new object[] {
            "Alien",
            "AlienUHF",
            "NPX"});
            this.cbxTagType.Location = new System.Drawing.Point(361, 6);
            this.cbxTagType.Name = "cbxTagType";
            this.cbxTagType.Size = new System.Drawing.Size(105, 20);
            this.cbxTagType.TabIndex = 7;
            this.toolTip1.SetToolTip(this.cbxTagType, "Tag Type");
            this.cbxTagType.SelectedIndexChanged += new System.EventHandler(this.cbxTagType_SelectedIndexChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(8, 61);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(553, 349);
            this.tabControl.TabIndex = 8;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtAutoEPC);
            this.tabPage1.Controls.Add(this.btnRecount);
            this.tabPage1.Controls.Add(this.lblCounted);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(545, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Automatic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtAutoEPC
            // 
            this.txtAutoEPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoEPC.Location = new System.Drawing.Point(6, 7);
            this.txtAutoEPC.Name = "txtAutoEPC";
            this.txtAutoEPC.ReadOnly = true;
            this.txtAutoEPC.Size = new System.Drawing.Size(288, 21);
            this.txtAutoEPC.TabIndex = 7;
            // 
            // btnRecount
            // 
            this.btnRecount.Location = new System.Drawing.Point(464, 6);
            this.btnRecount.Name = "btnRecount";
            this.btnRecount.Size = new System.Drawing.Size(75, 23);
            this.btnRecount.TabIndex = 6;
            this.btnRecount.Text = "Recount";
            this.btnRecount.UseVisualStyleBackColor = true;
            this.btnRecount.Click += new System.EventHandler(this.btnRecount_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtManualEPC);
            this.tabPage2.Controls.Add(this.btnReadTag);
            this.tabPage2.Controls.Add(this.btnWriteTag);
            this.tabPage2.Controls.Add(this.lstBoxTags);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(545, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Manual";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtManualEPC
            // 
            this.txtManualEPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtManualEPC.Location = new System.Drawing.Point(6, 7);
            this.txtManualEPC.Name = "txtManualEPC";
            this.txtManualEPC.ReadOnly = true;
            this.txtManualEPC.Size = new System.Drawing.Size(288, 21);
            this.txtManualEPC.TabIndex = 3;
            // 
            // btnReadTag
            // 
            this.btnReadTag.Location = new System.Drawing.Point(383, 6);
            this.btnReadTag.Name = "btnReadTag";
            this.btnReadTag.Size = new System.Drawing.Size(75, 23);
            this.btnReadTag.TabIndex = 2;
            this.btnReadTag.Text = "ReadTag";
            this.btnReadTag.UseVisualStyleBackColor = true;
            this.btnReadTag.Click += new System.EventHandler(this.btnReadTag_Click);
            // 
            // btnWriteTag
            // 
            this.btnWriteTag.Location = new System.Drawing.Point(464, 6);
            this.btnWriteTag.Name = "btnWriteTag";
            this.btnWriteTag.Size = new System.Drawing.Size(75, 23);
            this.btnWriteTag.TabIndex = 1;
            this.btnWriteTag.Text = "WriteTag";
            this.btnWriteTag.UseVisualStyleBackColor = true;
            this.btnWriteTag.Click += new System.EventHandler(this.btnWriteTag_Click);
            // 
            // lstBoxTags
            // 
            this.lstBoxTags.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lstBoxTags.FormattingEnabled = true;
            this.lstBoxTags.ItemHeight = 35;
            this.lstBoxTags.Location = new System.Drawing.Point(0, 39);
            this.lstBoxTags.Name = "lstBoxTags";
            this.lstBoxTags.ScrollAlwaysVisible = true;
            this.lstBoxTags.Size = new System.Drawing.Size(545, 284);
            this.lstBoxTags.TabIndex = 0;
            // 
            // btnClearProgMsg
            // 
            this.btnClearProgMsg.Location = new System.Drawing.Point(482, 6);
            this.btnClearProgMsg.Name = "btnClearProgMsg";
            this.btnClearProgMsg.Size = new System.Drawing.Size(79, 49);
            this.btnClearProgMsg.TabIndex = 9;
            this.btnClearProgMsg.Text = "Clear Log";
            this.btnClearProgMsg.UseVisualStyleBackColor = true;
            this.btnClearProgMsg.Click += new System.EventHandler(this.btnClearProgMsg_Click);
            // 
            // FrmTagInitial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 488);
            this.Controls.Add(this.btnClearProgMsg);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.txtProgMsg);
            this.Controls.Add(this.imgReaderStatus);
            this.Controls.Add(this.cbxTagType);
            this.Controls.Add(this.imgComPort);
            this.Controls.Add(this.cbxComPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmTagInitial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tag Initial";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TagInitial_FormClosing);
            this.Load += new System.EventHandler(this.TagInitial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgReaderStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgComPort)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProgMsg;
        private System.Windows.Forms.PictureBox imgReaderStatus;
        private System.Windows.Forms.PictureBox imgComPort;
        private System.Windows.Forms.ComboBox cbxComPort;
        private System.Windows.Forms.Label lblCounted;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbxTagType;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstBoxTags;
        private System.Windows.Forms.Button btnWriteTag;
        private System.Windows.Forms.Button btnClearProgMsg;
        private System.Windows.Forms.Button btnRecount;
        private System.Windows.Forms.Button btnReadTag;
        private System.Windows.Forms.TextBox txtAutoEPC;
        private System.Windows.Forms.TextBox txtManualEPC;
    }
}

