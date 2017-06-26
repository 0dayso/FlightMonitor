using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmLogOn : Form
    {
        #region ����
        private bool m_Login = false;
        /// <summary>
        /// �Ƿ�ɹ���½
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return this.m_Login;
            }
        }

        private int m_UserTypeId = 0;
        /// <summary>
        /// �û�����
        /// </summary>
        public int UserTypeId
        {
            get
            {
                return this.m_UserTypeId;
            }
        }
        #endregion

        /// <summary>
        /// ���캯��
        /// </summary>
        public fmLogOn()
        {
            InitializeComponent();
        }

        #region ��¼
        /// <summary>
        /// �����ȷ������¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //��ʼ����װ�Ľṹ����
            ReturnValueSF rvSF = new ReturnValueSF();

            //ҵ����۲����
            FlightMonitorBF.AccountBF accountBF = new AccountBF();


            //���ȼ���ϴ��Ƿ������˳� 
            for (int iLoop = 1; iLoop <= 100; iLoop++)
            {
                int iLogOff = Convert.ToInt32(ConfigSettings.ReadSetting("LogOFF" + iLoop.ToString()).ToString());
                string strLastLogUserId = ConfigSettings.ReadSetting("LogOnUser" + iLoop.ToString()).ToString();

                //���û�гɹ��˳���Ҫ�����޸ĵ�½�û���
                if (strLastLogUserId == txtUserID.Text.Trim() && iLogOff == 0)
                {
                    rvSF = accountBF.CheckLogOFF(strLastLogUserId);
                    if (rvSF.Result > 0)
                    {
                        ConfigSettings.WriteSetting("LogOFF" + iLoop.ToString(), "1");
                    }
                }
            }

            //��½�û��������Ϣʵ�����  
            FlightMonitorBM.AccountBM accountBM = new AccountBM();
            accountBM.UserId = txtUserID.Text.Trim();
            accountBM.UserPassword = txtPassword.Text;

            //��֤�û���½              
            rvSF = accountBF.LogOn(accountBM);

            //����ɹ����¼��½��Ϣ
            if (rvSF.Result > 0)
            {
                this.m_Login = true;
                accountBM = new AccountBM(rvSF.Dt.Rows[0]);
                accountBM.LogUser += 1;

                //�����û�����ID
                this.m_UserTypeId = accountBM.UserTypeId;
                fmMDIFlightDisp.LogAccount = accountBM;
                accountBF.UpdateLogUser(accountBM);

                int iLogIndex = 1;
                for (int iLoop = 1; iLoop <= 100; iLoop++)
                {
                    if (ConfigSettings.ReadSetting("LogOnUser" + iLoop.ToString()).ToString() == accountBM.UserId || ConfigSettings.ReadSetting("LogOnUser" + iLoop.ToString()).ToString() == "")
                    {
                        iLogIndex = iLoop;
                        break;
                    }
                }
                ConfigSettings.WriteSetting("LogOnUser" + iLogIndex.ToString(), accountBM.UserId);
                ConfigSettings.WriteSetting("LogOFF" + iLogIndex.ToString(), "0");
                ConfigSettings.WriteSetting("LogIndex", iLogIndex.ToString());
                this.Close();
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region �رմ����¼�
        /// <summary>
        /// �رմ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion

        #region ���ش����¼�
        /// <summary>
        /// ���ش����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmLogOn_Load(object sender, EventArgs e)
        {
            string updateUrl = string.Empty;
            string tempUpdatePath = string.Empty;
            XmlFiles updaterXmlFiles = null;
            int availableUpdate = 0;
            bool isRun = false;
            string mainAppExe = "";

            string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
            string serverXmlFile = string.Empty;

            try
            {
                //�ӱ��ض�ȡ���������ļ���Ϣ
                updaterXmlFiles = new XmlFiles(localXmlFile);
            }
            catch
            {
                MessageBox.Show("�����ļ�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //��ȡ��������ַ
            updateUrl = updaterXmlFiles.GetNodeValue("//Url");

            AppUpdater appUpdater = new AppUpdater();
            appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";

            //�����������,���ظ��������ļ�
            try
            {
                tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                appUpdater.DownAutoUpdateFile(tempUpdatePath);
            }
            catch
            {
                MessageBox.Show("�����������ʧ��,������ʱ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;

            }

            //��ȡ�����ļ��б�
            Hashtable htUpdateFile = new Hashtable();

            serverXmlFile = tempUpdatePath + "\\UpdateList.xml";
            if (!File.Exists(serverXmlFile))
            {
                return;
            }

            availableUpdate = appUpdater.CheckForUpdate(serverXmlFile, localXmlFile, out htUpdateFile);
            if (availableUpdate > 0)
            {
                string strPath = Application.StartupPath + "\\AutoUpdate.exe";
                System.Diagnostics.Process.Start(strPath);
                System.Environment.Exit(0);
                //				this.Close();
            }

            string strLogIndex = ConfigSettings.ReadSetting("LogIndex");
            //for (int iLoop = 1; iLoop <= 100; iLoop++)
            //{
            //    if (ConfigSettings.ReadSetting("LogOnUser" + iLoop.ToString()) == txtUserID.Text.Trim())
            //    {
            //        iLogIndex = iLoop;
            //        break;
            //    }
            //}

            txtUserID.Text = ConfigSettings.ReadSetting("LogOnUser" + strLogIndex);
           if (txtUserID.Text.Length > 0)
           {
               txtPassword.TabIndex = 0;
           }
       }
        #endregion

       #region �����¼�
       /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            Validator.KeyDown(sender, e);
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            Validator.KeyDown(sender, e);
        }

        private void txtUserID_Enter(object sender, EventArgs e)
        {
            txtPassword.TabIndex = 1;
            txtUserID.Text = "";
        }
       #endregion
    }
}