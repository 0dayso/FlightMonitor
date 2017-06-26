using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��������ԭ�����ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class FlightDelayReasonDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public FlightDelayReasonDA()
        {
        }

        #region ��ȡ���к����������
        private const string SELECT_AllFlightDelayReason = "SELECT * FROM tbFlightDelayReason";
        
        /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllFlightDelayReason()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllFlightDelayReason);
        }
        #endregion
    }
}
