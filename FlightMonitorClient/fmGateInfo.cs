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
    public partial class fmGateInfo : Form
    {
        private AccountBM _accountBM;                              //登陆用户实体对象
        private string _stationNamee;                              //场站名称
        private string _airportName;                               //机场名称


        public fmGateInfo()
        {
            InitializeComponent();
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountBM">登陆用户</param>
        public fmGateInfo(AccountBM accountBM, string stationName, string airportName)
        {
            _accountBM = accountBM;
            _stationNamee = stationName;
            _airportName = airportName;
        
            InitializeComponent();
        }
        #endregion 构造函数

        private void fmGateInfo_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        
        /// <summary>
        /// 加载停机位数据
        /// </summary>
        private void LoadData()
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();
            GateInfoBF gateInfoBF = new GateInfoBF();

            returnValueSF = gateInfoBF.GetList(_accountBM.StationThreeCode);
            if (returnValueSF.Result > 0)
            {
                #region 为了在格式化界面效果中快速定位行在数据表中位置使用
                DataTable dataTableResult = returnValueSF.Dt;
                dataTableResult.Columns.Add("cnvcIndex", typeof(string));
                dataTableResult.Columns.Add("cniIndex", typeof(string));
                for (int iIndex = 0; iIndex < dataTableResult.Rows.Count; iIndex++)
                {
                    dataTableResult.Rows[iIndex]["cnvcIndex"] = Convert.ToString(iIndex + 1);
                    dataTableResult.Rows[iIndex]["cniIndex"] = iIndex + 1;

                }
                dataTableResult.AcceptChanges();

                #endregion 为了在格式化界面效果中快速定位行在数据表中位置使用

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = returnValueSF.Dt;

                #region 为了在格式化界面效果中快速定位使用
                //DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
                //dataGridViewTextBoxColumn.Name = "dataGridViewTextBoxColumn_cnvcIndex";
                //dataGridView1.Columns.Add(dataGridViewTextBoxColumn);
                //dataGridViewTextBoxColumn.DisplayIndex = 1;

                //for (int iIndex = 0; iIndex < (dataGridView1.Rows.Count - 1); iIndex++)
                //{
                //    //string s1 = dataGridView1.Rows[iIndex].Cells["cnvcGate"].Value.ToString();
                //    //string s2 = dataGridView1.Rows[iIndex].Cells["cndOperationTime"].Value.ToString();
                //    dataGridView1.Rows[iIndex].Cells["dataGridViewTextBoxColumn_cnvcIndex"].Value = Convert.ToString(iIndex + 1);
                //}

                #endregion 为了在格式化界面效果中快速定位使用

                #region 数据列
                foreach (DataColumn dataColumn in returnValueSF.Dt.Columns)
                {
                    switch (dataColumn.ColumnName.ToLower().Trim())
                    {
                        case "cncstationthreecode":
                            dataColumn.DefaultValue = _accountBM.StationThreeCode ;
                            break;
                        case "cnvcairportname":
                            dataColumn.DefaultValue = _airportName;
                            break;
                        case "cnvcstationname":
                            dataColumn.DefaultValue = _stationNamee ;
                            break;
                        case "cnvcoperationuser":
                            dataColumn.DefaultValue =
                                _accountBM.UserName +
                                "(" +
                                _accountBM.UserId +
                                ")";
                            break;
                    }
                }
                #endregion 数据列

                #region 网格标题
                DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
                dataGridViewComboBoxColumn.Name = "dataGridViewComboBoxColumn_cnvcGateProperty" ;
                dataGridViewComboBoxColumn.DataPropertyName = "cnvcGateProperty";
                dataGridViewComboBoxColumn.Items.Add("远机位");
                dataGridViewComboBoxColumn.Items.Add("近机位");
                dataGridView1.Columns.Add(dataGridViewComboBoxColumn);
                dataGridViewComboBoxColumn.DisplayIndex = 4;
                dataGridViewComboBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic; //否则此列无法排序

                dataGridView1.Columns["cniindex"].DisplayIndex = 0;

                foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
                {
                    switch (dataGridViewColumn.DataPropertyName.ToLower().Trim())
                    {
                        case "cncstationthreecode":
                            dataGridViewColumn.HeaderText = "机场三字码";
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cnvcairportname":
                            dataGridViewColumn.HeaderText = "机场名称";
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cnvcstationname":
                            dataGridViewColumn.HeaderText = "场站名称";
                            dataGridViewColumn.ReadOnly = true;
                            dataGridViewColumn.Frozen = true;
                            break;
                        case "cnvcgate":
                            dataGridViewColumn.HeaderText = "停机位";
                            break;
                        case "cnvcgateproperty":
                            dataGridViewColumn.HeaderText = "属性";
                            if (dataGridViewColumn.Name.ToLower().Trim() == "cnvcgateproperty")
                                dataGridViewColumn.Visible = false;
                            break;
                        case "cnvcmemo":
                            dataGridViewColumn.HeaderText = "备注";
                            break;
                        case "cnvcoperationuser":
                            dataGridViewColumn.HeaderText = "操作用户";
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cndoperationtime":
                            dataGridViewColumn.HeaderText = "操作时间";
                            dataGridViewColumn.Width = 120;
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cniindex":
                            dataGridViewColumn.HeaderText = "序号";
                            dataGridViewColumn.ReadOnly = true;
                            //dataGridViewColumn.DisplayIndex = 1; 
                            break;
                        case "cnvcindex":
                            dataGridViewColumn.Visible = false;
                            break;

                    }
                }
                #endregion 网格标题

            }
            else
                MessageBox.Show(returnValueSF.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GateInfoBF gateInfoBF = new GateInfoBF();
            ReturnValueSF returnValueSF = new ReturnValueSF();

            if (dataGridView1.DataSource != null)
            {
                //如果没有发生变更的操作，退出
                if ((dataGridView1.DataSource as DataTable).GetChanges() == null)
                    return;

                #region 为了在格式化界面效果中快速定位使用
                (dataGridView1.DataSource as DataTable).Columns.Remove("cnvcIndex");
                #endregion 为了在格式化界面效果中快速定位使用

                returnValueSF = gateInfoBF.Save((dataGridView1.DataSource as DataTable));
                if (returnValueSF.Result > 0)
                {
                    MessageBox.Show("保存成功！", "提示");
                }
                else
                    MessageBox.Show(("保存失败，将重新加载数据。" + Environment.NewLine + "失败原因：" + returnValueSF.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoadData();

            }
            else
                MessageBox.Show("还未连接数据源！", "提示");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
                (dataGridView1.DataSource as DataTable).RejectChanges();
            else
                MessageBox.Show("还未连接数据源！", "提示");
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            //{
            //    string LogonUser = 
            //        _accountBM.UserName +
            //        "(" +
            //        _accountBM.UserId +
            //        ")"; ;
            //    if (dataGridView1.Rows[e.RowIndex].Cells["cnvcOperationUser"].Value.ToString() != LogonUser)
            //        dataGridView1.Rows[e.RowIndex].Cells["cnvcOperationUser"].Value = LogonUser;

            //    dataGridView1.Rows[e.RowIndex].Cells["cndOperationTime"].Value = DateTime.Now;

            //    //
            //    if (dataGridView1.Rows[e.RowIndex].IsNewRow)
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells["cniIndex"].Value = dataGridView1.Rows.Count;
            //    }
            //}
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.Value != null))
                {
                    DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];

                    #region
                    //DataRow[] dataRowsDataGridView1 = (dataGridView1.DataSource as DataTable).Select(
                    //    "(cncStationThreeCode = '" + dataGridView1["cncStationThreeCode", e.RowIndex] + "') and (cnvcGate = '" + dataGridView1["cnvcGate", e.RowIndex] + "')");
                    //if ()
                    #endregion
                    #region
                    //if (e.RowIndex < (dataGridView1.DataSource as DataTable).Rows.Count)
                    //{
                    //    if (((dataGridView1.DataSource as DataTable).Rows[e.RowIndex].RowState != DataRowState..Added) &&
                    //        ((dataGridView1.DataSource as DataTable).Rows[e.RowIndex].RowState != DataRowState.Deleted))
                    //    {
                    //        if ((dataGridView1.DataSource as DataTable).Rows[e.RowIndex][cell.OwningColumn.DataPropertyName, DataRowVersion.Original].ToString() !=
                    //            (dataGridView1.DataSource as DataTable).Rows[e.RowIndex][cell.OwningColumn.DataPropertyName].ToString())
                    //        {
                    //            e.CellStyle.ForeColor = Color.Blue;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        e.CellStyle.ForeColor = Color.Blue;
                    //    }
                    //}
                    #endregion

                    #region
                    if (dataGridView1.Columns.Contains("cnvcIndex"))
                    {
                        if (dataGridView1["cnvcIndex", e.RowIndex].Value.ToString().Trim() != "")
                        {
                            if ((dataGridView1.DataSource as DataTable).Rows[Convert.ToInt32(dataGridView1["cnvcIndex", e.RowIndex].Value.ToString().Trim()) - 1][cell.OwningColumn.DataPropertyName, DataRowVersion.Original].ToString() !=
                                (dataGridView1.DataSource as DataTable).Rows[Convert.ToInt32(dataGridView1["cnvcIndex", e.RowIndex].Value.ToString().Trim()) - 1][cell.OwningColumn.DataPropertyName].ToString())
                            {
                                e.CellStyle.ForeColor = Color.Blue;
                            }
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Blue;
                        }
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                string s = "";
                s = ex.Message;
                s = s ;
            }

        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    if (e.RowIndex > 0)
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells["cniIndex"].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex - 1].Cells["cniIndex"].Value.ToString()) + 1;
            //    }
            //    else if (e.RowIndex == 0)
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells["cniIndex"].Value = 1;
            //    }
            //}
            //if (dataGridView1.Rows.Count == 1)
            //    e.Row.Cells["cniIndex"].Value = 1;
            //else
            //    e.Row.Cells["cniIndex"].Value = dataGridView1.Rows.Count ;

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                string LogonUser =
                    _accountBM.UserName +
                    "(" +
                    _accountBM.UserId +
                    ")"; ;
                if (dataGridView1.Rows[e.RowIndex].Cells["cnvcOperationUser"].Value.ToString() != LogonUser)
                    dataGridView1.Rows[e.RowIndex].Cells["cnvcOperationUser"].Value = LogonUser;

                dataGridView1.Rows[e.RowIndex].Cells["cndOperationTime"].Value = DateTime.Now;

                //
                //if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                //{
                //    dataGridView1.Rows[e.RowIndex].Cells["cniIndex"].Value = dataGridView1.Rows.Count;
                //}
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                //
                if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["cniIndex"].Value = dataGridView1.Rows.Count;
                }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            //{
            //    //
            //    if (dataGridView1.Rows[e.RowIndex].IsNewRow)
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells["cniIndex"].Value = dataGridView1.Rows.Count;
            //    }
            //}
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.Columns[e.ColumnIndex].Name.ToLower().Trim() == "datagridviewcomboboxcolumn_cnvcgateproperty")
            //    {
            //        if (dataGridView1.Columns["dataGridViewComboBoxColumn_cnvcGateProperty"].HeaderCell.SortGlyphDirection != SortOrder.Ascending)
            //        {
            //            dataGridView1.Columns["dataGridViewComboBoxColumn_cnvcGateProperty"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            //            dataGridView1.Sort(dataGridView1.Columns["cnvcGateProperty"], ListSortDirection.Ascending);
            //        }
            //        else
            //        {
            //            dataGridView1.Columns["dataGridViewComboBoxColumn_cnvcGateProperty"].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            //            dataGridView1.Sort(dataGridView1.Columns["cnvcGateProperty"], ListSortDirection.Descending);

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string s = ex.Message;
            //    s = s;
            //}
        }
    }
}