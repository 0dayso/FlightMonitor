using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.AgentServiceBF;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.Public.SystemFramework;
using System.Configuration;
using System.Collections;

namespace MessageService.MessageServiceFM
{
    public partial class fmMessageService : Form
    {
        #region ��������
        //Զ�̴��������� AgentServiceDAF.objRemotingObject
        private bool blnSetRemotingObject = false; //��ʾ�Ƿ��Ѿ�������Զ�̴��������� AgentServiceDAF.objRemotingObject

        //���������Ϣ
        private System.Threading.Timer timer;   //���������Ϣ �õ��� �̶߳�ʱ��
        private bool blnBusy = false;           //��ʾ ���������Ϣ �� �̶߳�ʱ�� �Ƿ�æ��æ���˳�
        private bool blnBusy_timer1 = false;           //��ʾ ͬ�����������Ϣ���ݽ�� �� ��ʱ�� �Ƿ�æ��æ���˳�

        private string strDTTM = "";            //���һ����¼����Ϣ����ʱ��
        private int intEventID = -1;            //���һ����¼��������ֵ

        static private DataTable dataTableLog = null;   //��¼���߳���ʹ��

        static private DataTable dataTableLog_Main = null;   //��¼��������ʹ��

        static private object objDataTableLog = new object();   //�� dataTableLog ��¼�� �Ĳ���ͬ������

        //���к��ŵ������ʱ��
        private System.Threading.Timer timer_CC;   //���к��ŵ������ʱ�� �õ��� �̶߳�ʱ��
        private bool blnBusy_CC = false;           //��ʾ ���к��ŵ������ʱ�� �� �̶߳�ʱ�� �Ƿ�æ��æ���˳�
        private bool blnBusy_timer1_CC = false;           //��ʾ ͬ�����к��ŵ������ʱ�����ݴ����� �� ��ʱ�� �Ƿ�æ��æ���˳�

        static private DataTable dataTableLog_CC = null;   //��¼���߳���ʹ��

        static private DataTable dataTableLog_Main_CC = null;   //��¼��������ʹ��

        static private object objDataTableLog_CC = new object();   //�� dataTableLog ��¼�� �Ĳ���ͬ������


        private string strDEPSTNs = ""; //��ʾ��Ҫ����ĺ������ɻ���Ҫ�ڴ��б���

        //������澯��Ϣ
        private System.Threading.Timer[] _arrayTimer = new System.Threading.Timer[50]; //�洢�̶߳���λ��0 �������ʹ�ã�λ��1���� �������������ۺ���澯����ʹ��
        private bool[] _arrayBusy = new bool[50]; //��Ӧ������̶߳������飬��ʾ���̵߳ķ�æ״̬
        private DateTime[] _arrayDateTime = new DateTime[1]; //�洢�������ļ�¼�Ĳ���ʱ�䣬�������ʹ��
        private bool blnBusy_timer1_FlightAlarm = false;           //��ʾ ͬ������澯��Ϣ �� ��ʱ�� �Ƿ�æ��æ���˳�


        static private DataTable _dtOverStationTime = null; //��վ��վʱ�䣨�����������ݣ��ṩ��ѯ��ʹ�ã�
        static private DataTable _dtStationInfor = null; //��վ��Ϣ�������������ݣ��绬��ʱ�䣩���ṩ��ѯ��ʹ�ã�
        static private DataTable _dtTodayInOutFlights = null;   //�����ۺ������������״̬
        static DataTable[] _arrayTodayInOutFlights = new DataTable[50]; //�����ۺ���� ���飬��������״̬��ÿ�����ݱ��Ӧ����һ���߳�ʹ�� -- ����ԭ��ƾ������ƺ�����Ҫ�󣬹ʴ˱����ݲ�ʹ��

        static private object _objTodayInOutFlights = new object();   //�� _dtTodayInOutFlights ���ݱ� �Ĳ���ͬ������
        static private object[] _arrayObjTodayInOutFlights = new object[50];   //�� _arrayTodayInOutFlights �����е� ���ݱ� ��Ӧ�Ĳ���ͬ������ -- ����ԭ��ƾ������ƺ�����Ҫ�󣬹ʴ˱����ݲ�ʹ��


      
        #endregion ��������


        public fmMessageService()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //��ʾ Log ҳ��
            tabControl1.SelectedTab = tabControl1.TabPages[0]; ;

            //����������
            if (!blnSetRemotingObject) //��δ������Զ�̴��������� AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("�����������ȡʧ�ܣ������µ�¼��", "��ʾ", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //������Զ�̴��������� AgentServiceDAF.objRemotingObject
            }

            //��ȡ���һ����¼��ָ���û���
            #region ��ʹ��
            //strDTTM = ConfigurationSettings.AppSettings["DTTM"];

            //if (strDTTM.Trim() == "")
            //{
            //    ReturnValueSF rvSFChangeRecordBF = new ReturnValueSF();
            //    ChangeRecordBF changeRecordBF = new ChangeRecordBF();
            //    rvSFChangeRecordBF = changeRecordBF.GetLastRecord("SYS_PEK");
            //    if ((rvSFChangeRecordBF.Result > 0) &&
            //        (rvSFChangeRecordBF.Dt != null) &&
            //        (rvSFChangeRecordBF.Dt.Rows.Count > 0))
            //    {
            //        strDTTM = rvSFChangeRecordBF.Dt.Rows[0]["cncFOCOperatingTime"].ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show("�ӱ�����ȡ���һ����¼���û���SYS_PEK��ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK);
            //        return;
            //        //Environment.Exit(0);
            //    }
            //}
            #endregion ��ʹ��
            string strEventID = ConfigurationSettings.AppSettings["EventID"].ToString().Trim();
            try
            {
                intEventID = Convert.ToInt32(strEventID);
            }
            catch
            {
                intEventID = -1;
            }

