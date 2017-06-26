using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.AgentServiceBM;
using System.Data;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.AgentServiceBF
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
    public class ProcRecordsBF
    {
        #region ���ɹ��̼�¼��
        /// <summary>
        /// ���ɹ��̼�¼��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF CreateDatatable()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                rvSF.Dt = procRecordsDAF.CreateDatatable();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ���һ����¼
        /// <summary>
        /// ���һ����¼
        /// </summary>
        /// <param name="dtProcRecords">���̼�¼�����</param>
        /// <param name="procRecordsBM">Ҫ��ӵļ�¼</param>
        /// <returns>ReturnValueSF.Result:1 �ɹ���-1 ʧ��</returns>
        public ReturnValueSF AddRecord(DataTable dtProcRecords, ProcRecordsBM procRecordsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                rvSF.Result = procRecordsDAF.AddRecord(dtProcRecords,procRecordsBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            //
            return rvSF;
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
        public ReturnValueSF SynchronizeDatas(DataTable dtProcRecords, DataTable dtProcRecords_DAF, object SynchronizeLock)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                rvSF.Result = procRecordsDAF.SynchronizeDatas(dtProcRecords, dtProcRecords_DAF, SynchronizeLock);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            //
            return rvSF;
        }
        #endregion


    }
}
