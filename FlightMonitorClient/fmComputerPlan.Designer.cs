namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmComputerPlan
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
            this.components = new System.ComponentModel.Container();
            this.btnPreView = new System.Windows.Forms.Button();
            this.btnSetUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtBackUp = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbSpecialPrag = new System.Windows.Forms.CheckBox();
            this.cbImportant = new System.Windows.Forms.CheckBox();
            this.cbRoute = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAirplaneDD = new System.Windows.Forms.CheckBox();
            this.cbMet = new System.Windows.Forms.CheckBox();
            this.cbNotam = new System.Windows.Forms.CheckBox();
            this.cbFlightPlan = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.wbPlan = new System.Windows.Forms.WebBrowser();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPreView
            // 
            this.btnPreView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreView.Location = new System.Drawing.Point(650, 16);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(56, 23);
            this.btnPreView.TabIndex = 13;
            this.btnPreView.Text = "预 览";
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
            // 
            // btnSetUp
            // 
            this.btnSetUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetUp.Location = new System.Drawing.Point(586, 16);
            this.btnSetUp.Name = "btnSetUp";
            this.btnSetUp.Size = new System.Drawing.Size(56, 23);
            this.btnSetUp.TabIndex = 12;
            this.btnSetUp.Text = "设 置";
            this.btnSetUp.Click += new System.EventHandler(this.btnSetUp_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(579, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "备降场";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPreView);
            this.groupBox2.Controls.Add(this.btnSetUp);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 442);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(842, 48);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(778, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(714, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(56, 23);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtBackUp
            // 
            this.txtBackUp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBackUp.Location = new System.Drawing.Point(629, 15);
            this.txtBackUp.Name = "txtBackUp";
            this.txtBackUp.Size = new System.Drawing.Size(140, 21);
            this.txtBackUp.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(775, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(56, 23);
            this.btnQuery.TabIndex = 12;
            this.btnQuery.Text = "查 询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cbSpecialPrag
            // 
            this.cbSpecialPrag.Checked = true;
            this.cbSpecialPrag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSpecialPrag.Location = new System.Drawing.Point(480, 13);
            this.cbSpecialPrag.Name = "cbSpecialPrag";
            this.cbSpecialPrag.Size = new System.Drawing.Size(99, 24);
            this.cbSpecialPrag.TabIndex = 6;
            this.cbSpecialPrag.Text = "航线特殊程序";
            // 
            // cbImportant
            // 
            this.cbImportant.Checked = true;
            this.cbImportant.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbImportant.Location = new System.Drawing.Point(403, 13);
            this.cbImportant.Name = "cbImportant";
            this.cbImportant.Size = new System.Drawing.Size(73, 24);
            this.cbImportant.TabIndex = 5;
            this.cbImportant.Text = "重要信息";
            // 
            // cbRoute
            // 
            this.cbRoute.Checked = true;
            this.cbRoute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRoute.Location = new System.Drawing.Point(311, 13);
            this.cbRoute.Name = "cbRoute";
            this.cbRoute.Size = new System.Drawing.Size(88, 24);
            this.cbRoute.TabIndex = 4;
            this.cbRoute.Text = "签派放行单";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.cbSpecialPrag);
            this.groupBox1.Controls.Add(this.cbImportant);
            this.groupBox1.Controls.Add(this.cbRoute);
            this.groupBox1.Controls.Add(this.cbAirplaneDD);
            this.groupBox1.Controls.Add(this.cbMet);
            this.groupBox1.Controls.Add(this.cbNotam);
            this.groupBox1.Controls.Add(this.cbFlightPlan);
            this.groupBox1.Controls.Add(this.txtBackUp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(842, 42);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // cbAirplaneDD
            // 
            this.cbAirplaneDD.Checked = true;
            this.cbAirplaneDD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAirplaneDD.Location = new System.Drawing.Point(234, 13);
            this.cbAirplaneDD.Name = "cbAirplaneDD";
            this.cbAirplaneDD.Size = new System.Drawing.Size(73, 24);
            this.cbAirplaneDD.TabIndex = 3;
            this.cbAirplaneDD.Text = "飞机状况";
            // 
            // cbMet
            // 
            this.cbMet.Checked = true;
            this.cbMet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMet.Location = new System.Drawing.Point(157, 13);
            this.cbMet.Name = "cbMet";
            this.cbMet.Size = new System.Drawing.Size(73, 24);
            this.cbMet.TabIndex = 2;
            this.cbMet.Text = "气象信息";
            // 
            // cbNotam
            // 
            this.cbNotam.Checked = true;
            this.cbNotam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNotam.Location = new System.Drawing.Point(80, 13);
            this.cbNotam.Name = "cbNotam";
            this.cbNotam.Size = new System.Drawing.Size(73, 24);
            this.cbNotam.TabIndex = 1;
            this.cbNotam.Text = "航行通告";
            // 
            // cbFlightPlan
            // 
            this.cbFlightPlan.Checked = true;
            this.cbFlightPlan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFlightPlan.Location = new System.Drawing.Point(3, 13);
            this.cbFlightPlan.Name = "cbFlightPlan";
            this.cbFlightPlan.Size = new System.Drawing.Size(73, 24);
            this.cbFlightPlan.TabIndex = 0;
            this.cbFlightPlan.Text = "飞行计划";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.wbPlan);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(842, 400);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // wbPlan
            // 
            this.wbPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbPlan.Location = new System.Drawing.Point(3, 17);
            this.wbPlan.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPlan.Name = "wbPlan";
            this.wbPlan.Size = new System.Drawing.Size(836, 380);
            this.wbPlan.TabIndex = 0;
            // 
            // fmComputerPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 490);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "fmComputerPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计算机飞行计划";
            this.Load += new System.EventHandler(this.fmComputerPlan_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPreView;
        private System.Windows.Forms.Button btnSetUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtBackUp;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.CheckBox cbSpecialPrag;
        private System.Windows.Forms.CheckBox cbImportant;
        private System.Windows.Forms.CheckBox cbRoute;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbAirplaneDD;
        private System.Windows.Forms.CheckBox cbMet;
        private System.Windows.Forms.CheckBox cbNotam;
        private System.Windows.Forms.CheckBox cbFlightPlan;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.WebBrowser wbPlan;
    }
}