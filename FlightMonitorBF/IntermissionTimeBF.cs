using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ��׼��վʱ��ҵ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class IntermissionTimeBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public IntermissionTimeBF()
        {
        }

        /// <summary>
        /// ��ȡ���л��͵ı�׼��վʱ��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetStandardIntermissionTime()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            IntermissionTimeDAF intermissionTimeDAF = new IntermissionTimeDAF();

            try
            {
                rvSF.Dt = intermissionTimeDAF.GetStandardIntermissionTime();
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
