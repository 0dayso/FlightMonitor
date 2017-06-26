namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmFlightWatch
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
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.fpFlightInfo = new FarPoint.Win.Spread.FpSpread();
            this.shToday = new FarPoint.Win.Spread.SheetView();
            this.timerChangeRecord = new System.Windows.Forms.Timer(this.components);
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.popOutFlightNoMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miOutAircraftFlights = new System.Windows.Forms.ToolStripMenuItem();
            this.miOutChangeData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ֵ����ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��ת�����ÿ���ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�ÿ�����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.popInFlightNoMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miInAircraftFlights = new System.Windows.Forms.ToolStripMenuItem();
            this.miInChangeData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ֵ����ϢToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.��ת�����ÿ���ϢToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.�ÿ�����ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shToday)).BeginInit();
            this.popOutFlightNoMenu.SuspendLayout();
            this.popInFlightNoMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpFlightInfo
            // 
            this.fpFlightInfo.About = "2.5.2007.2005";
            this.fpFlightInfo.AccessibleDescription = "fpFlightInfo";
            this.fpFlightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFlightInfo.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpFlightInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.Location = new System.Drawing.Point(0, 0);
            this.fpFlightInfo.Name = "fpFlightInfo";
            this.fpFlightInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shToday});
            this.fpFlightInfo.Size = new System.Drawing.Size(820, 471);
            this.fpFlightInfo.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFlightInfo.TextTipAppearance = tipAppearance1;
            this.fpFlightInfo.TextTipDelay = 100;
            this.fpFlightInfo.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Fixed;
            this.fpFlightInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpFlightInfo_CellDoubleClick);
            this.fpFlightInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fpFlightInfo_KeyDown);
            this.fpFlightInfo.ChildViewCreated += new FarPoint.Win.Spread.ChildViewCreatedEventHandler(this.fpFlightInfo_ChildViewCreated);
            this.fpFlightInfo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fpFlightInfo_KeyUp);
            this.fpFlightInfo.TextTipFetch += new FarPoint.Win.Spread.TextTipFetchEventHandler(this.fpFlightInfo_TextTipFetch);
            this.fpFlightInfo.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpFlightInfo_CellClick);
            // 
            // shToday
            // 
            this.shToday.Reset();
            this.shToday.SheetName = "����";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shToday.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shToday.ColumnCount = 0;
            this.shToday.ColumnHeader.RowCount = 2;
            this.shToday.RowCount = 0;
            this.shToday.AutoGenerateColumns = false;
            this.shToday.DataAutoCellTypes = false;
            this.shToday.DataAutoHeadings = false;
            this.shToday.DataAutoSizeColumns = false;
            this.shToday.DefaultStyle.Border = lineBorder1;
            this.shToday.DefaultStyle.Locked = false;
            this.shToday.DefaultStyle.Parent = "DataAreaDefault";
            this.shToday.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shToday.RowHeader.Columns.Default.Resizable = false;
            this.shToday.SelectionBackColor = System.Drawing.Color.White;
            this.shToday.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shToday.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shToday.ZoomFactor = 0.96F;
            this.shToday.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlightInfo.SetActiveViewport(1, 1);
            // 
            // timerChangeRecord
            // 
            this.timerChangeRecord.Tick += new System.EventHandler(this.timerChangeRecord_Tick);
            // 
            // timerSplash
            // 
            this.timerSplash.Interval = 1000;
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // popOutFlightNoMenu
            // 
            this.popOutFlightNoMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOutAircraftFlights,
            this.miOutChangeData,
            this.toolStripMenuItem1,
            this.ֵ����ϢToolStripMenuItem,
            this.��ת�����ÿ���ϢToolStripMenuItem,
            this.�ÿ�����ToolStripMenuItem,
            this.toolStripMenuItem3});
            this.popOutFlightNoMenu.Name = "popOutFlightNoMenu";
            this.popOutFlightNoMenu.Size = new System.Drawing.Size(167, 126);
            // 
            // miOutAircraftFlights
            // 
            this.miOutAircraftFlights.Name = "miOutAircraftFlights";
            this.miOutAircraftFlights.Size = new System.Drawing.Size(166, 22);
            this.miOutAircraftFlights.Text = "������Ϣ";
            this.miOutAircraftFlights.Click += new System.EventHandler(this.miOutAircraftFlights_Click);
            // 
            // miOutChangeData
            // 
            this.miOutChangeData.Name = "miOutChangeData";
            this.miOutChangeData.Size = new System.Drawing.Size(166, 22);
            this.miOutChangeData.Text = "�������";
            this.miOutChangeData.Click += new System.EventHandler(this.miOutChangeData_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // ֵ����ϢToolStripMenuItem
            // 
            this.ֵ����ϢToolStripMenuItem.Name = "ֵ����ϢToolStripMenuItem";
            this.ֵ����ϢToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ֵ����ϢToolStripMenuItem.Text = "ֵ����Ϣ";
            this.ֵ����ϢToolStripMenuItem.Click += new System.EventHandler(this.miOutCheckPax_Click);
            // 
            // ��ת�����ÿ���ϢToolStripMenuItem
            // 
            this.��ת�����ÿ���ϢToolStripMenuItem.Name = "��ת�����ÿ���ϢToolStripMenuItem";
            this.��ת�����ÿ���ϢToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.��ת�����ÿ���ϢToolStripMenuItem.Text = "��ת�����ÿ���Ϣ";
            this.��ת�����ÿ���ϢToolStripMenuItem.Click += new System.EventHandler(this.miOutTransitPax_Click);
            // 
            // �ÿ�����ToolStripMenuItem
            // 
            this.�ÿ�����ToolStripMenuItem.Name = "�ÿ�����ToolStripMenuItem";
            this.�ÿ�����ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.�ÿ�����ToolStripMenuItem.Text = "�ÿ�����";
            this.�ÿ�����ToolStripMenuItem.Click += new System.EventHandler(this.miOutNameList_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(163, 6);
            // 
            // popInFlightNoMenu
            // 
            this.popInFlightNoMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInAircraftFlights,
            this.miInChangeData,
            this.toolStripMenuItem2,
            this.ֵ����ϢToolStripMenuItem1,
            this.��ת�����ÿ���ϢToolStripMenuItem1,
            this.�ÿ�����ToolStripMenuItem1,
            this.toolStripMenuItem4});
            this.popInFlightNoMenu.Name = "popOutFlightNoMenu";
            this.popInFlightNoMenu.Size = new System.Drawing.Size(167, 126);
            // 
            // miInAircraftFlights
            // 
            this.miInAircraftFlights.Name = "miInAircraftFlights";
            this.miInAircraftFlights.Size = new System.Drawing.Size(166, 22);
            this.miInAircraftFlights.Text = "������Ϣ";
            this.miInAircraftFlights.Click += new System.EventHandler(this.miInAircraftFlights_Click);
            // 
            // miInChangeData
            // 
            this.miInChangeData.Name = "miInChangeData";
            this.miInChangeData.Size = new System.Drawing.Size(166, 22);
            this.miInChangeData.Text = "�������";
            this.miInChangeData.Click += new System.EventHandler(this.miInChangeData_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
            // 
            // ֵ����ϢToolStripMenuItem1
            // 
            this.ֵ����ϢToolStripMenuItem1.Name = "ֵ����ϢToolStripMenuItem1";
            this.ֵ����ϢToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.ֵ����ϢToolStripMenuItem1.Text = "ֵ����Ϣ";
            this.ֵ����ϢToolStripMenuItem1.Click += new System.EventHandler(this.miInCheckPax_Click);
            // 
            // ��ת�����ÿ���ϢToolStripMenuItem1
            // 
            this.��ת�����ÿ���ϢToolStripMenuItem1.Name = "��ת�����ÿ���ϢToolStripMenuItem1";
            this.��ת�����ÿ���ϢToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.��ת�����ÿ���ϢToolStripMenuItem1.Text = "��ת�����ÿ���Ϣ";
            this.��ת�����ÿ���ϢToolStripMenuItem1.Click += new System.EventHandler(this.miInTransitPax_Click);
            // 
            // �ÿ�����ToolStripMenuItem1
            // 
            this.�ÿ�����ToolStripMenuItem1.Name = "�ÿ�����ToolStripMenuItem1";
            this.�ÿ�����ToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.�ÿ�����ToolStripMenuItem1.Text = "�ÿ�����";
            this.�ÿ�����ToolStripMenuItem1.Click += new System.EventHandler(this.miInNameList_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(163, 6);
            // 
            // fmFlightWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 471);
            this.Controls.Add(this.fpFlightInfo);
            this.Name = "fmFlightWatch";
            this.Text = "������";
            this.Load += new System.EventHandler(this.fmWatch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shToday)).EndInit();
            this.popOutFlightNoMenu.ResumeLayout(false);
            this.popInFlightNoMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpFlightInfo;
        private FarPoint.Win.Spread.SheetView shToday;
        private System.Windows.Forms.Timer timerChangeRecord;
        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.ContextMenuStrip popOutFlightNoMenu;
        private System.Windows.Forms.ToolStripMenuItem miOutAircraftFlights;
        private System.Windows.Forms.ContextMenuStrip popInFlightNoMenu;
        private System.Windows.Forms.ToolStripMenuItem miInAircraftFlights;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ֵ����ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��ת�����ÿ���ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �ÿ�����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ֵ����ϢToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ��ת�����ÿ���ϢToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem �ÿ�����ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miOutChangeData;
        private System.Windows.Forms.ToolStripMenuItem miInChangeData;
    }
}