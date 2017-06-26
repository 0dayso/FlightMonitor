using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 备降原因数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06－14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class DiversionDelayReasonDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DiversionDelayReasonDAF()
        {
        }

        /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDiversionDelayReason()
        {
            //定义返回值
            DataTable dDiversionDelayReason = new DataTable();
            DiversionDelayReasonDA diversionDelayReasonDA = new DiversionDelayReasonDA();

            try
            {
                //打开数据库联机
                diversionDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dDiversionDelayReason = diversionDelayReasonDA.GetAllDiversionDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                diversionDelayReasonDA.ConnClose();
            }

            return dDiversionDelayReason;
        }
    }
}
