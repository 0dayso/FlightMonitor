using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// �����ռ�������Ϣ����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-05-26
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeRecordCaptionDAF
    {
        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="guaranteeRecordCaptionBM">�����ռ�������Ϣ����</param>
        /// <returns></returns>
        public int Add(GuaranteeRecordCaptionBM guaranteeRecordCaptionBM)
        {
            int retVal = -1; //���巵��ֵ

            GuaranteeRecordCaptionDA guaranteeRecordCaptionDA = new GuaranteeRecordCaptionDA();

            try
            {
                guaranteeRecordCaptionDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //�����ݿ�����
                retVal =  guaranteeRecordCaptionDA.Add(guaranteeRecordCaptionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordCaptionDA.ConnClose();
            }

            return retVal;
        }
        #endregion ����һ������

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        public DataTable GetDataList()
        {
            DataTable dataTableResult = new DataTable(); //���巵��ֵ

            GuaranteeRecordCaptionDA guaranteeRecordCaptionDA = new GuaranteeRecordCaptionDA();

            try
            {
                guaranteeRecordCaptionDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //�����ݿ�����
                dataTableResult = guaranteeRecordCaptionDA.GetDataList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordCaptionDA.ConnClose();
            }

            return dataTableResult;
        }
        #endregion ��ȡ������Ϣ

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
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            DataTable dataTableResult = new DataTable(); //���巵��ֵ

            GuaranteeRecordCaptionDA guaranteeRecordCaptionDA = new GuaranteeRecordCaptionDA();

            try
            {
                guaranteeRecordCaptionDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //�����ݿ�����
                dataTableResult = guaranteeRecordCaptionDA.GetDataList(cncStation, cncDATOP, cnvcFLTID, cniLegNO, cnvcAC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordCaptionDA.ConnClose();
            }

            return dataTableResult;
        }
        #endregion ��ȡ������Ϣ(���ݺ��ࡢ��վ)

    }
}
