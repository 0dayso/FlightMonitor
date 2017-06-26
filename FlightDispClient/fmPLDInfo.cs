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

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmPLDInfo : Form
    {
        private ChangeLegsBM m_changeLegsBM;
        
        private PayLoadInfoBM m_payLoadInfoBM;
        /// <summary>
        /// 航班业载信息
        /// </summary>
        public PayLoadInfoBM PayLoadInfo
        {
            get { return m_payLoadInfoBM; }
        }
        public fmPLDInfo(ChangeLegsBM changeLegsBM)
        {
            InitializeComponent();
            this.m_changeLegsBM = changeLegsBM;
        }

        private void fmPLDInfo_Load(object sender, EventArgs e)
        {
            lblFlightNo.Text = m_changeLegsBM.FlightNo;
            lblAircraft.Text = m_changeLegsBM.LONG_REG;
            m_payLoadInfoBM = new PayLoadInfoBM();
        }

        #region 保存业载信息
        private void btnStore_Click(object sender, EventArgs e)
        {
            m_payLoadInfoBM.Psgs = txtPsgs.Text;
            m_payLoadInfoBM.Bags = txtBags.Text;
            m_payLoadInfoBM.Cargo = txtCargo.Text;
            m_payLoadInfoBM.TotalPayLoad = txtTotalPLD.Text;
        }
        #endregion

        #region 提取业载信息
        private void btnGetPLD_Click(object sender, EventArgs e)
        {
            txtPsgs.Text = "100";
            txtBags.Text = "2000";
            txtCargo.Text = "3000";
            txtTotalPLD.Text = "12200";
        }
        #endregion
    }
}