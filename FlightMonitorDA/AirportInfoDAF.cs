using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class AirportInfoDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AirportInfoDAF()
        {
        }

        /// <summary>
        /// ��ȡ���л�����Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetAirportInfors()
        {
            //���巵��ֵ
            DataTable dtAirportInfors = new DataTable();
            AirportInfoDA airportInfoDA = new AirportInfoDA();

            try
            {
                //�����ݿ�����
                airportInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtAirportInfors = airportInfoDA.GetAirportInfors();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                airportInfoDA.ConnClose();
            }

            return dtAirportInfors;
        }

        /// <summary>
        /// �����������ȡ������Ϣ
        /// </summary>
        /// <param name="strThreeCode">����������</param>
        /// <returns></returns>
        public DataTable GetAirportInforByThreeCode(string strThreeCode)
        {
            //���巵��ֵ
            DataTable dtAirportInfors = new DataTable();
            AirportInfoDA airportInfoDA = new AirportInfoDA();

            try
            {
                //�����ݿ�����
                airportInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtAirportInfors = airportInfoDA.GetAirportInforByThreeCode(strThreeCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                airportInfoDA.ConnClose();
            }

            return dtAirportInfors;
        }

        /// <summary>
        /// ���ݻ��ͺ���ɻ�����ȡ������
        /// </summary>
        /// <param name="strACTYPE">FOC����</param>
        /// <param name="strThreeCode">��ɻ���������</param>
        /// <returns></returns>
        public DataTable GetTaxiOil(string strACTYPE, string strThreeCode)
        {
            //���巵��ֵ
            DataTable dtAirportInfors = new DataTable();
            AirportInfoDA airportInfoDA = new AirportInfoDA();

            try
            {
                //�����ݿ�����
                airportInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtAirportInfors = airportInfoDA.GetTaxiOil(strACTYPE, strThreeCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                airportInfoDA.ConnClose();
            }

            return dtAirportInfors;
        }
    }
}
