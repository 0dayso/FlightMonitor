namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmFlightDisp
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
            this.fpsFlightDisp = new FarPoint.Win.Spread.FpSpread();
            this.shFlightDisp = new FarPoint.Win.Spread.SheetView();
            this.timerChangeRecord = new System.Windows.Forms.Timer(this.components);
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fpsFlightDisp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shFlightDisp)).BeginInit();
            this.SuspendLayout();
            // 
            // fpsFlightDisp
            // 
            this.fpsFlightDisp.About = "2.5.2007.2005";
            this.fpsFlightDisp.AccessibleDescription = "fpsFlightDisp";
            this.fpsFlightDisp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsFlightDisp.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsFlightDisp.Location = new System.Drawing.Point(0, 0);
            this.fpsFlightDisp.Name = "fpsFlightDisp";
            this.fpsFlightDisp.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shFlightDisp});
            this.fpsFlightDisp.Size = new System.Drawing.Size(292, 273);
            this.fpsFlightDisp.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpsFlightDisp.TextTipAppearance = tipAppearance1;
            this.fpsFlightDisp.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsFlightDisp.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpsFlightDisp_CellDoubleClick);
            // 
            // shFlightDisp
            // 
            this.shFlightDisp.Reset();
            this.shFlightDisp.SheetName = "ShToday";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shFlightDisp.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shFlightDisp.ColumnCount = 0;
            this.shFlightDisp.ColumnHeader.RowCount = 2;
            this.shFlightDisp.RowCount = 0;
            this.shFlightDisp.AutoGenerateColumns = false;
            this.shFlightDisp.DataAutoCellTypes = false;
            this.shFlightDisp.DataAutoHeadings = false;
            this.shFlightDisp.DataAutoSizeColumns = false;
            this.shFlightDisp.DefaultStyle.Border = lineBorder1;
            this.shFlightDisp.DefaultStyle.Locked = false;
            this.shFlightDisp.DefaultStyle.Parent = "DataAreaDefault";
            this.shFlightDisp.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shFlightDisp.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shFlightDisp.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shFlightDisp.ZoomFactor = 0.96F;
            this.shFlightDisp.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpsFlightDisp.SetActiveViewport(1, 1);
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
            // fmFlightDisp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ControlBox = false;
            this.Controls.Add(this.fpsFlightDisp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmFlightDisp";
            this.Text = "º½°à·ÅÐÐ";
            this.Load += new System.EventHandler(this.fmFlightDisp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsFlightDisp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shFlightDisp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpsFlightDisp;
        private FarPoint.Win.Spread.SheetView shFlightDisp;
        private System.Windows.Forms.Timer timerChangeRecord;
        private System.Windows.Forms.Timer timerSplash;
    }
}