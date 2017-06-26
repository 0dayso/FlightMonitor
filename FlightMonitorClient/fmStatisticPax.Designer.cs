namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmStatisticPax
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.dtFlightDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fpPax = new FarPoint.Win.Spread.FpSpread();
            this.fpPax_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPax_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtFlightDate
            // 
            this.dtFlightDate.Location = new System.Drawing.Point(8, 16);
            this.dtFlightDate.Name = "dtFlightDate";
            this.dtFlightDate.Size = new System.Drawing.Size(144, 21);
            this.dtFlightDate.TabIndex = 106;
            this.dtFlightDate.TabStop = false;
            this.dtFlightDate.ValueChanged += new System.EventHandler(this.dtFlightDate_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtFlightDate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 48);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(280, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 21;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(200, 13);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 20;
            this.btnExportExcel.Text = "导出Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Controls.Add(this.btnExportExcel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 259);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 42);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(360, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpPax);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(440, 211);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // fpPax
            // 
            this.fpPax.About = "2.5.2007.2005";
            this.fpPax.AccessibleDescription = "fpPax";
            this.fpPax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPax.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPax.Location = new System.Drawing.Point(3, 17);
            this.fpPax.Name = "fpPax";
            this.fpPax.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPax_Sheet1});
            this.fpPax.Size = new System.Drawing.Size(434, 191);
            this.fpPax.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPax.TextTipAppearance = tipAppearance1;
            // 
            // fpPax_Sheet1
            // 
            this.fpPax_Sheet1.Reset();
            this.fpPax_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPax_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPax_Sheet1.ColumnCount = 3;
            this.fpPax_Sheet1.RowCount = 0;
            this.fpPax_Sheet1.AutoGenerateColumns = false;
            this.fpPax_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "机型";
            this.fpPax_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "班次";
            this.fpPax_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "人数";
            this.fpPax_Sheet1.Columns.Get(0).Border = lineBorder1;
            this.fpPax_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpPax_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPax_Sheet1.Columns.Get(0).Label = "机型";
            this.fpPax_Sheet1.Columns.Get(0).Locked = true;
            this.fpPax_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPax_Sheet1.Columns.Get(0).Width = 120F;
            this.fpPax_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.fpPax_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPax_Sheet1.Columns.Get(1).Label = "班次";
            this.fpPax_Sheet1.Columns.Get(1).Locked = true;
            this.fpPax_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPax_Sheet1.Columns.Get(1).Width = 120F;
            this.fpPax_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.fpPax_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPax_Sheet1.Columns.Get(2).Label = "人数";
            this.fpPax_Sheet1.Columns.Get(2).Locked = true;
            this.fpPax_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPax_Sheet1.Columns.Get(2).Width = 120F;
            this.fpPax_Sheet1.DataAutoSizeColumns = false;
            this.fpPax_Sheet1.DefaultStyle.Border = lineBorder2;
            this.fpPax_Sheet1.DefaultStyle.Locked = false;
            this.fpPax_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPax_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpPax_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPax_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPax_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPax_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPax.SetActiveViewport(1, 0);
            // 
            // fmStatisticPax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 301);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmStatisticPax";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出港值机信息统计";
            this.Load += new System.EventHandler(this.fmStatisticPax_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPax_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtFlightDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread fpPax;
        private FarPoint.Win.Spread.SheetView fpPax_Sheet1;
    }
}