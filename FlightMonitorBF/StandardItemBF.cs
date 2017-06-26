using System;
using System.Collections;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// ��վ���ϱ�׼����ҵ���
    /// </summary>
    /// author : yanxian
    /// date : 2013-11-13
    public class StandardItemBF
    {
        private StandardItemDAF standardItemDAF = new StandardItemDAF();

        #region ������׼�����б����
        public int AddStandardItem(StandardBM standardBM)
        {
            if (standardBM.FourCode != null && standardBM.FourCode == "")
                return 0;
            else if (standardBM.FlightType != null && standardBM.FlightType == "")
                return 0;
            else if (standardBM.Actype != null && standardBM.Actype == "")
                return 0;
            else if (standardBM.Company != null && standardBM.Company == "")
                return 0;
            else
            {
                //�����ж��Ƿ����
                if (standardItemDAF.CheckStandardItem(standardBM))
                    return 2;
                else
                    return standardItemDAF.AddStandardItem(standardBM);
            }
        }
        #endregion

        #region ɾ����׼���̲���
        public int DelStandardItem(int iStandardItem)
        {
            if (iStandardItem.Equals(null))
                return 0;
            else
            {
                return standardItemDAF.DelStandardItem(iStandardItem);
            }
        }
        #endregion

        #region ���±�׼���̲���
        public int UpdateStandardItem(StandardBM nStandardItem)
        {
            if (nStandardItem.FourCode == null && nStandardItem.FourCode == "")
                return 0;
            else if (nStandardItem.FlightType == null && nStandardItem.FlightType == "")
                return 0;
            else if (nStandardItem.Actype == null && nStandardItem.Actype == "")
                return 0;
            else if (nStandardItem.Company == null && nStandardItem.Company == "")
                return 0;
            else
            {
                return standardItemDAF.UpdateStandardItem(nStandardItem);
            }
        }
        #endregion

        #region ���ص�����׼���̲���(������¼)
        public StandardBM GetStandardItemByID(StandardBM standardBM)
        {
            if (standardBM == null)
                return null;
            else
                return standardItemDAF.GetStandardItemByID(standardBM);
        }
        #endregion

        #region �������
        public int AddBatchStandardItems(string oAirPort, string oAcType, string nAirPort, string nAcType, string nCityName)
        {
            return standardItemDAF.BatchStandardItemOps(oAirPort, oAcType, nAirPort, nAcType, nCityName);
        }
        #endregion

        #region ���ر�׼�����б����
        public DataTable GetStandardItemList(StandardBM conditionStandardBM)
        {
            DataTable standardItemTable = new DataTable();
            standardItemTable = standardItemDAF.GetStandardItemList(conditionStandardBM);

            if (standardItemTable == null)
                return null;

            
            standardItemTable.Columns["cnvcItemName"].Caption = "��������";
            standardItemTable.Columns["cnvcFlightType"].Caption = "��������";
            standardItemTable.Columns["cnvcCompany"].Caption = "���չ�˾";
            standardItemTable.Columns["cnvcFlightIOType"].Caption = "����/�⺽��";
            standardItemTable.Columns["cnvcCNCityName"].Caption = "����";
            standardItemTable.Columns["cnvcActype"].Caption = "����";

            standardItemTable.Columns["cnvcCNBeginFactPoint"].Caption = "ʵ�ʿ�ʼʱ��";
            standardItemTable.Columns["cnvcCNEndFactPoint"].Caption = "ʵ�ʽ���ʱ��";
            standardItemTable.Columns["cnvcCNBeginRefencePoint"].Caption = "��ʼ�ο���";
            standardItemTable.Columns["cnvcCNEndRefencePoint"].Caption = "�����ο���";
            standardItemTable.Columns["cnvcBeginTimeSpace"].Caption = "�࿪ʼʱ���";
            standardItemTable.Columns["cnvcEndTimeSpace"].Caption = "�����ʱ���";


            standardItemTable.Columns["cnvcFourCode"].Caption = "����������";
            standardItemTable.Columns["cniView"].Caption = "�Ƿ���ʾ";
            standardItemTable.Columns["cniStandardNo"].Caption = "����ID";

            return standardItemTable;
        }
        #endregion

        #region
        public DataSet GetStandardItemList()
        {
            return standardItemDAF.GetStandardCNNameList();
        }
        #endregion

        public DataTable GetStandardItemList(string strIOCN)
        {
            return standardItemDAF.GetStandardCNNameIOList(strIOCN).Tables[0];
        }

        public DataTable GetOwnerList()
        {
            return standardItemDAF.GetOwnerList().Tables[0];
        }

        public DataTable GetFlightTypeList()
        {
            return standardItemDAF.GetFlightTypeList().Tables[0];
        }
    }
}
