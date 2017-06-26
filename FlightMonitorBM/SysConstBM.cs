using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ϵͳ���ó�����Ϣ��
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-23
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class SysConstBM
    {
        /// <summary>
		/// �汾��
		/// </summary>
		public const string VERSION = "1.0.*";

		/// <summary>
		/// ��Ȩ��˾
		/// </summary>
		public const string COMPANY = "�������չɷ����޹�˾��Ȩ����";
		
		/// <summary>
		/// ������ϵͳ���ݿ�
		/// </summary>
		public const string DB_FlightMonitor	= "dbFlightMonitor";

        /// <summary>
        /// ������ϵͳ�������ݿ�
        /// </summary>
        public const string DB_TEST_FlightMonitor = "db_test_FlightMonitor"; 

        /// <summary>
        /// ��������мƻ����ݿ�
        /// </summary>
        public const string DB_DSPBusiness = "dbDSPBusiness";

        /// <summary>
        /// �洢ACARS���ĵ����ݿ�
        /// </summary>
        public const string DB_ACARSMegs = "dbACARSMegs";

        /// <summary>
        /// ��ʱ�洢FE���ĵ����ݿ�
        /// </summary>
        public const string DB_FEMegs = "dbFEMegs";

        /// <summary>
        /// FleetWatchϵͳ���ݿ⣨Oracle��
        /// </summary>
        public const string DB_FleetWatch = "dbFleetWatch";

        /// <summary>
        /// ���������ݿ�
        /// </summary>
        public const string DB_Flt_Dept = "flt_dept";

        public const string DB_Smart = "smart";

        /// <summary>
        /// ��ǰ������
        /// </summary>
        public const string DB_VoyageReport = "dbVoyageReport";

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public const string DB_MessageService = "dbMessageService";

        /// <summary>
        /// ��½�û�������
        /// </summary>
        public const string FlightMonitor_ACCOUNTENTERIN_NODEFINE = "��½�û�������";

        /// <summary>
        /// ��½�û��������
        /// </summary>
        public const string FlightMonitor_ACCOUNTENTERIN_PWDERROR = "��½�û��������";

        /// <summary>
        /// ��½�û�����
        /// </summary>
        public const string FlightMonitor_MAX_USER = "�Ѿ��ﵽ����½�û�����";

        /// <summary>
        /// ע���ʺ��ظ�
        /// </summary>
        public const string FlightMonitor_ACCOUNT_INVALID = "���û��Ѿ����ڣ���������д";

        /// <summary>
        /// ע���ʺŲ�����
        /// </summary>
        public const string DEMO_ACCOUNT_VALID = "���ʺŲ����ڣ��Ͽ�ע���";

        /// <summary>
        /// ��ӳɹ�
        /// </summary>
        public const string SYS_ADD_SUCCESS = "��ӳɹ���";

        /// <summary>
        /// ���ʧ��
        /// </summary>
        public const string SYS_ADD_FALSE = "���ʧ�ܣ�";

        /// <summary>
        /// ɾ���ɹ�
        /// </summary>
        public const string SYS_DELETE_SUCCESS = "ɾ���ɹ���";

        /// <summary>
        /// ɾ��ʧ��
        /// </summary>
        public const string SYS_DELETE_FALSE = "ɾ��ʧ�ܣ�";

        /// <summary>
        /// �޸ĳɹ�
        /// </summary>
        public const string SYS_UPDATE_SUCCESS = "�޸ĳɹ���";

        /// <summary>
        /// �޸�ʧ��
        /// </summary>
        public const string SYS_UPDATE_FALSE = "�޸�ʧ�ܣ�";

        /// <summary>
        /// ���سɹ�
        /// </summary>
        public const string SYS_LOAD_SUCCESS = "���سɹ���";

        /// <summary>
        /// ����ʧ��
        /// </summary>
        public const string SYS_LOAD_FALSE = "����ʧ�ܣ�";
    }
}
