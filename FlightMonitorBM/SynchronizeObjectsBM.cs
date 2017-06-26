using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
    /// <summary>
    /// 同步对象（lock使用）
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-12-09
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class SynchronizeObjectsBM
    {
        #region 对象内部变量
        private static object _AgentServiceDAF_dtProcRecords__Lock = new object();  //AgentServiceDAF 类成员 dtProcRecords 的同步锁
        private static object _AgentServiceDAF_dtProcAnalysis__Lock = new object(); //AgentServiceDAF 类成员 dtProcAnalysis 的同步锁
        private static object _AgentServiceDAF_dtOnLineUsers__Lock = new object();  //AgentServiceDAF 类成员 dtOnLineUsers 的同步锁
        #endregion

        #region 对象属性定义
        /// <summary>
        /// AgentServiceDAF 类成员 dtProcRecords 的同步锁
        /// </summary>
        public static object AgentServiceDAF_dtProcRecords__Lock
        {
            get { return _AgentServiceDAF_dtProcRecords__Lock; }
        }
        /// <summary>
        /// AgentServiceDAF 类成员 dtProcAnalysis 的同步锁
        /// </summary>
        public static object AgentServiceDAF_dtProcAnalysis__Lock
        {
            get { return _AgentServiceDAF_dtProcAnalysis__Lock; }
        }
        /// <summary>
        /// AgentServiceDAF 类成员 dtOnLineUsers 的同步锁
        /// </summary>
        public static object AgentServiceDAF_dtOnLineUsers__Lock
        {
            get { return _AgentServiceDAF_dtOnLineUsers__Lock; }
        }

        #endregion

    }
}
