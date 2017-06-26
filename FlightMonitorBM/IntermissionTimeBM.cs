using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 标准过站时间实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class IntermissionTimeBM
    {
        #region 对象内部变量
        private int m_iIntermissionTimeId;
        private string m_strACTYPE;
        private int m_iAirportPaxType;
        private int m_iDomIntermissionTime;
        private int m_iInterIntermissionTime;
        private int m_iInitialCloseCabinTime;
        private int m_iIntermissionCloseCabinTime;       
        #endregion 

        /// <summary>
        /// 构造函数
        /// </summary>
        public IntermissionTimeBM()
        {
        }

        /// <summary>
        /// 构造函数:把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public IntermissionTimeBM(DataRow dataRow)
        {
            m_iIntermissionTimeId = Convert.ToInt32(dataRow["cniIntermissionTimeId"].ToString());
            m_strACTYPE = dataRow["cncACTYPE"].ToString();
            m_iAirportPaxType = Convert.ToInt32(dataRow["cniAirportPaxType"].ToString());
            m_iDomIntermissionTime = Convert.ToInt32(dataRow["cniDomIntermissionTime"].ToString());
            m_iInterIntermissionTime = Convert.ToInt32(dataRow["cniInterIntermissionTime"].ToString());
            m_iInitialCloseCabinTime = Convert.ToInt32(dataRow["cniInitialCloseCabinTime"].ToString());
            m_iIntermissionCloseCabinTime = Convert.ToInt32(dataRow["cniIntermissionCloseCabinTime"].ToString());
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int IntermissionTimeId
        {
            get { return m_iIntermissionTimeId; }
            set { m_iIntermissionTimeId = value; }
        }

        /// <summary>
        /// FOC机型
        /// </summary>
        public string ACTYPE
        {
            get { return m_strACTYPE; }
            set { m_strACTYPE = value; }
        }

        /// <summary>
        /// 机场类型（按旅客量分）
        /// </summary>
        public int AirportPaxType
        {
            get { return m_iAirportPaxType; }
            set { m_iAirportPaxType = value; }
        }

        /// <summary>
        /// 国内航班过站标准时间
        /// </summary>
        public int DomIntermissionTime
        {
            get { return m_iDomIntermissionTime; }
            set { m_iDomIntermissionTime = value; }
        }

        /// <summary>
        /// 国际航班过站标准
        /// </summary>
        public int InterIntermissionTime
        {
            get { return m_iInterIntermissionTime; }
            set { m_iInterIntermissionTime = value; }
        }

        /// <summary>
        /// 始发航班关舱标准
        /// </summary>
        public int InitialCloseCabinTime
        {
            get { return m_iInitialCloseCabinTime; }
            set { m_iInitialCloseCabinTime = value; }
        }

        /// <summary>
        /// 过站航班关舱标准
        /// </summary>
        public int IntermissionCloseCabinTime
        {
            get { return m_iIntermissionCloseCabinTime; }
            set { m_iIntermissionCloseCabinTime = value; }
        }
    }
}
