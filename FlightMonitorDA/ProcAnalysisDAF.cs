using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.AgentServiceBM;

namespace AirSoft.FlightMonitor.AgentServiceDA
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
    public class ProcAnalysisDAF
    {
        #region 生成过程分析表
        /// <summary>
        /// 生成过程分析表
        /// </summary>
        /// <returns>返回过程分析表对象</returns>
        public DataTable CreateDatatable()
        {
            #region 变量声明
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null;

            #endregion


            #region 编码实现
            //生成表格
            try
            {
                dataColumn = new DataColumn("cniProcAnalysisId", Type.GetType("System.Int32"));
                dataColumn.AutoIncrement = true;
                dataColumn.AutoIncrementSeed = 1;
                dataColumn.AutoIncrementStep = 1;
                dataColumn.Caption = "序号";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcProcName", Type.GetType("System.String"));
                dataColumn.Caption = "过程名";
                dataColumn.Unique = true;
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndLastOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "最后操作时间";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcLastOprationResult", Type.GetType("System.String"));
                dataColumn.Caption = "最后操作结果";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniOprationCount", Type.GetType("System.Int32"));
                dataColumn.Caption = "调用次数";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndCountStartTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "计数开始时间";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnlTotalLengthBeforeCompress", Type.GetType("System.Int64"));
                dataColumn.Caption = "压缩之前大小总数（byte）";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnlTotalLengthAfterCompress", Type.GetType("System.Int64"));
                dataColumn.Caption = "压缩之后大小总数（byte）";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfTotalProcTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "过程执行时间总数（秒）";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfTotalCompressTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "压缩时间总数（秒）";
                dataTable.Columns.Add(dataColumn);

                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["cnvcProcName"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //返回结果
            return dataTable;

            #endregion
        }
        #endregion

        #region 更新记录
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="dtProcAnalysis">过程分析表对象</param>
        /// <param name="procAnalysisBM">要修改的结果</param>
        /// <returns>1 成功；-1 失败</returns>
        public int UpdateRecord(DataTable dtProcAnalysis, ProcAnalysisBM procAnalysisBM)
        {
            #region 变量声明
            int retVal = -1;

            #endregion


            #region 编码实现
            //添加记录
            try
            {
                dtProcAnalysis.BeginLoadData();
                object[] arrObject = new object[] {null,procAnalysisBM.ProcName,procAnalysisBM.LastOprationTime,
                    procAnalysisBM.LastOprationResult,procAnalysisBM.OprationCount,procAnalysisBM.CountStartTime,
                    procAnalysisBM.TotalLengthBeforeCompress,procAnalysisBM.TotalLengthAfterCompress,
                    procAnalysisBM.TotalProcTimes,procAnalysisBM.TotalCompressTime};
                dtProcAnalysis.LoadDataRow(arrObject, true);
                dtProcAnalysis.EndLoadData();

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //返回结果
            return retVal;

            #endregion
        }
        #endregion

        #region 加锁操作

        #region 更新记录
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="dtProcAnalysis">过程分析表对象</param>
        /// <param name="procAnalysisBM">要修改的结果</param>
        /// <param name="SynchronizeLock">同步锁</param>
        /// <returns>1 成功；-1 失败</returns>
        public int UpdateRecord(DataTable dtProcAnalysis, ProcAnalysisBM procAnalysisBM, object SynchronizeLock)
        {
            #region 变量声明
            int retVal = -1;

            #endregion


            #region 编码实现
            //添加记录
            try
            {
                lock (SynchronizeLock)
                {
                    dtProcAnalysis.BeginLoadData();
                    object[] arrObject = new object[] {null,procAnalysisBM.ProcName,procAnalysisBM.LastOprationTime,
                    procAnalysisBM.LastOprationResult,procAnalysisBM.OprationCount,procAnalysisBM.CountStartTime,
                    procAnalysisBM.TotalLengthBeforeCompress,procAnalysisBM.TotalLengthAfterCompress,
                    procAnalysisBM.TotalProcTimes,procAnalysisBM.TotalCompressTime};
                    dtProcAnalysis.LoadDataRow(arrObject, true);
                    dtProcAnalysis.EndLoadData();
                }

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //返回结果
            return retVal;

            #endregion
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
        public int SynchronizeDatas(DataTable dtProcAnalysis, DataTable dtProcAnalysis_DAF, object SynchronizeLock)
        {
            #region 变量声明
            int retVal = -1;
            int iCountsOfProcAnalysis_DAF = 0;

            #endregion


            #region 编程实现
            try
            {
                lock (SynchronizeLock)
                {
                    iCountsOfProcAnalysis_DAF = dtProcAnalysis_DAF.Rows.Count;
                    for (int iIndex = 0; iIndex < iCountsOfProcAnalysis_DAF; iIndex++)
                    {
                        ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(dtProcAnalysis_DAF.Rows[iIndex]);
                        UpdateRecord(dtProcAnalysis, procAnalysisBM);
                    }
                }

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //返回结果
            return retVal;

            #endregion

        }
        #endregion

        #endregion

    }
}
