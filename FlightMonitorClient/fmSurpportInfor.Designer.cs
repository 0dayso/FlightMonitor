namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmSurpportInfor
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSetUp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPreView = new System.Windows.Forms.Button();
            this.wbSurpportInfor = new System.Windows.Forms.WebBrowser();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(680, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(616, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(56, 23);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.wbSurpportInfor);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(744, 410);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // btnSetUp
            // 
            this.btnSetUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetUp.Location = new System.Drawing.Point(488, 16);
            this.btnSetUp.Name = "btnSetUp";
            this.btnSetUp.Size = new System.Drawing.Size(56, 23);
            this.btnSetUp.TabIndex = 12;
            this.btnSetUp.Text = "设 置";
            this.btnSetUp.Click += new System.EventHandler(this.btnSetUp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPreView);
            this.groupBox2.Controls.Add(this.btnSetUp);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 410);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(744, 48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // btnPreView
            // 
            this.btnPreView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreView.Location = new System.Drawing.Point(552, 16);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(56, 23);
            this.btnPreView.TabIndex = 13;
            this.btnPreView.Text = "预 览";
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
            // 
            // wbSurpportInfor
            // 
            this.wbSurpportInfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbSurpportInfor.Location = new System.Drawing.Point(3, 17);
            this.wbSurpportInfor.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbSurpportInfor.Name = "wbSurpportInfor";
            this.wbSurpportInfor.Size = new System.Drawing.Size(738, 390);
            this.wbSurpportInfor.TabIndex = 0;
            // 
            // fmSurpportInfor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 458);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.MinimizeBox = false;
            this.Name = "fmSurpportInfor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "支持信息";
            this.Load += new System.EventHandler(this.fmSurpportInfor_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSetUp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPreView;
        private System.Windows.Forms.WebBrowser wbSurpportInfor;
    }
}