using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ϯλ��Ϣ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionInforBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionInforBF()
        {
        }

        /// <summary>
        /// ����ϯλ��Ż�ȡ���ڸ�ϯλ�����зɻ�����Ϣ
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetInforByPositionId(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            PositionInforDAF positionInforDAF = new PositionInforDAF();

            try
            {
                rvSF.Dt = positionInforDAF.GetInforByPositionId(positionNameBM);
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
        /// ��ĳϯλ����ɻ�
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertPositionInfors(FlightMonitorBM.PositionNameBM positionNameBM, IList ilPositionInforBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            PositionInforDAF positionInforDAF = new PositionInforDAF();
            try
            {
                rvSF.Result = positionInforDAF.InsertPositionInfors(positionNameBM, ilPositionInforBM);
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
