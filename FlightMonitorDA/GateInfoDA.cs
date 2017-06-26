using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��ȡͣ��λ��Ϣ
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-09-02
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GateInfoDA : SqlDatabase
    {
        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="StationThreeCode">����������</param>
        /// <returns></returns>
        public DataTable GetList(string StationThreeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cncStationThreeCode,cnvcAirportName,cnvcStationName,cnvcGate,cnvcGateProperty,cnvcMemo,cnvcOperationUser,cndOperationTime ");
            strSql.Append(" FROM tbGateInfo ");
            strSql.Append(" where cncStationThreeCode = '" + StationThreeCode + "'");

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion ��������б�

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="DataTable">��Ҫ��������ݱ�</param>
        /// <returns></returns>
        public int Save(DataTable DataTable)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = this.SqlConn;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * FROM [dbo].[tbGateInfo] where [cncStationThreeCode] = ''";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            //����
            return sqlDataAdapter.Update(DataTable);
        }
        #endregion ��������
    }
}
