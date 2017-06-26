using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Data;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ������Ȩ�����ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class DataItemPurviewBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DataItemPurviewBF()
        {
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetDataItems()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItems();
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
        /// ��ȡ�û������������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">����Ȩ��ʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF GetDataItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItemPurviewByUserId(accountBM);
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
        /// yanxian ��ȡ�ο���
        /// </summary>
        /// <param name="dataItemPurviewBM">����Ȩ��ʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF GetDataItemPointPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItemPointPurviewByUserId(accountBM);
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
        /// �ж�������Ȩ���Ƿ��Ѿ�����
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns>�Զ��巵��ֵ</returns>
        private ReturnValueSF ExistDataItemPurview(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                DataTable dtPurview = dataItemPurviewDAF.GetDataItemPurviewByDataItemNo(dataItemPurviewBM);

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
        /// ����һ����¼
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public ReturnValueSF Insert(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF = ExistDataItemPurview(dataItemPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF.Result = dataItemPurviewDAF.Insert(dataItemPurviewBM);
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
        public ReturnValueSF UpdatePurview(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF = ExistDataItemPurview(dataItemPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF = Insert(dataItemPurviewBM);
                }
                else if (rvSF.Result > 0)
                {
                    rvSF.Result = dataItemPurviewDAF.UpdatePurview(dataItemPurviewBM);
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
        /// ����������Ŀɼ���
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateVisible(IList ilDataItemPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Result = dataItemPurviewDAF.UpdateVisible(ilDataItemPurviewBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// �������������ʾ˳��
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateIndex(IList ilDataItemPurviewBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Result = dataItemPurviewDAF.UpdateIndex(ilDataItemPurviewBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ��ѯĳ�û�ĳ�������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF GetDataItemPurviewByDataItemNo(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
             //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItemPurviewByDataItemNo(dataItemPurviewBM);
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
        /// ��ȡ�û���Ȩ�޵�������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetPurviewDataItem(FlightMonitorBM.AccountBM accountBM)
        {
              //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetPurviewDataItem(accountBM);
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
        /// ��ȡ�û����õĿɼ���������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM)
        {
              //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetVisibleDataItem(accountBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        public ReturnValueSF GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM, string strType)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetVisibleDataItem(accountBM, strType);
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
