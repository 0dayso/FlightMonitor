namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmQueryGate
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
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.fpGate_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpGate = new FarPoint.Win.Spread.FpSpread();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtFlightDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpGate_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpGate)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(448, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(368, 16);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 14;
            this.btnExportExcel.Text = "导出Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnExportExcel);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 48);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(528, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // fpGate_Sheet1
            // 
            this.fpGate_Sheet1.Reset();
            this.fpGate_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpGate_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpGate_Sheet1.ColumnCount = 8;
            this.fpGate_Sheet1.ColumnHeader.RowCount = 2;
            this.fpGate_Sheet1.RowCount = 0;
            this.fpGate_Sheet1.AutoGenerateColumns = false;
            this.fpGate_Sheet1.Columns.Get(0).Border = lineBorder1;
            this.fpGate_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpGate_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(0).Locked = true;
            this.fpGate_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.fpGate_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(1).Locked = true;
            this.fpGate_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(1).Width = 63F;
            this.fpGate_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.fpGate_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(2).Locked = true;
            this.fpGate_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.fpGate_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.fpGate_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(4).Locked = false;
            this.fpGate_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.fpGate_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(5).Locked = true;
            this.fpGate_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(6).CellType = textCellType7;
            this.fpGate_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(6).Locked = true;
            this.fpGate_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(7).CellType = textCellType8;
            this.fpGate_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(7).Width = 120F;
            this.fpGate_Sheet1.DataAutoSizeColumns = false;
            this.fpGate_Sheet1.DefaultStyle.Border = lineBorder2;
            this.fpGate_Sheet1.DefaultStyle.Locked = false;
            this.fpGate_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpGate_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpGate_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpGate_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpGate_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpGate_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpGate
            // 
            this.fpGate.About = "2.5.2007.2005";
            this.fpGate.AccessibleDescription = "fpGate";
            this.fpGate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpGate.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpGate.Location = new System.Drawing.Point(3, 17);
            this.fpGate.Name = "fpGate";
            this.fpGate.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpGate_Sheet1});
            this.fpGate.Size = new System.Drawing.Size(602, 305);
            this.fpGate.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpGate.TextTipAppearance = tipAppearance1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpGate);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 325);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // dtFlightDate
            // 
            this.dtFlightDate.Location = new System.Drawing.Point(7, 11);
            this.dtFlightDate.Name = "dtFlightDate";
            this.dtFlightDate.Size = new System.Drawing.Size(144, 21);
            this.dtFlightDate.TabIndex = 105;
            this.dtFlightDate.TabStop = false;
            this.dtFlightDate.ValueChanged += new System.EventHandler(this.dtFlightDate_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtFlightDate);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(608, 40);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // fmQueryGate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 411);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmQueryGate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "航后机位查询";
            this.Load += new System.EventHandler(this.rmQueryGate_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpGate_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpGate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private FarPoint.Win.Spread.SheetView fpGate_Sheet1;
        private FarPoint.Win.Spread.FpSpread fpGate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtFlightDate;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}