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
    /// ��ȡ���캽վ�����к���
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
    /// ��ȡĳϯλ�����к��ද̬
    /// </summary>
    public byte[] GetFlightsByPosition(DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
    {
        byte[] bArrayResult = null;
        DataTable dtPositionFlights = new DataTable();

        //����ҵ����۲㷽��
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
    /// ���ౣ�ϻ�ȡ���±������
    /// </summary>
    public byte[] GetLastGuaranteeChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, StationBM stationBM)
    {
        byte[] bArrayResult = null;
        DataTable dtChangeTable = new DataTable();
        
        //����ҵ����۲㷽��
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
    /// �������ౣ��ʱ��ȡ���������
    /// </summary>
    public byte[] GetChangeRecords(DateTimeBM dateTimeBM, StationBM stationBM)
    {
        byte[] bArrayResult = null;
        DataTable dtChangeTable = new DataTable();

        //����ҵ����۲㷽��
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
    /// ������ʵʱ�������
    /// </summary>
    public byte[] GetLastWatchChangeRecords(int iLastRecordNo, DateTimeBM dateTimeBM, PositionNameBM positionNameBM)
    {
        byte[] bArrayResult = null;
        DataTable dtChangeTable = new DataTable();

        //����ҵ����۲㷽��
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
