using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ��վ������Աʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-07-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CommanderInforBM
    {
        #region �����ڲ�����
        private int m_iCommanderInforId;
        private string m_strCommanderAccount;
        private string m_strCommanderName;
        private string m_strCommanderType;
        private string m_strThreeCode;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public CommanderInforBM()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dataRow"></param>
        public CommanderInforBM(DataRow dataRow)
        {
            m_iCommanderInforId = Convert.ToInt32(dataRow["cniCommanderInforId"].ToString());
            m_strCommanderAccount = dataRow["cnvCommanderAccount"].ToString();
            m_strCommanderName = dataRow["cnvcCommanderName"].ToString();
            m_strCommanderType = dataRow["cnvcCommanderType"].ToString();
            m_strThreeCode = dataRow["cncThreeCode"].ToString();
        }

        /// <summary>
        /// ���
        /// </summary>
        public int CommanderInforId
        {
            get { return m_iCommanderInforId; }
            set { m_iCommanderInforId = value; }
        }

        /// <summary>
        /// ������Ա�ʺ�
        /// </summary>
        public string CommanderAccount
        {
            get { return m_strCommanderAccount; }
            set { m_strCommanderAccount = value; }
        }

        /// <summary>
        /// ������Ա����
        /// </summary>
        public string CommanderName
        {
            get { return m_strCommanderName; }
            set { m_strCommanderName = value; }
        }

        /// <summary>
        /// ������Ա����
        /// </summary>
        public string CommanderType
        {
            get { return m_strCommanderType; }
            set { m_strCommanderType = value; }
        }

        /// <summary>
        /// ������Ա������
        /// </summary>
        public string ThreeCode
        {
            get { return m_strThreeCode; }
            set { m_strThreeCode = value; }
        }
    }
}
