using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ��׼��վʱ��ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class IntermissionTimeBM
    {
        #region �����ڲ�����
        private int m_iIntermissionTimeId;
        private string m_strACTYPE;
        private int m_iAirportPaxType;
        private int m_iDomIntermissionTime;
        private int m_iInterIntermissionTime;
        private int m_iInitialCloseCabinTime;
        private int m_iIntermissionCloseCabinTime;       
        #endregion 

        /// <summary>
        /// ���캯��
        /// </summary>
        public IntermissionTimeBM()
        {
        }

        /// <summary>
        /// ���캯��:��һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">������</param>
        public IntermissionTimeBM(DataRow dataRow)
        {
            m_iIntermissionTimeId = Convert.ToInt32(dataRow["cniIntermissionTimeId"].ToString());
            m_strACTYPE = dataRow["cncACTYPE"].ToString();
            m_iAirportPaxType = Convert.ToInt32(dataRow["cniAirportPaxType"].ToString());
            m_iDomIntermissionTime = Convert.ToInt32(dataRow["cniDomIntermissionTime"].ToString());
            m_iInterIntermissionTime = Convert.ToInt32(dataRow["cniInterIntermissionTime"].ToString());
            m_iInitialCloseCabinTime = Convert.ToInt32(dataRow["cniInitialCloseCabinTime"].ToString());
            m_iIntermissionCloseCabinTime = Convert.ToInt32(dataRow["cniIntermissionCloseCabinTime"].ToString());
        }

        /// <summary>
        /// ���
        /// </summary>
        public int IntermissionTimeId
        {
            get { return m_iIntermissionTimeId; }
            set { m_iIntermissionTimeId = value; }
        }

        /// <summary>
        /// FOC����
        /// </summary>
        public string ACTYPE
        {
            get { return m_strACTYPE; }
            set { m_strACTYPE = value; }
        }

        /// <summary>
        /// �������ͣ����ÿ����֣�
        /// </summary>
        public int AirportPaxType
        {
            get { return m_iAirportPaxType; }
            set { m_iAirportPaxType = value; }
        }

        /// <summary>
        /// ���ں����վ��׼ʱ��
        /// </summary>
        public int DomIntermissionTime
        {
            get { return m_iDomIntermissionTime; }
            set { m_iDomIntermissionTime = value; }
        }

        /// <summary>
        /// ���ʺ����վ��׼
        /// </summary>
        public int InterIntermissionTime
        {
            get { return m_iInterIntermissionTime; }
            set { m_iInterIntermissionTime = value; }
        }

        /// <summary>
        /// ʼ������زձ�׼
        /// </summary>
        public int InitialCloseCabinTime
        {
            get { return m_iInitialCloseCabinTime; }
            set { m_iInitialCloseCabinTime = value; }
        }

        /// <summary>
        /// ��վ����زձ�׼
        /// </summary>
        public int IntermissionCloseCabinTime
        {
            get { return m_iIntermissionCloseCabinTime; }
            set { m_iIntermissionCloseCabinTime = value; }
        }
    }
}
