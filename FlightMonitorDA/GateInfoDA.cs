using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取停机位信息
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-09-02
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GateInfoDA : SqlDatabase
    {
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="StationThreeCode">机场三字码</param>
        /// <returns></returns>
        public DataTable GetList(string StationThreeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cncStationThreeCode,cnvcAirportName,cnvcStationName,cnvcGate,cnvcGateProperty,cnvcMemo,cnvcOperationUser,cndOperationTime ");
            strSql.Append(" FROM tbGateInfo ");
            strSql.Append(" where cncStationThreeCode = '" + StationThreeCode + "'");

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion 获得数据列表

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="DataTable">需要变更的数据表</param>
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
            //更新
            return sqlDataAdapter.Update(DataTable);
        }
        #endregion 保存数据
    }
}
