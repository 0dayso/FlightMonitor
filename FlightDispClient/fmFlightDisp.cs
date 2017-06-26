using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmFlightDisp : FlightDispInfo
    {
        //多线程定时器，必须生命成类变量，以免被垃圾回收
        private System.Threading.Timer timer;
        private int iRefreshInterval = 20;
        private object oMutexChangeRecords = new object();

        //闪烁前单元格颜色
        Color[,] colorArrOldBackGround;
        private Color m_cOldBackGroudColor;
        private Color m_cOldForeColor;

        /// <summary>
        /// 数据表显示控件
        /// </summary>
        public FarPoint.Win.Spread.FpSpread FpFlightInfo
        {
            get { return fpsFlightDisp; }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountBM">用户信息</param>
        /// <param name="dataItems">数据项</param>
        /// <param name="positionNameBM">席位信息</param>
        public fmFlightDisp(AccountBM accountBM, string strFormType, DataTable dtDeskAircraft, PositionNameBM deskNameBM)
        {
            InitializeComponent();

            //用户信息
            this.AccountBM = accountBM;
            //窗体类型
            this.FormType = strFormType;
            //席位名称
            this.DeskNameBM = deskNameBM;
            //显示的数据列
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(this.AccountBM, strFormType);
            this.DisplayDataItems = rvSF.Dt;
            //该席位所有飞机
            this.DeskAircrafts = dtDeskAircraft;
            //初始化席位航班动态
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
        }
        #endregion

        #region 窗体载入
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmFlightDisp_Load(object sender, EventArgs e)
        {
            //设置视图
            fpsFlightDisp.Sheets[0].RowHeader.Columns[0].Width = 50;
            timerChangeRecord.Interval = this.AccountBM.RefreshInterval * 1000;
            SpreadGrid spreadGrid = new SpreadGrid(this.AccountBM);
            spreadGrid.SetView(shFlightDisp, this.DisplayDataItems, 0);

            BindMainView();

            //将用户的刷新频率写入配置文件
            int iTempInterval = this.AccountBM.RefreshInterval * 1000;
            timerChangeRecord.Interval = iTempInterval;
            TimerCallback timerDelegate = new TimerCallback(GetChangeDate);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);
        }
        #endregion

        #region 获取变更数据（多线程定时调用）
        public void GetChangeDate(object state)
        {
            //调用业务外观层方法
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            lock (this)
            {
                //如果用户设置不刷新，则退出方法
                if (this.AccountBM.RefreshInterval == 0)
                    return;

                try
                {
                    //锁定互斥对象
                    Monitor.Enter(oMutexChangeRecords);
                    //获取最后一批变更数据
                    ReturnValueSF rvSF = changeRecordBF.GetLastWatchChangeRecords(this.LastRecordNo, GetDateTimeBM(1), this.DeskNameBM);
                    if (rvSF.Result > 0)
                    {
                        if (rvSF.Dt.Rows.Count > 0)
                        {
                            if (this.ChangeRecordTable.Columns.Count == 0)
                                this.ChangeRecordTable = rvSF.Dt.Clone();

                            //将查询到的变更数据写入到变更信息表中
                            foreach (DataRow dataRow in rvSF.Dt.Rows)
                            {
                                this.ChangeRecordTable.Rows.Add(dataRow.ItemArray);
                                //更改最大变更序号
                                this.LastRecordNo = Convert.ToInt32(dataRow["cniRecordNo"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string strError = ex.Message;
                }
                finally
                {
                    //释放互斥对象
                    Monitor.Exit(oMutexChangeRecords);
                }
            }
        }
        #endregion

        #region 定时处理航班变更
        private void timerChangeRecord_Tick(object sender, EventArgs e)
        {
            int iRefresh = 0;

            try
            {
                Monitor.Enter(oMutexChangeRecords);

                //是否需要刷新视图
                DataRow[] drChangeRecords = this.ChangeRecordTable.Select("cniRefresh = 1");

                #region 需要重新组织视图
                //需要重新组织视图
                if (drChangeRecords.Length > 0)
                {
                    iRefresh = 1;
                    timerSplash.Enabled = false;
                    DataTable dtChangeLegs = new DataTable();

                    //逐行处理变更记录
                    foreach (DataRow drFlightChange in this.ChangeRecordTable.Rows)
                    {
                        //本次变更实体对象
                        ChangeLegsBM oldChangeLegsBM = new ChangeLegsBM();
                        //根据变更前的数据生成变更实体
                        //对关键字赋值
                        oldChangeLegsBM.DATOP = drFlightChange["cncOldDATOP"].ToString();
                        oldChangeLegsBM.FLTID = drFlightChange["cnvcOldFLTID"].ToString();
                        oldChangeLegsBM.LEGNO = Convert.ToInt32(drFlightChange["cniOldLEGNO"].ToString());
                        oldChangeLegsBM.AC = drFlightChange["cnvcOldAC"].ToString();
                        //根据变更后的数据生成变更实体
                        ChangeLegsBM newChangeLegsBM = new ChangeLegsBM();
                        //对关键字进行赋值
                        if (drFlightChange["cncNewDATOP"].ToString() != "")
                        {
                            newChangeLegsBM.DATOP = drFlightChange["cncNewDATOP"].ToString();
                            newChangeLegsBM.FLTID = drFlightChange["cnvcNewFLTID"].ToString();
                            newChangeLegsBM.LEGNO = Convert.ToInt32(drFlightChange["cniNewLEGNO"].ToString());
                            newChangeLegsBM.AC = drFlightChange["cnvcNewAC"].ToString();
                        }
                        else
                        {
                            newChangeLegsBM = oldChangeLegsBM;
                        }
                        //根据变更后的航班信息查询航班的保障信息
                        FlightDispBF flightDispBF = new FlightDispBF();
                        dtChangeLegs = flightDispBF.GetFlightByKey(newChangeLegsBM).Dt;

                        //生成查询语句
                        string strSearch = "cncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "' OR " +
                            "cncDATOP = '" + drFlightChange["cncNewDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + drFlightChange["cnvcNewFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + drFlightChange["cniNewLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + drFlightChange["cnvcNewAC"].ToString() + "'";

                        //根据本次变更从内存表中查询航班动态和闪烁动态
                        DataRow[] drDeskFlights = this.DeskFlights.Select(strSearch);
                        DataRow[] drSplashTag = this.SplashTagTable.Select(strSearch);

                        //如果获取到相关航班信息And闪烁表中没有该航班的闪烁动态记录
                        if (drDeskFlights.Length > 0 && drSplashTag.Length <= 0)
                        {
                            //生成一条信息的闪烁动态记录
                            DataRow drTempSplashTag = this.SplashTagTable.NewRow();
                            drTempSplashTag["cncDATOP"] = drDeskFlights[0]["cncDATOP"].ToString();
                            drTempSplashTag["cnvcFLTID"] = drDeskFlights[0]["cnvcFLTID"].ToString();
                            drTempSplashTag["cniLEGNO"] = drDeskFlights[0]["cniLEGNO"].ToString();
                            drTempSplashTag["cnvcAC"] = drDeskFlights[0]["cnvcAC"].ToString();
                            this.SplashTagTable.Rows.Add(drTempSplashTag);

                            //将该条记录加入查询结果中
                            drSplashTag = this.SplashTagTable.Select(strSearch);
                        }

                        #region 航站航班动态有相应的记录
                        //航站航班动态有相应的记录
                        if (drDeskFlights.Length > 0)
                        {
                            //如果变更不是删除航班AND根据变更后的航班关键字查询航班保障信息结果不为空
                            if (drFlightChange["cncActionTag"].ToString() != "D" && dtChangeLegs.Rows.Count > 0)
                            {
                                //用变更后的数据更新该航班的保障信息
                                drDeskFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;

                                //设置闪烁时间
                                DataRow dr = null;
                                SetSplash(drSplashTag, drFlightChange, dtChangeLegs, dr, 2);
                            }
                            //如果变更是删除航班OR根据变更后的航班关键字查询航班保障信息结果为空
                            else if (drFlightChange["cncActionTag"].ToString() == "D" || dtChangeLegs.Rows.Count == 0)
                            {
                                //从航班动态表和闪烁动态表中将相关记录删除
                                this.DeskFlights.Rows.Remove(drDeskFlights[0]);
                                this.SplashTagTable.Rows.Remove(drSplashTag[0]);
                            }

                            //重新设置排序
                            this.DeskFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                            this.DeskFlights = this.DeskFlights.DefaultView.Table;

                            //显示所有航班                    
                            this.DisplayDeskFlights = FillFlightInfo(this.DeskAircrafts, this.DeskFlights, "Dsp", this.DisplayDataItems);

                            //重新绑定
                            fpsFlightDisp.Sheets[0].DataSource = this.DisplayDeskFlights;
                        }
                        #endregion

                        #region 航站航班动态无相应的记录（新增航班）
                        //航站航班动态无相应的记录
                        else
                        {
                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //插入一行
                                DataRow drNewFlight = this.DeskFlights.NewRow();
                                //将变更后的数据插入航班信息表中
                                drNewFlight.ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                this.DeskFlights.Rows.Add(drNewFlight);

                                //重新排序
                                this.DeskFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                                this.DeskFlights = this.DeskFlights.DefaultView.Table;

                                //显示所有航班                    
                                this.DisplayDeskFlights = FillFlightInfo(this.DeskAircrafts, this.DeskFlights, "Dsp", this.DisplayDataItems);
                                //重新绑定
                                fpsFlightDisp.Sheets[0].DataSource = this.DisplayDeskFlights;
                                //设置闪烁
                                SetSplash(drSplashTag, drFlightChange, dtChangeLegs, drNewFlight, 1);
                            }
                        }
                        #endregion
                    }

                    colorArrOldBackGround = new Color[fpsFlightDisp.Sheets[1].Rows.Count, fpsFlightDisp.Sheets[1].Columns.Count];

                    //初始化表格颜色
                    InitialGridColor(shFlightDisp);

                    ////设置表格颜色
                    //SetGridColor(1, m_dtTodayStationFlights);

                    timerSplash.Enabled = true;
                }
                #endregion

                #region 不需要重新组织视图
                //不需要重新组织视图
                else
                {
                    DataTable dtChangeLegs = new DataTable();

                    //逐条处理变更记录
                    foreach (DataRow drFlightChange in this.ChangeRecordTable.Rows)
                    {
                        //本次变更实体对象
                        ChangeLegsBM oldChangeLegsBM = new ChangeLegsBM();
                        //根据变更前的数据生成变更实体
                        //对关键字赋值
                        oldChangeLegsBM.DATOP = drFlightChange["cncOldDATOP"].ToString();
                        oldChangeLegsBM.FLTID = drFlightChange["cnvcOldFLTID"].ToString();
                        oldChangeLegsBM.LEGNO = Convert.ToInt32(drFlightChange["cniOldLEGNO"].ToString());
                        oldChangeLegsBM.AC = drFlightChange["cnvcOldAC"].ToString();
                        //根据变更后的数据生成变更实体
                        ChangeLegsBM newChangeLegsBM = new ChangeLegsBM();
                        //对关键字进行赋值
                        if (drFlightChange["cncNewDATOP"].ToString() != "")
                        {
                            newChangeLegsBM.DATOP = drFlightChange["cncNewDATOP"].ToString();
                            newChangeLegsBM.FLTID = drFlightChange["cnvcNewFLTID"].ToString();
                            newChangeLegsBM.LEGNO = Convert.ToInt32(drFlightChange["cniNewLEGNO"].ToString());
                            newChangeLegsBM.AC = drFlightChange["cnvcNewAC"].ToString();
                        }
                        else
                        {
                            newChangeLegsBM = oldChangeLegsBM;
                        }
                        //根据变更后的航班信息查询航班的保障信息
                        FlightDispBF flightDispBF = new FlightDispBF();
                        dtChangeLegs = flightDispBF.GetFlightByKey(newChangeLegsBM).Dt;

                        //查询语句
                        string strSearch = "cncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "'";

                        //查询涉及变更的航班和闪烁信息
                        DataRow[] drDeskFlights = this.DeskFlights.Select(strSearch);
                        DataRow[] drSplashTag = this.SplashTagTable.Select(strSearch);

                        //根据变更前的航班信息查询航班的保障信息
                        dtChangeLegs = flightDispBF.GetFlightByKey(newChangeLegsBM).Dt;

                        //有相应的记录
                        if (drDeskFlights.Length > 0)
                        {
                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //更新航班信息
                                drDeskFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                //重新设置显示
                                //SetInOutFlightDataRowValue(drDeskFlights[0]);

                                //设置闪烁
                                DataRow dr = null;
                                SetSplash(drSplashTag, drFlightChange, dtChangeLegs, dr, 0);
                            }
                        }
                    }
                }
                #endregion

                //将变更记录添加到变更显示表中
                if (this.ChangeRecordTable.Rows.Count != 0)
                {
                    //AddChangeDataToList(this.ChangeRecordTable, 0);
                }

                if (iRefresh == 0)
                {
                    ////计算相应的提示信息
                    //ComputeFlightsInfor(m_dtTodayStationFlights);

                    ////设置表格颜色
                    //SetGridColor(1, m_dtTodayStationFlights);
                }

                //清空变更记录
                this.ChangeRecordTable.Rows.Clear();
            }
            catch(Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                Monitor.Exit(oMutexChangeRecords);
            }
        }
        #endregion

        #region 设置闪烁时间
        /// <summary>
        /// 根据航班变更设置闪烁时间
        /// </summary>
        /// <param name="drFlightChange">航班变更记录</param>
        /// <param name="drSplashTag">闪烁标记记录</param>
        /// <param name="iChangeMode">航班变更模式</param>
        /// iChangeMode = 0：不需要刷新视图
        /// iChangeMode = 1：需要刷新视图，新增航班
        /// iChangeMode = 2：需要刷新视图，航班记录主键变更
        private void SetSplash(DataRow[] drSplashTag, DataRow drFlightChange, DataTable dtChangeLegs, DataRow drNewFlight, int iChangeMode)
        {
            int iSplash = 0;

            //查询变更的航班是否在显示的席位航班动态中
            string strSearch = "DspcncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
                "DspcnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
                "DspcniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
                "DspcnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "'";
            DataRow[] drSplashFlights = this.DisplayDeskFlights.Select(strSearch);

            //如果变更的航班显示在席位航班动态中
            if (drSplashFlights.Length > 0)
            {
                //查询涉及变更的数据项是否显示
                strSearch = "cnvcDataItemID = 'Dsp" + drFlightChange["cnvcChangeReasonCode"].ToString() + "'";
                DataRow[] drChangeDataItem = this.DisplayDataItems.Select(strSearch);
                //如果存在，则闪烁标记设为1
                if (drChangeDataItem.Length > 0)
                {
                    iSplash = 1;
                }
            }

            //判断涉及变更的数据项是否设置了闪烁
            strSearch = "cnvcPrimaryCodeField = '" + drFlightChange["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
            DataRow[] drDataItemSplash = this.DisplayDataItems.Select(strSearch);
            //变更的数据项的名称
            string strChangedField = drFlightChange["cnvcChangeReasonCode"].ToString();

            //如果需要闪烁提示
            if (drDataItemSplash.Length > 0)
            {
                switch (iChangeMode)
                {
                    case 0:
                        //设置闪烁时间
                        if (iSplash == 1)
                        {
                            drSplashTag[0][strChangedField] = this.AccountBM.SplashSeconds;
                        }
                        break;
                    case 1:
                        if (iSplash == 1)
                        {
                            //增加闪烁记录
                            DataRow drSplash = this.SplashTagTable.NewRow();
                            drSplash["cncDATOP"] = drNewFlight["cncDATOP"].ToString();
                            drSplash["cnvcFLTID"] = drNewFlight["cnvcFLTID"].ToString();
                            drSplash["cniLEGNO"] = drNewFlight["cniLEGNO"].ToString();
                            drSplash["cnvcAC"] = drNewFlight["cnvcAC"].ToString();
                            drSplash["cnvcLONG_REG"] = this.AccountBM.SplashSeconds.ToString();
                            //将新纪录添加到航班信息表和闪烁表中
                            this.SplashTagTable.Rows.Add(drSplash);
                        }
                        break;
                    case 2:
                        //如果变更的数据项为ETA或者ETD
                        if (strChangedField == "cncETA" || strChangedField == "cncETD")
                        {
                            //如果延误原因不为空AND闪烁标记=1
                            //即如果航班没有延误，ETA和ETD变化不闪烁
                            if (dtChangeLegs.Rows[0]["cnvcDELAY1"].ToString() != "" && iSplash == 1)
                            {
                                //设置闪烁时间
                                drSplashTag[0][strChangedField] = this.AccountBM.SplashSeconds.ToString();
                            }
                        }
                        //如果闪烁标记=1
                        else if (iSplash == 1)
                        {
                            //如果变更的数据项为航班状态
                            if (strChangedField == "cncSTATUS")
                            {
                                //如果航班状态变为DEP
                                if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEP")
                                {
                                    //航班起飞时间和落地时间单元格也闪烁
                                    drSplashTag[0]["cncTDWN"] = this.AccountBM.SplashSeconds;
                                    drSplashTag[0]["cncTOFF"] = this.AccountBM.SplashSeconds;
                                }

                                //如果航班状态变更为DEL
                                if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEL")
                                {
                                    //航班的预计起飞时间和预达时间也闪烁
                                    drSplashTag[0]["cncETD"] = this.AccountBM.SplashSeconds;
                                    drSplashTag[0]["cncETA"] = this.AccountBM.SplashSeconds;
                                }
                            }
                        }
                        //对于其他数据项，设置闪烁时间
                        drSplashTag[0][strChangedField] = this.AccountBM.SplashSeconds;
                        break;
                    default:
                        break;
                }//end switch
            }//end if (drDataItemSplash.Length > 0)
        }
        #endregion

        #region 定时闪烁
        private void timerSplash_Tick(object sender, EventArgs e)
        {
            Splash(this.DisplayDataItems, this.SplashTagTable, this.DisplayDeskFlights, fpsFlightDisp, m_cOldBackGroudColor);
        }
        #endregion

        #region 双击单元格，查看或维护数据
        private void fpsFlightDisp_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].BackColor == Color.DarkBlue)
            {
                fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].BackColor = m_cOldBackGroudColor;
                fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].ForeColor = m_cOldForeColor;
            }
            Maintennance(e, fpsFlightDisp, this.DisplayDataItems, this.DeskFlights, this.DisplayDeskFlights, this.FormType, this.AccountBM, this);
        }
       #endregion

        #region 重新设置视图
        /// <summary>
        /// 重新设置视图
        /// </summary>
        public void ViewRefresh(DataTable dtDisplayDataItems)
        {
            timerSplash.Enabled = false;

            //显示的数据列
            this.DisplayDataItems = dtDisplayDataItems;
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
            BindMainView();
            timerSplash.Enabled = true;
        }
        #endregion

        #region 切换席位
        public void DeskChange(DataTable dtDeskAircrafts)
        {
            timerSplash.Enabled = false;
            this.DeskAircrafts = dtDeskAircrafts;
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
            BindMainView();
            timerSplash.Enabled = true;
        }
        #endregion

        #region 刷新航班动态
        /// <summary>
        /// 刷新航班动态
        /// </summary>
        public override void FlightRefresh()
        {
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
            BindMainView();
        }
        #endregion

        #region 绑定主视图
        public void BindMainView()
        {
            //生成显示表
            DataTable dtDeskFlights = this.DisplayDeskFlights;
            DataSet dsDeskFlights = new DataSet();
            dsDeskFlights.Tables.Add(dtDeskFlights);

            //绑定数据源
            fpsFlightDisp.DataSource = dsDeskFlights;
            fpsFlightDisp.DataMember = dsDeskFlights.Tables[0].TableName;

            //初始化表格颜色
            this.InitialGridColor(shFlightDisp);
            //设置单元格颜色
            this.SetGridColor(this.DisplayDeskFlights, this.DeskFlights, this.DisplayDataItems, fpsFlightDisp, this.FormType);
        }
        #endregion
    }
}