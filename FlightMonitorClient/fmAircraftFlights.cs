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
    public partial class fmAircraftFlights : Form
    {
        private Color colorFavorate;

        private DateTimeBM m_dateTimeBM;
        private string m_strLONG_REG;
        private ChangeLegsBM m_changeLegsBM;
        private ChangeRecordBM m_changeRecordBM;
        private AccountBM m_accountBM;
        private DataTable m_dtDataItemPurview;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dateTimeBM">时间范围实体对象</param>
        /// <param name="strLONG_REG">飞机注册号</param>
        public fmAircraftFlights(DateTimeBM dateTimeBM, string strLONG_REG, AccountBM accountBM, DataTable dtDataItemPurview)
        {
            InitializeComponent();
            this.m_dateTimeBM = dateTimeBM;
            this.m_strLONG_REG = strLONG_REG;

            //自定义的背景色
            //colorFavorate = Color.FromArgb(182, 222, 187);           
            colorFavorate = Color.White;
            this.m_changeLegsBM = new ChangeLegsBM();
            this.m_changeRecordBM = new ChangeRecordBM();
            this.m_accountBM = accountBM;
            this.m_dtDataItemPurview = dtDataItemPurview;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmAircraftFlights_Load(object sender, EventArgs e)
        {
            fpAircraftFlights.ActiveSheet.Columns[0].DataField = "cnvcFLTID";
            fpAircraftFlights.ActiveSheet.Columns[1].DataField = "cnvcLONG_REG";
            fpAircraftFlights.ActiveSheet.Columns[2].DataField = "cnvcFlightCharacterAbbreviate";
            fpAircraftFlights.ActiveSheet.Columns[3].DataField = "cncStatusName";
            fpAircraftFlights.ActiveSheet.Columns[4].DataField = "cncDEPAirportCNAME";
            fpAircraftFlights.ActiveSheet.Columns[5].DataField = "cncSTD";
            fpAircraftFlights.ActiveSheet.Columns[6].DataField = "cncETD";
            fpAircraftFlights.ActiveSheet.Columns[7].DataField = "cncTOFF";
            fpAircraftFlights.ActiveSheet.Columns[8].DataField = "cncARRAirportCNAME";
            fpAircraftFlights.ActiveSheet.Columns[9].DataField = "cncSTA";
            fpAircraftFlights.ActiveSheet.Columns[10].DataField = "cncETA";
            fpAircraftFlights.ActiveSheet.Columns[11].DataField = "cncTDWN";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            guaranteeInforBF.GetAircraftFlights(m_dateTimeBM, m_strLONG_REG);

            ReturnValueSF rvSF = guaranteeInforBF.GetAircraftFlights(m_dateTimeBM, m_strLONG_REG);

            if (rvSF.Result > 0)
            {
                fpAircraftFlights.DataSource = rvSF.Dt;
                SetGridColor();
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void SetGridColor()
        {
            DataTable dtFlights = (DataTable) fpAircraftFlights.DataSource;
            int iRowIndex = 0;
            foreach (DataRow dataRow in dtFlights.Rows)
            {
                if (dataRow["cncSTATUS"].ToString() == "SCH")
                {
                    fpAircraftFlights.Sheets[0].Rows[iRowIndex].BackColor = colorFavorate;
                }
                else if (dataRow["cncSTATUS"].ToString() == "DEL")
                {
                    fpAircraftFlights.Sheets[0].Rows[iRowIndex].BackColor = Color.Yellow;
                }
                else if (dataRow["cncSTATUS"].ToString() == "DEP")
                {
                    fpAircraftFlights.Sheets[0].Rows[iRowIndex].BackColor = Color.LimeGreen;
                    if (dataRow["cnvcDELAY1"].ToString() != "")
                    {
                        fpAircraftFlights.Sheets[0].Cells[iRowIndex, 10].BackColor = Color.Yellow;
                        fpAircraftFlights.Sheets[0].Cells[iRowIndex, 6].BackColor = Color.Yellow;
                    }
                }
                else if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "CNL")
                {
                    fpAircraftFlights.Sheets[0].Rows[iRowIndex].BackColor = Color.Silver;
                    if (dataRow["cnvcDELAY1"].ToString() != "")
                    {
                        fpAircraftFlights.Sheets[0].Cells[iRowIndex, 10].BackColor = Color.Yellow;
                        fpAircraftFlights.Sheets[0].Cells[iRowIndex, 6].BackColor = Color.Yellow;
                    }
                }

                
                iRowIndex += 1;
            }
        }

        /// <summary>
        /// 变更数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutChangeData_Click(object sender, EventArgs e)
        {
            fmChangeData objfmChangeData = new fmChangeData(m_dateTimeBM, m_accountBM, m_changeLegsBM.FLTID, "", 0);
            objfmChangeData.ShowDialog();
        }

        /// <summary>
        /// 值机旅客信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutCheckPax_Click(object sender, EventArgs e)
        {          

            if (GetDataItemPurview("OutcniCheckNum") > 1)
            {
                fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(m_changeLegsBM, m_changeRecordBM);
                objfmCheckPax.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 中转连程旅客信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutTransitPax_Click(object sender, EventArgs e)
        {
            if (GetDataItemPurview("OutcnbTransitPaxTag") > 1)
            {
                fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(m_changeLegsBM, m_changeRecordBM);
                objfmMaintenTransitPax.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 旅客名单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutNameList_Click(object sender, EventArgs e)
        {
            if (GetDataItemPurview("OutcntPaxNameList") > 1)
            {
                fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(m_changeLegsBM, m_changeRecordBM);
                objfmMaintenPaxNameList.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 飞行计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFlightPlan_Click(object sender, EventArgs e)
        {
            fmComputerPlan objfmComputerPlan = new fmComputerPlan(m_changeLegsBM.FlightDate, m_changeLegsBM.FLTID, m_changeLegsBM.DEPSTN, m_changeLegsBM.ARRSTN);
            objfmComputerPlan.ShowDialog();
            objfmComputerPlan.Text += m_changeLegsBM.FLTID;
        }

        /// <summary>
        /// 任务书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutTaskSheet_Click(object sender, EventArgs e)
        {
            string strURL = "http://crw.hnair.net/WebUI/PilotWebUI/PrintFltTask/wfmPrintFltTask.aspx";
            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "-任务书" + m_changeLegsBM.FLTID;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 签派放行单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutDispathSheet_Click(object sender, EventArgs e)
        {
            fmDispatchSheet objfmDispatchSheet = new fmDispatchSheet(m_changeLegsBM.FlightDate, m_changeLegsBM.FLTID, m_changeLegsBM.DEPSTN, m_changeLegsBM.ARRSTN);

            objfmDispatchSheet.ShowDialog();
            objfmDispatchSheet.Text += m_changeLegsBM.FLTID;
        }


        /// <summary>
        /// 天气
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutWeather_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planMet.aspx?strCompanyID=9&strstdDate=";
            strURL += m_changeLegsBM.FlightDate + "&strDepstn=" + m_changeLegsBM.DEPSTN +
                "&strArrstn=" + m_changeLegsBM.ARRSTN + "&strFlightno=" + m_changeLegsBM.FLTID +
                "&strPlaneNo=" + m_changeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_changeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_changeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_changeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "天气" + m_changeLegsBM.FLTID;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 天气标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutWeatherStandard_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/notam/notam/standard/AirStandard.asp?CODEINPUT=" + m_changeLegsBM.ARRFourCode;

            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "天气标准" + m_changeLegsBM.FLTID;
            objfmSurpportInfor.ShowDialog();
        }
        /// <summary>
        /// 航行通告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutNOTAM_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planNotam.aspx?strCompanyID=9&strstdDate=";
            strURL += m_changeLegsBM.FlightDate + "&strDepstn=" + m_changeLegsBM.DEPSTN +
                "&strArrstn=" + m_changeLegsBM.ARRSTN + "&strFlightno=" + m_changeLegsBM.FLTID +
                "&strPlaneNo=" + m_changeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_changeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_changeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_changeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "航行通告" + m_changeLegsBM.FLTID;
            objfmSurpportInfor.ShowDialog();
        }
        /// <summary>
        /// 出港机组信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutCrew_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/PublicInfo/AirCrew.aspx?strCompanyID=9&strstdDate=";
            strURL += m_changeLegsBM.FlightDate + "&strDepstn=" + m_changeLegsBM.DEPSTN +
                "&strArrstn=" + m_changeLegsBM.ARRSTN + "&strFlightno=" + m_changeLegsBM.FLTID +
                "&strPlaneNo=" + m_changeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_changeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_changeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_changeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "机组" + m_changeLegsBM.FLTID;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 出港航线分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFlightLine_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Aircraft/airline.aspx?strCompanyID=9&strstdDate=";
            strURL += m_changeLegsBM.FlightDate + "&strDepstn=" + m_changeLegsBM.DEPSTN +
                "&strArrstn=" + m_changeLegsBM.ARRSTN + "&strFlightno=" + m_changeLegsBM.FLTID +
                "&strPlaneNo=" + m_changeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_changeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_changeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_changeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "航线分析" + m_changeLegsBM.FLTID;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 出港重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutWeightData_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/tps/weight_data/planbalancedata.asp?strCompanyID=9&strstdDate=";
            strURL += m_changeLegsBM.FlightDate + "&strDepstn=" + m_changeLegsBM.DEPSTN +
                "&strArrstn=" + m_changeLegsBM.ARRSTN + "&strFlightno=" + m_changeLegsBM.FLTID +
                "&strPlaneNo=" + m_changeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_changeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_changeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_changeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "重量数据" + m_changeLegsBM.FLTID;
            objfmSurpportInfor.ShowDialog();
        }


        /// <summary>
        /// 出港保留故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFault_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Aircraft/AircraftInfo.aspx?strCompanyID=9&strstdDate=";
            strURL += m_changeLegsBM.FlightDate + "&strDepstn=" + m_changeLegsBM.DEPSTN +
                "&strArrstn=" + m_changeLegsBM.ARRSTN + "&strFlightno=" + m_changeLegsBM.FLTID +
                "&strPlaneNo=" + m_changeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + m_changeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + m_changeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + m_changeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "保留故障" + m_changeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        private void fpAircraftFlights_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dtFlights = (DataTable)fpAircraftFlights.DataSource;

            m_changeLegsBM.DATOP = dtFlights.Rows[e.Row]["cncDATOP"].ToString();
            m_changeLegsBM.FLTID = dtFlights.Rows[e.Row]["cnvcFLTID"].ToString();
            m_changeLegsBM.LEGNO = Convert.ToInt32(dtFlights.Rows[e.Row]["cniLEGNO"].ToString());
            m_changeLegsBM.AC = dtFlights.Rows[e.Row]["cnvcAC"].ToString();

            m_changeLegsBM.FlightDate = dtFlights.Rows[e.Row]["cncFlightDate"].ToString();
            m_changeLegsBM.LONG_REG = dtFlights.Rows[e.Row]["cnvcLONG_REG"].ToString();
            m_changeLegsBM.CKIFlightDate = dtFlights.Rows[e.Row]["cncCKIFlightDate"].ToString();
            m_changeLegsBM.CKIFlightNo = dtFlights.Rows[e.Row]["cnvcCKIFlightNo"].ToString();
            m_changeLegsBM.DEPSTN = dtFlights.Rows[e.Row]["cncDEPSTN"].ToString();
            m_changeLegsBM.DEPFourCode = dtFlights.Rows[e.Row]["cncDEPAirportFourCode"].ToString();
            m_changeLegsBM.CityDEPSTN = dtFlights.Rows[e.Row]["cncDEPCityThreeCode"].ToString();
            m_changeLegsBM.ARRSTN = dtFlights.Rows[e.Row]["cncARRSTN"].ToString();
            m_changeLegsBM.ARRFourCode = dtFlights.Rows[e.Row]["cncARRAirportFourCode"].ToString();
            m_changeLegsBM.CityARRSTN = dtFlights.Rows[e.Row]["cncARRCityThreeCode"].ToString();
            m_changeLegsBM.STD = dtFlights.Rows[e.Row]["cncAllSTD"].ToString();
            m_changeLegsBM.ETD = dtFlights.Rows[e.Row]["cncAllETD"].ToString();
            m_changeLegsBM.STA = dtFlights.Rows[e.Row]["cncAllSTA"].ToString();
            m_changeLegsBM.ETA = dtFlights.Rows[e.Row]["cncAllETA"].ToString();
            m_changeLegsBM.STATUS = dtFlights.Rows[e.Row]["cncSTATUS"].ToString();
            m_changeLegsBM.STC = dtFlights.Rows[e.Row]["cnvcSTC"].ToString();


            //变更信息
            m_changeRecordBM.UserID = m_accountBM.UserId;
            m_changeRecordBM.OldDATOP = dtFlights.Rows[e.Row]["cncDATOP"].ToString();
            m_changeRecordBM.OldFLTID = dtFlights.Rows[e.Row]["cnvcFLTID"].ToString();
            m_changeRecordBM.OldLegNo = Convert.ToInt32(dtFlights.Rows[e.Row]["cniLEGNO"].ToString());
            m_changeRecordBM.OldAC = dtFlights.Rows[e.Row]["cnvcAC"].ToString();
            m_changeRecordBM.NewDATOP = dtFlights.Rows[e.Row]["cncDATOP"].ToString();
            m_changeRecordBM.NewFLTID = dtFlights.Rows[e.Row]["cnvcFLTID"].ToString();
            m_changeRecordBM.NewLegNo = Convert.ToInt32(dtFlights.Rows[e.Row]["cniLEGNO"].ToString());
            m_changeRecordBM.NewAC = dtFlights.Rows[e.Row]["cnvcAC"].ToString();

            m_changeRecordBM.OldDepSTN = dtFlights.Rows[e.Row]["cncDEPSTN"].ToString();
            m_changeRecordBM.OldArrSTN = dtFlights.Rows[e.Row]["cncARRSTN"].ToString();
            m_changeRecordBM.NewDepSTN = dtFlights.Rows[e.Row]["cncDEPSTN"].ToString();
            m_changeRecordBM.NewArrSTN = dtFlights.Rows[e.Row]["cncARRSTN"].ToString();
            m_changeRecordBM.STD = dtFlights.Rows[e.Row]["cncAllSTD"].ToString();
            m_changeRecordBM.ETD = dtFlights.Rows[e.Row]["cncAllETD"].ToString();
            m_changeRecordBM.STA = dtFlights.Rows[e.Row]["cncAllSTA"].ToString();
            m_changeRecordBM.ETA = dtFlights.Rows[e.Row]["cncAllETA"].ToString();
            m_changeRecordBM.ChangeOldContent = "";
            m_changeRecordBM.ActionTag = "U";
            m_changeRecordBM.Refresh = 0;

            if (e.Button == MouseButtons.Right)
            {
                fpAircraftFlights.ActiveSheet.AddSelection(e.Row, 0, 1, fpAircraftFlights.ActiveSheet.ColumnCount);
                Point p = new Point(e.X, e.Y);
                popOutFlightNoMenu.Show(fpAircraftFlights, p);
            }
        }

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="strDataItemID"></param>
        private int GetDataItemPurview(string strDataItemID)
        {
            DataRow[] dataRow = m_dtDataItemPurview.Select("cnvcDataItemID = '" + strDataItemID + "'");

            if (dataRow.Length > 0)
            {
                return Convert.ToInt32(dataRow[0]["cniDataItemPurview"].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 机场协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpAircraftFlights_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 4 || e.Column == 8)
            {
                //			http://gcbweb.hnair.net/agreement/agreement.asp?area0=



                string strURL = "http://gcbweb.hnair.net/agreement/agreement.asp?area0=" + fpAircraftFlights.Sheets[0].Cells[e.Row, e.Column].Text + "&sentit=查询&R1=全部";

                fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
                objfmSurpportInfor.Text += "机场协议";
                objfmSurpportInfor.ShowDialog();
            }
        }
        
    }
}