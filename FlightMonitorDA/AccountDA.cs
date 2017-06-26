using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 用户数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AccountDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountDA()
        {
        }

        #region 插入新用户
        /// <summary>
        /// 插入新用户的SQL语句
        /// </summary>
        private const string INSERT_Account = "INSERT INTO tbUser" +
            "(cnvcUserId,cniUserTypeId,cnvcUserName,cnvcUserPassword,cncStationThreeCode,cnvcUserDepartment," +
            "cniRefreshInterval,cniSplashAutoStop,cniSplashSeconds,cniSoundType,cniStartGuarantee,cniGuaranteeMinutes," + 
            "cniBoarding,cniBoardingMinutes,cniMCCReady,cniMCCReadyMinutes,cniMCCRelease,cniMCCReleasMinutes,cniTDWNPromt," +
            "cniTDWNMinutes,cniTOFFPromt,cniTOFFMinutes,cniIntermissionPrompt,cniIntermissionMinutes,cniClosePaxCabinPromt," +
            "cniDisplayAll,cniDisplayDelay,cniDelayMinutes,cniDisplayDiversion,cniDisplayIntermission,cniDisplayTDWN," +
            "cniDisplayTOFF,cniDisplayClosePaxCabin,cniMaxUser)" +
            " VALUES(@PARM_cnvcUserId,@PARM_cniUserTypeId,@PARM_cnvcUserName,@PARM_cnvcUserPassword,@PARM_cncStationThreeCode,@PARM_cnvcUserDepartment,"+
            "@PARM_cniRefreshInterval,@PARM_cniSplashAutoStop,@PARM_cniSplashSeconds,@PARM_cniSoundType,@PARM_cniStartGuarantee,@PARM_cniGuaranteeMinutes,"+
            "@PARM_cniBoarding,@PARM_cniBoardingMinutes,@PARM_cniMCCReady,@PARM_cniMCCReadyMinutes,@PARM_cniMCCRelease,@PARM_cniMCCReleasMinutes,@PARM_cniTDWNPromt,"+
            "@PARM_cniTDWNMinutes,@PARM_cniTOFFPromt,@PARM_cniTOFFMinutes,@PARM_cniIntermissionPrompt,@PARM_cniIntermissionMinutes,@PARM_cniClosePaxCabinPromt," +
            "@PARM_cniDisplayAll,@PARM_cniDisplayDelay,@PARM_cniDelayMinutes,@PARM_cniDisplayDiversion,@PARM_cniDisplayIntermission,@PARM_cniDisplayTDWN," +
            "@PARM_cniDisplayTOFF,@PARM_cniDisplayClosePaxCabin,@PARM_cniMaxUser)";

        /// <summary>
        /// 组合插入用户参数
        /// </summary>
        /// <param name="accountBM">用户对象实体</param>
        /// <returns>插入用户参数</returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Account);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcUserId", SqlDbType.NVarChar, 20),
                    new SqlParameter("@PARM_cniUserTypeId",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cnvcUserName", SqlDbType.NVarChar,20),
                    new SqlParameter("@PARM_cnvcUserPassword",SqlDbType.VarChar,100),
                    new SqlParameter("@PARM_cncStationThreeCode", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcUserDepartment", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cniRefreshInterval", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniSplashAutoStop", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniSplashSeconds",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniSoundType", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniStartGuarantee",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniGuaranteeMinutes", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniBoarding", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniBoardingMinutes", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMCCReady", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniMCCReadyMinutes",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMCCRelease", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMCCReleasMinutes",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniTDWNPromt", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniTDWNMinutes", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniTOFFPromt", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniTOFFMinutes",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniIntermissionPrompt", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniIntermissionMinutes", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniClosePaxCabinPromt", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayAll", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayDelay", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDelayMinutes", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayDiversion", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayIntermission", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayTDWN", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayTOFF", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayClosePaxCabin", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMaxUser", SqlDbType.Int, 0)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Account, parms);
            }
            parms[0].Value = accountBM.UserId;
            parms[1].Value = accountBM.UserTypeId;
            parms[2].Value = accountBM.UserName;
            parms[3].Value = accountBM.UserPassword;
            parms[4].Value = accountBM.StationThreeCode;
            parms[5].Value = accountBM.UserDepartment;
            parms[6].Value = accountBM.RefreshInterval;
            parms[7].Value = accountBM.SplashAutoStop;
            parms[8].Value = accountBM.SplashSeconds;
            parms[9].Value = accountBM.SoundType;
            parms[10].Value = accountBM.StartGuarantee;
            parms[11].Value = accountBM.GuaranteeMinutes;
            parms[12].Value = accountBM.Boarding;
            parms[13].Value = accountBM.BoardingMinutes;
            parms[14].Value = accountBM.MCCReady;
            parms[15].Value = accountBM.MCCReadyMinutes;
            parms[16].Value = accountBM.MCCRelease;
            parms[17].Value = accountBM.MCCReleasMinutes;
            parms[18].Value = accountBM.TDWNPromt;
            parms[19].Value = accountBM.TDWNMinutes;
            parms[20].Value = accountBM.TOFFPromt;
            parms[21].Value = accountBM.TOFFMinutes;
            parms[22].Value = accountBM.IntermissionPrompt;
            parms[23].Value = accountBM.IntermissionMinutes;
            parms[24].Value = accountBM.ClosePaxCabinPromt;
            parms[25].Value = accountBM.DisplayAll;
            parms[26].Value = accountBM.DisplayDelay;
            parms[27].Value = accountBM.DelayMinutes;
            parms[28].Value = accountBM.DisplayDiversion;
            parms[29].Value = accountBM.DisplayIntermission;
            parms[30].Value = accountBM.DisplayTDWN;
            parms[31].Value = accountBM.DisplayTOFF;
            parms[32].Value = accountBM.DisplayClosePaxCabin;
            parms[33].Value = accountBM.MaxUser;

            return parms;
        }

        /// <summary>
        /// 插入新用户
        /// </summary>
        /// <param name="accountBM">用户对象实体</param>
        /// <returns>1：成功；0：失败</returns>
        public int Insert(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = InsertParameters(accountBM);

            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, INSERT_Account, parms);

            return retVal;
        }
        #endregion

        #region  更新用户信息
        /// <summary>
        /// 更新所有用户信息SQL语句
        /// </summary>
        private const string UPDATE_UpdateAllInfo = "UPDATE tbUser SET cniUserTypeId = @PARM_cniUserTypeId, " +
            "cnvcUserName = @PARM_cnvcUserName," +
            "cncStationThreeCode = @PARM_cncStationThreeCode," +
            "cnvcUserDepartment = @PARM_cnvcUserDepartment," +
            "cnvcUserPassword = @PARM_cnvcUserPassword," +
            "cniRefreshInterval = @PARM_cniRefreshInterval," +
            "cniSplashAutoStop = @PARM_cniSplashAutoStop," +
            "cniSplashSeconds = @PARM_cniSplashSeconds," +
            "cniSoundType = @PARM_cniSoundType," +
            "cniStartGuarantee = @PARM_cniStartGuarantee," +
            "cniGuaranteeMinutes = @PARM_cniGuaranteeMinutes," +
            "cniBoarding = @PARM_cniBoarding," +
            "cniBoardingMinutes = @PARM_cniBoardingMinutes," +
            "cniMCCReady = @PARM_cniMCCReady," +
            "cniMCCReadyMinutes = @PARM_cniMCCReadyMinutes," +
            "cniMCCRelease = @PARM_cniMCCRelease," +
            "cniMCCReleasMinutes = @PARM_cniMCCReleasMinutes," +
            "cniTDWNPromt = @PARM_cniTDWNPromt," +
            "cniTDWNMinutes = @PARM_cniTDWNMinutes," +
            "cniTOFFPromt = @PARM_cniTOFFPromt," +
            "cniTOFFMinutes = @PARM_cniTOFFMinutes," +
            "cniIntermissionPrompt = @PARM_cniIntermissionPrompt," +
            "cniIntermissionMinutes = @PARM_cniIntermissionMinutes," +
            "cniClosePaxCabinPromt = @PARM_cniClosePaxCabinPromt," +
            "cniDisplayAll = @PARM_cniDisplayAll," +
            "cniDisplayDelay = @PARM_cniDisplayDelay," +
            "cniDelayMinutes = @PARM_cniDelayMinutes," +
            "cniDisplayDiversion = @PARM_cniDisplayDiversion," +
            "cniDisplayIntermission = @PARM_cniDisplayIntermission," +
            "cniDisplayTDWN = @PARM_cniDisplayTDWN," +
            "cniDisplayTOFF = @PARM_cniDisplayTOFF," +
            "cniDisplayClosePaxCabin = @PARM_cniDisplayClosePaxCabin," +
            "cniMaxUser = @PARM_cniMaxUser " +
            " WHERE  cnvcUserId = @PARM_cnvcUserId";                   
                  
        /// <summary>
        /// 组合更新所有用户信息参数
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>组合参数</returns>
        private SqlParameter[] UpdateAllInfoParameters(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_UpdateAllInfo);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {                    
                    new SqlParameter("@PARM_cniUserTypeId",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cnvcUserName", SqlDbType.NVarChar,20),
                    new SqlParameter("@PARM_cnvcUserPassword",SqlDbType.VarChar,100),
                    new SqlParameter("@PARM_cncStationThreeCode", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcUserDepartment", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cniRefreshInterval", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniSplashAutoStop", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniSplashSeconds",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniSoundType", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniStartGuarantee",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniGuaranteeMinutes", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniBoarding", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniBoardingMinutes", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMCCReady", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniMCCReadyMinutes",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMCCRelease", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMCCReleasMinutes",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniTDWNPromt", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniTDWNMinutes", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniTOFFPromt", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniTOFFMinutes",SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniIntermissionPrompt", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniIntermissionMinutes", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniClosePaxCabinPromt", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayAll", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayDelay", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDelayMinutes", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayDiversion", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayIntermission", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayTDWN", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayTOFF", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniDisplayClosePaxCabin", SqlDbType.Int,0),
                    new SqlParameter("@PARM_cniMaxUser", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserId", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_UpdateAllInfo, parms);
            }            
            parms[0].Value = accountBM.UserTypeId;
            parms[1].Value = accountBM.UserName;
            parms[2].Value = accountBM.UserPassword;
            parms[3].Value = accountBM.StationThreeCode;
            parms[4].Value = accountBM.UserDepartment;
            parms[5].Value = accountBM.RefreshInterval;
            parms[6].Value = accountBM.SplashAutoStop;
            parms[7].Value = accountBM.SplashSeconds;
            parms[8].Value = accountBM.SoundType;
            parms[9].Value = accountBM.StartGuarantee;
            parms[10].Value = accountBM.GuaranteeMinutes;
            parms[11].Value = accountBM.Boarding;
            parms[12].Value = accountBM.BoardingMinutes;
            parms[13].Value = accountBM.MCCReady;
            parms[14].Value = accountBM.MCCReadyMinutes;
            parms[15].Value = accountBM.MCCRelease;
            parms[16].Value = accountBM.MCCReleasMinutes;
            parms[17].Value = accountBM.TDWNPromt;
            parms[18].Value = accountBM.TDWNMinutes;
            parms[19].Value = accountBM.TOFFPromt;
            parms[20].Value = accountBM.TOFFMinutes;
            parms[21].Value = accountBM.IntermissionPrompt;
            parms[22].Value = accountBM.IntermissionMinutes;
            parms[23].Value = accountBM.ClosePaxCabinPromt;
            parms[24].Value = accountBM.DisplayAll;
            parms[25].Value = accountBM.DisplayDelay;
            parms[26].Value = accountBM.DelayMinutes;
            parms[27].Value = accountBM.DisplayDiversion;
            parms[28].Value = accountBM.DisplayIntermission;
            parms[29].Value = accountBM.DisplayTDWN;
            parms[30].Value = accountBM.DisplayTOFF;
            parms[31].Value = accountBM.DisplayClosePaxCabin;
            parms[32].Value = accountBM.MaxUser;            
            parms[33].Value = accountBM.UserId;

            return parms;
        }

        /// <summary>
        /// 更新所有用户信息
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int UpdateAllInfo(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = UpdateAllInfoParameters(accountBM);

            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_UpdateAllInfo, parms);

            return retVal;
        }
        #endregion

        #region  //删除用户
        /// <summary>
        /// 删除用户SQL语句
        /// </summary>
        private const string DELETE_Account = "DELETE from tbUser " +
           " WHERE cnvcUserId = @PARM_cnvcUserId";

        /// <summary>
        /// 组合删除用户参数
        /// </summary>
        /// <param name="accountBM">用户对象实体</param>
        /// <returns>删除用户参数</returns>
        private SqlParameter[] DeleteParameters(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, DELETE_Account);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcUserId",  SqlDbType.NVarChar,20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, DELETE_Account, parms);
            }
            parms[0].Value = accountBM.UserId;
            return parms;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="accountBM">用户对象实体</param>
        /// <returns>1：成功；0：失败</returns>
        public int Delete(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = DeleteParameters(accountBM);
            int retVAL = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, DELETE_Account, parms);
            return retVAL;
        }
        #endregion

        #region 更新用户密码
        /// <summary>
        /// 更新用户密码SQL语句
        /// </summary>
        private const string UPDATE_UpdatePassword = "UPDATE tbUser SET cnvcUserPassword = @PARM_cnvcUserPassword " +
            " WHERE cnvcUserId = @PARM_cnvcUserId";

        /// <summary>
        /// 组合更新用户密码参数
        /// </summary>
        /// <param name="accountBM">用户对象实体</param>
        /// <returns>更新用户密码参数</returns>
        private SqlParameter[] UpdatePasswordParameters(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_UpdatePassword);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcUserPassword", SqlDbType.VarChar,100),
                    new SqlParameter("@PARM_cnvcUserId", SqlDbType.NVarChar,20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_UpdatePassword, parms);
            }
            parms[0].Value = accountBM.UserPassword;
            parms[1].Value = accountBM.UserId;
            return parms;
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>1：成功；0：失败</returns>
        public int UpdatePassword(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = UpdatePasswordParameters(accountBM);

            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_UpdatePassword, parms);

            return retVal;
        }
        #endregion

        #region 更新登陆用户数
        /// <summary>
        /// 更新登陆用户数SQL语句
        /// </summary>
        private const string UPDATE_LogUser = "UPDATE tbUser SET cniLogUser = @PARM_cniLogUser " +
            " WHERE cnvcUserId = @PARM_cnvcUserId";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="accountBM">用户对象实体</param>
        /// <returns></returns>
        private SqlParameter[] UpdateLogUserParameters(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_LogUser);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniLogUser", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserId", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_LogUser, parms);
            }

            parms[0].Value = accountBM.LogUser;
            parms[1].Value = accountBM.UserId;

            return parms;
        }

        /// <summary>
        /// 更新用户登陆数
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns></returns>
        public int UpdateLogUser(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = UpdateLogUserParameters(accountBM);

            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_LogUser, parms);

            return retVal;
        }
        #endregion

        #region 根据航站或用户类型查询用户
        /// <summary>
        /// 根据航站或用户类型查询用户
        /// </summary>
        /// <param name="strStationThreeCode">航站三字码</param>
        /// <param name="strUserTypeId">用户类型</param>
        /// <returns>包含符合条件的用户的DataTable</returns>
        public DataTable GetAccountByStation(string strStationThreeCode, string strUserTypeId)
        {
            string sqlSearch = "SELECT  cnvcUserId, tbUser.cniUserTypeId, cnvcUserTypeName, cnvcUserName, cnvcUserPassword, cncStationThreeCode, " + 
                "cnvcUserDepartment,cniRefreshInterval, cniSplashAutoStop,cniSplashSeconds, cniSoundType, cniStartGuarantee, cniGuaranteeMinutes," + 
                "cniBoarding, cniBoardingMinutes,cniMCCReady, cniMCCReadyMinutes,cniMCCRelease, cniMCCReleasMinutes, cniTDWNPromt, cniTDWNMinutes," +
                "cniTOFFPromt, cniTOFFMinutes, cniIntermissionPrompt,cniIntermissionMinutes, cniClosePaxCabinPromt,cniDisplayAll,cniDisplayDelay," +
                "cniDelayMinutes,cniDisplayDiversion,cniDisplayIntermission,cniDisplayTDWN,cniDisplayTOFF,cniDisplayClosePaxCabin," +
                "cniMaxUser,cniLogUser, cnvcAgentIP, cnvcAgentPort FROM tbUser,tbUserType " +
                " WHERE tbUser.cniUserTypeId=tbUserType.cniUserTypeId AND ";
            if (strStationThreeCode != "")
            {
                sqlSearch += " cncStationThreeCode = '{0}' AND ";
            }
            if (strUserTypeId != "")
            {
                sqlSearch += " tbUser.cniUserTypeId = {1} AND ";
            }

            sqlSearch = this.SqlFormat(sqlSearch);

            //SQL语句和参数匹配
            sqlSearch = string.Format(sqlSearch, strStationThreeCode, strUserTypeId);

            return SqlHelper.ExecuteDataTable(this.SqlConn, this.Transaction, CommandType.Text, sqlSearch);
        }
        #endregion

        #region 根据用户ID获取用户信息
        /// <summary>
        /// 根据用户编码获取用户帐号信息的SQL语句
        /// </summary>
        private const string SELECT_AccountByUserId = "SELECT  cnvcUserId, tbUser.cniUserTypeId, cnvcUserTypeName, cnvcUserName, cnvcUserPassword, cncStationThreeCode, " + 
                "cnvcUserDepartment,cniRefreshInterval, cniSplashAutoStop,cniSplashSeconds, cniSoundType, cniStartGuarantee, cniGuaranteeMinutes," + 
                "cniBoarding, cniBoardingMinutes,cniMCCReady, cniMCCReadyMinutes,cniMCCRelease, cniMCCReleasMinutes, cniTDWNPromt, cniTDWNMinutes," +
                "cniTOFFPromt, cniTOFFMinutes, cniIntermissionPrompt,cniIntermissionMinutes, cniClosePaxCabinPromt,cniDisplayAll,cniDisplayDelay," +
                "cniDelayMinutes,cniDisplayDiversion,cniDisplayIntermission,cniDisplayTDWN,cniDisplayTOFF,cniDisplayClosePaxCabin," +
                "cniMaxUser,cniLogUser, cnvcAgentIP, cnvcAgentPort FROM tbUser,tbUserType  " +
                " WHERE tbUser.cniUserTypeId=tbUserType.cniUserTypeId AND " +
                " cnvcUserId = @PARM_cnvcUserId";

        /// <summary>
        /// 组合根据用户编码获取帐号信息的参数
        /// </summary>
        /// <param name="strUserId">用户编码</param>
        /// <returns>根据用户编码获取帐号信息的参数信息</returns>
        private SqlParameter[] GetAccountByUserIdParameters(string strUserId)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_AccountByUserId);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcUserId", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_AccountByUserId, parms);
            }

            parms[0].Value = strUserId;
            return parms;
        }

        /// <summary>
        /// 根据用户编码获取用户信息
        /// </summary>
        /// <param name="strUserId">用户编码</param>
        /// <returns>包含用户信息的DataTable</returns>
        public DataTable GetAccountByUserId(string strUserId)
        {
            SqlParameter[] parms = GetAccountByUserIdParameters(strUserId);

            return SqlHelper.ExecuteDataTable(this.SqlConn, this.Transaction, CommandType.Text, SELECT_AccountByUserId, parms);
        }
        #endregion

        #region 增加一个在线用户
        /// <summary>
        /// 增加一个在线用户
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns>新插入记录的自动增长Id</returns>
        public int InsertOnlineUser(AccountBM accountBM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbOnlineUsers(");
            strSql.Append("cnvcUserId,cnvcIpAddr,cnvcLogOnTime,iLogFlag");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("'" + accountBM.UserId + "',");
            strSql.Append("'" + accountBM.IPAddress + "',");
            strSql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            strSql.Append("1");
            strSql.Append(");select @@Identity");

            object iOnlineUserId = SqlHelper.ExecuteScalar(this.SqlConn, CommandType.Text, strSql.ToString());
            return Convert.ToInt32(iOnlineUserId);
        }
        #endregion

        #region 更新在线用户信息
        /// <summary>
        /// 更新在线用户信息
        /// </summary>
        /// <param name="accountBM"></param>
        public void UpdateOnlineUser(AccountBM accountBM, int iOnlineUserNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbOnlineUsers set ");
            strSql.Append("cnvcLastUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            strSql.Append("iLogFlag= 1");
            strSql.Append(" where iPK=" + iOnlineUserNo + "");

            SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion

        #region 删除在线用户信息
        /// <summary>
        /// 删除在线用户信息
        /// 用户不正常退出后，删除用户在线信息
        /// </summary>
        /// <param name="accountBM"></param>
        public void DeleteOnlineUser(int iOnlineUserNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbOnlineUsers set ");
            strSql.Append("cnvcKickOffTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            strSql.Append("iLogFlag= 0");
            strSql.Append(" where iPK=" + iOnlineUserNo + "");

            SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion

        #region 用户退出系统
        /// <summary>
        /// 用户退出系统
        /// 正常退出时记录退出时间
        /// </summary>
        /// <param name="iOnlineUserNo"></param>
        public void LogOffOnlineUser(int iOnlineUserNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbOnlineUsers set ");
            strSql.Append("cnvcLogOffTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            strSql.Append("iLogFlag= 0");
            strSql.Append(" where iPK=" + iOnlineUserNo + "");

            SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, strSql.ToString());
        }
        #endregion

        #region 查询在线用户数量
        /// <summary>
        /// 查询在线用户数量
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int SelectOnlineUserCount(AccountBM accountBM)
        {
            string strSql = "Select count (cnvcUserId) From tbOnlineUsers where iLogFlag = 1 and cnvcUserId = '" + accountBM.UserId + "'";

            object oCount = SqlHelper.ExecuteScalar(this.SqlConn, CommandType.Text, strSql);
            int iOnlineUserCount = 0;
            if (oCount != null)
            {
                iOnlineUserCount = Convert.ToInt16(oCount);
            }
            return iOnlineUserCount;
        }
        #endregion

        #region 查询在线用户列表
        /// <summary>
        /// 查询在线用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOnlineUsersList()
        {
            string strSql = "select iPK,cnvcLogOnTime,cnvcLastUpdateTime From tbOnlineUsers where iLogFlag = 1";

            DataTable dt = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql);
            return dt;
        }
        #endregion
    }
}
