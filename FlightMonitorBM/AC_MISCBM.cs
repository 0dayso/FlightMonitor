using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 飞机信息实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AC_MISCBM
    {
        #region 对象内部变量
        private string m_strAC;
        private string m_strSHORT_REG;
        private string m_strLONG_REG;
        private string m_strACTYPE;
        private string m_strOWNER;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public AC_MISCBM()
        {
        }

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow"></param>
        public AC_MISCBM(DataRow dataRow)
        {
            m_strAC = dataRow["cncAC"].ToString();
            m_strSHORT_REG = dataRow["cnvcSHORT_REG"].ToString();
            m_strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
            m_strACTYPE = dataRow["cncACTYPE"].ToString();
            m_strOWNER = dataRow["cnvcOWNER"].ToString();
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// 飞机短注册号
        /// </summary>
        public string SHORT_REG
        {
            get { return m_strSHORT_REG; }
            set { m_strSHORT_REG = value; }
        }

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string LONG_REG
        {
            get { return m_strLONG_REG; }
            set { m_strLONG_REG = value; }
        }

        /// <summary>
        /// FOC机型
        /// </summary>
        public string ACTYPE
        {
            get { return m_strACTYPE; }
            set { m_strACTYPE = value; }
        }

        /// <summary>
        /// 所属公司两字码
        /// </summary>
        public string OWNER
        {
            get { return m_strOWNER; }
            set { m_strOWNER = value; }
        }
    }
}
