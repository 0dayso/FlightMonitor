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
    /// 计算飞行计划窗体
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：张  黎
    /// 创建日期：2008-07-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public partial class fmComputeFP : Form
    {
        private ChangeLegsBM m_ChangeLegsBM;   //航班动态实体
        private MaintenGuaranteeInforBM m_MaintenGuaranteeInfoBM;        //保障信息维护实体
        private string m_strCFPId;       //飞行计划Id
        private CFPBM m_CFPBM;    //飞行计划实体

        //是否设置刷新
        private int _iRefresh = 0;
        /// <summary>
        /// 是否刷新主视图：0=不刷新；1=刷新
        /// </summary>
        public int RefreshMainView
        {
            get { return _iRefresh; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maintenGuaranteeInfoBM">保障信息维护实体</param>
        /// <param name="changeLegsBM">航班动态实体</param>
        /// <param name="iCFPId">飞行计划Id</param>
        public fmComputeFP(MaintenGuaranteeInforBM maintenGuaranteeInfoBM, ChangeLegsBM changeLegsBM, string strCFPId)
        {
            InitializeComponent();

            m_ChangeLegsBM = changeLegsBM;
            m_MaintenGuaranteeInfoBM = maintenGuaranteeInfoBM;
            m_CFPBM = new CFPBM();
            
            //是否已计算飞行计划
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

        #region 计算飞行计划
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            #region 输入参数
            //飞行计划实体
            CompFP.Plan plan = new CompFP.Plan();

            //飞行计划计算请求
            CompFP.PlanReq planReq = new CompFP.PlanReq();

            //用户信息
            CompFP.Login login = new CompFP.Login();
            login.userid = "chhhdq";
            login.passwd = "haklbs";
            planReq.login = login;

            //起飞时间
            planReq.deptime = Convert.ToInt16(txtETD.Text);

            //起飞机场
            CompFP.Airport pod = new CompFP.Airport();
            pod.arptid = txtPOD.Text;
            pod.airporttype = CompFP.AirportType.PUBLISHED;

            //目的机场
            CompFP.Airport poa = new CompFP.Airport();
            poa.arptid = txtPOA.Text;
            poa.airporttype = CompFP.AirportType.PUBLISHED;

            planReq.pod = pod;
            planReq.poa = poa;

            //航路
            planReq.rteinp = txtPrimaryRte.Text;

            //备降场
            CompFP.Alternate[] alts = new CompFP.Alternate[1];
            CompFP.Alternate alt1 = new CompFP.Alternate();
            alt1.arptid = txtALT.Text;
            alt1.type = CompFP.AltAirportType.CRDB;
            alt1.crdbname = txtALTRte.Text;
            alts[0] = alt1;
            planReq.alternates = alts;

            //飞机信息
            CompFP.Aircraft ac = new CompFP.Aircraft();
            ac.custaircraft = txtAircraft.Text;
            planReq.aircraft = ac;

            //油量信息
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
                #region 处理计算结果
                CompFP.PlanResp planResp = plan.computePlan(planReq);
                string strResult = planResp.plans[0].data;
                if (strResult != null)
                {
                    CompFP.Plan1 plan1 = planResp.plans[0];
                    this.m_PlanNo = plan1.plannumber.ToString();
                    strResult = strResult.Replace("\n", "\r\n");

                    //保存飞行计划关键数据
                    m_CFPBM.PlanNo = plan1.plannumber;
                    //离港油量
                    m_CFPBM.TotalFuelLB = plan1.fuels.depfuel;
                    //航线耗油
                    m_CFPBM.LineFuelLB = plan1.fuels.burn - plan1.fuels.hold - plan1.fuels.reserve;
                    //飞行时间
                    m_CFPBM.FlyTimeMin = Convert.ToInt16(plan1.times.time / 60);
                    //起飞重量
                    m_CFPBM.TOWLB = plan1.weights.depweight;
                    //落地重量
                    m_CFPBM.LWLB = plan1.weights.arrweight;
                    //飞行距离
                    m_CFPBM.DistanceNM = plan1.distances.dist;
                    //飞行计划内容
                    m_CFPBM.PlanContent = strResult;
                    //弹出新窗体
                    fmSendMegs obj = new fmSendMegs(m_MaintenGuaranteeInfoBM, m_ChangeLegsBM, m_CFPBM);
                    //如果发送了电报
                    if (obj.ShowDialog() == DialogResult.OK)
                    {
                        //刷新主视图
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

        #region 支持信息
        /// <summary>
        /// 查看文本气象
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
        /// 查看航行通告
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

        #region 保存飞行计划
        /// <summary>
        /// 保存飞行计划
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

                //获取服务器时间
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