using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 保障项目类表
    /// </summary>
    /// author : yanxian 
    /// date : 2013-11-13
    public class ItemSumBM
    {
        public ItemSumBM()
        {
        }

        public String ItemName
        {
            set { this.ItemName = value; }
            get { return this.ItemName; }
        }

        public String LeafNo
        {
            set { this.LeafNo = value; }
            get { return this.LeafNo; }
        }
    }
}
