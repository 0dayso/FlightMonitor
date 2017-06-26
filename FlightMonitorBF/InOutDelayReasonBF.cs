using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;


namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class InOutDelayReasonBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public InOutDelayReasonBF()
        {
        }

        /// <summary>
        /// ��ȡ���н���������ԭ�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllInOutDelayReason()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            InOutDelayReasonDAF inOutDelayReasonDAF = new InOutDelayReasonDAF();

            try
            {
                rvSF.Dt = inOutDelayReasonDAF.GetAllInOutDelayReason();
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
