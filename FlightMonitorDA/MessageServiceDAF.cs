using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 获取机场发送的消息的访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-04-15
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class MessageServiceDAF
    {
        /// <summary>
        /// 获取机场发送的最新消息
        /// </summary>
        /// <param name="DTTM">消息发送时间（格式如 2015-04-03 22:17:00）</param>
        /// <returns>机场发送的最新消息</returns>
        public DataTable GetMessages(string DTTM)
        {
            //定义返回值
            DataTable dtDataTable = new DataTable();
            MessageServiceDA messageServiceDA = new MessageServiceDA();

            try
            {
                //打开数据库联机
                messageServiceDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_MessageService, 1));
                dtDataTable = messageServiceDA.GetMessages(DTTM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                messageServiceDA.ConnClose();
            }

            return dtDataTable;
        }

        /// <summary>
        /// 获取机场发送的最新消息，使用 自增长值 EventID
        /// </summary>
        /// <param name="EventID">自增长值</param>
        /// <returns>机场发送的最新消息</returns>
        public DataTable GetMessages(int EventID)
        {
            //定义返回值
            DataTable dtDataTable = new DataTable();
            MessageServiceDA messageServiceDA = new MessageServiceDA();

            try
            {
                //打开数据库联机
                messageServiceDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_MessageService, 1));
                dtDataTable = messageServiceDA.GetMessages(EventID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                messageServiceDA.ConnClose();
            }

            return dtDataTable;
        }
    }
}
