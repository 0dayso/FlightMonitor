using System;
using System.Collections;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class StandardItemViewBF
    {
        private StandardItemViewDAF standardItemViewDAF = new StandardItemViewDAF();

        /// <summary>
        /// 时间格式转换
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public string HoursMinutesFormate(string strDateTime)
        {
            if (strDateTime.Equals(""))
            {
                return strDateTime;
            }
            else if (strDateTime.Length != 4)
            {
                strDateTime = DateTime.Parse(strDateTime).ToString("HH:mm:");
            }
            else
            {
                strDateTime = strDateTime.Substring(0, 2) + ":" + strDateTime.Substring(2, 2) + ":";
            }
            return strDateTime;
        }

        public IODataSet GetIODataSet(FlightParams iFlightParams, FlightParams oFlightParams)
        {
            DataSet istandardItemDataSet = new DataSet();
            DataSet igranteeDataSet = new DataSet();
            
            /*************进港航班*************/
            if (iFlightParams.FlightNum != null)
            {
                iFlightParams.IOFlight = '0';
                istandardItemDataSet = standardItemViewDAF.GetStandardItem(iFlightParams.LongReg, iFlightParams.ArrStn, iFlightParams.FlightType, iFlightParams.OIFlightType, iFlightParams.IOFlight);
                igranteeDataSet = standardItemViewDAF.GetGuaranteeInfor(iFlightParams);
            }

            /*************进港航班*************/

            /*************出港航班*************/
            DataSet ostandardItemDataSet = new DataSet();
            DataSet ogranteeDataSet = new DataSet();
            if (oFlightParams.FlightNum != null)
            {
                oFlightParams.IOFlight = '1';
                ostandardItemDataSet = standardItemViewDAF.GetStandardItem(oFlightParams.LongReg, oFlightParams.DepStn, oFlightParams.FlightType, oFlightParams.OIFlightType, oFlightParams.IOFlight);
                ogranteeDataSet = standardItemViewDAF.GetGuaranteeInfor(oFlightParams);
            }
            /*************出港航班*************/

            IODataSet ioDataSet = new IODataSet();
            ioDataSet.IDataSet = StandardSetData(istandardItemDataSet, igranteeDataSet, ogranteeDataSet, iFlightParams);
            ioDataSet.ODataSet = StandardSetData(ostandardItemDataSet, igranteeDataSet, ogranteeDataSet, oFlightParams);

            return ioDataSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="standardItemDataSet">标准数据项列表</param>
        /// <param name="iGanteeInfo">进港航班相应的保障数据</param>
        /// <param name="oGanteeInfo">出港航班相应的保障数据</param>
        /// <param name="flightParams">航班参数</param>
        /// <returns></returns>
        private DataSet StandardSetData(DataSet standardItemDataSet, DataSet iGanteeInfo, DataSet oGanteeInfo, FlightParams flightParams)
        {
            DataSet dataSet = standardItemViewDAF.GetBStandardItemName();
            if (standardItemDataSet != null && standardItemDataSet.Tables.Count != 0)
            {
                foreach (DataRow itemRow in standardItemDataSet.Tables[0].Rows)
                {
                    if (itemRow["parentId"].ToString() == "0")
                    {
                        //树的根
                        if (flightParams.IOFlight == '0')
                        {
                            itemRow["cnvcItemName"] = "|--进港航班" + flightParams.FlightNum;
                        }
                        else
                        {
                            itemRow["cnvcItemName"] = "|--出港航班" + flightParams.FlightNum;
                        }
                        itemRow["PercentDone"] = "100";
                        itemRow["priority"] = "1";
                        itemRow["cnvcParentId"] = "";
                        itemRow["cncIsLeaf"] = '0';
                        itemRow["Responsible"] = "";
                    }
                    else if (itemRow["parentId"].ToString() != "0" && itemRow["standardNo"].ToString() == "10000")
                    {
                        //第二级
                        //itemRow["parentId"] = parentId;
                        itemRow["cnvcItemName"] = "|----" + dataSet.Tables[0].Compute("min(cnvcBItemCNName)", " cniBItemNumber ='" + itemRow["parentId"].ToString() + "'");
                        itemRow["PercentDone"] = "100";
                        itemRow["priority"] = "1";
                        itemRow["cnvcParentId"] = "0";
                        itemRow["cncIsLeaf"] = '0';
                        itemRow["Responsible"] = "";
                    }
                    else
                    {
                        //第三级
                        string strBeginFactPoint = GetColumnName(itemRow, iGanteeInfo, oGanteeInfo, "cnvcBeginFactPoint");
                        //granteeDataSet.Tables[0].Rows[0][GetColumnName(itemRow["cnvcBeginFactPoint"].ToString())].ToString();
                        string strEndFactPoint = GetColumnName(itemRow, iGanteeInfo, oGanteeInfo, "cnvcEndFactPoint");
                        //granteeDataSet.Tables[0].Rows[0][GetColumnName(itemRow["cnvcEndFactPoint"].ToString())].ToString();
                        string strBeginRefencePoint = GetColumnName(itemRow, iGanteeInfo, oGanteeInfo, "cnvcBeginRefencePoint");
                        //granteeDataSet.Tables[0].Rows[0][GetColumnName(itemRow["cnvcBeginRefencePoint"].ToString())].ToString();
                        string strEndRefencePoint = GetColumnName(itemRow, iGanteeInfo, oGanteeInfo, "cnvcEndRefencePoint");
                        //granteeDataSet.Tables[0].Rows[0][GetColumnName(itemRow["cnvcEndRefencePoint"].ToString())].ToString();
                        if (!HoursMinutesFormate(strBeginRefencePoint).Equals(""))
                        {
                            itemRow["StartTime"] = DateTime.Parse(flightParams.Datop + " " + HoursMinutesFormate(strBeginRefencePoint)
                            + "00").AddMinutes(double.Parse(itemRow["cnvcBeginTimeSpace"].ToString())).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if (!HoursMinutesFormate(strEndRefencePoint).Equals(""))
                        {
                            itemRow["EndTime"] = DateTime.Parse(flightParams.Datop + " " + HoursMinutesFormate(strEndRefencePoint)
                            + "00").AddMinutes(double.Parse(itemRow["cnvcEndTimeSpace"].ToString())).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if (!HoursMinutesFormate(strBeginFactPoint).Equals(""))
                        {
                            itemRow["BaseLineStartTime"] = DateTime.Parse(flightParams.Datop + " " + HoursMinutesFormate(strBeginFactPoint)
                            + "00").ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if (!HoursMinutesFormate(strEndFactPoint).Equals(""))
                        {
                            itemRow["BaseLineEndTime"] = DateTime.Parse(flightParams.Datop + " " + HoursMinutesFormate(strEndFactPoint)
                            + "00").ToString("yyyy-MM-dd HH:mm:ss");
                        }


                        itemRow["cnvcItemName"] = "|------" + itemRow["cnvcItemName"].ToString();
                    }
                }

                foreach (DataRow itemRow in standardItemDataSet.Tables[0].Rows)
                {
                    if (itemRow["parentId"].ToString() == "0")
                    {
                        //第一级
                        itemRow["StartTime"] = standardItemDataSet.Tables[0].Compute("min(StartTime)", " StartTime <> '' and cniStandardNo <> 0 ").ToString();
                        itemRow["EndTime"] = standardItemDataSet.Tables[0].Compute("max(EndTime)", " EndTime <> '' and cniStandardNo <> 0 ").ToString();
                        itemRow["BaseLineStartTime"] = standardItemDataSet.Tables[0].Compute("min(BaseLineStartTime)", " BaseLineStartTime <> '' and  cniStandardNo <> 0 ").ToString();
                        itemRow["BaseLineEndTime"] = standardItemDataSet.Tables[0].Compute("max(BaseLineEndTime)", " BaseLineEndTime <> '' and  cniStandardNo <> 0 ").ToString();
                    }
                    else if (itemRow["parentId"].ToString() != "0" && itemRow["standardNo"].ToString() == "10000")
                    {
                        string parentId = itemRow["parentId"].ToString();
                        itemRow["StartTime"] = standardItemDataSet.Tables[0].Compute("min(StartTime)", " StartTime <> '' and  cniStandardNo <> 0 and parentId = '" + parentId + "'").ToString();
                        itemRow["EndTime"] = standardItemDataSet.Tables[0].Compute("max(EndTime)", " EndTime <> '' and  cniStandardNo <> 0 and parentId = '" + parentId + "'").ToString();
                        itemRow["BaseLineStartTime"] = standardItemDataSet.Tables[0].Compute("min(BaseLineStartTime)", " BaseLineStartTime <> '' and  cniStandardNo <> 0 and parentId = '" + parentId + "'").ToString();
                        itemRow["BaseLineEndTime"] = standardItemDataSet.Tables[0].Compute("max(BaseLineEndTime)", " BaseLineEndTime <> '' and  cniStandardNo <> 0 and parentId = '" + parentId + "'").ToString();
                        itemRow["parentId"] = "1";
                        itemRow["cnvcParentId"] = parentId;
                    }
                    else
                    {
                        string threeparentId = itemRow["parentId"].ToString();
                        itemRow["parentId"] = standardItemDataSet.Tables[0].Compute("min(SEQ)", " cnvcParentId = '" + threeparentId + "' and standardNo = 10000 ").ToString();
                    }
                }
            }

            return standardItemDataSet;
        }

        private string GetColumnName(DataRow itemRow, DataSet iGanteeInfo, DataSet oGanteeInfo, string pointName)
        {
            string nameValue = "";
            string sourceColumnName = itemRow[pointName].ToString();
            if (sourceColumnName.IndexOf("Out") == 0)
            {
                sourceColumnName = sourceColumnName.Substring(3, sourceColumnName.Length - 3);
                if (oGanteeInfo != null)
                {
                    nameValue = oGanteeInfo.Tables[0].Rows[0][sourceColumnName].ToString();
                }
            }
            else if (sourceColumnName.IndexOf("In") == 0)
            {
                sourceColumnName = sourceColumnName.Substring(2, sourceColumnName.Length - 2);
                if (iGanteeInfo != null)
                {
                    nameValue = iGanteeInfo.Tables[0].Rows[0][sourceColumnName].ToString();
                }
            }
            return nameValue;
        }
    }
}
