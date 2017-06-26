using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 数据库实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class DataItemPurviewBM
    {
        /// <summary>
        /// ACARS电报类型
        /// </summary>
        public enum DataType : uint
        {
            ITEM = 1,
            PURVIEW = 2
        }

        #region 对象内部变量
        private int m_iDataItemNO;
        private string m_strDataItemID;
        private string m_strDataItemName;
        private string m_strDataItemChangeName;
        private string m_strPrimaryCodeField;
        private string m_strPrimaryNameField;
        private string m_strForeignTable;
        private string m_strForeignCodeField;
        private string m_strForeignNameField;
        private int m_iFieldLength;
        private int m_iColumnWidth;
        private int m_iInOROut;
        private int m_iMaintenType;
        private int m_iFieldType;
        private int m_iDataItemPurview;
        private int m_iDataItemVisible;
        private int m_iViewIndex;
        private int m_iSplashPromptItem;
        private int m_iSoundPromptItem;
        private string m_strUserID;
        
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataItemPurviewBM()
        {
        }

        /// <summary>
        /// 构造函数:把一行DataRow数据赋给实体对象
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public DataItemPurviewBM(DataRow dataRow, DataType dataType)
        {
            m_iDataItemNO = Convert.ToInt32(dataRow["cniDataItemNo"].ToString());
            m_strDataItemID = dataRow["cnvcDataItemID"].ToString();
            m_strDataItemName = dataRow["cnvcDataItemName"].ToString();
            m_strDataItemChangeName = dataRow["cnvcDataItemChangeName"].ToString();
            m_strPrimaryCodeField = dataRow["cnvcPrimaryCodeField"].ToString();
            m_strPrimaryNameField = dataRow["cnvcPrimaryNameField"].ToString();
            m_strForeignTable = dataRow["cnvcForeignTable"].ToString();
            m_strForeignCodeField = dataRow["cnvcForeignCodeField"].ToString();
            m_strForeignNameField = dataRow["cnvcForeignNameField"].ToString();
            m_iFieldLength = Convert.ToInt32(dataRow["cniFieldLength"].ToString());
            m_iColumnWidth = Convert.ToInt32(dataRow["cniColumnWidth"].ToString());
            m_iInOROut = Convert.ToInt32(dataRow["cniInOROut"].ToString());
            m_iMaintenType = Convert.ToInt32(dataRow["cniMaintenType"].ToString());
            m_iFieldType = Convert.ToInt32(dataRow["cniFieldType"].ToString());

            if (dataType == DataType.PURVIEW)
            {
                m_iDataItemPurview = Convert.ToInt32(dataRow["cniDataItemPurview"].ToString());
                m_iDataItemVisible = Convert.ToInt32(dataRow["cniDataItemVisible"].ToString());
                m_iViewIndex = Convert.ToInt32(dataRow["cniViewIndex"].ToString());
                m_iSplashPromptItem = Convert.ToInt32(dataRow["cniSplashPromptItem"].ToString());
                m_iSoundPromptItem = Convert.ToInt32(dataRow["cniSoundPromptItem"].ToString());
                m_strUserID = dataRow["cnvcUserID"].ToString();
            }
           
        }    
        
       

        /// <summary>
        /// 数据项序号
        /// </summary>
        public int DataItemNO
        {
            get { return m_iDataItemNO; }
            set { m_iDataItemNO = value; }
        }

        /// <summary>
        /// 数据项编号
        /// </summary>
        public string DataItemID
        {
            get { return m_strDataItemID; }
            set { m_strDataItemID = value; }
        }

        /// <summary>
        /// 数据项名称
        /// </summary>
        public string DataItemName
        {
            get { return m_strDataItemName; }
            set { m_strDataItemName = value; }
        }

        /// <summary>
        /// 数据项变更名称
        /// </summary>
        public string DataItemChangeName
        {
            get { return m_strDataItemChangeName; }
            set { m_strDataItemChangeName = value; }
        }       

       
        /// <summary>
        /// 主表代码字段
        /// </summary>
        public string PrimaryCodeField
        {
            get { return m_strPrimaryCodeField; }
            set { m_strPrimaryCodeField = value; }
        }

       
        /// <summary>
        /// 主表名称字段
        /// </summary>
        public string PrimaryNameField
        {
            get { return m_strPrimaryNameField; }
            set { m_strPrimaryNameField = value; }
        }

        /// <summary>
        /// 外键表
        /// </summary>
        public string ForeignTable
        {
            get { return m_strForeignTable; }
            set { m_strForeignTable = value; }
        }


        /// <summary>
        /// 外键表代码字段
        /// </summary>
        public string ForeignCodeField
        {
            get { return m_strForeignCodeField; }
            set { m_strForeignCodeField = value; }
        }


        /// <summary>
        /// 外键表名称字段
        /// </summary>
        public string ForeignNameField
        {
            get { return m_strForeignNameField; }
            set { m_strForeignNameField = value; }
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
        /// 单元格宽度
        /// </summary>
        public int ColumnWidth
        {
            get { return m_iColumnWidth; }
            set { m_iColumnWidth = value; }
        }

        /// <summary>
        /// 进港还是出港航班
        /// </summary>
        public int InOROut
        {
            get { return m_iInOROut; }
            set { m_iInOROut = value; }
        }

        /// <summary>
        /// 维护数据类型
        /// </summary>
        public int MaintenType
        {
            get { return m_iMaintenType; }
            set { m_iMaintenType = value; }
        }

        /// <summary>
        /// 字段类型 1：文本 2：数字
        /// </summary>
        public int FieldType
        {
            get { return m_iFieldType; }
            set { m_iFieldType = value; }
        }

        //private int m_iDataItemPurview;
        //private int m_iDataItemVisible;
        //private int m_iViewIndex;
        //private int m_iSplashPromptItem;
        //private int m_iSoundPromptItem;
        //private string m_strUserID;

        /// <summary>
        /// 权限 0:无;1:浏览;2:维护
        /// </summary>
        public int DataItemPurview
        {
            get { return m_iDataItemPurview; }
            set { m_iDataItemPurview = value; }
        }

        /// <summary>
        /// 数据项是否可见
        /// </summary>
        public int DataItemVisible
        {
            get { return m_iDataItemVisible; }
            set { m_iDataItemVisible = value; }
        }
        
        /// <summary>
        /// 数据项显示顺序
        /// </summary>
        public int ViewIndex
        {
            get { return m_iViewIndex; }
            set { m_iViewIndex = value; }
        }

        /// <summary>
        /// 是否闪烁提示
        /// </summary>
        public int SplashPromptItem
        {
            get { return m_iSplashPromptItem; }
            set { m_iSplashPromptItem = value; }
        }

        /// <summary>
        /// 声音提示
        /// </summary>
        public int SoundPromptItem
        {
            get { return m_iSoundPromptItem; }
            set { m_iSoundPromptItem = value; }
        }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserID
        {
            get { return m_strUserID; }
            set { m_strUserID = value; }
        }

    }
}
