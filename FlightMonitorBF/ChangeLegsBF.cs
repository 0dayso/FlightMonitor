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
    /// 航班变更动态外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-04
    /// 修 改 人：张黎
    /// 修改日期：2008-06-17
    /// 版    本：
    public class ChangeLegsBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChangeLegsBF()
        {
        }

        #region 获取原航班动态
        /// <summary>
        /// 获取原航班动态
        /// </summary>
        /// <param name="changeLegsBM">航班动态变更实体</param>
        /// <returns>自定义返回值</returns>
        public ReturnValueSF GetOriginalLegs(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();            
            try
            {
                //首先以主键为条件获取原航班
                #region modified by LinYong -- 20091105
                //DataTable dtFlight = null;

                //AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                //if (objRemotingObject != null)
                //{
                //    byte[] bFlight = objRemotingObject.GetFlightByKey(changeLegsBM);
                //    if (bFlight == null)
                //        throw new Exception("返回数据表 null！");
                //    CompressionHelper compressionHelper = new CompressionHelper();
                //    dtFlight = compressionHelper.DecompressToDataTable(bFlight);
                //    if (dtFlight == null)
                //        throw new Exception("数据解压错误！");
                //}

                DataTable dtFlight = changeLegsDAF.GetFlightByKey(changeLegsBM);  //原码此行有效 -- 恢复 in 20160714
                #endregion

                rvSF.Result = 1;

                //以组合主键为条件获取原航班
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

        #region 航班变更实体对象公共信息
        /// <summary>
        /// 航班变更实体对象公共信息
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <param name="originalLegsBM">原航班实体对象</param>
        /// <returns></returns>
        private ChangeRecordBM GetGeneralChangeRecordInformation(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            ChangeRecordBM changeRecordBM = new ChangeRecordBM();
            changeRecordBM.UserID = "FOC";                                      //变更用户
            changeRecordBM.OldFLTID = originalLegsBM.FLTID;             //原航班的航班号
            changeRecordBM.OldDATOP = originalLegsBM.DATOP;        //原航班的航班日期
            changeRecordBM.OldLegNo = originalLegsBM.LEGNO;        //原航班的飞机长号
            changeRecordBM.OldAC = originalLegsBM.AC;                    //原航班的飞机短号
            changeRecordBM.NewFLTID = changeLegsBM.FLTID;            //新航班的航班号
            changeRecordBM.NewDATOP = changeLegsBM.DATOP;       //新航班的航班日期
            changeRecordBM.NewLegNo = changeLegsBM.LEGNO;       //新航班的飞机长号
            changeRecordBM.NewAC = changeLegsBM.AC;                   //新航班的飞机短号
            changeRecordBM.OldDepSTN = originalLegsBM.DEPSTN;    //原航班的起飞机场
            changeRecordBM.NewDepSTN = changeLegsBM.DEPSTN;    //新航班的起飞机场
            changeRecordBM.OldArrSTN = originalLegsBM.ARRSTN;      //原航班的目的机场
            changeRecordBM.NewArrSTN = changeLegsBM.ARRSTN;     //新航班的目的机场
            changeRecordBM.STD = changeLegsBM.STD;                      //STD
            changeRecordBM.ETD = changeLegsBM.ETD;                      //ETD
            changeRecordBM.STA = changeLegsBM.STA;                      //STA
            changeRecordBM.ETA = changeLegsBM.ETA;                      //ETA

            //FOC系统的变更时间
            changeRecordBM.FOCOperatingTime = changeLegsBM.TSTAMP;
            //航站系统的处理时间
            changeRecordBM.LocalOperatingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //变更标记
            changeRecordBM.ActionTag = changeLegsBM.DELACTION;

            return changeRecordBM;
        }
        #endregion

        #region 新旧航班动态对比，获取航班变更操作实体
        /// <summary>
        /// 新旧航班动态对比，获取航班变更操作实体
        /// </summary>
        /// <param name="changeLegsBM">变更后航班动态</param>
        /// <param name="originalLegsBM">变更前航班动态</param>
        /// <returns>航班操作实体列表</returns>
        public IList GetChangeRecordBM(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM)
        {
            //变更列表
            IList ilChangeRecordBM = new ArrayList();

            //比较航段信息
            if (changeLegsBM.LEGNO != originalLegsBM.LEGNO)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);                
                changeRecordBM.ChangeReasonCode = "cniLEGNO";                
                changeRecordBM.ChangeOldContent = originalLegsBM.LEGNO.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.LEGNO.ToString();
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }
        
            //比较飞机短号
            if (changeLegsBM.AC != originalLegsBM.AC)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcAC";                
                changeRecordBM.ChangeOldContent = originalLegsBM.AC;
                changeRecordBM.ChangeNewContent = changeLegsBM.AC;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //比较飞机长号
            if (changeLegsBM.LONG_REG != originalLegsBM.LONG_REG)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);                
                changeRecordBM.ChangeReasonCode = "cnvcLONG_REG";
                changeRecordBM.ChangeOldContent = originalLegsBM.LONG_REG;
                changeRecordBM.ChangeNewContent = changeLegsBM.LONG_REG;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }          

            //比较起飞机场
            if (changeLegsBM.DEPSTN != originalLegsBM.DEPSTN)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncDEPSTN";
                changeRecordBM.ChangeOldContent = originalLegsBM.DEPSTN;
                changeRecordBM.ChangeNewContent = changeLegsBM.DEPSTN;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //比较目的机场
            if (changeLegsBM.ARRSTN != originalLegsBM.ARRSTN)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncARRSTN";
                changeRecordBM.ChangeOldContent = originalLegsBM.ARRSTN;
                changeRecordBM.ChangeNewContent = changeLegsBM.ARRSTN;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //航班状态
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
            
            //航班号
            if (changeLegsBM.TRI_FLTID != originalLegsBM.TRI_FLTID)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcTRI_FLTID";
                changeRecordBM.ChangeOldContent = originalLegsBM.TRI_FLTID;
                changeRecordBM.ChangeNewContent = changeLegsBM.TRI_FLTID;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //备降返航延误原因代码
            if (changeLegsBM.DIV_RCODE != originalLegsBM.DIV_RCODE)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDIV_RCODE";
                changeRecordBM.ChangeOldContent = originalLegsBM.DIV_RCODE;
                changeRecordBM.ChangeNewContent = changeLegsBM.DIV_RCODE;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //备降原因代码
            if (changeLegsBM.DIV_FLAG != originalLegsBM.DIV_FLAG)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDIV_FLAG";
                changeRecordBM.ChangeOldContent = originalLegsBM.DIV_FLAG;
                changeRecordBM.ChangeNewContent = changeLegsBM.DIV_FLAG;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //旅客
            if (changeLegsBM.PAX != originalLegsBM.PAX)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcPAX";
                changeRecordBM.ChangeOldContent = originalLegsBM.PAX;
                changeRecordBM.ChangeNewContent = changeLegsBM.PAX;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }           

            //订座人数
            if (changeLegsBM.BOOK != originalLegsBM.BOOK)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcBOOK";
                changeRecordBM.ChangeOldContent = originalLegsBM.BOOK;
                changeRecordBM.ChangeNewContent = changeLegsBM.BOOK;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第一延误代码
            if (changeLegsBM.DELAY1 != originalLegsBM.DELAY1)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY1";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY1;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY1;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第一延误时间
            if (changeLegsBM.DUR1 != originalLegsBM.DUR1)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR1";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR1.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR1.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第二延误代码
            if (changeLegsBM.DELAY2 != originalLegsBM.DELAY2)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY2";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY2;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY2;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第二延误时间
            if (changeLegsBM.DUR2 != originalLegsBM.DUR2)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR2";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR2.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR2.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第三延误代码
            if (changeLegsBM.DELAY3 != originalLegsBM.DELAY3)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY3";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY3;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY3;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第三延误时间
            if (changeLegsBM.DUR3 != originalLegsBM.DUR3)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR3";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR3.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR3.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第四延误代码
            if (changeLegsBM.DELAY4 != originalLegsBM.DELAY4)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcDELAY4";
                changeRecordBM.ChangeOldContent = originalLegsBM.DELAY4;
                changeRecordBM.ChangeNewContent = changeLegsBM.DELAY4;
                changeRecordBM.Refresh = 1;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //第四延误时间
            if (changeLegsBM.DUR4 != originalLegsBM.DUR4)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cniDUR4";
                changeRecordBM.ChangeOldContent = originalLegsBM.DUR4.ToString();
                changeRecordBM.ChangeNewContent = changeLegsBM.DUR4.ToString();
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }           

            //登机门
            if (changeLegsBM.GATE != originalLegsBM.GATE)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cnvcGATE";
                changeRecordBM.ChangeOldContent = originalLegsBM.GATE;
                changeRecordBM.ChangeNewContent = changeLegsBM.GATE;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //航班性质代码
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

            //原始机型
            if (changeLegsBM.ORIG_ACTYP != originalLegsBM.ORIG_ACTYP)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncORIG_ACTYP";
                changeRecordBM.ChangeOldContent = originalLegsBM.ORIG_ACTYP;
                changeRecordBM.ChangeNewContent = changeLegsBM.ORIG_ACTYP;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //机型
            if (changeLegsBM.ACTYP != originalLegsBM.ACTYP)
            {
                ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, originalLegsBM);
                changeRecordBM.ChangeReasonCode = "cncACTYP";
                changeRecordBM.ChangeOldContent = originalLegsBM.ACTYP;
                changeRecordBM.ChangeNewContent = changeLegsBM.ACTYP;
                changeRecordBM.Refresh = 0;
                ilChangeRecordBM.Add(changeRecordBM);
            }

            //飞机所属公司
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

        #region 插入一条航班动态
        /// <summary>
        /// 插入一条航班动态
        /// </summary>
        /// <param name="changeLegsBM">变更后航班动态</param>
        /// <returns></returns>
        public ReturnValueSF Insert(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //获取变更信息
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, changeLegsBM);
            changeRecordBM.ChangeReasonCode = "cnvcFlightNo";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = "";
            changeRecordBM.Refresh = 1;
            changeRecordBM.ActionTag = "I";
            
            try
            {                
                //调用数据访问外观层方法
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

        #region 更新航班动态
        /// <summary>
        /// 更新航班动态，如果没有相应原记录，要插入一条航班动态
        /// </summary>
        /// <param name="changeLegsBM">变更后航班动态</param>
        /// <returns></returns>
        public ReturnValueSF Update(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            try
            {
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                rvSF = GetOriginalLegs(changeLegsBM);

                //有原记录
                if (rvSF.Result > 0)
                {
                    #region 修正导致同时出现多条重复航班（一般航班号相同，飞机号不同，有些航班为调整飞机前的航班，已不执飞）的问题 -- modified by LinYong in 20160707
                    //ChangeLegsBM originalLegsBM = new ChangeLegsBM(rvSF.Dt.Rows[0]); //原码此行有效
                    ChangeLegsBM originalLegsBM = new ChangeLegsBM(rvSF.Dt.Rows[0]);
                    DataRow[] arrayDataRowrvSFDt = rvSF.Dt.Select("cniDeleteTag = 0");
                    if (arrayDataRowrvSFDt.Length > 0)
                    {
                        originalLegsBM = new ChangeLegsBM(arrayDataRowrvSFDt[0]);
                    }
                    #endregion 修正导致同时出现多条重复航班（一般航班号相同，飞机号不同，有些航班为调整飞机前的航班，已不执飞）的问题 -- modified by LinYong in 20160707
                    IList ilChangeRecordBM = GetChangeRecordBM(changeLegsBM, originalLegsBM);

                    if (changeLegsBM.DELACTION == "I")
                    {
                        //主键未发生变化，则以主键为条件更新信息
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
                            //主键未发生变化，则以主键为条件更新信息
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
                    //插入航班动态
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

        #region 逻辑删除一条航班
        /// <summary>
        /// 逻辑删除一条航班
        /// </summary>
        /// <param name="changeLegsBM">变更后航班动态</param>
        /// <returns></returns>
        public ReturnValueSF LogicDelete(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            //获取变更信息
            ChangeRecordBM changeRecordBM = GetGeneralChangeRecordInformation(changeLegsBM, changeLegsBM);
            changeRecordBM.ChangeReasonCode = "cnvcFlightNo";
            changeRecordBM.ChangeOldContent = "";
            changeRecordBM.ChangeNewContent = "";
            changeRecordBM.Refresh = 1;
            changeRecordBM.ActionTag = "D";

            try
            {
                ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();

                //首先以主键为条件获取原航班
                #region modified by LinYong -- 20091105
                //DataTable dtFlight = null;

                //AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                //if (objRemotingObject != null)
                //{
                //    byte[] bFlight = objRemotingObject.GetFlightByKey(changeLegsBM);
                //    if (bFlight == null)
                //        throw new Exception("返回数据表 null！");
                //    CompressionHelper compressionHelper = new CompressionHelper();
                //    dtFlight = compressionHelper.DecompressToDataTable(bFlight);
                //    if (dtFlight == null)
                //        throw new Exception("数据解压错误！");
                //}

                DataTable dtFlight = changeLegsDAF.GetFlightByKey(changeLegsBM);  //原码此行有效 -- 恢复 in 20160714
                #endregion

                //有原记录
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

        #region 读取航班变更文件
        /// <summary>
        /// 读取航班变更文件
        /// </summary>
        /// <param name="strFullPath">变更文件完整路径</param>
        /// <returns></returns>
        public ReturnValueSF GetChangeLegsFromFile(string strFullPath)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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

        #region 以主键为条件查询一条记录
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
                #region modified by LinYong -- 20091105
                DataTable dtDecompressedDatatable = null;

                AgentServiceDAF objRemotingObject = AgentServiceDAF.objRemotingObject;
                if (objRemotingObject != null)
                {
                    byte[] bytesToDecompress = objRemotingObject.GetFlightByKey(changeLegsBM);
                    if (bytesToDecompress == null)
                        throw new Exception("返回数据表 null！");
                    CompressionHelper compressionHelper = new CompressionHelper();
                    dtDecompressedDatatable = compressionHelper.DecompressToDataTable(bytesToDecompress);
                    if (dtDecompressedDatatable == null)
                        throw new Exception("数据解压错误！");
                }
                rvSF.Dt = dtDecompressedDatatable;

                //ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();//原码此行有效
                //rvSF.Dt = changeLegsDAF.GetFlightByKey(changeLegsBM);//原码此行有效
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

        #region 获取当天所有的航班动态 --added in 2009.10.26 ,获取 tbLegs 所有字段
        /// <summary>
        /// 获取当天所有的航班动态
        /// </summary>
        /// <param name="DateTimeBM">航班变更动态实体</param>
        /// <returns></returns>
        public ReturnValueSF GetAllLegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
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
