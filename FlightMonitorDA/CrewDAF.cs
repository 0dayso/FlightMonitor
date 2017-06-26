using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ����������Ϣ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-07-03
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CrewDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CrewDAF()
        {
        }

        /// <summary>
        /// ��ȡ���������Ϣ
        /// </summary>
        /// <param name="strFlightDate">�������� ����ʱ��</param>
        /// <param name="strFlightNo">�����,�����ո�</param>
        /// <param name="strDEPSTN">���վ������</param>
        /// <param name="strARRSTN">����վ������</param>
        /// <returns></returns>
        public DataTable GetCrewInforByFlightNo(string strFlightDate, string strFlightNo, string strSEG)
        {
            //���巵��ֵ
            DataTable dtCrews = new DataTable();
            CrewDA crewDA = new CrewDA();

            try
            {
                //�����ݿ�����
                crewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Flt_Dept, 1));
                dtCrews = crewDA.GetCrewInforByFlightNo(strFlightDate, strFlightNo, strSEG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                crewDA.ConnClose();
            }

            return dtCrews;
        }

        /// <summary>
        /// ���ݻ�����ȡǰ�ɺ�����Ϣ
        /// </summary>
        /// <param name="strFlightDate">�����</param>
        /// <param name="strARRSTN">ǰ�ɺ���������</param>
        /// <param name="strCaptain">��������</param>
        /// <returns></returns>
        //public DataTable GetCrewInforByCaptain(string strFlightDate, string strSEG, string strSEGALL, string strCaptain, string strBeginTime, string strEndTime)
        //{
        //    //���巵��ֵ
        //    DataTable dtCrews = new DataTable();
        //    CrewDA crewDA = new CrewDA();

        //    try
        //    {
        //        //�����ݿ�����
        //        crewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_Flt_Dept, 1));
        //        dtCrews = crewDA.GetCrewInforByCaptain(strFlightDate, strSEG, strSEGALL, strCaptain,strBeginTime, strEndTime);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        crewDA.ConnClose();
        //    }

        //    return dtCrews;
        //}
    }
}
