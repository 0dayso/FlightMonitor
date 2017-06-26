using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// �˵�Ȩ�����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class MenuPurviewBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuPurviewBF()
        {
        }

        /// <summary>
        /// ��ȡ���еĲ˵���
        /// </summary>
        /// <returns>�������в˵�������ݱ�</returns>
        public ReturnValueSF GetMenuItems()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF.Dt = menuPurviewDAF.GetMenuItems();
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ��ȡ�˵���Ȩ��
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetMenuItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF.Dt = menuPurviewDAF.GetMenuItemPurview(accountBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

         /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public ReturnValueSF Insert(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF = ExistMenuPurview(menuPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF.Result = menuPurviewDAF.Insert(menuPurviewBM);
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// ����������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdatePurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)        
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();
            try
            {
                rvSF = ExistMenuPurview(menuPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF = Insert(menuPurviewBM);
                }
                else if (rvSF.Result > 0)
                {
                    rvSF.Result = menuPurviewDAF.UpdatePurview(menuPurviewBM);
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// �жϲ˵���Ȩ���Ƿ��Ѿ�����
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns>�Զ��巵��ֵ</returns>
        private ReturnValueSF ExistMenuPurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();
            try
            {
                DataTable dtPurview = menuPurviewDAF.GetMenuPurviewByMenuID(menuPurviewBM);

                if (dtPurview.Rows.Count == 0)
                {
                    rvSF.Result = 0;
                }
                else
                {
                    rvSF.Result = 1;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ��ѯĳ�û�ĳ�˵����Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF GetMenuPurviewByMenuID(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.MenuPurviewDAF menuPurviewDAF = new MenuPurviewDAF();

            try
            {
                rvSF.Dt = menuPurviewDAF.GetMenuPurviewByMenuID(menuPurviewBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

    }
}
