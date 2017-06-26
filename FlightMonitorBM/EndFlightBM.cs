using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航后航班动态实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class EndFlightBM : DataSet
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EndFlightBM()
        {
            BuildTables();
        }

                /// <summary>
        /// 定义航班计划表结构
        /// </summary>
        private void BuildTables()
        {
            DataTable dtEndFlights = new DataTable("EndFlights");
            DataColumnCollection dcEndFlights = dtEndFlights.Columns;

            dcEndFlights.Add("IncncDATOP", typeof(System.String));
            dcEndFlights.Add("IncnvcFLTID", typeof(System.String));
            dcEndFlights.Add("IncniLEGNO", typeof(System.Int32));
            dcEndFlights.Add("IncnvcAC", typeof(System.String));
            dcEndFlights.Add("IncncFlightDate", typeof(System.String));
            dcEndFlights.Add("IncnvcLONG_REG", typeof(System.String));
            dcEndFlights.Add("IncncDEPSTN", typeof(System.String));
            dcEndFlights.Add("IncncARRSTN", typeof(System.String));
            dcEndFlights.Add("IncncSTD", typeof(System.String));
            dcEndFlights.Add("IncncSTA", typeof(System.String));
            dcEndFlights.Add("IncncTDWN", typeof(System.String));
            dcEndFlights.Add("IncnvcInGATE", typeof(System.String));


            dcEndFlights.Add("OutcncDATOP", typeof(System.String));
            dcEndFlights.Add("OutcnvcFLTID", typeof(System.String));
            dcEndFlights.Add("OutcniLEGNO", typeof(System.Int32));
            dcEndFlights.Add("OutcnvcAC", typeof(System.String));
            dcEndFlights.Add("OutcncFlightDate", typeof(System.String));
            dcEndFlights.Add("OutcnvcLONG_REG", typeof(System.String));
            dcEndFlights.Add("OutcncDEPSTN", typeof(System.String));
            dcEndFlights.Add("OutcncARRSTN", typeof(System.String));
            dcEndFlights.Add("OutcncSTD", typeof(System.String));
            dcEndFlights.Add("OutcncSTA", typeof(System.String));            
            dcEndFlights.Add("OutcnvcOutGATE", typeof(System.String));//
            dcEndFlights.Add("OutcnvcGateRemark", typeof(System.String));
            
            this.Tables.Add(dtEndFlights);
        }
    }
}
