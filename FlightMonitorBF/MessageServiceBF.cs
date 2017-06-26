using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class MessageServiceBF
    {
        /// <summary>
        /// ��ȡ�������͵�������Ϣ
        /// </summary>
        /// <param name="DTTM">��Ϣ����ʱ�䣨��ʽ�� 2015-04-03 22:17:00��</param>
        /// <returns>�������͵�������Ϣ</returns>
        public ReturnValueSF GetMessages(string DTTM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            MessageServiceDAF messageServiceDAF = new MessageServiceDAF();

            try
            {
                rvSF.Dt = messageServiceDAF.GetMessages(DTTM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }

        /// <summary>
        /// ��ȡ�������͵�������Ϣ��ʹ�� ������ֵ EventID
        /// </summary>
        /// <param name="EventID">������ֵ</param>
        /// <returns>�������͵�������Ϣ</returns>
        public ReturnValueSF GetMessages(int EventID)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            MessageServiceDAF messageServiceDAF = new MessageServiceDAF();

            try
            {
                rvSF.Dt = messageServiceDAF.GetMessages(EventID);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
    }
}
