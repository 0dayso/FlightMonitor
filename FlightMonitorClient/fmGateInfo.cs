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
        private AccountBM _accountBM;                              //��½�û�ʵ�����
        private string _stationNamee;                              //��վ����
        private string _airportName;                               //��������


        public fmGateInfo()
        {
            InitializeComponent();
        }

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="accountBM">��½�û�</param>
        public fmGateInfo(AccountBM accountBM, string stationName, string airportName)
        {
            _accountBM = accountBM;
            _stationNamee = stationName;
            _airportName = airportName;
        
            InitializeComponent();
        }
        #endregion ���캯��

        private void fmGateInfo_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        
        /// <summary>
        /// ����ͣ��λ����
        /// </summary>
        private void LoadData()
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();
            GateInfoBF gateInfoBF = new GateInfoBF();

            returnValueSF = gateInfoBF.GetList(_accountBM.StationThreeCode);
            if (returnValueSF.Result > 0)
            {
                #region Ϊ���ڸ�ʽ������Ч���п��ٶ�λ�������ݱ���λ��ʹ��
                DataTable dataTableResult = returnValueSF.Dt;
                dataTableResult.Columns.Add("cnvcIndex", typeof(string));
                dataTableResult.Columns.Add("cniIndex", typeof(string));
                for (int iIndex = 0; iIndex < dataTableResult.Rows.Count; iIndex++)
                {
                    dataTableResult.Rows[iIndex]["cnvcIndex"] = Convert.ToString(iIndex + 1);
                    dataTableResult.Rows[iIndex]["cniIndex"] = iIndex + 1;

                }
                dataTableResult.AcceptChanges();

                #endregion Ϊ���ڸ�ʽ������Ч���п��ٶ�λ�������ݱ���λ��ʹ��

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = returnValueSF.Dt;

                #region Ϊ���ڸ�ʽ������Ч���п��ٶ�λʹ��
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

                #endregion Ϊ���ڸ�ʽ������Ч���п��ٶ�λʹ��

                #region ������
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
                #endregion ������

                #region �������
                DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
                dataGridViewComboBoxColumn.Name = "dataGridViewComboBoxColumn_cnvcGateProperty" ;
                dataGridViewComboBoxColumn.DataPropertyName = "cnvcGateProperty";
                dataGridViewComboBoxColumn.Items.Add("Զ��λ");
                dataGridViewComboBoxColumn.Items.Add("����λ");
                dataGridView1.Columns.Add(dataGridViewComboBoxColumn);
                dataGridViewComboBoxColumn.DisplayIndex = 4;
                dataGridViewComboBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic; //��������޷�����

                dataGridView1.Columns["cniindex"].DisplayIndex = 0;

                foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
                {
                    switch (dataGridViewColumn.DataPropertyName.ToLower().Trim())
                    {
                        case "cncstationthreecode":
                            dataGridViewColumn.HeaderText = "����������";
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cnvcairportname":
                            dataGridViewColumn.HeaderText = "��������";
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cnvcstationname":
                            dataGridViewColumn.HeaderText = "��վ����";
                            dataGridViewColumn.ReadOnly = true;
                            dataGridViewColumn.Frozen = true;
                            break;
                        case "cnvcgate":
                            dataGridViewColumn.HeaderText = "ͣ��λ";
                            break;
                        case "cnvcgateproperty":
                            dataGridViewColumn.HeaderText = "����";
                            if (dataGridViewColumn.Name.ToLower().Trim() == "cnvcgateproperty")
                                dataGridViewColumn.Visible = false;
                            break;
                        case "cnvcmemo":
                            dataGridViewColumn.HeaderText = "��ע";
                            break;
                        case "cnvcoperationuser":
                            dataGridViewColumn.HeaderText = "�����û�";
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cndoperationtime":
                            dataGridViewColumn.HeaderText = "����ʱ��";
                            dataGridViewColumn.Width = 120;
                            dataGridViewColumn.ReadOnly = true;
                            break;
                        case "cniindex":
                            dataGridViewColumn.HeaderText = "���";
                            dataGridViewColumn.ReadOnly = true;
                            //dataGridViewColumn.DisplayIndex = 1; 
                            break;
                        case "cnvcindex":
                            dataGridViewColumn.Visible = false;
                            break;

                    }
                }
                #endregion �������

            }
            else
                MessageBox.Show(returnValueSF.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GateInfoBF gateInfoBF = new GateInfoBF();
            ReturnValueSF returnValueSF = new ReturnValueSF();

            if (dataGridView1.DataSource != null)
            {
                //���û�з�������Ĳ������˳�
                if ((dataGridView1.DataSource as DataTable).GetChanges() == null)
                    return;

                #region Ϊ���ڸ�ʽ������Ч���п��ٶ�λʹ��
                (dataGridView1.DataSource as DataTable).Columns.Remove("cnvcIndex");
                #endregion Ϊ���ڸ�ʽ������Ч���п��ٶ�λʹ��

                returnValueSF = gateInfoBF.Save((dataGridView1.DataSource as DataTable));
                if (returnValueSF.Result > 0)
                {
                    MessageBox.Show("����ɹ���", "��ʾ");
                }
                else
                    MessageBox.Show(("����ʧ�ܣ������¼������ݡ�" + Environment.NewLine + "ʧ��ԭ��" + returnValueSF.Message), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoadData();

            }
            else
                MessageBox.Show("��δ��������Դ��", "��ʾ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
                (dataGridView1.DataSource as DataTable).RejectChanges();
            else
                MessageBox.Show("��δ��������Դ��", "��ʾ");
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