using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 登机门实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-08-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable]
    public class GateInfoBM
    {
        #region 声明
        private string _cncstationthreecode;
        private string _cnvcstationname;
        private string _cnvcgate;
        private string _cnvcgateproperty;
        private string _cnvcmemo;
        private string _cnvcoperationuser;
        private DateTime _cndoperationtime;

        private string _cnvcmemo_1;
        private bool _cnblnsuccess = false;

        #endregion 声明


        #region 属性
        /// <summary>
        /// 航站三字码
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
        /// 停机位
        /// </summary>
        public string cnvcGate
        {
            set { _cnvcgate = value; }
            get { return _cnvcgate; }
        }
        /// <summary>
        /// 属性，近机位 或 远机位。
        /// </summary>
        public string cnvcGateProperty
        {
            set { _cnvcgateproperty = value; }
            get { return _cnvcgateproperty; }
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
        /// 操作用户
        /// </summary>
        public string cnvcOperationUser
        {
            set { _cnvcoperationuser = value; }
            get { return _cnvcoperationuser; }
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

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public GateInfoBM()
        {
        }
        #endregion 构造函数

        #region 构造函数：把表格 tbGateInfo 的一行 DataRow 数据赋给实体对象
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow">数据行</param>
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
        #endregion 构造函数：把表格 tbGateInfo 的一行 DataRow 数据赋给实体对象
    }
}
