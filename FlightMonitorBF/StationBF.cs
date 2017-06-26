using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;


namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ��վ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-24
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class StationBF
    {
        /// <summary>
        /// ��ȡ���к�վ
        /// </summary>
        /// <returns>�Զ�������</returns>
        public ReturnValueSF GetAllStation()
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            StationDAF stationDAF = new StationDAF();
            try
            {
                rvSF.Dt = stationDAF.GetAllStation();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// ���ݻ����������ȡ��վ��Ϣ
        /// </summary>
        /// <param name="strStationThreeCode">��վ������</param>
        /// <returns></returns>
        public ReturnValueSF GetStationByThreeCode(string strStationThreeCode)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            StationDAF stationDAF = new StationDAF();
            try
            {
                rvSF.Dt = stationDAF.GetStationByThreeCode(strStationThreeCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// ���һ����վ
        /// </summary>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF InsertStation(StationBM stationBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            StationDAF stationDAF = new StationDAF();
            try
            {
                rvSF.Result = stationDAF.InsertStation(stationBM);                
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }


        /// <summary>
        /// ɾ��һ����վ
        /// </summary>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF DeleteStation(StationBM stationBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            StationDAF stationDAF = new StationDAF();
            try
            {
                rvSF.Result = stationDAF.DeleteStation(stationBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

    }
}
