using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmRemindInfo : Form
    {
        public fmRemindInfo(PositionNameBM deskNameBM)
        {
            InitializeComponent();
        }

        private void fmRemindInfo_Load(object sender, EventArgs e)
        {
            DataTable dtRemindInfo = new DataTable();

            dtRemindInfo.Columns.Add("��ʾ����");
            dtRemindInfo.Columns.Add("�ɻ���");
            dtRemindInfo.Columns.Add("�����");
            dtRemindInfo.Columns.Add("��ɻ���");
            dtRemindInfo.Columns.Add("Ŀ�Ļ���");
            dtRemindInfo.Columns.Add("��ʾ����");

            DataRow dr1 = dtRemindInfo.NewRow();
            dr1["��ʾ����"] = "ACARS";
            dr1["�ɻ���"] = "B2490";
            dr1["�����"] = "HU7181";
            dr1["��ɻ���"] = "����";
            dr1["Ŀ�Ļ���"] = "����";
            dr1["��ʾ����"] = "�ɻ��Ƴ�";
            dtRemindInfo.Rows.Add(dr1);

            DataRow dr2 = dtRemindInfo.NewRow();
            dr2["��ʾ����"] = "����";
            dr2["�ɻ���"] = "";
            dr2["�����"] = "";
            dr2["��ɻ���"] = "����";
            dr2["Ŀ�Ļ���"] = "̫ԭ";
            dr2["��ʾ����"] = "���";
            dtRemindInfo.Rows.Add(dr2);

            DataRow dr3 = dtRemindInfo.NewRow();
            dr3["��ʾ����"] = "����ͨ��";
            dr3["�ɻ���"] = "";
            dr3["�����"] = "";
            dr3["��ɻ���"] = "��³ľ��";
            dr3["Ŀ�Ļ���"] = "����";
            dr3["��ʾ����"] = "�վ��";
            dtRemindInfo.Rows.Add(dr3);

            DataRow dr4 = dtRemindInfo.NewRow();
            dr4["��ʾ����"] = "MEL";
            dr4["�ɻ���"] = "B2490";
            dr4["�����"] = "";
            dr4["��ɻ���"] = "";
            dr4["Ŀ�Ļ���"] = "";
            dr4["��ʾ����"] = "����������";
            dtRemindInfo.Rows.Add(dr4);

            DataRow dr5 = dtRemindInfo.NewRow();
            dr5["��ʾ����"] = "����";
            dr5["�ɻ���"] = "B2561";
            dr5["�����"] = "HU7358";
            dr5["��ɻ���"] = "̫ԭ";
            dr5["Ŀ�Ļ���"] = "����";
            dr5["��ʾ����"] = "��������";
            dtRemindInfo.Rows.Add(dr5);

            //DataRow dr1 = dtRemindInfo.NewRow();
            //dr1["��ʾ����"] = "ACARS";
            //dr1["�ɻ���"] = "B2490";
            //dr1["�����"] = "HU7181";
            //dr1["��ɻ���"] = "����";
            //dr1["Ŀ�Ļ���"] = "����";
            //dr1["��ʾ����"] = "�ɻ��Ƴ�";
            //dtRemindInfo.Rows.Add(dr1);

            dgvRemind.DataSource = dtRemindInfo;
            dgvRemind.DataMember = dtRemindInfo.TableName;
        }
    }
}