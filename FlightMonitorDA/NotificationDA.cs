using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ֪ͨ���ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-12-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class NotificationDA : SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public NotificationDA()
        {
        }

        #region ��ѯ��Ч֪ͨ����
        /// <summary>
        /// ��ѯ��Ч֪ͨ����
        /// </summary>
        /// <param name="currentDateTime">��ǰʱ��</param>
        /// <returns>���ص�ǰʱ������Ч��ֹʱ��ε�֪ͨ����</returns>
        public DataTable GetNotificationData(DateTime currentDateTime)
        {
            string strSql = "select * from tbNotification where (cndEffectiveDate_Start <= '" 
                + currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                + "') and (cndEffectiveDate_End >= '" 
                + currentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                + "') order by [cniPriorityLevel] desc";

            DataTable dt = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql);

            return dt;

        }
        #endregion ��ѯ��Ч֪ͨ����

    }
}
