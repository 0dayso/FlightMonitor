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
    public partial class fmMaintenTOFF : Form
    {
        private MaintenGuaranteeInforBM m_maintenGuaranteeInforBM;
        private ChangeLegsBM m_changeLegsBM;
        private ChangeRecordBM m_changeRecordBM = new ChangeRecordBM();

        private string m_strNewContent;
            
        public fmMaintenTOFF(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeLegsBM changeLegsBM, ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();
            this.m_maintenGuaranteeInforBM = maintenGuaranteeInforBM;
            this.m_changeLegsBM = changeLegsBM;
            this.m_changeRecordBM = changeRecordBM;
        }

        public string NewContent
        {
            get { return m_strNewContent; }
        }

        private void rdETD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdETD.Checked)
            {
                txtETD.ReadOnly = false;
                btnETDNow.Enabled = true;
                txtETD.Focus();
            }
            else
            {
                txtETD.ReadOnly = true;
                btnETDNow.Enabled = false;
            }
        }

        private void btnETDNow_Click(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();
            if (rvSF.Result > 0)
            {
                txtETD.Text = DateTime.Parse(rvSF.Message).ToString("HHmm");
            }
            txtETD.SelectAll();
        }

        private void rdATD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdATD.Checked)
            {
                txtATD.ReadOnly = false;
                btnATDNow.Enabled = true;
                txtATD.Focus();
            }
            else
            {
                txtATD.ReadOnly = true;
                btnATDNow.Enabled = false;
            }
        }

        private void btnATDNow_Click(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();
            if (rvSF.Result > 0)
            {
                txtATD.Text = DateTime.Parse(rvSF.Message).ToString("HHmm");
            }
            txtATD.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rdETD.Checked)
            {
                if (txtETD.Text.Length > 0 && txtETD.Text.Length != 4)
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtETD.Text.Length == 4)
                {
                    string strTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtETD.Text.Substring(0, 2) + ":" + txtETD.Text.Substring(2, 2) + " :00";
                    try
                    {
                        DateTime.Parse(strTime);
                    }
                    catch
                    {
                        this.DialogResult = DialogResult.Cancel;
                        MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                m_strNewContent = txtETD.Text;

                //获取服务器时间
                ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
                ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

                //更新时间
                string strETD = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtETD.Text.Substring(0, 2) + ":" + txtETD.Text.Substring(2, 2) + ":00";

                m_maintenGuaranteeInforBM.FieldName = "cncETD";
                m_maintenGuaranteeInforBM.NewContent = strETD;
                m_changeRecordBM.ChangeNewContent = txtETD.Text.Trim();
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

                rvSF = guaranteeInforBF.UpdateLegsInfor(m_maintenGuaranteeInforBM);
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


            }
            else if (rdATD.Checked)
            {
                if (txtATD.Text.Length > 0 && txtATD.Text.Length != 4)
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtATD.Text.Length == 4)
                {
                    string strTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtATD.Text.Substring(0, 2) + ":" + txtATD.Text.Substring(2, 2) + " :00";
                    try
                    {
                        DateTime.Parse(strTime);
                    }
                    catch
                    {
                        this.DialogResult = DialogResult.Cancel;
                        MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                m_strNewContent = txtATD.Text;

                //获取服务器时间
                ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
                ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

                //更新时间
                string strATD = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtATD.Text.Substring(0, 2) + ":" + txtATD.Text.Substring(2, 2) + ":00";

                m_maintenGuaranteeInforBM.FieldName = "cncTOFF";
                m_maintenGuaranteeInforBM.NewContent = strATD;
                m_changeRecordBM.ChangeNewContent = txtATD.Text.Trim();
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

                rvSF = guaranteeInforBF.UpdateLegsInfor(m_maintenGuaranteeInforBM);
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

                //更新状态
                m_maintenGuaranteeInforBM.FieldName = "cncSTATUS";
                m_maintenGuaranteeInforBM.NewContent = "DEP";

                if (m_changeLegsBM.STATUS == "SCH")
                {
                    m_changeRecordBM.ChangeOldContent = "计划";
                }
                else if (m_changeLegsBM.STATUS == "DEL")
                {
                    m_changeRecordBM.ChangeOldContent = "延误";
                }
                else if (m_changeLegsBM.STATUS == "DEP")
                {
                    m_changeRecordBM.ChangeOldContent = "出发";
                }
                m_changeRecordBM.ChangeNewContent = "到达";

                rvSF = guaranteeInforBF.UpdateLegsInfor(m_maintenGuaranteeInforBM);
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

            }

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmMaintenTOFF_Activated(object sender, EventArgs e)
        {

        }

        private void fmMaintenTOFF_Load(object sender, EventArgs e)
        {
            txtETD.ReadOnly = true;
            btnETDNow.Enabled = false;

            rdATD.Checked = true;

            txtATD.Text = DateTime.Parse(m_changeLegsBM.TOFF).ToString("HHmm");

            txtATD.SelectAll();
            txtETD.SelectAll();
        }
    }
}