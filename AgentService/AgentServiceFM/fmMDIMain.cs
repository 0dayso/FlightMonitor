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
using LinYong.PublicService;
using System.Configuration;

namespace AirSoft.FlightMonitor.AgentServiceFM
{
    public partial class fmMDIMain : Form
    {
        #region 声明变量
        //同步内存表和数据库表
        private System.Threading.Timer timer;   //同步内存表和数据库表 用到的 线程定时器
        private bool blnBusy = false;           //表示 同步内存表和数据库表 的 线程定时器 是否繁忙，忙则退出

        //DataGridView 显示 
        bool blnBusy_Show = false;
        private DataTable dtProcRecords = null;
        private DataTable dtProcAnalysis = null;
        private DataTable dtOnLineUsers = null;

        public DataTable ProcRecords
        {
            get { return dtProcRecords; }
        }

        public DataTable ProcAnalysis
        {
            get { return dtProcAnalysis; }
        }

        public DataTable OnLineUsers
        {
            get { return dtOnLineUsers; }
        }

        //状态栏 显示
        private string strSynchronizeMessage = "数据同步：";

        #endregion

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
            //this.Text = "航站保障服务代理：数据表更新（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";
            SysMsgBM.CaptureOffmMDIMain = "航站保障服务代理[VER 150427]：数据表更新（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

            blnBusy = true;

            ReturnValueSF returnValueSF = null;
            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
            returnValueSF = agentServiceBF.SynchronizeDatas();  //同步内存表和数据库表
            strSynchronizeMessage = returnValueSF.Message;

            blnBusy = false;

            //this.Text += "（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";
            SysMsgBM.CaptureOffmMDIMain += "（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

        }
        #endregion

        public fmMDIMain()
        {
            InitializeComponent();
        }

        private void fmMDIMain_Load(object sender, EventArgs e)
        {
            try
            {
                AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
                ProcRecordsBF procRecordsBF = new ProcRecordsBF();
                ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();
                ReturnValueSF returnValueSF = null;


                //
                SysMsgBM.AgentIP = ConfigurationManager.AppSettings["AgentIP"].Trim();
                SysMsgBM.AgentPort = ConfigurationManager.AppSettings["AgentPort"].Trim();
                SysMsgBM.GetAgentIP = ConfigurationManager.AppSettings["GetAgentIP"].Trim();
                SysMsgBM.GetAgentPort = ConfigurationManager.AppSettings["GetAgentPort"].Trim();
                SysMsgBM.AgentLevel = ConfigurationManager.AppSettings["AgentLevel"].Trim();
                SysMsgBM.Compress = ConfigurationManager.AppSettings["Compress"].Trim();
                SysMsgBM.RefreshInterval = ConfigurationManager.AppSettings["RefreshInterval"].Trim();

                #region 获取远程对象
                //获取数据的代理服务对象
                if (SysMsgBM.AgentLevel == "2")
                {
                    if ((SysMsgBM.GetAgentIP != "") && (SysMsgBM.GetAgentPort != ""))
                        returnValueSF = agentServiceBF.SetRemotingObject(SysMsgBM.GetAgentIP, SysMsgBM.GetAgentPort);
                    else
                        throw new Exception("GetAgentIP 或 GetAgentPort 存在空白数据，请确认！" + System.Environment.NewLine +
                            "AgentLevel：" + SysMsgBM.AgentLevel + System.Environment.NewLine +
                            "GetAgentIP：" + SysMsgBM.GetAgentIP + System.Environment.NewLine +
                            "GetAgentPort：" + SysMsgBM.GetAgentPort);

                    if (returnValueSF.Result < 0)
                        throw new Exception("获取数据的代理服务对象获取失败，请重新登录！" + System.Environment.NewLine +
                            "AgentLevel：" + SysMsgBM.AgentLevel + System.Environment.NewLine +
                            "GetAgentIP：" + SysMsgBM.GetAgentIP + System.Environment.NewLine +
                            "GetAgentPort：" + SysMsgBM.GetAgentPort + System.Environment.NewLine +
                            returnValueSF.Message);
                }
                #endregion 获取远程对象

                //
                agentServiceBF.SynchronizeDatas();  //同步内存表和数据库表
                agentServiceBF.InitializeDAL(";dtProcRecords;dtProcAnalysis;dtOnLineUsers;");     //初始化 AgentServiceDAF 类(记录表)
                InitShow_2(";dtProcRecords;dtProcAnalysis;dtOnLineUsers;");                       //初始化 类本身 记录表对象
                returnValueSF = agentServiceBF.RegisterChannel();   //注册Tcp通道
                if (returnValueSF.Result < 0)
                    throw new Exception(returnValueSF.Message);

                //调用线程定时器，定时 同步内存表和数据库表
                int iRefreshInterval = 20 * 1000;
                if (SysMsgBM.AgentLevel == "2")
                {
                    try
                    {
                        iRefreshInterval = Convert.ToInt32(SysMsgBM.RefreshInterval) * 1000;
                    }
                    catch
                    {
                        iRefreshInterval = 20 * 1000;
                    }
                }
                TimerCallback timerDelegate = new TimerCallback(SynchronizeDatas);
                timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);
                //
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                System.Environment.Exit(0);
            }

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

