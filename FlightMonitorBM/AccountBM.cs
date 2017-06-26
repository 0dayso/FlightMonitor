using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �û�ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable] 
    public class AccountBM
    {
        #region �����ڲ�����
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
        /// ���캯��
        /// </summary>
        public AccountBM()
        {
        }

        /// <summary>
        /// ���캯������һ��DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">������</param>
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

        #region �������Զ���
        /// <summary>
        /// �������IP
        /// </summary>
        public string AgentIP
        {
            get { return _cnvcAgentIP; }
            set { _cnvcAgentIP = value; }
        }

        /// <summary>
        /// �������˿�
        /// </summary>
        public string AgentPort
        {
            get { return _cnvcAgentPort; }
            set { _cnvcAgentPort = value; }
        }

        /// <summary>
        /// �û��ʺ�
        /// </summary>
        public string UserId
        {
            get { return m_strUserId; }
            set { m_strUserId = value; }
        }

        /// <summary>
        /// �û����ͱ���
        /// </summary>
        public int UserTypeId
        {
            get { return m_iUserTypeId; }
            set { m_iUserTypeId = value; }
        }

        /// <summary>
        /// �û���������
        /// </summary>
        public string UserTypeName
        {
            get { return m_strUserTypeName; }
            set { m_strUserTypeName = value; }
        }
        
        /// <summary>
        /// �û�����
        /// </summary>
        public string UserName
        {
            get { return m_strUserName; }
            set { m_strUserName = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string UserPassword
        {
            get { return m_strUserPassword; }
            set { m_strUserPassword = value; }
        }

        /// <summary>
        /// ��վ������
        /// </summary>
        public string StationThreeCode
        {
            get { return m_strStationThreeCode; }
            set { m_strStationThreeCode = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string UserDepartment
        {
            get { return m_strUserDepartment; }
            set { m_strUserDepartment = value; }
        }       
       
        /// <summary>
        /// ˢ��ʱ��
        /// </summary>
        public int RefreshInterval
        {
            get { return m_iRefreshInterval; }
            set { m_iRefreshInterval = value; }
        }

        /// <summary>
        /// ��˸�Ƿ��Զ���ֹ 1���Զ���ֹ 0�����Զ���ֹ
        /// </summary>
        public int SplashAutoStop
        {
            get { return m_iSplashAutoStop; }
            set { m_iSplashAutoStop = value; }
        }

        /// <summary>
        /// �Զ���ֹ��˸ʱ��˸ʱ�䳤��
        /// </summary>
        public int SplashSeconds
        {
            get { return m_iSplashSeconds; }
            set { m_iSplashSeconds = value; }
        }

        /// <summary>
        /// ������ʾ���� 0������1������
        /// </summary>
        public int SoundType
        {
            get { return m_iSoundType; }
            set { m_iSoundType = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ��ʼ����1���ǣ�0����
        /// </summary>
        public int StartGuarantee
        {
            get { return m_iStartGuarantee; }
            set { m_iStartGuarantee = value; }
        }

        /// <summary>
        /// ��ʼ����ʱ�����ƣ����ǰ�೤ʱ�俪ʼ��ʾ��
        /// </summary>
        public int GuaranteeMinutes
        {
            get { return m_iGuaranteeMinutes; }
            set { m_iGuaranteeMinutes = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ�ǻ�1���ǣ�0����
        /// </summary>
        public int Boarding
        {
            get { return m_iBoarding; }
            set { m_iBoarding = value; }
        }

        /// <summary>
        /// �ǻ���ʾʱ�����ƣ����ǰ�೤ʱ����ʾ�ǻ���
        /// </summary>
        public int BoardingMinutes
        {
            get { return m_iBoardingMinutes; }
            set { m_iBoardingMinutes = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ����λ1���ǣ�0����
        /// </summary>
        public int MCCReady
        {
            get { return m_iMCCReady; }
            set { m_iMCCReady = value; }
        }

        /// <summary>
        /// ����λ��ʾʱ�����ƣ��ɻ������೤ʱ����ʾ����λ��
        /// </summary>
        public int MCCReadyMinutes
        {
            get { return m_iMCCReadyMinutes; }
            set { m_iMCCReadyMinutes = value; }
        }

        /// <summary>
        ///�Ƿ���ʾ�������1���ǣ�0����
        /// </summary>
        public int MCCRelease
        {
            get { return m_iMCCRelease; }
            set { m_iMCCRelease = value; }
        }

        /// <summary>
        ///�������ʱ����ʾʱ�����ƣ���վ�ɻ������೤ʱ����ʾ���зɻ���
        /// </summary>
        public int MCCReleasMinutes
        {
            get { return m_iMCCReleasMinutes; }
            set { m_iMCCReleasMinutes = value; }
        }

        /// <summary>
        ///�Ƿ����ؽ�����ʾ1���ǣ�0����
        /// </summary>
        public int TDWNPromt
        {
            get { return m_iTDWNPromt; }
            set { m_iTDWNPromt = value; }
        }

        /// <summary>
        ///����Ԥ�����ʱ��೤ʱ��û����ض�̬ʱ��ʾ�û�
        /// </summary>
        public int TDWNMinutes
        {
            get { return m_iTDWNMinutes; }
            set { m_iTDWNMinutes = value; }
        }

        /// <summary>
        ///�Ƿ����ɽ�����ʾ1���ǣ�0����
        /// </summary>
        public int TOFFPromt
        {
            get { return m_iTOFFPromt; }
            set { m_iTOFFPromt = value; }
        }

        /// <summary>
        ///����Ԥ����ɶ೤ʱ��û����ɶ�̬��ʾ�û�
        /// </summary>
        public int TOFFMinutes
        {
            get { return m_iTOFFMinutes; }
            set { m_iTOFFMinutes = value; }
        }

        /// <summary>
        ///�Ƿ�Թ�վʱ�䲻�������ʾ1���ǣ�0����
        /// </summary>
        public int IntermissionPrompt
        {
            get { return m_iIntermissionPrompt; }
            set { m_iIntermissionPrompt = value; }
        }

        /// <summary>
        ///���ڹ�վʱ��೤ʱ�������ʾ
        /// </summary>
        public int IntermissionMinutes
        {
            get { return m_iIntermissionMinutes; }
            set { m_iIntermissionMinutes = value; }
        }

        /// <summary>
        ///�Ƿ�ԿͲչر���ʾ����׼������������������������壩1���ǣ�0����
        /// </summary>
        public int ClosePaxCabinPromt
        {
            get { return m_iClosePaxCabinPromt; }
            set { m_iClosePaxCabinPromt = value; }
        }

        /// <summary>
        /// ����½��
        /// </summary>
        public int MaxUser
        {
            get { return m_iMaxUser; }
            set { m_iMaxUser = value; }
        }

        /// <summary>
        /// ��½�û���
        /// </summary>
        public int LogUser
        {
            get { return m_iLogUser; }
            set { m_iLogUser = value; }
        }      

        /// <summary>
        /// 1:��ʾȫ������;0:�������õ�������ʾ����
        /// </summary>
        public int DisplayAll
        {
            get { return m_iDisplayAll; }
            set { m_iDisplayAll = value; }        }



        /// <summary>
        /// �Ƿ���ʾ����ĺ���
        /// </summary>
        public int DisplayDelay
        {
            get { return m_iDisplayDelay; }
            set { m_iDisplayDelay = value; }
        }

        /// <summary>
        /// ��ʾ����ʱ�䳬���೤ʱ�����ϵĺ���
        /// </summary>
        public int DelayMinutes
        {
            get { return m_iDelayMinutes; }
            set { m_iDelayMinutes = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ���������ĺ���
        /// </summary>
        public int DisplayDiversion
        {
            get { return m_iDisplayDiversion; }
            set { m_iDisplayDiversion = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ��վ����ĺ���
        /// </summary>
        public int DisplayIntermission
        {
            get { return m_iDisplayIntermission; }
            set { m_iDisplayIntermission = value; }
        }        

        /// <summary>
        /// �Ƿ���ʾû����ض�̬�ĺ���
        /// </summary>
        public int DisplayTDWN
        {
            get { return m_iDisplayTDWN; }
            set { m_iDisplayTDWN = value; }
        }

        /// <summary>
        /// �Ƿ���ʾû����ɶ�̬�ĺ���
        /// </summary>
        public int DisplayTOFF
        {
            get { return m_iDisplayTOFF; }
            set { m_iDisplayTOFF = value; }
        }

        /// <summary>
        /// �Ƿ���ʾû�йز�ʱ��ĺ���
        /// </summary>
        public int DisplayClosePaxCabin
        {
            get { return m_iDisplayClosePaxCabin; }
            set { m_iDisplayClosePaxCabin = value; }
        }

        /// <summary>
        /// �û���IP��ַ
        /// </summary>
        public string IPAddress
        {
            get { return m_strIPAddr; }
            set { m_strIPAddr = value; }
        }

        /// <summary>
        /// �û����һ�η��ʷ�������ʱ��
        /// </summary>
        public string LastUpdateTime
        {
            get { return m_strLastUpdateTime; }
            set { m_strLastUpdateTime = value; }
        }
        #endregion
    }
}
