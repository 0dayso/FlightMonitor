using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ACARS���ද̬�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-16
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSLegsBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ACARSLegsBF()
        {
        }

        /// <summary>
        /// ����ACARS���ද̬��ȡԭ���ද̬
        /// </summary>
        /// <param name="acarsBM">ACARSʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF GetOriginalLegs(FlightMonitorBM.ACARSLegsBM acarsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();

            try
            {
                if (acarsBM.MessageType == MsgType.OUT || acarsBM.MessageType == MsgType.OFF)
                {
                    rvSF.Dt = changeLegsDAF.GetFlightByDEPInfo(acarsBM);
                }
                else if (acarsBM.MessageType == MsgType.ON || acarsBM.MessageType == MsgType.IN)
                {
                    rvSF.Dt = changeLegsDAF.GetFlightByARRInfo(acarsBM);
                }
                else
                {
                    rvSF.Result = -1;
                }

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
        /// ������ʵ����󹫹���Ϣ
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <param name="originalLegsBM">ԭ����ʵ�����</param>
        /// <returns></returns>
        private ChangeRecordBM GetGeneralChangeRecordInformation(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            changeRecordBM.UserID = "ACARS";
            changeRecordBM.OldFLTID = originalLegsBM.FLTID;
            changeRecordBM.OldDATOP = originalLegsBM.DATOP;
            changeRecordBM.OldLegNo = originalLegsBM.LEGNO;
            changeRecordBM.OldAC = originalLegsBM.AC;
            changeRecordBM.NewFLTID = changeLegsBM.FLTID;
            changeRecordBM.NewDATOP = changeLegsBM.DATOP;
            changeRecordBM.NewLegNo = changeLegsBM.LEGNO;
            changeRecordBM.NewAC = changeLegsBM.AC;
            changeRecordBM.OldDepSTN = originalLegsBM.DEPSTN;
            changeRecordBM.NewDepSTN = changeLegsBM.DEPSTN;
            changeRecordBM.OldArrSTN = originalLegsBM.ARRSTN;
            changeRecordBM.NewArrSTN = changeLegsBM.ARRSTN;
            changeRecordBM.STD = changeLegsBM.STD;
            changeRecordBM.ETD = changeLegsBM.ETD;
            changeRecordBM.STA = changeLegsBM.STA;
            changeRecordBM.ETA = changeLegsBM.ETA;

            changeRecordBM.FOCOperatingTime = changeLegsBM.TSTAMP;
            changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            changeRecordBM.ActionTag = changeLegsBM.DELACTION;

            return changeRecordBM;
        }
        

        /// <summary>
        /// ���ı��ļ���ȡ���ද̬��Ϣ
        /// </summary>
        /// <param name="strFullPath">�ļ�·��</param>
        /// <returns>ACARS���ද̬�ı���Ϣ</returns>
        public ReturnValueSF GetACARSLegsBMFromFile(string strFullPath)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();
            try
            {
                rvSF.Message = acarsLegsDAF.GetACARSLegsBMFromFile(strFullPath);
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
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public ReturnValueSF UpdateACARSOnInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //���ʵ������б�
            IList ilChangeRecordBM = new ArrayList();

            //���ʱ������Ϣ
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(originalLegsBM, originalLegsBM);
            changeRecordBM.ChangeReasonCode = "cncACARSTDWN";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = DateTime.Parse(acarsLegsBM.TDWN).ToString("HHmm");
            changeRecordBM.ActionTag = "U";
            ilChangeRecordBM.Add(changeRecordBM);            

            try
            {
                rvSF.Result = acarsLegsDAF.UpdateACARSOnInfo(acarsLegsBM, originalLegsBM, ilChangeRecordBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public ReturnValueSF UpdateACARSInInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //���ʵ������б�
            IList ilChangeRecordBM = new ArrayList();

           
            //���յ�ʱ������Ϣ
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(originalLegsBM, originalLegsBM);
            changeRecordBM.ChangeReasonCode = "cncACARSATA";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = DateTime.Parse(acarsLegsBM.ATA).ToString("HHmm");
            changeRecordBM.ActionTag = "U";
            ilChangeRecordBM.Add(changeRecordBM);

            try
            {
                rvSF.Result = acarsLegsDAF.UpdateACARSInInfo(acarsLegsBM, originalLegsBM, ilChangeRecordBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }


         /// <summary>
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public ReturnValueSF UpdateACARSOutInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //���ʵ������б�
            IList ilChangeRecordBM = new ArrayList();

            //�Ƴ�ʱ������Ϣ
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(originalLegsBM, originalLegsBM);
            changeRecordBM.ChangeReasonCode = "cncACARSOUT";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = DateTime.Parse(acarsLegsBM.PushTime).ToString("HHmm");
            changeRecordBM.ActionTag = "U";
            ilChangeRecordBM.Add(changeRecordBM);

            try
            {
                rvSF.Result = acarsLegsDAF.UpdateACARSOutInfo(acarsLegsBM, originalLegsBM, ilChangeRecordBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ���º��ද̬��ACARS��Ϣ
        /// </summary>
        /// <param name="acarsLegsBM">ACARS���ද̬ʵ�����</param>
        /// <param name="originalLegsBM">ԭ���ද̬ʵ�����</param>
        /// <param name="ilChangeRecordBM">������ʵ������б�</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public ReturnValueSF UpdateACARSOffInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //���ʵ������б�
            IList ilChangeRecordBM = new ArrayList();
           

            //���ʱ��ʱ������Ϣ
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(originalLegsBM, originalLegsBM);
            changeRecordBM.ChangeReasonCode = "cncACARSTOFF";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = DateTime.Parse(acarsLegsBM.TOFF).ToString("HHmm");
            changeRecordBM.ActionTag = "U";
            ilChangeRecordBM.Add(changeRecordBM);


            try
            {
                rvSF.Result = acarsLegsDAF.UpdateACARSOffInfo(acarsLegsBM, originalLegsBM, ilChangeRecordBM);
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
