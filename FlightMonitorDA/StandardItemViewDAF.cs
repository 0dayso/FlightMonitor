using System;
using System.Collections;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class StandardItemViewDAF
    {
        private StandardItemViewDA standardItemViewDA = new StandardItemViewDA();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strLongReg"></param>
        /// <param name="strAirPort"></param>
        /// <param name="strFlightType"></param>
        /// <param name="strOIFlightType"></param>
        /// <param name="cIOFlight"></param>
        /// <returns></returns>
        public DataSet GetStandardItem(string strLongReg, string strAirPort, string strFlightType, string strOIFlightType, char cIOFlight)
        {
            try
            {
                standardItemViewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                return standardItemViewDA.GetStandardItem(strLongReg, strAirPort, strFlightType, strOIFlightType, cIOFlight);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemViewDA.ConnClose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flightParams"></param>
        /// <returns></returns>
        public DataSet GetGuaranteeInfor(FlightParams flightParams)
        {
            try
            {
                standardItemViewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                return standardItemViewDA.GetGuaranteeInfor(flightParams);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemViewDA.ConnClose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetBStandardItemName()
        {
            try
            {
                standardItemViewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                return standardItemViewDA.GetBStandardItemName();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemViewDA.ConnClose();
            }
        }
    }
}
