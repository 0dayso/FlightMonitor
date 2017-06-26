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
    /// �����ռ���Ϣ����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-05-26
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeRecordDAF
    {
        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="guaranteeRecordBM">�����ռ���Ϣ����</param>
        /// <returns></returns>
        public int Add(GuaranteeRecordBM guaranteeRecordBM)
        {
            int retVal = -1; //���巵��ֵ

            GuaranteeRecordDA guaranteeRecordDA = new GuaranteeRecordDA();

            try
            {
                guaranteeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //�����ݿ�����
                retVal = guaranteeRecordDA.Add(guaranteeRecordBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordDA.ConnClose();
            }

            return retVal;
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
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC, int cniGuaranteeRecordCaptionID)
        {
            DataTable dataTableResult = new DataTable(); //���巵��ֵ

            GuaranteeRecordDA guaranteeRecordDA = new GuaranteeRecordDA();

            try
            {
                guaranteeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //�����ݿ�����
                dataTableResult = guaranteeRecordDA.GetDataList( cncStation,  cncDATOP,  cnvcFLTID,  cniLegNO,  cnvcAC,  cniGuaranteeRecordCaptionID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordDA.ConnClose();
            }

            return dataTableResult;
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
        public DataTable GetDataList(string cncStation, string cncDATOP, string cnvcFLTID, int cniLegNO, string cnvcAC)
        {
            DataTable dataTableResult = new DataTable(); //���巵��ֵ

            GuaranteeRecordDA guaranteeRecordDA = new GuaranteeRecordDA();

            try
            {
                guaranteeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //�����ݿ�����
                dataTableResult = guaranteeRecordDA.GetDataList(cncStation, cncDATOP, cnvcFLTID, cniLegNO, cnvcAC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                guaranteeRecordDA.ConnClose();
            }

            return dataTableResult;
        }
        #endregion ��ȡ������Ϣ(���ݺ��ࡢ��վ)
    }
}
