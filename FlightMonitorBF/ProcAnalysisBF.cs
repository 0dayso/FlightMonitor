using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.FlightMonitor.AgentServiceDA;

namespace AirSoft.FlightMonitor.AgentServiceBF
{
    /// <summary>
    /// 过程分析表
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-11-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ProcAnalysisBF
    {
        #region 生成过程分析表
        /// <summary>
        /// 生成过程分析表
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF CreateDatatable()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                rvSF.Dt = procAnalysisDAF.CreateDatatable();
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

        #region 更新记录
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="dtProcAnalysis">过程分析表对象</param>
        /// <param name="procAnalysisBM">要修改的结果</param>
        /// <returns>ReturnValueSF.Result:1 成功；-1 失败</returns>
        public ReturnValueSF UpdateRecord(DataTable dtProcAnalysis, ProcAnalysisBM procAnalysisBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                rvSF.Result = procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM);
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

        #region 更新 本地 记录统计表，采用逐行更新的办法
        /// <summary>
        /// 更新 本地 记录统计表，采用逐行更新的办法
        /// </summary>
        /// <param name="dtProcAnalysis">窗体类 本地 dtProcAnalysis</param>
        /// <param name="dtProcAnalysis_DAF">AgentServiceDAF.dtProcAnalysis</param>
        /// <param name="SynchronizeLock">同步锁</param>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas(DataTable dtProcAnalysis, DataTable dtProcAnalysis_DAF, object SynchronizeLock)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                rvSF.Result = procAnalysisDAF.SynchronizeDatas(dtProcAnalysis, dtProcAnalysis_DAF, SynchronizeLock);
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
