namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmAircraftFlights
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpAircraftFlights = new FarPoint.Win.Spread.FpSpread();
            this.fpAircraftFlights_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.popOutFlightNoMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miOutCheckPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutTransitPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutNameList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.miOutFlightPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutTaskSheet = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutDispathSheet = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutWeather = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutWeatherStandard = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutNOTAM = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutCrew = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutFlightLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.miOutWeightData = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutFault = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutChangeData = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpAircraftFlights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAircraftFlights_Sheet1)).BeginInit();
            this.popOutFlightNoMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 342);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(789, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(722, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 24);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpAircraftFlights);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(789, 342);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // fpAircraftFlights
            // 
            this.fpAircraftFlights.About = "2.5.2007.2005";
            this.fpAircraftFlights.AccessibleDescription = "fpAircraftFlights";
            this.fpAircraftFlights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpAircraftFlights.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpAircraftFlights.Location = new System.Drawing.Point(3, 17);
            this.fpAircraftFlights.Name = "fpAircraftFlights";
            this.fpAircraftFlights.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpAircraftFlights_Sheet1});
            this.fpAircraftFlights.Size = new System.Drawing.Size(783, 322);
            this.fpAircraftFlights.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpAircraftFlights.TextTipAppearance = tipAppearance1;
            this.fpAircraftFlights.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpAircraftFlights.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpAircraftFlights_CellDoubleClick);
            this.fpAircraftFlights.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpAircraftFlights_CellClick);
            // 
            // fpAircraftFlights_Sheet1
            // 
            this.fpAircraftFlights_Sheet1.Reset();
            this.fpAircraftFlights_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpAircraftFlights_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpAircraftFlights_Sheet1.ColumnCount = 12;
            this.fpAircraftFlights_Sheet1.RowCount = 0;
            this.fpAircraftFlights_Sheet1.AutoGenerateColumns = false;
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "航班号";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "飞机号";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "航班性质";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "航班状态";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "起飞机场";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "计划起飞";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "预计起飞";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "实际起飞";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "到达机场";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "计划到达";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "预计到达";
            this.fpAircraftFlights_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "实际到达";
            this.fpAircraftFlights_Sheet1.Columns.Get(5).CellType = textCellType1;
            this.fpAircraftFlights_Sheet1.Columns.Get(5).Label = "计划起飞";
            this.fpAircraftFlights_Sheet1.Columns.Get(6).CellType = textCellType2;
            this.fpAircraftFlights_Sheet1.Columns.Get(6).Label = "预计起飞";
            this.fpAircraftFlights_Sheet1.Columns.Get(7).CellType = textCellType3;
            this.fpAircraftFlights_Sheet1.Columns.Get(7).Label = "实际起飞";
            this.fpAircraftFlights_Sheet1.Columns.Get(9).CellType = textCellType4;
            this.fpAircraftFlights_Sheet1.Columns.Get(9).Label = "计划到达";
            this.fpAircraftFlights_Sheet1.Columns.Get(10).CellType = textCellType5;
            this.fpAircraftFlights_Sheet1.Columns.Get(10).Label = "预计到达";
            this.fpAircraftFlights_Sheet1.Columns.Get(11).CellType = textCellType6;
            this.fpAircraftFlights_Sheet1.Columns.Get(11).Label = "实际到达";
            this.fpAircraftFlights_Sheet1.DataAutoSizeColumns = false;
            this.fpAircraftFlights_Sheet1.DefaultStyle.Border = lineBorder1;
            this.fpAircraftFlights_Sheet1.DefaultStyle.Locked = false;
            this.fpAircraftFlights_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpAircraftFlights_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpAircraftFlights_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpAircraftFlights_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpAircraftFlights_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpAircraftFlights_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpAircraftFlights.SetActiveViewport(1, 0);
            // 
            // popOutFlightNoMenu
            // 
            this.popOutFlightNoMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOutChangeData,
            this.toolStripMenuItem1,
            this.miOutCheckPax,
            this.miOutTransitPax,
            this.miOutNameList,
            this.toolStripMenuItem5,
            this.miOutFlightPlan,
            this.miOutTaskSheet,
            this.miOutDispathSheet,
            this.miOutWeather,
            this.miOutWeatherStandard,
            this.miOutNOTAM,
            this.miOutCrew,
            this.miOutFlightLine,
            this.toolStripMenuItem6,
            this.miOutWeightData,
            this.miOutFault});
            this.popOutFlightNoMenu.Name = "popOutFlightNoMenu";
            this.popOutFlightNoMenu.Size = new System.Drawing.Size(167, 330);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // miOutCheckPax
            // 
            this.miOutCheckPax.Name = "miOutCheckPax";
            this.miOutCheckPax.Size = new System.Drawing.Size(166, 22);
            this.miOutCheckPax.Text = "值机信息";
            this.miOutCheckPax.Click += new System.EventHandler(this.miOutCheckPax_Click);
            // 
            // miOutTransitPax
            // 
            this.miOutTransitPax.Name = "miOutTransitPax";
            this.miOutTransitPax.Size = new System.Drawing.Size(166, 22);
            this.miOutTransitPax.Text = "中转连程旅客信息";
            this.miOutTransitPax.Click += new System.EventHandler(this.miOutTransitPax_Click);
            // 
            // miOutNameList
            // 
            this.miOutNameList.Name = "miOutNameList";
            this.miOutNameList.Size = new System.Drawing.Size(166, 22);
            this.miOutNameList.Text = "旅客名单";
            this.miOutNameList.Click += new System.EventHandler(this.miOutNameList_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(163, 6);
            // 
            // miOutFlightPlan
            // 
            this.miOutFlightPlan.Name = "miOutFlightPlan";
            this.miOutFlightPlan.Size = new System.Drawing.Size(166, 22);
            this.miOutFlightPlan.Text = "飞行计划";
            this.miOutFlightPlan.Click += new System.EventHandler(this.miOutFlightPlan_Click);
            // 
            // miOutTaskSheet
            // 
            this.miOutTaskSheet.Name = "miOutTaskSheet";
            this.miOutTaskSheet.Size = new System.Drawing.Size(166, 22);
            this.miOutTaskSheet.Text = "任务书";
            this.miOutTaskSheet.Click += new System.EventHandler(this.miOutTaskSheet_Click);
            // 
            // miOutDispathSheet
            // 
            this.miOutDispathSheet.Name = "miOutDispathSheet";
            this.miOutDispathSheet.Size = new System.Drawing.Size(166, 22);
            this.miOutDispathSheet.Text = "签派放行单";
            this.miOutDispathSheet.Click += new System.EventHandler(this.miOutDispathSheet_Click);
            // 
            // miOutWeather
            // 
            this.miOutWeather.Name = "miOutWeather";
            this.miOutWeather.Size = new System.Drawing.Size(166, 22);
            this.miOutWeather.Text = "天气";
            this.miOutWeather.Click += new System.EventHandler(this.miOutWeather_Click);
            // 
            // miOutWeatherStandard
            // 
            this.miOutWeatherStandard.Name = "miOutWeatherStandard";
            this.miOutWeatherStandard.Size = new System.Drawing.Size(166, 22);
            this.miOutWeatherStandard.Text = "天气标准";
            this.miOutWeatherStandard.Click += new System.EventHandler(this.miOutWeatherStandard_Click);
            // 
            // miOutNOTAM
            // 
            this.miOutNOTAM.Name = "miOutNOTAM";
            this.miOutNOTAM.Size = new System.Drawing.Size(166, 22);
            this.miOutNOTAM.Text = "航行通告";
            this.miOutNOTAM.Click += new System.EventHandler(this.miOutNOTAM_Click);
            // 
            // miOutCrew
            // 
            this.miOutCrew.Name = "miOutCrew";
            this.miOutCrew.Size = new System.Drawing.Size(166, 22);
            this.miOutCrew.Text = "机组";
            this.miOutCrew.Click += new System.EventHandler(this.miOutCrew_Click);
            // 
            // miOutFlightLine
            // 
            this.miOutFlightLine.Name = "miOutFlightLine";
            this.miOutFlightLine.Size = new System.Drawing.Size(166, 22);
            this.miOutFlightLine.Text = "航线分析";
            this.miOutFlightLine.Click += new System.EventHandler(this.miOutFlightLine_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(163, 6);
            // 
            // miOutWeightData
            // 
            this.miOutWeightData.Name = "miOutWeightData";
            this.miOutWeightData.Size = new System.Drawing.Size(166, 22);
            this.miOutWeightData.Text = "重量数据";
            this.miOutWeightData.Click += new System.EventHandler(this.miOutWeightData_Click);
            // 
            // miOutFault
            // 
            this.miOutFault.Name = "miOutFault";
            this.miOutFault.Size = new System.Drawing.Size(166, 22);
            this.miOutFault.Text = "保留故障";
            this.miOutFault.Click += new System.EventHandler(this.miOutFault_Click);
            // 
            // miOutChangeData
            // 
            this.miOutChangeData.Name = "miOutChangeData";
            this.miOutChangeData.Size = new System.Drawing.Size(166, 22);
            this.miOutChangeData.Text = "变更数据";
            this.miOutChangeData.Click += new System.EventHandler(this.miOutChangeData_Click);
            // 
            // fmAircraftFlights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 385);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmAircraftFlights";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "航段信息";
            this.Load += new System.EventHandler(this.fmAircraftFlights_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpAircraftFlights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAircraftFlights_Sheet1)).EndInit();
            this.popOutFlightNoMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread fpAircraftFlights;
        private FarPoint.Win.Spread.SheetView fpAircraftFlights_Sheet1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip popOutFlightNoMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miOutCheckPax;
        private System.Windows.Forms.ToolStripMenuItem miOutTransitPax;
        private System.Windows.Forms.ToolStripMenuItem miOutNameList;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem miOutFlightPlan;
        private System.Windows.Forms.ToolStripMenuItem miOutTaskSheet;
        private System.Windows.Forms.ToolStripMenuItem miOutDispathSheet;
        private System.Windows.Forms.ToolStripMenuItem miOutWeather;
        private System.Windows.Forms.ToolStripMenuItem miOutWeatherStandard;
        private System.Windows.Forms.ToolStripMenuItem miOutNOTAM;
        private System.Windows.Forms.ToolStripMenuItem miOutCrew;
        private System.Windows.Forms.ToolStripMenuItem miOutFlightLine;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem miOutWeightData;
        private System.Windows.Forms.ToolStripMenuItem miOutFault;
        private System.Windows.Forms.ToolStripMenuItem miOutChangeData;
    }
}