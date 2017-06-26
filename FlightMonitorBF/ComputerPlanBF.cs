using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class ComputerPlanBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ComputerPlanBF()
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
        public ReturnValueSF GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ComputerPlanDAF computerPlanDAF = new ComputerPlanDAF();

            try
            {
                rvSF.Dt = computerPlanDAF.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

         /// <summary>
        /// �������ڻ�ȡ���м�������мƻ�
        /// </summary>
        /// <param name="strDate">��������</param>
        /// <returns></returns>
        public ReturnValueSF GetCFPByFlightDate(string strDate)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ComputerPlanDAF computerPlanDAF = new ComputerPlanDAF();

            try
            {
                rvSF.Dt = computerPlanDAF.GetCFPByFlightDate(strDate);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
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
        public ReturnValueSF GetComputerFlightPlan(string strDate, string strFlightNo, string strDepstn, string strArrstn, string strDATOP)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ComputerPlanDAF computerPlanDAF = new ComputerPlanDAF();

            try
            {
                rvSF.Dt = computerPlanDAF.GetComputerFlightPlan(strDate, strFlightNo, strDepstn, strArrstn, strDATOP);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion modified by LinYong in 2013.08.02

    }
}
