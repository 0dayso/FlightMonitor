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
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmDataItemPurview_Load(object sender, EventArgs e)
        {
            //��������
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
            //��ʾ�Ѿ����õ�Ȩ��
            ReturnValueSF rvDataItemsPurview = dataItemPurviewBF.GetDataItemPurviewByUserId(m_accountBM);
            if (rvDataItems.Result > 0 && rvDataItemsPurview.Result > 0)
            {
                for (int iLoop = 0; iLoop < rvDataItems.Dt.Rows.Count; iLoop++)
                {
                    DataRow[] drPurview = rvDataItemsPurview.Dt.Select("cniDataItemNo = " + rvDataItems.Dt.Rows[iLoop]["cniDataItemNo"].ToString());
                    //���Ȩ�ޱ���û����Ӧ��¼
                    if (drPurview.Length <= 0)
                    {
                        fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "��";
                    }
                    else
                    {
                        if (Convert.ToInt32(drPurview[0]["cniDataItemPurview"].ToString()) == 0)
                        {
                            fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "��";
                        }
                        else if (Convert.ToInt32(drPurview[0]["cniDataItemPurview"].ToString()) == 1)
                        {
                            fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "���";
                        }
                        else
                        {
                            fpDataItem.ActiveSheet.Cells[iLoop, 1].Text = "ά��Ȩ��";
                        }
                    }
                }

            }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            IList ilDataItemPurviewBM = new ArrayList();

            ReturnValueSF rvSF = new ReturnValueSF();
            //����ҵ����۲㷽��
            FlightMonitorBF.DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            int iRowIndex = 0;
           
            foreach (DataRow rowItem in dtDataItems.Rows)
            {
                FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem,DataItemPurviewBM.DataType.ITEM);
                dataItemPurviewBM.UserID = m_accountBM.UserId;
                if (fpDataItem.ActiveSheet.Cells[iRowIndex, 1].Text == "��")
                {
                    dataItemPurviewBM.DataItemPurview = 0;
                    dataItemPurviewBM.DataItemVisible = 0;
                    rvSF = dataItemPurviewBF.UpdatePurview(dataItemPurviewBM);                    
                    ilDataItemPurviewBM.Add(dataItemPurviewBM);
                }
                else if (fpDataItem.ActiveSheet.Cells[iRowIndex, 1].Text == "���")
                {
                    dataItemPurviewBM.DataItemPurview = 1;
                    rvSF = dataItemPurviewBF.UpdatePurview(dataItemPurviewBM);                   
                }
                else if (fpDataItem.ActiveSheet.Cells[iRowIndex, 1].Text == "ά��Ȩ��")
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }            
        }
    }
}