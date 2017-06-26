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
    /// ϯλ���ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionNameDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionNameDA()
        {
        }

        #region ��ȡ����ϯλ����
        private const string SELECT_AllPositionName = "SELECT * FROM tbPositionName";

        /// <summary>
        /// ��ȡ����ϯλ����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPositionName()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllPositionName);
        }
        #endregion

        #region ����µ�ϯλ
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
        /// ����һ��ϯλ
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

        #region ɾ��һ��ϯλ
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
        /// ɾ��һ��ϯλ����
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

        #region ����ϯλ��Ż�ȡһ��ϯλ
        private const string SELECT_PositionByPositionID = "SELECT * FROM tbPositionName WHERE cniPositionID = @PARM_cniPositionID";

        /// <summary>
        /// ����ϯλ��Ż�ȡϯλ��Ϣ
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
