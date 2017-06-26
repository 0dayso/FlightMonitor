using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航站数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-24
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class StationDAF
    {
        public DataTable GetAllStation()
        {
            DataTable dt = new DataTable(); //定义返回值

            StationDA stationDA = new StationDA();
            //打开数据库连接
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                dt = stationDA.GetAllStation();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return dt;
        }

        /// <summary>
        /// 根据机场三字码获取航站信息
        /// </summary>
        /// <param name="strStationThreeCode">航站三字码</param>
        /// <returns></returns>
        public DataTable GetStationByThreeCode(string strStationThreeCode)
        {
            DataTable dt = new DataTable(); //定义返回值

            StationDA stationDA = new StationDA();
            //打开数据库连接
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                dt = stationDA.GetStationByThreeCode(strStationThreeCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return dt;
        }

         /// <summary>
        /// 添加一个航站
        /// </summary>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public int InsertStation(StationBM stationBM)
        {
            int retVal = -1;

            StationDA stationDA = new StationDA();
            //打开数据库连接
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                retVal = stationDA.InsertStation(stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return retVal;
        }

         /// <summary>
        /// 删除一个航站
        /// </summary>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public int DeleteStation(StationBM stationBM)
        {
            int retVal = -1;

            StationDA stationDA = new StationDA();
            //打开数据库连接
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                retVal = stationDA.DeleteStation(stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return retVal;
        }
    }
}
