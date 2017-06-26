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
    public partial class fmMaintenList : Form
    {
        private MaintenGuaranteeInforBM m_maintenGuaranteeInforBM;
        private ChangeRecordBM m_changeRecordBM;
        private StationBM m_stationBM;
        private AccountBM m_accountBM;

        public fmMaintenList(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeRecordBM changeRecordBM, StationBM stationBM)
        {
            InitializeComponent();
            this.m_maintenGuaranteeInforBM = maintenGuaranteeInforBM;
            this.m_changeRecordBM = changeRecordBM;
            this.m_stationBM = stationBM;
        }

        public fmMaintenList(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeRecordBM changeRecordBM, StationBM stationBM, AccountBM accountBM) 
        {
            InitializeComponent();
            this.m_maintenGuaranteeInforBM = maintenGuaranteeInforBM;
            this.m_changeRecordBM = changeRecordBM;
            this.m_stationBM = stationBM;
            this.m_accountBM = accountBM;
        }

        public MaintenGuaranteeInforBM MMaintenGuaranteeInforBM
        {
            get { return m_maintenGuaranteeInforBM; }
        }

        private void fmMaintenList_Load(object sender, EventArgs e)
        {
            ReturnValueSF rvSF = new ReturnValueSF();
            switch (MMaintenGuaranteeInforBM.FieldName)
            {
                case "cnvcInDelayCode":
                    cmbList.ValueMember = "cnvcFlightDelayCode";
                    cmbList.DisplayMember = "cnvcDelayName";
                    InOutDelayReasonBF inDelayReasonBF = new InOutDelayReasonBF();
                    rvSF = inDelayReasonBF.GetAllInOutDelayReason();
                    break;
                case "cnvcDischargingDelCode":
                    cmbList.ValueMember = "cnvcFlightDelayCode";
                    cmbList.DisplayMember = "cnvcDelayName";
                    DischargingDelayReasonBF dischargingDelayReasonBF = new DischargingDelayReasonBF();
                    rvSF = dischargingDelayReasonBF.GetAllDischargingDelayReason();
                    break;
                case "cnvcOutDelayCode":
                    cmbList.ValueMember = "cnvcFlightDelayCode";
                    cmbList.DisplayMember = "cnvcDelayName";
                    InOutDelayReasonBF outDelayReasonBF = new InOutDelayReasonBF();
                    rvSF = outDelayReasonBF.GetAllInOutDelayReason();
                    break;
                case "cnvcChiefController":
                    cmbList.ValueMember = "cnvcCommanderName";
                    cmbList.DisplayMember = "cnvcCommanderName";
                    CommandInforBF chiefControllerBF = new CommandInforBF();
                    rvSF = chiefControllerBF.GetCommanderByTypeAndStation("主控", m_stationBM.ThreeCode);
                    break;
                case "cnvcAssistantController":
                    cmbList.ValueMember = "cnvcCommanderName";
                    cmbList.DisplayMember = "cnvcCommanderName";
                    CommandInforBF assistantControllerBF = new CommandInforBF();
                    rvSF = assistantControllerBF.GetCommanderByTypeAndStation("副控", m_stationBM.ThreeCode);
                    break;
                case "cnvcOutFieldController":
                    cmbList.ValueMember = "cnvcCommanderName";
                    cmbList.DisplayMember = "cnvcCommanderName";
                    CommandInforBF outFieldControllerBF = new CommandInforBF();
                    rvSF = outFieldControllerBF.GetCommanderByTypeAndStation("外场", m_stationBM.ThreeCode);
                    break;
                case "cnvcOutArrangeCargoOperator":
                case "cnvcOutRecheckLoadSheetOperator":
                case "cnvcOutConfirmLoadSheetOperator":
                case "cnvcOutCheckManifestOperator":
                case "cnvcOutRecheckManifestOperator":
                case "cnvcOutUploadManifestOperator":
                case "cnvcOutTransportManifestOperator":
                case "cnvcBalanceController":
                    cmbList.ValueMember = "cnvcCommanderName";
                    cmbList.DisplayMember = "cnvcCommanderName";
                    CommandInforBF balanceControllerBF = new CommandInforBF();
                    rvSF = balanceControllerBF.GetCommanderByTypeAndStation("配载员", m_stationBM.ThreeCode);
                    break;
                case "cnvcOutOverStationType":
                    #region 初始化 过站类型表
                    DataTable dataTableOutOverStationType = new DataTable();
                    dataTableOutOverStationType.Columns.Add("cnvcOutOverStationType", typeof(string));

                    DataRow dataRowOutOverStationType = dataTableOutOverStationType.NewRow();
                    dataRowOutOverStationType["cnvcOutOverStationType"] = "始发";
                    dataTableOutOverStationType.Rows.Add(dataRowOutOverStationType);

                    dataRowOutOverStationType = dataTableOutOverStationType.NewRow();
                    dataRowOutOverStationType["cnvcOutOverStationType"] = "过站";
                    dataTableOutOverStationType.Rows.Add(dataRowOutOverStationType);

                    dataRowOutOverStationType = dataTableOutOverStationType.NewRow();
                    dataRowOutOverStationType["cnvcOutOverStationType"] = "快速过站";
                    dataTableOutOverStationType.Rows.Add(dataRowOutOverStationType);

                    dataRowOutOverStationType = dataTableOutOverStationType.NewRow();
                    dataRowOutOverStationType["cnvcOutOverStationType"] = "航后";
                    dataTableOutOverStationType.Rows.Add(dataRowOutOverStationType);
                    #endregion 初始化 过站类型表
                    cmbList.ValueMember = "cnvcOutOverStationType";
                    cmbList.DisplayMember = "cnvcOutOverStationType";
                    rvSF = new ReturnValueSF();
                    rvSF.Result = 1;
                    rvSF.Dt = dataTableOutOverStationType;
                    break;  
            }

            if (rvSF.Result > 0)
            {
                cmbList.DataSource = rvSF.Dt;
                cmbList.Text = m_maintenGuaranteeInforBM.OldContent;
                #region added by LinYong in 20160617
                switch (MMaintenGuaranteeInforBM.FieldName) 
                {
                    case "cnvcOutArrangeCargoOperator":
                    case "cnvcOutRecheckLoadSheetOperator":
                    case "cnvcOutConfirmLoadSheetOperator":
                    case "cnvcOutCheckManifestOperator":
                    case "cnvcOutRecheckManifestOperator":
                    case "cnvcOutUploadManifestOperator":
                    case "cnvcOutTransportManifestOperator":
                        cmbList.Text = m_accountBM.UserName.Trim();
                        break;
                }
                #endregion added by LinYong in 20160617
                this.Text = m_maintenGuaranteeInforBM.ColumnCaption + "(" + m_maintenGuaranteeInforBM.FLTID + ")";
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            m_maintenGuaranteeInforBM.NewContent = cmbList.SelectedValue.ToString();
            m_maintenGuaranteeInforBM.NewText = cmbList.Text.ToString();
            m_changeRecordBM.ChangeNewContent = cmbList.Text.ToString();
            if (rvSF.Result > 0)
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            m_changeRecordBM.FOCOperatingTime = "";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(m_maintenGuaranteeInforBM);
            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}