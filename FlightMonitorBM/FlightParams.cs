using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    public class FlightParams
    {
        public FlightParams()
        {

        }

        private string m_flightNum;
        private string m_DepStn;
        private string m_ArrStn;
        private string m_LongReg;
        private string m_LegNo;
        private string m_Datop;
        private char m_IOFlight;//进出港标识位
        private string m_OIFlightType;//国内/国际航班
        private string m_FlightType;//航班类型

        public string FlightType
        {
            set { this.m_FlightType = value; }
            get { return this.m_FlightType; }
        }

        public string OIFlightType
        {
            set { this.m_OIFlightType = value; }
            get { return this.m_OIFlightType; }
        }

        public char IOFlight
        {
            set { this.m_IOFlight = value; }
            get { return this.m_IOFlight; }
        }

        public string FlightNum
        {
            set { this.m_flightNum = value; }
            get { return this.m_flightNum; }
        }

        public string DepStn
        {
            set { this.m_DepStn = value; }
            get { return this.m_DepStn; }
        }

        public string ArrStn
        {
            set { this.m_ArrStn = value; }
            get { return this.m_ArrStn; }
        }

        public string LongReg
        {
            set { this.m_LongReg = value; }
            get { return this.m_LongReg; }
        }

        public string Datop
        {
            set { this.m_Datop = value; }
            get { return this.m_Datop; }
        }

        public string LegNo
        {
            set { this.m_LegNo = value; }
            get { return this.m_LegNo; }
        }
    }
}
