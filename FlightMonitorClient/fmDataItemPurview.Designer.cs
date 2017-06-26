namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmDataItemPurview
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
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpDataItem = new FarPoint.Win.Spread.FpSpread();
            this.shDataItem = new FarPoint.Win.Spread.SheetView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpDataItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shDataItem)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(611, 40);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(531, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Location = new System.Drawing.Point(451, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpDataItem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(611, 362);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // fpDataItem
            // 
            this.fpDataItem.About = "2.5.2007.2005";
            this.fpDataItem.AccessibleDescription = "fpDataItem, Sheet1";
            this.fpDataItem.BackColor = System.Drawing.SystemColors.Control;
            this.fpDataItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDataItem.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpDataItem.Location = new System.Drawing.Point(3, 17);
            this.fpDataItem.Name = "fpDataItem";
            this.fpDataItem.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shDataItem});
            this.fpDataItem.Size = new System.Drawing.Size(605, 342);
            this.fpDataItem.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpDataItem.TextTipAppearance = tipAppearance1;
            this.fpDataItem.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // shDataItem
            // 
            this.shDataItem.Reset();
            this.shDataItem.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shDataItem.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shDataItem.ColumnCount = 2;
            this.shDataItem.RowCount = 0;
            this.shDataItem.AlternatingRows.Get(1).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.shDataItem.AutoGenerateColumns = false;
            this.shDataItem.ColumnHeader.Cells.Get(0, 0).Value = "控制项";
            this.shDataItem.ColumnHeader.Cells.Get(0, 1).Value = "控制权限";
            this.shDataItem.Columns.Get(0).Label = "控制项";
            this.shDataItem.Columns.Get(0).Locked = true;
            this.shDataItem.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.shDataItem.Columns.Get(0).Width = 260F;
            comboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType1.Items = new string[] {
        "无",
        "浏览",
        "维护权限"};
            this.shDataItem.Columns.Get(1).CellType = comboBoxCellType1;
            this.shDataItem.Columns.Get(1).Label = "控制权限";
            this.shDataItem.Columns.Get(1).Width = 260F;
            this.shDataItem.DataAutoSizeColumns = false;
            this.shDataItem.RowHeader.Columns.Default.Resizable = false;
            this.shDataItem.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpDataItem.SetActiveViewport(1, 0);
            // 
            // fmDataItemPurview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 402);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmDataItemPurview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "控制权限";
            this.Load += new System.EventHandler(this.fmDataItemPurview_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpDataItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shDataItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread fpDataItem;
        private FarPoint.Win.Spread.SheetView shDataItem;

    }
}