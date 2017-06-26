using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmDispatchSheet : Form
    {
        private string m_strFlightDate;
        private string m_strFlightNo;
        private string m_strDEPSTN;
        private string m_strARRSTN;

        private string m_strDATOP;      //modified by LinYong in 2013.08.02


        /// <summary>
        /// 构造函数
        /// </summary>
        public fmDispatchSheet(string strFlightDate, string strFlightNo, string strDEPSTN, string strARRSTN)
        {
            InitializeComponent();
            this.m_strFlightDate = strFlightDate;
            this.m_strFlightNo = strFlightNo;
            this.m_strDEPSTN = strDEPSTN;
            this.m_strARRSTN = strARRSTN;

        }

        /// <summary>
        /// 窗体加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmDispatchSheet_Load(object sender, EventArgs e)
        {
            ComputerPlanBF computerPlanBF = new ComputerPlanBF();
            #region modified by LinYong in 2013.08.02
            //ReturnValueSF rvSF = computerPlanBF.GetComputerFlightPlan(m_strFlightDate, m_strFlightNo, m_strDEPSTN, m_strARRSTN);
            ReturnValueSF rvSF = computerPlanBF.GetComputerFlightPlan(m_strFlightDate, m_strFlightNo, m_strDEPSTN, m_strARRSTN, m_strDATOP);
            #endregion modified by LinYong in 2013.08.02

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(rvSF.Dt.Rows.Count > 0)
            {
                txtReport.Text = rvSF.Dt.Rows[0]["cntReport"].ToString().Replace("<BR>", "\r\n").Replace("&nbsp;", " ");
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region modified by LinYong in 2013.08.02
        /// <summary>
        /// 构造函数
        /// </summary>
        public fmDispatchSheet(string strFlightDate, string strFlightNo, string strDEPSTN, string strARRSTN, string strDATOP)
        {
            InitializeComponent();
            this.m_strFlightDate = strFlightDate;
            this.m_strFlightNo = strFlightNo;
            this.m_strDEPSTN = strDEPSTN;
            this.m_strARRSTN = strARRSTN;

            this.m_strDATOP = strDATOP;

        }
        #endregion modified by LinYong in 2013.08.02


    }
}