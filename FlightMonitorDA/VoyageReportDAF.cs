using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取航前任务书信息数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2013-09-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class VoyageReportDAF
    {
        /// <summary>
        /// 获取航前任务书信息，获取时间段内的信息
        /// </summary>
        /// <param name="DATOP_Start">航班日期，开始</param>
        /// <param name="DATOP_End">航班日期，截止</param>
        /// <returns></returns>
        public DataTable GetVoyageReportData(DateTime DATOP_Start, DateTime DATOP_End)
        {
            //定义返回值
            DataTable dtDataTable = new DataTable();
            VoyageReportDA VoyageReportDA = new VoyageReportDA();

            try
            {
                //打开数据库联机
                string s = ConfigManagerSF.GetConfigString(SysConstBM.DB_VoyageReport, 1);
                VoyageReportDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_VoyageReport, 1));
                dtDataTable = VoyageReportDA.GetVoyageReportData(DATOP_Start, DATOP_End);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                VoyageReportDA.ConnClose();
            }

            return dtDataTable;
        }


        /// <summary>
        /// 获取航前任务书信息，获取单个航班的信息
        /// </summary>
        /// <param name="DATOP_Start">航班日期，开始</param>
        /// <param name="DATOP_End">航班日期，截止</param>
        /// <returns></returns>
        public DataTable GetVoyageReportDataBySingleFlight(string DATOP, string FLTIDS, string AC, string ROUTES)
        {
            //定义返回值
            DataTable dtDataTable = new DataTable();
            VoyageReportDA VoyageReportDA = new VoyageReportDA();

            try
            {
                //打开数据库联机
                VoyageReportDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_VoyageReport, 1));
                dtDataTable = VoyageReportDA.GetVoyageReportDataBySingleFlight( DATOP,  FLTIDS,  AC,  ROUTES);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                VoyageReportDA.ConnClose();
            }

            return dtDataTable;
        }








    }
}
