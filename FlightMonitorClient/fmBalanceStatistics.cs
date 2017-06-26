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
            #region 传入的数据源是 DataTable 的处理代码 -- 暂不使用
            /*
            //需要检索的列
            string statisticsColumns = ";OutcnvcOutArrangeCargoOperator;OutcnvcOutCheckManifestOperator;OutcnvcOutRecheckManifestOperator;OutcnvcOutTransportManifestOperator;OutcnvcOutUploadManifestOperator;";
            //界面中实际放置的列
            string actualStatisticsColumns = ";";
            //确定界面中实际放置的列
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
            //构建结果表
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
            //界面中实际放置的列 的 数组
            string[] arrayActualStatisticsColumns = actualStatisticsColumns.Trim(';').Split(';');
            //遍历数据源，进行统计
            foreach (DataRow dataRowDataSource in _dataTableDataSource.Rows)
            {
                foreach (string strOperator in arrayActualStatisticsColumns)
                {
                    if (dataRowDataSource[strOperator].ToString().Trim() == "") //如果操作者的值为空，跳过
                    {
                        continue;
                    }

                    DataRow[] arrayDataRowsStatisticsResult = dataTableStatisticsResult.Select("Operator = '" + dataRowDataSource[strOperator].ToString().Trim() + "'");
                    if (arrayDataRowsStatisticsResult.Length > 0) //结果表中 有 此操作者
                    {
                        arrayDataRowsStatisticsResult[0][strOperator + "Count"] = Convert.ToInt32(arrayDataRowsStatisticsResult[0][strOperator + "Count"].ToString()) + 1;
                    }
                    else //结果表中 无 此操作者
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

            //显示结果
            fpFlightInfo.Sheets[0].DataSource = dataTableStatisticsResult;
            */
            #endregion 传入的数据源是 DataTable 的处理代码 -- 暂不使用

            //标题
            this.Text = "配载统计数据： " + _selectDate + " " + _selectStation;

            //需要统计的列
            string actualStatisticsColumns = ";OutcnvcOutArrangeCargoOperator;OutcnvcOutCheckManifestOperator;OutcnvcOutRecheckManifestOperator;OutcnvcOutTransportManifestOperator;OutcnvcOutUploadManifestOperator;";

            //构建显示网格

            fpFlightInfo.ActiveSheet.ColumnCount = 6;

            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 0, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 1, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 2, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 3, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 4, 2, 1);
            fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 5, 2, 1);


            #region -- 测试 -- 开始 --
            //fpFlightInfo.ActiveSheet.RowCount = 6;
            //fpFlightInfo.ActiveSheet.Models.RowHeaderSpan.Add(1, 0, 2, 1);

            //fpFlightInfo.ActiveSheet.Models.Span.Add(1, 0, 2, 1);
            //fpFlightInfo.ActiveSheet.AddSpanCell(1, 1, 2, 3);
            //this.fpSpread1_Sheet1.AddRangeGroup(3,5, true)设置分组后的效果图
             
            #endregion -- 测试 -- 结束 --

            #region -- 测试 -- 开始 --
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 0, 3, 1);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 1, 3, 1);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(0, 2, 1, 4);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(1, 2, 1, 2);
            //fpFlightInfo.ActiveSheet.Models.ColumnHeaderSpan.Add(1, 4, 1, 2);
            #endregion -- 测试 -- 结束 --


            fpFlightInfo.ActiveSheet.Columns[0].DataField = "Operator";
            fpFlightInfo.ActiveSheet.Columns[1].DataField = "OutcnvcOutArrangeCargoOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[2].DataField = "OutcnvcOutCheckManifestOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[3].DataField = "OutcnvcOutRecheckManifestOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[4].DataField = "OutcnvcOutTransportManifestOperatorCount";
            fpFlightInfo.ActiveSheet.Columns[5].DataField = "OutcnvcOutUploadManifestOperatorCount";

            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 0].Text = "姓名";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 1].Text = "配货量";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 2].Text = "舱单核对量";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 3].Text = "舱单复核量";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 4].Text = "送舱单量";
            fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 5].Text = "舱单上传量";

            #region -- 测试 -- 开始 --
            //FarPoint.Win.Spread.CellType.CheckBoxCellType objCheckCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            //objCheckCell.ThreeState = false;
            //fpFlightInfo.ActiveSheet.Columns[6].CellType = objCheckCell;
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 0].Text = "姓名";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 1].Text = "配货量";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[0, 2].Text = "舱单";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[1, 2].Text = "检查舱单";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[1, 4].Text = "传送舱单";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 2].Text = "舱单核对量";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 3].Text = "舱单复核量";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 4].Text = "送舱单量";
            //fpFlightInfo.ActiveSheet.ColumnHeader.Cells[2, 5].Text = "舱单上传量";
            #endregion -- 测试 -- 结束 --

            //构建结果表
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
            //需要统计的列 的 数组
            string[] arrayActualStatisticsColumns = actualStatisticsColumns.Trim(';').Split(';');
            //遍历数据源，进行统计
            IEnumerator ieInOutFlights = _iListDataSource.GetEnumerator();
            //foreach (DataRow dataRowDataSource in _dataTableDataSource.Rows)
            while (ieInOutFlights.MoveNext())
            {
                GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieInOutFlights.Current;

                foreach (string strOperator in arrayActualStatisticsColumns)
                {
                    if (guaranteeInforBM[strOperator].ToString().Trim() == "") //如果操作者的值为空，跳过
                    {
                        continue;
                    }

                    DataRow[] arrayDataRowsStatisticsResult = dataTableStatisticsResult.Select("Operator = '" + guaranteeInforBM[strOperator].ToString().Trim() + "'");
                    if (arrayDataRowsStatisticsResult.Length > 0) //结果表中 有 此操作者
                    {
                        arrayDataRowsStatisticsResult[0][strOperator + "Count"] = Convert.ToInt32(arrayDataRowsStatisticsResult[0][strOperator + "Count"].ToString()) + 1;
                    }
                    else //结果表中 无 此操作者
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

            //显示结果
            fpFlightInfo.Sheets[0].DataSource = dataTableStatisticsResult;
        }

        private void fpFlightInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            #region 弹出右键菜单
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                popNoMenu.Show(fpFlightInfo, p);
            }
            #endregion
        }

        private void 导出统计结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel文件(*.xls)|*.xls";
            //覆盖文件时询问
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel文件另存为";
            saveExcel.FileName = this.Text;
            //设置默认路径
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
                        MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void 导出到PDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FarPoint.Win.Spread.PrintInfo printset = new FarPoint.Win.Spread.PrintInfo();
            //printset.PrintToPdf = true;
            //printset.PdfFileName = "D:\\results.pdf";
            ////设置打印机设置然后打印。
            //fpFlightInfo.Sheets[0].PrintInfo = printset;
            //fpFlightInfo.PrintSheet(0);
        }
    }
}