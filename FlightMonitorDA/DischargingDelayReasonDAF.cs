using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class DischargingDelayReasonDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DischargingDelayReasonDAF()
        {
        }

        /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDischargingDelayReason()
        {
            //���巵��ֵ
            DataTable dtDischargingDelayReason = new DataTable();
            DischargingDelayReasonDA dischargingDelayReasonDA = new DischargingDelayReasonDA();

            try
            {
                //�����ݿ�����
                dischargingDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDischargingDelayReason = dischargingDelayReasonDA.GetAllDischargingDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dischargingDelayReasonDA.ConnClose();
            }

            return dtDischargingDelayReason;
        }
    }
}
