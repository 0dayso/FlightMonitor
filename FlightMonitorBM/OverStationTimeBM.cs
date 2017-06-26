using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航站过站时间实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-07-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable]
    public class OverStationTimeBM
    {
        #region 声明
        private string _cnvcairportcname;
        private string _cncairportthreecode;
        private string _cnvcbigactyp;
        private string _cnvcsmallactyp;
        private int _cnistandardtime;
        private int _cnigroundtime;

        private string _cnvcmemo;
        private bool _cnblnsuccess = false;
        #endregion 声明

        #region 属性
        /// <summary>
        /// 机场中文名
        /// </summary>
        public string cnvcAirportCNAME
        {
            set { _cnvcairportcname = value; }
            get { return _cnvcairportcname; }
        }
        /// <summary>
        /// 机场三字码
        /// </summary>
        public string cncAirportThreeCode
        {
            set { _cncairportthreecode = value; }
            get { return _cncairportthreecode; }
        }
        /// <summary>
        /// 大机型
        /// </summary>
        public string cnvcBigACTYP
        {
            set { _cnvcbigactyp = value; }
            get { return _cnvcbigactyp; }
        }
        /// <summary>
        /// 小机型（和 PDCFlight 中的机型一致）
        /// </summary>
        public string cnvcSmallACTYP
        {
            set { _cnvcsmallactyp = value; }
            get { return _cnvcsmallactyp; }
        }
        /// <summary>
        /// 标准过站时间
        /// </summary>
        public int cniStandardTime
        {
            set { _cnistandardtime = value; }
            get { return _cnistandardtime; }
        }
        /// <summary>
        /// 地面过站时间
        /// </summary>
        public int cniGroundTime
        {
            set { _cnigroundtime = value; }
            get { return _cnigroundtime; }
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
        #region 构造函数：把表格 tbOverStationTime 的一行 DataRow 数据赋给实体对象
        /// <summary>
        /// 构造函数：把表格 tbOverStationTime 的一行 DataRow 数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
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
        #endregion 构造函数：把表格 tbOverStationTime 的一行 DataRow 数据赋给实体对象

        #endregion 函数
    }
}
