using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class InOutDelayReasonDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InOutDelayReasonDAF()
        {
        }

        /// <summary>
        /// 获取所有进出港延误原因代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllInOutDelayReason()
        {
            //定义返回值
            DataTable dtInOutDelayReason = new DataTable();
            InOutDelayReasonDA inOutDelayReasonDA = new InOutDelayReasonDA();

            try
            {
                //打开数据库联机
                inOutDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtInOutDelayReason = inOutDelayReasonDA.GetAllInOutDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                inOutDelayReasonDA.ConnClose();
            }

            return dtInOutDelayReason;
        }
    }
}
