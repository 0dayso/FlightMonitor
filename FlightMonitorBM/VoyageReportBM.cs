using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ��ǰ��������Ϣʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-09-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class VoyageReportBM
    {
        #region ����
        private string _id;
        private string _datop;
        private string _fltids;
        private string _ac;
        private string _dep_times;
        private string _routes;
        private string _maintain_stn;
        private string _cn_routes;
        private string _stcs;
        private string _fly_times;
        private string _accd;
        private string _compid;
        private string _captain;
        private string _captain_remark;
        private string _pilot_checker;
        private string _inspected_pilot;
        private string _check_type;
        private string _skipper1;
        private string _skipper1_remark;
        private string _skipper2;
        private string _skipper2_remark;
        private string _first_vice1;
        private string _first_vice1_remark;
        private string _first_vice2;
        private string _second_vice1;
        private string _sencond_vice2;
        private string _learner;
        private string _engineer;
        private string _teler;
        private string _accompany1;
        private string _accompany2;
        private string _pilot_deadhead_ops;
        private string _pilot_deadhead_other;
        private string _chief_steward_captain;
        private string _chief_steward_captain_remark;
        private string _steward_cap1;
        private string _steward_cap1_remark;
        private string _safer1;
        private string _safer2;
        private string _stewards;
        private string _steward_checker;
        private string _steward_instructor;
        private string _steward_checktype;
        private string _teler_remark;
        private string _steward_deadhead_ops;
        private string _steward_deadhead_other;
        private string _second_captain;
        private string _second_captain_remark;
        private string _steward_cap2;
        private string _steward_cap2_remark;
        private string _pilot_deadhead;
        private string _steward_deadhead;
        private string _translator;
        private string _jumpseating;
        private string _routedesc;
        private int _cnifltreportid;
        private string _captain_sid;
        private string _pilot_checker_sid;
        private string _inspected_pilot_sid;
        private string _skipper1_sid;
        private string _first_vice1_sid;
        private string _engineer_sid;
        private string _teler_sid;
        private string _accompany1_sid;
        private string _chief_steward_captain_sid;
        private string _steward_cap1_sid;
        private string _safer1_sid;
        private string _stewards_sid;
        private string _pilot_deadhead_ops_sid;
        private string _pilot_deadhead_other_sid;
        private string _jumpseating_sid;
        private string _steward_checker_sid;
        private string _steward_instructor_sid;
        private string _steward_deadhead_ops_sid;
        private string _steward_deadhead_other_sid;
        private string _crw_steward_inf;
        private string _crw_pilot_inf;
        private string _std;

        private string _cnvcmemo;
        private bool _cnblnsuccess = false;

        private DataTable _memberinfo = null;
        private DataTable _segmentinfo = null;

        #endregion ����

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DATOP
        {
            set { _datop = value; }
            get { return _datop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FLTIDS
        {
            set { _fltids = value; }
            get { return _fltids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AC
        {
            set { _ac = value; }
            get { return _ac; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DEP_TIMES
        {
            set { _dep_times = value; }
            get { return _dep_times; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ROUTES
        {
            set { _routes = value; }
            get { return _routes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MAINTAIN_STN
        {
            set { _maintain_stn = value; }
            get { return _maintain_stn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CN_ROUTES
        {
            set { _cn_routes = value; }
            get { return _cn_routes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STCS
        {
            set { _stcs = value; }
            get { return _stcs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FLY_TIMES
        {
            set { _fly_times = value; }
            get { return _fly_times; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCD
        {
            set { _accd = value; }
            get { return _accd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string compid
        {
            set { _compid = value; }
            get { return _compid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CAPTAIN
        {
            set { _captain = value; }
            get { return _captain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CAPTAIN_REMARK
        {
            set { _captain_remark = value; }
            get { return _captain_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PILOT_CHECKER
        {
            set { _pilot_checker = value; }
            get { return _pilot_checker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string INSPECTED_PILOT
        {
            set { _inspected_pilot = value; }
            get { return _inspected_pilot; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CHECK_TYPE
        {
            set { _check_type = value; }
            get { return _check_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKIPPER1
        {
            set { _skipper1 = value; }
            get { return _skipper1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKIPPER1_REMARK
        {
            set { _skipper1_remark = value; }
            get { return _skipper1_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string skipper2
        {
            set { _skipper2 = value; }
            get { return _skipper2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string skipper2_remark
        {
            set { _skipper2_remark = value; }
            get { return _skipper2_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FIRST_VICE1
        {
            set { _first_vice1 = value; }
            get { return _first_vice1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FIRST_VICE1_REMARK
        {
            set { _first_vice1_remark = value; }
            get { return _first_vice1_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FIRST_VICE2
        {
            set { _first_vice2 = value; }
            get { return _first_vice2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SECOND_VICE1
        {
            set { _second_vice1 = value; }
            get { return _second_vice1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SENCOND_VICE2
        {
            set { _sencond_vice2 = value; }
            get { return _sencond_vice2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LEARNER
        {
            set { _learner = value; }
            get { return _learner; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ENGINEER
        {
            set { _engineer = value; }
            get { return _engineer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TELER
        {
            set { _teler = value; }
            get { return _teler; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCOMPANY1
        {
            set { _accompany1 = value; }
            get { return _accompany1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ACCOMPANY2
        {
            set { _accompany2 = value; }
            get { return _accompany2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PILOT_DEADHEAD_OPS
        {
            set { _pilot_deadhead_ops = value; }
            get { return _pilot_deadhead_ops; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PILOT_DEADHEAD_OTHER
        {
            set { _pilot_deadhead_other = value; }
            get { return _pilot_deadhead_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CHIEF_STEWARD_CAPTAIN
        {
            set { _chief_steward_captain = value; }
            get { return _chief_steward_captain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CHIEF_STEWARD_CAPTAIN_REMARK
        {
            set { _chief_steward_captain_remark = value; }
            get { return _chief_steward_captain_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_CAP1
        {
            set { _steward_cap1 = value; }
            get { return _steward_cap1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_CAP1_REMARK
        {
            set { _steward_cap1_remark = value; }
            get { return _steward_cap1_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SAFER1
        {
            set { _safer1 = value; }
            get { return _safer1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SAFER2
        {
            set { _safer2 = value; }
            get { return _safer2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARDS
        {
            set { _stewards = value; }
            get { return _stewards; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_CHECKER
        {
            set { _steward_checker = value; }
            get { return _steward_checker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_INSTRUCTOR
        {
            set { _steward_instructor = value; }
            get { return _steward_instructor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_CHECKTYPE
        {
            set { _steward_checktype = value; }
            get { return _steward_checktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TELER_REMARK
        {
            set { _teler_remark = value; }
            get { return _teler_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_DEADHEAD_OPS
        {
            set { _steward_deadhead_ops = value; }
            get { return _steward_deadhead_ops; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_DEADHEAD_OTHER
        {
            set { _steward_deadhead_other = value; }
            get { return _steward_deadhead_other; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SECOND_CAPTAIN
        {
            set { _second_captain = value; }
            get { return _second_captain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SECOND_CAPTAIN_REMARK
        {
            set { _second_captain_remark = value; }
            get { return _second_captain_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_CAP2
        {
            set { _steward_cap2 = value; }
            get { return _steward_cap2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_CAP2_REMARK
        {
            set { _steward_cap2_remark = value; }
            get { return _steward_cap2_remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PILOT_DEADHEAD
        {
            set { _pilot_deadhead = value; }
            get { return _pilot_deadhead; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STEWARD_DEADHEAD
        {
            set { _steward_deadhead = value; }
            get { return _steward_deadhead; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TRANSLATOR
        {
            set { _translator = value; }
            get { return _translator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JUMPSEATING
        {
            set { _jumpseating = value; }
            get { return _jumpseating; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ROUTEDESC
        {
            set { _routedesc = value; }
            get { return _routedesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int cniFltReportId
        {
            set { _cnifltreportid = value; }
            get { return _cnifltreportid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Captain_SID
        {
            set { _captain_sid = value; }
            get { return _captain_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pilot_checker_SID
        {
            set { _pilot_checker_sid = value; }
            get { return _pilot_checker_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string INSPECTED_PILOT_SID
        {
            set { _inspected_pilot_sid = value; }
            get { return _inspected_pilot_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string skipper1_SID
        {
            set { _skipper1_sid = value; }
            get { return _skipper1_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string first_vice1_SID
        {
            set { _first_vice1_sid = value; }
            get { return _first_vice1_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Engineer_SID
        {
            set { _engineer_sid = value; }
            get { return _engineer_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Teler_SID
        {
            set { _teler_sid = value; }
            get { return _teler_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string accompany1_SID
        {
            set { _accompany1_sid = value; }
            get { return _accompany1_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string chief_steward_captain_SID
        {
            set { _chief_steward_captain_sid = value; }
            get { return _chief_steward_captain_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string steward_cap1_SID
        {
            set { _steward_cap1_sid = value; }
            get { return _steward_cap1_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string safer1_SID
        {
            set { _safer1_sid = value; }
            get { return _safer1_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Stewards_SID
        {
            set { _stewards_sid = value; }
            get { return _stewards_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pilot_deadhead_ops_SID
        {
            set { _pilot_deadhead_ops_sid = value; }
            get { return _pilot_deadhead_ops_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pilot_deadhead_other_SID
        {
            set { _pilot_deadhead_other_sid = value; }
            get { return _pilot_deadhead_other_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JUMPSEATING_SID
        {
            set { _jumpseating_sid = value; }
            get { return _jumpseating_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string steward_checker_SID
        {
            set { _steward_checker_sid = value; }
            get { return _steward_checker_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string steward_instructor_SID
        {
            set { _steward_instructor_sid = value; }
            get { return _steward_instructor_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string steward_deadhead_ops_SID
        {
            set { _steward_deadhead_ops_sid = value; }
            get { return _steward_deadhead_ops_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string steward_deadhead_other_SID
        {
            set { _steward_deadhead_other_sid = value; }
            get { return _steward_deadhead_other_sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CRW_STEWARD_INF
        {
            set { _crw_steward_inf = value; }
            get { return _crw_steward_inf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CRW_PILOT_INF
        {
            set { _crw_pilot_inf = value; }
            get { return _crw_pilot_inf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STD
        {
            set { _std = value; }
            get { return _std; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Memo
        {
            set { _cnvcmemo = value; }
            get { return _cnvcmemo; }
        }       
        
        /// <summary>
        /// ��ֵ�ɹ���־
        /// </summary>
        public bool Success
        {
            set { _cnblnsuccess = value; }
            get { return _cnblnsuccess; }
        }

        /// <summary>
        /// ��Ա��Ϣ
        /// </summary>
        public DataTable MemberInfo
        {
            get { return _memberinfo; }
        }      
        
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public DataTable SegmentInfo
        {
            get { return _segmentinfo; }
        }
        #endregion ����


        #region ����
        
        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public VoyageReportBM()
        {
        }
        #endregion ���캯��

        #region ���캯������һ�� VoyageReport DataRow���ݸ���ʵ�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">��ǰ������������</param>
        public VoyageReportBM(DataRow dataRow)
        {
            try
            {
                _id = dataRow["id"].ToString();
                _datop = dataRow["datop"].ToString();
                _fltids = dataRow["fltids"].ToString();
                _ac = dataRow["ac"].ToString();
                _dep_times = dataRow["dep_times"].ToString();
                _routes = dataRow["routes"].ToString();
                _maintain_stn = dataRow["maintain_stn"].ToString();
                _cn_routes = dataRow["cn_routes"].ToString();
                _stcs = dataRow["stcs"].ToString();
                _fly_times = dataRow["fly_times"].ToString();
                _accd = dataRow["accd"].ToString();
                _compid = dataRow["compid"].ToString();
                _captain = dataRow["captain"].ToString();
                _captain_remark = dataRow["captain_remark"].ToString();
                _pilot_checker = dataRow["pilot_checker"].ToString();
                _inspected_pilot = dataRow["inspected_pilot"].ToString();
                _check_type = dataRow["check_type"].ToString();
                _skipper1 = dataRow["skipper1"].ToString();
                _skipper1_remark = dataRow["skipper1_remark"].ToString();
                _skipper2 = dataRow["skipper2"].ToString();
                _skipper2_remark = dataRow["skipper2_remark"].ToString();
                _first_vice1 = dataRow["first_vice1"].ToString();
                _first_vice1_remark = dataRow["first_vice1_remark"].ToString();
                _first_vice2 = dataRow["first_vice2"].ToString();
                _second_vice1 = dataRow["second_vice1"].ToString();
                _sencond_vice2 = dataRow["sencond_vice2"].ToString();
                _learner = dataRow["learner"].ToString();
                _engineer = dataRow["engineer"].ToString();
                _teler = dataRow["teler"].ToString();
                _accompany1 = dataRow["accompany1"].ToString();
                _accompany2 = dataRow["accompany2"].ToString();
                _pilot_deadhead_ops = dataRow["pilot_deadhead_ops"].ToString();
                _pilot_deadhead_other = dataRow["pilot_deadhead_other"].ToString();
                _chief_steward_captain = dataRow["chief_steward_captain"].ToString();
                _chief_steward_captain_remark = dataRow["chief_steward_captain_remark"].ToString();
                _steward_cap1 = dataRow["steward_cap1"].ToString();
                _steward_cap1_remark = dataRow["steward_cap1_remark"].ToString();
                _safer1 = dataRow["safer1"].ToString();
                _safer2 = dataRow["safer2"].ToString();
                _stewards = dataRow["stewards"].ToString();
                _steward_checker = dataRow["steward_checker"].ToString();
                _steward_instructor = dataRow["steward_instructor"].ToString();
                _steward_checktype = dataRow["steward_checktype"].ToString();
                _teler_remark = dataRow["teler_remark"].ToString();
                _steward_deadhead_ops = dataRow["steward_deadhead_ops"].ToString();
                _steward_deadhead_other = dataRow["steward_deadhead_other"].ToString();
                _second_captain = dataRow["second_captain"].ToString();
                _second_captain_remark = dataRow["second_captain_remark"].ToString();
                _steward_cap2 = dataRow["steward_cap2"].ToString();
                _steward_cap2_remark = dataRow["steward_cap2_remark"].ToString();
                _pilot_deadhead = dataRow["pilot_deadhead"].ToString();
                _steward_deadhead = dataRow["steward_deadhead"].ToString();
                _translator = dataRow["translator"].ToString();
                _jumpseating = dataRow["jumpseating"].ToString();
                _routedesc = dataRow["routedesc"].ToString();
                _cnifltreportid = Convert.ToInt32( dataRow["cnifltreportid"].ToString());
                _captain_sid = dataRow["captain_sid"].ToString();
                _pilot_checker_sid = dataRow["pilot_checker_sid"].ToString();
                _inspected_pilot_sid = dataRow["inspected_pilot_sid"].ToString();
                _skipper1_sid = dataRow["skipper1_sid"].ToString();
                _first_vice1_sid = dataRow["first_vice1_sid"].ToString();
                _engineer_sid = dataRow["engineer_sid"].ToString();
                _teler_sid = dataRow["teler_sid"].ToString();
                _accompany1_sid = dataRow["accompany1_sid"].ToString();
                _chief_steward_captain_sid = dataRow["chief_steward_captain_sid"].ToString();
                _steward_cap1_sid = dataRow["steward_cap1_sid"].ToString();
                _safer1_sid = dataRow["safer1_sid"].ToString();
                _stewards_sid = dataRow["stewards_sid"].ToString();
                _pilot_deadhead_ops_sid = dataRow["pilot_deadhead_ops_sid"].ToString();
                _pilot_deadhead_other_sid = dataRow["pilot_deadhead_other_sid"].ToString();
                _jumpseating_sid = dataRow["jumpseating_sid"].ToString();
                _steward_checker_sid = dataRow["steward_checker_sid"].ToString();
                _steward_instructor_sid = dataRow["steward_instructor_sid"].ToString();
                _steward_deadhead_ops_sid = dataRow["steward_deadhead_ops_sid"].ToString();
                _steward_deadhead_other_sid = dataRow["steward_deadhead_other_sid"].ToString();
                _crw_steward_inf = dataRow["crw_steward_inf"].ToString();
                _crw_pilot_inf = dataRow["crw_pilot_inf"].ToString();
                _std = dataRow["std"].ToString();                         

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcmemo = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion ���캯������һ�� VoyageReport DataRow���ݸ���ʵ�����

        #region �������ݱ�������� MemberInfo ��Ϊ NULL����ֱ�ӷ��� MemberInfo������ÿ��һ�еı����ʽ������������Ա����
        /// <summary>
        /// �������ݱ�������� MemberInfo ��Ϊ NULL����ֱ�ӷ��� MemberInfo������ÿ��һ�еı����ʽ������������Ա����
        /// </summary>
        /// <returns>�������ݱ�������� MemberInfo ��Ϊ NULL����ֱ�ӷ��� MemberInfo������ÿ��һ�еı����ʽ������������Ա����</returns>
        public DataTable AnalyseVoyageReport_MemberInfo()
        {
            #region ��������
            DataTable dataTable = new DataTable();  //��ÿ��һ�еı����ʽ��������������
            DataColumn dataColumn = null;

            #endregion ��������


            #region ����ʵ��
            try
            {
                if (_memberinfo == null)
                {

                    #region ���ɱ��
                    dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
                    dataColumn.AutoIncrement = true;
                    dataColumn.AutoIncrementSeed = 1;
                    dataColumn.AutoIncrementStep = 1;
                    dataColumn.Caption = "���";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "λ��";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "����";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "����";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "����֤��";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "��˺���";
                    dataTable.Columns.Add(dataColumn);
                    #endregion ���ɱ��

                    //����
                    #region ����
                    if (_captain.Trim() != "")
                    {
                        string[] arrCaptain = _captain.Split(new char[2] { '@', '/' });
                        string[] arrCaptain_SID = _captain_sid.Split(new char[2] { '@', '/' });

                        for (int indexCaptain = 0; indexCaptain < arrCaptain.Length; indexCaptain++)
                        {
                            string strCaptain = arrCaptain[indexCaptain];

                            int intPos_1 = strCaptain.IndexOf("(");
                            int intPos_2 = strCaptain.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexCaptain == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���λ���";
                                else
                                    dataRow["cnvcPosition"] = "����";
                                dataRow["cnvcName"] = strCaptain.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strCaptain.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                dataRow["cnvcSID"] = arrCaptain_SID[indexCaptain].Substring(0, arrCaptain_SID[indexCaptain].IndexOf("(")).TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexCaptain == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���λ���";
                                else
                                    dataRow["cnvcPosition"] = "����";
                                dataRow["cnvcName"] = strCaptain.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrCaptain_SID[indexCaptain].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������л�����Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ����

                    #region �����ʻ
                    if (_skipper1.Trim() != "")
                    {
                        string[] arrSKIPPER1 = _skipper1.Split(new char[2] { '@', '/' });
                        string[] arrSKIPPER1_SID = _skipper1_sid.Split(new char[2] { '@', '/' });

                        for (int indexSKIPPER1 = 0; indexSKIPPER1 < arrSKIPPER1.Length; indexSKIPPER1++)
                        {
                            string strSKIPPER1 = arrSKIPPER1[indexSKIPPER1];

                            int intPos_1 = strSKIPPER1.IndexOf("(");
                            int intPos_2 = strSKIPPER1.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSKIPPER1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "�����ʻ";
                                else
                                    dataRow["cnvcPosition"] = "�����ʻ";
                                dataRow["cnvcName"] = strSKIPPER1.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strSKIPPER1.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                dataRow["cnvcSID"] = arrSKIPPER1_SID[indexSKIPPER1].Substring(0, arrSKIPPER1_SID[indexSKIPPER1].IndexOf("(")).TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSKIPPER1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "�����ʻ";
                                else
                                    dataRow["cnvcPosition"] = "�����ʻ";
                                dataRow["cnvcName"] = strSKIPPER1.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSKIPPER1_SID[indexSKIPPER1].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������������ʻ��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion �����ʻ

                    #region ����ʻ
                    if (_first_vice1.Trim() != "")
                    {

                        string[] arrFIRST_VICE1 = _first_vice1.Split(new char[2] { '@', '/' });
                        string[] arrFIRST_VICE1_SID = _first_vice1_sid.Split(new char[2] { '@', '/' });

                        for (int indexFIRST_VICE1 = 0; indexFIRST_VICE1 < arrFIRST_VICE1.Length; indexFIRST_VICE1++)
                        {
                            string strFIRST_VICE1 = arrFIRST_VICE1[indexFIRST_VICE1];

                            int intPos_1 = strFIRST_VICE1.IndexOf("(");
                            int intPos_2 = strFIRST_VICE1.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexFIRST_VICE1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����ʻ";
                                else
                                    dataRow["cnvcPosition"] = "����ʻ";
                                dataRow["cnvcName"] = strFIRST_VICE1.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strFIRST_VICE1.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                dataRow["cnvcSID"] = arrFIRST_VICE1_SID[indexFIRST_VICE1].Substring(0, arrFIRST_VICE1_SID[indexFIRST_VICE1].IndexOf("(")).TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexFIRST_VICE1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����ʻ";
                                else
                                    dataRow["cnvcPosition"] = "����ʻ";
                                dataRow["cnvcName"] = strFIRST_VICE1.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrFIRST_VICE1_SID[indexFIRST_VICE1].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������и���ʻ��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ����ʻ

                    #region ����Ա
                    if (_teler.Trim() != "")
                    {

                        string[] arrTELER = _teler.Split(new char[2] { '@', '/' });
                        string[] arrTELER_SID = _teler_sid.Split(new char[2] { '@', '/' });

                        for (int indexTELER = 0; indexTELER < arrTELER.Length; indexTELER++)
                        {
                            string strTELER = arrTELER[indexTELER];

                            int intPos_1 = strTELER.IndexOf("(");
                            int intPos_2 = strTELER.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexTELER == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����Ա";
                                else
                                    dataRow["cnvcPosition"] = "����Ա";
                                dataRow["cnvcName"] = strTELER.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strTELER.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                if (arrTELER_SID[indexTELER].IndexOf("(") >= 0)
                                    dataRow["cnvcSID"] = arrTELER_SID[indexTELER].Substring(0, arrTELER_SID[indexTELER].IndexOf("(")).TrimEnd('*'); //����֤��
                                else
                                    dataRow["cnvcSID"] = arrTELER_SID[indexTELER].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexTELER == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����Ա";
                                else
                                    dataRow["cnvcPosition"] = "����Ա";
                                dataRow["cnvcName"] = strTELER.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrTELER_SID[indexTELER].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������б���Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ����Ա

                    #region �����Ա
                    if (_maintain_stn.Trim() != "")
                    {
                        string[] arrMAINTAIN_STN = _maintain_stn.Split(new char[2] { '@', '/' });
                        //string[] arrMAINTAIN_STN_SID = _maintain_stn_sid.Split('@');

                        for (int indexMAINTAIN_STN = 0; indexMAINTAIN_STN < arrMAINTAIN_STN.Length; indexMAINTAIN_STN++)
                        {
                            string strMAINTAIN_STN = arrMAINTAIN_STN[indexMAINTAIN_STN];

                            int intPos_1 = strMAINTAIN_STN.IndexOf("(");
                            int intPos_2 = strMAINTAIN_STN.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexMAINTAIN_STN == 0)      //λ��
                                    dataRow["cnvcPosition"] = "�����Ա";
                                else
                                    dataRow["cnvcPosition"] = "�����Ա";
                                dataRow["cnvcName"] = strMAINTAIN_STN.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strMAINTAIN_STN.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                //dataRow["cnvcSID"] = arrMAINTAIN_STN_SID[indexMAINTAIN_STN].Substring(0, arrMAINTAIN_STN_SID[indexMAINTAIN_STN].IndexOf("(")).TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexMAINTAIN_STN == 0)      //λ��
                                    dataRow["cnvcPosition"] = "�����Ա";
                                else
                                    dataRow["cnvcPosition"] = "�����Ա";
                                dataRow["cnvcName"] = strMAINTAIN_STN.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                //dataRow["cnvcSID"] = arrMAINTAIN_STN_SID[indexMAINTAIN_STN].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������б����Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion �����Ա

                    #region ���л�����Ա
                    if (_pilot_checker.Trim() != "")
                    {

                        string[] arrPILOT_CHECKER = _pilot_checker.Split(new char[2] { '@', '/' });
                        string[] arrPILOT_CHECKER_SID = _pilot_checker_sid.Split(new char[2] { '@', '/' });

                        for (int indexPILOT_CHECKER = 0; indexPILOT_CHECKER < arrPILOT_CHECKER.Length; indexPILOT_CHECKER++)
                        {
                            string strPILOT_CHECKER = arrPILOT_CHECKER[indexPILOT_CHECKER];

                            int intPos_1 = strPILOT_CHECKER.IndexOf("(");
                            int intPos_2 = strPILOT_CHECKER.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexPILOT_CHECKER == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���л�����Ա";
                                else
                                    dataRow["cnvcPosition"] = "���л�����Ա";
                                dataRow["cnvcName"] = strPILOT_CHECKER.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strPILOT_CHECKER.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                if (arrPILOT_CHECKER_SID[indexPILOT_CHECKER].IndexOf("(") >= 0)
                                    dataRow["cnvcSID"] = arrPILOT_CHECKER_SID[indexPILOT_CHECKER].Substring(0, arrPILOT_CHECKER_SID[indexPILOT_CHECKER].IndexOf("(")).TrimEnd('*'); //����֤��
                                else
                                    dataRow["cnvcSID"] = arrPILOT_CHECKER_SID[indexPILOT_CHECKER].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexPILOT_CHECKER == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���л�����Ա";
                                else
                                    dataRow["cnvcPosition"] = "���л�����Ա";
                                dataRow["cnvcName"] = strPILOT_CHECKER.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrPILOT_CHECKER_SID[indexPILOT_CHECKER].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������з��л�����Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ���л�����Ա


                    #region ���γ���
                    if (_chief_steward_captain.Trim() != "")
                    {

                        string[] arrCHIEF_STEWARD_CAPTAIN = _chief_steward_captain.Split(new char[2] { '@', '/' });
                        string[] arrCHIEF_STEWARD_CAPTAIN_SID = _chief_steward_captain_sid.Split(new char[2] { '@', '/' });

                        for (int indexCHIEF_STEWARD_CAPTAIN = 0; indexCHIEF_STEWARD_CAPTAIN < arrCHIEF_STEWARD_CAPTAIN.Length; indexCHIEF_STEWARD_CAPTAIN++)
                        {
                            string strCHIEF_STEWARD_CAPTAIN = arrCHIEF_STEWARD_CAPTAIN[indexCHIEF_STEWARD_CAPTAIN];

                            int intPos_1 = strCHIEF_STEWARD_CAPTAIN.IndexOf("(");
                            int intPos_2 = strCHIEF_STEWARD_CAPTAIN.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexCHIEF_STEWARD_CAPTAIN == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���γ���";
                                else
                                    dataRow["cnvcPosition"] = "���γ���";
                                dataRow["cnvcName"] = strCHIEF_STEWARD_CAPTAIN.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strCHIEF_STEWARD_CAPTAIN.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                dataRow["cnvcSID"] = arrCHIEF_STEWARD_CAPTAIN_SID[indexCHIEF_STEWARD_CAPTAIN].Substring(0, arrCHIEF_STEWARD_CAPTAIN_SID[indexCHIEF_STEWARD_CAPTAIN].IndexOf("(")).TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexCHIEF_STEWARD_CAPTAIN == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���γ���";
                                else
                                    dataRow["cnvcPosition"] = "���γ���";
                                dataRow["cnvcName"] = strCHIEF_STEWARD_CAPTAIN.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrCHIEF_STEWARD_CAPTAIN_SID[indexCHIEF_STEWARD_CAPTAIN].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�����������γ�����Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ���γ���

                    #region ������Ա
                    if (_steward_checker.Trim() != "")
                    {

                        string[] arrSTEWARD_CHECKER = _steward_checker.Split(new char[2] { '@', '/' });
                        string[] arrSTEWARD_CHECKER_SID = _steward_checker_sid.Split(new char[2] { '@', '/' });

                        for (int indexSTEWARD_CHECKER = 0; indexSTEWARD_CHECKER < arrSTEWARD_CHECKER.Length; indexSTEWARD_CHECKER++)
                        {
                            string strSTEWARD_CHECKER = arrSTEWARD_CHECKER[indexSTEWARD_CHECKER];

                            int intPos_1 = strSTEWARD_CHECKER.IndexOf("(");
                            int intPos_2 = strSTEWARD_CHECKER.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_CHECKER == 0)      //λ��
                                    dataRow["cnvcPosition"] = "������Ա";
                                else
                                    dataRow["cnvcPosition"] = "������Ա";
                                dataRow["cnvcName"] = strSTEWARD_CHECKER.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strSTEWARD_CHECKER.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                if (arrSTEWARD_CHECKER_SID[indexSTEWARD_CHECKER].IndexOf("(") >= 0)
                                    dataRow["cnvcSID"] = arrSTEWARD_CHECKER_SID[indexSTEWARD_CHECKER].Substring(0, arrSTEWARD_CHECKER_SID[indexSTEWARD_CHECKER].IndexOf("(")).TrimEnd('*'); //����֤��
                                else
                                    dataRow["cnvcSID"] = arrSTEWARD_CHECKER_SID[indexSTEWARD_CHECKER].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_CHECKER == 0)      //λ��
                                    dataRow["cnvcPosition"] = "������Ա";
                                else
                                    dataRow["cnvcPosition"] = "������Ա";
                                dataRow["cnvcName"] = strSTEWARD_CHECKER.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSTEWARD_CHECKER_SID[indexSTEWARD_CHECKER].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������г�����Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ������Ա

                    #region ���ɽ�Ա
                    if (_steward_instructor.Trim() != "")
                    {

                        string[] arrSTEWARD_INSTRUCTOR = _steward_instructor.Split(new char[2] { '@', '/' });
                        string[] arrSTEWARD_INSTRUCTOR_SID = _steward_instructor_sid.Split(new char[2] { '@', '/' });

                        for (int indexSTEWARD_INSTRUCTOR = 0; indexSTEWARD_INSTRUCTOR < arrSTEWARD_INSTRUCTOR.Length; indexSTEWARD_INSTRUCTOR++)
                        {
                            string strSTEWARD_INSTRUCTOR = arrSTEWARD_INSTRUCTOR[indexSTEWARD_INSTRUCTOR];

                            int intPos_1 = strSTEWARD_INSTRUCTOR.IndexOf("(");
                            int intPos_2 = strSTEWARD_INSTRUCTOR.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_INSTRUCTOR == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���ɽ�Ա";
                                else
                                    dataRow["cnvcPosition"] = "���ɽ�Ա";
                                dataRow["cnvcName"] = strSTEWARD_INSTRUCTOR.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strSTEWARD_INSTRUCTOR.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                if (arrSTEWARD_INSTRUCTOR_SID[indexSTEWARD_INSTRUCTOR].IndexOf("(") >= 0)
                                    dataRow["cnvcSID"] = arrSTEWARD_INSTRUCTOR_SID[indexSTEWARD_INSTRUCTOR].Substring(0, arrSTEWARD_INSTRUCTOR_SID[indexSTEWARD_INSTRUCTOR].IndexOf("(")).TrimEnd('*'); //����֤��
                                else
                                    dataRow["cnvcSID"] = arrSTEWARD_INSTRUCTOR_SID[indexSTEWARD_INSTRUCTOR].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_INSTRUCTOR == 0)      //λ��
                                    dataRow["cnvcPosition"] = "���ɽ�Ա";
                                else
                                    dataRow["cnvcPosition"] = "���ɽ�Ա";
                                dataRow["cnvcName"] = strSTEWARD_INSTRUCTOR.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSTEWARD_INSTRUCTOR_SID[indexSTEWARD_INSTRUCTOR].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������д��ɽ�Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ���ɽ�Ա

                    #region ��ȫԱ
                    if (_safer1.Trim() != "")
                    {

                        string[] arrSAFER1 = _safer1.Split(new char[2] { '@', '/' });
                        string[] arrSAFER1_SID = _safer1_sid.Split(new char[2] { '@', '/' });

                        for (int indexSAFER1 = 0; indexSAFER1 < arrSAFER1.Length; indexSAFER1++)
                        {
                            string strSAFER1 = arrSAFER1[indexSAFER1];

                            int intPos_1 = strSAFER1.IndexOf("(");
                            int intPos_2 = strSAFER1.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSAFER1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "��ȫԱ";
                                else
                                    dataRow["cnvcPosition"] = "��ȫԱ";
                                dataRow["cnvcName"] = strSAFER1.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strSAFER1.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                if (arrSAFER1_SID[indexSAFER1].IndexOf("(") >= 0)
                                    dataRow["cnvcSID"] = arrSAFER1_SID[indexSAFER1].Substring(0, arrSAFER1_SID[indexSAFER1].IndexOf("(")).TrimEnd('*'); //����֤��
                                else
                                    dataRow["cnvcSID"] = arrSAFER1_SID[indexSAFER1].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSAFER1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "��ȫԱ";
                                else
                                    dataRow["cnvcPosition"] = "��ȫԱ";
                                dataRow["cnvcName"] = strSAFER1.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSAFER1_SID[indexSAFER1].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������а�ȫԱ��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ��ȫԱ

                    #region ����
                    if (_steward_cap1.Trim() != "")
                    {

                        string[] arrSTEWARD_CAP1 = _steward_cap1.Split(new char[2] { '@', '/' });
                        string[] arrSTEWARD_CAP1_SID = _steward_cap1_sid.Split(new char[2] { '@', '/' });

                        for (int indexSTEWARD_CAP1 = 0; indexSTEWARD_CAP1 < arrSTEWARD_CAP1.Length; indexSTEWARD_CAP1++)
                        {
                            string strSTEWARD_CAP1 = arrSTEWARD_CAP1[indexSTEWARD_CAP1];

                            int intPos_1 = strSTEWARD_CAP1.IndexOf("(");
                            int intPos_2 = strSTEWARD_CAP1.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_CAP1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����";
                                else
                                    dataRow["cnvcPosition"] = "����";
                                dataRow["cnvcName"] = strSTEWARD_CAP1.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strSTEWARD_CAP1.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                dataRow["cnvcSID"] = arrSTEWARD_CAP1_SID[indexSTEWARD_CAP1].Substring(0, arrSTEWARD_CAP1_SID[indexSTEWARD_CAP1].IndexOf("(")).TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_CAP1 == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����";
                                else
                                    dataRow["cnvcPosition"] = "����";
                                dataRow["cnvcName"] = strSTEWARD_CAP1.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSTEWARD_CAP1_SID[indexSTEWARD_CAP1].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������г�����Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ����

                    #region ����Ա
                    if (_stewards.Trim() != "")
                    {

                        string[] arrSTEWARDS = _stewards.Split(new char[2] { '@', '/' });
                        string[] arrSTEWARDS_SID = _stewards_sid.Split(new char[2] { '@', '/' });

                        for (int indexSTEWARDS = 0; indexSTEWARDS < arrSTEWARDS.Length; indexSTEWARDS++)
                        {
                            string strSTEWARDS = arrSTEWARDS[indexSTEWARDS];

                            int intPos_1 = strSTEWARDS.IndexOf("(");
                            int intPos_2 = strSTEWARDS.IndexOf(")");

                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARDS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����Ա";
                                else
                                    dataRow["cnvcPosition"] = "����Ա";
                                dataRow["cnvcName"] = strSTEWARDS.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = strSTEWARDS.Substring(intPos_1, (intPos_2 - intPos_1 + 1)); //����
                                if (arrSTEWARDS_SID[indexSTEWARDS].IndexOf("(") > 0)
                                    dataRow["cnvcSID"] = arrSTEWARDS_SID[indexSTEWARDS].Substring(0, arrSTEWARDS_SID[indexSTEWARDS].IndexOf("(")).TrimEnd('*'); //����֤��
                                else
                                    dataRow["cnvcSID"] = arrSTEWARDS_SID[indexSTEWARDS].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARDS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "����Ա";
                                else
                                    dataRow["cnvcPosition"] = "����Ա";
                                dataRow["cnvcName"] = strSTEWARDS.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSTEWARDS_SID[indexSTEWARDS].TrimEnd('*'); //����֤��

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("�������г���Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ����Ա


                    #region ִ�к���ӻ���ķ���Ա
                    if (_pilot_deadhead_ops.Trim() != "")
                    {

                        string[] arrPILOT_DEADHEAD_OPS = _pilot_deadhead_ops.Split(new char[2] { '@', '/' });
                        string[] arrPILOT_DEADHEAD_OPS_SID = _pilot_deadhead_ops_sid.Split(new char[2] { '@', '/' });

                        for (int indexPILOT_DEADHEAD_OPS = 0; indexPILOT_DEADHEAD_OPS < arrPILOT_DEADHEAD_OPS.Length; indexPILOT_DEADHEAD_OPS++)
                        {
                            string strPILOT_DEADHEAD_OPS = arrPILOT_DEADHEAD_OPS[indexPILOT_DEADHEAD_OPS];
                            strPILOT_DEADHEAD_OPS = strPILOT_DEADHEAD_OPS.Replace(" ", "");

                            int intPos_1 = strPILOT_DEADHEAD_OPS.IndexOf("(");
                            int intPos_2 = strPILOT_DEADHEAD_OPS.IndexOf(")");
                            int intPos_3 = strPILOT_DEADHEAD_OPS.IndexOf(")(");

                            if (intPos_3 > 0) //�������������²���*(G)(HAK-KWL) -- added by LinYong in 20150421
                            {
                                intPos_2 = strPILOT_DEADHEAD_OPS.IndexOf(")", (intPos_3 + 2));

                                DataRow dataRow = dataTable.NewRow();
                                if (indexPILOT_DEADHEAD_OPS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ķ���Ա";
                                else
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ķ���Ա";
                                dataRow["cnvcName"] = strPILOT_DEADHEAD_OPS.Substring(0, (intPos_3 + 1)).Replace("*", "");   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrPILOT_DEADHEAD_OPS_SID[indexPILOT_DEADHEAD_OPS].Substring(0, arrPILOT_DEADHEAD_OPS_SID[indexPILOT_DEADHEAD_OPS].IndexOf("(")).Replace("*", "").Replace(" ", ""); //����֤��
                                dataRow["cnvcDepArrStn"] = strPILOT_DEADHEAD_OPS.Substring((intPos_3 + 2), (intPos_2 - intPos_3 - 2)); //��˺���

                                dataTable.Rows.Add(dataRow);
                            }
                            else if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2)) //�����������������*(HAK-KWL)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexPILOT_DEADHEAD_OPS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ķ���Ա";
                                else
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ķ���Ա";
                                dataRow["cnvcName"] = strPILOT_DEADHEAD_OPS.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrPILOT_DEADHEAD_OPS_SID[indexPILOT_DEADHEAD_OPS].Substring(0, arrPILOT_DEADHEAD_OPS_SID[indexPILOT_DEADHEAD_OPS].IndexOf("(")).Replace("*", "").Replace(" ", ""); //����֤��
                                dataRow["cnvcDepArrStn"] = strPILOT_DEADHEAD_OPS.Substring((intPos_1 + 1), (intPos_2 - intPos_1 -1)); //��˺���

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexPILOT_DEADHEAD_OPS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ķ���Ա";
                                else
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ķ���Ա";
                                dataRow["cnvcName"] = strPILOT_DEADHEAD_OPS.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrPILOT_DEADHEAD_OPS_SID[indexPILOT_DEADHEAD_OPS].Replace("*", "").Replace(" ", ""); //����֤��
                                dataRow["cnvcDepArrStn"] = ""; //��˺���

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("��������ִ�к���ӻ���ķ���Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ִ�к���ӻ���ķ���Ա

                    #region ִ�к���ӻ���ĳ���Ա
                    if (_steward_deadhead_ops.Trim() != "")
                    {

                        string[] arrSTEWARD_DEADHEAD_OPS = _steward_deadhead_ops.Split(new char[2] { '@', '/' });
                        string[] arrSTEWARD_DEADHEAD_OPS_SID = _steward_deadhead_ops_sid.Split(new char[2] { '@', '/' });

                        for (int indexSTEWARD_DEADHEAD_OPS = 0; indexSTEWARD_DEADHEAD_OPS < arrSTEWARD_DEADHEAD_OPS.Length; indexSTEWARD_DEADHEAD_OPS++)
                        {
                            string strSTEWARD_DEADHEAD_OPS = arrSTEWARD_DEADHEAD_OPS[indexSTEWARD_DEADHEAD_OPS];
                            strSTEWARD_DEADHEAD_OPS = strSTEWARD_DEADHEAD_OPS.Replace(" ", "");

                            int intPos_1 = strSTEWARD_DEADHEAD_OPS.IndexOf("(");
                            int intPos_2 = strSTEWARD_DEADHEAD_OPS.IndexOf(")");
                            int intPos_3 = strSTEWARD_DEADHEAD_OPS.IndexOf(")(");

                            if (intPos_3 > 0) //�������������²���*(G)(HAK-KWL) -- added by LinYong in 20150421
                            {
                                intPos_2 = strSTEWARD_DEADHEAD_OPS.IndexOf(")", (intPos_3 + 2));

                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_DEADHEAD_OPS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ĳ���Ա";
                                else
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ĳ���Ա";
                                dataRow["cnvcName"] = strSTEWARD_DEADHEAD_OPS.Substring(0, (intPos_3 + 1)).Replace("*", "");   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSTEWARD_DEADHEAD_OPS_SID[indexSTEWARD_DEADHEAD_OPS].Substring(0, arrSTEWARD_DEADHEAD_OPS_SID[indexSTEWARD_DEADHEAD_OPS].IndexOf("(")).Replace("*","").Replace(" ",""); //����֤��
                                dataRow["cnvcDepArrStn"] = strSTEWARD_DEADHEAD_OPS.Substring((intPos_3 + 2), (intPos_2 - intPos_3 - 2)); //��˺���

                                dataTable.Rows.Add(dataRow);
                            }
                            if ((intPos_1 >= 0) && (intPos_2 >= 0) && (intPos_1 < intPos_2)) //�����������������*(HAK-KWL)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_DEADHEAD_OPS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ĳ���Ա";
                                else
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ĳ���Ա";
                                dataRow["cnvcName"] = strSTEWARD_DEADHEAD_OPS.Substring(0, intPos_1).TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSTEWARD_DEADHEAD_OPS_SID[indexSTEWARD_DEADHEAD_OPS].Substring(0, arrSTEWARD_DEADHEAD_OPS_SID[indexSTEWARD_DEADHEAD_OPS].IndexOf("(")).Replace("*", "").Replace(" ", ""); //����֤��
                                dataRow["cnvcDepArrStn"] = strSTEWARD_DEADHEAD_OPS.Substring((intPos_1 + 1), (intPos_2 - intPos_1 - 1)); //��˺���

                                dataTable.Rows.Add(dataRow);
                            }
                            else if (intPos_1 < 0)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                if (indexSTEWARD_DEADHEAD_OPS == 0)      //λ��
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ĳ���Ա";
                                else
                                    dataRow["cnvcPosition"] = "ִ�к���ӻ���ĳ���Ա";
                                dataRow["cnvcName"] = strSTEWARD_DEADHEAD_OPS.TrimEnd('*');   //����
                                dataRow["cnvcLevel"] = ""; //����
                                dataRow["cnvcSID"] = arrSTEWARD_DEADHEAD_OPS_SID[indexSTEWARD_DEADHEAD_OPS].Replace("*", "").Replace(" ", ""); //����֤��
                                dataRow["cnvcDepArrStn"] = ""; //��˺���

                                dataTable.Rows.Add(dataRow);

                            }
                            else
                            {
                                throw new Exception("��������ִ�к���ӻ���ĳ���Ա��Ϣ�ֽ�ʧ�ܣ���鿴��Ϣ��ɣ�");
                            }

                        }
                    }
                    #endregion ִ�к���ӻ���ĳ���Ա
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        

            //���ؽ��
            if (_memberinfo == null)
            {
                _memberinfo = dataTable;
                return dataTable;
            }
            else
                return _memberinfo;

            #endregion ����ʵ��

        }
        #endregion �������ݱ�������� MemberInfo ��Ϊ NULL����ֱ�ӷ��� MemberInfo������ÿ��һ�еı����ʽ������������Ա����

        #region �������ݱ�������� SegmentInfo ��Ϊ NULL����ֱ�ӷ��� SegmentInfo������ÿ����һ�еı����ʽ���������麽�����ݣ�ע��STD������ʱ��ָ���Ϊ "  "���ǵ��ո�
        /// <summary>
        /// �������ݱ�������� SegmentInfo ��Ϊ NULL����ֱ�ӷ��� SegmentInfo������ÿ����һ�еı����ʽ���������麽�����ݣ�ע��STD������ʱ��ָ���Ϊ "  "���ǵ��ո�
        /// </summary>
        /// <returns>�������ݱ�������� SegmentInfo ��Ϊ NULL����ֱ�ӷ��� SegmentInfo������ÿ����һ�еı����ʽ���������麽�����ݣ�ע��STD������ʱ��ָ���Ϊ "  "���ǵ��ո�</returns>
        public DataTable AnalyseVoyageReport_SegmentInfo()
        {
            #region ��������
            DataTable dataTable = new DataTable();  //��ÿ��һ�еı����ʽ��������������
            DataColumn dataColumn = null;

            #endregion ��������


            #region ����ʵ��
            try
            {
                if (_segmentinfo == null)
                {

                    #region ���ɱ��
                    dataColumn = new DataColumn("cniSegmentInfoId", Type.GetType("System.Int32"));
                    dataColumn.AutoIncrement = true;
                    dataColumn.AutoIncrementSeed = 1;
                    dataColumn.AutoIncrementStep = 1;
                    dataColumn.Caption = "���";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcDATOP", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "��������";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcFLTID", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "�����";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcAC", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "�ɻ���";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcSTD", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "���ʱ��";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcROUTE", Type.GetType("System.String"));
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "����";
                    dataTable.Columns.Add(dataColumn);

                    dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String")); //added by LinYong in 20150324
                    dataColumn.DefaultValue = "";
                    dataColumn.Caption = "��ע";
                    dataTable.Columns.Add(dataColumn);
                    #endregion ���ɱ��

                    //����
                    string[] arrFLTID = _fltids.Split(new char[2] { '@', '/' });
                    string[] arrAC = _ac.Split(new char[2] { '@', '/' });
                    string[] arrSTD = _std.Split(new char[2] { '@', '/' });
                    string[] arrROUTE = _routes.Split(new char[2] { '@', '/' });

                    for (int index = 0; index < arrSTD.Length; index++)
                    {
                        DataRow dataRow = dataTable.NewRow();

                        dataRow["cnvcDATOP"] = arrSTD[index].Trim().Substring(0, 10);   //��������
                        dataRow["cnvcFLTID"] = arrFLTID[index].Trim();   //�����
                        dataRow["cnvcAC"] = arrAC[index].Trim(); //�ɻ���
                        dataRow["cnvcSTD"] = arrSTD[index].Trim(); //���ʱ��
                        dataRow["cnvcROUTE"] = arrROUTE[index].Trim().Substring(0,7); //����

                        dataTable.Rows.Add(dataRow);

                    }


                }

            }
            catch (Exception ex)
            {
                throw new Exception("��ǰ�����麽����Ϣ������" + ex.Message);
            }


            //���ؽ��
            if (_segmentinfo == null)
            {
                _segmentinfo = dataTable;
                return dataTable;
            }
            else
                return _segmentinfo;

            #endregion ����ʵ��

        }
        #endregion �������ݱ�������� SegmentInfo ��Ϊ NULL����ֱ�ӷ��� SegmentInfo������ÿ����һ�еı����ʽ���������麽�����ݣ�ע��STD������ʱ��ָ���Ϊ "  "���ǵ��ո�


        #endregion ����
    }
}
