using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Collections;
using System.Data;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ���������ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-04
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ChangeLegsDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ChangeLegsDAF()
        {
        }

        /// <summary>
        /// ���뺽������̬
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <param name="changeRecordBM">�������ʵ�����</param>
        /// <returns>1���ɹ�0��ʧ��</returns>
        public int Insert(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            int retVal = -1;  //���巵��ֵ
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            //�����ݿ����ӷ���
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
            changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();


            try
            {
                //����һ�����ද̬���
                changeLegsDA.Insert(changeLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;
                changeRecordDA.Insert(changeRecordBM);

                //�����ύ
                changeLegsDA.Transaction.Commit();

                //����ֵ
                retVal = 1;
            }
            catch (Exception ex)
            {
                //����ع�
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// �߼�ɾ��һ�����ද̬
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <param name="changeRecordBM">�������ʵ��</param>
        /// <returns>1���ɹ�0��ʧ��</returns>
        public int LogicDelete(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            ///���巵��ֵ
            int retVal = -1;

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            //�����ݿ����ӷ���
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
            changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();

            try
            {
                //�߼�ɾ��һ�����ද̬
                changeLegsDA.LogicDelete(changeLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;
                changeRecordDA.Insert(changeRecordBM);

                //�����ύ
                changeLegsDA.Transaction.Commit();

                //����ֵ
                retVal = 1;
            }
            catch (Exception ex)
            {
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ������Ϊ�������º�����Ϣ
        /// </summary>
        /// <param name="oldChangeLegsBM">���ǰ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">�����������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateAllInfoByPriKey(ChangeLegsBM changeLegsBM, IList ilChangeRecordBM)
        {
            //���巵��ֵ
            int retVal = -1;

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            //�����ݿ�����
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
            changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();

            try
            {
                //������Ϊ�������º��ද̬
                changeLegsDA.UpdateAllInfoByPriKey(changeLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //�ύ����
                changeLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ���������Ϊ�������º�����Ϣ
        /// </summary>
        /// <param name="changeLegsBM">���ǰ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">�����������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateAllInfoByComKey(ChangeLegsBM changeLegsBM,ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //���巵��ֵ
            int retVal = -1;

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();           

            try
            {
                //�����ݿ�����
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();

                //������Ϊ�������º��ද̬
                changeLegsDA.UpdateAllInfoByComKey(changeLegsBM, originalLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    if (changeRecordBM.ChangeReasonCode != "cnvcAC")
                    {
                        changeRecordDA.Insert(changeRecordBM);
                    }
                }

                //�ύ����
                changeLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ������Ϊ������ѯһ����¼
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <returns></returns>
        public DataTable GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            //�����ݿ�����
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = changeLegsDA.GetFlightByKey(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }

        /// <summary>
        /// ���������Ϊ������ѯһ����¼
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <returns></returns>
        public DataTable GetFlightByCombineKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

           

            try
            {
                //�����ݿ�����
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeLegsDA.GetFlightByCombineKey(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }

         /// <summary>
        /// ����acars��ɱ���Ϣ��ȡ����
        /// </summary>
        /// <param name="acarsLegsBM">��ɱ�ʵ�����</param>
        /// <returns></returns>
        public DataTable GetFlightByDEPInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //�����ݿ�����
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeLegsDA.GetFlightByDEPInfo(acarsLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }

        /// <summary>
        /// ����acars��ر���Ϣ��ȡ����
        /// </summary>
        /// <param name="acarsLegsBM">��ر�ʵ�����</param>
        /// <returns></returns>
        public DataTable GetFlightByARRInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //�����ݿ�����
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeLegsDA.GetFlightByARRInfo(acarsLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }

            return dataTable;
        }

        /// <summary>
        /// ��ȡ�������ļ�����DataSet����ʽ����
        /// </summary>
        /// <param name="strFullPath">����ļ�����·��</param>
        /// <returns></returns>
        public DataSet GetChangeLegsFromFile(string strFullPath)
        {
            //���巵��ֵ
            DataSet dataSet = new ScheduleLegsBM();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            try
            {
                dataSet = changeLegsDA.GetChangeLegsFromFile(strFullPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }


        #region added by LinYong

        #region ��ȡ�������еĺ��ද̬ --added in 2009.10.26 ,��ȡ tbLegs �����ֶ�
        /// <summary>
        /// ��ȡ�������еĺ��ද̬
        /// </summary>
        /// <param name="DateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetAllLegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            //�����ݿ�����
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = changeLegsDA.GetAllLegsByDay(dateTimeBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }
        #endregion

        #endregion

    }
}
