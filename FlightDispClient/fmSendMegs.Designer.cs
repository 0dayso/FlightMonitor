namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmSendMegs
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
            this.btnSendFuelInfo = new System.Windows.Forms.Button();
            this.cbxAFTNAdress = new System.Windows.Forms.ComboBox();
            this.cbxSITAAdress = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.txtFuelInfo = new System.Windows.Forms.TextBox();
            this.btnSendMegs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnStorePlan = new System.Windows.Forms.Button();
            this.txtPlanContent = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTelMeg = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSendFuelInfo);
            this.groupBox1.Controls.Add(this.cbxAFTNAdress);
            this.groupBox1.Controls.Add(this.cbxSITAAdress);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMobileNo);
            this.groupBox1.Controls.Add(this.txtFuelInfo);
            this.groupBox1.Controls.Add(this.btnSendMegs);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 589);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 149);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送报文";
            // 
            // btnSendFuelInfo
            // 
            this.btnSendFuelInfo.Location = new System.Drawing.Point(122, 110);
            this.btnSendFuelInfo.Name = "btnSendFuelInfo";
            this.btnSendFuelInfo.Size = new System.Drawing.Size(96, 23);
            this.btnSendFuelInfo.TabIndex = 10;
            this.btnSendFuelInfo.Text = "发送油量短信";
            this.btnSendFuelInfo.UseVisualStyleBackColor = true;
            // 
            // cbxAFTNAdress
            // 
            this.cbxAFTNAdress.FormattingEnabled = true;
            this.cbxAFTNAdress.Location = new System.Drawing.Point(291, 83);
            this.cbxAFTNAdress.Name = "cbxAFTNAdress";
            this.cbxAFTNAdress.Size = new System.Drawing.Size(100, 20);
            this.cbxAFTNAdress.TabIndex = 9;
            // 
            // cbxSITAAdress
            // 
            this.cbxSITAAdress.FormattingEnabled = true;
            this.cbxSITAAdress.Location = new System.Drawing.Point(146, 83);
            this.cbxSITAAdress.Name = "cbxSITAAdress";
            this.cbxSITAAdress.Size = new System.Drawing.Size(100, 20);
            this.cbxSITAAdress.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "AFTN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "SITA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "选择发报源地址：";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Location = new System.Drawing.Point(88, 52);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(300, 21);
            this.txtMobileNo.TabIndex = 4;
            // 
            // txtFuelInfo
            // 
            this.txtFuelInfo.Location = new System.Drawing.Point(88, 25);
            this.txtFuelInfo.Name = "txtFuelInfo";
            this.txtFuelInfo.Size = new System.Drawing.Size(300, 21);
            this.txtFuelInfo.TabIndex = 3;
            // 
            // btnSendMegs
            // 
            this.btnSendMegs.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSendMegs.Location = new System.Drawing.Point(14, 110);
            this.btnSendMegs.Name = "btnSendMegs";
            this.btnSendMegs.Size = new System.Drawing.Size(75, 23);
            this.btnSendMegs.TabIndex = 2;
            this.btnSendMegs.Text = "发送报文";
            this.btnSendMegs.UseVisualStyleBackColor = true;
            this.btnSendMegs.Click += new System.EventHandler(this.btnSendMegs_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "手机号码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "油量短信";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.txtPlanContent);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(591, 741);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "计算机飞行计划";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnStorePlan);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(3, 675);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(585, 63);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // btnStorePlan
            // 
            this.btnStorePlan.Location = new System.Drawing.Point(490, 24);
            this.btnStorePlan.Name = "btnStorePlan";
            this.btnStorePlan.Size = new System.Drawing.Size(75, 23);
            this.btnStorePlan.TabIndex = 2;
            this.btnStorePlan.Text = "保存计划";
            this.btnStorePlan.UseVisualStyleBackColor = true;
            this.btnStorePlan.Click += new System.EventHandler(this.btnStorePlan_Click);
            // 
            // txtPlanContent
            // 
            this.txtPlanContent.Location = new System.Drawing.Point(3, 17);
            this.txtPlanContent.Multiline = true;
            this.txtPlanContent.Name = "txtPlanContent";
            this.txtPlanContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPlanContent.Size = new System.Drawing.Size(582, 658);
            this.txtPlanContent.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTelMeg);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(597, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 741);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FPL报";
            // 
            // txtTelMeg
            // 
            this.txtTelMeg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelMeg.Location = new System.Drawing.Point(3, 17);
            this.txtTelMeg.Multiline = true;
            this.txtTelMeg.Name = "txtTelMeg";
            this.txtTelMeg.Size = new System.Drawing.Size(413, 572);
            this.txtTelMeg.TabIndex = 0;
            // 
            // fmSendMegs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "fmSendMegs";
            this.Text = "发送电报";
            this.Load += new System.EventHandler(this.fmSendMegs_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.TextBox txtFuelInfo;
        private System.Windows.Forms.Button btnSendMegs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlanContent;
        private System.Windows.Forms.TextBox txtTelMeg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxAFTNAdress;
        private System.Windows.Forms.ComboBox cbxSITAAdress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSendFuelInfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnStorePlan;


    }
}