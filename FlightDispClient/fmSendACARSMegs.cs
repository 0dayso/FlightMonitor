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
    public partial class fmSendACARSMegs : Form
    {
        private ChangeLegsBM _changeLegsBM;
        public fmSendACARSMegs(ChangeLegsBM changeLegsBM)
        {
            InitializeComponent();

            _changeLegsBM = changeLegsBM;
        }

        private void fmSendACARSMegs_Load(object sender, EventArgs e)
        {
            txtLongReg.Text = _changeLegsBM.LONG_REG;
            txtFlightNo.Text = _changeLegsBM.FlightNo;
            cbxMegType.SelectedIndex = 0;
        }
    }
}