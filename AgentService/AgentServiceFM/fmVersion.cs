using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.AgentServiceBM;

namespace AirSoft.FlightMonitor.AgentServiceFM
{
    public partial class fmVersion : Form
    {
        public fmVersion()
        {
            InitializeComponent();
        }

        private void fmVersion_Load(object sender, EventArgs e)
        {
            label6.Text = SysMsgBM.AgentLevel;
            label7.Text = SysMsgBM.GetAgentIP;
            label8.Text = SysMsgBM.GetAgentPort;
            label9.Text = SysMsgBM.AgentIP;
            label10.Text = SysMsgBM.AgentPort;
            label11.Text = SysMsgBM.Compress;
            label12.Text = SysMsgBM.RefreshInterval;

        }
    }
}