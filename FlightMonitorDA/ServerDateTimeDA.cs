using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.DataHelper;
using System.Data;
using System.Data.SqlClient;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��ȡϵͳʱ��
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ServerDateTimeDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ServerDateTimeDA()
        {
        }

        private const string SELECT_DateTime = "select getdate()";

        /// <summary>
        /// ��ȡϵͳʱ��
        /// </summary>
        /// <returns></returns>
        public object GetServerDateTime()
        {
            return SqlHelper.ExecuteScalar(this.SqlConn, CommandType.Text, SELECT_DateTime);
        }
    }
}
