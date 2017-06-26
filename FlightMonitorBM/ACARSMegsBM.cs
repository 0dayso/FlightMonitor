using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ACARS����ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ��� ��
    /// �������ڣ�2008-02-25
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSMegsBM
    {
        PublicService.PublicService wsPublicService = new AirSoft.FlightMonitor.FlightMonitorBM.PublicService.PublicService();

        #region �����ڲ�����
        private string m_strDATOP;          //�������ڣ�����ƻ��иú�������ڣ�
        private string m_strFLTID;          //�����
        private int m_iLegNo;                 //�������
        private string m_strAC;               //FOC����
        private string m_strFLTID_Num;      //�����ȥ����ʶ������ֲ���
        private string m_strFlightDate;     //�������ڣ�������Ϊ���ͱ��ĵ����ڣ���DATOP�����������ڿ�0��ĺ��࣬���ͱ��ĵ����ڿ����뺽��ƻ��ĺ������ڲ�ͬ��
        private string m_strLONG_REG;       //�ɻ���
        private string m_strDEPSTN;         //��ɻ���
        private string m_strDEPSTN_LAT;     //��ɻ���γ��
        private string m_strDEPSTN_LON;     //��ɻ�������
        private string m_strARRSTN;         //Ŀ�Ļ���
        private string m_strARRSTN_LAT;     //Ŀ�Ļ���γ��
        private string m_strARRSTN_LON;     //Ŀ�Ļ�������
        private string m_strEqipType;       //�����豸���ͣ�Collins��Honeywell
        private string m_strOUT;            //OUTʱ��
        private string m_strOUT_FOB;        //OUT���е�FOB
        private string m_strOFF;            //OFFʱ��
        private string m_strOFF_FOB;        //OFF���е�FOB
        private string m_strETA;            //Ԥ�ﵽ��ʱ��
        private string m_strON;             //ONʱ��
        private string m_strON_FOB;         //ON���е�FOB
        private string m_strIN;             //INʱ��
        private string m_strIN_FOB;         //IN���е�FOB
        private string m_strRTN;            //Returnʱ��
        private string m_strRTN_FOB;        //Return���е�FOB
        private string m_strLON;            //POSITION���еľ���
        private string m_strLAT;            //POSITION���е�γ��
        private string m_strMCH;            //POSITION���е������
        private string m_strCAS;            //POSITION���е������
        private string m_strWD;             //POSITION���еķ���
        private string m_strWS;             //POSITION���еķ���
        private string m_strFL;             //POSITION���еķ��и߶�
        private string m_strTTG;            //POSITION���е�TTG
        private string m_strPOS_FOB;        //POSITION���еĻ���ȼ��
        private MsgType m_messageType;      //��������
        private string m_strMessageSendTime;//���ķ���ʱ��
        private string m_strMessageProcTime;//���Ĵ���ʱ�䣨�����ĵ��ﱾ�ص�ʱ�䣩
        private string m_strMessageContent; //��������
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public ACARSMegsBM()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="strACARSLegsInfo">������</param>
        /// <param name="strFormTime">�����γ�����</param>
        public ACARSMegsBM(string strACARSLegsInfo)
        {   
            //�����ķ��н��д���
            string ACARSLegsInfo = strACARSLegsInfo.Replace("\r", "");
            string[] arrACARSLegsInfo = ACARSLegsInfo.Split('\n');

            int iStartIndex = 0;
            int iEndIndex = 0;

            //������ͷ�Ļ�����Ϣ
            #region ȷ����������
            //��ȡ��ʶ�����豸�ͱ������͵Ĺؼ���
            //���Ȼ�ȡ���ĵ����У���ʶ�����豸���ֶ�
            string strTypeKey = arrACARSLegsInfo[2].Trim().Substring(1);
            strTypeKey += " ";
            if (arrACARSLegsInfo.Length > 5)
            {
                if (arrACARSLegsInfo[5].ToString().Trim() != "")
                {
                    //Ȼ���ȡ�����У���ʶ�������͵��ֶ�
                    strTypeKey += arrACARSLegsInfo[5].Trim().Replace(" ", "").Substring(1);

                    //ȷ����������
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

            //������Ĳ�������Ŀǰ��Ҫ��������ͣ��򲻼������н���
            if (m_messageType == MsgType.OTHER)
            {
                return;
            }
            #endregion

            #region ���ķ���ʱ��
            //���ķ���ʱ��
            iStartIndex = arrACARSLegsInfo[1].IndexOf(" ") + 1;
            m_strMessageSendTime = arrACARSLegsInfo[1].Substring(iStartIndex);
            m_strMessageSendTime = DateTime.UtcNow.ToString("yyyy-MM-") + m_strMessageSendTime.Substring(0, 2) + " " + m_strMessageSendTime.Substring(2, 2) + ":" + m_strMessageSendTime.Substring(4, 2) + ":00";
            
            //��Կ��µ�������д������ķ��ͺʹ���ֱ���ǰһ���µ����һ��ͺ�һ���µĵڶ���
            //eg.���ķ���ʱ��Ϊ1�·ݵ�312359������ʱ��DateTime.UtcNow��Ϊ2�·ݵ�010001�����׷�������
            //m_strMessageSendTime=��2008-02-31 23:59:00������Ȼ����ɴ���
            //�������������������ǰ��һ����
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

            #region �����
            //����ţ�ȡ��һ���ո�͵�һ����/��֮����ֶ�
            iStartIndex = arrACARSLegsInfo[3].IndexOf(" ") + 1;
            iEndIndex = arrACARSLegsInfo[3].IndexOf("/AN");
            m_strFLTID = arrACARSLegsInfo[3].Substring(iStartIndex, iEndIndex - iStartIndex).Replace(" ", "");
            //ȥ�����չ�˾��ʶ
            m_strFLTID_Num = m_strFLTID.Replace("HU", "").Replace("GS", "").Replace("JD", "").Replace("8L", "").Replace("HX", "").Replace("Y8", "");
            #endregion

            #region �ɻ���
            //�ɻ��ţ�ȡ�ڶ����ո�͡�/��֮����ֶ�
            iStartIndex = arrACARSLegsInfo[3].IndexOf(" ", iEndIndex) + 1;
            iEndIndex = arrACARSLegsInfo[3].IndexOf("/", iStartIndex);
            //����ɻ��ź��������ֶ�
            if (iEndIndex > 0)
            {
                m_strLONG_REG = arrACARSLegsInfo[3].Substring(iStartIndex, iEndIndex - iStartIndex);
            }
                //����ɻ���Ϊ��һ�����һ���ֶ�
            else if (iEndIndex < 0)
            {
                m_strLONG_REG = arrACARSLegsInfo[3].Substring(iStartIndex);
            }

            //ȥ���ɻ����е��л���
            m_strLONG_REG = m_strLONG_REG.Replace("-", "");
            #endregion

            //��������
            #region λ�ñ�
            //λ�ñ�
            if (m_messageType == MsgType.POS)
            {
                //��ȡ��������
                string strMegContent = "";
                if (arrACARSLegsInfo.Length >= 8)
                {
                    strMegContent = arrACARSLegsInfo[6].Replace("\r", "") + arrACARSLegsInfo[7].Replace("\r", "");
                }
                else
                {
                    strMegContent = arrACARSLegsInfo[6].Replace("\r", "");
                }

                //���������ĵĸ����ֶ����ö��š������ֿ�
                string[] strPosInfo = strMegContent.Split(',');

                //λ�ñ���12������������������������������
                if (strPosInfo.Length == 12)
                {
                    //��γ��
                    //γ��
                    iStartIndex = strPosInfo[3].IndexOf(" ");
                    m_strLAT = strPosInfo[3].Substring(iStartIndex + 1).Replace(" ", "");
                    if (m_strLAT != "")
                    {
                        //��γ��γ
                        string strNorS = m_strLAT.Substring(0, 1);
                        m_strLAT = strNorS == "N" ? m_strLAT.Substring(1) : "-" + m_strLAT.Substring(1);

                        if (m_strLAT.Contains("M"))
                        {
                            m_strLAT = "";
                        }
                    }

                    //����
                    iStartIndex = strPosInfo[4].IndexOf(" ");
                    m_strLON = strPosInfo[4].Substring(iStartIndex + 1).Replace(" ", "");
                    if (m_strLON != "")
                    {
                        //��������
                        string strEorW = m_strLON.Substring(0, 1);
                        //����
                        m_strLON = strEorW == "E" ? m_strLON.Substring(1) : "-" + m_strLON.Substring(1);

                        if (m_strLON.Contains("M"))
                        {
                            m_strLON = "";
                        }
                    }

                    //�����
                    iStartIndex = strPosInfo[5].LastIndexOf(" ");
                    m_strMCH = strPosInfo[5].Substring(iStartIndex + 1);

                    //�����
                    iStartIndex = strPosInfo[6].LastIndexOf(" ");
                    m_strCAS = strPosInfo[6].Substring(iStartIndex + 1);

                    //����
                    iStartIndex = strPosInfo[7].LastIndexOf(" ");
                    m_strWD = strPosInfo[7].Substring(iStartIndex + 1);

                    //����
                    iStartIndex = strPosInfo[8].LastIndexOf(" ");
                    m_strWS = strPosInfo[8].Substring(iStartIndex + 1);

                    //���и߶�
                    iStartIndex = strPosInfo[9].LastIndexOf(" ");
                    m_strFL = strPosInfo[9].Substring(iStartIndex + 1);

                    //ʣ�ൽ��ʱ��TTG
                    iStartIndex = strPosInfo[10].LastIndexOf(" ");
                    m_strTTG = strPosInfo[10].Substring(iStartIndex + 1);

                    //����ȼ��FOB
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
                //�����������������ֻȡ��Ҫ�ļ�������γ�ȣ����и߶ȣ�FOB
                else
                {
                    //��γ��
                    //����
                    iStartIndex = strMegContent.IndexOf("LON") + 4;
                    iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                    m_strLON = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex).Replace(" ", "");
                    if (m_strLON != "")
                    {
                        //��������
                        string strEorW = m_strLON.Substring(0, 1);
                        m_strLON = strEorW == "E" ? m_strLON.Substring(1) : "-" + m_strLON.Substring(1);

                        if (m_strLON.Contains("M"))
                        {
                            m_strLON = "";
                        }
                    }

                    //����
                    iStartIndex = strMegContent.IndexOf("LAT") + 4;
                    iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                    m_strLAT = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex).Replace(" ", "");
                    if (m_strLAT.Length > 0)
                    {
                        //��γ��γ
                        string strNorS = m_strLAT.Substring(0, 1);
                        //����
                        m_strLAT = strNorS == "N" ? m_strLAT.Substring(1) : "-" + m_strLAT.Substring(1);

                        if (m_strLAT.Contains("M"))
                        {
                            m_strLAT = "";
                        }
                    }

                    //���и߶�
                    iStartIndex = strMegContent.IndexOf("FL ") + 3;
                    iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                    //�жϱ������Ƿ��з��и߶�����
                    int iLength = iEndIndex - iStartIndex;
                    if (iLength > 0)
                    {
                        m_strFL = strMegContent.Substring(iStartIndex, iLength);
                    }
                    else
                    {
                        m_strFL = "";
                    }

                    //����ȼ��FOB
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

            #region OOOI��
            //OOOI��
            else if (m_messageType == MsgType.OUT || m_messageType == MsgType.OFF || m_messageType == MsgType.ON || m_messageType == MsgType.IN || m_messageType == MsgType.RTN)
            {
                //���������ĵĸ����ֶ����ö��š������ֿ�
                string strMegContent = arrACARSLegsInfo[6];

                #region ���Ŀ�Ļ���
                //��ɻ���
                iStartIndex = strMegContent.IndexOf("DEP") + 4;
                iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                m_strDEPSTN = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                //Ŀ�Ļ���
                iStartIndex = strMegContent.IndexOf("DES") + 4;
                iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                m_strARRSTN = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                //������������ת��������
                DataTable dtAirportInfo = new DataTable();
                
                //��ɻ���
                try
                {
                    dtAirportInfo = wsPublicService.GetAirportInfo(m_strDEPSTN, "", "", "", "", "").Tables[0];
                    if (dtAirportInfo.Rows.Count == 1)
                    {
                        m_strDEPSTN = dtAirportInfo.Rows[0]["AirportThreeCode"].ToString();

                        //��ȡ������γ��
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

                //Ŀ�Ļ���
                try
                {
                    dtAirportInfo = wsPublicService.GetAirportInfo(m_strARRSTN, "", "", "", "", "").Tables[0];
                    if (dtAirportInfo.Rows.Count == 1)
                    {
                        m_strARRSTN = dtAirportInfo.Rows[0]["AirportThreeCode"].ToString();

                        //��ȡ������γ��
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

                //���ݱ������ͻ�ȡ�����ؼ��ֵ�ֵ
                switch (m_messageType)
                {
                    #region OUT����ʱ���FOB
                    //OUT��
                    case MsgType.OUT:
                        //OUTʱ��
                        iStartIndex = strMegContent.IndexOf("OUT") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strOUT = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //FOB
                        iStartIndex = strMegContent.IndexOf("FOB");
                        if (iStartIndex != -1)
                        {
                            iStartIndex = iStartIndex + 4;
                            m_strOUT_FOB = strMegContent.Substring(iStartIndex);
                            //FOBֵ�����Ϊ�ջ�Ϊ��----��
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

                    #region OFF����OFFʱ�䡢ETAʱ���FOB
                    //OFF��
                    case MsgType.OFF:
                        //OFFʱ��
                        iStartIndex = strMegContent.IndexOf("OFF") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strOFF = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //ETAʱ��
                        iStartIndex = strMegContent.IndexOf("ETA") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strETA = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //���ETAʱ�䲻����
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

                    #region ON����ONʱ���FOB
                    //ON��
                    case MsgType.ON:
                        //ONʱ��
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

                    #region IN����INʱ���FOB
                    //IN��
                    case MsgType.IN:
                        //INʱ��
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

                    #region RTN����RTNʱ�䡢OUTʱ���FOB
                    //RTNʱ��
                    case MsgType.RTN:
                        //RTNʱ��
                        iStartIndex = strMegContent.IndexOf("RTN") + 4;
                        iEndIndex = strMegContent.IndexOf(",", iStartIndex);
                        m_strRTN = strMegContent.Substring(iStartIndex, iEndIndex - iStartIndex);

                        //OUTʱ��
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

        #region ���ݻ��������ѯ����λ�ã���γ�ȣ�
        public string[] GetAirportLocation(string strAPCode)
        {
            string[] strLocation = new string[2];
            string strLat = "";
            string strLon = "";

            //��ȡ����λ����Ϣ
            DataSet dsAPLocation = new DataSet();
            dsAPLocation = wsPublicService.GetAirportLocation(strAPCode);

            if (dsAPLocation != null)
            {
                DataTable dtAPLocation = dsAPLocation.Tables[0];
                if (dtAPLocation.Rows.Count == 1)
                {
                    //γ��
                    string strNoS = dtAPLocation.Rows[0]["Latitude1"].ToString();           //��ʶ�ϱ�γ
                    string strLatDegree = dtAPLocation.Rows[0]["Latitude2"].ToString();     //����
                    string strLatMinute = dtAPLocation.Rows[0]["Latitude3"].ToString();     //����

                    //��γ��ת���Զ�Ϊ��λ����ֵ
                    Double dLatDegree = Convert.ToDouble(strLatDegree);
                    Double dLatMinute = Convert.ToDouble(strLatMinute);
                    dLatMinute = dLatMinute / 60;
                    Double dLat = dLatDegree + dLatMinute;
                    strLat = Convert.ToString(dLat);

                    //ȡǰʮλ��ֵ
                    if (strLat.Length >= 10)
                    {
                        strLat = strLat.Substring(0, 10);
                    }

                    //�������γ������γ��ǰ�ӡ�-����
                    if (strNoS != "N")
                    {
                        strLat = "- " + strLat.Substring(0, 9);
                    }

                    //����
                    string strEoW = dtAPLocation.Rows[0]["Longitude1"].ToString();          //��ʶ������
                    string strLonDegree = dtAPLocation.Rows[0]["Longitude2"].ToString();    //����
                    string strLonMinute = dtAPLocation.Rows[0]["Longitude3"].ToString();    //����

                    //��γ��ת���Զ�Ϊ��λ����ֵ
                    Double dLonDegree = Convert.ToDouble(strLonDegree);
                    Double dLonMinute = Convert.ToDouble(strLonMinute);
                    dLonMinute = dLonMinute / 60;
                    Double dLon = dLonDegree + dLatMinute;
                    strLon = Convert.ToString(dLon);

                    //ȡǰʮλ��ֵ
                    if (strLon.Length >= 10)
                    {
                        strLon = strLon.Substring(0, 10);
                    }

                    //��������������ھ���ǰ�ӡ�-����
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

        #region ����
        /// <summary>
        /// �����
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// ����ƻ�����
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int LegNo
        {
            get { return m_iLegNo; }
            set { m_iLegNo = value; }
        }

        /// <summary>
        /// FOC����
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// ���ķ�������
        /// ������Ϊ���ͱ��ĵ����ڣ���DATOP������
        /// ���ڿ�0��ĺ��࣬���ͱ��ĵ����ڿ����뺽��ƻ��ĺ������ڲ�ͬ
        /// </summary>
        public string FlightDate
        {
            get { return m_strFlightDate; }
            set { m_strFlightDate = value; }
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
        /// ʼ��վγ��
        /// </summary>
        public string DEPSTN_LAT
        {
            get { return m_strDEPSTN_LAT; }
            set { m_strDEPSTN_LAT = value; }
        }

        /// <summary>
        /// ʼ��վ����
        /// </summary>
        public string DEPSTN_LON
        {
            get { return m_strDEPSTN_LON; }
            set { m_strDEPSTN_LON = value; }
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
        /// ����վ������
        /// </summary>
        public string ARRSTN_LAT
        {
            get { return m_strARRSTN_LAT; }
            set { m_strARRSTN_LAT = value; }
        }

        /// <summary>
        /// ����վ������
        /// </summary>
        public string ARRSTN_LON
        {
            get { return m_strARRSTN_LON; }
            set { m_strARRSTN_LON = value; }
        }

        /// <summary>
        /// �����豸����
        /// </summary>
        public string EquipType
        {
            get { return m_strEqipType; }
            set { m_strEqipType = value; }
        }

        /// <summary>
        /// �Ƴ�ʱ��
        /// </summary>
        public string ACARSOUT
        {
            get { return m_strOUT; }
            set { m_strOUT = value; }
        }

        /// <summary>
        /// OUT���е�FOB
        /// </summary>
        public string OUT_FOB
        {
            get { return m_strOUT_FOB; }
            set { m_strOUT_FOB = value; }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string ACARSOFF
        {
            get { return m_strOFF; }
            set { m_strOFF = value; }
        }

        /// <summary>
        /// OFF����ETA
        /// </summary>
        public string ACARSETA
        {
            get { return m_strETA; }
            set { m_strETA = value; }
        }

        /// <summary>
        /// OFF���е�FOB
        /// </summary>
        public string OFF_FOB
        {
            get { return m_strOFF_FOB; }
            set { m_strOFF_FOB = value; }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string ACARSON
        {
            get { return m_strON; }
            set { m_strON = value; }
        }

        /// <summary>
        /// ON���е�FOB
        /// </summary>
        public string ON_FOB
        {
            get { return m_strON_FOB; }
            set { m_strON_FOB = value; }
        }

        /// <summary>
        /// ���յ�ʱ��
        /// </summary>
        public string ACARSIN
        {
            get { return m_strIN; }
            set { m_strIN = value; }
        }

        /// <summary>
        /// IN���е�FOB
        /// </summary>
        public string IN_FOB
        {
            get { return m_strIN_FOB; }
            set { m_strIN_FOB = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string ACARSRTN
        {
            get { return m_strRTN; }
            set { m_strRTN = value; }
        }

        /// <summary>
        /// RTN����FOB
        /// </summary>
        public string RTN_FOB
        {
            get { return m_strRTN_FOB; }
            set { m_strRTN_FOB = value; }
        }

        /// <summary>
        /// λ�ñ��ľ���
        /// </summary>
        public string POS_LON
        {
            get { return m_strLON; }
            set { m_strLON = value; }
        }

        /// <summary>
        /// λ�ñ���γ��
        /// </summary>
        public string POS_LAT
        {
            get { return m_strLAT; }
            set { m_strLAT = value; }
        }

        /// <summary>
        /// λ�ñ��������
        /// </summary>
        public string POS_MCH
        {
            get { return m_strMCH; }
            set { m_strMCH = value; }
        }

        /// <summary>
        /// λ�ñ��������
        /// </summary>
        public string POS_CAS
        {
            get { return m_strCAS; }
            set { m_strCAS = value; }
        }

        /// <summary>
        /// λ�ñ��ķ���
        /// </summary>
        public string POS_WD
        {
            get { return m_strWD; }
            set { m_strWD = value; }
        }

        /// <summary>
        /// λ�ñ��ķ���
        /// </summary>
        public string POS_WS
        {
            get { return m_strWS; }
            set { m_strWS = value; }
        }

        /// <summary>
        /// λ�ñ��ķ��и߶�
        /// </summary>
        public string POS_FL
        {
            get { return m_strFL; }
            set { m_strFL = value; }
        }

        /// <summary>
        /// λ�ñ���TTG
        /// </summary>
        public string POS_TTG
        {
            get { return m_strTTG; }
            set { m_strTTG = value; }
        }

        /// <summary>
        /// λ�ñ���FOB
        /// </summary>
        public string POS_FOB
        {
            get { return m_strPOS_FOB; }
            set { m_strPOS_FOB = value; }
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
        /// ���ķ���ʱ�䣨�ӱ���ԭ����ȡ��
        /// </summary>
        public string MessageSendTime
        {
            get { return m_strMessageSendTime; }
            set { m_strMessageSendTime = value; }
        }
        
        /// <summary>
        /// ���Ĵ���ʱ�䣨�����ĵ��ﱾ�ش��̵�ʱ�䣩
        /// </summary>
        public string MessageProcTime
        {
            get { return m_strMessageProcTime; }
            set { m_strMessageProcTime = value; }
        }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public string MessageContent
        {
            get { return m_strMessageContent; }
            set { m_strMessageContent = value; }
        }
        #endregion
    }
}
