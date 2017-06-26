using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ACARS�籨����
    /// </summary>
    public enum MsgType : uint
    {
        OUT = 1,        //OUT��
        OFF = 2,        //OFF
        ON = 3,
        IN = 4,
        RTN = 5,
        POS = 6,        //λ�ñ�
        OTHER = 11,     //�������͵ı��ģ���OOOI��RTN֮��
        NON = 12        //��������
    }

    /// <summary>
    /// ACARS���ද̬ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-14
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSLegsBM
    {
        #region �����ڲ�����
        
        private string m_strFLTID;
        private string m_strDATOP;
        private string m_strLONG_REG;
        private string m_strDEPSTN;
        private string m_strARRSTN;
        private string m_strPushTime;
        private string m_strTOFF;
        private string m_strTDWN;
        private string m_strATA;
        private MsgType m_messageType;
        private string m_strMessageTime;
        private string m_strMessageContent;
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public ACARSLegsBM()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="strACARSLegsInfo">������</param>
        /// <param name="strFormTime">�����γ�����</param>
        public ACARSLegsBM(string strACARSLegsInfo)
        {   
            //�����ķ��н��д���
            string ACARSLegsInfo = strACARSLegsInfo.Replace("\r", "");
            string[] arrACARSLegsInfo = ACARSLegsInfo.Split('\n');

            //����� ������˾������
            int iStartSplitIndex = arrACARSLegsInfo[1].IndexOf("/");
            m_strFLTID = arrACARSLegsInfo[1].Substring(0, iStartSplitIndex);
            m_strFLTID = m_strFLTID.Replace("HU", "").Replace("GS", "").Replace("JD", "").Replace("8L", "").Replace("HX", "").Replace("Y8", "");
            
            //�����γ����ڣ�UTCʱ�䣩
            int iEndSplitIndex = arrACARSLegsInfo[1].IndexOf(".", iStartSplitIndex);
            m_strDATOP = DateTime.UtcNow.ToString("yyyy-MM-") + arrACARSLegsInfo[1].Substring(iStartSplitIndex + 1, iEndSplitIndex - iStartSplitIndex - 1);

            //�ɻ�ע���
            iStartSplitIndex = iEndSplitIndex + 1;
            iEndSplitIndex = arrACARSLegsInfo[1].IndexOf(".", iStartSplitIndex);
            m_strLONG_REG = arrACARSLegsInfo[1].Substring(iStartSplitIndex, iEndSplitIndex - iStartSplitIndex);

            //ʼ��վ������
            iStartSplitIndex = iEndSplitIndex + 1;
            m_strDEPSTN = arrACARSLegsInfo[1].Substring(iStartSplitIndex);

            //��������
            //OA��ON = ���
            //AA��IN = ���ֵ�
            //OD��OUT = ���ֵ�
            //AD��OFF = ���
            if (arrACARSLegsInfo[2].IndexOf("OA") == 0)
            {
                m_messageType = MsgType.ON;
            }
            else if (arrACARSLegsInfo[2].IndexOf("AA") == 0)
            {
                m_messageType = MsgType.IN;
            }
            else if (arrACARSLegsInfo[2].IndexOf("OD") == 0)
            {
                m_messageType = MsgType.OUT;
            }
            else if (arrACARSLegsInfo[2].IndexOf("AD") == 0)
            {
                m_messageType = MsgType.OFF;
            }
            //������Ĳ�ȫ��û�����Ϲؼ���
            else
            {
                m_messageType = MsgType.NON;
            }

            //���ݱ������ͣ���ȡ��Ӧ��ʱ��
            iStartSplitIndex = 2;
            iEndSplitIndex = arrACARSLegsInfo[2].IndexOf(" ");
            if (iEndSplitIndex > 2)
            {
                //��ȡ����ʱ��
                string strTime = arrACARSLegsInfo[2].Substring(iStartSplitIndex, iEndSplitIndex - iStartSplitIndex);
                //���ݡ�/��������ʱ��ֿ�
                string[] strArrTime = strTime.Split('/');
                //���ݱ��Ĺ���
                //�����ON������ȡ��һ��ʱ��
                if (m_messageType == MsgType.ON)
                {
                    m_strTDWN = strArrTime[0];
                    m_strATA = "";
                    m_strPushTime = "";
                    m_strTOFF = "";
                    if (m_strTDWN.Length != 4)
                    {
                        m_messageType = MsgType.NON;
                        return;
                    }
                }
                    //�����IN������ȡ�ڶ���ʱ��
                else if (m_messageType == MsgType.IN)
                {
                    m_strTDWN = "";
                    m_strATA = strArrTime[1];
                    m_strPushTime = "";
                    m_strTOFF = "";
                    if (m_strATA.Length != 4)
                    {
                        m_messageType = MsgType.NON;
                        return;
                    }
                }
                    //�����OUT������ȥ��һ��ʱ��
                else if (m_messageType == MsgType.OUT)
                {
                    m_strPushTime = strArrTime[0];
                    m_strTOFF = "";
                    m_strTDWN = "";
                    m_strATA = "";
                    if (m_strPushTime.Length != 4)
                    {
                        m_messageType = MsgType.NON;
                        return;
                    }
                }
                    //�����OFF������ȡ�ڶ���ʱ��
                else if (m_messageType == MsgType.OFF)
                {
                    m_strPushTime = "";
                    m_strTOFF = strArrTime[1];
                    m_strTDWN = "";
                    m_strATA = "";
                    if (m_strTOFF.Length != 4)
                    {
                        m_messageType = MsgType.NON;
                        return;
                    }
                }
            }

            //����վ
            //�����OUT����OFF�������EAʱ��֮��ȡ��վ��������
            if (MessageType == MsgType.OUT || MessageType == MsgType.OFF)
            {
                iStartSplitIndex = iEndSplitIndex;
                iEndSplitIndex = arrACARSLegsInfo[2].IndexOf("EA");
                iStartSplitIndex = iEndSplitIndex;
                iEndSplitIndex = arrACARSLegsInfo[2].IndexOf(" ");
                if (iEndSplitIndex > 0)
                {
                    m_strARRSTN = arrACARSLegsInfo[2].Substring(iEndSplitIndex + 3).Trim();
                }
            }
                //�����IN����ON�������ĩβȡ������
            else if (MessageType == MsgType.IN || MessageType == MsgType.ON)
            {
                if (iEndSplitIndex > 0)
                {
                    m_strARRSTN = arrACARSLegsInfo[2].Substring(iEndSplitIndex + 1).Trim();
                }
            }

            //�������е�ʱ��ת��Ϊ����ʱ��
            //������ʱ���������������պ�ת���ɱ���ʱ��
            if (m_strTDWN.Length == 4)
            {
                m_strTDWN = m_strDATOP + " " +  m_strTDWN.Substring(0, 2) + ":" + m_strTDWN.Substring(2, 2) + ":00";
                if (DateTime.Parse(m_strTDWN).ToLocalTime() > DateTime.Now.AddHours(1))
                {
                    m_strTDWN = DateTime.Parse(m_strTDWN).ToLocalTime().AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    m_strTDWN = DateTime.Parse(m_strTDWN).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
                //�����������ռ�������ʱ��
            else
            {
                m_strTDWN = m_strDATOP +  " 00:00:00";
            }

            //������ֵ�ʱ�����
            if (m_strATA.Length == 4)
            {
                m_strATA = m_strDATOP + " " + m_strATA.Substring(0, 2) + ":" + m_strATA.Substring(2, 2) + ":00";
                if (DateTime.Parse(m_strATA).ToLocalTime() > DateTime.Now.AddHours(1))
                {
                    m_strATA = DateTime.Parse(m_strATA).ToLocalTime().AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    m_strATA = DateTime.Parse(m_strATA).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            else
            {
                m_strATA = m_strDATOP + " 00:00:00";
            }

            //�Ƴ�ʱ�䣨���ֵ���
            if (m_strPushTime.Length == 4)
            {
                m_strPushTime = m_strDATOP + " " + m_strPushTime.Substring(0, 2) + ":" + m_strPushTime.Substring(2, 2) + ":00";
                if (DateTime.Parse(m_strPushTime).ToLocalTime() > DateTime.Now.AddHours(1))
                {
                    m_strPushTime = DateTime.Parse(m_strPushTime).ToLocalTime().AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    m_strPushTime = DateTime.Parse(m_strPushTime).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            else
            {
                m_strPushTime = m_strDATOP + " 00:00:00";
            }

            //���ʱ��
            if (m_strTOFF.Length == 4)
            {
                m_strTOFF = m_strDATOP + " " + m_strTOFF.Substring(0, 2) + ":" + m_strTOFF.Substring(2, 2) + ":00";
                if (DateTime.Parse(m_strTOFF).ToLocalTime() > DateTime.Now.AddHours(1))
                {
                    m_strTOFF = DateTime.Parse(m_strTOFF).ToLocalTime().AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    m_strTOFF = DateTime.Parse(m_strTOFF).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            else
            {
                m_strTOFF = m_strDATOP + " 00:00:00";
            }
        }

        

        /// <summary>
        /// �����
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// �����γ�����
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
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
        /// ʼ��վ������
        /// </summary>
        public string DEPSTN
        {
            get { return m_strDEPSTN; }
            set { m_strDEPSTN = value; }
        }

        /// <summary>
        /// ����վ������
        /// </summary>
        public string ARRSTN
        {
            get { return m_strARRSTN; }
            set { m_strARRSTN = value; }
        }

        /// <summary>
        /// �Ƴ�ʱ��
        /// </summary>
        public string PushTime
        {
            get { return m_strPushTime; }
            set { m_strPushTime = value; }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string TOFF
        {
            get { return m_strTOFF; }
            set { m_strTOFF = value; }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string TDWN
        {
            get { return m_strTDWN; }
            set { m_strTDWN = value; }
        }

        /// <summary>
        /// ���յ�ʱ��
        /// </summary>
        public string ATA
        {
            get { return m_strATA; }
            set { m_strATA = value; }
        }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public MsgType MessageType
        {
            get { return m_messageType; }
            set { m_messageType = value; }
        }

        /// <summary>
        /// ��Ϣʱ��
        /// </summary>
        public string MessageTime
        {
            get { return m_strMessageTime; }
            set { m_strMessageTime = value; }
        }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public string MessageContent
        {
            get { return m_strMessageContent; }
            set { m_strMessageContent = value; }
        }
    }
}
