namespace RFIDClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabPageAuth = new System.Windows.Forms.TabPage();
            this.panelFingerprint = new System.Windows.Forms.Panel();
            this.txtFingerprint = new System.Windows.Forms.TextBox();
            this.panelModule = new System.Windows.Forms.Panel();
            this.txtModule = new System.Windows.Forms.TextBox();
            this.panelUserModule = new System.Windows.Forms.Panel();
            this.txtUserModule = new System.Windows.Forms.TextBox();
            this.tabPageModule = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.panelBasic = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.picFingerprint = new System.Windows.Forms.PictureBox();
            this.picModule = new System.Windows.Forms.PictureBox();
            this.picUserModule = new System.Windows.Forms.PictureBox();
            this.tabPageAuth.SuspendLayout();
            this.panelFingerprint.SuspendLayout();
            this.panelModule.SuspendLayout();
            this.panelUserModule.SuspendLayout();
            this.tabPageModule.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picModule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserModule)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageAuth
            // 
            this.tabPageAuth.BackColor = System.Drawing.Color.Azure;
            this.tabPageAuth.Controls.Add(this.panelFingerprint);
            this.tabPageAuth.Controls.Add(this.panelModule);
            this.tabPageAuth.Controls.Add(this.panelUserModule);
            this.tabPageAuth.ForeColor = System.Drawing.Color.Navy;
            this.tabPageAuth.Location = new System.Drawing.Point(4, 30);
            this.tabPageAuth.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageAuth.Name = "tabPageAuth";
            this.tabPageAuth.Size = new System.Drawing.Size(946, 707);
            this.tabPageAuth.TabIndex = 1;
            this.tabPageAuth.Text = "Setting Authority";
            // 
            // panelFingerprint
            // 
            this.panelFingerprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFingerprint.Controls.Add(this.txtFingerprint);
            this.panelFingerprint.Controls.Add(this.picFingerprint);
            this.panelFingerprint.Location = new System.Drawing.Point(393, 11);
            this.panelFingerprint.Name = "panelFingerprint";
            this.panelFingerprint.Size = new System.Drawing.Size(160, 180);
            this.panelFingerprint.TabIndex = 9;
            this.panelFingerprint.Click += new System.EventHandler(this.panelFingerprint_Click);
            // 
            // txtFingerprint
            // 
            this.txtFingerprint.BackColor = System.Drawing.Color.White;
            this.txtFingerprint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFingerprint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtFingerprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFingerprint.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFingerprint.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtFingerprint.Location = new System.Drawing.Point(0, 136);
            this.txtFingerprint.Multiline = true;
            this.txtFingerprint.Name = "txtFingerprint";
            this.txtFingerprint.ReadOnly = true;
            this.txtFingerprint.Size = new System.Drawing.Size(158, 42);
            this.txtFingerprint.TabIndex = 4;
            this.txtFingerprint.Tag = "txtTagInit";
            this.txtFingerprint.Text = "Fingerprint Enrollment";
            this.txtFingerprint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFingerprint.Click += new System.EventHandler(this.panelFingerprint_Click);
            // 
            // panelModule
            // 
            this.panelModule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModule.Controls.Add(this.txtModule);
            this.panelModule.Controls.Add(this.picModule);
            this.panelModule.Location = new System.Drawing.Point(8, 11);
            this.panelModule.Name = "panelModule";
            this.panelModule.Size = new System.Drawing.Size(160, 180);
            this.panelModule.TabIndex = 8;
            this.panelModule.Click += new System.EventHandler(this.panelModule_Click);
            // 
            // txtModule
            // 
            this.txtModule.BackColor = System.Drawing.Color.White;
            this.txtModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtModule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModule.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModule.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtModule.Location = new System.Drawing.Point(0, 136);
            this.txtModule.Multiline = true;
            this.txtModule.Name = "txtModule";
            this.txtModule.ReadOnly = true;
            this.txtModule.Size = new System.Drawing.Size(158, 42);
            this.txtModule.TabIndex = 4;
            this.txtModule.Tag = "txtTagInit";
            this.txtModule.Text = "Maintain Module";
            this.txtModule.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtModule.Click += new System.EventHandler(this.panelModule_Click);
            // 
            // panelUserModule
            // 
            this.panelUserModule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserModule.Controls.Add(this.txtUserModule);
            this.panelUserModule.Controls.Add(this.picUserModule);
            this.panelUserModule.Location = new System.Drawing.Point(200, 12);
            this.panelUserModule.Name = "panelUserModule";
            this.panelUserModule.Size = new System.Drawing.Size(160, 180);
            this.panelUserModule.TabIndex = 7;
            this.panelUserModule.Click += new System.EventHandler(this.panelUserModule_Click);
            // 
            // txtUserModule
            // 
            this.txtUserModule.BackColor = System.Drawing.Color.White;
            this.txtUserModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserModule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtUserModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserModule.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserModule.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtUserModule.Location = new System.Drawing.Point(0, 136);
            this.txtUserModule.Multiline = true;
            this.txtUserModule.Name = "txtUserModule";
            this.txtUserModule.ReadOnly = true;
            this.txtUserModule.Size = new System.Drawing.Size(158, 42);
            this.txtUserModule.TabIndex = 4;
            this.txtUserModule.Tag = "txtTagInit";
            this.txtUserModule.Text = "Setting User Module";
            this.txtUserModule.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUserModule.Click += new System.EventHandler(this.panelUserModule_Click);
            // 
            // tabPageModule
            // 
            this.tabPageModule.BackColor = System.Drawing.Color.Azure;
            this.tabPageModule.Controls.Add(this.groupBox1);
            this.tabPageModule.Controls.Add(this.panelBasic);
            this.tabPageModule.ForeColor = System.Drawing.Color.Navy;
            this.tabPageModule.Location = new System.Drawing.Point(4, 30);
            this.tabPageModule.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageModule.Name = "tabPageModule";
            this.tabPageModule.Size = new System.Drawing.Size(946, 707);
            this.tabPageModule.TabIndex = 0;
            this.tabPageModule.Text = "Basic Module";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDesc);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(127)))), ((int)(((byte)(206)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 603);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(946, 104);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functional Description";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.Azure;
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDesc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(127)))), ((int)(((byte)(206)))));
            this.txtDesc.Location = new System.Drawing.Point(3, 22);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(940, 79);
            this.txtDesc.TabIndex = 5;
            // 
            // panelBasic
            // 
            this.panelBasic.AutoScroll = true;
            this.panelBasic.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panelBasic.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBasic.Location = new System.Drawing.Point(0, 0);
            this.panelBasic.Name = "panelBasic";
            this.panelBasic.Size = new System.Drawing.Size(946, 603);
            this.panelBasic.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageModule);
            this.tabControl1.Controls.Add(this.tabPageAuth);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(6, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(954, 741);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // picFingerprint
            // 
            this.picFingerprint.BackColor = System.Drawing.Color.Azure;
            this.picFingerprint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFingerprint.Dock = System.Windows.Forms.DockStyle.Top;
            this.picFingerprint.Image = global::RFIDClient.Properties.Resources.FrmFingerprint;
            this.picFingerprint.Location = new System.Drawing.Point(0, 0);
            this.picFingerprint.Name = "picFingerprint";
            this.picFingerprint.Size = new System.Drawing.Size(158, 136);
            this.picFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFingerprint.TabIndex = 3;
            this.picFingerprint.TabStop = false;
            this.picFingerprint.Tag = "txtTagInit";
            this.picFingerprint.Click += new System.EventHandler(this.panelFingerprint_Click);
            // 
            // picModule
            // 
            this.picModule.BackColor = System.Drawing.Color.Azure;
            this.picModule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picModule.Dock = System.Windows.Forms.DockStyle.Top;
            this.picModule.Image = global::RFIDClient.Properties.Resources.FrmModule1;
            this.picModule.Location = new System.Drawing.Point(0, 0);
            this.picModule.Name = "picModule";
            this.picModule.Size = new System.Drawing.Size(158, 136);
            this.picModule.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picModule.TabIndex = 3;
            this.picModule.TabStop = false;
            this.picModule.Tag = "txtTagInit";
            this.picModule.Click += new System.EventHandler(this.panelModule_Click);
            // 
            // picUserModule
            // 
            this.picUserModule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUserModule.Dock = System.Windows.Forms.DockStyle.Top;
            this.picUserModule.Image = global::RFIDClient.Properties.Resources.FrmUserModule;
            this.picUserModule.Location = new System.Drawing.Point(0, 0);
            this.picUserModule.Name = "picUserModule";
            this.picUserModule.Size = new System.Drawing.Size(158, 136);
            this.picUserModule.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserModule.TabIndex = 3;
            this.picUserModule.TabStop = false;
            this.picUserModule.Tag = "txtTagInit";
            this.picUserModule.Click += new System.EventHandler(this.panelUserModule_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(954, 741);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFID Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabPageAuth.ResumeLayout(false);
            this.panelFingerprint.ResumeLayout(false);
            this.panelFingerprint.PerformLayout();
            this.panelModule.ResumeLayout(false);
            this.panelModule.PerformLayout();
            this.panelUserModule.ResumeLayout(false);
            this.panelUserModule.PerformLayout();
            this.tabPageModule.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFingerprint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picModule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUserModule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageAuth;
        private System.Windows.Forms.TabPage tabPageModule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Panel panelBasic;
        private System.Windows.Forms.Panel panelUserModule;
        private System.Windows.Forms.TextBox txtUserModule;
        private System.Windows.Forms.PictureBox picUserModule;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panelModule;
        private System.Windows.Forms.TextBox txtModule;
        private System.Windows.Forms.PictureBox picModule;
        private System.Windows.Forms.Panel panelFingerprint;
        private System.Windows.Forms.TextBox txtFingerprint;
        private System.Windows.Forms.PictureBox picFingerprint;






    }
}

