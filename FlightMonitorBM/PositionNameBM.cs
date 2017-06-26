using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 席位名称实体
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionNameBM
    {
        #region 对象内部变量
        private int m_iPositionID;
        private string m_strPositionName;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionNameBM()
        {
        }

        /// <summary>
        /// 构造函数:把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="rowItem"></param>
        public PositionNameBM(DataRow rowItem)
        {
            m_iPositionID = Convert.ToInt32(rowItem["cniPositionID"].ToString());
            m_strPositionName = rowItem["cnvcPositionName"].ToString();
        }

        /// <summary>
        /// 席位编号
        /// </summary>
        public int PositionID
        {
            get { return m_iPositionID; }
            set { m_iPositionID = value; }
        }

        /// <summary>
        /// 席位名称
        /// </summary>
        public string PositionName
        {
            get { return m_strPositionName; }
            set { m_strPositionName = value; }
        }
    }
}
