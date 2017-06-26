using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ϯλx��Ϣ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class IntermissionTimeDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public IntermissionTimeDAF()
        {
        }

        /// <summary>
        /// ��ȡ���л��͵ı�׼��վʱ��
        /// </summary>
        /// <returns></returns>
        public DataTable GetStandardIntermissionTime()
        {
            //���巵��ֵ
            DataTable dtStandardIntermissionTime = new DataTable();

            IntermissionTimeDA intermissionTimeDA = new IntermissionTimeDA();

            try
            {
                //�����ݿ�����
                intermissionTimeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtStandardIntermissionTime = intermissionTimeDA.GetStandardIntermissionTime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                intermissionTimeDA.ConnClose();
            }

            return dtStandardIntermissionTime;
        }
    }
}
