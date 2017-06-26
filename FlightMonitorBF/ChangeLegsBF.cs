using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Data;
using System.Collections;
using AirSoft.FlightMonitor.AgentServiceBF;
using AirSoft.FlightMonitor.AgentServiceDA;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ��������̬�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-04
    /// �� �� �ˣ�����
    /// �޸����ڣ�2008-06-17
    /// ��    ����
    public class ChangeLegsBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ChangeLegsBF()
        {
        }

        #region ��ȡԭ���ද̬
        /// <summary>
        /// ��ȡԭ���ද̬
        /// </summary>
        /// <param name="changeLegsBM">���ද̬���ʵ��</param>
        /// <returns>�Զ��巵��ֵ</returns>
        public ReturnValueSF GetOriginalLegs(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();            
            try
            {
                //����������Ϊ������ȡԭ����
                #region modified by LinYong -- 20091105
                //DataTable dtFlight = null;

                //AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                //if (objRemotingObject != null)
                //{
                //    byte[] bFlight = objRemotingObject.GetFlightByKey(changeLegsBM);
                //    if (bFlight == null)
                //        throw new Exception("�������ݱ� null��");
                //    CompressionHelper compressionHelper = new CompressionHelper();
                //    dtFlight = compressionHelper.DecompressToDataTable(bFlight);
                //    if (dtFlight == null)
                //        throw new Exception("���ݽ�ѹ����");
                //}

                DataTable dtFlight = changeLegsDAF.GetFlightByKey(changeLegsBM);  //ԭ�������Ч -- �ָ� in 20160714
                #endregion

                rvSF.Result = 1;

                //���������Ϊ������ȡԭ����
                if (dtFlight.Rows.Count <= 0)
                {
                    dtFlight = changeLegsDAF.GetFlightByCombineKey(changeLegsBM);
                    rvSF.Result = 2;
                }

                rvSF.Dt = dtFlight;

                if (dtFlight.Rows.Count <= 0)
                {
                    rvSF.Result = 0;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ������ʵ����󹫹���Ϣ
        /// <summary>
        /// ������ʵ����󹫹���Ϣ
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <param name="originalLegsBM">ԭ����ʵ�����</param>
        /// <returns></returns>
        private ChangeRecordBM GetGeneralChangeRecordInformation(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            changeRecordBM.UserID = "FOC";                                      //����û�
            changeRecordBM.OldFLTID = originalLegsBM.FLTID;             //ԭ����ĺ����
            changeRecordBM.OldDATOP = originalLegsBM.DATOP;        //ԭ����ĺ�������
            changeRecordBM.OldLegNo = originalLegsBM.LEGNO;        //ԭ����ķɻ�����
            changeRecordBM.OldAC = originalLegsBM.AC;                    //ԭ����ķɻ��̺�
            changeRecordBM.NewFLTID = changeLegsBM.FLTID;            //�º���ĺ����
            changeRecordBM.NewDATOP = changeLegsBM.DATOP;       //�º���ĺ�������
            changeRecordBM.NewLegNo = changeLegsBM.LEGNO;       //�º���ķɻ�����
            changeRecordBM.NewAC = changeLegsBM.AC;                   //�º���ķɻ��̺�
            changeRecordBM.OldDepSTN = originalLegsBM.DEPSTN;    //ԭ�������ɻ���
            changeRecordBM.NewDepSTN = changeLegsBM.DEPSTN;    //�º������ɻ���
            changeRecordBM.OldArrSTN = originalLegsBM.ARRSTN;      //ԭ�����Ŀ�Ļ���
            changeRecordBM.NewArrSTN = changeLegsBM.ARRSTN;     //�º����Ŀ�Ļ���
            changeRecordBM.STD = changeLegsBM.STD;                      //STD
            changeRecordBM.ETD = changeLegsBM.ETD;                      //ETD
            changeRecordBM.STA = changeLegsBM.STA;                      //STA
            changeRecordBM.ETA = changeLegsBM.ETA;                      //ETA

            //FOCϵͳ�ı��ʱ��
            changeRecordBM.FOCOperatingTime = changeLegsBM.TSTAMP;
            //��վϵͳ�Ĵ���ʱ��
            changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //������
            changeRecordBM.ActionTag = changeLegsBM.DELACTION;

            return changeRecordBM;
        }
        #endregion

        #region �¾ɺ��ද̬�Աȣ���ȡ����������ʵ��
        /// <summary>
        /// �¾ɺ��ද̬�Աȣ���ȡ����������ʵ��
        /// </summary>
        /// <param name="changeLegsBM">����󺽰ද̬</param>
        /// <param name="originalLegsBM">���ǰ���ද̬</param>
        /// <returns>�������ʵ���б�</returns>
        public IList GetChangeRecordBM(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //����б�
            IList ilChangeRecordBM = new ArrayList();

            //�ȽϺ�����Ϣ
            if (changeLegsBM.LEGNO != originalLegsBM.LEGNO)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);                
                changeRecordBM.ChangeReasonCode = "cniLEGNO";                
                changeRecordBM.ChangeOldContent = originalLegsBM.LEGNO.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.LEGNO.ToString();
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }
        
            //�ȽϷɻ��̺�
            if (changeLegsBM.AC != originalLegsBM.AC)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcAC";                
                changeRecordBM.ChangeOldContent = originalLegsBM.AC;
                changeRecordBM.ChangeNewContent = changeLegsBM.AC;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�ȽϷɻ�����
            if (changeLegsBM.LONG_REG != originalLegsBM.LONG_REG)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);                
                changeRecordBM.ChangeReasonCode = "cnvcLONG_REG";
                changeRecordBM.ChangeOldContent = originalLegsBM.LONG_REG;
                changeRecordBM.ChangeNewContent = changeLegsBM.LONG_REG;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }          

            //�Ƚ���ɻ���
            if (changeLegsBM.DEPSTN != originalLegsBM.DEPSTN)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncDEPSTN";
                changeRecordBM.ChangeOldContent = originalLegsBM.DEPSTN;
                changeRecordBM.ChangeNewContent = changeLegsBM.DEPSTN;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�Ƚ�Ŀ�Ļ���
            if (changeLegsBM.ARRSTN != originalLegsBM.ARRSTN)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncARRSTN";
                changeRecordBM.ChangeOldContent = originalLegsBM.ARRSTN;
                changeRecordBM.ChangeNewContent = changeLegsBM.ARRSTN;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //����״̬
            if (changeLegsBM.STATUS != originalLegsBM.STATUS)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncSTATUS";
                changeRecordBM.ChangeOldContent = originalLegsBM.STATUS;
                changeRecordBM.ChangeNewContent = changeLegsBM.STATUS;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);                
            }

            //ETD
            if (changeLegsBM.ETD != originalLegsBM.ETD)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncETD";
                changeRecordBM.ChangeOldContent = DateTime.Parse(originalLegsBM.ETD).ToString("HHmm");
                changeRecordBM.ChangeNewContent = DateTime.Parse(changeLegsBM.ETD).ToString("HHmm");
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //ETA
            if (changeLegsBM.ETA != originalLegsBM.ETA)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncETA";
                changeRecordBM.ChangeOldContent = DateTime.Parse(originalLegsBM.ETA).ToString("HHmm");
                changeRecordBM.ChangeNewContent = DateTime.Parse(changeLegsBM.ETA).ToString("HHmm");
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //ATD
            if (changeLegsBM.ATD != originalLegsBM.ATD)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncATD";
                changeRecordBM.ChangeOldContent = DateTime.Parse(originalLegsBM.ATD).ToString("HHmm");
                changeRecordBM.ChangeNewContent = DateTime.Parse(changeLegsBM.ATD).ToString("HHmm");
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //TOFF
            if (changeLegsBM.TOFF != originalLegsBM.TOFF)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncTOFF";
                changeRecordBM.ChangeOldContent = DateTime.Parse(originalLegsBM.TOFF).ToString("HHmm");
                changeRecordBM.ChangeNewContent = DateTime.Parse(changeLegsBM.TOFF).ToString("HHmm");
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //TDWN
            if (changeLegsBM.TDWN != originalLegsBM.TDWN)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncTDWN";
                changeRecordBM.ChangeOldContent = DateTime.Parse(originalLegsBM.TDWN).ToString("HHmm");
                changeRecordBM.ChangeNewContent = DateTime.Parse(changeLegsBM.TDWN).ToString("HHmm");
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //ATA
            if (changeLegsBM.ATA != originalLegsBM.ATA)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncATA";
                changeRecordBM.ChangeOldContent = DateTime.Parse(originalLegsBM.ATA).ToString("HHmm");
                changeRecordBM.ChangeNewContent = DateTime.Parse(changeLegsBM.ATA).ToString("HHmm");
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }           
            
            //�����
            if (changeLegsBM.TRI_FLTID != originalLegsBM.TRI_FLTID)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcTRI_FLTID";
                changeRecordBM.ChangeOldContent = originalLegsBM.TRI_FLTID;
                changeRecordBM.ChangeNewContent = changeLegsBM.TRI_FLTID;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //������������ԭ�����
            if (changeLegsBM.DIV_RCODE != originalLegsBM.DIV_RCODE)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDIV_RCODE";
                changeRecordBM.ChangeOldContent = originalLegsBM.DIV_RCODE;
                changeRecordBM.ChangeNewContent = changeLegsBM.DIV_RCODE;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //����ԭ�����
            if (changeLegsBM.DIV_FLAG != originalLegsBM.DIV_FLAG)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDIV_FLAG";
                changeRecordBM.ChangeOldContent = originalLegsBM.DIV_FLAG;
                changeRecordBM.ChangeNewContent = changeLegsBM.DIV_FLAG;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�ÿ�
            if (changeLegsBM.PAX != originalLegsBM.PAX)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcPAX";
                changeRecordBM.ChangeOldContent = originalLegsBM.PAX;
                changeRecordBM.ChangeNewContent = changeLegsBM.PAX;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }           

            //��������
            if (changeLegsBM.BOOK != originalLegsBM.BOOK)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcBOOK";
                changeRecordBM.ChangeOldContent = originalLegsBM.BOOK;
                changeRecordBM.ChangeNewContent = changeLegsBM.BOOK;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //��һ�������
            if (changeLegsBM.DELAY1 != originalLegsBM.DELAY1)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY1";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY1;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY1;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //��һ����ʱ��
            if (changeLegsBM.DUR1 != originalLegsBM.DUR1)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR1";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR1.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR1.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�ڶ��������
            if (changeLegsBM.DELAY2 != originalLegsBM.DELAY2)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY2";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY2;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY2;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�ڶ�����ʱ��
            if (changeLegsBM.DUR2 != originalLegsBM.DUR2)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR2";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR2.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR2.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�����������
            if (changeLegsBM.DELAY3 != originalLegsBM.DELAY3)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY3";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY3;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY3;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //��������ʱ��
            if (changeLegsBM.DUR3 != originalLegsBM.DUR3)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR3";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR3.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR3.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�����������
            if (changeLegsBM.DELAY4 != originalLegsBM.DELAY4)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY4";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY4;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY4;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //��������ʱ��
            if (changeLegsBM.DUR4 != originalLegsBM.DUR4)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR4";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR4.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR4.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }           

            //�ǻ���
            if (changeLegsBM.GATE != originalLegsBM.GATE)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcGATE";
                changeRecordBM.ChangeOldContent = originalLegsBM.GATE;
                changeRecordBM.ChangeNewContent = changeLegsBM.GATE;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�������ʴ���
            if (changeLegsBM.STC != originalLegsBM.STC)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcSTC";
                changeRecordBM.ChangeOldContent = originalLegsBM.STC;
                changeRecordBM.ChangeNewContent = changeLegsBM.STC;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //Version
            if (changeLegsBM.VERSION != originalLegsBM.VERSION)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcVERSION";
                changeRecordBM.ChangeOldContent = originalLegsBM.VERSION;
                changeRecordBM.ChangeNewContent = changeLegsBM.VERSION;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //ԭʼ����
            if (changeLegsBM.ORIG_ACTYP != originalLegsBM.ORIG_ACTYP)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncORIG_ACTYP";
                changeRecordBM.ChangeOldContent = originalLegsBM.ORIG_ACTYP;
                changeRecordBM.ChangeNewContent = changeLegsBM.ORIG_ACTYP;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //����
            if (changeLegsBM.ACTYP != originalLegsBM.ACTYP)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncACTYP";
                changeRecordBM.ChangeOldContent = originalLegsBM.ACTYP;
                changeRecordBM.ChangeNewContent = changeLegsBM.ACTYP;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //�ɻ�������˾
            if (changeLegsBM.ACOWN != originalLegsBM.ACOWN)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcACOWN";
                changeRecordBM.ChangeOldContent = originalLegsBM.ACOWN;
                changeRecordBM.ChangeNewContent = changeLegsBM.ACOWN;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            return ilChangeRecordBM;
        }
        #endregion

        #region ����һ�����ද̬
        /// <summary>
        /// ����һ�����ද̬
        /// </summary>
        /// <param name="changeLegsBM">����󺽰ද̬</param>
        /// <returns></returns>
        public ReturnValueSF Insert(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //��ȡ�����Ϣ
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, changeLegsBM);
            changeRecordBM.ChangeReasonCode = "cnvcFlightNo";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = "";
            changeRecordBM.Refresh = 1;
            changeRecordBM.ActionTag = "I";
            
            try
            {                
                //�������ݷ�����۲㷽��
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF.Result = changeLegsDAF.Insert(changeLegsBM, changeRecordBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ���º��ද̬
        /// <summary>
        /// ���º��ද̬�����û����Ӧԭ��¼��Ҫ����һ�����ද̬
        /// </summary>
        /// <param name="changeLegsBM">����󺽰ද̬</param>
        /// <returns></returns>
        public ReturnValueSF Update(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();
            try
            {
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF = GetOriginalLegs(changeLegsBM);

                //��ԭ��¼
                if (rvSF.Result > 0)
                {
                    #region ��������ͬʱ���ֶ����ظ����ࣨһ�㺽�����ͬ���ɻ��Ų�ͬ����Щ����Ϊ�����ɻ�ǰ�ĺ��࣬�Ѳ�ִ�ɣ������� -- modified by LinYong in 20160707
                    //ChangeLegsBM originalLegsBM = new ChangeLegsBM(rvSF.Dt.Rows[0]); //ԭ�������Ч
                    ChangeLegsBM originalLegsBM = new ChangeLegsBM(rvSF.Dt.Rows[0]);
                    DataRow[] arrayDataRowrvSFDt = rvSF.Dt.Select("cniDeleteTag = 0");
                    if (arrayDataRowrvSFDt.Length > 0)
                    {
                        originalLegsBM = new ChangeLegsBM(arrayDataRowrvSFDt[0]);
                    }
                    #endregion ��������ͬʱ���ֶ����ظ����ࣨһ�㺽�����ͬ���ɻ��Ų�ͬ����Щ����Ϊ�����ɻ�ǰ�ĺ��࣬�Ѳ�ִ�ɣ������� -- modified by LinYong in 20160707
                    IList ilChangeRecordBM = GetChangeRecordBM(changeLegsBM, originalLegsBM);

                    if (changeLegsBM.DELACTION == "I")
                    {
                        //����δ�����仯����������Ϊ����������Ϣ
                        if (rvSF.Result == 1)
                        {
                            rvSF.Result = changeLegsDAF.UpdateAllInfoByPriKey(changeLegsBM, ilChangeRecordBM);
                        }
                        else
                        {
                            rvSF.Result = changeLegsDAF.UpdateAllInfoByComKey(changeLegsBM, originalLegsBM, ilChangeRecordBM);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(changeLegsBM.SEQ) > Convert.ToInt32(originalLegsBM.SEQ))
                        {
                            //����δ�����仯����������Ϊ����������Ϣ
                            if (rvSF.Result == 1)
                            {
                                rvSF.Result = changeLegsDAF.UpdateAllInfoByPriKey(changeLegsBM, ilChangeRecordBM);
                            }
                            else
                            {
                                rvSF.Result = changeLegsDAF.UpdateAllInfoByComKey(changeLegsBM, originalLegsBM, ilChangeRecordBM);
                            }
                        }
                    }
                }
                else if(rvSF.Result == 0)
                {
                    //���뺽�ද̬
                    rvSF = Insert(changeLegsBM);
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region �߼�ɾ��һ������
        /// <summary>
        /// �߼�ɾ��һ������
        /// </summary>
        /// <param name="changeLegsBM">����󺽰ද̬</param>
        /// <returns></returns>
        public ReturnValueSF LogicDelete(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //���巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            //��ȡ�����Ϣ
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, changeLegsBM);
            changeRecordBM.ChangeReasonCode = "cnvcFlightNo";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = "";
            changeRecordBM.Refresh = 1;
            changeRecordBM.ActionTag = "D";

            try
            {
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();

                //����������Ϊ������ȡԭ����
                #region modified by LinYong -- 20091105
                //DataTable dtFlight = null;

                //AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                //if (objRemotingObject != null)
                //{
                //    byte[] bFlight = objRemotingObject.GetFlightByKey(changeLegsBM);
                //    if (bFlight == null)
                //        throw new Exception("�������ݱ� null��");
                //    CompressionHelper compressionHelper = new CompressionHelper();
                //    dtFlight = compressionHelper.DecompressToDataTable(bFlight);
                //    if (dtFlight == null)
                //        throw new Exception("���ݽ�ѹ����");
                //}

                DataTable dtFlight = changeLegsDAF.GetFlightByKey(changeLegsBM);  //ԭ�������Ч -- �ָ� in 20160714
                #endregion

                //��ԭ��¼
                if(dtFlight.Rows.Count > 0)
                {
                    //ChangeLegsBM originalLegsBM = new ChangeLegsBM(rvSF.Dt.Rows[0]);
                    ChangeLegsBM originalLegsBM = new ChangeLegsBM(dtFlight.Rows[0]);
                    if (Convert.ToInt32(changeLegsBM.SEQ) > Convert.ToInt32(originalLegsBM.SEQ))
                    {
                        rvSF.Result = changeLegsDAF.LogicDelete(changeLegsBM, changeRecordBM);
                    }
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
        #endregion

        #region ��ȡ�������ļ�
        /// <summary>
        /// ��ȡ�������ļ�
        /// </summary>
        /// <param name="strFullPath">����ļ�����·��</param>
        /// <returns></returns>
        public ReturnValueSF GetChangeLegsFromFile(string strFullPath)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF.Ds = changeLegsDAF.GetChangeLegsFromFile(strFullPath);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #region ������Ϊ������ѯһ����¼
        /// <summary>
        /// ������Ϊ������ѯһ����¼
        /// </summary>
        /// <param name="changeLegsBM">��������̬ʵ��</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                #region modified by LinYong -- 20091105
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetFlightByKey(changeLegsBM);
                    if (bytesToDecompress == null)
                        throw new Exception("�������ݱ� null��");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("���ݽ�ѹ����");
                }
                rvSF.Dt = dtDecompressedDatatable;

                //ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();//ԭ�������Ч
                //rvSF.Dt = changeLegsDAF.GetFlightByKey(changeLegsBM);//ԭ�������Ч
                #endregion

                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion


        #region added by LinYong

        #region ��ȡ�������еĺ��ද̬ --added in 2009.10.26 ,��ȡ tbLegs �����ֶ�
        /// <summary>
        /// ��ȡ�������еĺ��ද̬
        /// </summary>
        /// <param name="DateTimeBM">��������̬ʵ��</param>
        /// <returns></returns>
        public ReturnValueSF GetAllLegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF.Dt = changeLegsDAF.GetAllLegsByDay(dateTimeBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        #endregion

        #endregion
    }
}
