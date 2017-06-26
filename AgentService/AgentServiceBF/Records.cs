using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.AgentService
{
    class Records
    {
        #region ���ɼ�¼��1
        /// <summary>
        /// ���ɼ�¼��
        /// </summary>
        /// <returns>���أ�ReturnValueSF.Dt ��¼��ReturnValueSF.Result 1 �ɹ���-1 ʧ�ܣ� </returns>
        public ReturnValueSF CreateDatatable_1()
        {
            #region ��������
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null ;

            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //���ɱ��
            try
            {
                dataColumn = new DataColumn("cniID", Type.GetType("System.Int32"));
                dataColumn.AutoIncrement = true;
                dataColumn.AutoIncrementSeed = 1;
                dataColumn.AutoIncrementStep = 1;
                dataColumn.Caption = "���";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcProcName", Type.GetType("System.String"));
                dataColumn.Caption = "������";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "����ʱ��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcOprationResult", Type.GetType("System.String"));
                dataColumn.Caption = "�������";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniOprationCount", Type.GetType("System.Int32"));
                dataColumn.Caption = "���ô���";
                dataTable.Columns.Add(dataColumn);

                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Dt = null;
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }

        #endregion

        #region ���һ����¼(��1)
        /// <summary>
        /// ���һ����¼
        /// </summary>
        /// <param name="dtRecords">��¼��</param>
        /// <param name="strProcName">������</param>
        /// <param name="dOprationTime">����ʱ��</param>
        /// <param name="strOprationResult">�������</param>
        /// <param name="iOprationCount">���ô���</param>
        /// <returns>ReturnValueSF.Result 1 �ɹ���-1 ʧ�ܣ�</returns>
        public ReturnValueSF AddRecord_1(DataTable dtRecords, string strProcName, DateTime dOprationTime, string strOprationResult, int iOprationCount)
        {
            #region ��������
            DataRow dataRow = null;
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��Ӽ�¼
            try
            {
                dataRow = dtRecords.NewRow();
                dataRow["cnvcProcName"] = strProcName;
                dataRow["cndOprationTime"] = dOprationTime;
                dataRow["cnvcOprationResult"] = strOprationResult;
                dataRow["cniOprationCount"] = iOprationCount;
                dtRecords.Rows.Add(dataRow);

                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }

        #endregion


        #region ���ɼ�¼��2
        /// <summary>
        /// ���ɼ�¼��
        /// </summary>
        /// <returns>���أ�ReturnValueSF.Dt ��¼��ReturnValueSF.Result 1 �ɹ���-1 ʧ�ܣ� </returns>
        public ReturnValueSF CreateDatatable_2()
        {
            #region ��������
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = null;

            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //���ɱ��
            try
            {
                dataColumn = new DataColumn("cniID", Type.GetType("System.Int32"));
                dataColumn.AutoIncrement = true;
                dataColumn.AutoIncrementSeed = 1;
                dataColumn.AutoIncrementStep = 1;
                dataColumn.Caption = "���";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcProcName", Type.GetType("System.String"));
                dataColumn.Caption = "������";
                dataColumn.Unique = true;
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndLastOprationTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "������ʱ��";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cnvcLastOprationResult", Type.GetType("System.String"));
                dataColumn.Caption = "���������";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cniOprationCount", Type.GetType("System.Int32"));
                dataColumn.Caption = "���ô���";
                dataTable.Columns.Add(dataColumn);

                dataColumn = new DataColumn("cndCountStartTime", Type.GetType("System.DateTime"));
                dataColumn.Caption = "������ʼʱ��";
                dataTable.Columns.Add(dataColumn);

                rvSF.Dt = dataTable;
                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Dt = null;
                rvSF.Result = -1;

            }


            //���ؽ��
            return rvSF;

            #endregion
        }

        #endregion

        #region ���һ����¼(��2)
        /// <summary>
        /// ���һ����¼
        /// </summary>
        /// <param name="dtRecords">��¼��2</param>
        /// <param name="strProcName">������</param>
        /// <param name="dLastOprationTime">������ʱ��</param>
        /// <param name="strLastOprationResult">���������</param>
        /// <param name="iOprationCount">���ô���</param>
        /// <param name="dCountStartTime">������ʼʱ��</param>
        /// <returns>ReturnValueSF.Result 1 �ɹ���-1 ʧ�ܣ�</returns>
        public ReturnValueSF AddRecord_2(DataTable dtRecords, string strProcName, DateTime dLastOprationTime, string strLastOprationResult, int iOprationCount, DateTime dCountStartTime)
        {
            #region ��������
            DataRow dataRow = null;
            ReturnValueSF rvSF = new ReturnValueSF();

            #endregion


            #region ����ʵ��
            //��Ӽ�¼
            try
            {
                dataRow = dtRecords.NewRow();
                dataRow["cnvcProcName"] = strProcName;
                dataRow["cndLastOprationTime"] = dLastOprationTime;
                dataRow["cnvcLastOprationResul"] = strLastOprationResult;
                dataRow["cniOprationCount"] = iOprationCount;
                dataRow["cndCountStartTime"] = dCountStartTime;
                dtRecords.Rows.Add(dataRow);

                rvSF.Result = 1;
            }
            catch
            {
                rvSF.Result = -1;
            }


            //���ؽ��
            return rvSF;

            #endregion
        }

        #endregion


    }

}
