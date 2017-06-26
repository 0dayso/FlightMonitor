using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ��������ԭ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-13
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class FlightDelayReasonBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public FlightDelayReasonBF()
        {
        }

         /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllFlightDelayReason()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightDelayReasonDAF flightDelayReasonDAF = new FlightDelayReasonDAF();

            try
            {
                rvSF.Dt = flightDelayReasonDAF.GetAllFlightDelayReason();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
    }
}
