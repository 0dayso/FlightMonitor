using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��ȡ��ǰ��������Ϣ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2013-09-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class VoyageReportDAF
    {
        /// <summary>
        /// ��ȡ��ǰ��������Ϣ����ȡʱ����ڵ���Ϣ
        /// </summary>
        /// <param name="DATOP_Start">�������ڣ���ʼ</param>
        /// <param name="DATOP_End">�������ڣ���ֹ</param>
        /// <returns></returns>
        public DataTable GetVoyageReportData(DateTime DATOP_Start, DateTime DATOP_End)
        {
            //���巵��ֵ
            DataTable dtDataTable = new DataTable();
            VoyageReportDA VoyageReportDA = new VoyageReportDA();

            try
            {
                //�����ݿ�����
                string s = ConfigManagerSF.GetConfigString(SysConstBM.DB_VoyageReport, 1);
                VoyageReportDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_VoyageReport, 1));
                dtDataTable = VoyageReportDA.GetVoyageReportData(DATOP_Start, DATOP_End);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                VoyageReportDA.ConnClose();
            }

            return dtDataTable;
        }


        /// <summary>
        /// ��ȡ��ǰ��������Ϣ����ȡ�����������Ϣ
        /// </summary>
        /// <param name="DATOP_Start">�������ڣ���ʼ</param>
        /// <param name="DATOP_End">�������ڣ���ֹ</param>
        /// <returns></returns>
        public DataTable GetVoyageReportDataBySingleFlight(string DATOP, string FLTIDS, string AC, string ROUTES)
        {
            //���巵��ֵ
            DataTable dtDataTable = new DataTable();
            VoyageReportDA VoyageReportDA = new VoyageReportDA();

            try
            {
                //�����ݿ�����
                VoyageReportDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_VoyageReport, 1));
                dtDataTable = VoyageReportDA.GetVoyageReportDataBySingleFlight( DATOP,  FLTIDS,  AC,  ROUTES);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                VoyageReportDA.ConnClose();
            }

            return dtDataTable;
        }








    }
}
