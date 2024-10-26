namespace RFIDClient
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.bntOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picAlien = new System.Windows.Forms.PictureBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblFinger = new System.Windows.Forms.Label();
            this.lblLine00 = new System.Windows.Forms.Label();
            this.lblVericalR = new System.Windows.Forms.Label();
            this.lblVericalL = new System.Windows.Forms.Label();
            this.lblLine05 = new System.Windows.Forms.Label();
            this.lblLine04 = new System.Windows.Forms.Label();
            this.lblLine03 = new System.Windows.Forms.Label();
            this.lblLine02 = new System.Windows.Forms.Label();
            this.lblLine01 = new System.Windows.Forms.Label();
            this.chkBoxTest = new System.Windows.Forms.CheckBox();
            this.cbxLang = new System.Windows.Forms.ComboBox();
            this.lblLang = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.cbxCountry = new System.Windows.Forms.ComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblAcct = new System.Windows.Forms.Label();
            this.lblFact = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbxVendor = new System.Windows.Forms.ComboBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPsw = new System.Windows.Forms.TextBox();
            this.panelMask = new System.Windows.Forms.Panel();
            this.picFinger = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(274, 122);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 22);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(100, 86);
            this.webBrowser.TabIndex = 2;
            this.webBrowser.Visible = false;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // bntOK
            // 
            this.bntOK.BackColor = System.Drawing.Color.Transparent;
            this.bntOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bntOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(237)))), ((int)(((byte)(248)))));
            this.bntOK.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntOK.Image = global::RFIDClient.Properties.Resources.imgbtnlogin;
            this.bntOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bntOK.Location = new System.Drawing.Point(130, 274);
            this.bntOK.Name = "bntOK";
            this.bntOK.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bntOK.Size = new System.Drawing.Size(139, 40);
            this.bntOK.TabIndex = 16;
            this.bntOK.Text = "Login";
            this.bntOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntOK.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bntOK.UseVisualStyleBackColor = false;
            this.bntOK.Click += new System.EventHandler(this.bntOK_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::RFIDClient.Properties.Resources.login;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picAlien);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.lblFinger);
            this.panel1.Controls.Add(this.lblLine00);
            this.panel1.Controls.Add(this.lblVericalR);
            this.panel1.Controls.Add(this.lblVericalL);
            this.panel1.Controls.Add(this.lblLine05);
            this.panel1.Controls.Add(this.lblLine04);
            this.panel1.Controls.Add(this.lblLine03);
            this.panel1.Controls.Add(this.lblLine02);
            this.panel1.Controls.Add(this.lblLine01);
            this.panel1.Controls.Add(this.chkBoxTest);
            this.panel1.Controls.Add(this.cbxLang);
            this.panel1.Controls.Add(this.lblLang);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.cbxCountry);
            this.panel1.Controls.Add(this.lblArea);
            this.panel1.Controls.Add(this.lblPwd);
            this.panel1.Controls.Add(this.lblAcct);
            this.panel1.Controls.Add(this.lblFact);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.cbxVendor);
            this.panel1.Controls.Add(this.bntOK);
            this.panel1.Controls.Add(this.txtLogin);
            this.panel1.Controls.Add(this.txtPsw);
            this.panel1.Controls.Add(this.panelMask);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 366);
            this.panel1.TabIndex = 17;
            // 
            // picAlien
            // 
            this.picAlien.BackColor = System.Drawing.Color.Transparent;
            this.picAlien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAlien.Image = global::RFIDClient.Properties.Resources.AlienLogo;
            this.picAlien.Location = new System.Drawing.Point(0, 7);
            this.picAlien.Margin = new System.Windows.Forms.Padding(0);
            this.picAlien.Name = "picAlien";
            this.picAlien.Size = new System.Drawing.Size(34, 36);
            this.picAlien.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAlien.TabIndex = 43;
            this.picAlien.TabStop = false;
            this.picAlien.Click += new System.EventHandler(this.picAlien_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMsg.Location = new System.Drawing.Point(116, 337);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(166, 22);
            this.lblMsg.TabIndex = 19;
            this.lblMsg.Text = "Checking the SSO";
            // 
            // lblFinger
            // 
            this.lblFinger.AutoEllipsis = true;
            this.lblFinger.BackColor = System.Drawing.Color.Transparent;
            this.lblFinger.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFinger.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblFinger.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinger.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblFinger.Location = new System.Drawing.Point(0, 317);
            this.lblFinger.Margin = new System.Windows.Forms.Padding(0);
            this.lblFinger.Name = "lblFinger";
            this.lblFinger.Size = new System.Drawing.Size(383, 47);
            this.lblFinger.TabIndex = 41;
            this.lblFinger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLine00
            // 
            this.lblLine00.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine00.ForeColor = System.Drawing.Color.Black;
            this.lblLine00.Location = new System.Drawing.Point(41, 7);
            this.lblLine00.Name = "lblLine00";
            this.lblLine00.Size = new System.Drawing.Size(300, 1);
            this.lblLine00.TabIndex = 39;
            this.lblLine00.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVericalR
            // 
            this.lblVericalR.BackColor = System.Drawing.Color.White;
            this.lblVericalR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVericalR.Location = new System.Drawing.Point(340, 7);
            this.lblVericalR.Name = "lblVericalR";
            this.lblVericalR.Size = new System.Drawing.Size(1, 141);
            this.lblVericalR.TabIndex = 38;
            // 
            // lblVericalL
            // 
            this.lblVericalL.BackColor = System.Drawing.Color.White;
            this.lblVericalL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVericalL.Location = new System.Drawing.Point(42, 7);
            this.lblVericalL.Name = "lblVericalL";
            this.lblVericalL.Size = new System.Drawing.Size(1, 141);
            this.lblVericalL.TabIndex = 37;
            // 
            // lblLine05
            // 
            this.lblLine05.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine05.Location = new System.Drawing.Point(42, 241);
            this.lblLine05.Name = "lblLine05";
            this.lblLine05.Size = new System.Drawing.Size(300, 1);
            this.lblLine05.TabIndex = 36;
            this.lblLine05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLine05.Visible = false;
            // 
            // lblLine04
            // 
            this.lblLine04.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine04.Location = new System.Drawing.Point(42, 193);
            this.lblLine04.Name = "lblLine04";
            this.lblLine04.Size = new System.Drawing.Size(300, 1);
            this.lblLine04.TabIndex = 35;
            this.lblLine04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLine04.Visible = false;
            // 
            // lblLine03
            // 
            this.lblLine03.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine03.Location = new System.Drawing.Point(42, 146);
            this.lblLine03.Name = "lblLine03";
            this.lblLine03.Size = new System.Drawing.Size(300, 1);
            this.lblLine03.TabIndex = 34;
            this.lblLine03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLine02
            // 
            this.lblLine02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine02.Location = new System.Drawing.Point(42, 100);
            this.lblLine02.Name = "lblLine02";
            this.lblLine02.Size = new System.Drawing.Size(300, 1);
            this.lblLine02.TabIndex = 33;
            this.lblLine02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLine01
            // 
            this.lblLine01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine01.ForeColor = System.Drawing.Color.Black;
            this.lblLine01.Location = new System.Drawing.Point(42, 52);
            this.lblLine01.Name = "lblLine01";
            this.lblLine01.Size = new System.Drawing.Size(300, 1);
            this.lblLine01.TabIndex = 32;
            this.lblLine01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkBoxTest
            // 
            this.chkBoxTest.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBoxTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkBoxTest.ForeColor = System.Drawing.Color.Black;
            this.chkBoxTest.Location = new System.Drawing.Point(-1, -1);
            this.chkBoxTest.Margin = new System.Windows.Forms.Padding(0);
            this.chkBoxTest.Name = "chkBoxTest";
            this.chkBoxTest.Size = new System.Drawing.Size(6, 7);
            this.chkBoxTest.TabIndex = 31;
            this.chkBoxTest.UseVisualStyleBackColor = true;
            this.chkBoxTest.CheckedChanged += new System.EventHandler(this.chkBoxTest_CheckedChanged);
            // 
            // cbxLang
            // 
            this.cbxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLang.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxLang.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxLang.ForeColor = System.Drawing.Color.Navy;
            this.cbxLang.FormattingEnabled = true;
            this.cbxLang.Location = new System.Drawing.Point(191, 16);
            this.cbxLang.Name = "cbxLang";
            this.cbxLang.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbxLang.Size = new System.Drawing.Size(140, 24);
            this.cbxLang.TabIndex = 30;
            this.cbxLang.SelectedIndexChanged += new System.EventHandler(this.cbxLang_SelectedIndexChanged);
            // 
            // lblLang
            // 
            this.lblLang.AutoSize = true;
            this.lblLang.BackColor = System.Drawing.Color.Transparent;
            this.lblLang.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblLang.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLang.ForeColor = System.Drawing.Color.Navy;
            this.lblLang.Location = new System.Drawing.Point(49, 15);
            this.lblLang.Name = "lblLang";
            this.lblLang.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblLang.Size = new System.Drawing.Size(102, 24);
            this.lblLang.TabIndex = 29;
            this.lblLang.Text = "Language";
            this.lblLang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::RFIDClient.Properties.Resources.country;
            this.pictureBox4.InitialImage = null;
            this.pictureBox4.Location = new System.Drawing.Point(151, 59);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(34, 36);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 28;
            this.pictureBox4.TabStop = false;
            // 
            // cbxCountry
            // 
            this.cbxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCountry.FormattingEnabled = true;
            this.cbxCountry.Location = new System.Drawing.Point(191, 66);
            this.cbxCountry.Name = "cbxCountry";
            this.cbxCountry.Size = new System.Drawing.Size(140, 21);
            this.cbxCountry.TabIndex = 27;
            this.cbxCountry.SelectedIndexChanged += new System.EventHandler(this.cbxCountry_SelectedIndexChanged);
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.BackColor = System.Drawing.Color.Transparent;
            this.lblArea.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArea.Location = new System.Drawing.Point(44, 62);
            this.lblArea.Name = "lblArea";
            this.lblArea.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblArea.Size = new System.Drawing.Size(76, 24);
            this.lblArea.TabIndex = 26;
            this.lblArea.Text = "Region";
            this.lblArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.BackColor = System.Drawing.Color.Transparent;
            this.lblPwd.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.Location = new System.Drawing.Point(44, 206);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPwd.Size = new System.Drawing.Size(103, 24);
            this.lblPwd.TabIndex = 25;
            this.lblPwd.Text = "Password";
            this.lblPwd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPwd.Visible = false;
            // 
            // lblAcct
            // 
            this.lblAcct.AutoSize = true;
            this.lblAcct.BackColor = System.Drawing.Color.Transparent;
            this.lblAcct.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcct.Location = new System.Drawing.Point(44, 158);
            this.lblAcct.Name = "lblAcct";
            this.lblAcct.Size = new System.Drawing.Size(85, 24);
            this.lblAcct.TabIndex = 24;
            this.lblAcct.Text = "Account";
            this.lblAcct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAcct.Visible = false;
            // 
            // lblFact
            // 
            this.lblFact.AutoSize = true;
            this.lblFact.BackColor = System.Drawing.Color.Transparent;
            this.lblFact.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFact.Location = new System.Drawing.Point(44, 108);
            this.lblFact.Name = "lblFact";
            this.lblFact.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFact.Size = new System.Drawing.Size(82, 24);
            this.lblFact.TabIndex = 23;
            this.lblFact.Text = "Factory";
            this.lblFact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::RFIDClient.Properties.Resources.key;
            this.pictureBox3.Location = new System.Drawing.Point(150, 199);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(34, 36);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 22;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::RFIDClient.Properties.Resources.user;
            this.pictureBox2.Location = new System.Drawing.Point(150, 152);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "";
            this.pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::RFIDClient.Properties.Resources.fact;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(150, 105);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // cbxVendor
            // 
            this.cbxVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVendor.FormattingEnabled = true;
            this.cbxVendor.Location = new System.Drawing.Point(191, 113);
            this.cbxVendor.Name = "cbxVendor";
            this.cbxVendor.Size = new System.Drawing.Size(140, 21);
            this.cbxVendor.TabIndex = 18;
            this.cbxVendor.SelectedIndexChanged += new System.EventHandler(this.cbxVendor_SelectedIndexChanged);
            // 
            // txtLogin
            // 
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtLogin.Location = new System.Drawing.Point(191, 158);
            this.txtLogin.MaxLength = 60;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(140, 21);
            this.txtLogin.TabIndex = 0;
            this.txtLogin.Visible = false;
            // 
            // txtPsw
            // 
            this.txtPsw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPsw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPsw.Location = new System.Drawing.Point(191, 206);
            this.txtPsw.MaxLength = 30;
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.PasswordChar = '*';
            this.txtPsw.Size = new System.Drawing.Size(140, 21);
            this.txtPsw.TabIndex = 13;
            this.txtPsw.Visible = false;
            // 
            // panelMask
            // 
            this.panelMask.BackColor = System.Drawing.Color.Transparent;
            this.panelMask.Controls.Add(this.picFinger);
            this.panelMask.Location = new System.Drawing.Point(42, 148);
            this.panelMask.Margin = new System.Windows.Forms.Padding(0);
            this.panelMask.Name = "panelMask";
            this.panelMask.Padding = new System.Windows.Forms.Padding(72, 0, 72, 0);
            this.panelMask.Size = new System.Drawing.Size(299, 167);
            this.panelMask.TabIndex = 42;
            // 
            // picFinger
            // 
            this.picFinger.BackColor = System.Drawing.Color.Transparent;
            this.picFinger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picFinger.ErrorImage = null;
            this.picFinger.Location = new System.Drawing.Point(72, 0);
            this.picFinger.Margin = new System.Windows.Forms.Padding(0);
            this.picFinger.Name = "picFinger";
            this.picFinger.Size = new System.Drawing.Size(155, 167);
            this.picFinger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picFinger.TabIndex = 40;
            this.picFinger.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.bntOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(385, 366);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.webBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLogin_FormClosed);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TextBox txtPsw;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Button bntOK;
        private System.Windows.Forms.ComboBox cbxVendor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblAcct;
        private System.Windows.Forms.Label lblFact;
        private System.Windows.Forms.ComboBox cbxCountry;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ComboBox cbxLang;
        private System.Windows.Forms.Label lblLang;
        internal System.Windows.Forms.CheckBox chkBoxTest;
        private System.Windows.Forms.Label lblLine01;
        private System.Windows.Forms.Label lblLine05;
        private System.Windows.Forms.Label lblLine04;
        private System.Windows.Forms.Label lblLine03;
        private System.Windows.Forms.Label lblLine02;
        private System.Windows.Forms.Label lblLine00;
        private System.Windows.Forms.Label lblVericalR;
        private System.Windows.Forms.Label lblVericalL;
        private System.Windows.Forms.PictureBox picFinger;
        private System.Windows.Forms.Label lblFinger;
        private System.Windows.Forms.Panel panelMask;
        private System.Windows.Forms.PictureBox picAlien;


    }
}