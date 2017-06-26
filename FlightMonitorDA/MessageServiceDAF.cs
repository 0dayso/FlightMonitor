using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��ȡ�������͵���Ϣ�ķ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-04-15
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class MessageServiceDAF
    {
        /// <summary>
        /// ��ȡ�������͵�������Ϣ
        /// </summary>
        /// <param name="DTTM">��Ϣ����ʱ�䣨��ʽ�� 2015-04-03 22:17:00��</param>
        /// <returns>�������͵�������Ϣ</returns>
        public DataTable GetMessages(string DTTM)
        {
            //���巵��ֵ
            DataTable dtDataTable = new DataTable();
            MessageServiceDA messageServiceDA = new MessageServiceDA();

            try
            {
                //�����ݿ�����
                messageServiceDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_MessageService, 1));
                dtDataTable = messageServiceDA.GetMessages(DTTM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                messageServiceDA.ConnClose();
            }

            return dtDataTable;
        }

        /// <summary>
        /// ��ȡ�������͵�������Ϣ��ʹ�� ������ֵ EventID
        /// </summary>
        /// <param name="EventID">������ֵ</param>
        /// <returns>�������͵�������Ϣ</returns>
        public DataTable GetMessages(int EventID)
        {
            //���巵��ֵ
            DataTable dtDataTable = new DataTable();
            MessageServiceDA messageServiceDA = new MessageServiceDA();

            try
            {
                //�����ݿ�����
                messageServiceDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_MessageService, 1));
                dtDataTable = messageServiceDA.GetMessages(EventID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                messageServiceDA.ConnClose();
            }

            return dtDataTable;
        }
    }
}
