using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class CrewSignInBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CrewSignInBF()
        {
        }

        /// <summary>
        /// 查询一个航班的机组签到信息
        /// </summary>
        /// <param name="strQueryTime">查询时间</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns></returns>
        public ReturnValueSF GetCrewSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetCrewSignIn(strQueryTime, strFlightNo, strDEPSTN);
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
        /// 获取某位飞行员的签到时间
        /// </summary>
        /// <param name="strCrewName">飞行员名字</param>
        /// <param name="strSignInFlag">签到标识</param>
        /// <returns></returns>
        public ReturnValueSF GetCrewSignTime(string strCrewName, string strSignInFlag)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetCrewSignTime(strCrewName, strSignInFlag);
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
        /// 查询一个航班的乘务签到信息
        /// </summary>
        /// <param name="strQueryTime">查询时间</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns>签到信息表</returns>
        public ReturnValueSF GetStewardSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetStewardSignIn(strQueryTime, strFlightNo, strDEPSTN);
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
        /// 获取某位乘务员的签到时间
        /// </summary>
        /// <param name="strStewardName">乘务员名字</param>
        /// <param name="strSignInFlag">签到标识</param>
        /// <returns></returns>
        public ReturnValueSF GetStewardSignTime(string strStewardName, string strSignInFlag)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetStewardSignTime(strStewardName, strSignInFlag);
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
