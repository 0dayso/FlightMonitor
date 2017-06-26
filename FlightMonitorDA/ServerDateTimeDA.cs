using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.DataHelper;
using System.Data;
using System.Data.SqlClient;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取系统时间
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ServerDateTimeDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServerDateTimeDA()
        {
        }

        private const string SELECT_DateTime = "select getdate()";

        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public object GetServerDateTime()
        {
            return SqlHelper.ExecuteScalar(this.SqlConn, CommandType.Text, SELECT_DateTime);
        }
    }
}
