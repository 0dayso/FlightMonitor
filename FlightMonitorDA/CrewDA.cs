using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 机长连飞信息数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-07-03
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CrewDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CrewDA()
        {
        }

        #region 获取某航班的机组信息
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
        /// 获取航班机组信息
        /// </summary>
        /// <param name="strFlightDate">航班日期 北京时间</param>
        /// <param name="strFlightNo">航班号,不含空格</param>
        /// <param name="strSEG">航段</param>       
        /// <returns></returns>
        public DataTable GetCrewInforByFlightNo(string strFlightDate, string strFlightNo, string strSEG)
        {
            SqlParameter[] parms = GetCrewInforByFlightNoParameters(strFlightDate, strFlightNo, strSEG);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CrewInforByFlightNo, parms);
        }
        #endregion

        #region 根据出港航班信息获取该机长前飞航班机组信息
        //private const string SELECT_CrewInforByCaptain = "select * from NET_AIRMAN_MASTERPLAN where AF_DATE=@PARM_AF_DATE and " + 
        //    "(AF_SEG like '%' + @PARM_AF_SEG or AF_SEG like '%' + @PARM_AF_SEG_ALL + '%')  and (AF_CAPTAIN = @PAMR_CAPTAIN OR AF_COPILOT1 = @PAMR_CAPTAIN OR AF_COPILOT2 = @PAMR_CAPTAIN) and (AF_BEGIN = @PARM_AF_BEGIN AND AF_END = @PARM_AF_END OR AF_END < @PARM_AF_BEGIN) order by AF_BEGIN asc";
        //查询当天该机长执行的所有航班中，到达机场为出港航班起飞机场，同时起飞时间小于出港航班的航班
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
        /// 根据机长获取前飞航班信息
        /// </summary>
        /// <param name="strFlightDate">航班日期（北京时间）</param>
        /// <param name="strStaffId">机长工号</param>
        /// <param name="strArrStn">前飞航班的到达机场，即出港航班的起飞机场</param>
        /// <param name="strDepartTime">出港航班的起飞时间</param>
        /// <returns></returns>
        public DataTable GetCrewInforByCaptain(string strFlightDate, string strStaffId, string strArrStn, string strDepartTime)
        {
            SqlParameter[] parms = GetCrewInforByCaptainParameters(strFlightDate, strStaffId, strArrStn, strDepartTime);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CrewInforByCaptain, parms);
        }
        #endregion


    }
}
