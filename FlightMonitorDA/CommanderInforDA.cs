using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class CommanderInforDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommanderInforDA()
        {
        }

        #region 获取某航站某类保障人员列表，适应通过“现场”匹配 “主控、副控、外场”的情况，modified by LinYong in 20160324
        /*
        #region 适应通过“现场”匹配 “主控、副控、外场”的情况，modified by LinYong in 20160324
        //private const string SELECT_CommanderByTypeAndStation = "SELECT * FROM tbCommanderInfor WHERE " +
        //    "cnvcCommanderType = @PARM_cnvcCommanderType AND " +
        //    "cncThreeCode = @PARM_cncThreeCode";

        private const string SELECT_CommanderByTypeAndStation = "SELECT * FROM tbCommanderInfor WHERE " +
            "cnvcCommanderType in (@PARM_cnvcCommanderType) AND " +
            "cncThreeCode = @PARM_cncThreeCode";

        #endregion 适应通过“现场”匹配 “主控、副控、外场”的情况，modified by LinYong in 20160324

        private SqlParameter[] GetCommanderByTypeAndStationParameters(string strCommderType, string strStationCode)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_CommanderByTypeAndStation);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcCommanderType", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cncThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_CommanderByTypeAndStation, parms);
            }

            parms[0].Value = strCommderType;
            parms[1].Value = strStationCode;

            return parms;
        }

        /// <summary>
        /// 获取某航站某类保障人员列表
        /// </summary>
        /// <param name="strCommderType"></param>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public DataTable GetCommanderByTypeAndStation(string strCommderType, string strStationCode)
        {
            SqlParameter[] parms = GetCommanderByTypeAndStationParameters(strCommderType, strStationCode);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CommanderByTypeAndStation, parms);
        }

        */
        /// <summary>
        /// 获取某航站某类保障人员列表
        /// </summary>
        /// <param name="strCommderType"></param>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public DataTable GetCommanderByTypeAndStation(string strCommderType, string strStationCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM tbCommanderInfor WHERE ");
            strSql.Append("(cnvcCommanderType in ('" + strCommderType + "')) AND ");
            strSql.Append("(cncThreeCode = '" + strStationCode + "')");

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());
        }

        #endregion 获取某航站某类保障人员列表，适应通过“现场”匹配 “主控、副控、外场”的情况，modified by LinYong in 20160324



        #region 获取某航站保障人员列表
        private const string SELECT_CommanderByStation = "SELECT * FROM tbCommanderInfor WHERE " +            
            "cncThreeCode = @PARM_cncThreeCode";

        private SqlParameter[] GetCommanderByStationParameters(string strStationCode)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_CommanderByStation);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {                   
                    new SqlParameter("@PARM_cncThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_CommanderByStation, parms);
            }
            
            parms[0].Value = strStationCode;

            return parms;
        }

        /// <summary>
        /// 获取某航站保障人员列表
        /// </summary>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public DataTable GetCommanderByStation(string strStationCode)
        {
            SqlParameter[] parms = GetCommanderByStationParameters(strStationCode);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_CommanderByStation, parms);
        }
        #endregion



        #region 添加一个保障人员
        private const string INSERT_Commander = "INSERT INTO tbCommanderInfor VALUES(" +
            "@PARM_cnvCommanderAccount, @PARM_cnvcCommanderName, @PARM_cnvcCommanderType, @PARM_cncThreeCode)";

        private SqlParameter[] GetInsertCommanderParameters(CommanderInforBM commanderInforBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Commander);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvCommanderAccount", SqlDbType.NVarChar, 20),
                    new SqlParameter("@PARM_cnvcCommanderName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cnvcCommanderType", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cncThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Commander, parms);
            }

            parms[0].Value = commanderInforBM.CommanderAccount;
            parms[1].Value = commanderInforBM.CommanderName;
            parms[2].Value = commanderInforBM.CommanderType;
            parms[3].Value = commanderInforBM.ThreeCode;
            return parms;
        }

        /// <summary>
        /// 添加一个航站保障人员
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public int InsertCommander(CommanderInforBM commanderInforBM)
        {
            SqlParameter[] parms = GetInsertCommanderParameters(commanderInforBM);

            return SqlHelper.ExecuteNonQuery(this.DBConnString, CommandType.Text, INSERT_Commander, parms);
        }
        #endregion

        #region 删除一个航站保障人员
        private const string Delete_Commander = "DELETE FROM tbCommanderInfor WHERE cniCommanderInforId = @PARM_cniCommanderInforId";

        private SqlParameter[] GetDeleteCommanderParameters(CommanderInforBM commandInforBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, Delete_Commander);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniCommanderInforId", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, Delete_Commander, parms);
            }

            parms[0].Value = commandInforBM.CommanderInforId;

            return parms;
        }

        /// <summary>
        /// 删除一个航站保障人员
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public int DeleteCommander(CommanderInforBM commanderInforBM)
        {
            SqlParameter[] parms = GetDeleteCommanderParameters(commanderInforBM);

            return SqlHelper.ExecuteNonQuery(this.DBConnString, CommandType.Text, Delete_Commander, parms);
        }
        #endregion


    }
}
