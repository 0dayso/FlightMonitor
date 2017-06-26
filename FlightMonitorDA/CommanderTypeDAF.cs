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
    /// ������Ա������Ϣ���������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2016-03-21
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CommanderTypeDAF
    {
        #region ��ȡ������Ա������Ϣ
        /// <summary>
        /// ��ȡ������Ա������Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();
            //�����ݿ�����
            CommanderTypeDA commanderTypeDA = new CommanderTypeDA();
            commanderTypeDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = commanderTypeDA.Select();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commanderTypeDA.ConnClose();
            }

            return dataTable;
        }
        #endregion ��ȡ������Ա������Ϣ
    }
}
