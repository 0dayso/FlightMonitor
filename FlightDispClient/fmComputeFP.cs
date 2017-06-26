using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    /// <summary>
    /// ������мƻ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���  ��
    /// �������ڣ�2008-07-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public partial class fmComputeFP : Form
    {
        private ChangeLegsBM m_ChangeLegsBM;   //���ද̬ʵ��
        private MaintenGuaranteeInforBM m_MaintenGuaranteeInfoBM;        //������Ϣά��ʵ��
        private string m_strCFPId;       //���мƻ�Id
        private CFPBM m_CFPBM;    //���мƻ�ʵ��

        //�Ƿ�����ˢ��
        private int _iRefresh = 0;
        /// <summary>
        /// �Ƿ�ˢ������ͼ��0=��ˢ�£�1=ˢ��
        /// </summary>
        public int RefreshMainView
        {
            get { return _iRefresh; }
        }

        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="maintenGuaranteeInfoBM">������Ϣά��ʵ��</param>
        /// <param name="changeLegsBM">���ද̬ʵ��</param>
        /// <param name="iCFPId">���мƻ�Id</param>
        public fmComputeFP(MaintenGuaranteeInforBM maintenGuaranteeInfoBM, ChangeLegsBM changeLegsBM, string strCFPId)
        {
            InitializeComponent();

            m_ChangeLegsBM = changeLegsBM;
            m_MaintenGuaranteeInfoBM = maintenGuaranteeInfoBM;
            m_CFPBM = new CFPBM();
            
            //�Ƿ��Ѽ�����мƻ�
            if (strCFPId != "")
            {
                FlightDispBF flightDispBF = new FlightDispBF();
                m_CFPBM = flightDispBF.GetCFPById(strCFPId);
                m_strCFPId = strCFPId;
            }
            else
            {
                m_CFPBM.DATOP = m_ChangeLegsBM.DATOP;
                m_CFPBM.FLTID = m_ChangeLegsBM.FLTID;
                m_CFPBM.LegNO = m_ChangeLegsBM.LEGNO;
                m_CFPBM.AC = m_ChangeLegsBM.AC;
            }
        }

        private string m_PlanNo;
        public string PlanNo
        {
            get { return m_PlanNo; }
        }

        private void fmComputeFP_Load(object sender, EventArgs e)
        {
            txtPOD.Text = m_ChangeLegsBM.DEPSTN;
            txtPOA.Text = m_ChangeLegsBM.ARRSTN;
            txtCallSign.Text = m_ChangeLegsBM.FLTID;
            txtAircraft.Text = m_ChangeLegsBM.LONG_REG;
            txtCI.Text = "CI30";
            txtETD.Text = DateTime.Parse(m_ChangeLegsBM.ETD).ToString("HHmm");
            txtPrimaryRte.Text = "rt/" + m_ChangeLegsBM.DEPFourCode.Substring(1) + m_ChangeLegsBM.ARRFourCode.Substring(1) + "-";

            if (m_strCFPId != null)
            {
                btnCheckPlan.Enabled = true;
            }
        }

        #region ������мƻ�
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            #region �������
            //���мƻ�ʵ��
            CompFP.Plan plan = new CompFP.Plan();

            //���мƻ���������
            CompFP.PlanReq planReq = new CompFP.PlanReq();

            //�û���Ϣ
            CompFP.Login login = new CompFP.Login();
            login.userid = "chhhdq";
            login.passwd = "haklbs";
            planReq.login = login;

            //���ʱ��
            planReq.deptime = Convert.ToInt16(txtETD.Text);

            //��ɻ���
            CompFP.Airport pod = new CompFP.Airport();
            pod.arptid = txtPOD.Text;
            pod.airporttype = CompFP.AirportType.PUBLISHED;

            //Ŀ�Ļ���
            CompFP.Airport poa = new CompFP.Airport();
            poa.arptid = txtPOA.Text;
            poa.airporttype = CompFP.AirportType.PUBLISHED;

            planReq.pod = pod;
            planReq.poa = poa;

            //��·
            planReq.rteinp = txtPrimaryRte.Text;

            //������
            CompFP.Alternate[] alts = new CompFP.Alternate[1];
            CompFP.Alternate alt1 = new CompFP.Alternate();
            alt1.arptid = txtALT.Text;
            alt1.type = CompFP.AltAirportType.CRDB;
            alt1.crdbname = txtALTRte.Text;
            alts[0] = alt1;
            planReq.alternates = alts;

            //�ɻ���Ϣ
            CompFP.Aircraft ac = new CompFP.Aircraft();
            ac.custaircraft = txtAircraft.Text;
            planReq.aircraft = ac;

            //������Ϣ
            CompFP.Case caseData = new CompFP.Case();
            caseData.fuel = Convert.ToInt16(txtFuel.Text);
            caseData.payload = Convert.ToInt16(txtPayLoad.Text);
            caseData.weightpayloadcase = CompFP.CaseType.ARRIVALFUELCASE;
            planReq.casedata = caseData;

            //Cruise Modes
            CompFP.CruiseModes cruise = new CompFP.CruiseModes();
            cruise.cruisetopoa = txtCI.Text;
            planReq.cruisemodes = cruise;
            #endregion

            try
            {
                #region ���������
                CompFP.PlanResp planResp = plan.computePlan(planReq);
                string strResult = planResp.plans[0].data;
                if (strResult != null)
                {
                    CompFP.Plan1 plan1 = planResp.plans[0];
                    this.m_PlanNo = plan1.plannumber.ToString();
                    strResult = strResult.Replace("\n", "\r\n");

                    //������мƻ��ؼ�����
                    m_CFPBM.PlanNo = plan1.plannumber;
                    //�������
                    m_CFPBM.TotalFuelLB = plan1.fuels.depfuel;
                    //���ߺ���
                    m_CFPBM.LineFuelLB = plan1.fuels.burn - plan1.fuels.hold - plan1.fuels.reserve;
                    //����ʱ��
                    m_CFPBM.FlyTimeMin = Convert.ToInt16(plan1.times.time / 60);
                    //�������
                    m_CFPBM.TOWLB = plan1.weights.depweight;
                    //�������
                    m_CFPBM.LWLB = plan1.weights.arrweight;
                    //���о���
                    m_CFPBM.DistanceNM = plan1.distances.dist;
                    //���мƻ�����
                    m_CFPBM.PlanContent = strResult;
                    //�����´���
                    fmSendMegs obj = new fmSendMegs(m_MaintenGuaranteeInfoBM, m_ChangeLegsBM, m_CFPBM);
                    //��������˵籨
                    if (obj.ShowDialog() == DialogResult.OK)
                    {
                        //ˢ������ͼ
                        _iRefresh = 1;
                        btnCheckPlan.Enabled = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(planResp.plans[0].errtext, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //txtPlanContent.Text = planResp.plans[0].errtext;
                }
                #endregion
            }
            catch (Exception ex)
            {
                //txtPlanContent.Text = ex.Message;
            }
        }
        #endregion

        #region ֧����Ϣ
        /// <summary>
        /// �鿴�ı�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblTextWX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strUrl = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planMet.aspx?strCompanyID=9&strstdDate=";
            strUrl += m_ChangeLegsBM.FlightDate + "&strDepstn=" + m_ChangeLegsBM.DEPSTN +
                "&strArrstn=" + m_ChangeLegsBM.ARRSTN + "&strFlightno=" + m_ChangeLegsBM.FlightNo +
                "&strPlaneNo=" + m_ChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_ChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_ChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_ChangeLegsBM.STD;

            wbSupportInfo.Navigate(strUrl);
        }

        /// <summary>
        /// �鿴����ͨ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblNotam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strUrl = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planNotam.aspx?strCompanyID=9&strstdDate=";
            strUrl += m_ChangeLegsBM.FlightDate + "&strDepstn=" + m_ChangeLegsBM.DEPSTN +
                "&strArrstn=" + m_ChangeLegsBM.ARRSTN + "&strFlightno=" + m_ChangeLegsBM.FlightNo +
                "&strPlaneNo=" + m_ChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_ChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_ChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_ChangeLegsBM.STD;

            wbSupportInfo.Navigate(strUrl);
        }
        #endregion

        #region ������мƻ�
        /// <summary>
        /// ������мƻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStorePlan_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightDispBF flightDispBF = new FlightDispBF();

            rvSF = flightDispBF.InsertCFP(m_CFPBM);
            if (rvSF.Result > 0)
            {
                ChangeRecordBM changeRecordBM = new ChangeRecordBM();

                m_MaintenGuaranteeInfoBM.NewContent = rvSF.Result.ToString();
                changeRecordBM.ChangeNewContent = rvSF.Result.ToString();

                //��ȡ������ʱ��
                ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
                rvSF = serverDateTimeBF.GetServerDateTime();

                if (rvSF.Result > 0)
                {
                    changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                changeRecordBM.FOCOperatingTime = "";
                ChangeRecordBF changeRecordBF = new ChangeRecordBF();

                rvSF = flightDispBF.UpdateGuaranteeInfor(m_MaintenGuaranteeInfoBM);
                //if (rvSF.Result > 0)
                //{
                //    rvSF = changeRecordBF.Insert(changeRecordBM);
                //}
            }
        }
        #endregion

        private void btnCheckPlan_Click(object sender, EventArgs e)
        {
            fmSendMegs obj = new fmSendMegs(m_MaintenGuaranteeInfoBM, m_ChangeLegsBM, m_CFPBM);
            obj.ShowDialog();
        }

        private void btnGetPayLoad_Click(object sender, EventArgs e)
        {
            fmPLDInfo obj = new fmPLDInfo(m_ChangeLegsBM);
            if (obj.ShowDialog() == DialogResult.OK)
            {
                txtPayLoad.Text = obj.PayLoadInfo.TotalPayLoad;
            }
        }
    }
}