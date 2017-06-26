using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;
using System.Configuration;
using System.Data;
using AirSoft.FlightMonitor.AgentServiceBM;


namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region 设置生产库配置
            //try
            //{
            //    //获取生产库配置
            //    string strFlightMonitor_Use = ConfigurationSettings.AppSettings["dbFlightMonitor_Use"].ToString().Trim();

            //    if (strFlightMonitor_Use != "")
            //    {
            //        //配置文件：使生产库配置生效
            //        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //        configuration.AppSettings.Settings["dbFlightMonitor"].Value = strFlightMonitor_Use;
            //        configuration.Save(ConfigurationSaveMode.Modified);
            //        ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件

            //        //标明使用的FlightMonitor系统数据库使用的配置项
            //        SysMsgBM.FlightMonitorConfig = "dbFlightMonitor_Use";
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            #endregion 设置生产库配置

            #region 显示通知信息
            //显示通知信息
            try
            {
                NotificationBF notificationBF = new NotificationBF();
                ReturnValueSF rvSF = notificationBF.GetAndCombineNotificationData(DateTime.Now);
                if ((rvSF.Result == 1) && (rvSF.Message.Trim() != ""))
                {
                    //
                    MessageBox.Show(rvSF.Message, "通知");

                    //显示附件信息
                    ReturnValueSF rvSF_1 = notificationBF.CombineNotificationAttachmentData(rvSF.Dt);

                    if ((rvSF_1.Result == 1) && (rvSF_1.Message.Trim() != ""))
                    {

                        if (MessageBox.Show(rvSF_1.Message, "是否打开附件？", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            foreach (DataRow dataRow in rvSF_1.Dt.Rows)
                            {
                                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + @"\" + dataRow["cnvcAttachment"].ToString());
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion 显示通知信息

            //航站保障系统主窗体
            fmMDIMain objfmMDIMain = new fmMDIMain();

            fmLogOn objfmLogOn = new fmLogOn();
            objfmLogOn.ShowDialog();
            if (objfmLogOn.IsLogin == true)
            {
                //显示升级信息
                string strShowRelease = ConfigSettings.ReadSetting("ShowUpdateRelease");
                if (strShowRelease == "1")
                {
                    fmRelease objfmRelease = new fmRelease();
                    objfmRelease.ShowDialog();
                }
                Application.Run(objfmMDIMain);
            }
        }
    }
}