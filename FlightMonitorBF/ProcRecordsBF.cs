using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.AgentServiceBM;
using System.Data;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.AgentServiceBF
{
    /// <summary>
    /// 过程记录表
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-11-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ProcRecordsBF
    {
        #region 生成过程记录表
        /// <summary>
        /// 生成过程记录表
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF CreateDatatable()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                rvSF.Dt = procRecordsDAF.CreateDatatable();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="dtProcRecords">过程记录表对象</param>
        /// <param name="procRecordsBM">要添加的记录</param>
        /// <returns>ReturnValueSF.Result:1 成功；-1 失败</returns>
        public ReturnValueSF AddRecord(DataTable dtProcRecords, ProcRecordsBM procRecordsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                rvSF.Result = procRecordsDAF.AddRecord(dtProcRecords,procRecordsBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            //
            return rvSF;
        }
        #endregion

        #region 更新 窗体类 本地 记录明细表，采用追加最新记录的办法
        /// <summary>
        /// 更新 窗体类 本地 记录明细表，采用追加最新记录的办法
        /// </summary>
        /// <param name="dtProcRecords">窗体类 本地 dtProcRecords</param>
        /// <param name="dtProcRecords_DAF">AgentServiceDAF.dtProcRecords</param>
        /// <param name="SynchronizeLock">同步锁</param>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas(DataTable dtProcRecords, DataTable dtProcRecords_DAF, object SynchronizeLock)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                rvSF.Result = procRecordsDAF.SynchronizeDatas(dtProcRecords, dtProcRecords_DAF, SynchronizeLock);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            //
            return rvSF;
        }
        #endregion


    }
}
