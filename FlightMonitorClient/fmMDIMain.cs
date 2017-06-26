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
    /// <summary>
    /// ��վ����ϵͳ��VER 101101����
    /// 1�� �޸� fmFlightGuarantee.cs �� private DataTable GetStationFlights(DateTimeBM dateTimeBM, StationBM stationBM, int iToday)
    ///     ʹ�� ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM);
    ///     �������ڱ��Ͻ���Ϳ�����ȡһ���µ����ݣ�����ֱ�Ӵ����ݿ��ȡ������������
    /// </summary>
    public partial class fmMDIMain : Form
    {
        private static FlightMonitorBM.AccountBM m_LogAccount; //��½�û�ʵ�����
        private DataTable dtStandardIntermissionTime;          //��׼��վʱ���
        private DataTable dtDataItems;  //�û����ó���������
        private DataTable dtMenuItemPurview;  //ϵͳȨ��
        private StationBM m_stationBM;   //�û�������վʵ�����     
        private PositionNameBM m_positionNameBM;   //ϯλ����
        private int m_iAutoAdjust = 0;
        private static int _iOnlineUserNo = 0;

        /// <summary>
        /// ���캯��
        /// </summary>
        public fmMDIMain()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// �û���½��Ϣ
        /// </summary>
        public static AccountBM LogAccount
        {
            get { return m_LogAccount; }
            set { m_LogAccount = value; }
        }
        /// <summary>
        /// ״̬��
        /// </summary>
        public StatusBar MainStatusBar
        {
            get { return statusBarMain; }
        }
        /// <summary>
        /// �Զ�������С
        /// </summary>
        public int AutoAdjust
        {
            get { return m_iAutoAdjust; }
            set { m_iAutoAdjust = value; }
        }
        /// <summary>
        /// �����û����
        /// </summary>
        public static int OnlineUserNo
        {
            get { return _iOnlineUserNo; }
            set { _iOnlineUserNo = value; }
        }
        #endregion

        /// <summary>
        /// ���ش����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMDIMain_Load(object sender, EventArgs e)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            m_iAutoAdjust = Convert.ToInt32(ConfigSettings.ReadSetting("AutoAdjust"));

            //�û�������վʵ�����
            DataTable dtStations = new DataTable();
            StationBF stationBF = new StationBF();
            rvSF = stationBF.GetAllStation();
            if (rvSF.Result > 0)
            {
                dtStations = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DataRow[] drStation = dtStations.Select("cncThreeCode = '" + m_LogAccount.StationThreeCode + "'");

            if (drStation.Length > 0)
            {
                m_stationBM = new StationBM(drStation[0]);
            }
            else
            {
                m_stationBM = new StationBM();
            }

            //ϯλ����ʵ�����
            int iPositionId = Convert.ToInt32(ConfigSettings.ReadSetting("PositionID").ToString());
            m_positionNameBM = new PositionNameBM();
            m_positionNameBM.PositionID = iPositionId;
            PositionNameBF positionNameBF = new PositionNameBF();
            rvSF = positionNameBF.GetPositionByID(m_positionNameBM);
            if (rvSF.Result > 0)
            {
                if(rvSF.Dt.Rows.Count > 0)
                {
                    m_positionNameBM = new PositionNameBM(rvSF.Dt.Rows[0]);
                }
            }
            else
            {                
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            //��ȡ��׼��վʱ��
            IntermissionTimeBF intermissionTimeBF = new IntermissionTimeBF();
            rvSF = intermissionTimeBF.GetStandardIntermissionTime();
            if (rvSF.Result > 0)
            {
                dtStandardIntermissionTime = rvSF.Dt;
            }
            else
            {
                dtStandardIntermissionTime = new DataTable();
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //��ȡ�û���Ȩ�޵�������
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount);
            dtDataItems = rvSF.Dt;

            //��ȡϵͳȨ��
            MenuPurviewBF menuPurviewBF = new MenuPurviewBF();
            rvSF = menuPurviewBF.GetMenuItemPurview(m_LogAccount);
            dtMenuItemPurview = rvSF.Dt;

            //�����û�������ʾ��ͬ�Ĵ���
            //��ʾ�����ؽ���
            if (m_LogAccount.UserTypeId == 1)
            {
                fmFlightWatch objfmFlightWatch = new fmFlightWatch(m_LogAccount, dtStandardIntermissionTime, dtDataItems, m_positionNameBM);
                objfmFlightWatch.Show();
                objfmFlightWatch.MdiParent = this;
                objfmFlightWatch.WindowState = FormWindowState.Maximized;

                statusBarMain.Panels[0].Text = "��½ϯλ:" + m_positionNameBM.PositionName;
                statusBarMain.Panels[1].Text = "��½�û�:" + m_LogAccount.UserName;
            }
            //��ʾ���ౣ�Ͻ���
            else
            {
                fmFlightGuarantee objfmFlightGuarantee = new fmFlightGuarantee(m_LogAccount, dtStandardIntermissionTime, dtDataItems, statusBarMain, m_iAutoAdjust);
                objfmFlightGuarantee.Show();
                objfmFlightGuarantee.MdiParent = this;
                objfmFlightGuarantee.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// �����½�û���Ϣ
        /// </summary>
        private void ClearLogInfo()
        {
            //����ҵ����۲㷽��
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            accountBF.LogOffOnlineUser(_iOnlineUserNo);
        }

        #region ϵͳ�˵�
        /// <summary>
        /// �û�����˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAccountManage_Click(object sender, EventArgs e)
        {
            AccountManage();
        }

        /// <summary>
        /// �����λ���Ų˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miArrangeGate_Click(object sender, EventArgs e)
        {
            ArrangeGate();
        }

        /// <summary>
        /// �����λ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miQueryGate_Click(object sender, EventArgs e)
        {
            QueryGate();
        }

        /// <summary>
        /// ������ݲ�ѯ�˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miChangeData_Click(object sender, EventArgs e)
        {
            GetChangeData();
        }


        /// <summary>
        /// ��ͼ���ò˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDataItemView_Click(object sender, EventArgs e)
        {
            DataItemViewSetting();
        }

        /// <summary>
        /// ϯλ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miPosition_Click(object sender, EventArgs e)
        {
            SetPosition();
        }


        /// <summary>
        /// �����ز˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDataItemWatch_Click(object sender, EventArgs e)
        {
            FlightWatch();
        }

        /// <summary>
        /// ���ౣ�ϲ˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDataItemGuarantee_Click(object sender, EventArgs e)
        {
            FlightGuarantee();
        }

        /// <summary>
        /// ϯλ����˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAssignAircraftNo_Click(object sender, EventArgs e)
        {
            AssignAircraft();
        }

        /// <summary>
        /// ϵͳ��ʾ�˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miSystemPrompt_Click(object sender, EventArgs e)
        {
            SystemPrompt();
        }

        /// <summary>
        /// ��ʾ�����˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDisplayCondition_Click(object sender, EventArgs e)
        {
            DisplayCondition();
        }

        /// <summary>
        /// �˳�ϵͳ�˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// �޸�����˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEditPassword_Click(object sender, EventArgs e)
        {
            EditPassword();
        }

        /// <summary>
        /// ϯλ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAddPosition_Click(object sender, EventArgs e)
        {
            AddPosition();
        }

        /// <summary>
        /// ����ƻ��˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miFlightSchedule_Click(object sender, EventArgs e)
        {
            FlightSchedule();
        }

        /// <summary>
        /// �����ÿ�����ͳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCheckNum_Click(object sender, EventArgs e)
        {
            StatisticPax();
        }

        private void miGuaranteeManagement_Click(object sender, EventArgs e)
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miGuaranteeManagement' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmCommanderInforList objfmCommanderInforList = new fmCommanderInforList(m_LogAccount);
            objfmCommanderInforList.ShowDialog();
        }


        private void miStation_Click(object sender, EventArgs e)
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miStation' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmStationgList objfmStationgList = new fmStationgList();
            objfmStationgList.ShowDialog();

        }
        #endregion

        #region ����˵��¼�
        /// <summary>
        /// ��ʾ�û�������
        /// </summary>
        private void AccountManage()
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

        /// <summary>
        /// �����λ����
        /// </summary>
        private void ArrangeGate()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miArrangeGate' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmArrangeGate objfmArrangeGate = new fmArrangeGate(m_LogAccount, m_stationBM);
            objfmArrangeGate.ShowDialog();
        }

        /// <summary>
        /// �����λ��ѯ
        /// </summary>
        private void QueryGate()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miQueryGate' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmQueryGate objfmQueryGate = new fmQueryGate(m_LogAccount, m_stationBM);
            objfmQueryGate.ShowDialog();
        }

        /// <summary>
        /// ��ѯ����������
        /// </summary>
        private void GetChangeData()
        {
            fmChangeData objfmChangeData = new fmChangeData(null, m_LogAccount, null, null, 1);
            objfmChangeData.ShowDialog();
        }

        /// <summary>
        /// ��ͼ����
        /// </summary>
        private void DataItemViewSetting()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemView' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //��ʾ��ͼ���ô���
            fmDataItemView objfmDataItemView = new fmDataItemView(m_LogAccount);
            if (objfmDataItemView.ShowDialog() == DialogResult.OK)
            {
                //��ȡ�û���Ȩ�޵�������
                DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
                ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_LogAccount);
                dtDataItems = rvSF.Dt;


                //������ͼ
                SpreadGrid spreadGrid = new SpreadGrid(m_LogAccount);

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightWatch)
                    {
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightWatch).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        (MdiChildren[iLoop] as fmFlightWatch).DataItems = dtDataItems;
                        (MdiChildren[iLoop] as fmFlightWatch).RefreshView();
                    }
                    else if (MdiChildren[iLoop] is fmFlightGuarantee)
                    {
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[0], dtDataItems, 0);
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[1], dtDataItems, 0);
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[2], dtDataItems, 0);
                        spreadGrid.SetView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.Sheets[3], dtDataItems, 0);

                        (MdiChildren[iLoop] as fmFlightGuarantee).DataItems = dtDataItems;
                        (MdiChildren[iLoop] as fmFlightGuarantee).RefreshView((MdiChildren[iLoop] as fmFlightGuarantee).FpFlightInfo.ActiveSheetIndex);
                    }
                }
            }
        }

        /// <summary>
        /// ������ʾϯλ
        /// </summary>
        private void SetPosition()
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

                for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
                {
                    if (MdiChildren[iLoop] is fmFlightWatch)
                    {
                        (MdiChildren[iLoop] as fmFlightWatch).MPositionNameBM = m_positionNameBM;
                        (MdiChildren[iLoop] as fmFlightWatch).FlightRefresh();
                        statusBarMain.Panels[0].Text = "��½ϯλ:" + m_positionNameBM.PositionName;
                        statusBarMain.Panels[1].Text = "��½�û�:" + m_LogAccount.UserName;
                    }                   
                }

            }
        }

        /// <summary>
        /// ������
        /// </summary>
        private void FlightWatch()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemWatch' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool blnFind = false;
            for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    MdiChildren[iLoop].WindowState = FormWindowState.Minimized;
                }
                else
                {
                    blnFind = true;
                    MdiChildren[iLoop].WindowState = FormWindowState.Maximized;
                }
            }

            if (blnFind == false)
            {
                fmFlightWatch objfmFlightWatch = new fmFlightWatch(m_LogAccount, dtStandardIntermissionTime, dtDataItems, m_positionNameBM);
                objfmFlightWatch.Show();
                objfmFlightWatch.MdiParent = this;
                objfmFlightWatch.WindowState = FormWindowState.Maximized;
               
            }

            statusBarMain.Panels[0].Text = "��½ϯλ:" + m_positionNameBM.PositionName;
            statusBarMain.Panels[1].Text = "��½�û�:" + m_LogAccount.UserName;
        }

        /// <summary>
        /// ���ౣ��
        /// </summary>
        private void FlightGuarantee()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDataItemGuarantee' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool blnFind = false;
            for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    MdiChildren[iLoop].WindowState = FormWindowState.Minimized;
                }
                else
                {
                    blnFind = true;
                    MdiChildren[iLoop].WindowState = FormWindowState.Maximized;
                    (MdiChildren[iLoop] as fmFlightGuarantee).SetInOutFlightsNum();
                }
            }

            if (blnFind == false)
            {
                fmFlightGuarantee objfmFlightGuarantee = new fmFlightGuarantee(m_LogAccount, dtStandardIntermissionTime, dtDataItems, statusBarMain, m_iAutoAdjust);
                objfmFlightGuarantee.Show();
                objfmFlightGuarantee.MdiParent = this;
                objfmFlightGuarantee.WindowState = FormWindowState.Maximized;
            }
        }

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

        /// <summary>
        /// ϵͳ��ʾ
        /// </summary>
        private void SystemPrompt()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miSystemPrompt' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmSystemPrompt objfmSystemPrompt = new fmSystemPrompt(m_LogAccount, m_iAutoAdjust);
            objfmSystemPrompt.ShowDialog();

            m_iAutoAdjust = objfmSystemPrompt.AutoAdjust;
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        private void DisplayCondition()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miDisplayCondition' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fmDisplayCondition objfmDisplayCondition = new fmDisplayCondition(m_LogAccount);

            if (objfmDisplayCondition.ShowDialog() == DialogResult.OK)
            {
                
                for (int iLoop = 0; iLoop < MdiChildren.Length; iLoop++)
                {
                    if ((MdiChildren[iLoop] is fmFlightWatch))
                    {
                        (MdiChildren[iLoop] as fmFlightWatch).ShowConditionFlights();
                    }
                }
            }
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        private void EditPassword()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miEditPassword' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmEditPassword objfmEditPassword = new fmEditPassword(m_LogAccount);
            objfmEditPassword.ShowDialog();
        }

        /// <summary>
        /// ϯλ����
        /// </summary>
        private void AddPosition()
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

        /// <summary>
        /// ����ƻ�
        /// </summary>
        private void FlightSchedule()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miFlightSchedule' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmFlightSchedule objfmFlightSchedule = new fmFlightSchedule(m_LogAccount);
            objfmFlightSchedule.ShowDialog();
        }

        private void StatisticPax()
        {
            DataRow[] drMenuPurview = dtMenuItemPurview.Select("cnvcMenuItemID = 'miCheckNum' AND cniMenuPurview = 1");
            if (drMenuPurview.Length <= 0)
            {
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fmStatisticPax objfmStatisticPax = new fmStatisticPax(m_LogAccount);
            objfmStatisticPax.ShowDialog();
        }

        #endregion

        /// <summary>
        /// ���ݺ���Ų��Һ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbQuery_Click(object sender, EventArgs e)
        {
            fmQueryFlight objfmQueryFlight = new fmQueryFlight();


            objfmQueryFlight.StartPosition = FormStartPosition.CenterScreen;
            objfmQueryFlight.Text += "�����";

            objfmQueryFlight.ShowDialog();

            if (!objfmQueryFlight.Find)
            {
                return;
            }

            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).FindFlightByFlightNo(objfmQueryFlight.FindContent);
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).FindFlightByFlightNo(objfmQueryFlight.FindContent);
                }
            }
        }

        /// <summary>
        /// �Ŵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbZoomOut_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).ZoomOut();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).ZoomOut();
                }
            }
        }

        /// <summary>
        /// ��С
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbZoomIn_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).ZoomIn();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).ZoomIn();
                }
            }
        }

        /// <summary>
        /// ������̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbExport_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).ExportData();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).ExportData();
                }
            }
        }

        /// <summary>
        /// ��ӡ���ද̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPrint_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).PrintData();
                }
            }
        }

        /// <summary>
        /// ˢ�º��ද̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRefresh_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightWatch)
                {
                    (MdiChildren[iLoop] as fmFlightWatch).FlightRefresh();
                }
                else if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).FlightRefresh();
                }
            }
        }

        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// ��ʾ�û��Ƿ��˳�ϵͳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMDIMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("�Ƿ��˳���վ����ϵͳ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearLogInfo();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

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

        /// <summary>
        /// ÿ����������������˷���һ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerHBT_Tick(object sender, EventArgs e)
        {
            AccountBF accountBF = new AccountBF();
            accountBF.UpdateOnlineUser(m_LogAccount, _iOnlineUserNo);
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ����ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonItem_Click(object sender, EventArgs e)
        {
            fmStandardItem fmStandardItem = new fmStandardItem(m_LogAccount);
            fmStandardItem.ShowDialog();
        }

        private void ͣ��λ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strStationName = "";
            string strAirportName = "";

            StationBF stationBF = new StationBF();
            strStationName = stationBF.GetStationByThreeCode(m_LogAccount.StationThreeCode).Dt.Rows[0]["cnvcStationName"].ToString();
            strAirportName = stationBF.GetStationByThreeCode(m_LogAccount.StationThreeCode).Dt.Rows[0]["cnvcAirportName"].ToString();

            fmGateInfo objfmGateInfo = new fmGateInfo(m_LogAccount, strStationName, strAirportName);
            objfmGateInfo.ShowDialog();
        }

        private void ���ع�����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int iLoop = 0; iLoop < this.MdiChildren.Length; iLoop++)
            {
                if (MdiChildren[iLoop] is fmFlightGuarantee)
                {
                    (MdiChildren[iLoop] as fmFlightGuarantee).BalanceStatistics();
                }
            }
        }
    }
}