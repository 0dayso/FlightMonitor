using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 席位名称外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionNameBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionNameBF()
        {
        }

        /// <summary>
        /// 获取所有席位名称
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllPositionName()
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Dt = positionNameDAF.GetAllPositionName();
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
        /// 插入一个席位
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertPositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Result = positionNameDAF.InsertPositionName(positionBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 删除一个席位名称
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF DeletePositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Result = positionNameDAF.DeletePositionName(positionBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 根据席位编号获取席位信息
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetPositionByID(FlightMonitorBM.PositionNameBM positionBM)
        {

            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Dt = positionNameDAF.GetPositionByID(positionBM);
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
