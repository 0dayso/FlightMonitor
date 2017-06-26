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
    /// 获取停机位信息数据访问服务
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-09-03
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GateInfoBF
    {
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="StationThreeCode">机场三字码</param>
        /// <returns></returns>
        public ReturnValueSF GetList(string StationThreeCode)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            GateInfoDAF gateInfoDAF = new GateInfoDAF();

            try
            {
                rvSF.Dt = gateInfoDAF.GetList(StationThreeCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion 获得数据列表

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="DataTable">需要变更的数据表</param>
        /// <returns></returns>
        public ReturnValueSF Save(DataTable DataTable)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            GateInfoDAF gateInfoDAF = new GateInfoDAF();

            try
            {
                rvSF.Result = gateInfoDAF.Save(DataTable);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;

        }
        #endregion 保存数据

    }
}
