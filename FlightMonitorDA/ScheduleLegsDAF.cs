using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ����ƻ����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ScheduleLegsDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ScheduleLegsDAF()
        {
        }

        /// <summary>
        /// ���غ���ƻ�����
        /// </summary>
        /// <param name="strFullPath">�ƻ��ļ���ȫ·��</param>
        /// <returns></returns>
        public int LoadScheduleFlight(string strFullPath)
        {
            //���巵��ֵ
            int retVal = 0;

            //���ƻ��ļ����ص����ݼ�
            FlightMonitorBM.ScheduleLegsBM sourceScheduleLegsBM = new FlightMonitorBM.ScheduleLegsBM();
            //FileStream fsScheduleFlight = new FileStream(strFullPath, FileMode.Open, FileAccess.Read, FileShare.Delete);
            try
            {
                sourceScheduleLegsBM.ReadXml(strFullPath);                              
            }
            catch (Exception ex)
            {
                retVal = -1;
                throw ex;                
            }
            //finally
            //{
            //    fsScheduleFlight.Close();
            //}

            //������ز��ɹ��򷵻�
            if (retVal < 0)
            {
                return retVal;
            }

            //�����ݼ����ص����ݿ���
            FlightMonitorDA.ScheduleLegsDA scheduleDA = new ScheduleLegsDA();
            try
            {
                //�����ݿ�����
                scheduleDA.GetConnOpen(Public.SystemFramework.ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                
                //retVal = scheduleDA.LoadScheduleFlight(sourceScheduleLegsBM);
                foreach (DataRow dataRow in sourceScheduleLegsBM.Tables[0].Rows)
                {
                    retVal = scheduleDA.Insert(new ChangeLegsBM(dataRow, 1));

                   
                }

                if (retVal > 0)
                {
                    File.Delete(strFullPath);
                }
               
            }
            catch (Exception ex)
            {
                retVal = -1;
                throw ex;              
            }

            return retVal;
        }
    }
}
