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
            //�������ڷָ�ʱ��ֻ��Ϊ����
            if (!Validator.IsNumber(txtDayLine.Text, "�������ڷָ�ʱ��ֻ��Ϊ���֣�"))
            {
                return;
            }

            //����ʱ������ֻ��Ϊ����
            if (!Validator.IsNumber(txtDelayTimeLine.Text, "����ʱ������ֻ��Ϊ���֣�"))
            {
                return;
            }

            //����ʱ������ֻ��Ϊ����
            if (!Validator.IsNumber(txtJoinTimeLine.Text, "����ʱ������ֻ��Ϊ���֣�"))
            {
                return;
            }

            //���������ʾʱ��ֻ��Ϊ����
            if (!Validator.IsNumber(txtDayLine.Text, "���������ʾʱ��ֻ��Ϊ���֣�"))
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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