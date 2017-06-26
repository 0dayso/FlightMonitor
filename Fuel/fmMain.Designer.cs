namespace Fuel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportFPLData = new System.Windows.Forms.ToolStripMenuItem();
            this.数据统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miStatisticFlyTime = new System.Windows.Forms.ToolStripMenuItem();
            this.miTaxiFlyTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbImportCFPData = new System.Windows.Forms.ToolStripButton();
            this.tbStatisticFlyTime = new System.Windows.Forms.ToolStripButton();
            this.tbTaxiFlyTime = new System.Windows.Forms.ToolStripButton();
            this.tbExport = new System.Windows.Forms.ToolStripButton();
            this.fpFlightInfo = new FarPoint.Win.Spread.FpSpread();
            this.shFlightInfor = new FarPoint.Win.Spread.SheetView();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shFlightInfor)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据管理ToolStripMenuItem,
            this.数据统计ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(870, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据管理ToolStripMenuItem
            // 
            this.数据管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miImportFPLData});
            this.数据管理ToolStripMenuItem.Name = "数据管理ToolStripMenuItem";
            this.数据管理ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.数据管理ToolStripMenuItem.Text = "数据管理";
            // 
            // miImportFPLData
            // 
            this.miImportFPLData.Name = "miImportFPLData";
            this.miImportFPLData.Size = new System.Drawing.Size(166, 22);
            this.miImportFPLData.Text = "导入飞行计划数据";
            this.miImportFPLData.Click += new System.EventHandler(this.miImportFPLData_Click);
            // 
            // 数据统计ToolStripMenuItem
            // 
            this.数据统计ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStatisticFlyTime,
            this.miTaxiFlyTime});
            this.数据统计ToolStripMenuItem.Name = "数据统计ToolStripMenuItem";
            this.数据统计ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.数据统计ToolStripMenuItem.Text = "数据统计";
            // 
            // miStatisticFlyTime
            // 
            this.miStatisticFlyTime.Name = "miStatisticFlyTime";
            this.miStatisticFlyTime.Size = new System.Drawing.Size(152, 22);
            this.miStatisticFlyTime.Text = "飞行时间统计";
            this.miStatisticFlyTime.Click += new System.EventHandler(this.miStatisticFlyTime_Click);
            // 
            // miTaxiFlyTime
            // 
            this.miTaxiFlyTime.Name = "miTaxiFlyTime";
            this.miTaxiFlyTime.Size = new System.Drawing.Size(152, 22);
            this.miTaxiFlyTime.Text = "滑行时间统计";
            this.miTaxiFlyTime.Click += new System.EventHandler(this.miTaxiFlyTime_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbImportCFPData,
            this.tbStatisticFlyTime,
            this.tbTaxiFlyTime,
            this.tbExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(870, 51);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbImportCFPData
            // 
            this.tbImportCFPData.Image = ((System.Drawing.Image)(resources.GetObject("tbImportCFPData.Image")));
            this.tbImportCFPData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbImportCFPData.Name = "tbImportCFPData";
            this.tbImportCFPData.Size = new System.Drawing.Size(75, 48);
            this.tbImportCFPData.Text = "导入CFP数据";
            this.tbImportCFPData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbImportCFPData.Click += new System.EventHandler(this.tbImportCFPData_Click);
            // 
            // tbStatisticFlyTime
            // 
            this.tbStatisticFlyTime.Image = ((System.Drawing.Image)(resources.GetObject("tbStatisticFlyTime.Image")));
            this.tbStatisticFlyTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbStatisticFlyTime.Name = "tbStatisticFlyTime";
            this.tbStatisticFlyTime.Size = new System.Drawing.Size(81, 48);
            this.tbStatisticFlyTime.Text = "飞行时间统计";
            this.tbStatisticFlyTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbStatisticFlyTime.Click += new System.EventHandler(this.tbStatisticFlyTime_Click);
            // 
            // tbTaxiFlyTime
            // 
            this.tbTaxiFlyTime.Image = ((System.Drawing.Image)(resources.GetObject("tbTaxiFlyTime.Image")));
            this.tbTaxiFlyTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbTaxiFlyTime.Name = "tbTaxiFlyTime";
            this.tbTaxiFlyTime.Size = new System.Drawing.Size(81, 48);
            this.tbTaxiFlyTime.Text = "滑行时间统计";
            this.tbTaxiFlyTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbTaxiFlyTime.Click += new System.EventHandler(this.tbTaxiFlyTime_Click);
            // 
            // tbExport
            // 
            this.tbExport.Image = ((System.Drawing.Image)(resources.GetObject("tbExport.Image")));
            this.tbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExport.Name = "tbExport";
            this.tbExport.Size = new System.Drawing.Size(57, 48);
            this.tbExport.Text = "导出动态";
            this.tbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbExport.Click += new System.EventHandler(this.tbExport_Click);
            // 
            // fpFlightInfo
            // 
            this.fpFlightInfo.About = "2.5.2007.2005";
            this.fpFlightInfo.AccessibleDescription = "fpFlightInfo";
            this.fpFlightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFlightInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpFlightInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.Location = new System.Drawing.Point(0, 75);
            this.fpFlightInfo.Name = "fpFlightInfo";
            this.fpFlightInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shFlightInfor});
            this.fpFlightInfo.Size = new System.Drawing.Size(870, 385);
            this.fpFlightInfo.TabIndex = 8;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFlightInfo.TextTipAppearance = tipAppearance2;
            this.fpFlightInfo.TextTipDelay = 100;
            this.fpFlightInfo.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Fixed;
            this.fpFlightInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // shFlightInfor
            // 
            this.shFlightInfor.Reset();
            this.shFlightInfor.SheetName = "今日";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shFlightInfor.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shFlightInfor.ColumnCount = 0;
            this.shFlightInfor.RowCount = 0;
            this.shFlightInfor.AutoGenerateColumns = false;
            this.shFlightInfor.DataAutoCellTypes = false;
            this.shFlightInfor.DataAutoHeadings = false;
            this.shFlightInfor.DataAutoSizeColumns = false;
            this.shFlightInfor.DefaultStyle.Border = lineBorder2;
            this.shFlightInfor.DefaultStyle.Locked = false;
            this.shFlightInfor.DefaultStyle.Parent = "DataAreaDefault";
            this.shFlightInfor.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shFlightInfor.RowHeader.Columns.Default.Resizable = false;
            this.shFlightInfor.SelectionBackColor = System.Drawing.Color.White;
            this.shFlightInfor.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shFlightInfor.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shFlightInfor.ZoomFactor = 0.96F;
            this.shFlightInfor.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlightInfo.SetActiveViewport(1, 1);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 460);
            this.Controls.Add(this.fpFlightInfo);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "节油减排数据统计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shFlightInfor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miImportFPLData;
        private System.Windows.Forms.ToolStripMenuItem 数据统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miStatisticFlyTime;
        private System.Windows.Forms.ToolStripMenuItem miTaxiFlyTime;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbImportCFPData;
        private System.Windows.Forms.ToolStripButton tbStatisticFlyTime;
        private System.Windows.Forms.ToolStripButton tbTaxiFlyTime;
        private FarPoint.Win.Spread.FpSpread fpFlightInfo;
        private FarPoint.Win.Spread.SheetView shFlightInfor;
        private System.Windows.Forms.ToolStripButton tbExport;
    }
}

