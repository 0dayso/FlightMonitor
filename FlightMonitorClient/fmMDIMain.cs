using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// 航站保障系统（VER 101101）：
    /// 1、 修改 fmFlightGuarantee.cs 的 private DataTable GetStationFlights(DateTimeBM dateTimeBM, StationBM stationBM, int iToday)
    ///     使用 ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM);
    ///     这样，在保障界面就可以提取一个月的数据，数据直接从数据库读取，不经过代理。
    /// </summary>
    public partial class fmMDIMain : Form
    {
        private static FlightMonitorBM.AccountBM m_LogAccount; //登陆用户实体对象
        private DataTable dtStandardIntermissionTime;          //标准过站时间表
        private DataTable dtDataItems;  //用户设置出的数据项
        private DataTable dtMenuItemPurview;  //系统权限
        private StationBM m_stationBM;   //用户所属航站实体对象     
        private PositionNameBM m_positionNameBM;   //席位名称
        private int m_iAutoAdjust = 0;
        private static int _iOnlineUserNo = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public fmMDIMain()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 用户登陆信息
        /// </summary>
        public static AccountBM LogAccount
        {
            get { return m_LogAccount; }
            set { m_LogAccount = value; }
        }
        /// <summary>
        /// 状态栏
        /// </summary>
        public StatusBar MainStatusBar
        {
            get { return statusBarMain; }
        }
        /// <summary>
        /// 自动调整大小
        /// </summary>
        public int AutoAdjust
        {
            get { return m_iAutoAdjust; }
            set { m_iAutoAdjust = value; }
        }
        /// <summary>
        /// 在线用户编号
        /// </summary>
        public static int OnlineUserNo
        {
            get { return _iOnlineUserNo; }
            set { _iOnlineUserNo = value; }
        }
        #endregion

        /// <summary>
        /// 加载窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMDIMain_Load(object sender, EventArgs e)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            m_iAutoAdjust = Convert.ToInt32(ConfigSettings.ReadSetting("AutoAdjust"));

            //用户所属航站实体对象
            DataTable dtStations = new DataTable();
            StationBF stationBF = new StationBF();
            rvSF = stationBF.GetAllStation();
            if (rvSF.Result > 0)
            {
                dtStations = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DataRow[] drStation = dtStations.Select("cncThreeCode = '" + m_LogAccount.StationThreeCode + "'");

            if (drStation.Length > 0)
            {
                m_stationBM = new StationBM(drStation[0]);
            }
            else
            {
                m_stationBM = new StationBM();
            }

            //席位名称实体对象
            int iPositionId = Convert.ToInt32(ConfigSettings.ReadSetting("PositionID").ToString());
            m_positionNameBM = new PositionNameBM();
            m_positionNameBM.PositionID = iPositionId;
            PositionNameBF positionNameBF = new PositionNameBF();
            rvSF = positionNameBF.GetPositionByID(m_positionNameBM);
            if (rvSF.Result > 0)
            {
                if(rvSF.Dt.Rows.Count > 0)
                {
                    m_positionNameBM = new PositionNameBM(rvSF.Dt.Rows[0]);
                }
            }
            else
            {                
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            //获取标准过站时间
            IntermissionTimeBF intermissionTimeBF = new IntermissionTimeBF();
            rvSF = intermissionTimeBF.GetStandardIntermissionTime();
            if (rvSF.Result > 0)
            {
                dtStandardIntermissionTime = rvSF.Dt;
            }
            else
            {
                dtStandardIntermissionTime = new DataTable();
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //获取用户有权限的数据项
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount);
            dtDataItems = rvSF.Dt;

            //获取系统权限
            MenuPurviewBF menuPurviewBF = new MenuPurviewBF();
            rvSF = menuPurviewBF.GetMenuItemPurview(m_LogAccount);
            dtMenuItemPurview = rvSF.Dt;

            //根据用户类型显示不同的窗体
            //显示航班监控界面
            if (m_LogAccount.UserTypeId == 1)
            {
                fmFlightWatch objfmFlightWatch = new fmFlightWatch(m_LogAccount, dtStandardIntermissionTime, dtDataItems, m_positionNameBM);
                objfmFlightWatch.Show();
                objfmFlightWatch.MdiParent = this;
                objfmFlightWatch.WindowState = FormWindowState.Maximized;

                statusBarMain.Panels[0].Text = "登陆席位:" + m_positionNameBM.PositionName;
                statusBarMain.Panels[1].Text = "登陆用户:" + m_LogAccount.UserName;
            }
            //显示航班保障界面
            else
            {
                fmFlightGuarantee objfmFlightGuarantee = new fmFlightGuarantee(m_LogAccount, dtStandardIntermissionTime, dtDataItems, statusBarMain, m_iAutoAdjust);
                objfmFlightGuarantee.Show();
                objfmFlightGuarantee.MdiParent = this;
                objfmFlightGuarantee.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 清除登陆用户信息
        /// </summary>
        private void ClearLogInfo()
        {
            //调用业务外观层方法
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            accountBF.LogOffOnlineUser(_iOnlineUserNo);
        }

        #region 系统菜单
        /// <summary>
        /// 用户管理菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAccountManage_Click(object sender, EventArgs e)
        {
            AccountManage();
        }

        /// <summary>
        /// 航后机位安排菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miArrangeGate_Click(object sender, EventArgs e)
        {
            ArrangeGate();
        }

        /// <summary>
        /// 航后机位查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miQueryGate_Click(object sender, EventArgs e)
        {
            QueryGate();
        }

        /// <summary>
        /// 变更数据查询菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miChangeData_Click(object sender, EventArgs e)
        {
            GetChangeData();
        }


        /// <summary>
        /// 视图设置菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDataItemView_Click(object sender, EventArgs e)
        {
            DataItemViewSetting();
        }

        /// <summary>
        /// 席位设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miPosition_Click(object sender, EventArgs e)
        {
            SetPosition();
        }


        /// <summary>
        /// 航班监控菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDataItemWatch_Click(object sender, EventArgs e)
        {
            FlightWatch();
        }

        /// <summary>
        /// 航班保障菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDataItemGuarantee_Click(object sender, EventArgs e)
        {
            FlightGuarantee();
        }

        /// <summary>
        /// 席位分配菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAssignAircraftNo_Click(object sender, EventArgs e)
        {
            AssignAircraft();
        }

        /// <summary>
        /// 系统提示菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miSystemPrompt_Click(object sender, EventArgs e)
        {
            SystemPrompt();
        }

        /// <summary>
        /// 显示条件菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDisplayCondition_Click(object sender, EventArgs e)
        {
            DisplayCondition();
        }

        /// <summary>
        /// 退出系统菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 修改密码菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEditPassword_Click(object sender, EventArgs e)
        {
            EditPassword();
        }

        /// <summary>
        /// 席位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAddPosition_Click(object sender, EventArgs e)
        {
            AddPosition();
        }

        /// <summary>
        /// 航班计划菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miFlightSchedule_Click(object sender, EventArgs e)
        {
            FlightSchedule();
        }

        /// <summary>
        /// 出港旅客人数统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCheckNum_Click(object sender, EventArgs e)
        {
            StatisticPax();
        }

        private void miGuaranteeManagement_Click(object sender, EventArgs e)
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miGuaranteeManagement' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmCommanderInforList objfmCommanderInforList = new fmCommanderInforList(m_LogAccount);
            objfmCommanderInforList.ShowDialog();
        }


        private void miStation_Click(object sender, EventArgs e)
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miStation' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmStationgList objfmStationgList = new fmStationgList();
            objfmStationgList.ShowDialog();

        }
        #endregion

        #region 点击菜单事件
        /// <summary>
        /// 显示用户管理窗体
        /// </summary>
        private void AccountManage()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miAccountManage' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmAccount objFmAccount = new fmAccount(m_LogAccount);
            objFmAccount.ShowDialog();
        }

        /// <summary>
        /// 航后机位安排
        /// </summary>
        private void ArrangeGate()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miArrangeGate' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmArrangeGate objfmArrangeGate = new fmArrangeGate(m_LogAccount, m_stationBM);
            objfmArrangeGate.ShowDialog();
        }

        /// <summary>
        /// 航后机位查询
        /// </summary>
        private void QueryGate()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miQueryGate' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmQueryGate objfmQueryGate = new fmQueryGate(m_LogAccount, m_stationBM);
            objfmQueryGate.ShowDialog();
        }

        /// <summary>
        /// 查询航班变更数据
        /// </summary>
        private void GetChangeData()
        {
            fmChangeData objfmChangeData = new fmChangeData(null, m_LogAccount, null, null, 1);
            objfmChangeData.ShowDialog();
        }

        /// <summary>
        /// 视图设置
        /// </summary>
        private void DataItemViewSetting()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemView' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //显示视图设置窗体
            fmDataItemView objfmDataItemView = new fmDataItemView(m_LogAccount);
            if (objfmDataItemView.ShowDialog() == DialogResult.OK)
            {
                //获取用户有权限的数据项
                DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
                ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount);
                dtDataItems = rvSF.Dt;


                //设置视图
                SpreadGrid spreadGrid = new SpreadGrid(m_LogAccount);

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightWatch)
                    {
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightWatch).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        (MdiChildren[iLoop] as fmFlightWatch).DataItems = dtDataItems;
                        (MdiChildren[iLoop] as fmFlightWatch).RefreshView();
                    }
                    else if (MdiChildren[iLoop] is fmFlightGuarantee)
                    {
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[1], dtDataItems, 0);
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[2], dtDataItems, 0);
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[3], dtDataItems, 0);

                        (MdiChildren[iLoop] as fmFlightGuarantee).DataItems = dtDataItems;
                        (MdiChildren[iLoop] as fmFlightGuarantee).RefreshView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.ActiveSheetIndex);
                    }
                }
            }
        }

        /// <summary>
        /// 设置显示席位
        /// </summary>
        private void SetPosition()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miPosition' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmSetPosition objfmSetPosition = new fmSetPosition(m_positionNameBM);
            if (objfmSetPosition.ShowDialog() == DialogResult.OK)
            {
                this.m_positionNameBM = objfmSetPosition.MPositionNameBM;

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightWatch)
                    {
                        (MdiChildren[iLoop] as fmFlightWatch).MPositionNameBM = m_positionNameBM;
                        (MdiChildren[iLoop] as fmFlightWatch).FlightRefresh();
                        statusBarMain.Panels[0].Text = "登陆席位:" + m_positionNameBM.PositionName;
                        statusBarMain.Panels[1].Text = "登陆用户:" + m_LogAccount.UserName;
                    }                   
                }

            }
        }

        /// <summary>
        /// 航班监控
        /// </summary>
        private void FlightWatch()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemWatch' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool blnFind = false;
            for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    MdiChildren[iLoop].WindowState = FormWindowState.Minimized;
                }
                else
                {
                    blnFind = true;
                    MdiChildren[iLoop].WindowState = FormWindowState.Maximized;
                }
            }

            if (blnFind == false)
            {
                fmFlightWatch objfmFlightWatch = new fmFlightWatch(m_LogAccount, dtStandardIntermissionTime, dtDataItems, m_positionNameBM);
                objfmFlightWatch.Show();
                objfmFlightWatch.MdiParent = this;
                objfmFlightWatch.WindowState = FormWindowState.Maximized;
               
            }

            statusBarMain.Panels[0].Text = "登陆席位:" + m_positionNameBM.PositionName;
            statusBarMain.Panels[1].Text = "登陆用户:" + m_LogAccount.UserName;
        }

        /// <summary>
        /// 航班保障
        /// </summary>
        private void FlightGuarantee()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemGuarantee' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool blnFind = false;
            for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    MdiChildren[iLoop].WindowState = FormWindowState.Minimized;
                }
                else
                {
                    blnFind = true;
                    MdiChildren[iLoop].WindowState = FormWindowState.Maximized;
                    (MdiChildren[iLoop] as fmFlightGuarantee).SetInOutFlightsNum();
                }
            }

            if (blnFind == false)
            {
                fmFlightGuarantee objfmFlightGuarantee = new fmFlightGuarantee(m_LogAccount, dtStandardIntermissionTime, dtDataItems, statusBarMain, m_iAutoAdjust);
                objfmFlightGuarantee.Show();
                objfmFlightGuarantee.MdiParent = this;
                objfmFlightGuarantee.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// 分配席位
        /// </summary>
        private void AssignAircraft()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miAssignAircraftNo' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmPositionInfor objfmPositionInfor = new fmPositionInfor();
            objfmPositionInfor.ShowDialog();
        }

        /// <summary>
        /// 系统提示
        /// </summary>
        private void SystemPrompt()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miSystemPrompt' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmSystemPrompt objfmSystemPrompt = new fmSystemPrompt(m_LogAccount, m_iAutoAdjust);
            objfmSystemPrompt.ShowDialog();

            m_iAutoAdjust = objfmSystemPrompt.AutoAdjust;
        }

        /// <summary>
        /// 显示条件
        /// </summary>
        private void DisplayCondition()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDisplayCondition' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmDisplayCondition objfmDisplayCondition = new fmDisplayCondition(m_LogAccount);

            if (objfmDisplayCondition.ShowDialog() == DialogResult.OK)
            {
                
                for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
                {
                    if ((MdiChildren[iLoop] is fmFlightWatch))
                    {
                        (MdiChildren[iLoop] as fmFlightWatch).ShowConditionFlights();
                    }
                }
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        private void EditPassword()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miEditPassword' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmEditPassword objfmEditPassword = new fmEditPassword(m_LogAccount);
            objfmEditPassword.ShowDialog();
        }

        /// <summary>
        /// 席位管理
        /// </summary>
        private void AddPosition()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miAddPosition' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmPostionManagement objfmPostionManagement = new fmPostionManagement();

            objfmPostionManagement.ShowDialog();
        }

        /// <summary>
        /// 航班计划
        /// </summary>
        private void FlightSchedule()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miFlightSchedule' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmFlightSchedule objfmFlightSchedule = new fmFlightSchedule(m_LogAccount);
            objfmFlightSchedule.ShowDialog();
        }

        private void StatisticPax()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miCheckNum' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmStatisticPax objfmStatisticPax = new fmStatisticPax(m_LogAccount);
            objfmStatisticPax.ShowDialog();
        }

        #endregion

        /// <summary>
        /// 根据航班号查找航班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbQuery_Click(object sender, EventArgs e)
        {
            fmQueryFlight objfmQueryFlight = new fmQueryFlight();


            objfmQueryFlight.StartPosition = FormStartPosition.CenterScreen;
            objfmQueryFlight.Text += "航班号";

            objfmQueryFlight.ShowDialog();

            if (!objfmQueryFlight.Find)
            {
                return;
            }

            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).FindFlightByFlightNo(objfmQueryFlight.FindContent);
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).FindFlightByFlightNo(objfmQueryFlight.FindContent);
                }
            }
        }

        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbZoomOut_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).ZoomOut();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).ZoomOut();
                }
            }
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbZoomIn_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).ZoomIn();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).ZoomIn();
                }
            }
        }

        /// <summary>
        /// 导出动态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbExport_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).ExportData();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).ExportData();
                }
            }
        }

        /// <summary>
        /// 打印航班动态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPrint_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).PrintData();
                }
            }
        }

        /// <summary>
        /// 刷新航班动态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRefresh_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).FlightRefresh();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).FlightRefresh();
                }
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 提示用户是否退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMDIMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出航站保障系统？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearLogInfo();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void miACMISCManage_Click(object sender, EventArgs e)
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miACMISCManage' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmACMISC objfmACMISC = new fmACMISC();
            objfmACMISC.ShowDialog();
        }

        /// <summary>
        /// 每隔三分钟向服务器端发送一次消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerHBT_Tick(object sender, EventArgs e)
        {
            AccountBF accountBF = new AccountBF();
            accountBF.UpdateOnlineUser(m_LogAccount, _iOnlineUserNo);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonItem_Click(object sender, EventArgs e)
        {
            fmStandardItem fmStandardItem = new fmStandardItem(m_LogAccount);
            fmStandardItem.ShowDialog();
        }

        private void 停机位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strStationName = "";
            string strAirportName = "";

            StationBF stationBF = new StationBF();
            strStationName = stationBF.GetStationByThreeCode(m_LogAccount.StationThreeCode).Dt.Rows[0]["cnvcStationName"].ToString();
            strAirportName = stationBF.GetStationByThreeCode(m_LogAccount.StationThreeCode).Dt.Rows[0]["cnvcAirportName"].ToString();

            fmGateInfo objfmGateInfo = new fmGateInfo(m_LogAccount, strStationName, strAirportName);
            objfmGateInfo.ShowDialog();
        }

        private void 配载工作量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).BalanceStatistics();
                }
            }
        }
    }
}