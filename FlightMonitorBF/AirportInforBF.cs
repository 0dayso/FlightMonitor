using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class AirportInforBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AirportInforBF()
        {
        }

        /// <summary>
        /// 获取所有机场信息
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAirportInfors()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            AirportInfoDAF airportInfoDAF = new AirportInfoDAF();

            try
            {
                rvSF.Dt = airportInfoDAF.GetAirportInfors();
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
        /// 根据三字码获取机场信息
        /// </summary>
        /// <param name="strThreeCode">机场三字码</param>
        /// <returns></returns>
        public ReturnValueSF GetAirportInforByThreeCode(string strThreeCode)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            AirportInfoDAF airportInfoDAF = new AirportInfoDAF();

            try
            {
                rvSF.Dt = airportInfoDAF.GetAirportInforByThreeCode(strThreeCode);
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
        /// 根据机型和起飞机场获取滑行油
        /// </summary>
        /// <param name="strACTYPE">FOC机型</param>
        /// <param name="strThreeCode">起飞机场三字码</param>
        /// <returns></returns>
        public ReturnValueSF GetTaxiOil(string strACTYPE, string strThreeCode)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            AirportInfoDAF airportInfoDAF = new AirportInfoDAF();

            try
            {
                rvSF.Dt = airportInfoDAF.GetTaxiOil(strACTYPE, strThreeCode);
                rvSF.Result = 1;
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
