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
    /// 飞机信息数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AC_MISCDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AC_MISCDA()
        {
        }

        #region 查询某席位可以选择的飞机号
        private const string SELECT_AirCraftByPositionId = "SELECT * FROM tbAC_MISC WHERE (tbAC_MISC.cnvcLONG_REG NOT IN " +
            "(SELECT tbAC_MISC.cnvcLONG_REG FROM tbPositionInfo,tbAC_MISC WHERE tbPositionInfo.cniPositionID = @PARM_cniPositionID " +
            "AND tbPositionInfo.cnvcLONG_REG = tbAC_MISC.cnvcLONG_REG)) ORDER BY tbAC_MISC.cnvcLONG_REG";

        private SqlParameter[] GetAirCraftByPositionIdParametes(PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_AirCraftByPositionId);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniPositionID", SqlDbType.Int, 0)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_AirCraftByPositionId, parms);
            }

            parms[0].Value = positionNameBM.PositionID;

            return parms;
        }

        /// <summary>
        /// 获取某席位没有选择的飞机号
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public DataTable GetAirCraftByPositionId(PositionNameBM positionBM)
        {
            SqlParameter[] parms = GetAirCraftByPositionIdParametes(positionBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AirCraftByPositionId, parms);
        }
        #endregion

        #region 添加新飞机注册号
        private const string INSERT_ACMISC = "INSERT INTO tbAC_MISC(cncAC,cnvcSHORT_REG,cnvcLONG_REG,cncACTYPE,cnvcOWNER) VALUES(" +
            "@PARM_cncAC, @PARM_cnvcSHORT_REG,@PARM_cnvcLONG_REG,@PARM_cncACTYPE,@PARM_cnvcOWNER)";

        private SqlParameter[] InsertParameters(AC_MISCBM ac_MISCBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_ACMISC);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncAC", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcSHORT_REG", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_cncACTYPE", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cnvcOWNER", SqlDbType.VarChar, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_ACMISC, parms);
            }

            parms[0].Value = ac_MISCBM.AC;
            parms[1].Value = ac_MISCBM.SHORT_REG;
            parms[2].Value = ac_MISCBM.LONG_REG;
            parms[3].Value = ac_MISCBM.ACTYPE;
            parms[4].Value = ac_MISCBM.OWNER;

            return parms;
        }

        /// <summary>
        /// 插入新飞机信息
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public int InsertACMISC(AC_MISCBM ac_MISCBM)
        {
            SqlParameter[] parms = InsertParameters(ac_MISCBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, INSERT_ACMISC, parms);

            return retVal;
        }

        #endregion

        #region 删除新飞机
        private const string DELETE_ACMISC = "DELETE FROM tbAC_MISC WHERE cncAC = @PARM_cncAC";

        private SqlParameter[] DeleteParameters(AC_MISCBM ac_MISCBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, DELETE_ACMISC);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncAC", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, DELETE_ACMISC, parms);
            }

            parms[0].Value = ac_MISCBM.AC;            

            return parms;
        }

        /// <summary>
        /// 删除飞机
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public int DeleteACMISC(AC_MISCBM ac_MISCBM)
        {
            SqlParameter[] parms = DeleteParameters(ac_MISCBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, DELETE_ACMISC, parms);

            return retVal;
        }
        #endregion

        #region 根据短号查询
        private const string SELECTACMISCBYAC = "SELECT * FROM tbAC_MISC WHERE cncAC = @PARM_cncAC";

        /// <summary>
        /// 根据短号获取一条记录
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public DataTable GetACMISCByAC(AC_MISCBM ac_MISCBM)
        {
            SqlParameter[] parms = DeleteParameters(ac_MISCBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECTACMISCBYAC, parms);
        }
        #endregion

        #region 获取所有飞机号列表
        private const string SelectACMISC = "SELECT * FROM tbAC_MISC";

        /// <summary>
        /// 获取所有飞机列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetACMISC()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SelectACMISC);
        }
        #endregion

        #region 获取所有飞机号列表
        private const string SelectACMISCGY = "SELECT cncACTYPE FROM tbAC_MISC group by cncACTYPE";

        /// <summary>
        /// 获取所有飞机机型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetACMISCGroupBy()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SelectACMISCGY);
        }
        #endregion
    }
}
