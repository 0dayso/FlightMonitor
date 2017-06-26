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
    public partial class fmAccount : Form
    {
        private AccountBM m_accountBM;

        public fmAccount(AccountBM accountBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
        }

        private void fmAccount_Load(object sender, EventArgs e)
        {
            //绑定航站信息
            Station_Bind();
            cmbStation.SelectedValue = m_accountBM.StationThreeCode;
            fpAccount.Focus();
        }

        /// <summary>
        /// 绑定航站信息
        /// </summary>
        private void Station_Bind()
        {
            //设置显示属性
            cmbStation.DisplayMember = "cnvcAirportName";
            cmbStation.ValueMember = "cncThreeCode";
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            //调用业务外观层方法
            FlightMonitorBF.StationBF stationBF = new StationBF();
            rvSF = stationBF.GetAllStation();

            if (rvSF.Result > 0)
            {
                //添加一个空行
                DataRow rowNull = rvSF.Dt.NewRow();
                rvSF.Dt.Rows.InsertAt(rowNull, 0);
                cmbStation.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 更改航站事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "":cmbUserType.SelectedIndex.ToString());
            fpAccount.Focus();
        }

        /// <summary>
        /// 更改用户类型事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
        }

        /// <summary>
        /// 根据航站或用户类型查询用户
        /// </summary>
        /// <param name="strStationThreeCode">航站三字码</param>
        /// <param name="strUserTypeId">用户类型</param>
        private void GetAccountByStation(string strStationThreeCode, string strUserTypeId)
        {
            //设置显示值
            fpAccount.Sheets[0].Columns[0].DataField = "cnvcUserId";
            fpAccount.Sheets[0].Columns[1].DataField = "cnvcUserName";
            fpAccount.Sheets[0].Columns[2].DataField = "cnvcUserDepartment";            
            fpAccount.Sheets[0].Columns[3].DataField = "cniRefreshInterval";
            fpAccount.Sheets[0].Columns[4].DataField = "cnvcUserTypeName";
            fpAccount.Sheets[0].Columns[5].DataField = "cniMaxUser";
            fpAccount.Sheets[0].Columns[6].DataField = "cniLogUser";

            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            //调用业务外观层方法
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            rvSF = accountBF.GetAccountByStation(strStationThreeCode, strUserTypeId);

            if (rvSF.Result > 0)
            {
                fpAccount.Sheets[0].DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 添加用户Button单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            fmAccountInfo objFmAccountInfo = new fmAccountInfo();
            objFmAccountInfo.ShowDialog();

            //重新绑定
            if (objFmAccountInfo.RvSF.Result > 0)
            {
                GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
            }
        }

        /// <summary>
        /// 修改用户Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditUser();
        }

        /// <summary>
        /// 双击表格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpAccount_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                return;
            }
            EditUser();
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        private void EditUser()
        {
            //判断是否选择了一条记录
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }

            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();
            
            //显示编辑窗体
            fmAccountInfo objFmAccountInfo = new fmAccountInfo(accountBM);
            objFmAccountInfo.ShowDialog();

            //重新绑定
            if (objFmAccountInfo.RvSF.Result > 0)
            {
                GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
            }
        }

        private FlightMonitorBM.AccountBM GetSelectedAccount()
        {           

            //获取数据源
            DataTable dtUser = (DataTable)fpAccount.Sheets[0].DataSource;

            int iSelectedRowIndex = fpAccount.Sheets[0].Models.Selection.AnchorRow;

            //生成实体对象
            return new FlightMonitorBM.AccountBM(dtUser.Rows[iSelectedRowIndex]);
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //判断是否选择了一条记录
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("是否删除所选的用户？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //获取选择的用户
                FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

                //调用业务外观层方法
                FlightMonitorBF.AccountBF accountBF = new AccountBF();
                ReturnValueSF rvSF = accountBF.Delete(accountBM);

                if (rvSF.Result > 0)
                {
                    GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
                }

                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 初始化密码按钮click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitialize_Click(object sender, EventArgs e)
        {
            //判断是否选择了一条记录
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //获取选择的用户
            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

            //调用业务外观层方法
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            accountBM.UserPassword = "111111";
            ReturnValueSF rvSF = accountBF.UpdatePassword(accountBM);

            MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 导出用户列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel文件(*.xls)|*.xls";
            //覆盖文件时询问
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel文件另存为";
            saveExcel.FileName = "帐户";
            //设置默认路径
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                //导出前，将列标签背景色改成Silver，以便导出的Excel文件表头颜色与内容区分开来
                fpAccount.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");

                string fileName = saveExcel.FileName;
                bool save = fpAccount.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                if (save)
                {
                    //恢复列标签背景色
                    fpAccount.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                }
                else
                {
                    //恢复列标签背景色
                    fpAccount.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                    MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /// <summary>
        /// 控制权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataItemPurview_Click(object sender, EventArgs e)
        {
            //判断是否选择了一条记录
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //获取选择的用户
            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

            fmDataItemPurview objFmDataItemPurview = new fmDataItemPurview(accountBM);
            objFmDataItemPurview.ShowDialog();
        }

        private void btnMenuPurview_Click(object sender, EventArgs e)
        {
            //判断是否选择了一条记录
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //获取选择的用户
            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

            fmMenuPurview objfmMenuPurview = new fmMenuPurview(accountBM);
            objfmMenuPurview.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //根据帐号或姓名定位位置
            string strSearch = textBox1.Text.Trim();
            if (strSearch == "")
            {
                MessageBox.Show("定位的内容不能为空！");
                return;
            }

            DataTable dataTablefpAccount = (fpAccount.Sheets[0].DataSource as DataTable);
            for(int indexdataTablefpAccount = 0;indexdataTablefpAccount < dataTablefpAccount.Rows.Count;indexdataTablefpAccount++)
            {
                if ((dataTablefpAccount.Rows[indexdataTablefpAccount]["cnvcUserId"].ToString() == strSearch) ||
                    (dataTablefpAccount.Rows[indexdataTablefpAccount]["cnvcUserName"].ToString() == strSearch))
                {
                    fpAccount.Sheets[0].AddSelection(indexdataTablefpAccount, 0, 1, fpAccount.Sheets[0].ColumnCount);
                    fpAccount.ShowRow(0, indexdataTablefpAccount, FarPoint.Win.Spread.VerticalPosition.Center);
                }
            }
        }
    }
}