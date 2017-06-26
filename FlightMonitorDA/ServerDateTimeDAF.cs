using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ϵͳʱ�����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06��14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ServerDateTimeDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ServerDateTimeDAF()
        {
        }

         /// <summary>
        /// ��ȡϵͳʱ��
        /// </summary>
        /// <returns></returns>
        public object GetServerDateTime()
        {
            //���巵��ֵ
            object oServerTime = new object();
            ServerDateTimeDA serverDateTimeDA = new ServerDateTimeDA();

            try
            {
                //�����ݿ�����
                serverDateTimeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                oServerTime = serverDateTimeDA.GetServerDateTime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serverDateTimeDA.ConnClose();
            }

            return oServerTime;
        }
    }
}
