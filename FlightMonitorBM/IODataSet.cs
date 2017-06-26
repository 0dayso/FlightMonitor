using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    public class IODataSet
    {
        public IODataSet()
        {
        }

        private DataSet iDataSet;
        private DataSet oDataSet;

        public DataSet IDataSet
        {
            set { this.iDataSet = value; }
            get { return this.iDataSet; }
        }

        public DataSet ODataSet
        {
            set { this.oDataSet = value; }
            get { return this.oDataSet; }
        }
    }
}
