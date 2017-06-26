using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取停机位信息数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-09-03
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GateInfoDAF
    {
        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="StationThreeCode">机场三字码</param>
        /// <returns></returns>
        public DataTable GetList(string StationThreeCode)
        {
            //定义返回值
            DataTable dtDataTable = new DataTable();
            GateInfoDA gateInfoDA = new GateInfoDA();

            try
            {
                //打开数据库连接
                gateInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataTable = gateInfoDA.GetList(StationThreeCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                gateInfoDA.ConnClose();
            }

            return dtDataTable;
        }
        #endregion 获得数据列表

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="DataTable">需要变更的数据表</param>
        /// <returns></returns>
        public int Save(DataTable DataTable)
        {
            int retVal = -1;
            GateInfoDA gateInfoDA = new GateInfoDA();

            try
            {
                gateInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                retVal = gateInfoDA.Save(DataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                gateInfoDA.ConnClose();
            }

            return retVal;
        }
        #endregion 保存数据
    }
}
