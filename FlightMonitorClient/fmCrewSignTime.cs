using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmCrewSignTime : Form
    {
        private string m_strQueryTime;
        private string m_strDEPSTN;
        private string m_strCrewName;

        public fmCrewSignTime(string strQueryTime, string strDEPSTN, string strCrewName)
        {
            InitializeComponent();
            this.m_strQueryTime = strQueryTime;
            this.m_strDEPSTN = strDEPSTN;
            this.m_strCrewName = strCrewName;
        }

        private void fmCrewSignTime_Load(object sender, EventArgs e)
        {
            //机组签到记录列表
            fpsGetCrewSignIn.ActiveSheet.Columns[0].DataField = "name";
            fpsGetCrewSignIn.ActiveSheet.Columns[1].DataField = "CardSerial";
            fpsGetCrewSignIn.ActiveSheet.Columns[2].DataField = "PersonnelID";
            //fpsGetCrewSignIn.ActiveSheet.Columns[3].DataField = "KSDATETIME";


            CrewSignInBF crewSignInBF = new CrewSignInBF();
            StationBF stationBF = new StationBF();

            ReturnValueSF rvSF = stationBF.GetStationByThreeCode(m_strDEPSTN);

            string strSingInFlag = "";
            if (rvSF.Result > 0)
            {
                if(rvSF.Dt.Rows.Count > 0)
                {
                    strSingInFlag = rvSF.Dt.Rows[0]["cnvcStationSignInFlag"].ToString();
                }
            }

            rvSF = crewSignInBF.GetCrewSignTime(m_strCrewName, strSingInFlag);

            fpsGetCrewSignIn.DataSource = rvSF.Dt;


            int iLoop = 0;
            foreach (DataRow dataRow in rvSF.Dt.Rows)
            {
                fpsGetCrewSignIn.ActiveSheet.Cells[iLoop, 3].Text = DateTime.Parse(dataRow["KSDATETIME"].ToString()).ToString("HHmm");
            }
        }
    }
}