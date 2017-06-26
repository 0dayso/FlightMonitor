using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 数据项数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-23
    /// 修 改 人：张  黎
    /// 修改日期：2008-07-01
    /// 版    本：
    public class DataItemPurviewDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataItemPurviewDAF()
        {
        }

        #region 获取所有数据项
        /// <summary>
        /// 获取所有数据项
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataItems()
        {
            //定义返回值
            DataTable dtDataItems = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            try
            {
                //打开数据库联机
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataItems = dataItemPurviewDA.GetDataItems();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }

            return dtDataItems;
        }
        #endregion

        #region 获取用户的数据项访问权限
        /// <summary>
        /// 获取用户的数据项访问权限
        /// </summary>
        /// <param name="dataItemPurviewBM">访问权限实体对象</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            DataTable dtDataItemsPurview = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            try
            {
                //打开数据库联机
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataItemsPurview = dataItemPurviewDA.GetDataItemPurviewByUserId(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }

            return dtDataItemsPurview;
        }
        #endregion

        #region 获取参考点 yanxian 2013-12-26
        /// <summary>
        /// 获取参考点
        /// </summary>
        /// <param name="dataItemPurviewBM">访问权限实体对象</param>
        /// <returns></returns>
        public DataTable GetDataItemPointPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            DataTable dtDataItemsPurview = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            try
            {
                //打开数据库联机
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataItemsPurview = dataItemPurviewDA.GetDataItemPointPurviewByUserId(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }

            return dtDataItemsPurview;
        }
        #endregion

        #region 插入一条记录
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public int Insert(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //定义返回值
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = dataItemPurviewDA.Insert(dataItemPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 更新数据项权限
        /// <summary>
        /// 更新数据项权限
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePurview(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //定义返回值
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = dataItemPurviewDA.UpdatePurview(dataItemPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 更新数据项的可见性
        /// <summary>
        /// 更新数据项的可见性
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateVisible(IList ilDataItemPurviewBM)
        {
            //定义返回值
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                IEnumerator ieDataItemPurviewBM = ilDataItemPurviewBM.GetEnumerator();
                while (ieDataItemPurviewBM.MoveNext())
                {
                    DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurviewBM.Current;
                    retVal = dataItemPurviewDA.UpdateVisible(dataItemPurviewBM);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 更新数据项的显示顺序
        /// <summary>
        /// 更新数据项的显示顺序
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateIndex(IList ilDataItemPurviewBM)
        {
            //定义返回值
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                
                IEnumerator ieDataItemPurviewBM = ilDataItemPurviewBM.GetEnumerator();
                while (ieDataItemPurviewBM.MoveNext())
                {
                    DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurviewBM.Current;
                    retVal = dataItemPurviewDA.UpdateIndex(dataItemPurviewBM);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 查询某用户某数据项的权限
        /// <summary>
        /// 查询某用户某数据项的权限
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByDataItemNo(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //定义返回值
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetDataItemPurviewByDataItemNo(dataItemPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region 获取用户有权限的数据项
        /// <summary>
        /// 获取用户有权限的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetPurviewDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //定义返回值
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetPurviewDataItem(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region 获取用户设置的可见的数据项
        /// <summary>
        /// 获取用户设置的可见的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //定义返回值
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetVisibleDataItem(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region 根据类型获取用户设置的可见的数据项
        /// <summary>
        /// 获取用户设置的可见的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM, string strType)
        {
            //定义返回值
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //打开数据库连接
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetVisibleDataItem(accountBM, strType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion
    }
}
