using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;


namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class InOutDelayReasonBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InOutDelayReasonBF()
        {
        }

        /// <summary>
        /// 获取所有进出港延误原因代码
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllInOutDelayReason()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            InOutDelayReasonDAF inOutDelayReasonDAF = new InOutDelayReasonDAF();

            try
            {
                rvSF.Dt = inOutDelayReasonDAF.GetAllInOutDelayReason();
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
