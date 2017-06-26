using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class CrewSignInBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CrewSignInBF()
        {
        }

        /// <summary>
        /// ��ѯһ������Ļ���ǩ����Ϣ
        /// </summary>
        /// <param name="strQueryTime">��ѯʱ��</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns></returns>
        public ReturnValueSF GetCrewSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetCrewSignIn(strQueryTime, strFlightNo, strDEPSTN);
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
        /// ��ȡĳλ����Ա��ǩ��ʱ��
        /// </summary>
        /// <param name="strCrewName">����Ա����</param>
        /// <param name="strSignInFlag">ǩ����ʶ</param>
        /// <returns></returns>
        public ReturnValueSF GetCrewSignTime(string strCrewName, string strSignInFlag)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetCrewSignTime(strCrewName, strSignInFlag);
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
        /// ��ѯһ������ĳ���ǩ����Ϣ
        /// </summary>
        /// <param name="strQueryTime">��ѯʱ��</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns>ǩ����Ϣ��</returns>
        public ReturnValueSF GetStewardSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetStewardSignIn(strQueryTime, strFlightNo, strDEPSTN);
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
        /// ��ȡĳλ����Ա��ǩ��ʱ��
        /// </summary>
        /// <param name="strStewardName">����Ա����</param>
        /// <param name="strSignInFlag">ǩ����ʶ</param>
        /// <returns></returns>
        public ReturnValueSF GetStewardSignTime(string strStewardName, string strSignInFlag)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            CrewSignInDAF crewSignInDAF = new CrewSignInDAF();
            try
            {
                rvSF.Dt = crewSignInDAF.GetStewardSignTime(strStewardName, strSignInFlag);
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
