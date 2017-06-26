using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ACARS变更数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-15
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSLegsDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ACARSLegsDA()
        {
        }

        /// <summary>
        /// 从文本文件读取航班动态信息
        /// </summary>
        /// <param name="strFullPath">文件路径</param>
        /// <returns>ACARS航班动态文本信息</returns>
        public string GetACARSLegsBMFromFile(string strFullPath)
        {
            string strACARSLegsInfo = "";

            FileStream fileStream = new FileStream(strFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader streamReader = new StreamReader(fileStream);

            try
            {
                strACARSLegsInfo = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                streamReader.Close();
                fileStream.Close();
            }
            return strACARSLegsInfo;
        }

        #region 更新航班动态的ACARS到达信息
        private const string UPDATE_ACARSOnInfor = "UPDATE tbGuaranteeInfor SET " +
            " cncACARSTDWN = @PARM_cncACARSTDWN" +            
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        private SqlParameter[] UpdateOnInforParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_ACARSOnInfor);

            if (parms == null)
            {
                parms = new SqlParameter[] 
                {
                    new SqlParameter("@PARM_cncACARSTDWN", SqlDbType.Char, 4),                    
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.NVarChar, 8),
                    new SqlParameter("@PARM_cniLegNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_ACARSOnInfor, parms);
            }

            parms[0].Value = DateTime.Parse(acarsLegsBM.TDWN).ToString("HHmm");           
            parms[1].Value = originalLegsBM.DATOP;
            parms[2].Value = originalLegsBM.FLTID;
            parms[3].Value = originalLegsBM.LEGNO;
            parms[4].Value = originalLegsBM.AC;

            return parms;
        }
        /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSOnInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateOnInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSOnInfor, parms);
            return retVal;
        }
        #endregion

        #region 更新航班动态的ACARS挡抡挡信息
        private const string UPDATE_ACARSInInfor = "UPDATE tbGuaranteeInfor SET " +
            " cncACARSATA = @PARM_cncACARSATA" +
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        private SqlParameter[] UpdateInInforParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_ACARSInInfor);

            if (parms == null)
            {
                parms = new SqlParameter[] 
                {
                    new SqlParameter("@PARM_cncACARSATA", SqlDbType.Char, 4),                    
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.NVarChar, 8),
                    new SqlParameter("@PARM_cniLegNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_ACARSInInfor, parms);
            }

            parms[0].Value = DateTime.Parse(acarsLegsBM.ATA).ToString("HHmm");
            parms[1].Value = originalLegsBM.DATOP;
            parms[2].Value = originalLegsBM.FLTID;
            parms[3].Value = originalLegsBM.LEGNO;
            parms[4].Value = originalLegsBM.AC;

            return parms;
        }
        /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSInInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateInInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSInInfor, parms);
            return retVal;
        }
        #endregion

        #region 更新航班动态的ACARS退出信息
        private const string UPDATE_ACARSOutInfor = "UPDATE tbGuaranteeInfor SET " +
            " cncACARSOUT = @PARM_cncPushTime" +            
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        private SqlParameter[] UpdateOutInforParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_ACARSOutInfor);

            if (parms == null)
            {
                parms = new SqlParameter[] 
                {
                    new SqlParameter("@PARM_cncPushTime", SqlDbType.Char, 4),                    
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.NVarChar, 8),
                    new SqlParameter("@PARM_cniLegNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_ACARSOutInfor, parms);
            }

            parms[0].Value = DateTime.Parse(acarsLegsBM.PushTime).ToString("HHmm");            
            parms[1].Value = originalLegsBM.DATOP;
            parms[2].Value = originalLegsBM.FLTID;
            parms[3].Value = originalLegsBM.LEGNO;
            parms[4].Value = originalLegsBM.AC;

            return parms;
        }
        /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSOutInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateOutInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSOutInfor, parms);
            return retVal;
        }
        #endregion

        #region 更新航班动态的ACARS起飞信息
        private const string UPDATE_ACARSOffInfor = "UPDATE tbGuaranteeInfor SET " +            
            " cncACARSTOFF = @PARM_cncACARSTOFF " +
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        private SqlParameter[] UpdateOffInforParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_ACARSOffInfor);

            if (parms == null)
            {
                parms = new SqlParameter[] 
                {                   
                    new SqlParameter("@PARM_cncACARSTOFF", SqlDbType.Char, 4),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.NVarChar, 8),
                    new SqlParameter("@PARM_cniLegNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_ACARSOffInfor, parms);
            }
           
            parms[0].Value = DateTime.Parse(acarsLegsBM.TOFF).ToString("HHmm");
            parms[1].Value = originalLegsBM.DATOP;
            parms[2].Value = originalLegsBM.FLTID;
            parms[3].Value = originalLegsBM.LEGNO;
            parms[4].Value = originalLegsBM.AC;

            return parms;
        }
        /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSOffInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateOffInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSOffInfor, parms);
            return retVal;
        }
        #endregion
    }
}
