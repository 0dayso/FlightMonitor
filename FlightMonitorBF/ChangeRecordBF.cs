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
    /// 航班操作记录外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-04
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ChangeRecordBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChangeRecordBF()
        {
        }

         /// <summary>
        /// 获取最后一批变更数据
        /// </summary>
        /// <param name="iLastRecordNo">系统已经处理的最大的变更序号</param>
        /// <returns></returns>
        public ReturnValueSF GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //自定义返回值
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
        /// 获取最大变更记录号
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetMaxRecordNo()
        {
            //自定义返回值
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
        /// 航站获取最后一批变更数据
        /// </summary>
        /// <param name="iLastRecordNo">系统已经处理的最大的变更序号</param>
        /// <returns></returns>
        public ReturnValueSF GetLastGuaranteeChangeRecords(int iLastRecordNo,DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            //自定义返回值
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
                        throw new Exception("返回数据表 null！");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("数据解压错误！");
                }
                rvSF.Dt = dtDecompressedDatatable;

                //ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();  //原码此行有效
                //rvSF.Dt = changeRecordDAF.GetLastGuaranteeChangeRecords(iLastRecordNo, dateTimeBM, stationBM, accountBM);   //原码此行有效

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
        /// 获取航站最新100条变更记录
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="stationBM">航站实体对象</param>
        /// <returns></returns>
        public ReturnValueSF GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //自定义返回值
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
        /// 插入一条航班操作记录
        /// </summary>
        /// <param name="changeRecordBM">航班操作记录实体对象</param>
        /// <returns>1：成功 0：失败</returns>
        public ReturnValueSF Insert(FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            //自定义返回值
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
        /// 根据航班日期、航班号和变更类型查询变更记录
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strFlightNo"></param>
        /// <param name="strChangeReason"></param>
        /// <returns></returns>
        public ReturnValueSF GetChangeRecordsByFlightNo(DateTimeBM dateTimeBM, string strFlightNo, string strChangeReason)
        {
            //自定义返回值
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

        #region 获取最后一批变更数据 （vw_FlightChangeRecord） --added in 2009.10.28
        /// <summary>
        /// 获取最后一批变更数据 （vw_FlightChangeRecord）
        /// </summary>
        /// <param name="dateTimeBM">时间对象，属性 StartDateTime 为提取的时间点</param>
        /// <returns></returns>
        public ReturnValueSF GetLastvw_FlightChangeRecord(DateTimeBM dateTimeBM)
        {
            //自定义返回值
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

        #region 获取最大变更记录号 --added in 2009.12.23
        /// <summary>
        /// 获取最大变更记录号 GetMaxRecordNo(AccountBM accountBM)
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetMaxRecordNo(AccountBM accountBM)
        {
            //自定义返回值
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

        #region 获取航站最新100条变更记录
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
            //自定义返回值
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
                        throw new Exception("返回数据表 null！");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("数据解压错误！");
                }
                rvSF.Dt = dtDecompressedDatatable;

                //ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();                //原码此行有效
                //rvSF.Dt = changeRecordDAF.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM);   //原码此行有效

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

        #region 获取最后一条记录（指定用户） -- added by LinYong in 20150420
        /// <summary>
        /// 获取最后一条记录（指定用户）
        /// </summary>
        /// <param name="UserID">用户帐号</param>
        /// <returns></returns>
        public ReturnValueSF GetLastRecord(string UserID)
        {
            //自定义返回值
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
        #endregion 获取最后一条记录（指定用户） -- added by LinYong in 20150420

        #endregion
    }
}
