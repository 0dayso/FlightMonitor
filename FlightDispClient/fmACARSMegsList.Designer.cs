namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmACARSMegsList
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
            this.txtMegsContent = new System.Windows.Forms.TextBox();
            this.fpsMegsList = new FarPoint.Win.Spread.FpSpread();
            this.shMegsList = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiUpMegs = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fpsMegsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shMegsList)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMegsContent
            // 
            this.txtMegsContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMegsContent.Location = new System.Drawing.Point(0, 0);
            this.txtMegsContent.Multiline = true;
            this.txtMegsContent.Name = "txtMegsContent";
            this.txtMegsContent.Size = new System.Drawing.Size(385, 549);
            this.txtMegsContent.TabIndex = 1;
            // 
            // fpsMegsList
            // 
            this.fpsMegsList.About = "2.5.2007.2005";
            this.fpsMegsList.AccessibleDescription = "fpsMegsList";
            this.fpsMegsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpsMegsList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsMegsList.Location = new System.Drawing.Point(0, 0);
            this.fpsMegsList.Name = "fpsMegsList";
            this.fpsMegsList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.shMegsList});
            this.fpsMegsList.Size = new System.Drawing.Size(403, 549);
            this.fpsMegsList.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpsMegsList.TextTipAppearance = tipAppearance1;
            this.fpsMegsList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpsMegsList.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpsMegsList_CellClick);
            // 
            // shMegsList
            // 
            this.shMegsList.Reset();
            this.shMegsList.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.shMegsList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.shMegsList.ColumnCount = 0;
            this.shMegsList.RowCount = 0;
            this.shMegsList.AutoGenerateColumns = false;
            this.shMegsList.DataAutoCellTypes = false;
            this.shMegsList.DataAutoHeadings = false;
            this.shMegsList.DataAutoSizeColumns = false;
            this.shMegsList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.shMegsList.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.shMegsList.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.shMegsList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpsMegsList.SetActiveViewport(1, 1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fpsMegsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtMegsContent);
            this.splitContainer1.Size = new System.Drawing.Size(792, 549);
            this.splitContainer1.SplitterDistance = 403;
            this.splitContainer1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUpMegs});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiUpMegs
            // 
            this.tsmiUpMegs.Name = "tsmiUpMegs";
            this.tsmiUpMegs.Size = new System.Drawing.Size(65, 20);
            this.tsmiUpMegs.Text = "上传报文";
            this.tsmiUpMegs.Click += new System.EventHandler(this.tsmiUpMegs_Click);
            // 
            // fmACARSMegsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fmACARSMegsList";
            this.Load += new System.EventHandler(this.fmACARSMegsList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpsMegsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shMegsList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMegsContent;
        private FarPoint.Win.Spread.FpSpread fpsMegsList;
        private FarPoint.Win.Spread.SheetView shMegsList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpMegs;
    }
}