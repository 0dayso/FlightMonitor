using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class DischargingDelayReasonBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DischargingDelayReasonBF()
        {
        }

        /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllDischargingDelayReason()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            DischargingDelayReasonDAF dischargingDelayReasonDAF = new DischargingDelayReasonDAF();

            try
            {
                rvSF.Dt = dischargingDelayReasonDAF.GetAllDischargingDelayReason();
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
