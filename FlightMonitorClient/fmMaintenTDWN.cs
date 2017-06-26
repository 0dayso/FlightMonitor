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
    public partial class fmMaintenTDWN : Form
    {
        private MaintenGuaranteeInforBM m_maintenGuaranteeInforBM;
        private ChangeLegsBM m_changeLegsBM;
        private ChangeRecordBM m_changeRecordBM = new ChangeRecordBM();

        private string m_strNewContent;

        public fmMaintenTDWN(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeLegsBM changeLegsBM, ChangeRecordBM changeRecordBM)
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

        private void rdETA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdETA.Checked)
            {
                txtETA.ReadOnly = false;
                btnETANow.Enabled = true;
                txtETA.Focus();
            }
            else
            {
                txtETA.ReadOnly = true;
                btnETANow.Enabled = false;
            }
        }

        private void btnETANow_Click(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();
            if (rvSF.Result > 0)
            {
                txtETA.Text = DateTime.Parse(rvSF.Message).ToString("HHmm");
            }
            txtETA.SelectAll();
        }

        private void rdATA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdATA.Checked)
            {
                txtATA.ReadOnly = false;
                btnATANOW.Enabled = true;
                txtATA.Focus();
            }
            else
            {
                txtATA.ReadOnly = true;
                btnATANOW.Enabled = false;
            }
        }

        private void btnATANOW_Click(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();
            if (rvSF.Result > 0)
            {
                txtATA.Text = DateTime.Parse(rvSF.Message).ToString("HHmm");
            }
            txtATA.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rdETA.Checked)
            {
                if (txtETA.Text.Length > 0 && txtETA.Text.Length != 4)
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtETA.Text.Length == 4)
                {
                    string strTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtETA.Text.Substring(0, 2) + ":" + txtETA.Text.Substring(2, 2) + " :00";
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

                m_strNewContent = txtETA.Text;

                //获取服务器时间
                ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
                ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

                //更新时间
                string strETA = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtETA.Text.Substring(0, 2) + ":" + txtETA.Text.Substring(2, 2) + ":00";

                m_maintenGuaranteeInforBM.FieldName = "cncETA";
                m_maintenGuaranteeInforBM.NewContent = strETA;
                m_changeRecordBM.ChangeNewContent = txtETA.Text.Trim();
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
                m_changeRecordBM.ChangeNewContent = "出发";

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
            else if (rdATA.Checked)
            {
                if (txtATA.Text.Length > 0 && txtATA.Text.Length != 4)
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtATA.Text.Length == 4)
                {
                    string strTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtATA.Text.Substring(0, 2) + ":" + txtATA.Text.Substring(2, 2) + " :00";
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

                m_strNewContent = txtATA.Text;

                //获取服务器时间
                ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
                ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

                //更新时间
                string strATA = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtATA.Text.Substring(0, 2) + ":" + txtATA.Text.Substring(2, 2) + ":00";

                m_maintenGuaranteeInforBM.FieldName = "cncTDWN";
                m_maintenGuaranteeInforBM.NewContent = strATA;
                m_changeRecordBM.ChangeNewContent = txtATA.Text.Trim();
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
                m_maintenGuaranteeInforBM.NewContent = "ATA";

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

        private void fmMaintenTDWN_Activated(object sender, EventArgs e)
        {
            if (m_changeLegsBM.STATUS == "SCH" || m_changeLegsBM.STATUS == "DEL")
            {
                rdETA.Checked = true;
                txtETA.ReadOnly = false;
                txtETA.Text = DateTime.Parse(m_changeLegsBM.ETA).ToString("HHmm");
                btnETANow.Enabled = true;

                rdATA.Checked = false;
                txtATA.ReadOnly = true;
                btnATANOW.Enabled = false;
            }
            else if (m_changeLegsBM.STATUS == "DEP")
            {
                rdETA.Checked = false;
                txtETA.ReadOnly = true;
                txtETA.Text = DateTime.Parse(m_changeLegsBM.ETA).ToString("HHmm");
                btnETANow.Enabled = false;

                rdATA.Checked = true;
                txtATA.ReadOnly = false;
                btnATANOW.Enabled = true;
            }
            else if (m_changeLegsBM.STATUS == "ATA")
            {
                rdETA.Checked = false;
                txtETA.ReadOnly = true;
                txtETA.Text = DateTime.Parse(m_changeLegsBM.ETA).ToString("HHmm");
                btnETANow.Enabled = false;

                rdATA.Checked = false;
                txtATA.ReadOnly = true;
                txtATA.Text = DateTime.Parse(m_changeLegsBM.TDWN).ToString("HHmm");
                btnATANOW.Enabled = false;
            }
			
        }

        private void fmMaintenTDWN_Load(object sender, EventArgs e)
        {
            txtATA.SelectAll();
            txtETA.SelectAll();
        }
    }
}