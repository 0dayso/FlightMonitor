using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ���������ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-23
    /// �� �� �ˣ���  ��
    /// �޸����ڣ�2008-07-01
    /// ��    ����
    public class DataItemPurviewDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DataItemPurviewDAF()
        {
        }

        #region ��ȡ����������
        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataItems()
        {
            //���巵��ֵ
            DataTable dtDataItems = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataItems = dataItemPurviewDA.GetDataItems();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }

            return dtDataItems;
        }
        #endregion

        #region ��ȡ�û������������Ȩ��
        /// <summary>
        /// ��ȡ�û������������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">����Ȩ��ʵ�����</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            DataTable dtDataItemsPurview = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataItemsPurview = dataItemPurviewDA.GetDataItemPurviewByUserId(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }

            return dtDataItemsPurview;
        }
        #endregion

        #region ��ȡ�ο��� yanxian 2013-12-26
        /// <summary>
        /// ��ȡ�ο���
        /// </summary>
        /// <param name="dataItemPurviewBM">����Ȩ��ʵ�����</param>
        /// <returns></returns>
        public DataTable GetDataItemPointPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            DataTable dtDataItemsPurview = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();

            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtDataItemsPurview = dataItemPurviewDA.GetDataItemPointPurviewByUserId(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }

            return dtDataItemsPurview;
        }
        #endregion

        #region ����һ����¼
        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public int Insert(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //���巵��ֵ
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = dataItemPurviewDA.Insert(dataItemPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ����������Ȩ��
        /// <summary>
        /// ����������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePurview(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //���巵��ֵ
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = dataItemPurviewDA.UpdatePurview(dataItemPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ����������Ŀɼ���
        /// <summary>
        /// ����������Ŀɼ���
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateVisible(IList ilDataItemPurviewBM)
        {
            //���巵��ֵ
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                IEnumerator ieDataItemPurviewBM = ilDataItemPurviewBM.GetEnumerator();
                while (ieDataItemPurviewBM.MoveNext())
                {
                    DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurviewBM.Current;
                    retVal = dataItemPurviewDA.UpdateVisible(dataItemPurviewBM);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �������������ʾ˳��
        /// <summary>
        /// �������������ʾ˳��
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateIndex(IList ilDataItemPurviewBM)
        {
            //���巵��ֵ
            int retVal = -1;
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                
                IEnumerator ieDataItemPurviewBM = ilDataItemPurviewBM.GetEnumerator();
                while (ieDataItemPurviewBM.MoveNext())
                {
                    DataItemPurviewBM dataItemPurviewBM = (DataItemPurviewBM)ieDataItemPurviewBM.Current;
                    retVal = dataItemPurviewDA.UpdateIndex(dataItemPurviewBM);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ��ѯĳ�û�ĳ�������Ȩ��
        /// <summary>
        /// ��ѯĳ�û�ĳ�������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByDataItemNo(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //���巵��ֵ
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetDataItemPurviewByDataItemNo(dataItemPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region ��ȡ�û���Ȩ�޵�������
        /// <summary>
        /// ��ȡ�û���Ȩ�޵�������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetPurviewDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //���巵��ֵ
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetPurviewDataItem(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region ��ȡ�û����õĿɼ���������
        /// <summary>
        /// ��ȡ�û����õĿɼ���������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //���巵��ֵ
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetVisibleDataItem(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion

        #region �������ͻ�ȡ�û����õĿɼ���������
        /// <summary>
        /// ��ȡ�û����õĿɼ���������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM, string strType)
        {
            //���巵��ֵ
            DataTable dt = new DataTable();
            DataItemPurviewDA dataItemPurviewDA = new DataItemPurviewDA();
            try
            {
                //�����ݿ�����
                dataItemPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = dataItemPurviewDA.GetVisibleDataItem(accountBM, strType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataItemPurviewDA.ConnClose();
            }
            return dt;
        }
        #endregion
    }
}
