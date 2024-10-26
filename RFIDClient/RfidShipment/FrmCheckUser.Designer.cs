namespace RFIDClient
{
    partial class FrmCheckUser
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
            this.lblScanId = new System.Windows.Forms.Label();
            this.txtUsrID = new System.Windows.Forms.TextBox();
            this.lblUsrName = new System.Windows.Forms.Label();
            this.txtUsrName = new System.Windows.Forms.TextBox();
            this.lblSsoUserNo = new System.Windows.Forms.Label();
            this.txtSsoUserNo = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblScanId
            // 
            this.lblScanId.AutoSize = true;
            this.lblScanId.BackColor = System.Drawing.Color.Transparent;
            this.lblScanId.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanId.Location = new System.Drawing.Point(12, 32);
            this.lblScanId.Name = "lblScanId";
            this.lblScanId.Size = new System.Drawing.Size(83, 22);
            this.lblScanId.TabIndex = 26;
            this.lblScanId.Text = "Scan ID:";
            this.lblScanId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsrID
            // 
            this.txtUsrID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsrID.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtUsrID.Location = new System.Drawing.Point(117, 34);
            this.txtUsrID.MaxLength = 60;
            this.txtUsrID.Name = "txtUsrID";
            this.txtUsrID.ShortcutsEnabled = false;
            this.txtUsrID.Size = new System.Drawing.Size(356, 22);
            this.txtUsrID.TabIndex = 25;
            this.txtUsrID.Click += new System.EventHandler(this.txtUsrID_Click);
            this.txtUsrID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsrID_KeyDown);
            // 
            // lblUsrName
            // 
            this.lblUsrName.AutoSize = true;
            this.lblUsrName.BackColor = System.Drawing.Color.Transparent;
            this.lblUsrName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsrName.Location = new System.Drawing.Point(12, 81);
            this.lblUsrName.Name = "lblUsrName";
            this.lblUsrName.Size = new System.Drawing.Size(65, 22);
            this.lblUsrName.TabIndex = 28;
            this.lblUsrName.Text = "Name:";
            this.lblUsrName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsrName
            // 
            this.txtUsrName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsrName.Enabled = false;
            this.txtUsrName.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtUsrName.Location = new System.Drawing.Point(117, 83);
            this.txtUsrName.MaxLength = 60;
            this.txtUsrName.Name = "txtUsrName";
            this.txtUsrName.Size = new System.Drawing.Size(356, 22);
            this.txtUsrName.TabIndex = 27;
            // 
            // lblSsoUserNo
            // 
            this.lblSsoUserNo.AutoSize = true;
            this.lblSsoUserNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSsoUserNo.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSsoUserNo.Location = new System.Drawing.Point(12, 135);
            this.lblSsoUserNo.Name = "lblSsoUserNo";
            this.lblSsoUserNo.Size = new System.Drawing.Size(84, 22);
            this.lblSsoUserNo.TabIndex = 30;
            this.lblSsoUserNo.Text = "Account:";
            this.lblSsoUserNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSsoUserNo
            // 
            this.txtSsoUserNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSsoUserNo.Enabled = false;
            this.txtSsoUserNo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSsoUserNo.Location = new System.Drawing.Point(117, 137);
            this.txtSsoUserNo.MaxLength = 60;
            this.txtSsoUserNo.Name = "txtSsoUserNo";
            this.txtSsoUserNo.Size = new System.Drawing.Size(356, 22);
            this.txtSsoUserNo.TabIndex = 29;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 195);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(93, 22);
            this.lblMessage.TabIndex = 32;
            this.lblMessage.Text = "Message:";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMessage.Location = new System.Drawing.Point(117, 197);
            this.txtMessage.MaxLength = 60;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(356, 38);
            this.txtMessage.TabIndex = 31;
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Arial", 14.25F);
            this.btnOk.Location = new System.Drawing.Point(117, 270);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 30);
            this.btnOk.TabIndex = 33;
            this.btnOk.Text = "Confirm";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Arial", 14.25F);
            this.btnReset.Location = new System.Drawing.Point(291, 270);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(99, 30);
            this.btnReset.TabIndex = 34;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FrmCheckUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 322);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lblSsoUserNo);
            this.Controls.Add(this.txtSsoUserNo);
            this.Controls.Add(this.lblUsrName);
            this.Controls.Add(this.txtUsrName);
            this.Controls.Add(this.lblScanId);
            this.Controls.Add(this.txtUsrID);
            this.Name = "FrmCheckUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check User";
            this.Load += new System.EventHandler(this.FrmCheckUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblScanId;
        private System.Windows.Forms.TextBox txtUsrID;
        private System.Windows.Forms.Label lblUsrName;
        private System.Windows.Forms.TextBox txtUsrName;
        private System.Windows.Forms.Label lblSsoUserNo;
        private System.Windows.Forms.TextBox txtSsoUserNo;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnReset;
    }
}