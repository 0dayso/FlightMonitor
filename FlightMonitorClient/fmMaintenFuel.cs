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
    public partial class fmMaintenFuel : Form
    {
        private MaintenGuaranteeInforBM m_maintenGuaranteeInforBM;
        private ChangeLegsBM m_changeLegsBM;
        private ChangeRecordBM m_changeRecordBM = new ChangeRecordBM();
        private string m_strNewContent;

        public fmMaintenFuel(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeLegsBM changeLegsBM, ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();

            this.m_changeLegsBM = changeLegsBM;
            this.m_changeRecordBM = changeRecordBM;
            this.m_maintenGuaranteeInforBM = maintenGuaranteeInforBM;
        }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewContent
        {
            get { return m_strNewContent; }
        }


        private void fmMaintenFuel_Load(object sender, EventArgs e)
        {
            ComputerPlanBF computerPlanBF = new ComputerPlanBF();
            ReturnValueSF rvSF = computerPlanBF.GetComputerFlightPlan(m_changeLegsBM.FlightDate, m_changeLegsBM.FLTID, m_changeLegsBM.DEPSTN, m_changeLegsBM.ARRSTN);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (rvSF.Dt.Rows.Count > 0)
            {
                txtReport.Text = rvSF.Dt.Rows[0]["cntReport"].ToString().Replace("<BR>", "\r\n").Replace("&nbsp;", " ");


                //总油量
                int iBegin = txtReport.Text.IndexOf("TTL FUEL:") + 9;
                int iEnd = txtReport.Text.IndexOf("KGS", iBegin) - 1;

                txtTotalFuel.Text = txtReport.Text.Substring(iBegin, iEnd - iBegin + 1);

                //航程耗油
                iBegin = txtReport.Text.IndexOf("TRIP FUEL:") + 10;
                iEnd = txtReport.Text.IndexOf("KGS", iBegin) - 1;
                txtTripFuel.Text = txtReport.Text.Substring(iBegin, iEnd - iBegin + 1);
            }

            //滑行油
            AirportInforBF airportInforBF = new AirportInforBF();
            rvSF = airportInforBF.GetTaxiOil(m_changeLegsBM.ACTYP, m_changeLegsBM.DEPSTN);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (rvSF.Dt.Rows.Count > 0)
            {
                txtTaxiFuel.Text = rvSF.Dt.Rows[0]["cniTaxiFuelKG"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validator.IsNumber(txtTotalFuel.Text, "总油量必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtTripFuel.Text, "航程耗油必须为整数！"))
            {
                return;
            }

            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSFTime = serverDateTimeBF.GetServerDateTime();


            m_maintenGuaranteeInforBM.FieldName = "cniTotalFuelWeight";
            m_maintenGuaranteeInforBM.NewContent = txtTotalFuel.Text;
            m_changeRecordBM.ChangeNewContent = txtTotalFuel.Text;

            if (rvSFTime.Result > 0)
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSFTime.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            m_changeRecordBM.FOCOperatingTime = "";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            ReturnValueSF rvSF = guaranteeInforBF.UpdateGuaranteeInfor(m_maintenGuaranteeInforBM);

            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



            m_maintenGuaranteeInforBM.FieldName = "cniTripFuelWeight";
            m_maintenGuaranteeInforBM.NewContent = txtTripFuel.Text;
            m_changeRecordBM.ChangeNewContent = txtTripFuel.Text;

            if (rvSFTime.Result > 0)
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSFTime.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            m_changeRecordBM.FOCOperatingTime = "";


            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(m_maintenGuaranteeInforBM);

            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_maintenGuaranteeInforBM.FieldName = "cniTaxiFuelWeight";
            m_maintenGuaranteeInforBM.NewContent = txtTaxiFuel.Text;
            m_changeRecordBM.ChangeNewContent = txtTaxiFuel.Text;

            if (rvSFTime.Result > 0)
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSFTime.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            m_changeRecordBM.FOCOperatingTime = "";


            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(m_maintenGuaranteeInforBM);

            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_strNewContent = txtTotalFuel.Text;

            this.Close();
                       
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}