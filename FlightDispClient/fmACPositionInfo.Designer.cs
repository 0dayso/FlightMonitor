namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmACPositionInfo
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
            this.dgvACPositionInf = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvACPositionInf)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvACPositionInf
            // 
            this.dgvACPositionInf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvACPositionInf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvACPositionInf.Location = new System.Drawing.Point(0, 0);
            this.dgvACPositionInf.Name = "dgvACPositionInf";
            this.dgvACPositionInf.RowTemplate.Height = 23;
            this.dgvACPositionInf.Size = new System.Drawing.Size(592, 573);
            this.dgvACPositionInf.TabIndex = 0;
            // 
            // fmACPositionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 573);
            this.Controls.Add(this.dgvACPositionInf);
            this.Name = "fmACPositionInfo";
            this.Text = "飞机位置信息";
            this.Load += new System.EventHandler(this.fmACPositionInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvACPositionInf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvACPositionInf;
    }
}