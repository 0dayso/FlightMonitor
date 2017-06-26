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
    /// 航班告警信息访问服务
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-08
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class FlightAlarmInfoBF
    {
        #region 插入航班告警信息记录
        /// <summary>
        /// 插入航班告警信息记录
        /// </summary>
        /// <param name="flightAlarmInfoBM">航班告警信息实体对象，提供参数信息</param>
        /// <returns></returns>
        public ReturnValueSF Add(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();

            //调用数据访问外观层方法
            try
            {
                rvSF.Result = flightAlarmInfoDAF.Add(flightAlarmInfoBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_ADD_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_ADD_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }
            return rvSF;
        }
        #endregion 插入航班告警信息记录

        #region 更新航班告警信息记录
        /// <summary>
        /// 更新航班告警信息记录
        /// </summary>
        /// <param name="flightAlarmInfoBM">航班告警信息实体对象，提供参数信息</param>
        /// <returns></returns>
        public ReturnValueSF Update(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();

            //调用数据访问外观层方法
            try
            {
                rvSF.Result = flightAlarmInfoDAF.Update(flightAlarmInfoBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_ADD_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_ADD_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }
            return rvSF;
        }
        #endregion 更新航班告警信息记录

        #region 根据关键参数获取航班告警信息记录
        /// <summary>
        /// 根据关键参数获取航班告警信息记录
        /// </summary>
        /// <param name="cncOutDATOP"></param>
        /// <param name="cnvcOutFLTID"></param>
        /// <param name="cniOutLEGNO"></param>
        /// <param name="cnvcOutAC"></param>
        /// <param name="cncInDATOP"></param>
        /// <param name="cnvcInFLTID"></param>
        /// <param name="cniInLEGNO"></param>
        /// <param name="cnvcInAC"></param>
        /// <returns></returns>
        public ReturnValueSF Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //定义返回值
            try
            {
                //定义数据访问层外观类方法
                FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();
                rvSF.Dt = flightAlarmInfoDAF.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion 根据关键参数获取航班告警信息记录

        #region 根据关键参数获取航班告警信息记录
        /// <summary>
        /// 根据关键参数获取航班告警信息记录
        /// </summary>
        /// <param name="cncOutDATOP"></param>
        /// <param name="cnvcOutFLTID"></param>
        /// <param name="cniOutLEGNO"></param>
        /// <param name="cnvcOutAC"></param>
        /// <param name="cncInDATOP"></param>
        /// <param name="cnvcInFLTID"></param>
        /// <param name="cniInLEGNO"></param>
        /// <param name="cnvcInAC"></param>
        /// <returns></returns>
        public ReturnValueSF Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC,
            string cnvcAlarmCode)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //定义返回值
            try
            {
                //定义数据访问层外观类方法
                FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();
                rvSF.Dt = flightAlarmInfoDAF.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC,
                    cnvcAlarmCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion 根据关键参数获取航班告警信息记录

        #region 根据航班日期区间参数获取航班告警信息记录
        /// <summary>
        /// 根据航班日期区间参数获取航班告警信息记录
        /// </summary>
        /// <param name="FlightDate_Start">开始日期</param>
        /// <param name="FlightDate_End">结束日期</param>
        /// <returns></returns>
        public ReturnValueSF Select(
            string FlightDate_Start,
            string FlightDate_End)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //定义返回值
            try
            {
                //定义数据访问层外观类方法
                FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();
                rvSF.Dt = flightAlarmInfoDAF.Select(
                    FlightDate_Start,
                    FlightDate_End);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion 根据航班日期区间参数获取航班告警信息记录
    }
}
