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
    /// ACARS报文处理数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：张  黎
    /// 创建日期：2007-02-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSMegsDA:SqlDatabase
    {
        public ACARSMegsDA()
        { }

        #region 存储OUT报
        /// <summary>
        /// 存储OUT报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOUTMegs(ACARSMegsBM acarsMegBM)
        {
            //完整格式的OUT时间
            string strOUTTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSOUT.Substring(0, 2) + ":" + acarsMegBM.ACARSOUT.Substring(2, 2) + ":00";

            //查询是否已有该航班的记录插入到数据库中
            StringBuilder strSqlS = new StringBuilder();
            strSqlS.Append("select * from tbFlightMegs");
            strSqlS.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlS.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlS.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlS.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlS.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "'");

            //插入记录的SQL语句
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

            //更新记录SQL语句
            StringBuilder strSqlU = new StringBuilder();
            strSqlU.Append("update tbFlightMegs set ");
            strSqlU.Append("cncOUT='" + strOUTTime + "',");
            strSqlU.Append("cncOUTFOB=" + ConvertFOB(acarsMegBM.OUT_FOB) + "");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");
            //更新完后查询更新的记录的编号
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");

            //返回值
            object retVal = new object();

            //查询是否已有该航班的记录插入到数据库中
            SqlDataReader rdr = SqlHelper.ExecuteReader(this.SqlConn, this.Transaction, CommandType.Text, strSqlS.ToString());
            bool bExist = rdr.Read();
            rdr.Close();

            try
            {
                //如果没有该航班的记录，则插入一条新纪录
                if (!bExist)
                {
                    retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
                }
                //否则更新已有记录
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

        #region 存储OFF报
        /// <summary>
        /// 存储OFF报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOFFMegs(ACARSMegsBM acarsMegBM)
        {
            //完整格式的OUT时间
            string strOFFTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSOFF.Substring(0, 2) + ":" + acarsMegBM.ACARSOFF.Substring(2, 2) + ":00";

            //OFF时间减一分钟
            DateTime dtOFFTime = Convert.ToDateTime(strOFFTime).AddMinutes(-1);
            strOFFTime = dtOFFTime.ToString("yyyy-MM-dd HH:mm:ss");

            //查询同一个航班的OUT时间
            string strOUTTime = GetPrevTime(acarsMegBM);
            int iOUTOFF = 0;
            //如果OUT时间存在，则记录OUT到OFF的时间差
            if (strOUTTime != "")
            {
                TimeSpan tsOUTOFF = Convert.ToDateTime(strOFFTime) - Convert.ToDateTime(strOUTTime);
                iOUTOFF = Convert.ToInt32(tsOUTOFF.TotalMinutes);
            }

            //插入记录的SQL语句
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

            //更新记录SQL语句
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
            //更新完后查询更新的记录的编号
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");

            object retVal = new object();
            int iUpdateFlag = 0;

            //更新记录，获取影响的行数
            try
            {
                iUpdateFlag = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }

            //如果受影响的记录数大于0，即有相关记录被更新
            if (iUpdateFlag > 0)
            {
                return iUpdateFlag;
            }
            //否则将该条记录插入表中
            else
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }

            return Convert.ToInt32(retVal);
        }
        #endregion

        #region 存储ON报
        /// <summary>
        /// 存储ON报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertONMegs(ACARSMegsBM acarsMegBM)
        {
            //完整格式的OUT时间
            string strONTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSON.Substring(0, 2) + ":" + acarsMegBM.ACARSON.Substring(2, 2) + ":00";

            //航班的OFF时间
            string strOFFTime = GetPrevTime(acarsMegBM);
            int iOFFON = 0;
            //如果OFF时间存在
            if (strOFFTime != "")
            {
                //从OFF到ON的时间间隔
                TimeSpan tsOFFON = Convert.ToDateTime(strONTime) - Convert.ToDateTime(strOFFTime);
                iOFFON = Convert.ToInt32(tsOFFON.TotalMinutes);
            }

            //插入记录的SQL语句
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

            //更新记录SQL语句
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
            //更新完后查询更新的记录的编号
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");

            object retVal = new object();
            int iUpdateFlag = 0;

            //更新记录，获取影响的行数
            try
            {
                iUpdateFlag = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //如果受影响的记录数大于0，即有相关记录被更新
            if (iUpdateFlag > 0)
            {
                return iUpdateFlag;
            }
            //否则将该条记录插入表中
            else
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }

            return Convert.ToInt32(retVal);
        }
        #endregion

        #region 存储IN报
        /// <summary>
        /// 存储IN报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertINMegs(ACARSMegsBM acarsMegBM)
        {
            //完整格式的OUT时间
            string strINTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSIN.Substring(0, 2) + ":" + acarsMegBM.ACARSIN.Substring(2, 2) + ":00";
            //该航班所有报文是否完整的标记
            string strValidateFlag = CheckFlightMegs(acarsMegBM);

            //航班的ON时间
            string strONTime = GetPrevTime(acarsMegBM);
            int iINON = 0;
            //如果ON时间存在
            if (strONTime != "")
            {
                //从ON到IN的时间间隔
                TimeSpan tsINON = Convert.ToDateTime(strINTime) - Convert.ToDateTime(strONTime);
                iINON = Convert.ToInt16(tsINON.TotalMinutes);
            }

            //插入记录的SQL语句
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

            //更新记录SQL语句
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
            //更新完后查询更新的记录的编号
            strSqlU.Append("select iPK from tbFlightMegs");
            strSqlU.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlU.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlU.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlU.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlU.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "';");
            
            object retVal = new object();
            int iUpdateFlag = 0;

            //更新记录，获取影响的行数
            try
            {
                iUpdateFlag = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSqlU.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //如果受影响的记录数大于0，即有相关记录被更新
            if (iUpdateFlag > 0)
            {
                return iUpdateFlag;
            }
            //否则将该条记录插入表中
            else
            {
                retVal = SqlHelper.ExecuteScalar(this.SqlConn, this.Transaction, CommandType.Text, strSqlI.ToString());
            }

            return Convert.ToInt32(retVal);
        }
        #endregion

        #region 存储RTN报
        /// <summary>
        /// 存储RTN报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertRTNMegs(ACARSMegsBM acarsMegBM)
        {
            //完整格式的OUT时间
            string strOUTTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSOUT.Substring(0, 2) + ":" + acarsMegBM.ACARSOUT.Substring(2, 2) + ":00";
            //完整格式的RTN时间
            string strRTNTime = acarsMegBM.FlightDate + " " + acarsMegBM.ACARSRTN.Substring(0, 2) + ":" + acarsMegBM.ACARSRTN.Substring(2, 2) + ":00";
            
            //从OUT到RTN的时间间隔
            TimeSpan tsOUTRTN = Convert.ToDateTime(strRTNTime) - Convert.ToDateTime(strOUTTime);
            int iOUTRTN = Convert.ToInt16(tsOUTRTN.TotalMinutes);

            //插入数据库的SQL语句
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

        #region 更新航班返航信息
        /// <summary>
        /// 更新航班返航信息
        /// 更改OUT时间和返航标记，将标记设置为刚插入RTN报文表的记录的id
        /// </summary>
        /// <param name="iRTNMegId"></param>
        /// <returns></returns>
        public int UpdateFlightRTNInfo(ACARSMegsBM acarsMegBM, int iRTNMegId)
        {
            //完整格式的OUT时间
            string strOUTTime = acarsMegBM.DATOP + " " + acarsMegBM.ACARSOUT.Substring(0, 2) + ":" + acarsMegBM.ACARSOUT.Substring(2, 2) + ":00";

            //更新记录SQL语句
            StringBuilder strSqlU = new StringBuilder();
            strSqlU.Append("update tbFlightMegs set ");
            strSqlU.Append("cncOUT='" + strOUTTime + "',");
            strSqlU.Append("cncOUTFOB=" + this.ConvertFOB(acarsMegBM.OUT_FOB) + ",");
            strSqlU.Append("cniRTNFlag = " + iRTNMegId + "");                   //更改RTN标记为RTN报记录ID
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

        #region 存储不确定的报文
        /// <summary>
        /// 存储不确定的报文
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
                        //如果时间完整，则为类型1，否则为类型2
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
                        //如果时间完整，则为类型1，否则为类型2
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
                        //如果时间完整，则为类型1，否则为类型2
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
                        //如果时间完整，则为类型1，否则为类型2
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
                        //如果时间完整，则为类型1，否则为类型2
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
            strSqlI.Append("0");        //处理标记设置为0
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

            //返回插入行的ID
            return Convert.ToInt32(retVal);
        }
        #endregion

        #region 更新不确定报文表
        /// <summary>
        /// 更新不确定报文表
        /// 增加该条报文修正后所属航班的信息
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

        #region 存储原始报文
        /// <summary>
        /// 存储原始报文
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

        #region 查找上一个报文的时间
        /// <summary>
        /// 查找特定报文的前一个时间
        /// 对OFF是OUT，对ON是OFF，对IN是ON，对RTN是OUT
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string GetPrevTime(ACARSMegsBM acarsMegBM)
        {
            //查找前一个时间，对OFF是OUT，对ON是OFF，对IN是ON，对RTN是OUT
            string strPrevTime = "";
            //查询的表名，对RTN是tbRTNMegs，对其他是tbFlightMegs
            string strTbName = "";
            //要返回的字段名
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

            //查询数据的SQL语句
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

        #region 查询该航班的报文是否完整
        /// <summary>
        /// 查询该航班的报文是否完整
        /// OOOI报都存在
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string CheckFlightMegs(ACARSMegsBM acarsMegBM)
        {
            //查询数据的SQL语句
            StringBuilder strSqlS = new StringBuilder();
            strSqlS.Append("select * from tbFlightMegs");
            strSqlS.Append(" where cncDATOP='" + acarsMegBM.DATOP + "' and ");
            strSqlS.Append("cnvcFLTID='" + acarsMegBM.FLTID + "' and ");
            strSqlS.Append("cnvcLONG_REG='" + acarsMegBM.LONG_REG + "' and ");
            strSqlS.Append("cncDEPSTN='" + acarsMegBM.DEPSTN + "' and ");
            strSqlS.Append("cncARRSTN='" + acarsMegBM.ARRSTN + "'");

            //是否完整标记：F=不完整；T=完整
            string retVal = "F";

            SqlDataReader rdr = SqlHelper.ExecuteReader(this.SqlConn, this.Transaction, CommandType.Text, strSqlS.ToString());
            if (rdr.Read())
            {
                string strOUTTime = rdr["cncOUT"].ToString();
                string strOFFTime = rdr["cncOFF"].ToString();
                string strONTime = rdr["cncON"].ToString();
                //string strINTime = rdr["cncIN"].ToString();

                //如果三个时间都不为空，则说明该航班报文完整，返回True
                //因为在插入IN报时间前判断，所以仅对这三个时间进行判断
                if (strOUTTime != "" && strOFFTime != "" && strOUTTime != "")
                {
                    retVal = "T";
                }
            }
            rdr.Close();
            return retVal;
        }
        #endregion

        #region 将油量信息从字符型转换成整型
        /// <summary>
        /// 将油量信息从字符型转换成整型
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

        #region 将报文插入FOC系统
        /// <summary>
        /// 将MVA报插入FOC系统
        /// </summary>
        /// <param name="strMVA">MVA报文字符串</param>
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

        #region 配置数据库查询参数
        /// <summary>
        /// 配置数据库查询参数
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

        #region 将FE报文插入数据库
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

        #region 提取FE报文用于发送
        public DataTable GetFEMegs(int iMaxNo)
        {
            string strSql = "SELECT iPK,cntFEMegContent,cncInsertTime FROM tbMegs where iPK > " + iMaxNo;

            DataTable dt = SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, strSql);

            return dt;
        }
        #endregion
    }
}
