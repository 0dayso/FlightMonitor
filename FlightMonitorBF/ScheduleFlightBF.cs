using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ����ƻ������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ScheduleFlightBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ScheduleFlightBF()
        {

        }

        /// <summary>
        /// ���غ���ƻ�
        /// </summary>
        /// <param name="strFullPath">�ƻ��ļ�·��</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF LoadScheduleFlight(string strFullPath)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //����ƻ����ݷ��������
            FlightMonitorDA.ScheduleLegsDAF scheduleLegsDAF = new ScheduleLegsDAF();

            try
            {
                //�������ݷ�����۲㷽��
                rvSF.Result = scheduleLegsDAF.LoadScheduleFlight(strFullPath);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_LOAD_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_LOAD_FALSE;
                }
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
