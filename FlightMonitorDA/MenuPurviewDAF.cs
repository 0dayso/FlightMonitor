using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 菜单权限访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class MenuPurviewDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuPurviewDAF()
        {
        }

         /// <summary>
        /// 获取所有的菜单项
        /// </summary>
        /// <returns>包含所有菜单项的数据表</returns>
        public DataTable GetMenuItems()
        {
            //定义返回值
            DataTable dtMenuItems = new DataTable();
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();

            try
            {
                //打开数据库联机
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtMenuItems = menuPurviewDA.GetMenuItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }

            return dtMenuItems;
        }

        /// <summary>
        /// 获取菜单项权限
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetMenuItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            //定义返回值
            DataTable dtMenuItems = new DataTable();
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();

            try
            {
                //打开数据库联机
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtMenuItems = menuPurviewDA.GetMenuItemPurview(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }

            return dtMenuItems;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public int Insert(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //定义返回值
            int retVal = -1;
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();
            try
            {
                //打开数据库连接
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = menuPurviewDA.Insert(menuPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// 更新数据项权限
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //定义返回值
            int retVal = -1;
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();
            try
            {
                //打开数据库连接
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = menuPurviewDA.UpdatePurview(menuPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// 查询某用户某菜单项的权限
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        public DataTable GetMenuPurviewByMenuID(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //定义返回值
            DataTable dtMenuItems = new DataTable();
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();

            try
            {
                //打开数据库联机
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtMenuItems = menuPurviewDA.GetMenuPurviewByMenuID(menuPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }

            return dtMenuItems;
        }
    }
}
