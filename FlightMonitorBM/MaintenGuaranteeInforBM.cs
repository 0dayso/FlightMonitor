using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 数据项维护实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-06-14
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class MaintenGuaranteeInforBM
    {
        #region 对象内部变量
        private string m_strDATOP;
        private string m_strFLTID;
        private string m_strLEGNO;
        private string m_strAC;
        private string m_strFieldName;
        private int m_iFieldLength;
        private string m_strColumnCaption;
        private string m_strOldContent;
        private string m_strNewContent;
        private string m_strNewText;
        private int m_iFieldType;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public MaintenGuaranteeInforBM()
        {
        }

        /// <summary>
        /// 进港主键
        /// </summary>
        public string DATOP
        {
            get { return m_strDATOP; }
            set { m_strDATOP = value; }
        }


        /// <summary>
        /// 进港主键
        /// </summary>
        public string FLTID
        {
            get { return m_strFLTID; }
            set { m_strFLTID = value; }
        }

        /// <summary>
        /// 进港主键
        /// </summary>
        public string LEGNO
        {
            get { return m_strLEGNO; }
            set { m_strLEGNO = value; }
        }

        /// <summary>
        /// 进港主键
        /// </summary>
        public string AC
        {
            get { return m_strAC; }
            set { m_strAC = value; }
        }

        /// <summary>
        /// 更新字段名
        /// </summary>
        public string FieldName
        {
            get { return m_strFieldName; }
            set { m_strFieldName = value; }
        }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int FieldLength
        {
            get { return m_iFieldLength; }
            set { m_iFieldLength = value; }
        }

        /// <summary>
        /// 行标题
        /// </summary>
        public string ColumnCaption
        {
            get { return m_strColumnCaption; }
            set { m_strColumnCaption = value; }
        }


        /// <summary>
        /// 旧值
        /// </summary>
        public string OldContent
        {
            get { return m_strOldContent; }
            set { m_strOldContent = value; }
        }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewContent
        {
            get { return m_strNewContent; }
            set { m_strNewContent = value; }
        }

        public string NewText
        {
            get { return m_strNewText; }
            set { m_strNewText = value; }
        }

        /// <summary>
        /// 字段类型 1：文本 2：数字
        /// </summary>
        public int FieldType
        {
            get { return m_iFieldType; }
            set { m_iFieldType = value; }
        }

    }
}
