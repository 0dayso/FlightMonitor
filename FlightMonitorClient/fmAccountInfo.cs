using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// 在添加或修改用户信息时显示的窗体
    /// </summary>
    public partial class fmAccountInfo : Form
    {
        //定义返回值
        private ReturnValueSF m_rvSF = new ReturnValueSF();      
   
        //编辑用户时传入的帐号实体对象
        private AccountBM m_accountBM;

        //标识是添加还是修改
        private int m_iUpdate = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public fmAccountInfo()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        public fmAccountInfo(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
            this.m_iUpdate = 1;        

        }

        /// <summary>
        /// 返回值：便于父窗体访问
        /// </summary>
        public ReturnValueSF RvSF
        {
            get { return m_rvSF; }
        }

        
        /// <summary>
        /// 确定按钮click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //帐号不能为空
            if (!Validator.CheckNull(txtUserID.Text, "帐号不能为空！"))
            {
                return;
            }

            //刷新时间只能为数字
            if(!Validator.IsNumber(txtRefreshInterval.Text, "刷新时间只能为数字！"))
            {
                return;
            }

            //最大登陆数只能为数字
            if (!Validator.IsNumber(txtMaxUser.Text, "最大登陆数只能为数字！"))
            {
                return;
            }

            //生成业务实体对象
            FlightMonitorBM.AccountBM accountBM = new AccountBM();
            accountBM.UserId = txtUserID.Text.Trim();
            accountBM.UserName = txtUserName.Text.Trim();
            accountBM.UserTypeId = cmbUserType.SelectedIndex + 1;
            
            accountBM.StationThreeCode = cmbStationThreeCode.SelectedValue.ToString();
            accountBM.UserDepartment = txtDepartMent.Text.Trim();
            accountBM.RefreshInterval = Convert.ToInt32(txtRefreshInterval.Text);
            if (m_iUpdate == 0)
            {
                accountBM.SplashAutoStop = 1;
                accountBM.SplashSeconds = 50;
                accountBM.SoundType = 0;
                accountBM.StartGuarantee = 1;
                accountBM.GuaranteeMinutes = 60;
                accountBM.Boarding = 0;
                accountBM.BoardingMinutes = 30;
                accountBM.MCCReady = 0;
                accountBM.MCCReadyMinutes = 15;
                accountBM.MCCRelease = 0;
                accountBM.MCCReleasMinutes = 15;
                accountBM.TDWNPromt = 1;
                accountBM.TDWNMinutes = 5;
                accountBM.TOFFPromt = 1;
                accountBM.TOFFMinutes = 0;
                accountBM.IntermissionPrompt = 1;
                accountBM.IntermissionMinutes = 5;
                accountBM.ClosePaxCabinPromt = 1;
                accountBM.DisplayAll = 1;
                accountBM.DisplayDelay = 1;
                accountBM.DelayMinutes = 120;
                accountBM.DisplayDiversion = 1;
                accountBM.DisplayIntermission = 1;
                accountBM.DisplayTDWN = 1;
                accountBM.DisplayTOFF = 1;
                accountBM.DisplayClosePaxCabin = 1;
                accountBM.UserPassword = "111111";
            }
            else
            {
                accountBM.UserPassword = m_accountBM.UserPassword;
                accountBM.SplashAutoStop = m_accountBM.SplashAutoStop;
                accountBM.SplashSeconds = m_accountBM.SplashSeconds;
                accountBM.SoundType = m_accountBM.SoundType;
                accountBM.StartGuarantee = m_accountBM.StartGuarantee;
                accountBM.GuaranteeMinutes = m_accountBM.GuaranteeMinutes;
                accountBM.Boarding = m_accountBM.Boarding;
                accountBM.BoardingMinutes = m_accountBM.BoardingMinutes;
                accountBM.MCCReady = m_accountBM.MCCReady;
                accountBM.MCCReadyMinutes = m_accountBM.MCCReadyMinutes;
                accountBM.MCCRelease = m_accountBM.MCCRelease;
                accountBM.MCCReleasMinutes = m_accountBM.MCCReleasMinutes;
                accountBM.TDWNPromt = m_accountBM.TDWNPromt;
                accountBM.TDWNMinutes = m_accountBM.TDWNMinutes;
                accountBM.TOFFPromt = m_accountBM.TOFFPromt;
                accountBM.TOFFPromt = m_accountBM.TOFFPromt;
                accountBM.IntermissionPrompt = m_accountBM.IntermissionPrompt;
                accountBM.IntermissionMinutes = m_accountBM.IntermissionMinutes;
                accountBM.ClosePaxCabinPromt = m_accountBM.ClosePaxCabinPromt;
                accountBM.DisplayAll = m_accountBM.DisplayAll;
                accountBM.DisplayDelay = m_accountBM.DisplayDelay;
                accountBM.DelayMinutes = m_accountBM.DelayMinutes;
                accountBM.DisplayDiversion = m_accountBM.DisplayDiversion;
                accountBM.DisplayIntermission = m_accountBM.DisplayIntermission;
                accountBM.DisplayTDWN = m_accountBM.DisplayTDWN;
                accountBM.DisplayTOFF = m_accountBM.DisplayTOFF;
                accountBM.DisplayClosePaxCabin = m_accountBM.DisplayClosePaxCabin;
            }

            accountBM.MaxUser = Convert.ToInt32(txtMaxUser.Text);

            IList ilDataItemPurview = GetInitialDataItemPurview(accountBM);
            
            //调用业务外观层方法
            FlightMonitorBF.AccountBF accountBF = new AccountBF();

            //添加
            if (m_iUpdate == 0)
            {
                m_rvSF = accountBF.Insert(accountBM, ilDataItemPurview);
            }
            else //更新
            {
                m_rvSF = accountBF.UpdateAllInfo(accountBM, ilDataItemPurview);
            }

            if (m_rvSF.Result < 0)
            {
                MessageBox.Show(m_rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmAccountInfo_Load(object sender, EventArgs e)
        {
            Station_Bind();           
            cmbUserType.SelectedIndex = 1;
            this.ActiveControl = txtUserID;

            if (m_iUpdate == 1)
            {
                //显示用户要修改的用户信息
                txtUserID.Text = m_accountBM.UserId;
                txtUserName.Text = m_accountBM.UserName;
                txtDepartMent.Text = m_accountBM.UserDepartment;
                txtRefreshInterval.Text = m_accountBM.RefreshInterval.ToString();
                SetSelectedStation(m_accountBM);
                cmbUserType.Text = m_accountBM.UserTypeName;
                txtMaxUser.Text = m_accountBM.MaxUser.ToString();
            }
        }

        /// <summary>
        /// 绑定航站信息
        /// </summary>
        private void Station_Bind()
        {
            //设置显示属性
            cmbStationThreeCode.DisplayMember = "cnvcAirportName";
            cmbStationThreeCode.ValueMember = "cncThreeCode";
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            //调用业务外观层方法
            FlightMonitorBF.StationBF stationBF = new StationBF();
            rvSF = stationBF.GetAllStation();

            if (rvSF.Result > 0)
            {
                //添加一个空行                
                cmbStationThreeCode.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 取消按钮click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {        
            this.Close();
        }             

        /// <summary>
        /// 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            Validator.KeyDown(sender, e);
        }

        /// <summary>
        /// 设置航站显示的项
        /// </summary>
        /// <param name="accountBM"></param>
        private void SetSelectedStation(FlightMonitorBM.AccountBM accountBM)
        {
            for (int iLoop = 0; iLoop < cmbStationThreeCode.Items.Count; iLoop++)
            {
                DataRowView rowItem = (DataRowView)cmbStationThreeCode.Items[iLoop];

                if (rowItem["cncThreeCode"].ToString() == accountBM.StationThreeCode)
                {
                    cmbStationThreeCode.SelectedIndex = iLoop;
                    break;
                }
            }
        }

        /// <summary>
        /// 获取初始化权限列表
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        /// <returns>初时数据项权限列表</returns>
        private IList GetInitialDataItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            IList ilDataItemPurview = new ArrayList();
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvDataItems = dataItemPurviewBF.GetDataItems();

            int iRowIndex = 0;
            foreach (DataRow rowItem in rvDataItems.Dt.Rows)
            {
                FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem, DataItemPurviewBM.DataType.ITEM);
                dataItemPurviewBM.DataItemPurview = 1;
                dataItemPurviewBM.DataItemVisible = 1;
                dataItemPurviewBM.ViewIndex = iRowIndex;
                dataItemPurviewBM.SplashPromptItem = 1;
                dataItemPurviewBM.SoundPromptItem = 1;
                dataItemPurviewBM.UserID = accountBM.UserId;
                ilDataItemPurview.Add(dataItemPurviewBM);
                iRowIndex++;
            }
            return ilDataItemPurview;
        }

       
    }
}