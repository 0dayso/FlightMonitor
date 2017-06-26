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
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.timerChange = new System.Windows.Forms.Timer(this.components);
            this.popOutFlightNoMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miOutAircraftFlights = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutChangeData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutFocusFlight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miOutCheckPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutTransitPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutNameList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.miInFocusFlight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miInCheckPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miInTransitPax = new System.Windows.Forms.ToolStripMenuItem();
            this.miInNameList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.fpFlightInfo = new FarPoint.Win.Spread.FpSpread();
            this.shYestoday = new FarPoint.Win.Spread.SheetView();
            this.shToday = new FarPoint.Win.Spread.SheetView();
            this.shTomorrow = new FarPoint.Win.Spread.SheetView();
            this.shSelectDate = new FarPoint.Win.Spread.SheetView();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataTabControlGanttView = new System.Windows.Forms.TabControl();
            this.msgTabPage = new System.Windows.Forms.TabPage();
            this.lvChangeContent = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.inTabPage = new System.Windows.Forms.TabPage();
            this.intabControl = new System.Windows.Forms.TabControl();
            this.inPSRtabPage = new System.Windows.Forms.TabPage();
            this.intabControl_PSR = new System.Windows.Forms.TabControl();
            this.inSumTabPage = new System.Windows.Forms.TabPage();
            this.inTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.inboardlabel = new System.Windows.Forms.Label();
            this.inchildlabel = new System.Windows.Forms.Label();
            this.inmeallabel = new System.Windows.Forms.Label();
            this.inbabylabel = new System.Windows.Forms.Label();
            this.injplabel = new System.Windows.Forms.Label();
            this.inFYlabel = new System.Windows.Forms.Label();
            this.intstlabel = new System.Windows.Forms.Label();
            this.injinlabel = new System.Windows.Forms.Label();
            this.insslabel = new System.Windows.Forms.Label();
            this.inbirthlabel = new System.Windows.Forms.Label();
            this.insumlabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.inviplabel = new System.Windows.Forms.Label();
            this.inVipTabPage = new System.Windows.Forms.TabPage();
            this.intVIPListView = new System.Windows.Forms.ListView();
            this.inSSTabPage = new System.Windows.Forms.TabPage();
            this.inSSListView = new System.Windows.Forms.ListView();
            this.inMealTabPage = new System.Windows.Forms.TabPage();
            this.inMealListView = new System.Windows.Forms.ListView();
            this.inJINTabPage = new System.Windows.Forms.TabPage();
            this.inclistView = new System.Windows.Forms.ListView();
            this.inTSTabPage = new System.Windows.Forms.TabPage();
            this.inTSTListView = new System.Windows.Forms.ListView();
            this.inOTHTabPage = new System.Windows.Forms.TabPage();
            this.inplistView = new System.Windows.Forms.ListView();
            this.inTaskBooktabPage = new System.Windows.Forms.TabPage();
            this.intabControl_Taskbook = new System.Windows.Forms.TabControl();
            this.inTaskbookAnalyseTabPage = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.inTaskbookCompareTabPage = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.inTaskbookOriginalTabPage = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.inTaskbookConfiguration = new System.Windows.Forms.TabPage();
            this.chkinTaskbookConfiguration_NewLine = new System.Windows.Forms.CheckBox();
            this.outTabPage = new System.Windows.Forms.TabPage();
            this.outtabControl = new System.Windows.Forms.TabControl();
            this.outPSRtabPage = new System.Windows.Forms.TabPage();
            this.outTabControl_PSR = new System.Windows.Forms.TabControl();
            this.outSumTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.outboardlabel = new System.Windows.Forms.Label();
            this.outchildlabel = new System.Windows.Forms.Label();
            this.outmeallabel = new System.Windows.Forms.Label();
            this.outbabylabel = new System.Windows.Forms.Label();
            this.outjplabel = new System.Windows.Forms.Label();
            this.outFYlabel = new System.Windows.Forms.Label();
            this.outtstlabel = new System.Windows.Forms.Label();
            this.outjinlabel = new System.Windows.Forms.Label();
            this.outsslabel = new System.Windows.Forms.Label();
            this.outbirthlabel = new System.Windows.Forms.Label();
            this.outsumlabel = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.outviplabel = new System.Windows.Forms.Label();
            this.outVIPTabPage = new System.Windows.Forms.TabPage();
            this.outVIPListView = new System.Windows.Forms.ListView();
            this.outSSTabPage = new System.Windows.Forms.TabPage();
            this.outSSListView = new System.Windows.Forms.ListView();
            this.outMealTabPage = new System.Windows.Forms.TabPage();
            this.outMealListView = new System.Windows.Forms.ListView();
            this.outJINTabPage = new System.Windows.Forms.TabPage();
            this.outclistView = new System.Windows.Forms.ListView();
            this.outTSTabPage = new System.Windows.Forms.TabPage();
            this.outTSTListView = new System.Windows.Forms.ListView();
            this.outOTHTabPage = new System.Windows.Forms.TabPage();
            this.outplistView = new System.Windows.Forms.ListView();
            this.outTaskBooktabPage = new System.Windows.Forms.TabPage();
            this.outtabControl_Taskbook = new System.Windows.Forms.TabControl();
            this.outTaskbookAnalyseTabPage = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.outTaskbookCompareTabPage = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.outTaskbookOriginalTabPage = new System.Windows.Forms.TabPage();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.outTaskbookConfiguration = new System.Windows.Forms.TabPage();
            this.chkoutTaskbookConfiguration_NewLine = new System.Windows.Forms.CheckBox();
            this.gantabPage = new System.Windows.Forms.TabPage();
            this.ganttwebBrowser = new System.Windows.Forms.WebBrowser();
            this.tabPageDataItem = new System.Windows.Forms.TabPage();
            this.ganttViewToolBox = new AirSoft.FlightMonitor.FlightMonitorClient.fmGanttView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label44 = new System.Windows.Forms.Label();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label43 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dtFlightDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UpDownValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStations = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.timerElapsed)).BeginInit();
            this.popOutFlightNoMenu.SuspendLayout();
            this.popInFlightNoMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shYestoday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shToday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shTomorrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shSelectDate)).BeginInit();
            this.panel1.SuspendLayout();
            this.dataTabControlGanttView.SuspendLayout();
            this.msgTabPage.SuspendLayout();
            this.inTabPage.SuspendLayout();
            this.intabControl.SuspendLayout();
            this.inPSRtabPage.SuspendLayout();
            this.intabControl_PSR.SuspendLayout();
            this.inSumTabPage.SuspendLayout();
            this.inTableLayoutPanel.SuspendLayout();
            this.inVipTabPage.SuspendLayout();
            this.inSSTabPage.SuspendLayout();
            this.inMealTabPage.SuspendLayout();
            this.inJINTabPage.SuspendLayout();
            this.inTSTabPage.SuspendLayout();
            this.inOTHTabPage.SuspendLayout();
            this.inTaskBooktabPage.SuspendLayout();
            this.intabControl_Taskbook.SuspendLayout();
            this.inTaskbookAnalyseTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.inTaskbookCompareTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.inTaskbookOriginalTabPage.SuspendLayout();
            this.inTaskbookConfiguration.SuspendLayout();
            this.outTabPage.SuspendLayout();
            this.outtabControl.SuspendLayout();
            this.outPSRtabPage.SuspendLayout();
            this.outTabControl_PSR.SuspendLayout();
            this.outSumTabPage.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.outVIPTabPage.SuspendLayout();
            this.outSSTabPage.SuspendLayout();
            this.outMealTabPage.SuspendLayout();
            this.outJINTabPage.SuspendLayout();
            this.outTSTabPage.SuspendLayout();
            this.outOTHTabPage.SuspendLayout();
            this.outTaskBooktabPage.SuspendLayout();
            this.outtabControl_Taskbook.SuspendLayout();
            this.outTaskbookAnalyseTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.outTaskbookCompareTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.outTaskbookOriginalTabPage.SuspendLayout();
            this.outTaskbookConfiguration.SuspendLayout();
            this.gantabPage.SuspendLayout();
            this.tabPageDataItem.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownValue)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            // popOutFlightNoMenu
            // 
            this.popOutFlightNoMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOutAircraftFlights,
            this.miOutChangeData,
            this.toolStripMenuItem8,
            this.miOutFocusFlight,
            this.toolStripMenuItem1,
            this.miOutCheckPax,
            this.miOutTransitPax,
            this.miOutNameList,
            this.toolStripMenuItem9,
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
            this.popOutFlightNoMenu.Size = new System.Drawing.Size(167, 468);
            this.popOutFlightNoMenu.Opening += new System.ComponentModel.CancelEventHandler(this.popOutFlightNoMenu_Opening);
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
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem8.Text = "保障记录";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
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
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem9.Text = "旅客信息";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
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
            this.toolStripMenuItem7,
            this.miInFocusFlight,
            this.toolStripMenuItem2,
            this.miInCheckPax,
            this.miInTransitPax,
            this.miInNameList,
            this.toolStripMenuItem10,
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
            this.popInFlightNoMenu.Size = new System.Drawing.Size(167, 468);
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
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem7.Text = "保障记录";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
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
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem10.Text = "旅客信息";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
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
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1411, 442);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 15;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.fpFlightInfo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Panel2.Controls.Add(this.textBox1);
            this.splitContainer2.Size = new System.Drawing.Size(1411, 257);
            this.splitContainer2.SplitterDistance = 1370;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
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
            this.shYestoday,
            this.shToday,
            this.shTomorrow,
            this.shSelectDate});
            this.fpFlightInfo.Size = new System.Drawing.Size(1370, 257);
            this.fpFlightInfo.TabIndex = 10;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 23);
            this.button1.TabIndex = 112;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 135);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(15, 21);
            this.textBox1.TabIndex = 113;
            this.textBox1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataTabControlGanttView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1411, 146);
            this.panel1.TabIndex = 107;
            // 
            // dataTabControlGanttView
            // 
            this.dataTabControlGanttView.Controls.Add(this.msgTabPage);
            this.dataTabControlGanttView.Controls.Add(this.inTabPage);
            this.dataTabControlGanttView.Controls.Add(this.outTabPage);
            this.dataTabControlGanttView.Controls.Add(this.gantabPage);
            this.dataTabControlGanttView.Controls.Add(this.tabPageDataItem);
            this.dataTabControlGanttView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTabControlGanttView.Location = new System.Drawing.Point(0, 0);
            this.dataTabControlGanttView.Name = "dataTabControlGanttView";
            this.dataTabControlGanttView.SelectedIndex = 0;
            this.dataTabControlGanttView.Size = new System.Drawing.Size(1411, 146);
            this.dataTabControlGanttView.TabIndex = 0;
            this.dataTabControlGanttView.SelectedIndexChanged += new System.EventHandler(this.dataTabControl_SelectedIndexChanged);
            // 
            // msgTabPage
            // 
            this.msgTabPage.Controls.Add(this.lvChangeContent);
            this.msgTabPage.Location = new System.Drawing.Point(4, 21);
            this.msgTabPage.Name = "msgTabPage";
            this.msgTabPage.Size = new System.Drawing.Size(1403, 121);
            this.msgTabPage.TabIndex = 5;
            this.msgTabPage.Text = "操作记录";
            this.msgTabPage.UseVisualStyleBackColor = true;
            // 
            // lvChangeContent
            // 
            this.lvChangeContent.BackColor = System.Drawing.SystemColors.Window;
            this.lvChangeContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvChangeContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvChangeContent.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvChangeContent.FullRowSelect = true;
            this.lvChangeContent.GridLines = true;
            this.lvChangeContent.Location = new System.Drawing.Point(0, 0);
            this.lvChangeContent.MultiSelect = false;
            this.lvChangeContent.Name = "lvChangeContent";
            this.lvChangeContent.Size = new System.Drawing.Size(1403, 121);
            this.lvChangeContent.SmallImageList = this.imageList1;
            this.lvChangeContent.TabIndex = 16;
            this.lvChangeContent.TabStop = false;
            this.lvChangeContent.UseCompatibleStateImageBehavior = false;
            this.lvChangeContent.View = System.Windows.Forms.View.Details;
            this.lvChangeContent.DoubleClick += new System.EventHandler(this.lvChangeContent_DoubleClick);
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
            // inTabPage
            // 
            this.inTabPage.Controls.Add(this.intabControl);
            this.inTabPage.Location = new System.Drawing.Point(4, 21);
            this.inTabPage.Name = "inTabPage";
            this.inTabPage.Size = new System.Drawing.Size(1403, 121);
            this.inTabPage.TabIndex = 4;
            this.inTabPage.Text = "进港航班";
            this.inTabPage.UseVisualStyleBackColor = true;
            // 
            // intabControl
            // 
            this.intabControl.Controls.Add(this.inPSRtabPage);
            this.intabControl.Controls.Add(this.inTaskBooktabPage);
            this.intabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intabControl.Location = new System.Drawing.Point(0, 0);
            this.intabControl.Name = "intabControl";
            this.intabControl.SelectedIndex = 0;
            this.intabControl.Size = new System.Drawing.Size(1403, 121);
            this.intabControl.TabIndex = 0;
            this.intabControl.SelectedIndexChanged += new System.EventHandler(this.intabControl_SelectedIndexChanged);
            // 
            // inPSRtabPage
            // 
            this.inPSRtabPage.Controls.Add(this.intabControl_PSR);
            this.inPSRtabPage.Location = new System.Drawing.Point(4, 21);
            this.inPSRtabPage.Name = "inPSRtabPage";
            this.inPSRtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inPSRtabPage.Size = new System.Drawing.Size(1395, 96);
            this.inPSRtabPage.TabIndex = 0;
            this.inPSRtabPage.Text = "旅客信息";
            this.inPSRtabPage.UseVisualStyleBackColor = true;
            // 
            // intabControl_PSR
            // 
            this.intabControl_PSR.Controls.Add(this.inSumTabPage);
            this.intabControl_PSR.Controls.Add(this.inVipTabPage);
            this.intabControl_PSR.Controls.Add(this.inSSTabPage);
            this.intabControl_PSR.Controls.Add(this.inMealTabPage);
            this.intabControl_PSR.Controls.Add(this.inJINTabPage);
            this.intabControl_PSR.Controls.Add(this.inTSTabPage);
            this.intabControl_PSR.Controls.Add(this.inOTHTabPage);
            this.intabControl_PSR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intabControl_PSR.Location = new System.Drawing.Point(3, 3);
            this.intabControl_PSR.Name = "intabControl_PSR";
            this.intabControl_PSR.SelectedIndex = 0;
            this.intabControl_PSR.Size = new System.Drawing.Size(1389, 90);
            this.intabControl_PSR.TabIndex = 0;
            this.intabControl_PSR.SelectedIndexChanged += new System.EventHandler(this.intabControl_PSR_SelectedIndexChanged);
            // 
            // inSumTabPage
            // 
            this.inSumTabPage.Controls.Add(this.inTableLayoutPanel);
            this.inSumTabPage.Location = new System.Drawing.Point(4, 21);
            this.inSumTabPage.Name = "inSumTabPage";
            this.inSumTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inSumTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inSumTabPage.TabIndex = 0;
            this.inSumTabPage.Text = "旅客值机数据";
            this.inSumTabPage.UseVisualStyleBackColor = true;
            // 
            // inTableLayoutPanel
            // 
            this.inTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.inTableLayoutPanel.ColumnCount = 6;
            this.inTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.inTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.inTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.inTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.inTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.inTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.inTableLayoutPanel.Controls.Add(this.inboardlabel, 5, 3);
            this.inTableLayoutPanel.Controls.Add(this.inchildlabel, 3, 3);
            this.inTableLayoutPanel.Controls.Add(this.inmeallabel, 1, 3);
            this.inTableLayoutPanel.Controls.Add(this.inbabylabel, 5, 2);
            this.inTableLayoutPanel.Controls.Add(this.injplabel, 3, 2);
            this.inTableLayoutPanel.Controls.Add(this.inFYlabel, 1, 2);
            this.inTableLayoutPanel.Controls.Add(this.intstlabel, 5, 1);
            this.inTableLayoutPanel.Controls.Add(this.injinlabel, 3, 1);
            this.inTableLayoutPanel.Controls.Add(this.insslabel, 1, 1);
            this.inTableLayoutPanel.Controls.Add(this.inbirthlabel, 5, 0);
            this.inTableLayoutPanel.Controls.Add(this.insumlabel, 1, 0);
            this.inTableLayoutPanel.Controls.Add(this.label5, 0, 0);
            this.inTableLayoutPanel.Controls.Add(this.label4, 0, 1);
            this.inTableLayoutPanel.Controls.Add(this.label6, 0, 2);
            this.inTableLayoutPanel.Controls.Add(this.label7, 0, 3);
            this.inTableLayoutPanel.Controls.Add(this.label8, 2, 0);
            this.inTableLayoutPanel.Controls.Add(this.label9, 2, 1);
            this.inTableLayoutPanel.Controls.Add(this.label10, 2, 2);
            this.inTableLayoutPanel.Controls.Add(this.label11, 2, 3);
            this.inTableLayoutPanel.Controls.Add(this.label12, 4, 0);
            this.inTableLayoutPanel.Controls.Add(this.label13, 4, 1);
            this.inTableLayoutPanel.Controls.Add(this.label14, 4, 2);
            this.inTableLayoutPanel.Controls.Add(this.label15, 4, 3);
            this.inTableLayoutPanel.Controls.Add(this.inviplabel, 3, 0);
            this.inTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.inTableLayoutPanel.Name = "inTableLayoutPanel";
            this.inTableLayoutPanel.RowCount = 4;
            this.inTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inTableLayoutPanel.Size = new System.Drawing.Size(1375, 59);
            this.inTableLayoutPanel.TabIndex = 2;
            // 
            // inboardlabel
            // 
            this.inboardlabel.AutoSize = true;
            this.inboardlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inboardlabel.Location = new System.Drawing.Point(1144, 43);
            this.inboardlabel.Name = "inboardlabel";
            this.inboardlabel.Size = new System.Drawing.Size(227, 15);
            this.inboardlabel.TabIndex = 23;
            this.inboardlabel.Text = "inboardlabel";
            this.inboardlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inchildlabel
            // 
            this.inchildlabel.AutoSize = true;
            this.inchildlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inchildlabel.Location = new System.Drawing.Point(688, 43);
            this.inchildlabel.Name = "inchildlabel";
            this.inchildlabel.Size = new System.Drawing.Size(221, 15);
            this.inchildlabel.TabIndex = 22;
            this.inchildlabel.Text = "inchildlabel";
            this.inchildlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inmeallabel
            // 
            this.inmeallabel.AutoSize = true;
            this.inmeallabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inmeallabel.Location = new System.Drawing.Point(232, 43);
            this.inmeallabel.Name = "inmeallabel";
            this.inmeallabel.Size = new System.Drawing.Size(221, 15);
            this.inmeallabel.TabIndex = 21;
            this.inmeallabel.Text = "inmeallabel";
            this.inmeallabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inbabylabel
            // 
            this.inbabylabel.AutoSize = true;
            this.inbabylabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inbabylabel.Location = new System.Drawing.Point(1144, 29);
            this.inbabylabel.Name = "inbabylabel";
            this.inbabylabel.Size = new System.Drawing.Size(227, 13);
            this.inbabylabel.TabIndex = 20;
            this.inbabylabel.Text = "inbabylabel";
            this.inbabylabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // injplabel
            // 
            this.injplabel.AutoSize = true;
            this.injplabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.injplabel.Location = new System.Drawing.Point(688, 29);
            this.injplabel.Name = "injplabel";
            this.injplabel.Size = new System.Drawing.Size(221, 13);
            this.injplabel.TabIndex = 19;
            this.injplabel.Text = "injplabel";
            this.injplabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inFYlabel
            // 
            this.inFYlabel.AutoSize = true;
            this.inFYlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inFYlabel.Location = new System.Drawing.Point(232, 29);
            this.inFYlabel.Name = "inFYlabel";
            this.inFYlabel.Size = new System.Drawing.Size(221, 13);
            this.inFYlabel.TabIndex = 18;
            this.inFYlabel.Text = "inFYlabel";
            this.inFYlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // intstlabel
            // 
            this.intstlabel.AutoSize = true;
            this.intstlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intstlabel.Location = new System.Drawing.Point(1144, 15);
            this.intstlabel.Name = "intstlabel";
            this.intstlabel.Size = new System.Drawing.Size(227, 13);
            this.intstlabel.TabIndex = 17;
            this.intstlabel.Text = "intstlabel";
            this.intstlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // injinlabel
            // 
            this.injinlabel.AutoSize = true;
            this.injinlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.injinlabel.Location = new System.Drawing.Point(688, 15);
            this.injinlabel.Name = "injinlabel";
            this.injinlabel.Size = new System.Drawing.Size(221, 13);
            this.injinlabel.TabIndex = 16;
            this.injinlabel.Text = "injinlabel";
            this.injinlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // insslabel
            // 
            this.insslabel.AutoSize = true;
            this.insslabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.insslabel.Location = new System.Drawing.Point(232, 15);
            this.insslabel.Name = "insslabel";
            this.insslabel.Size = new System.Drawing.Size(221, 13);
            this.insslabel.TabIndex = 15;
            this.insslabel.Text = "insslabel";
            this.insslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inbirthlabel
            // 
            this.inbirthlabel.AutoSize = true;
            this.inbirthlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inbirthlabel.Location = new System.Drawing.Point(1144, 1);
            this.inbirthlabel.Name = "inbirthlabel";
            this.inbirthlabel.Size = new System.Drawing.Size(227, 13);
            this.inbirthlabel.TabIndex = 14;
            this.inbirthlabel.Text = "inbirthlabel";
            this.inbirthlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // insumlabel
            // 
            this.insumlabel.AutoSize = true;
            this.insumlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.insumlabel.Location = new System.Drawing.Point(232, 1);
            this.insumlabel.Name = "insumlabel";
            this.insumlabel.Size = new System.Drawing.Size(221, 13);
            this.insumlabel.TabIndex = 13;
            this.insumlabel.Text = "insumlabel";
            this.insumlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(221, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "订座人数/机组/已值机人数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(221, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "特服旅客";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(4, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "F/C/Y舱";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(4, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(221, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "特殊餐食";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(460, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(221, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "VIP/CIP/M6";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(460, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(221, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "金鹏金卡/银卡";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(460, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(221, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "金鹿卡";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(460, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(221, 15);
            this.label11.TabIndex = 7;
            this.label11.Text = "儿童";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(916, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(221, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "生日旅客";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(916, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(221, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "中转旅客(转入/转出)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(916, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(221, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "婴儿";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(916, 43);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(221, 15);
            this.label15.TabIndex = 11;
            this.label15.Text = "登机口";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inviplabel
            // 
            this.inviplabel.AutoSize = true;
            this.inviplabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inviplabel.Location = new System.Drawing.Point(688, 1);
            this.inviplabel.Name = "inviplabel";
            this.inviplabel.Size = new System.Drawing.Size(221, 13);
            this.inviplabel.TabIndex = 12;
            this.inviplabel.Text = "inviplabel";
            this.inviplabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inVipTabPage
            // 
            this.inVipTabPage.Controls.Add(this.intVIPListView);
            this.inVipTabPage.Location = new System.Drawing.Point(4, 21);
            this.inVipTabPage.Name = "inVipTabPage";
            this.inVipTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inVipTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inVipTabPage.TabIndex = 1;
            this.inVipTabPage.Text = "要客信息";
            this.inVipTabPage.UseVisualStyleBackColor = true;
            // 
            // intVIPListView
            // 
            this.intVIPListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intVIPListView.Location = new System.Drawing.Point(3, 3);
            this.intVIPListView.Name = "intVIPListView";
            this.intVIPListView.Size = new System.Drawing.Size(1375, 59);
            this.intVIPListView.TabIndex = 0;
            this.intVIPListView.UseCompatibleStateImageBehavior = false;
            this.intVIPListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.intVIPListView_ColumnClick);
            // 
            // inSSTabPage
            // 
            this.inSSTabPage.Controls.Add(this.inSSListView);
            this.inSSTabPage.Location = new System.Drawing.Point(4, 21);
            this.inSSTabPage.Name = "inSSTabPage";
            this.inSSTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inSSTabPage.TabIndex = 2;
            this.inSSTabPage.Text = "特服旅客";
            this.inSSTabPage.UseVisualStyleBackColor = true;
            // 
            // inSSListView
            // 
            this.inSSListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inSSListView.Location = new System.Drawing.Point(0, 0);
            this.inSSListView.Name = "inSSListView";
            this.inSSListView.Size = new System.Drawing.Size(1381, 65);
            this.inSSListView.TabIndex = 0;
            this.inSSListView.UseCompatibleStateImageBehavior = false;
            this.inSSListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.inSSListView_ColumnClick);
            // 
            // inMealTabPage
            // 
            this.inMealTabPage.Controls.Add(this.inMealListView);
            this.inMealTabPage.Location = new System.Drawing.Point(4, 21);
            this.inMealTabPage.Name = "inMealTabPage";
            this.inMealTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inMealTabPage.TabIndex = 3;
            this.inMealTabPage.Text = "特餐旅客";
            this.inMealTabPage.UseVisualStyleBackColor = true;
            // 
            // inMealListView
            // 
            this.inMealListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inMealListView.Location = new System.Drawing.Point(0, 0);
            this.inMealListView.Name = "inMealListView";
            this.inMealListView.Size = new System.Drawing.Size(1381, 65);
            this.inMealListView.TabIndex = 0;
            this.inMealListView.UseCompatibleStateImageBehavior = false;
            this.inMealListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.inMealListView_ColumnClick);
            // 
            // inJINTabPage
            // 
            this.inJINTabPage.Controls.Add(this.inclistView);
            this.inJINTabPage.Location = new System.Drawing.Point(4, 21);
            this.inJINTabPage.Name = "inJINTabPage";
            this.inJINTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inJINTabPage.TabIndex = 4;
            this.inJINTabPage.Text = "常旅客";
            this.inJINTabPage.UseVisualStyleBackColor = true;
            // 
            // inclistView
            // 
            this.inclistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inclistView.Location = new System.Drawing.Point(0, 0);
            this.inclistView.Name = "inclistView";
            this.inclistView.Size = new System.Drawing.Size(1381, 65);
            this.inclistView.TabIndex = 0;
            this.inclistView.UseCompatibleStateImageBehavior = false;
            this.inclistView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.inclistView_ColumnClick);
            // 
            // inTSTabPage
            // 
            this.inTSTabPage.Controls.Add(this.inTSTListView);
            this.inTSTabPage.Location = new System.Drawing.Point(4, 21);
            this.inTSTabPage.Name = "inTSTabPage";
            this.inTSTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inTSTabPage.TabIndex = 5;
            this.inTSTabPage.Text = "中转旅客";
            this.inTSTabPage.UseVisualStyleBackColor = true;
            // 
            // inTSTListView
            // 
            this.inTSTListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inTSTListView.Location = new System.Drawing.Point(0, 0);
            this.inTSTListView.Name = "inTSTListView";
            this.inTSTListView.Size = new System.Drawing.Size(1381, 65);
            this.inTSTListView.TabIndex = 0;
            this.inTSTListView.UseCompatibleStateImageBehavior = false;
            this.inTSTListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.inTSTListView_ColumnClick);
            // 
            // inOTHTabPage
            // 
            this.inOTHTabPage.Controls.Add(this.inplistView);
            this.inOTHTabPage.Location = new System.Drawing.Point(4, 21);
            this.inOTHTabPage.Name = "inOTHTabPage";
            this.inOTHTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inOTHTabPage.TabIndex = 6;
            this.inOTHTabPage.Text = "普通旅客";
            this.inOTHTabPage.UseVisualStyleBackColor = true;
            // 
            // inplistView
            // 
            this.inplistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inplistView.Location = new System.Drawing.Point(0, 0);
            this.inplistView.Name = "inplistView";
            this.inplistView.Size = new System.Drawing.Size(1381, 65);
            this.inplistView.TabIndex = 0;
            this.inplistView.UseCompatibleStateImageBehavior = false;
            this.inplistView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.inplistView_ColumnClick);
            // 
            // inTaskBooktabPage
            // 
            this.inTaskBooktabPage.Controls.Add(this.intabControl_Taskbook);
            this.inTaskBooktabPage.Location = new System.Drawing.Point(4, 21);
            this.inTaskBooktabPage.Name = "inTaskBooktabPage";
            this.inTaskBooktabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inTaskBooktabPage.Size = new System.Drawing.Size(1395, 96);
            this.inTaskBooktabPage.TabIndex = 1;
            this.inTaskBooktabPage.Text = "任务书";
            this.inTaskBooktabPage.UseVisualStyleBackColor = true;
            // 
            // intabControl_Taskbook
            // 
            this.intabControl_Taskbook.Controls.Add(this.inTaskbookAnalyseTabPage);
            this.intabControl_Taskbook.Controls.Add(this.inTaskbookCompareTabPage);
            this.intabControl_Taskbook.Controls.Add(this.inTaskbookOriginalTabPage);
            this.intabControl_Taskbook.Controls.Add(this.inTaskbookConfiguration);
            this.intabControl_Taskbook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intabControl_Taskbook.Location = new System.Drawing.Point(3, 3);
            this.intabControl_Taskbook.Name = "intabControl_Taskbook";
            this.intabControl_Taskbook.SelectedIndex = 0;
            this.intabControl_Taskbook.Size = new System.Drawing.Size(1389, 90);
            this.intabControl_Taskbook.TabIndex = 0;
            this.intabControl_Taskbook.SelectedIndexChanged += new System.EventHandler(this.intabControl_Taskbook_SelectedIndexChanged);
            // 
            // inTaskbookAnalyseTabPage
            // 
            this.inTaskbookAnalyseTabPage.Controls.Add(this.dataGridView1);
            this.inTaskbookAnalyseTabPage.Location = new System.Drawing.Point(4, 21);
            this.inTaskbookAnalyseTabPage.Name = "inTaskbookAnalyseTabPage";
            this.inTaskbookAnalyseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inTaskbookAnalyseTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inTaskbookAnalyseTabPage.TabIndex = 0;
            this.inTaskbookAnalyseTabPage.Text = "分析（每人一行）";
            this.inTaskbookAnalyseTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1375, 59);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // inTaskbookCompareTabPage
            // 
            this.inTaskbookCompareTabPage.Controls.Add(this.dataGridView3);
            this.inTaskbookCompareTabPage.Location = new System.Drawing.Point(4, 21);
            this.inTaskbookCompareTabPage.Name = "inTaskbookCompareTabPage";
            this.inTaskbookCompareTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inTaskbookCompareTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inTaskbookCompareTabPage.TabIndex = 2;
            this.inTaskbookCompareTabPage.Text = "前后查询比较";
            this.inTaskbookCompareTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(1375, 59);
            this.dataGridView3.TabIndex = 1;
            this.dataGridView3.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            this.dataGridView3.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // inTaskbookOriginalTabPage
            // 
            this.inTaskbookOriginalTabPage.Controls.Add(this.webBrowser1);
            this.inTaskbookOriginalTabPage.Location = new System.Drawing.Point(4, 21);
            this.inTaskbookOriginalTabPage.Name = "inTaskbookOriginalTabPage";
            this.inTaskbookOriginalTabPage.Size = new System.Drawing.Size(1381, 65);
            this.inTaskbookOriginalTabPage.TabIndex = 3;
            this.inTaskbookOriginalTabPage.Text = "任务书(运行网)";
            this.inTaskbookOriginalTabPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1381, 65);
            this.webBrowser1.TabIndex = 1;
            // 
            // inTaskbookConfiguration
            // 
            this.inTaskbookConfiguration.Controls.Add(this.chkinTaskbookConfiguration_NewLine);
            this.inTaskbookConfiguration.Location = new System.Drawing.Point(4, 21);
            this.inTaskbookConfiguration.Name = "inTaskbookConfiguration";
            this.inTaskbookConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.inTaskbookConfiguration.Size = new System.Drawing.Size(1381, 65);
            this.inTaskbookConfiguration.TabIndex = 5;
            this.inTaskbookConfiguration.Text = "配置";
            this.inTaskbookConfiguration.UseVisualStyleBackColor = true;
            // 
            // chkinTaskbookConfiguration_NewLine
            // 
            this.chkinTaskbookConfiguration_NewLine.AutoSize = true;
            this.chkinTaskbookConfiguration_NewLine.Location = new System.Drawing.Point(33, 21);
            this.chkinTaskbookConfiguration_NewLine.Name = "chkinTaskbookConfiguration_NewLine";
            this.chkinTaskbookConfiguration_NewLine.Size = new System.Drawing.Size(72, 16);
            this.chkinTaskbookConfiguration_NewLine.TabIndex = 0;
            this.chkinTaskbookConfiguration_NewLine.Text = "网格换行";
            this.chkinTaskbookConfiguration_NewLine.UseVisualStyleBackColor = true;
            this.chkinTaskbookConfiguration_NewLine.CheckedChanged += new System.EventHandler(this.chkinTaskbookConfiguration_NewLine_CheckedChanged);
            // 
            // outTabPage
            // 
            this.outTabPage.Controls.Add(this.outtabControl);
            this.outTabPage.Location = new System.Drawing.Point(4, 21);
            this.outTabPage.Name = "outTabPage";
            this.outTabPage.Size = new System.Drawing.Size(1403, 121);
            this.outTabPage.TabIndex = 3;
            this.outTabPage.Text = "出港航班";
            this.outTabPage.UseVisualStyleBackColor = true;
            // 
            // outtabControl
            // 
            this.outtabControl.Controls.Add(this.outPSRtabPage);
            this.outtabControl.Controls.Add(this.outTaskBooktabPage);
            this.outtabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outtabControl.Location = new System.Drawing.Point(0, 0);
            this.outtabControl.Name = "outtabControl";
            this.outtabControl.SelectedIndex = 0;
            this.outtabControl.Size = new System.Drawing.Size(1403, 121);
            this.outtabControl.TabIndex = 0;
            this.outtabControl.SelectedIndexChanged += new System.EventHandler(this.outtabControl_SelectedIndexChanged);
            // 
            // outPSRtabPage
            // 
            this.outPSRtabPage.Controls.Add(this.outTabControl_PSR);
            this.outPSRtabPage.Location = new System.Drawing.Point(4, 21);
            this.outPSRtabPage.Name = "outPSRtabPage";
            this.outPSRtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outPSRtabPage.Size = new System.Drawing.Size(1395, 96);
            this.outPSRtabPage.TabIndex = 0;
            this.outPSRtabPage.Text = "旅客信息";
            this.outPSRtabPage.UseVisualStyleBackColor = true;
            // 
            // outTabControl_PSR
            // 
            this.outTabControl_PSR.Controls.Add(this.outSumTabPage);
            this.outTabControl_PSR.Controls.Add(this.outVIPTabPage);
            this.outTabControl_PSR.Controls.Add(this.outSSTabPage);
            this.outTabControl_PSR.Controls.Add(this.outMealTabPage);
            this.outTabControl_PSR.Controls.Add(this.outJINTabPage);
            this.outTabControl_PSR.Controls.Add(this.outTSTabPage);
            this.outTabControl_PSR.Controls.Add(this.outOTHTabPage);
            this.outTabControl_PSR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outTabControl_PSR.Location = new System.Drawing.Point(3, 3);
            this.outTabControl_PSR.Name = "outTabControl_PSR";
            this.outTabControl_PSR.SelectedIndex = 0;
            this.outTabControl_PSR.Size = new System.Drawing.Size(1389, 90);
            this.outTabControl_PSR.TabIndex = 1;
            this.outTabControl_PSR.SelectedIndexChanged += new System.EventHandler(this.outTabControl_PSR_SelectedIndexChanged);
            // 
            // outSumTabPage
            // 
            this.outSumTabPage.Controls.Add(this.tableLayoutPanel3);
            this.outSumTabPage.Location = new System.Drawing.Point(4, 21);
            this.outSumTabPage.Name = "outSumTabPage";
            this.outSumTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outSumTabPage.Size = new System.Drawing.Size(1381, 65);
            this.outSumTabPage.TabIndex = 0;
            this.outSumTabPage.Text = "旅客值机数据";
            this.outSumTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.outboardlabel, 5, 3);
            this.tableLayoutPanel3.Controls.Add(this.outchildlabel, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.outmeallabel, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.outbabylabel, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.outjplabel, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.outFYlabel, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.outtstlabel, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.outjinlabel, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.outsslabel, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.outbirthlabel, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.outsumlabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label54, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label55, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label56, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label57, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label58, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label59, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label60, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label61, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.label62, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.label63, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.label64, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.label65, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.outviplabel, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1375, 59);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // outboardlabel
            // 
            this.outboardlabel.AutoSize = true;
            this.outboardlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outboardlabel.Location = new System.Drawing.Point(1144, 43);
            this.outboardlabel.Name = "outboardlabel";
            this.outboardlabel.Size = new System.Drawing.Size(227, 15);
            this.outboardlabel.TabIndex = 23;
            this.outboardlabel.Text = "outboardlabel";
            this.outboardlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outchildlabel
            // 
            this.outchildlabel.AutoSize = true;
            this.outchildlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outchildlabel.Location = new System.Drawing.Point(688, 43);
            this.outchildlabel.Name = "outchildlabel";
            this.outchildlabel.Size = new System.Drawing.Size(221, 15);
            this.outchildlabel.TabIndex = 22;
            this.outchildlabel.Text = "outchildlabel";
            this.outchildlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outmeallabel
            // 
            this.outmeallabel.AutoSize = true;
            this.outmeallabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outmeallabel.Location = new System.Drawing.Point(232, 43);
            this.outmeallabel.Name = "outmeallabel";
            this.outmeallabel.Size = new System.Drawing.Size(221, 15);
            this.outmeallabel.TabIndex = 21;
            this.outmeallabel.Text = "outmeallabel";
            this.outmeallabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outbabylabel
            // 
            this.outbabylabel.AutoSize = true;
            this.outbabylabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outbabylabel.Location = new System.Drawing.Point(1144, 29);
            this.outbabylabel.Name = "outbabylabel";
            this.outbabylabel.Size = new System.Drawing.Size(227, 13);
            this.outbabylabel.TabIndex = 20;
            this.outbabylabel.Text = "outbabylabel";
            this.outbabylabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outjplabel
            // 
            this.outjplabel.AutoSize = true;
            this.outjplabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outjplabel.Location = new System.Drawing.Point(688, 29);
            this.outjplabel.Name = "outjplabel";
            this.outjplabel.Size = new System.Drawing.Size(221, 13);
            this.outjplabel.TabIndex = 19;
            this.outjplabel.Text = "outjplabel";
            this.outjplabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outFYlabel
            // 
            this.outFYlabel.AutoSize = true;
            this.outFYlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outFYlabel.Location = new System.Drawing.Point(232, 29);
            this.outFYlabel.Name = "outFYlabel";
            this.outFYlabel.Size = new System.Drawing.Size(221, 13);
            this.outFYlabel.TabIndex = 18;
            this.outFYlabel.Text = "outFYlabel";
            this.outFYlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outtstlabel
            // 
            this.outtstlabel.AutoSize = true;
            this.outtstlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outtstlabel.Location = new System.Drawing.Point(1144, 15);
            this.outtstlabel.Name = "outtstlabel";
            this.outtstlabel.Size = new System.Drawing.Size(227, 13);
            this.outtstlabel.TabIndex = 17;
            this.outtstlabel.Text = "outtstlabel";
            this.outtstlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outjinlabel
            // 
            this.outjinlabel.AutoSize = true;
            this.outjinlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outjinlabel.Location = new System.Drawing.Point(688, 15);
            this.outjinlabel.Name = "outjinlabel";
            this.outjinlabel.Size = new System.Drawing.Size(221, 13);
            this.outjinlabel.TabIndex = 16;
            this.outjinlabel.Text = "outjinlabel";
            this.outjinlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outsslabel
            // 
            this.outsslabel.AutoSize = true;
            this.outsslabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outsslabel.Location = new System.Drawing.Point(232, 15);
            this.outsslabel.Name = "outsslabel";
            this.outsslabel.Size = new System.Drawing.Size(221, 13);
            this.outsslabel.TabIndex = 15;
            this.outsslabel.Text = "outsslabel";
            this.outsslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outbirthlabel
            // 
            this.outbirthlabel.AutoSize = true;
            this.outbirthlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outbirthlabel.Location = new System.Drawing.Point(1144, 1);
            this.outbirthlabel.Name = "outbirthlabel";
            this.outbirthlabel.Size = new System.Drawing.Size(227, 13);
            this.outbirthlabel.TabIndex = 14;
            this.outbirthlabel.Text = "outbirthlabel";
            this.outbirthlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outsumlabel
            // 
            this.outsumlabel.AutoSize = true;
            this.outsumlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outsumlabel.Location = new System.Drawing.Point(232, 1);
            this.outsumlabel.Name = "outsumlabel";
            this.outsumlabel.Size = new System.Drawing.Size(221, 13);
            this.outsumlabel.TabIndex = 13;
            this.outsumlabel.Text = "outsumlabel";
            this.outsumlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label54.Location = new System.Drawing.Point(4, 1);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(221, 13);
            this.label54.TabIndex = 1;
            this.label54.Text = "订座人数/机组/已值机人数";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label55.Location = new System.Drawing.Point(4, 15);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(221, 13);
            this.label55.TabIndex = 0;
            this.label55.Text = "特服旅客";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label56.Location = new System.Drawing.Point(4, 29);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(221, 13);
            this.label56.TabIndex = 2;
            this.label56.Text = "F/C/Y舱";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label57.Location = new System.Drawing.Point(4, 43);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(221, 15);
            this.label57.TabIndex = 3;
            this.label57.Text = "特殊餐食";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label58.Location = new System.Drawing.Point(460, 1);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(221, 13);
            this.label58.TabIndex = 4;
            this.label58.Text = "VIP/CIP/M6";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label59.Location = new System.Drawing.Point(460, 15);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(221, 13);
            this.label59.TabIndex = 5;
            this.label59.Text = "金鹏金卡/银卡";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label60.Location = new System.Drawing.Point(460, 29);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(221, 13);
            this.label60.TabIndex = 6;
            this.label60.Text = "金鹿卡";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label61.Location = new System.Drawing.Point(460, 43);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(221, 15);
            this.label61.TabIndex = 7;
            this.label61.Text = "儿童";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label62.Location = new System.Drawing.Point(916, 1);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(221, 13);
            this.label62.TabIndex = 8;
            this.label62.Text = "生日旅客";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label63.Location = new System.Drawing.Point(916, 15);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(221, 13);
            this.label63.TabIndex = 9;
            this.label63.Text = "中转旅客(转入/转出)";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label64.Location = new System.Drawing.Point(916, 29);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(221, 13);
            this.label64.TabIndex = 10;
            this.label64.Text = "婴儿";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label65.Location = new System.Drawing.Point(916, 43);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(221, 15);
            this.label65.TabIndex = 11;
            this.label65.Text = "登机口";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outviplabel
            // 
            this.outviplabel.AutoSize = true;
            this.outviplabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outviplabel.Location = new System.Drawing.Point(688, 1);
            this.outviplabel.Name = "outviplabel";
            this.outviplabel.Size = new System.Drawing.Size(221, 13);
            this.outviplabel.TabIndex = 12;
            this.outviplabel.Text = "outviplabel";
            this.outviplabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outVIPTabPage
            // 
            this.outVIPTabPage.Controls.Add(this.outVIPListView);
            this.outVIPTabPage.Location = new System.Drawing.Point(4, 21);
            this.outVIPTabPage.Name = "outVIPTabPage";
            this.outVIPTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outVIPTabPage.Size = new System.Drawing.Size(1381, 65);
            this.outVIPTabPage.TabIndex = 1;
            this.outVIPTabPage.Text = "要客信息";
            this.outVIPTabPage.UseVisualStyleBackColor = true;
            // 
            // outVIPListView
            // 
            this.outVIPListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outVIPListView.Location = new System.Drawing.Point(3, 3);
            this.outVIPListView.Name = "outVIPListView";
            this.outVIPListView.Size = new System.Drawing.Size(1375, 59);
            this.outVIPListView.TabIndex = 2;
            this.outVIPListView.UseCompatibleStateImageBehavior = false;
            this.outVIPListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.outVIPListView_ColumnClick);
            // 
            // outSSTabPage
            // 
            this.outSSTabPage.Controls.Add(this.outSSListView);
            this.outSSTabPage.Location = new System.Drawing.Point(4, 21);
            this.outSSTabPage.Name = "outSSTabPage";
            this.outSSTabPage.Size = new System.Drawing.Size(1381, 65);
            this.outSSTabPage.TabIndex = 2;
            this.outSSTabPage.Text = "特服旅客";
            this.outSSTabPage.UseVisualStyleBackColor = true;
            // 
            // outSSListView
            // 
            this.outSSListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outSSListView.Location = new System.Drawing.Point(0, 0);
            this.outSSListView.Name = "outSSListView";
            this.outSSListView.Size = new System.Drawing.Size(1381, 65);
            this.outSSListView.TabIndex = 2;
            this.outSSListView.UseCompatibleStateImageBehavior = false;
            this.outSSListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.outSSListView_ColumnClick);
            // 
            // outMealTabPage
            // 
            this.outMealTabPage.Controls.Add(this.outMealListView);
            this.outMealTabPage.Location = new System.Drawing.Point(4, 21);
            this.outMealTabPage.Name = "outMealTabPage";
            this.outMealTabPage.Size = new System.Drawing.Size(1381, 65);
            this.outMealTabPage.TabIndex = 3;
            this.outMealTabPage.Text = "特餐旅客";
            this.outMealTabPage.UseVisualStyleBackColor = true;
            // 
            // outMealListView
            // 
            this.outMealListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outMealListView.Location = new System.Drawing.Point(0, 0);
            this.outMealListView.Name = "outMealListView";
            this.outMealListView.Size = new System.Drawing.Size(1381, 65);
            this.outMealListView.TabIndex = 2;
            this.outMealListView.UseCompatibleStateImageBehavior = false;
            this.outMealListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.outMealListView_ColumnClick);
            // 
            // outJINTabPage
            // 
            this.outJINTabPage.Controls.Add(this.outclistView);
            this.outJINTabPage.Location = new System.Drawing.Point(4, 21);
            this.outJINTabPage.Name = "outJINTabPage";
            this.outJINTabPage.Size = new System.Drawing.Size(1381, 65);
            this.outJINTabPage.TabIndex = 4;
            this.outJINTabPage.Text = "常旅客";
            this.outJINTabPage.UseVisualStyleBackColor = true;
            // 
            // outclistView
            // 
            this.outclistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outclistView.Location = new System.Drawing.Point(0, 0);
            this.outclistView.Name = "outclistView";
            this.outclistView.Size = new System.Drawing.Size(1381, 65);
            this.outclistView.TabIndex = 2;
            this.outclistView.UseCompatibleStateImageBehavior = false;
            this.outclistView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.outclistView_ColumnClick);
            // 
            // outTSTabPage
            // 
            this.outTSTabPage.Controls.Add(this.outTSTListView);
            this.outTSTabPage.Location = new System.Drawing.Point(4, 21);
            this.outTSTabPage.Name = "outTSTabPage";
            this.outTSTabPage.Size = new System.Drawing.Size(1381, 65);
            this.outTSTabPage.TabIndex = 5;
            this.outTSTabPage.Text = "中转旅客";
            this.outTSTabPage.UseVisualStyleBackColor = true;
            // 
            // outTSTListView
            // 
            this.outTSTListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outTSTListView.Location = new System.Drawing.Point(0, 0);
            this.outTSTListView.Name = "outTSTListView";
            this.outTSTListView.Size = new System.Drawing.Size(1381, 65);
            this.outTSTListView.TabIndex = 2;
            this.outTSTListView.UseCompatibleStateImageBehavior = false;
            this.outTSTListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.outTSTListView_ColumnClick);
            // 
            // outOTHTabPage
            // 
            this.outOTHTabPage.Controls.Add(this.outplistView);
            this.outOTHTabPage.Location = new System.Drawing.Point(4, 21);
            this.outOTHTabPage.Name = "outOTHTabPage";
            this.outOTHTabPage.Size = new System.Drawing.Size(1381, 65);
            this.outOTHTabPage.TabIndex = 6;
            this.outOTHTabPage.Text = "普通旅客";
            this.outOTHTabPage.UseVisualStyleBackColor = true;
            // 
            // outplistView
            // 
            this.outplistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outplistView.Location = new System.Drawing.Point(0, 0);
            this.outplistView.Name = "outplistView";
            this.outplistView.Size = new System.Drawing.Size(1381, 65);
            this.outplistView.TabIndex = 2;
            this.outplistView.UseCompatibleStateImageBehavior = false;
            this.outplistView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.outplistView_ColumnClick);
            // 
            // outTaskBooktabPage
            // 
            this.outTaskBooktabPage.Controls.Add(this.outtabControl_Taskbook);
            this.outTaskBooktabPage.Location = new System.Drawing.Point(4, 21);
            this.outTaskBooktabPage.Name = "outTaskBooktabPage";
            this.outTaskBooktabPage.Size = new System.Drawing.Size(1395, 96);
            this.outTaskBooktabPage.TabIndex = 1;
            this.outTaskBooktabPage.Text = "任务书";
            this.outTaskBooktabPage.UseVisualStyleBackColor = true;
            // 
            // outtabControl_Taskbook
            // 
            this.outtabControl_Taskbook.Controls.Add(this.outTaskbookAnalyseTabPage);
            this.outtabControl_Taskbook.Controls.Add(this.outTaskbookCompareTabPage);
            this.outtabControl_Taskbook.Controls.Add(this.outTaskbookOriginalTabPage);
            this.outtabControl_Taskbook.Controls.Add(this.outTaskbookConfiguration);
            this.outtabControl_Taskbook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outtabControl_Taskbook.Location = new System.Drawing.Point(0, 0);
            this.outtabControl_Taskbook.Name = "outtabControl_Taskbook";
            this.outtabControl_Taskbook.SelectedIndex = 0;
            this.outtabControl_Taskbook.Size = new System.Drawing.Size(1395, 96);
            this.outtabControl_Taskbook.TabIndex = 1;
            this.outtabControl_Taskbook.SelectedIndexChanged += new System.EventHandler(this.outtabControl_Taskbook_SelectedIndexChanged);
            // 
            // outTaskbookAnalyseTabPage
            // 
            this.outTaskbookAnalyseTabPage.Controls.Add(this.dataGridView2);
            this.outTaskbookAnalyseTabPage.Location = new System.Drawing.Point(4, 21);
            this.outTaskbookAnalyseTabPage.Name = "outTaskbookAnalyseTabPage";
            this.outTaskbookAnalyseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outTaskbookAnalyseTabPage.Size = new System.Drawing.Size(1387, 71);
            this.outTaskbookAnalyseTabPage.TabIndex = 0;
            this.outTaskbookAnalyseTabPage.Text = "分析（每人一行）";
            this.outTaskbookAnalyseTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1381, 65);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView2_SortCompare);
            this.dataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
            // 
            // outTaskbookCompareTabPage
            // 
            this.outTaskbookCompareTabPage.Controls.Add(this.dataGridView4);
            this.outTaskbookCompareTabPage.Location = new System.Drawing.Point(4, 21);
            this.outTaskbookCompareTabPage.Name = "outTaskbookCompareTabPage";
            this.outTaskbookCompareTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outTaskbookCompareTabPage.Size = new System.Drawing.Size(1387, 71);
            this.outTaskbookCompareTabPage.TabIndex = 1;
            this.outTaskbookCompareTabPage.Text = "前后查询比较";
            this.outTaskbookCompareTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(3, 3);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.RowTemplate.Height = 23;
            this.dataGridView4.Size = new System.Drawing.Size(1381, 65);
            this.dataGridView4.TabIndex = 1;
            this.dataGridView4.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView2_SortCompare);
            this.dataGridView4.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
            // 
            // outTaskbookOriginalTabPage
            // 
            this.outTaskbookOriginalTabPage.Controls.Add(this.webBrowser2);
            this.outTaskbookOriginalTabPage.Location = new System.Drawing.Point(4, 21);
            this.outTaskbookOriginalTabPage.Name = "outTaskbookOriginalTabPage";
            this.outTaskbookOriginalTabPage.Size = new System.Drawing.Size(1387, 71);
            this.outTaskbookOriginalTabPage.TabIndex = 2;
            this.outTaskbookOriginalTabPage.Text = "任务书(运行网)";
            this.outTaskbookOriginalTabPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser2.Location = new System.Drawing.Point(0, 0);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(1387, 71);
            this.webBrowser2.TabIndex = 2;
            // 
            // outTaskbookConfiguration
            // 
            this.outTaskbookConfiguration.Controls.Add(this.chkoutTaskbookConfiguration_NewLine);
            this.outTaskbookConfiguration.Location = new System.Drawing.Point(4, 21);
            this.outTaskbookConfiguration.Name = "outTaskbookConfiguration";
            this.outTaskbookConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.outTaskbookConfiguration.Size = new System.Drawing.Size(1387, 71);
            this.outTaskbookConfiguration.TabIndex = 4;
            this.outTaskbookConfiguration.Text = "配置";
            this.outTaskbookConfiguration.UseVisualStyleBackColor = true;
            // 
            // chkoutTaskbookConfiguration_NewLine
            // 
            this.chkoutTaskbookConfiguration_NewLine.AutoSize = true;
            this.chkoutTaskbookConfiguration_NewLine.Location = new System.Drawing.Point(36, 22);
            this.chkoutTaskbookConfiguration_NewLine.Name = "chkoutTaskbookConfiguration_NewLine";
            this.chkoutTaskbookConfiguration_NewLine.Size = new System.Drawing.Size(72, 16);
            this.chkoutTaskbookConfiguration_NewLine.TabIndex = 1;
            this.chkoutTaskbookConfiguration_NewLine.Text = "网格换行";
            this.chkoutTaskbookConfiguration_NewLine.UseVisualStyleBackColor = true;
            this.chkoutTaskbookConfiguration_NewLine.CheckedChanged += new System.EventHandler(this.chkoutTaskbookConfiguration_NewLine_CheckedChanged);
            // 
            // gantabPage
            // 
            this.gantabPage.Controls.Add(this.ganttwebBrowser);
            this.gantabPage.Location = new System.Drawing.Point(4, 21);
            this.gantabPage.Name = "gantabPage";
            this.gantabPage.Size = new System.Drawing.Size(1403, 121);
            this.gantabPage.TabIndex = 6;
            this.gantabPage.Text = "航班甘特图（暂不使用）";
            this.gantabPage.UseVisualStyleBackColor = true;
            // 
            // ganttwebBrowser
            // 
            this.ganttwebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ganttwebBrowser.Location = new System.Drawing.Point(0, 0);
            this.ganttwebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ganttwebBrowser.Name = "ganttwebBrowser";
            this.ganttwebBrowser.Size = new System.Drawing.Size(1403, 121);
            this.ganttwebBrowser.TabIndex = 0;
            // 
            // tabPageDataItem
            // 
            this.tabPageDataItem.Controls.Add(this.ganttViewToolBox);
            this.tabPageDataItem.Location = new System.Drawing.Point(4, 21);
            this.tabPageDataItem.Name = "tabPageDataItem";
            this.tabPageDataItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDataItem.Size = new System.Drawing.Size(1403, 121);
            this.tabPageDataItem.TabIndex = 7;
            this.tabPageDataItem.Text = "保障展示";
            this.tabPageDataItem.UseVisualStyleBackColor = true;
            // 
            // ganttViewToolBox
            // 
            this.ganttViewToolBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ganttViewToolBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ganttViewToolBox.DTINSTD = new System.DateTime(((long)(0)));
            this.ganttViewToolBox.DTOUTSTD = new System.DateTime(((long)(0)));
            this.ganttViewToolBox.HeaderLineHeight = 20;
            this.ganttViewToolBox.Location = new System.Drawing.Point(3, 0);
            this.ganttViewToolBox.MinuteWidth = 6;
            this.ganttViewToolBox.Name = "ganttViewToolBox";
            this.ganttViewToolBox.NodeHeight = 10;
            this.ganttViewToolBox.Size = new System.Drawing.Size(1042, 121);
            this.ganttViewToolBox.TabIndex = 0;
            this.ganttViewToolBox.VerticalSpace = 25;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox15);
            this.panel2.Controls.Add(this.checkBox14);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.checkBox13);
            this.panel2.Controls.Add(this.checkBox12);
            this.panel2.Controls.Add(this.checkBox11);
            this.panel2.Controls.Add(this.checkBox6);
            this.panel2.Controls.Add(this.checkBox7);
            this.panel2.Controls.Add(this.checkBox8);
            this.panel2.Controls.Add(this.checkBox9);
            this.panel2.Controls.Add(this.checkBox10);
            this.panel2.Controls.Add(this.checkBox5);
            this.panel2.Controls.Add(this.checkBox4);
            this.panel2.Controls.Add(this.checkBox3);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.dtFlightDate);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.UpDownValue);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbStations);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 146);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1411, 35);
            this.panel2.TabIndex = 106;
            // 
            // checkBox14
            // 
            this.checkBox14.AutoSize = true;
            this.checkBox14.Checked = true;
            this.checkBox14.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox14.Location = new System.Drawing.Point(1073, 10);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(36, 16);
            this.checkBox14.TabIndex = 132;
            this.checkBox14.Text = "9H";
            this.checkBox14.UseVisualStyleBackColor = true;
            this.checkBox14.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(1161, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 23);
            this.button2.TabIndex = 131;
            this.button2.Text = "刷 新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(1312, 10);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 130;
            this.radioButton2.Text = "历史库";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(1240, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 129;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "生产库";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label44
            // 
            this.label44.ForeColor = System.Drawing.Color.Blue;
            this.label44.Location = new System.Drawing.Point(1375, 10);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(34, 20);
            this.label44.TabIndex = 128;
            this.label44.Text = "（请点击 刷新 按钮）";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label44.Visible = false;
            this.label44.Click += new System.EventHandler(this.label44_Click);
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Checked = true;
            this.checkBox13.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox13.Location = new System.Drawing.Point(1034, 10);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(36, 16);
            this.checkBox13.TabIndex = 127;
            this.checkBox13.Text = "GX";
            this.checkBox13.UseVisualStyleBackColor = true;
            this.checkBox13.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Checked = true;
            this.checkBox12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox12.Location = new System.Drawing.Point(993, 10);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(36, 16);
            this.checkBox12.TabIndex = 126;
            this.checkBox12.Text = "AW";
            this.checkBox12.UseVisualStyleBackColor = true;
            this.checkBox12.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Checked = true;
            this.checkBox11.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox11.Location = new System.Drawing.Point(953, 10);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(36, 16);
            this.checkBox11.TabIndex = 125;
            this.checkBox11.Text = "UO";
            this.checkBox11.UseVisualStyleBackColor = true;
            this.checkBox11.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Checked = true;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox6.Location = new System.Drawing.Point(743, 10);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(36, 16);
            this.checkBox6.TabIndex = 124;
            this.checkBox6.Text = "8L";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Checked = true;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7.Location = new System.Drawing.Point(785, 10);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(36, 16);
            this.checkBox7.TabIndex = 123;
            this.checkBox7.Text = "Y8";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Checked = true;
            this.checkBox8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox8.Location = new System.Drawing.Point(827, 10);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(36, 16);
            this.checkBox8.TabIndex = 122;
            this.checkBox8.Text = "FU";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Checked = true;
            this.checkBox9.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox9.Location = new System.Drawing.Point(869, 10);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(36, 16);
            this.checkBox9.TabIndex = 121;
            this.checkBox9.Text = "UQ";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Checked = true;
            this.checkBox10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox10.Location = new System.Drawing.Point(911, 10);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(36, 16);
            this.checkBox10.TabIndex = 120;
            this.checkBox10.Text = "HX";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(701, 10);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(36, 16);
            this.checkBox5.TabIndex = 119;
            this.checkBox5.Text = "PN";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(659, 10);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(36, 16);
            this.checkBox4.TabIndex = 118;
            this.checkBox4.Text = "GS";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(617, 10);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(36, 16);
            this.checkBox3.TabIndex = 117;
            this.checkBox3.Text = "JD";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(575, 10);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(36, 16);
            this.checkBox2.TabIndex = 116;
            this.checkBox2.Text = "CN";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(535, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(36, 16);
            this.checkBox1.TabIndex = 115;
            this.checkBox1.Text = "HU";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label43
            // 
            this.label43.Location = new System.Drawing.Point(495, 8);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(44, 20);
            this.label43.TabIndex = 114;
            this.label43.Text = "公司：";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1411, 3);
            this.splitter1.TabIndex = 111;
            this.splitter1.TabStop = false;
            // 
            // dtFlightDate
            // 
            this.dtFlightDate.Location = new System.Drawing.Point(210, 7);
            this.dtFlightDate.Name = "dtFlightDate";
            this.dtFlightDate.Size = new System.Drawing.Size(144, 21);
            this.dtFlightDate.TabIndex = 110;
            this.dtFlightDate.TabStop = false;
            this.dtFlightDate.ValueChanged += new System.EventHandler(this.dtFlightDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(170, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 109;
            this.label3.Text = "日期";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(360, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 108;
            this.label2.Text = "放大/缩小";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UpDownValue
            // 
            this.UpDownValue.DecimalPlaces = 2;
            this.UpDownValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UpDownValue.Location = new System.Drawing.Point(424, 7);
            this.UpDownValue.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.UpDownValue.Name = "UpDownValue";
            this.UpDownValue.Size = new System.Drawing.Size(60, 21);
            this.UpDownValue.TabIndex = 107;
            this.UpDownValue.TabStop = false;
            this.UpDownValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownValue.ValueChanged += new System.EventHandler(this.UpDownValue_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 105;
            this.label1.Text = "基地";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbStations
            // 
            this.cmbStations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStations.Location = new System.Drawing.Point(48, 8);
            this.cmbStations.Name = "cmbStations";
            this.cmbStations.Size = new System.Drawing.Size(121, 20);
            this.cmbStations.TabIndex = 106;
            this.cmbStations.TabStop = false;
            this.cmbStations.SelectionChangeCommitted += new System.EventHandler(this.cmbStations_SelectionChangeCommitted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.label16, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.label17, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label18, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label19, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label20, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label21, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label22, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label23, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label24, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label25, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label26, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label27, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label28, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label29, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label30, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label31, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label32, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label33, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label34, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label35, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label36, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label37, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label38, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label39, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(709, 114);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(589, 85);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(116, 28);
            this.label16.TabIndex = 23;
            this.label16.Text = "label16";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(355, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 28);
            this.label17.TabIndex = 22;
            this.label17.Text = "label17";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(121, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(110, 28);
            this.label18.TabIndex = 21;
            this.label18.Text = "label18";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(589, 57);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(116, 27);
            this.label19.TabIndex = 20;
            this.label19.Text = "label19";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Location = new System.Drawing.Point(355, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(110, 27);
            this.label20.TabIndex = 19;
            this.label20.Text = "label20";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Location = new System.Drawing.Point(121, 57);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 27);
            this.label21.TabIndex = 18;
            this.label21.Text = "label21";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Location = new System.Drawing.Point(589, 29);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(116, 27);
            this.label22.TabIndex = 17;
            this.label22.Text = "label22";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Location = new System.Drawing.Point(355, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(110, 27);
            this.label23.TabIndex = 16;
            this.label23.Text = "label23";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Location = new System.Drawing.Point(121, 29);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(110, 27);
            this.label24.TabIndex = 15;
            this.label24.Text = "label24";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Location = new System.Drawing.Point(589, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(116, 27);
            this.label25.TabIndex = 14;
            this.label25.Text = "label25";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Location = new System.Drawing.Point(121, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(110, 27);
            this.label26.TabIndex = 13;
            this.label26.Text = "label26";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Location = new System.Drawing.Point(4, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(110, 27);
            this.label27.TabIndex = 1;
            this.label27.Text = "订座人数/已值机人数";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Location = new System.Drawing.Point(4, 29);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(110, 27);
            this.label28.TabIndex = 0;
            this.label28.Text = "特殊/特服";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Location = new System.Drawing.Point(4, 57);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(110, 27);
            this.label29.TabIndex = 2;
            this.label29.Text = "F/Y舱";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Location = new System.Drawing.Point(4, 85);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(110, 28);
            this.label30.TabIndex = 3;
            this.label30.Text = "特殊餐食";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Location = new System.Drawing.Point(238, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(110, 27);
            this.label31.TabIndex = 4;
            this.label31.Text = "VIP/M6";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Location = new System.Drawing.Point(238, 29);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(110, 27);
            this.label32.TabIndex = 5;
            this.label32.Text = "金鹏卡";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Location = new System.Drawing.Point(238, 57);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(110, 27);
            this.label33.TabIndex = 6;
            this.label33.Text = "金鹿卡";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Location = new System.Drawing.Point(238, 85);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(110, 28);
            this.label34.TabIndex = 7;
            this.label34.Text = "儿童";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Location = new System.Drawing.Point(472, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(110, 27);
            this.label35.TabIndex = 8;
            this.label35.Text = "生日旅客";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Location = new System.Drawing.Point(472, 29);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(110, 27);
            this.label36.TabIndex = 9;
            this.label36.Text = "中转旅客(转入/转出)";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Location = new System.Drawing.Point(472, 57);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(110, 27);
            this.label37.TabIndex = 10;
            this.label37.Text = "婴儿";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Location = new System.Drawing.Point(472, 85);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(110, 28);
            this.label38.TabIndex = 11;
            this.label38.Text = "登机口";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label39.Location = new System.Drawing.Point(355, 1);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(110, 27);
            this.label39.TabIndex = 12;
            this.label39.Text = "label39";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Controls.Add(this.label40, 5, 3);
            this.tableLayoutPanel2.Controls.Add(this.label41, 3, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label40.Location = new System.Drawing.Point(169, 64);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(27, 35);
            this.label40.TabIndex = 23;
            this.label40.Text = "label40";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Location = new System.Drawing.Point(103, 64);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(26, 35);
            this.label41.TabIndex = 22;
            this.label41.Text = "label41";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Location = new System.Drawing.Point(122, 85);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(111, 28);
            this.label42.TabIndex = 21;
            this.label42.Text = "label42";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(200, 100);
            this.tabPage1.TabIndex = 0;
            // 
            // checkBox15
            // 
            this.checkBox15.AutoSize = true;
            this.checkBox15.Checked = true;
            this.checkBox15.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox15.Location = new System.Drawing.Point(1114, 10);
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size(36, 16);
            this.checkBox15.TabIndex = 133;
            this.checkBox15.Text = "GT";
            this.checkBox15.UseVisualStyleBackColor = true;
            this.checkBox15.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // fmFlightGuarantee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 442);
            this.Controls.Add(this.splitContainer1);
            this.Name = "fmFlightGuarantee";
            this.Text = "航站保障";
            this.Load += new System.EventHandler(this.fmFlightGuarantee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timerElapsed)).EndInit();
            this.popOutFlightNoMenu.ResumeLayout(false);
            this.popInFlightNoMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shYestoday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shToday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shTomorrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shSelectDate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.dataTabControlGanttView.ResumeLayout(false);
            this.msgTabPage.ResumeLayout(false);
            this.inTabPage.ResumeLayout(false);
            this.intabControl.ResumeLayout(false);
            this.inPSRtabPage.ResumeLayout(false);
            this.intabControl_PSR.ResumeLayout(false);
            this.inSumTabPage.ResumeLayout(false);
            this.inTableLayoutPanel.ResumeLayout(false);
            this.inTableLayoutPanel.PerformLayout();
            this.inVipTabPage.ResumeLayout(false);
            this.inSSTabPage.ResumeLayout(false);
            this.inMealTabPage.ResumeLayout(false);
            this.inJINTabPage.ResumeLayout(false);
            this.inTSTabPage.ResumeLayout(false);
            this.inOTHTabPage.ResumeLayout(false);
            this.inTaskBooktabPage.ResumeLayout(false);
            this.intabControl_Taskbook.ResumeLayout(false);
            this.inTaskbookAnalyseTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.inTaskbookCompareTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.inTaskbookOriginalTabPage.ResumeLayout(false);
            this.inTaskbookConfiguration.ResumeLayout(false);
            this.inTaskbookConfiguration.PerformLayout();
            this.outTabPage.ResumeLayout(false);
            this.outtabControl.ResumeLayout(false);
            this.outPSRtabPage.ResumeLayout(false);
            this.outTabControl_PSR.ResumeLayout(false);
            this.outSumTabPage.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.outVIPTabPage.ResumeLayout(false);
            this.outSSTabPage.ResumeLayout(false);
            this.outMealTabPage.ResumeLayout(false);
            this.outJINTabPage.ResumeLayout(false);
            this.outTSTabPage.ResumeLayout(false);
            this.outOTHTabPage.ResumeLayout(false);
            this.outTaskBooktabPage.ResumeLayout(false);
            this.outtabControl_Taskbook.ResumeLayout(false);
            this.outTaskbookAnalyseTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.outTaskbookCompareTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.outTaskbookOriginalTabPage.ResumeLayout(false);
            this.outTaskbookConfiguration.ResumeLayout(false);
            this.outTaskbookConfiguration.PerformLayout();
            this.gantabPage.ResumeLayout(false);
            this.tabPageDataItem.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownValue)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Timers.Timer timerElapsed;
        private System.Windows.Forms.Timer timerFlightNum;
        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.Timer timerChange;
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private FarPoint.Win.Spread.FpSpread fpFlightInfo;
        private FarPoint.Win.Spread.SheetView shYestoday;
        private FarPoint.Win.Spread.SheetView shToday;
        private FarPoint.Win.Spread.SheetView shTomorrow;
        private FarPoint.Win.Spread.SheetView shSelectDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtFlightDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UpDownValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStations;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl dataTabControlGanttView;
        private System.Windows.Forms.TabPage outTabPage;
        private System.Windows.Forms.TabPage inTabPage;
        private System.Windows.Forms.TabPage msgTabPage;
        private System.Windows.Forms.ListView lvChangeContent;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage gantabPage;
        private System.Windows.Forms.TabPage inPSRtabPage;
        private System.Windows.Forms.TabControl outtabControl;
        private System.Windows.Forms.TabPage outPSRtabPage;
        private System.Windows.Forms.TabControl outTabControl_PSR;
        private System.Windows.Forms.TabPage outSumTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label outboardlabel;
        private System.Windows.Forms.Label outchildlabel;
        private System.Windows.Forms.Label outmeallabel;
        private System.Windows.Forms.Label outbabylabel;
        private System.Windows.Forms.Label outjplabel;
        private System.Windows.Forms.Label outFYlabel;
        private System.Windows.Forms.Label outtstlabel;
        private System.Windows.Forms.Label outjinlabel;
        private System.Windows.Forms.Label outsslabel;
        private System.Windows.Forms.Label outbirthlabel;
        private System.Windows.Forms.Label outsumlabel;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label outviplabel;
        private System.Windows.Forms.TabPage outVIPTabPage;
        private System.Windows.Forms.ListView outVIPListView;
        private System.Windows.Forms.TabPage outSSTabPage;
        private System.Windows.Forms.ListView outSSListView;
        private System.Windows.Forms.TabPage outMealTabPage;
        private System.Windows.Forms.ListView outMealListView;
        private System.Windows.Forms.TabPage outJINTabPage;
        private System.Windows.Forms.ListView outclistView;
        private System.Windows.Forms.TabPage outTSTabPage;
        private System.Windows.Forms.ListView outTSTListView;
        private System.Windows.Forms.TabPage outOTHTabPage;
        private System.Windows.Forms.ListView outplistView;
        private System.Windows.Forms.WebBrowser ganttwebBrowser;
        private System.Windows.Forms.TabPage inTaskBooktabPage;
        private System.Windows.Forms.TabControl intabControl_Taskbook;
        private System.Windows.Forms.TabPage inTaskbookAnalyseTabPage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage inTaskbookCompareTabPage;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TabControl intabControl;
        private System.Windows.Forms.TabPage outTaskBooktabPage;
        private System.Windows.Forms.TabControl outtabControl_Taskbook;
        private System.Windows.Forms.TabPage outTaskbookAnalyseTabPage;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage outTaskbookCompareTabPage;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.TabPage inTaskbookOriginalTabPage;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabPage outTaskbookOriginalTabPage;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.TabControl intabControl_PSR;
        private System.Windows.Forms.TabPage inSumTabPage;
        private System.Windows.Forms.TabPage inVipTabPage;
        private System.Windows.Forms.TableLayoutPanel inTableLayoutPanel;
        private System.Windows.Forms.Label inboardlabel;
        private System.Windows.Forms.Label inchildlabel;
        private System.Windows.Forms.Label inmeallabel;
        private System.Windows.Forms.Label inbabylabel;
        private System.Windows.Forms.Label injplabel;
        private System.Windows.Forms.Label inFYlabel;
        private System.Windows.Forms.Label intstlabel;
        private System.Windows.Forms.Label injinlabel;
        private System.Windows.Forms.Label insslabel;
        private System.Windows.Forms.Label inbirthlabel;
        private System.Windows.Forms.Label insumlabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label inviplabel;
        private System.Windows.Forms.TabPage inSSTabPage;
        private System.Windows.Forms.TabPage inMealTabPage;
        private System.Windows.Forms.TabPage inJINTabPage;
        private System.Windows.Forms.TabPage inTSTabPage;
        private System.Windows.Forms.TabPage inOTHTabPage;
        private System.Windows.Forms.ListView intVIPListView;
        private System.Windows.Forms.ListView inSSListView;
        private System.Windows.Forms.ListView inMealListView;
        private System.Windows.Forms.ListView inclistView;
        private System.Windows.Forms.ListView inTSTListView;
        private System.Windows.Forms.ListView inplistView;
        private System.Windows.Forms.TabPage inTaskbookConfiguration;
        private System.Windows.Forms.CheckBox chkinTaskbookConfiguration_NewLine;
        private System.Windows.Forms.TabPage outTaskbookConfiguration;
        private System.Windows.Forms.CheckBox chkoutTaskbookConfiguration_NewLine;
        private System.Windows.Forms.TabPage tabPageDataItem;
        private fmGanttView ganttViewToolBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.CheckBox checkBox14;
        private System.Windows.Forms.CheckBox checkBox15;
    }
}