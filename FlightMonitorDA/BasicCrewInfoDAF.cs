using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 机组（飞行、乘务）基础信息访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2013-09-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class BasicCrewInfoDAF
    {
        /// <summary>
        /// 从银湖排班获取机组联系方式等基础信息
        /// </summary>
        /// <returns>联系方式数据集</returns>
        public DataSet GetProfileInfo()
        {
            DataSet dataSet = new DataSet();
            BasicCrewInfoDA basicCrewInfoDA = new BasicCrewInfoDA();
            try
            {
                dataSet = basicCrewInfoDA.GetProfileInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataSet;
        }


    }
}
