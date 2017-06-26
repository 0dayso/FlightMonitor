using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Threading;
using AirSoft.Public.SystemFramework;
using System.Collections;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.AgentServiceBM;
using System.Collections;

namespace AirSoft.FlightMonitor.TaskBookServiceFM
{
    public partial class fmMDIMain : Form
    {
        #region ��������
        //ͬ���ڴ������ݿ��
        private System.Threading.Timer timer;   //ͬ���ڴ������ݿ�� �õ��� �̶߳�ʱ��
        private bool blnBusy = false;           //��ʾ ͬ���ڴ������ݿ�� �� �̶߳�ʱ�� �Ƿ�æ��æ���˳�

        //״̬�� ��ʾ
        private string strSynchronizeMessage = "����ͬ����";
        #endregion ��������


        public fmMDIMain()
        {
            InitializeComponent();
        }

        private void fmMDIMain_Load(object sender, EventArgs e)
        {
            TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
            taskBookServiceBF.SynchronizeDatas(); //ͬ���ڴ������ݿ��
            taskBookServiceBF.RegisterChannel();   //ע��Tcpͨ��

            //�����̶߳�ʱ������ʱ ͬ���ڴ������ݿ��
            int iRefreshInterval = 30 * 1000;
            TimerCallback timerDelegate = new TimerCallback(SynchronizeDatas);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);

            //
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
            ReturnValueSF returnValueSF = new ReturnValueSF();
            VoyageReportDAF voyageReportDAF = new VoyageReportDAF();
            BasicCrewInfoDAF basicCrewInfoDAF = new BasicCrewInfoDAF();

            //returnValueSF = taskBookServiceBF.SynchronizeDatas_tbFltReport__1();
            //dataGridView1.DataSource = returnValueSF.Dt;
            //dataGridView1.DataSource = voyageReportDAF.GetVoyageReportDataBySingleFlight("2013-09-28","HU7181","5373","HAK-PEK");
            dataGridView1.DataSource = basicCrewInfoDAF.GetProfileInfo().Tables[0];
            string s = basicCrewInfoDAF.GetProfileInfo().Tables[0].Rows.Count.ToString();
        }

        #region ͬ���ڴ������ݿ��
        /// <summary>
        /// ͬ���ڴ������ݿ��
        /// </summary>
        /// <param name="state"></param>
        public void SynchronizeDatas(object state)
        {
            //�ܿ���������������ݵ��������ʱ���
            if ((DateTime.Now >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 04:30")) &&
                (DateTime.Now <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 05:00")))
                return;
            //
            if (blnBusy)
                return;
            //
            //this.Text = "����������������ݱ���£�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";
            SysMsgBM.CaptureOffmMDIMain_Taskbook = "�������������VER 201504281655�������ݱ���£�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";

            blnBusy = true;

            ReturnValueSF returnValueSF = null;
            TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
            returnValueSF = taskBookServiceBF.SynchronizeDatas();  //ͬ���ڴ������ݿ��
            strSynchronizeMessage = returnValueSF.Message;

            blnBusy = false;

            //this.Text += "��" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";
            SysMsgBM.CaptureOffmMDIMain_Taskbook += "��" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //��ʾ����
            this.Text = SysMsgBM.CaptureOffmMDIMain_Taskbook;

            //���� ״̬�� ��Ϣ
            statusStrip1.Items[1].Text = strSynchronizeMessage;


        }
    }
}