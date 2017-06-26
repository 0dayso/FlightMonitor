using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmStatisticPax : Form
    {
        private AccountBM m_AccountBM;

        public fmStatisticPax(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_AccountBM = accountBM;
        }

        private void fmStatisticPax_Load(object sender, EventArgs e)
        {
            fpPax.Sheets[0].Columns[0].DataField = "cncIATATYPE";
            fpPax.Sheets[0].Columns[1].DataField = "cniFlgihtNum";
            fpPax.Sheets[0].Columns[2].DataField = "cniCheckNum";


            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = dtFlightDate.Value.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = dtFlightDate.Value.AddDays(1).ToString("yyyy-MM-dd") + "05:00:00";

            ReturnValueSF rvSF = guaranteeInforBF.GetStatisticPax(dateTimeBM, m_AccountBM.StationThreeCode);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                fpPax.DataSource = rvSF.Dt;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel�ļ�(*.xls)|*.xls";
            //�����ļ�ʱѯ��
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel�ļ����Ϊ";
            saveExcel.FileName = "�ÿ�����ͳ��" + DateTime.Now.ToString("yyyy-MM-dd");
            //����Ĭ��·��
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                //����ǰ�����б�ǩ����ɫ�ĳ�Silver���Ա㵼����Excel�ļ���ͷ��ɫ���������ֿ���
                fpPax.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");

                string fileName = saveExcel.FileName;
                bool save = fpPax.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                if (save)
                {
                    //�ָ��б�ǩ����ɫ
                    fpPax.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                }
                else
                {
                    //�ָ��б�ǩ����ɫ
                    fpPax.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                    MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();

            pi.PrintType = FarPoint.Win.Spread.PrintType.All;
            pi.ShowColumnHeaders = true;
            pi.ShowRowHeaders = true;
            pi.ShowGrid = true;
            pi.ShowBorder = true;
            pi.ShowShadows = false;
            pi.ShowColor = false;
            pi.UseMax = true;
            pi.BestFitCols = false;
            //			pi.Preview = true;
            pi.ShowPrintDialog = true;



            //			if (objfmClientMain.FpFlightInfo.ZoomFactor < 0.75)
            //			{
            //				pi.ZoomFactor = (float) objfmClientMain.FpFlightInfo.ZoomFactor;
            //			}
            //			else
            //			{
            pi.ZoomFactor = (float)1.8;
            //			}			

            //			pi.Margin = new FarPoint.Win.Spread.PrintMargin(20, 50, 20, 0, 0, 0);

            pi.Margin.Left = 200;
            pi.Margin.Top = 100;
            pi.Margin.Bottom = 50;

            pi.Header = "/fn\"����\"/fz\"14.25\"/fb1/fi0/fu0/fk0/c" + "�ÿ�����ͳ��(" + m_AccountBM.StationThreeCode + DateTime.Now.ToString("yyyy-MM-dd") + ")" + "/n";
            pi.Footer = "��ӡʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            fpPax.Sheets[0].PrintInfo = pi;

            //�������öԻ���
            fpPax.Sheets[0].PrintInfo = pi;
            fpPax.PrintSheet(0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtFlightDate_ValueChanged(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = dtFlightDate.Value.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = dtFlightDate.Value.AddDays(1).ToString("yyyy-MM-dd") + "05:00:00";

            ReturnValueSF rvSF = guaranteeInforBF.GetStatisticPax(dateTimeBM, m_AccountBM.StationThreeCode);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                fpPax.DataSource = rvSF.Dt;
            }
        }
    }
}