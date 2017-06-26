using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 数据项权限数据访问操作
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-23
    /// 修 改 人：张  黎
    /// 修改日期：2008-07-01
    /// 版    本：
    public class DataItemPurviewDA:SqlDatabase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataItemPurviewDA()
        {
        }

        #region 获取所有数据项
        /// <summary>
        /// 查询数据项SQL语句
        /// </summary>
        private const string SELECT_DataItem = "Select * from tbDataItem";       

        /// <summary>
        /// 获取所有的数据项
        /// </summary>
        /// <returns>包含所有数据项的数据表</returns>
        public DataTable GetDataItems()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItem);
        }
        #endregion

        #region  获取某用户的所有数据项权限
        /// <summary>
        /// 获取用户的所有数据项访问权限
        /// </summary>
        /// <param name="accountBM">用户实体</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            string SELECT_DataItemPurviewByUserId = "select * from vw_DataItems where cnvcUserID = '" + accountBM.UserId + "'";
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItemPurviewByUserId);
        }
        #endregion

        #region  获取参考点 yanxian 2013-12-26
        /// <summary>
        /// 获取参考点
        /// </summary>
        /// <param name="accountBM">用户实体</param>
        /// <returns></returns>
        public DataTable GetDataItemPointPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            string SELECT_DataItemPurviewByUserId = "select * from tbDataItem where cnvcPrimaryCodeField in ('cncSTA','cncETA','cncTDWN','cncATA','cncSTD','cncETD','cncOpenCabinTime','cncOpenCargoCabinTime')";
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItemPurviewByUserId);
        }
        #endregion

        #region 插入一条记录
        /// <summary>
        /// 插入记录SQL语句
        /// </summary>
        private const string INSERT_Purview = "insert into tbDataItemPurview(cniDataItemNo,cniDataItemPurview," + 
                                                                "cniDataItemVisible,cniViewIndex,cniSplashPromptItem,cniSoundPromptItem,cnvcUserID) values(" +
                                                                "@PARM_cniDataItemNo,@PARM_cniDataItemPurview,@PARM_cniDataItemVisible,@PARM_cniViewIndex," +
                                                                "@PARM_cniSplashPromptItem,@PARM_cniSoundPromptItem,@PARM_cnvcUserID)";
        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Purview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniDataItemNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniDataItemPurview", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniDataItemVisible", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniViewIndex", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniSplashPromptItem", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniSoundPromptItem", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Purview, parms);
            }
            parms[0].Value = dataItemPurviewBM.DataItemNO;
            parms[1].Value = dataItemPurviewBM.DataItemPurview;
            parms[2].Value = dataItemPurviewBM.DataItemVisible;
            parms[3].Value = dataItemPurviewBM.ViewIndex;
            parms[4].Value = dataItemPurviewBM.SplashPromptItem;
            parms[5].Value = dataItemPurviewBM.SoundPromptItem;
            parms[6].Value = dataItemPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public int Insert(FlightMonitorBM.DataItemPurviewBM dataItemPurview)
        {
            SqlParameter[] parms = InsertParameters(dataItemPurview);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, INSERT_Purview, parms);
            return retVal;
        }
        #endregion

        #region 更新用户数据项权限
        /// <summary>
        /// 更新用户权限SQL语句
        /// </summary>
        private const string UPDATE_Purview = "UPDATE tbDataItemPurview SET " +
            "cniDataItemPurview = @PARM_cniDataItemPurview WHERE " +
            "cniDataItemNo=@PARM_cniDataItemNo AND " +
            "cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateParameters(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_Purview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniDataItemPurview", SqlDbType.Int, 0),                    
                    new SqlParameter("@PARM_cniDataItemNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_Purview, parms);
            }
            parms[0].Value = dataItemPurviewBM.DataItemPurview;           
            parms[1].Value = dataItemPurviewBM.DataItemNO;
            parms[2].Value = dataItemPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 更新数据项权限
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePurview(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = UpdateParameters(dataItemPurviewBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_Purview, parms);
            return retVal;
        }       
        #endregion

        #region 更新用户数据项的可见性
        /// <summary>
        /// 更新数据项可见性
        /// </summary>
        private const string UPDATE_Visible = "UPDATE tbDataItemPurview SET " +
                    "cniDataItemVisible = @PARM_cniDataItemVisible,cniSplashPromptItem = @PARM_cniSplashPromptItem WHERE " +
                    "cniDataItemNo=@PARM_cniDataItemNo AND " +
                    "cnvcUserID=@PARM_cnvcUserID";
        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateVisibleParameters(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_Visible);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniDataItemVisible", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniSplashPromptItem", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniDataItemNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_Visible, parms);
            }
            parms[0].Value = dataItemPurviewBM.DataItemVisible;
            parms[1].Value = dataItemPurviewBM.SplashPromptItem;
            parms[2].Value = dataItemPurviewBM.DataItemNO;
            parms[3].Value = dataItemPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 更新数据项的可见性
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateVisible(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = UpdateVisibleParameters(dataItemPurviewBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_Visible, parms);
        }
        #endregion

        #region 更新数据项的显示顺序
        /// <summary>
        /// 更新数据项显示顺序
        /// </summary>
        private const string UPDATE_Index = "UPDATE tbDataItemPurview SET " +
                    "cniViewIndex = @PARM_cniViewIndex WHERE " +
                    "cniDataItemNo=@PARM_cniDataItemNo AND " +
                    "cnvcUserID=@PARM_cnvcUserID";
        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateIndexParameters(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_Index);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniViewIndex", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniDataItemNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_Index, parms);
            }
            parms[0].Value = dataItemPurviewBM.ViewIndex;
            parms[1].Value = dataItemPurviewBM.DataItemNO;
            parms[2].Value = dataItemPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 更新数据项的显示顺序
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateIndex(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = UpdateIndexParameters(dataItemPurviewBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_Index, parms);
        }       

        #endregion

        #region 根据用户名和数据项编号获取权限信息
        /// <summary>
        /// 根据数据项编号和用户编码获取
        /// </summary>
        private const string SELECT_DataItemPurviewByDataItemNo = "SELECT * FROM tbDataItemPurview WHERE " +
            "cniDataItemNo=@PARM_cniDataItemNo AND cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        private SqlParameter[] GetDataItemPurviewByDataItemNoParameters(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_DataItemPurviewByDataItemNo);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniDataItemNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_DataItemPurviewByDataItemNo, parms);
            }
            parms[0].Value = dataItemPurviewBM.DataItemNO;
            parms[1].Value = dataItemPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 查询某用户某数据项的权限
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByDataItemNo(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = GetDataItemPurviewByDataItemNoParameters(dataItemPurviewBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItemPurviewByDataItemNo, parms);
        }
        #endregion

        #region 查询某用户所有有权限但没有在视图中显示出的数据项
        /// <summary>
        /// 查询某用户所有有权限但没有在视图中显示出的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetPurviewDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //string SELECT_PurviewDataItem = "SELECT * FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemPurview > 0 AND cniDataItemVisible = 0 order by cniDataItemNo";

            //增加 数据项组合信息，如 [进港]进港机号，增加备注项
            string SELECT_PurviewDataItem = "SELECT *,'cnvcDataItemName_Combine' = case [cniInOROut] when 1 then ('【进港】 ' " +
            #region modified by LinYong in 20150422
                //" + " + "[cnvcDataItemName] else '[出港] ' " + " + " + "[cnvcDataItemName] end FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemPurview > 0 AND cniDataItemVisible = 0 order by cniDataItemNo";
            " + " + "left(([cnvcDataItemName] + '                                                                                         ' ),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + (case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   【' + cnvcMemo + '】' end ))" +
            " else ('【出港】 ' " + " + " + "left(([cnvcDataItemName] + '                                                              '),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + ( case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   【' + cnvcMemo + '】' end ))" + 
            " end FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemPurview > 0 AND cniDataItemVisible = 0 order by cniInOROut";
            #endregion modified by LinYong in 20150422

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_PurviewDataItem);
        }
        #endregion

        #region 查询某用户设置显示的数据项
        /// <summary>
        /// 查询某用户设置显示的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //string SELECT_VisibleDataItem = "SELECT * FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemVisible = 1 order by cniViewIndex";

            //增加 数据项组合信息，如 [进港]进港机号，增加备注项 -- modified by LinYong in 20150422
            string SELECT_VisibleDataItem = "SELECT *,'cnvcDataItemName_Combine' = case [cniInOROut] when 1 then ('【进港】 ' " +
            " + " + "left(([cnvcDataItemName] + '                                                                                         ' ),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + (case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   【' + cnvcMemo + '】' end ))" +
            " else ('【出港】 ' " + " + " + "left(([cnvcDataItemName] + '                                                              '),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + ( case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   【' + cnvcMemo + '】' end ))" + 
            " end FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemVisible = 1 order by cniViewIndex";

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VisibleDataItem);
        }
        #endregion

        #region 根据类型查询用户设置显示的数据项
        /// <summary>
        /// 查询某用户设置显示的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM, string strType)
        {
            string SELECT_VisibleDataItemByType = "SELECT * FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemVisible = 1 AND cnvcDataItemID LIKE '" + strType + "%' order by cniViewIndex";
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VisibleDataItemByType);
        }
        #endregion

        #region 更新用户提示设置
        /// <summary>
        /// 更新用户提示设置
        /// </summary>
        private const string UPDATE_Prompt = "UPDATE tbDataItemPurview SET " +
            "cniSplashPromptItem = @PARM_cniSplashPromptItem," +
            "cniSoundPromptItem = @PARM_cniSoundPromptItem WHERE " +
            "cniDataItemNo=@PARM_cniDataItemNo AND " +
            "cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// 组合参数
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] UpdatePromptParameters(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_Prompt);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniSplashPromptItem", SqlDbType.Int, 0),  
                    new SqlParameter("@PARM_cniSoundPromptItem", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniDataItemNo", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_Prompt, parms);
            }
            parms[0].Value = dataItemPurviewBM.SplashPromptItem;
            parms[1].Value = dataItemPurviewBM.SoundPromptItem;
            parms[2].Value = dataItemPurviewBM.DataItemNO;
            parms[3].Value = dataItemPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// 更新用户提示设置
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePrompt(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = UpdatePromptParameters(dataItemPurviewBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, UPDATE_Prompt, parms);
            return retVal;
        }
        #endregion
    }
}
