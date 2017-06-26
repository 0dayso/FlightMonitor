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
        #region ����

        private AccountBM m_AccountBM;
        /// <summary>
        /// �û���Ϣʵ��
        /// </summary>
        public AccountBM AccountBM
        {
            get { return m_AccountBM; }
            set { m_AccountBM = value; }
        }

        private DataTable m_dtDisplayDataItems;
        /// <summary>
        /// �û�������ʾ��������
        /// </summary>
        public DataTable DisplayDataItems
        {
            get { return m_dtDisplayDataItems; }
            set { m_dtDisplayDataItems = value; }
        }

        //private DataTable m_dtDataItemsPurview;
        ///// <summary>
        ///// �û����е�������
        ///// </summary>
        //public DataTable DataItemPurview
        //{
        //    get { return m_dtDataItemsPurview; }
        //    set { m_dtDataItemsPurview = value; }
        //}

        private PositionNameBM m_DeskNameBM;
        /// <summary>
        /// �û�ϯλ��Ϣʵ��
        /// </summary>
        public PositionNameBM DeskNameBM
        {
            get { return m_DeskNameBM; }
            set { m_DeskNameBM = value; }
        }

        private DataTable m_dtDeskFlights;
        /// <summary>
        /// ϯλ�������еĺ��ࣨ�����ݿ��еļ�¼��ͬ��
        /// </summary>
        public DataTable DeskFlights
        {
            get { return m_dtDeskFlights; }
            set { m_dtDeskFlights = value; }
        }

        private DataTable m_dtDisplayDeskFlights;
        /// <summary>
        /// ϯλ���ද̬��������֯�����ʾ��ͼ��
        /// </summary>
        public DataTable DisplayDeskFlights
        {
            get { return m_dtDisplayDeskFlights; }
            set { m_dtDisplayDeskFlights = value; }
        }

        private DataTable m_dtDeskAircrafts;
        /// <summary>
        /// ϯλ���еķɻ�
        /// </summary>
        public DataTable DeskAircrafts
        {
            get { return m_dtDeskAircrafts; }
            set { m_dtDeskAircrafts = value; }
        }

        private DataTable m_dtChangeTable = new DataTable();
        /// <summary>
        /// ������Ϣ�仯�б�
        /// </summary>
        public DataTable ChangeRecordTable
        {
            get { return m_dtChangeTable; }
            set { m_dtChangeTable = value; }
        }
        //
        //��˸ǰ��Ԫ����ɫ
        //
        Color[,] colorArrOldBackGround;

        private int m_iLastRecordNo;
        /// <summary>
        /// ��������
        /// </summary>
        public int LastRecordNo
        {
            get { return m_iLastRecordNo; }
            set { m_iLastRecordNo = value; }
        }

        DataTable m_dtSplashTag;
        /// <summary>
        /// ��˸��ʶ��
        /// </summary>
        public DataTable SplashTagTable
        {
            get { return m_dtSplashTag; }
            set { m_dtSplashTag = value; }
        }

        private string m_strFormType;
        /// <summary>
        /// ��������
        /// </summary>
        public string FormType
        {
            get { return m_strFormType; }
            set { m_strFormType = value; }
        }
        #endregion

        #region ���췽��
        public FlightDispInfo()
        { }
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="dataItems"></param>
        /// <param name="positionNameBM"></param>
        public FlightDispInfo(AccountBM accountBM, string strFormType, DataTable dtDeskAircraft)
        {
            //��������
            this.m_strFormType = strFormType;
            //�û���Ϣ
            this.m_AccountBM = accountBM;
            //��ʾ��������
            DataItemPurviewBF dataItemPurviewBF = new DataItemPurviewBF();
            ReturnValueSF rvSF = dataItemPurviewBF.GetVisibleDataItem(m_AccountBM, strFormType);
            this.m_dtDisplayDataItems = rvSF.Dt;
            //��ϯλ���зɻ�
            this.m_dtDeskAircrafts = dtDeskAircraft;
            //��ʼ��ϯλ���ද̬
            InitialDeskFlights(this.m_strFormType, this.m_dtDeskAircrafts, this.m_dtDisplayDataItems);
        }
        #endregion

        #region ��ȡ��½�û�������������Ȩ��
        /// <summary>
        /// ��ȡ��½�û���������Ȩ�ޣ�Ȩ�ޡ���ʾ��
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return dt;
        }
        #endregion

        #region ��ȡĳϯλ�����к��ද̬
        /// <summary>
        /// ��ȡĳϯλ�����к��ද̬
        /// </summary>
        public DataTable GetFlightsByDesk(DateTimeBM dateTimeBM, string strFormType)
        {
            //����ҵ����۲㷽��
            FlightDispBF flightDispBF = new FlightDispBF();
            ReturnValueSF rvSF = flightDispBF.GetFlightsByDesk(dateTimeBM, strFormType);
            DataTable dtDeskFlights = new DataTable();
            if (rvSF.Result > 0)
            {
                dtDeskFlights = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return dtDeskFlights;
        }
        #endregion

        #region ��ȡҪ��ʾ��ϯλ���ද̬��ܹ�
        /// <summary>
        /// ��ȡҪ��ʾ��ļܹ�
        /// </summary>
        private DataTable GenDisplaySchema(string strFormType)
        {
            DataTable dtDisplayTable = new DataTable();
            //����
            dtDisplayTable.Columns.Add(strFormType + "cncDATOP");
            dtDisplayTable.Columns.Add(strFormType + "cnvcFLTID");
            dtDisplayTable.Columns.Add(strFormType + "cniLEGNO");
            dtDisplayTable.Columns.Add(strFormType + "cnvcAC");
            //�����ֶΣ��ƻ����ʱ��
            dtDisplayTable.Columns.Add(strFormType + "cncSTDSort");
            dtDisplayTable.Columns.Add(strFormType + "cniViewIndex");
            //����Ҫ��ʾ���ֶ�����ϯλ���ද̬��ļܹ�
            foreach (DataRow dataRow in m_dtDisplayDataItems.Rows)
            {
                dtDisplayTable.Columns.Add(dataRow["cnvcDataItemID"].ToString());
            }
            return dtDisplayTable;
        }
        #endregion

        #region ������˸��ʶ�����Ҳ����¼������ֵ
        /// <summary>
        /// ������˸��ʶ�����Ҳ����¼������ֵ
        /// </summary>
        /// <param name="dtDeskFlights"></param>
        /// <returns></returns>
        public DataTable FillSplashTable(DataTable dtDeskFlights)
        {
            //������˸��ʶ��
            DataTable dtSplashTag = new DataTable();
            if (dtDeskFlights != null)
            {
                //�����
                for (int iLoop = 0; iLoop < dtDeskFlights.Columns.Count; iLoop++)
                {
                    ArrayList alColName = new ArrayList();
                    alColName.Add("cncDATOP");
                    alColName.Add("cnvcFLTID");
                    alColName.Add("cnvcAC");
                    string strColName = dtDeskFlights.Columns[iLoop].ColumnName;
                    //�����е���������Ϊstring
                    if (alColName.Contains(strColName))
                    {
                        dtSplashTag.Columns.Add(strColName, System.Type.GetType("System.String"));
                    }
                    //�����е���������Ϊint
                    else
                    {
                        dtSplashTag.Columns.Add(strColName, System.Type.GetType("System.Int32"));
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
                foreach (DataRow drDeskFlight in dtDeskFlights.Rows)
                {
                    DataRow drSplash = dtSplashTag.NewRow();
                    //��������ֵ
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

        #region ��亽�ද̬�б�
        /// <summary>
        /// ��亽�ද̬�б�
        /// </summary>
        /// <param name="dtDeskAircrafts"></param>
        /// <param name="dtDeskFlights"></param>
        /// <returns></returns>
        public DataTable FillFlightInfo(DataTable dtDeskAircrafts, DataTable dtDeskFlights, string strFormType, DataTable dtDisplayDataItems)
        {
            //���ɱ�Ľṹ
            DataTable dtFlightInfo = GenDisplaySchema(strFormType).Clone();
            if (dtDeskFlights != null)
            {
                //���ÿ�ܷɻ����д���
                foreach (DataRow drDeskAircraft in dtDeskAircrafts.Rows)
                {
                    //�ɻ���
                    string strLongReg = drDeskAircraft["cnvcLONG_REG"].ToString();
                    //�üܷɻ������к���
                    DataRow[] drFlights = dtDeskFlights.Select("cnvcLONG_REG = '" + strLongReg + "'", "cncSTD");
                    if (drFlights.Length > 0)
                    {
                        //���ÿ��������д���
                        foreach (DataRow dr in drFlights)
                        {
                            DataRow drFlightInfo = dtFlightInfo.NewRow();
                            //����
                            drFlightInfo[strFormType + "cncDATOP"] = dr["cncDATOP"].ToString();
                            drFlightInfo[strFormType + "cnvcFLTID"] = dr["cnvcFLTID"].ToString();
                            drFlightInfo[strFormType + "cniLEGNO"] = dr["cniLEGNO"].ToString();
                            drFlightInfo[strFormType + "cnvcAC"] = dr["cnvcAC"].ToString();
                            //�����ֶΣ��ƻ����ʱ��
                            drFlightInfo[strFormType + "cncSTDSort"] = dr["cncSTD"].ToString();
                            drFlightInfo[strFormType + "cniViewIndex"] = dr["cniViewIndex"].ToString();
                            //���û�����Ҫ��ʾ��ÿ���ֶθ�ֵ
                            foreach (DataRow drDataItem in dtDisplayDataItems.Rows)
                            {
                                //��������tbFlightDispInfo���е��ֶ�����
                                string strFieldName = drDataItem["cnvcPrimaryNameField"].ToString();
                                //�ֶε�ֵ
                                string strFieldValue = dr[strFieldName].ToString().Trim();
                                //����������
                                string strFieldId = drDataItem["cnvcDataItemID"].ToString();

                                //�������ֶε�ֵ��ʽ��
                                //�Ժ���ʱ�̸�ʽ��
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
                                //�Ի������Ƹ�ʽ��
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

                                //���ֶθ�ֵ
                                drFlightInfo[strFieldId] = strFieldValue;
                            }//foreach (DataRow drDataItem in dtDisplayDataItems.Rows)
                            dtFlightInfo.Rows.Add(drFlightInfo);
                        }//foreach (DataRow dr in drFlights)
                    }//if (drFlights.Length > 0)
                }//foreach (DataRow drDeskAircraft in dtDeskAircrafts.Rows)
            }//if (dtDeskFlights != null)

            //����
            //���ڷ��н���ĺ��࣬��STD��������
            if (strFormType == "Dsp")
            {
                dtFlightInfo.DefaultView.Sort = strFormType + "cncSTDSort";
            }
            //���ڼ�ؽ���ĺ��࣬���Ȱ�����״̬���У���ΰ�STD����
            else if(strFormType == "Mon")
            {
                dtFlightInfo.DefaultView.Sort = strFormType + "cniViewIndex desc, " + strFormType + "cncSTDSort";
            }
            return dtFlightInfo.DefaultView.ToTable();
        }
        #endregion

        #region �����վʱ�䡢���ز�ʱ���
        /// <summary>
        /// �����վʱ�䡢���ز�ʱ��
        /// </summary>
        //private void ComputeFlightsInfor(DataTable dtDeskFlights, AccountBM accountBM)
        //{
        //    int iRowIndex = 0;
        //    foreach (DataRow drDeskFlight in dtDeskFlights.Rows)
        //    {
        //        //���㿪�ز�ʱ��
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
        //        //�жϻ������Ƿ�Ϊ��
        //        if (drDeskFlight["cncDEPAirportCNAME"].ToString() == "")
        //        {
        //            drDeskFlight["cncDEPAirportCNAME"] = drDeskFlight["cncDEPCityThreeCode"].ToString();
        //        }
        //        if (drDeskFlight["cncARRAirportCNAME"].ToString() == "")
        //        {
        //            drDeskFlight["cncARRAirportCNAME"] = drDeskFlight["cncARRCityThreeCode"].ToString();
        //        }
        //        //�ж��Ƿ�û����ض�̬
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
        //        //�ж��Ƿ�����ɶ�̬
        //        //û����ɶ�̬�澯
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
        //        //��׼��վʱ��
        //        string strDEPAirportPaxType = drDeskFlight["cniDEPAirportPaxType"].ToString().Trim();
        //        if (strDEPAirportPaxType == "")
        //        {
        //            strDEPAirportPaxType = "1";
        //        }
        //        //�ж��Ƿ��վ����
        //        DataRow[] drStandardIntermission = dtStandardIntermissionTime.Select("cncACTYPE = '" + drDeskFlight["cncACTYP"].ToString() + "' AND cniAirportPaxType = " + strDEPAirportPaxType);
        //        TimeSpan tsIntermissionTime = new TimeSpan(0, 0, 0);
        //        if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() == dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
        //        {
        //            //�����վʱ��
        //            tsIntermissionTime = DateTime.Parse(drDeskFlight["cncETD"].ToString()).Subtract(DateTime.Parse(dtDeskFlights.Rows[iRowIndex - 1]["cncTDWN"].ToString()));
        //            drDeskFlight["cniIntermissionTime"] = tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes;
        //            if (drStandardIntermission.Length > 0)
        //            {
        //                if (drDeskFlight["cncDEPIsSelf"].ToString().Trim() == "1" && drDeskFlight["cncARRIsSelf"].ToString().Trim() == "1") //���ں���
        //                {
        //                    if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniDomIntermissionTime"].ToString()))
        //                    {
        //                        drDeskFlight["cniNotEnoughIntermissionTime"] = 1;
        //                    }
        //                }
        //                else   //���ʺ���
        //                {
        //                    if (tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes + m_accountBM.IntermissionMinutes < Convert.ToInt32(drStandardIntermission[0]["cniInterIntermissionTime"].ToString()))
        //                    {
        //                        drDeskFlight["cniNotEnoughIntermissionTime"] = 1;
        //                    }
        //                }//else
        //            }//if (drStandardIntermission.Length > 0)
        //        }//if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() == dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())

        //        //�����������ʱ��
        //        //ʼ������                
        //        if (iRowIndex == 0 || drDeskFlight["cnvcLONG_REG"].ToString() != dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
        //        {
        //            if (drDeskFlight["cnvcDELAY1"].ToString() != "")
        //            {
        //                drDeskFlight["cniDischargingDelayTime"] = drDeskFlight["cniDUR1"].ToString();
        //            }
        //        }
        //        //��վ����
        //        else if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() == dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString())
        //        {
        //            if (drStandardIntermission.Length > 0)
        //            {
        //                //�����վʱ��
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
        //            //�ж��Ƿ�û�йز�ʱ��
        //            if (iRowIndex != 0 && drDeskFlight["cnvcLONG_REG"].ToString() != dtDeskFlights.Rows[iRowIndex - 1]["cnvcLONG_REG"].ToString() && tsIntermissionTime.Hours * 60 + tsIntermissionTime.Minutes > 180) //��վ����
        //            {
        //                if (DateTime.Now.AddMinutes(Convert.ToInt32(drStandardIntermission[0]["cniIntermissionCloseCabinTime"].ToString())) > DateTime.Parse(drDeskFlight["cncETD"].ToString()))
        //                {
        //                    drDeskFlight["cniNotClosePaxCabineTime"] = 1;
        //                }
        //            }
        //            else  //ʼ������
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

        #region ���ñ����ɫ
        /// <summary>
        /// ���ñ����ɫ
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
                        //����״̬
                        string strFlightStatus = drDeskFlight[0]["cncSTATUS"].ToString();
                        //�������������򽫸��б���ɫ��Ϊ����ɫ
                        if (strFlightStatus == "ATA" || strFlightStatus == "ARR")
                        {
                            SetRowCellColor(fpsFlights, iRowIndex, Color.Silver);
                            fpsFlights.Sheets[0].Rows[iRowIndex].BackColor = Color.Silver;
                        }
                        else
                        {
                            foreach (DataRow drDataItem in dtDisplayDataItems.Rows)
                            {
                                //����������
                                string strFieldName = drDataItem["cnvcDataItemID"].ToString();
                                //����������ͼ�е��к�
                                int iColumnIndex = Convert.ToInt32(drDataItem["cniViewIndex"].ToString());

                                #region ����ŵ�Ԫ����ɫ
                                //����ŵ�Ԫ��
                                if (strFieldName == strFormType + "cnvcFlightNo")
                                {
                                    //����������������ŵ�Ԫ�񱳾�ɫ=Yellow
                                    if (drDeskFlight[0]["cnvcDIV_RCODE"].ToString() != "" || Convert.ToInt32(drDeskFlight[0]["cniLEGNO"].ToString()) % 100 != 0)
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.Yellow;
                                    }
                                    //�ص㱣�Ϻ��ࣺ����ŵ�Ԫ�񱳾�ɫ=Yellow
                                    //if (drDeskFlight[0]["cniFocusTag"].ToString().Trim() != "")
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.Orange;
                                    //    fpsFlightDisp.Sheets[0].SetNote(iRowIndex, iColumnIndex, drDeskFlight[0]["cniFocusTag"].ToString().Trim());
                                    //}
                                    //else
                                    //{
                                    //    fpsFlightDisp.Sheets[0].SetNote(iRowIndex, iColumnIndex, "");
                                    //}
                                    //��վʱ�䲻�㣺����ŵ�Ԫ�񱳾�ɫ=Plum
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

                                #region �������ʵ�Ԫ����ɫ
                                //��������
                                else if (strFieldName == strFormType + "cnvcFlightCharacterAbbreviate")
                                {
                                    //�������ʲ������ࣺ�������ʵ�Ԫ��ǰ��ɫ=Red
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

                                #region ������������µ�Ԫ����ɫ����
                                //��������
                                else if (strFieldName == strFormType + "cncETA" || strFieldName == strFormType + "cncETD")
                                {
                                    //��������ETA��ETD��Ԫ�񱳾�ɫ=Yellow
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

                                #region ���ʱ�䵥Ԫ����ɫ
                                //���ʱ��
                                else if (strFieldName == strFormType + "cncTOFF")
                                {
                                    //û����ɶ�̬�澯
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

                                #region ����ʱ�䵥Ԫ����ɫ
                                //����ʱ��
                                else if (strFieldName == strFormType + "cncTDWN")
                                {
                                    //��������ɣ�����ʱ�䵥Ԫ�񱳾�ɫ=LimeGreen
                                    string strTemp = drDeskFlight[0]["cncSTATUS"].ToString().Trim();
                                    if (drDeskFlight[0]["cncSTATUS"].ToString().Trim() == "DEP")
                                    {
                                        fpsFlights.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.LimeGreen;
                                    }
                                    //û�е��ﶯ̬�澯
                                    //if (m_accountBM.TDWNPromt == 1 && drDeskFlight[0]["cniNotTDWN"].ToString() == "1")
                                    //{
                                    //    fpsFlightDisp.Sheets[0].Cells[iRowIndex, iColumnIndex].BackColor = Color.SkyBlue;
                                    //}
                                }
                                #endregion

                                //����
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

        #region ����ĳ�е�Ԫ����ɫ
        /// <summary>
        /// ����ĳ�е�Ԫ����ɫ
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

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public int GetMaxRecordNo()
        {
            //����ҵ����۲㷽��
            ChangeRecordBF changeRecordBF = new ChangeRecordBF();
            ReturnValueSF rvSF = changeRecordBF.GetMaxRecordNo();
            int iLastRecordNo = 0;
            if (rvSF.Result > 0)
            {
                iLastRecordNo = rvSF.Result;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return iLastRecordNo;
        }
        #endregion

        #region ������˸ʱ��
        ///// <summary>
        ///// ���ݺ�����������˸ʱ��
        ///// </summary>
        ///// <param name="drFlightChange">��������¼</param>
        ///// <param name="drSplashTag">��˸��Ǽ�¼</param>
        ///// <param name="iChangeMode">������ģʽ</param>
        ///// iChangeMode = 0������Ҫˢ����ͼ
        ///// iChangeMode = 1����Ҫˢ����ͼ����������
        ///// iChangeMode = 2����Ҫˢ����ͼ�������¼�������
        //private void SetSplash(DataRow[] drSplashTag, DataRow drFlightChange, int iChangeMode)
        //{
        //    int iSplash = 0;

        //    //��ѯ����ĺ����Ƿ�����ʾ��ϯλ���ද̬��
        //    string strSearch = "DspcncDATOP = '" + drFlightChange["cncOldDATOP"].ToString() + "' AND " +
        //        "DspcnvcFLTID = '" + drFlightChange["cnvcOldFLTID"].ToString() + "' AND " +
        //        "DspcniLEGNO = " + drFlightChange["cniOldLEGNO"].ToString() + " AND " +
        //        "DspcnvcAC = '" + drFlightChange["cnvcOldAC"].ToString() + "'";
        //    DataRow[] drSplashFlights = m_dtDisplayDeskFlights.Select(strSearch);

        //    //�������ĺ�����ʾ��ϯλ���ද̬��
        //    if (drSplashFlights.Length > 0)
        //    {
        //        //��ѯ�漰������������Ƿ���ʾ
        //        strSearch = "cnvcDataItemID = 'Dsp" + drFlightChange["cnvcChangeReasonCode"].ToString() + "'";
        //        DataRow[] drChangeDataItem = m_dtDisplayDataItems.Select(strSearch);
        //        //������ڣ�����˸�����Ϊ1
        //        if (drChangeDataItem.Length > 0)
        //        {
        //            iSplash = 1;
        //        }
        //    }

        //    //�ж��漰������������Ƿ���������˸
        //    strSearch = "cnvcPrimaryCodeField = '" + drFlightChange["cnvcChangeReasonCode"].ToString() + "' AND cniSplashPromptItem = 1";
        //    DataRow[] drDataItemSplash = m_dtDataItemsPurview.Select(strSearch);
        //    //����������������
        //    string strChangedField = drFlightChange["cnvcChangeReasonCode"].ToString();

        //    //�����Ҫ��˸��ʾ
        //    if (drDataItemSplash.Length > 0)
        //    {
        //        switch (iChangeMode)
        //        {
        //            case 0:
        //                //������˸ʱ��
        //                if (iSplash == 1)
        //                {
        //                    drSplashTag[0][strChangeField] = m_AccountBM.SplashSeconds;
        //                }
        //                break;
        //            case 1:
        //                if (iSplash == 1)
        //                {
        //                    //������˸��¼
        //                    DataRow drSplash = m_dtSplashTag.NewRow();
        //                    drSplash["cncDATOP"] = drNewFlight["cncDATOP"].ToString();
        //                    drSplash["cnvcFLTID"] = drNewFlight["cnvcFLTID"].ToString();
        //                    drSplash["cniLEGNO"] = drNewFlight["cniLEGNO"].ToString();
        //                    drSplash["cnvcAC"] = drNewFlight["cnvcAC"].ToString();
        //                    drSplash["cnvcLONG_REG"] = m_AccountBM.SplashSeconds.ToString();
        //                    //���¼�¼��ӵ�������Ϣ�����˸����
        //                    m_dtSplashTag.Rows.Add(drSplash);
        //                }
        //                break;
        //            case 2:
        //                //��������������ΪETA����ETD
        //                if (strChangedField == "cncETA" || strChangedField == "cncETD")
        //                {
        //                    //�������ԭ��Ϊ��AND��˸���=1
        //                    //���������û������ETA��ETD�仯����˸
        //                    if (dtChangeLegs.Rows[0]["cnvcDELAY1"].ToString() != "" && iSplash == 1)
        //                    {
        //                        //������˸ʱ��
        //                        drSplashTag[0][strChangedField] = m_AccountBM.SplashSeconds.ToString();
        //                    }
        //                }
        //                //�����˸���=1
        //                else if (iSplash == 1)
        //                {
        //                    //��������������Ϊ����״̬
        //                    if (strChangedField == "cncSTATUS")
        //                    {
        //                        //�������״̬��ΪDEP
        //                        if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEP")
        //                        {
        //                            //�������ʱ������ʱ�䵥Ԫ��Ҳ��˸
        //                            drSplashTag[0]["cncTDWN"] = m_AccountBM.SplashSeconds;
        //                            drSplashTag[0]["cncTOFF"] = m_AccountBM.SplashSeconds;
        //                        }

        //                        //�������״̬���ΪDEL
        //                        if (dtChangeLegs.Rows[0]["cncSTATUS"].ToString() == "DEL")
        //                        {
        //                            //�����Ԥ�����ʱ���Ԥ��ʱ��Ҳ��˸
        //                            drSplashTag[0]["cncETD"] = m_AccountBM.SplashSeconds;
        //                            drSplashTag[0]["cncETA"] = m_AccountBM.SplashSeconds;
        //                        }
        //                    }
        //                }
        //                //�������������������˸ʱ��
        //                drSplashTag[0][strChangedField] = m_AccountBM.SplashSeconds;
        //                break;
        //            default:
        //                break;
        //        }//end switch
        //    }//end if (drDataItemSplash.Length > 0)
        //}
        #endregion

        #region ��˸����
        /// <summary>
        /// ��˸����
        /// </summary>
        /// <param name="dtDisplayDataItems">�û�������ʾ��������</param>
        /// <param name="dtSplashTagTable">��˸��ʶ��</param>
        /// <param name="dtDisplayDeskFlights">��ʾ��ϯλ���ද̬��</param>
        /// <param name="dtDeskFlights">ԭʼϯλ���ද̬��</param>
        /// <param name="fpsFlightDisp">����˸�Ĵ���</param>
        public void Splash(DataTable dtDisplayDataItems, DataTable dtSplashTagTable, DataTable dtDisplayDeskFlights, FpSpread fpsFlightDisp, Color oldBackGroundColor)
        {
            try
            {
                string strDisplayDataItemSearch = "";

                //���ɲ�ѯ�ַ���
                //���ڲ�ѯ�û���������ʾ���ֶ�
                //ͬʱ���ֶ�ֵ����0������������˸ʱ�䣩
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
                //��ѯ��˸��ʶ���У��û���������ʾ��ͬʱ��������˸�ļ�¼
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
                //����Ŀ��Ҫ��˸����
                DataRow[] drDeskFlights = dtDisplayDeskFlights.Select(strSearch.ToString());

                //���б�������Ŀ��Ҫ��˸�Ľ����ۺ�����Ϣ��¼��
                foreach (DataRow drDeskFlight in drDeskFlights)
                {
                    int iRowIndex = 0;
                    //ȷ����Ҫ��˸��ʾ�ĺ������ڵ���
                    iRowIndex = dtDisplayDeskFlights.Rows.IndexOf(drDeskFlight);
                    //���д�����Ҫ��˸���ֶ�
                    for (int iLoop = 0; iLoop < fpsFlightDisp.Sheets[0].Columns.Count; iLoop++)
                    {
                        //�����ֶ���Ϣ
                        string strSearchDataItem = "cnvcDataItemID = '" + fpsFlightDisp.Sheets[0].Columns[iLoop].DataField + "'";
                        DataRow[] drDataItem = dtDisplayDataItems.Select(strSearchDataItem);
                        string strPrimaryCodeField = drDataItem[0]["cnvcPrimaryCodeField"].ToString();
                        string strDataItemName = drDataItem[0]["cnvcDataItemName"].ToString();

                        //���Һ�����Ϣ
                        strSearch.Remove(0, strSearch.Length);
                        strSearch.Append("(DspcncDATOP = '" + drDeskFlight["cncDATOP"].ToString() + "' AND ");
                        strSearch.Append("DspcnvcFLTID = '" + drDeskFlight["cnvcFLTID"].ToString() + "' AND ");
                        strSearch.Append("DspcniLEGNO = " + drDeskFlight["cniLEGNO"].ToString() + " AND ");
                        strSearch.Append("DspcnvcAC = '" + drDeskFlight["cnvcAC"].ToString() + "') OR ");
                        drSplashTag = dtSplashTagTable.Select(strSearch.ToString());

                        if (drSplashTag.Length > 0)
                        {
                            //��˸���м�¼����˸ʱ��
                            string strSplashTime = drSplashTag[0][strPrimaryCodeField].ToString().Trim();
                            if (strSplashTime != "")
                            {
                                //ת��������
                                int iSplashTime = Convert.ToInt32(strSplashTime);
                                if (iSplashTime > 0)
                                {
                                    #region ����ǵ�ǰʱ���������ż��
                                    //����ǵ�ǰʱ���������ż��
                                    if (DateTime.Now.Second % 2 == 0)
                                    {
                                        //����洢���ڴ��е���˸�����˸ʱ�����û����õ���˸ʱ����ͬ
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
                                        //��Ԫ����ɫ��Ϊ��ɫ
                                        fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor = Color.Red;
                                        //�����б�����˸
                                        //����б���Ϊ����
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.Red;
                                        }
                                        else
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.Red;
                                        }
                                        //����û������Զ�ֹͣ��˸��������������˸����ʱ��
                                        if (m_AccountBM.SplashAutoStop == 1 || iSplashTime == m_AccountBM.SplashSeconds)
                                        {
                                            //��˸����ʱ��ݼ�һ��
                                            drSplashTag[0][strPrimaryCodeField] = iSplashTime - 1;
                                        }
                                        //�����˸����ʱ��Ϊ0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //�ָ���Ԫ��ԭ������ɫ
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

                                    #region ����ǵ�ǰʱ�������������
                                    //����ǵ�ǰʱ�������������
                                    else if (DateTime.Now.Second % 2 == 1)
                                    {
                                        //����洢���ڴ��е���˸�����˸ʱ�����û����õ���˸ʱ����ͬ
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
                                        //�ָ�����Ԫ��ԭ������ɫ
                                        fpsFlightDisp.Sheets[0].Cells[iRowIndex, iLoop].BackColor = colorArrOldBackGround[iRowIndex, iLoop];
                                        //�б���
                                        if (strDataItemName.IndexOf("|") >= 0)
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[1, iLoop].BackColor = Color.FromName("Control");
                                        }
                                        else
                                        {
                                            fpsFlightDisp.Sheets[0].ColumnHeader.Cells[0, iLoop].BackColor = Color.FromName("Control");
                                        }
                                        //��˸����ʱ��ݼ�һ��
                                        if (m_AccountBM.SplashAutoStop == 1 || iSplashTime == m_AccountBM.SplashSeconds)
                                        {
                                            drSplashTag[0][strPrimaryCodeField] = iSplashTime - 1;
                                        }
                                        //�������ʱ��Ϊ0
                                        if (Convert.ToInt32(drSplashTag[0][strPrimaryCodeField].ToString()) == 0)
                                        {
                                            //�ָ�ԭ������ɫ
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

        #region ��ʼ����񱳾�ɫ
        /// <summary>
        /// ��ʼ����񱳾�ɫ
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

        #region ��ʼ��ϯλ���ද̬
        /// <summary>
        /// ��ʼ��ϯλ���ද̬
        /// </summary>
        /// <param name="deskNameBM">ϯλ����</param>
        /// <param name="FormType">�������ͣ�Dsp/Mon</param>
        /// <param name="dtDeskAircrafts">ϯλ�ķɻ�</param>
        /// <param name="dtDeskFlights">ϯλ�ĺ���</param>
        public void InitialDeskFlights(string FormType, DataTable dtDeskAircrafts, DataTable dtDisplayDataItems)
        {
            //���ý����ʱ�䷶Χ
            DateTimeBM dateTimeBM = GetDateTimeBM(1);
            //��ѯ��ϯλ���к���
            this.DeskFlights = GetFlightsByDesk(dateTimeBM, FormType);
            //������ʾ�ĺ��ද̬��ͼ
            this.DisplayDeskFlights = FillFlightInfo(dtDeskAircrafts, this.DeskFlights, FormType, dtDisplayDataItems);
            //������˸��ʶ��
            this.SplashTagTable = FillSplashTable(this.DeskFlights);
            //��ѯ�������ʶ
            this.LastRecordNo = GetMaxRecordNo();
        }
        #endregion

        #region ��ȡĳ��ʱ�䷶Χ
        /// <summary>
        /// ��ȡĳ��ʱ�䷶Χ
        /// </summary>
        /// <param name="iDay">��һ��0:����1:����2:����3:ѡ�������</param>
        public DateTimeBM GetDateTimeBM(int iDay)
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

        #region �鿴��ά����Ԫ������
        /// <summary>
        /// �鿴��ά����Ԫ������
        /// </summary>
        /// <param name="e">��Ԫ��</param>
        /// <param name="fpsFlightDisp">��ͼ�ؼ�</param>
        /// <param name="dtDataItemPurview">�û�Ȩ���б�</param>
        /// <param name="dtDeskFlights">ϯλ���ද̬</param>
        /// <param name="dtDisplayDeskFlights">ϯλ��ʾ�ĺ��ද̬</param>
        /// <param name="strFormType">��������</param>
        /// <param name="accountBM">�û���Ϣ</param>
        public void Maintennance(FarPoint.Win.Spread.CellClickEventArgs e, FarPoint.Win.Spread.FpSpread fpsFlightDisp, DataTable dtDisplayDataItems, DataTable dtDeskFlights, DataTable dtDisplayDeskFlights, string strFormType, AccountBM accountBM, FlightDispInfo thisForm)
        {
            //����
            string strDataItemID = fpsFlightDisp.ActiveSheet.Columns[e.Column].DataField.ToString();
            //��ȡ�û�Ȩ����Ϣ
            DataRow[] drDataItemPurview = dtDisplayDataItems.Select("cnvcDataItemID = '" + strDataItemID + "'");
            //�ж��Ƿ���Ȩ��
            if (drDataItemPurview[0]["cnvcPrimaryCodeField"].ToString() != "cnbVIPTag")
            {
                if (drDataItemPurview[0]["cniDataItemPurview"].ToString() != "2")
                {
                    MessageBox.Show("�Բ�����û��Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //�û�Ȩ�޼���
            int iDataItemPurview = Convert.ToInt32(drDataItemPurview[0]["cniDataItemPurview"].ToString());
            //�ֶγ���
            int iFieldLength = Convert.ToInt32(drDataItemPurview[0]["cniFieldLength"].ToString());
            //ά������
            int iMainTainType = Convert.ToInt32(drDataItemPurview[0]["cniMaintenType"].ToString());
            //�ֶ����ͣ�1=�ı���2=����
            int iFieldType = Convert.ToInt32(drDataItemPurview[0]["cniFieldType"].ToString());
            //��˫����Ԫ���ӦtbGuaranteeInfo���е��ֶ�
            string strPrimaryCodeField = drDataItemPurview[0]["cnvcPrimaryCodeField"].ToString();

            //����ά��ʵ��
            MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
            //�������ʵ��
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            //���ද̬���ʵ��
            ChangeLegsBM changeLegsBM = new ChangeLegsBM();

            //�ֶ�����
            maintenGuaranteeInforBM.FieldType = iFieldType;
            //���ǰ��Ԫ���ֵ
            maintenGuaranteeInforBM.OldContent = fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text;
            //����
            maintenGuaranteeInforBM.ColumnCaption = drDataItemPurview[0]["cnvcDataItemName"].ToString();

            //���ද̬��Ϣ
            DataRow drFlightInfo = dtDisplayDeskFlights.Rows[e.Row];

            string strDATOP = drFlightInfo[strFormType + "cncDATOP"].ToString();
            string strFLTID = drFlightInfo[strFormType + "cnvcFLTID"].ToString();
            string strLEGNO = drFlightInfo[strFormType + "cniLEGNO"].ToString();
            string strAC = drFlightInfo[strFormType + "cnvcAC"].ToString();

            //ά����Ϣ
            maintenGuaranteeInforBM.DATOP = strDATOP;
            maintenGuaranteeInforBM.FLTID = strFLTID;
            maintenGuaranteeInforBM.LEGNO = strLEGNO;
            maintenGuaranteeInforBM.AC = strAC;
            maintenGuaranteeInforBM.FieldName = strPrimaryCodeField;
            maintenGuaranteeInforBM.FieldLength = iFieldLength;

            //������Ϣ
            changeLegsBM.DATOP = strDATOP;
            changeLegsBM.FLTID = strFLTID;
            changeLegsBM.LEGNO = Convert.ToInt32(strLEGNO);
            changeLegsBM.AC = strAC;

            //�����Ϣ
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

            #region �����ֶ����ͷֱ���
            //ʱ���ı���ά������=1
            if (iMainTainType == 1)
            {
                fmMaintenTime objfmMaintenTime = new fmMaintenTime(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenTime.ShowDialog() == DialogResult.OK)
                {
                    fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTime.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //�����ı���ά������=2
            else if (iMainTainType == 2)
            {
                fmMaitenSingleText objfmMaitenSingleText = new fmMaitenSingleText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaitenSingleText.ShowDialog() == DialogResult.OK)
                {
                    fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaitenSingleText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //�����ı���ά������=3
            else if (iMainTainType == 3)
            {
                fmMaintenMutiLineText objfmMaintenMutiLineText = new fmMaintenMutiLineText(maintenGuaranteeInforBM, changeRecordBM);
                if (objfmMaintenMutiLineText.ShowDialog() == DialogResult.OK)
                {
                    fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenMutiLineText.MMaintenGuaranteeInforBM.NewContent;
                }
            }
            //�����б�ά������=4
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
                #region ������4�������������������
                //��������мƻ�
                if (strPrimaryCodeField == "cniCFP")
                {
                    string strCFPId = drDeskFlight[0]["cniCFP"].ToString();
                    fmComputeFP obj = new fmComputeFP(maintenGuaranteeInforBM, changeLegsBM, strCFPId);
                    obj.ShowDialog();
                    if (obj.RefreshMainView == 1)
                    {
                        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = obj.PlanNo;

                        //��������˵籨
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

                //�ɻ�λ����Ϣ
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

                //����ACARS�籨
                if (strPrimaryCodeField == "cnvcACARSInMegs")
                {
                    fmACARSMegsList obj = new fmACARSMegsList(changeLegsBM);
                    obj.ShowDialog();
                }

                //����ACARS�籨
                if (strPrimaryCodeField == "cnvcACARSOutMegs")
                {
                    fmSendACARSMegs obj = new fmSendACARSMegs(changeLegsBM);
                    obj.ShowDialog();
                }

                //ҵ����Ϣ
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
                ////VIP���
                //if (strPrimaryCodeField == "cnbVIPTag")
                //{
                //    fmMaintenVIP objfmMaintenVIP = new fmMaintenVIP(changeLegsBM, m_accountBM, iDataItemPurview);
                //    objfmMaintenVIP.ShowDialog();
                //}
                ////ֵ������
                //else if (strPrimaryCodeField == "cniCheckNum" || strPrimaryCodeField == "cnvcBookNum" || strPrimaryCodeField == "cnvcInGATE")
                //{
                //    fmMaintenCheckPax objfmCheckPax = new fmMaintenCheckPax(changeLegsBM, changeRecordBM);
                //    if (objfmCheckPax.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmCheckPax.NewContent;
                //    }
                //}
                ////�ÿ�����
                //else if (strPrimaryCodeField == "cntPaxNameList")
                //{
                //    fmMaintenPaxNameList objfmMaintenPaxNameList = new fmMaintenPaxNameList(changeLegsBM, changeRecordBM);
                //    if (objfmMaintenPaxNameList.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenPaxNameList.NewContent;
                //    }
                //}
                ////��ת�����ÿ�
                //else if (strPrimaryCodeField == "cnbTransitPaxTag")
                //{
                //    fmMaintenTransitPax objfmMaintenTransitPax = new fmMaintenTransitPax(changeLegsBM, changeRecordBM);
                //    if (objfmMaintenTransitPax.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTransitPax.NewContent;
                //    }
                //}
                ////����ʱ��
                //else if (strPrimaryCodeField == "cncTDWN")
                //{
                //    changeRecordBM.Refresh = 1;
                //    fmMaintenTDWN objfmMaintenTDWN = new fmMaintenTDWN(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                //    if (objfmMaintenTDWN.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTDWN.NewContent;
                //    }
                //}
                ////���ʱ��
                //else if (strPrimaryCodeField == "cncTOFF")
                //{
                //    changeRecordBM.Refresh = 1;
                //    fmMaintenTOFF objfmMaintenTOFF = new fmMaintenTOFF(maintenGuaranteeInforBM, changeLegsBM, changeRecordBM);
                //    if (objfmMaintenTOFF.ShowDialog() == DialogResult.OK)
                //    {
                //        fpsFlightDisp.ActiveSheet.Cells[e.Row, e.Column].Text = objfmMaintenTOFF.NewContent;
                //    }
                //}
                ////������
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
