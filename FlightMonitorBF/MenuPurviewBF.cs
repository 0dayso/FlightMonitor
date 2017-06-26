using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 菜单权限数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class MenuPurviewBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuPurviewBF()
        {
        }

        /// <summary>
        /// 获取所有的菜单项
        /// </summary>
        /// <returns>包含所有菜单项的数据表</returns>
        public ReturnValueSF GetMenuItems()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF.Dt = menuPurviewDAF.GetMenuItems();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 获取菜单项权限
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetMenuItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF.Dt = menuPurviewDAF.GetMenuItemPurview(accountBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

         /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public ReturnValueSF Insert(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF = ExistMenuPurview(menuPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF.Result = menuPurviewDAF.Insert(menuPurviewBM);
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// 更新数据项权限
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdatePurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)        
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();
            try
            {
                rvSF = ExistMenuPurview(menuPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF = Insert(menuPurviewBM);
                }
                else if (rvSF.Result > 0)
                {
                    rvSF.Result = menuPurviewDAF.UpdatePurview(menuPurviewBM);
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 判断菜单项权限是否已经存在
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns>自定义返回值</returns>
        private ReturnValueSF ExistMenuPurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();
            try
            {
                DataTable dtPurview = menuPurviewDAF.GetMenuPurviewByMenuID(menuPurviewBM);

                if (dtPurview.Rows.Count == 0)
                {
                    rvSF.Result = 0;
                }
                else
                {
                    rvSF.Result = 1;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 查询某用户某菜单项的权限
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        public ReturnValueSF GetMenuPurviewByMenuID(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF.Dt = menuPurviewDAF.GetMenuPurviewByMenuID(menuPurviewBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

    }
}
