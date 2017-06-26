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
    /// �����ռ�������Ϣ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-06-24
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeRecordCaptionBF
    {
        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="guaranteeRecordCaptionBM">�����ռ�������Ϣ����</param>
        /// <returns></returns>
        public ReturnValueSF Add(GuaranteeRecordCaptionBM guaranteeRecordCaptionBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�����۲㷽��
                GuaranteeRecordCaptionDAF guaranteeRecordCaptionDAF = new GuaranteeRecordCaptionDAF();
                rvSF.Result = guaranteeRecordCaptionDAF.Add(guaranteeRecordCaptionBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion ����һ������

        #region ��ȡ������Ϣ(���ݺ��ࡢ��վ)
        /// <summary>
        /// ��ȡ������Ϣ(���ݺ��ࡢ��վ)
        /// </summary>
        /// <param name="cncStation">��վ</param>
        /// <param name="cncDATOP"></param>
        /// <param name="cnvcFLTID"></param>
        /// <param name="cniLegNO"></param>
        /// <param name="cnvcAC"></param>
        /// <returns>��ȡ������Ϣ(���ݺ��ࡢ��վ)</returns>
        public ReturnValueSF GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeRecordCaptionDAF guaranteeRecordCaptionDAF = new GuaranteeRecordCaptionDAF();
                rvSF.Dt = guaranteeRecordCaptionDAF.GetDataList(cncStation, cncDATOP, cnvcFLTID, cniLegNO, cnvcAC);
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
