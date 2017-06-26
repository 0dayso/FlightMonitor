using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 系统时间外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ServerDateTimeBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServerDateTimeBF()
        {
        }

         /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetServerDateTime()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            ServerDateTimeDAF serverDateTimeDAF = new ServerDateTimeDAF();

            //调用数据访问外观层方法
            try
            {
                rvSF.Message = Convert.ToString(serverDateTimeDAF.GetServerDateTime());
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }
            return rvSF;
        }
    }
}
