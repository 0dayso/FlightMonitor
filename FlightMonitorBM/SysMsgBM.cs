using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// ϵͳ��Ϣ��Ϣ��
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-12-07
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class SysMsgBM
    {
        /// <summary>
        /// fmMDIMain ������ı���
        /// </summary>
        static public string CaptureOffmMDIMain = "";

        /// <summary>
        /// ͬ����Ϣ
        /// </summary>
        static public string TraceInfo_MsgOfShowInDataGridView_2_1 = "";

        /// <summary>
        /// ͬ����Ϣ
        /// </summary>
        static public string TraceInfo_MsgOfShowInDataGridView_2_2 = "";

        /// <summary>
        /// timer1_Tick_1����ĸ�����Ϣ
        /// </summary>
        static public string TraceInfo_timer1_Tick_1 = "";

        /// <summary>
        /// GetLastGuaranteeChangeRecords ��ĸ�����Ϣ
        /// </summary>
        static public string TraceInfo_GetLastGuaranteeChangeRecords_1 = "";

        /// <summary>
        /// ����λ��
        /// </summary>
        static public string TraceInfo_Position = "";

        /// <summary>
        /// fmMDIMain ������ı���
        /// </summary>
        static public string CaptureOffmMDIMain_Taskbook = "";

        /// <summary>
        /// fmMessageService ������ı���
        /// </summary>
        static public string CaptureOffmMessageService = "";

        /// <summary>
        /// ��ȡ���ݵĴ�������ַ
        /// </summary>
        static public string GetAgentIP = "";

        /// <summary>
        /// ��ȡ���ݵĴ������˿�
        /// </summary>
        static public string GetAgentPort = "";

        /// <summary>
        /// �������ݵĴ�������ַ
        /// </summary>
        static public string AgentIP = "";

        /// <summary>
        /// �������ݵĴ������˿�
        /// </summary>
        static public string AgentPort = "";

        /// <summary>
        /// ����㼶
        /// </summary>
        static public string AgentLevel = "";

        /// <summary>
        /// �ӻ�ȡ���ݵĴ������˿ڻ�ȡ����ʱ�������Ƿ�ѹ�����䣬true��ѹ�������򣬲�ѹ�������� AgentLevel = 2 ʱ����Ч��
        /// </summary>
        static public string Compress = "";

        /// <summary>
        /// �ӻ�ȡ���ݵĴ������˿ڻ�ȡ����ʱ�����ʵ�Ƶ�ʣ���������룩������ AgentLevel = 2 ʱ����Ч��
        /// </summary>
        static public string RefreshInterval = "";


        #region CommunicationService ʹ��
        /// <summary>
        /// �ϴ����ļ������λ��
        /// </summary>
        static public string FilePath = "";

        #endregion CommunicationService ʹ��

        /// <summary>
        /// ��Ҫѡ��Ĺ�˾�б���ʽ�� ";HU;CN;"
        /// </summary>
        static public string Companys = ";HU;CN;JD;GS;PN;8L;Y8;FU;UQ;HX;UO;AW;GX;9H;GT;";
        static public string OldCompanys = ";HU;CN;JD;GS;PN;8L;Y8;FU;UQ;HX;UO;AW;GX;9H;GT;";

        #region MessageService ʹ��
        /// <summary>
        /// FlightMonitorϵͳ���ݿ�ʹ�õ�������
        /// </summary>
        static public string FlightMonitorConfig = "dbFlightMonitor_Use";
        #endregion MessageService ʹ��

    }
}
