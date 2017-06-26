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
        //���̶߳�ʱ������������������������ⱻ��������
        private System.Threading.Timer timer;
        private int iRefreshInterval = 20;
        private object oMutexChangeRecords = new object();

        //��˸ǰ��Ԫ����ɫ
        Color[,] colorArrOldBackGround;
        private Color m_cOldBackGroudColor;
        private Color m_cOldForeColor;

        /// <summary>
        /// ���ݱ���ʾ�ؼ�
        /// </summary>
        public FarPoint.Win.Spread.FpSpread FpFlightInfo
        {
            get { return fpsFlightDisp; }
        }

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="accountBM">�û���Ϣ</param>
        /// <param name="dataItems">������</param>
        /// <param name="positionNameBM">ϯλ��Ϣ</param>
        public fmFlightDisp(AccountBM accountBM, string strFormType, DataTable dtDeskAircraft, PositionNameBM deskNameBM)
        {
            InitializeComponent();

            //�û���Ϣ
            this.AccountBM = accountBM;
            //��������
            this.FormType = strFormType;
            //ϯλ����
            this.DeskNameBM = deskNameBM;
            //��ʾ��������
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(this.AccountBM, strFormType);
            this.DisplayDataItems = rvSF.Dt;
            //��ϯλ���зɻ�
            this.DeskAircrafts = dtDeskAircraft;
            //��ʼ��ϯλ���ද̬
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmFlightDisp_Load(object sender, EventArgs e)
        {
            //������ͼ
            fpsFlightDisp.Sheets[0].RowHeader.Columns[0].Width = 50;
            timerChangeRecord.Interval = this.AccountBM.RefreshInterval * 1000;
            SpreadGrid spreadGrid = new SpreadGrid(this.AccountBM);
            spreadGrid.SetView(shFlightDisp, this.DisplayDataItems, 0);

            BindMainView();

            //���û���ˢ��Ƶ��д�������ļ�
            int iTempInterval = this.AccountBM.RefreshInterval * 1000;
            timerChangeRecord.Interval = iTempInterval;
            TimerCallback timerDelegate = new TimerCallback(GetChangeDate);
            timer = new System.Threading.Timer(timerDelegate, null, 0, iRefreshInterval);
        }
        #endregion

        #region ��ȡ������ݣ����̶߳�ʱ���ã�
        public void GetChangeDate(object state)
        {
            //����ҵ����۲㷽��
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();

            lock (this)
            {
                //����û����ò�ˢ�£����˳�����
                if (this.AccountBM.RefreshInterval == 0)
                    return;

                try
                {
                    //�����������
                    Monitor.Enter(oMutexChangeRecords);
                    //��ȡ���һ���������
                    ReturnValueSF rvSF = changeRecordBF.GetLastWatchChangeRecords(this.LastRecordNo, GetDateTimeBM(1), this.DeskNameBM);
                    if (rvSF.Result > 0)
                    {
                        if (rvSF.Dt.Rows.Count > 0)
                        {
                            if (this.ChangeRecordTable.Columns.Count == 0)
                                this.ChangeRecordTable = rvSF.Dt.Clone();

                            //����ѯ���ı������д�뵽�����Ϣ����
                            foreach (DataRow dataRow in rvSF.Dt.Rows)
                            {
                                this.ChangeRecordTable.Rows.Add(dataRow.ItemArray);
                                //������������
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
                    //�ͷŻ������
                    Monitor.Exit(oMutexChangeRecords);
                }
            }
        }
        #endregion

        #region ��ʱ��������
        private void timerChangeRecord_Tick(object sender, EventArgs e)
        {
            int iRefresh = 0;

            try
            {
                Monitor.Enter(oMutexChangeRecords);

                //�Ƿ���Ҫˢ����ͼ
                DataRow[] drChangeRecords = this.ChangeRecordTable.Select("cniRefresh = 1");

                #region ��Ҫ������֯��ͼ
                //��Ҫ������֯��ͼ
                if (drChangeRecords.Length > 0)
                {
                    iRefresh = 1;
                    timerSplash.Enabled = false;
                    DataTable dtChangeLegs = new DataTable();

                    //���д�������¼
                    foreach (DataRow drFlightChange in this.ChangeRecordTable.Rows)
                    {
                        //���α��ʵ�����
                        ChangeLegsBM oldChangeLegsBM = new ChangeLegsBM();
                        //���ݱ��ǰ���������ɱ��ʵ��
                        //�Թؼ��ָ�ֵ
                        oldChangeLegsBM.DATOP = drFlightChange["cncOldDATOP"].ToString();
                        oldChangeLegsBM.FLTID = drFlightChange["cnvcOldFLTID"].ToString();
                        oldChangeLegsBM.LEGNO = Convert.ToInt32(drFlightChange["cniOldLEGNO"].ToString());
                        oldChangeLegsBM.AC = drFlightChange["cnvcOldAC"].ToString();
                        //���ݱ������������ɱ��ʵ��
                        ChangeLegsBM newChangeLegsBM = new ChangeLegsBM();
                        //�Թؼ��ֽ��и�ֵ
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
                        //���ݱ����ĺ�����Ϣ��ѯ����ı�����Ϣ
                        FlightDispBF flightDispBF = new FlightDispBF();
                        dtChangeLegs = flightDispBF.GetFlightByKey(newChangeLegsBM).Dt;

                        //���ɲ�ѯ���
                        string strSearch = "cncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "' OR " +
                            "cncDATOP = '" + drFlightChange["cncNewDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + drFlightChange["cnvcNewFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + drFlightChange["cniNewLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + drFlightChange["cnvcNewAC"].ToString() + "'";

                        //���ݱ��α�����ڴ���в�ѯ���ද̬����˸��̬
                        DataRow[] drDeskFlights = this.DeskFlights.Select(strSearch);
                        DataRow[] drSplashTag = this.SplashTagTable.Select(strSearch);

                        //�����ȡ����غ�����ϢAnd��˸����û�иú������˸��̬��¼
                        if (drDeskFlights.Length > 0 && drSplashTag.Length <= 0)
                        {
                            //����һ����Ϣ����˸��̬��¼
                            DataRow drTempSplashTag = this.SplashTagTable.NewRow();
                            drTempSplashTag["cncDATOP"] = drDeskFlights[0]["cncDATOP"].ToString();
                            drTempSplashTag["cnvcFLTID"] = drDeskFlights[0]["cnvcFLTID"].ToString();
                            drTempSplashTag["cniLEGNO"] = drDeskFlights[0]["cniLEGNO"].ToString();
                            drTempSplashTag["cnvcAC"] = drDeskFlights[0]["cnvcAC"].ToString();
                            this.SplashTagTable.Rows.Add(drTempSplashTag);

                            //��������¼�����ѯ�����
                            drSplashTag = this.SplashTagTable.Select(strSearch);
                        }

                        #region ��վ���ද̬����Ӧ�ļ�¼
                        //��վ���ද̬����Ӧ�ļ�¼
                        if (drDeskFlights.Length > 0)
                        {
                            //����������ɾ������AND���ݱ����ĺ���ؼ��ֲ�ѯ���ౣ����Ϣ�����Ϊ��
                            if (drFlightChange["cncActionTag"].ToString() != "D" && dtChangeLegs.Rows.Count > 0)
                            {
                                //�ñ��������ݸ��¸ú���ı�����Ϣ
                                drDeskFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;

                                //������˸ʱ��
                                DataRow dr = null;
                                SetSplash(drSplashTag, drFlightChange, dtChangeLegs, dr, 2);
                            }
                            //��������ɾ������OR���ݱ����ĺ���ؼ��ֲ�ѯ���ౣ����Ϣ���Ϊ��
                            else if (drFlightChange["cncActionTag"].ToString() == "D" || dtChangeLegs.Rows.Count == 0)
                            {
                                //�Ӻ��ද̬�����˸��̬���н���ؼ�¼ɾ��
                                this.DeskFlights.Rows.Remove(drDeskFlights[0]);
                                this.SplashTagTable.Rows.Remove(drSplashTag[0]);
                            }

                            //������������
                            this.DeskFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                            this.DeskFlights = this.DeskFlights.DefaultView.Table;

                            //��ʾ���к���                    
                            this.DisplayDeskFlights = FillFlightInfo(this.DeskAircrafts, this.DeskFlights, "Dsp", this.DisplayDataItems);

                            //���°�
                            fpsFlightDisp.Sheets[0].DataSource = this.DisplayDeskFlights;
                        }
                        #endregion

                        #region ��վ���ද̬����Ӧ�ļ�¼���������ࣩ
                        //��վ���ද̬����Ӧ�ļ�¼
                        else
                        {
                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //����һ��
                                DataRow drNewFlight = this.DeskFlights.NewRow();
                                //�����������ݲ��뺽����Ϣ����
                                drNewFlight.ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                this.DeskFlights.Rows.Add(drNewFlight);

                                //��������
                                this.DeskFlights.DefaultView.Sort = "cnvcLONG_REG, cncETD";
                                this.DeskFlights = this.DeskFlights.DefaultView.Table;

                                //��ʾ���к���                    
                                this.DisplayDeskFlights = FillFlightInfo(this.DeskAircrafts, this.DeskFlights, "Dsp", this.DisplayDataItems);
                                //���°�
                                fpsFlightDisp.Sheets[0].DataSource = this.DisplayDeskFlights;
                                //������˸
                                SetSplash(drSplashTag, drFlightChange, dtChangeLegs, drNewFlight, 1);
                            }
                        }
                        #endregion
                    }

                    colorArrOldBackGround = new Color[fpsFlightDisp.Sheets[1].Rows.Count, fpsFlightDisp.Sheets[1].Columns.Count];

                    //��ʼ�������ɫ
                    InitialGridColor(shFlightDisp);

                    ////���ñ����ɫ
                    //SetGridColor(1, m_dtTodayStationFlights);

                    timerSplash.Enabled = true;
                }
                #endregion

                #region ����Ҫ������֯��ͼ
                //����Ҫ������֯��ͼ
                else
                {
                    DataTable dtChangeLegs = new DataTable();

                    //������������¼
                    foreach (DataRow drFlightChange in this.ChangeRecordTable.Rows)
                    {
                        //���α��ʵ�����
                        ChangeLegsBM oldChangeLegsBM = new ChangeLegsBM();
                        //���ݱ��ǰ���������ɱ��ʵ��
                        //�Թؼ��ָ�ֵ
                        oldChangeLegsBM.DATOP = drFlightChange["cncOldDATOP"].ToString();
                        oldChangeLegsBM.FLTID = drFlightChange["cnvcOldFLTID"].ToString();
                        oldChangeLegsBM.LEGNO = Convert.ToInt32(drFlightChange["cniOldLEGNO"].ToString());
                        oldChangeLegsBM.AC = drFlightChange["cnvcOldAC"].ToString();
                        //���ݱ������������ɱ��ʵ��
                        ChangeLegsBM newChangeLegsBM = new ChangeLegsBM();
                        //�Թؼ��ֽ��и�ֵ
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
                        //���ݱ����ĺ�����Ϣ��ѯ����ı�����Ϣ
                        FlightDispBF flightDispBF = new FlightDispBF();
                        dtChangeLegs = flightDispBF.GetFlightByKey(newChangeLegsBM).Dt;

                        //��ѯ���
                        string strSearch = "cncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
                            "cnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
                            "cniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
                            "cnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "'";

                        //��ѯ�漰����ĺ������˸��Ϣ
                        DataRow[] drDeskFlights = this.DeskFlights.Select(strSearch);
                        DataRow[] drSplashTag = this.SplashTagTable.Select(strSearch);

                        //���ݱ��ǰ�ĺ�����Ϣ��ѯ����ı�����Ϣ
                        dtChangeLegs = flightDispBF.GetFlightByKey(newChangeLegsBM).Dt;

                        //����Ӧ�ļ�¼
                        if (drDeskFlights.Length > 0)
                        {
                            if (dtChangeLegs.Rows.Count > 0)
                            {
                                //���º�����Ϣ
                                drDeskFlights[0].ItemArray = dtChangeLegs.Rows[0].ItemArray;
                                //����������ʾ
                                //SetInOutFlightDataRowValue(drDeskFlights[0]);

                                //������˸
                                DataRow dr = null;
                                SetSplash(drSplashTag, drFlightChange, dtChangeLegs, dr, 0);
                            }
                        }
                    }
                }
                #endregion

                //�������¼��ӵ������ʾ����
                if (this.ChangeRecordTable.Rows.Count != 0)
                {
                    //AddChangeDataToList(this.ChangeRecordTable, 0);
                }

                if (iRefresh == 0)
                {
                    ////������Ӧ����ʾ��Ϣ
                    //ComputeFlightsInfor(m_dtTodayStationFlights);

                    ////���ñ����ɫ
                    //SetGridColor(1, m_dtTodayStationFlights);
                }

                //��ձ����¼
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

        #region ������˸ʱ��
        /// <summary>
        /// ���ݺ�����������˸ʱ��
        /// </summary>
        /// <param name="drFlightChange">��������¼</param>
        /// <param name="drSplashTag">��˸��Ǽ�¼</param>
        /// <param name="iChangeMode">������ģʽ</param>
        /// iChangeMode = 0������Ҫˢ����ͼ
        /// iChangeMode = 1����Ҫˢ����ͼ����������
        /// iChangeMode = 2����Ҫˢ����ͼ�������¼�������
        private void SetSplash(DataRow[] drSplashTag, DataRow drFlightChange, DataTable dtChangeLegs, DataRow drNewFlight, int iChangeMode)
        {
            int iSplash = 0;

            //��ѯ����ĺ����Ƿ�����ʾ��ϯλ���ද̬��
            string strSearch = "DspcncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
                "DspcnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
                "DspcniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
                "DspcnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "'";
            DataRow[] drSplashFlights = this.DisplayDeskFlights.Select(strSearch);

            //�������ĺ�����ʾ��ϯλ���ද̬��
            if (drSplashFlights.Length > 0)
            {
                //��ѯ�漰������������Ƿ���ʾ
                strSearch = "cnvcDataItemID = 'Dsp" + drFlightChange["cnvcChangeReasonCode"].ToString() + "'";
                DataRow[] drChangeDataItem = this.DisplayDataItems.Select(strSearch);
                //������ڣ�����˸�����Ϊ1
                if (drChangeDataItem.Length > 0)
                {
                    iSplash = 1;
                }
            }

            //�ж��漰������������Ƿ���������˸
            strSearch = "cnvcPrimaryCodeField = '" + drFlightChange["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
            DataRow[] drDataItemSplash = this.DisplayDataItems.Select(strSearch);
            //����������������
            string strChangedField = drFlightChange["cnvcChangeReasonCode"].ToString();

            //�����Ҫ��˸��ʾ
            if (drDataItemSplash.Length > 0)
            {
                switch (iChangeMode)
                {
                    case 0:
                        //������˸ʱ��
                        if (iSplash == 1)
                        {
                            drSplashTag[0][strChangedField] = this.AccountBM.SplashSeconds;
                        }
                        break;
                    case 1:
                        if (iSplash == 1)
                        {
                            //������˸��¼
                            DataRow drSplash = this.SplashTagTable.NewRow();
                            drSplash["cncDATOP"] = drNewFlight["cncDATOP"].ToString();
                            drSplash["cnvcFLTID"] = drNewFlight["cnvcFLTID"].ToString();
                            drSplash["cniLEGNO"] = drNewFlight["cniLEGNO"].ToString();
                            drSplash["cnvcAC"] = drNewFlight["cnvcAC"].ToString();
                            drSplash["cnvcLONG_REG"] = this.AccountBM.SplashSeconds.ToString();
                            //���¼�¼��ӵ�������Ϣ�����˸����
                            this.SplashTagTable.Rows.Add(drSplash);
                        }
                        break;
                    case 2:
                        //��������������ΪETA����ETD
                        if (strChangedField == "cncETA" || strChangedField == "cncETD")
                        {
                            //�������ԭ��Ϊ��AND��˸���=1
                            //���������û������ETA��ETD�仯����˸
                            if (dtChangeLegs.Rows[0]["cnvcDELAY1"].ToString() != "" && iSplash == 1)
                            {
                                //������˸ʱ��
                                drSplashTag[0][strChangedField] = this.AccountBM.SplashSeconds.ToString();
                            }
                        }
                        //�����˸���=1
                        else if (iSplash == 1)
                        {
                            //��������������Ϊ����״̬
                            if (strChangedField == "cncSTATUS")
                            {
                                //�������״̬��ΪDEP
                                if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEP")
                                {
                                    //�������ʱ������ʱ�䵥Ԫ��Ҳ��˸
                                    drSplashTag[0]["cncTDWN"] = this.AccountBM.SplashSeconds;
                                    drSplashTag[0]["cncTOFF"] = this.AccountBM.SplashSeconds;
                                }

                                //�������״̬���ΪDEL
                                if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEL")
                                {
                                    //�����Ԥ�����ʱ���Ԥ��ʱ��Ҳ��˸
                                    drSplashTag[0]["cncETD"] = this.AccountBM.SplashSeconds;
                                    drSplashTag[0]["cncETA"] = this.AccountBM.SplashSeconds;
                                }
                            }
                        }
                        //�������������������˸ʱ��
                        drSplashTag[0][strChangedField] = this.AccountBM.SplashSeconds;
                        break;
                    default:
                        break;
                }//end switch
            }//end if (drDataItemSplash.Length > 0)
        }
        #endregion

        #region ��ʱ��˸
        private void timerSplash_Tick(object sender, EventArgs e)
        {
            Splash(this.DisplayDataItems, this.SplashTagTable, this.DisplayDeskFlights, fpsFlightDisp, m_cOldBackGroudColor);
        }
        #endregion

        #region ˫����Ԫ�񣬲鿴��ά������
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

        #region ����������ͼ
        /// <summary>
        /// ����������ͼ
        /// </summary>
        public void ViewRefresh(DataTable dtDisplayDataItems)
        {
            timerSplash.Enabled = false;

            //��ʾ��������
            this.DisplayDataItems = dtDisplayDataItems;
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
            BindMainView();
            timerSplash.Enabled = true;
        }
        #endregion

        #region �л�ϯλ
        public void DeskChange(DataTable dtDeskAircrafts)
        {
            timerSplash.Enabled = false;
            this.DeskAircrafts = dtDeskAircrafts;
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
            BindMainView();
            timerSplash.Enabled = true;
        }
        #endregion

        #region ˢ�º��ද̬
        /// <summary>
        /// ˢ�º��ද̬
        /// </summary>
        public override void FlightRefresh()
        {
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
            BindMainView();
        }
        #endregion

        #region ������ͼ
        public void BindMainView()
        {
            //������ʾ��
            DataTable dtDeskFlights = this.DisplayDeskFlights;
            DataSet dsDeskFlights = new DataSet();
            dsDeskFlights.Tables.Add(dtDeskFlights);

            //������Դ
            fpsFlightDisp.DataSource = dsDeskFlights;
            fpsFlightDisp.DataMember = dsDeskFlights.Tables[0].TableName;

            //��ʼ�������ɫ
            this.InitialGridColor(shFlightDisp);
            //���õ�Ԫ����ɫ
            this.SetGridColor(this.DisplayDeskFlights, this.DeskFlights, this.DisplayDataItems, fpsFlightDisp, this.FormType);
        }
        #endregion
    }
}