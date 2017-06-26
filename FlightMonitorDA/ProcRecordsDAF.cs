using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.AgentServiceBM;

namespace AirSoft.FlightMonitor.AgentServiceDA
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
    public class ProcRecordsDAF
    {
        #region 生成过程记录表
        /// <summary>
        /// 生成过程记录表
        /// </summary>
        /// <returns>返回过程记录表对象</returns>
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
                dataColumn = new DataColumn("cniProcRecordsId", Type.GetType("System.Int32"));
                dataColumn.AutoIncrement = true;
                dataColumn.AutoIncrementSeed = 1;
                dataColumn.AutoIncrementStep = 1;
                dataColumn.Caption = "序号";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcProcName", Type.GetType("System.String"));
                dataColumn.Caption = "过程名";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "操作时间";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcOprationResult", Type.GetType("System.String"));
                dataColumn.Caption = "操作结果";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniOprationCount", Type.GetType("System.Int32"));
                dataColumn.Caption = "调用次数";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniLengthBeforeCompress", Type.GetType("System.Int32"));
                dataColumn.Caption = "压缩之前大小（byte）";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniLengthAfterCompress", Type.GetType("System.Int32"));
                dataColumn.Caption = "压缩之后大小（byte）";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfProcTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "过程执行时间（秒）";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfCompressTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "压缩时间（秒）";
                dataTable.Columns.Add(dataColumn);
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
        
        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="dtProcRecords">过程记录表对象</param>
        /// <param name="procRecordsBM">要添加的记录</param>
        /// <returns>1 成功；-1 失败</returns>
        public int AddRecord(DataTable dtProcRecords, ProcRecordsBM procRecordsBM)
        {
            #region 变量声明
            DataRow dataRow = null;
            int retVal = -1;

            #endregion


            #region 编码实现
            //添加记录
            try
            {
                dataRow = dtProcRecords.NewRow();
                dataRow["cnvcProcName"] = procRecordsBM.ProcName;
                dataRow["cndOprationTime"] = procRecordsBM.OprationTime;
                dataRow["cnvcOprationResult"] = procRecordsBM.OprationResult;
                dataRow["cniOprationCount"] = procRecordsBM.OprationCount;
                dataRow["cniLengthBeforeCompress"] = procRecordsBM.LengthBeforeCompress;
                dataRow["cniLengthAfterCompress"] = procRecordsBM.LengthAfterCompress;
                dataRow["cnfProcTimes"] = procRecordsBM.ProcTimes;
                dataRow["cnfCompressTimes"] = procRecordsBM.CompressTimes;
                dtProcRecords.Rows.Add(dataRow);
                dtProcRecords.AcceptChanges();

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

        #region 添加一条记录
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="dtProcRecords">过程记录表对象</param>
        /// <param name="procRecordsBM">要添加的记录</param>
        /// <param name="SynchronizeLock">同步锁</param>
        /// <returns>1 成功；-1 失败</returns>
        public int AddRecord(DataTable dtProcRecords, ProcRecordsBM procRecordsBM, object SynchronizeLock)
        {
            #region 变量声明
            DataRow dataRow = null;
            int retVal = -1;

            #endregion


            #region 编码实现
            //添加记录
            try
            {
                lock (SynchronizeLock)
                {
                    dataRow = dtProcRecords.NewRow();
                    dataRow["cnvcProcName"] = procRecordsBM.ProcName;
                    dataRow["cndOprationTime"] = procRecordsBM.OprationTime;
                    dataRow["cnvcOprationResult"] = procRecordsBM.OprationResult;
                    dataRow["cniOprationCount"] = procRecordsBM.OprationCount;
                    dataRow["cniLengthBeforeCompress"] = procRecordsBM.LengthBeforeCompress;
                    dataRow["cniLengthAfterCompress"] = procRecordsBM.LengthAfterCompress;
                    dataRow["cnfProcTimes"] = procRecordsBM.ProcTimes;
                    dataRow["cnfCompressTimes"] = procRecordsBM.CompressTimes;
                    dtProcRecords.Rows.Add(dataRow);
                    dtProcRecords.AcceptChanges();
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

        #region 更新 窗体类 本地 记录明细表，采用追加最新记录的办法
        /// <summary>
        /// 更新 窗体类 本地 记录明细表，采用追加最新记录的办法
        /// </summary>
        /// <param name="dtProcRecords">窗体类 本地 dtProcRecords</param>
        /// <param name="dtProcRecords_DAF">AgentServiceDAF.dtProcRecords</param>
        /// <param name="SynchronizeLock">同步锁</param>
        /// <returns></returns>
        public int SynchronizeDatas(DataTable dtProcRecords, DataTable dtProcRecords_DAF, object SynchronizeLock)
        {
            #region 变量声明
            int retVal = -1;
            int iCountsOfProcRecords = 0, iCountsOfProcRecords_DAF = 0;

            #endregion

            #region 编程实现
            try
            {
                lock (SynchronizeLock)
                {
                    iCountsOfProcRecords = dtProcRecords.Rows.Count;
                    iCountsOfProcRecords_DAF = dtProcRecords_DAF.Rows.Count;

                    for (int iIndex = iCountsOfProcRecords; iIndex < iCountsOfProcRecords_DAF; iIndex++)
                    {
                        if ((dtProcRecords_DAF.Rows[iIndex] == null) || (dtProcRecords_DAF.Rows[iIndex].RowState != DataRowState.Unchanged))
                            continue;
                        ProcRecordsBM procRecordsBM = new ProcRecordsBM(dtProcRecords_DAF.Rows[iIndex]);
                        AddRecord(dtProcRecords, procRecordsBM);
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
