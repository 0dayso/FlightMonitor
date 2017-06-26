using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using AirSoft.FlightMonitor.FlightMonitorBM;


namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmBalanceStatistics : Form
    {
        string _selectDate;
        string _selectStation;
        DataTable _dataTableDataSource;
        IList _iListDataSource;

        public fmBalanceStatistics()
        {
            InitializeComponent();
        }

        public fmBalanceStatistics(string selectDate, string selectStation, DataTable dataTableDataSource)
        {
            _selectDate = selectDate;
            _selectStation = selectStation;
            _dataTableDataSource = dataTableDataSource;

            InitializeComponent();
        }

        public fmBalanceStatistics(string selectDate, string selectStation, IList iListDataSource)
        {
            _selectDate = selectDate;
            _selectStation = selectStation;
            _iListDataSource = iListDataSource;

            InitializeComponent();
        }

        private void fmBalanceStatistics_Load(object sender, EventArgs e)
        {
            #region ���������Դ�� DataTable �Ĵ������ -- �ݲ�ʹ��
            /*
            //��Ҫ��������
            string statisticsColumns = ";OutcnvcOutArrangeCargoOperator;OutcnvcOutCheckManifestOperator;OutcnvcOutRecheckManifestOperator;OutcnvcOutTransportManifestOperator;OutcnvcOutUploadManifestOperator;";
            //������ʵ�ʷ��õ���
            string actualStatisticsColumns = ";";
            //ȷ��������ʵ�ʷ��õ���
            if (_dataTableDataSource != null)
            {
                foreach (DataColumn dataColumn in _dataTableDataSource.Columns)
                {
                    if (statisticsColumns.IndexOf(";" + dataColumn.ColumnName + ";") > 0)
                    {
                        actualStatisticsColumns = actualStatisticsColumns + dataColumn.ColumnName + ";";
                    }
                }
            }
            //���������
            DataTable dataTableStatisticsResult = new DataTable();
            DataColumn dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("Operator", typeof(string));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutArrangeCargoOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutCheckManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutRecheckManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutTransportManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutUploadManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            //������ʵ�ʷ��õ��� �� ����
            string[] arrayActualStatisticsColumns = actualStatisticsColumns.Trim(';').Split(';');
            //��������Դ������ͳ��
            foreach (DataRow dataRowDataSource in _dataTableDataSource.Rows)
            {
                foreach (string strOperator in arrayActualStatisticsColumns)
                {
                    if (dataRowDataSource[strOperator].ToString().Trim() == "") //��������ߵ�ֵΪ�գ�����
                    {
                        continue;
                    }

                    DataRow[] arrayDataRowsStatisticsResult = dataTableStatisticsResult.Select("Operator = '" + dataRowDataSource[strOperator].ToString().Trim() + "'");
                    if (arrayDataRowsStatisticsResult.Length > 0) //������� �� �˲�����
                    {
                        arrayDataRowsStatisticsResult[0][strOperator + "Count"] = Convert.ToInt32(arrayDataRowsStatisticsResult[0][strOperator + "Count"].ToString()) + 1;
                    }
                    else //������� �� �˲�����
                    {
                        DataRow dataRowNewStatisticsResult = dataTableStatisticsResult.NewRow();
                        dataRowNewStatisticsResult["Operator"] = dataRowDataSource[strOperator].ToString().Trim();
                        dataRowNewStatisticsResult["OutcnvcOutArrangeCargoOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutCheckManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutRecheckManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutTransportManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutUploadManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult[strOperator + "Count"] = 1;
                        dataTableStatisticsResult.Rows.Add(dataRowNewStatisticsResult);
                    }
                }
            }

            //��ʾ���
            fpFlightInfo.Sheets[0].DataSource = dataTableStatisticsResult;
            */
            #endregion ���������Դ�� DataTable �Ĵ������ -- �ݲ�ʹ��

            //����
            this.Text = "����ͳ�����ݣ� " + _selectDate + " " + _selectStation;

            //��Ҫͳ�Ƶ���
            string actualStatisticsColumns = ";OutcnvcOutArrangeCargoOperator;OutcnvcOutCheckManifestOperator;OutcnvcOutRecheckManifestOperator;OutcnvcOutTransportManifestOperator;OutcnvcOutUploadManifestOperator;";

            //������ʾ����

            fpFlightInfo.ActiveSheet.ColumnCount = 6;

            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 0, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 1, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 2, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 3, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 4, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 5, 2, 1);


            #region -- ���� -- ��ʼ --
            //fpFlightInfo.ActiveSheet.RowCount = 6;
            //fpFlightInfo.ActiveSheet.Models.RowHeaderSpan.Add(1, 0, 2, 1);

            //fpFlightInfo.ActiveSheet.Models.Span.Add(1, 0, 2, 1);
            //fpFlightInfo.ActiveSheet.AddSpanCell(1, 1, 2, 3);
            //this.fpSpread1_Sheet1.AddRangeGroup(3,5, true)���÷�����Ч��ͼ
             
            #endregion -- ���� -- ���� --

            #region -- ���� -- ��ʼ --
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 0, 3, 1);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 1, 3, 1);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 2, 1, 4);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(1, 2, 1, 2);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(1, 4, 1, 2);
            #endregion -- ���� -- ���� --


            fpFlightInfo.ActiveSheet.Columns[0].DataField = "Operator";
            fpFlightInfo.ActiveSheet.Columns[1].DataField = "OutcnvcOutArrangeCargoOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[2].DataField = "OutcnvcOutCheckManifestOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[3].DataField = "OutcnvcOutRecheckManifestOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[4].DataField = "OutcnvcOutTransportManifestOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[5].DataField = "OutcnvcOutUploadManifestOperatorCount";

            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 0].Text = "����";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 1].Text = "�����";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 2].Text = "�յ��˶���";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 3].Text = "�յ�������";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 4].Text = "�Ͳյ���";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 5].Text = "�յ��ϴ���";

            #region -- ���� -- ��ʼ --
            //FarPoint.Win.Spread.CellType.CheckBoxCellType objCheckCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            //objCheckCell.ThreeState = false;
            //fpFlightInfo.ActiveSheet.Columns[6].CellType = objCheckCell;
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 0].Text = "����";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 1].Text = "�����";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 2].Text = "�յ�";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[1, 2].Text = "���յ�";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[1, 4].Text = "���Ͳյ�";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 2].Text = "�յ��˶���";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 3].Text = "�յ�������";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 4].Text = "�Ͳյ���";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 5].Text = "�յ��ϴ���";
            #endregion -- ���� -- ���� --

            //���������
            DataTable dataTableStatisticsResult = new DataTable();
            DataColumn dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("Operator", typeof(string));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutArrangeCargoOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutCheckManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutRecheckManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutTransportManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            dataColumnDataTableStatisticsResult = dataTableStatisticsResult.Columns.Add("OutcnvcOutUploadManifestOperatorCount", typeof(int));
            dataColumnDataTableStatisticsResult.DefaultValue = 0;
            //��Ҫͳ�Ƶ��� �� ����
            string[] arrayActualStatisticsColumns = actualStatisticsColumns.Trim(';').Split(';');
            //��������Դ������ͳ��
            IEnumerator ieInOutFlights = _iListDataSource.GetEnumerator();
            //foreach (DataRow dataRowDataSource in _dataTableDataSource.Rows)
            while (ieInOutFlights.MoveNext())
            {
                GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieInOutFlights.Current;

                foreach (string strOperator in arrayActualStatisticsColumns)
                {
                    if (guaranteeInforBM[strOperator].ToString().Trim() == "") //��������ߵ�ֵΪ�գ�����
                    {
                        continue;
                    }

                    DataRow[] arrayDataRowsStatisticsResult = dataTableStatisticsResult.Select("Operator = '" + guaranteeInforBM[strOperator].ToString().Trim() + "'");
                    if (arrayDataRowsStatisticsResult.Length > 0) //������� �� �˲�����
                    {
                        arrayDataRowsStatisticsResult[0][strOperator + "Count"] = Convert.ToInt32(arrayDataRowsStatisticsResult[0][strOperator + "Count"].ToString()) + 1;
                    }
                    else //������� �� �˲�����
                    {
                        DataRow dataRowNewStatisticsResult = dataTableStatisticsResult.NewRow();
                        dataRowNewStatisticsResult["Operator"] = guaranteeInforBM[strOperator].ToString().Trim();
                        dataRowNewStatisticsResult["OutcnvcOutArrangeCargoOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutCheckManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutRecheckManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutTransportManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult["OutcnvcOutUploadManifestOperatorCount"] = 0;
                        dataRowNewStatisticsResult[strOperator + "Count"] = 1;
                        dataTableStatisticsResult.Rows.Add(dataRowNewStatisticsResult);
                    }
                }
            }

            //��ʾ���
            fpFlightInfo.Sheets[0].DataSource = dataTableStatisticsResult;
        }

        private void fpFlightInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            #region �����Ҽ��˵�
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                popNoMenu.Show(fpFlightInfo, p);
            }
            #endregion
        }

        private void ����ͳ�ƽ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel�ļ�(*.xls)|*.xls";
            //�����ļ�ʱѯ��
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel�ļ����Ϊ";
            saveExcel.FileName = this.Text;
            //����Ĭ��·��
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                string fileName = saveExcel.FileName;
                try
                {
                    bool save = fpFlightInfo.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    if (save)
                    {

                    }
                    else
                    {
                        MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ������PDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FarPoint.Win.Spread.PrintInfo printset = new FarPoint.Win.Spread.PrintInfo();
            //printset.PrintToPdf = true;
            //printset.PdfFileName = "D:\\results.pdf";
            ////���ô�ӡ������Ȼ���ӡ��
            //fpFlightInfo.Sheets[0].PrintInfo = printset;
            //fpFlightInfo.PrintSheet(0);
        }
    }
}