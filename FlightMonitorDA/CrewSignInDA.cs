using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ����ǩ�����ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-07-10
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CrewSignInDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CrewSignInDA()
        {
        }

        #region ��ѯһ���������ǩ����Ϣ
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
        /// ��ѯһ������Ļ���ǩ����Ϣ
        /// </summary>
        /// <param name="strQueryTime">��ѯʱ��</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns>ǩ����Ϣ��</returns>
        public DataTable GetCrewSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            SqlParameter[] parms = GetCrewSignInParameters(strQueryTime, strFlightNo, strDEPSTN);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.StoredProcedure, SELECT_CrewSignIn, parms);
        }
        #endregion

        #region ����������ѯ����ǩ����¼
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
        /// ��ȡĳλ����Ա��ǩ��ʱ��
        /// </summary>
        /// <param name="strCrewName">����Ա����</param>
        /// <param name="strSignInFlag">ǩ����ʶ</param>
        /// <returns></returns>
        public DataTable GetCrewSignTime(string strCrewName, string strSignInFlag)
        {
            SqlParameter[] parms = GetCrewSignTimeParameters(strCrewName, strSignInFlag);

            return SqlHelper.ExecuteDataTable(this.DBConnString, CommandType.StoredProcedure, SELECT_CrewSignTime, parms);
        }
        #endregion

        #region ��ѯһ������ĳ���ǩ����Ϣ
        private const string SELECT_StewardSignIn = "ChengWuYuanQianDao1";

        /// <summary>
        /// ��ѯһ������ĳ���ǩ����Ϣ
        /// </summary>
        /// <param name="strQueryTime">��ѯʱ��</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns>ǩ����Ϣ��</returns>
        public DataTable GetStewardSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            SqlParameter[] parms = GetCrewSignInParameters(strQueryTime, strFlightNo, strDEPSTN);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.StoredProcedure, SELECT_StewardSignIn, parms);
        }
        #endregion

        #region ��ѯĳλ����Ա��ǩ��ʱ��
        private const string SELECT_StewardSignTime = "qdxt_cwy_ChaXunJieGuo";

        /// <summary>
        /// ��ȡĳλ����Ա��ǩ��ʱ��
        /// </summary>
        /// <param name="strStewardName">����Ա����</param>
        /// <param name="strSignInFlag">ǩ����ʶ</param>
        /// <returns></returns>
        public DataTable GetStewardSignTime(string strStewardName, string strSignInFlag)
        {
            SqlParameter[] parms = GetCrewSignTimeParameters(strStewardName, strSignInFlag);

            return SqlHelper.ExecuteDataTable(this.DBConnString, CommandType.StoredProcedure, SELECT_StewardSignTime, parms);
        }
        #endregion
    }
}
