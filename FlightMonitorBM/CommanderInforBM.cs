using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航站保障人员实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-07-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CommanderInforBM
    {
        #region 对象内部变量
        private int m_iCommanderInforId;
        private string m_strCommanderAccount;
        private string m_strCommanderName;
        private string m_strCommanderType;
        private string m_strThreeCode;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommanderInforBM()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow"></param>
        public CommanderInforBM(DataRow dataRow)
        {
            m_iCommanderInforId = Convert.ToInt32(dataRow["cniCommanderInforId"].ToString());
            m_strCommanderAccount = dataRow["cnvCommanderAccount"].ToString();
            m_strCommanderName = dataRow["cnvcCommanderName"].ToString();
            m_strCommanderType = dataRow["cnvcCommanderType"].ToString();
            m_strThreeCode = dataRow["cncThreeCode"].ToString();
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int CommanderInforId
        {
            get { return m_iCommanderInforId; }
            set { m_iCommanderInforId = value; }
        }

        /// <summary>
        /// 保障人员帐号
        /// </summary>
        public string CommanderAccount
        {
            get { return m_strCommanderAccount; }
            set { m_strCommanderAccount = value; }
        }

        /// <summary>
        /// 保障人员姓名
        /// </summary>
        public string CommanderName
        {
            get { return m_strCommanderName; }
            set { m_strCommanderName = value; }
        }

        /// <summary>
        /// 保障人员类型
        /// </summary>
        public string CommanderType
        {
            get { return m_strCommanderType; }
            set { m_strCommanderType = value; }
        }

        /// <summary>
        /// 保障人员三字码
        /// </summary>
        public string ThreeCode
        {
            get { return m_strThreeCode; }
            set { m_strThreeCode = value; }
        }
    }
}
