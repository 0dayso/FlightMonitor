namespace AirSoft.FlightMonitor.Server
{
    partial class fmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新导入航班计划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetJoinFlight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvMonitor = new System.Windows.Forms.ListView();
            this.cnhTime = new System.Windows.Forms.ColumnHeader();
            this.cnType = new System.Windows.Forms.ColumnHeader();
            this.cnResult = new System.Windows.Forms.ColumnHeader();
            this.timerSchedule = new System.Windows.Forms.Timer(this.components);
            this.timerChange = new System.Windows.Forms.Timer(this.components);
            this.timerVIP = new System.Windows.Forms.Timer(this.components);
            this.timerJoinFlight = new System.Windows.Forms.Timer(this.components);
            this.timerCheckUsers = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统管理ToolStripMenuItem
            // 
            this.系统管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新导入航班计划ToolStripMenuItem,
            this.tsmiGetJoinFlight});
            this.系统管理ToolStripMenuItem.Name = "系统管理ToolStripMenuItem";
            this.系统管理ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.系统管理ToolStripMenuItem.Text = "系统管理";
            // 
            // 重新导入航班计划ToolStripMenuItem
            // 
            this.重新导入航班计划ToolStripMenuItem.Name = "重新导入航班计划ToolStripMenuItem";
            this.重新导入航班计划ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.重新导入航班计划ToolStripMenuItem.Text = "重新导入航班计划";
            this.重新导入航班计划ToolStripMenuItem.Click += new System.EventHandler(this.重新导入航班计划ToolStripMenuItem_Click);
            // 
            // tsmiGetJoinFlight
            // 
            this.tsmiGetJoinFlight.Name = "tsmiGetJoinFlight";
            this.tsmiGetJoinFlight.Size = new System.Drawing.Size(166, 22);
            this.tsmiGetJoinFlight.Text = "查询衔接航班";
            this.tsmiGetJoinFlight.Click += new System.EventHandler(this.tsmiGetJoinFlight_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(894, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(105, 22);
            this.toolStripButton1.Text = "重新导入航班计划";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvMonitor);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(894, 344);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "实时监控";
            // 
            // lvMonitor
            // 
            this.lvMonitor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cnhTime,
            this.cnType,
            this.cnResult});
            this.lvMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMonitor.FullRowSelect = true;
            this.lvMonitor.GridLines = true;
            this.lvMonitor.Location = new System.Drawing.Point(3, 17);
            this.lvMonitor.Name = "lvMonitor";
            this.lvMonitor.Size = new System.Drawing.Size(888, 324);
            this.lvMonitor.TabIndex = 0;
            this.lvMonitor.UseCompatibleStateImageBehavior = false;
            this.lvMonitor.View = System.Windows.Forms.View.Details;
            // 
            // cnhTime
            // 
            this.cnhTime.Text = "处理时间";
            this.cnhTime.Width = 100;
            // 
            // cnType
            // 
            this.cnType.Text = "处理类型";
            this.cnType.Width = 100;
            // 
            // cnResult
            // 
            this.cnResult.Text = "处理情况";
            this.cnResult.Width = 200;
            // 
            // timerSchedule
            // 
            this.timerSchedule.Enabled = true;
            this.timerSchedule.Interval = 30000;
            this.timerSchedule.Tick += new System.EventHandler(this.timerSchedule_Tick);
            // 
            // timerChange
            // 
            this.timerChange.Enabled = true;
            this.timerChange.Interval = 10000;
            this.timerChange.Tick += new System.EventHandler(this.timerChange_Tick);
            // 
            // timerVIP
            // 
            this.timerVIP.Interval = 600000;
            this.timerVIP.Tick += new System.EventHandler(this.timerVIP_Tick);
            // 
            // timerJoinFlight
            // 
            this.timerJoinFlight.Interval = 1800000;
            this.timerJoinFlight.Tick += new System.EventHandler(this.timerJoinFlight_Tick);
            // 
            // timerCheckUsers
            // 
            this.timerCheckUsers.Interval = 240000;
            this.timerCheckUsers.Tick += new System.EventHandler(this.timerCheckUsers_Tick);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 393);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "航班保障系统服务器端软件（FOC）（航班计划和航班变更处理，无其它）【VER 1607141139】";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新导入航班计划ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timerSchedule;
        private System.Windows.Forms.Timer timerChange;
        private System.Windows.Forms.Timer timerVIP;
        private System.Windows.Forms.Timer timerJoinFlight;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetJoinFlight;
        private System.Windows.Forms.Timer timerCheckUsers;
        private System.Windows.Forms.ListView lvMonitor;
        private System.Windows.Forms.ColumnHeader cnhTime;
        private System.Windows.Forms.ColumnHeader cnType;
        private System.Windows.Forms.ColumnHeader cnResult;
    }
}

