using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ϯλ����ʵ��
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionNameBM
    {
        #region �����ڲ�����
        private int m_iPositionID;
        private string m_strPositionName;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionNameBM()
        {
        }

        /// <summary>
        /// ���캯��:��һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="rowItem"></param>
        public PositionNameBM(DataRow rowItem)
        {
            m_iPositionID = Convert.ToInt32(rowItem["cniPositionID"].ToString());
            m_strPositionName = rowItem["cnvcPositionName"].ToString();
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
