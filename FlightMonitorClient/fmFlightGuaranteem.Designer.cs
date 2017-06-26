namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmFlightGuarantee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmFlightGuarantee));
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.LineBorder lineBorder9 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder10 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder12 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timerElapsed = new System.Timers.Timer();
            this.timerFlightNum = new System.Windows.Forms.Timer(this.components);
            this.dtFlightDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UpDownValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStations = new System.Windows.Forms.ComboBox();
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.timerChange = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.fpFlightInfo = new FarPoint.Win.Spread.FpSpread();
            this.shYestoday = new FarPoint.Win.Spread.SheetView();
            this.shToday = new FarPoint.Win.Spread.SheetView();
            this.shTomorrow = new FarPoint.Win.Spread.SheetView();
            this.shSelectDate = new FarPoint.Win.Spread.SheetView();
            this.popOutFlightNoMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miOutAircraftFlights = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutChangeData = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutFocusFlight = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miOutCrewSign = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutStewardSign = new System.Windows.Forms.ToolStripMenuItem();
            this.popInFlightNoMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miInAircraftFlights = new System.Windows.Forms.ToolStripMenuItem();
            this.miInChangeData = new System.Windows.Forms.ToolStripMenuItem();
            this.miInFocusFlight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miInCheckPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miInTransitPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miInNameList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miInFlightPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.miInTaskSheet = new System.Windows.Forms.ToolStripMenuItem();
            this.miInDispathSheet = new System.Windows.Forms.ToolStripMenuItem();
            this.miWeather = new System.Windows.Forms.ToolStripMenuItem();
            this.miInWeatherStandard = new System.Windows.Forms.ToolStripMenuItem();
            this.miInNOTAM = new System.Windows.Forms.ToolStripMenuItem();
            this.miInCrew = new System.Windows.Forms.ToolStripMenuItem();
            this.miInFlightLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miInWeightData = new System.Windows.Forms.ToolStripMenuItem();
            this.miInFault = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miInCrewSign = new System.Windows.Forms.ToolStripMenuItem();
            this.miInStewardSign = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.lvChangeContent = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.timerElapsed)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownValue)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shYestoday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shToday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shTomorrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shSelectDate)).BeginInit();
            this.popOutFlightNoMenu.SuspendLayout();
            this.popInFlightNoMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // timerElapsed
            // 
            this.timerElapsed.AutoReset = false;
            this.timerElapsed.Enabled = true;
            this.timerElapsed.Interval = 5000;
            this.timerElapsed.SynchronizingObject = this;
            // 
            // timerFlightNum
            // 
            this.timerFlightNum.Interval = 10000;
            // 
            // dtFlightDate
            // 
            this.dtFlightDate.Location = new System.Drawing.Point(211, 5);
            this.dtFlightDate.Name = "dtFlightDate";
            this.dtFlightDate.Size = new System.Drawing.Size(144, 21);
            this.dtFlightDate.TabIndex = 104;
            this.dtFlightDate.TabStop = false;
            this.dtFlightDate.ValueChanged += new System.EventHandler(this.dtFlightDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(171, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 103;
            this.label3.Text = "日期";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(356, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 102;
            this.label2.Text = "放大/缩小";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtFlightDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.UpDownValue);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbStations);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 434);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 32);
            this.panel1.TabIndex = 12;
            // 
            // UpDownValue
            // 
            this.UpDownValue.DecimalPlaces = 2;
            this.UpDownValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UpDownValue.Location = new System.Drawing.Point(421, 5);
            this.UpDownValue.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.UpDownValue.Name = "UpDownValue";
            this.UpDownValue.Size = new System.Drawing.Size(57, 21);
            this.UpDownValue.TabIndex = 101;
            this.UpDownValue.TabStop = false;
            this.UpDownValue.Value = new decimal(new int[] {
            96,
            0,
            0,
            131072});
            this.UpDownValue.ValueChanged += new System.EventHandler(this.UpDownValue_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "基地";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbStations
            // 
            this.cmbStations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStations.Location = new System.Drawing.Point(49, 6);
            this.cmbStations.Name = "cmbStations";
            this.cmbStations.Size = new System.Drawing.Size(121, 20);
            this.cmbStations.TabIndex = 100;
            this.cmbStations.TabStop = false;
            this.cmbStations.SelectionChangeCommitted += new System.EventHandler(this.cmbStations_SelectionChangeCommitted);
            // 
            // timerSplash
            // 
            this.timerSplash.Enabled = true;
            this.timerSplash.Interval = 1000;
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // timerChange
            // 
            this.timerChange.Enabled = true;
            this.timerChange.Interval = 20000;
            this.timerChange.Tick += new System.EventHandler(this.timerChange_Tick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpFlightInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(646, 325);
            this.panel2.TabIndex = 16;
            // 
            // fpFlightInfo
            // 
            this.fpFlightInfo.About = "2.5.2007.2005";
            this.fpFlightInfo.AccessibleDescription = "fpFlightInfo, 昨日, Row 0, Column 0, ";
            this.fpFlightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFlightInfo.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpFlightInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.Location = new System.Drawing.Point(0, 0);
            this.fpFlightInfo.Name = "fpFlightInfo";
            this.fpFlightInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shYestoday,
            this.shToday,
            this.shTomorrow,
            this.shSelectDate});
            this.fpFlightInfo.Size = new System.Drawing.Size(646, 325);
            this.fpFlightInfo.TabIndex = 7;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFlightInfo.TextTipAppearance = tipAppearance3;
            this.fpFlightInfo.TextTipDelay = 100;
            this.fpFlightInfo.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Fixed;
            this.fpFlightInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.ActiveSheetChanged += new System.EventHandler(this.fpFlightInfo_ActiveSheetChanged);
            this.fpFlightInfo.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpFlightInfo_CellDoubleClick);
            this.fpFlightInfo.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpFlightInfo_CellClick);
            this.fpFlightInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fpFlightInfo_KeyDown);
            this.fpFlightInfo.TextTipFetch += new FarPoint.Win.Spread.TextTipFetchEventHandler(this.fpFlightInfo_TextTipFetch);
            this.fpFlightInfo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fpFlightInfo_KeyUp);
            // 
            // shYestoday
            // 
            this.shYestoday.Reset();
            this.shYestoday.SheetName = "昨日";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shYestoday.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shYestoday.ColumnCount = 0;
            this.shYestoday.ColumnHeader.RowCount = 2;
            this.shYestoday.RowCount = 0;
            this.shYestoday.AutoGenerateColumns = false;
            this.shYestoday.DataAutoCellTypes = false;
            this.shYestoday.DataAutoHeadings = false;
            this.shYestoday.DataAutoSizeColumns = false;
            this.shYestoday.DefaultStyle.Border = lineBorder9;
            this.shYestoday.DefaultStyle.Locked = false;
            this.shYestoday.DefaultStyle.Parent = "DataAreaDefault";
            this.shYestoday.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shYestoday.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shYestoday.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shYestoday.ZoomFactor = 0.96F;
            this.shYestoday.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlightInfo.SetActiveViewport(1, 1);
            // 
            // shToday
            // 
            this.shToday.Reset();
            this.shToday.SheetName = "今日";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shToday.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shToday.ColumnCount = 0;
            this.shToday.ColumnHeader.RowCount = 2;
            this.shToday.RowCount = 0;
            this.shToday.AutoGenerateColumns = false;
            this.shToday.DataAutoCellTypes = false;
            this.shToday.DataAutoHeadings = false;
            this.shToday.DataAutoSizeColumns = false;
            this.shToday.DefaultStyle.Border = lineBorder10;
            this.shToday.DefaultStyle.Locked = false;
            this.shToday.DefaultStyle.Parent = "DataAreaDefault";
            this.shToday.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shToday.RowHeader.Columns.Default.Resizable = false;
            this.shToday.SelectionBackColor = System.Drawing.Color.White;
            this.shToday.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shToday.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shToday.ZoomFactor = 0.96F;
            this.shToday.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlightInfo.SetActiveViewport(1, 1, 1);
            // 
            // shTomorrow
            // 
            this.shTomorrow.Reset();
            this.shTomorrow.SheetName = "明日";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shTomorrow.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shTomorrow.ColumnCount = 0;
            this.shTomorrow.ColumnHeader.RowCount = 2;
            this.shTomorrow.RowCount = 0;
            this.shTomorrow.AutoGenerateColumns = false;
            this.shTomorrow.DataAutoCellTypes = false;
            this.shTomorrow.DataAutoHeadings = false;
            this.shTomorrow.DataAutoSizeColumns = false;
            this.shTomorrow.DefaultStyle.Border = lineBorder11;
            this.shTomorrow.DefaultStyle.Locked = false;
            this.shTomorrow.DefaultStyle.Parent = "DataAreaDefault";
            this.shTomorrow.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shTomorrow.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shTomorrow.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shTomorrow.ZoomFactor = 0.96F;
            this.shTomorrow.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlightInfo.SetActiveViewport(2, 1, 1);
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
            this.shSelectDate.DefaultStyle.Border = lineBorder12;
            this.shSelectDate.DefaultStyle.Locked = false;
            this.shSelectDate.DefaultStyle.Parent = "DataAreaDefault";
            this.shSelectDate.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shSelectDate.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shSelectDate.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shSelectDate.ZoomFactor = 0.96F;
            this.shSelectDate.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlightInfo.SetActiveViewport(3, 1, 1);
            // 
            // popOutFlightNoMenu
            // 
            this.popOutFlightNoMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOutAircraftFlights,
            this.miOutChangeData,
            this.miOutFocusFlight,
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
            this.miOutFault,
            this.toolStripMenuItem4,
            this.miOutCrewSign,
            this.miOutStewardSign});
            this.popOutFlightNoMenu.Name = "popOutFlightNoMenu";
            this.popOutFlightNoMenu.Size = new System.Drawing.Size(167, 424);
            // 
            // miOutAircraftFlights
            // 
            this.miOutAircraftFlights.Name = "miOutAircraftFlights";
            this.miOutAircraftFlights.Size = new System.Drawing.Size(166, 22);
            this.miOutAircraftFlights.Text = "航段信息";
            this.miOutAircraftFlights.Click += new System.EventHandler(this.miOutAircraftFlights_Click);
            // 
            // miOutChangeData
            // 
            this.miOutChangeData.Name = "miOutChangeData";
            this.miOutChangeData.Size = new System.Drawing.Size(166, 22);
            this.miOutChangeData.Text = "变更数据";
            this.miOutChangeData.Click += new System.EventHandler(this.miOutChangeData_Click);
            // 
            // miOutFocusFlight
            // 
            this.miOutFocusFlight.Name = "miOutFocusFlight";
            this.miOutFocusFlight.Size = new System.Drawing.Size(166, 22);
            this.miOutFocusFlight.Text = "重点保障航班说明";
            this.miOutFocusFlight.Click += new System.EventHandler(this.miOutFocusFlight_Click);
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
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(163, 6);
            // 
            // miOutCrewSign
            // 
            this.miOutCrewSign.Name = "miOutCrewSign";
            this.miOutCrewSign.Size = new System.Drawing.Size(166, 22);
            this.miOutCrewSign.Text = "机组签到";
            this.miOutCrewSign.Click += new System.EventHandler(this.miOutCrewSign_Click);
            // 
            // miOutStewardSign
            // 
            this.miOutStewardSign.Name = "miOutStewardSign";
            this.miOutStewardSign.Size = new System.Drawing.Size(166, 22);
            this.miOutStewardSign.Text = "乘务签到";
            this.miOutStewardSign.Click += new System.EventHandler(this.miOutStewardSign_Click);
            // 
            // popInFlightNoMenu
            // 
            this.popInFlightNoMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInAircraftFlights,
            this.miInChangeData,
            this.miInFocusFlight,
            this.toolStripMenuItem2,
            this.miInCheckPax,
            this.miInTransitPax,
            this.miInNameList,
            this.toolStripMenuItem3,
            this.miInFlightPlan,
            this.miInTaskSheet,
            this.miInDispathSheet,
            this.miWeather,
            this.miInWeatherStandard,
            this.miInNOTAM,
            this.miInCrew,
            this.miInFlightLine,
            this.toolStripSeparator1,
            this.miInWeightData,
            this.miInFault,
            this.toolStripSeparator2,
            this.miInCrewSign,
            this.miInStewardSign});
            this.popInFlightNoMenu.Name = "popOutFlightNoMenu";
            this.popInFlightNoMenu.Size = new System.Drawing.Size(167, 424);
            // 
            // miInAircraftFlights
            // 
            this.miInAircraftFlights.Name = "miInAircraftFlights";
            this.miInAircraftFlights.Size = new System.Drawing.Size(166, 22);
            this.miInAircraftFlights.Text = "航段信息";
            this.miInAircraftFlights.Click += new System.EventHandler(this.miInAircraftFlights_Click);
            // 
            // miInChangeData
            // 
            this.miInChangeData.Name = "miInChangeData";
            this.miInChangeData.Size = new System.Drawing.Size(166, 22);
            this.miInChangeData.Text = "变更数据";
            this.miInChangeData.Click += new System.EventHandler(this.miInChangeData_Click);
            // 
            // miInFocusFlight
            // 
            this.miInFocusFlight.Name = "miInFocusFlight";
            this.miInFocusFlight.Size = new System.Drawing.Size(166, 22);
            this.miInFocusFlight.Text = "重点保障航班说明";
            this.miInFocusFlight.Click += new System.EventHandler(this.miInFocusFlight_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
            // 
            // miInCheckPax
            // 
            this.miInCheckPax.Name = "miInCheckPax";
            this.miInCheckPax.Size = new System.Drawing.Size(166, 22);
            this.miInCheckPax.Text = "值机信息";
            this.miInCheckPax.Click += new System.EventHandler(this.miInCheckPax_Click);
            // 
            // miInTransitPax
            // 
            this.miInTransitPax.Name = "miInTransitPax";
            this.miInTransitPax.Size = new System.Drawing.Size(166, 22);
            this.miInTransitPax.Text = "中转连程旅客信息";
            this.miInTransitPax.Click += new System.EventHandler(this.miInTransitPax_Click);
            // 
            // miInNameList
            // 
            this.miInNameList.Name = "miInNameList";
            this.miInNameList.Size = new System.Drawing.Size(166, 22);
            this.miInNameList.Text = "旅客名单";
            this.miInNameList.Click += new System.EventHandler(this.miInNameList_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(163, 6);
            // 
            // miInFlightPlan
            // 
            this.miInFlightPlan.Name = "miInFlightPlan";
            this.miInFlightPlan.Size = new System.Drawing.Size(166, 22);
            this.miInFlightPlan.Text = "飞行计划";
            this.miInFlightPlan.Click += new System.EventHandler(this.miInFlightPlan_Click);
            // 
            // miInTaskSheet
            // 
            this.miInTaskSheet.Name = "miInTaskSheet";
            this.miInTaskSheet.Size = new System.Drawing.Size(166, 22);
            this.miInTaskSheet.Text = "任务书";
            this.miInTaskSheet.Click += new System.EventHandler(this.miInTaskSheet_Click);
            // 
            // miInDispathSheet
            // 
            this.miInDispathSheet.Name = "miInDispathSheet";
            this.miInDispathSheet.Size = new System.Drawing.Size(166, 22);
            this.miInDispathSheet.Text = "签派放行单";
            this.miInDispathSheet.Click += new System.EventHandler(this.miInDispathSheet_Click);
            // 
            // miWeather
            // 
            this.miWeather.Name = "miWeather";
            this.miWeather.Size = new System.Drawing.Size(166, 22);
            this.miWeather.Text = "天气";
            this.miWeather.Click += new System.EventHandler(this.miWeather_Click);
            // 
            // miInWeatherStandard
            // 
            this.miInWeatherStandard.Name = "miInWeatherStandard";
            this.miInWeatherStandard.Size = new System.Drawing.Size(166, 22);
            this.miInWeatherStandard.Text = "天气标准";
            this.miInWeatherStandard.Click += new System.EventHandler(this.miInWeatherStandard_Click);
            // 
            // miInNOTAM
            // 
            this.miInNOTAM.Name = "miInNOTAM";
            this.miInNOTAM.Size = new System.Drawing.Size(166, 22);
            this.miInNOTAM.Text = "航行通告";
            this.miInNOTAM.Click += new System.EventHandler(this.miInNOTAM_Click);
            // 
            // miInCrew
            // 
            this.miInCrew.Name = "miInCrew";
            this.miInCrew.Size = new System.Drawing.Size(166, 22);
            this.miInCrew.Text = "机组";
            this.miInCrew.Click += new System.EventHandler(this.miInCrew_Click);
            // 
            // miInFlightLine
            // 
            this.miInFlightLine.Name = "miInFlightLine";
            this.miInFlightLine.Size = new System.Drawing.Size(166, 22);
            this.miInFlightLine.Text = "航线分析";
            this.miInFlightLine.Click += new System.EventHandler(this.miInFlightLine_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // miInWeightData
            // 
            this.miInWeightData.Name = "miInWeightData";
            this.miInWeightData.Size = new System.Drawing.Size(166, 22);
            this.miInWeightData.Text = "重量数据";
            this.miInWeightData.Click += new System.EventHandler(this.miInWeightData_Click);
            // 
            // miInFault
            // 
            this.miInFault.Name = "miInFault";
            this.miInFault.Size = new System.Drawing.Size(166, 22);
            this.miInFault.Text = "保留故障";
            this.miInFault.Click += new System.EventHandler(this.miInFault_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(163, 6);
            // 
            // miInCrewSign
            // 
            this.miInCrewSign.Name = "miInCrewSign";
            this.miInCrewSign.Size = new System.Drawing.Size(166, 22);
            this.miInCrewSign.Text = "机组签到";
            this.miInCrewSign.Click += new System.EventHandler(this.miInCrewSign_Click);
            // 
            // miInStewardSign
            // 
            this.miInStewardSign.Name = "miInStewardSign";
            this.miInStewardSign.Size = new System.Drawing.Size(166, 22);
            this.miInStewardSign.Text = "乘务签到";
            this.miInStewardSign.Click += new System.EventHandler(this.miInStewardSign_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 325);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(646, 5);
            this.splitter1.TabIndex = 14;
            this.splitter1.TabStop = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "操作员";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "操作内容";
            this.columnHeader2.Width = 430;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "操作时间";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "航班日期";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 80;
            // 
            // lvChangeContent
            // 
            this.lvChangeContent.BackColor = System.Drawing.SystemColors.Window;
            this.lvChangeContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvChangeContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvChangeContent.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvChangeContent.FullRowSelect = true;
            this.lvChangeContent.GridLines = true;
            this.lvChangeContent.Location = new System.Drawing.Point(0, 330);
            this.lvChangeContent.MultiSelect = false;
            this.lvChangeContent.Name = "lvChangeContent";
            this.lvChangeContent.Size = new System.Drawing.Size(646, 104);
            this.lvChangeContent.SmallImageList = this.imageList1;
            this.lvChangeContent.TabIndex = 13;
            this.lvChangeContent.TabStop = false;
            this.lvChangeContent.UseCompatibleStateImageBehavior = false;
            this.lvChangeContent.View = System.Windows.Forms.View.Details;
            this.lvChangeContent.DoubleClick += new System.EventHandler(this.lvChangeContent_DoubleClick);
            // 
            // fmFlightGuarantee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 466);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvChangeContent);
            this.Controls.Add(this.panel1);
            this.Name = "fmFlightGuarantee";
            this.Text = "航站保障";
            this.Load += new System.EventHandler(this.fmFlightGuarantee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timerElapsed)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UpDownValue)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shYestoday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shToday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shTomorrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shSelectDate)).EndInit();
            this.popOutFlightNoMenu.ResumeLayout(false);
            this.popInFlightNoMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Timers.Timer timerElapsed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtFlightDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UpDownValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStations;
        private System.Windows.Forms.Timer timerFlightNum;
        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.Timer timerChange;
        private System.Windows.Forms.Panel panel2;
        private FarPoint.Win.Spread.FpSpread fpFlightInfo;
        private FarPoint.Win.Spread.SheetView shYestoday;
        private FarPoint.Win.Spread.SheetView shToday;
        private FarPoint.Win.Spread.SheetView shTomorrow;
        private FarPoint.Win.Spread.SheetView shSelectDate;
        private System.Windows.Forms.ContextMenuStrip popOutFlightNoMenu;
        private System.Windows.Forms.ToolStripMenuItem miOutAircraftFlights;
        private System.Windows.Forms.ContextMenuStrip popInFlightNoMenu;
        private System.Windows.Forms.ToolStripMenuItem miInAircraftFlights;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miOutCheckPax;
        private System.Windows.Forms.ToolStripMenuItem miOutTransitPax;
        private System.Windows.Forms.ToolStripMenuItem miOutNameList;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miInCheckPax;
        private System.Windows.Forms.ToolStripMenuItem miInTransitPax;
        private System.Windows.Forms.ToolStripMenuItem miInNameList;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miOutChangeData;
        private System.Windows.Forms.ToolStripMenuItem miInChangeData;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem miOutFlightPlan;
        private System.Windows.Forms.ToolStripMenuItem miOutDispathSheet;
        private System.Windows.Forms.ToolStripMenuItem miOutWeather;
        private System.Windows.Forms.ToolStripMenuItem miOutWeatherStandard;
        private System.Windows.Forms.ToolStripMenuItem miOutNOTAM;
        private System.Windows.Forms.ToolStripMenuItem miOutCrew;
        private System.Windows.Forms.ToolStripMenuItem miOutFlightLine;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem miOutCrewSign;
        private System.Windows.Forms.ToolStripMenuItem miOutStewardSign;
        private System.Windows.Forms.ToolStripMenuItem miInFlightPlan;
        private System.Windows.Forms.ToolStripMenuItem miInDispathSheet;
        private System.Windows.Forms.ToolStripMenuItem miWeather;
        private System.Windows.Forms.ToolStripMenuItem miInWeatherStandard;
        private System.Windows.Forms.ToolStripMenuItem miInNOTAM;
        private System.Windows.Forms.ToolStripMenuItem miInCrew;
        private System.Windows.Forms.ToolStripMenuItem miInFlightLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miInCrewSign;
        private System.Windows.Forms.ToolStripMenuItem miInStewardSign;
        private System.Windows.Forms.ToolStripMenuItem miOutFault;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miOutWeightData;
        private System.Windows.Forms.ToolStripMenuItem miInWeightData;
        private System.Windows.Forms.ToolStripMenuItem miInFault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miOutFocusFlight;
        private System.Windows.Forms.ToolStripMenuItem miInFocusFlight;
        private System.Windows.Forms.ToolStripMenuItem miOutTaskSheet;
        private System.Windows.Forms.ToolStripMenuItem miInTaskSheet;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView lvChangeContent;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}