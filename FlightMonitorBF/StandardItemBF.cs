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
    /// 航站保障标准流程业务层
    /// </summary>
    /// author : yanxian
    /// date : 2013-11-13
    public class StandardItemBF
    {
        private StandardItemDAF standardItemDAF = new StandardItemDAF();

        #region 新增标准流程列表操作
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
                //事先判断是否存在
                if (standardItemDAF.CheckStandardItem(standardBM))
                    return 2;
                else
                    return standardItemDAF.AddStandardItem(standardBM);
            }
        }
        #endregion

        #region 删除标准流程操作
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

        #region 更新标准流程操作
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

        #region 返回单个标准流程操作(单条记录)
        public StandardBM GetStandardItemByID(StandardBM standardBM)
        {
            if (standardBM == null)
                return null;
            else
                return standardItemDAF.GetStandardItemByID(standardBM);
        }
        #endregion

        #region 批量添加
        public int AddBatchStandardItems(string oAirPort, string oAcType, string nAirPort, string nAcType, string nCityName)
        {
            return standardItemDAF.BatchStandardItemOps(oAirPort, oAcType, nAirPort, nAcType, nCityName);
        }
        #endregion

        #region 返回标准流程列表操作
        public DataTable GetStandardItemList(StandardBM conditionStandardBM)
        {
            DataTable standardItemTable = new DataTable();
            standardItemTable = standardItemDAF.GetStandardItemList(conditionStandardBM);

            if (standardItemTable == null)
                return null;

            
            standardItemTable.Columns["cnvcItemName"].Caption = "流程名称";
            standardItemTable.Columns["cnvcFlightType"].Caption = "航班类型";
            standardItemTable.Columns["cnvcCompany"].Caption = "航空公司";
            standardItemTable.Columns["cnvcFlightIOType"].Caption = "国内/外航班";
            standardItemTable.Columns["cnvcCNCityName"].Caption = "机场";
            standardItemTable.Columns["cnvcActype"].Caption = "机型";

            standardItemTable.Columns["cnvcCNBeginFactPoint"].Caption = "实际开始时间";
            standardItemTable.Columns["cnvcCNEndFactPoint"].Caption = "实际结束时间";
            standardItemTable.Columns["cnvcCNBeginRefencePoint"].Caption = "开始参考点";
            standardItemTable.Columns["cnvcCNEndRefencePoint"].Caption = "结束参考点";
            standardItemTable.Columns["cnvcBeginTimeSpace"].Caption = "距开始时间点";
            standardItemTable.Columns["cnvcEndTimeSpace"].Caption = "距结束时间点";


            standardItemTable.Columns["cnvcFourCode"].Caption = "机场四字码";
            standardItemTable.Columns["cniView"].Caption = "是否显示";
            standardItemTable.Columns["cniStandardNo"].Caption = "流程ID";

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
