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

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmPostionManagement : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public fmPostionManagement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmPostionManagement_Load(object sender, EventArgs e)
        {
            PositionNameBF positionNameBF = new PositionNameBF();
            ReturnValueSF rvSF = positionNameBF.GetAllPositionName();

            if (rvSF.Result > 0)
            {
                fpPosition.Sheets[0].Columns[0].DataField = "cnvcPositionName";
                
                fpPosition.DataSource = rvSF.Dt;
            }
        }

        /// <summary>
        /// 删除一个席位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (fpPosition.Sheets[0].SelectionCount == 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //获取数据源
            DataTable dtPositionName = (DataTable)fpPosition.Sheets[0].DataSource;

            int iSelectedRowIndex = fpPosition.Sheets[0].Models.Selection.AnchorRow;

            //生成实体对象
            PositionNameBM positionNameBM = new PositionNameBM(dtPositionName.Rows[iSelectedRowIndex]);

            PositionNameBF positionNameBF = new PositionNameBF();

            ReturnValueSF rvSF = positionNameBF.DeletePositionName(positionNameBM);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                rvSF = positionNameBF.GetAllPositionName();

                if (rvSF.Result > 0)
                {
                    fpPosition.DataSource = rvSF.Dt;
                }
            }
        }

        /// <summary>
        /// 添加一个席位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPositionName.Text.Trim() == "")
            {
                MessageBox.Show("席位名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            PositionNameBF positionNameBF = new PositionNameBF();
            PositionNameBM positionNameBM = new PositionNameBM();
            positionNameBM.PositionName = txtPositionName.Text.Trim();

            ReturnValueSF rvSF = positionNameBF.InsertPositionName(positionNameBM);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                rvSF = positionNameBF.GetAllPositionName();

                if (rvSF.Result > 0)
                {
                    fpPosition.DataSource = rvSF.Dt;
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
    }
}