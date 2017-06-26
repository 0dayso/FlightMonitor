using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmStationgList : Form
    {
        public fmStationgList()
        {
            InitializeComponent();
        }

        private void fmStationgList_Load(object sender, EventArgs e)
        {
            StationBF stationBF = new StationBF();
            ReturnValueSF rvSF = stationBF.GetAllStation();
            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            fpsStationInfor.Sheets[0].Columns[0].DataField = "cncThreeCode";
            fpsStationInfor.Sheets[0].Columns[1].DataField = "cnvcStationName";
            fpsStationInfor.Sheets[0].Columns[2].DataField = "cnvcAirportName";
            fpsStationInfor.Sheets[0].Columns[3].DataField = "cnvcCommanderOfficeName";
            fpsStationInfor.Sheets[0].Columns[4].DataField = "cnvcStationSignInFlag";
            fpsStationInfor.Sheets[0].Columns[5].DataField = "cniDayLine";
            fpsStationInfor.Sheets[0].Columns[6].DataField = "cniDelayTimeLine";
            fpsStationInfor.Sheets[0].Columns[7].DataField = "cniJoinTimeLine";
            fpsStationInfor.Sheets[0].Columns[8].DataField = "cniDisconnectTimeLine";
            fpsStationInfor.Sheets[0].DataSource = rvSF.Dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            fmStationInfor objfmStationInfor = new fmStationInfor();
            if (objfmStationInfor.ShowDialog() == DialogResult.OK)
            {
                StationBF stationBF = new StationBF();
                ReturnValueSF rvSF = stationBF.GetAllStation();
                if (rvSF.Result < 0)
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                fpsStationInfor.Sheets[0].DataSource = rvSF.Dt;
            }
        }

        private void btnDeleteCommander_Click(object sender, EventArgs e)
        {
            if (fpsStationInfor.Sheets[0].SelectionCount == 0)
            {
                MessageBox.Show("请选择一条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("是否删除所选的用户？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            DataTable dtStation = (DataTable)fpsStationInfor.Sheets[0].DataSource;
            StationBM stationBM = new StationBM(dtStation.Rows[fpsStationInfor.Sheets[0].Models.Selection.AnchorRow]);

            StationBF stationBF = new StationBF();
            ReturnValueSF rvSF = stationBF.DeleteStation(stationBM);

            if (rvSF.Result > 0)
            {                
                rvSF = stationBF.GetAllStation();
                if (rvSF.Result < 0)
                {
                    MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                fpsStationInfor.Sheets[0].DataSource = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}