using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-31
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class IntermissionTimeDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IntermissionTimeDA()
        {
        }

        #region 获取所有机型的标准过站时间
        private const string SELECT_StandardIntermissionTime = "SELECT * FROM tbIntermissionTime";

        /// <summary>
        /// 获取所有机型的标准过站时间
        /// </summary>
        /// <returns></returns>
        public DataTable GetStandardIntermissionTime()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_StandardIntermissionTime);
        }
        #endregion


    }
}
