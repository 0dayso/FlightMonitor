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

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmDataItemView : Form
    {
        private FlightMonitorBM.AccountBM m_accountBM;
        private bool isLeftMouseButtonDown = false;
        private int iIndexOfDest_DragDrop = -1;
        private int iIndexOfSrc_DragDrop = -1; 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountBM">用户实体对象</param>
        public fmDataItemView(FlightMonitorBM.AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }

        private void fmDataItemView_Load(object sender, EventArgs e)
        {
            //绑定可选项
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvPurviewDataItem = dataItemPurviewBF.GetPurviewDataItem(m_accountBM);
            lstDataItem.DataSource = rvPurviewDataItem.Dt;
            //lstDataItem.DisplayMember = "cnvcDataItemName";
            lstDataItem.DisplayMember = "cnvcDataItemName_Combine";
            lstDataItem.ValueMember = "cniDataItemNo";

            //绑定已经选择的项
            ReturnValueSF rvVisibleDataItem = dataItemPurviewBF.GetVisibleDataItem(m_accountBM);
            lstDataView.DataSource = rvVisibleDataItem.Dt;
            //lstDataView.DisplayMember = "cnvcDataItemName";
            lstDataView.DisplayMember = "cnvcDataItemName_Combine";
            lstDataView.ValueMember = "cniDataItemNo";

        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置用户的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 添加按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDataItem();
        }

        /// <summary>
        /// 移除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveDataItem();
        }

        /// <summary>
        /// 上移按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            IList ilSelectedItems = new ArrayList();
            IList ilPreviousItems = new ArrayList();

            //已选项数据表
            DataTable dtDataView = (DataTable) lstDataView.DataSource;

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
            else if(iSelectedIndex > 0)
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

        /// <summary>
        /// 下移按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            else if(iSelectedIndex < lstDataView.Items.Count - 1)
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

        private void lstDataView_DoubleClick(object sender, EventArgs e)
        {
            RemoveDataItem();
        }

        private void lstDataItem_DoubleClick(object sender, EventArgs e)
        {
            AddDataItem();
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            while (lstDataItem.Items.Count > 0)
            {
                lstDataItem.SetSelected(0, true);
                AddDataItem();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            while (lstDataView.Items.Count > 0)
            {
                lstDataView.SetSelected(0, true);
                RemoveDataItem();
            }
        }

        private void btnSearchDataView_Click(object sender, EventArgs e)
        {
            DataTable dtVisibleDataItem = (DataTable)lstDataView.DataSource;
 
            int iCurrentPosition_dtVisibleDataItem = -1;
            string strSearch = "";

            strSearch = cbDataView.Text.ToString().Trim(); //需要搜索的信息
            iCurrentPosition_dtVisibleDataItem = lstDataView.SelectedIndex; //当前位置
            if (rbUp_DataView.Checked) //向上搜索
            {
                for (int index_dtVisibleDataItem = (iCurrentPosition_dtVisibleDataItem - 1); index_dtVisibleDataItem >= 0; index_dtVisibleDataItem--)
                {
                    if (dtVisibleDataItem.Rows[index_dtVisibleDataItem]["cnvcDataItemName"].ToString().IndexOf(strSearch) >= 0)
                    {
                        lstDataView.SelectedIndex = index_dtVisibleDataItem;
                        return;
                    }
                }
            }
            else if (rbDown_DataView.Checked) //向下搜索
            {
                for (int index_dtVisibleDataItem = (iCurrentPosition_dtVisibleDataItem + 1); index_dtVisibleDataItem < dtVisibleDataItem.Rows.Count; index_dtVisibleDataItem++)
                {
                    if (dtVisibleDataItem.Rows[index_dtVisibleDataItem]["cnvcDataItemName"].ToString().IndexOf(strSearch) >= 0)
                    {
                        lstDataView.SelectedIndex = index_dtVisibleDataItem;
                        return;
                    }
                }
            }

        }

        private void btnSearchDataItem_Click(object sender, EventArgs e)
        {
            DataTable dtPurviewDataItem = (DataTable)lstDataItem.DataSource;
            int iCurrentPosition_dtPurviewDataItem = -1;
            string strSearch = "";

            strSearch = cbDataItem.Text.ToString().Trim(); //需要搜索的信息
            iCurrentPosition_dtPurviewDataItem = lstDataItem.SelectedIndex; //当前位置
            if (rbUp_DataItem.Checked) //向上搜索
                for (int index_dtPurviewDataItem = (iCurrentPosition_dtPurviewDataItem - 1); index_dtPurviewDataItem >= 0; index_dtPurviewDataItem--)
                {
                    if (dtPurviewDataItem.Rows[index_dtPurviewDataItem]["cnvcDataItemName"].ToString().IndexOf(strSearch) >= 0)
                    {
                        lstDataItem.SelectedIndex = index_dtPurviewDataItem;
                        return;
                    }
                }
            else if (rbDown_DataItem.Checked) //向下搜索
                for (int index_dtPurviewDataItem = (iCurrentPosition_dtPurviewDataItem + 1); index_dtPurviewDataItem < dtPurviewDataItem.Rows.Count; index_dtPurviewDataItem++)
                {
                    if (dtPurviewDataItem.Rows[index_dtPurviewDataItem]["cnvcDataItemName"].ToString().IndexOf(strSearch) >= 0)
                    {
                        lstDataItem.SelectedIndex = index_dtPurviewDataItem;
                        return;
                    }
                }

        }

        private void lstDataView_MouseDown(object sender, MouseEventArgs e)
        {
            //取的LISTBOX中选中的项的索引
            iIndexOfSrc_DragDrop = ((ListBox)sender).IndexFromPoint(e.X, e.Y);

            if (iIndexOfSrc_DragDrop != ListBox.NoMatches)
            {

                //确定拖动的数据
                //((ListBox)sender).DoDragDrop(((ListBox)sender).Items[iIndexOfSrc_DragDrop].ToString(), DragDropEffects.All);
                ((ListBox)sender).DoDragDrop(((ListBox)sender).Items[iIndexOfSrc_DragDrop].ToString(), DragDropEffects.All);


            }


        }

        private void lstDataView_DragDrop(object sender, DragEventArgs e)
        {
            ListBox destListBox = sender as ListBox;


            //取的LISTBOX中放置项的索引
            iIndexOfDest_DragDrop = ((ListBox)sender).IndexFromPoint(((ListBox)sender).PointToClient(new Point(e.X, e.Y)));

            if (iIndexOfDest_DragDrop != ListBox.NoMatches)
            {
                ListBox srcListBox = sender as ListBox; 


                DataTable dtsrcListBox = (DataTable)srcListBox.DataSource;
                DataTable dtdestListBox = (DataTable)destListBox.DataSource;

                DataRow dataRow = dtdestListBox.NewRow();
                dataRow.ItemArray = dtsrcListBox.Rows[iIndexOfSrc_DragDrop].ItemArray;
                dtsrcListBox.Rows.RemoveAt(iIndexOfSrc_DragDrop);
                dtdestListBox.Rows.InsertAt(dataRow, iIndexOfDest_DragDrop);

                //重新设定序号
                int iRowIndex = 0;
                foreach (DataRow dataRow_1 in dtdestListBox.Rows)
                {
                    dtdestListBox.Rows[iRowIndex]["cniViewIndex"] = iRowIndex;
                    iRowIndex += 1;
                }

                //放置位置作为当前选择项
                destListBox.SelectedIndex = iIndexOfDest_DragDrop;

            }

        }

        private void lstDataView_DragOver(object sender, DragEventArgs e)
        {    
            if (e.Data.GetDataPresent(typeof(string)) && ((ListBox)sender).Equals(lstDataView))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;



        }

    }
}