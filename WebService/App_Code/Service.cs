using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    /// <summary>
    /// 获取当天航站的所有航班
    /// </summary>
    public byte[] GetStationFlights(DateTimeBM dateTimeBM, StationBM stationBM)
    {
        byte[] bArrayResult = null;
        DataTable dtStationFlights = new DataTable();

        GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
        ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByStation(dateTimeBM, stationBM);

        if (rvSF.Result > 0)
        {
            dtStationFlights = rvSF.Dt;
            dtStationFlights.RemotingFormat = SerializationFormat.Binary;

            MemoryStream ms = new MemoryStream();

            IFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, dtStationFlights);
            bArrayResult = ms.ToArray();
            ms.Close();
            ms.Dispose();
            
        }
        
        return bArrayResult;
    }

    [WebMethod]
    /// <summary>
    /// 获取某席位的所有航班动态
    /// </summary>
    public byte[] GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
    {
        byte[] bArrayResult = null;
        DataTable dtPositionFlights = new DataTable();

        //调用业务外观层方法
        GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();


        ReturnValueSF rvSF = guaranteeInforBF.GetFlightsByPosition(dateTimeBM, positionNameBM);

        if (rvSF.Result > 0)
        {
            dtPositionFlights = rvSF.Dt;
            dtPositionFlights.RemotingFormat = SerializationFormat.Binary;

            MemoryStream ms = new MemoryStream();

            IFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, dtPositionFlights);
            bArrayResult = ms.ToArray();
            ms.Close();
            ms.Dispose();            
        }

        return bArrayResult;
    }

    [WebMethod]
    /// <summary>
    /// 航班保障获取最新变更数据
    /// </summary>
    public byte[] GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
    {
        byte[] bArrayResult = null;
        DataTable dtChangeTable = new DataTable();
        
        //调用业务外观层方法
        ChangeRecordBF changeRecordBF = new ChangeRecordBF();

        ReturnValueSF rvSF = changeRecordBF.GetLastGuaranteeChangeRecords(iLastRecordNo, dateTimeBM, stationBM);

        if (rvSF.Result > 0)
        {
            dtChangeTable = rvSF.Dt;
            dtChangeTable.RemotingFormat = SerializationFormat.Binary;

            MemoryStream ms = new MemoryStream();

            IFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, dtChangeTable);
            bArrayResult = ms.ToArray();
            ms.Close();
            ms.Dispose();
        }

        return bArrayResult;
    }

    [WebMethod]
    /// <summary>
    /// 启动航班保障时获取最后变更数据
    /// </summary>
    public byte[] GetChangeRecords(DateTimeBM dateTimeBM, StationBM stationBM)
    {
        byte[] bArrayResult = null;
        DataTable dtChangeTable = new DataTable();

        //调用业务外观层方法
        ChangeRecordBF changeRecordBF = new ChangeRecordBF();

        ReturnValueSF rvSF = changeRecordBF.GetChangeRecords(m_i dateTimeBM, stationBM);

        if (rvSF.Result > 0)
        {
            dtChangeTable = rvSF.Dt;
            dtChangeTable.RemotingFormat = SerializationFormat.Binary;

            MemoryStream ms = new MemoryStream();

            IFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, dtChangeTable);
            bArrayResult = ms.ToArray();
            ms.Close();
            ms.Dispose();
        }

        return bArrayResult;
    }

    [WebMethod]
    /// <summary>
    /// 航班监控实时变更数据
    /// </summary>
    public byte[] GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
    {
        byte[] bArrayResult = null;
        DataTable dtChangeTable = new DataTable();

        //调用业务外观层方法
        ChangeRecordBF changeRecordBF = new ChangeRecordBF();

        ReturnValueSF rvSF = changeRecordBF.GetLastWatchChangeRecords(iLastRecordNo, dateTimeBM, positionNameBM);

        if (rvSF.Result > 0)
        {
            dtChangeTable = rvSF.Dt;
            dtChangeTable.RemotingFormat = SerializationFormat.Binary;

            MemoryStream ms = new MemoryStream();

            IFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, dtChangeTable);
            bArrayResult = ms.ToArray();
            ms.Close();
            ms.Dispose();
        }

        return bArrayResult;
    }
    
}
