namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmDataItemView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmDataItemView));
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstDataItem = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbDown_DataView = new System.Windows.Forms.RadioButton();
            this.rbUp_DataView = new System.Windows.Forms.RadioButton();
            this.btnSearchDataView = new System.Windows.Forms.Button();
            this.cbDataView = new System.Windows.Forms.ComboBox();
            this.lstDataView = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbDown_DataItem = new System.Windows.Forms.RadioButton();
            this.rbUp_DataItem = new System.Windows.Forms.RadioButton();
            this.btnSearchDataItem = new System.Windows.Forms.Button();
            this.cbDataItem = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(594, 55);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(30, 26);
            this.btnDown.TabIndex = 37;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(594, 12);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(30, 26);
            this.btnUp.TabIndex = 36;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(549, 314);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.Tag = "删除";
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(549, 211);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 32;
            this.btnAdd.Tag = "添加";
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstDataItem
            // 
            this.lstDataItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstDataItem.ItemHeight = 12;
            this.lstDataItem.Location = new System.Drawing.Point(3, 55);
            this.lstDataItem.Name = "lstDataItem";
            this.lstDataItem.Size = new System.Drawing.Size(517, 484);
            this.lstDataItem.TabIndex = 41;
            this.lstDataItem.DoubleClick += new System.EventHandler(this.lstDataItem_DoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.lstDataView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(649, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 542);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已选项";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbDown_DataView);
            this.panel3.Controls.Add(this.rbUp_DataView);
            this.panel3.Controls.Add(this.btnSearchDataView);
            this.panel3.Controls.Add(this.cbDataView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(517, 38);
            this.panel3.TabIndex = 43;
            // 
            // rbDown_DataView
            // 
            this.rbDown_DataView.AutoSize = true;
            this.rbDown_DataView.Checked = true;
            this.rbDown_DataView.Location = new System.Drawing.Point(442, 12);
            this.rbDown_DataView.Name = "rbDown_DataView";
            this.rbDown_DataView.Size = new System.Drawing.Size(47, 16);
            this.rbDown_DataView.TabIndex = 3;
            this.rbDown_DataView.TabStop = true;
            this.rbDown_DataView.Text = "向下";
            this.rbDown_DataView.UseVisualStyleBackColor = true;
            // 
            // rbUp_DataView
            // 
            this.rbUp_DataView.AutoSize = true;
            this.rbUp_DataView.Location = new System.Drawing.Point(389, 12);
            this.rbUp_DataView.Name = "rbUp_DataView";
            this.rbUp_DataView.Size = new System.Drawing.Size(47, 16);
            this.rbUp_DataView.TabIndex = 2;
            this.rbUp_DataView.Text = "向上";
            this.rbUp_DataView.UseVisualStyleBackColor = true;
            // 
            // btnSearchDataView
            // 
            this.btnSearchDataView.Location = new System.Drawing.Point(284, 6);
            this.btnSearchDataView.Name = "btnSearchDataView";
            this.btnSearchDataView.Size = new System.Drawing.Size(75, 23);
            this.btnSearchDataView.TabIndex = 1;
            this.btnSearchDataView.Text = "搜 索";
            this.btnSearchDataView.UseVisualStyleBackColor = true;
            this.btnSearchDataView.Click += new System.EventHandler(this.btnSearchDataView_Click);
            // 
            // cbDataView
            // 
            this.cbDataView.FormattingEnabled = true;
            this.cbDataView.Location = new System.Drawing.Point(3, 8);
            this.cbDataView.Name = "cbDataView";
            this.cbDataView.Size = new System.Drawing.Size(250, 20);
            this.cbDataView.TabIndex = 0;
            // 
            // lstDataView
            // 
            this.lstDataView.AllowDrop = true;
            this.lstDataView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstDataView.ItemHeight = 12;
            this.lstDataView.Location = new System.Drawing.Point(3, 55);
            this.lstDataView.Name = "lstDataView";
            this.lstDataView.Size = new System.Drawing.Size(517, 484);
            this.lstDataView.TabIndex = 33;
            this.lstDataView.DragOver += new System.Windows.Forms.DragEventHandler(this.lstDataView_DragOver);
            this.lstDataView.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstDataView_DragDrop);
            this.lstDataView.DoubleClick += new System.EventHandler(this.lstDataView_DoubleClick);
            this.lstDataView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstDataView_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Controls.Add(this.btnAddAll);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Controls.Add(this.btnUp);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1172, 542);
            this.panel1.TabIndex = 31;
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(549, 263);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAll.TabIndex = 41;
            this.btnDeleteAll.Tag = "删除";
            this.btnDeleteAll.Text = "全部删除";
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(549, 155);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(75, 23);
            this.btnAddAll.TabIndex = 40;
            this.btnAddAll.Tag = "全部添加";
            this.btnAddAll.Text = "全部添加";
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.lstDataItem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 542);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "可选项";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbDown_DataItem);
            this.panel2.Controls.Add(this.rbUp_DataItem);
            this.panel2.Controls.Add(this.btnSearchDataItem);
            this.panel2.Controls.Add(this.cbDataItem);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(517, 38);
            this.panel2.TabIndex = 42;
            // 
            // rbDown_DataItem
            // 
            this.rbDown_DataItem.AutoSize = true;
            this.rbDown_DataItem.Checked = true;
            this.rbDown_DataItem.Location = new System.Drawing.Point(439, 12);
            this.rbDown_DataItem.Name = "rbDown_DataItem";
            this.rbDown_DataItem.Size = new System.Drawing.Size(47, 16);
            this.rbDown_DataItem.TabIndex = 3;
            this.rbDown_DataItem.TabStop = true;
            this.rbDown_DataItem.Text = "向下";
            this.rbDown_DataItem.UseVisualStyleBackColor = true;
            // 
            // rbUp_DataItem
            // 
            this.rbUp_DataItem.AutoSize = true;
            this.rbUp_DataItem.Location = new System.Drawing.Point(386, 12);
            this.rbUp_DataItem.Name = "rbUp_DataItem";
            this.rbUp_DataItem.Size = new System.Drawing.Size(47, 16);
            this.rbUp_DataItem.TabIndex = 2;
            this.rbUp_DataItem.Text = "向上";
            this.rbUp_DataItem.UseVisualStyleBackColor = true;
            // 
            // btnSearchDataItem
            // 
            this.btnSearchDataItem.Location = new System.Drawing.Point(281, 6);
            this.btnSearchDataItem.Name = "btnSearchDataItem";
            this.btnSearchDataItem.Size = new System.Drawing.Size(75, 23);
            this.btnSearchDataItem.TabIndex = 1;
            this.btnSearchDataItem.Text = "搜 索";
            this.btnSearchDataItem.UseVisualStyleBackColor = true;
            this.btnSearchDataItem.Click += new System.EventHandler(this.btnSearchDataItem_Click);
            // 
            // cbDataItem
            // 
            this.cbDataItem.FormattingEnabled = true;
            this.cbDataItem.Location = new System.Drawing.Point(3, 8);
            this.cbDataItem.Name = "cbDataItem";
            this.cbDataItem.Size = new System.Drawing.Size(250, 20);
            this.cbDataItem.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(821, 566);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 30;
            this.btnClose.Tag = "删除";
            this.btnClose.Text = "关 闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(674, 566);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 29;
            this.btnOK.Tag = "添加";
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // fmDataItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 601);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmDataItemView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视图设置";
            this.Load += new System.EventHandler(this.fmDataItemView_Load);
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lstDataItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstDataView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbDataItem;
        private System.Windows.Forms.Button btnSearchDataItem;
        private System.Windows.Forms.RadioButton rbDown_DataItem;
        private System.Windows.Forms.RadioButton rbUp_DataItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbDown_DataView;
        private System.Windows.Forms.RadioButton rbUp_DataView;
        private System.Windows.Forms.Button btnSearchDataView;
        private System.Windows.Forms.ComboBox cbDataView;
    }
}