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
        #region 声明变量
        //同步内存表和数据库表
        private System.Threading.Timer timer;   //同步内存表和数据库表 用到的 线程定时器
        private bool blnBusy = false;           //表示 同步内存表和数据库表 的 线程定时器 是否繁忙，忙则退出

        //状态栏 显示
        private string strSynchronizeMessage = "数据同步：";
        #endregion 声明变量


        public fmMDIMain()
        {
            InitializeComponent();
        }

        private void fmMDIMain_Load(object sender, EventArgs e)
        {
            TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
            taskBookServiceBF.SynchronizeDatas(); //同步内存表和数据库表
            taskBookServiceBF.RegisterChannel();   //注册Tcp通道

            //调用线程定时器，定时 同步内存表和数据库表
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

        #region 同步内存表和数据库表
        /// <summary>
        /// 同步内存表和数据库表
        /// </summary>
        /// <param name="state"></param>
        public void SynchronizeDatas(object state)
        {
            //避开其它程序进行数据导入操作的时间段
            if ((DateTime.Now >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 04:30")) &&
                (DateTime.Now <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 05:00")))
                return;
            //
            if (blnBusy)
                return;
            //
            //this.Text = "任务书分析服务：数据表更新（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";
            SysMsgBM.CaptureOffmMDIMain_Taskbook = "任务书分析服务【VER 201504281655】：数据表更新（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

            blnBusy = true;

            ReturnValueSF returnValueSF = null;
            TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
            returnValueSF = taskBookServiceBF.SynchronizeDatas();  //同步内存表和数据库表
            strSynchronizeMessage = returnValueSF.Message;

            blnBusy = false;

            //this.Text += "（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";
            SysMsgBM.CaptureOffmMDIMain_Taskbook += "（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //显示标题
            this.Text = SysMsgBM.CaptureOffmMDIMain_Taskbook;

            //更新 状态栏 信息
            statusStrip1.Items[1].Text = strSynchronizeMessage;


        }
    }
}