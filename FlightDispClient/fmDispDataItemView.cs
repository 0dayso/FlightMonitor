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
using System.Collections;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmDispDataItemView : Form
    {
        private FlightMonitorBM.AccountBM _accountBM;
        public fmDispDataItemView(AccountBM accountBM)
        {
            InitializeComponent();
            _accountBM = accountBM;
        }

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmDispDataItemView_Load(object sender, EventArgs e)
        {
            cbxViewTypes.SelectedIndex = 0;

            BindDataItems("Dsp");
        }
        #endregion

        #region ����ͼ
        /// <summary>
        /// ����ͼ
        /// </summary>
        /// <param name="strViewType">��ͼ����</param>
        private void BindDataItems(string strViewType)
        {
            //�󶨿�ѡ��
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvPurviewDataItem = dataItemPurviewBF.GetPurviewDataItem(_accountBM);

            //������ͼ��������
            DataTable dtDataItems = rvPurviewDataItem.Dt;
            DataRow[] drDataItems = dtDataItems.Select("cnvcDataItemID LIKE '" + strViewType + "%'");
            //��������Դ�Ա��
            DataTable dtBindSource = dtDataItems.Clone();
            foreach (DataRow dr in drDataItems)
            {
                dtBindSource.ImportRow(dr);
            }
            dtBindSource.DefaultView.Sort = "cniViewIndex";
            //��
            lstDataItem.DataSource = dtBindSource.DefaultView.ToTable();
            lstDataItem.DisplayMember = "cnvcDataItemName";
            lstDataItem.ValueMember = "cniDataItemNo";

            //���Ѿ�ѡ�����
            ReturnValueSF rvVisibleDataItem = dataItemPurviewBF.GetVisibleDataItem(_accountBM);
            //������ͼ��������
            DataTable dtDisplayDataItems = rvVisibleDataItem.Dt;
            DataRow[] drDisplayDataItems = dtDisplayDataItems.Select("cnvcDataItemID LIKE '" + strViewType + "%'");
            //��������Դ�Ա��
            DataTable dtDisplayBindSource = dtDisplayDataItems.Clone();
            foreach (DataRow dr in drDisplayDataItems)
            {
                dtDisplayBindSource.ImportRow(dr);
            }
            dtDisplayBindSource.DefaultView.Sort = "cniViewIndex";
            //��
            lstDataView.DataSource = dtDisplayBindSource.DefaultView.ToTable();
            lstDataView.DisplayMember = "cnvcDataItemName";
            lstDataView.ValueMember = "cniDataItemNo";
        }
        #endregion

        #region ѡ����ͼ
        /// <summary>
        /// ѡ����ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxViewTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxViewTypes.SelectedItem.ToString() == "������ͼ")
            {
                BindDataItems("Dsp");
            }
            if (cbxViewTypes.SelectedItem.ToString() == "�����ͼ")
            {
                BindDataItems("Mon");
            }
        }
        #endregion

        #region ��������
        private void btnOK_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF = new ReturnValueSF();

            DataTable dtPurviewDataItem = (DataTable)lstDataItem.DataSource;
            DataTable dtVisibleDataItem = (DataTable)lstDataView.DataSource;

            //����ѡ���б���������VISIBLE����Ϊ���ɼ�
            IList ilPurviewDataItem = new ArrayList();

            foreach (DataRow rowItem in dtPurviewDataItem.Rows)
            {
                FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem, DataItemPurviewBM.DataType.PURVIEW);
                dataItemPurviewBM.DataItemVisible = 0;
                dataItemPurviewBM.SplashPromptItem = 0;
                ilPurviewDataItem.Add(dataItemPurviewBM);
            }
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            rvSF = dataItemPurviewBF.UpdateVisible(ilPurviewDataItem);

            //������ѡ���б��е�������Ŀɼ��Ժ���ʾ˳��
            IList ilVisibleDataItem = new ArrayList();
            int iRowIndex = 0;
            foreach (DataRow rowItem in dtVisibleDataItem.Rows)
            {
                FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem, DataItemPurviewBM.DataType.PURVIEW);
                dataItemPurviewBM.DataItemVisible = 1;
                dataItemPurviewBM.SplashPromptItem = 1;
                dataItemPurviewBM.ViewIndex = iRowIndex;
                ilVisibleDataItem.Add(dataItemPurviewBM);
                iRowIndex += 1;
            }

            rvSF = dataItemPurviewBF.UpdateVisible(ilVisibleDataItem);
            rvSF = dataItemPurviewBF.UpdateIndex(ilVisibleDataItem);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.Close();
        }
        #endregion

        #region ���������
        //���ȫ��
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            while (lstDataItem.Items.Count > 0)
            {
                lstDataItem.SetSelected(0, true);
                AddDataItem();
            }
        }
        //���һ��
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDataItem();
        }
        //ͨ��˫�����
        private void lstDataItem_DoubleClick(object sender, EventArgs e)
        {
            AddDataItem();
        }

        #region ����ѡ���е�ĳһ����ӵ���ѡ����
        /// <summary>
        /// ����ѡ���е�ĳһ����ӵ���ѡ����
        /// </summary>
        private void AddDataItem()
        {
            //δѡ�����ѡ�����ݱ�
            DataTable dtPurviewDataItem = (DataTable)lstDataItem.DataSource;
            DataTable dtVisibleDataItem = (DataTable)lstDataView.DataSource;

            if (lstDataItem.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��һ����¼!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //�����ѡ���б�Ϊ��
            if (dtVisibleDataItem.Rows.Count == 0)
            {
                dtVisibleDataItem.Rows.Add(dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray);
                dtPurviewDataItem.Rows.Remove(dtPurviewDataItem.Rows[lstDataItem.SelectedIndex]);
            }
            else
            {
                //��ѡ����ѡ�еĶ���ʵ��
                DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(dtPurviewDataItem.Rows[lstDataItem.SelectedIndex], DataItemPurviewBM.DataType.PURVIEW);
                //��ѡ���һ�����ʵ��
                DataItemPurviewBM dataItemFirstVisibleBM = new DataItemPurviewBM(dtVisibleDataItem.Rows[0], DataItemPurviewBM.DataType.PURVIEW);
                DataItemPurviewBM dataItemLastVisibleBM = new DataItemPurviewBM(dtVisibleDataItem.Rows[lstDataView.Items.Count - 1], DataItemPurviewBM.DataType.PURVIEW);

                //������С�ڵ�һ������
                if (dataItemPurviewBM.DataItemNO < dataItemFirstVisibleBM.DataItemNO)
                {
                    DataRow dataRow = dtVisibleDataItem.NewRow();
                    dataRow.ItemArray = dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray;
                    dtVisibleDataItem.Rows.InsertAt(dataRow, 0);
                    dtPurviewDataItem.Rows.RemoveAt(lstDataItem.SelectedIndex);
                }
                else if (dataItemFirstVisibleBM.DataItemNO > dataItemLastVisibleBM.DataItemNO) //���ѡ������Ҫ�������һ������
                {
                    DataRow dataRow = dtVisibleDataItem.NewRow();
                    dataRow.ItemArray = dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray;
                    dtVisibleDataItem.Rows.InsertAt(dataRow, lstDataView.Items.Count);
                }
                else //����һ�����ʵ�λ��
                {
                    bool blnFindPosition = false;
                    for (int iLoop = 0; iLoop < lstDataView.Items.Count - 1; iLoop++)
                    {
                        dataItemFirstVisibleBM = new DataItemPurviewBM(dtVisibleDataItem.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                        dataItemLastVisibleBM = new DataItemPurviewBM(dtVisibleDataItem.Rows[iLoop + 1], DataItemPurviewBM.DataType.PURVIEW);
                        // && 
                        if (dataItemPurviewBM.DataItemNO > dataItemFirstVisibleBM.DataItemNO && dataItemPurviewBM.DataItemNO < dataItemLastVisibleBM.DataItemNO)
                        {
                            DataRow dataRow = dtVisibleDataItem.NewRow();
                            dataRow.ItemArray = dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray;
                            dtVisibleDataItem.Rows.InsertAt(dataRow, iLoop + 1);
                            dtPurviewDataItem.Rows.RemoveAt(lstDataItem.SelectedIndex);
                            blnFindPosition = true;
                            break;
                        }
                    }
                    if (blnFindPosition == false)
                    {
                        DataRow dataRow = dtVisibleDataItem.NewRow();
                        dataRow.ItemArray = dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray;
                        dtVisibleDataItem.Rows.InsertAt(dataRow, lstDataView.Items.Count);
                        dtPurviewDataItem.Rows.RemoveAt(lstDataItem.SelectedIndex);
                    }
                }
            }

            int iRowIndex = 0;
            foreach (DataRow dataRow in dtVisibleDataItem.Rows)
            {
                dtVisibleDataItem.Rows[iRowIndex]["cniViewIndex"] = iRowIndex;
                iRowIndex += 1;
            }
        }
        #endregion

        #endregion

        #region ɾ��������
        //ɾ��ȫ��
        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            while (lstDataView.Items.Count > 0)
            {
                lstDataView.SetSelected(0, true);
                RemoveDataItem();
            }
        }
        //ɾ��һ��
        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveDataItem();
        }
        //ͨ��˫��ɾ��
        private void lstDataView_DoubleClick(object sender, EventArgs e)
        {
            RemoveDataItem();
        }

        #region ����ѡ���б���ɾ��ѡ�����
        /// <summary>
        /// ����ѡ���б���ɾ��ѡ�����
        /// </summary>
        private void RemoveDataItem()
        {
            if (lstDataView.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��һ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //δѡ�����ѡ�����ݱ�
            DataTable dtPurviewDataItem = (DataTable)lstDataItem.DataSource;
            DataTable dtVisibleDataItem = (DataTable)lstDataView.DataSource;

            //��ѡ��ѡ����ѡ�еĶ���ʵ��
            DataItemPurviewBM dataItemVisibleBM = new DataItemPurviewBM(dtVisibleDataItem.Rows[lstDataView.SelectedIndex], DataItemPurviewBM.DataType.PURVIEW);

            bool blnFind = false;
            for (int iLoop = 0; iLoop < lstDataItem.Items.Count; iLoop++)
            {
                DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(dtPurviewDataItem.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                if (dataItemVisibleBM.DataItemNO < dataItemPurviewBM.DataItemNO)
                {
                    blnFind = true;
                    DataRow dataRow = dtPurviewDataItem.NewRow();
                    dataRow.ItemArray = dtVisibleDataItem.Rows[lstDataView.SelectedIndex].ItemArray;
                    dtPurviewDataItem.Rows.InsertAt(dataRow, iLoop);
                    break;
                }
            }
            if (blnFind == false)
            {
                DataRow dataRow = dtPurviewDataItem.NewRow();
                dataRow.ItemArray = dtVisibleDataItem.Rows[lstDataView.SelectedIndex].ItemArray;
                dtPurviewDataItem.Rows.InsertAt(dataRow, lstDataItem.Items.Count);
            }

            dtVisibleDataItem.Rows.RemoveAt(lstDataView.SelectedIndex);

            int iRowIndex = 0;
            foreach (DataRow dataRow in dtVisibleDataItem.Rows)
            {
                dtVisibleDataItem.Rows[iRowIndex]["cniViewIndex"] = iRowIndex;
                iRowIndex += 1;
            }
        }
        #endregion

        #endregion

        #region ����������
        private void btnUp_Click(object sender, EventArgs e)
        {
            IList ilSelectedItems = new ArrayList();
            IList ilPreviousItems = new ArrayList();

            //��ѡ�����ݱ�
            DataTable dtDataView = (DataTable)lstDataView.DataSource;

            ////Ԥ����ʾ˳��
            //int iRowIndex = 0;
            //foreach (DataRow rowItem in dtDataView.Rows)
            //{
            //    rowItem["cniDataItemPurview"] = iRowIndex;
            //    iRowIndex += 1;
            //}

            //ѡ������Ϣ
            int iSelectedIndex = lstDataView.SelectedIndex;
            DataItemPurviewBM selectedItem = new DataItemPurviewBM(dtDataView.Rows[iSelectedIndex], DataItemPurviewBM.DataType.PURVIEW);
            string strSelectedItemName = selectedItem.DataItemName;
            string strSelectedGroupName = "";
            if (strSelectedItemName.IndexOf("|") > 0)
            {
                string[] strArrName = strSelectedItemName.Split('|');
                strSelectedGroupName = strArrName[0];
            }
            else
            {
                ilSelectedItems.Add(selectedItem);
            }


            if (strSelectedGroupName != "")
            {
                //���Һ�ѡ��������ͬһ�����
                for (int iLoop = 0; iLoop < lstDataView.Items.Count; iLoop++)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strSelectedGroupName + "|") >= 0)
                    {
                        ilSelectedItems.Add(dataItem);
                    }
                }
            }

            //��һ����Ϣ
            string strPreviousGroupName = "";
            if (strSelectedGroupName != "")
            {
                for (int iLoop = iSelectedIndex - 1; iLoop >= 0; iLoop--)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strSelectedGroupName + "|") < 0)
                    {
                        if (dataItem.DataItemName.IndexOf("|") < 0)
                        {
                            ilPreviousItems.Add(dataItem);
                        }
                        else
                        {
                            string[] strArrPreviousName = dataItem.DataItemName.Split('|');
                            strPreviousGroupName = strArrPreviousName[0];
                        }
                        break;
                    }
                }
            }
            else if (iSelectedIndex > 0)
            {
                DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iSelectedIndex - 1], DataItemPurviewBM.DataType.PURVIEW);

                if (dataItem.DataItemName.IndexOf("|") < 0)
                {
                    ilPreviousItems.Add(dataItem);
                }
                else
                {
                    string[] strArrPreviousName = dataItem.DataItemName.Split('|');
                    strPreviousGroupName = strArrPreviousName[0];
                }

            }

            if (strPreviousGroupName != "")
            {
                for (int iLoop = 0; iLoop < lstDataView.Items.Count; iLoop++)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strPreviousGroupName + "|") >= 0)
                    {
                        ilPreviousItems.Add(dataItem);
                    }
                }
            }

            //�޸���һ�����ʾ˳��
            IEnumerator iePreviousItems = ilPreviousItems.GetEnumerator();
            while (iePreviousItems.MoveNext())
            {
                DataItemPurviewBM dataItem = (DataItemPurviewBM)iePreviousItems.Current;
                DataRow[] dataRows = dtDataView.Select("cniDataItemNo=" + dataItem.DataItemNO);
                dataRows[0]["cniViewIndex"] = int.Parse(dataRows[0]["cniViewIndex"].ToString()) + ilSelectedItems.Count;
            }

            //�޸�ѡ�������ʾ˳��
            IEnumerator ieSelectedItems = ilSelectedItems.GetEnumerator();

            while (ieSelectedItems.MoveNext())
            {
                DataItemPurviewBM dataItem = (DataItemPurviewBM)ieSelectedItems.Current;
                DataRow[] dataRows = dtDataView.Select("cniDataItemNo=" + dataItem.DataItemNO);
                dataRows[0]["cniViewIndex"] = int.Parse(dataRows[0]["cniViewIndex"].ToString()) - ilPreviousItems.Count;
            }

            DataRow[] allDataRows = dtDataView.Select("cniDataItemNo <> 0", "cniViewIndex");


            DataTable dtNewView = dtDataView.Clone();

            for (int iLoop = 0; iLoop < allDataRows.Length; iLoop++)
            {

                DataRow dataRow = dtNewView.NewRow();
                dataRow.ItemArray = allDataRows[iLoop].ItemArray;
                dtNewView.Rows.InsertAt(dataRow, dtNewView.Rows.Count);
            }

            lstDataView.DataSource = dtNewView;


            lstDataView.SelectedIndex = iSelectedIndex - ilPreviousItems.Count;
        }
        #endregion

        #region ����������
        private void btnDown_Click(object sender, EventArgs e)
        {
            IList ilSelectedItems = new ArrayList();
            IList ilNextItems = new ArrayList();

            //��ѡ�����ݱ�
            DataTable dtDataView = (DataTable)lstDataView.DataSource;

            //Ԥ����ʾ˳��
            //int iRowIndex = 0;
            //foreach (DataRow rowItem in dtDataView.Rows)
            //{
            //    rowItem["cniDataItemPurview"] = iRowIndex;
            //    iRowIndex += 1;
            //}

            //ѡ������Ϣ
            int iSelectedIndex = lstDataView.SelectedIndex;
            DataItemPurviewBM selectedItem = new DataItemPurviewBM(dtDataView.Rows[iSelectedIndex], DataItemPurviewBM.DataType.PURVIEW);
            string strSelectedItemName = selectedItem.DataItemName;
            string strSelectedGroupName = "";
            if (strSelectedItemName.IndexOf("|") > 0)
            {
                string[] strArrName = strSelectedItemName.Split('|');
                strSelectedGroupName = strArrName[0];
            }
            else
            {
                ilSelectedItems.Add(selectedItem);
            }


            if (strSelectedGroupName != "")
            {
                //���Һ�ѡ��������ͬһ�����
                for (int iLoop = 0; iLoop < lstDataView.Items.Count; iLoop++)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strSelectedGroupName + "|") >= 0)
                    {
                        ilSelectedItems.Add(dataItem);
                    }
                }
            }


            //��һ����Ϣ
            string strNextGroupName = "";
            if (strSelectedGroupName != "")
            {
                for (int iLoop = iSelectedIndex; iLoop < lstDataView.Items.Count; iLoop++)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strSelectedGroupName + "|") < 0)
                    {
                        if (dataItem.DataItemName.IndexOf("|") < 0)
                        {
                            ilNextItems.Add(dataItem);
                        }
                        else
                        {
                            string[] strArrNextName = dataItem.DataItemName.Split('|');
                            strNextGroupName = strArrNextName[0];
                        }
                        break;
                    }
                }
            }
            else if (iSelectedIndex < lstDataView.Items.Count - 1)
            {
                DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iSelectedIndex + 1], DataItemPurviewBM.DataType.PURVIEW);

                if (dataItem.DataItemName.IndexOf("|") < 0)
                {
                    ilNextItems.Add(dataItem);
                }
                else
                {
                    string[] strArrNextName = dataItem.DataItemName.Split('|');
                    strNextGroupName = strArrNextName[0];
                }

            }

            if (strNextGroupName != "")
            {
                for (int iLoop = iSelectedIndex + 1; iLoop < lstDataView.Items.Count; iLoop++)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strNextGroupName + "|") >= 0)
                    {
                        ilNextItems.Add(dataItem);
                    }
                }
            }

            //�޸�ѡ�������ʾ˳��
            IEnumerator ieSelectedItems = ilSelectedItems.GetEnumerator();

            while (ieSelectedItems.MoveNext())
            {
                DataItemPurviewBM dataItem = (DataItemPurviewBM)ieSelectedItems.Current;
                DataRow[] dataRows = dtDataView.Select("cniDataItemNo=" + dataItem.DataItemNO);
                dataRows[0]["cniViewIndex"] = int.Parse(dataRows[0]["cniViewIndex"].ToString()) + ilNextItems.Count;
            }

            //�޸���һ�����ʾ˳��
            IEnumerator ieNextItems = ilNextItems.GetEnumerator();
            while (ieNextItems.MoveNext())
            {
                DataItemPurviewBM dataItem = (DataItemPurviewBM)ieNextItems.Current;
                DataRow[] dataRows = dtDataView.Select("cniDataItemNo=" + dataItem.DataItemNO);
                dataRows[0]["cniViewIndex"] = int.Parse(dataRows[0]["cniViewIndex"].ToString()) - ilSelectedItems.Count;
            }

            DataRow[] allDataRows = dtDataView.Select("cniDataItemNo <> 0", "cniViewIndex");


            DataTable dtNewView = dtDataView.Clone();

            for (int iLoop = 0; iLoop < allDataRows.Length; iLoop++)
            {

                DataRow dataRow = dtNewView.NewRow();
                dataRow.ItemArray = allDataRows[iLoop].ItemArray;
                dtNewView.Rows.InsertAt(dataRow, dtNewView.Rows.Count);
            }

            lstDataView.DataSource = dtNewView;


            lstDataView.SelectedIndex = iSelectedIndex + ilNextItems.Count;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}