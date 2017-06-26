using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AirSoft.FlightMonitor.AgentServiceFM
{
    public partial class fmProcAnalysis : Form
    {
        fmMDIMain m_fmMDIMain = null;

        public DataGridView DataGridView1
        {
            get { return dataGridView1; }
        }

        
        public fmProcAnalysis()
        {
            InitializeComponent();
        }

        public fmProcAnalysis(fmMDIMain objFmMDIMain)
        {
            m_fmMDIMain = objFmMDIMain;
            
            InitializeComponent();
        }

        private void fmProcAnalysis_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = m_fmMDIMain.ProcAnalysis.DefaultView;
            for (int iIndex = 0; iIndex < dataGridView1.Columns.Count; iIndex++)
                dataGridView1.Columns[iIndex].HeaderText = (dataGridView1.DataSource as DataView).Table.Columns[iIndex].Caption;
        }

        private void fmProcAnalysis_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.Text = "";
            else
                this.Text = "Í³¼ÆÊÓÍ¼";
        }
    }
}