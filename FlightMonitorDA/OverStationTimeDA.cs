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
    /// 航站过站时间信息
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class OverStationTimeDA : SqlDatabase
    {
        #region 获取航站过站时间信息记录
        /// <summary>
        /// 获取航站过站时间信息记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbOverStationTime");
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion 获取航站过站时间信息记录
    }
}
