using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 过站统计信息（过站数量、靠桥数量）对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-08-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable]
    public class OverStationStatisticsBM
    {
        #region 声明
        private string _cnvcstationthreecode;
        private string _cnvcstationname;
        private string _cncflightdate;
        private int _cnioverstationcount;
        private int _cnioverstationusebridgecount;

        private string _cnvcmemo;
        private bool _cnblnsuccess = false;
        #endregion 声明

        #region 属性
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
        public OverStationStatisticsBM()
        {
        }
        #endregion 构造函数

        #region 构造函数：把表格 tbOverStationStatistics 的一行 DataRow 数据赋给实体对象
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">数据行</param>
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
        #endregion 构造函数：把表格 tbOverStationStatistics 的一行 DataRow 数据赋给实体对象
        #endregion 函数
    }
}
