using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.DataHelper;
using System.Data.SqlClient;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class AirportInfoDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AirportInfoDA()
        {
        }

        #region
        private const string SELECT_AirPortInfor = "SELECT cncAirportThreeCode,cncCityThreeCode,cncAirportFourCode," +
            "SUBSTRING(cncAirportCNAME, 1, CHARINDEX('/', cncAirportCNAME + '/') - 1) AS cncAirportCNAME," +
            "cncAirportCSIMP,cniAirportAreaId,cnvcAirportArea,cncIsSelf,cnvcRemarks,cniAirportPaxType,cniAirportTaxiFuelType," +
            "RTRIM(cncAirportThreeCode + '/' + SUBSTRING(cncAirportCNAME, 1, CHARINDEX('/', cncAirportCNAME + '/') - 1)) AS cncLongAirportCNAME FROM tbAirportInfo " +
            " WHERE cncAirportThreeCode<> '///' ORDER BY cncAirportThreeCode";

        /// <summary>
        /// ��ȡ���л�����Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetAirportInfors()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AirPortInfor);
        }
        #endregion 

        #region �����������ȡ������Ϣ
        private const string SELECT_AirportInforByThreeCode = "SELECT * FROM tbAirportInfo WHERE cncAirportThreeCode = @PARM_cncAirportThreeCode";

        private SqlParameter[] GetAirportInforByThreeCodeParameters(string strThreeCode)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_AirportInforByThreeCode);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncAirportThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_AirportInforByThreeCode, parms);
            }

            parms[0].Value = strThreeCode;

            return parms;
        }

        /// <summary>
        /// �����������ȡ������Ϣ
        /// </summary>
        /// <param name="strThreeCode">����������</param>
        /// <returns></returns>
        public DataTable GetAirportInforByThreeCode(string strThreeCode)
        {
            SqlParameter[] parms = GetAirportInforByThreeCodeParameters(strThreeCode);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AirportInforByThreeCode, parms);
        }
        #endregion

        #region ���ݻ���������ͻ��ͻ�ȡ������
        private const string SELECT_TaxiOil = "SELECT * FROM tbAirportInfo,tbTaxiFuel WHERE " +
            "tbAirportInfo.cniAirportTaxiFuelType = tbTaxiFuel.cniTaxiFuelType AND " +
            "cncACTYPE = @PARM_cncACTYPE AND " +
            "cncAirportThreeCode = @PARM_cncAirportThreeCode";

        private SqlParameter[] GetTaxiOilParameters(string strACTYPE, string strThreeCode)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_TaxiOil);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncACTYPE", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncAirportThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_TaxiOil, parms);
            }

            parms[0].Value = strACTYPE;
            parms[1].Value = strThreeCode;

            return parms;
        }


        /// <summary>
        /// ���ݻ��ͺ���ɻ�����ȡ������
        /// </summary>
        /// <param name="strACTYPE">FOC����</param>
        /// <param name="strThreeCode">��ɻ���������</param>
        /// <returns></returns>
        public DataTable GetTaxiOil(string strACTYPE, string strThreeCode)
        {
            SqlParameter[] parms = GetTaxiOilParameters(strACTYPE, strThreeCode);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_TaxiOil, parms);
        }
        #endregion
    }
}
