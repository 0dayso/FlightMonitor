using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航班动态变更实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable] 
    public class ChangeLegsBM
    {
        #region 对象内部变量
        private string m_strDATOP;
        private string m_strFLTID;
        private int m_iLEGNO;
        private string m_strAC;
        private string m_strFlightDate;
        private string m_strCKIFlightDate;
        private string m_strFlightNo;
        private string m_strCKIFlightNo;
        private string m_strLONG_REG;
        private string m_strDEPSTN;
        private string m_strCityDEPSTN;
        private string m_strDEPFourCode;
        private string m_strARRSTN;
        private string m_strCityARRSTN;
        private string m_strARRFourCode;
        private string m_strSTD;
        private string m_strSTA;
        private string m_strSTATUS;
        private string m_strETD;
        private string m_strETA;
        private string m_strATD;
        private string m_strTOFF;
        private string m_strTDWN;
        private string m_strATA;
        private string m_strTRI_FLTID;
        private string m_strDIV_RCODE;
        private string m_strDIV_FLAG;
        private string m_strPAX;
        private string m_strBOOK;
        private string m_strDELAY1;
        private int m_iDUR1;
        private string m_strDELAY2;
        private int m_iDUR2;
        private string m_strDELAY3;
        private int m_iDUR3;
        private string m_strDELAY4;
        private int m_iDUR4;
        private string m_strGATE;
        private string m_strSTC;
        private string m_strVERSION;
        private string m_strORIG_ACTYP;
        private string m_strACTYP;
        private string m_strACOWN;
        private string m_strDELACTION;
        private string m_strSEQ;
        private string m_strTSTAMP;
        private int m_iDeleteTag;        
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChangeLegsBM()
        {
        }

        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow">从XML变更文件获取的数据行</param>
        /// <param name="iXMLTag">标识，便于重载构造函数</param>
        public ChangeLegsBM(DataRow dataRow, int iXMLTag)
        {
            m_strDATOP = dataRow["DATOP"].ToString();
            m_strFLTID = dataRow["FLTID"].ToString();
            m_iLEGNO = Convert.ToInt32(dataRow["LegNO"].ToString());
            m_strAC = dataRow["AC"].ToString();
            m_strFlightDate = dataRow["FlightDate"].ToString();
            m_strCKIFlightDate = dataRow["FlightDate"].ToString();
            m_strFlightNo = dataRow["FLTID"].ToString();
            m_strCKIFlightNo = dataRow["FLTID"].ToString();
            m_strLONG_REG = dataRow["LONG_REG"].ToString();
            m_strDEPSTN = dataRow["DEPSTN"].ToString();
            m_strARRSTN = dataRow["ARRSTN"].ToString();
            m_strSTD = dataRow["STD"].ToString();
            m_strSTA = dataRow["STA"].ToString();
            m_strSTATUS = dataRow["STATUS"].ToString();
            m_strETD = dataRow["ETD"].ToString();
            m_strETA = dataRow["ETA"].ToString();
            m_strATD = dataRow["ATD"].ToString();
            m_strTOFF = dataRow["TOFF"].ToString();
            m_strTDWN = dataRow["TDWN"].ToString();
            m_strATA = dataRow["ATA"].ToString();
            m_strTRI_FLTID = dataRow["TRI_FLTID"].ToString();
            m_strDIV_RCODE = dataRow["DIV_RCODE"].ToString();
            m_strDIV_FLAG = dataRow["DIV_FLAG"].ToString();
            m_strPAX = dataRow["PAX"].ToString();
            m_strBOOK = dataRow["BOOK"].ToString();
            m_strDELAY1 = dataRow["DELAY1"].ToString();
            m_iDUR1 = Convert.ToInt32(dataRow["DUR1"].ToString());
            m_strDELAY2 = dataRow["DELAY2"].ToString();
            m_iDUR2 = Convert.ToInt32(dataRow["DUR2"].ToString());
            m_strDELAY3 = dataRow["DELAY3"].ToString();
            m_iDUR3 = Convert.ToInt32(dataRow["DUR3"].ToString());
            m_strDELAY4 = dataRow["DELAY4"].ToString();
            m_iDUR4 = Convert.ToInt32(dataRow["DUR4"].ToString());
            m_strGATE = dataRow["GATE"].ToString();
            m_strSTC = dataRow["STC"].ToString();
            m_strVERSION = dataRow["VERSION"].ToString();
            m_strORIG_ACTYP = dataRow["ORIG_ACTYP"].ToString();
            m_strACTYP = dataRow["ACTYP"].ToString();
            m_strACOWN = dataRow["ACOWN"].ToString();
            m_strDELACTION = dataRow["DELACTION"].ToString();
            m_strSEQ = dataRow["SEQ"].ToString();
            m_strTSTAMP = dataRow["TSTAMP"].ToString();
            m_iDeleteTag = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public ChangeLegsBM(DataRow dataRow)
        {
            m_strDATOP = dataRow["cncDATOP"].ToString();
            m_strFLTID = dataRow["cnvcFLTID"].ToString();
            m_iLEGNO = Convert.ToInt32(dataRow["cniLEGNO"].ToString());
            m_strAC = dataRow["cnvcAC"].ToString();
            m_strFlightDate = dataRow["cncFlightDate"].ToString();
            m_strCKIFlightDate = dataRow["cncCKIFlightDate"].ToString();
            m_strFlightNo = dataRow["cnvcFlightNo"].ToString();
            m_strCKIFlightNo = dataRow["cnvcCKIFlightNo"].ToString();
            m_strLONG_REG = dataRow["cnvcLONG_REG"].ToString();
            m_strDEPSTN = dataRow["cncDEPSTN"].ToString();
            m_strARRSTN = dataRow["cncARRSTN"].ToString();
            m_strSTD = dataRow["cncSTD"].ToString();
            m_strSTA = dataRow["cncSTA"].ToString();
            m_strSTATUS = dataRow["cncSTATUS"].ToString();
            m_strETD = dataRow["cncETD"].ToString();
            m_strETA = dataRow["cncETA"].ToString();
            m_strATD = dataRow["cncATD"].ToString();
            m_strTOFF = dataRow["cncTOFF"].ToString();
            m_strTDWN = dataRow["cncTDWN"].ToString();
            m_strATA = dataRow["cncATA"].ToString();
            m_strTRI_FLTID = dataRow["cnvcTRI_FLTID"].ToString();
            m_strDIV_RCODE = dataRow["cnvcDIV_RCODE"].ToString();
            m_strDIV_FLAG = dataRow["cnvcDIV_FLAG"].ToString();
            m_strPAX = dataRow["cnvcPAX"].ToString();
            m_strBOOK = dataRow["cnvcBOOK"].ToString();
            m_strDELAY1 = dataRow["cnvcDELAY1"].ToString();
            m_iDUR1 = Convert.ToInt32(dataRow["cniDUR1"].ToString());
            m_strDELAY2 = dataRow["cnvcDELAY2"].ToString();
            m_iDUR2 = Convert.ToInt32(dataRow["cniDUR2"].ToString());
            m_strDELAY3 = dataRow["cnvcDELAY3"].ToString();
            m_iDUR3 = Convert.ToInt32(dataRow["cniDUR3"].ToString());
            m_strDELAY4 = dataRow["cnvcDELAY4"].ToString();
            m_iDUR4 = Convert.ToInt32(dataRow["cniDUR4"].ToString());
            m_strGATE = dataRow["cnvcGATE"].ToString();
            m_strSTC = dataRow["cnvcSTC"].ToString();
            m_strVERSION = dataRow["cnvcVERSION"].ToString();
            m_strORIG_ACTYP = dataRow["cncORIG_ACTYP"].ToString();
            m_strACTYP = dataRow["cncACTYP"].ToString();
            m_strACOWN = dataRow["cnvcACOWN"].ToString();
            m_strDELACTION = "";
            if (dataRow["cnvcSEQ"].ToString().Trim() == "")
            {
                m_strSEQ = "0";
            }
            else
            {
                m_strSEQ = dataRow["cnvcSEQ"].ToString();
            }
            m_strTSTAMP = "";
            m_iDeleteTag = Convert.ToInt32(dataRow["cniDeleteTag"].ToString());
        }
        

        /// <summary>
        /// FOC航班日期
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }

        /// <summary>
        /// FOC航班号
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// 航段序号
        /// </summary>
        public int LEGNO
        {
            get { return m_iLEGNO; }
            set { m_iLEGNO = value; }
        }        

        /// <summary>
        /// FOC AC
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// 本地时间航班日期
        /// </summary>
        public string FlightDate
        {
            get { return m_strFlightDate; }
            set { m_strFlightDate = value; }
        }

        /// <summary>
        /// 航信系统航班日期
        /// </summary>
        public string CKIFlightDate
        {
            get { return m_strCKIFlightDate; }
            set { m_strCKIFlightDate = value; }
        }


        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo
        {
            get { return m_strFlightNo; }
            set { m_strFlightNo = value; }
        }

        /// <summary>
        /// 航信系统航班号
        /// </summary>
        public string CKIFlightNo
        {
            get { return m_strCKIFlightNo; }
            set { m_strCKIFlightNo = value; }
        }


        /// <summary>
        /// 长飞机注册号
        /// </summary>
        public string LONG_REG
        {
            get { return m_strLONG_REG; }
            set { m_strLONG_REG = value; }
        }

        /// <summary>
        /// 始发站三字码
        /// </summary>
        public string DEPSTN
        {
            get { return m_strDEPSTN; }
            set { m_strDEPSTN = value; }
        }

        /// <summary>
        /// 始发站城市三字码
        /// </summary>
        public string CityDEPSTN
        {
            get { return m_strCityDEPSTN; }
            set { m_strCityDEPSTN = value; }
        }

        /// <summary>
        /// 始发站四字码
        /// </summary>
        public string DEPFourCode
        {
            get { return m_strDEPFourCode; }
            set { m_strDEPFourCode = value; }
        }


        /// <summary>
        /// 到达站三字码
        /// </summary>
        public string ARRSTN
        {
            get { return m_strARRSTN; }
            set { m_strARRSTN = value; }
        }

        /// <summary>
        /// 到达站城市三字码
        /// </summary>
        public string CityARRSTN
        {
            get { return m_strCityARRSTN; }
            set { m_strCityARRSTN = value; }
        }

        /// <summary>
        /// 到达站城市四字码
        /// </summary>
        public string ARRFourCode
        {
            get { return m_strARRFourCode; }
            set { m_strARRFourCode = value; }
        }
        
        /// <summary>
        /// 计划起飞时间
        /// </summary>
        public string STD
        {
            get { return m_strSTD; }
            set { m_strSTD = value; }
        }

        /// <summary>
        /// 计划到达时间
        /// </summary>
        public string STA
        {
            get { return m_strSTA; }
            set { m_strSTA = value; }
        }

        /// <summary>
        /// 航班状态
        /// </summary>
        public string STATUS
        {
            get { return m_strSTATUS; }
            set { m_strSTATUS = value; }
        }
       
        
        /// <summary>
        /// 预计起飞时间
        /// </summary>
        public string ETD
        {
            get { return m_strETD; }
            set { m_strETD = value; }
        }

        /// <summary>
        /// 预计到达时间
        /// </summary>
        public string ETA
        {
            get { return m_strETA; }
            set { m_strETA = value; }
        }        

        /// <summary>
        /// 撤抡挡时间
        /// </summary>
        public string ATD
        {
            get { return m_strATD; }
            set { m_strATD = value; }
        }

        /// <summary>
        /// 起飞时间
        /// </summary>
        public string TOFF
        {
            get { return m_strTOFF; }
            set { m_strTOFF = value; }
        }

        /// <summary>
        /// 落地时间
        /// </summary>
        public string TDWN
        {
            get { return m_strTDWN; }
            set { m_strTDWN = value; }
        }

        /// <summary>
        /// 挡抡挡时间
        /// </summary>
        public string ATA
        {
            get { return m_strATA; }
            set { m_strATA = value; }
        }
        
        /// <summary>
        /// Triangular flight ID of the “other” sector
        /// </summary>
        public string TRI_FLTID
        {
            get { return m_strTRI_FLTID; }
            set { m_strTRI_FLTID = value; }
        }

        /// <summary>
        /// 备降返航原因代码
        /// </summary>
        public string DIV_RCODE
        {
            get { return m_strDIV_RCODE; }
            set { m_strDIV_RCODE = value; }
        }
        
        /// <summary>
        /// Diversion reason code
        /// </summary>
        public string DIV_FLAG
        {
            get { return m_strDIV_FLAG; }
            set { m_strDIV_FLAG = value; }
        }

        /// <summary>
        /// Actual pax configuration
        /// </summary>
        public string PAX
        {
            get { return m_strPAX; }
            set { m_strPAX = value; }
        }

        /// <summary>
        /// Booked pax configuration
        /// </summary>
        public string BOOK
        {
            get { return m_strBOOK; }
            set { m_strBOOK = value; }
        }
        
        /// <summary>
        /// 第一延误代码
        /// </summary>
        public string DELAY1
        {
            get { return m_strDELAY1; }
            set { m_strDELAY1 = value; }
        }

        /// <summary>
        /// 第一延误时间
        /// </summary>
        public int DUR1
        {
            get { return m_iDUR1; }
            set { m_iDUR1 = value; }
        }

        /// <summary>
        /// 第二延误代码
        /// </summary>
        public string DELAY2
        {
            get { return m_strDELAY2; }
            set { m_strDELAY2 = value; }
        }

        /// <summary>
        /// 第二延误时间
        /// </summary>
        public int DUR2
        {
            get { return m_iDUR2; }
            set { m_iDUR2 = value; }
        }

       /// <summary>
        /// 第三延误代码
        /// </summary>
        public string DELAY3
        {
            get { return m_strDELAY3; }
            set { m_strDELAY3 = value; }
        }

        /// <summary>
        /// 第三延误时间
        /// </summary>
        public int DUR3
        {
            get { return m_iDUR3; }
            set { m_iDUR3 = value; }
        }

        /// <summary>
        /// 第四延误代码
        /// </summary>
        public string DELAY4
        {
            get { return m_strDELAY4; }
            set { m_strDELAY4 = value; }
        }

        /// <summary>
        /// 第四延误时间
        /// </summary>
        public int DUR4
        {
            get { return m_iDUR4; }
            set { m_iDUR4 = value; }
        }
        
        /// <summary>
        /// Gate
        /// </summary>
        public string GATE
        {
            get { return m_strGATE; }
            set { m_strGATE = value; }
        }

        /// <summary>
        /// 航班性质代码
        /// </summary>
        public string STC
        {
            get { return m_strSTC; }
            set { m_strSTC = value; }
        }

        /// <summary>
        /// Version
        /// </summary>
        public string VERSION
        {
            get { return m_strVERSION; }
            set { m_strVERSION = value; }
        }

        /// <summary>
        /// Original planned aircraft type
        /// </summary>
        public string ORIG_ACTYP
        {
            get { return m_strORIG_ACTYP; }
            set { m_strORIG_ACTYP = value; }
        }

        /// <summary>
        /// Aircraft type
        /// </summary>
        public string ACTYP
        {
            get { return m_strACTYP; }
            set { m_strACTYP = value; }
        }

        /// <summary>
        /// Aircraft owner/carrier
        /// </summary>
        public string ACOWN
        {
            get { return m_strACOWN; }
            set { m_strACOWN = value; }
        }

        /// <summary>
        /// 操作标识
        /// </summary>
        public string DELACTION
        {
            get { return m_strDELACTION; }
            set { m_strDELACTION = value; }
        }

        /// <summary>
        /// 最后变更序号
        /// </summary>
        public string SEQ
        {
            get { return m_strSEQ; }
            set { m_strSEQ = value; }
        }

        /// <summary>
        /// FOC变更时间
        /// </summary>
        public string TSTAMP
        {
            get { return m_strTSTAMP; }
            set { m_strTSTAMP = value; }
        }

        /// <summary>
        /// 删除标识
        /// </summary>
        public int DeleteTag
        {
            get { return m_iDeleteTag; }
            set { m_iDeleteTag = value; }
        }
    }
}
