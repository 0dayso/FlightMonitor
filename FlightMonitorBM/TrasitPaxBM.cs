using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ֵ���ÿ�ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-22
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class TrasitPaxBM
    {
        #region �����ڲ�����
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
        /// ���캯��
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
        /// ���캯��
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
        /// ��ת�ÿͱ�ʶ
        /// </summary>
        public string TransitPaxTag
        {
            get { return m_strTransitPaxTag; }
            set { m_strTransitPaxTag = value; }
        }

        /// <summary>
        /// ��ת�ÿ���Ϣ
        /// </summary>
        public string TransitPax
        {
            get { return m_strTransitPax; }
            set { m_strTransitPax = value; }
        }

        /// <summary>
        /// �Ƿ��ڷɻ���ɺ󱣴�����ת�����ÿ���Ϣ
        /// </summary>
        public int IsAutoSaveTransitPax
        {
            get { return m_iIsAutoSaveTransitPax; }
            set { m_iIsAutoSaveTransitPax = value; }
        }

    }
}
