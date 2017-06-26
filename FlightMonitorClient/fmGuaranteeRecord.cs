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
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using AirSoft.FlightMonitor.FlightMonitorDA;
using LinYong.PublicService;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmGuaranteeRecord : Form
    {
        #region �������
        private AccountBM _accountBM;                               //��½�û�ʵ�����
        private ChangeLegsBM _changeLegsBM = new ChangeLegsBM();    //��ѡ��ĳ��ۺ������Ϣ
        private StationBM _stationBM;                               //�û�ѡ��ĺ�վʵ�����     
        private FlightOrientation _flightOrientation;               //���ۡ�����

        #endregion �������


        public fmGuaranteeRecord()
        {
            InitializeComponent();
        }

        public fmGuaranteeRecord(AccountBM accountBM, StationBM stationBM, ChangeLegsBM changeLegsBM)
        {
            InitializeComponent();

            //
            _accountBM = accountBM;
            _stationBM = stationBM;
            _changeLegsBM = changeLegsBM;
        }

        public fmGuaranteeRecord(AccountBM accountBM, StationBM stationBM, ChangeLegsBM changeLegsBM, FlightOrientation flightOrientation)
        {
            InitializeComponent();

            //
            _accountBM = accountBM;
            _stationBM = stationBM;
            _changeLegsBM = changeLegsBM;
            _flightOrientation = flightOrientation;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //
            string strCaption = comboBox1.Text.Trim();

            //������Ϣ����Ϊ��
            if (strCaption == "")
            {
                MessageBox.Show("������Ϣ����Ϊ�գ�");
                return;
            }

            //��ʾȷ��
            if (MessageBox.Show(strCaption, "���������Ϣ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            //
            ReturnValueSF returnValueSF = new ReturnValueSF();
            
            GuaranteeRecordCaptionBM guaranteeRecordCaptionBM = new GuaranteeRecordCaptionBM();
            guaranteeRecordCaptionBM.cncDATOP = _changeLegsBM.DATOP;          
            guaranteeRecordCaptionBM.cnvcFLTID = _changeLegsBM.FLTID  ;      
            guaranteeRecordCaptionBM.cniLegNO = _changeLegsBM.LEGNO;        
            guaranteeRecordCaptionBM.cnvcAC = _changeLegsBM.AC;           
            guaranteeRecordCaptionBM.cncFlightDate = _changeLegsBM.FlightDate;    
            guaranteeRecordCaptionBM.cnvcFlightNo = _changeLegsBM.FlightNo;
            guaranteeRecordCaptionBM.cnvcLONG_REG = _changeLegsBM.LONG_REG;
            guaranteeRecordCaptionBM.cncDEPSTN = _changeLegsBM.DEPSTN;
            guaranteeRecordCaptionBM.cncARRSTN = _changeLegsBM.ARRSTN;       
            guaranteeRecordCaptionBM.cncSTD = _changeLegsBM.STD;           
            guaranteeRecordCaptionBM.cncStation = _stationBM.ThreeCode;       
            guaranteeRecordCaptionBM.cnvcCaption = comboBox1.Text.Trim();    
            guaranteeRecordCaptionBM.cndOperationTime = DateTime.Now ;
            guaranteeRecordCaptionBM.cnvcUserID  = _accountBM.UserId  ;   
            guaranteeRecordCaptionBM.cnvcUserName = _accountBM.UserName;
            guaranteeRecordCaptionBM.cnvcUserDepartment = _accountBM.UserDepartment;

            GuaranteeRecordCaptionBF guaranteeRecordCaptionBF = new GuaranteeRecordCaptionBF();
            returnValueSF = guaranteeRecordCaptionBF.Add(guaranteeRecordCaptionBM);

            if (returnValueSF.Result > 0)
            {
                //���µ�������
                guaranteeRecordCaptionBF = new GuaranteeRecordCaptionBF();
                returnValueSF = guaranteeRecordCaptionBF.GetDataList(_stationBM.ThreeCode,
                    _changeLegsBM.DATOP,
                    _changeLegsBM.FLTID,
                    _changeLegsBM.LEGNO,
                    _changeLegsBM.AC);

                if ((returnValueSF.Result >= 0) && (returnValueSF.Dt != null))
                {
                    DataTable dataTable = new DataTable();

                    dataTable = returnValueSF.Dt.Copy();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["cniGuaranteeRecordCaptionID"] = -1;
                    dataRow["cnvcCaption"] = "---ȫ��---";
                    dataTable.Rows.InsertAt(dataRow, 0);


                    comboBox1.DataSource = dataTable;
                    comboBox1.DisplayMember = "cnvcCaption";
                    comboBox1.ValueMember = "cniGuaranteeRecordCaptionID";

                    comboBox1.SelectedIndex = -1;
                    for (int indexItems = 0; indexItems < comboBox1.Items.Count; indexItems++)
                    {
                        if ((comboBox1.Items[indexItems] as DataRowView).Row["cnvcCaption"].ToString() == strCaption)
                        {
                            comboBox1.SelectedIndex = indexItems;
                            break;
                        }
                    }
                    //comboBox1.SelectedIndex = comboBox1.Items.IndexOf(strCaption);
                    comboBox1_SelectionChangeCommitted(sender, e);

                }
            }
            else
            {
                MessageBox.Show("�������ʧ�ܣ�" , "��ʾ");
                return;
            }

            #region �����Ϣ�� tbGuaranteeInfor �� tbFlightChangeRecord
            //��ȡԭ��¼��Ϣ
            returnValueSF = new ReturnValueSF();
            string strGuaranteeRecord = "";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            returnValueSF = guaranteeInforBF.GetFlightByKey(_changeLegsBM);
            if ((returnValueSF.Result > 0) && (returnValueSF.Dt != null))
            {
                if (_flightOrientation == FlightOrientation.In)
                {
                    strGuaranteeRecord = returnValueSF.Dt.Rows[0]["cnvcInGuaranteeRecord"].ToString() ;
                }
                else
                {
                    strGuaranteeRecord = returnValueSF.Dt.Rows[0]["cnvcOutGuaranteeRecord"].ToString();
                }
            }

            string[] arrayGuaranteeRecord =  strGuaranteeRecord.Split(new char[]{'(',')'});

            //����ά��ʵ��                                                                  
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //ά����Ϣ
            maintenGuaranteeInforBM.DATOP = _changeLegsBM.DATOP;
            maintenGuaranteeInforBM.FLTID = _changeLegsBM.FLTID;
            maintenGuaranteeInforBM.LEGNO = _changeLegsBM.LEGNO.ToString();
            maintenGuaranteeInforBM.AC = _changeLegsBM.AC;
            if (_flightOrientation == FlightOrientation.In)
            {
                maintenGuaranteeInforBM.FieldName = "cnvcInGuaranteeRecord" ;
            }
            else 
            {
                maintenGuaranteeInforBM.FieldName = "cnvcOutGuaranteeRecord" ;
            }
            maintenGuaranteeInforBM.FieldType = 1 ;
            if (arrayGuaranteeRecord.Length > 1)
                maintenGuaranteeInforBM.NewContent = "��(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[1]) + 1) + ")" + "��(" + arrayGuaranteeRecord[3] + ")";
            else
                maintenGuaranteeInforBM.NewContent = "��(1)��(0)";

            //��ȡ������ʱ��                                                                                                                                        
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            //�������ʵ��
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //�����Ϣ
            changeRecordBM.UserID = _accountBM.UserId;
            changeRecordBM.OldDATOP = _changeLegsBM.DATOP;
            changeRecordBM.OldFLTID = _changeLegsBM.FLTID;
            changeRecordBM.OldLegNo = _changeLegsBM.LEGNO;
            changeRecordBM.OldAC = _changeLegsBM.AC;
            changeRecordBM.NewDATOP = _changeLegsBM.DATOP;
            changeRecordBM.NewFLTID = _changeLegsBM.FLTID;
            changeRecordBM.NewLegNo = _changeLegsBM.LEGNO;
            changeRecordBM.NewAC = _changeLegsBM.AC;
            changeRecordBM.OldDepSTN = _changeLegsBM.DEPSTN;
            changeRecordBM.OldArrSTN = _changeLegsBM.ARRSTN;
            changeRecordBM.NewDepSTN = _changeLegsBM.DEPSTN;
            changeRecordBM.NewArrSTN = _changeLegsBM.ARRSTN;
            changeRecordBM.STD = _changeLegsBM.STD;
            changeRecordBM.ETD = _changeLegsBM.ETD;
            changeRecordBM.STA = _changeLegsBM.STA;
            changeRecordBM.ETA = _changeLegsBM.ETA;
            if (_flightOrientation == FlightOrientation.In)
            {
                changeRecordBM.ChangeReasonCode = "cnvcInGuaranteeRecord";
            }
            else
            {
                changeRecordBM.ChangeReasonCode = "cnvcOutGuaranteeRecord";
            }
            changeRecordBM.ChangeOldContent = strGuaranteeRecord;
            if (arrayGuaranteeRecord.Length > 1)
                changeRecordBM.ChangeNewContent = "��(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[1]) + 1)  + ")" + "��(" + arrayGuaranteeRecord[3] + ")"; 
            else
                changeRecordBM.ChangeNewContent = "��(1)��(0)";
            changeRecordBM.ActionTag = "U";
            changeRecordBM.Refresh = 0;
            if (rvSF.Result > 0)
            {
                changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            changeRecordBM.FOCOperatingTime = "";

            guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(maintenGuaranteeInforBM);
            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(changeRecordBM);

                if (rvSF.Result < 0)
                {
                    MessageBox.Show("��������¼��" + rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (rvSF.Result == 0)
                {
                    MessageBox.Show("��������¼��û�гɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rvSF.Result == 0)
            {
                MessageBox.Show("���±��ϼ�¼��û�гɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("���±��ϼ�¼��" + rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            #endregion �����Ϣ�� tbGuaranteeInfor �� tbFlightChangeRecord
        }

        private void fmGuaranteeRecord_Load(object sender, EventArgs e)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            //�������
            this.Text = "���ౣ���ռǡ��������ڣ�" +
                _changeLegsBM.FlightDate + " ����ţ�" +
                _changeLegsBM.FlightNo + " �ɻ��ţ�" +
                _changeLegsBM.LONG_REG + " ���Σ� " +
                _changeLegsBM.DEPSTN +
                " - " +
                _changeLegsBM.ARRSTN +
                "��";

            dateTimePicker1.Value = DateTime.Now;

            //
            GuaranteeRecordCaptionBF guaranteeRecordCaptionBF = new GuaranteeRecordCaptionBF();
            returnValueSF = guaranteeRecordCaptionBF.GetDataList(_stationBM.ThreeCode,
                _changeLegsBM.DATOP,
                _changeLegsBM.FLTID,
                _changeLegsBM.LEGNO,
                _changeLegsBM.AC);

            if ((returnValueSF.Result >= 0) && (returnValueSF.Dt != null))
            {
                DataTable dataTable = new DataTable();

                dataTable = returnValueSF.Dt.Copy();
                DataRow dataRow = dataTable.NewRow();
                dataRow["cniGuaranteeRecordCaptionID"] = -1;
                dataRow["cnvcCaption"] = "---ȫ��---";
                dataTable.Rows.InsertAt(dataRow, 0);


                comboBox1.DataSource = dataTable;
                comboBox1.DisplayMember = "cnvcCaption";
                comboBox1.ValueMember = "cniGuaranteeRecordCaptionID";

                comboBox1_SelectionChangeCommitted(sender, e);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string GuaranteeContent = "";


            //��������
            GuaranteeContent = textBox1.Text.Trim();

            //������Ϣ����Ϊ��
            if (comboBox1.SelectedIndex <= 0)
            {
                MessageBox.Show("����ѡ�������⣡");
                return;
            }

            //������Ϣ����Ϊ��
            if (GuaranteeContent == "")
            {
                MessageBox.Show("�������ݲ���Ϊ�գ�");
                return;
            }

            //��ʾȷ��
            if (MessageBox.Show(
                    ("�������⣺" + (comboBox1.Items[comboBox1.SelectedIndex] as DataRowView).Row["cnvcCaption"].ToString() + Environment.NewLine + "�������ݣ�" + GuaranteeContent), 
                    "��ӱ�����Ϣ", 
                     MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            //
            ReturnValueSF returnValueSF = new ReturnValueSF();

            GuaranteeRecordBM guaranteeRecordBM = new GuaranteeRecordBM();
            guaranteeRecordBM.cncDATOP = _changeLegsBM.DATOP;
            guaranteeRecordBM.cnvcFLTID = _changeLegsBM.FLTID;
            guaranteeRecordBM.cniLegNO = _changeLegsBM.LEGNO;
            guaranteeRecordBM.cnvcAC = _changeLegsBM.AC;
            guaranteeRecordBM.cncFlightDate = _changeLegsBM.FlightDate;
            guaranteeRecordBM.cnvcFlightNo = _changeLegsBM.FlightNo;
            guaranteeRecordBM.cnvcLONG_REG = _changeLegsBM.LONG_REG;
            guaranteeRecordBM.cncDEPSTN = _changeLegsBM.DEPSTN;
            guaranteeRecordBM.cncARRSTN = _changeLegsBM.ARRSTN;
            guaranteeRecordBM.cncSTD = _changeLegsBM.STD;
            guaranteeRecordBM.cncStation = _stationBM.ThreeCode;
            guaranteeRecordBM.cniGuaranteeRecordCaptionID = Convert.ToInt32( comboBox1.SelectedValue.ToString());
            guaranteeRecordBM.cndGuaranteeTime = dateTimePicker1.Value;
            guaranteeRecordBM.cnvcGuaranteeContent = GuaranteeContent; 
            guaranteeRecordBM.cndOperationTime = DateTime.Now;
            guaranteeRecordBM.cnvcUserID = _accountBM.UserId;
            guaranteeRecordBM.cnvcUserName = _accountBM.UserName;
            guaranteeRecordBM.cnvcUserDepartment = _accountBM.UserDepartment;

            GuaranteeRecordBF guaranteeRecordBF = new GuaranteeRecordBF();
            returnValueSF = guaranteeRecordBF.Add(guaranteeRecordBM);

            if (returnValueSF.Result > 0)
            {
                comboBox1_SelectionChangeCommitted(sender, e);
            }
            else
            {
                MessageBox.Show("��¼���ʧ�ܣ�", "��ʾ");
                return;
            }

            #region �����Ϣ�� tbGuaranteeInfor �� tbFlightChangeRecord
            //��ȡԭ��¼��Ϣ
            returnValueSF = new ReturnValueSF();
            string strGuaranteeRecord = "";

            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            returnValueSF = guaranteeInforBF.GetFlightByKey(_changeLegsBM);
            if ((returnValueSF.Result > 0) && (returnValueSF.Dt != null))
            {
                if (_flightOrientation == FlightOrientation.In)
                {
                    strGuaranteeRecord = returnValueSF.Dt.Rows[0]["cnvcInGuaranteeRecord"].ToString();
                }
                else
                {
                    strGuaranteeRecord = returnValueSF.Dt.Rows[0]["cnvcOutGuaranteeRecord"].ToString();
                }
            }

            string[] arrayGuaranteeRecord = strGuaranteeRecord.Split(new char[] { '(', ')' });

            //����ά��ʵ��                                                                  
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //ά����Ϣ
            maintenGuaranteeInforBM.DATOP = _changeLegsBM.DATOP;
            maintenGuaranteeInforBM.FLTID = _changeLegsBM.FLTID;
            maintenGuaranteeInforBM.LEGNO = _changeLegsBM.LEGNO.ToString();
            maintenGuaranteeInforBM.AC = _changeLegsBM.AC;
            if (_flightOrientation == FlightOrientation.In)
            {
                maintenGuaranteeInforBM.FieldName = "cnvcInGuaranteeRecord";
            }
            else
            {
                maintenGuaranteeInforBM.FieldName = "cnvcOutGuaranteeRecord";
            }
            maintenGuaranteeInforBM.FieldType = 1;
            if (arrayGuaranteeRecord.Length > 1)
                maintenGuaranteeInforBM.NewContent = "��(" + arrayGuaranteeRecord[1] + ")" + "��(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[3]) + 1) + ")";
            else
                maintenGuaranteeInforBM.NewContent = "��(1)��(1)";

            //��ȡ������ʱ��                                                                                                                                        
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            //�������ʵ��
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //�����Ϣ
            changeRecordBM.UserID = _accountBM.UserId;
            changeRecordBM.OldDATOP = _changeLegsBM.DATOP;
            changeRecordBM.OldFLTID = _changeLegsBM.FLTID;
            changeRecordBM.OldLegNo = _changeLegsBM.LEGNO;
            changeRecordBM.OldAC = _changeLegsBM.AC;
            changeRecordBM.NewDATOP = _changeLegsBM.DATOP;
            changeRecordBM.NewFLTID = _changeLegsBM.FLTID;
            changeRecordBM.NewLegNo = _changeLegsBM.LEGNO;
            changeRecordBM.NewAC = _changeLegsBM.AC;
            changeRecordBM.OldDepSTN = _changeLegsBM.DEPSTN;
            changeRecordBM.OldArrSTN = _changeLegsBM.ARRSTN;
            changeRecordBM.NewDepSTN = _changeLegsBM.DEPSTN;
            changeRecordBM.NewArrSTN = _changeLegsBM.ARRSTN;
            changeRecordBM.STD = _changeLegsBM.STD;
            changeRecordBM.ETD = _changeLegsBM.ETD;
            changeRecordBM.STA = _changeLegsBM.STA;
            changeRecordBM.ETA = _changeLegsBM.ETA;
            if (_flightOrientation == FlightOrientation.In)
            {
                changeRecordBM.ChangeReasonCode = "cnvcInGuaranteeRecord";
            }
            else
            {
                changeRecordBM.ChangeReasonCode = "cnvcOutGuaranteeRecord";
            }
            changeRecordBM.ChangeOldContent = strGuaranteeRecord;
            changeRecordBM.ChangeNewContent = "��(" + arrayGuaranteeRecord[1] + ")" + "��(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[3]) + 1) + ")";  
            changeRecordBM.ActionTag = "U";
            changeRecordBM.Refresh = 0;
            if (rvSF.Result > 0)
            {
                changeRecordBM.LocalOperatingTime = DateTime.Parse(rvSF.Message).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            changeRecordBM.FOCOperatingTime = "";

            guaranteeInforBF = new GuaranteeInforBF();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(maintenGuaranteeInforBM);
            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(changeRecordBM);

                if (rvSF.Result < 0)
                {
                    MessageBox.Show("��������¼��" + rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (rvSF.Result == 0)
                {
                    MessageBox.Show("��������¼��û�гɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rvSF.Result == 0)
            {
                MessageBox.Show("���±��ϼ�¼��û�гɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("���±��ϼ�¼��" + rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            #endregion �����Ϣ�� tbGuaranteeInfor �� tbFlightChangeRecord

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //
            ReturnValueSF returnValueSF = new ReturnValueSF();
            GuaranteeRecordBF guaranteeRecordBF = new GuaranteeRecordBF();


            if (comboBox1.SelectedIndex < 0)
            {
                return;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                //comboBox1.ForeColor = SystemColors.Control;

                guaranteeRecordBF = new GuaranteeRecordBF();
                returnValueSF = guaranteeRecordBF.GetDataList(_stationBM.ThreeCode,
                    _changeLegsBM.DATOP,
                    _changeLegsBM.FLTID,
                    _changeLegsBM.LEGNO,
                    _changeLegsBM.AC);

            }
            else
            {
                //comboBox1.ForeColor = SystemColors.WindowText;

                guaranteeRecordBF = new GuaranteeRecordBF();
                returnValueSF = guaranteeRecordBF.GetDataList(_stationBM.ThreeCode,
                    _changeLegsBM.DATOP,
                    _changeLegsBM.FLTID,
                    _changeLegsBM.LEGNO,
                    _changeLegsBM.AC,
                    Convert.ToInt32(comboBox1.SelectedValue.ToString()));
            }

            if ((returnValueSF.Result >= 0) && (returnValueSF.Dt != null))
            {
                dataGridView1.DataSource = returnValueSF.Dt;

                foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
                {
                    switch (dataGridViewColumn.DataPropertyName.ToUpper())
                    {
                        case "CNVCCAPTION":
                            dataGridViewColumn.HeaderText = "��������";
                            break;
                        case "CNDGUARANTEETIME":
                            dataGridViewColumn.HeaderText = "����ʱ��";
                            break;
                        case "CNVCGUARANTEECONTENT":
                            dataGridViewColumn.HeaderText = "��������";
                            dataGridViewColumn.Width = 300;
                            break;
                        case "CNDOPERATIONTIME":
                            dataGridViewColumn.HeaderText = "¼��ʱ��";
                            break;
                        case "CNVCUSER":
                            dataGridViewColumn.HeaderText = "¼����Ա";
                            break;
                        default:
                            dataGridViewColumn.Visible = false;
                            break;

                    }
                }
            }
        }
    }
}