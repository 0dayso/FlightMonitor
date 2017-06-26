using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// ͬ������lockʹ�ã�
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-12-09
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class SynchronizeObjectsBM
    {
        #region �����ڲ�����
        private static object _AgentServiceDAF_dtProcRecords__Lock = new object();  //AgentServiceDAF ���Ա dtProcRecords ��ͬ����
        private static object _AgentServiceDAF_dtProcAnalysis__Lock = new object(); //AgentServiceDAF ���Ա dtProcAnalysis ��ͬ����
        private static object _AgentServiceDAF_dtOnLineUsers__Lock = new object();  //AgentServiceDAF ���Ա dtOnLineUsers ��ͬ����
        #endregion

        #region �������Զ���
        /// <summary>
        /// AgentServiceDAF ���Ա dtProcRecords ��ͬ����
        /// </summary>
        public static object AgentServiceDAF_dtProcRecords__Lock
        {
            get { return _AgentServiceDAF_dtProcRecords__Lock; }
        }
        /// <summary>
        /// AgentServiceDAF ���Ա dtProcAnalysis ��ͬ����
        /// </summary>
        public static object AgentServiceDAF_dtProcAnalysis__Lock
        {
            get { return _AgentServiceDAF_dtProcAnalysis__Lock; }
        }
        /// <summary>
        /// AgentServiceDAF ���Ա dtOnLineUsers ��ͬ����
        /// </summary>
        public static object AgentServiceDAF_dtOnLineUsers__Lock
        {
            get { return _AgentServiceDAF_dtOnLineUsers__Lock; }
        }

        #endregion

    }
}
