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
    /// 航班计划数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    class ScheduleLegsDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleLegsDA()
        {
        }

        #region
        /// <summary>
        /// 载入航班计划数据存储过程
        /// </summary>
        private const string LOAD_ScheduleLegs = "sp_dbFlightMonitor_Ins";

        /// <summary>
        /// 组合载入航班计划数据参数
        /// </summary>
        /// <returns></returns>
        private SqlParameter[] LoadParameters()
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, LOAD_ScheduleLegs);

            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cncDATOP", SqlDbType.Char, 10, "DATOP"),
                    new SqlParameter("@PARM_cnvcFLTID", SqlDbType.VarChar, 8, "FLTID"),
                    new SqlParameter("@PARM_cniLEGNO", SqlDbType.Int, 0, "LegNO"),
                    new SqlParameter("@PARM_cnvcAC", SqlDbType.VarChar, 9, "AC"),
                    new SqlParameter("@PARM_cncFlightDate", SqlDbType.Char, 10, "FlightDate"),
                    new SqlParameter("@PARM_cncCKIFlightDate", SqlDbType.Char, 10, "FlightDate"),
                    new SqlParameter("@PARM_cnvcFlightNo", SqlDbType.VarChar, 8, "FLTID"),
                    new SqlParameter("@PARM_cnvcCKIFlightNo", SqlDbType.VarChar, 8, "FLTID"),
                    new SqlParameter("@PARM_cnvcLONG_REG", SqlDbType.VarChar, 10, "LONG_REG"),
                    new SqlParameter("@PARM_cncDEPSTN", SqlDbType.Char, 3, "DEPSTN"),
                    new SqlParameter("@PARM_cncARRSTN", SqlDbType.Char, 3, "ARRSTN"),
                    new SqlParameter("@PARM_cncSTD", SqlDbType.Char, 19, "STD"),
                    new SqlParameter("@PARM_cncSTA", SqlDbType.Char, 19, "STA"),
                    new SqlParameter("@PARM_cncSTATUS", SqlDbType.Char, 3, "STATUS"),
                    new SqlParameter("@PARM_cncETD", SqlDbType.Char, 19, "ETD"),
                    new SqlParameter("@PARM_cncETA", SqlDbType.Char, 19, "ETA"),
                    new SqlParameter("@PARM_cncATD", SqlDbType.Char, 19, "ATD"),
                    new SqlParameter("@PARM_cncTOFF", SqlDbType.Char, 19, "TOFF"),
                    new SqlParameter("@PARM_cncTDWN", SqlDbType.Char, 19, "TDWN"),
                    new SqlParameter("@PARM_cncATA", SqlDbType.Char, 19, "ATA"),
                    new SqlParameter("@PARM_cnvcTRI_FLTID", SqlDbType.VarChar, 8, "TRI_FLTID"),
                    new SqlParameter("@PARM_cnvcDIV_RCODE", SqlDbType.VarChar, 2, "DIV_RCODE"),
                    new SqlParameter("@PARM_cnvcDIV_FLAG", SqlDbType.VarChar, 1, "DIV_FLAG"),
                    new SqlParameter("@PARM_cnvcPAX", SqlDbType.VarChar, 12, "PAX"),
                    new SqlParameter("@PARM_cnvcBOOK", SqlDbType.VarChar, 12, "BOOK"),
                    new SqlParameter("@PARM_cnvcDELAY1", SqlDbType.VarChar, 3, "DELAY1"),
                    new SqlParameter("@PARM_cniDUR1", SqlDbType.Int, 0, "DUR1"),
                    new SqlParameter("@PARM_cnvcDELAY2", SqlDbType.VarChar, 3, "DELAY2"),
                    new SqlParameter("@PARM_cniDUR2", SqlDbType.Int, 0, "DUR2"),
                    new SqlParameter("@PARM_cnvcDELAY3", SqlDbType.VarChar, 3, "DELAY3"),
                    new SqlParameter("@PARM_cniDUR3", SqlDbType.Int, 0, "DUR3"),
                    new SqlParameter("@PARM_cnvcDELAY4", SqlDbType.VarChar, 3, "DELAY4"),
                    new SqlParameter("@PARM_cniDUR4", SqlDbType.Int, 0, "DUR4"),
                    new SqlParameter("@PARM_cnvcGATE", SqlDbType.VarChar, 5, "GATE"),
                    new SqlParameter("@PARM_cnvcSTC", SqlDbType.VarChar, 1, "STC"),
                    new SqlParameter("@PARM_cnvcVERSION", SqlDbType.VarChar, 7, "VERSION"),
                    new SqlParameter("@PARM_cncORIG_ACTYP", SqlDbType.Char, 3, "ORIG_ACTYP"),
                    new SqlParameter("@PARM_cncACTYP", SqlDbType.Char, 3, "ACTYP"),
                    new SqlParameter("@PARM_cnvcACOWN", SqlDbType.VarChar, 3, "ACOWN"),
                    new SqlParameter("@PARM_cnvcSEQ", SqlDbType.VarChar, 50, "SEQ")    
                    
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, LOAD_ScheduleLegs, parms);
            }
            return parms;
        }

        /// <summary>
        /// 将航班计划从源数据集加载到目的数据集
        /// </summary>
        /// <param name="sourceScheduleLegsBM">源数据集</param>
        /// <returns>FlightMonitorBM.ScheduleLegsBM</returns>
        private FlightMonitorBM.ScheduleLegsBM ExchangeDS(FlightMonitorBM.ScheduleLegsBM sourceScheduleLegsBM)
        {

            FlightMonitorBM.ScheduleLegsBM destScheduleLegsBM = new ScheduleLegsBM();

            foreach (DataRow dataRow in sourceScheduleLegsBM.Tables["legs"].Rows)
            {
                destScheduleLegsBM.Tables["legs"].Rows.Add(dataRow.ItemArray);
            }
            return destScheduleLegsBM;
        }

       
        /// <summary>
        /// 加载航班计划数据
        /// </summary>
        /// <param name="scheduleLegsBM">要写入到数据库中的数据集</param>
        /// <returns></returns>
        public int LoadScheduleFlight(FlightMonitorBM.ScheduleLegsBM scheduleLegsBM)
        {
            FlightMonitorBM.ScheduleLegsBM destScheduleLegsBM = ExchangeDS(scheduleLegsBM);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();


            dataAdapter.InsertCommand = new SqlCommand("sp_dbFlightMonitor_Ins");
            dataAdapter.InsertCommand.Connection = this.SqlConn;
            dataAdapter.InsertCommand.CommandType = CommandType.StoredProcedure;

            dataAdapter.UpdateCommand = new SqlCommand("sp_dbFlightMonitor_Ins");
            dataAdapter.UpdateCommand.Connection = this.SqlConn;
            dataAdapter.UpdateCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter[] parms = LoadParameters();
            for (int iLoop = 0; iLoop < parms.Length; iLoop++)
            {
                dataAdapter.InsertCommand.Parameters.Add(parms[iLoop]);
            }
            dataAdapter.TableMappings.Add("tbLegs", "legs");



            return dataAdapter.Update(destScheduleLegsBM, "tbLegs");
        }
        #endregion

        #region 插入一条航班动态
        /// <summary>
        /// 插入航班动态存储过程
        /// </summary>
        private const string INSERT_Flight = "sp_dbFlightMonitor_Ins";

        /// <summary>
        /// 组合插入航班参数
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <returns>组合参数</returns>
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
        /// 插入一条航班动态
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <returns>1：成功0：失败</returns>
        public int Insert(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            SqlParameter[] parms = InsertParameters(changeLegsBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.StoredProcedure, INSERT_Flight, parms);

            return retVal;
        }
        #endregion

    }
}
