using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Configuration;
using System.Data;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 航站过站时间信息访问服务
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class OverStationTimeBF
    {
        #region 获取航班告警信息记录
        /// <summary>
        /// 获取航班告警信息记录
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Select()
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //定义返回值
            try
            {
                //定义数据访问层外观类方法
                OverStationTimeDAF overStationTimeDAF = new OverStationTimeDAF();
                rvSF.Dt = overStationTimeDAF.Select();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion 获取航班告警信息记录
    }
}
