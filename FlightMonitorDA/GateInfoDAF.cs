using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ��ȡͣ��λ��Ϣ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2014-09-03
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GateInfoDAF
    {
        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="StationThreeCode">����������</param>
        /// <returns></returns>
        public DataTable GetList(string StationThreeCode)
        {
            //���巵��ֵ
            DataTable dtDataTable = new DataTable();
            GateInfoDA gateInfoDA = new GateInfoDA();

            try
            {
                //�����ݿ�����
                gateInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataTable = gateInfoDA.GetList(StationThreeCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                gateInfoDA.ConnClose();
            }

            return dtDataTable;
        }
        #endregion ��������б�

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="DataTable">��Ҫ��������ݱ�</param>
        /// <returns></returns>
        public int Save(DataTable DataTable)
        {
            int retVal = -1;
            GateInfoDA gateInfoDA = new GateInfoDA();

            try
            {
                gateInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                retVal = gateInfoDA.Save(DataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                gateInfoDA.ConnClose();
            }

            return retVal;
        }
        #endregion ��������
    }
}
