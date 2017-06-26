using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �����ռ�������Ϣʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-05-22
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeRecordCaptionBM
    {
        #region ����
        private int _cniguaranteerecordcaptionid;
        private string _cncdatop;
        private string _cnvcfltid;
        private int _cnilegno;
        private string _cnvcac;
        private string _cncflightdate;
        private string _cnvcflightno;
        private string _cnvclong_reg;
        private string _cncdepstn;
        private string _cncarrstn;
        private string _cncstd;
        private string _cncstation;
        private string _cnvccaption;
        private DateTime _cndoperationtime;
        private string _cnvcuserid;
        private string _cnvcusername;
        private string _cnvcuserdepartment;

        private string _cnvcmemo;
        private bool _cnblnsuccess = false;

        #endregion ����

        #region ����
        /// <summary>
        /// 
        /// </summary>
        public int cniGuaranteeRecordCaptionID
        {
            set { _cniguaranteerecordcaptionid = value; }
            get { return _cniguaranteerecordcaptionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncDATOP
        {
            set { _cncdatop = value; }
            get { return _cncdatop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cnvcFLTID
        {
            set { _cnvcfltid = value; }
            get { return _cnvcfltid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int cniLegNO
        {
            set { _cnilegno = value; }
            get { return _cnilegno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cnvcAC
        {
            set { _cnvcac = value; }
            get { return _cnvcac; }
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
        public string cnvcFlightNo
        {
            set { _cnvcflightno = value; }
            get { return _cnvcflightno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cnvcLONG_REG
        {
            set { _cnvclong_reg = value; }
            get { return _cnvclong_reg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncDEPSTN
        {
            set { _cncdepstn = value; }
            get { return _cncdepstn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncARRSTN
        {
            set { _cncarrstn = value; }
            get { return _cncarrstn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncSTD
        {
            set { _cncstd = value; }
            get { return _cncstd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cncStation
        {
            set { _cncstation = value; }
            get { return _cncstation; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string cnvcCaption
        {
            set { _cnvccaption = value; }
            get { return _cnvccaption; }
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
        /// ������Ա�ʺ�
        /// </summary>
        public string cnvcUserID
        {
            set { _cnvcuserid = value; }
            get { return _cnvcuserid; }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public string cnvcUserName
        {
            set { _cnvcusername = value; }
            get { return _cnvcusername; }
        }
        /// <summary>
        /// ������Ա����
        /// </summary>
        public string cnvcUserDepartment
        {
            set { _cnvcuserdepartment = value; }
            get { return _cnvcuserdepartment; }
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
        public GuaranteeRecordCaptionBM()
        {
        }
        #endregion ���캯��

        #region ���캯�����ѱ�� tbGuaranteeRecordCaption ��һ��  DataRow���ݸ���ʵ�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">�����ռ�������Ϣ������</param>
        public GuaranteeRecordCaptionBM(DataRow dataRow)
        {
            try
            {
                _cniguaranteerecordcaptionid = Convert.ToInt32(dataRow["cniguaranteerecordcaptioni"].ToString());
                _cncdatop = dataRow["cncdatop"].ToString();
                _cnvcfltid = dataRow["cnvcfltid"].ToString();
                _cnilegno = Convert.ToInt32(dataRow["cnilegno"].ToString());
                _cnvcac = dataRow["cnvcac"].ToString();
                _cncflightdate = dataRow["cncflightdate"].ToString();
                _cnvcflightno = dataRow["cnvcflightno"].ToString();
                _cnvclong_reg = dataRow["cnvclong_reg"].ToString();
                _cncdepstn = dataRow["cncdepstn"].ToString();
                _cncarrstn = dataRow["cncarrstn"].ToString();
                _cncstd = dataRow["cncstd"].ToString();
                _cncstation = dataRow["cncstation"].ToString();
                _cnvccaption = dataRow["cnvccaption"].ToString();
                _cndoperationtime = Convert.ToDateTime(dataRow["cndoperationtime"].ToString());
                _cnvcuserid = dataRow["cnvcuserid"].ToString();
                _cnvcusername = dataRow["cnvcusername"].ToString();
                _cnvcuserdepartment = dataRow["cnvcuserdepartment"].ToString();

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcmemo = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion ���캯������һ�� VoyageReport DataRow���ݸ���ʵ�����

        #endregion ����
    }
}
