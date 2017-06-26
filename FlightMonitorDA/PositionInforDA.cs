using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ϯλ��Ϣ���ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionInforDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionInforDA()
        {
        }

        #region ��ȡĳϯλ�����зɻ���
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
        /// ����ϯλ��Ż�ȡ���ڸ�ϯλ�����зɻ�����Ϣ
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public DataTable GetInforByPositionId(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SelectInforByPositionIdParameters(positionNameBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_InforByPositionId, parms);
        }
        #endregion
       

        #region ɾ��ĳһϯλ�����е���Ϣ
        private const string DELETE_InforByPositionId = "DELETE FROM tbPositionInfo WHERE " +
            "tbPositionInfo.cniPositionID = @PARM_cniPositionID";

        /// <summary>
        /// ɾ��ĳϯλ�����зɻ�
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

        #region ����һ����Ϣ
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
        /// ���һ��ϯλ��Ϣ
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
