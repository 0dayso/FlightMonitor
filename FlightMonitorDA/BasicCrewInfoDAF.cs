using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ���飨���С����񣩻�����Ϣ���������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-09-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class BasicCrewInfoDAF
    {
        /// <summary>
        /// �������Ű��ȡ������ϵ��ʽ�Ȼ�����Ϣ
        /// </summary>
        /// <returns>��ϵ��ʽ���ݼ�</returns>
        public DataSet GetProfileInfo()
        {
            DataSet dataSet = new DataSet();
            BasicCrewInfoDA basicCrewInfoDA = new BasicCrewInfoDA();
            try
            {
                dataSet = basicCrewInfoDA.GetProfileInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataSet;
        }


    }
}
