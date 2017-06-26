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

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmMaintenMutiLineText : Form
    {
        private MaintenGuaranteeInforBM m_maintenGuaranteeInforBM;
        private ChangeRecordBM m_changeRecordBM;

        public fmMaintenMutiLineText(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();
            this.m_maintenGuaranteeInforBM = maintenGuaranteeInforBM;
            this.m_changeRecordBM = changeRecordBM;
        }

        public MaintenGuaranteeInforBM MMaintenGuaranteeInforBM
        {
            get { return m_maintenGuaranteeInforBM; }
        }

        private void fmMaintenMutiLineText_Load(object sender, EventArgs e)
        {
            txtNewContent.MaxLength = m_maintenGuaranteeInforBM.FieldLength;
            txtNewContent.Text = m_maintenGuaranteeInforBM.OldContent;
            this.Text = m_maintenGuaranteeInforBM.ColumnCaption + "(" + m_maintenGuaranteeInforBM.FLTID + ")";
            txtNewContent.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            m_maintenGuaranteeInforBM.NewContent = txtNewContent.Text.Trim();
            m_changeRecordBM.ChangeNewContent = txtNewContent.Text.Trim();
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