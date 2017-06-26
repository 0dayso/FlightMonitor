namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmMaintenTOFF
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
            this.rdATD = new System.Windows.Forms.RadioButton();
            this.rdETD = new System.Windows.Forms.RadioButton();
            this.btnETDNow = new System.Windows.Forms.Button();
            this.txtETD = new System.Windows.Forms.TextBox();
            this.txtATD = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnATDNow = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdATD
            // 
            this.rdATD.Location = new System.Drawing.Point(9, 44);
            this.rdATD.Name = "rdATD";
            this.rdATD.Size = new System.Drawing.Size(72, 24);
            this.rdATD.TabIndex = 15;
            this.rdATD.Text = "实际起飞";
            this.rdATD.CheckedChanged += new System.EventHandler(this.rdATD_CheckedChanged);
            // 
            // rdETD
            // 
            this.rdETD.Location = new System.Drawing.Point(9, 12);
            this.rdETD.Name = "rdETD";
            this.rdETD.Size = new System.Drawing.Size(72, 24);
            this.rdETD.TabIndex = 14;
            this.rdETD.Text = "预计起飞";
            this.rdETD.CheckedChanged += new System.EventHandler(this.rdETD_CheckedChanged);
            // 
            // btnETDNow
            // 
            this.btnETDNow.Location = new System.Drawing.Point(209, 15);
            this.btnETDNow.Name = "btnETDNow";
            this.btnETDNow.Size = new System.Drawing.Size(32, 23);
            this.btnETDNow.TabIndex = 13;
            this.btnETDNow.Text = "Now";
            this.btnETDNow.Click += new System.EventHandler(this.btnETDNow_Click);
            // 
            // txtETD
            // 
            this.txtETD.Location = new System.Drawing.Point(81, 15);
            this.txtETD.MaxLength = 4;
            this.txtETD.Name = "txtETD";
            this.txtETD.Size = new System.Drawing.Size(125, 21);
            this.txtETD.TabIndex = 12;
            // 
            // txtATD
            // 
            this.txtATD.Location = new System.Drawing.Point(81, 47);
            this.txtATD.MaxLength = 4;
            this.txtATD.Name = "txtATD";
            this.txtATD.Size = new System.Drawing.Size(125, 21);
            this.txtATD.TabIndex = 16;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(192, 85);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(128, 85);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保 存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnATDNow
            // 
            this.btnATDNow.Location = new System.Drawing.Point(209, 45);
            this.btnATDNow.Name = "btnATDNow";
            this.btnATDNow.Size = new System.Drawing.Size(32, 23);
            this.btnATDNow.TabIndex = 17;
            this.btnATDNow.Text = "Now";
            this.btnATDNow.Click += new System.EventHandler(this.btnATDNow_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnATDNow);
            this.groupBox1.Controls.Add(this.txtATD);
            this.groupBox1.Controls.Add(this.rdATD);
            this.groupBox1.Controls.Add(this.rdETD);
            this.groupBox1.Controls.Add(this.btnETDNow);
            this.groupBox1.Controls.Add(this.txtETD);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 80);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // fmMaintenTOFF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 109);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Name = "fmMaintenTOFF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预计/实际起飞";
            this.Activated += new System.EventHandler(this.fmMaintenTOFF_Activated);
            this.Load += new System.EventHandler(this.fmMaintenTOFF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdATD;
        private System.Windows.Forms.RadioButton rdETD;
        private System.Windows.Forms.Button btnETDNow;
        private System.Windows.Forms.TextBox txtETD;
        private System.Windows.Forms.TextBox txtATD;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnATDNow;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}