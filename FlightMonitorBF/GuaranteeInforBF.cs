using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;
using System.Data;
using AirSoft.FlightMonitor.AgentServiceDA;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ���ౣ����Ϣҵ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-31
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class GuaranteeInforBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public GuaranteeInforBF()
        {
        }

        /// <summary>
        /// ��ȡĳϯλ��������к���
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="positionNameBM">ϯλ����ʵ�����</param>
        /// <returns>��ϯλ�����к���</returns>
        public ReturnValueSF GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();

            try
            {
                rvSF.Dt = guaranteeInforDAF.GetFlightsByPosition(dateTimeBM, positionNameBM);
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
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetFlightByKey(changeLegsBM);
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
        /// ��ȡĳ��վ�Ľ����ۺ���
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="stationBM">ϯλ����ʵ�����</param>
        /// <returns>�ú�վ�����к���</returns>
        public ReturnValueSF GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetFlightsByStation(dateTimeBM, stationBM);
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
        /// ����ĳ�������
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateGuaranteeInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Result = guaranteeInforDAF.UpdateGuaranteeInfor(maintenGuaranteeInforBM);               
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// ͬʱ���¶�������
        /// </summary>
        /// <param name="ilMaintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateGuaranteeInforList(IList ilMaintenGuaranteeInforBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Result = guaranteeInforDAF.UpdateGuaranteeInforList(ilMaintenGuaranteeInforBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// ����ĳ��ද̬����
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateLegsInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Result = guaranteeInforDAF.UpdateLegsInfor(maintenGuaranteeInforBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }



          /// <summary>
        /// ��ȡ�������еĺ��ද̬
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetAllLegsByDay(DateTimeBM dateTimeBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetAllLegsByDay(dateTimeBM);
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
        /// ��ȡ�����ֵ����Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetCheckInfor(ChangeLegsBM changeLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetCheckInfor(changeLegsBM);
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
        /// �����ÿ�ֵ����Ϣ
        /// </summary>
        /// <param name="checkPaxBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateCheckPaxInfor(CheckPaxBM checkPaxBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Result = guaranteeInforDAF.UpdateCheckPaxInfor(checkPaxBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

         /// <summary>
        /// ��ȡ�����ֵ����Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetPaxNameList(ChangeLegsBM changeLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetPaxNameList(changeLegsBM);
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
        /// �����ÿ�������Ϣ
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdatePaxNameList(PaxNameListBM paxNameListBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Result = guaranteeInforDAF.UpdatePaxNameList(paxNameListBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// ��ȡ��ת�����ÿ���Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetTrasitPaxList(ChangeLegsBM changeLegsBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetTrasitPaxList(changeLegsBM);
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
        /// ������ת�ÿ���Ϣ
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateTrasitPax(TrasitPaxBM trasitPaxBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Result = guaranteeInforDAF.UpdateTrasitPax(trasitPaxBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

         /// <summary>
        /// �����λ���Ŷ�̬
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetEndFlight(DateTimeBM endDateTimeBM, DateTimeBM startDateTime, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetEndFlight(endDateTimeBM, startDateTime, accountBM);
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
        /// ��ȡĳ�ݷɻ��������ɵ����к���
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strLONG_REG"></param>
        /// <returns></returns>
        public ReturnValueSF GetAircraftFlights(DateTimeBM dateTimeBM, string strLONG_REG)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetAircraftFlights(dateTimeBM, strLONG_REG);
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
        /// ��ȡ��վ�����ۺ���
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetStationFlight(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetStationFlight(dateTimeBM, stationBM);
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
        /// ���ݺ���ź����Ŀ�Ļ�����ѯ����ƻ�
        /// </summary>
        /// <param name="dateTimeBM">�¼���Χ</param>
        /// <param name="strDEPSTN">��ɻ���������</param>
        /// <param name="strARRSTN">Ŀ�Ļ���������</param>
        /// <param name="strFlightNo">�����</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightsByFlightNo(DateTimeBM dateTimeBM, string strDEPSTN, string strARRSTN, string strFlightNo)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetFlightsByFlightNo(dateTimeBM, strDEPSTN, strARRSTN, strFlightNo);
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
        /// ��ȡ����������Ϣ
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetJoinFlightNo(DateTimeBM dateTimeBM, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Result = guaranteeInforDAF.GetJoinFlightNo(dateTimeBM, accountBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

         /// <summary>
        /// ������ͳ�Ƴ����ÿ�����
        /// </summary>
        /// <param name="dateTimeBM">ʱ�䷶Χ</param>
        /// <param name="strDEPSTN">ʼ��վ������</param>
        /// <returns></returns>
        public ReturnValueSF GetStatisticPax(DateTimeBM dateTimeBM, string strDEPSTN)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetStatisticPax(dateTimeBM, strDEPSTN);
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
        /// ���ݵ���ʱ���ȡ����ĺ��ද̬
        /// </summary>
        /// <param name="strFlightDate">��������</param>
        /// <returns></returns>
        public ReturnValueSF GetLegsByFlightDate(string strStartFlightDate, string strEndFlightDate)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetLegsByFlightDate(strStartFlightDate, strEndFlightDate);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }


        #region added by LinYong

        #region ��ȡĳ��ʱ��������еĺ��ࣨvw_Legs�� --added in 2009.10.27
        /// <summary>
        /// ��ȡĳ��ʱ��������еĺ��ࣨvw_Legs��
        /// </summary>
        /// <param name="DateTimeBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetAllvw_LegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                rvSF.Dt = guaranteeInforDAF.GetAllvw_LegsByDay(dateTimeBM);
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

        #region ��ȡĳ��վ�Ľ����ۺ���
        /// <summary>
        /// ��ȡĳ��վ�Ľ����ۺ���
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="stationBM">ϯλ����ʵ�����</param>
        /// <returns>�ú�վ�����к���</returns>
        public ReturnValueSF GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //�������ݷ��ʲ�����෽��
                #region modified by LinYong -- 20091106
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
                    if (bytesToDecompress == null)
                        throw new Exception("�������ݱ� null��");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("���ݽ�ѹ����");
                }
                rvSF.Dt = dtDecompressedDatatable;

                //GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                //rvSF.Dt = guaranteeInforDAF.GetFlightsByStation(dateTimeBM, stationBM);
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


        #region ������Ϊ������ѯһ����¼��ʹ�� ReMoting ����  --added in 2015.02.06
        /// <summary>
        /// ������Ϊ������ѯһ����¼��ʹ�� ReMoting ����
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <param name="accountBM">��½�ʺ�ʵ��</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetFlightByKey(changeLegsBM, accountBM);
                    if (bytesToDecompress == null)
                        throw new Exception("�������ݱ� null��");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("���ݽ�ѹ����");
                }
                else
                {
                    throw new Exception("AgentServiceDAF.objRemotingObject == null");
                }


                rvSF.Dt = dtDecompressedDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        #endregion ������Ϊ������ѯһ����¼��ʹ�� ReMoting ����  --added in 2015.02.06

        #region ������Ϊ������ѯһ����¼��ʹ�� ReMoting ���󣬷��ص����ݲ�����ѹ��  --added in 2015.02.10
        /// <summary>
        /// ������Ϊ������ѯһ����¼��ʹ�� ReMoting ���󣬷��ص����ݲ�����ѹ��  --added in 2015.02.10
        /// </summary>
        /// <param name="changeLegsBM">������ʵ�����</param>
        /// <param name="accountBM">��½�ʺ�ʵ��</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightByKey_NotCompress(FlightMonitorBM.ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {

                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.GetFlightByKey_NotCompress(changeLegsBM, accountBM);
                    if (dtDatatable == null)
                        throw new Exception("�������ݱ� null��");
                }
                else
                {
                    throw new Exception("AgentServiceDAF.objRemotingObject == null");
                }


                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }


            //
            return rvSF;
        }

        #endregion ������Ϊ������ѯһ����¼��ʹ�� ReMoting ���󣬷��ص����ݲ�����ѹ��  --added in 2015.02.10

        #region ���ݱ��Ľ�������Ϣȷ������  --added in 2015.04.15
        /// <summary>
        /// ���ݱ��Ľ�������Ϣȷ������  --added in 2015.04.15
        /// </summary>
        /// <param name="FlightNo">�����</param>
        /// <param name="ST">�ƻ����ʱ�䣨IO��OUT�����ƻ�����ʱ�䣨IO��IN��</param>
        /// <param name="STN">��ɻ�����IO��OUT�������������IO��IN��</param>
        /// <param name="IO">���ۺ��ࣨIO��OUT�������ۺ��ࣨIO��IN��</param>
        /// <returns>ȷ���ĺ���</returns>
        public ReturnValueSF GetFlightsByMessage(string FlightNo, string ST, string STN, string IO)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {

                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.GetFlightsByMessage(FlightNo, ST, STN, IO);
                    if (dtDatatable == null)
                        throw new Exception("�������ݱ� null��");
                }
                else
                {
                    throw new Exception("AgentServiceDAF.objRemotingObject == null");
                }


                rvSF.Dt = dtDatatable;
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = ex.Message;
            }


            //
            return rvSF;
        }

        #endregion ���ݱ��Ľ�������Ϣȷ������  --added in 2015.04.15

        #endregion

    }
}
