using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 席位信息访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionInforDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionInforDA()
        {
        }

        #region 获取某席位的所有飞机号
        private const string SELECT_InforByPositionId = "SELECT cniInfoid,cnvcLONG_REG,tbPositionInfo.cniPositionID AS cniPositionID,cnvcPositionName FROM tbPositionInfo,tbPositionName WHERE " +
            "tbPositionInfo.cniPositionID = tbPositionName.cniPositionID AND " +
            "tbPositionInfo.cniPositionID = @PARM_cniPositionID ORDER BY cnvcLONG_REG";

        private SqlParameter[] SelectInforByPositionIdParameters(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_InforByPositionId);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniPositionID", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_InforByPositionId, parms);
            }

            parms[0].Value = positionNameBM.PositionID;

            return parms;
        }

        /// <summary>
        /// 根据席位编号获取属于该席位的所有飞机的信息
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public DataTable GetInforByPositionId(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SelectInforByPositionIdParameters(positionNameBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_InforByPositionId, parms);
        }
        #endregion
       

        #region 删除某一席位的所有的信息
        private const string DELETE_InforByPositionId = "DELETE FROM tbPositionInfo WHERE " +
            "tbPositionInfo.cniPositionID = @PARM_cniPositionID";

        /// <summary>
        /// 删除某席位的所有飞机
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public int DeleteInforByPositionId(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SelectInforByPositionIdParameters(positionNameBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, DELETE_InforByPositionId, parms);

            return retVal;
        }
        #endregion

        #region 插入一条信息
        private const string INSERT_Infor = "INSERT INTO tbPositionInfo VALUES(" +
            "@PARM_cnvcLONG_REG,@PARM_cniPositionID)";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        private SqlParameter[] InsertInforParameters(FlightMonitorBM.PositionInforBM positionInforBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Infor);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_cniPositionID", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Infor, parms);
            }

            parms[0].Value = positionInforBM.LONG_REG;
            parms[1].Value = positionInforBM.PositionID;

            return parms;
        }

        /// <summary>
        /// 添加一条席位信息
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public int InsertInfor(FlightMonitorBM.PositionInforBM positionInforBM)
        {
            SqlParameter[] parms = InsertInforParameters(positionInforBM);

            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, INSERT_Infor, parms);

            return retVal;
        }
        #endregion
    }
}
