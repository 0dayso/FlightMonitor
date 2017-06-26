using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 值机旅客实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-22
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class TrasitPaxBM
    {
        #region 对象内部变量
        private string m_strDATOP;
        private string m_strFLTID;
        private int m_iLegNO;
        private string m_strAC;
        private string m_strCKIFlightDate;
        private string m_strCKIFlightNo;
        private string m_strTransitPaxTag;
        private string m_strTransitPax;
        private int m_iIsAutoSaveTransitPax;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public TrasitPaxBM()
        {
            m_strDATOP = "";
            m_strFLTID = "";
            m_iLegNO = 0;
            m_strAC = "";
            m_strCKIFlightDate = "";
            m_strCKIFlightNo = "";
            m_strTransitPaxTag = "";
            m_strTransitPax = "";
            m_iIsAutoSaveTransitPax = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow"></param>
        public TrasitPaxBM(DataRow dataRow)
        {
            m_strDATOP = dataRow["cncDATOP"].ToString();
            m_strFLTID = dataRow["cnvcFLTID"].ToString();
            m_iLegNO = Convert.ToInt32(dataRow["cniLEGNO"].ToString());
            m_strAC = dataRow["cnvcAC"].ToString();
            m_strCKIFlightDate = dataRow["cncCKIFlightDate"].ToString();
            m_strCKIFlightNo = dataRow["cnvcCKIFlightNo"].ToString();
            m_strTransitPaxTag = dataRow["cnbTransitPaxTag"].ToString();
            m_strTransitPax = dataRow["cntTransitPax"].ToString();

            if (dataRow["cniIsAutoSaveTransitPax"].ToString() != "")
            {
                m_iIsAutoSaveTransitPax = Convert.ToInt32(dataRow["cniIsAutoSaveTransitPax"].ToString());
            }
            else
            {
                m_iIsAutoSaveTransitPax = 0;
            }
        }

        /// <summary>
        /// 航班日期
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }

        /// <summary>
        /// 航班号
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// 航段序号
        /// </summary>
        public int LegNO
        {
            get { return m_iLegNO; }
            set { m_iLegNO = value; }
        }

        /// <summary>
        /// 飞机号
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// 离港航班日期
        /// </summary>
        public string CKIFlightDate
        {
            get { return m_strCKIFlightDate; }
            set { m_strCKIFlightDate = value; }
        }

        /// <summary>
        /// 离港航班号
        /// </summary>
        public string CKIFlightNo
        {
            get { return m_strCKIFlightNo; }
            set { m_strCKIFlightNo = value; }
        }

        /// <summary>
        /// 中转旅客标识
        /// </summary>
        public string TransitPaxTag
        {
            get { return m_strTransitPaxTag; }
            set { m_strTransitPaxTag = value; }
        }

        /// <summary>
        /// 中转旅客信息
        /// </summary>
        public string TransitPax
        {
            get { return m_strTransitPax; }
            set { m_strTransitPax = value; }
        }

        /// <summary>
        /// 是否在飞机起飞后保存了中转连程旅客信息
        /// </summary>
        public int IsAutoSaveTransitPax
        {
            get { return m_iIsAutoSaveTransitPax; }
            set { m_iIsAutoSaveTransitPax = value; }
        }

    }
}
