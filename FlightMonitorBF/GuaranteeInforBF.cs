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
    /// 航班保障信息业务外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-31
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeInforBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GuaranteeInforBF()
        {
        }

        /// <summary>
        /// 获取某席位当天的所有航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="positionNameBM">席位名称实体对象</param>
        /// <returns>该席位的所有航班</returns>
        public ReturnValueSF GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
        {
            //自定义返回值
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
        /// 以主键为条件查询一条记录
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取某航站的进出港航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="stationBM">席位名称实体对象</param>
        /// <returns>该航站的所有航班</returns>
        public ReturnValueSF GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 更新某项保障内容
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateGuaranteeInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 同时更新多条数据
        /// </summary>
        /// <param name="ilMaintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateGuaranteeInforList(IList ilMaintenGuaranteeInforBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 更新某项航班动态内容
        /// </summary>
        /// <param name="maintenGuaranteeInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateLegsInfor(MaintenGuaranteeInforBM maintenGuaranteeInforBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取当天所有的航班动态
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetAllLegsByDay(DateTimeBM dateTimeBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取航班的值机信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetCheckInfor(ChangeLegsBM changeLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 更新旅客值机信息
        /// </summary>
        /// <param name="checkPaxBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateCheckPaxInfor(CheckPaxBM checkPaxBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取航班的值机信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetPaxNameList(ChangeLegsBM changeLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 更新旅客名单信息
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdatePaxNameList(PaxNameListBM paxNameListBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取中转连程旅客信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetTrasitPaxList(ChangeLegsBM changeLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 更新中转旅客信息
        /// </summary>
        /// <param name="paxNameListBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateTrasitPax(TrasitPaxBM trasitPaxBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 航后机位安排动态
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetEndFlight(DateTimeBM endDateTimeBM, DateTimeBM startDateTime, AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取某驾飞机当天所飞的所有航段
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="strLONG_REG"></param>
        /// <returns></returns>
        public ReturnValueSF GetAircraftFlights(DateTimeBM dateTimeBM, string strLONG_REG)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取航站进出港航班
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetStationFlight(DateTimeBM dateTimeBM, StationBM stationBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 根据航班号和起飞目的机场查询航班计划
        /// </summary>
        /// <param name="dateTimeBM">事件范围</param>
        /// <param name="strDEPSTN">起飞机场三字码</param>
        /// <param name="strARRSTN">目的机场三字码</param>
        /// <param name="strFlightNo">航班号</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightsByFlightNo(DateTimeBM dateTimeBM, string strDEPSTN, string strARRSTN, string strFlightNo)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 获取航班连飞信息
        /// </summary>
        /// <param name="dateTimeBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetJoinFlightNo(DateTimeBM dateTimeBM, AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 按机型统计出港旅客人数
        /// </summary>
        /// <param name="dateTimeBM">时间范围</param>
        /// <param name="strDEPSTN">始发站三字码</param>
        /// <returns></returns>
        public ReturnValueSF GetStatisticPax(DateTimeBM dateTimeBM, string strDEPSTN)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
        /// 根据当地时间获取当天的航班动态
        /// </summary>
        /// <param name="strFlightDate">航班日期</param>
        /// <returns></returns>
        public ReturnValueSF GetLegsByFlightDate(string strStartFlightDate, string strEndFlightDate)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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

        #region 获取某个时间段内所有的航班（vw_Legs） --added in 2009.10.27
        /// <summary>
        /// 获取某个时间段内所有的航班（vw_Legs）
        /// </summary>
        /// <param name="DateTimeBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetAllvw_LegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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

        #region 获取某航站的进出港航班
        /// <summary>
        /// 获取某航站的进出港航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="stationBM">席位名称实体对象</param>
        /// <returns>该航站的所有航班</returns>
        public ReturnValueSF GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                #region modified by LinYong -- 20091106
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetFlightsByStation(dateTimeBM, stationBM, accountBM);
                    if (bytesToDecompress == null)
                        throw new Exception("返回数据表 null！");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("数据解压错误！");
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


        #region 以主键为条件查询一条记录，使用 ReMoting 对象  --added in 2015.02.06
        /// <summary>
        /// 以主键为条件查询一条记录，使用 ReMoting 对象
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <param name="accountBM">登陆帐号实体</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetFlightByKey(changeLegsBM, accountBM);
                    if (bytesToDecompress == null)
                        throw new Exception("返回数据表 null！");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("数据解压错误！");
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

        #endregion 以主键为条件查询一条记录，使用 ReMoting 对象  --added in 2015.02.06

        #region 以主键为条件查询一条记录，使用 ReMoting 对象，返回的数据不进行压缩  --added in 2015.02.10
        /// <summary>
        /// 以主键为条件查询一条记录，使用 ReMoting 对象，返回的数据不进行压缩  --added in 2015.02.10
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <param name="accountBM">登陆帐号实体</param>
        /// <returns></returns>
        public ReturnValueSF GetFlightByKey_NotCompress(FlightMonitorBM.ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {

                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.GetFlightByKey_NotCompress(changeLegsBM, accountBM);
                    if (dtDatatable == null)
                        throw new Exception("返回数据表 null！");
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

        #endregion 以主键为条件查询一条记录，使用 ReMoting 对象，返回的数据不进行压缩  --added in 2015.02.10

        #region 根据报文解析的信息确定航班  --added in 2015.04.15
        /// <summary>
        /// 根据报文解析的信息确定航班  --added in 2015.04.15
        /// </summary>
        /// <param name="FlightNo">航班号</param>
        /// <param name="ST">计划起飞时间（IO是OUT）；计划到达时间（IO是IN）</param>
        /// <param name="STN">起飞机场（IO是OUT）；降落机场（IO是IN）</param>
        /// <param name="IO">出港航班（IO是OUT）；进港航班（IO是IN）</param>
        /// <returns>确定的航班</returns>
        public ReturnValueSF GetFlightsByMessage(string FlightNo, string ST, string STN, string IO)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {

                DataTable dtDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    dtDatatable = objRemotingObject.GetFlightsByMessage(FlightNo, ST, STN, IO);
                    if (dtDatatable == null)
                        throw new Exception("返回数据表 null！");
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

        #endregion 根据报文解析的信息确定航班  --added in 2015.04.15

        #endregion

    }
}
