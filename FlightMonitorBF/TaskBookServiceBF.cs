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
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ���������ݷ��ʷ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-09-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class TaskBookServiceBF
    {
        //ִ�� SynchronizeDatas_tbBasicCrewInfo_Profile__1 ��ʱ��
        static private DateTime execSynchronizeDatas_tbBasicCrewInfo_Profile__1 = Convert.ToDateTime( "1900-01-01");


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
                TcpChannel tcpChannel = new TcpChannel(Convert.ToInt32(ConfigurationManager.AppSettings["TaskBookServicePort"].Trim()));

                //ע��ͨ��
                ChannelServices.RegisterChannel(tcpChannel, false);

                RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(TaskBookServiceDAF), "TaskBookServiceDAF", WellKnownObjectMode.Singleton
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
                strURL = "tcp://" + ConfigurationManager.AppSettings["TaskBookServiceIP"].Trim() + ":"
                    + ConfigurationManager.AppSettings["TaskBookServicePort"].Trim() + "/TaskBookServiceDAF";
                TaskBookServiceDAF.objRemotingObject = (TaskBookServiceDAF)Activator.GetObject(
                    typeof(TaskBookServiceDAF),
                    strURL);

                if (TaskBookServiceDAF.objRemotingObject == null)
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
        public TaskBookServiceDAF GetRemotingObject()
        {
            return TaskBookServiceDAF.objRemotingObject;
        }
        #endregion


        #region ͬ���ڴ������ݿ��
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

            DataTable dttbFltReport = null, dttbBasicCrewInfo_Profile = null, dttbLegs = null;
            bool blntbFltReport = false, blntbBasicCrewInfo_Profile = false,  blntbLegs = false;
            #endregion


            #region ����ʵ��

            //
            strStartTime = DateTime.Now.ToString("HH:mm:ss");
            strMessage = "����ͬ����";

            //���� tbFltReport
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            rvSF = SynchronizeDatas_tbFltReport__1();
            if (rvSF.Result > 0)
            {
                strMessage += "��tbFltReport���ɹ���";
                dttbFltReport = rvSF.Dt;
                blntbFltReport = true;

            }
            else
                strMessage += "��tbFltReport��ʧ�ܣ�";

            //���� tbBasicCrewInfo_Profile
            if (DateTime.Now.ToString("yyyy-MM-dd") != execSynchronizeDatas_tbBasicCrewInfo_Profile__1.ToString("yyyy-MM-dd"))
            {
                strMessage += "[" + DateTime.Now.ToString("mm:ss") + "]";
                rvSF = SynchronizeDatas_tbBasicCrewInfo_Profile__1();
                if (rvSF.Result > 0)
                {
                    strMessage += "��tbBasicCrewInfo_Profile���ɹ���";
                    dttbBasicCrewInfo_Profile = rvSF.Dt;
                    blntbBasicCrewInfo_Profile = true;

                    execSynchronizeDatas_tbBasicCrewInfo_Profile__1 = DateTime.Now;
                }
                else
                    strMessage += "��tbBasicCrewInfo_Profile��ʧ�ܣ�";
            }

            //���� tbLegs
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            rvSF = SynchronizeDatas_tbLegs__1();
            if (rvSF.Result > 0)
            {
                strMessage += "��tbLegs���ɹ���";
                dttbLegs = rvSF.Dt;
                blntbLegs = true;

                #region ���� ����źͺ��� ������
                DataColumn dataColumn = new DataColumn("cnvcFlightNo_Cal", Type.GetType("System.String"));
                dataColumn.DefaultValue = "";
                dataColumn.Caption = "�����";
                dttbLegs.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcROUTE_Cal", Type.GetType("System.String"));
                dataColumn.DefaultValue = "";
                dataColumn.Caption = "����";
                dttbLegs.Columns.Add(dataColumn);

                foreach (DataRow dataRowdttbLegs in dttbLegs.Rows)
                {
                    dataRowdttbLegs["cnvcFlightNo_Cal"] = dataRowdttbLegs["cnvcFlightNo"].ToString().Replace(" ", "");
                    dataRowdttbLegs["cnvcROUTE_Cal"] = dataRowdttbLegs["cncDEPSTN"].ToString() + "-" + dataRowdttbLegs["cncARRSTN"].ToString();
                }

                #endregion ���� ����źͺ��� ������

            }
            else
                strMessage += "��tbLegs��ʧ�ܣ�";

            //
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            strEndTime = DateTime.Now.ToString("HH:mm:ss");

            //��ֵ TaskBookServiceDAF ����Ӧ����
            if (blntbFltReport)
                TaskBookServiceDAF.tbFltReport = dttbFltReport;
            if (blntbBasicCrewInfo_Profile)
                TaskBookServiceDAF.tbBasicCrewInfo_Profile = dttbBasicCrewInfo_Profile;
            if (blntbLegs)
                TaskBookServiceDAF.tbLegs = dttbLegs;

            //���ؽ��
            rvSF = new ReturnValueSF();
            rvSF.Result = intResult;
            //rvSF.Message = strMessage + "[" + strStartTime + "-" + strEndTime + "]";
            rvSF.Message = strMessage;

            return rvSF;

            #endregion
        }

        #endregion ͬ���ڴ������ݿ��

        #region ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbFltReport (tbFltReport)
        /// <summary>
        /// ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbFltReport (tbFltReport)
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbFltReport__1()
        {
            #region ��������
            DateTime DATOP_Start, DATOP_End;
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��
            DATOP_Start = Convert.ToDateTime(DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00");
            DATOP_End = Convert.ToDateTime( DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00");

            //���� tbFltReport
            try
            {
                //�������ݷ��ʲ�����෽��
                VoyageReportDAF voyageReportDAF = new VoyageReportDAF();
                rvSF.Dt = voyageReportDAF.GetVoyageReportData(DATOP_Start, DATOP_End);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
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
        #endregion ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbFltReport (tbFltReport)

        #region ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbBasicCrewInfo_Profile (tbBasicCrewInfo_Profile)
        /// <summary>
        /// ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbBasicCrewInfo_Profile (tbBasicCrewInfo_Profile)
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbBasicCrewInfo_Profile__1()
        {
            #region ��������
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��ʼ��

            //���� tbBasicCrewInfo_Profile
            try
            {
                //�������ݷ��ʲ�����෽��
                BasicCrewInfoDAF basicCrewInfoDAF = new BasicCrewInfoDAF();
                rvSF.Dt = basicCrewInfoDAF.GetProfileInfo().Tables[0];
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
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
        #endregion ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbBasicCrewInfo_Profile (tbBasicCrewInfo_Profile)

        #region ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbLegs (tbLegs)
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
        #endregion ͬ���ڴ������ݿ�� -- �������ݱ���δ��ֵTaskBookServiceDAF.tbLegs (tbLegs)


        #region ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���¸�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������ReturnValueSF GetVoyageReportDataBySingleFlight_In(...)
        /// <summary>
        /// ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���¸�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�����
        /// </summary>
        /// <param name="DATOP"></param>
        /// <param name="FLTIDS"></param>
        /// <param name="AC"></param>
        /// <param name="ROUTES"></param>
        /// <param name="STD">�ƻ����ʱ�䣬����ʱ��ָ���Ϊ���ո�</param>
        /// <param name="STA">�ƻ�����ʱ�䣬����ʱ��ָ���Ϊ���ո�</param>
        /// <returns></returns>
        public ReturnValueSF GetVoyageReportDataBySingleFlight_In(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                DataTable dtDecompressedDatatable = null;

                TaskBookServiceDAF objRemotingObject = TaskBookServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetVoyageReportDataBySingleFlight_In( DATOP,  FLTIDS,  AC,  ROUTES,  STD,  STA);
                    if (bytesToDecompress == null)
                        throw new Exception("�������ݱ� null��");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("���ݽ�ѹ����");
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
        #endregion ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���¸�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������ReturnValueSF GetVoyageReportDataBySingleFlight_In(...)

        #region ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���ϸ�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������ReturnValueSF GetVoyageReportDataBySingleFlight_Out(...)
        /// <summary>
        /// ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���ϸ�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�����
        /// </summary>
        /// <param name="DATOP"></param>
        /// <param name="FLTIDS"></param>
        /// <param name="AC"></param>
        /// <param name="ROUTES"></param>
        /// <param name="STD">�ƻ����ʱ�䣬����ʱ��ָ���Ϊ���ո�</param>
        /// <param name="STA">�ƻ�����ʱ�䣬����ʱ��ָ���Ϊ���ո�</param>
        /// <returns></returns>
        public ReturnValueSF GetVoyageReportDataBySingleFlight_Out(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                DataTable dtDecompressedDatatable = null;

                TaskBookServiceDAF objRemotingObject = TaskBookServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetVoyageReportDataBySingleFlight_Out(DATOP, FLTIDS, AC, ROUTES, STD, STA);
                    if (bytesToDecompress == null)
                        throw new Exception("�������ݱ� null��");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("���ݽ�ѹ����");
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
        #endregion ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���ϸ�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������ReturnValueSF GetVoyageReportDataBySingleFlight_Out(...)





    }
}
