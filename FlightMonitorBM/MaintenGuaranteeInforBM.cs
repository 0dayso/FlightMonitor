using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ������ά��ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class MaintenGuaranteeInforBM
    {
        #region �����ڲ�����
        private string m_strDATOP;
        private string m_strFLTID;
        private string m_strLEGNO;
        private string m_strAC;
        private string m_strFieldName;
        private int m_iFieldLength;
        private string m_strColumnCaption;
        private string m_strOldContent;
        private string m_strNewContent;
        private string m_strNewText;
        private int m_iFieldType;
        #endregion
        /// <summary>
        /// ���캯��
        /// </summary>
        public MaintenGuaranteeInforBM()
        {
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
        /// ��������
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string LEGNO
        {
            get { return m_strLEGNO; }
            set { m_strLEGNO = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// �����ֶ���
        /// </summary>
        public string FieldName
        {
            get { return m_strFieldName; }
            set { m_strFieldName = value; }
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
        /// �б���
        /// </summary>
        public string ColumnCaption
        {
            get { return m_strColumnCaption; }
            set { m_strColumnCaption = value; }
        }


        /// <summary>
        /// ��ֵ
        /// </summary>
        public string OldContent
        {
            get { return m_strOldContent; }
            set { m_strOldContent = value; }
        }

        /// <summary>
        /// ��ֵ
        /// </summary>
        public string NewContent
        {
            get { return m_strNewContent; }
            set { m_strNewContent = value; }
        }

        public string NewText
        {
            get { return m_strNewText; }
            set { m_strNewText = value; }
        }

        /// <summary>
        /// �ֶ����� 1���ı� 2������
        /// </summary>
        public int FieldType
        {
            get { return m_iFieldType; }
            set { m_iFieldType = value; }
        }

    }
}
