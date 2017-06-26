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

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmCommderInfor : Form
    {
        private AccountBM m_accountBM;

        public fmCommderInfor(AccountBM accountBM)
        {
            InitializeComponent();
            m_accountBM = accountBM;
        }

        private void fmCommderInfor_Load(object sender, EventArgs e)
        {
            StationBF stationBF = new StationBF();
            ReturnValueSF rvSF = stationBF.GetAllStation();

            cmbStation.DisplayMember = "cnvcStationName";
            cmbStation.ValueMember = "cncThreeCode";
            cmbStation.DataSource = rvSF.Dt;

            cmbStation.SelectedValue = m_accountBM.StationThreeCode;
            //cmbCommanderType.SelectedIndex = 2; //modified by LinYong in 20160406

            #region 保障人员类型信息 -- modified by LinYong in 20160321
            CommanderTypeBF commanderTypeBF = new CommanderTypeBF();
            ReturnValueSF rvSF_commanderTypeBF = commanderTypeBF.Select();

            cmbCommanderType.DataSource = rvSF_commanderTypeBF.Dt;
            cmbCommanderType.DisplayMember = "cnvcCommanderType";
            cmbCommanderType.ValueMember = "cniCommanderTypeID";
            #endregion 保障人员类型信息 -- modified by LinYong in 20160321
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            #region 检查录入项 --modified by LinYong in 20160406
            //检查 帐号
            if (txtCommanderAccount.Text.Trim() == "")
            {
                MessageBox.Show("帐号不能为空！");
                return;
            }

            //检查姓名
            if (txtCommanderName.Text.Trim() == "")
            {
                MessageBox.Show("姓名不能为空！");
                return;
            }

            //判断用户列表中是否存在此用户
            AccountBF accountBF = new AccountBF();
            ReturnValueSF returnValueSF = accountBF.GetAccountByUserId(txtCommanderAccount.Text.Trim());
            if (returnValueSF.Result <= 0)
            {
                MessageBox.Show("访问用户信息表出错：" + Environment.NewLine + returnValueSF.Message);
            }
            else if (returnValueSF.Dt.Rows.Count == 0)
            {
                MessageBox.Show("用户信息表没有此用户，请注意及时添加！");
            }
            else if (returnValueSF.Dt.Rows[0]["cnvcUserName"].ToString().Trim() != txtCommanderName.Text.Trim())
            {
                MessageBox.Show("用户信息表中此帐号的名称[" + returnValueSF.Dt.Rows[0]["cnvcUserName"].ToString().Trim() + "]与名称录入项的信息不匹配，请注意确认！");
            }
            #endregion 检查录入项 --modified by LinYong in 20160406

            CommanderInforBM commanderInforBM = new CommanderInforBM();
            commanderInforBM.CommanderAccount = txtCommanderAccount.Text.Trim();
            commanderInforBM.CommanderName = txtCommanderName.Text;
            commanderInforBM.CommanderType = cmbCommanderType.Text;
            commanderInforBM.ThreeCode = cmbStation.SelectedValue.ToString();

            CommandInforBF commandInforBF = new CommandInforBF();
            ReturnValueSF rvSF = commandInforBF.InsertCommander(commanderInforBM);

            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}