using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航站进出港航班实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-08-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable]
    public class StationFlightInfoBM
    {
        #region 声明
        private string _cnvcstationthreecode;
		private string _cnvcstationname;
		private int _cnirowindex;
		private string _cncindatop;
		private string _cnvcinfltid;
		private int _cniinlegno;
		private string _cnvcinac;
		private string _cncinflightdate;
		private string _cnvcinflightno;
		private string _cnvcinlong_reg;
		private string _cncindepstn;
		private string _cncindepairportcname;
		private string _cncinarrstn;
		private string _cncinarrairportcname;
		private string _cncineta;
		private string _cnvcingate;
		private string _cnvcingateproperty;
		private string _cnvcoutgate;
		private string _cnvcoutgateproperty;
		private string _cncoutdatop;
		private string _cnvcoutfltid;
		private int _cnioutlegno;
		private string _cnvcoutac;
		private string _cncoutflightdate;
		private string _cnvoutcflightno;
		private string _cnvcoutlong_reg;
		private string _cncoutdepstn;
		private string _cncoutdepairportcname;
		private string _cncoutarrstn;
		private string _cncoutarrairportcname;
		private string _cnvcflightproperty;

        private string _cnvcmemo;
        private bool _cnblnsuccess = false;
        #endregion 声明

        #region 属性
		/// <summary>
		/// 
		/// </summary>
		public string cnvcStationThreeCode
		{
			set{ _cnvcstationthreecode=value;}
			get{return _cnvcstationthreecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcStationName
		{
			set{ _cnvcstationname=value;}
			get{return _cnvcstationname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cniRowIndex
		{
			set{ _cnirowindex=value;}
			get{return _cnirowindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncInDATOP
		{
			set{ _cncindatop=value;}
			get{return _cncindatop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcInFLTID
		{
			set{ _cnvcinfltid=value;}
			get{return _cnvcinfltid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cniInLEGNO
		{
			set{ _cniinlegno=value;}
			get{return _cniinlegno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcInAC
		{
			set{ _cnvcinac=value;}
			get{return _cnvcinac;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncInFlightDate
		{
			set{ _cncinflightdate=value;}
			get{return _cncinflightdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcInFlightNo
		{
			set{ _cnvcinflightno=value;}
			get{return _cnvcinflightno;}
		}
		/// <summary>
		/// 进港机号
		/// </summary>
		public string cnvcInLONG_REG
		{
			set{ _cnvcinlong_reg=value;}
			get{return _cnvcinlong_reg;}
		}
		/// <summary>
		/// 进港机场|起飞
		/// </summary>
		public string cncInDEPSTN
		{
			set{ _cncindepstn=value;}
			get{return _cncindepstn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncInDEPAirportCNAME
		{
			set{ _cncindepairportcname=value;}
			get{return _cncindepairportcname;}
		}
		/// <summary>
		/// 进港机场|起飞
		/// </summary>
		public string cncInARRSTN
		{
			set{ _cncinarrstn=value;}
			get{return _cncinarrstn;}
		}
		/// <summary>
		/// 进港机场|起飞
		/// </summary>
		public string cncInARRAirportCNAME
		{
			set{ _cncinarrairportcname=value;}
			get{return _cncinarrairportcname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncInETA
		{
			set{ _cncineta=value;}
			get{return _cncineta;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcInGATE
		{
			set{ _cnvcingate=value;}
			get{return _cnvcingate;}
		}
		/// <summary>
		/// 机位属性，近机位 或 远机位
		/// </summary>
		public string cnvcInGateProperty
		{
			set{ _cnvcingateproperty=value;}
			get{return _cnvcingateproperty;}
		}
		/// <summary>
		/// 出港机位
		/// </summary>
		public string cnvcOutGate
		{
			set{ _cnvcoutgate=value;}
			get{return _cnvcoutgate;}
		}
		/// <summary>
		/// 机位属性，近机位 或 远机位
		/// </summary>
		public string cnvcOutGateProperty
		{
			set{ _cnvcoutgateproperty=value;}
			get{return _cnvcoutgateproperty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncOutDATOP
		{
			set{ _cncoutdatop=value;}
			get{return _cncoutdatop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcOutFLTID
		{
			set{ _cnvcoutfltid=value;}
			get{return _cnvcoutfltid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cniOutLEGNO
		{
			set{ _cnioutlegno=value;}
			get{return _cnioutlegno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcOutAC
		{
			set{ _cnvcoutac=value;}
			get{return _cnvcoutac;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncOutFlightDate
		{
			set{ _cncoutflightdate=value;}
			get{return _cncoutflightdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvOutcFlightNo
		{
			set{ _cnvoutcflightno=value;}
			get{return _cnvoutcflightno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcOutLONG_REG
		{
			set{ _cnvcoutlong_reg=value;}
			get{return _cnvcoutlong_reg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncOutDEPSTN
		{
			set{ _cncoutdepstn=value;}
			get{return _cncoutdepstn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncOutDEPAirportCNAME
		{
			set{ _cncoutdepairportcname=value;}
			get{return _cncoutdepairportcname;}
		}
		/// <summary>
		/// 进港机场|到达
		/// </summary>
		public string cncOutARRSTN
		{
			set{ _cncoutarrstn=value;}
			get{return _cncoutarrstn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cncOutARRAirportCNAME
		{
			set{ _cncoutarrairportcname=value;}
			get{return _cncoutarrairportcname;}
		}
		/// <summary>
		/// 航班属性，始发、过站或航后。
		/// </summary>
		public string cnvcFlightProperty
		{
			set{ _cnvcflightproperty=value;}
			get{return _cnvcflightproperty;}
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

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public StationFlightInfoBM()
        {
        }
        #endregion 构造函数

        #region 构造函数：把表格 tbStationFlightInfo 的一行 DataRow 数据赋给实体对象
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public StationFlightInfoBM(DataRow dataRow)
        {
            try
            {
                _cnvcstationthreecode = dataRow["cnvcstationthreecode"].ToString();
                _cnvcstationname = dataRow["cnvcstationname"].ToString();
                _cnirowindex = Convert.ToInt32(dataRow["cnirowindex"].ToString());
                _cncindatop = dataRow["cncindatop"].ToString();
                _cnvcinfltid = dataRow["cnvcinfltid"].ToString();
                if (dataRow["cniinlegno"].ToString().Trim() != "")
                {
                    _cniinlegno = Convert.ToInt32(dataRow["cniinlegno"].ToString());
                }
                _cnvcinac = dataRow["cnvcinac"].ToString();
                _cncinflightdate = dataRow["cncinflightdate"].ToString();
                _cnvcinflightno = dataRow["cnvcinflightno"].ToString();
                _cnvcinlong_reg = dataRow["cnvcinlong_reg"].ToString();
                _cncindepstn = dataRow["cncindepstn"].ToString();
                _cncindepairportcname = dataRow["cncindepairportcname"].ToString();
                _cncinarrstn = dataRow["cncinarrstn"].ToString();
                _cncinarrairportcname = dataRow["cncinarrairportcname"].ToString();
                _cncineta = dataRow["cncineta"].ToString();
                _cnvcingate = dataRow["cnvcingate"].ToString();
                _cnvcingateproperty = dataRow["cnvcingateproperty"].ToString();
                _cnvcoutgate = dataRow["cnvcoutgate"].ToString();
                _cnvcoutgateproperty = dataRow["cnvcoutgateproperty"].ToString();
                _cncoutdatop = dataRow["cncoutdatop"].ToString();
                _cnvcoutfltid = dataRow["cnvcoutfltid"].ToString();
                if (dataRow["cnioutlegno"].ToString().Trim() != "")
                {
                    _cnioutlegno = Convert.ToInt32(dataRow["cnioutlegno"].ToString());
                }
                _cnvcoutac = dataRow["cnvcoutac"].ToString();
                _cncoutflightdate = dataRow["cncoutflightdate"].ToString();
                _cnvoutcflightno = dataRow["cnvoutcflightno"].ToString();
                _cnvcoutlong_reg = dataRow["cnvcoutlong_reg"].ToString();
                _cncoutdepstn = dataRow["cncoutdepstn"].ToString();
                _cncoutdepairportcname = dataRow["cncoutdepairportcname"].ToString();
                _cncoutarrstn = dataRow["cncoutarrstn"].ToString();
                _cncoutarrairportcname = dataRow["cncoutarrairportcname"].ToString();
                _cnvcflightproperty = dataRow["cnvcflightproperty"].ToString();    

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcmemo = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion 构造函数：把表格 tbStationFlightInfo 的一行 DataRow 数据赋给实体对象
    }
}
