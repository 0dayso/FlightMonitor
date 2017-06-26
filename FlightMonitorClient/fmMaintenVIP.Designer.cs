namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmMaintenVIP
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRecover = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpVip = new FarPoint.Win.Spread.FpSpread();
            this.fpVip_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fpDeleteVip = new FarPoint.Win.Spread.FpSpread();
            this.fpDeleteVip_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpVip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpVip_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpDeleteVip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDeleteVip_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnRecover);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 478);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(889, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(565, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 32;
            this.btnEdit.Text = "修 改";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(484, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 31;
            this.btnAdd.Text = "添 加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRecover
            // 
            this.btnRecover.Location = new System.Drawing.Point(726, 16);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Size = new System.Drawing.Size(75, 23);
            this.btnRecover.TabIndex = 30;
            this.btnRecover.Text = "还 原";
            this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(646, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "删 除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(806, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpVip);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(889, 280);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // fpVip
            // 
            this.fpVip.About = "2.5.2007.2005";
            this.fpVip.AccessibleDescription = "fpVip, Sheet1";
            this.fpVip.BackColor = System.Drawing.SystemColors.Control;
            this.fpVip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpVip.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpVip.Location = new System.Drawing.Point(3, 17);
            this.fpVip.Name = "fpVip";
            this.fpVip.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpVip_Sheet1});
            this.fpVip.Size = new System.Drawing.Size(883, 260);
            this.fpVip.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpVip.TextTipAppearance = tipAppearance1;
            this.fpVip.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpVip.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpVip_CellDoubleClick);
            // 
            // fpVip_Sheet1
            // 
            this.fpVip_Sheet1.Reset();
            this.fpVip_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpVip_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpVip_Sheet1.ColumnCount = 11;
            this.fpVip_Sheet1.RowCount = 0;
            this.fpVip_Sheet1.AutoGenerateColumns = false;
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "数据源";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "职务";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "舱位";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "VIP类型";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "联系方式";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "联系人";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "特殊要求";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "备注";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "陪同人数";
            this.fpVip_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "陪同人员";
            this.fpVip_Sheet1.Columns.Get(0).Label = "数据源";
            this.fpVip_Sheet1.Columns.Get(0).Width = 50F;
            this.fpVip_Sheet1.Columns.Get(1).Label = "姓名";
            this.fpVip_Sheet1.Columns.Get(1).Width = 45F;
            this.fpVip_Sheet1.Columns.Get(2).Label = "职务";
            this.fpVip_Sheet1.Columns.Get(2).Width = 160F;
            this.fpVip_Sheet1.Columns.Get(3).Label = "舱位";
            this.fpVip_Sheet1.Columns.Get(3).Width = 35F;
            this.fpVip_Sheet1.Columns.Get(4).Label = "VIP类型";
            this.fpVip_Sheet1.Columns.Get(4).Width = 50F;
            this.fpVip_Sheet1.Columns.Get(5).Label = "联系方式";
            this.fpVip_Sheet1.Columns.Get(5).Width = 80F;
            this.fpVip_Sheet1.Columns.Get(7).Label = "特殊要求";
            this.fpVip_Sheet1.Columns.Get(7).Width = 120F;
            this.fpVip_Sheet1.Columns.Get(8).Label = "备注";
            this.fpVip_Sheet1.Columns.Get(8).Width = 120F;
            this.fpVip_Sheet1.DataAutoSizeColumns = false;
            this.fpVip_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpVip_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpVip_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpVip_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpVip_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpVip.SetActiveViewport(1, 0);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpDeleteVip);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 280);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(889, 198);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "已删除VIP";
            // 
            // fpDeleteVip
            // 
            this.fpDeleteVip.About = "2.5.2007.2005";
            this.fpDeleteVip.AccessibleDescription = "fpVip, Sheet1";
            this.fpDeleteVip.BackColor = System.Drawing.SystemColors.Control;
            this.fpDeleteVip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDeleteVip.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpDeleteVip.Location = new System.Drawing.Point(3, 17);
            this.fpDeleteVip.Name = "fpDeleteVip";
            this.fpDeleteVip.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpDeleteVip_Sheet1});
            this.fpDeleteVip.Size = new System.Drawing.Size(883, 178);
            this.fpDeleteVip.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpDeleteVip.TextTipAppearance = tipAppearance2;
            this.fpDeleteVip.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpDeleteVip_Sheet1
            // 
            this.fpDeleteVip_Sheet1.Reset();
            this.fpDeleteVip_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpDeleteVip_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpDeleteVip_Sheet1.ColumnCount = 11;
            this.fpDeleteVip_Sheet1.RowCount = 0;
            this.fpDeleteVip_Sheet1.AutoGenerateColumns = false;
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "数据源";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "职务";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "舱位";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "VIP类型";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "联系方式";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "联系人";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "特殊要求";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "备注";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "陪同人数";
            this.fpDeleteVip_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "陪同人员";
            this.fpDeleteVip_Sheet1.Columns.Get(0).Label = "数据源";
            this.fpDeleteVip_Sheet1.Columns.Get(0).Width = 50F;
            this.fpDeleteVip_Sheet1.Columns.Get(1).Label = "姓名";
            this.fpDeleteVip_Sheet1.Columns.Get(1).Width = 45F;
            this.fpDeleteVip_Sheet1.Columns.Get(2).Label = "职务";
            this.fpDeleteVip_Sheet1.Columns.Get(2).Width = 160F;
            this.fpDeleteVip_Sheet1.Columns.Get(3).Label = "舱位";
            this.fpDeleteVip_Sheet1.Columns.Get(3).Width = 35F;
            this.fpDeleteVip_Sheet1.Columns.Get(4).Label = "VIP类型";
            this.fpDeleteVip_Sheet1.Columns.Get(4).Width = 50F;
            this.fpDeleteVip_Sheet1.Columns.Get(5).Label = "联系方式";
            this.fpDeleteVip_Sheet1.Columns.Get(5).Width = 80F;
            this.fpDeleteVip_Sheet1.Columns.Get(7).Label = "特殊要求";
            this.fpDeleteVip_Sheet1.Columns.Get(7).Width = 120F;
            this.fpDeleteVip_Sheet1.Columns.Get(8).Label = "备注";
            this.fpDeleteVip_Sheet1.Columns.Get(8).Width = 120F;
            this.fpDeleteVip_Sheet1.DataAutoSizeColumns = false;
            this.fpDeleteVip_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDeleteVip_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpDeleteVip_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpDeleteVip_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpDeleteVip_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpDeleteVip.SetActiveViewport(1, 0);
            // 
            // fmMaintenVIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 526);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmMaintenVIP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIP旅客信息";
            this.Load += new System.EventHandler(this.fmMaintenVIP_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpVip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpVip_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpDeleteVip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDeleteVip_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRecover;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread fpVip;
        private FarPoint.Win.Spread.SheetView fpVip_Sheet1;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread fpDeleteVip;
        private FarPoint.Win.Spread.SheetView fpDeleteVip_Sheet1;
        private System.Windows.Forms.Button btnEdit;

    }
}