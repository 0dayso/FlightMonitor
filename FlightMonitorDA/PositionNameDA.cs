using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.DataHelper;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 席位数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionNameDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionNameDA()
        {
        }

        #region 获取所有席位名称
        private const string SELECT_AllPositionName = "SELECT * FROM tbPositionName";

        /// <summary>
        /// 获取所有席位名称
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPositionName()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllPositionName);
        }
        #endregion

        #region 添加新的席位
        private const string INSERT_Position = "INSERT INTO tbPositionName VALUES(@PARM_cnvcPositionName)";

        private SqlParameter[] InsertPositionParameters(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Position);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcPositionName", SqlDbType.NVarChar, 50)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Position, parms);
            }

            parms[0].Value = positionNameBM.PositionName;

            return parms;
        }

        /// <summary>
        /// 插入一个席位
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public int InsertPositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            SqlParameter[] parms = InsertPositionParameters(positionBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, INSERT_Position, parms);
            return retVal;
        }
        #endregion

        #region 删除一个席位
        private const string DELETE_Position = "DELETE FROM tbPositionName WHERE cniPositionID = @PARM_cniPositionID";

        private SqlParameter[] DeletePositionParameters(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, DELETE_Position);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniPositionID", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, DELETE_Position, parms);
            }

            parms[0].Value = positionNameBM.PositionID;

            return parms;
        }

        /// <summary>
        /// 删除一个席位名称
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public int DeletePositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            SqlParameter[] parms = DeletePositionParameters(positionBM);

            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, DELETE_Position, parms);

            return retVal;
        }
        #endregion

        #region 根据席位编号获取一个席位
        private const string SELECT_PositionByPositionID = "SELECT * FROM tbPositionName WHERE cniPositionID = @PARM_cniPositionID";

        /// <summary>
        /// 根据席位编号获取席位信息
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public DataTable GetPositionByID(FlightMonitorBM.PositionNameBM positionBM)
        {
            SqlParameter[] parms = DeletePositionParameters(positionBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_PositionByPositionID, parms);

           
        }
        #endregion
    }
}
