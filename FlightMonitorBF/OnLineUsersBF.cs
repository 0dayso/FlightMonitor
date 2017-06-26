using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.AgentServiceBF
{
    /// <summary>
    /// 在线用户表
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-11-30
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class OnLineUsersBF
    {
        #region 生成 在线用户表
        /// <summary>
        /// 生成 在线用户表
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF CreateDatatable()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                rvSF.Dt = onLineUsersDAF.CreateDatatable();
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

        #region 更新 在线用户信息
        /// <summary>
        /// 更新 在线用户信息
        /// </summary>
        /// <param name="dtOnLineUsers">在线用户信息表</param>
        /// <param name="accountBM">帐号对象实体</param>
        /// <returns>ReturnValueSF.Result:1 成功；-1 失败</returns>
        public ReturnValueSF RefreshOnLineUsersInfo(DataTable dtOnLineUsers, AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                rvSF.Result = onLineUsersDAF.RefreshOnLineUsersInfo(dtOnLineUsers,accountBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            //
            return rvSF;
        }
        #endregion

    }
}
