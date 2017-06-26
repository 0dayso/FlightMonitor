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
    /// ������Ȩ�����ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-23
    /// �� �� �ˣ���  ��
    /// �޸����ڣ�2008-07-01
    /// ��    ����
    public class DataItemPurviewDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DataItemPurviewDA()
        {
        }

        #region ��ȡ����������
        /// <summary>
        /// ��ѯ������SQL���
        /// </summary>
        private const string SELECT_DataItem = "Select * from tbDataItem";       

        /// <summary>
        /// ��ȡ���е�������
        /// </summary>
        /// <returns>������������������ݱ�</returns>
        public DataTable GetDataItems()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItem);
        }
        #endregion

        #region  ��ȡĳ�û�������������Ȩ��
        /// <summary>
        /// ��ȡ�û����������������Ȩ��
        /// </summary>
        /// <param name="accountBM">�û�ʵ��</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            string SELECT_DataItemPurviewByUserId = "select * from vw_DataItems where cnvcUserID = '" + accountBM.UserId + "'";
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItemPurviewByUserId);
        }
        #endregion

        #region  ��ȡ�ο��� yanxian 2013-12-26
        /// <summary>
        /// ��ȡ�ο���
        /// </summary>
        /// <param name="accountBM">�û�ʵ��</param>
        /// <returns></returns>
        public DataTable GetDataItemPointPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            string SELECT_DataItemPurviewByUserId = "select * from tbDataItem where cnvcPrimaryCodeField in ('cncSTA','cncETA','cncTDWN','cncATA','cncSTD','cncETD','cncOpenCabinTime','cncOpenCargoCabinTime')";
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItemPurviewByUserId);
        }
        #endregion

        #region ����һ����¼
        /// <summary>
        /// �����¼SQL���
        /// </summary>
        private const string INSERT_Purview = "insert into tbDataItemPurview(cniDataItemNo,cniDataItemPurview," + 
                                                                "cniDataItemVisible,cniViewIndex,cniSplashPromptItem,cniSoundPromptItem,cnvcUserID) values(" +
                                                                "@PARM_cniDataItemNo,@PARM_cniDataItemPurview,@PARM_cniDataItemVisible,@PARM_cniViewIndex," +
                                                                "@PARM_cniSplashPromptItem,@PARM_cniSoundPromptItem,@PARM_cnvcUserID)";
        /// <summary>
        /// ��ϲ���
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
        /// ����һ����¼
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

        #region �����û�������Ȩ��
        /// <summary>
        /// �����û�Ȩ��SQL���
        /// </summary>
        private const string UPDATE_Purview = "UPDATE tbDataItemPurview SET " +
            "cniDataItemPurview = @PARM_cniDataItemPurview WHERE " +
            "cniDataItemNo=@PARM_cniDataItemNo AND " +
            "cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// ��ϲ���
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
        /// ����������Ȩ��
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

        #region �����û�������Ŀɼ���
        /// <summary>
        /// ����������ɼ���
        /// </summary>
        private const string UPDATE_Visible = "UPDATE tbDataItemPurview SET " +
                    "cniDataItemVisible = @PARM_cniDataItemVisible,cniSplashPromptItem = @PARM_cniSplashPromptItem WHERE " +
                    "cniDataItemNo=@PARM_cniDataItemNo AND " +
                    "cnvcUserID=@PARM_cnvcUserID";
        /// <summary>
        /// ��ϲ���
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
        /// ����������Ŀɼ���
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateVisible(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = UpdateVisibleParameters(dataItemPurviewBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_Visible, parms);
        }
        #endregion

        #region �������������ʾ˳��
        /// <summary>
        /// ������������ʾ˳��
        /// </summary>
        private const string UPDATE_Index = "UPDATE tbDataItemPurview SET " +
                    "cniViewIndex = @PARM_cniViewIndex WHERE " +
                    "cniDataItemNo=@PARM_cniDataItemNo AND " +
                    "cnvcUserID=@PARM_cnvcUserID";
        /// <summary>
        /// ��ϲ���
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
        /// �������������ʾ˳��
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdateIndex(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = UpdateIndexParameters(dataItemPurviewBM);
            return SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_Index, parms);
        }       

        #endregion

        #region �����û������������Ż�ȡȨ����Ϣ
        /// <summary>
        /// �����������ź��û������ȡ
        /// </summary>
        private const string SELECT_DataItemPurviewByDataItemNo = "SELECT * FROM tbDataItemPurview WHERE " +
            "cniDataItemNo=@PARM_cniDataItemNo AND cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
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
        /// ��ѯĳ�û�ĳ�������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns></returns>
        public DataTable GetDataItemPurviewByDataItemNo(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            SqlParameter[] parms = GetDataItemPurviewByDataItemNoParameters(dataItemPurviewBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_DataItemPurviewByDataItemNo, parms);
        }
        #endregion

        #region ��ѯĳ�û�������Ȩ�޵�û������ͼ����ʾ����������
        /// <summary>
        /// ��ѯĳ�û�������Ȩ�޵�û������ͼ����ʾ����������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetPurviewDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //string SELECT_PurviewDataItem = "SELECT * FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemPurview > 0 AND cniDataItemVisible = 0 order by cniDataItemNo";

            //���� �����������Ϣ���� [����]���ۻ��ţ����ӱ�ע��
            string SELECT_PurviewDataItem = "SELECT *,'cnvcDataItemName_Combine' = case [cniInOROut] when 1 then ('�����ۡ� ' " +
            #region modified by LinYong in 20150422
                //" + " + "[cnvcDataItemName] else '[����] ' " + " + " + "[cnvcDataItemName] end FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemPurview > 0 AND cniDataItemVisible = 0 order by cniDataItemNo";
            " + " + "left(([cnvcDataItemName] + '                                                                                         ' ),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + (case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   ��' + cnvcMemo + '��' end ))" +
            " else ('�����ۡ� ' " + " + " + "left(([cnvcDataItemName] + '                                                              '),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + ( case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   ��' + cnvcMemo + '��' end ))" + 
            " end FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemPurview > 0 AND cniDataItemVisible = 0 order by cniInOROut";
            #endregion modified by LinYong in 20150422

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_PurviewDataItem);
        }
        #endregion

        #region ��ѯĳ�û�������ʾ��������
        /// <summary>
        /// ��ѯĳ�û�������ʾ��������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM)
        {
            //string SELECT_VisibleDataItem = "SELECT * FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemVisible = 1 order by cniViewIndex";

            //���� �����������Ϣ���� [����]���ۻ��ţ����ӱ�ע�� -- modified by LinYong in 20150422
            string SELECT_VisibleDataItem = "SELECT *,'cnvcDataItemName_Combine' = case [cniInOROut] when 1 then ('�����ۡ� ' " +
            " + " + "left(([cnvcDataItemName] + '                                                                                         ' ),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + (case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   ��' + cnvcMemo + '��' end ))" +
            " else ('�����ۡ� ' " + " + " + "left(([cnvcDataItemName] + '                                                              '),(30 - (datalength([cnvcDataItemName]) - len([cnvcDataItemName]))))" + " + ( case when cnvcMemo = '' then '' when cnvcMemo is null then '' else '   ��' + cnvcMemo + '��' end ))" + 
            " end FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemVisible = 1 order by cniViewIndex";

            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VisibleDataItem);
        }
        #endregion

        #region �������Ͳ�ѯ�û�������ʾ��������
        /// <summary>
        /// ��ѯĳ�û�������ʾ��������
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM, string strType)
        {
            string SELECT_VisibleDataItemByType = "SELECT * FROM vw_DataItems WHERE cnvcUserID = '" + accountBM.UserId + "' AND cniDataItemVisible = 1 AND cnvcDataItemID LIKE '" + strType + "%' order by cniViewIndex";
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_VisibleDataItemByType);
        }
        #endregion

        #region �����û���ʾ����
        /// <summary>
        /// �����û���ʾ����
        /// </summary>
        private const string UPDATE_Prompt = "UPDATE tbDataItemPurview SET " +
            "cniSplashPromptItem = @PARM_cniSplashPromptItem," +
            "cniSoundPromptItem = @PARM_cniSoundPromptItem WHERE " +
            "cniDataItemNo=@PARM_cniDataItemNo AND " +
            "cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// ��ϲ���
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
        /// �����û���ʾ����
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
