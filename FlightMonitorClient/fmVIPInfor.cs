using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmVIPInfor : Form
    {
        private ChangeLegsBM m_changeLegsBM;
        private VIPBM m_vipBM;
        private int m_iUpdate;
        private AccountBM m_accountBM;

        /// <summary>
        /// 添加VIP构造函数
        /// </summary>
        /// <param name="changeLegsBM"></param>
        public fmVIPInfor(ChangeLegsBM changeLegsBM, AccountBM accountBM, int iUpdate)
        {
            InitializeComponent();
            this.m_changeLegsBM = changeLegsBM;
            this.m_accountBM = accountBM;
            this.m_iUpdate = iUpdate;            
        }

        /// <summary>
        /// 修改VIP构造函数
        /// </summary>
        /// <param name="vipBM"></param>
        /// <param name="iUpdate"></param>
        public fmVIPInfor(ChangeLegsBM changeLegsBM, VIPBM vipBM, AccountBM accountBM, int iUpdate)
        {
            InitializeComponent();
            this.m_changeLegsBM = changeLegsBM;
            this.m_vipBM = vipBM;
            this.m_accountBM = accountBM;
            this.m_iUpdate = iUpdate;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF;
            VIPBF vipBF = new VIPBF();
             VIPBM vipBM = new VIPBM();


            //添加VIP            
            if (m_iUpdate == 0)            {
               
                vipBM.DATOP = m_changeLegsBM.DATOP;
                vipBM.FLTID = m_changeLegsBM.FLTID;
                vipBM.DEPSTN = m_changeLegsBM.DEPSTN;
                vipBM.ARRSTN = m_changeLegsBM.ARRSTN;
                vipBM.Name = txtName.Text.Trim();
                vipBM.POSITION = txtPosition.Text.Trim();
                vipBM.CLASS = txtClass.Text.Trim();
                vipBM.VipType = txtVipType.Text.Trim();
                vipBM.CONTRACT_NUMBER = txtContactNumber.Text.Trim();
                vipBM.INFORM_PERSON = txtInformPersion.Text;
                vipBM.SPECIAL_REQUIREMENTS = txtSpecialRequirements.Text.Trim();
                vipBM.ACCOMPANY_NBR = txtAccompanyNumber.Text.Trim();
                vipBM.ACCOMPANY_LEADER = txtAccompanyLeader.Text.Trim();
                vipBM.REMARKS = txtRemarks.Text.Trim();
                vipBM.DataSource = "现场";

                
                rvSF = vipBF.InsertVIP(vipBM);
            }
            else
            {
                vipBM.DATOP = m_vipBM.DATOP;
                vipBM.FLTID = m_vipBM.FLTID;
                vipBM.DEPSTN = m_vipBM.DEPSTN;
                vipBM.ARRSTN = m_vipBM.ARRSTN;
                vipBM.Name = m_vipBM.Name;
                vipBM.POSITION = txtPosition.Text.Trim();
                vipBM.CLASS = txtClass.Text.Trim();
                vipBM.VipType = txtVipType.Text.Trim();
                vipBM.CONTRACT_NUMBER = txtContactNumber.Text.Trim();
                vipBM.INFORM_PERSON = txtInformPersion.Text;
                vipBM.SPECIAL_REQUIREMENTS = txtSpecialRequirements.Text.Trim();
                vipBM.ACCOMPANY_NBR = txtAccompanyNumber.Text.Trim();
                vipBM.ACCOMPANY_LEADER = txtAccompanyLeader.Text.Trim();
                vipBM.REMARKS = txtRemarks.Text.Trim();
                vipBM.DataSource = "现场";
                rvSF = vipBF.UpdateVipInfor(vipBM);
            }


            if (rvSF.Result > 0)
            {
                ChangeRecordBF changeRecordBF = new ChangeRecordBF();
                ChangeRecordBM changeRecordBM = new ChangeRecordBM();
                changeRecordBM.UserID = m_accountBM.UserId;
                changeRecordBM.OldDATOP = m_changeLegsBM.DATOP;
                changeRecordBM.OldFLTID = m_changeLegsBM.FLTID;
                changeRecordBM.OldLegNo = m_changeLegsBM.LEGNO;
                changeRecordBM.OldAC = m_changeLegsBM.AC;
                changeRecordBM.NewDATOP = m_changeLegsBM.DATOP;
                changeRecordBM.NewFLTID = m_changeLegsBM.FLTID;
                changeRecordBM.NewLegNo = m_changeLegsBM.LEGNO;
                changeRecordBM.NewAC = m_changeLegsBM.AC;
                changeRecordBM.OldDepSTN = m_changeLegsBM.DEPSTN;
                changeRecordBM.OldArrSTN = m_changeLegsBM.ARRSTN;
                changeRecordBM.NewDepSTN = m_changeLegsBM.DEPSTN;
                changeRecordBM.NewArrSTN = m_changeLegsBM.ARRSTN;
                changeRecordBM.STD = m_changeLegsBM.STD;
                changeRecordBM.ETD = m_changeLegsBM.ETD;
                changeRecordBM.STA = m_changeLegsBM.STA;
                changeRecordBM.ETA = m_changeLegsBM.ETA;
                changeRecordBM.ChangeReasonCode = "cnbVIPTag";
                changeRecordBM.ChangeOldContent = "";
                changeRecordBM.ChangeNewContent = "有";
                changeRecordBM.ActionTag = "U";
                changeRecordBM.Refresh = 0;
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

                changeRecordBF.Insert(changeRecordBM);
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmVIPInfor_Load(object sender, EventArgs e)
        {
            if (m_iUpdate == 1)
            {
                txtName.ReadOnly = true;

                txtName.Text = m_vipBM.Name;
                txtPosition.Text = m_vipBM.POSITION;
                txtClass.Text = m_vipBM.CLASS;
                txtVipType.Text = m_vipBM.VipType;
                txtContactNumber.Text = m_vipBM.CONTRACT_NUMBER;
                txtInformPersion.Text = m_vipBM.INFORM_PERSON;
                txtAccompanyNumber.Text = m_vipBM.ACCOMPANY_NBR;
                txtAccompanyLeader.Text = m_vipBM.ACCOMPANY_LEADER;
                txtSpecialRequirements.Text = m_vipBM.SPECIAL_REQUIREMENTS;
                txtRemarks.Text = m_vipBM.REMARKS;
            }
        }
    }
}