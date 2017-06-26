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
    public partial class fmMaitenSingleText : Form
    {
        private MaintenGuaranteeInforBM m_maintenGuaranteeInforBM;
        private ChangeRecordBM m_changeRecordBM;

        /// <summary>
        /// 构造函数  只变更进港或出港
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <param name="changeRecordBM"></param>
        public fmMaitenSingleText(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();
            this.m_maintenGuaranteeInforBM = maintenGuaranteeInforBM;
            this.m_changeRecordBM = changeRecordBM;
        }

        public MaintenGuaranteeInforBM MMaintenGuaranteeInforBM
        {
            get { return m_maintenGuaranteeInforBM; }
        }

        private void fmMaitenSingleText_Load(object sender, EventArgs e)
        {
            txtNewContent.MaxLength = m_maintenGuaranteeInforBM.FieldLength;
            txtNewContent.Text = m_maintenGuaranteeInforBM.OldContent;
            this.Text = m_maintenGuaranteeInforBM.ColumnCaption + "(" + m_maintenGuaranteeInforBM.FLTID + ")";
            txtNewContent.SelectAll();

            if (m_changeRecordBM.ChangeReasonCode == "cnvcOutGate")
            {
                btnSave.Enabled = false;
            }
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            //出港机位维护
            if (m_changeRecordBM.ChangeReasonCode == "cnvcOutGate")
            {
                //判断是否含有中文
                LinYong.PublicService.PublicService PublicServiceObj = new LinYong.PublicService.PublicService();
                if (PublicServiceObj.ContainChinese(txtNewContent.Text.Trim()))
                {
                    MessageBox.Show("停机位信息不能含有中文！", "提示");
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }

            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            m_maintenGuaranteeInforBM.NewContent = txtNewContent.Text.Trim();
            m_changeRecordBM.ChangeNewContent = txtNewContent.Text.Trim();
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

            //出港机位维护
            if(m_changeRecordBM.ChangeReasonCode == "cnvcOutGate")
            {
                string gate = "UNK";
                if (txtNewContent.Text.Trim() != "")
                {
                    gate = txtNewContent.Text.Trim();
                }
 
                string strOutGate = m_changeRecordBM.OldFLTID.Replace(" ", "");
                //strOutGate += "/" + DateTime.Parse(m_changeRecordBM.STD).ToUniversalTime().ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
                strOutGate += "/" + DateTime.Parse(m_changeRecordBM.OldDATOP).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
                strOutGate += "%%";

                strOutGate += m_changeRecordBM.OldDepSTN + DateTime.Parse(m_changeRecordBM.STD).ToUniversalTime().ToString("HHmm") + " ";
                strOutGate += m_changeRecordBM.OldArrSTN + DateTime.Parse(m_changeRecordBM.STA).ToUniversalTime().ToString("HHmm") + "%%";

                strOutGate += m_changeRecordBM.OldDepSTN + "/" + gate + " " + m_changeRecordBM.OldArrSTN + "/UNK%%";

                #region modified by LinYong in 20151119
                //FocService.FleetWatch objFocService = new FocService.FleetWatch();
                FocService.FocService objFocService = new FocService.FocService();
                #endregion modified by LinYong in 20151119
                objFocService.InsertGate(strOutGate);
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

        private void txtNewContent_TextChanged(object sender, EventArgs e)
        {
            if (m_changeRecordBM.ChangeReasonCode == "cnvcOutGate")
            {
                if (txtNewContent.Text.Trim() != m_maintenGuaranteeInforBM.OldContent.Trim())
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
        }
    }
}