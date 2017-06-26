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
        #region 属性
        private bool m_Login = false;
        /// <summary>
        /// 是否成功登陆
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
        /// 用户类型
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
        /// 构造函数
        /// </summary>
        public fmLogOn()
        {
            InitializeComponent();
        }

        #region 登录
        /// <summary>
        /// 点击“确定”登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //初始化封装的结构处理
            ReturnValueSF rvSF = new ReturnValueSF();

            //业务外观层对象
            FlightMonitorBF.AccountBF accountBF = new AccountBF();


            //首先检查上次是否正常退出 
            for (int iLoop = 1; iLoop <= 100; iLoop++)
            {
                int iLogOff = Convert.ToInt32(ConfigSettings.ReadSetting("LogOFF" + iLoop.ToString()).ToString());
                string strLastLogUserId = ConfigSettings.ReadSetting("LogOnUser" + iLoop.ToString()).ToString();

                //如果没有成功退出需要首先修改登陆用户数
                if (strLastLogUserId == txtUserID.Text.Trim() && iLogOff == 0)
                {
                    rvSF = accountBF.CheckLogOFF(strLastLogUserId);
                    if (rvSF.Result > 0)
                    {
                        ConfigSettings.WriteSetting("LogOFF" + iLoop.ToString(), "1");
                    }
                }
            }

            //登陆用户输入的信息实体对象  
            FlightMonitorBM.AccountBM accountBM = new AccountBM();
            accountBM.UserId = txtUserID.Text.Trim();
            accountBM.UserPassword = txtPassword.Text;

            //验证用户登陆              
            rvSF = accountBF.LogOn(accountBM);

            //如果成功则记录登陆信息
            if (rvSF.Result > 0)
            {
                this.m_Login = true;
                accountBM = new AccountBM(rvSF.Dt.Rows[0]);
                accountBM.LogUser += 1;

                //返回用户类型ID
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
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 关闭窗体事件
        /// <summary>
        /// 关闭窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion

        #region 加载窗体事件
        /// <summary>
        /// 加载窗体事件
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
                //从本地读取更新配置文件信息
                updaterXmlFiles = new XmlFiles(localXmlFile);
            }
            catch
            {
                MessageBox.Show("配置文件出错!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //获取服务器地址
            updateUrl = updaterXmlFiles.GetNodeValue("//Url");

            AppUpdater appUpdater = new AppUpdater();
            appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";

            //与服务器连接,下载更新配置文件
            try
            {
                tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                appUpdater.DownAutoUpdateFile(tempUpdatePath);
            }
            catch
            {
                MessageBox.Show("与服务器连接失败,操作超时!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;

            }

            //获取更新文件列表
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

       #region 按键事件
       /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            Validator.KeyDown(sender, e);
        }

        /// <summary>
        /// 按键事件
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