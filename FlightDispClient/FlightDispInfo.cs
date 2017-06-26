using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public class FlightDispInfo:Form
    {
        #region 属性

        private AccountBM m_AccountBM;
        /// <summary>
        /// 用户信息实体
        /// </summary>
        public AccountBM AccountBM
        {
            get { return m_AccountBM; }
            set { m_AccountBM = value; }
        }

        private DataTable m_dtDisplayDataItems;
        /// <summary>
        /// 用户设置显示的数据列
        /// </summary>
        public DataTable DisplayDataItems
        {
            get { return m_dtDisplayDataItems; }
            set { m_dtDisplayDataItems = value; }
        }

        //private DataTable m_dtDataItemsPurview;
        ///// <summary>
        ///// 用户所有的数据列
        ///// </summary>
        //public DataTable DataItemPurview
        //{
        //    get { return m_dtDataItemsPurview; }
        //    set { m_dtDataItemsPurview = value; }
        //}

        private PositionNameBM m_DeskNameBM;
        /// <summary>
        /// 用户席位信息实体
        /// </summary>
        public PositionNameBM DeskNameBM
        {
            get { return m_DeskNameBM; }
            set { m_DeskNameBM = value; }
        }

        private DataTable m_dtDeskFlights;
        /// <summary>
        /// 席位当天所有的航班（与数据库中的记录相同）
        /// </summary>
        public DataTable DeskFlights
        {
            get { return m_dtDeskFlights; }
            set { m_dtDeskFlights = value; }
        }

        private DataTable m_dtDisplayDeskFlights;
        /// <summary>
        /// 席位航班动态（重新组织后的显示视图）
        /// </summary>
        public DataTable DisplayDeskFlights
        {
            get { return m_dtDisplayDeskFlights; }
            set { m_dtDisplayDeskFlights = value; }
        }

        private DataTable m_dtDeskAircrafts;
        /// <summary>
        /// 席位所有的飞机
        /// </summary>
        public DataTable DeskAircrafts
        {
            get { return m_dtDeskAircrafts; }
            set { m_dtDeskAircrafts = value; }
        }

        private DataTable m_dtChangeTable = new DataTable();
        /// <summary>
        /// 航班信息变化列表
        /// </summary>
        public DataTable ChangeRecordTable
        {
            get { return m_dtChangeTable; }
            set { m_dtChangeTable = value; }
        }
        //
        //闪烁前单元格颜色
        //
        Color[,] colorArrOldBackGround;

        private int m_iLastRecordNo;
        /// <summary>
        /// 最大变更序号
        /// </summary>
        public int LastRecordNo
        {
            get { return m_iLastRecordNo; }
            set { m_iLastRecordNo = value; }
        }

        DataTable m_dtSplashTag;
        /// <summary>
        /// 闪烁标识表
        /// </summary>
        public DataTable SplashTagTable
        {
            get { return m_dtSplashTag; }
            set { m_dtSplashTag = value; }
        }

        private string m_strFormType;
        /// <summary>
        /// 窗体类型
        /// </summary>
        public string FormType
        {
            get { return m_strFormType; }
            set { m_strFormType = value; }
        }
        #endregion

        #region 构造方法
        public FlightDispInfo()
        { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="dataItems"></param>
        /// <param name="positionNameBM"></param>
        public FlightDispInfo(AccountBM accountBM, string strFormType, DataTable dtDeskAircraft)
        {
            //窗体类型
            this.m_strFormType = strFormType;
            //用户信息
            this.m_AccountBM = accountBM;
            //显示的数据列
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_AccountBM, strFormType);
            this.m_dtDisplayDataItems = rvSF.Dt;
            //该席位所有飞机
            this.m_dtDeskAircrafts = dtDeskAircraft;
            //初始化席位航班动态
            InitialDeskFlights(this.m_strFormType, this.m_dtDeskAircrafts, this.m_dtDisplayDataItems);
        }
        #endregion

        #region 获取登陆用户的所有数据项权限
        /// <summary>
        /// 获取登陆用户的数据项权限（权限、提示）
        /// </summary>
        public DataTable GetUserDataItemPurview()
        {
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvSF = dataItemPurviewBF.GetDataItemPurviewByUserId(m_AccountBM);
            DataTable dt = new DataTable();
            if (rvSF.Result > 0)
            {
                dt = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return dt;
        }
        #endregion

        #region 获取某席位的所有航班动态
        /// <summary>
        /// 获取某席位的所有航班动态
        /// </summary>
        public DataTable GetFlightsByDesk(DateTimeBM dateTimeBM, string strFormType)
        {
            //调用业务外观层方法
            FlightDispBF flightDispBF = new FlightDispBF();
            ReturnValueSF rvSF = flightDispBF.GetFlightsByDesk(dateTimeBM, strFormType);
            DataTable dtDeskFlights = new DataTable();
            if (rvSF.Result > 0)
            {
                dtDeskFlights = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return dtDeskFlights;
        }
        #endregion

        #region 获取要显示的席位航班动态表架构
        /// <summary>
        /// 获取要显示表的架构
        /// </summary>
        private DataTable GenDisplaySchema(string strFormType)
        {
            DataTable dtDisplayTable = new DataTable();
            //主键
            dtDisplayTable.Columns.Add(strFormType + "cncDATOP");
            dtDisplayTable.Columns.Add(strFormType + "cnvcFLTID");
            dtDisplayTable.Columns.Add(strFormType + "cniLEGNO");
            dtDisplayTable.Columns.Add(strFormType + "cnvcAC");
            //排序字段：计划起飞时间
            dtDisplayTable.Columns.Add(strFormType + "cncSTDSort");
            dtDisplayTable.Columns.Add(strFormType + "cniViewIndex");
            //根据要显示的字段生成席位航班动态表的架构
            foreach (DataRow dataRow in m_dtDisplayDataItems.Rows)
            {
                dtDisplayTable.Columns.Add(dataRow["cnvcDataItemID"].ToString());
            }
            return dtDisplayTable;
        }
        #endregion

        #region 生成闪烁标识表，并且插入记录的主键值
        /// <summary>
        /// 生成闪烁标识表，并且插入记录的主键值
        /// </summary>
        /// <param name="dtDeskFlights"></param>
        /// <returns></returns>
        public DataTable FillSplashTable(DataTable dtDeskFlights)
        {
            //生成闪烁标识表
            DataTable dtSplashTag = new DataTable();
            if (dtDeskFlights != null)
            {
                //添加列
                for (int iLoop = 0; iLoop < dtDeskFlights.Columns.Count; iLoop++)
                {
                    ArrayList alColName = new ArrayList();
                    alColName.Add("cncDATOP");
                    alColName.Add("cnvcFLTID");
                    alColName.Add("cnvcAC");
                    string strColName = dtDeskFlights.Columns[iLoop].ColumnName;
                    //主键列的数据类型为string
                    if (alColName.Contains(strColName))
                    {
                        dtSplashTag.Columns.Add(strColName, System.Type.GetType("System.String"));
                    }
                    //其他列的数据类型为int
                    else
                    {
                        dtSplashTag.Columns.Add(strColName, System.Type.GetType("System.Int32"));
                    }
                }
                //设置主键
                DataColumn[] pk = new DataColumn[4];
                pk[0] = dtSplashTag.Columns["cncDATOP"];
                pk[1] = dtSplashTag.Columns["cnvcFLTID"];
                pk[2] = dtSplashTag.Columns["cniLEGNO"];
                pk[3] = dtSplashTag.Columns["cnvcAC"];
                dtSplashTag.PrimaryKey = pk;
                //清空记录
                dtSplashTag.Rows.Clear();
                //加入记录
                foreach (DataRow drDeskFlight in dtDeskFlights.Rows)
                {
                    DataRow drSplash = dtSplashTag.NewRow();
                    //对主键赋值
                    drSplash["cncDATOP"] = drDeskFlight["cncDATOP"].ToString();
                    drSplash["cnvcFLTID"] = drDeskFlight["cnvcFLTID"].ToString();
                    drSplash["cniLEGNO"] = drDeskFlight["cniLEGNO"].ToString();
                    drSplash["cnvcAC"] = drDeskFlight["cnvcAC"].ToString();
                    dtSplashTag.Rows.Add(drSplash);
                }
            }
            return dtSplashTag;
        }
        #endregion

        #region 填充航班动态列表
        /// <summary>
        /// 填充航班动态列表
        /// </summary>
        /// <param name="dtDeskAircrafts"></param>
        /// <param name="dtDeskFlights"></param>
        /// <returns></returns>
        public DataTable FillFlightInfo(DataTable dtDeskAircrafts, DataTable dtDeskFlights, string strFormType, DataTable dtDisplayDataItems)
        {
            //生成表的结构
            DataTable dtFlightInfo = GenDisplaySchema(strFormType).Clone();
            if (dtDeskFlights != null)
            {
                //针对每架飞机进行处理
                foreach (DataRow drDeskAircraft in dtDeskAircrafts.Rows)
                {
                    //飞机号
                    string strLongReg = drDeskAircraft["cnvcLONG_REG"].ToString();
                    //该架飞机的所有航班
                    DataRow[] drFlights = dtDeskFlights.Select("cnvcLONG_REG = '" + strLongReg + "'", "cncSTD");
                    if (drFlights.Length > 0)
                    {
                        //针对每个航班进行处理
                        foreach (DataRow dr in drFlights)
                        {
                            DataRow drFlightInfo = dtFlightInfo.NewRow();
                            //主键
                            drFlightInfo[strFormType + "cncDATOP"] = dr["cncDATOP"].ToString();
                            drFlightInfo[strFormType + "cnvcFLTID"] = dr["cnvcFLTID"].ToString();
                            drFlightInfo[strFormType + "cniLEGNO"] = dr["cniLEGNO"].ToString();
                            drFlightInfo[strFormType + "cnvcAC"] = dr["cnvcAC"].ToString();
                            //排序字段：计划起飞时间
                            drFlightInfo[strFormType + "cncSTDSort"] = dr["cncSTD"].ToString();
                            drFlightInfo[strFormType + "cniViewIndex"] = dr["cniViewIndex"].ToString();
                            //对用户设置要显示的每个字段赋值
                            foreach (DataRow drDataItem in dtDisplayDataItems.Rows)
                            {
                                //数据项在tbFlightDispInfo表中的字段名称
                                string strFieldName = drDataItem["cnvcPrimaryNameField"].ToString();
                                //字段的值
                                string strFieldValue = dr[strFieldName].ToString().Trim();
                                //数据项名称
                                string strFieldId = drDataItem["cnvcDataItemID"].ToString();

                                //对特殊字段的值格式化
                                //对航班时刻格式化
                                ArrayList alSpecialFields = new ArrayList();
                                alSpecialFields.Add(strFormType + "cncSTD");
                                alSpecialFields.Add(strFormType + "cncSTA");
                                alSpecialFields.Add(strFormType + "cncETD");
                                alSpecialFields.Add(strFormType + "cncETA");
                                alSpecialFields.Add(strFormType + "cncTOFF");
                                alSpecialFields.Add(strFormType + "cncATD");
                                alSpecialFields.Add(strFormType + "cncATA");
                                alSpecialFields.Add(strFormType + "cncTDWN");
                                if (alSpecialFields.Contains(strFieldId))
                                {
                                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                }
                                alSpecialFields.Clear();
                                //对机场名称格式化
                                alSpecialFields.Add(strFormType + "cncDEPAirportCNAME");
                                alSpecialFields.Add(strFormType + "cncARRAirportCNAME");
                                if (alSpecialFields.Contains(strFieldId))
                                {
                                    int iSplitIndex = strFieldValue.IndexOf("/");
                                    if (iSplitIndex > 0)
                                    {
                                        strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                                    }
                                }
                                alSpecialFields.Clear();

                                //对字段赋值
                                drFlightInfo[strFieldId] = strFieldValue;
                            }//foreach (DataRow drDataItem in dtDisplayDataItems.Rows)
                            dtFlightInfo.Rows.Add(drFlightInfo);
                        }//foreach (DataRow dr in drFlights)
                    }//if (drFlights.Length > 0)
                }//foreach (DataRow drDeskAircraft in dtDeskAircrafts.Rows)
            }//if (dtDeskFlights != null)

            //排序
            //对于放行界面的航班，按STD升序排列
            if (strFormType == "Dsp")
            {
                dtFlightInfo.DefaultView.Sort = strFormType + "cncSTDSort";
            }
            //对于监控界面的航班，首先按航班状态排列，其次按STD排列
            else if(strFormType == "Mon")
            {
                dtFlightInfo.DefaultView.Sort = strFormType + "cniViewIndex desc, " + strFormType + "cncSTDSort";
            }
            return dtFlightInfo.DefaultView.ToTable();
        }
        #endregion

        #region 计算过站时间、开关舱时间等
        /// <summary>
        /// 计算过站时间、开关舱时间
        /// </summary>
        //private void ComputeFlightsInfor(DataTable dtDeskFlights, AccountBM accountBM)
        //{
        //    int iRowIndex = 0;
        //    foreach (DataRow drDeskFlight in dtDeskFlights.Rows)
        //    {
        //        //计算开关舱时间
        //        if (drDeskFlight["cncOpenCabinTime"].ToString().Trim() != "" && drDeskFlight["cncClosePaxCabinTime"].ToString().Trim() != "")
        //        {
        //            int iOpenPaxCabinTime = 0;
        //            TimeSpan tsOpenCabinTime = new TimeSpan(0, Convert.ToInt32(drDeskFlight["cncOpenCabinTime"].ToString().Substring(0, 2)), Convert.ToInt32(drDeskFlight["cncOpenCabinTime"].ToString().Substring(2, 2)));
        //            TimeSpan tsClosePaxCabinTime = new TimeSpan(0, Convert.ToInt32(drDeskFlight["cncOpenCabinTime"].ToString().Substring(0, 2)), Convert.ToInt32(drDeskFlight["cncOpenCabinTime"].ToString().Substring(2, 2)));
        //            if (tsClosePaxCabinTime.Hours - tsOpenCabinTime.Hours < 0)
        //            {
        //                iOpenPaxCabinTime = 24 * 3600 + (tsClosePaxCabinTime.Hours - tsOpenCabinTime.Hours) * 60 + (tsClosePaxCabinTime.Minutes - tsOpenCabinTime.Minutes);
        //            }
        //            else
        //            {
        //                iOpenPaxCabinTime = (tsClosePaxCabinTime.Hours - tsOpenCabinTime.Hours) * 60 + (tsClosePaxCabinTime.Minutes - tsOpenCabinTime.Minutes);
        //            }
        //            drDeskFlight["cniOpenTime"] = iOpenPaxCabinTime;
        //        }
        //        //判断机场名是否为空
        //        if (drDeskFlight["cncDEPAirportCNAME"].ToString() == "")
        //        {
        //            drDeskFlight["cncDEPAirportCNAME"] = drDeskFlight["cncDEPCityThreeCode"].ToString();
        //        }
        //        if (drDeskFlight["cncARRAirportCNAME"].ToString() == "")
        //        {
        //            drDeskFlight["cncARRAirportCNAME"] = drDeskFlight["cncARRCityThreeCode"].ToString();
        //        }
        //        //判断是否没有落地动态
        //        if (drDeskFlight["cncSTATUS"].ToString().Trim() != "ATA")
        //        {
        //            if (DateTime.Parse(drDeskFlight["cncETA"].ToString()).AddMinutes(accountBM.TDWNMinutes).CompareTo(DateTime.Now) < 0)
        //            {
        //                drDeskFlight["cniNotTDWN"] = 1;
        //            }
        //            else
        //            {
        //                drDeskFlight["cniNotTDWN"] = 0;
        //            }
        //        }
        //        //判断是否有起飞动态
        //        //没有起飞动态告警
        //        if (drDeskFlight["cncSTATUS"].ToString().Trim() != "DEP" || drDeskFlight["cncSTATUS"].ToString().Trim() != "ATA")
        //        {
        //            if (DateTime.Parse(drDeskFlight["cncETD"].ToString()).AddMinutes(accountBM.TOFFPromt).CompareTo(DateTime.Now) < 0)
        //            {
        //                drDeskFlight["cniNotTOFF"] = 1;
        //            }
        //            else
        //            {
        //                drDeskFlight["cniNotTOFF"] = 0;
        //            }
        //        }
        //        //标准过站时间
        //        string strDEPAirportPaxType = drDeskFlight["cniDEPAirportPaxType"].ToString().Trim();
        //        if (strDEPAirportPaxType == "")
        //        {
        //            strDEPAirportPaxType = "1";
        //        }
        //        //判断是否过站不足
        //        DataRow[] drStandardIntermission = dtStandardIntermissionTime.Select("cncACTYPE = '" + drDeskFlight["cncACTYP"].ToString() + "' AND cniAirportPaxType = " + strDEPAirportPaxType);
        //        TimeSpan tsIntermissionTime = new TimeSpan(0, 0, 0);
        //        if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() == dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
        //        {
        //            //计算过站时间
        //            tsIntermissionTime = DateTime.Parse(drDeskFlight["cncETD"].ToString()).Subtract(DateTime.Parse(dtDeskFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
        //            drDeskFlight["cniIntermissionTime"] = tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes;
        //            if (drStandardIntermission.Length > 0)
        //            {
        //                if (drDeskFlight["cncDEPIsSelf"].ToString().Trim() == "1" && drDeskFlight["cncARRIsSelf"].ToString().Trim() == "1") //国内航班
        //                {
        //                    if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
        //                    {
        //                        drDeskFlight["cniNotEnoughIntermissionTime"] = 1;
        //                    }
        //                }
        //                else   //国际航班
        //                {
        //                    if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniInterIntermissionTime"].ToString()))
        //                    {
        //                        drDeskFlight["cniNotEnoughIntermissionTime"] = 1;
        //                    }
        //                }//else
        //            }//if (drStandardIntermission.Length > 0)
        //        }//if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() == dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())

        //        //计算放行延误时间
        //        //始发航班                
        //        if (iRowIndex == 0 || drDeskFlight["cnvcLONG_REG"].ToString() != dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
        //        {
        //            if (drDeskFlight["cnvcDELAY1"].ToString() != "")
        //            {
        //                drDeskFlight["cniDischargingDelayTime"] = drDeskFlight["cniDUR1"].ToString();
        //            }
        //        }
        //        //过站航班
        //        else if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() == dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
        //        {
        //            if (drStandardIntermission.Length > 0)
        //            {
        //                //计算过站时间
        //                tsIntermissionTime = DateTime.Parse(drDeskFlight["cncSTD"].ToString()).Subtract(DateTime.Parse(dtDeskFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
        //                if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes >= Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
        //                {
        //                    if (drDeskFlight["cnvcDELAY1"].ToString() != "")
        //                    {
        //                        drDeskFlight["cniDischargingDelayTime"] = drDeskFlight["cniDUR1"].ToString();
        //                    }
        //                }
        //                else
        //                {
        //                    tsIntermissionTime = DateTime.Parse(drDeskFlight["cncTOFF"].ToString()).Subtract(DateTime.Parse(dtDeskFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
        //                    if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
        //                    {
        //                        if (drDeskFlight["cnvcDELAY1"].ToString() != "")
        //                        {
        //                            drDeskFlight["cniDischargingDelayTime"] = tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes - Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString());
        //                        }//if (drDeskFlight["cnvcDELAY1"].ToString() != "")
        //                    }//if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
        //                }//else
        //            }//if (drStandardIntermission.Length > 0)
        //        }//else if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() == dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())

        //        if (drStandardIntermission.Length > 0)
        //        {
        //            //判断是否没有关舱时间
        //            if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() != dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() && tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > 180) //过站航班
        //            {
        //                if (DateTime.Now.AddMinutes(Convert.ToInt32(drStandardIntermission[0]["cniIntermissionCloseCabinTime"].ToString())) > DateTime.Parse(drDeskFlight["cncETD"].ToString()))
        //                {
        //                    drDeskFlight["cniNotClosePaxCabineTime"] = 1;
        //                }
        //            }
        //            else  //始发航班
        //            {
        //                if (DateTime.Now.AddMinutes(Convert.ToInt32(drStandardIntermission[0]["cniInitialCloseCabinTime"].ToString())) > DateTime.Parse(drDeskFlight["cncETD"].ToString()))
        //                {
        //                    drDeskFlight["cniNotClosePaxCabineTime"] = 1;
        //                }
        //            }
        //        }

        //        iRowIndex += 1;
        //    }
        //}
        #endregion

        #region 设置表格颜色
        /// <summary>
        /// 设置表格颜色
        /// </summary>
        public void SetGridColor(DataTable dtDisplayDeskFlights, DataTable dtDeskFlights, DataTable dtDisplayDataItems, FpSpread fpsFlights, string strFormType)
        {
            try
            {
                int iRowIndex = 0;
                foreach (DataRow drDisplayDeskFlight in dtDisplayDeskFlights.Rows)
                {
                    string strDATOP = drDisplayDeskFlight[strFormType + "cncDATOP"].ToString();
                    string strFLTID = drDisplayDeskFlight[strFormType + "cnvcFLTID"].ToString();
                    string strLEGNO = drDisplayDeskFlight[strFormType + "cniLEGNO"].ToString();
                    string strAC = drDisplayDeskFlight[strFormType + "cnvcAC"].ToString();
                    DataRow[] drDeskFlight = dtDeskFlights.Select("cncDATOP='" + strDATOP + "' AND cnvcFLTID = '" + strFLTID + "' AND cniLEGNO = " + strLEGNO + " AND cnvcAC = '" + strAC + "'");
                    if (drDeskFlight.Length > 0)
                    {
                        //航班状态
                        string strFlightStatus = drDeskFlight[0]["cncSTATUS"].ToString();
                        //如果航班已落地则将该行背景色设为银灰色
                        if (strFlightStatus == "ATA" || strFlightStatus == "ARR")
                        {
                            SetRowCellColor(fpsFlights, iRowIndex, Color.Silver);
                            fpsFlights.Sheets[0].Rows[iRowIndex].BackColor = Color.Silver;
                        }
                        else
                        {
                            foreach (DataRow drDataItem in dtDisplayDataItems.Rows)
                            {
                                //数据项名称
                                string strFieldName = drDataItem["cnvcDataItemID"].ToString();
                                //数据项在视图中的列号
                                int iColumnIndex = Convert.ToInt32(drDataItem["cniViewIndex"].ToString());

                                #region 航班号单元格颜色
                                //航班号单元格
                                if (strFieldName == strFormType + "cnvcFlightNo")
                                {
                                    //备降、返航：航班号单元格背景色=Yellow
                                    if (drDeskFlight[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drDeskFlight[0]["cniLEGNO"].ToString()) % 100 != 0)
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.Yellow;
                                    }
                                    //重点保障航班：航班号单元格背景色=Yellow
                                    //if (drDeskFlight[0]["cniFocusTag"].ToString().Trim() != "")
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.Orange;
                                    //    fpsFlightDisp.Sheets[0].SetNote(iRowIndex, iColumnIndex, drDeskFlight[0]["cniFocusTag"].ToString().Trim());
                                    //}
                                    //else
                                    //{
                                    //    fpsFlightDisp.Sheets[0].SetNote(iRowIndex, iColumnIndex, "");
                                    //}
                                    //过站时间不足：航班号单元格背景色=Plum
                                    //if (drDeskFlight[0]["cniNotEnoughIntermissionTime"].ToString() == "1")
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.Plum;
                                    //}
                                    //else
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = fpsFlightDisp.Sheets[0].Rows[iRowIndex].BackColor;
                                    //}
                                }
                                #endregion

                                #region 航班性质单元格颜色
                                //航班性质
                                else if (strFieldName == strFormType + "cnvcFlightCharacterAbbreviate")
                                {
                                    //航班性质不是正班：航班性质单元格前景色=Red
                                    if (drDeskFlight[0]["cnvcSTC"].ToString() != "J")
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].ForeColor = Color.Black;
                                    }
                                }
                                #endregion

                                #region 航班延误情况下单元格颜色设置
                                //航班延误
                                else if (strFieldName == strFormType + "cncETA" || strFieldName == strFormType + "cncETD")
                                {
                                    //航班延误：ETA和ETD单元格背景色=Yellow
                                    if (drDeskFlight[0]["cnvcDELAY1"].ToString().Trim() != "")
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.Yellow;
                                    }
                                    else
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = fpsFlights.Sheets[0].Rows[iRowIndex].BackColor;
                                    }
                                }
                                #endregion

                                #region 起飞时间单元格颜色
                                //起飞时间
                                else if (strFieldName == strFormType + "cncTOFF")
                                {
                                    //没有起飞动态告警
                                    //if (m_accountBM.TDWNPromt == 1 && drOutFlights[0]["cniNotTOFF"].ToString() == "1")
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.SkyBlue;
                                    //}
                                    //else
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = fpsFlightDisp.Sheets[0].Rows[iRowIndex].BackColor;
                                    //}
                                }
                                #endregion

                                #region 到达时间单元格颜色
                                //到达时间
                                else if (strFieldName == strFormType + "cncTDWN")
                                {
                                    //航班已起飞：到达时间单元格背景色=LimeGreen
                                    string strTemp = drDeskFlight[0]["cncSTATUS"].ToString().Trim();
                                    if (drDeskFlight[0]["cncSTATUS"].ToString().Trim() == "DEP")
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.LimeGreen;
                                    }
                                    //没有到达动态告警
                                    //if (m_accountBM.TDWNPromt == 1 && drDeskFlight[0]["cniNotTDWN"].ToString() == "1")
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.SkyBlue;
                                    //}
                                }
                                #endregion

                                //其他
                                else
                                {
                                    if (strFieldName.IndexOf(strFormType) == 0)
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = fpsFlights.Sheets[0].Rows[iRowIndex].BackColor; ;
                                    }//if (strFieldName.IndexOf(strFormType) == 0)
                                }//if (strFieldName == strFormType + "cnvcFlightNo")
                            }//foreach (DataRow drDataItem in dtDisplayDataItems.Rows)
                        }//if (strFlightStatus == "ATA" || strFlightStatus == "ARR")
                    }//if (drDeskFlight.Length > 0)
                    iRowIndex += 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
            }
        }
        #endregion

        #region 设置某行单元格颜色
        /// <summary>
        /// 设置某行单元格颜色
        /// </summary>
        /// <param name="iRowIndex"></param>
        public void SetRowCellColor(FpSpread fpsFlights, int iRowIndex, Color colorValue)
        {
            for (int jLoop = 0; jLoop < fpsFlights.Sheets[0].Columns.Count; jLoop++)
            {
                if (fpsFlights.Sheets[0].Cells[iRowIndex, jLoop].BackColor != Color.Yellow)
                {
                    fpsFlights.Sheets[0].Cells[iRowIndex, jLoop].BackColor = colorValue;
                    fpsFlights.Sheets[0].SetNote(iRowIndex, jLoop, "");
                }
            }
        }
        #endregion

        #region 获取最大变更序号
        /// <summary>
        /// 获取最大变更序号
        /// </summary>
        public int GetMaxRecordNo()
        {
            //调用业务外观层方法
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();
            ReturnValueSF rvSF = changeRecordBF.GetMaxRecordNo();
            int iLastRecordNo = 0;
            if (rvSF.Result > 0)
            {
                iLastRecordNo = rvSF.Result;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return iLastRecordNo;
        }
        #endregion

        #region 设置闪烁时间
        ///// <summary>
        ///// 根据航班变更设置闪烁时间
        ///// </summary>
        ///// <param name="drFlightChange">航班变更记录</param>
        ///// <param name="drSplashTag">闪烁标记记录</param>
        ///// <param name="iChangeMode">航班变更模式</param>
        ///// iChangeMode = 0：不需要刷新视图
        ///// iChangeMode = 1：需要刷新视图，新增航班
        ///// iChangeMode = 2：需要刷新视图，航班记录主键变更
        //private void SetSplash(DataRow[] drSplashTag, DataRow drFlightChange, int iChangeMode)
        //{
        //    int iSplash = 0;

        //    //查询变更的航班是否在显示的席位航班动态中
        //    string strSearch = "DspcncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
        //        "DspcnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
        //        "DspcniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
        //        "DspcnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "'";
        //    DataRow[] drSplashFlights = m_dtDisplayDeskFlights.Select(strSearch);

        //    //如果变更的航班显示在席位航班动态中
        //    if (drSplashFlights.Length > 0)
        //    {
        //        //查询涉及变更的数据项是否显示
        //        strSearch = "cnvcDataItemID = 'Dsp" + drFlightChange["cnvcChangeReasonCode"].ToString() + "'";
        //        DataRow[] drChangeDataItem = m_dtDisplayDataItems.Select(strSearch);
        //        //如果存在，则闪烁标记设为1
        //        if (drChangeDataItem.Length > 0)
        //        {
        //            iSplash = 1;
        //        }
        //    }

        //    //判断涉及变更的数据项是否设置了闪烁
        //    strSearch = "cnvcPrimaryCodeField = '" + drFlightChange["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
        //    DataRow[] drDataItemSplash = m_dtDataItemsPurview.Select(strSearch);
        //    //变更的数据项的名称
        //    string strChangedField = drFlightChange["cnvcChangeReasonCode"].ToString();

        //    //如果需要闪烁提示
        //    if (drDataItemSplash.Length > 0)
        //    {
        //        switch (iChangeMode)
        //        {
        //            case 0:
        //                //设置闪烁时间
        //                if (iSplash == 1)
        //                {
        //                    drSplashTag[0][strChangeField] = m_AccountBM.SplashSeconds;
        //                }
        //                break;
        //            case 1:
        //                if (iSplash == 1)
        //                {
        //                    //增加闪烁记录
        //                    DataRow drSplash = m_dtSplashTag.NewRow();
        //                    drSplash["cncDATOP"] = drNewFlight["cncDATOP"].ToString();
        //                    drSplash["cnvcFLTID"] = drNewFlight["cnvcFLTID"].ToString();
        //                    drSplash["cniLEGNO"] = drNewFlight["cniLEGNO"].ToString();
        //                    drSplash["cnvcAC"] = drNewFlight["cnvcAC"].ToString();
        //                    drSplash["cnvcLONG_REG"] = m_AccountBM.SplashSeconds.ToString();
        //                    //将新纪录添加到航班信息表和闪烁表中
        //                    m_dtSplashTag.Rows.Add(drSplash);
        //                }
        //                break;
        //            case 2:
        //                //如果变更的数据项为ETA或者ETD
        //                if (strChangedField == "cncETA" || strChangedField == "cncETD")
        //                {
        //                    //如果延误原因不为空AND闪烁标记=1
        //                    //即如果航班没有延误，ETA和ETD变化不闪烁
        //                    if (dtChangeLegs.Rows[0]["cnvcDELAY1"].ToString() != "" && iSplash == 1)
        //                    {
        //                        //设置闪烁时间
        //                        drSplashTag[0][strChangedField] = m_AccountBM.SplashSeconds.ToString();
        //                    }
        //                }
        //                //如果闪烁标记=1
        //                else if (iSplash == 1)
        //                {
        //                    //如果变更的数据项为航班状态
        //                    if (strChangedField == "cncSTATUS")
        //                    {
        //                        //如果航班状态变为DEP
        //                        if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEP")
        //                        {
        //                            //航班起飞时间和落地时间单元格也闪烁
        //                            drSplashTag[0]["cncTDWN"] = m_AccountBM.SplashSeconds;
        //                            drSplashTag[0]["cncTOFF"] = m_AccountBM.SplashSeconds;
        //                        }

        //                        //如果航班状态变更为DEL
        //                        if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEL")
        //                        {
        //                            //航班的预计起飞时间和预达时间也闪烁
        //                            drSplashTag[0]["cncETD"] = m_AccountBM.SplashSeconds;
        //                            drSplashTag[0]["cncETA"] = m_AccountBM.SplashSeconds;
        //                        }
        //                    }
        //                }
        //                //对于其他数据项，设置闪烁时间
        //                drSplashTag[0][strChangedField] = m_AccountBM.SplashSeconds;
        //                break;
        //            default:
        //                break;
        //        }//end switch
        //    }//end if (drDataItemSplash.Length > 0)
        //}
        #endregion

        #region 闪烁函数
        /// <summary>
        /// 闪烁函数
        /// </summary>
        /// <param name="dtDisplayDataItems">用户设置显示的数据项</param>
        /// <param name="dtSplashTagTable">闪烁标识表</param>
        /// <param name="dtDisplayDeskFlights">显示的席位航班动态表</param>
        /// <param name="dtDeskFlights">原始席位航班动态表</param>
        /// <param name="fpsFlightDisp">待闪烁的窗体</param>
        public void Splash(DataTable dtDisplayDataItems, DataTable dtSplashTagTable, DataTable dtDisplayDeskFlights, FpSpread fpsFlightDisp, Color oldBackGroundColor)
        {
            try
            {
                string strDisplayDataItemSearch = "";

                //生成查询字符串
                //用于查询用户设置了显示的字段
                //同时，字段值大于0（即设置了闪烁时间）
                foreach (DataRow drDisplayDataItem in dtDisplayDataItems.Rows)
                {
                    strDisplayDataItemSearch += drDisplayDataItem["cnvcPrimaryCodeField"].ToString() + ">0 OR ";
                }
                if (strDisplayDataItemSearch != null && strDisplayDataItemSearch != "")
                {
                    strDisplayDataItemSearch = strDisplayDataItemSearch.Substring(0, strDisplayDataItemSearch.Length - 3);
                }
                else
                {
                    return;
                }
                //查询闪烁标识表中，用户设置了显示，同时设置了闪烁的记录
                DataRow[] drSplashTag = dtSplashTagTable.Select(strDisplayDataItemSearch);
                if (drSplashTag.Length <= 0)
                {
                    return;
                }

                StringBuilder strSearch = new StringBuilder();
                foreach (DataRow drSplash in drSplashTag)
                {
                    strSearch.Append("(DspcncDATOP = '" + drSplash["cncDATOP"].ToString() + "' AND ");
                    strSearch.Append("DspcnvcFLTID = '" + drSplash["cnvcFLTID"].ToString() + "' AND ");
                    strSearch.Append("DspcniLEGNO = " + drSplash["cniLEGNO"].ToString() + " AND ");
                    strSearch.Append("DspcnvcAC = '" + drSplash["cnvcAC"].ToString() + "') OR ");
                }
                strSearch.Remove(strSearch.Length - 3, 3);
                //有项目需要闪烁的行
                DataRow[] drDeskFlights = dtDisplayDeskFlights.Select(strSearch.ToString());

                //逐行遍历有项目需要闪烁的进出港航班信息记录表
                foreach (DataRow drDeskFlight in drDeskFlights)
                {
                    int iRowIndex = 0;
                    //确定需要闪烁提示的航班所在的行
                    iRowIndex = dtDisplayDeskFlights.Rows.IndexOf(drDeskFlight);
                    //逐列处理需要闪烁的字段
                    for (int iLoop = 0; iLoop < fpsFlightDisp.Sheets[0].Columns.Count; iLoop++)
                    {
                        //查找字段信息
                        string strSearchDataItem = "cnvcDataItemID = '" + fpsFlightDisp.Sheets[0].Columns[iLoop].DataField + "'";
                        DataRow[] drDataItem = dtDisplayDataItems.Select(strSearchDataItem);
                        string strPrimaryCodeField = drDataItem[0]["cnvcPrimaryCodeField"].ToString();
                        string strDataItemName = drDataItem[0]["cnvcDataItemName"].ToString();

                        //查找航班信息
                        strSearch.Remove(0, strSearch.Length);
                        strSearch.Append("(DspcncDATOP = '" + drDeskFlight["cncDATOP"].ToString() + "' AND ");
                        strSearch.Append("DspcnvcFLTID = '" + drDeskFlight["cnvcFLTID"].ToString() + "' AND ");
                        strSearch.Append("DspcniLEGNO = " + drDeskFlight["cniLEGNO"].ToString() + " AND ");
                        strSearch.Append("DspcnvcAC = '" + drDeskFlight["cnvcAC"].ToString() + "') OR ");
                        drSplashTag = dtSplashTagTable.Select(strSearch.ToString());

                        if (drSplashTag.Length > 0)
                        {
                            //闪烁表中记录的闪烁时间
                            string strSplashTime = drSplashTag[0][strPrimaryCodeField].ToString().Trim();
                            if (strSplashTime != "")
                            {
                                //转换成整数
                                int iSplashTime = Convert.ToInt32(strSplashTime);
                                if (iSplashTime > 0)
                                {
                                    #region 如果是当前时间的秒数是偶数
                                    //如果是当前时间的秒数是偶数
                                    if (DateTime.Now.Second % 2 == 0)
                                    {
                                        //如果存储在内存中的闪烁表的闪烁时间与用户设置的闪烁时间相同
                                        if (iSplashTime == m_AccountBM.SplashSeconds)
                                        {
                                            if (fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor != Color.DarkBlue)
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor;
                                            }
                                            else
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = oldBackGroundColor;
                                            }
                                        }
                                        //单元格颜色设为红色
                                        fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor = Color.Red;
                                        //设置列标题闪烁
                                        //如果列标题为两行
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.Red;
                                        }
                                        else
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.Red;
                                        }
                                        //如果用户设置自动停止闪烁，或是设置了闪烁持续时间
                                        if (m_AccountBM.SplashAutoStop == 1 || iSplashTime == m_AccountBM.SplashSeconds)
                                        {
                                            //闪烁持续时间递减一秒
                                            drSplashTag[0][strPrimaryCodeField] = iSplashTime - 1;
                                        }
                                        //如果闪烁持续时间为0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //恢复单元格原来的颜色
                                            fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                            if (strDataItemName.IndexOf("|") >= 0)
                                            {
                                                fpsFlightDisp.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                            }
                                            else
                                            {
                                                fpsFlightDisp.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                            }
                                        }
                                    }
                                    #endregion

                                    #region 如果是当前时间的秒数是奇数
                                    //如果是当前时间的秒数是奇数
                                    else if (DateTime.Now.Second % 2 == 1)
                                    {
                                        //如果存储在内存中的闪烁表的闪烁时间与用户设置的闪烁时间相同
                                        if (iSplashTime == m_AccountBM.SplashSeconds)
                                        {
                                            if (fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor != Color.DarkBlue)
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor;
                                            }
                                            else
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = oldBackGroundColor;
                                            }
                                        }
                                        //恢复到单元格原来的颜色
                                        fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                        //列标题
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                        }
                                        else
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                        }
                                        //闪烁持续时间递减一秒
                                        if (m_AccountBM.SplashAutoStop == 1 || iSplashTime == m_AccountBM.SplashSeconds)
                                        {
                                            drSplashTag[0][strPrimaryCodeField] = iSplashTime - 1;
                                        }
                                        //如果持续时间为0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //恢复原来的颜色
                                            fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                            if (strDataItemName.IndexOf("|") >= 0)
                                            {
                                                fpsFlightDisp.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                            }
                                            else
                                            {
                                                fpsFlightDisp.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                            }
                                        }
                                    }
                                    #endregion

                                }//end if (Convert.ToInt32(iSplashTime) > 0)
                            }//end if (iSplashTime != "")
                        }//end if (drSplashTag.Length > 0)
                    }//end for (int iLoop = 0; iLoop < fpsFlightDisp.Sheets[1].Columns.Count; iLoop++)
                }//end foreach (DataRow drInOutFlight in drInOutFlights)
            }//end try
            catch (Exception ex)
            {
                string strError = ex.Message;
            }
        }
        #endregion

        #region 初始化表格背景色
        /// <summary>
        /// 初始化表格背景色
        /// </summary>
        public void InitialGridColor(FarPoint.Win.Spread.SheetView shGrid)
        {
            for (int iLoop = 0; iLoop < shGrid.Rows.Count; iLoop++)
            {
                shGrid.Rows[iLoop].BackColor = Color.White;
                for (int jLoop = 0; jLoop < shGrid.Columns.Count; jLoop++)
                {
                    shGrid.Cells[iLoop, jLoop].BackColor = Color.White;
                }
            }
        }
        #endregion

        #region 初始化席位航班动态
        /// <summary>
        /// 初始化席位航班动态
        /// </summary>
        /// <param name="deskNameBM">席位名称</param>
        /// <param name="FormType">窗体类型：Dsp/Mon</param>
        /// <param name="dtDeskAircrafts">席位的飞机</param>
        /// <param name="dtDeskFlights">席位的航班</param>
        public void InitialDeskFlights(string FormType, DataTable dtDeskAircrafts, DataTable dtDisplayDataItems)
        {
            //设置今天的时间范围
            DateTimeBM dateTimeBM = GetDateTimeBM(1);
            //查询该席位所有航班
            this.DeskFlights = GetFlightsByDesk(dateTimeBM, FormType);
            //生成显示的航班动态视图
            this.DisplayDeskFlights = FillFlightInfo(dtDeskAircrafts, this.DeskFlights, FormType, dtDisplayDataItems);
            //生成闪烁标识表
            this.SplashTagTable = FillSplashTable(this.DeskFlights);
            //查询最大变更标识
            this.LastRecordNo = GetMaxRecordNo();
        }
        #endregion

        #region 获取某天时间范围
        /// <summary>
        /// 获取某天时间范围
        /// </summary>
        /// <param name="iDay">哪一天0:昨天1:今天2:明天3:选择的日期</param>
        public DateTimeBM GetDateTimeBM(int iDay)
        {
            DateTimeBM dataTimeBM = new DateTimeBM();
            string strStartDateTime = "";
            string strEndDateTime = "";
            if (iDay == 0)  //昨天
            {
                strStartDateTime = DateTime.Now.AddHours(-5).Date.AddDays(-1).ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = DateTime.Now.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else if (iDay == 1) //当天时间范围实体对象
            {
                strStartDateTime = DateTime.Now.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = DateTime.Now.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else if (iDay == 2) //明天时间范围实体对象
            {
                strStartDateTime = DateTime.Now.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = DateTime.Now.AddHours(-5).Date.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";
            }
            dataTimeBM.StartDateTime = strStartDateTime;
            dataTimeBM.EndDateTime = strEndDateTime;
            return dataTimeBM;
        }
        #endregion

        #region 查看或维护单元格数据
        /// <summary>
        /// 查看或维护单元格数据
        /// </summary>
        /// <param name="e">单元格</param>
        /// <param name="fpsFlightDisp">视图控件</param>
        /// <param name="dtDataItemPurview">用户权限列表</param>
        /// <param name="dtDeskFlights">席位航班动态</param>
        /// <param name="dtDisplayDeskFlights">席位显示的航班动态</param>
        /// <param name="strFormType">窗体类型</param>
        /// <param name="accountBM">用户信息</param>
        public void Maintennance(FarPoint.Win.Spread.CellClickEventArgs e, FarPoint.Win.Spread.FpSpread fpsFlightDisp, DataTable dtDisplayDataItems, DataTable dtDeskFlights, DataTable dtDisplayDeskFlights, string strFormType, AccountBM accountBM, FlightDispInfo thisForm)
        {
            //列名
            string strDataItemID = fpsFlightDisp.ActiveSheet.Columns[e.Column].DataField.ToString();
            //获取用户权限信息
            DataRow[] drDataItemPurview = dtDisplayDataItems.Select("cnvcDataItemID = '" + strDataItemID + "'");
            //判断是否有权限
            if (drDataItemPurview[0]["cnvcPrimaryCodeField"].ToString() != "cnbVIPTag")
            {
                if (drDataItemPurview[0]["cniDataItemPurview"].ToString() != "2")
                {
                    MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //用户权限级别
            int iDataItemPurview = Convert.ToInt32(drDataItemPurview[0]["cniDataItemPurview"].ToString());
            //字段长度
            int iFieldLength = Convert.ToInt32(drDataItemPurview[0]["cniFieldLength"].ToString());
            //维护类型
            int iMainTainType = Convert.ToInt32(drDataItemPurview[0]["cniMaintenType"].ToString());
            //字段类型：1=文本；2=数字
            int iFieldType = Convert.ToInt32(drDataItemPurview[0]["cniFieldType"].ToString());
            //被双击单元格对应tbGuaranteeInfo表中的字段
            string strPrimaryCodeField = drDataItemPurview[0]["cnvcPrimaryCodeField"].ToString();

            //数据维护实体
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //变更操作实体
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //航班动态变更实体
            ChangeLegsBM changeLegsBM = new ChangeLegsBM();

            //字段类型
            maintenGuaranteeInforBM.FieldType = iFieldType;
            //变更前单元格的值
            maintenGuaranteeInforBM.OldContent = fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text;
            //列名
            maintenGuaranteeInforBM.ColumnCaption = drDataItemPurview[0]["cnvcDataItemName"].ToString();

            //航班动态信息
            DataRow drFlightInfo = dtDisplayDeskFlights.Rows[e.Row];

            string strDATOP = drFlightInfo[strFormType + "cncDATOP"].ToString();
            string strFLTID = drFlightInfo[strFormType + "cnvcFLTID"].ToString();
            string strLEGNO = drFlightInfo[strFormType + "cniLEGNO"].ToString();
            string strAC = drFlightInfo[strFormType + "cnvcAC"].ToString();

            //维护信息
            maintenGuaranteeInforBM.DATOP = strDATOP;
            maintenGuaranteeInforBM.FLTID = strFLTID;
            maintenGuaranteeInforBM.LEGNO = strLEGNO;
            maintenGuaranteeInforBM.AC = strAC;
            maintenGuaranteeInforBM.FieldName = strPrimaryCodeField;
            maintenGuaranteeInforBM.FieldLength = iFieldLength;

            //航班信息
            changeLegsBM.DATOP = strDATOP;
            changeLegsBM.FLTID = strFLTID;
            changeLegsBM.LEGNO = Convert.ToInt32(strLEGNO);
            changeLegsBM.AC = strAC;

            //变更信息
            changeRecordBM.UserID = accountBM.UserId;
            changeRecordBM.OldDATOP = strDATOP;
            changeRecordBM.OldFLTID = strFLTID;
            changeRecordBM.OldLegNo = Convert.ToInt32(strLEGNO);
            changeRecordBM.OldAC = strAC;
            changeRecordBM.NewDATOP = strDATOP;
            changeRecordBM.NewFLTID = strFLTID;
            changeRecordBM.NewLegNo = Convert.ToInt32(strLEGNO);
            changeRecordBM.NewAC = strAC;

            DataRow[] drDeskFlight = dtDeskFlights.Select("cncDATOP = '" + strDATOP + "' AND " +
                "cnvcFLTID = '" + strFLTID + "' AND " +
                "cniLEGNO = " + strLEGNO + " AND " +
                "cnvcAC = '" + strAC + "'");

            if (drDeskFlight.Length > 0)
            {
                changeRecordBM.OldDepSTN = drDeskFlight[0]["cncDEPSTN"].ToString();
                changeRecordBM.OldArrSTN = drDeskFlight[0]["cncARRSTN"].ToString();
                changeRecordBM.NewDepSTN = drDeskFlight[0]["cncDEPSTN"].ToString();
                changeRecordBM.NewArrSTN = drDeskFlight[0]["cncARRSTN"].ToString();
                changeRecordBM.STD = drDeskFlight[0]["cncSTD"].ToString();
                changeRecordBM.ETD = drDeskFlight[0]["cncETD"].ToString();
                changeRecordBM.STA = drDeskFlight[0]["cncSTA"].ToString();
                changeRecordBM.ETA = drDeskFlight[0]["cncETA"].ToString();
                changeRecordBM.ChangeReasonCode = strPrimaryCodeField;
                changeRecordBM.ChangeOldContent = fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text;
                changeRecordBM.ActionTag = "U";
                changeRecordBM.Refresh = 0;

                changeLegsBM.CKIFlightDate = drDeskFlight[0]["cncCKIFlightDate"].ToString();
                changeLegsBM.FlightDate = drDeskFlight[0]["cncFlightDate"].ToString();
                changeLegsBM.CKIFlightNo = drDeskFlight[0]["cnvcCKIFlightNo"].ToString();
                changeLegsBM.FlightNo = drDeskFlight[0]["cnvcFlightNo"].ToString();
                changeLegsBM.DEPSTN = drDeskFlight[0]["cncDEPSTN"].ToString();
                changeLegsBM.CityDEPSTN = drDeskFlight[0]["cncDEPCityThreeCode"].ToString();
                changeLegsBM.ARRSTN = drDeskFlight[0]["cncARRSTN"].ToString();
                changeLegsBM.CityARRSTN = drDeskFlight[0]["cncARRCityThreeCode"].ToString();
                changeLegsBM.LONG_REG = drDeskFlight[0]["cnvcLONG_REG"].ToString();
                changeLegsBM.DEPFourCode = drDeskFlight[0]["cncDEPAirportFourCode"].ToString();
                changeLegsBM.ARRFourCode = drDeskFlight[0]["cncARRAirportFourCode"].ToString();
                changeLegsBM.STD = drDeskFlight[0]["cncSTD"].ToString();
                changeLegsBM.ETD = drDeskFlight[0]["cncETD"].ToString();
                changeLegsBM.STA = drDeskFlight[0]["cncSTA"].ToString();
                changeLegsBM.ETA = drDeskFlight[0]["cncETA"].ToString();
                changeLegsBM.TOFF = drDeskFlight[0]["cncTOFF"].ToString();
                changeLegsBM.TDWN = drDeskFlight[0]["cncTDWN"].ToString();
                changeLegsBM.STATUS = drDeskFlight[0]["cncSTATUS"].ToString();
                changeLegsBM.ACTYP = drDeskFlight[0]["cncACTYP"].ToString();
            }
            else
            {
                return;
            }

            #region 根据字段类型分别处理
            //时间文本：维护类型=1
            if (iMainTainType == 1)
            {
                fmMaintenTime objfmMaintenTime = new fmMaintenTime(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenTime.ShowDialog() == DialogResult.OK)
                {
                    fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTime.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //单行文本：维护类型=2
            else if (iMainTainType == 2)
            {
                fmMaitenSingleText objfmMaitenSingleText = new fmMaitenSingleText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaitenSingleText.ShowDialog() == DialogResult.OK)
                {
                    fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaitenSingleText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //多行文本：维护类型=3
            else if (iMainTainType == 3)
            {
                fmMaintenMutiLineText objfmMaintenMutiLineText = new fmMaintenMutiLineText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenMutiLineText.ShowDialog() == DialogResult.OK)
                {
                    fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenMutiLineText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //下拉列表：维护类型=4
            else if (iMainTainType == 4)
            {
                //fmMaintenList objfmMaintenList = new fmMaintenList(maintenGuaranteeInforBM, changeRecordBM, m_stationBM);
                //if (objfmMaintenList.ShowDialog() == DialogResult.OK)
                //{
                //    fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenList.MMaintenGuaranteeInforBM.NewText;
                //}
            }
            else
            {
                #region 除以上4种类型以外的特殊类型
                //计算机飞行计划
                if (strPrimaryCodeField == "cniCFP")
                {
                    string strCFPId = drDeskFlight[0]["cniCFP"].ToString();
                    fmComputeFP obj = new fmComputeFP(maintenGuaranteeInforBM, changeLegsBM, strCFPId);
                    obj.ShowDialog();
                    if (obj.RefreshMainView == 1)
                    {
                        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = obj.PlanNo;

                        //如果发送了电报
                        thisForm.FlightRefresh();
                        for (int iLoop = 0; iLoop < thisForm.MdiParent.MdiChildren.Length; iLoop++)
                        {
                            if (thisForm.MdiParent.MdiChildren[iLoop] is fmFlightMoni)
                            {
                                (thisForm.MdiParent.MdiChildren[iLoop] as fmFlightMoni).FlightRefresh();
                            }
                        }
                    }
                }

                //飞机位置信息
                ArrayList alSpecialFields = new ArrayList();
                alSpecialFields.Add("cnvcACLat");
                alSpecialFields.Add("cnvcACLon");
                alSpecialFields.Add("cnvcNearestWP");
                alSpecialFields.Add("cniTimeToWP");
                alSpecialFields.Add("cniDisToWP");
                alSpecialFields.Add("cnvcFOBDiv");
                alSpecialFields.Add("cnvcFLDiv");
                alSpecialFields.Add("cncMegTime");
                if (alSpecialFields.Contains(strPrimaryCodeField))
                {
                    //if (fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text != "")
                    //{
                        fmACPositionInfo obj = new fmACPositionInfo(changeLegsBM);
                        obj.ShowDialog();
                    //}
                }
                alSpecialFields.Clear();

                //接收ACARS电报
                if (strPrimaryCodeField == "cnvcACARSInMegs")
                {
                    fmACARSMegsList obj = new fmACARSMegsList(changeLegsBM);
                    obj.ShowDialog();
                }

                //发送ACARS电报
                if (strPrimaryCodeField == "cnvcACARSOutMegs")
                {
                    fmSendACARSMegs obj = new fmSendACARSMegs(changeLegsBM);
                    obj.ShowDialog();
                }

                //业载信息
                alSpecialFields.Add("cnvcPsgs");
                alSpecialFields.Add("cnvcCargo");
                alSpecialFields.Add("cnvcBags");
                alSpecialFields.Add("cnvcTotalPayLoad");
                if (alSpecialFields.Contains(strPrimaryCodeField))
                {
                    fmPLDInfo obj = new fmPLDInfo(changeLegsBM);
                    if (obj.ShowDialog() == DialogResult.OK)
                    {
                        //int indexPsgs = dtDisplayDeskFlights.Columns[strFormType + "cnvcPsgs"].Ordinal - 4;
                        //int indexBags = dtDisplayDeskFlights.Columns[strFormType + "cnvcBags"].Ordinal - 4;
                        //int indexcnvcCargo = dtDisplayDeskFlights.Columns[strFormType + "cnvcCargo"].Ordinal - 4;
                        //int indexcnvcTotalPayLoad = dtDisplayDeskFlights.Columns[strFormType + "cnvcTotalPayLoad"].Ordinal - 4;

                        //fpsFlightDisp.ActiveSheet.Cells[e.Row, indexPsgs].Text = obj.PayLoadInfo.Psgs;
                        //fpsFlightDisp.ActiveSheet.Cells[e.Row, indexBags].Text = obj.PayLoadInfo.Bags;
                        //fpsFlightDisp.ActiveSheet.Cells[e.Row, indexcnvcCargo].Text = obj.PayLoadInfo.Cargo;
                        //fpsFlightDisp.ActiveSheet.Cells[e.Row, indexcnvcTotalPayLoad].Text = obj.PayLoadInfo.TotalPayLoad;
                    }
                }
                alSpecialFields.Clear();
                ////VIP标记
                //if (strPrimaryCodeField == "cnbVIPTag")
                //{
                //    fmMaintenVIP objfmMaintenVIP = new fmMaintenVIP(changeLegsBM, m_accountBM, iDataItemPurview);
                //    objfmMaintenVIP.ShowDialog();
                //}
                ////值机数据
                //else if (strPrimaryCodeField == "cniCheckNum" || strPrimaryCodeField == "cnvcBookNum" || strPrimaryCodeField == "cnvcInGATE")
                //{
                //    fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(changeLegsBM, changeRecordBM);
                //    if (objfmCheckPax.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmCheckPax.NewContent;
                //    }
                //}
                ////旅客名单
                //else if (strPrimaryCodeField == "cntPaxNameList")
                //{
                //    fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(changeLegsBM, changeRecordBM);
                //    if (objfmMaintenPaxNameList.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenPaxNameList.NewContent;
                //    }
                //}
                ////中转联程旅客
                //else if (strPrimaryCodeField == "cnbTransitPaxTag")
                //{
                //    fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(changeLegsBM, changeRecordBM);
                //    if (objfmMaintenTransitPax.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTransitPax.NewContent;
                //    }
                //}
                ////到达时间
                //else if (strPrimaryCodeField == "cncTDWN")
                //{
                //    changeRecordBM.Refresh = 1;
                //    fmMaintenTDWN objfmMaintenTDWN = new fmMaintenTDWN(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                //    if (objfmMaintenTDWN.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTDWN.NewContent;
                //    }
                //}
                ////起飞时间
                //else if (strPrimaryCodeField == "cncTOFF")
                //{
                //    changeRecordBM.Refresh = 1;
                //    fmMaintenTOFF objfmMaintenTOFF = new fmMaintenTOFF(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                //    if (objfmMaintenTOFF.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTOFF.NewContent;
                //    }
                //}
                ////总油量
                //else if (strPrimaryCodeField == "cniTotalFuelWeight")
                //{
                //    fmMaintenFuel objfmMaintenFuel = new fmMaintenFuel(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                //    if (objfmMaintenFuel.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenFuel.NewContent;
                //    }
                //}
                #endregion
            }
            #endregion
        }
        #endregion

        public virtual void FlightRefresh()
        { }
    }
}
