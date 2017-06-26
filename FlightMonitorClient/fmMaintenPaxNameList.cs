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
    public partial class fmMaintenPaxNameList : Form
    {
        private ChangeLegsBM m_changeLegsBM;
        private ChangeRecordBM m_changeRecordBM = new ChangeRecordBM();

        private PaxNameListBM m_paxNameListBM = new PaxNameListBM();
        private PaxNameListBM m_oldPaxNameListBM = new PaxNameListBM();

        private string m_strNewContent;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="changeRecordBM"></param>
        public fmMaintenPaxNameList(ChangeLegsBM changeLegsBM, ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();
            this.m_changeLegsBM = changeLegsBM;
            this.m_changeRecordBM = changeRecordBM;
        }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewContent
        {
            get { return m_strNewContent; }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMaintenCheckPaxNameList_Load(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ReturnValueSF rvSF = guaranteeInforBF.GetPaxNameList(m_changeLegsBM);
            if (rvSF.Result > 0)
            {
                if (rvSF.Dt.Rows.Count > 0)
                {
                    m_oldPaxNameListBM = new PaxNameListBM(rvSF.Dt.Rows[0]);
                    m_paxNameListBM = m_oldPaxNameListBM;
                }
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            if (m_oldPaxNameListBM.IsAutoSaveCheckPaxNameList == 0 || m_oldPaxNameListBM.PaxNameList.Trim() == "")
            {
                m_paxNameListBM.PaxNameList = GetPaxNameList(m_changeLegsBM);                
            }

            txtPaxNameList.Text = m_paxNameListBM.PaxNameList;
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReconnect_Click(object sender, EventArgs e)
        {
            PaxService.PaxService objPaxService = new PaxService.PaxService();
            if (!objPaxService.PaxCheckConnect())
            {
                btnReconnect.Enabled = true;
                MessageBox.Show("请重新连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                m_paxNameListBM.PaxNameList = GetPaxNameList(m_changeLegsBM);

                txtPaxNameList.Text = m_paxNameListBM.PaxNameList;
            }
        }

        /// <summary>
        /// 保存到本地数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();


            if (m_changeLegsBM.STATUS == "DEP" || m_changeLegsBM.STATUS == "ATA")
            {
                m_paxNameListBM.IsAutoSaveCheckPaxNameList = 1;
            }


            ReturnValueSF rvSF = guaranteeInforBF.UpdatePaxNameList(m_paxNameListBM);

            if (rvSF.Result > 0)
            {
                //获取服务器时间
                ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
                rvSF = serverDateTimeBF.GetServerDateTime();

                if (rvSF.Result > 0)
                {
                    m_changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    m_changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                m_changeRecordBM.FOCOperatingTime = "";

                m_changeRecordBM.ChangeNewContent = txtPaxNameList.Text;

                ChangeRecordBF changeRecordBF = new ChangeRecordBF();
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
            }

            m_strNewContent = txtPaxNameList.Text;

            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.Close();
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
        /// 通过服务获取旅客名单
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        private string GetPaxNameList(ChangeLegsBM changeLegsBM)
        {
            PaxService.PaxService objPaxService = new PaxService.PaxService();
            string strFlightNo = changeLegsBM.CKIFlightNo.Replace(" ", "").ToUpper();


            string strCommnadText = "PD:";
            strCommnadText += strFlightNo + "/";
            strCommnadText += DateTime.Parse(changeLegsBM.CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " * ";
            strCommnadText += changeLegsBM.DEPSTN + changeLegsBM.ARRSTN + ",ACC";


            //string strResult = objPaxService.PaxCheckNum(strCommnadText);
            string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");
            strResult = strResult.Replace("\n", "\r\n");


            return strResult;
        }
    }
}