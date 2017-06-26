using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �ɻ���Ϣʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class AC_MISCBM
    {
        #region �����ڲ�����
        private string m_strAC;
        private string m_strSHORT_REG;
        private string m_strLONG_REG;
        private string m_strACTYPE;
        private string m_strOWNER;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public AC_MISCBM()
        {
        }

        /// <summary>
        /// ���캯������һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow"></param>
        public AC_MISCBM(DataRow dataRow)
        {
            m_strAC = dataRow["cncAC"].ToString();
            m_strSHORT_REG = dataRow["cnvcSHORT_REG"].ToString();
            m_strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
            m_strACTYPE = dataRow["cncACTYPE"].ToString();
            m_strOWNER = dataRow["cnvcOWNER"].ToString();
        }

        /// <summary>
        /// ����
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// �ɻ���ע���
        /// </summary>
        public string SHORT_REG
        {
            get { return m_strSHORT_REG; }
            set { m_strSHORT_REG = value; }
        }

        /// <summary>
        /// �ɻ�ע���
        /// </summary>
        public string LONG_REG
        {
            get { return m_strLONG_REG; }
            set { m_strLONG_REG = value; }
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
        /// ������˾������
        /// </summary>
        public string OWNER
        {
            get { return m_strOWNER; }
            set { m_strOWNER = value; }
        }
    }
}
