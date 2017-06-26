using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AirSoft.FlightMonitor.AgentServiceFM
{
    public partial class fmProcRecords : Form
    {
        fmMDIMain m_fmMDIMain = null;

        public DataGridView DataGridView1
        {
            get { return dataGridView1; }
        }


        public fmProcRecords()
        {
            InitializeComponent();
        }

        public fmProcRecords(fmMDIMain objFmMDIMain)
        {
            m_fmMDIMain = objFmMDIMain;

            InitializeComponent();
        }

        private void fmProcRecords_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = m_fmMDIMain.ProcRecords.DefaultView;
            for (int iIndex = 0; iIndex < dataGridView1.Columns.Count; iIndex++)
                dataGridView1.Columns[iIndex].HeaderText = (dataGridView1.DataSource as DataView).Table.Columns[iIndex].Caption;

        }

        private void fmProcRecords_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.Text = "";
            else
                this.Text = "Ã÷Ï¸ÊÓÍ¼";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strRowFilter = "";

            try
            {
                strRowFilter = (dataGridView1.DataSource as DataView).RowFilter;
                (dataGridView1.DataSource as DataView).RowFilter = textBox1.Text;
            }
            catch (Exception ex)
            {
                (dataGridView1.DataSource as DataView).RowFilter = strRowFilter;
                MessageBox.Show(ex.Message);
            }
        }
    }
}