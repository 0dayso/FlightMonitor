using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;
using AirSoft.FlightMonitor.AgentServiceDA;
using CompressDataSet.Common;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ���������¼�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-04
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ChangeRecordBF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ChangeRecordBF()
        {
        }

         /// <summary>
        /// ��ȡ���һ���������
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <returns></returns>
        public ReturnValueSF GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();

            try
            {
                rvSF.Dt = changeRecordDAF.GetLastWatchChangeRecords(iLastRecordNo,dateTimeBM, positionNameBM);
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
        /// ��ȡ�������¼��
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetMaxRecordNo()
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();

            try
            {
                rvSF.Result = Convert.ToInt32(changeRecordDAF.GetMaxRecordNo());
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ��վ��ȡ���һ���������
        /// </summary>
        /// <param name="iLastRecordNo">ϵͳ�Ѿ���������ı�����</param>
        /// <returns></returns>
        public ReturnValueSF GetLastGuaranteeChangeRecords(int iLastRecordNo,DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                #region modified by LinYong -- 20091106
                //DateTime dateTime_Bef, dateTime_Aft;    //need to delete when pubish
                //dateTime_Bef = DateTime.Now;            //need to delete when pubish
                

                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetLastGuaranteeChangeRecords(iLastRecordNo, dateTimeBM, stationBM, accountBM);
                    if (bytesToDecompress == null)
                        throw new Exception("�������ݱ� null��");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("���ݽ�ѹ����");
                }
                rvSF.Dt = dtDecompressedDatatable;

                //ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();  //ԭ�������Ч
                //rvSF.Dt = changeRecordDAF.GetLastGuaranteeChangeRecords(iLastRecordNo, dateTimeBM, stationBM, accountBM);   //ԭ�������Ч

                //dateTime_Aft = DateTime.Now;    //need to delete when pubish
                //TimeSpan timeSpan = new TimeSpan(dateTime_Aft.Ticks - dateTime_Bef.Ticks);  //need to delete when pubish
                //double dPeriod = timeSpan.TotalSeconds; //need to delete when pubish
                //dPeriod = dPeriod;  //need to delete when pubish
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

         /// <summary>
        /// ��ȡ��վ����100�������¼
        /// </summary>
        /// <param name="dateTimeBM">����ʱ�䷶Χʵ�����</param>
        /// <param name="stationBM">��վʵ�����</param>
        /// <returns></returns>
        public ReturnValueSF GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();

            try
            {
                rvSF.Dt = changeRecordDAF.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM);
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
        /// ����һ�����������¼
        /// </summary>
        /// <param name="changeRecordBM">���������¼ʵ�����</param>
        /// <returns>1���ɹ� 0��ʧ��</returns>
        public ReturnValueSF Insert(FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();

            try
            {
                rvSF.Result = changeRecordDAF.Insert(changeRecordBM);                
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// ���ݺ������ڡ�����źͱ�����Ͳ�ѯ�����¼
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strFlightNo"></param>
        /// <param name="strChangeReason"></param>
        /// <returns></returns>
        public ReturnValueSF GetChangeRecordsByFlightNo(DateTimeBM dateTimeBM, string strFlightNo, string strChangeReason)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();

            try
            {
                rvSF.Dt = changeRecordDAF.GetChangeRecordsByFlightNo(dateTimeBM, strFlightNo, strChangeReason);
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

        #region ��ȡ���һ��������� ��vw_FlightChangeRecord�� --added in 2009.10.28
        /// <summary>
        /// ��ȡ���һ��������� ��vw_FlightChangeRecord��
        /// </summary>
        /// <param name="dateTimeBM">ʱ��������� StartDateTime Ϊ��ȡ��ʱ���</param>
        /// <returns></returns>
        public ReturnValueSF GetLastvw_FlightChangeRecord(DateTimeBM dateTimeBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();

            try
            {
                rvSF.Dt = changeRecordDAF.GetLastvw_FlightChangeRecord(dateTimeBM);
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

        #region ��ȡ�������¼�� --added in 2009.12.23
        /// <summary>
        /// ��ȡ�������¼�� GetMaxRecordNo(AccountBM accountBM)
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetMaxRecordNo(AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                #region modified by LinYong -- 20091223
                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    rvSF.Result = objRemotingObject.GetMaxRecordNo(accountBM);
                }

                //ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                //rvSF.Result = Convert.ToInt32(changeRecordDAF.GetMaxRecordNo());
                #endregion
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion

        #region ��ȡ��վ����100�������¼
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLastRecordNo"></param>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                #region modified by LinYong -- 20091223
                //DateTime dateTime_Bef, dateTime_Aft;    //need to delete when pubish
                //dateTime_Bef = DateTime.Now;            //need to delete when pubish


                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM, accountBM);
                    if (bytesToDecompress == null)
                        throw new Exception("�������ݱ� null��");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("���ݽ�ѹ����");
                }
                rvSF.Dt = dtDecompressedDatatable;

                //ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();                //ԭ�������Ч
                //rvSF.Dt = changeRecordDAF.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM);   //ԭ�������Ч

                //dateTime_Aft = DateTime.Now;    //need to delete when pubish
                //TimeSpan timeSpan = new TimeSpan(dateTime_Aft.Ticks - dateTime_Bef.Ticks);  //need to delete when pubish
                //double dPeriod = timeSpan.TotalSeconds; //need to delete when pubish
                //dPeriod = dPeriod;  //need to delete when pubish
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

        #region ��ȡ���һ����¼��ָ���û��� -- added by LinYong in 20150420
        /// <summary>
        /// ��ȡ���һ����¼��ָ���û���
        /// </summary>
        /// <param name="UserID">�û��ʺ�</param>
        /// <returns></returns>
        public ReturnValueSF GetLastRecord(string UserID)
        {
            //�Զ��巵��ֵ
            ReturnValueSF rvSF = new ReturnValueSF();

            ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();

            try
            {
                rvSF.Dt = changeRecordDAF.GetLastRecord(UserID);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
        #endregion ��ȡ���һ����¼��ָ���û��� -- added by LinYong in 20150420

        #endregion
    }
}
