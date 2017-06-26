using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Data;
using System.Collections;
using AirSoft.FlightMonitor.AgentServiceBF;
using AirSoft.FlightMonitor.AgentServiceDA;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// �����ռ���Ϣ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-06-26
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeRecordBF
    {
        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="guaranteeRecordBM">�����ռ���Ϣ����</param>
        /// <returns></returns>
        public ReturnValueSF Add(GuaranteeRecordBM guaranteeRecordBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�����۲㷽��
                GuaranteeRecordDAF guaranteeRecordDAF = new GuaranteeRecordDAF();
                rvSF.Result = guaranteeRecordDAF.Add(guaranteeRecordBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion ����һ������

        #region ��ȡ������Ϣ(���ݺ��ࡢ��վ������)
        /// <summary>
        /// ��ȡ������Ϣ(���ݺ��ࡢ��վ������)
        /// </summary>
        /// <param name="cncStation"></param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <param name="cniGuaranteeRecordCaptionID"></param>
        /// <returns></returns>
        public ReturnValueSF GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC, int cniGuaranteeRecordCaptionID)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeRecordDAF guaranteeRecordDAF = new GuaranteeRecordDAF();
                rvSF.Dt = guaranteeRecordDAF.GetDataList( cncStation,  cncDATOP,  cnvcFLTID,  cniLegNO,  cnvcAC,  cniGuaranteeRecordCaptionID);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion ��ȡ������Ϣ(���ݺ��ࡢ��վ������)

        #region ��ȡ������Ϣ(���ݺ��ࡢ��վ)
        /// <summary>
        /// ��ȡ������Ϣ(���ݺ��ࡢ��վ)
        /// </summary>
        /// <param name="cncStation"></param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <param name="cniGuaranteeRecordCaptionID"></param>
        /// <returns></returns>
        public ReturnValueSF GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeRecordDAF guaranteeRecordDAF = new GuaranteeRecordDAF();
                rvSF.Dt = guaranteeRecordDAF.GetDataList(cncStation, cncDATOP, cnvcFLTID, cniLegNO, cnvcAC);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion ��ȡ������Ϣ(���ݺ��ࡢ��վ)
    }
}
