using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ��վ��վʱ��ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-07-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable]
    public class OverStationTimeBM
    {
        #region ����
        private string _cnvcairportcname;
        private string _cncairportthreecode;
        private string _cnvcbigactyp;
        private string _cnvcsmallactyp;
        private int _cnistandardtime;
        private int _cnigroundtime;

        private string _cnvcmemo;
        private bool _cnblnsuccess = false;
        #endregion ����

        #region ����
        /// <summary>
        /// ����������
        /// </summary>
        public string cnvcAirportCNAME
        {
            set { _cnvcairportcname = value; }
            get { return _cnvcairportcname; }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string cncAirportThreeCode
        {
            set { _cncairportthreecode = value; }
            get { return _cncairportthreecode; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string cnvcBigACTYP
        {
            set { _cnvcbigactyp = value; }
            get { return _cnvcbigactyp; }
        }
        /// <summary>
        /// С���ͣ��� PDCFlight �еĻ���һ�£�
        /// </summary>
        public string cnvcSmallACTYP
        {
            set { _cnvcsmallactyp = value; }
            get { return _cnvcsmallactyp; }
        }
        /// <summary>
        /// ��׼��վʱ��
        /// </summary>
        public int cniStandardTime
        {
            set { _cnistandardtime = value; }
            get { return _cnistandardtime; }
        }
        /// <summary>
        /// �����վʱ��
        /// </summary>
        public int cniGroundTime
        {
            set { _cnigroundtime = value; }
            get { return _cnigroundtime; }
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
        #region ���캯�����ѱ�� tbOverStationTime ��һ�� DataRow ���ݸ���ʵ�����
        /// <summary>
        /// ���캯�����ѱ�� tbOverStationTime ��һ�� DataRow ���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">������</param>
        public OverStationTimeBM(DataRow dataRow)
        {
            try
            {
                _cnvcairportcname = dataRow["cnvcAirportCNAME"].ToString();
                _cncairportthreecode = dataRow["cncAirportThreeCode"].ToString();
                _cnvcbigactyp = dataRow["cnvcBigACTYP"].ToString();
                _cnvcsmallactyp = dataRow["cnvcSmallACTYP"].ToString();
                _cnistandardtime = Convert.ToInt32( dataRow["cniStandardTime"].ToString());
                _cnigroundtime = Convert.ToInt32( dataRow["cniGroundTime"].ToString());

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcmemo = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion ���캯�����ѱ�� tbOverStationTime ��һ�� DataRow ���ݸ���ʵ�����

        #endregion ����
    }
}
