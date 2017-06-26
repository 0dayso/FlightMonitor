using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 菜单项权限实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class MenuPurviewBM
    {
        /// <summary>
        /// 类型
        /// </summary>
        public enum DataType : uint
        {
            ITEM = 1,
            PURVIEW = 2
        }

        #region 对象内部变量
        private int m_iMenuPurviewID;
        private int m_iMenuID;
        private string m_strMenuItemID;
        private string m_strMenuItemName;
        private int m_iMenuPurview;
        private string m_strUserID;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuPurviewBM()
        {
        }

        /// <summary>
        /// 构造函数:把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        /// <param name="dataType">类型</param>
        public MenuPurviewBM(DataRow dataRow, DataType dataType)
        {
            m_iMenuID = Convert.ToInt32(dataRow["cniMenuID"].ToString());
            m_strMenuItemID = dataRow["cniMenuID"].ToString();
            m_strMenuItemName = dataRow["cnvcMenuItemName"].ToString();

            if (dataType == DataType.PURVIEW)
            {
                m_iMenuPurview = Convert.ToInt32(dataRow["cniMenuPurviewID"].ToString());
                m_iMenuPurview = Convert.ToInt32(dataRow["cniMenuPurview"].ToString());
                m_strUserID = dataRow["cnvcUserID"].ToString();
            }
        }

        /// <summary>
        /// 权限序号
        /// </summary>
        public int MenuPurviewID
        {
            get { return m_iMenuPurviewID; }
            set { m_iMenuPurviewID = value; }
        }

        /// <summary>
        /// 菜单序号
        /// </summary>
        public int MenuID
        {
            get { return m_iMenuID; }
            set { m_iMenuID = value; }
        }

        /// <summary>
        /// 菜单项编号
        /// </summary>
        public string MenuItemID
        {
            get { return m_strMenuItemID; }
            set { m_strMenuItemID = value; }
        }

        /// <summary>
        /// 菜单项名称
        /// </summary>
        public string MenuItemName
        {
            get { return m_strMenuItemName; }
            set { m_strMenuItemName = value; }
        }

        /// <summary>
        /// 菜单项权限
        /// </summary>
        public int MenuPurview
        {
            get { return m_iMenuPurview; }
            set { m_iMenuPurview = value; }
        }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserID
        {
            get { return m_strUserID; }
            set { m_strUserID = value; }
        }

    }
}
