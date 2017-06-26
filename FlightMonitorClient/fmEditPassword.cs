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
    public partial class fmEditPassword : Form
    {
        private AccountBM m_accountBM;

        public fmEditPassword(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }

        private void fmEditPassword_Load(object sender, EventArgs e)
        {
            txtUserID.Text = m_accountBM.UserId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_accountBM.UserPassword != txtOldPassword.Text)
            {
                MessageBox.Show("旧密码不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtNewPassword.Text != txtConformPassword.Text)
            {
                MessageBox.Show("新密码和确认密码不符！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            m_accountBM.UserPassword = txtNewPassword.Text;

            AccountBF accountBF = new AccountBF();
            ReturnValueSF rvSF = accountBF.UpdatePassword(m_accountBM);

            if (rvSF.Result < 0)
            {
                m_accountBM.UserPassword = txtOldPassword.Text;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}