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
    public partial class fmStewardSignTime : Form
    {
        private string m_strQueryTime;
        private string m_strDEPSTN;
        private string m_strStewardName;

        public fmStewardSignTime(string strQueryTime, string strDEPSTN, string strStewardName)
        {
            InitializeComponent();

            this.m_strQueryTime = strQueryTime;
            this.m_strDEPSTN = strDEPSTN;
            this.m_strStewardName = strStewardName;
        }

        private void fmStewardSignTime_Load(object sender, EventArgs e)
        {
            //乘务签到记录列表
            fpsGetStewardSignIn.ActiveSheet.Columns[0].DataField = "name";
            fpsGetStewardSignIn.ActiveSheet.Columns[1].DataField = "CardSerial";
            fpsGetStewardSignIn.ActiveSheet.Columns[2].DataField = "PersonnelID";

            CrewSignInBF crewSignInBF = new CrewSignInBF();
            StationBF stationBF = new StationBF();

            ReturnValueSF rvSF = stationBF.GetStationByThreeCode(m_strDEPSTN);

            string strSingInFlag = "";
            if (rvSF.Result > 0)
            {
                if (rvSF.Dt.Rows.Count > 0)
                {
                    strSingInFlag = rvSF.Dt.Rows[0]["cnvcStationSignInFlag"].ToString();
                }
            }

            rvSF = crewSignInBF.GetStewardSignTime(m_strStewardName, strSingInFlag);

            fpsGetStewardSignIn.DataSource = rvSF.Dt;


            int iLoop = 0;
            foreach (DataRow dataRow in rvSF.Dt.Rows)
            {
                fpsGetStewardSignIn.ActiveSheet.Cells[iLoop, 3].Text = DateTime.Parse(dataRow["KSDATETIME"].ToString()).ToString("HHmm");
            }
        }
    }
}