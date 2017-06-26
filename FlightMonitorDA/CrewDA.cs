using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ����������Ϣ���ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-07-03
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CrewDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CrewDA()
        {
        }

        #region ��ȡĳ����Ļ�����Ϣ
        //private const string SELECT_CrewInforByFlightNo = "select * from NET_AIRMAN_MASTERPLAN where AF_DATE=@PARM_AF_DATE and AP_NUMBER like '%' + @PARM_AP_NUMBER  + '%'" + 
        //    " and AF_SEG like '%' + @PARM_AF_SEG + '%'";
        private const string SELECT_CrewInforByFlightNo = "select * from tbDutyRoster where date = @Parm_FlightDate and flightNo like '%' + @Parm_FlightNo + '%' and sector = @Parm_Sector and rankid = 1";

        private SqlParameter[] GetCrewInforByFlightNoParameters(string strFlightDate, string strFlightNo, string strSEG)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_CrewInforByFlightNo);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    //new SqlParameter("@PARM_AF_DATE", SqlDbType.DateTime, 0),
                    //new SqlParameter("@PARM_AP_NUMBER", SqlDbType.VarChar, 15),
                    //new SqlParameter("@PARM_AF_SEG", SqlDbType.VarChar, 100)                    
                    new SqlParameter("@Parm_FlightDate", SqlDbType.DateTime, 0),
                    new SqlParameter("@Parm_FlightNo", SqlDbType.VarChar, 15),
                    new SqlParameter("@Parm_Sector", SqlDbType.VarChar, 100)   
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_CrewInforByFlightNo, parms);
            }

            parms[0].Value = strFlightDate;
            parms[1].Value = strFlightNo;
            parms[2].Value = strSEG;

            return parms;
        }

        /// <summary>
        /// ��ȡ���������Ϣ
        /// </summary>
        /// <param name="strFlightDate">�������� ����ʱ��</param>
        /// <param name="strFlightNo">�����,�����ո�</param>
        /// <param name="strSEG">����</param>       
        /// <returns></returns>
        public DataTable GetCrewInforByFlightNo(string strFlightDate, string strFlightNo, string strSEG)
        {
            SqlParameter[] parms = GetCrewInforByFlightNoParameters(strFlightDate, strFlightNo, strSEG);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CrewInforByFlightNo, parms);
        }
        #endregion

        #region ���ݳ��ۺ�����Ϣ��ȡ�û���ǰ�ɺ��������Ϣ
        //private const string SELECT_CrewInforByCaptain = "select * from NET_AIRMAN_MASTERPLAN where AF_DATE=@PARM_AF_DATE and " + 
        //    "(AF_SEG like '%' + @PARM_AF_SEG or AF_SEG like '%' + @PARM_AF_SEG_ALL + '%')  and (AF_CAPTAIN = @PAMR_CAPTAIN OR AF_COPILOT1 = @PAMR_CAPTAIN OR AF_COPILOT2 = @PAMR_CAPTAIN) and (AF_BEGIN = @PARM_AF_BEGIN AND AF_END = @PARM_AF_END OR AF_END < @PARM_AF_BEGIN) order by AF_BEGIN asc";
        //��ѯ����û���ִ�е����к����У��������Ϊ���ۺ�����ɻ�����ͬʱ���ʱ��С�ڳ��ۺ���ĺ���
        private const string SELECT_CrewInforByCaptain = "select * from tbDutyRoster where date = @Parm_FlightDate  and departtime < @Parm_DepartTime and staffid = @Parm_StaffId and sector like '%' + @Parm_DepStn order by departtime desc";
        //private SqlParameter[] GetCrewInforByCaptainParameters(string strFlightDate, string strSEG, string strSEGALL, string strCaptain, string strBeginTime, string strEndTime)
        //{
        //    SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_CrewInforByCaptain);
        //    if (parms == null)
        //    {
        //        parms = new SqlParameter[]
        //        {
        //            new SqlParameter("@PARM_AF_DATE", SqlDbType.DateTime, 0),                    
        //            new SqlParameter("@PARM_AF_SEG", SqlDbType.VarChar, 100),
        //            new SqlParameter("@PARM_AF_SEG_ALL", SqlDbType.VarChar, 100),
        //            new SqlParameter("@PAMR_CAPTAIN", SqlDbType.VarChar, 20),
        //            new SqlParameter("@PARM_AF_BEGIN", SqlDbType.DateTime, 0),
        //            new SqlParameter("@PARM_AF_END", SqlDbType.DateTime, 0)
        //        };

        //        SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_CrewInforByCaptain, parms);
        //    }

        //    parms[0].Value = strFlightDate;
        //    parms[1].Value = strSEG;
        //    parms[2].Value = strSEGALL;
        //    parms[3].Value = strCaptain;
        //    parms[4].Value = strBeginTime;
        //    parms[5].Value = strEndTime;

        //    return parms;
        //}
        private SqlParameter[] GetCrewInforByCaptainParameters(string strFlightDate, string strStaffId, string strArrStn, string strDepartTime)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_CrewInforByCaptain);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@Parm_FlightDate", SqlDbType.DateTime, 0),                    
                    new SqlParameter("@Parm_DepStn", SqlDbType.VarChar, 100),
                    new SqlParameter("@Parm_StaffId", SqlDbType.VarChar, 20),
                    new SqlParameter("@Parm_DepartTime", SqlDbType.VarChar, 10)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_CrewInforByCaptain, parms);
            }

            parms[0].Value = strFlightDate;
            parms[1].Value = strArrStn;
            parms[2].Value = strStaffId;
            parms[3].Value = strDepartTime;

            return parms;
        }

        
        //public DataTable GetCrewInforByCaptain(string strFlightDate, string strSEG, string strSEGALL,string strCaptain, string strBeginTime, string strEndTime)
        //{
        //    SqlParameter[] parms = GetCrewInforByCaptainParameters(strFlightDate, strSEG, strSEGALL, strCaptain, strBeginTime, strEndTime);

        //    return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CrewInforByCaptain, parms);
        //}
        /// <summary>
        /// ���ݻ�����ȡǰ�ɺ�����Ϣ
        /// </summary>
        /// <param name="strFlightDate">�������ڣ�����ʱ�䣩</param>
        /// <param name="strStaffId">��������</param>
        /// <param name="strArrStn">ǰ�ɺ���ĵ�������������ۺ������ɻ���</param>
        /// <param name="strDepartTime">���ۺ�������ʱ��</param>
        /// <returns></returns>
        public DataTable GetCrewInforByCaptain(string strFlightDate, string strStaffId, string strArrStn, string strDepartTime)
        {
            SqlParameter[] parms = GetCrewInforByCaptainParameters(strFlightDate, strStaffId, strArrStn, strDepartTime);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CrewInforByCaptain, parms);
        }
        #endregion


    }
}
