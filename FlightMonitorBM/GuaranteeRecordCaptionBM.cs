using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 保障日记主题信息实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-05-22
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeRecordCaptionBM
    {
        #region 声明
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

        #endregion 声明

        #region 属性
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
        /// 保障主题
        /// </summary>
        public string cnvcCaption
        {
            set { _cnvccaption = value; }
            get { return _cnvccaption; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime cndOperationTime
        {
            set { _cndoperationtime = value; }
            get { return _cndoperationtime; }
        }
        /// <summary>
        /// 操作人员帐号
        /// </summary>
        public string cnvcUserID
        {
            set { _cnvcuserid = value; }
            get { return _cnvcuserid; }
        }
        /// <summary>
        /// 操作人员姓名
        /// </summary>
        public string cnvcUserName
        {
            set { _cnvcusername = value; }
            get { return _cnvcusername; }
        }
        /// <summary>
        /// 操作人员部门
        /// </summary>
        public string cnvcUserDepartment
        {
            set { _cnvcuserdepartment = value; }
            get { return _cnvcuserdepartment; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Memo
        {
            set { _cnvcmemo = value; }
            get { return _cnvcmemo; }
        }

        /// <summary>
        /// 赋值成功标志
        /// </summary>
        public bool Success
        {
            set { _cnblnsuccess = value; }
            get { return _cnblnsuccess; }
        }

        #endregion 属性

        #region 函数

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public GuaranteeRecordCaptionBM()
        {
        }
        #endregion 构造函数

        #region 构造函数：把表格 tbGuaranteeRecordCaption 的一行  DataRow数据赋给实体对象
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">保障日记主题信息数据行</param>
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
        #endregion 构造函数：把一行 VoyageReport DataRow数据赋给实体对象

        #endregion 函数
    }
}
