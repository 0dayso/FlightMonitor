namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmAccount
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbUserType = new System.Windows.Forms.ComboBox();
            this.cmbStation = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDataItemPurview = new System.Windows.Forms.Button();
            this.btnMenuPurview = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fpAccount = new FarPoint.Win.Spread.FpSpread();
            this.fpAccount_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAccount_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbUserType);
            this.groupBox1.Controls.Add(this.cmbStation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 36);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "用户类别";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "航站";
            // 
            // cmbUserType
            // 
            this.cmbUserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserType.FormattingEnabled = true;
            this.cmbUserType.Items.AddRange(new object[] {
            "",
            "航班监控",
            "航班保障"});
            this.cmbUserType.Location = new System.Drawing.Point(268, 11);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Size = new System.Drawing.Size(130, 20);
            this.cmbUserType.TabIndex = 1;
            this.cmbUserType.SelectedIndexChanged += new System.EventHandler(this.cmbUserType_SelectedIndexChanged);
            // 
            // cmbStation
            // 
            this.cmbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStation.FormattingEnabled = true;
            this.cmbStation.Location = new System.Drawing.Point(39, 11);
            this.cmbStation.Name = "cmbStation";
            this.cmbStation.Size = new System.Drawing.Size(130, 20);
            this.cmbStation.TabIndex = 0;
            this.cmbStation.SelectedIndexChanged += new System.EventHandler(this.cmbStation_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnDataItemPurview);
            this.groupBox2.Controls.Add(this.btnMenuPurview);
            this.groupBox2.Controls.Add(this.btnInitialize);
            this.groupBox2.Controls.Add(this.btnAddAccount);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 447);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(804, 43);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(644, 13);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 24);
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "导 出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(723, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 24);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(249, 13);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(73, 24);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "修改用户";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(328, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 24);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "删除用户";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDataItemPurview
            // 
            this.btnDataItemPurview.Location = new System.Drawing.Point(565, 13);
            this.btnDataItemPurview.Name = "btnDataItemPurview";
            this.btnDataItemPurview.Size = new System.Drawing.Size(73, 24);
            this.btnDataItemPurview.TabIndex = 14;
            this.btnDataItemPurview.Text = "控制权限";
            this.btnDataItemPurview.Click += new System.EventHandler(this.btnDataItemPurview_Click);
            // 
            // btnMenuPurview
            // 
            this.btnMenuPurview.Location = new System.Drawing.Point(486, 13);
            this.btnMenuPurview.Name = "btnMenuPurview";
            this.btnMenuPurview.Size = new System.Drawing.Size(73, 24);
            this.btnMenuPurview.TabIndex = 13;
            this.btnMenuPurview.Text = "系统权限";
            this.btnMenuPurview.Click += new System.EventHandler(this.btnMenuPurview_Click);
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(407, 13);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(73, 24);
            this.btnInitialize.TabIndex = 1;
            this.btnInitialize.Text = "初始化密码";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.Location = new System.Drawing.Point(170, 13);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(73, 24);
            this.btnAddAccount.TabIndex = 0;
            this.btnAddAccount.Text = "添加用户";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpAccount);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(804, 411);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // fpAccount
            // 
            this.fpAccount.About = "2.5.2007.2005";
            this.fpAccount.AccessibleDescription = "fpAccount";
            this.fpAccount.BackColor = System.Drawing.SystemColors.Control;
            this.fpAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpAccount.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpAccount.Location = new System.Drawing.Point(3, 17);
            this.fpAccount.Name = "fpAccount";
            this.fpAccount.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpAccount_Sheet1});
            this.fpAccount.Size = new System.Drawing.Size(798, 391);
            this.fpAccount.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpAccount.TextTipAppearance = tipAppearance1;
            this.fpAccount.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpAccount.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpAccount_CellDoubleClick);
            // 
            // fpAccount_Sheet1
            // 
            this.fpAccount_Sheet1.Reset();
            this.fpAccount_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpAccount_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpAccount_Sheet1.ColumnCount = 7;
            this.fpAccount_Sheet1.RowCount = 0;
            this.fpAccount_Sheet1.AlternatingRows.Get(1).BackColor = System.Drawing.Color.LightGray;
            this.fpAccount_Sheet1.AutoGenerateColumns = false;
            this.fpAccount_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "帐号";
            this.fpAccount_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpAccount_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "部门";
            this.fpAccount_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "刷新间隔";
            this.fpAccount_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "用户类别";
            this.fpAccount_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "最大用户";
            this.fpAccount_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "登陆用户";
            this.fpAccount_Sheet1.Columns.Get(0).AllowAutoSort = true;
            this.fpAccount_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpAccount_Sheet1.Columns.Get(0).Label = "帐号";
            this.fpAccount_Sheet1.Columns.Get(0).SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Ascending;
            this.fpAccount_Sheet1.Columns.Get(0).Width = 100F;
            this.fpAccount_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpAccount_Sheet1.Columns.Get(1).Label = "姓名";
            this.fpAccount_Sheet1.Columns.Get(1).Width = 100F;
            this.fpAccount_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpAccount_Sheet1.Columns.Get(2).Label = "部门";
            this.fpAccount_Sheet1.Columns.Get(2).Width = 200F;
            this.fpAccount_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpAccount_Sheet1.Columns.Get(3).Label = "刷新间隔";
            this.fpAccount_Sheet1.Columns.Get(3).Width = 80F;
            this.fpAccount_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpAccount_Sheet1.Columns.Get(4).Label = "用户类别";
            this.fpAccount_Sheet1.Columns.Get(4).Width = 100F;
            this.fpAccount_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpAccount_Sheet1.Columns.Get(5).Label = "最大用户";
            this.fpAccount_Sheet1.Columns.Get(5).Width = 80F;
            this.fpAccount_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpAccount_Sheet1.Columns.Get(6).Label = "登陆用户";
            this.fpAccount_Sheet1.Columns.Get(6).Width = 80F;
            this.fpAccount_Sheet1.DataAutoSizeColumns = false;
            this.fpAccount_Sheet1.DefaultStyle.Border = lineBorder1;
            this.fpAccount_Sheet1.DefaultStyle.Locked = false;
            this.fpAccount_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpAccount_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpAccount_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpAccount_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpAccount_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpAccount_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpAccount.SetActiveViewport(1, 0);
            // 
            // fmAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 490);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.fmAccount_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAccount_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbUserType;
        private System.Windows.Forms.ComboBox cmbStation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private FarPoint.Win.Spread.FpSpread fpAccount;
        private FarPoint.Win.Spread.SheetView fpAccount_Sheet1;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDataItemPurview;
        private System.Windows.Forms.Button btnMenuPurview;
    }
}