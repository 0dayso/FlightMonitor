using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.DataHelper;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ���ݷ��ʲ���
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-31
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class IntermissionTimeDA:SqlDatabase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public IntermissionTimeDA()
        {
        }

        #region ��ȡ���л��͵ı�׼��վʱ��
        private const string SELECT_StandardIntermissionTime = "SELECT * FROM tbIntermissionTime";

        /// <summary>
        /// ��ȡ���л��͵ı�׼��վʱ��
        /// </summary>
        /// <returns></returns>
        public DataTable GetStandardIntermissionTime()
        {
            return SqlHelper.ExecuteDataTable(this.SqlConn, CommandType.Text, SELECT_StandardIntermissionTime);
        }
        #endregion


    }
}
