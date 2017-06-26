using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ϯλ��Ϣʵ��
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionInforBM
    {
        #region �����ڲ�����
        private int m_iInfoid;
        private string m_strLONG_REG;
        private int m_iPositionID;
        private string m_strPositionName;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionInforBM()
        {
        }

        /// <summary>
        /// ���캯��:��һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="rowItem"></param>
        public PositionInforBM(DataRow rowItem)
        {
            m_iInfoid = Convert.ToInt32(rowItem["cniInfoid"].ToString());
            m_strLONG_REG = rowItem["cnvcLONG_REG"].ToString();
            m_iPositionID = Convert.ToInt32(rowItem["cniPositionID"].ToString());
            m_strPositionName = rowItem["cnvcPositionName"].ToString();
        }

        /// <summary>
        /// ���
        /// </summary>
        public int Infoid
        {
            get { return m_iInfoid; }
            set { m_iInfoid = value; }
        }

        /// <summary>
        /// �ɻ���ע���
        /// </summary>
        public string LONG_REG
        {
            get { return m_strLONG_REG; }
            set { m_strLONG_REG = value; }
        }

        /// <summary>
        /// ϯλ���
        /// </summary>
        public int PositionID
        {
            get { return m_iPositionID; }
            set { m_iPositionID = value; }
        }

        /// <summary>
        /// ϯλ����
        /// </summary>
        public string PositionName
        {
            get { return m_strPositionName; }
            set { m_strPositionName = value; }
        }
    }
}
