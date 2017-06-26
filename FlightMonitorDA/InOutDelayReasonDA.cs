using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class InOutDelayReasonDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InOutDelayReasonDA()
        {
        }

        #region 获取所有进出港航班延误代码
        private const string SELECT_AllInOutDelayReason = "SELECT * FROM tbInOutDelayReason";

        /// <summary>
        /// 获取所有进出港延误原因代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllInOutDelayReason()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllInOutDelayReason);
        }
        #endregion
    }
}
