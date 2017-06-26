using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// ���̼�¼���¼ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-10-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ProcRecordsBM
    {
        #region �����ڲ�����
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

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public ProcRecordsBM()
        {
        }
        
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iProcRecordsId">���</param>
        /// <param name="strProcName">������</param>
        /// <param name="dOprationTime">����ʱ��</param>
        /// <param name="strOprationResult">�������</param>
        /// <param name="iOprationCount">���ô���</param>
        /// <param name="iLengthBeforeCompress">ѹ��֮ǰ��С��byte��</param>
        /// <param name="iLengthAfterCompress">ѹ��֮���С��byte��</param>
        /// <param name="fProcTimes">����ִ��ʱ�䣨�룩</param>
        /// <param name="fCompressTimes">ѹ��ʱ�䣨�룩</param>
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
        /// ���캯������һ��DataRow���ݸ���ʵ�����
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

        #region �������Զ���
        /// <summary>
        /// ���
        /// </summary>
        public int ProcRecordsId
        {
            get { return m_iProcRecordsId; }
            set { m_iProcRecordsId = value; }
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
        /// ����ʱ��
        /// </summary>
        public DateTime OprationTime
        {
            get { return m_dOprationTime; }
            set { m_dOprationTime = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string OprationResult
        {
            get { return m_strOprationResult; }
            set { m_strOprationResult = value; }
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
        /// ѹ��֮ǰ��С��byte��
        /// </summary>
        public int LengthBeforeCompress
        {
            get { return m_iLengthBeforeCompress; }
            set { m_iLengthBeforeCompress = value; }
        }

        /// <summary>
        /// ѹ��֮���С��byte��
        /// </summary>
        public int LengthAfterCompress
        {
            get { return m_iLengthAfterCompress; }
            set { m_iLengthAfterCompress = value; }
        }

        /// <summary>
        /// ����ִ��ʱ�䣨�룩
        /// </summary>
        public double ProcTimes
        {
            get { return m_fProcTimes; }
            set { m_fProcTimes = value; }
        }

        /// <summary>
        /// ѹ��ʱ�䣨�룩
        /// </summary>
        public double CompressTimes
        {
            get { return m_fCompressTimes; }
            set { m_fCompressTimes = value; }
        }

        #endregion

    }
}
