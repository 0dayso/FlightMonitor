using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ����������ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-04
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ChangeRecordDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ChangeRecordDAF()
        {
        }

        /// <summary>
        /// ��ȡ���һ���������
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <returns></returns>
        public DataTable GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //���巵��ֵ
            DataTable dtLastChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtLastChangeRecords = changeRecordDA.GetLastWatchChangeRecords(iLastRecordNo, dateTimeBM, positionNameBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtLastChangeRecords;
        }

         /// <summary>
        /// ��ȡ�������¼��
        /// </summary>
        /// <returns></returns>
        public object GetMaxRecordNo()
        {
            //���巵��ֵ
            object oMaxRecordNo;

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                oMaxRecordNo = changeRecordDA.GetMaxRecordNo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return oMaxRecordNo;
        }

        /// <summary>
        /// ��վ��ȡ���һ���������
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <returns></returns>
        public DataTable GetLastGuaranteeChangeRecords(int iLastRecordNo,DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            //���巵��ֵ
            DataTable dtLastChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                if (ConfigurationManager.AppSettings[accountBM.StationThreeCode + "DistSrv"] != null)
                {
                    string strConn = ConfigurationManager.AppSettings[stationBM.ThreeCode + "DistSrv"];
                    //�������ݿ������ַ���
                    byte[] bt = Convert.FromBase64String(strConn);
                    strConn = Encoding.ASCII.GetString(bt);
                    changeRecordDA.GetConnOpen(strConn);
                }
                else
                {
                    changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                }
                //changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtLastChangeRecords = changeRecordDA.GetLastGuaranteeChangeRecords(iLastRecordNo, dateTimeBM, stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtLastChangeRecords;
        }

        /// <summary>
        /// ��ȡ��վ����100�������¼
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public DataTable GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //���巵��ֵ
            DataTable dtChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtChangeRecords = changeRecordDA.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtChangeRecords;
        }

         /// <summary>
        /// ����һ�����������¼
        /// </summary>
        /// <param name="changeRecordBM">���������¼ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public int Insert(FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            //���巵��ֵ
            int retVal = -1;

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = changeRecordDA.Insert(changeRecordBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return retVal;
        }

       
        /// <summary>
        /// ���ݺ������ڡ�����źͱ�����Ͳ�ѯ�����¼
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strFlightNo"></param>
        /// <param name="strChangeReason"></param>
        /// <returns></returns>
        public DataTable GetChangeRecordsByFlightNo(DateTimeBM dateTimeBM, string strFlightNo, string strChangeReason)
        {
            //���巵��ֵ
            DataTable dtChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtChangeRecords = changeRecordDA.GetChangeRecordsByFlightNo(dateTimeBM, strFlightNo, strChangeReason);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtChangeRecords;
        }


        #region added by LinYong

        #region ��ȡ���һ��������� ��vw_FlightChangeRecord�� --added in 2009.10.28
       /// <summary>
        /// ��ȡ���һ��������� ��vw_FlightChangeRecord��
       /// </summary>
        /// <param name="dateTimeBM">ʱ��������� StartDateTime Ϊ��ȡ��ʱ���</param>
       /// <returns></returns>
        public DataTable GetLastvw_FlightChangeRecord(DateTimeBM dateTimeBM)
        {
            //���巵��ֵ
            DataTable dtLastChangeRecords = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                //if (ConfigurationManager.AppSettings[accountBM.StationThreeCode + "DistSrv"] != null)
                //{
                //    string strConn = ConfigurationManager.AppSettings[stationBM.ThreeCode + "DistSrv"];
                //    //�������ݿ������ַ���
                //    byte[] bt = Convert.FromBase64String(strConn);
                //    strConn = Encoding.ASCII.GetString(bt);
                //    changeRecordDA.GetConnOpen(strConn);
                //}
                //else
                //{
                //    changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                //}
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtLastChangeRecords = changeRecordDA.GetLastvw_FlightChangeRecord(dateTimeBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return dtLastChangeRecords;
        }
        #endregion

        #region ��ȡ���һ����¼��ָ���û��� -- added by LinYong in 20150420
        /// <summary>
        /// ��ȡ���һ����¼��ָ���û���
        /// </summary>
        /// <param name="UserID">�û��ʺ�</param>
        /// <returns></returns>
        public DataTable GetLastRecord(string UserID)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();

            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            try
            {
                //�����ݿ�����
                changeRecordDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeRecordDA.GetLastRecord(UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }
            return dataTable;
        }
        #endregion ��ȡ���һ����¼��ָ���û��� -- added by LinYong in 20150420


        #endregion
    }
}
