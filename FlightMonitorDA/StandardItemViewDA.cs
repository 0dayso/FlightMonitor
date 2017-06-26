using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class StandardItemViewDA : SqlDatabase
    {
        public StandardItemViewDA()
        {
        }

        /// <summary>
        /// 查询DataItem表数据
        /// </summary>
        /// <param name="strLongReg">飞机号</param>
        /// <param name="strAirPort">基地四字码</param>
        /// <param name="strFlightType">过站类型</param>
        /// <param name="strOIFlightType">国内/国际航班</param>
        /// <param name="cIOFlight">进出港航班标志</param>
        /// <returns></returns>
        public DataSet GetStandardItem(string strLongReg, string strAirPort, string strFlightType, string strOIFlightType,char cIOFlight)
        {
            try
            {
                string strSQL = "select *,row_number() over(order by t.parentId,t.standardNo desc) as SEQ," +
                        "'' as StartTime,'' as EndTime,'100' as PercentDone,'' as BaseLineStartTime,'' as BaseLineEndTime," +
                        "'1' as isLeaf,'' as responsible,'1' as priority  from (select " +
                        "case when grouping(cnvcParentId) = 1 then '0' else cast(cnvcParentId as varchar) end parentId," +
                        "case when grouping(cniStandardNo) = 1 then 10000 else cast(cniStandardNo as varchar) end standardNo " +
                        "FROM tbStandardItem where " +
                        "cnvcFourCode = (SELECT cncAirportFourCode FROM tbAirportInfo where cncAirPortThreeCode = @PARAM_AirPortFourCode) " +
                        "and cnvcActype = (SELECT cncActype from tbAC_MISC where cnvcLONG_REG = @PARAM_LongReg) " +
                        "and cnvcCompany = (SELECT cnvcOWNER from tbAC_MISC where cnvcLONG_REG = @PARAM_LongReg) " +
                        "and cnvcFlightType = @PARAM_FlightType and cnvcFlightIOType = @PARAM_FlightOIType and cncIOFlight = @PARAM_IOFlight " +
                        "group by cnvcParentId,cniStandardNo with rollup   ) t " +
                        "left join tbStandardItem k on t.parentId = k.cnvcParentId and t.standardNo = k.cniStandardNo ";

                SqlParameter[] parameter = 
                {
                    new SqlParameter("@PARAM_LongReg",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_AirPortFourCode",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FlightType",SqlDbType.VarChar),//过站类型
                    new SqlParameter("@PARAM_FlightOIType",SqlDbType.VarChar),//国内/国际航班类型
                    new SqlParameter("@PARAM_IOFlight",SqlDbType.Char)//进出港航班标志
                };

                parameter[0].Value = strLongReg;
                parameter[1].Value = strAirPort;
                parameter[2].Value = strFlightType;
                parameter[3].Value = strOIFlightType;
                parameter[4].Value = cIOFlight;

                return SqlHelper.ExecuteDataSet(this.DBConnString, CommandType.Text, strSQL, parameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flightParams">查找相应的航班数据</param>
        /// <returns></returns>
        public DataSet GetGuaranteeInfor(FlightParams flightParams)
        {
            if (flightParams == null)
                return null;
            else
            {
                string selectItemSQL = "select * from vw_Legs where cncDATOP = @PARAM_DATOP and cnvcFLTID = @PARAM_FLTID and " + 
                    " cnvcLONG_REG = @PARAM_LONGREG and cncDEPSTN = @PARAM_DEPSTN and cncARRSTN = @PARAM_ARRSTN ";

                SqlParameter[] sParameter = 
                    {
                        new SqlParameter("@PARAM_DATOP",SqlDbType.VarChar),
                        new SqlParameter("@PARAM_FLTID",SqlDbType.VarChar),
                        //new SqlParameter("@PARAM_LEGNO",SqlDbType.Int),
                        new SqlParameter("@PARAM_LONGREG",SqlDbType.VarChar),
                        new SqlParameter("@PARAM_DEPSTN",SqlDbType.VarChar),
                        new SqlParameter("@PARAM_ARRSTN",SqlDbType.VarChar)
                    };

                sParameter[0].Value = flightParams.Datop;
                sParameter[1].Value = flightParams.FlightNum;
                //sParameter[2].Value = flightParams.LegNo;
                sParameter[2].Value = flightParams.LongReg;
                sParameter[3].Value = flightParams.DepStn;
                sParameter[4].Value = flightParams.ArrStn;

                DataSet granteeDataSet = new DataSet();
                granteeDataSet = SqlHelper.ExecuteDataSet(this.DBConnString, CommandType.Text, selectItemSQL, sParameter);

                if (granteeDataSet != null)
                    return granteeDataSet;
                else
                    return null;
            }
        }

        public DataSet GetBStandardItemName()
        {
            DataSet dataSet = new DataSet();
            string strSQL = " select * from tbBigStandardItem ";
            dataSet = SqlHelper.ExecuteDataSet(this.DBConnString, CommandType.Text, strSQL, null);
            return dataSet;
        }
    }
}
