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
    public partial class fmPositionInfor : Form
    {
        public fmPositionInfor()
        {
            InitializeComponent();
        }

        private void fmPositionInfor_Load(object sender, EventArgs e)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //绑定席位名称
            PositionNameBF positionNameBF = new PositionNameBF();
            
            rvSF = positionNameBF.GetAllPositionName();

            if (rvSF.Result > 0)
            {

                DataTable dtPositionName = rvSF.Dt;

                cmbPositionName.DataSource = dtPositionName;
                cmbPositionName.DisplayMember = "cnvcPositionName";
                cmbPositionName.ValueMember = "cniPositionID";
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// 根据选择的席位显示已经分配到该席位的飞机和未分配到该席位的飞机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPositionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //选择的席位
            PositionNameBM positionNameBM = new PositionNameBM(((DataTable)cmbPositionName.DataSource).Rows[cmbPositionName.SelectedIndex]);

            //未分配到该席位的飞机号
            //业务外观类
            AC_MISCBF ac_miscBF = new AC_MISCBF();
            rvSF = ac_miscBF.GetAirCraftByPositionId(positionNameBM);
            if (rvSF.Result > 0)
            {
                lstAircraftNo.DataSource = rvSF.Dt;
                lstAircraftNo.DisplayMember = "cnvcLONG_REG";
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //已分配到该席位的飞机号
            //业务外观类
            PositionInforBF positionInforBF = new PositionInforBF();
            rvSF = positionInforBF.GetInforByPositionId(positionNameBM);
            if (rvSF.Result > 0)
            {
                lstAssignedNo.DataSource = rvSF.Dt;
                lstAssignedNo.DisplayMember = "cnvcLONG_REG";
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 分配飞机
        /// </summary>
        private void AddItem()
        {
            if (lstAircraftNo.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一条记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dtAssignedAircraftNo = (DataTable)lstAssignedNo.DataSource;
            DataTable dtAllAircraftNo = (DataTable)lstAircraftNo.DataSource;

            DataRow drSelectedAircraftNo = dtAssignedAircraftNo.NewRow();
            drSelectedAircraftNo["cniInfoid"] = 1;
            drSelectedAircraftNo["cnvcLONG_REG"] = dtAllAircraftNo.Rows[lstAircraftNo.SelectedIndex]["cnvcLONG_REG"].ToString();
            drSelectedAircraftNo["cniPositionID"] = cmbPositionName.SelectedValue.ToString();
            drSelectedAircraftNo["cnvcPositionName"] = cmbPositionName.SelectedText;

            bool blnFind = false;
            int iRowIndex = 0;
            foreach (DataRow dataRow in dtAssignedAircraftNo.Rows)
            {
                if (drSelectedAircraftNo["cnvcLONG_REG"].ToString().CompareTo(dataRow["cnvcLONG_REG"].ToString()) < 0)
                {
                    dtAssignedAircraftNo.Rows.InsertAt(drSelectedAircraftNo, iRowIndex);
                    blnFind = true;
                    break;
                }
                iRowIndex += 1;
            }

            if (blnFind == false)
            {
                dtAssignedAircraftNo.Rows.InsertAt(drSelectedAircraftNo, dtAssignedAircraftNo.Rows.Count);
            }

            dtAllAircraftNo.Rows.RemoveAt(lstAircraftNo.SelectedIndex);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        /// <summary>
        /// 移除项
        /// </summary>
        private void RemoveItem()
        {
            if (lstAssignedNo.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一条记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dtAssignedAircraftNo = (DataTable)lstAssignedNo.DataSource;
            DataTable dtAllAircraftNo = (DataTable)lstAircraftNo.DataSource;

            DataRow drSelectedAircraftNo = dtAllAircraftNo.NewRow();
            
            drSelectedAircraftNo["cnvcLONG_REG"] = dtAssignedAircraftNo.Rows[lstAssignedNo.SelectedIndex]["cnvcLONG_REG"].ToString();
            

            bool blnFind = false;
            int iRowIndex = 0;
            foreach (DataRow dataRow in dtAllAircraftNo.Rows)
            {
                if (drSelectedAircraftNo["cnvcLONG_REG"].ToString().CompareTo(dtAllAircraftNo.Rows[iRowIndex]["cnvcLONG_REG"].ToString()) < 0)
                {
                    dtAllAircraftNo.Rows.InsertAt(drSelectedAircraftNo, iRowIndex);
                    blnFind = true;
                    break;
                }
                iRowIndex += 1;
            }

            if (blnFind == false)
            {
                dtAllAircraftNo.Rows.InsertAt(drSelectedAircraftNo, dtAllAircraftNo.Rows.Count);
            }

            dtAssignedAircraftNo.Rows.RemoveAt(lstAssignedNo.SelectedIndex);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveItem();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IList ilPositionInfor = new ArrayList();

            DataTable dtPositionName = (DataTable)cmbPositionName.DataSource;
            DataTable dtAllAirCraft = (DataTable)lstAircraftNo.DataSource;
            DataTable dtAssignedAirCraft = (DataTable)lstAssignedNo.DataSource;

            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();          

            //席位信息实体对象
            PositionNameBM positionNameBM = new PositionNameBM(dtPositionName.Rows[cmbPositionName.SelectedIndex]);

            //席位信息实体对象列表
            foreach(DataRow dataRow in dtAssignedAirCraft.Rows)
            {
                PositionInforBM positionInforBM = new PositionInforBM(dataRow);
                ilPositionInfor.Add(positionInforBM);
            }

            //业务外观类
            PositionInforBF positionInforBF = new PositionInforBF();

            rvSF = positionInforBF.InsertPositionInfors(positionNameBM, ilPositionInfor);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            while (lstAircraftNo.Items.Count > 0)
            {
                lstAircraftNo.SelectedItems.Add(lstAircraftNo.Items[0]);
                AddItem();
            }
           
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            while (lstAssignedNo.Items.Count > 0)
            {
                lstAssignedNo.SelectedItems.Add(lstAssignedNo.Items[0]);
                RemoveItem();
            }
        }
    }
}