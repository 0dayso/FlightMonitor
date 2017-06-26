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
    /// ����澯��Ϣ���������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-07-08
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class FlightAlarmInfoDAF
    {
        #region ���뺽��澯��Ϣ��¼
        /// <summary>
        /// ���뺽��澯��Ϣ��¼
        /// </summary>
        /// <param name="flightAlarmInfoBM">����澯��Ϣʵ������ṩ������Ϣ</param>
        /// <returns></returns>
        public int Add(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //���巵��ֵ
            int retVal = -1;
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            //�����ݿ�����
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                return flightAlarmInfoDA.Add(flightAlarmInfoBM);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return retVal;
        }
        #endregion ���뺽��澯��Ϣ��¼

        #region ���º���澯��Ϣ��¼
        /// <summary>
        /// ���º���澯��Ϣ��¼
        /// </summary>
        /// <param name="flightAlarmInfoBM">����澯��Ϣʵ������ṩ������Ϣ</param>
        /// <returns></returns>
        public int Update(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //���巵��ֵ
            int retVal = -1;
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            //�����ݿ�����
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                return flightAlarmInfoDA.Update(flightAlarmInfoBM);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return retVal;
        }
        #endregion ���º���澯��Ϣ��¼

        #region ���ݹؼ�������ȡ����澯��Ϣ��¼
        /// <summary>
        /// ���ݹؼ�������ȡ����澯��Ϣ��¼
        /// </summary>
        /// <param name="cncOutDATOP"></param>
        /// <param name="cnvcOutFLTID"></param>
        /// <param name="cniOutLEGNO"></param>
        /// <param name="cnvcOutAC"></param>
        /// <param name="cncInDATOP"></param>
        /// <param name="cnvcInFLTID"></param>
        /// <param name="cniInLEGNO"></param>
        /// <param name="cnvcInAC"></param>
        /// <returns></returns>
        public DataTable Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();
            //�����ݿ�����
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = flightAlarmInfoDA.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return dataTable;
        }
        #endregion ���ݹؼ�������ȡ����澯��Ϣ��¼

        #region ���ݹؼ�������ȡ����澯��Ϣ��¼
        /// <summary>
        /// ���ݹؼ�������ȡ����澯��Ϣ��¼
        /// </summary>
        /// <param name="cncOutDATOP"></param>
        /// <param name="cnvcOutFLTID"></param>
        /// <param name="cniOutLEGNO"></param>
        /// <param name="cnvcOutAC"></param>
        /// <param name="cncInDATOP"></param>
        /// <param name="cnvcInFLTID"></param>
        /// <param name="cniInLEGNO"></param>
        /// <param name="cnvcInAC"></param>
        /// <returns></returns>
        public DataTable Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC,
            string cnvcAlarmCode)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();
            //�����ݿ�����
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = flightAlarmInfoDA.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC,
                    cnvcAlarmCode);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return dataTable;
        }
        #endregion ���ݹؼ�������ȡ����澯��Ϣ��¼

        #region ���ݺ����������������ȡ����澯��Ϣ��¼
        /// <summary>
        /// ���ݺ����������������ȡ����澯��Ϣ��¼
        /// </summary>
        /// <param name="FlightDate_Start">��ʼ����</param>
        /// <param name="FlightDate_End">��������</param>
        /// <returns></returns>
        public DataTable Select(
            string FlightDate_Start,
            string FlightDate_End)
        {
            //���巵��ֵ
            DataTable dataTable = new DataTable();
            //�����ݿ�����
            FlightAlarmInfoDA flightAlarmInfoDA = new FlightAlarmInfoDA();
            flightAlarmInfoDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = flightAlarmInfoDA.Select(
                    FlightDate_Start,
                    FlightDate_End);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                flightAlarmInfoDA.ConnClose();
            }

            return dataTable;
        }
        #endregion ���ݺ����������������ȡ����澯��Ϣ��¼


    }
}
