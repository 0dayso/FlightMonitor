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
        /// ���캯��
        /// </summary>
        public DiversionDelayReasonDA()
        {
        }

        #region ��ȡ���к����������
        private const string SELECT_AllDiversionDelayReason = "SELECT * FROM tbDiversionDelayReason";

        /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDiversionDelayReason()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllDiversionDelayReason);
        }
        #endregion
    }
}
