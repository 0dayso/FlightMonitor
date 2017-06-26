using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmRelease : Form
    {
        public fmRelease()
        {
            InitializeComponent();
        }

        private void fmRelease_Load(object sender, EventArgs e)
        {
            this.Text = "����˵��";

            StringBuilder strReleaseNote = new StringBuilder();
            strReleaseNote.Append("2008-09-23 �������ݣ�\r\n\r\n");
            strReleaseNote.Append("1�������˽��ۺ�����غ󺽰���˳���������⣻\r\n");
            strReleaseNote.Append("2�������˽��ۺ���ġ���λ��ʱ�̣����ֵ�ʱ�̣��ͳ��ۺ���ġ��Ƴ���ʱ�̣����ֵ�ʱ�̣���\r\n");
            strReleaseNote.Append("3���޸��˲����ֶε���ʾ���ƣ�\r\n");
            strReleaseNote.Append("   ���ۺ���ġ�����ʱ��|���� ��Ϊ�� ������ʱ��|Ԥ�\r\n");
            strReleaseNote.Append("   ���ۺ���ġ�����ʱ��|��̬�� ��Ϊ�� ������ʱ��|��ء�\r\n");
            strReleaseNote.Append("   ���ۺ���ġ�����ʱ��|�ƻ��� ��Ϊ�� ������ʱ��|�ƻ���\r\n");
            strReleaseNote.Append("   ���ۺ���ġ�����ʱ��|���� ��Ϊ�� ������ʱ��|Ԥ�ơ�\r\n");
            strReleaseNote.Append("   ���ۺ���ġ���ɶ�̬��     ��Ϊ�� ������ʱ��|��ɡ���\r\n");
            strReleaseNote.Append("4�����ۺ����Ƿ����󣬶���ʾ������ʱ��|Ԥ��򡰳���ʱ��|Ԥ�ơ�����ԭ����ʱ�䣩������������󣬸õ�Ԫ������ʾ��ɫ��\r\n");
            strReleaseNote.Append("5���������û���¼����ʾ����ȷ�����⡣");

            txtReleaseNote.Font = new Font("����", 10, FontStyle.Regular);
            txtReleaseNote.Text = strReleaseNote.ToString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //�����´β�����ʾ����˵��
            if (cbxIsShowNextTime.Checked)
            {
                ConfigSettings.WriteSetting("ShowUpdateRelease", "0");
            }
            this.Close();
        }
    }
}