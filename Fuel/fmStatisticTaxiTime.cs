using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace Fuel
{
    public partial class fmStatisticTaxiTime : Form
    {
        public fmStatisticTaxiTime()
        {
            InitializeComponent();
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            if (txtThreeCode.Text.Trim() == "")
            {
                MessageBox.Show("�����벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = dtStartFlightDate.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            dateTimeBM.EndDateTime = dtEndFlightDate.Value.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00";
            StationBM stationBM = new StationBM();
            stationBM.ThreeCode = txtThreeCode.Text.Trim();

            ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM);

            DataTable dtLegs = new DataTable();
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dtLegs = rvSF.Dt;

            int iStandard = 0;

            if (txtThreeCode.Text.Trim() == "PEK" || txtThreeCode.Text.Trim() == "PVG" || txtThreeCode.Text.Trim() == "SHA" || txtThreeCode.Text.Trim() == "SZX" || txtThreeCode.Text.Trim() == "CAN")
            {
                iStandard = 40;
            }
            else
            {
                iStandard = 25;
            }

            //����ʱ��
            DataRow[] drInLegs = dtLegs.Select("cncARRSTN = '" + txtThreeCode.Text.Trim() + "' AND cncDEPSTN <> '" + txtThreeCode.Text.Trim() + "' AND cncACARSTDWN <> '' AND cncACARSATA <> ''", "cncFlightDate, cnvcFlightNo");

            
            int[] iArrInTaxiTime = new int[drInLegs.Length];

            int iLoop = 0;

            int iNotInLegsCount = 0;
            foreach (DataRow dataRow in drInLegs)
            {
                int iInTaxiHour = Convert.ToInt32(dataRow["cncACARSATA"].ToString().Substring(0,2)) - Convert.ToInt32(dataRow["cncACARSTDWN"].ToString().Substring(0,2));
                if (iInTaxiHour < 0)
                {
                    iInTaxiHour += 24;
                }
                int iInTaxiMinute = Convert.ToInt32(dataRow["cncACARSATA"].ToString().Substring(2,2)) - Convert.ToInt32(dataRow["cncACARSTDWN"].ToString().Substring(2,2));

                int iInTaxiTime = iInTaxiHour * 60 + iInTaxiMinute;

                

                if (iInTaxiTime > iStandard)
                {
                    iArrInTaxiTime[iLoop] = iInTaxiTime;
                }
                else
                {
                    iArrInTaxiTime[iLoop] = 0;
                    iNotInLegsCount += 1;
                }
                

                iLoop += 1;                
            }



            if (drInLegs.Length > 0)
            {

                int iTotalInTaxiTime = 0;
                for (iLoop = 0; iLoop < drInLegs.Length; iLoop++)
                {
                    iTotalInTaxiTime += iArrInTaxiTime[iLoop];
                }

                //ƽ��ʱ��
                double dAverageInTaxiTime = (double)iTotalInTaxiTime / (drInLegs.Length-iNotInLegsCount);
                fpFlightInfo.Sheets[0].Cells[0, 2].Text = Convert.ToString(Math.Round(dAverageInTaxiTime, 1));

                double dTemp = 0;
                for (iLoop = 0; iLoop < drInLegs.Length; iLoop++)
                {
                    if (iArrInTaxiTime[iLoop] != 0)
                    {
                        dTemp += Math.Pow(iArrInTaxiTime[iLoop] - dAverageInTaxiTime, 2);
                    }
                }

                dTemp = dTemp / (drInLegs.Length - iNotInLegsCount - 1);

                double dSIGMA = Math.Sqrt(dTemp);

                //85��ʱ��
                double d85InTaxiTime = dAverageInTaxiTime + dSIGMA;
                fpFlightInfo.Sheets[0].Cells[0, 6].Text = Convert.ToString(Math.Round(d85InTaxiTime, 1));

                //���⣥15��ƽ��ʱ�估�б�               

                int i15Count = 0;
                int d15TotalTaxiTime = 0;

                for (iLoop = 0; iLoop < drInLegs.Length; iLoop++)
                {
                    if (iArrInTaxiTime[iLoop] > d85InTaxiTime)
                    {
                        i15Count += 1;
                        d15TotalTaxiTime += iArrInTaxiTime[iLoop];
                    }
                }

                //ƽ��ʱ��
                double d15AverageTaxiTime = 0;

                if (i15Count > 0)
                {
                    d15AverageTaxiTime = d15TotalTaxiTime / i15Count;
                }

                fpFlightInfo.Sheets[0].Cells[0, 10].Text = Convert.ToString(Math.Round(d15AverageTaxiTime, 1));



                //�б�
                fpFlightInfo.Sheets[0].RowCount = i15Count + 1;

                int iRowIndex = 1;
                for (iLoop = 0; iLoop < drInLegs.Length; iLoop++)
                {
                    if (iArrInTaxiTime[iLoop] > d85InTaxiTime)
                    {
                        fpFlightInfo.Sheets[0].Cells[iRowIndex, 0].Text = drInLegs[iLoop]["cnvcFlightNo"].ToString();
                        fpFlightInfo.Sheets[0].Cells[iRowIndex, 1].Text = drInLegs[iLoop]["cncFlightDate"].ToString();

                        if (drInLegs[iLoop]["cncDEPAirportCNAME"].ToString().IndexOf("/") < 0)
                        {
                            fpFlightInfo.Sheets[0].Cells[iRowIndex, 2].Text = drInLegs[iLoop]["cncDEPAirportCNAME"].ToString();
                        }
                        else
                        {
                            fpFlightInfo.Sheets[0].Cells[iRowIndex, 2].Text = drInLegs[iLoop]["cncDEPAirportCNAME"].ToString().Substring(0, drInLegs[iLoop]["cncDEPAirportCNAME"].ToString().IndexOf("/"));
                        }

                        if (drInLegs[iLoop]["cncARRAirportCNAME"].ToString().IndexOf("/") < 0)
                        {
                            fpFlightInfo.Sheets[0].Cells[iRowIndex, 3].Text = drInLegs[iLoop]["cncARRAirportCNAME"].ToString();
                        }
                        else
                        {
                            fpFlightInfo.Sheets[0].Cells[iRowIndex, 3].Text = drInLegs[iLoop]["cncARRAirportCNAME"].ToString().Substring(0, drInLegs[iLoop]["cncARRAirportCNAME"].ToString().IndexOf("/"));
                        }
                        fpFlightInfo.Sheets[0].Cells[iRowIndex, 4].Text = iArrInTaxiTime[iLoop].ToString();



                        iRowIndex += 1;
                    }
                }








                //����ʱ��
                DataRow[] drOutLegs = dtLegs.Select("cncDEPSTN = '" + txtThreeCode.Text.Trim() + "' AND cncARRSTN <> '" + txtThreeCode.Text.Trim() + "' AND cncACARSOUT <> '' AND cncACARSTOFF <> ''", "cncFlightDate, cnvcFlightNo");


                int[] iArrOutTaxiTime = new int[drOutLegs.Length];

                iLoop = 0;

                int iNotOutLegsCount = 0;
                foreach (DataRow dataRow in drOutLegs)
                {
                    int iOutTaxiHour = Convert.ToInt32(dataRow["cncACARSTOFF"].ToString().Substring(0, 2)) - Convert.ToInt32(dataRow["cncACARSOUT"].ToString().Substring(0, 2));
                    if (iOutTaxiHour < 0)
                    {
                        iOutTaxiHour += 24;
                    }
                    int iOutTaxiMinute = Convert.ToInt32(dataRow["cncACARSTOFF"].ToString().Substring(2, 2)) - Convert.ToInt32(dataRow["cncACARSOUT"].ToString().Substring(2, 2));

                    int iOutTaxiTime = iOutTaxiHour * 60 + iOutTaxiMinute;

                    if (iOutTaxiTime > iStandard)
                    {
                        iArrOutTaxiTime[iLoop] = iOutTaxiTime;
                    }
                    else
                    {
                        iArrOutTaxiTime[iLoop] = 0;
                        iNotOutLegsCount += 1;
                    }

                    iLoop += 1;
                }



                if (drOutLegs.Length > 0)
                {

                    int iTotalOutTaxiTime = 0;
                    for (iLoop = 0; iLoop < drOutLegs.Length; iLoop++)
                    {
                        iTotalOutTaxiTime += iArrOutTaxiTime[iLoop];
                    }

                    //ƽ��ʱ��
                    double dAverageOutTaxiTime = (double)iTotalOutTaxiTime / (drOutLegs.Length - iNotOutLegsCount);
                    fpFlightInfo.Sheets[1].Cells[0, 2].Text = Convert.ToString(Math.Round(dAverageOutTaxiTime, 1));

                    dTemp = 0;
                    for (iLoop = 0; iLoop < drOutLegs.Length; iLoop++)
                    {
                        if (iArrOutTaxiTime[iLoop] != 0)
                        {
                            dTemp += Math.Pow(iArrOutTaxiTime[iLoop] - dAverageOutTaxiTime, 2);
                        }
                    }

                    dTemp = dTemp / (drOutLegs.Length - iNotOutLegsCount - 1);

                    dSIGMA = Math.Sqrt(dTemp);

                    //85��ʱ��
                    double d85OutTaxiTime = dAverageOutTaxiTime + dSIGMA;
                    fpFlightInfo.Sheets[1].Cells[0, 6].Text = Convert.ToString(Math.Round(d85OutTaxiTime, 1));

                    //���⣥15��ƽ��ʱ�估�б�               

                    i15Count = 0;
                    d15TotalTaxiTime = 0;

                    for (iLoop = 0; iLoop < drOutLegs.Length; iLoop++)
                    {
                        if (iArrOutTaxiTime[iLoop] > d85OutTaxiTime)
                        {
                            i15Count += 1;
                            d15TotalTaxiTime += iArrOutTaxiTime[iLoop];
                        }
                    }

                    //ƽ��ʱ��
                    d15AverageTaxiTime = 0;

                    if (i15Count > 0)
                    {
                        d15AverageTaxiTime = d15TotalTaxiTime / i15Count;
                    }

                    fpFlightInfo.Sheets[1].Cells[0, 10].Text = Convert.ToString(Math.Round(d15AverageTaxiTime, 1));



                    //�б�
                    fpFlightInfo.Sheets[1].RowCount = i15Count + 1;

                    iRowIndex = 1;
                    for (iLoop = 0; iLoop < drOutLegs.Length; iLoop++)
                    {
                        if (iArrOutTaxiTime[iLoop] > d85OutTaxiTime)
                        {
                            fpFlightInfo.Sheets[1].Cells[iRowIndex, 0].Text = drOutLegs[iLoop]["cnvcFlightNo"].ToString();
                            fpFlightInfo.Sheets[1].Cells[iRowIndex, 1].Text = drOutLegs[iLoop]["cncFlightDate"].ToString();

                            if (drOutLegs[iLoop]["cncDEPAirportCNAME"].ToString().IndexOf("/") < 0)
                            {
                                fpFlightInfo.Sheets[1].Cells[iRowIndex, 2].Text = drOutLegs[iLoop]["cncDEPAirportCNAME"].ToString();
                            }
                            else
                            {
                                fpFlightInfo.Sheets[1].Cells[iRowIndex, 2].Text = drOutLegs[iLoop]["cncDEPAirportCNAME"].ToString().Substring(0, drOutLegs[iLoop]["cncDEPAirportCNAME"].ToString().IndexOf("/"));
                            }

                            if (drOutLegs[iLoop]["cncARRAirportCNAME"].ToString().IndexOf("/") < 0)
                            {
                                fpFlightInfo.Sheets[1].Cells[iRowIndex, 3].Text = drOutLegs[iLoop]["cncARRAirportCNAME"].ToString();
                            }
                            else
                            {
                                fpFlightInfo.Sheets[1].Cells[iRowIndex, 3].Text = drOutLegs[iLoop]["cncARRAirportCNAME"].ToString().Substring(0, drOutLegs[iLoop]["cncARRAirportCNAME"].ToString().IndexOf("/"));
                            }
                            fpFlightInfo.Sheets[1].Cells[iRowIndex, 4].Text = iArrOutTaxiTime[iLoop].ToString();



                            iRowIndex += 1;
                        }
                    }

                }
            }

            
        }

        private void fmStatisticTaxiTime_Load(object sender, EventArgs e)
        {
            fpFlightInfo.Sheets[0].Columns[1].Width = 80;            
            fpFlightInfo.Sheets[1].Columns[1].Width = 80;

            for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[0].ColumnCount; iLoop++)
            {
                fpFlightInfo.Sheets[0].Columns[iLoop].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                fpFlightInfo.Sheets[1].Columns[iLoop].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel�ļ�(*.xls)|*.xls";
            //�����ļ�ʱѯ��
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel�ļ����Ϊ";
            saveExcel.FileName = "����ʱ��";
            //����Ĭ��·��
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                //����ǰ�����б�ǩ����ɫ�ĳ�Silver���Ա㵼����Excel�ļ���ͷ��ɫ���������ֿ���


                //				objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");

                string fileName = saveExcel.FileName;


                try
                {
                    bool save = fpFlightInfo.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    if (save)
                    {
                        //�ָ��б�ǩ����ɫ
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                    }
                    else
                    {
                        //�ָ��б�ǩ����ɫ
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                        MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}