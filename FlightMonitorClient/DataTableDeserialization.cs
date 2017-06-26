using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public class DataTableDeserialization
    {
        public static DataTable GetDataTableFromByte(byte[] binaryData)
        {
            DataTable dtResult = new DataTable();
            MemoryStream ms = new MemoryStream(binaryData);

            IFormatter brFormatter = new BinaryFormatter();

            object obj = brFormatter.Deserialize(ms);

            dtResult = (DataTable)obj;

            return dtResult;
        }
    }
}
