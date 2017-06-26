using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ACARS���ද̬���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSLegsDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ACARSLegsDAF()
        {
        }

        /// <summary>
        /// ���ı��ļ���ȡ���ද̬��Ϣ
        /// </summary>
        /// <param name="strFullPath">�ļ�·��</param>
        /// <returns>ACARS���ද̬�ı���Ϣ</returns>
        public string GetACARSLegsBMFromFile(string strFullPath)
        {
            //���巵��ֵ
            string strACARSLegsInfo = "";
            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            try
            {
                strACARSLegsInfo = acarsLegsDA.GetACARSLegsBMFromFile(strFullPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strACARSLegsInfo;
        }

         /// <summary>
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSOnInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //���巵��ֵ
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //�����ݿ�����
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //����ACARS���ද̬
                acarsLegsDA.UpdateACARSOnInfo(acarsLegsBM, originalLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //��¼ACARS����
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                //�ύ����
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSInInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //���巵��ֵ
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //�����ݿ�����
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //����ACARS���ද̬
                acarsLegsDA.UpdateACARSInInfo(acarsLegsBM, originalLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }


                //��¼ACARS����
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



                //�ύ����
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSOutInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //���巵��ֵ
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //�����ݿ�����
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //����ACARS���ද̬
                acarsLegsDA.UpdateACARSOutInfo(acarsLegsBM, originalLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //��¼ACARS����
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



                //�ύ����
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int UpdateACARSOffInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //���巵��ֵ
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //�����ݿ�����
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //����ACARS���ද̬
                acarsLegsDA.UpdateACARSOffInfo(acarsLegsBM, originalLegsBM);

                //���뺽�������¼
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //��¼ACARS����
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



                //�ύ����
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }
    }
}
