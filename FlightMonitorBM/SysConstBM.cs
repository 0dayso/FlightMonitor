using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 系统配置常量信息类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class SysConstBM
    {
        /// <summary>
		/// 版本号
		/// </summary>
		public const string VERSION = "1.0.*";

		/// <summary>
		/// 版权公司
		/// </summary>
		public const string COMPANY = "海航航空股份有限公司版权所有";
		
		/// <summary>
		/// 航班监控系统数据库
		/// </summary>
		public const string DB_FlightMonitor	= "dbFlightMonitor";

        /// <summary>
        /// 航班监控系统测试数据库
        /// </summary>
        public const string DB_TEST_FlightMonitor = "db_test_FlightMonitor"; 

        /// <summary>
        /// 计算机飞行计划数据库
        /// </summary>
        public const string DB_DSPBusiness = "dbDSPBusiness";

        /// <summary>
        /// 存储ACARS报文的数据库
        /// </summary>
        public const string DB_ACARSMegs = "dbACARSMegs";

        /// <summary>
        /// 临时存储FE报文的数据库
        /// </summary>
        public const string DB_FEMegs = "dbFEMegs";

        /// <summary>
        /// FleetWatch系统数据库（Oracle）
        /// </summary>
        public const string DB_FleetWatch = "dbFleetWatch";

        /// <summary>
        /// 运行网数据库
        /// </summary>
        public const string DB_Flt_Dept = "flt_dept";

        public const string DB_Smart = "smart";

        /// <summary>
        /// 航前任务书
        /// </summary>
        public const string DB_VoyageReport = "dbVoyageReport";

        /// <summary>
        /// 机场消息
        /// </summary>
        public const string DB_MessageService = "dbMessageService";

        /// <summary>
        /// 登陆用户不存在
        /// </summary>
        public const string FlightMonitor_ACCOUNTENTERIN_NODEFINE = "登陆用户不存在";

        /// <summary>
        /// 登陆用户密码错误
        /// </summary>
        public const string FlightMonitor_ACCOUNTENTERIN_PWDERROR = "登陆用户密码错误";

        /// <summary>
        /// 登陆用户已满
        /// </summary>
        public const string FlightMonitor_MAX_USER = "已经达到最大登陆用户数！";

        /// <summary>
        /// 注册帐号重复
        /// </summary>
        public const string FlightMonitor_ACCOUNT_INVALID = "该用户已经存在，请重新填写";

        /// <summary>
        /// 注册帐号不存在
        /// </summary>
        public const string DEMO_ACCOUNT_VALID = "该帐号不存在，赶快注册吧";

        /// <summary>
        /// 添加成功
        /// </summary>
        public const string SYS_ADD_SUCCESS = "添加成功！";

        /// <summary>
        /// 添加失败
        /// </summary>
        public const string SYS_ADD_FALSE = "添加失败！";

        /// <summary>
        /// 删除成功
        /// </summary>
        public const string SYS_DELETE_SUCCESS = "删除成功！";

        /// <summary>
        /// 删除失败
        /// </summary>
        public const string SYS_DELETE_FALSE = "删除失败！";

        /// <summary>
        /// 修改成功
        /// </summary>
        public const string SYS_UPDATE_SUCCESS = "修改成功！";

        /// <summary>
        /// 修改失败
        /// </summary>
        public const string SYS_UPDATE_FALSE = "修改失败！";

        /// <summary>
        /// 加载成功
        /// </summary>
        public const string SYS_LOAD_SUCCESS = "加载成功！";

        /// <summary>
        /// 加载失败
        /// </summary>
        public const string SYS_LOAD_FALSE = "加载失败！";
    }
}
