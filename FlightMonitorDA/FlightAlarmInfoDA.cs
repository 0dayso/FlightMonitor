using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取航班告警信息
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-08
    /// 修 改 人：
    /// 修改日期：2016-06-01
    /// 版    本：
    public class FlightAlarmInfoDA : SqlDatabase
    {
        #region 插入航班告警信息记录
        /// <summary>
        /// 插入航班告警信息记录
        /// </summary>
        /// <param name="flightAlarmInfoBM">航班告警信息实体对象，提供参数信息</param>
        /// <returns></returns>
        public int Add(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            #region 生成执行语句
            if (flightAlarmInfoBM.cncInDATOP != null)
            {
                strSql1.Append("cncInDATOP,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInDATOP + "',");
            }
            if (flightAlarmInfoBM.cncInFlightDate != null)
            {
                strSql1.Append("cncInFlightDate,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInFlightDate + "',");
            }
            if (flightAlarmInfoBM.cnvcInFLTID != null)
            {
                strSql1.Append("cnvcInFLTID,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcInFLTID + "',");
            }
            if (flightAlarmInfoBM.cniInLEGNO != null)
            {
                strSql1.Append("cniInLEGNO,");
                strSql2.Append("" + flightAlarmInfoBM.cniInLEGNO.ToString() + ",");
            }
            if (flightAlarmInfoBM.cnvcInAC != null)
            {
                strSql1.Append("cnvcInAC,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcInAC + "',");
            }
            if (flightAlarmInfoBM.cnvcInLONG_REG != null)
            {
                strSql1.Append("cnvcInLONG_REG,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcInLONG_REG + "',");
            }
            if (flightAlarmInfoBM.cncInDEPSTN != null)
            {
                strSql1.Append("cncInDEPSTN,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInDEPSTN + "',");
            }
            if (flightAlarmInfoBM.cncInARRSTN != null)
            {
                strSql1.Append("cncInARRSTN,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInARRSTN + "',");
            }
            if (flightAlarmInfoBM.cncInSTA != null)
            {
                strSql1.Append("cncInSTA,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInSTA + "',");
            }
            if (flightAlarmInfoBM.cncInETA != null)
            {
                strSql1.Append("cncInETA,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInETA + "',");
            }
            if (flightAlarmInfoBM.cncInTDWN != null)
            {
                strSql1.Append("cncInTDWN,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInTDWN + "',");
            }
            if (flightAlarmInfoBM.cncInATA != null)
            {
                strSql1.Append("cncInATA,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInATA + "',");
            }
            if (flightAlarmInfoBM.cncInSTATUS != null)
            {
                strSql1.Append("cncInSTATUS,");
                strSql2.Append("'" + flightAlarmInfoBM.cncInSTATUS + "',");
            }
            if (flightAlarmInfoBM.cncOutDATOP != null)
            {
                strSql1.Append("cncOutDATOP,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutDATOP + "',");
            }
            if (flightAlarmInfoBM.cncOutFlightDate != null)
            {
                strSql1.Append("cncOutFlightDate,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutFlightDate + "',");
            }
            if (flightAlarmInfoBM.cnvcOutFLTID != null)
            {
                strSql1.Append("cnvcOutFLTID,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcOutFLTID + "',");
            }
            if (flightAlarmInfoBM.cniOutLEGNO != null)
            {
                strSql1.Append("cniOutLEGNO,");
                strSql2.Append("" + flightAlarmInfoBM.cniOutLEGNO.ToString() + ",");
            }
            if (flightAlarmInfoBM.cnvcOutAC != null)
            {
                strSql1.Append("cnvcOutAC,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcOutAC + "',");
            }
            if (flightAlarmInfoBM.cnvcOutLONG_REG != null)
            {
                strSql1.Append("cnvcOutLONG_REG,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcOutLONG_REG + "',");
            }
            if (flightAlarmInfoBM.cncOutDEPSTN != null)
            {
                strSql1.Append("cncOutDEPSTN,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutDEPSTN + "',");
            }
            if (flightAlarmInfoBM.cncOutARRSTN != null)
            {
                strSql1.Append("cncOutARRSTN,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutARRSTN + "',");
            }
            if (flightAlarmInfoBM.cncOutSTD != null)
            {
                strSql1.Append("cncOutSTD,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutSTD + "',");
            }
            if (flightAlarmInfoBM.cncOutETD != null)
            {
                strSql1.Append("cncOutETD,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutETD + "',");
            }
            if (flightAlarmInfoBM.cncOutTOFF != null)
            {
                strSql1.Append("cncOutTOFF,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutTOFF + "',");
            }
            if (flightAlarmInfoBM.cncOutATD != null)
            {
                strSql1.Append("cncOutATD,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutATD + "',");
            }
            if (flightAlarmInfoBM.cncOutSTATUS != null)
            {
                strSql1.Append("cncOutSTATUS,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOutSTATUS + "',");
            }
            if (flightAlarmInfoBM.cniInLEGNO != null)
            {
                strSql1.Append("cniTaxiOutMinutes,");
                strSql2.Append("" + flightAlarmInfoBM.cniTaxiOutMinutes.ToString() + ",");
            }
            if (flightAlarmInfoBM.cnvcOverStationType != null)
            {
                strSql1.Append("cnvcOverStationType,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcOverStationType + "',");
            }
            if (flightAlarmInfoBM.cniOverStationStandardTime != null)
            {
                strSql1.Append("cniOverStationStandardTime,");
                strSql2.Append("" + flightAlarmInfoBM.cniOverStationStandardTime.ToString() + ",");
            }
            if (flightAlarmInfoBM.cncOverStationStart != null)
            {
                strSql1.Append("cncOverStationStart,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOverStationStart + "',");
            }
            if (flightAlarmInfoBM.cncOverStationEnd != null)
            {
                strSql1.Append("cncOverStationEnd,");
                strSql2.Append("'" + flightAlarmInfoBM.cncOverStationEnd + "',");
            }
            if (flightAlarmInfoBM.cnvcAlarmCode != null)
            {
                strSql1.Append("cnvcAlarmCode,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcAlarmCode + "',");
            }
            if (flightAlarmInfoBM.cnvcAlarmValue != null)
            {
                strSql1.Append("cnvcAlarmValue,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcAlarmValue + "',");
            }
            if (flightAlarmInfoBM.cnvcAlarmPoint != null)
            {
                strSql1.Append("cnvcAlarmPoint,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcAlarmPoint + "',");
            }
            if (flightAlarmInfoBM.cniAlarmResult != null)
            {
                strSql1.Append("cniAlarmResult,");
                strSql2.Append("" + flightAlarmInfoBM.cniAlarmResult.ToString() + ",");
            }
            if (flightAlarmInfoBM.cnvcMemo != null)
            {
                strSql1.Append("cnvcMemo,");
                strSql2.Append("'" + flightAlarmInfoBM.cnvcMemo + "',");
            }
            if (flightAlarmInfoBM.cndOperationTime != null)
            {
                strSql1.Append("cndOperationTime,");
                strSql2.Append("'" + flightAlarmInfoBM.cndOperationTime + "',");
            }
            strSql1.Append("cndPutInDBTime,"); //
            strSql2.Append("getdate(),");

            strSql.Append("insert into tbFlightAlarmInfo(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            #endregion 生成执行语句

            int retVAL = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSql.ToString());
            return retVAL;
        }
        #endregion 插入航班告警信息记录

        #region 更新航班告警信息记录
        /// <summary>
        /// 更新航班告警信息记录
        /// </summary>
        /// <param name="flightAlarmInfoBM">航班告警信息实体对象，提供参数信息</param>
        /// <returns></returns>
        public int Update(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            StringBuilder strSql = new StringBuilder();

            #region 生成执行语句
            strSql.Append("update tbFlightAlarmInfo set ");
            if (flightAlarmInfoBM.cncInFlightDate != null)
            {
                strSql.Append("cncInFlightDate='" + flightAlarmInfoBM.cncInFlightDate + "',");
            }
            if (flightAlarmInfoBM.cnvcInLONG_REG != null)
            {
                strSql.Append("cnvcInLONG_REG='" + flightAlarmInfoBM.cnvcInLONG_REG + "',");
            }
            if (flightAlarmInfoBM.cncInDEPSTN != null)
            {
                strSql.Append("cncInDEPSTN='" + flightAlarmInfoBM.cncInDEPSTN + "',");
            }
            if (flightAlarmInfoBM.cncInARRSTN != null)
            {
                strSql.Append("cncInARRSTN='" + flightAlarmInfoBM.cncInARRSTN + "',");
            }
            if (flightAlarmInfoBM.cncInSTA != null)
            {
                strSql.Append("cncInSTA='" + flightAlarmInfoBM.cncInSTA + "',");
            }
            if (flightAlarmInfoBM.cncInETA != null)
            {
                strSql.Append("cncInETA='" + flightAlarmInfoBM.cncInETA + "',");
            }
            if (flightAlarmInfoBM.cncInTDWN != null)
            {
                strSql.Append("cncInTDWN='" + flightAlarmInfoBM.cncInTDWN + "',");
            }
            if (flightAlarmInfoBM.cncInATA != null)
            {
                strSql.Append("cncInATA='" + flightAlarmInfoBM.cncInATA + "',");
            }
            if (flightAlarmInfoBM.cncInSTATUS != null)
            {
                strSql.Append("cncInSTATUS='" + flightAlarmInfoBM.cncInSTATUS + "',");
            }
            if (flightAlarmInfoBM.cncOutFlightDate != null)
            {
                strSql.Append("cncOutFlightDate='" + flightAlarmInfoBM.cncOutFlightDate + "',");
            }
            if (flightAlarmInfoBM.cnvcOutLONG_REG != null)
            {
                strSql.Append("cnvcOutLONG_REG='" + flightAlarmInfoBM.cnvcOutLONG_REG + "',");
            }
            if (flightAlarmInfoBM.cncOutDEPSTN != null)
            {
                strSql.Append("cncOutDEPSTN='" + flightAlarmInfoBM.cncOutDEPSTN + "',");
            }
            if (flightAlarmInfoBM.cncOutARRSTN != null)
            {
                strSql.Append("cncOutARRSTN='" + flightAlarmInfoBM.cncOutARRSTN + "',");
            }
            if (flightAlarmInfoBM.cncOutSTD != null)
            {
                strSql.Append("cncOutSTD='" + flightAlarmInfoBM.cncOutSTD + "',");
            }
            if (flightAlarmInfoBM.cncOutETD != null)
            {
                strSql.Append("cncOutETD='" + flightAlarmInfoBM.cncOutETD + "',");
            }
            if (flightAlarmInfoBM.cncOutTOFF != null)
            {
                strSql.Append("cncOutTOFF='" + flightAlarmInfoBM.cncOutTOFF + "',");
            }
            if (flightAlarmInfoBM.cncOutATD != null)
            {
                strSql.Append("cncOutATD='" + flightAlarmInfoBM.cncOutATD + "',");
            }
            if (flightAlarmInfoBM.cncOutSTATUS != null)
            {
                strSql.Append("cncOutSTATUS='" + flightAlarmInfoBM.cncOutSTATUS + "',");
            }
            if (flightAlarmInfoBM.cniTaxiOutMinutes != null)
            {
                strSql.Append("cniTaxiOutMinutes=" + flightAlarmInfoBM.cniTaxiOutMinutes.ToString() + ",");
            }
            if (flightAlarmInfoBM.cnvcOverStationType != null)
            {
                strSql.Append("cnvcOverStationType='" + flightAlarmInfoBM.cnvcOverStationType + "',");
            }
            if (flightAlarmInfoBM.cniOverStationStandardTime != null)
            {
                strSql.Append("cniOverStationStandardTime=" + flightAlarmInfoBM.cniOverStationStandardTime.ToString() + ",");
            }
            if (flightAlarmInfoBM.cncOverStationStart != null)
            {
                strSql.Append("cncOverStationStart='" + flightAlarmInfoBM.cncOverStationStart + "',");
            }
            if (flightAlarmInfoBM.cncOverStationEnd != null)
            {
                strSql.Append("cncOverStationEnd='" + flightAlarmInfoBM.cncOverStationEnd + "',");
            }
            if (flightAlarmInfoBM.cnvcAlarmValue != null)
            {
                strSql.Append("cnvcAlarmValue='" + flightAlarmInfoBM.cnvcAlarmValue + "',");
            }
            if (flightAlarmInfoBM.cnvcAlarmPoint != null)
            {
                strSql.Append("cnvcAlarmPoint='" + flightAlarmInfoBM.cnvcAlarmPoint + "',");
            }
            if (flightAlarmInfoBM.cniAlarmResult != null)
            {
                strSql.Append("cniAlarmResult=" + flightAlarmInfoBM.cniAlarmResult.ToString() + ",");
            }
            if (flightAlarmInfoBM.cnvcMemo != null)
            {
                strSql.Append("cnvcMemo='" + flightAlarmInfoBM.cnvcMemo + "',");
            }
            if (flightAlarmInfoBM.cndOperationTime != null)
            {
                strSql.Append("cndOperationTime='" + flightAlarmInfoBM.cndOperationTime + "',");
            }
            strSql.Append("cndPutInDBTime = " + "getdate()" + ",");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where cncOutDATOP='" + flightAlarmInfoBM.cncOutDATOP + 
                "' and cnvcOutFLTID='" + flightAlarmInfoBM.cnvcOutFLTID + 
                "' and cniOutLEGNO=" + flightAlarmInfoBM.cniOutLEGNO.ToString() + 
                " and cnvcOutAC='" + flightAlarmInfoBM.cnvcOutAC + 
                "' and cncInDATOP='" + flightAlarmInfoBM.cncInDATOP + 
                "' and cnvcInFLTID='" + flightAlarmInfoBM.cnvcInFLTID + 
                "' and cniInLEGNO=" + flightAlarmInfoBM.cniInLEGNO.ToString() + 
                " and cnvcInAC='" + flightAlarmInfoBM.cnvcInAC +
                "' and cnvcAlarmCode = '" + flightAlarmInfoBM.cnvcAlarmCode + "'");
            #endregion 生成执行语句

            int retVAL = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSql.ToString());
            return retVAL;
        }
        #endregion 更新航班告警信息记录

        #region 根据关键参数获取航班告警信息记录
        /// <summary>
        /// 根据关键参数获取航班告警信息记录
        /// </summary>
        /// <param name="cncOutDATOP"></param>
        /// <param name="cnvcOutFLTID"></param>
        /// <param name="cniOutLEGNO"></param>
        /// <param name="cnvcOutAC"></param>
        /// <param name="cncInDATOP"></param>
        /// <param name="cnvcInFLTID"></param>
        /// <param name="cniInLEGNO"></param>
        /// <param name="cnvcInAC"></param>
        /// <returns></returns>
        public DataTable Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from tbFlightAlarmInfo");
            strSql.Append(" where cncOutDATOP='" + cncOutDATOP + "' and cnvcOutFLTID='" + cnvcOutFLTID + "' and cniOutLEGNO=" + cniOutLEGNO.ToString() + " and cnvcOutAC='" + cnvcOutAC + "' and cncInDATOP='" + cncInDATOP + "' and cnvcInFLTID='" + cnvcInFLTID + "' and cniInLEGNO=" + cniInLEGNO.ToString() + " and cnvcInAC='" + cnvcInAC + "' ");
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion 根据关键参数获取航班告警信息记录

        #region 根据关键参数获取航班告警信息记录
        /// <summary>
        /// 根据关键参数获取航班告警信息记录
        /// </summary>
        /// <param name="cncOutDATOP"></param>
        /// <param name="cnvcOutFLTID"></param>
        /// <param name="cniOutLEGNO"></param>
        /// <param name="cnvcOutAC"></param>
        /// <param name="cncInDATOP"></param>
        /// <param name="cnvcInFLTID"></param>
        /// <param name="cniInLEGNO"></param>
        /// <param name="cnvcInAC"></param>
        /// <param name="cnvcAlarmCode"></param>
        /// <returns></returns>
        public DataTable Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC,
            string cnvcAlarmCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbFlightAlarmInfo");
            strSql.Append(" where cncOutDATOP='" + cncOutDATOP + 
                "' and cnvcOutFLTID='" + cnvcOutFLTID + 
                "' and cniOutLEGNO=" + cniOutLEGNO.ToString() + 
                " and cnvcOutAC='" + cnvcOutAC + 
                "' and cncInDATOP='" + cncInDATOP + 
                "' and cnvcInFLTID='" + cnvcInFLTID + 
                "' and cniInLEGNO=" + cniInLEGNO.ToString() + 
                " and cnvcInAC='" + cnvcInAC +
                "' and cnvcAlarmCode = '" + cnvcAlarmCode + "'");
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion 根据关键参数获取航班告警信息记录

        #region 根据航班日期区间参数获取航班告警信息记录
        /// <summary>
        /// 根据航班日期区间参数获取航班告警信息记录
        /// </summary>
        /// <param name="FlightDate_Start">开始日期</param>
        /// <param name="FlightDate_End">结束日期</param>
        /// <returns></returns>
        public DataTable Select(
            string FlightDate_Start,
            string FlightDate_End)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbFlightAlarmInfo");
            strSql.Append(" where ((cncInFlightDate >= '" + FlightDate_Start + "') and (cncInFlightDate <= '" + FlightDate_End + "')) or (( cncOutFlightDate >= '" + FlightDate_Start + "') and (cncOutFlightDate <= '" + FlightDate_End + "')) ");
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion 根据航班日期区间参数获取航班告警信息记录

    }
}
