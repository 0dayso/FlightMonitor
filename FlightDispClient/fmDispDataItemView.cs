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

        #region 窗体载入
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmDispDataItemView_Load(object sender, EventArgs e)
        {
            cbxViewTypes.SelectedIndex = 0;

            BindDataItems("Dsp");
        }
        #endregion

        #region 绑定视图
        /// <summary>
        /// 绑定视图
        /// </summary>
        /// <param name="strViewType">视图类型</param>
        private void BindDataItems(string strViewType)
        {
            //绑定可选项
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvPurviewDataItem = dataItemPurviewBF.GetPurviewDataItem(_accountBM);

            //根据视图类型设置
            DataTable dtDataItems = rvPurviewDataItem.Dt;
            DataRow[] drDataItems = dtDataItems.Select("cnvcDataItemID LIKE '" + strViewType + "%'");
            //导入数据源以便绑定
            DataTable dtBindSource = dtDataItems.Clone();
            foreach (DataRow dr in drDataItems)
            {
                dtBindSource.ImportRow(dr);
            }
            dtBindSource.DefaultView.Sort = "cniViewIndex";
            //绑定
            lstDataItem.DataSource = dtBindSource.DefaultView.ToTable();
            lstDataItem.DisplayMember = "cnvcDataItemName";
            lstDataItem.ValueMember = "cniDataItemNo";

            //绑定已经选择的项
            ReturnValueSF rvVisibleDataItem = dataItemPurviewBF.GetVisibleDataItem(_accountBM);
            //根据视图类型设置
            DataTable dtDisplayDataItems = rvVisibleDataItem.Dt;
            DataRow[] drDisplayDataItems = dtDisplayDataItems.Select("cnvcDataItemID LIKE '" + strViewType + "%'");
            //导入数据源以便绑定
            DataTable dtDisplayBindSource = dtDisplayDataItems.Clone();
            foreach (DataRow dr in drDisplayDataItems)
            {
                dtDisplayBindSource.ImportRow(dr);
            }
            dtDisplayBindSource.DefaultView.Sort = "cniViewIndex";
            //绑定
            lstDataView.DataSource = dtDisplayBindSource.DefaultView.ToTable();
            lstDataView.DisplayMember = "cnvcDataItemName";
            lstDataView.ValueMember = "cniDataItemNo";
        }
        #endregion

        #region 选择视图
        /// <summary>
        /// 选择视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxViewTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxViewTypes.SelectedItem.ToString() == "放行视图")
            {
                BindDataItems("Dsp");
            }
            if (cbxViewTypes.SelectedItem.ToString() == "监控视图")
            {
                BindDataItems("Mon");
            }
        }
        #endregion

        #region 保存设置
        private void btnOK_Click(object sender, EventArgs e)
        {
            ReturnValueSF rvSF = new ReturnValueSF();

            DataTable dtPurviewDataItem = (DataTable)lstDataItem.DataSource;
            DataTable dtVisibleDataItem = (DataTable)lstDataView.DataSource;

            //将可选项列表的数据项的VISIBLE设置为不可见
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

            //设置已选项列表中的数据项的可见性和显示顺序
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
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            this.Close();
        }
        #endregion

        #region 添加数据项
        //添加全部
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            while (lstDataItem.Items.Count > 0)
            {
                lstDataItem.SetSelected(0, true);
                AddDataItem();
            }
        }
        //添加一项
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDataItem();
        }
        //通过双击添加
        private void lstDataItem_DoubleClick(object sender, EventArgs e)
        {
            AddDataItem();
        }

        #region 将可选项中的某一项添加到已选项中
        /// <summary>
        /// 将可选项中的某一项添加到已选项中
        /// </summary>
        private void AddDataItem()
        {
            //未选项和已选项数据表
            DataTable dtPurviewDataItem = (DataTable)lstDataItem.DataSource;
            DataTable dtVisibleDataItem = (DataTable)lstDataView.DataSource;

            if (lstDataItem.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一条记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //如果已选项列表为空
            if (dtVisibleDataItem.Rows.Count == 0)
            {
                dtVisibleDataItem.Rows.Add(dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray);
                dtPurviewDataItem.Rows.Remove(dtPurviewDataItem.Rows[lstDataItem.SelectedIndex]);
            }
            else
            {
                //可选项中选中的对象实体
                DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(dtPurviewDataItem.Rows[lstDataItem.SelectedIndex], DataItemPurviewBM.DataType.PURVIEW);
                //已选项第一项对象实体
                DataItemPurviewBM dataItemFirstVisibleBM = new DataItemPurviewBM(dtVisibleDataItem.Rows[0], DataItemPurviewBM.DataType.PURVIEW);
                DataItemPurviewBM dataItemLastVisibleBM = new DataItemPurviewBM(dtVisibleDataItem.Rows[lstDataView.Items.Count - 1], DataItemPurviewBM.DataType.PURVIEW);

                //如果序号小于第一项的序号
                if (dataItemPurviewBM.DataItemNO < dataItemFirstVisibleBM.DataItemNO)
                {
                    DataRow dataRow = dtVisibleDataItem.NewRow();
                    dataRow.ItemArray = dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray;
                    dtVisibleDataItem.Rows.InsertAt(dataRow, 0);
                    dtPurviewDataItem.Rows.RemoveAt(lstDataItem.SelectedIndex);
                }
                else if (dataItemFirstVisibleBM.DataItemNO > dataItemLastVisibleBM.DataItemNO) //如果选项阿项的需要大于最后一项的序号
                {
                    DataRow dataRow = dtVisibleDataItem.NewRow();
                    dataRow.ItemArray = dtPurviewDataItem.Rows[lstDataItem.SelectedIndex].ItemArray;
                    dtVisibleDataItem.Rows.InsertAt(dataRow, lstDataView.Items.Count);
                }
                else //查找一个合适的位置
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

        #region 删除数据项
        //删除全部
        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            while (lstDataView.Items.Count > 0)
            {
                lstDataView.SetSelected(0, true);
                RemoveDataItem();
            }
        }
        //删除一项
        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveDataItem();
        }
        //通过双击删除
        private void lstDataView_DoubleClick(object sender, EventArgs e)
        {
            RemoveDataItem();
        }

        #region 从已选项列表中删除选择的项
        /// <summary>
        /// 从已选项列表中删除选择的项
        /// </summary>
        private void RemoveDataItem()
        {
            if (lstDataView.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //未选项和已选项数据表
            DataTable dtPurviewDataItem = (DataTable)lstDataItem.DataSource;
            DataTable dtVisibleDataItem = (DataTable)lstDataView.DataSource;

            //已选项选项中选中的对象实体
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

        #region 数据项上移
        private void btnUp_Click(object sender, EventArgs e)
        {
            IList ilSelectedItems = new ArrayList();
            IList ilPreviousItems = new ArrayList();

            //已选项数据表
            DataTable dtDataView = (DataTable)lstDataView.DataSource;

            ////预设显示顺序
            //int iRowIndex = 0;
            //foreach (DataRow rowItem in dtDataView.Rows)
            //{
            //    rowItem["cniDataItemPurview"] = iRowIndex;
            //    iRowIndex += 1;
            //}

            //选中项信息
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
                //查找和选中项属于同一组的项
                for (int iLoop = 0; iLoop < lstDataView.Items.Count; iLoop++)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strSelectedGroupName + "|") >= 0)
                    {
                        ilSelectedItems.Add(dataItem);
                    }
                }
            }

            //上一项信息
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

            //修改上一项的显示顺序
            IEnumerator iePreviousItems = ilPreviousItems.GetEnumerator();
            while (iePreviousItems.MoveNext())
            {
                DataItemPurviewBM dataItem = (DataItemPurviewBM)iePreviousItems.Current;
                DataRow[] dataRows = dtDataView.Select("cniDataItemNo=" + dataItem.DataItemNO);
                dataRows[0]["cniViewIndex"] = int.Parse(dataRows[0]["cniViewIndex"].ToString()) + ilSelectedItems.Count;
            }

            //修改选择项的显示顺序
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

        #region 数据项下移
        private void btnDown_Click(object sender, EventArgs e)
        {
            IList ilSelectedItems = new ArrayList();
            IList ilNextItems = new ArrayList();

            //已选项数据表
            DataTable dtDataView = (DataTable)lstDataView.DataSource;

            //预设显示顺序
            //int iRowIndex = 0;
            //foreach (DataRow rowItem in dtDataView.Rows)
            //{
            //    rowItem["cniDataItemPurview"] = iRowIndex;
            //    iRowIndex += 1;
            //}

            //选中项信息
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
                //查找和选中项属于同一组的项
                for (int iLoop = 0; iLoop < lstDataView.Items.Count; iLoop++)
                {
                    DataItemPurviewBM dataItem = new DataItemPurviewBM(dtDataView.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                    if (dataItem.DataItemName.IndexOf(strSelectedGroupName + "|") >= 0)
                    {
                        ilSelectedItems.Add(dataItem);
                    }
                }
            }


            //下一项信息
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

            //修改选择项的显示顺序
            IEnumerator ieSelectedItems = ilSelectedItems.GetEnumerator();

            while (ieSelectedItems.MoveNext())
            {
                DataItemPurviewBM dataItem = (DataItemPurviewBM)ieSelectedItems.Current;
                DataRow[] dataRows = dtDataView.Select("cniDataItemNo=" + dataItem.DataItemNO);
                dataRows[0]["cniViewIndex"] = int.Parse(dataRows[0]["cniViewIndex"].ToString()) + ilNextItems.Count;
            }

            //修改下一项的显示顺序
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