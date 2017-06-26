using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ������Ա����ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ�����
    /// �������ڣ�2016-03-18
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    [Serializable]
    public class CommanderTypeBM
    {
        #region ����
        private int _cnicommandertypeid;
		private string _cnvccommandertype;
		private string _cnvcmemo;

        private string _cnvcresult;
        private bool _cnblnsuccess = false;
        #endregion ����

        #region ����
        /// <summary>
		/// 
		/// </summary>
		public int cniCommanderTypeID
		{
			set{ _cnicommandertypeid=value;}
			get{return _cnicommandertypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcCommanderType
		{
			set{ _cnvccommandertype=value;}
			get{return _cnvccommandertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cnvcMemo
		{
			set{ _cnvcmemo=value;}
			get{return _cnvcmemo;}
        }

        /// <summary>
        /// ��ֵ�ɹ���־
        /// </summary>
        public bool Success
        {
            set { _cnblnsuccess = value; }
            get { return _cnblnsuccess; }
        }
        #endregion ����

        #region ����

        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public CommanderTypeBM()
        {
        }
        #endregion ���캯��

        #region ���캯������һ�� tbCommanderType DataRow���ݸ���ʵ�����
        /// <summary>
        /// ��һ�� tbCommanderType DataRow���ݸ���ʵ�����
        /// </summary>
        /// <param name="dataRow">������Ա������Ϣ������</param>
        public CommanderTypeBM(DataRow dataRow)
        {
            try
            {
                _cnicommandertypeid = Convert.ToInt32(dataRow["cniCommanderTypeID"].ToString());
                _cnvccommandertype = dataRow["cnvcCommanderType"].ToString();
                _cnvcmemo = dataRow["cnvcMemo"].ToString();

                _cnblnsuccess = true;
            }
            catch (Exception ex)
            {

                _cnvcresult = ex.Message;
                _cnblnsuccess = false;
            }
        }
        #endregion ���캯������һ�� tbCommanderType DataRow���ݸ���ʵ�����

        #endregion ����

    }
}
