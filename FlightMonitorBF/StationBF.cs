using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;


namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 航站数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-24
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class StationBF
    {
        /// <summary>
        /// 获取所有航站
        /// </summary>
        /// <returns>自定义类型</returns>
        public ReturnValueSF GetAllStation()
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
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
        /// 根据机场三字码获取航站信息
        /// </summary>
        /// <param name="strStationThreeCode">航站三字码</param>
        /// <returns></returns>
        public ReturnValueSF GetStationByThreeCode(string strStationThreeCode)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
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
        /// 添加一个航站
        /// </summary>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public ReturnValueSF InsertStation(StationBM stationBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
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
        /// 删除一个航站
        /// </summary>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public ReturnValueSF DeleteStation(StationBM stationBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
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
