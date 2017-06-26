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
        #region 声明
        private StationBM _StationBM = new StationBM();
        private CommanderTypeBM _CommanderTypeBM = new CommanderTypeBM();

        private MaintenGuaranteeInforBM _maintenGuaranteeInforBM;
        private ChangeRecordBM _changeRecordBM;

        private string _CommanderList_Old;
        private string _CommanderList_New;

        private string _memo = "";
        private bool _success = false;
        #endregion 声明

        #region 属性

        /// 人员列表（旧）
        /// </summary>
        public string CommanderList_Old
        {
            set { _CommanderList_Old = value; }
            get { return _CommanderList_Old; }
        }


        /// 人员列表（新）
        /// </summary>
        public string CommanderList_New
        {
            set { _CommanderList_New = value; }
            get { return _CommanderList_New; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

        /// <summary>
        /// 赋值成功标志
        /// </summary>
        public bool Success
        {
            set { _success = value; }
            get { return _success; }
        }
        #endregion 属性

        #region 构造函数

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

        #endregion 构造函数

        private void button1_Click(object sender, EventArgs e)
        {
            string strCommanderList_Select = ""; //根据选择人员列表组合成的字符串，如“人员1(renyuan1)、人员2(renyuan2)、人员3(renyuan3)”

            for (int i = 0; i < checkedListBox2.Items.Count; i++) //组合字符串
            {
                strCommanderList_Select = strCommanderList_Select + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "、";
            }
            if ((strCommanderList_Select != "") &&
                (strCommanderList_Select[strCommanderList_Select.Length - 1] == '、')) //去掉末尾的字符“、”
                strCommanderList_Select = strCommanderList_Select.Substring(0,strCommanderList_Select.Length - 1);

            if (_CommanderList_Old.Trim() == strCommanderList_Select.Trim())
            {
                _success = false;
                _memo = "新值和原值一样，不再执行保存操作。";
                this.DialogResult = DialogResult.Cancel;

                MessageBox.Show("新值和原值一样，不再执行保存操作！");

                return;
            }

            if (System.Text.Encoding.Default.GetBytes(strCommanderList_Select).Length <= 500) //字符串总长度不能大于500字节
            {
                _CommanderList_New = strCommanderList_Select;
                _success = true;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                _success = false;
                _memo = "选择的人员字符总长度大于500，超过字段长度限制。";
                this.DialogResult = DialogResult.Cancel;

                MessageBox.Show("选择的人员字符总长度大于500，请重新选择，谢谢！");

                return;
            }

            #region 更新保障表及添加变更记录到变更记录表
            //获取服务器时间
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

            rvSF = guaranteeInforBF.UpdateGuaranteeInfor(_maintenGuaranteeInforBM); //更新保障表
            if (rvSF.Result > 0)
            {
                rvSF = changeRecordBF.Insert(_changeRecordBM); //添加变更记录到变更记录表
            }
            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Close();
            }

            #endregion 更新保障表及变更记录表
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
            this.Text = _maintenGuaranteeInforBM.ColumnCaption + "(" + _maintenGuaranteeInforBM.FLTID + ")"; //标题

            CommandInforBF commandInforBF = new CommandInforBF();
            ReturnValueSF rvSF = commandInforBF.GetCommanderByTypeAndStation(_CommanderTypeBM.cnvcCommanderType, _StationBM.ThreeCode); //根据机场和类型提取保障人员

            if ((rvSF.Result <= 0) || (rvSF.Dt == null))
            {
                MessageBox.Show(rvSF.Message);
                return;
            }

            if (_CommanderList_Old.Trim() != "") //原保障人员，放置到选择人员列表中
            {
                string[] arrayCommanderList_Old = _CommanderList_Old.Split('、'); 
                foreach (string strCommander in arrayCommanderList_Old)
                {
                    checkedListBox2.Items.Add(strCommander);
                }
            }

            foreach (DataRow dataRow in rvSF.Dt.Rows) //所有保障人员
            {
                string strCommander = dataRow["cnvcCommanderName"].ToString() + "(" + dataRow["cnvCommanderAccount"].ToString() + ")";
                if (checkedListBox2.Items.IndexOf(strCommander) < 0) //不在原保障人员列表中，则放置到未选择人员列表中
                    checkedListBox1.Items.Add(strCommander);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _success = false;
            _memo = "用户取消了操作。";

            this.Close();
        }
    }
}