namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmPostionManagement
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
            this.fpPosition = new FarPoint.Win.Spread.FpSpread();
            this.fpPosition_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPositionName = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPosition_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpPosition);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "席位列表";
            // 
            // fpPosition
            // 
            this.fpPosition.About = "2.5.2007.2005";
            this.fpPosition.AccessibleDescription = "fpAccount, Sheet1, Row 0, Column 0, ";
            this.fpPosition.BackColor = System.Drawing.SystemColors.Control;
            this.fpPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPosition.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPosition.Location = new System.Drawing.Point(3, 17);
            this.fpPosition.Name = "fpPosition";
            this.fpPosition.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPosition_Sheet1});
            this.fpPosition.Size = new System.Drawing.Size(226, 170);
            this.fpPosition.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPosition.TextTipAppearance = tipAppearance1;
            this.fpPosition.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPosition_Sheet1
            // 
            this.fpPosition_Sheet1.Reset();
            this.fpPosition_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPosition_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPosition_Sheet1.ColumnCount = 1;
            this.fpPosition_Sheet1.RowCount = 0;
            this.fpPosition_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.LightGray;
            this.fpPosition_Sheet1.AutoGenerateColumns = false;
            this.fpPosition_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "席位名称";
            this.fpPosition_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPosition_Sheet1.Columns.Get(0).Label = "席位名称";
            this.fpPosition_Sheet1.Columns.Get(0).ShowSortIndicator = false;
            this.fpPosition_Sheet1.Columns.Get(0).Width = 140F;
            this.fpPosition_Sheet1.DataAutoSizeColumns = false;
            this.fpPosition_Sheet1.DefaultStyle.Border = lineBorder1;
            this.fpPosition_Sheet1.DefaultStyle.Locked = false;
            this.fpPosition_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPosition_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpPosition_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPosition_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPosition_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPosition_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPosition.SetActiveViewport(1, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPositionName);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(232, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 190);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtPositionName
            // 
            this.txtPositionName.Location = new System.Drawing.Point(7, 49);
            this.txtPositionName.MaxLength = 50;
            this.txtPositionName.Name = "txtPositionName";
            this.txtPositionName.Size = new System.Drawing.Size(193, 21);
            this.txtPositionName.TabIndex = 12;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(74, 161);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "删 除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(140, 161);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(8, 161);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "添 加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "席位名称:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fmPostionManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 190);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmPostionManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "席位管理";
            this.Load += new System.EventHandler(this.fmPostionManagement_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPosition_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private FarPoint.Win.Spread.FpSpread fpPosition;
        private FarPoint.Win.Spread.SheetView fpPosition_Sheet1;
        private System.Windows.Forms.TextBox txtPositionName;
    }
}