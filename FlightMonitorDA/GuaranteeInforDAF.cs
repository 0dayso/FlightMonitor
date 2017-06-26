using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;
using System.Collections;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ���ౣ����Ϣ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-31
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeInforDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public GuaranteeInforDAF()
        {
        }

        #region ������Ϊ������ѯһ����¼
        /// <summary>
        /// ������Ϊ������ѯһ����¼
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <returns></returns>
        public DataTable GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            //�����ݿ�����
            guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = guaranteeInforDA.GetFlightByKey(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dataTable;
        }
        #endregion

        #region ��ȡĳϯλ��������к���
        /// <summary>
        /// ��ȡĳϯλ��������к���
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="positionNameBM">ϯλ����ʵ�����</param>
        /// <returns>��ϯλ�����к���</returns>
        public DataTable GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //���巵��ֵ
            DataTable dtPositionFlights = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();

            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtPositionFlights = guaranteeInforDA.GetFlightsByPosition(dateTimeBM, positionNameBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }

            return dtPositionFlights;
        }
        #endregion

        #region ��ȡĳ��վ�Ľ����ۺ���
        /// <summary>
        /// ��ȡĳ��վ�Ľ����ۺ���
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="stationBM">ϯλ����ʵ�����</param>
        /// <returns>�ú�վ�����к���</returns>
        public DataTable GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //���巵��ֵ
            DataTable dtStationFlights = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();

            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtStationFlights = guaranteeInforDA.GetFlightsByStation(dateTimeBM, stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }

            return dtStationFlights;
        }
        #endregion

        #region ����ĳ�������
        /// <summary>
        /// ����ĳ�������
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateGuaranteeInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //���巵��ֵ
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = guaranteeInforDA.UpdateGuaranteeInfor(maintenGuaranteeInforBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ͬʱ���¶�������
        /// <summary>
        /// ͬʱ���¶�������
        /// </summary>
        /// <param name="ilMaintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateGuaranteeInforList(IList ilMaintenGuaranteeInforBM)
        {
            //���巵��ֵ
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                IEnumerator ieMaintenGuaranteeInforBM = ilMaintenGuaranteeInforBM.GetEnumerator();
                while (ieMaintenGuaranteeInforBM.MoveNext())
                {
                    MaintenGuaranteeInforBM objMaintenGuaranteeInforBM = (MaintenGuaranteeInforBM)ieMaintenGuaranteeInforBM.Current;
                    retVal = guaranteeInforDA.UpdateGuaranteeInfor(objMaintenGuaranteeInforBM);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ����ĳ��ද̬����
        /// <summary>
        /// ����ĳ��ද̬����
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateLegsInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //���巵��ֵ
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = guaranteeInforDA.UpdateLegsInfor(maintenGuaranteeInforBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ��ȡ�������еĺ��ද̬
        /// <summary>
        /// ��ȡ�������еĺ��ද̬
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetAllLegsByDay(DateTimeBM dateTimeBM)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetAllLegsByDay(dateTimeBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region ��ȡ�����ֵ����Ϣ
        /// <summary>
        /// ��ȡ�����ֵ����Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetCheckInfor(ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetCheckInfor(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region �����ÿ�ֵ����Ϣ
        /// <summary>
        /// �����ÿ�ֵ����Ϣ
        /// </summary>
        /// <param name="checkPaxBM"></param>
        /// <returns></returns>
        public int UpdateCheckPaxInfor(CheckPaxBM checkPaxBM)
        {
            //���巵��ֵ
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = guaranteeInforDA.UpdateCheckPaxInfor(checkPaxBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }

            return retVal;
        }
        #endregion

        #region ��ȡ�����ֵ����Ϣ
        /// <summary>
        /// ��ȡ�����ֵ����Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetPaxNameList(ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetPaxNameList(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region �����ÿ�������Ϣ
        /// <summary>
        /// �����ÿ�������Ϣ
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public int UpdatePaxNameList(PaxNameListBM paxNameListBM)
        {
            //���巵��ֵ
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = guaranteeInforDA.UpdatePaxNameList(paxNameListBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ��ȡ��ת�����ÿ���Ϣ
        /// <summary>
        /// ��ȡ��ת�����ÿ���Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetTrasitPaxList(ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetTrasitPaxList(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region ������ת�ÿ���Ϣ
        /// <summary>
        /// ������ת�ÿ���Ϣ
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public int UpdateTrasitPax(TrasitPaxBM trasitPaxBM)
        {
            //���巵��ֵ
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = guaranteeInforDA.UpdateTrasitPax(trasitPaxBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �����λ���Ŷ�̬
        /// <summary>
        /// �����λ���Ŷ�̬
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetEndFlight(DateTimeBM endDateTimeBM, DateTimeBM startDateTime, AccountBM accountBM)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetEndFlight(endDateTimeBM, startDateTime, accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }

            return dtFlights;
        }
        #endregion

        #region ��ȡĳ�ݷɻ��������ɵ����к���
        /// <summary>
        /// ��ȡĳ�ݷɻ��������ɵ����к���
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strLONG_REG"></param>
        /// <returns></returns>
        public DataTable GetAircraftFlights(DateTimeBM dateTimeBM, string strLONG_REG)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetAircraftFlights(dateTimeBM, strLONG_REG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region ��ȡ��վ�����ۺ���
        /// <summary>
        /// ��ȡ��վ�����ۺ���
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <returns></returns>
        public DataTable GetStationFlight(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetStationFlight(dateTimeBM, stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region ���ݺ���ź����Ŀ�Ļ�����ѯ����ƻ�
        /// <summary>
        /// ���ݺ���ź����Ŀ�Ļ�����ѯ����ƻ�
        /// </summary>
        /// <param name="dateTimeBM">�¼���Χ</param>
        /// <param name="strDEPSTN">��ɻ���������</param>
        /// <param name="strARRSTN">Ŀ�Ļ���������</param>
        /// <param name="strFlightNo">�����</param>
        /// <returns></returns>
        public DataTable GetFlightsByFlightNo(DateTimeBM dateTimeBM, string strDEPSTN, string strARRSTN, string strFlightNo)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetFlightsByFlightNo(dateTimeBM, strDEPSTN, strARRSTN, strFlightNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region ��ȡ����������Ϣ
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int GetJoinFlightNo(DateTimeBM dateTimeBM, AccountBM accountBM)
        {
            //���巵��ֵ 
            int retVal = 1;
            //��ȡ���к�վ��Ϣ
            StationDA stationDA = new StationDA();
            DataTable dtStation = new DataTable();
            try
            {
                //�����ݿ�����
                stationDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtStation = stationDA.GetAllStation();
            }
            catch (Exception ex)
            {
                retVal = -1;
                throw ex;                
            }
            finally
            {
                stationDA.ConnClose();
            }
            //�ֱ��ȡ����վ�����ۺ��ද̬
            foreach (DataRow drStation in dtStation.Rows)
            {
                StationBM stationBM = new StationBM(drStation);
                GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
                ChangeRecordDA changeRecordDA = new ChangeRecordDA();
                CrewDA crewDA = new CrewDA();
                try
                {
                    //�����ݿ�����
                    guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                    //��ѯ���к���
                    DataTable dtStationFlight = new DataTable();
                    dtStationFlight = guaranteeInforDA.GetStationFlight(dateTimeBM, stationBM);
                    //��ѯ��վ���еĳ��ۺ���
                    DataRow[] outStationFlight = dtStationFlight.Select("cncDEPSTN = '" + stationBM.ThreeCode + "'");
                    //�����ݿ�����
                    //crewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Flt_Dept, 1));
                    crewDA.GetConnOpen(ConfigurationManager.AppSettings["CrewInfo"]);
                    foreach (DataRow drOutFlight in outStationFlight)
                    {
                        //���ۺ��������Ϣ
                        string strOutFlightDate = drOutFlight["cncFlightDate"].ToString();
                        string strOutFltId = drOutFlight["cnvcFLTID"].ToString();
                        string strOutDepStn = drOutFlight["cncDEPSTN"].ToString();
                        string strOutArrStn = drOutFlight["cncARRSTN"].ToString();
                        //����
                        string strSector = strOutDepStn + "-" + strOutArrStn;
                        //���ݳ��ۺ�����Ϣ��ѯ������Ϣ
                        DataTable dtOutCrewInfor = crewDA.GetCrewInforByFlightNo(strOutFlightDate, strOutFltId.Substring(3), strSector);
                        //���ۻ����Ľ���������Ϣ
                        if (dtOutCrewInfor.Rows.Count > 0)
                        {
                            //��������
                            string strStaffId = dtOutCrewInfor.Rows[0]["STAFFID"].ToString();
                            //���ۺ������ʱ��
                            string strDepTime = dtOutCrewInfor.Rows[0]["DEPARTTIME"].ToString();
                            //string aa = "-" + drOutFlight["cncDEPSTN"].ToString() + "-" + drOutFlight["cncARRSTN"].ToString();
                            //DataTable dtCaptainInFlightInfor = crewDA.GetCrewInforByCaptain(drOutFlight["cncFlightDate"].ToString(), "-" + drOutFlight["cncDEPSTN"].ToString(), aa, dtOutCrewInfor.Rows[0]["AF_CAPTAIN"].ToString(), dtOutCrewInfor.Rows[0]["AF_BEGIN"].ToString(), dtOutCrewInfor.Rows[0]["AF_END"].ToString());
                            //��ѯ����ִ�е���һ���������Ϣ
                            DataTable dtCaptainInFlightInfor = crewDA.GetCrewInforByCaptain(strOutFlightDate, strStaffId,  "-" + strOutDepStn, strDepTime);
                            //����л����Ľ�����Ϣ
                            //foreach (DataRow drCaptain in dtCaptainInFlightInfor.Rows)
                            //{
                            //    //�������ۺ���ĺ���ź����Ŀ�Ļ���
                            //    int iIndex = 0;
                            //    string[] strFlightNo = drCaptain["AP_NUMBER"].ToString().Substring(2).Split('/');
                            //    string[] strStation = drCaptain["AF_SEG"].ToString().Split('-');
                            //    for (int iLoop = 1; iLoop < strStation.Length; iLoop++)
                            //    {
                            //        if (strStation[iLoop] == drOutFlight["cncDEPSTN"].ToString())
                            //        {
                            //            iIndex = iLoop;
                            //            break;
                            //        }
                            //    }
                            //    //���ۺ�����Ϣ
                            //    string strSearch = "(";
                            //    for (int iLoop = 0; iLoop < strFlightNo.Length; iLoop++)
                            //    {
                            //        strSearch += "cnvcFLTID like '%" + strFlightNo[iLoop] + "%' OR ";
                            //    }
                            //����л����Ľ�����Ϣ
                            if(dtCaptainInFlightInfor.Rows.Count > 0)
                            {
                                //���ۺ���ĺ�������
                                string strInFlightDate = dtCaptainInFlightInfor.Rows[0]["DATE"].ToString();
                                //����
                                string strInSector = dtCaptainInFlightInfor.Rows[0]["SECTOR"].ToString();
                                //��ɻ���
                                string strInDepStn = strInSector.Substring(0, 3);
                                //Ŀ�Ļ���
                                string strInArrStn = strInSector.Substring(strInSector.IndexOf("-") + 1);
                                //����ţ�ȥ����˾������
                                string strInFltNo = dtCaptainInFlightInfor.Rows[0]["FLIGHTNO"].ToString().Trim().Substring(2);

                                StringBuilder strSearch = new StringBuilder();
                                strSearch.Append("cnvcFLTID like '%" + strInFltNo + "%'");
                                strSearch.Append(" AND cncFlightDate = '" + strInFlightDate + "'");
                                strSearch.Append(" AND cncDEPSTN = '" + strInDepStn + "'");
                                strSearch.Append(" AND cncARRSTN = '" + strInArrStn + "'");

                                //strSearch = strSearch.Substring(0, strSearch.Length - 3) + ") AND ";
                                //strSearch = strSearch + "cncFlightDate = '" + DateTime.Parse(dtCaptainInFlightInfor.Rows[0]["AF_DATE"].ToString()).ToString("yyyy-MM-dd") + "' AND " +
                                //    " cncDEPSTN = '" + strStation[iIndex - 1] + "' AND " +
                                //    " cncARRSTN = '" + strStation[iIndex] + "' AND " +
                                //    " cncDEPSTN <> '" + drOutFlight["cncDEPSTN"].ToString() + "' AND " +
                                //    " cncARRSTN <> '" + drOutFlight["cncARRSTN"].ToString() + "' AND " +
                                //    " cncAllETD < '" + drOutFlight["cncAllETD"].ToString() + "'";

                                DataRow[] drInFlightInfor = dtStationFlight.Select(strSearch.ToString(), "cncAllETD desc");

                                if (drInFlightInfor.Length > 0)
                                {
                                    if (DateTime.Parse(drInFlightInfor[0]["cncAllETD"].ToString()) < DateTime.Parse(drOutFlight["cncAllETD"].ToString()) && DateTime.Parse(drInFlightInfor[0]["cncAllETA"].ToString()).AddMinutes(stationBM.JoinTimeLine) > DateTime.Parse(drOutFlight["cncAllETD"].ToString()) && drOutFlight["cnvcJoinFlight"].ToString() != drInFlightInfor[0]["cnvcFLTID"].ToString().Substring(3))
                                    {
                                        string strInFlightNo = drInFlightInfor[0]["cnvcFLTID"].ToString().Substring(3);
                                        //ά����Ϣ
                                        MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                                        maintenGuaranteeInforBM.DATOP = drOutFlight["cncDATOP"].ToString();
                                        maintenGuaranteeInforBM.FLTID = drOutFlight["cnvcFLTID"].ToString();
                                        maintenGuaranteeInforBM.LEGNO = drOutFlight["cniLEGNO"].ToString();
                                        maintenGuaranteeInforBM.AC = drOutFlight["cnvcAC"].ToString();
                                        maintenGuaranteeInforBM.FieldName = "cnvcJoinFlight";
                                        maintenGuaranteeInforBM.FieldType = 1;
                                        maintenGuaranteeInforBM.NewContent = strInFlightNo;
                                        guaranteeInforDA.UpdateGuaranteeInfor(maintenGuaranteeInforBM);

                                        //�����Ϣ
                                        ChangeRecordBM changeRecordBM = new ChangeRecordBM();
                                        changeRecordBM.UserID = accountBM.UserId;
                                        changeRecordBM.OldDATOP = drOutFlight["cncDATOP"].ToString();
                                        changeRecordBM.OldFLTID = drOutFlight["cnvcFLTID"].ToString();
                                        changeRecordBM.OldLegNo = Convert.ToInt32(drOutFlight["cniLEGNO"].ToString());
                                        changeRecordBM.OldAC = drOutFlight["cnvcAC"].ToString();
                                        changeRecordBM.NewDATOP = drOutFlight["cncDATOP"].ToString();
                                        changeRecordBM.NewFLTID = drOutFlight["cnvcFLTID"].ToString();
                                        changeRecordBM.NewLegNo = Convert.ToInt32(drOutFlight["cniLEGNO"].ToString());
                                        changeRecordBM.NewAC = drOutFlight["cnvcAC"].ToString();
                                        changeRecordBM.OldDepSTN = drOutFlight["cncDEPSTN"].ToString();
                                        changeRecordBM.OldArrSTN = drOutFlight["cncARRSTN"].ToString();
                                        changeRecordBM.NewDepSTN = drOutFlight["cncDEPSTN"].ToString();
                                        changeRecordBM.NewArrSTN = drOutFlight["cncARRSTN"].ToString();
                                        changeRecordBM.STD = drOutFlight["cncAllSTD"].ToString();
                                        changeRecordBM.ETD = drOutFlight["cncAllETD"].ToString();
                                        changeRecordBM.STA = drOutFlight["cncAllSTA"].ToString();
                                        changeRecordBM.ETA = drOutFlight["cncAllETA"].ToString();
                                        changeRecordBM.ChangeReasonCode = "cnvcJoinFlight";
                                        changeRecordBM.ChangeOldContent = "";
                                        changeRecordBM.ChangeNewContent = strInFlightNo;
                                        changeRecordBM.ActionTag = "U";
                                        changeRecordBM.Refresh = 0;
                                        changeRecordBM.FOCOperatingTime = "";
                                        changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        //��������Ϣ

                                        changeRecordDA.SqlConn = guaranteeInforDA.SqlConn;
                                        changeRecordDA.Insert(changeRecordBM);
                                        changeRecordDA.ConnClose();
                                    }//if (DateTime.Parse(drInFlightInfor[0]["cncAllETD"].ToString()) < DateTime.Parse(drOutFlight["cncAllETD"].ToString()) && DateTime.Parse(drInFlightInfor[0]["cncAllETA"].ToString()).AddMinutes(stationBM.JoinTimeLine) > DateTime.Parse(drOutFlight["cncAllETD"].ToString()) && drOutFlight["cnvcJoinFlight"].ToString() != drInFlightInfor[0]["cnvcFLTID"].ToString().Substring(3))
                                }//if (drInFlightInfor.Length > 0)
                            }//foreach (DataRow drCaptain in dtCaptainInFlightInfor.Rows)
                        }//if (dtOutCrewInfor.Rows.Count > 0)
                    }//foreach (DataRow drOutFlight in outStationFlight)
                }
                catch (Exception ex)
                {
                    retVal = -1;
                    throw ex;
                }
                finally
                {
                    guaranteeInforDA.ConnClose();
                    crewDA.ConnClose();
                }
            }//foreach (DataRow drStation in dtStation.Rows)
            return retVal;
        }
        #endregion

        #region ������ͳ�Ƴ����ÿ�����
        /// <summary>
        /// ������ͳ�Ƴ����ÿ�����
        /// </summary>
        /// <param name="dateTimeBM">ʱ�䷶Χ</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns></returns>
        public DataTable GetStatisticPax(DateTimeBM dateTimeBM, string strDEPSTN)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetStatisticPax(dateTimeBM, strDEPSTN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion

        #region ���ݵ���ʱ���ȡ����ĺ��ද̬
        /// <summary>
        /// ���ݵ���ʱ���ȡ����ĺ��ද̬
        /// </summary>
        /// <param name="strFlightDate">��������</param>
        /// <returns></returns>
        public DataTable GetLegsByFlightDate(string strStartFlightDate, string strEndFlightDate)
        {
            //���巵��ֵ
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //�����ݿ�����
                guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtFlights = guaranteeInforDA.GetLegsByFlightDate(strStartFlightDate, strEndFlightDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dtFlights;
        }
        #endregion


        #region added by LinYong

        #region ��ȡĳ��ʱ��������еĺ��ࣨvw_Legs�� --added in 2009.10.27
        /// <summary>
        /// ��ȡĳ��ʱ��������еĺ��ࣨvw_Legs��
        /// </summary>
        /// <param name="DateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetAllvw_LegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();

            //�����ݿ�����
            guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = guaranteeInforDA.GetAllvw_LegsByDay(dateTimeBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeInforDA.ConnClose();
            }
            return dataTable;
        }
        #endregion

        #endregion

    }
}
