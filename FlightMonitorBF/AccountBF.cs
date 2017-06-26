using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBR;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorBF
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
    public class AccountBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountBF()
        {

        }

        #region 检查注册帐号合法性
        /// <summary>
        /// 检查注册帐号合法性
        /// </summary>
        /// <param name="strUserId">用户帐号</param>
        /// <returns>自定义类型</returns>
        public ReturnValueSF ValidAccount(string strUserId)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层数据访问外观类方法
                FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
                DataTable dtGetUser = accountDAF.GetAccountByUserId(strUserId);

                if (dtGetUser.Rows.Count > 0)
                {
                    //注册用户已存在
                    rvSF.Result = -1;
                    rvSF.Message = SysConstBM.FlightMonitor_ACCOUNT_INVALID;
                }
                else
                {
                    rvSF.Result = 1;
                    rvSF.Message = SysConstBM.DEMO_ACCOUNT_VALID;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion

        #region 插入用户
        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <param name="ilDataItemPurview">用户权限列表</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF Insert(FlightMonitorBM.AccountBM accountBM,IList ilDataItemPurview)
        {
            ReturnValueSF rvSF = new ReturnValueSF();  //定义返回值

            try
            {
                //数据访问外观类
                FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
                //判断用户是否合法
                rvSF = this.ValidAccount(accountBM.UserId);

                //判断帐号是否合法继续处理
                if (rvSF.Result > 0)
                {
                    rvSF.Result = accountDAF.Insert(accountBM, ilDataItemPurview);
                    if (rvSF.Result > 0)
                    {
                        rvSF.Message = FlightMonitorBM.SysConstBM.SYS_ADD_SUCCESS;
                    }
                    else
                    {
                        rvSF.Message = FlightMonitorBM.SysConstBM.SYS_ADD_FALSE;
                    }
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex); 
            }
            return rvSF;
        }
        #endregion

        #region 更新所有用户信息
        /// <summary>
        /// 更新所有用户信息
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <param name="ilDataItemPurview">用户权限列表</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF UpdateAllInfo(FlightMonitorBM.AccountBM accountBM,IList ilDataItemPurview)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();


            try
            {
                //判断用户是否存在
                rvSF = this.ValidAccount(accountBM.UserId);

                //用户不存在
                if (rvSF.Result > 0)
                {
                    //预留系统权限和数据项权限
                    rvSF = this.Insert(accountBM, ilDataItemPurview);
                }
                else  //更新用户
                {
                    rvSF.Result = accountDAF.UpdateAllInfo(accountBM, ilDataItemPurview);
                    if (rvSF.Result > 0)
                    {
                        rvSF.Message = SysConstBM.SYS_UPDATE_SUCCESS;
                    }
                    else
                    {
                        rvSF.Message = SysConstBM.SYS_UPDATE_FALSE;
                    }
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF Delete(FlightMonitorBM.AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();

            //调用数据访问外观层方法
            try
            {
                rvSF.Result = accountDAF.Delete(accountBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_DELETE_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_DELETE_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 验证用户登陆
        /// <summary>
        /// 验证用户登陆
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //返回值

            try
            {
                //调用业务规则层方法
                FlightMonitorBR.AccountBR accountBR = new FlightMonitorBR.AccountBR();
                rvSF = accountBR.LogOn(accountBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 验证用户登陆，增加了登陆方式的选择
        /// <summary>
        /// 验证用户登陆，增加了登陆方式的选择
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM, string LogOnType)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //返回值

            try
            {
                //调用业务规则层方法
                FlightMonitorBR.AccountBR accountBR = new FlightMonitorBR.AccountBR();
                rvSF = accountBR.LogOn(accountBM, LogOnType);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion 验证用户登陆，增加了登陆方式的选择

        #region 清除用户非正常情况退出时的登陆信息
        /// <summary>
        /// 清除用户非正常情况退出时的登陆信息
        /// </summary>
        /// <param name="strUserID">上次登陆用户ID</param>
        /// <returns></returns>
        public ReturnValueSF CheckLogOFF(string strUserID)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用业务规则层方法
                FlightMonitorBR.AccountBR accountBR = new AccountBR();
                accountBR.CheckLogOFF(strUserID);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 更新用户密码
        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="accountBM">用户对象实体</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF UpdatePassword(FlightMonitorBM.AccountBM accountBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF();  //定义返回值
            try
            {
                //调用数据访问层外观类方法
                FlightMonitorDA.AccountDAF accountDAF = new FlightMonitorDA.AccountDAF();
                rvSF.Result = accountDAF.UpdatePassword(accountBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 更新登陆用户
        /// <summary>
        /// 更新登陆用户
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF UpdateLogUser(FlightMonitorBM.AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
                rvSF.Result = accountDAF.UpdateLogUser(accountBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 根据航站或用户类型查询用户
        /// <summary>
        /// 根据航站或用户类型查询用户
        /// </summary>
        /// <param name="strStationThreeCode">航站三字码</param>
        /// <param name="strUserTypeId">用户类型</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF GetAccountByStation(string strStationThreeCode, string strUserTypeId)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //定义返回值
            try
            {
                //调用数据访问层外观类方法
                FlightMonitorDA.AccountDAF accountDAF = new FlightMonitorDA.AccountDAF();
                rvSF.Dt = accountDAF.GetAccountByStation(strStationThreeCode, strUserTypeId);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region 根据用户编码获取用户信息
        /// <summary>
        /// 根据用户编码获取用户信息
        /// </summary>
        /// <param name="strUserId">用户编码</param>
        /// <returns>自定义类型</returns>
        public ReturnValueSF GetAccountByUserId(string strUserId)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //定义返回值
            try
            {
                //定义数据访问层外观类方法
                FlightMonitorDA.AccountDAF accountDAF = new FlightMonitorDA.AccountDAF();
                rvSF.Dt = accountDAF.GetAccountByUserId(strUserId);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion

        #region 增加一个在线用户
        /// <summary>
        /// 增加一个在线用户
        /// </summary>
        /// <param name="accountBM"></param>
        public int InsertOnlineUser(AccountBM accountBM)
        {
            AccountDAF accountDAF = new AccountDAF();
            int iOnlineUserNo = accountDAF.InsertOnlineUser(accountBM);
            return iOnlineUserNo;
        }
        #endregion

        #region 更新在线用户信息
        /// <summary>
        /// 更新在线用户信息
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="iOnlineUserNo">用户登录记录的ID</param>
        public void UpdateOnlineUser(AccountBM accountBM, int iOnlineUserNo)
        {
            AccountDAF accountDAF = new AccountDAF();
            accountDAF.UpdateOnlineUser(accountBM, iOnlineUserNo);
        }
        #endregion

        #region 删除在线用户信息
        /// <summary>
        /// 删除在线用户信息
        /// </summary>
        /// <param name="accountBM"></param>
        public void DeleteOnlineUser(int iOnlineUserNo)
        {
            AccountDAF accountDAF = new AccountDAF();
            accountDAF.DeleteOnlineUser(iOnlineUserNo);
        }
        #endregion

        #region 用户退出系统
        /// <summary>
        /// 用户退出系统
        /// </summary>
        /// <param name="iOnlineUserNo"></param>
        public void LogOffOnlineUser(int iOnlineUserNo)
        {
            AccountDAF accountDAF = new AccountDAF();
            accountDAF.LogOffOnlineUser(iOnlineUserNo);
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
            AccountDAF accountDAF = new AccountDAF();
            int iCount = accountDAF.SelectOnlineUserCount(accountBM);
            return iCount;
        }
        #endregion

        #region 查询在线用户列表
        /// <summary>
        /// 查询在线用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOnlineUsersList()
        {
            AccountDAF accountDAF = new AccountDAF();
            DataTable dt = accountDAF.SelectOnlineUsersList();
            return dt;
        }
        #endregion

    }
}
