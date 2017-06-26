using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.AgentService
{
    class Records
    {
        #region 生成记录表1
        /// <summary>
        /// 生成记录表
        /// </summary>
        /// <returns>返回：ReturnValueSF.Dt 记录表；ReturnValueSF.Result 1 成功，-1 失败； </returns>
        public ReturnValueSF CreateDatatable_1()
        {
            #region 变量声明
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null ;

            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //生成表格
            try
            {
                dataColumn = new DataColumn("cniID", Type.GetType("System.Int32"));
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

                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }

        #endregion

        #region 添加一条记录(表1)
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="dtRecords">记录表</param>
        /// <param name="strProcName">过程名</param>
        /// <param name="dOprationTime">操作时间</param>
        /// <param name="strOprationResult">操作结果</param>
        /// <param name="iOprationCount">调用次数</param>
        /// <returns>ReturnValueSF.Result 1 成功；-1 失败；</returns>
        public ReturnValueSF AddRecord_1(DataTable dtRecords, string strProcName, DateTime dOprationTime, string strOprationResult, int iOprationCount)
        {
            #region 变量声明
            DataRow dataRow = null;
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //添加记录
            try
            {
                dataRow = dtRecords.NewRow();
                dataRow["cnvcProcName"] = strProcName;
                dataRow["cndOprationTime"] = dOprationTime;
                dataRow["cnvcOprationResult"] = strOprationResult;
                dataRow["cniOprationCount"] = iOprationCount;
                dtRecords.Rows.Add(dataRow);

                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }

        #endregion


        #region 生成记录表2
        /// <summary>
        /// 生成记录表
        /// </summary>
        /// <returns>返回：ReturnValueSF.Dt 记录表；ReturnValueSF.Result 1 成功，-1 失败； </returns>
        public ReturnValueSF CreateDatatable_2()
        {
            #region 变量声明
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null;

            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //生成表格
            try
            {
                dataColumn = new DataColumn("cniID", Type.GetType("System.Int32"));
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

                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Dt = null;
                rvSF.Result = -1;

            }


            //返回结果
            return rvSF;

            #endregion
        }

        #endregion

        #region 添加一条记录(表2)
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="dtRecords">记录表2</param>
        /// <param name="strProcName">过程名</param>
        /// <param name="dLastOprationTime">最后操作时间</param>
        /// <param name="strLastOprationResult">最后操作结果</param>
        /// <param name="iOprationCount">调用次数</param>
        /// <param name="dCountStartTime">计数开始时间</param>
        /// <returns>ReturnValueSF.Result 1 成功；-1 失败；</returns>
        public ReturnValueSF AddRecord_2(DataTable dtRecords, string strProcName, DateTime dLastOprationTime, string strLastOprationResult, int iOprationCount, DateTime dCountStartTime)
        {
            #region 变量声明
            DataRow dataRow = null;
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //添加记录
            try
            {
                dataRow = dtRecords.NewRow();
                dataRow["cnvcProcName"] = strProcName;
                dataRow["cndLastOprationTime"] = dLastOprationTime;
                dataRow["cnvcLastOprationResul"] = strLastOprationResult;
                dataRow["cniOprationCount"] = iOprationCount;
                dataRow["cndCountStartTime"] = dCountStartTime;
                dtRecords.Rows.Add(dataRow);

                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }

        #endregion


    }

}
