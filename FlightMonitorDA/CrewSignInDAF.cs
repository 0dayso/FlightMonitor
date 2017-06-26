using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ����ǩ�����ݷ��ʲ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-07-10
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CrewSignInDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CrewSignInDAF()
        {
        }

        /// <summary>
        /// ��ѯһ������Ļ���ǩ����Ϣ
        /// </summary>
        /// <param name="strQueryTime">��ѯʱ��</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns>ǩ����Ϣ��</returns>
        public DataTable GetCrewSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            //���巵��ֵ
            DataTable dtCrewSignIn = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //�����ݿ�����
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtCrewSignIn = crewSignInDA.GetCrewSignIn(strQueryTime, strFlightNo, strDEPSTN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtCrewSignIn;
        }

         /// <summary>
        /// ��ȡĳλ����Ա��ǩ��ʱ��
        /// </summary>
        /// <param name="strCrewName">����Ա����</param>
        /// <param name="strSignInFlag">ǩ����ʶ</param>
        /// <returns></returns>
        public DataTable GetCrewSignTime(string strCrewName, string strSignInFlag)
        {
            //���巵��ֵ
            DataTable dtCrewSignInTime = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //�����ݿ�����
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtCrewSignInTime = crewSignInDA.GetCrewSignTime(strCrewName, strSignInFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtCrewSignInTime;
        }

         /// <summary>
        /// ��ѯһ������ĳ���ǩ����Ϣ
        /// </summary>
        /// <param name="strQueryTime">��ѯʱ��</param>
        /// <param name="strFlightNo">�����</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns>ǩ����Ϣ��</returns>
        public DataTable GetStewardSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            //���巵��ֵ
            DataTable dtStewardSignIn = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //�����ݿ�����
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtStewardSignIn = crewSignInDA.GetStewardSignIn(strQueryTime, strFlightNo, strDEPSTN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtStewardSignIn;
        }

         /// <summary>
        /// ��ȡĳλ����Ա��ǩ��ʱ��
        /// </summary>
        /// <param name="strStewardName">����Ա����</param>
        /// <param name="strSignInFlag">ǩ����ʶ</param>
        /// <returns></returns>
        public DataTable GetStewardSignTime(string strStewardName, string strSignInFlag)
        {
            //���巵��ֵ
            DataTable dtStewardSignInTime = new DataTable();
            CrewSignInDA crewSignInDA = new CrewSignInDA();

            try
            {
                //�����ݿ�����
                crewSignInDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Smart, 1));
                dtStewardSignInTime = crewSignInDA.GetStewardSignTime(strStewardName, strSignInFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewSignInDA.ConnClose();
            }

            return dtStewardSignInTime;
        }
    }
}
