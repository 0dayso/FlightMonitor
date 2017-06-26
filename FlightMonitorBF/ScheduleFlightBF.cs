using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 航班计划外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ScheduleFlightBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleFlightBF()
        {

        }

        /// <summary>
        /// 加载航班计划
        /// </summary>
        /// <param name="strFullPath">计划文件路径</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF LoadScheduleFlight(string strFullPath)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //航班计划数据访问外观类
            FlightMonitorDA.ScheduleLegsDAF scheduleLegsDAF = new ScheduleLegsDAF();

            try
            {
                //调用数据访问外观层方法
                rvSF.Result = scheduleLegsDAF.LoadScheduleFlight(strFullPath);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_LOAD_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_LOAD_FALSE;
                }
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
