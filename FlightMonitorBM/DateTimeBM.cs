using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 当天实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-31
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable] 
    public class DateTimeBM
    {
        #region 对象内部变量
        private string m_strStartDateTime;
        private string m_strEndDateTime;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DateTimeBM()
        {
        }

        /// <summary>
        /// 当天开始时间
        /// </summary>
        public string StartDateTime
        {
            get { return m_strStartDateTime; }
            set { m_strStartDateTime = value; }
        }

        /// <summary>
        /// 当天结束时间
        /// </summary>
        public string EndDateTime
        {
            get { return m_strEndDateTime; }
            set { m_strEndDateTime = value; }
        }
    }
}
