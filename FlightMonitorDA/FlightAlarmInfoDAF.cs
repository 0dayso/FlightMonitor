using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航班告警信息访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-08
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class FlightAlarmInfoDAF
    {
        #region 插入航班告警信息记录
        /// <summary>
        /// 插入航班告警信息记录
        /// </summary>
        /// <param name="flightAlarmInfoBM">航班告警信息实体对象，提供参数信息</param>
        /// <returns></returns>
        public int Add(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //定义返回值
            int retVal = -1;
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            //打开数据库连接
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                return flightAlarmInfoDA.Add(flightAlarmInfoBM);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return retVal;
        }
        #endregion 插入航班告警信息记录

        #region 更新航班告警信息记录
        /// <summary>
        /// 更新航班告警信息记录
        /// </summary>
        /// <param name="flightAlarmInfoBM">航班告警信息实体对象，提供参数信息</param>
        /// <returns></returns>
        public int Update(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //定义返回值
            int retVal = -1;
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            //打开数据库连接
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                return flightAlarmInfoDA.Update(flightAlarmInfoBM);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return retVal;
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
        public DataTable Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC)
        {
            //定义返回值
            DataTable dataTable = new DataTable();
            //打开数据库连接
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = flightAlarmInfoDA.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return dataTable;
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
        public DataTable Select(
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
            //定义返回值
            DataTable dataTable = new DataTable();
            //打开数据库连接
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = flightAlarmInfoDA.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC,
                    cnvcAlarmCode);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return dataTable;
        }
        #endregion 根据关键参数获取航班告警信息记录

        #region 根据航班日期区间参数获取航班告警信息记录
        /// <summary>
        /// 根据航班日期区间参数获取航班告警信息记录
        /// </summary>
        /// <param name="FlightDate_Start">开始日期</param>
        /// <param name="FlightDate_End">结束日期</param>
        /// <returns></returns>
        public DataTable Select(
            string FlightDate_Start,
            string FlightDate_End)
        {
            //定义返回值
            DataTable dataTable = new DataTable();
            //打开数据库连接
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = flightAlarmInfoDA.Select(
                    FlightDate_Start,
                    FlightDate_End);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return dataTable;
        }
        #endregion 根据航班日期区间参数获取航班告警信息记录


    }
}
