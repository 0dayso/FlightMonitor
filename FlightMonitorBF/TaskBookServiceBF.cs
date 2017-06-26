using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Configuration;
using System.Data;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 任务书数据访问服务
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2013-09-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class TaskBookServiceBF
    {
        //执行 SynchronizeDatas_tbBasicCrewInfo_Profile__1 的时间
        static private DateTime execSynchronizeDatas_tbBasicCrewInfo_Profile__1 = Convert.ToDateTime( "1900-01-01");


        #region 注册Tcp通道
        /// <summary>
        /// 注册Tcp通道
        /// </summary>
        /// <returns>ReturnValueSF.Result：1 成功；-1 失败。 ReturnValueSF.Message：失败原因</returns>
        public ReturnValueSF RegisterChannel()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //创建Tcp通道
                TcpChannel tcpChannel = new TcpChannel(Convert.ToInt32(ConfigurationManager.AppSettings["TaskBookServicePort"].Trim()));

                //注册通道
                ChannelServices.RegisterChannel(tcpChannel, false);

                RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(TaskBookServiceDAF), "TaskBookServiceDAF", WellKnownObjectMode.Singleton
                );

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

        #region 设置远程对象
        public ReturnValueSF SetRemotingObject()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            string strURL = null;

            try
            {
                //
                rvSF.Result = 1;
                strURL = "tcp://" + ConfigurationManager.AppSettings["TaskBookServiceIP"].Trim() + ":"
                    + ConfigurationManager.AppSettings["TaskBookServicePort"].Trim() + "/TaskBookServiceDAF";
                TaskBookServiceDAF.objRemotingObject = (TaskBookServiceDAF)Activator.GetObject(
                    typeof(TaskBookServiceDAF),
                    strURL);

                if (TaskBookServiceDAF.objRemotingObject == null)
                {
                    rvSF.Result = -1;
                    rvSF.Message = "设置远程对象失败！";
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        #endregion

        #region 获取远程对象
        public TaskBookServiceDAF GetRemotingObject()
        {
            return TaskBookServiceDAF.objRemotingObject;
        }
        #endregion


        #region 同步内存表和数据库表
        /// <summary>
        /// 同步内存表和数据库表
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas()
        {
            #region 变量声明
            ReturnValueSF rvSF = null;
            int intResult = 1;
            string strMessage = "";
            string strStartTime = "", strEndTime = "";

            DataTable dttbFltReport = null, dttbBasicCrewInfo_Profile = null, dttbLegs = null;
            bool blntbFltReport = false, blntbBasicCrewInfo_Profile = false,  blntbLegs = false;
            #endregion


            #region 编码实现

            //
            strStartTime = DateTime.Now.ToString("HH:mm:ss");
            strMessage = "数据同步：";

            //更新 tbFltReport
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            rvSF = SynchronizeDatas_tbFltReport__1();
            if (rvSF.Result > 0)
            {
                strMessage += "（tbFltReport：成功）";
                dttbFltReport = rvSF.Dt;
                blntbFltReport = true;

            }
            else
                strMessage += "（tbFltReport：失败）";

            //更新 tbBasicCrewInfo_Profile
            if (DateTime.Now.ToString("yyyy-MM-dd") != execSynchronizeDatas_tbBasicCrewInfo_Profile__1.ToString("yyyy-MM-dd"))
            {
                strMessage += "[" + DateTime.Now.ToString("mm:ss") + "]";
                rvSF = SynchronizeDatas_tbBasicCrewInfo_Profile__1();
                if (rvSF.Result > 0)
                {
                    strMessage += "（tbBasicCrewInfo_Profile：成功）";
                    dttbBasicCrewInfo_Profile = rvSF.Dt;
                    blntbBasicCrewInfo_Profile = true;

                    execSynchronizeDatas_tbBasicCrewInfo_Profile__1 = DateTime.Now;
                }
                else
                    strMessage += "（tbBasicCrewInfo_Profile：失败）";
            }

            //更新 tbLegs
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            rvSF = SynchronizeDatas_tbLegs__1();
            if (rvSF.Result > 0)
            {
                strMessage += "（tbLegs：成功）";
                dttbLegs = rvSF.Dt;
                blntbLegs = true;

                #region 增加 航班号和航段 计算列
                DataColumn dataColumn = new DataColumn("cnvcFlightNo_Cal", Type.GetType("System.String"));
                dataColumn.DefaultValue = "";
                dataColumn.Caption = "航班号";
                dttbLegs.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcROUTE_Cal", Type.GetType("System.String"));
                dataColumn.DefaultValue = "";
                dataColumn.Caption = "航段";
                dttbLegs.Columns.Add(dataColumn);

                foreach (DataRow dataRowdttbLegs in dttbLegs.Rows)
                {
                    dataRowdttbLegs["cnvcFlightNo_Cal"] = dataRowdttbLegs["cnvcFlightNo"].ToString().Replace(" ", "");
                    dataRowdttbLegs["cnvcROUTE_Cal"] = dataRowdttbLegs["cncDEPSTN"].ToString() + "-" + dataRowdttbLegs["cncARRSTN"].ToString();
                }

                #endregion 增加 航班号和航段 计算列

            }
            else
                strMessage += "（tbLegs：失败）";

            //
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            strEndTime = DateTime.Now.ToString("HH:mm:ss");

            //赋值 TaskBookServiceDAF 的相应变量
            if (blntbFltReport)
                TaskBookServiceDAF.tbFltReport = dttbFltReport;
            if (blntbBasicCrewInfo_Profile)
                TaskBookServiceDAF.tbBasicCrewInfo_Profile = dttbBasicCrewInfo_Profile;
            if (blntbLegs)
                TaskBookServiceDAF.tbLegs = dttbLegs;

            //返回结果
            rvSF = new ReturnValueSF();
            rvSF.Result = intResult;
            //rvSF.Message = strMessage + "[" + strStartTime + "-" + strEndTime + "]";
            rvSF.Message = strMessage;

            return rvSF;

            #endregion
        }

        #endregion 同步内存表和数据库表

        #region 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbFltReport (tbFltReport)
        /// <summary>
        /// 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbFltReport (tbFltReport)
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbFltReport__1()
        {
            #region 变量声明
            DateTime DATOP_Start, DATOP_End;
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //初始化
            DATOP_Start = Convert.ToDateTime(DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00");
            DATOP_End = Convert.ToDateTime( DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00");

            //更新 tbFltReport
            try
            {
                //调用数据访问层外观类方法
                VoyageReportDAF voyageReportDAF = new VoyageReportDAF();
                rvSF.Dt = voyageReportDAF.GetVoyageReportData(DATOP_Start, DATOP_End);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                rvSF.Result = 1;
            }
            else
            {
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }
        #endregion 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbFltReport (tbFltReport)

        #region 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbBasicCrewInfo_Profile (tbBasicCrewInfo_Profile)
        /// <summary>
        /// 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbBasicCrewInfo_Profile (tbBasicCrewInfo_Profile)
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbBasicCrewInfo_Profile__1()
        {
            #region 变量声明
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //初始化

            //更新 tbBasicCrewInfo_Profile
            try
            {
                //调用数据访问层外观类方法
                BasicCrewInfoDAF basicCrewInfoDAF = new BasicCrewInfoDAF();
                rvSF.Dt = basicCrewInfoDAF.GetProfileInfo().Tables[0];
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                rvSF.Result = 1;
            }
            else
            {
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }
        #endregion 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbBasicCrewInfo_Profile (tbBasicCrewInfo_Profile)

        #region 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbLegs (tbLegs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbLegs__1()
        {
            #region 变量声明
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //初始化
            dateTimeBM.StartDateTime = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";

            //更新 tbLegs
            try
            {
                //调用数据访问层外观类方法
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF.Dt = changeLegsDAF.GetAllLegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                //AgentServiceDAF.tbLegs = rvSF.Dt;
                rvSF.Result = 1;
            }
            else
            {
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }
        #endregion 同步内存表和数据库表 -- 返回数据表，并未赋值TaskBookServiceDAF.tbLegs (tbLegs)


        #region 进港航班的航前任务书的解析信息（人员、联系方式、下个航班信息（是否连飞、是否更换飞机）），ReturnValueSF GetVoyageReportDataBySingleFlight_In(...)
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
        public ReturnValueSF GetVoyageReportDataBySingleFlight_In(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                DataTable dtDecompressedDatatable = null;

                TaskBookServiceDAF objRemotingObject = TaskBookServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetVoyageReportDataBySingleFlight_In( DATOP,  FLTIDS,  AC,  ROUTES,  STD,  STA);
                    if (bytesToDecompress == null)
                        throw new Exception("返回数据表 null！");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("数据解压错误！");
                }
                rvSF.Dt = dtDecompressedDatatable;


                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion 进港航班的航前任务书的解析信息（人员、联系方式、下个航班信息（是否连飞、是否更换飞机）），ReturnValueSF GetVoyageReportDataBySingleFlight_In(...)

        #region 出港航班的航前任务书的解析信息（人员、联系方式、上个航班信息（是否连飞、是否更换飞机）），ReturnValueSF GetVoyageReportDataBySingleFlight_Out(...)
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
        public ReturnValueSF GetVoyageReportDataBySingleFlight_Out(string DATOP, string FLTIDS, string AC, string ROUTES, string STD, string STA)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                DataTable dtDecompressedDatatable = null;

                TaskBookServiceDAF objRemotingObject = TaskBookServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetVoyageReportDataBySingleFlight_Out(DATOP, FLTIDS, AC, ROUTES, STD, STA);
                    if (bytesToDecompress == null)
                        throw new Exception("返回数据表 null！");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("数据解压错误！");
                }
                rvSF.Dt = dtDecompressedDatatable;


                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion 出港航班的航前任务书的解析信息（人员、联系方式、上个航班信息（是否连飞、是否更换飞机）），ReturnValueSF GetVoyageReportDataBySingleFlight_Out(...)





    }
}
