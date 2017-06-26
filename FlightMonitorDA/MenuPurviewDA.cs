using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 菜单项权限访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class MenuPurviewDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuPurviewDA()
        {
        }

        #region 获取所有菜单项
        /// <summary>
        /// 查询数据项SQL语句
        /// </summary>
        private const string SELECT_MenuItem = "Select * from tbMenuItem order by cniMenuID";

        /// <summary>
        /// 获取所有的菜单项
        /// </summary>
        /// <returns>包含所有菜单项的数据表</returns>
        public DataTable GetMenuItems()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuItem);
        }
        #endregion

        //#region  获取某用户的菜单项权限
        ///// <summary>
        ///// SQL语句
        ///// </summary>
        //private const string SELECT_MenuItemPurviewByUserId = "select * from tbMenuItem,tbMenuPurview where " +
        //    "tbMenuItem.cniMenuID = tbMenuPurview.cniMenuID and cnvcUserID = @PARM_cnvcUserID";

        ///// <summary>
        ///// 组合参数
        ///// </summary>
        ///// <returns></returns>
        //private SqlParameter[] GetMenuItemPurviewByUserIdParameters(FlightMonitorBM.AccountBM accountBM)
        //{
        //    SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_MenuItemPurviewByUserId);
        //    if (parms == null)
        //    {
        //        parms = new SqlParameter[]
        //        {
        //            new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
        //        };

        //        SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_MenuItemPurviewByUserId, parms);
        //    }
        //    parms[0].Value = accountBM.UserId;
        //    return parms;
        //}

        ///// <summary>
        ///// 获取用户的菜单项访问权限
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetMenuItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        //{
        //    SqlParameter[] parms = GetMenuItemPurviewByUserIdParameters(accountBM);
        //    return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuItemPurviewByUserId, parms);
        //}
        //#endregion

        #region 获取某用户所有菜单项权限
        /// <summary>
        /// 获取某用户所有菜单项权限
        /// </summary>
        private const string SELECT_MenuItemPurview = "SELECT * FROM tbMenuItem LEFT OUTER JOIN tbMenuPurview ON tbMenuItem.cniMenuID = tbMenuPurview.cniMenuID WHERE " +
            "cnvcUserID = @PARM_cnvcUserID order by tbMenuItem.cniMenuID";
        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns></returns>
        private SqlParameter[] GetMenuItemPurviewParameters(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_MenuItemPurview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                    {
                       new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                    };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_MenuItemPurview, parms);
            };

            parms[0].Value = accountBM.UserId;
            return parms;
        }

        /// <summary>
        /// 获取菜单项权限
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetMenuItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = GetMenuItemPurviewParameters(accountBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuItemPurview, parms);
        }
        #endregion

        #region 插入一条记录
        /// <summary>
        /// 插入记录SQL语句
        /// </summary>
        private const string INSERT_Purview = "insert into tbMenuPurview(cniMenuID,cniMenuPurview,cnvcUserID) values(" +
           "@PARM_cniMenuID,@PARM_cniMenuPurview,@PARM_cnvcUserID)";
        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Purview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniMenuID", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniMenuPurview", SqlDbType.Int, 0),                    
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Purview, parms);
            }
            parms[0].Value = menuPurviewBM.MenuID;
            parms[1].Value = menuPurviewBM.MenuPurview;
            parms[2].Value = menuPurviewBM.UserID;           
            return parms;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public int Insert(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = InsertParameters(menuPurviewBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, INSERT_Purview, parms);
            return retVal;
        }
        #endregion

        #region 更新用户数据项权限
        /// <summary>
        /// 更新用户权限SQL语句
        /// </summary>
        private const string UPDATE_Purview = "UPDATE tbMenuPurview SET " +
            "cniMenuPurview = @PARM_cniMenuPurview WHERE " +
            "cniMenuID=@PARM_cniMenuID AND " +
            "cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateParameters(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_Purview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniMenuPurview", SqlDbType.Int, 0),                    
                    new SqlParameter("@PARM_cniMenuID", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_Purview, parms);
            }
            parms[0].Value = menuPurviewBM.MenuPurview;
            parms[1].Value = menuPurviewBM.MenuID;
            parms[2].Value = menuPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 更新数据项权限
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = UpdateParameters(menuPurviewBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_Purview, parms);
            return retVal;
        }

        #endregion

        #region 根据用户名和菜单编号获取权限信息
        /// <summary>
        /// 根据菜单编号和用户编码获取
        /// </summary>
        private const string SELECT_MenuPurviewByMenuID = "SELECT * FROM tbMenuPurview WHERE " +
            "cniMenuID=@PARM_cniMenuID AND cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        private SqlParameter[] GetMenuPurviewByMenuIDParameters(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_MenuPurviewByMenuID);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniMenuID", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_MenuPurviewByMenuID, parms);
            }
            parms[0].Value = menuPurviewBM.MenuID;
            parms[1].Value = menuPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 查询某用户某菜单项的权限
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        public DataTable GetMenuPurviewByMenuID(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = GetMenuPurviewByMenuIDParameters(menuPurviewBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuPurviewByMenuID, parms);
        }

        #endregion

    }
}
