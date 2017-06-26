using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.AgentServiceBF;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.Public.SystemFramework;
using System.Configuration;
using System.Collections;

namespace MessageService.MessageServiceFM
{
    public partial class fmMessageService : Form
    {
        #region 声明变量
        //远程代理服务对象 AgentServiceDAF.objRemotingObject
        private bool blnSetRemotingObject = false; //表示是否已经设置了远程代理服务对象 AgentServiceDAF.objRemotingObject

        //处理机场消息
        private System.Threading.Timer timer;   //处理机场消息 用到的 线程定时器
        private bool blnBusy = false;           //表示 处理机场消息 的 线程定时器 是否繁忙，忙则退出
        private bool blnBusy_timer1 = false;           //表示 同步处理机场消息数据结果 的 定时器 是否繁忙，忙则退出

        private string strDTTM = "";            //最后一个记录的消息发送时间
        private int intEventID = -1;            //最后一个记录的自增长值

        static private DataTable dataTableLog = null;   //记录表，线程中使用

        static private DataTable dataTableLog_Main = null;   //记录表，主程序使用

        static private object objDataTableLog = new object();   //对 dataTableLog 记录表 的操作同步对象

        //从中航信导入截载时间
        private System.Threading.Timer timer_CC;   //从中航信导入截载时间 用到的 线程定时器
        private bool blnBusy_CC = false;           //表示 从中航信导入截载时间 的 线程定时器 是否繁忙，忙则退出
        private bool blnBusy_timer1_CC = false;           //表示 同步从中航信导入截载时间数据处理结果 的 定时器 是否繁忙，忙则退出

        static private DataTable dataTableLog_CC = null;   //记录表，线程中使用

        static private DataTable dataTableLog_Main_CC = null;   //记录表，主程序使用

        static private object objDataTableLog_CC = new object();   //对 dataTableLog 记录表 的操作同步对象


        private string strDEPSTNs = ""; //表示需要处理的航班的起飞机场要在此列表中

        //处理航班告警信息
        private System.Threading.Timer[] _arrayTimer = new System.Threading.Timer[50]; //存储线程对象：位置0 数据入库使用；位置1以上 各个机场进出港航班告警运算使用
        private bool[] _arrayBusy = new bool[50]; //对应上面的线程对象数组，表示该线程的繁忙状态
        private DateTime[] _arrayDateTime = new DateTime[1]; //存储最后操作的记录的操作时间，数据入库使用
        private bool blnBusy_timer1_FlightAlarm = false;           //表示 同步航班告警信息 的 定时器 是否繁忙，忙则退出


        static private DataTable _dtOverStationTime = null; //航站过站时间（包含所有数据，提供查询等使用）
        static private DataTable _dtStationInfor = null; //航站信息（包含所有数据（如滑行时间），提供查询等使用）
        static private DataTable _dtTodayInOutFlights = null;   //进出港航班表，保存最新状态
        static DataTable[] _arrayTodayInOutFlights = new DataTable[50]; //进出港航班表 数组，保存最新状态，每个数据表对应以上一个线程使用 -- 由于原设计经过完善后满足要求，故此变量暂不使用

        static private object _objTodayInOutFlights = new object();   //对 _dtTodayInOutFlights 数据表 的操作同步对象
        static private object[] _arrayObjTodayInOutFlights = new object[50];   //对 _arrayTodayInOutFlights 数组中的 数据表 对应的操作同步对象 -- 由于原设计经过完善后满足要求，故此变量暂不使用


      
        #endregion 声明变量


        public fmMessageService()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //显示 Log 页面
            tabControl1.SelectedTab = tabControl1.TabPages[0]; ;

            //代理服务对象
            if (!blnSetRemotingObject) //还未设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("代理服务对象获取失败，请重新登录！", "提示", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            }

            //获取最后一条记录（指定用户）
            #region 不使用
            //strDTTM = ConfigurationSettings.AppSettings["DTTM"];

            //if (strDTTM.Trim() == "")
            //{
            //    ReturnValueSF rvSFChangeRecordBF = new ReturnValueSF();
            //    ChangeRecordBF changeRecordBF = new ChangeRecordBF();
            //    rvSFChangeRecordBF = changeRecordBF.GetLastRecord("SYS_PEK");
            //    if ((rvSFChangeRecordBF.Result > 0) &&
            //        (rvSFChangeRecordBF.Dt != null) &&
            //        (rvSFChangeRecordBF.Dt.Rows.Count > 0))
            //    {
            //        strDTTM = rvSFChangeRecordBF.Dt.Rows[0]["cncFOCOperatingTime"].ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show("从变更表获取最后一条记录（用户：SYS_PEK）失败！", "提示", MessageBoxButtons.OK);
            //        return;
            //        //Environment.Exit(0);
            //    }
            //}
            #endregion 不使用
            string strEventID = ConfigurationSettings.AppSettings["EventID"].ToString().Trim();
            try
            {
                intEventID = Convert.ToInt32(strEventID);
            }
            catch
            {
                intEventID = -1;
            }

            //调用线程定时器，定时 处理机场消息
            int iRefreshInterval = 10 * 1000;
            TimerCallback timerDelegate = new TimerCallback(GetMessages);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);


            //
            timer1.Enabled = true;

