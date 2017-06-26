using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ��վͳ����Ϣ����վ��������������������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-08-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable]
    public class OverStationStatisticsBM
    {
        #region ����
        private string _cnvcstationthreecode;
        private string _cnvcstationname;
        private string _cncflightdate;
        private int _cnioverstationcount;
        private int _cnioverstationusebridgecount;

        private string _cnvcmemo;
        private bool _cnblnsuccess = false;
        #endregion ����

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public string cnvcStationThreeCode
        {
            set { _cnvcstationthreecode = value; }
            get { return _cnvcstationthreecode; }
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
        /// 
        /// </summary>
        public string cncFlightDate
        {
            set { _cncflightdate = value; }
            get { return _cncflightdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int cniOverStationCount
        {
            set { _cnioverstationcount = value; }
            get { return _cnioverstationcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int cniOverStationUseBridgeCount
        {
            set { _cnioverstationusebridgecount = value; }
            get { return _cnioverstationusebridgecount; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Memo
        {
            set { _cnvcmemo = value; }
            get { return _cnvcmemo; }
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

        #region ����
        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public OverStationStatisticsBM()
        {
        }
        #endregion ���캯��

        #region ���캯�����ѱ�� tbOverStationStatistics ��һ�� DataRow ���ݸ���ʵ�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">������</param>
        public OverStationStatisticsBM(DataRow dataRow)
        {
            try
            {
                _cnvcstationthreecode = dataRow["cnvcstationthreecode"].ToString();
                _cnvcstationname = dataRow["cnvcstationname"].ToString();
                _cncflightdate = dataRow["cncflightdate"].ToString();
                _cnioverstationcount = Convert.ToInt32( dataRow["cnioverstationcount"].ToString());
                _cnioverstationusebridgecount = Convert.ToInt32( dataRow["cnioverstationusebridgecount"].ToString());

                _cnvcmemo = "";
                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcmemo = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion ���캯�����ѱ�� tbOverStationStatistics ��һ�� DataRow ���ݸ���ʵ�����
        #endregion ����
    }
}
