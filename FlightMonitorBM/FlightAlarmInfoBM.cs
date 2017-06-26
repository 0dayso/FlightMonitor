using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ����澯��Ϣʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-07-07
    /// �� �� �ˣ�
    /// �޸����ڣ�2015-12-14
    /// ��    ����
    public class FlightAlarmInfoBM
    {
        #region ����
        private int _cniflightalarminfoid;
        private string _cncindatop;
        private string _cncinflightdate;
        private string _cnvcinfltid;
        private int _cniinlegno;
        private string _cnvcinac;
        private string _cnvcinlong_reg;
        private string _cncindepstn;
        private string _cncinarrstn;
        private string _cncinsta;
        private string _cncineta;
        private string _cncintdwn;
        private string _cncinata;
        private string _cncinstatus;
        private string _cncoutdatop;
        private string _cncoutflightdate;
        private string _cnvcoutfltid;
        private int _cnioutlegno;
        private string _cnvcoutac;
        private string _cnvcoutlong_reg;
        private string _cncoutdepstn;
        private string _cncoutarrstn;
        private string _cncoutstd;
        private string _cncoutetd;
        private string _cncouttoff;
        private string _cncoutatd;
        private string _cncoutstatus;
        private int _cnitaxioutminutes;
        private string _cnvcoverstationtype;
        private int _cnioverstationstandardtime;
        private string _cncoverstationstart;
        private string _cncoverstationend;
        private string _cnvcalarmcode;
        private string _cnvcalarmvalue;
        private string _cnvcalarmpoint;
        private int _cnialarmresult;
        private string _cnvcmemo;
        private DateTime _cndoperationtime;

        private string _cnvcmemo_1;
        private bool _cnblnsuccess = false;
        #endregion ����

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public int cniFlightAlarmInfoID
        {
            set { _cniflightalarminfoid = value; }
            get { return _cniflightalarminfoid; }
        }
        /// <summary>
        /// ��������(UTC)
        /// </summary>
        public string cncInDATOP
        {
            set { _cncindatop = value; }
            get { return _cncindatop; }
        }
        /// <summary>
        /// ��������(����ʱ)
        /// </summary>
        public string cncInFlightDate
        {
            set { _cncinflightdate = value; }
            get { return _cncinflightdate; }
        }
        /// <summary>
        /// ���ۺ����
        /// </summary>
        public string cnvcInFLTID
        {
            set { _cnvcinfltid = value; }
            get { return _cnvcinfltid; }
        }
        /// <summary>
        /// ���ۺ��κ�
        /// </summary>
        public int cniInLEGNO
        {
            set { _cniinlegno = value; }
            get { return _cniinlegno; }
        }
        /// <summary>
        /// ���۷ɻ�����
        /// </summary>
        public string cnvcInAC
        {
            set { _cnvcinac = value; }
            get { return _cnvcinac; }
        }
        /// <summary>
        /// ���۷ɻ���
        /// </summary>
        public string cnvcInLONG_REG
        {
            set { _cnvcinlong_reg = value; }
            get { return _cnvcinlong_reg; }
        }
        /// <summary>
        /// ���ۺ�����ɻ���
        /// </summary>
        public string cncInDEPSTN
        {
            set { _cncindepstn = value; }
            get { return _cncindepstn; }
        }
        /// <summary>
        /// ���ۺ��ཱུ�����
        /// </summary>
        public string cncInARRSTN
        {
            set { _cncinarrstn = value; }
            get { return _cncinarrstn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncInSTA
        {
            set { _cncinsta = value; }
            get { return _cncinsta; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncInETA
        {
            set { _cncineta = value; }
            get { return _cncineta; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncInTDWN
        {
            set { _cncintdwn = value; }
            get { return _cncintdwn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncInATA
        {
            set { _cncinata = value; }
            get { return _cncinata; }
        }
        /// <summary>
        /// ���ۺ���״̬
        /// </summary>
        public string cncInSTATUS
        {
            set { _cncinstatus = value; }
            get { return _cncinstatus; }
        }
        /// <summary>
        /// ��������(UTC)
        /// </summary>
        public string cncOutDATOP
        {
            set { _cncoutdatop = value; }
            get { return _cncoutdatop; }
        }
        /// <summary>
        /// ��������(����ʱ)
        /// </summary>
        public string cncOutFlightDate
        {
            set { _cncoutflightdate = value; }
            get { return _cncoutflightdate; }
        }
        /// <summary>
        /// ���ۺ����
        /// </summary>
        public string cnvcOutFLTID
        {
            set { _cnvcoutfltid = value; }
            get { return _cnvcoutfltid; }
        }
        /// <summary>
        /// ���ۺ��κ�
        /// </summary>
        public int cniOutLEGNO
        {
            set { _cnioutlegno = value; }
            get { return _cnioutlegno; }
        }
        /// <summary>
        /// ���۷ɻ�����
        /// </summary>
        public string cnvcOutAC
        {
            set { _cnvcoutac = value; }
            get { return _cnvcoutac; }
        }
        /// <summary>
        /// ���۷ɻ���
        /// </summary>
        public string cnvcOutLONG_REG
        {
            set { _cnvcoutlong_reg = value; }
            get { return _cnvcoutlong_reg; }
        }
        /// <summary>
        /// ���ۺ�����ɻ���
        /// </summary>
        public string cncOutDEPSTN
        {
            set { _cncoutdepstn = value; }
            get { return _cncoutdepstn; }
        }
        /// <summary>
        /// ���ۺ��ཱུ�����
        /// </summary>
        public string cncOutARRSTN
        {
            set { _cncoutarrstn = value; }
            get { return _cncoutarrstn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncOutSTD
        {
            set { _cncoutstd = value; }
            get { return _cncoutstd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncOutETD
        {
            set { _cncoutetd = value; }
            get { return _cncoutetd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncOutTOFF
        {
            set { _cncouttoff = value; }
            get { return _cncouttoff; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncOutATD
        {
            set { _cncoutatd = value; }
            get { return _cncoutatd; }
        }
        /// <summary>
        /// ���ۺ���״̬
        /// </summary>
        public string cncOutSTATUS
        {
            set { _cncoutstatus = value; }
            get { return _cncoutstatus; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int cniTaxiOutMinutes
        {
            set { _cnitaxioutminutes = value; }
            get { return _cnitaxioutminutes; }
        }
        /// <summary>
        /// ��վ���ͣ�ʼ������վ�����ٹ�վ������
        /// </summary>
        public string cnvcOverStationType
        {
            set { _cnvcoverstationtype = value; }
            get { return _cnvcoverstationtype; }
        }
        /// <summary>
        /// ��׼��վʱ�䣨���ӣ�
        /// </summary>
        public int cniOverStationStandardTime
        {
            set { _cnioverstationstandardtime = value; }
            get { return _cnioverstationstandardtime; }
        }
        /// <summary>
        /// ����õ��Ĺ�վ��ʼʱ��
        /// </summary>
        public string cncOverStationStart
        {
            set { _cncoverstationstart = value; }
            get { return _cncoverstationstart; }
        }
        /// <summary>
        /// ����õ��Ĺ�վ����ʱ��
        /// </summary>
        public string cncOverStationEnd
        {
            set { _cncoverstationend = value; }
            get { return _cncoverstationend; }
        }
        /// <summary>
        /// �澯���룬һ��Ϊ���ϱ�tbGuaranteeInfor�е��ֶ���
        /// </summary>
        public string cnvcAlarmCode
        {
            set { _cnvcalarmcode = value; }
            get { return _cnvcalarmcode; }
        }
        /// <summary>
        /// �澯���ֵ
        /// </summary>
        public string cnvcAlarmValue
        {
            set { _cnvcalarmvalue = value; }
            get { return _cnvcalarmvalue; }
        }
        /// <summary>
        /// �澯λ�õ�ֵ
        /// </summary>
        public string cnvcAlarmPoint
        {
            set { _cnvcalarmpoint = value; }
            get { return _cnvcalarmpoint; }
        }
        /// <summary>
        /// �澯�����0���޸澯��1���澯��
        /// </summary>
        public int cniAlarmResult
        {
            set { _cnialarmresult = value; }
            get { return _cnialarmresult; }
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
        /// ������¼���ĸ���ʱ��
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

        #region ����
        
        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public FlightAlarmInfoBM()
        {
        }
        #endregion ���캯��

        #region ���캯������һ�� tbFlightAlarmInfo DataRow���ݸ���ʵ�����
        /// <summary>
        /// ���캯������һ�� tbFlightAlarmInfo DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">����澯������</param>
        public FlightAlarmInfoBM(DataRow dataRow)
        {
            try
            {
                try
                {
                    _cniflightalarminfoid = Convert.ToInt32(dataRow["cniFlightAlarmInfoID"].ToString());
                }
                catch
                {
                }
                _cncindatop = dataRow["cncInDATOP"].ToString();
                _cncinflightdate = dataRow["cncInFlightDate"].ToString();
                _cnvcinfltid = dataRow["cnvcInFLTID"].ToString();
                _cniinlegno = Convert.ToInt32(dataRow["cniInLEGNO"].ToString());
                _cnvcinac = dataRow["cnvcInAC"].ToString();
                _cnvcinlong_reg = dataRow["cnvcInLONG_REG"].ToString();
                _cncindepstn = dataRow["cncInDEPSTN"].ToString();
                _cncinarrstn = dataRow["cncInARRSTN"].ToString();
                _cncinsta = dataRow["cncInSTA"].ToString();
                _cncineta = dataRow["cncInETA"].ToString();
                _cncintdwn = dataRow["cncInTDWN"].ToString();
                _cncinata = dataRow["cncInATA"].ToString();
                _cncinstatus = dataRow["cncInSTATUS"].ToString();
                _cncoutdatop = dataRow["cncOutDATOP"].ToString();
                _cncoutflightdate = dataRow["cncOutFlightDate"].ToString();
                _cnvcoutfltid = dataRow["cnvcOutFLTID"].ToString();
                _cnioutlegno = Convert.ToInt32(dataRow["cniOutLEGNO"].ToString());
                _cnvcoutac = dataRow["cnvcOutAC"].ToString();
                _cnvcoutlong_reg = dataRow["cnvcOutLONG_REG"].ToString();
                _cncoutdepstn = dataRow["cncOutDEPSTN"].ToString();
                _cncoutarrstn = dataRow["cncOutARRSTN"].ToString();
                _cncoutstd = dataRow["cncOutSTD"].ToString();
                _cncoutetd = dataRow["cncOutETD"].ToString();
                _cncouttoff = dataRow["cncOutTOFF"].ToString();
                _cncoutatd = dataRow["cncOutATD"].ToString();
                _cncoutstatus = dataRow["cncOutSTATUS"].ToString();
                _cnitaxioutminutes = Convert.ToInt32(dataRow["cniTaxiOutMinutes"].ToString());
                _cnvcoverstationtype = dataRow["cnvcOverStationType"].ToString();
                _cnioverstationstandardtime = Convert.ToInt32(dataRow["cniOverStationStandardTime"].ToString());
                _cncoverstationstart = dataRow["cncOverStationStart"].ToString();
                _cncoverstationend = dataRow["cncOverStationEnd"].ToString();
                _cnvcalarmcode = dataRow["cnvcAlarmCode"].ToString();
                _cnvcalarmvalue = dataRow["cnvcAlarmValue"].ToString();
                _cnvcalarmpoint = dataRow["cnvcAlarmPoint"].ToString();
                _cnialarmresult = Convert.ToInt32( dataRow["cniAlarmResult"].ToString());
                _cnvcmemo = dataRow["cnvcMemo"].ToString();
                _cndoperationtime = Convert.ToDateTime(dataRow["cndOperationTime"].ToString());                      

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcmemo_1 = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion ���캯������һ�� tbFlightAlarmInfo DataRow���ݸ���ʵ�����

        #endregion ����
    }
}
