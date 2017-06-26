using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorDA.FocService;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// VIP���ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-17
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class VIPDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public VIPDA()
        {
        }

        /// <summary>
        /// ��FOC��ȡVIP��Ϣ
        /// </summary>
        /// <param name="strStartDATOP">��ʼ���ڣ�UTCʱ�䣩</param>
        /// <returns>��FOC���ص�VIP���ݼ�</returns>
        public DataSet GetVIPFromFoc(string strStartDATOP)
        {
            FocService.FleetWatch objFocService = new FleetWatch();
            return objFocService.GetVIPInfo(strStartDATOP);
        }

        #region �ӱ������ݿ��л�ȡ����VIP
        /// <summary>
        /// ��DATOPΪ�����������ݿ��в�ѯ����VIP��Ϣ
        /// </summary>
        private const string SELECT_VIPByDATOP = "SELECT * FROM vw_VIP WHERE " +
            "cncDATOP>=@PARM_cncDATOP";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="strDATOP">���� UTCʱ��</param>
        /// <returns></returns>
        private SqlParameter[] GetVIPByDATOPParameters(string strDATOP)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_VIPByDATOP);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_VIPByDATOP);
            }
            parms[0].Value = strDATOP;

            return parms;
        }

        /// <summary>
        /// ����DATOP��ȡ����VIP
        /// </summary>
        /// <param name="strDATOP">����</param>
        /// <returns>�������з���������VIP�����ݱ�</returns>
        public DataTable GetVIPByDATOP(string strDATOP)
        {
            SqlParameter[] parms = GetVIPByDATOPParameters(strDATOP);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VIPByDATOP, parms);
        }

        #endregion

        #region  ����ĳ��VIP��Ϣ
        private const string UPDATE_VIPInfor = "UPDATE tbVIP SET " +
            "cnvcPOSITION=@PARM_cnvcPOSITION," +
            "cncCLASS=@PARM_cncCLASS," +
            "cnvcVipType=@PARM_cnvcVipType," +
            "cnvcACCOMPANY_NBR=@PARM_cnvcACCOMPANY_NBR," +
            "cnvcACCOMPANY_LEADER=@PARM_cnvcACCOMPANY_LEADER," +
            "cnvcCONTRACT_NUMBER=@PARM_cnvcCONTRACT_NUMBER," +
            "cnvcINFORM_PERSON=@PARM_cnvcINFORM_PERSON," +
            "cnvcSPECIAL_REQUIREMENTS=@PARM_cnvcSPECIAL_REQUIREMENTS," +
            "cnvcREMARKS=@PARM_cnvcREMARKS WHERE " +
            "cncDATOP=@PARM_cncDATOP AND " +
            "cnvcFLTID=@PARM_cnvcFLTID AND " +
            "cncDEPSTN=@PARM_cncDEPSTN AND " +
            "cncARRSTN=@PARM_cncARRSTN AND " +
            "cnvcNAME=@PARM_cnvcNAME";

        /// <summary>
        /// ��ϸ���VIP��Ϣ�Ĳ���
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] UpdateParameters(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_VIPInfor);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnvcPOSITION", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cncCLASS", SqlDbType.Char, 1),
                    new SqlParameter("@PARM_cnvcVipType", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcACCOMPANY_NBR", SqlDbType.VarChar,3),
                    new SqlParameter("@PARM_cnvcACCOMPANY_LEADER", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcCONTRACT_NUMBER", SqlDbType.VarChar, 40),
                    new SqlParameter("@PARM_cnvcINFORM_PERSON", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcSPECIAL_REQUIREMENTS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcREMARKS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_VIPInfor, parms);
            }

            parms[0].Value = vipBM.POSITION;
            parms[1].Value = vipBM.CLASS;
            parms[2].Value = vipBM.VipType;
            parms[3].Value = vipBM.ACCOMPANY_NBR;
            parms[4].Value = vipBM.ACCOMPANY_LEADER;
            parms[5].Value = vipBM.CONTRACT_NUMBER;
            parms[6].Value = vipBM.INFORM_PERSON;
            parms[7].Value = vipBM.SPECIAL_REQUIREMENTS;
            parms[8].Value = vipBM.REMARKS;
            parms[9].Value = vipBM.DATOP;
            parms[10].Value = vipBM.FLTID;
            parms[11].Value = vipBM.DEPSTN;
            parms[12].Value = vipBM.ARRSTN;
            parms[13].Value = vipBM.Name;

            return parms;
        }

        /// <summary>
        /// ����VIP������Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public int UpdateVipInfor(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = UpdateParameters(vipBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_VIPInfor, parms);
            return retVal;
        }
        #endregion

        #region ����һ���µ�VIP��Ϣ
        private const string INSERT_VIP = "INSERT INTO tbVIP VALUES(@PARM_cncDATOP,@PARM_cnvcFLTID," +
            "@PARM_cncDEPSTN,@PARM_cncARRSTN,@PARM_cnvcNAME,@PARM_cnvcPOSITION,@PARM_cncCLASS," +
            "@PARM_cnvcVipType,@PARM_cnvcACCOMPANY_NBR,@PARM_cnvcACCOMPANY_LEADER,@PARM_cnvcCONTRACT_NUMBER," +
            "@PARM_cnvcINFORM_PERSON,@PARM_cnvcSPECIAL_REQUIREMENTS,@PARM_cnvcREMARKS,@PARM_cnvcDataSource,0,0)";
        
        /// <summary>
        /// ��ϲ������
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_VIP);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PARM_cnvcPOSITION", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cncCLASS", SqlDbType.Char, 1),
                    new SqlParameter("@PARM_cnvcVipType", SqlDbType.VarChar, 50),
                    new SqlParameter("@PARM_cnvcACCOMPANY_NBR", SqlDbType.VarChar,3),
                    new SqlParameter("@PARM_cnvcACCOMPANY_LEADER", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcCONTRACT_NUMBER", SqlDbType.VarChar, 40),
                    new SqlParameter("@PARM_cnvcINFORM_PERSON", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcSPECIAL_REQUIREMENTS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcREMARKS", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PARM_cnvcDataSource", SqlDbType.VarChar, 10)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_VIP, parms);
            }

            parms[0].Value = vipBM.DATOP;
            parms[1].Value = vipBM.FLTID;
            parms[2].Value = vipBM.DEPSTN;
            parms[3].Value = vipBM.ARRSTN;
            parms[4].Value = vipBM.Name;
            parms[5].Value = vipBM.POSITION;
            parms[6].Value = vipBM.CLASS;
            parms[7].Value = vipBM.VipType;
            parms[8].Value = vipBM.ACCOMPANY_NBR;
            parms[9].Value = vipBM.ACCOMPANY_LEADER;
            parms[10].Value = vipBM.CONTRACT_NUMBER;
            parms[11].Value = vipBM.INFORM_PERSON;
            parms[12].Value = vipBM.SPECIAL_REQUIREMENTS;
            parms[13].Value = vipBM.REMARKS;
            parms[14].Value = vipBM.DataSource;

            return parms;
        }

        /// <summary>
        /// ����һ��VIP��Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public int InsertVIP(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = InsertParameters(vipBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, INSERT_VIP, parms);
            return retVal;
        }
        #endregion

        #region ɾ��VIP
        private const string DELETE_VIP = "DELETE FROM tbVIP WHERE " +
            "cncDATOP=@PARM_cncDATOP AND " +
            "cnvcFLTID=@PARM_cnvcFLTID AND " +
            "cncDEPSTN=@PARM_cncDEPSTN AND " +
            "cncARRSTN=@PARM_cncARRSTN";

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] DeleteParameters(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, DELETE_VIP);
            if(parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, DELETE_VIP, parms);
            }
            parms[0].Value = vipBM.DATOP;
            parms[1].Value = vipBM.FLTID;
            parms[2].Value = vipBM.DEPSTN;
            parms[3].Value = vipBM.ARRSTN;
            parms[4].Value = vipBM.Name;

            return parms;
        }

        /// <summary>
        /// ɾ��һ��VIP��Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public int DeleteVIP(FlightMonitorBM.VIPBM vipBM)
        {
            SqlParameter[] parms = DeleteParameters(vipBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, DELETE_VIP, parms);
            return retVal;
        }

        #endregion

        #region ���������ͺ�����Ϣ��ȡһλVIP
        private const string SELECT_VIPByName = "SELECT * FROM tbVIP WHERE " +
            "cncDATOP = @PARM_cncDATOP AND cnvcFLTID = @PARM_cnvcFLTID AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND cncARRSTN = @PARM_cncARRSTN AND " +
            "cnvcNAME = @PAMR_cnvcNAME";

        private SqlParameter[] GetVIPByNameParameters(VIPBM vipBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_VIPByName);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PAMR_cnvcNAME", SqlDbType.NVarChar,50)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_VIPByName, parms);
            }

            parms[0].Value = vipBM.DATOP;
            parms[1].Value = vipBM.FLTID;
            parms[2].Value = vipBM.DEPSTN;
            parms[3].Value = vipBM.ARRSTN;
            parms[4].Value = vipBM.Name;

            return parms;
        }


        /// <summary>
        /// �����ÿ������ͺ�����Ϣ��ȡVIP
        /// </summary>
        /// <param name="vipBM"></param>
        /// <returns></returns>
        public DataTable GetVIPByName(VIPBM vipBM)
        {
            SqlParameter[] parms = GetVIPByNameParameters(vipBM);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VIPByName, parms);
        }
        #endregion

        #region ��ȡĳ����ĳû���߼�ɾ�����߼�ɾ���ĺ����VIP
        private const string SELECT_VIPByFlight = "SELECT * FROM tbVIP WHERE " +
            "cncDATOP = @PARM_cncDATOP AND " +
            "cnvcFLTID = @PARM_cnvcFLTID AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND " +
            "cncARRSTN = @PARM_cncARRSTN AND " +
            "cnbDeleteTag = @PARM_cnbDeleteTag";

        private SqlParameter[] GetVIPByFlightParameters(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_VIPByFlight);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnbDeleteTag", SqlDbType.Bit, 0)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_VIPByFlight, parms);
            }

            parms[0].Value = changeLegsBM.DATOP;
            parms[1].Value = changeLegsBM.FLTID;
            parms[2].Value = changeLegsBM.DEPSTN;
            parms[3].Value = changeLegsBM.ARRSTN;
            parms[4].Value = iDeleteTag;

            return parms;
        }

        /// <summary>
        /// ��ȡ�����VIP��Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public DataTable GetVIPByFlight(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            SqlParameter[] parms = GetVIPByFlightParameters(changeLegsBM, iDeleteTag);

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VIPByFlight, parms);
        }
        #endregion

        #region �߼�ɾ�������һ��VIP
        private const string UPDATE_DeleteTag = "UPDATE tbVIP SET cnbDeleteTag = @PARM_cnbDeleteTag WHERE " +
             "cncDATOP = @PARM_cncDATOP AND " +
            "cnvcFLTID = @PARM_cnvcFLTID AND " +
            "cncDEPSTN = @PARM_cncDEPSTN AND " +
            "cncARRSTN = @PARM_cncARRSTN AND " +
            "cnvcNAME = @PARM_cnvcNAME";

        private SqlParameter[] UpdateDeleteTagParameters(VIPBM vipBM, int iDeleteTag)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_DeleteTag);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cnbDeleteTag", SqlDbType.Bit, 0),
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3),
                    new SqlParameter("@PARM_cnvcNAME", SqlDbType.NVarChar, 50)
                };

                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_DeleteTag, parms);
            }

            parms[0].Value = iDeleteTag;
            parms[1].Value = vipBM.DATOP;
            parms[2].Value = vipBM.FLTID;
            parms[3].Value = vipBM.DEPSTN;
            parms[4].Value = vipBM.ARRSTN;
            parms[5].Value = vipBM.Name;
            

            return parms;
        }

        /// <summary>
        /// �߼�ɾ�������һ������
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public int UpdateDeleteTag(VIPBM vipBM, int iDeleteTag)
        {
            SqlParameter[] parms = UpdateDeleteTagParameters(vipBM, iDeleteTag);

            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_DeleteTag, parms);
        }
        #endregion
    }
}
