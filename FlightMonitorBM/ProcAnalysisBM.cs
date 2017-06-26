using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// 过程分析表记录实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-10-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ProcAnalysisBM
    {
        #region 对象内部变量
        private int m_iProcAnalysisId;
        private string m_strProcName;
        private DateTime m_dLastOprationTime;
        private string m_strLastOprationResult;
        private int m_iOprationCount;
        private DateTime m_dCountStartTime;
        private long m_lTotalLengthBeforeCompress;
        private long m_lTotalLengthAfterCompress;
        private double m_fTotalProcTimes;
        private double m_fTotalCompressTimes;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcAnalysisBM()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iRecords1Id">序号</param>
        /// <param name="strProcName">过程名</param>
        /// <param name="dLastOprationTime">最后操作时间</param>
        /// <param name="strLastOprationResult">最后操作结果</param>
        /// <param name="iOprationCount">调用次数</param>
        /// <param name="dCountStartTime">计数开始时间</param>
        /// <param name="lTotalLengthBeforeCompress">压缩之前大小总数（byte）</param>
        /// <param name="lTotalLengthAfterCompress">压缩之后大小总数（byte）</param>
        /// <param name="fTotalProcTimes">过程执行时间总数（秒）</param>
        /// <param name="fTotalCompressTimes">压缩时间总数（秒）</param>
        public ProcAnalysisBM(int iProcAnalysisId, string strProcName, DateTime dLastOprationTime, 
            string strLastOprationResult, int iOprationCount, DateTime dCountStartTime,
            long lTotalLengthBeforeCompress, long lTotalLengthAfterCompress,
            double fTotalProcTimes, double fTotalCompressTimes)
        {
            m_iProcAnalysisId = iProcAnalysisId;
            m_strProcName = strProcName;
            m_dLastOprationTime = dLastOprationTime;
            m_strLastOprationResult = strLastOprationResult;
            m_iOprationCount = iOprationCount;
            m_dCountStartTime = dCountStartTime;
            m_lTotalLengthBeforeCompress = lTotalLengthBeforeCompress;
            m_lTotalLengthAfterCompress = lTotalLengthAfterCompress;
            m_fTotalProcTimes = fTotalProcTimes;
            m_fTotalCompressTimes = fTotalCompressTimes;
        }

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// </summary>
        public ProcAnalysisBM(DataRow dataRow)
        {
            m_iProcAnalysisId = Convert.ToInt32(dataRow["cniProcAnalysisId"].ToString());
            m_strProcName = dataRow["cnvcProcName"].ToString();
            m_dLastOprationTime = Convert.ToDateTime(dataRow["cndLastOprationTime"].ToString());
            m_strLastOprationResult = dataRow["cnvcLastOprationResult"].ToString();
            m_iOprationCount = Convert.ToInt32(dataRow["cniOprationCount"].ToString());
            m_dCountStartTime = Convert.ToDateTime(dataRow["cndCountStartTime"].ToString());
            m_lTotalLengthBeforeCompress = Convert.ToInt64(dataRow["cnlTotalLengthBeforeCompress"].ToString());
            m_lTotalLengthAfterCompress = Convert.ToInt64(dataRow["cnlTotalLengthAfterCompress"].ToString());
            m_fTotalProcTimes = Convert.ToDouble(dataRow["cnfTotalProcTimes"].ToString());
            m_fTotalCompressTimes = Convert.ToDouble(dataRow["cnfTotalCompressTimes"].ToString());
        }
        #endregion

        #region 对象属性定义
        /// <summary>
        /// 序号
        /// </summary>
        public int ProcAnalysisId
        {
            get { return m_iProcAnalysisId; }
            set { m_iProcAnalysisId = value; }
        }

        /// <summary>
        /// 过程名
        /// </summary>
        public string ProcName
        {
            get { return m_strProcName; }
            set { m_strProcName = value; }
        }

        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime LastOprationTime
        {
            get { return m_dLastOprationTime; }
            set { m_dLastOprationTime = value; }
        }

        /// <summary>
        /// 最后操作结果
        /// </summary>
        public string LastOprationResult
        {
            get { return m_strLastOprationResult; }
            set { m_strLastOprationResult = value; }
        }

        /// <summary>
        /// 调用次数 
        /// </summary>
        public int OprationCount
        {
            get { return m_iOprationCount; }
            set { m_iOprationCount = value; }
        }

        /// <summary>
        /// 计数开始时间
        /// </summary>
        public DateTime CountStartTime
        {
            get { return m_dCountStartTime; }
            set { m_dCountStartTime = value; }
        }


        /// <summary>
        /// 压缩之前大小总数（byte）
        /// </summary>
        public long TotalLengthBeforeCompress
        {
            get { return m_lTotalLengthBeforeCompress; }
            set { m_lTotalLengthBeforeCompress = value; }
        }

        /// <summary>
        /// 压缩之后大小总数（byte）
        /// </summary>
        public long TotalLengthAfterCompress
        {
            get { return m_lTotalLengthAfterCompress; }
            set { m_lTotalLengthAfterCompress = value; }
        }

        /// <summary>
        /// 过程执行时间总数（秒）
        /// </summary>
        public double TotalProcTimes
        {
            get { return m_fTotalProcTimes; }
            set { m_fTotalProcTimes = value; }
        }

        /// <summary>
        /// 压缩时间总数（秒）
        /// </summary>
        public double TotalCompressTime
        {
            get { return m_fTotalCompressTimes; }
            set { m_fTotalCompressTimes = value; }
        }

        #endregion

    }
}
