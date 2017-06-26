using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �ÿ�����ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PaxNameListBM
    {
        #region �����ڲ�����
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
        /// ���캯��
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
        /// ���캯��
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
        /// ��������
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int LegNO
        {
            get { return m_iLegNO; }
            set { m_iLegNO = value; }
        }

        /// <summary>
        /// �ɻ���
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// ��ۺ�������
        /// </summary>
        public string CKIFlightDate
        {
            get { return m_strCKIFlightDate; }
            set { m_strCKIFlightDate = value; }
        }

        /// <summary>
        /// ��ۺ����
        /// </summary>
        public string CKIFlightNo
        {
            get { return m_strCKIFlightNo; }
            set { m_strCKIFlightNo = value; }
        }

        /// <summary>
        /// ֵ���ÿ�����
        /// </summary>
        public string PaxNameList
        {
            get { return m_strPaxNameList; }
            set { m_strPaxNameList = value; }
        }

        /// <summary>
        /// ֵ���ÿ������Ƿ��Ѿ��ڷɻ���ɺ󱣴�
        /// </summary>
        public int IsAutoSaveCheckPaxNameList
        {
            get { return m_iIsAutoSaveCheckPaxNameList; }
            set { m_iIsAutoSaveCheckPaxNameList = value; }
        }

    }
}
