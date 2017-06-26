using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 席位名称数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionNameDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionNameDAF()
        {
        }

        /// <summary>
        /// 获取所有席位名称
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPositionName()
        {
            //定义返回值
            DataTable dtPositionName = new DataTable();

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //打开数据库连接
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtPositionName = positionNameDA.GetAllPositionName();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return dtPositionName;
        }

        /// <summary>
        /// 插入一个席位
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public int InsertPositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //定义返回值
            int retVal = -1;

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //打开数据库连接
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = positionNameDA.InsertPositionName(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 删除一个席位名称
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public int DeletePositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //定义返回值
            int retVal = -1;

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //打开数据库联机
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = positionNameDA.DeletePositionName(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 根据席位编号获取席位信息
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public DataTable GetPositionByID(FlightMonitorBM.PositionNameBM positionBM)
        {
            //定义返回值
            DataTable dtPositionName = new DataTable();

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //打开数据库连接
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtPositionName = positionNameDA.GetPositionByID(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return dtPositionName;
        }
    }
}
