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
            //������ʾֵ
            fpACMISC.Sheets[0].Columns[0].DataField = "cnvcSHORT_REG";
            fpACMISC.Sheets[0].Columns[1].DataField = "cnvcLONG_REG";
            fpACMISC.Sheets[0].Columns[2].DataField = "cncACTYPE";
            fpACMISC.Sheets[0].Columns[3].DataField = "cnvcOWNER";


            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            //����ҵ����۲㷽��
            FlightMonitorBF.AC_MISCBF ac_MISCBF = new AC_MISCBF();
            rvSF = ac_MISCBF.GetACMISC();

            if (rvSF.Result > 0)
            {
                fpACMISC.Sheets[0].DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show("��ע��Ų���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AC_MISCBM ac_MISCBM = new AC_MISCBM();
            ac_MISCBM.AC = txtSHORT_REG.Text.Trim();
            ac_MISCBM.SHORT_REG = txtSHORT_REG.Text.Trim();
            ac_MISCBM.LONG_REG = txtLONG_REG.Text.Trim();
            ac_MISCBM.ACTYPE = txtACTYPE.Text.Trim();
            ac_MISCBM.OWNER = txtOWNER.Text.Trim();

            //����ҵ����۲㷽��
            FlightMonitorBF.AC_MISCBF ac_MISCBF = new AC_MISCBF();

            ReturnValueSF rvSF = ac_MISCBF.InsertACMISC(ac_MISCBM);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (rvSF.Result == 2)
            {
                MessageBox.Show("�÷ɻ����Ѿ����ڣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


             rvSF = ac_MISCBF.GetACMISC();

             if (rvSF.Result > 0)
             {
                 fpACMISC.Sheets[0].DataSource = rvSF.Dt;
             }
             else
             {
                 MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //�ж��Ƿ�ѡ����һ����¼
            if (fpACMISC.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("��ѡ��һ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("�Ƿ�ɾ����ѡ�ķɻ��ţ�", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //��ȡѡ����û�
                FlightMonitorBM.AC_MISCBM ac_MISCBM = GetSelectedACMISC();

                //����ҵ����۲㷽��
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
                        MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                
            }
        }

        private AC_MISCBM GetSelectedACMISC()
        {

            //��ȡ����Դ
            DataTable dtACMISC = (DataTable)fpACMISC.Sheets[0].DataSource;

            int iSelectedRowIndex = fpACMISC.Sheets[0].Models.Selection.AnchorRow;

            //����ʵ�����
            return new AC_MISCBM(dtACMISC.Rows[iSelectedRowIndex]);
        }
    }
}