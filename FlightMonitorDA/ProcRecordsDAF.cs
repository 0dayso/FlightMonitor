using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.AgentServiceBM;

namespace AirSoft.FlightMonitor.AgentServiceDA
{
    /// <summary>
    /// ���̼�¼��
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-11-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ProcRecordsDAF
    {
        #region ���ɹ��̼�¼��
        /// <summary>
        /// ���ɹ��̼�¼��
        /// </summary>
        /// <returns>���ع��̼�¼�����</returns>
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
                dataColumn = new DataColumn("cniProcRecordsId", Type.GetType("System.Int32"));
                dataColumn.AutoIncrement = true;
                dataColumn.AutoIncrementSeed = 1;
                dataColumn.AutoIncrementStep = 1;
                dataColumn.Caption = "���";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcProcName", Type.GetType("System.String"));
                dataColumn.Caption = "������";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "����ʱ��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcOprationResult", Type.GetType("System.String"));
                dataColumn.Caption = "�������";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniOprationCount", Type.GetType("System.Int32"));
                dataColumn.Caption = "���ô���";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniLengthBeforeCompress", Type.GetType("System.Int32"));
                dataColumn.Caption = "ѹ��֮ǰ��С��byte��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniLengthAfterCompress", Type.GetType("System.Int32"));
                dataColumn.Caption = "ѹ��֮���С��byte��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfProcTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "����ִ��ʱ�䣨�룩";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnfCompressTimes", Type.GetType("System.Double"));
                dataColumn.Caption = "ѹ��ʱ�䣨�룩";
                dataTable.Columns.Add(dataColumn);
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
        
        #region ���һ����¼
        /// <summary>
        /// ���һ����¼
        /// </summary>
        /// <param name="dtProcRecords">���̼�¼�����</param>
        /// <param name="procRecordsBM">Ҫ��ӵļ�¼</param>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        public int AddRecord(DataTable dtProcRecords, ProcRecordsBM procRecordsBM)
        {
            #region ��������
            DataRow dataRow = null;
            int retVal = -1;

            #endregion


            #region ����ʵ��
            //��Ӽ�¼
            try
            {
                dataRow = dtProcRecords.NewRow();
                dataRow["cnvcProcName"] = procRecordsBM.ProcName;
                dataRow["cndOprationTime"] = procRecordsBM.OprationTime;
                dataRow["cnvcOprationResult"] = procRecordsBM.OprationResult;
                dataRow["cniOprationCount"] = procRecordsBM.OprationCount;
                dataRow["cniLengthBeforeCompress"] = procRecordsBM.LengthBeforeCompress;
                dataRow["cniLengthAfterCompress"] = procRecordsBM.LengthAfterCompress;
                dataRow["cnfProcTimes"] = procRecordsBM.ProcTimes;
                dataRow["cnfCompressTimes"] = procRecordsBM.CompressTimes;
                dtProcRecords.Rows.Add(dataRow);
                dtProcRecords.AcceptChanges();

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

        #region ���һ����¼
        /// <summary>
        /// ���һ����¼
        /// </summary>
        /// <param name="dtProcRecords">���̼�¼�����</param>
        /// <param name="procRecordsBM">Ҫ��ӵļ�¼</param>
        /// <param name="SynchronizeLock">ͬ����</param>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        public int AddRecord(DataTable dtProcRecords, ProcRecordsBM procRecordsBM, object SynchronizeLock)
        {
            #region ��������
            DataRow dataRow = null;
            int retVal = -1;

            #endregion


            #region ����ʵ��
            //��Ӽ�¼
            try
            {
                lock (SynchronizeLock)
                {
                    dataRow = dtProcRecords.NewRow();
                    dataRow["cnvcProcName"] = procRecordsBM.ProcName;
                    dataRow["cndOprationTime"] = procRecordsBM.OprationTime;
                    dataRow["cnvcOprationResult"] = procRecordsBM.OprationResult;
                    dataRow["cniOprationCount"] = procRecordsBM.OprationCount;
                    dataRow["cniLengthBeforeCompress"] = procRecordsBM.LengthBeforeCompress;
                    dataRow["cniLengthAfterCompress"] = procRecordsBM.LengthAfterCompress;
                    dataRow["cnfProcTimes"] = procRecordsBM.ProcTimes;
                    dataRow["cnfCompressTimes"] = procRecordsBM.CompressTimes;
                    dtProcRecords.Rows.Add(dataRow);
                    dtProcRecords.AcceptChanges();
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

        #region ���� ������ ���� ��¼��ϸ������׷�����¼�¼�İ취
        /// <summary>
        /// ���� ������ ���� ��¼��ϸ������׷�����¼�¼�İ취
        /// </summary>
        /// <param name="dtProcRecords">������ ���� dtProcRecords</param>
        /// <param name="dtProcRecords_DAF">AgentServiceDAF.dtProcRecords</param>
        /// <param name="SynchronizeLock">ͬ����</param>
        /// <returns></returns>
        public int SynchronizeDatas(DataTable dtProcRecords, DataTable dtProcRecords_DAF, object SynchronizeLock)
        {
            #region ��������
            int retVal = -1;
            int iCountsOfProcRecords = 0, iCountsOfProcRecords_DAF = 0;

            #endregion

            #region ���ʵ��
            try
            {
                lock (SynchronizeLock)
                {
                    iCountsOfProcRecords = dtProcRecords.Rows.Count;
                    iCountsOfProcRecords_DAF = dtProcRecords_DAF.Rows.Count;

                    for (int iIndex = iCountsOfProcRecords; iIndex < iCountsOfProcRecords_DAF; iIndex++)
                    {
                        if ((dtProcRecords_DAF.Rows[iIndex] == null) || (dtProcRecords_DAF.Rows[iIndex].RowState != DataRowState.Unchanged))
                            continue;
                        ProcRecordsBM procRecordsBM = new ProcRecordsBM(dtProcRecords_DAF.Rows[iIndex]);
                        AddRecord(dtProcRecords, procRecordsBM);
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
