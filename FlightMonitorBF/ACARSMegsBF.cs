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
    /// ACARS���Ĵ���ҵ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���  ��
    /// �������ڣ�2007-02-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSMegsBF
    {
        public ACARSMegsBF()
        { }

        ACARSMegsDAF acarsMegsDAF = new ACARSMegsDAF();
        //�Զ��巵��ֵ
        ReturnValueSF rvSF = new ReturnValueSF();
        //�������
        private object oMutex = new object();
        //����Web����
        //��������
        PublicService.PublicService wsPublicService = new AirSoft.FlightMonitor.FlightMonitorBF.PublicService.PublicService();
        //�������мƻ���Web����
        CFPWPService.clsWebServiceOfFPLInfo wsCFP = new AirSoft.FlightMonitor.FlightMonitorBF.CFPWPService.clsWebServiceOfFPLInfo();

        #region �洢ACARS����
        /// <summary>
        /// �洢ACARS����
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
        /// �洢ACARS����
        /// ����
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

        #region �洢��ȷ���ı���
        /// <summary>
        /// �洢��ȷ���ı���
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertUnCertMegs(ACARSMegsBM acarsMegBM)
        {
            rvSF.Result = acarsMegsDAF.InsertUnCertMegs(acarsMegBM);
            return rvSF;
        }
        #endregion

        #region �洢ԭʼ����
        /// <summary>
        /// �洢ԭʼ����
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertOrigMegs(ACARSMegsBM acarsMegBM)
        {
            rvSF.Result = acarsMegsDAF.InsertOrigMegs(acarsMegBM);
            return rvSF;
        }
        #endregion

        #region ������һ�����ĵ�ʱ��
        /// <summary>
        /// ������һ�����ĵ�ʱ��
        /// ��OFF��OUT����ON��OFF����IN��ON����RTN��OUT
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string GetPrevTime(ACARSMegsBM acarsMegBM)
        {
            string strPrevTime = acarsMegsDAF.GetPrevTime(acarsMegBM);
            return strPrevTime;
        }
        #endregion

        #region �����Ĳ���FOCϵͳ
        /// <summary>
        /// ��MVA������FOCϵͳ
        /// </summary>
        /// <param name="strMVA">MVA�����ַ���</param>
        /// <returns></returns>
        public ReturnValueSF InsertMVAMegs(string strMVA)
        {
            rvSF.Result = acarsMegsDAF.InsertMVAMegs(strMVA);
            return rvSF;
        }
        #endregion

        #region ����ACARS��������MVA��MVT
        public string GenMVAMegs(ACARSMegsBM acarsMegsBM)
        {
            string strMegType = "";

            //���ĵ�һ��
            string strMVA = "MVA\r\n";                                                  //�ָ���
            strMVA += acarsMegsBM.FLTID;                                                //�����
            strMVA += "/";
            strMVA += acarsMegsBM.FlightDate.Substring(8);                              //���ڣ��գ�
            strMVA += ".";
            strMVA += acarsMegsBM.LONG_REG;                                             //�ɻ���
            strMVA += ".";

            //����MVT�����Է��͵���վ����ϵͳ
            string strMVT = strMVA;

            #region ���ݱ����������ɱ���ʣ������
            //���ݱ����������ɱ���ʣ������
            switch (acarsMegsBM.MessageType)
            {
                case MsgType.OUT:
                    strMVA += acarsMegsBM.DEPSTN;                                       //��ɻ���
                    strMVA += "\r\n";                                                   //�ָ���
                    strMegType = "OUT";
                    strMVA += "AD";                                                     //��������
                    strMVA += acarsMegsBM.ACARSOUT;                                     //OUTʱ��
                    strMVA += "\r\n";

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //��ɻ���
                    strMVT += "\r\n";                                                   //�ָ���
                    strMVT += "OD";                                                     //��������
                    strMVT += acarsMegsBM.ACARSOUT;                                     //OUTʱ��
                    strMVT += "/ EA ";
                    strMVT += acarsMegsBM.ARRSTN;
                    strMVT += "\r\n";
                    break;
                case MsgType.OFF:
                    strMVA += acarsMegsBM.DEPSTN;                                       //��ɻ���
                    strMVA += "\r\n";                                                   //�ָ���
                    strMegType = "OFF";
                    strMVA += "AD";
                    //��ѯOUTʱ��
                    string strOUT = this.GetPrevTime(acarsMegsBM);
                    //����鵽OUTʱ��
                    if (strOUT != "")
                    {
                        strOUT = strOUT.Substring(11, 2) + strOUT.Substring(14, 2);
                    }
                    //���û��OUTʱ�䣬�����ĸ��ո����
                    else
                    {
                        strOUT = "    ";
                    }
                    strMVA += strOUT + "/" + acarsMegsBM.ACARSOFF + " ";                //���ʱ�䣺OUT/OFF
                    strMVA += "EA";

                    //���ݷ��мƻ��к���ķ���ʱ�����������Ԥ��ʱ��
                    //��ѯ���мƻ��еķ���ʱ��
                    string strFlightId = acarsMegsBM.FLTID.Replace("HU", "").Replace("GS", "").Replace("JD", "").Replace("8L", "").Replace("HX", "").Replace("Y8", "").Replace("CN", "");
                    DataSet ds = wsCFP.ReturnInfoOfFPL(Convert.ToDateTime(acarsMegsBM.DATOP), strFlightId, acarsMegsBM.LONG_REG.Substring(1), acarsMegsBM.DEPSTN, acarsMegsBM.ARRSTN);

                    string strFlyTime = "";
                    string strETA = "";
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables["���ݼ�_���������Ϣ"];
                        if (dt.Rows.Count > 0)
                        {
                            strFlyTime = dt.Rows[0]["Ŀ�Ļ���_ʱ��"].ToString();
                        }
                    }

                    //����ѯ����ʱ��ת��Ϊ����
                    if (strFlyTime != "")
                    {
                        int iFlyMinutes = Convert.ToInt16(strFlyTime);

                        try
                        {
                            //��OFFʱ����Ϸ���ʱ�䣬��ΪԤ��ʱ��
                            string strOFF = acarsMegsBM.DATOP + " " + acarsMegsBM.ACARSOFF.Substring(0, 2) + ":" + acarsMegsBM.ACARSOFF.Substring(2, 2) + ":00";
                            strETA = Convert.ToDateTime(strOFF).AddMinutes(iFlyMinutes).ToString("yyyy-MM-dd HH:mm:ss");
                            //��Ԥ��ʱ��ת��Ϊ4λ
                            strETA = strETA.Substring(11, 2) + strETA.Substring(14, 2);
                        }
                        catch
                        { }
                    }

                    strMVA += strETA + " ";                                             //Ԥ��ʱ��
                    strMVA += acarsMegsBM.ARRSTN + "\r\n";                              //Ŀ�Ļ���

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //��ɻ���
                    strMVT += "\r\n";                                                   //�ָ���
                    strMVT += "AD";
                    strMVT += strOUT.Trim() + "/" + acarsMegsBM.ACARSOFF + " ";         //���ʱ�䣺OUT/OFF
                    strMVT += "EA";
                    strMVT += strETA.Trim() + " ";
                    strMVT += acarsMegsBM.ARRSTN + "\r\n";
                    break;
                case MsgType.ON:
                    strMVA += acarsMegsBM.ARRSTN;                                       //Ŀ�Ļ���
                    strMVA += "\r\n";                                                   //�ָ���
                    strMegType = "ON";
                    strMVA += "AA";                                                     //��������
                    strMVA += acarsMegsBM.ACARSON;                                      //ONʱ��
                    strMVA += "\r\n";                                                   //�ָ���

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //Ŀ�Ļ���
                    strMVT += "\r\n";                                                   //�ָ���
                    strMVT += "OA";                                                     //��������
                    strMVT += acarsMegsBM.ACARSON;                                      //ONʱ��
                    strMVT += "/ " + acarsMegsBM.ARRSTN;
                    strMVT += "\r\n";                                                   //�ָ���
                    break;
                case MsgType.IN:
                    strMVA += acarsMegsBM.ARRSTN;                                       //Ŀ�Ļ���
                    strMVA += "\r\n";                                                   //�ָ���
                    strMegType = "IN";
                    strMVA += "AA";
                    //��ѯONʱ��
                    string strON = this.GetPrevTime(acarsMegsBM);
                    //�����ѯ��INʱ��
                    if (strON != "")
                    {
                        strON = strON.Substring(11, 2) + strON.Substring(14, 2);
                    }
                    //���û�в�ѯ��INʱ�䣬�����ĸ��ո����
                    else
                    {
                        strON = "    ";
                    }
                    strMVA += strON + "/" + acarsMegsBM.ACARSIN + "\r\n";               //���ʱ�䣺ON/IN

                    //MVT
                    strMVT += acarsMegsBM.DEPSTN;                                       //Ŀ�Ļ���
                    strMVT += "\r\n";                                                   //�ָ���
                    strMVT += "AA";
                    strMVT += strON.Trim() + "/" + acarsMegsBM.ACARSIN + " " + acarsMegsBM.ARRSTN + "\r\n";
                    break;
                default:
                    break;
            }
            #endregion

            //��MVT������txt�ļ��洢������
            //�Ա㺽վ����ϵͳ����

            //����һ�������
            Random rd = new Random();
            int iTemp = rd.Next(10001, 99999);

            //�����ļ���
            string strFileName = "MVT_" + DateTime.UtcNow.ToString("HHmmss") + iTemp.ToString() + ".txt";
            strFileName = "E:\\Fleetwatch\\" + strFileName;
            //File.WriteAllText(strFileName, strMVT);

            return strMVA;
        }
        #endregion

        #region ����ACARS��������FE����
        public ReturnValueSF GenFEMegs(ACARSMegsBM acarsMegsBM, string strACType)
        {
            rvSF.Result = 1;

            //��OUT��IN������FE����
            if (acarsMegsBM.MessageType == MsgType.OUT || acarsMegsBM.MessageType == MsgType.IN)
            {
                return rvSF;
            }

            //���λ�ñ��о�γ����ϢΪ�գ�����û�к���ţ�Ҳ������FE����
            if (acarsMegsBM.MessageType == MsgType.POS)
            {
                if (acarsMegsBM.POS_LON == "" || acarsMegsBM.POS_LAT == "" || acarsMegsBM.FLTID.Substring(2) == "0")
                {
                    return rvSF;
                }
            }

            //FE���Ĵ洢λ��
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);
            //FE�������к�sn
            int iFEMegsNo = 0;

            #region ��ȡ�������к�
            //��ȡ�������к�
            //��sn.txt
            FileStream fs = File.Open(strFEPath + "sn.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);

            //��ȡ��ǰ��sn
            string strSn = sr.ReadToEnd();
            iFEMegsNo = Convert.ToInt32(strSn);
            sr.Close();

            //�����ǰ�������кŴﵽ�����ֵ65535�������¿�ʼ����
            if (iFEMegsNo == 65535)
            {
                iFEMegsNo = 0;
            }
            //�����ڵ�ǰ�Ļ���������1
            else
            {
                iFEMegsNo++;
            }
            #endregion

            #region ��ͷ
            //���͵�FE�ı���
            string strFE = "1,";                        //Server Number Field1
            strFE += iFEMegsNo.ToString() + ",";        //�������к�    Field2

            //���������к�д��sn.txt
            fs = new FileStream(strFEPath + "sn.txt", FileMode.Open, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(iFEMegsNo);
            sw.Flush();
            sw.Close();

            //���ķ�������
            string strFltDate = acarsMegsBM.FlightDate.Replace("-", "");
            strFE += strFltDate + ",";                        //Field3                 
            //���ķ���ʱ��
            string strMegTime = acarsMegsBM.MessageSendTime.Substring(10).Trim().Replace(":", ""); 
            strFE += strMegTime + ",";                        //Field4
            //�������ͣ��������󣬷�����������
            strFE += "1,1,2,";                                //Field5,6,7
            //��������еĹ�˾���ִ���ת�����ִ���
            string strFltId = "CHH";
            //��HU������ĺ�����ʱ������
            if(acarsMegsBM.FLTID.Substring(0,2) == "HU")
            {
                strFltId = acarsMegsBM.FLTID.Replace("HU", "CHH");
            }
            else
            {
                rvSF.Result = 1;
                return rvSF;
            }
            strFE += strFltId + ",";                          //����� Field8
            strFE += acarsMegsBM.LONG_REG + ",";              //�ɻ��� Field9

            //����OFF��ON����ȡ��ɻ���
            string strPOD = "";
            DataTable dtAirportInfo = new DataTable();

            if (acarsMegsBM.DEPSTN != null)
            {
                dtAirportInfo = wsPublicService.GetAirportInfo("", acarsMegsBM.DEPSTN, "", "", "", "").Tables[0];
                if (dtAirportInfo.Rows.Count == 1)
                {
                    strPOD = dtAirportInfo.Rows[0]["AirportFourCode"].ToString();
                }
                strFE += strPOD + ",";                       //��ɻ��� Field10
            }
            else
            {
                strFE += ",";
            }

            //����OFF��ON����ȡ�ƻ�����
            if (acarsMegsBM.DATOP != null)
            {
                strFE += acarsMegsBM.DATOP.Replace("-", "") + ",";        //������� Field11
            }
            else
            {
                strFE += ",";
            }

            strFE += ",";                                   //���ʱ�䣺����ϵͳû�л�ȡ����ƻ����ʱ�̣����Դ������� Field12
            #endregion

            #region ������
            strFE += strFltDate + ",";        //�������� Field13
            strFE += strMegTime + ",";        //����ʱ�� Field14

            #region ��������
            //��������
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

            #region �ɻ�λ�þ�γ��
            //��γ��
            //�����λ�ñ�������λ�ñ��еľ�γ��
            if (acarsMegsBM.MessageType == MsgType.POS)
            {
                if (acarsMegsBM.POS_LAT == "" || acarsMegsBM.POS_LON == "")
                {
                    return rvSF;
                }
                //�����ṩ��γ��
                else
                {
                    strFE += acarsMegsBM.POS_LAT + ",";         //γ�� Field16
                    strFE += acarsMegsBM.POS_LON + ",";         //���� Field17
                }
            }
            else if (acarsMegsBM.MessageType == MsgType.OFF)
            {
                if (acarsMegsBM.DEPSTN_LAT == "" || acarsMegsBM.DEPSTN_LON == "")
                {
                    return rvSF;
                }
                //�����ṩ��γ��
                else
                {
                    strFE += acarsMegsBM.DEPSTN_LAT + ",";         //γ�� Field16
                    strFE += acarsMegsBM.DEPSTN_LON + ",";         //���� Field17
                }
            }
            else if (acarsMegsBM.MessageType == MsgType.ON)
            {
                if (acarsMegsBM.ARRSTN_LAT == "" || acarsMegsBM.ARRSTN_LON == "")
                {
                    return rvSF;
                }
                //�����ṩ��γ��
                else
                {
                    strFE += acarsMegsBM.ARRSTN_LAT + ",";         //γ�� Field16
                    strFE += acarsMegsBM.ARRSTN_LON + ",";         //���� Field17
                }
            }
            else
            {
                return rvSF;
            }

            //���ΪOFF��ON�������и߶�Ϊ0
            if (acarsMegsBM.MessageType == MsgType.OFF || acarsMegsBM.MessageType == MsgType.ON)
            {
                strFE += "0,";               //Altitude Field18
            }
            //���Ϊλ�ñ�����ȡACARS�����е�ֵ
            else
            {
                strFE += acarsMegsBM.POS_FL + ",";      //Altitude Field18
            }
            strFE += ",";               //Groud Speed Field19
            strFE += "1,";              //������Դ1=ACARS Field20
            strFE += "3600,";            //��һ�α��ķ���ʱ�䣺240��� Field21
            strFE += ",";               //ETA Field22
            #endregion

            #region ���Ŀ�Ļ���
            //���Ŀ�Ļ���
            //������������ת��������
            //��ɻ�����������ǰ���Ѿ�ȡ�ã��˴�ֻȡĿ�Ļ���������
            string strPOA = "";
            
            //Ŀ�Ļ���
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

            //��ɻ�����������ǰ���Ѿ�ȡ��
            strFE += strPOD + ",";      //Field23
            strFE += strPOA + ",";      //Field24
            #endregion

            strFE += strACType + ",";       //���ͣ�Field25
            strFE += ",,,";                 //ͨѶ��֤��ͨѶģʽ��ͨѶ���磬 Field26,27,28

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

            strFE += "1";           //FOB������λ��1=���������� Field30
            #endregion
            
            //��������ʱ�洢�����ݿ��У��Ա㶨ʱ����
            rvSF.Result = acarsMegsDAF.InsertFEMeg(strFE);
            return rvSF;
        }
        #endregion

        #region ����ACARS��OUT������FE��RTE��
        /// <summary>
        /// ����ACARS��OUT������FE��RTE��
        /// </summary>
        /// <param name="acarsMegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GenFERteMegs(ACARSMegsBM acarsMegsBM)
        {
            rvSF.Result = 1;

            //FE���Ĵ洢λ��
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);
            //FE�������к�sn
            int iFEMegsNo = 0;

            #region ��ȡ�������к�
            //��ȡ�������к�
            //��sn.txt
            FileStream fs = File.Open(strFEPath + "sn.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);

            //��ȡ��ǰ��sn
            string strSn = sr.ReadToEnd();
            iFEMegsNo = Convert.ToInt32(strSn);
            sr.Close();

            //�����ǰ�������кŴﵽ�����ֵ65535�������¿�ʼ����
            if (iFEMegsNo == 65535)
            {
                iFEMegsNo = 0;
            }
            //�����ڵ�ǰ�Ļ���������1
            else
            {
                iFEMegsNo++;
            }
            #endregion

            #region ��ͷ
            //���͵�FE�ı���
            string strFE = "1,";                        //Server Number Field1
            strFE += iFEMegsNo.ToString() + ",";        //�������к�    Field2

            //���������к�д��sn.txt
            fs = new FileStream(strFEPath + "sn.txt", FileMode.Open, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(iFEMegsNo);
            sw.Flush();
            sw.Close();

            //���ķ�������
            string strFltDate = acarsMegsBM.FlightDate.Replace("-", "");
            strFE += strFltDate + ",";                        //Field3                 
            //���ķ���ʱ��
            string strMegTime = acarsMegsBM.MessageSendTime.Substring(10).Trim().Replace(":", "");
            strFE += strMegTime + ",";                        //Field4
            //�������ͣ��������󣬷�����������
            strFE += "2,1,2,";                                //Field5,6,7
            //��������еĹ�˾���ִ���ת�����ִ���
            string strFltId = "CHH";
            //��HU������ĺ�����ʱ������
            if (acarsMegsBM.FLTID.Substring(0, 2) == "HU")
            {
                strFltId = acarsMegsBM.FLTID.Replace("HU", "CHH");
            }
            else
            {
                rvSF.Result = 1;
                return rvSF;
            }
            strFE += strFltId + ",";                          //����� Field8
            strFE += acarsMegsBM.LONG_REG + ",";              //�ɻ��� Field9

            //��ȡ��ɻ���
            string strPOD = "";
            DataTable dtAirportInfo = new DataTable();

            if (acarsMegsBM.DEPSTN != null)
            {
                dtAirportInfo = wsPublicService.GetAirportInfo("", acarsMegsBM.DEPSTN, "", "", "", "").Tables[0];
                if (dtAirportInfo.Rows.Count == 1)
                {
                    strPOD = dtAirportInfo.Rows[0]["AirportFourCode"].ToString();
                }
                strFE += strPOD + ",";                       //��ɻ��� Field10
            }
            else
            {
                strFE += ",";
            }

            //��ȡ�ƻ�����
            if (acarsMegsBM.DATOP != null)
            {
                strFE += acarsMegsBM.DATOP.Replace("-", "") + ",";        //������� Field11
            }
            else
            {
                strFE += ",";
            }

            //��ȡOUTʱ��
            if (acarsMegsBM.ACARSOUT != null)
            {
                strFE += acarsMegsBM.ACARSOUT.Substring(0, 2) + ",";      //���ʱ�䣺ֱ��ȡOUTʱ�� Field12
            }
            else
            {
                strFE += "00,";
            }
            #endregion

            #region ������
            strFE += strFltDate + ",";        //�������� Field13
            strFE += strMegTime + ",";        //����ʱ�� Field14
            strFE += "3,";                    //��·��ʽ Field15

            #region ���ɺ�·
            //��ȡ��·
            string strRoute = "\"";

            //������ʱ�޷��������żƻ����Ա���-����ͼ������ͼ-�����ĺ�·�ֹ�д��
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

                //����Web�����к���ʱ�̣�ʹ�ñ���ʱ����ACARS�����е�ʱ��ת�ɱ���ʱ
                DateTime dtFltDate = Convert.ToDateTime(acarsMegsBM.MessageSendTime).ToLocalTime();
                DataSet ds = wsCFP.ReturnInfoOfFPL(dtFltDate, strFlightId, acarsMegsBM.LONG_REG.Substring(1), acarsMegsBM.DEPSTN, acarsMegsBM.ARRSTN);

                string strLat = "";
                string strLon = "";
                if (ds != null)
                {
                    DataTable dt = ds.Tables["���ݼ�_��·����Ϣ"];
                    if (dt != null)
                    {
                        //��ȡ���к�·�����Ϣ
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //γ��
                            strLat = dt.Rows[i]["γ��"].ToString();
                            int iIndexLat = strLat.IndexOf(".");
                            int iLength = strLat.Length - iIndexLat;

                            //���γ�Ⱥ�����С����
                            if (iIndexLat > 0)
                            {
                                //��γ�����ݳ���������С�������λ
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

                            //����
                            strLon = dt.Rows[i]["����"].ToString();
                            iIndexLat = strLon.IndexOf(".");
                            iLength = strLon.Length - iIndexLat;

                            //������Ⱥ�����С����
                            if (iIndexLat > 0)
                            {
                                //��γ�����ݳ���������С�������λ
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
                            strRoute += strLat + "/" + strLon + "/" + dt.Rows[i]["��·��"].ToString() + ",";
                        }
                    }
                }
                //ȥ�����һ����������
                strRoute = strRoute.Substring(0, strRoute.Length - 1);
            }
            strRoute += "\"";

            strFE += strRoute + ",";          //��·���� Field16
            #endregion

            strFE += "1,";                    //Route extent Field17
            strFE += "4";                     //Route type Field18
            #endregion

            //��������ʱ�洢�����ݿ��У��Ա㶨ʱ����
            rvSF.Result = acarsMegsDAF.InsertFEMeg(strFE);
            return rvSF;
        }
        #endregion

        #region ����3PD�ļ�
        public bool Gen3PDFile(string strFileName)
        {
            //FE���Ĵ洢λ��
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);

            //��ȡ��һ����ȡ�����һ��FE��¼���
            FileStream fs = new FileStream(strFEPath + "MaxNo.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);
            string strMaxNoOld = sr.ReadToEnd();
            sr.Close();
            int iMaxNoOld = Convert.ToInt32(strMaxNoOld);

            //��ѯ����һ����ȡ���ĵ��������ɵ������±���
            DataTable dt = acarsMegsDAF.GetFEMegs(iMaxNoOld);

            //��ȡ������ȡ���ļ�¼����
            int iMegsCount = 0;
            //ʹ��try-catch�����򷵻ؽ��dtΪnull��ɴ���
            try
            {
                iMegsCount = dt.Rows.Count;
            }
            catch
            {
                iMegsCount = 0;
            }

            //�������û����ȡ���κ����ݣ�����false
            if (iMegsCount == 0)
            {
                return false;
            }
            else
            {
                //����3PD�ļ�
                int iMaxNo = 0;
                string str3PD = "3PDI2.0\r\n";

                //��ȡ��������
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str3PD += dt.Rows[i]["cntFEMegContent"].ToString();
                    str3PD += "\r\n";
                }

                //����������д������͵�3PD�ļ���
                //fs = new FileStream(strFEPath + strFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                //StreamWriter sw = new StreamWriter(fs);
                //sw.Write(str3PD);
                //sw.Flush;
                //sw.Close();
                File.WriteAllText(strFEPath + strFileName, str3PD);

                //��ȡ���λ�ȡ���ļ�¼id���ֵ
                iMaxNo = Convert.ToInt32(dt.Rows[iMegsCount - 1]["iPK"].ToString());

                //����¼д��MaxNo�ļ��б���
                File.WriteAllText(strFEPath + "MaxNo.txt", iMaxNo.ToString());

                return true;
            }
        }
        #endregion

        #region ����HBT��
        public void GenHBTMeg(string strFileName)
        {
            //FE���Ĵ洢λ��
            string strFEPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("FEMegsPath", 0);
            //FE�������к�sn
            int iMaxSn = 0;

            //��ȡ�������к�
            //��sn.txt
            FileStream fs = File.Open(strFEPath + "Sn.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);

            //��ȡ��ǰ��sn
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

            //����HBT��������
            string str3PD = "3PDI2.0\r\n";
            str3PD += "1," + iMaxSn + "," + DateTime.Now.ToString("yyyyMMdd") + "," + DateTime.Now.ToString("HHmmss") + ",6,,,,,,,\r\n";

            File.WriteAllText(strFEPath + strFileName, str3PD);
            File.WriteAllText(strFEPath + "Sn.txt", iMaxSn.ToString());
        }
        #endregion

        #region ͨ��FTP�ϴ��ļ�
        /// <summary>
        /// ͨ��FTP�ϴ��ļ�
        /// </summary>
        /// <param name="strFtpServer">��������ַ</param>
        /// <param name="strFtpUser">�û���</param>
        /// <param name="strFtpPass">����</param>
        /// <param name="strFile">���ϴ��ļ���·��</param>
        /// <returns>�Ƿ��ϴ��ɹ�</returns>
        public bool FtpFiles(string strFtpServer, string strFtpUser, string strFtpPass, string strFile)
        {
            FtpWebRequest ftpReq;
            //�����ļ�����
            FileInfo file3PD = new FileInfo(strFile);
            //Uri
            string strUri = "ftp://" + strFtpServer + "/" + file3PD.Name;

            //����FtpWebRequest����
            ftpReq = (FtpWebRequest)FtpWebRequest.Create(new Uri(strUri));

            //��������
            //�û�������
            ftpReq.Credentials = new NetworkCredential(strFtpUser, strFtpPass);
            //�����������������
            ftpReq.KeepAlive = true;
            //ָ��ִ�е�����
            ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
            //�Զ����Ʒ�ʽ����
            ftpReq.UseBinary = true;
            //��Passive��ʽ����
            ftpReq.UsePassive = true;

            //ָ����������С2kb
            int iBufferLen = 2048;
            //�½�������
            byte[] buff = new byte[iBufferLen];
            int iContentLen;

            //���ļ�����ȡҪ�ϴ����ļ�
            FileStream fs = file3PD.OpenRead();

            try
            {
                //�½������ڽ��ϴ��ļ�����д��������ϵ��ļ�
                Stream stm = ftpReq.GetRequestStream();

                //���ϴ��ļ�����д��ָ�������������ر��ζ�ȡ�����ݳ���
                iContentLen = fs.Read(buff, 0, iBufferLen);

                //����ϴ��ļ���û�н���
                while (iContentLen != 0)
                {
                    //��ָ�������������ݴ��ϴ��ļ���file streamд���ϴ�����������stream
                    stm.Write(buff, 0, iContentLen);

                    //���ϴ��ļ�����д��ָ�������������ر��ζ�ȡ�����ݳ���
                    iContentLen = fs.Read(buff, 0, iBufferLen);
                }

                //�ر���
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
