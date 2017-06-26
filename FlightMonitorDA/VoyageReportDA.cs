using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��ȡ��ǰ��������Ϣ
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-09-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class VoyageReportDA : SqlDatabase
    {
        #region ��ȡ��ǰ��������Ϣ����ȡʱ����ڵ���Ϣ
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

            ///���ں�ǰ����������Դ�� DATOP �ĸ�ʽ Ϊ �ַ����������ں�ʱ��ʹ���ַ���  ���ָ���ԣ�������ʱ��ת�����ַ���ʱҲ��ʹ�á�  ���ָ�
            parms[0].Value = DATOP_Start.ToString("yyyy-MM-dd  HH:mm");
            parms[1].Value = DATOP_End.ToString("yyyy-MM-dd  HH:mm");

            return parms;
        }

        /// <summary>
        /// ��ȡ��ǰ��������Ϣ����ȡʱ����ڵ���Ϣ
        /// </summary>
        /// <param name="DATOP_Start">�������ڣ���ʼ</param>
        /// <param name="DATOP_End">�������ڣ���ֹ</param>
        /// <returns></returns>
        public DataTable GetVoyageReportData(DateTime DATOP_Start, DateTime DATOP_End)
        {
            SqlParameter[] parms = GetVoyageReportDataParameters(DATOP_Start, DATOP_End);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetVoyageReportData, parms);
        }
        #endregion


        #region ��ȡ��ǰ��������Ϣ����ȡ�����������Ϣ
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

            ///���ں�ǰ����������Դ�� DATOP �ĸ�ʽ Ϊ �ַ����������ں�ʱ��ʹ���ַ���  ���ָ���ԣ�������ʱ��ת�����ַ���ʱҲ��ʹ�á�  ���ָ�
            parms[0].Value = DATOP;
            parms[1].Value = FLTIDS;
            parms[2].Value = AC;
            parms[3].Value = ROUTES;

            return parms;
        }

        /// <summary>
        /// ��ȡ��ǰ��������Ϣ����ȡ�����������Ϣ
        /// </summary>
        /// <param name="DATOP_Start">�������ڣ���ʼ</param>
        /// <param name="DATOP_End">�������ڣ���ֹ</param>
        /// <returns></returns>
        public DataTable GetVoyageReportDataBySingleFlight(string DATOP, string FLTIDS, string AC, string ROUTES)
        {
            SqlParameter[] parms = GetVoyageReportDataBySingleFlightParameters( DATOP,  FLTIDS,  AC, ROUTES);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetVoyageReportDataBySingleFlight, parms);
        }
        #endregion











    }
}
