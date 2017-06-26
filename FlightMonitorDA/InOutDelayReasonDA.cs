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
        /// ���캯��
        /// </summary>
        public InOutDelayReasonDA()
        {
        }

        #region ��ȡ���н����ۺ����������
        private const string SELECT_AllInOutDelayReason = "SELECT * FROM tbInOutDelayReason";

        /// <summary>
        /// ��ȡ���н���������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllInOutDelayReason()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllInOutDelayReason);
        }
        #endregion
    }
}
