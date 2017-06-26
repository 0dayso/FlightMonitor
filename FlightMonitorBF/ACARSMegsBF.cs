using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Collections;
using System.Data;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ACARS报文处理业务外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：张  黎
    /// 创建日期：2007-02-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSMegsBF
    {
        public ACARSMegsBF()
        { }

        ACARSMegsDAF acarsMegsDAF = new ACARSMegsDAF();
        //自定义返回值
        ReturnValueSF rvSF = new ReturnValueSF();
        //互斥对象
        private object oMutex = new object();
        //调用Web服务
        //公共服务
        PublicService.PublicService wsPublicService = new AirSoft.FlightMonitor.FlightMonitorBF.PublicService.PublicService();
        //解析飞行计划的Web服务
        CFPWPService.clsWebServiceOfFPLInfo wsCFP = new AirSoft.FlightMonitor.FlightMonitorBF.CFPWPService.clsWebServiceOfFPLInfo();

        #region 存储ACARS报文
        /// <summary>
        /// 存储ACARS报文
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertACARSMegs(ACARSMegsBM acarsMegBM)
        {
            switch (acarsMegBM.MessageType)
            {
                case MsgType.OUT:
                    rvSF.Result = acarsMegsDAF.InsertOUTMegs(acarsMegBM);
                    return rvSF;
                case MsgType.OFF:
                    rvSF.Result = acarsMegsDAF.InsertOFFMegs(acarsMegBM);
                    return rvSF;
                case MsgType.ON:
                    rvSF.Result = acarsMegsDAF.InsertONMegs(acarsMegBM);
                    return rvSF;
                case MsgType.IN:
                    rvSF.Result = acarsMegsDAF.InsertINMegs(acarsMegBM);
                    return rvSF;
                case MsgType.RTN:
                    rvSF.Result = acarsMegsDAF.InsertRTNMegs(acarsMegBM);
                    return rvSF;
                default:
                    rvSF.Result = -1;
                    return rvSF;
            }
        }

        /// <summary>
        /// 存储ACARS报文
        /// 重载
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertACARSMegs(ACARSMegsBM acarsMegBM,int iUnCertMegId)
        {
            switch (acarsMegBM.MessageType)
            {
                case MsgType.OUT:
                    rvSF.Result = acarsMegsDAF.InsertOUTMegs(acarsMegBM, iUnCertMegId);
                    return rvSF;
                case MsgType.OFF:
                    rvSF.Result = acarsMegsDAF.InsertOFFMegs(acarsMegBM, iUnCertMegId);
                    return rvSF;
                case MsgType.ON:
                    rvSF.Result = acarsMegsDAF.InsertONMegs(acarsMegBM, iUnCertMegId);
                    return rvSF;
                case MsgType.IN:
                    rvSF.Result = acarsMegsDAF.InsertINMegs(acarsMegBM, iUnCertMegId);
                    return rvSF;
                case MsgType.RTN:
                    rvSF.Result = acarsMegsDAF.InsertRTNMegs(acarsMegBM, iUnCertMegId);
                    return rvSF;
                default:
                    rvSF.Result = -1;
                    return rvSF;
            }
        }
        #endregion

        #region 存储不确定的报文
        /// <summary>
        /// 存储不确定的报文
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertUnCertMegs(ACARSMegsBM acarsMegBM)
        {
            rvSF.Result = acarsMegsDAF.InsertUnCertMegs(acarsMegBM);
            return rvSF;
        }
        #endregion

        #region 存储原始报文
        /// <summary>
        /// 存储原始报文
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertOrigMegs(ACARSMegsBM acarsMegBM)
        {
            rvSF.Result = acarsMegsDAF.InsertOrigMegs(acarsMegBM);
            return rvSF;
        }
        #endregion

        #region 查找上一个报文的时间
        /// <summary>
        /// 查找上一个报文的时间
        /// 对OFF是OUT，对ON是OFF，对IN是ON，对RTN是OUT
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string GetPrevTime(ACARSMegsBM acarsMegBM)
        {
            string strPrevTime = acarsMegsDAF.GetPrevTime(acarsMegBM);
            return strPrevTime;
        }
        #endregion

        #region 将报文插入FOC系统
        /// <summary>
        /// 将MVA报插入FOC系统
        /// </summary>
        /// <param name="strMVA">MVA报文字符串</param>
        /// <returns></returns>
        public ReturnValueSF InsertMVAMegs(string strMVA)
        {
            rvSF.Result = acarsMegsDAF.InsertMVAMegs(strMVA);
            return rvSF;
        }
        #endregion

        #region 根据ACARS报文生成MVA和MVT
        public string GenMVAMegs(ACARSMegsBM acarsMegsBM)
        {
            string strMegType = "";

            //报文第一行
            string strMVA = "MVA\r\n";                                                  //分隔符
            strMVA += acarsMegsBM.FLTID;                                                //航班号
            strMVA += "/";
            strMVA += acarsMegsBM.FlightDate.Substring(8);                              //日期（日）
            strMVA += ".";
            strMVA += acarsMegsBM.LONG_REG;                                             //飞机号
            strMVA += ".";

            //生成MVT报，以发送到航站保障系统
            string strMVT = strMVA;

            #region 根据报文类型生成报文剩余内容
            //根据报文类型生成报文剩余内容
            switch (acarsMegsBM.MessageType)
            {
                case MsgType.OUT:
                    strMVA += acarsMegsBM.DEPSTN;                                       //起飞机场
                    strMVA += "\r\n";                                                   //分隔符
                    strMegType = "OUT";
                    strMVA += "AD";                                                     //报文类型
                    strMVA += acarsMegsBM.ACARSOUT;                                     //OUT时间
                    strMVA += "\r\n";

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //起飞机场
                    strMVT += "\r\n";                                                   //分隔符
                    strMVT += "OD";                                                     //报文类型
                    strMVT += acarsMegsBM.ACARSOUT;                                     //OUT时间
                    strMVT += "/ EA ";
                    strMVT += acarsMegsBM.ARRSTN;
                    strMVT += "\r\n";
                    break;
                case MsgType.OFF:
                    strMVA += acarsMegsBM.DEPSTN;                                       //起飞机场
                    strMVA += "\r\n";                                                   //分隔符
                    strMegType = "OFF";
                    strMVA += "AD";
                    //查询OUT时间
                    string strOUT = this.GetPrevTime(acarsMegsBM);
                    //如果查到OUT时间
                    if (strOUT != "")
                    {
                        strOUT = strOUT.Substring(11, 2) + strOUT.Substring(14, 2);
                    }
                    //如果没有OUT时间，则以四个空格代替
                    else
                    {
                        strOUT = "    ";
                    }
                    strMVA += strOUT + "/" + acarsMegsBM.ACARSOFF + " ";                //起飞时间：OUT/OFF
                    strMVA += "EA";

                    //根据飞行计划中航班的飞行时间修正航班的预达时刻
                    //查询飞行计划中的飞行时间
                    string strFlightId = acarsMegsBM.FLTID.Replace("HU", "").Replace("GS", "").Replace("JD", "").Replace("8L", "").Replace("HX", "").Replace("Y8", "").Replace("CN", "");
                    DataSet ds = wsCFP.ReturnInfoOfFPL(Convert.ToDateTime(acarsMegsBM.DATOP), strFlightId, acarsMegsBM.LONG_REG.Substring(1), acarsMegsBM.DEPSTN, acarsMegsBM.ARRSTN);

                    string strFlyTime = "";
                    string strETA = "";
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables["数据集_航班汇总信息"];
                        if (dt.Rows.Count > 0)
                        {
                            strFlyTime = dt.Rows[0]["目的机场_时间"].ToString();
                        }
                    }

                    //将查询出的时间转化为数字
                    if (strFlyTime != "")
                    {
                        int iFlyMinutes = Convert.ToInt16(strFlyTime);

                        try
                        {
                            //用OFF时间加上飞行时间，即为预达时间
                            string strOFF = acarsMegsBM.DATOP + " " + acarsMegsBM.ACARSOFF.Substring(0, 2) + ":" + acarsMegsBM.ACARSOFF.Substring(2, 2) + ":00";
                            strETA = Convert.ToDateTime(strOFF).AddMinutes(iFlyMinutes).ToString("yyyy-MM-dd HH:mm:ss");
                            //将预达时间转化为4位
                            strETA = strETA.Substring(11, 2) + strETA.Substring(14, 2);
                        }
                        catch
                        { }
                    }

                    strMVA += strETA + " ";                                             //预达时间
                    strMVA += acarsMegsBM.ARRSTN + "\r\n";                              //目的机场

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //起飞机场
                    strMVT += "\r\n";                                                   //分隔符
                    strMVT += "AD";
                    strMVT += strOUT.Trim() + "/" + acarsMegsBM.ACARSOFF + " ";         //起飞时间：OUT/OFF
                    strMVT += "EA";
                    strMVT += strETA.Trim() + " ";
                    strMVT += acarsMegsBM.ARRSTN + "\r\n";
                    break;
                case MsgType.ON:
                    strMVA += acarsMegsBM.ARRSTN;                                       //目的机场
                    strMVA += "\r\n";                                                   //分隔符
                    strMegType = "ON";
                    strMVA += "AA";                                                     //报文类型
                    strMVA += acarsMegsBM.ACARSON;                                      //ON时间
                    strMVA += "\r\n";                                                   //分隔符

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //目的机场
                    strMVT += "\r\n";                                                   //分隔符
                    strMVT += "OA";                                                     //报文类型
                    strMVT += acarsMegsBM.ACARSON;                                      //ON时间
                    strMVT += "/ " + acarsMegsBM.ARRSTN;
                    strMVT += "\r\n";                                                   //分隔符
                    break;
                case MsgType.IN:
                    strMVA += acarsMegsBM.ARRSTN;                                       //目的机场
                    strMVA += "\r\n";                                                   //分隔符
                    strMegType = "IN";
                    strMVA += "AA";
                    //查询ON时间
                    string strON = this.GetPrevTime(acarsMegsBM);
                    //如果查询到IN时间
                    if (strON != "")
                    {
                        strON = strON.Substring(11, 2) + strON.Substring(14, 2);
                    }
                    //如果没有查询到IN时间，则以四个空格代替
                    else
                    {
                        strON = "    ";
                    }
                    strMVA += strON + "/" + acarsMegsBM.ACARSIN + "\r\n";               //起飞时间：ON/IN

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //目的机场
                    strMVT += "\r\n";                                                   //分隔符
                    strMVT += "AA";
                    strMVT += strON.Trim() + "/" + acarsMegsBM.ACARSIN + " " + acarsMegsBM.ARRSTN + "\r\n";
                    break;
                default:
                    break;
            }
            #endregion

            //将MVT报生成txt文件存储到本地
            //以便航站保障系统处理

            //生成一个随机数
            Random rd = new Random();
            int iTemp = rd.Next(10001, 99999);

            //生成文件名
            string strFileName = "MVT_" + DateTime.UtcNow.ToString("HHmmss") + iTemp.ToString() + ".txt";
            strFileName = "E:\\Fleetwatch\\" + strFileName;
            //File.WriteAllText(strFileName, strMVT);

            return strMVA;
        }
        #endregion

        #region 根据ACARS报文生成FE报文
        public ReturnValueSF GenFEMegs(ACARSMegsBM acarsMegsBM, string strACType)
        {
            rvSF.Result = 1;

            //对OUT和IN不生成FE报文
            if (acarsMegsBM.MessageType == MsgType.OUT || acarsMegsBM.MessageType == MsgType.IN)
            {
                return rvSF;
            }

            //如果位置报中经纬度信息为空，或者没有航班号，也不生成FE报文
            if (acarsMegsBM.MessageType == MsgType.POS)
            {
                if (acarsMegsBM.POS_LON == "" || acarsMegsBM.POS_LAT == "" || acarsMegsBM.FLTID.Substring(2) == "0")
                {
                    return rvSF;
                }
            }

            //FE报文存储位置
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);
            //FE报文序列号sn
            int iFEMegsNo = 0;

            #region 获取报文序列号
            //获取报文序列号
            //打开sn.txt
            FileStream fs = File.Open(strFEPath + "sn.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);

            //读取当前的sn
            string strSn = sr.ReadToEnd();
            iFEMegsNo = Convert.ToInt32(strSn);
            sr.Close();

            //如果当前最大的序列号达到了最大值65535，则重新开始计数
            if (iFEMegsNo == 65535)
            {
                iFEMegsNo = 0;
            }
            //否则在当前的基础上增加1
            else
            {
                iFEMegsNo++;
            }
            #endregion

            #region 报头
            //发送到FE的报文
            string strFE = "1,";                        //Server Number Field1
            strFE += iFEMegsNo.ToString() + ",";        //报文序列号    Field2

            //将最大的序列号写入sn.txt
            fs = new FileStream(strFEPath + "sn.txt", FileMode.Open, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(iFEMegsNo);
            sw.Flush();
            sw.Close();

            //报文发送日期
            string strFltDate = acarsMegsBM.FlightDate.Replace("-", "");
            strFE += strFltDate + ",";                        //Field3                 
            //报文发送时间
            string strMegTime = acarsMegsBM.MessageSendTime.Substring(10).Trim().Replace(":", ""); 
            strFE += strMegTime + ",";                        //Field4
            //报文类型，发报对象，发报对象类型
            strFE += "1,1,2,";                                //Field5,6,7
            //将航班号中的公司两字代码转成三字代码
            string strFltId = "CHH";
            //“HU”以外的航班暂时不处理
            if(acarsMegsBM.FLTID.Substring(0,2) == "HU")
            {
                strFltId = acarsMegsBM.FLTID.Replace("HU", "CHH");
            }
            else
            {
                rvSF.Result = 1;
                return rvSF;
            }
            strFE += strFltId + ",";                          //航班号 Field8
            strFE += acarsMegsBM.LONG_REG + ",";              //飞机号 Field9

            //对于OFF和ON，读取起飞机场
            string strPOD = "";
            DataTable dtAirportInfo = new DataTable();

            if (acarsMegsBM.DEPSTN != null)
            {
                dtAirportInfo = wsPublicService.GetAirportInfo("", acarsMegsBM.DEPSTN, "", "", "", "").Tables[0];
                if (dtAirportInfo.Rows.Count == 1)
                {
                    strPOD = dtAirportInfo.Rows[0]["AirportFourCode"].ToString();
                }
                strFE += strPOD + ",";                       //起飞机场 Field10
            }
            else
            {
                strFE += ",";
            }

            //对于OFF和ON，读取计划日期
            if (acarsMegsBM.DATOP != null)
            {
                strFE += acarsMegsBM.DATOP.Replace("-", "") + ",";        //起飞日期 Field11
            }
            else
            {
                strFE += ",";
            }

            strFE += ",";                                   //起飞时间：由于系统没有获取航班计划起飞时刻，所以此项留空 Field12
            #endregion

            #region 报文体
            strFE += strFltDate + ",";        //报文日期 Field13
            strFE += strMegTime + ",";        //报文时间 Field14

            #region 报文类型
            //报文类型
            switch (acarsMegsBM.MessageType)
            {
                case MsgType.OUT:
                    strFE += "1,";
                    break;
                case MsgType.OFF:
                    strFE += "2,";
                    break;
                case MsgType.POS:
                    strFE += "3,";
                    break;
                case MsgType.ON:
                    strFE += "4,";
                    break;
                case MsgType.IN:
                    strFE += "5,";
                    break;
                default:
                    strFE += "3,";              //Field15
                    break;
            }
            #endregion

            #region 飞机位置经纬度
            //经纬度
            //如果是位置报，则发送位置报中的经纬度
            if (acarsMegsBM.MessageType == MsgType.POS)
            {
                if (acarsMegsBM.POS_LAT == "" || acarsMegsBM.POS_LON == "")
                {
                    return rvSF;
                }
                //否则不提供经纬度
                else
                {
                    strFE += acarsMegsBM.POS_LAT + ",";         //纬度 Field16
                    strFE += acarsMegsBM.POS_LON + ",";         //经度 Field17
                }
            }
            else if (acarsMegsBM.MessageType == MsgType.OFF)
            {
                if (acarsMegsBM.DEPSTN_LAT == "" || acarsMegsBM.DEPSTN_LON == "")
                {
                    return rvSF;
                }
                //否则不提供经纬度
                else
                {
                    strFE += acarsMegsBM.DEPSTN_LAT + ",";         //纬度 Field16
                    strFE += acarsMegsBM.DEPSTN_LON + ",";         //经度 Field17
                }
            }
            else if (acarsMegsBM.MessageType == MsgType.ON)
            {
                if (acarsMegsBM.ARRSTN_LAT == "" || acarsMegsBM.ARRSTN_LON == "")
                {
                    return rvSF;
                }
                //否则不提供经纬度
                else
                {
                    strFE += acarsMegsBM.ARRSTN_LAT + ",";         //纬度 Field16
                    strFE += acarsMegsBM.ARRSTN_LON + ",";         //经度 Field17
                }
            }
            else
            {
                return rvSF;
            }

            //如果为OFF或ON报，飞行高度为0
            if (acarsMegsBM.MessageType == MsgType.OFF || acarsMegsBM.MessageType == MsgType.ON)
            {
                strFE += "0,";               //Altitude Field18
            }
            //如果为位置报，则取ACARS报文中的值
            else
            {
                strFE += acarsMegsBM.POS_FL + ",";      //Altitude Field18
            }
            strFE += ",";               //Groud Speed Field19
            strFE += "1,";              //报文来源1=ACARS Field20
            strFE += "3600,";            //下一次报文发送时间：240秒后 Field21
            strFE += ",";               //ETA Field22
            #endregion

            #region 起飞目的机场
            //起飞目的机场
            //将机场三字码转成四字码
            //起飞机场四字码在前面已经取得，此处只取目的机场四字码
            string strPOA = "";
            
            //目的机场
            if (acarsMegsBM.ARRSTN != null)
            {
                dtAirportInfo = wsPublicService.GetAirportInfo("", acarsMegsBM.ARRSTN, "", "", "", "").Tables[0];
                if (dtAirportInfo.Rows.Count == 1)
                {
                    strPOA = dtAirportInfo.Rows[0]["AirportFourCode"].ToString();
                }
                else
                {
                    strPOA = "";
                }
            }

            //起飞机场四字码在前面已经取得
            strFE += strPOD + ",";      //Field23
            strFE += strPOA + ",";      //Field24
            #endregion

            strFE += strACType + ",";       //机型，Field25
            strFE += ",,,";                 //通讯验证，通讯模式，通讯网络， Field26,27,28

            #region FOB
            //FOB
            string strFOB = "";
            switch (acarsMegsBM.MessageType)
            {
                case MsgType.OUT:
                    strFOB = acarsMegsBM.OUT_FOB;
                    break;
                case MsgType.OFF:
                    strFOB = acarsMegsBM.OFF_FOB;
                    break;
                case MsgType.POS:
                    strFOB = acarsMegsBM.POS_FOB;
                    break;
                case MsgType.ON:
                    strFOB = acarsMegsBM.ON_FOB;
                    break;
                case MsgType.IN:
                    strFOB = acarsMegsBM.IN_FOB;
                    break;
                default:
                    break;
            }
            strFE += strFOB + ",";  //Field29
            #endregion

            strFE += "1";           //FOB计量单位（1=重量，磅） Field30
            #endregion
            
            //将报文临时存储在数据库中，以便定时发送
            rvSF.Result = acarsMegsDAF.InsertFEMeg(strFE);
            return rvSF;
        }
        #endregion

        #region 根据ACARS的OUT报生成FE的RTE报
        /// <summary>
        /// 根据ACARS的OUT报生成FE的RTE报
        /// </summary>
        /// <param name="acarsMegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GenFERteMegs(ACARSMegsBM acarsMegsBM)
        {
            rvSF.Result = 1;

            //FE报文存储位置
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);
            //FE报文序列号sn
            int iFEMegsNo = 0;

            #region 获取报文序列号
            //获取报文序列号
            //打开sn.txt
            FileStream fs = File.Open(strFEPath + "sn.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);

            //读取当前的sn
            string strSn = sr.ReadToEnd();
            iFEMegsNo = Convert.ToInt32(strSn);
            sr.Close();

            //如果当前最大的序列号达到了最大值65535，则重新开始计数
            if (iFEMegsNo == 65535)
            {
                iFEMegsNo = 0;
            }
            //否则在当前的基础上增加1
            else
            {
                iFEMegsNo++;
            }
            #endregion

            #region 报头
            //发送到FE的报文
            string strFE = "1,";                        //Server Number Field1
            strFE += iFEMegsNo.ToString() + ",";        //报文序列号    Field2

            //将最大的序列号写入sn.txt
            fs = new FileStream(strFEPath + "sn.txt", FileMode.Open, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(iFEMegsNo);
            sw.Flush();
            sw.Close();

            //报文发送日期
            string strFltDate = acarsMegsBM.FlightDate.Replace("-", "");
            strFE += strFltDate + ",";                        //Field3                 
            //报文发送时间
            string strMegTime = acarsMegsBM.MessageSendTime.Substring(10).Trim().Replace(":", "");
            strFE += strMegTime + ",";                        //Field4
            //报文类型，发报对象，发报对象类型
            strFE += "2,1,2,";                                //Field5,6,7
            //将航班号中的公司两字代码转成三字代码
            string strFltId = "CHH";
            //“HU”以外的航班暂时不处理
            if (acarsMegsBM.FLTID.Substring(0, 2) == "HU")
            {
                strFltId = acarsMegsBM.FLTID.Replace("HU", "CHH");
            }
            else
            {
                rvSF.Result = 1;
                return rvSF;
            }
            strFE += strFltId + ",";                          //航班号 Field8
            strFE += acarsMegsBM.LONG_REG + ",";              //飞机号 Field9

            //读取起飞机场
            string strPOD = "";
            DataTable dtAirportInfo = new DataTable();

            if (acarsMegsBM.DEPSTN != null)
            {
                dtAirportInfo = wsPublicService.GetAirportInfo("", acarsMegsBM.DEPSTN, "", "", "", "").Tables[0];
                if (dtAirportInfo.Rows.Count == 1)
                {
                    strPOD = dtAirportInfo.Rows[0]["AirportFourCode"].ToString();
                }
                strFE += strPOD + ",";                       //起飞机场 Field10
            }
            else
            {
                strFE += ",";
            }

            //读取计划日期
            if (acarsMegsBM.DATOP != null)
            {
                strFE += acarsMegsBM.DATOP.Replace("-", "") + ",";        //起飞日期 Field11
            }
            else
            {
                strFE += ",";
            }

            //读取OUT时间
            if (acarsMegsBM.ACARSOUT != null)
            {
                strFE += acarsMegsBM.ACARSOUT.Substring(0, 2) + ",";      //起飞时间：直接取OUT时间 Field12
            }
            else
            {
                strFE += "00,";
            }
            #endregion

            #region 报文体
            strFE += strFltDate + ",";        //报文日期 Field13
            strFE += strMegTime + ",";        //报文时间 Field14
            strFE += "3,";                    //航路格式 Field15

            #region 生成航路
            //提取航路
            string strRoute = "\"";

            //由于暂时无法解析二放计划，对北京-西雅图和西雅图-北京的航路手工写入
            if (acarsMegsBM.DEPSTN == "PEK" && acarsMegsBM.ARRSTN == "SEA")
            {
                string strPEKSEA = "40.0716/116.5966/ZBAA,40.7333/116.7333/YV,41.2350/116.6250/GM,42.1833/118.1833/KAKAT,43.5583/122.1966/TGO,44.455/123.455/OTABO,44.9133/124.8033/FYU,45.6283/126.6283/HRB,46.840/130.840/JMU,47.8683/134.6466/IJ,47.8833/134.8833/ARGUK,48.5450/135.2100/HAB,48.9783/135.9783/TOMSU,49.4500/136.5666/FI,50.090/137.090/LALET,50.3833/137.55/XR,50.9166/138.9166/TERBO,51.7650/139.0933/ERMAN,53.150/140.150/UHNN,54.1433/142.1433/KUVAL,54.5916/142.5916/OMARU,55.7333/144.7333/IGODA,56.0833/145.0833/GRUMA,57.9966/149.9966/GAKRA,58.610/150.610/DATIR,59.030/151.030/VELUT,59.090/151.090/BUVAK,60.200/154.200/URABI,61.835/160.835/BUMAT,63.390/165.390/GORAS,64.6666/170.6666/UHMO,64.725/172.725/BETAM,64.735/177.735/UHMA,64.6816/179.6816/VEDLA,64.3766/-174.3766/GIRLO,64.3766/-173.2433/BC,64.3683/-171.3683/ABINA,64.4000/-171.0916/VALDA,62.950/-155.950/MCG,61.1500/-150.2066/ANC,61.0000/-149.0000/YESKA,60.4816/-146.4816/JOH,60.480/-146.480/ALJ,60.1183/-145.1183/HUMPY,59.5966/-142.5966/WOXOX,58.8033/-140.8033/LAIRE,56.860/-135.860/BKA,55.6916/-132.6916/UDENE,55.060/-131.060/ANN,54.6066/-131.6066/HANRY,53.035/-129.035/DUGGS,52.2383/-128.2383/PRYCE,50.6833/-127.3650/YZT,49.5916/-125.5916/ROYST,49.0733/-124.0733/ARRUE,48.7266/-123.7266/YYJ,48.345/-123.345/ORCUS,48.0316/-122.0316/JAWBN,47.9466/-122.9466/DIGGN,47.6216/-122.6216/ALKIA,47.4483/-122.3100/KSEA";
                strRoute += strPEKSEA;
            }
            else if (acarsMegsBM.DEPSTN == "SEA" && acarsMegsBM.ARRSTN == "PEK")
            {
                string strSEAPEK = "47.4483/-122.3100/KSEA,48.2983/-124.6266/TOU,49.050/-125.050/INHAM,50.6833/-127.3650/YZT,52.2383/-128.2383/PRYCE,53.035/-129.035/DUGGS,54.6066/-131.6066/HANRY,55.060/-131.060/ANN,55.6916/-132.6916/UDENE,56.860/-135.860/BKA,58.8033/-140.8033/LAIRE,59.5966/-142.5966/WOXOX,60.1183/-145.1183/HUMPY,60.480/-146.480/ALJ,60.4816/-146.4816/JOH,61.0000/-149.0000/YESKA,61.1500/-150.2066/ANC,62.950/-155.950/MCG,63.8916/-160.8916/UNK,64.345/-164.345/WONAB,64.485/-165.485/OME,64.4933/-165.4933/FDV,64.455/-168.455/YUREE,64.4000/-171.0916/VALDA,64.3683/-171.3683/ABINA,64.3766/-173.2433/BC,64.3766/-174.3766/GIRLO,64.6816/179.6816/VEDLA,64.735/177.735/UHMA,64.725/172.725/BETAM,64.6666/170.6666/UHMO,63.390/165.390/GORAS,61.835/160.835/BUMAT,60.200/154.200/URABI,59.090/151.090/BUVAK,59.030/151.030/VELUT,58.610/150.610/DATIR,57.9966/149.9966/GAKRA,56.0833/145.0833/GRUMA,55.7333/144.7333/IGODA,54.5916/142.5916/OMARU,54.1433/142.1433/KUVAL,53.150/140.150/UHNN,51.7650/139.0933/ERMAN,50.9166/138.9166/TERBO,50.3833/137.55/XR,50.090/137.090/LALET,49.4500/136.5666/FI,48.9783/135.9783/TOMSU,48.5450/135.2100/HAB,47.8833/134.8833/ARGUK,47.8683/134.6466/IJ,46.840/130.840/JMU,45.6283/126.6283/HRB,44.9133/124.8033/FYU,44.455/123.455/OTABO,43.5583/122.1966/TGO,41.4216/118.4216/DABMA,41.0000/117.0000/SABEM,40.745/116.745/GITUM,40.0716/116.5966/ZBAA";
                strRoute += strSEAPEK;
            }
            else
            {
                string strFlightId = acarsMegsBM.FLTID.Replace("HU", "").Replace("GS", "").Replace("JD", "").Replace("8L", "").Replace("HX", "").Replace("Y8", "").Replace("CN", "");

                //由于Web服务中航班时刻，使用本地时，将ACARS报文中的时间转成本地时
                DateTime dtFltDate = Convert.ToDateTime(acarsMegsBM.MessageSendTime).ToLocalTime();
                DataSet ds = wsCFP.ReturnInfoOfFPL(dtFltDate, strFlightId, acarsMegsBM.LONG_REG.Substring(1), acarsMegsBM.DEPSTN, acarsMegsBM.ARRSTN);

                string strLat = "";
                string strLon = "";
                if (ds != null)
                {
                    DataTable dt = ds.Tables["数据集_航路点信息"];
                    if (dt != null)
                    {
                        //读取所有航路点的信息
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //纬度
                            strLat = dt.Rows[i]["纬度"].ToString();
                            int iIndexLat = strLat.IndexOf(".");
                            int iLength = strLat.Length - iIndexLat;

                            //如果纬度后面有小数点
                            if (iIndexLat > 0)
                            {
                                //将纬度数据长度限制在小数点后四位
                                if (iLength > 4)
                                {
                                    strLat = strLat.Substring(0, iIndexLat) + strLat.Substring(iIndexLat, 5);
                                }
                                else if (iLength < 4)
                                {
                                    string strZero = "";
                                    for (int j = 0; j < (4 - iLength); j++)
                                    {
                                        strZero += "0";
                                    }
                                    strLat += strZero;
                                }
                            }
                            else
                            {
                                strLat += ".0000";
                            }

                            //经度
                            strLon = dt.Rows[i]["经度"].ToString();
                            iIndexLat = strLon.IndexOf(".");
                            iLength = strLon.Length - iIndexLat;

                            //如果经度后面有小数点
                            if (iIndexLat > 0)
                            {
                                //将纬度数据长度限制在小数点后四位
                                if (iLength > 4)
                                {
                                    strLon = strLon.Substring(0, iIndexLat) + strLon.Substring(iIndexLat, 5);
                                }
                                else if (iLength < 4)
                                {
                                    string strZero = "";
                                    for (int j = 0; j < (4 - iLength); j++)
                                    {
                                        strZero += "0";
                                    }
                                    strLon += strZero;
                                }
                            }
                            else
                            {
                                strLon += ".0000";
                            }
                            strRoute += strLat + "/" + strLon + "/" + dt.Rows[i]["航路点"].ToString() + ",";
                        }
                    }
                }
                //去掉最后一个“，”号
                strRoute = strRoute.Substring(0, strRoute.Length - 1);
            }
            strRoute += "\"";

            strFE += strRoute + ",";          //航路内容 Field16
            #endregion

            strFE += "1,";                    //Route extent Field17
            strFE += "4";                     //Route type Field18
            #endregion

            //将报文临时存储在数据库中，以便定时发送
            rvSF.Result = acarsMegsDAF.InsertFEMeg(strFE);
            return rvSF;
        }
        #endregion

        #region 生成3PD文件
        public bool Gen3PDFile(string strFileName)
        {
            //FE报文存储位置
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);

            //获取上一次提取的最后一个FE记录编号
            FileStream fs = new FileStream(strFEPath + "MaxNo.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);
            string strMaxNoOld = sr.ReadToEnd();
            sr.Close();
            int iMaxNoOld = Convert.ToInt32(strMaxNoOld);

            //查询从上一次提取报文到现在生成的所有新报文
            DataTable dt = acarsMegsDAF.GetFEMegs(iMaxNoOld);

            //获取本次提取到的记录总数
            int iMegsCount = 0;
            //使用try-catch避免因返回结果dt为null造成错误
            try
            {
                iMegsCount = dt.Rows.Count;
            }
            catch
            {
                iMegsCount = 0;
            }

            //如果本次没有提取到任何数据，返回false
            if (iMegsCount == 0)
            {
                return false;
            }
            else
            {
                //生成3PD文件
                int iMaxNo = 0;
                string str3PD = "3PDI2.0\r\n";

                //提取报文内容
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str3PD += dt.Rows[i]["cntFEMegContent"].ToString();
                    str3PD += "\r\n";
                }

                //将报文内容写入待发送的3PD文件中
                //fs = new FileStream(strFEPath + strFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                //StreamWriter sw = new StreamWriter(fs);
                //sw.Write(str3PD);
                //sw.Flush;
                //sw.Close();
                File.WriteAllText(strFEPath + strFileName, str3PD);

                //获取本次获取到的记录id最大值
                iMaxNo = Convert.ToInt32(dt.Rows[iMegsCount - 1]["iPK"].ToString());

                //将记录写入MaxNo文件中保存
                File.WriteAllText(strFEPath + "MaxNo.txt", iMaxNo.ToString());

                return true;
            }
        }
        #endregion

        #region 生成HBT报
        public void GenHBTMeg(string strFileName)
        {
            //FE报文存储位置
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);
            //FE报文序列号sn
            int iMaxSn = 0;

            //获取报文序列号
            //打开sn.txt
            FileStream fs = File.Open(strFEPath + "Sn.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);

            //读取当前的sn
            string strSn = sr.ReadToEnd();
            iMaxSn = Convert.ToInt32(strSn);
            sr.Close();

            if (iMaxSn == 65535)
            {
                iMaxSn = 0;
            }
            else
            {
                iMaxSn++;
            }

            //生成HBT报文内容
            string str3PD = "3PDI2.0\r\n";
            str3PD += "1," + iMaxSn + "," + DateTime.Now.ToString("yyyyMMdd") + "," + DateTime.Now.ToString("HHmmss") + ",6,,,,,,,\r\n";

            File.WriteAllText(strFEPath + strFileName, str3PD);
            File.WriteAllText(strFEPath + "Sn.txt", iMaxSn.ToString());
        }
        #endregion

        #region 通过FTP上传文件
        /// <summary>
        /// 通过FTP上传文件
        /// </summary>
        /// <param name="strFtpServer">服务器地址</param>
        /// <param name="strFtpUser">用户名</param>
        /// <param name="strFtpPass">密码</param>
        /// <param name="strFile">待上传文件的路径</param>
        /// <returns>是否上传成功</returns>
        public bool FtpFiles(string strFtpServer, string strFtpUser, string strFtpPass, string strFile)
        {
            FtpWebRequest ftpReq;
            //生成文件对象
            FileInfo file3PD = new FileInfo(strFile);
            //Uri
            string strUri = "ftp://" + strFtpServer + "/" + file3PD.Name;

            //生成FtpWebRequest对象
            ftpReq = (FtpWebRequest)FtpWebRequest.Create(new Uri(strUri));

            //设置属性
            //用户名密码
            ftpReq.Credentials = new NetworkCredential(strFtpUser, strFtpPass);
            //保持与服务器的连接
            ftpReq.KeepAlive = true;
            //指定执行的命令
            ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
            //以二进制方式传输
            ftpReq.UseBinary = true;
            //以Passive方式连接
            ftpReq.UsePassive = true;

            //指定缓冲区大小2kb
            int iBufferLen = 2048;
            //新建缓冲区
            byte[] buff = new byte[iBufferLen];
            int iContentLen;

            //打开文件流读取要上传的文件
            FileStream fs = file3PD.OpenRead();

            try
            {
                //新建流用于将上传文件的流写入服务器上的文件
                Stream stm = ftpReq.GetRequestStream();

                //将上传文件的流写入指定缓冲区，返回本次读取的内容长度
                iContentLen = fs.Read(buff, 0, iBufferLen);

                //如果上传文件流没有结束
                while (iContentLen != 0)
                {
                    //将指定缓冲区的数据从上传文件流file stream写入上传服务器的流stream
                    stm.Write(buff, 0, iContentLen);

                    //将上传文件的流写入指定缓冲区，返回本次读取的内容长度
                    iContentLen = fs.Read(buff, 0, iBufferLen);
                }

                //关闭流
                stm.Close();
                fs.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


    }
}
