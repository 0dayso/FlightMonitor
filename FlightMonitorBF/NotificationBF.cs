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
    /// 通知数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2013-12-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class NotificationBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NotificationBF()
        {

        }

        #region 获取有效通知数据，并组合成通知文本。
        /// <summary>
        /// 获取有效通知数据，并组合成通知文本。
        /// </summary>
        /// <param name="currentDateTime">当前时间</param>
        /// <returns>返回当前时间在生效起止时间段的通知数据，并组合成通知文本。</returns>
        public ReturnValueSF GetAndCombineNotificationData(DateTime currentDateTime)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //定义返回值
            try
            {
                //定义数据访问层外观类方法
                FlightMonitorDA.NotificationDAF notificationDAF = new FlightMonitorDA.NotificationDAF();
                DataTable dataTable = notificationDAF.GetNotificationData(currentDateTime);
                string strNotificationData = "";
                for (int index = 0; index < dataTable.Rows.Count; index++)
                {
                    strNotificationData = strNotificationData +
                        (index + 1).ToString() +
                        "、" +
                        dataTable.Rows[index]["cnvcCaption"].ToString() +
                        Environment.NewLine +
                        dataTable.Rows[index]["cnvcContent"].ToString() +
                        Environment.NewLine +
                        Environment.NewLine;
                }
                rvSF.Message = strNotificationData;
                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion 获取有效通知数据，并组合成通知文本。

        #region 获取附件信息数据，并组合成通知文本。
        /// <summary>
        /// 获取附件信息数据，并组合成通知文本。
        /// </summary>
        /// <param name="dataTableNotificationData">通知信息表</param>
        /// <returns>返回当前时间在生效起止时间段的通知数据中的附件信息数据，并组合成通知文本。</returns>
        public ReturnValueSF CombineNotificationAttachmentData(DataTable dataTableNotificationData)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //定义返回值
            try
            {
                string strNotificationData = "";
                DataTable dataTable = dataTableNotificationData.Clone();
                DataRow[] dataRowsDataTableNotificationData = dataTableNotificationData.Select("cnvcAttachment <> ''");
                for (int index = 0; index < dataRowsDataTableNotificationData.Length; index++)
                {
                    strNotificationData = strNotificationData +
                        (index + 1).ToString() +
                        "、" +
                        dataTableNotificationData.Rows[index]["cnvcCaption"].ToString() +
                        Environment.NewLine +
                        "   附件文件名：" +
                        dataTableNotificationData.Rows[index]["cnvcAttachment"].ToString() +
                        Environment.NewLine +
                        Environment.NewLine;

                    dataTable.ImportRow(dataTableNotificationData.Rows[index]);
                }
                rvSF.Message = strNotificationData;
                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion 获取附件信息数据，并组合成通知文本。


    }
}
