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
    public partial class fmComputerPlan : Form
    {
        private string m_strFlightDate;
        private string m_strFlightNo;
        private string m_strDEPSTN;
        private string m_strARRSTN;

        private string m_strDATOP;     //modified by LinYong in 2013.07.31


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strFlightDate">航班日期</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">起飞机场三字码</param>
        /// <param name="strARRSTN">到达机场三字码</param>
        public fmComputerPlan(string strFlightDate, string strFlightNo, string strDEPSTN, string strARRSTN)
        {
            InitializeComponent();
            this.m_strFlightDate = strFlightDate;
            this.m_strFlightNo = strFlightNo;
            this.m_strDEPSTN = strDEPSTN;
            this.m_strARRSTN = strARRSTN;
        }

        #region 载入窗体时打开运行网页面
        private void fmComputerPlan_Load(object sender, EventArgs e)
        {
            AirportInforBF airportInforBF = new AirportInforBF();
            ComputerPlanBF computerPlanBF = new ComputerPlanBF();

            //查询飞行计划内容
            #region modified by LinYong in 2013.08.02
            //ReturnValueSF rvSF = computerPlanBF.GetComputerFlightPlan(m_strFlightDate, m_strFlightNo, m_strDEPSTN, m_strARRSTN);
            ReturnValueSF rvSF = computerPlanBF.GetComputerFlightPlan(m_strFlightDate, m_strFlightNo, m_strDEPSTN, m_strARRSTN, m_strDATOP);     //modified by LinYong in 2013.08.02
            #endregion modified by LinYong in 2013.08.02

            #region 提取备降场
            //如果没有这个计划
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //如果找到计划
            else if (rvSF.Dt.Rows.Count > 0)
            {
                //飞行计划报告
                string strReport = rvSF.Dt.Rows[0]["cntReport"].ToString().Replace("<BR>", "\r\n").Replace("&nbsp;", " ");

                if (strReport != "")
                {
                    int iBegin = strReport.IndexOf("DEST");
                    iBegin = strReport.IndexOf("ALTN:", iBegin) + 5;
                    int iEnd = strReport.IndexOf("\r", iBegin) - 1;

                    //提取备降场
                    string[] strALTN = strReport.Substring(iBegin, iEnd - iBegin + 1).Split(' ');

                    //将所有备降场的三字码转成四字码，并填入备降场文本框中
                    for (int iLoop = 0; iLoop < strALTN.Length; iLoop++)
                    {
                        //如果备降场为“NIL”，进入下一备降场
                        if (strALTN[iLoop] == "NIL")
                        {
                            continue;
                        }

                        //根据三字码查询机场信息
                        rvSF = airportInforBF.GetAirportInforByThreeCode(strALTN[iLoop]);

                        //如果出现异常
                        if (rvSF.Result < 0)
                        {
                            MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        else
                        {
                            //如果该三字码没有对应的机场
                            if (rvSF.Dt.Rows.Count == 0)
                            {
                                MessageBox.Show("不存在三字码" + strALTN[iLoop] + "，请添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                //将四字码填入文本框中
                                txtBackUp.Text += rvSF.Dt.Rows[0]["cncAirportFourCode"].ToString() + " ";
                            }
                        }                       
                    }

                    txtBackUp.Text = txtBackUp.Text.Trim();
                }
            }
            #endregion

            //生成运行网页面地址
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Flightplan/PlanResultNew.aspx";
            strURL += "?strDate=" + m_strFlightDate;
            #region modified by LinYong in 2013.08.02
            //strURL += "&strtoffDate=" + m_strFlightDate;
            strURL += "&strtoffDate=" + m_strDATOP;   
            #endregion modified by LinYong in 2013.08.02
            strURL += "&strPlaneNo=";
            strURL += "&strFlightNumber=" + m_strFlightNo;
            strURL += "&strCourse=" + m_strDEPSTN + "-" + m_strARRSTN;
            strURL += "&strflightplan=1&strnotam=1&strmet=1&stairplaneDD=1&strroute=1&strimportant=1&strSpecialPrag=1";
            strURL += "&strAirport=&strBackUp=";            
            strURL += txtBackUp.Text;
            wbPlan.Navigate(strURL);
        }
        #endregion

        #region 重新查询飞行计划
        /// <summary>
        /// 重新查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //生成运行网页面地址
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Flightplan/PlanResultNew.aspx?strDate=";
            #region modified by LinYong in 2013.08.02
            //strURL += m_strFlightDate + "&strtoffDate=" + m_strFlightDate;
            strURL += m_strFlightDate + "&strtoffDate=" + m_strDATOP;
            #endregion modified by LinYong in 2013.08.02
            strURL += "&strPlaneNo=";
            strURL += "&strFlightNumber=" + m_strFlightNo + "&strCourse=" + m_strDEPSTN + "-" + m_strARRSTN;

            if (cbFlightPlan.Checked)
            {
                strURL += "&strflightplan=1";
            }
            else
            {
                strURL += "&strflightplan=";
            }

            if (cbNotam.Checked)
            {
                strURL += "&strnotam=1";
            }
            else
            {
                strURL += "&strnotam=";
            }

            if (cbMet.Checked)
            {
                strURL += "&strmet=1";
            }
            else
            {
                strURL += "&strmet=";
            }

            if (cbAirplaneDD.Checked)
            {
                strURL += "&stairplaneDD=1";
            }
            else
            {
                strURL += "&stairplaneDD=";
            }

            if (cbRoute.Checked)
            {
                strURL += "&strroute=1";
            }
            else
            {
                strURL += "&strroute=";
            }

            if (cbImportant.Checked)
            {
                strURL += "&strimportant=1";
            }
            else
            {
                strURL += "&strimportant=";
            }

            if (cbSpecialPrag.Checked)
            {
                strURL += "&strSpecialPrag=1";
            }
            else
            {
                strURL += "&strSpecialPrag=";
            }

            strURL += "&strAirport=&strBackUp=";
           
            strURL += txtBackUp.Text.Trim();

            wbPlan.Navigate(strURL);
        }
        #endregion

        private void btnSetUp_Click(object sender, EventArgs e)
        {
            wbPlan.ShowPageSetupDialog();           
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            wbPlan.ShowPrintPreviewDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            wbPlan.ShowPrintDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region modified by LinYong in 2013.07.31
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strFlightDate">航班日期</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strDEPSTN">起飞机场三字码</param>
        /// <param name="strARRSTN">到达机场三字码</param>
        /// <param name="strDATOP">航班日期（UTC）</param>
        public fmComputerPlan(string strFlightDate, string strFlightNo, string strDEPSTN, string strARRSTN, string strDATOP)
        {
            InitializeComponent();
            this.m_strFlightDate = strFlightDate;
            this.m_strFlightNo = strFlightNo;
            this.m_strDEPSTN = strDEPSTN;
            this.m_strARRSTN = strARRSTN;

            this.m_strDATOP = strDATOP;
        }
        #endregion modified by LinYong in 2013.07.31
    }
}