            //�����̶߳�ʱ������ʱ ���������Ϣ
            int iRefreshInterval = 10 * 1000;
            TimerCallback timerDelegate = new TimerCallback(GetMessages);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);


            //
            timer1.Enabled = true;

            button1.Enabled = false;
        }

        public void GetMessages(object state)
        {
            //�����æ�����˳�������
            if (blnBusy)
                return;

            //���÷�æ���
            blnBusy = true;

            //�������
            SysMsgBM.CaptureOffmMessageService = "������Ϣ����VER 201506261636�������ݱ���£�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";

            #region ��ȡ������Ϣ����ƥ�亽վ����ϵͳ���࣬���ά������վ����ϵͳ
            ReturnValueSF returnValueSF = null;
            MessageServiceBF messageServiceBF = new MessageServiceBF();
            //returnValueSF = messageServiceBF.GetMessages(strDTTM); //��ȡ������Ϣ�������ϴλ�ȡ�������һ����¼����Ϣ����ʱ����Ϊ��㣩
            returnValueSF = messageServiceBF.GetMessages(intEventID); //��ȡ������Ϣ�������ϴλ�ȡ�������һ����¼��������ֵ��Ϊ��㣩
            if ((returnValueSF.Result > 0) && (returnValueSF.Dt != null) && (returnValueSF.Dt.Rows.Count > 0)) //����Ϣ
            {
                DataTable dataTableMessage = returnValueSF.Dt;
                for (int indexDataTableMessage = 0; indexDataTableMessage < dataTableMessage.Rows.Count; indexDataTableMessage++) //������ȡ������Ϣ
                {
                    try
                    {
                        #region ��Ϣ�Ĺؼ�������
                        string DTTM = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString().Trim(); //���һ����¼����Ϣ����ʱ��
                        strDTTM = DTTM; //������ ȫ�־�̬���������һ����¼����Ϣ����ʱ��

                        int EventID = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString().Trim()); //���һ����¼��������ֵ
                        intEventID = EventID; //������ ȫ�־�̬���������һ����¼��������ֵ

                        string FlightNo = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString().Trim();
                        string ST = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString().Trim();
                        string STN = "PEK";
                        string IO = "";
                        if (dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString().Trim() == "D")
                            IO = "OUT";
                        else
                            IO = "IN";
                        string FieldENName = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString().Trim();
                        string ChangeReasonCode = "";
                        string ChangeNewContent = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString().Trim();
                        int FieldType = 1;

                        bool blnNeed = false; //����� false���򲻴���˼�¼
                        #endregion ��Ϣ�Ĺؼ�������

                        #region �����ļ��м�¼EventID
                        try
                        {
                            //�����ļ��м�¼EventID
                            //��������� EventID �� 1�� ��д�������ļ��е� EventID �ڵ㣨��֤����©���������¼���´���������ʱ�ٴδ��������¼��
                            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            configuration.AppSettings.Settings["EventID"].Value = ((Int32)(intEventID - 1)).ToString();
                            configuration.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings");//���¼����µ������ļ�
                        }
                        catch (Exception ex)
                        {

                        }
                        #endregion �����ļ��м�¼EventID

                        #region ���Ĵ�����ֶεĶ�Ӧ
                        switch (FieldENName)
                        {
                            //case "HUDC2GIS-STND": //ͣ��λ
                            //    if (IO == "OUT") //����ͣ��λ
                            //    {
                            //        ChangeReasonCode = "cnvcOutGate";
                            //        FieldType = 1;
                            //        blnNeed = true;
                            //    }
                            //    else //����ͣ��λ
                            //    {
                            //        ChangeReasonCode = "cnvcInGATE";
                            //        FieldType = 1;
                            //        blnNeed = true;
                            //    }
                            //    break;
                            case "HUDC2GIS-STND": //ͣ��λ -- ����ʹ��
                                if (IO == "OUT") //����ͣ��λ -- ����ʹ��
                                {
                                    ChangeReasonCode = "cnvcOutGate_Test"; 
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else //����ͣ��λ -- ����ʹ��
                                {
                                    ChangeReasonCode = "cnvcInGATE_Test";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDC2GIS-RWAY":    //�ܵ���
                                if (IO == "OUT")    //�����ܵ���
                                {
                                    ChangeReasonCode = "cnvcOutRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else    //�����ܵ���
                                {
                                    ChangeReasonCode = "cnvcInRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDCSRVT-RWAY":    //�ܵ���
                                if (IO == "OUT")    //�����ܵ���
                                {
                                    ChangeReasonCode = "cnvcOutRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else    //�����ܵ���
                                {
                                    ChangeReasonCode = "cnvcInRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDCMCDM-EIBT":    //Ԥ����λʱ��
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                else //����
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncInEXIT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDCMCDM-TOBT": //Ԥ���Ƴ�ʱ��
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutTOBT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-BOTM": //�ǻ�|��ʼ
                                if (IO == "OUT") //����
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardStartTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-BOTM": //�ǻ�|��ʼ
                                if (IO == "OUT") //����
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardStartTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-BCTM": //�ǻ�|���� 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-BCTM": //�ǻ�|���� 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-BETM": //����ʱ�� 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBridgeGuaranteeEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-BETM": //����ʱ�� 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBridgeGuaranteeEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-CITM": //��̨����ʱ�� 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutCheckCounterEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-DPRK": //����ƺ
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutDeicePing";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-STND": //����λ
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutDeiceWei";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            //case "": //��������ȴ���
                            //    if (IO == "OUT")
                            //    {
                            //        ChangeReasonCode = "";
                            //        FieldType = 1;
                            //        blnNeed = false;
                            //    }
                            //    else
                            //    {
                            //        ChangeReasonCode = "";
                            //        FieldType = 1;
                            //        blnNeed = false;
                            //    }
                            //    break;
                            case "HUDCDEIC-STDI": //������ʼ
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutDeiceStartTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-EDDI": //��������
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutDeiceEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-DOUT": //�뿪����λ
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutLeaveDeicePing";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-SLDI": //����������ʶ
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutSlowDeiceFlag";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCMCDM-TSAT ": //TSAT
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutTSAT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCMCDM-CTOT": //CTOT
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutCTOT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-CHDT": //HUDC2GIS-CHDT ת�̺�
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutTurnTableNO"; 
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "cnvcInTurnTableNO";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                        }
                        #endregion ���Ĵ�����ֶεĶ�Ӧ

                        #region ���������Ϣ
                        if (!blnNeed)
                        {
                            #region ��¼�� log��
                            lock (objDataTableLog)
                            {
                                DataRow dataRowLog = dataTableLog.NewRow();
                                dataRowLog["EventID"] = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString());
                                dataRowLog["cnvcFFID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFFID"].ToString();
                                dataRowLog["cnvcCOMPANY"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCOMPANY"].ToString();
                                dataRowLog["cnvcFLTID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString();
                                dataRowLog["cnvcST"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString();
                                dataRowLog["cncIOTAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString();
                                dataRowLog["cncHOMETAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncHOMETAG"].ToString();
                                dataRowLog["cnvcSNDR"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSNDR"].ToString();
                                dataRowLog["cnvcDTTM"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString();
                                dataRowLog["cnvcTYPE"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcTYPE"].ToString();
                                dataRowLog["cnvcSTYP"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSTYP"].ToString();
                                dataRowLog["cncVALIDFLAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncVALIDFLAG"].ToString();
                                dataRowLog["cnvcCreateTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCreateTime"].ToString();
                                dataRowLog["cnvcCancelTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCancelTime"].ToString();
                                dataRowLog["cnvcMsgValue"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString();
                                dataRowLog["cnvcFieldCNName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldCNName"].ToString();
                                dataRowLog["cnvcFieldENName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString();
                                dataRowLog["cnvcResult"] = "������";
                                dataRowLog["cnvcMemo"] = "";
                                dataTableLog.Rows.Add(dataRowLog);
                            }
                            #endregion ��¼�� log��

                            //������һ����Ϣ
                            continue;
                        }
                        #endregion ���������Ϣ

                        #region ���ݹؼ������ݻ�ȡ����Ϣ��Ӧ�ĺ��ࣨ��վ����ϵͳ�еĺ��ࣩ����ά������վ����ϵͳ
                        GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
                        ReturnValueSF returnValueSF_GetFlightsByMessage = guaranteeInforBF.GetFlightsByMessage(FlightNo, ST, STN, IO); //���ݹؼ������ݻ�ȡ����Ϣ��Ӧ�ĺ��ࣨ��վ����ϵͳ�еĺ��ࣩ
                        if ((returnValueSF_GetFlightsByMessage.Result > 0) &&
                            (returnValueSF_GetFlightsByMessage.Dt != null) &&
                            (returnValueSF_GetFlightsByMessage.Dt.Rows.Count == 1))
                        {//��ȡ��һ�����࣬����Ӧ��Ӧ��Ϣά������վ����ϵͳ
                            OMPWebReference.FlightMonitorData wrFlightMonitorData = new MessageService.OMPWebReference.FlightMonitorData();
                            bool blnwrFlightMonitorData_MaintainGuaranteeInfor = wrFlightMonitorData.MaintainGuaranteeInfor(
                                "SYS_PEK", // UserID,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cnvcFLTID"].ToString(),   // FLTID,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncDATOP"].ToString(),    // DATOP,
                                Convert.ToInt32(returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cniLEGNO"].ToString()),    // LegNo,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cnvcAC"].ToString(),    // AC,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncDEPSTN"].ToString(),    // DepSTN,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncARRSTN"].ToString(),    // ArrSTN,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncSTD"].ToString(),    // STD,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncETD"].ToString(),    // ETD,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncSTA"].ToString(),    // STA,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncETA"].ToString(),    // ETA,
                                ChangeReasonCode, // ChangeReasonCode,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0][ChangeReasonCode].ToString(), // ChangeOldContent,
                                ChangeNewContent, // ChangeNewContent,
                                FieldType, //  FieldType,
                                DTTM // LocalOperatingTime
                                );
                            if (!blnwrFlightMonitorData_MaintainGuaranteeInfor)
                                throw new Exception("������Ϣά������վ����ϵͳʧ�ܣ�" + Environment.NewLine +
                                    "FlightNo��" + FlightNo + Environment.NewLine +
                                    "ST��" + ST + Environment.NewLine +
                                    "STN��" + STN + Environment.NewLine +
                                    "IO��" + IO);

                            #region ά������վ����ϵͳ �ɹ�����¼�� log��
                            lock (objDataTableLog)
                            {
                                DataRow dataRowLog = dataTableLog.NewRow();
                                dataRowLog["EventID"] = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString());
                                dataRowLog["cnvcFFID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFFID"].ToString();
                                dataRowLog["cnvcCOMPANY"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCOMPANY"].ToString();
                                dataRowLog["cnvcFLTID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString();
                                dataRowLog["cnvcST"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString();
                                dataRowLog["cncIOTAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString();
                                dataRowLog["cncHOMETAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncHOMETAG"].ToString();
                                dataRowLog["cnvcSNDR"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSNDR"].ToString();
                                dataRowLog["cnvcDTTM"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString();
                                dataRowLog["cnvcTYPE"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcTYPE"].ToString();
                                dataRowLog["cnvcSTYP"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSTYP"].ToString();
                                dataRowLog["cncVALIDFLAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncVALIDFLAG"].ToString();
                                dataRowLog["cnvcCreateTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCreateTime"].ToString();
                                dataRowLog["cnvcCancelTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCancelTime"].ToString();
                                dataRowLog["cnvcMsgValue"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString();
                                dataRowLog["cnvcFieldCNName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldCNName"].ToString();
                                dataRowLog["cnvcFieldENName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString();
                                dataRowLog["cnvcResult"] = "�ɹ�";
                                dataRowLog["cnvcMemo"] = "";
                                dataTableLog.Rows.Add(dataRowLog);
                            }
                            #endregion ά������վ����ϵͳ �ɹ�����¼�� log��
                        }
                        else if ((returnValueSF_GetFlightsByMessage.Result < 0) || (returnValueSF_GetFlightsByMessage.Dt == null))
                        {
                            throw new Exception("���ݹؼ������ݻ�ȡ��Ϣ��Ӧ�ĺ��ࣨ��վ����ϵͳ�еĺ��ࣩʧ�ܣ�" + Environment.NewLine +
                                    "FlightNo��" + FlightNo + Environment.NewLine +
                                    "ST��" + ST + Environment.NewLine +
                                    "STN��" + STN + Environment.NewLine +
                                    "IO��" + IO);
                        }
                        else if ((returnValueSF_GetFlightsByMessage.Result > 0) &&
                            (returnValueSF_GetFlightsByMessage.Dt != null) &&
                            (returnValueSF_GetFlightsByMessage.Dt.Rows.Count > 1))
                        {
                            throw new Exception("���ݹؼ������ݻ�ȡ��Ϣ��Ӧ�ĺ��ࣨ��վ����ϵͳ�еĺ��ࣩ�ж�����" + Environment.NewLine +
                                    "FlightNo��" + FlightNo + Environment.NewLine +
                                    "ST��" + ST + Environment.NewLine +
                                    "STN��" + STN + Environment.NewLine +
                                    "IO��" + IO);
                        }
                        else
                        {
                            throw new Exception("���ݹؼ������ݻ�ȡ��Ϣ��Ӧ�ĺ��ࣨ��վ����ϵͳ�еĺ��ࣩʧ�ܣ������������" + Environment.NewLine +
                            "FlightNo��" + FlightNo + Environment.NewLine +
                            "ST��" + ST + Environment.NewLine +
                            "STN��" + STN + Environment.NewLine +
                            "IO��" + IO);
                        }
                        #endregion  ���ݹؼ������ݻ�ȡ����Ϣ��Ӧ�ĺ��ࣨ��վ����ϵͳ�еĺ��ࣩ����ά������վ����ϵͳ
                    }
                    catch (Exception ex)
                    {
                        #region ��¼�� log��
                        lock (objDataTableLog)
                        {
                            DataRow dataRowLog = dataTableLog.NewRow();
                            dataRowLog["EventID"] = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString());
                            dataRowLog["cnvcFFID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFFID"].ToString();
                            dataRowLog["cnvcCOMPANY"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCOMPANY"].ToString();
                            dataRowLog["cnvcFLTID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString();
                            dataRowLog["cnvcST"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString();
                            dataRowLog["cncIOTAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString();
                            dataRowLog["cncHOMETAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncHOMETAG"].ToString();
                            dataRowLog["cnvcSNDR"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSNDR"].ToString();
                            dataRowLog["cnvcDTTM"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString();
                            dataRowLog["cnvcTYPE"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcTYPE"].ToString();
                            dataRowLog["cnvcSTYP"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSTYP"].ToString();
                            dataRowLog["cncVALIDFLAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncVALIDFLAG"].ToString();
                            dataRowLog["cnvcCreateTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCreateTime"].ToString();
                            dataRowLog["cnvcCancelTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCancelTime"].ToString();
                            dataRowLog["cnvcMsgValue"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString();
                            dataRowLog["cnvcFieldCNName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldCNName"].ToString();
                            dataRowLog["cnvcFieldENName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString();
                            dataRowLog["cnvcResult"] = "ʧ��";
                            dataRowLog["cnvcMemo"] = ex.Message;
                            dataTableLog.Rows.Add(dataRowLog);
                        }
                        #endregion ��¼�� log��
                    }

                }
            }
            #endregion ��ȡ������Ϣ����ƥ�亽վ����ϵͳ���࣬���ά������վ����ϵͳ

            //�������
            SysMsgBM.CaptureOffmMessageService += "��" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "��";

            //�����æ���
            blnBusy = false;

        }

        private void fmMessageService_Load(object sender, EventArgs e)
        {
            //��ʼ�� log ��

            #region ��ȡ�׶���������
            if (dataTableLog == null)
            {
                //���߳�ʹ�õ� log ��
                dataTableLog = new DataTable();
                dataTableLog.Columns.Add("EventID", typeof(Int32));     //��Ϣ���������ֶΣ�����     
                dataTableLog.Columns.Add("cnvcFFID" , typeof(string));	//����ID���������ڱ��(��ʽ���ֶ�˳�򣬺��չ�˾���� , ����� , ����۱�־ , ����ƻ�ʱ��(�򺽰�����) , ���ʹ��ڱ�־)
                dataTableLog.Columns.Add("cnvcCOMPANY" , typeof(string));	//���չ�˾����
                dataTableLog.Columns.Add("cnvcFLTID" , typeof(string));	//�����
                dataTableLog.Columns.Add("cnvcST" , typeof(string));	//����ƻ�ʱ��򺽰�����
                dataTableLog.Columns.Add("cncIOTAG" , typeof(string));	//����۱�־��A:���ۺ��ࣻD:���ۺ��ࣩ
                dataTableLog.Columns.Add("cncHOMETAG" , typeof(string));	//������ʹ��ڱ�־��D�CDomestic/���ڣ�I-International/���ʣ�
                dataTableLog.Columns.Add("cnvcSNDR" , typeof(string));	//��Ϣ���ֶ�
                dataTableLog.Columns.Add("cnvcDTTM" , typeof(string));	//��Ϣ����ʱ��
                dataTableLog.Columns.Add("cnvcTYPE" , typeof(string));	//��Ϣ����
                dataTableLog.Columns.Add("cnvcSTYP" , typeof(string));	//��Ϣ������
                dataTableLog.Columns.Add("cncVALIDFLAG" , typeof(string));	//�Ƿ���Ч��־��Ŀǰ���ֶ��������壬����
                dataTableLog.Columns.Add("cnvcCreateTime" , typeof(string));	//��Ϣ���ʱ��
                dataTableLog.Columns.Add("cnvcCancelTime", typeof(string));	//ȡ��ʱ�䣻Ŀǰ���ֶ��������壬����
                dataTableLog.Columns.Add("cnvcMsgValue" , typeof(string));	//����Ϣ������ֵ
                dataTableLog.Columns.Add("cnvcFieldCNName" , typeof(string));	//�ü�¼��Ϣ���͵���������
                dataTableLog.Columns.Add("cnvcFieldENName" , typeof(string));	//�ü�¼��Ϣ���͵�Ӣ������
                dataTableLog.Columns.Add("cnvcResult", typeof(string));	//����������ɹ���ʧ�ܻ򲻴���
                dataTableLog.Columns.Add("cnvcMemo", typeof(string));	//��ע���ɹ� �� ʧ�� ʱ�ı�ע��Ϣ

                //������ʹ�õ� log ��
                dataTableLog_Main = dataTableLog.Clone();

                //����ͼ����Դ
                dataGridView1.DataSource = dataTableLog_Main;
            }
            #endregion ��ȡ�׶���������

            #region ���к��Ż�ȡ����ʱ������
            if (dataTableLog_CC == null)
            {
                //���߳�ʹ�õ� log ��
                dataTableLog_CC = new DataTable();
                dataTableLog_CC.Columns.Add("cncDATOP", typeof(string));	        //��������
                dataTableLog_CC.Columns.Add("cncCKIFlightDate", typeof(string));	//CKI��������
                dataTableLog_CC.Columns.Add("cnvcFLTID", typeof(string));	        //�����
                dataTableLog_CC.Columns.Add("cnvcCKIFlightNo", typeof(string));	    //CKI�����
                dataTableLog_CC.Columns.Add("cniLEGNO", typeof(Int32));	            //���κ�
                dataTableLog_CC.Columns.Add("cnvcAC", typeof(string));	            //�ɻ���Ϣ
                dataTableLog_CC.Columns.Add("cnvcLONG_REG", typeof(string));	    //���ɻ���
                dataTableLog_CC.Columns.Add("cncDEPSTN", typeof(string));	        //��ɻ���
                dataTableLog_CC.Columns.Add("cncARRSTN", typeof(string));	        //�������
                dataTableLog_CC.Columns.Add("cncSTD", typeof(string));	            //�ƻ����ʱ��
                dataTableLog_CC.Columns.Add("cncETD", typeof(string));	            //Ԥ�����ʱ��
                dataTableLog_CC.Columns.Add("cncOutFlightInterceptTime", typeof(string));	            //ϵͳ�б���Ľ���ʱ��
                dataTableLog_CC.Columns.Add("cnvcCC", typeof(string));	            //���к��Ż�ȡ�Ľ���ʱ��
                dataTableLog_CC.Columns.Add("cnvcOperationTime", typeof(string));	//������ʱ��
                dataTableLog_CC.Columns.Add("cnvcMemo", typeof(string));	        //��ע���ɹ� �� ʧ�� ʱ�ı�ע��Ϣ

                //������ʹ�õ� log �� 
                dataTableLog_Main_CC = dataTableLog_CC.Clone();

                //����ͼ����Դ
                dataGridView2.DataSource = dataTableLog_Main_CC;
            }
            #endregion ���к��Ż�ȡ����ʱ������

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region ��ȡ�׶���������
            if (blnBusy_timer1)
                return;

            try
            {
                blnBusy_timer1 = true;

                //
                lock (objDataTableLog)
                {
                    //
                    //foreach (DataRow dataRowDataTableLog in dataTableLog.Rows) 
                    //{
                    //    dataTableLog_Main.ImportRow(dataRowDataTableLog);
                    //    dataRowDataTableLog.Delete(); //�˲������������м���������״̬�����ö�ټ����޷����б����������׳��쳣
                    //}
                    for (int intIndex = 0; intIndex < dataTableLog.Rows.Count; intIndex++ )
                    {
                        dataTableLog_Main.ImportRow(dataTableLog.Rows[intIndex]);
                        dataTableLog.Rows[intIndex].Delete();
                    }
                    //
                    dataTableLog.AcceptChanges();
                }
                //
                if (dataTableLog_Main.Rows.Count > 10000)
                    dataTableLog_Main.Rows.Clear();

                //
                blnBusy_timer1 = false;
            }
            catch (Exception ex)
            {
                blnBusy_timer1 = false;
            }
            #endregion ��ȡ�׶���������

            #region ��ȡ�к��ź��������Ϣ
            if (blnBusy_timer1_CC)
                return;

            try
            {
                blnBusy_timer1_CC = true;

                //
                lock (objDataTableLog_CC)
                {
                    //
                    //foreach (DataRow dataRowDataTableLog_CC in dataTableLog_CC.Rows)
                    //{
                    //    dataTableLog_Main_CC.ImportRow(dataRowDataTableLog_CC);
                    //    dataRowDataTableLog_CC.Delete(); //�˲������������м���������״̬�����ö�ټ����޷����б����������׳��쳣
                    //}
                    for (int intIndex = 0; intIndex < dataTableLog_CC.Rows.Count; intIndex++)
                    {
                        dataTableLog_Main_CC.ImportRow(dataTableLog_CC.Rows[intIndex]);
                        dataTableLog_CC.Rows[intIndex].Delete();
                    }
                    //
                    dataTableLog_CC.AcceptChanges();
                }
                //
                if (dataTableLog_Main_CC.Rows.Count > 10000)
                    dataTableLog_Main_CC.Rows.Clear();

                //
                blnBusy_timer1_CC = false;
            }
            catch (Exception ex)
            {
                blnBusy_timer1_CC = false;
            }
            #endregion ��ȡ�к��ź��������Ϣ

            #region ����澯��Ϣ
            if (blnBusy_timer1_FlightAlarm)
                return;

            try
            {
                //���÷�æ���
                blnBusy_timer1_FlightAlarm = true;

                //����ͬ��
                if (_dtTodayInOutFlights != null)
                {
                    lock (_objTodayInOutFlights) //ͬ��
                    {
                        DataRow[] arrayDataRowsTodayInOutFlights = _dtTodayInOutFlights.Select("cndOperationTime < '" + DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 00:00:00'");
                        for (int intIndex = 0; intIndex < arrayDataRowsTodayInOutFlights.Length; intIndex++)
                        {
                            arrayDataRowsTodayInOutFlights[intIndex].Delete();
                        }
                        _dtTodayInOutFlights.AcceptChanges();


                        dataGridView4.DataSource = _dtTodayInOutFlights.Copy(); //���ݱ�����ڶ��߳���ʹ��ʱ����Ҫͬ�����Ѽ�ʱ�Ը澯��Ϣ��������չʾ
                    }
                }

                //�����æ���
                blnBusy_timer1_FlightAlarm = false;
            }
            catch(Exception ex)
            {
                blnBusy_timer1_FlightAlarm = false; //�����æ���
            }
            #endregion ����澯��Ϣ
        }

        private void fmMessageService_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    //�����ļ��м�¼DTTM
            //    if (strDTTM != "")
            //    {
            //        //��������� DTTMʱ�� ��д�������ļ��е� DTTM �ڵ�
            //        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //        configuration.AppSettings.Settings["DTTM"].Value = strDTTM;
            //        configuration.Save(ConfigurationSaveMode.Modified);  
            //        ConfigurationManager.RefreshSection("appSettings");//���¼����µ������ļ�
            //    }

            //    //�����ļ��м�¼EventID
            //    if (intEventID > 0)
            //    {
            //        //��������� EventID �� 1�� ��д�������ļ��е� EventID �ڵ㣨��֤����©���������¼���´���������ʱ�ٴδ��������¼��
            //        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //        configuration.AppSettings.Settings["EventID"].Value = ((intEventID - 1) as Int32).ToString();
            //        configuration.Save(ConfigurationSaveMode.Modified);
            //        ConfigurationManager.RefreshSection("appSettings");//���¼����µ������ļ�
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //��ʾ Log ҳ��
            tabControl1.SelectedTab = tabControl1.TabPages[1]; ;

            //����������
            if (!blnSetRemotingObject) //��δ������Զ�̴��������� AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("�����������ȡʧ�ܣ������µ�¼��", "��ʾ", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //������Զ�̴��������� AgentServiceDAF.objRemotingObject
            }


            //��ȡ��Ҫ����ĺ������ɻ����б�
            strDEPSTNs = ";";
            ReturnValueSF rvSFStationBF = new ReturnValueSF();
            StationBF stationBF = new StationBF();
            rvSFStationBF = stationBF.GetAllStation();
            if ((rvSFStationBF.Result > 0) &&
                (rvSFStationBF.Dt != null) &&
                (rvSFStationBF.Dt.Rows.Count > 0))
            {
                foreach (DataRow dataRowStationInfor in rvSFStationBF.Dt.Rows)
                {
                    strDEPSTNs = strDEPSTNs + dataRowStationInfor["cncThreeCode"].ToString().Trim() + ";";
                }

                strDEPSTNs = strDEPSTNs.Trim(';');
            }
            else
            {
                MessageBox.Show("��ȡ��Ҫ����ĺ������ɻ����б�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK);
                return;
                //Environment.Exit(0);
            }

            //�����̶߳�ʱ������ʱ ������к��ŵ������ʱ��
            int iRefreshInterval = 480 * 1000; //����Ƶ������Ϊ 8����
            TimerCallback timerDelegate = new TimerCallback(GetCCMessage);
            timer_CC = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);


            
            //
            timer1.Enabled = true;

            button2.Enabled = false;

        }

        public void GetCCMessage(object state)
        {
            //�����æ�����˳�������
            if (blnBusy_CC)
                return;

            //���÷�æ���
            blnBusy_CC = true;

            //�ֽ� strDEPSTNs ���õ������б�
            string[] arrayDEPSTNs = strDEPSTNs.Split(';');

            //���ô����ʱ���
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dateTimeBM.EndDateTime = DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss");

            //������������
            foreach (string strDEPSTN in arrayDEPSTNs)
            {
                //��ȡ���� strDEPSTN ��Ӧʱ����ڵĽ����ۺ���
                StationBM stationBM = new StationBM();
                AccountBM accountBM = new AccountBM();

                stationBM.ThreeCode = strDEPSTN;
                accountBM.UserName = "SYS_TTL";
                accountBM.IPAddress = "";

                ReturnValueSF returnValueSF = null;
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
                returnValueSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM); //���ô������

                if ((returnValueSF.Result > 0) && (returnValueSF.Dt != null) && (returnValueSF.Dt.Rows.Count > 0)) //��������
                {
                    DataTable dataTableFlights = returnValueSF.Dt;
                    foreach (DataRow dataRowFlights in dataTableFlights.Rows) //��������
                    {
                        if ((dataRowFlights["cncDEPSTN"].ToString().Trim() == strDEPSTN) && //ֻ������ɻ����ڻ����б���ĺ���
                            (Convert.ToDateTime(dataRowFlights["cncETD"].ToString().Trim()) >= Convert.ToDateTime(dateTimeBM.StartDateTime)) && //ֻ������ָ��ʱ����ڵĺ���
                            (Convert.ToDateTime(dataRowFlights["cncETD"].ToString().Trim()) <  Convert.ToDateTime(dateTimeBM.EndDateTime)))
                        {
                            bool blnExceptionMessage = false; //true ���쳣������false û���쳣����
                            string strExceptionMessage = ""; //�쳣�����Ĵ�����Ϣ

                            string strCKIFlightDate = "";
                            string strCKIFlightNo = "";
                            string strCC = ""; //���к���ϵͳ��ȡ���������Ϣ

                            try
                            {
                                bool blnPutInDB = false; //����ʶ��true ��⣬false �����
                                strCKIFlightDate = dataRowFlights["cncCKIFlightDate"].ToString().Trim();
                                strCKIFlightNo = dataRowFlights["cnvcCKIFlightNo"].ToString().Trim();
                                strCC = GetCCMessage(strCKIFlightDate, strCKIFlightNo, strDEPSTN); //���к���ϵͳ��ȡ���������Ϣ
                                #region ��������Ϣ���
                                if (dataRowFlights["cncOutFlightInterceptTime"].ToString().Trim().Length == 4) 
                                {
                                    if (strCC.Trim().Length == 4) 
                                    {
                                        if (dataRowFlights["cncOutFlightInterceptTime"].ToString().Trim() != strCC.Trim())
                                        {
                                            blnPutInDB = true;
                                        }
                                    }
                                }
                                else if (dataRowFlights["cncOutFlightInterceptTime"].ToString().Trim() != strCC.Trim())
                                {
                                    blnPutInDB = true;
                                }

                                //���� ����ʶ ����������
                                if (blnPutInDB)
                                {
                                    OMPWebReference.FlightMonitorData wrFlightMonitorData = new MessageService.OMPWebReference.FlightMonitorData();
                                    bool blnwrFlightMonitorData_MaintainGuaranteeInfor = wrFlightMonitorData.MaintainGuaranteeInfor(
                                        "SYS_TTL", // UserID,
                                        dataRowFlights["cnvcFLTID"].ToString(),   // FLTID,
                                        dataRowFlights["cncDATOP"].ToString(),    // DATOP,
                                        Convert.ToInt32(dataRowFlights["cniLEGNO"].ToString()),    // LegNo,
                                        dataRowFlights["cnvcAC"].ToString(),    // AC,
                                        dataRowFlights["cncDEPSTN"].ToString(),    // DepSTN,
                                        dataRowFlights["cncARRSTN"].ToString(),    // ArrSTN,
                                        dataRowFlights["cncSTD"].ToString(),    // STD,
                                        dataRowFlights["cncETD"].ToString(),    // ETD,
                                        dataRowFlights["cncSTA"].ToString(),    // STA,
                                        dataRowFlights["cncETA"].ToString(),    // ETA,
                                        "cncOutFlightInterceptTime", // ChangeReasonCode,
                                        dataRowFlights["cncOutFlightInterceptTime"].ToString(), // ChangeOldContent,
                                        strCC, // ChangeNewContent,
                                        1, //  FieldType,
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") // LocalOperatingTime
                                        );
                                    if (!blnwrFlightMonitorData_MaintainGuaranteeInfor)
                                        throw new Exception("������Ϣά������վ����ϵͳʧ�ܣ�" + Environment.NewLine +
                                            "DATOP��" + dataRowFlights["cncDATOP"].ToString() + Environment.NewLine +
                                            "FLTID��" + dataRowFlights["cnvcFLTID"].ToString() + Environment.NewLine +
                                            "LEGNO��" + dataRowFlights["cniLEGNO"].ToString() + Environment.NewLine +
                                            "AC��" + dataRowFlights["cnvcAC"].ToString());
                                }
                                #endregion ��������Ϣ���

                            }
                            catch (Exception ex)
                            {
                                blnExceptionMessage = true;
                                strExceptionMessage = ex.Message;
                            }

                            #region ��¼�� log��
                            lock (objDataTableLog_CC)
                            {
                                DataRow dataRowLog_CC = dataTableLog_CC.NewRow();
                                dataRowLog_CC["cncDATOP"] = dataRowFlights["cncDATOP"].ToString();
                                dataRowLog_CC["cncCKIFlightDate"] = dataRowFlights["cncCKIFlightDate"].ToString();
                                dataRowLog_CC["cnvcFLTID"] = dataRowFlights["cnvcFLTID"].ToString();
                                dataRowLog_CC["cnvcCKIFlightNo"] = dataRowFlights["cnvcCKIFlightNo"].ToString();
                                dataRowLog_CC["cniLEGNO"] = Convert.ToInt32(dataRowFlights["cniLEGNO"].ToString());
                                dataRowLog_CC["cnvcAC"] = dataRowFlights["cnvcAC"].ToString();
                                dataRowLog_CC["cnvcLONG_REG"] = dataRowFlights["cnvcLONG_REG"].ToString();
                                dataRowLog_CC["cncDEPSTN"] = dataRowFlights["cncDEPSTN"].ToString();
                                dataRowLog_CC["cncARRSTN"] = dataRowFlights["cncARRSTN"].ToString();
                                dataRowLog_CC["cncSTD"] = dataRowFlights["cncSTD"].ToString();
                                dataRowLog_CC["cncETD"] = dataRowFlights["cncETD"].ToString();
                                dataRowLog_CC["cncOutFlightInterceptTime"] = dataRowFlights["cncOutFlightInterceptTime"].ToString();
                                dataRowLog_CC["cnvcCC"] = strCC;
                                dataRowLog_CC["cnvcOperationTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                if (blnExceptionMessage)
                                    dataRowLog_CC["cnvcMemo"] = "����" + strExceptionMessage;
                                else
                                    dataRowLog_CC["cnvcMemo"] = "";
                                dataTableLog_CC.Rows.Add(dataRowLog_CC);
                            }
                            #endregion ��¼�� log��

                        }
                    }
                }

            }

            //�����æ���
            blnBusy_CC = false;

        }

        /// <summary>
        /// ���й�����Ϣ����ɷ����޹�˾��TravelSky Technology Limited,��ơ��к��š���ϵͳ��ȡ������Ϣ
        /// </summary>
        /// <param name="CKIFlightDate">�������ڣ���ʽ��18JUN15��ddMMMyy��ʽ</param>
        /// <param name="CKIFlightNo">�����</param>
        /// <param name="CityDEPSTN">��ɻ���</param>
        /// <returns>���ؽ����ַ������� CC0830 �����ؿ��ַ�������ʾ��δ�յ�������Ϣ������ ERROR����ʾ�д���</returns>
        private string GetCCMessage(string CKIFlightDate, string CKIFlightNo, string CityDEPSTN)
        {
            #region
            //PaxService.PaxService objPaxService = new PaxService.PaxService();
            //string strFlightNo = changeLegsBM.CKIFlightNo.Replace(" ", "").ToUpper();

            //string strCommnadText = "SY ";
            //strCommnadText += strFlightNo + "/";
            //strCommnadText += DateTime.Parse(changeLegsBM.CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "/";
            //strCommnadText += changeLegsBM.CityDEPSTN + ",Z";

            ////string strResult = objPaxService.PaxCheckNum(strCommnadText);
            //string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");

            //if (strResult.Length < 30 || strResult == "Disconncted,Please connect again")
            //{
            //    return new CheckPaxBM();
            //}
            //else
            //{
            //    return GetCheckPaxInfoFromTheText(strResult, changeLegsBM.CityDEPSTN, changeLegsBM.CityARRSTN);
            //}
            #endregion 

            try
            {
                PaxService.PaxService objPaxService = new PaxService.PaxService();
                string strFlightNo = CKIFlightNo.Replace(" ", "").ToUpper();

                string strCommnadText = "SY ";
                strCommnadText += strFlightNo + "/";
                strCommnadText += DateTime.Parse(CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "/";
                strCommnadText += CityDEPSTN + ",Z";

                string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");

                //
                if (strResult.Length < 30 || strResult == "Disconncted,Please connect again")
                {
                    return "ERR";
                }

                string[] arrayResult = strResult.Split('\n');
                if (arrayResult.Length < 3)
                {
                    return "ERR";
                }

                //
                string strResult_1 = arrayResult[0].ToString().Trim();
                string[] arrayResult_1 = strResult_1.Split(' ');

                if (arrayResult_1.Length == 0)
                {
                    return "ERR";
                }

                string strResult_1_l = arrayResult_1[arrayResult_1.Length - 1].ToString().Trim();
                string[] arrayResult_1_l = strResult_1_l.Split('/');

                if (arrayResult_1_l.Length != 2)
                {
                    return "ERR";
                }

                string strResult_1_l_1 = arrayResult_1_l[0].ToString().Trim();
                if (strResult_1_l_1.Length != 6)
                {
                    return "";
                }
                else if (strResult_1_l_1.Trim().Substring(0, 2).ToUpper() != "CL")
                {
                    return ""; 
                }
                else
                {
                    return strResult_1_l_1.Trim().Substring(2, 4); //��Ҫ�Ľ������ 0830
                }

            }
            catch (Exception ex)
            {
                return "ERR";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region ���ϸ澯���ֶ����ʼ��
            //��ȡ��վ��վʱ����Ϣ
            OverStationTimeBF overStationTimeBF = new OverStationTimeBF();
            ReturnValueSF rvsfOverStationTime = overStationTimeBF.Select();
            if ((rvsfOverStationTime.Result > 0) && (rvsfOverStationTime.Dt != null))
            {
                _dtOverStationTime = rvsfOverStationTime.Dt;
            }
            else
            {
                MessageBox.Show("�����ݿ�����ȡ��վ��վʱ����Ϣʧ�ܣ�" +
                    Environment.NewLine + "������Ϣ��" +
                    rvsfOverStationTime.Message
                    , "��ʾ", MessageBoxButtons.OK);

                Environment.Exit(0); //�˳�����
            }

            //��ȡ��վ����ʱ����Ϣ
            StationBF stationBF = new StationBF();
            ReturnValueSF rvsfStationBF = stationBF.GetAllStation();
            if ((rvsfStationBF.Result > 0) && (rvsfStationBF.Dt != null))
            {
                _dtStationInfor = rvsfStationBF.Dt;
            }
            else
            {
                MessageBox.Show("�����ݿ�����ȡ��վ����ʱ����Ϣʧ�ܣ�" +
                    Environment.NewLine + "������Ϣ��" +
                    rvsfStationBF.Message
                    , "��ʾ", MessageBoxButtons.OK);

                Environment.Exit(0); //�˳�����
            }



            //���졢���պ�����ĺ��������Ϣ
            FlightAlarmInfoBF flightAlarmInfoBF = new FlightAlarmInfoBF();
            ReturnValueSF rvsfFlightAlarmInfo = flightAlarmInfoBF.Select(
                DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            if ((rvsfFlightAlarmInfo.Result > 0) && (rvsfFlightAlarmInfo.Dt != null))
            {
                _dtTodayInOutFlights = rvsfFlightAlarmInfo.Dt;

                //��������
                _dtTodayInOutFlights.PrimaryKey = new DataColumn[] {
                    _dtTodayInOutFlights.Columns["cncOutDATOP"], 
                    _dtTodayInOutFlights.Columns["cnvcOutFLTID"],
                    _dtTodayInOutFlights.Columns["cniOutLEGNO"], 
                    _dtTodayInOutFlights.Columns["cnvcOutAC"],
                    _dtTodayInOutFlights.Columns["cncInDATOP"], 
                    _dtTodayInOutFlights.Columns["cnvcInFLTID"],
                    _dtTodayInOutFlights.Columns["cniInLEGNO"], 
                    _dtTodayInOutFlights.Columns["cnvcInAC"],
                    _dtTodayInOutFlights.Columns["cnvcAlarmCode"]
               };
            }
            else
            {
                MessageBox.Show("�����ݿ�����ȡ����澯��Ϣʧ�ܣ�" +
                    Environment.NewLine + "������Ϣ��" +
                    rvsfFlightAlarmInfo.Message
                    , "��ʾ", MessageBoxButtons.OK);

                Environment.Exit(0); //�˳�����
            }
            #endregion ���ϸ澯���ֶ����ʼ��

            //��ʾ Log ҳ��
            tabControl1.SelectedTab = tabControl1.TabPages[2];

            #region ����������
            if (!blnSetRemotingObject) //��δ������Զ�̴��������� AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("�����������ȡʧ�ܣ������µ�¼��", "��ʾ", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //������Զ�̴��������� AgentServiceDAF.objRemotingObject
            }
            #endregion ����������

            //��ʼ�� �Ѵ����˵Ľ����ۺ�������Ĳ���ʱ��
            _arrayDateTime[0] = DateTime.Now;

            #region �����Ҫ����Ļ����б������̶߳�ʱ������ʱ������澯��Ϣ
            //string strAirportList = "HAK;CAN;PEK;PVG;XIY;TYN;TSN";
            //string strAirportList = "HAK;CAN;PEK;PVG;XIY;SHA,DLC,TYN";
            string strAirportList = ConfigurationSettings.AppSettings["AirportList"].ToString().Trim();
            string[] arrayAirportList = strAirportList.Split(';');
            for (int iIndexAirportList = 0; iIndexAirportList < arrayAirportList.Length; iIndexAirportList++)
            {
                //����������
                string strAirport = arrayAirportList[iIndexAirportList];

                //��ʼ������
                StationBM stationBM = new StationBM();
                AccountBM accountBM = new AccountBM();

                stationBM.ThreeCode = strAirport;
                accountBM.UserId = "SYS_FlightAlarm"; //���Ե�ʱ���õ��ʺ��� l_w
                accountBM.UserName = "�澯����(ϵͳ)";
                accountBM.IPAddress = "";

                //��ȡ�û���Ȩ�޵�������
                DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
                DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

                //��ʼ�� �����ۺ���� �� ����ͬ������
                /*
                _arrayTodayInOutFlights[iIndexAirportList + 1] = _dtTodayInOutFlights.Copy();
                _dtTodayInOutFlights = _arrayTodayInOutFlights[iIndexAirportList + 1];

                _arrayObjTodayInOutFlights[iIndexAirportList + 1] = new object();
                _objTodayInOutFlights = _arrayObjTodayInOutFlights[iIndexAirportList + 1];
                */

                //�߳�ִ�к��������ĳ�ʼ��
                object[] objectsList = new object[9];
                objectsList[0] = _dtTodayInOutFlights; //�����ۺ����ڴ��
                objectsList[1] = _dtOverStationTime; //��վ��׼ʱ���ڴ��
                objectsList[2] = stationBM; //��վ����
                objectsList[3] = accountBM; //�û�����
                objectsList[4] = _arrayBusy; //��æ�������
                objectsList[5] = iIndexAirportList + 1; //ʹ�õ��� ��æ������� �е�λ��
                objectsList[6] = _objTodayInOutFlights; //�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������
                objectsList[7] = dtDataItems; //�û���Ȩ�޵�������
                objectsList[8] = _dtStationInfor; //����ʱ���ڴ��

                ////���� ������澯��Ϣ����
                //DealFlightAlarmInfo(objectsList);

                //�����̶߳�ʱ������ʱ ������澯��Ϣ
                int iRefreshInterval = 120 * 1000; //����Ƶ������Ϊ 2����
                TimerCallback timerDelegate = new TimerCallback(DealFlightAlarmInfo);
                _arrayTimer[iIndexAirportList + 1] = new System.Threading.Timer(timerDelegate, objectsList, 0, iRefreshInterval); //�洢���̶߳�ʱ�����������б�λ�� 1 �Ժ�
            }
            #endregion �����Ҫ����Ļ����б������̶߳�ʱ������ʱ������澯��Ϣ

            #region �����̶߳�ʱ������ʱ ������澯��Ϣ
            //�߳�ִ�к��������ĳ�ʼ��
            object[] arrayObjectList = new object[6];
            arrayObjectList[0] = _dtTodayInOutFlights; //�����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�)
            arrayObjectList[1] = _arrayDateTime; //�Ѵ����˵Ľ����ۺ�������Ĳ���ʱ�䣬ʱ������
            arrayObjectList[2] = 0; //�Ѵ����˵Ľ����ۺ�������Ĳ���ʱ�䣬λ��
            arrayObjectList[3] = _arrayBusy; //��æ�������
            arrayObjectList[4] = 0; //ʹ�õ��� ��æ������� �е�λ��
            arrayObjectList[5] = _objTodayInOutFlights; //�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������

            //�����̶߳�ʱ������ʱ ������澯��Ϣ
            int iRefreshInterval_0 = 60 * 1000; //����Ƶ������Ϊ 1 ���� -- ��ʱ���ã�����ʹ��
            TimerCallback timerDelegate_0 = new TimerCallback(DealFlightAlarmInfo_PutInDB);
            _arrayTimer[0] = new System.Threading.Timer(timerDelegate_0, arrayObjectList, 0, iRefreshInterval_0); //�洢���̶߳�ʱ�����������б�λ�� 0
            #endregion �����̶߳�ʱ������ʱ ������澯��Ϣ

            //
            timer1.Enabled = true;
            button3.Enabled = false;
        }


        #region ���ౣ�ϸ澯���� �õ��ĺ���

        #region �澯���ݴ������ȣ�
        /// <summary>
        /// �澯���ݴ������ȣ�
        /// </summary>
        /// <param name="parameterList">�����б�0 �����ۺ����ڴ��1 �Ѵ����˵Ľ����ۺ�������Ĳ���ʱ�䣻2 ��æ������飻3 ʹ�õ��� ��æ������� �е�λ��</param>
        private void DealFlightAlarmInfo_PutInDB(object parameterList)
        {
            //�����б�
            object[] objectsList = (object[])parameterList;
            DataTable todayInOutFlights_Mem = (DataTable)(objectsList[0]); //�����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�)
            DateTime[] arrayDateTime = (DateTime[])(objectsList[1]); //�Ѵ����˵Ľ����ۺ�������Ĳ���ʱ�䣬ʱ������
            int iIndexDateTime = (int)(objectsList[2]); ; //�Ѵ����˵Ľ����ۺ�������Ĳ���ʱ�䣬λ��
            string strOperationTime = arrayDateTime[iIndexDateTime].ToString("yyyy-MM-dd HH:mm:ss.fffffff"); //�Ѵ����˵Ľ����ۺ�������Ĳ���ʱ��
            bool[] arrayBusy = (bool[])(objectsList[3]); //��æ�������
            int iIndexBusy = (int)(objectsList[4]); //ʹ�õ��� ��æ������� �е�λ��
            object objTodayInOutFlights = (object)(objectsList[5]);  //�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������


            //�Ƿ�æ
            if (arrayBusy[iIndexBusy])
                return; //�����æ�����˳�
            arrayBusy[iIndexBusy] = true; //���� ��æ ���

            #region ���ݴ������ȣ�
            DataRow[] arrsyDataRowsTodayInOutFlights_Mem = null;
            lock (objTodayInOutFlights) //ѡ�������е� >= �Ƿ�Ӧ��Ϊ > �� �Ѹ�Ϊ >
            {
                arrsyDataRowsTodayInOutFlights_Mem = todayInOutFlights_Mem.Select("cndOperationTime > '" + strOperationTime + "'", "cndOperationTime"); //��ȡ��δ����ļ�¼
            }

            int iTraceInfo_1 = arrsyDataRowsTodayInOutFlights_Mem.Length; //������Ϣ��������ʹ�ã���ʾ��ȡ�ļ�¼����
            int iTraceInfo_2 = 1; //������Ϣ��������ʹ�ã���ʾ��¼�ڼ�¼����λ��
            foreach (DataRow dataRowTodayInOutFlights_Mem in arrsyDataRowsTodayInOutFlights_Mem) //������ȡ���Ļ�δ����ļ�¼
            {
                string strTraceInfo = ""; //������Ϣ��������ʹ��

                strTraceInfo = strTraceInfo + "[" + iTraceInfo_2.ToString() + @"/" + iTraceInfo_1.ToString() + "]";
                strTraceInfo = strTraceInfo + "Gao��" + DateTime.Now.ToString("mm:ss.fffffff") + "��" ;

                FlightAlarmInfoBM flightAlarmInfoBM = new FlightAlarmInfoBM(dataRowTodayInOutFlights_Mem); //���ɸ澯����

                #region ���
                strTraceInfo = strTraceInfo + "Ti��" + DateTime.Now.ToString("mm:ss.fffffff") + "��";

                FlightAlarmInfoBF flightAlarmInfoBF = new FlightAlarmInfoBF();
                ReturnValueSF rvsfFlightAlarmInfoBF = flightAlarmInfoBF.Select(
                    flightAlarmInfoBM.cncOutDATOP,
                    flightAlarmInfoBM.cnvcOutFLTID,
                    flightAlarmInfoBM.cniOutLEGNO,
                    flightAlarmInfoBM.cnvcOutAC,
                    flightAlarmInfoBM.cncInDATOP,
                    flightAlarmInfoBM.cnvcInFLTID,
                    flightAlarmInfoBM.cniInLEGNO,
                    flightAlarmInfoBM.cnvcInAC,
                    flightAlarmInfoBM.cnvcAlarmCode
                ); //���ݹؼ���Ϣ��ȡ���ݿ������ݣ��ж������Ƿ��Ѵ���

                strTraceInfo = strTraceInfo + "Ru��" + DateTime.Now.ToString("mm:ss.fffffff") + "��";

                if ((rvsfFlightAlarmInfoBF.Result > 0) &&
                    (rvsfFlightAlarmInfoBF.Dt.Rows.Count == 0)) //�޴˼�¼������
                {
                    ReturnValueSF rvsfFlightAlarmInfoBF_Add = flightAlarmInfoBF.Add(flightAlarmInfoBM);
                    #region ��¼���Ӳ������
                    if (rvsfFlightAlarmInfoBF_Add.Result == 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "�ɹ�������";
                    }
                    else if (rvsfFlightAlarmInfoBF_Add.Result > 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "ʧ�ܣ����Ӽ�¼��������1";
                    }
                    else if (rvsfFlightAlarmInfoBF_Add.Result == 0)
                    {
                        flightAlarmInfoBM.cnvcMemo = "ʧ�ܣ����Ӽ�¼��������0";
                    }
                    else
                    {
                        flightAlarmInfoBM.cnvcMemo = "ʧ�ܣ����ӣ�" + rvsfFlightAlarmInfoBF_Add.Message;
                    }
                    #endregion ��¼���Ӳ������
                }
                else if ((rvsfFlightAlarmInfoBF.Result > 0) &&
                    (rvsfFlightAlarmInfoBF.Dt.Rows.Count == 1)) //�д˼�¼������
                {
                    ReturnValueSF rvsfFlightAlarmInfoBF_Update = flightAlarmInfoBF.Update(flightAlarmInfoBM);
                    #region ��¼���Ӹ��½��
                    if (rvsfFlightAlarmInfoBF_Update.Result == 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "�ɹ�������";
                    }
                    else if (rvsfFlightAlarmInfoBF_Update.Result > 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "ʧ�ܣ����¼�¼��������1";
                    }
                    else if (rvsfFlightAlarmInfoBF_Update.Result == 0)
                    {
                        flightAlarmInfoBM.cnvcMemo = "ʧ�ܣ����¼�¼��������0";
                    }
                    else
                    {
                        flightAlarmInfoBM.cnvcMemo = "ʧ�ܣ����£�" + rvsfFlightAlarmInfoBF_Update.Message;
                    }
                    #endregion ��¼���Ӹ��½��
                }
                else //��������Ĵ���
                {
                    #region ��¼�������
                    flightAlarmInfoBM.cnvcMemo = "ʧ�ܣ�ѡ��" + rvsfFlightAlarmInfoBF.Message;
                    #endregion ��¼�������
                }
                #endregion ���

                strTraceInfo = strTraceInfo + "End��" + DateTime.Now.ToString("mm:ss.fffffff") + "��";

                #region ���ٷ��֣���ͬ���軨�� 1���� ʱ�䣬�ڴ������������ʱ�������������̺�ʱ�Ͼ� -- ��ע�ʹ���˲��ִ���
                /*
                #region ��������������ڴ��memo�ֶ� �� cndPutInDBTime�ֶΣ� --modified by LinYong in 20160601
                //���ٷ��֣���ͬ���軨�� 1���� ʱ�䣬�ڴ������������ʱ�������������̺�ʱ�Ͼ�
                lock (objTodayInOutFlights)
                {
                    dataRowTodayInOutFlights_Mem["cnvcMemo"] = flightAlarmInfoBM.cnvcMemo + "��" + strTraceInfo + "��";
                    dataRowTodayInOutFlights_Mem["cndPutInDBTime"] = DateTime.Now;
                }
                #endregion ��������������ڴ��memo�ֶΣ�
                */
                #endregion ���ٷ��֣���ͬ���軨�� 1���� ʱ�䣬�ڴ������������ʱ�������������̺�ʱ�Ͼ� -- ��ע�ʹ���˲��ִ���

                #region ��¼��������¼�����Ĳ���ʱ�䣬�洢���ڴ�ʱ�������У����´ε���ʹ��
                arrayDateTime[iIndexDateTime] = flightAlarmInfoBM.cndOperationTime;
                #endregion ��¼��������¼�����Ĳ���ʱ�䣬�洢���ڴ�ʱ�������У����´ε���ʹ��

                iTraceInfo_2 = iTraceInfo_2 + 1;
            }
            #endregion ���ݴ������ȣ�

            //�Ƿ�æ�������
            arrayBusy[iIndexBusy] = false; //���� ����æ ���
        }
        #endregion �澯���ݴ������ȣ�

        #region ������澯��Ϣ -- �ṩ�߳�ʹ�� -- ��ʹ��

        ///// <summary>
        ///// ������澯��Ϣ -- �ṩ�߳�ʹ��
        ///// </summary>
        ///// <param name="parameterList">�����б������ۺ����ڴ����վ��׼ʱ���ڴ��վ�����û�����</param>
        //private void DealFlightAlarmInfo_Bak20150819(object parameterList)
        //{
        //    //�����б�
        //    object[] objectsList = (object[])parameterList;
        //    DataTable todayInOutFlights = (DataTable)(objectsList[0]); //�����ۺ����ڴ��
        //    DataTable overStationTime = (DataTable)(objectsList[1]); //��վ��׼ʱ���ڴ��
        //    StationBM stationBM = (StationBM)(objectsList[2]); //��վ����
        //    AccountBM accountBM = (AccountBM)(objectsList[3]); //�û�����
        //    bool[] arrayBusy = (bool[])(objectsList[4]); //��æ�������
        //    int iIndexBusy = (int)(objectsList[5]); //ʹ�õ��� ��æ������� �е�λ��
        //    object objTodayInOutFlights = (object)(objectsList[6]);  //�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������
        //    DataTable dtDataItems = (DataTable)(objectsList[7]); //�û���Ȩ�޵�������



        //    //�Ƿ�æ
        //    if (arrayBusy[iIndexBusy])
        //        return; //�����æ�����˳�
        //    arrayBusy[iIndexBusy] = true; //���� ��æ ���

        //    //��ȡ�ú�վ��������к���
        //    GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
        //    DateTimeBM dateTimeBM = new DateTimeBM();
        //    if ((DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00")) &&
        //        (DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00")))
        //    {
        //        dateTimeBM.StartDateTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 05:00:00";
        //        dateTimeBM.EndDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
        //    }
        //    else
        //    {
        //        dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
        //        dateTimeBM.EndDateTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
        //    } //���Կ����¸������ʱ�����䣺����ddateTimeBM.StartDateTime��dateTimeBM.EndDateTime��ֵ��ʱ�����䣬�Լ�dateTimeBM.StartDateTime��dateTimeBM.EndDateTimeӦ�÷ֱ�ֵ����

        //    ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
        //    DataTable dtTodayStationFlights = rvSF.Dt;

        //    //��ȡ�û���Ȩ�޵�������
        //    //DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
        //    //DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

        //    //�����ۺ�����Schema
        //    DataTable dtInOutFlightsSchema = GetDisplaySchema(dtDataItems);

        //    //��ȡ��������ۺ��� 
        //    //DataTable dtTodayInOutFlights = FillInOutFlights(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);
        //    IList ilTodayInOutFlights = FillInOutFlights_1(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);

        //    //������
        //    //DataView dataView  = new DataView(dtTodayInOutFlights, "", "cniRowIndex", DataViewRowState.CurrentRows);
        //    //dataGridView3.DataSource = dataView;
        //    //dataGridView3.DataSource = ilTodayInOutFlights;

        //    #region ���������ۺ����
        //    IEnumerator ieTodayInOutFlights = ilTodayInOutFlights.GetEnumerator();
        //    while (ieTodayInOutFlights.MoveNext())
        //    {
        //        string strInDEPSTN = "";
        //        string strInARRSTN = "";
        //        string strOutDEPSTN = "";
        //        string strOutARRSTN = "";
        //        string strOverStationType = ""; //��վ���ͣ�ʼ������վ�����ٹ�վ������
        //        int iOverStationStandardTime = 0; //��վ��׼ʱ�䣨���ӣ�
        //        string strOverStationStart = ""; //��ʼʱ�̣��� ���㵽��ʱ��
        //        string strOverStationEnd = ""; //����ʱ�̣��� �������ʱ��
        //        string strAlarmCode = ""; //�澯����
        //        string strAlarmValue = ""; //�澯ֵ
        //        int iAlarmResult = 0; //�澯���


        //        try
        //        {
        //            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current; //ʵ������ǰ�����ۺ�����

        //            #region �˹�ģ�ⲿ�ֲ���
        //            //iOverStationStandardTime = 45; //ģ�� TYN �Ĺ�վʱ�䣬Ӧ�ý�� С���� ��̬����
        //            //strAlarmCode = "OutcncOutPilotArriveTime";
        //            #endregion �˹�ģ�ⲿ�ֲ���

        //            #region ȷ�������������������
        //            if (guaranteeInforBM.IncncDATOP != "1900-01-01") //���ۺ���
        //            {
        //                DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
        //                    "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
        //                    "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
        //                    "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

        //                if (dataRowFlight.Length > 0)
        //                {
        //                    strInDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
        //                    strInARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
        //                }
        //            }

        //            if (guaranteeInforBM.OutcncDATOP != "1900-01-01") //���ۺ���
        //            {
        //                DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
        //                   "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
        //                   "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
        //                   "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

        //                if (dataRowFlight.Length > 0)
        //                {
        //                    strOutDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
        //                    strOutARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();

        //                }
        //            }
        //            #endregion ȷ�������������������

        //            #region ȷ����վʱ��
        //            DataRow[] dataRowsOverStationTime = overStationTime.Select(
        //                "(cncAirportThreeCode = '" +
        //                stationBM.ThreeCode + "') and (cnvcSmallACTYP = '" +
        //                guaranteeInforBM.IncncACTYP + "')"); //���ݻ����������С���ͻ�ȡ��վʱ������
        //            if (dataRowsOverStationTime.Length > 0)
        //            {
        //                iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniGroundTime"].ToString());
        //            }
        //            else
        //            {
        //                throw new Exception("��վʱ�����û�д˼�¼��" + Environment.NewLine +
        //                    "������" + stationBM.ThreeCode + Environment.NewLine +
        //                    "С���ͣ�" + guaranteeInforBM.IncncACTYP);
        //            }
        //            #endregion ȷ����վʱ��

        //            #region ȷ����վ����
        //            if ((guaranteeInforBM.IncncDATOP == "1900-01-01") &&
        //                (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
        //            {
        //                strOverStationType = "ʼ��";
        //            }
        //            else if ((guaranteeInforBM.IncncDATOP != "1900-01-01") &&
        //                (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
        //            {
        //                strOverStationType = "��վ";
        //            }
        //            else
        //            {
        //                strOverStationType = "����";
        //            }
        //            #endregion ȷ����վ����

        //            #region ȷ�� ��ʼʱ�� �� ����ʱ�̣��� ȷ�� �������ʱ�� �� ���㵽��ʱ��
        //            if ((strOverStationType == "��վ") ||
        //                (strOverStationType == "���ٹ�վ"))
        //            {
        //                //���� ��ʼʱ��
        //                if (guaranteeInforBM.IncncAllStatus != "ATA")
        //                {
        //                    strOverStationStart = guaranteeInforBM.IncncAllETA;
        //                }
        //                else
        //                {
        //                    strOverStationStart = guaranteeInforBM.IncncAllATA;
        //                }
        //                //���� ����ʱ��
        //                if (Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime) > Convert.ToDateTime(guaranteeInforBM.OutcncAllETD))
        //                {
        //                    strOverStationEnd = Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime).ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else
        //                {
        //                    strOverStationEnd = guaranteeInforBM.OutcncAllETD;
        //                }

        //            }
        //            else if (strOverStationType == "ʼ��")
        //            {
        //                //���� ��ʼʱ��
        //                strOverStationStart = "";

        //                //���� ����ʱ��
        //                strOverStationEnd = guaranteeInforBM.OutcncAllETD;
        //            }
        //            else //���󺽰಻����
        //            {
        //                continue;
        //            }
        //            #endregion ȷ�� ��ʼʱ�� �� ����ʱ�̣��� ȷ�� �������ʱ�� �� ���㵽��ʱ��

        //            #region �����鵽λ��ʱ�� �ж� ʹ�����ֶ� OutcncOutCrewArriveTime -- �˲��ִ��벻ʹ��
        //            /*
        //            #region �����鵽λ��ʱ�� �ж�
        //            guaranteeInforBM.OutcncOutCrewArriveTime
        //            if (strAlarmCode == "OutcncOutPilotArriveTime")
        //            {
        //                DateTime dOutPilotArriveTime = new DateTime(); //ȷ������Ӧ�õ�λ��ʱ��
        //                if ((strOverStationType == "��վ") ||
        //                    (strOverStationType == "���ٹ�վ"))
        //                {

        //                    //ȷ������Ӧ�õ�λ��ʱ��
        //                    if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
        //                    {
        //                        dOutPilotArriveTime = Convert.ToDateTime(strOverStationStart);
        //                    }
        //                    else
        //                    {
        //                        dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                    }
        //                }
        //                else if (strOverStationType == "ʼ��")
        //                {
        //                    dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                }
        //                //�澯ֵ
        //                strAlarmValue = guaranteeInforBM.OutcncOutPilotArriveTime;

        //                //�澯��� �жϣ�����Ӧ�õ�λ��ʱ�� ��ǰ 5���� ��ʼ�жϣ�
        //                if (DateTime.Now < dOutPilotArriveTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //��δ���ж�ʱ��
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncOutPilotArriveTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncOutPilotArriveTime ��δ¼������
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncOutPilotArriveTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(2, 2) +
        //                            ":00"); //����ʵ�ʵ�λʱ��
        //                        if (dOutcncOutPilotArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
        //                        {
        //                            dOutcncOutPilotArriveTime.AddDays(-1);
        //                        }

        //                        if (dOutcncOutPilotArriveTime > dOutPilotArriveTime)
        //                        {
        //                            iAlarmResult = 2; //��
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //׼ʱ 
        //                        }
        //                    }
        //                }
        //                //�����ڴ�� _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    iAlarmResult.ToString());
        //            }
        //            #endregion �����鵽λ��ʱ�� �ж�
        //            */
        //            #endregion �����鵽λ��ʱ�� �ж� ʹ�����ֶ� OutcncOutCrewArriveTime -- �˲��ִ��벻ʹ��

        //            #region �����鵽λ��ʱ�� �ж�
        //            strAlarmCode = "OutcncOutCrewArriveTime";

        //            if (strAlarmCode == "OutcncOutCrewArriveTime")
        //            {
        //                DateTime dOutCrewArriveTime = new DateTime(); //ȷ������Ӧ�õ�λ��ʱ��
        //                if ((strOverStationType == "��վ") ||
        //                    (strOverStationType == "���ٹ�վ"))
        //                {

        //                    //ȷ������Ӧ�õ�λ��ʱ��
        //                    if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
        //                    {
        //                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationStart);
        //                    }
        //                    else
        //                    {
        //                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                    }
        //                }
        //                else if (strOverStationType == "ʼ��")
        //                {
        //                    dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                }
        //                //�澯ֵ
        //                strAlarmValue = guaranteeInforBM.OutcncOutCrewArriveTime;

        //                //�澯��� �жϣ�����Ӧ�õ�λ��ʱ�� ��ǰ 5���� ��ʼ�жϣ�
        //                if (DateTime.Now < dOutCrewArriveTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //��δ���ж�ʱ��
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncOutCrewArriveTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncOutCrewArriveTime ��δ¼������
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncOutCrewArriveTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(2, 2) +
        //                            ":00"); //����ʵ�ʵ�λʱ��
        //                        if (dOutcncOutCrewArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
        //                        {
        //                            dOutcncOutCrewArriveTime.AddDays(-1);
        //                        }

        //                        if (dOutcncOutCrewArriveTime > dOutCrewArriveTime)
        //                        {
        //                            iAlarmResult = 2; //��
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //׼ʱ 
        //                        }
        //                    }
        //                }
        //                //�����ڴ�� _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dOutCrewArriveTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion �����鵽λ��ʱ�� �ж�

        //            #region ������м�ʱ�� �ж�
        //            strAlarmCode = "OutcncMCCReleaseTime";

        //            if (strAlarmCode == "OutcncMCCReleaseTime")
        //            {
        //                DateTime dMCCReleaseTime = new DateTime(); //����Ӧ��������е�ʱ��

        //                //ȷ������Ӧ��������е�ʱ��
        //                if ((strOverStationType == "��վ") ||
        //                    (strOverStationType == "���ٹ�վ"))
        //                {
        //                    //ȷ������Ӧ��������е�ʱ��
        //                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737����
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-21);
        //                    }
        //                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200����
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
        //                    }
        //                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300����
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }
        //                    else //����������������ϵ�Ҫ������
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }

        //                }
        //                else if (strOverStationType == "ʼ��")
        //                {
        //                    dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-45);
        //                }

        //                //�澯ֵ
        //                strAlarmValue = guaranteeInforBM.OutcncMCCReleaseTime;

        //                //�澯��� �жϣ�����Ӧ��������е�ʱ�� ��ǰ 5���� ��ʼ�жϣ�
        //                if (DateTime.Now < dMCCReleaseTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //��δ���ж�ʱ��
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncMCCReleaseTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncMCCReleaseTime ��δ¼������
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncMCCReleaseTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(2, 2) +
        //                            ":00"); //�������ʱ��
        //                        if (dOutcncMCCReleaseTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
        //                        {
        //                            dOutcncMCCReleaseTime.AddDays(-1);
        //                        }

        //                        if (dOutcncMCCReleaseTime > dMCCReleaseTime)
        //                        {
        //                            iAlarmResult = 2; //�����
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //׼ʱ���� 
        //                        }
        //                    }
        //                }

        //                //�����ڴ�� _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dMCCReleaseTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion ������м�ʱ�� �ж�  

        //            #region �ɻ�׼����ϼ�ʱ�� �ж�
        //            strAlarmCode = "OutcncOutPlaneReadyEndTime";

        //            if (strAlarmCode == "OutcncOutPlaneReadyEndTime")
        //            {
        //                DateTime dOutPlaneReadyEndTime = new DateTime(); //�ɻ�Ӧ������׼����ϵ�ʱ��

        //                //ȷ���ɻ�Ӧ������׼����ϵ�ʱ��
        //                if ((strOverStationType == "��վ") ||
        //                    (strOverStationType == "���ٹ�վ"))
        //                {
        //                    //ȷ������Ӧ��������е�ʱ��
        //                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737����
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
        //                    }
        //                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200����
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
        //                    }
        //                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300����
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }
        //                    else //����������������ϵ�Ҫ������
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }

        //                }
        //                else if (strOverStationType == "ʼ��")
        //                {
        //                    dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
        //                }

        //                //�澯ֵ
        //                strAlarmValue = guaranteeInforBM.OutcncOutPlaneReadyEndTime;

        //                //�澯��� �жϣ��ɻ�Ӧ������׼����ϵ�ʱ�� ��ǰ 5���� ��ʼ�жϣ�
        //                if (DateTime.Now < dOutPlaneReadyEndTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //��δ���ж�ʱ��
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncOutPlaneReadyEndTime ��δ¼������
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncOutPlaneReadyEndTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(2, 2) +
        //                            ":00"); //׼�����ʱ��
        //                        if (dOutcncOutPlaneReadyEndTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
        //                        {
        //                            dOutcncOutPlaneReadyEndTime.AddDays(-1);
        //                        }

        //                        if (dOutcncOutPlaneReadyEndTime > dOutPlaneReadyEndTime)
        //                        {
        //                            iAlarmResult = 2; //׼����� -- ���
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //׼����� -- ׼ʱ 
        //                        }
        //                    }
        //                }

        //                //�����ڴ�� _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dOutPlaneReadyEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion �ɻ�׼����ϼ�ʱ�� �ж�

        //            #region ֪ͨ�Ͽͼ�ʱ�� �ж�
        //            strAlarmCode = "OutcncInformBoardTime";

        //            if (strAlarmCode == "OutcncInformBoardTime")
        //            {
        //                DateTime dInformBoardTime = new DateTime(); //����֪ͨ�Ͽ͵�ʱ��

        //                //ȷ������֪ͨ�Ͽ͵�ʱ��
        //                if ((strOverStationType == "��վ") ||
        //                    (strOverStationType == "���ٹ�վ"))
        //                {
        //                    //ȷ������֪ͨ�Ͽ͵�ʱ��
        //                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737����
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
        //                    }
        //                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200����
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
        //                    }
        //                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300����
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }
        //                    else //����������������ϵ�Ҫ������
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }

        //                }
        //                else if (strOverStationType == "ʼ��")
        //                {
        //                    dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
        //                }

        //                //�澯ֵ
        //                strAlarmValue = guaranteeInforBM.OutcncInformBoardTime;

        //                //�澯��� �жϣ�����֪ͨ�Ͽ͵�ʱ�� ��ǰ 5���� ��ʼ�жϣ�
        //                if (DateTime.Now < dInformBoardTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //��δ���ж�ʱ��
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncInformBoardTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncInformBoardTime ��δ¼������
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncInformBoardTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(2, 2) +
        //                            ":00"); //֪ͨ�Ͽ�ʱ��
        //                        if (dOutcncInformBoardTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
        //                        {
        //                            dOutcncInformBoardTime.AddDays(-1);
        //                        }

        //                        if (dOutcncInformBoardTime > dInformBoardTime)
        //                        {
        //                            iAlarmResult = 2; //֪ͨ�Ͽ� -- ���
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //֪ͨ�Ͽ� -- ׼ʱ 
        //                        }
        //                    }
        //                }

        //                //�����ڴ�� _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dInformBoardTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion ֪ͨ�Ͽͼ�ʱ�� �ж�

        //            #region �Ͳչرռ�ʱ�� �ж�
        //            strAlarmCode = "OutcncClosePaxCabinTime";

        //            if (strAlarmCode == "OutcncClosePaxCabinTime")
        //            {
        //                DateTime dClosePaxCabinTime = new DateTime(); //����Ͳչر�ʱ��

        //                //ȷ������Ͳչر�ʱ��
        //                if ((strOverStationType == "��վ") ||
        //                    (strOverStationType == "���ٹ�վ"))
        //                {
        //                    //ȷ������Ͳչر�ʱ��
        //                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }
        //                else if (strOverStationType == "ʼ��")
        //                {
        //                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }

        //                //�澯ֵ
        //                strAlarmValue = guaranteeInforBM.OutcncClosePaxCabinTime;

        //                //�澯��� �жϣ�����Ͳչر�ʱ�� ��ǰ 5���� ��ʼ�жϣ�
        //                if (DateTime.Now < dClosePaxCabinTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //��δ���ж�ʱ��
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncClosePaxCabinTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncClosePaxCabinTime ��δ¼������
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncClosePaxCabinTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(2, 2) +
        //                            ":00"); //�Ͳչر�ʱ��
        //                        if (dOutcncClosePaxCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
        //                        {
        //                            dOutcncClosePaxCabinTime.AddDays(-1);
        //                        }

        //                        if (dOutcncClosePaxCabinTime > dClosePaxCabinTime)
        //                        {
        //                            iAlarmResult = 2; //�Ͳչر� -- ���
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //�Ͳչر� -- ׼ʱ 
        //                        }
        //                    }
        //                }

        //                //�����ڴ�� _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dClosePaxCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion �Ͳչرռ�ʱ�� �ж�

        //            #region ���չرռ�ʱ�� �ж�
        //            strAlarmCode = "OutcncCloseCargoCabinTime";

        //            if (strAlarmCode == "OutcncCloseCargoCabinTime")
        //            {
        //                DateTime dCloseCargoCabinTime = new DateTime(); //������չر�ʱ��

        //                //ȷ��������չر�ʱ��
        //                if ((strOverStationType == "��վ") ||
        //                    (strOverStationType == "���ٹ�վ"))
        //                {
        //                    //ȷ��������չر�ʱ��
        //                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }
        //                else if (strOverStationType == "ʼ��")
        //                {
        //                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }

        //                //�澯ֵ
        //                strAlarmValue = guaranteeInforBM.OutcncCloseCargoCabinTime;

        //                //�澯��� �жϣ�������չر�ʱ�� ��ǰ 5���� ��ʼ�жϣ�
        //                if (DateTime.Now < dCloseCargoCabinTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //��δ���ж�ʱ��
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncCloseCargoCabinTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncCloseCargoCabinTime ��δ¼������
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncCloseCargoCabinTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(2, 2) +
        //                            ":00"); //���չر�ʱ��
        //                        if (dOutcncCloseCargoCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
        //                        {
        //                            dOutcncCloseCargoCabinTime.AddDays(-1);
        //                        }

        //                        if (dOutcncCloseCargoCabinTime > dCloseCargoCabinTime)
        //                        {
        //                            iAlarmResult = 2; //���չر� -- ���
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //���չر� -- ׼ʱ 
        //                        }
        //                    }
        //                }

        //                //�����ڴ�� _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dCloseCargoCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion ���չرռ�ʱ�� �ж�

        //        }
        //        catch (Exception ex)
        //        {
        //            string strExceptionMessage = ex.Message;
        //        }
        //    }
        //    #endregion ���������ۺ����

        //    //dataGridView4.DataSource = todayInOutFlights;

        //    //�Ƿ�æ�������
        //    arrayBusy[iIndexBusy] = false; //���� ����æ ���

        //}

        #endregion ������澯��Ϣ -- �ṩ�߳�ʹ�� -- ��ʹ��

        #region ������澯��Ϣ -- �ṩ�߳�ʹ�ã�������һ���߳��д����������������� -- ��Ҫ����ͷ����������󶨴��벿��ȥ�����ܷ����߳���ʹ��
        /// <summary>
        /// ������澯��Ϣ -- �ṩ�߳�ʹ��
        /// </summary>
        /// <param name="parameterList">�����б������ۺ����ڴ����վ��׼ʱ���ڴ��վ�����û�����</param>
        private void DealFlightAlarmInfo(object parameterList)
        {
            //�����б�
            object[] objectsList = (object[])parameterList;
            DataTable todayInOutFlights = (DataTable)(objectsList[0]); //�����ۺ����ڴ��
            DataTable overStationTime = (DataTable)(objectsList[1]); //��վ��׼ʱ���ڴ��
            StationBM stationBM_Parameter = (StationBM)(objectsList[2]); //��վ����
            AccountBM accountBM = (AccountBM)(objectsList[3]); //�û�����
            bool[] arrayBusy = (bool[])(objectsList[4]); //��æ�������
            int iIndexBusy = (int)(objectsList[5]); //ʹ�õ��� ��æ������� �е�λ��
            object objTodayInOutFlights = (object)(objectsList[6]);  //�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������
            DataTable dtDataItems = (DataTable)(objectsList[7]); //�û���Ȩ�޵�������
            DataTable stationInfor = (DataTable)(objectsList[8]); //����ʱ���ڴ��


            string strThreeCodeList = ""; //�����������б��ַ�������������(�� "HAK")��������(�� , �ָ����� "HAK,PEK")��


            //�Ƿ�æ
            if (arrayBusy[iIndexBusy])
                return; //�����æ�����˳�
            arrayBusy[iIndexBusy] = true; //���� ��æ ���

            //���ٷ���
            string strTraceInfo = ""; //������Ϣ
            int iTraceInfo_1 = 1;
            string strTraceInfo_2 = "";
            string strTraceInfo_3 = "";
 
            strTraceInfo = strTraceInfo + "Start: " + DateTime.Now.ToString("mm:ss.fffffff") + " ; "; //���ٷ���

            //
            StationBM stationBM = new StationBM(); //�������ɺ�վ���󣬱��������и��Ĳ����еĺ�վ�������Ϣ
            stationBM.ThreeCode = stationBM_Parameter.ThreeCode;
            strThreeCodeList = stationBM.ThreeCode; //�����������б��ַ�������������(�� "HAK")��������(�� , �ָ����� "HAK,PEK")��
            string[] arrayThreeCodeList = strThreeCodeList.Split(','); //�����������б�����
            strTraceInfo = strTraceInfo + "Airports: " + arrayThreeCodeList.Length.ToString() + " ; "; //���ٷ���
            foreach (string strThreeCode in arrayThreeCodeList) //�����������
            {
                strTraceInfo = strThreeCode + "(S): " + DateTime.Now.ToString("mm:ss.fffffff") + " ; "; //���ٷ���
                iTraceInfo_1 = 1; //���ٷ���

                //�� �������������� ��ֵ ��վ���� ThreeCode ����
                stationBM.ThreeCode = strThreeCode; //stationBM �����ǲ����еĺ�վ���������

                //��ȡ�ú�վ��������к���
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
                DateTimeBM dateTimeBM = new DateTimeBM();
                if ((DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00")) &&
                    (DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00")))
                {
                    dateTimeBM.StartDateTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 05:00:00";
                    dateTimeBM.EndDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
                }
                else
                {
                    dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
                    dateTimeBM.EndDateTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
                } //���Կ����¸������ʱ�����䣺����ddateTimeBM.StartDateTime��dateTimeBM.EndDateTime��ֵ��ʱ�����䣬�Լ�dateTimeBM.StartDateTime��dateTimeBM.EndDateTimeӦ�÷ֱ�ֵ����

                ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
                DataTable dtTodayStationFlights = rvSF.Dt;

                //��ȡ�û���Ȩ�޵�������
                //DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
                //DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

                //�����ۺ�����Schema
                DataTable dtInOutFlightsSchema = GetDisplaySchema(dtDataItems);

                //��ȡ��������ۺ��� 
                //DataTable dtTodayInOutFlights = FillInOutFlights(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);
                IList ilTodayInOutFlights = FillInOutFlights_1(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);

                //������
                //DataView dataView  = new DataView(dtTodayInOutFlights, "", "cniRowIndex", DataViewRowState.CurrentRows);
                //dataGridView3.DataSource = dataView;
                //dataGridView3.DataSource = ilTodayInOutFlights;

                strTraceInfo = strTraceInfo + strThreeCode + "(E:: " + ilTodayInOutFlights.Count.ToString() + "): " + DateTime.Now.ToString("mm:ss.fffffff") + " ; "; //���ٷ���
                
                #region ���������ۺ����
                IEnumerator ieTodayInOutFlights = ilTodayInOutFlights.GetEnumerator();
                while (ieTodayInOutFlights.MoveNext())
                {
                    strTraceInfo_2 = "Index: " + iTraceInfo_1.ToString() + " ; "; //���ٷ���

                    string strInDEPSTN = "";
                    string strInARRSTN = "";
                    string strOutDEPSTN = "";
                    string strOutARRSTN = "";
                    string strOverStationType = ""; //��վ���ͣ�ʼ������վ�����ٹ�վ������
                    int iOverStationStandardTime = 0; //��վ��׼ʱ�䣨���ӣ�
                    int iTaxiOutMinutes = 0; //����ʱ�䣨���ӣ�
                    string strOverStationStart = ""; //��ʼʱ�̣��� ���㵽��ʱ��
                    string strOverStationEnd = ""; //����ʱ�̣��� �������ʱ��
                    string strAlarmCode = ""; //�澯����
                    string strAlarmValue = ""; //�澯ֵ
                    int iAlarmResult = 0; //�澯���


                    try
                    {
                        GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current; //ʵ������ǰ�����ۺ�����

                        #region �˹�ģ�ⲿ�ֲ���
                        //iOverStationStandardTime = 45; //ģ�� TYN �Ĺ�վʱ�䣬Ӧ�ý�� С���� ��̬����
                        //strAlarmCode = "OutcncOutPilotArriveTime";
                        #endregion �˹�ģ�ⲿ�ֲ���

                        #region ȷ�������������������
                        if (guaranteeInforBM.IncncDATOP != "1900-01-01") //���ۺ���
                        {
                            DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                                "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                                "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                                "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                            if (dataRowFlight.Length > 0)
                            {
                                strInDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                                strInARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                            }
                        }

                        if (guaranteeInforBM.OutcncDATOP != "1900-01-01") //���ۺ���
                        {
                            DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                               "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                               "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                               "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                            if (dataRowFlight.Length > 0)
                            {
                                strOutDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                                strOutARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();

                            }
                        }
                        #endregion ȷ�������������������

                        #region ȷ����վʱ��
                        DataRow[] dataRowsOverStationTime = overStationTime.Select(
                            "(cncAirportThreeCode = '" +
                            stationBM.ThreeCode + "') and (cnvcSmallACTYP = '" +
                            guaranteeInforBM.IncncACTYP + "')"); //���ݻ����������С���ͻ�ȡ��վʱ������
                        if (dataRowsOverStationTime.Length > 0)
                        {
                            //iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniGroundTime"].ToString());
                            iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniStandardTime"].ToString()); //��׼��վʱ�� //added by LinYong in 20160310
                        }
                        else
                        {
                            throw new Exception("��վʱ�����û�д˼�¼��" + Environment.NewLine +
                                "������" + stationBM.ThreeCode + Environment.NewLine +
                                "С���ͣ�" + guaranteeInforBM.IncncACTYP);
                        }
                        #endregion ȷ����վʱ��

                        #region ȷ������ʱ��
                        DataRow[] dataRowsStationInfor = stationInfor.Select(
                            "cncThreeCode = '" +
                            stationBM.ThreeCode + "'"); //���ݻ����������ȡ����ʱ������
                        if (dataRowsStationInfor.Length > 0)
                        {
                            iTaxiOutMinutes = Convert.ToInt32(dataRowsStationInfor[0]["cniTaxiOutMinutes"].ToString());
                        }
                        else
                        {
                            throw new Exception("��վ����û�д˼�¼������ʱ�䣩��" + Environment.NewLine +
                                "������" + stationBM.ThreeCode + Environment.NewLine);
                        }
                        #endregion ȷ������ʱ��

                        #region ȷ����վ����
                        if ((guaranteeInforBM.IncncDATOP == "1900-01-01") &&
                            (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                        {
                            strOverStationType = "ʼ��";
                        }
                        else if ((guaranteeInforBM.IncncDATOP != "1900-01-01") &&
                            (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                        {
                            strOverStationType = "��վ";
                        }
                        else
                        {
                            strOverStationType = "����";
                        }
                        #endregion ȷ����վ����

                        #region ȷ�� ��ʼʱ�� �� ����ʱ�̣��� ȷ�� �������ʱ�� �� ���㵽��ʱ��
                        if ((strOverStationType == "��վ") ||
                            (strOverStationType == "���ٹ�վ"))
                        {
                            //���� ��ʼʱ��
                            if (guaranteeInforBM.IncncAllStatus != "ATA")
                            {
                                strOverStationStart = guaranteeInforBM.IncncAllETA;
                            }
                            else
                            {
                                strOverStationStart = guaranteeInforBM.IncncAllATA;
                            }
                            //���� ����ʱ��
                            if (Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime) > Convert.ToDateTime(guaranteeInforBM.OutcncAllETD))
                            {
                                strOverStationEnd = Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                            }

                        }
                        else if (strOverStationType == "ʼ��")
                        {
                            //���� ��ʼʱ��
                            strOverStationStart = "";

                            //���� ����ʱ��
                            strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                        }
                        else //���󺽰಻����
                        {
                            continue;
                        }
                        #endregion ȷ�� ��ʼʱ�� �� ����ʱ�̣��� ȷ�� �������ʱ�� �� ���㵽��ʱ��

                        #region ����ʱ����Ŀ����
                        lock (objTodayInOutFlights)
                        {

                            #region �����鵽λ��ʱ�� �ж� ʹ�����ֶ� OutcncOutCrewArriveTime -- �˲��ִ��벻ʹ��
                            /*
                #region �����鵽λ��ʱ�� �ж�
                guaranteeInforBM.OutcncOutCrewArriveTime
                if (strAlarmCode == "OutcncOutPilotArriveTime")
                {
                    DateTime dOutPilotArriveTime = new DateTime(); //ȷ������Ӧ�õ�λ��ʱ��
                    if ((strOverStationType == "��վ") ||
                        (strOverStationType == "���ٹ�վ"))
                    {

                        //ȷ������Ӧ�õ�λ��ʱ��
                        if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                        {
                            dOutPilotArriveTime = Convert.ToDateTime(strOverStationStart);
                        }
                        else
                        {
                            dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                        }
                    }
                    else if (strOverStationType == "ʼ��")
                    {
                        dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                    }
                    //�澯ֵ
                    strAlarmValue = guaranteeInforBM.OutcncOutPilotArriveTime;

                    //�澯��� �жϣ�����Ӧ�õ�λ��ʱ�� ��ǰ 5���� ��ʼ�жϣ�
                    if (DateTime.Now < dOutPilotArriveTime.AddMinutes(-5)) //
                    {
                        iAlarmResult = -1; //��δ���ж�ʱ��
                    }
                    else
                    {
                        if (guaranteeInforBM.OutcncOutPilotArriveTime.Trim() == "")
                        {
                            iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncOutPilotArriveTime ��δ¼������
                        }
                        else
                        {
                            DateTime dOutcncOutPilotArriveTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                guaranteeInforBM.OutcncFlightDate +
                                guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(0, 2) +
                                ":" +
                                guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(2, 2) +
                                ":00"); //����ʵ�ʵ�λʱ��
                            if (dOutcncOutPilotArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                            {
                                dOutcncOutPilotArriveTime.AddDays(-1);
                            }

                            if (dOutcncOutPilotArriveTime > dOutPilotArriveTime)
                            {
                                iAlarmResult = 2; //��
                            }
                            else
                            {
                                iAlarmResult = 0; //׼ʱ 
                            }
                        }
                    }
                    //�����ڴ�� _dtTodayInOutFlights
                    ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
                        todayInOutFlights,
                        guaranteeInforBM,
                        strInDEPSTN,
                        strInARRSTN,
                        strOutDEPSTN,
                        strOutARRSTN,
                        strOverStationType,
                        iOverStationStandardTime.ToString(),
                        strOverStationStart,
                        strOverStationEnd,
                        strAlarmCode,
                        strAlarmValue,
                        iAlarmResult.ToString());
                }
                #endregion �����鵽λ��ʱ�� �ж�
                */
                            #endregion �����鵽λ��ʱ�� �ж� ʹ�����ֶ� OutcncOutCrewArriveTime -- �˲��ִ��벻ʹ��

                            #region �����鵽λ��ʱ�� �ж� -- �˲��ִ��벻ʹ��
                            /*
                    strAlarmCode = "OutcncOutCrewArriveTime";

                    if (strAlarmCode == "OutcncOutCrewArriveTime")
                    {
                        DateTime dOutCrewArriveTime = new DateTime(); //ȷ������Ӧ�õ�λ��ʱ��
                        if ((strOverStationType == "��վ") ||
                            (strOverStationType == "���ٹ�վ"))
                        {

                            //ȷ������Ӧ�õ�λ��ʱ��
                            if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                            {
                                dOutCrewArriveTime = Convert.ToDateTime(strOverStationStart);
                            }
                            else
                            {
                                dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                            }
                        }
                        else if (strOverStationType == "ʼ��")
                        {
                            dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                        }
                        //�澯ֵ
                        strAlarmValue = guaranteeInforBM.OutcncOutCrewArriveTime;

                        //�澯��� �жϣ�����Ӧ�õ�λ��ʱ�� ��ǰ 5���� ��ʼ�жϣ�
                        if (DateTime.Now < dOutCrewArriveTime.AddMinutes(-5)) //
                        {
                            iAlarmResult = -1; //��δ���ж�ʱ��
                        }
                        else
                        {
                            if (guaranteeInforBM.OutcncOutCrewArriveTime.Trim() == "")
                            {
                                iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncOutCrewArriveTime ��δ¼������
                            }
                            else
                            {
                                DateTime dOutcncOutCrewArriveTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                    guaranteeInforBM.OutcncFlightDate +
                                    " " +
                                    guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(0, 2) +
                                    ":" +
                                    guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(2, 2) +
                                    ":00"); //����ʵ�ʵ�λʱ��
                                if (dOutcncOutCrewArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                {
                                    dOutcncOutCrewArriveTime.AddDays(-1);
                                }

                                if (dOutcncOutCrewArriveTime > dOutCrewArriveTime)
                                {
                                    iAlarmResult = 2; //��
                                }
                                else
                                {
                                    iAlarmResult = 0; //׼ʱ 
                                }
                            }
                        }
                        //�����ڴ�� _dtTodayInOutFlights
                        ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
                            todayInOutFlights,
                            guaranteeInforBM,
                            strInDEPSTN,
                            strInARRSTN,
                            strOutDEPSTN,
                            strOutARRSTN,
                            strOverStationType,
                            iOverStationStandardTime.ToString(),
                            strOverStationStart,
                            strOverStationEnd,
                            strAlarmCode,
                            strAlarmValue,
                            dOutCrewArriveTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            iAlarmResult.ToString(),
                            objTodayInOutFlights);
                    }
                    */
                            #endregion �����鵽λ��ʱ�� �ж� -- �˲��ִ��벻ʹ��


                            strTraceInfo_3 = strTraceInfo_2 + "K: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region �����鵽λ��ʱ�� �ж�
                            strAlarmCode = "OutcncOutCrewArriveTime";

                            if (strAlarmCode == "OutcncOutCrewArriveTime")
                            {
                                DateTime dOutCrewArriveTime = new DateTime(); //ȷ������Ӧ�õ�λ��ʱ��
                                if ((strOverStationType == "��վ") ||
                                    (strOverStationType == "���ٹ�վ"))
                                {

                                    //ȷ������Ӧ�õ�λ��ʱ��
                                    if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                                    {
                                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationStart);
                                    }
                                    else
                                    {
                                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                                    }
                                }
                                else if (strOverStationType == "ʼ��")
                                {
                                    dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                                }

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncOutCrewArriveTime;

                                //�澯��� �ж�
                                if (guaranteeInforBM.OutcncOutCrewArriveTime.Trim() == "") //��δ¼�뱣������
                                {
                                    if (DateTime.Now < dOutCrewArriveTime) //
                                    {
                                        iAlarmResult = 4; //��δ�� ����Ӧ�õ�λ��ʱ��
                                    }
                                    else
                                    {
                                        iAlarmResult = 3; //�ѵ� ����Ӧ�õ�λ��ʱ��
                                    }
                                }
                                else //�Ѿ�¼�뱣������
                                {
                                    DateTime dOutcncOutCrewArriveTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(2, 2) +
                                        ":00"); //����ʵ�ʵ�λʱ��
                                    if (dOutcncOutCrewArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                    {
                                        dOutcncOutCrewArriveTime.AddDays(-1);
                                    }

                                    if (dOutcncOutCrewArriveTime > dOutCrewArriveTime)
                                    {
                                        iAlarmResult = 2; //��
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutCrewArriveTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion �����鵽λ��ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "J: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region ������м�ʱ�� �ж�
                            strAlarmCode = "OutcncMCCReleaseTime";

                            if (strAlarmCode == "OutcncMCCReleaseTime")
                            {
                                DateTime dMCCReleaseTime = new DateTime(); //����Ӧ��������е�ʱ��

                                //ȷ������Ӧ��������е�ʱ��
                                if ((strOverStationType == "��վ") ||
                                    (strOverStationType == "���ٹ�վ"))
                                {
                                    //ȷ������Ӧ��������е�ʱ��
                                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737����
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-21);
                                    }
                                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200����
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
                                    }
                                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300����
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }
                                    else //����������������ϵ�Ҫ������
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }

                                }
                                else if (strOverStationType == "ʼ��")
                                {
                                    dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-45);
                                }

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncMCCReleaseTime;

                                //�澯��� �ж�
                                if (guaranteeInforBM.OutcncMCCReleaseTime.Trim() == "") //��Ӧ������ OutcncMCCReleaseTime ��δ¼������
                                {
                                    if (DateTime.Now < dMCCReleaseTime) //��δ�� ����Ӧ��������е�ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ����Ӧ��������е�ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //��Ӧ������ OutcncMCCReleaseTime ��¼������
                                {
                                    DateTime dOutcncMCCReleaseTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(2, 2) +
                                        ":00"); //�������ʱ��
                                    if (dOutcncMCCReleaseTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                    {
                                        dOutcncMCCReleaseTime.AddDays(-1);
                                    }

                                    if (dOutcncMCCReleaseTime > dMCCReleaseTime)
                                    {
                                        iAlarmResult = 2; //�����
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //׼ʱ���� 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dMCCReleaseTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion ������м�ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "F: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region �ɻ�׼����ϼ�ʱ�� �ж�
                            strAlarmCode = "OutcncOutPlaneReadyEndTime";

                            if (strAlarmCode == "OutcncOutPlaneReadyEndTime")
                            {
                                DateTime dOutPlaneReadyEndTime = new DateTime(); //�ɻ�Ӧ������׼����ϵ�ʱ��

                                //ȷ���ɻ�Ӧ������׼����ϵ�ʱ��
                                if ((strOverStationType == "��վ") ||
                                    (strOverStationType == "���ٹ�վ"))
                                {
                                    //ȷ���ɻ�Ӧ������׼����ϵ�ʱ��
                                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737����
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
                                    }
                                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200����
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
                                    }
                                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300����
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }
                                    else //����������������ϵ�Ҫ������
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }

                                }
                                else if (strOverStationType == "ʼ��")
                                {
                                    dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
                                }

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncOutPlaneReadyEndTime;

                                //�澯��� �ж�
                                if (guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim() == "") //��Ӧ������ OutcncOutPlaneReadyEndTime ��δ¼������
                                {
                                    if (DateTime.Now < dOutPlaneReadyEndTime) //��δ�� �ɻ�Ӧ������׼����ϵ�ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� �ɻ�Ӧ������׼����ϵ�ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //��Ӧ������ OutcncOutPlaneReadyEndTime ��¼������
                                {
                                    DateTime dOutcncOutPlaneReadyEndTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(2, 2) +
                                        ":00"); //׼�����ʱ��
                                    if (dOutcncOutPlaneReadyEndTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                    {
                                        dOutcncOutPlaneReadyEndTime.AddDays(-1);
                                    }

                                    if (dOutcncOutPlaneReadyEndTime > dOutPlaneReadyEndTime)
                                    {
                                        iAlarmResult = 2; //׼����� -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //׼����� -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutPlaneReadyEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion �ɻ�׼����ϼ�ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "T: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region ֪ͨ�Ͽͼ�ʱ�� �ж�
                            strAlarmCode = "OutcncInformBoardTime";

                            if (strAlarmCode == "OutcncInformBoardTime")
                            {
                                DateTime dInformBoardTime = new DateTime(); //����֪ͨ�Ͽ͵�ʱ��

                                //ȷ������֪ͨ�Ͽ͵�ʱ��
                                if ((strOverStationType == "��վ") ||
                                    (strOverStationType == "���ٹ�վ"))
                                {
                                    //ȷ������֪ͨ�Ͽ͵�ʱ��
                                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737����
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
                                    }
                                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200����
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
                                    }
                                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300����
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }
                                    else //����������������ϵ�Ҫ������
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }

                                }
                                else if (strOverStationType == "ʼ��")
                                {
                                    dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
                                }

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncInformBoardTime;

                                //�澯��� �ж�
                                if (guaranteeInforBM.OutcncInformBoardTime.Trim() == "") //��Ӧ������ OutcncInformBoardTime ��δ¼������
                                {
                                    if (DateTime.Now < dInformBoardTime) //��δ�� ����֪ͨ�Ͽ͵�ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ����֪ͨ�Ͽ͵�ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //��Ӧ������ OutcncInformBoardTime ��¼������
                                {
                                    DateTime dOutcncInformBoardTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(2, 2) +
                                        ":00"); //֪ͨ�Ͽ�ʱ��
                                    if (dOutcncInformBoardTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                    {
                                        dOutcncInformBoardTime.AddDays(-1);
                                    }

                                    if (dOutcncInformBoardTime > dInformBoardTime)
                                    {
                                        iAlarmResult = 2; //֪ͨ�Ͽ� -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //֪ͨ�Ͽ� -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dInformBoardTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion ֪ͨ�Ͽͼ�ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "KC: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region �Ͳչرռ�ʱ�� �ж�
                            strAlarmCode = "OutcncClosePaxCabinTime";

                            if (strAlarmCode == "OutcncClosePaxCabinTime")
                            {
                                DateTime dClosePaxCabinTime = new DateTime(); //����Ͳչر�ʱ��

                                //ȷ������Ͳչر�ʱ��
                                if ((strOverStationType == "��վ") ||
                                    (strOverStationType == "���ٹ�վ"))
                                {
                                    //ȷ������Ͳչر�ʱ��
                                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }
                                else if (strOverStationType == "ʼ��")
                                {
                                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncClosePaxCabinTime;

                                //�澯��� �ж�
                                if (guaranteeInforBM.OutcncClosePaxCabinTime.Trim() == "") //��Ӧ������ OutcncClosePaxCabinTime ��δ¼������
                                {
                                    if (DateTime.Now < dClosePaxCabinTime) //��δ�� ����Ͳչر�ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ����Ͳչر�ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //��Ӧ������ OutcncClosePaxCabinTime ��¼������
                                {
                                    DateTime dOutcncClosePaxCabinTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(2, 2) +
                                        ":00"); //�Ͳչر�ʱ��
                                    if (dOutcncClosePaxCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                    {
                                        dOutcncClosePaxCabinTime.AddDays(-1);
                                    }

                                    if (dOutcncClosePaxCabinTime > dClosePaxCabinTime)
                                    {
                                        iAlarmResult = 2; //�Ͳչر� -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //�Ͳչر� -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dClosePaxCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion �Ͳչرռ�ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "HC: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }


                            #region ���չرռ�ʱ�� �ж�
                            strAlarmCode = "OutcncCloseCargoCabinTime";

                            if (strAlarmCode == "OutcncCloseCargoCabinTime")
                            {
                                DateTime dCloseCargoCabinTime = new DateTime(); //������չر�ʱ��

                                //ȷ��������չر�ʱ��
                                if ((strOverStationType == "��վ") ||
                                    (strOverStationType == "���ٹ�վ"))
                                {
                                    //ȷ��������չر�ʱ��
                                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }
                                else if (strOverStationType == "ʼ��")
                                {
                                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncCloseCargoCabinTime;

                                //�澯��� �ж�
                                if (guaranteeInforBM.OutcncCloseCargoCabinTime.Trim() == "") //��Ӧ������ OutcncCloseCargoCabinTime ��δ¼������
                                {
                                    if (DateTime.Now < dCloseCargoCabinTime) //��δ�� ������չر�ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ������չر�ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //��Ӧ������ OutcncCloseCargoCabinTime ��¼������
                                {
                                    DateTime dOutcncCloseCargoCabinTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(2, 2) +
                                        ":00"); //���չر�ʱ��
                                    if (dOutcncCloseCargoCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                    {
                                        dOutcncCloseCargoCabinTime.AddDays(-1);
                                    }

                                    if (dOutcncCloseCargoCabinTime > dCloseCargoCabinTime)
                                    {
                                        iAlarmResult = 2; //���չر� -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //���չر� -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dCloseCargoCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion ���չرռ�ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "TC: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region �ɻ��Ƴ���ʱ�� �ж�
                            strAlarmCode = "OutcncAllATD";

                            if (strAlarmCode == "OutcncAllATD")
                            {
                                DateTime dOutcncAllSTD_Offset = new DateTime(); //����ɻ��Ƴ�ʱ��

                                //����ɻ��Ƴ�ʱ��
                                dOutcncAllSTD_Offset = Convert.ToDateTime(guaranteeInforBM.OutcncAllSTD).AddMinutes(5);

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncAllATD;

                                //�澯��� �ж�
                                if ((guaranteeInforBM.OutcncAllStatus == "SCH") ||
                                    (guaranteeInforBM.OutcncAllStatus == "DEL")) //����״̬��ʾ��δ�Ƴ�
                                {
                                    if (DateTime.Now < dOutcncAllSTD_Offset) //��δ�� ����ɻ��Ƴ�ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ����ɻ��Ƴ�ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //����״̬��ʾ���Ƴ�
                                {
                                    DateTime dOutcncAllATD = Convert.ToDateTime(guaranteeInforBM.OutcncAllATD); //ʵ�ʷɻ��Ƴ�ʱ��

                                    if (dOutcncAllATD > dOutcncAllSTD_Offset)
                                    {
                                        iAlarmResult = 2; //�ɻ��Ƴ� -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //�ɻ��Ƴ� -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutcncAllSTD_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion �ɻ��Ƴ���ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "QF: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region �ɻ���ɼ�ʱ�� �ж�
                            strAlarmCode = "OutcncAllTOFF";

                            if (strAlarmCode == "OutcncAllTOFF")
                            {
                                DateTime dOutcncAllTOFF_Offset = new DateTime(); //����ɻ���ɣ���أ�ʱ��

                                //����ɻ���ɣ���أ�ʱ��
                                dOutcncAllTOFF_Offset = Convert.ToDateTime(guaranteeInforBM.OutcncAllSTD).AddMinutes(iTaxiOutMinutes);

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.OutcncAllTOFF;

                                //�澯��� �ж�
                                if ((guaranteeInforBM.OutcncAllStatus == "SCH") ||
                                    (guaranteeInforBM.OutcncAllStatus == "DEL") ||
                                    (guaranteeInforBM.OutcncAllStatus == "ATD")) //����״̬��ʾ��δ��ɣ���أ�
                                {
                                    if (DateTime.Now < dOutcncAllTOFF_Offset) //��δ�� ����ɻ���ɣ���أ�ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ����ɻ���ɣ���أ�ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //����״̬��ʾ����ɣ���أ�
                                {
                                    DateTime dOutcncAllTOFF = Convert.ToDateTime(guaranteeInforBM.OutcncAllTOFF); //ʵ�ʷɻ���ɣ���أ�ʱ��

                                    if (dOutcncAllTOFF > dOutcncAllTOFF_Offset)
                                    {
                                        iAlarmResult = 2; //�ɻ���ɣ���أ� -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //�ɻ���ɣ���أ� -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutcncAllTOFF_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion �ɻ���ɼ�ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "LD: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region ���ۺ�����ؼ�ʱ�� �ж�
                            strAlarmCode = "IncncAllTDWN";

                            if (strAlarmCode == "IncncAllTDWN")
                            {
                                DateTime dIncncAllTDWN_Offset = new DateTime(); //����ɻ����ʱ��

                                //����ɻ����ʱ��
                                dIncncAllTDWN_Offset = Convert.ToDateTime(guaranteeInforBM.IncncAllSTA).AddMinutes(20);

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.IncncAllTDWN;

                                //�澯��� �ж�
                                if ((guaranteeInforBM.IncncAllStatus == "SCH") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEL") ||
                                    (guaranteeInforBM.IncncAllStatus == "ATD") ||
                                    (guaranteeInforBM.IncncAllStatus == "RTR") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEP")) //����״̬��ʾ��δ���
                                {
                                    if (DateTime.Now < dIncncAllTDWN_Offset) //��δ�� ����ɻ����ʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ����ɻ����ʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //����״̬��ʾ�����
                                {
                                    DateTime dIncncAllTDWN = Convert.ToDateTime(guaranteeInforBM.IncncAllTDWN); //ʵ�ʷɻ����ʱ��

                                    if (dIncncAllTDWN > dIncncAllTDWN_Offset)
                                    {
                                        iAlarmResult = 2; //�ɻ���� -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //�ɻ���� -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dIncncAllTDWN_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion ���ۺ�����ؼ�ʱ�� �ж�

                            strTraceInfo_3 = strTraceInfo_2 + "DW: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //���ٷ���
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "�ַ������ޣ�";
                            }

                            #region ���ۺ���ɻ���λ��ʱ�� �ж�
                            strAlarmCode = "IncncAllATA";

                            if (strAlarmCode == "IncncAllATA")
                            {
                                DateTime dIncncAllATA_Offset = new DateTime(); //����ɻ���λʱ��

                                //����ɻ���λʱ��
                                dIncncAllATA_Offset = Convert.ToDateTime(guaranteeInforBM.IncncAllSTA);

                                //�澯ֵ
                                strAlarmValue = guaranteeInforBM.IncncAllATA;

                                //�澯��� �ж�
                                if ((guaranteeInforBM.IncncAllStatus == "SCH") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEL") ||
                                    (guaranteeInforBM.IncncAllStatus == "ATD") ||
                                    (guaranteeInforBM.IncncAllStatus == "RTR") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEP") ||
                                    (guaranteeInforBM.IncncAllStatus == "ARR")) //����״̬��ʾ��δ��λ
                                {
                                    if (DateTime.Now < dIncncAllATA_Offset) //��δ�� ����ɻ���λʱ��
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //�ѵ� ����ɻ���λʱ��
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //����״̬��ʾ�ѵ�λ
                                {
                                    DateTime dIncncAllATA = Convert.ToDateTime(guaranteeInforBM.IncncAllATA); //ʵ�ʷɻ���λʱ��

                                    if (dIncncAllATA > dIncncAllATA_Offset)
                                    {
                                        iAlarmResult = 2; //�ɻ���λ -- ���
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //�ɻ���λ -- ׼ʱ 
                                    }
                                }

                                //�����ڴ�� _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dIncncAllATA_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion ���ۺ���ɻ���λ��ʱ�� �ж�

                        }
                        #endregion ����ʱ����Ŀ����




                    }
                    catch (Exception ex)
                    {
                        string strExceptionMessage = ex.Message;
                    }

                    iTraceInfo_1 = iTraceInfo_1 + 1; //���ٷ���
                }
                #endregion ���������ۺ����

                //dataGridView4.DataSource = todayInOutFlights;
            }

            //�Ƿ�æ�������
            arrayBusy[iIndexBusy] = false; //���� ����æ ���
        }
        #endregion ������澯��Ϣ -- �ṩ�߳�ʹ�ã�������һ���߳��д����������������� -- ��Ҫ����ͷ����������󶨴��벿��ȥ�����ܷ����߳���ʹ��

        #region �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼
        /// <summary>
        /// �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼
        /// </summary>
        /// <param name="todayInOutFlights">�����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) </param>
        /// <param name="guaranteeInforBM">�����ۺ���������ʵ��</param>
        /// <param name="inDEPSTN">������ɻ���</param>
        /// <param name="inARRSTN">���۵������</param>
        /// <param name="outDEPSTN">������ɻ���</param>
        /// <param name="outARRSTN">���۵������</param>
        /// <param name="overStationType">��վ���ͣ�ʼ������վ�����ٹ�վ������</param>
        /// <param name="overStationStandardTime">��վ��׼ʱ��</param>
        /// <param name="overStationStart">��վ��ʼʱ��</param>
        /// <param name="overStationEnd">��վ����ʱ��</param>
        /// <param name="alarmCode">�澯����</param>
        /// <param name="alarmValue">�澯ֵ</param>
        /// <param name="alarmResult">�澯���</param>
        /// <param name="objTodayInOutFlights">�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������</param>
        /// <returns>ReturnValueSF����Result��1���ɹ���-1 ���ɹ���Message���������</returns>
        private ReturnValueSF DealTodayInOutFlights_Bak20151109(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            returnValueSF.Result = -1;

            #region ���ڴ����ȡ�˽����ۺ�������¼
            DataRow[] dataRowsTodayInOutFlights = null;
            lock (objTodayInOutFlights)
            {
                dataRowsTodayInOutFlights = todayInOutFlights.Select("cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //��ȡ����
            }
            #endregion ���ڴ����ȡ�˽����ۺ�������¼

            #region ����
            if (dataRowsTodayInOutFlights.Length == 1) //����
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString())
                    ) //����������б䶯�����£�cndOperationTime���Ϊ����ʱ�䣩
                {
                    lock (objTodayInOutFlights)
                    {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;
                    }

                    returnValueSF.Result = 1;
                    returnValueSF.Message = "�����ڴ��ɹ���";
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "��������仯��û�и��£�";
                }
            }
            #endregion ����
            #region ������
            else if (dataRowsTodayInOutFlights.Length == 0) //�����ڣ�����
            {
                lock (objTodayInOutFlights)
                {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;
                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = "";
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);
                }

                returnValueSF.Result = 1;
                returnValueSF.Message = "���ڴ�����Ӽ�¼�ɹ���";
            }
            #endregion ������
            #region ���ڶ�����¼
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "�ڴ����ڶ����ؼ��ֲ�ѯ��¼��";
            }
            #endregion ���ڶ�����¼

            //
            return returnValueSF;
        }
        #endregion �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼

        #region �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼
        /// <summary>
        /// �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼
        /// </summary>
        /// <param name="todayInOutFlights">�����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) </param>
        /// <param name="guaranteeInforBM">�����ۺ���������ʵ��</param>
        /// <param name="inDEPSTN">������ɻ���</param>
        /// <param name="inARRSTN">���۵������</param>
        /// <param name="outDEPSTN">������ɻ���</param>
        /// <param name="outARRSTN">���۵������</param>
        /// <param name="overStationType">��վ���ͣ�ʼ������վ�����ٹ�վ������</param>
        /// <param name="overStationStandardTime">��վ��׼ʱ��</param>
        /// <param name="overStationStart">��վ��ʼʱ��</param>
        /// <param name="overStationEnd">��վ����ʱ��</param>
        /// <param name="alarmCode">�澯����</param>
        /// <param name="alarmValue">�澯ֵ</param>
        /// <param name="alarmResult">�澯���</param>
        /// <param name="objTodayInOutFlights">�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������</param>
        /// <returns>ReturnValueSF����Result��1���ɹ���-1 ���ɹ���Message���������</returns>
        private ReturnValueSF DealTodayInOutFlights_Bak20151201(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            returnValueSF.Result = -1;

            #region ���ڴ����ȡ�˽����ۺ�������¼
            DataRow[] dataRowsTodayInOutFlights = null;
            DataRow[] dataRowsTodayInOutFlights_Not = null;
            lock (objTodayInOutFlights)
            {
                dataRowsTodayInOutFlights = todayInOutFlights.Select(
                    "cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //��ȡ����

                dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                    "cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //��ȡ����
            }
            #endregion ���ڴ����ȡ�˽����ۺ�������¼

            #region ����
            if (dataRowsTodayInOutFlights.Length == 1) //����
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString()) ||
                    (guaranteeInforBM.IncncAllSTA != dataRowsTodayInOutFlights[0]["cncInSTA"].ToString()) ||
                    (guaranteeInforBM.IncncAllETA != dataRowsTodayInOutFlights[0]["cncInETA"].ToString()) ||
                    (guaranteeInforBM.IncncAllStatus != dataRowsTodayInOutFlights[0]["cncInSTATUS"].ToString()) ||
                    (guaranteeInforBM.OutcncAllSTD != dataRowsTodayInOutFlights[0]["cncOutSTD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllETD != dataRowsTodayInOutFlights[0]["cncOutETD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllStatus != dataRowsTodayInOutFlights[0]["cncOutSTATUS"].ToString())
                    ) //����������б䶯�����£�cndOperationTime���Ϊ����ʱ�䣩
                {
                    lock (objTodayInOutFlights)
                    {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                        dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                        dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                        dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                        dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                        dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                        dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;
                    }

                    returnValueSF.Result = 1;
                    returnValueSF.Message = "�����ڴ��ɹ���";
                }
                else if (dataRowsTodayInOutFlights_Not.Length >= 1)
                {
                    DateTime dateTimeTodayInOutFlights = Convert.ToDateTime( dataRowsTodayInOutFlights[0]["cndOperationTime"].ToString());
                    DateTime dateTimeTodayInOutFlights_Not = Convert.ToDateTime(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"].ToString());

                    if (dateTimeTodayInOutFlights_Not >= dateTimeTodayInOutFlights)
                    {
                        lock (objTodayInOutFlights)
                        {
                            dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                            dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                            dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                            dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                            dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                            dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                            dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                            dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                            dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                            dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                            dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                            dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;
                        }

                        returnValueSF.Result = 1;
                        returnValueSF.Message = "�����ڴ��ɹ���";
                    }
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "��������仯��û�и��£�";
                }
            }
            #endregion ����
            #region ������
            else if (dataRowsTodayInOutFlights.Length == 0) //�����ڣ�����
            {
                lock (objTodayInOutFlights)
                {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;
                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = "";
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);
                }

                returnValueSF.Result = 1;
                returnValueSF.Message = "���ڴ�����Ӽ�¼�ɹ���";
            }
            #endregion ������
            #region ���ڶ�����¼
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "�ڴ����ڶ����ؼ��ֲ�ѯ��¼��";
            }
            #endregion ���ڶ�����¼

            //
            return returnValueSF;
        }
        #endregion �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼

        #region �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼
        /// <summary>
        /// �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼
        /// </summary>
        /// <param name="todayInOutFlights">�����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) </param>
        /// <param name="guaranteeInforBM">�����ۺ���������ʵ��</param>
        /// <param name="inDEPSTN">������ɻ���</param>
        /// <param name="inARRSTN">���۵������</param>
        /// <param name="outDEPSTN">������ɻ���</param>
        /// <param name="outARRSTN">���۵������</param>
        /// <param name="taxiOutMinutes">����ʱ��</param>       
        /// <param name="overStationType">��վ���ͣ�ʼ������վ�����ٹ�վ������</param>
        /// <param name="overStationStandardTime">��վ��׼ʱ��</param>
        /// <param name="overStationStart">��վ��ʼʱ��</param>
        /// <param name="overStationEnd">��վ����ʱ��</param>
        /// <param name="alarmCode">�澯����</param>
        /// <param name="alarmValue">�澯ֵ</param>
        /// <param name="alarmResult">�澯���</param>
        /// <param name="objTodayInOutFlights">�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������</param>
        /// <returns>ReturnValueSF����Result��1���ɹ���-1 ���ɹ���Message���������</returns>
        private ReturnValueSF DealTodayInOutFlights_Bak20160603(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string taxiOutMinutes,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            returnValueSF.Result = -1;

            #region ���ڴ����ȡ�˽����ۺ�������¼
            DataRow[] dataRowsTodayInOutFlights = null;
            DataRow[] dataRowsTodayInOutFlights_Not = null;
            lock (objTodayInOutFlights)
            {
                dataRowsTodayInOutFlights = todayInOutFlights.Select(
                    "cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //��ȡ����

                dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                    "cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //��ȡ����
            }
            #endregion ���ڴ����ȡ�˽����ۺ�������¼

            #region ����
            if (dataRowsTodayInOutFlights.Length == 1) //����
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString()) ||
                    (guaranteeInforBM.IncncAllSTA != dataRowsTodayInOutFlights[0]["cncInSTA"].ToString()) ||
                    (guaranteeInforBM.IncncAllETA != dataRowsTodayInOutFlights[0]["cncInETA"].ToString()) ||
                    (guaranteeInforBM.IncncAllATA != dataRowsTodayInOutFlights[0]["cncInATA"].ToString()) || //added by LinYong in 20160310
                    (guaranteeInforBM.IncncAllStatus != dataRowsTodayInOutFlights[0]["cncInSTATUS"].ToString()) ||
                    (guaranteeInforBM.OutcncAllSTD != dataRowsTodayInOutFlights[0]["cncOutSTD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllETD != dataRowsTodayInOutFlights[0]["cncOutETD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllStatus != dataRowsTodayInOutFlights[0]["cncOutSTATUS"].ToString()) ||
                    (taxiOutMinutes != dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"].ToString())
                    ) //����������б䶯�����£�cndOperationTime���Ϊ����ʱ�䣩
                {
                    lock (objTodayInOutFlights)
                    {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                        dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                        dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                        dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                        dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                        dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                        dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                        dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                        dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                        dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + DateTime.Now.ToString("mm:ss.fffffff"); //���ٷ���

                    }

                    returnValueSF.Result = 1;
                    returnValueSF.Message = "�����ڴ��ɹ���";
                }
                else if (dataRowsTodayInOutFlights_Not.Length >= 1)
                {
                    DateTime dateTimeTodayInOutFlights = Convert.ToDateTime(dataRowsTodayInOutFlights[0]["cndOperationTime"].ToString());
                    DateTime dateTimeTodayInOutFlights_Not = Convert.ToDateTime(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"].ToString());

                    if (dateTimeTodayInOutFlights_Not >= dateTimeTodayInOutFlights)
                    {
                        lock (objTodayInOutFlights)
                        {
                            dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                            dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                            dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                            dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                            dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                            dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                            dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                            dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                            dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                            dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                            dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                            dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                            dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                            dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                            dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + DateTime.Now.ToString("mm:ss.fffffff"); //���ٷ���

                        }

                        returnValueSF.Result = 1;
                        returnValueSF.Message = "�����ڴ��ɹ���";
                    }
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "��������仯��û�и��£�";
                }
            }
            #endregion ����
            #region ������
            else if (dataRowsTodayInOutFlights.Length == 0) //�����ڣ�����
            {
                lock (objTodayInOutFlights)
                {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                    dataRowTodayInOutFlights["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + DateTime.Now.ToString("mm:ss.fffffff"); //���ٷ���
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);
                }

                returnValueSF.Result = 1;
                returnValueSF.Message = "���ڴ�����Ӽ�¼�ɹ���";
            }
            #endregion ������
            #region ���ڶ�����¼
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "�ڴ����ڶ����ؼ��ֲ�ѯ��¼��";
            }
            #endregion ���ڶ�����¼

            //
            return returnValueSF;
        }
        #endregion �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼

        #region �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼������û�ж�_ dtTodayInOutFlights ����ͬ��
        /// <summary>
        /// �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼������û�ж�_ dtTodayInOutFlights ����ͬ��
        /// </summary>
        /// <param name="todayInOutFlights">�����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) </param>
        /// <param name="guaranteeInforBM">�����ۺ���������ʵ��</param>
        /// <param name="inDEPSTN">������ɻ���</param>
        /// <param name="inARRSTN">���۵������</param>
        /// <param name="outDEPSTN">������ɻ���</param>
        /// <param name="outARRSTN">���۵������</param>
        /// <param name="taxiOutMinutes">����ʱ��</param>        
        /// <param name="overStationType">��վ���ͣ�ʼ������վ�����ٹ�վ������</param>
        /// <param name="overStationStandardTime">��վ��׼ʱ��</param>
        /// <param name="overStationStart">��վ��ʼʱ��</param>
        /// <param name="overStationEnd">��վ����ʱ��</param>
        /// <param name="alarmCode">�澯����</param>
        /// <param name="alarmValue">�澯ֵ</param>
        /// <param name="alarmResult">�澯���</param>
        /// <param name="objTodayInOutFlights">�� �����ۺ����ڴ��(_dtTodayInOutFlights ���ݱ�) �Ĳ���ͬ������</param>
        /// <returns>ReturnValueSF����Result��1���ɹ���-1 ���ɹ���Message���������</returns>
        private ReturnValueSF DealTodayInOutFlights_NotLock(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string taxiOutMinutes,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();
            string strTraceInfo = ""; //������Ϣ


            returnValueSF.Result = -1;

            #region ���ڴ����ȡ�˽����ۺ�������¼
            DataRow[] dataRowsTodayInOutFlights = null;
            DataRow[] dataRowsTodayInOutFlights_Not = null;
                //dataRowsTodayInOutFlights = todayInOutFlights.Select(
                //    "cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                //    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                //    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                //    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                //    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                //    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                //    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                //    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                //    "' and cnvcAlarmCode = '" + alarmCode + "'"); //��ȡ����
                dataRowsTodayInOutFlights = todayInOutFlights.Select(
                    "cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //��ȡ����

                strTraceInfo = strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //������Ϣ

                //dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                //    "cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                //    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                //    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                //    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                //    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                //    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                //    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                //    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                //    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //��ȡ����
                dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                    "cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //��ȡ����

                strTraceInfo = strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //������Ϣ
            #endregion ���ڴ����ȡ�˽����ۺ�������¼

            #region ����
            if (dataRowsTodayInOutFlights.Length == 1) //����
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString()) ||
                    (guaranteeInforBM.IncncAllSTA != dataRowsTodayInOutFlights[0]["cncInSTA"].ToString()) ||
                    (guaranteeInforBM.IncncAllETA != dataRowsTodayInOutFlights[0]["cncInETA"].ToString()) ||
                    (guaranteeInforBM.IncncAllATA != dataRowsTodayInOutFlights[0]["cncInATA"].ToString()) || //added by LinYong in 20160310
                    (guaranteeInforBM.IncncAllStatus != dataRowsTodayInOutFlights[0]["cncInSTATUS"].ToString()) ||
                    (guaranteeInforBM.OutcncAllSTD != dataRowsTodayInOutFlights[0]["cncOutSTD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllETD != dataRowsTodayInOutFlights[0]["cncOutETD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllStatus != dataRowsTodayInOutFlights[0]["cncOutSTATUS"].ToString()) ||
                    (taxiOutMinutes != dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"].ToString())
                    ) //����������б䶯�����£�cndOperationTime���Ϊ����ʱ�䣩
                {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                        dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                        dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                        dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                        dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                        dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                        dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                        dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                        dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                        dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff"); //���ٷ���


                    returnValueSF.Result = 1;
                    returnValueSF.Message = "�����ڴ��ɹ���";
                }
                else if (dataRowsTodayInOutFlights_Not.Length >= 1)
                {
                    //DateTime dateTimeTodayInOutFlights = Convert.ToDateTime(dataRowsTodayInOutFlights[0]["cndOperationTime"].ToString());
                    //DateTime dateTimeTodayInOutFlights_Not = Convert.ToDateTime(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"].ToString());
                    DateTime dateTimeTodayInOutFlights = ((DateTime)(dataRowsTodayInOutFlights[0]["cndOperationTime"]));
                    DateTime dateTimeTodayInOutFlights_Not = ((DateTime)(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"]));

                    if (dateTimeTodayInOutFlights_Not >= dateTimeTodayInOutFlights)
                    {
                            dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                            dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                            dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                            dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                            dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                            dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                            dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                            dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                            dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                            dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                            dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                            dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                            dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                            dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                            dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff"); //���ٷ���


                        returnValueSF.Result = 1;
                        returnValueSF.Message = "�����ڴ��ɹ���";
                    }
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "��������仯��û�и��£�";
                }
            }
            #endregion ����
            #region ������
            else if (dataRowsTodayInOutFlights.Length == 0) //�����ڣ�����
            {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                    dataRowTodayInOutFlights["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff"); //���ٷ���
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);

                returnValueSF.Result = 1;
                returnValueSF.Message = "���ڴ�����Ӽ�¼�ɹ���";
            }
            #endregion ������
            #region ���ڶ�����¼
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "�ڴ����ڶ����ؼ��ֲ�ѯ��¼��";
            }
            #endregion ���ڶ�����¼

            //
            return returnValueSF;
        }
        #endregion �����ڴ�澯�������ۺ����б����޸Ļ����Ӽ�¼������û�ж�_ dtTodayInOutFlights ����ͬ��




        #region ��ȡҪ��ʾ��ļܹ�
        /// <summary>
        /// ��ȡҪ��ʾ��ļܹ�
        /// </summary>
        /// <param name="dataItems">�������б�</param>
        /// <returns></returns>
        private DataTable GetDisplaySchema(DataTable dataItems)
        {
            DataTable dtInOutFlights = new DataTable();

            //���������������ֶ�
            dtInOutFlights.Columns.Add("IncncDATOP");                   //���ۺ�������
            dtInOutFlights.Columns.Add("IncnvcFLTID");                  //���ۺ��ຽ���
            dtInOutFlights.Columns.Add("IncniLEGNO");                   //���ۺ��ຽ����Ϣ
            dtInOutFlights.Columns.Add("IncnvcAC");                     //���ۺ���ɻ���
            dtInOutFlights.Columns.Add("IncncAllSTA");                  //���ۺ���ƻ�����ʱ�䣨������ʽ��
            dtInOutFlights.Columns.Add("IncncAllETA");                  //���ۺ���Ԥ�Ƶ���ʱ��
            dtInOutFlights.Columns.Add("IncncAllTDWN");                 //���ۺ������ʱ��
            dtInOutFlights.Columns.Add("IncncAllATA");                  //���ۺ��ൽλʱ��
            dtInOutFlights.Columns.Add("IncncAllStatus");               //���ۺ��ຽ��״̬
            dtInOutFlights.Columns.Add("IncniAllViewIndex");            //���ۺ�����ʾ˳��

            dtInOutFlights.Columns.Add("OutcncDATOP");                  //���ۺ�������
            dtInOutFlights.Columns.Add("OutcnvcFLTID");                 //���ۺ��ຽ���
            dtInOutFlights.Columns.Add("OutcniLEGNO");                  //���ۺ��ຽ����Ϣ
            dtInOutFlights.Columns.Add("OutcnvcAC");                    //���ۺ���ɻ���
            dtInOutFlights.Columns.Add("OutcncAllSTD");                 //���ۺ���ƻ����ʱ��
            dtInOutFlights.Columns.Add("OutcncAllETD");                 //���ۺ���Ԥ�����ʱ��
            dtInOutFlights.Columns.Add("OutcncAllATD");                 //���ۺ����Ƴ�ʱ��
            dtInOutFlights.Columns.Add("OutcncAllTOFF");                //���ۺ������ʱ��    
            dtInOutFlights.Columns.Add("OutcncAllStatus");              //���ۺ��ຽ��״̬
            dtInOutFlights.Columns.Add("OutcniAllViewIndex");           //���ۺ�����ʾ˳��

            //�����û����õ���ͼ���������ֶ�
            foreach (DataRow dataRow in dataItems.Rows)
            {
                dtInOutFlights.Columns.Add(dataRow["cnvcDataItemID"].ToString());
            }

            //�к�
            dtInOutFlights.Columns.Add("cniRowIndex",typeof(Int32));
            return dtInOutFlights;
        }
        #endregion

        #region �������ۺ���״̬
        /// <summary>
        /// �������ۺ���״̬
        /// ����վ������Ϣ��ֳɽ��ۺ�����Ϣ��ͳ��ۺ�����Ϣ��
        /// Ȼ��������������ϳ�һ�������ۺ�����Ϣ��
        /// </summary>
        /// <param name="dtStationFlights">�����Ľ����ۺ���</param>
        /// <param name="dtInOutFlightsSchema">Ҫ��ʾ��ļܹ�</param>
        /// <param name="dtDataItems">�������б�</param>
        /// <param name="stationBM">������Ϣ</param>
        /// <returns>���� �����ۺ�����Ϣ��</returns>
        private DataTable FillInOutFlights(DataTable dtStationFlights, DataTable dtInOutFlightsSchema, DataTable dtDataItems, StationBM stationBM)
        {
            IList ilInOutFlights = new ArrayList();

            //�����ۺ�����Ϣ��
            DataTable dtAllInOutFlights = dtInOutFlightsSchema.Clone();

            //�ֱ����ɽ��ۺ�����Ϣ��ͳ��ۺ�����Ϣ��
            //�Ա��Ժ�������ɽ����ۺ�����Ϣ��
            //���ۺ��ࣺĿ�Ļ����������뺽վ��������ͬ
            DataTable dtInFlights = dtStationFlights.Clone();
            //���ۺ��ࣺ��ɻ����������뺽վ��������ͬ
            DataTable dtOutFlights = dtStationFlights.Clone();

            //��ѯ���ۺ���
            DataRow[] drInFlights = dtStationFlights.Select("cncARRSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTDWN");
            foreach (DataRow dataRow in drInFlights)
            {
                dtInFlights.ImportRow(dataRow);
            }

            //��ѯ���ۺ���
            DataRow[] drOutFlights = dtStationFlights.Select("cncDEPSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTOFF");
            foreach (DataRow dataRow in drOutFlights)
            {
                dtOutFlights.ImportRow(dataRow);
            }

            #region ���ݽ��ۺ�����Ͻ����ۺ���
            //���ݽ��ۺ�����Ͻ����ۺ���
            foreach (DataRow dataRow in dtInFlights.Rows)
            {
                //���ۺ���ķɻ���
                string strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
                //���ۺ����Ԥ��ʱ��
                string strInETD = dataRow["cncETD"].ToString();

                //��ѯ�ַ��������ݳ��ۺ���ķɻ�������ۺ���ķɻ�����ͬANDԤ�����ʱ����ڽ��ۺ����Ԥ�����ʱ��
                string strSearch = "cnvcLONG_REG = '" + strLONG_REG + "' AND cncETD > '" + strInETD + "'";
                drOutFlights = dtOutFlights.Select(strSearch, "cncETD");

                #region û�г��ۺ���
                //û�г��ۺ���
                if (drOutFlights.Length <= 0)
                {
                    //���ݺ���״̬���ú�����ʾ˳��
                    string strOutViewIndex = "";
                    if (dataRow["cncSTATUS"].ToString() == "CNL")
                    {
                        strOutViewIndex = "0";
                    }
                    else if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = "3";
                    }

                    //�½�һ�м�¼
                    DataRow drInFlight = dtAllInOutFlights.NewRow();

                    #region �������������ֶθ�ֵ
                    //�������������ֶθ�ֵ
                    //���۲���
                    drInFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //���۲���
                    drInFlight["OutcncDATOP"] = "1900-01-01";
                    drInFlight["OutcnvcFLTID"] = "HU 0000";
                    drInFlight["OutcniLEGNO"] = 100;
                    drInFlight["OutcnvcAC"] = "HH";
                    drInFlight["OutcncAllSTD"] = "";
                    drInFlight["OutcncAllETD"] = "";
                    drInFlight["OutcncAllATD"] = "";
                    drInFlight["OutcncAllTOFF"] = "";
                    drInFlight["OutcncAllStatus"] = "";
                    drInFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //���Ƚ����ۻ�������Ϊ���ۻ���
                    if (dtAllInOutFlights.Columns.Contains("OutcnvcLONG_REG"))
                    {
                        drInFlight["OutcnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                    }

                    #region ���û���������ʾ���ֶν��и�ֵ
                    //���û���������ʾ���ֶν��и�ֵ
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            drInFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }
                    #endregion

                    dtAllInOutFlights.Rows.Add(drInFlight);
                }
                #endregion

                #region �г��ۺ���
                //�г��ۺ���
                else
                {
                    //�������ۺ���״̬����
                    string strOutViewIndex = "";
                    string strStatus = drOutFlights[0]["cncSTATUS"].ToString();
                    if (drOutFlights[0]["cncSTATUS"].ToString() == "ATA" || drOutFlights[0]["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = drOutFlights[0]["cniViewIndex"].ToString();
                    }

                    //�½�һ��
                    DataRow drInOutFlight = dtAllInOutFlights.NewRow();

                    #region �������������ֶθ�ֵ
                    //�������������ֶθ�ֵ
                    //���۲���
                    drInOutFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInOutFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInOutFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInOutFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInOutFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInOutFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInOutFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInOutFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInOutFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInOutFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //���۲���
                    drInOutFlight["OutcncDATOP"] = drOutFlights[0]["cncDATOP"].ToString();
                    drInOutFlight["OutcnvcFLTID"] = drOutFlights[0]["cnvcFLTID"].ToString();
                    drInOutFlight["OutcniLEGNO"] = drOutFlights[0]["cniLEGNO"].ToString();
                    drInOutFlight["OutcnvcAC"] = drOutFlights[0]["cnvcAC"].ToString();
                    drInOutFlight["OutcncAllSTD"] = drOutFlights[0]["cncSTD"].ToString();
                    drInOutFlight["OutcncAllETD"] = drOutFlights[0]["cncETD"].ToString();
                    drInOutFlight["OutcncAllATD"] = drOutFlights[0]["cncATD"].ToString();
                    drInOutFlight["OutcncAllTOFF"] = drOutFlights[0]["cncTOFF"].ToString();
                    drInOutFlight["OutcncAllStatus"] = drOutFlights[0]["cncSTATUS"].ToString();
                    drInOutFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //���û���������ʾ���ֶν��и�ֵ
                    //���ۺ��ಿ��
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //��ʽ�������ֶ�
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //���ۻ�λ
                        if (dataRowItems["cnvcDataItemID"].ToString() == "IncnvcInGATE")
                        {
                            if (drOutFlights[0]["cnvcOutGate"].ToString().Trim() == "")
                            {
                                strFieldValue = drOutFlights[0]["cnvcOutGate"].ToString().Trim();
                            }
                        }

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            string strTemp = dataRowItems["cnvcDataItemID"].ToString();
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    //���ۺ��ಿ��
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //��ʽ�������ֶ�
                        string strFieldValue = FormatOUTItem(drOutFlights[0], dataRowItems);

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                        {
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    dtAllInOutFlights.Rows.Add(drInOutFlight);
                    dtOutFlights.Rows.Remove(drOutFlights[0]);
                }
                #endregion
            }
            #endregion

            #region ���ݳ��ۺ�����Ͻ����ۺ���
            //���ݳ��ۺ�����Ͻ����ۺ���
            //��Խ��г��ۺ�������
            foreach (DataRow dataRow in dtOutFlights.Rows)
            {
                //�������ۺ����״̬����
                string strOutViewIndex = "";
                if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                {
                    strOutViewIndex = "2";
                }
                else
                {
                    strOutViewIndex = dataRow["cniViewIndex"].ToString();
                }

                //�½�һ��
                DataRow drOutFlight = dtAllInOutFlights.NewRow();

                //�������������ֶθ�ֵ
                drOutFlight["IncncDATOP"] = "1900-01-01";
                drOutFlight["IncnvcFLTID"] = "HU 0000";
                drOutFlight["IncniLEGNO"] = 100;
                drOutFlight["IncnvcAC"] = "HH";
                drOutFlight["IncncAllSTA"] = "";
                drOutFlight["IncncAllETA"] = "";
                drOutFlight["IncncAllTDWN"] = "";
                drOutFlight["IncncAllATA"] = "";
                drOutFlight["IncncAllStatus"] = "";
                drOutFlight["IncniAllViewIndex"] = "0";

                //���Ƚ����ۻ�λ����Ϊ���ۻ�λ
                if (dtAllInOutFlights.Columns.Contains("IncnvcInGATE"))
                {
                    drOutFlight["IncnvcInGATE"] = dataRow["cnvcOutGate"].ToString();
                }

                //���Ƚ����ۻ�������Ϊ���ۻ���
                if (dtAllInOutFlights.Columns.Contains("IncnvcLONG_REG"))
                {
                    drOutFlight["IncnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                }

                //�����ۻ�������Ϊ���ۻ��� -- added by LinYong in 20150330
                if (dtAllInOutFlights.Columns.Contains("IncncACTYP") && dtOutFlights.Columns.Contains("cncACTYP"))
                {
                    drOutFlight["IncncACTYP"] = dataRow["cncACTYP"].ToString();
                }

                drOutFlight["OutcncDATOP"] = dataRow["cncDATOP"].ToString();
                drOutFlight["OutcnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                drOutFlight["OutcniLEGNO"] = dataRow["cniLEGNO"].ToString();
                drOutFlight["OutcnvcAC"] = dataRow["cnvcAC"].ToString();
                drOutFlight["OutcncAllSTD"] = dataRow["cncSTD"].ToString();
                drOutFlight["OutcncAllETD"] = dataRow["cncETD"].ToString();
                drOutFlight["OutcncAllATD"] = dataRow["cncATD"].ToString();
                drOutFlight["OutcncAllTOFF"] = dataRow["cncTOFF"].ToString();
                drOutFlight["OutcncAllStatus"] = dataRow["cncSTATUS"].ToString();
                drOutFlight["OutcniAllViewIndex"] = strOutViewIndex;

                //���û���������ʾ���ֶν��и�ֵ
                foreach (DataRow dataRowItems in dtDataItems.Rows)
                {
                    //��ʽ�������ֶ�
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    //�������ֶν��д���
                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        drOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }

                dtAllInOutFlights.Rows.Add(drOutFlight);
            }
            #endregion

            //���ݽ����ۺ�����Ϣ�����ɺ��ౣ����Ϣʵ���б�
            foreach (DataRow dataRow in dtAllInOutFlights.Rows)
            {
                ilInOutFlights.Add(new GuaranteeInforBM(dataRow));

            }
            (ilInOutFlights as ArrayList).Sort();

            //���кŸ�ֵ
            IEnumerator ieInOutFlights = ilInOutFlights.GetEnumerator();
            int iRowIndex = 0;
            while (ieInOutFlights.MoveNext())
            {
                GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieInOutFlights.Current;
                DataRow[] drInOutFlights = dtAllInOutFlights.Select("IncncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                    "IncnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                    "IncniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                    "IncnvcAC = '" + guaranteeInforBM.IncnvcAC + "' AND " +
                    "OutcncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                    "OutcnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                    "OutcniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                    "OutcnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                //�Խ����ۺ�����Ϣ��ÿ�е�cniRowIndex�ֶΣ����кŸ�ֵ
                if (drInOutFlights.Length > 0)
                {
                    drInOutFlights[0]["cniRowIndex"] = iRowIndex;
                }
                iRowIndex += 1;
            }


            //���ؽ��
            return dtAllInOutFlights;
        }
        #endregion

        #region �������ۺ���״̬
        /// <summary>
        /// �������ۺ���״̬
        /// ����վ������Ϣ��ֳɽ��ۺ�����Ϣ��ͳ��ۺ�����Ϣ��
        /// Ȼ��������������ϳ�һ�������ۺ�����Ϣ��
        /// </summary>
        /// <param name="dtStationFlights">�����Ľ����ۺ���</param>
        /// <param name="dtInOutFlightsSchema">Ҫ��ʾ��ļܹ�</param>
        /// <param name="dtDataItems">�������б�</param>
        /// <param name="stationBM">������Ϣ</param>
        /// <returns>���� �����ۺ�����Ϣ��</returns>
        private IList FillInOutFlights_1(DataTable dtStationFlights, DataTable dtInOutFlightsSchema, DataTable dtDataItems, StationBM stationBM)
        {
            IList ilInOutFlights = new ArrayList();

            //�����ۺ�����Ϣ��
            DataTable dtAllInOutFlights = dtInOutFlightsSchema.Clone();

            //�ֱ����ɽ��ۺ�����Ϣ��ͳ��ۺ�����Ϣ��
            //�Ա��Ժ�������ɽ����ۺ�����Ϣ��
            //���ۺ��ࣺĿ�Ļ����������뺽վ��������ͬ
            DataTable dtInFlights = dtStationFlights.Clone();
            //���ۺ��ࣺ��ɻ����������뺽վ��������ͬ
            DataTable dtOutFlights = dtStationFlights.Clone();

            //��ѯ���ۺ���
            DataRow[] drInFlights = dtStationFlights.Select("cncARRSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTDWN");
            foreach (DataRow dataRow in drInFlights)
            {
                dtInFlights.ImportRow(dataRow);
            }

            //��ѯ���ۺ���
            DataRow[] drOutFlights = dtStationFlights.Select("cncDEPSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTOFF");
            foreach (DataRow dataRow in drOutFlights)
            {
                dtOutFlights.ImportRow(dataRow);
            }

            #region ���ݽ��ۺ�����Ͻ����ۺ���
            //���ݽ��ۺ�����Ͻ����ۺ���
            foreach (DataRow dataRow in dtInFlights.Rows)
            {
                //���ۺ���ķɻ���
                string strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
                //���ۺ����Ԥ��ʱ��
                string strInETD = dataRow["cncETD"].ToString();

                //��ѯ�ַ��������ݳ��ۺ���ķɻ�������ۺ���ķɻ�����ͬANDԤ�����ʱ����ڽ��ۺ����Ԥ�����ʱ��
                string strSearch = "cnvcLONG_REG = '" + strLONG_REG + "' AND cncETD > '" + strInETD + "'";
                drOutFlights = dtOutFlights.Select(strSearch, "cncETD");

                #region û�г��ۺ���
                //û�г��ۺ���
                if (drOutFlights.Length <= 0)
                {
                    //���ݺ���״̬���ú�����ʾ˳��
                    string strOutViewIndex = "";
                    if (dataRow["cncSTATUS"].ToString() == "CNL")
                    {
                        strOutViewIndex = "0";
                    }
                    else if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = "3";
                    }

                    //�½�һ�м�¼
                    DataRow drInFlight = dtAllInOutFlights.NewRow();

                    #region �������������ֶθ�ֵ
                    //�������������ֶθ�ֵ
                    //���۲���
                    drInFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //���۲���
                    drInFlight["OutcncDATOP"] = "1900-01-01";
                    drInFlight["OutcnvcFLTID"] = "HU 0000";
                    drInFlight["OutcniLEGNO"] = 100;
                    drInFlight["OutcnvcAC"] = "HH";
                    drInFlight["OutcncAllSTD"] = "";
                    drInFlight["OutcncAllETD"] = "";
                    drInFlight["OutcncAllATD"] = "";
                    drInFlight["OutcncAllTOFF"] = "";
                    drInFlight["OutcncAllStatus"] = "";
                    drInFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //���Ƚ����ۻ�������Ϊ���ۻ���
                    if (dtAllInOutFlights.Columns.Contains("OutcnvcLONG_REG"))
                    {
                        drInFlight["OutcnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                    }

                    #region ���û���������ʾ���ֶν��и�ֵ
                    //���û���������ʾ���ֶν��и�ֵ
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            drInFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }
                    #endregion

                    dtAllInOutFlights.Rows.Add(drInFlight);
                }
                #endregion

                #region �г��ۺ���
                //�г��ۺ���
                else
                {
                    //�������ۺ���״̬����
                    string strOutViewIndex = "";
                    string strStatus = drOutFlights[0]["cncSTATUS"].ToString();
                    if (drOutFlights[0]["cncSTATUS"].ToString() == "ATA" || drOutFlights[0]["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = drOutFlights[0]["cniViewIndex"].ToString();
                    }

                    //�½�һ��
                    DataRow drInOutFlight = dtAllInOutFlights.NewRow();

                    #region �������������ֶθ�ֵ
                    //�������������ֶθ�ֵ
                    //���۲���
                    drInOutFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInOutFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInOutFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInOutFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInOutFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInOutFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInOutFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInOutFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInOutFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInOutFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //���۲���
                    drInOutFlight["OutcncDATOP"] = drOutFlights[0]["cncDATOP"].ToString();
                    drInOutFlight["OutcnvcFLTID"] = drOutFlights[0]["cnvcFLTID"].ToString();
                    drInOutFlight["OutcniLEGNO"] = drOutFlights[0]["cniLEGNO"].ToString();
                    drInOutFlight["OutcnvcAC"] = drOutFlights[0]["cnvcAC"].ToString();
                    drInOutFlight["OutcncAllSTD"] = drOutFlights[0]["cncSTD"].ToString();
                    drInOutFlight["OutcncAllETD"] = drOutFlights[0]["cncETD"].ToString();
                    drInOutFlight["OutcncAllATD"] = drOutFlights[0]["cncATD"].ToString();
                    drInOutFlight["OutcncAllTOFF"] = drOutFlights[0]["cncTOFF"].ToString();
                    drInOutFlight["OutcncAllStatus"] = drOutFlights[0]["cncSTATUS"].ToString();
                    drInOutFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //���û���������ʾ���ֶν��и�ֵ
                    //���ۺ��ಿ��
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //��ʽ�������ֶ�
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //���ۻ�λ
                        if (dataRowItems["cnvcDataItemID"].ToString() == "IncnvcInGATE")
                        {
                            if (drOutFlights[0]["cnvcOutGate"].ToString().Trim() == "")
                            {
                                strFieldValue = drOutFlights[0]["cnvcOutGate"].ToString().Trim();
                            }
                        }

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            string strTemp = dataRowItems["cnvcDataItemID"].ToString();
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    //���ۺ��ಿ��
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //��ʽ�������ֶ�
                        string strFieldValue = FormatOUTItem(drOutFlights[0], dataRowItems);

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                        {
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    dtAllInOutFlights.Rows.Add(drInOutFlight);
                    dtOutFlights.Rows.Remove(drOutFlights[0]);
                }
                #endregion
            }
            #endregion

            #region ���ݳ��ۺ�����Ͻ����ۺ���
            //���ݳ��ۺ�����Ͻ����ۺ���
            //��Խ��г��ۺ�������
            foreach (DataRow dataRow in dtOutFlights.Rows)
            {
                //�������ۺ����״̬����
                string strOutViewIndex = "";
                if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                {
                    strOutViewIndex = "2";
                }
                else
                {
                    strOutViewIndex = dataRow["cniViewIndex"].ToString();
                }

                //�½�һ��
                DataRow drOutFlight = dtAllInOutFlights.NewRow();

                //�������������ֶθ�ֵ
                drOutFlight["IncncDATOP"] = "1900-01-01";
                drOutFlight["IncnvcFLTID"] = "HU 0000";
                drOutFlight["IncniLEGNO"] = 100;
                drOutFlight["IncnvcAC"] = "HH";
                drOutFlight["IncncAllSTA"] = "";
                drOutFlight["IncncAllETA"] = "";
                drOutFlight["IncncAllTDWN"] = "";
                drOutFlight["IncncAllATA"] = "";
                drOutFlight["IncncAllStatus"] = "";
                drOutFlight["IncniAllViewIndex"] = "0";

                //���Ƚ����ۻ�λ����Ϊ���ۻ�λ
                if (dtAllInOutFlights.Columns.Contains("IncnvcInGATE"))
                {
                    drOutFlight["IncnvcInGATE"] = dataRow["cnvcOutGate"].ToString();
                }

                //���Ƚ����ۻ�������Ϊ���ۻ���
                if (dtAllInOutFlights.Columns.Contains("IncnvcLONG_REG"))
                {
                    drOutFlight["IncnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                }

                //�����ۻ�������Ϊ���ۻ��� -- added by LinYong in 20150330
                if (dtAllInOutFlights.Columns.Contains("IncncACTYP") && dtOutFlights.Columns.Contains("cncACTYP"))
                {
                    drOutFlight["IncncACTYP"] = dataRow["cncACTYP"].ToString();
                }

                drOutFlight["OutcncDATOP"] = dataRow["cncDATOP"].ToString();
                drOutFlight["OutcnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                drOutFlight["OutcniLEGNO"] = dataRow["cniLEGNO"].ToString();
                drOutFlight["OutcnvcAC"] = dataRow["cnvcAC"].ToString();
                drOutFlight["OutcncAllSTD"] = dataRow["cncSTD"].ToString();
                drOutFlight["OutcncAllETD"] = dataRow["cncETD"].ToString();
                drOutFlight["OutcncAllATD"] = dataRow["cncATD"].ToString();
                drOutFlight["OutcncAllTOFF"] = dataRow["cncTOFF"].ToString();
                drOutFlight["OutcncAllStatus"] = dataRow["cncSTATUS"].ToString();
                drOutFlight["OutcniAllViewIndex"] = strOutViewIndex;

                //���û���������ʾ���ֶν��и�ֵ
                foreach (DataRow dataRowItems in dtDataItems.Rows)
                {
                    //��ʽ�������ֶ�
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    //�������ֶν��д���
                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        drOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }

                dtAllInOutFlights.Rows.Add(drOutFlight);
            }
            #endregion

            //���ݽ����ۺ�����Ϣ�����ɺ��ౣ����Ϣʵ���б�
            foreach (DataRow dataRow in dtAllInOutFlights.Rows)
            {
                ilInOutFlights.Add(new GuaranteeInforBM(dataRow));

            }
            (ilInOutFlights as ArrayList).Sort();


            //���ؽ��
            return ilInOutFlights;
        }
        #endregion

        #region ��ʽ�����ۺ��������ֶ�
        /// <summary>
        /// ��ʽ�����ۺ��������ֶ�
        /// </summary>
        /// <param name="dataRow">��������</param>
        /// <param name="dataRowItems">������</param>
        /// <returns>��ʽ���������</returns>
        public string FormatINItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //���ۺ�����ɻ���
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //���ۺ��ൽ�����
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //�ƻ�����ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncSTA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //����ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncETA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //if (dataRow["cnvcDELAY1"].ToString().Trim() != "")
                //{
                //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //}
                //else
                //{
                //    strFieldValue = "";
                //}
            }
            //���ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncTDWN")
            {
                //if (strStatus == "DEP" || strStatus == "ARR" || strStatus == "ATA")
                if (strStatus == "ARR" || strStatus == "ATA") //ʱ����ص���ʱ����ʾ���ʱ��
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //��λʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncATA")
            {
                if (strStatus == "ATA")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            return strFieldValue;
        }
        #endregion

        #region ��ʽ�����ۺ��������ֶ�
        /// <summary>
        /// ��ʽ�����ۺ��������ֶ�
        /// </summary>
        /// <param name="dataRow">��������</param>
        /// <param name="dataRowItems">������</param>
        /// <returns>��ʽ���������</returns>
        public string FormatOUTItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //���ۺ�����ɻ���
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //���ۺ�����ػ���
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //�ƻ����ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTD")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //�������ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncETD")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //if (dataRow["cnvcDELAY1"].ToString().Trim() != "")
                //{
                //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //}
                //else
                //{
                //    strFieldValue = "";
                //}
            }
            //�Ƴ�ʱ��
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncATD")
            {
                if (strStatus == "ATD" || strStatus == "DEP" || strStatus == "ATA" || strStatus == "ARR")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //��ɶ�̬�̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncTOFF")
            {
                if (strStatus == "DEP" || strStatus == "ATA" || strStatus == "ARR")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //�������
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcniDUR1")
            {
                if (strFieldValue == "0")
                {
                    strFieldValue = "";
                }
            }
            return strFieldValue;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            //��ȡ��վ��վʱ����Ϣ
            OverStationTimeBF overStationTimeBF = new OverStationTimeBF();
            ReturnValueSF rvsfOverStationTime = overStationTimeBF.Select();
            if ((rvsfOverStationTime.Result > 0) && (rvsfOverStationTime.Dt != null))
            {
                _dtOverStationTime = rvsfOverStationTime.Dt;
            }
            else
            {
                MessageBox.Show("�����ݿ�����ȡ��վ��վʱ����Ϣʧ�ܣ�" +
                    Environment.NewLine + "������Ϣ��" +
                    rvsfOverStationTime.Message
                    , "��ʾ", MessageBoxButtons.OK);

                Environment.Exit(0); //�˳�����
            }

            //���졢���պ�����ĺ��������Ϣ
            FlightAlarmInfoBF flightAlarmInfoBF = new FlightAlarmInfoBF();
            ReturnValueSF rvsfFlightAlarmInfo = flightAlarmInfoBF.Select(
                DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            if ((rvsfFlightAlarmInfo.Result > 0) && (rvsfFlightAlarmInfo.Dt != null))
            {
                _dtTodayInOutFlights = rvsfFlightAlarmInfo.Dt;
            }
            else
            {
                MessageBox.Show("�����ݿ�����ȡ����澯��Ϣʧ�ܣ�" +
                    Environment.NewLine + "������Ϣ��" +
                    rvsfFlightAlarmInfo.Message
                    , "��ʾ", MessageBoxButtons.OK);

                Environment.Exit(0); //�˳�����
            }

            //��ʾ Log ҳ��
            tabControl1.SelectedTab = tabControl1.TabPages[2]; ;

            //����������
            if (!blnSetRemotingObject) //��δ������Զ�̴��������� AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("�����������ȡʧ�ܣ������µ�¼��", "��ʾ", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //������Զ�̴��������� AgentServiceDAF.objRemotingObject
            }

            //��ȡ�ú�վ��������к���
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            DateTimeBM dateTimeBM = new DateTimeBM();
            StationBM stationBM = new StationBM();
            AccountBM accountBM = new AccountBM();

            dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            //stationBM.ThreeCode = "HAK";
            //accountBM.UserId = "SYS_FlightMonitor";
            //accountBM.UserName = "ϵͳ�û����澯���ܣ�";
            //accountBM.IPAddress = "";
            stationBM.ThreeCode = "PEK";
            accountBM.UserId = "l_w";
            accountBM.UserName = "���";
            accountBM.IPAddress = "";

            ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
            DataTable dtTodayStationFlights = rvSF.Dt;

            //��ȡ�û���Ȩ�޵�������
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

            //�����ۺ�����Schema
            DataTable dtInOutFlightsSchema = GetDisplaySchema(dtDataItems);

            //��ȡ��������ۺ��� 
            //DataTable dtTodayInOutFlights = FillInOutFlights(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);
            IList ilTodayInOutFlights = FillInOutFlights_1(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);

            //������
            //DataView dataView  = new DataView(dtTodayInOutFlights, "", "cniRowIndex", DataViewRowState.CurrentRows);
            //dataGridView3.DataSource = dataView;
            dataGridView3.DataSource = ilTodayInOutFlights;

            #region ���������ۺ����
            IEnumerator ieTodayInOutFlights = ilTodayInOutFlights.GetEnumerator();
            while (ieTodayInOutFlights.MoveNext())
            {
                string strInDEPSTN = "";
                string strInARRSTN = "";
                string strOutDEPSTN = "";
                string strOutARRSTN = "";
                string strOverStationType = ""; //��վ���ͣ�ʼ������վ�����ٹ�վ������
                int iOverStationStandardTime = 0; //��վ��׼ʱ�䣨���ӣ�
                string strOverStationStart = ""; //��ʼʱ��
                string strOverStationEnd = ""; //����ʱ��
                string strAlarmCode = ""; //�澯����
                string strAlarmValue = ""; //�澯ֵ
                int iAlarmResult = 0; //�澯���


                try
                {
                    GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current; //ʵ������ǰ�����ۺ�����

                    #region �˹�ģ�ⲿ�ֲ���
                    iOverStationStandardTime = 45; //ģ�� TYN �Ĺ�վʱ�䣬Ӧ�ý�� С���� ��̬����
                    strAlarmCode = "OutcncOutPilotArriveTime";
                    #endregion �˹�ģ�ⲿ�ֲ���

                    #region ȷ�������������������
                    if (guaranteeInforBM.IncncDATOP != "1900-01-01") //���ۺ���
                    {
                        DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                            "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                            "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                            "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                        if (dataRowFlight.Length > 0)
                        {
                            strInDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                            strInARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                        }
                    }

                    if (guaranteeInforBM.OutcncDATOP != "1900-01-01") //���ۺ���
                    {
                        DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                           "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                           "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                           "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                        if (dataRowFlight.Length > 0)
                        {
                            strOutDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                            strOutARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();

                        }
                    }
                    #endregion ȷ�������������������

                    #region ȷ����վʱ��
                    DataRow[] dataRowsOverStationTime = _dtOverStationTime.Select(
                        "(cncAirportThreeCode = '" +
                        stationBM.ThreeCode + "') and (cnvcSmallACTYP = '" +
                        guaranteeInforBM.IncncACTYP + "')"); //���ݻ����������С���ͻ�ȡ��վʱ������
                    if (dataRowsOverStationTime.Length > 0)
                    {
                        iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniGroundTime"].ToString());
                    }
                    else
                    {
                        throw new Exception("��վʱ�����û�д˼�¼��" + Environment.NewLine +
                            "������" + stationBM.ThreeCode + Environment.NewLine +
                            "С���ͣ�" + guaranteeInforBM.IncncACTYP);
                    }
                    #endregion ȷ����վʱ��

                    #region ȷ����վ����
                    if ((guaranteeInforBM.IncncDATOP == "1900-01-01") &&
                        (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                    {
                        strOverStationType = "ʼ��";
                    }
                    else if ((guaranteeInforBM.IncncDATOP != "1900-01-01") &&
                        (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                    {
                        strOverStationType = "��վ";
                    }
                    else
                    {
                        strOverStationType = "����";
                    }
                    #endregion ȷ����վ����

                    #region ȷ�� ��ʼʱ�� �� ����ʱ��
                    if ((strOverStationType == "��վ") ||
                        (strOverStationType == "���ٹ�վ"))
                    {
                        //���� ��ʼʱ��
                        if (guaranteeInforBM.IncncAllStatus != "ATA")
                        {
                            strOverStationStart = guaranteeInforBM.IncncAllETA;
                        }
                        else
                        {
                            strOverStationStart = guaranteeInforBM.IncncAllATA;
                        }
                        //���� ����ʱ��
                        if (Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime) > Convert.ToDateTime(guaranteeInforBM.OutcncAllETD))
                        {
                            strOverStationEnd = Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                        }

                    }
                    else if (strOverStationType == "ʼ��")
                    {
                        //���� ��ʼʱ��
                        strOverStationStart = "";

                        //���� ����ʱ��
                        strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                    }
                    else //���󺽰಻����
                    {
                        continue;
                    }
                    #endregion ȷ�� ��ʼʱ�� �� ����ʱ��

                    #region �����鵽λ��ʱ�� �ж�
                    if (strAlarmCode == "OutcncOutPilotArriveTime")
                    {
                        DateTime dOutPilotArriveTime = new DateTime(); //ȷ������Ӧ�õ�λ��ʱ��
                        if ((strOverStationType == "��վ") ||
                            (strOverStationType == "���ٹ�վ"))
                        {

                            //ȷ������Ӧ�õ�λ��ʱ��
                            if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                            {
                                dOutPilotArriveTime = Convert.ToDateTime(strOverStationStart);
                            }
                            else
                            {
                                dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                            }
                        }
                        else if (strOverStationType == "ʼ��")
                        {
                            dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                        }
                        //�澯ֵ
                        strAlarmValue = guaranteeInforBM.OutcncOutPilotArriveTime;

                        //�澯��� �жϣ�����Ӧ�õ�λ��ʱ�� ��ǰ 5���� ��ʼ�жϣ�
                        if (DateTime.Now < dOutPilotArriveTime.AddMinutes(-5)) //
                        {
                            iAlarmResult = -1; //��δ���ж�ʱ��
                        }
                        else
                        {
                            if (guaranteeInforBM.OutcncOutPilotArriveTime.Trim() == "")
                            {
                                iAlarmResult = 1; //�ѵ��ж�ʱ�䣬����Ӧ������ OutcncOutPilotArriveTime ��δ¼������
                            }
                            else
                            {
                                DateTime dOutcncOutPilotArriveTime = Convert.ToDateTime( //��վ����ϵͳ�м�¼������(ʱ��ֵ���� 0930)
                                    guaranteeInforBM.OutcncFlightDate +
                                    guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(0, 2) +
                                    ":" +
                                    guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(2, 2) +
                                    ":00");
                                if (dOutcncOutPilotArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //¼���ֵ��ǰһ���ʱ��ֵ������ʱ��ֵû�����ڲ��֣���Ҫ�߼��жϣ�
                                {
                                    dOutcncOutPilotArriveTime.AddDays(-1);
                                }

                                if (dOutcncOutPilotArriveTime > dOutPilotArriveTime)
                                {
                                    iAlarmResult = 2; //��
                                }
                                else
                                {
                                    iAlarmResult = 0; //׼ʱ 
                                }
                            }
                        }
                        //�����ڴ�� _dtTodayInOutFlights
                        //ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
                        //    _dtTodayInOutFlights,
                        //    guaranteeInforBM,
                        //    strInDEPSTN,
                        //    strInARRSTN,
                        //    strOutDEPSTN,
                        //    strOutARRSTN,
                        //    strOverStationType,
                        //    iOverStationStandardTime.ToString(),
                        //    strOverStationStart,
                        //    strOverStationEnd,
                        //    strAlarmCode,
                        //    strAlarmValue,
                        //    iAlarmResult.ToString());
                    }
                    #endregion �����鵽λ��ʱ�� �ж�

                }
                catch (Exception ex)
                {

                }
            }
            #endregion ���������ۺ����

            dataGridView4.DataSource = _dtTodayInOutFlights;
        }


        #endregion ���ౣ�ϸ澯���� �õ��ĺ���

        private void button5_Click(object sender, EventArgs e)
        {
            //object[] objArray = new object[2];
            //objArray[0] = 1;
            //objArray[1] = "hello";

            //int iRefreshInterval = 300 * 1000; //����Ƶ������Ϊ 5����
            //TimerCallback timerDelegate = new TimerCallback(Test);
            //timer_CC = new System.Threading.Timer(timerDelegate, objArray, 0, iRefreshInterval);

            //DateTime.Parse(m_outChangeRecordBM.STD).ToUniversalTime().ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
            string dateTimeTest = DateTime.Parse("2015-09-03").ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
            dateTimeTest = dateTimeTest;

        }
        public void Test(object state)
        {
            object[] objArray = (object[])state;
            int i = (int)(objArray[0]);
            string s = (string)(objArray[1]);

            string r = "";
        }
    }
}