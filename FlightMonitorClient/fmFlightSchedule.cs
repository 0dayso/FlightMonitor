using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmFlightSchedule : Form
    {
        private AccountBM m_accountBM;
        /// <summary>
        /// 构造函数
        /// </summary>
        public fmFlightSchedule(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }


        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmFlightSchedule_Load(object sender, EventArgs e)
        {
            cmbStation.DisplayMember = "cnvcAirportName";
            cmbStation.ValueMember = "cncThreeCode";
            cmbDEPStation.DisplayMember = "cncLongAirportCNAME";
            cmbARRStation.DisplayMember = "cncLongAirportCNAME";

            fpFlights.ActiveSheet.Columns[0].DataField = "cnvcFLTID";
            fpFlights.ActiveSheet.Columns[1].DataField = "cnvcLONG_REG";
            fpFlights.ActiveSheet.Columns[2].DataField = "cnvcFlightCharacterAbbreviate";
            fpFlights.ActiveSheet.Columns[3].DataField = "cncStatusName";
            fpFlights.ActiveSheet.Columns[4].DataField = "cncDEPAirportCNAME";
            fpFlights.ActiveSheet.Columns[5].DataField = "cncSTD";
            fpFlights.ActiveSheet.Columns[6].DataField = "cncETD";
            fpFlights.ActiveSheet.Columns[7].DataField = "cncTOFF";
            fpFlights.ActiveSheet.Columns[8].DataField = "cncARRAirportCNAME";
            fpFlights.ActiveSheet.Columns[9].DataField = "cncSTA";
            fpFlights.ActiveSheet.Columns[10].DataField = "cncETA";
            fpFlights.ActiveSheet.Columns[11].DataField = "cncTDWN";


            StationBF stationBF = new StationBF();
            ReturnValueSF rvSF = stationBF.GetAllStation();
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                cmbStation.DataSource = rvSF.Dt;
            }
            cmbStation.SelectedValue = m_accountBM.StationThreeCode;

            AirportInforBF airportInforBF = new AirportInforBF();
            rvSF = airportInforBF.GetAirportInfors();
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                cmbDEPStation.DataSource = rvSF.Dt;                
            }

            rvSF = airportInforBF.GetAirportInfors();
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                cmbARRStation.DataSource = rvSF.Dt;
            }

            cmbDEPStation.Text = "";
            cmbARRStation.Text = "";
           
        }

        /// <summary>
        /// 航站变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStation_SelectedIndexChanged(object sender, EventArgs e)
        {            
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = dtFlightdate.Value.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = dtFlightdate.Value.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";

            DataTable dtStations = (DataTable)cmbStation.DataSource;
            StationBM stationBM = new StationBM(dtStations.Rows[cmbStation.SelectedIndex]);

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ReturnValueSF rvSF = guaranteeInforBF.GetStationFlight(dateTimeBM, stationBM);
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //fpFlights.DataSource = rvSF.Dt;   //使用以下代码 -- 2011.02.10 modified by 林勇

                #region 根据 status 设置 实际起飞 和  实际落地 的值 -- 2011.02.10 modified by 林勇

                DataTable dtFlights = rvSF.Dt.Copy();

                foreach (DataRow drFlight in dtFlights.Rows)
                {
                    if (drFlight["cncSTATUS"].ToString().Trim().ToUpper() == "DEP")
                    {
                        drFlight["cncTDWN"] = "";
                    }
                    else if (drFlight["cncSTATUS"].ToString().Trim().ToUpper() == "ATA")
                    {
                    }
                    else
                    {
                        drFlight["cncTOFF"] = "";
                        drFlight["cncTDWN"] = "";
                    }

                }

                fpFlights.DataSource = dtFlights;

                #endregion 根据 status 设置 实际起飞 和  实际落地 的值 -- 2011.02.10 modified by 林勇

            }

            fpFlights.Focus();
        }

        /// <summary>
        /// 日期变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtFlightdate_ValueChanged(object sender, EventArgs e)
        {
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = dtFlightdate.Value.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = dtFlightdate.Value.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";

            DataTable dtStations = (DataTable)cmbStation.DataSource;
            StationBM stationBM = new StationBM(dtStations.Rows[cmbStation.SelectedIndex]);

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ReturnValueSF rvSF = guaranteeInforBF.GetStationFlight(dateTimeBM, stationBM);
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //fpFlights.DataSource = rvSF.Dt;   //使用以下代码 -- 2011.02.10 modified by 林勇

                #region 根据 status 设置 实际起飞 和  实际落地 的值 -- 2011.02.10 modified by 林勇

                DataTable dtFlights = rvSF.Dt.Copy();

                foreach (DataRow drFlight in dtFlights.Rows)
                {
                    if (drFlight["cncSTATUS"].ToString().Trim().ToUpper() == "DEP")
                    {
                        drFlight["cncTDWN"] = "";
                    }
                    else if (drFlight["cncSTATUS"].ToString().Trim().ToUpper() == "ATA")
                    {
                    }
                    else
                    {
                        drFlight["cncTOFF"] = "";
                        drFlight["cncTDWN"] = "";
                    }

                }

                fpFlights.DataSource = dtFlights;

                #endregion 根据 status 设置 实际起飞 和  实际落地 的值 -- 2011.02.10 modified by 林勇
            }

            fpFlights.Focus();
        }

        /// <summary>
        /// 按航班号或起飞目的地查询航班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSumbit_Click(object sender, EventArgs e)
        {
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = dtQueryFlightDate.Value.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = dtQueryFlightDate.Value.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            string strDEPSTN = "";
            string strARRSTN = "";
            if (cmbDEPStation.Text.IndexOf("/") > 0)
            {
                strDEPSTN = cmbDEPStation.Text.Substring(0, 3);
            }
            else if (cmbDEPStation.Text.IndexOf("/") < 0)
            {
                strDEPSTN = cmbDEPStation.Text;
            }
            if (cmbARRStation.Text.IndexOf("/") > 0)
            {
                strARRSTN = cmbARRStation.Text.Substring(0, 3);
            }
            else if (cmbARRStation.Text.IndexOf("/") < 0)
            {
                strARRSTN = cmbARRStation.Text;
            }

            ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByFlightNo(dateTimeBM, strDEPSTN, strARRSTN, txtFlightNo.Text.Trim());
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //fpFlights.DataSource = rvSF.Dt; //使用以下代码  -- 2011.02.09 modified by 林勇

                #region 根据 status 设置 实际起飞 和  实际落地 的值 -- 2011.02.09 modified by 林勇

                DataTable dtFlights = rvSF.Dt.Copy();

                foreach (DataRow drFlight in dtFlights.Rows)
                {
                    if (drFlight["cncSTATUS"].ToString().Trim().ToUpper() == "DEP")
                    {
                        drFlight["cncTDWN"] = "";
                    }
                    else if (drFlight["cncSTATUS"].ToString().Trim().ToUpper() == "ATA")
                    {
                    }
                    else
                    {
                        drFlight["cncTOFF"] = "";
                        drFlight["cncTDWN"] = "";
                    }

                }

                fpFlights.DataSource = dtFlights;

                #endregion 根据 status 设置 实际起飞 和  实际落地 的值 -- 2011.02.09 modified by 林勇

            }

            fpFlights.Focus();
        }

        /// <summary>
        /// 打印计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintPlan_Click(object sender, EventArgs e)
        {
            FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
            fpFlights.Sheets[0].PrintInfo = pi;
            //设置页边距
            pi.Margin = new FarPoint.Win.Spread.PrintMargin(30, 50, 0, 50, 0, 0);
            //弹出设置对话框
            pi.ShowPrintDialog = true;
            pi.Header = "/fn\"宋体\"/fz\"14.25\"/fb1/fi0/fu0/fk0/c" + "航班计划（" + cmbStation.Text + dtFlightdate.Value.ToString("yyyy-MM-dd") + "）" + "/n";
            pi.Footer = "打印时间：" + dtFlightdate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            fpFlights.PrintSheet(0);
        }

        /// <summary>
        /// 导出为EXCEL动态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel文件(*.xls)|*.xls";
            saveExcel.FileName = "航班计划" + dtFlightdate.Value.ToString("yyyy-MM-dd") + ".xls";

            //覆盖文件时询问
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel文件另存为";
            //设置默认路径
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\PlanFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                //导出前，将列标签背景色改成Silver，以便导出的Excel文件表头颜色与内容区分开来
                fpFlights.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");
                string fileName = saveExcel.FileName;
                bool save = fpFlights.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                if (save)
                {
                    //恢复列标签背景色
                    fpFlights.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");
                    //MessageBox.Show("导出成功！");
                }
                else
                {
                    //恢复列标签背景色
                    fpFlights.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");
                    MessageBox.Show("导出失败！");
                }
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
    }
}