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
        /// 构造函数
        /// </summary>
        public AirportInfoDAF()
        {
        }

        /// <summary>
        /// 获取所有机场信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAirportInfors()
        {
            //定义返回值
            DataTable dtAirportInfors = new DataTable();
            AirportInfoDA airportInfoDA = new AirportInfoDA();

            try
            {
                //打开数据库联机
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
        /// 根据三字码获取机场信息
        /// </summary>
        /// <param name="strThreeCode">机场三字码</param>
        /// <returns></returns>
        public DataTable GetAirportInforByThreeCode(string strThreeCode)
        {
            //定义返回值
            DataTable dtAirportInfors = new DataTable();
            AirportInfoDA airportInfoDA = new AirportInfoDA();

            try
            {
                //打开数据库联机
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
        /// 根据机型和起飞机场获取滑行油
        /// </summary>
        /// <param name="strACTYPE">FOC机型</param>
        /// <param name="strThreeCode">起飞机场三字码</param>
        /// <returns></returns>
        public DataTable GetTaxiOil(string strACTYPE, string strThreeCode)
        {
            //定义返回值
            DataTable dtAirportInfors = new DataTable();
            AirportInfoDA airportInfoDA = new AirportInfoDA();

            try
            {
                //打开数据库联机
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
