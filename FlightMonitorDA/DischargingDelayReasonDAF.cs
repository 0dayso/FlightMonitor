using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class DischargingDelayReasonDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DischargingDelayReasonDAF()
        {
        }

        /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDischargingDelayReason()
        {
            //定义返回值
            DataTable dtDischargingDelayReason = new DataTable();
            DischargingDelayReasonDA dischargingDelayReasonDA = new DischargingDelayReasonDA();

            try
            {
                //打开数据库联机
                dischargingDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDischargingDelayReason = dischargingDelayReasonDA.GetAllDischargingDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dischargingDelayReasonDA.ConnClose();
            }

            return dtDischargingDelayReason;
        }
    }
}
