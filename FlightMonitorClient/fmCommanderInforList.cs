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
    public partial class fmCommanderInforList : Form
    {
        private AccountBM m_accountBM;

        public fmCommanderInforList(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }

        private void fmCommanderInforList_Load(object sender, EventArgs e)
        {
            StationBF stationBF = new StationBF();
            ReturnValueSF rvSF = stationBF.GetAllStation();

            cmbStation.DisplayMember = "cnvcStationName";
            cmbStation.ValueMember = "cncThreeCode";
            cmbStation.DataSource = rvSF.Dt;

            cmbStation.SelectedValue = m_accountBM.StationThreeCode;

            #region 保障人员类型信息 -- modified by LinYong in 20160321
            CommanderTypeBF commanderTypeBF = new CommanderTypeBF();
            ReturnValueSF rvSF_commanderTypeBF = commanderTypeBF.Select();

            cmbCommanderType.DataSource = rvSF_commanderTypeBF.Dt;
            cmbCommanderType.DisplayMember = "cnvcCommanderType";
            cmbCommanderType.ValueMember = "cniCommanderTypeID";

            cmbCommanderType.SelectedIndex = 0;
            #endregion 保障人员类型信息 -- modified by LinYong in 20160321

            fpCommander.Sheets[0].Columns[0].DataField = "cnvCommanderAccount";
            fpCommander.Sheets[0].Columns[1].DataField = "cnvcCommanderName";
            fpCommander.Sheets[0].Columns[2].DataField = "cnvcCommanderType";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (fpCommander.Sheets[0].SelectionCount == 0)
            //{
            //    MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            //DataTable dtCommander = (DataTable)fpCommander.Sheets[0].DataSource;
            //CommanderInforBM commanderInforBM = new CommanderInforBM(dtCommander.Rows[fpCommander.Sheets[0].Models.Selection.AnchorRow]);

            fmCommderInfor objfmCommderInfor = new fmCommderInfor(m_accountBM);
            if (objfmCommderInfor.ShowDialog() == DialogResult.OK)
            {
                GetCommanderByStationAndType();
            }
        }

        private void btnDeleteCommander_Click(object sender, EventArgs e)
        {
            if (fpCommander.Sheets[0].SelectionCount == 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("是否删除所选的用户？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            DataTable dtCommander = (DataTable)fpCommander.Sheets[0].DataSource;
            CommanderInforBM commanderInforBM = new CommanderInforBM(dtCommander.Rows[fpCommander.Sheets[0].Models.Selection.AnchorRow]);

            CommandInforBF commanderInforBF = new CommandInforBF();
            commanderInforBF.DeleteCommander(commanderInforBM);

            GetCommanderByStationAndType();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCommanderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCommanderByStationAndType();
        }

        private void cmbStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCommanderByStationAndType();
        }

        private void GetCommanderByStationAndType()
        {

            ReturnValueSF rvSF = new ReturnValueSF();
            CommandInforBF commandInforBF = new CommandInforBF();

            if (cmbCommanderType.Text != "全部")
            {                
                rvSF = commandInforBF.GetCommanderByTypeAndStation(cmbCommanderType.Text, cmbStation.SelectedValue.ToString());
            }
            else
            {
                rvSF = commandInforBF.GetCommanderByStation(cmbStation.SelectedValue.ToString());
            }
            
            fpCommander.DataSource = rvSF.Dt;
        }
    }
}