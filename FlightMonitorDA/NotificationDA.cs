using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 通知数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2013-12-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class NotificationDA : SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NotificationDA()
        {
        }

        #region 查询有效通知数据
        /// <summary>
        /// 查询有效通知数据
        /// </summary>
        /// <param name="currentDateTime">当前时间</param>
        /// <returns>返回当前时间在生效起止时间段的通知数据</returns>
        public DataTable GetNotificationData(DateTime currentDateTime)
        {
            string strSql = "select * from tbNotification where (cndEffectiveDate_Start <= '" 
                + currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                + "') and (cndEffectiveDate_End >= '" 
                + currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                + "') order by [cniPriorityLevel] desc";

            DataTable dt = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql);

            return dt;

        }
        #endregion 查询有效通知数据

    }
}
