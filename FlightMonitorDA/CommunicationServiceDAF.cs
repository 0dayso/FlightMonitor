using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.AgentServiceBM;
using AirSoft.Public.SystemFramework;
using CompressDataSet.Common;
using System.IO;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ͨѶ�������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-03-02
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class CommunicationServiceDAF : MarshalByRefObject
    {
        #region Զ�̶���
        static public CommunicationServiceDAF _objRemotingObject = null;
        #endregion Զ�̶���

        #region �ڴ����ݱ�������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ�������� GetDataBySQL ���̡�
        //ͨѶ��Ϣ����ṹ�����ݿ��
        //�������� cniUploadResult ������ʶ��������¼ �� Ĭ��ֵ Ϊ 0����1���ɹ���0��δ��⣻-1��ʧ�ܣ�
        static public DataTable _tbCommunicationRecord = null;
        //�ļ���Ϣ�����������У��ļ����ƣ�server����cnvcFileName_Server���ļ����ݣ�cnbFileContent���ļ�����ʱ�䣺cndOperationTime���ļ���С��cniFileSize
        static public Hashtable _htFileRecord = null;   //��Ҫ�洢�ļ������ݵ�����ָ�룬�ܹ������ļ�����
        static public DataTable _tbFileRecord = null;   //��Ҫ���ڰ����ļ�����ʱ������ļ���Ϣ��ɾ�����ڵ��ļ�

        private static int _CommunicationRecordID;      //��ʶID
        private static int _FileID = 0;                 //�ļ���ʹ�ã�ȷ���ļ���Ψһ
        private static int _FileRecordTotalSize = 0;    //�ļ���Ϣ���� _htFileRecord �����������ļ� �� �ܵĴ�С
        private static Boolean _blnRecalculateFileRecordInfo = false;   //���¼���ͳ�� �ļ���Ϣ���� �� �����Ϣ�����ܵĴ�С

        #region ͬ����
        private static object _objCommunicationRecord__Lock = new object();  //_tbCommunicationRecord ��ͬ����
        private static object _objFileRecord__Lock = new object();           //_htFileRecord ��ͬ����

        private static object _objFileID_Lock = new object();                //_FileID ��ͬ����
        #endregion ͬ����

        #endregion �ڴ����ݱ�������ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ�������� GetDataBySQL ���̡�

        #region ������æ��־
        static public bool _blnBusy_tbCommunicationRecord = false;
        static public bool _blnBusy_htFileRecord = false;

        #endregion ������æ��־


        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public CommunicationServiceDAF()
        {
            //���� _tbCommunicationRecord
            if (_tbCommunicationRecord == null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("cniCommunicationRecordID", typeof(int));
                dataTable.Columns.Add("cnvcType", typeof(string));
                dataTable.Columns.Add("cnvcMessage", typeof(string));
                dataTable.Columns.Add("cnvcFileName_Client", typeof(string));
                dataTable.Columns.Add("cnvcFileName_Server", typeof(string));
                dataTable.Columns.Add("cnvcReceiverList", typeof(string));
                dataTable.Columns.Add("cnvcSenderAccount", typeof(string));
                dataTable.Columns.Add("cnvcSenderName", typeof(string));
                dataTable.Columns.Add("cniUploadResult", typeof(int));
                dataTable.Columns.Add("cndOperationTime", typeof(DateTime));
                _tbCommunicationRecord = dataTable;
            }

            //���� _htFileRecord 
            if (_htFileRecord == null)
            {
                _htFileRecord = new Hashtable();
            }

            //���� _tbFileRecord
            if (_tbFileRecord == null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("cnvcFileName_Server", typeof(string));
                dataTable.Columns.Add("cniFileSize", typeof(int));
                dataTable.Columns.Add("cndOperationTime", typeof(DateTime));    //�ļ�����ʱ��
                _tbFileRecord = dataTable;
            }
        }
        #endregion ���캯��

        #region ����Ĺ���

        #region �û�������Ϣ
        /// <summary>
        /// �û�������Ϣ
        /// </summary>
        /// <param name="message">��Ϣ�����־�����Ϣ��λ�þ�����Ϣ</param>
        /// <param name="receiverList">������������Ϣ����ϣ���ʾΪ��;(HAK);linyong;�������������루��ʾ���ڷ��ʴ˻���������Ϣ���û�������ʾΪ(HAK);�����û����û���½�ʺţ�����ʾΪlinyong</param>
        /// <param name="senderAccount">�������ʺ�</param>
        /// <param name="senderName">����������</param>
        /// <returns></returns>
        public DataTable SendMessage(string message, string receiverList, string senderAccount, string senderName)
        {
            #region ��������
            DataTable dataTableResult = new DataTable();

            #endregion ��������


            #region ����ʵ��
            //��������:cnvcCaption,������ ��� �� ��ע����� cnvcContent �� �ɹ� �� ʧ�ܣ����ʧ�ܣ��� ��ע �� �� cnvcContent ��������ԭ��
            dataTableResult.Columns.Add("cnvcCaption", typeof(string));
            dataTableResult.Columns.Add("cnvcContent", typeof(string));

            #region ��ӵ� _tbCommunicationRecord
            try
            {
                lock (_objCommunicationRecord__Lock)
                {
                    _CommunicationRecordID = _CommunicationRecordID + 1;

                    DataRow dataRowtbCommunicationRecord_New = _tbCommunicationRecord.NewRow();
                    dataRowtbCommunicationRecord_New["cniCommunicationRecordID"] = _CommunicationRecordID;
                    dataRowtbCommunicationRecord_New["cnvcType"] = "����";
                    dataRowtbCommunicationRecord_New["cnvcMessage"] = message;
                    dataRowtbCommunicationRecord_New["cnvcFileName_Client"] = "";
                    dataRowtbCommunicationRecord_New["cnvcFileName_Server"] = "";
                    dataRowtbCommunicationRecord_New["cnvcReceiverList"] = receiverList;
                    dataRowtbCommunicationRecord_New["cnvcSenderAccount"] = senderAccount;
                    dataRowtbCommunicationRecord_New["cnvcSenderName"] = senderName;
                    dataRowtbCommunicationRecord_New["cniUploadResult"] = 0;
                    dataRowtbCommunicationRecord_New["cndOperationTime"] = DateTime.Now;
                    _tbCommunicationRecord.Rows.Add(dataRowtbCommunicationRecord_New);
                }

                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //���ؽ������
                dataRowdataTableResult_New["cnvcCaption"] = "���";
                dataRowdataTableResult_New["cnvcContent"] = "�ɹ�";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }
            catch (Exception ex)
            {
                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //���ؽ������
                dataRowdataTableResult_New["cnvcCaption"] = "���";
                dataRowdataTableResult_New["cnvcContent"] = "ʧ��";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);

                dataRowdataTableResult_New = dataTableResult.NewRow();  //���ؽ������
                dataRowdataTableResult_New["cnvcCaption"] = "��ע";
                dataRowdataTableResult_New["cnvcContent"] = ex.Message;
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }
            #endregion ��ӵ� _tbCommunicationRecord


            //���ؽ��
            return dataTableResult;

            #endregion ����ʵ��
        }
        #endregion �û�������Ϣ


        #region �û������ļ�
        /// <summary>
        /// �û������ļ�
        /// </summary>
        /// <param name="message">��Ϣ�����־�����Ϣ��λ�þ�����Ϣ</param>
        /// <param name="fileContent">�ļ����ݣ�byte[]���ͣ��ļ��Ķ���������Ϣ</param>
        /// <param name="fileName_Client">�ļ����ƣ�client�����ͻ��˴��͹������ļ����ƣ���������ǡ�λ�á����У�</param>
        /// <param name="receiverList">������������Ϣ����ϣ���ʾΪ��;(HAK);linyong;�������������루��ʾ���ڷ��ʴ˻���������Ϣ���û�������ʾΪ(HAK);�����û����û���½�ʺţ�����ʾΪlinyong</param>
        /// <param name="senderAccount">�������ʺ�</param>
        /// <param name="senderName">����������</param>
        /// <returns></returns>
        public DataTable SendFile(string message, byte[] fileContent, string fileName_Client, string receiverList, string senderAccount, string senderName)
        {
            #region ��������
            DataTable dataTableResult = new DataTable();
            string fileName_Server;
            #endregion ��������


            #region ����ʵ��
            //��������:cnvcCaption,������ ��� �� ��ע����� cnvcContent �� �ɹ� �� ʧ�ܣ����ʧ�ܣ��� ��ע �� �� cnvcContent ��������ԭ��
            dataTableResult.Columns.Add("cnvcCaption", typeof(string));
            dataTableResult.Columns.Add("cnvcContent", typeof(string));

            //
            try
            {
                #region ȷ���ļ����ƣ�������
                lock (_objFileID_Lock)   //ȷ���ļ����ƣ�ȷ��Ψһ
                {
                    _FileID = _FileID + 1;
                    fileName_Server = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _FileID.ToString() + Path.GetExtension(fileName_Client);
                }
                FileStream stream = new FileStream(SysMsgBM.FilePath + @"\" + fileName_Server, FileMode.Create, FileAccess.Write, FileShare.Read | FileShare.Write);
                stream.SetLength(fileContent.LongLength);
                stream.Write(fileContent, 0, fileContent.Length);   //���������ļ�д��ָ��Ŀ¼
                stream.Close();
                #endregion ȷ���ļ����ƣ�������

                #region ��ӵ� �ļ���Ϣ���� _htFileRecord��_tbFileRecord
                lock (_objFileRecord__Lock)
                {
                    #region ���� �ļ���Ϣ���� _htFileRecord �����������ļ� �� �ܵĴ�С ������ 300M��314572800�ֽڣ� -- �ݲ�ʹ�ô˷��������ڱ�������ļ������
                    /*
                    while ((_FileRecordTotalSize + fileContent.Length) > 314572800)
                    {
                        DictionaryEntry dictionaryEntry_OperationTime_Min = null;   //��������ļ���Ϣ������ļ�

                        foreach (DictionaryEntry dictionaryEntry in _htFileRecord)  //������������ļ���Ϣ������ļ� 
                        {
                            if (dictionaryEntry_OperationTime_Min = null)
                            {
                                dictionaryEntry_OperationTime_Min = dictionaryEntry;
                            }
                            else
                            {
                                if ((dictionaryEntry.Value as FileRecordBM).OperationTime < (dictionaryEntry_OperationTime_Min.Value as FileRecordBM).OperationTime)
                                {
                                    dictionaryEntry_OperationTime_Min = dictionaryEntry;
                                }
                            }
                        }

                        _htFileRecord.Remove((dictionaryEntry_OperationTime_Min.Key as string));    //ɾ����������ļ���Ϣ������ļ�

                        _FileRecordTotalSize = _FileRecordTotalSize - (dictionaryEntry_OperationTime_Min.Value as FileRecordBM).FileSize;   //���������ļ��Ĵ�С֮�͵�ֵ
                    }
                    */
                    #endregion ���� �ļ���Ϣ���� _htFileRecord �����������ļ� �� �ܵĴ�С ������ 300M��314572800�ֽڣ� -- �ݲ�ʹ�ô˷��������ڱ�������ļ������

                    #region ���� �ļ���Ϣ���� _htFileRecord �����������ļ� �� �ܵĴ�С ������ 300M��314572800�ֽڣ�
                    if ((_FileRecordTotalSize + fileContent.Length) > 314572800)
                    {
                        DataRow[] dataRows_tbFileRecord = _tbFileRecord.Select("", "cndOperationTime asc"); //�ļ����絽������

                        int indexdataRows_tbFileRecord = 0;
                        while ((_FileRecordTotalSize + fileContent.Length) > 314572800)
                        {
                            _htFileRecord.Remove(dataRows_tbFileRecord[indexdataRows_tbFileRecord]["cnvcFileName_Server"].ToString());  //ɾ����������ļ���Ϣ������ļ� 

                            dataRows_tbFileRecord[indexdataRows_tbFileRecord].Delete(); //ɾ����������ļ���Ϣ������ļ��ļ�¼

                            _FileRecordTotalSize =
                                _FileRecordTotalSize -
                                Convert.ToInt32(dataRows_tbFileRecord[indexdataRows_tbFileRecord]["cniFileSize"].ToString());   //���������ļ��Ĵ�С֮�͵�ֵ

                            indexdataRows_tbFileRecord++;

                            if ((_FileRecordTotalSize < 0) || (indexdataRows_tbFileRecord >= dataRows_tbFileRecord.Length))
                            {
                                _blnRecalculateFileRecordInfo = true;   //��Ҫ ���¼���ͳ�� �ļ���Ϣ���� �� �����Ϣ���������ļ��Ĵ�С֮�͵�ֵ
                                break;
                            }
                        }
                        _tbFileRecord.AcceptChanges();  //Ӧ�ñ�����ͷ�ɾ���ļ�¼ռ�õ���Դ
                    }
                    #endregion ���� �ļ���Ϣ���� _htFileRecord �����������ļ� �� �ܵĴ�С ������ 300M��314572800�ֽڣ�

                    DateTime dOperationTime = DateTime.Now; //����ʱ��

                    #region �� �ļ���Ϣ���� _htFileRecord �� ������ļ�
                    FileRecordBM fileRecordBM = new FileRecordBM();
                    fileRecordBM.FileName_Server = fileName_Server;
                    fileRecordBM.FileContent = fileContent;
                    fileRecordBM.FileSize = fileContent.Length;
                    fileRecordBM.OperationTime = dOperationTime;
                    _htFileRecord.Add(fileName_Server, fileRecordBM);
                    #endregion �� �ļ���Ϣ���� _htFileRecord �� ������ļ�

                    #region �� �ļ���Ϣ���� _tbFileRecord �� ������ļ���¼
                    DataRow dataRow_tbFileRecord_New = _tbFileRecord.NewRow();
                    dataRow_tbFileRecord_New["cnvcFileName_Server"] = fileName_Server;
                    dataRow_tbFileRecord_New["cniFileSize"] = fileContent.Length;
                    dataRow_tbFileRecord_New["cndOperationTime"] = dOperationTime;
                    _tbFileRecord.Rows.Add(dataRow_tbFileRecord_New);
                    #endregion �� �ļ���Ϣ���� _tbFileRecord �� ������ļ���¼

                    _FileRecordTotalSize = _FileRecordTotalSize + fileContent.Length;   //���������ļ��Ĵ�С֮�͵�ֵ
                }
                #endregion ��ӵ� �ļ���Ϣ���� _htFileRecord��_tbFileRecord

                #region ��ӵ� ͨѶ��Ϣ�ڴ���� _tbCommunicationRecord
                lock (_objCommunicationRecord__Lock)
                {
                    _CommunicationRecordID = _CommunicationRecordID + 1;

                    DataRow dataRowtbCommunicationRecord_New = _tbCommunicationRecord.NewRow();
                    dataRowtbCommunicationRecord_New["cniCommunicationRecordID"] = _CommunicationRecordID;
                    dataRowtbCommunicationRecord_New["cnvcType"] = "λ��";
                    dataRowtbCommunicationRecord_New["cnvcMessage"] = message;
                    dataRowtbCommunicationRecord_New["cnvcFileName_Client"] = fileName_Client;
                    dataRowtbCommunicationRecord_New["cnvcFileName_Server"] = fileName_Server;
                    dataRowtbCommunicationRecord_New["cnvcReceiverList"] = receiverList;
                    dataRowtbCommunicationRecord_New["cnvcSenderAccount"] = senderAccount;
                    dataRowtbCommunicationRecord_New["cnvcSenderName"] = senderName;
                    dataRowtbCommunicationRecord_New["cniUploadResult"] = 0;
                    dataRowtbCommunicationRecord_New["cndOperationTime"] = DateTime.Now;
                    _tbCommunicationRecord.Rows.Add(dataRowtbCommunicationRecord_New);
                }
                #endregion ��ӵ� ͨѶ��Ϣ�ڴ���� _tbCommunicationRecord

                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //���ؽ������
                dataRowdataTableResult_New["cnvcCaption"] = "���";
                dataRowdataTableResult_New["cnvcContent"] = "�ɹ�";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }
            catch (Exception ex)
            {
                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //���ؽ������
                dataRowdataTableResult_New["cnvcCaption"] = "���";
                dataRowdataTableResult_New["cnvcContent"] = "ʧ��";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);

                dataRowdataTableResult_New = dataTableResult.NewRow();  //���ؽ������
                dataRowdataTableResult_New["cnvcCaption"] = "��ע";
                dataRowdataTableResult_New["cnvcContent"] = ex.Message;
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }


            //���ؽ��
            return dataTableResult;
            #endregion ����ʵ��
        }

        #endregion �û������ļ�
























        #endregion ����Ĺ���









        #region ���ݲ�ѯ�����ڴ����ȡ����[��� strTableName ��ʾ�����ݱ��ڶ��߳������޸Ĳ�������Ҫ����ͬ����]
        /// <summary>
        /// ���ݲ�ѯ�����ڴ����ȡ����
        /// </summary>
        /// <param name="strTableName">�ڴ����</param>
        /// <param name="strSQL">��ѯ���</param>
        /// <param name="strSort">�������</param>
        /// <param name="strFilterField">��Ҫ��ȡ���ֶΣ���",column1,column2,column3,"</param>
        /// <returns>��ѯ�Ľ�������󷵻� null</returns>
        public DataTable GetDataBySQL(string strTableName, string strSQL, string strSort, string strFilterField)
        {
            #region ��������
            DataTable dataTable = null;
            DataRow[] dataRows = null;

            #endregion


            #region ����ʵ��
            try
            {
                //_tbCommunicationRecord
                if (strTableName == "_tbCommunicationRecord")
                {
                    dataTable = _tbCommunicationRecord.Clone();
                    dataRows = _tbCommunicationRecord.Select(strSQL, strSort);
                }

                //�����ݵ��� dataTable
                foreach (DataRow dataRow in dataRows)
                {
                    dataTable.ImportRow(dataRow);
                }

                //�� dataTable �в���Ҫ���ֶ�ɾ��
                if (strFilterField != "")
                {
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        if (strFilterField.IndexOf(("," + dataColumn.ColumnName + ","), StringComparison.InvariantCultureIgnoreCase) < 0)
                            dataTable.Columns.Remove(dataColumn);
                    }
                }

            }
            catch
            {
                dataTable = null;
            }

            //���ؽ��
            return dataTable;

            #endregion
        }
        #endregion


    }

}
