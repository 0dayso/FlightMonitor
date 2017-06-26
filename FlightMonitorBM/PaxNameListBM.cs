using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 旅客名单实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PaxNameListBM
    {
        #region 对象内部变量
        private string m_strDATOP;
        private string m_strFLTID;
        private int m_iLegNO;
        private string m_strAC;
        private string m_strCKIFlightDate;
        private string m_strCKIFlightNo;
        private string m_strPaxNameList;
        private int m_iIsAutoSaveCheckPaxNameList;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaxNameListBM()
        {
            m_strDATOP = "";
            m_strFLTID = "";
            m_iLegNO = 0;
            m_strAC = "";
            m_strCKIFlightDate = "";
            m_strCKIFlightNo = "";
            m_strPaxNameList = "";
            m_iIsAutoSaveCheckPaxNameList = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow"></param>
        public PaxNameListBM(DataRow dataRow)
        {
            m_strDATOP = dataRow["cncDATOP"].ToString();
            m_strFLTID = dataRow["cnvcFLTID"].ToString();
            m_iLegNO = Convert.ToInt32(dataRow["cniLEGNO"].ToString());
            m_strAC = dataRow["cnvcAC"].ToString();
            m_strCKIFlightDate = dataRow["cncCKIFlightDate"].ToString();
            m_strCKIFlightNo = dataRow["cnvcCKIFlightNo"].ToString();
            m_strPaxNameList = dataRow["cntPaxNameList"].ToString();
            if (dataRow["cniIsAutoSaveCheckPaxNameList"].ToString() != "")
            {
                m_iIsAutoSaveCheckPaxNameList = Convert.ToInt32(dataRow["cniIsAutoSaveCheckPaxNameList"].ToString());
            }
            else
            {
                m_iIsAutoSaveCheckPaxNameList = 0;
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
        /// 值机旅客名单
        /// </summary>
        public string PaxNameList
        {
            get { return m_strPaxNameList; }
            set { m_strPaxNameList = value; }
        }

        /// <summary>
        /// 值机旅客名单是否已经在飞机起飞后保存
        /// </summary>
        public int IsAutoSaveCheckPaxNameList
        {
            get { return m_iIsAutoSaveCheckPaxNameList; }
            set { m_iIsAutoSaveCheckPaxNameList = value; }
        }

    }
}
