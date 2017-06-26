namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmStationgList
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
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder4 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder5 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder6 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder7 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder8 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder9 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpsStationInfor = new FarPoint.Win.Spread.FpSpread();
            this.fpsStationInfor_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDeleteCommander = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpsStationInfor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsStationInfor_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnDeleteCommander);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 338);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpsStationInfor);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 338);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // fpsStationInfor
            // 
            this.fpsStationInfor.About = "2.5.2007.2005";
            this.fpsStationInfor.AccessibleDescription = "fpsStationInfor";
            this.fpsStationInfor.BackColor = System.Drawing.SystemColors.Control;
            this.fpsStationInfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsStationInfor.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsStationInfor.Location = new System.Drawing.Point(3, 17);
            this.fpsStationInfor.Name = "fpsStationInfor";
            this.fpsStationInfor.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpsStationInfor_Sheet1});
            this.fpsStationInfor.Size = new System.Drawing.Size(721, 318);
            this.fpsStationInfor.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpsStationInfor.TextTipAppearance = tipAppearance1;
            this.fpsStationInfor.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpsStationInfor_Sheet1
            // 
            this.fpsStationInfor_Sheet1.Reset();
            this.fpsStationInfor_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpsStationInfor_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpsStationInfor_Sheet1.ColumnCount = 9;
            this.fpsStationInfor_Sheet1.RowCount = 0;
            this.fpsStationInfor_Sheet1.AutoGenerateColumns = false;
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "三字码";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "场站名称";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "机场名称";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "现场指挥室名称";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "场站签到标识";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "航班日期分割时刻";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "延误时间限制";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "连飞时间";
            this.fpsStationInfor_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "网络故障提示时间";
            this.fpsStationInfor_Sheet1.ColumnHeader.Columns.Default.Resizable = false;
            this.fpsStationInfor_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.fpsStationInfor_Sheet1.Columns.Default.Resizable = false;
            this.fpsStationInfor_Sheet1.Columns.Get(0).Border = lineBorder1;
            this.fpsStationInfor_Sheet1.Columns.Get(0).Label = "三字码";
            this.fpsStationInfor_Sheet1.Columns.Get(0).Locked = true;
            this.fpsStationInfor_Sheet1.Columns.Get(0).ShowSortIndicator = false;
            this.fpsStationInfor_Sheet1.Columns.Get(0).Width = 45F;
            this.fpsStationInfor_Sheet1.Columns.Get(1).Border = lineBorder2;
            this.fpsStationInfor_Sheet1.Columns.Get(1).Label = "场站名称";
            this.fpsStationInfor_Sheet1.Columns.Get(1).Locked = true;
            this.fpsStationInfor_Sheet1.Columns.Get(1).ShowSortIndicator = false;
            this.fpsStationInfor_Sheet1.Columns.Get(1).Width = 100F;
            this.fpsStationInfor_Sheet1.Columns.Get(2).Border = lineBorder3;
            this.fpsStationInfor_Sheet1.Columns.Get(2).Label = "机场名称";
            this.fpsStationInfor_Sheet1.Columns.Get(2).Locked = true;
            this.fpsStationInfor_Sheet1.Columns.Get(2).ShowSortIndicator = false;
            this.fpsStationInfor_Sheet1.Columns.Get(2).Width = 120F;
            this.fpsStationInfor_Sheet1.Columns.Get(3).Border = lineBorder4;
            this.fpsStationInfor_Sheet1.Columns.Get(3).Label = "现场指挥室名称";
            this.fpsStationInfor_Sheet1.Columns.Get(3).Locked = true;
            this.fpsStationInfor_Sheet1.Columns.Get(3).ShowSortIndicator = false;
            this.fpsStationInfor_Sheet1.Columns.Get(3).Width = 140F;
            this.fpsStationInfor_Sheet1.Columns.Get(4).Border = lineBorder5;
            this.fpsStationInfor_Sheet1.Columns.Get(4).Label = "场站签到标识";
            this.fpsStationInfor_Sheet1.Columns.Get(4).Width = 50F;
            this.fpsStationInfor_Sheet1.Columns.Get(5).Border = lineBorder6;
            this.fpsStationInfor_Sheet1.Columns.Get(5).CellType = textCellType1;
            this.fpsStationInfor_Sheet1.Columns.Get(5).Label = "航班日期分割时刻";
            this.fpsStationInfor_Sheet1.Columns.Get(5).Locked = true;
            this.fpsStationInfor_Sheet1.Columns.Get(5).ShowSortIndicator = false;
            this.fpsStationInfor_Sheet1.Columns.Get(6).Border = lineBorder7;
            this.fpsStationInfor_Sheet1.Columns.Get(6).CellType = textCellType2;
            this.fpsStationInfor_Sheet1.Columns.Get(6).Label = "延误时间限制";
            this.fpsStationInfor_Sheet1.Columns.Get(6).ShowSortIndicator = false;
            this.fpsStationInfor_Sheet1.Columns.Get(6).Width = 50F;
            this.fpsStationInfor_Sheet1.Columns.Get(7).Border = lineBorder8;
            this.fpsStationInfor_Sheet1.Columns.Get(7).CellType = textCellType3;
            this.fpsStationInfor_Sheet1.Columns.Get(7).Label = "连飞时间";
            this.fpsStationInfor_Sheet1.Columns.Get(7).ShowSortIndicator = false;
            this.fpsStationInfor_Sheet1.Columns.Get(7).Width = 40F;
            this.fpsStationInfor_Sheet1.Columns.Get(8).Border = lineBorder9;
            this.fpsStationInfor_Sheet1.Columns.Get(8).CellType = textCellType4;
            this.fpsStationInfor_Sheet1.Columns.Get(8).Label = "网络故障提示时间";
            this.fpsStationInfor_Sheet1.DataAutoSizeColumns = false;
            this.fpsStationInfor_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpsStationInfor_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpsStationInfor_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fpsStationInfor_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpsStationInfor_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors;
            this.fpsStationInfor_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpsStationInfor_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpsStationInfor.SetActiveViewport(1, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(531, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 35;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDeleteCommander
            // 
            this.btnDeleteCommander.Location = new System.Drawing.Point(595, 14);
            this.btnDeleteCommander.Name = "btnDeleteCommander";
            this.btnDeleteCommander.Size = new System.Drawing.Size(60, 23);
            this.btnDeleteCommander.TabIndex = 34;
            this.btnDeleteCommander.Text = "删除";
            this.btnDeleteCommander.Click += new System.EventHandler(this.btnDeleteCommander_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(659, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fmStationgList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 383);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmStationgList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "场站管理";
            this.Load += new System.EventHandler(this.fmStationgList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpsStationInfor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsStationInfor_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread fpsStationInfor;
        private FarPoint.Win.Spread.SheetView fpsStationInfor_Sheet1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDeleteCommander;
        private System.Windows.Forms.Button btnCancel;
    }
}