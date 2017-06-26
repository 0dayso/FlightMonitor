using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ACARS报文实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：张 黎
    /// 创建日期：2008-02-25
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSMegsBM
    {
        PublicService.PublicService wsPublicService = new AirSoft.FlightMonitor.FlightMonitorBM.PublicService.PublicService();

        #region 对象内部变量
        private string m_strDATOP;          //航班日期（航班计划中该航班的日期）
        private string m_strFLTID;          //航班号
        private int m_iLegNo;                 //航段序号
        private string m_strAC;               //FOC机号
        private string m_strFLTID_Num;      //航班号去掉标识后的数字部分
        private string m_strFlightDate;     //航班日期（此日期为发送报文的日期，与DATOP区别开来，对于跨0点的航班，发送报文的日期可能与航班计划的航班日期不同）
        private string m_strLONG_REG;       //飞机号
        private string m_strDEPSTN;         //起飞机场
        private string m_strDEPSTN_LAT;     //起飞机场纬度
        private string m_strDEPSTN_LON;     //起飞机场经度
        private string m_strARRSTN;         //目的机场
        private string m_strARRSTN_LAT;     //目的机场纬度
        private string m_strARRSTN_LON;     //目的机场经度
        private string m_strEqipType;       //机载设备类型：Collins、Honeywell
        private string m_strOUT;            //OUT时间
        private string m_strOUT_FOB;        //OUT报中的FOB
        private string m_strOFF;            //OFF时间
        private string m_strOFF_FOB;        //OFF报中的FOB
        private string m_strETA;            //预达到达时间
        private string m_strON;             //ON时间
        private string m_strON_FOB;         //ON报中的FOB
        private string m_strIN;             //IN时间
        private string m_strIN_FOB;         //IN报中的FOB
        private string m_strRTN;            //Return时间
        private string m_strRTN_FOB;        //Return报中的FOB
        private string m_strLON;            //POSITION报中的经度
        private string m_strLAT;            //POSITION报中的纬度
        private string m_strMCH;            //POSITION报中的马赫数
        private string m_strCAS;            //POSITION报中的真空速
        private string m_strWD;             //POSITION报中的风向
        private string m_strWS;             //POSITION报中的风速
        private string m_strFL;             //POSITION报中的飞行高度
        private string m_strTTG;            //POSITION报中的TTG
        private string m_strPOS_FOB;        //POSITION报中的机上燃油
        private MsgType m_messageType;      //报文类型
        private string m_strMessageSendTime;//报文发送时间
        private string m_strMessageProcTime;//报文处理时间（即报文到达本地的时间）
        private string m_strMessageContent; //报文内容
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public ACARSMegsBM()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strACARSLegsInfo">航班那</param>
        /// <param name="strFormTime">报文形成日期</param>
        public ACARSMegsBM(string strACARSLegsInfo)
        {   
            //将报文分行进行处理
            string ACARSLegsInfo = strACARSLegsInfo.Replace("\r", "");
            string[] arrACARSLegsInfo = ACARSLegsInfo.Split('\n');

            int iStartIndex = 0;
            int iEndIndex = 0;

            //分析报头的基本信息
            #region 确定报文类型
            //获取标识机载设备和报文类型的关键字
            //首先获取报文第三行，标识机载设备的字段
            string strTypeKey = arrACARSLegsInfo[2].Trim().Substring(1);
            strTypeKey += " ";
            if (arrACARSLegsInfo.Length > 5)
            {
                if (arrACARSLegsInfo[5].ToString().Trim() != "")
                {
                    //然后获取第六行，标识报文类型的字段
                    strTypeKey += arrACARSLegsInfo[5].Trim().Replace(" ", "").Substring(1);

                    //确定报文类型
                    switch (strTypeKey.Trim())
                    {
                        case "A80 OUT":
                            m_strEqipType = "Collins";
                            m_messageType = MsgType.OUT;
                            break;
                        case "A80 OFF":
                            m_strEqipType = "Collins";
                            m_messageType = MsgType.OFF;
                            break;
                        case "A80 ON":
                            m_strEqipType = "Collins";
                            m_messageType = MsgType.ON;
                            break;
                        case "A80 IN":
                            m_strEqipType = "Collins";
                            m_messageType = MsgType.IN;
                            break;
                        case "A80 RTN":
                            m_strEqipType = "Collins";
                            m_messageType = MsgType.RTN;
                            break;
                        case "A80 POSITION":
                            m_strEqipType = "Collins";
                            m_messageType = MsgType.POS;
                            break;
                        case "DEP OUT":
                            m_strEqipType = "Honeywell";
                            m_messageType = MsgType.OUT;
                            break;
                        case "DEP OFF":
                            m_strEqipType = "Honeywell";
                            m_messageType = MsgType.OFF;
                            break;
                        case "ARR ON":
                            m_strEqipType = "Honeywell";
                            m_messageType = MsgType.ON;
                            break;
                        case "ARR IN":
                            m_strEqipType = "Honeywell";
                            m_messageType = MsgType.IN;
                            break;
                        case "RTN RTN":
                            m_strEqipType = "Honeywell";
                            m_messageType = MsgType.RTN;
                            break;
                        case "M14 POSITION":
                            m_strEqipType = "Honeywell";
                            m_messageType = MsgType.POS;
                            break;
                        default:
                            m_messageType = MsgType.OTHER;
                            m_strEqipType = "Error";
                            break;
                    }
                }
                else
                {
                    m_messageType = MsgType.OTHER;
                    m_strEqipType = "Error";
                }
            }
            else
            {
                m_messageType = MsgType.OTHER;
                m_strEqipType = "Error";
            }

            //如果报文不是以上目前需要处理的类型，则不继续进行解析
            if (m_messageType == MsgType.OTHER)
            {
                return;
            }
            #endregion

            #region 报文发送时间
            //报文发送时间
            iStartIndex = arrACARSLegsInfo[1].IndexOf(" ") + 1;
            m_strMessageSendTime = arrACARSLegsInfo[1].Substring(iStartIndex);
            m_strMessageSendTime = DateTime.UtcNow.ToString("yyyy-MM-") + m_strMessageSendTime.Substring(0, 2) + " " + m_strMessageSendTime.Substring(2, 2) + ":" + m_strMessageSendTime.Substring(4, 2) + ":00";
            
            //针对跨月的情况进行处理：报文发送和处理分别在前一个月的最后一天和后一个月的第二天
            //eg.报文发送时间为1月份的312359，处理时（DateTime.UtcNow）为2月份的010001，则易发生错误
            //m_strMessageSendTime=‘2008-02-31 23:59:00’，显然会造成错误
            //若发生这种情况，则向前推一个月
            try
            {
                m_strMessageSendTime = Convert.ToDateTime(m_strMessageSendTime).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                m_strMessageSendTime = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-") + m_strMessageSendTime.Substring(0, 2) + " " + m_strMessageSendTime.Substring(2, 2) + ":" + m_strMessageSendTime.Substring(4, 2) + ":00";
            }

            m_strDATOP = m_strMessageSendTime.Substring(0, 10);
            m_strFlightDate = m_strMessageSendTime.Substring(0, 10);
            #endregion

            #region 航班号
            //航班号：取第一个空格和第一个“/”之间的字段
            iStartIndex = arrACARSLegsInfo[3].IndexOf(" ") + 1;
            iEndIndex = arrACARSLegsInfo[3].IndexOf("/AN");
            m_strFLTID = arrACARSLegsInfo[3].Substring(iStartIndex, iEndIndex - iStartIndex).Replace(" ", "");
            //去掉航空公司标识
            m_strFLTID_Num = m_strFLTID.Replace("HU", "").Replace("GS", "").Replace("JD", "").Replace("8L", "").Replace("HX", "").Replace("Y8", "");
            #endregion

            #region 飞机号
            //飞机号：取第二个空格和“/”之间的字段
            iStartIndex = arrACARSLegsInfo[3].IndexOf(" ", iEndIndex) + 1;
            iEndIndex = arrACARSLegsInfo[3].IndexOf("/", iStartIndex);
            //如果飞机号后还有其他字段
            if (iEndIndex > 0)
            {
                m_strLONG_REG = arrACARSLegsInfo[3].Substring(iStartIndex, iEndIndex - iStartIndex);
            }
                //如果飞机号为这一行最后一个字段
            else if (iEndIndex < 0)
            {
                m_strLONG_REG = arrACARSLegsInfo[3].Substring(iStartIndex);
            }

            //去掉飞机号中的中划线
            m_strLONG_REG = m_strLONG_REG.Replace("-", "");
            #endregion

            //解析正文
            #region 位置报
            //位置报
            if (m_messageType == MsgType.POS)
            {
                //获取报文正文
                string strMegContent = "";
                if (arrACARSLegsInfo.Length >= 8)
                {
                    strMegContent = arrACARSLegsInfo[6].Replace("\r", "") + arrACARSLegsInfo[7].Replace("\r", "");
                }
                else
                {
                    strMegContent = arrACARSLegsInfo[6].Replace("\r", "");
                }

                //将报文正文的各个字段利用逗号“，”分开
                string[] strPosInfo = strMegContent.Split(',');

                //位置报有12个参数，如果参数完整，则逐个解析
                if (strPosInfo.Length == 12)
                {
                    //经纬度
                    //纬度
                    iStartIndex = strPosInfo[3].IndexOf(" ");
                    m_strLAT = strPosInfo[3].Substring(iStartIndex + 1).Replace(" ", "");
                    if (m_strLAT != "")
                    {
                        //南纬北纬
                        string strNorS = m_strLAT.Substring(0, 1);
                        m_strLAT = strNorS == "N" ? m_strLAT.Substring(1) : "-" + m_strLAT.Substring(1);

                        if (m_strLAT.Contains("M"))
                        {
                            m_strLAT = "";
                        }
                    }

                    //经度
                    iStartIndex = strPosInfo[4].IndexOf(" ");
                    m_strLON = strPosInfo[4].Substring(iStartIndex + 1).Replace(" ", "");
                    if (m_strLON != "")
                    {
                        //东经西经
                        string strEorW = m_strLON.Substring(0, 1);
                        //度数
                        m_strLON = strEorW == "E" ? m_strLON.Substring(1) : "-" + m_strLON.Substring(1);

                        if (m_strLON.Contains("M"))
                        {
                            m_strLON = "";
                        }
                    }

                    //马赫数
                    iStartIndex = strPosInfo[5].LastIndexOf(" ");
                    m_strMCH = strPosInfo[5].Substring(iStartIndex + 1);

                    //真空速
                    iStartIndex = strPosInfo[6].LastIndexOf(" ");
                    m_strCAS = strPosInfo[6].Substring(iStartIndex + 1);

                    //风向
                    iStartIndex = strPosInfo[7].LastIndexOf(" ");
                    m_strWD = strPosInfo[7].Substring(iStartIndex + 1);

                    //风速
                    iStartIndex = strPosInfo[8].LastIndexOf(" ");
                    m_strWS = strPosInfo[8].Substring(iStartIndex + 1);

                    //飞行高度
                    iStartIndex = strPosInfo[9].LastIndexOf(" ");
                    m_strFL = strPosInfo[9].Substring(iStartIndex + 1);

                    //剩余到达时间TTG
                    iStartIndex = strPosInfo[10].LastIndexOf(" ");
                    m_strTTG = strPosInfo[10].Substring(iStartIndex + 1);

                    //机上燃油FOB
                    iStartIndex = strPosInfo[11].LastIndexOf(" ");
                    m_strPOS_FOB = strPosInfo[11].Substring(iStartIndex + 1);
                    try
                    {
                        m_strPOS_FOB = Convert.ToInt16(m_strPOS_FOB).ToString();
                    }
                    catch
                    {
                        m_strPOS_FOB = "";
                    }
                }
                //如果参数不完整，则只取重要的几个：经纬度，飞行高度，FOB
                else
                {
                    //经纬度
                    //经度
                    iStartIndex = strMegContent.IndexOf("LON") + 4;
                    iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                    m_strLON = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex).Replace(" ", "");
                    if (m_strLON != "")
                    {
                        //东经西经
                        string strEorW = m_strLON.Substring(0, 1);
                        m_strLON = strEorW == "E" ? m_strLON.Substring(1) : "-" + m_strLON.Substring(1);

                        if (m_strLON.Contains("M"))
                        {
                            m_strLON = "";
                        }
                    }

                    //经度
                    iStartIndex = strMegContent.IndexOf("LAT") + 4;
                    iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                    m_strLAT = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex).Replace(" ", "");
                    if (m_strLAT.Length > 0)
                    {
                        //南纬北纬
                        string strNorS = m_strLAT.Substring(0, 1);
                        //度数
                        m_strLAT = strNorS == "N" ? m_strLAT.Substring(1) : "-" + m_strLAT.Substring(1);

                        if (m_strLAT.Contains("M"))
                        {
                            m_strLAT = "";
                        }
                    }

                    //飞行高度
                    iStartIndex = strMegContent.IndexOf("FL ") + 3;
                    iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                    //判断报文中是否有飞行高度数据
                    int iLength = iEndIndex - iStartIndex;
                    if (iLength > 0)
                    {
                        m_strFL = strMegContent.Substring(iStartIndex, iLength);
                    }
                    else
                    {
                        m_strFL = "";
                    }

                    //机上燃油FOB
                    iStartIndex = strMegContent.IndexOf("FOB");
                    m_strPOS_FOB = strMegContent.Substring(iStartIndex).Replace(" ", "");
                    try
                    {
                        m_strPOS_FOB = Convert.ToInt16(m_strPOS_FOB).ToString();
                    }
                    catch
                    {
                        m_strPOS_FOB = "";
                    }
                }
                
            }
            #endregion

            #region OOOI报
            //OOOI报
            else if (m_messageType == MsgType.OUT || m_messageType == MsgType.OFF || m_messageType == MsgType.ON || m_messageType == MsgType.IN || m_messageType == MsgType.RTN)
            {
                //将报文正文的各个字段利用逗号“，”分开
                string strMegContent = arrACARSLegsInfo[6];

                #region 起飞目的机场
                //起飞机场
                iStartIndex = strMegContent.IndexOf("DEP") + 4;
                iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                m_strDEPSTN = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                //目的机场
                iStartIndex = strMegContent.IndexOf("DES") + 4;
                iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                m_strARRSTN = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                //将机场四字码转成三字码
                DataTable dtAirportInfo = new DataTable();
                
                //起飞机场
                try
                {
                    dtAirportInfo = wsPublicService.GetAirportInfo(m_strDEPSTN, "", "", "", "", "").Tables[0];
                    if (dtAirportInfo.Rows.Count == 1)
                    {
                        m_strDEPSTN = dtAirportInfo.Rows[0]["AirportThreeCode"].ToString();

                        //获取机场经纬度
                        string[] strLocation = this.GetAirportLocation(m_strDEPSTN);
                        if (strLocation != null)
                        {
                            m_strDEPSTN_LAT = strLocation[0].ToString();
                            m_strDEPSTN_LON = strLocation[1].ToString();
                        }
                        else
                        {
                            m_strDEPSTN_LAT = "";
                            m_strDEPSTN_LON = "";
                        }
                    }
                    else
                    {
                        m_strDEPSTN = "";
                        m_strDEPSTN_LAT = "";
                        m_strDEPSTN_LON = "";
                    }
                }
                catch
                {
                    m_strDEPSTN = "";
                    m_strDEPSTN_LAT = "";
                    m_strDEPSTN_LON = "";
                }

                //目的机场
                try
                {
                    dtAirportInfo = wsPublicService.GetAirportInfo(m_strARRSTN, "", "", "", "", "").Tables[0];
                    if (dtAirportInfo.Rows.Count == 1)
                    {
                        m_strARRSTN = dtAirportInfo.Rows[0]["AirportThreeCode"].ToString();

                        //获取机场经纬度
                        string[] strLocation = this.GetAirportLocation(m_strARRSTN);
                        if (strLocation != null)
                        {
                            m_strARRSTN_LAT = strLocation[0].ToString();
                            m_strARRSTN_LON = strLocation[1].ToString();
                        }
                        else
                        {
                            m_strARRSTN_LAT = "";
                            m_strARRSTN_LON = "";
                        }
                    }
                    else
                    {
                        m_strARRSTN = "";
                        m_strARRSTN_LAT = "";
                        m_strARRSTN_LON = "";
                    }
                }
                catch
                {
                    m_strARRSTN = "";
                    m_strARRSTN_LAT = "";
                    m_strARRSTN_LON = "";
                }
                #endregion

                //根据报文类型获取其他关键字的值
                switch (m_messageType)
                {
                    #region OUT报的时间和FOB
                    //OUT报
                    case MsgType.OUT:
                        //OUT时间
                        iStartIndex = strMegContent.IndexOf("OUT") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strOUT = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //FOB
                        iStartIndex = strMegContent.IndexOf("FOB");
                        if (iStartIndex != -1)
                        {
                            iStartIndex = iStartIndex + 4;
                            m_strOUT_FOB = strMegContent.Substring(iStartIndex);
                            //FOB值如果不为空或不为“----”
                            if (m_strOUT_FOB != "" && m_strOUT_FOB != "----" && m_strOUT_FOB.Length == 4)
                            {
                                m_strOUT_FOB = Convert.ToInt16(m_strOUT_FOB).ToString();
                            }
                            else
                            {
                                m_strOUT_FOB = "";
                            }
                        }
                        else
                        {
                            m_strOUT_FOB = "";
                        }
                        break;
                    #endregion

                    #region OFF报的OFF时间、ETA时间和FOB
                    //OFF报
                    case MsgType.OFF:
                        //OFF时间
                        iStartIndex = strMegContent.IndexOf("OFF") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strOFF = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //ETA时间
                        iStartIndex = strMegContent.IndexOf("ETA") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strETA = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //如果ETA时间不完整
                        if (m_strETA.Length != 4)
                        {
                            m_strETA = "0000";
                        }

                        //FOB
                        iStartIndex = strMegContent.IndexOf("FOB");
                        if (iStartIndex != -1)
                        {
                            iStartIndex = iStartIndex + 4;
                            m_strOFF_FOB = strMegContent.Substring(iStartIndex);
                            if (m_strOFF_FOB != "" && m_strOFF_FOB != "----" && m_strOFF_FOB.Length == 4)
                            {
                                m_strOFF_FOB = Convert.ToInt16(m_strOFF_FOB).ToString();
                            }
                            else
                            {
                                m_strOFF_FOB = "";
                            }
                        }
                        else
                        {
                            m_strOFF_FOB = "";
                        }
                        break;
                    #endregion

                    #region ON报的ON时间和FOB
                    //ON报
                    case MsgType.ON:
                        //ON时间
                        iStartIndex = strMegContent.IndexOf("ON") + 3;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strON = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //FOB
                        iStartIndex = strMegContent.IndexOf("FOB");
                        if (iStartIndex != -1)
                        {
                            iStartIndex = iStartIndex + 4;
                            m_strON_FOB = strMegContent.Substring(iStartIndex);
                            if (m_strON_FOB != "" && m_strON_FOB != "----" && m_strON_FOB.Length == 4)
                            {
                                m_strON_FOB = Convert.ToInt16(m_strON_FOB).ToString();
                            }
                            else
                            {
                                m_strON_FOB = "";
                            }
                        }
                        else
                        {
                            m_strON_FOB = "";
                        }
                        break;
                    #endregion

                    #region IN报的IN时间和FOB
                    //IN报
                    case MsgType.IN:
                        //IN时间
                        iStartIndex = strMegContent.IndexOf("IN") + 3;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strIN = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex).Trim();

                        //FOB
                        iStartIndex = strMegContent.IndexOf("FOB");
                        if (iStartIndex != -1)
                        {
                            iStartIndex = iStartIndex + 4;
                            m_strIN_FOB = strMegContent.Substring(iStartIndex);
                            if (m_strIN_FOB != "" && m_strIN_FOB != "----" && m_strIN_FOB.Length == 4)
                            {
                                m_strIN_FOB = Convert.ToInt16(m_strIN_FOB).ToString();
                            }
                            else
                            {
                                m_strIN_FOB = "";
                            }
                        }
                        else
                        {
                            m_strIN_FOB = "";
                        }
                        break;
                    #endregion

                    #region RTN报的RTN时间、OUT时间和FOB
                    //RTN时间
                    case MsgType.RTN:
                        //RTN时间
                        iStartIndex = strMegContent.IndexOf("RTN") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strRTN = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //OUT时间
                        iStartIndex = strMegContent.IndexOf("OUT") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strOUT = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //FOB
                        iStartIndex = strMegContent.IndexOf("FOB");
                        if (iStartIndex != -1)
                        {
                            iStartIndex = iStartIndex + 4;
                            m_strRTN_FOB = strMegContent.Substring(iStartIndex);
                            if (m_strRTN_FOB != "" && m_strRTN_FOB != "----" && m_strRTN_FOB.Length == 4)
                            {
                                m_strRTN_FOB = Convert.ToInt16(m_strRTN_FOB).ToString();
                            }
                            else
                            {
                                m_strRTN_FOB = "";
                            }
                        }
                        else
                        {
                            m_strRTN_FOB = "";
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
            #endregion
        }

        #region 根据机场代码查询机场位置（经纬度）
        public string[] GetAirportLocation(string strAPCode)
        {
            string[] strLocation = new string[2];
            string strLat = "";
            string strLon = "";

            //获取机场位置信息
            DataSet dsAPLocation = new DataSet();
            dsAPLocation = wsPublicService.GetAirportLocation(strAPCode);

            if (dsAPLocation != null)
            {
                DataTable dtAPLocation = dsAPLocation.Tables[0];
                if (dtAPLocation.Rows.Count == 1)
                {
                    //纬度
                    string strNoS = dtAPLocation.Rows[0]["Latitude1"].ToString();           //标识南北纬
                    string strLatDegree = dtAPLocation.Rows[0]["Latitude2"].ToString();     //度数
                    string strLatMinute = dtAPLocation.Rows[0]["Latitude3"].ToString();     //分数

                    //将纬度转成以度为单位的数值
                    Double dLatDegree = Convert.ToDouble(strLatDegree);
                    Double dLatMinute = Convert.ToDouble(strLatMinute);
                    dLatMinute = dLatMinute / 60;
                    Double dLat = dLatDegree + dLatMinute;
                    strLat = Convert.ToString(dLat);

                    //取前十位数值
                    if (strLat.Length >= 10)
                    {
                        strLat = strLat.Substring(0, 10);
                    }

                    //如果是南纬，则在纬度前加“-”号
                    if (strNoS != "N")
                    {
                        strLat = "- " + strLat.Substring(0, 9);
                    }

                    //经度
                    string strEoW = dtAPLocation.Rows[0]["Longitude1"].ToString();          //标识东西经
                    string strLonDegree = dtAPLocation.Rows[0]["Longitude2"].ToString();    //度数
                    string strLonMinute = dtAPLocation.Rows[0]["Longitude3"].ToString();    //分数

                    //将纬度转成以度为单位的数值
                    Double dLonDegree = Convert.ToDouble(strLonDegree);
                    Double dLonMinute = Convert.ToDouble(strLonMinute);
                    dLonMinute = dLonMinute / 60;
                    Double dLon = dLonDegree + dLatMinute;
                    strLon = Convert.ToString(dLon);

                    //取前十位数值
                    if (strLon.Length >= 10)
                    {
                        strLon = strLon.Substring(0, 10);
                    }

                    //如果是西经，则在经度前加“-”号
                    if (strEoW != "E")
                    {
                        strLon = "-" + strLon.Substring(0, 9);
                    }

                    strLocation[0] = strLat;
                    strLocation[1] = strLon;

                    return strLocation;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 航班号
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// 航班计划日期
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }

        /// <summary>
        /// 航段序号
        /// </summary>
        public int LegNo
        {
            get { return m_iLegNo; }
            set { m_iLegNo = value; }
        }

        /// <summary>
        /// FOC机号
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// 报文发送日期
        /// 此日期为发送报文的日期，与DATOP区别开来
        /// 对于跨0点的航班，发送报文的日期可能与航班计划的航班日期不同
        /// </summary>
        public string FlightDate
        {
            get { return m_strFlightDate; }
            set { m_strFlightDate = value; }
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
        /// 始发站纬度
        /// </summary>
        public string DEPSTN_LAT
        {
            get { return m_strDEPSTN_LAT; }
            set { m_strDEPSTN_LAT = value; }
        }

        /// <summary>
        /// 始发站经度
        /// </summary>
        public string DEPSTN_LON
        {
            get { return m_strDEPSTN_LON; }
            set { m_strDEPSTN_LON = value; }
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
        /// 到达站三字码
        /// </summary>
        public string ARRSTN_LAT
        {
            get { return m_strARRSTN_LAT; }
            set { m_strARRSTN_LAT = value; }
        }

        /// <summary>
        /// 到达站三字码
        /// </summary>
        public string ARRSTN_LON
        {
            get { return m_strARRSTN_LON; }
            set { m_strARRSTN_LON = value; }
        }

        /// <summary>
        /// 机载设备类型
        /// </summary>
        public string EquipType
        {
            get { return m_strEqipType; }
            set { m_strEqipType = value; }
        }

        /// <summary>
        /// 推出时间
        /// </summary>
        public string ACARSOUT
        {
            get { return m_strOUT; }
            set { m_strOUT = value; }
        }

        /// <summary>
        /// OUT报中的FOB
        /// </summary>
        public string OUT_FOB
        {
            get { return m_strOUT_FOB; }
            set { m_strOUT_FOB = value; }
        }

        /// <summary>
        /// 起飞时间
        /// </summary>
        public string ACARSOFF
        {
            get { return m_strOFF; }
            set { m_strOFF = value; }
        }

        /// <summary>
        /// OFF报中ETA
        /// </summary>
        public string ACARSETA
        {
            get { return m_strETA; }
            set { m_strETA = value; }
        }

        /// <summary>
        /// OFF报中的FOB
        /// </summary>
        public string OFF_FOB
        {
            get { return m_strOFF_FOB; }
            set { m_strOFF_FOB = value; }
        }

        /// <summary>
        /// 落地时间
        /// </summary>
        public string ACARSON
        {
            get { return m_strON; }
            set { m_strON = value; }
        }

        /// <summary>
        /// ON报中的FOB
        /// </summary>
        public string ON_FOB
        {
            get { return m_strON_FOB; }
            set { m_strON_FOB = value; }
        }

        /// <summary>
        /// 挡抡挡时间
        /// </summary>
        public string ACARSIN
        {
            get { return m_strIN; }
            set { m_strIN = value; }
        }

        /// <summary>
        /// IN报中的FOB
        /// </summary>
        public string IN_FOB
        {
            get { return m_strIN_FOB; }
            set { m_strIN_FOB = value; }
        }

        /// <summary>
        /// 返航时间
        /// </summary>
        public string ACARSRTN
        {
            get { return m_strRTN; }
            set { m_strRTN = value; }
        }

        /// <summary>
        /// RTN报中FOB
        /// </summary>
        public string RTN_FOB
        {
            get { return m_strRTN_FOB; }
            set { m_strRTN_FOB = value; }
        }

        /// <summary>
        /// 位置报的经度
        /// </summary>
        public string POS_LON
        {
            get { return m_strLON; }
            set { m_strLON = value; }
        }

        /// <summary>
        /// 位置报的纬度
        /// </summary>
        public string POS_LAT
        {
            get { return m_strLAT; }
            set { m_strLAT = value; }
        }

        /// <summary>
        /// 位置报的马赫数
        /// </summary>
        public string POS_MCH
        {
            get { return m_strMCH; }
            set { m_strMCH = value; }
        }

        /// <summary>
        /// 位置报的真空速
        /// </summary>
        public string POS_CAS
        {
            get { return m_strCAS; }
            set { m_strCAS = value; }
        }

        /// <summary>
        /// 位置报的风向
        /// </summary>
        public string POS_WD
        {
            get { return m_strWD; }
            set { m_strWD = value; }
        }

        /// <summary>
        /// 位置报的风速
        /// </summary>
        public string POS_WS
        {
            get { return m_strWS; }
            set { m_strWS = value; }
        }

        /// <summary>
        /// 位置报的飞行高度
        /// </summary>
        public string POS_FL
        {
            get { return m_strFL; }
            set { m_strFL = value; }
        }

        /// <summary>
        /// 位置报的TTG
        /// </summary>
        public string POS_TTG
        {
            get { return m_strTTG; }
            set { m_strTTG = value; }
        }

        /// <summary>
        /// 位置报的FOB
        /// </summary>
        public string POS_FOB
        {
            get { return m_strPOS_FOB; }
            set { m_strPOS_FOB = value; }
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
        /// 报文发送时间（从报文原文中取）
        /// </summary>
        public string MessageSendTime
        {
            get { return m_strMessageSendTime; }
            set { m_strMessageSendTime = value; }
        }
        
        /// <summary>
        /// 报文处理时间（即报文到达本地磁盘的时间）
        /// </summary>
        public string MessageProcTime
        {
            get { return m_strMessageProcTime; }
            set { m_strMessageProcTime = value; }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent
        {
            get { return m_strMessageContent; }
            set { m_strMessageContent = value; }
        }
        #endregion
    }
}