            button1.Enabled = false;
        }

        public void GetMessages(object state)
        {
            //如果繁忙，则退出函数。
            if (blnBusy)
                return;

            //设置繁忙标记
            blnBusy = true;

            //窗体标题
            SysMsgBM.CaptureOffmMessageService = "机场消息服务【VER 201506261636】：数据表更新（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

            #region 获取机场消息，并匹配航站保障系统航班，最后维护到航站保障系统
            ReturnValueSF returnValueSF = null;
            MessageServiceBF messageServiceBF = new MessageServiceBF();
            //returnValueSF = messageServiceBF.GetMessages(strDTTM); //获取机场消息（根据上次获取到的最后一个记录的消息发送时间作为起点）
            returnValueSF = messageServiceBF.GetMessages(intEventID); //获取机场消息（根据上次获取到的最后一个记录的自增长值作为起点）
            if ((returnValueSF.Result > 0) && (returnValueSF.Dt != null) && (returnValueSF.Dt.Rows.Count > 0)) //有消息
            {
                DataTable dataTableMessage = returnValueSF.Dt;
                for (int indexDataTableMessage = 0; indexDataTableMessage < dataTableMessage.Rows.Count; indexDataTableMessage++) //遍历获取到的消息
                {
                    try
                    {
                        #region 消息的关键点数据
                        string DTTM = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString().Trim(); //最后一个记录的消息发送时间
                        strDTTM = DTTM; //本对象 全局静态变量，最后一个记录的消息发送时间

                        int EventID = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString().Trim()); //最后一个记录的自增长值
                        intEventID = EventID; //本对象 全局静态变量，最后一个记录的自增长值

                        string FlightNo = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString().Trim();
                        string ST = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString().Trim();
                        string STN = "PEK";
                        string IO = "";
                        if (dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString().Trim() == "D")
                            IO = "OUT";
                        else
                            IO = "IN";
                        string FieldENName = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString().Trim();
                        string ChangeReasonCode = "";
                        string ChangeNewContent = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString().Trim();
                        int FieldType = 1;

                        bool blnNeed = false; //如果是 false，则不处理此记录
                        #endregion 消息的关键点数据

                        #region 配置文件中记录EventID
                        try
                        {
                            //配置文件中记录EventID
                            //把最后处理到的 EventID 减 1后 回写到配置文件中的 EventID 节点（保证不会漏处理此条记录，下次启动程序时再次处理此条记录）
                            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            configuration.AppSettings.Settings["EventID"].Value = ((Int32)(intEventID - 1)).ToString();
                            configuration.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件
                        }
                        catch (Exception ex)
                        {

                        }
                        #endregion 配置文件中记录EventID

                        #region 报文代码和字段的对应
                        switch (FieldENName)
                        {
                            //case "HUDC2GIS-STND": //停机位
                            //    if (IO == "OUT") //出港停机位
                            //    {
                            //        ChangeReasonCode = "cnvcOutGate";
                            //        FieldType = 1;
                            //        blnNeed = true;
                            //    }
                            //    else //进港停机位
                            //    {
                            //        ChangeReasonCode = "cnvcInGATE";
                            //        FieldType = 1;
                            //        blnNeed = true;
                            //    }
                            //    break;
                            case "HUDC2GIS-STND": //停机位 -- 测试使用
                                if (IO == "OUT") //出港停机位 -- 测试使用
                                {
                                    ChangeReasonCode = "cnvcOutGate_Test"; 
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else //进港停机位 -- 测试使用
                                {
                                    ChangeReasonCode = "cnvcInGATE_Test";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDC2GIS-RWAY":    //跑道号
                                if (IO == "OUT")    //出港跑道号
                                {
                                    ChangeReasonCode = "cnvcOutRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else    //进港跑道号
                                {
                                    ChangeReasonCode = "cnvcInRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDCSRVT-RWAY":    //跑道号
                                if (IO == "OUT")    //出港跑道号
                                {
                                    ChangeReasonCode = "cnvcOutRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else    //进港跑道号
                                {
                                    ChangeReasonCode = "cnvcInRunway";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDCMCDM-EIBT":    //预计入位时间
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                else //进港
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncInEXIT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                            case "HUDCMCDM-TOBT": //预计推出时间
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutTOBT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-BOTM": //登机|开始
                                if (IO == "OUT") //出港
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardStartTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-BOTM": //登机|开始
                                if (IO == "OUT") //出港
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardStartTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-BCTM": //登机|结束 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-BCTM": //登机|结束 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBoardEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-BETM": //撤桥时间 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBridgeGuaranteeEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-BETM": //撤桥时间 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutBridgeGuaranteeEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCSRVT-CITM": //柜台结束时间 
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutCheckCounterEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-DPRK": //除冰坪
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutDeicePing";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-STND": //除冰位
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutDeiceWei";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            //case "": //到达除冰等待点
                            //    if (IO == "OUT")
                            //    {
                            //        ChangeReasonCode = "";
                            //        FieldType = 1;
                            //        blnNeed = false;
                            //    }
                            //    else
                            //    {
                            //        ChangeReasonCode = "";
                            //        FieldType = 1;
                            //        blnNeed = false;
                            //    }
                            //    break;
                            case "HUDCDEIC-STDI": //除冰开始
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutDeiceStartTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-EDDI": //除冰结束
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutDeiceEndTime";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-DOUT": //离开除冰位
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutLeaveDeicePing";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCDEIC-SLDI": //慢车除冰标识
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutSlowDeiceFlag";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCMCDM-TSAT ": //TSAT
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutTSAT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDCMCDM-CTOT": //CTOT
                                if (IO == "OUT")
                                {
                                    if (ChangeNewContent.Trim() != "")
                                        ChangeNewContent = Convert.ToDateTime(ChangeNewContent).ToString("HHmm");
                                    ChangeReasonCode = "cncOutCTOT";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "";
                                    FieldType = 1;
                                    blnNeed = false;
                                }
                                break;
                            case "HUDC2GIS-CHDT": //HUDC2GIS-CHDT 转盘号
                                if (IO == "OUT")
                                {
                                    ChangeReasonCode = "cnvcOutTurnTableNO"; 
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                else
                                {
                                    ChangeReasonCode = "cnvcInTurnTableNO";
                                    FieldType = 1;
                                    blnNeed = true;
                                }
                                break;
                        }
                        #endregion 报文代码和字段的对应

                        #region 不处理的消息
                        if (!blnNeed)
                        {
                            #region 记录到 log表
                            lock (objDataTableLog)
                            {
                                DataRow dataRowLog = dataTableLog.NewRow();
                                dataRowLog["EventID"] = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString());
                                dataRowLog["cnvcFFID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFFID"].ToString();
                                dataRowLog["cnvcCOMPANY"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCOMPANY"].ToString();
                                dataRowLog["cnvcFLTID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString();
                                dataRowLog["cnvcST"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString();
                                dataRowLog["cncIOTAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString();
                                dataRowLog["cncHOMETAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncHOMETAG"].ToString();
                                dataRowLog["cnvcSNDR"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSNDR"].ToString();
                                dataRowLog["cnvcDTTM"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString();
                                dataRowLog["cnvcTYPE"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcTYPE"].ToString();
                                dataRowLog["cnvcSTYP"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSTYP"].ToString();
                                dataRowLog["cncVALIDFLAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncVALIDFLAG"].ToString();
                                dataRowLog["cnvcCreateTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCreateTime"].ToString();
                                dataRowLog["cnvcCancelTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCancelTime"].ToString();
                                dataRowLog["cnvcMsgValue"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString();
                                dataRowLog["cnvcFieldCNName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldCNName"].ToString();
                                dataRowLog["cnvcFieldENName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString();
                                dataRowLog["cnvcResult"] = "不处理";
                                dataRowLog["cnvcMemo"] = "";
                                dataTableLog.Rows.Add(dataRowLog);
                            }
                            #endregion 记录到 log表

                            //继续下一条消息
                            continue;
                        }
                        #endregion 不处理的消息

                        #region 根据关键点数据获取此消息对应的航班（航站保障系统中的航班），并维护到航站保障系统
                        GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
                        ReturnValueSF returnValueSF_GetFlightsByMessage = guaranteeInforBF.GetFlightsByMessage(FlightNo, ST, STN, IO); //根据关键点数据获取此消息对应的航班（航站保障系统中的航班）
                        if ((returnValueSF_GetFlightsByMessage.Result > 0) &&
                            (returnValueSF_GetFlightsByMessage.Dt != null) &&
                            (returnValueSF_GetFlightsByMessage.Dt.Rows.Count == 1))
                        {//获取到一个航班，把相应相应信息维护到航站保障系统
                            OMPWebReference.FlightMonitorData wrFlightMonitorData = new MessageService.OMPWebReference.FlightMonitorData();
                            bool blnwrFlightMonitorData_MaintainGuaranteeInfor = wrFlightMonitorData.MaintainGuaranteeInfor(
                                "SYS_PEK", // UserID,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cnvcFLTID"].ToString(),   // FLTID,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncDATOP"].ToString(),    // DATOP,
                                Convert.ToInt32(returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cniLEGNO"].ToString()),    // LegNo,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cnvcAC"].ToString(),    // AC,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncDEPSTN"].ToString(),    // DepSTN,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncARRSTN"].ToString(),    // ArrSTN,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncSTD"].ToString(),    // STD,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncETD"].ToString(),    // ETD,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncSTA"].ToString(),    // STA,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0]["cncETA"].ToString(),    // ETA,
                                ChangeReasonCode, // ChangeReasonCode,
                                returnValueSF_GetFlightsByMessage.Dt.Rows[0][ChangeReasonCode].ToString(), // ChangeOldContent,
                                ChangeNewContent, // ChangeNewContent,
                                FieldType, //  FieldType,
                                DTTM // LocalOperatingTime
                                );
                            if (!blnwrFlightMonitorData_MaintainGuaranteeInfor)
                                throw new Exception("机场信息维护到航站保障系统失败！" + Environment.NewLine +
                                    "FlightNo：" + FlightNo + Environment.NewLine +
                                    "ST：" + ST + Environment.NewLine +
                                    "STN：" + STN + Environment.NewLine +
                                    "IO：" + IO);

                            #region 维护到航站保障系统 成功，记录到 log表
                            lock (objDataTableLog)
                            {
                                DataRow dataRowLog = dataTableLog.NewRow();
                                dataRowLog["EventID"] = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString());
                                dataRowLog["cnvcFFID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFFID"].ToString();
                                dataRowLog["cnvcCOMPANY"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCOMPANY"].ToString();
                                dataRowLog["cnvcFLTID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString();
                                dataRowLog["cnvcST"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString();
                                dataRowLog["cncIOTAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString();
                                dataRowLog["cncHOMETAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncHOMETAG"].ToString();
                                dataRowLog["cnvcSNDR"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSNDR"].ToString();
                                dataRowLog["cnvcDTTM"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString();
                                dataRowLog["cnvcTYPE"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcTYPE"].ToString();
                                dataRowLog["cnvcSTYP"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSTYP"].ToString();
                                dataRowLog["cncVALIDFLAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncVALIDFLAG"].ToString();
                                dataRowLog["cnvcCreateTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCreateTime"].ToString();
                                dataRowLog["cnvcCancelTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCancelTime"].ToString();
                                dataRowLog["cnvcMsgValue"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString();
                                dataRowLog["cnvcFieldCNName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldCNName"].ToString();
                                dataRowLog["cnvcFieldENName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString();
                                dataRowLog["cnvcResult"] = "成功";
                                dataRowLog["cnvcMemo"] = "";
                                dataTableLog.Rows.Add(dataRowLog);
                            }
                            #endregion 维护到航站保障系统 成功，记录到 log表
                        }
                        else if ((returnValueSF_GetFlightsByMessage.Result < 0) || (returnValueSF_GetFlightsByMessage.Dt == null))
                        {
                            throw new Exception("根据关键点数据获取消息对应的航班（航站保障系统中的航班）失败！" + Environment.NewLine +
                                    "FlightNo：" + FlightNo + Environment.NewLine +
                                    "ST：" + ST + Environment.NewLine +
                                    "STN：" + STN + Environment.NewLine +
                                    "IO：" + IO);
                        }
                        else if ((returnValueSF_GetFlightsByMessage.Result > 0) &&
                            (returnValueSF_GetFlightsByMessage.Dt != null) &&
                            (returnValueSF_GetFlightsByMessage.Dt.Rows.Count > 1))
                        {
                            throw new Exception("根据关键点数据获取消息对应的航班（航站保障系统中的航班）有多条！" + Environment.NewLine +
                                    "FlightNo：" + FlightNo + Environment.NewLine +
                                    "ST：" + ST + Environment.NewLine +
                                    "STN：" + STN + Environment.NewLine +
                                    "IO：" + IO);
                        }
                        else
                        {
                            throw new Exception("根据关键点数据获取消息对应的航班（航站保障系统中的航班）失败（其他情况）！" + Environment.NewLine +
                            "FlightNo：" + FlightNo + Environment.NewLine +
                            "ST：" + ST + Environment.NewLine +
                            "STN：" + STN + Environment.NewLine +
                            "IO：" + IO);
                        }
                        #endregion  根据关键点数据获取此消息对应的航班（航站保障系统中的航班），并维护到航站保障系统
                    }
                    catch (Exception ex)
                    {
                        #region 记录到 log表
                        lock (objDataTableLog)
                        {
                            DataRow dataRowLog = dataTableLog.NewRow();
                            dataRowLog["EventID"] = Convert.ToInt32(dataTableMessage.Rows[indexDataTableMessage]["EventID"].ToString());
                            dataRowLog["cnvcFFID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFFID"].ToString();
                            dataRowLog["cnvcCOMPANY"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCOMPANY"].ToString();
                            dataRowLog["cnvcFLTID"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFLTID"].ToString();
                            dataRowLog["cnvcST"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcST"].ToString();
                            dataRowLog["cncIOTAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncIOTAG"].ToString();
                            dataRowLog["cncHOMETAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncHOMETAG"].ToString();
                            dataRowLog["cnvcSNDR"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSNDR"].ToString();
                            dataRowLog["cnvcDTTM"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcDTTM"].ToString();
                            dataRowLog["cnvcTYPE"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcTYPE"].ToString();
                            dataRowLog["cnvcSTYP"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcSTYP"].ToString();
                            dataRowLog["cncVALIDFLAG"] = dataTableMessage.Rows[indexDataTableMessage]["cncVALIDFLAG"].ToString();
                            dataRowLog["cnvcCreateTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCreateTime"].ToString();
                            dataRowLog["cnvcCancelTime"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcCancelTime"].ToString();
                            dataRowLog["cnvcMsgValue"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcMsgValue"].ToString();
                            dataRowLog["cnvcFieldCNName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldCNName"].ToString();
                            dataRowLog["cnvcFieldENName"] = dataTableMessage.Rows[indexDataTableMessage]["cnvcFieldENName"].ToString();
                            dataRowLog["cnvcResult"] = "失败";
                            dataRowLog["cnvcMemo"] = ex.Message;
                            dataTableLog.Rows.Add(dataRowLog);
                        }
                        #endregion 记录到 log表
                    }

                }
            }
            #endregion 获取机场消息，并匹配航站保障系统航班，最后维护到航站保障系统

            //窗体标题
            SysMsgBM.CaptureOffmMessageService += "（" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "）";

            //清楚繁忙标记
            blnBusy = false;

        }

        private void fmMessageService_Load(object sender, EventArgs e)
        {
            //初始化 log 表

            #region 获取首都机场数据
            if (dataTableLog == null)
            {
                //子线程使用的 log 表
                dataTableLog = new DataTable();
                dataTableLog.Columns.Add("EventID", typeof(Int32));     //消息表自增长字段；主键     
                dataTableLog.Columns.Add("cnvcFFID" , typeof(string));	//航班ID别名，便于辨别(格式中字段顺序，航空公司代码 , 航班号 , 进离港标志 , 航班计划时间(或航班日期) , 国际国内标志)
                dataTableLog.Columns.Add("cnvcCOMPANY" , typeof(string));	//航空公司代码
                dataTableLog.Columns.Add("cnvcFLTID" , typeof(string));	//航班号
                dataTableLog.Columns.Add("cnvcST" , typeof(string));	//航班计划时间或航班日期
                dataTableLog.Columns.Add("cncIOTAG" , typeof(string));	//进离港标志（A:进港航班；D:出港航班）
                dataTableLog.Columns.Add("cncHOMETAG" , typeof(string));	//航班国际国内标志（D–Domestic/国内；I-International/国际）
                dataTableLog.Columns.Add("cnvcSNDR" , typeof(string));	//消息表字段
                dataTableLog.Columns.Add("cnvcDTTM" , typeof(string));	//消息发送时间
                dataTableLog.Columns.Add("cnvcTYPE" , typeof(string));	//消息类型
                dataTableLog.Columns.Add("cnvcSTYP" , typeof(string));	//消息子类型
                dataTableLog.Columns.Add("cncVALIDFLAG" , typeof(string));	//是否有效标志；目前该字段暂无意义，保留
                dataTableLog.Columns.Add("cnvcCreateTime" , typeof(string));	//消息入库时间
                dataTableLog.Columns.Add("cnvcCancelTime", typeof(string));	//取消时间；目前该字段暂无意义，保留
                dataTableLog.Columns.Add("cnvcMsgValue" , typeof(string));	//该消息数据项值
                dataTableLog.Columns.Add("cnvcFieldCNName" , typeof(string));	//该记录消息类型的中文名称
                dataTableLog.Columns.Add("cnvcFieldENName" , typeof(string));	//该记录消息类型的英文名称
                dataTableLog.Columns.Add("cnvcResult", typeof(string));	//操作结果：成功、失败或不处理
                dataTableLog.Columns.Add("cnvcMemo", typeof(string));	//备注：成功 或 失败 时的备注信息

                //主程序使用的 log 表
                dataTableLog_Main = dataTableLog.Clone();

                //绑定视图数据源
                dataGridView1.DataSource = dataTableLog_Main;
            }
            #endregion 获取首都机场数据

            #region 从中航信获取截载时间数据
            if (dataTableLog_CC == null)
            {
                //子线程使用的 log 表
                dataTableLog_CC = new DataTable();
                dataTableLog_CC.Columns.Add("cncDATOP", typeof(string));	        //航班日期
                dataTableLog_CC.Columns.Add("cncCKIFlightDate", typeof(string));	//CKI航班日期
                dataTableLog_CC.Columns.Add("cnvcFLTID", typeof(string));	        //航班号
                dataTableLog_CC.Columns.Add("cnvcCKIFlightNo", typeof(string));	    //CKI航班号
                dataTableLog_CC.Columns.Add("cniLEGNO", typeof(Int32));	            //航段号
                dataTableLog_CC.Columns.Add("cnvcAC", typeof(string));	            //飞机信息
                dataTableLog_CC.Columns.Add("cnvcLONG_REG", typeof(string));	    //长飞机号
                dataTableLog_CC.Columns.Add("cncDEPSTN", typeof(string));	        //起飞机场
                dataTableLog_CC.Columns.Add("cncARRSTN", typeof(string));	        //降落机场
                dataTableLog_CC.Columns.Add("cncSTD", typeof(string));	            //计划起飞时间
                dataTableLog_CC.Columns.Add("cncETD", typeof(string));	            //预计起飞时间
                dataTableLog_CC.Columns.Add("cncOutFlightInterceptTime", typeof(string));	            //系统中保存的截载时间
                dataTableLog_CC.Columns.Add("cnvcCC", typeof(string));	            //从中航信获取的截载时间
                dataTableLog_CC.Columns.Add("cnvcOperationTime", typeof(string));	//入库操作时间
                dataTableLog_CC.Columns.Add("cnvcMemo", typeof(string));	        //备注：成功 或 失败 时的备注信息

                //主程序使用的 log 表 
                dataTableLog_Main_CC = dataTableLog_CC.Clone();

                //绑定视图数据源
                dataGridView2.DataSource = dataTableLog_Main_CC;
            }
            #endregion 从中航信获取截载时间数据

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 获取首都机场数据
            if (blnBusy_timer1)
                return;

            try
            {
                blnBusy_timer1 = true;

                //
                lock (objDataTableLog)
                {
                    //
                    //foreach (DataRow dataRowDataTableLog in dataTableLog.Rows) 
                    //{
                    //    dataTableLog_Main.ImportRow(dataRowDataTableLog);
                    //    dataRowDataTableLog.Delete(); //此操作导致数据行集合中有行状态变更，枚举集合无法进行遍历操作，抛出异常
                    //}
                    for (int intIndex = 0; intIndex < dataTableLog.Rows.Count; intIndex++ )
                    {
                        dataTableLog_Main.ImportRow(dataTableLog.Rows[intIndex]);
                        dataTableLog.Rows[intIndex].Delete();
                    }
                    //
                    dataTableLog.AcceptChanges();
                }
                //
                if (dataTableLog_Main.Rows.Count > 10000)
                    dataTableLog_Main.Rows.Clear();

                //
                blnBusy_timer1 = false;
            }
            catch (Exception ex)
            {
                blnBusy_timer1 = false;
            }
            #endregion 获取首都机场数据

            #region 获取中航信航班截载信息
            if (blnBusy_timer1_CC)
                return;

            try
            {
                blnBusy_timer1_CC = true;

                //
                lock (objDataTableLog_CC)
                {
                    //
                    //foreach (DataRow dataRowDataTableLog_CC in dataTableLog_CC.Rows)
                    //{
                    //    dataTableLog_Main_CC.ImportRow(dataRowDataTableLog_CC);
                    //    dataRowDataTableLog_CC.Delete(); //此操作导致数据行集合中有行状态变更，枚举集合无法进行遍历操作，抛出异常
                    //}
                    for (int intIndex = 0; intIndex < dataTableLog_CC.Rows.Count; intIndex++)
                    {
                        dataTableLog_Main_CC.ImportRow(dataTableLog_CC.Rows[intIndex]);
                        dataTableLog_CC.Rows[intIndex].Delete();
                    }
                    //
                    dataTableLog_CC.AcceptChanges();
                }
                //
                if (dataTableLog_Main_CC.Rows.Count > 10000)
                    dataTableLog_Main_CC.Rows.Clear();

                //
                blnBusy_timer1_CC = false;
            }
            catch (Exception ex)
            {
                blnBusy_timer1_CC = false;
            }
            #endregion 获取中航信航班截载信息

            #region 航班告警信息
            if (blnBusy_timer1_FlightAlarm)
                return;

            try
            {
                //设置繁忙标记
                blnBusy_timer1_FlightAlarm = true;

                //数据同步
                if (_dtTodayInOutFlights != null)
                {
                    lock (_objTodayInOutFlights) //同步
                    {
                        DataRow[] arrayDataRowsTodayInOutFlights = _dtTodayInOutFlights.Select("cndOperationTime < '" + DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") + " 00:00:00'");
                        for (int intIndex = 0; intIndex < arrayDataRowsTodayInOutFlights.Length; intIndex++)
                        {
                            arrayDataRowsTodayInOutFlights[intIndex].Delete();
                        }
                        _dtTodayInOutFlights.AcceptChanges();


                        dataGridView4.DataSource = _dtTodayInOutFlights.Copy(); //数据表对象在多线程中使用时，需要同步，把及时性告警信息在主界面展示
                    }
                }

                //清除繁忙标记
                blnBusy_timer1_FlightAlarm = false;
            }
            catch(Exception ex)
            {
                blnBusy_timer1_FlightAlarm = false; //清除繁忙标记
            }
            #endregion 航班告警信息
        }

        private void fmMessageService_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    //配置文件中记录DTTM
            //    if (strDTTM != "")
            //    {
            //        //把最后处理到的 DTTM时间 回写到配置文件中的 DTTM 节点
            //        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //        configuration.AppSettings.Settings["DTTM"].Value = strDTTM;
            //        configuration.Save(ConfigurationSaveMode.Modified);  
            //        ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件
            //    }

            //    //配置文件中记录EventID
            //    if (intEventID > 0)
            //    {
            //        //把最后处理到的 EventID 减 1后 回写到配置文件中的 EventID 节点（保证不会漏处理此条记录，下次启动程序时再次处理此条记录）
            //        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //        configuration.AppSettings.Settings["EventID"].Value = ((intEventID - 1) as Int32).ToString();
            //        configuration.Save(ConfigurationSaveMode.Modified);
            //        ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //显示 Log 页面
            tabControl1.SelectedTab = tabControl1.TabPages[1]; ;

            //代理服务对象
            if (!blnSetRemotingObject) //还未设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("代理服务对象获取失败，请重新登录！", "提示", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            }


            //获取需要处理的航班的起飞机场列表
            strDEPSTNs = ";";
            ReturnValueSF rvSFStationBF = new ReturnValueSF();
            StationBF stationBF = new StationBF();
            rvSFStationBF = stationBF.GetAllStation();
            if ((rvSFStationBF.Result > 0) &&
                (rvSFStationBF.Dt != null) &&
                (rvSFStationBF.Dt.Rows.Count > 0))
            {
                foreach (DataRow dataRowStationInfor in rvSFStationBF.Dt.Rows)
                {
                    strDEPSTNs = strDEPSTNs + dataRowStationInfor["cncThreeCode"].ToString().Trim() + ";";
                }

                strDEPSTNs = strDEPSTNs.Trim(';');
            }
            else
            {
                MessageBox.Show("获取需要处理的航班的起飞机场列表失败！", "提示", MessageBoxButtons.OK);
                return;
                //Environment.Exit(0);
            }

            //调用线程定时器，定时 处理从中航信导入截载时间
            int iRefreshInterval = 480 * 1000; //调用频率设置为 8分钟
            TimerCallback timerDelegate = new TimerCallback(GetCCMessage);
            timer_CC = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);


            
            //
            timer1.Enabled = true;

            button2.Enabled = false;

        }

        public void GetCCMessage(object state)
        {
            //如果繁忙，则退出函数。
            if (blnBusy_CC)
                return;

            //设置繁忙标记
            blnBusy_CC = true;

            //分解 strDEPSTNs ，得到机场列表
            string[] arrayDEPSTNs = strDEPSTNs.Split(';');

            //设置处理的时间段
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dateTimeBM.EndDateTime = DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss");

            //遍历各个机场
            foreach (string strDEPSTN in arrayDEPSTNs)
            {
                //获取机场 strDEPSTN 相应时间段内的进出港航班
                StationBM stationBM = new StationBM();
                AccountBM accountBM = new AccountBM();

                stationBM.ThreeCode = strDEPSTN;
                accountBM.UserName = "SYS_TTL";
                accountBM.IPAddress = "";

                ReturnValueSF returnValueSF = null;
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
                returnValueSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM); //调用代理服务

                if ((returnValueSF.Result > 0) && (returnValueSF.Dt != null) && (returnValueSF.Dt.Rows.Count > 0)) //返回正常
                {
                    DataTable dataTableFlights = returnValueSF.Dt;
                    foreach (DataRow dataRowFlights in dataTableFlights.Rows) //遍历航班
                    {
                        if ((dataRowFlights["cncDEPSTN"].ToString().Trim() == strDEPSTN) && //只处理起飞机场在机场列表里的航班
                            (Convert.ToDateTime(dataRowFlights["cncETD"].ToString().Trim()) >= Convert.ToDateTime(dateTimeBM.StartDateTime)) && //只处理在指定时间段内的航班
                            (Convert.ToDateTime(dataRowFlights["cncETD"].ToString().Trim()) <  Convert.ToDateTime(dateTimeBM.EndDateTime)))
                        {
                            bool blnExceptionMessage = false; //true 有异常发生；false 没有异常发生
                            string strExceptionMessage = ""; //异常产生的错误信息

                            string strCKIFlightDate = "";
                            string strCKIFlightNo = "";
                            string strCC = ""; //从中航信系统获取航班截载信息

                            try
                            {
                                bool blnPutInDB = false; //入库标识：true 入库，false 不入库
                                strCKIFlightDate = dataRowFlights["cncCKIFlightDate"].ToString().Trim();
                                strCKIFlightNo = dataRowFlights["cnvcCKIFlightNo"].ToString().Trim();
                                strCC = GetCCMessage(strCKIFlightDate, strCKIFlightNo, strDEPSTN); //从中航信系统获取航班截载信息
                                #region 将截载信息入库
                                if (dataRowFlights["cncOutFlightInterceptTime"].ToString().Trim().Length == 4) 
                                {
                                    if (strCC.Trim().Length == 4) 
                                    {
                                        if (dataRowFlights["cncOutFlightInterceptTime"].ToString().Trim() != strCC.Trim())
                                        {
                                            blnPutInDB = true;
                                        }
                                    }
                                }
                                else if (dataRowFlights["cncOutFlightInterceptTime"].ToString().Trim() != strCC.Trim())
                                {
                                    blnPutInDB = true;
                                }

                                //根据 入库标识 进行入库操作
                                if (blnPutInDB)
                                {
                                    OMPWebReference.FlightMonitorData wrFlightMonitorData = new MessageService.OMPWebReference.FlightMonitorData();
                                    bool blnwrFlightMonitorData_MaintainGuaranteeInfor = wrFlightMonitorData.MaintainGuaranteeInfor(
                                        "SYS_TTL", // UserID,
                                        dataRowFlights["cnvcFLTID"].ToString(),   // FLTID,
                                        dataRowFlights["cncDATOP"].ToString(),    // DATOP,
                                        Convert.ToInt32(dataRowFlights["cniLEGNO"].ToString()),    // LegNo,
                                        dataRowFlights["cnvcAC"].ToString(),    // AC,
                                        dataRowFlights["cncDEPSTN"].ToString(),    // DepSTN,
                                        dataRowFlights["cncARRSTN"].ToString(),    // ArrSTN,
                                        dataRowFlights["cncSTD"].ToString(),    // STD,
                                        dataRowFlights["cncETD"].ToString(),    // ETD,
                                        dataRowFlights["cncSTA"].ToString(),    // STA,
                                        dataRowFlights["cncETA"].ToString(),    // ETA,
                                        "cncOutFlightInterceptTime", // ChangeReasonCode,
                                        dataRowFlights["cncOutFlightInterceptTime"].ToString(), // ChangeOldContent,
                                        strCC, // ChangeNewContent,
                                        1, //  FieldType,
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") // LocalOperatingTime
                                        );
                                    if (!blnwrFlightMonitorData_MaintainGuaranteeInfor)
                                        throw new Exception("截载信息维护到航站保障系统失败！" + Environment.NewLine +
                                            "DATOP：" + dataRowFlights["cncDATOP"].ToString() + Environment.NewLine +
                                            "FLTID：" + dataRowFlights["cnvcFLTID"].ToString() + Environment.NewLine +
                                            "LEGNO：" + dataRowFlights["cniLEGNO"].ToString() + Environment.NewLine +
                                            "AC：" + dataRowFlights["cnvcAC"].ToString());
                                }
                                #endregion 将截载信息入库

                            }
                            catch (Exception ex)
                            {
                                blnExceptionMessage = true;
                                strExceptionMessage = ex.Message;
                            }

                            #region 记录到 log表
                            lock (objDataTableLog_CC)
                            {
                                DataRow dataRowLog_CC = dataTableLog_CC.NewRow();
                                dataRowLog_CC["cncDATOP"] = dataRowFlights["cncDATOP"].ToString();
                                dataRowLog_CC["cncCKIFlightDate"] = dataRowFlights["cncCKIFlightDate"].ToString();
                                dataRowLog_CC["cnvcFLTID"] = dataRowFlights["cnvcFLTID"].ToString();
                                dataRowLog_CC["cnvcCKIFlightNo"] = dataRowFlights["cnvcCKIFlightNo"].ToString();
                                dataRowLog_CC["cniLEGNO"] = Convert.ToInt32(dataRowFlights["cniLEGNO"].ToString());
                                dataRowLog_CC["cnvcAC"] = dataRowFlights["cnvcAC"].ToString();
                                dataRowLog_CC["cnvcLONG_REG"] = dataRowFlights["cnvcLONG_REG"].ToString();
                                dataRowLog_CC["cncDEPSTN"] = dataRowFlights["cncDEPSTN"].ToString();
                                dataRowLog_CC["cncARRSTN"] = dataRowFlights["cncARRSTN"].ToString();
                                dataRowLog_CC["cncSTD"] = dataRowFlights["cncSTD"].ToString();
                                dataRowLog_CC["cncETD"] = dataRowFlights["cncETD"].ToString();
                                dataRowLog_CC["cncOutFlightInterceptTime"] = dataRowFlights["cncOutFlightInterceptTime"].ToString();
                                dataRowLog_CC["cnvcCC"] = strCC;
                                dataRowLog_CC["cnvcOperationTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                if (blnExceptionMessage)
                                    dataRowLog_CC["cnvcMemo"] = "错误：" + strExceptionMessage;
                                else
                                    dataRowLog_CC["cnvcMemo"] = "";
                                dataTableLog_CC.Rows.Add(dataRowLog_CC);
                            }
                            #endregion 记录到 log表

                        }
                    }
                }

            }

            //清楚繁忙标记
            blnBusy_CC = false;

        }

        /// <summary>
        /// 从中国民航信息网络股份有限公司（TravelSky Technology Limited,简称“中航信”）系统获取截载信息
        /// </summary>
        /// <param name="CKIFlightDate">航班日期，格式如18JUN15，ddMMMyy格式</param>
        /// <param name="CKIFlightNo">航班号</param>
        /// <param name="CityDEPSTN">起飞机场</param>
        /// <returns>返回截载字符串，如 CC0830 ；返回空字符串，表示还未收到截载信息；返回 ERROR，表示有错误。</returns>
        private string GetCCMessage(string CKIFlightDate, string CKIFlightNo, string CityDEPSTN)
        {
            #region
            //PaxService.PaxService objPaxService = new PaxService.PaxService();
            //string strFlightNo = changeLegsBM.CKIFlightNo.Replace(" ", "").ToUpper();

            //string strCommnadText = "SY ";
            //strCommnadText += strFlightNo + "/";
            //strCommnadText += DateTime.Parse(changeLegsBM.CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "/";
            //strCommnadText += changeLegsBM.CityDEPSTN + ",Z";

            ////string strResult = objPaxService.PaxCheckNum(strCommnadText);
            //string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");

            //if (strResult.Length < 30 || strResult == "Disconncted,Please connect again")
            //{
            //    return new CheckPaxBM();
            //}
            //else
            //{
            //    return GetCheckPaxInfoFromTheText(strResult, changeLegsBM.CityDEPSTN, changeLegsBM.CityARRSTN);
            //}
            #endregion 

            try
            {
                PaxService.PaxService objPaxService = new PaxService.PaxService();
                string strFlightNo = CKIFlightNo.Replace(" ", "").ToUpper();

                string strCommnadText = "SY ";
                strCommnadText += strFlightNo + "/";
                strCommnadText += DateTime.Parse(CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "/";
                strCommnadText += CityDEPSTN + ",Z";

                string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");

                //
                if (strResult.Length < 30 || strResult == "Disconncted,Please connect again")
                {
                    return "ERR";
                }

                string[] arrayResult = strResult.Split('\n');
                if (arrayResult.Length < 3)
                {
                    return "ERR";
                }

                //
                string strResult_1 = arrayResult[0].ToString().Trim();
                string[] arrayResult_1 = strResult_1.Split(' ');

                if (arrayResult_1.Length == 0)
                {
                    return "ERR";
                }

                string strResult_1_l = arrayResult_1[arrayResult_1.Length - 1].ToString().Trim();
                string[] arrayResult_1_l = strResult_1_l.Split('/');

                if (arrayResult_1_l.Length != 2)
                {
                    return "ERR";
                }

                string strResult_1_l_1 = arrayResult_1_l[0].ToString().Trim();
                if (strResult_1_l_1.Length != 6)
                {
                    return "";
                }
                else if (strResult_1_l_1.Trim().Substring(0, 2).ToUpper() != "CL")
                {
                    return ""; 
                }
                else
                {
                    return strResult_1_l_1.Trim().Substring(2, 4); //需要的结果，如 0830
                }

            }
            catch (Exception ex)
            {
                return "ERR";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region 保障告警部分对象初始化
            //提取航站过站时间信息
            OverStationTimeBF overStationTimeBF = new OverStationTimeBF();
            ReturnValueSF rvsfOverStationTime = overStationTimeBF.Select();
            if ((rvsfOverStationTime.Result > 0) && (rvsfOverStationTime.Dt != null))
            {
                _dtOverStationTime = rvsfOverStationTime.Dt;
            }
            else
            {
                MessageBox.Show("从数据库中提取航站过站时间信息失败！" +
                    Environment.NewLine + "错误信息：" +
                    rvsfOverStationTime.Message
                    , "提示", MessageBoxButtons.OK);

                Environment.Exit(0); //退出程序
            }

            //提取航站滑行时间信息
            StationBF stationBF = new StationBF();
            ReturnValueSF rvsfStationBF = stationBF.GetAllStation();
            if ((rvsfStationBF.Result > 0) && (rvsfStationBF.Dt != null))
            {
                _dtStationInfor = rvsfStationBF.Dt;
            }
            else
            {
                MessageBox.Show("从数据库中提取航站滑行时间信息失败！" +
                    Environment.NewLine + "错误信息：" +
                    rvsfStationBF.Message
                    , "提示", MessageBoxButtons.OK);

                Environment.Exit(0); //退出程序
            }



            //昨天、今日和明天的航班组合信息
            FlightAlarmInfoBF flightAlarmInfoBF = new FlightAlarmInfoBF();
            ReturnValueSF rvsfFlightAlarmInfo = flightAlarmInfoBF.Select(
                DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            if ((rvsfFlightAlarmInfo.Result > 0) && (rvsfFlightAlarmInfo.Dt != null))
            {
                _dtTodayInOutFlights = rvsfFlightAlarmInfo.Dt;

                //设置主键
                _dtTodayInOutFlights.PrimaryKey = new DataColumn[] {
                    _dtTodayInOutFlights.Columns["cncOutDATOP"], 
                    _dtTodayInOutFlights.Columns["cnvcOutFLTID"],
                    _dtTodayInOutFlights.Columns["cniOutLEGNO"], 
                    _dtTodayInOutFlights.Columns["cnvcOutAC"],
                    _dtTodayInOutFlights.Columns["cncInDATOP"], 
                    _dtTodayInOutFlights.Columns["cnvcInFLTID"],
                    _dtTodayInOutFlights.Columns["cniInLEGNO"], 
                    _dtTodayInOutFlights.Columns["cnvcInAC"],
                    _dtTodayInOutFlights.Columns["cnvcAlarmCode"]
               };
            }
            else
            {
                MessageBox.Show("从数据库中提取航班告警信息失败！" +
                    Environment.NewLine + "错误信息：" +
                    rvsfFlightAlarmInfo.Message
                    , "提示", MessageBoxButtons.OK);

                Environment.Exit(0); //退出程序
            }
            #endregion 保障告警部分对象初始化

            //显示 Log 页面
            tabControl1.SelectedTab = tabControl1.TabPages[2];

            #region 代理服务对象
            if (!blnSetRemotingObject) //还未设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("代理服务对象获取失败，请重新登录！", "提示", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            }
            #endregion 代理服务对象

            //初始化 已处理了的进出港航班的最后的操作时间
            _arrayDateTime[0] = DateTime.Now;

            #region 针对需要处理的机场列表，调用线程定时器，定时处理航班告警信息
            //string strAirportList = "HAK;CAN;PEK;PVG;XIY;TYN;TSN";
            //string strAirportList = "HAK;CAN;PEK;PVG;XIY;SHA,DLC,TYN";
            string strAirportList = ConfigurationSettings.AppSettings["AirportList"].ToString().Trim();
            string[] arrayAirportList = strAirportList.Split(';');
            for (int iIndexAirportList = 0; iIndexAirportList < arrayAirportList.Length; iIndexAirportList++)
            {
                //机场三字码
                string strAirport = arrayAirportList[iIndexAirportList];

                //初始化参数
                StationBM stationBM = new StationBM();
                AccountBM accountBM = new AccountBM();

                stationBM.ThreeCode = strAirport;
                accountBM.UserId = "SYS_FlightAlarm"; //测试的时候用的帐号是 l_w
                accountBM.UserName = "告警运算(系统)";
                accountBM.IPAddress = "";

                //获取用户有权限的数据项
                DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
                DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

                //初始化 进出港航班表 和 操作同步对象
                /*
                _arrayTodayInOutFlights[iIndexAirportList + 1] = _dtTodayInOutFlights.Copy();
                _dtTodayInOutFlights = _arrayTodayInOutFlights[iIndexAirportList + 1];

                _arrayObjTodayInOutFlights[iIndexAirportList + 1] = new object();
                _objTodayInOutFlights = _arrayObjTodayInOutFlights[iIndexAirportList + 1];
                */

                //线程执行函数参数的初始化
                object[] objectsList = new object[9];
                objectsList[0] = _dtTodayInOutFlights; //进出港航班内存表
                objectsList[1] = _dtOverStationTime; //过站标准时间内存表
                objectsList[2] = stationBM; //航站对象
                objectsList[3] = accountBM; //用户对象
                objectsList[4] = _arrayBusy; //繁忙标记数组
                objectsList[5] = iIndexAirportList + 1; //使用到的 繁忙标记数组 中的位置
                objectsList[6] = _objTodayInOutFlights; //对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象
                objectsList[7] = dtDataItems; //用户有权限的数据项
                objectsList[8] = _dtStationInfor; //滑行时间内存表

                ////调用 处理航班告警信息函数
                //DealFlightAlarmInfo(objectsList);

                //调用线程定时器，定时 处理航班告警信息
                int iRefreshInterval = 120 * 1000; //调用频率设置为 2分钟
                TimerCallback timerDelegate = new TimerCallback(DealFlightAlarmInfo);
                _arrayTimer[iIndexAirportList + 1] = new System.Threading.Timer(timerDelegate, objectsList, 0, iRefreshInterval); //存储在线程定时器对象数组列表位置 1 以后
            }
            #endregion 针对需要处理的机场列表，调用线程定时器，定时处理航班告警信息

            #region 调用线程定时器，定时 处理航班告警信息
            //线程执行函数参数的初始化
            object[] arrayObjectList = new object[6];
            arrayObjectList[0] = _dtTodayInOutFlights; //进出港航班内存表(_dtTodayInOutFlights 数据表)
            arrayObjectList[1] = _arrayDateTime; //已处理了的进出港航班的最后的操作时间，时间数组
            arrayObjectList[2] = 0; //已处理了的进出港航班的最后的操作时间，位置
            arrayObjectList[3] = _arrayBusy; //繁忙标记数组
            arrayObjectList[4] = 0; //使用到的 繁忙标记数组 中的位置
            arrayObjectList[5] = _objTodayInOutFlights; //对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象

            //调用线程定时器，定时 处理航班告警信息
            int iRefreshInterval_0 = 60 * 1000; //调用频率设置为 1 分钟 -- 临时设置，调试使用
            TimerCallback timerDelegate_0 = new TimerCallback(DealFlightAlarmInfo_PutInDB);
            _arrayTimer[0] = new System.Threading.Timer(timerDelegate_0, arrayObjectList, 0, iRefreshInterval_0); //存储在线程定时器对象数组列表位置 0
            #endregion 调用线程定时器，定时 处理航班告警信息

            //
            timer1.Enabled = true;
            button3.Enabled = false;
        }


        #region 航班保障告警分析 用到的函数

        #region 告警数据处理（入库等）
        /// <summary>
        /// 告警数据处理（入库等）
        /// </summary>
        /// <param name="parameterList">参数列表：0 进出港航班内存表；1 已处理了的进出港航班的最后的操作时间；2 繁忙标记数组；3 使用到的 繁忙标记数组 中的位置</param>
        private void DealFlightAlarmInfo_PutInDB(object parameterList)
        {
            //参数列表
            object[] objectsList = (object[])parameterList;
            DataTable todayInOutFlights_Mem = (DataTable)(objectsList[0]); //进出港航班内存表(_dtTodayInOutFlights 数据表)
            DateTime[] arrayDateTime = (DateTime[])(objectsList[1]); //已处理了的进出港航班的最后的操作时间，时间数组
            int iIndexDateTime = (int)(objectsList[2]); ; //已处理了的进出港航班的最后的操作时间，位置
            string strOperationTime = arrayDateTime[iIndexDateTime].ToString("yyyy-MM-dd HH:mm:ss.fffffff"); //已处理了的进出港航班的最后的操作时间
            bool[] arrayBusy = (bool[])(objectsList[3]); //繁忙标记数组
            int iIndexBusy = (int)(objectsList[4]); //使用到的 繁忙标记数组 中的位置
            object objTodayInOutFlights = (object)(objectsList[5]);  //对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象


            //是否繁忙
            if (arrayBusy[iIndexBusy])
                return; //如果繁忙，则退出
            arrayBusy[iIndexBusy] = true; //设置 繁忙 标记

            #region 数据处理（入库等）
            DataRow[] arrsyDataRowsTodayInOutFlights_Mem = null;
            lock (objTodayInOutFlights) //选择条件中的 >= 是否应该为 > ？ 已改为 >
            {
                arrsyDataRowsTodayInOutFlights_Mem = todayInOutFlights_Mem.Select("cndOperationTime > '" + strOperationTime + "'", "cndOperationTime"); //提取还未处理的记录
            }

            int iTraceInfo_1 = arrsyDataRowsTodayInOutFlights_Mem.Length; //跟踪信息，供分析使用，表示提取的记录数量
            int iTraceInfo_2 = 1; //跟踪信息，供分析使用，表示记录在记录集中位置
            foreach (DataRow dataRowTodayInOutFlights_Mem in arrsyDataRowsTodayInOutFlights_Mem) //遍历提取到的还未处理的记录
            {
                string strTraceInfo = ""; //跟踪信息，供分析使用

                strTraceInfo = strTraceInfo + "[" + iTraceInfo_2.ToString() + @"/" + iTraceInfo_1.ToString() + "]";
                strTraceInfo = strTraceInfo + "Gao：" + DateTime.Now.ToString("mm:ss.fffffff") + "；" ;

                FlightAlarmInfoBM flightAlarmInfoBM = new FlightAlarmInfoBM(dataRowTodayInOutFlights_Mem); //生成告警对象

                #region 入库
                strTraceInfo = strTraceInfo + "Ti：" + DateTime.Now.ToString("mm:ss.fffffff") + "；";

                FlightAlarmInfoBF flightAlarmInfoBF = new FlightAlarmInfoBF();
                ReturnValueSF rvsfFlightAlarmInfoBF = flightAlarmInfoBF.Select(
                    flightAlarmInfoBM.cncOutDATOP,
                    flightAlarmInfoBM.cnvcOutFLTID,
                    flightAlarmInfoBM.cniOutLEGNO,
                    flightAlarmInfoBM.cnvcOutAC,
                    flightAlarmInfoBM.cncInDATOP,
                    flightAlarmInfoBM.cnvcInFLTID,
                    flightAlarmInfoBM.cniInLEGNO,
                    flightAlarmInfoBM.cnvcInAC,
                    flightAlarmInfoBM.cnvcAlarmCode
                ); //根据关键信息提取数据库中数据，判断数据是否已存在

                strTraceInfo = strTraceInfo + "Ru：" + DateTime.Now.ToString("mm:ss.fffffff") + "；";

                if ((rvsfFlightAlarmInfoBF.Result > 0) &&
                    (rvsfFlightAlarmInfoBF.Dt.Rows.Count == 0)) //无此记录，增加
                {
                    ReturnValueSF rvsfFlightAlarmInfoBF_Add = flightAlarmInfoBF.Add(flightAlarmInfoBM);
                    #region 记录增加操作结果
                    if (rvsfFlightAlarmInfoBF_Add.Result == 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "成功：增加";
                    }
                    else if (rvsfFlightAlarmInfoBF_Add.Result > 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "失败：增加记录数量大于1";
                    }
                    else if (rvsfFlightAlarmInfoBF_Add.Result == 0)
                    {
                        flightAlarmInfoBM.cnvcMemo = "失败：增加记录数量等于0";
                    }
                    else
                    {
                        flightAlarmInfoBM.cnvcMemo = "失败：增加：" + rvsfFlightAlarmInfoBF_Add.Message;
                    }
                    #endregion 记录增加操作结果
                }
                else if ((rvsfFlightAlarmInfoBF.Result > 0) &&
                    (rvsfFlightAlarmInfoBF.Dt.Rows.Count == 1)) //有此记录，更新
                {
                    ReturnValueSF rvsfFlightAlarmInfoBF_Update = flightAlarmInfoBF.Update(flightAlarmInfoBM);
                    #region 记录增加更新结果
                    if (rvsfFlightAlarmInfoBF_Update.Result == 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "成功：更新";
                    }
                    else if (rvsfFlightAlarmInfoBF_Update.Result > 1)
                    {
                        flightAlarmInfoBM.cnvcMemo = "失败：更新记录数量大于1";
                    }
                    else if (rvsfFlightAlarmInfoBF_Update.Result == 0)
                    {
                        flightAlarmInfoBM.cnvcMemo = "失败：更新记录数量等于0";
                    }
                    else
                    {
                        flightAlarmInfoBM.cnvcMemo = "失败：更新：" + rvsfFlightAlarmInfoBF_Update.Message;
                    }
                    #endregion 记录增加更新结果
                }
                else //其他情况的处理
                {
                    #region 记录其他情况
                    flightAlarmInfoBM.cnvcMemo = "失败：选择：" + rvsfFlightAlarmInfoBF.Message;
                    #endregion 记录其他情况
                }
                #endregion 入库

                strTraceInfo = strTraceInfo + "End：" + DateTime.Now.ToString("mm:ss.fffffff") + "；";

                #region 跟踪发现，此同步需花费 1秒钟 时间，在处理大批量数据时，导致整个过程耗时较久 -- 已注释处理此部分代码
                /*
                #region 将操作结果更新内存表（memo字段 和 cndPutInDBTime字段） --modified by LinYong in 20160601
                //跟踪发现，此同步需花费 1秒钟 时间，在处理大批量数据时，导致整个过程耗时较久
                lock (objTodayInOutFlights)
                {
                    dataRowTodayInOutFlights_Mem["cnvcMemo"] = flightAlarmInfoBM.cnvcMemo + "【" + strTraceInfo + "】";
                    dataRowTodayInOutFlights_Mem["cndPutInDBTime"] = DateTime.Now;
                }
                #endregion 将操作结果更新内存表（memo字段）
                */
                #endregion 跟踪发现，此同步需花费 1秒钟 时间，在处理大批量数据时，导致整个过程耗时较久 -- 已注释处理此部分代码

                #region 记录入库操作记录的最后的操作时间，存储在内存时间数组中，供下次调用使用
                arrayDateTime[iIndexDateTime] = flightAlarmInfoBM.cndOperationTime;
                #endregion 记录入库操作记录的最后的操作时间，存储在内存时间数组中，供下次调用使用

                iTraceInfo_2 = iTraceInfo_2 + 1;
            }
            #endregion 数据处理（入库等）

            //是否繁忙标记设置
            arrayBusy[iIndexBusy] = false; //设置 不繁忙 标记
        }
        #endregion 告警数据处理（入库等）

        #region 处理航班告警信息 -- 提供线程使用 -- 不使用

        ///// <summary>
        ///// 处理航班告警信息 -- 提供线程使用
        ///// </summary>
        ///// <param name="parameterList">参数列表：进出港航班内存表、过站标准时间内存表航站对象、用户对象</param>
        //private void DealFlightAlarmInfo_Bak20150819(object parameterList)
        //{
        //    //参数列表
        //    object[] objectsList = (object[])parameterList;
        //    DataTable todayInOutFlights = (DataTable)(objectsList[0]); //进出港航班内存表
        //    DataTable overStationTime = (DataTable)(objectsList[1]); //过站标准时间内存表
        //    StationBM stationBM = (StationBM)(objectsList[2]); //航站对象
        //    AccountBM accountBM = (AccountBM)(objectsList[3]); //用户对象
        //    bool[] arrayBusy = (bool[])(objectsList[4]); //繁忙标记数组
        //    int iIndexBusy = (int)(objectsList[5]); //使用到的 繁忙标记数组 中的位置
        //    object objTodayInOutFlights = (object)(objectsList[6]);  //对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象
        //    DataTable dtDataItems = (DataTable)(objectsList[7]); //用户有权限的数据项



        //    //是否繁忙
        //    if (arrayBusy[iIndexBusy])
        //        return; //如果繁忙，则退出
        //    arrayBusy[iIndexBusy] = true; //设置 繁忙 标记

        //    //获取该航站当天的所有航班
        //    GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
        //    DateTimeBM dateTimeBM = new DateTimeBM();
        //    if ((DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00")) &&
        //        (DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00")))
        //    {
        //        dateTimeBM.StartDateTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 05:00:00";
        //        dateTimeBM.EndDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
        //    }
        //    else
        //    {
        //        dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
        //        dateTimeBM.EndDateTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
        //    } //可以考虑下更合理的时间区间：进行ddateTimeBM.StartDateTime和dateTimeBM.EndDateTime赋值的时间区间，以及dateTimeBM.StartDateTime和dateTimeBM.EndDateTime应该分别赋值多少

        //    ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
        //    DataTable dtTodayStationFlights = rvSF.Dt;

        //    //获取用户有权限的数据项
        //    //DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
        //    //DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

        //    //进出港航班表格Schema
        //    DataTable dtInOutFlightsSchema = GetDisplaySchema(dtDataItems);

        //    //获取当天进出港航班 
        //    //DataTable dtTodayInOutFlights = FillInOutFlights(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);
        //    IList ilTodayInOutFlights = FillInOutFlights_1(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);

        //    //绑定数据
        //    //DataView dataView  = new DataView(dtTodayInOutFlights, "", "cniRowIndex", DataViewRowState.CurrentRows);
        //    //dataGridView3.DataSource = dataView;
        //    //dataGridView3.DataSource = ilTodayInOutFlights;

        //    #region 遍历进出港航班表
        //    IEnumerator ieTodayInOutFlights = ilTodayInOutFlights.GetEnumerator();
        //    while (ieTodayInOutFlights.MoveNext())
        //    {
        //        string strInDEPSTN = "";
        //        string strInARRSTN = "";
        //        string strOutDEPSTN = "";
        //        string strOutARRSTN = "";
        //        string strOverStationType = ""; //过站类型：始发、过站、快速过站、航后
        //        int iOverStationStandardTime = 0; //过站标准时间（分钟）
        //        string strOverStationStart = ""; //开始时刻，即 计算到港时刻
        //        string strOverStationEnd = ""; //结束时刻，即 计算离港时刻
        //        string strAlarmCode = ""; //告警代码
        //        string strAlarmValue = ""; //告警值
        //        int iAlarmResult = 0; //告警结果


        //        try
        //        {
        //            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current; //实例化当前进出港航班条

        //            #region 人工模拟部分参数
        //            //iOverStationStandardTime = 45; //模拟 TYN 的过站时间，应该结合 小机型 动态设置
        //            //strAlarmCode = "OutcncOutPilotArriveTime";
        //            #endregion 人工模拟部分参数

        //            #region 确定航班起落机场三字码
        //            if (guaranteeInforBM.IncncDATOP != "1900-01-01") //进港航班
        //            {
        //                DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
        //                    "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
        //                    "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
        //                    "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

        //                if (dataRowFlight.Length > 0)
        //                {
        //                    strInDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
        //                    strInARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
        //                }
        //            }

        //            if (guaranteeInforBM.OutcncDATOP != "1900-01-01") //出港航班
        //            {
        //                DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
        //                   "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
        //                   "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
        //                   "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

        //                if (dataRowFlight.Length > 0)
        //                {
        //                    strOutDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
        //                    strOutARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();

        //                }
        //            }
        //            #endregion 确定航班起落机场三字码

        //            #region 确定过站时间
        //            DataRow[] dataRowsOverStationTime = overStationTime.Select(
        //                "(cncAirportThreeCode = '" +
        //                stationBM.ThreeCode + "') and (cnvcSmallACTYP = '" +
        //                guaranteeInforBM.IncncACTYP + "')"); //根据机场三字码和小机型获取过站时间数据
        //            if (dataRowsOverStationTime.Length > 0)
        //            {
        //                iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniGroundTime"].ToString());
        //            }
        //            else
        //            {
        //                throw new Exception("过站时间表中没有此记录：" + Environment.NewLine +
        //                    "机场：" + stationBM.ThreeCode + Environment.NewLine +
        //                    "小机型：" + guaranteeInforBM.IncncACTYP);
        //            }
        //            #endregion 确定过站时间

        //            #region 确定过站类型
        //            if ((guaranteeInforBM.IncncDATOP == "1900-01-01") &&
        //                (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
        //            {
        //                strOverStationType = "始发";
        //            }
        //            else if ((guaranteeInforBM.IncncDATOP != "1900-01-01") &&
        //                (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
        //            {
        //                strOverStationType = "过站";
        //            }
        //            else
        //            {
        //                strOverStationType = "航后";
        //            }
        //            #endregion 确定过站类型

        //            #region 确定 开始时刻 和 结束时刻，即 确定 计算离港时刻 和 计算到港时刻
        //            if ((strOverStationType == "过站") ||
        //                (strOverStationType == "快速过站"))
        //            {
        //                //计算 开始时刻
        //                if (guaranteeInforBM.IncncAllStatus != "ATA")
        //                {
        //                    strOverStationStart = guaranteeInforBM.IncncAllETA;
        //                }
        //                else
        //                {
        //                    strOverStationStart = guaranteeInforBM.IncncAllATA;
        //                }
        //                //计算 结束时刻
        //                if (Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime) > Convert.ToDateTime(guaranteeInforBM.OutcncAllETD))
        //                {
        //                    strOverStationEnd = Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime).ToString("yyyy-MM-dd HH:mm:ss");
        //                }
        //                else
        //                {
        //                    strOverStationEnd = guaranteeInforBM.OutcncAllETD;
        //                }

        //            }
        //            else if (strOverStationType == "始发")
        //            {
        //                //计算 开始时刻
        //                strOverStationStart = "";

        //                //计算 结束时刻
        //                strOverStationEnd = guaranteeInforBM.OutcncAllETD;
        //            }
        //            else //航后航班不处理
        //            {
        //                continue;
        //            }
        //            #endregion 确定 开始时刻 和 结束时刻，即 确定 计算离港时刻 和 计算到港时刻

        //            #region 空勤组到位及时性 判断 使用了字段 OutcncOutCrewArriveTime -- 此部分代码不使用
        //            /*
        //            #region 空勤组到位及时性 判断
        //            guaranteeInforBM.OutcncOutCrewArriveTime
        //            if (strAlarmCode == "OutcncOutPilotArriveTime")
        //            {
        //                DateTime dOutPilotArriveTime = new DateTime(); //确定机组应该到位的时刻
        //                if ((strOverStationType == "过站") ||
        //                    (strOverStationType == "快速过站"))
        //                {

        //                    //确定机组应该到位的时刻
        //                    if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
        //                    {
        //                        dOutPilotArriveTime = Convert.ToDateTime(strOverStationStart);
        //                    }
        //                    else
        //                    {
        //                        dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                    }
        //                }
        //                else if (strOverStationType == "始发")
        //                {
        //                    dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                }
        //                //告警值
        //                strAlarmValue = guaranteeInforBM.OutcncOutPilotArriveTime;

        //                //告警结果 判断（机组应该到位的时刻 往前 5分钟 开始判断）
        //                if (DateTime.Now < dOutPilotArriveTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //还未到判断时间
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncOutPilotArriveTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncOutPilotArriveTime 还未录入数据
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncOutPilotArriveTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(2, 2) +
        //                            ":00"); //机组实际到位时刻
        //                        if (dOutcncOutPilotArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
        //                        {
        //                            dOutcncOutPilotArriveTime.AddDays(-1);
        //                        }

        //                        if (dOutcncOutPilotArriveTime > dOutPilotArriveTime)
        //                        {
        //                            iAlarmResult = 2; //晚到
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //准时 
        //                        }
        //                    }
        //                }
        //                //处理内存表 _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    iAlarmResult.ToString());
        //            }
        //            #endregion 空勤组到位及时性 判断
        //            */
        //            #endregion 空勤组到位及时性 判断 使用了字段 OutcncOutCrewArriveTime -- 此部分代码不使用

        //            #region 空勤组到位及时性 判断
        //            strAlarmCode = "OutcncOutCrewArriveTime";

        //            if (strAlarmCode == "OutcncOutCrewArriveTime")
        //            {
        //                DateTime dOutCrewArriveTime = new DateTime(); //确定机组应该到位的时刻
        //                if ((strOverStationType == "过站") ||
        //                    (strOverStationType == "快速过站"))
        //                {

        //                    //确定机组应该到位的时刻
        //                    if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
        //                    {
        //                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationStart);
        //                    }
        //                    else
        //                    {
        //                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                    }
        //                }
        //                else if (strOverStationType == "始发")
        //                {
        //                    dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
        //                }
        //                //告警值
        //                strAlarmValue = guaranteeInforBM.OutcncOutCrewArriveTime;

        //                //告警结果 判断（机组应该到位的时刻 往前 5分钟 开始判断）
        //                if (DateTime.Now < dOutCrewArriveTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //还未到判断时间
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncOutCrewArriveTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncOutCrewArriveTime 还未录入数据
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncOutCrewArriveTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(2, 2) +
        //                            ":00"); //机组实际到位时刻
        //                        if (dOutcncOutCrewArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
        //                        {
        //                            dOutcncOutCrewArriveTime.AddDays(-1);
        //                        }

        //                        if (dOutcncOutCrewArriveTime > dOutCrewArriveTime)
        //                        {
        //                            iAlarmResult = 2; //晚到
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //准时 
        //                        }
        //                    }
        //                }
        //                //处理内存表 _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dOutCrewArriveTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion 空勤组到位及时性 判断

        //            #region 机务放行及时性 判断
        //            strAlarmCode = "OutcncMCCReleaseTime";

        //            if (strAlarmCode == "OutcncMCCReleaseTime")
        //            {
        //                DateTime dMCCReleaseTime = new DateTime(); //机务应该最晚放行的时刻

        //                //确定机务应该最晚放行的时刻
        //                if ((strOverStationType == "过站") ||
        //                    (strOverStationType == "快速过站"))
        //                {
        //                    //确定机务应该最晚放行的时刻
        //                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737机型
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-21);
        //                    }
        //                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200机型
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
        //                    }
        //                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300机型
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }
        //                    else //其他情况，按照最严的要求设置
        //                    {
        //                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }

        //                }
        //                else if (strOverStationType == "始发")
        //                {
        //                    dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-45);
        //                }

        //                //告警值
        //                strAlarmValue = guaranteeInforBM.OutcncMCCReleaseTime;

        //                //告警结果 判断（机务应该最晚放行的时刻 往前 5分钟 开始判断）
        //                if (DateTime.Now < dMCCReleaseTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //还未到判断时间
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncMCCReleaseTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncMCCReleaseTime 还未录入数据
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncMCCReleaseTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(2, 2) +
        //                            ":00"); //机务放行时刻
        //                        if (dOutcncMCCReleaseTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
        //                        {
        //                            dOutcncMCCReleaseTime.AddDays(-1);
        //                        }

        //                        if (dOutcncMCCReleaseTime > dMCCReleaseTime)
        //                        {
        //                            iAlarmResult = 2; //晚放行
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //准时放行 
        //                        }
        //                    }
        //                }

        //                //处理内存表 _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dMCCReleaseTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion 机务放行及时性 判断  

        //            #region 飞机准备完毕及时性 判断
        //            strAlarmCode = "OutcncOutPlaneReadyEndTime";

        //            if (strAlarmCode == "OutcncOutPlaneReadyEndTime")
        //            {
        //                DateTime dOutPlaneReadyEndTime = new DateTime(); //飞机应该最晚准备完毕的时刻

        //                //确定飞机应该最晚准备完毕的时刻
        //                if ((strOverStationType == "过站") ||
        //                    (strOverStationType == "快速过站"))
        //                {
        //                    //确定机务应该最晚放行的时刻
        //                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737机型
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
        //                    }
        //                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200机型
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
        //                    }
        //                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300机型
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }
        //                    else //其他情况，按照最严的要求设置
        //                    {
        //                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }

        //                }
        //                else if (strOverStationType == "始发")
        //                {
        //                    dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
        //                }

        //                //告警值
        //                strAlarmValue = guaranteeInforBM.OutcncOutPlaneReadyEndTime;

        //                //告警结果 判断（飞机应该最晚准备完毕的时刻 往前 5分钟 开始判断）
        //                if (DateTime.Now < dOutPlaneReadyEndTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //还未到判断时间
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncOutPlaneReadyEndTime 还未录入数据
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncOutPlaneReadyEndTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(2, 2) +
        //                            ":00"); //准备完毕时刻
        //                        if (dOutcncOutPlaneReadyEndTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
        //                        {
        //                            dOutcncOutPlaneReadyEndTime.AddDays(-1);
        //                        }

        //                        if (dOutcncOutPlaneReadyEndTime > dOutPlaneReadyEndTime)
        //                        {
        //                            iAlarmResult = 2; //准备完毕 -- 晚点
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //准备完毕 -- 准时 
        //                        }
        //                    }
        //                }

        //                //处理内存表 _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dOutPlaneReadyEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion 飞机准备完毕及时性 判断

        //            #region 通知上客及时性 判断
        //            strAlarmCode = "OutcncInformBoardTime";

        //            if (strAlarmCode == "OutcncInformBoardTime")
        //            {
        //                DateTime dInformBoardTime = new DateTime(); //最晚通知上客的时刻

        //                //确定最晚通知上客的时刻
        //                if ((strOverStationType == "过站") ||
        //                    (strOverStationType == "快速过站"))
        //                {
        //                    //确定最晚通知上客的时刻
        //                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737机型
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
        //                    }
        //                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200机型
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
        //                    }
        //                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300机型
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }
        //                    else //其他情况，按照最严的要求设置
        //                    {
        //                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
        //                    }

        //                }
        //                else if (strOverStationType == "始发")
        //                {
        //                    dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
        //                }

        //                //告警值
        //                strAlarmValue = guaranteeInforBM.OutcncInformBoardTime;

        //                //告警结果 判断（最晚通知上客的时刻 往前 5分钟 开始判断）
        //                if (DateTime.Now < dInformBoardTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //还未到判断时间
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncInformBoardTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncInformBoardTime 还未录入数据
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncInformBoardTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(2, 2) +
        //                            ":00"); //通知上客时刻
        //                        if (dOutcncInformBoardTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
        //                        {
        //                            dOutcncInformBoardTime.AddDays(-1);
        //                        }

        //                        if (dOutcncInformBoardTime > dInformBoardTime)
        //                        {
        //                            iAlarmResult = 2; //通知上客 -- 晚点
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //通知上客 -- 准时 
        //                        }
        //                    }
        //                }

        //                //处理内存表 _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dInformBoardTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion 通知上客及时性 判断

        //            #region 客舱关闭及时性 判断
        //            strAlarmCode = "OutcncClosePaxCabinTime";

        //            if (strAlarmCode == "OutcncClosePaxCabinTime")
        //            {
        //                DateTime dClosePaxCabinTime = new DateTime(); //最晚客舱关闭时刻

        //                //确定最晚客舱关闭时刻
        //                if ((strOverStationType == "过站") ||
        //                    (strOverStationType == "快速过站"))
        //                {
        //                    //确定最晚客舱关闭时刻
        //                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }
        //                else if (strOverStationType == "始发")
        //                {
        //                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }

        //                //告警值
        //                strAlarmValue = guaranteeInforBM.OutcncClosePaxCabinTime;

        //                //告警结果 判断（最晚客舱关闭时刻 往前 5分钟 开始判断）
        //                if (DateTime.Now < dClosePaxCabinTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //还未到判断时间
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncClosePaxCabinTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncClosePaxCabinTime 还未录入数据
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncClosePaxCabinTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(2, 2) +
        //                            ":00"); //客舱关闭时刻
        //                        if (dOutcncClosePaxCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
        //                        {
        //                            dOutcncClosePaxCabinTime.AddDays(-1);
        //                        }

        //                        if (dOutcncClosePaxCabinTime > dClosePaxCabinTime)
        //                        {
        //                            iAlarmResult = 2; //客舱关闭 -- 晚点
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //客舱关闭 -- 准时 
        //                        }
        //                    }
        //                }

        //                //处理内存表 _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dClosePaxCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion 客舱关闭及时性 判断

        //            #region 货舱关闭及时性 判断
        //            strAlarmCode = "OutcncCloseCargoCabinTime";

        //            if (strAlarmCode == "OutcncCloseCargoCabinTime")
        //            {
        //                DateTime dCloseCargoCabinTime = new DateTime(); //最晚货舱关闭时刻

        //                //确定最晚货舱关闭时刻
        //                if ((strOverStationType == "过站") ||
        //                    (strOverStationType == "快速过站"))
        //                {
        //                    //确定最晚货舱关闭时刻
        //                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }
        //                else if (strOverStationType == "始发")
        //                {
        //                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
        //                }

        //                //告警值
        //                strAlarmValue = guaranteeInforBM.OutcncCloseCargoCabinTime;

        //                //告警结果 判断（最晚货舱关闭时刻 往前 5分钟 开始判断）
        //                if (DateTime.Now < dCloseCargoCabinTime.AddMinutes(-5)) //
        //                {
        //                    iAlarmResult = -1; //还未到判断时间
        //                }
        //                else
        //                {
        //                    if (guaranteeInforBM.OutcncCloseCargoCabinTime.Trim() == "")
        //                    {
        //                        iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncCloseCargoCabinTime 还未录入数据
        //                    }
        //                    else
        //                    {
        //                        DateTime dOutcncCloseCargoCabinTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
        //                            guaranteeInforBM.OutcncFlightDate +
        //                            " " +
        //                            guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(0, 2) +
        //                            ":" +
        //                            guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(2, 2) +
        //                            ":00"); //货舱关闭时刻
        //                        if (dOutcncCloseCargoCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
        //                        {
        //                            dOutcncCloseCargoCabinTime.AddDays(-1);
        //                        }

        //                        if (dOutcncCloseCargoCabinTime > dCloseCargoCabinTime)
        //                        {
        //                            iAlarmResult = 2; //货舱关闭 -- 晚点
        //                        }
        //                        else
        //                        {
        //                            iAlarmResult = 0; //货舱关闭 -- 准时 
        //                        }
        //                    }
        //                }

        //                //处理内存表 _dtTodayInOutFlights
        //                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
        //                    todayInOutFlights,
        //                    guaranteeInforBM,
        //                    strInDEPSTN,
        //                    strInARRSTN,
        //                    strOutDEPSTN,
        //                    strOutARRSTN,
        //                    strOverStationType,
        //                    iOverStationStandardTime.ToString(),
        //                    strOverStationStart,
        //                    strOverStationEnd,
        //                    strAlarmCode,
        //                    strAlarmValue,
        //                    dCloseCargoCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //                    iAlarmResult.ToString(),
        //                    objTodayInOutFlights);
        //            }
        //            #endregion 货舱关闭及时性 判断

        //        }
        //        catch (Exception ex)
        //        {
        //            string strExceptionMessage = ex.Message;
        //        }
        //    }
        //    #endregion 遍历进出港航班表

        //    //dataGridView4.DataSource = todayInOutFlights;

        //    //是否繁忙标记设置
        //    arrayBusy[iIndexBusy] = false; //设置 不繁忙 标记

        //}

        #endregion 处理航班告警信息 -- 提供线程使用 -- 不使用

        #region 处理航班告警信息 -- 提供线程使用，可以在一个线程中处理多个机场航班数据 -- 需要把里头的数据网格绑定代码部分去除才能放在线程中使用
        /// <summary>
        /// 处理航班告警信息 -- 提供线程使用
        /// </summary>
        /// <param name="parameterList">参数列表：进出港航班内存表、过站标准时间内存表航站对象、用户对象</param>
        private void DealFlightAlarmInfo(object parameterList)
        {
            //参数列表
            object[] objectsList = (object[])parameterList;
            DataTable todayInOutFlights = (DataTable)(objectsList[0]); //进出港航班内存表
            DataTable overStationTime = (DataTable)(objectsList[1]); //过站标准时间内存表
            StationBM stationBM_Parameter = (StationBM)(objectsList[2]); //航站对象
            AccountBM accountBM = (AccountBM)(objectsList[3]); //用户对象
            bool[] arrayBusy = (bool[])(objectsList[4]); //繁忙标记数组
            int iIndexBusy = (int)(objectsList[5]); //使用到的 繁忙标记数组 中的位置
            object objTodayInOutFlights = (object)(objectsList[6]);  //对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象
            DataTable dtDataItems = (DataTable)(objectsList[7]); //用户有权限的数据项
            DataTable stationInfor = (DataTable)(objectsList[8]); //滑行时间内存表


            string strThreeCodeList = ""; //机场三字码列表字符串（单个机场(如 "HAK")或多个机场(用 , 分隔，如 "HAK,PEK")）


            //是否繁忙
            if (arrayBusy[iIndexBusy])
                return; //如果繁忙，则退出
            arrayBusy[iIndexBusy] = true; //设置 繁忙 标记

            //跟踪分析
            string strTraceInfo = ""; //跟踪信息
            int iTraceInfo_1 = 1;
            string strTraceInfo_2 = "";
            string strTraceInfo_3 = "";
 
            strTraceInfo = strTraceInfo + "Start: " + DateTime.Now.ToString("mm:ss.fffffff") + " ; "; //跟踪分析

            //
            StationBM stationBM = new StationBM(); //重新生成航站对象，避免运行中更改参数中的航站对象的信息
            stationBM.ThreeCode = stationBM_Parameter.ThreeCode;
            strThreeCodeList = stationBM.ThreeCode; //机场三字码列表字符串（单个机场(如 "HAK")或多个机场(用 , 分隔，如 "HAK,PEK")）
            string[] arrayThreeCodeList = strThreeCodeList.Split(','); //机场三字码列表数组
            strTraceInfo = strTraceInfo + "Airports: " + arrayThreeCodeList.Length.ToString() + " ; "; //跟踪分析
            foreach (string strThreeCode in arrayThreeCodeList) //处理各个机场
            {
                strTraceInfo = strThreeCode + "(S): " + DateTime.Now.ToString("mm:ss.fffffff") + " ; "; //跟踪分析
                iTraceInfo_1 = 1; //跟踪分析

                //把 单个机场三字码 赋值 航站对象 ThreeCode 属性
                stationBM.ThreeCode = strThreeCode; //stationBM 不能是参数中的航站对象的引用

                //获取该航站当天的所有航班
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
                DateTimeBM dateTimeBM = new DateTimeBM();
                if ((DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00")) &&
                    (DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00")))
                {
                    dateTimeBM.StartDateTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 05:00:00";
                    dateTimeBM.EndDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
                }
                else
                {
                    dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
                    dateTimeBM.EndDateTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
                } //可以考虑下更合理的时间区间：进行ddateTimeBM.StartDateTime和dateTimeBM.EndDateTime赋值的时间区间，以及dateTimeBM.StartDateTime和dateTimeBM.EndDateTime应该分别赋值多少

                ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
                DataTable dtTodayStationFlights = rvSF.Dt;

                //获取用户有权限的数据项
                //DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
                //DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

                //进出港航班表格Schema
                DataTable dtInOutFlightsSchema = GetDisplaySchema(dtDataItems);

                //获取当天进出港航班 
                //DataTable dtTodayInOutFlights = FillInOutFlights(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);
                IList ilTodayInOutFlights = FillInOutFlights_1(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);

                //绑定数据
                //DataView dataView  = new DataView(dtTodayInOutFlights, "", "cniRowIndex", DataViewRowState.CurrentRows);
                //dataGridView3.DataSource = dataView;
                //dataGridView3.DataSource = ilTodayInOutFlights;

                strTraceInfo = strTraceInfo + strThreeCode + "(E:: " + ilTodayInOutFlights.Count.ToString() + "): " + DateTime.Now.ToString("mm:ss.fffffff") + " ; "; //跟踪分析
                
                #region 遍历进出港航班表
                IEnumerator ieTodayInOutFlights = ilTodayInOutFlights.GetEnumerator();
                while (ieTodayInOutFlights.MoveNext())
                {
                    strTraceInfo_2 = "Index: " + iTraceInfo_1.ToString() + " ; "; //跟踪分析

                    string strInDEPSTN = "";
                    string strInARRSTN = "";
                    string strOutDEPSTN = "";
                    string strOutARRSTN = "";
                    string strOverStationType = ""; //过站类型：始发、过站、快速过站、航后
                    int iOverStationStandardTime = 0; //过站标准时间（分钟）
                    int iTaxiOutMinutes = 0; //滑行时间（分钟）
                    string strOverStationStart = ""; //开始时刻，即 计算到港时刻
                    string strOverStationEnd = ""; //结束时刻，即 计算离港时刻
                    string strAlarmCode = ""; //告警代码
                    string strAlarmValue = ""; //告警值
                    int iAlarmResult = 0; //告警结果


                    try
                    {
                        GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current; //实例化当前进出港航班条

                        #region 人工模拟部分参数
                        //iOverStationStandardTime = 45; //模拟 TYN 的过站时间，应该结合 小机型 动态设置
                        //strAlarmCode = "OutcncOutPilotArriveTime";
                        #endregion 人工模拟部分参数

                        #region 确定航班起落机场三字码
                        if (guaranteeInforBM.IncncDATOP != "1900-01-01") //进港航班
                        {
                            DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                                "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                                "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                                "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                            if (dataRowFlight.Length > 0)
                            {
                                strInDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                                strInARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                            }
                        }

                        if (guaranteeInforBM.OutcncDATOP != "1900-01-01") //出港航班
                        {
                            DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                               "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                               "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                               "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                            if (dataRowFlight.Length > 0)
                            {
                                strOutDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                                strOutARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();

                            }
                        }
                        #endregion 确定航班起落机场三字码

                        #region 确定过站时间
                        DataRow[] dataRowsOverStationTime = overStationTime.Select(
                            "(cncAirportThreeCode = '" +
                            stationBM.ThreeCode + "') and (cnvcSmallACTYP = '" +
                            guaranteeInforBM.IncncACTYP + "')"); //根据机场三字码和小机型获取过站时间数据
                        if (dataRowsOverStationTime.Length > 0)
                        {
                            //iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniGroundTime"].ToString());
                            iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniStandardTime"].ToString()); //标准过站时间 //added by LinYong in 20160310
                        }
                        else
                        {
                            throw new Exception("过站时间表中没有此记录：" + Environment.NewLine +
                                "机场：" + stationBM.ThreeCode + Environment.NewLine +
                                "小机型：" + guaranteeInforBM.IncncACTYP);
                        }
                        #endregion 确定过站时间

                        #region 确定滑行时间
                        DataRow[] dataRowsStationInfor = stationInfor.Select(
                            "cncThreeCode = '" +
                            stationBM.ThreeCode + "'"); //根据机场三字码获取滑行时间数据
                        if (dataRowsStationInfor.Length > 0)
                        {
                            iTaxiOutMinutes = Convert.ToInt32(dataRowsStationInfor[0]["cniTaxiOutMinutes"].ToString());
                        }
                        else
                        {
                            throw new Exception("航站表中没有此记录（滑行时间）：" + Environment.NewLine +
                                "机场：" + stationBM.ThreeCode + Environment.NewLine);
                        }
                        #endregion 确定滑行时间

                        #region 确定过站类型
                        if ((guaranteeInforBM.IncncDATOP == "1900-01-01") &&
                            (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                        {
                            strOverStationType = "始发";
                        }
                        else if ((guaranteeInforBM.IncncDATOP != "1900-01-01") &&
                            (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                        {
                            strOverStationType = "过站";
                        }
                        else
                        {
                            strOverStationType = "航后";
                        }
                        #endregion 确定过站类型

                        #region 确定 开始时刻 和 结束时刻，即 确定 计算离港时刻 和 计算到港时刻
                        if ((strOverStationType == "过站") ||
                            (strOverStationType == "快速过站"))
                        {
                            //计算 开始时刻
                            if (guaranteeInforBM.IncncAllStatus != "ATA")
                            {
                                strOverStationStart = guaranteeInforBM.IncncAllETA;
                            }
                            else
                            {
                                strOverStationStart = guaranteeInforBM.IncncAllATA;
                            }
                            //计算 结束时刻
                            if (Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime) > Convert.ToDateTime(guaranteeInforBM.OutcncAllETD))
                            {
                                strOverStationEnd = Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                            }

                        }
                        else if (strOverStationType == "始发")
                        {
                            //计算 开始时刻
                            strOverStationStart = "";

                            //计算 结束时刻
                            strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                        }
                        else //航后航班不处理
                        {
                            continue;
                        }
                        #endregion 确定 开始时刻 和 结束时刻，即 确定 计算离港时刻 和 计算到港时刻

                        #region 各及时性项目运算
                        lock (objTodayInOutFlights)
                        {

                            #region 空勤组到位及时性 判断 使用了字段 OutcncOutCrewArriveTime -- 此部分代码不使用
                            /*
                #region 空勤组到位及时性 判断
                guaranteeInforBM.OutcncOutCrewArriveTime
                if (strAlarmCode == "OutcncOutPilotArriveTime")
                {
                    DateTime dOutPilotArriveTime = new DateTime(); //确定机组应该到位的时刻
                    if ((strOverStationType == "过站") ||
                        (strOverStationType == "快速过站"))
                    {

                        //确定机组应该到位的时刻
                        if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                        {
                            dOutPilotArriveTime = Convert.ToDateTime(strOverStationStart);
                        }
                        else
                        {
                            dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                        }
                    }
                    else if (strOverStationType == "始发")
                    {
                        dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                    }
                    //告警值
                    strAlarmValue = guaranteeInforBM.OutcncOutPilotArriveTime;

                    //告警结果 判断（机组应该到位的时刻 往前 5分钟 开始判断）
                    if (DateTime.Now < dOutPilotArriveTime.AddMinutes(-5)) //
                    {
                        iAlarmResult = -1; //还未到判断时间
                    }
                    else
                    {
                        if (guaranteeInforBM.OutcncOutPilotArriveTime.Trim() == "")
                        {
                            iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncOutPilotArriveTime 还未录入数据
                        }
                        else
                        {
                            DateTime dOutcncOutPilotArriveTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                guaranteeInforBM.OutcncFlightDate +
                                guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(0, 2) +
                                ":" +
                                guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(2, 2) +
                                ":00"); //机组实际到位时刻
                            if (dOutcncOutPilotArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                            {
                                dOutcncOutPilotArriveTime.AddDays(-1);
                            }

                            if (dOutcncOutPilotArriveTime > dOutPilotArriveTime)
                            {
                                iAlarmResult = 2; //晚到
                            }
                            else
                            {
                                iAlarmResult = 0; //准时 
                            }
                        }
                    }
                    //处理内存表 _dtTodayInOutFlights
                    ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
                        todayInOutFlights,
                        guaranteeInforBM,
                        strInDEPSTN,
                        strInARRSTN,
                        strOutDEPSTN,
                        strOutARRSTN,
                        strOverStationType,
                        iOverStationStandardTime.ToString(),
                        strOverStationStart,
                        strOverStationEnd,
                        strAlarmCode,
                        strAlarmValue,
                        iAlarmResult.ToString());
                }
                #endregion 空勤组到位及时性 判断
                */
                            #endregion 空勤组到位及时性 判断 使用了字段 OutcncOutCrewArriveTime -- 此部分代码不使用

                            #region 空勤组到位及时性 判断 -- 此部分代码不使用
                            /*
                    strAlarmCode = "OutcncOutCrewArriveTime";

                    if (strAlarmCode == "OutcncOutCrewArriveTime")
                    {
                        DateTime dOutCrewArriveTime = new DateTime(); //确定机组应该到位的时刻
                        if ((strOverStationType == "过站") ||
                            (strOverStationType == "快速过站"))
                        {

                            //确定机组应该到位的时刻
                            if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                            {
                                dOutCrewArriveTime = Convert.ToDateTime(strOverStationStart);
                            }
                            else
                            {
                                dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                            }
                        }
                        else if (strOverStationType == "始发")
                        {
                            dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                        }
                        //告警值
                        strAlarmValue = guaranteeInforBM.OutcncOutCrewArriveTime;

                        //告警结果 判断（机组应该到位的时刻 往前 5分钟 开始判断）
                        if (DateTime.Now < dOutCrewArriveTime.AddMinutes(-5)) //
                        {
                            iAlarmResult = -1; //还未到判断时间
                        }
                        else
                        {
                            if (guaranteeInforBM.OutcncOutCrewArriveTime.Trim() == "")
                            {
                                iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncOutCrewArriveTime 还未录入数据
                            }
                            else
                            {
                                DateTime dOutcncOutCrewArriveTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                    guaranteeInforBM.OutcncFlightDate +
                                    " " +
                                    guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(0, 2) +
                                    ":" +
                                    guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(2, 2) +
                                    ":00"); //机组实际到位时刻
                                if (dOutcncOutCrewArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                {
                                    dOutcncOutCrewArriveTime.AddDays(-1);
                                }

                                if (dOutcncOutCrewArriveTime > dOutCrewArriveTime)
                                {
                                    iAlarmResult = 2; //晚到
                                }
                                else
                                {
                                    iAlarmResult = 0; //准时 
                                }
                            }
                        }
                        //处理内存表 _dtTodayInOutFlights
                        ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
                            todayInOutFlights,
                            guaranteeInforBM,
                            strInDEPSTN,
                            strInARRSTN,
                            strOutDEPSTN,
                            strOutARRSTN,
                            strOverStationType,
                            iOverStationStandardTime.ToString(),
                            strOverStationStart,
                            strOverStationEnd,
                            strAlarmCode,
                            strAlarmValue,
                            dOutCrewArriveTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            iAlarmResult.ToString(),
                            objTodayInOutFlights);
                    }
                    */
                            #endregion 空勤组到位及时性 判断 -- 此部分代码不使用


                            strTraceInfo_3 = strTraceInfo_2 + "K: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 空勤组到位及时性 判断
                            strAlarmCode = "OutcncOutCrewArriveTime";

                            if (strAlarmCode == "OutcncOutCrewArriveTime")
                            {
                                DateTime dOutCrewArriveTime = new DateTime(); //确定机组应该到位的时刻
                                if ((strOverStationType == "过站") ||
                                    (strOverStationType == "快速过站"))
                                {

                                    //确定机组应该到位的时刻
                                    if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                                    {
                                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationStart);
                                    }
                                    else
                                    {
                                        dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                                    }
                                }
                                else if (strOverStationType == "始发")
                                {
                                    dOutCrewArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                                }

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncOutCrewArriveTime;

                                //告警结果 判断
                                if (guaranteeInforBM.OutcncOutCrewArriveTime.Trim() == "") //还未录入保障数据
                                {
                                    if (DateTime.Now < dOutCrewArriveTime) //
                                    {
                                        iAlarmResult = 4; //还未到 机组应该到位的时刻
                                    }
                                    else
                                    {
                                        iAlarmResult = 3; //已到 机组应该到位的时刻
                                    }
                                }
                                else //已经录入保障数据
                                {
                                    DateTime dOutcncOutCrewArriveTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncOutCrewArriveTime.Trim().Substring(2, 2) +
                                        ":00"); //机组实际到位时刻
                                    if (dOutcncOutCrewArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                    {
                                        dOutcncOutCrewArriveTime.AddDays(-1);
                                    }

                                    if (dOutcncOutCrewArriveTime > dOutCrewArriveTime)
                                    {
                                        iAlarmResult = 2; //晚到
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutCrewArriveTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion 空勤组到位及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "J: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 机务放行及时性 判断
                            strAlarmCode = "OutcncMCCReleaseTime";

                            if (strAlarmCode == "OutcncMCCReleaseTime")
                            {
                                DateTime dMCCReleaseTime = new DateTime(); //机务应该最晚放行的时刻

                                //确定机务应该最晚放行的时刻
                                if ((strOverStationType == "过站") ||
                                    (strOverStationType == "快速过站"))
                                {
                                    //确定机务应该最晚放行的时刻
                                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737机型
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-21);
                                    }
                                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200机型
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
                                    }
                                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300机型
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }
                                    else //其他情况，按照最严的要求设置
                                    {
                                        dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }

                                }
                                else if (strOverStationType == "始发")
                                {
                                    dMCCReleaseTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-45);
                                }

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncMCCReleaseTime;

                                //告警结果 判断
                                if (guaranteeInforBM.OutcncMCCReleaseTime.Trim() == "") //相应数据项 OutcncMCCReleaseTime 还未录入数据
                                {
                                    if (DateTime.Now < dMCCReleaseTime) //还未到 机务应该最晚放行的时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 机务应该最晚放行的时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //相应数据项 OutcncMCCReleaseTime 已录入数据
                                {
                                    DateTime dOutcncMCCReleaseTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncMCCReleaseTime.Trim().Substring(2, 2) +
                                        ":00"); //机务放行时刻
                                    if (dOutcncMCCReleaseTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                    {
                                        dOutcncMCCReleaseTime.AddDays(-1);
                                    }

                                    if (dOutcncMCCReleaseTime > dMCCReleaseTime)
                                    {
                                        iAlarmResult = 2; //晚放行
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //准时放行 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dMCCReleaseTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion 机务放行及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "F: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 飞机准备完毕及时性 判断
                            strAlarmCode = "OutcncOutPlaneReadyEndTime";

                            if (strAlarmCode == "OutcncOutPlaneReadyEndTime")
                            {
                                DateTime dOutPlaneReadyEndTime = new DateTime(); //飞机应该最晚准备完毕的时刻

                                //确定飞机应该最晚准备完毕的时刻
                                if ((strOverStationType == "过站") ||
                                    (strOverStationType == "快速过站"))
                                {
                                    //确定飞机应该最晚准备完毕的时刻
                                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737机型
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
                                    }
                                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200机型
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
                                    }
                                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300机型
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }
                                    else //其他情况，按照最严的要求设置
                                    {
                                        dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }

                                }
                                else if (strOverStationType == "始发")
                                {
                                    dOutPlaneReadyEndTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
                                }

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncOutPlaneReadyEndTime;

                                //告警结果 判断
                                if (guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim() == "") //相应数据项 OutcncOutPlaneReadyEndTime 还未录入数据
                                {
                                    if (DateTime.Now < dOutPlaneReadyEndTime) //还未到 飞机应该最晚准备完毕的时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 飞机应该最晚准备完毕的时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //相应数据项 OutcncOutPlaneReadyEndTime 已录入数据
                                {
                                    DateTime dOutcncOutPlaneReadyEndTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncOutPlaneReadyEndTime.Trim().Substring(2, 2) +
                                        ":00"); //准备完毕时刻
                                    if (dOutcncOutPlaneReadyEndTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                    {
                                        dOutcncOutPlaneReadyEndTime.AddDays(-1);
                                    }

                                    if (dOutcncOutPlaneReadyEndTime > dOutPlaneReadyEndTime)
                                    {
                                        iAlarmResult = 2; //准备完毕 -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //准备完毕 -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutPlaneReadyEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion 飞机准备完毕及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "T: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 通知上客及时性 判断
                            strAlarmCode = "OutcncInformBoardTime";

                            if (strAlarmCode == "OutcncInformBoardTime")
                            {
                                DateTime dInformBoardTime = new DateTime(); //最晚通知上客的时刻

                                //确定最晚通知上客的时刻
                                if ((strOverStationType == "过站") ||
                                    (strOverStationType == "快速过站"))
                                {
                                    //确定最晚通知上客的时刻
                                    if (guaranteeInforBM.IncncACTYP.Substring(0, 2) == "73") //B737机型
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-25);
                                    }
                                    else if (";76A;787;331;330;336;33v;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //B767/B787/A330-200机型
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-30);
                                    }
                                    else if (";335;334;".IndexOf(guaranteeInforBM.IncncACTYP) >= 0) //A330-300机型
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }
                                    else //其他情况，按照最严的要求设置
                                    {
                                        dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-35);
                                    }

                                }
                                else if (strOverStationType == "始发")
                                {
                                    dInformBoardTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-40);
                                }

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncInformBoardTime;

                                //告警结果 判断
                                if (guaranteeInforBM.OutcncInformBoardTime.Trim() == "") //相应数据项 OutcncInformBoardTime 还未录入数据
                                {
                                    if (DateTime.Now < dInformBoardTime) //还未到 最晚通知上客的时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 最晚通知上客的时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //相应数据项 OutcncInformBoardTime 已录入数据
                                {
                                    DateTime dOutcncInformBoardTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncInformBoardTime.Trim().Substring(2, 2) +
                                        ":00"); //通知上客时刻
                                    if (dOutcncInformBoardTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                    {
                                        dOutcncInformBoardTime.AddDays(-1);
                                    }

                                    if (dOutcncInformBoardTime > dInformBoardTime)
                                    {
                                        iAlarmResult = 2; //通知上客 -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //通知上客 -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dInformBoardTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion 通知上客及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "KC: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 客舱关闭及时性 判断
                            strAlarmCode = "OutcncClosePaxCabinTime";

                            if (strAlarmCode == "OutcncClosePaxCabinTime")
                            {
                                DateTime dClosePaxCabinTime = new DateTime(); //最晚客舱关闭时刻

                                //确定最晚客舱关闭时刻
                                if ((strOverStationType == "过站") ||
                                    (strOverStationType == "快速过站"))
                                {
                                    //确定最晚客舱关闭时刻
                                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }
                                else if (strOverStationType == "始发")
                                {
                                    dClosePaxCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncClosePaxCabinTime;

                                //告警结果 判断
                                if (guaranteeInforBM.OutcncClosePaxCabinTime.Trim() == "") //相应数据项 OutcncClosePaxCabinTime 还未录入数据
                                {
                                    if (DateTime.Now < dClosePaxCabinTime) //还未到 最晚客舱关闭时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 最晚客舱关闭时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //相应数据项 OutcncClosePaxCabinTime 已录入数据
                                {
                                    DateTime dOutcncClosePaxCabinTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncClosePaxCabinTime.Trim().Substring(2, 2) +
                                        ":00"); //客舱关闭时刻
                                    if (dOutcncClosePaxCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                    {
                                        dOutcncClosePaxCabinTime.AddDays(-1);
                                    }

                                    if (dOutcncClosePaxCabinTime > dClosePaxCabinTime)
                                    {
                                        iAlarmResult = 2; //客舱关闭 -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //客舱关闭 -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dClosePaxCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion 客舱关闭及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "HC: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }


                            #region 货舱关闭及时性 判断
                            strAlarmCode = "OutcncCloseCargoCabinTime";

                            if (strAlarmCode == "OutcncCloseCargoCabinTime")
                            {
                                DateTime dCloseCargoCabinTime = new DateTime(); //最晚货舱关闭时刻

                                //确定最晚货舱关闭时刻
                                if ((strOverStationType == "过站") ||
                                    (strOverStationType == "快速过站"))
                                {
                                    //确定最晚货舱关闭时刻
                                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }
                                else if (strOverStationType == "始发")
                                {
                                    dCloseCargoCabinTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-5);
                                }

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncCloseCargoCabinTime;

                                //告警结果 判断
                                if (guaranteeInforBM.OutcncCloseCargoCabinTime.Trim() == "") //相应数据项 OutcncCloseCargoCabinTime 还未录入数据
                                {
                                    if (DateTime.Now < dCloseCargoCabinTime) //还未到 最晚货舱关闭时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 最晚货舱关闭时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //相应数据项 OutcncCloseCargoCabinTime 已录入数据
                                {
                                    DateTime dOutcncCloseCargoCabinTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                        guaranteeInforBM.OutcncFlightDate +
                                        " " +
                                        guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(0, 2) +
                                        ":" +
                                        guaranteeInforBM.OutcncCloseCargoCabinTime.Trim().Substring(2, 2) +
                                        ":00"); //货舱关闭时刻
                                    if (dOutcncCloseCargoCabinTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                    {
                                        dOutcncCloseCargoCabinTime.AddDays(-1);
                                    }

                                    if (dOutcncCloseCargoCabinTime > dCloseCargoCabinTime)
                                    {
                                        iAlarmResult = 2; //货舱关闭 -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //货舱关闭 -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dCloseCargoCabinTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }
                            #endregion 货舱关闭及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "TC: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 飞机推出及时性 判断
                            strAlarmCode = "OutcncAllATD";

                            if (strAlarmCode == "OutcncAllATD")
                            {
                                DateTime dOutcncAllSTD_Offset = new DateTime(); //最晚飞机推出时刻

                                //最晚飞机推出时刻
                                dOutcncAllSTD_Offset = Convert.ToDateTime(guaranteeInforBM.OutcncAllSTD).AddMinutes(5);

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncAllATD;

                                //告警结果 判断
                                if ((guaranteeInforBM.OutcncAllStatus == "SCH") ||
                                    (guaranteeInforBM.OutcncAllStatus == "DEL")) //航班状态表示还未推出
                                {
                                    if (DateTime.Now < dOutcncAllSTD_Offset) //还未到 最晚飞机推出时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 最晚飞机推出时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //航班状态表示已推出
                                {
                                    DateTime dOutcncAllATD = Convert.ToDateTime(guaranteeInforBM.OutcncAllATD); //实际飞机推出时刻

                                    if (dOutcncAllATD > dOutcncAllSTD_Offset)
                                    {
                                        iAlarmResult = 2; //飞机推出 -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //飞机推出 -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutcncAllSTD_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion 飞机推出及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "QF: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 飞机起飞及时性 判断
                            strAlarmCode = "OutcncAllTOFF";

                            if (strAlarmCode == "OutcncAllTOFF")
                            {
                                DateTime dOutcncAllTOFF_Offset = new DateTime(); //最晚飞机起飞（离地）时刻

                                //最晚飞机起飞（离地）时刻
                                dOutcncAllTOFF_Offset = Convert.ToDateTime(guaranteeInforBM.OutcncAllSTD).AddMinutes(iTaxiOutMinutes);

                                //告警值
                                strAlarmValue = guaranteeInforBM.OutcncAllTOFF;

                                //告警结果 判断
                                if ((guaranteeInforBM.OutcncAllStatus == "SCH") ||
                                    (guaranteeInforBM.OutcncAllStatus == "DEL") ||
                                    (guaranteeInforBM.OutcncAllStatus == "ATD")) //航班状态表示还未起飞（离地）
                                {
                                    if (DateTime.Now < dOutcncAllTOFF_Offset) //还未到 最晚飞机起飞（离地）时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 最晚飞机起飞（离地）时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //航班状态表示已起飞（离地）
                                {
                                    DateTime dOutcncAllTOFF = Convert.ToDateTime(guaranteeInforBM.OutcncAllTOFF); //实际飞机起飞（离地）时刻

                                    if (dOutcncAllTOFF > dOutcncAllTOFF_Offset)
                                    {
                                        iAlarmResult = 2; //飞机起飞（离地） -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //飞机起飞（离地） -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dOutcncAllTOFF_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion 飞机起飞及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "LD: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 进港航班落地及时性 判断
                            strAlarmCode = "IncncAllTDWN";

                            if (strAlarmCode == "IncncAllTDWN")
                            {
                                DateTime dIncncAllTDWN_Offset = new DateTime(); //最晚飞机落地时刻

                                //最晚飞机落地时刻
                                dIncncAllTDWN_Offset = Convert.ToDateTime(guaranteeInforBM.IncncAllSTA).AddMinutes(20);

                                //告警值
                                strAlarmValue = guaranteeInforBM.IncncAllTDWN;

                                //告警结果 判断
                                if ((guaranteeInforBM.IncncAllStatus == "SCH") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEL") ||
                                    (guaranteeInforBM.IncncAllStatus == "ATD") ||
                                    (guaranteeInforBM.IncncAllStatus == "RTR") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEP")) //航班状态表示还未落地
                                {
                                    if (DateTime.Now < dIncncAllTDWN_Offset) //还未到 最晚飞机落地时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 最晚飞机落地时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //航班状态表示已落地
                                {
                                    DateTime dIncncAllTDWN = Convert.ToDateTime(guaranteeInforBM.IncncAllTDWN); //实际飞机落地时刻

                                    if (dIncncAllTDWN > dIncncAllTDWN_Offset)
                                    {
                                        iAlarmResult = 2; //飞机落地 -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //飞机落地 -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dIncncAllTDWN_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion 进港航班落地及时性 判断

                            strTraceInfo_3 = strTraceInfo_2 + "DW: " + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪分析
                            if ((strTraceInfo.Length + strTraceInfo_3.Length) < 260)
                            {
                                guaranteeInforBM.OutcnvcOutRemark = strTraceInfo + strTraceInfo_3;
                            }
                            else
                            {
                                guaranteeInforBM.OutcnvcOutRemark = "字符数超限！";
                            }

                            #region 进港航班飞机到位及时性 判断
                            strAlarmCode = "IncncAllATA";

                            if (strAlarmCode == "IncncAllATA")
                            {
                                DateTime dIncncAllATA_Offset = new DateTime(); //最晚飞机到位时刻

                                //最晚飞机到位时刻
                                dIncncAllATA_Offset = Convert.ToDateTime(guaranteeInforBM.IncncAllSTA);

                                //告警值
                                strAlarmValue = guaranteeInforBM.IncncAllATA;

                                //告警结果 判断
                                if ((guaranteeInforBM.IncncAllStatus == "SCH") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEL") ||
                                    (guaranteeInforBM.IncncAllStatus == "ATD") ||
                                    (guaranteeInforBM.IncncAllStatus == "RTR") ||
                                    (guaranteeInforBM.IncncAllStatus == "DEP") ||
                                    (guaranteeInforBM.IncncAllStatus == "ARR")) //航班状态表示还未到位
                                {
                                    if (DateTime.Now < dIncncAllATA_Offset) //还未到 最晚飞机到位时刻
                                    {
                                        iAlarmResult = 4;
                                    }
                                    else //已到 最晚飞机到位时刻
                                    {
                                        iAlarmResult = 3;
                                    }
                                }
                                else //航班状态表示已到位
                                {
                                    DateTime dIncncAllATA = Convert.ToDateTime(guaranteeInforBM.IncncAllATA); //实际飞机到位时刻

                                    if (dIncncAllATA > dIncncAllATA_Offset)
                                    {
                                        iAlarmResult = 2; //飞机到位 -- 晚点
                                    }
                                    else
                                    {
                                        iAlarmResult = 0; //飞机到位 -- 准时 
                                    }
                                }

                                //处理内存表 _dtTodayInOutFlights
                                ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights_NotLock(
                                    todayInOutFlights,
                                    guaranteeInforBM,
                                    strInDEPSTN,
                                    strInARRSTN,
                                    strOutDEPSTN,
                                    strOutARRSTN,
                                    iTaxiOutMinutes.ToString(),
                                    strOverStationType,
                                    iOverStationStandardTime.ToString(),
                                    strOverStationStart,
                                    strOverStationEnd,
                                    strAlarmCode,
                                    strAlarmValue,
                                    dIncncAllATA_Offset.ToString("yyyy-MM-dd HH:mm:ss"),
                                    iAlarmResult.ToString(),
                                    objTodayInOutFlights);
                            }

                            #endregion 进港航班飞机到位及时性 判断

                        }
                        #endregion 各及时性项目运算




                    }
                    catch (Exception ex)
                    {
                        string strExceptionMessage = ex.Message;
                    }

                    iTraceInfo_1 = iTraceInfo_1 + 1; //跟踪分析
                }
                #endregion 遍历进出港航班表

                //dataGridView4.DataSource = todayInOutFlights;
            }

            //是否繁忙标记设置
            arrayBusy[iIndexBusy] = false; //设置 不繁忙 标记
        }
        #endregion 处理航班告警信息 -- 提供线程使用，可以在一个线程中处理多个机场航班数据 -- 需要把里头的数据网格绑定代码部分去除才能放在线程中使用

        #region 处理内存告警表（进出港航班列表）：修改或增加记录
        /// <summary>
        /// 处理内存告警表（进出港航班列表）：修改或增加记录
        /// </summary>
        /// <param name="todayInOutFlights">进出港航班内存表(_dtTodayInOutFlights 数据表) </param>
        /// <param name="guaranteeInforBM">进出港航班条对象实例</param>
        /// <param name="inDEPSTN">进港起飞机场</param>
        /// <param name="inARRSTN">进港到达机场</param>
        /// <param name="outDEPSTN">出港起飞机场</param>
        /// <param name="outARRSTN">出港到达机场</param>
        /// <param name="overStationType">过站类型：始发、过站、快速过站、航后</param>
        /// <param name="overStationStandardTime">过站标准时间</param>
        /// <param name="overStationStart">过站开始时刻</param>
        /// <param name="overStationEnd">过站结束时刻</param>
        /// <param name="alarmCode">告警代码</param>
        /// <param name="alarmValue">告警值</param>
        /// <param name="alarmResult">告警结果</param>
        /// <param name="objTodayInOutFlights">对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象</param>
        /// <returns>ReturnValueSF对象：Result：1，成功；-1 不成功。Message：操作情况</returns>
        private ReturnValueSF DealTodayInOutFlights_Bak20151109(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            returnValueSF.Result = -1;

            #region 从内存表提取此进出港航班条记录
            DataRow[] dataRowsTodayInOutFlights = null;
            lock (objTodayInOutFlights)
            {
                dataRowsTodayInOutFlights = todayInOutFlights.Select("cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //提取数据
            }
            #endregion 从内存表提取此进出港航班条记录

            #region 存在
            if (dataRowsTodayInOutFlights.Length == 1) //存在
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString())
                    ) //相关数据项有变动，更新（cndOperationTime变更为最新时间）
                {
                    lock (objTodayInOutFlights)
                    {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;
                    }

                    returnValueSF.Result = 1;
                    returnValueSF.Message = "更新内存表成功！";
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "无数据项变化，没有更新！";
                }
            }
            #endregion 存在
            #region 不存在
            else if (dataRowsTodayInOutFlights.Length == 0) //不存在，增加
            {
                lock (objTodayInOutFlights)
                {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;
                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = "";
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);
                }

                returnValueSF.Result = 1;
                returnValueSF.Message = "在内存表增加记录成功！";
            }
            #endregion 不存在
            #region 存在多条记录
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "内存表存在多条关键字查询记录！";
            }
            #endregion 存在多条记录

            //
            return returnValueSF;
        }
        #endregion 处理内存告警表（进出港航班列表）：修改或增加记录

        #region 处理内存告警表（进出港航班列表）：修改或增加记录
        /// <summary>
        /// 处理内存告警表（进出港航班列表）：修改或增加记录
        /// </summary>
        /// <param name="todayInOutFlights">进出港航班内存表(_dtTodayInOutFlights 数据表) </param>
        /// <param name="guaranteeInforBM">进出港航班条对象实例</param>
        /// <param name="inDEPSTN">进港起飞机场</param>
        /// <param name="inARRSTN">进港到达机场</param>
        /// <param name="outDEPSTN">出港起飞机场</param>
        /// <param name="outARRSTN">出港到达机场</param>
        /// <param name="overStationType">过站类型：始发、过站、快速过站、航后</param>
        /// <param name="overStationStandardTime">过站标准时间</param>
        /// <param name="overStationStart">过站开始时刻</param>
        /// <param name="overStationEnd">过站结束时刻</param>
        /// <param name="alarmCode">告警代码</param>
        /// <param name="alarmValue">告警值</param>
        /// <param name="alarmResult">告警结果</param>
        /// <param name="objTodayInOutFlights">对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象</param>
        /// <returns>ReturnValueSF对象：Result：1，成功；-1 不成功。Message：操作情况</returns>
        private ReturnValueSF DealTodayInOutFlights_Bak20151201(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            returnValueSF.Result = -1;

            #region 从内存表提取此进出港航班条记录
            DataRow[] dataRowsTodayInOutFlights = null;
            DataRow[] dataRowsTodayInOutFlights_Not = null;
            lock (objTodayInOutFlights)
            {
                dataRowsTodayInOutFlights = todayInOutFlights.Select(
                    "cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //提取数据

                dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                    "cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //提取数据
            }
            #endregion 从内存表提取此进出港航班条记录

            #region 存在
            if (dataRowsTodayInOutFlights.Length == 1) //存在
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString()) ||
                    (guaranteeInforBM.IncncAllSTA != dataRowsTodayInOutFlights[0]["cncInSTA"].ToString()) ||
                    (guaranteeInforBM.IncncAllETA != dataRowsTodayInOutFlights[0]["cncInETA"].ToString()) ||
                    (guaranteeInforBM.IncncAllStatus != dataRowsTodayInOutFlights[0]["cncInSTATUS"].ToString()) ||
                    (guaranteeInforBM.OutcncAllSTD != dataRowsTodayInOutFlights[0]["cncOutSTD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllETD != dataRowsTodayInOutFlights[0]["cncOutETD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllStatus != dataRowsTodayInOutFlights[0]["cncOutSTATUS"].ToString())
                    ) //相关数据项有变动，更新（cndOperationTime变更为最新时间）
                {
                    lock (objTodayInOutFlights)
                    {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                        dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                        dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                        dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                        dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                        dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                        dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;
                    }

                    returnValueSF.Result = 1;
                    returnValueSF.Message = "更新内存表成功！";
                }
                else if (dataRowsTodayInOutFlights_Not.Length >= 1)
                {
                    DateTime dateTimeTodayInOutFlights = Convert.ToDateTime( dataRowsTodayInOutFlights[0]["cndOperationTime"].ToString());
                    DateTime dateTimeTodayInOutFlights_Not = Convert.ToDateTime(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"].ToString());

                    if (dateTimeTodayInOutFlights_Not >= dateTimeTodayInOutFlights)
                    {
                        lock (objTodayInOutFlights)
                        {
                            dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                            dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                            dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                            dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                            dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                            dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                            dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                            dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                            dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                            dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                            dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                            dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;
                        }

                        returnValueSF.Result = 1;
                        returnValueSF.Message = "更新内存表成功！";
                    }
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "无数据项变化，没有更新！";
                }
            }
            #endregion 存在
            #region 不存在
            else if (dataRowsTodayInOutFlights.Length == 0) //不存在，增加
            {
                lock (objTodayInOutFlights)
                {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;
                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = "";
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);
                }

                returnValueSF.Result = 1;
                returnValueSF.Message = "在内存表增加记录成功！";
            }
            #endregion 不存在
            #region 存在多条记录
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "内存表存在多条关键字查询记录！";
            }
            #endregion 存在多条记录

            //
            return returnValueSF;
        }
        #endregion 处理内存告警表（进出港航班列表）：修改或增加记录

        #region 处理内存告警表（进出港航班列表）：修改或增加记录
        /// <summary>
        /// 处理内存告警表（进出港航班列表）：修改或增加记录
        /// </summary>
        /// <param name="todayInOutFlights">进出港航班内存表(_dtTodayInOutFlights 数据表) </param>
        /// <param name="guaranteeInforBM">进出港航班条对象实例</param>
        /// <param name="inDEPSTN">进港起飞机场</param>
        /// <param name="inARRSTN">进港到达机场</param>
        /// <param name="outDEPSTN">出港起飞机场</param>
        /// <param name="outARRSTN">出港到达机场</param>
        /// <param name="taxiOutMinutes">滑出时间</param>       
        /// <param name="overStationType">过站类型：始发、过站、快速过站、航后</param>
        /// <param name="overStationStandardTime">过站标准时间</param>
        /// <param name="overStationStart">过站开始时刻</param>
        /// <param name="overStationEnd">过站结束时刻</param>
        /// <param name="alarmCode">告警代码</param>
        /// <param name="alarmValue">告警值</param>
        /// <param name="alarmResult">告警结果</param>
        /// <param name="objTodayInOutFlights">对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象</param>
        /// <returns>ReturnValueSF对象：Result：1，成功；-1 不成功。Message：操作情况</returns>
        private ReturnValueSF DealTodayInOutFlights_Bak20160603(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string taxiOutMinutes,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            returnValueSF.Result = -1;

            #region 从内存表提取此进出港航班条记录
            DataRow[] dataRowsTodayInOutFlights = null;
            DataRow[] dataRowsTodayInOutFlights_Not = null;
            lock (objTodayInOutFlights)
            {
                dataRowsTodayInOutFlights = todayInOutFlights.Select(
                    "cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //提取数据

                dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                    "cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //提取数据
            }
            #endregion 从内存表提取此进出港航班条记录

            #region 存在
            if (dataRowsTodayInOutFlights.Length == 1) //存在
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString()) ||
                    (guaranteeInforBM.IncncAllSTA != dataRowsTodayInOutFlights[0]["cncInSTA"].ToString()) ||
                    (guaranteeInforBM.IncncAllETA != dataRowsTodayInOutFlights[0]["cncInETA"].ToString()) ||
                    (guaranteeInforBM.IncncAllATA != dataRowsTodayInOutFlights[0]["cncInATA"].ToString()) || //added by LinYong in 20160310
                    (guaranteeInforBM.IncncAllStatus != dataRowsTodayInOutFlights[0]["cncInSTATUS"].ToString()) ||
                    (guaranteeInforBM.OutcncAllSTD != dataRowsTodayInOutFlights[0]["cncOutSTD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllETD != dataRowsTodayInOutFlights[0]["cncOutETD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllStatus != dataRowsTodayInOutFlights[0]["cncOutSTATUS"].ToString()) ||
                    (taxiOutMinutes != dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"].ToString())
                    ) //相关数据项有变动，更新（cndOperationTime变更为最新时间）
                {
                    lock (objTodayInOutFlights)
                    {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                        dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                        dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                        dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                        dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                        dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                        dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                        dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                        dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                        dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + DateTime.Now.ToString("mm:ss.fffffff"); //跟踪分析

                    }

                    returnValueSF.Result = 1;
                    returnValueSF.Message = "更新内存表成功！";
                }
                else if (dataRowsTodayInOutFlights_Not.Length >= 1)
                {
                    DateTime dateTimeTodayInOutFlights = Convert.ToDateTime(dataRowsTodayInOutFlights[0]["cndOperationTime"].ToString());
                    DateTime dateTimeTodayInOutFlights_Not = Convert.ToDateTime(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"].ToString());

                    if (dateTimeTodayInOutFlights_Not >= dateTimeTodayInOutFlights)
                    {
                        lock (objTodayInOutFlights)
                        {
                            dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                            dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                            dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                            dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                            dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                            dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                            dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                            dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                            dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                            dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                            dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                            dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                            dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                            dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                            dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + DateTime.Now.ToString("mm:ss.fffffff"); //跟踪分析

                        }

                        returnValueSF.Result = 1;
                        returnValueSF.Message = "更新内存表成功！";
                    }
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "无数据项变化，没有更新！";
                }
            }
            #endregion 存在
            #region 不存在
            else if (dataRowsTodayInOutFlights.Length == 0) //不存在，增加
            {
                lock (objTodayInOutFlights)
                {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                    dataRowTodayInOutFlights["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + DateTime.Now.ToString("mm:ss.fffffff"); //跟踪分析
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);
                }

                returnValueSF.Result = 1;
                returnValueSF.Message = "在内存表增加记录成功！";
            }
            #endregion 不存在
            #region 存在多条记录
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "内存表存在多条关键字查询记录！";
            }
            #endregion 存在多条记录

            //
            return returnValueSF;
        }
        #endregion 处理内存告警表（进出港航班列表）：修改或增加记录

        #region 处理内存告警表（进出港航班列表）：修改或增加记录，里面没有对_ dtTodayInOutFlights 加锁同步
        /// <summary>
        /// 处理内存告警表（进出港航班列表）：修改或增加记录，里面没有对_ dtTodayInOutFlights 加锁同步
        /// </summary>
        /// <param name="todayInOutFlights">进出港航班内存表(_dtTodayInOutFlights 数据表) </param>
        /// <param name="guaranteeInforBM">进出港航班条对象实例</param>
        /// <param name="inDEPSTN">进港起飞机场</param>
        /// <param name="inARRSTN">进港到达机场</param>
        /// <param name="outDEPSTN">出港起飞机场</param>
        /// <param name="outARRSTN">出港到达机场</param>
        /// <param name="taxiOutMinutes">滑出时间</param>        
        /// <param name="overStationType">过站类型：始发、过站、快速过站、航后</param>
        /// <param name="overStationStandardTime">过站标准时间</param>
        /// <param name="overStationStart">过站开始时刻</param>
        /// <param name="overStationEnd">过站结束时刻</param>
        /// <param name="alarmCode">告警代码</param>
        /// <param name="alarmValue">告警值</param>
        /// <param name="alarmResult">告警结果</param>
        /// <param name="objTodayInOutFlights">对 进出港航班内存表(_dtTodayInOutFlights 数据表) 的操作同步对象</param>
        /// <returns>ReturnValueSF对象：Result：1，成功；-1 不成功。Message：操作情况</returns>
        private ReturnValueSF DealTodayInOutFlights_NotLock(
            DataTable todayInOutFlights,
            GuaranteeInforBM guaranteeInforBM,
            string inDEPSTN,
            string inARRSTN,
            string outDEPSTN,
            string outARRSTN,
            string taxiOutMinutes,
            string overStationType,
            string overStationStandardTime,
            string overStationStart,
            string overStationEnd,
            string alarmCode,
            string alarmValue,
            string AlarmPoint,
            string alarmResult,
            object objTodayInOutFlights)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();
            string strTraceInfo = ""; //跟踪信息


            returnValueSF.Result = -1;

            #region 从内存表提取此进出港航班条记录
            DataRow[] dataRowsTodayInOutFlights = null;
            DataRow[] dataRowsTodayInOutFlights_Not = null;
                //dataRowsTodayInOutFlights = todayInOutFlights.Select(
                //    "cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                //    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                //    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                //    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                //    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                //    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                //    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                //    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                //    "' and cnvcAlarmCode = '" + alarmCode + "'"); //提取数据
                dataRowsTodayInOutFlights = todayInOutFlights.Select(
                    "cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cncInDATOP='" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID='" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO=" + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC='" + guaranteeInforBM.IncnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'"); //提取数据

                strTraceInfo = strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪信息

                //dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                //    "cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                //    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                //    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                //    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                //    "' and cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                //    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                //    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                //    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                //    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //提取数据
                dataRowsTodayInOutFlights_Not = todayInOutFlights.Select(
                    "cncOutDATOP='" + guaranteeInforBM.OutcncDATOP +
                    "' and cnvcOutFLTID='" + guaranteeInforBM.OutcnvcFLTID +
                    "' and cniOutLEGNO=" + guaranteeInforBM.OutcniLEGNO +
                    " and cnvcOutAC='" + guaranteeInforBM.OutcnvcAC +
                    "' and cncInDATOP <> '" + guaranteeInforBM.IncncDATOP +
                    "' and cnvcInFLTID <> '" + guaranteeInforBM.IncnvcFLTID +
                    "' and cniInLEGNO <> " + guaranteeInforBM.IncniLEGNO +
                    " and cnvcInAC <> '" + guaranteeInforBM.IncnvcAC +
                    "' and cnvcAlarmCode = '" + alarmCode + "'", "cndOperationTime desc"); //提取数据

                strTraceInfo = strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff") + " -- "; //跟踪信息
            #endregion 从内存表提取此进出港航班条记录

            #region 存在
            if (dataRowsTodayInOutFlights.Length == 1) //存在
            {
                if ((overStationType != dataRowsTodayInOutFlights[0]["cnvcOverStationType"].ToString()) ||
                    (overStationStandardTime != dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"].ToString()) ||
                    (overStationStart != dataRowsTodayInOutFlights[0]["cncOverStationStart"].ToString()) ||
                    (overStationEnd != dataRowsTodayInOutFlights[0]["cncOverStationEnd"].ToString()) ||
                    (alarmValue != dataRowsTodayInOutFlights[0]["cnvcAlarmValue"].ToString()) ||
                    (AlarmPoint != dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"].ToString()) ||
                    (alarmResult != dataRowsTodayInOutFlights[0]["cniAlarmResult"].ToString()) ||
                    (guaranteeInforBM.IncncAllSTA != dataRowsTodayInOutFlights[0]["cncInSTA"].ToString()) ||
                    (guaranteeInforBM.IncncAllETA != dataRowsTodayInOutFlights[0]["cncInETA"].ToString()) ||
                    (guaranteeInforBM.IncncAllATA != dataRowsTodayInOutFlights[0]["cncInATA"].ToString()) || //added by LinYong in 20160310
                    (guaranteeInforBM.IncncAllStatus != dataRowsTodayInOutFlights[0]["cncInSTATUS"].ToString()) ||
                    (guaranteeInforBM.OutcncAllSTD != dataRowsTodayInOutFlights[0]["cncOutSTD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllETD != dataRowsTodayInOutFlights[0]["cncOutETD"].ToString()) ||
                    (guaranteeInforBM.OutcncAllStatus != dataRowsTodayInOutFlights[0]["cncOutSTATUS"].ToString()) ||
                    (taxiOutMinutes != dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"].ToString())
                    ) //相关数据项有变动，更新（cndOperationTime变更为最新时间）
                {
                        dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                        dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                        dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                        dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                        dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                        dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                        dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                        dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                        dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                        dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                        dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                        dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                        dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                        dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                        dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                        dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff"); //跟踪分析


                    returnValueSF.Result = 1;
                    returnValueSF.Message = "更新内存表成功！";
                }
                else if (dataRowsTodayInOutFlights_Not.Length >= 1)
                {
                    //DateTime dateTimeTodayInOutFlights = Convert.ToDateTime(dataRowsTodayInOutFlights[0]["cndOperationTime"].ToString());
                    //DateTime dateTimeTodayInOutFlights_Not = Convert.ToDateTime(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"].ToString());
                    DateTime dateTimeTodayInOutFlights = ((DateTime)(dataRowsTodayInOutFlights[0]["cndOperationTime"]));
                    DateTime dateTimeTodayInOutFlights_Not = ((DateTime)(dataRowsTodayInOutFlights_Not[0]["cndOperationTime"]));

                    if (dateTimeTodayInOutFlights_Not >= dateTimeTodayInOutFlights)
                    {
                            dataRowsTodayInOutFlights[0]["cnvcOverStationType"] = overStationType;
                            dataRowsTodayInOutFlights[0]["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                            dataRowsTodayInOutFlights[0]["cncOverStationStart"] = overStationStart;
                            dataRowsTodayInOutFlights[0]["cncOverStationEnd"] = overStationEnd;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmValue"] = alarmValue;
                            dataRowsTodayInOutFlights[0]["cnvcAlarmPoint"] = AlarmPoint;
                            dataRowsTodayInOutFlights[0]["cniAlarmResult"] = Convert.ToInt32(alarmResult);

                            dataRowsTodayInOutFlights[0]["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                            dataRowsTodayInOutFlights[0]["cncInETA"] = guaranteeInforBM.IncncAllETA;
                            dataRowsTodayInOutFlights[0]["cncInATA"] = guaranteeInforBM.IncncAllATA; //added by LinYong in 20160310
                            dataRowsTodayInOutFlights[0]["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                            dataRowsTodayInOutFlights[0]["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                            dataRowsTodayInOutFlights[0]["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                            dataRowsTodayInOutFlights[0]["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                            dataRowsTodayInOutFlights[0]["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                            dataRowsTodayInOutFlights[0]["cndOperationTime"] = DateTime.Now;

                            dataRowsTodayInOutFlights[0]["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff"); //跟踪分析


                        returnValueSF.Result = 1;
                        returnValueSF.Message = "更新内存表成功！";
                    }
                }
                else
                {
                    returnValueSF.Result = 1;
                    returnValueSF.Message = "无数据项变化，没有更新！";
                }
            }
            #endregion 存在
            #region 不存在
            else if (dataRowsTodayInOutFlights.Length == 0) //不存在，增加
            {
                    DataRow dataRowTodayInOutFlights = todayInOutFlights.NewRow();
                    dataRowTodayInOutFlights["cncInDATOP"] = guaranteeInforBM.IncncDATOP;
                    dataRowTodayInOutFlights["cncInFlightDate"] = guaranteeInforBM.IncncFlightDate;
                    dataRowTodayInOutFlights["cnvcInFLTID"] = guaranteeInforBM.IncnvcFLTID;
                    dataRowTodayInOutFlights["cniInLEGNO"] = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                    dataRowTodayInOutFlights["cnvcInAC"] = guaranteeInforBM.IncnvcAC;
                    dataRowTodayInOutFlights["cnvcInLONG_REG"] = guaranteeInforBM.IncnvcLONG_REG;
                    dataRowTodayInOutFlights["cncInDEPSTN"] = inDEPSTN;
                    dataRowTodayInOutFlights["cncInARRSTN"] = inARRSTN;
                    dataRowTodayInOutFlights["cncInSTA"] = guaranteeInforBM.IncncAllSTA;
                    dataRowTodayInOutFlights["cncInETA"] = guaranteeInforBM.IncncAllETA;
                    dataRowTodayInOutFlights["cncInTDWN"] = guaranteeInforBM.IncncAllTDWN;
                    dataRowTodayInOutFlights["cncInATA"] = guaranteeInforBM.IncncAllATA;
                    dataRowTodayInOutFlights["cncInSTATUS"] = guaranteeInforBM.IncncAllStatus;
                    dataRowTodayInOutFlights["cncOutDATOP"] = guaranteeInforBM.OutcncDATOP;
                    dataRowTodayInOutFlights["cncOutFlightDate"] = guaranteeInforBM.OutcncFlightDate;
                    dataRowTodayInOutFlights["cnvcOutFLTID"] = guaranteeInforBM.OutcnvcFLTID;
                    dataRowTodayInOutFlights["cniOutLEGNO"] = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                    dataRowTodayInOutFlights["cnvcOutAC"] = guaranteeInforBM.OutcnvcAC;
                    dataRowTodayInOutFlights["cnvcOutLONG_REG"] = guaranteeInforBM.OutcnvcLONG_REG;
                    dataRowTodayInOutFlights["cncOutDEPSTN"] = outDEPSTN;
                    dataRowTodayInOutFlights["cncOutARRSTN"] = outARRSTN;
                    dataRowTodayInOutFlights["cncOutSTD"] = guaranteeInforBM.OutcncAllSTD;
                    dataRowTodayInOutFlights["cncOutETD"] = guaranteeInforBM.OutcncAllETD;
                    dataRowTodayInOutFlights["cncOutTOFF"] = guaranteeInforBM.OutcncAllTOFF;
                    dataRowTodayInOutFlights["cncOutATD"] = guaranteeInforBM.OutcncAllATD;
                    dataRowTodayInOutFlights["cncOutSTATUS"] = guaranteeInforBM.OutcncAllStatus;

                    dataRowTodayInOutFlights["cniTaxiOutMinutes"] = Convert.ToInt32(taxiOutMinutes);

                    dataRowTodayInOutFlights["cnvcOverStationType"] = overStationType;
                    dataRowTodayInOutFlights["cniOverStationStandardTime"] = Convert.ToInt32(overStationStandardTime);
                    dataRowTodayInOutFlights["cncOverStationStart"] = overStationStart;
                    dataRowTodayInOutFlights["cncOverStationEnd"] = overStationEnd;
                    dataRowTodayInOutFlights["cnvcAlarmCode"] = alarmCode;
                    dataRowTodayInOutFlights["cnvcAlarmValue"] = alarmValue;
                    dataRowTodayInOutFlights["cnvcAlarmPoint"] = AlarmPoint;
                    dataRowTodayInOutFlights["cniAlarmResult"] = Convert.ToInt32(alarmResult);
                    dataRowTodayInOutFlights["cnvcMemo"] = guaranteeInforBM.OutcnvcOutRemark + strTraceInfo + DateTime.Now.ToString("mm:ss.fffffff"); //跟踪分析
                    dataRowTodayInOutFlights["cndOperationTime"] = DateTime.Now;

                    todayInOutFlights.Rows.Add(dataRowTodayInOutFlights);

                returnValueSF.Result = 1;
                returnValueSF.Message = "在内存表增加记录成功！";
            }
            #endregion 不存在
            #region 存在多条记录
            else
            {
                returnValueSF.Result = -1;
                returnValueSF.Message = "内存表存在多条关键字查询记录！";
            }
            #endregion 存在多条记录

            //
            return returnValueSF;
        }
        #endregion 处理内存告警表（进出港航班列表）：修改或增加记录，里面没有对_ dtTodayInOutFlights 加锁同步




        #region 获取要显示表的架构
        /// <summary>
        /// 获取要显示表的架构
        /// </summary>
        /// <param name="dataItems">数据项列表</param>
        /// <returns></returns>
        private DataTable GetDisplaySchema(DataTable dataItems)
        {
            DataTable dtInOutFlights = new DataTable();

            //增加主键和排序字段
            dtInOutFlights.Columns.Add("IncncDATOP");                   //进港航班日期
            dtInOutFlights.Columns.Add("IncnvcFLTID");                  //进港航班航班号
            dtInOutFlights.Columns.Add("IncniLEGNO");                   //进港航班航段信息
            dtInOutFlights.Columns.Add("IncnvcAC");                     //进港航班飞机号
            dtInOutFlights.Columns.Add("IncncAllSTA");                  //进港航班计划到达时间（完整格式）
            dtInOutFlights.Columns.Add("IncncAllETA");                  //进港航班预计到达时间
            dtInOutFlights.Columns.Add("IncncAllTDWN");                 //进港航班落地时间
            dtInOutFlights.Columns.Add("IncncAllATA");                  //进港航班到位时间
            dtInOutFlights.Columns.Add("IncncAllStatus");               //进港航班航班状态
            dtInOutFlights.Columns.Add("IncniAllViewIndex");            //进港航班显示顺序

            dtInOutFlights.Columns.Add("OutcncDATOP");                  //出港航班日期
            dtInOutFlights.Columns.Add("OutcnvcFLTID");                 //出港航班航班号
            dtInOutFlights.Columns.Add("OutcniLEGNO");                  //出港航班航段信息
            dtInOutFlights.Columns.Add("OutcnvcAC");                    //出港航班飞机号
            dtInOutFlights.Columns.Add("OutcncAllSTD");                 //出港航班计划起飞时间
            dtInOutFlights.Columns.Add("OutcncAllETD");                 //出港航班预计起飞时间
            dtInOutFlights.Columns.Add("OutcncAllATD");                 //出港航班推出时间
            dtInOutFlights.Columns.Add("OutcncAllTOFF");                //出港航班起飞时间    
            dtInOutFlights.Columns.Add("OutcncAllStatus");              //出港航班航班状态
            dtInOutFlights.Columns.Add("OutcniAllViewIndex");           //出港航班显示顺序

            //根据用户设置的视图生成其他字段
            foreach (DataRow dataRow in dataItems.Rows)
            {
                dtInOutFlights.Columns.Add(dataRow["cnvcDataItemID"].ToString());
            }

            //行号
            dtInOutFlights.Columns.Add("cniRowIndex",typeof(Int32));
            return dtInOutFlights;
        }
        #endregion

        #region 填充进出港航班状态
        /// <summary>
        /// 填充进出港航班状态
        /// 将航站航班信息表分成进港航班信息表和出港航班信息表
        /// 然后将这两个表再组合成一个进出港航班信息表
        /// </summary>
        /// <param name="dtStationFlights">机场的进出港航班</param>
        /// <param name="dtInOutFlightsSchema">要显示表的架构</param>
        /// <param name="dtDataItems">数据项列表</param>
        /// <param name="stationBM">机场信息</param>
        /// <returns>返回 进出港航班信息表</returns>
        private DataTable FillInOutFlights(DataTable dtStationFlights, DataTable dtInOutFlightsSchema, DataTable dtDataItems, StationBM stationBM)
        {
            IList ilInOutFlights = new ArrayList();

            //进出港航班信息表
            DataTable dtAllInOutFlights = dtInOutFlightsSchema.Clone();

            //分别生成进港航班信息表和出港航班信息表
            //以便稍后组合生成进出港航班信息表
            //进港航班：目的机场三字码与航站三字码相同
            DataTable dtInFlights = dtStationFlights.Clone();
            //出港航班：起飞机场三字码与航站三字码相同
            DataTable dtOutFlights = dtStationFlights.Clone();

            //查询进港航班
            DataRow[] drInFlights = dtStationFlights.Select("cncARRSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTDWN");
            foreach (DataRow dataRow in drInFlights)
            {
                dtInFlights.ImportRow(dataRow);
            }

            //查询出港航班
            DataRow[] drOutFlights = dtStationFlights.Select("cncDEPSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTOFF");
            foreach (DataRow dataRow in drOutFlights)
            {
                dtOutFlights.ImportRow(dataRow);
            }

            #region 根据进港航班组合进出港航班
            //根据进港航班组合进出港航班
            foreach (DataRow dataRow in dtInFlights.Rows)
            {
                //进港航班的飞机号
                string strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
                //进港航班的预达时间
                string strInETD = dataRow["cncETD"].ToString();

                //查询字符串：根据出港航班的飞机号与进港航班的飞机号相同AND预计起飞时间大于进港航班的预计起飞时间
                string strSearch = "cnvcLONG_REG = '" + strLONG_REG + "' AND cncETD > '" + strInETD + "'";
                drOutFlights = dtOutFlights.Select(strSearch, "cncETD");

                #region 没有出港航班
                //没有出港航班
                if (drOutFlights.Length <= 0)
                {
                    //根据航班状态设置航班显示顺序
                    string strOutViewIndex = "";
                    if (dataRow["cncSTATUS"].ToString() == "CNL")
                    {
                        strOutViewIndex = "0";
                    }
                    else if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = "3";
                    }

                    //新建一行记录
                    DataRow drInFlight = dtAllInOutFlights.NewRow();

                    #region 对主键和排序字段赋值
                    //对主键和排序字段赋值
                    //进港部分
                    drInFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //出港部分
                    drInFlight["OutcncDATOP"] = "1900-01-01";
                    drInFlight["OutcnvcFLTID"] = "HU 0000";
                    drInFlight["OutcniLEGNO"] = 100;
                    drInFlight["OutcnvcAC"] = "HH";
                    drInFlight["OutcncAllSTD"] = "";
                    drInFlight["OutcncAllETD"] = "";
                    drInFlight["OutcncAllATD"] = "";
                    drInFlight["OutcncAllTOFF"] = "";
                    drInFlight["OutcncAllStatus"] = "";
                    drInFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //首先将进港机号设置为出港机号
                    if (dtAllInOutFlights.Columns.Contains("OutcnvcLONG_REG"))
                    {
                        drInFlight["OutcnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                    }

                    #region 对用户设置需显示的字段进行赋值
                    //对用户设置需显示的字段进行赋值
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            drInFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }
                    #endregion

                    dtAllInOutFlights.Rows.Add(drInFlight);
                }
                #endregion

                #region 有出港航班
                //有出港航班
                else
                {
                    //修正出港航班状态排序
                    string strOutViewIndex = "";
                    string strStatus = drOutFlights[0]["cncSTATUS"].ToString();
                    if (drOutFlights[0]["cncSTATUS"].ToString() == "ATA" || drOutFlights[0]["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = drOutFlights[0]["cniViewIndex"].ToString();
                    }

                    //新建一行
                    DataRow drInOutFlight = dtAllInOutFlights.NewRow();

                    #region 对主键和排序字段赋值
                    //对主键和排序字段赋值
                    //进港部分
                    drInOutFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInOutFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInOutFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInOutFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInOutFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInOutFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInOutFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInOutFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInOutFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInOutFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //出港部分
                    drInOutFlight["OutcncDATOP"] = drOutFlights[0]["cncDATOP"].ToString();
                    drInOutFlight["OutcnvcFLTID"] = drOutFlights[0]["cnvcFLTID"].ToString();
                    drInOutFlight["OutcniLEGNO"] = drOutFlights[0]["cniLEGNO"].ToString();
                    drInOutFlight["OutcnvcAC"] = drOutFlights[0]["cnvcAC"].ToString();
                    drInOutFlight["OutcncAllSTD"] = drOutFlights[0]["cncSTD"].ToString();
                    drInOutFlight["OutcncAllETD"] = drOutFlights[0]["cncETD"].ToString();
                    drInOutFlight["OutcncAllATD"] = drOutFlights[0]["cncATD"].ToString();
                    drInOutFlight["OutcncAllTOFF"] = drOutFlights[0]["cncTOFF"].ToString();
                    drInOutFlight["OutcncAllStatus"] = drOutFlights[0]["cncSTATUS"].ToString();
                    drInOutFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //对用户设置需显示的字段进行赋值
                    //进港航班部分
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //格式化特殊字段
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //进港机位
                        if (dataRowItems["cnvcDataItemID"].ToString() == "IncnvcInGATE")
                        {
                            if (drOutFlights[0]["cnvcOutGate"].ToString().Trim() == "")
                            {
                                strFieldValue = drOutFlights[0]["cnvcOutGate"].ToString().Trim();
                            }
                        }

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            string strTemp = dataRowItems["cnvcDataItemID"].ToString();
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    //出港航班部分
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //格式化特殊字段
                        string strFieldValue = FormatOUTItem(drOutFlights[0], dataRowItems);

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                        {
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    dtAllInOutFlights.Rows.Add(drInOutFlight);
                    dtOutFlights.Rows.Remove(drOutFlights[0]);
                }
                #endregion
            }
            #endregion

            #region 根据出港航班组合进出港航班
            //根据出港航班组合进出港航班
            //针对仅有出港航班的情况
            foreach (DataRow dataRow in dtOutFlights.Rows)
            {
                //修正出港航班的状态排序
                string strOutViewIndex = "";
                if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                {
                    strOutViewIndex = "2";
                }
                else
                {
                    strOutViewIndex = dataRow["cniViewIndex"].ToString();
                }

                //新建一行
                DataRow drOutFlight = dtAllInOutFlights.NewRow();

                //对主键和排序字段赋值
                drOutFlight["IncncDATOP"] = "1900-01-01";
                drOutFlight["IncnvcFLTID"] = "HU 0000";
                drOutFlight["IncniLEGNO"] = 100;
                drOutFlight["IncnvcAC"] = "HH";
                drOutFlight["IncncAllSTA"] = "";
                drOutFlight["IncncAllETA"] = "";
                drOutFlight["IncncAllTDWN"] = "";
                drOutFlight["IncncAllATA"] = "";
                drOutFlight["IncncAllStatus"] = "";
                drOutFlight["IncniAllViewIndex"] = "0";

                //首先将进港机位设置为出港机位
                if (dtAllInOutFlights.Columns.Contains("IncnvcInGATE"))
                {
                    drOutFlight["IncnvcInGATE"] = dataRow["cnvcOutGate"].ToString();
                }

                //首先将进港机号设置为出港机号
                if (dtAllInOutFlights.Columns.Contains("IncnvcLONG_REG"))
                {
                    drOutFlight["IncnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                }

                //将进港机型设置为出港机型 -- added by LinYong in 20150330
                if (dtAllInOutFlights.Columns.Contains("IncncACTYP") && dtOutFlights.Columns.Contains("cncACTYP"))
                {
                    drOutFlight["IncncACTYP"] = dataRow["cncACTYP"].ToString();
                }

                drOutFlight["OutcncDATOP"] = dataRow["cncDATOP"].ToString();
                drOutFlight["OutcnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                drOutFlight["OutcniLEGNO"] = dataRow["cniLEGNO"].ToString();
                drOutFlight["OutcnvcAC"] = dataRow["cnvcAC"].ToString();
                drOutFlight["OutcncAllSTD"] = dataRow["cncSTD"].ToString();
                drOutFlight["OutcncAllETD"] = dataRow["cncETD"].ToString();
                drOutFlight["OutcncAllATD"] = dataRow["cncATD"].ToString();
                drOutFlight["OutcncAllTOFF"] = dataRow["cncTOFF"].ToString();
                drOutFlight["OutcncAllStatus"] = dataRow["cncSTATUS"].ToString();
                drOutFlight["OutcniAllViewIndex"] = strOutViewIndex;

                //对用户设置需显示的字段进行赋值
                foreach (DataRow dataRowItems in dtDataItems.Rows)
                {
                    //格式化特殊字段
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    //对其他字段进行处理
                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        drOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }

                dtAllInOutFlights.Rows.Add(drOutFlight);
            }
            #endregion

            //根据进出港航班信息表生成航班保障信息实体列表
            foreach (DataRow dataRow in dtAllInOutFlights.Rows)
            {
                ilInOutFlights.Add(new GuaranteeInforBM(dataRow));

            }
            (ilInOutFlights as ArrayList).Sort();

            //对行号赋值
            IEnumerator ieInOutFlights = ilInOutFlights.GetEnumerator();
            int iRowIndex = 0;
            while (ieInOutFlights.MoveNext())
            {
                GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieInOutFlights.Current;
                DataRow[] drInOutFlights = dtAllInOutFlights.Select("IncncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                    "IncnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                    "IncniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                    "IncnvcAC = '" + guaranteeInforBM.IncnvcAC + "' AND " +
                    "OutcncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                    "OutcnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                    "OutcniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                    "OutcnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                //对进出港航班信息表每行的cniRowIndex字段，即行号赋值
                if (drInOutFlights.Length > 0)
                {
                    drInOutFlights[0]["cniRowIndex"] = iRowIndex;
                }
                iRowIndex += 1;
            }


            //返回结果
            return dtAllInOutFlights;
        }
        #endregion

        #region 填充进出港航班状态
        /// <summary>
        /// 填充进出港航班状态
        /// 将航站航班信息表分成进港航班信息表和出港航班信息表
        /// 然后将这两个表再组合成一个进出港航班信息表
        /// </summary>
        /// <param name="dtStationFlights">机场的进出港航班</param>
        /// <param name="dtInOutFlightsSchema">要显示表的架构</param>
        /// <param name="dtDataItems">数据项列表</param>
        /// <param name="stationBM">机场信息</param>
        /// <returns>返回 进出港航班信息表</returns>
        private IList FillInOutFlights_1(DataTable dtStationFlights, DataTable dtInOutFlightsSchema, DataTable dtDataItems, StationBM stationBM)
        {
            IList ilInOutFlights = new ArrayList();

            //进出港航班信息表
            DataTable dtAllInOutFlights = dtInOutFlightsSchema.Clone();

            //分别生成进港航班信息表和出港航班信息表
            //以便稍后组合生成进出港航班信息表
            //进港航班：目的机场三字码与航站三字码相同
            DataTable dtInFlights = dtStationFlights.Clone();
            //出港航班：起飞机场三字码与航站三字码相同
            DataTable dtOutFlights = dtStationFlights.Clone();

            //查询进港航班
            DataRow[] drInFlights = dtStationFlights.Select("cncARRSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTDWN");
            foreach (DataRow dataRow in drInFlights)
            {
                dtInFlights.ImportRow(dataRow);
            }

            //查询出港航班
            DataRow[] drOutFlights = dtStationFlights.Select("cncDEPSTN = '" + stationBM.ThreeCode + "'", "cniViewIndex,cncTOFF");
            foreach (DataRow dataRow in drOutFlights)
            {
                dtOutFlights.ImportRow(dataRow);
            }

            #region 根据进港航班组合进出港航班
            //根据进港航班组合进出港航班
            foreach (DataRow dataRow in dtInFlights.Rows)
            {
                //进港航班的飞机号
                string strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
                //进港航班的预达时间
                string strInETD = dataRow["cncETD"].ToString();

                //查询字符串：根据出港航班的飞机号与进港航班的飞机号相同AND预计起飞时间大于进港航班的预计起飞时间
                string strSearch = "cnvcLONG_REG = '" + strLONG_REG + "' AND cncETD > '" + strInETD + "'";
                drOutFlights = dtOutFlights.Select(strSearch, "cncETD");

                #region 没有出港航班
                //没有出港航班
                if (drOutFlights.Length <= 0)
                {
                    //根据航班状态设置航班显示顺序
                    string strOutViewIndex = "";
                    if (dataRow["cncSTATUS"].ToString() == "CNL")
                    {
                        strOutViewIndex = "0";
                    }
                    else if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = "3";
                    }

                    //新建一行记录
                    DataRow drInFlight = dtAllInOutFlights.NewRow();

                    #region 对主键和排序字段赋值
                    //对主键和排序字段赋值
                    //进港部分
                    drInFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //出港部分
                    drInFlight["OutcncDATOP"] = "1900-01-01";
                    drInFlight["OutcnvcFLTID"] = "HU 0000";
                    drInFlight["OutcniLEGNO"] = 100;
                    drInFlight["OutcnvcAC"] = "HH";
                    drInFlight["OutcncAllSTD"] = "";
                    drInFlight["OutcncAllETD"] = "";
                    drInFlight["OutcncAllATD"] = "";
                    drInFlight["OutcncAllTOFF"] = "";
                    drInFlight["OutcncAllStatus"] = "";
                    drInFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //首先将进港机号设置为出港机号
                    if (dtAllInOutFlights.Columns.Contains("OutcnvcLONG_REG"))
                    {
                        drInFlight["OutcnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                    }

                    #region 对用户设置需显示的字段进行赋值
                    //对用户设置需显示的字段进行赋值
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            drInFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }
                    #endregion

                    dtAllInOutFlights.Rows.Add(drInFlight);
                }
                #endregion

                #region 有出港航班
                //有出港航班
                else
                {
                    //修正出港航班状态排序
                    string strOutViewIndex = "";
                    string strStatus = drOutFlights[0]["cncSTATUS"].ToString();
                    if (drOutFlights[0]["cncSTATUS"].ToString() == "ATA" || drOutFlights[0]["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = drOutFlights[0]["cniViewIndex"].ToString();
                    }

                    //新建一行
                    DataRow drInOutFlight = dtAllInOutFlights.NewRow();

                    #region 对主键和排序字段赋值
                    //对主键和排序字段赋值
                    //进港部分
                    drInOutFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInOutFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInOutFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInOutFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInOutFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInOutFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInOutFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInOutFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInOutFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInOutFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //出港部分
                    drInOutFlight["OutcncDATOP"] = drOutFlights[0]["cncDATOP"].ToString();
                    drInOutFlight["OutcnvcFLTID"] = drOutFlights[0]["cnvcFLTID"].ToString();
                    drInOutFlight["OutcniLEGNO"] = drOutFlights[0]["cniLEGNO"].ToString();
                    drInOutFlight["OutcnvcAC"] = drOutFlights[0]["cnvcAC"].ToString();
                    drInOutFlight["OutcncAllSTD"] = drOutFlights[0]["cncSTD"].ToString();
                    drInOutFlight["OutcncAllETD"] = drOutFlights[0]["cncETD"].ToString();
                    drInOutFlight["OutcncAllATD"] = drOutFlights[0]["cncATD"].ToString();
                    drInOutFlight["OutcncAllTOFF"] = drOutFlights[0]["cncTOFF"].ToString();
                    drInOutFlight["OutcncAllStatus"] = drOutFlights[0]["cncSTATUS"].ToString();
                    drInOutFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //对用户设置需显示的字段进行赋值
                    //进港航班部分
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //格式化特殊字段
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //进港机位
                        if (dataRowItems["cnvcDataItemID"].ToString() == "IncnvcInGATE")
                        {
                            if (drOutFlights[0]["cnvcOutGate"].ToString().Trim() == "")
                            {
                                strFieldValue = drOutFlights[0]["cnvcOutGate"].ToString().Trim();
                            }
                        }

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            string strTemp = dataRowItems["cnvcDataItemID"].ToString();
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    //出港航班部分
                    foreach (DataRow dataRowItems in dtDataItems.Rows)
                    {
                        //格式化特殊字段
                        string strFieldValue = FormatOUTItem(drOutFlights[0], dataRowItems);

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                        {
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    dtAllInOutFlights.Rows.Add(drInOutFlight);
                    dtOutFlights.Rows.Remove(drOutFlights[0]);
                }
                #endregion
            }
            #endregion

            #region 根据出港航班组合进出港航班
            //根据出港航班组合进出港航班
            //针对仅有出港航班的情况
            foreach (DataRow dataRow in dtOutFlights.Rows)
            {
                //修正出港航班的状态排序
                string strOutViewIndex = "";
                if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                {
                    strOutViewIndex = "2";
                }
                else
                {
                    strOutViewIndex = dataRow["cniViewIndex"].ToString();
                }

                //新建一行
                DataRow drOutFlight = dtAllInOutFlights.NewRow();

                //对主键和排序字段赋值
                drOutFlight["IncncDATOP"] = "1900-01-01";
                drOutFlight["IncnvcFLTID"] = "HU 0000";
                drOutFlight["IncniLEGNO"] = 100;
                drOutFlight["IncnvcAC"] = "HH";
                drOutFlight["IncncAllSTA"] = "";
                drOutFlight["IncncAllETA"] = "";
                drOutFlight["IncncAllTDWN"] = "";
                drOutFlight["IncncAllATA"] = "";
                drOutFlight["IncncAllStatus"] = "";
                drOutFlight["IncniAllViewIndex"] = "0";

                //首先将进港机位设置为出港机位
                if (dtAllInOutFlights.Columns.Contains("IncnvcInGATE"))
                {
                    drOutFlight["IncnvcInGATE"] = dataRow["cnvcOutGate"].ToString();
                }

                //首先将进港机号设置为出港机号
                if (dtAllInOutFlights.Columns.Contains("IncnvcLONG_REG"))
                {
                    drOutFlight["IncnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                }

                //将进港机型设置为出港机型 -- added by LinYong in 20150330
                if (dtAllInOutFlights.Columns.Contains("IncncACTYP") && dtOutFlights.Columns.Contains("cncACTYP"))
                {
                    drOutFlight["IncncACTYP"] = dataRow["cncACTYP"].ToString();
                }

                drOutFlight["OutcncDATOP"] = dataRow["cncDATOP"].ToString();
                drOutFlight["OutcnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                drOutFlight["OutcniLEGNO"] = dataRow["cniLEGNO"].ToString();
                drOutFlight["OutcnvcAC"] = dataRow["cnvcAC"].ToString();
                drOutFlight["OutcncAllSTD"] = dataRow["cncSTD"].ToString();
                drOutFlight["OutcncAllETD"] = dataRow["cncETD"].ToString();
                drOutFlight["OutcncAllATD"] = dataRow["cncATD"].ToString();
                drOutFlight["OutcncAllTOFF"] = dataRow["cncTOFF"].ToString();
                drOutFlight["OutcncAllStatus"] = dataRow["cncSTATUS"].ToString();
                drOutFlight["OutcniAllViewIndex"] = strOutViewIndex;

                //对用户设置需显示的字段进行赋值
                foreach (DataRow dataRowItems in dtDataItems.Rows)
                {
                    //格式化特殊字段
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    //对其他字段进行处理
                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        drOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }

                dtAllInOutFlights.Rows.Add(drOutFlight);
            }
            #endregion

            //根据进出港航班信息表生成航班保障信息实体列表
            foreach (DataRow dataRow in dtAllInOutFlights.Rows)
            {
                ilInOutFlights.Add(new GuaranteeInforBM(dataRow));

            }
            (ilInOutFlights as ArrayList).Sort();


            //返回结果
            return ilInOutFlights;
        }
        #endregion

        #region 格式化进港航班特殊字段
        /// <summary>
        /// 格式化进港航班特殊字段
        /// </summary>
        /// <param name="dataRow">航班数据</param>
        /// <param name="dataRowItems">数据项</param>
        /// <returns>格式化后的数据</returns>
        public string FormatINItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //进港航班起飞机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //进港航班到达机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //计划到达时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncSTA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //延误时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncETA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //if (dataRow["cnvcDELAY1"].ToString().Trim() != "")
                //{
                //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //}
                //else
                //{
                //    strFieldValue = "";
                //}
            }
            //落地时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncTDWN")
            {
                //if (strStatus == "DEP" || strStatus == "ARR" || strStatus == "ATA")
                if (strStatus == "ARR" || strStatus == "ATA") //时间落地到达时才显示落地时间
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //到位时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncATA")
            {
                if (strStatus == "ATA")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            return strFieldValue;
        }
        #endregion

        #region 格式化出港航班特殊字段
        /// <summary>
        /// 格式化出港航班特殊字段
        /// </summary>
        /// <param name="dataRow">航班数据</param>
        /// <param name="dataRowItems">数据项</param>
        /// <returns>格式化后的数据</returns>
        public string FormatOUTItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //出港航班起飞机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //出港航班落地机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //计划起飞时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTD")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //起飞延误时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncETD")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //if (dataRow["cnvcDELAY1"].ToString().Trim() != "")
                //{
                //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //}
                //else
                //{
                //    strFieldValue = "";
                //}
            }
            //推出时间
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncATD")
            {
                if (strStatus == "ATD" || strStatus == "DEP" || strStatus == "ATA" || strStatus == "ARR")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //起飞动态短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncTOFF")
            {
                if (strStatus == "DEP" || strStatus == "ATA" || strStatus == "ARR")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //延误代码
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcniDUR1")
            {
                if (strFieldValue == "0")
                {
                    strFieldValue = "";
                }
            }
            return strFieldValue;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            //提取航站过站时间信息
            OverStationTimeBF overStationTimeBF = new OverStationTimeBF();
            ReturnValueSF rvsfOverStationTime = overStationTimeBF.Select();
            if ((rvsfOverStationTime.Result > 0) && (rvsfOverStationTime.Dt != null))
            {
                _dtOverStationTime = rvsfOverStationTime.Dt;
            }
            else
            {
                MessageBox.Show("从数据库中提取航站过站时间信息失败！" +
                    Environment.NewLine + "错误信息：" +
                    rvsfOverStationTime.Message
                    , "提示", MessageBoxButtons.OK);

                Environment.Exit(0); //退出程序
            }

            //昨天、今日和明天的航班组合信息
            FlightAlarmInfoBF flightAlarmInfoBF = new FlightAlarmInfoBF();
            ReturnValueSF rvsfFlightAlarmInfo = flightAlarmInfoBF.Select(
                DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            if ((rvsfFlightAlarmInfo.Result > 0) && (rvsfFlightAlarmInfo.Dt != null))
            {
                _dtTodayInOutFlights = rvsfFlightAlarmInfo.Dt;
            }
            else
            {
                MessageBox.Show("从数据库中提取航班告警信息失败！" +
                    Environment.NewLine + "错误信息：" +
                    rvsfFlightAlarmInfo.Message
                    , "提示", MessageBoxButtons.OK);

                Environment.Exit(0); //退出程序
            }

            //显示 Log 页面
            tabControl1.SelectedTab = tabControl1.TabPages[2]; ;

            //代理服务对象
            if (!blnSetRemotingObject) //还未设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            {
                ReturnValueSF returnValueSF = null;
                AgentServiceBF agentServiceBF = new AgentServiceBF();
                returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                {
                    MessageBox.Show("代理服务对象获取失败，请重新登录！", "提示", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }

                blnSetRemotingObject = true; //设置了远程代理服务对象 AgentServiceDAF.objRemotingObject
            }

            //获取该航站当天的所有航班
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            DateTimeBM dateTimeBM = new DateTimeBM();
            StationBM stationBM = new StationBM();
            AccountBM accountBM = new AccountBM();

            dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            //stationBM.ThreeCode = "HAK";
            //accountBM.UserId = "SYS_FlightMonitor";
            //accountBM.UserName = "系统用户（告警功能）";
            //accountBM.IPAddress = "";
            stationBM.ThreeCode = "PEK";
            accountBM.UserId = "l_w";
            accountBM.UserName = "李炜";
            accountBM.IPAddress = "";

            ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
            DataTable dtTodayStationFlights = rvSF.Dt;

            //获取用户有权限的数据项
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            DataTable dtDataItems = dataItemPurviewBF.GetVisibleDataItem(accountBM).Dt;

            //进出港航班表格Schema
            DataTable dtInOutFlightsSchema = GetDisplaySchema(dtDataItems);

            //获取当天进出港航班 
            //DataTable dtTodayInOutFlights = FillInOutFlights(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);
            IList ilTodayInOutFlights = FillInOutFlights_1(dtTodayStationFlights, dtInOutFlightsSchema, dtDataItems, stationBM);

            //绑定数据
            //DataView dataView  = new DataView(dtTodayInOutFlights, "", "cniRowIndex", DataViewRowState.CurrentRows);
            //dataGridView3.DataSource = dataView;
            dataGridView3.DataSource = ilTodayInOutFlights;

            #region 遍历进出港航班表
            IEnumerator ieTodayInOutFlights = ilTodayInOutFlights.GetEnumerator();
            while (ieTodayInOutFlights.MoveNext())
            {
                string strInDEPSTN = "";
                string strInARRSTN = "";
                string strOutDEPSTN = "";
                string strOutARRSTN = "";
                string strOverStationType = ""; //过站类型：始发、过站、快速过站、航后
                int iOverStationStandardTime = 0; //过站标准时间（分钟）
                string strOverStationStart = ""; //开始时刻
                string strOverStationEnd = ""; //结束时刻
                string strAlarmCode = ""; //告警代码
                string strAlarmValue = ""; //告警值
                int iAlarmResult = 0; //告警结果


                try
                {
                    GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current; //实例化当前进出港航班条

                    #region 人工模拟部分参数
                    iOverStationStandardTime = 45; //模拟 TYN 的过站时间，应该结合 小机型 动态设置
                    strAlarmCode = "OutcncOutPilotArriveTime";
                    #endregion 人工模拟部分参数

                    #region 确定航班起落机场三字码
                    if (guaranteeInforBM.IncncDATOP != "1900-01-01") //进港航班
                    {
                        DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                            "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                            "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                            "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                        if (dataRowFlight.Length > 0)
                        {
                            strInDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                            strInARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                        }
                    }

                    if (guaranteeInforBM.OutcncDATOP != "1900-01-01") //出港航班
                    {
                        DataRow[] dataRowFlight = dtTodayStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                           "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                           "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                           "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                        if (dataRowFlight.Length > 0)
                        {
                            strOutDEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                            strOutARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();

                        }
                    }
                    #endregion 确定航班起落机场三字码

                    #region 确定过站时间
                    DataRow[] dataRowsOverStationTime = _dtOverStationTime.Select(
                        "(cncAirportThreeCode = '" +
                        stationBM.ThreeCode + "') and (cnvcSmallACTYP = '" +
                        guaranteeInforBM.IncncACTYP + "')"); //根据机场三字码和小机型获取过站时间数据
                    if (dataRowsOverStationTime.Length > 0)
                    {
                        iOverStationStandardTime = Convert.ToInt32(dataRowsOverStationTime[0]["cniGroundTime"].ToString());
                    }
                    else
                    {
                        throw new Exception("过站时间表中没有此记录：" + Environment.NewLine +
                            "机场：" + stationBM.ThreeCode + Environment.NewLine +
                            "小机型：" + guaranteeInforBM.IncncACTYP);
                    }
                    #endregion 确定过站时间

                    #region 确定过站类型
                    if ((guaranteeInforBM.IncncDATOP == "1900-01-01") &&
                        (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                    {
                        strOverStationType = "始发";
                    }
                    else if ((guaranteeInforBM.IncncDATOP != "1900-01-01") &&
                        (guaranteeInforBM.OutcncDATOP != "1900-01-01"))
                    {
                        strOverStationType = "过站";
                    }
                    else
                    {
                        strOverStationType = "航后";
                    }
                    #endregion 确定过站类型

                    #region 确定 开始时刻 和 结束时刻
                    if ((strOverStationType == "过站") ||
                        (strOverStationType == "快速过站"))
                    {
                        //计算 开始时刻
                        if (guaranteeInforBM.IncncAllStatus != "ATA")
                        {
                            strOverStationStart = guaranteeInforBM.IncncAllETA;
                        }
                        else
                        {
                            strOverStationStart = guaranteeInforBM.IncncAllATA;
                        }
                        //计算 结束时刻
                        if (Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime) > Convert.ToDateTime(guaranteeInforBM.OutcncAllETD))
                        {
                            strOverStationEnd = Convert.ToDateTime(strOverStationStart).AddMinutes(iOverStationStandardTime).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                        }

                    }
                    else if (strOverStationType == "始发")
                    {
                        //计算 开始时刻
                        strOverStationStart = "";

                        //计算 结束时刻
                        strOverStationEnd = guaranteeInforBM.OutcncAllETD;
                    }
                    else //航后航班不处理
                    {
                        continue;
                    }
                    #endregion 确定 开始时刻 和 结束时刻

                    #region 空勤组到位及时性 判断
                    if (strAlarmCode == "OutcncOutPilotArriveTime")
                    {
                        DateTime dOutPilotArriveTime = new DateTime(); //确定机组应该到位的时刻
                        if ((strOverStationType == "过站") ||
                            (strOverStationType == "快速过站"))
                        {

                            //确定机组应该到位的时刻
                            if (Convert.ToDateTime(strOverStationStart) > Convert.ToDateTime(strOverStationEnd).AddMinutes(-60))
                            {
                                dOutPilotArriveTime = Convert.ToDateTime(strOverStationStart);
                            }
                            else
                            {
                                dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                            }
                        }
                        else if (strOverStationType == "始发")
                        {
                            dOutPilotArriveTime = Convert.ToDateTime(strOverStationEnd).AddMinutes(-60);
                        }
                        //告警值
                        strAlarmValue = guaranteeInforBM.OutcncOutPilotArriveTime;

                        //告警结果 判断（机组应该到位的时刻 往前 5分钟 开始判断）
                        if (DateTime.Now < dOutPilotArriveTime.AddMinutes(-5)) //
                        {
                            iAlarmResult = -1; //还未到判断时间
                        }
                        else
                        {
                            if (guaranteeInforBM.OutcncOutPilotArriveTime.Trim() == "")
                            {
                                iAlarmResult = 1; //已到判断时间，但相应数据项 OutcncOutPilotArriveTime 还未录入数据
                            }
                            else
                            {
                                DateTime dOutcncOutPilotArriveTime = Convert.ToDateTime( //航站保障系统中记录的数据(时间值，如 0930)
                                    guaranteeInforBM.OutcncFlightDate +
                                    guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(0, 2) +
                                    ":" +
                                    guaranteeInforBM.OutcncOutPilotArriveTime.Trim().Substring(2, 2) +
                                    ":00");
                                if (dOutcncOutPilotArriveTime >= Convert.ToDateTime(guaranteeInforBM.OutcncAllETD)) //录入的值是前一天的时间值（由于时间值没有日期部分，需要逻辑判断）
                                {
                                    dOutcncOutPilotArriveTime.AddDays(-1);
                                }

                                if (dOutcncOutPilotArriveTime > dOutPilotArriveTime)
                                {
                                    iAlarmResult = 2; //晚到
                                }
                                else
                                {
                                    iAlarmResult = 0; //准时 
                                }
                            }
                        }
                        //处理内存表 _dtTodayInOutFlights
                        //ReturnValueSF returnValueSF_DealTodayInOutFlights = DealTodayInOutFlights(
                        //    _dtTodayInOutFlights,
                        //    guaranteeInforBM,
                        //    strInDEPSTN,
                        //    strInARRSTN,
                        //    strOutDEPSTN,
                        //    strOutARRSTN,
                        //    strOverStationType,
                        //    iOverStationStandardTime.ToString(),
                        //    strOverStationStart,
                        //    strOverStationEnd,
                        //    strAlarmCode,
                        //    strAlarmValue,
                        //    iAlarmResult.ToString());
                    }
                    #endregion 空勤组到位及时性 判断

                }
                catch (Exception ex)
                {

                }
            }
            #endregion 遍历进出港航班表

            dataGridView4.DataSource = _dtTodayInOutFlights;
        }


        #endregion 航班保障告警分析 用到的函数

        private void button5_Click(object sender, EventArgs e)
        {
            //object[] objArray = new object[2];
            //objArray[0] = 1;
            //objArray[1] = "hello";

            //int iRefreshInterval = 300 * 1000; //调用频率设置为 5分钟
            //TimerCallback timerDelegate = new TimerCallback(Test);
            //timer_CC = new System.Threading.Timer(timerDelegate, objArray, 0, iRefreshInterval);

            //DateTime.Parse(m_outChangeRecordBM.STD).ToUniversalTime().ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
            string dateTimeTest = DateTime.Parse("2015-09-03").ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
            dateTimeTest = dateTimeTest;

        }
        public void Test(object state)
        {
            object[] objArray = (object[])state;
            int i = (int)(objArray[0]);
            string s = (string)(objArray[1]);

            string r = "";
        }
    }
}