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
    public partial class fmDataItemPurview : Form
    {
        private FlightMonitorBM.AccountBM m_accountBM;
        private DataTable dtDataItems;
        public fmDataItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmDataItemPurview_Load(object sender, EventArgs e)
        {
            //绑定数据项
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();

            ReturnValueSF rvDataItems = dataItemPurviewBF.GetDataItems();
            dtDataItems = rvDataItems.Dt;

            if (rvDataItems.Result > 0)
            {
                fpDataItem.Sheets[0].Columns[0].DataField = "cnvcDataItemName";
                fpDataItem.DataSource = rvDataItems.Dt;
            }

            FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM();
            dataItemPurviewBM.UserID = m_accountBM.UserId;
            //显示已经设置的权限
            ReturnValueSF rvDataItemsPurview = dataItemPurviewBF.GetDataItemPurviewByUserId(m_accountBM);
            if (rvDataItems.Result > 0 && rvDataItemsPurview.Result > 0)
            {
                for (int iLoop = 0; iLoop < rvDataItems.Dt.Rows.Count; iLoop++)
                {
                    DataRow[] drPurview = rvDataItemsPurview.Dt.Select("cniDataItemNo = " + rvDataItems.Dt.Rows[iLoop]["cniDataItemNo"].ToString());
                    //如果权限表中没有相应记录
                    if (drPurview.Length <= 0)
                    {
                        fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "无";
                    }
                    else
                    {
                        if (Convert.ToInt32(drPurview[0]["cniDataItemPurview"].ToString()) == 0)
                        {
                            fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "无";
                        }
                        else if (Convert.ToInt32(drPurview[0]["cniDataItemPurview"].ToString()) == 1)
                        {
                            fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "浏览";
                        }
                        else
                        {
                            fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "维护权限";
                        }
                    }
                }

            }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            IList ilDataItemPurviewBM = new ArrayList();

            ReturnValueSF rvSF = new ReturnValueSF();
            //调用业务外观层方法
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            int iRowIndex = 0;
           
            foreach (DataRow rowItem in dtDataItems.Rows)
            {
                FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem,DataItemPurviewBM.DataType.ITEM);
                dataItemPurviewBM.UserID = m_accountBM.UserId;
                if (fpDataItem.ActiveSheet.Cells[iRowIndex, 1].Text == "无")
                {
                    dataItemPurviewBM.DataItemPurview = 0;
                    dataItemPurviewBM.DataItemVisible = 0;
                    rvSF = dataItemPurviewBF.UpdatePurview(dataItemPurviewBM);                    
                    ilDataItemPurviewBM.Add(dataItemPurviewBM);
                }
                else if (fpDataItem.ActiveSheet.Cells[iRowIndex, 1].Text == "浏览")
                {
                    dataItemPurviewBM.DataItemPurview = 1;
                    rvSF = dataItemPurviewBF.UpdatePurview(dataItemPurviewBM);                   
                }
                else if (fpDataItem.ActiveSheet.Cells[iRowIndex, 1].Text == "维护权限")
                {
                    dataItemPurviewBM.DataItemPurview = 2;
                    rvSF = dataItemPurviewBF.UpdatePurview(dataItemPurviewBM);                    
                }
                
                iRowIndex += 1;
                if (rvSF.Result < 0)
                {
                    break;
                }
            }

            rvSF = dataItemPurviewBF.UpdateVisible(ilDataItemPurviewBM);

            rvSF = dataItemPurviewBF.GetVisibleDataItem(m_accountBM);
            ilDataItemPurviewBM.Clear();
            for (int iLoop = 0; iLoop < rvSF.Dt.Rows.Count; iLoop++)
            {
                DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rvSF.Dt.Rows[iLoop], DataItemPurviewBM.DataType.PURVIEW);
                dataItemPurviewBM.ViewIndex = iLoop;
                ilDataItemPurviewBM.Add(dataItemPurviewBM);
            }
            rvSF = dataItemPurviewBF.UpdateIndex(ilDataItemPurviewBM);


            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }            
        }
    }
}