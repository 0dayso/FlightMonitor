using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;
using System.Collections;

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
    public class NotificationDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NotificationDAF()
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
            FlightMonitorDA.NotificationDA notificationDA = new FlightMonitorDA.NotificationDA();
            DataTable dt = new DataTable();
            notificationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                dt = notificationDA.GetNotificationData(currentDateTime);
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }
            finally
            {
                notificationDA.ConnClose();
            }

            return dt;
        }
        #endregion 查询有效通知数据
    }
}
