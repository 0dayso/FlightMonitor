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
        #region 定义变量
        private AccountBM _accountBM;                               //登陆用户实体对象
        private ChangeLegsBM _changeLegsBM = new ChangeLegsBM();    //所选择的出港航班的信息
        private StationBM _stationBM;                               //用户选择的航站实体对象     
        private FlightOrientation _flightOrientation;               //进港、出港

        #endregion 定义变量


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

            //主题信息不能为空
            if (strCaption == "")
            {
                MessageBox.Show("主题信息不能为空！");
                return;
            }

            //提示确认
            if (MessageBox.Show(strCaption, "添加主题信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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
                //重新导入数据
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
                    dataRow["cnvcCaption"] = "---全部---";
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
                MessageBox.Show("主题添加失败！" , "提示");
                return;
            }

            #region 添加信息到 tbGuaranteeInfor 和 tbFlightChangeRecord
            //获取原记录信息
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

            //数据维护实体                                                                  
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //维护信息
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
                maintenGuaranteeInforBM.NewContent = "主(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[1]) + 1) + ")" + "记(" + arrayGuaranteeRecord[3] + ")";
            else
                maintenGuaranteeInforBM.NewContent = "主(1)记(0)";

            //获取服务器时间                                                                                                                                        
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            //变更操作实体
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //变更信息
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
                changeRecordBM.ChangeNewContent = "主(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[1]) + 1)  + ")" + "记(" + arrayGuaranteeRecord[3] + ")"; 
            else
                changeRecordBM.ChangeNewContent = "主(1)记(0)";
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
                    MessageBox.Show("插入变更记录：" + rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (rvSF.Result == 0)
                {
                    MessageBox.Show("插入变更记录：没有成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rvSF.Result == 0)
            {
                MessageBox.Show("更新保障记录：没有成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("更新保障记录：" + rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            #endregion 添加信息到 tbGuaranteeInfor 和 tbFlightChangeRecord
        }

        private void fmGuaranteeRecord_Load(object sender, EventArgs e)
        {
            ReturnValueSF returnValueSF = new ReturnValueSF();


            //窗体标题
            this.Text = "航班保障日记【航班日期：" +
                _changeLegsBM.FlightDate + " 航班号：" +
                _changeLegsBM.FlightNo + " 飞机号：" +
                _changeLegsBM.LONG_REG + " 航段： " +
                _changeLegsBM.DEPSTN +
                " - " +
                _changeLegsBM.ARRSTN +
                "】";

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
                dataRow["cnvcCaption"] = "---全部---";
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


            //保障内容
            GuaranteeContent = textBox1.Text.Trim();

            //主题信息不能为空
            if (comboBox1.SelectedIndex <= 0)
            {
                MessageBox.Show("请先选择保障主题！");
                return;
            }

            //保障信息不能为空
            if (GuaranteeContent == "")
            {
                MessageBox.Show("保障内容不能为空！");
                return;
            }

            //提示确认
            if (MessageBox.Show(
                    ("保障主题：" + (comboBox1.Items[comboBox1.SelectedIndex] as DataRowView).Row["cnvcCaption"].ToString() + Environment.NewLine + "保障内容：" + GuaranteeContent), 
                    "添加保障信息", 
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
                MessageBox.Show("记录添加失败！", "提示");
                return;
            }

            #region 添加信息到 tbGuaranteeInfor 和 tbFlightChangeRecord
            //获取原记录信息
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

            //数据维护实体                                                                  
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //维护信息
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
                maintenGuaranteeInforBM.NewContent = "主(" + arrayGuaranteeRecord[1] + ")" + "记(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[3]) + 1) + ")";
            else
                maintenGuaranteeInforBM.NewContent = "主(1)记(1)";

            //获取服务器时间                                                                                                                                        
            ServerDateTimeBF serverDateTimeBF = new ServerDateTimeBF();
            ReturnValueSF rvSF = serverDateTimeBF.GetServerDateTime();

            //变更操作实体
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //变更信息
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
            changeRecordBM.ChangeNewContent = "主(" + arrayGuaranteeRecord[1] + ")" + "记(" + Convert.ToString(Convert.ToInt32(arrayGuaranteeRecord[3]) + 1) + ")";  
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
                    MessageBox.Show("插入变更记录：" + rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (rvSF.Result == 0)
                {
                    MessageBox.Show("插入变更记录：没有成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rvSF.Result == 0)
            {
                MessageBox.Show("更新保障记录：没有成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("更新保障记录：" + rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            #endregion 添加信息到 tbGuaranteeInfor 和 tbFlightChangeRecord

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
                            dataGridViewColumn.HeaderText = "保障主题";
                            break;
                        case "CNDGUARANTEETIME":
                            dataGridViewColumn.HeaderText = "保障时间";
                            break;
                        case "CNVCGUARANTEECONTENT":
                            dataGridViewColumn.HeaderText = "保障内容";
                            dataGridViewColumn.Width = 300;
                            break;
                        case "CNDOPERATIONTIME":
                            dataGridViewColumn.HeaderText = "录入时间";
                            break;
                        case "CNVCUSER":
                            dataGridViewColumn.HeaderText = "录入人员";
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