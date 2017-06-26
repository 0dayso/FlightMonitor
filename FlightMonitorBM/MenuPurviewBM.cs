using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �˵���Ȩ��ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class MenuPurviewBM
    {
        /// <summary>
        /// ����
        /// </summary>
        public enum DataType : uint
        {
            ITEM = 1,
            PURVIEW = 2
        }

        #region �����ڲ�����
        private int m_iMenuPurviewID;
        private int m_iMenuID;
        private string m_strMenuItemID;
        private string m_strMenuItemName;
        private int m_iMenuPurview;
        private string m_strUserID;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuPurviewBM()
        {
        }

        /// <summary>
        /// ���캯��:��һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">������</param>
        /// <param name="dataType">����</param>
        public MenuPurviewBM(DataRow dataRow, DataType dataType)
        {
            m_iMenuID = Convert.ToInt32(dataRow["cniMenuID"].ToString());
            m_strMenuItemID = dataRow["cniMenuID"].ToString();
            m_strMenuItemName = dataRow["cnvcMenuItemName"].ToString();

            if (dataType == DataType.PURVIEW)
            {
                m_iMenuPurview = Convert.ToInt32(dataRow["cniMenuPurviewID"].ToString());
                m_iMenuPurview = Convert.ToInt32(dataRow["cniMenuPurview"].ToString());
                m_strUserID = dataRow["cnvcUserID"].ToString();
            }
        }

        /// <summary>
        /// Ȩ�����
        /// </summary>
        public int MenuPurviewID
        {
            get { return m_iMenuPurviewID; }
            set { m_iMenuPurviewID = value; }
        }

        /// <summary>
        /// �˵����
        /// </summary>
        public int MenuID
        {
            get { return m_iMenuID; }
            set { m_iMenuID = value; }
        }

        /// <summary>
        /// �˵�����
        /// </summary>
        public string MenuItemID
        {
            get { return m_strMenuItemID; }
            set { m_strMenuItemID = value; }
        }

        /// <summary>
        /// �˵�������
        /// </summary>
        public string MenuItemName
        {
            get { return m_strMenuItemName; }
            set { m_strMenuItemName = value; }
        }

        /// <summary>
        /// �˵���Ȩ��
        /// </summary>
        public int MenuPurview
        {
            get { return m_iMenuPurview; }
            set { m_iMenuPurview = value; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public string UserID
        {
            get { return m_strUserID; }
            set { m_strUserID = value; }
        }

    }
}
