using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 备降返航原因外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class DiversionDelayReasonBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DiversionDelayReasonBF()
        {
        }

        /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllDiversionDelayReason()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            DiversionDelayReasonDAF diversionDelayReasonDAF = new DiversionDelayReasonDAF();

            try
            {
                rvSF.Dt = diversionDelayReasonDAF.GetAllDiversionDelayReason();
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
