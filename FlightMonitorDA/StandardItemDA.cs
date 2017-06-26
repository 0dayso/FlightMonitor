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
    /// 标准流程数据访问操作
    /// </summary>
    /// author : yanxian 
    /// date : 2013-11-13
    public class StandardItemDA : SqlDatabase
    {
        #region 添加标准流程操作
        /// <summary>
        /// 添加标准流程操作类
        /// </summary>
        /// <param name="standardBM">标准流程实体类</param>
        /// <returns>0:失败;1:成功</returns>
        public int AddStandardItem(StandardBM standardBM)
        {
            int iReturn = 0;
            try
            {
                string strSQL = "insert into tbStandardItem values (@PARAM_ThreeCode,@PARAM_FourCode,@PARAM_Actype,@PARAM_ItemNo,@PARAM_BeginTimeSpace,@PARAM_EndTimeSpace,@PARAM_BeginRefencePoint,@PARAM_EndRefencePoint,@PARAM_FlightType,@PARAM_View,@PARAM_Company,@PARAM_ItemName,"
                    + "@PARAM_FlightIOType,@PARAM_BeginFactPoint,@PARAM_EndFactPoint,@PARAM_CNCityName,@PARAM_CNBeginRefencePoint,@PARAM_CNEndRefencePoint,@PARAM_CNBeginFactPoint,@PARAM_CNEndFactPoint,@PARAM_IOFlight,@PARAM_ParentId,@PARAM_IsLeaf)";
                SqlParameter []sqlParams = new SqlParameter[]{
                    new SqlParameter("@PARAM_ThreeCode",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FourCode",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_Actype",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_ItemNo",SqlDbType.Int),
                    new SqlParameter("@PARAM_BeginTimeSpace",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_EndTimeSpace",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_BeginRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_EndRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FlightType",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_View",SqlDbType.Int),
                    new SqlParameter("@PARAM_Company",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_ItemName",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FlightIOType",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_BeginFactPoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_EndFactPoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNCityName",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNBeginRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNEndRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNBeginFactPoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNEndFactPoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_IOFlight",SqlDbType.Char),
                    new SqlParameter("@PARAM_ParentId",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_IsLeaf",SqlDbType.Char)
                };

                sqlParams[0].Value = standardBM.ThreeCode;
                sqlParams[1].Value = standardBM.FourCode;
                sqlParams[2].Value = standardBM.Actype;
                sqlParams[3].Value = standardBM.ItemNo;
                sqlParams[4].Value = standardBM.BeginTimeSpace;
                sqlParams[5].Value = standardBM.EndTimeSpace;
                sqlParams[6].Value = standardBM.BeginRefencePoint;
                sqlParams[7].Value = standardBM.EndRefencePoint;
                sqlParams[8].Value = standardBM.FlightType;
                sqlParams[9].Value = standardBM.IView;
                sqlParams[10].Value = standardBM.Company;
                sqlParams[11].Value = standardBM.ItemName;
                sqlParams[12].Value = standardBM.FlightIOType;
                sqlParams[13].Value = standardBM.BeginFactPoint;
                sqlParams[14].Value = standardBM.EndFactPoint;
                sqlParams[15].Value = standardBM.CNCityName;
                sqlParams[16].Value = standardBM.CNBeginRefencePoint;
                sqlParams[17].Value = standardBM.CNEndRefencePoint;
                sqlParams[18].Value = standardBM.CNBeginFactPoint;
                sqlParams[19].Value = standardBM.CNEndFactPoint;
                sqlParams[20].Value = standardBM.IOFlight;
                sqlParams[21].Value = standardBM.ParentId;
                sqlParams[22].Value = standardBM.IsLeaf;
                iReturn = SqlHelper.ExecuteNonQuery(this.DBConnString, CommandType.Text, strSQL, sqlParams);

            }
            catch (Exception e)
            {
                throw e;
            }

            return iReturn;
        }
        #endregion

        #region 删除标准流程操作
        public int DelStandardItem(int iStandardItem)
        {
            int iReturn = 0;
            try
            {
                string strSQL = "delete from tbStandardItem where cniStandardNo = @cniStandardNo";

                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("@cniStandardNo",SqlDbType.Int)
                };

                sqlParams[0].Value = iStandardItem;

                iReturn = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, strSQL, sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }

            return iReturn;
        }
        #endregion

        #region 返回标准流程列表操作
        public DataSet GetStandardItemList(StandardBM conditionStandardBM)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string strCondition = " 1 = 1 ";
                string strSQL = "";


                if (conditionStandardBM == null)
                {
                    strSQL = "select [cniStandardNo],[cnvcItemName],[cnvcCompany],[cnvcFlightIOType],[cnvcFourCode],[cnvcActype],[cnvcCNCityName] "
                        + ",[cnvcCNBeginRefencePoint],[cnvcCNEndRefencePoint],[cnvcCNBeginFactPoint],[cnvcCNEndFactPoint]"
                        + ",[cncIOFlight],[cnvcParentId],[cniItemNo],[cnvcBeginTimeSpace],[cnvcEndTimeSpace],[cnvcBeginRefencePoint],[cnvcThreeCode]"
                        + ",[cnvcEndRefencePoint],[cnvcFlightType],[cniView],[cnvcBeginFactPoint],[cnvcEndFactPoint],[cncIsLeaf] from tbStandardItem where " + strCondition + " order by cniStandardNo desc";
                }
                else
                {
                    if (conditionStandardBM.CNCityName != null && conditionStandardBM.CNCityName != "")
                    {
                        strCondition += " and cnvcCNCityName = '" + conditionStandardBM.CNCityName + "'";
                    }
                    if (conditionStandardBM.ItemName != null && conditionStandardBM.ItemName != "")
                    {
                        strCondition += " and cnvcItemName = '" + conditionStandardBM.ItemName + "'";
                    }
                    if (conditionStandardBM.Actype != null && conditionStandardBM.Actype != "")
                    {
                        strCondition += " and cnvcActype = '" + conditionStandardBM.Actype + "'";
                    }
                    if (conditionStandardBM.BeginTimeSpace != null && conditionStandardBM.BeginTimeSpace != "")
                    {
                        strCondition += " and cnvcBeginTimeSpace = '" + conditionStandardBM.BeginTimeSpace + "'";
                    }
                    if (conditionStandardBM.EndTimeSpace != null && conditionStandardBM.EndTimeSpace != "")
                    {
                        strCondition += " and cnvcEndTimeSpace = '" + conditionStandardBM.EndTimeSpace + "'";
                    }
                    if (conditionStandardBM.CNBeginRefencePoint != null && conditionStandardBM.CNBeginRefencePoint != "")
                    {
                        strCondition += " and cnvcCNBeginRefencePoint = '" + conditionStandardBM.CNBeginRefencePoint + "'";
                    }
                    if (conditionStandardBM.CNEndRefencePoint != null && conditionStandardBM.CNEndRefencePoint != "")
                    {
                        strCondition += " and cnvcCNEndRefencePoint = '" + conditionStandardBM.CNEndRefencePoint + "'";
                    }
                    if (conditionStandardBM.FlightType != null && conditionStandardBM.FlightType != "")
                    {
                        strCondition += " and cnvcFlightType = '" + conditionStandardBM.FlightType + "'";
                    }
                    if (conditionStandardBM.FlightIOType != null && conditionStandardBM.FlightIOType != "")
                    {
                        strCondition += " and cnvcFlightIOType = '" + conditionStandardBM.FlightIOType + "'";
                    }
                    if (conditionStandardBM.CNBeginFactPoint != null && conditionStandardBM.CNBeginFactPoint != "")
                    {
                        strCondition += " and cnvcCNBeginFactPoint = '" + conditionStandardBM.CNBeginFactPoint + "'";
                    }
                    if (conditionStandardBM.CNEndFactPoint != null && conditionStandardBM.CNEndFactPoint != "")
                    {
                        strCondition += " and cnvcCNEndFactPoint = '" + conditionStandardBM.CNEndFactPoint + "'";
                    }
                    //if ( conditionStandardBM.Action!=null && conditionStandardBM.Action.Equals("Search"))
                    //{
                    //    strCondition += " and cniView = " + conditionStandardBM.IView;
                    //}

                    strSQL = "select [cniStandardNo],[cnvcItemName],[cnvcCompany],[cnvcFlightIOType],[cnvcFourCode],[cnvcActype],[cnvcCNCityName] "
                                + ",[cnvcCNBeginRefencePoint],[cnvcCNEndRefencePoint],[cnvcCNBeginFactPoint],[cnvcCNEndFactPoint]"
                                + ",[cncIOFlight],[cnvcParentId],[cniItemNo],[cnvcBeginTimeSpace],[cnvcEndTimeSpace],[cnvcBeginRefencePoint],[cnvcThreeCode]"
                                + ",[cnvcEndRefencePoint],[cnvcFlightType],[cniView],[cnvcBeginFactPoint],[cnvcEndFactPoint],[cncIsLeaf] from tbStandardItem where " + strCondition + " order by cniStandardNo desc";
                }

                dataSet = SqlHelper.ExecuteDataSet(this.SqlConn, CommandType.Text, strSQL);
            }
            catch (Exception e)
            {
                throw e;
            }

            return dataSet;
        }
        #endregion

        #region 返回单个标准流程操作(单条记录)
        public DataSet GetStandardItemByID(StandardBM standardBM)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string strSQL = "select * from tbStandardItem where cniStandardNo = @cniStandardNo";
                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("@cniStandardNo",SqlDbType.Int)
                };
                sqlParams[0].Value = standardBM.StandardNo;
                dataSet = SqlHelper.ExecuteDataSet(this.SqlConn, CommandType.Text, strSQL, sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
            return dataSet;
        }
        #endregion

        #region 更新标准流程操作
        public int UpdateStandardItem(StandardBM nStandardItem)
        {
            int iReturn = 0;
            try
            {
                string strSQL = "update tbStandardItem set cnvcThreeCode = @PARAM_ThreeCode,cnvcFourCode = @PARAM_FourCode,cnvcActype = @PARAM_Actype,cniItemNo = @PARAM_ItemNo,cnvcBeginTimeSpace = @PARAM_BeginTimeSpace,cnvcEndTimeSpace = @PARAM_EndTimeSpace," +
                    "cnvcFlightIOType = @PARAM_FlightIOType, cnvcBeginFactPoint = @PARAM_BeginFactPoint , cnvcEndFactPoint = @PARAM_EndFactPoint , cnvcCNCityName = @PARAM_CNCityName , cnvcCNBeginRefencePoint = @PARAM_CNBeginRefencePoint , cnvcCNEndRefencePoint = @PARAM_CNEndRefencePoint ,cnvcCNBeginFactPoint = @PARAM_CNBeginFactPoint, cnvcCNEndFactPoint = @PARAM_CNEndFactPoint," + 
                    "cnvcBeginRefencePoint = @PARAM_BeginRefencePoint,cnvcEndRefencePoint = @PARAM_EndRefencePoint,cnvcFlightType = @PARAM_FlightType,cniView = @PARAM_View,cnvcCompany = @PARAM_Company,cnvcItemName = @PARAM_ItemName where cniStandardNo = @cniStandardNo ";
                
                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("@PARAM_ThreeCode",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FourCode",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_Actype",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_ItemNo",SqlDbType.Int),
                    new SqlParameter("@PARAM_BeginTimeSpace",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_EndTimeSpace",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_BeginRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_EndRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FlightType",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_View",SqlDbType.Int),
                    new SqlParameter("@PARAM_Company",SqlDbType.VarChar),
                    new SqlParameter("@cniStandardNo",SqlDbType.Int),
                    new SqlParameter("@PARAM_ItemName",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FlightIOType",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_BeginFactPoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_EndFactPoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNCityName",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNBeginRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNEndRefencePoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNBeginFactPoint",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_CNEndFactPoint",SqlDbType.VarChar)
                };

                sqlParams[0].Value = nStandardItem.ThreeCode;
                sqlParams[1].Value = nStandardItem.FourCode;
                sqlParams[2].Value = nStandardItem.Actype;
                sqlParams[3].Value = nStandardItem.ItemNo;
                sqlParams[4].Value = nStandardItem.BeginTimeSpace;
                sqlParams[5].Value = nStandardItem.EndTimeSpace;
                sqlParams[6].Value = nStandardItem.BeginRefencePoint;
                sqlParams[7].Value = nStandardItem.EndRefencePoint;
                sqlParams[8].Value = nStandardItem.FlightType;
                sqlParams[9].Value = nStandardItem.IView;
                sqlParams[10].Value = nStandardItem.Company;
                sqlParams[11].Value = nStandardItem.StandardNo;
                sqlParams[12].Value = nStandardItem.ItemName;
                sqlParams[13].Value = nStandardItem.FlightIOType;
                sqlParams[14].Value = nStandardItem.BeginFactPoint;
                sqlParams[15].Value = nStandardItem.EndFactPoint;
                sqlParams[16].Value = nStandardItem.CNCityName;
                sqlParams[17].Value = nStandardItem.CNBeginRefencePoint;
                sqlParams[18].Value = nStandardItem.CNEndRefencePoint;
                sqlParams[19].Value = nStandardItem.CNBeginFactPoint;
                sqlParams[20].Value = nStandardItem.CNEndFactPoint;

                iReturn = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, strSQL, sqlParams);
            }
            catch (Exception e)
            {
                throw e;
            }
            return iReturn;
        }
        #endregion

        #region 判断该流程是否存在
        public DataSet CheckStandardItem(StandardBM standardBM)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string strSQL = "select * from tbStandardItem where cnvcFourCode = @PARAM_FourCode and " + 
                "cnvcItemName = @PARAM_ItemName and cnvcActype = @PARAM_Actpye and cnvcFlightType = @PARAM_FlightType and cnvcCompany = @PARAM_Company";
                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("@PARAM_FourCode", SqlDbType.VarChar),
                    new SqlParameter("@PARAM_ItemName",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_Actpye",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_FlightType",SqlDbType.VarChar),
                    new SqlParameter("@PARAM_Company",SqlDbType.VarChar)
                };

                sqlParams[0].Value = standardBM.FourCode;
                sqlParams[1].Value = standardBM.ItemName;
                sqlParams[2].Value = standardBM.Actype;
                sqlParams[3].Value = standardBM.FlightType;
                sqlParams[4].Value = standardBM.Company;

                dataSet = SqlHelper.ExecuteDataSet(this.SqlConn, CommandType.Text, strSQL, sqlParams);

            }
            catch (Exception e)
            {
                throw e;
            }
            return dataSet;
        }
        #endregion

        #region 批量新增
        public int AddBatchOps(string oAirPort,string oAcType,string nAirPort,string nAcType,string nCityName)
        {
            string strSQL = "";
            string strColumns = "";
            string strCondition = "";
            int iResult = 0;

            try
            {
                if (oAirPort != "" && oAirPort != null)
                {
                    strColumns += ", '" + nAirPort + "', '" + nCityName + "' ";

                    strCondition += " and cnvcFourCode = '" + oAirPort + "'";
                }
                else
                {
                    strColumns += ", [cnvcFourCode] ,[cnvcCNCityName] ";
                    strCondition += "  ";
                }

                if (oAcType != "" && oAcType != null)
                {
                    strColumns += ", '" + nAcType + "'";
                    strCondition += " and cnvcAcType = '" + oAcType + "'";
                }
                else
                {
                    strColumns += ", [cnvcActype] ";
                    strCondition += "  ";
                }

                strSQL = "Insert into tbStandardItem ([cnvcThreeCode], [cnvcFourCode] ,[cnvcCNCityName] , [cnvcAcType] ,[cniItemNo],[cnvcBeginTimeSpace],[cnvcEndTimeSpace],[cnvcBeginRefencePoint],[cnvcEndRefencePoint],[cnvcFlightType],"
                    + "[cniView],[cnvcCompany],[cnvcItemName],[cnvcFlightIOType],[cnvcBeginFactPoint],[cnvcEndFactPoint],[cnvcCNBeginRefencePoint],[cnvcCNEndRefencePoint],[cnvcCNBeginFactPoint],[cnvcCNEndFactPoint],[cncIOFlight],[cnvcParentId],[cncIsLeaf]) "
                    + "select [cnvcThreeCode]" + strColumns + ",[cniItemNo],[cnvcBeginTimeSpace],[cnvcEndTimeSpace],[cnvcBeginRefencePoint],[cnvcEndRefencePoint],[cnvcFlightType],[cniView],[cnvcCompany],[cnvcItemName] "
                    + ",[cnvcFlightIOType],[cnvcBeginFactPoint],[cnvcEndFactPoint],[cnvcCNBeginRefencePoint],[cnvcCNEndRefencePoint],[cnvcCNBeginFactPoint],[cnvcCNEndFactPoint],[cncIOFlight],[cnvcParentId],[cncIsLeaf] from tbStandardItem where 1 = 1 " + strCondition;

                iResult = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction,CommandType.Text, strSQL);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return iResult;
        }
        #endregion

        #region 批量删除操作
        public int DelBatchOps(string nAirPort, string nAcType)
        {
            int iResult = 0;
            string strSQL = "delete from tbStandardItem where 1 = 1 ";
            string strCondition = "";

            try
            {
                if (!nAirPort.Equals("") && nAirPort != null)
                {
                    strCondition += " and cnvcFourCode = '" + nAirPort + "'";
                }
                else
                {
                    strCondition += "  ";
                }

                if (!nAcType.Equals("") && nAcType != null)
                {
                    strCondition += " and cnvcAcType = '" + nAcType + "'";
                }
                else
                {
                    strCondition += "  ";
                }

                strSQL += strCondition;

                iResult = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction,CommandType.Text, strSQL);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return iResult;

        }

        #endregion


        #region
        public DataSet GetStandardCNNameList()
        {
            string strSQL = "select * from tbSmallStandardItem order by iPK";
            return SqlHelper.ExecuteDataSet(this.SqlConn, CommandType.Text, strSQL);
        }
        #endregion

        public DataSet GetOwnerList()
        {
            string strSQL = "select * from tbOwner";
            return SqlHelper.ExecuteDataSet(this.SqlConn, CommandType.Text, strSQL);
        }

        public DataSet GetFlightTypeList()
        {
            string strSQL = "select * from tbFlightType";
            return SqlHelper.ExecuteDataSet(this.SqlConn, CommandType.Text, strSQL);
        }

        public DataSet GetStandardCNNameIOList(string strIOCN)
        {
            string strSQL = "select * from tbSmallStandardItem where cnvcSmallStandardCNName like '%"+strIOCN+"%' order by iPK";
            return SqlHelper.ExecuteDataSet(this.SqlConn, CommandType.Text, strSQL);
        }
    }
}
