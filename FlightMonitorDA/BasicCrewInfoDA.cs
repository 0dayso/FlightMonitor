using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorDA.FocService;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 机组（飞行、乘务）基础信息访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2013-09-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class BasicCrewInfoDA : SqlDatabase
    {
        /// <summary>
        /// 从银湖排班获取机组联系方式等基础信息
        /// </summary>
        /// <returns>联系方式数据集</returns>
        public DataSet GetProfileInfo()
        {
            icms.Service icmsService = new AirSoft.FlightMonitor.FlightMonitorDA.icms.Service();
            return icmsService.GetBasicCrewInfo_Profile("", "", "", "1970-05-12", "3000-12-12");

            //return icmsService.GetCrewInformation("", "欧阳晨", "", "", "", "", "");
            //return icmsService.GetBasicCrewInfo_Profile(null, null, null, null, null);
        }


    }
}
