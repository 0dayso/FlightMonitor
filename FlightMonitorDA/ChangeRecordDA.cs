using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ������¼���ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ChangeRecordDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ChangeRecordDA()
        {
        }

        #region ����һ�����������¼
        /// <summary>
        /// ���뺽�������¼SQL���
        /// </summary>
        private const string INSERT_ChangeRecord = "INSERT INTO tbFlightChangeRecord " +
            "(cnvcUserID,cnvcOldFLTID,cncOldDATOP,cniOldLegNo,cnvcOldAC,cnvcNewFLTID,cncNewDATOP,cniNewLegNo,cnvcNewAC," +
            "cncOldDepSTN,cncOldArrSTN,cncNewDepSTN,cncNewArrSTN,cncSTD,cncETD,cncSTA,cncETA,cnvcChangeReasonCode," +
            "cnvcChangeOldContent,cnvcChangeNewContent,cncFOCOperatingTime,cncLocalOperatingTime,cncActionTag,cniRefresh) " +
            "VALUES(@PARM_cnvcUserID,@PARM_cnvcOldFLTID,@PARM_cncOldDATOP,@PARM_cniOldLegNo,@PARM_cnvcOldAC,@PARM_cnvcNewFLTID,@PARM_cncNewDATOP,@PARM_cniNewLegNo,@PARM_cnvcNewAC," +
            "@PARM_cncOldDepSTN,@PARM_cncOldArrSTN,@PARM_cncNewDepSTN,@PARM_cncNewArrSTN,@PARM_cncSTD,@PARM_cncETD,@PARM_cncSTA,@PARM_cncETA,@PARM_cnvcChangeReasonCode," +
            "@PARM_cnvcChangeOldContent,@PARM_cnvcChangeNewContent,@PARM_cncFOCOperatingTime,@PARM_cncLocalOperatingTime,@PARM_cncActionTag,@PARM_cniRefresh)";

        /// <summary>
        /// ��ϲ��뺽�������¼����
        /// </summary>
        /// <param name="changeRecordBM">���������¼ʵ�����</param>
        /// <returns>��ϲ���</returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_ChangeRecord);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20),
                    new SqlParameter("@PARM_cnvcOldFLTID", SqlDbType.NVarChar,8),
                    new SqlParameter("@PARM_cncOldDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cniOldLegNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcOldAC", SqlDbType.VarChar, 9),
                    new SqlParameter("@PARM_cnvcNewFLTID", SqlDbType.NVarChar, 8),
                    new SqlParameter("@PARM_cncNewDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cniNewLegNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcNewAC", SqlDbType.VarChar, 9),
                    new SqlParameter("@PARM_cncOldDepSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncOldArrSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncNewDepSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncNewArrSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncSTD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncETA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cnvcChangeReasonCode", SqlDbType.VarChar, 100),
                    new SqlParameter("@PARM_cnvcChangeOldContent", SqlDbType.VarChar, 2048),
                    new SqlParameter("@PARM_cnvcChangeNewContent", SqlDbType.VarChar, 2048),
                    new SqlParameter("@PARM_cncFOCOperatingTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncLocalOperatingTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncActionTag", SqlDbType.Char, 1),
                    new SqlParameter("@PARM_cniRefresh", SqlDbType.Char, 1)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_ChangeRecord, parms);
            }
            parms[0].Value = changeRecordBM.UserID;
            parms[1].Value = changeRecordBM.OldFLTID;
            parms[2].Value = changeRecordBM.OldDATOP;
            parms[3].Value = changeRecordBM.OldLegNo;
            parms[4].Value = changeRecordBM.OldAC;
            parms[5].Value = changeRecordBM.NewFLTID;
            parms[6].Value = changeRecordBM.NewDATOP;
            parms[7].Value = changeRecordBM.NewLegNo;
            parms[8].Value = changeRecordBM.NewAC;
            parms[9].Value = changeRecordBM.OldDepSTN;
            parms[10].Value = changeRecordBM.OldArrSTN;
            parms[11].Value = changeRecordBM.NewDepSTN;
            parms[12].Value = changeRecordBM.NewArrSTN;
            parms[13].Value = changeRecordBM.STD;
            parms[14].Value = changeRecordBM.ETD;
            parms[15].Value = changeRecordBM.STA;
            parms[16].Value = changeRecordBM.ETA;
            parms[17].Value = changeRecordBM.ChangeReasonCode;
            parms[18].Value = changeRecordBM.ChangeOldContent;
            parms[19].Value = changeRecordBM.ChangeNewContent;
            parms[20].Value = changeRecordBM.FOCOperatingTime;
            parms[21].Value = changeRecordBM.LocalOperatingTime;
            parms[22].Value = changeRecordBM.ActionTag;
            parms[23].Value = changeRecordBM.Refresh;

            return parms;
        }

        /// <summary>
        /// ����һ�����������¼
        /// </summary>
        /// <param name="changeRecordBM">���������¼ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int Insert(FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            SqlParameter[] parms = InsertParameters(changeRecordBM);

            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, INSERT_ChangeRecord, parms);

            return retVal;
        }
        #endregion

        #region ��ȡĳϯλ���һ���������
        /// <summary>
        /// ��ȡ���һ��������ݵ�SQL���
        /// </summary>
        private const string SELECT_LastWatchChangeRecords = "SELECT * FROM vw_FlightChangeRecord WHERE " +
            "cniRecordNo > @PARM_cniRecordNo AND " +
            "(cncETD >= @PARM_StartDateTime AND cncETD < @PARM_EndDateTime OR " +
            "cncETA >= @PARM_StartDateTime AND cncETA < @PARM_EndDateTime) AND " +
            "(cnvcOldLONG_REG IN (SELECT cnvcLONG_REG FROM tbPositionInfo WHERE cniPositionID = @PARM_cniPositionID) OR " +
            "cnvcNewLONG_REG IN (SELECT cnvcLONG_REG FROM tbPositionInfo WHERE cniPositionID = @PARM_cniPositionID))   ORDER BY cniRecordNo";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="iLastRecordNo"></param>
        /// <returns></returns>
        private SqlParameter[] SelectLastWatchChangeRecordsParameters(int iLastRecordNo,DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_LastWatchChangeRecords);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniRecordNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_StartDateTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndDateTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cniPositionID", SqlDbType.Int, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_LastWatchChangeRecords, parms);
            }

            parms[0].Value = iLastRecordNo;
            parms[1].Value = dateTimeBM.StartDateTime;
            parms[2].Value = dateTimeBM.EndDateTime;
            parms[3].Value = positionNameBM.PositionID;
            return parms;
        }

        /// <summary>
        /// ϯλ��ȡ���һ���������
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <returns></returns>
        public DataTable GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            SqlParameter[] parms = SelectLastWatchChangeRecordsParameters(iLastRecordNo, dateTimeBM,  positionNameBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_LastWatchChangeRecords, parms);
        }
        #endregion

        #region  ��ȡ��������
        /// <summary>
        /// ��ȡ�������¼���SQL���
        /// </summary>
        private const string SELECT_MaxRecordNo = "SELECT MAX(cniRecordNo) AS cniRecordNo FROM tbFlightChangeRecord";

        /// <summary>
        /// ��ȡ�������¼��
        /// </summary>
        /// <returns></returns>
        public object GetMaxRecordNo()
        {
            return SqlHelper.ExecuteScalar(this.SqlConn, CommandType.Text, SELECT_MaxRecordNo);
        }
        #endregion

        #region ��ȡĳ��վ���һ���������
        /// <summary>
        /// ���ౣ�ϻ�ȡ���һ��������ݵ�SQL���
        /// </summary>
        private const string SELECT_LastGuaranteeChangeRecords = "SELECT * FROM vw_FlightChangeRecord WHERE " +
            "cniRecordNo > @PARM_cniRecordNo AND " +
            "(cncOldDepSTN = @PARM_StationThreeCode OR " +
            "cncOldArrSTN = @PARM_StationThreeCode OR " +
            "cncNewDepSTN = @PARM_StationThreeCode OR " +
            "cncNewArrSTN = @PARM_StationThreeCode) AND " +
            "(cncETD >= @PARM_StartDateTime AND cncETD < @PARM_EndDateTime OR " +
            "cncETA >= @PARM_StartDateTime AND cncETA < @PARM_EndDateTime)  ORDER BY cniRecordNo";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] SelectLastGuaranteeChangeRecordsParameters(int iLastRecordNo,DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_LastGuaranteeChangeRecords);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniRecordNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_StationThreeCode", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_StartDateTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndDateTime", SqlDbType.Char, 19)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_LastGuaranteeChangeRecords, parms);
            }

            parms[0].Value = iLastRecordNo;
            parms[1].Value = stationBM.ThreeCode;
            parms[2].Value = dateTimeBM.StartDateTime;
            parms[3].Value = dateTimeBM.EndDateTime;

            return parms;
        }

        /// <summary>
        /// ��վ��ȡ���һ���������
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <returns></returns>
        public DataTable GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = SelectLastGuaranteeChangeRecordsParameters(iLastRecordNo,dateTimeBM, stationBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_LastGuaranteeChangeRecords, parms);
        }
        #endregion

        #region ��ȡ���100�������¼
        private const string SELECT_ChangeRecords = "SELECT TOP 100 * FROM vw_FlightChangeRecord WHERE " +
            "cniRecordNo > @PARM_cniRecordNo AND (cncETD >= @PARM_StartDateTime AND cncETD < @PARM_EndDateTime OR " +
            "cncETA >= @PARM_StartDateTime AND cncETA < @PARM_EndDateTime) AND " +
            "(cncOldDepSTN = @PARM_StationThreeCode OR " +
            "cncOldArrSTN = @PARM_StationThreeCode OR " +
            "cncNewDepSTN = @PARM_StationThreeCode OR " +
            "cncNewArrSTN = @PARM_StationThreeCode) ORDER BY cniRecordNo desc";

        private SqlParameter[] SelectChangeRecordsParameters(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_ChangeRecords);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniRecordNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_StartDateTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndDateTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_StationThreeCode", SqlDbType.Char, 3)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_ChangeRecords, parms);
            }

            parms[0].Value = iLastRecordNo;
            parms[1].Value = dateTimeBM.StartDateTime;
            parms[2].Value = dateTimeBM.EndDateTime;
            parms[3].Value = stationBM.ThreeCode;

            return parms;
        }

        /// <summary>
        /// ��ȡ��վ����100�������¼
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public DataTable GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
        {
            SqlParameter[] parms = SelectChangeRecordsParameters(iLastRecordNo, dateTimeBM, stationBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_ChangeRecords, parms);
        }

        #endregion

        #region ���ݺ������ڡ�����źͱ�����Ͳ�ѯ�����¼
        /// <summary>
        /// ���ݺ������ڡ�����źͱ�����Ͳ�ѯ�����¼
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strFlightNo"></param>
        /// <param name="strChangeReason"></param>
        /// <returns></returns>
        public DataTable GetChangeRecordsByFlightNo(DateTimeBM dateTimeBM, string strFlightNo, string strChangeReason)
        {
            string strSearch = "SELECT * FROM vw_FlightChangeRecord WHERE " +
            "(cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "' OR " +
            "cncETA >= '" + dateTimeBM.StartDateTime + "' AND cncETA < '" + dateTimeBM.EndDateTime + "')";

            if (strFlightNo != "")
            {
                strSearch += " AND cnvcOldFLTID like '%" + strFlightNo.Trim() + "%'";
            }

            if (strChangeReason != "")
            {
                strSearch += " AND cnvcChangeReasonName = '" + strChangeReason.Trim() + "'";
            }

            strSearch += "   ORDER BY cniRecordNo desc";

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSearch);
        }
        #endregion


        #region added by LinYong

        #region ��ȡ���һ��������� ��vw_FlightChangeRecord�� --added in 2009.10.28
        /// <summary>
        /// ��ȡ���һ��������ݵ�SQL��� ��vw_FlightChangeRecord��
        /// </summary>
        private const string SELECT_Lastvw_FlightChangeRecord = "SELECT * FROM vw_FlightChangeRecord WHERE " +
            "cncLocalOperatingTime >= @PARM_StartDateTime " +
            "ORDER BY cniRecordNo";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="dateTimeBM">ʱ��������� StartDateTime Ϊ��ȡ��ʱ���</param>
        /// <returns></returns>
        private SqlParameter[] SelectLastvw_FlightChangeRecordParameters(DateTimeBM dateTimeBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_Lastvw_FlightChangeRecord);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartDateTime", SqlDbType.Char, 19),
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_Lastvw_FlightChangeRecord, parms);
            }

            parms[0].Value = dateTimeBM.StartDateTime;

            return parms;
        }

        /// <summary>
        /// ��ȡ���һ��������� ��vw_FlightChangeRecord��
        /// </summary>
        /// <param name="dateTimeBM">ʱ��������� StartDateTime Ϊ��ȡ��ʱ���</param>
        /// <returns></returns>
        public DataTable GetLastvw_FlightChangeRecord(DateTimeBM dateTimeBM)
        {
            SqlParameter[] parms = SelectLastvw_FlightChangeRecordParameters(dateTimeBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_Lastvw_FlightChangeRecord, parms);
        }
        #endregion

        #region ��ȡ���һ����¼��ָ���û��� -- added by LinYong in 20150420
        /// <summary>
        /// ��ȡ���һ����¼��ָ���û�����SQL���
        /// </summary>
        private const string SELECT_GetLastRecord = "SELECT top(1) * FROM tbFlightChangeRecord WHERE " +
            "(cnvcUserID = @PARM_cnvcUserID)  " +
            "order by cncFOCOperatingTime desc";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="UserID">�û��ʺ�</param>
        /// <returns></returns>
        private SqlParameter[] GetLastRecordParameters(string UserID)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_GetLastRecord);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20),

                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_GetLastRecord, parms);
            }

            parms[0].Value = UserID;

            return parms;
        }

        /// <summary>
        /// ��ȡ���һ����¼��ָ���û���
        /// </summary>
        /// <param name="UserID">�û��ʺ�</param>
        /// <returns></returns>
        public DataTable GetLastRecord(string UserID)
        {
            SqlParameter[] parms = GetLastRecordParameters(UserID);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_GetLastRecord, parms);
        }
        #endregion ��ȡ���һ����¼��ָ���û��� -- added by LinYong in 20150420

        #endregion
    }
}
