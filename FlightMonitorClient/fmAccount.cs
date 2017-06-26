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
            //�󶨺�վ��Ϣ
            Station_Bind();
            cmbStation.SelectedValue = m_accountBM.StationThreeCode;
            fpAccount.Focus();
        }

        /// <summary>
        /// �󶨺�վ��Ϣ
        /// </summary>
        private void Station_Bind()
        {
            //������ʾ����
            cmbStation.DisplayMember = "cnvcAirportName";
            cmbStation.ValueMember = "cncThreeCode";
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            //����ҵ����۲㷽��
            FlightMonitorBF.StationBF stationBF = new StationBF();
            rvSF = stationBF.GetAllStation();

            if (rvSF.Result > 0)
            {
                //���һ������
                DataRow rowNull = rvSF.Dt.NewRow();
                rvSF.Dt.Rows.InsertAt(rowNull, 0);
                cmbStation.DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// ���ĺ�վ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "":cmbUserType.SelectedIndex.ToString());
            fpAccount.Focus();
        }

        /// <summary>
        /// �����û������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
        }

        /// <summary>
        /// ���ݺ�վ���û����Ͳ�ѯ�û�
        /// </summary>
        /// <param name="strStationThreeCode">��վ������</param>
        /// <param name="strUserTypeId">�û�����</param>
        private void GetAccountByStation(string strStationThreeCode, string strUserTypeId)
        {
            //������ʾֵ
            fpAccount.Sheets[0].Columns[0].DataField = "cnvcUserId";
            fpAccount.Sheets[0].Columns[1].DataField = "cnvcUserName";
            fpAccount.Sheets[0].Columns[2].DataField = "cnvcUserDepartment";            
            fpAccount.Sheets[0].Columns[3].DataField = "cniRefreshInterval";
            fpAccount.Sheets[0].Columns[4].DataField = "cnvcUserTypeName";
            fpAccount.Sheets[0].Columns[5].DataField = "cniMaxUser";
            fpAccount.Sheets[0].Columns[6].DataField = "cniLogUser";

            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            //����ҵ����۲㷽��
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            rvSF = accountBF.GetAccountByStation(strStationThreeCode, strUserTypeId);

            if (rvSF.Result > 0)
            {
                fpAccount.Sheets[0].DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// ����û�Button�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            fmAccountInfo objFmAccountInfo = new fmAccountInfo();
            objFmAccountInfo.ShowDialog();

            //���°�
            if (objFmAccountInfo.RvSF.Result > 0)
            {
                GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
            }
        }

        /// <summary>
        /// �޸��û�Click�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditUser();
        }

        /// <summary>
        /// ˫������¼�
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
        /// �༭�û�
        /// </summary>
        private void EditUser()
        {
            //�ж��Ƿ�ѡ����һ����¼
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("��ѡ��һ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }

            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();
            
            //��ʾ�༭����
            fmAccountInfo objFmAccountInfo = new fmAccountInfo(accountBM);
            objFmAccountInfo.ShowDialog();

            //���°�
            if (objFmAccountInfo.RvSF.Result > 0)
            {
                GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
            }
        }

        private FlightMonitorBM.AccountBM GetSelectedAccount()
        {           

            //��ȡ����Դ
            DataTable dtUser = (DataTable)fpAccount.Sheets[0].DataSource;

            int iSelectedRowIndex = fpAccount.Sheets[0].Models.Selection.AnchorRow;

            //����ʵ�����
            return new FlightMonitorBM.AccountBM(dtUser.Rows[iSelectedRowIndex]);
        }

        /// <summary>
        /// ɾ����ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //�ж��Ƿ�ѡ����һ����¼
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("��ѡ��һ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("�Ƿ�ɾ����ѡ���û���", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //��ȡѡ����û�
                FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

                //����ҵ����۲㷽��
                FlightMonitorBF.AccountBF accountBF = new AccountBF();
                ReturnValueSF rvSF = accountBF.Delete(accountBM);

                if (rvSF.Result > 0)
                {
                    GetAccountByStation(cmbStation.SelectedValue.ToString().Trim(), cmbUserType.SelectedIndex <= 0 ? "" : cmbUserType.SelectedIndex.ToString());
                }

                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// ��ʼ�����밴ťclick�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitialize_Click(object sender, EventArgs e)
        {
            //�ж��Ƿ�ѡ����һ����¼
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("��ѡ��һ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //��ȡѡ����û�
            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

            //����ҵ����۲㷽��
            FlightMonitorBF.AccountBF accountBF = new AccountBF();
            accountBM.UserPassword = "111111";
            ReturnValueSF rvSF = accountBF.UpdatePassword(accountBM);

            MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// �����û��б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel�ļ�(*.xls)|*.xls";
            //�����ļ�ʱѯ��
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel�ļ����Ϊ";
            saveExcel.FileName = "�ʻ�";
            //����Ĭ��·��
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                //����ǰ�����б�ǩ����ɫ�ĳ�Silver���Ա㵼����Excel�ļ���ͷ��ɫ���������ֿ���
                fpAccount.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");

                string fileName = saveExcel.FileName;
                bool save = fpAccount.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                if (save)
                {
                    //�ָ��б�ǩ����ɫ
                    fpAccount.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                }
                else
                {
                    //�ָ��б�ǩ����ɫ
                    fpAccount.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                    MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataItemPurview_Click(object sender, EventArgs e)
        {
            //�ж��Ƿ�ѡ����һ����¼
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("��ѡ��һ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //��ȡѡ����û�
            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

            fmDataItemPurview objFmDataItemPurview = new fmDataItemPurview(accountBM);
            objFmDataItemPurview.ShowDialog();
        }

        private void btnMenuPurview_Click(object sender, EventArgs e)
        {
            //�ж��Ƿ�ѡ����һ����¼
            if (fpAccount.Sheets[0].SelectionCount <= 0)
            {
                MessageBox.Show("��ѡ��һ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //��ȡѡ����û�
            FlightMonitorBM.AccountBM accountBM = GetSelectedAccount();

            fmMenuPurview objfmMenuPurview = new fmMenuPurview(accountBM);
            objfmMenuPurview.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //�����ʺŻ�������λλ��
            string strSearch = textBox1.Text.Trim();
            if (strSearch == "")
            {
                MessageBox.Show("��λ�����ݲ���Ϊ�գ�");
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