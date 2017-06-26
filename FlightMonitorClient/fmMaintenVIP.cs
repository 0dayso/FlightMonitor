using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmMaintenVIP : Form
    {
        private ChangeLegsBM m_changeLegsBM;
        private AccountBM m_accountBM;
        private int m_iDataItmePurview;

        public fmMaintenVIP(ChangeLegsBM changeLegsBM, AccountBM accountBM, int iDataItemPurview)
        {
            InitializeComponent();

            this.m_changeLegsBM = changeLegsBM;
            this.m_accountBM = accountBM;
            this.m_iDataItmePurview = iDataItemPurview;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMaintenVIP_Load(object sender, EventArgs e)
        {
            fpVip.Sheets[0].Columns[0].DataField = "cnvcDataSource";
            fpVip.Sheets[0].Columns[1].DataField = "cnvcNAME";
            fpVip.Sheets[0].Columns[2].DataField = "cnvcPOSITION";
            fpVip.Sheets[0].Columns[3].DataField = "cncCLASS";
            fpVip.Sheets[0].Columns[4].DataField = "cnvcVipType";
            fpVip.Sheets[0].Columns[5].DataField = "cnvcCONTRACT_NUMBER";
            fpVip.Sheets[0].Columns[6].DataField = "cnvcINFORM_PERSON";
            fpVip.Sheets[0].Columns[7].DataField = "cnvcSPECIAL_REQUIREMENTS";
            fpVip.Sheets[0].Columns[8].DataField = "cnvcREMARKS";
            fpVip.Sheets[0].Columns[9].DataField = "cnvcACCOMPANY_NBR";
            fpVip.Sheets[0].Columns[10].DataField = "cnvcACCOMPANY_LEADER";

            fpDeleteVip.Sheets[0].Columns[0].DataField = "cnvcDataSource";
            fpDeleteVip.Sheets[0].Columns[1].DataField = "cnvcNAME";
            fpDeleteVip.Sheets[0].Columns[2].DataField = "cnvcPOSITION";
            fpDeleteVip.Sheets[0].Columns[3].DataField = "cncCLASS";
            fpDeleteVip.Sheets[0].Columns[4].DataField = "cnvcVipType";
            fpDeleteVip.Sheets[0].Columns[5].DataField = "cnvcCONTRACT_NUMBER";
            fpDeleteVip.Sheets[0].Columns[6].DataField = "cnvcINFORM_PERSON";
            fpDeleteVip.Sheets[0].Columns[7].DataField = "cnvcSPECIAL_REQUIREMENTS";
            fpDeleteVip.Sheets[0].Columns[8].DataField = "cnvcREMARKS";
            fpDeleteVip.Sheets[0].Columns[9].DataField = "cnvcACCOMPANY_NBR";
            fpDeleteVip.Sheets[0].Columns[10].DataField = "cnvcACCOMPANY_LEADER";

            ReturnValueSF rvSF;

            VIPBF vipBF = new VIPBF();

            rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 0);

            if (rvSF.Result > 0)
            {
                fpVip.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 1);

            if (rvSF.Result > 0)
            {
                fpDeleteVip.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (m_iDataItmePurview < 2)
            {
                MessageBox.Show("对不起，您没有维护权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmVIPInfor objfmVIPInfor = new fmVIPInfor(m_changeLegsBM, m_accountBM, 0);

            if (objfmVIPInfor.ShowDialog() == DialogResult.OK)
            {
                ReturnValueSF rvSF;

                VIPBF vipBF = new VIPBF();

                rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 0);

                if (rvSF.Result > 0)
                {
                    fpVip.DataSource = rvSF.Dt;
                }
                else
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 1);

                if (rvSF.Result > 0)
                {
                    fpDeleteVip.DataSource = rvSF.Dt;
                }
                else
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private VIPBM GetSelectedVIP(int iSelectedRowIndex)
        {

            //获取数据源
            DataTable dtVIP = (DataTable)fpVip.Sheets[0].DataSource;           

            //生成实体对象
            return new VIPBM(dtVIP.Rows[iSelectedRowIndex]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private VIPBM GetDeleteSelectedVIP(int iSelectedRowIndex)
        {

            //获取数据源
            DataTable dtVIP = (DataTable)fpDeleteVip.Sheets[0].DataSource;

            //生成实体对象
            return new VIPBM(dtVIP.Rows[iSelectedRowIndex]);
        }


        /// <summary>
        /// 修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (m_iDataItmePurview < 2)
            {
                MessageBox.Show("对不起，您没有维护权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (fpVip.Sheets[0].SelectionCount == 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            fmVIPInfor objfmVIPInfor = new fmVIPInfor(m_changeLegsBM, GetSelectedVIP(fpVip.Sheets[0].Models.Selection.AnchorRow), m_accountBM, 1);

            if (objfmVIPInfor.ShowDialog() == DialogResult.OK)
            {
                ReturnValueSF rvSF;

                VIPBF vipBF = new VIPBF();

                rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 0);

                if (rvSF.Result > 0)
                {
                    fpVip.DataSource = rvSF.Dt;
                }
                else
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 1);

                if (rvSF.Result > 0)
                {
                    fpDeleteVip.DataSource = rvSF.Dt;
                }
                else
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (m_iDataItmePurview < 2)
            {
                MessageBox.Show("对不起，您没有维护权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (fpVip.Sheets[0].SelectionCount == 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            VIPBF vipBF = new VIPBF();

            ReturnValueSF rvSF = vipBF.UpdateDeleteTag(GetSelectedVIP(fpVip.Sheets[0].Models.Selection.AnchorRow), 1);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
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

            rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 0);

            if (rvSF.Result > 0)
            {
                fpVip.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 1);

            if (rvSF.Result > 0)
            {
                fpDeleteVip.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 还原事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecover_Click(object sender, EventArgs e)
        {
            if (m_iDataItmePurview < 2)
            {
                MessageBox.Show("对不起，您没有维护权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (fpDeleteVip.Sheets[0].SelectionCount == 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            VIPBF vipBF = new VIPBF();

            ReturnValueSF rvSF = vipBF.UpdateDeleteTag(GetDeleteSelectedVIP(fpDeleteVip.Sheets[0].Models.Selection.AnchorRow), 0);
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
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
            

           

            rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 0);

            if (rvSF.Result > 0)
            {
                fpVip.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 1);

            if (rvSF.Result > 0)
            {
                fpDeleteVip.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /// <summary>
        /// 修改VIP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpVip_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (m_iDataItmePurview < 2)
            {
                MessageBox.Show("对不起，您没有维护权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmVIPInfor objfmVIPInfor = new fmVIPInfor(m_changeLegsBM, GetSelectedVIP(e.Row), m_accountBM, 1);

            if (objfmVIPInfor.ShowDialog() == DialogResult.OK)
            {
                ReturnValueSF rvSF;

                VIPBF vipBF = new VIPBF();

                rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 0);

                if (rvSF.Result > 0)
                {
                    fpVip.DataSource = rvSF.Dt;
                }
                else
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                rvSF = vipBF.GetVIPByFlight(m_changeLegsBM, 1);

                if (rvSF.Result > 0)
                {
                    fpDeleteVip.DataSource = rvSF.Dt;
                }
                else
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}