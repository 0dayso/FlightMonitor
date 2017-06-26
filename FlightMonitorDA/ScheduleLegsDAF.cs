using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航班计划数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ScheduleLegsDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleLegsDAF()
        {
        }

        /// <summary>
        /// 加载航班计划数据
        /// </summary>
        /// <param name="strFullPath">计划文件完全路径</param>
        /// <returns></returns>
        public int LoadScheduleFlight(string strFullPath)
        {
            //定义返回值
            int retVal = 0;

            //将计划文件加载到数据集
            FlightMonitorBM.ScheduleLegsBM sourceScheduleLegsBM = new FlightMonitorBM.ScheduleLegsBM();
            //FileStream fsScheduleFlight = new FileStream(strFullPath, FileMode.Open, FileAccess.Read, FileShare.Delete);
            try
            {
                sourceScheduleLegsBM.ReadXml(strFullPath);                              
            }
            catch (Exception ex)
            {
                retVal = -1;
                throw ex;                
            }
            //finally
            //{
            //    fsScheduleFlight.Close();
            //}

            //如果加载不成功则返回
            if (retVal < 0)
            {
                return retVal;
            }

            //将数据集加载到数据库中
            FlightMonitorDA.ScheduleLegsDA scheduleDA = new ScheduleLegsDA();
            try
            {
                //打开数据库连接
                scheduleDA.GetConnOpen(Public.SystemFramework.ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                
                //retVal = scheduleDA.LoadScheduleFlight(sourceScheduleLegsBM);
                foreach (DataRow dataRow in sourceScheduleLegsBM.Tables[0].Rows)
                {
                    retVal = scheduleDA.Insert(new ChangeLegsBM(dataRow, 1));

                   
                }

                if (retVal > 0)
                {
                    File.Delete(strFullPath);
                }
               
            }
            catch (Exception ex)
            {
                retVal = -1;
                throw ex;              
            }

            return retVal;
        }
    }
}
