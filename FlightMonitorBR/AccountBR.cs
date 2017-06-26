using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBR
{
    /// <summary>
    /// 用户业务规则层
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AccountBR
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountBR()
        {
        }

        #region 验证用户登陆
        /// <summary>
        /// 验证用户登陆
        /// </summary>
        /// <param name="accountBM">用户登陆实体</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM)
        {
            //自定义返回类型
            ReturnValueSF rvSF = new ReturnValueSF();

            //调用数据访问层数据访问外观类方法
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
            DataTable dtGetUser = accountDAF.GetAccountByUserId(accountBM.UserId);

            if (dtGetUser.Rows.Count == 0)
            {
                //登陆用户不存在
                rvSF.Result = -1;
                rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_NODEFINE;
            }
            else
            {
                if (accountBM.UserPassword != dtGetUser.Rows[0]["cnvcUserPassword"].ToString())
                {
                    //密码不正确
                    rvSF.Result = -1;
                    rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_PWDERROR;
                }
                //检查已登录到系统的用户数量
                else
                {
                    int iOnlineUsersCount = accountDAF.SelectOnlineUserCount(accountBM);
                    if ( iOnlineUsersCount  >= Convert.ToInt32(dtGetUser.Rows[0]["cniMaxUser"]))
                    {
                        //已经达到最大登陆数
                        rvSF.Result = -1;
                        rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_MAX_USER;
                    }
                    else
                    {
                        rvSF.Dt = dtGetUser;
                        rvSF.Result = iOnlineUsersCount + 1;
                    }
                }
            }

            return rvSF;
        }
        #endregion

        #region 验证用户登陆，增加了登陆方式的选择
        /// <summary>
        /// 验证用户登陆，增加了登陆方式的选择
        /// </summary>
        /// <param name="accountBM">用户登陆实体</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM, string LogOnType)
        {
            //自定义返回类型
            ReturnValueSF rvSF = new ReturnValueSF();

            //调用数据访问层数据访问外观类方法
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
            DataTable dtGetUser = accountDAF.GetAccountByUserId(accountBM.UserId);

            if (dtGetUser.Rows.Count == 0)
            {
                //登陆用户不存在
                rvSF.Result = -1;
                rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_NODEFINE;
            }
            else
            {
                if ((LogOnType != "域登陆") && (accountBM.UserPassword != dtGetUser.Rows[0]["cnvcUserPassword"].ToString()))
                {
                    //密码不正确
                    rvSF.Result = -1;
                    rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_PWDERROR;
                }
                //检查已登录到系统的用户数量
                else
                {
                    int iOnlineUsersCount = accountDAF.SelectOnlineUserCount(accountBM);
                    if (iOnlineUsersCount >= Convert.ToInt32(dtGetUser.Rows[0]["cniMaxUser"]))
                    {
                        //已经达到最大登陆数
                        rvSF.Result = -1;
                        rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_MAX_USER;
                    }
                    else
                    {
                        rvSF.Dt = dtGetUser;
                        rvSF.Result = iOnlineUsersCount + 1;
                    }
                }
            }

            return rvSF;
        }
        #endregion 验证用户登陆，增加了登陆方式的选择

        public void CheckLogOFF(string strUserId)
        {
            //获取上次登陆用户实体对象
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
            DataTable dtLogUser = accountDAF.GetAccountByUserId(strUserId);
           
            //如果用户还存在数据库中
            if (dtLogUser.Rows.Count > 0)
            {
                FlightMonitorBM.AccountBM accountBM = new AccountBM(dtLogUser.Rows[0]);
                //清除该用户上次的登陆信息
                accountBM.LogUser -= 1;
                accountDAF.UpdateLogUser(accountBM);
            }
        }
    }
}
