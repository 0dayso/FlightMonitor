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
    /// �û����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class AccountDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountDAF()
        {
        }

        #region �������û�
        /// <summary>
        /// �������û�
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <param name="ilDataItemPurview">������Ȩ���б�</param>
        /// <returns>1���ɹ���0��ʧ��</returns>
        public int Insert(FlightMonitorBM.AccountBM accountBM, IList ilDataItemPurview)
        {
            int retVal = -1; //���巵��ֵ

            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            FlightMonitorDA.DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            IEnumerator ieDataItemPurview = ilDataItemPurview.GetEnumerator();

            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                accountDA.Transaction = accountDA.SqlConn.BeginTransaction();
                accountDA.Insert(accountBM);

                dataItemPurviewDA.SqlConn = accountDA.SqlConn;
                dataItemPurviewDA.Transaction = accountDA.Transaction;
                while (ieDataItemPurview.MoveNext())
                {
                    FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurview.Current;
                    dataItemPurviewDA.Insert(dataItemPurviewBM);
                }

                retVal = 1;
                accountDA.Transaction.Commit();
            }
            catch (Exception ex)
            {
                accountDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ���������û���Ϣ
        /// <summary>
        /// ���������û���Ϣ
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <param name="ilDataItemPurview">������Ȩ���б�</param>
        /// <returns>1���ɹ���0��ʧ��</returns>
        public int UpdateAllInfo(FlightMonitorBM.AccountBM accountBM, IList ilDataItemPurview)
        {
            int retVal = -1; //���巵��ֵ

            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            FlightMonitorDA.DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            IEnumerator ieDataItemPurview = ilDataItemPurview.GetEnumerator();

            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                accountDA.Transaction = accountDA.SqlConn.BeginTransaction();

                accountDA.UpdateAllInfo(accountBM);

                dataItemPurviewDA.SqlConn = accountDA.SqlConn;
                dataItemPurviewDA.Transaction = accountDA.Transaction;
                while (ieDataItemPurview.MoveNext())
                {
                    FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurview.Current;
                    dataItemPurviewDA.UpdatePrompt(dataItemPurviewBM);
                }

                retVal = 1;
                accountDA.Transaction.Commit();                
            }
            catch (Exception ex)
            {
                accountDA.Transaction.Rollback();     
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ɾ���û�
        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>1���ɹ���0��ʧ��</returns>
        public int Delete(FlightMonitorBM.AccountBM accountBM)
        {
            //���巵��ֵ
            int retVal = -1;
            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            //�����ݿ�����
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                accountDA.Delete(accountBM);
                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �����û�����
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>1���ɹ���0��ʧ��</returns>
        public int UpdatePassword(FlightMonitorBM.AccountBM accountBM)
        {
            int retVal = -1; //���巵��ֵ

            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();

            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); //�����ݿ�����
                accountDA.UpdatePassword(accountBM);
                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ���µ�½�û���
        /// <summary>
        /// ���µ�½�û���
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns>1���ɹ���0��ʧ��</returns>
        public int UpdateLogUser(FlightMonitorBM.AccountBM accountBM)
        {
            //���巵��ֵ
            int retVal = -1;

            FlightMonitorDA.AccountDA accountDA = new AccountDA();
            try
            {
                //�����ݿ�����
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

                accountDA.UpdateLogUser(accountBM);
                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }

            return retVal;

        }
        #endregion

        #region ���ݺ�վ���û����Ͳ�ѯ�û�
        /// <summary>
        /// ���ݺ�վ���û����Ͳ�ѯ�û�
        /// </summary>
        /// <param name="strStationThreeCode">��վ������</param>
        /// <param name="strUserTypeId">�û�����</param>
        /// <returns>���������������û���DataTable</returns>
        public DataTable GetAccountByStation(string strStationThreeCode, string strUserTypeId)
        {
            DataTable dt = new DataTable();// ���巵��ֵ
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            
            try
            {
                accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
                dt = accountDA.GetAccountByStation(strStationThreeCode, strUserTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region �����û�ID��ȡ�û���Ϣ
        /// <summary>
        /// �����û�ID��ȡ�û���Ϣ
        /// </summary>
        /// <param name="strUserId">�û�����</param>
        /// <returns>�����û���Ϣ��DataTable</returns>
        public DataTable GetAccountByUserId(string strUserId)
        {
            DataTable dt = new DataTable();//���巵��ֵ
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                dt = accountDA.GetAccountByUserId(strUserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region ����һ�������û�
        /// <summary>
        /// ����һ�������û�
        /// </summary>
        /// <param name="accountBM"></param>
        public int InsertOnlineUser(AccountBM accountBM)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            int iOnlineUserNo = 0;
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                iOnlineUserNo = accountDA.InsertOnlineUser(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
            return iOnlineUserNo;
        }
        #endregion

        #region ���������û���Ϣ
        /// <summary>
        /// ���������û���Ϣ
        /// </summary>
        /// <param name="accountBM"></param>
        /// <param name="iOnlineUserNo">�û���¼ʱ������Id</param>
        public void UpdateOnlineUser(AccountBM accountBM, int iOnlineUserNo)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                accountDA.UpdateOnlineUser(accountBM, iOnlineUserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
        }
        #endregion

        #region ɾ�������û���Ϣ
        /// <summary>
        /// ɾ�������û���Ϣ
        /// </summary>
        /// <param name="accountBM"></param>
        public void DeleteOnlineUser(int iOnlineUserNo)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                accountDA.DeleteOnlineUser(iOnlineUserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
        }
        #endregion

        #region �û��˳�ϵͳ
        /// <summary>
        /// �û��˳�ϵͳ
        /// </summary>
        /// <param name="iOnlineUserNo"></param>
        public void LogOffOnlineUser(int iOnlineUserNo)
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                accountDA.LogOffOnlineUser(iOnlineUserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }
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
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            int iOnlineUsersCount = 0;
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                iOnlineUsersCount = accountDA.SelectOnlineUserCount(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }

            return iOnlineUsersCount;
        }
        #endregion

        #region ��ѯ�����û��б�
        /// <summary>
        /// ��ѯ�����û��б�
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOnlineUsersList()
        {
            FlightMonitorDA.AccountDA accountDA = new FlightMonitorDA.AccountDA();
            DataTable dt = new DataTable();
            accountDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1)); // �����ݿ�����
            try
            {
                dt = accountDA.SelectOnlineUsersList();
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }
            finally
            {
                accountDA.ConnClose();
            }

            return dt;
        }
        #endregion
    }
}
