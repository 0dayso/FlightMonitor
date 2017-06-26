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
    /// ��վ���ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-24
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class StationDA:SqlDatabase
    {
        #region ��ȡ���к�վ��Ϣ
        /// <summary>
        /// ��ȡ���к�վ��ϢSQL���
        /// </summary>
        private const string SELECT_AllStation = "SELECT * FROM tbStationInfor";

        /// <summary>
        /// ��ȡ���к�վ��Ϣ
        /// </summary>
        /// <returns>�������к�վ��Ϣ��DataTable</returns>
        public DataTable GetAllStation()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, this.Transaction, CommandType.Text, SELECT_AllStation);
        }
        #endregion

        #region �����������ȡ��վ��Ϣ
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
        /// ���ݻ����������ȡ��վ��Ϣ
        /// </summary>
        /// <param name="strStationThreeCode">��վ������</param>
        /// <returns></returns>
        public DataTable GetStationByThreeCode(string strStationThreeCode)
        {
            SqlParameter[] parms = GetStationByThreeCodeParameters(strStationThreeCode);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_StationByThreeCode, parms);
        }
        #endregion

        #region ���һ����վ
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
        /// ���һ����վ
        /// </summary>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public int InsertStation(StationBM stationBM)
        {
            SqlParameter[] parms = GetInsertStationParameters(stationBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, INSERT_Station, parms);
        }
        #endregion

        #region ɾ��һ����վ
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
        /// ɾ��һ����վ
        /// </summary>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public int DeleteStation(StationBM stationBM)
        {
            SqlParameter[] parms = GetDeleteStationParameters(stationBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, DELETE_Station, parms);
        }
        #endregion
    }
}
