namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmFlightMoni
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
            this.fpsFlightMon = new FarPoint.Win.Spread.FpSpread();
            this.shFlightMon = new FarPoint.Win.Spread.SheetView();
            this.timerChangeRecord = new System.Windows.Forms.Timer(this.components);
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fpsFlightMon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shFlightMon)).BeginInit();
            this.SuspendLayout();
            // 
            // fpsFlightMon
            // 
            this.fpsFlightMon.About = "2.5.2007.2005";
            this.fpsFlightMon.AccessibleDescription = "fpsFlightMon";
            this.fpsFlightMon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsFlightMon.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsFlightMon.Location = new System.Drawing.Point(0, 0);
            this.fpsFlightMon.Name = "fpsFlightMon";
            this.fpsFlightMon.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shFlightMon});
            this.fpsFlightMon.Size = new System.Drawing.Size(292, 273);
            this.fpsFlightMon.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpsFlightMon.TextTipAppearance = tipAppearance1;
            this.fpsFlightMon.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsFlightMon.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpsFlightMon_CellDoubleClick);
            // 
            // shFlightMon
            // 
            this.shFlightMon.Reset();
            this.shFlightMon.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shFlightMon.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shFlightMon.ColumnCount = 0;
            this.shFlightMon.ColumnHeader.RowCount = 2;
            this.shFlightMon.RowCount = 0;
            this.shFlightMon.AutoGenerateColumns = false;
            this.shFlightMon.DataAutoCellTypes = false;
            this.shFlightMon.DataAutoHeadings = false;
            this.shFlightMon.DataAutoSizeColumns = false;
            this.shFlightMon.DefaultStyle.Border = lineBorder1;
            this.shFlightMon.DefaultStyle.Locked = false;
            this.shFlightMon.DefaultStyle.Parent = "DataAreaDefault";
            this.shFlightMon.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shFlightMon.ZoomFactor = 0.96F;
            this.shFlightMon.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpsFlightMon.SetActiveViewport(1, 1);
            // 
            // timerChangeRecord
            // 
            this.timerChangeRecord.Tick += new System.EventHandler(this.timerChangeRecord_Tick);
            // 
            // timerSplash
            // 
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // fmFlightMoni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ControlBox = false;
            this.Controls.Add(this.fpsFlightMon);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmFlightMoni";
            this.Text = "º½°à¼à¿Ø";
            this.Load += new System.EventHandler(this.fmFlightMoni_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsFlightMon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shFlightMon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpsFlightMon;
        private FarPoint.Win.Spread.SheetView shFlightMon;
        private System.Windows.Forms.Timer timerChangeRecord;
        private System.Windows.Forms.Timer timerSplash;
    }
}