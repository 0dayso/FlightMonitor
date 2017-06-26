using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ���ݿ�ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class DataItemPurviewBM
    {
        /// <summary>
        /// ACARS�籨����
        /// </summary>
        public enum DataType : uint
        {
            ITEM = 1,
            PURVIEW = 2
        }

        #region �����ڲ�����
        private int m_iDataItemNO;
        private string m_strDataItemID;
        private string m_strDataItemName;
        private string m_strDataItemChangeName;
        private string m_strPrimaryCodeField;
        private string m_strPrimaryNameField;
        private string m_strForeignTable;
        private string m_strForeignCodeField;
        private string m_strForeignNameField;
        private int m_iFieldLength;
        private int m_iColumnWidth;
        private int m_iInOROut;
        private int m_iMaintenType;
        private int m_iFieldType;
        private int m_iDataItemPurview;
        private int m_iDataItemVisible;
        private int m_iViewIndex;
        private int m_iSplashPromptItem;
        private int m_iSoundPromptItem;
        private string m_strUserID;
        
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public DataItemPurviewBM()
        {
        }

        /// <summary>
        /// ���캯��:��һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">������</param>
        public DataItemPurviewBM(DataRow dataRow, DataType dataType)
        {
            m_iDataItemNO = Convert.ToInt32(dataRow["cniDataItemNo"].ToString());
            m_strDataItemID = dataRow["cnvcDataItemID"].ToString();
            m_strDataItemName = dataRow["cnvcDataItemName"].ToString();
            m_strDataItemChangeName = dataRow["cnvcDataItemChangeName"].ToString();
            m_strPrimaryCodeField = dataRow["cnvcPrimaryCodeField"].ToString();
            m_strPrimaryNameField = dataRow["cnvcPrimaryNameField"].ToString();
            m_strForeignTable = dataRow["cnvcForeignTable"].ToString();
            m_strForeignCodeField = dataRow["cnvcForeignCodeField"].ToString();
            m_strForeignNameField = dataRow["cnvcForeignNameField"].ToString();
            m_iFieldLength = Convert.ToInt32(dataRow["cniFieldLength"].ToString());
            m_iColumnWidth = Convert.ToInt32(dataRow["cniColumnWidth"].ToString());
            m_iInOROut = Convert.ToInt32(dataRow["cniInOROut"].ToString());
            m_iMaintenType = Convert.ToInt32(dataRow["cniMaintenType"].ToString());
            m_iFieldType = Convert.ToInt32(dataRow["cniFieldType"].ToString());

            if (dataType == DataType.PURVIEW)
            {
                m_iDataItemPurview = Convert.ToInt32(dataRow["cniDataItemPurview"].ToString());
                m_iDataItemVisible = Convert.ToInt32(dataRow["cniDataItemVisible"].ToString());
                m_iViewIndex = Convert.ToInt32(dataRow["cniViewIndex"].ToString());
                m_iSplashPromptItem = Convert.ToInt32(dataRow["cniSplashPromptItem"].ToString());
                m_iSoundPromptItem = Convert.ToInt32(dataRow["cniSoundPromptItem"].ToString());
                m_strUserID = dataRow["cnvcUserID"].ToString();
            }
           
        }    
        
       

        /// <summary>
        /// ���������
        /// </summary>
        public int DataItemNO
        {
            get { return m_iDataItemNO; }
            set { m_iDataItemNO = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string DataItemID
        {
            get { return m_strDataItemID; }
            set { m_strDataItemID = value; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string DataItemName
        {
            get { return m_strDataItemName; }
            set { m_strDataItemName = value; }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public string DataItemChangeName
        {
            get { return m_strDataItemChangeName; }
            set { m_strDataItemChangeName = value; }
        }       

       
        /// <summary>
        /// ��������ֶ�
        /// </summary>
        public string PrimaryCodeField
        {
            get { return m_strPrimaryCodeField; }
            set { m_strPrimaryCodeField = value; }
        }

       
        /// <summary>
        /// ���������ֶ�
        /// </summary>
        public string PrimaryNameField
        {
            get { return m_strPrimaryNameField; }
            set { m_strPrimaryNameField = value; }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string ForeignTable
        {
            get { return m_strForeignTable; }
            set { m_strForeignTable = value; }
        }


        /// <summary>
        /// ���������ֶ�
        /// </summary>
        public string ForeignCodeField
        {
            get { return m_strForeignCodeField; }
            set { m_strForeignCodeField = value; }
        }


        /// <summary>
        /// ����������ֶ�
        /// </summary>
        public string ForeignNameField
        {
            get { return m_strForeignNameField; }
            set { m_strForeignNameField = value; }
        }
       
        /// <summary>
        /// �ֶγ���
        /// </summary>
        public int FieldLength
        {
            get { return m_iFieldLength; }
            set { m_iFieldLength = value; }
        }

        /// <summary>
        /// ��Ԫ����
        /// </summary>
        public int ColumnWidth
        {
            get { return m_iColumnWidth; }
            set { m_iColumnWidth = value; }
        }

        /// <summary>
        /// ���ۻ��ǳ��ۺ���
        /// </summary>
        public int InOROut
        {
            get { return m_iInOROut; }
            set { m_iInOROut = value; }
        }

        /// <summary>
        /// ά����������
        /// </summary>
        public int MaintenType
        {
            get { return m_iMaintenType; }
            set { m_iMaintenType = value; }
        }

        /// <summary>
        /// �ֶ����� 1���ı� 2������
        /// </summary>
        public int FieldType
        {
            get { return m_iFieldType; }
            set { m_iFieldType = value; }
        }

        //private int m_iDataItemPurview;
        //private int m_iDataItemVisible;
        //private int m_iViewIndex;
        //private int m_iSplashPromptItem;
        //private int m_iSoundPromptItem;
        //private string m_strUserID;

        /// <summary>
        /// Ȩ�� 0:��;1:���;2:ά��
        /// </summary>
        public int DataItemPurview
        {
            get { return m_iDataItemPurview; }
            set { m_iDataItemPurview = value; }
        }

        /// <summary>
        /// �������Ƿ�ɼ�
        /// </summary>
        public int DataItemVisible
        {
            get { return m_iDataItemVisible; }
            set { m_iDataItemVisible = value; }
        }
        
        /// <summary>
        /// ��������ʾ˳��
        /// </summary>
        public int ViewIndex
        {
            get { return m_iViewIndex; }
            set { m_iViewIndex = value; }
        }

        /// <summary>
        /// �Ƿ���˸��ʾ
        /// </summary>
        public int SplashPromptItem
        {
            get { return m_iSplashPromptItem; }
            set { m_iSplashPromptItem = value; }
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        public int SoundPromptItem
        {
            get { return m_iSoundPromptItem; }
            set { m_iSoundPromptItem = value; }
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
