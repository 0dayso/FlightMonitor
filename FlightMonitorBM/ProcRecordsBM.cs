using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// 过程记录表记录实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-10-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ProcRecordsBM
    {
        #region 对象内部变量
        private int m_iProcRecordsId;
        private string m_strProcName;
        private DateTime m_dOprationTime;
        private string m_strOprationResult;
        private int m_iOprationCount;
        private int m_iLengthBeforeCompress;
        private int m_iLengthAfterCompress;
        private double m_fProcTimes;
        private double m_fCompressTimes;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcRecordsBM()
        {
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iProcRecordsId">序号</param>
        /// <param name="strProcName">过程名</param>
        /// <param name="dOprationTime">操作时间</param>
        /// <param name="strOprationResult">操作结果</param>
        /// <param name="iOprationCount">调用次数</param>
        /// <param name="iLengthBeforeCompress">压缩之前大小（byte）</param>
        /// <param name="iLengthAfterCompress">压缩之后大小（byte）</param>
        /// <param name="fProcTimes">过程执行时间（秒）</param>
        /// <param name="fCompressTimes">压缩时间（秒）</param>
        public ProcRecordsBM(int iProcRecordsId, string strProcName, DateTime dOprationTime, 
            string strOprationResult, int iOprationCount, int iLengthBeforeCompress, int iLengthAfterCompress,
            double fProcTimes, double fCompressTimes)
        {
            m_iProcRecordsId = iProcRecordsId;
            m_strProcName = strProcName;
            m_dOprationTime = dOprationTime;
            m_strOprationResult = strOprationResult;
            m_iOprationCount = iOprationCount;
            m_iLengthBeforeCompress = iLengthBeforeCompress;
            m_iLengthAfterCompress = iLengthAfterCompress;
            m_fProcTimes = fProcTimes;
            m_fCompressTimes = fCompressTimes;
        }

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// </summary>
        public ProcRecordsBM(DataRow dataRow)
        {
            m_iProcRecordsId = Convert.ToInt32(dataRow["cniProcRecordsId"].ToString());
            m_strProcName = dataRow["cnvcProcName"].ToString();
            m_dOprationTime = Convert.ToDateTime(dataRow["cndOprationTime"].ToString());
            m_strOprationResult = dataRow["cnvcOprationResult"].ToString();
            m_iOprationCount = Convert.ToInt32(dataRow["cniOprationCount"].ToString());
            m_iLengthBeforeCompress = Convert.ToInt32(dataRow["cniLengthBeforeCompress"].ToString());
            m_iLengthAfterCompress = Convert.ToInt32(dataRow["cniLengthAfterCompress"].ToString());
            m_fProcTimes = Convert.ToDouble(dataRow["cnfProcTimes"].ToString());
            m_fCompressTimes = Convert.ToDouble(dataRow["cnfCompressTimes"].ToString());

        }
        #endregion

        #region 对象属性定义
        /// <summary>
        /// 序号
        /// </summary>
        public int ProcRecordsId
        {
            get { return m_iProcRecordsId; }
            set { m_iProcRecordsId = value; }
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
        /// 操作时间
        /// </summary>
        public DateTime OprationTime
        {
            get { return m_dOprationTime; }
            set { m_dOprationTime = value; }
        }

        /// <summary>
        /// 操作结果
        /// </summary>
        public string OprationResult
        {
            get { return m_strOprationResult; }
            set { m_strOprationResult = value; }
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
        /// 压缩之前大小（byte）
        /// </summary>
        public int LengthBeforeCompress
        {
            get { return m_iLengthBeforeCompress; }
            set { m_iLengthBeforeCompress = value; }
        }

        /// <summary>
        /// 压缩之后大小（byte）
        /// </summary>
        public int LengthAfterCompress
        {
            get { return m_iLengthAfterCompress; }
            set { m_iLengthAfterCompress = value; }
        }

        /// <summary>
        /// 过程执行时间（秒）
        /// </summary>
        public double ProcTimes
        {
            get { return m_fProcTimes; }
            set { m_fProcTimes = value; }
        }

        /// <summary>
        /// 压缩时间（秒）
        /// </summary>
        public double CompressTimes
        {
            get { return m_fCompressTimes; }
            set { m_fCompressTimes = value; }
        }

        #endregion

    }
}
