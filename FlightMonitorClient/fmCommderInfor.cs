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

            #region ������Ա������Ϣ -- modified by LinYong in 20160321
            CommanderTypeBF commanderTypeBF = new CommanderTypeBF();
            ReturnValueSF rvSF_commanderTypeBF = commanderTypeBF.Select();

            cmbCommanderType.DataSource = rvSF_commanderTypeBF.Dt;
            cmbCommanderType.DisplayMember = "cnvcCommanderType";
            cmbCommanderType.ValueMember = "cniCommanderTypeID";
            #endregion ������Ա������Ϣ -- modified by LinYong in 20160321
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            #region ���¼���� --modified by LinYong in 20160406
            //��� �ʺ�
            if (txtCommanderAccount.Text.Trim() == "")
            {
                MessageBox.Show("�ʺŲ���Ϊ�գ�");
                return;
            }

            //�������
            if (txtCommanderName.Text.Trim() == "")
            {
                MessageBox.Show("��������Ϊ�գ�");
                return;
            }

            //�ж��û��б����Ƿ���ڴ��û�
            AccountBF accountBF = new AccountBF();
            ReturnValueSF returnValueSF = accountBF.GetAccountByUserId(txtCommanderAccount.Text.Trim());
            if (returnValueSF.Result <= 0)
            {
                MessageBox.Show("�����û���Ϣ�����" + Environment.NewLine + returnValueSF.Message);
            }
            else if (returnValueSF.Dt.Rows.Count == 0)
            {
                MessageBox.Show("�û���Ϣ��û�д��û�����ע�⼰ʱ��ӣ�");
            }
            else if (returnValueSF.Dt.Rows[0]["cnvcUserName"].ToString().Trim() != txtCommanderName.Text.Trim())
            {
                MessageBox.Show("�û���Ϣ���д��ʺŵ�����[" + returnValueSF.Dt.Rows[0]["cnvcUserName"].ToString().Trim() + "]������¼�������Ϣ��ƥ�䣬��ע��ȷ�ϣ�");
            }
            #endregion ���¼���� --modified by LinYong in 20160406

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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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