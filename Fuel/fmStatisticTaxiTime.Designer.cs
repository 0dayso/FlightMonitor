namespace Fuel
{
    partial class fmStatisticTaxiTime
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
            FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder12 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtStartFlightDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtEndFlightDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtThreeCode = new System.Windows.Forms.TextBox();
            this.btnStatistic = new System.Windows.Forms.Button();
            this.fpFlightInfo = new FarPoint.Win.Spread.FpSpread();
            this.shInTaxi = new FarPoint.Win.Spread.SheetView();
            this.shOutTaxi = new FarPoint.Win.Spread.SheetView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shInTaxi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shOutTaxi)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStatistic);
            this.groupBox1.Controls.Add(this.txtThreeCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtEndFlightDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtStartFlightDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(829, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 500);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(829, 54);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fpFlightInfo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(829, 444);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // dtStartFlightDate
            // 
            this.dtStartFlightDate.Location = new System.Drawing.Point(76, 20);
            this.dtStartFlightDate.Name = "dtStartFlightDate";
            this.dtStartFlightDate.Size = new System.Drawing.Size(146, 21);
            this.dtStartFlightDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始日期";
            // 
            // dtEndFlightDate
            // 
            this.dtEndFlightDate.Location = new System.Drawing.Point(307, 20);
            this.dtEndFlightDate.Name = "dtEndFlightDate";
            this.dtEndFlightDate.Size = new System.Drawing.Size(146, 21);
            this.dtEndFlightDate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "结束日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "机场三字码";
            // 
            // txtThreeCode
            // 
            this.txtThreeCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtThreeCode.Location = new System.Drawing.Point(572, 20);
            this.txtThreeCode.MaxLength = 3;
            this.txtThreeCode.Name = "txtThreeCode";
            this.txtThreeCode.Size = new System.Drawing.Size(146, 21);
            this.txtThreeCode.TabIndex = 7;
            // 
            // btnStatistic
            // 
            this.btnStatistic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatistic.Location = new System.Drawing.Point(749, 20);
            this.btnStatistic.Name = "btnStatistic";
            this.btnStatistic.Size = new System.Drawing.Size(56, 23);
            this.btnStatistic.TabIndex = 13;
            this.btnStatistic.Text = "统 计";
            this.btnStatistic.Click += new System.EventHandler(this.btnStatistic_Click);
            // 
            // fpFlightInfo
            // 
            this.fpFlightInfo.About = "2.5.2007.2005";
            this.fpFlightInfo.AccessibleDescription = "fpFlightInfo";
            this.fpFlightInfo.BackColor = System.Drawing.SystemColors.Control;
            this.fpFlightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFlightInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpFlightInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFlightInfo.Location = new System.Drawing.Point(3, 17);
            this.fpFlightInfo.Name = "fpFlightInfo";
            this.fpFlightInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shInTaxi,
            this.shOutTaxi});
            this.fpFlightInfo.Size = new System.Drawing.Size(823, 424);
            this.fpFlightInfo.TabIndex = 8;
            tipAppearance6.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFlightInfo.TextTipAppearance = tipAppearance6;
            this.fpFlightInfo.TextTipDelay = 100;
            this.fpFlightInfo.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Fixed;
            this.fpFlightInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // shInTaxi
            // 
            this.shInTaxi.Reset();
            this.shInTaxi.SheetName = "滑入时间";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shInTaxi.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shInTaxi.ColumnCount = 12;
            this.shInTaxi.RowCount = 1;
            this.shInTaxi.ActiveColumnIndex = 8;
            this.shInTaxi.AutoGenerateColumns = false;
            this.shInTaxi.Cells.Get(0, 0).ColumnSpan = 2;
            this.shInTaxi.Cells.Get(0, 0).Value = "平均时间";
            this.shInTaxi.Cells.Get(0, 2).ColumnSpan = 2;
            this.shInTaxi.Cells.Get(0, 4).ColumnSpan = 2;
            this.shInTaxi.Cells.Get(0, 4).Value = "85%时间";
            this.shInTaxi.Cells.Get(0, 6).ColumnSpan = 2;
            this.shInTaxi.Cells.Get(0, 8).ColumnSpan = 2;
            this.shInTaxi.Cells.Get(0, 8).Value = "15%的平均时间";
            this.shInTaxi.Cells.Get(0, 10).ColumnSpan = 2;
            this.shInTaxi.DataAutoCellTypes = false;
            this.shInTaxi.DataAutoHeadings = false;
            this.shInTaxi.DataAutoSizeColumns = false;
            this.shInTaxi.DefaultStyle.Border = lineBorder11;
            this.shInTaxi.DefaultStyle.Locked = false;
            this.shInTaxi.DefaultStyle.Parent = "DataAreaDefault";
            this.shInTaxi.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shInTaxi.RowHeader.Columns.Default.Resizable = false;
            this.shInTaxi.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shInTaxi.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shInTaxi.ZoomFactor = 0.96F;
            this.shInTaxi.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // shOutTaxi
            // 
            this.shOutTaxi.Reset();
            this.shOutTaxi.SheetName = "滑出时间";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shOutTaxi.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shOutTaxi.ColumnCount = 12;
            this.shOutTaxi.RowCount = 1;
            this.shOutTaxi.AutoGenerateColumns = false;
            this.shOutTaxi.Cells.Get(0, 0).ColumnSpan = 2;
            this.shOutTaxi.Cells.Get(0, 0).Value = "平均时间";
            this.shOutTaxi.Cells.Get(0, 2).ColumnSpan = 2;
            this.shOutTaxi.Cells.Get(0, 4).ColumnSpan = 2;
            this.shOutTaxi.Cells.Get(0, 4).Value = "85%时间";
            this.shOutTaxi.Cells.Get(0, 6).ColumnSpan = 2;
            this.shOutTaxi.Cells.Get(0, 8).ColumnSpan = 2;
            this.shOutTaxi.Cells.Get(0, 8).Value = "15%的平均时间";
            this.shOutTaxi.Cells.Get(0, 10).ColumnSpan = 2;
            this.shOutTaxi.DataAutoCellTypes = false;
            this.shOutTaxi.DataAutoHeadings = false;
            this.shOutTaxi.DataAutoSizeColumns = false;
            this.shOutTaxi.DefaultStyle.Border = lineBorder12;
            this.shOutTaxi.DefaultStyle.Locked = false;
            this.shOutTaxi.DefaultStyle.Parent = "DataAreaDefault";
            this.shOutTaxi.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shOutTaxi.RowHeader.Columns.Default.Resizable = false;
            this.shOutTaxi.SelectionBackColor = System.Drawing.Color.White;
            this.shOutTaxi.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shOutTaxi.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shOutTaxi.ZoomFactor = 0.96F;
            this.shOutTaxi.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(699, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "导 出";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(761, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "关闭";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // fmStatisticTaxiTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 554);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "fmStatisticTaxiTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "滑行时间统计";
            this.Load += new System.EventHandler(this.fmStatisticTaxiTime_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFlightInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shInTaxi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shOutTaxi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtStartFlightDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThreeCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtEndFlightDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStatistic;
        private FarPoint.Win.Spread.FpSpread fpFlightInfo;
        private FarPoint.Win.Spread.SheetView shInTaxi;
        private FarPoint.Win.Spread.SheetView shOutTaxi;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}