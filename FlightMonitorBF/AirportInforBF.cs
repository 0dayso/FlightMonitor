using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class AirportInforBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AirportInforBF()
        {
        }

        /// <summary>
        /// ��ȡ���л�����Ϣ
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAirportInfors()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            AirportInfoDAF airportInfoDAF = new AirportInfoDAF();

            try
            {
                rvSF.Dt = airportInfoDAF.GetAirportInfors();
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
        /// �����������ȡ������Ϣ
        /// </summary>
        /// <param name="strThreeCode">����������</param>
        /// <returns></returns>
        public ReturnValueSF GetAirportInforByThreeCode(string strThreeCode)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            AirportInfoDAF airportInfoDAF = new AirportInfoDAF();

            try
            {
                rvSF.Dt = airportInfoDAF.GetAirportInforByThreeCode(strThreeCode);
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
        /// ���ݻ��ͺ���ɻ�����ȡ������
        /// </summary>
        /// <param name="strACTYPE">FOC����</param>
        /// <param name="strThreeCode">��ɻ���������</param>
        /// <returns></returns>
        public ReturnValueSF GetTaxiOil(string strACTYPE, string strThreeCode)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            AirportInfoDAF airportInfoDAF = new AirportInfoDAF();

            try
            {
                rvSF.Dt = airportInfoDAF.GetTaxiOil(strACTYPE, strThreeCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
    }
}
