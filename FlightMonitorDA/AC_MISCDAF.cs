using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// �ɻ���Ϣ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class AC_MISCDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AC_MISCDAF()
        {
        }

         /// <summary>
        /// ��ȡĳϯλû��ѡ��ķɻ���
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public DataTable GetAirCraftByPositionId(PositionNameBM positionBM)
        {
            //���巵��ֵ
            DataTable dtAirCraft = new DataTable();
            AC_MISCDA ac_miscDA = new AC_MISCDA();

            try
            {
                ac_miscDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtAirCraft = ac_miscDA.GetAirCraftByPositionId(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_miscDA.ConnClose();
            }

            return dtAirCraft;
        }

        /// <summary>
        /// �����·ɻ�
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public int InsertACMISC(AC_MISCBM ac_MISCBM)
        {
            int retVal = -1;
            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                retVal = ac_MISCDA.InsertACMISC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ɾ���ɻ�
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public int DeleteACMISC(AC_MISCBM ac_MISCBM)
        {
            int retVal = -1;
            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                retVal = ac_MISCDA.DeleteACMISC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ���ݶ̺Ż�ȡһ����¼
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public DataTable GetACMISCByAC(AC_MISCBM ac_MISCBM)
        {
            DataTable dtACMISC = new DataTable();       // ���巵��ֵ

            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                dtACMISC = ac_MISCDA.GetACMISCByAC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return dtACMISC;
        }

        /// <summary>
        /// ��ȡ���зɻ��б�
        /// </summary>
        /// <returns></returns>
        public DataTable GetACMISC()
        {
            DataTable dtACMISC = new DataTable();       // ���巵��ֵ

            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                dtACMISC = ac_MISCDA.GetACMISC();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return dtACMISC;
        }

        /// <summary>
        /// ��ȡ���зɻ��б�
        /// </summary>
        /// <returns></returns>
        public DataTable GetACMISCGroupBy()
        {
            DataTable dtACMISC = new DataTable();       // ���巵��ֵ

            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //�����ݿ�����
                dtACMISC = ac_MISCDA.GetACMISCGroupBy();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return dtACMISC;
        }
    }
}
