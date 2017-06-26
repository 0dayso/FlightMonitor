using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// VIP实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-10
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class VIPBM
    {
        #region 对象内部变量
        private int m_iVipID;
        private string m_strDATOP;
        private string m_strFLTID;
        private int m_iLEGNO;
        private string m_strAC;
        private string m_strDEPSTN;
        private string m_strARRSTN;
        private string m_strName;
        private string m_strPOSITION;
        private string m_strCLASS;
        private string m_strVipType;
        private string m_strACCOMPANY_NBR;
        private string m_strACCOMPANY_LEADER;
        private string m_strCONTRACT_NUMBER;
        private string m_strINFORM_PERSON;
        private string m_strSPECIAL_REQUIREMENTS;
        private string m_strREMARKS;
        private string m_strDataSource;
        private int m_iDeleteTag;
        private int m_iHandTag;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public VIPBM()
        {
        }

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public VIPBM(DataRow dataRow)
        {
            m_iVipID = Convert.ToInt32(dataRow["cniVipID"].ToString());
            m_strDATOP = dataRow["cncDATOP"].ToString();
            m_strFLTID = dataRow["cnvcFLTID"].ToString();
            //m_iLEGNO = Convert.ToInt32(dataRow["cniLEGNO"].ToString());
            //m_strAC = dataRow["cnvcAC"].ToString();
            m_strDEPSTN = dataRow["cncDEPSTN"].ToString();
            m_strARRSTN = dataRow["cncARRSTN"].ToString();
            m_strName = dataRow["cnvcNAME"].ToString();
            m_strPOSITION = dataRow["cnvcPOSITION"].ToString();
            m_strCLASS = dataRow["cncCLASS"].ToString();
            m_strVipType = dataRow["cnvcVipType"].ToString();
            m_strACCOMPANY_NBR = dataRow["cnvcACCOMPANY_NBR"].ToString();
            m_strACCOMPANY_LEADER = dataRow["cnvcACCOMPANY_LEADER"].ToString();
            m_strCONTRACT_NUMBER = dataRow["cnvcCONTRACT_NUMBER"].ToString();
            m_strINFORM_PERSON = dataRow["cnvcINFORM_PERSON"].ToString();
            m_strSPECIAL_REQUIREMENTS = dataRow["cnvcSPECIAL_REQUIREMENTS"].ToString();
            m_strREMARKS = dataRow["cnvcREMARKS"].ToString();
            m_strDataSource = dataRow["cnvcDataSource"].ToString();
            m_iDeleteTag = Convert.ToInt32(dataRow["cnbDeleteTag"].ToString());
            m_iHandTag = Convert.ToInt32(dataRow["cnbHandTag"].ToString());
        }

        #region modified by LinYong in 2013.08.19
        /*

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public VIPBM(DataRow dataRow, int iOracle)
        {
            //Oracle 数据库的处理
            m_strDATOP = dataRow["DATOP"].ToString().Trim();
            m_strFLTID = dataRow["FLTID"].ToString().Trim();
            m_strDEPSTN = dataRow["DEPSTN"].ToString().Trim();
            m_strARRSTN = dataRow["ARRSTN"].ToString().Trim();
            m_strName = UnicodeToChinese(dataRow["NAME"].ToString().Replace(" ", "")).Trim();
            m_strPOSITION = UnicodeToChinese(dataRow["POSITION"].ToString().Replace(" ", "")).Trim();
            m_strCLASS = dataRow["CLASS"].ToString().Trim();
            m_strACCOMPANY_NBR = dataRow["ACCOMPANY_NBR"].ToString().Trim();
            m_strACCOMPANY_LEADER = UnicodeToChinese(dataRow["ACCOMPANY_LEADER"].ToString().Replace(" ", "")).Trim();
            m_strCONTRACT_NUMBER = dataRow["CONTRACT_NUMBER"].ToString().Trim();
            m_strINFORM_PERSON = UnicodeToChinese(dataRow["INFORM_PERSON"].ToString().Replace(" ", "")).Trim();
            m_strSPECIAL_REQUIREMENTS = UnicodeToChinese(dataRow["SPECIAL_REQUIREMENTS"].ToString().Replace(" ", "")).Trim();
            m_strREMARKS = UnicodeToChinese(dataRow["REMARKS"].ToString().Replace(" ", "")).Trim();

        }    
   
        */

        /// <summary>
        /// 构造函数：把一行DataRow数据赋给实体对象
        /// 函数说明：把对fleetwatch Oracle 的处理变更为 fleetwatch SQL 数据库 的 处理
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public VIPBM(DataRow dataRow, int iOracle)
        {

            //SQL 数据库的处理
            m_strDATOP = dataRow["DATOP"].ToString().Trim();
            m_strFLTID = dataRow["FLTID"].ToString().Trim();
            m_strDEPSTN = dataRow["DEPSTN"].ToString().Trim();
            m_strARRSTN = dataRow["ARRSTN"].ToString().Trim();
            m_strName = dataRow["NAME"].ToString().Replace(" ", "").Trim();
            m_strPOSITION = dataRow["POSITION"].ToString().Replace(" ", "").Trim();
            m_strCLASS = dataRow["CLASS"].ToString().Trim();
            m_strACCOMPANY_NBR = dataRow["ACCOMPANY_NBR"].ToString().Trim();
            m_strACCOMPANY_LEADER = dataRow["ACCOMPANY_LEADER"].ToString().Replace(" ", "").Trim();
            m_strCONTRACT_NUMBER = dataRow["CONTRACT_NUMBER"].ToString().Trim();
            m_strINFORM_PERSON = dataRow["INFORM_PERSON"].ToString().Replace(" ", "").Trim();
            m_strSPECIAL_REQUIREMENTS = dataRow["SPECIAL_REQUIREMENTS"].ToString().Replace(" ", "").Trim();
            m_strREMARKS = dataRow["REMARKS"].ToString().Replace(" ", "").Trim();

        }

        #endregion modified by LinYong in 2013.08.19

        /// <summary>
        /// ID 代理主键
        /// </summary>
        public int VipID
        {
            get { return m_iVipID; }
            set { m_iVipID = value; }
        }

        /// <summary>
        /// 航班日期 UTC时间
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }

        /// <summary>
        /// 航班号
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
        /// AC
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
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
        /// 到达站三字码
        /// </summary>
        public string ARRSTN
        {
            get { return m_strARRSTN; }
            set { m_strARRSTN = value; }
        }
       
        /// <summary>
        /// 旅客性命
        /// </summary>
        public string Name
        {
            get { return m_strName; }
            set { m_strName = value; }
        }

        /// <summary>
        /// 职位
        /// </summary>
        public string POSITION
        {
            get { return m_strPOSITION; }
            set { m_strPOSITION = value; }
        }

        /// <summary>
        /// 舱位等级
        /// </summary>
        public string CLASS
        {
            get { return m_strCLASS; }
            set { m_strCLASS = value; }
        }

        /// <summary>
        /// VIP类型
        /// </summary>
        public string VipType
        {
            get { return m_strVipType; }
            set { m_strVipType = value; }
        }

        /// <summary>
        /// 陪同人数
        /// </summary>
        public string ACCOMPANY_NBR
        {
            get { return m_strACCOMPANY_NBR; }
            set { m_strACCOMPANY_NBR = value; }
        }

        /// <summary>
        /// 陪同人员
        /// </summary>
        public string ACCOMPANY_LEADER
        {
            get { return m_strACCOMPANY_LEADER; }
            set { m_strACCOMPANY_LEADER = value; }
        }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string CONTRACT_NUMBER
        {
            get { return m_strCONTRACT_NUMBER; }
            set { m_strCONTRACT_NUMBER = value; }
        }

        /// <summary>
        /// 联系人
        /// </summary>
        public string INFORM_PERSON
        {
            get { return m_strINFORM_PERSON; }
            set { m_strINFORM_PERSON = value; }
        }

        /// <summary>
        /// 特殊要求
        /// </summary>
        public string SPECIAL_REQUIREMENTS
        {
            get { return m_strSPECIAL_REQUIREMENTS; }
            set { m_strSPECIAL_REQUIREMENTS = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARKS
        {
            get { return m_strREMARKS; }
            set { m_strREMARKS = value; }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource
        {
            get { return m_strDataSource; }
            set { m_strDataSource = value; }
        }

        /// <summary>
        /// 将Unicode代码转换成中文
        /// </summary>
        /// <param name="strUnicode">unicode代码</param>
        /// <returns>转换后的字符</returns>
        private string UnicodeToChinese(string strUnicode)
        {

            //776D 5753 0177 9F53 3F65 4F53 3B4E 2D5E  海南省原政协主席

            byte[] array = new byte[2000];//最多1000个汉字

            string[] s = new string[2000];

            int[] t = new int[2000];



            int strlen = strUnicode.Length;



            for (int n = 0; n < strlen / 2; n++)

                s[n] = strUnicode.Substring(n * 2, 2);



            for (int i = 0; i < strlen / 2; i++)

                t[i] = Convert.ToInt32(s[i], 16);



            for (int m = 0; m < strlen / 2; m++)

                array[m] = (byte)t[m];



            string strReturn = System.Text.Encoding.Unicode.GetString(array, 0, strlen);

            return strReturn;

        }

    }
}
