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
        #region ��������
        //ͬ���ڴ������ݿ��
        private System.Threading.Timer timer;   //ͬ���ڴ������ݿ�� �õ��� �̶߳�ʱ��
        private bool blnBusy = false;           //��ʾ ͬ���ڴ������ݿ�� �� �̶߳�ʱ�� �Ƿ�æ��æ���˳�

        //DataGridView ��ʾ 
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

        //״̬�� ��ʾ
        private string strSynchronizeMessage = "����ͬ����";

        #endregion

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
            //this.Text = "��վ���Ϸ���������ݱ���£�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";
            SysMsgBM.CaptureOffmMDIMain = "��վ���Ϸ������[VER 150427]�����ݱ���£�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";

            blnBusy = true;

            ReturnValueSF returnValueSF = null;
            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
            returnValueSF = agentServiceBF.SynchronizeDatas();  //ͬ���ڴ������ݿ��
            strSynchronizeMessage = returnValueSF.Message;

            blnBusy = false;

            //this.Text += "��" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";
            SysMsgBM.CaptureOffmMDIMain += "��" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";

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

                #region ��ȡԶ�̶���
                //��ȡ���ݵĴ���������
                if (SysMsgBM.AgentLevel == "2")
                {
                    if ((SysMsgBM.GetAgentIP != "") && (SysMsgBM.GetAgentPort != ""))
                        returnValueSF = agentServiceBF.SetRemotingObject(SysMsgBM.GetAgentIP, SysMsgBM.GetAgentPort);
                    else
                        throw new Exception("GetAgentIP �� GetAgentPort ���ڿհ����ݣ���ȷ�ϣ�" + System.Environment.NewLine +
                            "AgentLevel��" + SysMsgBM.AgentLevel + System.Environment.NewLine +
                            "GetAgentIP��" + SysMsgBM.GetAgentIP + System.Environment.NewLine +
                            "GetAgentPort��" + SysMsgBM.GetAgentPort);

                    if (returnValueSF.Result < 0)
                        throw new Exception("��ȡ���ݵĴ����������ȡʧ�ܣ������µ�¼��" + System.Environment.NewLine +
                            "AgentLevel��" + SysMsgBM.AgentLevel + System.Environment.NewLine +
                            "GetAgentIP��" + SysMsgBM.GetAgentIP + System.Environment.NewLine +
                            "GetAgentPort��" + SysMsgBM.GetAgentPort + System.Environment.NewLine +
                            returnValueSF.Message);
                }
                #endregion ��ȡԶ�̶���

                //
                agentServiceBF.SynchronizeDatas();  //ͬ���ڴ������ݿ��
                agentServiceBF.InitializeDAL(";dtProcRecords;dtProcAnalysis;dtOnLineUsers;");     //��ʼ�� AgentServiceDAF ��(��¼��)
                InitShow_2(";dtProcRecords;dtProcAnalysis;dtOnLineUsers;");                       //��ʼ�� �౾�� ��¼�����
                returnValueSF = agentServiceBF.RegisterChannel();   //ע��Tcpͨ��
                if (returnValueSF.Result < 0)
                    throw new Exception(returnValueSF.Message);

                //�����̶߳�ʱ������ʱ ͬ���ڴ������ݿ��
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
                MessageBox.Show(ex.Message, "��ʾ");
                System.Environment.Exit(0);
            }

        }

        #region DataGridView ��ʾ

        /// <summary>
        /// ��ʱ���� DataGridView ������Դ
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


            //���ؽ��
            return returnValueSF;

        }

        /// <summary>
        /// ��ʱ���� DataGridView ������Դ
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
                SysMsgBM.TraceInfo_Position = "ProcRecords��1";   //trace

                //���� ���� ��¼��ϸ������׷�����¼�¼�İ취
                AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
                dtProcRecords_DAF = agentServiceBF.GetDatatable("dtProcRecords").Dt;
                iCountsOfProcRecords = dtProcRecords.Rows.Count;
                iCountsOfProcRecords_DAF = dtProcRecords_DAF.Rows.Count;

                ProcRecordsBF procRecordsBF = new ProcRecordsBF();
                for (int iIndex = iCountsOfProcRecords; iIndex < iCountsOfProcRecords_DAF; iIndex++)
                {
                    SysMsgBM.TraceInfo_Position = "ProcRecords��2";   //trace
                    if ((dtProcRecords_DAF.Rows[iIndex] == null) || (dtProcRecords_DAF.Rows[iIndex].RowState != DataRowState.Unchanged))
                        continue;
                    ProcRecordsBM procRecordsBM = new ProcRecordsBM(dtProcRecords_DAF.Rows[iIndex]);
                    SysMsgBM.TraceInfo_Position = "ProcRecords��3";   //trace
                    returnValueSF_Msg = procRecordsBF.AddRecord(dtProcRecords, procRecordsBM);
                    //
                    SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_1 = "[" + DateTime.Now.ToString() + "]" +
                        "[ProcRecords��Position:" + iCountsOfProcRecords.ToString() + "," +
                        iCountsOfProcRecords_DAF.ToString() + ";Result:" + returnValueSF_Msg.Result.ToString() +
                        ";Message:" + returnValueSF_Msg.Message + "]";  //trace
                }

                //���� ���� ��¼ͳ�Ʊ��������и��µİ취
                SysMsgBM.TraceInfo_Position = "ProcAnalysis��1";   //trace
                dtProcAnalysis_DAF = agentServiceBF.GetDatatable("dtProcAnalysis").Dt;
                iCountsOfProcAnalysis_DAF = dtProcAnalysis_DAF.Rows.Count;
                ProcAnalysisBF procAnalysisBF = new ProcAnalysisBF();
                for (int iIndex = 0; iIndex < iCountsOfProcAnalysis_DAF; iIndex++)
                {
                    ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(dtProcAnalysis_DAF.Rows[iIndex]);
                    returnValueSF_Msg = procAnalysisBF.UpdateRecord(dtProcAnalysis, procAnalysisBM);
                    //
                    SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_1 += "[" + DateTime.Now.ToString() + "]" +
                        "[ProcAnalysis��Position:" + iCountsOfProcAnalysis_DAF.ToString() +
                        ";Result:" + returnValueSF_Msg.Result.ToString() +
                        ";Message:" + returnValueSF_Msg.Message + "]";  //trace
                }

                //���� ���� �����û���Ϣ�������滻���ݱ�İ취
                SysMsgBM.TraceInfo_Position = "OnLineUsers��1";   //trace
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


            //���ؽ��
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


            //���ؽ��
            return returnValueSF;

        }

        /// <summary>
        /// �����ṩ�Ĳ�����ʼ�����ش������ ��¼�� ����
        /// </summary>
        /// <param name="strInitTable">��Ҫ��ʼ���ı�񣬸�ʽ�磺";dtProcRecords;dtProcAnalysis;dtOnLineUsers;"</param>
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


            //���ؽ��
            return returnValueSF;

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //��ʾ����
            this.Text = SysMsgBM.CaptureOffmMDIMain;

            //
            if (blnBusy_Show)
                return;

            blnBusy_Show = true;

            //��ʱͬ����¼�����ݵ���������Ӧ�ڲ�����
            SysMsgBM.TraceInfo_timer1_Tick_1 = "[��ʼ��" + DateTime.Now.ToString() + "]"; //trace

            ShowInDataGridView_2();

            SysMsgBM.TraceInfo_timer1_Tick_1 += "[ִ�����ShowInDataGridView_2��" + DateTime.Now.ToString() + "]"; //trace

            for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop].Name == "fmOnLineUsers")
                {
                    SysMsgBM.TraceInfo_timer1_Tick_1 += "[����fmOnLineUsers���£�" + DateTime.Now.ToString() + "]"; //trace
                    
                    (MdiChildren[iLoop] as fmOnLineUsers).DataGridView1.DataSource = dtOnLineUsers.DefaultView;
                }
            }
            //�����¼���¼�ﵽ10000���������ɣ����������������ͼ������Դ������
            AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
            if (agentServiceBF.GetDatatable("dtProcRecords").Dt.Rows.Count > 20000)
            {
                ReturnValueSF returnValueSF_Msg = new ReturnValueSF();  //trace

                SysMsgBM.TraceInfo_timer1_Tick_1 += "[����dtProcRecords���£�" + DateTime.Now.ToString() + "]"; //trace

                returnValueSF_Msg = agentServiceBF.InitializeDAL(";dtProcRecords;");     //��ʼ�� AgentServiceDAF ��(��¼��)
                
                SysMsgBM.TraceInfo_timer1_Tick_1 += "[agentServiceBF.InitializeDAL��Time:" +
                    DateTime.Now.ToString() + ";Result:" + returnValueSF_Msg.Result + "]"; //trace

                returnValueSF_Msg = InitShow_2(";dtProcRecords;");                       //��ʼ�� DataGridView ������

                SysMsgBM.TraceInfo_timer1_Tick_1 += "[InitShow_2��Time:" +
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
            //���� ״̬�� ��Ϣ
            statusStrip1.Items[1].Text = strSynchronizeMessage;

            SysMsgBM.TraceInfo_timer1_Tick_1 += "[������" + DateTime.Now.ToString() + "]"; //trace

            blnBusy_Show = false;
        }

        #region ���ݴ���������ʾ��Ӧ�Ĵ���
        /// <summary>
        /// ���ݴ���������ʾ��Ӧ�Ĵ���
        /// </summary>
        /// <param name="FormName">��������</param>
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
            if (MessageBox.Show("�Ƿ��˳��������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void �˳�ϵͳToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ������ϸToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �汾ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("fmVersion");
        }


    }
}