using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Configuration;
using System.Data;
using AirSoft.FlightMonitor.AgentServiceBM;
using CompressDataSet.Common;
using System.Collections;

namespace AirSoft.FlightMonitor.AgentServiceBF
{
    /// <summary>
    /// ���ݷ��ʴ������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-11-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class AgentServiceBF
    {
        #region ע��Tcpͨ��
        /// <summary>
        /// ע��Tcpͨ��
        /// </summary>
        /// <returns>ReturnValueSF.Result��1 �ɹ���-1 ʧ�ܡ� ReturnValueSF.Message��ʧ��ԭ��</returns>
        public ReturnValueSF RegisterChannel()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //����Tcpͨ��
                //TcpChannel tcpChannel = new TcpChannel(Convert.ToInt32(ConfigurationManager.AppSettings["AgentPort"].Trim()));
                IDictionary props = new Hashtable();
                props["name"] = "ChannelName";
                props["port"] = Convert.ToInt32(ConfigurationManager.AppSettings["AgentPort"].Trim());
                TcpChannel tcpChannel = new TcpChannel(props, new BinaryClientFormatterSinkProvider(), new BinaryServerFormatterSinkProvider());
                //IChannel channel = new TcpChannel(props, new BinaryClientFormatterSinkProvider(), new BinaryServerFormatterSinkProvider());
                //ChannelServices.RegisterChannel(channel, true);
                
                //ע��ͨ��
                ChannelServices.RegisterChannel(tcpChannel, false);

                RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(AgentServiceDAF), "AgentServiceDAF", WellKnownObjectMode.Singleton
                );

                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion

        #region ע��Tcpͨ��
        /// <summary>
        /// ע��Tcpͨ��
        /// </summary>
        /// <returns>ReturnValueSF.Result��1 �ɹ���-1 ʧ�ܡ� ReturnValueSF.Message��ʧ��ԭ��</returns>
        public ReturnValueSF RegisterChannel_Bak()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //����Tcpͨ��
                TcpChannel tcpChannel = new TcpChannel(Convert.ToInt32(ConfigurationManager.AppSettings["AgentPort"].Trim()));


                //ע��ͨ��
                ChannelServices.RegisterChannel(tcpChannel, false);

                RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(AgentServiceDAF), "AgentServiceDAF", WellKnownObjectMode.Singleton
                );

                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion

        #region ��ʼ�� AgentServiceDAF ��(��¼��)
        /// <summary>
        /// ��ʼ�� AgentServiceDAF ��(��¼��)
        /// </summary>
        /// <returns>ReturnValueSF.Result��1 �ɹ���-1 ʧ�ܡ� ReturnValueSF.Message��ʧ��ԭ��</returns>
        public ReturnValueSF InitializeDAL()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();

                AgentServiceDAF.dtProcRecords = procRecordsDAF.CreateDatatable();
                AgentServiceDAF.dtProcAnalysis = procAnalysisDAF.CreateDatatable();

                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// �����ṩ�Ĳ�����ʼ�� AgentServiceDAF�� �� ��¼�� ����
        /// </summary>
        /// <param name="strInitTable">��Ҫ��ʼ���ı�񣬸�ʽ�磺";dtProcRecords;dtProcAnalysis;dtOnLineUsers;"</param>
        /// <returns></returns>
        public ReturnValueSF InitializeDAL(string strInitTables)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //
                if (strInitTables.IndexOf(";dtProcRecords;") >= 0)
                {
                    ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                    AgentServiceDAF.dtProcRecords = procRecordsDAF.CreateDatatable();
                }
                if (strInitTables.IndexOf(";dtProcAnalysis;") >= 0)
                {
                    ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                    AgentServiceDAF.dtProcAnalysis = procAnalysisDAF.CreateDatatable();
                }
                if (strInitTables.IndexOf(";dtOnLineUsers;") >= 0)
                {
                    OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                    AgentServiceDAF.dtOnLineUsers = onLineUsersDAF.CreateDatatable();
                }

                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ��ȡ AgentServiceDAF ��� ��̬���
        /// <summary>
        /// ��ȡ AgentServiceDAF ��� ��̬���
        /// </summary>
        /// <param name="strDatatableName">������ƣ�tbLegs;vw_Legs;vw_FlightChangeRecord;dtProcRecords;dtProcAnalysis</param>
        /// <returns></returns>
        public ReturnValueSF GetDatatable(string strDatatableName)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //
                rvSF.Result = 1;

                if (strDatatableName == "tbLegs")
                    rvSF.Dt = AgentServiceDAF.tbLegs;
                else if (strDatatableName == "vw_Legs")
                    rvSF.Dt = AgentServiceDAF.vw_Legs;
                else if (strDatatableName == "vw_FlightChangeRecord")
                    rvSF.Dt = AgentServiceDAF.vw_FlightChangeRecord;
                else if (strDatatableName == "dtProcRecords")
                    rvSF.Dt = AgentServiceDAF.dtProcRecords;
                else if (strDatatableName == "dtProcAnalysis")
                    rvSF.Dt = AgentServiceDAF.dtProcAnalysis;
                else if (strDatatableName == "dtOnLineUsers")
                    rvSF.Dt = AgentServiceDAF.dtOnLineUsers;
                else
                {
                    rvSF.Result = -1;
                    rvSF.Message = "û�б��" + strDatatableName + "!";
                    rvSF.Dt = null;
                }

            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        #endregion

        #region ����Զ�̶���
        public ReturnValueSF SetRemotingObject()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            string strURL = null;

            try
            {
                //
                rvSF.Result = 1;
                strURL = "tcp://" + ConfigurationManager.AppSettings["AgentIP"].Trim() + ":"
                    + ConfigurationManager.AppSettings["AgentPort"].Trim() + "/AgentServiceDAF";
                AgentServiceDAF.objRemotingObject = (AgentServiceDAF)Activator.GetObject(
                    typeof(AgentServiceDAF),
                    strURL);

                if (AgentServiceDAF.objRemotingObject == null)
                {
                    rvSF.Result = -1;
                    rvSF.Message = "����Զ�̶���ʧ�ܣ�";
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }
            return rvSF;
        }

        #endregion

        #region ����Զ�̶���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentIP">�������IP</param>
        /// <param name="agentPort">�������˿�</param>
        /// <returns></returns>
        public ReturnValueSF SetRemotingObject(string agentIP, string agentPort)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            string strURL = null;

            try
            {
                //
                rvSF.Result = 1;
                strURL = "tcp://" + agentIP.Trim() + ":"
                    + agentPort.Trim() + "/AgentServiceDAF";
                AgentServiceDAF.objRemotingObject = (AgentServiceDAF)Activator.GetObject(
                    typeof(AgentServiceDAF),
                    strURL);

                if (AgentServiceDAF.objRemotingObject == null)
                {
                    rvSF.Result = -1;
                    rvSF.Message = "����Զ�̶���ʧ�ܣ�";
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        #endregion

        #region ��ȡԶ�̶���
        public AgentServiceDAF GetRemotingObject()
        {
            return AgentServiceDAF.objRemotingObject;
        }
        #endregion


        #region ͬ���ڴ������ݿ��

        #region ͬ���ڴ������ݿ��
        /// <summary>
        /// ͬ���ڴ������ݿ��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_Bak()
        {
            #region ��������
            ReturnValueSF rvSF = null;
            int intResult = 1;
            string strMessage = "";
            string strStartTime = "", strEndTime = "";

            #endregion


            #region ����ʵ��

            //
            strStartTime = DateTime.Now.ToString("HH:mm:ss");
            strMessage = "����ͬ����";

            //���� tbLegs
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            rvSF = SynchronizeDatas_tbLegs();
            if (rvSF.Result > 0)
                strMessage += "��tbLegs���ɹ���";
            else
                strMessage += "��tbLegs��ʧ�ܣ�";

            //���� vw_Legs
            strMessage += "[" + DateTime.Now.ToString("mm:ss") + "]";
            rvSF = SynchronizeDatas_vw_Legs();
            if (rvSF.Result > 0)
                strMessage += "��vw_Legs���ɹ���";
            else
                strMessage += "��vw_Legs��ʧ�ܣ�";

            //���� vw_FlightChangeRecord
            strMessage += "[" + DateTime.Now.ToString("m:ss") + "]";
            rvSF = SynchronizeDatas_vw_FlightChangeRecord();
            if (rvSF.Result > 0)
                strMessage += "��vw_FlightChangeRecord���ɹ���";
            else
                strMessage += "��vw_FlightChangeRecord��ʧ�ܣ�";

            //
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            strEndTime = DateTime.Now.ToString("HH:mm:ss");

            //���ؽ��
            rvSF = new ReturnValueSF();
            rvSF.Result = intResult;
            //rvSF.Message = strMessage + "[" + strStartTime + "-" + strEndTime + "]";
            rvSF.Message = strMessage;

            return rvSF;

            #endregion
        }

        #endregion

        #region ͬ���ڴ������ݿ�� (tbLegs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbLegs()
        {
            #region ��������
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��
            dateTimeBM.StartDateTime = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";

            //���� tbLegs
            try
            {
                //�������ݷ��ʲ�����෽��
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF.Dt = changeLegsDAF.GetAllLegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                AgentServiceDAF.tbLegs = rvSF.Dt;
                rvSF.Result = 1;
            }
            else
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }
        #endregion

        #region ͬ���ڴ������ݿ�� (vw_Legs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_Legs()
        {
            #region ��������
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��
            dateTimeBM.StartDateTime = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";

            //���� vw_Legs
            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetAllvw_LegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                AgentServiceDAF.vw_Legs = rvSF.Dt;
            }
            else
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }
        #endregion

        #region ͬ���ڴ������ݿ�� (vw_FlightChangeRecord)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_FlightChangeRecord()
        {
            #region ��������
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��
            dateTimeBM.StartDateTime = DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss");
            dateTimeBM.EndDateTime = dateTimeBM.StartDateTime;

            //���� vw_FlightChangeRecord
            try
            {
                ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                rvSF.Dt = changeRecordDAF.GetLastvw_FlightChangeRecord(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Dt = null;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                AgentServiceDAF.vw_FlightChangeRecord = rvSF.Dt;
            }
            else
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }
        #endregion


        #region ͬ���ڴ������ݿ��,��Խ������ guaranteeInforBF.GetFlightsByStation �ĺ������ݺͱ������ͬ������
        /// <summary>
        /// ͬ���ڴ������ݿ��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas()
        {
            #region ��������
            ReturnValueSF rvSF = null;
            int intResult = 1;
            string strMessage = "";
            string strStartTime = "", strEndTime = "";

            DataTable dttbLegs = null, dtvw_legs = null, dtvw_FlightChangeRecord = null;
            bool blntbLegs = false, blnvw_legs = false, blnvw_FlightChangeRecord = false;
            #endregion


            #region ����ʵ��

            //
            strStartTime = DateTime.Now.ToString("HH:mm:ss");
            strMessage = "����ͬ����";

            //���� tbLegs
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            if (SysMsgBM.AgentLevel == "2")
            {
                if (SysMsgBM.Compress.ToUpper() == "TRUE")
                    rvSF = GettbLegs();
                else
                    rvSF = GettbLegs_NotCompress();
            }
            else
                rvSF = SynchronizeDatas_tbLegs__1();

            if (rvSF.Result > 0)
            {
                strMessage += "��tbLegs���ɹ���";
                dttbLegs = rvSF.Dt;
                blntbLegs = true;
            }
            else
                strMessage += "��tbLegs��ʧ�ܣ�";

            //���� vw_FlightChangeRecord
            strMessage += "[" + DateTime.Now.ToString("m:ss") + "]";
            if (SysMsgBM.AgentLevel == "2")
            {
                if (SysMsgBM.Compress.ToUpper() == "TRUE")
                    rvSF = Getvw_FlightChangeRecord();
                else
                    rvSF = Getvw_FlightChangeRecord_NotCompress();
            }
            else
                rvSF = SynchronizeDatas_vw_FlightChangeRecord__1();

            if (rvSF.Result > 0)
            {
                strMessage += "��vw_FlightChangeRecord���ɹ���";
                dtvw_FlightChangeRecord = rvSF.Dt;
                blnvw_FlightChangeRecord = true;
            }
            else
                strMessage += "��vw_FlightChangeRecord��ʧ�ܣ�";

            //���� vw_Legs
            strMessage += "[" + DateTime.Now.ToString("mm:ss") + "]";

            if (SysMsgBM.AgentLevel == "2")
            {
                if (SysMsgBM.Compress.ToUpper() == "TRUE")
                    rvSF = Getvw_Legs();
                else
                    rvSF = Getvw_Legs_NotCompress();
            }
            else
                rvSF = SynchronizeDatas_vw_Legs__1();

            if (rvSF.Result > 0)
            {
                strMessage += "��vw_Legs���ɹ���";
                dtvw_legs = rvSF.Dt;
                blnvw_legs = true;
            }
            else
                strMessage += "��vw_Legs��ʧ�ܣ�";

            //
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            strEndTime = DateTime.Now.ToString("HH:mm:ss");

            //��ֵ AgentServiceDAF ����Ӧ����
            if (blntbLegs)
                AgentServiceDAF.tbLegs = dttbLegs;
            if (blnvw_FlightChangeRecord && blnvw_legs) //�ȸ�ֵvw_Legs��Ȼ����vw_FlightChangeRecord������������
            {
                AgentServiceDAF.vw_Legs = dtvw_legs;
                AgentServiceDAF.vw_FlightChangeRecord = dtvw_FlightChangeRecord;
            }

            //���ؽ��
            rvSF = new ReturnValueSF();
            rvSF.Result = intResult;
            //rvSF.Message = strMessage + "[" + strStartTime + "-" + strEndTime + "]";
            rvSF.Message = strMessage;

            return rvSF;

            #endregion
        }

        #endregion

        #region ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵAgentServiceDAF.tbLegs (tbLegs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbLegs__1()
        {
            #region ��������
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��
            dateTimeBM.StartDateTime = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";

            //���� tbLegs
            try
            {
                //�������ݷ��ʲ�����෽��
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF.Dt = changeLegsDAF.GetAllLegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                //AgentServiceDAF.tbLegs = rvSF.Dt;
                rvSF.Result = 1;
            }
            else
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }
        #endregion

        #region ͬ���ڴ������ݿ�� -- ��δ��ֵAgentServiceDAF.vw_Legs (vw_Legs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_Legs__1()
        {
            #region ��������
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��
            dateTimeBM.StartDateTime = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";

            //���� vw_Legs
            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetAllvw_LegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                //AgentServiceDAF.vw_Legs = rvSF.Dt;
                rvSF.Result = 1;
            }
            else
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }
        #endregion

        #region ͬ���ڴ������ݿ�� -- ��δ��ֵAgentServiceDAF.vw_FlightChangeRecord (vw_FlightChangeRecord)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_FlightChangeRecord__1()
        {
            #region ��������
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��
            dateTimeBM.StartDateTime = DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss");
            dateTimeBM.EndDateTime = dateTimeBM.StartDateTime;

            //���� vw_FlightChangeRecord
            try
            {
                ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                rvSF.Dt = changeRecordDAF.GetLastvw_FlightChangeRecord(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Dt = null;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                //AgentServiceDAF.vw_FlightChangeRecord = rvSF.Dt;
                rvSF.Result = 1;
            }
            else
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }
        #endregion


        #region GettbLegs()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����
        /// <summary>
        /// GettbLegs()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GettbLegs()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GettbLegs();
                    if (bytesToDecompress == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs]�������ݱ� null��[\GettbLegs][\AgentServiceDAF]");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs]���ݽ�ѹ����[\GettbLegs][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][GettbLegs]AgentServiceDAF.objRemotingObject Ϊ null��[\GettbLegs][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDecompressedDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion GettbLegs()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����

        #region GettbLegs_NotCompress()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// <summary>
        /// GettbLegs_NotCompress()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GettbLegs_NotCompress()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.GettbLegs_NotCompress();
                    if (dtDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs_NotCompress]�������ݱ� null��[\GettbLegs_NotCompress][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][GettbLegs_NotCompress]AgentServiceDAF.objRemotingObject Ϊ null��[\GettbLegs_NotCompress][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion GettbLegs_NotCompress()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��


        #region Getvw_Legs()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е�����
        /// <summary>
        /// Getvw_Legs()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_Legs()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.Getvw_Legs();
                    if (bytesToDecompress == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_Legs]�������ݱ� null��[\Getvw_Legs][\AgentServiceDAF]");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_Legs]���ݽ�ѹ����[\Getvw_Legs][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_Legs]AgentServiceDAF.objRemotingObject Ϊ null��[\Getvw_Legs][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDecompressedDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion Getvw_Legs()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е�����

        #region Getvw_Legs_NotCompress()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// <summary>
        /// Getvw_Legs_NotCompress()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_Legs_NotCompress()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.Getvw_Legs_NotCompress();
                    if (dtDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_Legs_NotCompress]�������ݱ� null��[\Getvw_Legs_NotCompress][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_Legs_NotCompress]AgentServiceDAF.objRemotingObject Ϊ null��[\Getvw_Legs_NotCompress][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion Getvw_Legs_NotCompress()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��


        #region Getvw_FlightChangeRecord()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е�����
        /// <summary>
        /// Getvw_FlightChangeRecord()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_FlightChangeRecord()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.Getvw_FlightChangeRecord();
                    if (bytesToDecompress == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord]�������ݱ� null��[\Getvw_FlightChangeRecord][\AgentServiceDAF]");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord]���ݽ�ѹ����[\Getvw_FlightChangeRecord][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord]AgentServiceDAF.objRemotingObject Ϊ null��[\Getvw_FlightChangeRecord][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDecompressedDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion Getvw_FlightChangeRecord()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е�����

        #region Getvw_FlightChangeRecord_NotCompress()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// <summary>
        /// Getvw_FlightChangeRecord_NotCompress()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_FlightChangeRecord_NotCompress()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.Getvw_FlightChangeRecord_NotCompress();
                    if (dtDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs_NotCompress]�������ݱ� null��[\GettbLegs_NotCompress][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord_NotCompress]AgentServiceDAF.objRemotingObject Ϊ null��[\Getvw_FlightChangeRecord_NotCompress][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion Getvw_FlightChangeRecord_NotCompress()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е����ݣ����ص����ݲ�����ѹ��















        #endregion

    }
}
