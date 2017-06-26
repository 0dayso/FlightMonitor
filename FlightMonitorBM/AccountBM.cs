using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 用户实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    [Serializable] 
    public class AccountBM
    {
        #region 对象内部变量
        private string m_strUserId;
        private int m_iUserTypeId;
        private string m_strUserTypeName;
        private string m_strUserName;
        private string m_strUserPassword;
        private string m_strStationThreeCode;
        private string m_strUserDepartment;
        private int m_iRefreshInterval;
        private int m_iSplashAutoStop;
        private int m_iSplashSeconds;
        private int m_iSoundType;
        private int m_iStartGuarantee;
        private int m_iGuaranteeMinutes;
        private int m_iBoarding;
        private int m_iBoardingMinutes;
        private int m_iMCCReady;
        private int m_iMCCReadyMinutes;
        private int m_iMCCRelease;
        private int m_iMCCReleasMinutes;
        private int m_iTDWNPromt;
        private int m_iTDWNMinutes;
        private int m_iTOFFPromt;
        private int m_iTOFFMinutes;
        private int m_iIntermissionPrompt;
        private int m_iIntermissionMinutes;
        private int m_iClosePaxCabinPromt;
        private int m_iDisplayAll;
        private int m_iDisplayDelay;
        private int m_iDelayMinutes;
        private int m_iDisplayDiversion;
        private int m_iDisplayIntermission;
        private int m_iDisplayTDWN;
        private int m_iDisplayTOFF;
        private int m_iDisplayClosePaxCabin;
        private int m_iMaxUser;
        private int m_iLogUser;
        private string m_strIPAddr;
        private string m_strLastUpdateTime;

        private string _cnvcAgentIP;
        private string _cnvcAgentPort;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountBM()
        {
        }

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public AccountBM(DataRow dataRow)
        {
            m_strUserId = dataRow["cnvcUserId"].ToString();
            m_iUserTypeId = Convert.ToInt32(dataRow["cniUserTypeId"].ToString());
            m_strUserTypeName = dataRow["cnvcUserTypeName"].ToString();
            m_strUserName = dataRow["cnvcUserName"].ToString();
            m_strUserPassword = dataRow["cnvcUserPassword"].ToString();
            m_strStationThreeCode = dataRow["cncStationThreeCode"].ToString();
            m_strUserDepartment = dataRow["cnvcUserDepartment"].ToString();
            m_iRefreshInterval = Convert.ToInt32(dataRow["cniRefreshInterval"].ToString());
            m_iSplashAutoStop = Convert.ToInt32(dataRow["cniSplashAutoStop"].ToString());
            m_iSplashSeconds = Convert.ToInt32(dataRow["cniSplashSeconds"].ToString());
            m_iSoundType = Convert.ToInt32(dataRow["cniSoundType"].ToString());
            m_iStartGuarantee = Convert.ToInt32(dataRow["cniStartGuarantee"].ToString());
            m_iGuaranteeMinutes = Convert.ToInt32(dataRow["cniGuaranteeMinutes"].ToString());
            m_iBoarding = Convert.ToInt32(dataRow["cniBoarding"].ToString());
            m_iBoardingMinutes = Convert.ToInt32(dataRow["cniBoardingMinutes"].ToString());
            m_iMCCReady = Convert.ToInt32(dataRow["cniMCCReady"].ToString());
            m_iMCCReadyMinutes = Convert.ToInt32(dataRow["cniMCCReadyMinutes"].ToString());
            m_iMCCRelease = Convert.ToInt32(dataRow["cniMCCRelease"].ToString());
            m_iMCCReleasMinutes = Convert.ToInt32(dataRow["cniMCCReleasMinutes"].ToString());
            m_iTDWNPromt = Convert.ToInt32(dataRow["cniTDWNPromt"].ToString());
            m_iTDWNMinutes = Convert.ToInt32(dataRow["cniTDWNMinutes"].ToString());
            m_iTOFFPromt = Convert.ToInt32(dataRow["cniTOFFPromt"].ToString());
            m_iTOFFMinutes = Convert.ToInt32(dataRow["cniTOFFMinutes"].ToString());
            m_iIntermissionPrompt = Convert.ToInt32(dataRow["cniIntermissionPrompt"].ToString());
            m_iIntermissionMinutes = Convert.ToInt32(dataRow["cniIntermissionMinutes"].ToString());
            m_iClosePaxCabinPromt = Convert.ToInt32(dataRow["cniClosePaxCabinPromt"].ToString());
            m_iDisplayAll = Convert.ToInt32(dataRow["cniDisplayAll"].ToString());
            m_iDisplayDelay = Convert.ToInt32(dataRow["cniDisplayDelay"].ToString());
            m_iDelayMinutes = Convert.ToInt32(dataRow["cniDelayMinutes"].ToString());
            m_iDisplayDiversion = Convert.ToInt32(dataRow["cniDisplayDiversion"].ToString());
            m_iDisplayIntermission = Convert.ToInt32(dataRow["cniDisplayIntermission"].ToString());
            m_iDisplayTDWN = Convert.ToInt32(dataRow["cniDisplayTDWN"].ToString());
            m_iDisplayTOFF = Convert.ToInt32(dataRow["cniDisplayTOFF"].ToString());
            m_iDisplayClosePaxCabin = Convert.ToInt32(dataRow["cniDisplayClosePaxCabin"].ToString());
            m_iMaxUser = Convert.ToInt32(dataRow["cniMaxUser"].ToString());
            m_iLogUser = Convert.ToInt32(dataRow["cniLogUser"].ToString());

            _cnvcAgentIP = dataRow["cnvcAgentIP"].ToString().Trim();
            _cnvcAgentPort = dataRow["cnvcAgentPort"].ToString().Trim();
        }

        #region 对象属性定义
        /// <summary>
        /// 代理服务IP
        /// </summary>
        public string AgentIP
        {
            get { return _cnvcAgentIP; }
            set { _cnvcAgentIP = value; }
        }

        /// <summary>
        /// 代理服务端口
        /// </summary>
        public string AgentPort
        {
            get { return _cnvcAgentPort; }
            set { _cnvcAgentPort = value; }
        }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserId
        {
            get { return m_strUserId; }
            set { m_strUserId = value; }
        }

        /// <summary>
        /// 用户类型编码
        /// </summary>
        public int UserTypeId
        {
            get { return m_iUserTypeId; }
            set { m_iUserTypeId = value; }
        }

        /// <summary>
        /// 用户类型名称
        /// </summary>
        public string UserTypeName
        {
            get { return m_strUserTypeName; }
            set { m_strUserTypeName = value; }
        }
        
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get { return m_strUserName; }
            set { m_strUserName = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword
        {
            get { return m_strUserPassword; }
            set { m_strUserPassword = value; }
        }

        /// <summary>
        /// 航站三字码
        /// </summary>
        public string StationThreeCode
        {
            get { return m_strStationThreeCode; }
            set { m_strStationThreeCode = value; }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string UserDepartment
        {
            get { return m_strUserDepartment; }
            set { m_strUserDepartment = value; }
        }       
       
        /// <summary>
        /// 刷新时间
        /// </summary>
        public int RefreshInterval
        {
            get { return m_iRefreshInterval; }
            set { m_iRefreshInterval = value; }
        }

        /// <summary>
        /// 闪烁是否自动终止 1：自动终止 0：不自动终止
        /// </summary>
        public int SplashAutoStop
        {
            get { return m_iSplashAutoStop; }
            set { m_iSplashAutoStop = value; }
        }

        /// <summary>
        /// 自动终止闪烁时闪烁时间长短
        /// </summary>
        public int SplashSeconds
        {
            get { return m_iSplashSeconds; }
            set { m_iSplashSeconds = value; }
        }

        /// <summary>
        /// 声音提示类型 0：音箱1：主板
        /// </summary>
        public int SoundType
        {
            get { return m_iSoundType; }
            set { m_iSoundType = value; }
        }

        /// <summary>
        /// 是否提示开始保障1：是；0：否
        /// </summary>
        public int StartGuarantee
        {
            get { return m_iStartGuarantee; }
            set { m_iStartGuarantee = value; }
        }

        /// <summary>
        /// 开始保障时间限制（起飞前多长时间开始提示）
        /// </summary>
        public int GuaranteeMinutes
        {
            get { return m_iGuaranteeMinutes; }
            set { m_iGuaranteeMinutes = value; }
        }

        /// <summary>
        /// 是否提示登机1：是；0：否
        /// </summary>
        public int Boarding
        {
            get { return m_iBoarding; }
            set { m_iBoarding = value; }
        }

        /// <summary>
        /// 登机提示时间限制（起飞前多长时间提示登机）
        /// </summary>
        public int BoardingMinutes
        {
            get { return m_iBoardingMinutes; }
            set { m_iBoardingMinutes = value; }
        }

        /// <summary>
        /// 是否提示机务到位1：是；0：否
        /// </summary>
        public int MCCReady
        {
            get { return m_iMCCReady; }
            set { m_iMCCReady = value; }
        }

        /// <summary>
        /// 机务到位提示时间限制（飞机到达后多长时间提示机务到位）
        /// </summary>
        public int MCCReadyMinutes
        {
            get { return m_iMCCReadyMinutes; }
            set { m_iMCCReadyMinutes = value; }
        }

        /// <summary>
        ///是否提示机务放行1：是；0：否
        /// </summary>
        public int MCCRelease
        {
            get { return m_iMCCRelease; }
            set { m_iMCCRelease = value; }
        }

        /// <summary>
        ///机务放行时间提示时间限制（过站飞机到达后多长时间提示放行飞机）
        /// </summary>
        public int MCCReleasMinutes
        {
            get { return m_iMCCReleasMinutes; }
            set { m_iMCCReleasMinutes = value; }
        }

        /// <summary>
        ///是否对落地进行提示1：是；0：否
        /// </summary>
        public int TDWNPromt
        {
            get { return m_iTDWNPromt; }
            set { m_iTDWNPromt = value; }
        }

        /// <summary>
        ///超过预计落地时间多长时间没有落地动态时提示用户
        /// </summary>
        public int TDWNMinutes
        {
            get { return m_iTDWNMinutes; }
            set { m_iTDWNMinutes = value; }
        }

        /// <summary>
        ///是否对起飞进行提示1：是；0：否
        /// </summary>
        public int TOFFPromt
        {
            get { return m_iTOFFPromt; }
            set { m_iTOFFPromt = value; }
        }

        /// <summary>
        ///超过预计起飞多长时间没有起飞动态提示用户
        /// </summary>
        public int TOFFMinutes
        {
            get { return m_iTOFFMinutes; }
            set { m_iTOFFMinutes = value; }
        }

        /// <summary>
        ///是否对过站时间不足进行提示1：是；0：否
        /// </summary>
        public int IntermissionPrompt
        {
            get { return m_iIntermissionPrompt; }
            set { m_iIntermissionPrompt = value; }
        }

        /// <summary>
        ///少于过站时间多长时间进行提示
        /// </summary>
        public int IntermissionMinutes
        {
            get { return m_iIntermissionMinutes; }
            set { m_iIntermissionMinutes = value; }
        }

        /// <summary>
        ///是否对客舱关闭提示（标准见表ｔｂｉｎｔｅｒｍｉｓｓｉｏｎｔｉｍｅ）1：是；0：否
        /// </summary>
        public int ClosePaxCabinPromt
        {
            get { return m_iClosePaxCabinPromt; }
            set { m_iClosePaxCabinPromt = value; }
        }

        /// <summary>
        /// 最大登陆数
        /// </summary>
        public int MaxUser
        {
            get { return m_iMaxUser; }
            set { m_iMaxUser = value; }
        }

        /// <summary>
        /// 登陆用户数
        /// </summary>
        public int LogUser
        {
            get { return m_iLogUser; }
            set { m_iLogUser = value; }
        }      

        /// <summary>
        /// 1:显示全部航班;0:按照设置的条件显示航班
        /// </summary>
        public int DisplayAll
        {
            get { return m_iDisplayAll; }
            set { m_iDisplayAll = value; }        }



        /// <summary>
        /// 是否显示延误的航班
        /// </summary>
        public int DisplayDelay
        {
            get { return m_iDisplayDelay; }
            set { m_iDisplayDelay = value; }
        }

        /// <summary>
        /// 显示延误时间超过多长时间以上的航班
        /// </summary>
        public int DelayMinutes
        {
            get { return m_iDelayMinutes; }
            set { m_iDelayMinutes = value; }
        }

        /// <summary>
        /// 是否显示返航备降的航班
        /// </summary>
        public int DisplayDiversion
        {
            get { return m_iDisplayDiversion; }
            set { m_iDisplayDiversion = value; }
        }

        /// <summary>
        /// 是否显示过站不足的航班
        /// </summary>
        public int DisplayIntermission
        {
            get { return m_iDisplayIntermission; }
            set { m_iDisplayIntermission = value; }
        }        

        /// <summary>
        /// 是否显示没有落地动态的航班
        /// </summary>
        public int DisplayTDWN
        {
            get { return m_iDisplayTDWN; }
            set { m_iDisplayTDWN = value; }
        }

        /// <summary>
        /// 是否显示没有起飞动态的航班
        /// </summary>
        public int DisplayTOFF
        {
            get { return m_iDisplayTOFF; }
            set { m_iDisplayTOFF = value; }
        }

        /// <summary>
        /// 是否显示没有关舱时间的航班
        /// </summary>
        public int DisplayClosePaxCabin
        {
            get { return m_iDisplayClosePaxCabin; }
            set { m_iDisplayClosePaxCabin = value; }
        }

        /// <summary>
        /// 用户的IP地址
        /// </summary>
        public string IPAddress
        {
            get { return m_strIPAddr; }
            set { m_strIPAddr = value; }
        }

        /// <summary>
        /// 用户最后一次访问服务器的时间
        /// </summary>
        public string LastUpdateTime
        {
            get { return m_strLastUpdateTime; }
            set { m_strLastUpdateTime = value; }
        }
        #endregion
    }
}
