using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 航班延误原因外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-13
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class FlightDelayReasonBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FlightDelayReasonBF()
        {
        }

         /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllFlightDelayReason()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightDelayReasonDAF flightDelayReasonDAF = new FlightDelayReasonDAF();

            try
            {
                rvSF.Dt = flightDelayReasonDAF.GetAllFlightDelayReason();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
    }
}
