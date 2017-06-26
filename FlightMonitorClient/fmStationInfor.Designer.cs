namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmStationInfor
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtThreeCode = new System.Windows.Forms.TextBox();
            this.lblAirportIATACode = new System.Windows.Forms.Label();
            this.txtStationName = new System.Windows.Forms.TextBox();
            this.lblStationName = new System.Windows.Forms.Label();
            this.txtCommanderOfficeName = new System.Windows.Forms.TextBox();
            this.txtAirportName = new System.Windows.Forms.TextBox();
            this.lblCommanderOfficeName = new System.Windows.Forms.Label();
            this.lblAirportName = new System.Windows.Forms.Label();
            this.txtStationSignInFlag = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDayLine = new System.Windows.Forms.TextBox();
            this.lblDayLine = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDelayTimeLine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJoinTimeLine = new System.Windows.Forms.TextBox();
            this.txtDisconnectTimeLine = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSumbit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSumbit);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 41);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDisconnectTimeLine);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDelayTimeLine);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtJoinTimeLine);
            this.groupBox2.Controls.Add(this.txtStationSignInFlag);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDayLine);
            this.groupBox2.Controls.Add(this.lblDayLine);
            this.groupBox2.Controls.Add(this.txtCommanderOfficeName);
            this.groupBox2.Controls.Add(this.txtAirportName);
            this.groupBox2.Controls.Add(this.lblCommanderOfficeName);
            this.groupBox2.Controls.Add(this.lblAirportName);
            this.groupBox2.Controls.Add(this.txtStationName);
            this.groupBox2.Controls.Add(this.lblStationName);
            this.groupBox2.Controls.Add(this.txtThreeCode);
            this.groupBox2.Controls.Add(this.lblAirportIATACode);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(529, 169);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtThreeCode
            // 
            this.txtThreeCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtThreeCode.Location = new System.Drawing.Point(134, 16);
            this.txtThreeCode.MaxLength = 3;
            this.txtThreeCode.Name = "txtThreeCode";
            this.txtThreeCode.Size = new System.Drawing.Size(125, 21);
            this.txtThreeCode.TabIndex = 0;
            // 
            // lblAirportIATACode
            // 
            this.lblAirportIATACode.AutoSize = true;
            this.lblAirportIATACode.Location = new System.Drawing.Point(8, 20);
            this.lblAirportIATACode.Name = "lblAirportIATACode";
            this.lblAirportIATACode.Size = new System.Drawing.Size(65, 12);
            this.lblAirportIATACode.TabIndex = 34;
            this.lblAirportIATACode.Text = "机场三字码";
            // 
            // txtStationName
            // 
            this.txtStationName.Location = new System.Drawing.Point(393, 20);
            this.txtStationName.MaxLength = 10;
            this.txtStationName.Name = "txtStationName";
            this.txtStationName.Size = new System.Drawing.Size(125, 21);
            this.txtStationName.TabIndex = 37;
            // 
            // lblStationName
            // 
            this.lblStationName.AutoSize = true;
            this.lblStationName.Location = new System.Drawing.Point(273, 20);
            this.lblStationName.Name = "lblStationName";
            this.lblStationName.Size = new System.Drawing.Size(53, 12);
            this.lblStationName.TabIndex = 36;
            this.lblStationName.Text = "场站名称";
            // 
            // txtCommanderOfficeName
            // 
            this.txtCommanderOfficeName.Location = new System.Drawing.Point(393, 47);
            this.txtCommanderOfficeName.MaxLength = 20;
            this.txtCommanderOfficeName.Name = "txtCommanderOfficeName";
            this.txtCommanderOfficeName.Size = new System.Drawing.Size(125, 21);
            this.txtCommanderOfficeName.TabIndex = 41;
            // 
            // txtAirportName
            // 
            this.txtAirportName.Location = new System.Drawing.Point(134, 45);
            this.txtAirportName.MaxLength = 20;
            this.txtAirportName.Name = "txtAirportName";
            this.txtAirportName.Size = new System.Drawing.Size(125, 21);
            this.txtAirportName.TabIndex = 40;
            // 
            // lblCommanderOfficeName
            // 
            this.lblCommanderOfficeName.AutoSize = true;
            this.lblCommanderOfficeName.Location = new System.Drawing.Point(273, 49);
            this.lblCommanderOfficeName.Name = "lblCommanderOfficeName";
            this.lblCommanderOfficeName.Size = new System.Drawing.Size(89, 12);
            this.lblCommanderOfficeName.TabIndex = 39;
            this.lblCommanderOfficeName.Text = "现场指挥室名称";
            // 
            // lblAirportName
            // 
            this.lblAirportName.AutoSize = true;
            this.lblAirportName.Location = new System.Drawing.Point(8, 49);
            this.lblAirportName.Name = "lblAirportName";
            this.lblAirportName.Size = new System.Drawing.Size(53, 12);
            this.lblAirportName.TabIndex = 38;
            this.lblAirportName.Text = "机场名称";
            // 
            // txtStationSignInFlag
            // 
            this.txtStationSignInFlag.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStationSignInFlag.Location = new System.Drawing.Point(134, 74);
            this.txtStationSignInFlag.Name = "txtStationSignInFlag";
            this.txtStationSignInFlag.Size = new System.Drawing.Size(125, 21);
            this.txtStationSignInFlag.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 48;
            this.label3.Text = "场站签到标识";
            // 
            // txtDayLine
            // 
            this.txtDayLine.Location = new System.Drawing.Point(393, 76);
            this.txtDayLine.MaxLength = 4;
            this.txtDayLine.Name = "txtDayLine";
            this.txtDayLine.Size = new System.Drawing.Size(125, 21);
            this.txtDayLine.TabIndex = 49;
            // 
            // lblDayLine
            // 
            this.lblDayLine.AutoSize = true;
            this.lblDayLine.Location = new System.Drawing.Point(273, 78);
            this.lblDayLine.Name = "lblDayLine";
            this.lblDayLine.Size = new System.Drawing.Size(101, 12);
            this.lblDayLine.TabIndex = 46;
            this.lblDayLine.Text = "航班日期分割时刻";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 53;
            this.label2.Text = "连飞时间限制(分)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDelayTimeLine
            // 
            this.txtDelayTimeLine.Location = new System.Drawing.Point(134, 103);
            this.txtDelayTimeLine.MaxLength = 4;
            this.txtDelayTimeLine.Name = "txtDelayTimeLine";
            this.txtDelayTimeLine.Size = new System.Drawing.Size(125, 21);
            this.txtDelayTimeLine.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 50;
            this.label1.Text = "延误时间限制(分)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtJoinTimeLine
            // 
            this.txtJoinTimeLine.Location = new System.Drawing.Point(393, 108);
            this.txtJoinTimeLine.MaxLength = 4;
            this.txtJoinTimeLine.Name = "txtJoinTimeLine";
            this.txtJoinTimeLine.Size = new System.Drawing.Size(125, 21);
            this.txtJoinTimeLine.TabIndex = 52;
            // 
            // txtDisconnectTimeLine
            // 
            this.txtDisconnectTimeLine.Location = new System.Drawing.Point(134, 132);
            this.txtDisconnectTimeLine.Name = "txtDisconnectTimeLine";
            this.txtDisconnectTimeLine.Size = new System.Drawing.Size(125, 21);
            this.txtDisconnectTimeLine.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 54;
            this.label4.Text = "网络故障提示时间(分)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSumbit
            // 
            this.btnSumbit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSumbit.Location = new System.Drawing.Point(396, 13);
            this.btnSumbit.Name = "btnSumbit";
            this.btnSumbit.Size = new System.Drawing.Size(60, 23);
            this.btnSumbit.TabIndex = 60;
            this.btnSumbit.Text = "保存";
            this.btnSumbit.Click += new System.EventHandler(this.btnSumbit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(461, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fmStationInfor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 210);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmStationInfor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "航站信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDisconnectTimeLine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDelayTimeLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJoinTimeLine;
        private System.Windows.Forms.TextBox txtStationSignInFlag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDayLine;
        private System.Windows.Forms.Label lblDayLine;
        private System.Windows.Forms.TextBox txtCommanderOfficeName;
        private System.Windows.Forms.TextBox txtAirportName;
        private System.Windows.Forms.Label lblCommanderOfficeName;
        private System.Windows.Forms.Label lblAirportName;
        private System.Windows.Forms.TextBox txtStationName;
        private System.Windows.Forms.Label lblStationName;
        private System.Windows.Forms.TextBox txtThreeCode;
        private System.Windows.Forms.Label lblAirportIATACode;
        private System.Windows.Forms.Button btnSumbit;
        private System.Windows.Forms.Button btnCancel;
    }
}