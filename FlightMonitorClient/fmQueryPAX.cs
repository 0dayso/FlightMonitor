using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBM;
using LinYong.PublicService;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmQueryPAX : Form
    {
        #region 定义变量
        private ChangeLegsBM _changeLegsBM = new ChangeLegsBM();    //所选择的出港航班的信息
        private FlightOrientation _flightOrientation;               //进港、出港
        #endregion 定义变量


        public fmQueryPAX()
        {
            InitializeComponent();
        }

        public fmQueryPAX(ChangeLegsBM changeLegsBM, FlightOrientation flightOrientation)
        {
            InitializeComponent();

            //
            _changeLegsBM = changeLegsBM;
            _flightOrientation = flightOrientation;
        }

        private void fmQueryPAX_Load(object sender, EventArgs e)
        {
            #region 窗体标题
            this.Text = "旅客分类信息【航班日期：" +
                _changeLegsBM.FlightDate + " 航班号：" +
                _changeLegsBM.FlightNo + " 飞机号：" +
                _changeLegsBM.LONG_REG + " 航段： " +
                _changeLegsBM.DEPSTN +
                " - " +
                _changeLegsBM.ARRSTN +
                "】";

            if (_flightOrientation == FlightOrientation.In)
            {
                this.Text = "进港：" + this.Text;
            }
            else
            {
                this.Text = "出港：" + this.Text;
            }
            #endregion 窗体标题

            #region 接入旅客信息
            LinYong.PublicService.PublicService publicService = new LinYong.PublicService.PublicService();
            String encodeUsername = publicService.DESEncrypt("pass4-opm", "q1w2e3r4");// pass1-opm,pass2-opm,pass3-opm,pass4-opm,根据用户级别用一个, pass1-opm级别最低
            String encodeFltNo = publicService.DESEncrypt(_changeLegsBM.FlightNo.Replace(" ",""), "q1w2e3r4");
            String encodeDatopChn = publicService.DESEncrypt(_changeLegsBM.FlightDate, "q1w2e3r4");//航班日期（北京时间）

            DateTime dateTimeNow = DateTime.UtcNow;
            //DateTime dateTimeNow = Convert.ToDateTime("2016-3-30 10:28:32.934"); //测试使用
            DateTime dateTimeStart = Convert.ToDateTime("1970-01-01 00:00:00.000");
            TimeSpan timeSpan = new TimeSpan();
            timeSpan = dateTimeNow - dateTimeStart;
            long totalMilliseconds = Convert.ToInt64(timeSpan.TotalMilliseconds);
            string encodePwd = publicService.DESEncrypt(totalMilliseconds.ToString(), "q1w2e3r4");//调用时刻UTC时间

            byte[] post = Encoding.UTF8.GetBytes("fltNo=" + encodeFltNo + "&datopChn=" + encodeDatopChn);
            String url = "http://" + encodeUsername + ":" + encodePwd + "@10.72.16.131/opm-web/passenger/passengerDetail.do";
            String PostHeaders = "Content-Type: application/x-www-form-urlencoded";
            webBrowser1.Navigate(url, null, post, PostHeaders);
            #endregion 接入旅客信息

        }
    }
}