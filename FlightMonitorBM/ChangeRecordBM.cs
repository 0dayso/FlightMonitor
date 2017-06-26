using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �������ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-02
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ChangeRecordBM
    {
        #region �����ڲ�����
        private int m_iRecordNo;
        private string m_strUserID;
        private string m_strOldFLTID;
        private string m_strOldDATOP;
        private int m_iOldLegNo;
        private string m_strOldAC;
        private string m_strNewFLTID;
        private string m_strNewDATOP;
        private int m_iNewLegNo;
        private string m_strNewAC;
        private string m_strOldDepSTN;
        private string m_strOldDepSTNName;
        private string m_strOldArrSTN;
        private string m_strOldArrSTNName;
        private string m_strNewDepSTN;
        private string m_strNewDepSTNName;
        private string m_strNewArrSTN;
        private string m_strNewArrSTNName;
        private string m_strSTD;
        private string m_strETD;
        private string m_strSTA;
        private string m_strETA;
        private string m_strChangeReasonCode;
        private string m_strFieldName;
        private string m_strChangReasonName;
        private string m_strChangeOldContent;
        private string m_strChangeNewContent;
        private string m_strFOCOperatingTime;
        private string m_strLocalOperatingTime;
        private string m_strActionTag;
        private int m_iRefresh;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public ChangeRecordBM()
        {
        }
        

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dataRow">������</param>
        public ChangeRecordBM(DataRow dataRow)
        {
            m_iRecordNo = Convert.ToInt32(dataRow["cniRecordNo"].ToString());
            m_strUserID = dataRow["cnvcUserID"].ToString();
            m_strOldFLTID = dataRow["cnvcOldFLTID"].ToString();
            m_strOldDATOP = dataRow["cncOldDATOP"].ToString();
            m_iOldLegNo = Convert.ToInt32(dataRow["cniOldLegNo"].ToString());
            m_strOldAC = dataRow["cncOldAC"].ToString();
            m_strNewFLTID = dataRow["cnvcNewFLTID"].ToString();
            m_strNewDATOP = dataRow["cncNewDATOP"].ToString();
            m_iNewLegNo = Convert.ToInt32(dataRow["cniNewLegNo"].ToString());
            m_strNewAC = dataRow["cncNewAC"].ToString();
            m_strOldDepSTN = dataRow["cncOldDepSTN"].ToString();
            m_strOldDepSTNName = dataRow["cncOldDepSTNName"].ToString();
            m_strOldArrSTN = dataRow["cncOldArrSTN"].ToString();
            m_strOldArrSTNName = dataRow["cncOldArrSTNName"].ToString();
            m_strNewDepSTN = dataRow["cncNewDepSTN"].ToString();
            m_strNewDepSTNName = dataRow["cncNewDepSTNName"].ToString();
            m_strNewArrSTN = dataRow["cncNewArrSTN"].ToString();
            m_strNewArrSTNName = dataRow["cncNewArrSTNName"].ToString();
            m_strSTD = dataRow["cncSTD"].ToString();
            m_strETD = dataRow["cncETD"].ToString();
            m_strSTA = dataRow["cncSTA"].ToString();
            m_strETA = dataRow["cncETA"].ToString();
            m_strChangeReasonCode = dataRow["cnvcChangReasonCode"].ToString();
            m_strFieldName = dataRow["cnvcFieldName"].ToString();
            m_strChangReasonName = dataRow["cnvcChangReasonName"].ToString();
            m_strChangeOldContent = dataRow["cnvcChangeOldContent"].ToString();
            m_strChangeNewContent = dataRow["cnvcChangeNewContent"].ToString();
            m_strFOCOperatingTime = dataRow["cncFOCOperatingTime"].ToString();
            m_strLocalOperatingTime = dataRow["cncLocalOperatingTime"].ToString();
            m_strActionTag = dataRow["cncActionTag"].ToString();
            m_iRefresh = Convert.ToInt32(dataRow["cniRefresh"].ToString());
        }


        /// <summary>
        /// ����������
        /// </summary>
        public int RecordNo
        {
            get { return m_iRecordNo; }
            set { m_iRecordNo = value; }
        }

        /// <summary>
        /// �û����
        /// </summary>
        public string UserID
        {
            get { return m_strUserID; }
            set { m_strUserID = value; }
        }

        /// <summary>
        /// ���ǰ����ţ���ƣϣ�ϵͳ��ͬ��
        /// </summary>
        public string OldFLTID
        {
            get { return m_strOldFLTID; }
            set { m_strOldFLTID = value; }
        }

        /// <summary>
        /// ���ǰ�������ڣ���ƣϣ�ϵͳ��ͬ��
        /// </summary>
        public string OldDATOP
        {
            get { return m_strOldDATOP; }
            set { m_strOldDATOP = value; }
        }

        /// <summary>
        /// ���ǰ�������
        /// </summary>
        public int OldLegNo
        {
            get { return m_iOldLegNo; }
            set { m_iOldLegNo = value; }
        }

        /// <summary>
        /// ���ǰ����
        /// </summary>
        public string OldAC
        {
            get { return m_strOldAC; }
            set { m_strOldAC = value; }
        }       

        /// <summary>
        /// ����󺽰��
        /// </summary>
        public string NewFLTID
        {
            get { return m_strNewFLTID; }
            set { m_strNewFLTID = value; }
        }

        /// <summary>
        /// ����󺽰�����
        /// </summary>
        public string NewDATOP
        {
            get { return m_strNewDATOP; }
            set { m_strNewDATOP = value; }
        }

        /// <summary>
        /// ����󺽶����
        /// </summary>
        public int NewLegNo
        {
            get { return m_iNewLegNo; }
            set { m_iNewLegNo = value; }
        }

       
        
        /// <summary>
        /// �����AC
        /// </summary>
        public string NewAC
        {
            get { return m_strNewAC; }
            set { m_strNewAC = value; }
        }

        /// <summary>
        /// ���ǰʼ��վ������
        /// </summary>
        public string OldDepSTN
        {
            get { return m_strOldDepSTN; }
            set { m_strOldDepSTN = value; }
        }

        /// <summary>
        /// ���ǰʼ��վ������
        /// </summary>
        public string OldDepSTNName
        {
            get { return m_strOldDepSTNName; }
            set { m_strOldDepSTNName = value; }
        }

        /// <summary>
        /// ���ǰ����վ������
        /// </summary>
        public string OldArrSTN
        {
            get { return m_strOldArrSTN; }
            set { m_strOldArrSTN = value; }
        }

        /// <summary>
        /// ���ǰ����վ������
        /// </summary>
        public string OldArrSTNName
        {
            get { return m_strOldArrSTNName; }
            set { m_strOldArrSTNName = value; }
        }
        

        /// <summary>
        /// �����ʼ��վ������
        /// </summary>
        public string NewDepSTN
        {
            get { return m_strNewDepSTN; }
            set { m_strNewDepSTN = value; }
        }

        /// <summary>
        /// �����ʼ��վ������
        /// </summary>
        public string NewDepSTNName
        {
            get { return m_strNewDepSTNName; }
            set { m_strNewDepSTNName = value; }
        }

        /// <summary>
        /// ����󵽴�վ������
        /// </summary>
        public string NewArrSTN
        {
            get { return m_strNewArrSTN; }
            set { m_strNewArrSTN = value; }
        }

        /// <summary>
        /// ����󵽴�վ������
        /// </summary>
        public string NewArrSTNName
        {
            get { return m_strNewArrSTNName; }
            set { m_strNewArrSTNName = value; }
        }

       
        /// <summary>
        /// �ƻ����ʱ�䣨����󣬴���Ӧ���䣩
        /// </summary>
        public string STD
        {
            get { return m_strSTD; }
            set { m_strSTD = value; }
        }

        /// <summary>
        /// Ԥ�����ʱ�䣨�����
        /// </summary>
        public string ETD
        {
            get { return m_strETD; }
            set { m_strETD = value; }
        }

        /// <summary>
        /// �ƻ�����ʱ�䣨����󣬴���Ӧ���䣩
        /// </summary>
        public string STA
        {
            get { return m_strSTA; }
            set { m_strSTA = value; }
        }

        /// <summary>
        /// Ԥ�Ƶ���ʱ�䣨�����
        /// </summary>
        public string ETA
        {
            get { return m_strETA; }
            set { m_strETA = value; }
        }
       
        /// <summary>
        /// ���ԭ�����
        /// </summary>
        public string ChangeReasonCode
        {
            get { return m_strChangeReasonCode; }
            set { m_strChangeReasonCode = value; }
        }

        /// <summary>
        /// ��Ӧ�ֶ���
        /// </summary>
        public string FieldName
        {
            get { return m_strFieldName; }
            set { m_strFieldName = value; }
        }


        /// <summary>
        /// ���ԭ������
        /// </summary>
        public string ChangReasonName
        {
            get { return m_strChangReasonName; }
            set { m_strChangReasonName = value; }
        }


        /// <summary>
        /// ���ǰ����
        /// </summary>
        public string ChangeOldContent
        {
            get { return m_strChangeOldContent; }
            set { m_strChangeOldContent = value; }
        }


        /// <summary>
        /// ���������
        /// </summary>
        public string ChangeNewContent
        {
            get { return m_strChangeNewContent; }
            set { m_strChangeNewContent = value; }
        }

        /// <summary>
        /// FOC���ʱ��
        /// </summary>
        public string FOCOperatingTime
        {
            get { return m_strFOCOperatingTime; }
            set { m_strFOCOperatingTime = value; }
        }

        /// <summary>
        /// ��վ����ϵͳ����ʱ��
        /// </summary>
        public string LocalOperatingTime
        {
            get { return m_strLocalOperatingTime; }
            set { m_strLocalOperatingTime = value; }
        }

        /// <summary>
        /// ���������ʶ
        /// </summary>
        public string ActionTag
        {
            get { return m_strActionTag; }
            set { m_strActionTag = value; }
        }

        /// <summary>
        /// ���������ʶ
        /// </summary>
        public int Refresh
        {
            get { return m_iRefresh; }
            set { m_iRefresh = value; }
        }


        

    }
}
