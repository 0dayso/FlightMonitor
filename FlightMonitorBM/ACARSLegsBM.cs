using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ACARS电报类型
    /// </summary>
    public enum MsgType : uint
    {
        OUT = 1,        //OUT报
        OFF = 2,        //OFF
        ON = 3,
        IN = 4,
        RTN = 5,
        POS = 6,        //位置报
        OTHER = 11,     //其他类型的报文：除OOOI和RTN之外
        NON = 12        //不明类型
    }

    /// <summary>
    /// ACARS航班动态实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSLegsBM
    {
        #region 对象内部变量
        
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
        /// 构造函数
        /// </summary>
        public ACARSLegsBM()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strACARSLegsInfo">航班那</param>
        /// <param name="strFormTime">报文形成日期</param>
        public ACARSLegsBM(string strACARSLegsInfo)
        {   
            //将报文分行进行处理
            string ACARSLegsInfo = strACARSLegsInfo.Replace("\r", "");
            string[] arrACARSLegsInfo = ACARSLegsInfo.Split('\n');

            //航班号 不带公司两字码
            int iStartSplitIndex = arrACARSLegsInfo[1].IndexOf("/");
            m_strFLTID = arrACARSLegsInfo[1].Substring(0, iStartSplitIndex);
            m_strFLTID = m_strFLTID.Replace("HU", "").Replace("GS", "").Replace("JD", "").Replace("8L", "").Replace("HX", "").Replace("Y8", "");
            
            //报文形成日期（UTC时间）
            int iEndSplitIndex = arrACARSLegsInfo[1].IndexOf(".", iStartSplitIndex);
            m_strDATOP = DateTime.UtcNow.ToString("yyyy-MM-") + arrACARSLegsInfo[1].Substring(iStartSplitIndex + 1, iEndSplitIndex - iStartSplitIndex - 1);

            //飞机注册号
            iStartSplitIndex = iEndSplitIndex + 1;
            iEndSplitIndex = arrACARSLegsInfo[1].IndexOf(".", iStartSplitIndex);
            m_strLONG_REG = arrACARSLegsInfo[1].Substring(iStartSplitIndex, iEndSplitIndex - iStartSplitIndex);

            //始发站三字码
            iStartSplitIndex = iEndSplitIndex + 1;
            m_strDEPSTN = arrACARSLegsInfo[1].Substring(iStartSplitIndex);

            //报文类型
            //OA：ON = 落地
            //AA：IN = 挡轮档
            //OD：OUT = 撤轮档
            //AD：OFF = 起飞
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
            //如果报文不全，没有以上关键字
            else
            {
                m_messageType = MsgType.NON;
            }

            //根据报文类型，获取相应的时间
            iStartSplitIndex = 2;
            iEndSplitIndex = arrACARSLegsInfo[2].IndexOf(" ");
            if (iEndSplitIndex > 2)
            {
                //获取两个时间
                string strTime = arrACARSLegsInfo[2].Substring(iStartSplitIndex, iEndSplitIndex - iStartSplitIndex);
                //根据“/”将两个时间分开
                string[] strArrTime = strTime.Split('/');
                //根据报文规则
                //如果是ON报，则取第一个时间
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
                    //如果是IN报，则取第二个时间
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
                    //如果是OUT报，则去第一个时间
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
                    //如果是OFF报，则取第二个时间
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

            //到达站
            //如果是OUT报或OFF报，则从EA时间之后取航站的三字码
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
                //如果是IN报或ON报，则从末尾取三字码
            else if (MessageType == MsgType.IN || MessageType == MsgType.ON)
            {
                if (iEndSplitIndex > 0)
                {
                    m_strARRSTN = arrACARSLegsInfo[2].Substring(iEndSplitIndex + 1).Trim();
                }
            }

            //将报文中的时间转换为本地时间
            //如果落地时间规则，则加上年月日后转换成本地时间
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
                //否则用年月日加上零点的时间
            else
            {
                m_strTDWN = m_strDATOP +  " 00:00:00";
            }

            //如果挡轮档时间规则
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

            //推出时间（撤轮档）
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

            //起飞时间
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
        /// 航班号
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// 报文形成日期
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }
      
        /// <summary>
        /// 飞机长注册号
        /// </summary>
        public string LONG_REG
        {
            get { return m_strLONG_REG; }
            set { m_strLONG_REG = value; }
        }

        /// <summary>
        /// 始发站三字码
        /// </summary>
        public string DEPSTN
        {
            get { return m_strDEPSTN; }
            set { m_strDEPSTN = value; }
        }

        /// <summary>
        /// 到达站三字码
        /// </summary>
        public string ARRSTN
        {
            get { return m_strARRSTN; }
            set { m_strARRSTN = value; }
        }

        /// <summary>
        /// 推出时间
        /// </summary>
        public string PushTime
        {
            get { return m_strPushTime; }
            set { m_strPushTime = value; }
        }

        /// <summary>
        /// 起飞时间
        /// </summary>
        public string TOFF
        {
            get { return m_strTOFF; }
            set { m_strTOFF = value; }
        }

        /// <summary>
        /// 落地时间
        /// </summary>
        public string TDWN
        {
            get { return m_strTDWN; }
            set { m_strTDWN = value; }
        }

        /// <summary>
        /// 挡抡挡时间
        /// </summary>
        public string ATA
        {
            get { return m_strATA; }
            set { m_strATA = value; }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MessageType
        {
            get { return m_messageType; }
            set { m_messageType = value; }
        }

        /// <summary>
        /// 消息时间
        /// </summary>
        public string MessageTime
        {
            get { return m_strMessageTime; }
            set { m_strMessageTime = value; }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent
        {
            get { return m_strMessageContent; }
            set { m_strMessageContent = value; }
        }
    }
}
