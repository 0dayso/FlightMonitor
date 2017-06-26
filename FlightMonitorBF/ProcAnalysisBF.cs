using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.FlightMonitor.AgentServiceDA;

namespace AirSoft.FlightMonitor.AgentServiceBF
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
    public class ProcAnalysisBF
    {
        #region ���ɹ��̷�����
        /// <summary>
        /// ���ɹ��̷�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF CreateDatatable()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                rvSF.Dt = procAnalysisDAF.CreateDatatable();
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

        #region ���¼�¼
        /// <summary>
        /// ���¼�¼
        /// </summary>
        /// <param name="dtProcAnalysis">���̷��������</param>
        /// <param name="procAnalysisBM">Ҫ�޸ĵĽ��</param>
        /// <returns>ReturnValueSF.Result:1 �ɹ���-1 ʧ��</returns>
        public ReturnValueSF UpdateRecord(DataTable dtProcAnalysis, ProcAnalysisBM procAnalysisBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                rvSF.Result = procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM);
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

        #region ���� ���� ��¼ͳ�Ʊ��������и��µİ취
        /// <summary>
        /// ���� ���� ��¼ͳ�Ʊ��������и��µİ취
        /// </summary>
        /// <param name="dtProcAnalysis">������ ���� dtProcAnalysis</param>
        /// <param name="dtProcAnalysis_DAF">AgentServiceDAF.dtProcAnalysis</param>
        /// <param name="SynchronizeLock">ͬ����</param>
        /// <returns></returns>
        public ReturnValueSF SynchronizeDatas(DataTable dtProcAnalysis, DataTable dtProcAnalysis_DAF, object SynchronizeLock)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                rvSF.Result = procAnalysisDAF.SynchronizeDatas(dtProcAnalysis, dtProcAnalysis_DAF, SynchronizeLock);
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
