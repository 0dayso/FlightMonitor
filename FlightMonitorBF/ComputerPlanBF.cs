using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class ComputerPlanBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComputerPlanBF()
        {
        }

        /// <summary>
        /// 获取计算机飞行计划
        /// </summary>
        /// <param name="strDate">航班日期</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDepstn">始发站三字码</param>
        /// <param name="strArrstn">到达站三字码</param>
        /// <returns></returns>
        public ReturnValueSF GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            ComputerPlanDAF computerPlanDAF = new ComputerPlanDAF();

            try
            {
                rvSF.Dt = computerPlanDAF.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn);
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
        /// 根据日期获取所有计算机飞行计划
        /// </summary>
        /// <param name="strDate">航班日期</param>
        /// <returns></returns>
        public ReturnValueSF GetCFPByFlightDate(string strDate)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            ComputerPlanDAF computerPlanDAF = new ComputerPlanDAF();

            try
            {
                rvSF.Dt = computerPlanDAF.GetCFPByFlightDate(strDate);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }


        #region modified by LinYong in 2013.08.02
        /// <summary>
        /// 获取计算机飞行计划
        /// </summary>
        /// <param name="strDate">航班日期</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDepstn">始发站三字码</param>
        /// <param name="strArrstn">到达站三字码</param>
        /// <param name="strDATOP">航班日期（UTC）</param>
        /// <returns></returns>
        public ReturnValueSF GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn, string strDATOP)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            ComputerPlanDAF computerPlanDAF = new ComputerPlanDAF();

            try
            {
                rvSF.Dt = computerPlanDAF.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn, strDATOP);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion modified by LinYong in 2013.08.02

    }
}
