using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.Public.SystemFramework;
using CompressDataSet.Common;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 数据访问代理服务
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-11-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class TaskBookServiceDAF : MarshalByRefObject
    {
        #region 远程对象
        static public TaskBookServiceDAF objRemotingObject = null;
        #endregion

        #region 内存数据表，如果数据表在多线程中有修改操作，需要增加同步锁【如 GetDataBySQL 过程】
        static public DataTable tbFltReport = null;  //航前任务书，如果数据表在多线程中有修改操作，需要增加同步锁【如  过程】
        static public DataTable tbBasicCrewInfo_Profile = null; //人员基础信息，如果数据表在多线程中有修改操作，需要增加同步锁【如  过程】

        static public DataTable tbLegs = null;  //航班动态，如果数据表在多线程中有修改操作，需要增加同步锁【如  过程】


        #endregion

        #region 操作繁忙标志
        static public bool blnBusy_tbFltReport = false;
        static public bool blnBusy_tbBasicCrewInfo_Profile = false;

        #endregion


        #region 代理的过程

        #region 进港航班的航前任务书的解析信息（人员、联系方式、下个航班信息（是否连飞、是否更换飞机）），byte[] GetVoyageReportDataBySingleFlight_In(...)
        /// <summary>
        /// 进港航班的航前任务书的解析信息（人员、联系方式、下个航班信息（是否连飞、是否更换飞机））
        /// </summary>
        /// <param name="DATOP"></param>
        /// <param name="FLTIDS"></param>
        /// <param name="AC"></param>
        /// <param name="ROUTES"></param>
        /// <param name="STD">计划起飞时间，日期时间分隔符为单空格</param>
        /// <param name="STA">计划到达时间，日期时间分隔符为单空格</param>
        /// <returns></returns>
        public byte[] GetVoyageReportDataBySingleFlight_In(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";


            DataTable dataTableResult = new DataTable();   //返回结果（表格，进港航班）
            string strFltReportId = "";     //任务书 ID，运行网任务书链接使用

            #region 记录使用
            //过程名
            string strProcName = ""; 
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;
            #endregion 记录使用

            #endregion 变量声明


            #region 编码实现
            #region 生成表格
            DataColumn dataColumn = null;

            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
            dataColumn.AutoIncrement = true;
            dataColumn.AutoIncrementSeed = 1;
            dataColumn.AutoIncrementStep = 1;
            dataColumn.Caption = "序号";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "位置";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "姓名";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "级别";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "工作证号";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "搭乘航段";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "联系电话";
            dataTableResult.Columns.Add(dataColumn);
            
            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "执行航班（下段）";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "备注";
            dataTableResult.Columns.Add(dataColumn);
            #endregion 生成表格


            #region 提取数据（根据条件提取航前任务书数据）
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbFltReport)
                {
                    //记录使用
                    strProcName = "(内)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "(STD like '%" + DATOP + "%') and " +
                        "(FLTIDS like '%" + FLTIDS + "%') and " +
                        "(AC like '%" + AC + "%') and " +
                        "(ROUTES like '%" + ROUTES + "%') " ;

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    VoyageReportDAF voyageReportDAF = new VoyageReportDAF();
                    dataTable = voyageReportDAF.GetVoyageReportDataBySingleFlight(DATOP, FLTIDS, AC, ROUTES);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion 提取数据（根据条件提取航前任务书数据）

            #region 记录 cniFltReportId
            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                strFltReportId = dataTable.Rows[0]["cniFltReportId"].ToString();
            }
            #endregion 记录 cniFltReportId

            #region 航前任务书中的 MemberInfo 放入 返回结果表，在执行航班加机组中过滤掉非搭乘此航段的人员信息


            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                VoyageReportBM voyageReportBM = new VoyageReportBM(dataTable.Rows[0]);
                if (voyageReportBM.Success)
                {
                    voyageReportBM.AnalyseVoyageReport_MemberInfo();
                    if (voyageReportBM.MemberInfo != null)
                    {
                        foreach (DataRow dataRow in voyageReportBM.MemberInfo.Rows)
                        {
                            //if ((dataRow["cnvcDepArrStn"].ToString().Trim() == "") || (dataRow["cnvcDepArrStn"].ToString().Trim() == ROUTES))
                            if ((dataRow["cnvcDepArrStn"].ToString().Trim() == "") || (dataRow["cnvcDepArrStn"].ToString().Trim().IndexOf( ROUTES) >= 0))
                                {
                                DataRow dataRowdataTableResult = dataTableResult.NewRow();

                                dataRowdataTableResult["cnvcPosition"] = dataRow["cnvcPosition"].ToString();
                                dataRowdataTableResult["cnvcName"] = dataRow["cnvcName"].ToString();
                                dataRowdataTableResult["cnvcLevel"] = dataRow["cnvcLevel"].ToString();
                                dataRowdataTableResult["cnvcSID"] = dataRow["cnvcSID"].ToString();
                                dataRowdataTableResult["cnvcDepArrStn"] = dataRow["cnvcDepArrStn"].ToString();

                                dataTableResult.Rows.Add(dataRowdataTableResult);
                            }
                        }
                    }
                }
            }

            #endregion 航前任务书中的 MemberInfo 放入 返回结果表，在执行航班加机组中过滤掉非搭乘此航段的人员信息

            #region 添加人员联系信息
            foreach (DataRow dataRowIndex in dataTableResult.Rows)
            {
                //提取人员联系信息
                DataRow[] dataRowsBasicCrewInfo_Profile = tbBasicCrewInfo_Profile.Select("STAFFID = '" + dataRowIndex["cnvcSID"].ToString().Trim() + "'");
                if (dataRowsBasicCrewInfo_Profile.Length > 0)
                    dataRowIndex["cnvcMOBILE"] = dataRowsBasicCrewInfo_Profile[0]["MOBILE"].ToString();
            }

            #endregion 添加人员联系信息

            #region 各个成员的下段执行航班信息
            foreach (DataRow dataRowIndex in dataTableResult.Rows)   //遍历每个成员，成员数据行 dataRowIndex
            {
                try
                {
                    DataTable dataTableSelectMember = null;     //保存 某个成员 在 内存表 中 的 任务书数据（每个任务书可能含有多段航段信息）

                    strSQL = "(Captain_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SKIPPER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(FIRST_VICE1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(TELER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(PILOT_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(CHIEF_STEWARD_CAPTAIN_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_INSTRUCTOR_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SAFER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CAP1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARDS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +  //added by LinYong in 20150325
                        "(PILOT_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') ";

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTableSelectMember = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);

                    if ((dataTableSelectMember != null) && (dataTableSelectMember.Rows.Count > 0))
                    {

                        DataTable dataTableSelectMember_SegmentInfo = new DataTable(); //保存 某个人员 在 内存任务书表 中 的 所有执行航段信息

                        foreach (DataRow dataRowdataTableSelectMember in dataTableSelectMember.Rows)
                        {
                            VoyageReportBM voyageReportBMdataTableSelectMember = new VoyageReportBM(dataRowdataTableSelectMember);
                            if (voyageReportBMdataTableSelectMember.Success)
                            {
                                string strMemo = "";    //added by LinYong in 20150325
                                string strDepArrStn = "";   //added by LinYong in 20150325

                                if (voyageReportBMdataTableSelectMember.pilot_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)    //added by LinYong in 20150325        
                                    strMemo = "加机组";
                                if (voyageReportBMdataTableSelectMember.steward_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)  //added by LinYong in 20150325
                                    strMemo = "加机组";

                                if (strMemo == "加机组")    //added by LinYong in 20150325   
                                {
                                    voyageReportBMdataTableSelectMember.AnalyseVoyageReport_MemberInfo();
                                    if (voyageReportBMdataTableSelectMember.MemberInfo != null)
                                    {
                                        DataRow[] dataRowsvoyageReportBMdataTableSelectMember_MemberInfo = voyageReportBMdataTableSelectMember.MemberInfo.Select("cnvcSID = '" + dataRowIndex["cnvcSID"].ToString() + "'");
                                        if (dataRowsvoyageReportBMdataTableSelectMember_MemberInfo.Length > 0)
                                        {
                                            strDepArrStn = dataRowsvoyageReportBMdataTableSelectMember_MemberInfo[0]["cnvcDepArrStn"].ToString();
                                        }
                                    }
                                }

                                voyageReportBMdataTableSelectMember.AnalyseVoyageReport_SegmentInfo();
                                if (voyageReportBMdataTableSelectMember.SegmentInfo != null)
                                {
                                    if (dataTableSelectMember_SegmentInfo.Columns.Count <= 0)
                                    {
                                        dataTableSelectMember_SegmentInfo = voyageReportBMdataTableSelectMember.SegmentInfo.Clone();
                                    }

                                    foreach (DataRow dataRowvoyageReportBMdataTableSelectMember_SegmentInfo in voyageReportBMdataTableSelectMember.SegmentInfo.Rows)
                                    {
                                        if (strMemo == "加机组")    //added by LinYong in 20150325 
                                        {
                                            if (strDepArrStn.IndexOf(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcROUTE"].ToString().Trim()) < 0)
                                                continue;
                                        }

                                        dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcMemo"] = strMemo;   //added by LinYong in 20150325
                                        dataTableSelectMember_SegmentInfo.ImportRow(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo);
                                    }
                                }
                                else
                                {
                                    throw new Exception("各个成员的下段执行航班信息：生成 VoyageReportBM.SegmentInfo 时失败！");
                                }
                            }
                            else
                                throw new Exception("各个成员的下段执行航班信息：生成 VoyageReportBM 时失败！");
                        }

                        //计算下个航班
                        if (dataTableSelectMember_SegmentInfo != null) //在现在这种算法下，不会出现 dataTableSelectMember_SegmentInfo == null 的情况
                        {
                            //默认 针对同个航班 从航站保障系统中传送过来的计划起飞时间STD 和 航前任务书数据表中的计划起飞时间 是一样的
                            DataRow[] dataRowsdataTableSelectMember_SegmentInfo = dataTableSelectMember_SegmentInfo.Select(("cnvcSTD > '" + STD.Replace(" ", "  ") + "'"), "cnvcSTD");
                            if (dataRowsdataTableSelectMember_SegmentInfo.Length > 0)
                            {
                                //是否 加机组 航班  added by LinYong in 20150324
                                if (dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcMemo"].ToString() == "加机组")
                                    dataRowIndex["cnvcExecFlightInfo"] = "加机组：";

                                //航班基本信息
                                dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() +    //modified by LinYong in 20150325
                                    "起飞时间：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " ") + " ； " +
                                    "飞机号：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString() + " ； " +
                                    "航班号：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcFLTID"].ToString() + " ； " +
                                    "航段：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcROUTE"].ToString() + " ； " ;

                                //连飞 判断
                                DateTime dtCurrentFlightSTA = Convert.ToDateTime(STA);
                                DateTime dtNextFlightSTD = Convert.ToDateTime(dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " "));
                                if (dtNextFlightSTD <= dtCurrentFlightSTA.AddHours(2))
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "连飞：是 ； ";
                                else
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "连飞：否 ； ";

                                //更换飞机 判断
                                if (AC == dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString())
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "更换飞机：否 ； ";
                                else
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "更换飞机：是 ； ";

                            }

                        }
 
                    }
                    else if (dataTableSelectMember.Rows.Count <= 0)
                    {
                        if (dataRowIndex["cnvcPosition"].ToString().IndexOf("加机组") < 0)
                        {
                            throw new Exception("各个成员的下段执行航班信息：从内存表中没有提取到此成员的任务书信息（应该有，需查找原因）！");
                        }
                    }
                    else if (dataTableSelectMember == null)
                    {
                        throw new Exception("各个成员的下段执行航班信息：从内存表中提取此成员的任务书信息时出现异常！");
                    }

                }
                catch (Exception ex)
                {
                    dataRowIndex["cnvcMemo"] = dataRowIndex["cnvcMemo"] + "【" + ex.Message + "】";
                }

            }
            #endregion 各个成员的下段执行航班信息

            #region 增加 统计行

            string strSIDs = "";
            int multipleBusiness = 0;   //一个人在多个岗位
            int countTatal = 0;

            foreach (DataRow dataRowdataTableResult in dataTableResult.Rows)   //遍历每个成员，成员数据行 dataRowIndex
            {
                if (strSIDs.IndexOf(dataRowdataTableResult["cnvcSID"].ToString()) < 0)
                {
                    strSIDs = strSIDs + dataRowdataTableResult["cnvcSID"].ToString() + "；";
                    countTatal++;
                }
                else
                    multipleBusiness++;
            }

            DataRow dataRowdataTableResult_sum = dataTableResult.NewRow();
            dataRowdataTableResult_sum["cnvcPosition"] = "合计";
            dataRowdataTableResult_sum["cnvcExecFlightInfo"] = "总人数：" + countTatal.ToString() + 
                "，其中兼职人数：" + multipleBusiness.ToString() 
                + "。【任务书ID：" + strFltReportId + "】";
            dataTableResult.Rows.InsertAt(dataRowdataTableResult_sum, 0);

            #endregion 增加 统计行


            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTableResult != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTableResult, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion 把数据表压缩和系列化成二进制流


            //返回结果
            return bResult;


            #endregion 编码实现

        }
        #endregion 进港航班的航前任务书的解析信息（人员、联系方式、下个航班信息（是否连飞、是否更换飞机）），byte[] GetVoyageReportDataBySingleFlight_In(...)

        #region 出港航班的航前任务书的解析信息（人员、联系方式、上个航班信息（是否连飞、是否更换飞机）），byte[] GetVoyageReportDataBySingleFlight_Out(...)
        /// <summary>
        /// 出港航班的航前任务书的解析信息（人员、联系方式、上个航班信息（是否连飞、是否更换飞机））
        /// </summary>
        /// <param name="DATOP"></param>
        /// <param name="FLTIDS"></param>
        /// <param name="AC"></param>
        /// <param name="ROUTES"></param>
        /// <param name="STD">计划起飞时间，日期时间分隔符为单空格</param>
        /// <param name="STA">计划到达时间，日期时间分隔符为单空格</param>
        /// <returns></returns>
        public byte[] GetVoyageReportDataBySingleFlight_Out(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";


            DataTable dataTableResult = new DataTable();   //返回结果（表格，进港航班）
            string strFltReportId = "";     //任务书 ID，运行网任务书链接使用

            #region 记录使用
            //过程名
            string strProcName = "";
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;
            #endregion 记录使用

            #endregion 变量声明


            #region 编码实现

            #region 生成表格
            DataColumn dataColumn = null;

            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
            dataColumn.AutoIncrement = true;
            dataColumn.AutoIncrementSeed = 1;
            dataColumn.AutoIncrementStep = 1;
            dataColumn.Caption = "序号";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "位置";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "姓名";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "级别";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "工作证号";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "搭乘航段";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "联系电话";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "执行航班（上段）";
            dataTableResult.Columns.Add(dataColumn);

            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
            dataColumn.DefaultValue = "";
            dataColumn.Caption = "备注";
            dataTableResult.Columns.Add(dataColumn);
            #endregion 生成表格


            #region 提取数据（根据条件提取航前任务书数据）
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbFltReport)
                {
                    //记录使用
                    strProcName = "(内)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "(STD like '%" + DATOP + "%') and " +
                        "(FLTIDS like '%" + FLTIDS + "%') and " +
                        "(AC like '%" + AC + "%') and " +
                        "(ROUTES like '%" + ROUTES + "%') ";

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetVoyageReportDataBySingleFlight[VoyageReportDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    VoyageReportDAF voyageReportDAF = new VoyageReportDAF();
                    dataTable = voyageReportDAF.GetVoyageReportDataBySingleFlight(DATOP, FLTIDS, AC, ROUTES);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion 提取数据（根据条件提取航前任务书数据）

            #region 记录 cniFltReportId
            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                strFltReportId = dataTable.Rows[0]["cniFltReportId"].ToString();
            }
            #endregion 记录 cniFltReportId


            #region 航前任务书中的 MemberInfo 放入 返回结果表，在执行航班加机组中过滤掉非搭乘此航段的人员信息


            if ((dataTable != null) && (dataTable.Rows.Count == 1))
            {
                VoyageReportBM voyageReportBM = new VoyageReportBM(dataTable.Rows[0]);
                if (voyageReportBM.Success)
                {
                    voyageReportBM.AnalyseVoyageReport_MemberInfo();
                    if (voyageReportBM.MemberInfo != null)
                    {
                        foreach (DataRow dataRow in voyageReportBM.MemberInfo.Rows)
                        {
                            if ((dataRow["cnvcDepArrStn"].ToString().Trim() == "") || (dataRow["cnvcDepArrStn"].ToString().Trim().IndexOf( ROUTES) >= 0))
                            {
                                DataRow dataRowdataTableResult = dataTableResult.NewRow();

                                dataRowdataTableResult["cnvcPosition"] = dataRow["cnvcPosition"].ToString();
                                dataRowdataTableResult["cnvcName"] = dataRow["cnvcName"].ToString();
                                dataRowdataTableResult["cnvcLevel"] = dataRow["cnvcLevel"].ToString();
                                dataRowdataTableResult["cnvcSID"] = dataRow["cnvcSID"].ToString();
                                dataRowdataTableResult["cnvcDepArrStn"] = dataRow["cnvcDepArrStn"].ToString();

                                dataTableResult.Rows.Add(dataRowdataTableResult);
                            }
                        }
                    }
                }
            }

            #endregion 航前任务书中的 MemberInfo 放入 返回结果表，在执行航班加机组中过滤掉非搭乘此航段的人员信息

            #region 添加人员联系信息
            foreach (DataRow dataRowIndex in dataTableResult.Rows)
            {
                //提取人员联系信息
                DataRow[] dataRowsBasicCrewInfo_Profile = tbBasicCrewInfo_Profile.Select("STAFFID = '" + dataRowIndex["cnvcSID"].ToString().Trim() + "'");
                if (dataRowsBasicCrewInfo_Profile.Length > 0)
                    dataRowIndex["cnvcMOBILE"] = dataRowsBasicCrewInfo_Profile[0]["MOBILE"].ToString();
            }

            #endregion 添加人员联系信息

            #region 各个成员的上段执行航班信息
            foreach (DataRow dataRowIndex in dataTableResult.Rows)   //遍历每个成员，成员数据行 dataRowIndex
            {
                try
                {
                    DataTable dataTableSelectMember = null;     //保存 某个成员 在 内存表 中 的 任务书数据（每个任务书可能含有多段航段信息）

                    strSQL = "(Captain_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SKIPPER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(FIRST_VICE1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(TELER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(PILOT_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(CHIEF_STEWARD_CAPTAIN_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CHECKER_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_INSTRUCTOR_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(SAFER1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_CAP1_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARDS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +  //added by LinYong in 20150324
                        "(PILOT_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') or " +
                        "(STEWARD_DEADHEAD_OPS_SID like '%" + dataRowIndex["cnvcSID"].ToString() + "%') ";

                    strSort = "DATOP";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTableSelectMember = GetDataBySQL("tbFltReport", strSQL, strSort, strFilterField);

                    if ((dataTableSelectMember != null) && (dataTableSelectMember.Rows.Count > 0))
                    {

                        DataTable dataTableSelectMember_SegmentInfo = new DataTable(); //保存 某个人员 在 内存任务书表 中 的 所有执行航段信息

                        foreach (DataRow dataRowdataTableSelectMember in dataTableSelectMember.Rows)
                        {
                            VoyageReportBM voyageReportBMdataTableSelectMember = new VoyageReportBM(dataRowdataTableSelectMember);
                            if (voyageReportBMdataTableSelectMember.Success)
                            {
                                string strMemo = "";    //added by LinYong in 20150324
                                string strDepArrStn = "";   //added by LinYong in 20150325

                                if (voyageReportBMdataTableSelectMember.pilot_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)    //added by LinYong in 20150324        
                                    strMemo = "加机组";
                                if (voyageReportBMdataTableSelectMember.steward_deadhead_ops_SID.IndexOf(dataRowIndex["cnvcSID"].ToString()) >= 0)  //added by LinYong in 20150324
                                    strMemo = "加机组";

                                if (strMemo == "加机组")    //added by LinYong in 20150325   
                                {
                                    voyageReportBMdataTableSelectMember.AnalyseVoyageReport_MemberInfo();
                                    if (voyageReportBMdataTableSelectMember.MemberInfo != null)
                                    {
                                        DataRow[] dataRowsvoyageReportBMdataTableSelectMember_MemberInfo = voyageReportBMdataTableSelectMember.MemberInfo.Select("cnvcSID = '" + dataRowIndex["cnvcSID"].ToString() + "'");
                                        if (dataRowsvoyageReportBMdataTableSelectMember_MemberInfo.Length > 0)
                                        {
                                            strDepArrStn = dataRowsvoyageReportBMdataTableSelectMember_MemberInfo[0]["cnvcDepArrStn"].ToString();
                                        }
                                    }
                                }

                                voyageReportBMdataTableSelectMember.AnalyseVoyageReport_SegmentInfo();
                                if (voyageReportBMdataTableSelectMember.SegmentInfo != null)
                                {
                                    if (dataTableSelectMember_SegmentInfo.Columns.Count <= 0)
                                    {
                                        dataTableSelectMember_SegmentInfo = voyageReportBMdataTableSelectMember.SegmentInfo.Clone();
                                    }

                                    foreach (DataRow dataRowvoyageReportBMdataTableSelectMember_SegmentInfo in voyageReportBMdataTableSelectMember.SegmentInfo.Rows)
                                    {
                                        if (strMemo == "加机组")    //added by LinYong in 20150325 
                                        {
                                            if (strDepArrStn.IndexOf(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcROUTE"].ToString().Trim()) < 0)
                                                continue;
                                        }

                                        dataRowvoyageReportBMdataTableSelectMember_SegmentInfo["cnvcMemo"] = strMemo;   //added by LinYong in 20150324
                                        dataTableSelectMember_SegmentInfo.ImportRow(dataRowvoyageReportBMdataTableSelectMember_SegmentInfo);
                                    }
                                }
                                else
                                {
                                    throw new Exception("各个成员的下段执行航班信息：生成 VoyageReportBM.SegmentInfo 时失败！");
                                }
                            }
                            else
                                throw new Exception("各个成员的下段执行航班信息：生成 VoyageReportBM 时失败！");
                        }

                        //补充 计划到达时间
                        if (dataTableSelectMember_SegmentInfo != null) //在现在这种算法下，不会出现 dataTableSelectMember_SegmentInfo == null 的情况
                        {
                            //在 dataTableSelectMember_SegmentInfo 中 增加 计划到达时间 列
                            dataColumn = new DataColumn("cnvcSTA", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "到达时间";
                            dataTableSelectMember_SegmentInfo.Columns.Add(dataColumn);

                            //
                            foreach (DataRow dataRowdataTableSelectMember_SegmentInfo in dataTableSelectMember_SegmentInfo.Rows)
                            {
                                string strSelect = "(cncFlightDate = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcDATOP"].ToString() + "') and " +
                                    "(cnvcFlightNo_Cal = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcFLTID"].ToString() + "') and " +
                                    "(cnvcLONG_REG = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcAC"].ToString() + "') and " +
                                    "(cnvcROUTE_Cal = '" + dataRowdataTableSelectMember_SegmentInfo["cnvcROUTE"].ToString() + "') " ;

                                DataRow[] dataRowstbLegs = tbLegs.Select(strSelect);

                                if (dataRowstbLegs.Length == 1)
                                {
                                    //日期时间分隔符 由 单空格 变为 "  "
                                    dataRowdataTableSelectMember_SegmentInfo["cnvcSTA"] = dataRowstbLegs[0]["cncSTA"].ToString().Substring(0, 16).Replace(" ", "  ");
                                }
                                else
                                    continue;
                            }

                        }

                        //计算上个航班（和进港航班运算函数有差异的地方）
                        if (dataTableSelectMember_SegmentInfo != null) //在现在这种算法下，不会出现 dataTableSelectMember_SegmentInfo == null 的情况
                        {
                            //默认 针对同个航班 从航站保障系统中传送过来的计划起飞时间STD 和 航前任务书数据表中的计划起飞时间 是一样的
                            DataRow[] dataRowsdataTableSelectMember_SegmentInfo = dataTableSelectMember_SegmentInfo.Select(("cnvcSTD < '" + STD.Replace(" ", "  ") + "'"), "cnvcSTD desc");
                            if (dataRowsdataTableSelectMember_SegmentInfo.Length > 0)
                            {
                                //是否 加机组 航班  added by LinYong in 20150324
                                if (dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcMemo"].ToString() == "加机组")
                                    dataRowIndex["cnvcExecFlightInfo"] = "加机组：";

                                //航班基本信息
                                if (dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTA"].ToString().Length > 16)
                                {
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() +    //modified by LinYong in 20150324
                                        "起降时间：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " ") +
                                        " - " +
                                        dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTA"].ToString().Substring(12, 5) + " ； " +
                                        "飞机号：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString() + " ； " +
                                        "航班号：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcFLTID"].ToString() + " ； " +
                                        "航段：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcROUTE"].ToString() + " ； ";
                                }
                                else
                                {
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() +    //modified by LinYong in 20150324 
                                        "起降时间：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTD"].ToString().Replace("  ", " ") +
                                        " - " +
                                        " ； " +
                                        "飞机号：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString() + " ； " +
                                        "航班号：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcFLTID"].ToString() + " ； " +
                                        "航段：" + dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcROUTE"].ToString() + " ； ";

                                }

                                //连飞 判断
                                try
                                {
                                    DateTime dtCurrentFlightSTD = Convert.ToDateTime(STD);
                                    DateTime dtPreFlightSTA = Convert.ToDateTime(dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcSTA"].ToString().Replace("  ", " "));
                                    if (dtCurrentFlightSTD <= dtPreFlightSTA.AddHours(2))
                                        dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "连飞：是 ； ";
                                    else
                                        dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "连飞：否 ； ";
                                }
                                catch (Exception ex)
                                {
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "连飞：  ； ";
                                }

                                //更换飞机 判断
                                if (AC == dataRowsdataTableSelectMember_SegmentInfo[0]["cnvcAC"].ToString())
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "更换飞机：否 ； ";
                                else
                                    dataRowIndex["cnvcExecFlightInfo"] = dataRowIndex["cnvcExecFlightInfo"].ToString() + "更换飞机：是 ； ";

                            }

                        }

                    }
                    else if (dataTableSelectMember.Rows.Count <= 0)
                    {
                        if (dataRowIndex["cnvcPosition"].ToString().IndexOf("加机组") < 0)
                        {
                            throw new Exception("各个成员的上段执行航班信息：从内存表中没有提取到此成员的任务书信息（应该有，需查找原因）！");
                        }
                    }
                    else if (dataTableSelectMember == null)
                    {
                        throw new Exception("各个成员的上段执行航班信息：从内存表中提取此成员的任务书信息时出现异常！");
                    }

                }
                catch (Exception ex)
                {
                    dataRowIndex["cnvcMemo"] = dataRowIndex["cnvcMemo"] + "【" + ex.Message + "】";
                }

            }
            #endregion 各个成员的上段执行航班信息

            #region 增加 统计行

            string strSIDs = "";
            int multipleBusiness = 0;   //一个人在多个岗位
            int countTatal = 0;

            foreach (DataRow dataRowdataTableResult in dataTableResult.Rows)   //遍历每个成员，成员数据行 dataRowIndex
            {
                if (strSIDs.IndexOf(dataRowdataTableResult["cnvcSID"].ToString()) < 0)
                {
                    strSIDs = strSIDs + dataRowdataTableResult["cnvcSID"].ToString() + "；";
                    countTatal++;
                }
                else
                    multipleBusiness++;
            }

            DataRow dataRowdataTableResult_sum = dataTableResult.NewRow();
            dataRowdataTableResult_sum["cnvcPosition"] = "合计";
            dataRowdataTableResult_sum["cnvcExecFlightInfo"] = "总人数：" + countTatal.ToString() 
                + "，其中兼职人数：" + multipleBusiness.ToString()
                + "。【任务书ID：" + strFltReportId + "】";
            dataTableResult.Rows.InsertAt(dataRowdataTableResult_sum, 0);

            #endregion 增加 统计行


            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTableResult != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTableResult, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion 把数据表压缩和系列化成二进制流


            //返回结果
            return bResult;


            #endregion 编码实现

        }
        #endregion 进港航班的航前任务书的解析信息（人员、联系方式、下个航班信息（是否连飞、是否更换飞机）），byte[] GetVoyageReportDataBySingleFlight_Out(...)



        #endregion 代理的过程


        #region 根据查询语句从内存表提取数据[如果 strTableName 表示的数据表在多线程中有修改操作，需要增加同步锁]
        /// <summary>
        /// 根据查询语句从内存表提取数据
        /// </summary>
        /// <param name="strTableName">内存表名</param>
        /// <param name="strSQL">查询语句</param>
        /// <param name="strSort">排序语句</param>
        /// <param name="strFilterField">需要提取的字段，如",column1,column2,column3,"</param>
        /// <returns>查询的结果：错误返回 null</returns>
        public DataTable GetDataBySQL(string strTableName, string strSQL, string strSort, string strFilterField)
        {
            #region 变量声明
            DataTable dataTable = null;
            DataRow[] dataRows = null;

            #endregion


            #region 编码实现
            try
            {
                //tbFltReport
                if (strTableName == "tbFltReport")
                {
                    dataTable = tbFltReport.Clone();
                    dataRows = tbFltReport.Select(strSQL, strSort);
                }
                //tbBasicCrewInfo_Profile
                else if (strTableName == "tbBasicCrewInfo_Profile")
                {
                    dataTable = tbBasicCrewInfo_Profile.Clone();
                    dataRows = tbBasicCrewInfo_Profile.Select(strSQL, strSort);
                }


                //把数据导入 dataTable
                foreach (DataRow dataRow in dataRows)
                {
                    dataTable.ImportRow(dataRow);
                }

                //把 dataTable 中不需要的字段删除
                if (strFilterField != "")
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        if (strFilterField.IndexOf(("," + dataColumn.ColumnName + ","), StringComparison.InvariantCultureIgnoreCase) < 0)
                            dataTable.Columns.Remove(dataColumn);
                    }
                }

            }
            catch
            {
                dataTable = null;
            }

            //返回结果
            return dataTable;

            #endregion
        }
        #endregion




    }
}
