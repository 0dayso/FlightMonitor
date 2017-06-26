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
    /// ACARS������ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-15
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSLegsDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ACARSLegsDA()
        {
        }

        /// <summary>
        /// ���ı��ļ���ȡ���ද̬��Ϣ
        /// </summary>
        /// <param name="strFullPath">�ļ�·��</param>
        /// <returns>ACARS���ද̬�ı���Ϣ</returns>
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

        #region ���º��ද̬��ACARS������Ϣ
        private const string UPDATE_ACARSOnInfor = "UPDATE tbGuaranteeInfor SET " +
            " cncACARSTDWN = @PARM_cncACARSTDWN" +            
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
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
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSOnInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateOnInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSOnInfor, parms);
            return retVal;
        }
        #endregion

        #region ���º��ද̬��ACARS���յ���Ϣ
        private const string UPDATE_ACARSInInfor = "UPDATE tbGuaranteeInfor SET " +
            " cncACARSATA = @PARM_cncACARSATA" +
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
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
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSInInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateInInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSInInfor, parms);
            return retVal;
        }
        #endregion

        #region ���º��ද̬��ACARS�˳���Ϣ
        private const string UPDATE_ACARSOutInfor = "UPDATE tbGuaranteeInfor SET " +
            " cncACARSOUT = @PARM_cncPushTime" +            
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
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
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSOutInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateOutInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSOutInfor, parms);
            return retVal;
        }
        #endregion

        #region ���º��ද̬��ACARS�����Ϣ
        private const string UPDATE_ACARSOffInfor = "UPDATE tbGuaranteeInfor SET " +            
            " cncACARSTOFF = @PARM_cncACARSTOFF " +
            " WHERE cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLegNO = @PARM_cniLegNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
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
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSOffInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpdateOffInforParameters(acarsLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_ACARSOffInfor, parms);
            return retVal;
        }
        #endregion
    }
}
