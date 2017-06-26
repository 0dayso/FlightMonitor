using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmSelectUsers : Form
    {
        #region ����
        private StationBM _StationBM = new StationBM();
        private CommanderTypeBM _CommanderTypeBM = new CommanderTypeBM();

        private MaintenGuaranteeInforBM _maintenGuaranteeInforBM;
        private ChangeRecordBM _changeRecordBM;

        private string _CommanderList_Old;
        private string _CommanderList_New;

        private string _memo = "";
        private bool _success = false;
        #endregion ����

        #region ����

        /// ��Ա�б��ɣ�
        /// </summary>
        public string CommanderList_Old
        {
            set { _CommanderList_Old = value; }
            get { return _CommanderList_Old; }
        }


        /// ��Ա�б��£�
        /// </summary>
        public string CommanderList_New
        {
            set { _CommanderList_New = value; }
            get { return _CommanderList_New; }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

        /// <summary>
        /// ��ֵ�ɹ���־
        /// </summary>
        public bool Success
        {
            set { _success = value; }
            get { return _success; }
        }
        #endregion ����

        #region ���캯��

        public fmSelectUsers()
        {
            InitializeComponent();
        }

        public fmSelectUsers(StationBM stationBM, CommanderTypeBM commanderTypeBM, string commanderList_Old)
        {
            InitializeComponent();

            _StationBM = stationBM;
            _CommanderTypeBM = commanderTypeBM;
            _CommanderList_Old = commanderList_Old;
        }

        public fmSelectUsers(StationBM stationBM, 
            CommanderTypeBM commanderTypeBM, 
            string commanderList_Old,
            MaintenGuaranteeInforBM maintenGuaranteeInforBM,
            ChangeRecordBM changeRecordBM)
        {
            InitializeComponent();

            _StationBM = stationBM;
            _CommanderTypeBM = commanderTypeBM;
            _CommanderList_Old = commanderList_Old;
            _maintenGuaranteeInforBM = maintenGuaranteeInforBM;
            _changeRecordBM = changeRecordBM;

        }

        #endregion ���캯��

        private void button1_Click(object sender, EventArgs e)
        {
            string strCommanderList_Select = ""; //����ѡ����Ա�б���ϳɵ��ַ������硰��Ա1(renyuan1)����Ա2(renyuan2)����Ա3(renyuan3)��

            for (int i = 0; i < checkedListBox2.Items.Count; i++) //����ַ���
            {
                strCommanderList_Select = strCommanderList_Select + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "��";
            }
            if ((strCommanderList_Select != "") &&
                (strCommanderList_Select[strCommanderList_Select.Length - 1] == '��')) //ȥ��ĩβ���ַ�������
                strCommanderList_Select = strCommanderList_Select.Substring(0,strCommanderList_Select.Length - 1);

            if (_CommanderList_Old.Trim() == strCommanderList_Select.Trim())
            {
                _success = false;
                _memo = "��ֵ��ԭֵһ��������ִ�б��������";
                this.DialogResult = DialogResult.Cancel;

                MessageBox.Show("��ֵ��ԭֵһ��������ִ�б��������");

                return;
            }

            if (System.Text.Encoding.Default.GetBytes(strCommanderList_Select).Length <= 500) //�ַ����ܳ��Ȳ��ܴ���500�ֽ�
            {
                _CommanderList_New = strCommanderList_Select;
                _success = true;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                _success = false;
                _memo = "ѡ�����Ա�ַ��ܳ��ȴ���500�������ֶγ������ơ�";
                this.DialogResult = DialogResult.Cancel;

                MessageBox.Show("ѡ�����Ա�ַ��ܳ��ȴ���500��������ѡ��лл��");

                return;
            }

            #region ���±��ϱ���ӱ����¼�������¼��
            //��ȡ������ʱ��
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            _maintenGuaranteeInforBM.NewContent = strCommanderList_Select;
            _maintenGuaranteeInforBM.NewText = strCommanderList_Select;
            _changeRecordBM.ChangeNewContent = strCommanderList_Select;
            if (rvSF.Result > 0)
            {
                _changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                _changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            _changeRecordBM.FOCOperatingTime = "";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(_maintenGuaranteeInforBM); //���±��ϱ�
            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(_changeRecordBM); //��ӱ����¼�������¼��
            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }

            #endregion ���±��ϱ������¼��
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        checkedListBox2.Items.Add(checkedListBox1.GetItemText(checkedListBox1.Items[i]));
                        checkedListBox1.Items.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox2.Items.Add(checkedListBox1.GetItemText(checkedListBox1.Items[i]));
                    checkedListBox1.Items.RemoveAt(i);
                    i--;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    checkedListBox1.Items.Add(checkedListBox2.GetItemText(checkedListBox2.Items[i]));
                    checkedListBox2.Items.RemoveAt(i);
                    i--;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    if (checkedListBox2.GetItemChecked(i))
                    {
                        checkedListBox1.Items.Add(checkedListBox2.GetItemText(checkedListBox2.Items[i]));
                        checkedListBox2.Items.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fmSelectUsers_Load(object sender, EventArgs e)
        {
            this.Text = _maintenGuaranteeInforBM.ColumnCaption + "(" + _maintenGuaranteeInforBM.FLTID + ")"; //����

            CommandInforBF commandInforBF = new CommandInforBF();
            ReturnValueSF rvSF = commandInforBF.GetCommanderByTypeAndStation(_CommanderTypeBM.cnvcCommanderType, _StationBM.ThreeCode); //���ݻ�����������ȡ������Ա

            if ((rvSF.Result <= 0) || (rvSF.Dt == null))
            {
                MessageBox.Show(rvSF.Message);
                return;
            }

            if (_CommanderList_Old.Trim() != "") //ԭ������Ա�����õ�ѡ����Ա�б���
            {
                string[] arrayCommanderList_Old = _CommanderList_Old.Split('��'); 
                foreach (string strCommander in arrayCommanderList_Old)
                {
                    checkedListBox2.Items.Add(strCommander);
                }
            }

            foreach (DataRow dataRow in rvSF.Dt.Rows) //���б�����Ա
            {
                string strCommander = dataRow["cnvcCommanderName"].ToString() + "(" + dataRow["cnvCommanderAccount"].ToString() + ")";
                if (checkedListBox2.Items.IndexOf(strCommander) < 0) //����ԭ������Ա�б��У�����õ�δѡ����Ա�б���
                    checkedListBox1.Items.Add(strCommander);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _success = false;
            _memo = "�û�ȡ���˲�����";

            this.Close();
        }
    }
}