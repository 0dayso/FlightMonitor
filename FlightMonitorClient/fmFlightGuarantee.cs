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
using LinYong.PublicService;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.AgentServiceBM;
using System.Configuration;


namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmFlightGuarantee : Form
    {
        #region 定义变量
        private Color colorFavorate;

        private AccountBM m_accountBM;                              //登陆用户实体对象
        private DataTable m_dtDataItemPurview;                      //登陆用户的数据项权限
        private DataTable m_dtStandardIntermissionTime;             //标准过站时间表
        private int m_iLastRecordNo;                                //最大变更序号
        private DataTable m_dtDataItems;                            //用户设置出的数据项
        private string m_strDataItemSearch;                         //根据用户设置出的数据项查询要闪烁的记录
        private DataTable m_dtStations;                             //基地列表
        private StationBM m_stationBM;                              //用户选择的航站实体对象     
        private DataTable m_dtFlightDelayReason;                    //航班延误原因代码表
        private DataTable m_dtDiversionDelayReason;                 //备降返航代码表


        private DataTable m_dtYesterdayStationFlights;              //航站昨天所有的进出港航班
        private DataTable m_dtTodayStationFlights;                  //航站当天所有的进出港航班
        private DataTable m_dtTomorrowStationFlights;               //航站明天所有的进出港航班
        private DataTable m_dtSelectDateStationFlights;             //航站明天所有的进出港航班

        private DataTable m_dtInOutFlightsSchema;                   //按进出港格式显示的航班动态的表结构

        private IList m_ilYesterdayInOutFlights;                    //按进出港格式显示的昨天天航班动态的表格
        private IList m_ilTodayInOutFlights;                        //按进出港格式显示的今天航班动态的表格
        private DataTable m_dtTodayInOutFlights;                    //按进出港格式显示的今天航班动态的表格
        private IList m_ilTomorrowInOutFlights;                     //按进出港格式显示的明天航班动态的表格
        private IList m_ilSelectDateInOutFlights;                   //按进出港格式显示的所选日期航班动态的表格


        #region yanxian 2013-10-15 航班旅客数据
        private TravellerBF traveller = new TravellerBF();
        private DataRow[] t_inVipTraveller; //进港要客数据
        private DataRow[] t_inSsTraveller;//进港特殊旅客
        private DataRow[] t_inMealTraveller;//进港特餐旅客
        private DataRow[] t_inJINTraveller;//进港金鹏旅客
        private DataRow[] t_inTSTTraveller;//进港中转旅客
        private DataRow[] t_inOTHTTraveller;//进港普通旅客
        private DataTable t_inALLTraveller;//进港航班所有旅客

        private DataRow[] t_outVipTraveller; //出港要客数据
        private DataRow[] t_outSsTraveller;//出港特殊旅客
        private DataRow[] t_outMealTraveller;//出港特餐旅客
        private DataRow[] t_outJINTraveller;//出港金鹏旅客
        private DataRow[] t_outTSTTraveller;//出港中转旅客
        private DataRow[] t_outOTHTTraveller;//出港普通旅客
        private DataTable t_outALLTraveller;//出港航班所有旅客

        private DateTime lastSearchDateTime;//上次查询同一航班旅客数据时刻
        private ChangeLegsBM lastInChangeLegsBM = new ChangeLegsBM();//上一次查询进港航班实体
        private ChangeLegsBM lastOutChangeLegsBM = new ChangeLegsBM();//上一次查询出港航班实体
        private int lastSelectedRow = -1;//上一次查询所选中的行
        #endregion

        //闪烁前单元格颜色
        Color[,] colorArrOldBackGround;
        //int[,] iFirstEnterSplash;

        private DataTable m_dtSplashTag;  //航班闪烁标志

        //变更列表颜色计数器
        private int m_iGreenRecordNum = 0;
        private int m_iRedRecordNum;

        //记录选中的行和列以及原颜色
        private int m_iOldSelectedRow = -1;
        private int m_iOldSelectedColumn = -1;
        private Color m_cOldBackGroudColor, m_cOldForeColor;

        //右键菜单所选择的行
        private int m_iRightButtonRow = 0;

        //所选择的出港航班的信息
        //private GuaranteeInforBM selectedGuaranteeInfo = null;
        private ChangeLegsBM inChangeLegsBM = new ChangeLegsBM();
        private ChangeLegsBM outChangeLegsBM = new ChangeLegsBM();

        private StatusBar m_statusBar;

        //多线程定时器，必须生命成类变量，以免被垃圾回收
        private System.Threading.Timer timer;
        private int iRefreshInterval = 20;
        private object oMutexChangeRecords = new object();
        private DataTable m_dtChangeTable = new DataTable();   //变化列表
        private int m_iAutoAdjust = 0;
        #endregion

        //航前任务书对象，包含所有航班 -- modified by LinYong in 2013.10.16
        static public DataTable _dataTableTaskBooks = null;

        #region 定义属性
        /// <summary>
        /// 数据项
        /// </summary>
        public DataTable DataItems
        {
            get { return m_dtDataItems; }
            set
            {
                m_dtDataItems = value;
                m_strDataItemSearch = "";
                foreach (DataRow dataRow in m_dtDataItems.Rows)
                {
                    m_strDataItemSearch += dataRow["cnvcPrimaryCodeField"].ToString() + ">0 OR ";
                }
                if (m_strDataItemSearch != null && m_strDataItemSearch != "")
                {
                    m_strDataItemSearch = m_strDataItemSearch.Substring(0, m_strDataItemSearch.Length - 3);
                }
            }
        }

        /// <summary>
        /// 数据表显示控件
        /// </summary>
        public FarPoint.Win.Spread.FpSpread FpFlightInfo
        {
            get { return fpFlightInfo; }
        }
        #endregion

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountBM">登陆用户实体对象</param>
        /// <param name="dtIntermissionTime">标准过站时间表</param>
        /// <param name="iLastRecordNo">最大变更序号</param>
        public fmFlightGuarantee(AccountBM accountBM, DataTable dtIntermissionTime, DataTable dataItems, StatusBar statusBar, int iAutoAdjust)
        {
            InitializeComponent();

            this.m_accountBM = accountBM;
            this.m_dtStandardIntermissionTime = dtIntermissionTime;
            this.m_dtDataItems = dataItems;
            foreach (DataRow dataRow in m_dtDataItems.Rows)
            {
                m_strDataItemSearch += dataRow["cnvcPrimaryCodeField"].ToString() + ">0 OR ";
            }

            if (m_strDataItemSearch != null && m_strDataItemSearch != "")
            {
                m_strDataItemSearch = m_strDataItemSearch.Substring(0, m_strDataItemSearch.Length - 3);
            }

            this.m_statusBar = statusBar;

            //自定义的背景色
            //colorFavorate = Color.FromArgb(182, 222, 187);      
            colorFavorate = Color.White;

            m_iAutoAdjust = iAutoAdjust;
        }
        #endregion

        #region 载入窗体
        private void fmFlightGuarantee_Load(object sender, EventArgs e)
        {
            //设置获取变更数据的间隔
            timerChange.Interval = m_accountBM.RefreshInterval * 1000;

            //设置视图
            SpreadGrid spreadGrid = new SpreadGrid(m_accountBM);

            spreadGrid.SetView(shYestoday, m_dtDataItems, 0);
            spreadGrid.SetView(shToday, m_dtDataItems, 0);
            spreadGrid.SetView(shTomorrow, m_dtDataItems, 0);
            spreadGrid.SetView(shSelectDate, m_dtDataItems, 0);

            //获取用户的数据项权限
            GetUserDataItemPurview();

            //设置显示的航站
            GetStations();

            //获取航班延误和备降原因
            GetFlightDelayReason();
            GetDiversionDelayReason();

            //获取最大变更序号和最后100条变更数据
            GetMaxRecordNo();

            //获取该航站当天的所有航班
            m_dtTodayStationFlights = GetStationFlights(GetDateTimeBM(1), m_stationBM, 1);

            //计算相应的提示信息
            ComputeFlightsInfor(m_dtTodayStationFlights);

            //进出港航班表格Schema
            m_dtInOutFlightsSchema = GetDisplaySchema();

            //获取当天进出港航班
            m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

            //绑定进出港航班信息
            fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;
            fpFlightInfo.Sheets[3].DataSource = m_ilTodayInOutFlights;

            //获取变更记录
            GetChangeData();

            //记录闪烁单元格背景色和是否第一次进入闪烁
            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];

            //初始化表格颜色
            InitialGridColor(shToday);

            //设置当天单元格颜色
            SetGridColor(1, m_dtTodayStationFlights);

            fpFlightInfo.ActiveSheetIndex = 1;
            fpFlightInfo.Sheets[3].SheetName = dtFlightDate.Value.ToString("yyyy-MM-dd");

            //进出港航班数
            SetInOutFlightsNum();

            //modified by LinYong in 20140707
            UpDownValue_ValueChanged(sender, e);

            //将用户的刷新频率写入配置文件
            int iTempInterval = m_accountBM.RefreshInterval * 1000;
            TimerCallback timerDelegate = new TimerCallback(GetChangeDate);
            //timer = new System.Threading.Timer(timerDelegate, null, 0, (iRefreshInterval * 1000));
            timer = new System.Threading.Timer(timerDelegate, null, 0, iTempInterval);

        }
        #endregion

        #region 获取某天时间范围
        /// <summary>
        /// 获取某天时间范围
        /// </summary>
        /// <param name="iDay">哪一天：0=昨天；1=今天；2=明天；3=选择的日期</param>
        private DateTimeBM GetDateTimeBM(int iDay)
        {
            DateTimeBM dataTimeBM = new DateTimeBM();

            string strStartDateTime;
            string strEndDateTime;

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
            else   //所选日期
            {
                strStartDateTime = dtFlightDate.Value.Date.ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = dtFlightDate.Value.Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            }

            dataTimeBM.StartDateTime = strStartDateTime;
            dataTimeBM.EndDateTime = strEndDateTime;

            return dataTimeBM;
        }
        #endregion

        #region 获取航站列表并设置显示的航站
        /// <summary>
        /// 获取航站列表并设置显示的航站
        /// </summary>
        private void GetStations()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            StationBF stationBF = new StationBF();

            rvSF = stationBF.GetAllStation();
            if (rvSF.Result > 0)
            {
                m_dtStations = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //绑定场站列表
            cmbStations.DataSource = m_dtStations;
            cmbStations.ValueMember = "cncThreeCode";
            cmbStations.DisplayMember = "cnvcAirportName";
            cmbStations.SelectedValue = m_accountBM.StationThreeCode;

            //选择的航站实体对象
            DataRow[] drStation = m_dtStations.Select("cncThreeCode = '" + m_accountBM.StationThreeCode + "'");
            if (drStation.Length > 0)
            {
                m_stationBM = new StationBM(drStation[0]);
            }
            else
            {
                m_stationBM = new StationBM();
            }
        }
        #endregion

        #region 获取航班延误原因代码表
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
        #endregion

        #region 获取备降返航代码表
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
        #endregion

        #region 获取登陆用户的数据项权限（权限、提示）
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
        #endregion

        #region 获取当天航站的所有航班
        /// <summary>
        /// 获取当天航站的所有航班
        /// </summary>
        /// <param name="dateTimeBM">日期范围</param>
        /// <param name="stationBM">航站实体</param>
        /// <param name="iToday">获取哪天的航班：0=昨天；1=今天；2=明天；3=选择的日期</param>
        private DataTable GetStationFlights(DateTimeBM dateTimeBM, StationBM stationBM, int iToday)
        {
            //航站所有航班
            DataTable dtStationFlights = new DataTable();
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();

            #region modified by LinYong -- 20091117
            ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM);
            //ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, m_accountBM);
            #endregion

            if (rvSF.Result > 0)
            {

                //航站航班信息表
                dtStationFlights = rvSF.Dt;

                #region 测试根据选择的航空公司过滤航班 
                string strCompanys = SysMsgBM.Companys;
                dtStationFlights = rvSF.Dt.Clone();
                foreach (DataRow dataRowrvSFDtRows in rvSF.Dt.Rows)
                {
                    string strCompany = dataRowrvSFDtRows["cnvcFLTID"].ToString().Substring(0, 3).Trim();
                    if (strCompanys.IndexOf(";" + strCompany + ";") >= 0)
                    {
                        dtStationFlights.ImportRow(dataRowrvSFDtRows);
                    }
                }

                //
                SysMsgBM.OldCompanys = SysMsgBM.Companys;

                //隐藏 改变公司选择列表 的提示信息
                //label44.Visible = false;
                button2.Visible = false;
                #endregion 测试根据选择的航空公司过滤航班 


                //如果查找今天的航班信息
                //则生成闪烁表示表
                if (iToday == 1)
                {
                    //闪烁标识表
                    m_dtSplashTag = new DataTable();

                    //生成闪烁表的结构
                    //闪烁表字段与vw_legs视图字段相同
                    for (int iLoop = 0; iLoop < dtStationFlights.Columns.Count; iLoop++)
                    {
                        if (dtStationFlights.Columns[iLoop].ColumnName == "cncDATOP" || dtStationFlights.Columns[iLoop].ColumnName == "cnvcFLTID" || dtStationFlights.Columns[iLoop].ColumnName == "cnvcAC")
                        {
                            m_dtSplashTag.Columns.Add(dtStationFlights.Columns[iLoop].ColumnName, System.Type.GetType("System.String"));
                        }
                        else
                        {
                            m_dtSplashTag.Columns.Add(dtStationFlights.Columns[iLoop].ColumnName, System.Type.GetType("System.Int32"));
                        }
                    }

                    //闪烁标识表的主键
                    DataColumn[] pk = new DataColumn[4];
                    pk[0] = m_dtSplashTag.Columns["cncDATOP"];
                    pk[1] = m_dtSplashTag.Columns["cnvcFLTID"];
                    pk[2] = m_dtSplashTag.Columns["cniLEGNO"];
                    pk[3] = m_dtSplashTag.Columns["cnvcAC"];
                    m_dtSplashTag.PrimaryKey = pk;

                    m_dtSplashTag.Rows.Clear();

                    //向闪烁标识表中添加记录
                    //仅添加主键字段的值
                    foreach (DataRow dataRow in dtStationFlights.Rows)
                    {
                        DataRow dataRowSplash = m_dtSplashTag.NewRow();
                        dataRowSplash["cncDATOP"] = dataRow["cncDATOP"].ToString();
                        dataRowSplash["cnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                        dataRowSplash["cniLEGNO"] = dataRow["cniLEGNO"].ToString();
                        dataRowSplash["cnvcAC"] = dataRow["cnvcAC"].ToString();

                        m_dtSplashTag.Rows.Add(dataRowSplash);
                    }
                }
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return dtStationFlights;
        }
        #endregion

        #region 计算过站时间、开关舱时间、
        /// <summary>
        /// 计算过站时间、开关舱时间、
        /// </summary>
        private void ComputeFlightsInfor(DataTable dtStationFlights)
        {
            int iRowIndex = 0;
            foreach (DataRow dataRow in dtStationFlights.Rows)
            {
                //计算开关舱时间
                if (dataRow["cncSTATUS"].ToString() != "CNL" && dataRow["cncOpenCabinTime"].ToString().Trim() != "" && dataRow["cncClosePaxCabinTime"].ToString().Trim() != "")
                {
                    int iOpenPaxCabinTime = 0;
                    TimeSpan tsOpenCabinTime = new TimeSpan(0, Convert.ToInt32(dataRow["cncOpenCabinTime"].ToString().Substring(0, 2)), Convert.ToInt32(dataRow["cncOpenCabinTime"].ToString().Substring(2, 2)));
                    TimeSpan tsClosePaxCabinTime = new TimeSpan(0, Convert.ToInt32(dataRow["cncOpenCabinTime"].ToString().Substring(0, 2)), Convert.ToInt32(dataRow["cncOpenCabinTime"].ToString().Substring(2, 2)));
                    if (tsClosePaxCabinTime.Hours - tsOpenCabinTime.Hours < 0)
                    {
                        iOpenPaxCabinTime = 24 * 3600 + (tsClosePaxCabinTime.Hours - tsOpenCabinTime.Hours) * 60 + (tsClosePaxCabinTime.Minutes - tsOpenCabinTime.Minutes);
                    }
                    else
                    {
                        iOpenPaxCabinTime = (tsClosePaxCabinTime.Hours - tsOpenCabinTime.Hours) * 60 + (tsClosePaxCabinTime.Minutes - tsOpenCabinTime.Minutes);
                    }
                    dataRow["cniOpenTime"] = iOpenPaxCabinTime;
                }
                //判断机场名是否为空
                if (dataRow["cncDEPAirportCNAME"].ToString() == "")
                {
                    dataRow["cncDEPAirportCNAME"] = dataRow["cncDEPCityThreeCode"].ToString();
                }
                if (dataRow["cncARRAirportCNAME"].ToString() == "")
                {
                    dataRow["cncARRAirportCNAME"] = dataRow["cncARRCityThreeCode"].ToString();
                }
                //判断是否没有落地动态
                if (dataRow["cncSTATUS"].ToString() != "CNL" && dataRow["cncSTATUS"].ToString().Trim() != "ATA")
                {
                    if (DateTime.Parse(dataRow["cncETA"].ToString()).AddMinutes(m_accountBM.TDWNMinutes).CompareTo(DateTime.Now) < 0)
                    {
                        dataRow["cniNotTDWN"] = 1;
                    }
                    else
                    {
                        dataRow["cniNotTDWN"] = 0;
                    }
                }
                //判断是否有起飞动态
                //没有起飞动态告警
                if (dataRow["cncSTATUS"].ToString() != "CNL" && dataRow["cncSTATUS"].ToString().Trim() != "DEP" || dataRow["cncSTATUS"].ToString().Trim() != "ATA")
                {
                    if (DateTime.Parse(dataRow["cncETD"].ToString()).AddMinutes(m_accountBM.TOFFPromt).CompareTo(DateTime.Now) < 0)
                    {
                        dataRow["cniNotTOFF"] = 1;
                    }
                    else
                    {
                        dataRow["cniNotTOFF"] = 0;
                    }
                }

                //标准过站时间
                string strDEPAirportPaxType = "1";
                if (dataRow["cniDEPAirportPaxType"].ToString().Trim() != "")
                {
                    strDEPAirportPaxType = dataRow["cniDEPAirportPaxType"].ToString();
                }
                DataRow[] drStandardIntermission = m_dtStandardIntermissionTime.Select("cncACTYPE = '" + dataRow["cncACTYP"].ToString() + "' AND cniAirportPaxType = " + strDEPAirportPaxType);

                TimeSpan tsIntermissionTime = new TimeSpan(0, 0, 0);
                //判断是否过站不足
                if (dataRow["cncSTATUS"].ToString() != "CNL" && iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    //计算过站时间
                    tsIntermissionTime = DateTime.Parse(dataRow["cncETD"].ToString()).Subtract(DateTime.Parse(dtStationFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));

                    dataRow["cniIntermissionTime"] = tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes;


                    if (drStandardIntermission.Length > 0)
                    {
                        if (dataRow["cncDEPIsSelf"].ToString().Trim() == "1" && dataRow["cncARRIsSelf"].ToString().Trim() == "1") //国内航班
                        {
                            if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
                            {
                                dataRow["cniNotEnoughIntermissionTime"] = 1;
                            }
                        }
                        else   //国际航班
                        {
                            if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniInterIntermissionTime"].ToString()))
                            {
                                dataRow["cniNotEnoughIntermissionTime"] = 1;
                            }
                        }
                    }
                }

                //计算放行延误时间
                //始发航班                
                if (dataRow["cncSTATUS"].ToString() != "CNL" && (iRowIndex == 0 || dataRow["cnvcLONG_REG"].ToString() != dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString()))
                {
                    if (dataRow["cnvcDELAY1"].ToString() != "")
                    {
                        dataRow["cniDischargingDelayTime"] = dataRow["cniDUR1"].ToString();
                    }
                }
                //过站航班
                else if (dataRow["cncSTATUS"].ToString() != "CNL" && iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    if (drStandardIntermission.Length > 0)
                    {
                        //计算过站时间
                        tsIntermissionTime = DateTime.Parse(dataRow["cncSTD"].ToString()).Subtract(DateTime.Parse(dtStationFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
                        if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes >= Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
                        {
                            if (dataRow["cnvcDELAY1"].ToString() != "")
                            {
                                dataRow["cniDischargingDelayTime"] = dataRow["cniDUR1"].ToString();
                            }
                        }
                        else
                        {
                            tsIntermissionTime = DateTime.Parse(dataRow["cncTOFF"].ToString()).Subtract(DateTime.Parse(dtStationFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
                            if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
                            {
                                if (dataRow["cnvcDELAY1"].ToString() != "")
                                {
                                    dataRow["cniDischargingDelayTime"] = tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes - Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString());
                                }
                            }
                        }
                    }
                }

                if (drStandardIntermission.Length > 0)
                {
                    //判断是否没有关舱时间
                    if (dataRow["cncSTATUS"].ToString() != "CNL" && iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() != dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() && tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > 180) //过站航班
                    {
                        if (DateTime.Now.AddMinutes(Convert.ToInt32(drStandardIntermission[0]["cniIntermissionCloseCabinTime"].ToString())) > DateTime.Parse(dataRow["cncETD"].ToString()))
                        {
                            dataRow["cniNotClosePaxCabineTime"] = 1;
                        }

                    }
                    else  //始发航班
                    {
                        if (DateTime.Now.AddMinutes(Convert.ToInt32(drStandardIntermission[0]["cniInitialCloseCabinTime"].ToString())) > DateTime.Parse(dataRow["cncETD"].ToString()))
                        {
                            dataRow["cniNotClosePaxCabineTime"] = 1;
                        }
                    }
                }


                //判断是否开始保障
                if (dataRow["cncSTATUS"].ToString() != "CNL" && DateTime.Now.AddMinutes(m_accountBM.GuaranteeMinutes) > DateTime.Parse(dataRow["cncETD"].ToString()))
                {
                    dataRow["cniStartGuarantee"] = 1;
                }

                //判断是否开始登机
                if (dataRow["cncSTATUS"].ToString() != "CNL" && DateTime.Now.AddMinutes(m_accountBM.BoardingMinutes) > DateTime.Parse(dataRow["cncETD"].ToString()))
                {
                    dataRow["cniBoarding"] = 1;
                }

                //判断是否机务到位
                //if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                //{
                if (iRowIndex != 0 && dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() != "")
                {
                    if (dtStationFlights.Rows[iRowIndex - 1]["cncSTATUS"].ToString() != "CNL" && DateTime.Parse(dtStationFlights.Rows[iRowIndex - 1]["cncETA"].ToString()).AddMinutes(-m_accountBM.MCCReadyMinutes) < DateTime.Now)
                    {
                        dtStationFlights.Rows[iRowIndex - 1]["cniMCCReady"] = 1;
                    }
                }


                //判断是否机务放行
                if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    if (dataRow["cncSTATUS"].ToString() != "CNL" && DateTime.Now > DateTime.Parse(dtStationFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()).AddMinutes(m_accountBM.MCCReleasMinutes))
                    {
                        dataRow["cniMCCRelease"] = 1;
                    }
                }

                iRowIndex += 1;
            }

        }
        #endregion

        #region 获取要显示表的架构
        /// <summary>
        /// 获取要显示表的架构
        /// </summary>
        private DataTable GetDisplaySchema()
        {
            DataTable dtInOutFlights = new DataTable();

            //增加主键和排序字段
            dtInOutFlights.Columns.Add("IncncDATOP");                   //进港航班日期
            dtInOutFlights.Columns.Add("IncnvcFLTID");                  //进港航班航班号
            dtInOutFlights.Columns.Add("IncniLEGNO");                   //进港航班航段信息
            dtInOutFlights.Columns.Add("IncnvcAC");                     //进港航班飞机号
            dtInOutFlights.Columns.Add("IncncAllSTA");                  //进港航班计划到达时间（完整格式）
            dtInOutFlights.Columns.Add("IncncAllETA");                  //进港航班预计到达时间
            dtInOutFlights.Columns.Add("IncncAllTDWN");                 //进港航班落地时间
            dtInOutFlights.Columns.Add("IncncAllATA");                  //进港航班到位时间
            dtInOutFlights.Columns.Add("IncncAllStatus");               //进港航班航班状态
            dtInOutFlights.Columns.Add("IncniAllViewIndex");            //进港航班显示顺序

            dtInOutFlights.Columns.Add("OutcncDATOP");                  //出港航班日期
            dtInOutFlights.Columns.Add("OutcnvcFLTID");                 //出港航班航班号
            dtInOutFlights.Columns.Add("OutcniLEGNO");                  //出港航班航段信息
            dtInOutFlights.Columns.Add("OutcnvcAC");                    //出港航班飞机号
            dtInOutFlights.Columns.Add("OutcncAllSTD");                 //出港航班计划起飞时间
            dtInOutFlights.Columns.Add("OutcncAllETD");                 //出港航班预计起飞时间
            dtInOutFlights.Columns.Add("OutcncAllATD");                 //出港航班推出时间
            dtInOutFlights.Columns.Add("OutcncAllTOFF");                //出港航班起飞时间    
            dtInOutFlights.Columns.Add("OutcncAllStatus");              //出港航班航班状态
            dtInOutFlights.Columns.Add("OutcniAllViewIndex");           //出港航班显示顺序

            //根据用户设置的视图生成其他字段
            foreach (DataRow dataRow in m_dtDataItems.Rows)
            {
                dtInOutFlights.Columns.Add(dataRow["cnvcDataItemID"].ToString());
            }

            //行号
            dtInOutFlights.Columns.Add("cniRowIndex");
            return dtInOutFlights;
        }
        #endregion

        #region 填充进出港航班状态
        /// <summary>
        /// 填充进出港航班状态
        /// 将航站航班信息表分成进港航班信息表和出港航班信息表
        /// 然后将这两个表再组合成一个进出港航班信息表
        /// </summary>
        private IList FillInOutFlights(DataTable dtStationFlights, int iToday)
        {
            IList ilInOutFlights = new ArrayList();

            //进出港航班信息表
            DataTable dtAllInOutFlights = m_dtInOutFlightsSchema.Clone();

            //分别生成进港航班信息表和出港航班信息表
            //以便稍后组合生成进出港航班信息表
            //进港航班：目的机场三字码与航站三字码相同
            DataTable dtInFlights = dtStationFlights.Clone();
            //出港航班：起飞机场三字码与航站三字码相同
            DataTable dtOutFlights = dtStationFlights.Clone();

            //查询进港航班
            DataRow[] drInFlights = dtStationFlights.Select("cncARRSTN = '" + m_stationBM.ThreeCode + "'", "cniViewIndex,cncTDWN");
            foreach (DataRow dataRow in drInFlights)
            {
                dtInFlights.ImportRow(dataRow);
            }

            //查询出港航班
            DataRow[] drOutFlights = dtStationFlights.Select("cncDEPSTN = '" + m_stationBM.ThreeCode + "'", "cniViewIndex,cncTOFF");
            foreach (DataRow dataRow in drOutFlights)
            {
                dtOutFlights.ImportRow(dataRow);
            }

            #region 根据进港航班组合进出港航班
            //根据进港航班组合进出港航班
            foreach (DataRow dataRow in dtInFlights.Rows)
            {
                //进港航班的飞机号
                string strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
                //进港航班的预达时间
                string strInETD = dataRow["cncETD"].ToString();

                //查询字符串：根据出港航班的飞机号与进港航班的飞机号相同AND预计起飞时间大于进港航班的预计起飞时间
                string strSearch = "cnvcLONG_REG = '" + strLONG_REG + "' AND cncETD > '" + strInETD + "'";
                drOutFlights = dtOutFlights.Select(strSearch, "cncETD");

                #region 没有出港航班
                //没有出港航班
                if (drOutFlights.Length <= 0)
                {
                    //根据航班状态设置航班显示顺序
                    string strOutViewIndex = "";
                    if (dataRow["cncSTATUS"].ToString() == "CNL")
                    {
                        strOutViewIndex = "0";
                    }
                    else if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = "3";
                    }

                    //新建一行记录
                    DataRow drInFlight = dtAllInOutFlights.NewRow();

                    #region 对主键和排序字段赋值
                    //对主键和排序字段赋值
                    //进港部分
                    drInFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //出港部分
                    drInFlight["OutcncDATOP"] = "1900-01-01";
                    drInFlight["OutcnvcFLTID"] = "HU 0000";
                    drInFlight["OutcniLEGNO"] = 100;
                    drInFlight["OutcnvcAC"] = "HH";
                    drInFlight["OutcncAllSTD"] = "";
                    drInFlight["OutcncAllETD"] = "";
                    drInFlight["OutcncAllATD"] = "";
                    drInFlight["OutcncAllTOFF"] = "";
                    drInFlight["OutcncAllStatus"] = "";
                    drInFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //首先将进港机号设置为出港机号
                    if (dtAllInOutFlights.Columns.Contains("OutcnvcLONG_REG"))
                    {
                        drInFlight["OutcnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                    }

                    #region 对用户设置需显示的字段进行赋值
                    //对用户设置需显示的字段进行赋值
                    foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                    {
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            drInFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }
                    #endregion

                    dtAllInOutFlights.Rows.Add(drInFlight);
                }
                #endregion

                #region 有出港航班
                //有出港航班
                else
                {
                    //修正出港航班状态排序
                    string strOutViewIndex = "";
                    string strStatus = drOutFlights[0]["cncSTATUS"].ToString();
                    if (drOutFlights[0]["cncSTATUS"].ToString() == "ATA" || drOutFlights[0]["cncSTATUS"].ToString() == "ARR")
                    {
                        strOutViewIndex = "2";
                    }
                    else
                    {
                        strOutViewIndex = drOutFlights[0]["cniViewIndex"].ToString();
                    }

                    //新建一行
                    DataRow drInOutFlight = dtAllInOutFlights.NewRow();

                    #region 对主键和排序字段赋值
                    //对主键和排序字段赋值
                    //进港部分
                    drInOutFlight["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                    drInOutFlight["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    drInOutFlight["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    drInOutFlight["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                    drInOutFlight["IncncAllSTA"] = dataRow["cncSTA"].ToString();
                    drInOutFlight["IncncAllETA"] = dataRow["cncETA"].ToString();
                    drInOutFlight["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();
                    drInOutFlight["IncncAllATA"] = dataRow["cncATA"].ToString();
                    drInOutFlight["IncncAllStatus"] = dataRow["cncSTATUS"].ToString();
                    drInOutFlight["IncniAllViewIndex"] = dataRow["cniViewIndex"].ToString();
                    //出港部分
                    drInOutFlight["OutcncDATOP"] = drOutFlights[0]["cncDATOP"].ToString();
                    drInOutFlight["OutcnvcFLTID"] = drOutFlights[0]["cnvcFLTID"].ToString();
                    drInOutFlight["OutcniLEGNO"] = drOutFlights[0]["cniLEGNO"].ToString();
                    drInOutFlight["OutcnvcAC"] = drOutFlights[0]["cnvcAC"].ToString();
                    drInOutFlight["OutcncAllSTD"] = drOutFlights[0]["cncSTD"].ToString();
                    drInOutFlight["OutcncAllETD"] = drOutFlights[0]["cncETD"].ToString();
                    drInOutFlight["OutcncAllATD"] = drOutFlights[0]["cncATD"].ToString();
                    drInOutFlight["OutcncAllTOFF"] = drOutFlights[0]["cncTOFF"].ToString();
                    drInOutFlight["OutcncAllStatus"] = drOutFlights[0]["cncSTATUS"].ToString();
                    drInOutFlight["OutcniAllViewIndex"] = strOutViewIndex;
                    #endregion

                    //对用户设置需显示的字段进行赋值
                    //进港航班部分
                    foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                    {
                        //格式化特殊字段
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //进港机位
                        if (dataRowItems["cnvcDataItemID"].ToString() == "IncnvcInGATE")
                        {
                            if (drOutFlights[0]["cnvcOutGate"].ToString().Trim() == "")
                            {
                                strFieldValue = drOutFlights[0]["cnvcOutGate"].ToString().Trim();
                            }
                        }

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            string strTemp = dataRowItems["cnvcDataItemID"].ToString();
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    //出港航班部分
                    foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                    {
                        //格式化特殊字段
                        string strFieldValue = FormatOUTItem(drOutFlights[0], dataRowItems);

                        //其他字段直接读取值
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                        {
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    dtAllInOutFlights.Rows.Add(drInOutFlight);
                    dtOutFlights.Rows.Remove(drOutFlights[0]);
                }
                #endregion
            }
            #endregion

            #region 根据出港航班组合进出港航班
            //根据出港航班组合进出港航班
            //针对仅有出港航班的情况
            foreach (DataRow dataRow in dtOutFlights.Rows)
            {
                //修正出港航班的状态排序
                string strOutViewIndex = "";
                if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                {
                    strOutViewIndex = "2";
                }
                else
                {
                    strOutViewIndex = dataRow["cniViewIndex"].ToString();
                }

                //新建一行
                DataRow drOutFlight = dtAllInOutFlights.NewRow();

                //对主键和排序字段赋值
                drOutFlight["IncncDATOP"] = "1900-01-01";
                drOutFlight["IncnvcFLTID"] = "HU 0000";
                drOutFlight["IncniLEGNO"] = 100;
                drOutFlight["IncnvcAC"] = "HH";
                drOutFlight["IncncAllSTA"] = "";
                drOutFlight["IncncAllETA"] = "";
                drOutFlight["IncncAllTDWN"] = "";
                drOutFlight["IncncAllATA"] = "";
                drOutFlight["IncncAllStatus"] = "";
                drOutFlight["IncniAllViewIndex"] = "0";

                //首先将进港机位设置为出港机位
                if (dtAllInOutFlights.Columns.Contains("IncnvcInGATE"))
                {
                    drOutFlight["IncnvcInGATE"] = dataRow["cnvcOutGate"].ToString();
                }

                //首先将进港机号设置为出港机号
                if (dtAllInOutFlights.Columns.Contains("IncnvcLONG_REG"))
                {
                    drOutFlight["IncnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                }

                //将进港机型设置为出港机型 -- added by LinYong in 20150330
                if (dtAllInOutFlights.Columns.Contains("IncncACTYP") && dtOutFlights.Columns.Contains("cncACTYP"))
                {
                    drOutFlight["IncncACTYP"] = dataRow["cncACTYP"].ToString();
                }

                drOutFlight["OutcncDATOP"] = dataRow["cncDATOP"].ToString();
                drOutFlight["OutcnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                drOutFlight["OutcniLEGNO"] = dataRow["cniLEGNO"].ToString();
                drOutFlight["OutcnvcAC"] = dataRow["cnvcAC"].ToString();
                drOutFlight["OutcncAllSTD"] = dataRow["cncSTD"].ToString();
                drOutFlight["OutcncAllETD"] = dataRow["cncETD"].ToString();
                drOutFlight["OutcncAllATD"] = dataRow["cncATD"].ToString();
                drOutFlight["OutcncAllTOFF"] = dataRow["cncTOFF"].ToString();
                drOutFlight["OutcncAllStatus"] = dataRow["cncSTATUS"].ToString();
                drOutFlight["OutcniAllViewIndex"] = strOutViewIndex;

                //对用户设置需显示的字段进行赋值
                foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                {
                    //格式化特殊字段
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    //对其他字段进行处理
                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        drOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }

                dtAllInOutFlights.Rows.Add(drOutFlight);
            }
            #endregion

            //根据进出港航班信息表生成航班保障信息实体列表
            foreach (DataRow dataRow in dtAllInOutFlights.Rows)
            {
                ilInOutFlights.Add(new GuaranteeInforBM(dataRow));

            }
            (ilInOutFlights as ArrayList).Sort();

            //对行号赋值
            IEnumerator ieInOutFlights = ilInOutFlights.GetEnumerator();
            int iRowIndex = 0;
            while (ieInOutFlights.MoveNext())
            {
                GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieInOutFlights.Current;
                DataRow[] drInOutFlights = dtAllInOutFlights.Select("IncncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                    "IncnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                    "IncniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                    "IncnvcAC = '" + guaranteeInforBM.IncnvcAC + "' AND " +
                    "OutcncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                    "OutcnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                    "OutcniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                    "OutcnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                //对进出港航班信息表每行的cniRowIndex字段，即行号赋值
                if (drInOutFlights.Length > 0)
                {
                    drInOutFlights[0]["cniRowIndex"] = iRowIndex;
                }
                iRowIndex += 1;
            }

            //如果是今天
            if (iToday == 1)
            {
                m_dtTodayInOutFlights = dtAllInOutFlights;
            }
            return ilInOutFlights;
        }
        #endregion

        #region 初始化表格背景色
        /// <summary>
        /// 初始化表格背景色
        /// </summary>
        private void InitialGridColor(FarPoint.Win.Spread.SheetView shGrid)
        {
            for (int iLoop = 0; iLoop < shGrid.Rows.Count; iLoop++)
            {
                shGrid.Rows[iLoop].BackColor = colorFavorate;
                for (int jLoop = 0; jLoop < shGrid.Columns.Count; jLoop++)
                {
                    shGrid.Cells[iLoop, jLoop].BackColor = colorFavorate;
                }
            }
        }
        #endregion

        #region 设置某行单元格颜色
        /// <summary>
        /// 设置某行单元格颜色
        /// </summary>
        /// <param name="iRowIndex"></param>
        private void SetRowCellColor(int iDay, int iRowIndex, Color colorValue)
        {
            fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor = colorValue;
            for (int jLoop = 0; jLoop < fpFlightInfo.Sheets[iDay].Columns.Count; jLoop++)
            {
                if (fpFlightInfo.Sheets[iDay].Cells[iRowIndex, jLoop].BackColor != Color.Yellow)
                {
                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, jLoop].BackColor = colorValue;
                }
            }
        }
        #endregion

        #region 根据航班状态调整航班显示范围
        /// <summary>
        /// 根据航班状态调整航班显示范围
        /// </summary>
        private void AdjustScreen()
        {
            if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[1].Rows.Count; iLoop++)
                {
                    if (fpFlightInfo.Sheets[1].Rows[iLoop].BackColor != Color.Silver)
                    {
                        fpFlightInfo.ShowRow(0, iLoop, FarPoint.Win.Spread.VerticalPosition.Top);
                        break;
                    }
                }
            }
        }
        #endregion

        #region 设置表格颜色
        /// <summary>
        /// 设置表格颜色
        /// </summary>
        /// <param name="iDay">0:昨天 1:今天: 2:明天 3:所选日期</param>
        private void SetGridColor(int iDay, DataTable dtStationFlights)
        {
            try
            {
                IList ilInOutFlights = new ArrayList();
                if (iDay == 0)
                {
                    ilInOutFlights = m_ilYesterdayInOutFlights;
                }
                else if (iDay == 1)
                {
                    ilInOutFlights = m_ilTodayInOutFlights;
                }
                else if (iDay == 2)
                {
                    ilInOutFlights = m_ilTomorrowInOutFlights;
                }
                else if (iDay == 3)
                {
                    ilInOutFlights = m_ilSelectDateInOutFlights;
                }

                IEnumerator ieInOutFlights = ilInOutFlights.GetEnumerator();

                int iRowIndex = 0;
                while (ieInOutFlights.MoveNext())
                {
                    GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)ieInOutFlights.Current;

                    DataRow[] drInFlights = dtStationFlights.Select("cncDATOP='" + guaranteeInforBM.IncncDATOP + "' AND cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");
                    DataRow[] drOutFlights = dtStationFlights.Select("cncDATOP='" + guaranteeInforBM.OutcncDATOP + "' AND cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                    //设置行颜色
                    if (drInFlights.Length > 0 && drOutFlights.Length <= 0)  //只有进港航班
                    {
                        if (drInFlights[0]["cncSTATUS"].ToString() == "CNL" || drInFlights[0]["cncSTATUS"].ToString() == "ATA" || drInFlights[0]["cncSTATUS"].ToString() == "ARR")
                        {
                            SetRowCellColor(iDay, iRowIndex, Color.Silver);
                        }
                    }
                    else if (drInFlights.Length <= 0 && drOutFlights.Length > 0)  //只有出港港航班
                    {
                        if (drOutFlights[0]["cncSTATUS"].ToString() == "CNL" || drOutFlights[0]["cncSTATUS"].ToString() == "DEP" || drOutFlights[0]["cncSTATUS"].ToString() == "ATA" || drOutFlights[0]["cncSTATUS"].ToString() == "ATD" || drOutFlights[0]["cncSTATUS"].ToString() == "ARR")
                        {
                            SetRowCellColor(iDay, iRowIndex, Color.Silver);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(guaranteeInforBM.OutcniAllViewIndex) < 3)
                        {
                            SetRowCellColor(iDay, iRowIndex, Color.Silver);
                        }
                    }


                    foreach (DataRow dataRowDataItem in m_dtDataItems.Rows)
                    {
                        if (drInFlights.Length > 0)   //如果有进港航班
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightNo")  //进港航班号
                            {
                                //备降、返航
                                if (drInFlights[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drInFlights[0]["cniLEGNO"].ToString()) % 100 != 0)
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }

                                //重点保障航班
                                if (drInFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Orange;
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }

                            }
                            //进港性质
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightCharacterAbbreviate")  //进港性质
                            {
                                if (drInFlights[0]["cnvcSTC"].ToString() != "J")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Red;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Black;
                                }
                            }
                            //进港预达时间
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncETA")
                            {
                                if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "DEP") //起飞
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.LimeGreen;
                                }
                                else if ((drInFlights[0]["cncSTATUS"].ToString().Trim() == "ARR") ||
                                    (drInFlights[0]["cncSTATUS"].ToString().Trim() == "ATA")) //到达
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                }
                                else if (drInFlights[0]["cnvcDELAY1"].ToString().Trim() != "") //延误
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }

                                else //其他状态
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }

                            //进港落地时间
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncTDWN")
                            {
                                //if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "DEP")
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.LimeGreen;
                                //}
                                if ((drInFlights[0]["cncSTATUS"].ToString().Trim() != "ARR") && (drInFlights[0]["cncSTATUS"].ToString().Trim() != "ATA")) //非落地 或 非到达
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor; ;
                                }
                                //else if (drInFlights[0]["cnvcDELAY1"].ToString().Trim() != "") //延误
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                //}
                                //else if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "ATA") //到达
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                //}
                                else //其他
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                }

                                //没有到达动态告警

                                if (m_accountBM.TDWNPromt == 1 && drInFlights[0]["cncSTATUS"].ToString() != "CNL" && drInFlights[0]["cniNotTDWN"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }

                            }

                            //进港到达时间
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncATA")
                            {
                                if (drInFlights[0]["cncSTATUS"].ToString().Trim() != "ATA")  //非到达
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor; ;
                                }
                                //else if (drInFlights[0]["cnvcDELAY1"].ToString().Trim() != "") //延误
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                //}
                                else //到达
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                }
                            }

                            //值机人数
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncniCheckNum")
                            {
                                if (drInFlights[0]["cntCheckInfo"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cntCheckInfo"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //中转旅客信息
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnbTransitPaxTag")
                            {
                                if (drInFlights[0]["cntTransitPax"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cntTransitPax"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //旅客名单
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncntPaxNameList")
                            {
                                if (drInFlights[0]["cntPaxNameList"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cntPaxNameList"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //进港备注
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcInRemark")
                            {
                                if (drInFlights[0]["cnvcInRemark"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cnvcInRemark"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //进港重点保障航班
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncniFocusTag")
                            {
                                if (drInFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {
                                    //fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Orange;
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //机务到位
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncInMCCReadyTime")
                            {
                                if (m_accountBM.MCCReady == 1 && Convert.ToInt32(guaranteeInforBM.OutcniAllViewIndex) > 2 && drInFlights[0]["cniMCCReady"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor; ;
                                }
                            }
                        }

                        //只有进港航班
                        if (drInFlights.Length > 0 && drOutFlights.Length == 0)
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightNo")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //值机信息
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniCheckNum")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //中转连程旅客
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnbTransitPaxTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //旅客名单
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcntPaxNameList")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //备注
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcOutRemark")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //重点保障航班
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniFocusTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                        }

                        //只有出港航班
                        if (drInFlights.Length == 0 && drOutFlights.Length > 0)
                        {
                            //
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightNo")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //中转旅客信息
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnbTransitPaxTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //旅客名单
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncntPaxNameList")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //进港备注
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcInRemark")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //进港重点保障航班
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncniFocusTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                        }


                        if (drOutFlights.Length > 0)
                        {
                            //出港延误时间
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncETD")
                            {
                                if (drOutFlights[0]["cnvcDELAY1"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncTOFF")
                            {
                                //没有起飞动态告警
                                if (m_accountBM.TDWNPromt == 1 && Convert.ToInt32(guaranteeInforBM.OutcniAllViewIndex) > 2 && drOutFlights[0]["cniNotTOFF"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightNo")
                            {
                                //重点保障航班
                                if (drOutFlights[0]["cniFocusTag"].ToString() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Orange;

                                }

                                //备降、返航
                                else if (drOutFlights[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drOutFlights[0]["cniLEGNO"].ToString()) % 100 != 0)
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }
                                //过站时间不足
                                else if (drOutFlights[0]["cniNotEnoughIntermissionTime"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Plum;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }

                                if (drOutFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //出港性质
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightCharacterAbbreviate")  //出港性质
                            {
                                if (drOutFlights[0]["cnvcSTC"].ToString() != "J")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Red;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Black;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncSTD")
                            {
                                //开始保障提示
                                if (m_accountBM.StartGuarantee == 1 && Convert.ToInt32(guaranteeInforBM.OutcniAllViewIndex) > 2 && drOutFlights[0]["cniStartGuarantee"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncBoardTime")
                            {
                                //开始登机提示
                                if (m_accountBM.Boarding == 1 && Convert.ToInt32(guaranteeInforBM.OutcniAllViewIndex) > 2 && drOutFlights[0]["cniBoarding"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncMCCReleaseTime")
                            {
                                //机务放行提示
                                if (m_accountBM.MCCRelease == 1 && Convert.ToInt32(guaranteeInforBM.OutcniAllViewIndex) > 2 && drOutFlights[0]["cniMCCRelease"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }
                            //值机信息
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniCheckNum")
                            {
                                if (drOutFlights[0]["cntCheckInfo"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cntCheckInfo"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //中转连程旅客
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnbTransitPaxTag")
                            {
                                if (drOutFlights[0]["cntTransitPax"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cntTransitPax"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //旅客名单
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcntPaxNameList")
                            {
                                if (drOutFlights[0]["cntPaxNameList"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cntPaxNameList"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //备注
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcOutRemark")
                            {
                                if (drOutFlights[0]["cnvcOutRemark"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cnvcOutRemark"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }

                             //出港重点保障航班
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniFocusTag")
                            {
                                if (drOutFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }



                        }

                    }

                    iRowIndex += 1;
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 获取最大变更序号
        /// <summary>
        /// 获取最大变更序号
        /// </summary>
        private void GetMaxRecordNo()
        {
            //调用业务外观层方法
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            #region modified by LinYong in 20091223
            //ReturnValueSF rvSF = changeRecordBF.GetMaxRecordNo();
            ReturnValueSF rvSF = changeRecordBF.GetMaxRecordNo(m_accountBM);
            #endregion

            if (rvSF.Result > 0)
            {
                m_iLastRecordNo = rvSF.Result;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 系统启动时获取变更数据
        /// <summary>
        /// 系统启动时获取变更数据
        /// </summary>
        private void GetChangeData()
        {
            //变更数据
            lvChangeContent.Items.Clear();
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            #region modified by LinYong in 20091223
            //ReturnValueSF rvSF = changeRecordBF.GetChangeRecords(m_iLastRecordNo - 500, GetDateTimeBM(1), m_stationBM);
            ReturnValueSF rvSF = changeRecordBF.GetChangeRecords(m_iLastRecordNo - 500, GetDateTimeBM(1), m_stationBM, m_accountBM);
            #endregion

            if (rvSF.Result > 0)
            {
                AddChangeDataToList(rvSF.Dt, 1);
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 将变更记录加入变更列表中，并判断是否进行声音提示
        /// <summary>
        /// 将变更记录加入变更列表中，并判断是否进行声音提示
        /// </summary>
        /// <param name="dtChangeData">变更信息列表</param>
        /// <param name="iAdd">1=系统启动时，变更记录添加到变更列表最后一行；0=定时刷新时，变更记录从第一行插入到变更列表</param>
        private void AddChangeDataToList(DataTable dtChangeData, int iAdd)
        {
            int iAddRowNum = 0;

            #region 将变更记录插入变更列表中
            //逐条遍历变更记录表中的记录
            foreach (DataRow dataRow in dtChangeData.Rows)
            {
                //延误原因
                string strDelayCode = "";
                //是否为出港航班
                int iOutFlight = 0;
                //查询字符串
                string strSearch;
                //进出港航班信息
                GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM();

                #region 判断是进港航班还是出港航班
                //进出港航班信息列表
                IEnumerator ieTodayInOutFlights = m_ilTodayInOutFlights.GetEnumerator();
                //逐条遍历进出港航班信息表
                //判断是进港航班还是出港航班
                while (ieTodayInOutFlights.MoveNext())
                {
                    guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current;
                    //如果进港航班涉及到变更
                    if (guaranteeInforBM["IncncDATOP"] == dataRow["cncOldDATOP"].ToString() &&
                        guaranteeInforBM["IncnvcFLTID"] == dataRow["cnvcOldFLTID"].ToString() &&
                        guaranteeInforBM["IncniLEGNO"] == dataRow["cniOldLEGNO"].ToString() &&
                        guaranteeInforBM["IncnvcAC"] == dataRow["cnvcOldAC"].ToString() ||
                        guaranteeInforBM["IncncDATOP"] == dataRow["cncNewDATOP"].ToString() &&
                        guaranteeInforBM["IncnvcFLTID"] == dataRow["cnvcNewFLTID"].ToString() &&
                        guaranteeInforBM["IncniLEGNO"] == dataRow["cniNewLEGNO"].ToString() &&
                        guaranteeInforBM["IncnvcAC"] == dataRow["cnvcNewAC"].ToString())
                    {
                        strDelayCode = guaranteeInforBM["IncnvcFlightDelayName"];
                        break;
                    }
                    //如果出港航班涉及到变更
                    else if (guaranteeInforBM["OutcncDATOP"] == dataRow["cncOldDATOP"].ToString() &&
                        guaranteeInforBM["OutcnvcFLTID"] == dataRow["cnvcOldFLTID"].ToString() &&
                        guaranteeInforBM["OutcniLEGNO"] == dataRow["cniOldLEGNO"].ToString() &&
                        guaranteeInforBM["OutcnvcAC"] == dataRow["cnvcOldAC"].ToString() ||
                        guaranteeInforBM["OutcncDATOP"] == dataRow["cncNewDATOP"].ToString() &&
                        guaranteeInforBM["OutcnvcFLTID"] == dataRow["cnvcNewFLTID"].ToString() &&
                        guaranteeInforBM["OutcniLEGNO"] == dataRow["cniNewLEGNO"].ToString() &&
                        guaranteeInforBM["OutcnvcAC"] == dataRow["cnvcNewAC"].ToString())
                    {
                        iOutFlight = 1;
                        strDelayCode = guaranteeInforBM["IncnvcFlightDelayName"];
                        break;
                    }
                }//end while
                #endregion

                #region 判断是否声音提示
                //判断是否声音提示
                if (iAdd == 0)
                {
                    //用户是否设置声音提示
                    if (iOutFlight == 1)
                    {
                        strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniSoundPromptItem = 1";
                    }
                    else
                    {
                        strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniSoundPromptItem = 1";
                    }
                    DataRow[] drDataItemSound = m_dtDataItemPurview.Select(strSearch);

                    //用户设置通过声音提示
                    if (drDataItemSound.Length > 0)
                    {
                        //如果用户自定义了声音
                        if (m_accountBM.SoundType == 0)
                        {
                            string mstrfileName = Application.StartupPath;
                            mstrfileName = mstrfileName + @"\WAV\front.WAV";
                            SoundHelpers.PlaySound(mstrfileName, IntPtr.Zero, SoundHelpers.PlaySoundFlags.SND_FILENAME | SoundHelpers.PlaySoundFlags.SND_ASYNC);
                        }
                        //主板声音
                        else
                        {
                            Beep(0X0FF, 100);
                        }
                    }
                }//end if (iAdd == 0)
                #endregion

                #region 判断是否有权限：浏览及其以上
                //判断是否有权限：浏览及其以上
                if (iOutFlight == 1)
                {
                    strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniDataItemPurview > 0";
                }
                else
                {
                    strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniDataItemPurview > 0";
                }
                DataRow[] drDataItemPurview = m_dtDataItemPurview.Select(strSearch);
                #endregion

                #region 生成变更信息列表
                //如果用户有权限
                if (drDataItemPurview.Length > 0)
                {
                    //如果是ETA或ETD变更，或者延误代码为空，则不进行处理
                    if ((dataRow["cnvcPrimaryNameField"].ToString() == "cncETA" || dataRow["cnvcPrimaryNameField"].ToString() == "cncETD") && strDelayCode == "")
                    {
                        continue;
                    }

                    //变更内容
                    string strOldContent, strNewContent, strOldDepSTNName, strOldArrSTNName;
                    strOldContent = dataRow["cnvcChangeOldContent"].ToString();             //变更前内容
                    strNewContent = dataRow["cnvcChangeNewContent"].ToString();             //变更后内容
                    strOldDepSTNName = dataRow["cnvcOldDepSTNName"].ToString();             //变更前起飞机场
                    strOldArrSTNName = dataRow["cnvcOldArrSTNName"].ToString();             //变更前目的机场

                    #region 针对特殊变更项目进行处理

                    #region 航班状态代码转换
                    //航班状态代码转换
                    if (dataRow["cnvcPrimaryNameField"].ToString() == "cncStatusName")
                    {
                        //变更前
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

                        //变更后
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
                    #endregion

                    #region 延误原因名称
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
                    #endregion

                    #region 机场名称
                    //起飞机场
                    int iSplitIndex = strOldDepSTNName.IndexOf("/");
                    if (iSplitIndex > 0)
                    {
                        strOldDepSTNName = strOldDepSTNName.Substring(0, iSplitIndex).Trim();
                    }
                    else
                    {
                        strOldDepSTNName = strOldDepSTNName.Trim();
                    }

                    //目的机场
                    iSplitIndex = strOldArrSTNName.IndexOf("/");
                    if (iSplitIndex > 0)
                    {
                        strOldArrSTNName = strOldArrSTNName.Substring(0, iSplitIndex).Trim();
                    }
                    else
                    {
                        strOldArrSTNName = strOldArrSTNName.Trim();
                    }
                    #endregion

                    #region 变更原因
                    //变更原因
                    string strChangeReason = dataRow["cnvcChangeReasonName"].ToString();
                    if (dataRow["cncActionTag"].ToString() == "I")
                    {
                        strChangeReason = "新增航班";
                    }
                    else if (dataRow["cncActionTag"].ToString() == "D")
                    {
                        strChangeReason = "删除航班";
                    }
                    #endregion

                    #endregion

                    #region 生成变更信息列表
                    //生成变更信息列表
                    string[] strArr = new string[5];
                    strArr[0] = dataRow["cnvcUserName"].ToString();                                 //操作员
                    //变更内容
                    strArr[1] = dataRow["cnvcOldFLTID"].ToString() + "(" +                          //航班号
                        strOldDepSTNName +                                                          //变更前起飞机场
                        DateTime.Parse(dataRow["cncSTD"].ToString()).ToString("HHmm") + "—" +      //计划起飞时间
                        DateTime.Parse(dataRow["cncSTA"].ToString()).ToString("HHmm") +             //计划到达时间
                        strOldArrSTNName + ")" + "<" +                                              //变更前目的机场
                        strChangeReason + ">" +                                                     //变更原因
                        strOldContent + "->" +                                                      //变更前内容
                        strNewContent;                                                              //变更后内容
                    strArr[2] = dataRow["cncLocalOperatingTime"].ToString();                        //操作时间
                    strArr[3] = dataRow["cncSTD"].ToString().Substring(0, 10);                      //航班日期

                    //生成列表中的一条记录
                    ListViewItem objListViewItem = new ListViewItem(strArr);

                    #region 增加信息，为了定位监控界面的具体列。 -- added by LinYong in 20150215
                    if (iOutFlight == 1)
                    {
                        objListViewItem.Tag = "Out" + dataRow["cnvcPrimaryNameField"].ToString();
                    }
                    else
                    {
                        objListViewItem.Tag = "In" + dataRow["cnvcPrimaryNameField"].ToString();
                    }
                    #endregion 增加信息，为了定位监控界面的具体列。 -- added by LinYong in 20150215

                    //如果是系统启动时
                    //变更记录按次序添加到变更列表中
                    if (iAdd == 1)
                    {
                        lvChangeContent.Items.Add(objListViewItem);
                    }
                    //如果是系统定时刷新时
                    //变更记录插到变更列表的第一行
                    //以便显示在最前面
                    else
                    {
                        lvChangeContent.Items.Insert(0, objListViewItem);
                        iAddRowNum += 1;
                        if (lvChangeContent.Items.Count > 100)
                        {
                            lvChangeContent.Items.RemoveAt(lvChangeContent.Items.Count - 1);
                        }
                    }
                    #endregion

                    //如果涉及变更的航班为进港航班，则底色设为Silver
                    //若为出港航班，则为默认颜色白色
                    if (dataRow["cncOldArrSTN"].ToString() == m_stationBM.ThreeCode || dataRow["cncNewArrSTN"].ToString() == m_stationBM.ThreeCode)
                    {
                        lvChangeContent.Items[lvChangeContent.Items.Count - 1].BackColor = Color.FromName("Silver");
                    }
                }//end if (drDataItemPurview.Length > 0)
                #endregion

            }//end foreach (DataRow dataRow in dtChangeData.Rows)
            #endregion

            #region 设置变更记录显示颜色
            //如果是在定时刷新时添加了新的变更信息
            if (iAdd == 0 && iAddRowNum != 0)
            {
                //本次变更的字体颜色为Red
                for (int iLoop = 0; iLoop < iAddRowNum; iLoop++)
                {
                    lvChangeContent.Items[iLoop].ForeColor = Color.FromName("Red");
                }

                //上一次变更的字体颜色为Green
                for (int iLoop = iAddRowNum; iLoop < iAddRowNum + m_iRedRecordNum; iLoop++)
                {
                    lvChangeContent.Items[iLoop].ForeColor = Color.FromName("Green");
                }

                //其他变更的背景色为Black
                for (int iLoop = iAddRowNum + m_iRedRecordNum; iLoop < iAddRowNum + m_iRedRecordNum + m_iGreenRecordNum; iLoop++)
                {
                    lvChangeContent.Items[iLoop].ForeColor = Color.FromName("Black");
                }

                m_iGreenRecordNum = m_iRedRecordNum;
                m_iRedRecordNum = iAddRowNum;
            }
            #endregion
        }
        #endregion

        #region 选择的基地实体对象
        /// <summary>
        /// 选择的基地实体对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStations_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_stationBM = new StationBM(m_dtStations.Rows[cmbStations.SelectedIndex]);
            fpFlightInfo.ActiveSheetIndex = 1;

            //获取最大变更序号和最后100条变更数据
            GetMaxRecordNo();
            GetChangeData();

            //获取该航站当天的所有航班
            m_dtTodayStationFlights = GetStationFlights(GetDateTimeBM(1), m_stationBM, 1);

            //计算相应的提示信息
            ComputeFlightsInfor(m_dtTodayStationFlights);

            //获取当天进出港航班
            m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

            //绑定数据
            fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;

            //设置选中的行
            if (m_iOldSelectedRow >= FpFlightInfo.Sheets[1].RowCount)
            {
                m_iOldSelectedRow = -1;
            }

            //进出港航班数
            SetInOutFlightsNum();

            //记录闪烁单元格背景色和是否第一次进入闪烁
            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];

            //初始化表格颜色
            InitialGridColor(shToday);

            //设置当天单元格颜色
            SetGridColor(1, m_dtTodayStationFlights);

            fpFlightInfo.Focus();
        }
        #endregion

        #region 切换页面
        /// <summary>
        /// 切换页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_ActiveSheetChanged(object sender, EventArgs e)
        {
            //昨天
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                //获取该航站当天的所有航班
                m_dtYesterdayStationFlights = GetStationFlights(GetDateTimeBM(0), m_stationBM, 0);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtYesterdayStationFlights);

                //获取当天进出港航班
                m_ilYesterdayInOutFlights = FillInOutFlights(m_dtYesterdayStationFlights, 0);

                //绑定数据
                fpFlightInfo.Sheets[0].DataSource = m_ilYesterdayInOutFlights;

                //进出港航班数
                SetInOutFlightsNum();

                //初始化表格颜色
                InitialGridColor(shYestoday);

                //设置当天单元格颜色
                SetGridColor(0, m_dtYesterdayStationFlights);

                #region yanxian 2013-10-14 号修改;用于旅客数据提取
                //记录选中的行和列以及原颜色
                m_iOldSelectedRow = -1;
                m_iOldSelectedColumn = -1;
                #endregion
            }
            //今天
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                //进出港航班数
                SetInOutFlightsNum();
            }
            //明天
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                //获取该航站当天的所有航班
                m_dtTomorrowStationFlights = GetStationFlights(GetDateTimeBM(2), m_stationBM, 2);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtTomorrowStationFlights);

                //获取当天进出港航班
                m_ilTomorrowInOutFlights = FillInOutFlights(m_dtTomorrowStationFlights, 0);

                //绑定数据
                fpFlightInfo.Sheets[2].DataSource = m_ilTomorrowInOutFlights;

                //进出港航班数
                SetInOutFlightsNum();

                //初始化表格颜色
                InitialGridColor(shTomorrow);

                //设置当天单元格颜色
                SetGridColor(2, m_dtTomorrowStationFlights);

                #region yanxian 2013-10-14 号修改;用于旅客数据提取
                //记录选中的行和列以及原颜色
                m_iOldSelectedRow = -1;
                m_iOldSelectedColumn = -1;
                #endregion
            }
            //用户选择的日期
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                //获取该航站当天的所有航班
                m_dtSelectDateStationFlights = GetStationFlights(GetDateTimeBM(3), m_stationBM, 3);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtSelectDateStationFlights);

                //获取当天进出港航班
                m_ilSelectDateInOutFlights = FillInOutFlights(m_dtSelectDateStationFlights, 0);

                fpFlightInfo.Sheets[3].DataSource = m_ilSelectDateInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();

                #region yanxian 2013-10-14 号修改;用于旅客数据提取
                //记录选中的行和列以及原颜色  
                m_iOldSelectedRow = -1;
                m_iOldSelectedColumn = -1;
                #endregion
            }

            //modified by LinYong in 20140707
            UpDownValue_ValueChanged(sender, e);

        }
        #endregion

        #region 用户选择日期进行查看
        /// <summary>
        /// 用户选择日期进行查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtFlightDate_ValueChanged(object sender, EventArgs e)
        {
            if (fpFlightInfo.ActiveSheetIndex != 3)
            {
                fpFlightInfo.ActiveSheetIndex = 3;
            }
            else
            {
                //获取该航站当天的所有航班
                m_dtSelectDateStationFlights = GetStationFlights(GetDateTimeBM(3), m_stationBM, 3);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtSelectDateStationFlights);

                //获取当天进出港航班
                m_ilSelectDateInOutFlights = FillInOutFlights(m_dtSelectDateStationFlights, 0);

                fpFlightInfo.Sheets[3].DataSource = m_ilSelectDateInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();
            }
            fpFlightInfo.Sheets[3].SheetName = dtFlightDate.Value.ToString("yyyy-MM-dd");

            fpFlightInfo.Focus();
        }
        #endregion

        #region 用户重新设置视图时，重新刷新颜色
        /// <summary>
        /// 重新设置视图时，重新刷新颜色
        /// </summary>
        public void RefreshView(int iDay)
        {
            timerSplash.Enabled = false;
            DataTable dtStationFlights;

            //获取航班信息
            if (iDay == 0)
            {
                dtStationFlights = m_dtYesterdayStationFlights;
            }
            else if (iDay == 1)
            {
                dtStationFlights = m_dtTodayStationFlights;
            }
            else if (iDay == 2)
            {
                dtStationFlights = m_dtTomorrowStationFlights;
            }
            else
            {
                dtStationFlights = m_dtSelectDateStationFlights;
            }

            //计算相应的提示信息
            ComputeFlightsInfor(dtStationFlights);

            //进出港航班表格Schema
            m_dtInOutFlightsSchema = GetDisplaySchema();

            //显示所有航班          
            if (iDay == 0)
            {
                m_ilYesterdayInOutFlights = FillInOutFlights(dtStationFlights, 0);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilYesterdayInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();
            }
            else if (iDay == 1)
            {
                m_ilTodayInOutFlights = FillInOutFlights(dtStationFlights, 1);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilTodayInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();
            }
            else if (iDay == 2)
            {
                m_ilTomorrowInOutFlights = FillInOutFlights(dtStationFlights, 0);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilTomorrowInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();
            }
            else
            {
                m_ilSelectDateInOutFlights = FillInOutFlights(dtStationFlights, 0);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilSelectDateInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();
            }

            //闪烁标识
            if (iDay == 1)
            {
                colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];
            }

            //初始化表格颜色
            InitialGridColor(fpFlightInfo.Sheets[iDay]);

            //设置表格颜色
            SetGridColor(iDay, dtStationFlights);

            timerSplash.Enabled = true;
        }
        #endregion

        #region 闪烁函数
        /// <summary>
        /// 闪烁函数
        /// </summary>
        private void Splash()
        {
            try
            {
                //如果显示的数据项为空
                if (m_strDataItemSearch == "" || m_strDataItemSearch == null)
                {
                    return;
                }

                //今天进出港航班信息实体列表
                IEnumerator ieTodayInOutFlights = m_ilTodayInOutFlights.GetEnumerator();

                DataRow[] drSplashTag;
                //用户设置要显示的数据项
                drSplashTag = m_dtSplashTag.Select(m_strDataItemSearch);
                //如果没有显示的数据项
                if (drSplashTag.Length <= 0)
                {
                    return;
                }

                int iRowIndex = 0;

                //生成查询字符串
                //查询有项目需要闪烁的进出港航班记录
                string strSearch = "";
                foreach (DataRow dataRow in drSplashTag)
                {
                    strSearch += "(IncncDATOP = '" + dataRow["cncDATOP"].ToString() + "' AND " +
                        "IncnvcFLTID = '" + dataRow["cnvcFLTID"].ToString() + "' AND " +
                        "IncniLEGNO = " + dataRow["cniLEGNO"].ToString() + " AND " +
                        "IncnvcAC = '" + dataRow["cnvcAC"].ToString() + "') OR (" +
                        "OutcncDATOP = '" + dataRow["cncDATOP"].ToString() + "' AND " +
                        "OutcnvcFLTID = '" + dataRow["cnvcFLTID"].ToString() + "' AND " +
                        "OutcniLEGNO = " + dataRow["cniLEGNO"].ToString() + " AND " +
                        "OutcnvcAC = '" + dataRow["cnvcAC"].ToString() + "') OR ";
                }
                strSearch += strSearch.Substring(0, strSearch.Length - 3);

                //有项目需要闪烁的行
                DataRow[] drInOutFlights = m_dtTodayInOutFlights.Select(strSearch);

                //test
                //if (drInOutFlights.Length > 0)
                //    MessageBox.Show(strSearch);




                //逐行遍历有项目需要闪烁的进出港航班信息记录表
                foreach (DataRow drInOutFlight in drInOutFlights)
                {
                    //test
                    //MessageBox.Show(drInOutFlight["OutcnvcFLTID"].ToString() + " ； " + drInOutFlight["OutcnvcAC"].ToString());




                    //有项目需要闪烁的进出港航班实体
                    GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM(drInOutFlight);
                    //行号
                    iRowIndex = Convert.ToInt32(drInOutFlight["cniRowIndex"].ToString());

                    //逐列查找需要闪烁的列名（字段名）
                    for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[1].Columns.Count; iLoop++)
                    {
                        //查找字段信息
                        strSearch = "cnvcDataItemID = '" + fpFlightInfo.Sheets[1].Columns[iLoop].DataField + "'";
                        DataRow[] drDataItem = m_dtDataItems.Select(strSearch);

                        //字段名称
                        string strPrimaryCodeField = drDataItem[0]["cnvcPrimaryCodeField"].ToString();
                        //数据项中文名称
                        string strDataItemName = drDataItem[0]["cnvcDataItemName"].ToString();

                        //如果是航班的进港信息字段
                        if (fpFlightInfo.Sheets[1].Columns[iLoop].DataField.IndexOf("In") == 0)
                        {
                            //查找航班信息
                            strSearch = "cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                                "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                                "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                                "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'";
                        }

                        //如果是航班的出港信息字段
                        else
                        {
                            //查找航班信息
                            strSearch = "cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                                "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                                "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                                "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'";
                        }

                        //判断该字段是否需要闪烁
                        drSplashTag = m_dtSplashTag.Select(strSearch);
                        if (drSplashTag.Length > 0)
                        {
                            //如果用户设置的闪烁时间不为空
                            if (drSplashTag[0][strPrimaryCodeField].ToString().Trim() != "")
                            {
                                //如果用户设置的闪烁时间大于0
                                if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) > 0)
                                {
                                    //设置闪烁：偶数秒该项目底色为红色，奇数秒底色恢复

                                    #region 当前时间的秒数是偶数
                                    //如果是当前时间的秒数是偶数
                                    if (DateTime.Now.Second % 2 == 0)
                                    {
                                        //如果存储在内存中的闪烁表的闪烁时间与用户设置的闪烁时间相同
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            if (fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor != Color.DarkBlue)
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor;
                                            }
                                            else
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = m_cOldBackGroudColor;
                                            }
                                        }

                                        //单元格颜色设为红色
                                        fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor = Color.Red;

                                        //设置列标题闪烁
                                        //如果列标题为两行
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[1, iLoop].BackColor = Color.Red;
                                        }
                                        else
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[0, iLoop].BackColor = Color.Red;
                                        }

                                        //如果用户设置自动停止闪烁，或是设置了闪烁持续时间
                                        if (m_accountBM.SplashAutoStop == 1 || Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            //闪烁持续时间递减一秒
                                            drSplashTag[0][strPrimaryCodeField] = Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) - 1;
                                        }

                                        //如果闪烁持续时间为0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //恢复单元格原来的颜色
                                            fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                            //恢复列标题的原来的颜色
                                            if (strDataItemName.IndexOf("|") >= 0)
                                            {
                                                fpFlightInfo.Sheets[1].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                            }
                                            else
                                            {
                                                fpFlightInfo.Sheets[1].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                            }
                                        }
                                    }
                                    #endregion

                                    #region 当前时间的秒数是奇数
                                    //如果是当前时间的秒数是奇数
                                    else if (DateTime.Now.Second % 2 == 1)
                                    {
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            if (fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor != Color.DarkBlue)
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor;
                                            }
                                            else
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = m_cOldBackGroudColor;
                                            }
                                        }

                                        //恢复到单元格原来的颜色
                                        fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                        //列标题
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                        }
                                        else
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                        }

                                        //闪烁持续时间递减一秒
                                        if (m_accountBM.SplashAutoStop == 1 || Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            drSplashTag[0][strPrimaryCodeField] = Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) - 1;
                                        }

                                        //如果持续时间为0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //恢复原来的颜色
                                            fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                            if (strDataItemName.IndexOf("|") >= 0)
                                            {
                                                fpFlightInfo.Sheets[1].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                            }
                                            else
                                            {
                                                fpFlightInfo.Sheets[1].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                            }
                                        }//end if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                    }//end else if (DateTime.Now.Second % 2 == 1)
                                    #endregion

                                }//end if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) > 0)
                            }//end if (drSplashTag[0][strPrimaryCodeField].ToString().Trim() != "")
                        }//end if (drSplashTag.Length > 0)
                    }//end for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[1].Columns.Count; iLoop++)
                }//end foreach (DataRow drInOutFlight in drInOutFlights)
            }//end try
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 重新设置进出港航班显示值
        /// <summary>
        /// 重新设置进出港航班显示值
        /// </summary>
        /// <param name="dataRow"></param>
        private void SetInOutFlightDataRowValue(DataRow dataRow)
        {
            int iInFlight = 0;
            int iOutFlight = 0;
            IEnumerator ieTodayInOutFlights = m_ilTodayInOutFlights.GetEnumerator();

            GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM();
            while (ieTodayInOutFlights.MoveNext())
            {
                guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current;
                if (guaranteeInforBM["IncncDATOP"] == dataRow["cncDATOP"].ToString() &&
                    guaranteeInforBM["IncnvcFLTID"] == dataRow["cnvcFLTID"].ToString() &&
                    guaranteeInforBM["IncniLEGNO"] == dataRow["cniLEGNO"].ToString() &&
                    guaranteeInforBM["IncnvcAC"] == dataRow["cnvcAC"].ToString())
                {
                    iInFlight = 1;
                    break;
                }
                else if (guaranteeInforBM["OutcncDATOP"] == dataRow["cncDATOP"].ToString() &&
                    guaranteeInforBM["OutcnvcFLTID"] == dataRow["cnvcFLTID"].ToString() &&
                    guaranteeInforBM["OutcniLEGNO"] == dataRow["cniLEGNO"].ToString() &&
                    guaranteeInforBM["OutcnvcAC"] == dataRow["cnvcAC"].ToString())
                {
                    iOutFlight = 1;
                    break;
                }
            }

            if (iInFlight == 1)
            {
                //主键和到达信息
                guaranteeInforBM["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                guaranteeInforBM["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                guaranteeInforBM["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                guaranteeInforBM["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                guaranteeInforBM["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();

                foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                {
                    //格式化特殊字段
                    string strFieldValue = FormatINItem(dataRow, dataRowItems);

                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                    {
                        guaranteeInforBM[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }
            }
            else if (iOutFlight == 1)
            {
                guaranteeInforBM["OutcncDATOP"] = dataRow["cncDATOP"].ToString();
                guaranteeInforBM["OutcnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                guaranteeInforBM["OutcniLEGNO"] = dataRow["cniLEGNO"].ToString();
                guaranteeInforBM["OutcnvcAC"] = dataRow["cnvcAC"].ToString();
                guaranteeInforBM["OutcncAllTOFF"] = dataRow["cncTOFF"].ToString();

                foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                {
                    //格式化特殊字段
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        guaranteeInforBM[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }
            }
        }
        #endregion

        #region 获取变更数据
        public void GetChangeDate(object state)
        {
            //调用业务外观层方法
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            lock (this)
            {
                if (m_accountBM.RefreshInterval == 0)
                {
                    return;
                }

                try
                {
                    Monitor.Enter(oMutexChangeRecords);
                    ReturnValueSF rvSF = changeRecordBF.GetLastGuaranteeChangeRecords(m_iLastRecordNo, GetDateTimeBM(1), m_stationBM, m_accountBM);
                    if (rvSF.Result > 0)
                    {
                        if (rvSF.Dt.Rows.Count > 0)
                        {
                            if (m_dtChangeTable.Columns.Count == 0)
                            {
                                m_dtChangeTable = rvSF.Dt.Clone();
                            }
                            foreach (DataRow dataRow in rvSF.Dt.Rows)
                            {
                                m_dtChangeTable.Rows.Add(dataRow.ItemArray);
                                m_iLastRecordNo = Convert.ToInt32(dataRow["cniRecordNo"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    Monitor.Exit(oMutexChangeRecords);
                }
            }
        }
        #endregion

        #region 定时获取航班变更操作
        /// <summary>
        /// 定时获取航班变更操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerChange_Tick(object sender, EventArgs e)
        {
            //视图刷新标记
            int iRefresh = 0;

            try
            {
                //调用多线程互斥对象，则定时刷新时不再通过GetChangeDate(object state)方法定时获取变更信息
                Monitor.Enter(oMutexChangeRecords);
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();

                //是否需要刷新视图
                DataRow[] drChangeRecords = m_dtChangeTable.Select("cniRefresh = 1");

                #region 需要重新组织视图
                //需要重新组织视图
                if (drChangeRecords.Length > 0)
                {
                    iRefresh = 1;
                    timerSplash.Enabled = false;

                    //上次变更从数据库中提取的航班动态实体对象
                    ChangeLegsBM previousChangeLegsBM = new ChangeLegsBM();
                    DataTable dtChangeLegs = new DataTable();

                    //逐行处理变更记录
                    foreach (DataRow dataRow in m_dtChangeTable.Rows)
                    {
                        //本次变更实体对象
                        ChangeLegsBM changeLegsBM = new ChangeLegsBM();

                        //根据变更前的数据生成变更实体
                        //对关键字赋值
                        changeLegsBM.DATOP = dataRow["cncOldDATOP"].ToString();
                        changeLegsBM.FLTID = dataRow["cnvcOldFLTID"].ToString();
                        changeLegsBM.LEGNO = Convert.ToInt32(dataRow["cniOldLEGNO"].ToString());
                        changeLegsBM.AC = dataRow["cnvcOldAC"].ToString();

                        //根据变更后的数据生成变更实体
                        ChangeLegsBM changeNewKeyLegsBM = new ChangeLegsBM();
                        //对关键字进行赋值
                        if (dataRow["cncNewDATOP"].ToString() != "")
                        {
                            changeNewKeyLegsBM.DATOP = dataRow["cncNewDATOP"].ToString();
                            changeNewKeyLegsBM.FLTID = dataRow["cnvcNewFLTID"].ToString();
                            changeNewKeyLegsBM.LEGNO = Convert.ToInt32(dataRow["cniNewLEGNO"].ToString());
                            changeNewKeyLegsBM.AC = dataRow["cnvcNewAC"].ToString();
                        }
                        else
                        {
                            changeNewKeyLegsBM = changeLegsBM;
                        }

                        //生成查询语句
                        string strSearch = "cncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "' OR " +
                            "cncDATOP = '" + dataRow["cncNewDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + dataRow["cnvcNewFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + dataRow["cniNewLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + dataRow["cnvcNewAC"].ToString() + "'";

                        string strSearchSplashTag = strSearch;

                        //根据本次变更从内存表中查询航班动态和闪烁动态
                        DataRow[] drStationFlights = m_dtTodayStationFlights.Select(strSearch);
                        DataRow[] drSplashTag = m_dtSplashTag.Select(strSearch);

                        //如果获取到相关航班信息And闪烁表中没有该航班的闪烁动态记录
                        if (drStationFlights.Length > 0 && drSplashTag.Length <= 0)
                        {
                            //生成一条信息的闪烁动态记录
                            DataRow drTempSplashTag = m_dtSplashTag.NewRow();
                            drTempSplashTag["cncDATOP"] = drStationFlights[0]["cncDATOP"].ToString();
                            drTempSplashTag["cnvcFLTID"] = drStationFlights[0]["cnvcFLTID"].ToString();
                            drTempSplashTag["cniLEGNO"] = drStationFlights[0]["cniLEGNO"].ToString();
                            drTempSplashTag["cnvcAC"] = drStationFlights[0]["cnvcAC"].ToString();
                            m_dtSplashTag.Rows.Add(drTempSplashTag);

                            //将该条记录加入查询结果中
                            drSplashTag = m_dtSplashTag.Select(strSearch);
                        }

                        //根据变更后的航班信息查询航班的保障信息
                        #region modified by LinYong in 2015.02.06
                        //dtChangeLegs = guaranteeInforBF.GetFlightByKey(changeNewKeyLegsBM).Dt;
                        dtChangeLegs = guaranteeInforBF.GetFlightByKey_NotCompress(changeNewKeyLegsBM, m_accountBM).Dt;
                        #endregion modified by LinYong in 2015.02.06
                        //变更前的航班信息
                        previousChangeLegsBM = changeLegsBM;

                        #region 航站航班动态有相应的记录
                        //航站航班动态有相应的记录
                        if (drStationFlights.Length > 0)
                        {
                            int iSplash = 0;

                            #region 根据航班为进港或出港，分别获取是否闪烁
                            //在进港航班中查询
                            string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                            DataRow[] drInFlights = m_dtTodayInOutFlights.Select(strInSearch);

                            //在出港航班中查询
                            string strOutSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                "OutcnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                            //"IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                            DataRow[] drOutFlights = m_dtTodayInOutFlights.Select(strOutSearch);

                            //如果是进港航班的变更信息
                            if (drInFlights.Length > 0)
                            {
                                //根据变更原因
                                strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                //查询涉及到变更的数据项
                                DataRow[] drInDataItem = m_dtDataItems.Select(strSearch);
                                //如果存在，则闪烁标记设为1
                                if (drInDataItem.Length > 0)
                                {
                                    iSplash = 1;
                                }
                            }

                            //如果是出港航班的变更信息
                            if (drOutFlights.Length > 0)
                            {
                                //根据变更原因
                                strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                //查询涉及到变更的数据项
                                DataRow[] drOutDataItem = m_dtDataItems.Select(strSearch);
                                //如果存在，则闪烁标记设为1
                                if (drOutDataItem.Length > 0)
                                {
                                    iSplash = 1;
                                }
                            }
                            #endregion

                            //判断是否闪烁提示
                            strSearch = "cnvcPrimaryCodeField = '" + dataRow["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
                            DataRow[] drDataItemSplash = m_dtDataItemPurview.Select(strSearch);

                            //如果变更不是删除航班AND根据变更后的航班关键字查询航班保障信息结果不为空
                            if (dataRow["cncActionTag"].ToString() != "D" && dtChangeLegs.Rows.Count > 0)
                            {
                                //用变更后的数据更新该航班的保障信息
                                drStationFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;

                                //新加代码，确保该航班的闪烁信息的记录的航班关键信息为最新变更信息 -- modified by linyong in 2011.08.10 
                                drSplashTag[0]["cncDATOP"] = dtChangeLegs.Rows[0]["cncDATOP"].ToString();
                                drSplashTag[0]["cnvcFLTID"] = dtChangeLegs.Rows[0]["cnvcFLTID"].ToString();
                                drSplashTag[0]["cniLEGNO"] = Convert.ToInt32(dtChangeLegs.Rows[0]["cniLEGNO"].ToString());
                                drSplashTag[0]["cnvcAC"] = dtChangeLegs.Rows[0]["cnvcAC"].ToString();


                                #region 设置闪烁时间
                                //如果需要闪烁提示
                                if (drDataItemSplash.Length > 0)
                                {
                                    //如果变更的数据项为ETA或者ETD
                                    if (dataRow["cnvcChangeReasonCode"].ToString() == "cncETA" || dataRow["cnvcChangeReasonCode"].ToString() == "cncETD")
                                    {
                                        //如果延误原因为空AND闪烁标记=1
                                        if (dtChangeLegs.Rows[0]["cnvcDELAY1"].ToString() != "" && iSplash == 1)
                                        {
                                            //设置闪烁时间
                                            drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds.ToString();
                                        }
                                    }
                                    //如果闪烁标记=1
                                    else if (iSplash == 1)
                                    {
                                        //如果变更的数据项为航班状态
                                        if (dataRow["cnvcChangeReasonCode"].ToString() == "cncSTATUS")
                                        {
                                            //如果航班状态变为DEP
                                            if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEP")
                                            {
                                                //航班起飞时间和落地时间单元格也闪烁
                                                drSplashTag[0]["cncTDWN"] = m_accountBM.SplashSeconds;
                                                drSplashTag[0]["cncTOFF"] = m_accountBM.SplashSeconds;
                                            }

                                            //如果航班状态变更为DEL
                                            if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEL")
                                            {
                                                //航班的预计起飞时间和预达时间也闪烁
                                                drSplashTag[0]["cncETD"] = m_accountBM.SplashSeconds;
                                                drSplashTag[0]["cncETA"] = m_accountBM.SplashSeconds;
                                            }
                                        }

                                        //对于其他数据项，设置闪烁时间
                                        drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds;
                                    }
                                }
                                #endregion

                            }
                            //如果变更是删除航班OR根据变更后的航班关键字查询航班保障信息结果为空
                            else if (dataRow["cncActionTag"].ToString() == "D" || dtChangeLegs.Rows.Count == 0)
                            {
                                //从航班动态表和闪烁动态表中将相关记录删除
                                m_dtTodayStationFlights.Rows.Remove(drStationFlights[0]);
                                m_dtSplashTag.Rows.Remove(drSplashTag[0]);
                            }

                            //重新设置排序
                            m_dtTodayStationFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                            m_dtTodayStationFlights = m_dtTodayStationFlights.DefaultView.Table;

                            //计算相应的提示信息
                            ComputeFlightsInfor(m_dtTodayStationFlights);

                            //显示所有航班                    
                            m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

                            //重新绑定
                            fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;
                        }
                        #endregion

                        #region 航站航班动态无相应的记录（新增航班）
                        //航站航班动态无相应的记录
                        else
                        {
                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //插入一行
                                DataRow drFlight = m_dtTodayStationFlights.NewRow();
                                //将变更后的数据插入航班信息表中
                                drFlight.ItemArray = dtChangeLegs.Rows[0].ItemArray;

                                //增加闪烁记录
                                DataRow drSplash = m_dtSplashTag.NewRow();
                                drSplash["cncDATOP"] = drFlight["cncDATOP"].ToString();
                                drSplash["cnvcFLTID"] = drFlight["cnvcFLTID"].ToString();
                                drSplash["cniLEGNO"] = drFlight["cniLEGNO"].ToString();
                                drSplash["cnvcAC"] = drFlight["cnvcAC"].ToString();

                                //重新排序
                                m_dtTodayStationFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                                m_dtTodayStationFlights = m_dtTodayStationFlights.DefaultView.Table;

                                //计算相应的提示信息
                                ComputeFlightsInfor(m_dtTodayStationFlights);

                                //显示所有航班                    
                                m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);
                                //重新绑定
                                fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;

                                #region 根据航班为进港或出港，分别获取是否闪烁
                                int iSplash = 0;

                                string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                    "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                    "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                    "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                                DataRow[] drInFlights = m_dtTodayInOutFlights.Select(strInSearch);

                                //在出港港航班中查询
                                string strOutSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                    "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                    "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                    "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                                DataRow[] drOutFlights = m_dtTodayInOutFlights.Select(strOutSearch);

                                if (drInFlights.Length > 0)
                                {
                                    strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                    DataRow[] drInDataItem = m_dtDataItems.Select(strSearch);

                                    if (drInDataItem.Length > 0)
                                    {
                                        iSplash = 1;
                                    }
                                }

                                if (drOutFlights.Length > 0)
                                {
                                    strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                    DataRow[] drOutDataItem = m_dtDataItems.Select(strSearch);

                                    if (drOutDataItem.Length > 0)
                                    {
                                        iSplash = 1;
                                    }
                                }
                                #endregion

                                //判断是否闪烁提示
                                strSearch = "cnvcPrimaryCodeField = 'cnvcLONG_REG' AND cniSplashPromptItem = 1";
                                DataRow[] drDataItemSplash = m_dtDataItemPurview.Select(strSearch);

                                //设置飞机号单元格闪烁时间
                                if (drDataItemSplash.Length > 0 && iSplash == 1)
                                {
                                    drSplash["cnvcLONG_REG"] = m_accountBM.SplashSeconds.ToString();
                                }

                                //将新纪录添加到航班信息表和闪烁表中
                                m_dtTodayStationFlights.Rows.Add(drFlight);
                                m_dtSplashTag.Rows.Add(drSplash);
                            }
                        }
                        #endregion
                    }

                    //进出港航班数
                    SetInOutFlightsNum();

                    colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];

                    //初始化表格颜色
                    InitialGridColor(shToday);

                    //设置表格颜色
                    SetGridColor(1, m_dtTodayStationFlights);

                    timerSplash.Enabled = true;
                }
                #endregion

                #region 不需要重新组织视图
                //不需要重新组织视图
                else
                {
                    DataTable dtChangeLegs = new DataTable();
                    ChangeLegsBM previousChangeLegsBM = new ChangeLegsBM();

                    //逐条处理变更记录
                    foreach (DataRow dataRow in m_dtChangeTable.Rows)
                    {
                        //根据变更前的数据生成变更实体
                        ChangeLegsBM changeLegsBM = new ChangeLegsBM();
                        changeLegsBM.DATOP = dataRow["cncOldDATOP"].ToString();
                        changeLegsBM.FLTID = dataRow["cnvcOldFLTID"].ToString();
                        changeLegsBM.LEGNO = Convert.ToInt32(dataRow["cniOldLEGNO"].ToString());
                        changeLegsBM.AC = dataRow["cnvcOldAC"].ToString();

                        //查询语句
                        string strSearch = "cncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                        //查询涉及变更的航班和闪烁信息
                        DataRow[] drPositionFlights = m_dtTodayStationFlights.Select(strSearch);
                        DataRow[] drSplashTag = m_dtSplashTag.Select(strSearch);

                        if (!(changeLegsBM.DATOP == previousChangeLegsBM.DATOP && changeLegsBM.FLTID == previousChangeLegsBM.FLTID && changeLegsBM.LEGNO == previousChangeLegsBM.LEGNO && changeLegsBM.AC == previousChangeLegsBM.AC))
                        {
                            //根据变更前的航班信息查询航班的保障信息
                            #region modified by LinYong in 2015.02.06
                            //dtChangeLegs = guaranteeInforBF.GetFlightByKey(changeLegsBM).Dt;
                            dtChangeLegs = guaranteeInforBF.GetFlightByKey_NotCompress(changeLegsBM, m_accountBM).Dt;
                            #endregion modified by LinYong in 2015.02.06

                            previousChangeLegsBM = changeLegsBM;
                        }

                        #region 根据航班为进港或出港，分别获取是否闪烁
                        int iSplash = 0;
                        //在进港航班中查询
                        string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                        DataRow[] drInFlights = m_dtTodayInOutFlights.Select(strInSearch);

                        string strOutSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                        DataRow[] drOutFlights = m_dtTodayInOutFlights.Select(strOutSearch);

                        if (drInFlights.Length > 0)
                        {
                            strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                            DataRow[] drInDataItem = m_dtDataItems.Select(strSearch);
                            if (drInDataItem.Length > 0)
                            {
                                iSplash = 1;
                            }
                        }

                        if (drOutFlights.Length > 0)
                        {
                            strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                            DataRow[] drOutDataItem = m_dtDataItems.Select(strSearch);

                            if (drOutDataItem.Length > 0)
                            {
                                iSplash = 1;
                            }
                        }
                        #endregion

                        #region 设置闪烁时间
                        //有相应的记录
                        if (drPositionFlights.Length > 0)
                        {
                            //判断是否闪烁提示
                            strSearch = "cnvcPrimaryCodeField = '" + dataRow["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
                            DataRow[] drDataItemSplash = m_dtDataItemPurview.Select(strSearch);

                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //更新航班信息
                                drPositionFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                //重新设置显示
                                SetInOutFlightDataRowValue(drPositionFlights[0]);
                                //设置闪烁时间
                                if (drDataItemSplash.Length > 0 && iSplash == 1)
                                {
                                    drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds;
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                //将变更记录添加到变更显示表中
                if (m_dtChangeTable.Rows.Count != 0)
                {
                    AddChangeDataToList(m_dtChangeTable, 0);
                }

                if (iRefresh == 0)
                {
                    //计算相应的提示信息
                    ComputeFlightsInfor(m_dtTodayStationFlights);

                    //设置表格颜色
                    SetGridColor(1, m_dtTodayStationFlights);
                }

                //清空变更记录
                m_dtChangeTable.Rows.Clear();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //释放互斥对象
                Monitor.Exit(oMutexChangeRecords);
            }

            if (m_iAutoAdjust == 1)
            {
                AdjustScreen();
            }
        }
        #endregion

        #region 闪烁定时器事件
        /// <summary>
        /// 闪烁定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSplash_Tick(object sender, EventArgs e)
        {
            Splash();
            //每天系统在北京时间 5点自动刷新航班动态
            if (DateTime.UtcNow.Hour == 21 && DateTime.UtcNow.Minute == 0 && DateTime.UtcNow.Second == 10)
            {
                FlightRefresh();
            }
        }
        #endregion

        #region 获取选中的进出港航班动态实体对象
        /// <summary>
        /// 获取选中的进出港航班动态实体对象
        /// </summary>
        /// <param name="iRow">选中的行号</param>
        private void GetInOutChangeLegsBM(int iRow)
        {
            GuaranteeInforBM selectGuaranteeInforBM = new GuaranteeInforBM();
            DataTable dtStationFlights = new DataTable();

            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                selectGuaranteeInforBM = (GuaranteeInforBM)(m_ilYesterdayInOutFlights as ArrayList)[iRow];
                dtStationFlights = m_dtYesterdayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                selectGuaranteeInforBM = (GuaranteeInforBM)(m_ilTodayInOutFlights as ArrayList)[iRow];
                dtStationFlights = m_dtTodayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                selectGuaranteeInforBM = (GuaranteeInforBM)(m_ilTomorrowInOutFlights as ArrayList)[iRow];
                dtStationFlights = m_dtTomorrowStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                selectGuaranteeInforBM = (GuaranteeInforBM)(m_ilSelectDateInOutFlights as ArrayList)[iRow];
                dtStationFlights = m_dtSelectDateStationFlights;
            }

            //进港航班信息
            inChangeLegsBM.DATOP = selectGuaranteeInforBM.IncncDATOP;
            inChangeLegsBM.FLTID = selectGuaranteeInforBM.IncnvcFLTID;
            inChangeLegsBM.LEGNO = Convert.ToInt32(selectGuaranteeInforBM.IncniLEGNO);
            inChangeLegsBM.AC = selectGuaranteeInforBM.IncnvcAC;

            DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + selectGuaranteeInforBM.IncncDATOP + "' AND " +
                       "cnvcFLTID = '" + selectGuaranteeInforBM.IncnvcFLTID + "' AND " +
                       "cniLEGNO = " + selectGuaranteeInforBM.IncniLEGNO + " AND " +
                       "cnvcAC = '" + selectGuaranteeInforBM.IncnvcAC + "'");

            if (dataRowFlight.Length > 0)
            {
                inChangeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                inChangeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                inChangeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                inChangeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                inChangeLegsBM.FlightNo = dataRowFlight[0]["cnvcFlightNo"].ToString();
                inChangeLegsBM.LONG_REG = dataRowFlight[0]["cnvcLONG_REG"].ToString();
                inChangeLegsBM.DEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                inChangeLegsBM.CityDEPSTN = dataRowFlight[0]["cncDEPCityThreeCode"].ToString();
                inChangeLegsBM.DEPFourCode = dataRowFlight[0]["cncDEPAirportFourCode"].ToString();
                inChangeLegsBM.ARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                inChangeLegsBM.CityARRSTN = dataRowFlight[0]["cncARRCityThreeCode"].ToString();
                inChangeLegsBM.ARRFourCode = dataRowFlight[0]["cncARRAirportFourCode"].ToString();
                inChangeLegsBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                inChangeLegsBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                inChangeLegsBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                inChangeLegsBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                inChangeLegsBM.TOFF = dataRowFlight[0]["cncTOFF"].ToString();
                inChangeLegsBM.TDWN = dataRowFlight[0]["cncTDWN"].ToString();
                inChangeLegsBM.STATUS = dataRowFlight[0]["cncSTATUS"].ToString();
                inChangeLegsBM.STC = dataRowFlight[0]["cnvcSTC"].ToString();
                inChangeLegsBM.ACTYP = dataRowFlight[0]["cncACTYP"].ToString();
            }

            dataRowFlight = dtStationFlights.Select("cncDATOP = '" + selectGuaranteeInforBM.OutcncDATOP + "' AND " +
                                   "cnvcFLTID = '" + selectGuaranteeInforBM.OutcnvcFLTID + "' AND " +
                                   "cniLEGNO = " + selectGuaranteeInforBM.OutcniLEGNO + " AND " +
                                   "cnvcAC = '" + selectGuaranteeInforBM.OutcnvcAC + "'");


            //出港航班信息
            outChangeLegsBM.DATOP = selectGuaranteeInforBM.OutcncDATOP;
            outChangeLegsBM.FLTID = selectGuaranteeInforBM.OutcnvcFLTID;
            outChangeLegsBM.LEGNO = Convert.ToInt32(selectGuaranteeInforBM.OutcniLEGNO);
            outChangeLegsBM.AC = selectGuaranteeInforBM.OutcnvcAC;
            if (dataRowFlight.Length > 0)
            {
                outChangeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                outChangeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                outChangeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                outChangeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                outChangeLegsBM.FlightNo = dataRowFlight[0]["cnvcFlightNo"].ToString();
                outChangeLegsBM.LONG_REG = dataRowFlight[0]["cnvcLONG_REG"].ToString();
                outChangeLegsBM.DEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                outChangeLegsBM.CityDEPSTN = dataRowFlight[0]["cncDEPCityThreeCode"].ToString();
                outChangeLegsBM.CityDEPSTN = dataRowFlight[0]["cncDEPCityThreeCode"].ToString();
                outChangeLegsBM.ARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                outChangeLegsBM.CityARRSTN = dataRowFlight[0]["cncARRCityThreeCode"].ToString();
                outChangeLegsBM.ARRFourCode = dataRowFlight[0]["cncARRAirportFourCode"].ToString();
                outChangeLegsBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                outChangeLegsBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                outChangeLegsBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                outChangeLegsBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                outChangeLegsBM.TOFF = dataRowFlight[0]["cncTOFF"].ToString();
                outChangeLegsBM.TDWN = dataRowFlight[0]["cncTDWN"].ToString();
                outChangeLegsBM.STATUS = dataRowFlight[0]["cncSTATUS"].ToString();
                outChangeLegsBM.STC = dataRowFlight[0]["cnvcSTC"].ToString();
                outChangeLegsBM.ACTYP = dataRowFlight[0]["cncACTYP"].ToString();
            }
        }
        #endregion

        #region 单击单元格事件，使闪烁停止
        /// <summary>
        /// 单击单元格事件，使闪烁停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!e.ColumnHeader)
            {
                #region yanxian 2013-10-14;切换界面为变更数据列表
                dataTabControlGanttView.SelectedIndex = 0;
                //intabControl.SelectedIndex = 0; //放在进入 进港航班tab 时处理 -- modified by LinYong in 20160316
                //outtabControl.SelectedIndex = 0; //放在进入 出港航班tab 时处理 -- modified by LinYong in 20160316
                //msgTabPage.Text = "【操作记录】";
                #endregion

                lastInChangeLegsBM = inChangeLegsBM;
                lastInChangeLegsBM = outChangeLegsBM;

                GetInOutChangeLegsBM(e.Row);
                
                #region modified by LinYong in 20150330
                //string strOutTitle = DateTime.Parse(outChangeLegsBM.DATOP).ToString("MM月dd日;") + outChangeLegsBM.FLTID.ToString() + ";" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN;
                //string strInTitle = DateTime.Parse(inChangeLegsBM.DATOP).ToString("MM月dd日;") + inChangeLegsBM.FLTID.ToString() + ";" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN;
                string strInTitle = "";
                string strOutTitle = "";
                #endregion modified by LinYong in 20150330

                if (inChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strInTitle = "无";
                }
                else    //added by LinYong in 20150403
                {
                    strInTitle = DateTime.Parse(inChangeLegsBM.FlightDate).ToString("MM月dd日;") + inChangeLegsBM.FLTID.ToString() + ";" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN;
                }

                if (outChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strOutTitle = "无";
                }
                else    //added by LinYong in 20150403
                {
                    strOutTitle = DateTime.Parse(outChangeLegsBM.FlightDate).ToString("MM月dd日;") + outChangeLegsBM.FLTID.ToString() + ";" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN;

                }

                inTabPage.Text =  "进港航班(" + strInTitle + ")";
                outTabPage.Text =  "出港航班(" +strOutTitle + ")";
            }

            # region 航班监控界面为"今天"时,事件处理
            if (fpFlightInfo.ActiveSheetIndex == 1 && !e.ColumnHeader)
            {
                GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(m_ilTodayInOutFlights as ArrayList)[e.Row];

                string strChangeReasonCode = "";
                string strDataItemName = "";
                string strSearch = "";

                if (fpFlightInfo.Sheets[1].Columns[e.Column].DataField.IndexOf("In") == 0) //进港航班
                {
                    strChangeReasonCode = fpFlightInfo.Sheets[1].Columns[e.Column].DataField.Substring(2);
                    strSearch = "cncDATOP = '" + guaranteeInforBM["IncncDATOP"].ToString() + "' AND " +
                        "cnvcFLTID = '" + guaranteeInforBM["IncnvcFLTID"].ToString() + "' AND " +
                        "cniLEGNO = " + guaranteeInforBM["IncniLEGNO"].ToString() + " AND " +
                        "cnvcAC = '" + guaranteeInforBM["IncnvcAC"].ToString() + "'";
                }
                else
                {
                    strChangeReasonCode = fpFlightInfo.Sheets[1].Columns[e.Column].DataField.Substring(3);
                    strSearch = "cncDATOP = '" + guaranteeInforBM["OutcncDATOP"].ToString() + "' AND " +
                       "cnvcFLTID = '" + guaranteeInforBM["OutcnvcFLTID"].ToString() + "' AND " +
                       "cniLEGNO = " + guaranteeInforBM["OutcniLEGNO"].ToString() + " AND " +
                       "cnvcAC = '" + guaranteeInforBM["OutcnvcAC"].ToString() + "'";
                }

                DataRow[] drDataItems = m_dtDataItems.Select("cnvcPrimaryNameField = '" + strChangeReasonCode + "'");

                if (drDataItems.Length > 0)
                {
                    strChangeReasonCode = drDataItems[0]["cnvcPrimaryCodeField"].ToString();
                    strDataItemName = drDataItems[0]["cnvcDataItemName"].ToString();
                }

                DataRow[] drSplashTag = m_dtSplashTag.Select(strSearch);

                if (drSplashTag.Length > 0)
                {
                    if (strDataItemName.IndexOf("|") >= 0)
                    {
                        fpFlightInfo.Sheets[1].ColumnHeader.Cells[1, e.Column].BackColor = Color.FromName("Control");
                    }
                    else
                    {
                        fpFlightInfo.Sheets[1].ColumnHeader.Cells[0, e.Column].BackColor = Color.FromName("Control");
                    }

                    if (drSplashTag[0][strChangeReasonCode].ToString() != "")
                    {
                        fpFlightInfo.Sheets[1].Cells[e.Row, e.Column].BackColor = colorArrOldBackGround[e.Row, e.Column];
                    }
                    drSplashTag[0][strChangeReasonCode] = "0";
                    //iFirstEnterSplash[e.Row, e.Column] = 1;
                }

                //设置单元格选中颜色
                if (m_iOldSelectedRow != -1)
                {
                    fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;
                    fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;
                }

                m_iOldSelectedRow = e.Row;
                m_iOldSelectedColumn = e.Column;
                m_cOldBackGroudColor = fpFlightInfo.Sheets[1].Cells[e.Row, e.Column].BackColor;
                m_cOldForeColor = fpFlightInfo.Sheets[1].Cells[e.Row, e.Column].ForeColor;
                fpFlightInfo.Sheets[1].Cells[e.Row, e.Column].BackColor = Color.DarkBlue;
                fpFlightInfo.Sheets[1].Cells[e.Row, e.Column].ForeColor = Color.White;

                //selectedGuaranteeInfo = (GuaranteeInforBM) (m_ilTodayInOutFlights as ArrayList)[e.Row];
            }
            # endregion

            #region yanxian 2013-10-14;航班监控界面为"昨天"时,事件处理
            if (fpFlightInfo.ActiveSheetIndex == 0 && !e.ColumnHeader)
            {
                //设置单元格选中颜色
                if (m_iOldSelectedRow != -1)
                {
                    fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;
                    fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;
                }

                m_iOldSelectedRow = e.Row;
                m_iOldSelectedColumn = e.Column;
                m_cOldBackGroudColor = fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].BackColor;
                m_cOldForeColor = fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].ForeColor;
                fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].BackColor = Color.DarkBlue;
                fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].ForeColor = Color.White;
            }
            #endregion

            #region yanxian 2013-10-14;航班监控界面为"明天"时,事件处理
            if (fpFlightInfo.ActiveSheetIndex == 2 && !e.ColumnHeader)
            {
                //设置单元格选中颜色
                if (m_iOldSelectedRow != -1)
                {
                    fpFlightInfo.Sheets[2].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;
                    fpFlightInfo.Sheets[2].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;
                }

                m_iOldSelectedRow = e.Row;
                m_iOldSelectedColumn = e.Column;
                m_cOldBackGroudColor = fpFlightInfo.Sheets[2].Cells[e.Row, e.Column].BackColor;
                m_cOldForeColor = fpFlightInfo.Sheets[2].Cells[e.Row, e.Column].ForeColor;
                fpFlightInfo.Sheets[2].Cells[e.Row, e.Column].BackColor = Color.DarkBlue;
                fpFlightInfo.Sheets[2].Cells[e.Row, e.Column].ForeColor = Color.White;
            }
            #endregion

            #region LinYong 2014-4-10;航班监控界面为"选择日期"时,事件处理
            if (fpFlightInfo.ActiveSheetIndex == 3 && !e.ColumnHeader)
            {
                //设置单元格选中颜色
                if (m_iOldSelectedRow != -1)
                {
                    fpFlightInfo.Sheets[3].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;
                    fpFlightInfo.Sheets[3].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;
                }

                m_iOldSelectedRow = e.Row;
                m_iOldSelectedColumn = e.Column;
                m_cOldBackGroudColor = fpFlightInfo.Sheets[3].Cells[e.Row, e.Column].BackColor;
                m_cOldForeColor = fpFlightInfo.Sheets[3].Cells[e.Row, e.Column].ForeColor;
                fpFlightInfo.Sheets[3].Cells[e.Row, e.Column].BackColor = Color.DarkBlue;
                fpFlightInfo.Sheets[3].Cells[e.Row, e.Column].ForeColor = Color.White;
            }
            #endregion

            #region 单击列标题查找航班
            if (e.ColumnHeader)
            {
                if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcFlightNo" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcFlightNo")
                {
                    string strDataField = fpFlightInfo.ActiveSheet.Columns[e.Column].DataField;
                    //查找数据变化项的字段信息
                    DataRow[] drDataItems = m_dtDataItems.Select("cnvcDataItemID = '" + strDataField + "'");

                    DataItemPurviewBM objDataItemInfo = null;

                    if (drDataItems.Length > 0)
                    {
                        objDataItemInfo = new DataItemPurviewBM(drDataItems[0], DataItemPurviewBM.DataType.ITEM);
                    }

                    if (objDataItemInfo == null)
                    {
                        return;
                    }


                    fmQueryFlight objfmQueryFlight = new fmQueryFlight();

                    if (e.X < Screen.PrimaryScreen.WorkingArea.Width - objfmQueryFlight.Width)
                    {
                        objfmQueryFlight.Left = e.X + 20;
                    }
                    else
                    {
                        objfmQueryFlight.Left = e.X - objfmQueryFlight.Width - 20;
                    }
                    if (e.Y < Screen.PrimaryScreen.WorkingArea.Height - objfmQueryFlight.Height)
                    {
                        objfmQueryFlight.Top = e.Y + objfmQueryFlight.Height / 2 - 5;
                    }
                    else
                    {
                        objfmQueryFlight.Top = e.Y - objfmQueryFlight.Height / 2 + 5;
                    }
                    objfmQueryFlight.Text += objDataItemInfo.DataItemName;

                    objfmQueryFlight.ShowDialog();

                    if (!objfmQueryFlight.Find)
                    {
                        return;
                    }

                    //当前显示的数据列表
                    IList ilFlightData;
                    if (fpFlightInfo.ActiveSheetIndex == 0)
                    {
                        ilFlightData = m_ilYesterdayInOutFlights;
                    }
                    else if (fpFlightInfo.ActiveSheetIndex == 1)
                    {
                        ilFlightData = m_ilTodayInOutFlights;
                    }
                    else if (fpFlightInfo.ActiveSheetIndex == 2)
                    {
                        ilFlightData = m_ilTomorrowInOutFlights;
                    }
                    else if (fpFlightInfo.ActiveSheetIndex == 3)
                    {
                        ilFlightData = m_ilSelectDateInOutFlights;
                    }
                    else
                    {
                        ilFlightData = null;
                    }



                    //查找选中的记录
                    IEnumerator ieFlightData = ilFlightData.GetEnumerator();
                    GuaranteeInforBM findGuaranteeInfo = null;
                    int iRowIndex = 0;
                    //根据飞机号查找
                    if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcLONG_REG")
                    {
                        iRowIndex = -1;

                        while (ieFlightData.MoveNext())
                        {
                            iRowIndex += 1;
                            findGuaranteeInfo = (GuaranteeInforBM)ieFlightData.Current;
                            if (findGuaranteeInfo.IncnvcLONG_REG != null && findGuaranteeInfo.IncnvcLONG_REG.IndexOf(objfmQueryFlight.FindContent) >= 0 || findGuaranteeInfo.OutcnvcLONG_REG != null && findGuaranteeInfo.OutcnvcLONG_REG.IndexOf(objfmQueryFlight.FindContent) >= 0)
                            {
                                fpFlightInfo.ActiveSheet.AddSelection(iRowIndex, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                                fpFlightInfo.ShowRow(0, iRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                                GetInOutChangeLegsBM(iRowIndex);
                                break;
                            }
                            findGuaranteeInfo = null;

                        }
                    }
                    else   //根据航班号查找
                    {
                        iRowIndex = -1;
                        while (ieFlightData.MoveNext())
                        {
                            iRowIndex += 1;
                            findGuaranteeInfo = (GuaranteeInforBM)ieFlightData.Current;
                            if (findGuaranteeInfo.IncnvcFlightNo != null && findGuaranteeInfo.IncnvcFlightNo.IndexOf(objfmQueryFlight.FindContent) >= 0 || findGuaranteeInfo.OutcnvcFlightNo != null && findGuaranteeInfo.OutcnvcFlightNo.IndexOf(objfmQueryFlight.FindContent) >= 0)
                            {
                                fpFlightInfo.ActiveSheet.AddSelection(iRowIndex, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                                fpFlightInfo.ShowRow(0, iRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                                GetInOutChangeLegsBM(iRowIndex);
                                break;
                            }
                            findGuaranteeInfo = null;

                        }
                    }

                    if (findGuaranteeInfo != null && fpFlightInfo.ActiveSheetIndex == 1 && m_iOldSelectedRow != -1)
                    {
                        if (m_iOldSelectedRow != -1)
                        {
                            fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;

                            fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;
                        }

                        m_iOldSelectedRow = iRowIndex;
                        m_cOldBackGroudColor = fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor;
                        m_cOldForeColor = fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor;

                        //设置成新的选中的颜色
                        fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                        fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor = Color.White;

                    }

                }
            }
            #endregion

            #region 弹出右键菜单
            if (e.Button == MouseButtons.Right)
            {
                m_iRightButtonRow = e.Row;
                fpFlightInfo.ActiveSheet.AddSelection(e.Row, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                Point p = new Point(e.X, e.Y);
                if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcLONG_REG")
                {

                }
                else if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcFlightNo")
                {
                    popInFlightNoMenu.Show(fpFlightInfo, p);
                }
                else if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcFlightNo")
                {
                    popOutFlightNoMenu.Show(fpFlightInfo, p);
                }

            }
            #endregion
        }
        #endregion

        #region 根据航班号查找航班
        /// <summary>
        /// 根据航班号查找航班
        /// </summary>
        public void FindFlightByFlightNo(string strFlightNo)
        {
            //当前显示的数据列表
            IList ilFlightData;
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                ilFlightData = m_ilYesterdayInOutFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                ilFlightData = m_ilTodayInOutFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                ilFlightData = m_ilTomorrowInOutFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                ilFlightData = m_ilSelectDateInOutFlights;
            }
            else
            {
                ilFlightData = null;
            }

            int iRowIndex = -1;
            //查找选中的记录
            IEnumerator ieFlightData = ilFlightData.GetEnumerator();
            GuaranteeInforBM findGuaranteeInfo = null;

            while (ieFlightData.MoveNext())
            {
                iRowIndex += 1;
                findGuaranteeInfo = (GuaranteeInforBM)ieFlightData.Current;
                if (findGuaranteeInfo.IncnvcFlightNo != null && findGuaranteeInfo.IncnvcFlightNo.IndexOf(strFlightNo) >= 0 || findGuaranteeInfo.OutcnvcFlightNo != null && findGuaranteeInfo.OutcnvcFlightNo.IndexOf(strFlightNo) >= 0)
                {
                    fpFlightInfo.ActiveSheet.AddSelection(iRowIndex, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                    fpFlightInfo.ShowRow(0, iRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                    GetInOutChangeLegsBM(iRowIndex);
                    break;
                }

                findGuaranteeInfo = null;
            }

            if (findGuaranteeInfo != null && fpFlightInfo.ActiveSheetIndex == 1 && m_iOldSelectedRow != -1)
            {
                if (m_iOldSelectedRow != -1)
                {
                    fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;

                    fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;
                }
                m_iOldSelectedRow = iRowIndex;
                m_cOldBackGroudColor = fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor;
                m_cOldForeColor = fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor;

                //设置成新的选中的颜色
                fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor = Color.White;

            }
        }
        #endregion

        #region 刷新航班动态
        /// <summary>
        /// 刷新航班动态
        /// </summary>
        public void FlightRefresh()
        {
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                //获取该航站当天的所有航班
                m_dtYesterdayStationFlights = GetStationFlights(GetDateTimeBM(0), m_stationBM, 0);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtYesterdayStationFlights);

                //获取当天进出港航班
                m_ilYesterdayInOutFlights = FillInOutFlights(m_dtYesterdayStationFlights, 0);

                fpFlightInfo.Sheets[0].DataSource = m_ilYesterdayInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();

                //初始化表格颜色
                InitialGridColor(shYestoday);

                //设置当天单元格颜色
                SetGridColor(0, m_dtYesterdayStationFlights);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                //获取最大变更序号和最后100条变更数据
                GetMaxRecordNo();

                //获取该航站当天的所有航班
                m_dtTodayStationFlights = GetStationFlights(GetDateTimeBM(1), m_stationBM, 1);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtTodayStationFlights);


                //进出港航班表格Schema
                m_dtInOutFlightsSchema = GetDisplaySchema();

                //获取当天进出港航班
                m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

                fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();

                //获取变更记录
                GetChangeData();

                //记录闪烁单元格背景色和是否第一次进入闪烁
                colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];
                //iFirstEnterSplash = new int[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];
                //for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[1].Rows.Count; iLoop++)
                //{
                //    for (int jLoop = 0; jLoop < fpFlightInfo.Sheets[1].Columns.Count; jLoop++)
                //    {
                //        iFirstEnterSplash[iLoop, jLoop] = 1;
                //    }
                //}

                //初始化表格颜色
                InitialGridColor(shToday);

                //设置当天单元格颜色
                SetGridColor(1, m_dtTodayStationFlights);

                fpFlightInfo.ActiveSheetIndex = 1;
                fpFlightInfo.Sheets[3].SheetName = dtFlightDate.Value.ToString("yyyy-MM-dd");

                if (m_iOldSelectedRow >= FpFlightInfo.Sheets[1].RowCount)
                {
                    m_iOldSelectedRow = -1;
                }
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                //获取该航站当天的所有航班
                m_dtTomorrowStationFlights = GetStationFlights(GetDateTimeBM(2), m_stationBM, 2);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtTomorrowStationFlights);

                //获取当天进出港航班
                m_ilTomorrowInOutFlights = FillInOutFlights(m_dtTomorrowStationFlights, 0);

                fpFlightInfo.Sheets[2].DataSource = m_ilTomorrowInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();

                //初始化表格颜色
                InitialGridColor(shTomorrow);

                //设置当天单元格颜色
                SetGridColor(2, m_dtTomorrowStationFlights);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                //获取该航站当天的所有航班
                m_dtSelectDateStationFlights = GetStationFlights(GetDateTimeBM(3), m_stationBM, 3);

                //计算相应的提示信息
                ComputeFlightsInfor(m_dtSelectDateStationFlights);

                //获取当天进出港航班
                m_ilSelectDateInOutFlights = FillInOutFlights(m_dtSelectDateStationFlights, 0);

                fpFlightInfo.Sheets[3].DataSource = m_ilSelectDateInOutFlights;
                //进出港航班数
                SetInOutFlightsNum();
            }

        }
        #endregion

        #region 双击单元格事件
        /// <summary>
        /// 双击单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                if (fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].BackColor == Color.DarkBlue)
                {
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].BackColor = m_cOldBackGroudColor;
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].ForeColor = m_cOldForeColor;
                }
            }
            Maintennance(e);
        }
        #endregion

        #region 维护相关数据项数据
        private void Maintennance(FarPoint.Win.Spread.CellClickEventArgs e)
        {
            IList ilInOutFlights = new ArrayList();
            DataTable dtStationFlights = new DataTable();

            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                ilInOutFlights = m_ilYesterdayInOutFlights;
                dtStationFlights = m_dtYesterdayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                ilInOutFlights = m_ilTodayInOutFlights;
                dtStationFlights = m_dtTodayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                ilInOutFlights = m_ilTomorrowInOutFlights;
                dtStationFlights = m_dtTomorrowStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                ilInOutFlights = m_ilSelectDateInOutFlights;
                dtStationFlights = m_dtSelectDateStationFlights;
            }

            //获取被双击单元格所在行的进出港航班实体
            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(ilInOutFlights as ArrayList)[e.Row];

            //列名
            string strDataItemID = fpFlightInfo.ActiveSheet.Columns[e.Column].DataField.ToString();
            //获取用户权限信息
            DataRow[] dataRow = m_dtDataItemPurview.Select("cnvcDataItemID = '" + strDataItemID + "'");
            //判断是否有权限
            if (dataRow[0]["cnvcPrimaryCodeField"].ToString() != "cnbVIPTag")
            {
                if (dataRow[0]["cniDataItemPurview"].ToString() != "2")
                {
                    MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //用户权限级别
            int iDataItemPurview = Convert.ToInt32(dataRow[0]["cniDataItemPurview"].ToString());
            //字段长度
            int iFieldLength = Convert.ToInt32(dataRow[0]["cniFieldLength"].ToString());
            //维护类型
            int iMainTainType = Convert.ToInt32(dataRow[0]["cniMaintenType"].ToString());
            //字段类型：1=文本；2=数字
            int iFieldType = Convert.ToInt32(dataRow[0]["cniFieldType"].ToString());
            //被双击单元格对应tbGuaranteeInfo表中的字段
            string strPrimaryCodeField = dataRow[0]["cnvcPrimaryCodeField"].ToString();

            //数据维护实体
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //变更操作实体
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //航班动态变更实体
            ChangeLegsBM changeLegsBM = new ChangeLegsBM();

            //字段类型
            maintenGuaranteeInforBM.FieldType = iFieldType;
            //变更前单元格的值
            maintenGuaranteeInforBM.OldContent = fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text;
            //列名
            maintenGuaranteeInforBM.ColumnCaption = dataRow[0]["cnvcDataItemName"].ToString();

            #region 维护进港航班数据
            //如果维护的进港航班的数据
            if (strDataItemID.IndexOf("In") == 0 && guaranteeInforBM.IncncDATOP != "1900-01-01")
            {
                //维护信息
                maintenGuaranteeInforBM.DATOP = guaranteeInforBM.IncncDATOP;
                maintenGuaranteeInforBM.FLTID = guaranteeInforBM.IncnvcFLTID;
                maintenGuaranteeInforBM.LEGNO = guaranteeInforBM.IncniLEGNO;
                maintenGuaranteeInforBM.AC = guaranteeInforBM.IncnvcAC;
                maintenGuaranteeInforBM.FieldName = strPrimaryCodeField;
                maintenGuaranteeInforBM.FieldLength = iFieldLength;

                //航班信息
                changeLegsBM.DATOP = guaranteeInforBM.IncncDATOP;
                changeLegsBM.FLTID = guaranteeInforBM.IncnvcFLTID;
                changeLegsBM.LEGNO = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeLegsBM.AC = guaranteeInforBM.IncnvcAC;

                //变更信息
                changeRecordBM.UserID = m_accountBM.UserId;
                changeRecordBM.OldDATOP = guaranteeInforBM.IncncDATOP;
                changeRecordBM.OldFLTID = guaranteeInforBM.IncnvcFLTID;
                changeRecordBM.OldLegNo = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeRecordBM.OldAC = guaranteeInforBM.IncnvcAC;
                changeRecordBM.NewDATOP = guaranteeInforBM.IncncDATOP;
                changeRecordBM.NewFLTID = guaranteeInforBM.IncnvcFLTID;
                changeRecordBM.NewLegNo = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeRecordBM.NewAC = guaranteeInforBM.IncnvcAC;

                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                    "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                    "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                    "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeRecordBM.OldDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.OldArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.NewDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.NewArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeRecordBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeRecordBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeRecordBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeRecordBM.ChangeReasonCode = strPrimaryCodeField;
                    changeRecordBM.ChangeOldContent = fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text;
                    changeRecordBM.ActionTag = "U";
                    changeRecordBM.Refresh = 0;

                    changeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                    changeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                    changeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                    changeLegsBM.DEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeLegsBM.CityDEPSTN = dataRowFlight[0]["cncDEPCityThreeCode"].ToString();
                    changeLegsBM.ARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeLegsBM.CityARRSTN = dataRowFlight[0]["cncARRCityThreeCode"].ToString();
                    changeLegsBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeLegsBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeLegsBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeLegsBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeLegsBM.TOFF = dataRowFlight[0]["cncTOFF"].ToString();
                    changeLegsBM.TDWN = dataRowFlight[0]["cncTDWN"].ToString();
                    changeLegsBM.STATUS = dataRowFlight[0]["cncSTATUS"].ToString();
                    changeLegsBM.ACTYP = dataRowFlight[0]["cncACTYP"].ToString();
                }
            }
            #endregion

            #region 维护出港航班的数据
            //出港航班或进港停机位
            else if (strDataItemID.IndexOf("Out") == 0 && guaranteeInforBM.OutcncDATOP != "1900-01-01" || strDataItemID == "IncnvcInGATE")
            {
                //维护信息
                maintenGuaranteeInforBM.DATOP = guaranteeInforBM.OutcncDATOP;
                maintenGuaranteeInforBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                maintenGuaranteeInforBM.LEGNO = guaranteeInforBM.OutcniLEGNO;
                maintenGuaranteeInforBM.AC = guaranteeInforBM.OutcnvcAC;
                maintenGuaranteeInforBM.FieldName = strPrimaryCodeField;
                maintenGuaranteeInforBM.FieldLength = iFieldLength;

                //航班信息
                changeLegsBM.DATOP = guaranteeInforBM.OutcncDATOP;
                changeLegsBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                changeLegsBM.LEGNO = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeLegsBM.AC = guaranteeInforBM.OutcnvcAC;

                //变更信息
                changeRecordBM.UserID = m_accountBM.UserId;
                changeRecordBM.OldDATOP = guaranteeInforBM.OutcncDATOP;
                changeRecordBM.OldFLTID = guaranteeInforBM.OutcnvcFLTID;
                changeRecordBM.OldLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeRecordBM.OldAC = guaranteeInforBM.OutcnvcAC;
                changeRecordBM.NewDATOP = guaranteeInforBM.OutcncDATOP;
                changeRecordBM.NewFLTID = guaranteeInforBM.OutcnvcFLTID;
                changeRecordBM.NewLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeRecordBM.NewAC = guaranteeInforBM.OutcnvcAC;

                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                   "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                   "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                   "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeRecordBM.OldDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.OldArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.NewDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.NewArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeRecordBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeRecordBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeRecordBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeRecordBM.ChangeReasonCode = strPrimaryCodeField;
                    changeRecordBM.ChangeOldContent = fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text;
                    changeRecordBM.ActionTag = "U";
                    changeRecordBM.Refresh = 0;

                    changeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                    changeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                    changeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                    changeLegsBM.DEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeLegsBM.CityDEPSTN = dataRowFlight[0]["cncDEPCityThreeCode"].ToString();
                    changeLegsBM.ARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeLegsBM.CityARRSTN = dataRowFlight[0]["cncARRCityThreeCode"].ToString();
                    changeLegsBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeLegsBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeLegsBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeLegsBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeLegsBM.TOFF = dataRowFlight[0]["cncTOFF"].ToString();
                    changeLegsBM.TDWN = dataRowFlight[0]["cncTDWN"].ToString();
                    changeLegsBM.STATUS = dataRowFlight[0]["cncSTATUS"].ToString();
                    changeLegsBM.ACTYP = dataRowFlight[0]["cncACTYP"].ToString();
                }
            }
            #endregion

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
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTime.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //单行文本：维护类型=2
            else if (iMainTainType == 2)
            {
                fmMaitenSingleText objfmMaitenSingleText = new fmMaitenSingleText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaitenSingleText.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaitenSingleText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //多行文本：维护类型=3
            else if (iMainTainType == 3)
            {
                fmMaintenMutiLineText objfmMaintenMutiLineText = new fmMaintenMutiLineText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenMutiLineText.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenMutiLineText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //下拉列表：维护类型=4
            else if (iMainTainType == 4)
            {
                //fmMaintenList objfmMaintenList = new fmMaintenList(maintenGuaranteeInforBM, changeRecordBM, m_stationBM); --modified by LinYong in 20160617
                fmMaintenList objfmMaintenList = new fmMaintenList(maintenGuaranteeInforBM, changeRecordBM, m_stationBM, m_accountBM);

                if (objfmMaintenList.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenList.MMaintenGuaranteeInforBM.NewText;
                }
            }
            else
            {
                #region 除以上4种类型以外的特殊类型
                //VIP标记
                if (strPrimaryCodeField == "cnbVIPTag")
                {
                    fmMaintenVIP objfmMaintenVIP = new fmMaintenVIP(changeLegsBM, m_accountBM, iDataItemPurview);
                    objfmMaintenVIP.ShowDialog();
                }
                //进港机位
                else if (strPrimaryCodeField == "cnvcInGATE")
                {
                    MaintenGuaranteeInforBM outMaintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                    ChangeRecordBM outChangeRecordBM = new ChangeRecordBM();

                    int iOut = 0;

                    //如果进港航班后有衔接的出港航班
                    if (guaranteeInforBM.OutcncDATOP != "1900-01-01")
                    {
                        //出港航班维护信息
                        outMaintenGuaranteeInforBM.DATOP = guaranteeInforBM.OutcncDATOP;
                        outMaintenGuaranteeInforBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                        outMaintenGuaranteeInforBM.LEGNO = guaranteeInforBM.OutcniLEGNO;
                        outMaintenGuaranteeInforBM.AC = guaranteeInforBM.OutcnvcAC;
                        outMaintenGuaranteeInforBM.FieldName = "cnvcOutGate";
                        outMaintenGuaranteeInforBM.FieldLength = iFieldLength;
                        outMaintenGuaranteeInforBM.FieldType = 1;

                        //出港航班变更信息
                        outChangeRecordBM.UserID = m_accountBM.UserId;
                        outChangeRecordBM.OldDATOP = guaranteeInforBM.OutcncDATOP;
                        outChangeRecordBM.OldFLTID = guaranteeInforBM.OutcnvcFLTID;
                        outChangeRecordBM.OldLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                        outChangeRecordBM.OldAC = guaranteeInforBM.OutcnvcAC;
                        outChangeRecordBM.NewDATOP = guaranteeInforBM.OutcncDATOP;
                        outChangeRecordBM.NewFLTID = guaranteeInforBM.OutcnvcFLTID;
                        outChangeRecordBM.NewLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                        outChangeRecordBM.NewAC = guaranteeInforBM.OutcnvcAC;

                        DataRow[] dataRowOutGateFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                           "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                           "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                           "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                        if (dataRowOutGateFlight.Length > 0)
                        {
                            outChangeRecordBM.OldDepSTN = dataRowOutGateFlight[0]["cncDEPSTN"].ToString();
                            outChangeRecordBM.OldArrSTN = dataRowOutGateFlight[0]["cncARRSTN"].ToString();
                            outChangeRecordBM.NewDepSTN = dataRowOutGateFlight[0]["cncDEPSTN"].ToString();
                            outChangeRecordBM.NewArrSTN = dataRowOutGateFlight[0]["cncARRSTN"].ToString();
                            outChangeRecordBM.STD = dataRowOutGateFlight[0]["cncSTD"].ToString();
                            outChangeRecordBM.ETD = dataRowOutGateFlight[0]["cncETD"].ToString();
                            outChangeRecordBM.STA = dataRowOutGateFlight[0]["cncSTA"].ToString();
                            outChangeRecordBM.ETA = dataRowOutGateFlight[0]["cncETA"].ToString();
                            outChangeRecordBM.ChangeReasonCode = "cnvcOutGate";
                            outChangeRecordBM.ChangeOldContent = fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text;
                            outChangeRecordBM.ActionTag = "U";
                            outChangeRecordBM.Refresh = 0;
                        }
                        //设置标记，标识有衔接出港航班
                        iOut = 1;
                    }

                    fmMaintenInGate objfmMaintenInGate = new fmMaintenInGate(maintenGuaranteeInforBM, outMaintenGuaranteeInforBM, changeRecordBM, outChangeRecordBM, iOut);
                    if (objfmMaintenInGate.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenInGate.MMaintenGuaranteeInforBM.NewContent;
                    }
                }
                //值机数据
                else if (strPrimaryCodeField == "cniCheckNum" || strPrimaryCodeField == "cnvcBookNum" || strPrimaryCodeField == "cnvcInGATE")
                {
                    fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(changeLegsBM, changeRecordBM);
                    if (objfmCheckPax.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmCheckPax.NewContent;
                    }
                }
                //旅客名单
                else if (strPrimaryCodeField == "cntPaxNameList")
                {
                    fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(changeLegsBM, changeRecordBM);
                    if (objfmMaintenPaxNameList.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenPaxNameList.NewContent;
                    }
                }
                //中转联程旅客
                else if (strPrimaryCodeField == "cnbTransitPaxTag")
                {
                    fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(changeLegsBM, changeRecordBM);
                    if (objfmMaintenTransitPax.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTransitPax.NewContent;
                    }
                }
                //到达时间
                else if (strPrimaryCodeField == "cncTDWN")
                {
                    changeRecordBM.Refresh = 1;
                    fmMaintenTDWN objfmMaintenTDWN = new fmMaintenTDWN(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenTDWN.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTDWN.NewContent;
                    }
                }
                //起飞时间
                else if (strPrimaryCodeField == "cncTOFF")
                {
                    changeRecordBM.Refresh = 1;
                    fmMaintenTOFF objfmMaintenTOFF = new fmMaintenTOFF(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenTOFF.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTOFF.NewContent;
                    }
                }
                //总油量
                else if (strPrimaryCodeField == "cniTotalFuelWeight")
                {
                    fmMaintenFuel objfmMaintenFuel = new fmMaintenFuel(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenFuel.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenFuel.NewContent;
                    }
                }
                //进港保障信息简字
                else if (strPrimaryCodeField == "cnvcInGuaranteeRecord")
                {
                    fmGuaranteeRecord objfmGuaranteeRecord = new fmGuaranteeRecord(m_accountBM, m_stationBM, inChangeLegsBM, FlightOrientation.In);
                    objfmGuaranteeRecord.ShowDialog();
                }
                //出港保障信息简字
                else if (strPrimaryCodeField == "cnvcOutGuaranteeRecord")
                {
                    fmGuaranteeRecord objfmGuaranteeRecord = new fmGuaranteeRecord(m_accountBM, m_stationBM, outChangeLegsBM, FlightOrientation.Out);
                    objfmGuaranteeRecord.ShowDialog();
                }
                #region 进出港航班关注人员 added by LinYong in 20160324
                else if (
                    (strPrimaryCodeField == "cnvcOutSpotUsers") || 
                    (strPrimaryCodeField == "cnvcOutMaintenanceUsers") || 
                    (strPrimaryCodeField == "cnvcOutFleetUsers") ||
                    (strPrimaryCodeField == "cnvcOutVIPRoomUsers") || 
                    (strPrimaryCodeField == "cnvcOutAviationDietUsers") || 
                    (strPrimaryCodeField == "cnvcOutCheckInUsers") ||
                    (strPrimaryCodeField == "cnvcOutCleaningTeamUsers") || 

                    (strPrimaryCodeField == "cnvcInSpotUsers") || 
                    (strPrimaryCodeField == "cnvcInMaintenanceUsers") ||
                    (strPrimaryCodeField == "cnvcInFleetUsers") || 
                    (strPrimaryCodeField == "cnvcInVIPRoomUsers") || 
                    (strPrimaryCodeField == "cnvcInAviationDietUsers") ||
                    (strPrimaryCodeField == "cnvcInCheckInUsers") ||
                    (strPrimaryCodeField == "cnvcInCleaningTeamUsers"))
                {
                    CommanderTypeBM commanderTypeBM = new CommanderTypeBM();
                    switch (strPrimaryCodeField)
                    {
                        case "cnvcOutSpotUsers":
                        case "cnvcInSpotUsers":
                            commanderTypeBM.cniCommanderTypeID = 101112;
                            commanderTypeBM.cnvcCommanderType = "主控','副控','外场";
                            break;
                        case "cnvcOutMaintenanceUsers":
                        case "cnvcInMaintenanceUsers":
                            commanderTypeBM.cniCommanderTypeID = 20;
                            commanderTypeBM.cnvcCommanderType = "机务";
                            break;
                        case "cnvcOutFleetUsers":
                        case "cnvcInFleetUsers":
                            commanderTypeBM.cniCommanderTypeID = 30;
                            commanderTypeBM.cnvcCommanderType = "车队";
                            break;
                        case "cnvcOutVIPRoomUsers":
                        case "cnvcInVIPRoomUsers":
                            commanderTypeBM.cniCommanderTypeID = 40;
                            commanderTypeBM.cnvcCommanderType = "贵宾室";
                            break;
                        case "cnvcOutAviationDietUsers":
                        case "cnvcInAviationDietUsers":
                            commanderTypeBM.cniCommanderTypeID = 50;
                            commanderTypeBM.cnvcCommanderType = "航食";
                            break;
                        case "cnvcOutCheckInUsers":
                        case "cnvcInCheckInUsers":
                            commanderTypeBM.cniCommanderTypeID = 60;
                            commanderTypeBM.cnvcCommanderType = "值机";
                            break;
                        case "cnvcOutCleaningTeamUsers":
                        case "cnvcInCleaningTeamUsers":
                            commanderTypeBM.cniCommanderTypeID = 70;
                            commanderTypeBM.cnvcCommanderType = "清洁队";
                            break;
                    }
                    fmSelectUsers objfmSelectUsers = new fmSelectUsers(m_stationBM, commanderTypeBM, maintenGuaranteeInforBM.OldContent, maintenGuaranteeInforBM, changeRecordBM);
                    if (objfmSelectUsers.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmSelectUsers.CommanderList_New;
                    }
                }
                #endregion 进出港航班关注人员 added by LinYong in 20160324
                #endregion
            }
            #endregion
        }
        #endregion

        #region 回车允许用户编辑
        /// <summary>
        /// 回车允许用户编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_KeyDown(object sender, KeyEventArgs e)
        {
            FarPoint.Win.Spread.CellClickEventArgs cellClickEventArgs = new FarPoint.Win.Spread.CellClickEventArgs(null, m_iOldSelectedRow, m_iOldSelectedColumn, 10, 10, MouseButtons.None, false, false);

            if (fpFlightInfo.ActiveSheetIndex == 1 && m_iOldSelectedRow != -1)
            {
                if (e.KeyCode.ToString() == Keys.Enter.ToString())
                {
                    Maintennance(cellClickEventArgs);
                }
            }
        }
        #endregion

        #region 上下左右键
        /// <summary>
        /// 上下左右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_KeyUp(object sender, KeyEventArgs e)
        {

            if (fpFlightInfo.ActiveSheetIndex == 1 && m_iOldSelectedRow != -1)
            {
                //还原颜色
                fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;
                fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;

                if (e.KeyCode.ToString() == Keys.Up.ToString())
                {
                    if (m_iOldSelectedRow > 0)
                    {
                        m_iOldSelectedRow -= 1;
                    }
                }
                else if (e.KeyCode.ToString() == Keys.Down.ToString())
                {
                    if (m_iOldSelectedRow < fpFlightInfo.Sheets[1].RowCount - 1)
                    {
                        m_iOldSelectedRow += 1;
                    }
                }
                else if (e.KeyCode.ToString() == Keys.Left.ToString())
                {
                    if (m_iOldSelectedColumn > 0)
                    {
                        m_iOldSelectedColumn -= 1;
                    }
                }
                else if (e.KeyCode.ToString() == Keys.Right.ToString())
                {
                    if (m_iOldSelectedColumn < fpFlightInfo.Sheets[1].ColumnCount - 1)
                    {
                        m_iOldSelectedColumn += 1;
                    }
                }


                m_cOldBackGroudColor = fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor;
                m_cOldForeColor = fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor;


                //设置成新的选中的颜色
                fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = Color.White;

                fpFlightInfo.Sheets[1].AddSelection(m_iOldSelectedRow, 0, 1, fpFlightInfo.Sheets[1].ColumnCount);
            }
        }
        #endregion

        #region 根据选择的变更列表中的数据查找航班动态
        /// <summary>
        /// 根据选择的变更列表中的数据查找航班动态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvChangeContent_DoubleClick(object sender, EventArgs e)
        {
            int iRecordNo = 0;
            if (lvChangeContent.SelectedItems.Count == 0 || fpFlightInfo.ActiveSheetIndex != 1)
            {
                return;
            }

            GuaranteeInforBM findGuaranteeInfo = null;
            string strFlightNo = lvChangeContent.SelectedItems[0].SubItems[1].Text;
            strFlightNo = strFlightNo.Substring(0, strFlightNo.IndexOf("(")).Trim();

            ArrayList alFlightData = (ArrayList)m_ilTodayInOutFlights;
            for (int iLoop = 0; iLoop < alFlightData.Count; iLoop++)
            {
                findGuaranteeInfo = (GuaranteeInforBM)alFlightData[iLoop];
                if (findGuaranteeInfo.IncnvcFlightNo != null && findGuaranteeInfo.IncnvcFlightNo.IndexOf(strFlightNo) >= 0 || findGuaranteeInfo.OutcnvcFlightNo != null && findGuaranteeInfo.OutcnvcFlightNo.IndexOf(strFlightNo) >= 0)
                {
                    #region 确定 列 的位置，并定位到选中位置，如果没有找到 列，则定位到 行 -- added by LinYong in 20150215
                    //列 的 DataItemNo
                    string strDataItemNo = lvChangeContent.SelectedItems[0].Tag.ToString();
                    int indexShTodayColumns = 0;
                    for (indexShTodayColumns = 0; indexShTodayColumns < shToday.Columns.Count; indexShTodayColumns++)
                    {
                        if (shToday.Columns[indexShTodayColumns].DataField.ToUpper() == strDataItemNo.ToUpper())
                            break;
                    }

                    if (indexShTodayColumns < shToday.Columns.Count)
                    {
                        fpFlightInfo.ActiveSheet.AddSelection(iLoop, indexShTodayColumns, 1, 1);
                        //fpFlightInfo.ShowCell(0, 0, iLoop, indexShTodayColumns, FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                        fpFlightInfo.ShowRow(0, iLoop, FarPoint.Win.Spread.VerticalPosition.Center);
                        fpFlightInfo.ShowColumn(0, indexShTodayColumns, FarPoint.Win.Spread.HorizontalPosition.Center);
                    }
                    else
                    {
                        fpFlightInfo.ActiveSheet.AddSelection(iLoop, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                        fpFlightInfo.ShowRow(0, iLoop, FarPoint.Win.Spread.VerticalPosition.Center);
                    }
                    //fpFlightInfo.ActiveSheet.AddSelection(iLoop, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                    //fpFlightInfo.ShowRow(0, iLoop, FarPoint.Win.Spread.VerticalPosition.Center);

                    #endregion 确定 列 的位置，并定位到选中位置，如果没有找到 列，则定位到 行 -- added by LinYong in 20150215

                    GetInOutChangeLegsBM(iLoop);
                    iRecordNo = iLoop;
                    break;
                }
            }

            if (findGuaranteeInfo != null)
            {
                if (m_iOldSelectedRow != -1)
                {
                    fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;

                    fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;

                    m_iOldSelectedRow = iRecordNo;
                    m_cOldBackGroudColor = fpFlightInfo.ActiveSheet.Cells[iRecordNo, m_iOldSelectedColumn].BackColor;
                    m_cOldForeColor = fpFlightInfo.ActiveSheet.Cells[iRecordNo, m_iOldSelectedColumn].ForeColor;

                    //设置成新的选中的颜色
                    fpFlightInfo.ActiveSheet.Cells[iRecordNo, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                    fpFlightInfo.ActiveSheet.Cells[iRecordNo, m_iOldSelectedColumn].ForeColor = Color.White;
                }
            }

            fpFlightInfo.Focus();
        }
        #endregion

        #region 放大缩小单元格
        /// <summary>
        /// 放大
        /// </summary>
        public void ZoomOut()
        {
            UpDownValue.Value += (decimal)0.01;
            FpFlightInfo.ZoomFactor = (float)UpDownValue.Value;
        }

        /// <summary>
        /// 缩小
        /// </summary>
        public void ZoomIn()
        {
            UpDownValue.Value -= (decimal)0.01;
            FpFlightInfo.ZoomFactor = (float)UpDownValue.Value;
        }

        /// <summary>
        /// 放大缩小值变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpDownValue_ValueChanged(object sender, EventArgs e)
        {
            fpFlightInfo.ZoomFactor = (float)UpDownValue.Value;
            fpFlightInfo.Focus();
        }
        #endregion

        #region 导出航班动态
        /// <summary>
        /// 导出航班动态
        /// </summary>
        public void ExportData()
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel文件(*.xls)|*.xls";
            //覆盖文件时询问
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel文件另存为";
            saveExcel.FileName = "航班动态" + DateTime.Now.ToString("yyyy-MM-dd");
            //设置默认路径
            //			saveExcel.InitialDirectory = Application.StartupPath.Trim() + "\\..\\..\\GateFile" ;
            if (saveExcel.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                string fileName = saveExcel.FileName;
                try
                {
                    bool save = fpFlightInfo.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    if (save)
                    {
                        //恢复列标签背景色
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                    }
                    else
                    {
                        //恢复列标签背景色
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                        MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        #endregion

        /// <summary>
        /// 配载业务，显示 统计配载各操作人工作量统计窗体
        /// </summary>
        public void BalanceStatistics()
        {
            string selectDate = dtFlightDate.Value.ToString("yyyy年MM月dd日");
            string selectStation = cmbStations.Text;
            IList iListDataSource = fpFlightInfo.ActiveSheet.DataSource as IList;
            //DataTable dataTableDataSource = (fpFlightInfo.ActiveSheet.DataSource as DataTable).Copy();

            //fmBalanceStatistics objfmBalanceStatistics = new fmBalanceStatistics(selectDate, selectStation, dataTableDataSource);
            fmBalanceStatistics objfmBalanceStatistics = new fmBalanceStatistics(selectDate, selectStation, iListDataSource);

            objfmBalanceStatistics.Show();
        }

        #region 打印进出港航班动态
        /// <summary>
        /// 打印进出港航班动态
        /// </summary>
        public void PrintData()
        {
            if (fpFlightInfo.ActiveSheetIndex == 0 && m_ilYesterdayInOutFlights != null)
            {
                shSelectDate.RowCount = 0;
                shSelectDate.DataSource = null;
                shSelectDate.DataSource = m_ilYesterdayInOutFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1 && m_ilTodayInOutFlights != null)
            {
                shSelectDate.RowCount = 0;
                shSelectDate.DataSource = null;
                shSelectDate.DataSource = m_ilTodayInOutFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2 && m_ilTomorrowInOutFlights != null)
            {
                shSelectDate.RowCount = 0;
                shSelectDate.DataSource = null;
                shSelectDate.DataSource = m_ilTomorrowInOutFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3 && m_ilSelectDateInOutFlights != null)
            {
                shSelectDate.RowCount = 0;
                shSelectDate.DataSource = null;
                shSelectDate.DataSource = m_ilSelectDateInOutFlights;
            }
            else
            {
                shSelectDate.RowCount = 0;
                shSelectDate.DataSource = null;
                shSelectDate.DataSource = m_ilTomorrowInOutFlights;
            }
            //			int iOldSheetIndex = fpFlightInfo.ActiveSheetIndex;
            //			fpFlightInfo.ActiveSheetIndex = 3;
            FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();

            //			pi.ZoomFactor = (float) 0.9;
            //			pi.Header = "海南航空航班进出港动态表(" + SelectedStationInfo.AirportName + SelectedDate  + ")";
            //		
            //		
            //			
            //			pi.Footer = "打印时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "                  打印人:" + UserInfo.UserName;
            //			pi.Preview = true;
            //
            //			pi.PageStart = 1;
            //
            //
            //			pi.FirstPageNumber = 1;
            //			pi.PageOrder = FarPoint.Win.Spread.PrintPageOrder.Auto;
            //			pi.ShowShadows = false;
            //			
            //			
            //
            //			//设置页边距
            //			pi.Margin = new FarPoint.Win.Spread.PrintMargin(50, 100, 0, 50, 0, 0);
            //
            //			
            //			fpFlightInfo.ActiveSheet.PrintInfo = pi;

            pi.PrintType = FarPoint.Win.Spread.PrintType.All;
            pi.ShowColumnHeaders = true;
            pi.ShowRowHeaders = true;
            pi.ShowGrid = true;
            pi.ShowBorder = true;
            pi.ShowShadows = false;
            pi.ShowColor = false;
            pi.UseMax = true;
            pi.BestFitCols = false;

            pi.ShowPrintDialog = true;
            //			pi.Preview = true;	

            float fTotalWidth = 0;

            for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[3].ColumnCount; iLoop++)
            {
                fTotalWidth += fpFlightInfo.Sheets[3].Columns[iLoop].Width;
            }


            //			if (fpFlightInfo.ZoomFactor < 0.75)
            //			{
            //				pi.ZoomFactor = (float) fpFlightInfo.ZoomFactor;
            //			}
            //			else
            //			{
            pi.ZoomFactor = 750 / fTotalWidth;
            //			}			

            //			pi.Margin = new FarPoint.Win.Spread.PrintMargin(20, 50, 20, 0, 0, 0);

            pi.Margin.Left = 20;
            pi.Margin.Top = 50;
            pi.Margin.Bottom = 50;

            string printDate = "";
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                printDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                printDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                printDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }
            else
            {
                printDate = dtFlightDate.Value.ToString("yyyy-MM-dd");
            }
            pi.Header = "/fn\"宋体\"/fz\"14.25\"/fb1/fi0/fu0/fk0/c" + "海南航空航班进出港动态表(" + m_stationBM.AirportName + printDate + ")" + "/n";
            pi.Footer = "打印时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "                  打印人:" + m_accountBM.UserName;

            fpFlightInfo.Sheets[3].PrintInfo = pi;
            //弹出设置对话框
            fpFlightInfo.PrintSheet(3);

            //			fpFlightInfo.ActiveSheetIndex = iOldSheetIndex;
        }
        #endregion

        #region 航班动态变更实体
        private ChangeRecordBM GetChangeRecordBM(int iOut)
        {
            IList ilInOutFlights = new ArrayList();
            DataTable dtStationFlights = new DataTable();

            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                ilInOutFlights = m_ilYesterdayInOutFlights;
                dtStationFlights = m_dtYesterdayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                ilInOutFlights = m_ilTodayInOutFlights;
                dtStationFlights = m_dtTodayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                ilInOutFlights = m_ilTomorrowInOutFlights;
                dtStationFlights = m_dtTomorrowStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                ilInOutFlights = m_ilSelectDateInOutFlights;
                dtStationFlights = m_dtSelectDateStationFlights;
            }

            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(ilInOutFlights as ArrayList)[m_iRightButtonRow];

            if (iOut == 0)
            {
                //航班信息
                //变更信息
                changeRecordBM.UserID = m_accountBM.UserId;
                changeRecordBM.OldDATOP = guaranteeInforBM.IncncDATOP;
                changeRecordBM.OldFLTID = guaranteeInforBM.IncnvcFLTID;
                changeRecordBM.OldLegNo = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeRecordBM.OldAC = guaranteeInforBM.IncnvcAC;
                changeRecordBM.NewDATOP = guaranteeInforBM.IncncDATOP;
                changeRecordBM.NewFLTID = guaranteeInforBM.IncnvcFLTID;
                changeRecordBM.NewLegNo = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeRecordBM.NewAC = guaranteeInforBM.IncnvcAC;

                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                    "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                    "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                    "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeRecordBM.OldDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.OldArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.NewDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.NewArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeRecordBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeRecordBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeRecordBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeRecordBM.ChangeReasonCode = "cniCheckNum";
                    changeRecordBM.ChangeOldContent = "";
                    changeRecordBM.ActionTag = "U";
                    changeRecordBM.Refresh = 0;
                }
            }
            else
            {
                //航班信息
                changeRecordBM.UserID = m_accountBM.UserId;
                changeRecordBM.OldDATOP = guaranteeInforBM.OutcncDATOP;
                changeRecordBM.OldFLTID = guaranteeInforBM.OutcnvcFLTID;
                changeRecordBM.OldLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeRecordBM.OldAC = guaranteeInforBM.OutcnvcAC;
                changeRecordBM.NewDATOP = guaranteeInforBM.OutcncDATOP;
                changeRecordBM.NewFLTID = guaranteeInforBM.OutcnvcFLTID;
                changeRecordBM.NewLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeRecordBM.NewAC = guaranteeInforBM.OutcnvcAC;

                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                       "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                       "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                       "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeRecordBM.OldDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.OldArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.NewDepSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeRecordBM.NewArrSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeRecordBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeRecordBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeRecordBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeRecordBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeRecordBM.ChangeReasonCode = "cniCheckNum";
                    changeRecordBM.ChangeOldContent = "";
                    changeRecordBM.ActionTag = "U";
                    changeRecordBM.Refresh = 0;
                }

            }

            return changeRecordBM;
        }
        #endregion

        #region 获取航班动态实体
        private ChangeLegsBM GetChangeLegsBM(int iOut)
        {
            IList ilInOutFlights = new ArrayList();
            DataTable dtStationFlights = new DataTable();

            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                ilInOutFlights = m_ilYesterdayInOutFlights;
                dtStationFlights = m_dtYesterdayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                ilInOutFlights = m_ilTodayInOutFlights;
                dtStationFlights = m_dtTodayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                ilInOutFlights = m_ilTomorrowInOutFlights;
                dtStationFlights = m_dtTomorrowStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                ilInOutFlights = m_ilSelectDateInOutFlights;
                dtStationFlights = m_dtSelectDateStationFlights;
            }


            ChangeLegsBM changeLegsBM = new ChangeLegsBM();
            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(ilInOutFlights as ArrayList)[m_iRightButtonRow];


            if (iOut == 0)
            {
                //航班信息
                changeLegsBM.DATOP = guaranteeInforBM.IncncDATOP;
                changeLegsBM.FLTID = guaranteeInforBM.IncnvcFLTID;
                changeLegsBM.LEGNO = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeLegsBM.AC = guaranteeInforBM.IncnvcAC;

                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                        "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                        "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                        "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                    changeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                    changeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                    changeLegsBM.DEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeLegsBM.CityDEPSTN = dataRowFlight[0]["cncDEPCityThreeCode"].ToString();
                    changeLegsBM.ARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeLegsBM.CityARRSTN = dataRowFlight[0]["cncARRCityThreeCode"].ToString();
                    changeLegsBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeLegsBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeLegsBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeLegsBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeLegsBM.STATUS = dataRowFlight[0]["cncSTATUS"].ToString();
                    changeLegsBM.ACTYP = dataRowFlight[0]["cncACTYP"].ToString();
                }
            }
            else
            {
                //航班信息
                changeLegsBM.DATOP = guaranteeInforBM.OutcncDATOP;
                changeLegsBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                changeLegsBM.LEGNO = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeLegsBM.AC = guaranteeInforBM.OutcnvcAC;
                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                        "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                        "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                        "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                    changeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                    changeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                    changeLegsBM.DEPSTN = dataRowFlight[0]["cncDEPSTN"].ToString();
                    changeLegsBM.CityDEPSTN = dataRowFlight[0]["cncDEPCityThreeCode"].ToString();
                    changeLegsBM.ARRSTN = dataRowFlight[0]["cncARRSTN"].ToString();
                    changeLegsBM.CityARRSTN = dataRowFlight[0]["cncARRCityThreeCode"].ToString();
                    changeLegsBM.STD = dataRowFlight[0]["cncSTD"].ToString();
                    changeLegsBM.ETD = dataRowFlight[0]["cncETD"].ToString();
                    changeLegsBM.STA = dataRowFlight[0]["cncSTA"].ToString();
                    changeLegsBM.ETA = dataRowFlight[0]["cncETA"].ToString();
                    changeLegsBM.STATUS = dataRowFlight[0]["cncSTATUS"].ToString();
                    changeLegsBM.ACTYP = dataRowFlight[0]["cncACTYP"].ToString();
                }
            }


            return changeLegsBM;
        }
        #endregion

        #region 数据项维护实体
        private MaintenGuaranteeInforBM GetMaintenGuaranteeInforBM(int iOut)
        {
            IList ilInOutFlights = new ArrayList();
            DataTable dtStationFlights = new DataTable();

            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                ilInOutFlights = m_ilYesterdayInOutFlights;
                dtStationFlights = m_dtYesterdayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                ilInOutFlights = m_ilTodayInOutFlights;
                dtStationFlights = m_dtTodayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                ilInOutFlights = m_ilTomorrowInOutFlights;
                dtStationFlights = m_dtTomorrowStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                ilInOutFlights = m_ilSelectDateInOutFlights;
                dtStationFlights = m_dtSelectDateStationFlights;
            }


            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(ilInOutFlights as ArrayList)[m_iRightButtonRow];

            maintenGuaranteeInforBM.FieldType = 1;

            //维护信息
            if (iOut == 0)
            {


                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                        "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                        "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                        "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    maintenGuaranteeInforBM.OldContent = dataRowFlight[0]["cniFocusTag"].ToString();
                }
                maintenGuaranteeInforBM.DATOP = guaranteeInforBM.IncncDATOP;
                maintenGuaranteeInforBM.FLTID = guaranteeInforBM.IncnvcFLTID;
                maintenGuaranteeInforBM.LEGNO = guaranteeInforBM.IncniLEGNO;
                maintenGuaranteeInforBM.AC = guaranteeInforBM.IncnvcAC;
                maintenGuaranteeInforBM.FieldName = "cniFocusTag";
                maintenGuaranteeInforBM.FieldLength = 50;
                maintenGuaranteeInforBM.ColumnCaption = "进港重点保障航班";
            }
            else
            {
                DataRow[] dataRowFlight = dtStationFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                        "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                        "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                        "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    maintenGuaranteeInforBM.OldContent = dataRowFlight[0]["cniFocusTag"].ToString();
                }

                maintenGuaranteeInforBM.DATOP = guaranteeInforBM.OutcncDATOP;
                maintenGuaranteeInforBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                maintenGuaranteeInforBM.LEGNO = guaranteeInforBM.OutcniLEGNO;
                maintenGuaranteeInforBM.AC = guaranteeInforBM.OutcnvcAC;
                maintenGuaranteeInforBM.FieldName = "cniFocusTag";
                maintenGuaranteeInforBM.FieldLength = 50;
                maintenGuaranteeInforBM.ColumnCaption = "出港重点保障航班";
            }

            return maintenGuaranteeInforBM;
        }
        #endregion

        #region 右键菜单事件
        #region 出港航班航段信息
        /// <summary>
        /// 出港航班航段信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutAircraftFlights_Click(object sender, EventArgs e)
        {
            DateTimeBM dateTimeBM = new DateTimeBM();

            string strLONG_REG = "";
            //if (inChangeLegsBM.LONG_REG != "" && inChangeLegsBM.LONG_REG != null)
            //{
            //    strLONG_REG = inChangeLegsBM.LONG_REG;
            //}
            //else
            //{
            strLONG_REG = outChangeLegsBM.LONG_REG;
            //}
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                dateTimeBM = GetDateTimeBM(0);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                dateTimeBM = GetDateTimeBM(1);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                dateTimeBM = GetDateTimeBM(2);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                dateTimeBM = GetDateTimeBM(3);
            }

            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1);


            fmAircraftFlights objfmAircraftFlights = new fmAircraftFlights(dateTimeBM, strLONG_REG, m_accountBM, m_dtDataItemPurview);
            objfmAircraftFlights.ShowDialog();
        }
        #endregion

        #region 出港变更数据
        /// <summary>
        /// 出港变更数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutChangeData_Click(object sender, EventArgs e)
        {
            fmChangeData objfmChangeData = new fmChangeData(GetDateTimeBM(fpFlightInfo.ActiveSheetIndex), m_accountBM, outChangeLegsBM.FlightNo, "", 0);
            objfmChangeData.ShowDialog();
        }
        #endregion

        #region 出港航班重点保障航班说明
        /// <summary>
        /// 出港航班重点保障航班说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFocusFlight_Click(object sender, EventArgs e)
        {
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = GetMaintenGuaranteeInforBM(1);
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);

            changeRecordBM.ChangeReasonCode = "cniFocusTag";

            if (GetDataItemPurview("OutcniFocusTag") > 1)
            {
                fmMaintenMutiLineText objfmMaintenMutiLineText = new fmMaintenMutiLineText(maintenGuaranteeInforBM, changeRecordBM);

                objfmMaintenMutiLineText.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 判断是否有权限
        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="strDataItemID"></param>
        private int GetDataItemPurview(string strDataItemID)
        {
            DataRow[] dataRow = m_dtDataItemPurview.Select("cnvcDataItemID = '" + strDataItemID + "'");

            if (dataRow.Length > 0)
            {
                return Convert.ToInt32(dataRow[0]["cniDataItemPurview"].ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 出港值机信息
        /// <summary>
        /// 出港值机信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutCheckPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1);

            if (GetDataItemPurview("OutcniCheckNum") > 1)
            {
                fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(changeLegsBM, changeRecordBM);
                objfmCheckPax.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 出港中转旅客信息
        /// <summary>
        /// 出港中转旅客信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutTransitPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1);
            changeRecordBM.ChangeReasonCode = "cnbTransitPaxTag";
            if (GetDataItemPurview("OutcnbTransitPaxTag") > 1)
            {
                fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(changeLegsBM, changeRecordBM);
                objfmMaintenTransitPax.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region 出港旅客名单
        /// <summary>
        /// 出港旅客名单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutNameList_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1);
            changeRecordBM.ChangeReasonCode = "cntPaxNameList";
            if (GetDataItemPurview("OutcntPaxNameList") > 1)
            {
                fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(changeLegsBM, changeRecordBM);
                objfmMaintenPaxNameList.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region 出港计算机飞行计划
        /// <summary>
        /// 出港计算机飞行计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFlightPlan_Click(object sender, EventArgs e)
        {
            #region 增加 航班日期（UTC） 作为判断条件，modified by LinYong in 2013.07.31
            //fmComputerPlan objfmComputerPlan = new fmComputerPlan(outChangeLegsBM.FlightDate, outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN);
            fmComputerPlan objfmComputerPlan = new fmComputerPlan(outChangeLegsBM.FlightDate, outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN, outChangeLegsBM.DATOP);
            #endregion 增加 航班日期（UTC） 作为判断条件，modified by LinYong in 2013.07.31

            objfmComputerPlan.ShowDialog();
            objfmComputerPlan.Text += outChangeLegsBM.FlightNo;
        }
        #endregion

        #region 出港任务书
        /// <summary>
        /// 出港任务书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutTaskSheet_Click(object sender, EventArgs e)
        {
            string strURL = "http://crw.hnair.net/WebUI/PilotWebUI/PrintFltTask/wfmPrintFltTask.aspx";
            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "-任务书" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        #region 出港签派放行单
        /// <summary>
        /// 出港签派放行单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutDispathSheet_Click(object sender, EventArgs e)
        {
            #region LinYong in 2013.08.02
            //fmDispatchSheet objfmDispatchSheet = new fmDispatchSheet(outChangeLegsBM.FlightDate, outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN);
            fmDispatchSheet objfmDispatchSheet = new fmDispatchSheet(outChangeLegsBM.FlightDate, outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN, outChangeLegsBM.DATOP);
            #endregion modified by LinYong in 2013.08.02

            objfmDispatchSheet.ShowDialog();
            objfmDispatchSheet.Text += outChangeLegsBM.FlightNo;
        }
        #endregion

        #region 出港天气
        /// <summary>
        /// 出港天气
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutWeather_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planMet.aspx?strCompanyID=9&strstdDate=";
            strURL += outChangeLegsBM.FlightDate + "&strDepstn=" + outChangeLegsBM.DEPSTN +
                "&strArrstn=" + outChangeLegsBM.ARRSTN + "&strFlightno=" + outChangeLegsBM.FlightNo +
                "&strPlaneNo=" + outChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + outChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + outChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + outChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "天气" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        #region 出港天气标准
        /// <summary>
        /// 出港天气标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutWeatherStandard_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/notam/notam/standard/AirStandard.asp?CODEINPUT=" + outChangeLegsBM.ARRFourCode;

            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "天气标准" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        #region 出港航行通告
        /// <summary>
        /// 出港航行通告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutNOTAM_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planNotam.aspx?strCompanyID=9&strstdDate=";
            strURL += outChangeLegsBM.FlightDate + "&strDepstn=" + outChangeLegsBM.DEPSTN +
                "&strArrstn=" + outChangeLegsBM.ARRSTN + "&strFlightno=" + outChangeLegsBM.FlightNo +
                "&strPlaneNo=" + outChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + outChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + outChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + outChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "航行通告" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 出港机组信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutCrew_Click(object sender, EventArgs e)
        {
            //string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/PublicInfo/AirCrew.aspx?strCompanyID=9&strstdDate=";
            //strURL += outChangeLegsBM.FlightDate + "&strDepstn=" + outChangeLegsBM.DEPSTN +
            //    "&strArrstn=" + outChangeLegsBM.ARRSTN + "&strFlightno=" + outChangeLegsBM.FlightNo +
            //    "&strPlaneNo=" + outChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + outChangeLegsBM.DATOP +
            //    "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + outChangeLegsBM.STC +
            //    "&strDispatchID=&strPosition=ALL&strstdTime=" + outChangeLegsBM.STD;
            StringBuilder strUrl = new StringBuilder();
            strUrl.Append("http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/PublicInfo/icmsCaptainInfonew.aspx");
            strUrl.Append("?strCompanyID=");
            strUrl.Append("&strstdDate=" + outChangeLegsBM.FlightDate);
            strUrl.Append("&strDepstn=" + outChangeLegsBM.DEPSTN);
            strUrl.Append("&strArrstn=" + outChangeLegsBM.ARRSTN);
            strUrl.Append("&strFlightno=" + outChangeLegsBM.FlightNo);
            strUrl.Append("&strPlaneNo=" + outChangeLegsBM.LONG_REG);
            strUrl.Append("&strPlanType=");
            strUrl.Append("&strDatop=" + outChangeLegsBM.DATOP);
            strUrl.Append("&strIsPlan=1");
            strUrl.Append("&strSeatId=ALL");
            strUrl.Append("&strFocCharter=" + outChangeLegsBM.STC);
            strUrl.Append("&strDispatchID=");
            strUrl.Append("&strPosition=ALL");
            strUrl.Append("&strstdTime=" + outChangeLegsBM.STD);

            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strUrl.ToString());
            objfmSurpportInfor.Text += "机组" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 出港航线分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFlightLine_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Aircraft/airline.aspx?strCompanyID=9&strstdDate=";
            strURL += outChangeLegsBM.FlightDate + "&strDepstn=" + outChangeLegsBM.DEPSTN +
                "&strArrstn=" + outChangeLegsBM.ARRSTN + "&strFlightno=" + outChangeLegsBM.FlightNo +
                "&strPlaneNo=" + outChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + outChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + outChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + outChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "航线分析" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 出港重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutWeightData_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/tps/weight_data/planbalancedata.asp?strCompanyID=9&strstdDate=";
            strURL += outChangeLegsBM.FlightDate + "&strDepstn=" + outChangeLegsBM.DEPSTN +
                "&strArrstn=" + outChangeLegsBM.ARRSTN + "&strFlightno=" + outChangeLegsBM.FlightNo +
                "&strPlaneNo=" + outChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + outChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + outChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + outChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "重量数据" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 出港保留故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFault_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Aircraft/AircraftInfo.aspx?strCompanyID=9&strstdDate=";
            strURL += outChangeLegsBM.FlightDate + "&strDepstn=" + outChangeLegsBM.DEPSTN +
                "&strArrstn=" + outChangeLegsBM.ARRSTN + "&strFlightno=" + outChangeLegsBM.FlightNo +
                "&strPlaneNo=" + outChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + outChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + outChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + outChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "保留故障" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 出港机组签到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutCrewSign_Click(object sender, EventArgs e)
        {
            fmCrewSignIn objfmCrewSignIn = new fmCrewSignIn(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN);
            objfmCrewSignIn.ShowDialog();
        }

        /// <summary>
        /// 出港乘务签到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutStewardSign_Click(object sender, EventArgs e)
        {
            fmStewardSignIn objfmStewardSignIn = new fmStewardSignIn(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN);
            objfmStewardSignIn.ShowDialog();
        }

        /// <summary>
        /// 进港航班航段信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInAircraftFlights_Click(object sender, EventArgs e)
        {
            DateTimeBM dateTimeBM = new DateTimeBM();
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                dateTimeBM = GetDateTimeBM(0);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                dateTimeBM = GetDateTimeBM(1);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                dateTimeBM = GetDateTimeBM(2);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                dateTimeBM = GetDateTimeBM(3);
            }
            string strLONG_REG = inChangeLegsBM.LONG_REG;
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0);

            fmAircraftFlights objfmAircraftFlights = new fmAircraftFlights(dateTimeBM, strLONG_REG, m_accountBM, m_dtDataItemPurview);
            objfmAircraftFlights.ShowDialog();
        }

        /// <summary>
        /// 进港变更数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInChangeData_Click(object sender, EventArgs e)
        {
            fmChangeData objfmChangeData = new fmChangeData(GetDateTimeBM(fpFlightInfo.ActiveSheetIndex), m_accountBM, inChangeLegsBM.FlightNo, "", 0);
            objfmChangeData.ShowDialog();
        }

        /// <summary>
        /// 进港航班重点保障航班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInFocusFlight_Click(object sender, EventArgs e)
        {
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = GetMaintenGuaranteeInforBM(0);
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            changeRecordBM.ChangeReasonCode = "cniFocusTag";

            if (GetDataItemPurview("IncniFocusTag") > 1)
            {
                fmMaintenMutiLineText objfmMaintenMutiLineText = new fmMaintenMutiLineText(maintenGuaranteeInforBM, changeRecordBM);

                objfmMaintenMutiLineText.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 进港值机信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInCheckPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0);

            if (GetDataItemPurview("IncniCheckNum") > 1)
            {
                fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(changeLegsBM, changeRecordBM);
                objfmCheckPax.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void miInTransitPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0);
            changeRecordBM.ChangeReasonCode = "cnbTransitPaxTag";
            if (GetDataItemPurview("IncnbTransitPaxTag") > 1)
            {
                fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(changeLegsBM, changeRecordBM);
                objfmMaintenTransitPax.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void miInNameList_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0);
            changeRecordBM.ChangeReasonCode = "cntPaxNameList";
            if (GetDataItemPurview("IncntPaxNameList") > 1) 
            {
                fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(changeLegsBM, changeRecordBM);
                objfmMaintenPaxNameList.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SetInOutFlightsNum()
        {
            fmMDIMain objfmMDIMain = (this.MdiParent as fmMDIMain);
            DataTable dtStationFlights = new DataTable();
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                dtStationFlights = m_dtYesterdayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                dtStationFlights = m_dtTodayStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                dtStationFlights = m_dtTomorrowStationFlights;
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                dtStationFlights = m_dtSelectDateStationFlights;
            }

            DataRow[] drInFlights = dtStationFlights.Select("cncARRSTN='" + m_stationBM.ThreeCode + "' AND cncSTATUS <> 'CNL'");
            DataRow[] drOutFlights = dtStationFlights.Select("cncDEPSTN='" + m_stationBM.ThreeCode + "' AND cncSTATUS <> 'CNL'");

            m_statusBar.Panels[0].Text = "登陆用户:" + m_accountBM.UserName;
            m_statusBar.Panels[1].Text = "进港:" + drInFlights.Length + "      " + "出港:" + drOutFlights.Length;
        }

        /// <summary>
        /// 进港计算机飞行计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInFlightPlan_Click(object sender, EventArgs e)
        {
            #region 增加 航班日期（UTC） 作为判断条件，modified by LinYong in 2013.07.31
            //fmComputerPlan objfmComputerPlan = new fmComputerPlan(inChangeLegsBM.FlightDate, inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN);
            fmComputerPlan objfmComputerPlan = new fmComputerPlan(inChangeLegsBM.FlightDate, inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN, inChangeLegsBM.DATOP);
            #endregion 增加 航班日期（UTC） 作为判断条件，modified by LinYong in 2013.07.31

            objfmComputerPlan.ShowDialog();
            objfmComputerPlan.Text += inChangeLegsBM.FlightNo;
        }

        /// <summary>
        /// 进港任务书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInTaskSheet_Click(object sender, EventArgs e)
        {
            string strURL = "http://crw.hnair.net/WebUI/PilotWebUI/PrintFltTask/wfmPrintFltTask.aspx";
            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "任务书" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 进港签派放行单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInDispathSheet_Click(object sender, EventArgs e)
        {
            #region LinYong in 2013.08.02
            //fmDispatchSheet objfmDispatchSheet = new fmDispatchSheet(inChangeLegsBM.FlightDate, inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN);
            fmDispatchSheet objfmDispatchSheet = new fmDispatchSheet(inChangeLegsBM.FlightDate, inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN, inChangeLegsBM.DATOP);
            #endregion LinYong in 2013.08.02

            objfmDispatchSheet.ShowDialog();
            objfmDispatchSheet.Text += inChangeLegsBM.FlightNo;
        }

        /// <summary>
        /// 进港天气
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miWeather_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planMet.aspx?strCompanyID=9&strstdDate=";
            strURL += inChangeLegsBM.FlightDate + "&strDepstn=" + inChangeLegsBM.DEPSTN +
                "&strArrstn=" + inChangeLegsBM.ARRSTN + "&strFlightno=" + inChangeLegsBM.FlightNo +
                "&strPlaneNo=" + inChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + inChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + inChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + inChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "天气" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 进港天气标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInWeatherStandard_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/notam/notam/standard/AirStandard.asp?CODEINPUT=" + inChangeLegsBM.ARRFourCode;

            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "天气标准" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 进港航行通告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInNOTAM_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/planNotam.aspx?strCompanyID=9&strstdDate=";
            strURL += inChangeLegsBM.FlightDate + "&strDepstn=" + inChangeLegsBM.DEPSTN +
                "&strArrstn=" + inChangeLegsBM.ARRSTN + "&strFlightno=" + inChangeLegsBM.FlightNo +
                "&strPlaneNo=" + inChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + inChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + inChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + inChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "航行通告" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 机组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInCrew_Click(object sender, EventArgs e)
        {
            //string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/PublicInfo/AirCrew.aspx?strCompanyID=9&strstdDate=";
            //strURL += inChangeLegsBM.FlightDate + "&strDepstn=" + inChangeLegsBM.DEPSTN +
            //    "&strArrstn=" + inChangeLegsBM.ARRSTN + "&strFlightno=" + inChangeLegsBM.FlightNo +
            //    "&strPlaneNo=" + inChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + inChangeLegsBM.DATOP +
            //    "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + inChangeLegsBM.STC +
            //    "&strDispatchID=&strPosition=ALL&strstdTime=" + inChangeLegsBM.STD;
            StringBuilder strUrl = new StringBuilder();
            strUrl.Append("http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/PublicInfo/icmsCaptainInfonew.aspx");
            strUrl.Append("?strCompanyID=");
            strUrl.Append("&strstdDate=" + inChangeLegsBM.FlightDate);
            strUrl.Append("&strDepstn=" + inChangeLegsBM.DEPSTN);
            strUrl.Append("&strArrstn=" + inChangeLegsBM.ARRSTN);
            strUrl.Append("&strFlightno=" + inChangeLegsBM.FlightNo);
            strUrl.Append("&strPlaneNo=" + inChangeLegsBM.LONG_REG);
            strUrl.Append("&strPlanType=");
            strUrl.Append("&strDatop=" + inChangeLegsBM.DATOP);
            strUrl.Append("&strIsPlan=1");
            strUrl.Append("&strSeatId=ALL");
            strUrl.Append("&strFocCharter=" + inChangeLegsBM.STC);
            strUrl.Append("&strDispatchID=");
            strUrl.Append("&strPosition=ALL");
            strUrl.Append("&strstdTime=" + inChangeLegsBM.STD);

            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strUrl.ToString());
            objfmSurpportInfor.Text += "机组" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 进港航线分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInFlightLine_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Aircraft/airline.aspx?strCompanyID=9&strstdDate=";
            strURL += inChangeLegsBM.FlightDate + "&strDepstn=" + inChangeLegsBM.DEPSTN +
                "&strArrstn=" + inChangeLegsBM.ARRSTN + "&strFlightno=" + inChangeLegsBM.FlightNo +
                "&strPlaneNo=" + inChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + inChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + inChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + inChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "航线分析" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 进港重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInWeightData_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/tps/weight_data/planbalancedata.asp?strCompanyID=9&strstdDate=";
            strURL += inChangeLegsBM.FlightDate + "&strDepstn=" + inChangeLegsBM.DEPSTN +
                "&strArrstn=" + inChangeLegsBM.ARRSTN + "&strFlightno=" + inChangeLegsBM.FlightNo +
                "&strPlaneNo=" + inChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + inChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + inChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + inChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "重量数据" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// 进港保留故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInFault_Click(object sender, EventArgs e)
        {
            string strURL = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Aircraft/AircraftInfo.aspx?strCompanyID=9&strstdDate=";
            strURL += inChangeLegsBM.FlightDate + "&strDepstn=" + inChangeLegsBM.DEPSTN +
                "&strArrstn=" + inChangeLegsBM.ARRSTN + "&strFlightno=" + inChangeLegsBM.FlightNo +
                "&strPlaneNo=" + inChangeLegsBM.LONG_REG + "&strPlanType=" + "&strDatop=" + inChangeLegsBM.DATOP +
                "&strIsPlan=1&strSeatId=ALL" + "&strFocCharter=" + inChangeLegsBM.STC +
                "&strDispatchID=&strPosition=ALL&strstdTime=" + inChangeLegsBM.STD;


            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "保留故障" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }


        /// <summary>
        /// 进港机组签到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInCrewSign_Click(object sender, EventArgs e)
        {
            fmCrewSignIn objfmCrewSignIn = new fmCrewSignIn(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN);
            objfmCrewSignIn.ShowDialog();
        }

        /// <summary>
        /// 进港乘务签到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInStewardSign_Click(object sender, EventArgs e)
        {
            fmStewardSignIn objfmStewardSignIn = new fmStewardSignIn(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN);
            objfmStewardSignIn.ShowDialog();
        }

        private void fpFlightInfo_TextTipFetch(object sender, FarPoint.Win.Spread.TextTipFetchEventArgs e)
        {
            e.TipText = fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Note;
            if (fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Note != "")
            {
                e.ShowTip = true;
            }
            else
            {
                e.ShowTip = false;
            }
        }
        #endregion

        #region 格式化进港航班特殊字段
        /// <summary>
        /// 格式化进港航班特殊字段
        /// </summary>
        /// <param name="dataRow">航班数据</param>
        /// <param name="dataRowItems">数据项</param>
        /// <returns>格式化后的数据</returns>
        public string FormatINItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //进港航班起飞机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //进港航班到达机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //计划到达时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncSTA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //延误时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncETA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //if (dataRow["cnvcDELAY1"].ToString().Trim() != "")
                //{
                //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //}
                //else
                //{
                //    strFieldValue = "";
                //}
            }
            //落地时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncTDWN")
            {
                //if (strStatus == "DEP" || strStatus == "ARR" || strStatus == "ATA")
                if (strStatus == "ARR" || strStatus == "ATA") //时间落地到达时才显示落地时间
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //到位时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncATA")
            {
                if (strStatus == "ATA")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }


            return strFieldValue;
        }
        #endregion

        #region 格式化出港航班特殊字段
        /// <summary>
        /// 格式化出港航班特殊字段
        /// </summary>
        /// <param name="dataRow">航班数据</param>
        /// <param name="dataRowItems">数据项</param>
        /// <returns>格式化后的数据</returns>
        public string FormatOUTItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //出港航班起飞机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //出港航班落地机场
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //计划起飞时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTD")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //起飞延误时间短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncETD")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //if (dataRow["cnvcDELAY1"].ToString().Trim() != "")
                //{
                //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                //}
                //else
                //{
                //    strFieldValue = "";
                //}
            }
            //推出时间
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncATD")
            {
                if (strStatus == "ATD" || strStatus == "DEP" || strStatus == "ATA" || strStatus == "ARR")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //起飞动态短格式
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncTOFF")
            {
                if (strStatus == "DEP" || strStatus == "ATA" || strStatus == "ARR")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //延误代码
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcniDUR1")
            {
                if (strFieldValue == "0")
                {
                    strFieldValue = "";
                }
            }

            #region added by LinYong in 2016.05.04
            //出发到达|计划
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //出发到达|预达
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncETA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");

            }
            //出发到达|落地
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncTDWN")
            {
                //if (strStatus == "DEP" || strStatus == "ARR" || strStatus == "ATA")
                if (strStatus == "ARR" || strStatus == "ATA") //时间落地到达时才显示落地时间
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //出发到达|到位
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncATA")
            {
                if (strStatus == "ATA")
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            #endregion added by LinYong in 2016.05.04


            return strFieldValue;
        }
        #endregion

        #region 航班旅客信息
        /// <summary>
        /// 进港航班查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //判断是否选中某一行

                if (m_iOldSelectedRow == -1)
                {
                    MessageBox.Show("请选择具体某一航班", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string strOutTitle = DateTime.Parse(outChangeLegsBM.FlightDate).ToString("MM月dd日;") + outChangeLegsBM.FLTID.ToString() + ";" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN; //DATOP 改为 FlightDate -- modified by LinYong in 20160316
                string strInTitle = DateTime.Parse(inChangeLegsBM.FlightDate).ToString("MM月dd日;") + inChangeLegsBM.FLTID.ToString() + ";" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN; //DATOP 改为 FlightDate -- modified by LinYong in 20160316
                if (inChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strInTitle = "无";
                }
                if (outChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strOutTitle = "无";
                }

                #region 航班甘特图
                if (dataTabControlGanttView.SelectedIndex == 4)
                {

                    /*****************************************************************/
                    //先清除所有节点
                    this.ganttViewToolBox.remove();

                    DataSet oDataSet = new DataSet();//出港航班
                    DataSet iDataSet = new DataSet();//进港航班

                    IODataSet ioDataSet = new IODataSet();
                    StandardItemViewBF standardItemViewBF = new StandardItemViewBF();
                    FlightParams oFlightParams = new FlightParams();
                    FlightParams iFlightParams = new FlightParams();
                    if (!outChangeLegsBM.FLTID.Equals("HU 0000"))
                    {
                        this.ganttViewToolBox.DTOUTSTD = DateTime.Parse(outChangeLegsBM.STD);
                        oFlightParams.FlightNum = outChangeLegsBM.FLTID;
                        oFlightParams.Datop = outChangeLegsBM.DATOP;
                        oFlightParams.DepStn = outChangeLegsBM.DEPSTN;
                        oFlightParams.ArrStn = outChangeLegsBM.ARRSTN;

                        if (inChangeLegsBM.FLTID.Equals("HU 0000"))
                        {
                            oFlightParams.FlightType = "始发";
                            this.ganttViewToolBox.DTINSTD = DateTime.MinValue;
                        }
                        else
                        {
                            oFlightParams.FlightType = "过站";
                        }


                        PublicService.PublicService airPortService = new AirSoft.FlightMonitor.FlightMonitorClient.PublicService.PublicService();
                        DataSet dataAirPortSet = new DataSet();
                        dataAirPortSet = airPortService.GetAirportInfo(outChangeLegsBM.ARRSTN);

                        if (dataAirPortSet != null || dataAirPortSet.Tables[0].Rows.Count != 0)
                        {
                            if (!"CHN".Equals(dataAirPortSet.Tables[0].Rows[0]["Country"].ToString()))
                            {
                                oFlightParams.OIFlightType = "国际";
                            }
                            else
                            {
                                oFlightParams.OIFlightType = "国内";
                            }

                        }

                        oFlightParams.LongReg = outChangeLegsBM.LONG_REG;
                    }
                    if (!inChangeLegsBM.FLTID.Equals("HU 0000"))
                    {
                        this.ganttViewToolBox.DTINSTD = DateTime.Parse(inChangeLegsBM.STA);
                        iFlightParams.FlightNum = inChangeLegsBM.FLTID;
                        iFlightParams.Datop = inChangeLegsBM.DATOP;
                        iFlightParams.DepStn = inChangeLegsBM.DEPSTN;
                        iFlightParams.ArrStn = inChangeLegsBM.ARRSTN;

                        if (outChangeLegsBM.FLTID.Equals("HU 0000"))
                        {
                            this.ganttViewToolBox.DTOUTSTD = DateTime.MinValue;
                            iFlightParams.FlightType = "始发";
                        }
                        else
                        {
                            iFlightParams.FlightType = "过站";
                        }
                        //iFlightParams.FlightType = "始发";
                        
                        PublicService.PublicService airPortService = new AirSoft.FlightMonitor.FlightMonitorClient.PublicService.PublicService();
                        DataSet dataAirPortSet = new DataSet();
                        dataAirPortSet = airPortService.GetAirportInfo(inChangeLegsBM.ARRSTN);

                        if (dataAirPortSet != null || dataAirPortSet.Tables[0].Rows.Count != 0)
                        {
                            if (!"CHN".Equals(dataAirPortSet.Tables[0].Rows[0]["Country"].ToString()))
                            {
                                iFlightParams.OIFlightType = "国际";
                            }
                            else
                            {
                                iFlightParams.OIFlightType = "国内";
                            }

                        }

                        iFlightParams.LongReg = inChangeLegsBM.LONG_REG;
                    }

                    ioDataSet = standardItemViewBF.GetIODataSet(iFlightParams, oFlightParams);
                    iDataSet = ioDataSet.IDataSet;
                    oDataSet = ioDataSet.ODataSet;

                    DataRow[] iDataRow = null;
                    DataRow[] oDataRow = null;

                    if (iDataSet != null && iDataSet.Tables.Count != 0)
                    {
                        iDataRow = iDataSet.Tables[0].Select("", " SEQ ");
                    }
                    if (oDataSet != null && oDataSet.Tables.Count != 0)
                    {
                        oDataRow = oDataSet.Tables[0].Select("", " SEQ ");
                    }

                    if (iDataRow != null)
                    {
                        Random oRandom = new Random();
                        
                        foreach (DataRow dr in iDataRow)
                        {
                            if (!("".Equals(dr["StartTime"].ToString()) || "".Equals(dr["EndTime"].ToString())) &&
                                !("".Equals(dr["BaseLineStartTime"].ToString()) || "".Equals(dr["BaseLineEndTime"].ToString())))
                            {
                                this.ganttViewToolBox.add(
                                        dr["cnvcItemName"].ToString(),
                                        oRandom.Next(1, 100),
                                        DateTime.Parse(dr["StartTime"].ToString()),
                                        DateTime.Parse(dr["EndTime"].ToString()),
                                        DateTime.Parse(dr["BaseLineStartTime"].ToString()),
                                        DateTime.Parse(dr["BaseLineEndTime"].ToString())
                                );
                            }
                            else if (!("".Equals(dr["StartTime"].ToString()) && "".Equals(dr["EndTime"].ToString())))
                            {
                                this.ganttViewToolBox.add(
                                        dr["cnvcItemName"].ToString(),
                                        oRandom.Next(1, 100),
                                        DateTime.Parse(dr["StartTime"].ToString()),
                                        DateTime.Parse(dr["EndTime"].ToString())
                                );
                            }
                            else if (!("".Equals(dr["BaseLineStartTime"].ToString()) || "".Equals(dr["BaseLineEndTime"].ToString())))
                            {
                                this.ganttViewToolBox.add(
                                        dr["cnvcItemName"].ToString(),
                                        oRandom.Next(1, 100),
                                        DateTime.Parse(dr["BaseLineStartTime"].ToString()),
                                        DateTime.Parse(dr["BaseLineEndTime"].ToString())
                                );
                            }
                        }
                    }


                    if (oDataRow != null)
                    {
                        Random oRandom = new Random();

                        foreach (DataRow dr in oDataRow)
                        {
                            if (!("".Equals(dr["StartTime"].ToString()) || "".Equals(dr["EndTime"].ToString())) &&
                                !("".Equals(dr["BaseLineStartTime"].ToString()) || "".Equals(dr["BaseLineEndTime"].ToString())))
                            {
                                this.ganttViewToolBox.add(
                                        dr["cnvcItemName"].ToString(),
                                        oRandom.Next(1, 100),
                                        DateTime.Parse(dr["StartTime"].ToString()),
                                        DateTime.Parse(dr["EndTime"].ToString()),
                                        DateTime.Parse(dr["BaseLineStartTime"].ToString()),
                                        DateTime.Parse(dr["BaseLineEndTime"].ToString())
                                );
                            }
                            else if (!("".Equals(dr["StartTime"].ToString()) && "".Equals(dr["EndTime"].ToString())))
                            {
                                 this.ganttViewToolBox.add(
                                        dr["cnvcItemName"].ToString(),
                                        oRandom.Next(1, 100),
                                        DateTime.Parse(dr["StartTime"].ToString()),
                                        DateTime.Parse(dr["EndTime"].ToString())
                                );
                            }
                            else if (!("".Equals(dr["BaseLineStartTime"].ToString()) || "".Equals(dr["BaseLineEndTime"].ToString())))
                            {
                                this.ganttViewToolBox.add(
                                        dr["cnvcItemName"].ToString(),
                                        oRandom.Next(1, 100),
                                        DateTime.Parse(dr["BaseLineStartTime"].ToString()),
                                        DateTime.Parse(dr["BaseLineEndTime"].ToString())
                                );
                            }
                        }
                    }

                    this.ganttViewToolBox.draw();
                    /*****************************************************************/
            
                }
                #endregion

                #region 进港航班处理
                if (dataTabControlGanttView.SelectedIndex == 1)
                {
                    inTabPage.Text = "进港航班" + "(" + strInTitle + ")";

                    intabControl_SelectedIndexChanged(sender, e);
                }
                #endregion

                #region 出港航班处理
                if (dataTabControlGanttView.SelectedIndex == 2)
                {
                    outTabPage.Text = "出港航班" + "(" + strOutTitle + ")";
                    
                    outtabControl_SelectedIndexChanged(sender, e);

                }
                #endregion

                setDataTabControlTile(dataTabControlGanttView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        #region yanxian 2013-10-16 ListView赋值
        private void showListViewData(ListView lListView, DataRow[] dataRow)
        {
            lListView.Clear();

            lListView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度   

            lListView.Columns.Add("序号", 50, HorizontalAlignment.Center);
            lListView.Columns.Add("姓名", 80, HorizontalAlignment.Right);
            lListView.Columns.Add("座位号", 50, HorizontalAlignment.Left);
            lListView.Columns.Add("卡号", 150, HorizontalAlignment.Left);
            lListView.Columns.Add("联系方式", 300, HorizontalAlignment.Left);
            lListView.Columns.Add("性别", 50, HorizontalAlignment.Left);
            lListView.Columns.Add("行李号", 300, HorizontalAlignment.Left);
            lListView.Columns.Add("值机方式", 80, HorizontalAlignment.Left);

            lListView.View = View.Details;
            lListView.Visible = true;
            lListView.FullRowSelect = true;
            lListView.Scrollable = true;
            lListView.GridLines = true;

            for (int i = 0; i < dataRow.Length; i++)   //添加行数据   
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Clear();

                lvi.Text = (i+1).ToString() ;

                lvi.SubItems.Add(dataRow[i][0].ToString());

                lvi.SubItems.Add(dataRow[i][1].ToString());

                lvi.SubItems.Add(dataRow[i][2].ToString());

                lvi.SubItems.Add(dataRow[i][3].ToString());

                lvi.SubItems.Add(dataRow[i][4].ToString());

                lvi.SubItems.Add(dataRow[i][5].ToString());

                lvi.SubItems.Add(dataRow[i][6].ToString());

                lListView.Items.Add(lvi);
            }

            lListView.EndUpdate();  //结束数据处理，UI界面一次性绘制。  
        }
        #endregion


        private void intabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(intabControl);

            #region 任务书处理
            if (intabControl.SelectedTab.Name.ToUpper() == "inTaskBooktabPage".ToUpper())
            {
                //进港航班任务书标题栏显示
                intabControl_Taskbook_SelectedIndexChanged(sender, e);

                try
                {
                    ReturnValueSF returnValueSF = new ReturnValueSF();
                    TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
                    string strCurrentTime = ""; //当前查询时刻
                    string strLastTime = ""; //上次查询时刻
                    DataTable dataTable_1 = null;
                    #region 判断前后两次查询同一航班的时间间隔，控制访问任务书服务的频率
                    if ((_dataTableTaskBooks != null) && (inChangeLegsBM != null))
                    {
                        DataRow[] dataRows_dataTableTaskBooks_1 = _dataTableTaskBooks.Select("(cnvcDATOP = '" + inChangeLegsBM.DATOP + "') and " +
                                "(cnvcFlightNo = '" + inChangeLegsBM.FlightNo + "') and " +
                                "(cnvcLONG_REG = '" + inChangeLegsBM.LONG_REG + "') and " +
                                "(cnvcSTD = '" + inChangeLegsBM.STD + "') and " +
                                "(cnvcROUTE = '" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN + "') ");

                        if (dataRows_dataTableTaskBooks_1.Length > 0)
                        {
                            //
                            if (dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString().Trim() != "")
                            {
                                if (DateTime.Now < Convert.ToDateTime(dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString()).AddSeconds(30))
                                {
                                    //MessageBox.Show("此航班上次查询时间：" +
                                    //    dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString() +
                                    //    "，间隔30秒后才会重新提取数据！", "任务书查询");

                                    this.m_statusBar.Panels[2].Text = "【" + inChangeLegsBM.FLTID.Replace(" ", "") + "】航班上次查询时间：" +
                                        dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString().Trim() + "，间隔30秒后才会重新提取任务书数据！";
                                    return;
                                }
                            }

                        }
                    }
                    #endregion 判断前后两次查询同一航班的时间间隔，控制访问任务书服务的频率
                    if (inChangeLegsBM != null)
                    {
                        dataTable_1 = taskBookServiceBF.GetVoyageReportDataBySingleFlight_In(
                            inChangeLegsBM.FlightDate,   //inChangeLegsBM.DATOP, -- modified by LinYong in 20150330
                            inChangeLegsBM.FlightNo.Replace(" ", ""),
                            inChangeLegsBM.LONG_REG,
                            (inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN),
                            inChangeLegsBM.STD.Substring(0, 16),
                            inChangeLegsBM.STA.Substring(0, 16)).Dt;

                        strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //记录当前操作时间
                    }


                    //dataGridView1.DataSource = dataTable_1;

                    #region 不绑定数据源，手动添加信息 到 DataGridView
                    //添加列
                    dataGridView1.Columns.Clear();
                    foreach (DataColumn dataColumndataTable_1 in dataTable_1.Columns)
                    {
                        dataGridView1.Columns.Add(dataColumndataTable_1.ColumnName, dataColumndataTable_1.Caption);
                    }
                    //添加行
                    dataGridView1.Rows.Clear();
                    foreach (DataRow dataRowdataTable_1 in dataTable_1.Rows)
                    {
                        //
                        dataRowdataTable_1["cnvcExecFlightInfo"] = dataRowdataTable_1["cnvcExecFlightInfo"].ToString().Replace("； ", ("；" + Environment.NewLine));

                        //
                        dataGridView1.Rows.Add(dataRowdataTable_1.ItemArray);
                    }


                    #endregion 不绑定数据源，手动添加信息 到 DataGridView


                    #region 冻结 合计行
                    dataGridView1.Rows[0].Frozen = true;
                    #endregion 冻结 合计行


                    #region 网格标题
                    //单元格换行
                    if (chkinTaskbookConfiguration_NewLine.Checked)
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    else
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                    //设置行高
                    foreach (DataGridViewRow dataGridViewRowdataGridView1 in dataGridView1.Rows)
                    {
                        if (dataGridViewRowdataGridView1.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                        {
                            if (chkinTaskbookConfiguration_NewLine.Checked)
                                dataGridViewRowdataGridView1.Height = 80;
                            else
                                dataGridViewRowdataGridView1.Height = 17;

                        }
                    }

                    //foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
                    //{
                    //    dataGridViewColumn.HeaderText = ((DataTable)dataGridView1.DataSource).Columns[ dataGridViewColumn.DataPropertyName].Caption;
                    //}
                    foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
                    {
                        //switch (dataGridViewColumn.DataPropertyName.ToUpper())
                        switch (dataGridViewColumn.Name.ToUpper())
                        {
                            case "CNICREWINFOID":
                                dataGridViewColumn.HeaderText = "序号";
                                dataGridViewColumn.Width = 55;
                                //dataGridViewColumn.Visible = false;
                                break;
                            case "CNVCPOSITION":
                                dataGridViewColumn.HeaderText = "位置 ";
                                dataGridViewColumn.Width = 140;
                                break;
                            case "CNVCNAME":
                                dataGridViewColumn.HeaderText = "姓名 ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCLEVEL":
                                dataGridViewColumn.HeaderText = "级别 ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCSID":
                                dataGridViewColumn.HeaderText = "工作证号 ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCDEPARRSTN":
                                dataGridViewColumn.HeaderText = "搭乘航段 ";
                                dataGridViewColumn.Width = 120;
                                break;
                            case "CNVCMOBILE":
                                dataGridViewColumn.HeaderText = "联系电话 ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCEXECFLIGHTINFO":
                                dataGridViewColumn.HeaderText = "执行航班（下段） ";
                                if (chkinTaskbookConfiguration_NewLine.Checked)
                                    dataGridViewColumn.Width = 500;
                                else
                                    dataGridViewColumn.Width = 720;

                                dataGridViewColumn.DisplayIndex = 4;
                                break;
                            case "CNVCMEMO":
                                dataGridViewColumn.HeaderText = "备注 ";
                                break;
                        }
                    }
                    #endregion 网格标题


                    #region 比较 并 保存 航前任务书
                    //创建 航前任务书对象
                    if (_dataTableTaskBooks == null)    //null，表示还未创建对象，则创建对象
                    {
                        #region 生成表格
                        try
                        {
                            _dataTableTaskBooks = new DataTable();
                            DataColumn dataColumn = null;

                            dataColumn = new DataColumn("cnvcDATOP", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "航班日期";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcFlightNo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "航班号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLONG_REG", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "飞机号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSTD", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "起飞时间";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcROUTE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "航段";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
                            dataColumn.Caption = "序号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "位置";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "姓名";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "级别";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "工作证号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "搭乘航段";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "联系电话";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "执行航班（上段或下段）";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "备注";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcOperateTime", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "操作时间";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                        }
                        catch (Exception ex)
                        {
                            _dataTableTaskBooks = null;
                            throw new Exception(ex.Message);
                        }
                        #endregion 生成表格
                    }

                    #region 处理进港航班
                    #region 现版本和原版本进行比较
                    if ((inChangeLegsBM != null) && (dataTable_1 != null) && (dataTable_1.Columns.Count > 0))
                    {
                        DataTable dataTableCompareIn = dataTable_1.Copy();  //现版本数据
                        dataTableCompareIn.Columns.Add("cnvcCompareResult", Type.GetType("System.String"));  //增加 比较结果列


                        foreach (DataRow dataRow_dataTableTaskBooks in _dataTableTaskBooks.Select("(cnvcDATOP = '" + inChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + inChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + inChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + inChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN + "') "))  //遍历原版本
                        {
                            //上次查询时间
                            strLastTime = dataRow_dataTableTaskBooks["cnvcOperateTime"].ToString();

                            //
                            if (dataTable_1.Select("(cnvcSID = '" + dataRow_dataTableTaskBooks["cnvcSID"].ToString() + "') ").Length == 0)  //判断 现版本 中 是否 存在 原版本数据
                            {
                                DataRow dataRowdataTableCompareIn = dataTableCompareIn.NewRow();

                                dataRowdataTableCompareIn["cniCrewInfoId"] = Convert.ToInt32(dataRow_dataTableTaskBooks["cniCrewInfoId"].ToString());
                                dataRowdataTableCompareIn["cnvcPosition"] = dataRow_dataTableTaskBooks["cnvcPosition"].ToString();
                                dataRowdataTableCompareIn["cnvcName"] = dataRow_dataTableTaskBooks["cnvcName"].ToString();
                                dataRowdataTableCompareIn["cnvcLevel"] = dataRow_dataTableTaskBooks["cnvcLevel"].ToString();
                                dataRowdataTableCompareIn["cnvcSID"] = dataRow_dataTableTaskBooks["cnvcSID"].ToString();
                                dataRowdataTableCompareIn["cnvcDepArrStn"] = dataRow_dataTableTaskBooks["cnvcDepArrStn"].ToString();
                                dataRowdataTableCompareIn["cnvcMOBILE"] = dataRow_dataTableTaskBooks["cnvcMOBILE"].ToString();
                                dataRowdataTableCompareIn["cnvcExecFlightInfo"] = dataRow_dataTableTaskBooks["cnvcExecFlightInfo"].ToString();
                                dataRowdataTableCompareIn["cnvcMemo"] = dataRow_dataTableTaskBooks["cnvcMemo"].ToString();
                                dataRowdataTableCompareIn["cnvcCompareResult"] = "删除";

                                dataTableCompareIn.Rows.Add(dataRowdataTableCompareIn);
                            }
                        }


                        foreach (DataRow dataRowdataTableCompareIn in dataTableCompareIn.Rows)  //遍历现版本
                        {
                            if (_dataTableTaskBooks.Select("(cnvcDATOP = '" + inChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + inChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + inChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + inChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN + "') and " +
                            "(cnvcSID = '" + dataRowdataTableCompareIn["cnvcSID"].ToString() + "') ").Length > 0)  //判断 原版本 中 是否 存在 现版本数据
                            {
                                dataRowdataTableCompareIn["cnvcCompareResult"] = "不变";
                            }
                            else
                            {
                                if (strLastTime != "")
                                    dataRowdataTableCompareIn["cnvcCompareResult"] = "增加";
                                else
                                    dataRowdataTableCompareIn["cnvcCompareResult"] = "";

                            }
                        }



                        //更改合计行信息
                        if (dataTableCompareIn.Select("cnvcPosition = '合计'").Length == 1)
                        {
                            dataTableCompareIn.Select("cnvcPosition = '合计'")[0]["cnvcExecFlightInfo"] = "上次查询：" + strLastTime + " -- 本次查询：" + strCurrentTime;
                            dataTableCompareIn.Select("cnvcPosition = '合计'")[0]["cnvcCompareResult"] = "";
                        }

                        //
                        //dataGridView3.DataSource = dataTableCompareIn;

                        #region 不绑定数据源，手动添加信息 到 DataGridView
                        //添加列
                        dataGridView3.Columns.Clear();
                        foreach (DataColumn dataColumndataTableCompareIn in dataTableCompareIn.Columns)
                        {
                            dataGridView3.Columns.Add(dataColumndataTableCompareIn.ColumnName, dataColumndataTableCompareIn.Caption);
                        }
                        //添加行
                        dataGridView3.Rows.Clear();
                        foreach (DataRow dataRowdataTableCompareIn in dataTableCompareIn.Rows)
                        {
                            dataGridView3.Rows.Add(dataRowdataTableCompareIn.ItemArray);
                        }


                        #endregion 不绑定数据源，手动添加信息 到 DataGridView


                        #region 冻结 合计行
                        dataGridView3.Rows[0].Frozen = true;
                        #endregion 冻结 合计行

                        #region 网格标题
                        //单元格换行
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        else
                            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                        //设置行高
                        foreach (DataGridViewRow dataGridViewRowdataGridView3 in dataGridView3.Rows)
                        {
                            if (dataGridViewRowdataGridView3.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                            {
                                if (chkinTaskbookConfiguration_NewLine.Checked)
                                    dataGridViewRowdataGridView3.Height = 80;
                                else
                                    dataGridViewRowdataGridView3.Height = 17;
                            }
                        }

                        //foreach (DataGridViewColumn dataGridViewColumn in dataGridView3.Columns)
                        //{
                        //    dataGridViewColumn.HeaderText = ((DataTable)dataGridView3.DataSource).Columns[dataGridViewColumn.DataPropertyName].Caption;
                        //}

                        foreach (DataGridViewColumn dataGridViewColumn in dataGridView3.Columns)
                        {
                            switch (dataGridViewColumn.Name.ToUpper())
                            {
                                case "CNICREWINFOID":
                                    dataGridViewColumn.HeaderText = "序号";
                                    dataGridViewColumn.Width = 45;
                                    //dataGridViewColumn.Visible = false;
                                    break;
                                case "CNVCPOSITION":
                                    dataGridViewColumn.HeaderText = "位置 ";
                                    dataGridViewColumn.Width = 140;
                                    break;
                                case "CNVCNAME":
                                    dataGridViewColumn.HeaderText = "姓名 ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCLEVEL":
                                    dataGridViewColumn.HeaderText = "级别 ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCSID":
                                    dataGridViewColumn.HeaderText = "工作证号 ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCDEPARRSTN":
                                    dataGridViewColumn.HeaderText = "搭乘航段 ";
                                    dataGridViewColumn.Width = 120;
                                    break;
                                case "CNVCMOBILE":
                                    dataGridViewColumn.HeaderText = "联系电话 ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCEXECFLIGHTINFO":
                                    dataGridViewColumn.HeaderText = "执行航班（下段） ";
                                    if (chkinTaskbookConfiguration_NewLine.Checked)
                                        dataGridViewColumn.Width = 500;
                                    else
                                        dataGridViewColumn.Width = 720;

                                    dataGridViewColumn.DisplayIndex = 4;
                                    break;
                                case "CNVCMEMO":
                                    dataGridViewColumn.HeaderText = "备注 ";
                                    break;
                                case "CNVCCOMPARERESULT":
                                    dataGridViewColumn.HeaderText = "比较结果 ";
                                    dataGridViewColumn.Width = 60;
                                    dataGridViewColumn.DisplayIndex = 1;
                                    break;
                            }
                        }

                        #endregion 网格标题

                    }
                    else
                    {
                        dataGridView3.DataSource = null;

                    }
                    #endregion 现版本和原版本进行比较

                    #region 删除 航班 原数据
                    if (inChangeLegsBM != null)
                    {
                        DataRow[] dataRows_dataTableTaskBooks = _dataTableTaskBooks.Select("(cnvcDATOP = '" + inChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + inChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + inChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + inChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN + "') ");

                        foreach (DataRow dataRowdataRows_dataTableTaskBooks in dataRows_dataTableTaskBooks)
                            _dataTableTaskBooks.Rows.Remove(dataRowdataRows_dataTableTaskBooks);
                    }
                    #endregion 删除 航班 原数据


                    #region 保存 航班 航前任务书数据
                    foreach (DataRow dataRowFlightIn in dataTable_1.Rows)
                    {
                        DataRow dataRow_dataTableTaskBooks = _dataTableTaskBooks.NewRow();

                        dataRow_dataTableTaskBooks["cnvcDATOP"] = inChangeLegsBM.DATOP;
                        dataRow_dataTableTaskBooks["cnvcFlightNo"] = inChangeLegsBM.FlightNo;
                        dataRow_dataTableTaskBooks["cnvcLONG_REG"] = inChangeLegsBM.LONG_REG;
                        dataRow_dataTableTaskBooks["cnvcSTD"] = inChangeLegsBM.STD;
                        dataRow_dataTableTaskBooks["cnvcROUTE"] = inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN;

                        dataRow_dataTableTaskBooks["cniCrewInfoId"] = Convert.ToInt32(dataRowFlightIn["cniCrewInfoId"].ToString());
                        dataRow_dataTableTaskBooks["cnvcPosition"] = dataRowFlightIn["cnvcPosition"].ToString();
                        dataRow_dataTableTaskBooks["cnvcName"] = dataRowFlightIn["cnvcName"].ToString();
                        dataRow_dataTableTaskBooks["cnvcLevel"] = dataRowFlightIn["cnvcLevel"].ToString();
                        dataRow_dataTableTaskBooks["cnvcSID"] = dataRowFlightIn["cnvcSID"].ToString();
                        dataRow_dataTableTaskBooks["cnvcDepArrStn"] = dataRowFlightIn["cnvcDepArrStn"].ToString();
                        dataRow_dataTableTaskBooks["cnvcMOBILE"] = dataRowFlightIn["cnvcMOBILE"].ToString();
                        dataRow_dataTableTaskBooks["cnvcExecFlightInfo"] = dataRowFlightIn["cnvcExecFlightInfo"].ToString();
                        dataRow_dataTableTaskBooks["cnvcMemo"] = dataRowFlightIn["cnvcMemo"].ToString();
                        dataRow_dataTableTaskBooks["cnvcOperateTime"] = strCurrentTime;

                        _dataTableTaskBooks.Rows.Add(dataRow_dataTableTaskBooks);
                    }
                    #endregion 保存 航班 航前任务书数据


                    #endregion 处理进港航班

                    #endregion 比较 并 保存 航前任务书


                    #region 任务书（运行网）
                    if ((inChangeLegsBM != null) && (dataTable_1 != null) && (dataTable_1.Columns.Count > 0))
                    {
                        DataRow[] dataRowsdataTable_1 = dataTable_1.Select("cnvcPosition = '合计'");
                        if (dataRowsdataTable_1.Length == 1)
                        {
                            int intPos_1 = -1, intPos_2 = -1;
                            string strFltReportId = "";


                            intPos_1 = dataRowsdataTable_1[0]["cnvcExecFlightInfo"].ToString().IndexOf("【任务书ID：");
                            intPos_2 = dataRowsdataTable_1[0]["cnvcExecFlightInfo"].ToString().IndexOf("】");
                            if (intPos_2 > (intPos_1 + "【任务书ID：".Length))
                            {
                                strFltReportId = dataRowsdataTable_1[0]["cnvcExecFlightInfo"].ToString().Substring((intPos_1 + "【任务书ID：".Length), (intPos_2 - intPos_1 - "【任务书ID：".Length));
                                string strWebBrowser = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/CrewFltTaskNew.aspx?cniId=" +
                                    strFltReportId;
                                webBrowser1.Navigate(strWebBrowser);
                            }
                        }
                    }
                    #endregion 任务书（运行网）


                }
                catch (Exception ex)
                {
                    string errMessage = ex.Message;
                }

            }
            #endregion

            #region 进港旅客信息处理
            if (intabControl.SelectedTab.Name.ToUpper() == "inPSRtabPage".ToUpper())
            {
                //权限判断 -- modified by LinYong in 20160316
                if (GetDataItemPurview("IncntPaxNameList") < 2)
                {
                    #region
                    inbabylabel.Text = "";
                    inbirthlabel.Text = "";
                    inchildlabel.Text = "";
                    inviplabel.Text = "";
                    inboardlabel.Text = "";
                    inFYlabel.Text = "";
                    insumlabel.Text = "";
                    injinlabel.Text = "";
                    injplabel.Text = "";
                    inmeallabel.Text = "";
                    insslabel.Text = "";
                    intstlabel.Text = "";
                    #endregion

                    //intabControl.SelectedTab = inTaskBooktabPage;
                    intabControl.SelectedIndex = 1;

                    MessageBox.Show("对不起，您没有查看旅客信息的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                //进港旅客tab标签标题显示
                intabControl_PSR_SelectedIndexChanged(sender, e);

                if (lastInChangeLegsBM != null && lastOutChangeLegsBM != null && (lastSelectedRow == m_iOldSelectedRow)
                    && (DateTime.Now < lastSearchDateTime.AddSeconds(30)))
                {
                    //MessageBox.Show("此航班上次查询时间：" +
                    //                    lastSearchDateTime.ToString("yyyy-MM-dd HH:mm:ss") +
                    //                    "，间隔30秒后才会重新提取数据！", "进港旅客数据查询");
                    this.m_statusBar.Panels[2].Text = "【" + lastInChangeLegsBM.FLTID.Replace(" ","") + "】航班上次查询时间：" + lastSearchDateTime.ToString("HH:mm:ss") + "，间隔30秒后才会重新提取旅客数据！";
                }
                else
                {
                    DateTime thisSearchDateTime = DateTime.Now;
                    DataSet inDataSet = new DataSet();
                    DataSet zjDataSet = new DataSet();
                    if (inChangeLegsBM.FLTID.Equals("HU 0000") || outChangeLegsBM == null)
                    {
                        //to do;要将页面表格的值初始化
                        return;
                    }
                    #region 进港航班旅客数据展示
                    zjDataSet = traveller.getTravellerSumDataSet(inChangeLegsBM.FLTID.Replace(" ", ""), inChangeLegsBM.DATOP, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN);
                    if (zjDataSet == null || zjDataSet.Tables.Count == 0)
                    {
                        return;
                    }
                    inbabylabel.Text = (int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_YINFANT_NUM"].ToString()) +
                        int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_FINFANT_NUM"].ToString()) +
                        int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_CINFANT_NUM"].ToString())).ToString();
                    inbirthlabel.Text = (int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_YBIRTH_NUM"].ToString()) +
                        int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_FBIRTH_NUM"].ToString()) +
                        int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_CBIRTH_NUM"].ToString())).ToString();
                    inchildlabel.Text = (int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_YCHILD_NUM"].ToString()) +
                        int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_FCHILD_NUM"].ToString()) +
                        int.Parse(zjDataSet.Tables[0].Rows[0]["SEG_CCHILD_NUM"].ToString())).ToString();
                    inviplabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_VIPGO_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_CIPGO_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_M6GO_NUM"].ToString();
                    inboardlabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_BOARD1"].ToString();
                    inFYlabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_FGO_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_CGO_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_YGO_NUM"].ToString();
                    insumlabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_ET_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_CREWGO_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_ETGO_NUM"].ToString();
                    injinlabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_JPGO_GOLDEN_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_JPGO_SILVER_NUM"].ToString();
                    injplabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_JLGO_NUM"].ToString();
                    inmeallabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_SPEMEAL_NUM"].ToString();
                    insslabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_SPEPSR_NUM"].ToString();
                    intstlabel.Text = zjDataSet.Tables[0].Rows[0]["SEG_INBOUND_GO_NUM"].ToString() + "/" + zjDataSet.Tables[0].Rows[0]["SEG_OUTBOUND_GO_NUM"].ToString();

                    //进港航班旅客数据处理
                    inDataSet = traveller.getTravellerDataSet(inChangeLegsBM.FLTID.Replace(" ", ""), inChangeLegsBM.DATOP, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN);
                    if (inDataSet == null || inDataSet.Tables.Count == 0)
                    {
                        return;
                    }
                    t_inALLTraveller = inDataSet.Tables[0];
                    t_inVipTraveller = t_inALLTraveller.Select("PSR_M6_MARK <> 0 or PSR_SUSPECTEDVIP_MARK <> 0 ");
                    t_inJINTraveller = t_inALLTraveller.Select("PSR_FFP_MARK <> 0");
                    t_inMealTraveller = t_inALLTraveller.Select("PSR_MEAL_MARK = 1");
                    t_inOTHTTraveller = t_inALLTraveller.Select("PSR_M6_MARK = 0 and PSR_SUSPECTEDVIP_MARK = 0 and PSR_FFP_MARK = 0 and PSR_MEAL_MARK = 0 and PSR_SS_MARK = 0 and PSR_INFANT_MARK = 0 and PSRD_IN_STN is NULL and PSRD_OUT_STN is NULL");
                    t_inSsTraveller = t_inALLTraveller.Select("PSR_SS_MARK <> 0 or PSR_INFANT_MARK = 1 ");
                    t_inTSTTraveller = t_inALLTraveller.Select("PSRD_IN_STN is not NULL or PSRD_OUT_STN is not NULL");

                    showListViewData(intVIPListView, t_inVipTraveller);//要客信息
                    showListViewData(inSSListView, t_inSsTraveller);//特服
                    showListViewData(inMealListView, t_inMealTraveller);//特餐
                    showListViewData(inTSTListView, t_inTSTTraveller);//中转
                    showListViewData(inclistView, t_inJINTraveller);//常旅客
                    showListViewData(inplistView, t_inOTHTTraveller);//普通旅客

                    inVipTabPage.Text = "要客信息(" + t_inVipTraveller.Length.ToString() + "人)";
                    inSSTabPage.Text = "特服旅客信息(" + t_inSsTraveller.Length.ToString() + "人)";
                    inMealTabPage.Text = "特餐旅客信息(" + t_inMealTraveller.Length.ToString() + "人)";
                    inTSTabPage.Text = "中转旅客信息(" + t_inTSTTraveller.Length.ToString() + "人)";
                    inOTHTabPage.Text = "普通旅客信息(" + t_inOTHTTraveller.Length.ToString() + "人)";
                    inJINTabPage.Text = "常旅客信息(" + t_inJINTraveller.Length.ToString() + "人)";

                    //保存上次查询时间
                    lastSearchDateTime = thisSearchDateTime;
                    lastInChangeLegsBM = inChangeLegsBM;
                    lastSelectedRow = m_iOldSelectedRow;
                    //}
                    #endregion
                }
            }
            #endregion

        }

        #region 设置TabControl标签
        private void intabControl_PSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(intabControl_PSR);
        }
        #endregion

        private void outtabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(outtabControl);

            #region 出港航班任务书处理
            if (outtabControl.SelectedTab.Name.ToUpper() == "outTaskBooktabPage".ToUpper())
            {
                //标签标题设置
                outtabControl_Taskbook_SelectedIndexChanged(sender, e);
                try
                {
                    ReturnValueSF returnValueSF = new ReturnValueSF();
                    TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
                    string strCurrentTime = ""; //当前查询时刻
                    string strLastTime = ""; //上次查询时刻

                    DataTable dataTable_2 = null;


                    #region 判断前后两次查询同一航班的时间间隔，控制访问任务书服务的频率
                    if ((_dataTableTaskBooks != null) && (outChangeLegsBM != null))
                    {

                        DataRow[] dataRows_dataTableTaskBooks_1 = _dataTableTaskBooks.Select("(cnvcDATOP = '" + outChangeLegsBM.DATOP + "') and " +
                                "(cnvcFlightNo = '" + outChangeLegsBM.FlightNo + "') and " +
                                "(cnvcLONG_REG = '" + outChangeLegsBM.LONG_REG + "') and " +
                                "(cnvcSTD = '" + outChangeLegsBM.STD + "') and " +
                                "(cnvcROUTE = '" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN + "') ");

                        if (dataRows_dataTableTaskBooks_1.Length > 0)
                        {
                            //
                            if (dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString().Trim() != "")
                            {
                                if (DateTime.Now < Convert.ToDateTime(dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString()).AddSeconds(30))
                                {
                                    //MessageBox.Show("此航班上次查询时间：" +
                                    //    dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString() +
                                    //    "，间隔30秒后才会重新提取数据！", "任务书查询");

                                    this.m_statusBar.Panels[2].Text = "【" + outChangeLegsBM.FLTID.Replace(" ", "") + "】航班上次查询时间：" +
                                        dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString().Trim() + "，间隔30秒后才会重新提取任务书数据！";

                                    return;
                                }
                            }

                        }
                    }
                    #endregion 判断前后两次查询同一航班的时间间隔，控制访问任务书服务的频率


                    if (outChangeLegsBM != null)
                    {
                        dataTable_2 = taskBookServiceBF.GetVoyageReportDataBySingleFlight_Out(
                            outChangeLegsBM.FlightDate,  //outChangeLegsBM.DATOP, -- modified by LinYong in 20150330
                            outChangeLegsBM.FlightNo.Replace(" ", ""),
                            outChangeLegsBM.LONG_REG,
                            (outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN),
                            outChangeLegsBM.STD.Substring(0, 16),
                            outChangeLegsBM.STA.Substring(0, 16)).Dt;

                        strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //记录当前操作时间

                    }

                    //dataGridView2.DataSource = dataTable_2;

                    #region 不绑定数据源，手动添加信息 到 DataGridView
                    //添加列
                    dataGridView2.Columns.Clear();
                    foreach (DataColumn dataColumndataTable_2 in dataTable_2.Columns)
                    {
                        dataGridView2.Columns.Add(dataColumndataTable_2.ColumnName, dataColumndataTable_2.Caption);
                    }
                    //添加行
                    dataGridView2.Rows.Clear();
                    foreach (DataRow dataRowdataTable_2 in dataTable_2.Rows)
                    {
                        //
                        dataRowdataTable_2["cnvcExecFlightInfo"] = dataRowdataTable_2["cnvcExecFlightInfo"].ToString().Replace("； ", ("；" + Environment.NewLine));
                        //
                        dataGridView2.Rows.Add(dataRowdataTable_2.ItemArray);
                    }


                    #endregion 不绑定数据源，手动添加信息 到 DataGridView


                    #region 网格标题
                    //单元格换行
                    if (chkoutTaskbookConfiguration_NewLine.Checked)
                        dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    else
                        dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                    //设置行高
                    foreach (DataGridViewRow dataGridViewRowdataGridView2 in dataGridView2.Rows)
                    {
                        if (dataGridViewRowdataGridView2.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                        {
                            if (chkoutTaskbookConfiguration_NewLine.Checked)
                                dataGridViewRowdataGridView2.Height = 80;
                            else
                                dataGridViewRowdataGridView2.Height = 17;
                        }
                    }

                    foreach (DataGridViewColumn dataGridViewColumn in dataGridView2.Columns)
                    {
                        switch (dataGridViewColumn.Name.ToUpper())
                        {
                            case "CNICREWINFOID":
                                dataGridViewColumn.HeaderText = "序号";
                                dataGridViewColumn.Width = 55;
                                //dataGridViewColumn.Visible = false;
                                break;
                            case "CNVCPOSITION":
                                dataGridViewColumn.HeaderText = "位置 ";
                                dataGridViewColumn.Width = 140;
                                break;
                            case "CNVCNAME":
                                dataGridViewColumn.HeaderText = "姓名 ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCLEVEL":
                                dataGridViewColumn.HeaderText = "级别 ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCSID":
                                dataGridViewColumn.HeaderText = "工作证号 ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCDEPARRSTN":
                                dataGridViewColumn.HeaderText = "搭乘航段 ";
                                dataGridViewColumn.Width = 120;
                                break;
                            case "CNVCMOBILE":
                                dataGridViewColumn.HeaderText = "联系电话 ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCEXECFLIGHTINFO":
                                dataGridViewColumn.HeaderText = "执行航班（上段） ";
                                if (chkoutTaskbookConfiguration_NewLine.Checked)
                                    dataGridViewColumn.Width = 500;
                                else
                                    dataGridViewColumn.Width = 720;

                                dataGridViewColumn.DisplayIndex = 4;
                                break;
                            case "CNVCMEMO":
                                dataGridViewColumn.HeaderText = "备注 ";
                                break;
                            case "CNVCCOMPARERESULT":
                                dataGridViewColumn.HeaderText = "比较结果 ";
                                dataGridViewColumn.Width = 60;
                                break;
                        }
                    }

                    #endregion 网格标题

                    #region 冻结 合计行
                    dataGridView2.Rows[0].Frozen = true;
                    #endregion 冻结 合计行

                    #region 比较 并 保存 航前任务书
                    //创建 航前任务书对象
                    if (_dataTableTaskBooks == null)    //null，表示还未创建对象，则创建对象
                    {
                        #region 生成表格
                        try
                        {
                            _dataTableTaskBooks = new DataTable();
                            DataColumn dataColumn = null;

                            dataColumn = new DataColumn("cnvcDATOP", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "航班日期";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcFlightNo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "航班号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLONG_REG", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "飞机号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSTD", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "起飞时间";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcROUTE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "航段";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
                            dataColumn.Caption = "序号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "位置";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "姓名";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "级别";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "工作证号";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "搭乘航段";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "联系电话";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "执行航班（上段或下段）";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "备注";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcOperateTime", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "操作时间";
                            _dataTableTaskBooks.Columns.Add(dataColumn);
                        }
                        catch (Exception ex)
                        {
                            _dataTableTaskBooks = null;
                            throw new Exception(ex.Message);
                        }
                        #endregion 生成表格
                    }

                    #region 处理出港航班
                    #region 现版本和原版本进行比较
                    if ((outChangeLegsBM != null) && (dataTable_2 != null) && (dataTable_2.Columns.Count > 0))
                    {
                        DataTable dataTableCompareOut = dataTable_2.Copy();  //现版本数据
                        dataTableCompareOut.Columns.Add("cnvcCompareResult", Type.GetType("System.String"));  //增加 比较结果列

                        foreach (DataRow dataRow_dataTableTaskBooks in _dataTableTaskBooks.Select("(cnvcDATOP = '" + outChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + outChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + outChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + outChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN + "') "))  //遍历原版本
                        {
                            //上次查询时间
                            strLastTime = dataRow_dataTableTaskBooks["cnvcOperateTime"].ToString();

                            if (dataTable_2.Select("(cnvcSID = '" + dataRow_dataTableTaskBooks["cnvcSID"].ToString() + "') ").Length == 0)  //判断 现版本 中 是否 存在 原版本数据
                            {
                                DataRow dataRowdataTableCompareOut = dataTableCompareOut.NewRow();

                                dataRowdataTableCompareOut["cniCrewInfoId"] = Convert.ToInt32(dataRow_dataTableTaskBooks["cniCrewInfoId"].ToString());
                                dataRowdataTableCompareOut["cnvcPosition"] = dataRow_dataTableTaskBooks["cnvcPosition"].ToString();
                                dataRowdataTableCompareOut["cnvcName"] = dataRow_dataTableTaskBooks["cnvcName"].ToString();
                                dataRowdataTableCompareOut["cnvcLevel"] = dataRow_dataTableTaskBooks["cnvcLevel"].ToString();
                                dataRowdataTableCompareOut["cnvcSID"] = dataRow_dataTableTaskBooks["cnvcSID"].ToString();
                                dataRowdataTableCompareOut["cnvcDepArrStn"] = dataRow_dataTableTaskBooks["cnvcDepArrStn"].ToString();
                                dataRowdataTableCompareOut["cnvcMOBILE"] = dataRow_dataTableTaskBooks["cnvcMOBILE"].ToString();
                                dataRowdataTableCompareOut["cnvcExecFlightInfo"] = dataRow_dataTableTaskBooks["cnvcExecFlightInfo"].ToString();
                                dataRowdataTableCompareOut["cnvcMemo"] = dataRow_dataTableTaskBooks["cnvcMemo"].ToString();
                                dataRowdataTableCompareOut["cnvcCompareResult"] = "删除";

                                dataTableCompareOut.Rows.Add(dataRowdataTableCompareOut);
                            }
                        }

                        foreach (DataRow dataRowdataTableCompareOut in dataTableCompareOut.Rows)  //遍历现版本
                        {
                            if (_dataTableTaskBooks.Select("(cnvcDATOP = '" + outChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + outChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + outChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + outChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN + "') and " +
                            "(cnvcSID = '" + dataRowdataTableCompareOut["cnvcSID"].ToString() + "') ").Length > 0)  //判断 原版本 中 是否 存在 现版本数据
                            {
                                dataRowdataTableCompareOut["cnvcCompareResult"] = "不变";
                            }
                            else
                            {
                                if (strLastTime != "")
                                    dataRowdataTableCompareOut["cnvcCompareResult"] = "增加";
                                else
                                    dataRowdataTableCompareOut["cnvcCompareResult"] = "";

                            }
                        }

                        //更改合计行信息
                        if (dataTableCompareOut.Select("cnvcPosition = '合计'").Length == 1)
                        {
                            dataTableCompareOut.Select("cnvcPosition = '合计'")[0]["cnvcExecFlightInfo"] = "上次查询：" + strLastTime + " -- 本次查询：" + strCurrentTime;
                            dataTableCompareOut.Select("cnvcPosition = '合计'")[0]["cnvcCompareResult"] = "";
                        }

                        //
                        //dataGridView4.DataSource = dataTableCompareOut;


                        #region 不绑定数据源，手动添加信息 到 DataGridView
                        //添加列
                        dataGridView4.Columns.Clear();
                        foreach (DataColumn dataColumndataTableCompareOut in dataTableCompareOut.Columns)
                        {
                            dataGridView4.Columns.Add(dataColumndataTableCompareOut.ColumnName, dataColumndataTableCompareOut.Caption);
                        }
                        //添加行
                        dataGridView4.Rows.Clear();
                        foreach (DataRow dataRowdataTableCompareOut in dataTableCompareOut.Rows)
                        {
                            dataGridView4.Rows.Add(dataRowdataTableCompareOut.ItemArray);
                        }
                        #endregion 不绑定数据源，手动添加信息 到 DataGridView


                        #region 冻结 合计行
                        dataGridView4.Rows[0].Frozen = true;
                        #endregion 冻结 合计行

                        #region 网格标题
                        //单元格换行
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        else
                            dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                        //设置行高
                        foreach (DataGridViewRow dataGridViewRowdataGridView4 in dataGridView4.Rows)
                        {
                            if (dataGridViewRowdataGridView4.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                            {
                                if (chkoutTaskbookConfiguration_NewLine.Checked)
                                    dataGridViewRowdataGridView4.Height = 80;
                                else
                                    dataGridViewRowdataGridView4.Height = 17;
                            }
                        }

                        foreach (DataGridViewColumn dataGridViewColumn in dataGridView4.Columns)
                        {
                            switch (dataGridViewColumn.Name.ToUpper())
                            {
                                case "CNICREWINFOID":
                                    dataGridViewColumn.HeaderText = "序号";
                                    dataGridViewColumn.Width = 45;
                                    //dataGridViewColumn.Visible = false;
                                    break;
                                case "CNVCPOSITION":
                                    dataGridViewColumn.HeaderText = "位置 ";
                                    dataGridViewColumn.Width = 140;
                                    break;
                                case "CNVCNAME":
                                    dataGridViewColumn.HeaderText = "姓名 ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCLEVEL":
                                    dataGridViewColumn.HeaderText = "级别 ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCSID":
                                    dataGridViewColumn.HeaderText = "工作证号 ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCDEPARRSTN":
                                    dataGridViewColumn.HeaderText = "搭乘航段 ";
                                    dataGridViewColumn.Width = 120;
                                    break;
                                case "CNVCMOBILE":
                                    dataGridViewColumn.HeaderText = "联系电话 ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCEXECFLIGHTINFO":
                                    dataGridViewColumn.HeaderText = "执行航班（上段） ";
                                    if (chkoutTaskbookConfiguration_NewLine.Checked)
                                        dataGridViewColumn.Width = 500;
                                    else
                                        dataGridViewColumn.Width = 720;

                                    dataGridViewColumn.DisplayIndex = 4;
                                    break;
                                case "CNVCMEMO":
                                    dataGridViewColumn.HeaderText = "备注 ";
                                    break;
                                case "CNVCCOMPARERESULT":
                                    dataGridViewColumn.HeaderText = "比较结果 ";
                                    dataGridViewColumn.Width = 60;
                                    dataGridViewColumn.DisplayIndex = 1;
                                    break;
                            }
                        }

                        #endregion 网格标题
                    }
                    else
                    {
                        dataGridView4.DataSource = null;
                    }
                    #endregion 现版本和原版本进行比较

                    #region 删除 航班 原数据
                    if (outChangeLegsBM != null)
                    {
                        DataRow[] dataRows_dataTableTaskBooks = _dataTableTaskBooks.Select("(cnvcDATOP = '" + outChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + outChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + outChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + outChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN + "') ");

                        foreach (DataRow dataRowdataRows_dataTableTaskBooks in dataRows_dataTableTaskBooks)
                            _dataTableTaskBooks.Rows.Remove(dataRowdataRows_dataTableTaskBooks);
                    }
                    #endregion 删除 航班 原数据

                    #region 保存 航班 航前任务书数据
                    foreach (DataRow dataRowFlightIn in dataTable_2.Rows)
                    {
                        DataRow dataRow_dataTableTaskBooks = _dataTableTaskBooks.NewRow();

                        dataRow_dataTableTaskBooks["cnvcDATOP"] = outChangeLegsBM.DATOP;
                        dataRow_dataTableTaskBooks["cnvcFlightNo"] = outChangeLegsBM.FlightNo;
                        dataRow_dataTableTaskBooks["cnvcLONG_REG"] = outChangeLegsBM.LONG_REG;
                        dataRow_dataTableTaskBooks["cnvcSTD"] = outChangeLegsBM.STD;
                        dataRow_dataTableTaskBooks["cnvcROUTE"] = outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN;

                        dataRow_dataTableTaskBooks["cniCrewInfoId"] = Convert.ToInt32(dataRowFlightIn["cniCrewInfoId"].ToString());
                        dataRow_dataTableTaskBooks["cnvcPosition"] = dataRowFlightIn["cnvcPosition"].ToString();
                        dataRow_dataTableTaskBooks["cnvcName"] = dataRowFlightIn["cnvcName"].ToString();
                        dataRow_dataTableTaskBooks["cnvcLevel"] = dataRowFlightIn["cnvcLevel"].ToString();
                        dataRow_dataTableTaskBooks["cnvcSID"] = dataRowFlightIn["cnvcSID"].ToString();
                        dataRow_dataTableTaskBooks["cnvcDepArrStn"] = dataRowFlightIn["cnvcDepArrStn"].ToString();
                        dataRow_dataTableTaskBooks["cnvcMOBILE"] = dataRowFlightIn["cnvcMOBILE"].ToString();
                        dataRow_dataTableTaskBooks["cnvcExecFlightInfo"] = dataRowFlightIn["cnvcExecFlightInfo"].ToString();
                        dataRow_dataTableTaskBooks["cnvcMemo"] = dataRowFlightIn["cnvcMemo"].ToString();
                        dataRow_dataTableTaskBooks["cnvcOperateTime"] = strCurrentTime;

                        _dataTableTaskBooks.Rows.Add(dataRow_dataTableTaskBooks);
                    }
                    #endregion 保存 航班 航前任务书数据

                    #endregion 处理出港航班


                    #endregion 比较 并 保存 航前任务书


                    #region 任务书（运行网）
                    if ((outChangeLegsBM != null) && (dataTable_2 != null) && (dataTable_2.Columns.Count > 0))
                    {
                        DataRow[] dataRowsdataTable_2 = dataTable_2.Select("cnvcPosition = '合计'");
                        if (dataRowsdataTable_2.Length == 1)
                        {
                            int intPos_1 = -1, intPos_2 = -1;
                            string strFltReportId = "";


                            intPos_1 = dataRowsdataTable_2[0]["cnvcExecFlightInfo"].ToString().IndexOf("【任务书ID：");
                            intPos_2 = dataRowsdataTable_2[0]["cnvcExecFlightInfo"].ToString().IndexOf("】");
                            if (intPos_2 > (intPos_1 + "【任务书ID：".Length))
                            {
                                strFltReportId = dataRowsdataTable_2[0]["cnvcExecFlightInfo"].ToString().Substring((intPos_1 + "【任务书ID：".Length), (intPos_2 - intPos_1 - "【任务书ID：".Length));
                                string strWebBrowser = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/CrewFltTaskNew.aspx?cniId=" +
                                    strFltReportId;
                                webBrowser2.Navigate(strWebBrowser);
                            }
                        }
                    }
                    #endregion 任务书（运行网）



                }
                catch (Exception ex)
                {
                    string errMessage = ex.Message;
                }

            }
            #endregion

            if (outtabControl.SelectedTab.Name.ToUpper() == "outPSRtabPage".ToUpper())
            {
                //权限判断 -- modified by LinYong in 20160316
                if (GetDataItemPurview("OutcntPaxNameList") < 2)
                {
                    #region
                    outbabylabel.Text = "";
                    outbirthlabel.Text = "";
                    outchildlabel.Text = "";
                    outviplabel.Text = "";
                    outboardlabel.Text = "";
                    outFYlabel.Text = "";
                    outsumlabel.Text = "";
                    outjinlabel.Text = "";
                    outjplabel.Text = "";
                    outmeallabel.Text = "";
                    outsslabel.Text = "";
                    outtstlabel.Text = "";
                    #endregion

                    //outtabControl.SelectedTab = outTaskBooktabPage;
                    outtabControl.SelectedIndex = 1;

                    MessageBox.Show("对不起，您没有查看旅客信息的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                //标签标题栏显示
                outTabControl_PSR_SelectedIndexChanged(sender, e);
                #region 出港航班旅客数据展示
                //if (lastInChangeLegsBM != null && lastOutChangeLegsBM != null && (lastOutChangeLegsBM.DATOP == outChangeLegsBM.DATOP && lastOutChangeLegsBM.FLTID == outChangeLegsBM.FLTID && lastOutChangeLegsBM.LONG_REG == outChangeLegsBM.LONG_REG)
                //    && (DateTime.Now < lastSearchDateTime.AddSeconds(30)))
                if (lastInChangeLegsBM != null && lastOutChangeLegsBM != null && (lastSelectedRow == m_iOldSelectedRow)
                    && (DateTime.Now < lastSearchDateTime.AddSeconds(30)))
                {
                    //MessageBox.Show("此航班上次查询时间：" +
                    //                    lastSearchDateTime.ToString("yyyy-MM-dd HH:mm:ss") +
                    //                    "，间隔30秒后才会重新提取数据！", "出港旅客数据查询");
                    this.m_statusBar.Panels[2].Text = "【" + lastOutChangeLegsBM.FLTID.Replace(" ", "") + "】航班上次查询时间：" + lastSearchDateTime.ToString("HH:mm:ss") + "，间隔30秒后才会重新提取旅客数据！";
                }
                else
                {
                    DateTime thisSearchDateTime = DateTime.Now;
                    DataSet outDataSet = new DataSet();
                    DataSet outzjDataSet = new DataSet();
                    if (outChangeLegsBM.FLTID.Equals("HU 0000") || outChangeLegsBM == null)
                    {
                        //to do;要将页面表格的值初始化
                        return;
                    }
                    //各类旅客值机统计
                    outzjDataSet = traveller.getTravellerSumDataSet(outChangeLegsBM.FLTID.Replace(" ", ""), outChangeLegsBM.DATOP, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN);
                    if (outzjDataSet == null || outzjDataSet.Tables.Count == 0)
                    {
                        return;
                    }

                    outbabylabel.Text = (int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_YINFANT_NUM"].ToString()) +
                        int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_FINFANT_NUM"].ToString()) +
                        int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_CINFANT_NUM"].ToString())).ToString();
                    outbirthlabel.Text = (int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_YBIRTH_NUM"].ToString()) +
                        int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_FBIRTH_NUM"].ToString()) +
                        int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_CBIRTH_NUM"].ToString())).ToString();
                    outchildlabel.Text = (int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_YCHILD_NUM"].ToString()) +
                        int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_FCHILD_NUM"].ToString()) +
                        int.Parse(outzjDataSet.Tables[0].Rows[0]["SEG_CCHILD_NUM"].ToString())).ToString();
                    outviplabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_VIPGO_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_CIPGO_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_M6GO_NUM"].ToString();
                    outboardlabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_BOARD1"].ToString();
                    outFYlabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_FGO_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_CGO_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_YGO_NUM"].ToString();
                    outsumlabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_ET_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_CREWGO_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_ETGO_NUM"].ToString();
                    outjinlabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_JPGO_GOLDEN_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_JPGO_SILVER_NUM"].ToString();
                    outjplabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_JLGO_NUM"].ToString();
                    outmeallabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_SPEMEAL_NUM"].ToString();
                    outsslabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_SPEPSR_NUM"].ToString();
                    outtstlabel.Text = outzjDataSet.Tables[0].Rows[0]["SEG_INBOUND_GO_NUM"].ToString() + "/" + outzjDataSet.Tables[0].Rows[0]["SEG_OUTBOUND_GO_NUM"].ToString();

                    //出港航班旅客数据处理
                    outDataSet = traveller.getTravellerDataSet(outChangeLegsBM.FLTID.Replace(" ", ""), outChangeLegsBM.DATOP, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN);
                    if (outDataSet == null || outDataSet.Tables.Count == 0)
                    {
                        return;
                    }
                    t_outALLTraveller = outDataSet.Tables[0];
                    t_outVipTraveller = t_outALLTraveller.Select("PSR_M6_MARK <> 0 or PSR_SUSPECTEDVIP_MARK <> 0");//要客信息
                    t_outJINTraveller = t_outALLTraveller.Select("PSR_FFP_MARK <> 0");//常旅客
                    t_outMealTraveller = t_outALLTraveller.Select("PSR_MEAL_MARK = 1");//特餐旅客
                    t_outOTHTTraveller = t_outALLTraveller.Select("PSR_M6_MARK = 0 and PSR_SUSPECTEDVIP_MARK = 0 and PSR_FFP_MARK = 0 and PSR_MEAL_MARK = 0 and PSR_SS_MARK = 0 and PSR_INFANT_MARK = 0 and PSRD_IN_STN is NULL and PSRD_OUT_STN is NULL");
                    t_outSsTraveller = t_outALLTraveller.Select("PSR_SS_MARK <> 0 or PSR_INFANT_MARK = 1 ");//特服旅客
                    t_outTSTTraveller = t_outALLTraveller.Select("PSRD_IN_STN is not NULL or PSRD_OUT_STN is not NULL");//中转旅客

                    showListViewData(outVIPListView, t_outVipTraveller);//要客信息
                    showListViewData(outSSListView, t_outSsTraveller);//特服
                    showListViewData(outMealListView, t_outMealTraveller);//特餐
                    showListViewData(outTSTListView, t_outTSTTraveller);//中转
                    showListViewData(outclistView, t_outJINTraveller);//常旅客
                    showListViewData(outplistView, t_outOTHTTraveller);//普通旅客

                    outVIPTabPage.Text = "要客信息(" + t_outVipTraveller.Length.ToString() + "人)";
                    outSSTabPage.Text = "特服旅客信息(" + t_outSsTraveller.Length.ToString() + "人)";
                    outMealTabPage.Text = "特餐旅客信息(" + t_outMealTraveller.Length.ToString() + "人)";
                    outTSTabPage.Text = "中转旅客信息(" + t_outTSTTraveller.Length.ToString() + "人)";
                    outOTHTabPage.Text = "普通旅客信息(" + t_outOTHTTraveller.Length.ToString() + "人)";
                    outJINTabPage.Text = "常旅客信息(" + t_outJINTraveller.Length.ToString() + "人)";

                    //保存上次查询时间
                    lastSearchDateTime = thisSearchDateTime;
                    lastOutChangeLegsBM = outChangeLegsBM;
                    lastSelectedRow = m_iOldSelectedRow;
                }
                #endregion
            }
        }

        #region 设置TabControl标签名称
        private void outTabControl_PSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(outTabControl_PSR);
        }
        #endregion

        #region gridView操作
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.Value != null))
            {
                DataGridView dataGridView = (sender as DataGridView);  //网格控件

                if (dataGridView.Rows[e.RowIndex].Cells["cnvcPosition"].Value.ToString() == "合计")
                //处理 合计行，改变相应 cell 底色
                {
                    //
                    dataGridView.Rows[e.RowIndex].Cells["cniCrewInfoId"].Value = "";

                    //改变底色
                    e.CellStyle.BackColor = Color.Gainsboro;
                    e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, e.CellStyle.Font.Size, FontStyle.Bold);
                }
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.Value != null))
            {
                DataGridView dataGridView = (sender as DataGridView);  //网格控件

                if (dataGridView.Rows[e.RowIndex].Cells["cnvcPosition"].Value.ToString() == "合计")
                //处理 合计行，改变相应 cell 底色
                {
                    //
                    dataGridView.Rows[e.RowIndex].Cells["cniCrewInfoId"].Value = "";

                    //改变底色
                    e.CellStyle.BackColor = Color.Gainsboro;
                    e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, e.CellStyle.Font.Size, FontStyle.Bold);

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {


        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            DataGridView dataGridView = (sender as DataGridView);


            if ((dataGridView.Rows[e.RowIndex1].Cells["cnvcPosition"].Value.ToString().Trim() == "合计") ||
                (dataGridView.Rows[e.RowIndex2].Cells["cnvcPosition"].Value.ToString().Trim() == "合计"))
            {
                e.SortResult = 0;
                e.Handled = true;
            }
        }

        private void dataGridView2_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            DataGridView dataGridView = (sender as DataGridView);


            if ((dataGridView.Rows[e.RowIndex1].Cells["cnvcPosition"].Value.ToString().Trim() == "合计") ||
                (dataGridView.Rows[e.RowIndex2].Cells["cnvcPosition"].Value.ToString().Trim() == "合计"))
            {
                e.SortResult = 0;
                e.Handled = true;
            }
        }
        #endregion


        #region 标签标题切换
        private void outtabControl_Taskbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(outtabControl_Taskbook);
        }

        private void intabControl_Taskbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(intabControl_Taskbook);
        }
        #endregion

        //设置tabControl标签标题
        private void setDataTabControlTile(TabControl tabControl)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {

                if (tabControl.TabPages[i] == tabControl.SelectedTab)
                {
                    if (!tabControl.TabPages[i].Text.Contains("【"))
                    {
                        tabControl.TabPages[i].Text = "【" + tabControl.TabPages[i].Text + "】";
                    }
                }
                else
                {
                    tabControl.TabPages[i].Text = tabControl.TabPages[i].Text.ToString().Replace("【", "");
                    tabControl.TabPages[i].Text = tabControl.TabPages[i].Text.ToString().Replace("】", "");
                }
            }
        }

        private void chkinTaskbookConfiguration_NewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkinTaskbookConfiguration_NewLine.Checked)
            {
                #region dataGridView1
                //网格换行
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView1 in dataGridView1.Rows)
                {
                    if (dataGridViewRowdataGridView1.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView1.Height = 80;
                        else
                            dataGridViewRowdataGridView1.Height = 17;

                    }
                }
                //列宽
                dataGridView1.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView1

                #region dataGridView3
                //网格换行
                dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView3 in dataGridView3.Rows)
                {
                    if (dataGridViewRowdataGridView3.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView3.Height = 80;
                        else
                            dataGridViewRowdataGridView3.Height = 17;

                    }
                }
                //列宽
                dataGridView3.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView3


            }
            else
            {
                #region dataGridView1
                //网格换行
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView1 in dataGridView1.Rows)
                {
                    if (dataGridViewRowdataGridView1.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView1.Height = 80;
                        else
                            dataGridViewRowdataGridView1.Height = 17;

                    }
                }
                //列宽
                dataGridView1.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView1

                #region dataGridView3
                //网格换行
                dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView3 in dataGridView3.Rows)
                {
                    if (dataGridViewRowdataGridView3.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView3.Height = 80;
                        else
                            dataGridViewRowdataGridView3.Height = 17;

                    }
                }
                //列宽
                dataGridView3.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView3
            }

        }

        private void chkoutTaskbookConfiguration_NewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkoutTaskbookConfiguration_NewLine.Checked)
            {
                #region dataGridView2
                //网格换行
                dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView2 in dataGridView2.Rows)
                {
                    if (dataGridViewRowdataGridView2.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView2.Height = 80;
                        else
                            dataGridViewRowdataGridView2.Height = 17;

                    }
                }
                //列宽
                dataGridView2.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView2

                #region dataGridView4
                //网格换行
                dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView4 in dataGridView4.Rows)
                {
                    if (dataGridViewRowdataGridView4.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView4.Height = 80;
                        else
                            dataGridViewRowdataGridView4.Height = 17;

                    }
                }
                //列宽
                dataGridView4.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView4


            }
            else
            {
                #region dataGridView2
                //网格换行
                dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView2 in dataGridView2.Rows)
                {
                    if (dataGridViewRowdataGridView2.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView2.Height = 80;
                        else
                            dataGridViewRowdataGridView2.Height = 17;

                    }
                }
                //列宽
                dataGridView2.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView2

                #region dataGridView4
                //网格换行
                dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //设置行高
                foreach (DataGridViewRow dataGridViewRowdataGridView4 in dataGridView4.Rows)
                {
                    if (dataGridViewRowdataGridView4.Cells["cnvcPosition"].Value.ToString().IndexOf("合计") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView4.Height = 80;
                        else
                            dataGridViewRowdataGridView4.Height = 17;

                    }
                }
                //列宽
                dataGridView4.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView4
            }
        }



        #region 排序事件处理
        private void intVIPListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(intVIPListView, sender, e);
        }

        private void listViewSortFunc(ListView ownerListView, object sender, ColumnClickEventArgs e)
        {
            if (ownerListView.Columns[e.Column].Tag == null)
                ownerListView.Columns[e.Column].Tag = true;
            bool flag = (bool)ownerListView.Columns[e.Column].Tag;
            if (flag)
                ownerListView.Columns[e.Column].Tag = false;
            else
                ownerListView.Columns[e.Column].Tag = true;
            ownerListView.ListViewItemSorter = new ListViewSort(e.Column, ownerListView.Columns[e.Column].Tag);
            ownerListView.Sort();//对列表进行自定义排序
        }

        private void inplistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(inplistView, sender, e);
        }

        private void inSSListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(inSSListView, sender, e);
        }

        private void inMealListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(inMealListView, sender, e);
        }

        private void inclistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(inclistView, sender, e);
        }

        private void inTSTListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(inTSTListView, sender, e);
        }

        private void outVIPListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(outVIPListView, sender, e);
        }

        private void outSSListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(outSSListView, sender, e);
        }

        private void outMealListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(outMealListView, sender, e);
        }

        private void outclistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(outclistView, sender, e);
        }

        private void outTSTListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(outTSTListView, sender, e);
        }

        private void outplistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            listViewSortFunc(outplistView, sender, e);
        }
        #endregion

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            //IList ilInOutFlights = new ArrayList();


            //获取被右击单元格所在行的进出港航班实体
            //GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(ilInOutFlights as ArrayList)[e.Row];

            //如果右击位置的航班为空
            //if (guaranteeInforBM.IncncDATOP != "1900-01-01")
            //{
            //    fmGuaranteeRecord objfmGuaranteeRecord = new fmGuaranteeRecord(m_accountBM, m_stationBM, inChangeLegsBM);
            //    objfmGuaranteeRecord.ShowDialog();
            //}

            fmGuaranteeRecord objfmGuaranteeRecord = new fmGuaranteeRecord(m_accountBM, m_stationBM, inChangeLegsBM, FlightOrientation.In);
            objfmGuaranteeRecord.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            fmGuaranteeRecord objfmGuaranteeRecord = new fmGuaranteeRecord(m_accountBM, m_stationBM, outChangeLegsBM, FlightOrientation.Out);
            objfmGuaranteeRecord.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                //string[] arrayString = textBox1.Text.Trim().Split(',');
                ////fpFlightInfo.ActiveSheet.AddSelection(Convert.ToInt32(arrayString[0]),
                ////    Convert.ToInt32(arrayString[1]),
                ////    Convert.ToInt32(arrayString[2]),
                ////    fpFlightInfo.ActiveSheet.ColumnCount);
                //    //Convert.ToInt32(arrayString[3]));
                //fpFlightInfo.ActiveSheet.SetActiveCell(Convert.ToInt32(arrayString[0]), Convert.ToInt32(arrayString[1]), false);
                //fpFlightInfo.ActiveSheet.Columns[2].Locked = true;
                //fpFlightInfo.ActiveSheet.Rows[6].Locked = true;
                //fpFlightInfo.ActiveSheet.FrozenColumnCount = 2;

                //string strDTTM = "";

                ////获取最后一条记录（指定用户）
                //if (strDTTM.Trim() == "")
                //{
                //    ReturnValueSF rvSFChangeRecordBF = new ReturnValueSF();
                //    ChangeRecordBF changeRecordBF = new ChangeRecordBF();
                //    rvSFChangeRecordBF = changeRecordBF.GetLastRecord("SYS_PEK");
                //    if ((rvSFChangeRecordBF.Result > 0) &&
                //        (rvSFChangeRecordBF.Dt != null) &&
                //        (rvSFChangeRecordBF.Dt.Rows.Count > 0))
                //    {
                //        strDTTM = rvSFChangeRecordBF.Dt.Rows[0]["cncFOCOperatingTime"].ToString();
                //    }
                //    else
                //    {
                //        MessageBox.Show("从变更表获取最后一条记录（用户：SYS_PEK）失败，请重新登录！", "提示", MessageBoxButtons.OK);
                //        return;
                //        //Environment.Exit(0);
                //    }
                //}
                #endregion

                #region 测试接入旅客信息
                LinYong.PublicService.PublicService publicService = new LinYong.PublicService.PublicService();
                String encodeUsername = publicService.DESEncrypt("pass4-opm", "q1w2e3r4");// pass1-opm,pass2-opm,pass3-opm,pass4-opm,根据用户级别用一个, pass1-opm级别最低
                String encodePwd = publicService.DESEncrypt("2015-10-22 09:45:00.000", "q1w2e3r4");//调用时刻UTC时间
                String encodeFltNo = publicService.DESEncrypt("HU7181", "q1w2e3r4");
                String encodeDatopChn = publicService.DESEncrypt("2015-10-23", "q1w2e3r4");//航班日期（北京时间）


                DateTime dateTimeNow = Convert.ToDateTime("2015-10-23 08:37:00.000");
                DateTime dateTimeStart = Convert.ToDateTime("1970-01-01 00:00:00.000");
                TimeSpan timeSpan = new TimeSpan();
                timeSpan = dateTimeNow - dateTimeStart;
                long totalMilliseconds = Convert.ToInt64(timeSpan.TotalMilliseconds);
                encodePwd = publicService.DESEncrypt(totalMilliseconds.ToString(), "q1w2e3r4");//调用时刻UTC时间



                //String encodeUsername = CryptUtils.encode("pass4-opm");// pass1-opm,pass2-opm,pass3-opm,pass4-opm,根据用户级别用一个, pass1-opm级别最低

                //String encodePwd = CryptUtils.encode("2015-10-20 15:00:00");//调用时刻UTC时间

                //String encodeFltNo = CryptUtils.encode("HU0496");

                //String encodeDatopChn = CryptUtils.encode("2015-10-20");//航班日期（北京时间）

                //ASCIIEncoding Encode = new ASCIIEncoding();

                //byte[] post = Encode.GetBytes("fltNo=" + encodeFltNo + "&datopChn=" + encodeDatopChn);

                //encodeUsername = "94165272C94C7EB3565320C075ED5018";// pass1-opm,pass2-opm,pass3-opm,pass4-opm,根据用户级别用一个, pass1-opm级别最低
                //encodePwd = "D0EECDD49B10673AC125C4721D9A4540";//调用时刻UTC时间
                //encodeFltNo = "4FAA89A658A18B20";
                //encodeDatopChn = "60741D1FF780F672782BF312592D1869";//航班日期（北京时间）

                byte[] post = Encoding.UTF8.GetBytes("fltNo=" + encodeFltNo + "&datopChn=" + encodeDatopChn);

                String url = "http://" + encodeUsername + ":" + encodePwd + "@10.72.16.131/opm-web/passenger/passengerDetail.do";

                String PostHeaders = "Content-Type: application/x-www-form-urlencoded";

                //webBrowser3.Navigate(url, null, post, PostHeaders);

                #endregion 测试接入旅客信息

            }
            catch(Exception ex)
            {
                string s = ex.Message;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string strCompanys = ";";


            if (checkBox1.Checked)
                strCompanys = strCompanys + checkBox1.Text + ";";
            if (checkBox2.Checked)
                strCompanys = strCompanys + checkBox2.Text + ";";
            if (checkBox3.Checked)
                strCompanys = strCompanys + checkBox3.Text + ";";
            if (checkBox4.Checked)
                strCompanys = strCompanys + checkBox4.Text + ";";
            if (checkBox5.Checked)
                strCompanys = strCompanys + checkBox5.Text + ";";
            if (checkBox6.Checked)
                strCompanys = strCompanys + checkBox6.Text + ";";
            if (checkBox7.Checked)
                strCompanys = strCompanys + checkBox7.Text + ";";
            if (checkBox8.Checked)
                strCompanys = strCompanys + checkBox8.Text + ";";
            if (checkBox9.Checked)
                strCompanys = strCompanys + checkBox9.Text + ";";
            if (checkBox10.Checked)
                strCompanys = strCompanys + checkBox10.Text + ";";
            if (checkBox11.Checked)
                strCompanys = strCompanys + checkBox11.Text + ";";
            if (checkBox12.Checked)
                strCompanys = strCompanys + checkBox12.Text + ";";
            if (checkBox13.Checked)
                strCompanys = strCompanys + checkBox13.Text + ";";
            if (checkBox14.Checked)
                strCompanys = strCompanys + checkBox14.Text + ";";
            if (checkBox15.Checked)
                strCompanys = strCompanys + checkBox15.Text + ";";

            SysMsgBM.Companys = strCompanys;
            if (SysMsgBM.OldCompanys != strCompanys)
                //label44.Visible = true;
                button2.Visible = true;
            else
                //label44.Visible = false;
                button2.Visible = false;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) //选择了生产库
            {
                //获取生产库配置
                string strFlightMonitor_Use = ConfigurationSettings.AppSettings["dbFlightMonitor_Use"].ToString().Trim();

                if (strFlightMonitor_Use != "")
                {
                    //配置文件：使生产库配置生效
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["dbFlightMonitor"].Value = strFlightMonitor_Use;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件

                    //标明使用的FlightMonitor系统数据库使用的配置项
                    SysMsgBM.FlightMonitorConfig = "dbFlightMonitor_Use";
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) //历史库
            {
                //获取历史库配置
                string strFlightMonitor_His = ConfigurationSettings.AppSettings["dbFlightMonitor_His"].ToString().Trim();

                if (strFlightMonitor_His != "")
                {
                    //配置文件：使备份库配置生效
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["dbFlightMonitor"].Value = strFlightMonitor_His;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件

                    //标明使用的FlightMonitor系统数据库使用的配置项
                    SysMsgBM.FlightMonitorConfig = "dbFlightMonitor_His";
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FlightRefresh();
        }

        private void popOutFlightNoMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (GetDataItemPurview("OutcntPaxNameList") > 1)
            {
                fmQueryPAX objfmQueryPAX = new fmQueryPAX(outChangeLegsBM, FlightOrientation.Out);
                objfmQueryPAX.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (GetDataItemPurview("IncntPaxNameList") > 1)
            {
                fmQueryPAX objfmQueryPAX = new fmQueryPAX(inChangeLegsBM, FlightOrientation.In);
                objfmQueryPAX.ShowDialog();
            }
            else
            {
                MessageBox.Show("对不起，您没有权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label44_Click(object sender, EventArgs e)
        {
            //fmSelectUsers objfmSelectUsers = new fmSelectUsers();
            //objfmSelectUsers.ShowDialog();

            StationBM stationBM = new StationBM();
            CommanderTypeBM commanderTypeBM = new CommanderTypeBM();

            stationBM.ThreeCode = m_accountBM.StationThreeCode;
            commanderTypeBM.cniCommanderTypeID = 13;
            commanderTypeBM.cnvcCommanderType = "外场";

            fmSelectUsers objfmSelectUsers = new fmSelectUsers(stationBM, commanderTypeBM, "陈建平(chenjianping)、陈旭光(chenxuguang)");
            objfmSelectUsers.ShowDialog();
        }

    }


    /// <summary>
    /// 播放声音类
    /// </summary>
    internal class SoundHelpers
    {
        [Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */ //同步
            SND_ASYNC = 0x0001,  /* play asynchronously */ //异步
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        [DllImport("winmm")]
        public static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);
    }

    class ListViewSort : IComparer
    {
        private int col;
        private bool descK;
        public ListViewSort()
        {
            col = 0;
        }
        public ListViewSort(int column, object Desc)
        {
            descK = (bool)Desc;
            col = column; //当前列,0,1,2...,参数由ListView控件的ColumnClick事件传递 
        }
        public int Compare(object x, object y)
        {
            int tempInt = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            if (descK) return -tempInt;
            else return tempInt;
        }
    }
}
