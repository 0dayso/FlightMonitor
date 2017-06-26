using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class InOutDelayReasonDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public InOutDelayReasonDAF()
        {
        }

        /// <summary>
        /// ��ȡ���н���������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllInOutDelayReason()
        {
            //���巵��ֵ
            DataTable dtInOutDelayReason = new DataTable();
            InOutDelayReasonDA inOutDelayReasonDA = new InOutDelayReasonDA();

            try
            {
                //�����ݿ�����
                inOutDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtInOutDelayReason = inOutDelayReasonDA.GetAllInOutDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                inOutDelayReasonDA.ConnClose();
            }

            return dtInOutDelayReason;
        }
    }
}
