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
    public partial class fmMaintenTime : Form
    {
        private MaintenGuaranteeInforBM m_maintenGuaranteeInforBM;
        private ChangeRecordBM m_changeRecordBM;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        public fmMaintenTime(MaintenGuaranteeInforBM maintenGuaranteeInforBM, ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();
            this.m_maintenGuaranteeInforBM = maintenGuaranteeInforBM;
            this.m_changeRecordBM = changeRecordBM;
        }

        public MaintenGuaranteeInforBM MMaintenGuaranteeInforBM
        {
            get { return m_maintenGuaranteeInforBM; }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMaintenTime_Load(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();
            if (rvSF.Result > 0)
            {
                txtNewContent.Text = DateTime.Parse(rvSF.Message).ToString("HHmm");
            }

            this.Text = m_maintenGuaranteeInforBM.ColumnCaption + "(" + m_maintenGuaranteeInforBM.FLTID + ")";
            txtNewContent.SelectAll();
        }

        /// <summary>
        /// 保存用户录入的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNewContent.Text.Length > 0 && txtNewContent.Text.Length != 4)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtNewContent.Text.Length == 4)
            {
                string strTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + txtNewContent.Text.Substring(0, 2) + ":" + txtNewContent.Text.Substring(2, 2) + " :00";
                try
                {
                    DateTime.Parse(strTime);
                }
                catch
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("时间格式不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();
            

            m_maintenGuaranteeInforBM.NewContent = txtNewContent.Text.Trim();
            m_changeRecordBM.ChangeNewContent = txtNewContent.Text.Trim();
            if (rvSF.Result > 0)
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                m_changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            m_changeRecordBM.FOCOperatingTime = "";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();
            
            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(m_maintenGuaranteeInforBM);
            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnNow_Click(object sender, EventArgs e)
        {
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();
            if (rvSF.Result > 0)
            {
                txtNewContent.Text = DateTime.Parse(rvSF.Message).ToString("HHmm");
            }
            txtNewContent.SelectAll();
        }


    }
}