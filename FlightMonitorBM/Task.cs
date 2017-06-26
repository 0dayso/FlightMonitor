using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    public class Task
    {
        private string iPK;
        private string id;
        private string name;
        private string startDate;
        private string endDate;
        private string precentDone;
        private string parentId;
        private string isLeaf;
        private string response;

        public Task()
        {
        }

        public string IPK
        {
            set
            {
                this.iPK = value;
            }
            get
            {
                return this.iPK;
            }
        }

        public string ID
        {
            set
            {
                this.id = value;
            }
            get
            {
                return this.id;
            }
        }

        public string NAME
        {
            set
            {
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }

        public string STARTDATE
        {
            set
            {
                this.startDate = value;
            }
            get
            {
                return this.startDate;
            }
        }

        public string ENDDATE
        {
            set
            {
                this.endDate = value;
            }
            get
            {
                return this.endDate;
            }
        }

        public string PRECENTDONE
        {
            set
            {
                this.precentDone = value;
            }
            get
            {
                return this.precentDone;
            }
        }

        public string PARENTID
        {
            set
            {
                this.parentId = value;
            }
            get
            {
                return this.parentId;
            }
        }

        public string ISLEAF
        {
            set
            {
                this.isLeaf = value;
            }
            get
            {
                return this.isLeaf;
            }
        }

        public string RESPONSE
        {
            set
            {
                this.response = value;
            }
            get
            {
                return this.response;
            }
        }
    }
}
