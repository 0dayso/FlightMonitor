using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.AgentServiceDA
{    
    /// <summary>
    /// �����û���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2009-11-27
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class OnLineUsersDAF
    {
        #region ���� �����û���
        /// <summary>
        /// ���� �����û���
        /// </summary>
        /// <returns></returns>
        public DataTable CreateDatatable()
        {
            #region ��������
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null;

            #endregion


            #region ����ʵ��
            //���ɱ��
            try
            {
                dataColumn = new DataColumn("cnvcIPAddress", Type.GetType("System.String"));
                dataColumn.Caption = "IP��ַ";
                dataTable.Columns.Add(dataColumn);
                
                dataColumn = new DataColumn("cnvcUserId", Type.GetType("System.String"));
                dataColumn.Caption = "E���ʺ�";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcUserName", Type.GetType("System.String"));
                dataColumn.Caption = "�û���";
                dataTable.Columns.Add(dataColumn);
 
                dataColumn = new DataColumn("cnvcStationThreeCode", Type.GetType("System.String"));
                dataColumn.Caption = "��վ��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniRefreshInterval", Type.GetType("System.Int32"));
                dataColumn.Caption = "ˢ��Ƶ�ʣ��룩";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndLastOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "������ʱ��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcResult", Type.GetType("System.String"));
                dataColumn.Caption = "״̬";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcMemo", Type.GetType("System.String"));
                dataColumn.Caption = "��ע";
                dataTable.Columns.Add(dataColumn);
                
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["cnvcIPAddress"], dataTable.Columns["cnvcUserId"] };
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //���ؽ��
            return dataTable;

            #endregion
        }
        #endregion

        #region ���� �����û���Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtOnLineUsers">�����û���Ϣ��</param>
        /// <param name="accountBM">�ʺŶ���ʵ��</param>
        /// <returns></returns>
        public int RefreshOnLineUsersInfo(DataTable dtOnLineUsers, AccountBM accountBM)
        {
            #region ��������
            DataRow dataRow = null;
            bool blnFind = false;

            int retVal = -1;
            #endregion

            #region ����ʵ��
            try
            {
                //���¼�¼
                for (int iIndex = 0; iIndex < dtOnLineUsers.Rows.Count; iIndex++)
                {
                    if ((dtOnLineUsers.Rows[iIndex]["cnvcIPAddress"].ToString() == accountBM.IPAddress) &&
                        (dtOnLineUsers.Rows[iIndex]["cnvcUserId"].ToString() == accountBM.UserId))
                    {
                        dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"] = DateTime.Now;
                        dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "����";
                        dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "";
                        dtOnLineUsers.AcceptChanges();

                        blnFind = true;
                    }
                    else
                    {
                        double dInterval = (new TimeSpan(DateTime.Now.Ticks
                                                        - ((DateTime)(dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"])).Ticks
                                                        )).TotalMinutes;    //���ʱ�䣨���ӣ�
                        if ((dInterval > 10) && (dInterval <= 20))
                        {
                            dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "�ж�";
                            dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "������ж���" + dInterval.ToString() + "���ӣ�ԭ�򣺱����������ˢ��Ƶ�ʡ�";
                            dtOnLineUsers.AcceptChanges();
                        }
                        else if ((dInterval > 20) && (dInterval <= 2880))
                        {
                            dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "����";
                            dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "������ж���" + dInterval.ToString() + "���ӣ�ԭ�򣺳��������˳����쳣�˳���ˢ��Ƶ�ʡ�";
                            dtOnLineUsers.AcceptChanges();
                        }
                        else if (dInterval > 2880)
                        {
                            dtOnLineUsers.Rows[iIndex].Delete();
                            dtOnLineUsers.AcceptChanges();
                        }
                    }

                }
                //���û�У������
                if (!blnFind)
                {
                    dataRow = dtOnLineUsers.NewRow();
                    dataRow["cnvcIPAddress"] = accountBM.IPAddress;
                    dataRow["cnvcUserId"] = accountBM.UserId;
                    dataRow["cnvcUserName"] = accountBM.UserName;
                    dataRow["cnvcStationThreeCode"] = accountBM.StationThreeCode;
                    dataRow["cniRefreshInterval"] = accountBM.RefreshInterval;
                    dataRow["cndLastOprationTime"] = DateTime.Now;
                    dataRow["cnvcResult"] = "����";
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
            
            
            //���ؽ��
            return retVal;

            #endregion
        }
        #endregion

        #region ��������
        #region ���� �����û���Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtOnLineUsers">�����û���Ϣ��</param>
        /// <param name="accountBM">�ʺŶ���ʵ��</param>
        /// <param name="SynchronizeLock">ͬ����</param>
        /// <returns></returns>
        public int RefreshOnLineUsersInfo(DataTable dtOnLineUsers, AccountBM accountBM, object SynchronizeLock)
        {
            #region ��������
            DataRow dataRow = null;
            bool blnFind = false;

            int retVal = -1;
            #endregion

            #region ����ʵ��
            try
            {
                lock (SynchronizeLock)
                {
                    //���¼�¼
                    for (int iIndex = 0; iIndex < dtOnLineUsers.Rows.Count; iIndex++)
                    {
                        if ((dtOnLineUsers.Rows[iIndex]["cnvcIPAddress"].ToString() == accountBM.IPAddress) &&
                            (dtOnLineUsers.Rows[iIndex]["cnvcUserId"].ToString() == accountBM.UserId))
                        {
                            dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"] = DateTime.Now;
                            dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "����";
                            dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "";
                            dtOnLineUsers.AcceptChanges();

                            blnFind = true;
                        }
                        else
                        {
                            double dInterval = (new TimeSpan(DateTime.Now.Ticks
                                                            - ((DateTime)(dtOnLineUsers.Rows[iIndex]["cndLastOprationTime"])).Ticks
                                                            )).TotalMinutes;    //���ʱ�䣨���ӣ�
                            if ((dInterval > 10) && (dInterval <= 20))
                            {
                                dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "�ж�";
                                dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "������ж���" + dInterval.ToString() + "���ӣ�ԭ�򣺱����������ˢ��Ƶ�ʡ�";
                                dtOnLineUsers.AcceptChanges();
                            }
                            else if ((dInterval > 20) && (dInterval <= 2880))
                            {
                                dtOnLineUsers.Rows[iIndex]["cnvcResult"] = "����";
                                dtOnLineUsers.Rows[iIndex]["cnvcMemo"] = "������ж���" + dInterval.ToString() + "���ӣ�ԭ�򣺳��������˳����쳣�˳���ˢ��Ƶ�ʡ�";
                                dtOnLineUsers.AcceptChanges();
                            }
                            else if (dInterval > 2880)
                            {
                                dtOnLineUsers.Rows[iIndex].Delete();
                                dtOnLineUsers.AcceptChanges();
                            }
                        }

                    }
                    //���û�У������
                    if (!blnFind)
                    {
                        dataRow = dtOnLineUsers.NewRow();
                        dataRow["cnvcIPAddress"] = accountBM.IPAddress;
                        dataRow["cnvcUserId"] = accountBM.UserId;
                        dataRow["cnvcUserName"] = accountBM.UserName;
                        dataRow["cnvcStationThreeCode"] = accountBM.StationThreeCode;
                        dataRow["cniRefreshInterval"] = accountBM.RefreshInterval;
                        dataRow["cndLastOprationTime"] = DateTime.Now;
                        dataRow["cnvcResult"] = "����";
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


            //���ؽ��
            return retVal;

            #endregion
        }
        #endregion

        #endregion

    }
}
