using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class MessageServiceBF
    {
        /// <summary>
        /// 获取机场发送的最新消息
        /// </summary>
        /// <param name="DTTM">消息发送时间（格式如 2015-04-03 22:17:00）</param>
        /// <returns>机场发送的最新消息</returns>
        public ReturnValueSF GetMessages(string DTTM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            MessageServiceDAF messageServiceDAF = new MessageServiceDAF();

            try
            {
                rvSF.Dt = messageServiceDAF.GetMessages(DTTM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }

        /// <summary>
        /// 获取机场发送的最新消息，使用 自增长值 EventID
        /// </summary>
        /// <param name="EventID">自增长值</param>
        /// <returns>机场发送的最新消息</returns>
        public ReturnValueSF GetMessages(int EventID)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            MessageServiceDAF messageServiceDAF = new MessageServiceDAF();

            try
            {
                rvSF.Dt = messageServiceDAF.GetMessages(EventID);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
    }
}
