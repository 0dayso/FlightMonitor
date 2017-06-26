using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
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
            //��½�û��������Ϣʵ�����  
            FlightMonitorBM.AccountBM accountBM = new AccountBM();
            accountBM.UserId = txtUserID.Text.Trim();
            accountBM.UserPassword = txtPassword.Text;
            //��֤�û���½
            //string LogOnType = "";
            //if (rbDomain.Checked)
            //    LogOnType = "���½";
            //else
            //    LogOnType = "����";

            //rvSF = accountBF.LogOn(accountBM, LogOnType);
            rvSF = accountBF.LogOn(accountBM);

            //����ɹ����¼��½��Ϣ
            if (rvSF.Result > 0)
            {
                //
                accountBM = new AccountBM(rvSF.Dt.Rows[0]);

                //��ȡԶ�̶���
                //����������
                ReturnValueSF returnValueSF = null;
                AgentServiceBF.AgentServiceBF agentServiceBF = new AgentServiceBF.AgentServiceBF();
                if ((accountBM.AgentIP != "") && (accountBM.AgentPort != ""))
                    returnValueSF = agentServiceBF.SetRemotingObject(accountBM.AgentIP, accountBM.AgentPort);
                else
                    returnValueSF = agentServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                    throw new Exception("�����������ȡʧ�ܣ������µ�¼��" + System.Environment.NewLine + returnValueSF.Message);
                //����������������
                returnValueSF = null;
                TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
                returnValueSF = taskBookServiceBF.SetRemotingObject();
                if (returnValueSF.Result < 0)
                    throw new Exception("�����������������ȡʧ�ܣ������µ�¼��" + System.Environment.NewLine + returnValueSF.Message);

                //
                this.m_Login = true;
                //��ȡ�ͻ���IP��ַ
                string strHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] ipAddr = ipEntry.AddressList;
                accountBM.IPAddress = ipAddr[0].ToString();
                //��¼�û���
                accountBM.LogUser = rvSF.Result;
                //��¼�û���Ϣ
                int iOnlineUserNo = accountBF.InsertOnlineUser(accountBM);
                accountBF.UpdateLogUser(accountBM);
                ConfigSettings.WriteSetting("LastLogOnUser", accountBM.UserId);
                //�����û�����ID
                this.m_UserTypeId = accountBM.UserTypeId;
                fmMDIMain.LogAccount = accountBM;
                fmMDIMain.OnlineUserNo = iOnlineUserNo;
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
            }

            //string strLogIndex = ConfigSettings.ReadSetting("LogIndex");
            //txtUserID.Text = ConfigSettings.ReadSetting("LogOnUser" + strLogIndex);
            txtUserID.Text = ConfigSettings.ReadSetting("LastLogOnUser");
            //rbDomain_CheckedChanged(sender, e); //Ĭ��ʹ�����½��ʽ

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

        private void rbDomain_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //�� HNANET ���û���ʹ�� ���� ��ʽ ��½
                if (Environment.UserDomainName.ToUpper() != "HNANET")
                {
                    MessageBox.Show("Ŀǰ�ĵ�½������ HNANET �򣬽�ʹ�� ���� ��ʽ��½��");
                    rbNotDomain.Checked = true;
                    return;
                }

                //HNANET ���û�
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;

                txtUserID.Text = Environment.UserName;
                txtPassword.Text = "";
            }
            catch (Exception ex)
            {
                txtUserID.Enabled = true;
                txtPassword.Enabled = true;

                rbNotDomain.Checked = true;
                txtUserID.Text = ConfigSettings.ReadSetting("LastLogOnUser");
            }

        }

        private void rbNotDomain_CheckedChanged(object sender, EventArgs e)
        {
            txtUserID.Enabled = true;
            txtPassword.Enabled = true;

        }
    }
}