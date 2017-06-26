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
    /// 保障人员类型信息访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2016-03-21
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CommanderTypeDAF
    {
        #region 获取保障人员类型信息
        /// <summary>
        /// 获取保障人员类型信息
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            //定义返回值
            DataTable dataTable = new DataTable();
            //打开数据库连接
            CommanderTypeDA commanderTypeDA = new CommanderTypeDA();
            commanderTypeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = commanderTypeDA.Select();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commanderTypeDA.ConnClose();
            }

            return dataTable;
        }
        #endregion 获取保障人员类型信息
    }
}
