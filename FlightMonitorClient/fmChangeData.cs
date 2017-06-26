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
    public partial class fmChangeData : Form
    {
        private DataTable m_dtDataItemPurview;
        private DataTable m_dtFlightDelayReason;
        private DataTable m_dtDiversionDelayReason;
        private DateTimeBM m_dateTimeBM;
        private AccountBM m_accountBM;
        private string m_strFlightNo;
        private string m_strChangeReasonName;
        private int m_iQuery;       


       
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dateTimeBM">时间范围</param>
        /// <param name="strFlightNo">航班号</param>
        /// <param name="strChangeReasonName">变更原因</param>
        /// <param name="iQuery">是否时从主菜单查询</param>
        public fmChangeData(DateTimeBM dateTimeBM, AccountBM accountBM, string strFlightNo, string strChangeReasonName, int iQuery)
        {
            InitializeComponent();

            
            this.m_dateTimeBM = dateTimeBM;
            this.m_accountBM = accountBM;
            this.m_strFlightNo = strFlightNo;
            this.m_strChangeReasonName = strChangeReasonName;
            this.m_iQuery = iQuery;

            GetFlightDelayReason();
            GetDiversionDelayReason();
            GetUserDataItemPurview();
        }

        /// <summary>
        /// 获取航班延误原因代码表
        /// </summary>
        private void GetFlightDelayReason()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightDelayReasonBF flightDelayReasonBF = new FlightDelayReasonBF();
            rvSF = flightDelayReasonBF.GetAllFlightDelayReason();

            if (rvSF.Result > 0)
            {
                m_dtFlightDelayReason = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 获取备降返航代码表
        /// </summary>
        private void GetDiversionDelayReason()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            DiversionDelayReasonBF diversionDelayReasonBF = new DiversionDelayReasonBF();
            rvSF = diversionDelayReasonBF.GetAllDiversionDelayReason();

            if (rvSF.Result > 0)
            {
                m_dtDiversionDelayReason = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 获取登陆用户的数据项权限（权限、提示）
        /// </summary>
        private void GetUserDataItemPurview()
        {
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();

            ReturnValueSF rvSF = dataItemPurviewBF.GetDataItemPurviewByUserId(m_accountBM);

            if (rvSF.Result > 0)
            {
                m_dtDataItemPurview = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 将变更记录加入变更列表中
        /// </summary>
        /// <param name="dtChangeData"></param>
        private void AddChangeDataToList(DataTable dtChangeData)
        {
            lvChangeContent.Items.Clear();
            foreach (DataRow dataRow in dtChangeData.Rows)
            {
                //string strDelayCode = "";



                 

                //判断是否有权限

                string strSearch = "cnvcPrimaryNameField = '" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniDataItemPurview > 0";
               

                DataRow[] drDataItemPurview = m_dtDataItemPurview.Select(strSearch);

                if (drDataItemPurview.Length > 0)
                {
                    //if ((dataRow["cnvcPrimaryNameField"].ToString() == "cncETA" || dataRow["cnvcPrimaryNameField"].ToString() == "cncETD") && strDelayCode == "")
                    //{
                    //    continue;
                    //}
                    string strOldContent, strNewContent, strOldDepSTNName, strOldArrSTNName;
                    strOldContent = dataRow["cnvcChangeOldContent"].ToString();
                    strNewContent = dataRow["cnvcChangeNewContent"].ToString();
                    strOldDepSTNName = dataRow["cnvcOldDepSTNName"].ToString();
                    strOldArrSTNName = dataRow["cnvcOldArrSTNName"].ToString();

                    //航班状态代码转换
                    if (dataRow["cnvcPrimaryNameField"].ToString() == "cncStatusName")
                    {
                        switch (strOldContent)
                        {
                            case "SCH":
                                strOldContent = "计划";
                                break;
                            case "DEL":
                                strOldContent = "延误";
                                break;
                            case "ATD":
                                strOldContent = "推出";
                                break;
                            case "DEP":
                                strOldContent = "起飞";
                                break;
                            case "ARR":
                                strOldContent = "落地";
                                break;
                            case "ATA":
                                strOldContent = "到达";
                                break;
                        }

                        switch (strNewContent)
                        {
                            case "SCH":
                                strNewContent = "计划";
                                break;
                            case "DEL":
                                strNewContent = "延误";
                                break;
                            case "ATD":
                                strNewContent = "推出";
                                break;
                            case "DEP":
                                strNewContent = "出发";
                                break;
                            case "ARR":
                                strNewContent = "落地";
                                break;
                            case "ATA":
                                strNewContent = "到达";
                                break;
                        }
                    }

                    //延误原因代码转化
                    if (dataRow["cnvcPrimaryNameField"].ToString() == "cnvcFlightDelayName")
                    {
                        DataRow[] dataRowReason = m_dtFlightDelayReason.Select("cnvcFlightDelayCode='" + strOldContent + "'");
                        if (dataRowReason.Length > 0)
                        {
                            strOldContent = dataRowReason[0]["cnvcDelayName"].ToString();
                        }

                        dataRowReason = m_dtFlightDelayReason.Select("cnvcFlightDelayCode='" + strNewContent + "'");
                        if (dataRowReason.Length > 0)
                        {
                            strNewContent = dataRowReason[0]["cnvcDelayName"].ToString();
                        }
                    }

                    //备降返航
                    if (dataRow["cnvcPrimaryNameField"].ToString() == "cnvcDiversionDelayName")
                    {
                        DataRow[] dataRowReason = m_dtDiversionDelayReason.Select("cnvcFlightDelayCode='" + strOldContent + "'");
                        if (dataRowReason.Length > 0)
                        {
                            strOldContent = dataRowReason[0]["cnvcDelayName"].ToString();
                        }

                        dataRowReason = m_dtDiversionDelayReason.Select("cnvcFlightDelayCode='" + strNewContent + "'");
                        if (dataRowReason.Length > 0)
                        {
                            strNewContent = dataRowReason[0]["cnvcDelayName"].ToString();
                        }
                    }

                    int iSplitIndex = strOldDepSTNName.IndexOf("/");
                    if (iSplitIndex > 0)
                    {
                        strOldDepSTNName = strOldDepSTNName.Substring(0, iSplitIndex).Trim();
                    }
                    else
                    {
                        strOldDepSTNName = strOldDepSTNName.Trim();
                    }

                    iSplitIndex = strOldArrSTNName.IndexOf("/");
                    if (iSplitIndex > 0)
                    {
                        strOldArrSTNName = strOldArrSTNName.Substring(0, iSplitIndex).Trim();
                    }
                    else
                    {
                        strOldArrSTNName = strOldArrSTNName.Trim();
                    }

                    string strChangeReason = dataRow["cnvcChangeReasonName"].ToString();
                    if (dataRow["cncActionTag"].ToString() == "I")
                    {
                        strChangeReason = "新增航班";
                    }
                    else if (dataRow["cncActionTag"].ToString() == "D")
                    {
                        strChangeReason = "删除航班";
                    }

                    string[] strArr = new string[5];
                    strArr[0] = dataRow["cnvcUserName"].ToString();
                    strArr[1] = dataRow["cnvcOldFLTID"].ToString() + "(" +
                        strOldDepSTNName +
                        DateTime.Parse(dataRow["cncSTD"].ToString()).ToString("HHmm") + "—" +
                        DateTime.Parse(dataRow["cncSTA"].ToString()).ToString("HHmm") +
                        strOldArrSTNName + ")" + "<" +
                        strChangeReason + ">" +
                        strOldContent + "->" +
                        strNewContent;
                    strArr[2] = dataRow["cncLocalOperatingTime"].ToString();
                    strArr[3] = dataRow["cncSTD"].ToString().Substring(0, 10);
                    ListViewItem objListViewItem = new ListViewItem(strArr);
                   
                    lvChangeContent.Items.Add(objListViewItem); 



                   
                }
            }            
        }

        /// <summary>
        /// 窗体加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmChangeData_Load(object sender, EventArgs e)
        {
            if (m_iQuery == 0)
            {
                txtFlightNo.Text = m_strFlightNo;
                ChangeRecordBF changeRecordBF = new ChangeRecordBF();
                ReturnValueSF rvSF = changeRecordBF.GetChangeRecordsByFlightNo(m_dateTimeBM, m_strFlightNo, m_strChangeReasonName);

                if (rvSF.Result < 0)
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddChangeDataToList(rvSF.Dt);
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = dtFlightDate.Value.ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = dtFlightDate.Value.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";

            ChangeRecordBF changeRecordBF = new ChangeRecordBF();
            ReturnValueSF rvSF = changeRecordBF.GetChangeRecordsByFlightNo(dateTimeBM, txtFlightNo.Text, cmbType.Text);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                AddChangeDataToList(rvSF.Dt);
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