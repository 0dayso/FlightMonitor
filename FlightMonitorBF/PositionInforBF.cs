using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 席位信息外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionInforBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionInforBF()
        {
        }

        /// <summary>
        /// 根据席位编号获取属于该席位的所有飞机的信息
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetInforByPositionId(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //数据访问外观类
            PositionInforDAF positionInforDAF = new PositionInforDAF();

            try
            {
                rvSF.Dt = positionInforDAF.GetInforByPositionId(positionNameBM);
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
        /// 给某席位分配飞机
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertPositionInfors(FlightMonitorBM.PositionNameBM positionNameBM, IList ilPositionInforBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            PositionInforDAF positionInforDAF = new PositionInforDAF();
            try
            {
                rvSF.Result = positionInforDAF.InsertPositionInfors(positionNameBM, ilPositionInforBM);
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
