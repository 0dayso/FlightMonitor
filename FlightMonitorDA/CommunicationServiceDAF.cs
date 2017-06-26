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
    /// 通讯代理服务
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-03-02
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class CommunicationServiceDAF : MarshalByRefObject
    {
        #region 远程对象
        static public CommunicationServiceDAF _objRemotingObject = null;
        #endregion 远程对象

        #region 内存数据表，如果数据表在多线程中有修改操作，需要增加同步锁【如 GetDataBySQL 过程】
        //通讯信息，表结构见数据库表，
        //另增加列 cniUploadResult （入库标识（新增记录 的 默认值 为 0）：1，成功；0，未入库；-1，失败）
        static public DataTable _tbCommunicationRecord = null;
        //文件信息，包含数据列：文件名称（server）：cnvcFileName_Server、文件内容：cnbFileContent、文件操作时间：cndOperationTime、文件大小：cniFileSize
        static public Hashtable _htFileRecord = null;   //主要存储文件的内容的引用指针，能够访问文件内容
        static public DataTable _tbFileRecord = null;   //主要便于按照文件操作时间过滤文件信息，删除早期的文件

        private static int _CommunicationRecordID;      //标识ID
        private static int _FileID = 0;                 //文件名使用，确保文件名唯一
        private static int _FileRecordTotalSize = 0;    //文件信息对象 _htFileRecord 包含的所有文件 的 总的大小
        private static Boolean _blnRecalculateFileRecordInfo = false;   //重新计算统计 文件信息对象 的 相关信息，如总的大小

        #region 同步锁
        private static object _objCommunicationRecord__Lock = new object();  //_tbCommunicationRecord 的同步锁
        private static object _objFileRecord__Lock = new object();           //_htFileRecord 的同步锁

        private static object _objFileID_Lock = new object();                //_FileID 的同步锁
        #endregion 同步锁

        #endregion 内存数据表，如果数据表在多线程中有修改操作，需要增加同步锁【如 GetDataBySQL 过程】

        #region 操作繁忙标志
        static public bool _blnBusy_tbCommunicationRecord = false;
        static public bool _blnBusy_htFileRecord = false;

        #endregion 操作繁忙标志


        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunicationServiceDAF()
        {
            //构造 _tbCommunicationRecord
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

            //构造 _htFileRecord 
            if (_htFileRecord == null)
            {
                _htFileRecord = new Hashtable();
            }

            //构造 _tbFileRecord
            if (_tbFileRecord == null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("cnvcFileName_Server", typeof(string));
                dataTable.Columns.Add("cniFileSize", typeof(int));
                dataTable.Columns.Add("cndOperationTime", typeof(DateTime));    //文件操作时间
                _tbFileRecord = dataTable;
            }
        }
        #endregion 构造函数

        #region 代理的过程

        #region 用户发送信息
        /// <summary>
        /// 用户发送信息
        /// </summary>
        /// <param name="message">信息：文字具体信息或位置具体信息</param>
        /// <param name="receiverList">内容是以下信息的组合（表示为：;(HAK);linyong;）：机场三字码（表示正在访问此机场航班信息的用户）：表示为(HAK);具体用户（用户登陆帐号）：表示为linyong</param>
        /// <param name="senderAccount">发送者帐号</param>
        /// <param name="senderName">发送者姓名</param>
        /// <returns></returns>
        public DataTable SendMessage(string message, string receiverList, string senderAccount, string senderName)
        {
            #region 变量声明
            DataTable dataTableResult = new DataTable();

            #endregion 变量声明


            #region 编码实现
            //构造结果表:cnvcCaption,内容有 结果 和 备注，结果 cnvcContent 是 成功 或 失败，如果失败，在 备注 行 的 cnvcContent 描述具体原因
            dataTableResult.Columns.Add("cnvcCaption", typeof(string));
            dataTableResult.Columns.Add("cnvcContent", typeof(string));

            #region 添加到 _tbCommunicationRecord
            try
            {
                lock (_objCommunicationRecord__Lock)
                {
                    _CommunicationRecordID = _CommunicationRecordID + 1;

                    DataRow dataRowtbCommunicationRecord_New = _tbCommunicationRecord.NewRow();
                    dataRowtbCommunicationRecord_New["cniCommunicationRecordID"] = _CommunicationRecordID;
                    dataRowtbCommunicationRecord_New["cnvcType"] = "文字";
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

                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //返回结果设置
                dataRowdataTableResult_New["cnvcCaption"] = "结果";
                dataRowdataTableResult_New["cnvcContent"] = "成功";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }
            catch (Exception ex)
            {
                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //返回结果设置
                dataRowdataTableResult_New["cnvcCaption"] = "结果";
                dataRowdataTableResult_New["cnvcContent"] = "失败";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);

                dataRowdataTableResult_New = dataTableResult.NewRow();  //返回结果设置
                dataRowdataTableResult_New["cnvcCaption"] = "备注";
                dataRowdataTableResult_New["cnvcContent"] = ex.Message;
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }
            #endregion 添加到 _tbCommunicationRecord


            //返回结果
            return dataTableResult;

            #endregion 编码实现
        }
        #endregion 用户发送信息


        #region 用户发送文件
        /// <summary>
        /// 用户发送文件
        /// </summary>
        /// <param name="message">信息：文字具体信息或位置具体信息</param>
        /// <param name="fileContent">文件内容：byte[]类型，文件的二进制流信息</param>
        /// <param name="fileName_Client">文件名称（client）：客户端传送过来的文件名称（针对类型是“位置”的行）</param>
        /// <param name="receiverList">内容是以下信息的组合（表示为：;(HAK);linyong;）：机场三字码（表示正在访问此机场航班信息的用户）：表示为(HAK);具体用户（用户登陆帐号）：表示为linyong</param>
        /// <param name="senderAccount">发送者帐号</param>
        /// <param name="senderName">发送者姓名</param>
        /// <returns></returns>
        public DataTable SendFile(string message, byte[] fileContent, string fileName_Client, string receiverList, string senderAccount, string senderName)
        {
            #region 变量声明
            DataTable dataTableResult = new DataTable();
            string fileName_Server;
            #endregion 变量声明


            #region 编码实现
            //构造结果表:cnvcCaption,内容有 结果 和 备注，结果 cnvcContent 是 成功 或 失败，如果失败，在 备注 行 的 cnvcContent 描述具体原因
            dataTableResult.Columns.Add("cnvcCaption", typeof(string));
            dataTableResult.Columns.Add("cnvcContent", typeof(string));

            //
            try
            {
                #region 确定文件名称，并保存
                lock (_objFileID_Lock)   //确定文件名称，确保唯一
                {
                    _FileID = _FileID + 1;
                    fileName_Server = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _FileID.ToString() + Path.GetExtension(fileName_Client);
                }
                FileStream stream = new FileStream(SysMsgBM.FilePath + @"\" + fileName_Server, FileMode.Create, FileAccess.Write, FileShare.Read | FileShare.Write);
                stream.SetLength(fileContent.LongLength);
                stream.Write(fileContent, 0, fileContent.Length);   //将二进制文件写到指定目录
                stream.Close();
                #endregion 确定文件名称，并保存

                #region 添加到 文件信息对象 _htFileRecord、_tbFileRecord
                lock (_objFileRecord__Lock)
                {
                    #region 控制 文件信息对象 _htFileRecord 包含的所有文件 的 总的大小 不大于 300M（314572800字节） -- 暂不使用此方法，存在遍历过多的极端情况
                    /*
                    while ((_FileRecordTotalSize + fileContent.Length) > 314572800)
                    {
                        DictionaryEntry dictionaryEntry_OperationTime_Min = null;   //最早进入文件信息对象的文件

                        foreach (DictionaryEntry dictionaryEntry in _htFileRecord)  //查找最早进入文件信息对象的文件 
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

                        _htFileRecord.Remove((dictionaryEntry_OperationTime_Min.Key as string));    //删除最早进入文件信息对象的文件

                        _FileRecordTotalSize = _FileRecordTotalSize - (dictionaryEntry_OperationTime_Min.Value as FileRecordBM).FileSize;   //调整所有文件的大小之和的值
                    }
                    */
                    #endregion 控制 文件信息对象 _htFileRecord 包含的所有文件 的 总的大小 不大于 300M（314572800字节） -- 暂不使用此方法，存在遍历过多的极端情况

                    #region 控制 文件信息对象 _htFileRecord 包含的所有文件 的 总的大小 不大于 300M（314572800字节）
                    if ((_FileRecordTotalSize + fileContent.Length) > 314572800)
                    {
                        DataRow[] dataRows_tbFileRecord = _tbFileRecord.Select("", "cndOperationTime asc"); //文件从早到晚排序

                        int indexdataRows_tbFileRecord = 0;
                        while ((_FileRecordTotalSize + fileContent.Length) > 314572800)
                        {
                            _htFileRecord.Remove(dataRows_tbFileRecord[indexdataRows_tbFileRecord]["cnvcFileName_Server"].ToString());  //删除最早进入文件信息对象的文件 

                            dataRows_tbFileRecord[indexdataRows_tbFileRecord].Delete(); //删除最早进入文件信息对象的文件的记录

                            _FileRecordTotalSize =
                                _FileRecordTotalSize -
                                Convert.ToInt32(dataRows_tbFileRecord[indexdataRows_tbFileRecord]["cniFileSize"].ToString());   //调整所有文件的大小之和的值

                            indexdataRows_tbFileRecord++;

                            if ((_FileRecordTotalSize < 0) || (indexdataRows_tbFileRecord >= dataRows_tbFileRecord.Length))
                            {
                                _blnRecalculateFileRecordInfo = true;   //需要 重新计算统计 文件信息对象 的 相关信息，如所有文件的大小之和的值
                                break;
                            }
                        }
                        _tbFileRecord.AcceptChanges();  //应用变更，释放删除的记录占用的资源
                    }
                    #endregion 控制 文件信息对象 _htFileRecord 包含的所有文件 的 总的大小 不大于 300M（314572800字节）

                    DateTime dOperationTime = DateTime.Now; //操作时间

                    #region 在 文件信息对象 _htFileRecord 中 添加新文件
                    FileRecordBM fileRecordBM = new FileRecordBM();
                    fileRecordBM.FileName_Server = fileName_Server;
                    fileRecordBM.FileContent = fileContent;
                    fileRecordBM.FileSize = fileContent.Length;
                    fileRecordBM.OperationTime = dOperationTime;
                    _htFileRecord.Add(fileName_Server, fileRecordBM);
                    #endregion 在 文件信息对象 _htFileRecord 中 添加新文件

                    #region 在 文件信息对象 _tbFileRecord 中 添加新文件记录
                    DataRow dataRow_tbFileRecord_New = _tbFileRecord.NewRow();
                    dataRow_tbFileRecord_New["cnvcFileName_Server"] = fileName_Server;
                    dataRow_tbFileRecord_New["cniFileSize"] = fileContent.Length;
                    dataRow_tbFileRecord_New["cndOperationTime"] = dOperationTime;
                    _tbFileRecord.Rows.Add(dataRow_tbFileRecord_New);
                    #endregion 在 文件信息对象 _tbFileRecord 中 添加新文件记录

                    _FileRecordTotalSize = _FileRecordTotalSize + fileContent.Length;   //调整所有文件的大小之和的值
                }
                #endregion 添加到 文件信息对象 _htFileRecord、_tbFileRecord

                #region 添加到 通讯信息内存对象 _tbCommunicationRecord
                lock (_objCommunicationRecord__Lock)
                {
                    _CommunicationRecordID = _CommunicationRecordID + 1;

                    DataRow dataRowtbCommunicationRecord_New = _tbCommunicationRecord.NewRow();
                    dataRowtbCommunicationRecord_New["cniCommunicationRecordID"] = _CommunicationRecordID;
                    dataRowtbCommunicationRecord_New["cnvcType"] = "位置";
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
                #endregion 添加到 通讯信息内存对象 _tbCommunicationRecord

                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //返回结果设置
                dataRowdataTableResult_New["cnvcCaption"] = "结果";
                dataRowdataTableResult_New["cnvcContent"] = "成功";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }
            catch (Exception ex)
            {
                DataRow dataRowdataTableResult_New = dataTableResult.NewRow();  //返回结果设置
                dataRowdataTableResult_New["cnvcCaption"] = "结果";
                dataRowdataTableResult_New["cnvcContent"] = "失败";
                dataTableResult.Rows.Add(dataRowdataTableResult_New);

                dataRowdataTableResult_New = dataTableResult.NewRow();  //返回结果设置
                dataRowdataTableResult_New["cnvcCaption"] = "备注";
                dataRowdataTableResult_New["cnvcContent"] = ex.Message;
                dataTableResult.Rows.Add(dataRowdataTableResult_New);
            }


            //返回结果
            return dataTableResult;
            #endregion 编码实现
        }

        #endregion 用户发送文件
























        #endregion 代理的过程









        #region 根据查询语句从内存表提取数据[如果 strTableName 表示的数据表在多线程中有修改操作，需要增加同步锁]
        /// <summary>
        /// 根据查询语句从内存表提取数据
        /// </summary>
        /// <param name="strTableName">内存表名</param>
        /// <param name="strSQL">查询语句</param>
        /// <param name="strSort">排序语句</param>
        /// <param name="strFilterField">需要提取的字段，如",column1,column2,column3,"</param>
        /// <returns>查询的结果：错误返回 null</returns>
        public DataTable GetDataBySQL(string strTableName, string strSQL, string strSort, string strFilterField)
        {
            #region 变量声明
            DataTable dataTable = null;
            DataRow[] dataRows = null;

            #endregion


            #region 编码实现
            try
            {
                //_tbCommunicationRecord
                if (strTableName == "_tbCommunicationRecord")
                {
                    dataTable = _tbCommunicationRecord.Clone();
                    dataRows = _tbCommunicationRecord.Select(strSQL, strSort);
                }

                //把数据导入 dataTable
                foreach (DataRow dataRow in dataRows)
                {
                    dataTable.ImportRow(dataRow);
                }

                //把 dataTable 中不需要的字段删除
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

            //返回结果
            return dataTable;

            #endregion
        }
        #endregion


    }

}
