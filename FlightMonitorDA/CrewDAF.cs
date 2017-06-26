using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 机长连飞信息数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-07-03
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CrewDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CrewDAF()
        {
        }

        /// <summary>
        /// 获取航班机组信息
        /// </summary>
        /// <param name="strFlightDate">航班日期 北京时间</param>
        /// <param name="strFlightNo">航班号,不含空格</param>
        /// <param name="strDEPSTN">起飞站三字码</param>
        /// <param name="strARRSTN">到达站三字码</param>
        /// <returns></returns>
        public DataTable GetCrewInforByFlightNo(string strFlightDate, string strFlightNo, string strSEG)
        {
            //定义返回值
            DataTable dtCrews = new DataTable();
            CrewDA crewDA = new CrewDA();

            try
            {
                //打开数据库联机
                crewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Flt_Dept, 1));
                dtCrews = crewDA.GetCrewInforByFlightNo(strFlightDate, strFlightNo, strSEG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewDA.ConnClose();
            }

            return dtCrews;
        }

        /// <summary>
        /// 根据机长获取前飞航班信息
        /// </summary>
        /// <param name="strFlightDate">航班号</param>
        /// <param name="strARRSTN">前飞航班三字码</param>
        /// <param name="strCaptain">机长名字</param>
        /// <returns></returns>
        //public DataTable GetCrewInforByCaptain(string strFlightDate, string strSEG, string strSEGALL, string strCaptain, string strBeginTime, string strEndTime)
        //{
        //    //定义返回值
        //    DataTable dtCrews = new DataTable();
        //    CrewDA crewDA = new CrewDA();

        //    try
        //    {
        //        //打开数据库联机
        //        crewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Flt_Dept, 1));
        //        dtCrews = crewDA.GetCrewInforByCaptain(strFlightDate, strSEG, strSEGALL, strCaptain,strBeginTime, strEndTime);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        crewDA.ConnClose();
        //    }

        //    return dtCrews;
        //}
    }
}
