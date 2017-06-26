using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航班操作数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-04
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ChangeRecordDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChangeRecordDAF()
        {
        }

        /// <summary>
        /// 获取最后一批变更数据
        /// </summary>
        /// <param name="iLastRecordNo">系统已经处理的最大的变更序号</param>
        /// <returns></returns>
        public DataTable GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //定义返回值
            DataTable dtLastChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtLastChangeRecords = changeRecordDA.GetLastWatchChangeRecords(iLastRecordNo, dateTimeBM, positionNameBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtLastChangeRecords;
        }

         /// <summary>
        /// 获取最大变更记录号
        /// </summary>
        /// <returns></returns>
        public object GetMaxRecordNo()
        {
            //定义返回值
            object oMaxRecordNo;

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                oMaxRecordNo = changeRecordDA.GetMaxRecordNo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return oMaxRecordNo;
        }

        /// <summary>
        /// 航站获取最后一批变更数据
        /// </summary>
        /// <param name="iLastRecordNo">系统已经处理的最大的变更序号</param>
        /// <returns></returns>
        public DataTable GetLastGuaranteeChangeRecords(int iLastRecordNo,DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            //定义返回值
            DataTable dtLastChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                if (ConfigurationManager.AppSettings[accountBM.StationThreeCode + "DistSrv"] != null)
                {
                    string strConn = ConfigurationManager.AppSettings[stationBM.ThreeCode + "DistSrv"];
                    //解密数据库连接字符串
                    byte[] bt = Convert.FromBase64String(strConn);
                    strConn = Encoding.ASCII.GetString(bt);
                    changeRecordDA.GetConnOpen(strConn);
                }
                else
                {
                    changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                }
                //changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtLastChangeRecords = changeRecordDA.GetLastGuaranteeChangeRecords(iLastRecordNo, dateTimeBM, stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtLastChangeRecords;
        }

        /// <summary>
        /// 获取航站最新100条变更记录
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public DataTable GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //定义返回值
            DataTable dtChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtChangeRecords = changeRecordDA.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtChangeRecords;
        }

         /// <summary>
        /// 插入一条航班操作记录
        /// </summary>
        /// <param name="changeRecordBM">航班操作记录实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        public int Insert(FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            //定义返回值
            int retVal = -1;

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = changeRecordDA.Insert(changeRecordBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return retVal;
        }

       
        /// <summary>
        /// 根据航班日期、航班号和变更类型查询变更记录
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strFlightNo"></param>
        /// <param name="strChangeReason"></param>
        /// <returns></returns>
        public DataTable GetChangeRecordsByFlightNo(DateTimeBM dateTimeBM, string strFlightNo, string strChangeReason)
        {
            //定义返回值
            DataTable dtChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtChangeRecords = changeRecordDA.GetChangeRecordsByFlightNo(dateTimeBM, strFlightNo, strChangeReason);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtChangeRecords;
        }


        #region added by LinYong

        #region 获取最后一批变更数据 （vw_FlightChangeRecord） --added in 2009.10.28
       /// <summary>
        /// 获取最后一批变更数据 （vw_FlightChangeRecord）
       /// </summary>
        /// <param name="dateTimeBM">时间对象，属性 StartDateTime 为提取的时间点</param>
       /// <returns></returns>
        public DataTable GetLastvw_FlightChangeRecord(DateTimeBM dateTimeBM)
        {
            //定义返回值
            DataTable dtLastChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                //if (ConfigurationManager.AppSettings[accountBM.StationThreeCode + "DistSrv"] != null)
                //{
                //    string strConn = ConfigurationManager.AppSettings[stationBM.ThreeCode + "DistSrv"];
                //    //解密数据库连接字符串
                //    byte[] bt = Convert.FromBase64String(strConn);
                //    strConn = Encoding.ASCII.GetString(bt);
                //    changeRecordDA.GetConnOpen(strConn);
                //}
                //else
                //{
                //    changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                //}
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtLastChangeRecords = changeRecordDA.GetLastvw_FlightChangeRecord(dateTimeBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtLastChangeRecords;
        }
        #endregion

        #region 获取最后一条记录（指定用户） -- added by LinYong in 20150420
        /// <summary>
        /// 获取最后一条记录（指定用户）
        /// </summary>
        /// <param name="UserID">用户帐号</param>
        /// <returns></returns>
        public DataTable GetLastRecord(string UserID)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //打开数据库连接
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeRecordDA.GetLastRecord(UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }
            return dataTable;
        }
        #endregion 获取最后一条记录（指定用户） -- added by LinYong in 20150420


        #endregion
    }
}
