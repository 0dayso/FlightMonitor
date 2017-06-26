using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 航班计划实体对象
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ScheduleLegsBM:DataSet
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScheduleLegsBM()
        {
            BuildTables();
        }

        /// <summary>
        /// 定义航班计划表结构
        /// </summary>
        private void BuildTables()
        {
            DataTable dtLegs = new DataTable("legs");
            DataColumnCollection dcLegs = dtLegs.Columns;

            dcLegs.Add("DATOP", typeof(System.String));
            dcLegs.Add("FLTID", typeof(System.String));
            dcLegs.Add("LegNO", typeof(System.Int32));
            dcLegs.Add("AC", typeof(System.String));
            dcLegs.Add("FlightDate", typeof(System.String));
            dcLegs.Add("LONG_REG", typeof(System.String));
            dcLegs.Add("DEPSTN", typeof(System.String));
            dcLegs.Add("ARRSTN", typeof(System.String));
            dcLegs.Add("STD", typeof(System.String));
            dcLegs.Add("STA", typeof(System.String));
            dcLegs.Add("STATUS", typeof(System.String));
            dcLegs.Add("ETD", typeof(System.String));
            dcLegs.Add("ETA", typeof(System.String));
            dcLegs.Add("ATD", typeof(System.String));
            dcLegs.Add("TOFF", typeof(System.String));
            dcLegs.Add("TDWN", typeof(System.String));
            dcLegs.Add("ATA", typeof(System.String));
            dcLegs.Add("TRI_FLTID", typeof(System.String));
            dcLegs.Add("DIV_RCODE", typeof(System.String));
            dcLegs.Add("DIV_FLAG", typeof(System.String));
            dcLegs.Add("PAX", typeof(System.String));
            dcLegs.Add("BOOK", typeof(System.String));
            dcLegs.Add("DELAY1", typeof(System.String));
            dcLegs.Add("DUR1", typeof(System.Int32));
            dcLegs.Add("DELAY2", typeof(System.String));
            dcLegs.Add("DUR2", typeof(System.String));
            dcLegs.Add("DELAY3", typeof(System.String));
            dcLegs.Add("DUR3", typeof(System.String));
            dcLegs.Add("DELAY4", typeof(System.String));
            dcLegs.Add("DUR4", typeof(System.Int32));
            dcLegs.Add("GATE", typeof(System.String));
            dcLegs.Add("STC", typeof(System.String));
            dcLegs.Add("VERSION", typeof(System.String));
            dcLegs.Add("ORIG_ACTYP", typeof(System.String));
            dcLegs.Add("ACTYP", typeof(System.String));
            dcLegs.Add("ACOWN", typeof(System.String));
            dcLegs.Add("DELACTION", typeof(System.String));
            dcLegs.Add("SEQ", typeof(System.String));
            dcLegs.Add("TSTAMP", typeof(System.String));
            this.Tables.Add(dtLegs);
        }
    }
}
