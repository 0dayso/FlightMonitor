using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    /// <summary>
    /// 签派放行系统主界面
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：张  黎
    /// 创建日期：2008-07-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public partial class fmMDIFlightDisp : Form
    {
        private DataTable dtMenuItemPurview;  //系统权限
        private PositionNameBM m_positionNameBM;   //席位名称

        public fmMDIFlightDisp()
        {
            InitializeComponent();
        }

        private static AccountBM m_LogAccount;
        /// <summary>
        /// 用户登陆信息
        /// </summary>
        public static AccountBM LogAccount
        {
            get { return m_LogAccount; }
            set { m_LogAccount = value; }
        }

        private int _iDispFlightsCount = 0;
        /// <summary>
        /// 待放行航班数量
        /// </summary>
        public int DispFlightsCount
        {
            get { return _iDispFlightsCount; }
            set { _iDispFlightsCount = value; }
        }

        private int _iMoniFlightsCount = 0;
        /// <summary>
        /// 监控航班数量
        /// </summary>
        public int MoniFlightsCount
        {
            get { return _iMoniFlightsCount; }
            set { _iMoniFlightsCount = value; }
        }
        #region 载入窗体
        private void fmMDIFlightDisp_Load(object sender, EventArgs e)
        {
            //返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            //获取系统权限
            MenuPurviewBF menuPurviewBF = new MenuPurviewBF();
            rvSF = menuPurviewBF.GetMenuItemPurview(m_LogAccount);
            dtMenuItemPurview = rvSF.Dt;
            //席位名称实体对象
            int iPositionId = Convert.ToInt32(ConfigSettings.ReadSetting("PositionID").ToString());
            m_positionNameBM = new PositionNameBM();
            m_positionNameBM.PositionID = iPositionId;
            PositionNameBF positionNameBF = new PositionNameBF();
            rvSF = positionNameBF.GetPositionByID(m_positionNameBM);
            if (rvSF.Result > 0)
            {
                if (rvSF.Dt.Rows.Count > 0)
                {
                    m_positionNameBM = new PositionNameBM(rvSF.Dt.Rows[0]);
                }
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            statusBarMain.Panels[0].Text = "登陆席位:" + m_positionNameBM.PositionName;
            statusBarMain.Panels[1].Text = "登陆用户:" + m_LogAccount.UserName;
            //查询席位所有的航班
            PositionInforBF positionInforBF = new PositionInforBF();
            rvSF = positionInforBF.GetInforByPositionId(m_positionNameBM);
            DataTable dtDeskAircrafts = rvSF.Dt;
            
            //打开子窗体
            string strDispForm = "Dsp";
            string strMoniForm = "Mon";

            fmFlightMoni objfmFlightMoni = new fmFlightMoni(m_LogAccount, strMoniForm, dtDeskAircrafts, m_positionNameBM);
            fmFlightDisp objfmFlightDisp = new fmFlightDisp(m_LogAccount, strDispForm, dtDeskAircrafts, m_positionNameBM);

            string strDeskInfo = "席位飞机总数：" + dtDeskAircrafts.Rows.Count.ToString();
            strDeskInfo += "    席位航班总数：" + Convert.ToString(objfmFlightDisp.DisplayDeskFlights.Rows.Count + objfmFlightMoni.DisplayDeskFlights.Rows.Count);
            statusBarMain.Panels[2].Text = strDeskInfo;

            strDeskInfo = "";
            strDeskInfo = "待放行航班：" + Convert.ToString(objfmFlightDisp.DisplayDeskFlights.Rows.Count);
            strDeskInfo += "    已放行航班：" + Convert.ToString(objfmFlightMoni.DisplayDeskFlights.Rows.Count);
            statusBarMain.Panels[3].Text = strDeskInfo;

            objfmFlightMoni.Show();
            objfmFlightDisp.Show();

            objfmFlightMoni.MdiParent = this;
            objfmFlightDisp.MdiParent = this;

            objfmFlightDisp.WindowState = FormWindowState.Maximized;
            objfmFlightMoni.WindowState = FormWindowState.Maximized;

            objfmFlightDisp.Activate();
            //垂直排列
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }
        #endregion

        #region 菜单及工具栏按钮事件

        #region 用户信息管理
        private void miAccountManage_Click(object sender, EventArgs e)
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
        #endregion

        #region 席位管理
        /// <summary>
        /// 席位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeskManager_Click(object sender, EventArgs e)
        {
            ManageDesk();
        }
        private void miAddPosition_Click(object sender, EventArgs e)
        {
            ManageDesk();
        }
        #endregion

        #region 设置视图
        /// <summary>
        /// 设置视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSetView_Click(object sender, EventArgs e)
        {
            SetView();
        }
        private void miDataItemView_Click(object sender, EventArgs e)
        {
            SetView();
        }
        #endregion

        #region 选择席位
        /// <summary>
        /// 选择席位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDeskChange_Click(object sender, EventArgs e)
        {
            DeskChange();
        }
        private void miPosition_Click(object sender, EventArgs e)
        {
            DeskChange();
        }
        #endregion

        #region 将飞机分配到席位
        private void miAssignAircraftNo_Click(object sender, EventArgs e)
        {
            AssignAircraft();
        }
        #endregion

        #region 飞机信息管理
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
        #endregion

        #region 刷新视图
        private void tbRefresh_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightDisp)
                {
                    (MdiChildren[iLoop] as fmFlightDisp).FlightRefresh();
                }
                else if (MdiChildren[iLoop] is fmFlightMoni)
                {
                    (MdiChildren[iLoop] as fmFlightMoni).FlightRefresh();
                }
            }
        }
        #endregion

        #endregion

        #region 选择席位
        private void DeskChange()
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
                PositionInforBF positionInforBF = new PositionInforBF();
                ReturnValueSF rvSF = positionInforBF.GetInforByPositionId(m_positionNameBM);
                DataTable dtDeskAircrafts = rvSF.Dt;

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightDisp)
                    {
                        (MdiChildren[iLoop] as fmFlightDisp).DeskChange(dtDeskAircrafts);
                    }

                    if (MdiChildren[iLoop] is fmFlightMoni)
                    {
                        (MdiChildren[iLoop] as fmFlightMoni).DeskChange(dtDeskAircrafts);
                    }

                    statusBarMain.Panels[0].Text = "登陆席位:" + m_positionNameBM.PositionName;
                    statusBarMain.Panels[1].Text = "登陆用户:" + m_LogAccount.UserName;
                }
            }
        }
        #endregion

        #region 管理席位
        private void ManageDesk()
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
        #endregion

        #region 设置视图
        private void SetView()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemView' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //显示视图设置窗体
            fmDispDataItemView objfmDataItemView = new fmDispDataItemView(m_LogAccount);
            if (objfmDataItemView.ShowDialog() == DialogResult.OK)
            {
                //设置视图
                SpreadGrid spreadGrid = new SpreadGrid(m_LogAccount);

                //获取用户有权限的数据项
                DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightDisp)
                    {
                        ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount, "Dsp");
                        DataTable dtDataItems = rvSF.Dt;
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightDisp).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        (MdiChildren[iLoop] as fmFlightDisp).ViewRefresh(dtDataItems);
                    }

                    if (MdiChildren[iLoop] is fmFlightMoni)
                    {
                        ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount, "Mon");
                        DataTable dtDataItems = rvSF.Dt;
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightMoni).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        (MdiChildren[iLoop] as fmFlightMoni).ViewRefresh(dtDataItems);
                    }
                }//for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            }//if (objfmDataItemView.ShowDialog() == DialogResult.OK)
        }
        #endregion

        #region 将飞机分配到席位
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
        #endregion

        #region 主窗口关闭事件
        private void fmMDIFlightDisp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearLogInfo();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 清除登陆用户信息
        /// </summary>
        private void ClearLogInfo()
        {
            //调用业务外观层方法
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            m_LogAccount.LogUser -= 1;
            accountBF.UpdateLogUser(m_LogAccount);

            int iLogIndex = 1;
            for (int iLoop = 1; iLoop <= 100; iLoop++)
            {
                if (ConfigSettings.ReadSetting("LogOnUser" + iLoop.ToString()).ToString() == m_LogAccount.UserId)
                {
                    iLogIndex = iLoop;
                    break;
                }
            }
            ConfigSettings.WriteSetting("LogOFF" + iLogIndex.ToString(), "1");
        }
        #endregion


        private void timerRemind_Tick(object sender, EventArgs e)
        {
            Image imgSpash = Image.FromFile("..\\..\\Resources\\Splash.gif");
            tbRemind.Image = imgSpash;
        }

        private void tbRemind_Click(object sender, EventArgs e)
        {
            Image imgNormal = Image.FromFile("..\\..\\Resources\\Normal.gif");
            tbRemind.Image = imgNormal;
            timerRemind.Enabled = false;
            fmRemindInfo obj = new fmRemindInfo(m_positionNameBM);
            obj.ShowDialog();
            timerRemind.Enabled = true;
        }
    }
}
