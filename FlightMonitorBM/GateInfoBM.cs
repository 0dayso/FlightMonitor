using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �ǻ���ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-08-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable]
    public class GateInfoBM
    {
        #region ����
        private string _cncstationthreecode;
        private string _cnvcstationname;
        private string _cnvcgate;
        private string _cnvcgateproperty;
        private string _cnvcmemo;
        private string _cnvcoperationuser;
        private DateTime _cndoperationtime;

        private string _cnvcmemo_1;
        private bool _cnblnsuccess = false;

        #endregion ����


        #region ����
        /// <summary>
        /// ��վ������
        /// </summary>
        public string cncStationThreeCode
        {
            set { _cncstationthreecode = value; }
            get { return _cncstationthreecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cnvcStationName
        {
            set { _cnvcstationname = value; }
            get { return _cnvcstationname; }
        }
        /// <summary>
        /// ͣ��λ
        /// </summary>
        public string cnvcGate
        {
            set { _cnvcgate = value; }
            get { return _cnvcgate; }
        }
        /// <summary>
        /// ���ԣ�����λ �� Զ��λ��
        /// </summary>
        public string cnvcGateProperty
        {
            set { _cnvcgateproperty = value; }
            get { return _cnvcgateproperty; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string cnvcMemo
        {
            set { _cnvcmemo = value; }
            get { return _cnvcmemo; }
        }
        /// <summary>
        /// �����û�
        /// </summary>
        public string cnvcOperationUser
        {
            set { _cnvcoperationuser = value; }
            get { return _cnvcoperationuser; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime cndOperationTime
        {
            set { _cndoperationtime = value; }
            get { return _cndoperationtime; }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Memo
        {
            set { _cnvcmemo_1 = value; }
            get { return _cnvcmemo_1; }
        }

        /// <summary>
        /// ��ֵ�ɹ���־
        /// </summary>
        public bool Success
        {
            set { _cnblnsuccess = value; }
            get { return _cnblnsuccess; }
        }
        #endregion ����

        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public GateInfoBM()
        {
        }
        #endregion ���캯��

        #region ���캯�����ѱ�� tbGateInfo ��һ�� DataRow ���ݸ���ʵ�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">������</param>
        public GateInfoBM(DataRow dataRow)
        {
            try
            {
                _cncstationthreecode = dataRow["cncstationthreecode"].ToString();
                _cnvcstationname = dataRow["cnvcstationname"].ToString();
                _cnvcgate = dataRow["cnvcgate"].ToString();
                _cnvcgateproperty = dataRow["cnvcgateproperty"].ToString();
                _cnvcmemo = dataRow["cnvcmemo"].ToString();
                _cnvcoperationuser = dataRow["cnvcOperationUser"].ToString();
                _cndoperationtime = Convert.ToDateTime(dataRow["cndOperationTime"].ToString());

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcmemo_1 = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion ���캯�����ѱ�� tbGateInfo ��һ�� DataRow ���ݸ���ʵ�����
    }
}
