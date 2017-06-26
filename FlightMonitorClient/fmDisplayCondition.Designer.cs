namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmDisplayCondition
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
            this.rdbCondition = new System.Windows.Forms.RadioButton();
            this.rdbAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkClosePaxCabin = new System.Windows.Forms.CheckBox();
            this.chkTOFFFlight = new System.Windows.Forms.CheckBox();
            this.chkTDWNFlight = new System.Windows.Forms.CheckBox();
            this.chkIntermissionFlight = new System.Windows.Forms.CheckBox();
            this.chkDiversionFlight = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.updownDelayHour = new System.Windows.Forms.NumericUpDown();
            this.chkDelayFlight = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownDelayHour)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbCondition);
            this.groupBox1.Controls.Add(this.rdbAll);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 41);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示类型";
            // 
            // rdbCondition
            // 
            this.rdbCondition.AutoSize = true;
            this.rdbCondition.Location = new System.Drawing.Point(86, 15);
            this.rdbCondition.Name = "rdbCondition";
            this.rdbCondition.Size = new System.Drawing.Size(119, 16);
            this.rdbCondition.TabIndex = 1;
            this.rdbCondition.TabStop = true;
            this.rdbCondition.Text = "根据设置条件显示";
            this.rdbCondition.UseVisualStyleBackColor = true;
            // 
            // rdbAll
            // 
            this.rdbAll.AutoSize = true;
            this.rdbAll.Location = new System.Drawing.Point(6, 15);
            this.rdbAll.Name = "rdbAll";
            this.rdbAll.Size = new System.Drawing.Size(71, 16);
            this.rdbAll.TabIndex = 0;
            this.rdbAll.TabStop = true;
            this.rdbAll.Text = "全部显示";
            this.rdbAll.UseVisualStyleBackColor = true;
            this.rdbAll.CheckedChanged += new System.EventHandler(this.rdbAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 40);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(291, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(211, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkClosePaxCabin);
            this.groupBox3.Controls.Add(this.chkTOFFFlight);
            this.groupBox3.Controls.Add(this.chkTDWNFlight);
            this.groupBox3.Controls.Add(this.chkIntermissionFlight);
            this.groupBox3.Controls.Add(this.chkDiversionFlight);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.updownDelayHour);
            this.groupBox3.Controls.Add(this.chkDelayFlight);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(373, 111);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "显示条件";
            // 
            // chkClosePaxCabin
            // 
            this.chkClosePaxCabin.AutoSize = true;
            this.chkClosePaxCabin.Location = new System.Drawing.Point(247, 82);
            this.chkClosePaxCabin.Name = "chkClosePaxCabin";
            this.chkClosePaxCabin.Size = new System.Drawing.Size(96, 16);
            this.chkClosePaxCabin.TabIndex = 109;
            this.chkClosePaxCabin.Text = "没有关舱时间";
            this.chkClosePaxCabin.UseVisualStyleBackColor = true;
            // 
            // chkTOFFFlight
            // 
            this.chkTOFFFlight.AutoSize = true;
            this.chkTOFFFlight.Location = new System.Drawing.Point(247, 50);
            this.chkTOFFFlight.Name = "chkTOFFFlight";
            this.chkTOFFFlight.Size = new System.Drawing.Size(96, 16);
            this.chkTOFFFlight.TabIndex = 108;
            this.chkTOFFFlight.Text = "没有起飞动态";
            this.chkTOFFFlight.UseVisualStyleBackColor = true;
            // 
            // chkTDWNFlight
            // 
            this.chkTDWNFlight.AutoSize = true;
            this.chkTDWNFlight.Location = new System.Drawing.Point(247, 16);
            this.chkTDWNFlight.Name = "chkTDWNFlight";
            this.chkTDWNFlight.Size = new System.Drawing.Size(96, 16);
            this.chkTDWNFlight.TabIndex = 107;
            this.chkTDWNFlight.Text = "没有落地动态";
            this.chkTDWNFlight.UseVisualStyleBackColor = true;
            // 
            // chkIntermissionFlight
            // 
            this.chkIntermissionFlight.AutoSize = true;
            this.chkIntermissionFlight.Location = new System.Drawing.Point(3, 82);
            this.chkIntermissionFlight.Name = "chkIntermissionFlight";
            this.chkIntermissionFlight.Size = new System.Drawing.Size(120, 16);
            this.chkIntermissionFlight.TabIndex = 106;
            this.chkIntermissionFlight.Text = "过站时间不足航班";
            this.chkIntermissionFlight.UseVisualStyleBackColor = true;
            // 
            // chkDiversionFlight
            // 
            this.chkDiversionFlight.AutoSize = true;
            this.chkDiversionFlight.Location = new System.Drawing.Point(3, 50);
            this.chkDiversionFlight.Name = "chkDiversionFlight";
            this.chkDiversionFlight.Size = new System.Drawing.Size(96, 16);
            this.chkDiversionFlight.TabIndex = 105;
            this.chkDiversionFlight.Text = "备降返航航班";
            this.chkDiversionFlight.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 104;
            this.label1.Text = "分钟以上的航班";
            // 
            // updownDelayHour
            // 
            this.updownDelayHour.Location = new System.Drawing.Point(75, 15);
            this.updownDelayHour.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updownDelayHour.Name = "updownDelayHour";
            this.updownDelayHour.Size = new System.Drawing.Size(48, 21);
            this.updownDelayHour.TabIndex = 103;
            this.updownDelayHour.TabStop = false;
            this.updownDelayHour.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // chkDelayFlight
            // 
            this.chkDelayFlight.AutoSize = true;
            this.chkDelayFlight.Location = new System.Drawing.Point(3, 17);
            this.chkDelayFlight.Name = "chkDelayFlight";
            this.chkDelayFlight.Size = new System.Drawing.Size(72, 16);
            this.chkDelayFlight.TabIndex = 0;
            this.chkDelayFlight.Text = "显示延误";
            this.chkDelayFlight.UseVisualStyleBackColor = true;
            // 
            // fmDisplayCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 192);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmDisplayCondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "航班监控显示条件设置";
            this.Load += new System.EventHandler(this.fmDisplayCondition_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownDelayHour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbCondition;
        private System.Windows.Forms.RadioButton rdbAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkDelayFlight;
        private System.Windows.Forms.CheckBox chkIntermissionFlight;
        private System.Windows.Forms.CheckBox chkDiversionFlight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown updownDelayHour;
        private System.Windows.Forms.CheckBox chkClosePaxCabin;
        private System.Windows.Forms.CheckBox chkTOFFFlight;
        private System.Windows.Forms.CheckBox chkTDWNFlight;
    }
}