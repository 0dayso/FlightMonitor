using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 文件（图片、文档、视频等）信息内存对象存储的记录的实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：林勇
    /// 创建日期：2015-03-04
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class FileRecordBM
    {
        #region 声明
        private string _cnvcfilename_server;
        private byte[] _cnbfilecontent;
        private int _cnifilesize;
        private DateTime _cndoperationtime;
        #endregion 声明


        #region 属性
        /// <summary>
        /// 文件名称（server）：服务器上保存文件时使用的名称（具有唯一性）
        /// </summary>
        public string FileName_Server
        {
            set { _cnvcfilename_server = value; }
            get { return _cnvcfilename_server; }
        }

        /// <summary>
        /// 文件内容：byte[]类型，文件的二进制流信息
        /// </summary>
        public byte[] FileContent
        {
            set { _cnbfilecontent = value; }
            get { return _cnbfilecontent; }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize
        {
            set { _cnifilesize = value; }
            get { return _cnifilesize; }
        }

        /// <summary>
        /// 创建时间：此对象（行）创建的时间
        /// </summary>
        public DateTime OperationTime
        {
            set { _cndoperationtime = value; }
            get { return _cndoperationtime; }
        }
        #endregion 属性
    }
}
