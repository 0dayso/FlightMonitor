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
using AirSoft.FlightMonitor.AgentServiceBM;
using System.Threading;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.AgentServiceFM
{
    public partial class fmMain : Form
    {
        #region 声明变量
        //同步内存表和数据库表
        private System.Threading.Timer timer;   //同步内存表和数据库表 用到的 线程定时器
        private bool blnBusy = false;           //表示 同步内存表和数据库表 的 线程定时器 是否繁忙，忙则退出

        //DataGridView 显示 
        bool blnBusy_Show = false;
        private DataTable dtProcRecords = null;
        private DataTable dtProcAnalysis = null;
        #endregion

        #region 同步内存表和数据库表
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
        #endregion

        //
        public fmMain()
        {
            InitializeComponent();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
            ProcRecordsBF procRecordsBF = new ProcRecordsBF();
            ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();

            agentServiceBF.RegisterChannel();   //注册Tcp通道
            agentServiceBF.SynchronizeDatas();  //同步内存表和数据库表
            agentServiceBF.InitializeDAL();     //初始化 AgentServiceDAF 类(记录表)
            InitShow_2();                       //初始化 DataGridView 标题栏

            //调用线程定时器，定时 同步内存表和数据库表
            int iRefreshInterval = 20 * 1000;
            TimerCallback timerDelegate = new TimerCallback(SynchronizeDatas);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);
            //
            timer1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ReturnValueSF rvSF ;
            DataTable dataTable;

            AgentServiceBF.AgentServiceBF agentServiceBF = new AirSoft.FlightMonitor.AgentServiceBF.AgentServiceBF();
            rvSF = agentServiceBF.GetDatatable("tbLegs");
            dataTable = rvSF.Dt;
            dataGridView1.DataSource = dataTable.DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF;
            DataTable dataTable;

            AgentServiceBF.AgentServiceBF agentServiceBF = new AirSoft.FlightMonitor.AgentServiceBF.AgentServiceBF();
            rvSF = agentServiceBF.GetDatatable("vw_Legs");
            dataTable = rvSF.Dt;
            dataGridView1.DataSource = dataTable.DefaultView;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF;
            DataTable dataTable;

            AgentServiceBF.AgentServiceBF agentServiceBF = new AirSoft.FlightMonitor.AgentServiceBF.AgentServiceBF();
            rvSF = agentServiceBF.GetDatatable("vw_FlightChangeRecord");
            dataTable = rvSF.Dt;
            dataGridView1.DataSource = dataTable.DefaultView;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
            ProcRecordsBF procRecordsBF = new ProcRecordsBF();
            ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();

            agentServiceBF.RegisterChannel();   //注册Tcp通道
            agentServiceBF.SynchronizeDatas();  //同步内存表和数据库表
            agentServiceBF.InitializeDAL();     //初始化 AgentServiceDAF 类(记录表)

            //调用线程定时器，定时 同步内存表和数据库表
            int iRefreshInterval = 20 * 1000;
            TimerCallback timerDelegate = new TimerCallback(SynchronizeDatas);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);
            //
            timer1.Enabled = true;

            string s = "";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (blnBusy_Show)
                return;

            blnBusy_Show = true;

            ShowInDataGridView_2();

            blnBusy_Show = false;
        }

        #region DataGridView 显示

        /// <summary>
        /// 定时更换 DataGridView 的数据源
        /// </summary>
        /// <returns></returns>
        private ReturnValueSF ShowInDataGridView_1()
        {
            //
            ReturnValueSF returnValueSF = new ReturnValueSF();

            DataTable dtProcRecords = null;
            DataTable dtProcAnalysis = null;


            //
            try
            {
                AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
                dtProcRecords = agentServiceBF.GetDatatable("dtProcRecords").Dt.Copy();
                dtProcAnalysis = agentServiceBF.GetDatatable("dtProcAnalysis").Dt.Copy();

                dataGridView1.DataSource = dtProcRecords.DefaultView;
                for (int iIndex = 0; iIndex < dataGridView1.Columns.Count; iIndex++)
                    dataGridView1.Columns[iIndex].HeaderText = (dataGridView1.DataSource as DataView).Table.Columns[iIndex].Caption;
                dataGridView2.DataSource = dtProcAnalysis.DefaultView;
                for (int iIndex = 0; iIndex < dataGridView2.Columns.Count; iIndex++)
                    dataGridView2.Columns[iIndex].HeaderText = (dataGridView2.DataSource as DataView).Table.Columns[iIndex].Caption;
            }
            catch(Exception ex)
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = ex.Message;
            }


            //返回结果
            return returnValueSF;

        }

        /// <summary>
        /// 定时更新 DataGridView 的数据源
        /// </summary>
        /// <returns></returns>
        private ReturnValueSF ShowInDataGridView_2()
        {
            //
            ReturnValueSF returnValueSF = new ReturnValueSF();
            
            int iCountsOfProcRecords = 0, iCountsOfProcRecords_DAF = 0;
            DataTable dtProcRecords_DAF = null, dtProcAnalysis_DAF = null;
            int iCountsOfProcAnalysis_DAF = 0;


            //
            try
            {
                //
                AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
                dtProcRecords_DAF = agentServiceBF.GetDatatable("dtProcRecords").Dt;
                iCountsOfProcRecords = dtProcRecords.Rows.Count;
                iCountsOfProcRecords_DAF = dtProcRecords_DAF.Rows.Count;

                ProcRecordsBF procRecordsBF = new ProcRecordsBF();
                for (int iIndex = iCountsOfProcRecords; iIndex < iCountsOfProcRecords_DAF; iIndex++)
                {
                    ProcRecordsBM procRecordsBM = new ProcRecordsBM(dtProcRecords_DAF.Rows[iIndex]);
                    procRecordsBF.AddRecord(dtProcRecords, procRecordsBM);
                }

                dtProcAnalysis_DAF = agentServiceBF.GetDatatable("dtProcAnalysis").Dt;
                iCountsOfProcAnalysis_DAF = dtProcAnalysis_DAF.Rows.Count;
                ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();
                for (int iIndex = 0; iIndex < iCountsOfProcAnalysis_DAF; iIndex++)
                {
                    ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(dtProcAnalysis_DAF.Rows[iIndex]);
                    procAnalysisBF.UpdateRecord(dtProcAnalysis, procAnalysisBM);
                }

            }
            catch (Exception ex)
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = ex.Message;
            }


            //返回结果
            return returnValueSF;

        }

        private ReturnValueSF InitShow_2()
        {
            //
            ReturnValueSF returnValueSF = new ReturnValueSF();


            //
            try
            {
                ProcRecordsBF procRecordsBF = new ProcRecordsBF();
                dtProcRecords = procRecordsBF.CreateDatatable().Dt;
                dataGridView1.DataSource = dtProcRecords.DefaultView;
                for (int iIndex = 0; iIndex < dataGridView1.Columns.Count; iIndex++)
                    dataGridView1.Columns[iIndex].HeaderText = (dataGridView1.DataSource as DataView).Table.Columns[iIndex].Caption;

                ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();
                dtProcAnalysis = procAnalysisBF.CreateDatatable().Dt;
                dataGridView2.DataSource = dtProcAnalysis.DefaultView;
                for (int iIndex = 0; iIndex < dataGridView2.Columns.Count; iIndex++)
                    dataGridView2.Columns[iIndex].HeaderText = (dataGridView2.DataSource as DataView).Table.Columns[iIndex].Caption;

            }
            catch (Exception ex)
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = ex.Message;
            }


            //返回结果
            return returnValueSF;

        }
 
        #endregion
    }
}