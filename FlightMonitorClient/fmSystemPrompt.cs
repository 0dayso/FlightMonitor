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
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmSystemPrompt : Form
    {
        private AccountBM m_accountBM;
        private int m_iAutoAdjust = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountBM"></param>
        public fmSystemPrompt(AccountBM accountBM, int iAutoAdjust)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
            this.m_iAutoAdjust = iAutoAdjust;
        }

        public int AutoAdjust
        {
            get
            {
                return m_iAutoAdjust;
            }
            set
            {
                m_iAutoAdjust = value;
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmSystemPrompt_Load(object sender, EventArgs e)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //闪烁提示参数设定
            if (m_accountBM.SplashAutoStop == 1)
            {
                radAuto.Checked = true;
                radHand.Checked = false;
                updownSplashMinutes.Enabled = true;
                updownSplashMinutes.Value = m_accountBM.SplashSeconds;
            }
            else
            {
                radAuto.Checked = false;
                radHand.Checked = true;
                updownSplashMinutes.Enabled = false;               
            }

            //声音提示参数设定
            cmbSoundType.SelectedIndex = m_accountBM.SoundType;

            //监控提示
            if (m_accountBM.TDWNPromt == 1)
            {
                chkTDWN.Checked = true;
                updownTDWN.Value = m_accountBM.TDWNMinutes;
            }
            else
            {
                chkTDWN.Checked = false;
                updownTDWN.Value = m_accountBM.TDWNMinutes;
                updownTDWN.Enabled = false;
            }
            if (m_accountBM.TOFFPromt == 1)
            {
                chkTOFF.Checked = true;
                updownTOFF.Value = m_accountBM.TOFFMinutes;
            }
            else
            {
                chkTOFF.Checked = false;
                updownTOFF.Value = m_accountBM.TOFFMinutes;
                updownTOFF.Enabled = false;
            }
            if (m_accountBM.IntermissionPrompt == 1)
            {
                chkIntermission.Checked = true;
                updownIntermission.Value = m_accountBM.IntermissionMinutes;
            }
            else
            {
                chkIntermission.Checked = false;
                updownIntermission.Value = m_accountBM.IntermissionMinutes;
                updownIntermission.Enabled = false;
            }
            if (m_accountBM.ClosePaxCabinPromt == 1)
            {
                chkClosePaxCabin.Checked = true;
            }
            else
            {
                chkClosePaxCabin.Checked = false;
            }

            //保障提示
            updownGuaranteeMinutes.Value = m_accountBM.GuaranteeMinutes;
            if (m_accountBM.StartGuarantee == 1)
            {
                chkStartGuarantee.Checked = true;
            }
            else
            {
                chkStartGuarantee.Checked = false;
                updownGuaranteeMinutes.Enabled = false;
            }

            updownBoardingMinutes.Value = m_accountBM.BoardingMinutes;
            if (m_accountBM.Boarding == 1)
            {
                chkStartBoarding.Checked = true;
            }
            else
            {
                chkStartBoarding.Checked = false;
                updownBoardingMinutes.Enabled = false;
            }

            updownMccReadyMinutes.Value = m_accountBM.MCCReadyMinutes;
            if (m_accountBM.MCCReady == 1)
            {
                chkMCCReady.Checked = true;
            }
            else
            {
                chkMCCReady.Checked = false;
                updownMccReadyMinutes.Enabled = false;
            }

            updownMccReleaseMinutes.Value = m_accountBM.MCCReleasMinutes;
            if (m_accountBM.MCCRelease == 1)
            {
                chkMCCRelease.Checked = true;
            }
            else
            {
                chkMCCRelease.Checked = false;
                updownMccReleaseMinutes.Enabled = false;
            }


            //调用数据访问权限业务外观层
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();

            rvSF = dataItemPurviewBF.GetDataItemPurviewByUserId(m_accountBM);

            if (rvSF.Result > 0)
            {
                //绑定闪烁提示显示项
                chklsSplashPromptItem.DataSource = rvSF.Dt;

                chklsSplashPromptItem.ValueMember = "cniDataItemNo";
                chklsSplashPromptItem.DisplayMember = "cnvcDataItemName";

                //绑定声音提示显示项
                chklsSoundPromptItem.DataSource = rvSF.Dt;

                chklsSoundPromptItem.ValueMember = "cniDataItemNo";
                chklsSoundPromptItem.DisplayMember = "cnvcDataItemName";

                //设置是否已经选中了项
                int iRowIndex = 0;
                foreach (DataRow dataRow in rvSF.Dt.Rows)
                {                   
                    if (Convert.ToInt32(dataRow["cniSplashPromptItem"].ToString()) == 1)
                    {
                        chklsSplashPromptItem.SetItemChecked(iRowIndex, true);
                    }
                    else
                    {
                        chklsSplashPromptItem.SetItemChecked(iRowIndex, false);
                    }

                    if (Convert.ToInt32(dataRow["cniSoundPromptItem"].ToString()) == 1)
                    {
                        chklsSoundPromptItem.SetItemChecked(iRowIndex, true);
                    }
                    else
                    {
                        chklsSoundPromptItem.SetItemChecked(iRowIndex, false);
                    }
                    iRowIndex += 1;
                }
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (m_iAutoAdjust == 1)
            {
                chkAutoAdjust.Checked = true;
            }
            else
            {
                chkAutoAdjust.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radAuto.Checked == true)
            {
                updownSplashMinutes.Enabled = true;
            }
            else
            {
                updownSplashMinutes.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTDWN_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTDWN.Checked == true)
            {
                updownTDWN.Enabled = true;
            }
            else
            {
                updownTDWN.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTOFF_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTOFF.Checked == true)
            {
                updownTOFF.Enabled = true;
            }
            else
            {
                updownTOFF.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIntermission_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntermission.Checked == true)
            {
                updownIntermission.Enabled = true;
            }
            else
            {
                updownIntermission.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkStartGuarantee_CheckedChanged(object sender, EventArgs e)
        {
            updownGuaranteeMinutes.Enabled = chkStartGuarantee.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkStartBoarding_CheckedChanged(object sender, EventArgs e)
        {            
                updownBoardingMinutes.Enabled = chkStartBoarding.Checked;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMCCReady_CheckedChanged(object sender, EventArgs e)
        {
            updownMccReadyMinutes.Enabled = chkMCCReady.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMCCRelease_CheckedChanged(object sender, EventArgs e)
        {
            updownMccReleaseMinutes.Enabled = chkMCCRelease.Checked;
        }

        /// <summary>
        /// 保存系统提示设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //将除闪烁提示和声音提示信息保存到表tbUser表中
            //闪烁提示参数设定
            if (radAuto.Checked == true)
            {
                m_accountBM.SplashAutoStop = 1;
                m_accountBM.SplashSeconds = Convert.ToInt32(updownSplashMinutes.Value);
            }
            else
            {
                m_accountBM.SplashAutoStop = 0;
            }
            //声音提示参数设定
            m_accountBM.SoundType = cmbSoundType.SelectedIndex;

            //监控提示
            if (chkTDWN.Checked == true)
            {
                m_accountBM.TDWNPromt = 1;
                m_accountBM.TDWNMinutes = Convert.ToInt32(updownTDWN.Value);
            }
            else
            {
                m_accountBM.TDWNPromt = 0;
            }

            if (chkTOFF.Checked == true)
            {
                m_accountBM.TOFFPromt = 1;
                m_accountBM.TOFFMinutes = Convert.ToInt32(updownTOFF.Value);
            }
            else
            {
                m_accountBM.TOFFPromt = 0;
            }

            if (chkIntermission.Checked == true)
            {
                m_accountBM.IntermissionPrompt = 1;
                m_accountBM.IntermissionMinutes = Convert.ToInt32(updownIntermission.Value);
            }
            else
            {
                m_accountBM.IntermissionPrompt = 0;
            }

            if (chkClosePaxCabin.Checked == true)
            {
                m_accountBM.ClosePaxCabinPromt = 1;               
            }
            else
            {
                m_accountBM.ClosePaxCabinPromt = 0;
            }

            //保障提示
            m_accountBM.StartGuarantee = chkStartGuarantee.Checked ? 1 : 0;
            m_accountBM.GuaranteeMinutes = Convert.ToInt32(updownGuaranteeMinutes.Value);

            m_accountBM.Boarding = chkStartBoarding.Checked ? 1 : 0;
            m_accountBM.BoardingMinutes = Convert.ToInt32(updownBoardingMinutes.Value);

            m_accountBM.MCCReady = chkMCCReady.Checked ? 1 : 0;
            m_accountBM.MCCReadyMinutes = Convert.ToInt32(updownMccReadyMinutes.Value);

            m_accountBM.MCCRelease = chkMCCRelease.Checked ? 1 : 0;
            m_accountBM.MCCReleasMinutes = Convert.ToInt32(updownMccReleaseMinutes.Value);

            //将闪烁提示和声音提示保存到tbDataItemPurview表中
            IList ilDataItemPurviewBM = new ArrayList();
            DataTable dtDataItem = (DataTable) chklsSplashPromptItem.DataSource;
            int iRowIndex = 0;
            foreach (DataRow dataRow in dtDataItem.Rows)
            {
                DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(dataRow, DataItemPurviewBM.DataType.PURVIEW);
                if (dataItemPurviewBM.DataItemVisible == 1)
                {
                    dataItemPurviewBM.SplashPromptItem = chklsSplashPromptItem.GetItemChecked(iRowIndex) == true ? 1 : 0;
                }
                else
                {
                    dataItemPurviewBM.SplashPromptItem = 0;
                }
                dataItemPurviewBM.SoundPromptItem = chklsSoundPromptItem.GetItemChecked(iRowIndex) == true ? 1 : 0;
                ilDataItemPurviewBM.Add(dataItemPurviewBM);
                iRowIndex += 1;
            }



            //调用业务外观层方法
            AccountBF accountBF = new AccountBF();
            ReturnValueSF rvSF = accountBF.UpdateAllInfo(m_accountBM, ilDataItemPurviewBM);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }

            string strAdjust = "0";
            m_iAutoAdjust = 0;
            if(chkAutoAdjust.Checked == true)
            {
                strAdjust = "1";
                m_iAutoAdjust = 1;
            }
            ConfigSettings.WriteSetting("AutoAdjust", strAdjust);


            
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}