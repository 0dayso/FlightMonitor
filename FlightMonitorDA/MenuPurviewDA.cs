using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// �˵���Ȩ�޷��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-06-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class MenuPurviewDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MenuPurviewDA()
        {
        }

        #region ��ȡ���в˵���
        /// <summary>
        /// ��ѯ������SQL���
        /// </summary>
        private const string SELECT_MenuItem = "Select * from tbMenuItem order by cniMenuID";

        /// <summary>
        /// ��ȡ���еĲ˵���
        /// </summary>
        /// <returns>�������в˵�������ݱ�</returns>
        public DataTable GetMenuItems()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuItem);
        }
        #endregion

        //#region  ��ȡĳ�û��Ĳ˵���Ȩ��
        ///// <summary>
        ///// SQL���
        ///// </summary>
        //private const string SELECT_MenuItemPurviewByUserId = "select * from tbMenuItem,tbMenuPurview where " +
        //    "tbMenuItem.cniMenuID = tbMenuPurview.cniMenuID and cnvcUserID = @PARM_cnvcUserID";

        ///// <summary>
        ///// ��ϲ���
        ///// </summary>
        ///// <returns></returns>
        //private SqlParameter[] GetMenuItemPurviewByUserIdParameters(FlightMonitorBM.AccountBM accountBM)
        //{
        //    SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_MenuItemPurviewByUserId);
        //    if (parms == null)
        //    {
        //        parms = new SqlParameter[]
        //        {
        //            new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
        //        };

        //        SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_MenuItemPurviewByUserId, parms);
        //    }
        //    parms[0].Value = accountBM.UserId;
        //    return parms;
        //}

        ///// <summary>
        ///// ��ȡ�û��Ĳ˵������Ȩ��
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetMenuItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        //{
        //    SqlParameter[] parms = GetMenuItemPurviewByUserIdParameters(accountBM);
        //    return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuItemPurviewByUserId, parms);
        //}
        //#endregion

        #region ��ȡĳ�û����в˵���Ȩ��
        /// <summary>
        /// ��ȡĳ�û����в˵���Ȩ��
        /// </summary>
        private const string SELECT_MenuItemPurview = "SELECT * FROM tbMenuItem LEFT OUTER JOIN tbMenuPurview ON tbMenuItem.cniMenuID = tbMenuPurview.cniMenuID WHERE " +
            "cnvcUserID = @PARM_cnvcUserID order by tbMenuItem.cniMenuID";
        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="accountBM">�û�ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] GetMenuItemPurviewParameters(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_MenuItemPurview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                    {
                       new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                    };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_MenuItemPurview, parms);
            };

            parms[0].Value = accountBM.UserId;
            return parms;
        }

        /// <summary>
        /// ��ȡ�˵���Ȩ��
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public DataTable GetMenuItemPurview(FlightMonitorBM.AccountBM accountBM)
        {
            SqlParameter[] parms = GetMenuItemPurviewParameters(accountBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuItemPurview, parms);
        }
        #endregion

        #region ����һ����¼
        /// <summary>
        /// �����¼SQL���
        /// </summary>
        private const string INSERT_Purview = "insert into tbMenuPurview(cniMenuID,cniMenuPurview,cnvcUserID) values(" +
           "@PARM_cniMenuID,@PARM_cniMenuPurview,@PARM_cnvcUserID)";
        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] InsertParameters(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, INSERT_Purview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniMenuID", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cniMenuPurview", SqlDbType.Int, 0),                    
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, INSERT_Purview, parms);
            }
            parms[0].Value = menuPurviewBM.MenuID;
            parms[1].Value = menuPurviewBM.MenuPurview;
            parms[2].Value = menuPurviewBM.UserID;           
            return parms;
        }

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public int Insert(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = InsertParameters(menuPurviewBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, this.Transaction, CommandType.Text, INSERT_Purview, parms);
            return retVal;
        }
        #endregion

        #region �����û�������Ȩ��
        /// <summary>
        /// �����û�Ȩ��SQL���
        /// </summary>
        private const string UPDATE_Purview = "UPDATE tbMenuPurview SET " +
            "cniMenuPurview = @PARM_cniMenuPurview WHERE " +
            "cniMenuID=@PARM_cniMenuID AND " +
            "cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateParameters(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, UPDATE_Purview);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniMenuPurview", SqlDbType.Int, 0),                    
                    new SqlParameter("@PARM_cniMenuID", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, UPDATE_Purview, parms);
            }
            parms[0].Value = menuPurviewBM.MenuPurview;
            parms[1].Value = menuPurviewBM.MenuID;
            parms[2].Value = menuPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// ����������Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public int UpdatePurview(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = UpdateParameters(menuPurviewBM);
            int retVal = SqlHelper.ExecuteNonQuery(this.SqlConn, CommandType.Text, UPDATE_Purview, parms);
            return retVal;
        }

        #endregion

        #region �����û����Ͳ˵���Ż�ȡȨ����Ϣ
        /// <summary>
        /// ���ݲ˵���ź��û������ȡ
        /// </summary>
        private const string SELECT_MenuPurviewByMenuID = "SELECT * FROM tbMenuPurview WHERE " +
            "cniMenuID=@PARM_cniMenuID AND cnvcUserID=@PARM_cnvcUserID";

        /// <summary>
        /// ��ϲ���
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns></returns>
        private SqlParameter[] GetMenuPurviewByMenuIDParameters(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(this.DBConnString, SELECT_MenuPurviewByMenuID);
            if (parms == null)
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@PARM_cniMenuID", SqlDbType.Int, 0),
                    new SqlParameter("@PARM_cnvcUserID", SqlDbType.NVarChar, 20)
                };
                SqlHelperParameterCache.CacheParameterSet(this.DBConnString, SELECT_MenuPurviewByMenuID, parms);
            }
            parms[0].Value = menuPurviewBM.MenuID;
            parms[1].Value = menuPurviewBM.UserID;
            return parms;
        }

        /// <summary>
        /// ��ѯĳ�û�ĳ�˵����Ȩ��
        /// </summary>
        /// <param name="dataItemPurviewBM">������Ȩ��ʵ�����</param>
        /// <returns></returns>
        public DataTable GetMenuPurviewByMenuID(FlightMonitorBM.MenuPurviewBM menuPurviewBM)
        {
            SqlParameter[] parms = GetMenuPurviewByMenuIDParameters(menuPurviewBM);
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_MenuPurviewByMenuID, parms);
        }

        #endregion

    }
}
