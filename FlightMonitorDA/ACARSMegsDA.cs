using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using AirSoft.Public.DataHelper;
using System.Configuration;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ACARS���Ĵ������ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���  ��
    /// �������ڣ�2007-02-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSMegsDA:SqlDatabase
    {
        public ACARSMegsDA()
        { }

        #region �洢OUT��
        /// <summary>
        /// �洢OUT��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOUTMegs(ACARSMegsBM acarsMegBM)
        {
            //������ʽ��OUTʱ��
            string strOUTTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSOUT.Substring(0, 2) + ":" + acarsMegBM.ACARSOUT.Substring(2, 2) + ":00";

            //��ѯ�Ƿ����иú���ļ�¼���뵽���ݿ���
            StringBuilder strSqlS = new StringBuilder();
            strSqlS.Append("select * from tbFlightMegs");
            strSqlS.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlS.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlS.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlS.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlS.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "'");

            //�����¼��SQL���
            StringBuilder strSqlI = new StringBuilder();
            strSqlI.Append("insert into tbFlightMegs(");
            strSqlI.Append("cncDATOP,cnvcFLTID,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncOUT,cncOUTFOB,cncValidateFlag,cniRTNFlag");
            strSqlI.Append(")");
            strSqlI.Append(" values (");
            strSqlI.Append("'" + acarsMegBM.DATOP + "',");
            strSqlI.Append("'" + acarsMegBM.FLTID + "',");
            strSqlI.Append("'" + acarsMegBM.LONG_REG + "',");
            strSqlI.Append("'" + acarsMegBM.DEPSTN + "',");
            strSqlI.Append("'" + acarsMegBM.ARRSTN + "',");
            strSqlI.Append("'" + strOUTTime + "',");
            strSqlI.Append("" + ConvertFOB(acarsMegBM.OUT_FOB) + ",");
            strSqlI.Append("'F',");
            strSqlI.Append("0");
            strSqlI.Append(");select @@IDENTITY");

            //���¼�¼SQL���
            StringBuilder strSqlU = new StringBuilder();
            strSqlU.Append("update tbFlightMegs set ");
            strSqlU.Append("cncOUT='" + strOUTTime + "',");
            strSqlU.Append("cncOUTFOB=" + ConvertFOB(acarsMegBM.OUT_FOB) + "");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");
            //��������ѯ���µļ�¼�ı��
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");

            //����ֵ
            object retVal = new object();

            //��ѯ�Ƿ����иú���ļ�¼���뵽���ݿ���
            SqlDataReader rdr = SqlHelper.ExecuteReader(this.SqlConn, this.Transaction, CommandType.Text, strSqlS.ToString());
            bool bExist = rdr.Read();
            rdr.Close();

            try
            {
                //���û�иú���ļ�¼�������һ���¼�¼
                if (!bExist)
                {
                    retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
                }
                //����������м�¼
                else
                {
                    retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
                }
            }
            catch (Exception ex)
            {
                retVal = -1;
                throw ex;
            }
            return Convert.ToInt32(retVal);
        }
        #endregion

        #region �洢OFF��
        /// <summary>
        /// �洢OFF��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOFFMegs(ACARSMegsBM acarsMegBM)
        {
            //������ʽ��OUTʱ��
            string strOFFTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSOFF.Substring(0, 2) + ":" + acarsMegBM.ACARSOFF.Substring(2, 2) + ":00";

            //OFFʱ���һ����
            DateTime dtOFFTime = Convert.ToDateTime(strOFFTime).AddMinutes(-1);
            strOFFTime = dtOFFTime.ToString("yyyy-MM-dd HH:mm:ss");

            //��ѯͬһ�������OUTʱ��
            string strOUTTime = GetPrevTime(acarsMegBM);
            int iOUTOFF = 0;
            //���OUTʱ����ڣ����¼OUT��OFF��ʱ���
            if (strOUTTime != "")
            {
                TimeSpan tsOUTOFF = Convert.ToDateTime(strOFFTime) - Convert.ToDateTime(strOUTTime);
                iOUTOFF = Convert.ToInt32(tsOUTOFF.TotalMinutes);
            }

            //�����¼��SQL���
            StringBuilder strSqlI = new StringBuilder();
            strSqlI.Append("insert into tbFlightMegs(");
            strSqlI.Append("cncDATOP,cnvcFLTID,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncOFF,cncOFFFOB,cniTOUTOFF,cncValidateFlag,cniRTNFlag");
            strSqlI.Append(")");
            strSqlI.Append(" values (");
            strSqlI.Append("'" + acarsMegBM.DATOP + "',");
            strSqlI.Append("'" + acarsMegBM.FLTID + "',");
            strSqlI.Append("'" + acarsMegBM.LONG_REG + "',");
            strSqlI.Append("'" + acarsMegBM.DEPSTN + "',");
            strSqlI.Append("'" + acarsMegBM.ARRSTN + "',");
            strSqlI.Append("'" + strOFFTime + "',");
            strSqlI.Append("" + ConvertFOB(acarsMegBM.OFF_FOB) + ",");
            strSqlI.Append("" + iOUTOFF + ",");
            strSqlI.Append("'F',");
            strSqlI.Append("0");
            strSqlI.Append(");select @@IDENTITY");

            //���¼�¼SQL���
            StringBuilder strSqlU = new StringBuilder();
            strSqlU.Append("update tbFlightMegs set ");
            strSqlU.Append("cncOFF='" + strOFFTime + "',");
            strSqlU.Append("cncOFFFOB=" + ConvertFOB(acarsMegBM.OFF_FOB) + ",");
            strSqlU.Append("cniTOUTOFF =" + iOUTOFF + "");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");
            //��������ѯ���µļ�¼�ı��
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");

            object retVal = new object();
            int iUpdateFlag = 0;

            //���¼�¼����ȡӰ�������
            try
            {
                iUpdateFlag = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }

            //�����Ӱ��ļ�¼������0��������ؼ�¼������
            if (iUpdateFlag > 0)
            {
                return iUpdateFlag;
            }
            //���򽫸�����¼�������
            else
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }

            return Convert.ToInt32(retVal);
        }
        #endregion

        #region �洢ON��
        /// <summary>
        /// �洢ON��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertONMegs(ACARSMegsBM acarsMegBM)
        {
            //������ʽ��OUTʱ��
            string strONTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSON.Substring(0, 2) + ":" + acarsMegBM.ACARSON.Substring(2, 2) + ":00";

            //�����OFFʱ��
            string strOFFTime = GetPrevTime(acarsMegBM);
            int iOFFON = 0;
            //���OFFʱ�����
            if (strOFFTime != "")
            {
                //��OFF��ON��ʱ����
                TimeSpan tsOFFON = Convert.ToDateTime(strONTime) - Convert.ToDateTime(strOFFTime);
                iOFFON = Convert.ToInt32(tsOFFON.TotalMinutes);
            }

            //�����¼��SQL���
            StringBuilder strSqlI = new StringBuilder();
            strSqlI.Append("insert into tbFlightMegs(");
            strSqlI.Append("cncDATOP,cnvcFLTID,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncON,cncONFOB,cniTOFFON, cncValidateFlag,cniRTNFlag");
            strSqlI.Append(")");
            strSqlI.Append(" values (");
            strSqlI.Append("'" + acarsMegBM.DATOP + "',");
            strSqlI.Append("'" + acarsMegBM.FLTID + "',");
            strSqlI.Append("'" + acarsMegBM.LONG_REG + "',");
            strSqlI.Append("'" + acarsMegBM.DEPSTN + "',");
            strSqlI.Append("'" + acarsMegBM.ARRSTN + "',");
            strSqlI.Append("'" + strONTime + "',");
            strSqlI.Append("" + ConvertFOB(acarsMegBM.ON_FOB) + ",");
            strSqlI.Append("" + iOFFON + ",");
            strSqlI.Append("'F',");
            strSqlI.Append("0");
            strSqlI.Append(");select @@IDENTITY");

            //���¼�¼SQL���
            StringBuilder strSqlU = new StringBuilder();
            strSqlU.Append("update tbFlightMegs set ");
            strSqlU.Append("cncON='" + strONTime + "',");
            strSqlU.Append("cncONFOB=" + ConvertFOB(acarsMegBM.ON_FOB) + ",");
            strSqlU.Append("cniTOFFON=" + iOFFON + "");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");
            //��������ѯ���µļ�¼�ı��
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");

            object retVal = new object();
            int iUpdateFlag = 0;

            //���¼�¼����ȡӰ�������
            try
            {
                iUpdateFlag = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //�����Ӱ��ļ�¼������0��������ؼ�¼������
            if (iUpdateFlag > 0)
            {
                return iUpdateFlag;
            }
            //���򽫸�����¼�������
            else
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }

            return Convert.ToInt32(retVal);
        }
        #endregion

        #region �洢IN��
        /// <summary>
        /// �洢IN��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertINMegs(ACARSMegsBM acarsMegBM)
        {
            //������ʽ��OUTʱ��
            string strINTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSIN.Substring(0, 2) + ":" + acarsMegBM.ACARSIN.Substring(2, 2) + ":00";
            //�ú������б����Ƿ������ı��
            string strValidateFlag = CheckFlightMegs(acarsMegBM);

            //�����ONʱ��
            string strONTime = GetPrevTime(acarsMegBM);
            int iINON = 0;
            //���ONʱ�����
            if (strONTime != "")
            {
                //��ON��IN��ʱ����
                TimeSpan tsINON = Convert.ToDateTime(strINTime) - Convert.ToDateTime(strONTime);
                iINON = Convert.ToInt16(tsINON.TotalMinutes);
            }

            //�����¼��SQL���
            StringBuilder strSqlI = new StringBuilder();
            strSqlI.Append("insert into tbFlightMegs(");
            strSqlI.Append("cncDATOP,cnvcFLTID,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncIN,cncINFOB,cniTONIN, cncValidateFlag,cniRTNFlag");
            strSqlI.Append(")");
            strSqlI.Append(" values (");
            strSqlI.Append("'" + acarsMegBM.DATOP + "',");
            strSqlI.Append("'" + acarsMegBM.FLTID + "',");
            strSqlI.Append("'" + acarsMegBM.LONG_REG + "',");
            strSqlI.Append("'" + acarsMegBM.DEPSTN + "',");
            strSqlI.Append("'" + acarsMegBM.ARRSTN + "',");
            strSqlI.Append("'" + strINTime + "',");
            strSqlI.Append("" + ConvertFOB(acarsMegBM.IN_FOB) + ",");
            strSqlI.Append("" + iINON + ",");
            strSqlI.Append("'" + strValidateFlag + "',");
            strSqlI.Append("0");
            strSqlI.Append(");select @@IDENTITY");

            //���¼�¼SQL���
            StringBuilder strSqlU = new StringBuilder();
            strSqlU.Append("update tbFlightMegs set ");
            strSqlU.Append("cncIN='" + strINTime + "',");
            strSqlU.Append("cncINFOB=" + ConvertFOB(acarsMegBM.IN_FOB) + ",");
            strSqlU.Append("cniTONIN=" + iINON + ",");
            strSqlU.Append("cncValidateFlag='" + strValidateFlag + "'");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");
            //��������ѯ���µļ�¼�ı��
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");
            
            object retVal = new object();
            int iUpdateFlag = 0;

            //���¼�¼����ȡӰ�������
            try
            {
                iUpdateFlag = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //�����Ӱ��ļ�¼������0��������ؼ�¼������
            if (iUpdateFlag > 0)
            {
                return iUpdateFlag;
            }
            //���򽫸�����¼�������
            else
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }

            return Convert.ToInt32(retVal);
        }
        #endregion

        #region �洢RTN��
        /// <summary>
        /// �洢RTN��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertRTNMegs(ACARSMegsBM acarsMegBM)
        {
            //������ʽ��OUTʱ��
            string strOUTTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSOUT.Substring(0, 2) + ":" + acarsMegBM.ACARSOUT.Substring(2, 2) + ":00";
            //������ʽ��RTNʱ��
            string strRTNTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSRTN.Substring(0, 2) + ":" + acarsMegBM.ACARSRTN.Substring(2, 2) + ":00";
            
            //��OUT��RTN��ʱ����
            TimeSpan tsOUTRTN = Convert.ToDateTime(strRTNTime) - Convert.ToDateTime(strOUTTime);
            int iOUTRTN = Convert.ToInt16(tsOUTRTN.TotalMinutes);

            //�������ݿ��SQL���
            StringBuilder strSqlI = new StringBuilder();
            strSqlI.Append("insert into tbRTNMegs(");
            strSqlI.Append("cncDATOP,cnvcFLTID,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncOUT,cncRTN,cncRTNFOB, cniOUTRTN");
            strSqlI.Append(")");
            strSqlI.Append(" values (");
            strSqlI.Append("'" + acarsMegBM.DATOP + "',");
            strSqlI.Append("'" + acarsMegBM.FLTID + "',");
            strSqlI.Append("'" + acarsMegBM.LONG_REG + "',");
            strSqlI.Append("'" + acarsMegBM.DEPSTN + "',");
            strSqlI.Append("'" + acarsMegBM.ARRSTN + "',");
            strSqlI.Append("'" + strOUTTime + "',");
            strSqlI.Append("'" + strRTNTime + "',");
            strSqlI.Append("" + ConvertFOB(acarsMegBM.RTN_FOB) + ",");
            strSqlI.Append("" + iOUTRTN + "");
            strSqlI.Append(");select @@IDENTITY");

            object retVal = new object();
            try
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(retVal);
        }
        #endregion

        #region ���º��෵����Ϣ
        /// <summary>
        /// ���º��෵����Ϣ
        /// ����OUTʱ��ͷ�����ǣ����������Ϊ�ղ���RTN���ı�ļ�¼��id
        /// </summary>
        /// <param name="iRTNMegId"></param>
        /// <returns></returns>
        public int UpdateFlightRTNInfo(ACARSMegsBM acarsMegBM, int iRTNMegId)
        {
            //������ʽ��OUTʱ��
            string strOUTTime = acarsMegBM.DATOP + " " + acarsMegBM.ACARSOUT.Substring(0, 2) + ":" + acarsMegBM.ACARSOUT.Substring(2, 2) + ":00";

            //���¼�¼SQL���
            StringBuilder strSqlU = new StringBuilder();
            strSqlU.Append("update tbFlightMegs set ");
            strSqlU.Append("cncOUT='" + strOUTTime + "',");
            strSqlU.Append("cncOUTFOB=" + this.ConvertFOB(acarsMegBM.OUT_FOB) + ",");
            strSqlU.Append("cniRTNFlag = " + iRTNMegId + "");                   //����RTN���ΪRTN����¼ID
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "'");

            int retVal = 0;
            try
            {
                retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        #endregion

        #region �洢��ȷ���ı���
        /// <summary>
        /// �洢��ȷ���ı���
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertUnCertMegs(ACARSMegsBM acarsMegBM)
        {
            int iUnCertType = 1;
            string strMegType = "";
            switch (acarsMegBM.MessageType)
            {
                case MsgType.OUT:
                    if (acarsMegBM.ACARSOUT != "")
                    {
                        //���ʱ����������Ϊ����1������Ϊ����2
                        iUnCertType = acarsMegBM.ACARSOUT.Length == 4 ? 1 : 2;
                    }
                    else
                    {
                        iUnCertType = 2;
                    }
                    strMegType = "OUT";
                    break;
                case MsgType.OFF:
                    if (acarsMegBM.ACARSOFF != "")
                    {
                        //���ʱ����������Ϊ����1������Ϊ����2
                        iUnCertType = acarsMegBM.ACARSOFF.Length == 4 ? 1 : 2;
                    }
                    else
                    {
                        iUnCertType = 2;
                    }
                    strMegType = "OFF";
                    break;
                case MsgType.ON:
                    if (acarsMegBM.ACARSON != "")
                    {
                        //���ʱ����������Ϊ����1������Ϊ����2
                        iUnCertType = acarsMegBM.ACARSON.Length == 4 ? 1 : 2;
                    }
                    else
                    {
                        iUnCertType = 2;
                    }
                    strMegType = "ON";
                    break;
                case MsgType.IN:
                    if (acarsMegBM.ACARSIN != "")
                    {
                        //���ʱ����������Ϊ����1������Ϊ����2
                        iUnCertType = acarsMegBM.ACARSIN.Length == 4 ? 1 : 2;
                    }
                    else
                    {
                        iUnCertType = 2;
                    }
                    strMegType = "IN";
                    break;
                case MsgType.RTN:
                    if (acarsMegBM.ACARSRTN != "")
                    {
                        //���ʱ����������Ϊ����1������Ϊ����2
                        iUnCertType = acarsMegBM.ACARSRTN.Length == 4 ? 1 : 2;
                    }
                    else
                    {
                        iUnCertType = 2;
                    }
                    strMegType = "RTN";
                    break;
            }

            StringBuilder strSqlI = new StringBuilder();
            strSqlI.Append("insert into tbUnCertMegs(");
            strSqlI.Append("cncDATOP,cnvcFLTID,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cnvcMegsType,cniUnCertMegsType,cntACARSMessage,cncMessageSendTime,cncMessageProcTime,cniFlightId");
            strSqlI.Append(")");
            strSqlI.Append(" values (");
            strSqlI.Append("'" + acarsMegBM.DATOP + "',");
            strSqlI.Append("'" + acarsMegBM.FLTID + "',");
            strSqlI.Append("'" + acarsMegBM.LONG_REG + "',");
            strSqlI.Append("'" + acarsMegBM.DEPSTN + "',");
            strSqlI.Append("'" + acarsMegBM.ARRSTN + "',");
            strSqlI.Append("'" + strMegType + "',");
            strSqlI.Append("" + iUnCertType + ",");
            strSqlI.Append("'" + acarsMegBM.MessageContent + "',");
            strSqlI.Append("'" + acarsMegBM.MessageSendTime + "',");
            strSqlI.Append("'" + acarsMegBM.MessageProcTime + "',");
            strSqlI.Append("0");        //����������Ϊ0
            strSqlI.Append(");select @@IDENTITY");

            object retVal = new object();
            try
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }

            //���ز����е�ID
            return Convert.ToInt32(retVal);
        }
        #endregion

        #region ���²�ȷ�����ı�
        /// <summary>
        /// ���²�ȷ�����ı�
        /// ���Ӹ������������������������Ϣ
        /// </summary>
        /// <param name="iFlightId"></param>
        /// <param name="iUnCertMegId"></param>
        /// <returns></returns>
        public int UpdateUnCertMeg(int iFlightId, int iUnCertMegId)
        {
            string strSqlU = "update tbUnCertMegs set cniFlightId = " + iFlightId + " where iPK = " + iUnCertMegId;

            int retVal = 0;
            try
            {
                retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        #endregion

        #region �洢ԭʼ����
        /// <summary>
        /// �洢ԭʼ����
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOrigMegs(ACARSMegsBM acarsMegBM)
        {
            StringBuilder strSqlI = new StringBuilder();
            strSqlI.Append("insert into tbOrigMegs(");
            strSqlI.Append("cncDATOP,cnvcFLTID,cnvcLONG_REG,cnvcEquipType,cncDEPSTN,cncARRSTN,cntACARSMessage,cncMessageSendTime,cncMessageProcTime");
            strSqlI.Append(")");
            strSqlI.Append(" values (");
            strSqlI.Append("'" + acarsMegBM.DATOP + "',");
            strSqlI.Append("'" + acarsMegBM.FLTID + "',");
            strSqlI.Append("'" + acarsMegBM.LONG_REG + "',");
            strSqlI.Append("'" + acarsMegBM.EquipType + "',");
            strSqlI.Append("'" + acarsMegBM.DEPSTN + "',");
            strSqlI.Append("'" + acarsMegBM.ARRSTN + "',");
            strSqlI.Append("'" + acarsMegBM.MessageContent + "',");
            strSqlI.Append("'" + acarsMegBM.MessageSendTime + "',");
            strSqlI.Append("'" + acarsMegBM.MessageProcTime + "'");
            strSqlI.Append(")");

            int retVal = 0;
            try
            {
                retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        #endregion

        #region ������һ�����ĵ�ʱ��
        /// <summary>
        /// �����ض����ĵ�ǰһ��ʱ��
        /// ��OFF��OUT����ON��OFF����IN��ON����RTN��OUT
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string GetPrevTime(ACARSMegsBM acarsMegBM)
        {
            //����ǰһ��ʱ�䣬��OFF��OUT����ON��OFF����IN��ON����RTN��OUT
            string strPrevTime = "";
            //��ѯ�ı�������RTN��tbRTNMegs����������tbFlightMegs
            string strTbName = "";
            //Ҫ���ص��ֶ���
            string strCnName = "";
            switch (acarsMegBM.MessageType)
            { 
                case MsgType.OFF:
                    strTbName = "tbFlightMegs";
                    strCnName = "cncOUT";
                    break;
                case MsgType.ON:
                    strTbName = "tbFlightMegs";
                    strCnName = "cncOFF";
                    break;
                case MsgType.IN:
                    strTbName = "tbFlightMegs";
                    strCnName = "cncON";
                    break;
                case MsgType.RTN:
                    strTbName = "tbRTNMegs";
                    strCnName = "cncOUT";
                    break;
                default:
                    break;
            }

            //��ѯ���ݵ�SQL���
            StringBuilder strSqlS = new StringBuilder();
            strSqlS.Append("select * from " + strTbName + "");
            strSqlS.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlS.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlS.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlS.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlS.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "'");

            SqlDataReader rdr = SqlHelper.ExecuteReader(this.SqlConn, this.Transaction, CommandType.Text, strSqlS.ToString());
            if (rdr.Read())
            {
                strPrevTime = rdr[strCnName].ToString();
            }
            rdr.Close();
            return strPrevTime;
        }
        #endregion

        #region ��ѯ�ú���ı����Ƿ�����
        /// <summary>
        /// ��ѯ�ú���ı����Ƿ�����
        /// OOOI��������
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string CheckFlightMegs(ACARSMegsBM acarsMegBM)
        {
            //��ѯ���ݵ�SQL���
            StringBuilder strSqlS = new StringBuilder();
            strSqlS.Append("select * from tbFlightMegs");
            strSqlS.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlS.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlS.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlS.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlS.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "'");

            //�Ƿ�������ǣ�F=��������T=����
            string retVal = "F";

            SqlDataReader rdr = SqlHelper.ExecuteReader(this.SqlConn, this.Transaction, CommandType.Text, strSqlS.ToString());
            if (rdr.Read())
            {
                string strOUTTime = rdr["cncOUT"].ToString();
                string strOFFTime = rdr["cncOFF"].ToString();
                string strONTime = rdr["cncON"].ToString();
                //string strINTime = rdr["cncIN"].ToString();

                //�������ʱ�䶼��Ϊ�գ���˵���ú��౨������������True
                //��Ϊ�ڲ���IN��ʱ��ǰ�жϣ����Խ���������ʱ������ж�
                if (strOUTTime != "" && strOFFTime != "" && strOUTTime != "")
                {
                    retVal = "T";
                }
            }
            rdr.Close();
            return retVal;
        }
        #endregion

        #region ��������Ϣ���ַ���ת��������
        /// <summary>
        /// ��������Ϣ���ַ���ת��������
        /// </summary>
        /// <param name="strFOB"></param>
        /// <returns></returns>
        public int ConvertFOB(string strFOB)
        {
            int retVal = 0;
            if (strFOB != "")
            {
                retVal = Convert.ToInt16(strFOB);
            }
            return retVal;
        }
        #endregion

        #region �����Ĳ���FOCϵͳ
        /// <summary>
        /// ��MVA������FOCϵͳ
        /// </summary>
        /// <param name="strMVA">MVA�����ַ���</param>
        /// <returns></returns>
        public int InsertMVAMegs(string strMVA, string strConn)
        {
            OracleCommand cmd = new OracleCommand();
            CommandType cmdType = CommandType.Text;

            int retVal = 0;
            string strInsertTime = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            using (OracleConnection objConnection = new OracleConnection(strConn))
            {
                string cmdText = "insert into FLEETWATCH.INBOX values('" + strInsertTime + "', 'ACARS', 'MVA', '" + strMVA + "')";
                PrepareCommand(cmd, objConnection, null, cmdType, cmdText, null);
                retVal = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            return retVal;
        }
        #endregion

        #region �������ݿ��ѯ����
        /// <summary>
        /// �������ݿ��ѯ����
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region ��FE���Ĳ������ݿ�
        public int InsertFEMeg(string strFE)
        {
            string strSql = "insert into tbMegs (cntFEMegContent,cncInsertTime) values ('" + strFE + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            int retVal = 0;
            try
            {
                retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        #endregion

        #region ��ȡFE�������ڷ���
        public DataTable GetFEMegs(int iMaxNo)
        {
            string strSql = "SELECT iPK,cntFEMegContent,cncInsertTime FROM tbMegs where iPK > " + iMaxNo;

            DataTable dt = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql);

            return dt;
        }
        #endregion
    }
}
