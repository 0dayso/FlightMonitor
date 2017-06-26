using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��ȡ�������͵���Ϣ
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-04-15
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class MessageServiceDA : SqlDatabase
    {
        #region ��ȡ�������͵�������Ϣ
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
        /// ��ȡ�������͵�������Ϣ
        /// </summary>
        /// <param name="DTTM">��Ϣ����ʱ�䣨��ʽ�� 2015-04-03 22:17:00��</param>
        /// <returns>�������͵�������Ϣ</returns>
        public DataTable GetMessages(string DTTM)
        {
            SqlParameter[] parms = GetMessagesParameters(DTTM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetMessages, parms);
        }
        #endregion ��ȡ�������͵�������Ϣ

        #region ��ȡ�������͵�������Ϣ��ʹ�� ������ֵ EventID
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
        /// ��ȡ�������͵�������Ϣ��ʹ�� ������ֵ EventID
        /// </summary>
        /// <param name="EventID">������ֵ</param>
        /// <returns>�������͵�������Ϣ</returns>
        public DataTable GetMessages(int EventID)
        {
            SqlParameter[] parms = GetMessagesParameters(EventID);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetMessages_1, parms);
        }
        #endregion ��ȡ�������͵�������Ϣ��ʹ�� ������ֵ EventID

    }
}
