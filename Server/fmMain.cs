using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBR;
using System.IO;
using System.Web.Services;
using System.Web;
using System.Net;

namespace AirSoft.FlightMonitor.Server
{
    /// <summary>
    /// 服务器端数据处理类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-14
    /// 修 改 人：张  黎
    /// 修改日期：2008-02-26
    /// 版    本：
    public partial class fmMain : Form
    {
        private string scheduleXMLFilePath;     //航班计划文件位置
        private string changeXMLPath;           //航班动态文件位置

        /// <summary>
        /// 构造函数
        /// </summary>
        public fmMain()
        {
            InitializeComponent();
        }

        #region 加载窗体
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMain_Load(object sender, EventArgs e)
        {

            //从config文件读取各类文件存储位置
            scheduleXMLFilePath = Public.SystemFramework.ConfigManagerSF.GetConfigString("ScheduleXMLFilePath", 1);
            changeXMLPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("ChangeXMLFilePath", 1);

            //读取航班计划文件列表
            string[] arrScheduleFileName = Directory.GetFiles(scheduleXMLFilePath);
            //逐个解析每个文件
            foreach (string fileName in arrScheduleFileName)
            {
                string strCreationTime = System.IO.File.GetCreationTime(fileName).ToString("yyyy-MM-dd HH:mm:ss");
                if (DateTime.Parse(strCreationTime).AddSeconds(10) < DateTime.Now)
                {
                    LoadScheduleFlight(fileName);
                }
            }

            //读取航班动态变更文件列表
            string[] arrChangeFileName = Directory.GetFiles(changeXMLPath);
            //逐个解析每个变更文件
            foreach (string fileName in arrChangeFileName)
            {
                string strCreationTime = System.IO.File.GetCreationTime(fileName).ToString("yyyy-MM-dd HH:mm:ss");
                if (DateTime.Parse(strCreationTime).AddSeconds(5) < DateTime.Now)
                {
                    LoadChangeFlight(fileName);
                }
            }

        }
        #endregion

        //定时触发的事件
        #region 定时获取航班计划文件列表并解析
        /// <summary>
        /// 定时获取航班计划文件列表并解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSchedule_Tick(object sender, EventArgs e)
        {
            scheduleXMLFilePath = Public.SystemFramework.ConfigManagerSF.GetConfigString("ScheduleXMLFilePath", 1);

            //读取航班计划文件列表
            string[] arrScheduleFileName = Directory.GetFiles(scheduleXMLFilePath);
            //逐个解析每个文件
            foreach (string fileName in arrScheduleFileName)
            {
                string strCreationTime = System.IO.File.GetCreationTime(fileName).ToString("yyyy-MM-dd HH:mm:ss");

                if (DateTime.Parse(strCreationTime).AddSeconds(10) < DateTime.Now)
                {
                    LoadScheduleFlight(fileName);
                }
            }
        }
        #endregion

        #region 定时获取航班动态变更文件列表并解析
        /// <summary>
        /// 定时获取航班动态变更文件列表并解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerChange_Tick(object sender, EventArgs e)
        {
            changeXMLPath = Public.SystemFramework.ConfigManagerSF.GetConfigString("ChangeXMLFilePath", 1);

            //读取航班动态变更文件列表
            string[] arrChangeFileName = Directory.GetFiles(changeXMLPath);
            //逐个解析每个文件
            foreach (string fileName in arrChangeFileName)
            {
                string strCreationTime = System.IO.File.GetCreationTime(fileName).ToString("yyyy-MM-dd HH:mm:ss");

                if (DateTime.Parse(strCreationTime).AddSeconds(5) < DateTime.Now)
                {
                    LoadChangeFlight(fileName);
                }
            }
        }
        #endregion

        #region 定时更新VIP信息
        /// <summary>
        /// 定时更新VIP信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerVIP_Tick(object sender, EventArgs e)
        {
            UpdateVIP();
        }
        #endregion

        #region 定时查询衔接航班信息
        /// <summary>
        /// 定时查询衔接航班信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerJoinFlight_Tick(object sender, EventArgs e)
        {
            GetJoinFlightNo();
        }
        #endregion

