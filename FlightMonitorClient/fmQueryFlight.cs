using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmQueryFlight : Form
    {
        private string _findContent = "";
        private bool _blnFind = false;

        public fmQueryFlight()
        {
            InitializeComponent();
        }

        public string FindContent
        {
            get
            {
                return _findContent;
            }
        }

        public bool Find
        {
            get
            {
                return _blnFind;
            }
        }

        private void fmQueryFlight_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtQueryContent.Text.Trim() == "")
            {
                MessageBox.Show("查找内容不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _findContent = txtQueryContent.Text.Trim();
            _blnFind = true;

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}