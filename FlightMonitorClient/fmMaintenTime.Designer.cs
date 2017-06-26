namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmMaintenTime
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNewContent = new System.Windows.Forms.TextBox();
            this.btnNow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(141, 31);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(7, 31);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保 存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNewContent
            // 
            this.txtNewContent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewContent.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNewContent.Location = new System.Drawing.Point(7, 5);
            this.txtNewContent.MaxLength = 4;
            this.txtNewContent.Name = "txtNewContent";
            this.txtNewContent.Size = new System.Drawing.Size(152, 21);
            this.txtNewContent.TabIndex = 8;
            // 
            // btnNow
            // 
            this.btnNow.Location = new System.Drawing.Point(165, 5);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(32, 23);
            this.btnNow.TabIndex = 11;
            this.btnNow.Text = "Now";
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // fmMaintenTime
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 59);
            this.Controls.Add(this.btnNow);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNewContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmMaintenTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时间";
            this.Load += new System.EventHandler(this.fmMaintenTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNewContent;
        private System.Windows.Forms.Button btnNow;
    }
}