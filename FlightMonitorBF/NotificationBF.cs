using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBR;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Collections;


namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ֪ͨ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-12-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class NotificationBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public NotificationBF()
        {

        }

        #region ��ȡ��Ч֪ͨ���ݣ�����ϳ�֪ͨ�ı���
        /// <summary>
        /// ��ȡ��Ч֪ͨ���ݣ�����ϳ�֪ͨ�ı���
        /// </summary>
        /// <param name="currentDateTime">��ǰʱ��</param>
        /// <returns>���ص�ǰʱ������Ч��ֹʱ��ε�֪ͨ���ݣ�����ϳ�֪ͨ�ı���</returns>
        public ReturnValueSF GetAndCombineNotificationData(DateTime currentDateTime)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                FlightMonitorDA.NotificationDAF notificationDAF = new FlightMonitorDA.NotificationDAF();
                DataTable dataTable = notificationDAF.GetNotificationData(currentDateTime);
                string strNotificationData = "";
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    strNotificationData = strNotificationData +
                        (index + 1).ToString() +
                        "��" +
                        dataTable.Rows[index]["cnvcCaption"].ToString() +
                        Environment.NewLine +
                        dataTable.Rows[index]["cnvcContent"].ToString() +
                        Environment.NewLine +
                        Environment.NewLine;
                }
                rvSF.Message = strNotificationData;
                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion ��ȡ��Ч֪ͨ���ݣ�����ϳ�֪ͨ�ı���

        #region ��ȡ������Ϣ���ݣ�����ϳ�֪ͨ�ı���
        /// <summary>
        /// ��ȡ������Ϣ���ݣ�����ϳ�֪ͨ�ı���
        /// </summary>
        /// <param name="dataTableNotificationData">֪ͨ��Ϣ��</param>
        /// <returns>���ص�ǰʱ������Ч��ֹʱ��ε�֪ͨ�����еĸ�����Ϣ���ݣ�����ϳ�֪ͨ�ı���</returns>
        public ReturnValueSF CombineNotificationAttachmentData(DataTable dataTableNotificationData)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //���巵��ֵ
            try
            {
                string strNotificationData = "";
                DataTable dataTable = dataTableNotificationData.Clone();
                DataRow[] dataRowsDataTableNotificationData = dataTableNotificationData.Select("cnvcAttachment <> ''");
                for (int index = 0; index < dataRowsDataTableNotificationData.Length; index++)
                {
                    strNotificationData = strNotificationData +
                        (index + 1).ToString() +
                        "��" +
                        dataTableNotificationData.Rows[index]["cnvcCaption"].ToString() +
                        Environment.NewLine +
                        "   �����ļ�����" +
                        dataTableNotificationData.Rows[index]["cnvcAttachment"].ToString() +
                        Environment.NewLine +
                        Environment.NewLine;

                    dataTable.ImportRow(dataTableNotificationData.Rows[index]);
                }
                rvSF.Message = strNotificationData;
                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion ��ȡ������Ϣ���ݣ�����ϳ�֪ͨ�ı���


    }
}
