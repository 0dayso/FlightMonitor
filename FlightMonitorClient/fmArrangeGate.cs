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
    public partial class fmArrangeGate : Form
    {
        private AccountBM m_accountBM;
        private StationBM m_stationBM;
        /// <summary>
        /// ���캯��
        /// </summary>
        public fmArrangeGate(AccountBM accountBM, StationBM stationBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
            this.m_stationBM = stationBM;
        }

        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmArrangeGate_Load(object sender, EventArgs e)
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

            if(rvSF.Result >= 0)
            {
                fpGate.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

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


        private void btnSave_Click(object sender, EventArgs e)
        {
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            #region modified by LinYong in 20151119
            //FocService.FleetWatch objFocService = new FocService.FleetWatch();
            FocService.FocService objFocService = new FocService.FocService();
            #endregion modified by LinYong in 20151119
            DataTable dtInOutFlight = (DataTable)fpGate.DataSource;
            int iRowIndex = 0;
            foreach (DataRow dataRow in dtInOutFlight.Rows)
            {

                if (fpGate.ActiveSheet.Cells[iRowIndex, 3].Text.Length > 10 || fpGate.ActiveSheet.Cells[iRowIndex, 4].Text.Length > 10)
                {
                    MessageBox.Show("ͣ��λ�Ϊ10λ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                //�ж��Ƿ�������
                LinYong.PublicService.PublicService PublicServiceObj = new LinYong.PublicService.PublicService();
                if (PublicServiceObj.ContainChinese(fpGate.ActiveSheet.Cells[iRowIndex, 3].Text) ||
                    PublicServiceObj.ContainChinese(fpGate.ActiveSheet.Cells[iRowIndex, 4].Text))
                {
                    MessageBox.Show("ͣ��λ��Ϣ���ܺ������ģ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (dataRow["IncnvcInGATE"].ToString() == "")
                {
                    dataRow["IncnvcInGATE"] = "";
                }
                if (dataRow["OutcnvcOutGATE"].ToString() == "")
                {
                    dataRow["OutcnvcOutGATE"] = "";
                }

                if (dataRow["OutcnvcGateRemark"].ToString() == "")
                {
                    dataRow["OutcnvcGateRemark"] = "";
                }



                if (dataRow["IncnvcFLTID"].ToString().Trim() != "")
                {
                    MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                    maintenGuaranteeInforBM.DATOP = dataRow["IncncDATOP"].ToString();
                    maintenGuaranteeInforBM.FLTID = dataRow["IncnvcFLTID"].ToString();
                    maintenGuaranteeInforBM.LEGNO = dataRow["IncniLEGNO"].ToString();
                    maintenGuaranteeInforBM.AC = dataRow["IncnvcAC"].ToString();
                    maintenGuaranteeInforBM.FieldName = "cnvcInGATE";
                    maintenGuaranteeInforBM.FieldType = 1;
                    maintenGuaranteeInforBM.NewContent = fpGate.ActiveSheet.Cells[iRowIndex, 3].Text.ToUpper();
                    guaranteeInforBF.UpdateGuaranteeInfor(maintenGuaranteeInforBM);

                    //����ͣ��λ
                    string gate = fpGate.ActiveSheet.Cells[iRowIndex, 3].Text;
                    if (gate == "")
                    {
                        gate = "UNK";
                    }
                    string strSTD = dataRow["IncncFlightDate"].ToString() + " " + dataRow["IncncSTD"].ToString().Substring(0, 2) + ":" + dataRow["IncncSTD"].ToString().Substring(2, 2) + ":00";
                    string strSTA = dataRow["IncncFlightDate"].ToString() + " " + dataRow["IncncSTA"].ToString().Substring(0, 2) + ":" + dataRow["IncncSTA"].ToString().Substring(2, 2) + ":00";
                    string strInGate = dataRow["IncnvcFLTID"].ToString().Replace(" ", "");
                    strInGate += "/" + DateTime.Parse(strSTD).ToUniversalTime().ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
                    strInGate += "%%";

                    strInGate += dataRow["IncncDEPSTN"].ToString() + DateTime.Parse(strSTD).ToUniversalTime().ToString("HHmm") + " ";
                    strInGate += dataRow["IncncARRSTN"].ToString() + DateTime.Parse(strSTA).ToUniversalTime().ToString("HHmm") + "%%";

                    strInGate += dataRow["IncncDEPSTN"].ToString() + "/UNK" + " " + dataRow["IncncARRSTN"].ToString()  + "/" + gate + "%%";


                    objFocService.InsertGate(strInGate);

                }

                if (dataRow["OutcnvcFLTID"].ToString().Trim() != "")
                {
                    MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                    maintenGuaranteeInforBM.DATOP = dataRow["OutcncDATOP"].ToString();
                    maintenGuaranteeInforBM.FLTID = dataRow["OutcnvcFLTID"].ToString();
                    maintenGuaranteeInforBM.LEGNO = dataRow["OutcniLEGNO"].ToString();
                    maintenGuaranteeInforBM.AC = dataRow["OutcnvcAC"].ToString();
                    maintenGuaranteeInforBM.FieldName = "cnvcOutGATE";
                    maintenGuaranteeInforBM.FieldType = 1;
                    maintenGuaranteeInforBM.NewContent = fpGate.ActiveSheet.Cells[iRowIndex, 4].Text.ToUpper();
                    guaranteeInforBF.UpdateGuaranteeInfor(maintenGuaranteeInforBM);

                    maintenGuaranteeInforBM.FieldName = "cnvcGateRemark";
                    maintenGuaranteeInforBM.NewContent = fpGate.ActiveSheet.Cells[iRowIndex, 7].Text.ToUpper();
                    guaranteeInforBF.UpdateGuaranteeInfor(maintenGuaranteeInforBM);


                    //����ͣ��λ
                    string gate = fpGate.ActiveSheet.Cells[iRowIndex, 4].Text.Trim().ToUpper();
                    if (gate == "")
                    {
                        gate = "UNK";
                    }

                    string strSTD = dataRow["OutcncFlightDate"].ToString() + " " + dataRow["OutcncSTD"].ToString().Substring(0, 2) + ":" + dataRow["OutcncSTD"].ToString().Substring(2, 2) + ":00";
                    string strSTA = dataRow["OutcncFlightDate"].ToString() + " " + dataRow["OutcncSTA"].ToString().Substring(0, 2) + ":" + dataRow["OutcncSTA"].ToString().Substring(2, 2) + ":00";
                    string strOutGate = dataRow["OutcnvcFLTID"].ToString().Replace(" ", "");
                    strOutGate += "/" + DateTime.Parse(strSTD).ToUniversalTime().ToString("ddMMMyy", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToUpper();
                    strOutGate += "%%";
                    /*
                    strOutGate += dataRow["IncncDEPSTN"].ToString() + DateTime.Parse(strSTD).ToUniversalTime().ToString("HHmm") + " ";
                    strOutGate += dataRow["IncncARRSTN"].ToString() + DateTime.Parse(strSTA).ToUniversalTime().ToString("HHmm") + "%%";

                    strOutGate += dataRow["IncncDEPSTN"].ToString() + "/" + gate + " " + dataRow["IncncARRSTN"].ToString() + "/UNK%%";
                    */
                    strOutGate += dataRow["OutcncDEPSTN"].ToString() + DateTime.Parse(strSTD).ToUniversalTime().ToString("HHmm") + " ";
                    strOutGate += dataRow["OutcncARRSTN"].ToString() + DateTime.Parse(strSTA).ToUniversalTime().ToString("HHmm") + "%%";

                    strOutGate += dataRow["OutcncDEPSTN"].ToString() + "/" + gate + " " + dataRow["OutcncARRSTN"].ToString() + "/UNK%%";

                    objFocService.InsertGate(strOutGate);                   
                }

                iRowIndex += 1;
            }

            MessageBox.Show("����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// ������EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// ��ͬ���ڵĺ��󺽰�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}