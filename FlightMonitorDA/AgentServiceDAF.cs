using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.Public.SystemFramework;
using CompressDataSet.Common;


namespace AirSoft.FlightMonitor.AgentServiceDA
{
    /// <summary>
    /// 数据访问代理服务
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-11-01
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AgentServiceDAF : MarshalByRefObject
    {
        #region 远程对象
        static public AgentServiceDAF objRemotingObject = null;
        #endregion

        #region 内存数据表，如果数据表在多线程中有修改操作，需要增加同步锁【如 GetDataBySQL 过程】
        static public DataTable tbLegs = null;  //如果数据表在多线程中有修改操作，需要增加同步锁【如 GetDataBySQL 过程】
        static public DataTable vw_Legs = null; //如果数据表在多线程中有修改操作，需要增加同步锁【如 GetDataBySQL 过程】
        static public DataTable vw_FlightChangeRecord = null;   //如果数据表在多线程中有修改操作，需要增加同步锁【如 GetDataBySQL 过程】

        #endregion

        #region 操作繁忙标志
        static public bool blnBusy_tbLegs = false;
        static public bool blnBusy_vw_Legs = false;
        static public bool blnBusy_vw_FlightChangeRecord = false;

        #endregion

        #region 记录表
        //
        static public bool blnRecord = true;    //是否需要记录：true 需要；false 不需要
        //
        static public DataTable dtProcRecords = null;       //过程记录表
        static public DataTable dtProcAnalysis = null;      //过程分析表
        static public DataTable dtOnLineUsers = null;       //在线用户表 procRecordsDAF.AddRecord
        //
        private static object objProcRecordsDAF_AddRecord__Lock = new object(); //ProcRecordsDAF.AddRecord 的同步锁
        private static object objProcAnalysisDAF_UpdateRecord__Lock = new object(); //ProcAnalysisDAF.UpdateRecord 的同步锁
        private static object objOnLineUsersDAF_RefreshOnLineUsersInfo__Lock = new object();    //OnLineUsersDAF.RefreshOnLineUsersInfo 的同步锁

        #region ChangeLegsDAF.GetFlightByKey
        //访问锁
        private static object objChangeLegsDAF_GetFlightByKey__SQL__Lock = new object();
        private static object objChangeLegsDAF_GetFlightByKey__MEM__Lock = new object();
        //调用次数
        static public int iChangeLegsDAF_GetFlightByKey__SQL__OprationCount = 0;
        static public int iChangeLegsDAF_GetFlightByKey__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dChangeLegsDAF_GetFlightByKey__SQL__CountStartTime;
        static public DateTime dChangeLegsDAF_GetFlightByKey__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes = 0;
        static public double fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes = 0;
        static public double fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes = 0;

        #endregion

        #region ChangeRecordDAF.GetLastGuaranteeChangeRecords
        //访问锁
        private static object objChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__Lock = new object();
        private static object objChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__Lock = new object();
        //调用次数
        static public int iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount = 0;
        static public int iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__CountStartTime;
        static public DateTime dChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes = 0;
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes = 0;
        static public double fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes = 0;

        #endregion

        #region ChangeRecordDAF.GetMaxRecordNo
        //访问锁
        private static object objChangeRecordDAF_GetMaxRecordNo__SQL__Lock = new object();
        private static object objChangeRecordDAF_GetMaxRecordNo__MEM__Lock = new object();
        //调用次数
        static public int iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount = 0;
        static public int iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dChangeRecordDAF_GetMaxRecordNo__SQL__CountStartTime;
        static public DateTime dChangeRecordDAF_GetMaxRecordNo__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes = 0;
        static public double fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes = 0;
        static public double fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes = 0;

        #endregion

        #region ChangeRecordDAF.GetChangeRecords
        //访问锁
        private static object objChangeRecordDAF_GetChangeRecords__SQL__Lock = new object();
        private static object objChangeRecordDAF_GetChangeRecords__MEM__Lock = new object();
        //调用次数
        static public int iChangeRecordDAF_GetChangeRecords__SQL__OprationCount = 0;
        static public int iChangeRecordDAF_GetChangeRecords__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dChangeRecordDAF_GetChangeRecords__SQL__CountStartTime;
        static public DateTime dChangeRecordDAF_GetChangeRecords__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress = 0;
        static public long lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress = 0;
        static public long lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes = 0;
        static public double fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes = 0;
        static public double fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes = 0;

        #endregion

        #region GuaranteeInforDAF.GetFlightsByStation
        //访问锁
        private static object objGuaranteeInforDAF_GetFlightsByStation__SQL__Lock = new object();
        private static object objGuaranteeInforDAF_GetFlightsByStation__MEM__Lock = new object();
        //调用次数
        static public int iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount = 0;
        static public int iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dGuaranteeInforDAF_GetFlightsByStation__SQL__CountStartTime;
        static public DateTime dGuaranteeInforDAF_GetFlightsByStation__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes = 0;

        #endregion

        #region GuaranteeInforDAF.GetFlightByKey
        //访问锁
        private static object objGuaranteeInforDAF_GetFlightByKey__SQL__Lock = new object();
        private static object objGuaranteeInforDAF_GetFlightByKey__MEM__Lock = new object();
        //调用次数
        static public int iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount = 0;
        static public int iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime;
        static public DateTime dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes = 0;

        #endregion

        #region GuaranteeInforDAF.GetFlightsByMessage
        //访问锁
        private static object objGuaranteeInforDAF_GetFlightsByMessage__SQL__Lock = new object();
        private static object objGuaranteeInforDAF_GetFlightsByMessage__MEM__Lock = new object();
        //调用次数
        static public int iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount = 0;
        static public int iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dGuaranteeInforDAF_GetFlightsByMessage__SQL__CountStartTime;
        static public DateTime dGuaranteeInforDAF_GetFlightsByMessage__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress = 0;
        static public long lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes = 0;
        static public double fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes = 0;

        #endregion


        #region MEM.GettbLegs 获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据
        //访问锁
        private static object objMEM_GettbLegs__SQL__Lock = new object();
        private static object objMEM_GettbLegs__MEM__Lock = new object();
        //调用次数
        static public int iMEM_GettbLegs__SQL__OprationCount = 0;
        static public int iMEM_GettbLegs__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dMEM_GettbLegs__SQL__CountStartTime;
        static public DateTime dMEM_GettbLegs__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lMEM_GettbLegs__SQL__TotalLengthBeforeCompress = 0;
        static public long lMEM_GettbLegs__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lMEM_GettbLegs__SQL__TotalLengthAfterCompress = 0;
        static public long lMEM_GettbLegs__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fMEM_GettbLegs__SQL__TotalProcTimes = 0;
        static public double fMEM_GettbLegs__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fMEM_GettbLegs__SQL__TotalCompressTimes = 0;
        static public double fMEM_GettbLegs__MEM__TotalCompressTimes = 0;

        #endregion MEM.GettbLegs 获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据

        #region MEM.Getvw_Legs 获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据
        //访问锁
        private static object objMEM_Getvw_Legs__SQL__Lock = new object();
        private static object objMEM_Getvw_Legs__MEM__Lock = new object();
        //调用次数
        static public int iMEM_Getvw_Legs__SQL__OprationCount = 0;
        static public int iMEM_Getvw_Legs__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dMEM_Getvw_Legs__SQL__CountStartTime;
        static public DateTime dMEM_Getvw_Legs__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress = 0;
        static public long lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress = 0;
        static public long lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fMEM_Getvw_Legs__SQL__TotalProcTimes = 0;
        static public double fMEM_Getvw_Legs__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fMEM_Getvw_Legs__SQL__TotalCompressTimes = 0;
        static public double fMEM_Getvw_Legs__MEM__TotalCompressTimes = 0;

        #endregion MEM.Getvw_Legs 获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据 

        #region MEM.Getvw_FlightChangeRecord 获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据
        //访问锁
        private static object objMEM_Getvw_FlightChangeRecord__SQL__Lock = new object();
        private static object objMEM_Getvw_FlightChangeRecord__MEM__Lock = new object();
        //调用次数
        static public int iMEM_Getvw_FlightChangeRecord__SQL__OprationCount = 0;
        static public int iMEM_Getvw_FlightChangeRecord__MEM__OprationCount = 0;
        //计数开始时间
        static public DateTime dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime;
        static public DateTime dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime;
        //压缩之前大小总数（byte）
        static public long lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress = 0;
        static public long lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress = 0;
        //压缩之后大小总数（byte）
        static public long lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress = 0;
        static public long lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress = 0;
        //过程执行时间总数（秒）
        static public double fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes = 0;
        static public double fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes = 0;
        //压缩时间总数（秒）
        static public double fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes = 0;
        static public double fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes = 0;

        #endregion MEM.Getvw_FlightChangeRecord 获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据 

        #endregion


        #region 代理的过程

        #region ChangeLegsDAF

        #region 以主键为条件查询一条记录 GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        /// <summary>
        /// 以主键为条件查询一条记录
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <returns>系列化和压缩后的数据表二进制流</returns>
        public byte[] GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "" ;
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0 ;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0 ;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbLegs)
                {
                    //记录使用
                    strProcName = "(内)GetFlightByKey[ChangeLegsDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = " cncDATOP = '" + changeLegsBM.DATOP + "' AND " +
                            " cnvcFLTID = '" + changeLegsBM.FLTID  + "' AND " +
                            " cniLEGNO = " + changeLegsBM.LEGNO + " AND " +
                            " cnvcAC = '" + changeLegsBM.AC  + "' ";
                    strSort = "";
                    strFilterField = ",cncDATOP,cnvcFLTID,cniLEGNO,cnvcAC,cncFlightDate,cncCKIFlightDate,cnvcFlightNo,cnvcCKIFlightNo,cnvcLONG_REG,cncDEPSTN,cncARRSTN,cncSTD,cncSTA,cncSTATUS,cncETD,cncETA,cncATD,cncTOFF,cncTDWN,cncATA,cnvcTRI_FLTID,cnvcDIV_RCODE,cnvcDIV_FLAG,cnvcPAX,cnvcBOOK,cnvcDELAY1,cniDUR1,cnvcDELAY2,cniDUR2,cnvcDELAY3,cniDUR3,cnvcDELAY4,cniDUR4,cnvcGATE,cnvcSTC,cnvcVERSION,cncORIG_ACTYP,cncACTYP,cnvcACOWN,cnvcSEQ,cncInsertTime,cniDeleteTag,";
                    strFilterField = strFilterField.Replace(" ","");

                    dataTable = GetDataBySQL("tbLegs", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetFlightByKey[ChangeLegsDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeLegsDAF changeLegsDAF = new ChangeLegsDAF();
                    dataTable = changeLegsDAF.GetFlightByKey(changeLegsBM);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objChangeLegsDAF_GetFlightByKey__MEM__Lock)
                    {
                        iOprationCount = iChangeLegsDAF_GetFlightByKey__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeLegsDAF_GetFlightByKey__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes;

                        iChangeLegsDAF_GetFlightByKey__MEM__OprationCount++;
                        if (iChangeLegsDAF_GetFlightByKey__MEM__OprationCount == 0)
                            dChangeLegsDAF_GetFlightByKey__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress =
                            lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress =
                            lChangeLegsDAF_GetFlightByKey__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes =
                            fChangeLegsDAF_GetFlightByKey__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes =
                            fChangeLegsDAF_GetFlightByKey__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeLegsDAF_GetFlightByKey__SQL__Lock)
                    {
                        iOprationCount = iChangeLegsDAF_GetFlightByKey__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeLegsDAF_GetFlightByKey__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes;

                        iChangeLegsDAF_GetFlightByKey__SQL__OprationCount++;
                        if (iChangeLegsDAF_GetFlightByKey__SQL__OprationCount == 0)
                            dChangeLegsDAF_GetFlightByKey__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress =
                            lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress =
                            lChangeLegsDAF_GetFlightByKey__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes =
                            fChangeLegsDAF_GetFlightByKey__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes =
                            fChangeLegsDAF_GetFlightByKey__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return bResult;

            #endregion
        }
        #endregion

        #endregion

        #region ChangeRecordDAF

        #region 航站获取最后一批变更数据 GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLastRecordNo">系统已经处理的最大的变更序号</param>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public byte[] GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;
            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //开始和结束记录号
            string strRecordNo_Start = "", strRecordNo_End = "";

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_vw_FlightChangeRecord)
                {
                    //记录使用
                    strProcName = "(内)GetLastGuaranteeChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "cniRecordNo > " + iLastRecordNo.ToString() + " AND " +
                        "(cncOldDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncOldArrSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewArrSTN = '" + stationBM.ThreeCode + "') AND " +
                        "(cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "' OR " +
                        "cncETA >= '" + dateTimeBM.StartDateTime + "' AND cncETA < '" + dateTimeBM.EndDateTime + "')";
                    strSort = "cniRecordNo";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_FlightChangeRecord", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetLastGuaranteeChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                    dataTable = changeRecordDAF.GetLastGuaranteeChangeRecords(iLastRecordNo,
                        dateTimeBM, stationBM, accountBM);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录号
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    strRecordNo_Start = dataTable.Rows[0]["cniRecordNo"].ToString();
                    strRecordNo_End = dataTable.Rows[dataTable.Rows.Count - 1]["cniRecordNo"].ToString();
                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes;

                        iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount++;
                        if (iChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__OprationCount == 1)
                            dChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes;

                        iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount++;
                        if (iChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__OprationCount == 1)
                            dChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes =
                            fChangeRecordDAF_GetLastGuaranteeChangeRecords__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo：" + strRecordNo_Start + "-" + strRecordNo_End + "]"), 
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo：" + strRecordNo_Start + "-" + strRecordNo_End + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);

