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
            get { return fpsFlightMon; }
        }

        #region ���췽��
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="strFormType"></param>
        /// <param name="positionNameBM"></param>
        public fmFlightMoni(AccountBM accountBM, string strFormType, DataTable dtDeskAircraft, PositionNameBM deskNameBM)
        {
            InitializeComponent();

            //��������
            this.FormType = strFormType;
            //�û���Ϣ
            this.AccountBM = accountBM;
            //ϯλ����
            this.DeskNameBM = deskNameBM;
            //��ʾ��������
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(this.AccountBM, strFormType);
            this.DisplayDataItems = rvSF.Dt;
            //��ѯ��ϯλ���зɻ�
            this.DeskAircrafts = dtDeskAircraft;
            //��ʼ��ϯλ���ද̬
            InitialDeskFlights(this.FormType, this.DeskAircrafts, this.DisplayDataItems);
        }
        #endregion

        #region ���봰��
        /// <summary>
        /// ���봰��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmFlightMoni_Load(object sender, EventArgs e)
        {
            //������ͼ
            fpsFlightMon.Sheets[0].RowHeader.Columns[0].Width = 50;
            timerChangeRecord.Interval = this.AccountBM.RefreshInterval * 1000;
            SpreadGrid spreadGrid = new SpreadGrid(this.AccountBM);
            spreadGrid.SetView(shFlightMon, this.DisplayDataItems, 0);

            BindMainView();

            //���û���ˢ��Ƶ��д�������ļ�
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

        #region ˫����Ԫ���¼�
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

        #region ������ͼ
        public void BindMainView()
        {
            //������ʾ��
            DataTable dtDeskFlights = this.DisplayDeskFlights;
            DataSet dsDeskFlights = new DataSet();
            dsDeskFlights.Tables.Add(dtDeskFlights);

            //������Դ
            fpsFlightMon.DataSource = dsDeskFlights;
            fpsFlightMon.DataMember = dsDeskFlights.Tables[0].TableName;

            //��ʼ�������ɫ
            this.InitialGridColor(shFlightMon);
            //���õ�Ԫ����ɫ
            this.SetGridColor(this.DisplayDeskFlights, this.DeskFlights, this.DisplayDataItems, fpsFlightMon, this.FormType);
        }
        #endregion
    }
}