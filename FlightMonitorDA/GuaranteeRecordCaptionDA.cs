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
    /// �����ռ�������Ϣ����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-05-22
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeRecordCaptionDA : SqlDatabase
    {
        #region �Ƿ���ڸü�¼
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string cnvcCaption, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC, string cncStation)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) as 'count' from tbGuaranteeRecordCaption");
            strSql.Append(" where cnvcCaption=@cnvcCaption and cncDATOP=@cncDATOP and cnvcFLTID=@cnvcFLTID and cniLegNO=@cniLegNO and cnvcAC=@cnvcAC and  cncStation = @cncStation");
            SqlParameter[] parameters = {
					new SqlParameter("@cnvcCaption", SqlDbType.VarChar,50),
					new SqlParameter("@cncDATOP", SqlDbType.Char,10),
					new SqlParameter("@cnvcFLTID", SqlDbType.VarChar,50),
					new SqlParameter("@cniLegNO", SqlDbType.Int,4),
					new SqlParameter("@cnvcAC", SqlDbType.VarChar,50),
                    new SqlParameter("@cncStation", SqlDbType.VarChar,3)};

            parameters[0].Value = cnvcCaption;
            parameters[1].Value = cncDATOP;
            parameters[2].Value = cnvcFLTID;
            parameters[3].Value = cniLegNO;
            parameters[4].Value = cnvcAC;
            parameters[5].Value = cncStation;

            DataTable dataTableResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString(), parameters);
            if (Convert.ToInt32(dataTableResult.Rows[0]["count"].ToString()) == 0)
                return false;
            else
                return true;
        }

        #endregion �Ƿ���ڸü�¼

        #region  ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="guaranteeRecordCaptionBM">�����ռ�������Ϣ����</param>
        /// <returns></returns>
        public int Add(GuaranteeRecordCaptionBM guaranteeRecordCaptionBM)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbGuaranteeRecordCaption(");
            strSql.Append("cncDATOP,cnvcFLTID,cniLegNO,cnvcAC,cncFlightDate,cnvcFlightNo,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncStation,cnvcCaption,cndOperationTime,cnvcUserID,cnvcUserName,cnvcUserDepartment)");
            strSql.Append(" values (");
            strSql.Append("@cncDATOP,@cnvcFLTID,@cniLegNO,@cnvcAC,@cncFlightDate,@cnvcFlightNo,@cnvcLONG_REG,@cncDEPSTN,@cncARRSTN,@cncSTD,@cncStation,@cnvcCaption,@cndOperationTime,@cnvcUserID,@cnvcUserName,@cnvcUserDepartment)");
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
					new SqlParameter("@cnvcCaption", SqlDbType.VarChar,200),
					new SqlParameter("@cndOperationTime", SqlDbType.DateTime),
					new SqlParameter("@cnvcUserID", SqlDbType.NVarChar,20),
					new SqlParameter("@cnvcUserName", SqlDbType.NVarChar,20),
					new SqlParameter("@cnvcUserDepartment", SqlDbType.NVarChar,50)};
            parameters[0].Value = guaranteeRecordCaptionBM.cncDATOP;
            parameters[1].Value = guaranteeRecordCaptionBM.cnvcFLTID;
            parameters[2].Value = guaranteeRecordCaptionBM.cniLegNO;
            parameters[3].Value = guaranteeRecordCaptionBM.cnvcAC;
            parameters[4].Value = guaranteeRecordCaptionBM.cncFlightDate;
            parameters[5].Value = guaranteeRecordCaptionBM.cnvcFlightNo;
            parameters[6].Value = guaranteeRecordCaptionBM.cnvcLONG_REG;
            parameters[7].Value = guaranteeRecordCaptionBM.cncDEPSTN;
            parameters[8].Value = guaranteeRecordCaptionBM.cncARRSTN;
            parameters[9].Value = guaranteeRecordCaptionBM.cncSTD;
            parameters[10].Value = guaranteeRecordCaptionBM.cncStation;
            parameters[11].Value = guaranteeRecordCaptionBM.cnvcCaption;
            parameters[12].Value = guaranteeRecordCaptionBM.cndOperationTime;
            parameters[13].Value = guaranteeRecordCaptionBM.cnvcUserID;
            parameters[14].Value = guaranteeRecordCaptionBM.cnvcUserName;
            parameters[15].Value = guaranteeRecordCaptionBM.cnvcUserDepartment;

            //
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSql.ToString(), parameters);

            return retVal;
        }
        #endregion  ����һ������

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        public DataTable GetDataList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbGuaranteeRecordCaption");

            DataTable dataTableResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql.ToString());

            //���ؽ��
            return dataTableResult;
        }

        #endregion ��ȡ������Ϣ

        #region ��ȡ������Ϣ(���ݺ��ࡢ��վ)
        /// <summary>
        /// ��ȡ������Ϣ(���ݺ��ࡢ��վ)
        /// </summary>
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cniGuaranteeRecordCaptionID,cncDATOP,cnvcFLTID,cniLegNO,cnvcAC,cncFlightDate,cnvcFlightNo,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncStation,cnvcCaption,cndOperationTime,cnvcUserID,cnvcUserName,cnvcUserDepartment from tbGuaranteeRecordCaption ");
            strSql.Append(" where cncStation=@cncStation and cncDATOP=@cncDATOP and cnvcFLTID=@cnvcFLTID and cniLegNO=@cniLegNO and cnvcAC=@cnvcAC ");
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

            //���ؽ��
            return dataTableResult;
        }

        #endregion ��ȡ������Ϣ(���ݺ��ࡢ��վ)

    }
}
