using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航班告警信息实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-07
    /// 修 改 人：
    /// 修改日期：2015-12-14
    /// 版    本：
    public class FlightAlarmInfoBM
    {
        #region 声明
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
        #endregion 声明

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int cniFlightAlarmInfoID
        {
            set { _cniflightalarminfoid = value; }
            get { return _cniflightalarminfoid; }
        }
        /// <summary>
        /// 进港日期(UTC)
        /// </summary>
        public string cncInDATOP
        {
            set { _cncindatop = value; }
            get { return _cncindatop; }
        }
        /// <summary>
        /// 进港日期(北京时)
        /// </summary>
        public string cncInFlightDate
        {
            set { _cncinflightdate = value; }
            get { return _cncinflightdate; }
        }
        /// <summary>
        /// 进港航班号
        /// </summary>
        public string cnvcInFLTID
        {
            set { _cnvcinfltid = value; }
            get { return _cnvcinfltid; }
        }
        /// <summary>
        /// 进港航段号
        /// </summary>
        public int cniInLEGNO
        {
            set { _cniinlegno = value; }
            get { return _cniinlegno; }
        }
        /// <summary>
        /// 进港飞机代码
        /// </summary>
        public string cnvcInAC
        {
            set { _cnvcinac = value; }
            get { return _cnvcinac; }
        }
        /// <summary>
        /// 进港飞机号
        /// </summary>
        public string cnvcInLONG_REG
        {
            set { _cnvcinlong_reg = value; }
            get { return _cnvcinlong_reg; }
        }
        /// <summary>
        /// 进港航班起飞机场
        /// </summary>
        public string cncInDEPSTN
        {
            set { _cncindepstn = value; }
            get { return _cncindepstn; }
        }
        /// <summary>
        /// 进港航班降落机场
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
        /// 进港航班状态
        /// </summary>
        public string cncInSTATUS
        {
            set { _cncinstatus = value; }
            get { return _cncinstatus; }
        }
        /// <summary>
        /// 出港日期(UTC)
        /// </summary>
        public string cncOutDATOP
        {
            set { _cncoutdatop = value; }
            get { return _cncoutdatop; }
        }
        /// <summary>
        /// 出港日期(北京时)
        /// </summary>
        public string cncOutFlightDate
        {
            set { _cncoutflightdate = value; }
            get { return _cncoutflightdate; }
        }
        /// <summary>
        /// 出港航班号
        /// </summary>
        public string cnvcOutFLTID
        {
            set { _cnvcoutfltid = value; }
            get { return _cnvcoutfltid; }
        }
        /// <summary>
        /// 出港航段号
        /// </summary>
        public int cniOutLEGNO
        {
            set { _cnioutlegno = value; }
            get { return _cnioutlegno; }
        }
        /// <summary>
        /// 出港飞机代码
        /// </summary>
        public string cnvcOutAC
        {
            set { _cnvcoutac = value; }
            get { return _cnvcoutac; }
        }
        /// <summary>
        /// 出港飞机号
        /// </summary>
        public string cnvcOutLONG_REG
        {
            set { _cnvcoutlong_reg = value; }
            get { return _cnvcoutlong_reg; }
        }
        /// <summary>
        /// 出港航班起飞机场
        /// </summary>
        public string cncOutDEPSTN
        {
            set { _cncoutdepstn = value; }
            get { return _cncoutdepstn; }
        }
        /// <summary>
        /// 出港航班降落机场
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
        /// 出港航班状态
        /// </summary>
        public string cncOutSTATUS
        {
            set { _cncoutstatus = value; }
            get { return _cncoutstatus; }
        }
        /// <summary>
        /// 滑出时间
        /// </summary>
        public int cniTaxiOutMinutes
        {
            set { _cnitaxioutminutes = value; }
            get { return _cnitaxioutminutes; }
        }
        /// <summary>
        /// 过站类型：始发、过站、快速过站、航后
        /// </summary>
        public string cnvcOverStationType
        {
            set { _cnvcoverstationtype = value; }
            get { return _cnvcoverstationtype; }
        }
        /// <summary>
        /// 标准过站时间（分钟）
        /// </summary>
        public int cniOverStationStandardTime
        {
            set { _cnioverstationstandardtime = value; }
            get { return _cnioverstationstandardtime; }
        }
        /// <summary>
        /// 计算得到的过站开始时刻
        /// </summary>
        public string cncOverStationStart
        {
            set { _cncoverstationstart = value; }
            get { return _cncoverstationstart; }
        }
        /// <summary>
        /// 计算得到的过站结束时刻
        /// </summary>
        public string cncOverStationEnd
        {
            set { _cncoverstationend = value; }
            get { return _cncoverstationend; }
        }
        /// <summary>
        /// 告警代码，一般为保障表tbGuaranteeInfor中的字段名
        /// </summary>
        public string cnvcAlarmCode
        {
            set { _cnvcalarmcode = value; }
            get { return _cnvcalarmcode; }
        }
        /// <summary>
        /// 告警项的值
        /// </summary>
        public string cnvcAlarmValue
        {
            set { _cnvcalarmvalue = value; }
            get { return _cnvcalarmvalue; }
        }
        /// <summary>
        /// 告警位置的值
        /// </summary>
        public string cnvcAlarmPoint
        {
            set { _cnvcalarmpoint = value; }
            get { return _cnvcalarmpoint; }
        }
        /// <summary>
        /// 告警结果：0，无告警；1，告警。
        /// </summary>
        public int cniAlarmResult
        {
            set { _cnialarmresult = value; }
            get { return _cnialarmresult; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string cnvcMemo
        {
            set { _cnvcmemo = value; }
            get { return _cnvcmemo; }
        }
        /// <summary>
        /// 此条记录最后的更新时间
        /// </summary>
        public DateTime cndOperationTime
        {
            set { _cndoperationtime = value; }
            get { return _cndoperationtime; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Memo
        {
            set { _cnvcmemo_1 = value; }
            get { return _cnvcmemo_1; }
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
        public FlightAlarmInfoBM()
        {
        }
        #endregion 构造函数

        #region 构造函数：把一行 tbFlightAlarmInfo DataRow数据赋给实体对象
        /// <summary>
        /// 构造函数：把一行 tbFlightAlarmInfo DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">航班告警数据行</param>
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
        #endregion 构造函数：把一行 tbFlightAlarmInfo DataRow数据赋给实体对象

        #endregion 函数
    }
}
