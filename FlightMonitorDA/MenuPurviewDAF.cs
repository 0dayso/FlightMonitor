using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// �˵�Ȩ�޷��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class MenuPurviewDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuPurviewDAF()
        {
        }

         /// <summary>
        /// ��ȡ���еĲ˵���
        /// </summary>
        /// <returns>�������в˵�������ݱ�</returns>
        public DataTable GetMenuItems()
        {
            //���巵��ֵ
            DataTable dtMenuItems = new DataTable();
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();

            try
            {
                //�����ݿ�����
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtMenuItems = menuPurviewDA.GetMenuItems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }

            return dtMenuItems;
        }

        /// <summary>
        /// ��ȡ�˵���Ȩ��
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetMenuItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            //���巵��ֵ
            DataTable dtMenuItems = new DataTable();
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();

            try
            {
                //�����ݿ�����
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtMenuItems = menuPurviewDA.GetMenuItemPurview(accountBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }

            return dtMenuItems;
        }

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public int Insert(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //���巵��ֵ
            int retVal = -1;
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();
            try
            {
                //�����ݿ�����
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = menuPurviewDA.Insert(menuPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// ����������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //���巵��ֵ
            int retVal = -1;
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();
            try
            {
                //�����ݿ�����
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                retVal = menuPurviewDA.UpdatePurview(menuPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// ��ѯĳ�û�ĳ�˵����Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns></returns>
        public DataTable GetMenuPurviewByMenuID(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //���巵��ֵ
            DataTable dtMenuItems = new DataTable();
            MenuPurviewDA menuPurviewDA = new MenuPurviewDA();

            try
            {
                //�����ݿ�����
                menuPurviewDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtMenuItems = menuPurviewDA.GetMenuPurviewByMenuID(menuPurviewBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                menuPurviewDA.ConnClose();
            }

            return dtMenuItems;
        }
    }
}
