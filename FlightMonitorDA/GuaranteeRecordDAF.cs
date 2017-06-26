using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 保障日记信息访问
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-05-26
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeRecordDAF
    {
        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="guaranteeRecordBM">保障日记信息对象</param>
        /// <returns></returns>
        public int Add(GuaranteeRecordBM guaranteeRecordBM)
        {
            int retVal = -1; //定义返回值

            GuaranteeRecordDA guaranteeRecordDA = new GuaranteeRecordDA();

            try
            {
                guaranteeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //打开数据库连接
                retVal = guaranteeRecordDA.Add(guaranteeRecordBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordDA.ConnClose();
            }

            return retVal;
        }
        #endregion 增加一条数据

        #region 获取所有信息(根据航班、航站、主题)
        /// <summary>
        /// 获取所有信息(根据航班、航站、主题)
        /// </summary>
        /// <param name="cncStation"></param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <param name="cniGuaranteeRecordCaptionID"></param>
        /// <returns></returns>
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC, int cniGuaranteeRecordCaptionID)
        {
            DataTable dataTableResult = new DataTable(); //定义返回值

            GuaranteeRecordDA guaranteeRecordDA = new GuaranteeRecordDA();

            try
            {
                guaranteeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //打开数据库连接
                dataTableResult = guaranteeRecordDA.GetDataList( cncStation,  cncDATOP,  cnvcFLTID,  cniLegNO,  cnvcAC,  cniGuaranteeRecordCaptionID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordDA.ConnClose();
            }

            return dataTableResult;
        }
        #endregion 获取所有信息(根据航班、航站、主题)

        #region 获取所有信息(根据航班、航站)
        /// <summary>
        /// 获取所有信息(根据航班、航站)
        /// </summary>
        /// <param name="cncStation"></param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <param name="cniGuaranteeRecordCaptionID"></param>
        /// <returns></returns>
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            DataTable dataTableResult = new DataTable(); //定义返回值

            GuaranteeRecordDA guaranteeRecordDA = new GuaranteeRecordDA();

            try
            {
                guaranteeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //打开数据库连接
                dataTableResult = guaranteeRecordDA.GetDataList(cncStation, cncDATOP, cnvcFLTID, cniLegNO, cnvcAC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordDA.ConnClose();
            }

            return dataTableResult;
        }
        #endregion 获取所有信息(根据航班、航站)
    }
}
