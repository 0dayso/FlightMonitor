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
    public partial class fmACMISC : Form
    {
        public fmACMISC()
        {
            InitializeComponent();
        }

        private void fmACMISC_Load(object sender, EventArgs e)
        {
            //设置显示值
            fpACMISC.Sheets[0].Columns[0].DataField = "cnvcSHORT_REG";
            fpACMISC.Sheets[0].Columns[1].DataField = "cnvcLONG_REG";
            fpACMISC.Sheets[0].Columns[2].DataField = "cncACTYPE";
            fpACMISC.Sheets[0].Columns[3].DataField = "cnvcOWNER";


            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            //调用业务外观层方法
            FlightMonitorBF.AC_MISCBF ac_MISCBF = new AC_MISCBF();
            rvSF = ac_MISCBF.GetACMISC();

            if (rvSF.Result > 0)
            {
                fpACMISC.Sheets[0].DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtSHORT_REG.Text.Trim() == "")
            {
                MessageBox.Show("短注册号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AC_MISCBM ac_MISCBM = new AC_MISCBM();
            ac_MISCBM.AC = txtSHORT_REG.Text.Trim();
            ac_MISCBM.SHORT_REG = txtSHORT_REG.Text.Trim();
            ac_MISCBM.LONG_REG = txtLONG_REG.Text.Trim();
            ac_MISCBM.ACTYPE = txtACTYPE.Text.Trim();
            ac_MISCBM.OWNER = txtOWNER.Text.Trim();

            //调用业务外观层方法
            FlightMonitorBF.AC_MISCBF ac_MISCBF = new AC_MISCBF();

            ReturnValueSF rvSF = ac_MISCBF.InsertACMISC(ac_MISCBM);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (rvSF.Result == 2)
            {
                MessageBox.Show("该飞机号已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


             rvSF = ac_MISCBF.GetACMISC();

             if (rvSF.Result > 0)
             {
                 fpACMISC.Sheets[0].DataSource = rvSF.Dt;
             }
             else
             {
                 MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //判断是否选择了一条记录
            if (fpACMISC.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("是否删除所选的飞机号？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //获取选择的用户
                FlightMonitorBM.AC_MISCBM ac_MISCBM = GetSelectedACMISC();

                //调用业务外观层方法
                FlightMonitorBF.AC_MISCBF ac_MISCBF = new AC_MISCBF();
                ReturnValueSF rvSF = ac_MISCBF.DeleteACMISC(ac_MISCBM);

                if (rvSF.Result > 0)
                {
                    rvSF = ac_MISCBF.GetACMISC();

                    if (rvSF.Result > 0)
                    {
                        fpACMISC.Sheets[0].DataSource = rvSF.Dt;
                    }
                    else
                    {
                        MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                
            }
        }

        private AC_MISCBM GetSelectedACMISC()
        {

            //获取数据源
            DataTable dtACMISC = (DataTable)fpACMISC.Sheets[0].DataSource;

            int iSelectedRowIndex = fpACMISC.Sheets[0].Models.Selection.AnchorRow;

            //生成实体对象
            return new AC_MISCBM(dtACMISC.Rows[iSelectedRowIndex]);
        }
    }
}