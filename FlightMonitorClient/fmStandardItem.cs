using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmStandardItem : Form
    {
        private AirportInforBF airportInfoBF = new AirportInforBF();
        private AC_MISCBF actypeBF = new AC_MISCBF();
        private AccountBM m_AccountBM;
        private DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
        private StandardItemBF standardItemBF = new StandardItemBF();

        private DataTable standardDataItemList = new DataTable();// ��ǰ�����б�
        private StandardBM currentStandardBM = new StandardBM(); // ��ǰѡ�е�һ��

        private DataTable standardCNNameList = new DataTable();

        public fmStandardItem()
        {
            InitializeComponent();
        }

        public fmStandardItem(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_AccountBM = accountBM;
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmStandardItemAdd_Load(object sender, EventArgs e)
        {
            //��ȡ���������б�
            standardDataItemList = standardItemBF.GetStandardItemList(currentStandardBM);

            //��ȡ������Ŀ�����б�
            //DataSet standardCNNameList = new DataSet();
            standardCNNameList = standardItemBF.GetStandardItemList().Tables[0];

            //��ȡ�ο����������б�
            ReturnValueSF dataItemRetVSF = new ReturnValueSF();
            dataItemRetVSF = dataItemPurviewBF.GetDataItemPointPurviewByUserId(m_AccountBM);

            //��ȡ�ο����������б�
            ReturnValueSF dataItemFactRetVSF = new ReturnValueSF();
            dataItemFactRetVSF = dataItemPurviewBF.GetDataItemPurviewByUserId(m_AccountBM);


            //��ȡ�����б�
            ReturnValueSF retVSF = new ReturnValueSF();
            retVSF = airportInfoBF.GetAirportInfors();
            DataTable dataTable = new DataTable();
            dataTable = retVSF.Dt;

            comboBoxAirPort.DataSource = dataTable;
            comboBoxAirPort.DisplayMember = "cncAirportCNAME";
            comboBoxAirPort.ValueMember = "cncAirportFourCode";
            comboBoxAirPort.Text = "--��ѡ��--";

            ReturnValueSF acRetVSF = new ReturnValueSF();
            acRetVSF = actypeBF.GetACMISCGroupBy();
            DataTable acDataTable = new DataTable();
            acDataTable = acRetVSF.Dt;
            comboBoxActype.DataSource = acDataTable;
            comboBoxActype.DisplayMember = "cncACTYPE";
            comboBoxActype.ValueMember = "cncACTYPE";
            comboBoxActype.Text = "--��ѡ��--";

            comboBoxFlightType.DataSource = standardItemBF.GetFlightTypeList();
            comboBoxFlightType.DisplayMember = "cnvcFlightType";
            comboBoxFlightType.ValueMember = "cnvcFlightType";
            comboBoxFlightType.Text = "--��ѡ��--";

            comboBoxCompany.DataSource = standardItemBF.GetOwnerList();
            comboBoxCompany.DisplayMember = "cnvcOwner";
            comboBoxCompany.ValueMember = "cnvcOwner";

            comboBoxCompany.Text = "--��ѡ��--";

            comboBoxFlightIO.Items.Clear();
            comboBoxFlightIO.Items.Add("����");
            comboBoxFlightIO.Items.Add("����");
            comboBoxFlightIO.Text = "--��ѡ��--";

            comboBoxIO.Items.Clear();
            comboBoxIO.Items.Add("����");
            comboBoxIO.Items.Add("����");
            comboBoxIO.Text = "--��ѡ��--";

            comboBoxBatchAcType.Enabled = false;
            comboBoxBatchAirPort.Enabled = false;
            rbty.Checked = false;

            //loadComBoxItem(comboBoxItemName,dataItemRetVSF.Dt);
            comboBoxItemName.SelectedIndexChanged -= new System.EventHandler(this.comboBoxItemName_SelectedIndexChanged);
            comboBoxItemName.DataSource = standardCNNameList;
            comboBoxItemName.DisplayMember = "cnvcSmallStandardCNName";
            comboBoxItemName.ValueMember = "iPK";
            comboBoxItemName.Text = "--��ѡ��--";
            comboBoxItemName.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemName_SelectedIndexChanged);

            this.numericBeginUpDown.Value = 0;
            this.numericEndUpDown.Value = 0;

            loadComBoxItem(comboBoxBeginRefencePoint,dataItemRetVSF.Dt);
            loadComBoxItem(comboBoxEndRefencePoint,dataItemRetVSF.Dt);
            loadComBoxItem(comboBoxBeginFactPoint,dataItemFactRetVSF.Dt);
            loadComBoxItem(comboBoxEndFactPoint,dataItemFactRetVSF.Dt);

            loadDataGridView(standardDataItemList);
            dataGridViewStandardItem.ReadOnly = true;

            
        }

        /// <summary>
        /// ��ʼ������Combox
        /// </summary>
        /// <param name="comBoxItem"></param>
        /// <param name="dataTable"></param>
        private void loadComBoxItem(ComboBox comBoxItem, DataTable dataTable)
        {
            comBoxItem.DataSource = dataTable.Copy();
            comBoxItem.DisplayMember = "cnvcDataItemName";
            comBoxItem.ValueMember = "cnvcDataItemID";
            comBoxItem.Text = "--��ѡ��--";
        }

        /// <summary>
        /// ��ʼ������DataGridView
        /// </summary>
        /// <param name="standardItemTable"></param>
        private void loadDataGridView(DataTable standardItemTable)
        {
            if (standardItemTable == null)
                return;
            //�����
            dataGridViewStandardItem.Columns.Clear();
            foreach (DataColumn dataColumnItem in standardItemTable.Columns)
            {
                dataGridViewStandardItem.Columns.Add(dataColumnItem.ColumnName, dataColumnItem.Caption);
                if (dataColumnItem.ColumnName == dataColumnItem.Caption)
                {
                    dataGridViewStandardItem.Columns[dataColumnItem.ColumnName].Visible = false;
                    //DataGridViewColumnSortMode.Programmatic
                    //dataGridViewStandardItem.SortedColumn = "";
                }
            }
            //�����
            
            dataGridViewStandardItem.Rows.Clear();
            foreach (DataRow dataRowItem in standardItemTable.Rows)
            {
                dataGridViewStandardItem.Rows.Add(dataRowItem.ItemArray);
            }
        }

        /// <summary>
        /// ��Ӻ�վ���ϱ�׼���̲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            StandardBM standardItem = new StandardBM();
            standardItem = setToolBoxFun();
            try
            {
                if (standardItemBF.AddStandardItem(standardItem) == 1)
                {
                    MessageBox.Show("��ӳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    currentStandardBM.Clear();
                    standardDataItemList = standardItemBF.GetStandardItemList(currentStandardBM);
                    loadDataGridView(standardDataItemList);
                }
                else if (standardItemBF.AddStandardItem(standardItem) == 2)
                {
                    MessageBox.Show("�������Ѵ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("���ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// ���Ҳ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeach_Click(object sender, EventArgs e)
        {
            StandardBM standardSearchBM = new StandardBM();
            standardSearchBM.ItemName = comboBoxItemName.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxItemName.Text.ToString();
            standardSearchBM.FlightIOType = comboBoxFlightIO.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxFlightIO.Text.ToString();
            standardSearchBM.FlightType = comboBoxFlightType.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxFlightType.Text.ToString();
            //standardSearchBM.IView = rbty.Checked ? 1 : 0;
            standardSearchBM.Actype = comboBoxActype.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxActype.Text.ToString();
            standardSearchBM.Company = comboBoxCompany.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxCompany.Text.ToString();
            standardSearchBM.CNBeginFactPoint = comboBoxBeginFactPoint.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxBeginFactPoint.Text.ToString();
            standardSearchBM.CNBeginRefencePoint = comboBoxBeginRefencePoint.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxBeginRefencePoint.Text.ToString();
            //standardSearchBM.BeginTimeSpace = numericBeginUpDown.Value.ToString();
            standardSearchBM.CNEndFactPoint = comboBoxEndFactPoint.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxEndFactPoint.Text.ToString();
            standardSearchBM.CNEndRefencePoint = comboBoxEndRefencePoint.Text.ToString().Equals("--��ѡ��--") ? null : comboBoxEndRefencePoint.Text.ToString();
            //standardSearchBM.EndTimeSpace = numericEndUpDown.Value.ToString();
            standardSearchBM.CNCityName = this.comboBoxAirPort.Text.ToString().Equals("--��ѡ��--") ? null : this.comboBoxAirPort.Text.ToString();
            standardSearchBM.Action = "Search";
            loadDataGridView(standardItemBF.GetStandardItemList(standardSearchBM));

        }

        /// <summary>
        /// ɾ��ĳ���̲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentStandardBM.StandardNo.Equals(null))
            {
                MessageBox.Show("��ѡ��Ҫɾ��������!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("ȷ���Ƿ�ɾ��������?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                standardItemBF.DelStandardItem(currentStandardBM.StandardNo);
                MessageBox.Show("ɾ���ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentStandardBM.Clear();
                fmStandardItemAdd_Load(sender, e);
            }
            else{
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ���浱ǰѡ����ʵ��
        /// </summary>
        /// <param name="strStandardNo"></param>
        /// <returns></returns>
        private StandardBM GetCurrentStandardBMSelected(string strStandardNo)
        {
            DataRow[] arrayDR = standardDataItemList.Select("cniStandardNo = " + strStandardNo);
            foreach (DataRow dr in arrayDR)
            {
                currentStandardBM.StandardNo = int.Parse(dr["cniStandardNo"].ToString());
                currentStandardBM.ItemName = dr["cnvcItemName"].ToString();
                currentStandardBM.Actype = dr["cnvcActype"].ToString();
                currentStandardBM.BeginFactPoint = dr["cnvcBeginFactPoint"].ToString();
                currentStandardBM.BeginRefencePoint = dr["cnvcBeginRefencePoint"].ToString();
                currentStandardBM.BeginTimeSpace = dr["cnvcBeginTimeSpace"].ToString();
                currentStandardBM.CNBeginFactPoint = dr["cnvcCNBeginFactPoint"].ToString();
                currentStandardBM.CNBeginRefencePoint = dr["cnvcCNBeginRefencePoint"].ToString();
                currentStandardBM.CNCityName = dr["cnvcCNCityName"].ToString();
                currentStandardBM.CNEndFactPoint = dr["cnvcCNEndFactPoint"].ToString();
                currentStandardBM.CNEndRefencePoint = dr["cnvcCNEndRefencePoint"].ToString();
                currentStandardBM.Company = dr["cnvcCompany"].ToString();
                currentStandardBM.EndFactPoint = dr["cnvcEndFactPoint"].ToString();
                currentStandardBM.EndRefencePoint = dr["cnvcEndRefencePoint"].ToString();
                currentStandardBM.EndTimeSpace = dr["cnvcEndTimeSpace"].ToString();
                currentStandardBM.FlightIOType = dr["cnvcFlightIOType"].ToString();
                currentStandardBM.FlightType = dr["cnvcFlightType"].ToString();
                currentStandardBM.FourCode = dr["cnvcFourCode"].ToString();
                currentStandardBM.ItemNo = int.Parse(dr["cniItemNo"].ToString());
                currentStandardBM.IView = int.Parse(dr["cniView"].ToString());
                currentStandardBM.ThreeCode = dr["cnvcThreeCode"].ToString();
                currentStandardBM.IOFlight = dr["cncIOFlight"].ToString()[0];
                currentStandardBM.ParentId = dr["cnvcParentId"].ToString();
                currentStandardBM.IsLeaf = dr["cncIsLeaf"].ToString()[0];
            }
            return currentStandardBM;
        }

        /// <summary>
        /// �������̲���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StandardBM standardItem = new StandardBM();
            standardItem = setToolBoxFun();
            try
            {
                if (standardItem.FourCode == currentStandardBM.FourCode && standardItem.ItemName == currentStandardBM.ItemName
                    && standardItem.FlightType == currentStandardBM.FlightType && standardItem.FlightIOType == currentStandardBM.FlightIOType
                    && standardItem.Actype == currentStandardBM.Actype && standardItem.Company == currentStandardBM.Company)
                {
                    standardItem.StandardNo = currentStandardBM.StandardNo;
                    standardItem.FourCode = currentStandardBM.FourCode;
                    standardItem.CNCityName = currentStandardBM.CNCityName;
                    standardItem.ItemName = currentStandardBM.ItemName;
                    standardItem.FlightType = currentStandardBM.FlightType;
                    standardItem.FlightIOType = currentStandardBM.FlightIOType;
                    standardItem.Actype = currentStandardBM.Actype;
                    standardItem.Company = currentStandardBM.Company;
                    if (standardItemBF.UpdateStandardItem(standardItem) == 1)
                    {
                        MessageBox.Show("�޸ĳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        currentStandardBM = new StandardBM();
                        fmStandardItemAdd_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("�޸�ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("�����޸Ļ��ء����͡��������͡��������ơ���˾������/�⺽����Ϣ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// set/get����
        /// </summary>
        /// <returns></returns>
        private StandardBM setToolBoxFun()
        {
            StandardBM standardItem = new StandardBM();

            standardItem.FourCode = comboBoxAirPort.SelectedValue.ToString();
            standardItem.CNCityName = comboBoxAirPort.Text.ToString();

            standardItem.Actype = comboBoxActype.SelectedValue.ToString();

            standardItem.Company = comboBoxCompany.Text.ToString();
            standardItem.FlightType = comboBoxFlightType.Text.ToString();
            standardItem.FlightIOType = comboBoxFlightIO.Text.ToString();

            standardItem.BeginRefencePoint =  comboBoxBeginRefencePoint.SelectedValue.ToString() ;

            standardItem.CNBeginRefencePoint = comboBoxBeginRefencePoint.Text.ToString();

            standardItem.EndRefencePoint =  comboBoxEndRefencePoint.SelectedValue.ToString() ;
            standardItem.CNEndRefencePoint = comboBoxEndRefencePoint.Text.ToString();

            standardItem.BeginTimeSpace = numericBeginUpDown.Value.ToString();
            standardItem.EndTimeSpace = numericEndUpDown.Value.ToString();

            standardItem.BeginFactPoint =  comboBoxBeginFactPoint.SelectedValue.ToString() ;
            standardItem.CNBeginFactPoint = comboBoxBeginFactPoint.Text.ToString();
            standardItem.EndFactPoint =  comboBoxEndFactPoint.SelectedValue.ToString() ;
            standardItem.CNEndFactPoint = comboBoxEndFactPoint.Text.ToString();

            standardItem.ItemName = comboBoxItemName.Text.ToString();

            int iView = 0;
            if (rbty.Checked)
                iView = 1;
            standardItem.IView = iView;
            standardItem.ThreeCode = string.Empty;
            standardItem.ItemNo = 999;

            char cIOFlight = '1';
            if (comboBoxItemName.Text.ToString().IndexOf("����") == 0)//���ۺ���
            {
                cIOFlight = '0';
            }
            else//���ۺ���
            {
                cIOFlight = '1';
            }

            standardItem.IOFlight = cIOFlight;

            for (int i = 0; i < standardCNNameList.Rows.Count; i++)
            {
                if (standardCNNameList.Rows[i]["iPK"].ToString() == comboBoxItemName.SelectedValue.ToString())
                {
                    standardItem.ParentId = standardCNNameList.Rows[i]["cniBelongTo"].ToString();
                }
            }

            //standardItem.ParentId = comboBoxItemName.SelectedValue.ToString(); 
            standardItem.IsLeaf = '1';

            return standardItem;
        }

        private void dataGridViewStandardItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView selectedDataGridView = (sender as DataGridView);
            if (selectedDataGridView.SelectedRows.Count != 0 && e.RowIndex != -1)
            {
                string strStandardNo = selectedDataGridView.Rows[e.RowIndex].Cells["cniStandardNo"].Value.ToString();
                GetCurrentStandardBMSelected(strStandardNo);

                comboBoxAirPort.Text = currentStandardBM.CNCityName.Replace(" ", "");
                comboBoxAirPort.SelectedValue = currentStandardBM.FourCode;

                comboBoxActype.Text = currentStandardBM.Actype.Replace(" ", "");
                comboBoxActype.SelectedValue = currentStandardBM.Actype;

                comboBoxCompany.Text = currentStandardBM.Company.Replace(" ", "");
                comboBoxCompany.SelectedValue = currentStandardBM.Company;

                comboBoxFlightType.Text = currentStandardBM.FlightType.Replace(" ", "");
                comboBoxFlightType.SelectedValue = currentStandardBM.FlightType;

                comboBoxFlightIO.Text = currentStandardBM.FlightIOType.Replace(" ", "");
                comboBoxFlightIO.SelectedValue = currentStandardBM.FlightIOType;

                comboBoxBeginRefencePoint.Text = currentStandardBM.CNBeginRefencePoint.Replace(" ", "");
                comboBoxBeginRefencePoint.SelectedValue = currentStandardBM.BeginRefencePoint;

                comboBoxEndRefencePoint.Text = currentStandardBM.CNEndRefencePoint.Replace(" ", "");
                comboBoxEndRefencePoint.SelectedValue = currentStandardBM.EndRefencePoint;

                numericBeginUpDown.Value = decimal.Parse(currentStandardBM.BeginTimeSpace.Replace(" ", ""));
                numericEndUpDown.Value = decimal.Parse(currentStandardBM.EndTimeSpace.Replace(" ", ""));

                comboBoxBeginFactPoint.Text = currentStandardBM.CNBeginFactPoint.Replace(" ", "");
                comboBoxBeginFactPoint.SelectedValue = currentStandardBM.BeginFactPoint;

                comboBoxEndFactPoint.Text = currentStandardBM.CNEndFactPoint.Replace(" ", "");
                comboBoxEndFactPoint.SelectedValue = currentStandardBM.EndFactPoint;
                
                comboBoxItemName.SelectedIndexChanged -= new System.EventHandler(this.comboBoxItemName_SelectedIndexChanged);
                comboBoxIO.SelectedIndexChanged -= new System.EventHandler(this.comboBoxIO_SelectedIndexChanged);
                comboBoxItemName.Text = currentStandardBM.ItemName.Replace(" ", "");
                if (comboBoxItemName.Text.Contains("����"))
                {
                    this.comboBoxIO.Text = "����";
                }
                else
                {
                    this.comboBoxIO.Text = "����";
                }

                comboBoxIO.SelectedIndexChanged += new System.EventHandler(this.comboBoxIO_SelectedIndexChanged);
                comboBoxItemName.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemName_SelectedIndexChanged);
                comboBoxBatchAcType.Enabled = false;
                comboBoxBatchAirPort.Enabled = false;

                if (currentStandardBM.IView == 1)
                    rbty.Checked = true;
            }
            else
            {
                MessageBox.Show("��ѡ��ĳһ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBatchAdd_Click(object sender, EventArgs e)
        {
            if (btnBatchAdd.Text.Equals("�������"))
            {
                btnBatchAdd.Text = "��������";
                comboBoxBatchAcType.Enabled = true;
                comboBoxBatchAirPort.Enabled = true;
                comboBoxCompany.Enabled = false;
                comboBoxBeginFactPoint.Enabled = false;
                comboBoxBeginRefencePoint.Enabled = false;
                comboBoxEndFactPoint.Enabled = false;
                comboBoxEndRefencePoint.Enabled = false;
                comboBoxFlightIO.Enabled = false;
                comboBoxFlightType.Enabled = false;
                comboBoxItemName.Enabled = false;
                numericBeginUpDown.Enabled = false;
                numericEndUpDown.Enabled = false;

                //��ȡ�����б�
                ReturnValueSF retVSF = new ReturnValueSF();
                retVSF = airportInfoBF.GetAirportInfors();
                DataTable dataTable = new DataTable();
                dataTable = retVSF.Dt;

                comboBoxBatchAirPort.DataSource = dataTable;
                comboBoxBatchAirPort.DisplayMember = "cncAirportCNAME";
                comboBoxBatchAirPort.ValueMember = "cncAirportFourCode";
                comboBoxBatchAirPort.Text = "--��ѡ��--";

                ReturnValueSF acRetVSF = new ReturnValueSF();
                acRetVSF = actypeBF.GetACMISCGroupBy();
                DataTable acDataTable = new DataTable();
                acDataTable = acRetVSF.Dt;

                comboBoxBatchAcType.DataSource = acDataTable;
                comboBoxBatchAcType.DisplayMember = "cncACTYPE";
                comboBoxBatchAcType.ValueMember = "cncACTYPE";
                comboBoxBatchAcType.Text = "--��ѡ��--";
            }
            else
            {
                string strAirPort = comboBoxAirPort.Text.ToString().Equals("--��ѡ��--") ? "" : comboBoxAirPort.SelectedValue.ToString();
                string strAcType = comboBoxActype.Text.ToString().Equals("--��ѡ��--") ? "" : comboBoxActype.Text.ToString();
                string strBatchAirPort = comboBoxBatchAirPort.Text.ToString().Equals("--��ѡ��--") ? "" : comboBoxBatchAirPort.SelectedValue.ToString();
                string strBatchAcType = comboBoxBatchAcType.Text.ToString().Equals("--��ѡ��--") ? "" : comboBoxBatchAcType.Text.ToString();
                string nCityName = "";
                int iResult = 0;
                if (strBatchAirPort != "")
                {
                    nCityName = comboBoxBatchAirPort.Text.ToString();
                }

                bool sourceAirPortSign = strAirPort.Equals("");
                bool sourceBatchAirPort = strBatchAirPort.Equals("");

                bool destAcTypeSign = strAcType.Equals("");
                bool destBatchAcType = strBatchAcType.Equals("");

                if (((sourceAirPortSign == sourceBatchAirPort) || (destAcTypeSign == destBatchAcType)) 
                    && (sourceAirPortSign == destBatchAcType != true))
                {
                    string stroInformation = "";
                    string strnInformation = "";

                    if (!strAirPort.Equals(""))
                    {
                        stroInformation += comboBoxAirPort.Text.ToString().Replace(" ", "") + "����";
                    }
                    if (!strAcType.Equals(""))
                    {
                        stroInformation += strAcType + "����";
                    }
                    stroInformation = "��" + stroInformation + "��";

                    if (!strBatchAirPort.Equals(""))
                    {
                        strnInformation += nCityName + "����";
                    }
                    if (!strBatchAcType.Equals(""))
                    {
                        strnInformation += strBatchAcType + "����";
                    }
                    strnInformation = "��" + strnInformation + "��";

                    DialogResult mResult = MessageBox.Show("��ȷ�Ͻ�" + stroInformation + "�����̱�׼���Ǹ��µ�" + strnInformation + "��!", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (mResult == DialogResult.Yes)
                    {
                        //to do batch
                        iResult = standardItemBF.AddBatchStandardItems(strAirPort, strAcType, strBatchAirPort, strBatchAcType, nCityName);
                        if (iResult != 0)
                        {
                            MessageBox.Show("������ӳɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        btnBatchAdd.Text = "�������";
                        comboBoxCompany.Enabled = true;
                        comboBoxBeginFactPoint.Enabled = true;
                        comboBoxBeginRefencePoint.Enabled = true;
                        comboBoxEndFactPoint.Enabled = true;
                        comboBoxEndRefencePoint.Enabled = true;
                        comboBoxFlightIO.Enabled = true;
                        comboBoxFlightType.Enabled = true;
                        comboBoxItemName.Enabled = true;
                        numericBeginUpDown.Enabled = true;
                        numericEndUpDown.Enabled = true;
                        fmStandardItemAdd_Load(sender, e);
                        return;
                    }
                    else if (mResult == DialogResult.No)
                    {
                    }
                    else
                    {
                        btnBatchAdd.Text = "�������";
                        comboBoxCompany.Enabled = true;
                        comboBoxBeginFactPoint.Enabled = true;
                        comboBoxBeginRefencePoint.Enabled = true;
                        comboBoxEndFactPoint.Enabled = true;
                        comboBoxEndRefencePoint.Enabled = true;
                        comboBoxFlightIO.Enabled = true;
                        comboBoxFlightType.Enabled = true;
                        comboBoxItemName.Enabled = true;
                        numericBeginUpDown.Enabled = true;
                        numericEndUpDown.Enabled = true;
                        fmStandardItemAdd_Load(sender, e);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("��ѡ�������������������Ļ���/����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBatchAdd.Text = "�������";
                    comboBoxCompany.Enabled = true;
                    comboBoxBeginFactPoint.Enabled = true;
                    comboBoxBeginRefencePoint.Enabled = true;
                    comboBoxEndFactPoint.Enabled = true;
                    comboBoxEndRefencePoint.Enabled = true;
                    comboBoxFlightIO.Enabled = true;
                    comboBoxFlightType.Enabled = true;
                    comboBoxItemName.Enabled = true;
                    numericBeginUpDown.Enabled = true;
                    numericEndUpDown.Enabled = true;
                    fmStandardItemAdd_Load(sender, e);
                    return;
                }
            }
        }


        //���ڽض��ֶ�����
        private string GetColumnName(string sourceColumnName)
        {
            if (sourceColumnName.IndexOf("Out") == 0)
            {
                sourceColumnName = sourceColumnName.Substring(3, sourceColumnName.Length - 3);
            }
            else if (sourceColumnName.IndexOf("In") == 0)
            {
                sourceColumnName = sourceColumnName.Substring(2, sourceColumnName.Length - 2);
            }
            return sourceColumnName;
        }

        private void comboBoxIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxIO.Text.Equals("����"))
            {
                comboBoxItemName.DataSource = standardItemBF.GetStandardItemList("����");
                comboBoxItemName.DisplayMember = "cnvcSmallStandardCNName";
                comboBoxItemName.ValueMember = "iPK";
                comboBoxItemName.Text = "--��ѡ��--";
            }
            else if (this.comboBoxIO.Text.Equals("����"))
            {
                comboBoxItemName.DataSource = standardItemBF.GetStandardItemList("����");
                comboBoxItemName.DisplayMember = "cnvcSmallStandardCNName";
                comboBoxItemName.ValueMember = "iPK";
                comboBoxItemName.Text = "--��ѡ��--";
            }
            else
            {
                comboBoxItemName.DataSource = standardCNNameList;
                comboBoxItemName.DisplayMember = "cnvcSmallStandardCNName";
                comboBoxItemName.ValueMember = "iPK";
                comboBoxItemName.Text = "--��ѡ��--";
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.currentStandardBM = null;
            fmStandardItemAdd_Load( sender,  e);
        }

        private void comboBoxItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemName.Text.ToString().Equals("") 
                || comboBoxItemName.Text.ToString().Equals("--��ѡ��--"))
            {

            }
            else
            {
                string strBeginFactPoint = "";
                string strEndFactPoint = "";
                for (int i = 0; i < standardCNNameList.Rows.Count; i++)
                {
                    if (standardCNNameList.Rows[i]["iPK"].ToString() == comboBoxItemName.SelectedValue.ToString())
                    {
                        strBeginFactPoint = standardCNNameList.Rows[i]["cnvcArg0"].ToString();
                        strEndFactPoint = standardCNNameList.Rows[i]["cnvcArg1"].ToString();
                    }
                }
                this.comboBoxBeginFactPoint.Text = strBeginFactPoint;
                this.comboBoxEndFactPoint.Text = strEndFactPoint;
           }
        }
    }
}