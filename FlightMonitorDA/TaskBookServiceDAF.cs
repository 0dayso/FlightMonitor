using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.Public.SystemFramework;
using CompressDataSet.Common;


namespace AirSoft.FlightMonitor.FlightMonitorDA
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
    public class TaskBookServiceDAF : MarshalByRefObject
    {
        #region Զ�̶���
        static public TaskBookServiceDAF objRemotingObject = null;
        #endregion

        #region �ڴ����ݱ�������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ�������� GetDataBySQL ���̡�
        static public DataTable tbFltReport = null;  //��ǰ�����飬������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ��������  ���̡�
        static public DataTable tbBasicCrewInfo_Profile = null; //��Ա������Ϣ��������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ��������  ���̡�

        static public DataTable tbLegs = null;  //���ද̬��������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ��������  ���̡�


        #endregion

        #region ������æ��־
        static public bool blnBusy_tbFltReport = false;
        static public bool blnBusy_tbBasicCrewInfo_Profile = false;

        #endregion


        #region ����Ĺ���

        #region ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���¸�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������byte[] GetVoyageReportDataBySingleFlight_In(...)
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
        public byte[] GetVoyageReportDataBySingleFlight_In(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";


            DataTable dataTableResult = new DataTable();   //���ؽ������񣬽��ۺ��ࣩ
            string strFltReportId = "";     //������ ID������������������ʹ��

            #region ��¼ʹ��
            //������
            string strProcName = ""; 
            //�������
            string strOprationResult = "";
            //���ô���(SQL + MEM)
            int iOprationCount = 0;
            //������ʼʱ��
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //ѹ��֮ǰ��С������byte��
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //ѹ��֮���С������byte��
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //����ִ��ʱ���������룩
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //����ִ��ǰʱ��
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //����ִ�к�ʱ��
            double fTotalProcTimes = 0;

            //ѹ��ʱ���������룩
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //ѹ��ִ��ǰʱ��
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //ѹ��ִ�к�ʱ��
            double fTotalCompressTimes = 0;
            #endregion ��¼ʹ��

            #endregion ��������


            #region ����ʵ��
            #region ���ɱ��
            DataColumn dataColumn = null;

            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
            dataColumn.AutoIncrement = true;
            dataColumn.AutoIncrementSeed = 1;
            dataColumn.AutoIncrementStep = 1;
            dataColumn.Caption = "���";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "λ��";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "����";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "����";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "����֤��";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "��˺���";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "��ϵ�绰";
            dataTableResult.Columns.Add(dataColumn);
            
            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "ִ�к��ࣨ�¶Σ�";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "��ע";
            dataTableResult.Columns.Add(dataColumn);
            #endregion ���ɱ��


            #region ��ȡ���ݣ�����������ȡ��ǰ���������ݣ�
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbFltReport)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "(STD like '%" + DATOP + "%') and " +
                        "(FLTIDS like '%" + FLTIDS + "%') and " +
                        "(AC like '%" + AC + "%') and " +
                        "(ROUTES like '%" + ROUTES + "%') " ;

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    VoyageReportDAF voyageReportDAF = new VoyageReportDAF();
                    dataTable = voyageReportDAF.GetVoyageReportDataBySingleFlight(DATOP, FLTIDS, AC, ROUTES);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //��¼ʹ��
                strOprationResult = "�ɹ�";
            }
            catch
            {
                dataTable = null;

                //��¼ʹ��
                strOprationResult = "ʧ��";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion ��ȡ���ݣ�����������ȡ��ǰ���������ݣ�

            #region ��¼ cniFltReportId
            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                strFltReportId = dataTable.Rows[0]["cniFltReportId"].ToString();
            }
            #endregion ��¼ cniFltReportId

            #region ��ǰ�������е� MemberInfo ���� ���ؽ������ִ�к���ӻ����й��˵��Ǵ�˴˺��ε���Ա��Ϣ


            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                VoyageReportBM voyageReportBM = new VoyageReportBM(dataTable.Rows[0]);
                if (voyageReportBM.Success)
                {
                    voyageReportBM.AnalyseVoyageReport_MemberInfo();
                    if (voyageReportBM.MemberInfo != null)
                    {
                        foreach (DataRow dataRow in voyageReportBM.MemberInfo.Rows)
                        {
                            //if ((dataRow["cnvcDepArrStn"].ToString().Trim() == "") || (dataRow["cnvcDepArrStn"].ToString().Trim() == ROUTES))
                            if ((dataRow["cnvcDepArrStn"].ToString().Trim() == "") || (dataRow["cnvcDepArrStn"].ToString().Trim().IndexOf( ROUTES) >= 0))
                                {
                                DataRow dataRowdataTableResult = dataTableResult.NewRow();

                                dataRowdataTableResult["cnvcPosition"] = dataRow["cnvcPosition"].ToString();
                                dataRowdataTableResult["cnvcName"] = dataRow["cnvcName"].ToString();
                                dataRowdataTableResult["cnvcLevel"] = dataRow["cnvcLevel"].ToString();
                                dataRowdataTableResult["cnvcSID"] = dataRow["cnvcSID"].ToString();
                                dataRowdataTableResult["cnvcDepArrStn"] = dataRow["cnvcDepArrStn"].ToString();

                                dataTableResult.Rows.Add(dataRowdataTableResult);
                            }
                        }
                    }
                }
            }

            #endregion ��ǰ�������е� MemberInfo ���� ���ؽ������ִ�к���ӻ����й��˵��Ǵ�˴˺��ε���Ա��Ϣ

            #region �����Ա��ϵ��Ϣ
            foreach (DataRow dataRowIndex in dataTableResult.Rows)
            {
                //��ȡ��Ա��ϵ��Ϣ
                DataRow[] dataRowsBasicCrewInfo_Profile = tbBasicCrewInfo_Profile.Select("STAFFID = '" + dataRowIndex["cnvcSID"].ToString().Trim() + "'");
                if (dataRowsBasicCrewInfo_Profile.Length > 0)
                    dataRowIndex["cnvcMOBILE"] = dataRowsBasicCrewInfo_Profile[0]["MOBILE"].ToString();
            }

            #endregion �����Ա��ϵ��Ϣ

            #region ������Ա���¶�ִ�к�����Ϣ
            foreach (DataRow dataRowIndex in dataTableResult.Rows)   //����ÿ����Ա����Ա������ dataRowIndex
            {
                try
                {
                    DataTable dataTableSelectMember = null;     //���� ĳ����Ա �� �ڴ�� �� �� ���������ݣ�ÿ����������ܺ��ж�κ�����Ϣ��

                    strSQL = "(Captain_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SKIPPER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(FIRST_VICE1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(TELER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(PILOT_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(CHIEF_STEWARD_CAPTAIN_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_INSTRUCTOR_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SAFER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CAP1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARDS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +  //added by LinYong in 20150325
                        "(PILOT_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') ";

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTableSelectMember = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);

                    if ((dataTableSelectMember != null) && (dataTableSelectMember.Rows.Count > 0))
                    {

                        DataTable dataTableSelectMember_SegmentInfo = new DataTable(); //���� ĳ����Ա �� �ڴ�������� �� �� ����ִ�к�����Ϣ

                        foreach (DataRow dataRowdataTableSelectMember in dataTableSelectMember.Rows)
                        {
                            VoyageReportBM voyageReportBMdataTableSelectMember = new VoyageReportBM(dataRowdataTableSelectMember);
                            if (voyageReportBMdataTableSelectMember.Success)
                            {
                                string strMemo = "";    //added by LinYong in 20150325
                                string strDepArrStn = "";   //added by LinYong in 20150325

                                if (voyageReportBMdataTableSelectMember.pilot_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)    //added by LinYong in 20150325        
                                    strMemo = "�ӻ���";
                                if (voyageReportBMdataTableSelectMember.steward_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)  //added by LinYong in 20150325
                                    strMemo = "�ӻ���";

                                if (strMemo == "�ӻ���")    //added by LinYong in 20150325   
                                {
                                    voyageReportBMdataTableSelectMember.AnalyseVoyageReport_MemberInfo();
                                    if (voyageReportBMdataTableSelectMember.MemberInfo != null)
                                    {
                                        DataRow[] dataRowsvoyageReportBMdataTableSelectMember_MemberInfo = voyageReportBMdataTableSelectMember.MemberInfo.Select("cnvcSID = '" + dataRowIndex["cnvcSID"].ToString() + "'");
                                        if (dataRowsvoyageReportBMdataTableSelectMember_MemberInfo.Length > 0)
                                        {
                                            strDepArrStn = dataRowsvoyageReportBMdataTableSelectMember_MemberInfo[0]["cnvcDepArrStn"].ToString();
                                        }
                                    }
                                }

                                voyageReportBMdataTableSelectMember.AnalyseVoyageReport_SegmentInfo();
                                if (voyageReportBMdataTableSelectMember.SegmentInfo != null)
                                {
                                    if (dataTableSelectMember_SegmentInfo.Columns.Count <= 0)
                                    {
                                        dataTableSelectMember_SegmentInfo = voyageReportBMdataTableSelectMember.SegmentInfo.Clone();
                                    }

                                    foreach (DataRow dataRowvoyageReportBMdataTableSelectMember_SegmentInfo in voyageReportBMdataTableSelectMember.SegmentInfo.Rows)
                                    {
                                        if (strMemo == "�ӻ���")    //added by LinYong in 20150325 
                                        {
                                            if (strDepArrStn.IndexOf(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcROUTE"].ToString().Trim()) < 0)
                                                continue;
                                        }

                                        dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcMemo"] = strMemo;   //added by LinYong in 20150325
                                        dataTableSelectMember_SegmentInfo.ImportRow(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo);
                                    }
                                }
                                else
                                {
                                    throw new Exception("������Ա���¶�ִ�к�����Ϣ������ VoyageReportBM.SegmentInfo ʱʧ�ܣ�");
                                }
                            }
                            else
                                throw new Exception("������Ա���¶�ִ�к�����Ϣ������ VoyageReportBM ʱʧ�ܣ�");
                        }

                        //�����¸�����
                        if (dataTableSelectMember_SegmentInfo != null) //�����������㷨�£�������� dataTableSelectMember_SegmentInfo == null �����
                        {
                            //Ĭ�� ���ͬ������ �Ӻ�վ����ϵͳ�д��͹����ļƻ����ʱ��STD �� ��ǰ���������ݱ��еļƻ����ʱ�� ��һ����
                            DataRow[] dataRowsdataTableSelectMember_SegmentInfo = dataTableSelectMember_SegmentInfo.Select(("cnvcSTD > '" + STD.Replace(" ", "  ") + "'"), "cnvcSTD");
                            if (dataRowsdataTableSelectMember_SegmentInfo.Length > 0)
                            {
                                //�Ƿ� �ӻ��� ����  added by LinYong in 20150324
                                if (dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcMemo"].ToString() == "�ӻ���")
                                    dataRowIndex["cnvcExecFlightInfo"] = "�ӻ��飺";

                                //���������Ϣ
                                dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() +    //modified by LinYong in 20150325
                                    "���ʱ�䣺" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " ") + " �� " +
                                    "�ɻ��ţ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString() + " �� " +
                                    "����ţ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcFLTID"].ToString() + " �� " +
                                    "���Σ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcROUTE"].ToString() + " �� " ;

                                //���� �ж�
                                DateTime dtCurrentFlightSTA = Convert.ToDateTime(STA);
                                DateTime dtNextFlightSTD = Convert.ToDateTime(dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " "));
                                if (dtNextFlightSTD <= dtCurrentFlightSTA.AddHours(2))
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "���ɣ��� �� ";
                                else
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "���ɣ��� �� ";

                                //�����ɻ� �ж�
                                if (AC == dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString())
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "�����ɻ����� �� ";
                                else
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "�����ɻ����� �� ";

                            }

                        }
 
                    }
                    else if (dataTableSelectMember.Rows.Count <= 0)
                    {
                        if (dataRowIndex["cnvcPosition"].ToString().IndexOf("�ӻ���") < 0)
                        {
                            throw new Exception("������Ա���¶�ִ�к�����Ϣ�����ڴ����û����ȡ���˳�Ա����������Ϣ��Ӧ���У������ԭ�򣩣�");
                        }
                    }
                    else if (dataTableSelectMember == null)
                    {
                        throw new Exception("������Ա���¶�ִ�к�����Ϣ�����ڴ������ȡ�˳�Ա����������Ϣʱ�����쳣��");
                    }

                }
                catch (Exception ex)
                {
                    dataRowIndex["cnvcMemo"] = dataRowIndex["cnvcMemo"] + "��" + ex.Message + "��";
                }

            }
            #endregion ������Ա���¶�ִ�к�����Ϣ

            #region ���� ͳ����

            string strSIDs = "";
            int multipleBusiness = 0;   //һ�����ڶ����λ
            int countTatal = 0;

            foreach (DataRow dataRowdataTableResult in dataTableResult.Rows)   //����ÿ����Ա����Ա������ dataRowIndex
            {
                if (strSIDs.IndexOf(dataRowdataTableResult["cnvcSID"].ToString()) < 0)
                {
                    strSIDs = strSIDs + dataRowdataTableResult["cnvcSID"].ToString() + "��";
                    countTatal++;
                }
                else
                    multipleBusiness++;
            }

            DataRow dataRowdataTableResult_sum = dataTableResult.NewRow();
            dataRowdataTableResult_sum["cnvcPosition"] = "�ϼ�";
            dataRowdataTableResult_sum["cnvcExecFlightInfo"] = "��������" + countTatal.ToString() + 
                "�����м�ְ������" + multipleBusiness.ToString() 
                + "����������ID��" + strFltReportId + "��";
            dataTableResult.Rows.InsertAt(dataRowdataTableResult_sum, 0);

            #endregion ���� ͳ����


            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTableResult != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTableResult, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion �����ݱ�ѹ����ϵ�л��ɶ�������


            //���ؽ��
            return bResult;


            #endregion ����ʵ��

        }
        #endregion ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���¸�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������byte[] GetVoyageReportDataBySingleFlight_In(...)

        #region ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���ϸ�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������byte[] GetVoyageReportDataBySingleFlight_Out(...)
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
        public byte[] GetVoyageReportDataBySingleFlight_Out(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";


            DataTable dataTableResult = new DataTable();   //���ؽ������񣬽��ۺ��ࣩ
            string strFltReportId = "";     //������ ID������������������ʹ��

            #region ��¼ʹ��
            //������
            string strProcName = "";
            //�������
            string strOprationResult = "";
            //���ô���(SQL + MEM)
            int iOprationCount = 0;
            //������ʼʱ��
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //ѹ��֮ǰ��С������byte��
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //ѹ��֮���С������byte��
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //����ִ��ʱ���������룩
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //����ִ��ǰʱ��
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //����ִ�к�ʱ��
            double fTotalProcTimes = 0;

            //ѹ��ʱ���������룩
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //ѹ��ִ��ǰʱ��
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //ѹ��ִ�к�ʱ��
            double fTotalCompressTimes = 0;
            #endregion ��¼ʹ��

            #endregion ��������


            #region ����ʵ��

            #region ���ɱ��
            DataColumn dataColumn = null;

            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
            dataColumn.AutoIncrement = true;
            dataColumn.AutoIncrementSeed = 1;
            dataColumn.AutoIncrementStep = 1;
            dataColumn.Caption = "���";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "λ��";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "����";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "����";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "����֤��";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "��˺���";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "��ϵ�绰";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "ִ�к��ࣨ�϶Σ�";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "��ע";
            dataTableResult.Columns.Add(dataColumn);
            #endregion ���ɱ��


            #region ��ȡ���ݣ�����������ȡ��ǰ���������ݣ�
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbFltReport)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "(STD like '%" + DATOP + "%') and " +
                        "(FLTIDS like '%" + FLTIDS + "%') and " +
                        "(AC like '%" + AC + "%') and " +
                        "(ROUTES like '%" + ROUTES + "%') ";

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    VoyageReportDAF voyageReportDAF = new VoyageReportDAF();
                    dataTable = voyageReportDAF.GetVoyageReportDataBySingleFlight(DATOP, FLTIDS, AC, ROUTES);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //��¼ʹ��
                strOprationResult = "�ɹ�";
            }
            catch
            {
                dataTable = null;

                //��¼ʹ��
                strOprationResult = "ʧ��";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion ��ȡ���ݣ�����������ȡ��ǰ���������ݣ�

            #region ��¼ cniFltReportId
            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                strFltReportId = dataTable.Rows[0]["cniFltReportId"].ToString();
            }
            #endregion ��¼ cniFltReportId


            #region ��ǰ�������е� MemberInfo ���� ���ؽ������ִ�к���ӻ����й��˵��Ǵ�˴˺��ε���Ա��Ϣ


            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                VoyageReportBM voyageReportBM = new VoyageReportBM(dataTable.Rows[0]);
                if (voyageReportBM.Success)
                {
                    voyageReportBM.AnalyseVoyageReport_MemberInfo();
                    if (voyageReportBM.MemberInfo != null)
                    {
                        foreach (DataRow dataRow in voyageReportBM.MemberInfo.Rows)
                        {
                            if ((dataRow["cnvcDepArrStn"].ToString().Trim() == "") || (dataRow["cnvcDepArrStn"].ToString().Trim().IndexOf( ROUTES) >= 0))
                            {
                                DataRow dataRowdataTableResult = dataTableResult.NewRow();

                                dataRowdataTableResult["cnvcPosition"] = dataRow["cnvcPosition"].ToString();
                                dataRowdataTableResult["cnvcName"] = dataRow["cnvcName"].ToString();
                                dataRowdataTableResult["cnvcLevel"] = dataRow["cnvcLevel"].ToString();
                                dataRowdataTableResult["cnvcSID"] = dataRow["cnvcSID"].ToString();
                                dataRowdataTableResult["cnvcDepArrStn"] = dataRow["cnvcDepArrStn"].ToString();

                                dataTableResult.Rows.Add(dataRowdataTableResult);
                            }
                        }
                    }
                }
            }

            #endregion ��ǰ�������е� MemberInfo ���� ���ؽ������ִ�к���ӻ����й��˵��Ǵ�˴˺��ε���Ա��Ϣ

            #region �����Ա��ϵ��Ϣ
            foreach (DataRow dataRowIndex in dataTableResult.Rows)
            {
                //��ȡ��Ա��ϵ��Ϣ
                DataRow[] dataRowsBasicCrewInfo_Profile = tbBasicCrewInfo_Profile.Select("STAFFID = '" + dataRowIndex["cnvcSID"].ToString().Trim() + "'");
                if (dataRowsBasicCrewInfo_Profile.Length > 0)
                    dataRowIndex["cnvcMOBILE"] = dataRowsBasicCrewInfo_Profile[0]["MOBILE"].ToString();
            }

            #endregion �����Ա��ϵ��Ϣ

            #region ������Ա���϶�ִ�к�����Ϣ
            foreach (DataRow dataRowIndex in dataTableResult.Rows)   //����ÿ����Ա����Ա������ dataRowIndex
            {
                try
                {
                    DataTable dataTableSelectMember = null;     //���� ĳ����Ա �� �ڴ�� �� �� ���������ݣ�ÿ����������ܺ��ж�κ�����Ϣ��

                    strSQL = "(Captain_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SKIPPER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(FIRST_VICE1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(TELER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(PILOT_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(CHIEF_STEWARD_CAPTAIN_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_INSTRUCTOR_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SAFER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CAP1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARDS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +  //added by LinYong in 20150324
                        "(PILOT_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') ";

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTableSelectMember = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);

                    if ((dataTableSelectMember != null) && (dataTableSelectMember.Rows.Count > 0))
                    {

                        DataTable dataTableSelectMember_SegmentInfo = new DataTable(); //���� ĳ����Ա �� �ڴ�������� �� �� ����ִ�к�����Ϣ

                        foreach (DataRow dataRowdataTableSelectMember in dataTableSelectMember.Rows)
                        {
                            VoyageReportBM voyageReportBMdataTableSelectMember = new VoyageReportBM(dataRowdataTableSelectMember);
                            if (voyageReportBMdataTableSelectMember.Success)
                            {
                                string strMemo = "";    //added by LinYong in 20150324
                                string strDepArrStn = "";   //added by LinYong in 20150325

                                if (voyageReportBMdataTableSelectMember.pilot_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)    //added by LinYong in 20150324        
                                    strMemo = "�ӻ���";
                                if (voyageReportBMdataTableSelectMember.steward_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)  //added by LinYong in 20150324
                                    strMemo = "�ӻ���";

                                if (strMemo == "�ӻ���")    //added by LinYong in 20150325   
                                {
                                    voyageReportBMdataTableSelectMember.AnalyseVoyageReport_MemberInfo();
                                    if (voyageReportBMdataTableSelectMember.MemberInfo != null)
                                    {
                                        DataRow[] dataRowsvoyageReportBMdataTableSelectMember_MemberInfo = voyageReportBMdataTableSelectMember.MemberInfo.Select("cnvcSID = '" + dataRowIndex["cnvcSID"].ToString() + "'");
                                        if (dataRowsvoyageReportBMdataTableSelectMember_MemberInfo.Length > 0)
                                        {
                                            strDepArrStn = dataRowsvoyageReportBMdataTableSelectMember_MemberInfo[0]["cnvcDepArrStn"].ToString();
                                        }
                                    }
                                }

                                voyageReportBMdataTableSelectMember.AnalyseVoyageReport_SegmentInfo();
                                if (voyageReportBMdataTableSelectMember.SegmentInfo != null)
                                {
                                    if (dataTableSelectMember_SegmentInfo.Columns.Count <= 0)
                                    {
                                        dataTableSelectMember_SegmentInfo = voyageReportBMdataTableSelectMember.SegmentInfo.Clone();
                                    }

                                    foreach (DataRow dataRowvoyageReportBMdataTableSelectMember_SegmentInfo in voyageReportBMdataTableSelectMember.SegmentInfo.Rows)
                                    {
                                        if (strMemo == "�ӻ���")    //added by LinYong in 20150325 
                                        {
                                            if (strDepArrStn.IndexOf(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcROUTE"].ToString().Trim()) < 0)
                                                continue;
                                        }

                                        dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcMemo"] = strMemo;   //added by LinYong in 20150324
                                        dataTableSelectMember_SegmentInfo.ImportRow(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo);
                                    }
                                }
                                else
                                {
                                    throw new Exception("������Ա���¶�ִ�к�����Ϣ������ VoyageReportBM.SegmentInfo ʱʧ�ܣ�");
                                }
                            }
                            else
                                throw new Exception("������Ա���¶�ִ�к�����Ϣ������ VoyageReportBM ʱʧ�ܣ�");
                        }

                        //���� �ƻ�����ʱ��
                        if (dataTableSelectMember_SegmentInfo != null) //�����������㷨�£�������� dataTableSelectMember_SegmentInfo == null �����
                        {
                            //�� dataTableSelectMember_SegmentInfo �� ���� �ƻ�����ʱ�� ��
                            dataColumn = new DataColumn("cnvcSTA", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����ʱ��";
                            dataTableSelectMember_SegmentInfo.Columns.Add(dataColumn);

                            //
                            foreach (DataRow dataRowdataTableSelectMember_SegmentInfo in dataTableSelectMember_SegmentInfo.Rows)
                            {
                                string strSelect = "(cncFlightDate = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcDATOP"].ToString() + "') and " +
                                    "(cnvcFlightNo_Cal = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcFLTID"].ToString() + "') and " +
                                    "(cnvcLONG_REG = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcAC"].ToString() + "') and " +
                                    "(cnvcROUTE_Cal = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcROUTE"].ToString() + "') " ;

                                DataRow[] dataRowstbLegs = tbLegs.Select(strSelect);

                                if (dataRowstbLegs.Length == 1)
                                {
                                    //����ʱ��ָ��� �� ���ո� ��Ϊ "  "
                                    dataRowdataTableSelectMember_SegmentInfo["cnvcSTA"] = dataRowstbLegs[0]["cncSTA"].ToString().Substring(0, 16).Replace(" ", "  ");
                                }
                                else
                                    continue;
                            }

                        }

                        //�����ϸ����ࣨ�ͽ��ۺ������㺯���в���ĵط���
                        if (dataTableSelectMember_SegmentInfo != null) //�����������㷨�£�������� dataTableSelectMember_SegmentInfo == null �����
                        {
                            //Ĭ�� ���ͬ������ �Ӻ�վ����ϵͳ�д��͹����ļƻ����ʱ��STD �� ��ǰ���������ݱ��еļƻ����ʱ�� ��һ����
                            DataRow[] dataRowsdataTableSelectMember_SegmentInfo = dataTableSelectMember_SegmentInfo.Select(("cnvcSTD < '" + STD.Replace(" ", "  ") + "'"), "cnvcSTD desc");
                            if (dataRowsdataTableSelectMember_SegmentInfo.Length > 0)
                            {
                                //�Ƿ� �ӻ��� ����  added by LinYong in 20150324
                                if (dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcMemo"].ToString() == "�ӻ���")
                                    dataRowIndex["cnvcExecFlightInfo"] = "�ӻ��飺";

                                //���������Ϣ
                                if (dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTA"].ToString().Length > 16)
                                {
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() +    //modified by LinYong in 20150324
                                        "��ʱ�䣺" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " ") +
                                        " - " +
                                        dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTA"].ToString().Substring(12, 5) + " �� " +
                                        "�ɻ��ţ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString() + " �� " +
                                        "����ţ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcFLTID"].ToString() + " �� " +
                                        "���Σ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcROUTE"].ToString() + " �� ";
                                }
                                else
                                {
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() +    //modified by LinYong in 20150324 
                                        "��ʱ�䣺" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " ") +
                                        " - " +
                                        " �� " +
                                        "�ɻ��ţ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString() + " �� " +
                                        "����ţ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcFLTID"].ToString() + " �� " +
                                        "���Σ�" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcROUTE"].ToString() + " �� ";

                                }

                                //���� �ж�
                                try
                                {
                                    DateTime dtCurrentFlightSTD = Convert.ToDateTime(STD);
                                    DateTime dtPreFlightSTA = Convert.ToDateTime(dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTA"].ToString().Replace("  ", " "));
                                    if (dtCurrentFlightSTD <= dtPreFlightSTA.AddHours(2))
                                        dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "���ɣ��� �� ";
                                    else
                                        dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "���ɣ��� �� ";
                                }
                                catch (Exception ex)
                                {
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "���ɣ�  �� ";
                                }

                                //�����ɻ� �ж�
                                if (AC == dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString())
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "�����ɻ����� �� ";
                                else
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "�����ɻ����� �� ";

                            }

                        }

                    }
                    else if (dataTableSelectMember.Rows.Count <= 0)
                    {
                        if (dataRowIndex["cnvcPosition"].ToString().IndexOf("�ӻ���") < 0)
                        {
                            throw new Exception("������Ա���϶�ִ�к�����Ϣ�����ڴ����û����ȡ���˳�Ա����������Ϣ��Ӧ���У������ԭ�򣩣�");
                        }
                    }
                    else if (dataTableSelectMember == null)
                    {
                        throw new Exception("������Ա���϶�ִ�к�����Ϣ�����ڴ������ȡ�˳�Ա����������Ϣʱ�����쳣��");
                    }

                }
                catch (Exception ex)
                {
                    dataRowIndex["cnvcMemo"] = dataRowIndex["cnvcMemo"] + "��" + ex.Message + "��";
                }

            }
            #endregion ������Ա���϶�ִ�к�����Ϣ

            #region ���� ͳ����

            string strSIDs = "";
            int multipleBusiness = 0;   //һ�����ڶ����λ
            int countTatal = 0;

            foreach (DataRow dataRowdataTableResult in dataTableResult.Rows)   //����ÿ����Ա����Ա������ dataRowIndex
            {
                if (strSIDs.IndexOf(dataRowdataTableResult["cnvcSID"].ToString()) < 0)
                {
                    strSIDs = strSIDs + dataRowdataTableResult["cnvcSID"].ToString() + "��";
                    countTatal++;
                }
                else
                    multipleBusiness++;
            }

            DataRow dataRowdataTableResult_sum = dataTableResult.NewRow();
            dataRowdataTableResult_sum["cnvcPosition"] = "�ϼ�";
            dataRowdataTableResult_sum["cnvcExecFlightInfo"] = "��������" + countTatal.ToString() 
                + "�����м�ְ������" + multipleBusiness.ToString()
                + "����������ID��" + strFltReportId + "��";
            dataTableResult.Rows.InsertAt(dataRowdataTableResult_sum, 0);

            #endregion ���� ͳ����


            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTableResult != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTableResult, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion �����ݱ�ѹ����ϵ�л��ɶ�������


            //���ؽ��
            return bResult;


            #endregion ����ʵ��

        }
        #endregion ���ۺ���ĺ�ǰ������Ľ�����Ϣ����Ա����ϵ��ʽ���¸�������Ϣ���Ƿ����ɡ��Ƿ�����ɻ�������byte[] GetVoyageReportDataBySingleFlight_Out(...)



        #endregion ����Ĺ���


        #region ���ݲ�ѯ�����ڴ����ȡ����[��� strTableName ��ʾ�����ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ����]
        /// <summary>
        /// ���ݲ�ѯ�����ڴ����ȡ����
        /// </summary>
        /// <param name="strTableName">�ڴ����</param>
        /// <param name="strSQL">��ѯ���</param>
        /// <param name="strSort">�������</param>
        /// <param name="strFilterField">��Ҫ��ȡ���ֶΣ���",column1,column2,column3,"</param>
        /// <returns>��ѯ�Ľ�������󷵻� null</returns>
        public DataTable GetDataBySQL(string strTableName, string strSQL, string strSort, string strFilterField)
        {
            #region ��������
            DataTable dataTable = null;
            DataRow[] dataRows = null;

            #endregion


            #region ����ʵ��
            try
            {
                //tbFltReport
                if (strTableName == "tbFltReport")
                {
                    dataTable = tbFltReport.Clone();
                    dataRows = tbFltReport.Select(strSQL, strSort);
                }
                //tbBasicCrewInfo_Profile
                else if (strTableName == "tbBasicCrewInfo_Profile")
                {
                    dataTable = tbBasicCrewInfo_Profile.Clone();
                    dataRows = tbBasicCrewInfo_Profile.Select(strSQL, strSort);
                }


                //�����ݵ��� dataTable
                foreach (DataRow dataRow in dataRows)
                {
                    dataTable.ImportRow(dataRow);
                }

                //�� dataTable �в���Ҫ���ֶ�ɾ��
                if (strFilterField != "")
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        if (strFilterField.IndexOf(("," + dataColumn.ColumnName + ","), StringComparison.InvariantCultureIgnoreCase) < 0)
                            dataTable.Columns.Remove(dataColumn);
                    }
                }

            }
            catch
            {
                dataTable = null;
            }

            //���ؽ��
            return dataTable;

            #endregion
        }
        #endregion




    }
}
