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
    /// 保障日记主题信息访问
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-05-26
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeRecordCaptionDAF
    {
        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="guaranteeRecordCaptionBM">保障日记主题信息对象</param>
        /// <returns></returns>
        public int Add(GuaranteeRecordCaptionBM guaranteeRecordCaptionBM)
        {
            int retVal = -1; //定义返回值

            GuaranteeRecordCaptionDA guaranteeRecordCaptionDA = new GuaranteeRecordCaptionDA();

            try
            {
                guaranteeRecordCaptionDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //打开数据库连接
                retVal =  guaranteeRecordCaptionDA.Add(guaranteeRecordCaptionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordCaptionDA.ConnClose();
            }

            return retVal;
        }
        #endregion 增加一条数据

        #region 获取所有信息
        /// <summary>
        /// 获取所有信息
        /// </summary>
        public DataTable GetDataList()
        {
            DataTable dataTableResult = new DataTable(); //定义返回值

            GuaranteeRecordCaptionDA guaranteeRecordCaptionDA = new GuaranteeRecordCaptionDA();

            try
            {
                guaranteeRecordCaptionDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //打开数据库连接
                dataTableResult = guaranteeRecordCaptionDA.GetDataList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordCaptionDA.ConnClose();
            }

            return dataTableResult;
        }
        #endregion 获取所有信息

        #region 获取所有信息(根据航班、航站)
        /// <summary>
        /// 获取所有信息(根据航班、航站)
        /// </summary>
        /// <param name="cncStation">航站</param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <returns>获取所有信息(根据航班、航站)</returns>
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            DataTable dataTableResult = new DataTable(); //定义返回值

            GuaranteeRecordCaptionDA guaranteeRecordCaptionDA = new GuaranteeRecordCaptionDA();

            try
            {
                guaranteeRecordCaptionDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //打开数据库连接
                dataTableResult = guaranteeRecordCaptionDA.GetDataList(cncStation, cncDATOP, cnvcFLTID, cniLegNO, cnvcAC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordCaptionDA.ConnClose();
            }

            return dataTableResult;
        }
        #endregion 获取所有信息(根据航班、航站)

    }
}
