using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Configuration;
using System.Data;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ��վ��վʱ����Ϣ���ʷ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-07-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class OverStationTimeBF
    {
        #region ��ȡ����澯��Ϣ��¼
        /// <summary>
        /// ��ȡ����澯��Ϣ��¼
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Select()
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                OverStationTimeDAF overStationTimeDAF = new OverStationTimeDAF();
                rvSF.Dt = overStationTimeDAF.Select();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion ��ȡ����澯��Ϣ��¼
    }
}
