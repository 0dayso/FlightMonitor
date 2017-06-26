using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;
using System.Collections;

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
    public class NotificationDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public NotificationDAF()
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
            FlightMonitorDA.NotificationDA notificationDA = new FlightMonitorDA.NotificationDA();
            DataTable dt = new DataTable();
            notificationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                dt = notificationDA.GetNotificationData(currentDateTime);
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }
            finally
            {
                notificationDA.ConnClose();
            }

            return dt;
        }
        #endregion ��ѯ��Ч֪ͨ����
    }
}
