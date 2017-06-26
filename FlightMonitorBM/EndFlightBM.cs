using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// ���󺽰ද̬ʵ�����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class EndFlightBM : DataSet
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public EndFlightBM()
        {
            BuildTables();
        }

                /// <summary>
        /// ���庽��ƻ���ṹ
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
