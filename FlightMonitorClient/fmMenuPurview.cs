using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmMenuPurview : Form
    {
        private AccountBM m_accountBM;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountBM"></param>
        public fmMenuPurview(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMenuPurview_Load(object sender, EventArgs e)
        {
            MenuPurviewBF menuPurviewBF = new MenuPurviewBF();
            ReturnValueSF rvSF = menuPurviewBF.GetMenuItems();
            if (rvSF.Result > 0)
            {
                chklsMenuItem.DataSource = rvSF.Dt;
                chklsMenuItem.ValueMember = "cniMenuID";
                chklsMenuItem.DisplayMember = "cnvcMenuItemName";
            }

            DataTable dtMenuItem = (DataTable)chklsMenuItem.DataSource;

            rvSF = menuPurviewBF.GetMenuItemPurview(m_accountBM);
            if (rvSF.Result > 0)
            {
                //int iRowIndex = 0;
                //foreach (DataRow dataRow in rvSF.Dt.Rows)
                //{
                //    if (dataRow["cniMenuPurview"].ToString() == "1")
                //    {
                //        chklsMenuItem.SetItemChecked(iRowIndex, true);
                //    }
                //    else
                //    {
                //        chklsMenuItem.SetItemChecked(iRowIndex, false);
                //    }
                //    iRowIndex += 1;
                //}

                int iRowIndex = 0;
                foreach (DataRow dataRow in dtMenuItem.Rows)
                {
                    DataRow[] drPurview = rvSF.Dt.Select("cniMenuID = " + dataRow["cniMenuID"].ToString() + " AND cniMenuPurview = 1");
                    if (drPurview.Length > 0)
                    {
                        chklsMenuItem.SetItemChecked(iRowIndex, true);
                    }
                    else
                    {
                        chklsMenuItem.SetItemChecked(iRowIndex, false);
                    }
                    iRowIndex += 1;
                }


            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtMenuItems = (DataTable)chklsMenuItem.DataSource;

            IList ilMenuItemPurviewBM = new ArrayList();

            ReturnValueSF rvSF = new ReturnValueSF();
            //调用业务外观层方法
            FlightMonitorBF.MenuPurviewBF menuPurviewBF = new MenuPurviewBF();
            int iRowIndex = 0;
            foreach (DataRow rowItem in dtMenuItems.Rows)
            {
                FlightMonitorBM.MenuPurviewBM menuPurviewBM = new MenuPurviewBM(rowItem, MenuPurviewBM.DataType.ITEM);
                menuPurviewBM.UserID = m_accountBM.UserId;

                if (chklsMenuItem.GetItemChecked(iRowIndex) == true)
                {
                    menuPurviewBM.MenuPurview = 1;
                }
                else
                {
                    menuPurviewBM.MenuPurview = 0;
                }
                rvSF = menuPurviewBF.UpdatePurview(menuPurviewBM);
               
                iRowIndex += 1;
                if (rvSF.Result < 0)
                {
                    break;
                }
            }

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }            
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}