using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.AgentServiceBF
{
    /// <summary>
    /// �����û���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-11-30
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class OnLineUsersBF
    {
        #region ���� �����û���
        /// <summary>
        /// ���� �����û���
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF CreateDatatable()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                rvSF.Dt = onLineUsersDAF.CreateDatatable();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ���� �����û���Ϣ
        /// <summary>
        /// ���� �����û���Ϣ
        /// </summary>
        /// <param name="dtOnLineUsers">�����û���Ϣ��</param>
        /// <param name="accountBM">�ʺŶ���ʵ��</param>
        /// <returns>ReturnValueSF.Result:1 �ɹ���-1 ʧ��</returns>
        public ReturnValueSF RefreshOnLineUsersInfo(DataTable dtOnLineUsers, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                rvSF.Result = onLineUsersDAF.RefreshOnLineUsersInfo(dtOnLineUsers,accountBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            //
            return rvSF;
        }
        #endregion

    }
}
