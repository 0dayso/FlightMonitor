using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// yanxian
    /// 2013-10-09
    /// 旅客信息处理类
    /// </summary>
    public class TravellerBF
    {
        private SimWebService.Service simWebService = new AirSoft.FlightMonitor.FlightMonitorBF.SimWebService.Service();

        public DataSet getTravellerDataSet(string strFLTID,string strDATOP,string strDEP,string strARR)
        {
            DataSet dataSet = new DataSet();
            dataSet = simWebService.getFLTPSR(strFLTID,strDATOP,strDEP,strARR);
            if (dataSet == null || dataSet.Tables.Count == 0)
                return null;
            else
                return dataSet;
        }

        public DataSet getTravellerSumDataSet(string strFLTID, string strDATOP, string strDEP, string strARR)
        {
            DataSet dataSet = new DataSet();
            dataSet = simWebService.getFLTPSRSUM(strFLTID, strDATOP, strDEP, strARR);
            if (dataSet == null || dataSet.Tables.Count == 0)
                return null;
            else
                return dataSet;
        }
    }
}
