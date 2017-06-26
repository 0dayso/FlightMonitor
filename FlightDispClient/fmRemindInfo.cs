using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmRemindInfo : Form
    {
        public fmRemindInfo(PositionNameBM deskNameBM)
        {
            InitializeComponent();
        }

        private void fmRemindInfo_Load(object sender, EventArgs e)
        {
            DataTable dtRemindInfo = new DataTable();

            dtRemindInfo.Columns.Add("提示类型");
            dtRemindInfo.Columns.Add("飞机号");
            dtRemindInfo.Columns.Add("航班号");
            dtRemindInfo.Columns.Add("起飞机场");
            dtRemindInfo.Columns.Add("目的机场");
            dtRemindInfo.Columns.Add("提示内容");

            DataRow dr1 = dtRemindInfo.NewRow();
            dr1["提示类型"] = "ACARS";
            dr1["飞机号"] = "B2490";
            dr1["航班号"] = "HU7181";
            dr1["起飞机场"] = "海口";
            dr1["目的机场"] = "北京";
            dr1["提示内容"] = "飞机推出";
            dtRemindInfo.Rows.Add(dr1);

            DataRow dr2 = dtRemindInfo.NewRow();
            dr2["提示类型"] = "气象";
            dr2["飞机号"] = "";
            dr2["航班号"] = "";
            dr2["起飞机场"] = "西安";
            dr2["目的机场"] = "太原";
            dr2["提示内容"] = "大风";
            dtRemindInfo.Rows.Add(dr2);

            DataRow dr3 = dtRemindInfo.NewRow();
            dr3["提示类型"] = "航行通告";
            dr3["飞机号"] = "";
            dr3["航班号"] = "";
            dr3["起飞机场"] = "乌鲁木齐";
            dr3["目的机场"] = "北京";
            dr3["提示内容"] = "空军活动";
            dtRemindInfo.Rows.Add(dr3);

            DataRow dr4 = dtRemindInfo.NewRow();
            dr4["提示类型"] = "MEL";
            dr4["飞机号"] = "B2490";
            dr4["航班号"] = "";
            dr4["起飞机场"] = "";
            dr4["目的机场"] = "";
            dr4["提示内容"] = "发动机问题";
            dtRemindInfo.Rows.Add(dr4);

            DataRow dr5 = dtRemindInfo.NewRow();
            dr5["提示类型"] = "机组";
            dr5["飞机号"] = "B2561";
            dr5["航班号"] = "HU7358";
            dr5["起飞机场"] = "太原";
            dr5["目的机场"] = "海口";
            dr5["提示内容"] = "机长生病";
            dtRemindInfo.Rows.Add(dr5);

            //DataRow dr1 = dtRemindInfo.NewRow();
            //dr1["提示类型"] = "ACARS";
            //dr1["飞机号"] = "B2490";
            //dr1["航班号"] = "HU7181";
            //dr1["起飞机场"] = "海口";
            //dr1["目的机场"] = "北京";
            //dr1["提示内容"] = "飞机推出";
            //dtRemindInfo.Rows.Add(dr1);

            dgvRemind.DataSource = dtRemindInfo;
            dgvRemind.DataMember = dtRemindInfo.TableName;
        }
    }
}