        #region 定时检查在线用户状态
        private void timerCheckUsers_Tick(object sender, EventArgs e)
        {
            AccountBF accountBF = new AccountBF();
            AccountBM accountBM = new AccountBM();
            DataTable dtOnlineUsersList = accountBF.SelectOnlineUsersList();
            if (dtOnlineUsersList == null)
            {
                return;
            }
            int iTotalOnLineUsersCount = dtOnlineUsersList.Rows.Count;
            int iNoRespUsersCount = 0;
            int iKickedUsersCount = 0;
            foreach (DataRow dr in dtOnlineUsersList.Rows)
            {
                string strLastUpdateTime = dr["cnvcLastUpdateTime"].ToString();
                if (strLastUpdateTime == "")
                {
                    strLastUpdateTime = dr["cnvcLogOnTime"].ToString();
                }
                TimeSpan ts = DateTime.Now - Convert.ToDateTime(strLastUpdateTime);
                //如果用户超过3分半钟没有发送消息给服务器
                if (ts.TotalSeconds > 210)
                {
                    iNoRespUsersCount++;
                    int iOnlineUserNo = Convert.ToInt32(dr["iPK"]);
                    try
                    {
                        //则将用户在线信息删除
                        accountBF.DeleteOnlineUser(iOnlineUserNo);
                        iKickedUsersCount++;
                    }
                    catch
                    { }
                }
            }

            //Log
            string[] strLogs = new string[3];
            strLogs[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strLogs[1] = "检查用户状态";
            strLogs[2] = "在线用户：" + iTotalOnLineUsersCount.ToString() + " 未响应用户：" + iNoRespUsersCount.ToString() + " 服务器重置的用户：" + iKickedUsersCount.ToString();
            GenLog(strLogs);
        }
        #endregion

        //定时触发事件相应的处理事件
        #region 处理航班计划
        /// <summary>
        /// 加载航班计划
        /// </summary>
        /// <param name="strFullName">文件全路径</param>
        private void LoadScheduleFlight(string strFullName)
        {
            //调用业务外观层方法
            FlightMonitorBF.ScheduleFlightBF scheduleFlightBF = new ScheduleFlightBF();
            ReturnValueSF rvSF = scheduleFlightBF.LoadScheduleFlight(strFullName);
            //Log
            string[] strLogs = new string[3];   
            strLogs[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strLogs[1] = "航班计划";
            string strMeg = rvSF.Message;
            if (strMeg == "")
            {
                strMeg = "处理成功";
            }
            strLogs[2] = strMeg;
            GenLog(strLogs);
        }
        #endregion

        #region 处理航班变更
        /// <summary>
        /// 加载航班变更
        /// </summary>
        /// <param name="strFullName"></param>
        private void LoadChangeFlight(string strFullName)
        {
            //获取航班变更实体
            FlightMonitorBF.ChangeLegsBF changeLegsBF = new ChangeLegsBF();
            ReturnValueSF rvSF = changeLegsBF.GetChangeLegsFromFile(strFullName);

            //rvSF.Result = 文件解析成功标记
            //rvSF.DS = 文件解析内容
            if (rvSF.Result > 0)
            {
                if (rvSF.Ds.Tables.Count > 0 && rvSF.Ds.Tables[0].Rows.Count > 0)
                {
                    ChangeLegsBM changeLegsBM = new ChangeLegsBM(rvSF.Ds.Tables[0].Rows[0], 1);
                    switch (changeLegsBM.DELACTION)
                    {
                        case "U":
                            rvSF = changeLegsBF.Update(changeLegsBM);
                            break;
                        case "I":
                            rvSF = changeLegsBF.Update(changeLegsBM);
                            break;
                        case "D":
                            rvSF = changeLegsBF.LogicDelete(changeLegsBM);
                            break;
                    }
                }
            }

            //如果处理成功，删除文件
            if (rvSF.Result > 0)
            {
                File.Delete(strFullName);
            }

            //Log
            string[] strLogs = new string[3];
            strLogs[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strLogs[1] = "航班变更";
            string strMeg = rvSF.Message;
            if (strMeg == "")
            {
                strMeg = "处理成功";
            }
            strLogs[2] = strMeg;
            GenLog(strLogs);
        }
        #endregion

        #region 更新VIP信息
        /// <summary>
        /// 每天更新一次VIP信息
        /// </summary>
        private void UpdateVIP()
        {
            int iADD = 0;
            VIPBF vipBF = new VIPBF();
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = DateTime.Now.AddHours(-5).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddHours(-5).AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";

            DataTable dtLegs = guaranteeInforBF.GetAllLegsByDay(dateTimeBM).Dt;

            DataTable dtFOCVIP = vipBF.GetVIPFromFoc(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")).Ds.Tables[0];

            foreach (DataRow dataRow in dtLegs.Rows)
            {
                iADD = 0;

                DataRow[] drFOCVIPs = dtFOCVIP.Select("DATOP='" + dataRow["cncDATOP"].ToString() + "' AND " +
                    "FLTID = '" + dataRow["cnvcFLTID"].ToString() + "' AND " +
                    "DEPSTN = '" + dataRow["cncDEPSTN"].ToString() + "' AND " +
                    "ARRSTN = '" + dataRow["cncARRSTN"].ToString() + "'");

                if (drFOCVIPs.Length > 0)
                {
                    foreach (DataRow drFOCVIP in drFOCVIPs)
                    {
                        VIPBM vipBM = new VIPBM(drFOCVIP, 1);
                        vipBM.VipType = "";
                        vipBM.DataSource = "FOC";

                        ReturnValueSF rvSF = vipBF.ValidVIP(vipBM);
                        if (rvSF.Result == 0)
                        {
                            vipBF.InsertVIP(vipBM);
                            iADD = 1;
                        }
                        else
                        {
                            vipBF.UpdateVipInfor(vipBM);
                        }
                    }

                    if (iADD == 1)
                    {
                        MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();

                        maintenGuaranteeInforBM.DATOP = dataRow["cncDATOP"].ToString();
                        maintenGuaranteeInforBM.FLTID = dataRow["cnvcFLTID"].ToString();
                        maintenGuaranteeInforBM.LEGNO = dataRow["cniLegNO"].ToString();
                        maintenGuaranteeInforBM.AC = dataRow["cnvcAC"].ToString();
                        maintenGuaranteeInforBM.FieldName = "cnbVIPTag";
                        maintenGuaranteeInforBM.FieldType = 1;
                        maintenGuaranteeInforBM.NewContent = "有";
                        ReturnValueSF rvSF = guaranteeInforBF.UpdateGuaranteeInfor(maintenGuaranteeInforBM);


                        if (rvSF.Result > 0)
                        {
                            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
                            changeRecordBM.UserID = "FOC";
                            changeRecordBM.OldDATOP = dataRow["cncDATOP"].ToString();
                            changeRecordBM.OldFLTID = dataRow["cnvcFLTID"].ToString();
                            changeRecordBM.OldLegNo = Convert.ToInt32(dataRow["cniLegNO"].ToString());
                            changeRecordBM.OldAC = dataRow["cnvcAC"].ToString();
                            changeRecordBM.NewDATOP = dataRow["cncDATOP"].ToString();
                            changeRecordBM.NewFLTID = dataRow["cnvcFLTID"].ToString();
                            changeRecordBM.NewLegNo = Convert.ToInt32(dataRow["cniLegNO"].ToString());
                            changeRecordBM.NewAC = dataRow["cnvcAC"].ToString();
                            changeRecordBM.OldDepSTN = dataRow["cncDEPSTN"].ToString();
                            changeRecordBM.OldArrSTN = dataRow["cncARRSTN"].ToString();
                            changeRecordBM.NewDepSTN = dataRow["cncDEPSTN"].ToString();
                            changeRecordBM.NewArrSTN = dataRow["cncARRSTN"].ToString();
                            changeRecordBM.STD = dataRow["cncSTD"].ToString();
                            changeRecordBM.ETD = dataRow["cncETD"].ToString();
                            changeRecordBM.STA = dataRow["cncSTA"].ToString();
                            changeRecordBM.ETA = dataRow["cncETA"].ToString();
                            changeRecordBM.ChangeReasonCode = "cnbVIPTag";
                            changeRecordBM.ChangeOldContent = "";
                            changeRecordBM.ChangeNewContent = "有";
                            changeRecordBM.ActionTag = "U";
                            changeRecordBM.Refresh = 0;
                            changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            changeRecordBM.FOCOperatingTime = "";

                            changeRecordBF.Insert(changeRecordBM);
                        }
                    }
                }
            }
            //Log
            string[] strLogs = new string[3];
            strLogs[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strLogs[1] = "更新VIP信息";
            strLogs[2] = "";
            GenLog(strLogs);
        }
        #endregion

        #region 查询衔接航班
        private void GetJoinFlightNo()
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = DateTime.Now.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            AccountBM accountBM = new AccountBM();
            accountBM.UserId = "FOC";
            ReturnValueSF rvSF = guaranteeInforBF.GetJoinFlightNo(dateTimeBM, accountBM);

            //Log
            string[] strLogs = new string[3];
            strLogs[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strLogs[1] = "衔接航班";
            string strMeg = rvSF.Message;
            if (strMeg == "")
            {
                strMeg = "处理成功";
            }
            strLogs[2] = strMeg;
            GenLog(strLogs);
        }
        #endregion

        #region 手工获取航班连飞信息
        /// <summary>
        /// 手工获取航班连飞信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiGetJoinFlight_Click(object sender, EventArgs e)
        {
            GetJoinFlightNo();
        }
        #endregion

        #region 生成Log文件
        /// <summary>
        /// 生成Log文件
        /// </summary>
        /// <param name="strLog"></param>
        private void GenLog(string[] strLog)
        {
            ListViewItem lvItem = new ListViewItem(strLog);
            lvMonitor.Items.Insert(0, lvItem);
            int iItemCount = lvMonitor.Items.Count;
            //监控中最多包含最近的1000条记录
            if (iItemCount > 1000)
            {
                lvMonitor.Items.RemoveAt(iItemCount - 1);
            }
        }
        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void 重新导入航班计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}