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
    /// 航班保障信息数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-31
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeInforDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GuaranteeInforDAF()
        {
        }

        #region 以主键为条件查询一条记录
        /// <summary>
        /// 以主键为条件查询一条记录
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <returns></returns>
        public DataTable GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            //打开数据库连接
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

        #region 获取某席位当天的所有航班
        /// <summary>
        /// 获取某席位当天的所有航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="positionNameBM">席位名称实体对象</param>
        /// <returns>该席位的所有航班</returns>
        public DataTable GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //定义返回值
            DataTable dtPositionFlights = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();

            try
            {
                //打开数据库连接
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

        #region 获取某航站的进出港航班
        /// <summary>
        /// 获取某航站的进出港航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="stationBM">席位名称实体对象</param>
        /// <returns>该航站的所有航班</returns>
        public DataTable GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //定义返回值
            DataTable dtStationFlights = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();

            try
            {
                //打开数据库连接
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

        #region 更新某项保障内容
        /// <summary>
        /// 更新某项保障内容
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateGuaranteeInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //定义返回值
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 同时更新多条数据
        /// <summary>
        /// 同时更新多条数据
        /// </summary>
        /// <param name="ilMaintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateGuaranteeInforList(IList ilMaintenGuaranteeInforBM)
        {
            //定义返回值
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 更新某项航班动态内容
        /// <summary>
        /// 更新某项航班动态内容
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateLegsInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //定义返回值
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取当天所有的航班动态
        /// <summary>
        /// 获取当天所有的航班动态
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetAllLegsByDay(DateTimeBM dateTimeBM)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取航班的值机信息
        /// <summary>
        /// 获取航班的值机信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetCheckInfor(ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 更新旅客值机信息
        /// <summary>
        /// 更新旅客值机信息
        /// </summary>
        /// <param name="checkPaxBM"></param>
        /// <returns></returns>
        public int UpdateCheckPaxInfor(CheckPaxBM checkPaxBM)
        {
            //定义返回值
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取航班的值机信息
        /// <summary>
        /// 获取航班的值机信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetPaxNameList(ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 更新旅客名单信息
        /// <summary>
        /// 更新旅客名单信息
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public int UpdatePaxNameList(PaxNameListBM paxNameListBM)
        {
            //定义返回值
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取中转连程旅客信息
        /// <summary>
        /// 获取中转连程旅客信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetTrasitPaxList(ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 更新中转旅客信息
        /// <summary>
        /// 更新中转旅客信息
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public int UpdateTrasitPax(TrasitPaxBM trasitPaxBM)
        {
            //定义返回值
            int retVal = -1;
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 航后机位安排动态
        /// <summary>
        /// 航后机位安排动态
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetEndFlight(DateTimeBM endDateTimeBM, DateTimeBM startDateTime, AccountBM accountBM)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取某驾飞机当天所飞的所有航段
        /// <summary>
        /// 获取某驾飞机当天所飞的所有航段
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strLONG_REG"></param>
        /// <returns></returns>
        public DataTable GetAircraftFlights(DateTimeBM dateTimeBM, string strLONG_REG)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取航站进出港航班
        /// <summary>
        /// 获取航站进出港航班
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <returns></returns>
        public DataTable GetStationFlight(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 根据航班号和起飞目的机场查询航班计划
        /// <summary>
        /// 根据航班号和起飞目的机场查询航班计划
        /// </summary>
        /// <param name="dateTimeBM">事件范围</param>
        /// <param name="strDEPSTN">起飞机场三字码</param>
        /// <param name="strARRSTN">目的机场三字码</param>
        /// <param name="strFlightNo">航班号</param>
        /// <returns></returns>
        public DataTable GetFlightsByFlightNo(DateTimeBM dateTimeBM, string strDEPSTN, string strARRSTN, string strFlightNo)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取航班连飞信息
        /// <summary>
        /// 获取航班连飞信息
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int GetJoinFlightNo(DateTimeBM dateTimeBM, AccountBM accountBM)
        {
            //定义返回值 
            int retVal = 1;
            //获取所有航站信息
            StationDA stationDA = new StationDA();
            DataTable dtStation = new DataTable();
            try
            {
                //打开数据库连接
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
            //分别获取各航站进出港航班动态
            foreach (DataRow drStation in dtStation.Rows)
            {
                StationBM stationBM = new StationBM(drStation);
                GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
                ChangeRecordDA changeRecordDA = new ChangeRecordDA();
                CrewDA crewDA = new CrewDA();
                try
                {
                    //打开数据库连接
                    guaranteeInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                    //查询所有航班
                    DataTable dtStationFlight = new DataTable();
                    dtStationFlight = guaranteeInforDA.GetStationFlight(dateTimeBM, stationBM);
                    //查询航站所有的出港航班
                    DataRow[] outStationFlight = dtStationFlight.Select("cncDEPSTN = '" + stationBM.ThreeCode + "'");
                    //打开数据库连接
                    //crewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Flt_Dept, 1));
                    crewDA.GetConnOpen(ConfigurationManager.AppSettings["CrewInfo"]);
                    foreach (DataRow drOutFlight in outStationFlight)
                    {
                        //出港航班机组信息
                        string strOutFlightDate = drOutFlight["cncFlightDate"].ToString();
                        string strOutFltId = drOutFlight["cnvcFLTID"].ToString();
                        string strOutDepStn = drOutFlight["cncDEPSTN"].ToString();
                        string strOutArrStn = drOutFlight["cncARRSTN"].ToString();
                        //航段
                        string strSector = strOutDepStn + "-" + strOutArrStn;
                        //根据出港航班信息查询机组信息
                        DataTable dtOutCrewInfor = crewDA.GetCrewInforByFlightNo(strOutFlightDate, strOutFltId.Substring(3), strSector);
                        //出港机长的进航航班信息
                        if (dtOutCrewInfor.Rows.Count > 0)
                        {
                            //机长工号
                            string strStaffId = dtOutCrewInfor.Rows[0]["STAFFID"].ToString();
                            //出港航班起飞时间
                            string strDepTime = dtOutCrewInfor.Rows[0]["DEPARTTIME"].ToString();
                            //string aa = "-" + drOutFlight["cncDEPSTN"].ToString() + "-" + drOutFlight["cncARRSTN"].ToString();
                            //DataTable dtCaptainInFlightInfor = crewDA.GetCrewInforByCaptain(drOutFlight["cncFlightDate"].ToString(), "-" + drOutFlight["cncDEPSTN"].ToString(), aa, dtOutCrewInfor.Rows[0]["AF_CAPTAIN"].ToString(), dtOutCrewInfor.Rows[0]["AF_BEGIN"].ToString(), dtOutCrewInfor.Rows[0]["AF_END"].ToString());
                            //查询机长执行的上一个航班的信息
                            DataTable dtCaptainInFlightInfor = crewDA.GetCrewInforByCaptain(strOutFlightDate, strStaffId,  "-" + strOutDepStn, strDepTime);
                            //如果有机长的进港信息
                            //foreach (DataRow drCaptain in dtCaptainInFlightInfor.Rows)
                            //{
                            //    //分析进港航班的航班号和起飞目的机场
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
                            //    //进港航班信息
                            //    string strSearch = "(";
                            //    for (int iLoop = 0; iLoop < strFlightNo.Length; iLoop++)
                            //    {
                            //        strSearch += "cnvcFLTID like '%" + strFlightNo[iLoop] + "%' OR ";
                            //    }
                            //如果有机长的进港信息
                            if(dtCaptainInFlightInfor.Rows.Count > 0)
                            {
                                //进港航班的航班日期
                                string strInFlightDate = dtCaptainInFlightInfor.Rows[0]["DATE"].ToString();
                                //航段
                                string strInSector = dtCaptainInFlightInfor.Rows[0]["SECTOR"].ToString();
                                //起飞机场
                                string strInDepStn = strInSector.Substring(0, 3);
                                //目的机场
                                string strInArrStn = strInSector.Substring(strInSector.IndexOf("-") + 1);
                                //航班号，去掉公司二字码
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
                                        //维护信息
                                        MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                                        maintenGuaranteeInforBM.DATOP = drOutFlight["cncDATOP"].ToString();
                                        maintenGuaranteeInforBM.FLTID = drOutFlight["cnvcFLTID"].ToString();
                                        maintenGuaranteeInforBM.LEGNO = drOutFlight["cniLEGNO"].ToString();
                                        maintenGuaranteeInforBM.AC = drOutFlight["cnvcAC"].ToString();
                                        maintenGuaranteeInforBM.FieldName = "cnvcJoinFlight";
                                        maintenGuaranteeInforBM.FieldType = 1;
                                        maintenGuaranteeInforBM.NewContent = strInFlightNo;
                                        guaranteeInforDA.UpdateGuaranteeInfor(maintenGuaranteeInforBM);

                                        //变更信息
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
                                        //插入变更信息

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

        #region 按机型统计出港旅客人数
        /// <summary>
        /// 按机型统计出港旅客人数
        /// </summary>
        /// <param name="dateTimeBM">时间范围</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns></returns>
        public DataTable GetStatisticPax(DateTimeBM dateTimeBM, string strDEPSTN)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 根据当地时间获取当天的航班动态
        /// <summary>
        /// 根据当地时间获取当天的航班动态
        /// </summary>
        /// <param name="strFlightDate">航班日期</param>
        /// <returns></returns>
        public DataTable GetLegsByFlightDate(string strStartFlightDate, string strEndFlightDate)
        {
            //定义返回值
            DataTable dtFlights = new DataTable();
            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();
            try
            {
                //打开数据库连接
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

        #region 获取某个时间段内所有的航班（vw_Legs） --added in 2009.10.27
        /// <summary>
        /// 获取某个时间段内所有的航班（vw_Legs）
        /// </summary>
        /// <param name="DateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetAllvw_LegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            GuaranteeInforDA guaranteeInforDA = new GuaranteeInforDA();

            //打开数据库连接
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
