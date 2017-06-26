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
using AirSoft.FlightMonitor.AgentServiceBM;
using CompressDataSet.Common;
using System.Collections;

namespace AirSoft.FlightMonitor.AgentServiceBF
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
    public class AgentServiceBF
    {
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
                //TcpChannel tcpChannel = new TcpChannel(Convert.ToInt32(ConfigurationManager.AppSettings["AgentPort"].Trim()));
                IDictionary props = new Hashtable();
                props["name"] = "ChannelName";
                props["port"] = Convert.ToInt32(ConfigurationManager.AppSettings["AgentPort"].Trim());
                TcpChannel tcpChannel = new TcpChannel(props, new BinaryClientFormatterSinkProvider(), new BinaryServerFormatterSinkProvider());
                //IChannel channel = new TcpChannel(props, new BinaryClientFormatterSinkProvider(), new BinaryServerFormatterSinkProvider());
                //ChannelServices.RegisterChannel(channel, true);
                
                //注册通道
                ChannelServices.RegisterChannel(tcpChannel, false);

                RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(AgentServiceDAF), "AgentServiceDAF", WellKnownObjectMode.Singleton
                );

                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion

        #region 注册Tcp通道
        /// <summary>
        /// 注册Tcp通道
        /// </summary>
        /// <returns>ReturnValueSF.Result：1 成功；-1 失败。 ReturnValueSF.Message：失败原因</returns>
        public ReturnValueSF RegisterChannel_Bak()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //创建Tcp通道
                TcpChannel tcpChannel = new TcpChannel(Convert.ToInt32(ConfigurationManager.AppSettings["AgentPort"].Trim()));


                //注册通道
                ChannelServices.RegisterChannel(tcpChannel, false);

                RemotingConfiguration.RegisterWellKnownServiceType
                (
                typeof(AgentServiceDAF), "AgentServiceDAF", WellKnownObjectMode.Singleton
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

        #region 初始化 AgentServiceDAF 类(记录表)
        /// <summary>
        /// 初始化 AgentServiceDAF 类(记录表)
        /// </summary>
        /// <returns>ReturnValueSF.Result：1 成功；-1 失败。 ReturnValueSF.Message：失败原因</returns>
        public ReturnValueSF InitializeDAL()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();

                AgentServiceDAF.dtProcRecords = procRecordsDAF.CreateDatatable();
                AgentServiceDAF.dtProcAnalysis = procAnalysisDAF.CreateDatatable();

                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// 根据提供的参数初始化 AgentServiceDAF类 的 记录表 对象
        /// </summary>
        /// <param name="strInitTable">需要初始化的表格，格式如：";dtProcRecords;dtProcAnalysis;dtOnLineUsers;"</param>
        /// <returns></returns>
        public ReturnValueSF InitializeDAL(string strInitTables)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //
                if (strInitTables.IndexOf(";dtProcRecords;") >= 0)
                {
                    ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                    AgentServiceDAF.dtProcRecords = procRecordsDAF.CreateDatatable();
                }
                if (strInitTables.IndexOf(";dtProcAnalysis;") >= 0)
                {
                    ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                    AgentServiceDAF.dtProcAnalysis = procAnalysisDAF.CreateDatatable();
                }
                if (strInitTables.IndexOf(";dtOnLineUsers;") >= 0)
                {
                    OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                    AgentServiceDAF.dtOnLineUsers = onLineUsersDAF.CreateDatatable();
                }

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

        #region 获取 AgentServiceDAF 类的 静态表格
        /// <summary>
        /// 获取 AgentServiceDAF 类的 静态表格
        /// </summary>
        /// <param name="strDatatableName">表格名称：tbLegs;vw_Legs;vw_FlightChangeRecord;dtProcRecords;dtProcAnalysis</param>
        /// <returns></returns>
        public ReturnValueSF GetDatatable(string strDatatableName)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //
                rvSF.Result = 1;

                if (strDatatableName == "tbLegs")
                    rvSF.Dt = AgentServiceDAF.tbLegs;
                else if (strDatatableName == "vw_Legs")
                    rvSF.Dt = AgentServiceDAF.vw_Legs;
                else if (strDatatableName == "vw_FlightChangeRecord")
                    rvSF.Dt = AgentServiceDAF.vw_FlightChangeRecord;
                else if (strDatatableName == "dtProcRecords")
                    rvSF.Dt = AgentServiceDAF.dtProcRecords;
                else if (strDatatableName == "dtProcAnalysis")
                    rvSF.Dt = AgentServiceDAF.dtProcAnalysis;
                else if (strDatatableName == "dtOnLineUsers")
                    rvSF.Dt = AgentServiceDAF.dtOnLineUsers;
                else
                {
                    rvSF.Result = -1;
                    rvSF.Message = "没有表格：" + strDatatableName + "!";
                    rvSF.Dt = null;
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
                strURL = "tcp://" + ConfigurationManager.AppSettings["AgentIP"].Trim() + ":"
                    + ConfigurationManager.AppSettings["AgentPort"].Trim() + "/AgentServiceDAF";
                AgentServiceDAF.objRemotingObject = (AgentServiceDAF)Activator.GetObject(
                    typeof(AgentServiceDAF),
                    strURL);

                if (AgentServiceDAF.objRemotingObject == null)
                {
                    rvSF.Result = -1;
                    rvSF.Message = "设置远程对象失败！";
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }
            return rvSF;
        }

        #endregion

        #region 设置远程对象
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentIP">代理服务IP</param>
        /// <param name="agentPort">代理服务端口</param>
        /// <returns></returns>
        public ReturnValueSF SetRemotingObject(string agentIP, string agentPort)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            string strURL = null;

            try
            {
                //
                rvSF.Result = 1;
                strURL = "tcp://" + agentIP.Trim() + ":"
                    + agentPort.Trim() + "/AgentServiceDAF";
                AgentServiceDAF.objRemotingObject = (AgentServiceDAF)Activator.GetObject(
                    typeof(AgentServiceDAF),
                    strURL);

                if (AgentServiceDAF.objRemotingObject == null)
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
        public AgentServiceDAF GetRemotingObject()
        {
            return AgentServiceDAF.objRemotingObject;
        }
        #endregion


        #region 同步内存表和数据库表

        #region 同步内存表和数据库表
        /// <summary>
        /// 同步内存表和数据库表
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_Bak()
        {
            #region 变量声明
            ReturnValueSF rvSF = null;
            int intResult = 1;
            string strMessage = "";
            string strStartTime = "", strEndTime = "";

            #endregion


            #region 编码实现

            //
            strStartTime = DateTime.Now.ToString("HH:mm:ss");
            strMessage = "数据同步：";

            //更新 tbLegs
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            rvSF = SynchronizeDatas_tbLegs();
            if (rvSF.Result > 0)
                strMessage += "（tbLegs：成功）";
            else
                strMessage += "（tbLegs：失败）";

            //更新 vw_Legs
            strMessage += "[" + DateTime.Now.ToString("mm:ss") + "]";
            rvSF = SynchronizeDatas_vw_Legs();
            if (rvSF.Result > 0)
                strMessage += "（vw_Legs：成功）";
            else
                strMessage += "（vw_Legs：失败）";

            //更新 vw_FlightChangeRecord
            strMessage += "[" + DateTime.Now.ToString("m:ss") + "]";
            rvSF = SynchronizeDatas_vw_FlightChangeRecord();
            if (rvSF.Result > 0)
                strMessage += "（vw_FlightChangeRecord：成功）";
            else
                strMessage += "（vw_FlightChangeRecord：失败）";

            //
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            strEndTime = DateTime.Now.ToString("HH:mm:ss");

            //返回结果
            rvSF = new ReturnValueSF();
            rvSF.Result = intResult;
            //rvSF.Message = strMessage + "[" + strStartTime + "-" + strEndTime + "]";
            rvSF.Message = strMessage;

            return rvSF;

            #endregion
        }

        #endregion

        #region 同步内存表和数据库表 (tbLegs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_tbLegs()
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
                AgentServiceDAF.tbLegs = rvSF.Dt;
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
        #endregion

        #region 同步内存表和数据库表 (vw_Legs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_Legs()
        {
            #region 变量声明
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //初始化
            dateTimeBM.StartDateTime = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";

            //更新 vw_Legs
            try
            {
                //调用数据访问层外观类方法
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetAllvw_LegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                AgentServiceDAF.vw_Legs = rvSF.Dt;
            }
            else
            {
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }
        #endregion

        #region 同步内存表和数据库表 (vw_FlightChangeRecord)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_FlightChangeRecord()
        {
            #region 变量声明
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //初始化
            dateTimeBM.StartDateTime = DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss");
            dateTimeBM.EndDateTime = dateTimeBM.StartDateTime;

            //更新 vw_FlightChangeRecord
            try
            {
                ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                rvSF.Dt = changeRecordDAF.GetLastvw_FlightChangeRecord(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Dt = null;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                AgentServiceDAF.vw_FlightChangeRecord = rvSF.Dt;
            }
            else
            {
                rvSF.Result = -1;
            }


            //返回结果
            return rvSF;

            #endregion
        }
        #endregion


        #region 同步内存表和数据库表,针对解决调用 guaranteeInforBF.GetFlightsByStation 的航班数据和变更数据同步问题
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

            DataTable dttbLegs = null, dtvw_legs = null, dtvw_FlightChangeRecord = null;
            bool blntbLegs = false, blnvw_legs = false, blnvw_FlightChangeRecord = false;
            #endregion


            #region 编码实现

            //
            strStartTime = DateTime.Now.ToString("HH:mm:ss");
            strMessage = "数据同步：";

            //更新 tbLegs
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            if (SysMsgBM.AgentLevel == "2")
            {
                if (SysMsgBM.Compress.ToUpper() == "TRUE")
                    rvSF = GettbLegs();
                else
                    rvSF = GettbLegs_NotCompress();
            }
            else
                rvSF = SynchronizeDatas_tbLegs__1();

            if (rvSF.Result > 0)
            {
                strMessage += "（tbLegs：成功）";
                dttbLegs = rvSF.Dt;
                blntbLegs = true;
            }
            else
                strMessage += "（tbLegs：失败）";

            //更新 vw_FlightChangeRecord
            strMessage += "[" + DateTime.Now.ToString("m:ss") + "]";
            if (SysMsgBM.AgentLevel == "2")
            {
                if (SysMsgBM.Compress.ToUpper() == "TRUE")
                    rvSF = Getvw_FlightChangeRecord();
                else
                    rvSF = Getvw_FlightChangeRecord_NotCompress();
            }
            else
                rvSF = SynchronizeDatas_vw_FlightChangeRecord__1();

            if (rvSF.Result > 0)
            {
                strMessage += "（vw_FlightChangeRecord：成功）";
                dtvw_FlightChangeRecord = rvSF.Dt;
                blnvw_FlightChangeRecord = true;
            }
            else
                strMessage += "（vw_FlightChangeRecord：失败）";

            //更新 vw_Legs
            strMessage += "[" + DateTime.Now.ToString("mm:ss") + "]";

            if (SysMsgBM.AgentLevel == "2")
            {
                if (SysMsgBM.Compress.ToUpper() == "TRUE")
                    rvSF = Getvw_Legs();
                else
                    rvSF = Getvw_Legs_NotCompress();
            }
            else
                rvSF = SynchronizeDatas_vw_Legs__1();

            if (rvSF.Result > 0)
            {
                strMessage += "（vw_Legs：成功）";
                dtvw_legs = rvSF.Dt;
                blnvw_legs = true;
            }
            else
                strMessage += "（vw_Legs：失败）";

            //
            strMessage += "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
            strEndTime = DateTime.Now.ToString("HH:mm:ss");

            //赋值 AgentServiceDAF 的相应变量
            if (blntbLegs)
                AgentServiceDAF.tbLegs = dttbLegs;
            if (blnvw_FlightChangeRecord && blnvw_legs) //先赋值vw_Legs，然后是vw_FlightChangeRecord，这样更合理
            {
                AgentServiceDAF.vw_Legs = dtvw_legs;
                AgentServiceDAF.vw_FlightChangeRecord = dtvw_FlightChangeRecord;
            }

            //返回结果
            rvSF = new ReturnValueSF();
            rvSF.Result = intResult;
            //rvSF.Message = strMessage + "[" + strStartTime + "-" + strEndTime + "]";
            rvSF.Message = strMessage;

            return rvSF;

            #endregion
        }

        #endregion

        #region 同步内存表和数据库表 -- 返回数据表，并未赋值AgentServiceDAF.tbLegs (tbLegs)
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
        #endregion

        #region 同步内存表和数据库表 -- 并未赋值AgentServiceDAF.vw_Legs (vw_Legs)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_Legs__1()
        {
            #region 变量声明
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //初始化
            dateTimeBM.StartDateTime = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";

            //更新 vw_Legs
            try
            {
                //调用数据访问层外观类方法
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetAllvw_LegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                //AgentServiceDAF.vw_Legs = rvSF.Dt;
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
        #endregion

        #region 同步内存表和数据库表 -- 并未赋值AgentServiceDAF.vw_FlightChangeRecord (vw_FlightChangeRecord)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas_vw_FlightChangeRecord__1()
        {
            #region 变量声明
            DateTimeBM dateTimeBM = new DateTimeBM();
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region 编码实现
            //初始化
            dateTimeBM.StartDateTime = DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss");
            dateTimeBM.EndDateTime = dateTimeBM.StartDateTime;

            //更新 vw_FlightChangeRecord
            try
            {
                ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                rvSF.Dt = changeRecordDAF.GetLastvw_FlightChangeRecord(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Dt = null;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            if ((rvSF.Result > 0) && (rvSF.Dt != null))
            {
                //AgentServiceDAF.vw_FlightChangeRecord = rvSF.Dt;
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
        #endregion


        #region GettbLegs()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据
        /// <summary>
        /// GettbLegs()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GettbLegs()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GettbLegs();
                    if (bytesToDecompress == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs]返回数据表 null！[\GettbLegs][\AgentServiceDAF]");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs]数据解压错误！[\GettbLegs][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][GettbLegs]AgentServiceDAF.objRemotingObject 为 null！[\GettbLegs][\AgentServiceDAF]");
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
        #endregion GettbLegs()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据

        #region GettbLegs_NotCompress()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据，返回的数据不进行压缩
        /// <summary>
        /// GettbLegs_NotCompress()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据，返回的数据不进行压缩
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GettbLegs_NotCompress()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.GettbLegs_NotCompress();
                    if (dtDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs_NotCompress]返回数据表 null！[\GettbLegs_NotCompress][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][GettbLegs_NotCompress]AgentServiceDAF.objRemotingObject 为 null！[\GettbLegs_NotCompress][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion GettbLegs_NotCompress()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据，返回的数据不进行压缩


        #region Getvw_Legs()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据
        /// <summary>
        /// Getvw_Legs()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_Legs()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.Getvw_Legs();
                    if (bytesToDecompress == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_Legs]返回数据表 null！[\Getvw_Legs][\AgentServiceDAF]");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_Legs]数据解压错误！[\Getvw_Legs][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_Legs]AgentServiceDAF.objRemotingObject 为 null！[\Getvw_Legs][\AgentServiceDAF]");
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
        #endregion Getvw_Legs()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据

        #region Getvw_Legs_NotCompress()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据，返回的数据不进行压缩
        /// <summary>
        /// Getvw_Legs_NotCompress()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据，返回的数据不进行压缩
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_Legs_NotCompress()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.Getvw_Legs_NotCompress();
                    if (dtDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_Legs_NotCompress]返回数据表 null！[\Getvw_Legs_NotCompress][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_Legs_NotCompress]AgentServiceDAF.objRemotingObject 为 null！[\Getvw_Legs_NotCompress][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion Getvw_Legs_NotCompress()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据，返回的数据不进行压缩


        #region Getvw_FlightChangeRecord()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据
        /// <summary>
        /// Getvw_FlightChangeRecord()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_FlightChangeRecord()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.Getvw_FlightChangeRecord();
                    if (bytesToDecompress == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord]返回数据表 null！[\Getvw_FlightChangeRecord][\AgentServiceDAF]");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord]数据解压错误！[\Getvw_FlightChangeRecord][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord]AgentServiceDAF.objRemotingObject 为 null！[\Getvw_FlightChangeRecord][\AgentServiceDAF]");
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
        #endregion Getvw_FlightChangeRecord()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据

        #region Getvw_FlightChangeRecord_NotCompress()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据，返回的数据不进行压缩
        /// <summary>
        /// Getvw_FlightChangeRecord_NotCompress()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据，返回的数据不进行压缩
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF Getvw_FlightChangeRecord_NotCompress()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.Getvw_FlightChangeRecord_NotCompress();
                    if (dtDatatable == null)
                        throw new Exception(@"[AgentServiceDAF][GettbLegs_NotCompress]返回数据表 null！[\GettbLegs_NotCompress][\AgentServiceDAF]");
                }
                else
                {
                    throw new Exception(@"[AgentServiceDAF][Getvw_FlightChangeRecord_NotCompress]AgentServiceDAF.objRemotingObject 为 null！[\Getvw_FlightChangeRecord_NotCompress][\AgentServiceDAF]");
                }

                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion Getvw_FlightChangeRecord_NotCompress()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据，返回的数据不进行压缩















        #endregion

    }
}
