using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ∫Ω∞‡¿‡–Õ±Ì
    /// </summary>
    /// author : yanxian 
    /// date : 2013-11-13
    public class FlightTypeBM
    {
        public FlightTypeBM()
        {
        }

        public String FlightTypeName
        {
            set { this.FlightTypeName = value; }
            get { return this.FlightTypeName; }
        }

        public String FNCode
        {
            set { this.FNCode = value; }
            get { return this.FNCode; }
        }
    }
}
