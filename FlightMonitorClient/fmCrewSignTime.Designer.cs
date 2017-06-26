namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmCrewSignTime
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.fpsGetCrewSignIn_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpsGetCrewSignIn = new FarPoint.Win.Spread.FpSpread();
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetCrewSignIn_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetCrewSignIn)).BeginInit();
            this.SuspendLayout();
            // 
            // fpsGetCrewSignIn_Sheet1
            // 
            this.fpsGetCrewSignIn_Sheet1.Reset();
            this.fpsGetCrewSignIn_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpsGetCrewSignIn_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpsGetCrewSignIn_Sheet1.ColumnCount = 4;
            this.fpsGetCrewSignIn_Sheet1.RowCount = 0;
            this.fpsGetCrewSignIn_Sheet1.AutoGenerateColumns = false;
            this.fpsGetCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.fpsGetCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "卡系列号";
            this.fpsGetCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "卡编号";
            this.fpsGetCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "签到时间";
            this.fpsGetCrewSignIn_Sheet1.Columns.Get(1).Label = "卡系列号";
            this.fpsGetCrewSignIn_Sheet1.Columns.Get(1).Width = 80F;
            this.fpsGetCrewSignIn_Sheet1.Columns.Get(2).Label = "卡编号";
            this.fpsGetCrewSignIn_Sheet1.Columns.Get(2).Width = 80F;
            this.fpsGetCrewSignIn_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.fpsGetCrewSignIn_Sheet1.Columns.Get(3).Label = "签到时间";
            this.fpsGetCrewSignIn_Sheet1.Columns.Get(3).Width = 100F;
            this.fpsGetCrewSignIn_Sheet1.DataAutoSizeColumns = false;
            this.fpsGetCrewSignIn_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpsGetCrewSignIn_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpsGetCrewSignIn_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpsGetCrewSignIn_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.None;
            this.fpsGetCrewSignIn_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpsGetCrewSignIn_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpsGetCrewSignIn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // fpsGetCrewSignIn
            // 
            this.fpsGetCrewSignIn.About = "2.5.2007.2005";
            this.fpsGetCrewSignIn.AccessibleDescription = "fpsGetCrewSignIn, Sheet1";
            this.fpsGetCrewSignIn.BackColor = System.Drawing.SystemColors.Control;
            this.fpsGetCrewSignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsGetCrewSignIn.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsGetCrewSignIn.Location = new System.Drawing.Point(3, 17);
            this.fpsGetCrewSignIn.Name = "fpsGetCrewSignIn";
            this.fpsGetCrewSignIn.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpsGetCrewSignIn_Sheet1});
            this.fpsGetCrewSignIn.Size = new System.Drawing.Size(378, 81);
            this.fpsGetCrewSignIn.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpsGetCrewSignIn.TextTipAppearance = tipAppearance1;
            this.fpsGetCrewSignIn.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fmCrewSignTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 101);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmCrewSignTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机组签到记录";
            this.Load += new System.EventHandler(this.fmCrewSignTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetCrewSignIn_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpsGetCrewSignIn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.SheetView fpsGetCrewSignIn_Sheet1;
        private FarPoint.Win.Spread.FpSpread fpsGetCrewSignIn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}