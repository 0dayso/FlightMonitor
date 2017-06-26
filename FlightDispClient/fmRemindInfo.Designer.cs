namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmRemindInfo
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
            this.dgvRemind = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxChangeDesk = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxChangeType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRemind)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRemind
            // 
            this.dgvRemind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRemind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRemind.Location = new System.Drawing.Point(3, 17);
            this.dgvRemind.Name = "dgvRemind";
            this.dgvRemind.RowTemplate.Height = 23;
            this.dgvRemind.Size = new System.Drawing.Size(786, 508);
            this.dgvRemind.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxChangeType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxChangeDesk);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "席位选择";
            // 
            // cbxChangeDesk
            // 
            this.cbxChangeDesk.FormattingEnabled = true;
            this.cbxChangeDesk.Items.AddRange(new object[] {
            "FOC签派放行席1",
            "FOC签派放行席2",
            "FOC签派放行席3",
            "所有席位"});
            this.cbxChangeDesk.Location = new System.Drawing.Point(71, 14);
            this.cbxChangeDesk.Name = "cbxChangeDesk";
            this.cbxChangeDesk.Size = new System.Drawing.Size(121, 20);
            this.cbxChangeDesk.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "提示类型";
            // 
            // cbxChangeType
            // 
            this.cbxChangeType.FormattingEnabled = true;
            this.cbxChangeType.Items.AddRange(new object[] {
            "ACARS信息",
            "气象信息",
            "航行通告信息",
            "MEL信息",
            "机组信息"});
            this.cbxChangeType.Location = new System.Drawing.Point(257, 14);
            this.cbxChangeType.Name = "cbxChangeType";
            this.cbxChangeType.Size = new System.Drawing.Size(121, 20);
            this.cbxChangeType.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvRemind);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(792, 528);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // fmRemindInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "fmRemindInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重要提示";
            this.Load += new System.EventHandler(this.fmRemindInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRemind)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRemind;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxChangeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxChangeDesk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}