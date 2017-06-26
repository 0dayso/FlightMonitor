using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航班保障信息类实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-02
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class GuaranteeInforBM:IComparable
    {
        #region 对象内部变量
        #region 以往
        private string m_strInDATOP;
        private string m_strInFLTID;
        private string m_strInLEGNO;
        private string m_strInAC;
        private string m_strInAllSTA;
        private string m_strInAllETA;
        private string m_strInAllTDWN;
        private string m_strInAllATA;
        private string m_strInAllStatus;
        private string m_strInAllViewIndex;
        private string m_strInLONG_REG;
        private string m_strInFlightDate;
        private string m_strInFlightNo;
        private string m_strInACTYP;
        private string m_strInFlightCharacterAbbreviate;
        private string m_strInDEPAirportCNAME;
        private string m_strInARRAirportCNAME;
        private string m_strInSTA;
        private string m_strInETA;
        private string m_strInTDWN;   //落地时间
        private string m_strInATA;       //到位时间
        private string m_strInACARSTOFF;
        private string m_strInACARSTDWN;
        private string m_strInACARSATA;
        private string m_strInInDelayName;
        private string m_strInFlightDelayName;
        private string m_strInDiversionDelayName;
        private string m_strInInCarInfor;
        private string m_strInInCarDepTime;
        private string m_strInInCarArrTime;
        private string m_strInVIPTag;
        private string m_strInCheckNum;
        private string m_strInTransitPaxTag;
        private string m_strInBaggageWeight;
        private string m_strInCargoWeight;
        private string m_strInStatusName;
        private string m_strInInRemark;
        private string m_strInInAircraftStatus;
        private string m_strInInGATE;
        private string m_strOutDATOP;
        private string m_strOutFLTID;
        private string m_strOutLEGNO;
        private string m_strOutAC;
        private string m_strOutAllSTD;
        private string m_strOutAllETD;
        private string m_strOutAllATD;
        private string m_strOutAllTOFF;
        private string m_strOutAllStatus;
        private string m_strOutAllViewIndex;
        private string m_strOutOutGate;
        private string m_strOutLONG_REG;
        private string m_strOutFlightDate;
        private string m_strOutFlightNo;
        private string m_strOutFlightCharacterAbbreviate;
        private string m_strOutDEPAirportCNAME;
        private string m_strOutARRAirportCNAME;
        private string m_strOutSTD;
        private string m_strOutETD;
        private string m_strOutStartGuaranteeTime;
        private string m_strOutIntermissionTime;
        private string m_strOutJoinFlight;
        private string m_strOutCheckCounter;
        private string m_strOutWaitHall;
        private string m_strOutBoradingGate;
        private string m_strOutOpenCabinTime;
        private string m_strOutOutCarInfor;
        private string m_strOutOutCarDepTime;
        private string m_strOutOutCarArrTime;
        private string m_strOutPilotArrTime;
        private string m_strOutStewardArrTime;
        private string m_strOutDeiceStartTime;
        private string m_strOutDeiceEndTime;
        private string m_strOutTaskSheetChangeTime;
        private string m_strOutDispatchTime;
        private string m_strOutDispatchPrintTime;
        private string m_strOutOutMCCTechEngr;                  //机务|技术员
        private string m_strOutOutMCCDispEngr;                  //机务|放行人员
        private string m_strOutOutMCCReadyTime;
        private string m_strInInMCCReadyTime;
        private string m_strInInTime;
        private string m_strOutMCCReleaseTime;
        private string m_strOutOutTime;
        private string m_strOutAircraftStatus;
        private string m_strOutDeplaneTime;
        private string m_strOutCleanStartTime;
        private string m_strOutCleanEndTime;
        private string m_strOutSewageStartTime;
        private string m_strOutSewageEndTime;
        private string m_strOutCabinSupplyStartTime;
        private string m_strOutCabinSupplyEndTime;
        private string m_strOutOilStartTime;
        private string m_strOutOilEndTime;
        private string m_strOutOpenCargoCabinTime;
        private string m_strOutCargoStartTime;
        private string m_strOutBaggageTime;
        private string m_strOutCloseCargoCabinTime;
        private string m_strOutTrailerArrTime;
        private string m_strOutDirtyWaterCarArrTime;
        private string m_strOutFerryDepTime;
        private string m_strOutLastFerryArrTime;
        private string m_strOutDragPoleArrTime;
        private string m_strOutLadderCarArrTime;
        private string m_strOutInformBoardTime;
        private string m_strOutBoardTime;
        private string m_strOutBookNum;
        private string m_strOutCheckNum;
        private string m_strOutTransitPaxTag;
        private string m_strOutXCRNum;
        private string m_strOutAdultNum;
        private string m_strOutChildNum;
        private string m_strOutInfantNum;
        private string m_strOutFirstClassNum;
        private string m_strOutOfficialClassNum;
        private string m_strOutTouristClassNum;
        private string m_strOutAscendingPaxNum;
        private string m_strOutPaxNameList;
        private string m_strOutBoardOverTime;
        private string m_strOutLoadSheetArrTime;
        private string m_strOutClosePaxCabinTime;
        private string m_strOutOpenTime;
        private string m_strOutInternalGuestTogether;
        private string m_strOutPushTime;
        private string m_strOutATD;            //推出时间
        private string m_strOutTOFF;           //起飞时间
        private string m_strOutACARSOUT;
        private string m_strOutACARSTOFF;
        private string m_strOutDisChargingDelName;
        private string m_strOutOutDelayName;
        private string m_strOutFlightDelayName;
        private string m_strOutDiversionDelayName;
        private string m_strOutCargoWeight;
        private string m_strOutMailWeight;
        private string m_strOutBaggageWeight;
        private string m_strOutAirMaterialWeight;
        private string m_strOutAirMaterialRemark;
        private string m_strOutBaggageNum;
        private string m_strOutVIPTag;
        private string m_strOutTotalFuelWeight;
        private string m_strOutTripFuelWeight;
        private string m_strOutTaxiFuelWeight;
        private string m_strOutUnloadCargoWeight;
        private string m_strOutCargoRemark;
        private string m_strOutStatusName;
        private string m_strOutOutRemark;
        private string m_strOutChiefController;
        private string m_strOutAssistantController;
        private string m_strOutOutFieldController;
        private string m_strOutBalanceController;
        private string m_strOutDischargingDelayTime;
        private string m_strOutcniDUR1;
        private string m_strOutFocusTag;
        private string m_strOutArrangedTask;

        private string m_strInInMCCTechEngr;                        //机务|航后技术员   -- added in 20101113
        private string m_strInInArrangedTask;                       //机务|航后工作     -- added in 20101113
        private string m_strInInSMCRemark;                          //航站进港备注      -- added in 20101113
        private string m_strOutOutSMCRemark;                        //航站出港备注      -- added in 20101113

        private string m_strInMCCReleaseTime;                           //机务|航后放行                 -- added in 20111031
        private string m_strOutWAOperationInfoYK;                       //西部航空航班运行信息|运控      -- added in 20111031
        private string m_strOutWAOperationInfoFX;                       //西部航空航班运行信息|飞行      -- added in 20111031
        private string m_strOutWAOperationInfoKF;                       //西部航空航班运行信息|客服      -- added in 20111031
        private string m_strOutWAOperationInfoGC;                       //西部航空航班运行信息|工程      -- added in 20111031
        private string m_strOutWAOperationInfoAJ;                       //西部航空航班运行信息|安监      -- added in 20111031
        private string m_strOutWAOperationInfoSC;                       //西部航空航班运行信息|市场      -- added in 20111031

        private string m_strOutcncCDMeOutTime;                          //CDM预计推出时间           -- added in 20121113
        private string m_strOutcncCDMeOffTime;                          //CDM预计起飞时间           -- added in 20121113
        private string m_strOutcncCDMvStartTime;                        //CDM排序机组车发车时间     -- added in 20121113

        #endregion 以往

        #region 三期 added in 20140514
        private string m_strincncinladdercarguaranteestarttime;
        private string m_strincncinladdercarguaranteeendtime;
        private string m_strincncinbridgeguaranteestarttime;
        private string m_strincncinbridgeguaranteeendtime;
        private string m_strincncinferryrguaranteestarttime;
        private string m_strincncinferryguaranteeendtime;
        private string m_strincncinferryguaranteefirstarriveplanetime;
        private string m_strincncinferryguaranteelastarriveplanetime;
        private string m_strincncinferryrguaranteearrivetime;
        private string m_strincncincrewcarrguaranteestarttime;
        private string m_strincncincrewcarguaranteeendtime;
        private string m_strincncinconveyerbeltvehicleguaranteestarttime;
        private string m_strincncinconveyerbeltvehicleguaranteeendtime;
        private string m_strincncinplatformcarguaranteestarttime;
        private string m_strincncinplatformcarguaranteeendtime;
        private string m_strincncintrailcarguaranteestarttime;
        private string m_strincncintrailcarguaranteeendtime;
        private string m_strincncintrailcarguaranteearrivetime;
        private string m_strincncintrailcarguaranteeholdthewheeltime;
        private string m_strincncinpowercarguaranteestarttime;
        private string m_strincncinpowercarguaranteeendtime;
        private string m_strincncinairsupplycarguaranteestarttime;
        private string m_strincncinairsupplycarguaranteeendtime;
        private string m_strincncinairconditionercarguaranteestarttime;
        private string m_strincncinairconditionercarguaranteeendtime;
        private string m_strincncincleanwatercarguaranteestarttime;
        private string m_strincncincleanwatercarguaranteeendtime;
        private string m_strincncinsewagecarguaranteestarttime;
        private string m_strincncinsewagecarguaranteeendtime;
        private string m_stroutcncoutladdercarrguaranteestarttime;
        private string m_stroutcncoutladdercarguaranteeendtime;
        private string m_stroutcncoutbridgerguaranteestarttime;
        private string m_stroutcncoutbridgeguaranteeendtime;
        private string m_stroutcncoutferryrguaranteestarttime;
        private string m_stroutcncoutferryguaranteeendtime;
        private string m_stroutcncoutferryguaranteefirstouttime;
        private string m_stroutcncoutferryguaranteefirstarrivegatetime;
        private string m_stroutcncoutferryguaranteefirstarriveplanetime;
        private string m_stroutcncoutferryguaranteelastouttime;
        private string m_stroutcncoutferryguaranteelastarrivegatetime;
        private string m_stroutcncoutferryguaranteelastarriveplanetime;
        private string m_stroutcncoutferryrguaranteearrivetime;
        private string m_stroutcncoutcrewcarrguaranteestarttime;
        private string m_stroutcncoutcrewcarguaranteeendtime;
        private string m_stroutcncoutconveyerbeltvehicleguaranteestarttime;
        private string m_stroutcncoutconveyerbeltvehicleguaranteeendtime;
        private string m_stroutcncoutplatformcarguaranteestarttime;
        private string m_stroutcncoutplatformcarguaranteeendtime;
        private string m_stroutcncouttrailcarguaranteestarttime;
        private string m_stroutcncouttrailcarguaranteeendtime;
        private string m_stroutcncouttrailcarguaranteearrivetime;
        private string m_stroutcncouttrailcarguaranteeholdthewheeltime;
        private string m_stroutcncoutpowercarguaranteestarttime;
        private string m_stroutcncoutpowercarguaranteeendtime;
        private string m_stroutcncoutairsupplycarguaranteestarttime;
        private string m_stroutcncoutairsupplycarguaranteeendtime;
        private string m_stroutcncoutairconditionercarguaranteestarttime;
        private string m_stroutcncoutairconditionercarguaranteeendtime;
        private string m_stroutcncoutcleanwatercarguaranteestarttime;
        private string m_stroutcncoutcleanwatercarguaranteeendtime;
        private string m_stroutcncoutsewagecarguaranteestarttime;
        private string m_stroutcncoutsewagecarguaranteeendtime;
        private string m_stroutcncoutfuelfillingstarttime;
        private string m_stroutcncoutfuelfillingendtime;
        private string m_stroutcncoutcabinsupplystarttime;
        private string m_stroutcncoutcabinsupplyendtime;
        private string m_strincncincargomailbaggageguaranteestarttime;
        private string m_strincncincargomailbaggageguaranteeendtime;
        private string m_strincncincargocabinopentime;
        private string m_strincncincargocabinclosetime;
        private string m_strincncinbaggageunloadstarttime;
        private string m_strincncinbaggageunloadendtime;
        private string m_strincncincargounloadstarttime;
        private string m_strincncincargounloadendtime;
        private string m_stroutcncoutcargomailbaggageguaranteestarttime;
        private string m_stroutcncoutcargomailbaggageguaranteeendtime;
        private string m_stroutcncoutcargomailbaggagereporttime;
        private string m_stroutcncoutinfopickupbaggagetime;
        private string m_stroutcncoutcargocabinopentime;
        private string m_stroutcncoutcargocabinclosetime;
        private string m_stroutcncoutbaggageloadstarttime;
        private string m_stroutcncoutbaggageloadendtime;
        private string m_stroutcncoutcargoloadstarttime;
        private string m_stroutcncoutcargoloadendtime;
        private string m_strincncinpilotbraketime;
        private string m_stroutcncoutpilotarrivetime;
        private string m_stroutcncoutpilotlooseskidstime;
        private string m_stroutcncoutpilotpushofftime;
        private string m_strincncincabinopentime;
        private string m_strincncincabinclosetime;
        private string m_stroutcncoutcabinopentime;
        private string m_stroutcncoutcabinclosetime;
        private string m_stroutcncoutstewardarrivetime;
        private string m_stroutcncoutcheckinguaranteestarttime;
        private string m_stroutcncoutcheckinguaranteeendtime;
        private string m_strincncincabincleanstarttime;
        private string m_strincncincabincleanendtime;
        private string m_stroutcncoutcabincleanstarttime;
        private string m_stroutcncoutcabincleanendtime;
        private string m_strincncinguestoutstarttime;
        private string m_strincncinguestoutendtime;
        private string m_strincncinguiderarrivetime;
        private string m_strincncinguiderguaranteestarttime;
        private string m_strincncinguiderguaranteeendtime;
        private string m_stroutcncoutguestinstarttime;
        private string m_stroutcncoutguestinendtime;
        private string m_stroutcncoutguiderarrivetime;
        private string m_stroutcncoutguiderguaranteestarttime;
        private string m_stroutcncoutguiderguaranteeendtime;
        private string m_stroutcncoutinformboardtime;
        private string m_stroutcncoutboardinggateclosetime;
        private string m_stroutcncoutloadsheetuploadfinishedtime;
        private string m_stroutcncoutcaptainconfirmtime;
        private string m_strincncinvippickupguaranteestarttime;
        private string m_strincncinvippickupguaranteeendtime;
        private string m_stroutcncoutvipseeoffguaranteestarttime;
        private string m_stroutcncoutvipseeoffguaranteeendtime;
        private string m_stroutcncoutcustomguaranteestarttime;
        private string m_stroutcncoutcustomguaranteeendtime;
        private string m_stroutcncoutfrontierinspectionguaranteestarttime;
        private string m_stroutcncoutfrontierinspectionguaranteeendtime;
        private string m_stroutcncoutquarantineguaranteestarttime;
        private string m_stroutcncoutquarantineguaranteeendtime;

        private string m_strincnvcinguaranteerecord;        //进港 保障信息      -- added in 20140709
        private string m_stroutcnvcoutguaranteerecord;      //出港 保障信息      -- added in 20140709

        #endregion 三期 added in 20140514

        #region 三期 added in 201504
        private string _OutcnvcOutRunway;	//出港跑道号         
        private string _IncnvcInRunway;	//进港跑道号         
        private string _IncncInEXIT;	//EXIT               
        private string _OutcncOutTOBT;	//TOBT               
        private string _OutcncOutBoardStartTime;	//登机|开始          
        private string _OutcncOutBoardEndTime;	//登机|结束          
        private string _OutcncOutCheckCounterEndTime;	//柜台结束时间       
        private string _OutcnvcOutDeicePing;	//除冰|除冰坪        
        private string _OutcnvcOutDeiceWei;	//除冰|除冰位        
        private string _OutcncOutArriveDeicePing;	//除冰|到达除冰等待点
        private string _OutcncOutDeiceStartTime;	//除冰|除冰开始      
        private string _OutcncOutDeiceEndTime;	//除冰|除冰结束      
        private string _OutcncOutLeaveDeicePing;	//除冰|离开除冰位    
        private string _OutcnvcOutSlowDeiceFlag;	//除冰|慢车除冰标识  
        private string _OutcncOutTSAT;	//TSAT               
        private string _OutcncOutCTOT;	//CTOT
        #endregion 三期 added in 201504
        
        #region 三期 added in 20150428
        private string _OutcncOutFirstPassengerComeInCabinTime; //第一位旅客|进入客舱
        private string _OutcncOutPlaneReadyEndTime; //飞机准备|完毕
        #endregion 三期 added in 20150428

        #region 三期 added in 20150624
        private string _IncnvcInGATE_Test; //测试|进港机位
        private string _OutcnvcOutGate_Test; //测试|出港机位
        private string _OutcncOutFlightInterceptTime; //出港截载|时间
        private string _IncnvcInTurnTableNO; //进港转盘|号码
        private string _OutcnvcOutTurnTableNO; //出港转盘|号码
        #endregion 三期 added in 20150624

        #region 三期 added in 20150730
        private string _OutcncOutCrewStartTime; //出港空勤组|出发
        private string _OutcncOutCrewArriveTime; //出港空勤组|到位
        private string _OutcnvcOutOverStationType; //出港航班|过站类型
        #endregion 三期 added in 20150730

        #region 三期 added in 20160323
        private string _OutcnvcOutSpotUsers;  //出港关注人员|现场         
        private string _OutcnvcOutMaintenanceUsers;  //出港关注人员|机务         
        private string _OutcnvcOutFleetUsers;  //出港关注人员|车队         
        private string _OutcnvcOutVIPRoomUsers;  //出港关注人员|贵宾室       
        private string _OutcnvcOutAviationDietUsers;  //出港关注人员|航食         
        private string _OutcnvcOutCheckInUsers;  //出港关注人员|值机         
        private string _OutcnvcOutCleaningTeamUsers;  //出港关注人员|清洁队       
        private string _IncnvcInSpotUsers;  //进港关注人员|现场         
        private string _IncnvcInMaintenanceUsers;  //进港关注人员|机务         
        private string _IncnvcInFleetUsers;  //进港关注人员|车队         
        private string _IncnvcInVIPRoomUsers;  //进港关注人员|贵宾室       
        private string _IncnvcInAviationDietUsers;  //进港关注人员|航食         
        private string _IncnvcInCheckInUsers;  //进港关注人员|值机         
        private string _IncnvcInCleaningTeamUsers;  //进港关注人员|清洁队  
        #endregion 三期 added in 20160323

        #region 三期 added in 20160503
        private string _OutcncSTA;  //出发到达|计划  
        private string _OutcncETA;  //出发到达|预达  
        private string _OutcncTDWN; //出发到达|落地 
        private string _OutcncATA;  //出发到达|到位  
        #endregion 三期 added in 20160503

        #region 三期 added in 20160527
        private string _IncniInConveyerBeltVehicleGuaranteeCount; //                    进港传送带车|用车次数
        private string _OutcniOutConveyerBeltVehicleGuaranteeCount; //                      出港传送带车|用车次数
        private string _IncniInBaggageCarGuaranteeCount; //           进港行李牵引车|用车次数
        private string _OutcniOutBaggageCarGuaranteeCount; //             出港行李牵引车|用车次数
        private string _IncniInCrewCarGuaranteeCount; //        进港机组车|用车次数
        private string _OutcniOutCrewCarGuaranteeCount; //          出港机组车|用车次数
        private string _IncniInPlatformCarGuaranteeCount; //            进港平台车|用车次数
        private string _OutcniOutPlatformCarGuaranteeCount; //              出港平台车|用车次数
        private string _IncniInLadderCarGuaranteeCount; //          进港客梯车|用车次数
        private string _OutcniOutLadderCarGuaranteeCount; //            出港客梯车|用车次数
        private string _IncniInFerryGuaranteeCount; //      进港摆渡车|用车次数
        private string _OutcniOutFerryGuaranteeCount; //        出港摆渡车|用车次数
        private string _OutcniOutDeicingVehicleGuaranteeCount; //                 出港除冰车|用车次数
        private string _OutcniOutTrailCarGuaranteeCount; //           出港拖车|用车次数
        private string _OutcnvcOutRemark_Fleet; //  出港备注|车队
        private string _IncnvcInRemark_Fleet; //进港备注|车队
        #endregion 三期 added in 20160527

        #region 三期 added in 20160616
        private string _OutcnvcOutArrangeCargoOperator;   //      配货|操作人
        private string _OutcncOutArrangeCargoOperateTime;   //        配货|时间
        private string _OutcnvcOutRecheckLoadSheetOperator;   //          装机单复核|操作人
        private string _OutcncOutRecheckLoadSheetOperateTime;   //            装机单复核|时间
        private string _OutcnvcOutConfirmLoadSheetOperator;   //          装机单确认|操作人
        private string _OutcncOutConfirmLoadSheetOperateTime;   //            装机单确认|时间
        private string _OutcnvcOutConfirmLoadSheetOperateVerNumber;   //                  装机单确认|版本号
        private string _OutcnvcOutCheckManifestOperator;   //       舱单核对|操作人
        private string _OutcncOutCheckManifestOperateTime;   //         舱单核对|时间
        private string _OutcnvcOutRecheckManifestOperator;   //         舱单复核|操作人
        private string _OutcncOutRecheckManifestOperateTime;   //           舱单复核|时间
        private string _OutcnvcOutUploadManifestOperator;   //        舱单上传|操作人
        private string _OutcncOutUploadManifestOperateTime;   //          舱单上传|时间
        private string _OutcnvcOutUploadManifestOperateResult;   //             舱单上传|上传结果
        private string _OutcnvcOutCrewConfirmManifestOperateResult;   //                  舱单确认情况|机组确认结果
        private string _OutcncOutCrewConfirmManifestOperateTime;   //               舱单确认情况|时间
        private string _OutcnvcOutTransportManifestOperator;   //           送舱单或平衡图|操作人
        private string _OutcncOutTransportManifestOperateTime;   //             送舱单或平衡图|时间
        private string _OutcnvcOutRemark_Balance;   //出港备注|配载
        #endregion 三期 added in 20160616

        #region 三期 added in 20160704
        private string _IncnvcInLeaders;        //进港关注人员|领导
        private string _OutcnvcOutLeaders;      //出港关注人员|领导
        #endregion 三期 added in 20160704

        #endregion 对象内部变量

        /// <summary>
        /// 构造函数
        /// </summary>
        public GuaranteeInforBM()
        {
        }

        /// <summary>
        ///  构造函数:把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow"></param>
        public GuaranteeInforBM(DataRow dataRow)
        {
            #region 以往
            if (dataRow.Table.Columns.Contains("IncncDATOP"))
            {
                m_strInDATOP = dataRow["IncncDATOP"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcFLTID"))
            {
                m_strInFLTID = dataRow["IncnvcFLTID"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncniLEGNO"))
            {
                m_strInLEGNO = dataRow["IncniLEGNO"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcAC"))
            {
                m_strInAC = dataRow["IncnvcAC"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncAllSTA"))
            {
                m_strInAllSTA = dataRow["IncncAllSTA"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncAllETA"))
            {
                m_strInAllETA = dataRow["IncncAllETA"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncAllTDWN"))
            {
                m_strInAllTDWN = dataRow["IncncAllTDWN"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncniAllViewIndex"))
            {
                m_strInAllViewIndex = dataRow["IncniAllViewIndex"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncAllATA"))
            {
                m_strInAllATA = dataRow["IncncAllATA"].ToString();
            }
            

            if (dataRow.Table.Columns.Contains("IncncAllStatus"))
            {
                m_strInAllStatus = dataRow["IncncAllStatus"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcLONG_REG"))
            {
                m_strInLONG_REG = dataRow["IncnvcLONG_REG"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncFlightDate"))
            {
                m_strInFlightDate = dataRow["IncncFlightDate"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcFlightNo"))
            {
                m_strInFlightNo = dataRow["IncnvcFlightNo"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncACTYP"))
            {
                m_strInACTYP = dataRow["IncncACTYP"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcFlightCharacterAbbreviate"))
            {
                m_strInFlightCharacterAbbreviate = dataRow["IncnvcFlightCharacterAbbreviate"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncDEPAirportCNAME"))
            {
                m_strInDEPAirportCNAME = dataRow["IncncDEPAirportCNAME"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncARRAirportCNAME"))
            {
                m_strInARRAirportCNAME = dataRow["IncncARRAirportCNAME"].ToString();
            }
            //计划到达
            if (dataRow.Table.Columns.Contains("IncncSTA"))
            {
                m_strInSTA = dataRow["IncncSTA"].ToString();
            }
            //预计到达
            if (dataRow.Table.Columns.Contains("IncncETA"))
            {
                m_strInETA = dataRow["IncncETA"].ToString();
            }
            //落地时间
            if (dataRow.Table.Columns.Contains("IncncTDWN"))
            {
                m_strInTDWN = dataRow["IncncTDWN"].ToString();
            }
            //到位时间
            if (dataRow.Table.Columns.Contains("IncncATA"))
            {
                m_strInATA = dataRow["IncncATA"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncACARSTOFF"))
            {
                m_strInACARSTOFF = dataRow["IncncACARSTOFF"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncncACARSTDWN"))
            {
                m_strInACARSTDWN = dataRow["IncncACARSTDWN"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncACARSATA"))
            {
                m_strInACARSATA = dataRow["IncncACARSATA"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInDelayName"))
            {
                m_strInInDelayName = dataRow["IncnvcInDelayName"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcFlightDelayName"))
            {
                m_strInFlightDelayName = dataRow["IncnvcFlightDelayName"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcDiversionDelayName"))
            {
                m_strInDiversionDelayName = dataRow["IncnvcDiversionDelayName"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnvcInCarInfor"))
            {
                m_strInInCarInfor = dataRow["IncnvcInCarInfor"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncncInCarDepTime"))
            {
                m_strInInCarDepTime = dataRow["IncncInCarDepTime"].ToString();
            }
            //进港航班APU|接电源车
            if (dataRow.Table.Columns.Contains("IncncInPowerCarStartTime"))
            {
                m_strIncncInPowerCarStartTime = dataRow["IncncInPowerCarStartTime"].ToString();
            }
            //进港航班APU|APU关
            if (dataRow.Table.Columns.Contains("IncncAPUDown"))
            {
                m_strIncncAPUDown = dataRow["IncncAPUDown"].ToString();
            }
            //进港航班APU|撤电源车
            if (dataRow.Table.Columns.Contains("IncncInPowerCarEndTime"))
            {
                m_strIncncInPowerCarEndTime = dataRow["IncncInPowerCarEndTime"].ToString();
            }
            //进港航班APU|电源车号
            if (dataRow.Table.Columns.Contains("IncncInPowerCarNo"))
            {
                m_IncncInPowerCarNo = dataRow["IncncInPowerCarNo"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncncInCarArrTime"))
            {
                m_strInInCarArrTime = dataRow["IncncInCarArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnbVIPTag"))
            {
                m_strInVIPTag = dataRow["IncnbVIPTag"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncniCheckNum"))
            {
                m_strInCheckNum = dataRow["IncniCheckNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnbTransitPaxTag"))
            {
                m_strInTransitPaxTag = dataRow["IncnbTransitPaxTag"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncniBaggageWeight"))
            {
                m_strInBaggageWeight = dataRow["IncniBaggageWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncniCargoWeight"))
            {
                m_strInCargoWeight = dataRow["IncniCargoWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncncStatusName"))
            {
                m_strInStatusName = dataRow["IncncStatusName"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnvcInRemark"))
            {
                m_strInInRemark = dataRow["IncnvcInRemark"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnvcInAircraftStatus"))
            {
                m_strInInAircraftStatus = dataRow["IncnvcInAircraftStatus"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnvcInGATE"))
            {
                m_strInInGATE = dataRow["IncnvcInGATE"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncDATOP"))
            {
                m_strOutDATOP = dataRow["OutcncDATOP"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcFLTID"))
            {
                m_strOutFLTID = dataRow["OutcnvcFLTID"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniLEGNO"))
            {
                m_strOutLEGNO = dataRow["OutcniLEGNO"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcAC"))
            {
                m_strOutAC = dataRow["OutcnvcAC"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncAllSTD"))
            {
                m_strOutAllSTD = dataRow["OutcncAllSTD"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncAllETD"))
            {
                m_strOutAllETD = dataRow["OutcncAllETD"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncAllATD"))
            {
                m_strOutAllATD = dataRow["OutcncAllATD"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncAllTOFF"))
            {
                m_strOutAllTOFF = dataRow["OutcncAllTOFF"].ToString();
            }
           

            if (dataRow.Table.Columns.Contains("OutcncAllStatus"))
            {
                m_strOutAllStatus = dataRow["OutcncAllStatus"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniAllViewIndex"))
            {
                m_strOutAllViewIndex = dataRow["OutcniAllViewIndex"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutGate"))
            {
                m_strOutOutGate = dataRow["OutcnvcOutGate"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcLONG_REG"))
            {
                m_strOutLONG_REG = dataRow["OutcnvcLONG_REG"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncFlightDate"))
            {
                m_strOutFlightDate = dataRow["OutcncFlightDate"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcFlightNo"))
            {
                m_strOutFlightNo = dataRow["OutcnvcFlightNo"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcFlightCharacterAbbreviate"))
            {
                m_strOutFlightCharacterAbbreviate = dataRow["OutcnvcFlightCharacterAbbreviate"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDEPAirportCNAME"))
            {
                m_strOutDEPAirportCNAME = dataRow["OutcncDEPAirportCNAME"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncARRAirportCNAME"))
            {
                m_strOutARRAirportCNAME = dataRow["OutcncARRAirportCNAME"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncSTD"))
            {
                m_strOutSTD = dataRow["OutcncSTD"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncETD"))
            {
                m_strOutETD = dataRow["OutcncETD"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncStartGuaranteeTime"))
            {
                m_strOutStartGuaranteeTime = dataRow["OutcncStartGuaranteeTime"].ToString();
            }
            //出港航班APU|接电源车
            if (dataRow.Table.Columns.Contains("OutcncOutPowerCarStartTime"))
            {
                m_strOutcncOutPowerCarStartTime = dataRow["OutcncOutPowerCarStartTime"].ToString();
            }
            //出港航班APU|APU开
            if (dataRow.Table.Columns.Contains("OutcncAPUUp"))
            {
                m_strOutcncAPUUp = dataRow["OutcncAPUUp"].ToString();
            }
            //出港航班APU|撤电源车
            if (dataRow.Table.Columns.Contains("OutcncOutPowerCarEndTime"))
            {
                m_strOutcncOutPowerCarEndTime = dataRow["OutcncOutPowerCarEndTime"].ToString();
            }
            //出港航班APU|电源车号
            if (dataRow.Table.Columns.Contains("OutcncOutPowerCarNo"))
            {
                m_strOutcncOutPowerCarNo = dataRow["OutcncOutPowerCarNo"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniIntermissionTime"))
            {
                m_strOutIntermissionTime = dataRow["OutcniIntermissionTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcJoinFlight"))
            {
                m_strOutJoinFlight = dataRow["OutcnvcJoinFlight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcCheckCounter"))
            {
                m_strOutCheckCounter = dataRow["OutcnvcCheckCounter"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcWaitHall"))
            {
                m_strOutWaitHall = dataRow["OutcnvcWaitHall"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcBoradingGate"))
            {
                m_strOutBoradingGate = dataRow["OutcnvcBoradingGate"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOpenCabinTime"))
            {
                m_strOutOpenCabinTime = dataRow["OutcncOpenCabinTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutCarInfor"))
            {
                m_strOutOutCarInfor = dataRow["OutcnvcOutCarInfor"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOutCarDepTime"))
            {
                m_strOutOutCarDepTime = dataRow["OutcncOutCarDepTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOutCarArrTime"))
            {
                m_strOutOutCarArrTime = dataRow["OutcncOutCarArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncPilotArrTime"))
            {
                m_strOutPilotArrTime = dataRow["OutcncPilotArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncStewardArrTime"))
            {
                m_strOutStewardArrTime = dataRow["OutcncStewardArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDeiceStartTime"))
            {
                m_strOutDeiceStartTime = dataRow["OutcncDeiceStartTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDeiceEndTime"))
            {
                m_strOutDeiceEndTime = dataRow["OutcncDeiceEndTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncTaskSheetChangeTime"))
            {
                m_strOutTaskSheetChangeTime = dataRow["OutcncTaskSheetChangeTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDispatchTime"))
            {
                m_strOutDispatchTime = dataRow["OutcncDispatchTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDispatchPrintTime"))
            {
                m_strOutDispatchPrintTime = dataRow["OutcncDispatchPrintTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOutMCCTechEngr"))
            {
                m_strOutOutMCCTechEngr = dataRow["OutcncOutMCCTechEngr"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOutMCCDispEngr"))
            {
                m_strOutOutMCCDispEngr = dataRow["OutcncOutMCCDispEngr"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOutMCCReadyTime"))
            {
                m_strOutOutMCCReadyTime = dataRow["OutcncOutMCCReadyTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncncInMCCReadyTime"))
            {
                m_strInInMCCReadyTime = dataRow["IncncInMCCReadyTime"].ToString();
            }
           
            if (dataRow.Table.Columns.Contains("IncncInTime"))
            {
                m_strInInTime = dataRow["IncncInTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncMCCReleaseTime"))
            {
                m_strOutMCCReleaseTime = dataRow["OutcncMCCReleaseTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOutTime"))
            {
                m_strOutOutTime = dataRow["OutcncOutTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcAircraftStatus"))
            {
                m_strOutAircraftStatus = dataRow["OutcnvcAircraftStatus"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDeplaneTime"))
            {
                m_strOutDeplaneTime = dataRow["OutcncDeplaneTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCleanStartTime"))
            {
                m_strOutCleanStartTime = dataRow["OutcncCleanStartTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCleanEndTime"))
            {
                m_strOutCleanEndTime = dataRow["OutcncCleanEndTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncSewageStartTime"))
            {
                m_strOutSewageStartTime = dataRow["OutcncSewageStartTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncSewageEndTime"))
            {
                m_strOutSewageEndTime = dataRow["OutcncSewageEndTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCabinSupplyStartTime"))
            {
                m_strOutCabinSupplyStartTime = dataRow["OutcncCabinSupplyStartTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCabinSupplyEndTime"))
            {
                m_strOutCabinSupplyEndTime = dataRow["OutcncCabinSupplyEndTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOilStartTime"))
            {
                m_strOutOilStartTime = dataRow["OutcncOilStartTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOilEndTime"))
            {
                m_strOutOilEndTime = dataRow["OutcncOilEndTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncOpenCargoCabinTime"))
            {
                m_strOutOpenCargoCabinTime = dataRow["OutcncOpenCargoCabinTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCargoStartTime"))
            {
                m_strOutCargoStartTime = dataRow["OutcncCargoStartTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncBaggageTime"))
            {
                m_strOutBaggageTime = dataRow["OutcncBaggageTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCloseCargoCabinTime"))
            {
                m_strOutCloseCargoCabinTime = dataRow["OutcncCloseCargoCabinTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncTrailerArrTime"))
            {
                m_strOutTrailerArrTime = dataRow["OutcncTrailerArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDirtyWaterCarArrTime"))
            {
                m_strOutDirtyWaterCarArrTime = dataRow["OutcncDirtyWaterCarArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncFerryDepTime"))
            {
                m_strOutFerryDepTime = dataRow["OutcncFerryDepTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncLastFerryArrTime"))
            {
                m_strOutLastFerryArrTime = dataRow["OutcncLastFerryArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncDragPoleArrTime"))
            {
                m_strOutDragPoleArrTime = dataRow["OutcncDragPoleArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncLadderCarArrTime"))
            {
                m_strOutLadderCarArrTime = dataRow["OutcncLadderCarArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncInformBoardTime"))
            {
                m_strOutInformBoardTime = dataRow["OutcncInformBoardTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncBoardTime"))
            {
                m_strOutBoardTime = dataRow["OutcncBoardTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcBookNum"))
            {
                m_strOutBookNum = dataRow["OutcnvcBookNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniCheckNum"))
            {
                m_strOutCheckNum = dataRow["OutcniCheckNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnbTransitPaxTag"))
            {
                m_strOutTransitPaxTag = dataRow["OutcnbTransitPaxTag"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniXCRNum"))
            {
                m_strOutXCRNum = dataRow["OutcniXCRNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniAdultNum"))
            {
                m_strOutAdultNum = dataRow["OutcniAdultNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniChildNum"))
            {
                m_strOutChildNum = dataRow["OutcniChildNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniInfantNum"))
            {
                m_strOutInfantNum = dataRow["OutcniInfantNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniFirstClassNum"))
            {
                m_strOutFirstClassNum = dataRow["OutcniFirstClassNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniOfficialClassNum"))
            {
                m_strOutOfficialClassNum = dataRow["OutcniOfficialClassNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniTouristClassNum"))
            {
                m_strOutTouristClassNum = dataRow["OutcniTouristClassNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniAscendingPaxNum"))
            {
                m_strOutAscendingPaxNum = dataRow["OutcniAscendingPaxNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcntPaxNameList"))
            {
                m_strOutPaxNameList = dataRow["OutcntPaxNameList"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncBoardOverTime"))
            {
                m_strOutBoardOverTime = dataRow["OutcncBoardOverTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncLoadSheetArrTime"))
            {
                m_strOutLoadSheetArrTime = dataRow["OutcncLoadSheetArrTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncClosePaxCabinTime"))
            {
                m_strOutClosePaxCabinTime = dataRow["OutcncClosePaxCabinTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniOpenTime"))
            {
                m_strOutOpenTime = dataRow["OutcniOpenTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncInternalGuestTogether"))
            {
                m_strOutInternalGuestTogether = dataRow["OutcncInternalGuestTogether"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncPushTime"))
            {
                m_strOutPushTime = dataRow["OutcncPushTime"].ToString();
            }
            //推出时间
            if (dataRow.Table.Columns.Contains("OutcncATD"))
            {
                m_strOutATD = dataRow["OutcncATD"].ToString();
            }
            //起飞时间
            if (dataRow.Table.Columns.Contains("OutcncTOFF"))
            {
                m_strOutTOFF = dataRow["OutcncTOFF"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncACARSOUT"))
            {
                m_strOutACARSOUT = dataRow["OutcncACARSOUT"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncACARSTOFF"))
            {
                m_strOutACARSTOFF = dataRow["OutcncACARSTOFF"].ToString();
            }            
            if (dataRow.Table.Columns.Contains("OutcnvcDisChargingDelName"))
            {
                m_strOutDisChargingDelName = dataRow["OutcnvcDisChargingDelName"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutDelayName"))
            {
                m_strOutOutDelayName = dataRow["OutcnvcOutDelayName"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcFlightDelayName"))
            {
                m_strOutFlightDelayName = dataRow["OutcnvcFlightDelayName"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcDiversionDelayName"))
            {
                m_strOutDiversionDelayName = dataRow["OutcnvcDiversionDelayName"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniCargoWeight"))
            {
                m_strOutCargoWeight = dataRow["OutcniCargoWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniMailWeight"))
            {
                m_strOutMailWeight = dataRow["OutcniMailWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniBaggageWeight"))
            {
                m_strOutBaggageWeight = dataRow["OutcniBaggageWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniAirMaterialWeight"))
            {
                m_strOutAirMaterialWeight = dataRow["OutcniAirMaterialWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcAirMaterialRemark"))
            {
                m_strOutAirMaterialRemark = dataRow["OutcnvcAirMaterialRemark"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniBaggageNum"))
            {
                m_strOutBaggageNum = dataRow["OutcniBaggageNum"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnbVIPTag"))
            {
                m_strOutVIPTag = dataRow["OutcnbVIPTag"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniTotalFuelWeight"))
            {
                m_strOutTotalFuelWeight = dataRow["OutcniTotalFuelWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniTripFuelWeight"))
            {
                m_strOutTripFuelWeight = dataRow["OutcniTripFuelWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniTaxiFuelWeight"))
            {
                m_strOutTaxiFuelWeight = dataRow["OutcniTaxiFuelWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniUnloadCargoWeight"))
            {
                m_strOutUnloadCargoWeight = dataRow["OutcniUnloadCargoWeight"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcCargoRemark"))
            {
                m_strOutCargoRemark  = dataRow["OutcnvcCargoRemark"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncStatusName"))
            {
                m_strOutStatusName = dataRow["OutcncStatusName"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutRemark"))
            {
                m_strOutOutRemark = dataRow["OutcnvcOutRemark"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcChiefController"))
            {
                m_strOutChiefController = dataRow["OutcnvcChiefController"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcAssistantController"))
            {
                m_strOutAssistantController = dataRow["OutcnvcAssistantController"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutFieldController"))
            {
                m_strOutOutFieldController = dataRow["OutcnvcOutFieldController"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcBalanceController"))
            {
                m_strOutBalanceController = dataRow["OutcnvcBalanceController"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniDischargingDelayTime"))
            {
                m_strOutDischargingDelayTime = dataRow["OutcniDischargingDelayTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniDUR1"))
            {
                m_strOutcniDUR1 = dataRow["OutcniDUR1"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcniFocusTag"))
            {
                m_strOutFocusTag = dataRow["OutcniFocusTag"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcArrangedTask"))
            {
                m_strOutArrangedTask = dataRow["OutcnvcArrangedTask"].ToString();
            }
            #endregion 以往

            #region added in 20101113
            if (dataRow.Table.Columns.Contains("IncncInMCCTechEngr"))
            {
                m_strInInMCCTechEngr = dataRow["IncncInMCCTechEngr"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnvcInArrangedTask"))
            {
                m_strInInArrangedTask = dataRow["IncnvcInArrangedTask"].ToString();
            }
            if (dataRow.Table.Columns.Contains("IncnvcInSMCRemark"))
            {
                m_strInInSMCRemark = dataRow["IncnvcInSMCRemark"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutSMCRemark"))
            {
                m_strOutOutSMCRemark = dataRow["OutcnvcOutSMCRemark"].ToString();
            }
            #endregion added in 20101113

            #region added in 20111031
            if (dataRow.Table.Columns.Contains("IncncInMCCReleaseTime"))
            {
                m_strInMCCReleaseTime = dataRow["IncncInMCCReleaseTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutWAOperationInfoYK"))
            {
                m_strOutWAOperationInfoYK = dataRow["OutcnvcOutWAOperationInfoYK"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutWAOperationInfoFX"))
            {
                m_strOutWAOperationInfoFX = dataRow["OutcnvcOutWAOperationInfoFX"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutWAOperationInfoKF"))
            {
                m_strOutWAOperationInfoKF = dataRow["OutcnvcOutWAOperationInfoKF"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutWAOperationInfoGC"))
            {
                m_strOutWAOperationInfoGC = dataRow["OutcnvcOutWAOperationInfoGC"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutWAOperationInfoAJ"))
            {
                m_strOutWAOperationInfoAJ = dataRow["OutcnvcOutWAOperationInfoAJ"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcnvcOutWAOperationInfoSC"))
            {
                m_strOutWAOperationInfoSC = dataRow["OutcnvcOutWAOperationInfoSC"].ToString();
            }
            #endregion added in 20111031

            #region added in 20121113
            if (dataRow.Table.Columns.Contains("OutcncCDMeOutTime"))
            {
                m_strOutcncCDMeOutTime = dataRow["OutcncCDMeOutTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCDMeOffTime"))
            {
                m_strOutcncCDMeOffTime = dataRow["OutcncCDMeOffTime"].ToString();
            }
            if (dataRow.Table.Columns.Contains("OutcncCDMvStartTime"))
            {
                m_strOutcncCDMvStartTime = dataRow["OutcncCDMvStartTime"].ToString();
            }
            #endregion added in 20121113

            #region 三期 added in 20140514
            if (dataRow.Table.Columns.Contains("IncncInLadderCarGuaranteeStartTime"))
            {
                m_strincncinladdercarguaranteestarttime = dataRow["IncncInLadderCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInLadderCarGuaranteeEndTime"))
            {
                m_strincncinladdercarguaranteeendtime = dataRow["IncncInLadderCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInBridgeGuaranteeStartTime"))
            {
                m_strincncinbridgeguaranteestarttime = dataRow["IncncInBridgeGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInBridgeGuaranteeEndTime"))
            {
                m_strincncinbridgeguaranteeendtime = dataRow["IncncInBridgeGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInFerryrGuaranteeStartTime"))
            {
                m_strincncinferryrguaranteestarttime = dataRow["IncncInFerryrGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInFerryGuaranteeEndTime"))
            {
                m_strincncinferryguaranteeendtime = dataRow["IncncInFerryGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInFerryGuaranteeFirstArrivePlaneTime"))
            {
                m_strincncinferryguaranteefirstarriveplanetime = dataRow["IncncInFerryGuaranteeFirstArrivePlaneTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInFerryGuaranteeLastArrivePlaneTime"))
            {
                m_strincncinferryguaranteelastarriveplanetime = dataRow["IncncInFerryGuaranteeLastArrivePlaneTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInFerryrGuaranteeArriveTime"))
            {
                m_strincncinferryrguaranteearrivetime = dataRow["IncncInFerryrGuaranteeArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCrewCarrGuaranteeStartTime"))
            {
                m_strincncincrewcarrguaranteestarttime = dataRow["IncncInCrewCarrGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCrewCarGuaranteeEndTime"))
            {
                m_strincncincrewcarguaranteeendtime = dataRow["IncncInCrewCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInConveyerBeltVehicleGuaranteeStartTime"))
            {
                m_strincncinconveyerbeltvehicleguaranteestarttime = dataRow["IncncInConveyerBeltVehicleGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInConveyerBeltVehicleGuaranteeEndTime"))
            {
                m_strincncinconveyerbeltvehicleguaranteeendtime = dataRow["IncncInConveyerBeltVehicleGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInPlatformCarGuaranteeStartTime"))
            {
                m_strincncinplatformcarguaranteestarttime = dataRow["IncncInPlatformCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInPlatformCarGuaranteeEndTime"))
            {
                m_strincncinplatformcarguaranteeendtime = dataRow["IncncInPlatformCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInTrailCarGuaranteeStartTime"))
            {
                m_strincncintrailcarguaranteestarttime = dataRow["IncncInTrailCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInTrailCarGuaranteeEndTime"))
            {
                m_strincncintrailcarguaranteeendtime = dataRow["IncncInTrailCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInTrailCarGuaranteeArriveTime"))
            {
                m_strincncintrailcarguaranteearrivetime = dataRow["IncncInTrailCarGuaranteeArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInTrailCarGuaranteeHoldTheWheelTime"))
            {
                m_strincncintrailcarguaranteeholdthewheeltime = dataRow["IncncInTrailCarGuaranteeHoldTheWheelTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInPowerCarGuaranteeStartTime"))
            {
                m_strincncinpowercarguaranteestarttime = dataRow["IncncInPowerCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInPowerCarGuaranteeEndTime"))
            {
                m_strincncinpowercarguaranteeendtime = dataRow["IncncInPowerCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInAirSupplyCarGuaranteeStartTime"))
            {
                m_strincncinairsupplycarguaranteestarttime = dataRow["IncncInAirSupplyCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInAirSupplyCarGuaranteeEndTime"))
            {
                m_strincncinairsupplycarguaranteeendtime = dataRow["IncncInAirSupplyCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInAirConditionerCarGuaranteeStartTime"))
            {
                m_strincncinairconditionercarguaranteestarttime = dataRow["IncncInAirConditionerCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInAirConditionerCarGuaranteeEndTime"))
            {
                m_strincncinairconditionercarguaranteeendtime = dataRow["IncncInAirConditionerCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCleanWaterCarGuaranteeStartTime"))
            {
                m_strincncincleanwatercarguaranteestarttime = dataRow["IncncInCleanWaterCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCleanWaterCarGuaranteeEndTime"))
            {
                m_strincncincleanwatercarguaranteeendtime = dataRow["IncncInCleanWaterCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInSewageCarGuaranteeStartTime"))
            {
                m_strincncinsewagecarguaranteestarttime = dataRow["IncncInSewageCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInSewageCarGuaranteeEndTime"))
            {
                m_strincncinsewagecarguaranteeendtime = dataRow["IncncInSewageCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutLadderCarrGuaranteeStartTime"))
            {
                m_stroutcncoutladdercarrguaranteestarttime = dataRow["OutcncOutLadderCarrGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutLadderCarGuaranteeEndTime"))
            {
                m_stroutcncoutladdercarguaranteeendtime = dataRow["OutcncOutLadderCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutBridgerGuaranteeStartTime"))
            {
                m_stroutcncoutbridgerguaranteestarttime = dataRow["OutcncOutBridgerGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutBridgeGuaranteeEndTime"))
            {
                m_stroutcncoutbridgeguaranteeendtime = dataRow["OutcncOutBridgeGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryrGuaranteeStartTime"))
            {
                m_stroutcncoutferryrguaranteestarttime = dataRow["OutcncOutFerryrGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryGuaranteeEndTime"))
            {
                m_stroutcncoutferryguaranteeendtime = dataRow["OutcncOutFerryGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryGuaranteeFirstOutTime"))
            {
                m_stroutcncoutferryguaranteefirstouttime = dataRow["OutcncOutFerryGuaranteeFirstOutTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryGuaranteeFirstArriveGateTime"))
            {
                m_stroutcncoutferryguaranteefirstarrivegatetime = dataRow["OutcncOutFerryGuaranteeFirstArriveGateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryGuaranteeFirstArrivePlaneTime"))
            {
                m_stroutcncoutferryguaranteefirstarriveplanetime = dataRow["OutcncOutFerryGuaranteeFirstArrivePlaneTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryGuaranteeLastOutTime"))
            {
                m_stroutcncoutferryguaranteelastouttime = dataRow["OutcncOutFerryGuaranteeLastOutTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryGuaranteeLastArriveGateTime"))
            {
                m_stroutcncoutferryguaranteelastarrivegatetime = dataRow["OutcncOutFerryGuaranteeLastArriveGateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryGuaranteeLastArrivePlaneTime"))
            {
                m_stroutcncoutferryguaranteelastarriveplanetime = dataRow["OutcncOutFerryGuaranteeLastArrivePlaneTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFerryrGuaranteeArriveTime"))
            {
                m_stroutcncoutferryrguaranteearrivetime = dataRow["OutcncOutFerryrGuaranteeArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCrewCarrGuaranteeStartTime"))
            {
                m_stroutcncoutcrewcarrguaranteestarttime = dataRow["OutcncOutCrewCarrGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCrewCarGuaranteeEndTime"))
            {
                m_stroutcncoutcrewcarguaranteeendtime = dataRow["OutcncOutCrewCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutConveyerBeltVehicleGuaranteeStartTime"))
            {
                m_stroutcncoutconveyerbeltvehicleguaranteestarttime = dataRow["OutcncOutConveyerBeltVehicleGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutConveyerBeltVehicleGuaranteeEndTime"))
            {
                m_stroutcncoutconveyerbeltvehicleguaranteeendtime = dataRow["OutcncOutConveyerBeltVehicleGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPlatformCarGuaranteeStartTime"))
            {
                m_stroutcncoutplatformcarguaranteestarttime = dataRow["OutcncOutPlatformCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPlatformCarGuaranteeEndTime"))
            {
                m_stroutcncoutplatformcarguaranteeendtime = dataRow["OutcncOutPlatformCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutTrailCarGuaranteeStartTime"))
            {
                m_stroutcncouttrailcarguaranteestarttime = dataRow["OutcncOutTrailCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutTrailCarGuaranteeEndTime"))
            {
                m_stroutcncouttrailcarguaranteeendtime = dataRow["OutcncOutTrailCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutTrailCarGuaranteeArriveTime"))
            {
                m_stroutcncouttrailcarguaranteearrivetime = dataRow["OutcncOutTrailCarGuaranteeArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutTrailCarGuaranteeHoldTheWheelTime"))
            {
                m_stroutcncouttrailcarguaranteeholdthewheeltime = dataRow["OutcncOutTrailCarGuaranteeHoldTheWheelTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPowerCarGuaranteeStartTime"))
            {
                m_stroutcncoutpowercarguaranteestarttime = dataRow["OutcncOutPowerCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPowerCarGuaranteeEndTime"))
            {
                m_stroutcncoutpowercarguaranteeendtime = dataRow["OutcncOutPowerCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutAirSupplyCarGuaranteeStartTime"))
            {
                m_stroutcncoutairsupplycarguaranteestarttime = dataRow["OutcncOutAirSupplyCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutAirSupplyCarGuaranteeEndTime"))
            {
                m_stroutcncoutairsupplycarguaranteeendtime = dataRow["OutcncOutAirSupplyCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutAirConditionerCarGuaranteeStartTime"))
            {
                m_stroutcncoutairconditionercarguaranteestarttime = dataRow["OutcncOutAirConditionerCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutAirConditionerCarGuaranteeEndTime"))
            {
                m_stroutcncoutairconditionercarguaranteeendtime = dataRow["OutcncOutAirConditionerCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCleanWaterCarGuaranteeStartTime"))
            {
                m_stroutcncoutcleanwatercarguaranteestarttime = dataRow["OutcncOutCleanWaterCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCleanWaterCarGuaranteeEndTime"))
            {
                m_stroutcncoutcleanwatercarguaranteeendtime = dataRow["OutcncOutCleanWaterCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutSewageCarGuaranteeStartTime"))
            {
                m_stroutcncoutsewagecarguaranteestarttime = dataRow["OutcncOutSewageCarGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutSewageCarGuaranteeEndTime"))
            {
                m_stroutcncoutsewagecarguaranteeendtime = dataRow["OutcncOutSewageCarGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFuelFillingStartTime"))
            {
                m_stroutcncoutfuelfillingstarttime = dataRow["OutcncOutFuelFillingStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFuelFillingEndTime"))
            {
                m_stroutcncoutfuelfillingendtime = dataRow["OutcncOutFuelFillingEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCabinSupplyStartTime"))
            {
                m_stroutcncoutcabinsupplystarttime = dataRow["OutcncOutCabinSupplyStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCabinSupplyEndTime"))
            {
                m_stroutcncoutcabinsupplyendtime = dataRow["OutcncOutCabinSupplyEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCargoMailBaggageGuaranteeStartTime"))
            {
                m_strincncincargomailbaggageguaranteestarttime = dataRow["IncncInCargoMailBaggageGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCargoMailBaggageGuaranteeEndTime"))
            {
                m_strincncincargomailbaggageguaranteeendtime = dataRow["IncncInCargoMailBaggageGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCargoCabinOpenTime"))
            {
                m_strincncincargocabinopentime = dataRow["IncncInCargoCabinOpenTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCargoCabinCloseTime"))
            {
                m_strincncincargocabinclosetime = dataRow["IncncInCargoCabinCloseTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInBaggageUnloadStartTime"))
            {
                m_strincncinbaggageunloadstarttime = dataRow["IncncInBaggageUnloadStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInBaggageUnloadEndTime"))
            {
                m_strincncinbaggageunloadendtime = dataRow["IncncInBaggageUnloadEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCargoUnloadStartTime"))
            {
                m_strincncincargounloadstarttime = dataRow["IncncInCargoUnloadStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCargoUnloadEndTime"))
            {
                m_strincncincargounloadendtime = dataRow["IncncInCargoUnloadEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCargoMailBaggageGuaranteeStartTime"))
            {
                m_stroutcncoutcargomailbaggageguaranteestarttime = dataRow["OutcncOutCargoMailBaggageGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCargoMailBaggageGuaranteeEndTime"))
            {
                m_stroutcncoutcargomailbaggageguaranteeendtime = dataRow["OutcncOutCargoMailBaggageGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCargoMailBaggageReportTime"))
            {
                m_stroutcncoutcargomailbaggagereporttime = dataRow["OutcncOutCargoMailBaggageReportTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutInfoPickUpBaggageTime"))
            {
                m_stroutcncoutinfopickupbaggagetime = dataRow["OutcncOutInfoPickUpBaggageTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCargoCabinOpenTime"))
            {
                m_stroutcncoutcargocabinopentime = dataRow["OutcncOutCargoCabinOpenTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCargoCabinCloseTime"))
            {
                m_stroutcncoutcargocabinclosetime = dataRow["OutcncOutCargoCabinCloseTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutBaggageLoadStartTime"))
            {
                m_stroutcncoutbaggageloadstarttime = dataRow["OutcncOutBaggageLoadStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutBaggageLoadEndTime"))
            {
                m_stroutcncoutbaggageloadendtime = dataRow["OutcncOutBaggageLoadEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCargoLoadStartTime"))
            {
                m_stroutcncoutcargoloadstarttime = dataRow["OutcncOutCargoLoadStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCargoLoadEndTime"))
            {
                m_stroutcncoutcargoloadendtime = dataRow["OutcncOutCargoLoadEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInPilotBrakeTime"))
            {
                m_strincncinpilotbraketime = dataRow["IncncInPilotBrakeTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPilotArriveTime"))
            {
                m_stroutcncoutpilotarrivetime = dataRow["OutcncOutPilotArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPilotLooseSkidsTime"))
            {
                m_stroutcncoutpilotlooseskidstime = dataRow["OutcncOutPilotLooseSkidsTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPilotPushOffTime"))
            {
                m_stroutcncoutpilotpushofftime = dataRow["OutcncOutPilotPushOffTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCabinOpenTime"))
            {
                m_strincncincabinopentime = dataRow["IncncInCabinOpenTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCabinCloseTime"))
            {
                m_strincncincabinclosetime = dataRow["IncncInCabinCloseTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCabinOpenTime"))
            {
                m_stroutcncoutcabinopentime = dataRow["OutcncOutCabinOpenTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCabinCloseTime"))
            {
                m_stroutcncoutcabinclosetime = dataRow["OutcncOutCabinCloseTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutStewardArriveTime"))
            {
                m_stroutcncoutstewardarrivetime = dataRow["OutcncOutStewardArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCheckInGuaranteeStartTime"))
            {
                m_stroutcncoutcheckinguaranteestarttime = dataRow["OutcncOutCheckInGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCheckInGuaranteeEndTime"))
            {
                m_stroutcncoutcheckinguaranteeendtime = dataRow["OutcncOutCheckInGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCabinCleanStartTime"))
            {
                m_strincncincabincleanstarttime = dataRow["IncncInCabinCleanStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInCabinCleanEndTime"))
            {
                m_strincncincabincleanendtime = dataRow["IncncInCabinCleanEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCabinCleanStartTime"))
            {
                m_stroutcncoutcabincleanstarttime = dataRow["OutcncOutCabinCleanStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCabinCleanEndTime"))
            {
                m_stroutcncoutcabincleanendtime = dataRow["OutcncOutCabinCleanEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInGuestOutStartTime"))
            {
                m_strincncinguestoutstarttime = dataRow["IncncInGuestOutStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInGuestOutEndTime"))
            {
                m_strincncinguestoutendtime = dataRow["IncncInGuestOutEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInGuiderArriveTime"))
            {
                m_strincncinguiderarrivetime = dataRow["IncncInGuiderArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInGuiderGuaranteeStartTime"))
            {
                m_strincncinguiderguaranteestarttime = dataRow["IncncInGuiderGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInGuiderGuaranteeEndTime"))
            {
                m_strincncinguiderguaranteeendtime = dataRow["IncncInGuiderGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutGuestInStartTime"))
            {
                m_stroutcncoutguestinstarttime = dataRow["OutcncOutGuestInStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutGuestInEndTime"))
            {
                m_stroutcncoutguestinendtime = dataRow["OutcncOutGuestInEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutGuiderArriveTime"))
            {
                m_stroutcncoutguiderarrivetime = dataRow["OutcncOutGuiderArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutGuiderGuaranteeStartTime"))
            {
                m_stroutcncoutguiderguaranteestarttime = dataRow["OutcncOutGuiderGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutGuiderGuaranteeEndTime"))
            {
                m_stroutcncoutguiderguaranteeendtime = dataRow["OutcncOutGuiderGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutInformBoardTime"))
            {
                m_stroutcncoutinformboardtime = dataRow["OutcncOutInformBoardTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutBoardingGateCloseTime"))
            {
                m_stroutcncoutboardinggateclosetime = dataRow["OutcncOutBoardingGateCloseTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutLoadsheetUploadFinishedTime"))
            {
                m_stroutcncoutloadsheetuploadfinishedtime = dataRow["OutcncOutLoadsheetUploadFinishedTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCaptainConfirmTime"))
            {
                m_stroutcncoutcaptainconfirmtime = dataRow["OutcncOutCaptainConfirmTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInVipPickUpGuaranteeStartTime"))
            {
                m_strincncinvippickupguaranteestarttime = dataRow["IncncInVipPickUpGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInVipPickUpGuaranteeEndTime"))
            {
                m_strincncinvippickupguaranteeendtime = dataRow["IncncInVipPickUpGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutVipSeeOffGuaranteeStartTime"))
            {
                m_stroutcncoutvipseeoffguaranteestarttime = dataRow["OutcncOutVipSeeOffGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutVipSeeOffGuaranteeEndTime"))
            {
                m_stroutcncoutvipseeoffguaranteeendtime = dataRow["OutcncOutVipSeeOffGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCustomGuaranteeStartTime"))
            {
                m_stroutcncoutcustomguaranteestarttime = dataRow["OutcncOutCustomGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCustomGuaranteeEndTime"))
            {
                m_stroutcncoutcustomguaranteeendtime = dataRow["OutcncOutCustomGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFrontierInspectionGuaranteeStartTime"))
            {
                m_stroutcncoutfrontierinspectionguaranteestarttime = dataRow["OutcncOutFrontierInspectionGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFrontierInspectionGuaranteeEndTime"))
            {
                m_stroutcncoutfrontierinspectionguaranteeendtime = dataRow["OutcncOutFrontierInspectionGuaranteeEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutQuarantineGuaranteeStartTime"))
            {
                m_stroutcncoutquarantineguaranteestarttime = dataRow["OutcncOutQuarantineGuaranteeStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutQuarantineGuaranteeEndTime"))
            {
                m_stroutcncoutquarantineguaranteeendtime = dataRow["OutcncOutQuarantineGuaranteeEndTime"].ToString();
            }


            if (dataRow.Table.Columns.Contains("IncnvcInGuaranteeRecord")) //进港 保障记录
            {
                m_strincnvcinguaranteerecord = dataRow["IncnvcInGuaranteeRecord"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutGuaranteeRecord")) //出港 保障记录
            {
                m_stroutcnvcoutguaranteerecord = dataRow["OutcnvcOutGuaranteeRecord"].ToString();
            }
            #endregion 三期 added in 20140514

            #region 三期 added in 201504
            if (dataRow.Table.Columns.Contains("OutcnvcOutRunway"))
            {
                _OutcnvcOutRunway = dataRow["OutcnvcOutRunway"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInRunway"))
            {
                _IncnvcInRunway = dataRow["IncnvcInRunway"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncncInEXIT"))
            {
                _IncncInEXIT = dataRow["IncncInEXIT"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutTOBT"))
            {
                _OutcncOutTOBT = dataRow["OutcncOutTOBT"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutBoardStartTime"))
            {
                _OutcncOutBoardStartTime = dataRow["OutcncOutBoardStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutBoardEndTime"))
            {
                _OutcncOutBoardEndTime = dataRow["OutcncOutBoardEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCheckCounterEndTime"))
            {
                _OutcncOutCheckCounterEndTime = dataRow["OutcncOutCheckCounterEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutDeicePing"))
            {
                _OutcnvcOutDeicePing = dataRow["OutcnvcOutDeicePing"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutDeiceWei"))
            {
                _OutcnvcOutDeiceWei = dataRow["OutcnvcOutDeiceWei"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutArriveDeicePing"))
            {
                _OutcncOutArriveDeicePing = dataRow["OutcncOutArriveDeicePing"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutDeiceStartTime"))
            {
                _OutcncOutDeiceStartTime = dataRow["OutcncOutDeiceStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutDeiceEndTime"))
            {
                _OutcncOutDeiceEndTime = dataRow["OutcncOutDeiceEndTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutLeaveDeicePing"))
            {
                _OutcncOutLeaveDeicePing = dataRow["OutcncOutLeaveDeicePing"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutSlowDeiceFlag"))
            {
                _OutcnvcOutSlowDeiceFlag = dataRow["OutcnvcOutSlowDeiceFlag"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutTSAT"))
            {
                _OutcncOutTSAT = dataRow["OutcncOutTSAT"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCTOT"))
            {
                _OutcncOutCTOT = dataRow["OutcncOutCTOT"].ToString();
            }

            #endregion 三期 added in 201504

            #region 三期 added in 20150428
            if (dataRow.Table.Columns.Contains("OutcncOutFirstPassengerComeInCabinTime"))
            {
                _OutcncOutFirstPassengerComeInCabinTime = dataRow["OutcncOutFirstPassengerComeInCabinTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutPlaneReadyEndTime"))
            {
                _OutcncOutPlaneReadyEndTime = dataRow["OutcncOutPlaneReadyEndTime"].ToString();
            }
            #endregion 三期 added in 20150428

            #region 三期 added in 20150624
            if (dataRow.Table.Columns.Contains("IncnvcInGATE_Test"))
            {
                _IncnvcInGATE_Test = dataRow["IncnvcInGATE_Test"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutGate_Test"))
            {
                _OutcnvcOutGate_Test = dataRow["OutcnvcOutGate_Test"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutFlightInterceptTime"))
            {
                _OutcncOutFlightInterceptTime = dataRow["OutcncOutFlightInterceptTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInTurnTableNO"))
            {
                _IncnvcInTurnTableNO = dataRow["IncnvcInTurnTableNO"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutTurnTableNO"))
            {
                _OutcnvcOutTurnTableNO = dataRow["OutcnvcOutTurnTableNO"].ToString();
            }

            #endregion 三期 added in 20150624

            #region 三期 added in 20150730
            if (dataRow.Table.Columns.Contains("OutcncOutCrewStartTime"))
            {
                _OutcncOutCrewStartTime = dataRow["OutcncOutCrewStartTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCrewArriveTime"))
            {
                _OutcncOutCrewArriveTime = dataRow["OutcncOutCrewArriveTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutOverStationType"))
            {
                _OutcnvcOutOverStationType = dataRow["OutcnvcOutOverStationType"].ToString();
            }
            #endregion 三期 added in 20150730

            #region 三期 added in 20160323
            if (dataRow.Table.Columns.Contains("OutcnvcOutSpotUsers"))
            {
                _OutcnvcOutSpotUsers = dataRow["OutcnvcOutSpotUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutMaintenanceUsers"))
            {
                _OutcnvcOutMaintenanceUsers = dataRow["OutcnvcOutMaintenanceUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutFleetUsers"))
            {
                _OutcnvcOutFleetUsers = dataRow["OutcnvcOutFleetUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutVIPRoomUsers"))
            {
                _OutcnvcOutVIPRoomUsers = dataRow["OutcnvcOutVIPRoomUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutAviationDietUsers"))
            {
                _OutcnvcOutAviationDietUsers = dataRow["OutcnvcOutAviationDietUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutCheckInUsers"))
            {
                _OutcnvcOutCheckInUsers = dataRow["OutcnvcOutCheckInUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutCleaningTeamUsers"))
            {
                _OutcnvcOutCleaningTeamUsers = dataRow["OutcnvcOutCleaningTeamUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInSpotUsers"))
            {
                _IncnvcInSpotUsers = dataRow["IncnvcInSpotUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInMaintenanceUsers"))
            {
                _IncnvcInMaintenanceUsers = dataRow["IncnvcInMaintenanceUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInFleetUsers"))
            {
                _IncnvcInFleetUsers = dataRow["IncnvcInFleetUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInVIPRoomUsers"))
            {
                _IncnvcInVIPRoomUsers = dataRow["IncnvcInVIPRoomUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInAviationDietUsers"))
            {
                _IncnvcInAviationDietUsers = dataRow["IncnvcInAviationDietUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInCheckInUsers"))
            {
                _IncnvcInCheckInUsers = dataRow["IncnvcInCheckInUsers"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInCleaningTeamUsers"))
            {
                _IncnvcInCleaningTeamUsers = dataRow["IncnvcInCleaningTeamUsers"].ToString();
            }
            #endregion 三期 added in 20160323

            #region 三期 added in 20160503
            if (dataRow.Table.Columns.Contains("OutcncSTA"))
            {
                _OutcncSTA = dataRow["OutcncSTA"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncETA"))
            {
                _OutcncETA = dataRow["OutcncETA"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncTDWN"))
            {
                _OutcncTDWN = dataRow["OutcncTDWN"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncATA"))
            {
                _OutcncATA = dataRow["OutcncATA"].ToString();
            }
            #endregion 三期 added in 20160503

            #region 三期 added in 20160527
            if (dataRow.Table.Columns.Contains("IncniInConveyerBeltVehicleGuaranteeCount"))
            {
                _IncniInConveyerBeltVehicleGuaranteeCount = dataRow["IncniInConveyerBeltVehicleGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutConveyerBeltVehicleGuaranteeCount"))
            {
                _OutcniOutConveyerBeltVehicleGuaranteeCount = dataRow["OutcniOutConveyerBeltVehicleGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncniInBaggageCarGuaranteeCount"))
            {
                _IncniInBaggageCarGuaranteeCount = dataRow["IncniInBaggageCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutBaggageCarGuaranteeCount"))
            {
                _OutcniOutBaggageCarGuaranteeCount = dataRow["OutcniOutBaggageCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncniInCrewCarGuaranteeCount"))
            {
                _IncniInCrewCarGuaranteeCount = dataRow["IncniInCrewCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutCrewCarGuaranteeCount"))
            {
                _OutcniOutCrewCarGuaranteeCount = dataRow["OutcniOutCrewCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncniInPlatformCarGuaranteeCount"))
            {
                _IncniInPlatformCarGuaranteeCount = dataRow["IncniInPlatformCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutPlatformCarGuaranteeCount"))
            {
                _OutcniOutPlatformCarGuaranteeCount = dataRow["OutcniOutPlatformCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncniInLadderCarGuaranteeCount"))
            {
                _IncniInLadderCarGuaranteeCount = dataRow["IncniInLadderCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutLadderCarGuaranteeCount"))
            {
                _OutcniOutLadderCarGuaranteeCount = dataRow["OutcniOutLadderCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncniInFerryGuaranteeCount"))
            {
                _IncniInFerryGuaranteeCount = dataRow["IncniInFerryGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutFerryGuaranteeCount"))
            {
                _OutcniOutFerryGuaranteeCount = dataRow["OutcniOutFerryGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutDeicingVehicleGuaranteeCount"))
            {
                _OutcniOutDeicingVehicleGuaranteeCount = dataRow["OutcniOutDeicingVehicleGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcniOutTrailCarGuaranteeCount"))
            {
                _OutcniOutTrailCarGuaranteeCount = dataRow["OutcniOutTrailCarGuaranteeCount"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutRemark_Fleet"))
            {
                _OutcnvcOutRemark_Fleet = dataRow["OutcnvcOutRemark_Fleet"].ToString();
            }

            if (dataRow.Table.Columns.Contains("IncnvcInRemark_Fleet"))
            {
                _IncnvcInRemark_Fleet = dataRow["IncnvcInRemark_Fleet"].ToString();
            }
            #endregion 三期 added in 20160527

            #region 三期 added in 20160616
            if (dataRow.Table.Columns.Contains("OutcnvcOutArrangeCargoOperator"))
            {
                _OutcnvcOutArrangeCargoOperator = dataRow["OutcnvcOutArrangeCargoOperator"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutArrangeCargoOperateTime"))
            {
                _OutcncOutArrangeCargoOperateTime = dataRow["OutcncOutArrangeCargoOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutRecheckLoadSheetOperator"))
            {
                _OutcnvcOutRecheckLoadSheetOperator = dataRow["OutcnvcOutRecheckLoadSheetOperator"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutRecheckLoadSheetOperateTime"))
            {
                _OutcncOutRecheckLoadSheetOperateTime = dataRow["OutcncOutRecheckLoadSheetOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutConfirmLoadSheetOperator"))
            {
                _OutcnvcOutConfirmLoadSheetOperator = dataRow["OutcnvcOutConfirmLoadSheetOperator"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutConfirmLoadSheetOperateTime"))
            {
                _OutcncOutConfirmLoadSheetOperateTime = dataRow["OutcncOutConfirmLoadSheetOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutConfirmLoadSheetOperateVerNumber"))
            {
                _OutcnvcOutConfirmLoadSheetOperateVerNumber = dataRow["OutcnvcOutConfirmLoadSheetOperateVerNumber"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutCheckManifestOperator"))
            {
                _OutcnvcOutCheckManifestOperator = dataRow["OutcnvcOutCheckManifestOperator"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCheckManifestOperateTime"))
            {
                _OutcncOutCheckManifestOperateTime = dataRow["OutcncOutCheckManifestOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutRecheckManifestOperator"))
            {
                _OutcnvcOutRecheckManifestOperator = dataRow["OutcnvcOutRecheckManifestOperator"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutRecheckManifestOperateTime"))
            {
                _OutcncOutRecheckManifestOperateTime = dataRow["OutcncOutRecheckManifestOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutUploadManifestOperator"))
            {
                _OutcnvcOutUploadManifestOperator = dataRow["OutcnvcOutUploadManifestOperator"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutUploadManifestOperateTime"))
            {
                _OutcncOutUploadManifestOperateTime = dataRow["OutcncOutUploadManifestOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutUploadManifestOperateResult"))
            {
                _OutcnvcOutUploadManifestOperateResult = dataRow["OutcnvcOutUploadManifestOperateResult"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutCrewConfirmManifestOperateResult"))
            {
                _OutcnvcOutCrewConfirmManifestOperateResult = dataRow["OutcnvcOutCrewConfirmManifestOperateResult"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutCrewConfirmManifestOperateTime"))
            {
                _OutcncOutCrewConfirmManifestOperateTime = dataRow["OutcncOutCrewConfirmManifestOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutTransportManifestOperator"))
            {
                _OutcnvcOutTransportManifestOperator = dataRow["OutcnvcOutTransportManifestOperator"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcncOutTransportManifestOperateTime"))
            {
                _OutcncOutTransportManifestOperateTime = dataRow["OutcncOutTransportManifestOperateTime"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutRemark_Balance"))
            {
                _OutcnvcOutRemark_Balance = dataRow["OutcnvcOutRemark_Balance"].ToString();
            }
            #endregion 三期 added in 20160616

            #region 三期 added in 20160704
            if (dataRow.Table.Columns.Contains("IncnvcInLeaders"))
            {
                _IncnvcInLeaders = dataRow["IncnvcInLeaders"].ToString();
            }

            if (dataRow.Table.Columns.Contains("OutcnvcOutLeaders"))
            {
                _OutcnvcOutLeaders = dataRow["OutcnvcOutLeaders"].ToString();
            }
            #endregion 三期 added in 20160704
        }

        #region 属性
        #region 以往
        /// <summary>
        /// 进港主键
        /// </summary>
        public string IncncDATOP
        {
            get { return m_strInDATOP; }
            set { m_strInDATOP = value; }
        }


        /// <summary>
        /// 进港主键
        /// </summary>
        public string IncnvcFLTID
        {
            get { return m_strInFLTID; }
            set { m_strInFLTID = value; }
        }

        /// <summary>
        /// 进港主键
        /// </summary>
        public string IncniLEGNO
        {
            get { return m_strInLEGNO; }
            set { m_strInLEGNO = value; }
        }

        /// <summary>
        /// 进港主键
        /// </summary>
        public string IncnvcAC
        {
            get { return m_strInAC; }
            set { m_strInAC = value; }
        }

        /// <summary>
        /// 进港计划到达时间（长格式）
        /// </summary>
        public string IncncAllSTA
        {
            get { return m_strInAllSTA; }
            set { m_strInAllSTA = value; }
        }

        /// <summary>
        /// 进港预计到达时间（长格式）
        /// </summary>
        public string IncncAllETA
        {
            get { return m_strInAllETA; }
            set { m_strInAllETA = value; }
        }

        /// <summary>
        /// 进港落地时间（长格式）
        /// </summary>
        public string IncncAllTDWN
        {
            get { return m_strInAllTDWN; }
            set { m_strInAllTDWN = value; }
        }

        /// <summary>
        /// 进港挡抡挡时间（长格式）
        /// </summary>
        public string IncncAllATA
        {
            get { return m_strInAllATA; }
            set { m_strInAllATA = value; }
        }

        /// <summary>
        /// 进港航班状态代码
        /// </summary>
        public string IncncAllStatus
        {
            get { return m_strInAllStatus; }
            set { m_strInAllStatus = value; }
        }

        /// <summary>
        /// 进港航班状态排序
        /// </summary>
        public string IncniAllViewIndex
        {
            get { return m_strInAllViewIndex; }
            set { m_strInAllViewIndex = value; }
        }

        /// <summary>
        /// 进港飞机号
        /// </summary>
        public string IncnvcLONG_REG
        {
            get { return m_strInLONG_REG; }
            set { m_strInLONG_REG = value; }
        }

        /// <summary>
        /// 进港日期
        /// </summary>
        public string IncncFlightDate
        {
            get { return m_strInFlightDate; }
            set { m_strInFlightDate = value; }
        }

        /// <summary>
        /// 进港航班号
        /// </summary>
        public string IncnvcFlightNo
        {
            get { return m_strInFlightNo; }
            set { m_strInFlightNo = value; }
        }

        /// <summary>
        /// 机型
        /// </summary>
        public string IncncACTYP
        {
            get { return m_strInACTYP; }
            set { m_strInACTYP = value; }
        }

        /// <summary>
        /// 进港性质
        /// </summary>
        public string IncnvcFlightCharacterAbbreviate
        {
            get { return m_strInFlightCharacterAbbreviate; }
            set { m_strInFlightCharacterAbbreviate = value; }
        }

        /// <summary>
        /// 进港起飞机场
        /// </summary>
        public string IncncDEPAirportCNAME
        {
            get { return m_strInDEPAirportCNAME; }
            set { m_strInDEPAirportCNAME = value; }
        }

        /// <summary>
        /// 进港到达机场
        /// </summary>
        public string IncncARRAirportCNAME
        {
            get { return m_strInARRAirportCNAME; }
            set { m_strInARRAirportCNAME = value; }
        }

        /// <summary>
        /// 进港计划到达时间（短格式）
        /// </summary>
        public string IncncSTA
        {
            get { return m_strInSTA; }
            set { m_strInSTA = value; }
        }

        /// <summary>
        /// 进港预计到达时间（短格式）
        /// </summary>
        public string IncncETA
        {
            get { return m_strInETA; }
            set { m_strInETA = value; }
        }

        /// <summary>
        /// 进港落地时间（短格式）
        /// </summary>
        public string IncncTDWN
        {
            get { return m_strInTDWN; }
            set { m_strInTDWN = value; }
        }

        /// <summary>
        /// 进港到位时间（短格式）
        /// </summary>
        public string IncncATA
        {
            get { return m_strInATA; }
            set { m_strInATA = value; }
        }

        /// <summary>
        /// ACARS起飞时间
        /// </summary>
        public string IncncACARSTOFF
        {
            get { return m_strInACARSTOFF; }
            set { m_strInACARSTOFF = value; }
        }

        /// <summary>
        /// ACARS到达时间
        /// </summary>
        public string IncncACARSTDWN
        {
            get { return m_strInACARSTDWN; }
            set { m_strInACARSTDWN = value; }
        }

        /// <summary>
        /// ACARS挡抡挡时间
        /// </summary>
        public string IncncACARSATA
        {
            get { return m_strInACARSATA; }
            set { m_strInACARSATA = value; }
        }

        /// <summary>
        /// 进港延误原因
        /// </summary>
        public string IncnvcInDelayName
        {
            get { return m_strInInDelayName; }
            set { m_strInInDelayName = value; }
        }

        /// <summary>
        /// 进港航班延误原因
        /// </summary>
        public string IncnvcFlightDelayName
        {
            get { return m_strInFlightDelayName; }
            set { m_strInFlightDelayName = value; }
        }

        /// <summary>
        /// 进港备降原因
        /// </summary>
        public string IncnvcDiversionDelayName
        {
            get { return m_strInDiversionDelayName; }
            set { m_strInDiversionDelayName = value; }
        }

        /// <summary>
        /// 进港保障车辆|信息
        /// </summary>
        public string IncnvcInCarInfor
        {
            get { return m_strInInCarInfor; }
            set { m_strInInCarInfor = value; }
        }

        /// <summary>
        /// 进港保障车辆|出发
        /// </summary>
        public string IncncInCarDepTime
        {
            get { return m_strInInCarDepTime; }
            set { m_strInInCarDepTime = value; }
        }

        /// <summary>
        /// 进港保障车辆|到达
        /// </summary>
        public string IncncInCarArrTime
        {
            get { return m_strInInCarArrTime; }
            set { m_strInInCarArrTime = value; }
        }

        private string m_strIncncInPowerCarStartTime;
        /// <summary>
        /// 进港航班APU|接电源车
        /// </summary>
        public string IncncInPowerCarStartTime
        {
            get { return m_strIncncInPowerCarStartTime; }
            set { m_strIncncInPowerCarStartTime = value; }
        }

        private string m_strIncncAPUDown;
        /// <summary>
        /// 进港航班APU|APU关
        /// </summary>
        public string IncncAPUDown
        {
            get { return m_strIncncAPUDown; }
            set { m_strIncncAPUDown = value; }
        }

        private string m_strIncncInPowerCarEndTime;
        /// <summary>
        /// 进港航班APU|撤电源车
        /// </summary>
        public string IncncInPowerCarEndTime
        {
            get { return m_strIncncInPowerCarEndTime; }
            set { m_strIncncInPowerCarEndTime = value; }
        }

        private string m_IncncInPowerCarNo;
        /// <summary>
        /// 进港航班APU|电源车号
        /// </summary>
        public string IncncInPowerCarNo
        {
            get { return m_IncncInPowerCarNo; }
            set { m_IncncInPowerCarNo = value; }
        }

        /// <summary>
        /// 进港VIP
        /// </summary>
        public string IncnbVIPTag
        {
            get { return m_strInVIPTag; }
            set { m_strInVIPTag = value; }
        }

        /// <summary>
        /// 进港旅客|值机人数
        /// </summary>
        public string IncniCheckNum
        {
            get { return m_strInCheckNum; }
            set { m_strInCheckNum = value; }
        }

        /// <summary>
        /// 进港旅客|中转旅客信息
        /// </summary>
        public string IncnbTransitPaxTag
        {
            get { return m_strInTransitPaxTag; }
            set { m_strInTransitPaxTag = value; }
        }

        /// <summary>
        /// 进港旅客|行李重量
        /// </summary>
        public string IncniBaggageWeight
        {
            get { return m_strInBaggageWeight; }
            set { m_strInBaggageWeight = value; }
        }

        /// <summary>
        /// 货物重量
        /// </summary>
        public string IncniCargoWeight
        {
            get { return m_strInCargoWeight; }
            set { m_strInCargoWeight = value; }
        }

        /// <summary>
        /// 进港状态
        /// </summary>
        public string IncncStatusName
        {
            get { return m_strInStatusName; }
            set { m_strInStatusName = value; }
        }

        /// <summary>
        /// 进港备注
        /// </summary>
        public string IncnvcInRemark
        {
            get { return m_strInInRemark; }
            set { m_strInInRemark = value; }
        }

       

        /// <summary>
        /// 进港飞机状态
        /// </summary>
        public string IncnvcInAircraftStatus
        {
            get { return m_strInInAircraftStatus; }
            set { m_strInInAircraftStatus = value; }
        }

        /// <summary>
        /// 进港机位
        /// </summary>
        public string IncnvcInGATE
        {
            get { return m_strInInGATE; }
            set { m_strInInGATE = value; }
        }

        /// <summary>
        /// 出港主键
        /// </summary>
        public string OutcncDATOP
        {
            get { return m_strOutDATOP; }
            set { m_strOutDATOP = value; }
        }

        /// <summary>
        /// 出港主键
        /// </summary>
        public string OutcnvcFLTID
        {
            get { return m_strOutFLTID; }
            set { m_strOutFLTID = value; }
        }


        /// <summary>
        /// 出港主键
        /// </summary>
        public string OutcniLEGNO
        {
            get { return m_strOutLEGNO; }
            set { m_strOutLEGNO = value; }
        }

        /// <summary>
        /// 出港主键
        /// </summary>
        public string OutcnvcAC
        {
            get { return m_strOutAC; }
            set { m_strOutAC = value; }
        }

        /// <summary>
        /// 出港计划起飞时间（长格式）
        /// </summary>
        public string OutcncAllSTD
        {
            get { return m_strOutAllSTD; }
            set { m_strOutAllSTD = value; }
        }

        /// <summary>
        /// 出港预计起飞时间（长格式）
        /// </summary>
        public string OutcncAllETD
        {
            get { return m_strOutAllETD; }
            set { m_strOutAllETD = value; }
        }

        /// <summary>
        /// 出港推出时间（长格式）
        /// </summary>
        public string OutcncAllATD
        {
            get { return m_strOutAllATD; }
            set { m_strOutAllATD = value; }
        }

        /// <summary>
        /// 出港起飞时间（长格式）
        /// </summary>
        public string OutcncAllTOFF
        {
            get { return m_strOutAllTOFF; }
            set { m_strOutAllTOFF = value; }
        }

        /// <summary>
        /// 出港航班状态代码
        /// </summary>
        public string OutcncAllStatus
        {
            get { return m_strOutAllStatus; }
            set { m_strOutAllStatus = value; }
        }

        /// <summary>
        /// 出港港航班状态排序
        /// </summary>
        public string OutcniAllViewIndex
        {
            get { return m_strOutAllViewIndex; }
            set { m_strOutAllViewIndex = value; }
        }

        /// <summary>
        /// 出港机位
        /// </summary>
        public string OutcnvcOutGate
        {
            get { return m_strOutOutGate; }
            set { m_strOutOutGate = value; }
        }

        /// <summary>
        /// 出港机号
        /// </summary>
        public string OutcnvcLONG_REG
        {
            get { return m_strOutLONG_REG; }
            set { m_strOutLONG_REG = value; }
        }

        /// <summary>
        /// 出港日期
        /// </summary>
        public string OutcncFlightDate
        {
            get { return m_strOutFlightDate; }
            set { m_strOutFlightDate = value; }
        }

        /// <summary>
        /// 出港航班
        /// </summary>
        public string OutcnvcFlightNo
        {
            get { return m_strOutFlightNo; }
            set { m_strOutFlightNo = value; }
        }

        /// <summary>
        /// 出港性质
        /// </summary>
        public string OutcnvcFlightCharacterAbbreviate
        {
            get { return m_strOutFlightCharacterAbbreviate; }
            set { m_strOutFlightCharacterAbbreviate = value; }
        }

        /// <summary>
        /// 出港机场|起飞
        /// </summary>
        public string OutcncDEPAirportCNAME
        {
            get { return m_strOutDEPAirportCNAME; }
            set { m_strOutDEPAirportCNAME = value; }
        }

        /// <summary>
        /// 出港机场|到达
        /// </summary>
        public string OutcncARRAirportCNAME
        {
            get { return m_strOutARRAirportCNAME; }
            set { m_strOutARRAirportCNAME = value; }
        }

        /// <summary>
        /// 航班时刻|计划
        /// </summary>
        public string OutcncSTD
        {
            get { return m_strOutSTD; }
            set { m_strOutSTD = value; }
        }

        /// <summary>
        /// 航班时刻|延误
        /// </summary>
        public string OutcncETD
        {
            get { return m_strOutETD; }
            set { m_strOutETD = value; }
        }

        /// <summary>
        /// 开始保障时间
        /// </summary>
        public string OutcncStartGuaranteeTime
        {
            get { return m_strOutStartGuaranteeTime; }
            set { m_strOutStartGuaranteeTime = value; }
        }

        private string m_strOutcncOutPowerCarStartTime;
        /// <summary>
        /// 出港航班APU|接电源车
        /// </summary>
        public string OutcncOutPowerCarStartTime
        {
            get { return m_strOutcncOutPowerCarStartTime; }
            set { m_strOutcncOutPowerCarStartTime = value; }
        }

        private string m_strOutcncAPUUp;
        /// <summary>
        /// 出港航班APU|APU开
        /// </summary>
        public string OutcncAPUUp
        {
            get { return m_strOutcncAPUUp; }
            set { m_strOutcncAPUUp = value; }
        }

        private string m_strOutcncOutPowerCarEndTime;
        /// <summary>
        /// 出港航班APU|撤电源车
        /// </summary>
        public string OutcncOutPowerCarEndTime
        {
            get { return m_strOutcncOutPowerCarEndTime; }
            set { m_strOutcncOutPowerCarEndTime = value; }
        }

        private string m_strOutcncOutPowerCarNo;
        /// <summary>
        /// 出港航班APU|电源车号
        /// </summary>
        public string OutcncOutPowerCarNo
        {
            get { return m_strOutcncOutPowerCarNo; }
            set { m_strOutcncOutPowerCarNo = value; }
        }
	

        /// <summary>
        /// 过站时间
        /// </summary>
        public string OutcniIntermissionTime
        {
            get { return m_strOutIntermissionTime; }
            set { m_strOutIntermissionTime = value; }
        }

        /// <summary>
        /// 连飞航班
        /// </summary>
        public string OutcnvcJoinFlight
        {
            get { return m_strOutJoinFlight; }
            set { m_strOutJoinFlight = value; }
        }

        /// <summary>
        /// 值机柜台
        /// </summary>
        public string OutcnvcCheckCounter
        {
            get { return m_strOutCheckCounter; }
            set { m_strOutCheckCounter = value; }
        }

        /// <summary>
        /// 候机厅
        /// </summary>
        public string OutcnvcWaitHall
        {
            get { return m_strOutWaitHall; }
            set { m_strOutWaitHall = value; }
        }

        /// <summary>
        /// 登机门
        /// </summary>
        public string OutcnvcBoradingGate
        {
            get { return m_strOutBoradingGate; }
            set { m_strOutBoradingGate = value; }
        }

        /// <summary>
        /// 客舱开启
        /// </summary>
        public string OutcncOpenCabinTime
        {
            get { return m_strOutOpenCabinTime; }
            set { m_strOutOpenCabinTime = value; }
        }

        /// <summary>
        /// 出港保障车辆|信息
        /// </summary>
        public string OutcnvcOutCarInfor
        {
            get { return m_strOutOutCarInfor; }
            set { m_strOutOutCarInfor = value; }
        }

        /// <summary>
        /// 出港保障车辆|出发
        /// </summary>
        public string OutcncOutCarDepTime
        {
            get { return m_strOutOutCarDepTime; }
            set { m_strOutOutCarDepTime = value; }
        }

        /// <summary>
        /// 出港保障车辆|到达
        /// </summary>
        public string OutcncOutCarArrTime
        {
            get { return m_strOutOutCarArrTime; }
            set { m_strOutOutCarArrTime = value; }
        }

        /// <summary>
        /// 机组到达|飞行
        /// </summary>
        public string OutcncPilotArrTime
        {
            get { return m_strOutPilotArrTime; }
            set { m_strOutPilotArrTime = value; }
        }

        /// <summary>
        /// 机组到达|乘务
        /// </summary>
        public string OutcncStewardArrTime
        {
            get { return m_strOutStewardArrTime; }
            set { m_strOutStewardArrTime = value; }
        }

        /// <summary>
        /// 飞机除冰|开始
        /// </summary>
        public string OutcncDeiceStartTime
        {
            get { return m_strOutDeiceStartTime; }
            set { m_strOutDeiceStartTime = value; }
        }

        /// <summary>
        /// 飞机除冰|结束
        /// </summary>
        public string OutcncDeiceEndTime
        {
            get { return m_strOutDeiceEndTime; }
            set { m_strOutDeiceEndTime = value; }
        }

        /// <summary>
        /// 任务书变更
        /// </summary>
        public string OutcncTaskSheetChangeTime
        {
            get { return m_strOutTaskSheetChangeTime; }
            set { m_strOutTaskSheetChangeTime = value; }
        }

        /// <summary>
        /// 签派放行
        /// </summary>
        public string OutcncDispatchTime
        {
            get { return m_strOutDispatchTime; }
            set { m_strOutDispatchTime = value; }
        }

        /// <summary>
        /// 放行资料打印
        /// </summary>
        public string OutcncDispatchPrintTime
        {
            get { return m_strOutDispatchPrintTime; }
            set { m_strOutDispatchPrintTime = value; }
        }

        /// <summary>
        /// 机务|技术员
        /// </summary>
        public string OutcncOutMCCTechEngr
        {
            get { return m_strOutOutMCCTechEngr; }
            set { m_strOutOutMCCTechEngr = value; }
        }

        /// <summary>
        /// 机务|放行人员
        /// </summary>
        public string OutcncOutMCCDispEngr
        {
            get { return m_strOutOutMCCDispEngr; }
            set { m_strOutOutMCCDispEngr = value; }
        }

        /// <summary>
        /// 机务|航前到位
        /// </summary>
        public string OutcncOutMCCReadyTime
        {
            get { return m_strOutOutMCCReadyTime; }
            set { m_strOutOutMCCReadyTime = value; }
        }


        /// <summary>
        /// 机务|到位
        /// </summary>
        public string IncncInMCCReadyTime
        {
            get { return m_strInInMCCReadyTime; }
            set { m_strInInMCCReadyTime = value; }
        }

        /// <summary>
        /// 机务|挡抡挡
        /// </summary>
        public string IncncInTime
        {
            get { return m_strInInTime; }
            set { m_strInInTime = value; }
        }

        /// <summary>
        /// 机务|放行
        /// </summary>
        public string OutcncMCCReleaseTime
        {
            get { return m_strOutMCCReleaseTime; }
            set { m_strOutMCCReleaseTime = value; }
        }

        /// <summary>
        /// 机务|撤抡挡
        /// </summary>
        public string OutcncOutTime
        {
            get { return m_strOutOutTime; }
            set { m_strOutOutTime = value; }
        }

        /// <summary>
        /// 飞机状态
        /// </summary>
        public string OutcnvcAircraftStatus
        {
            get { return m_strOutAircraftStatus; }
            set { m_strOutAircraftStatus = value; }
        }

        /// <summary>
        /// 下客完毕
        /// </summary>
        public string OutcncDeplaneTime
        {
            get { return m_strOutDeplaneTime; }
            set { m_strOutDeplaneTime = value; }
        }

        /// <summary>
        /// 客舱清洁|开始
        /// </summary>
        public string OutcncCleanStartTime
        {
            get { return m_strOutCleanStartTime; }
            set { m_strOutCleanStartTime = value; }
        }

        /// <summary>
        /// 客舱清洁|结束
        /// </summary>
        public string OutcncCleanEndTime
        {
            get { return m_strOutCleanEndTime; }
            set { m_strOutCleanEndTime = value; }
        }


        /// <summary>
        /// 清污水|开始
        /// </summary>
        public string OutcncSewageStartTime
        {
            get { return m_strOutSewageStartTime; }
            set { m_strOutSewageStartTime = value; }
        }

        /// <summary>
        /// 清污水|结束
        /// </summary>
        public string OutcncSewageEndTime
        {
            get { return m_strOutSewageEndTime; }
            set { m_strOutSewageEndTime = value; }
        }

        /// <summary>
        /// 客舱供应|开始
        /// </summary>
        public string OutcncCabinSupplyStartTime
        {
            get { return m_strOutCabinSupplyStartTime; }
            set { m_strOutCabinSupplyStartTime = value; }
        }

        /// <summary>
        /// 客舱供应|结束
        /// </summary>
        public string OutcncCabinSupplyEndTime
        {
            get { return m_strOutCabinSupplyEndTime; }
            set { m_strOutCabinSupplyEndTime = value; }
        }

        /// <summary>
        /// 加油|开始
        /// </summary>
        public string OutcncOilStartTime
        {
            get { return m_strOutOilStartTime; }
            set { m_strOutOilStartTime = value; }
        }


        /// <summary>
        /// 加油|结束
        /// </summary>
        public string OutcncOilEndTime
        {
            get { return m_strOutOilEndTime; }
            set { m_strOutOilEndTime = value; }
        }

        /// <summary>
        /// 货舱开启
        /// </summary>
        public string OutcncOpenCargoCabinTime
        {
            get { return m_strOutOpenCargoCabinTime; }
            set { m_strOutOpenCargoCabinTime = value; }
        }

        /// <summary>
        /// 装货时间
        /// </summary>
        public string OutcncCargoStartTime
        {
            get { return m_strOutCargoStartTime; }
            set { m_strOutCargoStartTime = value; }
        }

        /// <summary>
        /// 装行李时间
        /// </summary>
        public string OutcncBaggageTime
        {
            get { return m_strOutBaggageTime; }
            set { m_strOutBaggageTime = value; }
        }

        /// <summary>
        /// 货舱关闭
        /// </summary>
        public string OutcncCloseCargoCabinTime
        {
            get { return m_strOutCloseCargoCabinTime; }
            set { m_strOutCloseCargoCabinTime = value; }
        }

        /// <summary>
        /// 拖车到达
        /// </summary>
        public string OutcncTrailerArrTime
        {
            get { return m_strOutTrailerArrTime; }
            set { m_strOutTrailerArrTime = value; }
        }

        /// <summary>
        /// 污水车到达
        /// </summary>
        public string OutcncDirtyWaterCarArrTime
        {
            get { return m_strOutDirtyWaterCarArrTime; }
            set { m_strOutDirtyWaterCarArrTime = value; }
        }

        /// <summary>
        /// 摆渡车发车|首班
        /// </summary>
        public string OutcncFerryDepTime
        {
            get { return m_strOutFerryDepTime; }
            set { m_strOutFerryDepTime = value; }
        }

        /// <summary>
        /// 摆渡车发车|末班
        /// </summary>
        public string OutcncLastFerryArrTime
        {
            get { return m_strOutLastFerryArrTime; }
            set { m_strOutLastFerryArrTime = value; }
        }

        /// <summary>
        /// 拖杆
        /// </summary>
        public string OutcncDragPoleArrTime
        {
            get { return m_strOutDragPoleArrTime; }
            set { m_strOutDragPoleArrTime = value; }
        }

        /// <summary>
        /// 客梯车
        /// </summary>
        public string OutcncLadderCarArrTime
        {
            get { return m_strOutLadderCarArrTime; }
            set { m_strOutLadderCarArrTime = value; }
        }

        /// <summary>
        /// 通知上客
        /// </summary>
        public string OutcncInformBoardTime
        {
            get { return m_strOutInformBoardTime; }
            set { m_strOutInformBoardTime = value; }
        }

        /// <summary>
        /// 登机
        /// </summary>
        public string OutcncBoardTime
        {
            get { return m_strOutBoardTime; }
            set { m_strOutBoardTime = value; }
        }

        /// <summary>
        /// 旅客信息|订座人数
        /// </summary>
        public string OutcnvcBookNum
        {
            get { return m_strOutBookNum; }
            set { m_strOutBookNum = value; }
        }

        /// <summary>
        /// 旅客信息|值机人数
        /// </summary>
        public string OutcniCheckNum
        {
            get { return m_strOutCheckNum; }
            set { m_strOutCheckNum = value; }
        }

        /// <summary>
        /// 旅客信息|中转连程旅客
        /// </summary>
        public string OutcnbTransitPaxTag
        {
            get { return m_strOutTransitPaxTag; }
            set { m_strOutTransitPaxTag = value; }
        }

        /// <summary>
        /// 旅客信息|加机组
        /// </summary>
        public string OutcniXCRNum
        {
            get { return m_strOutXCRNum; }
            set { m_strOutXCRNum = value; }
        }

        /// <summary>
        /// 旅客信息|成人
        /// </summary>
        public string OutcniAdultNum
        {
            get { return m_strOutAdultNum; }
            set { m_strOutAdultNum = value; }
        }

        /// <summary>
        /// 旅客信息|儿童
        /// </summary>
        public string OutcniChildNum
        {
            get { return m_strOutChildNum; }
            set { m_strOutChildNum = value; }
        }


        /// <summary>
        /// 旅客信息|婴儿
        /// </summary>
        public string OutcniInfantNum
        {
            get { return m_strOutInfantNum; }
            set { m_strOutInfantNum = value; }
        }

        /// <summary>
        /// 旅客信息|头等舱
        /// </summary>
        public string OutcniFirstClassNum
        {
            get { return m_strOutFirstClassNum; }
            set { m_strOutFirstClassNum = value; }
        }

        /// <summary>
        /// 旅客信息|公务舱
        /// </summary>
        public string OutcniOfficialClassNum
        {
            get { return m_strOutOfficialClassNum; }
            set { m_strOutOfficialClassNum = value; }
        }

        /// <summary>
        /// 旅客信息|经济舱
        /// </summary>
        public string OutcniTouristClassNum
        {
            get { return m_strOutTouristClassNum; }
            set { m_strOutTouristClassNum = value; }
        }


        /// <summary>
        /// 旅客信息|升舱人数
        /// </summary>
        public string OutcniAscendingPaxNum
        {
            get { return m_strOutAscendingPaxNum; }
            set { m_strOutAscendingPaxNum = value; }
        }

        /// <summary>
        /// 旅客信息|旅客名单
        /// </summary>
        public string OutcntPaxNameList
        {
            get { return m_strOutPaxNameList; }
            set { m_strOutPaxNameList = value; }
        }

        /// <summary>
        /// 上客完毕
        /// </summary>
        public string OutcncBoardOverTime
        {
            get { return m_strOutBoardOverTime; }
            set { m_strOutBoardOverTime = value; }
        }


        /// <summary>
        /// 舱单送达
        /// </summary>
        public string OutcncLoadSheetArrTime
        {
            get { return m_strOutLoadSheetArrTime; }
            set { m_strOutLoadSheetArrTime = value; }
        }

        /// <summary>
        /// 客舱关闭
        /// </summary>
        public string OutcncClosePaxCabinTime
        {
            get { return m_strOutClosePaxCabinTime; }
            set { m_strOutClosePaxCabinTime = value; }
        }

        /// <summary>
        /// 开舱时长
        /// </summary>
        public string OutcniOpenTime
        {
            get { return m_strOutOpenTime; }
            set { m_strOutOpenTime = value; }
        }

        /// <summary>
        /// 内部客齐
        /// </summary>
        public string OutcncInternalGuestTogether
        {
            get { return m_strOutInternalGuestTogether; }
            set { m_strOutInternalGuestTogether = value; }
        }

        /// <summary>
        /// 飞机推出
        /// </summary>
        public string OutcncPushTime
        {
            get { return m_strOutPushTime; }
            set { m_strOutPushTime = value; }
        }

        /// <summary>
        /// 推出时间
        /// </summary>
        public string OutcncATD
        {
            get { return m_strOutATD; }
            set { m_strOutATD = value; }
        }

        /// <summary>
        /// 起飞动态
        /// </summary>
        public string OutcncTOFF
        {
            get { return m_strOutTOFF; }
            set { m_strOutTOFF = value; }
        }

        /// <summary>
        /// ACARS|撤抡挡
        /// </summary>
        public string OutcncACARSOUT
        {
            get { return m_strOutACARSOUT; }
            set { m_strOutACARSOUT = value; }
        }

        /// <summary>
        /// ACARS|起飞
        /// </summary>
        public string OutcncACARSTOFF
        {
            get { return m_strOutACARSTOFF; }
            set { m_strOutACARSTOFF = value; }
        }

        /// <summary>
        /// 保障超时原因
        /// </summary>
        public string OutcnvcDisChargingDelName
        {
            get { return m_strOutDisChargingDelName; }
            set { m_strOutDisChargingDelName = value; }
        }

        /// <summary>
        /// 出港放行延误原因
        /// </summary>
        public string OutcnvcOutDelayName
        {
            get { return m_strOutOutDelayName; }
            set { m_strOutOutDelayName = value; }
        }

        /// <summary>
        /// 出港航班延误原因
        /// </summary>
        public string OutcnvcFlightDelayName
        {
            get { return m_strOutFlightDelayName; }
            set { m_strOutFlightDelayName = value; }
        }

        /// <summary>
        /// 出港备降原因
        /// </summary>
        public string OutcnvcDiversionDelayName
        {
            get { return m_strOutDiversionDelayName; }
            set { m_strOutDiversionDelayName = value; }
        }

        /// <summary>
        /// 货物重量
        /// </summary>
        public string OutcniCargoWeight
        {
            get { return m_strOutCargoWeight; }
            set { m_strOutCargoWeight = value; }
        }

        /// <summary>
        /// 邮件重量
        /// </summary>
        public string OutcniMailWeight
        {
            get { return m_strOutMailWeight; }
            set { m_strOutMailWeight = value; }
        }

        /// <summary>
        /// 行李重量
        /// </summary>
        public string OutcniBaggageWeight
        {
            get { return m_strOutBaggageWeight; }
            set { m_strOutBaggageWeight = value; }
        }

        /// <summary>
        /// 航材重量
        /// </summary>
        public string OutcniAirMaterialWeight
        {
            get { return m_strOutAirMaterialWeight; }
            set { m_strOutAirMaterialWeight = value; }
        }

        /// <summary>
        /// 航材备注
        /// </summary>
        public string OutcnvcAirMaterialRemark
        {
            get { return m_strOutAirMaterialRemark; }
            set { m_strOutAirMaterialRemark = value; }
        }

        /// <summary>
        /// 行李件数
        /// </summary>
        public string OutcniBaggageNum
        {
            get { return m_strOutBaggageNum; }
            set { m_strOutBaggageNum = value; }
        }

        /// <summary>
        /// 出港VIP
        /// </summary>
        public string OutcnbVIPTag
        {
            get { return m_strOutVIPTag; }
            set { m_strOutVIPTag = value; }
        }

        /// <summary>
        /// 总油量
        /// </summary>
        public string OutcniTotalFuelWeight
        {
            get { return m_strOutTotalFuelWeight; }
            set { m_strOutTotalFuelWeight = value; }
        }

        /// <summary>
        /// 航程耗油
        /// </summary>
        public string OutcniTripFuelWeight
        {
            get { return m_strOutTripFuelWeight; }
            set { m_strOutTripFuelWeight = value; }
        }

        /// <summary>
        /// 滑行油
        /// </summary>
        public string OutcniTaxiFuelWeight
        {
            get { return m_strOutTaxiFuelWeight; }
            set { m_strOutTaxiFuelWeight = value; }
        }

        /// <summary>
        /// 拉货重量
        /// </summary>
        public string OutcniUnloadCargoWeight
        {
            get { return m_strOutUnloadCargoWeight; }
            set { m_strOutUnloadCargoWeight = value; }
        }

        /// <summary>
        /// 货运备注
        /// </summary>
        public string OutcnvcCargoRemark
        {
            get { return m_strOutCargoRemark; }
            set { m_strOutCargoRemark = value; }
        }

        /// <summary>
        /// 出港状态
        /// </summary>
        public string OutcncStatusName
        {
            get { return m_strOutStatusName; }
            set { m_strOutStatusName = value; }
        }

        /// <summary>
        /// 出港备注
        /// </summary>
        public string OutcnvcOutRemark
        {
            get { return m_strOutOutRemark; }
            set { m_strOutOutRemark = value; }
        }

        /// <summary>
        /// 现场指挥人员|主控
        /// </summary>
        public string OutcnvcChiefController
        {
            get { return m_strOutChiefController; }
            set { m_strOutChiefController = value; }
        }

        /// <summary>
        /// 现场指挥人员|副控
        /// </summary>
        public string OutcnvcAssistantController
        {
            get { return m_strOutAssistantController; }
            set { m_strOutAssistantController = value; }
        }

        /// <summary>
        /// 现场指挥人员|外场
        /// </summary>
        public string OutcnvcOutFieldController
        {
            get { return m_strOutOutFieldController; }
            set { m_strOutOutFieldController = value; }
        }

        /// <summary>
        /// 配载员
        /// </summary>
        public string OutcnvcBalanceController
        {
            get { return m_strOutBalanceController; }
            set { m_strOutBalanceController = value; }
        }

        /// <summary>
        /// 放行延误时间
        /// </summary>
        public string OutcniDischargingDelayTime
        {
            get { return m_strOutDischargingDelayTime; }
            set { m_strOutDischargingDelayTime = value; }
        }

        /// <summary>
        /// 实际延误时间
        /// </summary>
        public string OutcniDUR1
        {
            get { return m_strOutcniDUR1; }
            set { m_strOutcniDUR1 = value; }
        }

        /// <summary>
        /// 重点保障航班
        /// </summary>
        public string OutcniFocusTag
        {
            get { return m_strOutFocusTag; }
            set { m_strOutFocusTag = value; }
        }

        /// <summary>
        /// 工作安排
        /// </summary>
        public string OutcnvcArrangedTask
        {
            get { return m_strOutArrangedTask; }
            set { m_strOutArrangedTask = value; }
        }

        #endregion 以往

        #region added in 20101113
        /// <summary>
        /// 机务|航后技术员
        /// </summary>
        public string IncncInMCCTechEngr
        {
            get { return m_strInInMCCTechEngr; }
            set { m_strInInMCCTechEngr = value; }
        }

        /// <summary>
        /// 机务|航后工作
        /// </summary>
        public string IncnvcInArrangedTask
        {
            get { return m_strInInArrangedTask; }
            set { m_strInInArrangedTask = value; }
        }

        /// <summary>
        /// 航站进港备注
        /// </summary>
        public string IncnvcInSMCRemark
        {
            get { return m_strInInSMCRemark; }
            set { m_strInInSMCRemark = value; }
        }

        /// <summary>
        /// 航站出港备注
        /// </summary>
        public string OutcnvcOutSMCRemark
        {
            get { return m_strOutOutSMCRemark; }
            set { m_strOutOutSMCRemark = value; }
        }

        #endregion added in 20101113

        #region added in 20111031
        /// <summary>
        /// 机务|航后放行
        /// </summary>
        public string IncncInMCCReleaseTime
        {
            get { return m_strInMCCReleaseTime; }
            set { m_strInMCCReleaseTime = value; }
        }

        /// <summary>
        /// 西部航空航班运行信息|运控
        /// </summary>
        public string OutcnvcOutWAOperationInfoYK
        {
            get { return m_strOutWAOperationInfoYK; }
            set { m_strOutWAOperationInfoYK = value; }
        }

        /// <summary>
        /// 西部航空航班运行信息|飞行
        /// </summary>
        public string OutcnvcOutWAOperationInfoFX
        {
            get { return m_strOutWAOperationInfoFX; }
            set { m_strOutWAOperationInfoFX = value; }
        }

        /// <summary>
        /// 西部航空航班运行信息|客服
        /// </summary>
        public string OutcnvcOutWAOperationInfoKF
        {
            get { return m_strOutWAOperationInfoKF; }
            set { m_strOutWAOperationInfoKF = value; }
        }

        /// <summary>
        /// 西部航空航班运行信息|工程
        /// </summary>
        public string OutcnvcOutWAOperationInfoGC
        {
            get { return m_strOutWAOperationInfoGC; }
            set { m_strOutWAOperationInfoGC = value; }
        }

        /// <summary>
        /// 西部航空航班运行信息|安监
        /// </summary>
        public string OutcnvcOutWAOperationInfoAJ
        {
            get { return m_strOutWAOperationInfoAJ; }
            set { m_strOutWAOperationInfoAJ = value; }
        }

        /// <summary>
        /// 西部航空航班运行信息|市场
        /// </summary>
        public string OutcnvcOutWAOperationInfoSC
        {
            get { return m_strOutWAOperationInfoSC; }
            set { m_strOutWAOperationInfoSC = value; }
        }
        #endregion added in 20111031

        #region added in 20121113
        /// <summary>
        /// CDM预计推出时间
        /// </summary>
        public string OutcncCDMeOutTime
        {
            get { return m_strOutcncCDMeOutTime; }
            set { m_strOutcncCDMeOutTime = value; }
        }

        /// <summary>
        /// CDM预计起飞时间
        /// </summary>
        public string OutcncCDMeOffTime
        {
            get { return m_strOutcncCDMeOffTime; }
            set { m_strOutcncCDMeOffTime = value; }
        }

        /// <summary>
        /// CDM排序机组车发车时间
        /// </summary>
        public string OutcncCDMvStartTime
        {
            get { return m_strOutcncCDMvStartTime; }
            set { m_strOutcncCDMvStartTime = value; }
        }
        #endregion added in 20121113

        #region 三期 added in 20140514
        /// <summary>
        /// 进港客梯车|开始
        /// </summary>
        public string IncncInLadderCarGuaranteeStartTime
        {
            set { m_strincncinladdercarguaranteestarttime = value; }
            get { return m_strincncinladdercarguaranteestarttime; }
        }
        /// <summary>
        /// 进港客梯车|结束
        /// </summary>
        public string IncncInLadderCarGuaranteeEndTime
        {
            set { m_strincncinladdercarguaranteeendtime = value; }
            get { return m_strincncinladdercarguaranteeendtime; }
        }
        /// <summary>
        /// 进港廊桥|开始
        /// </summary>
        public string IncncInBridgeGuaranteeStartTime
        {
            set { m_strincncinbridgeguaranteestarttime = value; }
            get { return m_strincncinbridgeguaranteestarttime; }
        }
        /// <summary>
        /// 进港廊桥|结束
        /// </summary>
        public string IncncInBridgeGuaranteeEndTime
        {
            set { m_strincncinbridgeguaranteeendtime = value; }
            get { return m_strincncinbridgeguaranteeendtime; }
        }
        /// <summary>
        /// 进港摆渡车|开始
        /// </summary>
        public string IncncInFerryrGuaranteeStartTime
        {
            set { m_strincncinferryrguaranteestarttime = value; }
            get { return m_strincncinferryrguaranteestarttime; }
        }
        /// <summary>
        /// 进港摆渡车|结束
        /// </summary>
        public string IncncInFerryGuaranteeEndTime
        {
            set { m_strincncinferryguaranteeendtime = value; }
            get { return m_strincncinferryguaranteeendtime; }
        }
        /// <summary>
        /// 进港摆渡车首班|到达飞机下
        /// </summary>
        public string IncncInFerryGuaranteeFirstArrivePlaneTime
        {
            set { m_strincncinferryguaranteefirstarriveplanetime = value; }
            get { return m_strincncinferryguaranteefirstarriveplanetime; }
        }
        /// <summary>
        /// 进港摆渡车末班|到达飞机下
        /// </summary>
        public string IncncInFerryGuaranteeLastArrivePlaneTime
        {
            set { m_strincncinferryguaranteelastarriveplanetime = value; }
            get { return m_strincncinferryguaranteelastarriveplanetime; }
        }
        /// <summary>
        /// 进港摆渡车|到位
        /// </summary>
        public string IncncInFerryrGuaranteeArriveTime
        {
            set { m_strincncinferryrguaranteearrivetime = value; }
            get { return m_strincncinferryrguaranteearrivetime; }
        }
        /// <summary>
        /// 进港机组车|开始
        /// </summary>
        public string IncncInCrewCarrGuaranteeStartTime
        {
            set { m_strincncincrewcarrguaranteestarttime = value; }
            get { return m_strincncincrewcarrguaranteestarttime; }
        }
        /// <summary>
        /// 进港机组车|结束
        /// </summary>
        public string IncncInCrewCarGuaranteeEndTime
        {
            set { m_strincncincrewcarguaranteeendtime = value; }
            get { return m_strincncincrewcarguaranteeendtime; }
        }
        /// <summary>
        /// 进港传送带车|开始
        /// </summary>
        public string IncncInConveyerBeltVehicleGuaranteeStartTime
        {
            set { m_strincncinconveyerbeltvehicleguaranteestarttime = value; }
            get { return m_strincncinconveyerbeltvehicleguaranteestarttime; }
        }
        /// <summary>
        /// 进港传送带车|结束
        /// </summary>
        public string IncncInConveyerBeltVehicleGuaranteeEndTime
        {
            set { m_strincncinconveyerbeltvehicleguaranteeendtime = value; }
            get { return m_strincncinconveyerbeltvehicleguaranteeendtime; }
        }
        /// <summary>
        /// 进港平台车|开始
        /// </summary>
        public string IncncInPlatformCarGuaranteeStartTime
        {
            set { m_strincncinplatformcarguaranteestarttime = value; }
            get { return m_strincncinplatformcarguaranteestarttime; }
        }
        /// <summary>
        /// 进港平台车|结束
        /// </summary>
        public string IncncInPlatformCarGuaranteeEndTime
        {
            set { m_strincncinplatformcarguaranteeendtime = value; }
            get { return m_strincncinplatformcarguaranteeendtime; }
        }
        /// <summary>
        /// 进港拖车|开始
        /// </summary>
        public string IncncInTrailCarGuaranteeStartTime
        {
            set { m_strincncintrailcarguaranteestarttime = value; }
            get { return m_strincncintrailcarguaranteestarttime; }
        }
        /// <summary>
        /// 进港拖车|结束
        /// </summary>
        public string IncncInTrailCarGuaranteeEndTime
        {
            set { m_strincncintrailcarguaranteeendtime = value; }
            get { return m_strincncintrailcarguaranteeendtime; }
        }
        /// <summary>
        /// 进港拖车|到位
        /// </summary>
        public string IncncInTrailCarGuaranteeArriveTime
        {
            set { m_strincncintrailcarguaranteearrivetime = value; }
            get { return m_strincncintrailcarguaranteearrivetime; }
        }
        /// <summary>
        /// 进港拖车|抱轮
        /// </summary>
        public string IncncInTrailCarGuaranteeHoldTheWheelTime
        {
            set { m_strincncintrailcarguaranteeholdthewheeltime = value; }
            get { return m_strincncintrailcarguaranteeholdthewheeltime; }
        }
        /// <summary>
        /// 进港电源车|开始
        /// </summary>
        public string IncncInPowerCarGuaranteeStartTime
        {
            set { m_strincncinpowercarguaranteestarttime = value; }
            get { return m_strincncinpowercarguaranteestarttime; }
        }
        /// <summary>
        /// 进港电源车|结束
        /// </summary>
        public string IncncInPowerCarGuaranteeEndTime
        {
            set { m_strincncinpowercarguaranteeendtime = value; }
            get { return m_strincncinpowercarguaranteeendtime; }
        }
        /// <summary>
        /// 进港气源车|开始
        /// </summary>
        public string IncncInAirSupplyCarGuaranteeStartTime
        {
            set { m_strincncinairsupplycarguaranteestarttime = value; }
            get { return m_strincncinairsupplycarguaranteestarttime; }
        }
        /// <summary>
        /// 进港气源车|结束
        /// </summary>
        public string IncncInAirSupplyCarGuaranteeEndTime
        {
            set { m_strincncinairsupplycarguaranteeendtime = value; }
            get { return m_strincncinairsupplycarguaranteeendtime; }
        }
        /// <summary>
        /// 进港空调车|开始
        /// </summary>
        public string IncncInAirConditionerCarGuaranteeStartTime
        {
            set { m_strincncinairconditionercarguaranteestarttime = value; }
            get { return m_strincncinairconditionercarguaranteestarttime; }
        }
        /// <summary>
        /// 进港空调车|结束
        /// </summary>
        public string IncncInAirConditionerCarGuaranteeEndTime
        {
            set { m_strincncinairconditionercarguaranteeendtime = value; }
            get { return m_strincncinairconditionercarguaranteeendtime; }
        }
        /// <summary>
        /// 进港清水车|开始
        /// </summary>
        public string IncncInCleanWaterCarGuaranteeStartTime
        {
            set { m_strincncincleanwatercarguaranteestarttime = value; }
            get { return m_strincncincleanwatercarguaranteestarttime; }
        }
        /// <summary>
        /// 进港清水车|结束
        /// </summary>
        public string IncncInCleanWaterCarGuaranteeEndTime
        {
            set { m_strincncincleanwatercarguaranteeendtime = value; }
            get { return m_strincncincleanwatercarguaranteeendtime; }
        }
        /// <summary>
        /// 进港污水车|开始
        /// </summary>
        public string IncncInSewageCarGuaranteeStartTime
        {
            set { m_strincncinsewagecarguaranteestarttime = value; }
            get { return m_strincncinsewagecarguaranteestarttime; }
        }
        /// <summary>
        /// 进港污水车|结束
        /// </summary>
        public string IncncInSewageCarGuaranteeEndTime
        {
            set { m_strincncinsewagecarguaranteeendtime = value; }
            get { return m_strincncinsewagecarguaranteeendtime; }
        }
        /// <summary>
        /// 出港客梯车|开始
        /// </summary>
        public string OutcncOutLadderCarrGuaranteeStartTime
        {
            set { m_stroutcncoutladdercarrguaranteestarttime = value; }
            get { return m_stroutcncoutladdercarrguaranteestarttime; }
        }
        /// <summary>
        /// 出港客梯车|结束
        /// </summary>
        public string OutcncOutLadderCarGuaranteeEndTime
        {
            set { m_stroutcncoutladdercarguaranteeendtime = value; }
            get { return m_stroutcncoutladdercarguaranteeendtime; }
        }
        /// <summary>
        /// 出港廊桥|开始
        /// </summary>
        public string OutcncOutBridgerGuaranteeStartTime
        {
            set { m_stroutcncoutbridgerguaranteestarttime = value; }
            get { return m_stroutcncoutbridgerguaranteestarttime; }
        }
        /// <summary>
        /// 出港廊桥|结束
        /// </summary>
        public string OutcncOutBridgeGuaranteeEndTime
        {
            set { m_stroutcncoutbridgeguaranteeendtime = value; }
            get { return m_stroutcncoutbridgeguaranteeendtime; }
        }
        /// <summary>
        /// 出港摆渡车|开始
        /// </summary>
        public string OutcncOutFerryrGuaranteeStartTime
        {
            set { m_stroutcncoutferryrguaranteestarttime = value; }
            get { return m_stroutcncoutferryrguaranteestarttime; }
        }
        /// <summary>
        /// 出港摆渡车|结束
        /// </summary>
        public string OutcncOutFerryGuaranteeEndTime
        {
            set { m_stroutcncoutferryguaranteeendtime = value; }
            get { return m_stroutcncoutferryguaranteeendtime; }
        }
        /// <summary>
        /// 进港摆渡车首班|出发
        /// </summary>
        public string OutcncOutFerryGuaranteeFirstOutTime
        {
            set { m_stroutcncoutferryguaranteefirstouttime = value; }
            get { return m_stroutcncoutferryguaranteefirstouttime; }
        }
        /// <summary>
        /// 进港摆渡车首班|到达登机口
        /// </summary>
        public string OutcncOutFerryGuaranteeFirstArriveGateTime
        {
            set { m_stroutcncoutferryguaranteefirstarrivegatetime = value; }
            get { return m_stroutcncoutferryguaranteefirstarrivegatetime; }
        }
        /// <summary>
        /// 进港摆渡车首班|到达飞机下
        /// </summary>
        public string OutcncOutFerryGuaranteeFirstArrivePlaneTime
        {
            set { m_stroutcncoutferryguaranteefirstarriveplanetime = value; }
            get { return m_stroutcncoutferryguaranteefirstarriveplanetime; }
        }
        /// <summary>
        /// 进港摆渡车末班|出发
        /// </summary>
        public string OutcncOutFerryGuaranteeLastOutTime
        {
            set { m_stroutcncoutferryguaranteelastouttime = value; }
            get { return m_stroutcncoutferryguaranteelastouttime; }
        }
        /// <summary>
        /// 进港摆渡车末班|到达登机口
        /// </summary>
        public string OutcncOutFerryGuaranteeLastArriveGateTime
        {
            set { m_stroutcncoutferryguaranteelastarrivegatetime = value; }
            get { return m_stroutcncoutferryguaranteelastarrivegatetime; }
        }
        /// <summary>
        /// 进港摆渡车末班|到达飞机下
        /// </summary>
        public string OutcncOutFerryGuaranteeLastArrivePlaneTime
        {
            set { m_stroutcncoutferryguaranteelastarriveplanetime = value; }
            get { return m_stroutcncoutferryguaranteelastarriveplanetime; }
        }
        /// <summary>
        /// 出港摆渡车|到位
        /// </summary>
        public string OutcncOutFerryrGuaranteeArriveTime
        {
            set { m_stroutcncoutferryrguaranteearrivetime = value; }
            get { return m_stroutcncoutferryrguaranteearrivetime; }
        }
        /// <summary>
        /// 出港机组车|开始
        /// </summary>
        public string OutcncOutCrewCarrGuaranteeStartTime
        {
            set { m_stroutcncoutcrewcarrguaranteestarttime = value; }
            get { return m_stroutcncoutcrewcarrguaranteestarttime; }
        }
        /// <summary>
        /// 出港机组车|结束
        /// </summary>
        public string OutcncOutCrewCarGuaranteeEndTime
        {
            set { m_stroutcncoutcrewcarguaranteeendtime = value; }
            get { return m_stroutcncoutcrewcarguaranteeendtime; }
        }
        /// <summary>
        /// 出港传送带车|开始
        /// </summary>
        public string OutcncOutConveyerBeltVehicleGuaranteeStartTime
        {
            set { m_stroutcncoutconveyerbeltvehicleguaranteestarttime = value; }
            get { return m_stroutcncoutconveyerbeltvehicleguaranteestarttime; }
        }
        /// <summary>
        /// 出港传送带车|结束
        /// </summary>
        public string OutcncOutConveyerBeltVehicleGuaranteeEndTime
        {
            set { m_stroutcncoutconveyerbeltvehicleguaranteeendtime = value; }
            get { return m_stroutcncoutconveyerbeltvehicleguaranteeendtime; }
        }
        /// <summary>
        /// 出港平台车|开始
        /// </summary>
        public string OutcncOutPlatformCarGuaranteeStartTime
        {
            set { m_stroutcncoutplatformcarguaranteestarttime = value; }
            get { return m_stroutcncoutplatformcarguaranteestarttime; }
        }
        /// <summary>
        /// 出港平台车|结束
        /// </summary>
        public string OutcncOutPlatformCarGuaranteeEndTime
        {
            set { m_stroutcncoutplatformcarguaranteeendtime = value; }
            get { return m_stroutcncoutplatformcarguaranteeendtime; }
        }
        /// <summary>
        /// 出港拖车|开始
        /// </summary>
        public string OutcncOutTrailCarGuaranteeStartTime
        {
            set { m_stroutcncouttrailcarguaranteestarttime = value; }
            get { return m_stroutcncouttrailcarguaranteestarttime; }
        }
        /// <summary>
        /// 出港拖车|结束
        /// </summary>
        public string OutcncOutTrailCarGuaranteeEndTime
        {
            set { m_stroutcncouttrailcarguaranteeendtime = value; }
            get { return m_stroutcncouttrailcarguaranteeendtime; }
        }
        /// <summary>
        /// 出港拖车|到位
        /// </summary>
        public string OutcncOutTrailCarGuaranteeArriveTime
        {
            set { m_stroutcncouttrailcarguaranteearrivetime = value; }
            get { return m_stroutcncouttrailcarguaranteearrivetime; }
        }
        /// <summary>
        /// 出港拖车|抱轮
        /// </summary>
        public string OutcncOutTrailCarGuaranteeHoldTheWheelTime
        {
            set { m_stroutcncouttrailcarguaranteeholdthewheeltime = value; }
            get { return m_stroutcncouttrailcarguaranteeholdthewheeltime; }
        }
        /// <summary>
        /// 出港电源车|开始
        /// </summary>
        public string OutcncOutPowerCarGuaranteeStartTime
        {
            set { m_stroutcncoutpowercarguaranteestarttime = value; }
            get { return m_stroutcncoutpowercarguaranteestarttime; }
        }
        /// <summary>
        /// 出港电源车|结束
        /// </summary>
        public string OutcncOutPowerCarGuaranteeEndTime
        {
            set { m_stroutcncoutpowercarguaranteeendtime = value; }
            get { return m_stroutcncoutpowercarguaranteeendtime; }
        }
        /// <summary>
        /// 出港气源车|开始
        /// </summary>
        public string OutcncOutAirSupplyCarGuaranteeStartTime
        {
            set { m_stroutcncoutairsupplycarguaranteestarttime = value; }
            get { return m_stroutcncoutairsupplycarguaranteestarttime; }
        }
        /// <summary>
        /// 出港气源车|结束
        /// </summary>
        public string OutcncOutAirSupplyCarGuaranteeEndTime
        {
            set { m_stroutcncoutairsupplycarguaranteeendtime = value; }
            get { return m_stroutcncoutairsupplycarguaranteeendtime; }
        }
        /// <summary>
        /// 出港空调车|开始
        /// </summary>
        public string OutcncOutAirConditionerCarGuaranteeStartTime
        {
            set { m_stroutcncoutairconditionercarguaranteestarttime = value; }
            get { return m_stroutcncoutairconditionercarguaranteestarttime; }
        }
        /// <summary>
        /// 出港空调车|结束
        /// </summary>
        public string OutcncOutAirConditionerCarGuaranteeEndTime
        {
            set { m_stroutcncoutairconditionercarguaranteeendtime = value; }
            get { return m_stroutcncoutairconditionercarguaranteeendtime; }
        }
        /// <summary>
        /// 出港清水车|开始
        /// </summary>
        public string OutcncOutCleanWaterCarGuaranteeStartTime
        {
            set { m_stroutcncoutcleanwatercarguaranteestarttime = value; }
            get { return m_stroutcncoutcleanwatercarguaranteestarttime; }
        }
        /// <summary>
        /// 出港清水车|结束
        /// </summary>
        public string OutcncOutCleanWaterCarGuaranteeEndTime
        {
            set { m_stroutcncoutcleanwatercarguaranteeendtime = value; }
            get { return m_stroutcncoutcleanwatercarguaranteeendtime; }
        }
        /// <summary>
        /// 出港污水车|开始
        /// </summary>
        public string OutcncOutSewageCarGuaranteeStartTime
        {
            set { m_stroutcncoutsewagecarguaranteestarttime = value; }
            get { return m_stroutcncoutsewagecarguaranteestarttime; }
        }
        /// <summary>
        /// 出港污水车|结束
        /// </summary>
        public string OutcncOutSewageCarGuaranteeEndTime
        {
            set { m_stroutcncoutsewagecarguaranteeendtime = value; }
            get { return m_stroutcncoutsewagecarguaranteeendtime; }
        }
        /// <summary>
        /// 出港航油加注|开始
        /// </summary>
        public string OutcncOutFuelFillingStartTime
        {
            set { m_stroutcncoutfuelfillingstarttime = value; }
            get { return m_stroutcncoutfuelfillingstarttime; }
        }
        /// <summary>
        /// 出港航油加注|结束
        /// </summary>
        public string OutcncOutFuelFillingEndTime
        {
            set { m_stroutcncoutfuelfillingendtime = value; }
            get { return m_stroutcncoutfuelfillingendtime; }
        }
        /// <summary>
        /// 出港客舱供应|开始
        /// </summary>
        public string OutcncOutCabinSupplyStartTime
        {
            set { m_stroutcncoutcabinsupplystarttime = value; }
            get { return m_stroutcncoutcabinsupplystarttime; }
        }
        /// <summary>
        /// 出港客舱供应|结束
        /// </summary>
        public string OutcncOutCabinSupplyEndTime
        {
            set { m_stroutcncoutcabinsupplyendtime = value; }
            get { return m_stroutcncoutcabinsupplyendtime; }
        }
        /// <summary>
        /// 进港货邮行李|开始
        /// </summary>
        public string IncncInCargoMailBaggageGuaranteeStartTime
        {
            set { m_strincncincargomailbaggageguaranteestarttime = value; }
            get { return m_strincncincargomailbaggageguaranteestarttime; }
        }
        /// <summary>
        /// 进港货邮行李|结束
        /// </summary>
        public string IncncInCargoMailBaggageGuaranteeEndTime
        {
            set { m_strincncincargomailbaggageguaranteeendtime = value; }
            get { return m_strincncincargomailbaggageguaranteeendtime; }
        }
        /// <summary>
        /// 进港货舱门|开启
        /// </summary>
        public string IncncInCargoCabinOpenTime
        {
            set { m_strincncincargocabinopentime = value; }
            get { return m_strincncincargocabinopentime; }
        }
        /// <summary>
        /// 进港货舱门|关闭
        /// </summary>
        public string IncncInCargoCabinCloseTime
        {
            set { m_strincncincargocabinclosetime = value; }
            get { return m_strincncincargocabinclosetime; }
        }
        /// <summary>
        /// 进港卸载行李|开始
        /// </summary>
        public string IncncInBaggageUnloadStartTime
        {
            set { m_strincncinbaggageunloadstarttime = value; }
            get { return m_strincncinbaggageunloadstarttime; }
        }
        /// <summary>
        /// 进港卸载行李|结束
        /// </summary>
        public string IncncInBaggageUnloadEndTime
        {
            set { m_strincncinbaggageunloadendtime = value; }
            get { return m_strincncinbaggageunloadendtime; }
        }
        /// <summary>
        /// 进港卸载货物|开始
        /// </summary>
        public string IncncInCargoUnloadStartTime
        {
            set { m_strincncincargounloadstarttime = value; }
            get { return m_strincncincargounloadstarttime; }
        }
        /// <summary>
        /// 进港卸载货物|结束
        /// </summary>
        public string IncncInCargoUnloadEndTime
        {
            set { m_strincncincargounloadendtime = value; }
            get { return m_strincncincargounloadendtime; }
        }
        /// <summary>
        /// 出港货邮行李|开始
        /// </summary>
        public string OutcncOutCargoMailBaggageGuaranteeStartTime
        {
            set { m_stroutcncoutcargomailbaggageguaranteestarttime = value; }
            get { return m_stroutcncoutcargomailbaggageguaranteestarttime; }
        }
        /// <summary>
        /// 出港货邮行李|结束
        /// </summary>
        public string OutcncOutCargoMailBaggageGuaranteeEndTime
        {
            set { m_stroutcncoutcargomailbaggageguaranteeendtime = value; }
            get { return m_stroutcncoutcargomailbaggageguaranteeendtime; }
        }
        /// <summary>
        /// 出港货邮行李|报载
        /// </summary>
        public string OutcncOutCargoMailBaggageReportTime
        {
            set { m_stroutcncoutcargomailbaggagereporttime = value; }
            get { return m_stroutcncoutcargomailbaggagereporttime; }
        }
        /// <summary>
        /// 出港行李|通知挑拣
        /// </summary>
        public string OutcncOutInfoPickUpBaggageTime
        {
            set { m_stroutcncoutinfopickupbaggagetime = value; }
            get { return m_stroutcncoutinfopickupbaggagetime; }
        }
        /// <summary>
        /// 出港货舱门|开启
        /// </summary>
        public string OutcncOutCargoCabinOpenTime
        {
            set { m_stroutcncoutcargocabinopentime = value; }
            get { return m_stroutcncoutcargocabinopentime; }
        }
        /// <summary>
        /// 出港货舱门|关闭
        /// </summary>
        public string OutcncOutCargoCabinCloseTime
        {
            set { m_stroutcncoutcargocabinclosetime = value; }
            get { return m_stroutcncoutcargocabinclosetime; }
        }
        /// <summary>
        /// 出港装载行李|开始
        /// </summary>
        public string OutcncOutBaggageLoadStartTime
        {
            set { m_stroutcncoutbaggageloadstarttime = value; }
            get { return m_stroutcncoutbaggageloadstarttime; }
        }
        /// <summary>
        /// 出港装载行李|结束
        /// </summary>
        public string OutcncOutBaggageLoadEndTime
        {
            set { m_stroutcncoutbaggageloadendtime = value; }
            get { return m_stroutcncoutbaggageloadendtime; }
        }
        /// <summary>
        /// 出港装载货物|开始
        /// </summary>
        public string OutcncOutCargoLoadStartTime
        {
            set { m_stroutcncoutcargoloadstarttime = value; }
            get { return m_stroutcncoutcargoloadstarttime; }
        }
        /// <summary>
        /// 出港装载货物|结束
        /// </summary>
        public string OutcncOutCargoLoadEndTime
        {
            set { m_stroutcncoutcargoloadendtime = value; }
            get { return m_stroutcncoutcargoloadendtime; }
        }
        /// <summary>
        /// 出港飞行员|刹车
        /// </summary>
        public string IncncInPilotBrakeTime
        {
            set { m_strincncinpilotbraketime = value; }
            get { return m_strincncinpilotbraketime; }
        }
        /// <summary>
        /// 出港飞行员|到位
        /// </summary>
        public string OutcncOutPilotArriveTime
        {
            set { m_stroutcncoutpilotarrivetime = value; }
            get { return m_stroutcncoutpilotarrivetime; }
        }
        /// <summary>
        /// 出港飞行员|松刹车
        /// </summary>
        public string OutcncOutPilotLooseSkidsTime
        {
            set { m_stroutcncoutpilotlooseskidstime = value; }
            get { return m_stroutcncoutpilotlooseskidstime; }
        }
        /// <summary>
        /// 出港飞行员|推出
        /// </summary>
        public string OutcncOutPilotPushOffTime
        {
            set { m_stroutcncoutpilotpushofftime = value; }
            get { return m_stroutcncoutpilotpushofftime; }
        }
        /// <summary>
        /// 进港客舱门|开启
        /// </summary>
        public string IncncInCabinOpenTime
        {
            set { m_strincncincabinopentime = value; }
            get { return m_strincncincabinopentime; }
        }
        /// <summary>
        /// 进港客舱门|关闭
        /// </summary>
        public string IncncInCabinCloseTime
        {
            set { m_strincncincabinclosetime = value; }
            get { return m_strincncincabinclosetime; }
        }
        /// <summary>
        /// 出港客舱门|开启
        /// </summary>
        public string OutcncOutCabinOpenTime
        {
            set { m_stroutcncoutcabinopentime = value; }
            get { return m_stroutcncoutcabinopentime; }
        }
        /// <summary>
        /// 出港客舱门|关闭
        /// </summary>
        public string OutcncOutCabinCloseTime
        {
            set { m_stroutcncoutcabinclosetime = value; }
            get { return m_stroutcncoutcabinclosetime; }
        }
        /// <summary>
        /// 出港乘务员|到位
        /// </summary>
        public string OutcncOutStewardArriveTime
        {
            set { m_stroutcncoutstewardarrivetime = value; }
            get { return m_stroutcncoutstewardarrivetime; }
        }
        /// <summary>
        /// 出港值机|开始
        /// </summary>
        public string OutcncOutCheckInGuaranteeStartTime
        {
            set { m_stroutcncoutcheckinguaranteestarttime = value; }
            get { return m_stroutcncoutcheckinguaranteestarttime; }
        }
        /// <summary>
        /// 出港值机|结束
        /// </summary>
        public string OutcncOutCheckInGuaranteeEndTime
        {
            set { m_stroutcncoutcheckinguaranteeendtime = value; }
            get { return m_stroutcncoutcheckinguaranteeendtime; }
        }
        /// <summary>
        /// 进港客舱清洁|开始
        /// </summary>
        public string IncncInCabinCleanStartTime
        {
            set { m_strincncincabincleanstarttime = value; }
            get { return m_strincncincabincleanstarttime; }
        }
        /// <summary>
        /// 进港客舱清洁|结束
        /// </summary>
        public string IncncInCabinCleanEndTime
        {
            set { m_strincncincabincleanendtime = value; }
            get { return m_strincncincabincleanendtime; }
        }
        /// <summary>
        /// 出港客舱清洁|开始
        /// </summary>
        public string OutcncOutCabinCleanStartTime
        {
            set { m_stroutcncoutcabincleanstarttime = value; }
            get { return m_stroutcncoutcabincleanstarttime; }
        }
        /// <summary>
        /// 出港客舱清洁|结束
        /// </summary>
        public string OutcncOutCabinCleanEndTime
        {
            set { m_stroutcncoutcabincleanendtime = value; }
            get { return m_stroutcncoutcabincleanendtime; }
        }
        /// <summary>
        /// 进港下客|开始
        /// </summary>
        public string IncncInGuestOutStartTime
        {
            set { m_strincncinguestoutstarttime = value; }
            get { return m_strincncinguestoutstarttime; }
        }
        /// <summary>
        /// 进港下客|结束
        /// </summary>
        public string IncncInGuestOutEndTime
        {
            set { m_strincncinguestoutendtime = value; }
            get { return m_strincncinguestoutendtime; }
        }
        /// <summary>
        /// 进港引导员|到位
        /// </summary>
        public string IncncInGuiderArriveTime
        {
            set { m_strincncinguiderarrivetime = value; }
            get { return m_strincncinguiderarrivetime; }
        }
        /// <summary>
        /// 进港引导员|开始
        /// </summary>
        public string IncncInGuiderGuaranteeStartTime
        {
            set { m_strincncinguiderguaranteestarttime = value; }
            get { return m_strincncinguiderguaranteestarttime; }
        }
        /// <summary>
        /// 进港引导员|结束
        /// </summary>
        public string IncncInGuiderGuaranteeEndTime
        {
            set { m_strincncinguiderguaranteeendtime = value; }
            get { return m_strincncinguiderguaranteeendtime; }
        }
        /// <summary>
        /// 出港上客|开始
        /// </summary>
        public string OutcncOutGuestInStartTime
        {
            set { m_stroutcncoutguestinstarttime = value; }
            get { return m_stroutcncoutguestinstarttime; }
        }
        /// <summary>
        /// 出港上客|结束
        /// </summary>
        public string OutcncOutGuestInEndTime
        {
            set { m_stroutcncoutguestinendtime = value; }
            get { return m_stroutcncoutguestinendtime; }
        }
        /// <summary>
        /// 出港引导员|到位
        /// </summary>
        public string OutcncOutGuiderArriveTime
        {
            set { m_stroutcncoutguiderarrivetime = value; }
            get { return m_stroutcncoutguiderarrivetime; }
        }
        /// <summary>
        /// 出港引导员|开始
        /// </summary>
        public string OutcncOutGuiderGuaranteeStartTime
        {
            set { m_stroutcncoutguiderguaranteestarttime = value; }
            get { return m_stroutcncoutguiderguaranteestarttime; }
        }
        /// <summary>
        /// 出港引导员|结束
        /// </summary>
        public string OutcncOutGuiderGuaranteeEndTime
        {
            set { m_stroutcncoutguiderguaranteeendtime = value; }
            get { return m_stroutcncoutguiderguaranteeendtime; }
        }
        /// <summary>
        /// 出港通知|上客
        /// </summary>
        public string OutcncOutInformBoardTime
        {
            set { m_stroutcncoutinformboardtime = value; }
            get { return m_stroutcncoutinformboardtime; }
        }
        /// <summary>
        /// 出港登机门|关闭
        /// </summary>
        public string OutcncOutBoardingGateCloseTime
        {
            set { m_stroutcncoutboardinggateclosetime = value; }
            get { return m_stroutcncoutboardinggateclosetime; }
        }
        /// <summary>
        /// 出港舱单|上传完成
        /// </summary>
        public string OutcncOutLoadsheetUploadFinishedTime
        {
            set { m_stroutcncoutloadsheetuploadfinishedtime = value; }
            get { return m_stroutcncoutloadsheetuploadfinishedtime; }
        }
        /// <summary>
        /// 出港舱单|机长确认
        /// </summary>
        public string OutcncOutCaptainConfirmTime
        {
            set { m_stroutcncoutcaptainconfirmtime = value; }
            get { return m_stroutcncoutcaptainconfirmtime; }
        }
        /// <summary>
        /// 进港贵宾接机|开始
        /// </summary>
        public string IncncInVipPickUpGuaranteeStartTime
        {
            set { m_strincncinvippickupguaranteestarttime = value; }
            get { return m_strincncinvippickupguaranteestarttime; }
        }
        /// <summary>
        /// 进港贵宾接机|结束
        /// </summary>
        public string IncncInVipPickUpGuaranteeEndTime
        {
            set { m_strincncinvippickupguaranteeendtime = value; }
            get { return m_strincncinvippickupguaranteeendtime; }
        }
        /// <summary>
        /// 出港贵宾送机|开始
        /// </summary>
        public string OutcncOutVipSeeOffGuaranteeStartTime
        {
            set { m_stroutcncoutvipseeoffguaranteestarttime = value; }
            get { return m_stroutcncoutvipseeoffguaranteestarttime; }
        }
        /// <summary>
        /// 出港贵宾送机|结束
        /// </summary>
        public string OutcncOutVipSeeOffGuaranteeEndTime
        {
            set { m_stroutcncoutvipseeoffguaranteeendtime = value; }
            get { return m_stroutcncoutvipseeoffguaranteeendtime; }
        }
        /// <summary>
        /// 出港海关保障|开始
        /// </summary>
        public string OutcncOutCustomGuaranteeStartTime
        {
            set { m_stroutcncoutcustomguaranteestarttime = value; }
            get { return m_stroutcncoutcustomguaranteestarttime; }
        }
        /// <summary>
        /// 出港海关保障|结束
        /// </summary>
        public string OutcncOutCustomGuaranteeEndTime
        {
            set { m_stroutcncoutcustomguaranteeendtime = value; }
            get { return m_stroutcncoutcustomguaranteeendtime; }
        }
        /// <summary>
        /// 出港边检保障|开始
        /// </summary>
        public string OutcncOutFrontierInspectionGuaranteeStartTime
        {
            set { m_stroutcncoutfrontierinspectionguaranteestarttime = value; }
            get { return m_stroutcncoutfrontierinspectionguaranteestarttime; }
        }
        /// <summary>
        /// 出港边检保障|结束
        /// </summary>
        public string OutcncOutFrontierInspectionGuaranteeEndTime
        {
            set { m_stroutcncoutfrontierinspectionguaranteeendtime = value; }
            get { return m_stroutcncoutfrontierinspectionguaranteeendtime; }
        }
        /// <summary>
        /// 出港检疫保障|开始
        /// </summary>
        public string OutcncOutQuarantineGuaranteeStartTime
        {
            set { m_stroutcncoutquarantineguaranteestarttime = value; }
            get { return m_stroutcncoutquarantineguaranteestarttime; }
        }
        /// <summary>
        /// 出港检疫保障|结束
        /// </summary>
        public string OutcncOutQuarantineGuaranteeEndTime
        {
            set { m_stroutcncoutquarantineguaranteeendtime = value; }
            get { return m_stroutcncoutquarantineguaranteeendtime; }
        }


        /// <summary>
        /// 进港保障记录
        /// </summary>
        public string IncnvcInGuaranteeRecord
        {
            set { m_strincnvcinguaranteerecord = value; }
            get { return m_strincnvcinguaranteerecord; }
        }
        /// <summary>
        /// 出港保障记录
        /// </summary>
        public string OutcnvcOutGuaranteeRecord
        {
            set { m_stroutcnvcoutguaranteerecord = value; }
            get { return m_stroutcnvcoutguaranteerecord; }
        }
        #endregion 三期 added in 20140514

        #region 三期 added in 201504
        /// <summary>
        ///出港跑道号         
        /// </summary>
        public string OutcnvcOutRunway
        {
            set { _OutcnvcOutRunway = value; }
            get { return _OutcnvcOutRunway; }
        }

        /// <summary>
        ///进港跑道号         
        /// </summary>
        public string IncnvcInRunway
        {
            set { _IncnvcInRunway = value; }
            get { return _IncnvcInRunway; }
        }

        /// <summary>
        ///EXIT               
        /// </summary>
        public string IncncInEXIT
        {
            set { _IncncInEXIT = value; }
            get { return _IncncInEXIT; }
        }

        /// <summary>
        ///TOBT               
        /// </summary>
        public string OutcncOutTOBT
        {
            set { _OutcncOutTOBT = value; }
            get { return _OutcncOutTOBT; }
        }

        /// <summary>
        ///登机|开始          
        /// </summary>
        public string OutcncOutBoardStartTime
        {
            set { _OutcncOutBoardStartTime = value; }
            get { return _OutcncOutBoardStartTime; }
        }

        /// <summary>
        ///登机|结束          
        /// </summary>
        public string OutcncOutBoardEndTime
        {
            set { _OutcncOutBoardEndTime = value; }
            get { return _OutcncOutBoardEndTime; }
        }

        /// <summary>
        ///柜台结束时间       
        /// </summary>
        public string OutcncOutCheckCounterEndTime
        {
            set { _OutcncOutCheckCounterEndTime = value; }
            get { return _OutcncOutCheckCounterEndTime; }
        }

        /// <summary>
        ///除冰|除冰坪        
        /// </summary>
        public string OutcnvcOutDeicePing
        {
            set { _OutcnvcOutDeicePing = value; }
            get { return _OutcnvcOutDeicePing; }
        }

        /// <summary>
        ///除冰|除冰位        
        /// </summary>
        public string OutcnvcOutDeiceWei
        {
            set { _OutcnvcOutDeiceWei = value; }
            get { return _OutcnvcOutDeiceWei; }
        }

        /// <summary>
        ///除冰|到达除冰等待点
        /// </summary>
        public string OutcncOutArriveDeicePing
        {
            set { _OutcncOutArriveDeicePing = value; }
            get { return _OutcncOutArriveDeicePing; }
        }

        /// <summary>
        ///除冰|除冰开始      
        /// </summary>
        public string OutcncOutDeiceStartTime
        {
            set { _OutcncOutDeiceStartTime = value; }
            get { return _OutcncOutDeiceStartTime; }
        }

        /// <summary>
        ///除冰|除冰结束      
        /// </summary>
        public string OutcncOutDeiceEndTime
        {
            set { _OutcncOutDeiceEndTime = value; }
            get { return _OutcncOutDeiceEndTime; }
        }

        /// <summary>
        ///除冰|离开除冰位    
        /// </summary>
        public string OutcncOutLeaveDeicePing
        {
            set { _OutcncOutLeaveDeicePing = value; }
            get { return _OutcncOutLeaveDeicePing; }
        }

        /// <summary>
        ///除冰|慢车除冰标识  
        /// </summary>
        public string OutcnvcOutSlowDeiceFlag
        {
            set { _OutcnvcOutSlowDeiceFlag = value; }
            get { return _OutcnvcOutSlowDeiceFlag; }
        }

        /// <summary>
        /// TSAT               
        /// </summary>
        public string OutcncOutTSAT
        {
            set { _OutcncOutTSAT = value; }
            get { return _OutcncOutTSAT; }
        }

        /// <summary>
        /// CTOT               
        /// </summary>
        public string OutcncOutCTOT
        {
            set { _OutcncOutCTOT = value; }
            get { return _OutcncOutCTOT; }
        }

        #endregion 三期 added in 201504

        #region 三期 added in 20150428
        /// <summary>
        /// 第一位旅客|进入客舱               
        /// </summary>
        public string OutcncOutFirstPassengerComeInCabinTime
        {
            set { _OutcncOutFirstPassengerComeInCabinTime = value; }
            get { return _OutcncOutFirstPassengerComeInCabinTime; }
        }

        /// <summary>
        /// 飞机准备|完毕               
        /// </summary>
        public string OutcncOutPlaneReadyEndTime
        {
            set { _OutcncOutPlaneReadyEndTime = value; }
            get { return _OutcncOutPlaneReadyEndTime; }
        }
        #endregion 三期 added in 20150428

        #region 三期 added in 20150624
        /// <summary>
        /// 测试|进港机位               
        /// </summary>
        public string IncnvcInGATE_Test
        {
            set { _IncnvcInGATE_Test = value; }
            get { return _IncnvcInGATE_Test; }
        }

        /// <summary>
        /// 测试|出港机位               
        /// </summary>
        public string OutcnvcOutGate_Test
        {
            set { _OutcnvcOutGate_Test = value; }
            get { return _OutcnvcOutGate_Test; }
        }

        /// <summary>
        /// 出港截载|时间               
        /// </summary>
        public string OutcncOutFlightInterceptTime
        {
            set { _OutcncOutFlightInterceptTime = value; }
            get { return _OutcncOutFlightInterceptTime; }
        }

        /// <summary>
        /// 进港转盘|号码              
        /// </summary>
        public string IncnvcInTurnTableNO
        {
            set { _IncnvcInTurnTableNO = value; }
            get { return _IncnvcInTurnTableNO; }
        }

        /// <summary>
        /// 出港转盘|号码             
        /// </summary>
        public string OutcnvcOutTurnTableNO
        {
            set { _OutcnvcOutTurnTableNO = value; }
            get { return _OutcnvcOutTurnTableNO; }
        }
        #endregion 三期 added in 20150624

        #region 三期 added in 20150730
        /// <summary>
        /// 出港空勤组|出发              
        /// </summary>
        public string OutcncOutCrewStartTime
        {
            set { _OutcncOutCrewStartTime = value; }
            get { return _OutcncOutCrewStartTime; }
        }

        /// <summary>
        /// 出港空勤组|到位               
        /// </summary>
        public string OutcncOutCrewArriveTime
        {
            set { _OutcncOutCrewArriveTime = value; }
            get { return _OutcncOutCrewArriveTime; }
        }

        /// <summary>
        /// 出港航班|过站类型               
        /// </summary>
        public string OutcnvcOutOverStationType
        {
            set { _OutcnvcOutOverStationType = value; }
            get { return _OutcnvcOutOverStationType; }
        }
        #endregion 三期 added in 20150730

        #region 三期 added in 20160323
        /// <summary>
        /// 出港关注人员|现场                          
        /// </summary>
        public string OutcnvcOutSpotUsers
        {
            set { _OutcnvcOutSpotUsers = value; }
            get { return _OutcnvcOutSpotUsers; }

        }

        /// <summary>
        /// 出港关注人员|机务                          
        /// </summary>
        public string OutcnvcOutMaintenanceUsers
        {
            set { _OutcnvcOutMaintenanceUsers = value; }
            get { return _OutcnvcOutMaintenanceUsers; }

        }

        /// <summary>
        /// 出港关注人员|车队                          
        /// </summary>
        public string OutcnvcOutFleetUsers
        {
            set { _OutcnvcOutFleetUsers = value; }
            get { return _OutcnvcOutFleetUsers; }

        }

        /// <summary>
        /// 出港关注人员|贵宾室                        
        /// </summary>
        public string OutcnvcOutVIPRoomUsers
        {
            set { _OutcnvcOutVIPRoomUsers = value; }
            get { return _OutcnvcOutVIPRoomUsers; }

        }

        /// <summary>
        /// 出港关注人员|航食                          
        /// </summary>
        public string OutcnvcOutAviationDietUsers
        {
            set { _OutcnvcOutAviationDietUsers = value; }
            get { return _OutcnvcOutAviationDietUsers; }

        }

        /// <summary>
        /// 出港关注人员|值机                          
        /// </summary>
        public string OutcnvcOutCheckInUsers
        {
            set { _OutcnvcOutCheckInUsers = value; }
            get { return _OutcnvcOutCheckInUsers; }

        }

        /// <summary>
        /// 出港关注人员|清洁队                        
        /// </summary>
        public string OutcnvcOutCleaningTeamUsers
        {
            set { _OutcnvcOutCleaningTeamUsers = value; }
            get { return _OutcnvcOutCleaningTeamUsers; }

        }

        /// <summary>
        /// 进港关注人员|现场                          
        /// </summary>
        public string IncnvcInSpotUsers
        {
            set { _IncnvcInSpotUsers = value; }
            get { return _IncnvcInSpotUsers; }

        }

        /// <summary>
        /// 进港关注人员|机务                          
        /// </summary>
        public string IncnvcInMaintenanceUsers
        {
            set { _IncnvcInMaintenanceUsers = value; }
            get { return _IncnvcInMaintenanceUsers; }

        }

        /// <summary>
        /// 进港关注人员|车队                          
        /// </summary>
        public string IncnvcInFleetUsers
        {
            set { _IncnvcInFleetUsers = value; }
            get { return _IncnvcInFleetUsers; }

        }

        /// <summary>
        /// 进港关注人员|贵宾室                        
        /// </summary>
        public string IncnvcInVIPRoomUsers
        {
            set { _IncnvcInVIPRoomUsers = value; }
            get { return _IncnvcInVIPRoomUsers; }

        }

        /// <summary>
        /// 进港关注人员|航食                          
        /// </summary>
        public string IncnvcInAviationDietUsers
        {
            set { _IncnvcInAviationDietUsers = value; }
            get { return _IncnvcInAviationDietUsers; }

        }

        /// <summary>
        /// 进港关注人员|值机                          
        /// </summary>
        public string IncnvcInCheckInUsers
        {
            set { _IncnvcInCheckInUsers = value; }
            get { return _IncnvcInCheckInUsers; }

        }

        /// <summary>
        /// 进港关注人员|清洁队                        
        /// </summary>
        public string IncnvcInCleaningTeamUsers
        {
            set { _IncnvcInCleaningTeamUsers = value; }
            get { return _IncnvcInCleaningTeamUsers; }

        }

        #endregion 三期 added in 20160323

        #region 三期 added in 20160503
        /// <summary>
        /// 出发到达|计划                        
        /// </summary>
        public string OutcncSTA
        {
            set { _OutcncSTA = value; }
            get { return _OutcncSTA; }
        }

        /// <summary>
        /// 出发到达|预达                       
        /// </summary>
        public string OutcncETA
        {
            set { _OutcncETA = value; }
            get { return _OutcncETA; }
        }

        /// <summary>
        /// 出发到达|落地                       
        /// </summary>
        public string OutcncTDWN
        {
            set { _OutcncTDWN = value; }
            get { return _OutcncTDWN; }
        }

        /// <summary>
        /// 出发到达|到位                       
        /// </summary>
        public string OutcncATA
        {
            set { _OutcncATA = value; }
            get { return _OutcncATA; }
        }

        #endregion 三期 added in 20160503

        #region 三期 added in 20160527
        /// <summary>
        /// 进港传送带车|用车次数                            
        /// </summary>
        public string IncniInConveyerBeltVehicleGuaranteeCount
        {
            set { _IncniInConveyerBeltVehicleGuaranteeCount = value; }
            get { return _IncniInConveyerBeltVehicleGuaranteeCount; }
        }

        /// <summary>
        /// 出港传送带车|用车次数                            
        /// </summary>
        public string OutcniOutConveyerBeltVehicleGuaranteeCount
        {
            set { _OutcniOutConveyerBeltVehicleGuaranteeCount = value; }
            get { return _OutcniOutConveyerBeltVehicleGuaranteeCount; }
        }

        /// <summary>
        /// 进港行李牵引车|用车次数                          
        /// </summary>
        public string IncniInBaggageCarGuaranteeCount
        {
            set { _IncniInBaggageCarGuaranteeCount = value; }
            get { return _IncniInBaggageCarGuaranteeCount; }
        }

        /// <summary>
        /// 出港行李牵引车|用车次数                          
        /// </summary>
        public string OutcniOutBaggageCarGuaranteeCount
        {
            set { _OutcniOutBaggageCarGuaranteeCount = value; }
            get { return _OutcniOutBaggageCarGuaranteeCount; }
        }

        /// <summary>
        /// 进港机组车|用车次数                              
        /// </summary>
        public string IncniInCrewCarGuaranteeCount
        {
            set { _IncniInCrewCarGuaranteeCount = value; }
            get { return _IncniInCrewCarGuaranteeCount; }
        }

        /// <summary>
        /// 出港机组车|用车次数                              
        /// </summary>
        public string OutcniOutCrewCarGuaranteeCount
        {
            set { _OutcniOutCrewCarGuaranteeCount = value; }
            get { return _OutcniOutCrewCarGuaranteeCount; }
        }

        /// <summary>
        /// 进港平台车|用车次数                              
        /// </summary>
        public string IncniInPlatformCarGuaranteeCount
        {
            set { _IncniInPlatformCarGuaranteeCount = value; }
            get { return _IncniInPlatformCarGuaranteeCount; }
        }

        /// <summary>
        /// 出港平台车|用车次数                              
        /// </summary>
        public string OutcniOutPlatformCarGuaranteeCount
        {
            set { _OutcniOutPlatformCarGuaranteeCount = value; }
            get { return _OutcniOutPlatformCarGuaranteeCount; }
        }

        /// <summary>
        /// 进港客梯车|用车次数                              
        /// </summary>
        public string IncniInLadderCarGuaranteeCount
        {
            set { _IncniInLadderCarGuaranteeCount = value; }
            get { return _IncniInLadderCarGuaranteeCount; }
        }

        /// <summary>
        /// 出港客梯车|用车次数                              
        /// </summary>
        public string OutcniOutLadderCarGuaranteeCount
        {
            set { _OutcniOutLadderCarGuaranteeCount = value; }
            get { return _OutcniOutLadderCarGuaranteeCount; }
        }

        /// <summary>
        /// 进港摆渡车|用车次数                              
        /// </summary>
        public string IncniInFerryGuaranteeCount
        {
            set { _IncniInFerryGuaranteeCount = value; }
            get { return _IncniInFerryGuaranteeCount; }
        }

        /// <summary>
        /// 出港摆渡车|用车次数                              
        /// </summary>
        public string OutcniOutFerryGuaranteeCount
        {
            set { _OutcniOutFerryGuaranteeCount = value; }
            get { return _OutcniOutFerryGuaranteeCount; }
        }

        /// <summary>
        /// 出港除冰车|用车次数                              
        /// </summary>
        public string OutcniOutDeicingVehicleGuaranteeCount
        {
            set { _OutcniOutDeicingVehicleGuaranteeCount = value; }
            get { return _OutcniOutDeicingVehicleGuaranteeCount; }
        }

        /// <summary>
        /// 出港拖车|用车次数                                
        /// </summary>
        public string OutcniOutTrailCarGuaranteeCount
        {
            set { _OutcniOutTrailCarGuaranteeCount = value; }
            get { return _OutcniOutTrailCarGuaranteeCount; }
        }

        /// <summary>
        /// 出港备注|车队                                    
        /// </summary>
        public string OutcnvcOutRemark_Fleet
        {
            set { _OutcnvcOutRemark_Fleet = value; }
            get { return _OutcnvcOutRemark_Fleet; }
        }

        /// <summary>
        /// 进港备注|车队                                    
        /// </summary>
        public string IncnvcInRemark_Fleet
        {
            set { _IncnvcInRemark_Fleet = value; }
            get { return _IncnvcInRemark_Fleet; }
        }

        #endregion 三期 added in 20160527

        #region 三期 added in 20160616

        /// <summary> 
        /// 配货|操作人                                                   
        /// </summary> 
        public string OutcnvcOutArrangeCargoOperator
        {
            set { _OutcnvcOutArrangeCargoOperator = value; }
            get { return _OutcnvcOutArrangeCargoOperator; }
        }

        /// <summary> 
        /// 配货|时间                                                     
        /// </summary> 
        public string OutcncOutArrangeCargoOperateTime
        {
            set { _OutcncOutArrangeCargoOperateTime = value; }
            get { return _OutcncOutArrangeCargoOperateTime; }
        }

        /// <summary> 
        /// 装机单复核|操作人                                             
        /// </summary> 
        public string OutcnvcOutRecheckLoadSheetOperator
        {
            set { _OutcnvcOutRecheckLoadSheetOperator = value; }
            get { return _OutcnvcOutRecheckLoadSheetOperator; }
        }

        /// <summary> 
        /// 装机单复核|时间                                               
        /// </summary> 
        public string OutcncOutRecheckLoadSheetOperateTime
        {
            set { _OutcncOutRecheckLoadSheetOperateTime = value; }
            get { return _OutcncOutRecheckLoadSheetOperateTime; }
        }

        /// <summary> 
        /// 装机单确认|操作人                                             
        /// </summary> 
        public string OutcnvcOutConfirmLoadSheetOperator
        {
            set { _OutcnvcOutConfirmLoadSheetOperator = value; }
            get { return _OutcnvcOutConfirmLoadSheetOperator; }
        }

        /// <summary> 
        /// 装机单确认|时间                                               
        /// </summary> 
        public string OutcncOutConfirmLoadSheetOperateTime
        {
            set { _OutcncOutConfirmLoadSheetOperateTime = value; }
            get { return _OutcncOutConfirmLoadSheetOperateTime; }
        }

        /// <summary> 
        /// 装机单确认|版本号                                             
        /// </summary> 
        public string OutcnvcOutConfirmLoadSheetOperateVerNumber
        {
            set { _OutcnvcOutConfirmLoadSheetOperateVerNumber = value; }
            get { return _OutcnvcOutConfirmLoadSheetOperateVerNumber; }
        }

        /// <summary> 
        /// 舱单核对|操作人                                               
        /// </summary> 
        public string OutcnvcOutCheckManifestOperator
        {
            set { _OutcnvcOutCheckManifestOperator = value; }
            get { return _OutcnvcOutCheckManifestOperator; }
        }

        /// <summary> 
        /// 舱单核对|时间                                                 
        /// </summary> 
        public string OutcncOutCheckManifestOperateTime
        {
            set { _OutcncOutCheckManifestOperateTime = value; }
            get { return _OutcncOutCheckManifestOperateTime; }
        }

        /// <summary> 
        /// 舱单复核|操作人                                               
        /// </summary> 
        public string OutcnvcOutRecheckManifestOperator
        {
            set { _OutcnvcOutRecheckManifestOperator = value; }
            get { return _OutcnvcOutRecheckManifestOperator; }
        }

        /// <summary> 
        /// 舱单复核|时间                                                 
        /// </summary> 
        public string OutcncOutRecheckManifestOperateTime
        {
            set { _OutcncOutRecheckManifestOperateTime = value; }
            get { return _OutcncOutRecheckManifestOperateTime; }
        }

        /// <summary> 
        /// 舱单上传|操作人                                               
        /// </summary> 
        public string OutcnvcOutUploadManifestOperator
        {
            set { _OutcnvcOutUploadManifestOperator = value; }
            get { return _OutcnvcOutUploadManifestOperator; }
        }

        /// <summary> 
        /// 舱单上传|时间                                                 
        /// </summary> 
        public string OutcncOutUploadManifestOperateTime
        {
            set { _OutcncOutUploadManifestOperateTime = value; }
            get { return _OutcncOutUploadManifestOperateTime; }
        }

        /// <summary> 
        /// 舱单上传|上传结果                                             
        /// </summary> 
        public string OutcnvcOutUploadManifestOperateResult
        {
            set { _OutcnvcOutUploadManifestOperateResult = value; }
            get { return _OutcnvcOutUploadManifestOperateResult; }
        }

        /// <summary> 
        /// 舱单确认情况|机组确认结果                                     
        /// </summary> 
        public string OutcnvcOutCrewConfirmManifestOperateResult
        {
            set { _OutcnvcOutCrewConfirmManifestOperateResult = value; }
            get { return _OutcnvcOutCrewConfirmManifestOperateResult; }
        }

        /// <summary> 
        /// 舱单确认情况|时间                                             
        /// </summary> 
        public string OutcncOutCrewConfirmManifestOperateTime
        {
            set { _OutcncOutCrewConfirmManifestOperateTime = value; }
            get { return _OutcncOutCrewConfirmManifestOperateTime; }
        }

        /// <summary> 
        /// 送舱单或平衡图|操作人                                         
        /// </summary> 
        public string OutcnvcOutTransportManifestOperator
        {
            set { _OutcnvcOutTransportManifestOperator = value; }
            get { return _OutcnvcOutTransportManifestOperator; }
        }

        /// <summary> 
        /// 送舱单或平衡图|时间                                           
        /// </summary> 
        public string OutcncOutTransportManifestOperateTime
        {
            set { _OutcncOutTransportManifestOperateTime = value; }
            get { return _OutcncOutTransportManifestOperateTime; }
        }

        /// <summary> 
        /// 出港备注|配载                                                 
        /// </summary> 
        public string OutcnvcOutRemark_Balance
        {
            set { _OutcnvcOutRemark_Balance = value; }
            get { return _OutcnvcOutRemark_Balance; }
        }  
        #endregion 三期 added in 20160616

        #region 三期 added in 20160704
        /// <summary> 
        /// 进港关注人员|领导                                                 
        /// </summary> 
        public string IncnvcInLeaders
        {
            set { _IncnvcInLeaders = value; }
            get { return _IncnvcInLeaders; }
        }

        /// <summary> 
        /// 出港关注人员|领导                                                 
        /// </summary> 
        public string OutcnvcOutLeaders
        {
            set { _OutcnvcOutLeaders = value; }
            get { return _OutcnvcOutLeaders; }
        }
        #endregion 三期 added in 20160704

        #endregion 属性

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="sIndex"></param>
        /// <returns></returns>
        public string this[string sIndex]
        {
            get
            {
                #region 以往
                if (sIndex == "IncncDATOP")
                {
                    return m_strInDATOP;
                }

                else if (sIndex == "IncnvcFLTID")
                {
                    return m_strInFLTID;
                }

                else if (sIndex == "IncniLEGNO")
                {
                    return m_strInLEGNO;
                }

                else if (sIndex == "IncnvcAC")
                {
                    return m_strInAC;
                }

                else if (sIndex == "IncncAllSTA")
                {
                    return m_strInAllSTA;
                }


                else if (sIndex == "IncncAllETA")
                {
                    return m_strInAllETA;
                }

                else if (sIndex == "IncncAllTDWN")
                {
                    return m_strInAllTDWN;
                }

                else if (sIndex == "IncniAllViewIndex")
                {
                    return m_strInAllViewIndex;
                }

                else if (sIndex == "IncncAllATA")
                {
                    return m_strInAllATA;
                }

                else if (sIndex == "IncncAllStatus")
                {
                    return m_strInAllStatus;
                }

                else if (sIndex == "IncnvcLONG_REG")
                {
                    return m_strInLONG_REG;
                }
                else if (sIndex == "IncncFlightDate")
                {
                    return m_strInFlightDate; ;
                }
                else if (sIndex == "IncnvcFlightNo")
                {
                    return m_strInFlightNo;
                }
                else if (sIndex == "IncncACTYP")
                {
                    return m_strInACTYP;
                }
                else if (sIndex == "IncnvcFlightCharacterAbbreviate")
                {
                    return m_strInFlightCharacterAbbreviate;
                }
                else if (sIndex == "IncncDEPAirportCNAME")
                {
                    return m_strInDEPAirportCNAME;
                }
                else if (sIndex == "IncncARRAirportCNAME")
                {
                    return m_strInARRAirportCNAME;
                }
                else if (sIndex == "IncncSTA")
                {
                    return m_strInSTA;
                }
                else if (sIndex == "IncncETA")
                {
                    return m_strInETA;
                }
                else if (sIndex == "IncncTDWN")
                {
                    return m_strInTDWN;
                }
                else if (sIndex == "IncncATA")
                {
                    return m_strInATA;
                }
                else if (sIndex == "IncncACARSTOFF")
                {
                    return m_strInACARSTOFF;
                }
                else if (sIndex == "IncncACARSTDWN")
                {
                    return m_strInACARSTDWN;
                }
                else if (sIndex == "IncncACARSATA")
                {
                    return m_strInACARSATA;
                }
                else if (sIndex == "IncnvcInDelayName")
                {
                    return m_strInInDelayName;
                }
                else if (sIndex == "IncnvcFlightDelayName")
                {
                    return m_strInFlightDelayName;
                }
                else if (sIndex == "IncnvcDiversionDelayName")
                {
                    return m_strInDiversionDelayName;
                }
                else if (sIndex == "IncnvcInCarInfor")
                {
                    return m_strInInCarInfor;
                }
                else if (sIndex == "IncncInCarDepTime")
                {
                    return m_strInInCarDepTime;
                }
                else if (sIndex == "IncncInCarArrTime")
                {
                    return m_strInInCarArrTime;
                }
                else if (sIndex == "IncnbVIPTag")
                {
                    return m_strInVIPTag;
                }
                else if (sIndex == "IncniCheckNum")
                {
                    return m_strInCheckNum;
                }
                else if (sIndex == "IncnbTransitPaxTag")
                {
                    return m_strInTransitPaxTag;
                }
                else if (sIndex == "IncniBaggageWeight")
                {
                    return m_strInBaggageWeight;
                }
                else if (sIndex == "IncniCargoWeight")
                {
                    return m_strInCargoWeight;
                }
                else if (sIndex == "IncncStatusName")
                {
                    return m_strInStatusName;
                }
                else if (sIndex == "IncnvcInRemark")
                {
                    return m_strInInRemark;
                }
                else if (sIndex == "IncnvcInAircraftStatus")
                {
                    return m_strInInAircraftStatus;
                }
                else if (sIndex == "IncnvcInGATE")
                {
                    return m_strInInGATE;
                }
                else if (sIndex == "OutcncDATOP")
                {
                    return m_strOutDATOP;
                }

                else if (sIndex == "OutcnvcFLTID")
                {
                    return m_strOutFLTID;
                }

                else if (sIndex == "OutcniLEGNO")
                {
                    return m_strOutLEGNO;
                }

                else if (sIndex == "OutcnvcAC")
                {
                    return m_strOutAC;
                }

                else if (sIndex == "OutcncAllSTD")
                {
                    return m_strOutAllSTD;
                }


                else if (sIndex == "OutcncAllETD")
                {
                    return m_strOutAllETD;
                }

                else if (sIndex == "OutcncAllTOFF")
                {
                    return m_strOutAllTOFF;
                }

                else if (sIndex == "OutcniAllViewIndex")
                {
                    return m_strOutAllViewIndex;
                }

                else if (sIndex == "OutcncAllATD")
                {
                    return m_strOutAllATD;
                }

                else if (sIndex == "OutcncAllStatus")
                {
                    return m_strOutAllStatus;
                }

                else if (sIndex == "OutcnvcOutGate")
                {
                    return m_strOutOutGate;
                }
                else if (sIndex == "OutcnvcLONG_REG")
                {
                    return m_strOutLONG_REG;
                }
                else if (sIndex == "OutcncFlightDate")
                {
                    return m_strOutFlightDate;
                }
                else if (sIndex == "OutcnvcFlightNo")
                {
                    return m_strOutFlightNo;
                }
                else if (sIndex == "OutcnvcFlightCharacterAbbreviate")
                {
                    return m_strOutFlightCharacterAbbreviate;
                }
                else if (sIndex == "OutcncDEPAirportCNAME")
                {
                    return m_strOutDEPAirportCNAME;
                }
                else if (sIndex == "OutcncARRAirportCNAME")
                {
                    return m_strOutARRAirportCNAME;
                }
                else if (sIndex == "OutcncSTD")
                {
                    return m_strOutSTD;
                }
                else if (sIndex == "OutcncETD")
                {
                    return m_strOutETD;
                }
                else if (sIndex == "OutcncStartGuaranteeTime")
                {
                    return m_strOutStartGuaranteeTime;
                }
                else if (sIndex == "OutcniIntermissionTime")
                {
                    return m_strOutIntermissionTime;
                }
                else if (sIndex == "OutcnvcJoinFlight")
                {
                    return m_strOutJoinFlight;
                }
                else if (sIndex == "OutcnvcCheckCounter")
                {
                    return m_strOutCheckCounter;
                }
                else if (sIndex == "OutcnvcWaitHall")
                {
                    return m_strOutWaitHall;
                }
                else if (sIndex == "OutcnvcBoradingGate")
                {
                    return m_strOutBoradingGate;
                }
                else if (sIndex == "OutcncOpenCabinTime")
                {
                    return m_strOutOpenCabinTime;
                }
                else if (sIndex == "OutcnvcOutCarInfor")
                {
                    return m_strOutOutCarInfor;
                }
                else if (sIndex == "OutcncOutCarDepTime")
                {
                    return m_strOutOutCarDepTime;
                }
                else if (sIndex == "OutcncOutCarArrTime")
                {
                    return m_strOutOutCarArrTime;
                }
                else if (sIndex == "OutcncPilotArrTime")
                {
                    return m_strOutPilotArrTime;
                }
                else if (sIndex == "OutcncStewardArrTime")
                {
                    return m_strOutStewardArrTime;
                }
                else if (sIndex == "OutcncDeiceStartTime")
                {
                    return m_strOutDeiceStartTime;
                }
                else if (sIndex == "OutcncDeiceEndTime")
                {
                    return m_strOutDeiceEndTime;
                }
                else if (sIndex == "OutcncTaskSheetChangeTime")
                {
                    return m_strOutTaskSheetChangeTime;
                }
                else if (sIndex == "OutcncDispatchTime")
                {
                    return m_strOutDispatchTime;
                }
                else if (sIndex == "OutcncDispatchPrintTime")
                {
                    return m_strOutDispatchPrintTime;
                }
                else if (sIndex == "OutcncOutMCCReadyTime")
                {
                    return m_strOutOutMCCReadyTime;
                }
                else if (sIndex == "OutcncOutMCCReadyTime")
                {
                    return m_strOutOutMCCReadyTime;
                }
                else if (sIndex == "IncncInTime")
                {
                    return m_strInInTime;
                }                
                else if (sIndex == "OutcncMCCReleaseTime")
                {
                    return m_strOutMCCReleaseTime;
                }
                else if (sIndex == "OutcncOutTime")
                {
                    return m_strOutOutTime;
                }
                else if (sIndex == "OutcnvcAircraftStatus")
                {
                    return m_strOutAircraftStatus;
                }
                else if (sIndex == "OutcncDeplaneTime")
                {
                    return m_strOutDeplaneTime;
                }
                else if (sIndex == "OutcncCleanStartTime")
                {
                    return m_strOutCleanStartTime;
                }
                else if (sIndex == "OutcncCleanEndTime")
                {
                    return m_strOutCleanEndTime;
                }
                else if (sIndex == "OutcncSewageStartTime")
                {
                    return m_strOutSewageStartTime;
                }
                else if (sIndex == "OutcncSewageEndTime")
                {
                    return m_strOutSewageEndTime;
                }
                else if (sIndex == "OutcncCabinSupplyStartTime")
                {
                    return m_strOutCabinSupplyStartTime;
                }
                else if (sIndex == "OutcncCabinSupplyEndTime")
                {
                    return m_strOutCabinSupplyEndTime;
                }
                else if (sIndex == "OutcncOilStartTime")
                {
                    return m_strOutOilStartTime;
                }
                else if (sIndex == "OutcncOilEndTime")
                {
                    return m_strOutOilEndTime;
                }
                else if (sIndex == "OutcncOpenCargoCabinTime")
                {
                    return m_strOutOpenCargoCabinTime;
                }
                else if (sIndex == "OutcncCargoStartTime")
                {
                    return m_strOutCargoStartTime;
                }
                else if (sIndex == "OutcncBaggageTime")
                {
                    return m_strOutBaggageTime;
                }
                else if (sIndex == "OutcncCloseCargoCabinTime")
                {
                    return m_strOutCloseCargoCabinTime;
                }
                else if (sIndex == "OutcncTrailerArrTime")
                {
                    return m_strOutTrailerArrTime;
                }
                else if (sIndex == "OutcncDirtyWaterCarArrTime")
                {
                    return m_strOutDirtyWaterCarArrTime;
                }
                else if (sIndex == "OutcncFerryDepTime")
                {
                    return m_strOutFerryDepTime;
                }
                else if (sIndex == "OutcncLastFerryArrTime")
                {
                    return m_strOutLastFerryArrTime;
                }
                else if (sIndex == "OutcncDragPoleArrTime")
                {
                    return m_strOutDragPoleArrTime;
                }
                else if (sIndex == "OutcncLadderCarArrTime")
                {
                    return m_strOutLadderCarArrTime;
                }
                else if (sIndex == "OutcncInformBoardTime")
                {
                    return m_strOutInformBoardTime;
                }
                else if (sIndex == "OutcncBoardTime")
                {
                    return m_strOutBoardTime;
                }
                else if (sIndex == "OutcnvcBookNum")
                {
                    return m_strOutBookNum;
                }
                else if (sIndex == "OutcniCheckNum")
                {
                    return m_strOutCheckNum;
                }
                else if (sIndex == "OutcnbTransitPaxTag")
                {
                    return m_strOutTransitPaxTag;
                }
                else if (sIndex == "OutcniXCRNum")
                {
                    return m_strOutXCRNum;
                }
                else if (sIndex == "OutcniAdultNum")
                {
                    return m_strOutAdultNum;
                }
                else if (sIndex == "OutcniChildNum")
                {
                    return m_strOutChildNum;
                }
                else if (sIndex == "OutcniInfantNum")
                {
                    return m_strOutInfantNum;
                }
                else if (sIndex == "OutcniFirstClassNum")
                {
                    return m_strOutFirstClassNum;
                }
                else if (sIndex == "OutcniOfficialClassNum")
                {
                    return m_strOutOfficialClassNum;
                }
                else if (sIndex == "OutcniTouristClassNum")
                {
                    return m_strOutTouristClassNum;
                }
                else if (sIndex == "OutcniAscendingPaxNum")
                {
                    return m_strOutAscendingPaxNum;
                }
                else if (sIndex == "OutcntPaxNameList")
                {
                    return m_strOutPaxNameList;
                }
                else if (sIndex == "OutcncBoardOverTime")
                {
                    return m_strOutBoardOverTime;
                }
                else if (sIndex == "OutcncLoadSheetArrTime")
                {
                    return m_strOutLoadSheetArrTime;
                }
                else if (sIndex == "OutcncClosePaxCabinTime")
                {
                    return m_strOutClosePaxCabinTime;
                }
                else if (sIndex == "OutcniOpenTime")
                {
                    return m_strOutOpenTime;
                }
                else if (sIndex == "OutcncInternalGuestTogether")
                {
                    return m_strOutInternalGuestTogether;
                }
                else if (sIndex == "OutcncPushTime")
                {
                    return m_strOutPushTime;
                }
                else if (sIndex == "OutcncATD")
                {
                    return m_strOutATD;
                }
                else if (sIndex == "OutcncTOFF")
                {
                    return m_strOutTOFF;
                }
                else if (sIndex == "OutcncACARSOUT")
                {
                    return m_strOutACARSOUT;
                }
                else if (sIndex == "OutcncACARSTOFF")
                {
                    return m_strOutACARSTOFF;
                }
                else if (sIndex == "OutcnvcDisChargingDelName")
                {
                    return m_strOutDisChargingDelName;
                }
                else if (sIndex == "OutcnvcOutDelayName")
                {
                    return m_strOutOutDelayName;
                }
                else if (sIndex == "OutcnvcFlightDelayName")
                {
                    return m_strOutFlightDelayName;
                }
                else if (sIndex == "OutcnvcDiversionDelayName")
                {
                    return m_strOutDiversionDelayName;
                }
                else if (sIndex == "OutcniCargoWeight")
                {
                    return m_strOutCargoWeight;
                }
                else if (sIndex == "OutcniMailWeight")
                {
                    return m_strOutMailWeight;
                }
                else if (sIndex == "OutcniBaggageWeight")
                {
                    return m_strOutBaggageWeight;
                }
                else if (sIndex == "OutcniAirMaterialWeight")
                {
                    return m_strOutAirMaterialWeight;
                }
                else if (sIndex == "OutcnvcAirMaterialRemark")
                {
                    return m_strOutAirMaterialRemark;
                }
                else if (sIndex == "OutcniBaggageNum")
                {
                    return m_strOutBaggageNum;
                }
                else if (sIndex == "OutcnbVIPTag")
                {
                    return m_strOutVIPTag;
                }
                else if (sIndex == "OutcniTotalFuelWeight")
                {
                    return m_strOutTotalFuelWeight;
                }
                else if (sIndex == "OutcniTripFuelWeight")
                {
                    return m_strOutTripFuelWeight;
                }
                else if (sIndex == "OutcniTaxiFuelWeight")
                {
                    return m_strOutTaxiFuelWeight;
                }
                else if (sIndex == "OutcniUnloadCargoWeight")
                {
                    return m_strOutUnloadCargoWeight;
                }
                else if (sIndex == "OutcnvcCargoRemark")
                {
                    return m_strOutCargoRemark;
                }
                else if (sIndex == "OutcncStatusName")
                {
                    return m_strOutStatusName;
                }
                else if (sIndex == "OutcnvcOutRemark")
                {
                    return m_strOutOutRemark;
                }
                else if (sIndex == "OutcnvcChiefController")
                {
                    return m_strOutChiefController;
                }
                else if (sIndex == "OutcnvcAssistantController")
                {
                    return m_strOutAssistantController;
                }
                else if (sIndex == "OutcnvcOutFieldController")
                {
                    return m_strOutOutFieldController;
                }
                else if (sIndex == "OutcnvcBalanceController")
                {
                    return m_strOutBalanceController;
                }
                else if (sIndex == "OutcniDischargingDelayTime")
                {
                    return m_strOutDischargingDelayTime;
                }
                else if (sIndex == "OutcniDUR1")
                {
                    return m_strOutcniDUR1;
                }
                else if (sIndex == "OutcniFocusTag")
                {
                    return m_strOutFocusTag;
                }
                else if (sIndex == "OutcnvcArrangedTask")
                {
                    return m_strOutArrangedTask;
                }
                #endregion 以往

                #region added in 20101113
                else if (sIndex == "IncncInMCCTechEngr")
                {
                    return m_strInInMCCTechEngr;
                }
                else if (sIndex == "IncnvcInArrangedTask")
                {
                    return m_strInInArrangedTask;
                }
                else if (sIndex == "IncnvcInSMCRemark")
                {
                    return m_strInInSMCRemark;
                }
                else if (sIndex == "OutcnvcOutSMCRemark")
                {
                    return m_strOutOutSMCRemark;
                }

                #endregion added in 20101113

                #region added in 20111031
                else if (sIndex == "IncncInMCCReleaseTime")
                {
                    return m_strInMCCReleaseTime;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoYK")
                {
                    return m_strOutWAOperationInfoYK;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoFX")
                {
                    return m_strOutWAOperationInfoFX;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoKF")
                {
                    return m_strOutWAOperationInfoKF;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoGC")
                {
                    return m_strOutWAOperationInfoGC;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoAJ")
                {
                    return m_strOutWAOperationInfoAJ;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoSC")
                {
                    return m_strOutWAOperationInfoSC;
                }
                #endregion added in 20111031

                #region added in 20121113
                else if (sIndex == "OutcncCDMeOutTime")
                {
                    return m_strOutcncCDMeOutTime;
                }
                else if (sIndex == "OutcncCDMeOffTime")
                {
                    return m_strOutcncCDMeOffTime;
                }
                else if (sIndex == "OutcncCDMvStartTime")
                {
                    return m_strOutcncCDMvStartTime;
                }
                #endregion added in 20121113

                #region 三期 added in 20140514
                else if (sIndex == "IncncInLadderCarGuaranteeStartTime")
                {
                    return m_strincncinladdercarguaranteestarttime;
                }

                else if (sIndex == "IncncInLadderCarGuaranteeEndTime")
                {
                    return m_strincncinladdercarguaranteeendtime;
                }

                else if (sIndex == "IncncInBridgeGuaranteeStartTime")
                {
                    return m_strincncinbridgeguaranteestarttime;
                }

                else if (sIndex == "IncncInBridgeGuaranteeEndTime")
                {
                    return m_strincncinbridgeguaranteeendtime;
                }

                else if (sIndex == "IncncInFerryrGuaranteeStartTime")
                {
                    return m_strincncinferryrguaranteestarttime;
                }

                else if (sIndex == "IncncInFerryGuaranteeEndTime")
                {
                    return m_strincncinferryguaranteeendtime;
                }

                else if (sIndex == "IncncInFerryGuaranteeFirstArrivePlaneTime")
                {
                    return m_strincncinferryguaranteefirstarriveplanetime;
                }

                else if (sIndex == "IncncInFerryGuaranteeLastArrivePlaneTime")
                {
                    return m_strincncinferryguaranteelastarriveplanetime;
                }

                else if (sIndex == "IncncInFerryrGuaranteeArriveTime")
                {
                    return m_strincncinferryrguaranteearrivetime;
                }

                else if (sIndex == "IncncInCrewCarrGuaranteeStartTime")
                {
                    return m_strincncincrewcarrguaranteestarttime;
                }

                else if (sIndex == "IncncInCrewCarGuaranteeEndTime")
                {
                    return m_strincncincrewcarguaranteeendtime;
                }

                else if (sIndex == "IncncInConveyerBeltVehicleGuaranteeStartTime")
                {
                    return m_strincncinconveyerbeltvehicleguaranteestarttime;
                }

                else if (sIndex == "IncncInConveyerBeltVehicleGuaranteeEndTime")
                {
                    return m_strincncinconveyerbeltvehicleguaranteeendtime;
                }

                else if (sIndex == "IncncInPlatformCarGuaranteeStartTime")
                {
                    return m_strincncinplatformcarguaranteestarttime;
                }

                else if (sIndex == "IncncInPlatformCarGuaranteeEndTime")
                {
                    return m_strincncinplatformcarguaranteeendtime;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeStartTime")
                {
                    return m_strincncintrailcarguaranteestarttime;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeEndTime")
                {
                    return m_strincncintrailcarguaranteeendtime;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeArriveTime")
                {
                    return m_strincncintrailcarguaranteearrivetime;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeHoldTheWheelTime")
                {
                    return m_strincncintrailcarguaranteeholdthewheeltime;
                }

                else if (sIndex == "IncncInPowerCarGuaranteeStartTime")
                {
                    return m_strincncinpowercarguaranteestarttime;
                }

                else if (sIndex == "IncncInPowerCarGuaranteeEndTime")
                {
                    return m_strincncinpowercarguaranteeendtime;
                }

                else if (sIndex == "IncncInAirSupplyCarGuaranteeStartTime")
                {
                    return m_strincncinairsupplycarguaranteestarttime;
                }

                else if (sIndex == "IncncInAirSupplyCarGuaranteeEndTime")
                {
                    return m_strincncinairsupplycarguaranteeendtime;
                }

                else if (sIndex == "IncncInAirConditionerCarGuaranteeStartTime")
                {
                    return m_strincncinairconditionercarguaranteestarttime;
                }

                else if (sIndex == "IncncInAirConditionerCarGuaranteeEndTime")
                {
                    return m_strincncinairconditionercarguaranteeendtime;
                }

                else if (sIndex == "IncncInCleanWaterCarGuaranteeStartTime")
                {
                    return m_strincncincleanwatercarguaranteestarttime;
                }

                else if (sIndex == "IncncInCleanWaterCarGuaranteeEndTime")
                {
                    return m_strincncincleanwatercarguaranteeendtime;
                }

                else if (sIndex == "IncncInSewageCarGuaranteeStartTime")
                {
                    return m_strincncinsewagecarguaranteestarttime;
                }

                else if (sIndex == "IncncInSewageCarGuaranteeEndTime")
                {
                    return m_strincncinsewagecarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutLadderCarrGuaranteeStartTime")
                {
                    return m_stroutcncoutladdercarrguaranteestarttime;
                }

                else if (sIndex == "OutcncOutLadderCarGuaranteeEndTime")
                {
                    return m_stroutcncoutladdercarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutBridgerGuaranteeStartTime")
                {
                    return m_stroutcncoutbridgerguaranteestarttime;
                }

                else if (sIndex == "OutcncOutBridgeGuaranteeEndTime")
                {
                    return m_stroutcncoutbridgeguaranteeendtime;
                }

                else if (sIndex == "OutcncOutFerryrGuaranteeStartTime")
                {
                    return m_stroutcncoutferryrguaranteestarttime;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeEndTime")
                {
                    return m_stroutcncoutferryguaranteeendtime;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeFirstOutTime")
                {
                    return m_stroutcncoutferryguaranteefirstouttime;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeFirstArriveGateTime")
                {
                    return m_stroutcncoutferryguaranteefirstarrivegatetime;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeFirstArrivePlaneTime")
                {
                    return m_stroutcncoutferryguaranteefirstarriveplanetime;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeLastOutTime")
                {
                    return m_stroutcncoutferryguaranteelastouttime;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeLastArriveGateTime")
                {
                    return m_stroutcncoutferryguaranteelastarrivegatetime;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeLastArrivePlaneTime")
                {
                    return m_stroutcncoutferryguaranteelastarriveplanetime;
                }

                else if (sIndex == "OutcncOutFerryrGuaranteeArriveTime")
                {
                    return m_stroutcncoutferryrguaranteearrivetime;
                }

                else if (sIndex == "OutcncOutCrewCarrGuaranteeStartTime")
                {
                    return m_stroutcncoutcrewcarrguaranteestarttime;
                }

                else if (sIndex == "OutcncOutCrewCarGuaranteeEndTime")
                {
                    return m_stroutcncoutcrewcarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutConveyerBeltVehicleGuaranteeStartTime")
                {
                    return m_stroutcncoutconveyerbeltvehicleguaranteestarttime;
                }

                else if (sIndex == "OutcncOutConveyerBeltVehicleGuaranteeEndTime")
                {
                    return m_stroutcncoutconveyerbeltvehicleguaranteeendtime;
                }

                else if (sIndex == "OutcncOutPlatformCarGuaranteeStartTime")
                {
                    return m_stroutcncoutplatformcarguaranteestarttime;
                }

                else if (sIndex == "OutcncOutPlatformCarGuaranteeEndTime")
                {
                    return m_stroutcncoutplatformcarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeStartTime")
                {
                    return m_stroutcncouttrailcarguaranteestarttime;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeEndTime")
                {
                    return m_stroutcncouttrailcarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeArriveTime")
                {
                    return m_stroutcncouttrailcarguaranteearrivetime;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeHoldTheWheelTime")
                {
                    return m_stroutcncouttrailcarguaranteeholdthewheeltime;
                }

                else if (sIndex == "OutcncOutPowerCarGuaranteeStartTime")
                {
                    return m_stroutcncoutpowercarguaranteestarttime;
                }

                else if (sIndex == "OutcncOutPowerCarGuaranteeEndTime")
                {
                    return m_stroutcncoutpowercarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutAirSupplyCarGuaranteeStartTime")
                {
                    return m_stroutcncoutairsupplycarguaranteestarttime;
                }

                else if (sIndex == "OutcncOutAirSupplyCarGuaranteeEndTime")
                {
                    return m_stroutcncoutairsupplycarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutAirConditionerCarGuaranteeStartTime")
                {
                    return m_stroutcncoutairconditionercarguaranteestarttime;
                }

                else if (sIndex == "OutcncOutAirConditionerCarGuaranteeEndTime")
                {
                    return m_stroutcncoutairconditionercarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutCleanWaterCarGuaranteeStartTime")
                {
                    return m_stroutcncoutcleanwatercarguaranteestarttime;
                }

                else if (sIndex == "OutcncOutCleanWaterCarGuaranteeEndTime")
                {
                    return m_stroutcncoutcleanwatercarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutSewageCarGuaranteeStartTime")
                {
                    return m_stroutcncoutsewagecarguaranteestarttime;
                }

                else if (sIndex == "OutcncOutSewageCarGuaranteeEndTime")
                {
                    return m_stroutcncoutsewagecarguaranteeendtime;
                }

                else if (sIndex == "OutcncOutFuelFillingStartTime")
                {
                    return m_stroutcncoutfuelfillingstarttime;
                }

                else if (sIndex == "OutcncOutFuelFillingEndTime")
                {
                    return m_stroutcncoutfuelfillingendtime;
                }

                else if (sIndex == "OutcncOutCabinSupplyStartTime")
                {
                    return m_stroutcncoutcabinsupplystarttime;
                }

                else if (sIndex == "OutcncOutCabinSupplyEndTime")
                {
                    return m_stroutcncoutcabinsupplyendtime;
                }

                else if (sIndex == "IncncInCargoMailBaggageGuaranteeStartTime")
                {
                    return m_strincncincargomailbaggageguaranteestarttime;
                }

                else if (sIndex == "IncncInCargoMailBaggageGuaranteeEndTime")
                {
                    return m_strincncincargomailbaggageguaranteeendtime;
                }

                else if (sIndex == "IncncInCargoCabinOpenTime")
                {
                    return m_strincncincargocabinopentime;
                }

                else if (sIndex == "IncncInCargoCabinCloseTime")
                {
                    return m_strincncincargocabinclosetime;
                }

                else if (sIndex == "IncncInBaggageUnloadStartTime")
                {
                    return m_strincncinbaggageunloadstarttime;
                }

                else if (sIndex == "IncncInBaggageUnloadEndTime")
                {
                    return m_strincncinbaggageunloadendtime;
                }

                else if (sIndex == "IncncInCargoUnloadStartTime")
                {
                    return m_strincncincargounloadstarttime;
                }

                else if (sIndex == "IncncInCargoUnloadEndTime")
                {
                    return m_strincncincargounloadendtime;
                }

                else if (sIndex == "OutcncOutCargoMailBaggageGuaranteeStartTime")
                {
                    return m_stroutcncoutcargomailbaggageguaranteestarttime;
                }

                else if (sIndex == "OutcncOutCargoMailBaggageGuaranteeEndTime")
                {
                    return m_stroutcncoutcargomailbaggageguaranteeendtime;
                }

                else if (sIndex == "OutcncOutCargoMailBaggageReportTime")
                {
                    return m_stroutcncoutcargomailbaggagereporttime;
                }

                else if (sIndex == "OutcncOutInfoPickUpBaggageTime")
                {
                    return m_stroutcncoutinfopickupbaggagetime;
                }

                else if (sIndex == "OutcncOutCargoCabinOpenTime")
                {
                    return m_stroutcncoutcargocabinopentime;
                }

                else if (sIndex == "OutcncOutCargoCabinCloseTime")
                {
                    return m_stroutcncoutcargocabinclosetime;
                }

                else if (sIndex == "OutcncOutBaggageLoadStartTime")
                {
                    return m_stroutcncoutbaggageloadstarttime;
                }

                else if (sIndex == "OutcncOutBaggageLoadEndTime")
                {
                    return m_stroutcncoutbaggageloadendtime;
                }

                else if (sIndex == "OutcncOutCargoLoadStartTime")
                {
                    return m_stroutcncoutcargoloadstarttime;
                }

                else if (sIndex == "OutcncOutCargoLoadEndTime")
                {
                    return m_stroutcncoutcargoloadendtime;
                }

                else if (sIndex == "IncncInPilotBrakeTime")
                {
                    return m_strincncinpilotbraketime;
                }

                else if (sIndex == "OutcncOutPilotArriveTime")
                {
                    return m_stroutcncoutpilotarrivetime;
                }

                else if (sIndex == "OutcncOutPilotLooseSkidsTime")
                {
                    return m_stroutcncoutpilotlooseskidstime;
                }

                else if (sIndex == "OutcncOutPilotPushOffTime")
                {
                    return m_stroutcncoutpilotpushofftime;
                }

                else if (sIndex == "IncncInCabinOpenTime")
                {
                    return m_strincncincabinopentime;
                }

                else if (sIndex == "IncncInCabinCloseTime")
                {
                    return m_strincncincabinclosetime;
                }

                else if (sIndex == "OutcncOutCabinOpenTime")
                {
                    return m_stroutcncoutcabinopentime;
                }

                else if (sIndex == "OutcncOutCabinCloseTime")
                {
                    return m_stroutcncoutcabinclosetime;
                }

                else if (sIndex == "OutcncOutStewardArriveTime")
                {
                    return m_stroutcncoutstewardarrivetime;
                }

                else if (sIndex == "OutcncOutCheckInGuaranteeStartTime")
                {
                    return m_stroutcncoutcheckinguaranteestarttime;
                }

                else if (sIndex == "OutcncOutCheckInGuaranteeEndTime")
                {
                    return m_stroutcncoutcheckinguaranteeendtime;
                }

                else if (sIndex == "IncncInCabinCleanStartTime")
                {
                    return m_strincncincabincleanstarttime;
                }

                else if (sIndex == "IncncInCabinCleanEndTime")
                {
                    return m_strincncincabincleanendtime;
                }

                else if (sIndex == "OutcncOutCabinCleanStartTime")
                {
                    return m_stroutcncoutcabincleanstarttime;
                }

                else if (sIndex == "OutcncOutCabinCleanEndTime")
                {
                    return m_stroutcncoutcabincleanendtime;
                }

                else if (sIndex == "IncncInGuestOutStartTime")
                {
                    return m_strincncinguestoutstarttime;
                }

                else if (sIndex == "IncncInGuestOutEndTime")
                {
                    return m_strincncinguestoutendtime;
                }

                else if (sIndex == "IncncInGuiderArriveTime")
                {
                    return m_strincncinguiderarrivetime;
                }

                else if (sIndex == "IncncInGuiderGuaranteeStartTime")
                {
                    return m_strincncinguiderguaranteestarttime;
                }

                else if (sIndex == "IncncInGuiderGuaranteeEndTime")
                {
                    return m_strincncinguiderguaranteeendtime;
                }

                else if (sIndex == "OutcncOutGuestInStartTime")
                {
                    return m_stroutcncoutguestinstarttime;
                }

                else if (sIndex == "OutcncOutGuestInEndTime")
                {
                    return m_stroutcncoutguestinendtime;
                }

                else if (sIndex == "OutcncOutGuiderArriveTime")
                {
                    return m_stroutcncoutguiderarrivetime;
                }

                else if (sIndex == "OutcncOutGuiderGuaranteeStartTime")
                {
                    return m_stroutcncoutguiderguaranteestarttime;
                }

                else if (sIndex == "OutcncOutGuiderGuaranteeEndTime")
                {
                    return m_stroutcncoutguiderguaranteeendtime;
                }

                else if (sIndex == "OutcncOutInformBoardTime")
                {
                    return m_stroutcncoutinformboardtime;
                }

                else if (sIndex == "OutcncOutBoardingGateCloseTime")
                {
                    return m_stroutcncoutboardinggateclosetime;
                }

                else if (sIndex == "OutcncOutLoadsheetUploadFinishedTime")
                {
                    return m_stroutcncoutloadsheetuploadfinishedtime;
                }

                else if (sIndex == "OutcncOutCaptainConfirmTime")
                {
                    return m_stroutcncoutcaptainconfirmtime;
                }

                else if (sIndex == "IncncInVipPickUpGuaranteeStartTime")
                {
                    return m_strincncinvippickupguaranteestarttime;
                }

                else if (sIndex == "IncncInVipPickUpGuaranteeEndTime")
                {
                    return m_strincncinvippickupguaranteeendtime;
                }

                else if (sIndex == "OutcncOutVipSeeOffGuaranteeStartTime")
                {
                    return m_stroutcncoutvipseeoffguaranteestarttime;
                }

                else if (sIndex == "OutcncOutVipSeeOffGuaranteeEndTime")
                {
                    return m_stroutcncoutvipseeoffguaranteeendtime;
                }

                else if (sIndex == "OutcncOutCustomGuaranteeStartTime")
                {
                    return m_stroutcncoutcustomguaranteestarttime;
                }

                else if (sIndex == "OutcncOutCustomGuaranteeEndTime")
                {
                    return m_stroutcncoutcustomguaranteeendtime;
                }

                else if (sIndex == "OutcncOutFrontierInspectionGuaranteeStartTime")
                {
                    return m_stroutcncoutfrontierinspectionguaranteestarttime;
                }

                else if (sIndex == "OutcncOutFrontierInspectionGuaranteeEndTime")
                {
                    return m_stroutcncoutfrontierinspectionguaranteeendtime;
                }

                else if (sIndex == "OutcncOutQuarantineGuaranteeStartTime")
                {
                    return m_stroutcncoutquarantineguaranteestarttime;
                }

                else if (sIndex == "OutcncOutQuarantineGuaranteeEndTime")
                {
                    return m_stroutcncoutquarantineguaranteeendtime;
                }


                else if (sIndex == "IncnvcInGuaranteeRecord")
                {
                    return m_strincnvcinguaranteerecord;
                }
                else if (sIndex == "OutcnvcOutGuaranteeRecord")
                {
                    return m_stroutcnvcoutguaranteerecord;
                }
                #endregion 三期 added in 20140514

                #region 三期 added in 201504
                else if (sIndex == "OutcnvcOutRunway")
                {
                    return _OutcnvcOutRunway;
                }
                else if (sIndex == "IncnvcInRunway")
                {
                    return _IncnvcInRunway;
                }
                else if (sIndex == "IncncInEXIT")
                {
                    return _IncncInEXIT;
                }
                else if (sIndex == "OutcncOutTOBT")
                {
                    return _OutcncOutTOBT;
                }
                else if (sIndex == "OutcncOutBoardStartTime")
                {
                    return _OutcncOutBoardStartTime;
                }
                else if (sIndex == "OutcncOutBoardEndTime")
                {
                    return _OutcncOutBoardEndTime;
                }
                else if (sIndex == "OutcncOutCheckCounterEndTime")
                {
                    return _OutcncOutCheckCounterEndTime;
                }
                else if (sIndex == "OutcnvcOutDeicePing")
                {
                    return _OutcnvcOutDeicePing;
                }
                else if (sIndex == "OutcnvcOutDeiceWei")
                {
                    return _OutcnvcOutDeiceWei;
                }
                else if (sIndex == "OutcncOutArriveDeicePing")
                {
                    return _OutcncOutArriveDeicePing;
                }
                else if (sIndex == "OutcncOutDeiceStartTime")
                {
                    return _OutcncOutDeiceStartTime;
                }
                else if (sIndex == "OutcncOutDeiceEndTime")
                {
                    return _OutcncOutDeiceEndTime;
                }
                else if (sIndex == "OutcncOutLeaveDeicePing")
                {
                    return _OutcncOutLeaveDeicePing;
                }
                else if (sIndex == "OutcnvcOutSlowDeiceFlag")
                {
                    return _OutcnvcOutSlowDeiceFlag;
                }
                else if (sIndex == "OutcncOutTSAT")
                {
                    return _OutcncOutTSAT;
                }
                else if (sIndex == "OutcncOutCTOT")
                {
                    return _OutcncOutCTOT;
                }
                #endregion 三期 added in 201504

                #region 三期 added in 20150428
                else if (sIndex == "OutcncOutFirstPassengerComeInCabinTime")
                {
                    return _OutcncOutFirstPassengerComeInCabinTime;
                }
                else if (sIndex == "OutcncOutPlaneReadyEndTime")
                {
                    return _OutcncOutPlaneReadyEndTime;
                }
                #endregion 三期 added in 20150428

                #region 三期 added in 20150624
                else if (sIndex == "IncnvcInGATE_Test")
                {
                    return _IncnvcInGATE_Test;
                }
                else if (sIndex == "OutcnvcOutGate_Test")
                {
                    return _OutcnvcOutGate_Test;
                }
                else if (sIndex == "OutcncOutFlightInterceptTime")
                {
                    return _OutcncOutFlightInterceptTime;
                }
                else if (sIndex == "IncnvcInTurnTableNO")
                {
                    return _IncnvcInTurnTableNO;
                }
                else if (sIndex == "OutcnvcOutTurnTableNO")
                {
                    return _OutcnvcOutTurnTableNO;
                }
                #endregion 三期 added in 20150624

                #region 三期 added in 20150730
                else if (sIndex == "OutcncOutCrewStartTime")
                {
                    return _OutcncOutCrewStartTime;
                }
                else if (sIndex == "OutcncOutCrewArriveTime")
                {
                    return _OutcncOutCrewArriveTime;
                }
                else if (sIndex == "OutcnvcOutOverStationType")
                {
                    return _OutcnvcOutOverStationType;
                }
                #endregion 三期 added in 20150730

                #region 三期 added in 20160323
                else if (sIndex == "OutcnvcOutSpotUsers")
                {
                    return _OutcnvcOutSpotUsers;
                }

                else if (sIndex == "OutcnvcOutMaintenanceUsers")
                {
                    return _OutcnvcOutMaintenanceUsers;
                }

                else if (sIndex == "OutcnvcOutFleetUsers")
                {
                    return _OutcnvcOutFleetUsers;
                }

                else if (sIndex == "OutcnvcOutVIPRoomUsers")
                {
                    return _OutcnvcOutVIPRoomUsers;
                }

                else if (sIndex == "OutcnvcOutAviationDietUsers")
                {
                    return _OutcnvcOutAviationDietUsers;
                }

                else if (sIndex == "OutcnvcOutCheckInUsers")
                {
                    return _OutcnvcOutCheckInUsers;
                }

                else if (sIndex == "OutcnvcOutCleaningTeamUsers")
                {
                    return _OutcnvcOutCleaningTeamUsers;
                }

                else if (sIndex == "IncnvcInSpotUsers")
                {
                    return _IncnvcInSpotUsers;
                }

                else if (sIndex == "IncnvcInMaintenanceUsers")
                {
                    return _IncnvcInMaintenanceUsers;
                }

                else if (sIndex == "IncnvcInFleetUsers")
                {
                    return _IncnvcInFleetUsers;
                }

                else if (sIndex == "IncnvcInVIPRoomUsers")
                {
                    return _IncnvcInVIPRoomUsers;
                }

                else if (sIndex == "IncnvcInAviationDietUsers")
                {
                    return _IncnvcInAviationDietUsers;
                }

                else if (sIndex == "IncnvcInCheckInUsers")
                {
                    return _IncnvcInCheckInUsers;
                }

                else if (sIndex == "IncnvcInCleaningTeamUsers")
                {
                    return _IncnvcInCleaningTeamUsers;
                }

                #endregion 三期 added in 20160323

                #region 三期 added in 20160503
                else if (sIndex == "OutcncSTA")
                {
                    return _OutcncSTA;
                }

                else if (sIndex == "OutcncETA")
                {
                    return _OutcncETA;
                }

                else if (sIndex == "OutcncTDWN")
                {
                    return _OutcncTDWN;
                }

                else if (sIndex == "OutcncATA")
                {
                    return _OutcncATA;
                }
                #endregion 三期 added in 20160503

                #region 三期 added in 20160527
                else if (sIndex == "IncniInConveyerBeltVehicleGuaranteeCount")
                {
                    return _IncniInConveyerBeltVehicleGuaranteeCount;
                }

                else if (sIndex == "OutcniOutConveyerBeltVehicleGuaranteeCount")
                {
                    return _OutcniOutConveyerBeltVehicleGuaranteeCount;
                }

                else if (sIndex == "IncniInBaggageCarGuaranteeCount")
                {
                    return _IncniInBaggageCarGuaranteeCount;
                }

                else if (sIndex == "OutcniOutBaggageCarGuaranteeCount")
                {
                    return _OutcniOutBaggageCarGuaranteeCount;
                }

                else if (sIndex == "IncniInCrewCarGuaranteeCount")
                {
                    return _IncniInCrewCarGuaranteeCount;
                }

                else if (sIndex == "OutcniOutCrewCarGuaranteeCount")
                {
                    return _OutcniOutCrewCarGuaranteeCount;
                }

                else if (sIndex == "IncniInPlatformCarGuaranteeCount")
                {
                    return _IncniInPlatformCarGuaranteeCount;
                }

                else if (sIndex == "OutcniOutPlatformCarGuaranteeCount")
                {
                    return _OutcniOutPlatformCarGuaranteeCount;
                }

                else if (sIndex == "IncniInLadderCarGuaranteeCount")
                {
                    return _IncniInLadderCarGuaranteeCount;
                }

                else if (sIndex == "OutcniOutLadderCarGuaranteeCount")
                {
                    return _OutcniOutLadderCarGuaranteeCount;
                }

                else if (sIndex == "IncniInFerryGuaranteeCount")
                {
                    return _IncniInFerryGuaranteeCount;
                }

                else if (sIndex == "OutcniOutFerryGuaranteeCount")
                {
                    return _OutcniOutFerryGuaranteeCount;
                }

                else if (sIndex == "OutcniOutDeicingVehicleGuaranteeCount")
                {
                    return _OutcniOutDeicingVehicleGuaranteeCount;
                }

                else if (sIndex == "OutcniOutTrailCarGuaranteeCount")
                {
                    return _OutcniOutTrailCarGuaranteeCount;
                }

                else if (sIndex == "OutcnvcOutRemark_Fleet")
                {
                    return _OutcnvcOutRemark_Fleet;
                }

                else if (sIndex == "IncnvcInRemark_Fleet")
                {
                    return _IncnvcInRemark_Fleet;
                }

                #endregion 三期 added in 20160527

                #region 三期 added in 20160616
                else if (sIndex == "OutcnvcOutArrangeCargoOperator")
                {
                    return _OutcnvcOutArrangeCargoOperator;
                }

                else if (sIndex == "OutcncOutArrangeCargoOperateTime")
                {
                    return _OutcncOutArrangeCargoOperateTime;
                }

                else if (sIndex == "OutcnvcOutRecheckLoadSheetOperator")
                {
                    return _OutcnvcOutRecheckLoadSheetOperator;
                }

                else if (sIndex == "OutcncOutRecheckLoadSheetOperateTime")
                {
                    return _OutcncOutRecheckLoadSheetOperateTime;
                }

                else if (sIndex == "OutcnvcOutConfirmLoadSheetOperator")
                {
                    return _OutcnvcOutConfirmLoadSheetOperator;
                }

                else if (sIndex == "OutcncOutConfirmLoadSheetOperateTime")
                {
                    return _OutcncOutConfirmLoadSheetOperateTime;
                }

                else if (sIndex == "OutcnvcOutConfirmLoadSheetOperateVerNumber")
                {
                    return _OutcnvcOutConfirmLoadSheetOperateVerNumber;
                }

                else if (sIndex == "OutcnvcOutCheckManifestOperator")
                {
                    return _OutcnvcOutCheckManifestOperator;
                }

                else if (sIndex == "OutcncOutCheckManifestOperateTime")
                {
                    return _OutcncOutCheckManifestOperateTime;
                }

                else if (sIndex == "OutcnvcOutRecheckManifestOperator")
                {
                    return _OutcnvcOutRecheckManifestOperator;
                }

                else if (sIndex == "OutcncOutRecheckManifestOperateTime")
                {
                    return _OutcncOutRecheckManifestOperateTime;
                }

                else if (sIndex == "OutcnvcOutUploadManifestOperator")
                {
                    return _OutcnvcOutUploadManifestOperator;
                }

                else if (sIndex == "OutcncOutUploadManifestOperateTime")
                {
                    return _OutcncOutUploadManifestOperateTime;
                }

                else if (sIndex == "OutcnvcOutUploadManifestOperateResult")
                {
                    return _OutcnvcOutUploadManifestOperateResult;
                }

                else if (sIndex == "OutcnvcOutCrewConfirmManifestOperateResult")
                {
                    return _OutcnvcOutCrewConfirmManifestOperateResult;
                }

                else if (sIndex == "OutcncOutCrewConfirmManifestOperateTime")
                {
                    return _OutcncOutCrewConfirmManifestOperateTime;
                }

                else if (sIndex == "OutcnvcOutTransportManifestOperator")
                {
                    return _OutcnvcOutTransportManifestOperator;
                }

                else if (sIndex == "OutcncOutTransportManifestOperateTime")
                {
                    return _OutcncOutTransportManifestOperateTime;
                }

                else if (sIndex == "OutcnvcOutRemark_Balance")
                {
                    return _OutcnvcOutRemark_Balance;
                }
                #endregion 三期 added in 20160616

                #region 三期 added in 20160704
                else if (sIndex == "IncnvcInLeaders")
                {
                    return _IncnvcInLeaders;
                }

                else if (sIndex == "OutcnvcOutLeaders")
                {
                    return _OutcnvcOutLeaders;
                }
                #endregion 三期 added in 20160704

                else
                {
                    return "";
                }
            }

            set
            {
                #region 以往
                if (sIndex == "IncncDATOP")
                {
                    m_strInDATOP = value;
                }

                else if (sIndex == "IncnvcFLTID")
                {
                    m_strInFLTID = value;
                }

                else if (sIndex == "IncniLEGNO")
                {
                    m_strInLEGNO = value;
                }

                else if (sIndex == "IncnvcAC")
                {
                    m_strInAC = value;
                }

                else if (sIndex == "IncncAllSTA")
                {
                    m_strInAllSTA = value;
                }


                else if (sIndex == "IncncAllETA")
                {
                    m_strInAllETA = value;
                }

                else if (sIndex == "IncncAllTDWN")
                {
                    m_strInAllTDWN = value;
                }

                else if (sIndex == "IncniAllViewIndex")
                {
                    m_strInAllViewIndex = value;
                }

                else if (sIndex == "IncncAllATA")
                {
                    m_strInAllATA = value;
                }

                else if (sIndex == "IncncAllStatus")
                {
                    m_strInAllStatus = value;
                }

                else if (sIndex == "IncnvcLONG_REG")
                {
                    m_strInLONG_REG = value;
                }
                else if (sIndex == "IncncFlightDate")
                {
                    m_strInFlightDate = value;
                }
                else if (sIndex == "IncnvcFlightNo")
                {
                    m_strInFlightNo = value;
                }
                else if (sIndex == "IncncACTYP")
                {
                    m_strInACTYP = value;
                }
                else if (sIndex == "IncnvcFlightCharacterAbbreviate")
                {
                    m_strInFlightCharacterAbbreviate = value;
                }
                else if (sIndex == "IncncDEPAirportCNAME")
                {
                    m_strInDEPAirportCNAME = value;
                }
                else if (sIndex == "IncncARRAirportCNAME")
                {
                    m_strInARRAirportCNAME = value;
                }
                else if (sIndex == "IncncSTA")
                {
                    m_strInSTA = value;
                }
                else if (sIndex == "IncncETA")
                {
                    m_strInETA = value;
                }
                else if (sIndex == "IncncTDWN")
                {
                    m_strInTDWN = value;
                }
                else if (sIndex == "IncncACARSTOFF")
                {
                    m_strInACARSTOFF = value;
                }
                else if (sIndex == "IncncACARSTDWN")
                {
                    m_strInACARSTDWN = value;
                }
                else if (sIndex == "IncncACARSATA")
                {
                    m_strInACARSATA = value;
                }
                else if (sIndex == "IncnvcInDelayName")
                {
                    m_strInInDelayName = value;
                }
                else if (sIndex == "IncnvcFlightDelayName")
                {
                    m_strInFlightDelayName = value;
                }
                else if (sIndex == "IncnvcDiversionDelayName")
                {
                    m_strInDiversionDelayName = value;
                }
                else if (sIndex == "IncnvcInCarInfor")
                {
                    m_strInInCarInfor = value;
                }
                else if (sIndex == "IncncInCarDepTime")
                {
                    m_strInInCarDepTime = value;
                }
                else if (sIndex == "IncncInCarArrTime")
                {
                    m_strInInCarArrTime = value;
                }
                else if (sIndex == "IncnbVIPTag")
                {
                    m_strInVIPTag = value;
                }
                else if (sIndex == "IncniCheckNum")
                {
                    m_strInCheckNum = value;
                }
                else if (sIndex == "IncnbTransitPaxTag")
                {
                    m_strInTransitPaxTag = value;
                }
                else if (sIndex == "IncniBaggageWeight")
                {
                    m_strInBaggageWeight = value;
                }
                else if (sIndex == "IncniCargoWeight")
                {
                    m_strInCargoWeight = value;
                }
                else if (sIndex == "IncncStatusName")
                {
                    m_strInStatusName = value;
                }
                else if (sIndex == "IncnvcInRemark")
                {
                    m_strInInRemark = value;
                }
                else if (sIndex == "IncnvcInAircraftStatus")
                {
                    m_strInInAircraftStatus = value;
                }
                else if (sIndex == "IncnvcInGATE")
                {
                    m_strInInGATE = value;
                }
                else if (sIndex == "OutcncDATOP")
                {
                    m_strOutDATOP = value;
                }

                else if (sIndex == "OutcnvcFLTID")
                {
                    m_strOutFLTID = value;
                }

                else if (sIndex == "OutcniLEGNO")
                {
                    m_strOutLEGNO = value;
                }

                else if (sIndex == "OutcnvcAC")
                {
                    m_strOutAC = value;
                }

                else if (sIndex == "OutcncAllSTD")
                {
                    m_strOutAllSTD = value;
                }


                else if (sIndex == "OutcncAllETD")
                {
                    m_strOutAllETD = value;
                }

                else if (sIndex == "OutcncAllTOFF")
                {
                    m_strOutAllTOFF = value;
                }

                else if (sIndex == "OutcniAllViewIndex")
                {
                    m_strOutAllViewIndex = value;
                }

                else if (sIndex == "OutcncAllATD")
                {
                    m_strOutAllATD = value;
                }

                else if (sIndex == "OutcncAllStatus")
                {
                    m_strOutAllStatus = value;
                }
                else if (sIndex == "OutcnvcOutGate")
                {
                    m_strOutOutGate = value;
                }
                else if (sIndex == "OutcnvcLONG_REG")
                {
                    m_strOutLONG_REG = value;
                }
                else if (sIndex == "OutcncFlightDate")
                {
                    m_strOutFlightDate = value;
                }
                else if (sIndex == "OutcnvcFlightNo")
                {
                    m_strOutFlightNo = value;
                }
                else if (sIndex == "OutcnvcFlightCharacterAbbreviate")
                {
                    m_strOutFlightCharacterAbbreviate = value;
                }
                else if (sIndex == "OutcncDEPAirportCNAME")
                {
                    m_strOutDEPAirportCNAME = value;
                }
                else if (sIndex == "OutcncARRAirportCNAME")
                {
                    m_strOutARRAirportCNAME = value;
                }
                else if (sIndex == "OutcncSTD")
                {
                    m_strOutSTD = value;
                }
                else if (sIndex == "OutcncETD")
                {
                    m_strOutETD = value;
                }
                else if (sIndex == "OutcncStartGuaranteeTime")
                {
                    m_strOutStartGuaranteeTime = value;
                }
                else if (sIndex == "OutcniIntermissionTime")
                {
                    m_strOutIntermissionTime = value;
                }
                else if (sIndex == "OutcnvcJoinFlight")
                {
                    m_strOutJoinFlight = value;
                }
                else if (sIndex == "OutcnvcCheckCounter")
                {
                    m_strOutCheckCounter = value;
                }
                else if (sIndex == "OutcnvcWaitHall")
                {
                    m_strOutWaitHall = value;
                }
                else if (sIndex == "OutcnvcBoradingGate")
                {
                    m_strOutBoradingGate = value;
                }
                else if (sIndex == "OutcncOpenCabinTime")
                {
                    m_strOutOpenCabinTime = value;
                }
                else if (sIndex == "OutcnvcOutCarInfor")
                {
                    m_strOutOutCarInfor = value;
                }
                else if (sIndex == "OutcncOutCarDepTime")
                {
                    m_strOutOutCarDepTime = value;
                }
                else if (sIndex == "OutcncOutCarArrTime")
                {
                    m_strOutOutCarArrTime = value;
                }
                else if (sIndex == "OutcncPilotArrTime")
                {
                    m_strOutPilotArrTime = value;
                }
                else if (sIndex == "OutcncStewardArrTime")
                {
                    m_strOutStewardArrTime = value;
                }
                else if (sIndex == "OutcncDeiceStartTime")
                {
                    m_strOutDeiceStartTime = value;
                }
                else if (sIndex == "OutcncDeiceEndTime")
                {
                    m_strOutDeiceEndTime = value;
                }
                else if (sIndex == "OutcncTaskSheetChangeTime")
                {
                    m_strOutTaskSheetChangeTime = value;
                }
                else if (sIndex == "OutcncDispatchTime")
                {
                    m_strOutDispatchTime = value;
                }
                else if (sIndex == "OutcncDispatchPrintTime")
                {
                    m_strOutDispatchPrintTime = value;
                }
                else if (sIndex == "OutcncOutMCCReadyTime")
                {
                    m_strOutOutMCCReadyTime = value;
                }
                else if (sIndex == "IncncInMCCReadyTime")
                {
                    m_strInInMCCReadyTime = value;
                }
                else if (sIndex == "IncncInTime")
                {
                    m_strInInTime = value;
                }                
                else if (sIndex == "OutcncMCCReleaseTime")
                {
                    m_strOutMCCReleaseTime = value;
                }
                else if (sIndex == "OutcncOutTime")
                {
                    m_strOutOutTime = value;
                }
                else if (sIndex == "OutcnvcAircraftStatus")
                {
                    m_strOutAircraftStatus = value;
                }
                else if (sIndex == "OutcncDeplaneTime")
                {
                    m_strOutDeplaneTime = value;
                }
                else if (sIndex == "OutcncCleanStartTime")
                {
                    m_strOutCleanStartTime = value;
                }
                else if (sIndex == "OutcncCleanEndTime")
                {
                    m_strOutCleanEndTime = value;
                }
                else if (sIndex == "OutcncSewageStartTime")
                {
                    m_strOutSewageStartTime = value;
                }
                else if (sIndex == "OutcncSewageEndTime")
                {
                    m_strOutSewageEndTime = value;
                }
                else if (sIndex == "OutcncCabinSupplyStartTime")
                {
                    m_strOutCabinSupplyStartTime = value;
                }
                else if (sIndex == "OutcncCabinSupplyEndTime")
                {
                    m_strOutCabinSupplyEndTime = value;
                }
                else if (sIndex == "OutcncOilStartTime")
                {
                    m_strOutOilStartTime = value;
                }
                else if (sIndex == "OutcncOilEndTime")
                {
                    m_strOutOilEndTime = value;
                }
                else if (sIndex == "OutcncOpenCargoCabinTime")
                {
                    m_strOutOpenCargoCabinTime = value;
                }
                else if (sIndex == "OutcncCargoStartTime")
                {
                    m_strOutCargoStartTime = value;
                }
                else if (sIndex == "OutcncBaggageTime")
                {
                    m_strOutBaggageTime = value;
                }
                else if (sIndex == "OutcncCloseCargoCabinTime")
                {
                    m_strOutCloseCargoCabinTime = value;
                }
                else if (sIndex == "OutcncTrailerArrTime")
                {
                    m_strOutTrailerArrTime = value;
                }
                else if (sIndex == "OutcncDirtyWaterCarArrTime")
                {
                    m_strOutDirtyWaterCarArrTime = value;
                }
                else if (sIndex == "OutcncFerryDepTime")
                {
                    m_strOutFerryDepTime = value;
                }
                else if (sIndex == "OutcncLastFerryArrTime")
                {
                    m_strOutLastFerryArrTime = value;
                }
                else if (sIndex == "OutcncDragPoleArrTime")
                {
                    m_strOutDragPoleArrTime = value;
                }
                else if (sIndex == "OutcncLadderCarArrTime")
                {
                    m_strOutLadderCarArrTime = value;
                }
                else if (sIndex == "OutcncInformBoardTime")
                {
                    m_strOutInformBoardTime = value;
                }
                else if (sIndex == "OutcncBoardTime")
                {
                    m_strOutBoardTime = value;
                }
                else if (sIndex == "OutcnvcBookNum")
                {
                    m_strOutBookNum = value;
                }
                else if (sIndex == "OutcniCheckNum")
                {
                    m_strOutCheckNum = value;
                }
                else if (sIndex == "OutcnbTransitPaxTag")
                {
                    m_strOutTransitPaxTag = value;
                }
                else if (sIndex == "OutcniXCRNum")
                {
                    m_strOutXCRNum = value;
                }
                else if (sIndex == "OutcniAdultNum")
                {
                    m_strOutAdultNum = value;
                }
                else if (sIndex == "OutcniChildNum")
                {
                    m_strOutChildNum = value;
                }
                else if (sIndex == "OutcniInfantNum")
                {
                    m_strOutInfantNum = value;
                }
                else if (sIndex == "OutcniFirstClassNum")
                {
                    m_strOutFirstClassNum = value;
                }
                else if (sIndex == "OutcniOfficialClassNum")
                {
                    m_strOutOfficialClassNum = value;
                }
                else if (sIndex == "OutcniTouristClassNum")
                {
                    m_strOutTouristClassNum = value;
                }
                else if (sIndex == "OutcniAscendingPaxNum")
                {
                    m_strOutAscendingPaxNum = value;
                }
                else if (sIndex == "OutcntPaxNameList")
                {
                    m_strOutPaxNameList = value;
                }
                else if (sIndex == "OutcncBoardOverTime")
                {
                    m_strOutBoardOverTime = value;
                }
                else if (sIndex == "OutcncLoadSheetArrTime")
                {
                    m_strOutLoadSheetArrTime = value;
                }
                else if (sIndex == "OutcncClosePaxCabinTime")
                {
                    m_strOutClosePaxCabinTime = value;
                }
                else if (sIndex == "OutcniOpenTime")
                {
                    m_strOutOpenTime = value;
                }
                else if (sIndex == "OutcncInternalGuestTogether")
                {
                    m_strOutInternalGuestTogether = value;
                }
                else if (sIndex == "OutcncPushTime")
                {
                    m_strOutPushTime = value;
                }
                else if (sIndex == "OutcncTOFF")
                {
                    m_strOutTOFF = value;
                }
                else if (sIndex == "OutcncACARSOUT")
                {
                    m_strOutACARSOUT = value;
                }
                else if (sIndex == "OutcncACARSTOFF")
                {
                    m_strOutACARSTOFF = value;
                }
                else if (sIndex == "OutcnvcDisChargingDelName")
                {
                    m_strOutDisChargingDelName = value;
                }
                else if (sIndex == "OutcnvcOutDelayName")
                {
                    m_strOutOutDelayName = value;
                }
                else if (sIndex == "OutcnvcFlightDelayName")
                {
                    m_strOutFlightDelayName = value;
                }
                else if (sIndex == "OutcnvcDiversionDelayName")
                {
                    m_strOutDiversionDelayName = value;
                }
                else if (sIndex == "OutcniCargoWeight")
                {
                    m_strOutCargoWeight = value;
                }
                else if (sIndex == "OutcniMailWeight")
                {
                    m_strOutMailWeight = value;
                }
                else if (sIndex == "OutcniBaggageWeight")
                {
                    m_strOutBaggageWeight = value;
                }
                else if (sIndex == "OutcniAirMaterialWeight")
                {
                    m_strOutAirMaterialWeight = value;
                }
                else if (sIndex == "OutcnvcAirMaterialRemark")
                {
                    m_strOutAirMaterialRemark = value;
                }
                else if (sIndex == "OutcniBaggageNum")
                {
                    m_strOutBaggageNum = value;
                }
                else if (sIndex == "OutcnbVIPTag")
                {
                    m_strOutVIPTag = value;
                }
                else if (sIndex == "OutcniTotalFuelWeight")
                {
                    m_strOutTotalFuelWeight = value;
                }
                else if (sIndex == "OutcniTripFuelWeight")
                {
                    m_strOutTripFuelWeight = value;
                }
                else if (sIndex == "OutcniTaxiFuelWeight")
                {
                    m_strOutTaxiFuelWeight = value;
                }
                else if (sIndex == "OutcniUnloadCargoWeight")
                {
                    m_strOutUnloadCargoWeight = value;
                }
                else if (sIndex == "OutcnvcCargoRemark")
                {
                    m_strOutCargoRemark = value;
                }
                else if (sIndex == "OutcncStatusName")
                {
                    m_strOutStatusName = value;
                }
                else if (sIndex == "OutcnvcOutRemark")
                {
                    m_strOutOutRemark = value;
                }
                else if (sIndex == "OutcnvcChiefController")
                {
                    m_strOutChiefController = value;
                }
                else if (sIndex == "OutcnvcAssistantController")
                {
                    m_strOutAssistantController = value;
                }
                else if (sIndex == "OutcnvcOutFieldController")
                {
                    m_strOutOutFieldController = value;
                }
                else if (sIndex == "OutcnvcBalanceController")
                {
                    m_strOutBalanceController = value;
                }
                else if (sIndex == "OutcniDischargingDelayTime")
                {
                    m_strOutDischargingDelayTime = value;
                }
                else if (sIndex == "OutcniDUR1")
                {
                    m_strOutcniDUR1 = value;
                }
                else if (sIndex == "OutcniFocusTag")
                {
                    m_strOutFocusTag = value;
                }
                else if (sIndex == "OutcnvcArrangedTask")
                {
                    m_strOutArrangedTask = value;
                }
                #endregion 以往

                #region added in 20101113
                else if (sIndex == "IncncInMCCTechEngr")
                {
                    m_strInInMCCTechEngr = value;
                }
                else if (sIndex == "IncnvcInArrangedTask")
                {
                    m_strInInArrangedTask = value;
                }
                else if (sIndex == "IncnvcInSMCRemark")
                {
                    m_strInInSMCRemark = value;
                }
                else if (sIndex == "OutcnvcOutSMCRemark")
                {
                    m_strOutOutSMCRemark = value;
                }

                #endregion added in 20101113

                #region added in 20111031
                else if (sIndex == "IncncInMCCReleaseTime")
                {
                    m_strInMCCReleaseTime = value;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoYK")
                {
                    m_strOutWAOperationInfoYK = value;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoFX")
                {
                    m_strOutWAOperationInfoFX = value;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoKF")
                {
                    m_strOutWAOperationInfoKF = value;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoGC")
                {
                    m_strOutWAOperationInfoGC = value;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoAJ")
                {
                    m_strOutWAOperationInfoAJ = value;
                }
                else if (sIndex == "OutcnvcOutWAOperationInfoSC")
                {
                    m_strOutWAOperationInfoSC = value;
                }
                #endregion added in 20111031

                #region added in 20121113
                else if (sIndex == "OutcncCDMeOutTime")
                {
                    m_strOutcncCDMeOutTime = value;
                }
                else if (sIndex == "OutcncCDMeOffTime")
                {
                    m_strOutcncCDMeOffTime = value;
                }
                else if (sIndex == "OutcncCDMvStartTime")
                {
                    m_strOutcncCDMvStartTime = value;
                }
                #endregion added in 20121113

                #region 三期 added in 20140514
                else if (sIndex == "IncncInLadderCarGuaranteeStartTime")
                {
                    m_strincncinladdercarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInLadderCarGuaranteeEndTime")
                {
                    m_strincncinladdercarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInBridgeGuaranteeStartTime")
                {
                    m_strincncinbridgeguaranteestarttime = value;
                }

                else if (sIndex == "IncncInBridgeGuaranteeEndTime")
                {
                    m_strincncinbridgeguaranteeendtime = value;
                }

                else if (sIndex == "IncncInFerryrGuaranteeStartTime")
                {
                    m_strincncinferryrguaranteestarttime = value;
                }

                else if (sIndex == "IncncInFerryGuaranteeEndTime")
                {
                    m_strincncinferryguaranteeendtime = value;
                }

                else if (sIndex == "IncncInFerryGuaranteeFirstArrivePlaneTime")
                {
                    m_strincncinferryguaranteefirstarriveplanetime = value;
                }

                else if (sIndex == "IncncInFerryGuaranteeLastArrivePlaneTime")
                {
                    m_strincncinferryguaranteelastarriveplanetime = value;
                }

                else if (sIndex == "IncncInFerryrGuaranteeArriveTime")
                {
                    m_strincncinferryrguaranteearrivetime = value;
                }

                else if (sIndex == "IncncInCrewCarrGuaranteeStartTime")
                {
                    m_strincncincrewcarrguaranteestarttime = value;
                }

                else if (sIndex == "IncncInCrewCarGuaranteeEndTime")
                {
                    m_strincncincrewcarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInConveyerBeltVehicleGuaranteeStartTime")
                {
                    m_strincncinconveyerbeltvehicleguaranteestarttime = value;
                }

                else if (sIndex == "IncncInConveyerBeltVehicleGuaranteeEndTime")
                {
                    m_strincncinconveyerbeltvehicleguaranteeendtime = value;
                }

                else if (sIndex == "IncncInPlatformCarGuaranteeStartTime")
                {
                    m_strincncinplatformcarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInPlatformCarGuaranteeEndTime")
                {
                    m_strincncinplatformcarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeStartTime")
                {
                    m_strincncintrailcarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeEndTime")
                {
                    m_strincncintrailcarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeArriveTime")
                {
                    m_strincncintrailcarguaranteearrivetime = value;
                }

                else if (sIndex == "IncncInTrailCarGuaranteeHoldTheWheelTime")
                {
                    m_strincncintrailcarguaranteeholdthewheeltime = value;
                }

                else if (sIndex == "IncncInPowerCarGuaranteeStartTime")
                {
                    m_strincncinpowercarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInPowerCarGuaranteeEndTime")
                {
                    m_strincncinpowercarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInAirSupplyCarGuaranteeStartTime")
                {
                    m_strincncinairsupplycarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInAirSupplyCarGuaranteeEndTime")
                {
                    m_strincncinairsupplycarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInAirConditionerCarGuaranteeStartTime")
                {
                    m_strincncinairconditionercarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInAirConditionerCarGuaranteeEndTime")
                {
                    m_strincncinairconditionercarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInCleanWaterCarGuaranteeStartTime")
                {
                    m_strincncincleanwatercarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInCleanWaterCarGuaranteeEndTime")
                {
                    m_strincncincleanwatercarguaranteeendtime = value;
                }

                else if (sIndex == "IncncInSewageCarGuaranteeStartTime")
                {
                    m_strincncinsewagecarguaranteestarttime = value;
                }

                else if (sIndex == "IncncInSewageCarGuaranteeEndTime")
                {
                    m_strincncinsewagecarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutLadderCarrGuaranteeStartTime")
                {
                    m_stroutcncoutladdercarrguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutLadderCarGuaranteeEndTime")
                {
                    m_stroutcncoutladdercarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutBridgerGuaranteeStartTime")
                {
                    m_stroutcncoutbridgerguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutBridgeGuaranteeEndTime")
                {
                    m_stroutcncoutbridgeguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutFerryrGuaranteeStartTime")
                {
                    m_stroutcncoutferryrguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeEndTime")
                {
                    m_stroutcncoutferryguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeFirstOutTime")
                {
                    m_stroutcncoutferryguaranteefirstouttime = value;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeFirstArriveGateTime")
                {
                    m_stroutcncoutferryguaranteefirstarrivegatetime = value;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeFirstArrivePlaneTime")
                {
                    m_stroutcncoutferryguaranteefirstarriveplanetime = value;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeLastOutTime")
                {
                    m_stroutcncoutferryguaranteelastouttime = value;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeLastArriveGateTime")
                {
                    m_stroutcncoutferryguaranteelastarrivegatetime = value;
                }

                else if (sIndex == "OutcncOutFerryGuaranteeLastArrivePlaneTime")
                {
                    m_stroutcncoutferryguaranteelastarriveplanetime = value;
                }

                else if (sIndex == "OutcncOutFerryrGuaranteeArriveTime")
                {
                    m_stroutcncoutferryrguaranteearrivetime = value;
                }

                else if (sIndex == "OutcncOutCrewCarrGuaranteeStartTime")
                {
                    m_stroutcncoutcrewcarrguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutCrewCarGuaranteeEndTime")
                {
                    m_stroutcncoutcrewcarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutConveyerBeltVehicleGuaranteeStartTime")
                {
                    m_stroutcncoutconveyerbeltvehicleguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutConveyerBeltVehicleGuaranteeEndTime")
                {
                    m_stroutcncoutconveyerbeltvehicleguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutPlatformCarGuaranteeStartTime")
                {
                    m_stroutcncoutplatformcarguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutPlatformCarGuaranteeEndTime")
                {
                    m_stroutcncoutplatformcarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeStartTime")
                {
                    m_stroutcncouttrailcarguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeEndTime")
                {
                    m_stroutcncouttrailcarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeArriveTime")
                {
                    m_stroutcncouttrailcarguaranteearrivetime = value;
                }

                else if (sIndex == "OutcncOutTrailCarGuaranteeHoldTheWheelTime")
                {
                    m_stroutcncouttrailcarguaranteeholdthewheeltime = value;
                }

                else if (sIndex == "OutcncOutPowerCarGuaranteeStartTime")
                {
                    m_stroutcncoutpowercarguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutPowerCarGuaranteeEndTime")
                {
                    m_stroutcncoutpowercarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutAirSupplyCarGuaranteeStartTime")
                {
                    m_stroutcncoutairsupplycarguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutAirSupplyCarGuaranteeEndTime")
                {
                    m_stroutcncoutairsupplycarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutAirConditionerCarGuaranteeStartTime")
                {
                    m_stroutcncoutairconditionercarguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutAirConditionerCarGuaranteeEndTime")
                {
                    m_stroutcncoutairconditionercarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutCleanWaterCarGuaranteeStartTime")
                {
                    m_stroutcncoutcleanwatercarguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutCleanWaterCarGuaranteeEndTime")
                {
                    m_stroutcncoutcleanwatercarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutSewageCarGuaranteeStartTime")
                {
                    m_stroutcncoutsewagecarguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutSewageCarGuaranteeEndTime")
                {
                    m_stroutcncoutsewagecarguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutFuelFillingStartTime")
                {
                    m_stroutcncoutfuelfillingstarttime = value;
                }

                else if (sIndex == "OutcncOutFuelFillingEndTime")
                {
                    m_stroutcncoutfuelfillingendtime = value;
                }

                else if (sIndex == "OutcncOutCabinSupplyStartTime")
                {
                    m_stroutcncoutcabinsupplystarttime = value;
                }

                else if (sIndex == "OutcncOutCabinSupplyEndTime")
                {
                    m_stroutcncoutcabinsupplyendtime = value;
                }

                else if (sIndex == "IncncInCargoMailBaggageGuaranteeStartTime")
                {
                    m_strincncincargomailbaggageguaranteestarttime = value;
                }

                else if (sIndex == "IncncInCargoMailBaggageGuaranteeEndTime")
                {
                    m_strincncincargomailbaggageguaranteeendtime = value;
                }

                else if (sIndex == "IncncInCargoCabinOpenTime")
                {
                    m_strincncincargocabinopentime = value;
                }

                else if (sIndex == "IncncInCargoCabinCloseTime")
                {
                    m_strincncincargocabinclosetime = value;
                }

                else if (sIndex == "IncncInBaggageUnloadStartTime")
                {
                    m_strincncinbaggageunloadstarttime = value;
                }

                else if (sIndex == "IncncInBaggageUnloadEndTime")
                {
                    m_strincncinbaggageunloadendtime = value;
                }

                else if (sIndex == "IncncInCargoUnloadStartTime")
                {
                    m_strincncincargounloadstarttime = value;
                }

                else if (sIndex == "IncncInCargoUnloadEndTime")
                {
                    m_strincncincargounloadendtime = value;
                }

                else if (sIndex == "OutcncOutCargoMailBaggageGuaranteeStartTime")
                {
                    m_stroutcncoutcargomailbaggageguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutCargoMailBaggageGuaranteeEndTime")
                {
                    m_stroutcncoutcargomailbaggageguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutCargoMailBaggageReportTime")
                {
                    m_stroutcncoutcargomailbaggagereporttime = value;
                }

                else if (sIndex == "OutcncOutInfoPickUpBaggageTime")
                {
                    m_stroutcncoutinfopickupbaggagetime = value;
                }

                else if (sIndex == "OutcncOutCargoCabinOpenTime")
                {
                    m_stroutcncoutcargocabinopentime = value;
                }

                else if (sIndex == "OutcncOutCargoCabinCloseTime")
                {
                    m_stroutcncoutcargocabinclosetime = value;
                }

                else if (sIndex == "OutcncOutBaggageLoadStartTime")
                {
                    m_stroutcncoutbaggageloadstarttime = value;
                }

                else if (sIndex == "OutcncOutBaggageLoadEndTime")
                {
                    m_stroutcncoutbaggageloadendtime = value;
                }

                else if (sIndex == "OutcncOutCargoLoadStartTime")
                {
                    m_stroutcncoutcargoloadstarttime = value;
                }

                else if (sIndex == "OutcncOutCargoLoadEndTime")
                {
                    m_stroutcncoutcargoloadendtime = value;
                }

                else if (sIndex == "IncncInPilotBrakeTime")
                {
                    m_strincncinpilotbraketime = value;
                }

                else if (sIndex == "OutcncOutPilotArriveTime")
                {
                    m_stroutcncoutpilotarrivetime = value;
                }

                else if (sIndex == "OutcncOutPilotLooseSkidsTime")
                {
                    m_stroutcncoutpilotlooseskidstime = value;
                }

                else if (sIndex == "OutcncOutPilotPushOffTime")
                {
                    m_stroutcncoutpilotpushofftime = value;
                }

                else if (sIndex == "IncncInCabinOpenTime")
                {
                    m_strincncincabinopentime = value;
                }

                else if (sIndex == "IncncInCabinCloseTime")
                {
                    m_strincncincabinclosetime = value;
                }

                else if (sIndex == "OutcncOutCabinOpenTime")
                {
                    m_stroutcncoutcabinopentime = value;
                }

                else if (sIndex == "OutcncOutCabinCloseTime")
                {
                    m_stroutcncoutcabinclosetime = value;
                }

                else if (sIndex == "OutcncOutStewardArriveTime")
                {
                    m_stroutcncoutstewardarrivetime = value;
                }

                else if (sIndex == "OutcncOutCheckInGuaranteeStartTime")
                {
                    m_stroutcncoutcheckinguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutCheckInGuaranteeEndTime")
                {
                    m_stroutcncoutcheckinguaranteeendtime = value;
                }

                else if (sIndex == "IncncInCabinCleanStartTime")
                {
                    m_strincncincabincleanstarttime = value;
                }

                else if (sIndex == "IncncInCabinCleanEndTime")
                {
                    m_strincncincabincleanendtime = value;
                }

                else if (sIndex == "OutcncOutCabinCleanStartTime")
                {
                    m_stroutcncoutcabincleanstarttime = value;
                }

                else if (sIndex == "OutcncOutCabinCleanEndTime")
                {
                    m_stroutcncoutcabincleanendtime = value;
                }

                else if (sIndex == "IncncInGuestOutStartTime")
                {
                    m_strincncinguestoutstarttime = value;
                }

                else if (sIndex == "IncncInGuestOutEndTime")
                {
                    m_strincncinguestoutendtime = value;
                }

                else if (sIndex == "IncncInGuiderArriveTime")
                {
                    m_strincncinguiderarrivetime = value;
                }

                else if (sIndex == "IncncInGuiderGuaranteeStartTime")
                {
                    m_strincncinguiderguaranteestarttime = value;
                }

                else if (sIndex == "IncncInGuiderGuaranteeEndTime")
                {
                    m_strincncinguiderguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutGuestInStartTime")
                {
                    m_stroutcncoutguestinstarttime = value;
                }

                else if (sIndex == "OutcncOutGuestInEndTime")
                {
                    m_stroutcncoutguestinendtime = value;
                }

                else if (sIndex == "OutcncOutGuiderArriveTime")
                {
                    m_stroutcncoutguiderarrivetime = value;
                }

                else if (sIndex == "OutcncOutGuiderGuaranteeStartTime")
                {
                    m_stroutcncoutguiderguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutGuiderGuaranteeEndTime")
                {
                    m_stroutcncoutguiderguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutInformBoardTime")
                {
                    m_stroutcncoutinformboardtime = value;
                }

                else if (sIndex == "OutcncOutBoardingGateCloseTime")
                {
                    m_stroutcncoutboardinggateclosetime = value;
                }

                else if (sIndex == "OutcncOutLoadsheetUploadFinishedTime")
                {
                    m_stroutcncoutloadsheetuploadfinishedtime = value;
                }

                else if (sIndex == "OutcncOutCaptainConfirmTime")
                {
                    m_stroutcncoutcaptainconfirmtime = value;
                }

                else if (sIndex == "IncncInVipPickUpGuaranteeStartTime")
                {
                    m_strincncinvippickupguaranteestarttime = value;
                }

                else if (sIndex == "IncncInVipPickUpGuaranteeEndTime")
                {
                    m_strincncinvippickupguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutVipSeeOffGuaranteeStartTime")
                {
                    m_stroutcncoutvipseeoffguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutVipSeeOffGuaranteeEndTime")
                {
                    m_stroutcncoutvipseeoffguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutCustomGuaranteeStartTime")
                {
                    m_stroutcncoutcustomguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutCustomGuaranteeEndTime")
                {
                    m_stroutcncoutcustomguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutFrontierInspectionGuaranteeStartTime")
                {
                    m_stroutcncoutfrontierinspectionguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutFrontierInspectionGuaranteeEndTime")
                {
                    m_stroutcncoutfrontierinspectionguaranteeendtime = value;
                }

                else if (sIndex == "OutcncOutQuarantineGuaranteeStartTime")
                {
                    m_stroutcncoutquarantineguaranteestarttime = value;
                }

                else if (sIndex == "OutcncOutQuarantineGuaranteeEndTime")
                {
                    m_stroutcncoutquarantineguaranteeendtime = value;
                }


                else if (sIndex == "IncnvcInGuaranteeRecord") //进港保障信息
                {
                    m_strincnvcinguaranteerecord = value;
                }
                else if (sIndex == "OutcnvcOutGuaranteeRecord") //出港保障信息
                {
                    m_stroutcnvcoutguaranteerecord = value;
                }
                #endregion 三期 added in 20140514

                #region added in 201504
                else if (sIndex == "OutcnvcOutRunway")
                {
                    _OutcnvcOutRunway = value;
                }
                else if (sIndex == "IncnvcInRunway")
                {
                    _IncnvcInRunway = value;
                }
                else if (sIndex == "IncncInEXIT")
                {
                    _IncncInEXIT = value;
                }
                else if (sIndex == "OutcncOutTOBT")
                {
                    _OutcncOutTOBT = value;
                }
                else if (sIndex == "OutcncOutBoardStartTime")
                {
                    _OutcncOutBoardStartTime = value;
                }
                else if (sIndex == "OutcncOutBoardEndTime")
                {
                    _OutcncOutBoardEndTime = value;
                }
                else if (sIndex == "OutcncOutCheckCounterEndTime")
                {
                    _OutcncOutCheckCounterEndTime = value;
                }
                else if (sIndex == "OutcnvcOutDeicePing")
                {
                    _OutcnvcOutDeicePing = value;
                }
                else if (sIndex == "OutcnvcOutDeiceWei")
                {
                    _OutcnvcOutDeiceWei = value;
                }
                else if (sIndex == "OutcncOutArriveDeicePing")
                {
                    _OutcncOutArriveDeicePing = value;
                }
                else if (sIndex == "OutcncOutDeiceStartTime")
                {
                    _OutcncOutDeiceStartTime = value;
                }
                else if (sIndex == "OutcncOutDeiceEndTime")
                {
                    _OutcncOutDeiceEndTime = value;
                }
                else if (sIndex == "OutcncOutLeaveDeicePing")
                {
                    _OutcncOutLeaveDeicePing = value;
                }
                else if (sIndex == "OutcnvcOutSlowDeiceFlag")
                {
                    _OutcnvcOutSlowDeiceFlag = value;
                }
                else if (sIndex == "OutcncOutTSAT")
                {
                    _OutcncOutTSAT = value;
                }
                else if (sIndex == "OutcncOutCTOT")
                {
                    _OutcncOutCTOT = value;
                }
                #endregion added in 201504

                #region 三期 added in 20150428
                else if (sIndex == "OutcncOutFirstPassengerComeInCabinTime")
                {
                    _OutcncOutFirstPassengerComeInCabinTime = value;
                }
                else if (sIndex == "OutcncOutPlaneReadyEndTime")
                {
                    _OutcncOutPlaneReadyEndTime = value;
                }
                #endregion 三期 added in 20150428

                #region 三期 added in 20150624
                else if (sIndex == "IncnvcInGATE_Test")
                {
                    _IncnvcInGATE_Test = value;
                }
                else if (sIndex == "OutcnvcOutGate_Test")
                {
                    _OutcnvcOutGate_Test = value;
                }
                else if (sIndex == "OutcncOutFlightInterceptTime")
                {
                    _OutcncOutFlightInterceptTime = value;
                }
                else if (sIndex == "IncnvcInTurnTableNO")
                {
                    _IncnvcInTurnTableNO = value;
                }
                else if (sIndex == "OutcnvcOutTurnTableNO")
                {
                    _OutcnvcOutTurnTableNO = value;
                }
                #endregion 三期 added in 20150624

                #region 三期 added in 20150730
                else if (sIndex == "OutcncOutCrewStartTime")
                {
                    _OutcncOutCrewStartTime = value;
                }
                else if (sIndex == "OutcncOutCrewArriveTime")
                {
                    _OutcncOutCrewArriveTime = value;
                }
                else if (sIndex == "OutcnvcOutOverStationType")
                {
                    _OutcnvcOutOverStationType = value;
                }
                #endregion 三期 added in 20150730

                #region 三期 added in 20160323
                else if (sIndex == "OutcnvcOutSpotUsers")
                {
                    _OutcnvcOutSpotUsers = value;
                }

                else if (sIndex == "OutcnvcOutMaintenanceUsers")
                {
                    _OutcnvcOutMaintenanceUsers = value;
                }

                else if (sIndex == "OutcnvcOutFleetUsers")
                {
                    _OutcnvcOutFleetUsers = value;
                }

                else if (sIndex == "OutcnvcOutVIPRoomUsers")
                {
                    _OutcnvcOutVIPRoomUsers = value;
                }

                else if (sIndex == "OutcnvcOutAviationDietUsers")
                {
                    _OutcnvcOutAviationDietUsers = value;
                }

                else if (sIndex == "OutcnvcOutCheckInUsers")
                {
                    _OutcnvcOutCheckInUsers = value;
                }

                else if (sIndex == "OutcnvcOutCleaningTeamUsers")
                {
                    _OutcnvcOutCleaningTeamUsers = value;
                }

                else if (sIndex == "IncnvcInSpotUsers")
                {
                    _IncnvcInSpotUsers = value;
                }

                else if (sIndex == "IncnvcInMaintenanceUsers")
                {
                    _IncnvcInMaintenanceUsers = value;
                }

                else if (sIndex == "IncnvcInFleetUsers")
                {
                    _IncnvcInFleetUsers = value;
                }

                else if (sIndex == "IncnvcInVIPRoomUsers")
                {
                    _IncnvcInVIPRoomUsers = value;
                }

                else if (sIndex == "IncnvcInAviationDietUsers")
                {
                    _IncnvcInAviationDietUsers = value;
                }

                else if (sIndex == "IncnvcInCheckInUsers")
                {
                    _IncnvcInCheckInUsers = value;
                }

                else if (sIndex == "IncnvcInCleaningTeamUsers")
                {
                    _IncnvcInCleaningTeamUsers = value;
                }

                #endregion 三期 added in 20160323

                #region 三期 added in 20160503
                else if (sIndex == "OutcncSTA")
                {
                    _OutcncSTA = value;
                }

                else if (sIndex == "OutcncETA")
                {
                    _OutcncETA = value;
                }

                else if (sIndex == "OutcncTDWN")
                {
                    _OutcncTDWN = value;
                }

                else if (sIndex == "OutcncATA")
                {
                    _OutcncATA = value;
                }
                #endregion 三期 added in 20160503

                #region 三期 added in 20160527
                else if (sIndex == "IncniInConveyerBeltVehicleGuaranteeCount")
                {
                    _IncniInConveyerBeltVehicleGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutConveyerBeltVehicleGuaranteeCount")
                {
                    _OutcniOutConveyerBeltVehicleGuaranteeCount = value;
                }

                else if (sIndex == "IncniInBaggageCarGuaranteeCount")
                {
                    _IncniInBaggageCarGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutBaggageCarGuaranteeCount")
                {
                    _OutcniOutBaggageCarGuaranteeCount = value;
                }

                else if (sIndex == "IncniInCrewCarGuaranteeCount")
                {
                    _IncniInCrewCarGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutCrewCarGuaranteeCount")
                {
                    _OutcniOutCrewCarGuaranteeCount = value;
                }

                else if (sIndex == "IncniInPlatformCarGuaranteeCount")
                {
                    _IncniInPlatformCarGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutPlatformCarGuaranteeCount")
                {
                    _OutcniOutPlatformCarGuaranteeCount = value;
                }

                else if (sIndex == "IncniInLadderCarGuaranteeCount")
                {
                    _IncniInLadderCarGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutLadderCarGuaranteeCount")
                {
                    _OutcniOutLadderCarGuaranteeCount = value;
                }

                else if (sIndex == "IncniInFerryGuaranteeCount")
                {
                    _IncniInFerryGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutFerryGuaranteeCount")
                {
                    _OutcniOutFerryGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutDeicingVehicleGuaranteeCount")
                {
                    _OutcniOutDeicingVehicleGuaranteeCount = value;
                }

                else if (sIndex == "OutcniOutTrailCarGuaranteeCount")
                {
                    _OutcniOutTrailCarGuaranteeCount = value;
                }

                else if (sIndex == "OutcnvcOutRemark_Fleet")
                {
                    _OutcnvcOutRemark_Fleet = value;
                }

                else if (sIndex == "IncnvcInRemark_Fleet")
                {
                    _IncnvcInRemark_Fleet = value;
                }

                #endregion 三期 added in 20160527

                #region 三期 added in 20160616
                else if (sIndex == "OutcnvcOutArrangeCargoOperator")
                {
                    _OutcnvcOutArrangeCargoOperator = value;
                }

                else if (sIndex == "OutcncOutArrangeCargoOperateTime")
                {
                    _OutcncOutArrangeCargoOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutRecheckLoadSheetOperator")
                {
                    _OutcnvcOutRecheckLoadSheetOperator = value;
                }

                else if (sIndex == "OutcncOutRecheckLoadSheetOperateTime")
                {
                    _OutcncOutRecheckLoadSheetOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutConfirmLoadSheetOperator")
                {
                    _OutcnvcOutConfirmLoadSheetOperator = value;
                }

                else if (sIndex == "OutcncOutConfirmLoadSheetOperateTime")
                {
                    _OutcncOutConfirmLoadSheetOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutConfirmLoadSheetOperateVerNumber")
                {
                    _OutcnvcOutConfirmLoadSheetOperateVerNumber = value;
                }

                else if (sIndex == "OutcnvcOutCheckManifestOperator")
                {
                    _OutcnvcOutCheckManifestOperator = value;
                }

                else if (sIndex == "OutcncOutCheckManifestOperateTime")
                {
                    _OutcncOutCheckManifestOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutRecheckManifestOperator")
                {
                    _OutcnvcOutRecheckManifestOperator = value;
                }

                else if (sIndex == "OutcncOutRecheckManifestOperateTime")
                {
                    _OutcncOutRecheckManifestOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutUploadManifestOperator")
                {
                    _OutcnvcOutUploadManifestOperator = value;
                }

                else if (sIndex == "OutcncOutUploadManifestOperateTime")
                {
                    _OutcncOutUploadManifestOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutUploadManifestOperateResult")
                {
                    _OutcnvcOutUploadManifestOperateResult = value;
                }

                else if (sIndex == "OutcnvcOutCrewConfirmManifestOperateResult")
                {
                    _OutcnvcOutCrewConfirmManifestOperateResult = value;
                }

                else if (sIndex == "OutcncOutCrewConfirmManifestOperateTime")
                {
                    _OutcncOutCrewConfirmManifestOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutTransportManifestOperator")
                {
                    _OutcnvcOutTransportManifestOperator = value;
                }

                else if (sIndex == "OutcncOutTransportManifestOperateTime")
                {
                    _OutcncOutTransportManifestOperateTime = value;
                }

                else if (sIndex == "OutcnvcOutRemark_Balance")
                {
                    _OutcnvcOutRemark_Balance = value;
                }
                #endregion 三期 added in 20160616

                #region 三期 added in 20160704
                else if (sIndex == "IncnvcInLeaders")
                {
                    _IncnvcInLeaders = value;
                }

                else if (sIndex == "OutcnvcOutLeaders")
                {
                    _OutcnvcOutLeaders = value;
                }
                #endregion 三期 added in 20160704

            }
        }


        /// <summary>
        /// 比较函数（以便实现排序）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            GuaranteeInforBM guaranteeInforBM = (GuaranteeInforBM)obj;

            if (m_strOutAllViewIndex.CompareTo(guaranteeInforBM.m_strOutAllViewIndex) < 0)
            {
                return -1;
            }
            else if (m_strOutAllViewIndex.CompareTo(guaranteeInforBM.m_strOutAllViewIndex) == 0)
            {
                if (m_strInAllViewIndex.CompareTo(guaranteeInforBM.m_strInAllViewIndex) < 0)
                {
                    return -1;
                }
                else if ((m_strInAllViewIndex.CompareTo(guaranteeInforBM.m_strInAllViewIndex) == 0))
                {
                    if (m_strInAllTDWN.CompareTo(guaranteeInforBM.m_strInAllTDWN) < 0)
                    {
                        return -1;
                    }
                    else if (m_strInAllTDWN.CompareTo(guaranteeInforBM.m_strInAllTDWN) == 0)
                    {
                        if (m_strOutAllTOFF.CompareTo(guaranteeInforBM.m_strOutAllTOFF) < 0)
                        {
                            return -1;
                        }
                        else if (m_strOutAllTOFF.CompareTo(guaranteeInforBM.m_strOutAllTOFF) == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }

        }


    }
}
