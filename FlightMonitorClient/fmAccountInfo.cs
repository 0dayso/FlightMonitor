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
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// ����ӻ��޸��û���Ϣʱ��ʾ�Ĵ���
    /// </summary>
    public partial class fmAccountInfo : Form
    {
        //���巵��ֵ
        private ReturnValueSF m_rvSF = new ReturnValueSF();      
   
        //�༭�û�ʱ������ʺ�ʵ�����
        private AccountBM m_accountBM;

        //��ʶ����ӻ����޸�
        private int m_iUpdate = 0;

        /// <summary>
        /// ���캯��
        /// </summary>
        public fmAccountInfo()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        public fmAccountInfo(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
            this.m_iUpdate = 1;        

        }

        /// <summary>
        /// ����ֵ�����ڸ��������
        /// </summary>
        public ReturnValueSF RvSF
        {
            get { return m_rvSF; }
        }

        
        /// <summary>
        /// ȷ����ťclick�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //�ʺŲ���Ϊ��
            if (!Validator.CheckNull(txtUserID.Text, "�ʺŲ���Ϊ�գ�"))
            {
                return;
            }

            //ˢ��ʱ��ֻ��Ϊ����
            if(!Validator.IsNumber(txtRefreshInterval.Text, "ˢ��ʱ��ֻ��Ϊ���֣�"))
            {
                return;
            }

            //����½��ֻ��Ϊ����
            if (!Validator.IsNumber(txtMaxUser.Text, "����½��ֻ��Ϊ���֣�"))
            {
                return;
            }

            //����ҵ��ʵ�����
            FlightMonitorBM.AccountBM accountBM = new AccountBM();
            accountBM.UserId = txtUserID.Text.Trim();
            accountBM.UserName = txtUserName.Text.Trim();
            accountBM.UserTypeId = cmbUserType.SelectedIndex + 1;
            
            accountBM.StationThreeCode = cmbStationThreeCode.SelectedValue.ToString();
            accountBM.UserDepartment = txtDepartMent.Text.Trim();
            accountBM.RefreshInterval = Convert.ToInt32(txtRefreshInterval.Text);
            if (m_iUpdate == 0)
            {
                accountBM.SplashAutoStop = 1;
                accountBM.SplashSeconds = 50;
                accountBM.SoundType = 0;
                accountBM.StartGuarantee = 1;
                accountBM.GuaranteeMinutes = 60;
                accountBM.Boarding = 0;
                accountBM.BoardingMinutes = 30;
                accountBM.MCCReady = 0;
                accountBM.MCCReadyMinutes = 15;
                accountBM.MCCRelease = 0;
                accountBM.MCCReleasMinutes = 15;
                accountBM.TDWNPromt = 1;
                accountBM.TDWNMinutes = 5;
                accountBM.TOFFPromt = 1;
                accountBM.TOFFMinutes = 0;
                accountBM.IntermissionPrompt = 1;
                accountBM.IntermissionMinutes = 5;
                accountBM.ClosePaxCabinPromt = 1;
                accountBM.DisplayAll = 1;
                accountBM.DisplayDelay = 1;
                accountBM.DelayMinutes = 120;
                accountBM.DisplayDiversion = 1;
                accountBM.DisplayIntermission = 1;
                accountBM.DisplayTDWN = 1;
                accountBM.DisplayTOFF = 1;
                accountBM.DisplayClosePaxCabin = 1;
                accountBM.UserPassword = "111111";
            }
            else
            {
                accountBM.UserPassword = m_accountBM.UserPassword;
                accountBM.SplashAutoStop = m_accountBM.SplashAutoStop;
                accountBM.SplashSeconds = m_accountBM.SplashSeconds;
                accountBM.SoundType = m_accountBM.SoundType;
                accountBM.StartGuarantee = m_accountBM.StartGuarantee;
                accountBM.GuaranteeMinutes = m_accountBM.GuaranteeMinutes;
                accountBM.Boarding = m_accountBM.Boarding;
                accountBM.BoardingMinutes = m_accountBM.BoardingMinutes;
                accountBM.MCCReady = m_accountBM.MCCReady;
                accountBM.MCCReadyMinutes = m_accountBM.MCCReadyMinutes;
                accountBM.MCCRelease = m_accountBM.MCCRelease;
                accountBM.MCCReleasMinutes = m_accountBM.MCCReleasMinutes;
                accountBM.TDWNPromt = m_accountBM.TDWNPromt;
                accountBM.TDWNMinutes = m_accountBM.TDWNMinutes;
                accountBM.TOFFPromt = m_accountBM.TOFFPromt;
                accountBM.TOFFPromt = m_accountBM.TOFFPromt;
                accountBM.IntermissionPrompt = m_accountBM.IntermissionPrompt;
                accountBM.IntermissionMinutes = m_accountBM.IntermissionMinutes;
                accountBM.ClosePaxCabinPromt = m_accountBM.ClosePaxCabinPromt;
                accountBM.DisplayAll = m_accountBM.DisplayAll;
                accountBM.DisplayDelay = m_accountBM.DisplayDelay;
                accountBM.DelayMinutes = m_accountBM.DelayMinutes;
                accountBM.DisplayDiversion = m_accountBM.DisplayDiversion;
                accountBM.DisplayIntermission = m_accountBM.DisplayIntermission;
                accountBM.DisplayTDWN = m_accountBM.DisplayTDWN;
                accountBM.DisplayTOFF = m_accountBM.DisplayTOFF;
                accountBM.DisplayClosePaxCabin = m_accountBM.DisplayClosePaxCabin;
            }

            accountBM.MaxUser = Convert.ToInt32(txtMaxUser.Text);

            IList ilDataItemPurview = GetInitialDataItemPurview(accountBM);
            
            //����ҵ����۲㷽��
            FlightMonitorBF.AccountBF accountBF = new AccountBF();

            //���
            if (m_iUpdate == 0)
            {
                m_rvSF = accountBF.Insert(accountBM, ilDataItemPurview);
            }
            else //����
            {
                m_rvSF = accountBF.UpdateAllInfo(accountBM, ilDataItemPurview);
            }

            if (m_rvSF.Result < 0)
            {
                MessageBox.Show(m_rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmAccountInfo_Load(object sender, EventArgs e)
        {
            Station_Bind();           
            cmbUserType.SelectedIndex = 1;
            this.ActiveControl = txtUserID;

            if (m_iUpdate == 1)
            {
                //��ʾ�û�Ҫ�޸ĵ��û���Ϣ
                txtUserID.Text = m_accountBM.UserId;
                txtUserName.Text = m_accountBM.UserName;
                txtDepartMent.Text = m_accountBM.UserDepartment;
                txtRefreshInterval.Text = m_accountBM.RefreshInterval.ToString();
                SetSelectedStation(m_accountBM);
                cmbUserType.Text = m_accountBM.UserTypeName;
                txtMaxUser.Text = m_accountBM.MaxUser.ToString();
            }
        }

        /// <summary>
        /// �󶨺�վ��Ϣ
        /// </summary>
        private void Station_Bind()
        {
            //������ʾ����
            cmbStationThreeCode.DisplayMember = "cnvcAirportName";
            cmbStationThreeCode.ValueMember = "cncThreeCode";
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            //����ҵ����۲㷽��
            FlightMonitorBF.StationBF stationBF = new StationBF();
            rvSF = stationBF.GetAllStation();

            if (rvSF.Result > 0)
            {
                //���һ������                
                cmbStationThreeCode.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// ȡ����ťclick�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {        
            this.Close();
        }             

        /// <summary>
        /// �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            Validator.KeyDown(sender, e);
        }

        /// <summary>
        /// ���ú�վ��ʾ����
        /// </summary>
        /// <param name="accountBM"></param>
        private void SetSelectedStation(FlightMonitorBM.AccountBM accountBM)
        {
            for (int iLoop = 0; iLoop < cmbStationThreeCode.Items.Count; iLoop++)
            {
                DataRowView rowItem = (DataRowView)cmbStationThreeCode.Items[iLoop];

                if (rowItem["cncThreeCode"].ToString() == accountBM.StationThreeCode)
                {
                    cmbStationThreeCode.SelectedIndex = iLoop;
                    break;
                }
            }
        }

        /// <summary>
        /// ��ȡ��ʼ��Ȩ���б�
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>��ʱ������Ȩ���б�</returns>
        private IList GetInitialDataItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            IList ilDataItemPurview = new ArrayList();
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvDataItems = dataItemPurviewBF.GetDataItems();

            int iRowIndex = 0;
            foreach (DataRow rowItem in rvDataItems.Dt.Rows)
            {
                FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem, DataItemPurviewBM.DataType.ITEM);
                dataItemPurviewBM.DataItemPurview = 1;
                dataItemPurviewBM.DataItemVisible = 1;
                dataItemPurviewBM.ViewIndex = iRowIndex;
                dataItemPurviewBM.SplashPromptItem = 1;
                dataItemPurviewBM.SoundPromptItem = 1;
                dataItemPurviewBM.UserID = accountBM.UserId;
                ilDataItemPurview.Add(dataItemPurviewBM);
                iRowIndex++;
            }
            return ilDataItemPurview;
        }

       
    }
}