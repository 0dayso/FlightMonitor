using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取机场发送的消息
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-04-15
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class MessageServiceDA : SqlDatabase
    {
        #region 获取机场发送的最新消息
        private const string SELECT_GetMessages = "select * from ASUP_DCDM where (cnvcDTTM > @Parm_DTTM) order by cnvcDTTM";

        private SqlParameter[] GetMessagesParameters(string DTTM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_GetMessages);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@Parm_DTTM", SqlDbType.VarChar, 50),
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_GetMessages, parms);
            }

            //
            parms[0].Value = DTTM;

            return parms;
        }

        /// <summary>
        /// 获取机场发送的最新消息
        /// </summary>
        /// <param name="DTTM">消息发送时间（格式如 2015-04-03 22:17:00）</param>
        /// <returns>机场发送的最新消息</returns>
        public DataTable GetMessages(string DTTM)
        {
            SqlParameter[] parms = GetMessagesParameters(DTTM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetMessages, parms);
        }
        #endregion 获取机场发送的最新消息

        #region 获取机场发送的最新消息，使用 自增长值 EventID
        private const string SELECT_GetMessages_1 = "select * from ASUP_DCDM where (EventID > @Parm_EventID) order by EventID asc";

        private SqlParameter[] GetMessagesParameters(int EventID)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_GetMessages_1);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@Parm_EventID", SqlDbType.Int),
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_GetMessages_1, parms);
            }

            //
            parms[0].Value = EventID;

            return parms;
        }

        /// <summary>
        /// 获取机场发送的最新消息，使用 自增长值 EventID
        /// </summary>
        /// <param name="EventID">自增长值</param>
        /// <returns>机场发送的最新消息</returns>
        public DataTable GetMessages(int EventID)
        {
            SqlParameter[] parms = GetMessagesParameters(EventID);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetMessages_1, parms);
        }
        #endregion 获取机场发送的最新消息，使用 自增长值 EventID

    }
}
