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
    /// ������Ա������Ϣ����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2016-03-21
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CommanderTypeDA : SqlDatabase
    {
        #region ��ȡ������Ա������Ϣ
        /// <summary>
        /// ��ȡ������Ա������Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbCommanderType");
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion ��ȡ������Ա������Ϣ
    }
}
