using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Fuel
{
    public partial class fmStatisticFlyTimeRange : Form
    {
        private string strStartDate;
        private string strEndDate;

        public string StartDate
        {
            get
            {
                return strStartDate;
            }
        }

        public string EndDate
        {
            get
            {
                return strEndDate;
            }
        }

        public fmStatisticFlyTimeRange()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            strStartDate = dtStartFlightDate.Value.ToString("yyyy-MM-dd");
            strEndDate = dtEndFlightDate.Value.ToString("yyyy-MM-dd");

           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}