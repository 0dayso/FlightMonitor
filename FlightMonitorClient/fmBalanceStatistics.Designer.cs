namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmBalanceStatistics
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.fpFlightInfo = new FarPoint.Win.Spread.FpSpread();
            this.shSelectDate = new FarPoint.Win.Spread.SheetView();
            this.popNoMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.导出统计结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出到PDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shSelectDate)).BeginInit();
            this.popNoMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpFlightInfo
            // 
            this.fpFlightInfo.About = "2.5.2007.2005";
            this.fpFlightInfo.AccessibleDescription = "fpFlightInfo";
            this.fpFlightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFlightInfo.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpFlightInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.Location = new System.Drawing.Point(0, 0);
            this.fpFlightInfo.Name = "fpFlightInfo";
            this.fpFlightInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shSelectDate});
            this.fpFlightInfo.Size = new System.Drawing.Size(713, 464);
            this.fpFlightInfo.TabIndex = 11;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFlightInfo.TextTipAppearance = tipAppearance2;
            this.fpFlightInfo.TextTipDelay = 100;
            this.fpFlightInfo.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Fixed;
            this.fpFlightInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpFlightInfo_CellClick);
            // 
            // shSelectDate
            // 
            this.shSelectDate.Reset();
            this.shSelectDate.SheetName = "SelectDate";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shSelectDate.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shSelectDate.ColumnCount = 0;
            this.shSelectDate.ColumnHeader.RowCount = 2;
            this.shSelectDate.RowCount = 0;
            this.shSelectDate.AlternatingRows.Get(1).BackColor = System.Drawing.Color.White;
            this.shSelectDate.AutoGenerateColumns = false;
            this.shSelectDate.DataAutoCellTypes = false;
            this.shSelectDate.DataAutoHeadings = false;
            this.shSelectDate.DataAutoSizeColumns = false;
            this.shSelectDate.DefaultStyle.Border = lineBorder2;
            this.shSelectDate.DefaultStyle.Locked = false;
            this.shSelectDate.DefaultStyle.Parent = "DataAreaDefault";
            this.shSelectDate.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shSelectDate.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shSelectDate.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shSelectDate.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlightInfo.SetActiveViewport(1, 1);
            // 
            // popNoMenu
            // 
            this.popNoMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出统计结果ToolStripMenuItem,
            this.导出到PDFToolStripMenuItem});
            this.popNoMenu.Name = "popNoMenu";
            this.popNoMenu.Size = new System.Drawing.Size(143, 48);
            // 
            // 导出统计结果ToolStripMenuItem
            // 
            this.导出统计结果ToolStripMenuItem.Name = "导出统计结果ToolStripMenuItem";
            this.导出统计结果ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.导出统计结果ToolStripMenuItem.Text = "导出统计结果";
            this.导出统计结果ToolStripMenuItem.Click += new System.EventHandler(this.导出统计结果ToolStripMenuItem_Click);
            // 
            // 导出到PDFToolStripMenuItem
            // 
            this.导出到PDFToolStripMenuItem.Name = "导出到PDFToolStripMenuItem";
            this.导出到PDFToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.导出到PDFToolStripMenuItem.Text = "导出到PDF";
            this.导出到PDFToolStripMenuItem.Visible = false;
            this.导出到PDFToolStripMenuItem.Click += new System.EventHandler(this.导出到PDFToolStripMenuItem_Click);
            // 
            // fmBalanceStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 464);
            this.Controls.Add(this.fpFlightInfo);
            this.Name = "fmBalanceStatistics";
            this.Text = "配载统计数据";
            this.Load += new System.EventHandler(this.fmBalanceStatistics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shSelectDate)).EndInit();
            this.popNoMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpFlightInfo;
        private FarPoint.Win.Spread.SheetView shSelectDate;
        private System.Windows.Forms.ContextMenuStrip popNoMenu;
        private System.Windows.Forms.ToolStripMenuItem 导出统计结果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出到PDFToolStripMenuItem;
    }
}