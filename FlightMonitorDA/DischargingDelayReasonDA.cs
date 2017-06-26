using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class DischargingDelayReasonDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DischargingDelayReasonDA()
        {
        }

        #region ��ȡ���к����������
        private const string SELECT_AllDischargingDelayReason = "SELECT * FROM tbDischargingDelayReason";

        /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDischargingDelayReason()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllDischargingDelayReason);
        }
        #endregion
        
    }
}
