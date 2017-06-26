using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.DataHelper;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航站数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-24
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class StationDA:SqlDatabase
    {
        #region 获取所有航站信息
        /// <summary>
        /// 获取所有航站信息SQL语句
        /// </summary>
        private const string SELECT_AllStation = "SELECT * FROM tbStationInfor";

        /// <summary>
        /// 获取所有航站信息
        /// </summary>
        /// <returns>包含所有航站信息的DataTable</returns>
        public DataTable GetAllStation()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, this.Transaction, CommandType.Text, SELECT_AllStation);
        }
        #endregion

        #region 根据三字码获取航站信息
        private const string SELECT_StationByThreeCode = "SELECT * FROM tbStationInfor WHERE cncThreeCode = @PARM_cncThreeCode";

        private SqlParameter[] GetStationByThreeCodeParameters(string strStationThreeCode)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_StationByThreeCode);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncThreeCode", SqlDbType.Char, 3)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_StationByThreeCode, parms);
            }

            parms[0].Value = strStationThreeCode;

            return parms;
        }

        /// <summary>
        /// 根据机场三字码获取航站信息
        /// </summary>
        /// <param name="strStationThreeCode">航站三字码</param>
        /// <returns></returns>
        public DataTable GetStationByThreeCode(string strStationThreeCode)
        {
            SqlParameter[] parms = GetStationByThreeCodeParameters(strStationThreeCode);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_StationByThreeCode, parms);
        }
        #endregion

        #region 添加一个航站
        private const string INSERT_Station = "INSERT INTO tbStationInfor VALUES(" +
            "@PARM_cncThreeCode, @PARM_cnvcStationName, @PARM_cnvcAirportName," +
            "@PARM_cnvcCommanderOfficeName, @PARM_cnvcStationSignInFlag," +
            "@PARM_cniDayLine, @PARM_cniDelayTimeLine, @PARM_cniJoinTimeLine, @PARM_cniDisconnectTimeLine)";

        private SqlParameter[] GetInsertStationParameters(StationBM stationBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Station);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncThreeCode", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcStationName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cnvcAirportName", SqlDbType.NVarChar,20),
                    new SqlParameter("@PARM_cnvcCommanderOfficeName", SqlDbType.NVarChar,20),
                    new SqlParameter("@PARM_cnvcStationSignInFlag", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cniDayLine", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniDelayTimeLine", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniJoinTimeLine", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniDisconnectTimeLine", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Station, parms);
            }

            parms[0].Value = stationBM.ThreeCode;
            parms[1].Value = stationBM.StationName;
            parms[2].Value = stationBM.AirportName;
            parms[3].Value = stationBM.CommanderOfficeName;
            parms[4].Value = stationBM.StationSignInFlag;
            parms[5].Value = stationBM.DayLine;
            parms[6].Value = stationBM.DelayTimeLine;
            parms[7].Value = stationBM.JoinTimeLine;
            parms[8].Value = stationBM.DisconnectTimeLine;

            return parms;
        }

        /// <summary>
        /// 添加一个航站
        /// </summary>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public int InsertStation(StationBM stationBM)
        {
            SqlParameter[] parms = GetInsertStationParameters(stationBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, INSERT_Station, parms);
        }
        #endregion

        #region 删除一个航站
        private const string DELETE_Station = "DELETE FROM tbStationInfor WHERE cniStationInforId = @PARM_cniStationInforId";

        private SqlParameter[] GetDeleteStationParameters(StationBM stationBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, DELETE_Station);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniStationInforId", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, DELETE_Station, parms);
            }

            parms[0].Value = stationBM.StationInforId;

            return parms;
        }

        /// <summary>
        /// 删除一个航站
        /// </summary>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public int DeleteStation(StationBM stationBM)
        {
            SqlParameter[] parms = GetDeleteStationParameters(stationBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, DELETE_Station, parms);
        }
        #endregion
    }
}
