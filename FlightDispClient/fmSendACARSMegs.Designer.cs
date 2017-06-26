namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmSendACARSMegs
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxMegType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLongReg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFlightNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMegContent = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "报文类型";
            // 
            // cbxMegType
            // 
            this.cbxMegType.FormattingEnabled = true;
            this.cbxMegType.Items.AddRange(new object[] {
            "AOC Print",
            "Crew Message",
            "FreeText For Collins",
            "WX Report"});
            this.cbxMegType.Location = new System.Drawing.Point(116, 19);
            this.cbxMegType.Name = "cbxMegType";
            this.cbxMegType.Size = new System.Drawing.Size(121, 20);
            this.cbxMegType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "飞机号";
            // 
            // txtLongReg
            // 
            this.txtLongReg.Location = new System.Drawing.Point(116, 60);
            this.txtLongReg.Name = "txtLongReg";
            this.txtLongReg.Size = new System.Drawing.Size(100, 21);
            this.txtLongReg.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "航班号";
            // 
            // txtFlightNo
            // 
            this.txtFlightNo.Location = new System.Drawing.Point(116, 104);
            this.txtFlightNo.Name = "txtFlightNo";
            this.txtFlightNo.Size = new System.Drawing.Size(100, 21);
            this.txtFlightNo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "报文内容";
            // 
            // txtMegContent
            // 
            this.txtMegContent.Location = new System.Drawing.Point(116, 148);
            this.txtMegContent.Multiline = true;
            this.txtMegContent.Name = "txtMegContent";
            this.txtMegContent.Size = new System.Drawing.Size(200, 150);
            this.txtMegContent.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "发送报文";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fmSendACARSMegs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 373);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMegContent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFlightNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLongReg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxMegType);
            this.Controls.Add(this.label1);
            this.Name = "fmSendACARSMegs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发送ACARS报文";
            this.Load += new System.EventHandler(this.fmSendACARSMegs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxMegType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLongReg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFlightNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMegContent;
        private System.Windows.Forms.Button button1;
    }
}