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
using System.Threading;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmFlightWatch : Form
    {
        private Color colorFavorate;
        private int m_iLastRecordNo;

        private AccountBM m_accountBM;   //��½�û�ʵ�����
        private DataTable dtDataItemPurview; //��½�û���������Ȩ��
        private DataTable dtStandardIntermissionTime;  //��׼��վʱ���
        private PositionNameBM m_positionNameBM;   //ϯλ����
        private StationBM m_stationBM;            //�û�������վ

        private DataTable dtPositionFlights;  //ĳϯλ�����к��ද̬     
        private DataTable dtConditionFlights; //����������ĳϯλ�ĺ��ද̬
        private DataTable dtInOutFlights;  //�������۸�ʽ��ʾ�ĺ��ද̬�ı��
        private DataSet dsInOutFlights;   //�������۸�ʽ��ʾ�ĺ��ද̬�����ݼ�

        private DataTable dtDataItems;  //�û����ó���������
        private string m_strDataItemSearch;  //�����û����ó����������ѯҪ��˸�ļ�¼
        private DataTable dtPositionAircrafts;  //��ϯλ��ص����к���

        private DataTable dtSplashTag;  //������˸��־

        //��˸ǰ��Ԫ����ɫ
        Color[,] colorArrOldBackGround;

        //��¼ѡ�е��к����Լ�ԭ��ɫ
        private int m_iOldSelectedRow = -1;
        private int m_iOldSelectedColumn = -1;
        private Color m_cOldBackGroudColor, m_cOldForeColor;

        //�Ҽ��˵���ѡ�����
        private int m_iRightButtonRow = 0;

        //������ͷ������
        private int m_iHeadRow = 0;

        //��ѡ��ĳ��ۺ������Ϣ
        private ChangeLegsBM inChangeLegsBM = new ChangeLegsBM();
        private ChangeLegsBM outChangeLegsBM = new ChangeLegsBM();

        //���̶߳�ʱ������������������������ⱻ��������
        private System.Threading.Timer timer;
        private int iRefreshInterval = 20;
        private object oMutexChangeRecords = new object();
        private DataTable m_dtChangeTable = new DataTable();   //�仯�б�

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration); 

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="accountBM"></param>
        public fmFlightWatch(AccountBM accountBM, DataTable dtIntermissionTime, DataTable dataItems, PositionNameBM positionNameBM)
        {
            InitializeComponent();
            this.m_accountBM = accountBM;
            this.dtStandardIntermissionTime = dtIntermissionTime;
            this.m_positionNameBM = positionNameBM;

            //�Զ���ı���ɫ
            colorFavorate = Color.White;
            this.dtDataItems = dataItems;
            foreach (DataRow dataRow in dtDataItems.Rows)
            {
                m_strDataItemSearch += dataRow["cnvcPrimaryCodeField"].ToString() + ">0 OR ";
            }

            if (m_strDataItemSearch != null && m_strDataItemSearch != "")
            {
                m_strDataItemSearch = m_strDataItemSearch.Substring(0, m_strDataItemSearch.Length - 3);
            }
        }

        #region ����
        /// <summary>
        /// ���ݱ���ʾ�ؼ�
        /// </summary>
        public FarPoint.Win.Spread.FpSpread FpFlightInfo
        {
            get { return fpFlightInfo; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public DataTable DataItems
        {
            get { return dtDataItems; }
            set
            {
                dtDataItems = value;
                m_strDataItemSearch = "";
                foreach (DataRow dataRow in dtDataItems.Rows)
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
        /// ϯλ��Ϣ
        /// </summary>
        public PositionNameBM MPositionNameBM
        {
            set { m_positionNameBM = value; }
        }
        #endregion

        #region ���ش���
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmWatch_Load(object sender, EventArgs e)
        {
            fpFlightInfo.Sheets[0].RowHeader.Columns[0].Width = 50;
            //���û�ȡ������ݵļ��
            timerChangeRecord.Interval = m_accountBM.RefreshInterval * 1000;           
            //������ͼ
            SpreadGrid spreadGrid = new SpreadGrid(m_accountBM);

            //�û�������վ
            m_stationBM = new StationBM();
            m_stationBM.ThreeCode = m_accountBM.StationThreeCode;
           
            spreadGrid.SetView(shToday, dtDataItems, 0);

            //����ʱ�䷶Χʵ�����
            string strStartDateTime = DateTime.Now.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
            string strEndDateTime = DateTime.Now.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";

            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = strStartDateTime;
            dateTimeBM.EndDateTime = strEndDateTime;
           
            //��ȡ������Ȩ��
            GetUserDataItemPurview();

            //��ȡĳϯλ�ķɻ���Ϣ
            GetPositionAircrafts(m_positionNameBM);

            //��������
            GetMaxRecordNo();

            //��ȡĳϯλ��ص����к���
            GetFlightsByPosition(dateTimeBM, m_positionNameBM);

            //������Ӧ����ʾ��Ϣ
            ComputeFlightsInfor();

            //�����ۺ�����Schema
            GetDisplaySchema();

            //�󶨿ؼ�
            if (dsInOutFlights != null)
            {
                for (int iLoop = 0; iLoop < dsInOutFlights.Tables.Count; iLoop++)
                {
                    dsInOutFlights.Tables[iLoop].Constraints.Clear();
                }
                dsInOutFlights.Relations.Clear();
                dsInOutFlights.Tables.Clear();		
                dsInOutFlights.Dispose();
            }
            dsInOutFlights = new DataSet();
            //��ʾ���к���
            if (m_accountBM.DisplayAll == 1)
            {
                dtInOutFlights = FillInOutFlights(dtPositionAircrafts);
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtPositionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            }         
            //�������õ���ʾ������ʾ�����ۺ���
            else
            {
                GetShowInfor();
                DataTable dtConditionAircrafs = GetConditionAircrafs();
                dtInOutFlights = FillInOutFlights(dtConditionAircrafs);
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtConditionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            }

            fpFlightInfo.DataSource = dsInOutFlights;
            fpFlightInfo.DataMember = "InOutFlights";

            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[0].Rows.Count, fpFlightInfo.Sheets[0].Columns.Count];

            //��ʼ�������ɫ
            InitialGridColor();

            //���ñ����ɫ
            SetGridColor();

            timerChangeRecord.Enabled = true;
            timerSplash.Enabled = true;

            //���û���ˢ��Ƶ��д�������ļ�
            int iTempInterval = m_accountBM.RefreshInterval * 1000;
            TimerCallback timerDelegate = new TimerCallback(GetChangeDate);
            //timer = new System.Threading.Timer(timerDelegate, null, 0, (iRefreshInterval * 1000));
            timer = new System.Threading.Timer(timerDelegate, null, 0, iTempInterval);
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
            ReturnValueSF rvSF = changeRecordBF.GetMaxRecordNo();

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

        #region ��ȡĳϯλ�����к��ද̬
        /// <summary>
        /// ��ȡĳϯλ�����к��ද̬
        /// </summary>
        private void GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //����ҵ����۲㷽��
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByPosition(dateTimeBM, positionNameBM);

            if (rvSF.Result > 0)
            {
                dtPositionFlights = rvSF.Dt;
        
                //������˸��ʶ��
                dtSplashTag = new DataTable();
                //�����
                for (int iLoop = 0; iLoop < dtPositionFlights.Columns.Count; iLoop++)
                {
                    //�����е���������Ϊstring
                    if (dtPositionFlights.Columns[iLoop].ColumnName == "cncDATOP" || dtPositionFlights.Columns[iLoop].ColumnName == "cnvcFLTID" || dtPositionFlights.Columns[iLoop].ColumnName == "cnvcAC")
                    {
                        dtSplashTag.Columns.Add(dtPositionFlights.Columns[iLoop].ColumnName, System.Type.GetType("System.String"));
                    }
                    //�����е���������Ϊint
                    else
                    {
                        dtSplashTag.Columns.Add(dtPositionFlights.Columns[iLoop].ColumnName, System.Type.GetType("System.Int32"));
                    }
                }

                //��������
                DataColumn[] pk = new DataColumn[4];
                pk[0] = dtSplashTag.Columns["cncDATOP"];
                pk[1] = dtSplashTag.Columns["cnvcFLTID"];
                pk[2] = dtSplashTag.Columns["cniLEGNO"];
                pk[3] = dtSplashTag.Columns["cnvcAC"];
                dtSplashTag.PrimaryKey = pk;

                //��ռ�¼
                dtSplashTag.Rows.Clear();
                //�����¼
                foreach (DataRow dataRow in dtPositionFlights.Rows)
                {
                    DataRow dataRowSplash = dtSplashTag.NewRow();
                    //��������ֵ
                    dataRowSplash["cncDATOP"] = dataRow["cncDATOP"].ToString();
                    dataRowSplash["cnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                    dataRowSplash["cniLEGNO"] = dataRow["cniLEGNO"].ToString();
                    dataRowSplash["cnvcAC"] = dataRow["cnvcAC"].ToString();

                    dtSplashTag.Rows.Add(dataRowSplash);
                }
            }
            else
            {
                dtPositionFlights = new DataTable();
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ��ȡ��½�û���������Ȩ��
        /// <summary>
        /// ��ȡ��½�û���������Ȩ�ޣ�Ȩ�ޡ���ʾ��
        /// </summary>
        public void GetUserDataItemPurview()
        {
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();

            ReturnValueSF rvSF = dataItemPurviewBF.GetDataItemPurviewByUserId(m_accountBM);

            if (rvSF.Result > 0)
            {
                dtDataItemPurview = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region �жϸ�����¼�Ƿ���ʾ
        /// <summary>
        /// �жϸ�����¼�Ƿ���ʾ
        /// </summary>
        private void GetShowInfor()
        {
            foreach (DataRow dataRow in dtPositionAircrafts.Rows)
            {
                DataRow[] drFlightsByAircraft = dtPositionFlights.Select("cnvcLONG_REG = '" + dataRow["cnvcLONG_REG"].ToString() + "'", "cncETD");

                if (drFlightsByAircraft.Length > 0)
                {
                    //�ҵ���һ���ƻ�������ĺ���
                    bool blnFind = false;
                    int iRowIndx = 0;
                    for (int iLoop = 0; iLoop < drFlightsByAircraft.Length; iLoop++)
                    {
                        if (drFlightsByAircraft[iLoop]["cncSTATUS"].ToString() == "SCH" || drFlightsByAircraft[iLoop]["cncSTATUS"].ToString() == "DEL")
                        {
                            iRowIndx = iLoop;
                            blnFind = true;
                            break;
                        }
                    }

                    if (blnFind == false)
                    {
                        drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cniShow"] = 1;
                    }
                    else
                    {
                        drFlightsByAircraft[iRowIndx]["cniShow"] = 1;
                        if (iRowIndx != 0)
                        {

                            drFlightsByAircraft[iRowIndx - 1]["cniShow"] = 1;
                        }
                    }
                }
            }
        }
        #endregion

        #region �����վʱ�䡢���ز�ʱ��
        /// <summary>
        /// �����վʱ�䡢���ز�ʱ�䡢
        /// </summary>
        private void ComputeFlightsInfor()
        {
            int iRowIndex = 0;           
            foreach (DataRow dataRow in dtPositionFlights.Rows)
            {             

                //���㿪�ز�ʱ��
                if (dataRow["cncOpenCabinTime"].ToString().Trim() != "" && dataRow["cncClosePaxCabinTime"].ToString().Trim() != "")
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
                if (dataRow["cncSTATUS"].ToString().Trim() != "ATA")
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
                if (dataRow["cncSTATUS"].ToString().Trim() != "DEP" || dataRow["cncSTATUS"].ToString().Trim() != "ATA")
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
                string strDEPAirportPaxType = dataRow["cniDEPAirportPaxType"].ToString().Trim();
                if (strDEPAirportPaxType == "")
                {
                    strDEPAirportPaxType = "1";
                }
                DataRow[] drStandardIntermission = dtStandardIntermissionTime.Select("cncACTYPE = '" + dataRow["cncACTYP"].ToString() + "' AND cniAirportPaxType = " + strDEPAirportPaxType);

                TimeSpan tsIntermissionTime = new TimeSpan(0, 0 , 0);
                //�ж��Ƿ��վ����
                if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtPositionFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    //�����վʱ��
                    tsIntermissionTime = DateTime.Parse(dataRow["cncETD"].ToString()).Subtract(DateTime.Parse(dtPositionFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));

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
                if (iRowIndex == 0 || dataRow["cnvcLONG_REG"].ToString() != dtPositionFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    if (dataRow["cnvcDELAY1"].ToString() != "")
                    {
                        dataRow["cniDischargingDelayTime"] = dataRow["cniDUR1"].ToString();
                    }
                }
                //��վ����
                else if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtPositionFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    if (drStandardIntermission.Length > 0)
                    {
                        //�����վʱ��
                        tsIntermissionTime = DateTime.Parse(dataRow["cncSTD"].ToString()).Subtract(DateTime.Parse(dtPositionFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
                        if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes >= Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
                        {
                            if (dataRow["cnvcDELAY1"].ToString() != "")
                            {
                                dataRow["cniDischargingDelayTime"] = dataRow["cniDUR1"].ToString();
                            }
                        }
                        else
                        {
                            tsIntermissionTime = DateTime.Parse(dataRow["cncTOFF"].ToString()).Subtract(DateTime.Parse(dtPositionFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
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
                    if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() != dtPositionFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() && tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > 180) //��վ����
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
                if (DateTime.Now.AddMinutes(m_accountBM.GuaranteeMinutes) > DateTime.Parse(dataRow["cncETD"].ToString()))
                {
                    dataRow["cniStartGuarantee"] = 1;
                }

                //�ж��Ƿ�ʼ�ǻ�
                if (DateTime.Now.AddMinutes(m_accountBM.BoardingMinutes) > DateTime.Parse(dataRow["cncETD"].ToString()))
                {
                    dataRow["cniBoarding"] = 1;
                }

                //�ж��Ƿ����λ
                //if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtPositionFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                //{
                if (iRowIndex != 0 && dtPositionFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() != "")
                {
                    if (DateTime.Parse(dtPositionFlights.Rows[iRowIndex - 1]["cncETA"].ToString()).AddMinutes(-m_accountBM.MCCReadyMinutes) < DateTime.Now)
                    {
                        dtPositionFlights.Rows[iRowIndex - 1]["cniMCCReady"] = 1;
                    }
                }

                //�ж��Ƿ�������
                if (iRowIndex != 0 && dataRow["cnvcLONG_REG"].ToString() == dtPositionFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
                {
                    if (DateTime.Now > DateTime.Parse(dtPositionFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()).AddMinutes(m_accountBM.MCCReleasMinutes))
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
        private void GetDisplaySchema()
        {
            dtInOutFlights = new DataTable();

            //�����������ֶ�
            dtInOutFlights.Columns.Add("IncncDATOP");
            dtInOutFlights.Columns.Add("IncnvcFLTID");
            dtInOutFlights.Columns.Add("IncniLEGNO");
            dtInOutFlights.Columns.Add("IncnvcAC");
            dtInOutFlights.Columns.Add("IncncAllTDWN");
            dtInOutFlights.Columns.Add("OutcncDATOP");
            dtInOutFlights.Columns.Add("OutcnvcFLTID");
            dtInOutFlights.Columns.Add("OutcniLEGNO");
            dtInOutFlights.Columns.Add("OutcnvcAC");
            dtInOutFlights.Columns.Add("OutcncAllTOFF");

            foreach (DataRow dataRow in dtDataItems.Rows)
            {
                dtInOutFlights.Columns.Add(dataRow["cnvcDataItemID"].ToString());
            }
        }
        #endregion

        #region ����ϯλ��Ż�ȡ���ڸ�ϯλ�����зɻ�����Ϣ
        /// <summary>
        /// ����ϯλ��Ż�ȡ���ڸ�ϯλ�����зɻ�����Ϣ
        /// </summary>
        /// <param name="positionInforBM"></param>
        private void GetPositionAircrafts(PositionNameBM positionNameBM)
        {
             PositionInforBF positionInforBF = new PositionInforBF();
             ReturnValueSF rvSF = new ReturnValueSF();

             rvSF = positionInforBF.GetInforByPositionId(positionNameBM);

             if (rvSF.Result > 0)
             {
                 dtPositionAircrafts = rvSF.Dt;
             }
             else
             {
                 dtPositionAircrafts = new DataTable();
                 MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
         }
        #endregion

         #region �������ۺ���״̬
         /// <summary>
        /// �������ۺ���״̬
        /// </summary>
        private DataTable FillInOutFlights(DataTable dtAircrafts)
        {
            DataTable dtAllInOutFlights = dtInOutFlights.Clone();
            dtConditionFlights = dtPositionFlights.Clone();

            //��ѯÿ�ܷɻ�ִ�еĺ���
            foreach (DataRow dataRow in dtAircrafts.Rows)
            {
                //��ѯϯλ���к���
                DataRow[] drFlightsByAircraft = dtPositionFlights.Select("cnvcLONG_REG = '" + dataRow["cnvcLONG_REG"].ToString() + "'", "cncETD");
                if (drFlightsByAircraft.Length > 0)
                {
                    for (int iLoop = 0; iLoop < drFlightsByAircraft.Length; iLoop++)
                    {
                        dtConditionFlights.ImportRow(drFlightsByAircraft[iLoop]);
                    }
                    //�ҵ���һ���ƻ�������ĺ���
                    bool blnFind = false;
                    int iRowIndx = 0;
                    for (int iLoop = 0; iLoop < drFlightsByAircraft.Length; iLoop++)
                    {
                        dtConditionFlights.ImportRow(drFlightsByAircraft[iLoop]);

                        if (drFlightsByAircraft[iLoop]["cncSTATUS"].ToString() == "SCH" || drFlightsByAircraft[iLoop]["cncSTATUS"].ToString() == "DEL")
                        {
                            iRowIndx = iLoop;
                            blnFind = true;
                            break;
                        }
                    }

                    //�����ҵ�������������ۺ���
                    //û���ҵ��ƻ��ĺ���
                    if (blnFind == false)
                    {
                        DataRow drInFlight = dtAllInOutFlights.NewRow();
                        drInFlight["IncncDATOP"] = drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncDATOP"].ToString();
                        drInFlight["IncnvcFLTID"] = drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cnvcFLTID"].ToString();
                        drInFlight["IncniLEGNO"] = drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cniLEGNO"].ToString();
                        drInFlight["IncnvcAC"] = drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cnvcAC"].ToString();
                        drInFlight["IncncAllTDWN"] = drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncTDWN"].ToString();

                        drInFlight["OutcncDATOP"] = "1900-01-01";
                        drInFlight["OutcnvcFLTID"] = "HU 0000";
                        drInFlight["OutcniLEGNO"] = 100;
                        drInFlight["OutcnvcAC"] = "HH";
                        //���Ƚ����ۻ�������Ϊ���ۻ���
                        if (dtAllInOutFlights.Columns.Contains("OutcnvcLONG_REG"))
                        {
                            drInFlight["OutcnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                        }

                        foreach (DataRow dataRowItems in dtDataItems.Rows)
                        {
                            string strFieldValue = drFlightsByAircraft[drFlightsByAircraft.Length - 1][dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();

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

                            //�ƻ�����ʱ��
                            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncSTA")
                            {
                                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                            }

                            //����ʱ��
                            if(dataRowItems["cnvcDataItemID"].ToString() == "IncncETA")
                            {
                                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                //if (drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cnvcDELAY1"].ToString().Trim() != "")
                                //{
                                //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                //}
                                //else
                                //{
                                //    strFieldValue = "";
                                //}
                            }

                            //���ʱ��
                            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncTDWN")
                            {
                                if (drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncSTATUS"].ToString().Trim() == "DEP" || drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncSTATUS"].ToString().Trim() == "ARR" || drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncSTATUS"].ToString().Trim() == "ATA")
                                {
                                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                }
                                else
                                {
                                    strFieldValue = "";
                                }
                            }

                            //��λʱ��
                            if (dataRowItems["cnvcDataItemID"].ToString() == "IncncATA")
                            {
                                if (drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncSTATUS"].ToString().Trim() == "ATA")
                                {
                                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                }
                                else
                                {
                                    strFieldValue = "";
                                }
                            }

                            if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                            {
                                drInFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                            }
                        }

                        dtAllInOutFlights.Rows.Add(drInFlight);
                    }
                    else
                    {
                        DataRow drInOutFlight = dtAllInOutFlights.NewRow();
                        //�ҵ��ĺ��಻Ϊʼ������
                        if (iRowIndx > 0)
                        {
                            drInOutFlight["IncncDATOP"] = drFlightsByAircraft[iRowIndx - 1]["cncDATOP"].ToString();
                            drInOutFlight["IncnvcFLTID"] = drFlightsByAircraft[iRowIndx - 1]["cnvcFLTID"].ToString();
                            drInOutFlight["IncniLEGNO"] = drFlightsByAircraft[iRowIndx - 1]["cniLEGNO"].ToString();
                            drInOutFlight["IncnvcAC"] = drFlightsByAircraft[iRowIndx - 1]["cnvcAC"].ToString();
                            drInOutFlight["IncncAllTDWN"] = drFlightsByAircraft[iRowIndx - 1]["cncTDWN"].ToString();

                            foreach (DataRow dataRowItems in dtDataItems.Rows)
                            {
                                string strFieldValue = drFlightsByAircraft[iRowIndx - 1][dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();
                                if (dataRowItems["cnvcDataItemID"].ToString() == "IncncDEPAirportCNAME")
                                {
                                    int iSplitIndex = strFieldValue.IndexOf("/");
                                    if (iSplitIndex > 0)
                                    {
                                        strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                                    }
                                }

                                if (dataRowItems["cnvcDataItemID"].ToString() == "IncncARRAirportCNAME")
                                {
                                    int iSplitIndex = strFieldValue.IndexOf("/");
                                    if (iSplitIndex > 0)
                                    {
                                        strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                                    }
                                }

                                //�ƻ�����ʱ��
                                if (dataRowItems["cnvcDataItemID"].ToString() == "IncncSTA")
                                {
                                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                }

                                //����ʱ��
                                if (dataRowItems["cnvcDataItemID"].ToString() == "IncncETA")
                                {
                                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                    //if (drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cnvcDELAY1"].ToString().Trim() != "")
                                    //{
                                    //    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                    //}
                                    //else
                                    //{
                                    //    strFieldValue = "";
                                    //}
                                }

                                //���ʱ��
                                if (dataRowItems["cnvcDataItemID"].ToString() == "IncncTDWN")
                                {
                                    if (drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncSTATUS"].ToString().Trim() == "DEP" || drFlightsByAircraft[iRowIndx - 1]["cncSTATUS"].ToString().Trim() == "ARR" || drFlightsByAircraft[iRowIndx - 1]["cncSTATUS"].ToString().Trim() == "ATA")
                                    {
                                        strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                    }
                                    else
                                    {
                                        strFieldValue = "";
                                    }
                                }

                                //��λʱ��
                                if (dataRowItems["cnvcDataItemID"].ToString() == "IncncATA")
                                {
                                    if (drFlightsByAircraft[drFlightsByAircraft.Length - 1]["cncSTATUS"].ToString().Trim() == "ATA")
                                    {
                                        strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                    }
                                    else
                                    {
                                        strFieldValue = "";
                                    }
                                }

                                //���ۻ�λ
                                if (dataRowItems["cnvcDataItemID"].ToString() == "IncnvcInGATE")
                                {
                                    if (drFlightsByAircraft[iRowIndx]["cnvcOutGate"].ToString().Trim() == "")
                                    {
                                        strFieldValue = drFlightsByAircraft[iRowIndx]["cnvcOutGate"].ToString().Trim();
                                    }
                                }

                                if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                                {
                                    drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                                }
                            }
                        }
                        else  //û�н��ۺ���
                        {                           
                            drInOutFlight["IncncDATOP"] = "1900-01-01";
                            drInOutFlight["IncnvcFLTID"] = "HU 0000";
                            drInOutFlight["IncniLEGNO"] = 100;
                            drInOutFlight["IncnvcAC"] = "HH";
                            //���Ƚ����ۻ�λ����Ϊ���ۻ�λ
                            if (dtInOutFlights.Columns.Contains("IncnvcInGATE"))
                            {
                                drInOutFlight["IncnvcInGATE"] = drFlightsByAircraft[iRowIndx]["cnvcOutGate"].ToString();
                            }

                            //���Ƚ����ۻ�������Ϊ���ۻ���
                            if (dtAllInOutFlights.Columns.Contains("IncnvcLONG_REG"))
                            {
                                drInOutFlight["IncnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                            }
                        }

                        drInOutFlight["OutcncDATOP"] = drFlightsByAircraft[iRowIndx]["cncDATOP"].ToString();
                        drInOutFlight["OutcnvcFLTID"] = drFlightsByAircraft[iRowIndx]["cnvcFLTID"].ToString();
                        drInOutFlight["OutcniLEGNO"] = drFlightsByAircraft[iRowIndx]["cniLEGNO"].ToString();
                        drInOutFlight["OutcnvcAC"] = drFlightsByAircraft[iRowIndx]["cnvcAC"].ToString();
                        drInOutFlight["OutcncAllTOFF"] = drFlightsByAircraft[iRowIndx]["cncTOFF"].ToString();

                        foreach (DataRow dataRowItems in dtDataItems.Rows)
                        {
                            string strFieldValue = drFlightsByAircraft[iRowIndx][dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();

                            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncDEPAirportCNAME")
                            {
                                int iSplitIndex = strFieldValue.IndexOf("/");
                                if (iSplitIndex > 0)
                                {
                                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                                }
                            }

                            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncARRAirportCNAME")
                            {
                                int iSplitIndex = strFieldValue.IndexOf("/");
                                if (iSplitIndex > 0)
                                {
                                    strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                                }
                            }

                            //�ƻ����ʱ��
                            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTD")
                            {
                                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                            }

                            //�������ʱ��
                            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncETD")
                            {
                                strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                //if (drFlightsByAircraft[iRowIndx]["cnvcDELAY1"].ToString().Trim() != "")
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
                                if (drFlightsByAircraft[iRowIndx]["cncSTATUS"].ToString().Trim() == "ATD" || drFlightsByAircraft[iRowIndx]["cncSTATUS"].ToString().Trim() == "DEP" || drFlightsByAircraft[iRowIndx]["cncSTATUS"].ToString().Trim() == "ARR" || drFlightsByAircraft[iRowIndx]["cncSTATUS"].ToString().Trim() == "ATA")
                                {
                                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                }
                                else
                                {
                                    strFieldValue = "";
                                }
                            }

                            //���ʱ��
                            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncTOFF")
                            {
                                if (drFlightsByAircraft[iRowIndx]["cncSTATUS"].ToString().Trim() == "DEP" || drFlightsByAircraft[iRowIndx]["cncSTATUS"].ToString().Trim() == "ARR" || drFlightsByAircraft[iRowIndx]["cncSTATUS"].ToString().Trim() == "ATA")
                                {
                                    strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                                }
                                else
                                {
                                    strFieldValue = "";
                                }
                            }

                            if (dataRowItems["cnvcDataItemID"].ToString() == "OutcniDUR1")
                            {
                                if (strFieldValue == "0")
                                {
                                    strFieldValue = "";
                                }
                            }

                            if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                            {
                                drInOutFlight[dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                            }
                        }

                        dtAllInOutFlights.Rows.Add(drInOutFlight);

                    }
                }
            }

            dtAllInOutFlights.DefaultView.Sort = "OutcncAllTOFF,IncncAllTDWN"; 
            //dtAllInOutFlights.DefaultView.Sort = "OutcnvcLONG_REG"; 
            return dtAllInOutFlights.DefaultView.ToTable() ;
        }
        #endregion

        #region ��ȡ������ʾ�����ķɻ���
        /// <summary>
        /// ��ȡ������ʾ�����ķɻ���
        /// </summary>
        /// <returns></returns>
        private DataTable GetConditionAircrafs()
        {            
            string strSearch = "";

            //���������ķɻ��ű�ܹ�
            DataTable dtConditionAirCrats = dtPositionAircrafts.Clone();

            //��ʾ���󺽰�
            if (m_accountBM.DisplayDelay == 1)
            {
                strSearch += "cniShow = 1 AND cnvcDELAY1 <>  '' AND cniDUR1 > " + m_accountBM.DelayMinutes + " OR ";
            }

            //��ʾ������������
            //drOutFlights[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drOutFlights[0]["cniLEGNO"].ToString()) % 100 != 0)
            if (m_accountBM.DisplayDiversion == 1)
            {
                strSearch += "cniShow = 1 AND cnvcDIV_RCODE <> '' OR cniLEGNO % 100 <> 0 OR ";
            }

            //��վʱ�䲻��
            if (m_accountBM.DisplayIntermission == 1)
            {
                strSearch += "(cncSTATUS = 'SCH' OR cncSTATUS = 'DEL') AND cniShow = 1 AND cniNotEnoughIntermissionTime = 1 OR ";
            }

            //û����ض�̬
            if (m_accountBM.DisplayTDWN == 1)
            {
                strSearch += "cncSTATUS = 'DEP' AND cniShow = 1 AND cniNotTDWN = 1 OR ";
            }

            //û����ɶ�̬
            if (m_accountBM.DisplayTOFF == 1)
            {
                strSearch += "(cncSTATUS = 'SCH' OR cncSTATUS = 'DEL') AND cniShow = 1 AND cniNotTOFF = 1 OR ";
            }

            //û�йز�ʱ��
            if (m_accountBM.DisplayClosePaxCabin == 1)
            {
                strSearch += "(cncSTATUS = 'SCH' OR cncSTATUS = 'DEL') AND  cniShow = 1 AND cniNotClosePaxCabineTime = 1 OR ";
            }

            if (strSearch != "")
            {
                strSearch =strSearch.Substring(0, strSearch.Length - 4);
                //���������ĺ���
                DataRow[] drConditionFlights = dtPositionFlights.Select(strSearch, "cnvcLONG_REG");

                foreach (DataRow dataRow in drConditionFlights)
                {
                    //�ж��Ƿ��Ѿ����ڸ÷ɻ���
                    DataRow[] drArrTempAircrafts = dtConditionAirCrats.Select("cnvcLONG_REG = '" + dataRow["cnvcLONG_REG"].ToString() + "'");

                    if (drArrTempAircrafts.Length <= 0)
                    {
                        DataRow drTempAircrafts = dtConditionAirCrats.NewRow();
                        drTempAircrafts["cnvcLONG_REG"] = dataRow["cnvcLONG_REG"].ToString();
                        dtConditionAirCrats.Rows.Add(drTempAircrafts);
                    }
                }
            }

            if (strSearch == "")
            {
                return dtPositionAircrafts;
            }
            else
            {
                return dtConditionAirCrats;
            }


        }
        #endregion

        #region ��ʼ����񱳾�ɫ
        /// <summary>
        /// ��ʼ����񱳾�ɫ
        /// </summary>
        private void InitialGridColor()
        {
            for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[0].Rows.Count; iLoop++)
            {
                fpFlightInfo.Sheets[0].Rows[iLoop].BackColor = colorFavorate;
                for (int jLoop = 0; jLoop < fpFlightInfo.Sheets[0].Columns.Count; jLoop++)
                {
                    fpFlightInfo.Sheets[0].Cells[iLoop, jLoop].BackColor = colorFavorate;
                }
            }
        }
        #endregion

        #region ����ĳ�е�Ԫ����ɫ
        /// <summary>
        /// ����ĳ�е�Ԫ����ɫ
        /// </summary>
        /// <param name="iRowIndex"></param>
        private void SetRowCellColor(int iRowIndex, Color colorValue)
        {         
            for (int jLoop = 0; jLoop < fpFlightInfo.Sheets[0].Columns.Count; jLoop++)
            {
                if (fpFlightInfo.Sheets[0].Cells[iRowIndex, jLoop].BackColor != Color.Yellow)
                {
                    fpFlightInfo.Sheets[0].Cells[iRowIndex, jLoop].BackColor = colorValue;
                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, jLoop, "");
                }
            }
        }
        #endregion

        #region ���ñ����ɫ
        /// <summary>
        /// ���ñ����ɫ
        /// </summary>
        private void SetGridColor()
        {
            try
            {
                int iRowIndex = 0;
                foreach (DataRow dataRowFlights in dtInOutFlights.Rows)
                {
                    DataRow[] drInFlights = dtPositionFlights.Select("cncDATOP='" + dataRowFlights["IncncDATOP"].ToString() + "' AND cnvcFLTID = '" + dataRowFlights["IncnvcFLTID"].ToString() + "' AND cniLEGNO = " + dataRowFlights["IncniLEGNO"].ToString() + " AND cnvcAC = '" + dataRowFlights["IncnvcAC"].ToString() + "'");
                    DataRow[] drOutFlights = dtPositionFlights.Select("cncDATOP='" + dataRowFlights["OutcncDATOP"].ToString() + "' AND cnvcFLTID = '" + dataRowFlights["OutcnvcFLTID"].ToString() + "' AND cniLEGNO = " + dataRowFlights["OutcniLEGNO"].ToString() + " AND cnvcAC = '" + dataRowFlights["OutcnvcAC"].ToString() + "'");

                    foreach (DataRow dataRowDataItem in dtDataItems.Rows)
                    {
                        if (drInFlights.Length > 0)   //����н��ۺ���
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightNo")  //���ۺ����
                            {
                                //����������
                                if (drInFlights[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drInFlights[0]["cniLEGNO"].ToString()) % 100 != 0)
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }

                                if (drInFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {

                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Orange;

                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //��������
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightCharacterAbbreviate")  //��������
                            {
                                if (drInFlights[0]["cnvcSTC"].ToString() != "J")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Red;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Black;
                                }
                            }
                            //��������ʱ��
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncETA")
                            {
                                if (drInFlights[0]["cnvcDELAY1"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor;
                                }
                            }

                            //����ʱ��
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncTDWN")
                            {
                                if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "DEP")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.LimeGreen;
                                }
                                else if (drInFlights[0]["cncSTATUS"].ToString().Trim() == "ATA")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Silver;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor; ;
                                }

                                //û�е��ﶯ̬�澯

                                if (m_accountBM.TDWNPromt == 1 && drInFlights[0]["cniNotTDWN"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }

                            }
                            //ֵ������
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncniCheckNum")
                            {
                                if (drInFlights[0]["cntCheckInfo"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cntCheckInfo"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //��ת�ÿ���Ϣ
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnbTransitPaxTag")
                            {
                                if (drInFlights[0]["cntTransitPax"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cntTransitPax"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //�ÿ�����
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncntPaxNameList")
                            {
                                if (drInFlights[0]["cntPaxNameList"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cntPaxNameList"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //���۱�ע
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcInRemark")
                            {
                                if (drInFlights[0]["cnvcInRemark"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cnvcInRemark"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncniFocusTag")
                            {
                                if (drInFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {
                                    //fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Orange;
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drInFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //����λ
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncncInMCCReadyTime")
                            {
                                if (m_accountBM.MCCReady == 1 && drInFlights[0]["cniMCCReady"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor; ;
                                }
                            }
                        }
                        else
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                            {
                                fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor; ;
                            }
                        }

                        //ֻ�н��ۺ���
                        if (drInFlights.Length > 0 && drOutFlights.Length == 0)
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightNo")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //ֵ����Ϣ
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniCheckNum")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //��ת�����ÿ�
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnbTransitPaxTag")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //�ÿ�����
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcntPaxNameList")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //��ע
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcOutRemark")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniFocusTag")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                        }

                        //ֻ�г��ۺ���
                        if (drInFlights.Length == 0 && drOutFlights.Length > 0)
                        {
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcFlightNo")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //��ת�ÿ���Ϣ
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnbTransitPaxTag")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //�ÿ�����
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncntPaxNameList")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            //���۱�ע
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncnvcInRemark")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "IncniFocusTag")
                            {
                                fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                            }
                        }

                        if (drOutFlights.Length > 0)
                        {
                            //��������ʱ��
                            if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncETD")
                            {
                                if (drOutFlights[0]["cnvcDELAY1"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncTOFF")
                            {
                                //û����ɶ�̬�澯
                                if (m_accountBM.TDWNPromt == 1 && drOutFlights[0]["cniNotTOFF"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightNo")
                            {
                                //�ص㱣�Ϻ���
                                if (drOutFlights[0]["cniFocusTag"].ToString() != "")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Orange;
                                }
                                //����������
                                else if (drOutFlights[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drOutFlights[0]["cniLEGNO"].ToString()) % 100 != 0)
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Yellow;
                                }
                                //��վʱ�䲻��
                                else if (drOutFlights[0]["cniNotEnoughIntermissionTime"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.Plum;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor;
                                }

                                if (drOutFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //��������
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcFlightCharacterAbbreviate")  //��������
                            {
                                if (drOutFlights[0]["cnvcSTC"].ToString() != "J")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Red;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].ForeColor = Color.Black;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncSTD")
                            {
                                //��ʼ������ʾ
                                if (m_accountBM.StartGuarantee == 1 && drOutFlights[0]["cniStartGuarantee"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncBoardTime")
                            {
                                //��ʼ�ǻ���ʾ
                                if (m_accountBM.Boarding == 1 && drOutFlights[0]["cniBoarding"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor;
                                }
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcncMCCReleaseTime")
                            {
                                //���������ʾ
                                if (m_accountBM.MCCRelease == 1 && drOutFlights[0]["cniMCCRelease"].ToString() == "1")
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = Color.SkyBlue;
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor;
                                }
                            }
                            //ֵ����Ϣ
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniCheckNum")
                            {
                                if (drOutFlights[0]["cntCheckInfo"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cntCheckInfo"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //��ת�����ÿ�
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnbTransitPaxTag")
                            {
                                if (drOutFlights[0]["cntTransitPax"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cntTransitPax"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //�ÿ�����
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcntPaxNameList")
                            {
                                if (drOutFlights[0]["cntPaxNameList"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cntPaxNameList"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //��ע
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcnvcOutRemark")
                            {
                                if (drOutFlights[0]["cnvcOutRemark"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cnvcOutRemark"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                            //�ص��ע����
                            else if (dataRowDataItem["cnvcDataItemID"].ToString() == "OutcniFocusTag")
                            {
                                if (drOutFlights[0]["cniFocusTag"].ToString().Trim() != "")
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), drOutFlights[0]["cniFocusTag"].ToString().Trim());
                                }
                                else
                                {
                                    fpFlightInfo.Sheets[0].SetNote(iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString()), "");
                                }
                            }
                        }
                        else
                        {
                            if (drInFlights[0]["cncSTATUS"].ToString() == "ATA")  //�Ѿ�ִ�������һ������
                            {
                                SetRowCellColor(iRowIndex, Color.Silver);
                                fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor = Color.Silver;
                            }
                            else if (dataRowDataItem["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                            {
                                fpFlightInfo.Sheets[0].Cells[iRowIndex, Convert.ToInt32(dataRowDataItem["cniViewIndex"].ToString())].BackColor = fpFlightInfo.Sheets[0].Rows[iRowIndex].BackColor; ;
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

        #region ��ȡĳ��ʱ�䷶Χ
        /// <summary>
        /// ��ȡĳ��ʱ�䷶Χ
        /// </summary>
        /// <param name="iDay">��һ��0:����1:����2:����3:ѡ�������</param>
        private DateTimeBM GetDateTimeBM(int iDay)
        {
            DateTimeBM dataTimeBM = new DateTimeBM();

            string strStartDateTime = "";
            string strEndDateTime = "";

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


            dataTimeBM.StartDateTime = strStartDateTime;
            dataTimeBM.EndDateTime = strEndDateTime;

            return dataTimeBM;
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
                    ReturnValueSF rvSF = changeRecordBF.GetLastWatchChangeRecords(m_iLastRecordNo, GetDateTimeBM(1), m_positionNameBM);
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
        private void timerChangeRecord_Tick(object sender, EventArgs e)
        {
            int iRefresh = 0;

            try
            {
                Monitor.Enter(oMutexChangeRecords);
                GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();

                DataRow[] drChangeRecords = m_dtChangeTable.Select("cniRefresh = 1");
                //��Ҫ������֯��ͼ
                if (drChangeRecords.Length > 0)
                {
                    iRefresh = 1;
                    timerSplash.Enabled = false;

                    DataTable dtChangeLegs = new DataTable();
                    ChangeLegsBM previousChangeLegsBM = new ChangeLegsBM();

                    foreach (DataRow dataRow in m_dtChangeTable.Rows)
                    {
                        ChangeLegsBM changeLegsBM = new ChangeLegsBM();
                        changeLegsBM.DATOP = dataRow["cncOldDATOP"].ToString();
                        changeLegsBM.FLTID = dataRow["cnvcOldFLTID"].ToString();
                        changeLegsBM.LEGNO = Convert.ToInt32(dataRow["cniOldLEGNO"].ToString());
                        changeLegsBM.AC = dataRow["cnvcOldAC"].ToString();

                        ChangeLegsBM changeNewKeyLegsBM = new ChangeLegsBM();

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

                        string strSearch = "cncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                           "cnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                           "cniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                           "cnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "' OR " +
                           "cncDATOP = '" + dataRow["cncNewDATOP"].ToString() + "' AND " +
                           "cnvcFLTID = '" + dataRow["cnvcNewFLTID"].ToString() + "' AND " +
                           "cniLEGNO = " + dataRow["cniNewLEGNO"].ToString() + " AND " +
                           "cnvcAC = '" + dataRow["cnvcNewAC"].ToString() + "'";

                        DataRow[] drPositionFlights = dtPositionFlights.Select(strSearch);
                        DataRow[] drSplashTag = dtSplashTag.Select(strSearch);

                        if (drPositionFlights.Length > 0 && drSplashTag.Length <= 0)
                        {
                            DataRow drTempSplashTag = dtSplashTag.NewRow();
                            drTempSplashTag["cncDATOP"] = drPositionFlights[0]["cncDATOP"].ToString();
                            drTempSplashTag["cnvcFLTID"] = drPositionFlights[0]["cnvcFLTID"].ToString();
                            drTempSplashTag["cniLEGNO"] = drPositionFlights[0]["cniLEGNO"].ToString();
                            drTempSplashTag["cnvcAC"] = drPositionFlights[0]["cnvcAC"].ToString();
                            dtSplashTag.Rows.Add(drTempSplashTag);
                            drSplashTag = dtSplashTag.Select(strSearch);
                        }

                        #region modified by LinYong in 2015.02.06
                        //dtChangeLegs = guaranteeInforBF.GetFlightByKey(changeNewKeyLegsBM).Dt;
                        dtChangeLegs = guaranteeInforBF.GetFlightByKey_NotCompress(changeNewKeyLegsBM, m_accountBM).Dt;
                        #endregion modified by LinYong in 2015.02.06

                        previousChangeLegsBM = changeLegsBM;

                        //����Ӧ�ļ�¼
                        if (drPositionFlights.Length > 0)
                        {
                            int iSplash = 0;
                            string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                            DataRow[] drInFlights = dtInOutFlights.Select(strInSearch);

                            string strOutSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                            DataRow[] drOutFlights = dtInOutFlights.Select(strOutSearch);

                            if (drInFlights.Length > 0)
                            {
                                strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                DataRow[] drInDataItem = dtDataItems.Select(strSearch);
                                if (drInDataItem.Length > 0)
                                {
                                    iSplash = 1;
                                }
                            }

                            if (drOutFlights.Length > 0)
                            {
                                strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                                DataRow[] drOutDataItem = dtDataItems.Select(strSearch);

                                if (drOutDataItem.Length > 0)
                                {
                                    iSplash = 1;
                                }
                            }

                            //�ж��Ƿ���˸��ʾ
                            strSearch = "cnvcPrimaryCodeField = '" + dataRow["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
                            DataRow[] drDataItemSplash = dtDataItemPurview.Select(strSearch);


                            if (dataRow["cncActionTag"].ToString() != "D" && dtChangeLegs.Rows.Count > 0)
                            {
                                drPositionFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                //SetInOutFlightDataRowValue(drPositionFlights[0]);
                                if (drDataItemSplash.Length > 0)
                                {
                                    if (dataRow["cnvcChangeReasonCode"].ToString() == "cncETA" || dataRow["cnvcChangeReasonCode"].ToString() == "cncETD")
                                    {
                                        if (dtChangeLegs.Rows[0]["cnvcDELAY1"].ToString() != "" && iSplash == 1)
                                        {
                                            drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds.ToString();
                                        }
                                    }
                                    else if (iSplash == 1)
                                    {
                                        if (dataRow["cnvcChangeReasonCode"].ToString() == "cncSTATUS")
                                        {
                                            if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEP")
                                            {
                                                drSplashTag[0]["cncTDWN"] = m_accountBM.SplashSeconds;
                                                drSplashTag[0]["cncTOFF"] = m_accountBM.SplashSeconds;
                                            }

                                            if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEL")
                                            {
                                                drSplashTag[0]["cncETD"] = m_accountBM.SplashSeconds;
                                                drSplashTag[0]["cncETA"] = m_accountBM.SplashSeconds;
                                            }

                                        }
                                        drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds;
                                    }
                                }
                            }
                            else if (dataRow["cncActionTag"].ToString() == "D" || dtChangeLegs.Rows.Count < 0)
                            {
                                dtPositionFlights.Rows.Remove(drPositionFlights[0]);
                                dtSplashTag.Rows.Remove(drSplashTag[0]);
                            }

                            dtPositionFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                            dtPositionFlights = dtPositionFlights.DefaultView.Table;

                            //������Ӧ����ʾ��Ϣ
                            ComputeFlightsInfor();

                            //��ʾ���к���
                            if (dsInOutFlights != null)
                            {
                                for (int iLoop = 0; iLoop < dsInOutFlights.Tables.Count; iLoop++)
                                {
                                    dsInOutFlights.Tables[iLoop].Constraints.Clear();
                                }
                                dsInOutFlights.Relations.Clear();
                                dsInOutFlights.Tables.Clear();
                                dsInOutFlights.Dispose();
                            }
                            dsInOutFlights = new DataSet();
                            if (m_accountBM.DisplayAll == 1)
                            {
                                dtInOutFlights = FillInOutFlights(dtPositionAircrafts);
                                dsInOutFlights.Tables.Add(dtInOutFlights);
                                dsInOutFlights.Tables.Add(dtPositionFlights);
                                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                                {
                                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                                }
                            }
                            //�������õ���ʾ������ʾ�����ۺ���
                            else
                            {
                                GetShowInfor();
                                dtInOutFlights = FillInOutFlights(GetConditionAircrafs());
                                dsInOutFlights.Tables.Add(dtInOutFlights);
                                dsInOutFlights.Tables.Add(dtConditionFlights);
                                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                                {
                                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                                }
                            }
                        }
                        else  //����Ӧ�ļ�¼
                        {
                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                DataRow drFlight = dtPositionFlights.NewRow();
                                drFlight.ItemArray = dtChangeLegs.Rows[0].ItemArray;

                                DataRow drSplash = dtSplashTag.NewRow();
                                drSplash["cncDATOP"] = drFlight["cncDATOP"].ToString();
                                drSplash["cnvcFLTID"] = drFlight["cnvcFLTID"].ToString();
                                drSplash["cniLEGNO"] = drFlight["cniLEGNO"].ToString();
                                drSplash["cnvcAC"] = drFlight["cnvcAC"].ToString();

                                dtPositionFlights.Rows.Add(drFlight);
                                dtSplashTag.Rows.Add(drSplash);

                                dtPositionFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                                dtPositionFlights = dtPositionFlights.DefaultView.Table;

                                //������Ӧ����ʾ��Ϣ
                                ComputeFlightsInfor();

                                //��ʾ���к���
                                if (dsInOutFlights != null)
                                {
                                    for (int iLoop = 0; iLoop < dsInOutFlights.Tables.Count; iLoop++)
                                    {
                                        dsInOutFlights.Tables[iLoop].Constraints.Clear();
                                    }
                                    dsInOutFlights.Relations.Clear();
                                    dsInOutFlights.Tables.Clear();
                                    dsInOutFlights.Dispose();
                                }
                                dsInOutFlights = new DataSet();
                                if (m_accountBM.DisplayAll == 1)
                                {
                                    dtInOutFlights = FillInOutFlights(dtPositionAircrafts);
                                    dsInOutFlights.Tables.Add(dtInOutFlights);
                                    dsInOutFlights.Tables.Add(dtPositionFlights);
                                    dsInOutFlights.Tables[0].TableName = "InOutFlights";
                                    dsInOutFlights.Tables[1].TableName = "PositionFlights";
                                    if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                                    {
                                        dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                                    }
                                }
                                //�������õ���ʾ������ʾ�����ۺ���
                                else
                                {
                                    GetShowInfor();
                                    dtInOutFlights = FillInOutFlights(GetConditionAircrafs());
                                    dsInOutFlights.Tables.Add(dtInOutFlights);
                                    dsInOutFlights.Tables.Add(dtConditionFlights);
                                    dsInOutFlights.Tables[0].TableName = "InOutFlights";
                                    dsInOutFlights.Tables[1].TableName = "PositionFlights";
                                    if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                                    {
                                        dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                                    }
                                }

                                //�ж��Ƿ���˸��ʾ                        


                                int iSplash = 0;
                                string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                    "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                    "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                    "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                                DataRow[] drInFlights = dtInOutFlights.Select(strInSearch);

                                string strOutSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                    "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                    "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                    "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                                DataRow[] drOutFlights = dtInOutFlights.Select(strOutSearch);

                                if (drInFlights.Length > 0)
                                {
                                    strSearch = "cnvcDataItemID = 'IncnvcLONG_REG'";
                                    DataRow[] drInDataItem = dtDataItems.Select(strSearch);
                                    if (drInDataItem.Length > 0)
                                    {
                                        iSplash = 1;
                                    }
                                }

                                if (drOutFlights.Length > 0)
                                {
                                    strSearch = "cnvcDataItemID = 'OutcnvcLONG_REG'";
                                    DataRow[] drOutDataItem = dtDataItems.Select(strSearch);

                                    if (drOutDataItem.Length > 0)
                                    {
                                        iSplash = 1;
                                    }
                                }

                                strSearch = "cnvcPrimaryCodeField = 'cnvcLONG_REG' AND cniSplashPromptItem = 1";
                                DataRow[] drDataItemSplash = dtDataItemPurview.Select(strSearch);

                                if (drDataItemSplash.Length > 0 && iSplash == 1)
                                {
                                    drSplash["cnvcLONG_REG"] = m_accountBM.SplashSeconds.ToString();
                                }

                            }
                        }
                    }

                    fpFlightInfo.DataSource = dsInOutFlights;
                    fpFlightInfo.DataMember = "InOutFlights";

                    colorArrOldBackGround = new Color[fpFlightInfo.Sheets[0].Rows.Count, fpFlightInfo.Sheets[0].Columns.Count];

                    //��ʼ�������ɫ
                    InitialGridColor();

                    //���ñ����ɫ
                    SetGridColor();

                    timerSplash.Enabled = true;

                }
                else   //����Ҫ������֯��ͼ
                {
                    ChangeLegsBM previousChangeLegsBM = new ChangeLegsBM();
                    DataTable dtChangeLegs = new DataTable();

                    foreach (DataRow dataRow in m_dtChangeTable.Rows)
                    {
                        ChangeLegsBM changeLegsBM = new ChangeLegsBM();
                        changeLegsBM.DATOP = dataRow["cncOldDATOP"].ToString();
                        changeLegsBM.FLTID = dataRow["cnvcOldFLTID"].ToString();
                        changeLegsBM.LEGNO = Convert.ToInt32(dataRow["cniOldLEGNO"].ToString());
                        changeLegsBM.AC = dataRow["cnvcOldAC"].ToString();

                        string strSearch = "cncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";


                        DataRow[] drPositionFlights = dtPositionFlights.Select(strSearch);
                        DataRow[] drSplashTag = dtSplashTag.Select(strSearch);

                        if (!(changeLegsBM.DATOP == previousChangeLegsBM.DATOP && changeLegsBM.FLTID == previousChangeLegsBM.FLTID && changeLegsBM.LEGNO == previousChangeLegsBM.LEGNO && changeLegsBM.AC == previousChangeLegsBM.AC))
                        {
                            #region modified by LinYong in 2015.02.06
                            //dtChangeLegs = guaranteeInforBF.GetFlightByKey(changeLegsBM).Dt;
                            dtChangeLegs = guaranteeInforBF.GetFlightByKey_NotCompress(changeLegsBM, m_accountBM).Dt;
                            #endregion modified by LinYong in 2015.02.06

                            previousChangeLegsBM = changeLegsBM;
                        }

                        int iSplash = 0;
                        string strInSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                        DataRow[] drInFlights = dtInOutFlights.Select(strInSearch);

                        string strOutSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                            "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                            "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                            "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "'";

                        DataRow[] drOutFlights = dtInOutFlights.Select(strOutSearch);

                        if (drInFlights.Length > 0)
                        {
                            strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                            DataRow[] drInDataItem = dtDataItems.Select(strSearch);
                            if (drInDataItem.Length > 0)
                            {
                                iSplash = 1;
                            }
                        }

                        if (drOutFlights.Length > 0)
                        {
                            strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcChangeReasonCode"].ToString() + "'";
                            DataRow[] drOutDataItem = dtDataItems.Select(strSearch);

                            if (drOutDataItem.Length > 0)
                            {
                                iSplash = 1;
                            }
                        }

                        //����Ӧ�ļ�¼
                        if (drPositionFlights.Length > 0)
                        {//�ж��Ƿ���˸��ʾ
                            strSearch = "cnvcPrimaryCodeField = '" + dataRow["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
                            DataRow[] drDataItemSplash = dtDataItemPurview.Select(strSearch);

                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                drPositionFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                SetInOutFlightDataRowValue(drPositionFlights[0]);

                                if (drDataItemSplash.Length > 0 && iSplash == 1)
                                {
                                    drSplashTag[0][dataRow["cnvcChangeReasonCode"].ToString()] = m_accountBM.SplashSeconds.ToString();
                                }
                            }
                        }
                    }

                }

                //������ʾ
                SoundPropt(m_dtChangeTable);

                if (iRefresh == 0 && m_accountBM.DisplayAll == 0 && (m_accountBM.DisplayTDWN == 1 || m_accountBM.DisplayTOFF == 1 || m_accountBM.DisplayClosePaxCabin == 1))
                {
                    timerSplash.Enabled = false;
                    //������Ӧ����ʾ��Ϣ
                    ComputeFlightsInfor();

                    if (dsInOutFlights != null)
                    {
                        for (int iLoop = 0; iLoop < dsInOutFlights.Tables.Count; iLoop++)
                        {
                            dsInOutFlights.Tables[iLoop].Constraints.Clear();
                        }
                        dsInOutFlights.Relations.Clear();
                        dsInOutFlights.Tables.Clear();
                        dsInOutFlights.Dispose();
                    }
                    dsInOutFlights = new DataSet();
                    GetShowInfor();
                    dtInOutFlights = FillInOutFlights(GetConditionAircrafs());
                    dsInOutFlights.Tables.Add(dtInOutFlights);
                    dsInOutFlights.Tables.Add(dtPositionFlights);
                    dsInOutFlights.Tables[0].TableName = "InOutFlights";
                    dsInOutFlights.Tables[1].TableName = "PositionFlights";
                    if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                    {
                        dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                    }

                    fpFlightInfo.DataSource = dsInOutFlights;
                    fpFlightInfo.DataMember = "InOutFlights";

                    colorArrOldBackGround = new Color[fpFlightInfo.Sheets[0].Rows.Count, fpFlightInfo.Sheets[0].Columns.Count];

                    //��ʼ�������ɫ
                    InitialGridColor();

                    //���ñ����ɫ
                    SetGridColor();

                    timerSplash.Enabled = true;

                }
                else if (iRefresh == 0)
                {
                    //������Ӧ����ʾ��Ϣ
                    ComputeFlightsInfor();

                    //���ñ����ɫ
                    SetGridColor();
                }

                m_dtChangeTable.Rows.Clear();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Monitor.Exit(oMutexChangeRecords);
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
            string strInSearch = "IncncDATOP = '" + dataRow["cncDATOP"].ToString() + "' AND " +
                            "IncnvcFLTID = '" +  dataRow["cnvcFLTID"].ToString() + "' AND " +
                            "IncniLEGNO = " +  dataRow["cniLEGNO"].ToString() + " AND " +
                            "IncnvcAC = '" +  dataRow["cnvcAC"].ToString() + "'";
            DataRow[] drInFlights = dtInOutFlights.Select(strInSearch);

            if (drInFlights.Length > 0)
            {
                //�����͵�����Ϣ
                drInFlights[0]["IncncDATOP"] = dataRow["cncDATOP"].ToString();
                drInFlights[0]["IncnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                drInFlights[0]["IncniLEGNO"] = dataRow["cniLEGNO"].ToString();
                drInFlights[0]["IncnvcAC"] = dataRow["cnvcAC"].ToString();
                drInFlights[0]["IncncAllTDWN"] = dataRow["cncTDWN"].ToString();

                foreach (DataRow dataRowItems in dtDataItems.Rows)
                {                    
                    string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();

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

                    //�ƻ�����ʱ��
                    if (dataRowItems["cnvcDataItemID"].ToString() == "IncncSTA")
                    {
                        strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                    }

                    //����ʱ��
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

                    //���ʱ��
                    if (dataRowItems["cnvcDataItemID"].ToString() == "IncncTDWN")
                    {
                        if (dataRow["cncSTATUS"].ToString().Trim() == "DEP" || dataRow["cncSTATUS"].ToString().Trim() == "ARR" || dataRow["cncSTATUS"].ToString().Trim() == "ATA")
                        {
                            strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                        }
                        else
                        {
                            strFieldValue = "";
                        }
                    }

                    //���ʱ��
                    if (dataRowItems["cnvcDataItemID"].ToString() == "IncncATA")
                    {
                        if (dataRow["cncSTATUS"].ToString().Trim() == "ATA")
                        {
                            strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                        }
                        else
                        {
                            strFieldValue = "";
                        }
                    }

                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("In") == 0)
                    {
                        drInFlights[0][dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }              
            }

            string strOutSearch = "OutcncDATOP = '" + dataRow["cncDATOP"].ToString() + "' AND " +
                                        "OutcnvcFLTID = '" + dataRow["cnvcFLTID"].ToString() + "' AND " +
                                        "OutcniLEGNO = " + dataRow["cniLEGNO"].ToString() + " AND " +
                                        "OutcnvcAC = '" + dataRow["cnvcAC"].ToString() + "'";
            DataRow[] drOutFlights = dtInOutFlights.Select(strOutSearch);

            if (drOutFlights.Length > 0)
            {
                drOutFlights[0]["OutcncDATOP"] = dataRow["cncDATOP"].ToString();
                drOutFlights[0]["OutcnvcFLTID"] = dataRow["cnvcFLTID"].ToString();
                drOutFlights[0]["OutcniLEGNO"] = dataRow["cniLEGNO"].ToString();
                drOutFlights[0]["OutcnvcAC"] = dataRow["cnvcAC"].ToString();
                drOutFlights[0]["OutcncAllTOFF"] = dataRow["cncTOFF"].ToString();

                foreach (DataRow dataRowItems in dtDataItems.Rows)
                {
                    string strFieldValue = dataRow[dataRowItems["cnvcPrimaryNameField"].ToString()].ToString().Trim();

                    if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncDEPAirportCNAME")
                    {
                        int iSplitIndex = strFieldValue.IndexOf("/");
                        if (iSplitIndex > 0)
                        {
                            strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                        }
                    }

                    if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncARRAirportCNAME")
                    {
                        int iSplitIndex = strFieldValue.IndexOf("/");
                        if (iSplitIndex > 0)
                        {
                            strFieldValue = strFieldValue.Substring(0, iSplitIndex);
                        }
                    }

                    //�ƻ����ʱ��
                    if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncSTD")
                    {
                        strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                    }

                    //�������ʱ��
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

                    //��ɶ�̬
                    if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncATD")
                    {
                        if (dataRow["cncSTATUS"].ToString().Trim() == "ATD" || dataRow["cncSTATUS"].ToString().Trim() == "DEP" || dataRow["cncSTATUS"].ToString().Trim() == "ARR" || dataRow["cncSTATUS"].ToString().Trim() == "ATA")
                        {
                            strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                        }
                        else
                        {
                            strFieldValue = "";
                        }
                    }

                    //��ɶ�̬
                    if (dataRowItems["cnvcDataItemID"].ToString() == "OutcncTOFF")
                    {
                        if (dataRow["cncSTATUS"].ToString().Trim() == "DEP" || dataRow["cncSTATUS"].ToString().Trim() == "ARR" || dataRow["cncSTATUS"].ToString().Trim() == "ATA")
                        {
                            strFieldValue = DateTime.Parse(strFieldValue).ToString("HHmm");
                        }
                        else
                        {
                            strFieldValue = "";
                        }
                    }

                    if (dataRowItems["cnvcDataItemID"].ToString() == "OutcniDUR1")
                    {
                        if (strFieldValue == "0")
                        {
                            strFieldValue = "";
                        }
                    }

                    if (dataRowItems["cnvcDataItemID"].ToString().IndexOf("Out") == 0)
                    {
                        drOutFlights[0][dataRowItems["cnvcDataItemID"].ToString()] = strFieldValue;
                    }
                }
            }
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
                DataRow[] drSplashTag;

                if (m_strDataItemSearch == null || m_strDataItemSearch == "")
                {
                    return;
                }
                drSplashTag = dtSplashTag.Select(m_strDataItemSearch);

                if (drSplashTag.Length <= 0)
                {
                    return;
                }
                int iRowIndex = 0;

                string strSearch = string.Empty;
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
                DataRow[] drInOutFlights = dtInOutFlights.Select(strSearch);

                foreach (DataRow drInOutFlight in drInOutFlights)
                {
                    iRowIndex = dtInOutFlights.Rows.IndexOf(drInOutFlight);
                    for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[0].Columns.Count; iLoop++)
                    {
                        //�����ֶ���Ϣ
                        strSearch = "cnvcDataItemID = '" + fpFlightInfo.Sheets[0].Columns[iLoop].DataField + "'";
                        DataRow[] drDataItem = dtDataItems.Select(strSearch);

                        string strPrimaryCodeField = drDataItem[0]["cnvcPrimaryCodeField"].ToString();
                        string strDataItemName = drDataItem[0]["cnvcDataItemName"].ToString();


                        if (fpFlightInfo.Sheets[0].Columns[iLoop].DataField.IndexOf("In") == 0)  //�����ֶ�
                        {
                            //���Һ�����Ϣ
                            strSearch = "cncDATOP = '" + drInOutFlight["IncncDATOP"].ToString() + "' AND " +
                                "cnvcFLTID = '" + drInOutFlight["IncnvcFLTID"].ToString() + "' AND " +
                                "cniLEGNO = " + drInOutFlight["IncniLEGNO"].ToString() + " AND " +
                                "cnvcAC = '" + drInOutFlight["IncnvcAC"].ToString() + "'";
                        }
                        else
                        {
                            //���Һ�����Ϣ
                            strSearch = "cncDATOP = '" + drInOutFlight["OutcncDATOP"].ToString() + "' AND " +
                                "cnvcFLTID = '" + drInOutFlight["OutcnvcFLTID"].ToString() + "' AND " +
                                "cniLEGNO = " + drInOutFlight["OutcniLEGNO"].ToString() + " AND " +
                                "cnvcAC = '" + drInOutFlight["OutcnvcAC"].ToString() + "'";
                        }

                        drSplashTag = dtSplashTag.Select(strSearch);

                        if (drSplashTag.Length > 0)
                        {
                            if (drSplashTag[0][strPrimaryCodeField].ToString().Trim() != "")
                            {
                                if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) > 0)
                                {
                                    //��Ϊż��
                                    if (DateTime.Now.Second % 2 == 0)
                                    {
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            if (fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor != Color.DarkBlue)
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor;
                                            }
                                            else
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = m_cOldBackGroudColor;
                                            }
                                        }

                                        fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor = Color.Red;
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpFlightInfo.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.Red;
                                        }
                                        else
                                        {
                                            fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.Red;
                                        }
                                        if (m_accountBM.SplashAutoStop == 1 || Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            drSplashTag[0][strPrimaryCodeField] = Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) - 1;
                                        }

                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                            if (strDataItemName.IndexOf("|") >= 0)
                                            {
                                                fpFlightInfo.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                            }
                                            else
                                            {
                                                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                            }
                                        }
                                    }
                                    //��Ϊ����
                                    else if (DateTime.Now.Second % 2 == 1)
                                    {
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            if (fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor != Color.DarkBlue)
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor;
                                            }
                                            else
                                            {
                                                colorArrOldBackGround[iRowIndex, iLoop] = m_cOldBackGroudColor;
                                            }
                                        }

                                        fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpFlightInfo.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                        }
                                        else
                                        {
                                            fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                        }

                                        if (m_accountBM.SplashAutoStop == 1 || Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) == m_accountBM.SplashSeconds)
                                        {
                                            drSplashTag[0][strPrimaryCodeField] = Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString().Trim()) - 1;
                                        }

                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            fpFlightInfo.Sheets[0].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                            if (strDataItemName.IndexOf("|") >= 0)
                                            {
                                                fpFlightInfo.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                            }
                                            else
                                            {
                                                fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
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

        #region ��񵥻���ʹ��˸ֹͣ
        /// <summary>
        /// ��񵥻��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader)
            {
                m_iHeadRow = e.Row;
            }
            if (e.View.Sheets[0].RowCount != dtInOutFlights.Rows.Count)
            {
                return;
            }
            if (!e.ColumnHeader && !e.RowHeader)
            {

                string strChangeReasonCode = "";
                string strDataItemName = "";
                string strSearch = "";

                if (fpFlightInfo.Sheets[0].Columns[e.Column].DataField.IndexOf("In") == 0) //���ۺ���
                {
                    strChangeReasonCode = fpFlightInfo.Sheets[0].Columns[e.Column].DataField.Substring(2);
                    strSearch = "cncDATOP = '" + dtInOutFlights.Rows[e.Row]["IncncDATOP"].ToString() + "' AND " +
                        "cnvcFLTID = '" + dtInOutFlights.Rows[e.Row]["IncnvcFLTID"].ToString() + "' AND " +
                        "cniLEGNO = " + dtInOutFlights.Rows[e.Row]["IncniLEGNO"].ToString() + " AND " +
                        "cnvcAC = '" + dtInOutFlights.Rows[e.Row]["IncnvcAC"].ToString() + "'";
                }
                else
                {
                    strChangeReasonCode = fpFlightInfo.Sheets[0].Columns[e.Column].DataField.Substring(3);
                    strSearch = "cncDATOP = '" + dtInOutFlights.Rows[e.Row]["OutcncDATOP"].ToString() + "' AND " +
                       "cnvcFLTID = '" + dtInOutFlights.Rows[e.Row]["OutcnvcFLTID"].ToString() + "' AND " +
                       "cniLEGNO = " + dtInOutFlights.Rows[e.Row]["OutcniLEGNO"].ToString() + " AND " +
                       "cnvcAC = '" + dtInOutFlights.Rows[e.Row]["OutcnvcAC"].ToString() + "'";
                }

                DataRow[] drDataItems = dtDataItems.Select("cnvcPrimaryNameField = '" + strChangeReasonCode + "'");

                if (drDataItems.Length > 0)
                {
                    strChangeReasonCode = drDataItems[0]["cnvcPrimaryCodeField"].ToString();
                    strDataItemName = drDataItems[0]["cnvcDataItemName"].ToString();
                }

                DataRow[] drSplashTag = dtSplashTag.Select(strSearch);

                if (drSplashTag.Length > 0)
                {
                    if (strDataItemName.IndexOf("|") >= 0)
                    {
                        fpFlightInfo.Sheets[0].ColumnHeader.Cells[1, e.Column].BackColor = Color.FromName("Control");
                    }
                    else
                    {
                        fpFlightInfo.Sheets[0].ColumnHeader.Cells[0, e.Column].BackColor = Color.FromName("Control");
                    }
                    

                    if (drSplashTag[0][strChangeReasonCode].ToString() != "")
                    {
                        fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].BackColor = colorArrOldBackGround[e.Row, e.Column];
                    }
                    drSplashTag[0][strChangeReasonCode] = "0";
                    //iFirstEnterSplash[e.Row, e.Column] = 1;
                }

                //���õ�Ԫ��ѡ����ɫ
                if (m_iOldSelectedRow != -1 && m_iOldSelectedRow < fpFlightInfo.Sheets[0].RowCount)
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

                //selectedGuaranteeInfo = new GuaranteeInforBM(dtInOutFlights.Rows[e.Row]);
                inChangeLegsBM = GetChangeLegsBM(0, e.Row);
                outChangeLegsBM = GetChangeLegsBM(1, e.Row);
            }

            if (e.ColumnHeader)
            {
                if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcFlightNo" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcFlightNo")
                {
                    string strDataField = fpFlightInfo.ActiveSheet.Columns[e.Column].DataField;
                    //�������ݱ仯����ֶ���Ϣ
                    DataRow[] drDataItems = dtDataItems.Select("cnvcDataItemID = '" + strDataField + "'");

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

                    
                    //����ѡ�еļ�¼                    
                    GuaranteeInforBM findGuaranteeInfo = null;
                    int iRowIndex = 0;
                    //���ݷɻ��Ų���
                    if (fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "IncnvcLONG_REG" || fpFlightInfo.ActiveSheet.Columns[e.Column].DataField == "OutcnvcLONG_REG")
                    {
                        iRowIndex = 0;
                        foreach (DataRow drInOutFlight in dtInOutFlights.Rows)
                        {
                            findGuaranteeInfo = new GuaranteeInforBM(drInOutFlight);
                            if (findGuaranteeInfo.OutcnvcLONG_REG.IndexOf(objfmQueryFlight.FindContent) >= 0)
                            {
                                fpFlightInfo.ActiveSheet.AddSelection(iRowIndex, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                                fpFlightInfo.ShowRow(0, iRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                                //selectedGuaranteeInfo = findGuaranteeInfo;
                                inChangeLegsBM = GetChangeLegsBM(0, iRowIndex);
                                outChangeLegsBM = GetChangeLegsBM(1, iRowIndex);
                                break;
                            }
                            findGuaranteeInfo = null;
                            iRowIndex += 1;
                        }
                    }
                    else   //���ݺ���Ų���
                    {
                        iRowIndex = 0;
                        foreach (DataRow drInOutFlight in dtInOutFlights.Rows)
                        {

                            findGuaranteeInfo = new GuaranteeInforBM(drInOutFlight);
                            if (findGuaranteeInfo.IncnvcFlightNo.IndexOf(objfmQueryFlight.FindContent) >= 0 || findGuaranteeInfo.OutcnvcFlightNo.IndexOf(objfmQueryFlight.FindContent) >= 0)
                            {
                                fpFlightInfo.ActiveSheet.AddSelection(iRowIndex, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                                fpFlightInfo.ShowRow(0, iRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                                //selectedGuaranteeInfo = findGuaranteeInfo;
                                inChangeLegsBM = GetChangeLegsBM(0, iRowIndex);
                                outChangeLegsBM = GetChangeLegsBM(1, iRowIndex);
                                break;
                            }
                            findGuaranteeInfo = null;
                            iRowIndex += 1;
                        }
                    }

                    if (findGuaranteeInfo != null)
                    {
                        if (m_iOldSelectedRow != -1)
                        {
                            fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;

                            fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;

                            m_iOldSelectedRow = iRowIndex;
                            m_cOldBackGroudColor = fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor;
                            m_cOldForeColor = fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor;

                            //���ó��µ�ѡ�е���ɫ
                            fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                            fpFlightInfo.ActiveSheet.Cells[iRowIndex, m_iOldSelectedColumn].ForeColor = Color.White;
                        }
                    }

                }
            }

            //�����Ҽ��˵�
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

        }
        #endregion

        #region ���ݺ���Ų��Һ���
        /// <summary>
        /// ���ݺ���Ų��Һ���
        /// </summary>
        public void FindFlightByFlightNo(string strFlightNo)
        {
            //����ѡ�еļ�¼                    
            GuaranteeInforBM findGuaranteeInfo = null;
            int iRowIndex = 0;
            foreach (DataRow drInOutFlight in dtInOutFlights.Rows)
            {

                findGuaranteeInfo = new GuaranteeInforBM(drInOutFlight);
                if (findGuaranteeInfo.IncnvcFlightNo.IndexOf(strFlightNo) >= 0 || findGuaranteeInfo.OutcnvcFlightNo.IndexOf(strFlightNo) >= 0)
                {
                    fpFlightInfo.ActiveSheet.AddSelection(iRowIndex, 0, 1, fpFlightInfo.ActiveSheet.ColumnCount);
                    fpFlightInfo.ShowRow(0, iRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                    //selectedGuaranteeInfo = findGuaranteeInfo;
                    inChangeLegsBM = GetChangeLegsBM(0, iRowIndex);
                    outChangeLegsBM = GetChangeLegsBM(1, iRowIndex);
                    break;
                }
                iRowIndex += 1;
            }

            if (findGuaranteeInfo != null)
            {
                if (m_iOldSelectedRow != -1)
                {
                    fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;

                    fpFlightInfo.ActiveSheet.Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;

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

        #region �����û����õ���ʾ������ʾѡ��ĺ���
        /// <summary>
        /// �����û����õ���ʾ������ʾѡ��ĺ���
        /// </summary>
        public void ShowConditionFlights()
        {
            timerChangeRecord.Enabled = false;
            timerSplash.Enabled = false;

            //������Ӧ����ʾ��Ϣ
            ComputeFlightsInfor();
            if (dsInOutFlights != null)
            {
                for (int iLoop = 0; iLoop < dsInOutFlights.Tables.Count; iLoop++)
                {
                    dsInOutFlights.Tables[iLoop].Constraints.Clear();
                }
                dsInOutFlights.Relations.Clear();
                dsInOutFlights.Tables.Clear();		
                dsInOutFlights.Dispose();
            }
            dsInOutFlights = new DataSet();
            //��ʾ���к���
            if (m_accountBM.DisplayAll == 1)
            {
                dtInOutFlights = FillInOutFlights(dtPositionAircrafts);                
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtPositionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            }
            //�������õ���ʾ������ʾ�����ۺ���
            else
            {
                GetShowInfor();
                dtInOutFlights = FillInOutFlights(GetConditionAircrafs());                
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtConditionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            }

            //dtInOutFlights.DefaultView.Sort = "OutcncAllTOFF,IncncAllTDWN"; 
            fpFlightInfo.DataSource = dsInOutFlights;
            fpFlightInfo.DataMember = "InOutFlights";

            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[0].Rows.Count, fpFlightInfo.Sheets[0].Columns.Count];
            //iFirstEnterSplash = new int[fpFlightInfo.Sheets[0].Rows.Count, fpFlightInfo.Sheets[0].Columns.Count];
            //for (int iLoop = 0; iLoop < fpFlightInfo.Sheets[0].Rows.Count; iLoop++)
            //{
            //    for (int jLoop = 0; jLoop < fpFlightInfo.Sheets[0].Columns.Count; jLoop++)
            //    {
            //        iFirstEnterSplash[iLoop, jLoop] = 1;
            //    }
            //}

            //��ʼ�������ɫ
            InitialGridColor();

            //���ñ����ɫ
            SetGridColor();

            timerChangeRecord.Enabled = true;
            timerSplash.Enabled = true;
        }
        #endregion

        #region ˢ�º��ද̬
        /// <summary>
        /// ˢ�º��ද̬
        /// </summary>
        public void FlightRefresh()
        {
            //����ʱ�䷶Χʵ�����
            string strStartDateTime = DateTime.Now.AddHours(-5).Date.ToString("yyyy-MM-dd") + " 05:00:00";
            string strEndDateTime = DateTime.Now.AddHours(-5).Date.AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";

            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = strStartDateTime;
            dateTimeBM.EndDateTime = strEndDateTime;

            //��ȡĳϯλ�ķɻ���Ϣ
            GetPositionAircrafts(m_positionNameBM);

            //��ȡĳϯλ��ص����к���
            GetFlightsByPosition(dateTimeBM, m_positionNameBM);

            //������Ӧ����ʾ��Ϣ
            ComputeFlightsInfor();

            //�����ۺ�����Schema
            GetDisplaySchema();

            //��ʾ���к���
            if (dsInOutFlights != null)
            {
                for (int iLoop = 0; iLoop < dsInOutFlights.Tables.Count; iLoop++)
                {
                    dsInOutFlights.Tables[iLoop].Constraints.Clear();
                }
                dsInOutFlights.Relations.Clear();
                dsInOutFlights.Tables.Clear();		
                dsInOutFlights.Dispose();
            }
            dsInOutFlights = new DataSet();
            if (m_accountBM.DisplayAll == 1)
            {
                dtInOutFlights = FillInOutFlights(dtPositionAircrafts);
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtPositionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            
                
            }
            //�������õ���ʾ������ʾ�����ۺ���
            else
            {
                GetShowInfor();
                dtInOutFlights = FillInOutFlights(GetConditionAircrafs());
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtConditionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            
            }

            fpFlightInfo.DataSource = dsInOutFlights;
            fpFlightInfo.DataMember = "InOutFlights";

            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[0].Rows.Count, fpFlightInfo.Sheets[0].Columns.Count];

            //��ʼ�������ɫ
            InitialGridColor();

            //���ñ����ɫ
            SetGridColor();

            if (m_iOldSelectedRow >= FpFlightInfo.Sheets[0].RowCount)
            {
                m_iOldSelectedRow = -1;
            }
        }
        #endregion

        #region ����������ͼʱ������ˢ����ɫ
        /// <summary>
        /// ����������ͼʱ������ˢ����ɫ
        /// </summary>
        public void RefreshView()
        {
           
            timerSplash.Enabled = false;
            //������Ӧ����ʾ��Ϣ
            ComputeFlightsInfor();

            //�����ۺ�����Schema
            GetDisplaySchema();

            //��ʾ���к���
            if (dsInOutFlights != null)
            {
                for (int iLoop = 0; iLoop < dsInOutFlights.Tables.Count; iLoop++)
                {
                    dsInOutFlights.Tables[iLoop].Constraints.Clear();
                }
                dsInOutFlights.Relations.Clear();
                dsInOutFlights.Tables.Clear();			
                dsInOutFlights.Dispose();
            }
            dsInOutFlights = new DataSet();
            if (m_accountBM.DisplayAll == 1)
            {
                dtInOutFlights = FillInOutFlights(dtPositionAircrafts);             
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtPositionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            
            }
            //�������õ���ʾ������ʾ�����ۺ���
            else
            {
                GetShowInfor();
                dtInOutFlights = FillInOutFlights(GetConditionAircrafs());
                dsInOutFlights.Tables.Add(dtInOutFlights);
                dsInOutFlights.Tables.Add(dtConditionFlights);
                dsInOutFlights.Tables[0].TableName = "InOutFlights";
                dsInOutFlights.Tables[1].TableName = "PositionFlights";
                if (dsInOutFlights.Tables[0].Columns.Contains("OutcnvcLONG_REG"))
                {
                    dsInOutFlights.Relations.Add("LONG_REG", dsInOutFlights.Tables[0].Columns["OutcnvcLONG_REG"], dsInOutFlights.Tables[1].Columns["cnvcLONG_REG"]);
                }
            }

            fpFlightInfo.DataSource = dsInOutFlights;
            fpFlightInfo.DataMember = "InOutFlights";

            colorArrOldBackGround = new Color[fpFlightInfo.Sheets[0].Rows.Count, fpFlightInfo.Sheets[0].Columns.Count];

            //��ʼ�������ɫ
            InitialGridColor();

            //���ñ����ɫ
            SetGridColor();

            timerSplash.Enabled = true;
        }
        #endregion

        #region ������ʾ
        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="dtChangeData"></param>
        private void SoundPropt(DataTable dtChangeData)
        {
            foreach (DataRow dataRow in dtChangeData.Rows)
            {               
                int iInFlight = 0;
                int iOutFlight = 0;
                
                string strSearch = "IncncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                    "IncnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                    "IncniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                    "IncnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "' OR " +
                    "IncncDATOP = '" + dataRow["cncNewDATOP"].ToString() + "' AND " +
                    "IncnvcFLTID = '" + dataRow["cnvcNewFLTID"].ToString() + "' AND " +
                    "IncniLEGNO = " + dataRow["cniNewLEGNO"].ToString() + " AND " +
                    "IncnvcAC = '" + dataRow["cnvcNewAC"].ToString() + "'";

                DataRow[] drInFlight = dtInOutFlights.Select(strSearch);

                if (drInFlight.Length > 0)
                {
                    iInFlight = 1;
                }

                strSearch = "OutcncDATOP = '" + dataRow["cncOldDATOP"].ToString() + "' AND " +
                                    "OutcnvcFLTID = '" + dataRow["cnvcOldFLTID"].ToString() + "' AND " +
                                    "OutcniLEGNO = " + dataRow["cniOldLEGNO"].ToString() + " AND " +
                                    "OutcnvcAC = '" + dataRow["cnvcOldAC"].ToString() + "' OR " +
                                    "OutcncDATOP = '" + dataRow["cncNewDATOP"].ToString() + "' AND " +
                                    "OutcnvcFLTID = '" + dataRow["cnvcNewFLTID"].ToString() + "' AND " +
                                    "OutcniLEGNO = " + dataRow["cniNewLEGNO"].ToString() + " AND " +
                                    "OutcnvcAC = '" + dataRow["cnvcNewAC"].ToString() + "'";

                DataRow[] drOutFlight = dtInOutFlights.Select(strSearch);

                if (drOutFlight.Length > 0)
                {
                    iOutFlight = 1;
                }

                if (iInFlight == 1 || iOutFlight == 1)
                {

                    if (iOutFlight == 1)
                    {
                        strSearch = "cnvcDataItemID = 'Out" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniSoundPromptItem = 1";
                    }
                    else
                    {
                        strSearch = "cnvcDataItemID = 'In" + dataRow["cnvcPrimaryNameField"].ToString() + "' AND cniSoundPromptItem = 1";
                    }

                    DataRow[] drDataItemSound = dtDataItemPurview.Select(strSearch);
                    if (drDataItemSound.Length > 0)
                    {
                        if (m_accountBM.SoundType == 0)
                        {
                            string mstrfileName = Application.StartupPath;
                            mstrfileName = mstrfileName + @"\WAV\front.WAV";
                            SoundHelpers.PlaySound(mstrfileName, IntPtr.Zero, SoundHelpers.PlaySoundFlags.SND_FILENAME | SoundHelpers.PlaySoundFlags.SND_ASYNC);
                        }
                        else
                        {
                            Beep(0X0FF, 100);
                        }
                    }
                }
            }
        }
        #endregion

        #region ˫����Ԫ�񣬵���ά�����ݴ���
        /// <summary>
        /// ˫����Ԫ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.View.Sheets[0].RowCount != dtInOutFlights.Rows.Count)
            {
                return;
            }

            if (fpFlightInfo.ActiveSheetIndex == 0)
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

        #region ά������
        /// <summary>
        /// ά������
        /// </summary>
        /// <param name="e"></param>
        private void Maintennance(FarPoint.Win.Spread.CellClickEventArgs e)
        {

            GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM(dtInOutFlights.Rows[e.Row]);            

            string strDataItemID = fpFlightInfo.Sheets[0].Columns[e.Column].DataField.ToString();

            DataRow[] dataRow = dtDataItemPurview.Select("cnvcDataItemID = '" + strDataItemID + "'");

            if (dataRow[0]["cnvcPrimaryCodeField"].ToString() != "cnbVIPTag")
            {
                if (dataRow[0]["cniDataItemPurview"].ToString() != "2")
                {
                    MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            int iDataItemPurview = Convert.ToInt32(dataRow[0]["cniDataItemPurview"].ToString());

            int iFieldLength = Convert.ToInt32(dataRow[0]["cniFieldLength"].ToString());

            //ά������
            int iMainTainType = Convert.ToInt32(dataRow[0]["cniMaintenType"].ToString());
            //�ֶ�����
            int iFieldType = Convert.ToInt32(dataRow[0]["cniFieldType"].ToString());

            string strPrimaryCodeField = dataRow[0]["cnvcPrimaryCodeField"].ToString();



            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            ChangeLegsBM changeLegsBM = new ChangeLegsBM();

            maintenGuaranteeInforBM.FieldType = iFieldType;
            maintenGuaranteeInforBM.OldContent = fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text;
            maintenGuaranteeInforBM.ColumnCaption = dataRow[0]["cnvcDataItemName"].ToString();

            if (strDataItemID.IndexOf("In") == 0 && guaranteeInforBM.IncncDATOP != "1900-01-01") //���ۺ���
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

                DataRow[] dataRowFlight = dtPositionFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
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
                    changeRecordBM.ChangeOldContent = fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text;
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
            // 
            else if (strDataItemID.IndexOf("Out") == 0 && guaranteeInforBM.OutcncDATOP != "1900-01-01" || strDataItemID == "IncnvcInGATE")  //���ۺ���
            {
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

                changeRecordBM.UserID = m_accountBM.UserId;
                changeRecordBM.OldDATOP = guaranteeInforBM.OutcncDATOP;
                changeRecordBM.OldFLTID = guaranteeInforBM.OutcnvcFLTID;
                changeRecordBM.OldLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeRecordBM.OldAC = guaranteeInforBM.OutcnvcAC;
                changeRecordBM.NewDATOP = guaranteeInforBM.OutcncDATOP;
                changeRecordBM.NewFLTID = guaranteeInforBM.OutcnvcFLTID;
                changeRecordBM.NewLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                changeRecordBM.NewAC = guaranteeInforBM.OutcnvcAC;

                DataRow[] dataRowFlight = dtPositionFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
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
                    changeRecordBM.ChangeOldContent = fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text;
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
            else
            {
                return;
            }

            //ʱ���ı�
            if (iMainTainType == 1)
            {
                fmMaintenTime objfmMaintenTime = new fmMaintenTime(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenTime.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenTime.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            else if (iMainTainType == 2) //SingleText
            {
                fmMaitenSingleText objfmMaitenSingleText = new fmMaitenSingleText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaitenSingleText.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaitenSingleText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            else if (iMainTainType == 3) //MultiTex
            {
                fmMaintenMutiLineText objfmMaintenMutiLineText = new fmMaintenMutiLineText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenMutiLineText.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenMutiLineText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            else if (iMainTainType == 4) //List
            {
                fmMaintenList objfmMaintenList = new fmMaintenList(maintenGuaranteeInforBM, changeRecordBM, m_stationBM);
                if (objfmMaintenList.ShowDialog() == DialogResult.OK)
                {
                    fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenList.MMaintenGuaranteeInforBM.NewText;
                }
            }
            else
            {
                if (strPrimaryCodeField == "cnbVIPTag")
                {
                    fmMaintenVIP objfmMaintenVIP = new fmMaintenVIP(changeLegsBM, m_accountBM, iDataItemPurview);
                    objfmMaintenVIP.ShowDialog();
                }
                else if (strPrimaryCodeField == "cnvcInGATE")
                {
                    MaintenGuaranteeInforBM outMaintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                    ChangeRecordBM outChangeRecordBM = new ChangeRecordBM();

                    int iOut = 0;
                    if (guaranteeInforBM.OutcncDATOP != "1900-01-01")
                    {

                        outMaintenGuaranteeInforBM.DATOP = guaranteeInforBM.OutcncDATOP;
                        outMaintenGuaranteeInforBM.FLTID = guaranteeInforBM.OutcnvcFLTID;
                        outMaintenGuaranteeInforBM.LEGNO = guaranteeInforBM.OutcniLEGNO;
                        outMaintenGuaranteeInforBM.AC = guaranteeInforBM.OutcnvcAC;
                        outMaintenGuaranteeInforBM.FieldName = "cnvcOutGate";
                        outMaintenGuaranteeInforBM.FieldLength = iFieldLength;
                        outMaintenGuaranteeInforBM.FieldType = 1;

                        outChangeRecordBM.UserID = m_accountBM.UserId;
                        outChangeRecordBM.OldDATOP = guaranteeInforBM.OutcncDATOP;
                        outChangeRecordBM.OldFLTID = guaranteeInforBM.OutcnvcFLTID;
                        outChangeRecordBM.OldLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                        outChangeRecordBM.OldAC = guaranteeInforBM.OutcnvcAC;
                        outChangeRecordBM.NewDATOP = guaranteeInforBM.OutcncDATOP;
                        outChangeRecordBM.NewFLTID = guaranteeInforBM.OutcnvcFLTID;
                        outChangeRecordBM.NewLegNo = Convert.ToInt32(guaranteeInforBM.OutcniLEGNO);
                        outChangeRecordBM.NewAC = guaranteeInforBM.OutcnvcAC;

                        DataRow[] dataRowOutGateFlight = dtPositionFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
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
                            outChangeRecordBM.ChangeOldContent = fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text;
                            outChangeRecordBM.ActionTag = "U";
                            outChangeRecordBM.Refresh = 0;
                        }

                        iOut = 1;
                    }

                    fmMaintenInGate objfmMaintenInGate = new fmMaintenInGate(maintenGuaranteeInforBM, outMaintenGuaranteeInforBM, changeRecordBM, outChangeRecordBM, iOut);
                    if (objfmMaintenInGate.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenInGate.MMaintenGuaranteeInforBM.NewContent;
                    }
                }
                else if (strPrimaryCodeField == "cniCheckNum" || strPrimaryCodeField == "cnvcBookNum" || strPrimaryCodeField == "cnvcInGATE")
                {
                    fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(changeLegsBM, changeRecordBM);
                    if (objfmCheckPax.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmCheckPax.NewContent;
                    }
                }
                else if (strPrimaryCodeField == "cntPaxNameList")
                {
                    fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(changeLegsBM, changeRecordBM);
                    if (objfmMaintenPaxNameList.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenPaxNameList.NewContent;
                    }
                }
                else if (strPrimaryCodeField == "cnbTransitPaxTag")
                {
                    fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(changeLegsBM, changeRecordBM);
                    if (objfmMaintenTransitPax.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenTransitPax.NewContent;
                    }
                }
                else if (strPrimaryCodeField == "cncTDWN")
                {
                    changeRecordBM.Refresh = 1;
                    fmMaintenTDWN objfmMaintenTDWN = new fmMaintenTDWN(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenTDWN.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenTDWN.NewContent;
                    }
                }
                else if (strPrimaryCodeField == "cncTOFF")
                {
                    changeRecordBM.Refresh = 1;
                    fmMaintenTOFF objfmMaintenTOFF = new fmMaintenTOFF(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenTOFF.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.Sheets[0].Cells[e.Row, e.Column].Text = objfmMaintenTOFF.NewContent;
                    }
                }
                else if (strPrimaryCodeField == "cniTotalFuelWeight")
                {
                    fmMaintenFuel objfmMaintenFuel = new fmMaintenFuel(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                    if (objfmMaintenFuel.ShowDialog() == DialogResult.OK)
                    {
                        fpFlightInfo.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenFuel.NewContent;
                    }
                }
            }
        }
        #endregion

        #region �����û��س��༭
        /// <summary>
        /// �����û��س��༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpFlightInfo_KeyDown(object sender, KeyEventArgs e)
        {
            FarPoint.Win.Spread.CellClickEventArgs cellClickEventArgs = new FarPoint.Win.Spread.CellClickEventArgs(null, m_iOldSelectedRow, m_iOldSelectedColumn, 10, 10, MouseButtons.None, false, false);

            if (m_iOldSelectedRow != -1)
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
            if (m_iOldSelectedRow != -1)
            {
                //��ԭ��ɫ
                fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = m_cOldBackGroudColor;
                fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = m_cOldForeColor;

                if (e.KeyCode.ToString() == Keys.Up.ToString())
                {
                    if (m_iOldSelectedRow > 0)
                    {
                        m_iOldSelectedRow -= 1;
                    }
                }
                else if (e.KeyCode.ToString() == Keys.Down.ToString())
                {
                    if (m_iOldSelectedRow < fpFlightInfo.Sheets[0].Rows.Count - 1)
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
                    if (m_iOldSelectedColumn < fpFlightInfo.Sheets[0].ColumnCount - 1)
                    {
                        m_iOldSelectedColumn += 1;
                    }
                }

                m_cOldBackGroudColor = fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor;
                m_cOldForeColor = fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor;

                //���ó��µ�ѡ�е���ɫ
                fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].BackColor = Color.DarkBlue;
                fpFlightInfo.Sheets[0].Cells[m_iOldSelectedRow, m_iOldSelectedColumn].ForeColor = Color.White;

                fpFlightInfo.Sheets[0].AddSelection(m_iOldSelectedRow, 0, 1, fpFlightInfo.Sheets[0].ColumnCount);
            }
        }
        #endregion

        #region �Ŵ���С�¼�
        /// <summary>
        /// �Ŵ�
        /// </summary>
        public void ZoomOut()
        {
            FpFlightInfo.ZoomFactor +=(float) 0.01;
        }

        /// <summary>
        /// ��С
        /// </summary>
        public void ZoomIn()
        {
            FpFlightInfo.ZoomFactor -=(float) 0.01;
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
                //����ǰ�����б�ǩ����ɫ�ĳ�Silver���Ա㵼����Excel�ļ���ͷ��ɫ���������ֿ���
                //				objfmClientMain.FpFlightInfo.ActiveSheet.ColumnHeader.DefaultStyle.BackColor = Color.FromName("Silver");
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

        #region ��ȡ���ද̬���ʵ��
        private ChangeRecordBM GetChangeRecordBM(int iOut)
        {
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM(dtInOutFlights.Rows[m_iRightButtonRow]);

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

                DataRow[] dataRowFlight = dtPositionFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
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

                DataRow[] dataRowFlight = dtPositionFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
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

        #region ��ȡ������ʵ��
        private ChangeLegsBM GetChangeLegsBM(int iOut, int iRow)
        {
            ChangeLegsBM changeLegsBM = new ChangeLegsBM();
            GuaranteeInforBM guaranteeInforBM = new GuaranteeInforBM(dtInOutFlights.Rows[iRow]);         

            if (iOut == 0)
            {
                //������Ϣ
                changeLegsBM.DATOP = guaranteeInforBM.IncncDATOP;
                changeLegsBM.FLTID = guaranteeInforBM.IncnvcFLTID;
                changeLegsBM.LEGNO = Convert.ToInt32(guaranteeInforBM.IncniLEGNO);
                changeLegsBM.AC = guaranteeInforBM.IncnvcAC;

                DataRow[] dataRowFlight = dtPositionFlights.Select("cncDATOP = '" + guaranteeInforBM.IncncDATOP + "' AND " +
                        "cnvcFLTID = '" + guaranteeInforBM.IncnvcFLTID + "' AND " +
                        "cniLEGNO = " + guaranteeInforBM.IncniLEGNO + " AND " +
                        "cnvcAC = '" + guaranteeInforBM.IncnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                    changeLegsBM.FlightNo = dataRowFlight[0]["cnvcFlightNo"].ToString();                    
                    changeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                    changeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                    changeLegsBM.LONG_REG = dataRowFlight[0]["cnvcLONG_REG"].ToString();
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
                DataRow[] dataRowFlight = dtPositionFlights.Select("cncDATOP = '" + guaranteeInforBM.OutcncDATOP + "' AND " +
                        "cnvcFLTID = '" + guaranteeInforBM.OutcnvcFLTID + "' AND " +
                        "cniLEGNO = " + guaranteeInforBM.OutcniLEGNO + " AND " +
                        "cnvcAC = '" + guaranteeInforBM.OutcnvcAC + "'");

                if (dataRowFlight.Length > 0)
                {
                    changeLegsBM.FlightDate = dataRowFlight[0]["cncFlightDate"].ToString();
                    changeLegsBM.FlightNo = dataRowFlight[0]["cnvcFlightNo"].ToString();
                    changeLegsBM.CKIFlightDate = dataRowFlight[0]["cncCKIFlightDate"].ToString();
                    changeLegsBM.CKIFlightNo = dataRowFlight[0]["cnvcCKIFlightNo"].ToString();
                    changeLegsBM.LONG_REG = dataRowFlight[0]["cnvcLONG_REG"].ToString();
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

        #region �Ҽ��˵��¼�
        /// <summary>
        /// ���ۺ��ຽ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutAircraftFlights_Click(object sender, EventArgs e)
        {
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = DateTime.Now.AddHours(-5).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddHours(-5).AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            string strLONG_REG = "";
            //if (inChangeLegsBM.LONG_REG != null && inChangeLegsBM.LONG_REG != "")
            //{
            //    strLONG_REG = inChangeLegsBM.LONG_REG;
            //}
            //else
            //{
                strLONG_REG = outChangeLegsBM.LONG_REG;
            //}
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1, m_iRightButtonRow);
            fmAircraftFlights objfmAircraftFlights = new fmAircraftFlights(dateTimeBM, strLONG_REG, m_accountBM, dtDataItemPurview);
            objfmAircraftFlights.ShowDialog();
        }

        /// <summary>
        /// ���۱������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutChangeData_Click(object sender, EventArgs e)
        {
            fmChangeData objfmChangeData = new fmChangeData(GetDateTimeBM(1), m_accountBM, outChangeLegsBM.FlightNo, "", 0);
            objfmChangeData.ShowDialog();
        }

        
        /// <summary>
        /// �ж��Ƿ���Ȩ��
        /// </summary>
        /// <param name="strDataItemID"></param>
        private int GetDataItemPurview(string strDataItemID)
        {
            DataRow[] dataRow = dtDataItemPurview.Select("cnvcDataItemID = '" + strDataItemID + "'");

            if (dataRow.Length > 0)
            {
                return Convert.ToInt32(dataRow[0]["cniDataItemPurview"].ToString());
            }
            else
            {
                return 0;
            }
        }



        /// <summary>
        /// ����ֵ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutCheckPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1, m_iRightButtonRow);

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

        /// <summary>
        /// ������ת�ÿ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutTransitPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1, m_iRightButtonRow);
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

        /// <summary>
        /// �����ÿ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOutNameList_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(1);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(1, m_iRightButtonRow);
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

        /// <summary>
        /// ���ۺ��ຽ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInAircraftFlights_Click(object sender, EventArgs e)
        {
            DateTimeBM dateTimeBM = new DateTimeBM();
            dateTimeBM.StartDateTime = DateTime.Now.AddHours(-5).ToString("yyyy-MM-dd") + " 05:00:00";
            dateTimeBM.EndDateTime = DateTime.Now.AddHours(-5).AddDays(1).ToString("yyyy-MM-dd") + " 05:00:00";
            string strLONG_REG = "";
            //if (inChangeLegsBM.LONG_REG != null && inChangeLegsBM.LONG_REG != "")
            //{
            //    strLONG_REG = inChangeLegsBM.LONG_REG;
            //}
            //else
            //{
                strLONG_REG = inChangeLegsBM.LONG_REG;
            //}

            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0, m_iRightButtonRow);
            fmAircraftFlights objfmAircraftFlights = new fmAircraftFlights(dateTimeBM, strLONG_REG,  m_accountBM, dtDataItemPurview);
            objfmAircraftFlights.ShowDialog();
        }

        /// <summary>
        /// ���۱������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInChangeData_Click(object sender, EventArgs e)
        {
            fmChangeData objfmChangeData = new fmChangeData(GetDateTimeBM(1), m_accountBM, inChangeLegsBM.FlightNo, "", 0);
            objfmChangeData.ShowDialog();
        }

        /// <summary>
        /// ����ֵ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInCheckPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0, m_iRightButtonRow);

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

        /// <summary>
        /// ������ת�ÿ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInTransitPax_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0, m_iRightButtonRow);
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

        /// <summary>
        /// �����ÿ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miInNameList_Click(object sender, EventArgs e)
        {
            ChangeRecordBM changeRecordBM = GetChangeRecordBM(0);
            ChangeLegsBM changeLegsBM = GetChangeLegsBM(0, m_iRightButtonRow);
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
        #endregion

        #region ��ʾ���Ե��ı�
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

        #region ��ʾ���б�
        private void fpFlightInfo_ChildViewCreated(object sender, FarPoint.Win.Spread.ChildViewCreatedEventArgs e)
        {
            e.SheetView.ColumnHeader.RowCount = 2;
            e.SheetView.AutoGenerateColumns = false;
            e.SheetView.DataAutoSizeColumns = false;
            e.SheetView.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            SpreadGrid spreadGrid = new SpreadGrid(m_accountBM);
            spreadGrid.SetView(e.SheetView, dtDataItems, 1);

            int iColumnIndex = 0;
            foreach (DataRow dataRow in dtDataItems.Rows)
            {
                if (dataRow["cnvcPrimaryCodeField"].ToString() == "cncDEPSTN" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncARRSTN")
                {
                    string[] strAirportName = new string[e.SheetView.RowCount];
                    for (int jLoop = 0; jLoop < e.SheetView.RowCount; jLoop++)
                    {
                        strAirportName[jLoop] = e.SheetView.Cells[jLoop, iColumnIndex].Text;
                    }

                    e.SheetView.Columns[iColumnIndex].DataField = ""; 
                    for(int jLoop = 0; jLoop < e.SheetView.RowCount; jLoop++)
                    {
                        int iSplitIndedx = strAirportName[jLoop].IndexOf("/");
                        if (iSplitIndedx > 0)
                        {
                            e.SheetView.Cells[jLoop, iColumnIndex].Text = strAirportName[jLoop].Substring(0, iSplitIndedx);
                        }
                        else
                        {
                            e.SheetView.Cells[jLoop, iColumnIndex].Text = strAirportName[jLoop];
                        }
                    }                    
                }
                else if (dataRow["cnvcPrimaryCodeField"].ToString() == "cncSTA" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncETA" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncTDWN" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncATA" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncSTD" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncETD" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncTOFF" || dataRow["cnvcPrimaryCodeField"].ToString() == "cncATD")
                {
                    e.SheetView.Columns[iColumnIndex].DataField = "";

                    DataRow[] drArrChild = dtInOutFlights.Rows[m_iHeadRow].GetChildRows("LONG_REG");

                    for (int jLoop = 0; jLoop < e.SheetView.RowCount; jLoop++)
                    {
                        e.SheetView.Cells[jLoop, iColumnIndex].Text = DateTime.Parse(drArrChild[jLoop][dataRow["cnvcPrimaryCodeField"].ToString()].ToString()).ToString("HHmm");
                        if (dataRow["cnvcPrimaryCodeField"].ToString() == "cncTDWN")
                        {
                            if (drArrChild[jLoop]["cncSTATUS"].ToString() == "SCH")
                            {
                                e.SheetView.Cells[jLoop, iColumnIndex].BackColor = colorFavorate;
                            }                            
                            else if (drArrChild[jLoop]["cncSTATUS"].ToString() == "DEP")
                            {
                                e.SheetView.Cells[jLoop, iColumnIndex].BackColor = Color.LimeGreen;
                            }
                            else if (drArrChild[jLoop]["cncSTATUS"].ToString() == "ATA")
                            {
                                e.SheetView.Cells[jLoop, iColumnIndex].BackColor = Color.Silver;
                            }
                        }

                        if (dataRow["cnvcPrimaryCodeField"].ToString() == "cncETA")
                        {
                            if (drArrChild[jLoop]["cnvcDELAY1"].ToString() != "")
                            {
                                e.SheetView.Cells[jLoop, iColumnIndex].BackColor = Color.Yellow;
                            }                            
                        }
                    }    
                }

                iColumnIndex += 1;
            }
        }
        #endregion
    }    
}