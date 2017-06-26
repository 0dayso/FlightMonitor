using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmRelease : Form
    {
        public fmRelease()
        {
            InitializeComponent();
        }

        private void fmRelease_Load(object sender, EventArgs e)
        {
            this.Text = "升级说明";

            StringBuilder strReleaseNote = new StringBuilder();
            strReleaseNote.Append("2008-09-23 升级内容：\r\n\r\n");
            strReleaseNote.Append("1、修正了进港航班落地后航班条顺序错误的问题；\r\n");
            strReleaseNote.Append("2、增加了进港航班的“到位”时刻（挡轮档时刻）和出港航班的“推出”时刻（撤轮档时刻）；\r\n");
            strReleaseNote.Append("3、修改了部分字段的显示名称：\r\n");
            strReleaseNote.Append("   进港航班的“到达时刻|延误” 改为： “到达时刻|预达”\r\n");
            strReleaseNote.Append("   进港航班的“到达时刻|动态” 改为： “到达时刻|落地”\r\n");
            strReleaseNote.Append("   出港航班的“航班时刻|计划” 改为： “出发时刻|计划”\r\n");
            strReleaseNote.Append("   出港航班的“出发时刻|延误” 改为： “出发时刻|预计”\r\n");
            strReleaseNote.Append("   出港航班的“起飞动态”     改为： “出发时刻|起飞”；\r\n");
            strReleaseNote.Append("4、无论航班是否延误，都显示“到达时刻|预达”或“出发时刻|预计”（即原延误时间），如果航班延误，该单元格仍显示黄色；\r\n");
            strReleaseNote.Append("5、修正了用户登录数显示不正确的问题。");

            txtReleaseNote.Font = new Font("宋体", 10, FontStyle.Regular);
            txtReleaseNote.Text = strReleaseNote.ToString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //设置下次不再显示更新说明
            if (cbxIsShowNextTime.Checked)
            {
                ConfigSettings.WriteSetting("ShowUpdateRelease", "0");
            }
            this.Close();
        }
    }
}