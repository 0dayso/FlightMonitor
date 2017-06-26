using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 用户数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AccountDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountDAF()
        {
        }

        #region 插入新用户
        /// <summary>
        /// 插入新用户
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <param name="ilDataItemPurview">数据项权限列表</param>
        /// <returns>1：成功；0：失败</returns>
        public int Insert(FlightMonitorBM.AccountBM accountBM, IList ilDataItemPurview)
        {
            int retVal = -1; //定义返回值

            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            FlightMonitorDA.DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            IEnumerator ieDataItemPurview = ilDataItemPurview.GetEnumerator();

            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                accountDA.Transaction = accountDA.SqlConn.BeginTransaction();
                accountDA.Insert(accountBM);

                dataItemPurviewDA.SqlConn = accountDA.SqlConn;
                dataItemPurviewDA.Transaction = accountDA.Transaction;
                while (ieDataItemPurview.MoveNext())
                {
                    FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurview.Current;
                    dataItemPurviewDA.Insert(dataItemPurviewBM);
                }

                retVal = 1;
                accountDA.Transaction.Commit();
            }
            catch (Exception ex)
            {
                accountDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 更新所有用户信息
        /// <summary>
        /// 更新所有用户信息
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <param name="ilDataItemPurview">数据项权限列表</param>
        /// <returns>1：成功；0：失败</returns>
        public int UpdateAllInfo(FlightMonitorBM.AccountBM accountBM, IList ilDataItemPurview)
        {
            int retVal = -1; //定义返回值

            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            FlightMonitorDA.DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            IEnumerator ieDataItemPurview = ilDataItemPurview.GetEnumerator();

            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                accountDA.Transaction = accountDA.SqlConn.BeginTransaction();

                accountDA.UpdateAllInfo(accountBM);

                dataItemPurviewDA.SqlConn = accountDA.SqlConn;
                dataItemPurviewDA.Transaction = accountDA.Transaction;
                while (ieDataItemPurview.MoveNext())
                {
                    FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurview.Current;
                    dataItemPurviewDA.UpdatePrompt(dataItemPurviewBM);
                }

                retVal = 1;
                accountDA.Transaction.Commit();                
            }
            catch (Exception ex)
            {
                accountDA.Transaction.Rollback();     
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>1：成功；0：失败</returns>
        public int Delete(FlightMonitorBM.AccountBM accountBM)
        {
            //定义返回值
            int retVal = -1;
            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            //打开数据库连接
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                accountDA.Delete(accountBM);
                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 更新用户密码
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>1：成功；0：失败</returns>
        public int UpdatePassword(FlightMonitorBM.AccountBM accountBM)
        {
            int retVal = -1; //定义返回值

            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();

            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //打开数据库连接
                accountDA.UpdatePassword(accountBM);
                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region 更新登陆用户数
        /// <summary>
        /// 更新登陆用户数
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>1：成功；0：失败</returns>
        public int UpdateLogUser(FlightMonitorBM.AccountBM accountBM)
        {
            //定义返回值
            int retVal = -1;

            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            try
            {
                //打开数据库连接
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

                accountDA.UpdateLogUser(accountBM);
                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }

            return retVal;

        }
        #endregion

        #region 根据航站或用户类型查询用户
        /// <summary>
        /// 根据航站或用户类型查询用户
        /// </summary>
        /// <param name="strStationThreeCode">航站三字码</param>
        /// <param name="strUserTypeId">用户类型</param>
        /// <returns>包含符合条件的用户的DataTable</returns>
        public DataTable GetAccountByStation(string strStationThreeCode, string strUserTypeId)
        {
            DataTable dt = new DataTable();// 定义返回值
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            
            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
                dt = accountDA.GetAccountByStation(strStationThreeCode, strUserTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region 根据用户ID获取用户信息
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="strUserId">用户编码</param>
        /// <returns>包含用户信息的DataTable</returns>
        public DataTable GetAccountByUserId(string strUserId)
        {
            DataTable dt = new DataTable();//定义返回值
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                dt = accountDA.GetAccountByUserId(strUserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region 增加一个在线用户
        /// <summary>
        /// 增加一个在线用户
        /// </summary>
        /// <param name="accountBM"></param>
        public int InsertOnlineUser(AccountBM accountBM)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            int iOnlineUserNo = 0;
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                iOnlineUserNo = accountDA.InsertOnlineUser(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return iOnlineUserNo;
        }
        #endregion

        #region 更新在线用户信息
        /// <summary>
        /// 更新在线用户信息
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="iOnlineUserNo">用户登录时产生的Id</param>
        public void UpdateOnlineUser(AccountBM accountBM, int iOnlineUserNo)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                accountDA.UpdateOnlineUser(accountBM, iOnlineUserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
        }
        #endregion

        #region 删除在线用户信息
        /// <summary>
        /// 删除在线用户信息
        /// </summary>
        /// <param name="accountBM"></param>
        public void DeleteOnlineUser(int iOnlineUserNo)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                accountDA.DeleteOnlineUser(iOnlineUserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
        }
        #endregion

        #region 用户退出系统
        /// <summary>
        /// 用户退出系统
        /// </summary>
        /// <param name="iOnlineUserNo"></param>
        public void LogOffOnlineUser(int iOnlineUserNo)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                accountDA.LogOffOnlineUser(iOnlineUserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
        }
        #endregion

        #region 查询在线用户数量
        /// <summary>
        /// 查询在线用户数量
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int SelectOnlineUserCount(AccountBM accountBM)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            int iOnlineUsersCount = 0;
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                iOnlineUsersCount = accountDA.SelectOnlineUserCount(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }

            return iOnlineUsersCount;
        }
        #endregion

        #region 查询在线用户列表
        /// <summary>
        /// 查询在线用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOnlineUsersList()
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            DataTable dt = new DataTable();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // 打开数据库连接
            try
            {
                dt = accountDA.SelectOnlineUsersList();
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }

            return dt;
        }
        #endregion
    }
}
