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

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmQueryGate : Form
    {
        private AccountBM m_accountBM;
        private StationBM m_stationBM;

        public fmQueryGate(AccountBM accountBM, StationBM stationBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
            this.m_stationBM = stationBM;
        }

        private void rmQueryGate_Load(object sender, EventArgs e)
        {
            fpGate.Sheets[0].Models.ColumnHeaderSpan.Add(0, 0, 2, 1);
            fpGate.Sheets[0].ColumnHeader.Cells[0, 0].Text = "����";
            fpGate.Sheets[0].Columns[0].DataField = "IncnvcLONG_REG";

            fpGate.Sheets[0].Models.ColumnHeaderSpan.Add(0, 1, 1, 3);
            fpGate.Sheets[0].ColumnHeader.Cells[0, 1].Text = "���պ���";

            fpGate.Sheets[0].ColumnHeader.Cells[1, 1].Text = "�����";
            fpGate.Sheets[0].Columns[1].DataField = "IncnvcFLTID";
            fpGate.Sheets[0].ColumnHeader.Cells[1, 2].Text = "����ʱ��";
            fpGate.Sheets[0].Columns[2].DataField = "IncncTDWN";
            fpGate.Sheets[0].ColumnHeader.Cells[1, 3].Text = "ͣ��λ";
            fpGate.Sheets[0].Columns[3].DataField = "IncnvcInGATE";



            fpGate.Sheets[0].Models.ColumnHeaderSpan.Add(0, 4, 1, 3);
            fpGate.Sheets[0].ColumnHeader.Cells[0, 4].Text = "����ʼ��";

            fpGate.Sheets[0].ColumnHeader.Cells[1, 4].Text = "ͣ��λ";
            fpGate.Sheets[0].Columns[4].DataField = "OutcnvcOutGATE";
            fpGate.Sheets[0].ColumnHeader.Cells[1, 5].Text = "�����";
            fpGate.Sheets[0].Columns[5].DataField = "OutcnvcFLTID";
            fpGate.Sheets[0].ColumnHeader.Cells[1, 6].Text = "���ʱ��";
            fpGate.Sheets[0].Columns[6].DataField = "OutcncSTD";

            fpGate.Sheets[0].Models.ColumnHeaderSpan.Add(0, 7, 2, 1);
            fpGate.Sheets[0].ColumnHeader.Cells[0, 7].Text = "��ע";
            fpGate.Sheets[0].Columns[7].DataField = "OutcnvcGateRemark";




            fpGate.ActiveSheet.ColumnHeader.DefaultStyle.Border = new FarPoint.Win.LineBorder(Color.Black, 1, false, false, true, true);

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();


            ReturnValueSF rvSF = guaranteeInforBF.GetEndFlight(GetDateTimeBM(1), GetDateTimeBM(2), m_accountBM);

            if (rvSF.Result >= 0)
            {
                fpGate.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtFlightDate_ValueChanged(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();


            ReturnValueSF rvSF = guaranteeInforBF.GetEndFlight(GetDateTimeBM(1), GetDateTimeBM(2), m_accountBM);

            if (rvSF.Result >= 0)
            {
                fpGate.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel�ļ�(*.xls)|*.xls";
            //�����ļ�ʱѯ��
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel�ļ����Ϊ";
            saveExcel.FileName = "�����λ����" + dtFlightDate.Value.ToString("yyyy-MM-dd");
            //����Ĭ��·��
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                //����ǰ�����б�ǩ����ɫ�ĳ�Silver���Ա㵼����Excel�ļ���ͷ��ɫ���������ֿ���
                fpGate.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");

                string fileName = saveExcel.FileName;
                bool save = fpGate.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                if (save)
                {
                    //�ָ��б�ǩ����ɫ
                    fpGate.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                }
                else
                {
                    //�ָ��б�ǩ����ɫ
                    fpGate.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

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
            pi.ZoomFactor = (float)1.3;
            //			}			

            //			pi.Margin = new FarPoint.Win.Spread.PrintMargin(20, 50, 20, 0, 0, 0);

            pi.Margin.Left = 50;
            pi.Margin.Top = 100;
            pi.Margin.Bottom = 50;

            pi.Header = "/fn\"����\"/fz\"14.25\"/fb1/fi0/fu0/fk0/c" + "�����λ����(" + m_stationBM.AirportName + DateTime.Now.ToString("yyyy-MM-dd") + ")" + "/n";
            pi.Footer = "��ӡʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            fpGate.Sheets[0].PrintInfo = pi;

            //�������öԻ���
            fpGate.Sheets[0].PrintInfo = pi;
            fpGate.PrintSheet(0);
        }

        /// <summary>
        /// �رմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ��ȡĳ��ʱ�䷶Χ
        /// </summary>
        /// <param name="iDay">��һ��0:����1:����2:����3:ѡ�������</param>
        private DateTimeBM GetDateTimeBM(int iDay)
        {
            DateTimeBM dataTimeBM = new DateTimeBM();

            string strStartDateTime;
            string strEndDateTime;

            if (iDay == 0)  //����
            {
                strStartDateTime = dtFlightDate.Value.AddHours(-5).Date.AddDays(-1).ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = dtFlightDate.Value.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else if (iDay == 1) //����ʱ�䷶Χʵ�����
            {
                strStartDateTime = dtFlightDate.Value.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = dtFlightDate.Value.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else if (iDay == 2) //����ʱ�䷶Χʵ�����
            {
                strStartDateTime = dtFlightDate.Value.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = dtFlightDate.Value.AddHours(-5).Date.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else   //��ѡ����
            {
                strStartDateTime = dtFlightDate.Value.Date.ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = dtFlightDate.Value.Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            }


            dataTimeBM.StartDateTime = strStartDateTime;
            dataTimeBM.EndDateTime = strEndDateTime;

            return dataTimeBM;
        }
    }
}