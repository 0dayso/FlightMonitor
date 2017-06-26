using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��������мƻ����ݷ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-07-01
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ComputerPlanDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ComputerPlanDAF()
        {
        }

        /// <summary>
        /// ��ȡ��������мƻ�
        /// </summary>
        /// <param name="strDate">��������</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDepstn">ʼ��վ������</param>
        /// <param name="strArrstn">����վ������</param>
        /// <returns></returns>
        public DataTable GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn)
        {
            //���巵��ֵ
            DataTable dtComputerPlan = new DataTable();

            ComputerPlanDA computerPlanDA = new ComputerPlanDA();

            try
            {
                //�����ݿ�����
                computerPlanDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_DSPBusiness, 1));
                dtComputerPlan = computerPlanDA.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                computerPlanDA.ConnClose();
            }

            return dtComputerPlan;
        }

        /// <summary>
        /// �������ڻ�ȡ���м�������мƻ�
        /// </summary>
        /// <param name="strDate">��������</param>
        /// <returns></returns>
        public DataTable GetCFPByFlightDate(string strDate)
        {
            //���巵��ֵ
            DataTable dtComputerPlan = new DataTable();

            ComputerPlanDA computerPlanDA = new ComputerPlanDA();

            try
            {
                //�����ݿ�����
                computerPlanDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_DSPBusiness, 1));
                dtComputerPlan = computerPlanDA.GetCFPByFlightDate(strDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                computerPlanDA.ConnClose();
            }

            return dtComputerPlan;
        }


        #region modified by LinYong in 2013.08.02
        /// <summary>
        /// ��ȡ��������мƻ�
        /// </summary>
        /// <param name="strDate">��������</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDepstn">ʼ��վ������</param>
        /// <param name="strArrstn">����վ������</param>
        /// <param name="strDATOP">�������ڣ�UTC��</param> 
        /// <returns></returns>
        public DataTable GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn, string strDATOP)
        {
            //���巵��ֵ
            DataTable dtComputerPlan = new DataTable();

            ComputerPlanDA computerPlanDA = new ComputerPlanDA();

            try
            {
                //�����ݿ�����
                computerPlanDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_DSPBusiness, 1));
                dtComputerPlan = computerPlanDA.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn, strDATOP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                computerPlanDA.ConnClose();
            }

            return dtComputerPlan;
        }
        #endregion modified by LinYong in 2013.08.02

    }
}
