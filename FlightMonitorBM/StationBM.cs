using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航站实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-24
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable] 
    public class StationBM
    {
        #region 对象内部变量
        private int m_iStationInforId;
        private string m_strThreeCode;
        private string m_strStationName;
        private string m_strAirportName;
        private string m_strCommanderOfficeName;
        private string m_strStationSignInFlag;
        private int m_iDayLine;
        private int m_iDelayTimeLine;
        private int m_iJoinTimeLine;
        private int m_iDisconnectTimeLine;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public StationBM()
        {
        }

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public StationBM(DataRow dataRow)
        {
            m_iStationInforId = Convert.ToInt32(dataRow["cniStationInforId"].ToString());
            m_strThreeCode = dataRow["cncThreeCode"].ToString();
            m_strStationName = dataRow["cnvcStationName"].ToString();
            m_strAirportName = dataRow["cnvcAirportName"].ToString();
            m_strCommanderOfficeName = dataRow["cnvcCommanderOfficeName"].ToString();
            m_strStationSignInFlag = dataRow["cnvcStationSignInFlag"].ToString();
            m_iDayLine = Convert.ToInt32(dataRow["cniDayLine"].ToString());
            m_iDelayTimeLine = Convert.ToInt32(dataRow["cniDelayTimeLine"].ToString());
            m_iJoinTimeLine = Convert.ToInt32(dataRow["cniJoinTimeLine"].ToString());
            m_iDisconnectTimeLine = Convert.ToInt32(dataRow["cniDisconnectTimeLine"].ToString());
        }

        #region 对象属性定义
        /// <summary>
        /// 航站序号
        /// </summary>
        public int StationInforId
        {
            get { return m_iStationInforId; }
            set { m_iStationInforId = value; }
        }

        /// <summary>
        /// 三字码
        /// </summary>
        public string ThreeCode
        {
            get { return m_strThreeCode; }
            set { m_strThreeCode = value; }
        }

        /// <summary>
        /// 航站名称
        /// </summary>
        public string StationName
        {
            get { return m_strStationName; }
            set { m_strStationName = value; }
        }

        /// <summary>
        /// 机场名称
        /// </summary>
        public string AirportName
        {
            get { return m_strAirportName; }
            set { m_strAirportName = value; }
        }

        /// <summary>
        /// 现场指挥室名称
        /// </summary>
        public string CommanderOfficeName
        {
            get { return m_strCommanderOfficeName; }
            set { m_strCommanderOfficeName = value; }
        }

        /// <summary>
        /// 签到标示
        /// </summary>
        public string StationSignInFlag
        {
            get { return m_strStationSignInFlag; }
            set { m_strStationSignInFlag = value; }
        }

        /// <summary>
        /// 航班日期分割时刻
        /// </summary>
        public int DayLine
        {
            get { return m_iDayLine; }
            set { m_iDayLine = value; }
        }

        /// <summary>
        /// 延误时间限制
        /// </summary>
        public int DelayTimeLine
        {
            get { return m_iDelayTimeLine; }
            set { m_iDelayTimeLine = value; }
        }

        /// <summary>
        /// 连飞时间限制
        /// </summary>
        public int JoinTimeLine
        {
            get { return m_iJoinTimeLine; }
            set { m_iJoinTimeLine = value; }
        }

        /// <summary>
        /// 网络故障提醒时间（超过一定时间没有收到航班变更则提示用户）
        /// </summary>
        public int DisconnectTimeLine
        {
            get { return m_iDisconnectTimeLine; }
            set { m_iDisconnectTimeLine = value; }
        }

        #endregion

    }
}
