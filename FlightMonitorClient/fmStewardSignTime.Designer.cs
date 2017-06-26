namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmStewardSignTime
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpsGetStewardSignIn = new FarPoint.Win.Spread.FpSpread();
            this.fpsGetStewardSignIn_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetStewardSignIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetStewardSignIn_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpsGetStewardSignIn
            // 
            this.fpsGetStewardSignIn.About = "2.5.2007.2005";
            this.fpsGetStewardSignIn.AccessibleDescription = "fpsGetStewardSignIn, Sheet1";
            this.fpsGetStewardSignIn.BackColor = System.Drawing.SystemColors.Control;
            this.fpsGetStewardSignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsGetStewardSignIn.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsGetStewardSignIn.Location = new System.Drawing.Point(0, 0);
            this.fpsGetStewardSignIn.Name = "fpsGetStewardSignIn";
            this.fpsGetStewardSignIn.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpsGetStewardSignIn_Sheet1});
            this.fpsGetStewardSignIn.Size = new System.Drawing.Size(384, 109);
            this.fpsGetStewardSignIn.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpsGetStewardSignIn.TextTipAppearance = tipAppearance2;
            this.fpsGetStewardSignIn.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpsGetStewardSignIn_Sheet1
            // 
            this.fpsGetStewardSignIn_Sheet1.Reset();
            this.fpsGetStewardSignIn_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpsGetStewardSignIn_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpsGetStewardSignIn_Sheet1.ColumnCount = 4;
            this.fpsGetStewardSignIn_Sheet1.RowCount = 0;
            this.fpsGetStewardSignIn_Sheet1.AutoGenerateColumns = false;
            this.fpsGetStewardSignIn_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.fpsGetStewardSignIn_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "卡系列号";
            this.fpsGetStewardSignIn_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "卡编号";
            this.fpsGetStewardSignIn_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "签到时间";
            this.fpsGetStewardSignIn_Sheet1.Columns.Get(1).Label = "卡系列号";
            this.fpsGetStewardSignIn_Sheet1.Columns.Get(1).Width = 80F;
            this.fpsGetStewardSignIn_Sheet1.Columns.Get(2).Label = "卡编号";
            this.fpsGetStewardSignIn_Sheet1.Columns.Get(2).Width = 80F;
            this.fpsGetStewardSignIn_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.fpsGetStewardSignIn_Sheet1.Columns.Get(3).Label = "签到时间";
            this.fpsGetStewardSignIn_Sheet1.Columns.Get(3).Width = 100F;
            this.fpsGetStewardSignIn_Sheet1.DataAutoSizeColumns = false;
            this.fpsGetStewardSignIn_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpsGetStewardSignIn_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpsGetStewardSignIn_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpsGetStewardSignIn_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.None;
            this.fpsGetStewardSignIn_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpsGetStewardSignIn_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpsGetStewardSignIn.SetActiveViewport(1, 0);
            // 
            // fmStewardSignTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 109);
            this.Controls.Add(this.fpsGetStewardSignIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmStewardSignTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "乘务签到记录";
            this.Load += new System.EventHandler(this.fmStewardSignTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetStewardSignIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetStewardSignIn_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpsGetStewardSignIn;
        private FarPoint.Win.Spread.SheetView fpsGetStewardSignIn_Sheet1;
    }
}