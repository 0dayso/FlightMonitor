using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// VIPҵ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-18
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class VIPBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public VIPBF()
        {
        }

        /// <summary>
        /// ��FOC��ȡ����������VIP
        /// </summary>
        /// <param name="strStartDATOP">��ʼ���� ��ʽ2007-05-12</param>
        /// <returns>�������ݼ���VIP</returns>
        public ReturnValueSF GetVIPFromFoc(string strStartDATOP)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�������෽��
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();
                rvSF.Ds = vipDAF.GetVIPFromFoc(strStartDATOP);
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
        /// ����DATOP��ȡ����VIP
        /// </summary>
        /// <param name="strDATOP">����</param>
        /// <returns>�������з���������VIP�����ݱ�</returns>
        public ReturnValueSF GetVIPByDATOP(string strDATOP)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�������෽��
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();
                rvSF.Dt = vipDAF.GetVIPByDATOP(strDATOP);
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
        /// ����VIP������Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF UpdateVipInfor(FlightMonitorBM.VIPBM vipBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.UpdateVipInfor(vipBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

         /// <summary>
        /// ����һ��VIP��Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF InsertVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�������෽��
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.InsertVIP(vipBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ɾ��һ��VIP��Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF DeleteVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�������෽��
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.DeleteVIP(vipBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// �����ÿ������ͺ�����Ϣ��ȡVIP
        /// </summary>
        /// <param name="vipBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetVIPByName(VIPBM vipBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�������෽��
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();
                rvSF.Dt = vipDAF.GetVIPByName(vipBM);
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
        /// ���VIP�Ƿ����
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns>�Զ�������</returns>
        public ReturnValueSF ValidVIP(VIPBM vipBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ����ݷ�������෽��
                VIPDAF vipDAF = new VIPDAF();
                DataTable dtVIP = vipDAF.GetVIPByName(vipBM);

                if (dtVIP.Rows.Count > 0)
                {
                    //VIP�û��Ѵ���
                    rvSF.Result = 1;                    
                }
                else
                {
                    rvSF.Result = 0;                    
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ��ȡ�����VIP��Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public ReturnValueSF GetVIPByFlight(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ����ݷ�������෽��
                VIPDAF vipDAF = new VIPDAF();
                rvSF.Dt = vipDAF.GetVIPByFlight(changeLegsBM, iDeleteTag);

                
                rvSF.Result = 1;
               
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// �߼�ɾ�������һ������
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateDeleteTag(VIPBM vipBM, int iDeleteTag)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ�������෽��
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.UpdateDeleteTag(vipBM, iDeleteTag);
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
