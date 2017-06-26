using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ����ԭ�����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06��14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class DiversionDelayReasonDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DiversionDelayReasonDAF()
        {
        }

        /// <summary>
        /// ��ȡ���к�������ԭ�����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDiversionDelayReason()
        {
            //���巵��ֵ
            DataTable dDiversionDelayReason = new DataTable();
            DiversionDelayReasonDA diversionDelayReasonDA = new DiversionDelayReasonDA();

            try
            {
                //�����ݿ�����
                diversionDelayReasonDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dDiversionDelayReason = diversionDelayReasonDA.GetAllDiversionDelayReason();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                diversionDelayReasonDA.ConnClose();
            }

            return dDiversionDelayReason;
        }
    }
}
