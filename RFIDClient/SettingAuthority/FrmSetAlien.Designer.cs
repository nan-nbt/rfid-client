namespace RFIDClient
{
    partial class FrmSetAlien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetAlien));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAlien = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRfLevel = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRssiMax = new System.Windows.Forms.TextBox();
            this.txtRssiMin = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.txtRfAtten = new System.Windows.Forms.TextBox();
            this.lblRfAtten = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radAnt1 = new System.Windows.Forms.RadioButton();
            this.radAnt0 = new System.Windows.Forms.RadioButton();
            this.tabUHF = new System.Windows.Forms.TabPage();
            this.btnDefult = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUhfRead = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUhfWrite = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUhfTime = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabAlien.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabUHF.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAlien);
            this.tabControl1.Controls.Add(this.tabUHF);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(394, 286);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAlien
            // 
            this.tabAlien.Controls.Add(this.groupBox4);
            this.tabAlien.Controls.Add(this.groupBox3);
            this.tabAlien.Controls.Add(this.groupBox2);
            this.tabAlien.Controls.Add(this.lblRfAtten);
            this.tabAlien.Controls.Add(this.groupBox1);
            this.tabAlien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAlien.Location = new System.Drawing.Point(4, 27);
            this.tabAlien.Name = "tabAlien";
            this.tabAlien.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlien.Size = new System.Drawing.Size(386, 255);
            this.tabAlien.TabIndex = 0;
            this.tabAlien.Text = "ALIEN Reader";
            this.tabAlien.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtRfLevel);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(3, 183);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(380, 59);
            this.groupBox4.TabIndex = 45;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "RF Level:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(284, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 19);
            this.label3.TabIndex = 30;
            this.label3.Text = "0 ~ 316";
            // 
            // txtRfLevel
            // 
            this.txtRfLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRfLevel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRfLevel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtRfLevel.Location = new System.Drawing.Point(16, 25);
            this.txtRfLevel.MaxLength = 60;
            this.txtRfLevel.Name = "txtRfLevel";
            this.txtRfLevel.Size = new System.Drawing.Size(60, 26);
            this.txtRfLevel.TabIndex = 26;
            this.txtRfLevel.TextChanged += new System.EventHandler(this.txtRfLevel_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtRssiMax);
            this.groupBox3.Controls.Add(this.txtRssiMin);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(3, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 59);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RSSI Filter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(271, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 19);
            this.label2.TabIndex = 29;
            this.label2.Text = "0 ~ 60000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 19);
            this.label1.TabIndex = 28;
            this.label1.Text = "~";
            // 
            // txtRssiMax
            // 
            this.txtRssiMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRssiMax.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRssiMax.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtRssiMax.Location = new System.Drawing.Point(145, 26);
            this.txtRssiMax.MaxLength = 60;
            this.txtRssiMax.Name = "txtRssiMax";
            this.txtRssiMax.Size = new System.Drawing.Size(60, 26);
            this.txtRssiMax.TabIndex = 26;
            this.txtRssiMax.TextChanged += new System.EventHandler(this.txtRssiMax_TextChanged);
            // 
            // txtRssiMin
            // 
            this.txtRssiMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRssiMin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRssiMin.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtRssiMin.Location = new System.Drawing.Point(16, 25);
            this.txtRssiMin.MaxLength = 30;
            this.txtRssiMin.Name = "txtRssiMin";
            this.txtRssiMin.Size = new System.Drawing.Size(60, 26);
            this.txtRssiMin.TabIndex = 27;
            this.txtRssiMin.TextChanged += new System.EventHandler(this.txtRssiMin_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.txtRfAtten);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 62);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RF Attenuation:";
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.trackBar1.Location = new System.Drawing.Point(117, 22);
            this.trackBar1.Maximum = 120;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(260, 37);
            this.trackBar1.TabIndex = 39;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // txtRfAtten
            // 
            this.txtRfAtten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRfAtten.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRfAtten.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtRfAtten.Location = new System.Drawing.Point(16, 25);
            this.txtRfAtten.MaxLength = 60;
            this.txtRfAtten.Name = "txtRfAtten";
            this.txtRfAtten.Size = new System.Drawing.Size(60, 26);
            this.txtRfAtten.TabIndex = 26;
            this.txtRfAtten.TextChanged += new System.EventHandler(this.txtRfAtten_TextChanged);
            // 
            // lblRfAtten
            // 
            this.lblRfAtten.AutoSize = true;
            this.lblRfAtten.BackColor = System.Drawing.Color.Transparent;
            this.lblRfAtten.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRfAtten.Location = new System.Drawing.Point(18, 241);
            this.lblRfAtten.Name = "lblRfAtten";
            this.lblRfAtten.Size = new System.Drawing.Size(0, 24);
            this.lblRfAtten.TabIndex = 41;
            this.lblRfAtten.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radAnt1);
            this.groupBox1.Controls.Add(this.radAnt0);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 59);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Antenna Selection:";
            // 
            // radAnt1
            // 
            this.radAnt1.AutoSize = true;
            this.radAnt1.Location = new System.Drawing.Point(181, 25);
            this.radAnt1.Name = "radAnt1";
            this.radAnt1.Size = new System.Drawing.Size(159, 23);
            this.radAnt1.TabIndex = 37;
            this.radAnt1.Tag = "1";
            this.radAnt1.Text = "Ant1（External）";
            this.radAnt1.UseVisualStyleBackColor = true;
            // 
            // radAnt0
            // 
            this.radAnt0.AutoSize = true;
            this.radAnt0.Location = new System.Drawing.Point(12, 25);
            this.radAnt0.Name = "radAnt0";
            this.radAnt0.Size = new System.Drawing.Size(153, 23);
            this.radAnt0.TabIndex = 36;
            this.radAnt0.Tag = "0";
            this.radAnt0.Text = "Ant0（Internal）";
            this.radAnt0.UseVisualStyleBackColor = true;
            // 
            // tabUHF
            // 
            this.tabUHF.Controls.Add(this.groupBox7);
            this.tabUHF.Controls.Add(this.groupBox6);
            this.tabUHF.Controls.Add(this.groupBox5);
            this.tabUHF.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabUHF.Location = new System.Drawing.Point(4, 27);
            this.tabUHF.Name = "tabUHF";
            this.tabUHF.Padding = new System.Windows.Forms.Padding(3);
            this.tabUHF.Size = new System.Drawing.Size(386, 255);
            this.tabUHF.TabIndex = 1;
            this.tabUHF.Text = "UHF Reader";
            this.tabUHF.UseVisualStyleBackColor = true;
            // 
            // btnDefult
            // 
            this.btnDefult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnDefult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefult.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefult.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnDefult.Location = new System.Drawing.Point(99, 292);
            this.btnDefult.Name = "btnDefult";
            this.btnDefult.Size = new System.Drawing.Size(81, 29);
            this.btnDefult.TabIndex = 47;
            this.btnDefult.Text = "Get Ref.";
            this.btnDefult.UseVisualStyleBackColor = false;
            this.btnDefult.Click += new System.EventHandler(this.btnDefult_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.Location = new System.Drawing.Point(212, 292);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 29);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtUhfRead);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(380, 59);
            this.groupBox5.TabIndex = 46;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Output power for read:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(311, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 19);
            this.label4.TabIndex = 30;
            this.label4.Text = "1 ~ 30";
            // 
            // txtUhfRead
            // 
            this.txtUhfRead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUhfRead.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUhfRead.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtUhfRead.Location = new System.Drawing.Point(16, 25);
            this.txtUhfRead.MaxLength = 60;
            this.txtUhfRead.Name = "txtUhfRead";
            this.txtUhfRead.Size = new System.Drawing.Size(60, 26);
            this.txtUhfRead.TabIndex = 26;
            this.txtUhfRead.TextChanged += new System.EventHandler(this.txtUhfRead_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.txtUhfWrite);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(3, 62);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(380, 59);
            this.groupBox6.TabIndex = 47;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Output power for write:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(311, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 19);
            this.label5.TabIndex = 30;
            this.label5.Text = "1 ~ 30";
            // 
            // txtUhfWrite
            // 
            this.txtUhfWrite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUhfWrite.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUhfWrite.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtUhfWrite.Location = new System.Drawing.Point(16, 25);
            this.txtUhfWrite.MaxLength = 60;
            this.txtUhfWrite.Name = "txtUhfWrite";
            this.txtUhfWrite.Size = new System.Drawing.Size(60, 26);
            this.txtUhfWrite.TabIndex = 26;
            this.txtUhfWrite.TextChanged += new System.EventHandler(this.txtUhfWrite_TextChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.txtUhfTime);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(3, 121);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(380, 59);
            this.groupBox7.TabIndex = 48;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Maximum try write times:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(161, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 19);
            this.label6.TabIndex = 30;
            this.label6.Text = "0 or empty try infinite times";
            // 
            // txtUhfTime
            // 
            this.txtUhfTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUhfTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUhfTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtUhfTime.Location = new System.Drawing.Point(16, 25);
            this.txtUhfTime.MaxLength = 60;
            this.txtUhfTime.Name = "txtUhfTime";
            this.txtUhfTime.Size = new System.Drawing.Size(60, 26);
            this.txtUhfTime.TabIndex = 26;
            this.txtUhfTime.TextChanged += new System.EventHandler(this.txtUhfTime_TextChanged);
            // 
            // FrmSetAlien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(394, 336);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDefult);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetAlien";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting Reader Parameters";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmSetAlien_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabAlien.ResumeLayout(false);
            this.tabAlien.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabUHF.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAlien;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRfLevel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRssiMax;
        private System.Windows.Forms.TextBox txtRssiMin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox txtRfAtten;
        private System.Windows.Forms.Label lblRfAtten;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radAnt1;
        private System.Windows.Forms.RadioButton radAnt0;
        private System.Windows.Forms.TabPage tabUHF;
        private System.Windows.Forms.Button btnDefult;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUhfTime;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUhfWrite;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUhfRead;

    }
}