using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ϵͳʱ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ServerDateTimeBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ServerDateTimeBF()
        {
        }

         /// <summary>
        /// ��ȡϵͳʱ��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetServerDateTime()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            ServerDateTimeDAF serverDateTimeDAF = new ServerDateTimeDAF();

            //�������ݷ�����۲㷽��
            try
            {
                rvSF.Message = Convert.ToString(serverDateTimeDAF.GetServerDateTime());
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }
            return rvSF;
        }
    }
}
