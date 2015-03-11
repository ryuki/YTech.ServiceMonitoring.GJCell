namespace YTech.SIS.GJCell.SMS.WinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRecipients = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.gbConnect = new System.Windows.Forms.GroupBox();
            this.btnRefreshPort = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cboTimeout = new System.Windows.Forms.ComboBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.gbSMS = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.gbConnect.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbSMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbSMS);
            this.groupBox1.Controls.Add(this.gbConnect);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 532);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtRecipients
            // 
            this.txtRecipients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecipients.Location = new System.Drawing.Point(3, 16);
            this.txtRecipients.Multiline = true;
            this.txtRecipients.Name = "txtRecipients";
            this.txtRecipients.Size = new System.Drawing.Size(235, 212);
            this.txtRecipients.TabIndex = 0;
            // 
            // txtMessage
            // 
            this.txtMessage.AcceptsReturn = true;
            this.txtMessage.AcceptsTab = true;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(3, 16);
            this.txtMessage.MaxLength = 160;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(294, 149);
            this.txtMessage.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(256, 193);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(294, 57);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send SMS";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // gbConnect
            // 
            this.gbConnect.Controls.Add(this.btnDisconnect);
            this.gbConnect.Controls.Add(this.txtOutput);
            this.gbConnect.Controls.Add(this.btnRefreshPort);
            this.gbConnect.Controls.Add(this.lblPort);
            this.gbConnect.Controls.Add(this.cboBaudRate);
            this.gbConnect.Controls.Add(this.cboPort);
            this.gbConnect.Controls.Add(this.btnOK);
            this.gbConnect.Controls.Add(this.cboTimeout);
            this.gbConnect.Controls.Add(this.lblTimeout);
            this.gbConnect.Controls.Add(this.lblBaudRate);
            this.gbConnect.Location = new System.Drawing.Point(6, 12);
            this.gbConnect.Name = "gbConnect";
            this.gbConnect.Size = new System.Drawing.Size(554, 241);
            this.gbConnect.TabIndex = 25;
            this.gbConnect.TabStop = false;
            this.gbConnect.Text = "Koneksi Modem";
            // 
            // btnRefreshPort
            // 
            this.btnRefreshPort.Location = new System.Drawing.Point(238, 16);
            this.btnRefreshPort.Name = "btnRefreshPort";
            this.btnRefreshPort.Size = new System.Drawing.Size(105, 23);
            this.btnRefreshPort.TabIndex = 23;
            this.btnRefreshPort.Text = "Refresh Port";
            this.btnRefreshPort.UseVisualStyleBackColor = true;
            this.btnRefreshPort.Click += new System.EventHandler(this.btnRefreshPort_Click);
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(16, 16);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(88, 23);
            this.lblPort.TabIndex = 15;
            this.lblPort.Text = "&COM-Port:";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.Location = new System.Drawing.Point(128, 40);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(104, 21);
            this.cboBaudRate.TabIndex = 18;
            // 
            // cboPort
            // 
            this.cboPort.Location = new System.Drawing.Point(128, 16);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(104, 21);
            this.cboPort.TabIndex = 16;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(128, 91);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "Connect";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboTimeout
            // 
            this.cboTimeout.Location = new System.Drawing.Point(128, 64);
            this.cboTimeout.Name = "cboTimeout";
            this.cboTimeout.Size = new System.Drawing.Size(104, 21);
            this.cboTimeout.TabIndex = 20;
            this.cboTimeout.Visible = false;
            // 
            // lblTimeout
            // 
            this.lblTimeout.Location = new System.Drawing.Point(16, 64);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(100, 23);
            this.lblTimeout.TabIndex = 19;
            this.lblTimeout.Text = "Ti&meout (ms):";
            this.lblTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTimeout.Visible = false;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.Location = new System.Drawing.Point(16, 40);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(100, 23);
            this.lblBaudRate.TabIndex = 17;
            this.lblBaudRate.Text = "&Baud rate:";
            this.lblBaudRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRecipients);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 231);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Penerima";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMessage);
            this.groupBox3.Location = new System.Drawing.Point(253, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 168);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pesan (Maksimal 160 karakter)";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(6, 120);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(542, 115);
            this.txtOutput.TabIndex = 24;
            this.txtOutput.WordWrap = false;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(209, 91);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(103, 23);
            this.btnDisconnect.TabIndex = 24;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // gbSMS
            // 
            this.gbSMS.Controls.Add(this.groupBox2);
            this.gbSMS.Controls.Add(this.groupBox3);
            this.gbSMS.Controls.Add(this.btnSend);
            this.gbSMS.Enabled = false;
            this.gbSMS.Location = new System.Drawing.Point(6, 259);
            this.gbSMS.Name = "gbSMS";
            this.gbSMS.Size = new System.Drawing.Size(554, 263);
            this.gbSMS.TabIndex = 28;
            this.gbSMS.TabStop = false;
            this.gbSMS.Text = "SMS";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 532);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMS Broadcast";
            this.groupBox1.ResumeLayout(false);
            this.gbConnect.ResumeLayout(false);
            this.gbConnect.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbSMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtRecipients;
        private System.Windows.Forms.GroupBox gbConnect;
        private System.Windows.Forms.Button btnRefreshPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ComboBox cboBaudRate;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.GroupBox gbSMS;
    }
}

