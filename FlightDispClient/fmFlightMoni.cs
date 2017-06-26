using System;
using System.Collections.Generic;
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
    public partial class fmFlightMoni : FlightDispInfo
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
            get { return fpsFlightMon; }
        }

        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="strFormType"></param>
        /// <param name="positionNameBM"></param>
        public fmFlightMoni(AccountBM accountBM, string strFormType, DataTable dtDeskAircraft, PositionNameBM deskNameBM)
        {
            InitializeComponent();

            //窗体类型
            this.FormType = strFormType;
            //用户信息
            this.AccountBM = accountBM;
            //席位名称
            this.DeskNameBM = deskNameBM;
            //显示的数据列
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(this.AccountBM, strFormType);
            this.DisplayDataItems = rvSF.Dt;
            //查询该席位所有飞机
            this.DeskAircrafts = dtDeskAircraft;
            //初始化席位航班动态
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
        }
        #endregion

        #region 载入窗体
        /// <summary>
        /// 载入窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmFlightMoni_Load(object sender, EventArgs e)
        {
            //设置视图
            fpsFlightMon.Sheets[0].RowHeader.Columns[0].Width = 50;
            timerChangeRecord.Interval = this.AccountBM.RefreshInterval * 1000;
            SpreadGrid spreadGrid = new SpreadGrid(this.AccountBM);
            spreadGrid.SetView(shFlightMon, this.DisplayDataItems, 0);

            BindMainView();

            //将用户的刷新频率写入配置文件
            int iTempInterval = this.AccountBM.RefreshInterval * 1000;
            TimerCallback timerDelegate = new TimerCallback(GetChangeDate);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);
        }
        #endregion

        private void timerChangeRecord_Tick(object sender, EventArgs e)
        {

        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {

        }

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

        #region 双击单元格事件
        private void fpsFlightMon_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (fpsFlightMon.ActiveSheet.Cells[e.Row, e.Column].BackColor == Color.DarkBlue)
            {
                fpsFlightMon.ActiveSheet.Cells[e.Row, e.Column].BackColor = m_cOldBackGroudColor;
                fpsFlightMon.ActiveSheet.Cells[e.Row, e.Column].ForeColor = m_cOldForeColor;
            }
            Maintennance(e, fpsFlightMon, this.DisplayDataItems, this.DeskFlights, this.DisplayDeskFlights, this.FormType, this.AccountBM, this);
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

        #region 绑定主视图
        public void BindMainView()
        {
            //生成显示表
            DataTable dtDeskFlights = this.DisplayDeskFlights;
            DataSet dsDeskFlights = new DataSet();
            dsDeskFlights.Tables.Add(dtDeskFlights);

            //绑定数据源
            fpsFlightMon.DataSource = dsDeskFlights;
            fpsFlightMon.DataMember = dsDeskFlights.Tables[0].TableName;

            //初始化表格颜色
            this.InitialGridColor(shFlightMon);
            //设置单元格颜色
            this.SetGridColor(this.DisplayDeskFlights, this.DeskFlights, this.DisplayDataItems, fpsFlightMon, this.FormType);
        }
        #endregion
    }
}