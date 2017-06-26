using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航班延误原因数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06－14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class FlightDelayReasonDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FlightDelayReasonDAF()
        {
        }

        /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllFlightDelayReason()
        {
            //定义返回值
            DataTable dtFlightDelayReason = new DataTable();
            FlightDelayReasonDA flightDelayReasonDA = new FlightDelayReasonDA();

            try
            {
                //打开数据库联机
                flightDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlightDelayReason = flightDelayReasonDA.GetAllFlightDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightDelayReasonDA.ConnClose();
            }

            return dtFlightDelayReason;
        }
    }
}
