using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 保障日记信息操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-05-22
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeRecordDA : SqlDatabase
    {

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="guaranteeRecordBM">保障日记信息对象</param>
        /// <returns></returns>
        public int Add(GuaranteeRecordBM guaranteeRecordBM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbGuaranteeRecord(");
            strSql.Append("cncDATOP,cnvcFLTID,cniLegNO,cnvcAC,cncFlightDate,cnvcFlightNo,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncStation,cniGuaranteeRecordCaptionID,cndGuaranteeTime,cnvcGuaranteeContent,cndOperationTime,cnvcUserID,cnvcUserName,cnvcUserDepartment)");
            strSql.Append(" values (");
            strSql.Append("@cncDATOP,@cnvcFLTID,@cniLegNO,@cnvcAC,@cncFlightDate,@cnvcFlightNo,@cnvcLONG_REG,@cncDEPSTN,@cncARRSTN,@cncSTD,@cncStation,@cniGuaranteeRecordCaptionID,@cndGuaranteeTime,@cnvcGuaranteeContent,@cndOperationTime,@cnvcUserID,@cnvcUserName,@cnvcUserDepartment)");
            SqlParameter[] parameters = {
					new SqlParameter("@cncDATOP", SqlDbType.Char,10),
					new SqlParameter("@cnvcFLTID", SqlDbType.VarChar,8),
					new SqlParameter("@cniLegNO", SqlDbType.Int,4),
					new SqlParameter("@cnvcAC", SqlDbType.VarChar,9),
					new SqlParameter("@cncFlightDate", SqlDbType.Char,10),
					new SqlParameter("@cnvcFlightNo", SqlDbType.VarChar,8),
					new SqlParameter("@cnvcLONG_REG", SqlDbType.VarChar,10),
					new SqlParameter("@cncDEPSTN", SqlDbType.Char,3),
					new SqlParameter("@cncARRSTN", SqlDbType.Char,3),
					new SqlParameter("@cncSTD", SqlDbType.Char,19),
					new SqlParameter("@cncStation", SqlDbType.Char,3),
					new SqlParameter("@cniGuaranteeRecordCaptionID", SqlDbType.Int,4),
					new SqlParameter("@cndGuaranteeTime", SqlDbType.DateTime),
					new SqlParameter("@cnvcGuaranteeContent", SqlDbType.VarChar,500),
					new SqlParameter("@cndOperationTime", SqlDbType.DateTime),
					new SqlParameter("@cnvcUserID", SqlDbType.NVarChar,20),
					new SqlParameter("@cnvcUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@cnvcUserDepartment", SqlDbType.NVarChar,50)};
            parameters[0].Value = guaranteeRecordBM.cncDATOP;
            parameters[1].Value = guaranteeRecordBM.cnvcFLTID;
            parameters[2].Value = guaranteeRecordBM.cniLegNO;
            parameters[3].Value = guaranteeRecordBM.cnvcAC;
            parameters[4].Value = guaranteeRecordBM.cncFlightDate;
            parameters[5].Value = guaranteeRecordBM.cnvcFlightNo;
            parameters[6].Value = guaranteeRecordBM.cnvcLONG_REG;
            parameters[7].Value = guaranteeRecordBM.cncDEPSTN;
            parameters[8].Value = guaranteeRecordBM.cncARRSTN;
            parameters[9].Value = guaranteeRecordBM.cncSTD;
            parameters[10].Value = guaranteeRecordBM.cncStation;
            parameters[11].Value = guaranteeRecordBM.cniGuaranteeRecordCaptionID;
            parameters[12].Value = guaranteeRecordBM.cndGuaranteeTime;
            parameters[13].Value = guaranteeRecordBM.cnvcGuaranteeContent;
            parameters[14].Value = guaranteeRecordBM.cndOperationTime;
            parameters[15].Value = guaranteeRecordBM.cnvcUserID;
            parameters[16].Value = guaranteeRecordBM.cnvcUserName;
            parameters[17].Value = guaranteeRecordBM.cnvcUserDepartment;

            //
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSql.ToString(), parameters);

            return retVal;
        }

        #endregion 增加一条数据

        #region 获取所有信息(根据航班、航站、主题)
        /// <summary>
        /// 获取所有信息(根据航班、航站、主题)
        /// </summary>
        /// <param name="cncStation"></param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <param name="cniGuaranteeRecordCaptionID"></param>
        /// <returns></returns>
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC, int cniGuaranteeRecordCaptionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.cnvcCaption, r.cniGuaranteeRecordID,r.cncDATOP,r.cnvcFLTID,r.cniLegNO,r.cnvcAC,r.cncFlightDate,r.cnvcFlightNo,r.cnvcLONG_REG,r.cncDEPSTN,r.cncARRSTN,r.cncSTD,r.cncStation,r.cniGuaranteeRecordCaptionID,r.cndGuaranteeTime,r.cnvcGuaranteeContent,r.cndOperationTime,r.cnvcUserID,r.cnvcUserName,r.cnvcUserDepartment ");
            strSql.Append(", (r.cnvcUserName " + " + '['" + " + " + "r.cnvcUserID" + " + " + "  ']') as 'cnvcUser' ");
            strSql.Append(" FROM tbGuaranteeRecord r,tbGuaranteeRecordCaption c ");
            strSql.Append(" where r.cniGuaranteeRecordCaptionID = c.cniGuaranteeRecordCaptionID and r.cncStation=@cncStation and r.cniGuaranteeRecordCaptionID=@cniGuaranteeRecordCaptionID and r.cncDATOP=@cncDATOP and r.cnvcFLTID=@cnvcFLTID and r.cniLegNO=@cniLegNO and r.cnvcAC=@cnvcAC ");
            SqlParameter[] parameters = {
					new SqlParameter("@cncStation", SqlDbType.Char,10),
					new SqlParameter("@cniGuaranteeRecordCaptionID", SqlDbType.Int,4),
					new SqlParameter("@cncDATOP", SqlDbType.Char,10),
					new SqlParameter("@cnvcFLTID", SqlDbType.VarChar,50),
					new SqlParameter("@cniLegNO", SqlDbType.Int,4),
					new SqlParameter("@cnvcAC", SqlDbType.VarChar,50)};
            parameters[0].Value = cncStation;
            parameters[1].Value = cniGuaranteeRecordCaptionID;
            parameters[2].Value = cncDATOP;
            parameters[3].Value = cnvcFLTID;
            parameters[4].Value = cniLegNO;
            parameters[5].Value = cnvcAC;

            DataTable dataTableResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString(), parameters);

            //返回结果
            return dataTableResult;
        }

        #endregion 获取所有信息(根据航班、航站、主题)

        #region 获取所有信息(根据航班、航站)
        /// <summary>
        /// 获取所有信息(根据航班、航站)
        /// </summary>
        /// <param name="cncStation"></param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <param name="cniGuaranteeRecordCaptionID"></param>
        /// <returns></returns>
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.cnvcCaption, r.cniGuaranteeRecordID,r.cncDATOP,r.cnvcFLTID,r.cniLegNO,r.cnvcAC,r.cncFlightDate,r.cnvcFlightNo,r.cnvcLONG_REG,r.cncDEPSTN,r.cncARRSTN,r.cncSTD,r.cncStation,r.cniGuaranteeRecordCaptionID,r.cndGuaranteeTime,r.cnvcGuaranteeContent,r.cndOperationTime,r.cnvcUserID,r.cnvcUserName,r.cnvcUserDepartment ");
            strSql.Append(", (r.cnvcUserName " + " + '['" + " + " + "r.cnvcUserID" + " + " + "  ']') as 'cnvcUser' ");
            strSql.Append(" FROM tbGuaranteeRecord r,tbGuaranteeRecordCaption c ");
            strSql.Append(" where r.cniGuaranteeRecordCaptionID = c.cniGuaranteeRecordCaptionID and r.cncStation=@cncStation and r.cncDATOP=@cncDATOP and r.cnvcFLTID=@cnvcFLTID and r.cniLegNO=@cniLegNO and r.cnvcAC=@cnvcAC ");
            SqlParameter[] parameters = {
					new SqlParameter("@cncStation", SqlDbType.Char,10),
					new SqlParameter("@cncDATOP", SqlDbType.Char,10),
					new SqlParameter("@cnvcFLTID", SqlDbType.VarChar,50),
					new SqlParameter("@cniLegNO", SqlDbType.Int,4),
					new SqlParameter("@cnvcAC", SqlDbType.VarChar,50)};
            parameters[0].Value = cncStation;
            parameters[1].Value = cncDATOP;
            parameters[2].Value = cnvcFLTID;
            parameters[3].Value = cniLegNO;
            parameters[4].Value = cnvcAC;

            DataTable dataTableResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString(), parameters);

            //返回结果
            return dataTableResult;
        }

        #endregion 获取所有信息(根据航班、航站)

    }
}
