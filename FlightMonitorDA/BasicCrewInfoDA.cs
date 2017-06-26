using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorDA.FocService;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ���飨���С����񣩻�����Ϣ���ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-09-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class BasicCrewInfoDA : SqlDatabase
    {
        /// <summary>
        /// �������Ű��ȡ������ϵ��ʽ�Ȼ�����Ϣ
        /// </summary>
        /// <returns>��ϵ��ʽ���ݼ�</returns>
        public DataSet GetProfileInfo()
        {
            icms.Service icmsService = new AirSoft.FlightMonitor.FlightMonitorDA.icms.Service();
            return icmsService.GetBasicCrewInfo_Profile("", "", "", "1970-05-12", "3000-12-12");

            //return icmsService.GetCrewInformation("", "ŷ����", "", "", "", "", "");
            //return icmsService.GetBasicCrewInfo_Profile(null, null, null, null, null);
        }


    }
}
