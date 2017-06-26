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


namespace AirSoft.FlightMonitor.AgentServiceDA
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
    public class AgentServiceDAF : MarshalByRefObject
    {
        #region Զ�̶���
        static public AgentServiceDAF objRemotingObject = null;
        #endregion

        #region �ڴ����ݱ�������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ�������� GetDataBySQL ���̡�
        static public DataTable tbLegs = null;  //������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ�������� GetDataBySQL ���̡�
        static public DataTable vw_Legs = null; //������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ�������� GetDataBySQL ���̡�
        static public DataTable vw_FlightChangeRecord = null;   //������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ�������� GetDataBySQL ���̡�

        #endregion

        #region ������æ��־
        static public bool blnBusy_tbLegs = false;
        static public bool blnBusy_vw_Legs = false;
        static public bool blnBusy_vw_FlightChangeRecord = false;

        #endregion

        #region ��¼��
        //
        static public bool blnRecord = true;    //�Ƿ���Ҫ��¼��true ��Ҫ��false ����Ҫ
        //
        static public DataTable dtProcRecords = null;       //���̼�¼��
        static public DataTable dtProcAnalysis = null;      //���̷�����
        static public DataTable dtOnLineUsers = null;       //�����û��� procRecordsDAF.AddRecord
        //
        private static object objProcRecordsDAF_AddRecord__Lock = new object(); //ProcRecordsDAF.AddRecord ��ͬ����
        private static object objProcAnalysisDAF_UpdateRecord__Lock = new object(); //ProcAnalysisDAF.UpdateRecord ��ͬ����
        private static object objOnLineUsersDAF_RefreshOnLineUsersInfo__Lock = new object();    //OnLineUsersDAF.RefreshOnLineUsersInfo ��ͬ����

        #region ChangeLegsDAF.GetFlightByKey
        //������
        private static object objChangeLegsDAF_GetFlightByKey__SQL__Lock = new object();
        private static object objChangeLegsDAF_GetFlightByKey__MEM__Lock = new object();
        //���ô���
        static public int iChangeLegsDAF_GetFlightByKey__SQL__OprationCount = 0;
        static public int iChangeLegsDAF_GetFlightByKey__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dChangeLegsDAF_GetFlightByKey__SQL__CountStartTime;
        static public DateTime dChangeLegsDAF_GetFlightByKey__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes = 0;
        static public double fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes = 0;
        static public double fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes = 0;

        #endregion

        #region ChangeRecordDAF.GetLastGuaranteeChangeRecords
        //������
        private static object objChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__Lock = new object();
        private static object objChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__Lock = new object();
        //���ô���
        static public int iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount = 0;
        static public int iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__CountStartTime;
        static public DateTime dChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes = 0;
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes = 0;
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes = 0;

        #endregion

        #region ChangeRecordDAF.GetMaxRecordNo
        //������
        private static object objChangeRecordDAF_GetMaxRecordNo__SQL__Lock = new object();
        private static object objChangeRecordDAF_GetMaxRecordNo__MEM__Lock = new object();
        //���ô���
        static public int iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount = 0;
        static public int iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dChangeRecordDAF_GetMaxRecordNo__SQL__CountStartTime;
        static public DateTime dChangeRecordDAF_GetMaxRecordNo__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes = 0;
        static public double fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes = 0;
        static public double fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes = 0;

        #endregion

        #region ChangeRecordDAF.GetChangeRecords
        //������
        private static object objChangeRecordDAF_GetChangeRecords__SQL__Lock = new object();
        private static object objChangeRecordDAF_GetChangeRecords__MEM__Lock = new object();
        //���ô���
        static public int iChangeRecordDAF_GetChangeRecords__SQL__OprationCount = 0;
        static public int iChangeRecordDAF_GetChangeRecords__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dChangeRecordDAF_GetChangeRecords__SQL__CountStartTime;
        static public DateTime dChangeRecordDAF_GetChangeRecords__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes = 0;
        static public double fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes = 0;
        static public double fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes = 0;

        #endregion

        #region GuaranteeInforDAF.GetFlightsByStation
        //������
        private static object objGuaranteeInforDAF_GetFlightsByStation__SQL__Lock = new object();
        private static object objGuaranteeInforDAF_GetFlightsByStation__MEM__Lock = new object();
        //���ô���
        static public int iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount = 0;
        static public int iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dGuaranteeInforDAF_GetFlightsByStation__SQL__CountStartTime;
        static public DateTime dGuaranteeInforDAF_GetFlightsByStation__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes = 0;

        #endregion

        #region GuaranteeInforDAF.GetFlightByKey
        //������
        private static object objGuaranteeInforDAF_GetFlightByKey__SQL__Lock = new object();
        private static object objGuaranteeInforDAF_GetFlightByKey__MEM__Lock = new object();
        //���ô���
        static public int iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount = 0;
        static public int iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime;
        static public DateTime dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes = 0;

        #endregion

        #region GuaranteeInforDAF.GetFlightsByMessage
        //������
        private static object objGuaranteeInforDAF_GetFlightsByMessage__SQL__Lock = new object();
        private static object objGuaranteeInforDAF_GetFlightsByMessage__MEM__Lock = new object();
        //���ô���
        static public int iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount = 0;
        static public int iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dGuaranteeInforDAF_GetFlightsByMessage__SQL__CountStartTime;
        static public DateTime dGuaranteeInforDAF_GetFlightsByMessage__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes = 0;

        #endregion


        #region MEM.GettbLegs ��ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����
        //������
        private static object objMEM_GettbLegs__SQL__Lock = new object();
        private static object objMEM_GettbLegs__MEM__Lock = new object();
        //���ô���
        static public int iMEM_GettbLegs__SQL__OprationCount = 0;
        static public int iMEM_GettbLegs__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dMEM_GettbLegs__SQL__CountStartTime;
        static public DateTime dMEM_GettbLegs__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lMEM_GettbLegs__SQL__TotalLengthBeforeCompress = 0;
        static public long lMEM_GettbLegs__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lMEM_GettbLegs__SQL__TotalLengthAfterCompress = 0;
        static public long lMEM_GettbLegs__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fMEM_GettbLegs__SQL__TotalProcTimes = 0;
        static public double fMEM_GettbLegs__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fMEM_GettbLegs__SQL__TotalCompressTimes = 0;
        static public double fMEM_GettbLegs__MEM__TotalCompressTimes = 0;

        #endregion MEM.GettbLegs ��ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����

        #region MEM.Getvw_Legs ��ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е�����
        //������
        private static object objMEM_Getvw_Legs__SQL__Lock = new object();
        private static object objMEM_Getvw_Legs__MEM__Lock = new object();
        //���ô���
        static public int iMEM_Getvw_Legs__SQL__OprationCount = 0;
        static public int iMEM_Getvw_Legs__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dMEM_Getvw_Legs__SQL__CountStartTime;
        static public DateTime dMEM_Getvw_Legs__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress = 0;
        static public long lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress = 0;
        static public long lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fMEM_Getvw_Legs__SQL__TotalProcTimes = 0;
        static public double fMEM_Getvw_Legs__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fMEM_Getvw_Legs__SQL__TotalCompressTimes = 0;
        static public double fMEM_Getvw_Legs__MEM__TotalCompressTimes = 0;

        #endregion MEM.Getvw_Legs ��ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е����� 

        #region MEM.Getvw_FlightChangeRecord ��ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е�����
        //������
        private static object objMEM_Getvw_FlightChangeRecord__SQL__Lock = new object();
        private static object objMEM_Getvw_FlightChangeRecord__MEM__Lock = new object();
        //���ô���
        static public int iMEM_Getvw_FlightChangeRecord__SQL__OprationCount = 0;
        static public int iMEM_Getvw_FlightChangeRecord__MEM__OprationCount = 0;
        //������ʼʱ��
        static public DateTime dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime;
        static public DateTime dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime;
        //ѹ��֮ǰ��С������byte��
        static public long lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress = 0;
        static public long lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress = 0;
        //ѹ��֮���С������byte��
        static public long lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress = 0;
        static public long lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress = 0;
        //����ִ��ʱ���������룩
        static public double fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes = 0;
        static public double fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes = 0;
        //ѹ��ʱ���������룩
        static public double fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes = 0;
        static public double fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes = 0;

        #endregion MEM.Getvw_FlightChangeRecord ��ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е����� 

        #endregion


        #region ����Ĺ���

        #region ChangeLegsDAF

        #region ������Ϊ������ѯһ����¼ GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        /// <summary>
        /// ������Ϊ������ѯһ����¼
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <returns>ϵ�л���ѹ��������ݱ��������</returns>
        public byte[] GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "" ;
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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
            long lTotalLengthAfterCompress = 0 ;
            //����ִ��ʱ���������룩
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //����ִ��ǰʱ��
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //����ִ�к�ʱ��
            double fTotalProcTimes = 0 ;

            //ѹ��ʱ���������룩
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //ѹ��ִ��ǰʱ��
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //ѹ��ִ�к�ʱ��
            double fTotalCompressTimes = 0;

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbLegs)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetFlightByKey[ChangeLegsDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = " cncDATOP = '" + changeLegsBM.DATOP + "' AND " +
                            " cnvcFLTID = '" + changeLegsBM.FLTID  + "' AND " +
                            " cniLEGNO = " + changeLegsBM.LEGNO + " AND " +
                            " cnvcAC = '" + changeLegsBM.AC  + "' ";
                    strSort = "";
                    strFilterField = ",cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cncFlightDate,cncCKIFlightDate,cnvcFlightNo,cnvcCKIFlightNo,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncSTA,cncSTATUS,cncETD,cncETA,cncATD,cncTOFF,cncTDWN,cncATA,cnvcTRI_FLTID,cnvcDIV_RCODE,cnvcDIV_FLAG,cnvcPAX,cnvcBOOK,cnvcDELAY1,cniDUR1,cnvcDELAY2,cniDUR2,cnvcDELAY3,cniDUR3,cnvcDELAY4,cniDUR4,cnvcGATE,cnvcSTC,cnvcVERSION,cncORIG_ACTYP,cncACTYP,cnvcACOWN,cnvcSEQ,cncInsertTime,cniDeleteTag,";
                    strFilterField = strFilterField.Replace(" ","");

                    dataTable = GetDataBySQL("tbLegs", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetFlightByKey[ChangeLegsDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                    dataTable = changeLegsDAF.GetFlightByKey(changeLegsBM);
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objChangeLegsDAF_GetFlightByKey__MEM__Lock)
                    {
                        iOprationCount = iChangeLegsDAF_GetFlightByKey__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeLegsDAF_GetFlightByKey__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes;

                        iChangeLegsDAF_GetFlightByKey__MEM__OprationCount++;
                        if (iChangeLegsDAF_GetFlightByKey__MEM__OprationCount == 0)
                            dChangeLegsDAF_GetFlightByKey__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress =
                            lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress =
                            lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes =
                            fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes =
                            fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeLegsDAF_GetFlightByKey__SQL__Lock)
                    {
                        iOprationCount = iChangeLegsDAF_GetFlightByKey__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeLegsDAF_GetFlightByKey__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes;

                        iChangeLegsDAF_GetFlightByKey__SQL__OprationCount++;
                        if (iChangeLegsDAF_GetFlightByKey__SQL__OprationCount == 0)
                            dChangeLegsDAF_GetFlightByKey__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress =
                            lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress =
                            lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes =
                            fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes =
                            fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion
        }
        #endregion

        #endregion

        #region ChangeRecordDAF

        #region ��վ��ȡ���һ��������� GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public byte[] GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //��ʼ�ͽ�����¼��
            string strRecordNo_Start = "", strRecordNo_End = "";

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_vw_FlightChangeRecord)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetLastGuaranteeChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "cniRecordNo > " + iLastRecordNo.ToString() + " AND " +
                        "(cncOldDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncOldArrSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewArrSTN = '" + stationBM.ThreeCode + "') AND " +
                        "(cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "' OR " +
                        "cncETA >= '" + dateTimeBM.StartDateTime + "' AND cncETA < '" + dateTimeBM.EndDateTime + "')";
                    strSort = "cniRecordNo";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_FlightChangeRecord", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetLastGuaranteeChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                    dataTable = changeRecordDAF.GetLastGuaranteeChangeRecords(iLastRecordNo,
                        dateTimeBM, stationBM, accountBM);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //��¼��
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    strRecordNo_Start = dataTable.Rows[0]["cniRecordNo"].ToString();
                    strRecordNo_End = dataTable.Rows[dataTable.Rows.Count - 1]["cniRecordNo"].ToString();
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes;

                        iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount++;
                        if (iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount == 1)
                            dChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes;

                        iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount++;
                        if (iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount == 1)
                            dChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo��" + strRecordNo_Start + "-" + strRecordNo_End + "]"), 
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo��" + strRecordNo_Start + "-" + strRecordNo_End + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);

                OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                onLineUsersDAF.RefreshOnLineUsersInfo(dtOnLineUsers, accountBM, SynchronizeObjectsBM.AgentServiceDAF_dtOnLineUsers__Lock);
            }
            catch(Exception ex)
            {
                SysMsgBM.TraceInfo_GetLastGuaranteeChangeRecords_1 = "[" + DateTime.Now.ToString() + "]" + ex.Message;
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion
        }
        #endregion

        #region ��ȡ�������� GetMaxRecordNo(AccountBM accountBM)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int GetMaxRecordNo(AccountBM accountBM)
        {
            #region ��������
            //
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";
            int iResult = -1;

            //��¼ʹ��
            string strProcName = ""; //������
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

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_vw_FlightChangeRecord)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetMaxRecordNo[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "cniRecordNo = max(cniRecordNo)";
                    strSort = "cniRecordNo desc";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_FlightChangeRecord", strSQL, strSort, strFilterField);
                    if ((dataTable != null) && (dataTable.Rows.Count > 0))
                        iResult = Convert.ToInt32(dataTable.Rows[0]["cniRecordNo"].ToString());
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (iResult == -1)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetMaxRecordNo[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                    iResult = Convert.ToInt32(changeRecordDAF.GetMaxRecordNo());
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
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objChangeRecordDAF_GetMaxRecordNo__MEM__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetMaxRecordNo__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes;

                        iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount++;
                        if (iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount == 1)
                            dChangeRecordDAF_GetMaxRecordNo__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes =
                            fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes =
                            fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeRecordDAF_GetMaxRecordNo__SQL__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetMaxRecordNo__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes;

                        iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount++;
                        if (iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount == 1)
                            dChangeRecordDAF_GetMaxRecordNo__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes =
                            fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes =
                            fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);

            }
            catch (Exception ex)
            {
            }
            #endregion


            //���ؽ��
            return iResult;

            #endregion

        }
        #endregion

        #region ��ȡ��վ����100�������¼ GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLastRecordNo"></param>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public byte[] GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //��ʼ�ͽ�����¼��
            string strRecordNo_Start = "", strRecordNo_End = "";

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_vw_FlightChangeRecord)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "cniRecordNo > " + iLastRecordNo.ToString() + " AND " +
                        "(cncOldDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncOldArrSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewArrSTN = '" + stationBM.ThreeCode + "') AND " +
                        "(cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "' OR " +
                        "cncETA >= '" + dateTimeBM.StartDateTime + "' AND cncETA < '" + dateTimeBM.EndDateTime + "')";
                    strSort = "cniRecordNo desc";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_FlightChangeRecord", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                    dataTable = changeRecordDAF.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //��¼��
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    strRecordNo_Start = dataTable.Rows[dataTable.Rows.Count - 1]["cniRecordNo"].ToString();
                    strRecordNo_End = dataTable.Rows[0]["cniRecordNo"].ToString();
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objChangeRecordDAF_GetChangeRecords__MEM__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetChangeRecords__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetChangeRecords__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes;

                        iChangeRecordDAF_GetChangeRecords__MEM__OprationCount++;
                        if (iChangeRecordDAF_GetChangeRecords__MEM__OprationCount == 1)
                            dChangeRecordDAF_GetChangeRecords__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes =
                            fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes =
                            fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeRecordDAF_GetChangeRecords__SQL__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetChangeRecords__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetChangeRecords__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes;

                        iChangeRecordDAF_GetChangeRecords__SQL__OprationCount++;
                        if (iChangeRecordDAF_GetChangeRecords__SQL__OprationCount == 1)
                            dChangeRecordDAF_GetChangeRecords__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes =
                            fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes =
                            fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo��" + strRecordNo_Start + "-" + strRecordNo_End + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo��" + strRecordNo_Start + "-" + strRecordNo_End + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);

            }
            catch (Exception ex)
            {
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion
        }
        #endregion

        #endregion

        #region GuaranteeInforDAF

        #region ��ȡĳ��վ�Ľ����ۺ���
        /// <summary>
        /// ��ȡĳ��վ�Ľ����ۺ���
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="stationBM">��վ����ʵ�����</param>
        /// <param name="accountBM">��½�ʺ�ʵ�����</param>
        /// <returns>�ú�վ�����к���</returns>
        public byte[] GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_vw_Legs)
                {
                    //��¼ʹ�� 
                    strProcName = "(��)GetFlightsByStation[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "(cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "' OR " +
                        "cncETA >= '" + dateTimeBM.StartDateTime + "' AND cncETA < '" + dateTimeBM.EndDateTime + "') AND cniDeleteTag = 0 AND cncSTATUS <> 'CNL' AND " +
                        "(cncDEPSTN = '" + stationBM.ThreeCode + "' OR cncARRSTN = '" + stationBM.ThreeCode + "') ";
                    strSort = "cnvcLONG_REG, cncETD";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetFlightsByStation[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //

                    GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                    dataTable = guaranteeInforDAF.GetFlightsByStation(dateTimeBM, stationBM);
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightsByStation__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByStation__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByStation__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightsByStation__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByStation__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByStation__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM,SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion

        }
        #endregion

        #region ������Ϊ������ѯһ����¼����������������������GetFlightByKey(ChangeLegsBM changeLegsBM) ����Ϊ GetFlightByKey(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        /// <summary>
        /// ������Ϊ������ѯһ����¼����������������������GetFlightByKey(ChangeLegsBM changeLegsBM) ����Ϊ GetFlightByKey(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <param name="accountBM">��½�ʺ�ʵ�����</param>
        /// <returns></returns>
        public byte[] GetFlightByKey(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_vw_Legs)
                {
                    //��¼ʹ�� 
                    strProcName = "(��)GetFlightByKey[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = " cncDATOP = '" + changeLegsBM.DATOP + "' AND " +
                            " cnvcFLTID = '" + changeLegsBM.FLTID + "' AND " +
                            " cniLEGNO = " + changeLegsBM.LEGNO + " AND " +
                            " cnvcAC = '" + changeLegsBM.AC + "'";
                    strSort = "";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetFlightByKey[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //

                    GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                    dataTable = guaranteeInforDAF.GetFlightByKey(changeLegsBM);
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion

        }
        #endregion

        #region ������Ϊ������ѯһ����¼����������������������GetFlightByKey(ChangeLegsBM changeLegsBM) ����Ϊ DataTable GetFlightByKey_NotCompress(ChangeLegsBM changeLegsBM, AccountBM accountBM)�����ص����ݲ�����ѹ��
        /// <summary>
        /// ������Ϊ������ѯһ����¼����������������������GetFlightByKey(ChangeLegsBM changeLegsBM) ����Ϊ DataTable GetFlightByKey_NotCompress(ChangeLegsBM changeLegsBM, AccountBM accountBM)�����ص����ݲ�����ѹ��
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <param name="accountBM">��½�ʺ�ʵ�����</param>
        /// <returns></returns>
        public DataTable GetFlightByKey_NotCompress(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //�������ݱ�ļ�¼��
            int iRecordCount = 0;

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_vw_Legs)
                {
                    //��¼ʹ�� 
                    strProcName = "(��)GetFlightByKey[GuaranteeInforDAF][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = " cncDATOP = '" + changeLegsBM.DATOP + "' AND " +
                            " cnvcFLTID = '" + changeLegsBM.FLTID + "' AND " +
                            " cniLEGNO = " + changeLegsBM.LEGNO + " AND " +
                            " cnvcAC = '" + changeLegsBM.AC + "'";
                    strSort = "";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);

                    //�������ݱ�ļ�¼��
                    if (dataTable != null)
                        iRecordCount = dataTable.Rows.Count;
                    else
                        iRecordCount = 0;

                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //�������ݿ�
                if (dataTable == null)
                {
                    //��¼ʹ��
                    strProcName = "(��)GetFlightByKey[GuaranteeInforDAF][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //

                    GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                    dataTable = guaranteeInforDAF.GetFlightByKey(changeLegsBM);

                    //�������ݱ�ļ�¼��
                    if (dataTable != null)
                        iRecordCount = dataTable.Rows.Count;
                    else
                        iRecordCount = 0;

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

            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ������� -- ��ʹ��
            ////�����ݱ�ѹ����ϵ�л��ɶ�������
            //if (dataTable != null)
            //{
            //    //��¼ʹ��
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //��¼ʹ��
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[��¼������" + iRecordCount.ToString() + "][" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return dataTable;

            #endregion

        }
        #endregion

        #region ���ݱ��Ľ�������Ϣȷ������
        /// <summary>
        /// ���ݱ��Ľ�������Ϣȷ������
        /// </summary>
        /// <param name="FlightNo">�����</param>
        /// <param name="ST">�ƻ����ʱ�䣨IO��OUT�����ƻ�����ʱ�䣨IO��IN��</param>
        /// <param name="STN">��ɻ�����IO��OUT�������������IO��IN��</param>
        /// <param name="IO">���ۺ��ࣨIO��OUT�������ۺ��ࣨIO��IN��</param>
        /// <returns>ȷ���ĺ���</returns>
        public DataTable GetFlightsByMessage(string FlightNo, string ST, string STN, string IO)
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_vw_Legs)
                {
                    //��¼ʹ�� 
                    strProcName = "(��)GetFlightsByMessage[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    if (IO == "OUT")
                    {
                        strSQL = "(cnvcFlightNo = '" + FlightNo + "') and (cncSTD = '" + ST + "') and (cncDEPSTN = '" + STN + "')";
                    }
                    else
                    {
                        strSQL = "(cnvcFlightNo = '" + FlightNo + "') and (cncSTA = '" + ST + "') and (cncARRSTN = '" + STN + "')";
                    }
                    strSort = "";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //
                if (dataTable == null)
                {
                    throw new Exception("��ѯ�ڴ��ȡ����ʧ�ܣ�");
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
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightsByMessage__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByMessage__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByMessage__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightsByMessage__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByMessage__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByMessage__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }

                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult,
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult,
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return dataTable;

            #endregion

        }
        #endregion ���ݱ��Ľ�������Ϣȷ������

        #endregion


        #region MEM����ȡ AgentServiceDAF�� �� tbLegs��vw_Legs��vw_FlightChangeRecord�ֶ� �����е�����
        #region byte[] GettbLegs()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����
        /// <summary>
        /// byte[] GettbLegs()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����
        /// </summary>
        /// <returns>ϵ�л���ѹ��������ݱ��������</returns>
        public byte[] GettbLegs()
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //
            string strRecordCount = ""; //���ݱ������

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbLegs)
                {
                    //��¼ʹ��
                    strProcName = "(��)GettbLegs[MEM]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = tbLegs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objMEM_GettbLegs__MEM__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__MEM__TotalCompressTimes;

                        iMEM_GettbLegs__MEM__OprationCount++;
                        if (iMEM_GettbLegs__MEM__OprationCount == 0)
                            dMEM_GettbLegs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__MEM__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__MEM__TotalLengthAfterCompress =
                            lMEM_GettbLegs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__MEM__TotalProcTimes =
                            fMEM_GettbLegs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__MEM__TotalCompressTimes =
                            fMEM_GettbLegs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_GettbLegs__SQL__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__SQL__TotalCompressTimes;

                        iMEM_GettbLegs__SQL__OprationCount++;
                        if (iMEM_GettbLegs__SQL__OprationCount == 0)
                            dMEM_GettbLegs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__SQL__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__SQL__TotalLengthAfterCompress =
                            lMEM_GettbLegs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__SQL__TotalProcTimes =
                            fMEM_GettbLegs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__SQL__TotalCompressTimes =
                            fMEM_GettbLegs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[��¼����" + strRecordCount + "��]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion
        }
        #endregion byte[] GettbLegs()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е�����

        #region DataTable GettbLegs_NotCompress()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// <summary>
        /// DataTable GettbLegs_NotCompress()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// </summary>
        /// <returns>���ص����ݱ�</returns>
        public DataTable GettbLegs_NotCompress()
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //
            string strRecordCount = ""; //���ݱ������

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbLegs)
                {
                    //��¼ʹ��
                    strProcName = "(��)GettbLegs[MEM][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = tbLegs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //
                if (dataTable == null)
                    throw new Exception("dataTable == null");

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
            #endregion

            //#region �����ݱ�ѹ����ϵ�л��ɶ�������
            ////�����ݱ�ѹ����ϵ�л��ɶ�������
            //if (dataTable != null)
            //{
            //    //��¼ʹ��
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //��¼ʹ��
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            //#endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objMEM_GettbLegs__MEM__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__MEM__TotalCompressTimes;

                        iMEM_GettbLegs__MEM__OprationCount++;
                        if (iMEM_GettbLegs__MEM__OprationCount == 0)
                            dMEM_GettbLegs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__MEM__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__MEM__TotalLengthAfterCompress =
                            lMEM_GettbLegs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__MEM__TotalProcTimes =
                            fMEM_GettbLegs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__MEM__TotalCompressTimes =
                            fMEM_GettbLegs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_GettbLegs__SQL__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__SQL__TotalCompressTimes;

                        iMEM_GettbLegs__SQL__OprationCount++;
                        if (iMEM_GettbLegs__SQL__OprationCount == 0)
                            dMEM_GettbLegs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__SQL__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__SQL__TotalLengthAfterCompress =
                            lMEM_GettbLegs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__SQL__TotalProcTimes =
                            fMEM_GettbLegs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__SQL__TotalCompressTimes =
                            fMEM_GettbLegs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[��¼����" + strRecordCount + "��]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return dataTable;

            #endregion
        }
        #endregion DataTable GettbLegs_NotCompress()����ȡ AgentServiceDAF�� �� tbLegs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��


        #region byte[] Getvw_Legs()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е�����
        /// <summary>
        /// byte[] Getvw_Legs()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е�����
        /// </summary>
        /// <returns>ϵ�л���ѹ��������ݱ��������</returns>
        public byte[] Getvw_Legs()
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //
            string strRecordCount = ""; //���ݱ������

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbLegs)
                {
                    //��¼ʹ��
                    strProcName = "(��)Getvw_Legs[MEM]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_Legs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objMEM_Getvw_Legs__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__MEM__TotalCompressTimes;

                        iMEM_Getvw_Legs__MEM__OprationCount++;
                        if (iMEM_Getvw_Legs__MEM__OprationCount == 0)
                            dMEM_Getvw_Legs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__MEM__TotalProcTimes =
                            fMEM_Getvw_Legs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__MEM__TotalCompressTimes =
                            fMEM_Getvw_Legs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_Legs__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__SQL__TotalCompressTimes;

                        iMEM_Getvw_Legs__SQL__OprationCount++;
                        if (iMEM_Getvw_Legs__SQL__OprationCount == 0)
                            dMEM_Getvw_Legs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__SQL__TotalProcTimes =
                            fMEM_Getvw_Legs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__SQL__TotalCompressTimes =
                            fMEM_Getvw_Legs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[��¼����" + strRecordCount + "��]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion
        }
        #endregion byte[] Getvw_Legs()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е�����

        #region DataTable Getvw_Legs_NotCompress()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// <summary>
        /// DataTable Getvw_Legs_NotCompress()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// </summary>
        /// <returns>���ص����ݱ�</returns>
        public DataTable Getvw_Legs_NotCompress()
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //
            string strRecordCount = ""; //���ݱ������

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbLegs)
                {
                    //��¼ʹ��
                    strProcName = "(��)Getvw_Legs[MEM][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_Legs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //
                if (dataTable == null)
                    throw new Exception("dataTable == null");
                   
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
            #endregion

            //#region �����ݱ�ѹ����ϵ�л��ɶ�������
            ////�����ݱ�ѹ����ϵ�л��ɶ�������
            //if (dataTable != null)
            //{
            //    //��¼ʹ��
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //��¼ʹ��
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            //#endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objMEM_Getvw_Legs__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__MEM__TotalCompressTimes;

                        iMEM_Getvw_Legs__MEM__OprationCount++;
                        if (iMEM_Getvw_Legs__MEM__OprationCount == 0)
                            dMEM_Getvw_Legs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__MEM__TotalProcTimes =
                            fMEM_Getvw_Legs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__MEM__TotalCompressTimes =
                            fMEM_Getvw_Legs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_Legs__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__SQL__TotalCompressTimes;

                        iMEM_Getvw_Legs__SQL__OprationCount++;
                        if (iMEM_Getvw_Legs__SQL__OprationCount == 0)
                            dMEM_Getvw_Legs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__SQL__TotalProcTimes =
                            fMEM_Getvw_Legs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__SQL__TotalCompressTimes =
                            fMEM_Getvw_Legs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[��¼����" + strRecordCount + "��]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return dataTable;

            #endregion
        }
        #endregion DataTable Getvw_Legs_NotCompress()����ȡ AgentServiceDAF�� �� vw_Legs�ֶ� �����е����ݣ����ص����ݲ�����ѹ��


        #region byte[] Getvw_FlightChangeRecord()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е�����
        /// <summary>
        /// byte[] Getvw_FlightChangeRecord()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е�����
        /// </summary>
        /// <returns>ϵ�л���ѹ��������ݱ��������</returns>
        public byte[] Getvw_FlightChangeRecord()
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //
            string strRecordCount = ""; //���ݱ������

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbLegs)
                {
                    //��¼ʹ��
                    strProcName = "(��)Getvw_FlightChangeRecord[MEM]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_FlightChangeRecord.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
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
            #endregion

            #region �����ݱ�ѹ����ϵ�л��ɶ�������
            //�����ݱ�ѹ����ϵ�л��ɶ�������
            if (dataTable != null)
            {
                //��¼ʹ��
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //��¼ʹ��
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objMEM_Getvw_FlightChangeRecord__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__MEM__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__MEM__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_FlightChangeRecord__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__SQL__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__SQL__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[��¼����" + strRecordCount + "��]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return bResult;

            #endregion
        }
        #endregion byte[] Getvw_FlightChangeRecord()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е�����

        #region DataTable Getvw_FlightChangeRecord_NotCompress()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// <summary>
        /// DataTable Getvw_FlightChangeRecord_NotCompress()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е����ݣ����ص����ݲ�����ѹ��
        /// </summary>
        /// <returns>���ص����ݱ�</returns>
        public DataTable Getvw_FlightChangeRecord_NotCompress()
        {
            #region ��������
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //��¼ʹ��
            string strProcName = ""; //������
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

            //
            string strRecordCount = ""; //���ݱ������

            #endregion


            #region ����ʵ��
            #region ��ȡ����
            //��ȡ����
            try
            {
                //�����ڴ��
                if (!blnBusy_tbLegs)
                {
                    //��¼ʹ��
                    strProcName = "(��)Getvw_FlightChangeRecord[MEM][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_FlightChangeRecord.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //��¼ʹ��
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //
                if (dataTable == null)
                    throw new Exception("dataTable == null");

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
            #endregion

            //#region �����ݱ�ѹ����ϵ�л��ɶ�������
            ////�����ݱ�ѹ����ϵ�л��ɶ�������
            //if (dataTable != null)
            //{
            //    //��¼ʹ��
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //��¼ʹ��
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            //#endregion

            #region ��¼ʹ��--��¼��Ϣ��ӽ���¼��
            //��¼ʹ��--��¼��Ϣ��ӽ���¼�� 
            //��λ��Ӧ�� ������
            try
            {
                if (strProcName.IndexOf("��") >= 0)
                {
                    lock (objMEM_Getvw_FlightChangeRecord__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__MEM__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__MEM__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_FlightChangeRecord__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__SQL__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__SQL__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //д����¼��
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[��¼����" + strRecordCount + "��]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //���ؽ��
            return dataTable;

            #endregion
        }
        #endregion DataTable Getvw_FlightChangeRecord_NotCompress()����ȡ AgentServiceDAF�� �� vw_FlightChangeRecord�ֶ� �����е����ݣ����ص����ݲ�����ѹ��

        #endregion MEM����ȡ AgentServiceDAF�� �� tbLegs��vw_Legs��vw_FlightChangeRecord�ֶ� �����е�����


        #region test -- ����ʹ��
        public String HelloMethod(String name)
        {
            //Console.WriteLine("�������� : {0}", name);
            return "�����ǣ�" + name;
        }
        #endregion 

        #endregion


        #region ���ݲ�ѯ�����ڴ����ȡ����[��� strTableName ��ʾ�����ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ����]
        /// <summary>
        /// ���ݲ�ѯ�����ڴ����ȡ����
        /// </summary>
        /// <param name="strTableName">�ڴ����</param>
        /// <param name="strSQL">��ѯ���</param>
        /// <param name="strSort">�������</param>
        /// <param name="strFilterField">��Ҫ��ȡ���ֶΣ���",column1,column2,column3,"</param>
        /// <returns>��ѯ�Ľ�������󷵻� null</returns>
        public DataTable GetDataBySQL(string strTableName, string strSQL,string strSort,string strFilterField)
        {
            #region ��������
            DataTable dataTable = null;
            DataRow[] dataRows = null;

            #endregion


            #region ����ʵ��
            try
            {
                //tbLegs
                if (strTableName == "tbLegs")
                {
                    dataTable = tbLegs.Clone();
                    dataRows = tbLegs.Select(strSQL, strSort);
                }
                //vw_Legs
                else if (strTableName == "vw_Legs")
                {
                    dataTable = vw_Legs.Clone();
                    dataRows = vw_Legs.Select(strSQL, strSort);
                }
                //vw_FlightChangeRecord
                else if (strTableName == "vw_FlightChangeRecord")
                {
                    dataTable = vw_FlightChangeRecord.Clone();
                    dataRows = vw_FlightChangeRecord.Select(strSQL, strSort);
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