                OnLineUsersDAF onLineUsersDAF = new OnLineUsersDAF();
                onLineUsersDAF.RefreshOnLineUsersInfo(dtOnLineUsers, accountBM, SynchronizeObjectsBM.AgentServiceDAF_dtOnLineUsers__Lock);
            }
            catch(Exception ex)
            {
                SysMsgBM.TraceInfo_GetLastGuaranteeChangeRecords_1 = "[" + DateTime.Now.ToString() + "]" + ex.Message;
            }
            #endregion


            //返回结果
            return bResult;

            #endregion
        }
        #endregion

        #region 获取最大变更序号 GetMaxRecordNo(AccountBM accountBM)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public int GetMaxRecordNo(AccountBM accountBM)
        {
            #region 变量声明
            //
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";
            int iResult = -1;

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;
            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_vw_FlightChangeRecord)
                {
                    //记录使用
                    strProcName = "(内)GetMaxRecordNo[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "cniRecordNo = max(cniRecordNo)";
                    strSort = "cniRecordNo desc";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_FlightChangeRecord", strSQL, strSort, strFilterField);
                    if ((dataTable != null) && (dataTable.Rows.Count > 0))
                        iResult = Convert.ToInt32(dataTable.Rows[0]["cniRecordNo"].ToString());
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (iResult == -1)
                {
                    //记录使用
                    strProcName = "(库)GetMaxRecordNo[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                    iResult = Convert.ToInt32(changeRecordDAF.GetMaxRecordNo());
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objChangeRecordDAF_GetMaxRecordNo__MEM__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetMaxRecordNo__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes;

                        iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount++;
                        if (iChangeRecordDAF_GetMaxRecordNo__MEM__OprationCount == 1)
                            dChangeRecordDAF_GetMaxRecordNo__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetMaxRecordNo__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes =
                            fChangeRecordDAF_GetMaxRecordNo__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes =
                            fChangeRecordDAF_GetMaxRecordNo__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeRecordDAF_GetMaxRecordNo__SQL__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetMaxRecordNo__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes;

                        iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount++;
                        if (iChangeRecordDAF_GetMaxRecordNo__SQL__OprationCount == 1)
                            dChangeRecordDAF_GetMaxRecordNo__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetMaxRecordNo__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes =
                            fChangeRecordDAF_GetMaxRecordNo__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes =
                            fChangeRecordDAF_GetMaxRecordNo__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);

            }
            catch (Exception ex)
            {
            }
            #endregion


            //返回结果
            return iResult;

            #endregion

        }
        #endregion

        #region 获取航站最新100条变更记录 GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLastRecordNo"></param>
        /// <param name="dateTimeBM"></param>
        /// <param name="stationBM"></param>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public byte[] GetChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;
            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //开始和结束记录号
            string strRecordNo_Start = "", strRecordNo_End = "";

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_vw_FlightChangeRecord)
                {
                    //记录使用
                    strProcName = "(内)GetChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "cniRecordNo > " + iLastRecordNo.ToString() + " AND " +
                        "(cncOldDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncOldArrSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewDepSTN = '" + stationBM.ThreeCode + "' OR " +
                        "cncNewArrSTN = '" + stationBM.ThreeCode + "') AND " +
                        "(cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "' OR " +
                        "cncETA >= '" + dateTimeBM.StartDateTime + "' AND cncETA < '" + dateTimeBM.EndDateTime + "')";
                    strSort = "cniRecordNo desc";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_FlightChangeRecord", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetChangeRecords[ChangeRecordDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    ChangeRecordDAF changeRecordDAF = new ChangeRecordDAF();
                    dataTable = changeRecordDAF.GetChangeRecords(iLastRecordNo, dateTimeBM, stationBM);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录号
                if ((dataTable != null) && (dataTable.Rows.Count > 0))
                {
                    strRecordNo_Start = dataTable.Rows[dataTable.Rows.Count - 1]["cniRecordNo"].ToString();
                    strRecordNo_End = dataTable.Rows[0]["cniRecordNo"].ToString();
                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objChangeRecordDAF_GetChangeRecords__MEM__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetChangeRecords__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetChangeRecords__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes;

                        iChangeRecordDAF_GetChangeRecords__MEM__OprationCount++;
                        if (iChangeRecordDAF_GetChangeRecords__MEM__OprationCount == 1)
                            dChangeRecordDAF_GetChangeRecords__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetChangeRecords__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes =
                            fChangeRecordDAF_GetChangeRecords__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes =
                            fChangeRecordDAF_GetChangeRecords__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objChangeRecordDAF_GetChangeRecords__SQL__Lock)
                    {
                        iOprationCount = iChangeRecordDAF_GetChangeRecords__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dChangeRecordDAF_GetChangeRecords__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes;
                        fTotalCompressTimes = fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes;

                        iChangeRecordDAF_GetChangeRecords__SQL__OprationCount++;
                        if (iChangeRecordDAF_GetChangeRecords__SQL__OprationCount == 1)
                            dChangeRecordDAF_GetChangeRecords__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress =
                            lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress =
                            lChangeRecordDAF_GetChangeRecords__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes =
                            fChangeRecordDAF_GetChangeRecords__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes =
                            fChangeRecordDAF_GetChangeRecords__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo：" + strRecordNo_Start + "-" + strRecordNo_End + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]" + "[RecordNo：" + strRecordNo_Start + "-" + strRecordNo_End + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);

            }
            catch (Exception ex)
            {
            }
            #endregion


            //返回结果
            return bResult;

            #endregion
        }
        #endregion

        #endregion

        #region GuaranteeInforDAF

        #region 获取某航站的进出港航班
        /// <summary>
        /// 获取某航站的进出港航班
        /// </summary>
        /// <param name="dateTimeBM">当天时间范围实体对象</param>
        /// <param name="stationBM">航站名称实体对象</param>
        /// <param name="accountBM">登陆帐号实体对象</param>
        /// <returns>该航站的所有航班</returns>
        public byte[] GetFlightsByStation(DateTimeBM dateTimeBM, StationBM stationBM, AccountBM accountBM)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;
            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_vw_Legs)
                {
                    //记录使用 
                    strProcName = "(内)GetFlightsByStation[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = "(cncETD >= '" + dateTimeBM.StartDateTime + "' AND cncETD < '" + dateTimeBM.EndDateTime + "' OR " +
                        "cncETA >= '" + dateTimeBM.StartDateTime + "' AND cncETA < '" + dateTimeBM.EndDateTime + "') AND cniDeleteTag = 0 AND cncSTATUS <> 'CNL' AND " +
                        "(cncDEPSTN = '" + stationBM.ThreeCode + "' OR cncARRSTN = '" + stationBM.ThreeCode + "') ";
                    strSort = "cnvcLONG_REG, cncETD";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetFlightsByStation[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //

                    GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                    dataTable = guaranteeInforDAF.GetFlightsByStation(dateTimeBM, stationBM);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightsByStation__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByStation__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByStation__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByStation__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightsByStation__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByStation__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByStation__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByStation__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByStation__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByStation__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM,SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return bResult;

            #endregion

        }
        #endregion

        #region 以主键为条件查询一条记录，由于类中有重名函数，GetFlightByKey(ChangeLegsBM changeLegsBM) 更改为 GetFlightByKey(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        /// <summary>
        /// 以主键为条件查询一条记录，由于类中有重名函数，GetFlightByKey(ChangeLegsBM changeLegsBM) 更改为 GetFlightByKey(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <param name="accountBM">登陆帐号实体对象</param>
        /// <returns></returns>
        public byte[] GetFlightByKey(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;
            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_vw_Legs)
                {
                    //记录使用 
                    strProcName = "(内)GetFlightByKey[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = " cncDATOP = '" + changeLegsBM.DATOP + "' AND " +
                            " cnvcFLTID = '" + changeLegsBM.FLTID + "' AND " +
                            " cniLEGNO = " + changeLegsBM.LEGNO + " AND " +
                            " cnvcAC = '" + changeLegsBM.AC + "'";
                    strSort = "";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetFlightByKey[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //

                    GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                    dataTable = guaranteeInforDAF.GetFlightByKey(changeLegsBM);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return bResult;

            #endregion

        }
        #endregion

        #region 以主键为条件查询一条记录，由于类中有重名函数，GetFlightByKey(ChangeLegsBM changeLegsBM) 更改为 DataTable GetFlightByKey_NotCompress(ChangeLegsBM changeLegsBM, AccountBM accountBM)，返回的数据不进行压缩
        /// <summary>
        /// 以主键为条件查询一条记录，由于类中有重名函数，GetFlightByKey(ChangeLegsBM changeLegsBM) 更改为 DataTable GetFlightByKey_NotCompress(ChangeLegsBM changeLegsBM, AccountBM accountBM)，返回的数据不进行压缩
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <param name="accountBM">登陆帐号实体对象</param>
        /// <returns></returns>
        public DataTable GetFlightByKey_NotCompress(ChangeLegsBM changeLegsBM, AccountBM accountBM)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;
            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //返回数据表的记录数
            int iRecordCount = 0;

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_vw_Legs)
                {
                    //记录使用 
                    strProcName = "(内)GetFlightByKey[GuaranteeInforDAF][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    strSQL = " cncDATOP = '" + changeLegsBM.DATOP + "' AND " +
                            " cnvcFLTID = '" + changeLegsBM.FLTID + "' AND " +
                            " cniLEGNO = " + changeLegsBM.LEGNO + " AND " +
                            " cnvcAC = '" + changeLegsBM.AC + "'";
                    strSort = "";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);

                    //返回数据表的记录数
                    if (dataTable != null)
                        iRecordCount = dataTable.Rows.Count;
                    else
                        iRecordCount = 0;

                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //调用数据库
                if (dataTable == null)
                {
                    //记录使用
                    strProcName = "(库)GetFlightByKey[GuaranteeInforDAF][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //

                    GuaranteeInforDAF guaranteeInforDAF = new GuaranteeInforDAF();
                    dataTable = guaranteeInforDAF.GetFlightByKey(changeLegsBM);

                    //返回数据表的记录数
                    if (dataTable != null)
                        iRecordCount = dataTable.Rows.Count;
                    else
                        iRecordCount = 0;

                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;

                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }

            #endregion

            #region 把数据表压缩和系列化成二进制流 -- 不使用
            ////把数据表压缩和系列化成二进制流
            //if (dataTable != null)
            //{
            //    //记录使用
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //记录使用
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightByKey__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightByKey__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightByKey__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightByKey__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightByKey__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[记录数量：" + iRecordCount.ToString() + "][" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[" + accountBM.IPAddress + "][" + accountBM.UserName + "]"),
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return dataTable;

            #endregion

        }
        #endregion

        #region 根据报文解析的信息确定航班
        /// <summary>
        /// 根据报文解析的信息确定航班
        /// </summary>
        /// <param name="FlightNo">航班号</param>
        /// <param name="ST">计划起飞时间（IO是OUT）；计划到达时间（IO是IN）</param>
        /// <param name="STN">起飞机场（IO是OUT）；降落机场（IO是IN）</param>
        /// <param name="IO">出港航班（IO是OUT）；进港航班（IO是IN）</param>
        /// <returns>确定的航班</returns>
        public DataTable GetFlightsByMessage(string FlightNo, string ST, string STN, string IO)
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;
            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_vw_Legs)
                {
                    //记录使用 
                    strProcName = "(内)GetFlightsByMessage[GuaranteeInforDAF]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    if (IO == "OUT")
                    {
                        strSQL = "(cnvcFlightNo = '" + FlightNo + "') and (cncSTD = '" + ST + "') and (cncDEPSTN = '" + STN + "')";
                    }
                    else
                    {
                        strSQL = "(cnvcFlightNo = '" + FlightNo + "') and (cncSTA = '" + ST + "') and (cncARRSTN = '" + STN + "')";
                    }
                    strSort = "";
                    strFilterField = "";
                    strFilterField = strFilterField.Replace(" ", "");

                    dataTable = GetDataBySQL("vw_Legs", strSQL, strSort, strFilterField);
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }
                //
                if (dataTable == null)
                {
                    throw new Exception("查询内存获取航班失败！");
                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objGuaranteeInforDAF_GetFlightsByMessage__MEM__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByMessage__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByMessage__MEM__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByMessage__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objGuaranteeInforDAF_GetFlightsByMessage__SQL__Lock)
                    {
                        iOprationCount = iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dGuaranteeInforDAF_GetFlightsByMessage__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes;
                        fTotalCompressTimes = fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes;

                        iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount++;
                        if (iGuaranteeInforDAF_GetFlightsByMessage__SQL__OprationCount == 1)
                            dGuaranteeInforDAF_GetFlightsByMessage__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress =
                            lGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes =
                            fGuaranteeInforDAF_GetFlightsByMessage__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }

                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult,
                    (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult,
                    (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return dataTable;

            #endregion

        }
        #endregion 根据报文解析的信息确定航班

        #endregion


        #region MEM，获取 AgentServiceDAF类 的 tbLegs、vw_Legs和vw_FlightChangeRecord字段 的所有的数据
        #region byte[] GettbLegs()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据
        /// <summary>
        /// byte[] GettbLegs()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据
        /// </summary>
        /// <returns>系列化和压缩后的数据表二进制流</returns>
        public byte[] GettbLegs()
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //
            string strRecordCount = ""; //数据表的行数

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbLegs)
                {
                    //记录使用
                    strProcName = "(内)GettbLegs[MEM]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = tbLegs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objMEM_GettbLegs__MEM__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__MEM__TotalCompressTimes;

                        iMEM_GettbLegs__MEM__OprationCount++;
                        if (iMEM_GettbLegs__MEM__OprationCount == 0)
                            dMEM_GettbLegs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__MEM__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__MEM__TotalLengthAfterCompress =
                            lMEM_GettbLegs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__MEM__TotalProcTimes =
                            fMEM_GettbLegs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__MEM__TotalCompressTimes =
                            fMEM_GettbLegs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_GettbLegs__SQL__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__SQL__TotalCompressTimes;

                        iMEM_GettbLegs__SQL__OprationCount++;
                        if (iMEM_GettbLegs__SQL__OprationCount == 0)
                            dMEM_GettbLegs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__SQL__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__SQL__TotalLengthAfterCompress =
                            lMEM_GettbLegs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__SQL__TotalProcTimes =
                            fMEM_GettbLegs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__SQL__TotalCompressTimes =
                            fMEM_GettbLegs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[记录数：" + strRecordCount + "条]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return bResult;

            #endregion
        }
        #endregion byte[] GettbLegs()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据

        #region DataTable GettbLegs_NotCompress()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据，返回的数据不进行压缩
        /// <summary>
        /// DataTable GettbLegs_NotCompress()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据，返回的数据不进行压缩
        /// </summary>
        /// <returns>返回的数据表</returns>
        public DataTable GettbLegs_NotCompress()
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //
            string strRecordCount = ""; //数据表的行数

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbLegs)
                {
                    //记录使用
                    strProcName = "(内)GettbLegs[MEM][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = tbLegs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //
                if (dataTable == null)
                    throw new Exception("dataTable == null");

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            //#region 把数据表压缩和系列化成二进制流
            ////把数据表压缩和系列化成二进制流
            //if (dataTable != null)
            //{
            //    //记录使用
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //记录使用
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            //#endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objMEM_GettbLegs__MEM__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__MEM__TotalCompressTimes;

                        iMEM_GettbLegs__MEM__OprationCount++;
                        if (iMEM_GettbLegs__MEM__OprationCount == 0)
                            dMEM_GettbLegs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__MEM__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__MEM__TotalLengthAfterCompress =
                            lMEM_GettbLegs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__MEM__TotalProcTimes =
                            fMEM_GettbLegs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__MEM__TotalCompressTimes =
                            fMEM_GettbLegs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_GettbLegs__SQL__Lock)
                    {
                        iOprationCount = iMEM_GettbLegs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_GettbLegs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_GettbLegs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_GettbLegs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_GettbLegs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_GettbLegs__SQL__TotalCompressTimes;

                        iMEM_GettbLegs__SQL__OprationCount++;
                        if (iMEM_GettbLegs__SQL__OprationCount == 0)
                            dMEM_GettbLegs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_GettbLegs__SQL__TotalLengthBeforeCompress =
                            lMEM_GettbLegs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_GettbLegs__SQL__TotalLengthAfterCompress =
                            lMEM_GettbLegs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_GettbLegs__SQL__TotalProcTimes =
                            fMEM_GettbLegs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_GettbLegs__SQL__TotalCompressTimes =
                            fMEM_GettbLegs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[记录数：" + strRecordCount + "条]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return dataTable;

            #endregion
        }
        #endregion DataTable GettbLegs_NotCompress()：获取 AgentServiceDAF类 的 tbLegs字段 的所有的数据，返回的数据不进行压缩


        #region byte[] Getvw_Legs()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据
        /// <summary>
        /// byte[] Getvw_Legs()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据
        /// </summary>
        /// <returns>系列化和压缩后的数据表二进制流</returns>
        public byte[] Getvw_Legs()
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //
            string strRecordCount = ""; //数据表的行数

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbLegs)
                {
                    //记录使用
                    strProcName = "(内)Getvw_Legs[MEM]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_Legs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objMEM_Getvw_Legs__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__MEM__TotalCompressTimes;

                        iMEM_Getvw_Legs__MEM__OprationCount++;
                        if (iMEM_Getvw_Legs__MEM__OprationCount == 0)
                            dMEM_Getvw_Legs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__MEM__TotalProcTimes =
                            fMEM_Getvw_Legs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__MEM__TotalCompressTimes =
                            fMEM_Getvw_Legs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_Legs__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__SQL__TotalCompressTimes;

                        iMEM_Getvw_Legs__SQL__OprationCount++;
                        if (iMEM_Getvw_Legs__SQL__OprationCount == 0)
                            dMEM_Getvw_Legs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__SQL__TotalProcTimes =
                            fMEM_Getvw_Legs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__SQL__TotalCompressTimes =
                            fMEM_Getvw_Legs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[记录数：" + strRecordCount + "条]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return bResult;

            #endregion
        }
        #endregion byte[] Getvw_Legs()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据

        #region DataTable Getvw_Legs_NotCompress()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据，返回的数据不进行压缩
        /// <summary>
        /// DataTable Getvw_Legs_NotCompress()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据，返回的数据不进行压缩
        /// </summary>
        /// <returns>返回的数据表</returns>
        public DataTable Getvw_Legs_NotCompress()
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //
            string strRecordCount = ""; //数据表的行数

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbLegs)
                {
                    //记录使用
                    strProcName = "(内)Getvw_Legs[MEM][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_Legs.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //
                if (dataTable == null)
                    throw new Exception("dataTable == null");
                   
                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            //#region 把数据表压缩和系列化成二进制流
            ////把数据表压缩和系列化成二进制流
            //if (dataTable != null)
            //{
            //    //记录使用
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //记录使用
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            //#endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objMEM_Getvw_Legs__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__MEM__TotalCompressTimes;

                        iMEM_Getvw_Legs__MEM__OprationCount++;
                        if (iMEM_Getvw_Legs__MEM__OprationCount == 0)
                            dMEM_Getvw_Legs__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__MEM__TotalProcTimes =
                            fMEM_Getvw_Legs__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__MEM__TotalCompressTimes =
                            fMEM_Getvw_Legs__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_Legs__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_Legs__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_Legs__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_Legs__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_Legs__SQL__TotalCompressTimes;

                        iMEM_Getvw_Legs__SQL__OprationCount++;
                        if (iMEM_Getvw_Legs__SQL__OprationCount == 0)
                            dMEM_Getvw_Legs__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_Legs__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_Legs__SQL__TotalProcTimes =
                            fMEM_Getvw_Legs__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_Legs__SQL__TotalCompressTimes =
                            fMEM_Getvw_Legs__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[记录数：" + strRecordCount + "条]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return dataTable;

            #endregion
        }
        #endregion DataTable Getvw_Legs_NotCompress()：获取 AgentServiceDAF类 的 vw_Legs字段 的所有的数据，返回的数据不进行压缩


        #region byte[] Getvw_FlightChangeRecord()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据
        /// <summary>
        /// byte[] Getvw_FlightChangeRecord()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据
        /// </summary>
        /// <returns>系列化和压缩后的数据表二进制流</returns>
        public byte[] Getvw_FlightChangeRecord()
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //
            string strRecordCount = ""; //数据表的行数

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbLegs)
                {
                    //记录使用
                    strProcName = "(内)Getvw_FlightChangeRecord[MEM]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_FlightChangeRecord.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            #region 把数据表压缩和系列化成二进制流
            //把数据表压缩和系列化成二进制流
            if (dataTable != null)
            {
                //记录使用
                dDatetimeBeforeCompress = DateTime.Now;
                //
                CompressionHelper compressionHelper = new CompressionHelper();
                bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
                //记录使用
                dDatetimeAfterCompress = DateTime.Now;
            }
            #endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objMEM_Getvw_FlightChangeRecord__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__MEM__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__MEM__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_FlightChangeRecord__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__SQL__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__SQL__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[记录数：" + strRecordCount + "条]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return bResult;

            #endregion
        }
        #endregion byte[] Getvw_FlightChangeRecord()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据

        #region DataTable Getvw_FlightChangeRecord_NotCompress()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据，返回的数据不进行压缩
        /// <summary>
        /// DataTable Getvw_FlightChangeRecord_NotCompress()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据，返回的数据不进行压缩
        /// </summary>
        /// <returns>返回的数据表</returns>
        public DataTable Getvw_FlightChangeRecord_NotCompress()
        {
            #region 变量声明
            //
            byte[] bResult = null;
            DataTable dataTable = null;
            string strSQL = "";
            string strSort = "";
            string strFilterField = "";

            //记录使用
            string strProcName = ""; //过程名
            //操作结果
            string strOprationResult = "";
            //调用次数(SQL + MEM)
            int iOprationCount = 0;
            //计数开始时间
            DateTime dCountStartTime = Convert.ToDateTime("1901-01-01 01:01:01");
            //压缩之前大小总数（byte）
            int iLengthBeforeCompress = 0;
            long lTotalLengthBeforeCompress = 0;
            //压缩之后大小总数（byte）
            int iLengthAfterCompress = 0;
            long lTotalLengthAfterCompress = 0;
            //过程执行时间总数（秒）
            DateTime dDatetimeBeforeEXEC = Convert.ToDateTime("1901-01-01 01:01:01"); //过程执行前时刻
            DateTime dDatetimeAfterEXEC = Convert.ToDateTime("1901-01-01 01:01:01");  //过程执行后时刻
            double fTotalProcTimes = 0;

            //压缩时间总数（秒）
            DateTime dDatetimeBeforeCompress = Convert.ToDateTime("1901-01-01 01:01:01"); //压缩执行前时刻
            DateTime dDatetimeAfterCompress = Convert.ToDateTime("1901-01-01 01:01:01");  //压缩执行后时刻
            double fTotalCompressTimes = 0;

            //
            string strRecordCount = ""; //数据表的行数

            #endregion


            #region 编码实现
            #region 提取数据
            //提取数据
            try
            {
                //调用内存表
                if (!blnBusy_tbLegs)
                {
                    //记录使用
                    strProcName = "(内)Getvw_FlightChangeRecord[MEM][NotCompress]";
                    dDatetimeBeforeEXEC = DateTime.Now;
                    //
                    dataTable = vw_FlightChangeRecord.Copy();
                    strRecordCount = dataTable.Rows.Count.ToString();
                    //记录使用
                    dDatetimeAfterEXEC = DateTime.Now;
                }

                //
                if (dataTable == null)
                    throw new Exception("dataTable == null");

                //记录使用
                strOprationResult = "成功";
            }
            catch
            {
                dataTable = null;

                //记录使用
                strOprationResult = "失败";
                if (dDatetimeBeforeEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeBeforeEXEC = DateTime.Now;
                    dDatetimeAfterEXEC = dDatetimeBeforeEXEC;
                }
                if (dDatetimeAfterEXEC == Convert.ToDateTime("1901-01-01 01:01:01"))
                {
                    dDatetimeAfterEXEC = DateTime.Now;
                }
            }
            #endregion

            //#region 把数据表压缩和系列化成二进制流
            ////把数据表压缩和系列化成二进制流
            //if (dataTable != null)
            //{
            //    //记录使用
            //    dDatetimeBeforeCompress = DateTime.Now;
            //    //
            //    CompressionHelper compressionHelper = new CompressionHelper();
            //    bResult = compressionHelper.CompressToBytes(dataTable, ref iLengthBeforeCompress, ref iLengthAfterCompress);
            //    //记录使用
            //    dDatetimeAfterCompress = DateTime.Now;
            //}
            //#endregion

            #region 记录使用--记录信息添加进记录表
            //记录使用--记录信息添加进记录表 
            //此位置应加 访问锁
            try
            {
                if (strProcName.IndexOf("内") >= 0)
                {
                    lock (objMEM_Getvw_FlightChangeRecord__MEM__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__MEM__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__MEM__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__MEM__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__MEM__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__MEM__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__MEM__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                else
                {
                    lock (objMEM_Getvw_FlightChangeRecord__SQL__Lock)
                    {
                        iOprationCount = iMEM_Getvw_FlightChangeRecord__SQL__OprationCount;
                        if (iOprationCount == 0)
                            dCountStartTime = dDatetimeBeforeEXEC;
                        else
                            dCountStartTime = dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime;
                        lTotalLengthBeforeCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress;
                        lTotalLengthAfterCompress = lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress;
                        fTotalProcTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes;
                        fTotalCompressTimes = fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes;

                        iMEM_Getvw_FlightChangeRecord__SQL__OprationCount++;
                        if (iMEM_Getvw_FlightChangeRecord__SQL__OprationCount == 0)
                            dMEM_Getvw_FlightChangeRecord__SQL__CountStartTime = dDatetimeBeforeEXEC;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthBeforeCompress + iLengthBeforeCompress;
                        lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress =
                            lMEM_Getvw_FlightChangeRecord__SQL__TotalLengthAfterCompress + iLengthAfterCompress;
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalProcTimes +
                            (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds);
                        fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes =
                            fMEM_Getvw_FlightChangeRecord__SQL__TotalCompressTimes +
                            (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds);
                    }
                }
                //写进记录表
                ProcRecordsBM procRecordsBM = new ProcRecordsBM(-1, strProcName, dDatetimeBeforeEXEC,
                    (strOprationResult + "[记录数：" + strRecordCount + "条]"), (iOprationCount + 1), iLengthBeforeCompress, iLengthAfterCompress,
                    (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds),
                    (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds));
                ProcAnalysisBM procAnalysisBM = new ProcAnalysisBM(-1, strProcName, dDatetimeBeforeEXEC,
                    strOprationResult, (iOprationCount + 1), dCountStartTime,
                    (lTotalLengthBeforeCompress + iLengthBeforeCompress),
                    (lTotalLengthAfterCompress + iLengthAfterCompress),
                    (fTotalProcTimes + (new TimeSpan(dDatetimeAfterEXEC.Ticks - dDatetimeBeforeEXEC.Ticks).TotalSeconds)),
                    (fTotalCompressTimes + (new TimeSpan(dDatetimeAfterCompress.Ticks - dDatetimeBeforeCompress.Ticks).TotalSeconds)));

                ProcRecordsDAF procRecordsDAF = new ProcRecordsDAF();
                procRecordsDAF.AddRecord(dtProcRecords, procRecordsBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcRecords__Lock);

                ProcAnalysisDAF procAnalysisDAF = new ProcAnalysisDAF();
                procAnalysisDAF.UpdateRecord(dtProcAnalysis, procAnalysisBM, SynchronizeObjectsBM.AgentServiceDAF_dtProcAnalysis__Lock);
            }
            catch
            {
            }
            #endregion


            //返回结果
            return dataTable;

            #endregion
        }
        #endregion DataTable Getvw_FlightChangeRecord_NotCompress()：获取 AgentServiceDAF类 的 vw_FlightChangeRecord字段 的所有的数据，返回的数据不进行压缩

        #endregion MEM，获取 AgentServiceDAF类 的 tbLegs、vw_Legs和vw_FlightChangeRecord字段 的所有的数据


        #region test -- 测试使用
        public String HelloMethod(String name)
        {
            //Console.WriteLine("服务器端 : {0}", name);
            return "这里是：" + name;
        }
        #endregion 

        #endregion


        #region 根据查询语句从内存表提取数据[如果 strTableName 表示的数据表在多线程中有修改操作，需要增加同步锁]
        /// <summary>
        /// 根据查询语句从内存表提取数据
        /// </summary>
        /// <param name="strTableName">内存表名</param>
        /// <param name="strSQL">查询语句</param>
        /// <param name="strSort">排序语句</param>
        /// <param name="strFilterField">需要提取的字段，如",column1,column2,column3,"</param>
        /// <returns>查询的结果：错误返回 null</returns>
        public DataTable GetDataBySQL(string strTableName, string strSQL,string strSort,string strFilterField)
        {
            #region 变量声明
            DataTable dataTable = null;
            DataRow[] dataRows = null;

            #endregion


            #region 编码实现
            try
            {
                //tbLegs
                if (strTableName == "tbLegs")
                {
                    dataTable = tbLegs.Clone();
                    dataRows = tbLegs.Select(strSQL, strSort);
                }
                //vw_Legs
                else if (strTableName == "vw_Legs")
                {
                    dataTable = vw_Legs.Clone();
                    dataRows = vw_Legs.Select(strSQL, strSort);
                }
                //vw_FlightChangeRecord
                else if (strTableName == "vw_FlightChangeRecord")
                {
                    dataTable = vw_FlightChangeRecord.Clone();
                    dataRows = vw_FlightChangeRecord.Select(strSQL, strSort);
                }


                //把数据导入 dataTable
                foreach (DataRow dataRow in dataRows)
                {
                    dataTable.ImportRow(dataRow);
                }

                //把 dataTable 中不需要的字段删除
                if (strFilterField != "")
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        if (strFilterField.IndexOf(("," + dataColumn.ColumnName + ","), StringComparison.InvariantCultureIgnoreCase) < 0)
                            dataTable.Columns.Remove(dataColumn);
                    }
                }

            }
            catch
            {
                dataTable = null;
            }

            //返回结果
            return dataTable;

            #endregion
        }
        #endregion

    }
}
