using System;
using System.Collections;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 标准流程操作外观类
    /// </summary>
    /// author : yanxian
    /// date : 2013-11-13
    public class StandardItemDAF
    {
        private StandardItemDA standardItemDA = new StandardItemDA();

        #region 新增标准流程列表操作
        public int AddStandardItem(StandardBM standardBM)
        {
            try
            {
                standardItemDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                return standardItemDA.AddStandardItem(standardBM);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemDA.ConnClose();
            }
            
        }
        #endregion

        #region 删除标准流程操作
        public int DelStandardItem(int iStandardItem)
        {
            try
            {
                standardItemDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                return standardItemDA.DelStandardItem(iStandardItem);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemDA.ConnClose();
            }
            
        }
        #endregion

        #region 返回标准流程列表操作
        public DataTable GetStandardItemList(StandardBM conditionStandardBM)
        {
            DataSet dataSet = new DataSet();
            DataTable StandardItemTable = new DataTable();
            try
            {
                standardItemDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                dataSet = standardItemDA.GetStandardItemList(conditionStandardBM);
                if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
                {
                    StandardItemTable = null;
                }
                else
                {
                    StandardItemTable = dataSet.Tables[0];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemDA.ConnClose();
            }

            return StandardItemTable;
        }
        #endregion

        #region 更新标准流程操作
        public int UpdateStandardItem(StandardBM nStandardItem)
        {
            try
            {
                standardItemDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                return standardItemDA.UpdateStandardItem(nStandardItem);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemDA.ConnClose();
            }

        }
        #endregion

        #region 返回单个标准流程操作(单条记录)
        public StandardBM GetStandardItemByID(StandardBM standardBM)
        {
            StandardBM retstandardBM = new StandardBM();
            DataSet dataSet = new DataSet();
            try
            {
                standardItemDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                dataSet = standardItemDA.GetStandardItemByID(standardBM);
                if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
                {
                    retstandardBM = null;
                }
                else
                {
                    standardBM.StandardNo = int.Parse(dataSet.Tables[0].Rows[0]["cniStandardNo"].ToString());
                    standardBM.ThreeCode = dataSet.Tables[0].Rows[0]["cnvcThreeCode"].ToString();
                    standardBM.FourCode = dataSet.Tables[0].Rows[0]["cnvcFourCode"].ToString();
                    standardBM.Actype = dataSet.Tables[0].Rows[0]["cnvcActype"].ToString();
                    standardBM.ItemNo = int.Parse(dataSet.Tables[0].Rows[0]["cniItemNo"].ToString());
                    standardBM.BeginTimeSpace = dataSet.Tables[0].Rows[0]["cnvcBeginTimeSpace"].ToString();
                    standardBM.EndTimeSpace = dataSet.Tables[0].Rows[0]["cnvcEndTimeSpace"].ToString();
                    standardBM.BeginRefencePoint = dataSet.Tables[0].Rows[0]["cnvcBeginRefencePoint"].ToString();
                    standardBM.EndRefencePoint = dataSet.Tables[0].Rows[0]["cnvcEndRefencePoint"].ToString();
                    standardBM.FlightType = dataSet.Tables[0].Rows[0]["cnvcFlightType"].ToString();
                    standardBM.IView = int.Parse(dataSet.Tables[0].Rows[0]["cniView"].ToString());
                    standardBM.Company = dataSet.Tables[0].Rows[0]["cnvcCompany"].ToString();
                    standardBM.FlightIOType = dataSet.Tables[0].Rows[0]["cnvcFlightIOType"].ToString();
                    standardBM.BeginFactPoint = dataSet.Tables[0].Rows[0]["cnvcBeginFactPoint"].ToString();
                    standardBM.EndFactPoint = dataSet.Tables[0].Rows[0]["cnvcEndFactPoint"].ToString();
                    standardBM.CNCityName = dataSet.Tables[0].Rows[0]["cnvcCNCityName"].ToString();
                    standardBM.CNBeginRefencePoint = dataSet.Tables[0].Rows[0]["cnvcCNBeginRefencePoint"].ToString();
                    standardBM.CNEndRefencePoint = dataSet.Tables[0].Rows[0]["cnvcCNEndRefencePoint"].ToString();
                    standardBM.CNBeginFactPoint = dataSet.Tables[0].Rows[0]["cnvcCNBeginFactPoint"].ToString();
                    standardBM.CNEndFactPoint = dataSet.Tables[0].Rows[0]["cnvcCNEndFactPoint"].ToString();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemDA.ConnClose();
            }
            return retstandardBM;
        }
        #endregion

        #region 判断该流程是否存在
        public bool CheckStandardItem(StandardBM standardBM)
        {
            bool bReturn = false;
            DataSet dataSet = new DataSet();
            try
            {
                standardItemDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                dataSet = standardItemDA.CheckStandardItem(standardBM);
                if (dataSet != null && dataSet.Tables[0].Rows.Count != 0)
                    bReturn = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                standardItemDA.ConnClose();
            }
            return bReturn;
        }
        #endregion

        #region 批量操作
        public int BatchStandardItemOps(string oAirPort, string oAcType, string nAirPort, string nAcType, string nCityName)
        {
            int iResult = 0;
            int aResult = 0;
            int uResult = 0;
            try
            {
                standardItemDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_TEST_FlightMonitor, 1));
                standardItemDA.Transaction = standardItemDA.SqlConn.BeginTransaction();
                aResult = standardItemDA.DelBatchOps(nAirPort, nAcType);
                uResult = standardItemDA.AddBatchOps(oAirPort, oAcType, nAirPort, nAcType, nCityName);
                standardItemDA.Transaction.Commit();
                if (aResult !=0 && uResult != 0)
                {
                    iResult = 1;
                }
            }
            catch (Exception e)
            {
                standardItemDA.Transaction.Rollback();
                iResult = -1;
                throw e;
            }
            finally
            {
                standardItemDA.ConnClose();
            }
            return iResult;
        }
        #endregion

        #region
        public DataSet GetStandardCNNameList()
        {
            return standardItemDA.GetStandardCNNameList();
        }
        #endregion

        public DataSet GetOwnerList()
        {
            return standardItemDA.GetOwnerList();
        }

        public DataSet GetFlightTypeList()
        {
            return standardItemDA.GetFlightTypeList();
        }

        public DataSet GetStandardCNNameIOList(string strIOCN)
        {
            return standardItemDA.GetStandardCNNameIOList(strIOCN);
        }
    }
}
