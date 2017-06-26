using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
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
    public class StationDAF
    {
        public DataTable GetAllStation()
        {
            DataTable dt = new DataTable(); //���巵��ֵ

            StationDA stationDA = new StationDA();
            //�����ݿ�����
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                dt = stationDA.GetAllStation();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return dt;
        }

        /// <summary>
        /// ���ݻ����������ȡ��վ��Ϣ
        /// </summary>
        /// <param name="strStationThreeCode">��վ������</param>
        /// <returns></returns>
        public DataTable GetStationByThreeCode(string strStationThreeCode)
        {
            DataTable dt = new DataTable(); //���巵��ֵ

            StationDA stationDA = new StationDA();
            //�����ݿ�����
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                dt = stationDA.GetStationByThreeCode(strStationThreeCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return dt;
        }

         /// <summary>
        /// ���һ����վ
        /// </summary>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public int InsertStation(StationBM stationBM)
        {
            int retVal = -1;

            StationDA stationDA = new StationDA();
            //�����ݿ�����
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                retVal = stationDA.InsertStation(stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return retVal;
        }

         /// <summary>
        /// ɾ��һ����վ
        /// </summary>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public int DeleteStation(StationBM stationBM)
        {
            int retVal = -1;

            StationDA stationDA = new StationDA();
            //�����ݿ�����
            stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));


            try
            {
                retVal = stationDA.DeleteStation(stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stationDA.ConnClose();
            }
            return retVal;
        }
    }
}
