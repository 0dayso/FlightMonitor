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
    public partial class fmSendMegs : Form
    {
        private ChangeLegsBM m_ChangeLegsBM;   //航班动态实体
        private MaintenGuaranteeInforBM m_MaintenGuaranteeInfoBM;        //保障信息维护实体
        private CFPBM m_CFPBM;

        public fmSendMegs(MaintenGuaranteeInforBM maintenGuaranteeInfoBM, ChangeLegsBM changeLegsBM, CFPBM cfpBM)
        {
            InitializeComponent();

            m_ChangeLegsBM = changeLegsBM;
            m_MaintenGuaranteeInfoBM = maintenGuaranteeInfoBM;
            m_CFPBM = cfpBM;
        }

        private void fmSendMegs_Load(object sender, EventArgs e)
        {
            txtPlanContent.Text = m_CFPBM.PlanContent;
            string strReleaseMeg = "ZCZC UJA 180821\r\nQU HAKZPCA\r\n.HAKUOHU 180821\r\nDISPATCH RELEASE:\r\nHU7087/17JUL  ETD2339 B2113/B733\r\nDEP:HAK/ALTN:NIL\r\nROUTE ALTN:NIL\r\nDEST:KMG/ALTN:NNG KWE\r\nFLT RULE:IFR\r\nTRIP FUEL:3844KGS/8475LBS\r\nTTL FUEL:8900KGS/19621LBS\r\nCREW:LIBO/LISHANJIE\r\nSI:CFP\r\nTEL:0898-65756523\r\nFAX:0898-65751587\r\nDSP SIGN:\r\nPIC SIGN:\r\n\r\n(FPL-CHH7087-IS\r\n-B733/M-SDHIRW/S\r\n-ZJHK2339\r\n-M074S0920 HAK LH R339 BSE A599 KMG\r\n-ZPPP0123 ZGNN ZUGY\r\n-EET/ZGZU0006 ZPKM0056\r\nREG/B2113 SEL/DGEH RMK/ACAS)\r\nNNNN";
            txtTelMeg.Text = strReleaseMeg;
            txtFuelInfo.Text = "18JULHU7087HAKKMGB2113FUEL8900KG/19621LBT0898-65756523,16:21";
            txtMobileNo.Text = "13876037166";

            cbxSITAAdress.Text = "HAKUOHU";
            cbxAFTNAdress.Text = "ZJHKCHHO";
        }

        #region 发送报文
        /// <summary>
        /// 发送报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMegs_Click(object sender, EventArgs e)
        {
            SaveCFP();
            m_MaintenGuaranteeInfoBM.FieldName = "cnvcTelMegs";
            m_MaintenGuaranteeInfoBM.NewContent = "已发送报文";
            FlightDispBF flightDispBF = new FlightDispBF();
            flightDispBF.UpdateGuaranteeInfor(m_MaintenGuaranteeInfoBM);
        }
        #endregion

        
        private void btnStorePlan_Click(object sender, EventArgs e)
        {
            SaveCFP();
        }
        
        #region 保存报文
        private void SaveCFP()
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
    }
}