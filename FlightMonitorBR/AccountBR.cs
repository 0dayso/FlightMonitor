using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBR
{
    /// <summary>
    /// �û�ҵ������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class AccountBR
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountBR()
        {
        }

        #region ��֤�û���½
        /// <summary>
        /// ��֤�û���½
        /// </summary>
        /// <param name="accountBM">�û���½ʵ��</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM)
        {
            //�Զ��巵������
            ReturnValueSF rvSF = new ReturnValueSF();

            //�������ݷ��ʲ����ݷ�������෽��
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
            DataTable dtGetUser = accountDAF.GetAccountByUserId(accountBM.UserId);

            if (dtGetUser.Rows.Count == 0)
            {
                //��½�û�������
                rvSF.Result = -1;
                rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_NODEFINE;
            }
            else
            {
                if (accountBM.UserPassword != dtGetUser.Rows[0]["cnvcUserPassword"].ToString())
                {
                    //���벻��ȷ
                    rvSF.Result = -1;
                    rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_PWDERROR;
                }
                //����ѵ�¼��ϵͳ���û�����
                else
                {
                    int iOnlineUsersCount = accountDAF.SelectOnlineUserCount(accountBM);
                    if ( iOnlineUsersCount  >= Convert.ToInt32(dtGetUser.Rows[0]["cniMaxUser"]))
                    {
                        //�Ѿ��ﵽ����½��
                        rvSF.Result = -1;
                        rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_MAX_USER;
                    }
                    else
                    {
                        rvSF.Dt = dtGetUser;
                        rvSF.Result = iOnlineUsersCount + 1;
                    }
                }
            }

            return rvSF;
        }
        #endregion

        #region ��֤�û���½�������˵�½��ʽ��ѡ��
        /// <summary>
        /// ��֤�û���½�������˵�½��ʽ��ѡ��
        /// </summary>
        /// <param name="accountBM">�û���½ʵ��</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM, string LogOnType)
        {
            //�Զ��巵������
            ReturnValueSF rvSF = new ReturnValueSF();

            //�������ݷ��ʲ����ݷ�������෽��
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
            DataTable dtGetUser = accountDAF.GetAccountByUserId(accountBM.UserId);

            if (dtGetUser.Rows.Count == 0)
            {
                //��½�û�������
                rvSF.Result = -1;
                rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_NODEFINE;
            }
            else
            {
                if ((LogOnType != "���½") && (accountBM.UserPassword != dtGetUser.Rows[0]["cnvcUserPassword"].ToString()))
                {
                    //���벻��ȷ
                    rvSF.Result = -1;
                    rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_ACCOUNTENTERIN_PWDERROR;
                }
                //����ѵ�¼��ϵͳ���û�����
                else
                {
                    int iOnlineUsersCount = accountDAF.SelectOnlineUserCount(accountBM);
                    if (iOnlineUsersCount >= Convert.ToInt32(dtGetUser.Rows[0]["cniMaxUser"]))
                    {
                        //�Ѿ��ﵽ����½��
                        rvSF.Result = -1;
                        rvSF.Message = FlightMonitorBM.SysConstBM.FlightMonitor_MAX_USER;
                    }
                    else
                    {
                        rvSF.Dt = dtGetUser;
                        rvSF.Result = iOnlineUsersCount + 1;
                    }
                }
            }

            return rvSF;
        }
        #endregion ��֤�û���½�������˵�½��ʽ��ѡ��

        public void CheckLogOFF(string strUserId)
        {
            //��ȡ�ϴε�½�û�ʵ�����
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
            DataTable dtLogUser = accountDAF.GetAccountByUserId(strUserId);
           
            //����û����������ݿ���
            if (dtLogUser.Rows.Count > 0)
            {
                FlightMonitorBM.AccountBM accountBM = new AccountBM(dtLogUser.Rows[0]);
                //������û��ϴεĵ�½��Ϣ
                accountBM.LogUser -= 1;
                accountDAF.UpdateLogUser(accountBM);
            }
        }
    }
}
