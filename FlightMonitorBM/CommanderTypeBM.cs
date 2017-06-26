using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 保障人员类型实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2016-03-18
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable]
    public class CommanderTypeBM
    {
        #region 声明
        private int _cnicommandertypeid;
		private string _cnvccommandertype;
		private string _cnvcmemo;

        private string _cnvcresult;
        private bool _cnblnsuccess = false;
        #endregion 声明

        #region 属性
        /// <summary>
		/// 
		/// </summary>
		public int cniCommanderTypeID
		{
			set{ _cnicommandertypeid=value;}
			get{return _cnicommandertypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcCommanderType
		{
			set{ _cnvccommandertype=value;}
			get{return _cnvccommandertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcMemo
		{
			set{ _cnvcmemo=value;}
			get{return _cnvcmemo;}
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
        public CommanderTypeBM()
        {
        }
        #endregion 构造函数

        #region 构造函数：把一行 tbCommanderType DataRow数据赋给实体对象
        /// <summary>
        /// 把一行 tbCommanderType DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">保障人员类型信息数据行</param>
        public CommanderTypeBM(DataRow dataRow)
        {
            try
            {
                _cnicommandertypeid = Convert.ToInt32(dataRow["cniCommanderTypeID"].ToString());
                _cnvccommandertype = dataRow["cnvcCommanderType"].ToString();
                _cnvcmemo = dataRow["cnvcMemo"].ToString();

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcresult = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion 构造函数：把一行 tbCommanderType DataRow数据赋给实体对象

        #endregion 函数

    }
}
