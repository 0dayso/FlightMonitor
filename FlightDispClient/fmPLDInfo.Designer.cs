namespace AirSoft.FlightMonitor.FlightDispClient
{
    partial class fmPLDInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPsgs = new System.Windows.Forms.TextBox();
            this.txtBags = new System.Windows.Forms.TextBox();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.txtTotalPLD = new System.Windows.Forms.TextBox();
            this.btnGetPLD = new System.Windows.Forms.Button();
            this.btnStore = new System.Windows.Forms.Button();
            this.lblFlightNo = new System.Windows.Forms.Label();
            this.lblAircraft = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "旅客人数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "行李重量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "货物重量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "总业载";
            // 
            // txtPsgs
            // 
            this.txtPsgs.Location = new System.Drawing.Point(72, 36);
            this.txtPsgs.Name = "txtPsgs";
            this.txtPsgs.Size = new System.Drawing.Size(100, 21);
            this.txtPsgs.TabIndex = 4;
            // 
            // txtBags
            // 
            this.txtBags.Location = new System.Drawing.Point(72, 70);
            this.txtBags.Name = "txtBags";
            this.txtBags.Size = new System.Drawing.Size(100, 21);
            this.txtBags.TabIndex = 5;
            // 
            // txtCargo
            // 
            this.txtCargo.Location = new System.Drawing.Point(72, 104);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.Size = new System.Drawing.Size(100, 21);
            this.txtCargo.TabIndex = 6;
            // 
            // txtTotalPLD
            // 
            this.txtTotalPLD.Location = new System.Drawing.Point(72, 138);
            this.txtTotalPLD.Name = "txtTotalPLD";
            this.txtTotalPLD.Size = new System.Drawing.Size(100, 21);
            this.txtTotalPLD.TabIndex = 7;
            // 
            // btnGetPLD
            // 
            this.btnGetPLD.Location = new System.Drawing.Point(13, 185);
            this.btnGetPLD.Name = "btnGetPLD";
            this.btnGetPLD.Size = new System.Drawing.Size(75, 23);
            this.btnGetPLD.TabIndex = 8;
            this.btnGetPLD.Text = "提取业载";
            this.btnGetPLD.UseVisualStyleBackColor = true;
            this.btnGetPLD.Click += new System.EventHandler(this.btnGetPLD_Click);
            // 
            // btnStore
            // 
            this.btnStore.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStore.Location = new System.Drawing.Point(97, 185);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(75, 23);
            this.btnStore.TabIndex = 9;
            this.btnStore.Text = "保存";
            this.btnStore.UseVisualStyleBackColor = true;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // lblFlightNo
            // 
            this.lblFlightNo.AutoSize = true;
            this.lblFlightNo.Location = new System.Drawing.Point(13, 13);
            this.lblFlightNo.Name = "lblFlightNo";
            this.lblFlightNo.Size = new System.Drawing.Size(41, 12);
            this.lblFlightNo.TabIndex = 10;
            this.lblFlightNo.Text = "label5";
            // 
            // lblAircraft
            // 
            this.lblAircraft.AutoSize = true;
            this.lblAircraft.Location = new System.Drawing.Point(81, 13);
            this.lblAircraft.Name = "lblAircraft";
            this.lblAircraft.Size = new System.Drawing.Size(41, 12);
            this.lblAircraft.TabIndex = 11;
            this.lblAircraft.Text = "label5";
            // 
            // fmPLDInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.lblAircraft);
            this.Controls.Add(this.lblFlightNo);
            this.Controls.Add(this.btnStore);
            this.Controls.Add(this.btnGetPLD);
            this.Controls.Add(this.txtTotalPLD);
            this.Controls.Add(this.txtCargo);
            this.Controls.Add(this.txtBags);
            this.Controls.Add(this.txtPsgs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "fmPLDInfo";
            this.Text = "业载信息";
            this.Load += new System.EventHandler(this.fmPLDInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPsgs;
        private System.Windows.Forms.TextBox txtBags;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.TextBox txtTotalPLD;
        private System.Windows.Forms.Button btnGetPLD;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.Label lblFlightNo;
        private System.Windows.Forms.Label lblAircraft;
    }
}