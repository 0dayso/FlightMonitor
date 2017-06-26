using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ���ද̬���ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable] 
    public class ChangeLegsBM
    {
        #region �����ڲ�����
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
        /// ���캯��
        /// </summary>
        public ChangeLegsBM()
        {
        }

        
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dataRow">��XML����ļ���ȡ��������</param>
        /// <param name="iXMLTag">��ʶ���������ع��캯��</param>
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
        /// ���캯��
        /// </summary>
        /// <param name="dataRow">������</param>
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
        /// FOC��������
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }

        /// <summary>
        /// FOC�����
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// �������
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
        /// ����ʱ�亽������
        /// </summary>
        public string FlightDate
        {
            get { return m_strFlightDate; }
            set { m_strFlightDate = value; }
        }

        /// <summary>
        /// ����ϵͳ��������
        /// </summary>
        public string CKIFlightDate
        {
            get { return m_strCKIFlightDate; }
            set { m_strCKIFlightDate = value; }
        }


        /// <summary>
        /// �����
        /// </summary>
        public string FlightNo
        {
            get { return m_strFlightNo; }
            set { m_strFlightNo = value; }
        }

        /// <summary>
        /// ����ϵͳ�����
        /// </summary>
        public string CKIFlightNo
        {
            get { return m_strCKIFlightNo; }
            set { m_strCKIFlightNo = value; }
        }


        /// <summary>
        /// ���ɻ�ע���
        /// </summary>
        public string LONG_REG
        {
            get { return m_strLONG_REG; }
            set { m_strLONG_REG = value; }
        }

        /// <summary>
        /// ʼ��վ������
        /// </summary>
        public string DEPSTN
        {
            get { return m_strDEPSTN; }
            set { m_strDEPSTN = value; }
        }

        /// <summary>
        /// ʼ��վ����������
        /// </summary>
        public string CityDEPSTN
        {
            get { return m_strCityDEPSTN; }
            set { m_strCityDEPSTN = value; }
        }

        /// <summary>
        /// ʼ��վ������
        /// </summary>
        public string DEPFourCode
        {
            get { return m_strDEPFourCode; }
            set { m_strDEPFourCode = value; }
        }


        /// <summary>
        /// ����վ������
        /// </summary>
        public string ARRSTN
        {
            get { return m_strARRSTN; }
            set { m_strARRSTN = value; }
        }

        /// <summary>
        /// ����վ����������
        /// </summary>
        public string CityARRSTN
        {
            get { return m_strCityARRSTN; }
            set { m_strCityARRSTN = value; }
        }

        /// <summary>
        /// ����վ����������
        /// </summary>
        public string ARRFourCode
        {
            get { return m_strARRFourCode; }
            set { m_strARRFourCode = value; }
        }
        
        /// <summary>
        /// �ƻ����ʱ��
        /// </summary>
        public string STD
        {
            get { return m_strSTD; }
            set { m_strSTD = value; }
        }

        /// <summary>
        /// �ƻ�����ʱ��
        /// </summary>
        public string STA
        {
            get { return m_strSTA; }
            set { m_strSTA = value; }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public string STATUS
        {
            get { return m_strSTATUS; }
            set { m_strSTATUS = value; }
        }
       
        
        /// <summary>
        /// Ԥ�����ʱ��
        /// </summary>
        public string ETD
        {
            get { return m_strETD; }
            set { m_strETD = value; }
        }

        /// <summary>
        /// Ԥ�Ƶ���ʱ��
        /// </summary>
        public string ETA
        {
            get { return m_strETA; }
            set { m_strETA = value; }
        }        

        /// <summary>
        /// ���յ�ʱ��
        /// </summary>
        public string ATD
        {
            get { return m_strATD; }
            set { m_strATD = value; }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string TOFF
        {
            get { return m_strTOFF; }
            set { m_strTOFF = value; }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public string TDWN
        {
            get { return m_strTDWN; }
            set { m_strTDWN = value; }
        }

        /// <summary>
        /// ���յ�ʱ��
        /// </summary>
        public string ATA
        {
            get { return m_strATA; }
            set { m_strATA = value; }
        }
        
        /// <summary>
        /// Triangular flight ID of the ��other�� sector
        /// </summary>
        public string TRI_FLTID
        {
            get { return m_strTRI_FLTID; }
            set { m_strTRI_FLTID = value; }
        }

        /// <summary>
        /// ��������ԭ�����
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
        /// ��һ�������
        /// </summary>
        public string DELAY1
        {
            get { return m_strDELAY1; }
            set { m_strDELAY1 = value; }
        }

        /// <summary>
        /// ��һ����ʱ��
        /// </summary>
        public int DUR1
        {
            get { return m_iDUR1; }
            set { m_iDUR1 = value; }
        }

        /// <summary>
        /// �ڶ��������
        /// </summary>
        public string DELAY2
        {
            get { return m_strDELAY2; }
            set { m_strDELAY2 = value; }
        }

        /// <summary>
        /// �ڶ�����ʱ��
        /// </summary>
        public int DUR2
        {
            get { return m_iDUR2; }
            set { m_iDUR2 = value; }
        }

       /// <summary>
        /// �����������
        /// </summary>
        public string DELAY3
        {
            get { return m_strDELAY3; }
            set { m_strDELAY3 = value; }
        }

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public int DUR3
        {
            get { return m_iDUR3; }
            set { m_iDUR3 = value; }
        }

        /// <summary>
        /// �����������
        /// </summary>
        public string DELAY4
        {
            get { return m_strDELAY4; }
            set { m_strDELAY4 = value; }
        }

        /// <summary>
        /// ��������ʱ��
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
        /// �������ʴ���
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
        /// ������ʶ
        /// </summary>
        public string DELACTION
        {
            get { return m_strDELACTION; }
            set { m_strDELACTION = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string SEQ
        {
            get { return m_strSEQ; }
            set { m_strSEQ = value; }
        }

        /// <summary>
        /// FOC���ʱ��
        /// </summary>
        public string TSTAMP
        {
            get { return m_strTSTAMP; }
            set { m_strTSTAMP = value; }
        }

        /// <summary>
        /// ɾ����ʶ
        /// </summary>
        public int DeleteTag
        {
            get { return m_iDeleteTag; }
            set { m_iDeleteTag = value; }
        }
    }
}
