using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��վ��վʱ����Ϣ
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-07-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class OverStationTimeDA : SqlDatabase
    {
        #region ��ȡ��վ��վʱ����Ϣ��¼
        /// <summary>
        /// ��ȡ��վ��վʱ����Ϣ��¼
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbOverStationTime");
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion ��ȡ��վ��վʱ����Ϣ��¼
    }
}
