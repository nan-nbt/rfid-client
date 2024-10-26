namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_Connect = new Button();
            btn_ReadOne = new Button();
            txt_Message = new TextBox();
            btn_Write = new Button();
            txt_mask = new TextBox();
            btn_Mask = new Button();
            btn_ReadMulti = new Button();
            cb_Port = new ComboBox();
            btn_ClearMessage = new Button();
            label1 = new Label();
            cb_Power = new ComboBox();
            label2 = new Label();
            btn_SetPower = new Button();
            txt_write = new TextBox();
            btn_unMask = new Button();
            SuspendLayout();
            // 
            // btn_Connect
            // 
            btn_Connect.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Connect.Location = new Point(26, 31);
            btn_Connect.Name = "btn_Connect";
            btn_Connect.Size = new Size(110, 40);
            btn_Connect.TabIndex = 0;
            btn_Connect.Text = "連線";
            btn_Connect.UseVisualStyleBackColor = true;
            btn_Connect.Click += btn_Connect_Click;
            // 
            // btn_ReadOne
            // 
            btn_ReadOne.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_ReadOne.Location = new Point(26, 116);
            btn_ReadOne.Name = "btn_ReadOne";
            btn_ReadOne.Size = new Size(110, 40);
            btn_ReadOne.TabIndex = 2;
            btn_ReadOne.Text = "單筆讀取";
            btn_ReadOne.UseVisualStyleBackColor = true;
            btn_ReadOne.Click += btn_ReadOne_Click;
            // 
            // txt_Message
            // 
            txt_Message.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Message.Location = new Point(159, 116);
            txt_Message.Multiline = true;
            txt_Message.Name = "txt_Message";
            txt_Message.ScrollBars = ScrollBars.Vertical;
            txt_Message.Size = new Size(889, 251);
            txt_Message.TabIndex = 3;
            // 
            // btn_Write
            // 
            btn_Write.BackColor = Color.FromArgb(0, 192, 192);
            btn_Write.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Write.Location = new Point(904, 31);
            btn_Write.Name = "btn_Write";
            btn_Write.Size = new Size(150, 40);
            btn_Write.TabIndex = 4;
            btn_Write.Text = "寫碼";
            btn_Write.UseVisualStyleBackColor = false;
            btn_Write.Click += btn_Write_Click;
            // 
            // txt_mask
            // 
            txt_mask.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_mask.Location = new Point(384, 77);
            txt_mask.Name = "txt_mask";
            txt_mask.Size = new Size(322, 33);
            txt_mask.TabIndex = 5;
            // 
            // btn_Mask
            // 
            btn_Mask.BackColor = Color.FromArgb(0, 192, 192);
            btn_Mask.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Mask.Location = new Point(384, 31);
            btn_Mask.Name = "btn_Mask";
            btn_Mask.Size = new Size(150, 40);
            btn_Mask.TabIndex = 6;
            btn_Mask.Text = "設定 Mask";
            btn_Mask.UseVisualStyleBackColor = false;
            btn_Mask.Click += btn_Mask_Click;
            // 
            // btn_ReadMulti
            // 
            btn_ReadMulti.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_ReadMulti.Location = new Point(26, 162);
            btn_ReadMulti.Name = "btn_ReadMulti";
            btn_ReadMulti.Size = new Size(110, 40);
            btn_ReadMulti.TabIndex = 7;
            btn_ReadMulti.Text = "多筆讀取";
            btn_ReadMulti.UseVisualStyleBackColor = true;
            btn_ReadMulti.Click += btn_ReadMulti_Click;
            // 
            // cb_Port
            // 
            cb_Port.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cb_Port.FormattingEnabled = true;
            cb_Port.Location = new Point(159, 31);
            cb_Port.Name = "cb_Port";
            cb_Port.Size = new Size(160, 33);
            cb_Port.TabIndex = 9;
            // 
            // btn_ClearMessage
            // 
            btn_ClearMessage.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_ClearMessage.Location = new Point(910, 377);
            btn_ClearMessage.Name = "btn_ClearMessage";
            btn_ClearMessage.Size = new Size(138, 40);
            btn_ClearMessage.TabIndex = 10;
            btn_ClearMessage.Text = "清除記錄";
            btn_ClearMessage.UseVisualStyleBackColor = true;
            btn_ClearMessage.Click += btn_ClearMessage_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(159, 386);
            label1.Name = "label1";
            label1.Size = new Size(143, 25);
            label1.TabIndex = 11;
            label1.Text = "Output Power";
            // 
            // cb_Power
            // 
            cb_Power.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            cb_Power.ForeColor = Color.Red;
            cb_Power.FormattingEnabled = true;
            cb_Power.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" });
            cb_Power.Location = new Point(308, 380);
            cb_Power.Name = "cb_Power";
            cb_Power.Size = new Size(132, 37);
            cb_Power.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(446, 388);
            label2.Name = "label2";
            label2.Size = new Size(56, 25);
            label2.TabIndex = 13;
            label2.Text = "dBm";
            // 
            // btn_SetPower
            // 
            btn_SetPower.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_SetPower.Location = new Point(508, 378);
            btn_SetPower.Name = "btn_SetPower";
            btn_SetPower.Size = new Size(118, 40);
            btn_SetPower.TabIndex = 14;
            btn_SetPower.Text = "設定";
            btn_SetPower.UseVisualStyleBackColor = true;
            btn_SetPower.Click += btn_SetPower_Click;
            // 
            // txt_write
            // 
            txt_write.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txt_write.Location = new Point(726, 77);
            txt_write.Name = "txt_write";
            txt_write.Size = new Size(322, 33);
            txt_write.TabIndex = 15;
            // 
            // btn_unMask
            // 
            btn_unMask.BackColor = Color.FromArgb(0, 192, 192);
            btn_unMask.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_unMask.Location = new Point(556, 31);
            btn_unMask.Name = "btn_unMask";
            btn_unMask.Size = new Size(150, 40);
            btn_unMask.TabIndex = 16;
            btn_unMask.Text = "解除 Mask";
            btn_unMask.UseVisualStyleBackColor = false;
            btn_unMask.Click += btn_unMask_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 442);
            Controls.Add(btn_unMask);
            Controls.Add(txt_write);
            Controls.Add(btn_SetPower);
            Controls.Add(label2);
            Controls.Add(cb_Power);
            Controls.Add(label1);
            Controls.Add(btn_ClearMessage);
            Controls.Add(cb_Port);
            Controls.Add(btn_ReadMulti);
            Controls.Add(btn_Mask);
            Controls.Add(txt_mask);
            Controls.Add(btn_Write);
            Controls.Add(txt_Message);
            Controls.Add(btn_ReadOne);
            Controls.Add(btn_Connect);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Use DLL Demo 20240315 v1.3";
            FormClosed += Form1_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_Connect;
        private Button btn_ReadOne;
        private TextBox txt_Message;
        private Button btn_Write;
        private TextBox txt_mask;
        private Button btn_Mask;
        private Button btn_ReadMulti;
        private ComboBox cb_Port;
        private Button btn_ClearMessage;
        private Label label1;
        private ComboBox cb_Power;
        private Label label2;
        private Button btn_SetPower;
        private TextBox txt_write;
        private Button btn_unMask;
    }
}