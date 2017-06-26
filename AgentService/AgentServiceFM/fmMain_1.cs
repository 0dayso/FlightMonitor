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
using AirSoft.FlightMonitor.AgentServiceBF;
using System.Threading;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.AgentServiceFM
{
    public partial class fmMain_1 : Form
    {
        //
        private System.Threading.Timer timer;
        private System.Threading.Timer timer_1;
        
        private bool blnBusy = false;
        private bool blnBusy_1 = false;

        /// <summary>
        /// 同步内存表和数据库表
        /// </summary>
        /// <param name="state"></param>
        public void SynchronizeDatas(object state)
        {
            //
            if (blnBusy)
                return;
            //
            this.Text = "代理服务：数据表更新（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

            blnBusy = true;

            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
            agentServiceBF.SynchronizeDatas();  //同步内存表和数据库表

            blnBusy = false;

            this.Text += "（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void SynchronizeDatas_1(object state)
        {
            //
            if (blnBusy_1)
                return;
            //

            blnBusy_1 = true;

            DataRow dataRow = null;

            dataRow = AgentServiceDA.AgentServiceDAF.dtProcRecords.NewRow();
            dataRow["cnvcProcName"] = "test_1";
            dataRow["cndOprationTime"] = DateTime.Now;
            dataRow["cnvcOprationResult"] = "成功_1";
            dataRow["cniOprationCount"] = 5;
            dataRow["cniLengthBeforeCompress"] = 2000;
            dataRow["cniLengthAfterCompress"] = 200;
            dataRow["cnfProcTimes"] = 2.2;
            dataRow["cnfCompressTimes"] = 2.6;
            AgentServiceDA.AgentServiceDAF.dtProcRecords.Rows.Add(dataRow);

            blnBusy_1 = false;

        }


        public fmMain_1()
        {
            InitializeComponent();
        }

        private void fmMain_1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();

            agentServiceBF.RegisterChannel();   //注册Tcp通道
            agentServiceBF.SynchronizeDatas();  //同步内存表和数据库表
            agentServiceBF.InitializeDAL();     //初始化 AgentServiceDAF 类(记录表)
            //dataGridView1.DataSource = agentServiceBF.GetDatatable("dtProcRecords").Dt.DefaultView;
            dataGridView1.DataSource = agentServiceBF.GetDatatable("dtProcRecords").Dt;
            dataGridView2.DataSource = agentServiceBF.GetDatatable("dtProcAnalysis").Dt.DefaultView;
            //调用线程定时器，定时 同步内存表和数据库表
            int iRefreshInterval = 20 * 1000;
            TimerCallback timerDelegate = new TimerCallback(SynchronizeDatas);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);

            TimerCallback timerDelegate_1 = new TimerCallback(SynchronizeDatas_1);
            timer_1 = new System.Threading.Timer(timerDelegate_1, null, 0, 1000);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF;
            DataTable dataTable;

            AgentServiceBF.AgentServiceBF agentServiceBF = new AirSoft.FlightMonitor.AgentServiceBF.AgentServiceBF();
            rvSF = agentServiceBF.GetDatatable("vw_Legs");
            dataTable = rvSF.Dt;
            dataGridView1.DataSource = dataTable.DefaultView;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF;
            DataTable dataTable;

            AgentServiceBF.AgentServiceBF agentServiceBF = new AirSoft.FlightMonitor.AgentServiceBF.AgentServiceBF();
            rvSF = agentServiceBF.GetDatatable("vw_FlightChangeRecord");
            dataTable = rvSF.Dt;
            dataGridView1.DataSource = dataTable.DefaultView;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF;
            DataTable dataTable;

            AgentServiceBF.AgentServiceBF agentServiceBF = new AirSoft.FlightMonitor.AgentServiceBF.AgentServiceBF();
            rvSF = agentServiceBF.GetDatatable("tbLegs");
            dataTable = rvSF.Dt;
            dataGridView1.DataSource = dataTable.DefaultView;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataRow dataRow = null;

            dataRow = AgentServiceDA.AgentServiceDAF.dtProcRecords.NewRow();
            dataRow["cnvcProcName"] = "test";
            dataRow["cndOprationTime"] = DateTime.Now;
            dataRow["cnvcOprationResult"] = "成功";
            dataRow["cniOprationCount"] = 5;
            dataRow["cniLengthBeforeCompress"] = 1000;
            dataRow["cniLengthAfterCompress"] = 100;
            dataRow["cnfProcTimes"] = 4.2;
            dataRow["cnfCompressTimes"] = 5.6;
            AgentServiceDA.AgentServiceDAF.dtProcRecords.Rows.Add(dataRow);

        }
    }
}