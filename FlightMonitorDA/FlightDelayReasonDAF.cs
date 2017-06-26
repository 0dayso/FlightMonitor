using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��������ԭ�����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06��14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class FlightDelayReasonDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public FlightDelayReasonDAF()
        {
        }

        /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllFlightDelayReason()
        {
            //���巵��ֵ
            DataTable dtFlightDelayReason = new DataTable();
            FlightDelayReasonDA flightDelayReasonDA = new FlightDelayReasonDA();

            try
            {
                //�����ݿ�����
                flightDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlightDelayReason = flightDelayReasonDA.GetAllFlightDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightDelayReasonDA.ConnClose();
            }

            return dtFlightDelayReason;
        }
    }
}
