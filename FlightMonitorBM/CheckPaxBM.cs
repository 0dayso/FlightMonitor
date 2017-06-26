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
    /// 创建日期：2007-06-21
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CheckPaxBM
    {
        #region 对象内部变量
        private string m_strDATOP;
        private string m_strFLTID;
        private int m_iLegNO;
        private string m_strAC;
        private string m_strCKIFlightDate;
        private string m_strCKIFlightNo;
        private string m_strBookNum;
        private string m_strCheckInfo;
        private int m_iCheckNum;
        private int m_iXCRNum;
        private int m_iAdultNum;
        private int m_iChildNum;
        private int m_iInfantNum;
        private int m_iFirstClassNum;
        private int m_iOfficialClassNum;
        private int m_iTouristClassNum;
        private int m_iAscendingPaxNum;
        private int m_iBaggageWeight;
        private int m_iBaggageNum;
        private int m_iIsAutoSaveCheckPaxInfo;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckPaxBM()
        {
            m_strDATOP = "";
            m_strFLTID = "";
            m_iLegNO = 0;
            m_strAC = "";
            m_strCKIFlightDate = "";
            m_strCKIFlightNo = "";
            m_strBookNum = "";
            m_strCheckInfo = "";
            m_iCheckNum = 0;
            m_iXCRNum = 0;
            m_iAdultNum = 0;
            m_iChildNum = 0;
            m_iInfantNum = 0;
            m_iFirstClassNum = 0;
            m_iOfficialClassNum = 0;
            m_iTouristClassNum = 0;
            m_iAscendingPaxNum = 0;
            m_iBaggageWeight = 0;
            m_iBaggageNum = 0;
            m_iIsAutoSaveCheckPaxInfo = 0;
        }

        public CheckPaxBM(DataRow dataRow)
        {
            m_strDATOP = dataRow["cncDATOP"].ToString();
            m_strFLTID = dataRow["cnvcFLTID"].ToString();
            m_iLegNO = Convert.ToInt32(dataRow["cniLEGNO"].ToString());
            m_strAC = dataRow["cnvcAC"].ToString();
            m_strCKIFlightDate = dataRow["cncCKIFlightDate"].ToString();
            m_strCKIFlightNo = dataRow["cnvcCKIFlightNo"].ToString();
            m_strBookNum = dataRow["cnvcBookNum"].ToString();
            m_strCheckInfo = dataRow["cntCheckInfo"].ToString();
            if (dataRow["cniCheckNum"].ToString() != "")
            {
                m_iCheckNum = Convert.ToInt32(dataRow["cniCheckNum"].ToString());
            }
            else
            {
                m_iCheckNum = 0;
            }
            if (dataRow["cniXCRNum"].ToString() != "")
            {
                m_iXCRNum = Convert.ToInt32(dataRow["cniXCRNum"].ToString());
            }
            else
            {
                m_iXCRNum = 0;
            }

            if (dataRow["cniAdultNum"].ToString() != "")
            {
                m_iAdultNum = Convert.ToInt32(dataRow["cniAdultNum"].ToString());
            }
            else
            {
                m_iAdultNum = 0;
            }

            if (dataRow["cniChildNum"].ToString() != "")
            {
                m_iChildNum = Convert.ToInt32(dataRow["cniChildNum"].ToString());
            }
            else
            {
                m_iChildNum = 0;
            }


            if (dataRow["cniInfantNum"].ToString() != "")
            {
                m_iInfantNum = Convert.ToInt32(dataRow["cniInfantNum"].ToString());
            }
            else
            {
                m_iInfantNum = 0;
            }

            if (dataRow["cniFirstClassNum"].ToString() != "")
            {
                m_iFirstClassNum = Convert.ToInt32(dataRow["cniFirstClassNum"].ToString());
            }
            else
            {
                m_iFirstClassNum = 0;
            }

            if (dataRow["cniOfficialClassNum"].ToString() != "")
            {
                m_iOfficialClassNum = Convert.ToInt32(dataRow["cniOfficialClassNum"].ToString());
            }
            else
            {
                m_iOfficialClassNum = 0;
            }

            if (dataRow["cniTouristClassNum"].ToString() != "")
            {
                m_iTouristClassNum = Convert.ToInt32(dataRow["cniTouristClassNum"].ToString());
            }
            else
            {
                m_iTouristClassNum = 0;
            }


            if (dataRow["cniAscendingPaxNum"].ToString() != "")
            {
                m_iAscendingPaxNum = Convert.ToInt32(dataRow["cniAscendingPaxNum"].ToString());
            }
            else
            {
                m_iAscendingPaxNum = 0;
            }

            if (dataRow["cniBaggageWeight"].ToString() != "")
            {
                m_iBaggageWeight = Convert.ToInt32(dataRow["cniBaggageWeight"].ToString());
            }
            else
            {
                m_iBaggageWeight = 0;
            }

            if (dataRow["cniBaggageNum"].ToString() != "")
            {
                m_iBaggageNum = Convert.ToInt32(dataRow["cniBaggageNum"].ToString());
            }
            else
            {
                m_iBaggageNum = 0;
            }

            if (dataRow["cniIsAutoSaveCheckPaxInfo"].ToString() != "")
            {
                m_iIsAutoSaveCheckPaxInfo = Convert.ToInt32(dataRow["cniIsAutoSaveCheckPaxInfo"].ToString());
            }
            else
            {
                m_iIsAutoSaveCheckPaxInfo = 0;
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
        /// 订座人数
        /// </summary>
        public string BookNum
        {
            get { return m_strBookNum; }
            set { m_strBookNum = value; }
        }

        /// <summary>
        /// 值机信息
        /// </summary>
        public string CheckInfo
        {
            get { return m_strCheckInfo; }
            set { m_strCheckInfo = value; }
        }
        
        /// <summary>
        /// 值机人数
        /// </summary>
        public int CheckNum
        {
            get { return m_iCheckNum; }
            set { m_iCheckNum = value; }
        }

        /// <summary>
        /// 加机组人数
        /// </summary>
        public int XCRNum
        {
            get { return m_iXCRNum; }
            set { m_iXCRNum = value; }
        }

        /// <summary>
        /// 成人数
        /// </summary>
        public int AdultNum
        {
            get { return m_iAdultNum; }
            set { m_iAdultNum = value; }
        }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNum
        {
            get { return m_iChildNum; }
            set { m_iChildNum = value; }
        }

        /// <summary>
        /// 婴儿人数
        /// </summary>
        public int InfantNum
        {
            get { return m_iInfantNum; }
            set { m_iInfantNum = value; }
        }      

        /// <summary>
        /// 头等舱人数
        /// </summary>
        public int FirstClassNum
        {
            get { return m_iFirstClassNum; }
            set { m_iFirstClassNum = value; }
        }

        /// <summary>
        /// 公务舱人数
        /// </summary>
        public int OfficialClassNum
        {
            get { return m_iOfficialClassNum; }
            set { m_iOfficialClassNum = value; }
        }

        /// <summary>
        /// 经济舱人数
        /// </summary>
        public int TouristClassNum
        {
            get { return m_iTouristClassNum; }
            set { m_iTouristClassNum = value; }
        }

        /// <summary>
        /// 升舱人数
        /// </summary>
        public int AscendingPaxNum
        {
            get { return m_iAscendingPaxNum; }
            set { m_iAscendingPaxNum = value; }
        }
        
        /// <summary>
        /// 行李重量
        /// </summary>
        public int BaggageWeight
        {
            get { return m_iBaggageWeight; }
            set { m_iBaggageWeight = value; }
        }

        /// <summary>
        /// 行李件数
        /// </summary>
        public int BaggageNum
        {
            get { return m_iBaggageNum; }
            set { m_iBaggageNum = value; }
        }

        public int IsAutoSaveCheckPaxInfo
        {
            get { return m_iIsAutoSaveCheckPaxInfo; }
            set { m_iIsAutoSaveCheckPaxInfo = value; }
        }
    }
}
