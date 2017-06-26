using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ACARS航班动态外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AC_MISCBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AC_MISCBF()
        {
        }

        /// <summary>
        /// 获取某席位没有选择的飞机号
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetAirCraftByPositionId(PositionNameBM positionBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Dt = ac_miscDAF.GetAirCraftByPositionId(positionBM);
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
        /// 插入新飞机
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertACMISC(AC_MISCBM ac_MISCBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            DataTable dtACMISC = ac_miscDAF.GetACMISCByAC(ac_MISCBM);

            if (dtACMISC.Rows.Count == 0)
            {
                try
                {
                    rvSF.Result = ac_miscDAF.InsertACMISC(ac_MISCBM);
                }
                catch (Exception ex)
                {
                    rvSF.Result = -1;
                    rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
                }
            }
            else
            {
                rvSF.Result = 2;
            }

            return rvSF;            
        }

         /// <summary>
        /// 删除飞机
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public ReturnValueSF DeleteACMISC(AC_MISCBM ac_MISCBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Result = ac_miscDAF.DeleteACMISC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;

        }

         /// <summary>
        /// 获取所有飞机列表
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetACMISC()
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Result = 1;
                rvSF.Dt = ac_miscDAF.GetACMISC();
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 获取所有飞机机型
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetACMISCGroupBy()
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Result = 1;
                rvSF.Dt = ac_miscDAF.GetACMISCGroupBy();
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
