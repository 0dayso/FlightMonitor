using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ϯλ�������ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionNameDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionNameDAF()
        {
        }

        /// <summary>
        /// ��ȡ����ϯλ����
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPositionName()
        {
            //���巵��ֵ
            DataTable dtPositionName = new DataTable();

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //�����ݿ�����
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtPositionName = positionNameDA.GetAllPositionName();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return dtPositionName;
        }

        /// <summary>
        /// ����һ��ϯλ
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public int InsertPositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //���巵��ֵ
            int retVal = -1;

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //�����ݿ�����
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = positionNameDA.InsertPositionName(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ɾ��һ��ϯλ����
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public int DeletePositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //���巵��ֵ
            int retVal = -1;

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //�����ݿ�����
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = positionNameDA.DeletePositionName(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ����ϯλ��Ż�ȡϯλ��Ϣ
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public DataTable GetPositionByID(FlightMonitorBM.PositionNameBM positionBM)
        {
            //���巵��ֵ
            DataTable dtPositionName = new DataTable();

            PositionNameDA positionNameDA = new PositionNameDA();

            try
            {
                //�����ݿ�����
                positionNameDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtPositionName = positionNameDA.GetPositionByID(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionNameDA.ConnClose();
            }

            return dtPositionName;
        }
    }
}
