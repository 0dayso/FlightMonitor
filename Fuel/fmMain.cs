using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace Fuel
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
        }


        private void ImportCFPData()
        {
            fmImportCFPData objfmImportCFPData = new fmImportCFPData();
            objfmImportCFPData.ShowDialog();
        }

        private void miImportFPLData_Click(object sender, EventArgs e)
        {
            ImportCFPData();
        }

        private void tbImportCFPData_Click(object sender, EventArgs e)
        {
            ImportCFPData();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void miStatisticFlyTime_Click(object sender, EventArgs e)
        {
            StatisticFlyTime();
        }

        private void tbStatisticFlyTime_Click(object sender, EventArgs e)
        {
            StatisticFlyTime();
        }

        private void StatisticFlyTime()
        {
            fmStatisticFlyTimeRange objfmStatisticFlyTimeRange = new fmStatisticFlyTimeRange();

            if (objfmStatisticFlyTimeRange.ShowDialog() == DialogResult.OK)
            {
                string strStartFlightDate = objfmStatisticFlyTimeRange.StartDate;
                string strEndFlightDate = objfmStatisticFlyTimeRange.EndDate;

                //航班动态
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
                ReturnValueSF rvSF = guaranteeInforBF.GetLegsByFlightDate(strStartFlightDate, strEndFlightDate);


                DataTable dtGuaranteeInfor = new DataTable();
                if (rvSF.Result > 0)
                {
                    dtGuaranteeInfor = rvSF.Dt;
                }
                else
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DataRow[] drLegs = dtGuaranteeInfor.Select("cnvcFlightNo LIKE 'HU%' AND cncACARSTOFF<>'' AND cncACARSTDWN<> '' AND cncCFPTOFF <> '' AND cncCFPTDWN <> ''", "cncFlightDate,cnvcFlightNo");


                DataTable dtResult = new DataTable();

                dtResult.Columns.Add("cnvcFlightNo");
                dtResult.Columns.Add("cncFlightDate");
                dtResult.Columns.Add("cncDEPAirportCNAME");
                dtResult.Columns.Add("cncARRAirportCNAME");
                
                dtResult.Columns.Add("cncCFPTOFF");
                dtResult.Columns.Add("cncCFPTDWN");
                dtResult.Columns.Add("cniCFPFlyTime");

                dtResult.Columns.Add("cncTOFF");
                dtResult.Columns.Add("cncTDWN");
                dtResult.Columns.Add("cniFlyTime");

                dtResult.Columns.Add("cncACARSTOFF");
                dtResult.Columns.Add("cncACARSTDWN");
                dtResult.Columns.Add("cniACARSFlyTime");


                dtResult.Columns.Add("cniDiff");
                

                foreach (DataRow rowItem in drLegs)
                {
                    int iCFPHourToff = Convert.ToInt32(rowItem["cncCFPTOFF"].ToString().Substring(0, 2));
                    int iCFPHourTdwn = Convert.ToInt32(rowItem["cncCFPTDWN"].ToString().Substring(0, 2));
                    int iCFPFlyHour = iCFPHourTdwn - iCFPHourToff;
                    if (iCFPFlyHour < 0)
                    {
                        iCFPFlyHour += 24;
                    }

                    int iCFPMinuteToff = Convert.ToInt32(rowItem["cncCFPTOFF"].ToString().Substring(2, 2));
                    int iCFPMinuteTdwn = Convert.ToInt32(rowItem["cncCFPTDWN"].ToString().Substring(2, 2));

                    int iCFPFlyMinute = iCFPFlyHour * 60 + iCFPMinuteTdwn - iCFPMinuteToff;



                    int iHourToff = Convert.ToInt32(DateTime.Parse(rowItem["cncTOFF"].ToString()).ToString("HHmm").Substring(0, 2));
                    int iHourTdwn = Convert.ToInt32(DateTime.Parse(rowItem["cncTDWN"].ToString()).ToString("HHmm").Substring(0, 2));
                    int iFlyHour = iHourTdwn - iHourToff;
                    if (iFlyHour < 0)
                    {
                        iFlyHour += 24;
                    }

                    int iMinuteToff = Convert.ToInt32(DateTime.Parse(rowItem["cncTOFF"].ToString()).ToString("HHmm").Substring(2, 2));
                    int iMinuteTdwn = Convert.ToInt32(DateTime.Parse(rowItem["cncTDWN"].ToString()).ToString("HHmm").Substring(2, 2));

                    int iFlyMinute = iFlyHour * 60 + iMinuteTdwn - iMinuteToff;




                    int iACARSHourToff = Convert.ToInt32(rowItem["cncACARSTOFF"].ToString().Substring(0, 2));
                    int iACARSHourTdwn = Convert.ToInt32(rowItem["cncACARSTDWN"].ToString().Substring(0, 2));
                    int iACARSFlyHour = iACARSHourTdwn - iACARSHourToff;
                    if (iACARSFlyHour < 0)
                    {
                        iACARSFlyHour += 24;
                    }

                    int iACARSMinuteToff = Convert.ToInt32(rowItem["cncACARSTOFF"].ToString().Substring(2, 2));
                    int iACARSMinuteTdwn = Convert.ToInt32(rowItem["cncACARSTDWN"].ToString().Substring(2, 2));

                    int iACARSFlyMinute = iACARSFlyHour * 60 + iACARSMinuteTdwn - iACARSMinuteToff;



                    int iDiff1 = iACARSFlyMinute - iCFPFlyMinute;
                    int iDiff2 = iFlyMinute - iCFPFlyMinute;
                    int iDiff = 0;

                    if (Math.Abs(iDiff2 - iDiff1) > 10)
                    {
                        iDiff = iDiff2;
                    }
                    else
                    {
                        iDiff = iDiff1;
                    }

                    if (iDiff > 10)
                    {
                        DataRow drResult = dtResult.NewRow();
                        drResult["cnvcFlightNo"] = rowItem["cnvcFlightNo"].ToString();
                        drResult["cncFlightDate"] = rowItem["cncFlightDate"].ToString();
                        if (rowItem["cncDEPAirportCNAME"].ToString().IndexOf("/") < 0)
                        {
                            drResult["cncDEPAirportCNAME"] = rowItem["cncDEPAirportCNAME"].ToString();
                        }
                        else
                        {
                            drResult["cncDEPAirportCNAME"] = rowItem["cncDEPAirportCNAME"].ToString().Substring(0, rowItem["cncDEPAirportCNAME"].ToString().IndexOf("/"));
                        }

                        if (rowItem["cncARRAirportCNAME"].ToString().IndexOf("/") < 0)
                        {
                            drResult["cncARRAirportCNAME"] = rowItem["cncARRAirportCNAME"].ToString();
                        }
                        else
                        {
                            drResult["cncARRAirportCNAME"] = rowItem["cncARRAirportCNAME"].ToString().Substring(0, rowItem["cncARRAirportCNAME"].ToString().IndexOf("/"));
                        }


                        drResult["cncCFPTOFF"] = rowItem["cncCFPTOFF"].ToString();
                        drResult["cncCFPTDWN"] = rowItem["cncCFPTDWN"].ToString();
                        drResult["cniCFPFlyTime"] = iCFPFlyMinute;

                        drResult["cncTOFF"] = DateTime.Parse(rowItem["cncTOFF"].ToString()).ToString("HHmm");
                        drResult["cncTDWN"] = DateTime.Parse(rowItem["cncTDWN"].ToString()).ToString("HHmm");
                        drResult["cniFlyTime"] = iFlyMinute;

                        drResult["cncACARSTOFF"] = rowItem["cncACARSTOFF"].ToString();
                        drResult["cncACARSTDWN"] = rowItem["cncACARSTDWN"].ToString();
                        drResult["cniACARSFlyTime"] = iACARSFlyMinute;


                        drResult["cniDiff"] = iDiff;

                        dtResult.Rows.Add(drResult);
                    }

                }


                fpFlightInfo.Sheets[0].ColumnCount = 14;

                fpFlightInfo.Sheets[0].ColumnHeader.Rows[0].Height = 60;
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 0].Text = "航班号";                
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 1].Text = "航班日期";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 2].Text = "始发站";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 3].Text = "到达站";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 4].Text = "飞行计划起飞时间";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 5].Text = "飞行计划到达时间";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 6].Text = "计划飞行时间长";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 7].Text = "FOC起飞时间";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 8].Text = "FOC落地时间";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 9].Text = "FOC飞行时间长";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 10].Text = "ACARS起飞时间";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 11].Text = "ACARS落地时间";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 12].Text = "ACARS飞行时间长";
                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, 13].Text = "时间差";


                fpFlightInfo.Sheets[0].Columns[0].Width = 60;
                fpFlightInfo.Sheets[0].Columns[1].Width = 80;
                fpFlightInfo.Sheets[0].Columns[2].Width = 70;
                fpFlightInfo.Sheets[0].Columns[3].Width = 70;
                fpFlightInfo.Sheets[0].Columns[4].Width = 60;
                fpFlightInfo.Sheets[0].Columns[5].Width = 60;
                fpFlightInfo.Sheets[0].Columns[6].Width = 60;
                fpFlightInfo.Sheets[0].Columns[7].Width = 60;
                fpFlightInfo.Sheets[0].Columns[8].Width = 60;
                fpFlightInfo.Sheets[0].Columns[9].Width = 60;
                fpFlightInfo.Sheets[0].Columns[10].Width = 60;
                fpFlightInfo.Sheets[0].Columns[11].Width = 60;
                fpFlightInfo.Sheets[0].Columns[12].Width = 60;
                fpFlightInfo.Sheets[0].Columns[13].Width = 60;
                



                fpFlightInfo.ActiveSheet.Columns[0].DataField = "cnvcFlightNo";                
                fpFlightInfo.ActiveSheet.Columns[1].DataField = "cncFlightDate";
                fpFlightInfo.ActiveSheet.Columns[2].DataField = "cncDEPAirportCNAME";
                fpFlightInfo.ActiveSheet.Columns[3].DataField = "cncARRAirportCNAME";
                fpFlightInfo.ActiveSheet.Columns[4].DataField = "cncCFPTOFF";
                fpFlightInfo.ActiveSheet.Columns[5].DataField = "cncCFPTDWN";
                fpFlightInfo.ActiveSheet.Columns[6].DataField = "cniCFPFlyTime";
                fpFlightInfo.ActiveSheet.Columns[7].DataField = "cncTOFF";
                fpFlightInfo.ActiveSheet.Columns[8].DataField = "cncTDWN";
                fpFlightInfo.ActiveSheet.Columns[9].DataField = "cniFlyTime";
                fpFlightInfo.ActiveSheet.Columns[10].DataField = "cncACARSTOFF";
                fpFlightInfo.ActiveSheet.Columns[11].DataField = "cncACARSTDWN";
                fpFlightInfo.ActiveSheet.Columns[12].DataField = "cniACARSFlyTime";
                fpFlightInfo.ActiveSheet.Columns[13].DataField = "cniDiff";

                for (int iLoop = 0; iLoop < 14; iLoop++)
                {
                    fpFlightInfo.ActiveSheet.Columns[iLoop].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }

                fpFlightInfo.Sheets[0].DataSource = dtResult;
                
                 
            }
        }

        private void tbExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel文件(*.xls)|*.xls";
            //覆盖文件时询问
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel文件另存为";
            saveExcel.FileName = "航班动态";
            //设置默认路径
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                //导出前，将列标签背景色改成Silver，以便导出的Excel文件表头颜色与内容区分开来


                //				objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");

                string fileName = saveExcel.FileName;


                try
                {
                    bool save = fpFlightInfo.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    if (save)
                    {
                        //恢复列标签背景色
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                    }
                    else
                    {
                        //恢复列标签背景色
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                        MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void miTaxiFlyTime_Click(object sender, EventArgs e)
        {
            TaxiTime();
        }

        private void tbTaxiFlyTime_Click(object sender, EventArgs e)
        {
            TaxiTime();
        }

        private void TaxiTime()
        {
            fmStatisticTaxiTime objfmStatisticTaxiTime = new fmStatisticTaxiTime();
            objfmStatisticTaxiTime.ShowDialog();
        }




    }
}