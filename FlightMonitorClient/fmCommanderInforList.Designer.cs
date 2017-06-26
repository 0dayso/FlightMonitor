namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmCommanderInforList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCommanderType = new System.Windows.Forms.ComboBox();
            this.cmbStation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDeleteCommander = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fpCommander = new FarPoint.Win.Spread.FpSpread();
            this.fpCommander_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpCommander)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCommander_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbCommanderType);
            this.groupBox1.Controls.Add(this.cmbStation);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 42);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.TabIndex = 43;
            this.label1.Text = "类型";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCommanderType
            // 
            this.cmbCommanderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommanderType.Items.AddRange(new object[] {
            "主控",
            "副控",
            "外场",
            "全部"});
            this.cmbCommanderType.Location = new System.Drawing.Point(44, 12);
            this.cmbCommanderType.Name = "cmbCommanderType";
            this.cmbCommanderType.Size = new System.Drawing.Size(124, 20);
            this.cmbCommanderType.TabIndex = 41;
            this.cmbCommanderType.SelectedIndexChanged += new System.EventHandler(this.cmbCommanderType_SelectedIndexChanged);
            // 
            // cmbStation
            // 
            this.cmbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStation.Location = new System.Drawing.Point(219, 12);
            this.cmbStation.Name = "cmbStation";
            this.cmbStation.Size = new System.Drawing.Size(124, 20);
            this.cmbStation.TabIndex = 40;
            this.cmbStation.SelectedIndexChanged += new System.EventHandler(this.cmbStation_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(183, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 42;
            this.label2.Text = "场站";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnDeleteCommander);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 42);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(169, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 32;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDeleteCommander
            // 
            this.btnDeleteCommander.Location = new System.Drawing.Point(233, 13);
            this.btnDeleteCommander.Name = "btnDeleteCommander";
            this.btnDeleteCommander.Size = new System.Drawing.Size(60, 23);
            this.btnDeleteCommander.TabIndex = 31;
            this.btnDeleteCommander.Text = "删除";
            this.btnDeleteCommander.Click += new System.EventHandler(this.btnDeleteCommander_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(297, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpCommander);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 189);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // fpCommander
            // 
            this.fpCommander.About = "2.5.2007.2005";
            this.fpCommander.AccessibleDescription = "fpCommander";
            this.fpCommander.BackColor = System.Drawing.SystemColors.Control;
            this.fpCommander.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpCommander.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCommander.Location = new System.Drawing.Point(3, 17);
            this.fpCommander.Name = "fpCommander";
            this.fpCommander.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCommander_Sheet1});
            this.fpCommander.Size = new System.Drawing.Size(359, 169);
            this.fpCommander.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCommander.TextTipAppearance = tipAppearance1;
            this.fpCommander.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpCommander_Sheet1
            // 
            this.fpCommander_Sheet1.Reset();
            this.fpCommander_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCommander_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCommander_Sheet1.ColumnCount = 3;
            this.fpCommander_Sheet1.RowCount = 0;
            this.fpCommander_Sheet1.AutoGenerateColumns = false;
            this.fpCommander_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "帐号";
            this.fpCommander_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpCommander_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "类型";
            this.fpCommander_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpCommander_Sheet1.Columns.Get(0).Label = "帐号";
            this.fpCommander_Sheet1.Columns.Get(0).ShowSortIndicator = false;
            this.fpCommander_Sheet1.Columns.Get(0).Width = 100F;
            this.fpCommander_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpCommander_Sheet1.Columns.Get(1).Label = "姓名";
            this.fpCommander_Sheet1.Columns.Get(1).Width = 100F;
            this.fpCommander_Sheet1.Columns.Get(2).Label = "类型";
            this.fpCommander_Sheet1.Columns.Get(2).Width = 100F;
            this.fpCommander_Sheet1.DataAutoSizeColumns = false;
            this.fpCommander_Sheet1.DefaultStyle.Border = lineBorder1;
            this.fpCommander_Sheet1.DefaultStyle.Locked = false;
            this.fpCommander_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpCommander_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpCommander_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCommander_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpCommander_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpCommander_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpCommander.SetActiveViewport(1, 0);
            // 
            // fmCommanderInforList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 273);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmCommanderInforList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保障人员管理";
            this.Load += new System.EventHandler(this.fmCommanderInforList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpCommander)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCommander_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCommanderType;
        private System.Windows.Forms.ComboBox cmbStation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDeleteCommander;
        private System.Windows.Forms.Button btnCancel;
        private FarPoint.Win.Spread.FpSpread fpCommander;
        private FarPoint.Win.Spread.SheetView fpCommander_Sheet1;
    }
}