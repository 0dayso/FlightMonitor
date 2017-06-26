using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ��վʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-24
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable] 
    public class StationBM
    {
        #region �����ڲ�����
        private int m_iStationInforId;
        private string m_strThreeCode;
        private string m_strStationName;
        private string m_strAirportName;
        private string m_strCommanderOfficeName;
        private string m_strStationSignInFlag;
        private int m_iDayLine;
        private int m_iDelayTimeLine;
        private int m_iJoinTimeLine;
        private int m_iDisconnectTimeLine;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public StationBM()
        {
        }

        /// <summary>
        /// ���캯������һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">������</param>
        public StationBM(DataRow dataRow)
        {
            m_iStationInforId = Convert.ToInt32(dataRow["cniStationInforId"].ToString());
            m_strThreeCode = dataRow["cncThreeCode"].ToString();
            m_strStationName = dataRow["cnvcStationName"].ToString();
            m_strAirportName = dataRow["cnvcAirportName"].ToString();
            m_strCommanderOfficeName = dataRow["cnvcCommanderOfficeName"].ToString();
            m_strStationSignInFlag = dataRow["cnvcStationSignInFlag"].ToString();
            m_iDayLine = Convert.ToInt32(dataRow["cniDayLine"].ToString());
            m_iDelayTimeLine = Convert.ToInt32(dataRow["cniDelayTimeLine"].ToString());
            m_iJoinTimeLine = Convert.ToInt32(dataRow["cniJoinTimeLine"].ToString());
            m_iDisconnectTimeLine = Convert.ToInt32(dataRow["cniDisconnectTimeLine"].ToString());
        }

        #region �������Զ���
        /// <summary>
        /// ��վ���
        /// </summary>
        public int StationInforId
        {
            get { return m_iStationInforId; }
            set { m_iStationInforId = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string ThreeCode
        {
            get { return m_strThreeCode; }
            set { m_strThreeCode = value; }
        }

        /// <summary>
        /// ��վ����
        /// </summary>
        public string StationName
        {
            get { return m_strStationName; }
            set { m_strStationName = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string AirportName
        {
            get { return m_strAirportName; }
            set { m_strAirportName = value; }
        }

        /// <summary>
        /// �ֳ�ָ��������
        /// </summary>
        public string CommanderOfficeName
        {
            get { return m_strCommanderOfficeName; }
            set { m_strCommanderOfficeName = value; }
        }

        /// <summary>
        /// ǩ����ʾ
        /// </summary>
        public string StationSignInFlag
        {
            get { return m_strStationSignInFlag; }
            set { m_strStationSignInFlag = value; }
        }

        /// <summary>
        /// �������ڷָ�ʱ��
        /// </summary>
        public int DayLine
        {
            get { return m_iDayLine; }
            set { m_iDayLine = value; }
        }

        /// <summary>
        /// ����ʱ������
        /// </summary>
        public int DelayTimeLine
        {
            get { return m_iDelayTimeLine; }
            set { m_iDelayTimeLine = value; }
        }

        /// <summary>
        /// ����ʱ������
        /// </summary>
        public int JoinTimeLine
        {
            get { return m_iJoinTimeLine; }
            set { m_iJoinTimeLine = value; }
        }

        /// <summary>
        /// �����������ʱ�䣨����һ��ʱ��û���յ�����������ʾ�û���
        /// </summary>
        public int DisconnectTimeLine
        {
            get { return m_iDisconnectTimeLine; }
            set { m_iDisconnectTimeLine = value; }
        }

        #endregion

    }
}
