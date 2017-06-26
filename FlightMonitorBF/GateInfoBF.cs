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
    /// ��ȡͣ��λ��Ϣ���ݷ��ʷ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-09-03
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GateInfoBF
    {
        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="StationThreeCode">����������</param>
        /// <returns></returns>
        public ReturnValueSF GetList(string StationThreeCode)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            GateInfoDAF gateInfoDAF = new GateInfoDAF();

            try
            {
                rvSF.Dt = gateInfoDAF.GetList(StationThreeCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion ��������б�

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="DataTable">��Ҫ��������ݱ�</param>
        /// <returns></returns>
        public ReturnValueSF Save(DataTable DataTable)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            GateInfoDAF gateInfoDAF = new GateInfoDAF();

            try
            {
                rvSF.Result = gateInfoDAF.Save(DataTable);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;

        }
        #endregion ��������

    }
}
