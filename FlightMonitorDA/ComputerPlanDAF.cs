using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 计算机飞行计划数据访问类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-07-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ComputerPlanDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComputerPlanDAF()
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
        public DataTable GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn)
        {
            //定义返回值
            DataTable dtComputerPlan = new DataTable();

            ComputerPlanDA computerPlanDA = new ComputerPlanDA();

            try
            {
                //打开数据库连接
                computerPlanDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_DSPBusiness, 1));
                dtComputerPlan = computerPlanDA.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                computerPlanDA.ConnClose();
            }

            return dtComputerPlan;
        }

        /// <summary>
        /// 根据日期获取所有计算机飞行计划
        /// </summary>
        /// <param name="strDate">航班日期</param>
        /// <returns></returns>
        public DataTable GetCFPByFlightDate(string strDate)
        {
            //定义返回值
            DataTable dtComputerPlan = new DataTable();

            ComputerPlanDA computerPlanDA = new ComputerPlanDA();

            try
            {
                //打开数据库连接
                computerPlanDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_DSPBusiness, 1));
                dtComputerPlan = computerPlanDA.GetCFPByFlightDate(strDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                computerPlanDA.ConnClose();
            }

            return dtComputerPlan;
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
        public DataTable GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn, string strDATOP)
        {
            //定义返回值
            DataTable dtComputerPlan = new DataTable();

            ComputerPlanDA computerPlanDA = new ComputerPlanDA();

            try
            {
                //打开数据库连接
                computerPlanDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_DSPBusiness, 1));
                dtComputerPlan = computerPlanDA.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn, strDATOP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                computerPlanDA.ConnClose();
            }

            return dtComputerPlan;
        }
        #endregion modified by LinYong in 2013.08.02

    }
}
