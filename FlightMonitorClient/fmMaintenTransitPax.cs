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
    public partial class fmMaintenTransitPax : Form
    {
        private ChangeLegsBM m_changeLegsBM;
        private ChangeRecordBM m_changeRecordBM = new ChangeRecordBM();

        private TrasitPaxBM m_trasitPaxBM = new TrasitPaxBM();
        private TrasitPaxBM m_oldTrasitPaxBM  = new TrasitPaxBM();

        private string m_strNewContent;

        public fmMaintenTransitPax(ChangeLegsBM changeLegsBM, ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();
            this.m_changeLegsBM = changeLegsBM;
            this.m_changeRecordBM = changeRecordBM;
        }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewContent
        {
            get { return m_strNewContent; }
        }


        private void fmMaintenTransitPax_Load(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ReturnValueSF rvSF = guaranteeInforBF.GetTrasitPaxList(m_changeLegsBM);
            if (rvSF.Result > 0)
            {
                if (rvSF.Dt.Rows.Count > 0)
                {
                    m_oldTrasitPaxBM = new TrasitPaxBM(rvSF.Dt.Rows[0]);
                    m_trasitPaxBM = m_oldTrasitPaxBM;
                }
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            if (m_oldTrasitPaxBM.IsAutoSaveTransitPax == 0 || m_oldTrasitPaxBM.TransitPax.Trim() == "")
            {
                m_trasitPaxBM.TransitPax = GetTransitPax(m_changeLegsBM);
            }

            txtPaxNameList.Text = m_trasitPaxBM.TransitPax;
        }

        private void btnReconnect_Click(object sender, EventArgs e)
        {
            PaxService.PaxService objPaxService = new PaxService.PaxService();
            if (!objPaxService.PaxCheckConnect())
            {
                btnReconnect.Enabled = true;
                MessageBox.Show("请重新连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                m_trasitPaxBM.TransitPax = GetTransitPax(m_changeLegsBM);

                txtPaxNameList.Text = m_trasitPaxBM.TransitPax;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();


            if (m_changeLegsBM.STATUS == "DEP" || m_changeLegsBM.STATUS == "ATA")
            {
                m_trasitPaxBM.IsAutoSaveTransitPax = 1;
            }

            int iStart = txtPaxNameList.Text.IndexOf("\n");
            iStart = txtPaxNameList.Text.IndexOf("\n", iStart);
            if (iStart < 0)
            {
                iStart = 0;
            }

            iStart = txtPaxNameList.Text.IndexOf("1.", iStart);

            if (iStart > 0)
            {
                m_changeRecordBM.ChangeNewContent = "有";
                m_trasitPaxBM.TransitPaxTag = "有";
            }
            else
            {
                m_changeRecordBM.ChangeNewContent = "";
                m_trasitPaxBM.TransitPaxTag = "";
            }

            ReturnValueSF rvSF = guaranteeInforBF.UpdateTrasitPax(m_trasitPaxBM);

            if (rvSF.Result > 0)
            {
                //获取服务器时间
                ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
                rvSF = serverDateTimeBF.GetServerDateTime();

                if (rvSF.Result > 0)
                {
                    m_changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    m_changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                m_changeRecordBM.FOCOperatingTime = "";

                m_strNewContent = m_trasitPaxBM.TransitPaxTag;
                ChangeRecordBF changeRecordBF = new ChangeRecordBF();
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
            }

            m_strNewContent = m_changeRecordBM.ChangeNewContent;

            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 通过服务获取中转旅客信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        private string GetTransitPax(ChangeLegsBM changeLegsBM)
        {
            PaxService.PaxService objPaxService = new PaxService.PaxService();
            string strFlightNo = changeLegsBM.CKIFlightNo.Replace(" ", "").ToUpper();


            string strCommnadText = "PD:";
            strCommnadText += strFlightNo + "/";
            strCommnadText += DateTime.Parse(changeLegsBM.CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " * ";
            strCommnadText += changeLegsBM.DEPSTN + changeLegsBM.ARRSTN + ",o";


            //string strResult = objPaxService.PaxCheckNum(strCommnadText);
            string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");
            strResult = strResult.Replace("\n", "\r\n");


            return strResult;
        }
    }
}