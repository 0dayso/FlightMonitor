using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航班保障信息数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-31
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeInforDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GuaranteeInforDA()
        {
        }

        #region  根据主键查询一条记录
        /// <summary>
        /// 以主键为条件查询航班SQL语句
        /// </summary>
        private const string SELECT_FlightByPriKey = "SELECT * FROM vw_Legs WHERE " +
            " cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLEGNO = @PARM_cniLEGNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// 组合主键参数
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <returns>组合参数</returns>
        private SqlParameter[] GetPriKeyParameters(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, "FlightPriKey");

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, "FlightPriKey", parms);
            }
            parms[0].Value = changeLegsBM.DATOP;
            parms[1].Value = changeLegsBM.FLTID;
            parms[2].Value = changeLegsBM.LEGNO;
            parms[3].Value = changeLegsBM.AC;
            return parms;
        }

        /// <summary>
        /// 以主键为条件查询一条记录
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <returns>符合内容的数据表</returns>
        public DataTable GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = GetPriKeyParameters(changeLegsBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByPriKey, parms);
        }
        #endregion

        #region 获取某席位当天所有的航班
        private const string SELECT_FlightsByPosition = "SELECT * FROM vw_Legs WHERE " +
            "cncETD >= @PARM_StartcncETD AND cncETD < @PARM_EndcncETD AND cniDeleteTag = 0 AND " +
            "cnvcLONG_REG IN (SELECT cnvcLONG_REG FROM tbPositionInfo WHERE cniPositionID = @PARM_cniPositionID) " +
            "ORDER BY cnvcLONG_REG, cncETD";

        private SqlParameter[] GetFlightsByPositionParameters(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightsByPosition);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartcncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndcncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cniPositionID", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_FlightsByPosition, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;
            parms[2].Value = positionNameBM.PositionID;

            return parms;
        }

        /// <summary>
        /// 获取某席位当天的所有航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="positionNameBM">席位名称实体对象</param>
        /// <returns>该席位的所有航班</returns>
        public DataTable GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = GetFlightsByPositionParameters(dateTimeBM, positionNameBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightsByPosition, parms);
        }
        #endregion


        #region 获取某航站当天所有的航班
        private const string SELECT_FlightsByStation = "SELECT * FROM vw_Legs WHERE " +
            "(cncETD >= @PARM_StartcncETD AND cncETD < @PARM_EndcncETD OR " +
            "cncETA >= @PARM_StartcncETD AND cncETA < @PARM_EndcncETD) AND cniDeleteTag = 0 AND cncSTATUS <> 'CNL' AND " +
            "(cncDEPSTN = @PARM_StationThreeCode OR cncARRSTN = @PARM_StationThreeCode) " +
            "ORDER BY  cnvcLONG_REG, cncETD";

        private SqlParameter[] SelectFlightsByStationParameters(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightsByStation);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartcncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndcncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_StationThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_FlightsByStation, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;
            parms[2].Value = stationBM.ThreeCode;

            return parms;
        }

        /// <summary>
        /// 获取某航站的进出港航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="stationBM">席位名称实体对象</param>
        /// <returns>该航站的所有航班</returns>
        public DataTable GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = SelectFlightsByStationParameters(dateTimeBM, stationBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightsByStation, parms);
        }

        #endregion

        #region 更新某项内容
        /// <summary>
        /// 更新某项保障内容
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateGuaranteeInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            string sqlUpdate;
            if (maintenGuaranteeInforBM.FieldType == 1)
            {
                sqlUpdate = "UPDATE tbGuaranteeInfor SET " +
                    maintenGuaranteeInforBM.FieldName + "='" + maintenGuaranteeInforBM.NewContent + "' " +
                    "WHERE cncDATOP = '" + maintenGuaranteeInforBM.DATOP + "' AND cnvcFLTID = '" + maintenGuaranteeInforBM.FLTID +
                    "' AND cniLegNO = " + maintenGuaranteeInforBM.LEGNO + " AND cnvcAC = '" + maintenGuaranteeInforBM.AC + "'";
            }
            else
            {
                sqlUpdate = "UPDATE tbGuaranteeInfor SET " +
                   maintenGuaranteeInforBM.FieldName + "=" + maintenGuaranteeInforBM.NewContent +
                   " WHERE cncDATOP = '" + maintenGuaranteeInforBM.DATOP + "' AND cnvcFLTID = '" + maintenGuaranteeInforBM.FLTID +
                   "' AND cniLegNO = " + maintenGuaranteeInforBM.LEGNO + " AND cnvcAC = '" + maintenGuaranteeInforBM.AC + "'";
            }

            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, sqlUpdate); 
        }
        #endregion
        
        #region 更新落地或起飞时间
        /// <summary>
        /// 更新某项航班动态内容
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public int UpdateLegsInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            string sqlUpdate;
            if (maintenGuaranteeInforBM.FieldType == 1)
            {
                sqlUpdate = "UPDATE tbLegs SET " +
                    maintenGuaranteeInforBM.FieldName + "='" + maintenGuaranteeInforBM.NewContent + "' " +
                    "WHERE cncDATOP = '" + maintenGuaranteeInforBM.DATOP + "' AND cnvcFLTID = '" + maintenGuaranteeInforBM.FLTID +
                    "' AND cniLegNO = " + maintenGuaranteeInforBM.LEGNO + " AND cnvcAC = '" + maintenGuaranteeInforBM.AC + "'";
            }
            else
            {
                sqlUpdate = "UPDATE tbLegs SET " +
                   maintenGuaranteeInforBM.FieldName + "=" + maintenGuaranteeInforBM.NewContent +
                   " WHERE cncDATOP = '" + maintenGuaranteeInforBM.DATOP + "' AND cnvcFLTID = '" + maintenGuaranteeInforBM.FLTID +
                   "' AND cniLegNO = " + maintenGuaranteeInforBM.LEGNO + " AND cnvcAC = '" + maintenGuaranteeInforBM.AC + "'";
            }

            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, sqlUpdate);
        }
        #endregion

        #region 获取当天所有的航班动态
        private const string SELECT_AllLegsByDay = "SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cncDEPSTN,cncARRSTN,cncSTD,cncETD,cncSTA,cncETA FROM tbLegs WHERE " +
            "cncETD >= @PARM_StartTime AND cncETD <= @PARM_EndTime";

        private SqlParameter[] GetAllLegsByDayParameters(DateTimeBM dateTimeBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_AllLegsByDay);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTime", SqlDbType.Char, 19)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_AllLegsByDay, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;

            return parms;
        }

        /// <summary>
        /// 获取当天所有的航班动态
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetAllLegsByDay(DateTimeBM dateTimeBM)
        {
            SqlParameter[] parms = GetAllLegsByDayParameters(dateTimeBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllLegsByDay, parms);
        }
        #endregion

        #region 获取某航班的值机信息
        private const string SELECT_CheckInfor = "SELECT cncDATOP,cnvcFLTID,cniLegNO,cnvcAC,cncCKIFlightDate,cnvcCKIFlightNo," +
            "cnvcBookNum,cntCheckInfo,cniCheckNum,cniXCRNum,cniAdultNum,cniChildNum,cniInfantNum,cniFirstClassNum,cniOfficialClassNum," +
            "cniTouristClassNum,cniAscendingPaxNum,cniBaggageWeight,cniBaggageNum,cniIsAutoSaveCheckPaxInfo FROM vw_Legs WHERE " +
            "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND cniLEGNO = @PARM_cniLEGNO AND cnvcAC = @PARM_cnvcAC";


        /// <summary>
        /// 获取航班的值机信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetCheckInfor(ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = GetPriKeyParameters(changeLegsBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CheckInfor, parms);
        }
        #endregion

        #region 更新航班值机信息
        private const string UPDATE_CheckInfor = "UPDATE tbGuaranteeInfor SET " + 
            "cnvcBookNum = @PARM_cnvcBookNum,"+
            "cntCheckInfo = @PARM_cntCheckInfo,"+
            "cniCheckNum = @PARM_cniCheckNum," +
            "cniXCRNum = @PARM_cniXCRNum,"+
            "cniAdultNum = @PARM_cniAdultNum,"+
            "cniChildNum = @PARM_cniChildNum,"+
            "cniInfantNum = @PARM_cniInfantNum," +
            "cniFirstClassNum = @PARM_cniFirstClassNum," +
            "cniOfficialClassNum = @PARM_cniOfficialClassNum," +
            "cniTouristClassNum = @PARM_cniTouristClassNum," +
            "cniAscendingPaxNum = @PARM_cniAscendingPaxNum," +
            "cniBaggageWeight = @PARM_cniBaggageWeight," +
            "cniBaggageNum = @PARM_cniBaggageNum," +
            "cniIsAutoSaveCheckPaxInfo = @PARM_cniIsAutoSaveCheckPaxInfo WHERE " +
            "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND cniLEGNO = @PARM_cniLEGNO AND cnvcAC = @PARM_cnvcAC";

        private SqlParameter[] UpdateCheckInforParameters(CheckPaxBM checkPaxBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_CheckInfor);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcBookNum", SqlDbType.NVarChar, 10),
                    new SqlParameter("@PARM_cntCheckInfo", SqlDbType.Text, 0),
                    new SqlParameter("@PARM_cniCheckNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniXCRNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniAdultNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniChildNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniInfantNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniFirstClassNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniOfficialClassNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniTouristClassNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniAscendingPaxNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniBaggageWeight", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniBaggageNum", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniIsAutoSaveCheckPaxInfo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                    
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_CheckInfor, parms);
            }

            parms[0].Value = checkPaxBM.BookNum;
            parms[1].Value = checkPaxBM.CheckInfo;
            parms[2].Value = checkPaxBM.CheckNum;
            parms[3].Value = checkPaxBM.XCRNum;
            parms[4].Value = checkPaxBM.AdultNum;
            parms[5].Value = checkPaxBM.ChildNum;
            parms[6].Value = checkPaxBM.InfantNum;
            parms[7].Value = checkPaxBM.FirstClassNum;
            parms[8].Value = checkPaxBM.OfficialClassNum;
            parms[9].Value = checkPaxBM.TouristClassNum;
            parms[10].Value = checkPaxBM.AscendingPaxNum;
            parms[11].Value = checkPaxBM.BaggageWeight;
            parms[12].Value = checkPaxBM.BaggageNum;
            parms[13].Value = checkPaxBM.IsAutoSaveCheckPaxInfo;
            parms[14].Value = checkPaxBM.DATOP;
            parms[15].Value = checkPaxBM.FLTID;
            parms[16].Value = checkPaxBM.LegNO;
            parms[17].Value = checkPaxBM.AC;

            return parms;
        }

        /// <summary>
        /// 更新旅客值机信息
        /// </summary>
        /// <param name="checkPaxBM"></param>
        /// <returns></returns>
        public int UpdateCheckPaxInfor(CheckPaxBM checkPaxBM)
        {
            SqlParameter[] parms = UpdateCheckInforParameters(checkPaxBM);

            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_CheckInfor, parms);
        }
        #endregion

        #region 旅客名单信息
        private const string SELECT_PaxNameList = "SELECT cncDATOP,cnvcFLTID,cniLegNO,cnvcAC,cncCKIFlightDate,cnvcCKIFlightNo," +
            "cntPaxNameList,cniIsAutoSaveCheckPaxNameList FROM vw_Legs WHERE " +
            "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND cniLEGNO = @PARM_cniLEGNO AND cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// 获取航班的值机信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetPaxNameList(ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = GetPriKeyParameters(changeLegsBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_PaxNameList, parms);
        }
        #endregion

        #region 更新旅客名单
        private const string UPDATE_PaxNameList = "UPDATE tbGuaranteeInfor SET " + 
            "cntPaxNameList = @PARM_cntPaxNameList," +
            "cniIsAutoSaveCheckPaxNameList = @PARM_cniIsAutoSaveCheckPaxNameList WHERE " +
            "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND cniLEGNO = @PARM_cniLEGNO AND cnvcAC = @PARM_cnvcAC";

        private SqlParameter[] UpdatePaxNameListParameters(PaxNameListBM paxNameListBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_PaxNameList);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cntPaxNameList", SqlDbType.Text, 0),
                    new SqlParameter("@PARM_cniIsAutoSaveCheckPaxNameList", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_PaxNameList, parms);
            }

            parms[0].Value = paxNameListBM.PaxNameList;
            parms[1].Value = paxNameListBM.IsAutoSaveCheckPaxNameList;
            parms[2].Value = paxNameListBM.DATOP;
            parms[3].Value = paxNameListBM.FLTID;
            parms[4].Value = paxNameListBM.LegNO;
            parms[5].Value = paxNameListBM.AC;

            return parms;
        }

        /// <summary>
        /// 更新旅客名单信息
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public int UpdatePaxNameList(PaxNameListBM paxNameListBM)
        {
            SqlParameter[] parms = UpdatePaxNameListParameters(paxNameListBM);

            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_PaxNameList, parms);
        }
        #endregion

        #region 中转旅客信息
        private const string SELECT_TrasitPax = "SELECT cncDATOP,cnvcFLTID,cniLegNO,cnvcAC,cncCKIFlightDate,cnvcCKIFlightNo," +
                   "cnbTransitPaxTag,cntTransitPax,cniIsAutoSaveTransitPax FROM vw_Legs WHERE " +
                   "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND cniLEGNO = @PARM_cniLEGNO AND cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// 获取航班中转连程旅客信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public DataTable GetTrasitPaxList(ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = GetPriKeyParameters(changeLegsBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_TrasitPax, parms);
        }
        #endregion

        #region 更新中转旅客信息
        private const string UPDATE_TransitPax = "UPDATE tbGuaranteeInfor SET " +
                   "cnbTransitPaxTag = @PARM_cnbTransitPaxTag," +
                   "cntTransitPax = @PARM_cntTransitPax," +
                   "cniIsAutoSaveTransitPax = @PARM_cniIsAutoSaveTransitPax WHERE " +
                   "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND cniLEGNO = @PARM_cniLEGNO AND cnvcAC = @PARM_cnvcAC";

        private SqlParameter[] UpdateTransetPaxParameters(TrasitPaxBM trasitPaxBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_TransitPax);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnbTransitPaxTag", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_cntTransitPax", SqlDbType.Text, 0),
                    new SqlParameter("@PARM_cniIsAutoSaveTransitPax", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_TransitPax, parms);
            }

            parms[0].Value = trasitPaxBM.TransitPaxTag;
            parms[1].Value = trasitPaxBM.TransitPax;
            parms[2].Value = trasitPaxBM.IsAutoSaveTransitPax;
            parms[3].Value = trasitPaxBM.DATOP;
            parms[4].Value = trasitPaxBM.FLTID;
            parms[5].Value = trasitPaxBM.LegNO;
            parms[6].Value = trasitPaxBM.AC;

            return parms;
        }

        /// <summary>
        /// 更新中转旅客信息
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public int UpdateTrasitPax(TrasitPaxBM trasitPaxBM)
        {
            SqlParameter[] parms = UpdateTransetPaxParameters(trasitPaxBM);

            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_TransitPax, parms);
        }
        #endregion

        #region 获取航后机位航班动态
        private const string SELECT_EndFlight = "SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cncFlightDate,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncSTA,cncTDWN,cnvcInGATE,cnvcGateRemark FROM vw_Legs WHERE " +
            "cncSTD >= @PARM_StartTime AND cncSTD <= @PARM_EndTime AND cncARRSTN = @PARM_ThreeCode AND cncSTATUS<> 'CNL' AND cniDeleteTag = 0 AND (NOT EXISTS " + 
            "(SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cnvcLONG_REG FROM tbLegs WHERE " +
            "cncSTD >= @PARM_StartTime AND cncSTD <= @PARM_EndTime AND cncDEPSTN = @PARM_ThreeCode AND cncSTATUS<> 'CNL'  AND cniDeleteTag = 0 AND " +
            "cnvcLONG_REG = vw_Legs.cnvcLONG_REG AND cncETD > vw_Legs.cncETD)) ORDER BY cnvcLONG_REG, cncTDWN";

        private const string SELECT_StartFlight = "SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cncFlightDate,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncSTA,cnvcOutGate,cnvcGateRemark FROM vw_Legs WHERE " +
            "cncSTD >= @PARM_StartTime AND cncSTD <= @PARM_EndTime AND cncDEPSTN = @PARM_ThreeCode AND cncSTATUS<> 'CNL' AND cniDeleteTag = 0 AND (NOT EXISTS " + 
            "(SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cnvcLONG_REG FROM tbLegs WHERE " +
            "cncSTD >= @PARM_StartTime AND cncSTD <= @PARM_EndTime AND cncARRSTN = @PARM_ThreeCode AND cncSTATUS<> 'CNL' AND cniDeleteTag = 0 AND " +
            "cnvcLONG_REG = vw_Legs.cnvcLONG_REG AND cncETD < vw_Legs.cncETD)) ORDER BY cnvcLONG_REG, cncSTD";

        private SqlParameter[] GetEndFlightParameters(DateTimeBM dateTimeBM, AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_EndFlight);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_ThreeCode", SqlDbType.Char,3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_EndFlight, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;
            parms[2].Value = accountBM.StationThreeCode;

            return parms;
        }
        /// <summary>
        /// 航后机位安排动态
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetEndFlight(DateTimeBM endDateTimeBM, DateTimeBM startDateTime, AccountBM accountBM)
        {
            SqlParameter[] endParms = GetEndFlightParameters(endDateTimeBM, accountBM);
            SqlParameter[] startParms = GetEndFlightParameters(startDateTime, accountBM);

            DataTable dtEndFlight = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_EndFlight, endParms);
            DataTable dtStartFlight = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_StartFlight, startParms);           


            EndFlightBM endInOutFlgihtBM = new EndFlightBM();

            foreach (DataRow drStartFlight in dtStartFlight.Rows)
            {
                DataRow drInOutFlight = endInOutFlgihtBM.Tables[0].NewRow();
                drInOutFlight["OutcncDATOP"] = drStartFlight["cncDATOP"].ToString();
                drInOutFlight["OutcnvcFLTID"] = drStartFlight["cnvcFLTID"].ToString();
                drInOutFlight["OutcniLEGNO"] = drStartFlight["cniLEGNO"].ToString();
                drInOutFlight["OutcnvcAC"] = drStartFlight["cnvcAC"].ToString();               
                drInOutFlight["OutcncFlightDate"] = drStartFlight["cncFlightDate"].ToString();
                drInOutFlight["OutcnvcLONG_REG"] = drStartFlight["cnvcLONG_REG"].ToString();
                drInOutFlight["OutcncDEPSTN"] = drStartFlight["cncDEPSTN"].ToString();
                drInOutFlight["OutcncARRSTN"] = drStartFlight["cncARRSTN"].ToString();
                drInOutFlight["OutcncSTD"] = DateTime.Parse(drStartFlight["cncSTD"].ToString()).ToString("HHmm");
                drInOutFlight["OutcncSTA"] = DateTime.Parse(drStartFlight["cncSTA"].ToString()).ToString("HHmm");
                drInOutFlight["OutcnvcOutGATE"] = drStartFlight["cnvcOutGATE"].ToString();
                drInOutFlight["OutcnvcGateRemark"] = drStartFlight["cnvcGateRemark"].ToString();


                DataRow[] drEndFlight = dtEndFlight.Select("cnvcLONG_REG='" + drStartFlight["cnvcLONG_REG"].ToString() + "'");
                if (drEndFlight.Length > 0)
                {
                    drInOutFlight["IncncDATOP"] = drEndFlight[0]["cncDATOP"].ToString();
                    drInOutFlight["IncnvcFLTID"] = drEndFlight[0]["cnvcFLTID"].ToString();
                    drInOutFlight["IncniLEGNO"] = drEndFlight[0]["cniLEGNO"].ToString();
                    drInOutFlight["IncnvcAC"] = drEndFlight[0]["cnvcAC"].ToString();
                    drInOutFlight["IncncFlightDate"] = drEndFlight[0]["cncFlightDate"].ToString();
                    drInOutFlight["IncnvcLONG_REG"] = drEndFlight[0]["cnvcLONG_REG"].ToString();
                    drInOutFlight["IncncDEPSTN"] = drEndFlight[0]["cncDEPSTN"].ToString();
                    drInOutFlight["IncncARRSTN"] = drEndFlight[0]["cncARRSTN"].ToString();
                    drInOutFlight["IncncSTD"] = DateTime.Parse(drEndFlight[0]["cncSTD"].ToString()).ToString("HHmm");
                    drInOutFlight["IncncSTA"] = DateTime.Parse(drEndFlight[0]["cncSTA"].ToString()).ToString("HHmm");
                    drInOutFlight["IncncTDWN"] = DateTime.Parse(drEndFlight[0]["cncTDWN"].ToString()).ToString("HHmm");
                    drInOutFlight["IncnvcInGATE"] = drEndFlight[0]["cnvcInGATE"].ToString();
                    dtEndFlight.Rows.Remove(drEndFlight[0]);
                }
                else
                {
                    drInOutFlight["IncnvcLONG_REG"] = drStartFlight["cnvcLONG_REG"].ToString();
                }

                endInOutFlgihtBM.Tables[0].Rows.Add(drInOutFlight);
            }

            foreach (DataRow drEndFlight in dtEndFlight.Rows)
            {
                DataRow drInOutFlight = endInOutFlgihtBM.Tables[0].NewRow();
                drInOutFlight["IncncDATOP"] = drEndFlight["cncDATOP"].ToString();
                drInOutFlight["IncnvcFLTID"] = drEndFlight["cnvcFLTID"].ToString();
                drInOutFlight["IncniLEGNO"] = drEndFlight["cniLEGNO"].ToString();
                drInOutFlight["IncnvcAC"] = drEndFlight["cnvcAC"].ToString();
                drInOutFlight["IncncFlightDate"] = drEndFlight["cncFlightDate"].ToString();
                drInOutFlight["IncnvcLONG_REG"] = drEndFlight["cnvcLONG_REG"].ToString();
                drInOutFlight["IncncDEPSTN"] = drEndFlight["cncDEPSTN"].ToString();
                drInOutFlight["IncncARRSTN"] = drEndFlight["cncARRSTN"].ToString();
                drInOutFlight["IncncSTD"] = DateTime.Parse(drEndFlight["cncSTD"].ToString()).ToString("HHmm");
                drInOutFlight["IncncSTA"] = DateTime.Parse(drEndFlight["cncSTA"].ToString()).ToString("HHmm");
                drInOutFlight["IncncTDWN"] = DateTime.Parse(drEndFlight["cncTDWN"].ToString()).ToString("HHmm");
                drInOutFlight["IncnvcInGATE"] = drEndFlight["cnvcInGATE"].ToString();
                drInOutFlight["OutcnvcGateRemark"] = drEndFlight["cnvcGateRemark"].ToString();
                endInOutFlgihtBM.Tables[0].Rows.Add(drInOutFlight);
                
            }

            endInOutFlgihtBM.Tables[0].DefaultView.Sort = "OutcncSTD,IncncTDWN"; 
            return endInOutFlgihtBM.Tables[0].DefaultView.ToTable();
        }
        #endregion

        #region 当天某驾飞机所飞的所有航段
        private const string SELECT_Aircraft_Flight = "SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cnvcLONG_REG,cncFlightDate," +
            "cncCKIFlightDate,cnvcCKIFlightNo,cncDEPSTN,cncDEPAirportFourCode,cncDEPCityThreeCode,cncARRSTN,cncARRAirportFourCode,cncARRCityThreeCode," +
            "cnvcFlightCharacterAbbreviate,cncSTATUS,cncStatusName,SUBSTRING(cncDEPAirportCNAME, 1, CHARINDEX('/', cncDEPAirportCNAME + '/') - 1) AS cncDEPAirportCNAME," +
            "REPLACE(SUBSTRING(cncSTD, 12, 5), ':', '') AS cncSTD,cncSTD AS cncAllSTD,REPLACE(SUBSTRING(cncETD, 12, 5), ':', '') AS cncETD, cncETD AS cncAllETD," +
            "REPLACE(SUBSTRING(cncTOFF, 12, 5), ':', '') AS cncTOFF,SUBSTRING(cncARRAirportCNAME, 1, CHARINDEX('/', cncARRAirportCNAME + '/') - 1) AS cncARRAirportCNAME," +
            "REPLACE(SUBSTRING(cncSTA, 12, 5), ':', '') AS cncSTA,cncSTA AS cncAllSTA,REPLACE(SUBSTRING(cncETA, 12, 5), ':', '') AS cncETA,cncETA AS cncAllETA,REPLACE(SUBSTRING(cncTDWN, 12, 5), ':', '') AS cncTDWN,cnvcDELAY1,cnvcSTC,cnvcJoinFlight FROM vw_Legs " +
            "WHERE cncETD >= @PARM_StartTime AND cncETD < @PARM_EndTime AND " +
            "cnvcLONG_REG = @PARM_cnvcLONG_REG ORDER BY cncAllETD";

        private SqlParameter[] GetAircraftFlightParameters(DateTimeBM dateTimeBM, string strLONG_REG)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_Aircraft_Flight);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_Aircraft_Flight, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;
            parms[2].Value = strLONG_REG;

            return parms;
        }

        /// <summary>
        /// 获取某驾飞机当天所飞的所有航段
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strLONG_REG"></param>
        /// <returns></returns>
        public DataTable GetAircraftFlights(DateTimeBM dateTimeBM, string strLONG_REG)
        {
            SqlParameter[] parms = GetAircraftFlightParameters(dateTimeBM, strLONG_REG);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_Aircraft_Flight, parms);
        }
        #endregion

        #region 获取某航站的所有进出港航班
        private const string SELECT_Station_Flight = "SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cnvcLONG_REG,cncFlightDate," +
             "cncCKIFlightDate,cnvcCKIFlightNo,cncDEPSTN,cncDEPAirportFourCode,cncDEPCityThreeCode,cncARRSTN,cncARRAirportFourCode,cncARRCityThreeCode," +
            "cnvcFlightCharacterAbbreviate,cncSTATUS,cncStatusName,SUBSTRING(cncDEPAirportCNAME, 1, CHARINDEX('/', cncDEPAirportCNAME + '/') - 1) AS cncDEPAirportCNAME," +
            "REPLACE(SUBSTRING(cncSTD, 12, 5), ':', '') AS cncSTD,cncSTD AS cncAllSTD,REPLACE(SUBSTRING(cncETD, 12, 5), ':', '') AS cncETD, cncETD AS cncAllETD," +
            "REPLACE(SUBSTRING(cncTOFF, 12, 5), ':', '') AS cncTOFF,SUBSTRING(cncARRAirportCNAME, 1, CHARINDEX('/', cncARRAirportCNAME + '/') - 1) AS cncARRAirportCNAME," +
            "REPLACE(SUBSTRING(cncSTA, 12, 5), ':', '') AS cncSTA,cncSTA AS cncAllSTA,REPLACE(SUBSTRING(cncETA, 12, 5), ':', '') AS cncETA,cncETA AS cncAllETA, REPLACE(SUBSTRING(cncTDWN, 12, 5), ':', '') AS cncTDWN,cnvcDELAY1,cnvcSTC,cnvcJoinFlight FROM vw_Legs " +
            "WHERE (cncETD >= @PARM_StartTime AND cncETD < @PARM_EndTime OR  " +
            "cncETA >= @PARM_StartTime AND cncETA < @PARM_EndTime) AND  " +
            "(cncDEPSTN = @PARM_ThreeCode OR cncARRSTN = @PARM_ThreeCode) ORDER BY cncAllETD";

        private SqlParameter[] GetStationFlightParameters(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_Station_Flight);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_ThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_Station_Flight, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;
            parms[2].Value = stationBM.ThreeCode;

            return parms;
        }

        /// <summary>
        /// 获取航站进出港航班
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <returns></returns>
        public DataTable GetStationFlight(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = GetStationFlightParameters(dateTimeBM, stationBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_Station_Flight, parms);
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
            string strSQL = "SELECT cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cnvcLONG_REG,cncFlightDate," +
            "cncCKIFlightDate,cnvcCKIFlightNo,cncDEPSTN,cncDEPAirportFourCode,cncDEPCityThreeCode,cncARRSTN,cncARRAirportFourCode,cncARRCityThreeCode," +
            "cnvcFlightCharacterAbbreviate,cncSTATUS,cncStatusName,SUBSTRING(cncDEPAirportCNAME, 1, CHARINDEX('/', cncDEPAirportCNAME + '/') - 1) AS cncDEPAirportCNAME," +
            "REPLACE(SUBSTRING(cncSTD, 12, 5), ':', '') AS cncSTD,cncSTD AS cncAllSTD,REPLACE(SUBSTRING(cncETD, 12, 5), ':', '') AS cncETD, cncETD AS cncAllETD," +
            "REPLACE(SUBSTRING(cncTOFF, 12, 5), ':', '') AS cncTOFF,SUBSTRING(cncARRAirportCNAME, 1, CHARINDEX('/', cncARRAirportCNAME + '/') - 1) AS cncARRAirportCNAME," +
            "REPLACE(SUBSTRING(cncSTA, 12, 5), ':', '') AS cncSTA,cncSTA AS cncAllSTA,REPLACE(SUBSTRING(cncETA, 12, 5), ':', '') AS cncETA, cncETA AS cncAllETA, REPLACE(SUBSTRING(cncTDWN, 12, 5), ':', '') AS cncTDWN,cnvcDELAY1,cnvcSTC,cnvcJoinFlight FROM vw_Legs " +
            "WHERE cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "'";

            if (strDEPSTN != "")
            {
                strSQL += " AND cncDEPSTN = '" + strDEPSTN + "'";
            }

            if (strARRSTN != "")
            {
                strSQL += " AND cncARRSTN = '" + strARRSTN + "'";
            }

            if(strFlightNo != "")
            {
                strSQL += " AND cnvcFLTID like '%" + strFlightNo + "%'";
            }

            strSQL += " ORDER BY cncAllETA";

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSQL);
        }
        #endregion

        #region 出港旅客值机人数统计
        private const string SELECT_STATISTIC_PAX_NUM_BY_TYPE = "SELECT cncIATATYPE, COUNT(*) AS cniFlgihtNum," +
            "SUM(cniCheckNum) AS cniCheckNum FROM vw_Legs,tbACTYPE_MISC WHERE " +
            "vw_Legs.cncACTYP = tbACTYPE_MISC.cncACTYPE AND " +
            "cncSTD > = @PARM_StartTime AND cncSTD < @PARM_EndTIme AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND " +
            "cncSTATUS <> 'CNL' " +
            "GROUP BY cncIATATYPE ORDER BY cncIATATYPE";

        private SqlParameter[] GetStatisticPaxParameters(DateTimeBM dateTimeBM, string strDEPSTN)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_STATISTIC_PAX_NUM_BY_TYPE);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTIme", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_STATISTIC_PAX_NUM_BY_TYPE, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;
            parms[2].Value = strDEPSTN;

            return parms;
        }

        /// <summary>
        /// 按机型统计出港旅客人数
        /// </summary>
        /// <param name="dateTimeBM">时间范围</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns></returns>
        public DataTable GetStatisticPax(DateTimeBM dateTimeBM, string strDEPSTN)
        {
            SqlParameter[] parms = GetStatisticPaxParameters(dateTimeBM, strDEPSTN);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_STATISTIC_PAX_NUM_BY_TYPE, parms);
        }
        #endregion

        #region  根据北京时间航班日期获取当天所有航班
        /// <summary>
        /// 根据当地日期获取当天所有航班动态
        /// </summary>
        private const string SELECT_LegsByFlightDate = "select * from vw_Legs where cncFlightDate >= @PARM_cncStartFlightDate and cncFlightDate <= @PARM_cncEndFlightDate and cniDeleteTag = 0";

        private SqlParameter[] GetLegsByFlightDateParameters(string strStartFlightDate, string strEndFlightDate)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_LegsByFlightDate);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncStartFlightDate", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cncEndFlightDate", SqlDbType.Char, 10)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_LegsByFlightDate, parms);
            }

            parms[0].Value = strStartFlightDate;
            parms[1].Value = strEndFlightDate;
            
            return parms;
        }

        /// <summary>
        /// 根据当地时间获取当天的航班动态
        /// </summary>
        /// <param name="strFlightDate">航班日期</param>
        /// <returns></returns>
        public DataTable GetLegsByFlightDate(string strStartFlightDate, string strEndFlightDate)
        {
            SqlParameter[] parms = GetLegsByFlightDateParameters(strStartFlightDate, strEndFlightDate);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_LegsByFlightDate, parms);
        }

        #endregion


        #region added by LinYong

        #region 获取某个时间段内所有的航班（vw_Legs）
        private const string strDataField_Allvw_LegsByDay = "cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cncFlightDate,cncCKIFlightDate,cnvcFlightNo,cnvcCKIFlightNo,cnvcLONG_REG,cncDEPSTN,cncDEPCityThreeCode,cncDEPAirportFourCode,cncDEPAirportCNAME,cncDEPIsSelf,cniDEPAirportPaxType,cniDEPAirportTaxiFuelType,cncARRSTN,cncARRCityThreeCode,cncARRAirportFourCode" +
            ",cncARRAirportCNAME,cncARRIsSelf,cniARRAirportPaxType,cniARRAirportTaxiFuelType,cncSTD,cncSTA,cncSTATUS,cncStatusName,cniViewIndex,cncETD,cncETA,cncATD,cncTOFF,cncTDWN,cncATA,cnvcTRI_FLTID,cnvcDIV_RCODE,cnvcDiversionDelayName,cnvcDIV_FLAG,cnvcPAX,cnvcBOOK,cnvcDELAY1,cnvcFlightDelayName,cniDUR1" +
            ",cnvcDELAY2,cniDUR2,cnvcDELAY3,cniDUR3,cnvcDELAY4,cniDUR4,cnvcGATE,cnvcSTC,cnvcFlightCharacterAbbreviate,cnvcVERSION,cncORIG_ACTYP,cncACTYP,cnvcACOWN,cnvcSEQ,cncInsertTime,cniDeleteTag,cncACARSTDWN,cncACARSATA,cncInTime,cnvcInDelayCode,cnvcInDelayName,cnvcInCarInfor,cncInCarDepTime,cncInCarArrTime" +
            ",cnvcInRemark,cnvcInGATE,cncInMCCReadyTime,cnvcOutGate,cncStartGuaranteeTime,cnvcJoinFlight,cnvcCheckCounter,cnvcWaitHall,cnvcBoradingGate,cncOpenCabinTime,cnvcOutCarInfor,cncOutCarDepTime,cncOutCarArrTime,cncPilotArrTime,cncStewardArrTime,cncDeiceStartTime,cncDeiceEndTime,cncTaskSheetChangeTime,cncDispatchTime" +
            ",cncDispatchPrintTime,cncOutMCCReadyTime,cncMCCReleaseTime,cnvcAircraftStatus,cncDeplaneTime,cncCleanStartTime,cncCleanEndTime,cncSewageStartTime,cncSewageEndTime,cncCabinSupplyStartTime,cncCabinSupplyEndTime,cncOilStartTime,cncOilEndTime,cncOpenCargoCabinTime,cncCargoStartTime,cncBaggageTime,cncCloseCargoCabinTime" +
            ",cncTrailerArrTime,cncDirtyWaterCarArrTime,cncFerryDepTime,cncLastFerryArrTime,cncDragPoleArrTime,cncLadderCarArrTime,cncInformBoardTime,cncBoardTime,cnvcBookNum,cniCheckNum,cnbTransitPaxTag,cntTransitPax,cniXCRNum,cniAdultNum,cniChildNum,cniInfantNum,cniFirstClassNum,cniOfficialClassNum,cniTouristClassNum" +
            ",cniAscendingPaxNum,cntPaxNameList,cncBoardOverTime,cncLoadSheetArrTime,cncClosePaxCabinTime,cncInternalGuestTogether,cncOutTime,cncPushTime,cncACARSOUT,cncACARSTOFF,cnvcDischargingDelCode,cnvcDisChargingDelName,cnvcOutDelayCode,cnvcOutDelayName,cniCargoWeight,cniMailWeight,cniBaggageWeight,cniAirMaterialWeight" +
            ",cnvcAirMaterialRemark,cniBaggageNum,cnbVIPTag,cniTotalFuelWeight,cniTripFuelWeight,cniTaxiFuelWeight,cniUnloadCargoWeight,cnvcCargoRemark,cnvcOutRemark,cnvcChiefController,cnvcAssistantController,cnvcOutFieldController,cnvcBalanceController,cniDischargingDelayTime,cnvcGateRemark,cniFocusTag,cniIsAutoSaveCheckPaxInfo" +
            ",cniIsAutoSaveCheckPaxNameList,cniIsAutoSaveTransitPax,cniIntermissionTime,cniNotTDWN,cniNotTOFF,cniNotClosePaxCabineTime,cniOpenTime,cniNotEnoughIntermissionTime,cniStartGuarantee,cniBoarding,cniMCCReady,cniMCCRelease,cniShow,cncCFPTOFF,cncCFPTDWN,cnvcInAircraftStatus,cncOutMCCTechEngr,cncOutMCCDispEngr,cncOutPowerCarStartTime" +
            ",cncOutPowerCarNo,cncAPUUp,cncOutPowerCarEndTime,cncInPowerCarStartTime,cncInPowerCarNo,cncAPUDown,cncInPowerCarEndTime ";

        //private const string SELECT_Allvw_LegsByDay = "SELECT " + strDataField_Allvw_LegsByDay + "FROM vw_Legs WHERE " +
        private const string SELECT_Allvw_LegsByDay = "SELECT * FROM vw_Legs WHERE " +
           "cncETD >= @PARM_StartcncETD AND cncETD < @PARM_EndcncETD " +
            "ORDER BY cnvcLONG_REG, cncETD";

        private SqlParameter[] GetAllvw_LegsByDayParameters(DateTimeBM dateTimeBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_Allvw_LegsByDay);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartcncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndcncETD", SqlDbType.Char, 19),
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_Allvw_LegsByDay, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;
            parms[1].Value = dateTimeBM.EndDateTime;

            return parms;
        }

        /// <summary>
        /// 获取某个时间段内所有的航班（vw_Legs）
        /// </summary>
        /// <param name="dateTimeBM">时间段范围实体对象</param>
        /// <returns>某个时间段内所有的航班（vw_Legs）</returns>
        public DataTable GetAllvw_LegsByDay(DateTimeBM dateTimeBM)
        {
            SqlParameter[] parms = GetAllvw_LegsByDayParameters(dateTimeBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_Allvw_LegsByDay, parms);
        }
        #endregion

        #endregion

    }
}
