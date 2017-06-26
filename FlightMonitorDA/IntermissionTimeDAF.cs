using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 席位x信息数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class IntermissionTimeDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IntermissionTimeDAF()
        {
        }

        /// <summary>
        /// 获取所有机型的标准过站时间
        /// </summary>
        /// <returns></returns>
        public DataTable GetStandardIntermissionTime()
        {
            //定义返回值
            DataTable dtStandardIntermissionTime = new DataTable();

            IntermissionTimeDA intermissionTimeDA = new IntermissionTimeDA();

            try
            {
                //打开数据库连接
                intermissionTimeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtStandardIntermissionTime = intermissionTimeDA.GetStandardIntermissionTime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                intermissionTimeDA.ConnClose();
            }

            return dtStandardIntermissionTime;
        }
    }
}
