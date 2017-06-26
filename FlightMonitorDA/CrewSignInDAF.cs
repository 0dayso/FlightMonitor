using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 机组签到数据访问操作外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-07-10
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CrewSignInDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CrewSignInDAF()
        {
        }

        /// <summary>
        /// 查询一个航班的机组签到信息
        /// </summary>
        /// <param name="strQueryTime">查询时间</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns>签到信息表</returns>
        public DataTable GetCrewSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            //定义返回值
            DataTable dtCrewSignIn = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //打开数据库联机
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtCrewSignIn = crewSignInDA.GetCrewSignIn(strQueryTime, strFlightNo, strDEPSTN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtCrewSignIn;
        }

         /// <summary>
        /// 获取某位飞行员的签到时间
        /// </summary>
        /// <param name="strCrewName">飞行员名字</param>
        /// <param name="strSignInFlag">签到标识</param>
        /// <returns></returns>
        public DataTable GetCrewSignTime(string strCrewName, string strSignInFlag)
        {
            //定义返回值
            DataTable dtCrewSignInTime = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //打开数据库联机
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtCrewSignInTime = crewSignInDA.GetCrewSignTime(strCrewName, strSignInFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtCrewSignInTime;
        }

         /// <summary>
        /// 查询一个航班的乘务签到信息
        /// </summary>
        /// <param name="strQueryTime">查询时间</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns>签到信息表</returns>
        public DataTable GetStewardSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            //定义返回值
            DataTable dtStewardSignIn = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //打开数据库联机
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtStewardSignIn = crewSignInDA.GetStewardSignIn(strQueryTime, strFlightNo, strDEPSTN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtStewardSignIn;
        }

         /// <summary>
        /// 获取某位乘务员的签到时间
        /// </summary>
        /// <param name="strStewardName">乘务员名字</param>
        /// <param name="strSignInFlag">签到标识</param>
        /// <returns></returns>
        public DataTable GetStewardSignTime(string strStewardName, string strSignInFlag)
        {
            //定义返回值
            DataTable dtStewardSignInTime = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //打开数据库联机
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtStewardSignInTime = crewSignInDA.GetStewardSignTime(strStewardName, strSignInFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtStewardSignInTime;
        }
    }
}
