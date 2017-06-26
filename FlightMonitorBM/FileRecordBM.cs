using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// �ļ���ͼƬ���ĵ�����Ƶ�ȣ���Ϣ�ڴ����洢�ļ�¼��ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2015-03-04
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class FileRecordBM
    {
        #region ����
        private string _cnvcfilename_server;
        private byte[] _cnbfilecontent;
        private int _cnifilesize;
        private DateTime _cndoperationtime;
        #endregion ����


        #region ����
        /// <summary>
        /// �ļ����ƣ�server�����������ϱ����ļ�ʱʹ�õ����ƣ�����Ψһ�ԣ�
        /// </summary>
        public string FileName_Server
        {
            set { _cnvcfilename_server = value; }
            get { return _cnvcfilename_server; }
        }

        /// <summary>
        /// �ļ����ݣ�byte[]���ͣ��ļ��Ķ���������Ϣ
        /// </summary>
        public byte[] FileContent
        {
            set { _cnbfilecontent = value; }
            get { return _cnbfilecontent; }
        }

        /// <summary>
        /// �ļ���С
        /// </summary>
        public int FileSize
        {
            set { _cnifilesize = value; }
            get { return _cnifilesize; }
        }

        /// <summary>
        /// ����ʱ�䣺�˶����У�������ʱ��
        /// </summary>
        public DateTime OperationTime
        {
            set { _cndoperationtime = value; }
            get { return _cndoperationtime; }
        }
        #endregion ����
    }
}
