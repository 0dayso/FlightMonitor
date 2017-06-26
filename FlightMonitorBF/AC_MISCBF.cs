using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ACARS���ද̬�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class AC_MISCBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AC_MISCBF()
        {
        }

        /// <summary>
        /// ��ȡĳϯλû��ѡ��ķɻ���
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetAirCraftByPositionId(PositionNameBM positionBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Dt = ac_miscDAF.GetAirCraftByPositionId(positionBM);
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
        /// �����·ɻ�
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertACMISC(AC_MISCBM ac_MISCBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            DataTable dtACMISC = ac_miscDAF.GetACMISCByAC(ac_MISCBM);

            if (dtACMISC.Rows.Count == 0)
            {
                try
                {
                    rvSF.Result = ac_miscDAF.InsertACMISC(ac_MISCBM);
                }
                catch (Exception ex)
                {
                    rvSF.Result = -1;
                    rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
                }
            }
            else
            {
                rvSF.Result = 2;
            }

            return rvSF;            
        }

         /// <summary>
        /// ɾ���ɻ�
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public ReturnValueSF DeleteACMISC(AC_MISCBM ac_MISCBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Result = ac_miscDAF.DeleteACMISC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;

        }

         /// <summary>
        /// ��ȡ���зɻ��б�
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetACMISC()
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Result = 1;
                rvSF.Dt = ac_miscDAF.GetACMISC();
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ��ȡ���зɻ�����
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetACMISCGroupBy()
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //���ݷ��������
            AC_MISCDAF ac_miscDAF = new AC_MISCDAF();

            try
            {
                rvSF.Result = 1;
                rvSF.Dt = ac_miscDAF.GetACMISCGroupBy();
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
