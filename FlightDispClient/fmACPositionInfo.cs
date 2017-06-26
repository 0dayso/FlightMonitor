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
    public partial class fmACPositionInfo : Form
    {
        private ChangeLegsBM _changeLegsBM;
        public fmACPositionInfo(ChangeLegsBM changeLegsBM)
        {
            InitializeComponent();

            _changeLegsBM = changeLegsBM;
        }

        private void fmACPositionInfo_Load(object sender, EventArgs e)
        {
            ACPositionInfoBF acPositionInfoBF = new ACPositionInfoBF();
            DataTable dt = acPositionInfoBF.GetACPositionInfo(_changeLegsBM);
            dgvACPositionInf.DataSource = dt;
            dgvACPositionInf.DataMember = dt.TableName;
        }
    }
}