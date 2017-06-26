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

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmSetPosition : Form
    {
        private PositionNameBM m_positionNameBM;
        /// <summary>
        /// ¹¹Ôìº¯Êý
        /// </summary>
        public fmSetPosition(PositionNameBM positionNameBM)
        {
            InitializeComponent();

            this.m_positionNameBM = positionNameBM;
        }

        public PositionNameBM MPositionNameBM
        {
            get { return m_positionNameBM; }
        }

        private void fmSetPosition_Load(object sender, EventArgs e)
        {
            PositionNameBF positionNameBF = new PositionNameBF();
            ReturnValueSF rvSF = positionNameBF.GetAllPositionName();

            if (rvSF.Result > 0)
            {
                cmbPosition.DisplayMember = "cnvcPositionName";
                cmbPosition.ValueMember = "cniPositionID";
                cmbPosition.DataSource = rvSF.Dt;
            }

            cmbPosition.Text = m_positionNameBM.PositionName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dtPositionName = (DataTable) cmbPosition.DataSource;
            m_positionNameBM = new PositionNameBM(dtPositionName.Rows[cmbPosition.SelectedIndex]);
            ConfigSettings.WriteSetting("PositionID", m_positionNameBM.PositionID.ToString());           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}