                //dataGridView1.DataSource = dtProcRecords.DefaultView;
                //for (int iIndex = 0; iIndex < dataGridView1.Columns.Count; iIndex++)
                //    dataGridView1.Columns[iIndex].HeaderText = (dataGridView1.DataSource as DataView).Table.Columns[iIndex].Caption;
                //dataGridView2.DataSource = dtProcAnalysis.DefaultView;
                //for (int iIndex = 0; iIndex < dataGridView2.Columns.Count; iIndex++)
                //    dataGridView2.Columns[iIndex].HeaderText = (dataGridView2.DataSource as DataView).Table.Columns[iIndex].Caption;
            }
            catch (Exception ex)
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
            ReturnValueSF returnValueSF_Msg = null; //trace

            int iCountsOfProcRecords = 0, iCountsOfProcRecords_DAF = 0;
            DataTable dtProcRecords_DAF = null, dtProcAnalysis_DAF = null, dtOnLineUsers_DAF = null;
            int iCountsOfProcAnalysis_DAF = 0;


            //
            try
            {
                //
                SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_1 = "";    //trace
                SysMsgBM.TraceInfo_Position = "ProcRecords：1";   //trace

                //更新 本地 记录明细表，采用追加最新记录的办法
                AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
                dtProcRecords_DAF = agentServiceBF.GetDatatable("dtProcRecords").Dt;
                iCountsOfProcRecords = dtProcRecords.Rows.Count;
                iCountsOfProcRecords_DAF = dtProcRecords_DAF.Rows.Count;

                ProcRecordsBF procRecordsBF = new ProcRecordsBF();
                for (int iIndex = iCountsOfProcRecords; iIndex < iCountsOfProcRecords_DAF; iIndex++)
                {
                    SysMsgBM.TraceInfo_Position = "ProcRecords：2";   //trace
                    if ((dtProcRecords_DAF.Rows[iIndex] == null) || (dtProcRecords_DAF.Rows[iIndex].RowState != DataRowState.Unchanged))
                        continue;
                    ProcRecordsBM procRecordsBM = new ProcRecordsBM(dtProcRecords_DAF.Rows[iIndex]);
                    SysMsgBM.TraceInfo_Position = "ProcRecords：3";   //trace
                    returnValueSF_Msg = procRecordsBF.AddRecord(dtProcRecords, procRecordsBM);
                    //
                    SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_1 = "[" + DateTime.Now.ToString() + "]" +
                        "[ProcRecords：Position:" + iCountsOfProcRecords.ToString() + "," +
                        iCountsOfProcRecords_DAF.ToString() + ";Result:" + returnValueSF_Msg.Result.ToString() +
                        ";Message:" + returnValueSF_Msg.Message + "]";  //trace
                }

                //更新 本地 记录统计表，采用逐行更新的办法
                SysMsgBM.TraceInfo_Position = "ProcAnalysis：1";   //trace
                dtProcAnalysis_DAF = agentServiceBF.GetDatatable("dtProcAnalysis").Dt;
                iCountsOfProcAnalysis_DAF = dtProcAnalysis_DAF.Rows.Count;
                ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();
                for (int iIndex = 0; iIndex < iCountsOfProcAnalysis_DAF; iIndex++)
                {
                    ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(dtProcAnalysis_DAF.Rows[iIndex]);
                    returnValueSF_Msg = procAnalysisBF.UpdateRecord(dtProcAnalysis, procAnalysisBM);
                    //
                    SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_1 += "[" + DateTime.Now.ToString() + "]" +
                        "[ProcAnalysis：Position:" + iCountsOfProcAnalysis_DAF.ToString() +
                        ";Result:" + returnValueSF_Msg.Result.ToString() +
                        ";Message:" + returnValueSF_Msg.Message + "]";  //trace
                }

                //更新 本地 在线用户信息表，采用替换数据表的办法
                SysMsgBM.TraceInfo_Position = "OnLineUsers：1";   //trace
                dtOnLineUsers_DAF = agentServiceBF.GetDatatable("dtOnLineUsers").Dt;
                dtOnLineUsers = dtOnLineUsers_DAF.Copy();
                //
                SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_1 += "[" + DateTime.Now.ToString() + "]" +
                    "[OnLineUsers]";    //trace

            }
            catch (Exception ex)
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = ex.Message;
                //
                SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_2 = "[" + DateTime.Now.ToString() + "]" +
                    "[" + SysMsgBM.TraceInfo_Position + "]" + ex.Message;    //trace
            }


            //返回结果
            return returnValueSF;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ReturnValueSF InitShow_2()
        {
            //
            ReturnValueSF returnValueSF = new ReturnValueSF();


            //
            try
            {
                ProcRecordsBF procRecordsBF = new ProcRecordsBF();
                dtProcRecords = procRecordsBF.CreateDatatable().Dt;
                //dataGridView1.DataSource = dtProcRecords.DefaultView;
                //for (int iIndex = 0; iIndex < dataGridView1.Columns.Count; iIndex++)
                //    dataGridView1.Columns[iIndex].HeaderText = (dataGridView1.DataSource as DataView).Table.Columns[iIndex].Caption;

                ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();
                dtProcAnalysis = procAnalysisBF.CreateDatatable().Dt;
                //dataGridView2.DataSource = dtProcAnalysis.DefaultView;
                //for (int iIndex = 0; iIndex < dataGridView2.Columns.Count; iIndex++)
                //    dataGridView2.Columns[iIndex].HeaderText = (dataGridView2.DataSource as DataView).Table.Columns[iIndex].Caption;

                OnLineUsersBF onLineUsersBF = new OnLineUsersBF();
                dtOnLineUsers = onLineUsersBF.CreateDatatable().Dt;
            }
            catch (Exception ex)
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = ex.Message;
            }


            //返回结果
            return returnValueSF;

        }

        /// <summary>
        /// 根据提供的参数初始化本地窗体类的 记录表 对象
        /// </summary>
        /// <param name="strInitTable">需要初始化的表格，格式如：";dtProcRecords;dtProcAnalysis;dtOnLineUsers;"</param>
        /// <returns></returns>
        private ReturnValueSF InitShow_2(string strInitTables)
        {
            //
            ReturnValueSF returnValueSF = new ReturnValueSF();


            //
            try
            {
                if (strInitTables.IndexOf(";dtProcRecords;") >= 0)
                {
                    ProcRecordsBF procRecordsBF = new ProcRecordsBF();
                    dtProcRecords = procRecordsBF.CreateDatatable().Dt;
                }
                if (strInitTables.IndexOf(";dtProcAnalysis;") >= 0)
                {
                    ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();
                    dtProcAnalysis = procAnalysisBF.CreateDatatable().Dt;
                }
                if (strInitTables.IndexOf(";dtOnLineUsers;") >= 0)
                {
                    OnLineUsersBF onLineUsersBF = new OnLineUsersBF();
                    dtOnLineUsers = onLineUsersBF.CreateDatatable().Dt;
                }

                returnValueSF.Result = 1;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            //显示标题
            this.Text = SysMsgBM.CaptureOffmMDIMain;

            //
            if (blnBusy_Show)
                return;

            blnBusy_Show = true;

            //定时同步记录表数据到本窗体相应内部对象
            SysMsgBM.TraceInfo_timer1_Tick_1 = "[开始：" + DateTime.Now.ToString() + "]"; //trace

            ShowInDataGridView_2();

            SysMsgBM.TraceInfo_timer1_Tick_1 += "[执行完毕ShowInDataGridView_2：" + DateTime.Now.ToString() + "]"; //trace

            for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop].Name == "fmOnLineUsers")
                {
                    SysMsgBM.TraceInfo_timer1_Tick_1 += "[进入fmOnLineUsers更新：" + DateTime.Now.ToString() + "]"; //trace
                    
                    (MdiChildren[iLoop] as fmOnLineUsers).DataGridView1.DataSource = dtOnLineUsers.DefaultView;
                }
            }
            //如果记录表记录达到10000，重新生成，并更新相关数据试图的数据源到最新
            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
            if (agentServiceBF.GetDatatable("dtProcRecords").Dt.Rows.Count > 20000)
            {
                ReturnValueSF returnValueSF_Msg = new ReturnValueSF();  //trace

                SysMsgBM.TraceInfo_timer1_Tick_1 += "[进入dtProcRecords更新：" + DateTime.Now.ToString() + "]"; //trace

                returnValueSF_Msg = agentServiceBF.InitializeDAL(";dtProcRecords;");     //初始化 AgentServiceDAF 类(记录表)
                
                SysMsgBM.TraceInfo_timer1_Tick_1 += "[agentServiceBF.InitializeDAL：Time:" +
                    DateTime.Now.ToString() + ";Result:" + returnValueSF_Msg.Result + "]"; //trace

                returnValueSF_Msg = InitShow_2(";dtProcRecords;");                       //初始化 DataGridView 标题栏

                SysMsgBM.TraceInfo_timer1_Tick_1 += "[InitShow_2：Time:" +
                   DateTime.Now.ToString() + ";Result:" + returnValueSF_Msg.Result + "]"; //trace
                
                for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop].Name == "fmProcRecords")
                    {
                        (MdiChildren[iLoop] as fmProcRecords).DataGridView1.DataSource = dtProcRecords.DefaultView;
                    }
                    else if (MdiChildren[iLoop].Name == "fmProcAnalysis")
                    {
                        (MdiChildren[iLoop] as fmProcAnalysis).DataGridView1.DataSource = dtProcAnalysis.DefaultView;
                    }
                    else if (MdiChildren[iLoop].Name == "fmOnLineUsers")
                    {
                        (MdiChildren[iLoop] as fmOnLineUsers).DataGridView1.DataSource = dtOnLineUsers.DefaultView;
                    }
                }

            }
            //更新 状态栏 信息
            statusStrip1.Items[1].Text = strSynchronizeMessage;

            SysMsgBM.TraceInfo_timer1_Tick_1 += "[结束：" + DateTime.Now.ToString() + "]"; //trace

            blnBusy_Show = false;
        }

        #region 根据窗体名字显示相应的窗体
        /// <summary>
        /// 根据窗体名字显示相应的窗体
        /// </summary>
        /// <param name="FormName">窗体名字</param>
        private void ShowForm(string FormName)
        {
            bool blnFind = false;
            for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop].Name != FormName)
                {
                    MdiChildren[iLoop].WindowState = FormWindowState.Minimized;
                }
                else
                {
                    blnFind = true;
                    MdiChildren[iLoop].WindowState = FormWindowState.Maximized;
                }
            }

            if (blnFind == false)
            {
                if (FormName == "fmProcRecords")
                {
                    fmProcRecords objForm = new fmProcRecords(this);
                    objForm.Name = "fmProcRecords";
                    objForm.Show();
                    objForm.MdiParent = this;
                    objForm.WindowState = FormWindowState.Maximized;
                }
                else if (FormName == "fmProcAnalysis")
                {
                    fmProcAnalysis objForm = new fmProcAnalysis(this);
                    objForm.Name = "fmProcAnalysis";
                    objForm.Show();
                    objForm.MdiParent = this;
                    objForm.WindowState = FormWindowState.Maximized;
                }
                else if (FormName == "fmOnLineUsers")
                {
                    fmOnLineUsers objForm = new fmOnLineUsers(this);
                    objForm.Name = "fmOnLineUsers";
                    objForm.Show();
                    objForm.MdiParent = this;
                    objForm.WindowState = FormWindowState.Maximized;
                }
                else if (FormName == "fmMesssage")
                {
                    fmMessage objForm = new fmMessage();
                    objForm.Name = "fmMesssage";
                    objForm.Show();
                    objForm.MdiParent = this;
                    objForm.WindowState = FormWindowState.Maximized;
                }
                else if (FormName == "fmVersion")
                {
                    fmVersion objForm = new fmVersion();
                    objForm.Name = "fmVersion";
                    objForm.Show();
                    objForm.MdiParent = this;
                    objForm.WindowState = FormWindowState.Maximized;
                }
    
            }
        }
        #endregion

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowForm("fmProcRecords");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowForm("fmOnLineUsers");
        }

        private void fmMDIMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出代理服务？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ShowForm("fmProcAnalysis");
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ShowForm("fmMesssage");
        }

        private void 数据明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("fmVersion");
        }


    }
}