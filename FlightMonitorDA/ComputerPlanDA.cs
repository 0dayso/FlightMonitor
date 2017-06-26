using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��������мƻ����ݷ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-07-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ComputerPlanDA:SqlDatabase
    {
        /// <summary>
        /// ��ѯ��������мƻ�SQL���
        /// </summary>
        private const string SELECT_FlightPlan = "select cndtStdDate,cnvcFlightNo,cnvcDepstn,cnvcArrstn,cntContent,cntReport from tbFlightPlan where cndtStdDate = @PARM_cndtStdDate and cnvcFlightNo = @PARM_cnvcFlightNo and cnvcDepstn = @PARM_cnvcDepstn and cnvcArrstn = @PARM_cnvcArrstn";

        private SqlParameter[] GetComputerPlanParameters(string strDate, string strFlightNo, string strDepstn, string strArrstn)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightPlan);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cndtStdDate", SqlDbType.DateTime, 0),
                    new SqlParameter("@PARM_cnvcFlightNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcDepstn", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcArrstn", SqlDbType.VarChar, 50)
                };
            }

            parms[0].Value = strDate;
            parms[1].Value = strFlightNo;
            parms[2].Value = strDepstn;
            parms[3].Value = strArrstn;

            return parms;
        }

        /// <summary>
        /// ��ȡ��������мƻ�
        /// </summary>
        /// <param name="strDate">��������</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDepstn">ʼ��վ������</param>
        /// <param name="strArrstn">����վ������</param>
        /// <returns></returns>
        public DataTable GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn)
        {
            SqlParameter[] parms = GetComputerPlanParameters(strDate, strFlightNo, strDepstn, strArrstn);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightPlan, parms);
        }

        /// <summary>
        /// ��ѯһ����мƻ���SQL���
        /// </summary>
        private const string Select_CFPByFlightDate = "select * from tbFlightPlan where cndtStdDate = @PARM_cndtStdDate";

        private SqlParameter[] GetCFPByFlightDateParameters(string strDate)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, Select_CFPByFlightDate);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cndtStdDate", SqlDbType.DateTime, 0)
                };
            }

            parms[0].Value = strDate;          

            return parms;
        }

        /// <summary>
        /// �������ڻ�ȡ���м�������мƻ�
        /// </summary>
        /// <param name="strDate">��������</param>
        /// <returns></returns>
        public DataTable GetCFPByFlightDate(string strDate)
        {
            SqlParameter[] parms = GetCFPByFlightDateParameters(strDate);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, Select_CFPByFlightDate, parms);
        }


        #region modified by LinYong in 2013.08.02
        /// <summary>
        /// ��ѯ��������мƻ�SQL���
        /// </summary>
        private const string SELECT_FlightPlan_1 = "select cndtStdDate,cnvcFlightNo,cnvcDepstn,cnvcArrstn,cntContent,cntReport from tbFlightPlan where cndtStdDate = @PARM_cndtStdDate and cnvcFlightNo = @PARM_cnvcFlightNo and cnvcDepstn = @PARM_cnvcDepstn and cnvcArrstn = @PARM_cnvcArrstn and cndtdatop = @PARM_cndtdatop";

        private SqlParameter[] GetComputerPlanParameters(string strDate, string strFlightNo, string strDepstn, string strArrstn, string strDATOP)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightPlan_1);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cndtStdDate", SqlDbType.DateTime, 0),
                    new SqlParameter("@PARM_cnvcFlightNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcDepstn", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcArrstn", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cndtdatop", SqlDbType.DateTime, 0)
               };
            }

            parms[0].Value = strDate;
            parms[1].Value = strFlightNo;
            parms[2].Value = strDepstn;
            parms[3].Value = strArrstn;
            parms[4].Value = strDATOP;

            return parms;
        }

        /// <summary>
        /// ��ȡ��������мƻ�
        /// </summary>
        /// <param name="strDate">��������</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDepstn">ʼ��վ������</param>
        /// <param name="strArrstn">����վ������</param>
        /// <param name="strDATOP">�������ڣ�UTC��</param> 
        /// <returns></returns>
        public DataTable GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn, string strDATOP)
        {
            SqlParameter[] parms = GetComputerPlanParameters(strDate, strFlightNo, strDepstn, strArrstn, strDATOP);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightPlan_1, parms);
        }
        #endregion modified by LinYong in 2013.08.02
    }
}


