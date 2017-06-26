using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    /// <summary>
    /// ǩ�ɷ���ϵͳ������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���  ��
    /// �������ڣ�2008-07-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public partial class fmMDIFlightDisp : Form
    {
        private DataTable dtMenuItemPurview;  //ϵͳȨ��
        private PositionNameBM m_positionNameBM;   //ϯλ����

        public fmMDIFlightDisp()
        {
            InitializeComponent();
        }

        private static AccountBM m_LogAccount;
        /// <summary>
        /// �û���½��Ϣ
        /// </summary>
        public static AccountBM LogAccount
        {
            get { return m_LogAccount; }
            set { m_LogAccount = value; }
        }

        private int _iDispFlightsCount = 0;
        /// <summary>
        /// �����к�������
        /// </summary>
        public int DispFlightsCount
        {
            get { return _iDispFlightsCount; }
            set { _iDispFlightsCount = value; }
        }

        private int _iMoniFlightsCount = 0;
        /// <summary>
        /// ��غ�������
        /// </summary>
        public int MoniFlightsCount
        {
            get { return _iMoniFlightsCount; }
            set { _iMoniFlightsCount = value; }
        }
        #region ���봰��
        private void fmMDIFlightDisp_Load(object sender, EventArgs e)
        {
            //����ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            //��ȡϵͳȨ��
            MenuPurviewBF menuPurviewBF = new MenuPurviewBF();
            rvSF = menuPurviewBF.GetMenuItemPurview(m_LogAccount);
            dtMenuItemPurview = rvSF.Dt;
            //ϯλ����ʵ�����
            int iPositionId = Convert.ToInt32(ConfigSettings.ReadSetting("PositionID").ToString());
            m_positionNameBM = new PositionNameBM();
            m_positionNameBM.PositionID = iPositionId;
            PositionNameBF positionNameBF = new PositionNameBF();
            rvSF = positionNameBF.GetPositionByID(m_positionNameBM);
            if (rvSF.Result > 0)
            {
                if (rvSF.Dt.Rows.Count > 0)
                {
                    m_positionNameBM = new PositionNameBM(rvSF.Dt.Rows[0]);
                }
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            statusBarMain.Panels[0].Text = "��½ϯλ:" + m_positionNameBM.PositionName;
            statusBarMain.Panels[1].Text = "��½�û�:" + m_LogAccount.UserName;
            //��ѯϯλ���еĺ���
            PositionInforBF positionInforBF = new PositionInforBF();
            rvSF = positionInforBF.GetInforByPositionId(m_positionNameBM);
            DataTable dtDeskAircrafts = rvSF.Dt;
            
            //���Ӵ���
            string strDispForm = "Dsp";
            string strMoniForm = "Mon";

            fmFlightMoni objfmFlightMoni = new fmFlightMoni(m_LogAccount, strMoniForm, dtDeskAircrafts, m_positionNameBM);
            fmFlightDisp objfmFlightDisp = new fmFlightDisp(m_LogAccount, strDispForm, dtDeskAircrafts, m_positionNameBM);

            string strDeskInfo = "ϯλ�ɻ�������" + dtDeskAircrafts.Rows.Count.ToString();
            strDeskInfo += "    ϯλ����������" + Convert.ToString(objfmFlightDisp.DisplayDeskFlights.Rows.Count + objfmFlightMoni.DisplayDeskFlights.Rows.Count);
            statusBarMain.Panels[2].Text = strDeskInfo;

            strDeskInfo = "";
            strDeskInfo = "�����к��ࣺ" + Convert.ToString(objfmFlightDisp.DisplayDeskFlights.Rows.Count);
            strDeskInfo += "    �ѷ��к��ࣺ" + Convert.ToString(objfmFlightMoni.DisplayDeskFlights.Rows.Count);
            statusBarMain.Panels[3].Text = strDeskInfo;

            objfmFlightMoni.Show();
            objfmFlightDisp.Show();

            objfmFlightMoni.MdiParent = this;
            objfmFlightDisp.MdiParent = this;

            objfmFlightDisp.WindowState = FormWindowState.Maximized;
            objfmFlightMoni.WindowState = FormWindowState.Maximized;

            objfmFlightDisp.Activate();
            //��ֱ����
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }
        #endregion

        #region �˵�����������ť�¼�

        #region �û���Ϣ����
        private void miAccountManage_Click(object sender, EventArgs e)
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miAccountManage' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmAccount objFmAccount = new fmAccount(m_LogAccount);
            objFmAccount.ShowDialog();
        }
        #endregion

        #region ϯλ����
        /// <summary>
        /// ϯλ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeskManager_Click(object sender, EventArgs e)
        {
            ManageDesk();
        }
        private void miAddPosition_Click(object sender, EventArgs e)
        {
            ManageDesk();
        }
        #endregion

        #region ������ͼ
        /// <summary>
        /// ������ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSetView_Click(object sender, EventArgs e)
        {
            SetView();
        }
        private void miDataItemView_Click(object sender, EventArgs e)
        {
            SetView();
        }
        #endregion

        #region ѡ��ϯλ
        /// <summary>
        /// ѡ��ϯλ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDeskChange_Click(object sender, EventArgs e)
        {
            DeskChange();
        }
        private void miPosition_Click(object sender, EventArgs e)
        {
            DeskChange();
        }
        #endregion

        #region ���ɻ����䵽ϯλ
        private void miAssignAircraftNo_Click(object sender, EventArgs e)
        {
            AssignAircraft();
        }
        #endregion

        #region �ɻ���Ϣ����
        private void miACMISCManage_Click(object sender, EventArgs e)
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miACMISCManage' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            fmACMISC objfmACMISC = new fmACMISC();
            objfmACMISC.ShowDialog();
        }
        #endregion

        #region ˢ����ͼ
        private void tbRefresh_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightDisp)
                {
                    (MdiChildren[iLoop] as fmFlightDisp).FlightRefresh();
                }
                else if (MdiChildren[iLoop] is fmFlightMoni)
                {
                    (MdiChildren[iLoop] as fmFlightMoni).FlightRefresh();
                }
            }
        }
        #endregion

        #endregion

        #region ѡ��ϯλ
        private void DeskChange()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miPosition' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmSetPosition objfmSetPosition = new fmSetPosition(m_positionNameBM);
            if (objfmSetPosition.ShowDialog() == DialogResult.OK)
            {
                this.m_positionNameBM = objfmSetPosition.MPositionNameBM;
                PositionInforBF positionInforBF = new PositionInforBF();
                ReturnValueSF rvSF = positionInforBF.GetInforByPositionId(m_positionNameBM);
                DataTable dtDeskAircrafts = rvSF.Dt;

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightDisp)
                    {
                        (MdiChildren[iLoop] as fmFlightDisp).DeskChange(dtDeskAircrafts);
                    }

                    if (MdiChildren[iLoop] is fmFlightMoni)
                    {
                        (MdiChildren[iLoop] as fmFlightMoni).DeskChange(dtDeskAircrafts);
                    }

                    statusBarMain.Panels[0].Text = "��½ϯλ:" + m_positionNameBM.PositionName;
                    statusBarMain.Panels[1].Text = "��½�û�:" + m_LogAccount.UserName;
                }
            }
        }
        #endregion

        #region ����ϯλ
        private void ManageDesk()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miAddPosition' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmPostionManagement objfmPostionManagement = new fmPostionManagement();

            objfmPostionManagement.ShowDialog();
        }
        #endregion

        #region ������ͼ
        private void SetView()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemView' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //��ʾ��ͼ���ô���
            fmDispDataItemView objfmDataItemView = new fmDispDataItemView(m_LogAccount);
            if (objfmDataItemView.ShowDialog() == DialogResult.OK)
            {
                //������ͼ
                SpreadGrid spreadGrid = new SpreadGrid(m_LogAccount);

                //��ȡ�û���Ȩ�޵�������
                DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightDisp)
                    {
                        ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount, "Dsp");
                        DataTable dtDataItems = rvSF.Dt;
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightDisp).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        (MdiChildren[iLoop] as fmFlightDisp).ViewRefresh(dtDataItems);
                    }

                    if (MdiChildren[iLoop] is fmFlightMoni)
                    {
                        ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount, "Mon");
                        DataTable dtDataItems = rvSF.Dt;
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightMoni).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        (MdiChildren[iLoop] as fmFlightMoni).ViewRefresh(dtDataItems);
                    }
                }//for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            }//if (objfmDataItemView.ShowDialog() == DialogResult.OK)
        }
        #endregion

        #region ���ɻ����䵽ϯλ
        /// <summary>
        /// ����ϯλ
        /// </summary>
        private void AssignAircraft()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miAssignAircraftNo' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmPositionInfor objfmPositionInfor = new fmPositionInfor();
            objfmPositionInfor.ShowDialog();
        }
        #endregion

        #region �����ڹر��¼�
        private void fmMDIFlightDisp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("�Ƿ��˳���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearLogInfo();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// �����½�û���Ϣ
        /// </summary>
        private void ClearLogInfo()
        {
            //����ҵ����۲㷽��
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            m_LogAccount.LogUser -= 1;
            accountBF.UpdateLogUser(m_LogAccount);

            int iLogIndex = 1;
            for (int iLoop = 1; iLoop <= 100; iLoop++)
            {
                if (ConfigSettings.ReadSetting("LogOnUser" + iLoop.ToString()).ToString() == m_LogAccount.UserId)
                {
                    iLogIndex = iLoop;
                    break;
                }
            }
            ConfigSettings.WriteSetting("LogOFF" + iLogIndex.ToString(), "1");
        }
        #endregion


        private void timerRemind_Tick(object sender, EventArgs e)
        {
            Image imgSpash = Image.FromFile("..\\..\\Resources\\Splash.gif");
            tbRemind.Image = imgSpash;
        }

        private void tbRemind_Click(object sender, EventArgs e)
        {
            Image imgNormal = Image.FromFile("..\\..\\Resources\\Normal.gif");
            tbRemind.Image = imgNormal;
            timerRemind.Enabled = false;
            fmRemindInfo obj = new fmRemindInfo(m_positionNameBM);
            obj.ShowDialog();
            timerRemind.Enabled = true;
        }
    }
}
