using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class DiversionDelayReasonDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DiversionDelayReasonDA()
        {
        }

        #region 获取所有航班延误代码
        private const string SELECT_AllDiversionDelayReason = "SELECT * FROM tbDiversionDelayReason";

        /// <summary>
        /// 获取所有航班延误原因代码
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDiversionDelayReason()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllDiversionDelayReason);
        }
        #endregion
    }
}
