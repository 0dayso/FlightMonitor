namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmACMISC
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fpACMISC = new FarPoint.Win.Spread.FpSpread();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtOWNER = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtACTYPE = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLONG_REG = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSHORT_REG = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fpACMISC_sheetView2 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpACMISC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpACMISC_sheetView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOWNER);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtACTYPE);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtLONG_REG);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSHORT_REG);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 73);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpACMISC);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(435, 184);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // fpACMISC
            // 
            this.fpACMISC.About = "2.5.2007.2005";
            this.fpACMISC.AccessibleDescription = "fpACMISC";
            this.fpACMISC.BackColor = System.Drawing.SystemColors.Control;
            this.fpACMISC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpACMISC.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpACMISC.Location = new System.Drawing.Point(3, 17);
            this.fpACMISC.Name = "fpACMISC";
            this.fpACMISC.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpACMISC_sheetView2});
            this.fpACMISC.Size = new System.Drawing.Size(429, 164);
            this.fpACMISC.TabIndex = 1;
            tipAppearance6.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpACMISC.TextTipAppearance = tipAppearance6;
            this.fpACMISC.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(373, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 24);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(312, 14);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 24);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "删　除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(250, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 24);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "添　加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtOWNER
            // 
            this.txtOWNER.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOWNER.Location = new System.Drawing.Point(290, 40);
            this.txtOWNER.MaxLength = 2;
            this.txtOWNER.Name = "txtOWNER";
            this.txtOWNER.Size = new System.Drawing.Size(130, 21);
            this.txtOWNER.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(210, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "公司两字码";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtACTYPE
            // 
            this.txtACTYPE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtACTYPE.Location = new System.Drawing.Point(71, 40);
            this.txtACTYPE.MaxLength = 3;
            this.txtACTYPE.Name = "txtACTYPE";
            this.txtACTYPE.Size = new System.Drawing.Size(130, 21);
            this.txtACTYPE.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "FOC机型";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLONG_REG
            // 
            this.txtLONG_REG.Location = new System.Drawing.Point(290, 13);
            this.txtLONG_REG.MaxLength = 10;
            this.txtLONG_REG.Name = "txtLONG_REG";
            this.txtLONG_REG.Size = new System.Drawing.Size(130, 21);
            this.txtLONG_REG.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(210, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "长注册号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSHORT_REG
            // 
            this.txtSHORT_REG.Location = new System.Drawing.Point(71, 13);
            this.txtSHORT_REG.MaxLength = 3;
            this.txtSHORT_REG.Name = "txtSHORT_REG";
            this.txtSHORT_REG.Size = new System.Drawing.Size(130, 21);
            this.txtSHORT_REG.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "短注册号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fpACMISC_sheetView2
            // 
            this.fpACMISC_sheetView2.Reset();
            this.fpACMISC_sheetView2.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpACMISC_sheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpACMISC_sheetView2.ColumnCount = 4;
            this.fpACMISC_sheetView2.RowCount = 0;
            this.fpACMISC_sheetView2.AutoGenerateColumns = false;
            this.fpACMISC_sheetView2.ColumnHeader.Cells.Get(0, 0).Value = "短注册号";
            this.fpACMISC_sheetView2.ColumnHeader.Cells.Get(0, 1).Value = "长注册号";
            this.fpACMISC_sheetView2.ColumnHeader.Cells.Get(0, 2).Value = "FOC机型";
            this.fpACMISC_sheetView2.ColumnHeader.Cells.Get(0, 3).Value = "公司两字码";
            this.fpACMISC_sheetView2.Columns.Get(0).Label = "短注册号";
            this.fpACMISC_sheetView2.Columns.Get(0).Width = 90F;
            this.fpACMISC_sheetView2.Columns.Get(1).Label = "长注册号";
            this.fpACMISC_sheetView2.Columns.Get(1).Width = 89F;
            this.fpACMISC_sheetView2.Columns.Get(2).Label = "FOC机型";
            this.fpACMISC_sheetView2.Columns.Get(2).Width = 90F;
            this.fpACMISC_sheetView2.Columns.Get(3).Label = "公司两字码";
            this.fpACMISC_sheetView2.Columns.Get(3).Width = 90F;
            this.fpACMISC_sheetView2.DataAutoSizeColumns = false;
            this.fpACMISC_sheetView2.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpACMISC_sheetView2.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpACMISC_sheetView2.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpACMISC_sheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fmACMISC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 301);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmACMISC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "飞机注册号管理";
            this.Load += new System.EventHandler(this.fmACMISC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpACMISC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpACMISC_sheetView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread fpACMISC;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtOWNER;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtACTYPE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLONG_REG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSHORT_REG;
        private System.Windows.Forms.Label label1;
        private FarPoint.Win.Spread.SheetView fpACMISC_sheetView2;
    }
}