using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBR;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// �û����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class AccountBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountBF()
        {

        }

        #region ���ע���ʺźϷ���
        /// <summary>
        /// ���ע���ʺźϷ���
        /// </summary>
        /// <param name="strUserId">�û��ʺ�</param>
        /// <returns>�Զ�������</returns>
        public ReturnValueSF ValidAccount(string strUserId)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ����ݷ�������෽��
                FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
                DataTable dtGetUser = accountDAF.GetAccountByUserId(strUserId);

                if (dtGetUser.Rows.Count > 0)
                {
                    //ע���û��Ѵ���
                    rvSF.Result = -1;
                    rvSF.Message = SysConstBM.FlightMonitor_ACCOUNT_INVALID;
                }
                else
                {
                    rvSF.Result = 1;
                    rvSF.Message = SysConstBM.DEMO_ACCOUNT_VALID;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion

        #region �����û�
        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <param name="ilDataItemPurview">�û�Ȩ���б�</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF Insert(FlightMonitorBM.AccountBM accountBM,IList ilDataItemPurview)
        {
            ReturnValueSF rvSF = new ReturnValueSF();  //���巵��ֵ

            try
            {
                //���ݷ��������
                FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
                //�ж��û��Ƿ�Ϸ�
                rvSF = this.ValidAccount(accountBM.UserId);

                //�ж��ʺ��Ƿ�Ϸ���������
                if (rvSF.Result > 0)
                {
                    rvSF.Result = accountDAF.Insert(accountBM, ilDataItemPurview);
                    if (rvSF.Result > 0)
                    {
                        rvSF.Message = FlightMonitorBM.SysConstBM.SYS_ADD_SUCCESS;
                    }
                    else
                    {
                        rvSF.Message = FlightMonitorBM.SysConstBM.SYS_ADD_FALSE;
                    }
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex); 
            }
            return rvSF;
        }
        #endregion

        #region ���������û���Ϣ
        /// <summary>
        /// ���������û���Ϣ
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <param name="ilDataItemPurview">�û�Ȩ���б�</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF UpdateAllInfo(FlightMonitorBM.AccountBM accountBM,IList ilDataItemPurview)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();


            try
            {
                //�ж��û��Ƿ����
                rvSF = this.ValidAccount(accountBM.UserId);

                //�û�������
                if (rvSF.Result > 0)
                {
                    //Ԥ��ϵͳȨ�޺�������Ȩ��
                    rvSF = this.Insert(accountBM, ilDataItemPurview);
                }
                else  //�����û�
                {
                    rvSF.Result = accountDAF.UpdateAllInfo(accountBM, ilDataItemPurview);
                    if (rvSF.Result > 0)
                    {
                        rvSF.Message = SysConstBM.SYS_UPDATE_SUCCESS;
                    }
                    else
                    {
                        rvSF.Message = SysConstBM.SYS_UPDATE_FALSE;
                    }
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ɾ���û�
        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF Delete(FlightMonitorBM.AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();

            //�������ݷ�����۲㷽��
            try
            {
                rvSF.Result = accountDAF.Delete(accountBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_DELETE_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_DELETE_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ��֤�û���½
        /// <summary>
        /// ��֤�û���½
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //����ֵ

            try
            {
                //����ҵ�����㷽��
                FlightMonitorBR.AccountBR accountBR = new FlightMonitorBR.AccountBR();
                rvSF = accountBR.LogOn(accountBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ��֤�û���½�������˵�½��ʽ��ѡ��
        /// <summary>
        /// ��֤�û���½�������˵�½��ʽ��ѡ��
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF LogOn(FlightMonitorBM.AccountBM accountBM, string LogOnType)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //����ֵ

            try
            {
                //����ҵ�����㷽��
                FlightMonitorBR.AccountBR accountBR = new FlightMonitorBR.AccountBR();
                rvSF = accountBR.LogOn(accountBM, LogOnType);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion ��֤�û���½�������˵�½��ʽ��ѡ��

        #region ����û�����������˳�ʱ�ĵ�½��Ϣ
        /// <summary>
        /// ����û�����������˳�ʱ�ĵ�½��Ϣ
        /// </summary>
        /// <param name="strUserID">�ϴε�½�û�ID</param>
        /// <returns></returns>
        public ReturnValueSF CheckLogOFF(string strUserID)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //����ҵ�����㷽��
                FlightMonitorBR.AccountBR accountBR = new AccountBR();
                accountBR.CheckLogOFF(strUserID);
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

        #region �����û�����
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <param name="accountBM">�û�����ʵ��</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF UpdatePassword(FlightMonitorBM.AccountBM accountBM)
        {
            ReturnValueSF rvSF = new ReturnValueSF();  //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                FlightMonitorDA.AccountDAF accountDAF = new FlightMonitorDA.AccountDAF();
                rvSF.Result = accountDAF.UpdatePassword(accountBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ���µ�½�û�
        /// <summary>
        /// ���µ�½�û�
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF UpdateLogUser(FlightMonitorBM.AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                FlightMonitorDA.AccountDAF accountDAF = new AirSoft.FlightMonitor.FlightMonitorDA.AccountDAF();
                rvSF.Result = accountDAF.UpdateLogUser(accountBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_UPDATE_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ���ݺ�վ���û����Ͳ�ѯ�û�
        /// <summary>
        /// ���ݺ�վ���û����Ͳ�ѯ�û�
        /// </summary>
        /// <param name="strStationThreeCode">��վ������</param>
        /// <param name="strUserTypeId">�û�����</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF GetAccountByStation(string strStationThreeCode, string strUserTypeId)
        {
            ReturnValueSF rvSF = new ReturnValueSF(); //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                FlightMonitorDA.AccountDAF accountDAF = new FlightMonitorDA.AccountDAF();
                rvSF.Dt = accountDAF.GetAccountByStation(strStationThreeCode, strUserTypeId);
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

        #region �����û������ȡ�û���Ϣ
        /// <summary>
        /// �����û������ȡ�û���Ϣ
        /// </summary>
        /// <param name="strUserId">�û�����</param>
        /// <returns>�Զ�������</returns>
        public ReturnValueSF GetAccountByUserId(string strUserId)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                FlightMonitorDA.AccountDAF accountDAF = new FlightMonitorDA.AccountDAF();
                rvSF.Dt = accountDAF.GetAccountByUserId(strUserId);
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

        #region ����һ�������û�
        /// <summary>
        /// ����һ�������û�
        /// </summary>
        /// <param name="accountBM"></param>
        public int InsertOnlineUser(AccountBM accountBM)
        {
            AccountDAF accountDAF = new AccountDAF();
            int iOnlineUserNo = accountDAF.InsertOnlineUser(accountBM);
            return iOnlineUserNo;
        }
        #endregion

        #region ���������û���Ϣ
        /// <summary>
        /// ���������û���Ϣ
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="iOnlineUserNo">�û���¼��¼��ID</param>
        public void UpdateOnlineUser(AccountBM accountBM, int iOnlineUserNo)
        {
            AccountDAF accountDAF = new AccountDAF();
            accountDAF.UpdateOnlineUser(accountBM, iOnlineUserNo);
        }
        #endregion

        #region ɾ�������û���Ϣ
        /// <summary>
        /// ɾ�������û���Ϣ
        /// </summary>
        /// <param name="accountBM"></param>
        public void DeleteOnlineUser(int iOnlineUserNo)
        {
            AccountDAF accountDAF = new AccountDAF();
            accountDAF.DeleteOnlineUser(iOnlineUserNo);
        }
        #endregion

        #region �û��˳�ϵͳ
        /// <summary>
        /// �û��˳�ϵͳ
        /// </summary>
        /// <param name="iOnlineUserNo"></param>
        public void LogOffOnlineUser(int iOnlineUserNo)
        {
            AccountDAF accountDAF = new AccountDAF();
            accountDAF.LogOffOnlineUser(iOnlineUserNo);
        }
        #endregion

        #region ��ѯ�����û�����
        /// <summary>
        /// ��ѯ�����û�����
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int SelectOnlineUserCount(AccountBM accountBM)
        {
            AccountDAF accountDAF = new AccountDAF();
            int iCount = accountDAF.SelectOnlineUserCount(accountBM);
            return iCount;
        }
        #endregion

        #region ��ѯ�����û��б�
        /// <summary>
        /// ��ѯ�����û��б�
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOnlineUsersList()
        {
            AccountDAF accountDAF = new AccountDAF();
            DataTable dt = accountDAF.SelectOnlineUsersList();
            return dt;
        }
        #endregion

    }
}
