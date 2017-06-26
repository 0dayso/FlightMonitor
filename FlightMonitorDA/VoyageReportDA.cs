using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取航前任务书信息
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2013-09-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class VoyageReportDA : SqlDatabase
    {
        #region 获取航前任务书信息，获取时间段内的信息
        private const string SELECT_GetVoyageReportData = "select * from tbFltReport where (DATOP >= @Parm_DATOP_Start) and (DATOP < @Parm_DATOP_End) order by DATOP";

        private SqlParameter[] GetVoyageReportDataParameters(DateTime DATOP_Start, DateTime DATOP_End)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_GetVoyageReportData);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@Parm_DATOP_Start", SqlDbType.VarChar, 200),
                    new SqlParameter("@Parm_DATOP_End", SqlDbType.VarChar, 200),
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_GetVoyageReportData, parms);
            }

            ///由于航前任务书数据源的 DATOP 的格式 为 字符串，且日期和时间使用字符“  ”分割，所以，参数的时间转换成字符串时也需使用“  ”分割
            parms[0].Value = DATOP_Start.ToString("yyyy-MM-dd  HH:mm");
            parms[1].Value = DATOP_End.ToString("yyyy-MM-dd  HH:mm");

            return parms;
        }

        /// <summary>
        /// 获取航前任务书信息，获取时间段内的信息
        /// </summary>
        /// <param name="DATOP_Start">航班日期，开始</param>
        /// <param name="DATOP_End">航班日期，截止</param>
        /// <returns></returns>
        public DataTable GetVoyageReportData(DateTime DATOP_Start, DateTime DATOP_End)
        {
            SqlParameter[] parms = GetVoyageReportDataParameters(DATOP_Start, DATOP_End);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetVoyageReportData, parms);
        }
        #endregion


        #region 获取航前任务书信息，获取单个航班的信息
        private const string SELECT_GetVoyageReportDataBySingleFlight = "select * from tbFltReport where (STD like '%' + @Parm_DATOP + '%') and " +
            "(FLTIDS like '%' + @Parm_FLTIDS + '%') and " +
            "(AC like '%' + @Parm_AC + '%') and " +
            "(ROUTES like '%' + @Parm_ROUTES + '%') " +
            " order by DATOP";

        private SqlParameter[] GetVoyageReportDataBySingleFlightParameters(string DATOP, string FLTIDS, string AC, string ROUTES)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_GetVoyageReportDataBySingleFlight);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@Parm_DATOP", SqlDbType.VarChar, 200),
                    new SqlParameter("@Parm_FLTIDS", SqlDbType.VarChar, 200),
                    new SqlParameter("@Parm_AC", SqlDbType.VarChar, 200),
                    new SqlParameter("@Parm_ROUTES", SqlDbType.VarChar, 200),
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_GetVoyageReportDataBySingleFlight, parms);
            }

            ///由于航前任务书数据源的 DATOP 的格式 为 字符串，且日期和时间使用字符“  ”分割，所以，参数的时间转换成字符串时也需使用“  ”分割
            parms[0].Value = DATOP;
            parms[1].Value = FLTIDS;
            parms[2].Value = AC;
            parms[3].Value = ROUTES;

            return parms;
        }

        /// <summary>
        /// 获取航前任务书信息，获取单个航班的信息
        /// </summary>
        /// <param name="DATOP_Start">航班日期，开始</param>
        /// <param name="DATOP_End">航班日期，截止</param>
        /// <returns></returns>
        public DataTable GetVoyageReportDataBySingleFlight(string DATOP, string FLTIDS, string AC, string ROUTES)
        {
            SqlParameter[] parms = GetVoyageReportDataBySingleFlightParameters( DATOP,  FLTIDS,  AC, ROUTES);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetVoyageReportDataBySingleFlight, parms);
        }
        #endregion











    }
}
