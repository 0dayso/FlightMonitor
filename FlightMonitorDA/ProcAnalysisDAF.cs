using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.AgentServiceBM;

namespace AirSoft.FlightMonitor.AgentServiceDA
{
    /// <summary>
    /// ���̷�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-11-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ProcAnalysisDAF
    {
        #region ���ɹ��̷�����
        /// <summary>
        /// ���ɹ��̷�����
        /// </summary>
        /// <returns>���ع��̷��������</returns>
        public DataTable CreateDatatable()
        {
            #region ��������
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null;

            #endregion


            #region ����ʵ��
            //���ɱ��
            try
            {
                dataColumn = new DataColumn("cniProcAnalysisId", Type.GetType("System.Int32"));
                dataColumn.AutoIncrement = true;
                dataColumn.AutoIncrementSeed = 1;
                dataColumn.AutoIncrementStep = 1;
                dataColumn.Caption = "���";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcProcName", Type.GetType("System.String"));
                dataColumn.Caption = "������";
                dataColumn.Unique = true;
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndLastOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "������ʱ��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcLastOprationResult", Type.GetType("System.String"));
                dataColumn.Caption = "���������";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniOprationCount", Type.GetType("System.Int32"));
                dataColumn.Caption = "���ô���";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndCountStartTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "������ʼʱ��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnlTotalLengthBeforeCompress", Type.GetType("System.Int64"));
                dataColumn.Caption = "ѹ��֮ǰ��С������byte��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnlTotalLengthAfterCompress", Type.GetType("System.Int64"));
                dataColumn.Caption = "ѹ��֮���С������byte��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfTotalProcTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "����ִ��ʱ���������룩";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfTotalCompressTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "ѹ��ʱ���������룩";
                dataTable.Columns.Add(dataColumn);

                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["cnvcProcName"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //���ؽ��
            return dataTable;

            #endregion
        }
        #endregion

        #region ���¼�¼
        /// <summary>
        /// ���¼�¼
        /// </summary>
        /// <param name="dtProcAnalysis">���̷��������</param>
        /// <param name="procAnalysisBM">Ҫ�޸ĵĽ��</param>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        public int UpdateRecord(DataTable dtProcAnalysis, ProcAnalysisBM procAnalysisBM)
        {
            #region ��������
            int retVal = -1;

            #endregion


            #region ����ʵ��
            //��Ӽ�¼
            try
            {
                dtProcAnalysis.BeginLoadData();
                object[] arrObject = new object[] {null,procAnalysisBM.ProcName,procAnalysisBM.LastOprationTime,
                    procAnalysisBM.LastOprationResult,procAnalysisBM.OprationCount,procAnalysisBM.CountStartTime,
                    procAnalysisBM.TotalLengthBeforeCompress,procAnalysisBM.TotalLengthAfterCompress,
                    procAnalysisBM.TotalProcTimes,procAnalysisBM.TotalCompressTime};
                dtProcAnalysis.LoadDataRow(arrObject, true);
                dtProcAnalysis.EndLoadData();

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //���ؽ��
            return retVal;

            #endregion
        }
        #endregion

        #region ��������

        #region ���¼�¼
        /// <summary>
        /// ���¼�¼
        /// </summary>
        /// <param name="dtProcAnalysis">���̷��������</param>
        /// <param name="procAnalysisBM">Ҫ�޸ĵĽ��</param>
        /// <param name="SynchronizeLock">ͬ����</param>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        public int UpdateRecord(DataTable dtProcAnalysis, ProcAnalysisBM procAnalysisBM, object SynchronizeLock)
        {
            #region ��������
            int retVal = -1;

            #endregion


            #region ����ʵ��
            //��Ӽ�¼
            try
            {
                lock (SynchronizeLock)
                {
                    dtProcAnalysis.BeginLoadData();
                    object[] arrObject = new object[] {null,procAnalysisBM.ProcName,procAnalysisBM.LastOprationTime,
                    procAnalysisBM.LastOprationResult,procAnalysisBM.OprationCount,procAnalysisBM.CountStartTime,
                    procAnalysisBM.TotalLengthBeforeCompress,procAnalysisBM.TotalLengthAfterCompress,
                    procAnalysisBM.TotalProcTimes,procAnalysisBM.TotalCompressTime};
                    dtProcAnalysis.LoadDataRow(arrObject, true);
                    dtProcAnalysis.EndLoadData();
                }

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //���ؽ��
            return retVal;

            #endregion
        }
        #endregion

        #region ���� ���� ��¼ͳ�Ʊ��������и��µİ취
        /// <summary>
        /// ���� ���� ��¼ͳ�Ʊ��������и��µİ취
        /// </summary>
        /// <param name="dtProcAnalysis">������ ���� dtProcAnalysis</param>
        /// <param name="dtProcAnalysis_DAF">AgentServiceDAF.dtProcAnalysis</param>
        /// <param name="SynchronizeLock">ͬ����</param>
        /// <returns></returns>
        public int SynchronizeDatas(DataTable dtProcAnalysis, DataTable dtProcAnalysis_DAF, object SynchronizeLock)
        {
            #region ��������
            int retVal = -1;
            int iCountsOfProcAnalysis_DAF = 0;

            #endregion


            #region ���ʵ��
            try
            {
                lock (SynchronizeLock)
                {
                    iCountsOfProcAnalysis_DAF = dtProcAnalysis_DAF.Rows.Count;
                    for (int iIndex = 0; iIndex < iCountsOfProcAnalysis_DAF; iIndex++)
                    {
                        ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(dtProcAnalysis_DAF.Rows[iIndex]);
                        UpdateRecord(dtProcAnalysis, procAnalysisBM);
                    }
                }

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //���ؽ��
            return retVal;

            #endregion

        }
        #endregion

        #endregion

    }
}
