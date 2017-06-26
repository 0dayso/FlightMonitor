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
        #region �������
        private Color colorFavorate;

        private AccountBM m_accountBM;                              //��½�û�ʵ�����
        private DataTable m_dtDataItemPurview;                      //��½�û���������Ȩ��
        private DataTable m_dtStandardIntermissionTime;             //��׼��վʱ���
        private int m_iLastRecordNo;                                //��������
        private DataTable m_dtDataItems;                            //�û����ó���������
        private string m_strDataItemSearch;                         //�����û����ó����������ѯҪ��˸�ļ�¼
        private DataTable m_dtStations;                             //�����б�
        private StationBM m_stationBM;                              //�û�ѡ��ĺ�վʵ�����     
        private DataTable m_dtFlightDelayReason;                    //��������ԭ������
        private DataTable m_dtDiversionDelayReason;                 //�������������


        private DataTable m_dtYesterdayStationFlights;              //��վ�������еĽ����ۺ���
        private DataTable m_dtTodayStationFlights;                  //��վ�������еĽ����ۺ���
        private DataTable m_dtTomorrowStationFlights;               //��վ�������еĽ����ۺ���
        private DataTable m_dtSelectDateStationFlights;             //��վ�������еĽ����ۺ���

        private DataTable m_dtInOutFlightsSchema;                   //�������۸�ʽ��ʾ�ĺ��ද̬�ı�ṹ

        private IList m_ilYesterdayInOutFlights;                    //�������۸�ʽ��ʾ�������캽�ද̬�ı��
        private IList m_ilTodayInOutFlights;                        //�������۸�ʽ��ʾ�Ľ��캽�ද̬�ı��
        private DataTable m_dtTodayInOutFlights;                    //�������۸�ʽ��ʾ�Ľ��캽�ද̬�ı��
        private IList m_ilTomorrowInOutFlights;                     //�������۸�ʽ��ʾ�����캽�ද̬�ı��
        private IList m_ilSelectDateInOutFlights;                   //�������۸�ʽ��ʾ����ѡ���ں��ද̬�ı��


        #region yanxian 2013-10-15 �����ÿ�����
        private TravellerBF traveller = new TravellerBF();
        private DataRow[] t_inVipTraveller; //����Ҫ������
        private DataRow[] t_inSsTraveller;//���������ÿ�
        private DataRow[] t_inMealTraveller;//�����ز��ÿ�
        private DataRow[] t_inJINTraveller;//���۽����ÿ�
        private DataRow[] t_inTSTTraveller;//������ת�ÿ�
        private DataRow[] t_inOTHTTraveller;//������ͨ�ÿ�
        private DataTable t_inALLTraveller;//���ۺ��������ÿ�

        private DataRow[] t_outVipTraveller; //����Ҫ������
        private DataRow[] t_outSsTraveller;//���������ÿ�
        private DataRow[] t_outMealTraveller;//�����ز��ÿ�
        private DataRow[] t_outJINTraveller;//���۽����ÿ�
        private DataRow[] t_outTSTTraveller;//������ת�ÿ�
        private DataRow[] t_outOTHTTraveller;//������ͨ�ÿ�
        private DataTable t_outALLTraveller;//���ۺ��������ÿ�

        private DateTime lastSearchDateTime;//�ϴβ�ѯͬһ�����ÿ�����ʱ��
        private ChangeLegsBM lastInChangeLegsBM = new ChangeLegsBM();//��һ�β�ѯ���ۺ���ʵ��
        private ChangeLegsBM lastOutChangeLegsBM = new ChangeLegsBM();//��һ�β�ѯ���ۺ���ʵ��
        private int lastSelectedRow = -1;//��һ�β�ѯ��ѡ�е���
        #endregion

        //��˸ǰ��Ԫ����ɫ
        Color[,] colorArrOldBackGround;
        //int[,] iFirstEnterSplash;

        private DataTable m_dtSplashTag;  //������˸��־

        //����б���ɫ������
        private int m_iGreenRecordNum = 0;
        private int m_iRedRecordNum;

        //��¼ѡ�е��к����Լ�ԭ��ɫ
        private int m_iOldSelectedRow = -1;
        private int m_iOldSelectedColumn = -1;
        private Color m_cOldBackGroudColor, m_cOldForeColor;

        //�Ҽ��˵���ѡ�����
        private int m_iRightButtonRow = 0;

        //��ѡ��ĳ��ۺ������Ϣ
        //private GuaranteeInforBM selectedGuaranteeInfo = null;
        private ChangeLegsBM inChangeLegsBM = new ChangeLegsBM();
        private ChangeLegsBM outChangeLegsBM = new ChangeLegsBM();

        private StatusBar m_statusBar;

        //���̶߳�ʱ������������������������ⱻ��������
        private System.Threading.Timer timer;
        private int iRefreshInterval = 20;
        private object oMutexChangeRecords = new object();
        private DataTable m_dtChangeTable = new DataTable();   //�仯�б�
        private int m_iAutoAdjust = 0;
        #endregion

        //��ǰ��������󣬰������к��� -- modified by LinYong in 2013.10.16
        static public DataTable _dataTableTaskBooks = null;

        #region ��������
        /// <summary>
        /// ������
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
        /// ���ݱ���ʾ�ؼ�
        /// </summary>
        public FarPoint.Win.Spread.FpSpread FpFlightInfo
        {
            get { return fpFlightInfo; }
        }
        #endregion

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="accountBM">��½�û�ʵ�����</param>
        /// <param name="dtIntermissionTime">��׼��վʱ���</param>
        /// <param name="iLastRecordNo">��������</param>
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

            //�Զ���ı���ɫ
            //colorFavorate = Color.FromArgb(182, 222, 187);      
            colorFavorate = Color.White;

            m_iAutoAdjust = iAutoAdjust;
        }
        #endregion

        #region ���봰��
        private void fmFlightGuarantee_Load(object sender, EventArgs e)
        {
            //���û�ȡ������ݵļ��
            timerChange.Interval = m_accountBM.RefreshInterval * 1000;

            //������ͼ
            SpreadGrid spreadGrid = new SpreadGrid(m_accountBM);

            spreadGrid.SetView(shYestoday, m_dtDataItems, 0);
            spreadGrid.SetView(shToday, m_dtDataItems, 0);
            spreadGrid.SetView(shTomorrow, m_dtDataItems, 0);
            spreadGrid.SetView(shSelectDate, m_dtDataItems, 0);

            //��ȡ�û���������Ȩ��
            GetUserDataItemPurview();

            //������ʾ�ĺ�վ
            GetStations();

            //��ȡ��������ͱ���ԭ��
            GetFlightDelayReason();
            GetDiversionDelayReason();

            //��ȡ�������ź����100���������
            GetMaxRecordNo();

            //��ȡ�ú�վ��������к���
            m_dtTodayStationFlights = GetStationFlights(GetDateTimeBM(1), m_stationBM, 1);

            //������Ӧ����ʾ��Ϣ
            ComputeFlightsInfor(m_dtTodayStationFlights);

            //�����ۺ�����Schema
            m_dtInOutFlightsSchema = GetDisplaySchema();

            //��ȡ��������ۺ���
            m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

            //�󶨽����ۺ�����Ϣ
            fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;
            fpFlightInfo.Sheets[3].DataSource = m_ilTodayInOutFlights;

            //��ȡ�����¼
            GetChangeData();

            //��¼��˸��Ԫ�񱳾�ɫ���Ƿ��һ�ν�����˸
            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];

            //��ʼ�������ɫ
            InitialGridColor(shToday);

            //���õ��쵥Ԫ����ɫ
            SetGridColor(1, m_dtTodayStationFlights);

            fpFlightInfo.ActiveSheetIndex = 1;
            fpFlightInfo.Sheets[3].SheetName = dtFlightDate.Value.ToString("yyyy-MM-dd");

            //�����ۺ�����
            SetInOutFlightsNum();

            //modified by LinYong in 20140707
            UpDownValue_ValueChanged(sender, e);

            //���û���ˢ��Ƶ��д�������ļ�
            int iTempInterval = m_accountBM.RefreshInterval * 1000;
            TimerCallback timerDelegate = new TimerCallback(GetChangeDate);
            //timer = new System.Threading.Timer(timerDelegate, null, 0, (iRefreshInterval * 1000));
            timer = new System.Threading.Timer(timerDelegate, null, 0, iTempInterval);

        }
        #endregion

        #region ��ȡĳ��ʱ�䷶Χ
        /// <summary>
        /// ��ȡĳ��ʱ�䷶Χ
        /// </summary>
        /// <param name="iDay">��һ�죺0=���죻1=���죻2=���죻3=ѡ�������</param>
        private DateTimeBM GetDateTimeBM(int iDay)
        {
            DateTimeBM dataTimeBM = new DateTimeBM();

            string strStartDateTime;
            string strEndDateTime;

            if (iDay == 0)  //����
            {
                strStartDateTime = DateTime.Now.AddHours(-5).Date.AddDays(-1).ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = DateTime.Now.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else if (iDay == 1) //����ʱ�䷶Χʵ�����
            {
                strStartDateTime = DateTime.Now.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = DateTime.Now.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else if (iDay == 2) //����ʱ�䷶Χʵ�����
            {
                strStartDateTime = DateTime.Now.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = DateTime.Now.AddHours(-5).Date.AddDays(2).ToString("yyyy-MM-dd") + " 05:00:00";
            }
            else   //��ѡ����
            {
                strStartDateTime = dtFlightDate.Value.Date.ToString("yyyy-MM-dd") + " 05:00:00";
                strEndDateTime = dtFlightDate.Value.Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            }

            dataTimeBM.StartDateTime = strStartDateTime;
            dataTimeBM.EndDateTime = strEndDateTime;

            return dataTimeBM;
        }
        #endregion

        #region ��ȡ��վ�б�������ʾ�ĺ�վ
        /// <summary>
        /// ��ȡ��վ�б�������ʾ�ĺ�վ
        /// </summary>
        private void GetStations()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            StationBF stationBF = new StationBF();

            rvSF = stationBF.GetAllStation();
            if (rvSF.Result > 0)
            {
                m_dtStations = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //�󶨳�վ�б�
            cmbStations.DataSource = m_dtStations;
            cmbStations.ValueMember = "cncThreeCode";
            cmbStations.DisplayMember = "cnvcAirportName";
            cmbStations.SelectedValue = m_accountBM.StationThreeCode;

            //ѡ��ĺ�վʵ�����
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

        #region ��ȡ��������ԭ������
        /// <summary>
        /// ��ȡ��������ԭ������
        /// </summary>
        private void GetFlightDelayReason()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightDelayReasonBF flightDelayReasonBF = new FlightDelayReasonBF();

            rvSF = flightDelayReasonBF.GetAllFlightDelayReason();
            if (rvSF.Result > 0)
            {
                m_dtFlightDelayReason = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ��ȡ�������������
        /// <summary>
        /// ��ȡ�������������
        /// </summary>
        private void GetDiversionDelayReason()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            DiversionDelayReasonBF diversionDelayReasonBF = new DiversionDelayReasonBF();

            rvSF = diversionDelayReasonBF.GetAllDiversionDelayReason();
            if (rvSF.Result > 0)
            {
                m_dtDiversionDelayReason = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ��ȡ��½�û���������Ȩ�ޣ�Ȩ�ޡ���ʾ��
        /// <summary>
        /// ��ȡ��½�û���������Ȩ�ޣ�Ȩ�ޡ���ʾ��
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ��ȡ���캽վ�����к���
        /// <summary>
        /// ��ȡ���캽վ�����к���
        /// </summary>
        /// <param name="dateTimeBM">���ڷ�Χ</param>
        /// <param name="stationBM">��վʵ��</param>
        /// <param name="iToday">��ȡ����ĺ��ࣺ0=���죻1=���죻2=���죻3=ѡ�������</param>
        private DataTable GetStationFlights(DateTimeBM dateTimeBM, StationBM stationBM, int iToday)
        {
            //��վ���к���
            DataTable dtStationFlights = new DataTable();
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();

            #region modified by LinYong -- 20091117
            ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM);
            //ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM, m_accountBM);
            #endregion

            if (rvSF.Result > 0)
            {

                //��վ������Ϣ��
                dtStationFlights = rvSF.Dt;

                #region ���Ը���ѡ��ĺ��չ�˾���˺��� 
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

                //���� �ı乫˾ѡ���б� ����ʾ��Ϣ
                //label44.Visible = false;
                button2.Visible = false;
                #endregion ���Ը���ѡ��ĺ��չ�˾���˺��� 


                //������ҽ���ĺ�����Ϣ
                //��������˸��ʾ��
                if (iToday == 1)
                {
                    //��˸��ʶ��
                    m_dtSplashTag = new DataTable();

                    //������˸��Ľṹ
                    //��˸���ֶ���vw_legs��ͼ�ֶ���ͬ
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

                    //��˸��ʶ�������
                    DataColumn[] pk = new DataColumn[4];
                    pk[0] = m_dtSplashTag.Columns["cncDATOP"];
                    pk[1] = m_dtSplashTag.Columns["cnvcFLTID"];
                    pk[2] = m_dtSplashTag.Columns["cniLEGNO"];
                    pk[3] = m_dtSplashTag.Columns["cnvcAC"];
                    m_dtSplashTag.PrimaryKey = pk;

                    m_dtSplashTag.Rows.Clear();

                    //����˸��ʶ������Ӽ�¼
                    //����������ֶε�ֵ
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return dtStationFlights;
        }
        #endregion

        #region �����վʱ�䡢���ز�ʱ�䡢
        /// <summary>
        /// �����վʱ�䡢���ز�ʱ�䡢
        /// </summary>
        private void ComputeFlightsInfor(DataTable dtStationFlights)
        {
            int iRowIndex = 0;
            foreach (DataRow dataRow in dtStationFlights.Rows)
            {
                //���㿪�ز�ʱ��
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
                //�жϻ������Ƿ�Ϊ��
                if (dataRow["cncDEPAirportCNAME"].ToString() == "")
                {
                    dataRow["cncDEPAirportCNAME"] = dataRow["cncDEPCityThreeCode"].ToString();
                }
                if (dataRow["cncARRAirportCNAME"].ToString() == "")
                {
                    dataRow["cncARRAirportCNAME"] = dataRow["cncARRCityThreeCode"].ToString();
                }
                //�ж��Ƿ�û����ض�̬
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
                //�ж��Ƿ�����ɶ�̬
                //û����ɶ�̬�澯
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

                //��׼��վʱ��
                string strDEPAirportPaxType = "1";
                if (dataRow["cniDEPAirportPaxType"].ToString().Trim() != "")
                {
                    strDEPAirportPaxType = dataRow["cniDEPAirportPaxType"].ToString();
                }
                DataRow[] drStandardIntermission = m_dtStandardIntermissionTime.Select("cncACTYPE = '" + dataRow["cncACTYP"].ToString() + "' AND cniAirportPaxType = " + strDEPAirportPaxType);

                TimeSpan tsIntermissionTime = new TimeSpan(0, 0, 0);
                //�ж��Ƿ��վ����
                if (dataRow["cncSTATUS"].ToString() != "CNL" && iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    //�����վʱ��
                    tsIntermissionTime = DateTime.Parse(dataRow["cncETD"].ToString()).Subtract(DateTime.Parse(dtStationFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));

                    dataRow["cniIntermissionTime"] = tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes;


                    if (drStandardIntermission.Length > 0)
                    {
                        if (dataRow["cncDEPIsSelf"].ToString().Trim() == "1" && dataRow["cncARRIsSelf"].ToString().Trim() == "1") //���ں���
                        {
                            if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
                            {
                                dataRow["cniNotEnoughIntermissionTime"] = 1;
                            }
                        }
                        else   //���ʺ���
                        {
                            if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniInterIntermissionTime"].ToString()))
                            {
                                dataRow["cniNotEnoughIntermissionTime"] = 1;
                            }
                        }
                    }
                }

                //�����������ʱ��
                //ʼ������                
                if (dataRow["cncSTATUS"].ToString() != "CNL" && (iRowIndex == 0 || dataRow["cnvcLONG_REG"].ToString() != dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString()))
                {
                    if (dataRow["cnvcDELAY1"].ToString() != "")
                    {
                        dataRow["cniDischargingDelayTime"] = dataRow["cniDUR1"].ToString();
                    }
                }
                //��վ����
                else if (dataRow["cncSTATUS"].ToString() != "CNL" && iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    if (drStandardIntermission.Length > 0)
                    {
                        //�����վʱ��
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
                    //�ж��Ƿ�û�йز�ʱ��
                    if (dataRow["cncSTATUS"].ToString() != "CNL" && iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() != dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() && tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > 180) //��վ����
                    {
                        if (DateTime.Now.AddMinutes(Convert.ToInt32(drStandardIntermission[0]["cniIntermissionCloseCabinTime"].ToString())) > DateTime.Parse(dataRow["cncETD"].ToString()))
                        {
                            dataRow["cniNotClosePaxCabineTime"] = 1;
                        }

                    }
                    else  //ʼ������
                    {
                        if (DateTime.Now.AddMinutes(Convert.ToInt32(drStandardIntermission[0]["cniInitialCloseCabinTime"].ToString())) > DateTime.Parse(dataRow["cncETD"].ToString()))
                        {
                            dataRow["cniNotClosePaxCabineTime"] = 1;
                        }
                    }
                }


                //�ж��Ƿ�ʼ����
                if (dataRow["cncSTATUS"].ToString() != "CNL" && DateTime.Now.AddMinutes(m_accountBM.GuaranteeMinutes) > DateTime.Parse(dataRow["cncETD"].ToString()))
                {
                    dataRow["cniStartGuarantee"] = 1;
                }

                //�ж��Ƿ�ʼ�ǻ�
                if (dataRow["cncSTATUS"].ToString() != "CNL" && DateTime.Now.AddMinutes(m_accountBM.BoardingMinutes) > DateTime.Parse(dataRow["cncETD"].ToString()))
                {
                    dataRow["cniBoarding"] = 1;
                }

                //�ж��Ƿ����λ
                //if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                //{
                if (iRowIndex != 0 && dtStationFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() != "")
                {
                    if (dtStationFlights.Rows[iRowIndex - 1]["cncSTATUS"].ToString() != "CNL" && DateTime.Parse(dtStationFlights.Rows[iRowIndex - 1]["cncETA"].ToString()).AddMinutes(-m_accountBM.MCCReadyMinutes) < DateTime.Now)
                    {
                        dtStationFlights.Rows[iRowIndex - 1]["cniMCCReady"] = 1;
                    }
                }


                //�ж��Ƿ�������
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

        #region ��ȡҪ��ʾ��ļܹ�
        /// <summary>
        /// ��ȡҪ��ʾ��ļܹ�
        /// </summary>
        private DataTable GetDisplaySchema()
        {
            DataTable dtInOutFlights = new DataTable();

            //���������������ֶ�
            dtInOutFlights.Columns.Add("IncncDATOP");                   //���ۺ�������
            dtInOutFlights.Columns.Add("IncnvcFLTID");                  //���ۺ��ຽ���
            dtInOutFlights.Columns.Add("IncniLEGNO");                   //���ۺ��ຽ����Ϣ
            dtInOutFlights.Columns.Add("IncnvcAC");                     //���ۺ���ɻ���
            dtInOutFlights.Columns.Add("IncncAllSTA");                  //���ۺ���ƻ�����ʱ�䣨������ʽ��
            dtInOutFlights.Columns.Add("IncncAllETA");                  //���ۺ���Ԥ�Ƶ���ʱ��
            dtInOutFlights.Columns.Add("IncncAllTDWN");                 //���ۺ������ʱ��
            dtInOutFlights.Columns.Add("IncncAllATA");                  //���ۺ��ൽλʱ��
            dtInOutFlights.Columns.Add("IncncAllStatus");               //���ۺ��ຽ��״̬
            dtInOutFlights.Columns.Add("IncniAllViewIndex");            //���ۺ�����ʾ˳��

            dtInOutFlights.Columns.Add("OutcncDATOP");                  //���ۺ�������
            dtInOutFlights.Columns.Add("OutcnvcFLTID");                 //���ۺ��ຽ���
            dtInOutFlights.Columns.Add("OutcniLEGNO");                  //���ۺ��ຽ����Ϣ
            dtInOutFlights.Columns.Add("OutcnvcAC");                    //���ۺ���ɻ���
            dtInOutFlights.Columns.Add("OutcncAllSTD");                 //���ۺ���ƻ����ʱ��
            dtInOutFlights.Columns.Add("OutcncAllETD");                 //���ۺ���Ԥ�����ʱ��
            dtInOutFlights.Columns.Add("OutcncAllATD");                 //���ۺ����Ƴ�ʱ��
            dtInOutFlights.Columns.Add("OutcncAllTOFF");                //���ۺ������ʱ��    
            dtInOutFlights.Columns.Add("OutcncAllStatus");              //���ۺ��ຽ��״̬
            dtInOutFlights.Columns.Add("OutcniAllViewIndex");           //���ۺ�����ʾ˳��

            //�����û����õ���ͼ���������ֶ�
            foreach (DataRow dataRow in m_dtDataItems.Rows)
            {
                dtInOutFlights.Columns.Add(dataRow["cnvcDataItemID"].ToString());
            }

            //�к�
            dtInOutFlights.Columns.Add("cniRowIndex");
            return dtInOutFlights;
        }
        #endregion

        #region �������ۺ���״̬
        /// <summary>
        /// �������ۺ���״̬
        /// ����վ������Ϣ��ֳɽ��ۺ�����Ϣ��ͳ��ۺ�����Ϣ��
        /// Ȼ��������������ϳ�һ�������ۺ�����Ϣ��
        /// </summary>
        private IList FillInOutFlights(DataTable dtStationFlights, int iToday)
        {
            IList ilInOutFlights = new ArrayList();

            //�����ۺ�����Ϣ��
            DataTable dtAllInOutFlights = m_dtInOutFlightsSchema.Clone();

            //�ֱ����ɽ��ۺ�����Ϣ��ͳ��ۺ�����Ϣ��
            //�Ա��Ժ�������ɽ����ۺ�����Ϣ��
            //���ۺ��ࣺĿ�Ļ����������뺽վ��������ͬ
            DataTable dtInFlights = dtStationFlights.Clone();
            //���ۺ��ࣺ��ɻ����������뺽վ��������ͬ
            DataTable dtOutFlights = dtStationFlights.Clone();

            //��ѯ���ۺ���
            DataRow[] drInFlights = dtStationFlights.Select("cncARRSTN = '" + m_stationBM.ThreeCode + "'", "cniViewIndex,cncTDWN");
            foreach (DataRow dataRow in drInFlights)
            {
                dtInFlights.ImportRow(dataRow);
            }

            //��ѯ���ۺ���
            DataRow[] drOutFlights = dtStationFlights.Select("cncDEPSTN = '" + m_stationBM.ThreeCode + "'", "cniViewIndex,cncTOFF");
            foreach (DataRow dataRow in drOutFlights)
            {
                dtOutFlights.ImportRow(dataRow);
            }

            #region ���ݽ��ۺ�����Ͻ����ۺ���
            //���ݽ��ۺ�����Ͻ����ۺ���
            foreach (DataRow dataRow in dtInFlights.Rows)
            {
                //���ۺ���ķɻ���
                string strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
                //���ۺ����Ԥ��ʱ��
                string strInETD = dataRow["cncETD"].ToString();

                //��ѯ�ַ��������ݳ��ۺ���ķɻ�������ۺ���ķɻ�����ͬANDԤ�����ʱ����ڽ��ۺ����Ԥ�����ʱ��
                string strSearch = "cnvcLONG_REG = '" + strLONG_REG + "' AND cncETD > '" + strInETD + "'";
                drOutFlights = dtOutFlights.Select(strSearch, "cncETD");

                #region û�г��ۺ���
                //û�г��ۺ���
                if (drOutFlights.Length <= 0)
                {
                    //���ݺ���״̬���ú�����ʾ˳��
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

                    //�½�һ�м�¼
                    DataRow drInFlight = dtAllInOutFlights.NewRow();

                    #region �������������ֶθ�ֵ
                    //�������������ֶθ�ֵ
                    //���۲���
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
                    //���۲���
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

                    //���Ƚ����ۻ�������Ϊ���ۻ���
                    if (dtAllInOutFlights.Columns.Contains("OutcnvcLONG_REG"))
                    {
                        drInFlight["OutcnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                    }

                    #region ���û���������ʾ���ֶν��и�ֵ
                    //���û���������ʾ���ֶν��и�ֵ
                    foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                    {
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            drInFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }
                    #endregion

                    dtAllInOutFlights.Rows.Add(drInFlight);
                }
                #endregion

                #region �г��ۺ���
                //�г��ۺ���
                else
                {
                    //�������ۺ���״̬����
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

                    //�½�һ��
                    DataRow drInOutFlight = dtAllInOutFlights.NewRow();

                    #region �������������ֶθ�ֵ
                    //�������������ֶθ�ֵ
                    //���۲���
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
                    //���۲���
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

                    //���û���������ʾ���ֶν��и�ֵ
                    //���ۺ��ಿ��
                    foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                    {
                        //��ʽ�������ֶ�
                        string strFieldValue = FormatINItem(dataRow, dataRowItems);

                        //���ۻ�λ
                        if (dataRowItems["cnvcDataItemID"].ToString() == "IncnvcInGATE")
                        {
                            if (drOutFlights[0]["cnvcOutGate"].ToString().Trim() == "")
                            {
                                strFieldValue = drOutFlights[0]["cnvcOutGate"].ToString().Trim();
                            }
                        }

                        //�����ֶ�ֱ�Ӷ�ȡֵ
                        if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                        {
                            string strTemp = dataRowItems["cnvcDataItemID"].ToString();
                            drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                        }
                    }

                    //���ۺ��ಿ��
                    foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                    {
                        //��ʽ�������ֶ�
                        string strFieldValue = FormatOUTItem(drOutFlights[0], dataRowItems);

                        //�����ֶ�ֱ�Ӷ�ȡֵ
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

            #region ���ݳ��ۺ�����Ͻ����ۺ���
            //���ݳ��ۺ�����Ͻ����ۺ���
            //��Խ��г��ۺ�������
            foreach (DataRow dataRow in dtOutFlights.Rows)
            {
                //�������ۺ����״̬����
                string strOutViewIndex = "";
                if (dataRow["cncSTATUS"].ToString() == "ATA" || dataRow["cncSTATUS"].ToString() == "ARR")
                {
                    strOutViewIndex = "2";
                }
                else
                {
                    strOutViewIndex = dataRow["cniViewIndex"].ToString();
                }

                //�½�һ��
                DataRow drOutFlight = dtAllInOutFlights.NewRow();

                //�������������ֶθ�ֵ
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

                //���Ƚ����ۻ�λ����Ϊ���ۻ�λ
                if (dtAllInOutFlights.Columns.Contains("IncnvcInGATE"))
                {
                    drOutFlight["IncnvcInGATE"] = dataRow["cnvcOutGate"].ToString();
                }

                //���Ƚ����ۻ�������Ϊ���ۻ���
                if (dtAllInOutFlights.Columns.Contains("IncnvcLONG_REG"))
                {
                    drOutFlight["IncnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                }

                //�����ۻ�������Ϊ���ۻ��� -- added by LinYong in 20150330
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

                //���û���������ʾ���ֶν��и�ֵ
                foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                {
                    //��ʽ�������ֶ�
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    //�������ֶν��д���
                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        drOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }

                dtAllInOutFlights.Rows.Add(drOutFlight);
            }
            #endregion

            //���ݽ����ۺ�����Ϣ�����ɺ��ౣ����Ϣʵ���б�
            foreach (DataRow dataRow in dtAllInOutFlights.Rows)
            {
                ilInOutFlights.Add(new GuaranteeInforBM(dataRow));

            }
            (ilInOutFlights as ArrayList).Sort();

            //���кŸ�ֵ
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

                //�Խ����ۺ�����Ϣ��ÿ�е�cniRowIndex�ֶΣ����кŸ�ֵ
                if (drInOutFlights.Length > 0)
                {
                    drInOutFlights[0]["cniRowIndex"] = iRowIndex;
                }
                iRowIndex += 1;
            }

            //����ǽ���
            if (iToday == 1)
            {
                m_dtTodayInOutFlights = dtAllInOutFlights;
            }
            return ilInOutFlights;
        }
        #endregion

        #region ��ʼ����񱳾�ɫ
        /// <summary>
        /// ��ʼ����񱳾�ɫ
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

        #region ����ĳ�е�Ԫ����ɫ
        /// <summary>
        /// ����ĳ�е�Ԫ����ɫ
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

        #region ���ݺ���״̬����������ʾ��Χ
        /// <summary>
        /// ���ݺ���״̬����������ʾ��Χ
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

        #region ���ñ����ɫ
        /// <summary>
        /// ���ñ����ɫ
        /// </summary>
        /// <param name="iDay">0:���� 1:����: 2:���� 3:��ѡ����</param>
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

                    //��������ɫ
                    if (drInFlights.Length > 0 && drOutFlights.Length <= 0)  //ֻ�н��ۺ���
                    {
                        if (drInFlights[0]["cncSTATUS"].ToString() == "CNL" || drInFlights[0]["cncSTATUS"].ToString() == "ATA" || drInFlights[0]["cncSTATUS"].ToString() == "ARR")
                        {
                            SetRowCellColor(iDay, iRowIndex, Color.Silver);
                        }
                    }
                    else if (drInFlights.Length <= 0 && drOutFlights.Length > 0)  //ֻ�г��۸ۺ���
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
                        if (drInFlights.Length > 0)   //����н��ۺ���
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightNo")  //���ۺ����
                            {
                                //����������
                                if (drInFlights[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drInFlights[0]["cniLEGNO"].ToString()) % 100 != 0)
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }

                                //�ص㱣�Ϻ���
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
                            //��������
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightCharacterAbbreviate")  //��������
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
                            //����Ԥ��ʱ��
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncETA")
                            {
                                if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "DEP") //���
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.LimeGreen;
                                }
                                else if ((drInFlights[0]["cncSTATUS"].ToString().Trim() == "ARR") ||
                                    (drInFlights[0]["cncSTATUS"].ToString().Trim() == "ATA")) //����
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                }
                                else if (drInFlights[0]["cnvcDELAY1"].ToString().Trim() != "") //����
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }

                                else //����״̬
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }

                            //�������ʱ��
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncTDWN")
                            {
                                //if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "DEP")
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.LimeGreen;
                                //}
                                if ((drInFlights[0]["cncSTATUS"].ToString().Trim() != "ARR") && (drInFlights[0]["cncSTATUS"].ToString().Trim() != "ATA")) //����� �� �ǵ���
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor; ;
                                }
                                //else if (drInFlights[0]["cnvcDELAY1"].ToString().Trim() != "") //����
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                //}
                                //else if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "ATA") //����
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                //}
                                else //����
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                }

                                //û�е��ﶯ̬�澯

                                if (m_accountBM.TDWNPromt == 1 && drInFlights[0]["cncSTATUS"].ToString() != "CNL" && drInFlights[0]["cniNotTDWN"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }

                            }

                            //���۵���ʱ��
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncATA")
                            {
                                if (drInFlights[0]["cncSTATUS"].ToString().Trim() != "ATA")  //�ǵ���
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor; ;
                                }
                                //else if (drInFlights[0]["cnvcDELAY1"].ToString().Trim() != "") //����
                                //{
                                //    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                //}
                                else //����
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                }
                            }

                            //ֵ������
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
                            //��ת�ÿ���Ϣ
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
                            //�ÿ�����
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
                            //���۱�ע
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
                            //�����ص㱣�Ϻ���
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
                            //����λ
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

                        //ֻ�н��ۺ���
                        if (drInFlights.Length > 0 && drOutFlights.Length == 0)
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightNo")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //ֵ����Ϣ
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniCheckNum")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //��ת�����ÿ�
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnbTransitPaxTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //�ÿ�����
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcntPaxNameList")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //��ע
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcOutRemark")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //�ص㱣�Ϻ���
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniFocusTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                        }

                        //ֻ�г��ۺ���
                        if (drInFlights.Length == 0 && drOutFlights.Length > 0)
                        {
                            //
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightNo")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //��ת�ÿ���Ϣ
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnbTransitPaxTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //�ÿ�����
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncntPaxNameList")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //���۱�ע
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcInRemark")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //�����ص㱣�Ϻ���
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncniFocusTag")
                            {
                                fpFlightInfo.Sheets[iDay].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                        }


                        if (drOutFlights.Length > 0)
                        {
                            //��������ʱ��
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
                                //û����ɶ�̬�澯
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
                                //�ص㱣�Ϻ���
                                if (drOutFlights[0]["cniFocusTag"].ToString() != "")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Orange;

                                }

                                //����������
                                else if (drOutFlights[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drOutFlights[0]["cniLEGNO"].ToString()) % 100 != 0)
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }
                                //��վʱ�䲻��
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
                            //��������
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightCharacterAbbreviate")  //��������
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
                                //��ʼ������ʾ
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
                                //��ʼ�ǻ���ʾ
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
                                //���������ʾ
                                if (m_accountBM.MCCRelease == 1 && Convert.ToInt32(guaranteeInforBM.OutcniAllViewIndex) > 2 && drOutFlights[0]["cniMCCRelease"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[iDay].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[iDay].Rows[iRowIndex].BackColor;
                                }
                            }
                            //ֵ����Ϣ
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
                            //��ת�����ÿ�
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
                            //�ÿ�����
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
                            //��ע
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

                             //�����ص㱣�Ϻ���
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

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void GetMaxRecordNo()
        {
            //����ҵ����۲㷽��
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ϵͳ����ʱ��ȡ�������
        /// <summary>
        /// ϵͳ����ʱ��ȡ�������
        /// </summary>
        private void GetChangeData()
        {
            //�������
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region �������¼�������б��У����ж��Ƿ����������ʾ
        /// <summary>
        /// �������¼�������б��У����ж��Ƿ����������ʾ
        /// </summary>
        /// <param name="dtChangeData">�����Ϣ�б�</param>
        /// <param name="iAdd">1=ϵͳ����ʱ�������¼��ӵ�����б����һ�У�0=��ʱˢ��ʱ�������¼�ӵ�һ�в��뵽����б�</param>
        private void AddChangeDataToList(DataTable dtChangeData, int iAdd)
        {
            int iAddRowNum = 0;

            #region �������¼�������б���
            //�������������¼���еļ�¼
            foreach (DataRow dataRow in dtChangeData.Rows)
            {
                //����ԭ��
                string strDelayCode = "";
                //�Ƿ�Ϊ���ۺ���
                int iOutFlight = 0;
                //��ѯ�ַ���
                string strSearch;
                //�����ۺ�����Ϣ
                GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM();

                #region �ж��ǽ��ۺ��໹�ǳ��ۺ���
                //�����ۺ�����Ϣ�б�
                IEnumerator ieTodayInOutFlights = m_ilTodayInOutFlights.GetEnumerator();
                //�������������ۺ�����Ϣ��
                //�ж��ǽ��ۺ��໹�ǳ��ۺ���
                while (ieTodayInOutFlights.MoveNext())
                {
                    guaranteeInforBM = (GuaranteeInforBM)ieTodayInOutFlights.Current;
                    //������ۺ����漰�����
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
                    //������ۺ����漰�����
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

                #region �ж��Ƿ�������ʾ
                //�ж��Ƿ�������ʾ
                if (iAdd == 0)
                {
                    //�û��Ƿ�����������ʾ
                    if (iOutFlight == 1)
                    {
                        strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniSoundPromptItem = 1";
                    }
                    else
                    {
                        strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniSoundPromptItem = 1";
                    }
                    DataRow[] drDataItemSound = m_dtDataItemPurview.Select(strSearch);

                    //�û�����ͨ��������ʾ
                    if (drDataItemSound.Length > 0)
                    {
                        //����û��Զ���������
                        if (m_accountBM.SoundType == 0)
                        {
                            string mstrfileName = Application.StartupPath;
                            mstrfileName = mstrfileName + @"\WAV\front.WAV";
                            SoundHelpers.PlaySound(mstrfileName, IntPtr.Zero, SoundHelpers.PlaySoundFlags.SND_FILENAME | SoundHelpers.PlaySoundFlags.SND_ASYNC);
                        }
                        //��������
                        else
                        {
                            Beep(0X0FF, 100);
                        }
                    }
                }//end if (iAdd == 0)
                #endregion

                #region �ж��Ƿ���Ȩ�ޣ������������
                //�ж��Ƿ���Ȩ�ޣ������������
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

                #region ���ɱ����Ϣ�б�
                //����û���Ȩ��
                if (drDataItemPurview.Length > 0)
                {
                    //�����ETA��ETD����������������Ϊ�գ��򲻽��д���
                    if ((dataRow["cnvcPrimaryNameField"].ToString() == "cncETA" || dataRow["cnvcPrimaryNameField"].ToString() == "cncETD") && strDelayCode == "")
                    {
                        continue;
                    }

                    //�������
                    string strOldContent, strNewContent, strOldDepSTNName, strOldArrSTNName;
                    strOldContent = dataRow["cnvcChangeOldContent"].ToString();             //���ǰ����
                    strNewContent = dataRow["cnvcChangeNewContent"].ToString();             //���������
                    strOldDepSTNName = dataRow["cnvcOldDepSTNName"].ToString();             //���ǰ��ɻ���
                    strOldArrSTNName = dataRow["cnvcOldArrSTNName"].ToString();             //���ǰĿ�Ļ���

                    #region �����������Ŀ���д���

                    #region ����״̬����ת��
                    //����״̬����ת��
                    if (dataRow["cnvcPrimaryNameField"].ToString() == "cncStatusName")
                    {
                        //���ǰ
                        switch (strOldContent)
                        {
                            case "SCH":
                                strOldContent = "�ƻ�";
                                break;
                            case "DEL":
                                strOldContent = "����";
                                break;
                            case "ATD":
                                strOldContent = "�Ƴ�";
                                break;
                            case "DEP":
                                strOldContent = "���";
                                break;
                            case "ARR":
                                strOldContent = "���";
                                break;
                            case "ATA":
                                strOldContent = "����";
                                break;

                        }

                        //�����
                        switch (strNewContent)
                        {
                            case "SCH":
                                strNewContent = "�ƻ�";
                                break;
                            case "DEL":
                                strNewContent = "����";
                                break;
                            case "ATD":
                                strNewContent = "�Ƴ�";
                                break;
                            case "DEP":
                                strNewContent = "����";
                                break;
                            case "ARR":
                                strNewContent = "���";
                                break;
                            case "ATA":
                                strNewContent = "����";
                                break;

                        }
                    }
                    #endregion

                    #region ����ԭ������
                    //����ԭ�����ת��
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

                    //��������
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

                    #region ��������
                    //��ɻ���
                    int iSplitIndex = strOldDepSTNName.IndexOf("/");
                    if (iSplitIndex > 0)
                    {
                        strOldDepSTNName = strOldDepSTNName.Substring(0, iSplitIndex).Trim();
                    }
                    else
                    {
                        strOldDepSTNName = strOldDepSTNName.Trim();
                    }

                    //Ŀ�Ļ���
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

                    #region ���ԭ��
                    //���ԭ��
                    string strChangeReason = dataRow["cnvcChangeReasonName"].ToString();
                    if (dataRow["cncActionTag"].ToString() == "I")
                    {
                        strChangeReason = "��������";
                    }
                    else if (dataRow["cncActionTag"].ToString() == "D")
                    {
                        strChangeReason = "ɾ������";
                    }
                    #endregion

                    #endregion

                    #region ���ɱ����Ϣ�б�
                    //���ɱ����Ϣ�б�
                    string[] strArr = new string[5];
                    strArr[0] = dataRow["cnvcUserName"].ToString();                                 //����Ա
                    //�������
                    strArr[1] = dataRow["cnvcOldFLTID"].ToString() + "(" +                          //�����
                        strOldDepSTNName +                                                          //���ǰ��ɻ���
                        DateTime.Parse(dataRow["cncSTD"].ToString()).ToString("HHmm") + "��" +      //�ƻ����ʱ��
                        DateTime.Parse(dataRow["cncSTA"].ToString()).ToString("HHmm") +             //�ƻ�����ʱ��
                        strOldArrSTNName + ")" + "<" +                                              //���ǰĿ�Ļ���
                        strChangeReason + ">" +                                                     //���ԭ��
                        strOldContent + "->" +                                                      //���ǰ����
                        strNewContent;                                                              //���������
                    strArr[2] = dataRow["cncLocalOperatingTime"].ToString();                        //����ʱ��
                    strArr[3] = dataRow["cncSTD"].ToString().Substring(0, 10);                      //��������

                    //�����б��е�һ����¼
                    ListViewItem objListViewItem = new ListViewItem(strArr);

                    #region ������Ϣ��Ϊ�˶�λ��ؽ���ľ����С� -- added by LinYong in 20150215
                    if (iOutFlight == 1)
                    {
                        objListViewItem.Tag = "Out" + dataRow["cnvcPrimaryNameField"].ToString();
                    }
                    else
                    {
                        objListViewItem.Tag = "In" + dataRow["cnvcPrimaryNameField"].ToString();
                    }
                    #endregion ������Ϣ��Ϊ�˶�λ��ؽ���ľ����С� -- added by LinYong in 20150215

                    //�����ϵͳ����ʱ
                    //�����¼��������ӵ�����б���
                    if (iAdd == 1)
                    {
                        lvChangeContent.Items.Add(objListViewItem);
                    }
                    //�����ϵͳ��ʱˢ��ʱ
                    //�����¼�嵽����б�ĵ�һ��
                    //�Ա���ʾ����ǰ��
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

                    //����漰����ĺ���Ϊ���ۺ��࣬���ɫ��ΪSilver
                    //��Ϊ���ۺ��࣬��ΪĬ����ɫ��ɫ
                    if (dataRow["cncOldArrSTN"].ToString() == m_stationBM.ThreeCode || dataRow["cncNewArrSTN"].ToString() == m_stationBM.ThreeCode)
                    {
                        lvChangeContent.Items[lvChangeContent.Items.Count - 1].BackColor = Color.FromName("Silver");
                    }
                }//end if (drDataItemPurview.Length > 0)
                #endregion

            }//end foreach (DataRow dataRow in dtChangeData.Rows)
            #endregion

            #region ���ñ����¼��ʾ��ɫ
            //������ڶ�ʱˢ��ʱ������µı����Ϣ
            if (iAdd == 0 && iAddRowNum != 0)
            {
                //���α����������ɫΪRed
                for (int iLoop = 0; iLoop < iAddRowNum; iLoop++)
                {
                    lvChangeContent.Items[iLoop].ForeColor = Color.FromName("Red");
                }

                //��һ�α����������ɫΪGreen
                for (int iLoop = iAddRowNum; iLoop < iAddRowNum + m_iRedRecordNum; iLoop++)
                {
                    lvChangeContent.Items[iLoop].ForeColor = Color.FromName("Green");
                }

                //��������ı���ɫΪBlack
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

        #region ѡ��Ļ���ʵ�����
        /// <summary>
        /// ѡ��Ļ���ʵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStations_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_stationBM = new StationBM(m_dtStations.Rows[cmbStations.SelectedIndex]);
            fpFlightInfo.ActiveSheetIndex = 1;

            //��ȡ�������ź����100���������
            GetMaxRecordNo();
            GetChangeData();

            //��ȡ�ú�վ��������к���
            m_dtTodayStationFlights = GetStationFlights(GetDateTimeBM(1), m_stationBM, 1);

            //������Ӧ����ʾ��Ϣ
            ComputeFlightsInfor(m_dtTodayStationFlights);

            //��ȡ��������ۺ���
            m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

            //������
            fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;

            //����ѡ�е���
            if (m_iOldSelectedRow >= FpFlightInfo.Sheets[1].RowCount)
            {
                m_iOldSelectedRow = -1;
            }

            //�����ۺ�����
            SetInOutFlightsNum();

            //��¼��˸��Ԫ�񱳾�ɫ���Ƿ��һ�ν�����˸
            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];

            //��ʼ�������ɫ
            InitialGridColor(shToday);

            //���õ��쵥Ԫ����ɫ
            SetGridColor(1, m_dtTodayStationFlights);

            fpFlightInfo.Focus();
        }
        #endregion

        #region �л�ҳ��
        /// <summary>
        /// �л�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_ActiveSheetChanged(object sender, EventArgs e)
        {
            //����
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                //��ȡ�ú�վ��������к���
                m_dtYesterdayStationFlights = GetStationFlights(GetDateTimeBM(0), m_stationBM, 0);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtYesterdayStationFlights);

                //��ȡ��������ۺ���
                m_ilYesterdayInOutFlights = FillInOutFlights(m_dtYesterdayStationFlights, 0);

                //������
                fpFlightInfo.Sheets[0].DataSource = m_ilYesterdayInOutFlights;

                //�����ۺ�����
                SetInOutFlightsNum();

                //��ʼ�������ɫ
                InitialGridColor(shYestoday);

                //���õ��쵥Ԫ����ɫ
                SetGridColor(0, m_dtYesterdayStationFlights);

                #region yanxian 2013-10-14 ���޸�;�����ÿ�������ȡ
                //��¼ѡ�е��к����Լ�ԭ��ɫ
                m_iOldSelectedRow = -1;
                m_iOldSelectedColumn = -1;
                #endregion
            }
            //����
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                //�����ۺ�����
                SetInOutFlightsNum();
            }
            //����
            else if (fpFlightInfo.ActiveSheetIndex == 2)
            {
                //��ȡ�ú�վ��������к���
                m_dtTomorrowStationFlights = GetStationFlights(GetDateTimeBM(2), m_stationBM, 2);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtTomorrowStationFlights);

                //��ȡ��������ۺ���
                m_ilTomorrowInOutFlights = FillInOutFlights(m_dtTomorrowStationFlights, 0);

                //������
                fpFlightInfo.Sheets[2].DataSource = m_ilTomorrowInOutFlights;

                //�����ۺ�����
                SetInOutFlightsNum();

                //��ʼ�������ɫ
                InitialGridColor(shTomorrow);

                //���õ��쵥Ԫ����ɫ
                SetGridColor(2, m_dtTomorrowStationFlights);

                #region yanxian 2013-10-14 ���޸�;�����ÿ�������ȡ
                //��¼ѡ�е��к����Լ�ԭ��ɫ
                m_iOldSelectedRow = -1;
                m_iOldSelectedColumn = -1;
                #endregion
            }
            //�û�ѡ�������
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                //��ȡ�ú�վ��������к���
                m_dtSelectDateStationFlights = GetStationFlights(GetDateTimeBM(3), m_stationBM, 3);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtSelectDateStationFlights);

                //��ȡ��������ۺ���
                m_ilSelectDateInOutFlights = FillInOutFlights(m_dtSelectDateStationFlights, 0);

                fpFlightInfo.Sheets[3].DataSource = m_ilSelectDateInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();

                #region yanxian 2013-10-14 ���޸�;�����ÿ�������ȡ
                //��¼ѡ�е��к����Լ�ԭ��ɫ  
                m_iOldSelectedRow = -1;
                m_iOldSelectedColumn = -1;
                #endregion
            }

            //modified by LinYong in 20140707
            UpDownValue_ValueChanged(sender, e);

        }
        #endregion

        #region �û�ѡ�����ڽ��в鿴
        /// <summary>
        /// �û�ѡ�����ڽ��в鿴
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
                //��ȡ�ú�վ��������к���
                m_dtSelectDateStationFlights = GetStationFlights(GetDateTimeBM(3), m_stationBM, 3);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtSelectDateStationFlights);

                //��ȡ��������ۺ���
                m_ilSelectDateInOutFlights = FillInOutFlights(m_dtSelectDateStationFlights, 0);

                fpFlightInfo.Sheets[3].DataSource = m_ilSelectDateInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();
            }
            fpFlightInfo.Sheets[3].SheetName = dtFlightDate.Value.ToString("yyyy-MM-dd");

            fpFlightInfo.Focus();
        }
        #endregion

        #region �û�����������ͼʱ������ˢ����ɫ
        /// <summary>
        /// ����������ͼʱ������ˢ����ɫ
        /// </summary>
        public void RefreshView(int iDay)
        {
            timerSplash.Enabled = false;
            DataTable dtStationFlights;

            //��ȡ������Ϣ
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

            //������Ӧ����ʾ��Ϣ
            ComputeFlightsInfor(dtStationFlights);

            //�����ۺ�����Schema
            m_dtInOutFlightsSchema = GetDisplaySchema();

            //��ʾ���к���          
            if (iDay == 0)
            {
                m_ilYesterdayInOutFlights = FillInOutFlights(dtStationFlights, 0);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilYesterdayInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();
            }
            else if (iDay == 1)
            {
                m_ilTodayInOutFlights = FillInOutFlights(dtStationFlights, 1);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilTodayInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();
            }
            else if (iDay == 2)
            {
                m_ilTomorrowInOutFlights = FillInOutFlights(dtStationFlights, 0);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilTomorrowInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();
            }
            else
            {
                m_ilSelectDateInOutFlights = FillInOutFlights(dtStationFlights, 0);
                fpFlightInfo.Sheets[iDay].DataSource = m_ilSelectDateInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();
            }

            //��˸��ʶ
            if (iDay == 1)
            {
                colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];
            }

            //��ʼ�������ɫ
            InitialGridColor(fpFlightInfo.Sheets[iDay]);

            //���ñ����ɫ
            SetGridColor(iDay, dtStationFlights);

            timerSplash.Enabled = true;
        }
        #endregion

        #region ��˸����
        /// <summary>
        /// ��˸����
        /// </summary>
        private void Splash()
        {
            try
            {
                //�����ʾ��������Ϊ��
                if (m_strDataItemSearch == "" || m_strDataItemSearch == null)
                {
                    return;
                }

                //��������ۺ�����Ϣʵ���б�
                IEnumerator ieTodayInOutFlights = m_ilTodayInOutFlights.GetEnumerator();

                DataRow[] drSplashTag;
                //�û�����Ҫ��ʾ��������
                drSplashTag = m_dtSplashTag.Select(m_strDataItemSearch);
                //���û����ʾ��������
                if (drSplashTag.Length <= 0)
                {
                    return;
                }

                int iRowIndex = 0;

                //���ɲ�ѯ�ַ���
                //��ѯ����Ŀ��Ҫ��˸�Ľ����ۺ����¼
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

                //����Ŀ��Ҫ��˸����
                DataRow[] drInOutFlights = m_dtTodayInOutFlights.Select(strSearch);

                //test
                //if (drInOutFlights.Length > 0)
                //    MessageBox.Show(strSearch);




                //���б�������Ŀ��Ҫ��˸�Ľ����ۺ�����Ϣ��¼��
                foreach (DataRow drInOutFlight in drInOutFlights)
                {
                    //test
                    //MessageBox.Show(drInOutFlight["OutcnvcFLTID"].ToString() + " �� " + drInOutFlight["OutcnvcAC"].ToString());




                    //����Ŀ��Ҫ��˸�Ľ����ۺ���ʵ��
                    GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM(drInOutFlight);
                    //�к�
                    iRowIndex = Convert.ToInt32(drInOutFlight["cniRowIndex"].ToString());

                    //���в�����Ҫ��˸���������ֶ�����
                    for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[1].Columns.Count; iLoop++)
                    {
                        //�����ֶ���Ϣ
                        strSearch = "cnvcDataItemID = '" + fpFlightInfo.Sheets[1].Columns[iLoop].DataField + "'";
                        DataRow[] drDataItem = m_dtDataItems.Select(strSearch);

                        //�ֶ�����
                        string strPrimaryCodeField = drDataItem[0]["cnvcPrimaryCodeField"].ToString();
                        //��������������
                        string strDataItemName = drDataItem[0]["cnvcDataItemName"].ToString();

                        //����Ǻ���Ľ�����Ϣ�ֶ�
                        if (fpFlightInfo.Sheets[1].Columns[iLoop].DataField.IndexOf("In") == 0)
                        {
                            //���Һ�����Ϣ
                            strSearch = "cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                                "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                                "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                                "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'";
                        }

                        //����Ǻ���ĳ�����Ϣ�ֶ�
                        else
                        {
                            //���Һ�����Ϣ
                            strSearch = "cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                                "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                                "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                                "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'";
                        }

                        //�жϸ��ֶ��Ƿ���Ҫ��˸
                        drSplashTag = m_dtSplashTag.Select(strSearch);
                        if (drSplashTag.Length > 0)
                        {
                            //����û����õ���˸ʱ�䲻Ϊ��
                            if (drSplashTag[0][strPrimaryCodeField].ToString().Trim() != "")
                            {
                                //����û����õ���˸ʱ�����0
                                if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) > 0)
                                {
                                    //������˸��ż�������Ŀ��ɫΪ��ɫ���������ɫ�ָ�

                                    #region ��ǰʱ���������ż��
                                    //����ǵ�ǰʱ���������ż��
                                    if (DateTime.Now.Second % 2 == 0)
                                    {
                                        //����洢���ڴ��е���˸�����˸ʱ�����û����õ���˸ʱ����ͬ
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

                                        //��Ԫ����ɫ��Ϊ��ɫ
                                        fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor = Color.Red;

                                        //�����б�����˸
                                        //����б���Ϊ����
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[1, iLoop].BackColor = Color.Red;
                                        }
                                        else
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[0, iLoop].BackColor = Color.Red;
                                        }

                                        //����û������Զ�ֹͣ��˸��������������˸����ʱ��
                                        if (m_accountBM.SplashAutoStop == 1 || Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            //��˸����ʱ��ݼ�һ��
                                            drSplashTag[0][strPrimaryCodeField] = Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) - 1;
                                        }

                                        //�����˸����ʱ��Ϊ0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //�ָ���Ԫ��ԭ������ɫ
                                            fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                            //�ָ��б����ԭ������ɫ
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

                                    #region ��ǰʱ�������������
                                    //����ǵ�ǰʱ�������������
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

                                        //�ָ�����Ԫ��ԭ������ɫ
                                        fpFlightInfo.Sheets[1].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                        //�б���
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                        }
                                        else
                                        {
                                            fpFlightInfo.Sheets[1].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                        }

                                        //��˸����ʱ��ݼ�һ��
                                        if (m_accountBM.SplashAutoStop == 1 || Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            drSplashTag[0][strPrimaryCodeField] = Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) - 1;
                                        }

                                        //�������ʱ��Ϊ0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //�ָ�ԭ������ɫ
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

        #region �������ý����ۺ�����ʾֵ
        /// <summary>
        /// �������ý����ۺ�����ʾֵ
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
                //�����͵�����Ϣ
                guaranteeInforBM["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                guaranteeInforBM["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                guaranteeInforBM["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                guaranteeInforBM["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                guaranteeInforBM["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();

                foreach (DataRow dataRowItems in m_dtDataItems.Rows)
                {
                    //��ʽ�������ֶ�
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
                    //��ʽ�������ֶ�
                    string strFieldValue = FormatOUTItem(dataRow, dataRowItems);

                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        guaranteeInforBM[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }
            }
        }
        #endregion

        #region ��ȡ�������
        public void GetChangeDate(object state)
        {
            //����ҵ����۲㷽��
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

        #region ��ʱ��ȡ����������
        /// <summary>
        /// ��ʱ��ȡ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerChange_Tick(object sender, EventArgs e)
        {
            //��ͼˢ�±��
            int iRefresh = 0;

            try
            {
                //���ö��̻߳��������ʱˢ��ʱ����ͨ��GetChangeDate(object state)������ʱ��ȡ�����Ϣ
                Monitor.Enter(oMutexChangeRecords);
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();

                //�Ƿ���Ҫˢ����ͼ
                DataRow[] drChangeRecords = m_dtChangeTable.Select("cniRefresh = 1");

                #region ��Ҫ������֯��ͼ
                //��Ҫ������֯��ͼ
                if (drChangeRecords.Length > 0)
                {
                    iRefresh = 1;
                    timerSplash.Enabled = false;

                    //�ϴα�������ݿ�����ȡ�ĺ��ද̬ʵ�����
                    ChangeLegsBM previousChangeLegsBM = new ChangeLegsBM();
                    DataTable dtChangeLegs = new DataTable();

                    //���д�������¼
                    foreach (DataRow dataRow in m_dtChangeTable.Rows)
                    {
                        //���α��ʵ�����
                        ChangeLegsBM changeLegsBM = new ChangeLegsBM();

                        //���ݱ��ǰ���������ɱ��ʵ��
                        //�Թؼ��ָ�ֵ
                        changeLegsBM.DATOP = dataRow["cncOldDATOP"].ToString();
                        changeLegsBM.FLTID = dataRow["cnvcOldFLTID"].ToString();
                        changeLegsBM.LEGNO = Convert.ToInt32(dataRow["cniOldLEGNO"].ToString());
                        changeLegsBM.AC = dataRow["cnvcOldAC"].ToString();

                        //���ݱ������������ɱ��ʵ��
                        ChangeLegsBM changeNewKeyLegsBM = new ChangeLegsBM();
                        //�Թؼ��ֽ��и�ֵ
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

                        //���ɲ�ѯ���
                        string strSearch = "cncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "' OR " +
                            "cncDATOP = '" + dataRow["cncNewDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + dataRow["cnvcNewFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + dataRow["cniNewLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + dataRow["cnvcNewAC"].ToString() + "'";

                        string strSearchSplashTag = strSearch;

                        //���ݱ��α�����ڴ���в�ѯ���ද̬����˸��̬
                        DataRow[] drStationFlights = m_dtTodayStationFlights.Select(strSearch);
                        DataRow[] drSplashTag = m_dtSplashTag.Select(strSearch);

                        //�����ȡ����غ�����ϢAnd��˸����û�иú������˸��̬��¼
                        if (drStationFlights.Length > 0 && drSplashTag.Length <= 0)
                        {
                            //����һ����Ϣ����˸��̬��¼
                            DataRow drTempSplashTag = m_dtSplashTag.NewRow();
                            drTempSplashTag["cncDATOP"] = drStationFlights[0]["cncDATOP"].ToString();
                            drTempSplashTag["cnvcFLTID"] = drStationFlights[0]["cnvcFLTID"].ToString();
                            drTempSplashTag["cniLEGNO"] = drStationFlights[0]["cniLEGNO"].ToString();
                            drTempSplashTag["cnvcAC"] = drStationFlights[0]["cnvcAC"].ToString();
                            m_dtSplashTag.Rows.Add(drTempSplashTag);

                            //��������¼�����ѯ�����
                            drSplashTag = m_dtSplashTag.Select(strSearch);
                        }

                        //���ݱ����ĺ�����Ϣ��ѯ����ı�����Ϣ
                        #region modified by LinYong in 2015.02.06
                        //dtChangeLegs = guaranteeInforBF.GetFlightByKey(changeNewKeyLegsBM).Dt;
                        dtChangeLegs = guaranteeInforBF.GetFlightByKey_NotCompress(changeNewKeyLegsBM, m_accountBM).Dt;
                        #endregion modified by LinYong in 2015.02.06
                        //���ǰ�ĺ�����Ϣ
                        previousChangeLegsBM = changeLegsBM;

                        #region ��վ���ද̬����Ӧ�ļ�¼
                        //��վ���ද̬����Ӧ�ļ�¼
                        if (drStationFlights.Length > 0)
                        {
                            int iSplash = 0;

                            #region ���ݺ���Ϊ���ۻ���ۣ��ֱ��ȡ�Ƿ���˸
                            //�ڽ��ۺ����в�ѯ
                            string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                            DataRow[] drInFlights = m_dtTodayInOutFlights.Select(strInSearch);

                            //�ڳ��ۺ����в�ѯ
                            string strOutSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                "OutcnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                            //"IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                            DataRow[] drOutFlights = m_dtTodayInOutFlights.Select(strOutSearch);

                            //����ǽ��ۺ���ı����Ϣ
                            if (drInFlights.Length > 0)
                            {
                                //���ݱ��ԭ��
                                strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                //��ѯ�漰�������������
                                DataRow[] drInDataItem = m_dtDataItems.Select(strSearch);
                                //������ڣ�����˸�����Ϊ1
                                if (drInDataItem.Length > 0)
                                {
                                    iSplash = 1;
                                }
                            }

                            //����ǳ��ۺ���ı����Ϣ
                            if (drOutFlights.Length > 0)
                            {
                                //���ݱ��ԭ��
                                strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                //��ѯ�漰�������������
                                DataRow[] drOutDataItem = m_dtDataItems.Select(strSearch);
                                //������ڣ�����˸�����Ϊ1
                                if (drOutDataItem.Length > 0)
                                {
                                    iSplash = 1;
                                }
                            }
                            #endregion

                            //�ж��Ƿ���˸��ʾ
                            strSearch = "cnvcPrimaryCodeField = '" + dataRow["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
                            DataRow[] drDataItemSplash = m_dtDataItemPurview.Select(strSearch);

                            //����������ɾ������AND���ݱ����ĺ���ؼ��ֲ�ѯ���ౣ����Ϣ�����Ϊ��
                            if (dataRow["cncActionTag"].ToString() != "D" && dtChangeLegs.Rows.Count > 0)
                            {
                                //�ñ��������ݸ��¸ú���ı�����Ϣ
                                drStationFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;

                                //�¼Ӵ��룬ȷ���ú������˸��Ϣ�ļ�¼�ĺ���ؼ���ϢΪ���±����Ϣ -- modified by linyong in 2011.08.10 
                                drSplashTag[0]["cncDATOP"] = dtChangeLegs.Rows[0]["cncDATOP"].ToString();
                                drSplashTag[0]["cnvcFLTID"] = dtChangeLegs.Rows[0]["cnvcFLTID"].ToString();
                                drSplashTag[0]["cniLEGNO"] = Convert.ToInt32(dtChangeLegs.Rows[0]["cniLEGNO"].ToString());
                                drSplashTag[0]["cnvcAC"] = dtChangeLegs.Rows[0]["cnvcAC"].ToString();


                                #region ������˸ʱ��
                                //�����Ҫ��˸��ʾ
                                if (drDataItemSplash.Length > 0)
                                {
                                    //��������������ΪETA����ETD
                                    if (dataRow["cnvcChangeReasonCode"].ToString() == "cncETA" || dataRow["cnvcChangeReasonCode"].ToString() == "cncETD")
                                    {
                                        //�������ԭ��Ϊ��AND��˸���=1
                                        if (dtChangeLegs.Rows[0]["cnvcDELAY1"].ToString() != "" && iSplash == 1)
                                        {
                                            //������˸ʱ��
                                            drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds.ToString();
                                        }
                                    }
                                    //�����˸���=1
                                    else if (iSplash == 1)
                                    {
                                        //��������������Ϊ����״̬
                                        if (dataRow["cnvcChangeReasonCode"].ToString() == "cncSTATUS")
                                        {
                                            //�������״̬��ΪDEP
                                            if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEP")
                                            {
                                                //�������ʱ������ʱ�䵥Ԫ��Ҳ��˸
                                                drSplashTag[0]["cncTDWN"] = m_accountBM.SplashSeconds;
                                                drSplashTag[0]["cncTOFF"] = m_accountBM.SplashSeconds;
                                            }

                                            //�������״̬���ΪDEL
                                            if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEL")
                                            {
                                                //�����Ԥ�����ʱ���Ԥ��ʱ��Ҳ��˸
                                                drSplashTag[0]["cncETD"] = m_accountBM.SplashSeconds;
                                                drSplashTag[0]["cncETA"] = m_accountBM.SplashSeconds;
                                            }
                                        }

                                        //�������������������˸ʱ��
                                        drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds;
                                    }
                                }
                                #endregion

                            }
                            //��������ɾ������OR���ݱ����ĺ���ؼ��ֲ�ѯ���ౣ����Ϣ���Ϊ��
                            else if (dataRow["cncActionTag"].ToString() == "D" || dtChangeLegs.Rows.Count == 0)
                            {
                                //�Ӻ��ද̬�����˸��̬���н���ؼ�¼ɾ��
                                m_dtTodayStationFlights.Rows.Remove(drStationFlights[0]);
                                m_dtSplashTag.Rows.Remove(drSplashTag[0]);
                            }

                            //������������
                            m_dtTodayStationFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                            m_dtTodayStationFlights = m_dtTodayStationFlights.DefaultView.Table;

                            //������Ӧ����ʾ��Ϣ
                            ComputeFlightsInfor(m_dtTodayStationFlights);

                            //��ʾ���к���                    
                            m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

                            //���°�
                            fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;
                        }
                        #endregion

                        #region ��վ���ද̬����Ӧ�ļ�¼���������ࣩ
                        //��վ���ද̬����Ӧ�ļ�¼
                        else
                        {
                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //����һ��
                                DataRow drFlight = m_dtTodayStationFlights.NewRow();
                                //�����������ݲ��뺽����Ϣ����
                                drFlight.ItemArray = dtChangeLegs.Rows[0].ItemArray;

                                //������˸��¼
                                DataRow drSplash = m_dtSplashTag.NewRow();
                                drSplash["cncDATOP"] = drFlight["cncDATOP"].ToString();
                                drSplash["cnvcFLTID"] = drFlight["cnvcFLTID"].ToString();
                                drSplash["cniLEGNO"] = drFlight["cniLEGNO"].ToString();
                                drSplash["cnvcAC"] = drFlight["cnvcAC"].ToString();

                                //��������
                                m_dtTodayStationFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                                m_dtTodayStationFlights = m_dtTodayStationFlights.DefaultView.Table;

                                //������Ӧ����ʾ��Ϣ
                                ComputeFlightsInfor(m_dtTodayStationFlights);

                                //��ʾ���к���                    
                                m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);
                                //���°�
                                fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;

                                #region ���ݺ���Ϊ���ۻ���ۣ��ֱ��ȡ�Ƿ���˸
                                int iSplash = 0;

                                string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                    "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                    "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                    "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";
                                DataRow[] drInFlights = m_dtTodayInOutFlights.Select(strInSearch);

                                //�ڳ��۸ۺ����в�ѯ
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

                                //�ж��Ƿ���˸��ʾ
                                strSearch = "cnvcPrimaryCodeField = 'cnvcLONG_REG' AND cniSplashPromptItem = 1";
                                DataRow[] drDataItemSplash = m_dtDataItemPurview.Select(strSearch);

                                //���÷ɻ��ŵ�Ԫ����˸ʱ��
                                if (drDataItemSplash.Length > 0 && iSplash == 1)
                                {
                                    drSplash["cnvcLONG_REG"] = m_accountBM.SplashSeconds.ToString();
                                }

                                //���¼�¼��ӵ�������Ϣ�����˸����
                                m_dtTodayStationFlights.Rows.Add(drFlight);
                                m_dtSplashTag.Rows.Add(drSplash);
                            }
                        }
                        #endregion
                    }

                    //�����ۺ�����
                    SetInOutFlightsNum();

                    colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];

                    //��ʼ�������ɫ
                    InitialGridColor(shToday);

                    //���ñ����ɫ
                    SetGridColor(1, m_dtTodayStationFlights);

                    timerSplash.Enabled = true;
                }
                #endregion

                #region ����Ҫ������֯��ͼ
                //����Ҫ������֯��ͼ
                else
                {
                    DataTable dtChangeLegs = new DataTable();
                    ChangeLegsBM previousChangeLegsBM = new ChangeLegsBM();

                    //������������¼
                    foreach (DataRow dataRow in m_dtChangeTable.Rows)
                    {
                        //���ݱ��ǰ���������ɱ��ʵ��
                        ChangeLegsBM changeLegsBM = new ChangeLegsBM();
                        changeLegsBM.DATOP = dataRow["cncOldDATOP"].ToString();
                        changeLegsBM.FLTID = dataRow["cnvcOldFLTID"].ToString();
                        changeLegsBM.LEGNO = Convert.ToInt32(dataRow["cniOldLEGNO"].ToString());
                        changeLegsBM.AC = dataRow["cnvcOldAC"].ToString();

                        //��ѯ���
                        string strSearch = "cncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                        //��ѯ�漰����ĺ������˸��Ϣ
                        DataRow[] drPositionFlights = m_dtTodayStationFlights.Select(strSearch);
                        DataRow[] drSplashTag = m_dtSplashTag.Select(strSearch);

                        if (!(changeLegsBM.DATOP == previousChangeLegsBM.DATOP && changeLegsBM.FLTID == previousChangeLegsBM.FLTID && changeLegsBM.LEGNO == previousChangeLegsBM.LEGNO && changeLegsBM.AC == previousChangeLegsBM.AC))
                        {
                            //���ݱ��ǰ�ĺ�����Ϣ��ѯ����ı�����Ϣ
                            #region modified by LinYong in 2015.02.06
                            //dtChangeLegs = guaranteeInforBF.GetFlightByKey(changeLegsBM).Dt;
                            dtChangeLegs = guaranteeInforBF.GetFlightByKey_NotCompress(changeLegsBM, m_accountBM).Dt;
                            #endregion modified by LinYong in 2015.02.06

                            previousChangeLegsBM = changeLegsBM;
                        }

                        #region ���ݺ���Ϊ���ۻ���ۣ��ֱ��ȡ�Ƿ���˸
                        int iSplash = 0;
                        //�ڽ��ۺ����в�ѯ
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

                        #region ������˸ʱ��
                        //����Ӧ�ļ�¼
                        if (drPositionFlights.Length > 0)
                        {
                            //�ж��Ƿ���˸��ʾ
                            strSearch = "cnvcPrimaryCodeField = '" + dataRow["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
                            DataRow[] drDataItemSplash = m_dtDataItemPurview.Select(strSearch);

                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //���º�����Ϣ
                                drPositionFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                //����������ʾ
                                SetInOutFlightDataRowValue(drPositionFlights[0]);
                                //������˸ʱ��
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

                //�������¼��ӵ������ʾ����
                if (m_dtChangeTable.Rows.Count != 0)
                {
                    AddChangeDataToList(m_dtChangeTable, 0);
                }

                if (iRefresh == 0)
                {
                    //������Ӧ����ʾ��Ϣ
                    ComputeFlightsInfor(m_dtTodayStationFlights);

                    //���ñ����ɫ
                    SetGridColor(1, m_dtTodayStationFlights);
                }

                //��ձ����¼
                m_dtChangeTable.Rows.Clear();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //�ͷŻ������
                Monitor.Exit(oMutexChangeRecords);
            }

            if (m_iAutoAdjust == 1)
            {
                AdjustScreen();
            }
        }
        #endregion

        #region ��˸��ʱ���¼�
        /// <summary>
        /// ��˸��ʱ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSplash_Tick(object sender, EventArgs e)
        {
            Splash();
            //ÿ��ϵͳ�ڱ���ʱ�� 5���Զ�ˢ�º��ද̬
            if (DateTime.UtcNow.Hour == 21 && DateTime.UtcNow.Minute == 0 && DateTime.UtcNow.Second == 10)
            {
                FlightRefresh();
            }
        }
        #endregion

        #region ��ȡѡ�еĽ����ۺ��ද̬ʵ�����
        /// <summary>
        /// ��ȡѡ�еĽ����ۺ��ද̬ʵ�����
        /// </summary>
        /// <param name="iRow">ѡ�е��к�</param>
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

            //���ۺ�����Ϣ
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


            //���ۺ�����Ϣ
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

        #region ������Ԫ���¼���ʹ��˸ֹͣ
        /// <summary>
        /// ������Ԫ���¼���ʹ��˸ֹͣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!e.ColumnHeader)
            {
                #region yanxian 2013-10-14;�л�����Ϊ��������б�
                dataTabControlGanttView.SelectedIndex = 0;
                //intabControl.SelectedIndex = 0; //���ڽ��� ���ۺ���tab ʱ���� -- modified by LinYong in 20160316
                //outtabControl.SelectedIndex = 0; //���ڽ��� ���ۺ���tab ʱ���� -- modified by LinYong in 20160316
                //msgTabPage.Text = "��������¼��";
                #endregion

                lastInChangeLegsBM = inChangeLegsBM;
                lastInChangeLegsBM = outChangeLegsBM;

                GetInOutChangeLegsBM(e.Row);
                
                #region modified by LinYong in 20150330
                //string strOutTitle = DateTime.Parse(outChangeLegsBM.DATOP).ToString("MM��dd��;") + outChangeLegsBM.FLTID.ToString() + ";" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN;
                //string strInTitle = DateTime.Parse(inChangeLegsBM.DATOP).ToString("MM��dd��;") + inChangeLegsBM.FLTID.ToString() + ";" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN;
                string strInTitle = "";
                string strOutTitle = "";
                #endregion modified by LinYong in 20150330

                if (inChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strInTitle = "��";
                }
                else    //added by LinYong in 20150403
                {
                    strInTitle = DateTime.Parse(inChangeLegsBM.FlightDate).ToString("MM��dd��;") + inChangeLegsBM.FLTID.ToString() + ";" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN;
                }

                if (outChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strOutTitle = "��";
                }
                else    //added by LinYong in 20150403
                {
                    strOutTitle = DateTime.Parse(outChangeLegsBM.FlightDate).ToString("MM��dd��;") + outChangeLegsBM.FLTID.ToString() + ";" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN;

                }

                inTabPage.Text =  "���ۺ���(" + strInTitle + ")";
                outTabPage.Text =  "���ۺ���(" +strOutTitle + ")";
            }

            # region �����ؽ���Ϊ"����"ʱ,�¼�����
            if (fpFlightInfo.ActiveSheetIndex == 1 && !e.ColumnHeader)
            {
                GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(m_ilTodayInOutFlights as ArrayList)[e.Row];

                string strChangeReasonCode = "";
                string strDataItemName = "";
                string strSearch = "";

                if (fpFlightInfo.Sheets[1].Columns[e.Column].DataField.IndexOf("In") == 0) //���ۺ���
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

                //���õ�Ԫ��ѡ����ɫ
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

            #region yanxian 2013-10-14;�����ؽ���Ϊ"����"ʱ,�¼�����
            if (fpFlightInfo.ActiveSheetIndex == 0 && !e.ColumnHeader)
            {
                //���õ�Ԫ��ѡ����ɫ
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

            #region yanxian 2013-10-14;�����ؽ���Ϊ"����"ʱ,�¼�����
            if (fpFlightInfo.ActiveSheetIndex == 2 && !e.ColumnHeader)
            {
                //���õ�Ԫ��ѡ����ɫ
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

            #region LinYong 2014-4-10;�����ؽ���Ϊ"ѡ������"ʱ,�¼�����
            if (fpFlightInfo.ActiveSheetIndex == 3 && !e.ColumnHeader)
            {
                //���õ�Ԫ��ѡ����ɫ
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

            #region �����б�����Һ���
            if (e.ColumnHeader)
            {
                if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcFlightNo" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcFlightNo")
                {
                    string strDataField = fpFlightInfo.ActiveSheet.Columns[e.Column].DataField;
                    //�������ݱ仯����ֶ���Ϣ
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

                    //��ǰ��ʾ�������б�
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



                    //����ѡ�еļ�¼
                    IEnumerator ieFlightData = ilFlightData.GetEnumerator();
                    GuaranteeInforBM findGuaranteeInfo = null;
                    int iRowIndex = 0;
                    //���ݷɻ��Ų���
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
                    else   //���ݺ���Ų���
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

                        //���ó��µ�ѡ�е���ɫ
                        fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                        fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor = Color.White;

                    }

                }
            }
            #endregion

            #region �����Ҽ��˵�
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

        #region ���ݺ���Ų��Һ���
        /// <summary>
        /// ���ݺ���Ų��Һ���
        /// </summary>
        public void FindFlightByFlightNo(string strFlightNo)
        {
            //��ǰ��ʾ�������б�
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
            //����ѡ�еļ�¼
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

                //���ó��µ�ѡ�е���ɫ
                fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor = Color.White;

            }
        }
        #endregion

        #region ˢ�º��ද̬
        /// <summary>
        /// ˢ�º��ද̬
        /// </summary>
        public void FlightRefresh()
        {
            if (fpFlightInfo.ActiveSheetIndex == 0)
            {
                //��ȡ�ú�վ��������к���
                m_dtYesterdayStationFlights = GetStationFlights(GetDateTimeBM(0), m_stationBM, 0);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtYesterdayStationFlights);

                //��ȡ��������ۺ���
                m_ilYesterdayInOutFlights = FillInOutFlights(m_dtYesterdayStationFlights, 0);

                fpFlightInfo.Sheets[0].DataSource = m_ilYesterdayInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();

                //��ʼ�������ɫ
                InitialGridColor(shYestoday);

                //���õ��쵥Ԫ����ɫ
                SetGridColor(0, m_dtYesterdayStationFlights);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 1)
            {
                //��ȡ�������ź����100���������
                GetMaxRecordNo();

                //��ȡ�ú�վ��������к���
                m_dtTodayStationFlights = GetStationFlights(GetDateTimeBM(1), m_stationBM, 1);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtTodayStationFlights);


                //�����ۺ�����Schema
                m_dtInOutFlightsSchema = GetDisplaySchema();

                //��ȡ��������ۺ���
                m_ilTodayInOutFlights = FillInOutFlights(m_dtTodayStationFlights, 1);

                fpFlightInfo.Sheets[1].DataSource = m_ilTodayInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();

                //��ȡ�����¼
                GetChangeData();

                //��¼��˸��Ԫ�񱳾�ɫ���Ƿ��һ�ν�����˸
                colorArrOldBackGround = new Color[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];
                //iFirstEnterSplash = new int[fpFlightInfo.Sheets[1].Rows.Count, fpFlightInfo.Sheets[1].Columns.Count];
                //for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[1].Rows.Count; iLoop++)
                //{
                //    for (int jLoop = 0; jLoop < fpFlightInfo.Sheets[1].Columns.Count; jLoop++)
                //    {
                //        iFirstEnterSplash[iLoop, jLoop] = 1;
                //    }
                //}

                //��ʼ�������ɫ
                InitialGridColor(shToday);

                //���õ��쵥Ԫ����ɫ
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
                //��ȡ�ú�վ��������к���
                m_dtTomorrowStationFlights = GetStationFlights(GetDateTimeBM(2), m_stationBM, 2);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtTomorrowStationFlights);

                //��ȡ��������ۺ���
                m_ilTomorrowInOutFlights = FillInOutFlights(m_dtTomorrowStationFlights, 0);

                fpFlightInfo.Sheets[2].DataSource = m_ilTomorrowInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();

                //��ʼ�������ɫ
                InitialGridColor(shTomorrow);

                //���õ��쵥Ԫ����ɫ
                SetGridColor(2, m_dtTomorrowStationFlights);
            }
            else if (fpFlightInfo.ActiveSheetIndex == 3)
            {
                //��ȡ�ú�վ��������к���
                m_dtSelectDateStationFlights = GetStationFlights(GetDateTimeBM(3), m_stationBM, 3);

                //������Ӧ����ʾ��Ϣ
                ComputeFlightsInfor(m_dtSelectDateStationFlights);

                //��ȡ��������ۺ���
                m_ilSelectDateInOutFlights = FillInOutFlights(m_dtSelectDateStationFlights, 0);

                fpFlightInfo.Sheets[3].DataSource = m_ilSelectDateInOutFlights;
                //�����ۺ�����
                SetInOutFlightsNum();
            }

        }
        #endregion

        #region ˫����Ԫ���¼�
        /// <summary>
        /// ˫����Ԫ���¼�
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

        #region ά���������������
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

            //��ȡ��˫����Ԫ�������еĽ����ۺ���ʵ��
            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(ilInOutFlights as ArrayList)[e.Row];

            //����
            string strDataItemID = fpFlightInfo.ActiveSheet.Columns[e.Column].DataField.ToString();
            //��ȡ�û�Ȩ����Ϣ
            DataRow[] dataRow = m_dtDataItemPurview.Select("cnvcDataItemID = '" + strDataItemID + "'");
            //�ж��Ƿ���Ȩ��
            if (dataRow[0]["cnvcPrimaryCodeField"].ToString() != "cnbVIPTag")
            {
                if (dataRow[0]["cniDataItemPurview"].ToString() != "2")
                {
                    MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //�û�Ȩ�޼���
            int iDataItemPurview = Convert.ToInt32(dataRow[0]["cniDataItemPurview"].ToString());
            //�ֶγ���
            int iFieldLength = Convert.ToInt32(dataRow[0]["cniFieldLength"].ToString());
            //ά������
            int iMainTainType = Convert.ToInt32(dataRow[0]["cniMaintenType"].ToString());
            //�ֶ����ͣ�1=�ı���2=����
            int iFieldType = Convert.ToInt32(dataRow[0]["cniFieldType"].ToString());
            //��˫����Ԫ���ӦtbGuaranteeInfo���е��ֶ�
            string strPrimaryCodeField = dataRow[0]["cnvcPrimaryCodeField"].ToString();

            //����ά��ʵ��
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //�������ʵ��
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //���ද̬���ʵ��
            ChangeLegsBM changeLegsBM = new ChangeLegsBM();

            //�ֶ�����
            maintenGuaranteeInforBM.FieldType = iFieldType;
            //���ǰ��Ԫ���ֵ
            maintenGuaranteeInforBM.OldContent = fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text;
            //����
            maintenGuaranteeInforBM.ColumnCaption = dataRow[0]["cnvcDataItemName"].ToString();

            #region ά�����ۺ�������
            //���ά���Ľ��ۺ��������
            if (strDataItemID.IndexOf("In") == 0 && guaranteeInforBM.IncncDATOP != "1900-01-01")
            {
                //ά����Ϣ
                maintenGuaranteeInforBM.DATOP = guaranteeInforBM.IncncDATOP;
                maintenGuaranteeInforBM.FLTID = guaranteeInforBM.IncnvcFLTID;
                maintenGuaranteeInforBM.LEGNO = guaranteeInforBM.IncniLEGNO;
                maintenGuaranteeInforBM.AC = guaranteeInforBM.IncnvcAC;
                maintenGuaranteeInforBM.FieldName = strPrimaryCodeField;
                maintenGuaranteeInforBM.FieldLength = iFieldLength;

                //������Ϣ
                changeLegsBM.DATOP = guaranteeInforBM.IncncDATOP;
                changeLegsBM.FLTID = guaranteeInforBM.IncnvcFLTID;
                changeLegsBM.LEGNO = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeLegsBM.AC = guaranteeInforBM.IncnvcAC;

                //�����Ϣ
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

            #region ά�����ۺ��������
            //���ۺ�������ͣ��λ
            else if (strDataItemID.IndexOf("Out") == 0 && guaranteeInforBM.OutcncDATOP != "1900-01-01" || strDataItemID == "IncnvcInGATE")
            {
                //ά����Ϣ
                maintenGuaranteeInforBM.DATOP = guaranteeInforBM.OutcncDATOP;
                maintenGuaranteeInforBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                maintenGuaranteeInforBM.LEGNO = guaranteeInforBM.OutcniLEGNO;
                maintenGuaranteeInforBM.AC = guaranteeInforBM.OutcnvcAC;
                maintenGuaranteeInforBM.FieldName = strPrimaryCodeField;
                maintenGuaranteeInforBM.FieldLength = iFieldLength;

                //������Ϣ
                changeLegsBM.DATOP = guaranteeInforBM.OutcncDATOP;
                changeLegsBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                changeLegsBM.LEGNO = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeLegsBM.AC = guaranteeInforBM.OutcnvcAC;

                //�����Ϣ
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

            #region �����ֶ����ͷֱ���
            //ʱ���ı���ά������=1
            if (iMainTainType == 1)
            {
                fmMaintenTime objfmMaintenTime = new fmMaintenTime(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenTime.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTime.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //�����ı���ά������=2
            else if (iMainTainType == 2)
            {
                fmMaitenSingleText objfmMaitenSingleText = new fmMaitenSingleText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaitenSingleText.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaitenSingleText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //�����ı���ά������=3
            else if (iMainTainType == 3)
            {
                fmMaintenMutiLineText objfmMaintenMutiLineText = new fmMaintenMutiLineText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenMutiLineText.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenMutiLineText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //�����б�ά������=4
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
                #region ������4�������������������
                //VIP���
                if (strPrimaryCodeField == "cnbVIPTag")
                {
                    fmMaintenVIP objfmMaintenVIP = new fmMaintenVIP(changeLegsBM, m_accountBM, iDataItemPurview);
                    objfmMaintenVIP.ShowDialog();
                }
                //���ۻ�λ
                else if (strPrimaryCodeField == "cnvcInGATE")
                {
                    MaintenGuaranteeInforBM outMaintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                    ChangeRecordBM outChangeRecordBM = new ChangeRecordBM();

                    int iOut = 0;

                    //������ۺ�������νӵĳ��ۺ���
                    if (guaranteeInforBM.OutcncDATOP != "1900-01-01")
                    {
                        //���ۺ���ά����Ϣ
                        outMaintenGuaranteeInforBM.DATOP = guaranteeInforBM.OutcncDATOP;
                        outMaintenGuaranteeInforBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                        outMaintenGuaranteeInforBM.LEGNO = guaranteeInforBM.OutcniLEGNO;
                        outMaintenGuaranteeInforBM.AC = guaranteeInforBM.OutcnvcAC;
                        outMaintenGuaranteeInforBM.FieldName = "cnvcOutGate";
                        outMaintenGuaranteeInforBM.FieldLength = iFieldLength;
                        outMaintenGuaranteeInforBM.FieldType = 1;

                        //���ۺ�������Ϣ
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
                        //���ñ�ǣ���ʶ���νӳ��ۺ���
                        iOut = 1;
                    }

                    fmMaintenInGate objfmMaintenInGate = new fmMaintenInGate(maintenGuaranteeInforBM, outMaintenGuaranteeInforBM, changeRecordBM, outChangeRecordBM, iOut);
                    if (objfmMaintenInGate.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenInGate.MMaintenGuaranteeInforBM.NewContent;
                    }
                }
                //ֵ������
                else if (strPrimaryCodeField == "cniCheckNum" || strPrimaryCodeField == "cnvcBookNum" || strPrimaryCodeField == "cnvcInGATE")
                {
                    fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(changeLegsBM, changeRecordBM);
                    if (objfmCheckPax.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmCheckPax.NewContent;
                    }
                }
                //�ÿ�����
                else if (strPrimaryCodeField == "cntPaxNameList")
                {
                    fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(changeLegsBM, changeRecordBM);
                    if (objfmMaintenPaxNameList.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenPaxNameList.NewContent;
                    }
                }
                //��ת�����ÿ�
                else if (strPrimaryCodeField == "cnbTransitPaxTag")
                {
                    fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(changeLegsBM, changeRecordBM);
                    if (objfmMaintenTransitPax.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTransitPax.NewContent;
                    }
                }
                //����ʱ��
                else if (strPrimaryCodeField == "cncTDWN")
                {
                    changeRecordBM.Refresh = 1;
                    fmMaintenTDWN objfmMaintenTDWN = new fmMaintenTDWN(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenTDWN.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTDWN.NewContent;
                    }
                }
                //���ʱ��
                else if (strPrimaryCodeField == "cncTOFF")
                {
                    changeRecordBM.Refresh = 1;
                    fmMaintenTOFF objfmMaintenTOFF = new fmMaintenTOFF(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenTOFF.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTOFF.NewContent;
                    }
                }
                //������
                else if (strPrimaryCodeField == "cniTotalFuelWeight")
                {
                    fmMaintenFuel objfmMaintenFuel = new fmMaintenFuel(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenFuel.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenFuel.NewContent;
                    }
                }
                //���۱�����Ϣ����
                else if (strPrimaryCodeField == "cnvcInGuaranteeRecord")
                {
                    fmGuaranteeRecord objfmGuaranteeRecord = new fmGuaranteeRecord(m_accountBM, m_stationBM, inChangeLegsBM, FlightOrientation.In);
                    objfmGuaranteeRecord.ShowDialog();
                }
                //���۱�����Ϣ����
                else if (strPrimaryCodeField == "cnvcOutGuaranteeRecord")
                {
                    fmGuaranteeRecord objfmGuaranteeRecord = new fmGuaranteeRecord(m_accountBM, m_stationBM, outChangeLegsBM, FlightOrientation.Out);
                    objfmGuaranteeRecord.ShowDialog();
                }
                #region �����ۺ����ע��Ա added by LinYong in 20160324
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
                            commanderTypeBM.cnvcCommanderType = "����','����','�ⳡ";
                            break;
                        case "cnvcOutMaintenanceUsers":
                        case "cnvcInMaintenanceUsers":
                            commanderTypeBM.cniCommanderTypeID = 20;
                            commanderTypeBM.cnvcCommanderType = "����";
                            break;
                        case "cnvcOutFleetUsers":
                        case "cnvcInFleetUsers":
                            commanderTypeBM.cniCommanderTypeID = 30;
                            commanderTypeBM.cnvcCommanderType = "����";
                            break;
                        case "cnvcOutVIPRoomUsers":
                        case "cnvcInVIPRoomUsers":
                            commanderTypeBM.cniCommanderTypeID = 40;
                            commanderTypeBM.cnvcCommanderType = "�����";
                            break;
                        case "cnvcOutAviationDietUsers":
                        case "cnvcInAviationDietUsers":
                            commanderTypeBM.cniCommanderTypeID = 50;
                            commanderTypeBM.cnvcCommanderType = "��ʳ";
                            break;
                        case "cnvcOutCheckInUsers":
                        case "cnvcInCheckInUsers":
                            commanderTypeBM.cniCommanderTypeID = 60;
                            commanderTypeBM.cnvcCommanderType = "ֵ��";
                            break;
                        case "cnvcOutCleaningTeamUsers":
                        case "cnvcInCleaningTeamUsers":
                            commanderTypeBM.cniCommanderTypeID = 70;
                            commanderTypeBM.cnvcCommanderType = "����";
                            break;
                    }
                    fmSelectUsers objfmSelectUsers = new fmSelectUsers(m_stationBM, commanderTypeBM, maintenGuaranteeInforBM.OldContent, maintenGuaranteeInforBM, changeRecordBM);
                    if (objfmSelectUsers.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmSelectUsers.CommanderList_New;
                    }
                }
                #endregion �����ۺ����ע��Ա added by LinYong in 20160324
                #endregion
            }
            #endregion
        }
        #endregion

        #region �س������û��༭
        /// <summary>
        /// �س������û��༭
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

        #region �������Ҽ�
        /// <summary>
        /// �������Ҽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_KeyUp(object sender, KeyEventArgs e)
        {

            if (fpFlightInfo.ActiveSheetIndex == 1 && m_iOldSelectedRow != -1)
            {
                //��ԭ��ɫ
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


                //���ó��µ�ѡ�е���ɫ
                fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                fpFlightInfo.Sheets[1].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = Color.White;

                fpFlightInfo.Sheets[1].AddSelection(m_iOldSelectedRow, 0, 1, fpFlightInfo.Sheets[1].ColumnCount);
            }
        }
        #endregion

        #region ����ѡ��ı���б��е����ݲ��Һ��ද̬
        /// <summary>
        /// ����ѡ��ı���б��е����ݲ��Һ��ද̬
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
                    #region ȷ�� �� ��λ�ã�����λ��ѡ��λ�ã����û���ҵ� �У���λ�� �� -- added by LinYong in 20150215
                    //�� �� DataItemNo
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

                    #endregion ȷ�� �� ��λ�ã�����λ��ѡ��λ�ã����û���ҵ� �У���λ�� �� -- added by LinYong in 20150215

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

                    //���ó��µ�ѡ�е���ɫ
                    fpFlightInfo.ActiveSheet.Cells[iRecordNo, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                    fpFlightInfo.ActiveSheet.Cells[iRecordNo, m_iOldSelectedColumn].ForeColor = Color.White;
                }
            }

            fpFlightInfo.Focus();
        }
        #endregion

        #region �Ŵ���С��Ԫ��
        /// <summary>
        /// �Ŵ�
        /// </summary>
        public void ZoomOut()
        {
            UpDownValue.Value += (decimal)0.01;
            FpFlightInfo.ZoomFactor = (float)UpDownValue.Value;
        }

        /// <summary>
        /// ��С
        /// </summary>
        public void ZoomIn()
        {
            UpDownValue.Value -= (decimal)0.01;
            FpFlightInfo.ZoomFactor = (float)UpDownValue.Value;
        }

        /// <summary>
        /// �Ŵ���Сֵ�仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpDownValue_ValueChanged(object sender, EventArgs e)
        {
            fpFlightInfo.ZoomFactor = (float)UpDownValue.Value;
            fpFlightInfo.Focus();
        }
        #endregion

        #region �������ද̬
        /// <summary>
        /// �������ද̬
        /// </summary>
        public void ExportData()
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel�ļ�(*.xls)|*.xls";
            //�����ļ�ʱѯ��
            saveExcel.OverwritePrompt = true;
            saveExcel.Title = "Excel�ļ����Ϊ";
            saveExcel.FileName = "���ද̬" + DateTime.Now.ToString("yyyy-MM-dd");
            //����Ĭ��·��
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
                        //�ָ��б�ǩ����ɫ
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                    }
                    else
                    {
                        //�ָ��б�ǩ����ɫ
                        //					objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Control");

                        MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        #endregion

        /// <summary>
        /// ����ҵ����ʾ ͳ�����ظ������˹�����ͳ�ƴ���
        /// </summary>
        public void BalanceStatistics()
        {
            string selectDate = dtFlightDate.Value.ToString("yyyy��MM��dd��");
            string selectStation = cmbStations.Text;
            IList iListDataSource = fpFlightInfo.ActiveSheet.DataSource as IList;
            //DataTable dataTableDataSource = (fpFlightInfo.ActiveSheet.DataSource as DataTable).Copy();

            //fmBalanceStatistics objfmBalanceStatistics = new fmBalanceStatistics(selectDate, selectStation, dataTableDataSource);
            fmBalanceStatistics objfmBalanceStatistics = new fmBalanceStatistics(selectDate, selectStation, iListDataSource);

            objfmBalanceStatistics.Show();
        }

        #region ��ӡ�����ۺ��ද̬
        /// <summary>
        /// ��ӡ�����ۺ��ද̬
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
            //			pi.Header = "���Ϻ��պ�������۶�̬��(" + SelectedStationInfo.AirportName + SelectedDate  + ")";
            //		
            //		
            //			
            //			pi.Footer = "��ӡʱ��:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "                  ��ӡ��:" + UserInfo.UserName;
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
            //			//����ҳ�߾�
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
            pi.Header = "/fn\"����\"/fz\"14.25\"/fb1/fi0/fu0/fk0/c" + "���Ϻ��պ�������۶�̬��(" + m_stationBM.AirportName + printDate + ")" + "/n";
            pi.Footer = "��ӡʱ��:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "                  ��ӡ��:" + m_accountBM.UserName;

            fpFlightInfo.Sheets[3].PrintInfo = pi;
            //�������öԻ���
            fpFlightInfo.PrintSheet(3);

            //			fpFlightInfo.ActiveSheetIndex = iOldSheetIndex;
        }
        #endregion

        #region ���ද̬���ʵ��
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
                //������Ϣ
                //�����Ϣ
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
                //������Ϣ
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

        #region ��ȡ���ද̬ʵ��
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
                //������Ϣ
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
                //������Ϣ
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

        #region ������ά��ʵ��
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

            //ά����Ϣ
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
                maintenGuaranteeInforBM.ColumnCaption = "�����ص㱣�Ϻ���";
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
                maintenGuaranteeInforBM.ColumnCaption = "�����ص㱣�Ϻ���";
            }

            return maintenGuaranteeInforBM;
        }
        #endregion

        #region �Ҽ��˵��¼�
        #region ���ۺ��ຽ����Ϣ
        /// <summary>
        /// ���ۺ��ຽ����Ϣ
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

        #region ���۱������
        /// <summary>
        /// ���۱������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutChangeData_Click(object sender, EventArgs e)
        {
            fmChangeData objfmChangeData = new fmChangeData(GetDateTimeBM(fpFlightInfo.ActiveSheetIndex), m_accountBM, outChangeLegsBM.FlightNo, "", 0);
            objfmChangeData.ShowDialog();
        }
        #endregion

        #region ���ۺ����ص㱣�Ϻ���˵��
        /// <summary>
        /// ���ۺ����ص㱣�Ϻ���˵��
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region �ж��Ƿ���Ȩ��
        /// <summary>
        /// �ж��Ƿ���Ȩ��
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

        #region ����ֵ����Ϣ
        /// <summary>
        /// ����ֵ����Ϣ
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ������ת�ÿ���Ϣ
        /// <summary>
        /// ������ת�ÿ���Ϣ
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region �����ÿ�����
        /// <summary>
        /// �����ÿ�����
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region ���ۼ�������мƻ�
        /// <summary>
        /// ���ۼ�������мƻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutFlightPlan_Click(object sender, EventArgs e)
        {
            #region ���� �������ڣ�UTC�� ��Ϊ�ж�������modified by LinYong in 2013.07.31
            //fmComputerPlan objfmComputerPlan = new fmComputerPlan(outChangeLegsBM.FlightDate, outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN);
            fmComputerPlan objfmComputerPlan = new fmComputerPlan(outChangeLegsBM.FlightDate, outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN, outChangeLegsBM.DATOP);
            #endregion ���� �������ڣ�UTC�� ��Ϊ�ж�������modified by LinYong in 2013.07.31

            objfmComputerPlan.ShowDialog();
            objfmComputerPlan.Text += outChangeLegsBM.FlightNo;
        }
        #endregion

        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutTaskSheet_Click(object sender, EventArgs e)
        {
            string strURL = "http://crw.hnair.net/WebUI/PilotWebUI/PrintFltTask/wfmPrintFltTask.aspx";
            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "-������" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        #region ����ǩ�ɷ��е�
        /// <summary>
        /// ����ǩ�ɷ��е�
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

        #region ��������
        /// <summary>
        /// ��������
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
            objfmSurpportInfor.Text += "����" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        #region ����������׼
        /// <summary>
        /// ����������׼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutWeatherStandard_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/notam/notam/standard/AirStandard.asp?CODEINPUT=" + outChangeLegsBM.ARRFourCode;

            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "������׼" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        #region ���ۺ���ͨ��
        /// <summary>
        /// ���ۺ���ͨ��
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
            objfmSurpportInfor.Text += "����ͨ��" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }
        #endregion

        /// <summary>
        /// ���ۻ�����Ϣ
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
            objfmSurpportInfor.Text += "����" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ���ۺ��߷���
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
            objfmSurpportInfor.Text += "���߷���" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ������������
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
            objfmSurpportInfor.Text += "��������" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ���۱�������
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
            objfmSurpportInfor.Text += "��������" + outChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ���ۻ���ǩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutCrewSign_Click(object sender, EventArgs e)
        {
            fmCrewSignIn objfmCrewSignIn = new fmCrewSignIn(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN);
            objfmCrewSignIn.ShowDialog();
        }

        /// <summary>
        /// ���۳���ǩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutStewardSign_Click(object sender, EventArgs e)
        {
            fmStewardSignIn objfmStewardSignIn = new fmStewardSignIn(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), outChangeLegsBM.FlightNo, outChangeLegsBM.DEPSTN);
            objfmStewardSignIn.ShowDialog();
        }

        /// <summary>
        /// ���ۺ��ຽ����Ϣ
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
        /// ���۱������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInChangeData_Click(object sender, EventArgs e)
        {
            fmChangeData objfmChangeData = new fmChangeData(GetDateTimeBM(fpFlightInfo.ActiveSheetIndex), m_accountBM, inChangeLegsBM.FlightNo, "", 0);
            objfmChangeData.ShowDialog();
        }

        /// <summary>
        /// ���ۺ����ص㱣�Ϻ���
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// ����ֵ����Ϣ
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            m_statusBar.Panels[0].Text = "��½�û�:" + m_accountBM.UserName;
            m_statusBar.Panels[1].Text = "����:" + drInFlights.Length + "      " + "����:" + drOutFlights.Length;
        }

        /// <summary>
        /// ���ۼ�������мƻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInFlightPlan_Click(object sender, EventArgs e)
        {
            #region ���� �������ڣ�UTC�� ��Ϊ�ж�������modified by LinYong in 2013.07.31
            //fmComputerPlan objfmComputerPlan = new fmComputerPlan(inChangeLegsBM.FlightDate, inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN);
            fmComputerPlan objfmComputerPlan = new fmComputerPlan(inChangeLegsBM.FlightDate, inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN, inChangeLegsBM.ARRSTN, inChangeLegsBM.DATOP);
            #endregion ���� �������ڣ�UTC�� ��Ϊ�ж�������modified by LinYong in 2013.07.31

            objfmComputerPlan.ShowDialog();
            objfmComputerPlan.Text += inChangeLegsBM.FlightNo;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInTaskSheet_Click(object sender, EventArgs e)
        {
            string strURL = "http://crw.hnair.net/WebUI/PilotWebUI/PrintFltTask/wfmPrintFltTask.aspx";
            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "������" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ����ǩ�ɷ��е�
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
        /// ��������
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
            objfmSurpportInfor.Text += "����" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ����������׼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInWeatherStandard_Click(object sender, EventArgs e)
        {
            string strURL = "http://flight.hnair.com/notam/notam/standard/AirStandard.asp?CODEINPUT=" + inChangeLegsBM.ARRFourCode;

            fmSurpportInfor objfmSurpportInfor = new fmSurpportInfor(strURL);
            objfmSurpportInfor.Text += "������׼" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ���ۺ���ͨ��
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
            objfmSurpportInfor.Text += "����ͨ��" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ����
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
            objfmSurpportInfor.Text += "����" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ���ۺ��߷���
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
            objfmSurpportInfor.Text += "���߷���" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ������������
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
            objfmSurpportInfor.Text += "��������" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }

        /// <summary>
        /// ���۱�������
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
            objfmSurpportInfor.Text += "��������" + inChangeLegsBM.FlightNo;
            objfmSurpportInfor.ShowDialog();
        }


        /// <summary>
        /// ���ۻ���ǩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInCrewSign_Click(object sender, EventArgs e)
        {
            fmCrewSignIn objfmCrewSignIn = new fmCrewSignIn(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), inChangeLegsBM.FlightNo, inChangeLegsBM.DEPSTN);
            objfmCrewSignIn.ShowDialog();
        }

        /// <summary>
        /// ���۳���ǩ��
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

        #region ��ʽ�����ۺ��������ֶ�
        /// <summary>
        /// ��ʽ�����ۺ��������ֶ�
        /// </summary>
        /// <param name="dataRow">��������</param>
        /// <param name="dataRowItems">������</param>
        /// <returns>��ʽ���������</returns>
        public string FormatINItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //���ۺ�����ɻ���
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //���ۺ��ൽ�����
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //�ƻ�����ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncSTA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //����ʱ��̸�ʽ
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
            //���ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncTDWN")
            {
                //if (strStatus == "DEP" || strStatus == "ARR" || strStatus == "ATA")
                if (strStatus == "ARR" || strStatus == "ATA") //ʱ����ص���ʱ����ʾ���ʱ��
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //��λʱ��̸�ʽ
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

        #region ��ʽ�����ۺ��������ֶ�
        /// <summary>
        /// ��ʽ�����ۺ��������ֶ�
        /// </summary>
        /// <param name="dataRow">��������</param>
        /// <param name="dataRowItems">������</param>
        /// <returns>��ʽ���������</returns>
        public string FormatOUTItem(DataRow dataRow, DataRow dataRowItems)
        {
            string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
            string strStatus = dataRow["cncSTATUS"].ToString().Trim();
            //���ۺ�����ɻ���
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncDEPAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //���ۺ�����ػ���
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncARRAirportCNAME")
            {
                int iSplitIndex = strFieldValue.IndexOf("/");
                if (iSplitIndex > 0)
                {
                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                }
            }
            //�ƻ����ʱ��̸�ʽ
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTD")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //�������ʱ��̸�ʽ
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
            //�Ƴ�ʱ��
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
            //��ɶ�̬�̸�ʽ
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
            //�������
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcniDUR1")
            {
                if (strFieldValue == "0")
                {
                    strFieldValue = "";
                }
            }

            #region added by LinYong in 2016.05.04
            //��������|�ƻ�
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
            }
            //��������|Ԥ��
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncETA")
            {
                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");

            }
            //��������|���
            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncTDWN")
            {
                //if (strStatus == "DEP" || strStatus == "ARR" || strStatus == "ATA")
                if (strStatus == "ARR" || strStatus == "ATA") //ʱ����ص���ʱ����ʾ���ʱ��
                {
                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                }
                else
                {
                    strFieldValue = "";
                }
            }
            //��������|��λ
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

        #region �����ÿ���Ϣ
        /// <summary>
        /// ���ۺ����ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //�ж��Ƿ�ѡ��ĳһ��

                if (m_iOldSelectedRow == -1)
                {
                    MessageBox.Show("��ѡ�����ĳһ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string strOutTitle = DateTime.Parse(outChangeLegsBM.FlightDate).ToString("MM��dd��;") + outChangeLegsBM.FLTID.ToString() + ";" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN; //DATOP ��Ϊ FlightDate -- modified by LinYong in 20160316
                string strInTitle = DateTime.Parse(inChangeLegsBM.FlightDate).ToString("MM��dd��;") + inChangeLegsBM.FLTID.ToString() + ";" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN; //DATOP ��Ϊ FlightDate -- modified by LinYong in 20160316
                if (inChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strInTitle = "��";
                }
                if (outChangeLegsBM.FLTID.Equals("HU 0000"))
                {
                    strOutTitle = "��";
                }

                #region �������ͼ
                if (dataTabControlGanttView.SelectedIndex == 4)
                {

                    /*****************************************************************/
                    //��������нڵ�
                    this.ganttViewToolBox.remove();

                    DataSet oDataSet = new DataSet();//���ۺ���
                    DataSet iDataSet = new DataSet();//���ۺ���

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
                            oFlightParams.FlightType = "ʼ��";
                            this.ganttViewToolBox.DTINSTD = DateTime.MinValue;
                        }
                        else
                        {
                            oFlightParams.FlightType = "��վ";
                        }


                        PublicService.PublicService airPortService = new AirSoft.FlightMonitor.FlightMonitorClient.PublicService.PublicService();
                        DataSet dataAirPortSet = new DataSet();
                        dataAirPortSet = airPortService.GetAirportInfo(outChangeLegsBM.ARRSTN);

                        if (dataAirPortSet != null || dataAirPortSet.Tables[0].Rows.Count != 0)
                        {
                            if (!"CHN".Equals(dataAirPortSet.Tables[0].Rows[0]["Country"].ToString()))
                            {
                                oFlightParams.OIFlightType = "����";
                            }
                            else
                            {
                                oFlightParams.OIFlightType = "����";
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
                            iFlightParams.FlightType = "ʼ��";
                        }
                        else
                        {
                            iFlightParams.FlightType = "��վ";
                        }
                        //iFlightParams.FlightType = "ʼ��";
                        
                        PublicService.PublicService airPortService = new AirSoft.FlightMonitor.FlightMonitorClient.PublicService.PublicService();
                        DataSet dataAirPortSet = new DataSet();
                        dataAirPortSet = airPortService.GetAirportInfo(inChangeLegsBM.ARRSTN);

                        if (dataAirPortSet != null || dataAirPortSet.Tables[0].Rows.Count != 0)
                        {
                            if (!"CHN".Equals(dataAirPortSet.Tables[0].Rows[0]["Country"].ToString()))
                            {
                                iFlightParams.OIFlightType = "����";
                            }
                            else
                            {
                                iFlightParams.OIFlightType = "����";
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

                #region ���ۺ��ദ��
                if (dataTabControlGanttView.SelectedIndex == 1)
                {
                    inTabPage.Text = "���ۺ���" + "(" + strInTitle + ")";

                    intabControl_SelectedIndexChanged(sender, e);
                }
                #endregion

                #region ���ۺ��ദ��
                if (dataTabControlGanttView.SelectedIndex == 2)
                {
                    outTabPage.Text = "���ۺ���" + "(" + strOutTitle + ")";
                    
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


        #region yanxian 2013-10-16 ListView��ֵ
        private void showListViewData(ListView lListView, DataRow[] dataRow)
        {
            lListView.Clear();

            lListView.BeginUpdate();   //���ݸ��£�UI��ʱ����ֱ��EndUpdate���ƿؼ���������Ч������˸�������߼����ٶ�   

            lListView.Columns.Add("���", 50, HorizontalAlignment.Center);
            lListView.Columns.Add("����", 80, HorizontalAlignment.Right);
            lListView.Columns.Add("��λ��", 50, HorizontalAlignment.Left);
            lListView.Columns.Add("����", 150, HorizontalAlignment.Left);
            lListView.Columns.Add("��ϵ��ʽ", 300, HorizontalAlignment.Left);
            lListView.Columns.Add("�Ա�", 50, HorizontalAlignment.Left);
            lListView.Columns.Add("�����", 300, HorizontalAlignment.Left);
            lListView.Columns.Add("ֵ����ʽ", 80, HorizontalAlignment.Left);

            lListView.View = View.Details;
            lListView.Visible = true;
            lListView.FullRowSelect = true;
            lListView.Scrollable = true;
            lListView.GridLines = true;

            for (int i = 0; i < dataRow.Length; i++)   //���������   
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

            lListView.EndUpdate();  //�������ݴ���UI����һ���Ի��ơ�  
        }
        #endregion


        private void intabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(intabControl);

            #region �����鴦��
            if (intabControl.SelectedTab.Name.ToUpper() == "inTaskBooktabPage".ToUpper())
            {
                //���ۺ����������������ʾ
                intabControl_Taskbook_SelectedIndexChanged(sender, e);

                try
                {
                    ReturnValueSF returnValueSF = new ReturnValueSF();
                    TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
                    string strCurrentTime = ""; //��ǰ��ѯʱ��
                    string strLastTime = ""; //�ϴβ�ѯʱ��
                    DataTable dataTable_1 = null;
                    #region �ж�ǰ�����β�ѯͬһ�����ʱ���������Ʒ�������������Ƶ��
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
                                    //MessageBox.Show("�˺����ϴβ�ѯʱ�䣺" +
                                    //    dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString() +
                                    //    "�����30���Ż�������ȡ���ݣ�", "�������ѯ");

                                    this.m_statusBar.Panels[2].Text = "��" + inChangeLegsBM.FLTID.Replace(" ", "") + "�������ϴβ�ѯʱ�䣺" +
                                        dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString().Trim() + "�����30���Ż�������ȡ���������ݣ�";
                                    return;
                                }
                            }

                        }
                    }
                    #endregion �ж�ǰ�����β�ѯͬһ�����ʱ���������Ʒ�������������Ƶ��
                    if (inChangeLegsBM != null)
                    {
                        dataTable_1 = taskBookServiceBF.GetVoyageReportDataBySingleFlight_In(
                            inChangeLegsBM.FlightDate,   //inChangeLegsBM.DATOP, -- modified by LinYong in 20150330
                            inChangeLegsBM.FlightNo.Replace(" ", ""),
                            inChangeLegsBM.LONG_REG,
                            (inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN),
                            inChangeLegsBM.STD.Substring(0, 16),
                            inChangeLegsBM.STA.Substring(0, 16)).Dt;

                        strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //��¼��ǰ����ʱ��
                    }


                    //dataGridView1.DataSource = dataTable_1;

                    #region ��������Դ���ֶ������Ϣ �� DataGridView
                    //�����
                    dataGridView1.Columns.Clear();
                    foreach (DataColumn dataColumndataTable_1 in dataTable_1.Columns)
                    {
                        dataGridView1.Columns.Add(dataColumndataTable_1.ColumnName, dataColumndataTable_1.Caption);
                    }
                    //�����
                    dataGridView1.Rows.Clear();
                    foreach (DataRow dataRowdataTable_1 in dataTable_1.Rows)
                    {
                        //
                        dataRowdataTable_1["cnvcExecFlightInfo"] = dataRowdataTable_1["cnvcExecFlightInfo"].ToString().Replace("�� ", ("��" + Environment.NewLine));

                        //
                        dataGridView1.Rows.Add(dataRowdataTable_1.ItemArray);
                    }


                    #endregion ��������Դ���ֶ������Ϣ �� DataGridView


                    #region ���� �ϼ���
                    dataGridView1.Rows[0].Frozen = true;
                    #endregion ���� �ϼ���


                    #region �������
                    //��Ԫ����
                    if (chkinTaskbookConfiguration_NewLine.Checked)
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    else
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                    //�����и�
                    foreach (DataGridViewRow dataGridViewRowdataGridView1 in dataGridView1.Rows)
                    {
                        if (dataGridViewRowdataGridView1.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
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
                                dataGridViewColumn.HeaderText = "���";
                                dataGridViewColumn.Width = 55;
                                //dataGridViewColumn.Visible = false;
                                break;
                            case "CNVCPOSITION":
                                dataGridViewColumn.HeaderText = "λ�� ";
                                dataGridViewColumn.Width = 140;
                                break;
                            case "CNVCNAME":
                                dataGridViewColumn.HeaderText = "���� ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCLEVEL":
                                dataGridViewColumn.HeaderText = "���� ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCSID":
                                dataGridViewColumn.HeaderText = "����֤�� ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCDEPARRSTN":
                                dataGridViewColumn.HeaderText = "��˺��� ";
                                dataGridViewColumn.Width = 120;
                                break;
                            case "CNVCMOBILE":
                                dataGridViewColumn.HeaderText = "��ϵ�绰 ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCEXECFLIGHTINFO":
                                dataGridViewColumn.HeaderText = "ִ�к��ࣨ�¶Σ� ";
                                if (chkinTaskbookConfiguration_NewLine.Checked)
                                    dataGridViewColumn.Width = 500;
                                else
                                    dataGridViewColumn.Width = 720;

                                dataGridViewColumn.DisplayIndex = 4;
                                break;
                            case "CNVCMEMO":
                                dataGridViewColumn.HeaderText = "��ע ";
                                break;
                        }
                    }
                    #endregion �������


                    #region �Ƚ� �� ���� ��ǰ������
                    //���� ��ǰ���������
                    if (_dataTableTaskBooks == null)    //null����ʾ��δ���������򴴽�����
                    {
                        #region ���ɱ��
                        try
                        {
                            _dataTableTaskBooks = new DataTable();
                            DataColumn dataColumn = null;

                            dataColumn = new DataColumn("cnvcDATOP", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��������";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcFlightNo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "�����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLONG_REG", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "�ɻ���";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSTD", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "���ʱ��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcROUTE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
                            dataColumn.Caption = "���";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "λ��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����֤��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��˺���";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��ϵ�绰";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "ִ�к��ࣨ�϶λ��¶Σ�";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��ע";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcOperateTime", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����ʱ��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                        }
                        catch (Exception ex)
                        {
                            _dataTableTaskBooks = null;
                            throw new Exception(ex.Message);
                        }
                        #endregion ���ɱ��
                    }

                    #region ������ۺ���
                    #region �ְ汾��ԭ�汾���бȽ�
                    if ((inChangeLegsBM != null) && (dataTable_1 != null) && (dataTable_1.Columns.Count > 0))
                    {
                        DataTable dataTableCompareIn = dataTable_1.Copy();  //�ְ汾����
                        dataTableCompareIn.Columns.Add("cnvcCompareResult", Type.GetType("System.String"));  //���� �ȽϽ����


                        foreach (DataRow dataRow_dataTableTaskBooks in _dataTableTaskBooks.Select("(cnvcDATOP = '" + inChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + inChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + inChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + inChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN + "') "))  //����ԭ�汾
                        {
                            //�ϴβ�ѯʱ��
                            strLastTime = dataRow_dataTableTaskBooks["cnvcOperateTime"].ToString();

                            //
                            if (dataTable_1.Select("(cnvcSID = '" + dataRow_dataTableTaskBooks["cnvcSID"].ToString() + "') ").Length == 0)  //�ж� �ְ汾 �� �Ƿ� ���� ԭ�汾����
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
                                dataRowdataTableCompareIn["cnvcCompareResult"] = "ɾ��";

                                dataTableCompareIn.Rows.Add(dataRowdataTableCompareIn);
                            }
                        }


                        foreach (DataRow dataRowdataTableCompareIn in dataTableCompareIn.Rows)  //�����ְ汾
                        {
                            if (_dataTableTaskBooks.Select("(cnvcDATOP = '" + inChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + inChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + inChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + inChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + inChangeLegsBM.DEPSTN + "-" + inChangeLegsBM.ARRSTN + "') and " +
                            "(cnvcSID = '" + dataRowdataTableCompareIn["cnvcSID"].ToString() + "') ").Length > 0)  //�ж� ԭ�汾 �� �Ƿ� ���� �ְ汾����
                            {
                                dataRowdataTableCompareIn["cnvcCompareResult"] = "����";
                            }
                            else
                            {
                                if (strLastTime != "")
                                    dataRowdataTableCompareIn["cnvcCompareResult"] = "����";
                                else
                                    dataRowdataTableCompareIn["cnvcCompareResult"] = "";

                            }
                        }



                        //���ĺϼ�����Ϣ
                        if (dataTableCompareIn.Select("cnvcPosition = '�ϼ�'").Length == 1)
                        {
                            dataTableCompareIn.Select("cnvcPosition = '�ϼ�'")[0]["cnvcExecFlightInfo"] = "�ϴβ�ѯ��" + strLastTime + " -- ���β�ѯ��" + strCurrentTime;
                            dataTableCompareIn.Select("cnvcPosition = '�ϼ�'")[0]["cnvcCompareResult"] = "";
                        }

                        //
                        //dataGridView3.DataSource = dataTableCompareIn;

                        #region ��������Դ���ֶ������Ϣ �� DataGridView
                        //�����
                        dataGridView3.Columns.Clear();
                        foreach (DataColumn dataColumndataTableCompareIn in dataTableCompareIn.Columns)
                        {
                            dataGridView3.Columns.Add(dataColumndataTableCompareIn.ColumnName, dataColumndataTableCompareIn.Caption);
                        }
                        //�����
                        dataGridView3.Rows.Clear();
                        foreach (DataRow dataRowdataTableCompareIn in dataTableCompareIn.Rows)
                        {
                            dataGridView3.Rows.Add(dataRowdataTableCompareIn.ItemArray);
                        }


                        #endregion ��������Դ���ֶ������Ϣ �� DataGridView


                        #region ���� �ϼ���
                        dataGridView3.Rows[0].Frozen = true;
                        #endregion ���� �ϼ���

                        #region �������
                        //��Ԫ����
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        else
                            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                        //�����и�
                        foreach (DataGridViewRow dataGridViewRowdataGridView3 in dataGridView3.Rows)
                        {
                            if (dataGridViewRowdataGridView3.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
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
                                    dataGridViewColumn.HeaderText = "���";
                                    dataGridViewColumn.Width = 45;
                                    //dataGridViewColumn.Visible = false;
                                    break;
                                case "CNVCPOSITION":
                                    dataGridViewColumn.HeaderText = "λ�� ";
                                    dataGridViewColumn.Width = 140;
                                    break;
                                case "CNVCNAME":
                                    dataGridViewColumn.HeaderText = "���� ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCLEVEL":
                                    dataGridViewColumn.HeaderText = "���� ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCSID":
                                    dataGridViewColumn.HeaderText = "����֤�� ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCDEPARRSTN":
                                    dataGridViewColumn.HeaderText = "��˺��� ";
                                    dataGridViewColumn.Width = 120;
                                    break;
                                case "CNVCMOBILE":
                                    dataGridViewColumn.HeaderText = "��ϵ�绰 ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCEXECFLIGHTINFO":
                                    dataGridViewColumn.HeaderText = "ִ�к��ࣨ�¶Σ� ";
                                    if (chkinTaskbookConfiguration_NewLine.Checked)
                                        dataGridViewColumn.Width = 500;
                                    else
                                        dataGridViewColumn.Width = 720;

                                    dataGridViewColumn.DisplayIndex = 4;
                                    break;
                                case "CNVCMEMO":
                                    dataGridViewColumn.HeaderText = "��ע ";
                                    break;
                                case "CNVCCOMPARERESULT":
                                    dataGridViewColumn.HeaderText = "�ȽϽ�� ";
                                    dataGridViewColumn.Width = 60;
                                    dataGridViewColumn.DisplayIndex = 1;
                                    break;
                            }
                        }

                        #endregion �������

                    }
                    else
                    {
                        dataGridView3.DataSource = null;

                    }
                    #endregion �ְ汾��ԭ�汾���бȽ�

                    #region ɾ�� ���� ԭ����
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
                    #endregion ɾ�� ���� ԭ����


                    #region ���� ���� ��ǰ����������
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
                    #endregion ���� ���� ��ǰ����������


                    #endregion ������ۺ���

                    #endregion �Ƚ� �� ���� ��ǰ������


                    #region �����飨��������
                    if ((inChangeLegsBM != null) && (dataTable_1 != null) && (dataTable_1.Columns.Count > 0))
                    {
                        DataRow[] dataRowsdataTable_1 = dataTable_1.Select("cnvcPosition = '�ϼ�'");
                        if (dataRowsdataTable_1.Length == 1)
                        {
                            int intPos_1 = -1, intPos_2 = -1;
                            string strFltReportId = "";


                            intPos_1 = dataRowsdataTable_1[0]["cnvcExecFlightInfo"].ToString().IndexOf("��������ID��");
                            intPos_2 = dataRowsdataTable_1[0]["cnvcExecFlightInfo"].ToString().IndexOf("��");
                            if (intPos_2 > (intPos_1 + "��������ID��".Length))
                            {
                                strFltReportId = dataRowsdataTable_1[0]["cnvcExecFlightInfo"].ToString().Substring((intPos_1 + "��������ID��".Length), (intPos_2 - intPos_1 - "��������ID��".Length));
                                string strWebBrowser = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/CrewFltTaskNew.aspx?cniId=" +
                                    strFltReportId;
                                webBrowser1.Navigate(strWebBrowser);
                            }
                        }
                    }
                    #endregion �����飨��������


                }
                catch (Exception ex)
                {
                    string errMessage = ex.Message;
                }

            }
            #endregion

            #region �����ÿ���Ϣ����
            if (intabControl.SelectedTab.Name.ToUpper() == "inPSRtabPage".ToUpper())
            {
                //Ȩ���ж� -- modified by LinYong in 20160316
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

                    MessageBox.Show("�Բ�����û�в鿴�ÿ���Ϣ��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                //�����ÿ�tab��ǩ������ʾ
                intabControl_PSR_SelectedIndexChanged(sender, e);

                if (lastInChangeLegsBM != null && lastOutChangeLegsBM != null && (lastSelectedRow == m_iOldSelectedRow)
                    && (DateTime.Now < lastSearchDateTime.AddSeconds(30)))
                {
                    //MessageBox.Show("�˺����ϴβ�ѯʱ�䣺" +
                    //                    lastSearchDateTime.ToString("yyyy-MM-dd HH:mm:ss") +
                    //                    "�����30���Ż�������ȡ���ݣ�", "�����ÿ����ݲ�ѯ");
                    this.m_statusBar.Panels[2].Text = "��" + lastInChangeLegsBM.FLTID.Replace(" ","") + "�������ϴβ�ѯʱ�䣺" + lastSearchDateTime.ToString("HH:mm:ss") + "�����30���Ż�������ȡ�ÿ����ݣ�";
                }
                else
                {
                    DateTime thisSearchDateTime = DateTime.Now;
                    DataSet inDataSet = new DataSet();
                    DataSet zjDataSet = new DataSet();
                    if (inChangeLegsBM.FLTID.Equals("HU 0000") || outChangeLegsBM == null)
                    {
                        //to do;Ҫ��ҳ�����ֵ��ʼ��
                        return;
                    }
                    #region ���ۺ����ÿ�����չʾ
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

                    //���ۺ����ÿ����ݴ���
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

                    showListViewData(intVIPListView, t_inVipTraveller);//Ҫ����Ϣ
                    showListViewData(inSSListView, t_inSsTraveller);//�ط�
                    showListViewData(inMealListView, t_inMealTraveller);//�ز�
                    showListViewData(inTSTListView, t_inTSTTraveller);//��ת
                    showListViewData(inclistView, t_inJINTraveller);//���ÿ�
                    showListViewData(inplistView, t_inOTHTTraveller);//��ͨ�ÿ�

                    inVipTabPage.Text = "Ҫ����Ϣ(" + t_inVipTraveller.Length.ToString() + "��)";
                    inSSTabPage.Text = "�ط��ÿ���Ϣ(" + t_inSsTraveller.Length.ToString() + "��)";
                    inMealTabPage.Text = "�ز��ÿ���Ϣ(" + t_inMealTraveller.Length.ToString() + "��)";
                    inTSTabPage.Text = "��ת�ÿ���Ϣ(" + t_inTSTTraveller.Length.ToString() + "��)";
                    inOTHTabPage.Text = "��ͨ�ÿ���Ϣ(" + t_inOTHTTraveller.Length.ToString() + "��)";
                    inJINTabPage.Text = "���ÿ���Ϣ(" + t_inJINTraveller.Length.ToString() + "��)";

                    //�����ϴβ�ѯʱ��
                    lastSearchDateTime = thisSearchDateTime;
                    lastInChangeLegsBM = inChangeLegsBM;
                    lastSelectedRow = m_iOldSelectedRow;
                    //}
                    #endregion
                }
            }
            #endregion

        }

        #region ����TabControl��ǩ
        private void intabControl_PSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(intabControl_PSR);
        }
        #endregion

        private void outtabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(outtabControl);

            #region ���ۺ��������鴦��
            if (outtabControl.SelectedTab.Name.ToUpper() == "outTaskBooktabPage".ToUpper())
            {
                //��ǩ��������
                outtabControl_Taskbook_SelectedIndexChanged(sender, e);
                try
                {
                    ReturnValueSF returnValueSF = new ReturnValueSF();
                    TaskBookServiceBF taskBookServiceBF = new TaskBookServiceBF();
                    string strCurrentTime = ""; //��ǰ��ѯʱ��
                    string strLastTime = ""; //�ϴβ�ѯʱ��

                    DataTable dataTable_2 = null;


                    #region �ж�ǰ�����β�ѯͬһ�����ʱ���������Ʒ�������������Ƶ��
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
                                    //MessageBox.Show("�˺����ϴβ�ѯʱ�䣺" +
                                    //    dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString() +
                                    //    "�����30���Ż�������ȡ���ݣ�", "�������ѯ");

                                    this.m_statusBar.Panels[2].Text = "��" + outChangeLegsBM.FLTID.Replace(" ", "") + "�������ϴβ�ѯʱ�䣺" +
                                        dataRows_dataTableTaskBooks_1[0]["cnvcOperateTime"].ToString().Trim() + "�����30���Ż�������ȡ���������ݣ�";

                                    return;
                                }
                            }

                        }
                    }
                    #endregion �ж�ǰ�����β�ѯͬһ�����ʱ���������Ʒ�������������Ƶ��


                    if (outChangeLegsBM != null)
                    {
                        dataTable_2 = taskBookServiceBF.GetVoyageReportDataBySingleFlight_Out(
                            outChangeLegsBM.FlightDate,  //outChangeLegsBM.DATOP, -- modified by LinYong in 20150330
                            outChangeLegsBM.FlightNo.Replace(" ", ""),
                            outChangeLegsBM.LONG_REG,
                            (outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN),
                            outChangeLegsBM.STD.Substring(0, 16),
                            outChangeLegsBM.STA.Substring(0, 16)).Dt;

                        strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //��¼��ǰ����ʱ��

                    }

                    //dataGridView2.DataSource = dataTable_2;

                    #region ��������Դ���ֶ������Ϣ �� DataGridView
                    //�����
                    dataGridView2.Columns.Clear();
                    foreach (DataColumn dataColumndataTable_2 in dataTable_2.Columns)
                    {
                        dataGridView2.Columns.Add(dataColumndataTable_2.ColumnName, dataColumndataTable_2.Caption);
                    }
                    //�����
                    dataGridView2.Rows.Clear();
                    foreach (DataRow dataRowdataTable_2 in dataTable_2.Rows)
                    {
                        //
                        dataRowdataTable_2["cnvcExecFlightInfo"] = dataRowdataTable_2["cnvcExecFlightInfo"].ToString().Replace("�� ", ("��" + Environment.NewLine));
                        //
                        dataGridView2.Rows.Add(dataRowdataTable_2.ItemArray);
                    }


                    #endregion ��������Դ���ֶ������Ϣ �� DataGridView


                    #region �������
                    //��Ԫ����
                    if (chkoutTaskbookConfiguration_NewLine.Checked)
                        dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    else
                        dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                    //�����и�
                    foreach (DataGridViewRow dataGridViewRowdataGridView2 in dataGridView2.Rows)
                    {
                        if (dataGridViewRowdataGridView2.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
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
                                dataGridViewColumn.HeaderText = "���";
                                dataGridViewColumn.Width = 55;
                                //dataGridViewColumn.Visible = false;
                                break;
                            case "CNVCPOSITION":
                                dataGridViewColumn.HeaderText = "λ�� ";
                                dataGridViewColumn.Width = 140;
                                break;
                            case "CNVCNAME":
                                dataGridViewColumn.HeaderText = "���� ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCLEVEL":
                                dataGridViewColumn.HeaderText = "���� ";
                                dataGridViewColumn.Width = 70;
                                break;
                            case "CNVCSID":
                                dataGridViewColumn.HeaderText = "����֤�� ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCDEPARRSTN":
                                dataGridViewColumn.HeaderText = "��˺��� ";
                                dataGridViewColumn.Width = 120;
                                break;
                            case "CNVCMOBILE":
                                dataGridViewColumn.HeaderText = "��ϵ�绰 ";
                                dataGridViewColumn.Width = 80;
                                break;
                            case "CNVCEXECFLIGHTINFO":
                                dataGridViewColumn.HeaderText = "ִ�к��ࣨ�϶Σ� ";
                                if (chkoutTaskbookConfiguration_NewLine.Checked)
                                    dataGridViewColumn.Width = 500;
                                else
                                    dataGridViewColumn.Width = 720;

                                dataGridViewColumn.DisplayIndex = 4;
                                break;
                            case "CNVCMEMO":
                                dataGridViewColumn.HeaderText = "��ע ";
                                break;
                            case "CNVCCOMPARERESULT":
                                dataGridViewColumn.HeaderText = "�ȽϽ�� ";
                                dataGridViewColumn.Width = 60;
                                break;
                        }
                    }

                    #endregion �������

                    #region ���� �ϼ���
                    dataGridView2.Rows[0].Frozen = true;
                    #endregion ���� �ϼ���

                    #region �Ƚ� �� ���� ��ǰ������
                    //���� ��ǰ���������
                    if (_dataTableTaskBooks == null)    //null����ʾ��δ���������򴴽�����
                    {
                        #region ���ɱ��
                        try
                        {
                            _dataTableTaskBooks = new DataTable();
                            DataColumn dataColumn = null;

                            dataColumn = new DataColumn("cnvcDATOP", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��������";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcFlightNo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "�����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLONG_REG", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "�ɻ���";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSTD", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "���ʱ��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcROUTE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cniCrewInfoId", Type.GetType("System.Int32"));
                            dataColumn.Caption = "���";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcPosition", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "λ��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcName", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcLevel", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcSID", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����֤��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcDepArrStn", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��˺���";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMOBILE", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��ϵ�绰";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcExecFlightInfo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "ִ�к��ࣨ�϶λ��¶Σ�";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "��ע";
                            _dataTableTaskBooks.Columns.Add(dataColumn);

                            dataColumn = new DataColumn("cnvcOperateTime", Type.GetType("System.String"));
                            dataColumn.DefaultValue = "";
                            dataColumn.Caption = "����ʱ��";
                            _dataTableTaskBooks.Columns.Add(dataColumn);
                        }
                        catch (Exception ex)
                        {
                            _dataTableTaskBooks = null;
                            throw new Exception(ex.Message);
                        }
                        #endregion ���ɱ��
                    }

                    #region ������ۺ���
                    #region �ְ汾��ԭ�汾���бȽ�
                    if ((outChangeLegsBM != null) && (dataTable_2 != null) && (dataTable_2.Columns.Count > 0))
                    {
                        DataTable dataTableCompareOut = dataTable_2.Copy();  //�ְ汾����
                        dataTableCompareOut.Columns.Add("cnvcCompareResult", Type.GetType("System.String"));  //���� �ȽϽ����

                        foreach (DataRow dataRow_dataTableTaskBooks in _dataTableTaskBooks.Select("(cnvcDATOP = '" + outChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + outChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + outChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + outChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN + "') "))  //����ԭ�汾
                        {
                            //�ϴβ�ѯʱ��
                            strLastTime = dataRow_dataTableTaskBooks["cnvcOperateTime"].ToString();

                            if (dataTable_2.Select("(cnvcSID = '" + dataRow_dataTableTaskBooks["cnvcSID"].ToString() + "') ").Length == 0)  //�ж� �ְ汾 �� �Ƿ� ���� ԭ�汾����
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
                                dataRowdataTableCompareOut["cnvcCompareResult"] = "ɾ��";

                                dataTableCompareOut.Rows.Add(dataRowdataTableCompareOut);
                            }
                        }

                        foreach (DataRow dataRowdataTableCompareOut in dataTableCompareOut.Rows)  //�����ְ汾
                        {
                            if (_dataTableTaskBooks.Select("(cnvcDATOP = '" + outChangeLegsBM.DATOP + "') and " +
                            "(cnvcFlightNo = '" + outChangeLegsBM.FlightNo + "') and " +
                            "(cnvcLONG_REG = '" + outChangeLegsBM.LONG_REG + "') and " +
                            "(cnvcSTD = '" + outChangeLegsBM.STD + "') and " +
                            "(cnvcROUTE = '" + outChangeLegsBM.DEPSTN + "-" + outChangeLegsBM.ARRSTN + "') and " +
                            "(cnvcSID = '" + dataRowdataTableCompareOut["cnvcSID"].ToString() + "') ").Length > 0)  //�ж� ԭ�汾 �� �Ƿ� ���� �ְ汾����
                            {
                                dataRowdataTableCompareOut["cnvcCompareResult"] = "����";
                            }
                            else
                            {
                                if (strLastTime != "")
                                    dataRowdataTableCompareOut["cnvcCompareResult"] = "����";
                                else
                                    dataRowdataTableCompareOut["cnvcCompareResult"] = "";

                            }
                        }

                        //���ĺϼ�����Ϣ
                        if (dataTableCompareOut.Select("cnvcPosition = '�ϼ�'").Length == 1)
                        {
                            dataTableCompareOut.Select("cnvcPosition = '�ϼ�'")[0]["cnvcExecFlightInfo"] = "�ϴβ�ѯ��" + strLastTime + " -- ���β�ѯ��" + strCurrentTime;
                            dataTableCompareOut.Select("cnvcPosition = '�ϼ�'")[0]["cnvcCompareResult"] = "";
                        }

                        //
                        //dataGridView4.DataSource = dataTableCompareOut;


                        #region ��������Դ���ֶ������Ϣ �� DataGridView
                        //�����
                        dataGridView4.Columns.Clear();
                        foreach (DataColumn dataColumndataTableCompareOut in dataTableCompareOut.Columns)
                        {
                            dataGridView4.Columns.Add(dataColumndataTableCompareOut.ColumnName, dataColumndataTableCompareOut.Caption);
                        }
                        //�����
                        dataGridView4.Rows.Clear();
                        foreach (DataRow dataRowdataTableCompareOut in dataTableCompareOut.Rows)
                        {
                            dataGridView4.Rows.Add(dataRowdataTableCompareOut.ItemArray);
                        }
                        #endregion ��������Դ���ֶ������Ϣ �� DataGridView


                        #region ���� �ϼ���
                        dataGridView4.Rows[0].Frozen = true;
                        #endregion ���� �ϼ���

                        #region �������
                        //��Ԫ����
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        else
                            dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                        //�����и�
                        foreach (DataGridViewRow dataGridViewRowdataGridView4 in dataGridView4.Rows)
                        {
                            if (dataGridViewRowdataGridView4.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
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
                                    dataGridViewColumn.HeaderText = "���";
                                    dataGridViewColumn.Width = 45;
                                    //dataGridViewColumn.Visible = false;
                                    break;
                                case "CNVCPOSITION":
                                    dataGridViewColumn.HeaderText = "λ�� ";
                                    dataGridViewColumn.Width = 140;
                                    break;
                                case "CNVCNAME":
                                    dataGridViewColumn.HeaderText = "���� ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCLEVEL":
                                    dataGridViewColumn.HeaderText = "���� ";
                                    dataGridViewColumn.Width = 70;
                                    break;
                                case "CNVCSID":
                                    dataGridViewColumn.HeaderText = "����֤�� ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCDEPARRSTN":
                                    dataGridViewColumn.HeaderText = "��˺��� ";
                                    dataGridViewColumn.Width = 120;
                                    break;
                                case "CNVCMOBILE":
                                    dataGridViewColumn.HeaderText = "��ϵ�绰 ";
                                    dataGridViewColumn.Width = 80;
                                    break;
                                case "CNVCEXECFLIGHTINFO":
                                    dataGridViewColumn.HeaderText = "ִ�к��ࣨ�϶Σ� ";
                                    if (chkoutTaskbookConfiguration_NewLine.Checked)
                                        dataGridViewColumn.Width = 500;
                                    else
                                        dataGridViewColumn.Width = 720;

                                    dataGridViewColumn.DisplayIndex = 4;
                                    break;
                                case "CNVCMEMO":
                                    dataGridViewColumn.HeaderText = "��ע ";
                                    break;
                                case "CNVCCOMPARERESULT":
                                    dataGridViewColumn.HeaderText = "�ȽϽ�� ";
                                    dataGridViewColumn.Width = 60;
                                    dataGridViewColumn.DisplayIndex = 1;
                                    break;
                            }
                        }

                        #endregion �������
                    }
                    else
                    {
                        dataGridView4.DataSource = null;
                    }
                    #endregion �ְ汾��ԭ�汾���бȽ�

                    #region ɾ�� ���� ԭ����
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
                    #endregion ɾ�� ���� ԭ����

                    #region ���� ���� ��ǰ����������
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
                    #endregion ���� ���� ��ǰ����������

                    #endregion ������ۺ���


                    #endregion �Ƚ� �� ���� ��ǰ������


                    #region �����飨��������
                    if ((outChangeLegsBM != null) && (dataTable_2 != null) && (dataTable_2.Columns.Count > 0))
                    {
                        DataRow[] dataRowsdataTable_2 = dataTable_2.Select("cnvcPosition = '�ϼ�'");
                        if (dataRowsdataTable_2.Length == 1)
                        {
                            int intPos_1 = -1, intPos_2 = -1;
                            string strFltReportId = "";


                            intPos_1 = dataRowsdataTable_2[0]["cnvcExecFlightInfo"].ToString().IndexOf("��������ID��");
                            intPos_2 = dataRowsdataTable_2[0]["cnvcExecFlightInfo"].ToString().IndexOf("��");
                            if (intPos_2 > (intPos_1 + "��������ID��".Length))
                            {
                                strFltReportId = dataRowsdataTable_2[0]["cnvcExecFlightInfo"].ToString().Substring((intPos_1 + "��������ID��".Length), (intPos_2 - intPos_1 - "��������ID��".Length));
                                string strWebBrowser = "http://opcnet1.hnair.net/OpcNetWebUI/DispatchWebUI/Dispatch/CrewFltTaskNew.aspx?cniId=" +
                                    strFltReportId;
                                webBrowser2.Navigate(strWebBrowser);
                            }
                        }
                    }
                    #endregion �����飨��������



                }
                catch (Exception ex)
                {
                    string errMessage = ex.Message;
                }

            }
            #endregion

            if (outtabControl.SelectedTab.Name.ToUpper() == "outPSRtabPage".ToUpper())
            {
                //Ȩ���ж� -- modified by LinYong in 20160316
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

                    MessageBox.Show("�Բ�����û�в鿴�ÿ���Ϣ��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                //��ǩ��������ʾ
                outTabControl_PSR_SelectedIndexChanged(sender, e);
                #region ���ۺ����ÿ�����չʾ
                //if (lastInChangeLegsBM != null && lastOutChangeLegsBM != null && (lastOutChangeLegsBM.DATOP == outChangeLegsBM.DATOP && lastOutChangeLegsBM.FLTID == outChangeLegsBM.FLTID && lastOutChangeLegsBM.LONG_REG == outChangeLegsBM.LONG_REG)
                //    && (DateTime.Now < lastSearchDateTime.AddSeconds(30)))
                if (lastInChangeLegsBM != null && lastOutChangeLegsBM != null && (lastSelectedRow == m_iOldSelectedRow)
                    && (DateTime.Now < lastSearchDateTime.AddSeconds(30)))
                {
                    //MessageBox.Show("�˺����ϴβ�ѯʱ�䣺" +
                    //                    lastSearchDateTime.ToString("yyyy-MM-dd HH:mm:ss") +
                    //                    "�����30���Ż�������ȡ���ݣ�", "�����ÿ����ݲ�ѯ");
                    this.m_statusBar.Panels[2].Text = "��" + lastOutChangeLegsBM.FLTID.Replace(" ", "") + "�������ϴβ�ѯʱ�䣺" + lastSearchDateTime.ToString("HH:mm:ss") + "�����30���Ż�������ȡ�ÿ����ݣ�";
                }
                else
                {
                    DateTime thisSearchDateTime = DateTime.Now;
                    DataSet outDataSet = new DataSet();
                    DataSet outzjDataSet = new DataSet();
                    if (outChangeLegsBM.FLTID.Equals("HU 0000") || outChangeLegsBM == null)
                    {
                        //to do;Ҫ��ҳ�����ֵ��ʼ��
                        return;
                    }
                    //�����ÿ�ֵ��ͳ��
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

                    //���ۺ����ÿ����ݴ���
                    outDataSet = traveller.getTravellerDataSet(outChangeLegsBM.FLTID.Replace(" ", ""), outChangeLegsBM.DATOP, outChangeLegsBM.DEPSTN, outChangeLegsBM.ARRSTN);
                    if (outDataSet == null || outDataSet.Tables.Count == 0)
                    {
                        return;
                    }
                    t_outALLTraveller = outDataSet.Tables[0];
                    t_outVipTraveller = t_outALLTraveller.Select("PSR_M6_MARK <> 0 or PSR_SUSPECTEDVIP_MARK <> 0");//Ҫ����Ϣ
                    t_outJINTraveller = t_outALLTraveller.Select("PSR_FFP_MARK <> 0");//���ÿ�
                    t_outMealTraveller = t_outALLTraveller.Select("PSR_MEAL_MARK = 1");//�ز��ÿ�
                    t_outOTHTTraveller = t_outALLTraveller.Select("PSR_M6_MARK = 0 and PSR_SUSPECTEDVIP_MARK = 0 and PSR_FFP_MARK = 0 and PSR_MEAL_MARK = 0 and PSR_SS_MARK = 0 and PSR_INFANT_MARK = 0 and PSRD_IN_STN is NULL and PSRD_OUT_STN is NULL");
                    t_outSsTraveller = t_outALLTraveller.Select("PSR_SS_MARK <> 0 or PSR_INFANT_MARK = 1 ");//�ط��ÿ�
                    t_outTSTTraveller = t_outALLTraveller.Select("PSRD_IN_STN is not NULL or PSRD_OUT_STN is not NULL");//��ת�ÿ�

                    showListViewData(outVIPListView, t_outVipTraveller);//Ҫ����Ϣ
                    showListViewData(outSSListView, t_outSsTraveller);//�ط�
                    showListViewData(outMealListView, t_outMealTraveller);//�ز�
                    showListViewData(outTSTListView, t_outTSTTraveller);//��ת
                    showListViewData(outclistView, t_outJINTraveller);//���ÿ�
                    showListViewData(outplistView, t_outOTHTTraveller);//��ͨ�ÿ�

                    outVIPTabPage.Text = "Ҫ����Ϣ(" + t_outVipTraveller.Length.ToString() + "��)";
                    outSSTabPage.Text = "�ط��ÿ���Ϣ(" + t_outSsTraveller.Length.ToString() + "��)";
                    outMealTabPage.Text = "�ز��ÿ���Ϣ(" + t_outMealTraveller.Length.ToString() + "��)";
                    outTSTabPage.Text = "��ת�ÿ���Ϣ(" + t_outTSTTraveller.Length.ToString() + "��)";
                    outOTHTabPage.Text = "��ͨ�ÿ���Ϣ(" + t_outOTHTTraveller.Length.ToString() + "��)";
                    outJINTabPage.Text = "���ÿ���Ϣ(" + t_outJINTraveller.Length.ToString() + "��)";

                    //�����ϴβ�ѯʱ��
                    lastSearchDateTime = thisSearchDateTime;
                    lastOutChangeLegsBM = outChangeLegsBM;
                    lastSelectedRow = m_iOldSelectedRow;
                }
                #endregion
            }
        }

        #region ����TabControl��ǩ����
        private void outTabControl_PSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(outTabControl_PSR);
        }
        #endregion

        #region gridView����
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.Value != null))
            {
                DataGridView dataGridView = (sender as DataGridView);  //����ؼ�

                if (dataGridView.Rows[e.RowIndex].Cells["cnvcPosition"].Value.ToString() == "�ϼ�")
                //���� �ϼ��У��ı���Ӧ cell ��ɫ
                {
                    //
                    dataGridView.Rows[e.RowIndex].Cells["cniCrewInfoId"].Value = "";

                    //�ı��ɫ
                    e.CellStyle.BackColor = Color.Gainsboro;
                    e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, e.CellStyle.Font.Size, FontStyle.Bold);
                }
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.Value != null))
            {
                DataGridView dataGridView = (sender as DataGridView);  //����ؼ�

                if (dataGridView.Rows[e.RowIndex].Cells["cnvcPosition"].Value.ToString() == "�ϼ�")
                //���� �ϼ��У��ı���Ӧ cell ��ɫ
                {
                    //
                    dataGridView.Rows[e.RowIndex].Cells["cniCrewInfoId"].Value = "";

                    //�ı��ɫ
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


            if ((dataGridView.Rows[e.RowIndex1].Cells["cnvcPosition"].Value.ToString().Trim() == "�ϼ�") ||
                (dataGridView.Rows[e.RowIndex2].Cells["cnvcPosition"].Value.ToString().Trim() == "�ϼ�"))
            {
                e.SortResult = 0;
                e.Handled = true;
            }
        }

        private void dataGridView2_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            DataGridView dataGridView = (sender as DataGridView);


            if ((dataGridView.Rows[e.RowIndex1].Cells["cnvcPosition"].Value.ToString().Trim() == "�ϼ�") ||
                (dataGridView.Rows[e.RowIndex2].Cells["cnvcPosition"].Value.ToString().Trim() == "�ϼ�"))
            {
                e.SortResult = 0;
                e.Handled = true;
            }
        }
        #endregion


        #region ��ǩ�����л�
        private void outtabControl_Taskbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(outtabControl_Taskbook);
        }

        private void intabControl_Taskbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDataTabControlTile(intabControl_Taskbook);
        }
        #endregion

        //����tabControl��ǩ����
        private void setDataTabControlTile(TabControl tabControl)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {

                if (tabControl.TabPages[i] == tabControl.SelectedTab)
                {
                    if (!tabControl.TabPages[i].Text.Contains("��"))
                    {
                        tabControl.TabPages[i].Text = "��" + tabControl.TabPages[i].Text + "��";
                    }
                }
                else
                {
                    tabControl.TabPages[i].Text = tabControl.TabPages[i].Text.ToString().Replace("��", "");
                    tabControl.TabPages[i].Text = tabControl.TabPages[i].Text.ToString().Replace("��", "");
                }
            }
        }

        private void chkinTaskbookConfiguration_NewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkinTaskbookConfiguration_NewLine.Checked)
            {
                #region dataGridView1
                //������
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView1 in dataGridView1.Rows)
                {
                    if (dataGridViewRowdataGridView1.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView1.Height = 80;
                        else
                            dataGridViewRowdataGridView1.Height = 17;

                    }
                }
                //�п�
                dataGridView1.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView1

                #region dataGridView3
                //������
                dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView3 in dataGridView3.Rows)
                {
                    if (dataGridViewRowdataGridView3.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView3.Height = 80;
                        else
                            dataGridViewRowdataGridView3.Height = 17;

                    }
                }
                //�п�
                dataGridView3.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView3


            }
            else
            {
                #region dataGridView1
                //������
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView1 in dataGridView1.Rows)
                {
                    if (dataGridViewRowdataGridView1.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView1.Height = 80;
                        else
                            dataGridViewRowdataGridView1.Height = 17;

                    }
                }
                //�п�
                dataGridView1.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView1

                #region dataGridView3
                //������
                dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView3 in dataGridView3.Rows)
                {
                    if (dataGridViewRowdataGridView3.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkinTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView3.Height = 80;
                        else
                            dataGridViewRowdataGridView3.Height = 17;

                    }
                }
                //�п�
                dataGridView3.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView3
            }

        }

        private void chkoutTaskbookConfiguration_NewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkoutTaskbookConfiguration_NewLine.Checked)
            {
                #region dataGridView2
                //������
                dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView2 in dataGridView2.Rows)
                {
                    if (dataGridViewRowdataGridView2.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView2.Height = 80;
                        else
                            dataGridViewRowdataGridView2.Height = 17;

                    }
                }
                //�п�
                dataGridView2.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView2

                #region dataGridView4
                //������
                dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView4 in dataGridView4.Rows)
                {
                    if (dataGridViewRowdataGridView4.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView4.Height = 80;
                        else
                            dataGridViewRowdataGridView4.Height = 17;

                    }
                }
                //�п�
                dataGridView4.Columns["cnvcExecFlightInfo"].Width = 500;
                #endregion dataGridView4


            }
            else
            {
                #region dataGridView2
                //������
                dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView2 in dataGridView2.Rows)
                {
                    if (dataGridViewRowdataGridView2.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView2.Height = 80;
                        else
                            dataGridViewRowdataGridView2.Height = 17;

                    }
                }
                //�п�
                dataGridView2.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView2

                #region dataGridView4
                //������
                dataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                //�����и�
                foreach (DataGridViewRow dataGridViewRowdataGridView4 in dataGridView4.Rows)
                {
                    if (dataGridViewRowdataGridView4.Cells["cnvcPosition"].Value.ToString().IndexOf("�ϼ�") < 0)
                    {
                        if (chkoutTaskbookConfiguration_NewLine.Checked)
                            dataGridViewRowdataGridView4.Height = 80;
                        else
                            dataGridViewRowdataGridView4.Height = 17;

                    }
                }
                //�п�
                dataGridView4.Columns["cnvcExecFlightInfo"].Width = 720;
                #endregion dataGridView4
            }
        }



        #region �����¼�����
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
            ownerListView.Sort();//���б�����Զ�������
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


            //��ȡ���һ���Ԫ�������еĽ����ۺ���ʵ��
            //GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)(ilInOutFlights as ArrayList)[e.Row];

            //����һ�λ�õĺ���Ϊ��
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

                ////��ȡ���һ����¼��ָ���û���
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
                //        MessageBox.Show("�ӱ�����ȡ���һ����¼���û���SYS_PEK��ʧ�ܣ������µ�¼��", "��ʾ", MessageBoxButtons.OK);
                //        return;
                //        //Environment.Exit(0);
                //    }
                //}
                #endregion

                #region ���Խ����ÿ���Ϣ
                LinYong.PublicService.PublicService publicService = new LinYong.PublicService.PublicService();
                String encodeUsername = publicService.DESEncrypt("pass4-opm", "q1w2e3r4");// pass1-opm,pass2-opm,pass3-opm,pass4-opm,�����û�������һ��, pass1-opm�������
                String encodePwd = publicService.DESEncrypt("2015-10-22 09:45:00.000", "q1w2e3r4");//����ʱ��UTCʱ��
                String encodeFltNo = publicService.DESEncrypt("HU7181", "q1w2e3r4");
                String encodeDatopChn = publicService.DESEncrypt("2015-10-23", "q1w2e3r4");//�������ڣ�����ʱ�䣩


                DateTime dateTimeNow = Convert.ToDateTime("2015-10-23 08:37:00.000");
                DateTime dateTimeStart = Convert.ToDateTime("1970-01-01 00:00:00.000");
                TimeSpan timeSpan = new TimeSpan();
                timeSpan = dateTimeNow - dateTimeStart;
                long totalMilliseconds = Convert.ToInt64(timeSpan.TotalMilliseconds);
                encodePwd = publicService.DESEncrypt(totalMilliseconds.ToString(), "q1w2e3r4");//����ʱ��UTCʱ��



                //String encodeUsername = CryptUtils.encode("pass4-opm");// pass1-opm,pass2-opm,pass3-opm,pass4-opm,�����û�������һ��, pass1-opm�������

                //String encodePwd = CryptUtils.encode("2015-10-20 15:00:00");//����ʱ��UTCʱ��

                //String encodeFltNo = CryptUtils.encode("HU0496");

                //String encodeDatopChn = CryptUtils.encode("2015-10-20");//�������ڣ�����ʱ�䣩

                //ASCIIEncoding Encode = new ASCIIEncoding();

                //byte[] post = Encode.GetBytes("fltNo=" + encodeFltNo + "&datopChn=" + encodeDatopChn);

                //encodeUsername = "94165272C94C7EB3565320C075ED5018";// pass1-opm,pass2-opm,pass3-opm,pass4-opm,�����û�������һ��, pass1-opm�������
                //encodePwd = "D0EECDD49B10673AC125C4721D9A4540";//����ʱ��UTCʱ��
                //encodeFltNo = "4FAA89A658A18B20";
                //encodeDatopChn = "60741D1FF780F672782BF312592D1869";//�������ڣ�����ʱ�䣩

                byte[] post = Encoding.UTF8.GetBytes("fltNo=" + encodeFltNo + "&datopChn=" + encodeDatopChn);

                String url = "http://" + encodeUsername + ":" + encodePwd + "@10.72.16.131/opm-web/passenger/passengerDetail.do";

                String PostHeaders = "Content-Type: application/x-www-form-urlencoded";

                //webBrowser3.Navigate(url, null, post, PostHeaders);

                #endregion ���Խ����ÿ���Ϣ

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
            if (radioButton1.Checked) //ѡ����������
            {
                //��ȡ����������
                string strFlightMonitor_Use = ConfigurationSettings.AppSettings["dbFlightMonitor_Use"].ToString().Trim();

                if (strFlightMonitor_Use != "")
                {
                    //�����ļ���ʹ������������Ч
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["dbFlightMonitor"].Value = strFlightMonitor_Use;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");//���¼����µ������ļ�

                    //����ʹ�õ�FlightMonitorϵͳ���ݿ�ʹ�õ�������
                    SysMsgBM.FlightMonitorConfig = "dbFlightMonitor_Use";
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) //��ʷ��
            {
                //��ȡ��ʷ������
                string strFlightMonitor_His = ConfigurationSettings.AppSettings["dbFlightMonitor_His"].ToString().Trim();

                if (strFlightMonitor_His != "")
                {
                    //�����ļ���ʹ���ݿ�������Ч
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["dbFlightMonitor"].Value = strFlightMonitor_His;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");//���¼����µ������ļ�

                    //����ʹ�õ�FlightMonitorϵͳ���ݿ�ʹ�õ�������
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            commanderTypeBM.cnvcCommanderType = "�ⳡ";

            fmSelectUsers objfmSelectUsers = new fmSelectUsers(stationBM, commanderTypeBM, "�½�ƽ(chenjianping)�������(chenxuguang)");
            objfmSelectUsers.ShowDialog();
        }

    }


    /// <summary>
    /// ����������
    /// </summary>
    internal class SoundHelpers
    {
        [Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */ //ͬ��
            SND_ASYNC = 0x0001,  /* play asynchronously */ //�첽
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
            col = column; //��ǰ��,0,1,2...,������ListView�ؼ���ColumnClick�¼����� 
        }
        public int Compare(object x, object y)
        {
            int tempInt = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            if (descK) return -tempInt;
            else return tempInt;
        }
    }
}
