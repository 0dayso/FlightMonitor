using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ϯλ���������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionNameBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionNameBF()
        {
        }

        /// <summary>
        /// ��ȡ����ϯλ����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetAllPositionName()
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Dt = positionNameDAF.GetAllPositionName();
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
        /// ����һ��ϯλ
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertPositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Result = positionNameDAF.InsertPositionName(positionBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ɾ��һ��ϯλ����
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF DeletePositionName(FlightMonitorBM.PositionNameBM positionBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Result = positionNameDAF.DeletePositionName(positionBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ����ϯλ��Ż�ȡϯλ��Ϣ
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetPositionByID(FlightMonitorBM.PositionNameBM positionBM)
        {

            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            PositionNameDAF positionNameDAF = new PositionNameDAF();
            try
            {
                rvSF.Dt = positionNameDAF.GetPositionByID(positionBM);
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
