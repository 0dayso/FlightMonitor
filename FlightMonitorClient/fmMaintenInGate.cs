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
    public partial class fmMaintenInGate : Form
    {
        private MaintenGuaranteeInforBM m_inMaintenGuaranteeInforBM;
        private ChangeRecordBM m_inChangeRecordBM;

        private MaintenGuaranteeInforBM m_outMaintenGuaranteeInforBM;
        private ChangeRecordBM m_outChangeRecordBM;

        private int m_iOut;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inMaintenGuaranteeInforBM"></param>
        /// <param name="outMaintenGuaranteeInforBM"></param>
        /// <param name="inChangeRecordBM"></param>
        /// <param name="outChangeRecordBM"></param>
        /// <param name="iOut"></param>
        public fmMaintenInGate(MaintenGuaranteeInforBM inMaintenGuaranteeInforBM, MaintenGuaranteeInforBM outMaintenGuaranteeInforBM, ChangeRecordBM inChangeRecordBM, ChangeRecordBM outChangeRecordBM ,int iOut)
        {
            InitializeComponent();

            this.m_inMaintenGuaranteeInforBM = inMaintenGuaranteeInforBM;
            this.m_inChangeRecordBM = inChangeRecordBM;

            this.m_outMaintenGuaranteeInforBM = outMaintenGuaranteeInforBM;
            
            this.m_outChangeRecordBM = outChangeRecordBM;
            

            this.m_iOut = iOut;
        }

        /// <summary>
        /// 获取用户维护的值
        /// </summary>
        public MaintenGuaranteeInforBM MMaintenGuaranteeInforBM
        {
            get { return m_inMaintenGuaranteeInforBM; }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMiantenInGate_Load(object sender, EventArgs e)
        {
            txtNewContent.MaxLength = m_inMaintenGuaranteeInforBM.FieldLength;
            txtNewContent.Text = m_inMaintenGuaranteeInforBM.OldContent;
            this.Text = m_inMaintenGuaranteeInforBM.ColumnCaption + "(" + m_inMaintenGuaranteeInforBM.FLTID + ")";
            txtNewContent.SelectAll();

            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断是否含有中文
            LinYong.PublicService.PublicService PublicServiceObj = new LinYong.PublicService.PublicService()  ;
            if (PublicServiceObj.ContainChinese(txtNewContent.Text.Trim()))
            {
                MessageBox.Show("停机位信息不能含有中文！", "提示" );
                this.DialogResult = DialogResult.None;
                return;
            }


            ReturnValueSF rvSF = new ReturnValueSF();
            
            //获取服务器时间
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSFTime = serverDateTimeBF.GetServerDateTime();

            //进港机位
            m_inMaintenGuaranteeInforBM.NewContent = txtNewContent.Text.Trim();
            m_inChangeRecordBM.ChangeNewContent = txtNewContent.Text.Trim();
            m_inChangeRecordBM.ChangeNewContent = txtNewContent.Text.Trim();
            if (rvSFTime.Result > 0)
            {
                m_inChangeRecordBM.LocalOperatingTime = DateTime.Parse(rvSFTime.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                m_inChangeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            m_inChangeRecordBM.FOCOperatingTime = "";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            if (!(m_inMaintenGuaranteeInforBM.DATOP == m_outMaintenGuaranteeInforBM.DATOP && m_inMaintenGuaranteeInforBM.FLTID == m_outMaintenGuaranteeInforBM.FLTID && m_inMaintenGuaranteeInforBM.LEGNO == m_outMaintenGuaranteeInforBM.LEGNO && m_inMaintenGuaranteeInforBM.AC == m_outMaintenGuaranteeInforBM.AC))
            {
                rvSF = guaranteeInforBF.UpdateGuaranteeInfor(m_inMaintenGuaranteeInforBM);
            }

            string gate = "UNK";
            if (txtNewContent.Text.Trim() != "")
            {
                gate = txtNewContent.Text.Trim();
            }

            
            string strInGate = m_inChangeRecordBM.OldFLTID.Replace(" ", "");
            //strInGate += "/" + DateTime.Parse(m_inChangeRecordBM.STD).ToUniversalTime().ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
            strInGate += "/" + DateTime.Parse(m_inChangeRecordBM.OldDATOP).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
            strInGate += "%%";

            strInGate += m_inChangeRecordBM.OldDepSTN + DateTime.Parse(m_inChangeRecordBM.STD).ToUniversalTime().ToString("HHmm") + " ";
            strInGate += m_inChangeRecordBM.OldArrSTN + DateTime.Parse(m_inChangeRecordBM.STA).ToUniversalTime().ToString("HHmm") + "%%";

            strInGate += m_inChangeRecordBM.OldDepSTN + "/UNK" + " " + m_inChangeRecordBM.OldArrSTN + "/" + gate + "%%";

            #region modified by LinYong in 20151119
            //FocService.FleetWatch objFocService = new FocService.FleetWatch();
            FocService.FocService objFocService = new FocService.FocService();
            #endregion modified by LinYong in 20151119

            if (!(m_inMaintenGuaranteeInforBM.DATOP == m_outMaintenGuaranteeInforBM.DATOP && m_inMaintenGuaranteeInforBM.FLTID == m_outMaintenGuaranteeInforBM.FLTID && m_inMaintenGuaranteeInforBM.LEGNO == m_outMaintenGuaranteeInforBM.LEGNO && m_inMaintenGuaranteeInforBM.AC == m_outMaintenGuaranteeInforBM.AC))
            {
                objFocService.InsertGate(strInGate);
                rvSF = changeRecordBF.Insert(m_inChangeRecordBM);
            }
            
                
           

            //出港机位
            if (m_iOut == 1)
            {
                if (m_inMaintenGuaranteeInforBM.DATOP == m_outMaintenGuaranteeInforBM.DATOP && m_inMaintenGuaranteeInforBM.FLTID == m_outMaintenGuaranteeInforBM.FLTID && m_inMaintenGuaranteeInforBM.LEGNO == m_outMaintenGuaranteeInforBM.LEGNO && m_inMaintenGuaranteeInforBM.AC == m_outMaintenGuaranteeInforBM.AC)
                {
                    m_outChangeRecordBM.Refresh = 1;
                }

                m_outMaintenGuaranteeInforBM.NewContent = txtNewContent.Text.Trim();
                m_outChangeRecordBM.ChangeNewContent = txtNewContent.Text.Trim();
                m_outChangeRecordBM.ChangeNewContent = txtNewContent.Text.Trim();
                if (rvSFTime.Result > 0)
                {
                    m_outChangeRecordBM.LocalOperatingTime = DateTime.Parse(rvSFTime.Message).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    m_outChangeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                m_outChangeRecordBM.FOCOperatingTime = "";

                
                string strOutGate = m_outChangeRecordBM.OldFLTID.Replace(" ", "");
                //strOutGate += "/" + DateTime.Parse(m_outChangeRecordBM.STD).ToUniversalTime().ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
                strOutGate += "/" + DateTime.Parse(m_outChangeRecordBM.OldDATOP).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
                strOutGate += "%%";

                strOutGate += m_outChangeRecordBM.OldDepSTN + DateTime.Parse(m_outChangeRecordBM.STD).ToUniversalTime().ToString("HHmm") + " ";
                strOutGate += m_outChangeRecordBM.OldArrSTN + DateTime.Parse(m_outChangeRecordBM.STA).ToUniversalTime().ToString("HHmm") + "%%";

                strOutGate += m_outChangeRecordBM.OldDepSTN + "/" + gate + " " + m_outChangeRecordBM.OldArrSTN + "/UNK%%";

                
                objFocService.InsertGate(strOutGate);

                rvSF = guaranteeInforBF.UpdateGuaranteeInfor(m_outMaintenGuaranteeInforBM);
                if (rvSF.Result > 0)
                {
                    rvSF = changeRecordBF.Insert(m_outChangeRecordBM);
                }
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

        private void txtNewContent_TextChanged(object sender, EventArgs e)
        {
            if (txtNewContent.Text != m_inMaintenGuaranteeInforBM.OldContent)
            {
                btnSave.Enabled = true;
            }
            else
                btnSave.Enabled = false;
        }



    }
}