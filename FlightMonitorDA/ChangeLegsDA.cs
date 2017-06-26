using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;
using System.IO;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ���������ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-30
    /// �� �� �ˣ���  ��
    /// �޸����ڣ�2008-04-01
    /// ��    ����
    public class ChangeLegsDA:SqlDatabase
    {
        private const string strDataField = "cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cncFlightDate,cncCKIFlightDate,cnvcFlightNo,cnvcCKIFlightNo,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncSTA,cncSTATUS,cncETD,cncETA,cncATD,cncTOFF,cncTDWN,cncATA,cnvcTRI_FLTID,cnvcDIV_RCODE,cnvcDIV_FLAG,cnvcPAX,cnvcBOOK,cnvcDELAY1,cniDUR1,cnvcDELAY2,cniDUR2,cnvcDELAY3,cniDUR3,cnvcDELAY4,cniDUR4,cnvcGATE,cnvcSTC,cnvcVERSION,cncORIG_ACTYP,cncACTYP,cnvcACOWN,cnvcSEQ,cncInsertTime,cniDeleteTag";

        #region �����������
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>��ϲ���</returns>
        private SqlParameter[] GetPriKeyParameters(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter [] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, "FlightPriKey");

            if(parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, "FlightPriKey", parms);
            }
            parms[0].Value = changeLegsBM.DATOP;
            parms[1].Value = changeLegsBM.FLTID;
            parms[2].Value = changeLegsBM.LEGNO;
            parms[3].Value = changeLegsBM.AC;
            return parms;
        }
        #endregion

        #region ���������Ϊ������ѯһ������
        /// <summary>
        /// ���������Ϊ������ѯ����SQL���
        /// </summary>
        private const string SELECT_FlightByComKey = "SELECT " + strDataField + " FROM tbLegs WHERE " +
            "cncDATOP = @PARM_cncDATOP AND " +
            "cnvcFLTID = @PARM_cnvcFLTID AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND " +
            "cncSTD = @PARM_cncSTD order by cncInsertTime desc";

        /// <summary>
        /// ���DATOP��STD��DEPSTN��STD����
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>��ϲ���</returns>
        private SqlParameter[] GetComKeyParameters(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightByComKey);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncSTD", SqlDbType.Char, 19)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_FlightByComKey, parms);
            }
            parms[0].Value = changeLegsBM.DATOP;
            parms[1].Value = changeLegsBM.FLTID;
            parms[2].Value = changeLegsBM.DEPSTN;
            parms[3].Value = changeLegsBM.STD;

            return parms;
        }

        /// <summary>
        /// ���������Ϊ������ѯһ������
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>�������ݵ����ݱ�</returns>
        public DataTable GetFlightByCombineKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = GetComKeyParameters(changeLegsBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByComKey, parms);
        }
        #endregion

        #region  ����������ѯһ����¼
        /// <summary>
        /// ������Ϊ������ѯ����SQL���
        /// </summary>
        private const string SELECT_FlightByPriKey = "SELECT " + strDataField + " FROM tbLegs WHERE " +
            " cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLEGNO = @PARM_cniLEGNO AND " +
            " cnvcAC = @PARM_cnvcAC";
        /// <summary>
        /// ������Ϊ������ѯһ����¼
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>�������ݵ����ݱ�</returns>
        public DataTable GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = GetPriKeyParameters(changeLegsBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByPriKey, parms);
        }
        #endregion

        #region ����һ�����ද̬
        /// <summary>
        /// ���뺽�ද̬�洢����
        /// </summary>
        private const string INSERT_Flight = "sp_dbFlightMonitor_Ins";

        /// <summary>
        /// ��ϲ��뺽�����
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>��ϲ���</returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Flight);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9),
                    new SqlParameter("@PARM_cncFlightDate", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cncCKIFlightDate", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFlightNo", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcCKIFlightNo", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncSTD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTATUS", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncETA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncATD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncTOFF", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncTDWN", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncATA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cnvcTRI_FLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcDIV_RCODE", SqlDbType.VarChar, 2),
                    new SqlParameter("@PARM_cnvcDIV_FLAG", SqlDbType.VarChar, 1),
                    new SqlParameter("@PARM_cnvcPAX", SqlDbType.VarChar, 12),
                    new SqlParameter("@PARM_cnvcBOOK", SqlDbType.VarChar, 12),
                    new SqlParameter("@PARM_cnvcDELAY1", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR1", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY2", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR2", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY3", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR3", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY4", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR4", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcGATE", SqlDbType.VarChar, 5),
                    new SqlParameter("@PARM_cnvcSTC", SqlDbType.VarChar, 1),
                    new SqlParameter("@PARM_cnvcVERSION", SqlDbType.VarChar, 7),
                    new SqlParameter("@PARM_cncORIG_ACTYP", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncACTYP", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcACOWN", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cnvcSEQ", SqlDbType.VarChar, 50)    
                    
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Flight, parms);
            }

            parms[0].Value = changeLegsBM.DATOP;
            parms[1].Value = changeLegsBM.FLTID;
            parms[2].Value = changeLegsBM.LEGNO;
            parms[3].Value = changeLegsBM.AC;
            parms[4].Value = changeLegsBM.FlightDate;
            parms[5].Value = changeLegsBM.CKIFlightDate;
            parms[6].Value = changeLegsBM.FlightNo;
            parms[7].Value = changeLegsBM.CKIFlightNo;
            parms[8].Value = changeLegsBM.LONG_REG;
            parms[9].Value = changeLegsBM.DEPSTN;
            parms[10].Value = changeLegsBM.ARRSTN;
            parms[11].Value = changeLegsBM.STD;
            parms[12].Value = changeLegsBM.STA;
            parms[13].Value = changeLegsBM.STATUS;
            parms[14].Value = changeLegsBM.ETD;
            parms[15].Value = changeLegsBM.ETA;
            parms[16].Value = changeLegsBM.ATD;
            parms[17].Value = changeLegsBM.TOFF;
            parms[18].Value = changeLegsBM.TDWN;
            parms[19].Value = changeLegsBM.ATA;
            parms[20].Value = changeLegsBM.TRI_FLTID;
            parms[21].Value = changeLegsBM.DIV_RCODE;
            parms[22].Value = changeLegsBM.DIV_FLAG;
            parms[23].Value = changeLegsBM.PAX;
            parms[24].Value = changeLegsBM.BOOK;
            parms[25].Value = changeLegsBM.DELAY1;
            parms[26].Value = changeLegsBM.DUR1;
            parms[27].Value = changeLegsBM.DELAY2;
            parms[28].Value = changeLegsBM.DUR2;
            parms[29].Value = changeLegsBM.DELAY3;
            parms[30].Value = changeLegsBM.DUR3;
            parms[31].Value = changeLegsBM.DELAY4;
            parms[32].Value = changeLegsBM.DUR4;
            parms[33].Value = changeLegsBM.GATE;
            parms[34].Value = changeLegsBM.STC;
            parms[35].Value = changeLegsBM.VERSION;
            parms[36].Value = changeLegsBM.ORIG_ACTYP;
            parms[37].Value = changeLegsBM.ACTYP;
            parms[38].Value = changeLegsBM.ACOWN;
            parms[39].Value = changeLegsBM.SEQ;

            return parms;
        }

        /// <summary>
        /// ����һ�����ද̬
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>1���ɹ�0��ʧ��</returns>
        public int Insert(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = InsertParameters(changeLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.StoredProcedure, INSERT_Flight, parms);
            return retVal;
        }
        #endregion

        #region  ������ɹ���ACARS�������
        private const string INSERT_ACARSMessage = "INSERT INTO tbACARSMessageList VALUES(" +
            "@PARM_cncDATOP,@PARM_cnvcFLTID,@PARM_cniLEGNO,@PARM_cnvcAC," +
            "@PARM_cncFlightDate,@PARM_cnvcFlightNo,@PARM_cnvcLONG_REG," +
            "@PARM_cncDEPSTN,@PARM_cncARRSTN,@PARM_cncSTD,@PARM_cncETD,@PARM_cncTOFF,@PARM_cncSTA,@PARM_cncETA,@PARM_cncTDWN," +
            "@PARM_cntACARSMessage,@PARM_cncACARSMessageTime,@PARM_cncInsertTime)";

        private SqlParameter[] InsertACARSMessageParameters(FlightMonitorBM.ChangeLegsBM changeLegsBM, string strACARSMessage, string strACARSMessageTime, string strInsertTime)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_ACARSMessage);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9),
                    new SqlParameter("@PARM_cncFlightDate", SqlDbType.Char, 10),                   
                    new SqlParameter("@PARM_cnvcFlightNo", SqlDbType.VarChar, 8),                    
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncSTD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncTOFF", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTA", SqlDbType.Char, 19), 
                    new SqlParameter("@PARM_cncETA", SqlDbType.Char, 19), 
                    new SqlParameter("@PARM_cncTDWN", SqlDbType.Char, 19), 
                    new SqlParameter("@PARM_cntACARSMessage", SqlDbType.Text, 0),
                    new SqlParameter("@PARM_cncACARSMessageTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncInsertTime", SqlDbType.Char, 19)
                    
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_ACARSMessage, parms);
            }

            parms[0].Value = changeLegsBM.DATOP;
            parms[1].Value = changeLegsBM.FLTID;
            parms[2].Value = changeLegsBM.LEGNO;
            parms[3].Value = changeLegsBM.AC;
            parms[4].Value = changeLegsBM.FlightDate;            
            parms[5].Value = changeLegsBM.FlightNo;           
            parms[6].Value = changeLegsBM.LONG_REG;
            parms[7].Value = changeLegsBM.DEPSTN;
            parms[8].Value = changeLegsBM.ARRSTN;
            parms[9].Value = changeLegsBM.STD;
            parms[10].Value = changeLegsBM.ETD;
            parms[11].Value = changeLegsBM.TOFF;
            parms[12].Value = changeLegsBM.STA;
            parms[13].Value = changeLegsBM.ETA;
            parms[14].Value = changeLegsBM.TDWN;
            parms[15].Value = strACARSMessage;
            parms[16].Value = strACARSMessageTime;
            parms[17].Value = strInsertTime;
            
            return parms;
        }

        public int InsertACARSMessage(FlightMonitorBM.ChangeLegsBM changeLegsBM, string strACARSMessage, string strACARSMessageTime, string strInsertTime)
        {
            SqlParameter[] parms = InsertACARSMessageParameters(changeLegsBM, strACARSMessage, strACARSMessageTime, strInsertTime);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, INSERT_ACARSMessage, parms);
            return retVal;
        }
        #endregion

        #region �߼�ɾ��һ�����ද̬
        private const string DELETE_Flight = "UPDATE tbLegs SET cniDeleteTag = 1 WHERE " +
            " cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLEGNO = @PARM_cniLEGNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// �߼�ɾ��һ�����ද̬
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>1���ɹ�0��ʧ��</returns>
        public int LogicDelete(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = GetPriKeyParameters(changeLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, DELETE_Flight, parms);
            return retVal;
        }
        #endregion

        #region ������DATOP��FLTID��LEGNO��ACΪ��������һ����¼
        private const string UPDATE_AllInfoByPriKey = "UPDATE tblegs SET " +
            "cncFlightDate = @PARM_cncFlightDate," +
            "cncCKIFlightDate = @PARM_cncCKIFlightDate," +
            "cnvcFlightNo = @PARM_cnvcFlightNo," +
            "cnvcCKIFlightNo = @PARM_cnvcCKIFlightNo," +
            "cnvcLONG_REG = @PARM_cnvcLONG_REG," +
            "cncDEPSTN = @PARM_cncDEPSTN," +
            "cncARRSTN = @PARM_cncARRSTN," +
            "cncSTD = @PARM_cncSTD," +
            "cncSTA = @PARM_cncSTA," +
            "cncSTATUS = @PARM_cncSTATUS," +
            "cncETD = @PARM_cncETD," +
            "cncETA = @PARM_cncETA," +
            "cncATD = @PARM_cncATD," +
            "cncTOFF =@PARM_cncTOFF," +
            "cncTDWN = @PARM_cncTDWN," +
            "cncATA = @PARM_cncATA," +
            "cnvcTRI_FLTID = @PARM_cnvcTRI_FLTID," +
            "cnvcDIV_RCODE = @PARM_cnvcDIV_RCODE," +
            "cnvcDIV_FLAG = @PARM_cnvcDIV_FLAG," +
            "cnvcPAX = @PARM_cnvcPAX," +
            "cnvcBOOK = @PARM_cnvcBOOK," +
            "cnvcDELAY1 = @PARM_cnvcDELAY1," +
            "cniDUR1 = @PARM_cniDUR1," +
            "cnvcDELAY2 = @PARM_cnvcDELAY2," +
            "cnvcDELAY3 = @PARM_cnvcDELAY3," +
            "cniDUR3 = @PARM_cniDUR3," +
            "cnvcDELAY4 = @PARM_cnvcDELAY4," +
            "cniDUR4 = @PARM_cniDUR4," +
            "cnvcGATE = @PARM_cnvcGATE," +
            "cnvcSTC = @PARM_cnvcSTC," +
            "cnvcVERSION = @PARM_cnvcVERSION," +
            "cncORIG_ACTYP = @PARM_cncORIG_ACTYP," +
            "cncACTYP = @PARM_cncACTYP," +
            "cnvcACOWN = @PARM_cnvcACOWN," +
            "cnvcSEQ = @PARM_cnvcSEQ," +
            "cniDeleteTag = 0 WHERE "  +
            " cncDATOP = @PARM_cncDATOP AND " +
            " cnvcFLTID = @PARM_cnvcFLTID AND " +
            " cniLEGNO = @PARM_cniLEGNO AND " +
            " cnvcAC = @PARM_cnvcAC";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] UpadateAllInfoByPriKeyParameters(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_AllInfoByPriKey);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {                
                    new SqlParameter("@PARM_cncFlightDate", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cncCKIFlightDate", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFlightNo", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcCKIFlightNo", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncSTD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTATUS", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncETA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncATD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncTOFF", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncTDWN", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncATA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cnvcTRI_FLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcDIV_RCODE", SqlDbType.VarChar, 2),
                    new SqlParameter("@PARM_cnvcDIV_FLAG", SqlDbType.VarChar, 1),
                    new SqlParameter("@PARM_cnvcPAX", SqlDbType.VarChar, 12),
                    new SqlParameter("@PARM_cnvcBOOK", SqlDbType.VarChar, 12),
                    new SqlParameter("@PARM_cnvcDELAY1", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR1", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY2", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR2", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY3", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR3", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY4", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR4", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcGATE", SqlDbType.VarChar, 5),
                    new SqlParameter("@PARM_cnvcSTC", SqlDbType.VarChar, 1),
                    new SqlParameter("@PARM_cnvcVERSION", SqlDbType.VarChar, 7),
                    new SqlParameter("@PARM_cncORIG_ACTYP", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncACTYP", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcACOWN", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cnvcSEQ", SqlDbType.VarChar, 50),
                     new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9)                    
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_AllInfoByPriKey, parms);
            }
            
            parms[0].Value = changeLegsBM.FlightDate;
            parms[1].Value = changeLegsBM.CKIFlightDate;
            parms[2].Value = changeLegsBM.FlightNo;
            parms[3].Value = changeLegsBM.CKIFlightNo;
            parms[4].Value = changeLegsBM.LONG_REG;
            parms[5].Value = changeLegsBM.DEPSTN;
            parms[6].Value = changeLegsBM.ARRSTN;
            parms[7].Value = changeLegsBM.STD;
            parms[8].Value = changeLegsBM.STA;
            parms[9].Value = changeLegsBM.STATUS;
            parms[10].Value = changeLegsBM.ETD;
            parms[11].Value = changeLegsBM.ETA;
            parms[12].Value = changeLegsBM.ATD;
            parms[13].Value = changeLegsBM.TOFF;
            parms[14].Value = changeLegsBM.TDWN;
            parms[15].Value = changeLegsBM.ATA;
            parms[16].Value = changeLegsBM.TRI_FLTID;
            parms[17].Value = changeLegsBM.DIV_RCODE;
            parms[18].Value = changeLegsBM.DIV_FLAG;
            parms[19].Value = changeLegsBM.PAX;
            parms[20].Value = changeLegsBM.BOOK;
            parms[21].Value = changeLegsBM.DELAY1;
            parms[22].Value = changeLegsBM.DUR1;
            parms[23].Value = changeLegsBM.DELAY2;
            parms[24].Value = changeLegsBM.DUR2;
            parms[25].Value = changeLegsBM.DELAY3;
            parms[26].Value = changeLegsBM.DUR3;
            parms[27].Value = changeLegsBM.DELAY4;
            parms[28].Value = changeLegsBM.DUR4;
            parms[29].Value = changeLegsBM.GATE;
            parms[30].Value = changeLegsBM.STC;
            parms[31].Value = changeLegsBM.VERSION;
            parms[32].Value = changeLegsBM.ORIG_ACTYP;
            parms[33].Value = changeLegsBM.ACTYP;
            parms[34].Value = changeLegsBM.ACOWN;
            parms[35].Value = changeLegsBM.SEQ;
            parms[36].Value = changeLegsBM.DATOP;
            parms[37].Value = changeLegsBM.FLTID;
            parms[38].Value = changeLegsBM.LEGNO;
            parms[39].Value = changeLegsBM.AC;

            return parms;
        }

        /// <summary>
        /// ������Ϊ��������������Ϣ
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>1���ɹ���0��ʧ��</returns>
        public int UpdateAllInfoByPriKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = UpadateAllInfoByPriKeyParameters(changeLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_AllInfoByPriKey, parms);
            return retVal;
        }
        #endregion

        #region ���������Ϊ��������������Ϣ
        private const string UPDATE_AllInfoByComKey = "UPDATE tblegs SET " +
            "cniLEGNO = @PARM_cniLEGNO," +
            "cnvcAC = @PARM_cnvcAC," +
            "cncFlightDate = @PARM_cncFlightDate," +
            "cncCKIFlightDate = @PARM_cncCKIFlightDate," +
            "cnvcFlightNo = @PARM_cnvcFlightNo," +
            "cnvcCKIFlightNo = @PARM_cnvcCKIFlightNo," +
            "cnvcLONG_REG = @PARM_cnvcLONG_REG," +
            "cncDEPSTN = @PARM_cncDEPSTN," +
            "cncARRSTN = @PARM_cncARRSTN," +
            "cncSTD = @PARM_cncSTD," +
            "cncSTA = @PARM_cncSTA," +
            "cncSTATUS = @PARM_cncSTATUS," +
            "cncETD = @PARM_cncETD," +
            "cncETA = @PARM_cncETA," +
            "cncATD = @PARM_cncATD," +
            "cncTOFF =@PARM_cncTOFF," +
            "cncTDWN = @PARM_cncTDWN," +
            "cncATA = @PARM_cncATA," +
            "cnvcTRI_FLTID = @PARM_cnvcTRI_FLTID," +
            "cnvcDIV_RCODE = @PARM_cnvcDIV_RCODE," +
            "cnvcDIV_FLAG = @PARM_cnvcDIV_FLAG," +
            "cnvcPAX = @PARM_cnvcPAX," +
            "cnvcBOOK = @PARM_cnvcBOOK," +
            "cnvcDELAY1 = @PARM_cnvcDELAY1," +
            "cniDUR1 = @PARM_cniDUR1," +
            "cnvcDELAY2 = @PARM_cnvcDELAY2," +
            "cnvcDELAY3 = @PARM_cnvcDELAY3," +
            "cniDUR3 = @PARM_cniDUR3," +
            "cnvcDELAY4 = @PARM_cnvcDELAY4," +
            "cniDUR4 = @PARM_cniDUR4," +
            "cnvcGATE = @PARM_cnvcGATE," +
            "cnvcSTC = @PARM_cnvcSTC," +
            "cnvcVERSION = @PARM_cnvcVERSION," +
            "cncORIG_ACTYP = @PARM_cncORIG_ACTYP," +
            "cncACTYP = @PARM_cncACTYP," +
            "cnvcACOWN = @PARM_cnvcACOWN," +
            "cnvcSEQ = @PARM_cnvcSEQ," +
            "cniDeleteTag = 0 WHERE " +
            " cncDATOP = @PARM_cncOriginalDATOP AND " +
            " cnvcFLTID = @PARM_cnvcOriginalFLTID AND " +
            " cniLEGNO = @PARM_cniOriginalLEGNO AND " +
            " cnvcAC = @PARM_cnvcOriginalAC";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] UpadateAllInfoByComKeyParameters(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_AllInfoByComKey);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {               
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9),
                    new SqlParameter("@PARM_cncFlightDate", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cncCKIFlightDate", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFlightNo", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcCKIFlightNo", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncSTD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncSTATUS", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncETD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncETA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncATD", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncTOFF", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncTDWN", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cncATA", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_cnvcTRI_FLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cnvcDIV_RCODE", SqlDbType.VarChar, 2),
                    new SqlParameter("@PARM_cnvcDIV_FLAG", SqlDbType.VarChar, 1),
                    new SqlParameter("@PARM_cnvcPAX", SqlDbType.VarChar, 12),
                    new SqlParameter("@PARM_cnvcBOOK", SqlDbType.VarChar, 12),
                    new SqlParameter("@PARM_cnvcDELAY1", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR1", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY2", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR2", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY3", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR3", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcDELAY4", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cniDUR4", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcGATE", SqlDbType.VarChar, 5),
                    new SqlParameter("@PARM_cnvcSTC", SqlDbType.VarChar, 1),
                    new SqlParameter("@PARM_cnvcVERSION", SqlDbType.VarChar, 7),
                    new SqlParameter("@PARM_cncORIG_ACTYP", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncACTYP", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcACOWN", SqlDbType.VarChar, 3),
                    new SqlParameter("@PARM_cnvcSEQ", SqlDbType.VarChar, 50),
                     new SqlParameter("@PARM_cncOriginalDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcOriginalFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cniOriginalLEGNO", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcOriginalAC", SqlDbType.VarChar, 9)      
                                    
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_AllInfoByComKey, parms);
            }

            parms[0].Value = changeLegsBM.LEGNO;
            parms[1].Value = changeLegsBM.AC;
            parms[2].Value = changeLegsBM.FlightDate;
            parms[3].Value = changeLegsBM.CKIFlightDate;
            parms[4].Value = changeLegsBM.FlightNo;
            parms[5].Value = changeLegsBM.CKIFlightNo;
            parms[6].Value = changeLegsBM.LONG_REG;
            parms[7].Value = changeLegsBM.DEPSTN;
            parms[8].Value = changeLegsBM.ARRSTN;
            parms[9].Value = changeLegsBM.STD;
            parms[10].Value = changeLegsBM.STA;
            parms[11].Value = changeLegsBM.STATUS;
            parms[12].Value = changeLegsBM.ETD;
            parms[13].Value = changeLegsBM.ETA;
            parms[14].Value = changeLegsBM.ATD;
            parms[15].Value = changeLegsBM.TOFF;
            parms[16].Value = changeLegsBM.TDWN;
            parms[17].Value = changeLegsBM.ATA;
            parms[18].Value = changeLegsBM.TRI_FLTID;
            parms[19].Value = changeLegsBM.DIV_RCODE;
            parms[20].Value = changeLegsBM.DIV_FLAG;
            parms[21].Value = changeLegsBM.PAX;
            parms[22].Value = changeLegsBM.BOOK;
            parms[23].Value = changeLegsBM.DELAY1;
            parms[24].Value = changeLegsBM.DUR1;
            parms[25].Value = changeLegsBM.DELAY2;
            parms[26].Value = changeLegsBM.DUR2;
            parms[27].Value = changeLegsBM.DELAY3;
            parms[28].Value = changeLegsBM.DUR3;
            parms[29].Value = changeLegsBM.DELAY4;
            parms[30].Value = changeLegsBM.DUR4;
            parms[31].Value = changeLegsBM.GATE;
            parms[32].Value = changeLegsBM.STC;
            parms[33].Value = changeLegsBM.VERSION;
            parms[34].Value = changeLegsBM.ORIG_ACTYP;
            parms[35].Value = changeLegsBM.ACTYP;
            parms[36].Value = changeLegsBM.ACOWN;
            parms[37].Value = changeLegsBM.SEQ;
            parms[38].Value = originalLegsBM.DATOP;
            parms[39].Value = originalLegsBM.FLTID;
            parms[40].Value = originalLegsBM.LEGNO;
            parms[41].Value = originalLegsBM.AC;      

            return parms;
        }

        /// <summary>
        /// ���������Ϊ��������������Ϣ
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <returns>1���ɹ���0��ʧ��</returns>
        public int UpdateAllInfoByComKey(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            SqlParameter[] parms = UpadateAllInfoByComKeyParameters(changeLegsBM, originalLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_AllInfoByComKey, parms);
            return retVal;
        }
        #endregion

        #region ����ACARS��ɱ��Ļ�ȡ��Ӧ�ĺ��ද̬
        //��������ʱ���������йؼ��ֲ�ѯ
        private const string SELECT_FlightByACARSDEPInfo = "SELECT " + strDataField + " FROM tbLegs WHERE " +
            "cnvcFLTID like '%' + @PARM_cnvcFLTID AND " +
            "cncDEPSTN=@PARM_cncDEPSTN AND " +
            "cncARRSTN=@PARM_cncARRSTN AND " +
            "cnvcLONG_REG=@PARM_cnvcLONG_REG AND " +
            "LEFT(cncETD, 10)=@PARM_FlightDate AND " +
            "cniDeleteTag = 0 and cncSTATUS<>'CNL' order by cncETD";

        private const string SELECT_FlightBySubACARSDEPInfo = "SELECT " + strDataField + " FROM tbLegs WHERE " +
           "cnvcFLTID like '%' + @PARM_cnvcFLTID AND " +
           "cncDEPSTN=@PARM_cncDEPSTN AND " +
           "cnvcLONG_REG=@PARM_cnvcLONG_REG AND " +
           "LEFT(cncETD, 10)=@PARM_FlightDate AND " +
           "cniDeleteTag = 0 and cncSTATUS<>'CNL' order by cncETD";

        //���Ĳ�����ʱ�����ݷɻ��ź�ʱ��ȥƥ��
        private const string SELECT_FlightByACARSDEP = "SELECT " + strDataField + " FROM tbLegs WHERE " +
            "cnvcLONG_REG=@PARM_cnvcLONG_REG AND " +
            "cncETD >= @PARM_StartTOFF AND cncETD <= @PARM_EndTOFF";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">��ɱ�ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] GetACARSDEPInfoParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightByACARSDEPInfo);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_FlightDate", SqlDbType.Char, 10)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_FlightByACARSDEPInfo, parms);
            }

            if (acarsLegsBM.FLTID.Length == 4)
            {
                parms[0].Value = acarsLegsBM.FLTID;
            }
            else
            {
                parms[0].Value = "****";
            }
            parms[1].Value = acarsLegsBM.DEPSTN;
            parms[2].Value = acarsLegsBM.ARRSTN;
            parms[3].Value = acarsLegsBM.LONG_REG;
            if (acarsLegsBM.MessageType == MsgType.OFF)
            {
                parms[4].Value = acarsLegsBM.TOFF.Substring(0, 10);
            }
            else
            {
                parms[4].Value = acarsLegsBM.PushTime.Substring(0, 10);
            }
            return parms;
        }

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">��ɱ�ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] GetACARSDEPParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightByACARSDEP);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcLONG_REG",  SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_StartTOFF", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTOFF", SqlDbType.Char, 19)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_FlightByACARSDEP, parms);
            }

            parms[0].Value = acarsLegsBM.LONG_REG;

            //�����OFF������ʹ��OFFʱ����ɻ���ETD�Ƚ�
            if (acarsLegsBM.MessageType == MsgType.OFF)
            {
                parms[1].Value = DateTime.Parse(acarsLegsBM.TOFF).AddMinutes(-90).ToString("yyyy-MM-dd HH:mm:ss");
                parms[2].Value = DateTime.Parse(acarsLegsBM.TOFF).AddMinutes(90).ToString("yyyy-MM-dd HH:mm:ss");
            }
                //�����OUT������ʹ��OUTʱ����бȽ�
            else if (acarsLegsBM.MessageType == MsgType.OUT)
            {
                parms[1].Value = DateTime.Parse(acarsLegsBM.PushTime).AddMinutes(-90).ToString("yyyy-MM-dd HH:mm:ss");
                parms[2].Value = DateTime.Parse(acarsLegsBM.PushTime).AddMinutes(90).ToString("yyyy-MM-dd HH:mm:ss");
            }
                //�����RTN������ʹ��OUTʱ����бȽϣ����OUTʱ��Ϊ�գ���ʹ��RTNʱ��
            else
            {
                parms[1].Value = DateTime.Parse(acarsLegsBM.PushTime).AddMinutes(-90).ToString("yyyy-MM-dd HH:mm:ss");
                parms[2].Value = DateTime.Parse(acarsLegsBM.PushTime).AddMinutes(90).ToString("yyyy-MM-dd HH:mm:ss");
            }

            return parms;
        }

        /// <summary>
        /// ����acars��ɱ���Ϣ��ȡ����
        /// </summary>
        /// <param name="acarsLegsBM">��ɱ�ʵ�����</param>
        /// <returns></returns>
        public DataTable GetFlightByDEPInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            DataTable dtResult = new DataTable();
            SqlParameter[] parms = GetACARSDEPInfoParameters(acarsLegsBM);
            if (parms[2].Value.ToString() != "")
            {
                dtResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByACARSDEPInfo, parms);
            }
            else
            {
                dtResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByACARSDEPInfo, parms);
            }

            if (dtResult.Rows.Count == 0)
            {
                parms = GetACARSDEPParameters(acarsLegsBM);
                dtResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByACARSDEP, parms);
            }

            return dtResult;
        }
        #endregion

        #region ����ACARS��ر��Ļ�ȡ��Ӧ�ĺ��ද̬
        private const string SELECT_FlightByACARSARRInfo = "SELECT " + strDataField + " FROM tbLegs WHERE " +
            "cnvcFLTID like '%' + @PARM_cnvcFLTID AND " +
            "cncDEPSTN=@PARM_cncDEPSTN AND " +
            "cncARRSTN=@PARM_cncARRSTN AND " +
            "cnvcLONG_REG=@PARM_cnvcLONG_REG AND " +
            "LEFT(cncETA, 10)=@PARM_FlightDate AND " +
            "cniDeleteTag = 0 and cncSTATUS<>'CNL' order by cncETA";

        private const string SELECT_FlightBySubACARSARRInfo = "SELECT " + strDataField + " FROM tbLegs WHERE " +
           "cnvcFLTID like '%' + @PARM_cnvcFLTID AND " +
           "cncDEPSTN=@PARM_cncDEPSTN AND " +
           "cnvcLONG_REG=@PARM_cnvcLONG_REG AND " +
           "LEFT(cncETA, 10)=@PARM_FlightDate AND " +
           "cniDeleteTag = 0 and cncSTATUS<>'CNL' order by cncETA";

        private const string SELECT_FlightByACARSARR = "SELECT " + strDataField + " FROM tbLegs WHERE " +
            "cnvcLONG_REG=@PARM_cnvcLONG_REG AND " +
             "cncETA >= @PARM_StartTDWN AND cncETA <= @PARM_EndTDWN";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">��ر�ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] GetACARSARRInfoParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightByACARSARRInfo);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_FlightDate", SqlDbType.Char, 10)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_FlightByACARSARRInfo, parms);
            }

            if (acarsLegsBM.FLTID.Length == 4)
            {
                parms[0].Value = acarsLegsBM.FLTID;
            }
            else
            {
                parms[0].Value = "****";
            }
            parms[1].Value = acarsLegsBM.DEPSTN;
            parms[2].Value = acarsLegsBM.ARRSTN;
            parms[3].Value = acarsLegsBM.LONG_REG;
            if (acarsLegsBM.MessageType == MsgType.ON)
            {
                parms[4].Value = acarsLegsBM.TDWN.Substring(0, 10);
            }
            else
            {
                parms[4].Value = acarsLegsBM.TDWN.Substring(0, 10);
            }
            return parms;
        }

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="acarsLegsBM">��ر�ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] GetACARSARRParameters(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_FlightByACARSARR);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcLONG_REG",  SqlDbType.VarChar, 10),
                    new SqlParameter("@PARM_StartTDWN", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTDWN", SqlDbType.Char, 19)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_FlightByACARSARR, parms);
            }

            parms[0].Value = acarsLegsBM.LONG_REG;
            if (acarsLegsBM.MessageType == MsgType.ON)
            {
                parms[1].Value = DateTime.Parse(acarsLegsBM.TDWN).AddMinutes(-90).ToString("yyyy-MM-dd HH:mm:ss");
                parms[2].Value = DateTime.Parse(acarsLegsBM.TDWN).AddMinutes(90).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                parms[1].Value = DateTime.Parse(acarsLegsBM.ATA).AddMinutes(-90).ToString("yyyy-MM-dd HH:mm:ss");
                parms[2].Value = DateTime.Parse(acarsLegsBM.ATA).AddMinutes(90).ToString("yyyy-MM-dd HH:mm:ss");
            }

            return parms;
        }

        /// <summary>
        /// ����acars��ر���Ϣ��ȡ����
        /// </summary>
        /// <param name="acarsLegsBM">��ر�ʵ�����</param>
        /// <returns></returns>
        public DataTable GetFlightByARRInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            DataTable dtResult = new DataTable();
            SqlParameter[] parms = GetACARSARRInfoParameters(acarsLegsBM);
            if (parms[2].Value.ToString() != "")
            {
                dtResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByACARSARRInfo, parms);
            }
            else
            {
                dtResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByACARSARRInfo, parms);
            }

            if (dtResult.Rows.Count == 0)
            {
                parms = GetACARSARRParameters(acarsLegsBM);
                dtResult = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_FlightByACARSARR, parms);
            }

            return dtResult;
        }
        #endregion

        #region ��ȡ�������ļ�����DataSet����ʽ����
        /// <summary>
        /// ��ȡ�������ļ�����DataSet����ʽ����
        /// </summary>
        /// <param name="strFullFile">����ļ�����·��</param>
        /// <returns></returns>
        public DataSet GetChangeLegsFromFile(string strFullPath)
        {
            DataSet dsChangeLegs = new ScheduleLegsBM();
            try
            {
                dsChangeLegs.ReadXml(strFullPath);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
           return dsChangeLegs;
       }
        #endregion

       #region added by LinYong

       #region ��ȡ�������еĺ��ද̬ --added in 2009.10.26 ,��ȡ tbLegs �����ֶ�
       private const string SELECT_AllLegsByDay = "SELECT * from tbLegs WHERE " +
           "cncETD >= @PARM_StartTime AND cncETD <= @PARM_EndTime";

       private SqlParameter[] GetAllLegsByDayParameters(DateTimeBM dateTimeBM)
       {
           SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_AllLegsByDay);

           if (parms == null)
           {
               parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_StartTime", SqlDbType.Char, 19),
                    new SqlParameter("@PARM_EndTime", SqlDbType.Char, 19)
                };

               SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_AllLegsByDay, parms);
           }

           parms[0].Value = dateTimeBM.StartDateTime;
           parms[1].Value = dateTimeBM.EndDateTime;

           return parms;
       }

       /// <summary>
       /// ��ȡ�������еĺ��ද̬
       /// </summary>
       /// <param name="dateTimeBM"></param>
       /// <returns></returns>
       public DataTable GetAllLegsByDay(DateTimeBM dateTimeBM)
       {
           SqlParameter[] parms = GetAllLegsByDayParameters(dateTimeBM);

           return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_AllLegsByDay, parms);
       }
       #endregion

       #endregion
   }
}
