using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航班延误原因数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class FlightDelayReasonDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FlightDelayReasonDA()
        {
        }

        #region 获取所有航班延误代码
        private const string SELECT_AllFlightDelayReason = "SELECT * FROM tbFlightDelayReason";
        
        /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllFlightDelayReason()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllFlightDelayReason);
        }
        #endregion
    }
}
