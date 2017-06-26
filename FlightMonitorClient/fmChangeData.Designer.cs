namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    partial class fmChangeData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmChangeData));
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvChangeContent = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtFlightNo = new System.Windows.Forms.TextBox();
            this.dtFlightDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(758, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "�� ��";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvChangeContent);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(845, 301);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            // 
            // lvChangeContent
            // 
            this.lvChangeContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvChangeContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvChangeContent.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvChangeContent.FullRowSelect = true;
            this.lvChangeContent.GridLines = true;
            this.lvChangeContent.Location = new System.Drawing.Point(3, 17);
            this.lvChangeContent.MultiSelect = false;
            this.lvChangeContent.Name = "lvChangeContent";
            this.lvChangeContent.Size = new System.Drawing.Size(839, 281);
            this.lvChangeContent.SmallImageList = this.imageList1;
            this.lvChangeContent.TabIndex = 12;
            this.lvChangeContent.TabStop = false;
            this.lvChangeContent.UseCompatibleStateImageBehavior = false;
            this.lvChangeContent.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "����Ա";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "��������";
            this.columnHeader2.Width = 450;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "����ʱ��";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "��������";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 80;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // txtFlightNo
            // 
            this.txtFlightNo.Location = new System.Drawing.Point(245, 16);
            this.txtFlightNo.Name = "txtFlightNo";
            this.txtFlightNo.Size = new System.Drawing.Size(119, 21);
            this.txtFlightNo.TabIndex = 106;
            // 
            // dtFlightDate
            // 
            this.dtFlightDate.Location = new System.Drawing.Point(69, 16);
            this.dtFlightDate.Name = "dtFlightDate";
            this.dtFlightDate.Size = new System.Drawing.Size(119, 21);
            this.dtFlightDate.TabIndex = 105;
            this.dtFlightDate.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 349);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(845, 48);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(758, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 107;
            this.btnQuery.Text = "�� ѯ";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cmbType
            // 
            this.cmbType.Items.AddRange(new object[] {
            "����",
            "��������",
            "�����",
            "����",
            "��������",
            "��ɻ���",
            "�������",
            "����ʱ��|�ƻ�",
            "����ʱ��|����",
            "����ʱ��|��̬",
            "ACARS��ɶ�̬",
            "ACARS���ʱ��",
            "ACARS�յ�ʱ��",
            "��������ԭ��",
            "��������ԭ��",
            "���౸��ԭ��",
            "���۱��ϳ���|��Ϣ",
            "���۱��ϳ���|����",
            "���۱��ϳ���|����",
            "VIP",
            "ֵ������",
            "��ת�����ÿ�",
            "�ÿ�����",
            "��������",
            "��������",
            "����״̬",
            "���۱�ע",
            "ͣ��λ",
            "����",
            "����ʱ��|�ƻ�",
            "����ʱ��|����",
            "��ʼ����ʱ��",
            "��վʱ��",
            "���ɺ���",
            "ֵ����̨",
            "�����",
            "�ǻ���",
            "�Ͳտ���",
            "���۱��ϳ���|��Ϣ",
            "���۱��ϳ���|����",
            "���۱��ϳ���|����",
            "���鵽��|����",
            "���鵽��|����",
            "�ɻ�����|��ʼ",
            "�ɻ�����|����",
            "��������ʱ��",
            "ǩ�ɷ���ʱ��",
            "�������ϴ�ӡʱ��",
            "����|��λ",
            "����|����",
            "�ɻ�״̬",
            "�¿����",
            "�Ͳ����|��ʼ",
            "�Ͳ����|����",
            "����ˮ|��ʼ",
            "����ˮ|����",
            "�Ͳչ�Ӧ|��ʼ",
            "�Ͳչ�Ӧ|����",
            "����|��ʼ",
            "����|����",
            "���տ���",
            "װ��ʱ��",
            "װ����ʱ��",
            "���չر�",
            "�ϳ�����",
            "��ˮ������ʱ��",
            "�ڶɳ�����|�װ�",
            "�ڶɳ�����|ĩ��",
            "�ϸ�",
            "���ݳ�",
            "֪ͨ�Ͽ�",
            "�ǻ�",
            "��������",
            "ֵ������",
            "��ת�����ÿ�",
            "�ӻ���",
            "�ÿ�����|����",
            "�ÿ�����|��ͯ",
            "�ÿ�����|Ӥ��",
            "�ÿ�����|ͷ�Ȳ�",
            "�ÿ�����|�����",
            "�ÿ�����|���ò�",
            "��������",
            "�ÿ�����",
            "�Ͽ����",
            "�յ��ʹ�",
            "�Ͳչر�",
            "����ʱ��",
            "�ڲ�����",
            "�ɻ��Ƴ�",
            "��ɶ�̬",
            "ACARS��ɶ�̬",
            "���ϳ�ʱԭ��",
            "���۷�������ԭ��",
            "��������ԭ��",
            "���౸��ԭ��",
            "��������",
            "�ʼ�����",
            "��������",
            "��������",
            "���ı�ע",
            "�������",
            "VIP",
            "������",
            "���̺���",
            "������",
            "��������",
            "���˱�ע",
            "����״̬",
            "���۱�ע",
            "�ֳ�ָ����Ա|����",
            "�ֳ�ָ����Ա|����",
            "�ֳ�ָ����Ա|�ⳡ",
            "����Ա",
            "��������ʱ��",
            "ʵ������ʱ��",
            "�ص㱣�Ϻ���"});
            this.cmbType.Location = new System.Drawing.Point(409, 16);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(119, 20);
            this.cmbType.TabIndex = 113;
            this.cmbType.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(372, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 112;
            this.label4.Text = "����";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 109;
            this.label2.Text = "�����";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 108;
            this.label1.Text = "��������";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.txtFlightNo);
            this.groupBox1.Controls.Add(this.dtFlightDate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(845, 48);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // fmChangeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 397);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "fmChangeData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "�������";
            this.Load += new System.EventHandler(this.fmChangeData_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvChangeContent;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtFlightNo;
        private System.Windows.Forms.DateTimePicker dtFlightDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}