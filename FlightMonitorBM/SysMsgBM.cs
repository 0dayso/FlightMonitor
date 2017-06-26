using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// 系统消息信息类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-12-07
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class SysMsgBM
    {
        /// <summary>
        /// fmMDIMain 窗体类的标题
        /// </summary>
        static public string CaptureOffmMDIMain = "";

        /// <summary>
        /// 同步信息
        /// </summary>
        static public string TraceInfo_MsgOfShowInDataGridView_2_1 = "";

        /// <summary>
        /// 同步信息
        /// </summary>
        static public string TraceInfo_MsgOfShowInDataGridView_2_2 = "";

        /// <summary>
        /// timer1_Tick_1里面的跟踪消息
        /// </summary>
        static public string TraceInfo_timer1_Tick_1 = "";

        /// <summary>
        /// GetLastGuaranteeChangeRecords 里的跟踪信息
        /// </summary>
        static public string TraceInfo_GetLastGuaranteeChangeRecords_1 = "";

        /// <summary>
        /// 跟踪位置
        /// </summary>
        static public string TraceInfo_Position = "";

        /// <summary>
        /// fmMDIMain 窗体类的标题
        /// </summary>
        static public string CaptureOffmMDIMain_Taskbook = "";

        /// <summary>
        /// fmMessageService 窗体类的标题
        /// </summary>
        static public string CaptureOffmMessageService = "";

        /// <summary>
        /// 获取数据的代理服务地址
        /// </summary>
        static public string GetAgentIP = "";

        /// <summary>
        /// 获取数据的代理服务端口
        /// </summary>
        static public string GetAgentPort = "";

        /// <summary>
        /// 发布数据的代理服务地址
        /// </summary>
        static public string AgentIP = "";

        /// <summary>
        /// 发布数据的代理服务端口
        /// </summary>
        static public string AgentPort = "";

        /// <summary>
        /// 代理层级
        /// </summary>
        static public string AgentLevel = "";

        /// <summary>
        /// 从获取数据的代理服务端口获取数据时，数据是否压缩传输，true：压缩，否则，不压缩。【当 AgentLevel = 2 时才有效】
        /// </summary>
        static public string Compress = "";

        /// <summary>
        /// 从获取数据的代理服务端口获取数据时，访问的频率（间隔多少秒）。【当 AgentLevel = 2 时才有效】
        /// </summary>
        static public string RefreshInterval = "";


        #region CommunicationService 使用
        /// <summary>
        /// 上传的文件保存的位置
        /// </summary>
        static public string FilePath = "";

        #endregion CommunicationService 使用

        /// <summary>
        /// 需要选择的公司列表，格式如 ";HU;CN;"
        /// </summary>
        static public string Companys = ";HU;CN;JD;GS;PN;8L;Y8;FU;UQ;HX;UO;AW;GX;9H;GT;";
        static public string OldCompanys = ";HU;CN;JD;GS;PN;8L;Y8;FU;UQ;HX;UO;AW;GX;9H;GT;";

        #region MessageService 使用
        /// <summary>
        /// FlightMonitor系统数据库使用的配置项
        /// </summary>
        static public string FlightMonitorConfig = "dbFlightMonitor_Use";
        #endregion MessageService 使用

    }
}
