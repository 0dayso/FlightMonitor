using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Data;
using System.Collections;
using AirSoft.FlightMonitor.AgentServiceBF;
using AirSoft.FlightMonitor.AgentServiceDA;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 保障日记主题信息外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2014-06-24
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeRecordCaptionBF
    {
        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="guaranteeRecordCaptionBM">保障日记主题信息对象</param>
        /// <returns></returns>
        public ReturnValueSF Add(GuaranteeRecordCaptionBM guaranteeRecordCaptionBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问外观层方法
                GuaranteeRecordCaptionDAF guaranteeRecordCaptionDAF = new GuaranteeRecordCaptionDAF();
                rvSF.Result = guaranteeRecordCaptionDAF.Add(guaranteeRecordCaptionBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion 增加一条数据

        #region 获取所有信息(根据航班、航站)
        /// <summary>
        /// 获取所有信息(根据航班、航站)
        /// </summary>
        /// <param name="cncStation">航站</param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <returns>获取所有信息(根据航班、航站)</returns>
        public ReturnValueSF GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                GuaranteeRecordCaptionDAF guaranteeRecordCaptionDAF = new GuaranteeRecordCaptionDAF();
                rvSF.Dt = guaranteeRecordCaptionDAF.GetDataList(cncStation, cncDATOP, cnvcFLTID, cniLegNO, cnvcAC);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion 获取所有信息(根据航班、航站)
    }
}
