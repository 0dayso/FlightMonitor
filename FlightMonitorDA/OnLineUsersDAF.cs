using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.AgentServiceDA
{    
    /// <summary>
    /// 在线用户表
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2009-11-27
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class OnLineUsersDAF
    {
        #region 生成 在线用户表
        /// <summary>
        /// 生成 在线用户表
        /// </summary>
        /// <returns></returns>
        public DataTable CreateDatatable()
        {
            #region 变量声明
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null;

            #endregion


            #region 编码实现
            //生成表格
            try
            {
                dataColumn = new DataColumn("cnvcIPAddress", Type.GetType("System.String"));
                dataColumn.Caption = "IP地址";
                dataTable.Columns.Add(dataColumn);
                
                dataColumn = new DataColumn("cnvcUserId", Type.GetType("System.String"));
                dataColumn.Caption = "E网帐号";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcUserName", Type.GetType("System.String"));
                dataColumn.Caption = "用户名";
                dataTable.Columns.Add(dataColumn);
 
                dataColumn = new DataColumn("cnvcStationThreeCode", Type.GetType("System.String"));
                dataColumn.Caption = "航站名";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniRefreshInterval", Type.GetType("System.Int32"));
                dataColumn.Caption = "刷新频率（秒）";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndLastOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "最后操作时间";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcResult", Type.GetType("System.String"));
                dataColumn.Caption = "状态";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
                dataColumn.Caption = "备注";
                dataTable.Columns.Add(dataColumn);
                
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["cnvcIPAddress"], dataTable.Columns["cnvcUserId"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //返回结果
            return dataTable;

            #endregion
        }
        #endregion

        #region 更新 在线用户信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtOnLineUsers">在线用户信息表</param>
        /// <param name="accountBM">帐号对象实体</param>
        /// <returns></returns>
        public int RefreshOnLineUsersInfo(DataTable dtOnLineUsers, AccountBM accountBM)
        {
            #region 变量声明
            DataRow dataRow = null;
            bool blnFind = false;

            int retVal = -1;
            #endregion

            #region 编码实现
            try
            {
                //更新记录
                for (int iIndex = 0; iIndex < dtOnLineUsers.Rows.Count; iIndex++)
                {
                    if ((dtOnLineUsers.Rows[iIndex]["cnvcIPAddress"].ToString() == accountBM.IPAddress) &&
                        (dtOnLineUsers.Rows[iIndex]["cnvcUserId"].ToString() == accountBM.UserId))
                    {
                        dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"] = DateTime.Now;
                        dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "在线";
                        dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "";
                        dtOnLineUsers.AcceptChanges();

                        blnFind = true;
                    }
                    else
                    {
                        double dInterval = (new TimeSpan(DateTime.Now.Ticks
                                                        - ((DateTime)(dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"])).Ticks
                                                        )).TotalMinutes;    //间隔时间（分钟）
                        if ((dInterval > 10) && (dInterval <= 20))
                        {
                            dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "中断";
                            dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "间隔：中断了" + dInterval.ToString() + "分钟；原因：本机、网络或刷新频率。";
                            dtOnLineUsers.AcceptChanges();
                        }
                        else if ((dInterval > 20) && (dInterval <= 2880))
                        {
                            dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "离线";
                            dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "间隔：中断了" + dInterval.ToString() + "分钟；原因：程序正常退出、异常退出或刷新频率。";
                            dtOnLineUsers.AcceptChanges();
                        }
                        else if (dInterval > 2880)
                        {
                            dtOnLineUsers.Rows[iIndex].Delete();
                            dtOnLineUsers.AcceptChanges();
                        }
                    }

                }
                //如果没有，则添加
                if (!blnFind)
                {
                    dataRow = dtOnLineUsers.NewRow();
                    dataRow["cnvcIPAddress"] = accountBM.IPAddress;
                    dataRow["cnvcUserId"] = accountBM.UserId;
                    dataRow["cnvcUserName"] = accountBM.UserName;
                    dataRow["cnvcStationThreeCode"] = accountBM.StationThreeCode;
                    dataRow["cniRefreshInterval"] = accountBM.RefreshInterval;
                    dataRow["cndLastOprationTime"] = DateTime.Now;
                    dataRow["cnvcResult"] = "在线";
                    dataRow["cnvcMemo"] = "";
                    dtOnLineUsers.Rows.Add(dataRow);
                    dtOnLineUsers.AcceptChanges();
                }

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }          
            
            
            //返回结果
            return retVal;

            #endregion
        }
        #endregion

        #region 加锁操作
        #region 更新 在线用户信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtOnLineUsers">在线用户信息表</param>
        /// <param name="accountBM">帐号对象实体</param>
        /// <param name="SynchronizeLock">同步锁</param>
        /// <returns></returns>
        public int RefreshOnLineUsersInfo(DataTable dtOnLineUsers, AccountBM accountBM, object SynchronizeLock)
        {
            #region 变量声明
            DataRow dataRow = null;
            bool blnFind = false;

            int retVal = -1;
            #endregion

            #region 编码实现
            try
            {
                lock (SynchronizeLock)
                {
                    //更新记录
                    for (int iIndex = 0; iIndex < dtOnLineUsers.Rows.Count; iIndex++)
                    {
                        if ((dtOnLineUsers.Rows[iIndex]["cnvcIPAddress"].ToString() == accountBM.IPAddress) &&
                            (dtOnLineUsers.Rows[iIndex]["cnvcUserId"].ToString() == accountBM.UserId))
                        {
                            dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"] = DateTime.Now;
                            dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "在线";
                            dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "";
                            dtOnLineUsers.AcceptChanges();

                            blnFind = true;
                        }
                        else
                        {
                            double dInterval = (new TimeSpan(DateTime.Now.Ticks
                                                            - ((DateTime)(dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"])).Ticks
                                                            )).TotalMinutes;    //间隔时间（分钟）
                            if ((dInterval > 10) && (dInterval <= 20))
                            {
                                dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "中断";
                                dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "间隔：中断了" + dInterval.ToString() + "分钟；原因：本机、网络或刷新频率。";
                                dtOnLineUsers.AcceptChanges();
                            }
                            else if ((dInterval > 20) && (dInterval <= 2880))
                            {
                                dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "离线";
                                dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "间隔：中断了" + dInterval.ToString() + "分钟；原因：程序正常退出、异常退出或刷新频率。";
                                dtOnLineUsers.AcceptChanges();
                            }
                            else if (dInterval > 2880)
                            {
                                dtOnLineUsers.Rows[iIndex].Delete();
                                dtOnLineUsers.AcceptChanges();
                            }
                        }

                    }
                    //如果没有，则添加
                    if (!blnFind)
                    {
                        dataRow = dtOnLineUsers.NewRow();
                        dataRow["cnvcIPAddress"] = accountBM.IPAddress;
                        dataRow["cnvcUserId"] = accountBM.UserId;
                        dataRow["cnvcUserName"] = accountBM.UserName;
                        dataRow["cnvcStationThreeCode"] = accountBM.StationThreeCode;
                        dataRow["cniRefreshInterval"] = accountBM.RefreshInterval;
                        dataRow["cndLastOprationTime"] = DateTime.Now;
                        dataRow["cnvcResult"] = "在线";
                        dataRow["cnvcMemo"] = "";
                        dtOnLineUsers.Rows.Add(dataRow);
                        dtOnLineUsers.AcceptChanges();
                    }
                }

                retVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //返回结果
            return retVal;

            #endregion
        }
        #endregion

        #endregion

    }
}
