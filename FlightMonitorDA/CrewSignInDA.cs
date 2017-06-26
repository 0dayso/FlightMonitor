using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 机组签到数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-07-10
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CrewSignInDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CrewSignInDA()
        {
        }

        #region 查询一个航机组班签到信息
        private const string SELECT_CrewSignIn = "FeiXingYuanQianDao1";

        private SqlParameter[] GetCrewSignInParameters(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_CrewSignIn);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@ChaXunShiJian", SqlDbType.VarChar, 30),
                    new SqlParameter("@HangBanHao", SqlDbType.VarChar, 20),
                    new SqlParameter("@QiFeiJiChang", SqlDbType.VarChar, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_CrewSignIn, parms);
            }

            parms[0].Value = strQueryTime;
            parms[1].Value = strFlightNo;
            parms[2].Value = strDEPSTN;

            return parms;
        }

        /// <summary>
        /// 查询一个航班的机组签到信息
        /// </summary>
        /// <param name="strQueryTime">查询时间</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns>签到信息表</returns>
        public DataTable GetCrewSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            SqlParameter[] parms = GetCrewSignInParameters(strQueryTime, strFlightNo, strDEPSTN);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.StoredProcedure, SELECT_CrewSignIn, parms);
        }
        #endregion

        #region 根据姓名查询机组签到记录
        private const string SELECT_CrewSignTime = "qdxt_fxy_ChaXunJieGuo";

        SqlParameter[] GetCrewSignTimeParameters(string strCrewName, string strSignInFlag)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_CrewSignTime);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@jidi", SqlDbType.VarChar, 10),
                    new SqlParameter("@gongsi", SqlDbType.VarChar, 5),
                    new SqlParameter("@kaoqinren", SqlDbType.VarChar, 20),
                    new SqlParameter("@kaoqinshijian1", SqlDbType.DateTime),
                    new SqlParameter("@kaoqinshijian2", SqlDbType.DateTime),
                    new SqlParameter("@flag_type", SqlDbType.Int, 2),
                    new SqlParameter("@flag", SqlDbType.Int, 2)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_CrewSignTime, parms);
            }

            if (strSignInFlag != "")
            {
                parms[0].Value = strSignInFlag;
            }
            else
            {
                parms[0].Value = "HU";
            }

            parms[1].Value = "0206";
            parms[2].Value = strCrewName;
            parms[3].Value = DateTime.Now.ToString("yyyy-MM-dd");
            parms[4].Value = DateTime.Now.ToString("yyyy-MM-dd");
            parms[5].Value = "0";

            parms[6].Direction = ParameterDirection.Output;

            return parms;
        }

        /// <summary>
        /// 获取某位飞行员的签到时间
        /// </summary>
        /// <param name="strCrewName">飞行员名字</param>
        /// <param name="strSignInFlag">签到标识</param>
        /// <returns></returns>
        public DataTable GetCrewSignTime(string strCrewName, string strSignInFlag)
        {
            SqlParameter[] parms = GetCrewSignTimeParameters(strCrewName, strSignInFlag);

            return SqlHelper.ExecuteDataTable(this.DBConnString, CommandType.StoredProcedure, SELECT_CrewSignTime, parms);
        }
        #endregion

        #region 查询一个航班的乘务签到信息
        private const string SELECT_StewardSignIn = "ChengWuYuanQianDao1";

        /// <summary>
        /// 查询一个航班的乘务签到信息
        /// </summary>
        /// <param name="strQueryTime">查询时间</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns>签到信息表</returns>
        public DataTable GetStewardSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            SqlParameter[] parms = GetCrewSignInParameters(strQueryTime, strFlightNo, strDEPSTN);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.StoredProcedure, SELECT_StewardSignIn, parms);
        }
        #endregion

        #region 查询某位乘务员的签到时间
        private const string SELECT_StewardSignTime = "qdxt_cwy_ChaXunJieGuo";

        /// <summary>
        /// 获取某位乘务员的签到时间
        /// </summary>
        /// <param name="strStewardName">乘务员名字</param>
        /// <param name="strSignInFlag">签到标识</param>
        /// <returns></returns>
        public DataTable GetStewardSignTime(string strStewardName, string strSignInFlag)
        {
            SqlParameter[] parms = GetCrewSignTimeParameters(strStewardName, strSignInFlag);

            return SqlHelper.ExecuteDataTable(this.DBConnString, CommandType.StoredProcedure, SELECT_StewardSignTime, parms);
        }
        #endregion
    }
}
