using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// ���̷������¼ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-10-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ProcAnalysisBM
    {
        #region �����ڲ�����
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

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public ProcAnalysisBM()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iRecords1Id">���</param>
        /// <param name="strProcName">������</param>
        /// <param name="dLastOprationTime">������ʱ��</param>
        /// <param name="strLastOprationResult">���������</param>
        /// <param name="iOprationCount">���ô���</param>
        /// <param name="dCountStartTime">������ʼʱ��</param>
        /// <param name="lTotalLengthBeforeCompress">ѹ��֮ǰ��С������byte��</param>
        /// <param name="lTotalLengthAfterCompress">ѹ��֮���С������byte��</param>
        /// <param name="fTotalProcTimes">����ִ��ʱ���������룩</param>
        /// <param name="fTotalCompressTimes">ѹ��ʱ���������룩</param>
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
        /// ���캯������һ��DataRow���ݸ���ʵ�����
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

        #region �������Զ���
        /// <summary>
        /// ���
        /// </summary>
        public int ProcAnalysisId
        {
            get { return m_iProcAnalysisId; }
            set { m_iProcAnalysisId = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string ProcName
        {
            get { return m_strProcName; }
            set { m_strProcName = value; }
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime LastOprationTime
        {
            get { return m_dLastOprationTime; }
            set { m_dLastOprationTime = value; }
        }

        /// <summary>
        /// ���������
        /// </summary>
        public string LastOprationResult
        {
            get { return m_strLastOprationResult; }
            set { m_strLastOprationResult = value; }
        }

        /// <summary>
        /// ���ô��� 
        /// </summary>
        public int OprationCount
        {
            get { return m_iOprationCount; }
            set { m_iOprationCount = value; }
        }

        /// <summary>
        /// ������ʼʱ��
        /// </summary>
        public DateTime CountStartTime
        {
            get { return m_dCountStartTime; }
            set { m_dCountStartTime = value; }
        }


        /// <summary>
        /// ѹ��֮ǰ��С������byte��
        /// </summary>
        public long TotalLengthBeforeCompress
        {
            get { return m_lTotalLengthBeforeCompress; }
            set { m_lTotalLengthBeforeCompress = value; }
        }

        /// <summary>
        /// ѹ��֮���С������byte��
        /// </summary>
        public long TotalLengthAfterCompress
        {
            get { return m_lTotalLengthAfterCompress; }
            set { m_lTotalLengthAfterCompress = value; }
        }

        /// <summary>
        /// ����ִ��ʱ���������룩
        /// </summary>
        public double TotalProcTimes
        {
            get { return m_fTotalProcTimes; }
            set { m_fTotalProcTimes = value; }
        }

        /// <summary>
        /// ѹ��ʱ���������룩
        /// </summary>
        public double TotalCompressTime
        {
            get { return m_fTotalCompressTimes; }
            set { m_fTotalCompressTimes = value; }
        }

        #endregion

    }
}
