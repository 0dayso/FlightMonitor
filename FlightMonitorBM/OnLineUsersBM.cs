using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.AgentServiceBM
{
   /// <summary>
    /// �����û����¼ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-12-03
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable]
    public class OnLineUsersBM
    {
        #region �����ڲ�����
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

        #region �������Զ���
        /// <summary>
        /// ��ʶ����IPAddress��UserId�͵�¼ʱ����϶���
        /// </summary>
        public string OnLineUsersID
        {
            set { _onlineusersid = value; }
            get { return _onlineusersid; }
        }
        /// <summary>
        /// IP��ַ
        /// </summary>
        public string IPAddress
        {
            set { _ipaddress = value; }
            get { return _ipaddress; }
        }
        /// <summary>
        /// E���ʺ�
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// ���������ͻ��˵������ơ�
        /// </summary>
        public string HostName
        {
            set { _hostname = value; }
            get { return _hostname; }
        }
        /// <summary>
        /// domain\user���ͻ��˵��Ե�¼��Ϣ��
        /// </summary>
        public string LogOnInfo
        {
            set { _logoninfo = value; }
            get { return _logoninfo; }
        }
        /// <summary>
        /// ��վ��
        /// </summary>
        public string StationThreeCode
        {
            set { _stationthreecode = value; }
            get { return _stationthreecode; }
        }
        /// <summary>
        /// ˢ��Ƶ�ʣ��룩
        /// </summary>
        public int RefreshInterval
        {
            set { _refreshinterval = value; }
            get { return _refreshinterval; }
        }
        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime LastOprationTime
        {
            set { _lastoprationtime = value; }
            get { return _lastoprationtime; }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public string Result
        {
            set { _result = value; }
            get { return _result; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string CommandInfo
        {
            set { _commandinfo = value; }
            get { return _commandinfo; }
        }
        /// <summary>
        /// ��ʾ��Ϣ
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
