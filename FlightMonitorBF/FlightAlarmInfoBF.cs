using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using AirSoft.FlightMonitor.AgentServiceDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Configuration;
using System.Data;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ����澯��Ϣ���ʷ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-07-08
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class FlightAlarmInfoBF
    {
        #region ���뺽��澯��Ϣ��¼
        /// <summary>
        /// ���뺽��澯��Ϣ��¼
        /// </summary>
        /// <param name="flightAlarmInfoBM">����澯��Ϣʵ������ṩ������Ϣ</param>
        /// <returns></returns>
        public ReturnValueSF Add(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();

            //�������ݷ�����۲㷽��
            try
            {
                rvSF.Result = flightAlarmInfoDAF.Add(flightAlarmInfoBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_ADD_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_ADD_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }
            return rvSF;
        }
        #endregion ���뺽��澯��Ϣ��¼

        #region ���º���澯��Ϣ��¼
        /// <summary>
        /// ���º���澯��Ϣ��¼
        /// </summary>
        /// <param name="flightAlarmInfoBM">����澯��Ϣʵ������ṩ������Ϣ</param>
        /// <returns></returns>
        public ReturnValueSF Update(FlightAlarmInfoBM flightAlarmInfoBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();

            //�������ݷ�����۲㷽��
            try
            {
                rvSF.Result = flightAlarmInfoDAF.Update(flightAlarmInfoBM);
                if (rvSF.Result > 0)
                {
                    rvSF.Message = SysConstBM.SYS_ADD_SUCCESS;
                }
                else
                {
                    rvSF.Message = SysConstBM.SYS_ADD_FALSE;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }
            return rvSF;
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
        public ReturnValueSF Select(
            string cncOutDATOP,
            string cnvcOutFLTID,
            int cniOutLEGNO,
            string cnvcOutAC,
            string cncInDATOP,
            string cnvcInFLTID,
            int cniInLEGNO,
            string cnvcInAC)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();
                rvSF.Dt = flightAlarmInfoDAF.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
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
        public ReturnValueSF Select(
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
            ReturnValueSF rvSF = new ReturnValueSF();   //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();
                rvSF.Dt = flightAlarmInfoDAF.Select(
                    cncOutDATOP,
                    cnvcOutFLTID,
                    cniOutLEGNO,
                    cnvcOutAC,
                    cncInDATOP,
                    cnvcInFLTID,
                    cniInLEGNO,
                    cnvcInAC,
                    cnvcAlarmCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion ���ݹؼ�������ȡ����澯��Ϣ��¼

        #region ���ݺ����������������ȡ����澯��Ϣ��¼
        /// <summary>
        /// ���ݺ����������������ȡ����澯��Ϣ��¼
        /// </summary>
        /// <param name="FlightDate_Start">��ʼ����</param>
        /// <param name="FlightDate_End">��������</param>
        /// <returns></returns>
        public ReturnValueSF Select(
            string FlightDate_Start,
            string FlightDate_End)
        {
            ReturnValueSF rvSF = new ReturnValueSF();   //���巵��ֵ
            try
            {
                //�������ݷ��ʲ�����෽��
                FlightAlarmInfoDAF flightAlarmInfoDAF = new FlightAlarmInfoDAF();
                rvSF.Dt = flightAlarmInfoDAF.Select(
                    FlightDate_Start,
                    FlightDate_End);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }

            return rvSF;
        }
        #endregion ���ݺ����������������ȡ����澯��Ϣ��¼
    }
}
