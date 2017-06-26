namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmCrewSignIn
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame);
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.fpsCrewSignIn_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpsCrewSignIn = new FarPoint.Win.Spread.FpSpread();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.fpsCrewSignIn_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsCrewSignIn)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpsCrewSignIn_Sheet1
            // 
            this.fpsCrewSignIn_Sheet1.Reset();
            this.fpsCrewSignIn_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpsCrewSignIn_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpsCrewSignIn_Sheet1.ColumnCount = 21;
            this.fpsCrewSignIn_Sheet1.RowCount = 0;
            this.fpsCrewSignIn_Sheet1.AutoGenerateColumns = false;
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "航班号";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "起飞时间";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "到达时间";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "飞机号";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "航段";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "机长";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "副驾1";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "副驾2";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "学员";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "检查1";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "检查2";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "检查员";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "签到截止时间";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "机长签到标记";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "副驾1签到标记";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "副驾2签到标记";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "学员签到标记";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "检查1签到标记";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "检查2签到标记";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "检查员签到标记";
            this.fpsCrewSignIn_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "备注";
            this.fpsCrewSignIn_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F);
            this.fpsCrewSignIn_Sheet1.Columns.Get(0).Label = "航班号";
            this.fpsCrewSignIn_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpsCrewSignIn_Sheet1.Columns.Get(1).Label = "起飞时间";
            this.fpsCrewSignIn_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.fpsCrewSignIn_Sheet1.Columns.Get(2).Label = "到达时间";
            this.fpsCrewSignIn_Sheet1.Columns.Get(12).CellType = textCellType3;
            this.fpsCrewSignIn_Sheet1.Columns.Get(12).Label = "签到截止时间";
            this.fpsCrewSignIn_Sheet1.Columns.Get(12).Width = 85F;
            this.fpsCrewSignIn_Sheet1.Columns.Get(13).Label = "机长签到标记";
            this.fpsCrewSignIn_Sheet1.Columns.Get(13).Visible = false;
            this.fpsCrewSignIn_Sheet1.Columns.Get(14).Label = "副驾1签到标记";
            this.fpsCrewSignIn_Sheet1.Columns.Get(14).Visible = false;
            this.fpsCrewSignIn_Sheet1.Columns.Get(15).Label = "副驾2签到标记";
            this.fpsCrewSignIn_Sheet1.Columns.Get(15).Visible = false;
            this.fpsCrewSignIn_Sheet1.Columns.Get(16).Label = "学员签到标记";
            this.fpsCrewSignIn_Sheet1.Columns.Get(16).Visible = false;
            this.fpsCrewSignIn_Sheet1.Columns.Get(17).Label = "检查1签到标记";
            this.fpsCrewSignIn_Sheet1.Columns.Get(17).Visible = false;
            this.fpsCrewSignIn_Sheet1.Columns.Get(18).Label = "检查2签到标记";
            this.fpsCrewSignIn_Sheet1.Columns.Get(18).Visible = false;
            this.fpsCrewSignIn_Sheet1.Columns.Get(19).Label = "检查员签到标记";
            this.fpsCrewSignIn_Sheet1.Columns.Get(19).Visible = false;
            this.fpsCrewSignIn_Sheet1.DataAutoSizeColumns = false;
            this.fpsCrewSignIn_Sheet1.DefaultStyle.Border = lineBorder1;
            this.fpsCrewSignIn_Sheet1.DefaultStyle.Locked = false;
            this.fpsCrewSignIn_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpsCrewSignIn_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpsCrewSignIn_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpsCrewSignIn_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpsCrewSignIn_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.None;
            this.fpsCrewSignIn_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpsCrewSignIn_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpsCrewSignIn
            // 
            this.fpsCrewSignIn.About = "2.5.2007.2005";
            this.fpsCrewSignIn.AccessibleDescription = "fpsCrewSignIn, Sheet1";
            this.fpsCrewSignIn.BackColor = System.Drawing.SystemColors.Control;
            this.fpsCrewSignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsCrewSignIn.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsCrewSignIn.Location = new System.Drawing.Point(3, 17);
            this.fpsCrewSignIn.Name = "fpsCrewSignIn";
            this.fpsCrewSignIn.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpsCrewSignIn_Sheet1});
            this.fpsCrewSignIn.Size = new System.Drawing.Size(586, 81);
            this.fpsCrewSignIn.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpsCrewSignIn.TextTipAppearance = tipAppearance1;
            this.fpsCrewSignIn.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsCrewSignIn.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpsCrewSignIn_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpsCrewSignIn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // fmCrewSignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 101);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmCrewSignIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机组签到";
            this.Load += new System.EventHandler(this.fmCrewSignIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsCrewSignIn_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsCrewSignIn)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.SheetView fpsCrewSignIn_Sheet1;
        private FarPoint.Win.Spread.FpSpread fpsCrewSignIn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}