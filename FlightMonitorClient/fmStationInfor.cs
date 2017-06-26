using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmStationInfor : Form
    {
        public fmStationInfor()
        {
            InitializeComponent();
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            //航班日期分割时刻只能为数字
            if (!Validator.IsNumber(txtDayLine.Text, "航班日期分割时刻只能为数字！"))
            {
                return;
            }

            //延误时间限制只能为数字
            if (!Validator.IsNumber(txtDelayTimeLine.Text, "延误时间限制只能为数字！"))
            {
                return;
            }

            //连飞时间限制只能为数字
            if (!Validator.IsNumber(txtJoinTimeLine.Text, "连飞时间限制只能为数字！"))
            {
                return;
            }

            //网络故障提示时间只能为数字
            if (!Validator.IsNumber(txtDayLine.Text, "网络故障提示时间只能为数字！"))
            {
                return;
            }


            StationBM stationBM = new StationBM();
            stationBM.ThreeCode = txtThreeCode.Text.Trim();
            stationBM.StationName = txtStationName.Text.Trim();
            stationBM.AirportName = txtAirportName.Text.Trim();
            stationBM.CommanderOfficeName = txtCommanderOfficeName.Text.Trim();
            stationBM.StationSignInFlag = txtStationSignInFlag.Text.Trim();

            stationBM.DayLine = Convert.ToInt32(txtDayLine.Text);
            stationBM.DelayTimeLine = Convert.ToInt32(txtDelayTimeLine.Text);
            stationBM.JoinTimeLine = Convert.ToInt32(txtJoinTimeLine.Text);
            stationBM.DisconnectTimeLine = Convert.ToInt32(txtDisconnectTimeLine.Text);


            StationBF stationBF = new StationBF();
            ReturnValueSF rvSF = stationBF.InsertStation(stationBM);

            if (rvSF.Result < 0)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}