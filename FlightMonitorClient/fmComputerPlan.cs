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
        /// ���캯��
        /// </summary>
        /// <param name="strFlightDate">��������</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">��ɻ���������</param>
        /// <param name="strARRSTN">�������������</param>
        public fmComputerPlan(string strFlightDate, string strFlightNo, string strDEPSTN, string strARRSTN)
        {
            InitializeComponent();
            this.m_strFlightDate = strFlightDate;
            this.m_strFlightNo = strFlightNo;
            this.m_strDEPSTN = strDEPSTN;
            this.m_strARRSTN = strARRSTN;
        }

        #region ���봰��ʱ��������ҳ��
        private void fmComputerPlan_Load(object sender, EventArgs e)
        {
            AirportInforBF airportInforBF = new AirportInforBF();
            ComputerPlanBF computerPlanBF = new ComputerPlanBF();

            //��ѯ���мƻ�����
            #region modified by LinYong in 2013.08.02
            //ReturnValueSF rvSF = computerPlanBF.GetComputerFlightPlan(m_strFlightDate, m_strFlightNo, m_strDEPSTN, m_strARRSTN);
            ReturnValueSF rvSF = computerPlanBF.GetComputerFlightPlan(m_strFlightDate, m_strFlightNo, m_strDEPSTN, m_strARRSTN, m_strDATOP);     //modified by LinYong in 2013.08.02
            #endregion modified by LinYong in 2013.08.02

            #region ��ȡ������
            //���û������ƻ�
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //����ҵ��ƻ�
            else if (rvSF.Dt.Rows.Count > 0)
            {
                //���мƻ�����
                string strReport = rvSF.Dt.Rows[0]["cntReport"].ToString().Replace("<BR>", "\r\n").Replace("&nbsp;", " ");

                if (strReport != "")
                {
                    int iBegin = strReport.IndexOf("DEST");
                    iBegin = strReport.IndexOf("ALTN:", iBegin) + 5;
                    int iEnd = strReport.IndexOf("\r", iBegin) - 1;

                    //��ȡ������
                    string[] strALTN = strReport.Substring(iBegin, iEnd - iBegin + 1).Split(' ');

                    //�����б�������������ת�������룬�����뱸�����ı�����
                    for (int iLoop = 0; iLoop < strALTN.Length; iLoop++)
                    {
                        //���������Ϊ��NIL����������һ������
                        if (strALTN[iLoop] == "NIL")
                        {
                            continue;
                        }

                        //�����������ѯ������Ϣ
                        rvSF = airportInforBF.GetAirportInforByThreeCode(strALTN[iLoop]);

                        //��������쳣
                        if (rvSF.Result < 0)
                        {
                            MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        else
                        {
                            //�����������û�ж�Ӧ�Ļ���
                            if (rvSF.Dt.Rows.Count == 0)
                            {
                                MessageBox.Show("������������" + strALTN[iLoop] + "������ӣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                //�������������ı�����
                                txtBackUp.Text += rvSF.Dt.Rows[0]["cncAirportFourCode"].ToString() + " ";
                            }
                        }                       
                    }

                    txtBackUp.Text = txtBackUp.Text.Trim();
                }
            }
            #endregion

            //����������ҳ���ַ
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

        #region ���²�ѯ���мƻ�
        /// <summary>
        /// ���²�ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //����������ҳ���ַ
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
        /// ���캯��
        /// </summary>
        /// <param name="strFlightDate">��������</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">��ɻ���������</param>
        /// <param name="strARRSTN">�������������</param>
        /// <param name="strDATOP">�������ڣ�UTC��</param>
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