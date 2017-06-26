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
    /// ACARS航班动态外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSLegsBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ACARSLegsBF()
        {
        }

        /// <summary>
        /// 根据ACARS航班动态获取原航班动态
        /// </summary>
        /// <param name="acarsBM">ACARS实体对象</param>
        /// <returns></returns>
        public ReturnValueSF GetOriginalLegs(FlightMonitorBM.ACARSLegsBM acarsBM)
        {
            //自定义返回值
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
        /// 航班变更实体对象公共信息
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <param name="originalLegsBM">原航班实体对象</param>
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
        /// 从文本文件读取航班动态信息
        /// </summary>
        /// <param name="strFullPath">文件路径</param>
        /// <returns>ACARS航班动态文本信息</returns>
        public ReturnValueSF GetACARSLegsBMFromFile(string strFullPath)
        {
            //自定义返回值
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
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public ReturnValueSF UpdateACARSOnInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //变更实体对象列表
            IList ilChangeRecordBM = new ArrayList();

            //落地时间变更信息
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
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public ReturnValueSF UpdateACARSInInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //变更实体对象列表
            IList ilChangeRecordBM = new ArrayList();

           
            //挡抡挡时间变更信息
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
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public ReturnValueSF UpdateACARSOutInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //变更实体对象列表
            IList ilChangeRecordBM = new ArrayList();

            //推出时间变更信息
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
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public ReturnValueSF UpdateACARSOffInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            ACARSLegsDAF acarsLegsDAF = new ACARSLegsDAF();

            //变更实体对象列表
            IList ilChangeRecordBM = new ArrayList();
           

            //起飞时间时间变更信息
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
