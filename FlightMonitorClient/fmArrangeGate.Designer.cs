namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmArrangeGate
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
            FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder4 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.dtFlightDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fpGate = new FarPoint.Win.Spread.FpSpread();
            this.fpGate_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpGate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpGate_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpGate);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(608, 293);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // fpGate
            // 
            this.fpGate.About = "2.5.2007.2005";
            this.fpGate.AccessibleDescription = "fpGate, Sheet1";
            this.fpGate.BackColor = System.Drawing.SystemColors.Control;
            this.fpGate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpGate.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpGate.Location = new System.Drawing.Point(3, 17);
            this.fpGate.Name = "fpGate";
            this.fpGate.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpGate_Sheet1});
            this.fpGate.Size = new System.Drawing.Size(602, 273);
            this.fpGate.TabIndex = 5;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpGate.TextTipAppearance = tipAppearance2;
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
            this.fpGate_Sheet1.Columns.Get(0).Border = lineBorder3;
            this.fpGate_Sheet1.Columns.Get(0).CellType = textCellType9;
            this.fpGate_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(0).Locked = true;
            this.fpGate_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(1).CellType = textCellType10;
            this.fpGate_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(1).Locked = true;
            this.fpGate_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(1).Width = 63F;
            this.fpGate_Sheet1.Columns.Get(2).CellType = textCellType11;
            this.fpGate_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(2).Locked = true;
            this.fpGate_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType12.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textCellType12.MaxLength = 10;
            this.fpGate_Sheet1.Columns.Get(3).CellType = textCellType12;
            this.fpGate_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType13.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            textCellType13.MaxLength = 10;
            this.fpGate_Sheet1.Columns.Get(4).CellType = textCellType13;
            this.fpGate_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(4).Locked = false;
            this.fpGate_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(5).CellType = textCellType14;
            this.fpGate_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(5).Locked = true;
            this.fpGate_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(6).CellType = textCellType15;
            this.fpGate_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(6).Locked = true;
            this.fpGate_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(7).CellType = textCellType16;
            this.fpGate_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpGate_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpGate_Sheet1.Columns.Get(7).Width = 120F;
            this.fpGate_Sheet1.DataAutoSizeColumns = false;
            this.fpGate_Sheet1.DefaultStyle.Border = lineBorder4;
            this.fpGate_Sheet1.DefaultStyle.Locked = false;
            this.fpGate_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpGate_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpGate_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpGate.SetActiveViewport(1, 0);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(528, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(288, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "保 存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(448, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtFlightDate);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnExportExcel);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 341);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(368, 16);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 17;
            this.btnExportExcel.Text = "导出Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // fmArrangeGate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 389);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmArrangeGate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "航后机位安排";
            this.Load += new System.EventHandler(this.fmArrangeGate_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpGate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpGate_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtFlightDate;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread fpGate;
        private FarPoint.Win.Spread.SheetView fpGate_Sheet1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExportExcel;
    }
}