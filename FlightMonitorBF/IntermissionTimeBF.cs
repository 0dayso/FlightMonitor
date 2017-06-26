using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 标准过站时间业务外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class IntermissionTimeBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IntermissionTimeBF()
        {
        }

        /// <summary>
        /// 获取所有机型的标准过站时间
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetStandardIntermissionTime()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            IntermissionTimeDAF intermissionTimeDAF = new IntermissionTimeDAF();

            try
            {
                rvSF.Dt = intermissionTimeDAF.GetStandardIntermissionTime();
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
