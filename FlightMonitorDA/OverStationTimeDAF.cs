using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��վ��վʱ����Ϣ���������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-07-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class OverStationTimeDAF
    {
        #region ��ȡ��վ��վʱ����Ϣ��¼
        /// <summary>
        /// ��ȡ��վ��վʱ����Ϣ��¼
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();
            //�����ݿ�����
            OverStationTimeDA overStationTimeDA = new OverStationTimeDA();
            overStationTimeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = overStationTimeDA.Select();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                overStationTimeDA.ConnClose();
            }

            return dataTable;
        }
        #endregion ��ȡ��վ��վʱ����Ϣ��¼

    }
}
