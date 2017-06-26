using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ��������ԭ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class DiversionDelayReasonBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DiversionDelayReasonBF()
        {
        }

        /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllDiversionDelayReason()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            DiversionDelayReasonDAF diversionDelayReasonDAF = new DiversionDelayReasonDAF();

            try
            {
                rvSF.Dt = diversionDelayReasonDAF.GetAllDiversionDelayReason();
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
