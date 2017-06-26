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
    /// 航站过站时间信息访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class OverStationTimeDAF
    {
        #region 获取航站过站时间信息记录
        /// <summary>
        /// 获取航站过站时间信息记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            //定义返回值
            DataTable dataTable = new DataTable();
            //打开数据库连接
            OverStationTimeDA overStationTimeDA = new OverStationTimeDA();
            overStationTimeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = overStationTimeDA.Select();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                overStationTimeDA.ConnClose();
            }

            return dataTable;
        }
        #endregion 获取航站过站时间信息记录

    }
}
