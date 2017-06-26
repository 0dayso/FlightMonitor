namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmMaintenTDWN
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
            this.rdATA = new System.Windows.Forms.RadioButton();
            this.rdETA = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnATANOW = new System.Windows.Forms.Button();
            this.txtATA = new System.Windows.Forms.TextBox();
            this.btnETANow = new System.Windows.Forms.Button();
            this.txtETA = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdATA
            // 
            this.rdATA.Location = new System.Drawing.Point(9, 44);
            this.rdATA.Name = "rdATA";
            this.rdATA.Size = new System.Drawing.Size(72, 24);
            this.rdATA.TabIndex = 4;
            this.rdATA.Text = "实际到达";
            this.rdATA.CheckedChanged += new System.EventHandler(this.rdATA_CheckedChanged);
            // 
            // rdETA
            // 
            this.rdETA.Location = new System.Drawing.Point(9, 12);
            this.rdETA.Name = "rdETA";
            this.rdETA.Size = new System.Drawing.Size(72, 24);
            this.rdETA.TabIndex = 1;
            this.rdETA.Text = "预计到达";
            this.rdETA.CheckedChanged += new System.EventHandler(this.rdETA_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnATANOW);
            this.groupBox1.Controls.Add(this.txtATA);
            this.groupBox1.Controls.Add(this.rdATA);
            this.groupBox1.Controls.Add(this.rdETA);
            this.groupBox1.Controls.Add(this.btnETANow);
            this.groupBox1.Controls.Add(this.txtETA);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 80);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // btnATANOW
            // 
            this.btnATANOW.Location = new System.Drawing.Point(209, 45);
            this.btnATANOW.Name = "btnATANOW";
            this.btnATANOW.Size = new System.Drawing.Size(32, 23);
            this.btnATANOW.TabIndex = 6;
            this.btnATANOW.Text = "Now";
            this.btnATANOW.Click += new System.EventHandler(this.btnATANOW_Click);
            // 
            // txtATA
            // 
            this.txtATA.Location = new System.Drawing.Point(81, 47);
            this.txtATA.MaxLength = 4;
            this.txtATA.Name = "txtATA";
            this.txtATA.Size = new System.Drawing.Size(125, 21);
            this.txtATA.TabIndex = 5;
            // 
            // btnETANow
            // 
            this.btnETANow.Location = new System.Drawing.Point(209, 15);
            this.btnETANow.Name = "btnETANow";
            this.btnETANow.Size = new System.Drawing.Size(32, 23);
            this.btnETANow.TabIndex = 3;
            this.btnETANow.Text = "Now";
            this.btnETANow.Click += new System.EventHandler(this.btnETANow_Click);
            // 
            // txtETA
            // 
            this.txtETA.Location = new System.Drawing.Point(81, 15);
            this.txtETA.MaxLength = 4;
            this.txtETA.Name = "txtETA";
            this.txtETA.Size = new System.Drawing.Size(125, 21);
            this.txtETA.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(192, 85);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(128, 85);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保 存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fmMaintenTDWN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 109);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmMaintenTDWN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预计/实际到达";
            this.Activated += new System.EventHandler(this.fmMaintenTDWN_Activated);
            this.Load += new System.EventHandler(this.fmMaintenTDWN_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdATA;
        private System.Windows.Forms.RadioButton rdETA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnATANOW;
        private System.Windows.Forms.TextBox txtATA;
        private System.Windows.Forms.Button btnETANow;
        private System.Windows.Forms.TextBox txtETA;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}