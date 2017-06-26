using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorClient.PaxService;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;



namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmMaintenCheckPax : Form
    {
        private ChangeLegsBM m_changeLegsBM;
        private ChangeRecordBM m_changeRecordBM = new ChangeRecordBM();

        private CheckPaxBM m_checkPaxBM = new CheckPaxBM();
        private CheckPaxBM m_oldCheckPaxBM = new CheckPaxBM();

        private string m_strNewContent;

        public fmMaintenCheckPax(ChangeLegsBM changeLegsBM, ChangeRecordBM changeRecordBM)
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
        private void fmCheckPax_Load(object sender, EventArgs e)
        {           
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ReturnValueSF rvSF = guaranteeInforBF.GetCheckInfor(m_changeLegsBM);
            if (rvSF.Result > 0)
            {
                if (rvSF.Dt.Rows.Count > 0)
                {
                    m_oldCheckPaxBM = new CheckPaxBM(rvSF.Dt.Rows[0]);
                    m_checkPaxBM = m_oldCheckPaxBM;
                }
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            if (m_oldCheckPaxBM.IsAutoSaveCheckPaxInfo == 0 || m_oldCheckPaxBM.CheckInfo.Trim() == "")
            {
                m_checkPaxBM = GetCheckPaxInfo(m_changeLegsBM);
                m_checkPaxBM.AscendingPaxNum = GetAscendingPaxNum(m_changeLegsBM);
            }


            txtCheckInfo.Text = m_checkPaxBM.CheckInfo;
            txtBookNum.Text = m_checkPaxBM.BookNum;
            txtCheckNum.Text = m_checkPaxBM.CheckNum.ToString();
            txtXCRNum.Text = m_checkPaxBM.XCRNum.ToString();
            txtAdult.Text = m_checkPaxBM.AdultNum.ToString();
            txtChild.Text = m_checkPaxBM.ChildNum.ToString();
            txtInfant.Text = m_checkPaxBM.InfantNum.ToString();
            txtFirstClass.Text = m_checkPaxBM.FirstClassNum.ToString();
            txtOfficialClass.Text = m_checkPaxBM.OfficialClassNum.ToString();
            txtTouristClass.Text = m_checkPaxBM.TouristClassNum.ToString();
            txtAscendingNum.Text = m_checkPaxBM.AscendingPaxNum.ToString();
            txtBagNum.Text = m_checkPaxBM.BaggageNum.ToString();
            txtBagWeight.Text = m_checkPaxBM.BaggageWeight.ToString();
        }


        /// <summary>
        /// 获取某航班的值机旅客信息
        /// </summary>        
        public CheckPaxBM GetCheckPaxInfo(ChangeLegsBM changeLegsBM)
        {
       
            PaxService.PaxService objPaxService = new PaxService.PaxService();
            string strFlightNo = changeLegsBM.CKIFlightNo.Replace(" ", "").ToUpper();           

            string strCommnadText = "SY ";
            strCommnadText += strFlightNo + "/";
            strCommnadText += DateTime.Parse(changeLegsBM.CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "/";
            strCommnadText += changeLegsBM.CityDEPSTN + ",Z";

            //string strResult = objPaxService.PaxCheckNum(strCommnadText);
            string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");

            if (strResult.Length < 30 || strResult == "Disconncted,Please connect again")
            {
                return new CheckPaxBM();
            }
            else
            {
                return GetCheckPaxInfoFromTheText(strResult, changeLegsBM.CityDEPSTN, changeLegsBM.CityARRSTN);
            }
        }


        /// <summary>
        /// 值机信息分析函数
        /// </summary>
        /// <param name="result"></param>
        /// <param name="depIATACode"></param>
        /// <param name="arrIATACode"></param>
        /// <returns></returns>
        private CheckPaxBM GetCheckPaxInfoFromTheText(string result, string depIATACode, string arrIATACode)
        {
            CheckPaxBM checkPaxBM = new CheckPaxBM();
            string strResult = result;
            string[] strArrResult = strResult.Split('\n');

            if (strArrResult.Length < 3)
            {
                checkPaxBM = new CheckPaxBM();                
                return checkPaxBM;
            }

            int iBegin = strResult.IndexOf("ZONES");
            int iEnd = 0;
            //有合计项
            if (strResult.IndexOf("TOTALS", iBegin) >= 0)
            {
                iBegin = strResult.IndexOf("TOTALS", iBegin) + 7;
            }
            else
            {
                iBegin = strResult.IndexOf(depIATACode + arrIATACode, iBegin) + 6;
            }
            strResult = strResult.Substring(iBegin);


            //各等级订座旅客数
            string[] strArrBookNum;           //各等级订座旅客数
            iBegin = strResult.IndexOf(" R") + 2;
            if (iBegin < 0)
            {
                checkPaxBM = new CheckPaxBM();
                return checkPaxBM;
            }
            iEnd = strResult.IndexOf(" ", iBegin) - 1;
            string strBookNum = strResult.Substring(iBegin, iEnd - iBegin + 1);
            strArrBookNum = strBookNum.Split('/');

            //各等级值机旅客数
            string[] strArrCheckNum;           //各等级值机旅客数
            iBegin = strResult.IndexOf(" C", iBegin) + 2;
            if (iBegin < 0)
            {
                checkPaxBM = new CheckPaxBM();
                return checkPaxBM;
            }
            iEnd = strResult.IndexOf(" ", iBegin) - 1;
            string strCheckNum = strResult.Substring(iBegin, iEnd - iBegin + 1);
            strArrCheckNum = strCheckNum.Split('/');

            //行李件数和重量
            strResult = strResult.Substring(iEnd + 1).Trim();
            iBegin = strResult.IndexOf("B") + 1;
            string strBagNum = strResult.Substring(iBegin, 4);
            string strBagWeight = strResult.Substring(iBegin + 5, 6);
            strResult = strResult.Substring(iBegin + 11);

            //各到达站婴儿数
            string strInfantNum = "0";
            iBegin = strResult.IndexOf(" I");
            if (iBegin >= 0)
            {
                strInfantNum = strResult.Substring(iBegin + 2, 2);
                try
                {
                    strInfantNum = Convert.ToInt32(strInfantNum).ToString();
                }
                catch
                {
                    strInfantNum = "0";
                }
            }

            //各到达站儿童数	
            string strChildNum = "0";
            iBegin = strResult.IndexOf(" CHD");
            if (iBegin >= 0)
            {
                strChildNum = strResult.Substring(iBegin + 4, 3);
                //				strCheckNum = Convert.ToInt32(strChildNum).ToString();
            }

            //各等级加机组人数
            string[] strArrXCRGradeNum = new string[strArrCheckNum.Length];
            if (strResult.IndexOf(" XCR") >= 0)
            {
                iBegin = strResult.IndexOf(" XCR") + 4;
                iEnd = strResult.IndexOf(" ", iBegin) - 1;

                string strXCRGradeNum = strResult.Substring(iBegin, iEnd - iBegin + 1);
                strXCRGradeNum = strXCRGradeNum.Replace("\n", "");
                strArrXCRGradeNum = strXCRGradeNum.Split('/');
            }
            else
            {
                for (int iLoop = 0; iLoop < strArrCheckNum.Length; iLoop++)
                {
                    strArrXCRGradeNum[iLoop] = "0";
                }
            }

            string strAllCheckNum;
            string strAllBookNum;
            string strXCRNum;
            string strAdultNum;
            if (strArrCheckNum.Length == 1)
            {
                strAllBookNum = Convert.ToInt32(strArrBookNum[0]).ToString();
                strAllCheckNum = Convert.ToInt32(strArrCheckNum[0]).ToString();
                strAdultNum = Convert.ToString(int.Parse(strCheckNum) - int.Parse(strChildNum));      

                checkPaxBM.DATOP = m_changeLegsBM.DATOP;
                checkPaxBM.FLTID = m_changeLegsBM.FLTID;
                checkPaxBM.LegNO = m_changeLegsBM.LEGNO;
                checkPaxBM.AC = m_changeLegsBM.AC;
                checkPaxBM.CKIFlightDate = m_changeLegsBM.CKIFlightDate;
                checkPaxBM.CKIFlightNo = m_changeLegsBM.CKIFlightNo;
                checkPaxBM.BookNum = strAllBookNum;
                checkPaxBM.CheckInfo = result.Replace("\n", "\r\n");
                checkPaxBM.CheckNum = Convert.ToInt32(strAdultNum) + Convert.ToInt32(strChildNum) + Convert.ToInt32(strInfantNum);
                checkPaxBM.XCRNum = Convert.ToInt32(Convert.ToInt32(strArrXCRGradeNum[0]));
                checkPaxBM.AdultNum = Convert.ToInt32(strAdultNum);
                checkPaxBM.ChildNum = Convert.ToInt32(strChildNum);
                checkPaxBM.InfantNum = Convert.ToInt32(strInfantNum);
                checkPaxBM.FirstClassNum = 0;
                checkPaxBM.OfficialClassNum = 0;
                checkPaxBM.TouristClassNum = checkPaxBM.CheckNum;
                checkPaxBM .BaggageWeight = Convert.ToInt32(strBagWeight);
                checkPaxBM.BaggageNum = Convert.ToInt32(strBagNum);               

            }
            else if (strArrCheckNum.Length == 2)
            {
                strAllBookNum = Convert.ToInt32(strArrBookNum[0]).ToString() + "/" + Convert.ToInt32(strArrBookNum[1]).ToString();
                strAllCheckNum = Convert.ToInt32(strArrCheckNum[0]).ToString() + "/" + Convert.ToInt32(strArrCheckNum[1]).ToString();
                strXCRNum = Convert.ToInt32(strArrXCRGradeNum[0]).ToString() + "/" + Convert.ToInt32(strArrXCRGradeNum[1]).ToString();
                strAdultNum = Convert.ToString(int.Parse(strArrCheckNum[0]) + int.Parse(strArrCheckNum[1]) - int.Parse(strChildNum));
                
                checkPaxBM.DATOP = m_changeLegsBM.DATOP;
                checkPaxBM.FLTID = m_changeLegsBM.FLTID;
                checkPaxBM.LegNO = m_changeLegsBM.LEGNO;
                checkPaxBM.AC = m_changeLegsBM.AC;
                checkPaxBM.CKIFlightDate = m_changeLegsBM.CKIFlightDate;
                checkPaxBM.CKIFlightNo = m_changeLegsBM.CKIFlightNo;
                checkPaxBM.BookNum = strAllBookNum;
                checkPaxBM.CheckInfo = result.Replace("\n", "\r\n");
                checkPaxBM.CheckNum = Convert.ToInt32(strAdultNum) + Convert.ToInt32(strChildNum) + Convert.ToInt32(strInfantNum);
                checkPaxBM.XCRNum = Convert.ToInt32(strArrXCRGradeNum[0]) + Convert.ToInt32(strArrXCRGradeNum[1]);
                checkPaxBM.AdultNum = Convert.ToInt32(strAdultNum);
                checkPaxBM.ChildNum = Convert.ToInt32(strChildNum);
                checkPaxBM.InfantNum = Convert.ToInt32(strInfantNum);
                checkPaxBM.FirstClassNum = Convert.ToInt32(strArrCheckNum[0]);
                checkPaxBM.OfficialClassNum = 0;
                checkPaxBM.TouristClassNum = Convert.ToInt32(strArrCheckNum[1]);
                checkPaxBM.BaggageWeight = Convert.ToInt32(strBagWeight);
                checkPaxBM.BaggageNum = Convert.ToInt32(strBagNum);           
            }
            else if (strArrCheckNum.Length == 3)
            {
                strAllBookNum = Convert.ToInt32(strArrBookNum[0]).ToString() + "/" + Convert.ToInt32(strArrBookNum[1]).ToString() + "/" + Convert.ToInt32(strArrBookNum[2]).ToString();
                strAllCheckNum = Convert.ToInt32(strArrCheckNum[0]).ToString() + "/" + Convert.ToInt32(strArrCheckNum[1]).ToString() + "/" + Convert.ToInt32(strArrCheckNum[2]).ToString();
                strXCRNum = Convert.ToInt32(strArrXCRGradeNum[0]).ToString() + "/" + Convert.ToInt32(strArrXCRGradeNum[1]).ToString() + "/" + Convert.ToInt32(strArrXCRGradeNum[2]).ToString();
                strAdultNum = Convert.ToString(int.Parse(strArrCheckNum[0]) + int.Parse(strArrCheckNum[1]) + int.Parse(strArrCheckNum[2]) - int.Parse(strChildNum));

                checkPaxBM.DATOP = m_changeLegsBM.DATOP;
                checkPaxBM.FLTID = m_changeLegsBM.FLTID;
                checkPaxBM.LegNO = m_changeLegsBM.LEGNO;
                checkPaxBM.AC = m_changeLegsBM.AC;
                checkPaxBM.CKIFlightDate = m_changeLegsBM.CKIFlightDate;
                checkPaxBM.CKIFlightNo = m_changeLegsBM.CKIFlightNo;
                checkPaxBM.BookNum = strAllBookNum;
                checkPaxBM.CheckInfo = result.Replace("\n", "\r\n");
                checkPaxBM.CheckNum = Convert.ToInt32(strAdultNum) + Convert.ToInt32(strChildNum) + Convert.ToInt32(strInfantNum);
                checkPaxBM.XCRNum = Convert.ToInt32(strArrXCRGradeNum[0]) + Convert.ToInt32(strArrXCRGradeNum[1]) + Convert.ToInt32(strArrXCRGradeNum[2]);
                checkPaxBM.AdultNum = Convert.ToInt32(strAdultNum);
                checkPaxBM.ChildNum = Convert.ToInt32(strChildNum);
                checkPaxBM.InfantNum = Convert.ToInt32(strInfantNum);
                checkPaxBM.FirstClassNum = Convert.ToInt32(strArrCheckNum[0]);
                checkPaxBM.OfficialClassNum = Convert.ToInt32(strArrCheckNum[1]);
                checkPaxBM.TouristClassNum = Convert.ToInt32(strArrCheckNum[2]);
                checkPaxBM.BaggageWeight = Convert.ToInt32(strBagWeight);
                checkPaxBM.BaggageNum = Convert.ToInt32(strBagNum);
            }
            return checkPaxBM;
        }

        /// <summary>
        /// 获取升舱
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        private int GetAscendingPaxNum(ChangeLegsBM changeLegsBM)
        {
            PaxService.PaxService objPaxService = new PaxService.PaxService();
            string strFlightNo = changeLegsBM.CKIFlightNo.Replace(" ", "").ToUpper();

            
            string strCommnadText = "PD:";
            strCommnadText += strFlightNo + "/";
            strCommnadText += DateTime.Parse(changeLegsBM.CKIFlightDate).ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) + " * ";
            strCommnadText += changeLegsBM.CityDEPSTN + changeLegsBM.CityARRSTN + ",UPG";


            //string strResult = objPaxService.PaxCheckNum(strCommnadText);
            string strResult = objPaxService.PaxCheckNum(strCommnadText, "FlightMonitor", "FlightMonitor@hnair.net");

            int iNum = 0;
            int iStart = strResult.IndexOf("\n");
            iStart = strResult.IndexOf("\n" + 4);

            if (iStart < 0)
            {
                iStart = 0;
            }
            for (int iLoop = 1; iLoop < 100; iLoop++)
            {
                if (strResult.IndexOf(iLoop.ToString() + ".", iStart) < 0)
                {
                    break;
                }
                iNum = iLoop;
                
            }

            return iNum;
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
                m_checkPaxBM = GetCheckPaxInfo(m_changeLegsBM);
                m_checkPaxBM.AscendingPaxNum = GetAscendingPaxNum(m_changeLegsBM);



                txtCheckInfo.Text = m_checkPaxBM.CheckInfo;
                txtBookNum.Text = m_checkPaxBM.BookNum;
                txtCheckNum.Text = m_checkPaxBM.CheckNum.ToString();
                txtXCRNum.Text = m_checkPaxBM.XCRNum.ToString();
                txtAdult.Text = m_checkPaxBM.AdultNum.ToString();
                txtChild.Text = m_checkPaxBM.ChildNum.ToString();
                txtInfant.Text = m_checkPaxBM.InfantNum.ToString();
                txtFirstClass.Text = m_checkPaxBM.FirstClassNum.ToString();
                txtOfficialClass.Text = m_checkPaxBM.OfficialClassNum.ToString();
                txtTouristClass.Text = m_checkPaxBM.TouristClassNum.ToString();
                txtAscendingNum.Text = m_checkPaxBM.AscendingPaxNum.ToString();
                txtBagNum.Text = m_checkPaxBM.BaggageNum.ToString();
                txtBagWeight.Text = m_checkPaxBM.BaggageWeight.ToString();
            }
        }

        /// <summary>
        /// 保存值机信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();

            if (!Validator.IsNumber(txtCheckNum.Text, "值机人数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtXCRNum.Text, "加机组数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtAdult.Text, "成人人数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtChild.Text, "儿童数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtInfant.Text, "婴儿数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtCheckNum.Text, "值机人数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtFirstClass.Text, "头等舱人数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtOfficialClass.Text, "公务舱人数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtTouristClass.Text, "经济人数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtAdult.Text, "升舱人数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtBagNum.Text, "行李件数必须为整数！"))
            {
                return;
            }
            if (!Validator.IsNumber(txtBagWeight.Text, "行李重量必须为整数！"))
            {
                return;
            }
            //允许用户修改
            if (m_changeLegsBM.STATUS == "DEP" || m_changeLegsBM.STATUS == "ATA")
            {
                m_checkPaxBM.IsAutoSaveCheckPaxInfo = 1;
            }
            m_checkPaxBM.BookNum = txtBookNum.Text.Trim();
            m_checkPaxBM.CheckNum = Convert.ToInt32(txtCheckNum.Text);
            m_checkPaxBM.XCRNum = Convert.ToInt32(txtXCRNum.Text);
            m_checkPaxBM.AdultNum = Convert.ToInt32(txtAdult.Text);
            m_checkPaxBM.ChildNum = Convert.ToInt32(txtChild.Text);
            m_checkPaxBM.InfantNum = Convert.ToInt32(txtInfant.Text);
            m_checkPaxBM.FirstClassNum = Convert.ToInt32(txtFirstClass.Text);
            m_checkPaxBM.OfficialClassNum = Convert.ToInt32(txtOfficialClass.Text);
            m_checkPaxBM.TouristClassNum = Convert.ToInt32(txtTouristClass.Text);
            m_checkPaxBM.AscendingPaxNum = Convert.ToInt32(txtAscendingNum.Text);
            m_checkPaxBM.BaggageNum = Convert.ToInt32(txtBagNum.Text);
            m_checkPaxBM.BaggageWeight = Convert.ToInt32(txtBagWeight.Text);

            ReturnValueSF rvSF = guaranteeInforBF.UpdateCheckPaxInfor(m_checkPaxBM);


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
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();


            if (rvSF.Result > 0)
            {
                //值机内容
                m_changeRecordBM.ChangeReasonCode = "cntCheckInfo";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.CheckInfo;
                m_changeRecordBM.ChangeNewContent = txtCheckInfo.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //订座人数
                m_changeRecordBM.ChangeReasonCode = "cnvcBookNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.BookNum;
                m_changeRecordBM.ChangeNewContent = txtBookNum.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //值机人数
                m_changeRecordBM.ChangeReasonCode = "cniCheckNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.CheckNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtCheckNum.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //加机组人数
                m_changeRecordBM.ChangeReasonCode = "cniXCRNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.XCRNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtXCRNum.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //成人
                m_changeRecordBM.ChangeReasonCode = "cniAdultNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.AdultNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtAdult.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //儿童
                m_changeRecordBM.ChangeReasonCode = "cniChildNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.ChildNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtChild.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //婴儿
                m_changeRecordBM.ChangeReasonCode = "cniInfantNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.InfantNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtInfant.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //头等舱
                m_changeRecordBM.ChangeReasonCode = "cniFirstClassNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.FirstClassNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtFirstClass.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //公务舱
                m_changeRecordBM.ChangeReasonCode = "cniOfficialClassNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.OfficialClassNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtOfficialClass.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //经济舱
                m_changeRecordBM.ChangeReasonCode = "cniTouristClassNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.TouristClassNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtTouristClass.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //升舱人数
                m_changeRecordBM.ChangeReasonCode = "cniAscendingPaxNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.AscendingPaxNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtAscendingNum.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //行李件数
                m_changeRecordBM.ChangeReasonCode = "cniBaggageNum";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.BaggageNum.ToString();
                m_changeRecordBM.ChangeNewContent = txtBagNum.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);
                //行李重量
                m_changeRecordBM.ChangeReasonCode = "cniBaggageWeight";
                m_changeRecordBM.ChangeOldContent = m_oldCheckPaxBM.BaggageWeight.ToString();
                m_changeRecordBM.ChangeNewContent = txtBagWeight.Text;
                rvSF = changeRecordBF.Insert(m_changeRecordBM);

            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (m_changeRecordBM.ChangeReasonCode == "cnvcBookNum")
                {
                    this.m_strNewContent = txtBookNum.Text;
                }
                else
                {
                    this.m_strNewContent = txtCheckNum.Text;
                }
                this.Close();
            }          
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
        /// 转移焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPaxKeyDown(object sender, KeyEventArgs e)
        {
            Validator.KeyDown(sender, e);
        }

        
    }
}