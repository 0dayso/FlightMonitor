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
    /// 保障人员类型信息操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2016-03-21
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CommanderTypeDA : SqlDatabase
    {
        #region 获取保障人员类型信息
        /// <summary>
        /// 获取保障人员类型信息
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbCommanderType");
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion 获取保障人员类型信息
    }
}
