using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
   /// <summary>
    /// 在线用户表记录实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-12-03
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable]
    public class OnLineUsersBM
    {
        #region 对象内部变量
        private string _onlineusersid;
        private string _ipaddress;
        private string _userid;
        private string _username;
        private string _hostname;
        private string _logoninfo;
        private string _stationthreecode;
        private int _refreshinterval;
        private DateTime _lastoprationtime;
        private string _result;
        private string _memo;
        private string _commandinfo;
        private string _tipinfo;
        #endregion

        #region 对象属性定义
        /// <summary>
        /// 标识，由IPAddress、UserId和登录时间组合而成
        /// </summary>
        public string OnLineUsersID
        {
            set { _onlineusersid = value; }
            get { return _onlineusersid; }
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress
        {
            set { _ipaddress = value; }
            get { return _ipaddress; }
        }
        /// <summary>
        /// E网帐号
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 主机名，客户端电脑名称。
        /// </summary>
        public string HostName
        {
            set { _hostname = value; }
            get { return _hostname; }
        }
        /// <summary>
        /// domain\user，客户端电脑登录信息。
        /// </summary>
        public string LogOnInfo
        {
            set { _logoninfo = value; }
            get { return _logoninfo; }
        }
        /// <summary>
        /// 航站名
        /// </summary>
        public string StationThreeCode
        {
            set { _stationthreecode = value; }
            get { return _stationthreecode; }
        }
        /// <summary>
        /// 刷新频率（秒）
        /// </summary>
        public int RefreshInterval
        {
            set { _refreshinterval = value; }
            get { return _refreshinterval; }
        }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime LastOprationTime
        {
            set { _lastoprationtime = value; }
            get { return _lastoprationtime; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 命令信息
        /// </summary>
        public string CommandInfo
        {
            set { _commandinfo = value; }
            get { return _commandinfo; }
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string TipInfo
        {
            set { _tipinfo = value; }
            get { return _tipinfo; }
        }
        #endregion

        #region
        #endregion
    }
}
