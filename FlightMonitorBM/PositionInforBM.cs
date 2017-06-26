using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 席位信息实体
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionInforBM
    {
        #region 对象内部变量
        private int m_iInfoid;
        private string m_strLONG_REG;
        private int m_iPositionID;
        private string m_strPositionName;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionInforBM()
        {
        }

        /// <summary>
        /// 构造函数:把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="rowItem"></param>
        public PositionInforBM(DataRow rowItem)
        {
            m_iInfoid = Convert.ToInt32(rowItem["cniInfoid"].ToString());
            m_strLONG_REG = rowItem["cnvcLONG_REG"].ToString();
            m_iPositionID = Convert.ToInt32(rowItem["cniPositionID"].ToString());
            m_strPositionName = rowItem["cnvcPositionName"].ToString();
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int Infoid
        {
            get { return m_iInfoid; }
            set { m_iInfoid = value; }
        }

        /// <summary>
        /// 飞机长注册号
        /// </summary>
        public string LONG_REG
        {
            get { return m_strLONG_REG; }
            set { m_strLONG_REG = value; }
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
