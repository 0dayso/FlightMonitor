using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorDA.FocService;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// VIP数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-17
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class VIPDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VIPDA()
        {
        }

        /// <summary>
        /// 从FOC获取VIP信息
        /// </summary>
        /// <param name="strStartDATOP">开始日期（UTC时间）</param>
        /// <returns>从FOC返回的VIP数据集</returns>
        public DataSet GetVIPFromFoc(string strStartDATOP)
        {
            FocService.FleetWatch objFocService = new FleetWatch();
            return objFocService.GetVIPInfo(strStartDATOP);
        }

        #region 从本地数据库中获取所有VIP
        /// <summary>
        /// 以DATOP为参数，从数据库中查询所有VIP信息
        /// </summary>
        private const string SELECT_VIPByDATOP = "SELECT * FROM vw_VIP WHERE " +
            "cncDATOP>=@PARM_cncDATOP";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="strDATOP">日期 UTC时间</param>
        /// <returns></returns>
        private SqlParameter[] GetVIPByDATOPParameters(string strDATOP)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_VIPByDATOP);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_VIPByDATOP);
            }
            parms[0].Value = strDATOP;

            return parms;
        }

        /// <summary>
        /// 根据DATOP获取所有VIP
        /// </summary>
        /// <param name="strDATOP">日期</param>
        /// <returns>包含所有符合条件的VIP的数据表</returns>
        public DataTable GetVIPByDATOP(string strDATOP)
        {
            SqlParameter[] parms = GetVIPByDATOPParameters(strDATOP);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VIPByDATOP, parms);
        }

        #endregion

        #region  更新某个VIP信息
        private const string UPDATE_VIPInfor = "UPDATE tbVIP SET " +
            "cnvcPOSITION=@PARM_cnvcPOSITION," +
            "cncCLASS=@PARM_cncCLASS," +
            "cnvcVipType=@PARM_cnvcVipType," +
            "cnvcACCOMPANY_NBR=@PARM_cnvcACCOMPANY_NBR," +
            "cnvcACCOMPANY_LEADER=@PARM_cnvcACCOMPANY_LEADER," +
            "cnvcCONTRACT_NUMBER=@PARM_cnvcCONTRACT_NUMBER," +
            "cnvcINFORM_PERSON=@PARM_cnvcINFORM_PERSON," +
            "cnvcSPECIAL_REQUIREMENTS=@PARM_cnvcSPECIAL_REQUIREMENTS," +
            "cnvcREMARKS=@PARM_cnvcREMARKS WHERE " +
            "cncDATOP=@PARM_cncDATOP AND " +
            "cnvcFLTID=@PARM_cnvcFLTID AND " +
            "cncDEPSTN=@PARM_cncDEPSTN AND " +
            "cncARRSTN=@PARM_cncARRSTN AND " +
            "cnvcNAME=@PARM_cnvcNAME";

        /// <summary>
        /// 组合更新VIP信息的参数
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        private SqlParameter[] UpdateParameters(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_VIPInfor);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcPOSITION", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cncCLASS", SqlDbType.Char, 1),
                    new SqlParameter("@PARM_cnvcVipType", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcACCOMPANY_NBR", SqlDbType.VarChar,3),
                    new SqlParameter("@PARM_cnvcACCOMPANY_LEADER", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcCONTRACT_NUMBER", SqlDbType.VarChar, 40),
                    new SqlParameter("@PARM_cnvcINFORM_PERSON", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcSPECIAL_REQUIREMENTS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcREMARKS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_VIPInfor, parms);
            }

            parms[0].Value = vipBM.POSITION;
            parms[1].Value = vipBM.CLASS;
            parms[2].Value = vipBM.VipType;
            parms[3].Value = vipBM.ACCOMPANY_NBR;
            parms[4].Value = vipBM.ACCOMPANY_LEADER;
            parms[5].Value = vipBM.CONTRACT_NUMBER;
            parms[6].Value = vipBM.INFORM_PERSON;
            parms[7].Value = vipBM.SPECIAL_REQUIREMENTS;
            parms[8].Value = vipBM.REMARKS;
            parms[9].Value = vipBM.DATOP;
            parms[10].Value = vipBM.FLTID;
            parms[11].Value = vipBM.DEPSTN;
            parms[12].Value = vipBM.ARRSTN;
            parms[13].Value = vipBM.Name;

            return parms;
        }

        /// <summary>
        /// 更新VIP所有信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public int UpdateVipInfor(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = UpdateParameters(vipBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_VIPInfor, parms);
            return retVal;
        }
        #endregion

        #region 插入一条新的VIP信息
        private const string INSERT_VIP = "INSERT INTO tbVIP VALUES(@PARM_cncDATOP,@PARM_cnvcFLTID," +
            "@PARM_cncDEPSTN,@PARM_cncARRSTN,@PARM_cnvcNAME,@PARM_cnvcPOSITION,@PARM_cncCLASS," +
            "@PARM_cnvcVipType,@PARM_cnvcACCOMPANY_NBR,@PARM_cnvcACCOMPANY_LEADER,@PARM_cnvcCONTRACT_NUMBER," +
            "@PARM_cnvcINFORM_PERSON,@PARM_cnvcSPECIAL_REQUIREMENTS,@PARM_cnvcREMARKS,@PARM_cnvcDataSource,0,0)";
        
        /// <summary>
        /// 组合插入参数
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_VIP);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cnvcPOSITION", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cncCLASS", SqlDbType.Char, 1),
                    new SqlParameter("@PARM_cnvcVipType", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcACCOMPANY_NBR", SqlDbType.VarChar,3),
                    new SqlParameter("@PARM_cnvcACCOMPANY_LEADER", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcCONTRACT_NUMBER", SqlDbType.VarChar, 40),
                    new SqlParameter("@PARM_cnvcINFORM_PERSON", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcSPECIAL_REQUIREMENTS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcREMARKS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcDataSource", SqlDbType.VarChar, 10)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_VIP, parms);
            }

            parms[0].Value = vipBM.DATOP;
            parms[1].Value = vipBM.FLTID;
            parms[2].Value = vipBM.DEPSTN;
            parms[3].Value = vipBM.ARRSTN;
            parms[4].Value = vipBM.Name;
            parms[5].Value = vipBM.POSITION;
            parms[6].Value = vipBM.CLASS;
            parms[7].Value = vipBM.VipType;
            parms[8].Value = vipBM.ACCOMPANY_NBR;
            parms[9].Value = vipBM.ACCOMPANY_LEADER;
            parms[10].Value = vipBM.CONTRACT_NUMBER;
            parms[11].Value = vipBM.INFORM_PERSON;
            parms[12].Value = vipBM.SPECIAL_REQUIREMENTS;
            parms[13].Value = vipBM.REMARKS;
            parms[14].Value = vipBM.DataSource;

            return parms;
        }

        /// <summary>
        /// 插入一条VIP信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public int InsertVIP(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = InsertParameters(vipBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, INSERT_VIP, parms);
            return retVal;
        }
        #endregion

        #region 删除VIP
        private const string DELETE_VIP = "DELETE FROM tbVIP WHERE " +
            "cncDATOP=@PARM_cncDATOP AND " +
            "cnvcFLTID=@PARM_cnvcFLTID AND " +
            "cncDEPSTN=@PARM_cncDEPSTN AND " +
            "cncARRSTN=@PARM_cncARRSTN";

        /// <summary>
        /// 组合主参数
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        private SqlParameter[] DeleteParameters(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, DELETE_VIP);
            if(parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, DELETE_VIP, parms);
            }
            parms[0].Value = vipBM.DATOP;
            parms[1].Value = vipBM.FLTID;
            parms[2].Value = vipBM.DEPSTN;
            parms[3].Value = vipBM.ARRSTN;
            parms[4].Value = vipBM.Name;

            return parms;
        }

        /// <summary>
        /// 删除一条VIP信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public int DeleteVIP(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = DeleteParameters(vipBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, DELETE_VIP, parms);
            return retVal;
        }

        #endregion

        #region 根据姓名和航班信息获取一位VIP
        private const string SELECT_VIPByName = "SELECT * FROM tbVIP WHERE " +
            "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND cncARRSTN = @PARM_cncARRSTN AND " +
            "cnvcNAME = @PAMR_cnvcNAME";

        private SqlParameter[] GetVIPByNameParameters(VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_VIPByName);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PAMR_cnvcNAME", SqlDbType.NVarChar,50)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_VIPByName, parms);
            }

            parms[0].Value = vipBM.DATOP;
            parms[1].Value = vipBM.FLTID;
            parms[2].Value = vipBM.DEPSTN;
            parms[3].Value = vipBM.ARRSTN;
            parms[4].Value = vipBM.Name;

            return parms;
        }


        /// <summary>
        /// 根据旅客姓名和航班信息获取VIP
        /// </summary>
        /// <param name="vipBM"></param>
        /// <returns></returns>
        public DataTable GetVIPByName(VIPBM vipBM)
        {
            SqlParameter[] parms = GetVIPByNameParameters(vipBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VIPByName, parms);
        }
        #endregion

        #region 获取某航班某没有逻辑删除或逻辑删除的航班的VIP
        private const string SELECT_VIPByFlight = "SELECT * FROM tbVIP WHERE " +
            "cncDATOP = @PARM_cncDATOP AND " +
            "cnvcFLTID = @PARM_cnvcFLTID AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND " +
            "cncARRSTN = @PARM_cncARRSTN AND " +
            "cnbDeleteTag = @PARM_cnbDeleteTag";

        private SqlParameter[] GetVIPByFlightParameters(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_VIPByFlight);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnbDeleteTag", SqlDbType.Bit, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_VIPByFlight, parms);
            }

            parms[0].Value = changeLegsBM.DATOP;
            parms[1].Value = changeLegsBM.FLTID;
            parms[2].Value = changeLegsBM.DEPSTN;
            parms[3].Value = changeLegsBM.ARRSTN;
            parms[4].Value = iDeleteTag;

            return parms;
        }

        /// <summary>
        /// 获取航班的VIP信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public DataTable GetVIPByFlight(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            SqlParameter[] parms = GetVIPByFlightParameters(changeLegsBM, iDeleteTag);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VIPByFlight, parms);
        }
        #endregion

        #region 逻辑删除或添加一个VIP
        private const string UPDATE_DeleteTag = "UPDATE tbVIP SET cnbDeleteTag = @PARM_cnbDeleteTag WHERE " +
             "cncDATOP = @PARM_cncDATOP AND " +
            "cnvcFLTID = @PARM_cnvcFLTID AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND " +
            "cncARRSTN = @PARM_cncARRSTN AND " +
            "cnvcNAME = @PARM_cnvcNAME";

        private SqlParameter[] UpdateDeleteTagParameters(VIPBM vipBM, int iDeleteTag)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_DeleteTag);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnbDeleteTag", SqlDbType.Bit, 0),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_DeleteTag, parms);
            }

            parms[0].Value = iDeleteTag;
            parms[1].Value = vipBM.DATOP;
            parms[2].Value = vipBM.FLTID;
            parms[3].Value = vipBM.DEPSTN;
            parms[4].Value = vipBM.ARRSTN;
            parms[5].Value = vipBM.Name;
            

            return parms;
        }

        /// <summary>
        /// 逻辑删除或添加一个航班
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public int UpdateDeleteTag(VIPBM vipBM, int iDeleteTag)
        {
            SqlParameter[] parms = UpdateDeleteTagParameters(vipBM, iDeleteTag);

            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_DeleteTag, parms);
        }
        #endregion
    }
}
