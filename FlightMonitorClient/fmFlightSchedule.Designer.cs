namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmFlightSchedule
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
            FarPoint.Win.Spread.TipAppearance tipAppearance6 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType31 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType32 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType33 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType34 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType35 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType36 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder6 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbStation = new System.Windows.Forms.ComboBox();
            this.dtFlightdate = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSumbit = new System.Windows.Forms.Button();
            this.txtFlightNo = new System.Windows.Forms.TextBox();
            this.dtQueryFlightDate = new System.Windows.Forms.DateTimePicker();
            this.cmbARRStation = new System.Windows.Forms.ComboBox();
            this.cmbDEPStation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpFlights = new FarPoint.Win.Spread.FpSpread();
            this.fpFlights_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrintPlan = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlights_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cmbStation);
            this.groupBox4.Controls.Add(this.dtFlightdate);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(784, 48);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "航站进出港动态";
            // 
            // cmbStation
            // 
            this.cmbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStation.Location = new System.Drawing.Point(42, 18);
            this.cmbStation.Name = "cmbStation";
            this.cmbStation.Size = new System.Drawing.Size(120, 20);
            this.cmbStation.TabIndex = 10;
            this.cmbStation.SelectedIndexChanged += new System.EventHandler(this.cmbStation_SelectedIndexChanged);
            // 
            // dtFlightdate
            // 
            this.dtFlightdate.Location = new System.Drawing.Point(225, 18);
            this.dtFlightdate.Name = "dtFlightdate";
            this.dtFlightdate.Size = new System.Drawing.Size(120, 21);
            this.dtFlightdate.TabIndex = 11;
            this.dtFlightdate.ValueChanged += new System.EventHandler(this.dtFlightdate_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmbDEPStation);
            this.groupBox3.Controls.Add(this.cmbARRStation);
            this.groupBox3.Controls.Add(this.btnSumbit);
            this.groupBox3.Controls.Add(this.txtFlightNo);
            this.groupBox3.Controls.Add(this.dtQueryFlightDate);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(784, 48);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "航班动态查询";
            // 
            // btnSumbit
            // 
            this.btnSumbit.Location = new System.Drawing.Point(713, 16);
            this.btnSumbit.Name = "btnSumbit";
            this.btnSumbit.Size = new System.Drawing.Size(63, 23);
            this.btnSumbit.TabIndex = 3;
            this.btnSumbit.Text = "查　询";
            this.btnSumbit.Click += new System.EventHandler(this.btnSumbit_Click);
            // 
            // txtFlightNo
            // 
            this.txtFlightNo.Location = new System.Drawing.Point(587, 17);
            this.txtFlightNo.Name = "txtFlightNo";
            this.txtFlightNo.Size = new System.Drawing.Size(120, 21);
            this.txtFlightNo.TabIndex = 2;
            // 
            // dtQueryFlightDate
            // 
            this.dtQueryFlightDate.Location = new System.Drawing.Point(44, 16);
            this.dtQueryFlightDate.Name = "dtQueryFlightDate";
            this.dtQueryFlightDate.Size = new System.Drawing.Size(120, 21);
            this.dtQueryFlightDate.TabIndex = 15;
            // 
            // cmbARRStation
            // 
            this.cmbARRStation.Location = new System.Drawing.Point(414, 17);
            this.cmbARRStation.Name = "cmbARRStation";
            this.cmbARRStation.Size = new System.Drawing.Size(120, 20);
            this.cmbARRStation.TabIndex = 16;
            // 
            // cmbDEPStation
            // 
            this.cmbDEPStation.Location = new System.Drawing.Point(229, 18);
            this.cmbDEPStation.Name = "cmbDEPStation";
            this.cmbDEPStation.Size = new System.Drawing.Size(120, 20);
            this.cmbDEPStation.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "航站";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "日期";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "航班日期";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "起飞机场";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(355, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "目的机场";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(540, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "航班号";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnPrintPlan);
            this.groupBox1.Controls.Add(this.btnExportExcel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 498);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 43);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpFlights);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(784, 402);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // fpFlights
            // 
            this.fpFlights.About = "2.5.2007.2005";
            this.fpFlights.AccessibleDescription = "fpFlights";
            this.fpFlights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFlights.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlights.Location = new System.Drawing.Point(3, 17);
            this.fpFlights.Name = "fpFlights";
            this.fpFlights.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpFlights_Sheet1});
            this.fpFlights.Size = new System.Drawing.Size(778, 382);
            this.fpFlights.TabIndex = 8;
            tipAppearance6.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFlights.TextTipAppearance = tipAppearance6;
            this.fpFlights.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpFlights_Sheet1
            // 
            this.fpFlights_Sheet1.Reset();
            this.fpFlights_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFlights_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFlights_Sheet1.ColumnCount = 12;
            this.fpFlights_Sheet1.RowCount = 0;
            this.fpFlights_Sheet1.AutoGenerateColumns = false;
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "航班号";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "飞机号";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "航班性质";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "航班状态";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "起飞机场";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "计划起飞";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "预计起飞";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "实际起飞";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "到达机场";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "计划到达";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "预计到达";
            this.fpFlights_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "实际到达";
            this.fpFlights_Sheet1.Columns.Get(5).CellType = textCellType31;
            this.fpFlights_Sheet1.Columns.Get(5).Label = "计划起飞";
            this.fpFlights_Sheet1.Columns.Get(6).CellType = textCellType32;
            this.fpFlights_Sheet1.Columns.Get(6).Label = "预计起飞";
            this.fpFlights_Sheet1.Columns.Get(7).CellType = textCellType33;
            this.fpFlights_Sheet1.Columns.Get(7).Label = "实际起飞";
            this.fpFlights_Sheet1.Columns.Get(9).CellType = textCellType34;
            this.fpFlights_Sheet1.Columns.Get(9).Label = "计划到达";
            this.fpFlights_Sheet1.Columns.Get(10).CellType = textCellType35;
            this.fpFlights_Sheet1.Columns.Get(10).Label = "预计到达";
            this.fpFlights_Sheet1.Columns.Get(11).CellType = textCellType36;
            this.fpFlights_Sheet1.Columns.Get(11).Label = "实际到达";
            this.fpFlights_Sheet1.DataAutoSizeColumns = false;
            this.fpFlights_Sheet1.DefaultStyle.Border = lineBorder6;
            this.fpFlights_Sheet1.DefaultStyle.Locked = false;
            this.fpFlights_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpFlights_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpFlights_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpFlights_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpFlights_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpFlights_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFlights.SetActiveViewport(1, 0);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(703, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrintPlan
            // 
            this.btnPrintPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintPlan.Location = new System.Drawing.Point(543, 14);
            this.btnPrintPlan.Name = "btnPrintPlan";
            this.btnPrintPlan.Size = new System.Drawing.Size(75, 23);
            this.btnPrintPlan.TabIndex = 18;
            this.btnPrintPlan.Text = "打印计划";
            this.btnPrintPlan.Click += new System.EventHandler(this.btnPrintPlan_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.Location = new System.Drawing.Point(623, 14);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 17;
            this.btnExportExcel.Text = "Excel计划";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // fmFlightSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 541);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmFlightSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "航班计划";
            this.Load += new System.EventHandler(this.fmFlightSchedule_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFlights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlights_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbStation;
        private System.Windows.Forms.DateTimePicker dtFlightdate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSumbit;
        private System.Windows.Forms.TextBox txtFlightNo;
        private System.Windows.Forms.DateTimePicker dtQueryFlightDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDEPStation;
        private System.Windows.Forms.ComboBox cmbARRStation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread fpFlights;
        private FarPoint.Win.Spread.SheetView fpFlights_Sheet1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrintPlan;
        private System.Windows.Forms.Button btnExportExcel;
    }
}