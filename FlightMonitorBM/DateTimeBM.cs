using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ����ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-31
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable] 
    public class DateTimeBM
    {
        #region �����ڲ�����
        private string m_strStartDateTime;
        private string m_strEndDateTime;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public DateTimeBM()
        {
        }

        /// <summary>
        /// ���쿪ʼʱ��
        /// </summary>
        public string StartDateTime
        {
            get { return m_strStartDateTime; }
            set { m_strStartDateTime = value; }
        }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public string EndDateTime
        {
            get { return m_strEndDateTime; }
            set { m_strEndDateTime = value; }
        }
    }
}
