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
    public partial class fmMessage : Form
    {
        public fmMessage()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_1;
            textBox2.Text = SysMsgBM.TraceInfo_MsgOfShowInDataGridView_2_2;
            textBox3.Text = SysMsgBM.TraceInfo_timer1_Tick_1;
            textBox4.Text = SysMsgBM.TraceInfo_GetLastGuaranteeChangeRecords_1;
        }

        private void fmMessage_Load(object sender, EventArgs e)
        {

        }

        private void fmMessage_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.Text = "";
            else
                this.Text = "œ˚œ¢ ”Õº";
        }
    }
}