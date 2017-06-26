using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmDisplayCondition : Form
    {
        AccountBM m_accountBM;

        public fmDisplayCondition(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }

        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmDisplayCondition_Load(object sender, EventArgs e)
        {
            //�ָ��Ѿ����浽���ݿ��е�ֵ
            if (m_accountBM.DisplayAll == 1)
            {
                rdbAll.Checked = true;
                rdbCondition.Checked = false;

                chkDelayFlight.Enabled = false;
                updownDelayHour.Enabled = false;
                chkDiversionFlight.Enabled = false;
                chkIntermissionFlight.Enabled = false;
                chkTDWNFlight.Enabled = false;
                chkTOFFFlight.Enabled = false;
                chkClosePaxCabin.Enabled = false;
            }
            else
            {
                rdbCondition.Checked = true;
                rdbAll.Checked = false;

                //���󺽰�
                if (m_accountBM.DisplayDelay == 1)
                {
                    chkDelayFlight.Checked = true;
                }
                else
                {
                    chkDelayFlight.Checked = false;
                }

                updownDelayHour.Value = m_accountBM.DelayMinutes;

                //������������
                if (m_accountBM.DisplayDiversion == 1)
                {
                    chkDiversionFlight.Checked = true;
                }
                else
                {
                    chkDiversionFlight.Checked = false;
                }

                //��վʱ�䲻�㺽��
                if (m_accountBM.DisplayIntermission == 1)
                {
                    chkIntermissionFlight.Checked = true;
                }
                else
                {
                    chkIntermissionFlight.Checked = false;
                }

                //û����ض�̬
                if (m_accountBM.DisplayTDWN == 1)
                {
                    chkTDWNFlight.Checked = true;
                }
                else
                {
                    chkTDWNFlight.Checked = false;
                }

                //û����ɶ�̬
                if (m_accountBM.DisplayTOFF == 1)
                {
                    chkTOFFFlight.Checked = true;
                }
                else
                {
                    chkTOFFFlight.Checked = false;
                }

                //û�йز�ʱ��
                if (m_accountBM.DisplayClosePaxCabin == 1)
                {
                    chkClosePaxCabin.Checked = true;
                }
                else
                {
                    chkClosePaxCabin.Checked = false;
                }
            }


        }

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            chkDelayFlight.Enabled = rdbCondition.Checked;
            updownDelayHour.Enabled = rdbCondition.Checked;
            chkDiversionFlight.Enabled = rdbCondition.Checked;
            chkIntermissionFlight.Enabled = rdbCondition.Checked;
            chkTDWNFlight.Enabled = rdbCondition.Checked;
            chkTOFFFlight.Enabled = rdbCondition.Checked;
            chkClosePaxCabin.Enabled = rdbCondition.Checked;

            if (rdbCondition.Checked == true)
            {
                //���󺽰�
                if (m_accountBM.DisplayDelay == 1)
                {
                    chkDelayFlight.Checked = true;
                }
                else
                {
                    chkDelayFlight.Checked = false;
                }

                updownDelayHour.Value = m_accountBM.DelayMinutes;

                //������������
                if (m_accountBM.DisplayDiversion == 1)
                {
                    chkDiversionFlight.Checked = true;
                }
                else
                {
                    chkDiversionFlight.Checked = false;
                }

                //��վʱ�䲻�㺽��
                if (m_accountBM.DisplayIntermission == 1)
                {
                    chkIntermissionFlight.Checked = true;
                }
                else
                {
                    chkIntermissionFlight.Checked = false;
                }

                //û����ض�̬
                if (m_accountBM.DisplayTDWN == 1)
                {
                    chkTDWNFlight.Checked = true;
                }
                else
                {
                    chkTDWNFlight.Checked = false;
                }

                //û����ɶ�̬
                if (m_accountBM.DisplayTOFF == 1)
                {
                    chkTOFFFlight.Checked = true;
                }
                else
                {
                    chkTOFFFlight.Checked = false;
                }

                //û�йز�ʱ��
                if (m_accountBM.DisplayClosePaxCabin == 1)
                {
                    chkClosePaxCabin.Checked = true;
                }
                else
                {
                    chkClosePaxCabin.Checked = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //ҵ����۲㷽��
            AccountBF accountBF = new AccountBF();

            if (rdbAll.Checked == true)
            {
                m_accountBM.DisplayAll = 1;
            }
            else
            {
                m_accountBM.DisplayAll = 0;
            }
            m_accountBM.DisplayDelay = chkDelayFlight.Checked ? 1 : 0;
            m_accountBM.DelayMinutes = Convert.ToInt32(updownDelayHour.Value);
            m_accountBM.DisplayDiversion = chkDiversionFlight.Checked ? 1 : 0;
            m_accountBM.DisplayIntermission = chkIntermissionFlight.Checked ? 1 : 0;
            m_accountBM.DisplayTDWN = chkTDWNFlight.Checked ? 1 : 0;
            m_accountBM.DisplayTOFF = chkTOFFFlight.Checked ? 1 : 0;
            m_accountBM.DisplayClosePaxCabin = chkClosePaxCabin.Checked ? 1 : 0;

            rvSF = accountBF.UpdateAllInfo(m_accountBM, new ArrayList());

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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