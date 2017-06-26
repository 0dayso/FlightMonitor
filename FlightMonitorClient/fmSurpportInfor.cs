using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmSurpportInfor : Form
    {
        private string m_strURL;

        public fmSurpportInfor(string strURL)
        {
            InitializeComponent();
            this.m_strURL = strURL;
        }

        private void fmSurpportInfor_Load(object sender, EventArgs e)
        {
            wbSurpportInfor.Navigate(m_strURL);
        }

        private void btnSetUp_Click(object sender, EventArgs e)
        {
            wbSurpportInfor.ShowPageSetupDialog();
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            wbSurpportInfor.ShowPrintPreviewDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            wbSurpportInfor.ShowPrintDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}