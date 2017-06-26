using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 系统时间数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06－14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ServerDateTimeDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServerDateTimeDAF()
        {
        }

         /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public object GetServerDateTime()
        {
            //定义返回值
            object oServerTime = new object();
            ServerDateTimeDA serverDateTimeDA = new ServerDateTimeDA();

            try
            {
                //打开数据库联机
                serverDateTimeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                oServerTime = serverDateTimeDA.GetServerDateTime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serverDateTimeDA.ConnClose();
            }

            return oServerTime;
        }
    }
}
