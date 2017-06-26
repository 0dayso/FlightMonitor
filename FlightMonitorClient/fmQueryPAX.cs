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
        #region �������
        private ChangeLegsBM _changeLegsBM = new ChangeLegsBM();    //��ѡ��ĳ��ۺ������Ϣ
        private FlightOrientation _flightOrientation;               //���ۡ�����
        #endregion �������


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
            #region �������
            this.Text = "�ÿͷ�����Ϣ���������ڣ�" +
                _changeLegsBM.FlightDate + " ����ţ�" +
                _changeLegsBM.FlightNo + " �ɻ��ţ�" +
                _changeLegsBM.LONG_REG + " ���Σ� " +
                _changeLegsBM.DEPSTN +
                " - " +
                _changeLegsBM.ARRSTN +
                "��";

            if (_flightOrientation == FlightOrientation.In)
            {
                this.Text = "���ۣ�" + this.Text;
            }
            else
            {
                this.Text = "���ۣ�" + this.Text;
            }
            #endregion �������

            #region �����ÿ���Ϣ
            LinYong.PublicService.PublicService publicService = new LinYong.PublicService.PublicService();
            String encodeUsername = publicService.DESEncrypt("pass4-opm", "q1w2e3r4");// pass1-opm,pass2-opm,pass3-opm,pass4-opm,�����û�������һ��, pass1-opm�������
            String encodeFltNo = publicService.DESEncrypt(_changeLegsBM.FlightNo.Replace(" ",""), "q1w2e3r4");
            String encodeDatopChn = publicService.DESEncrypt(_changeLegsBM.FlightDate, "q1w2e3r4");//�������ڣ�����ʱ�䣩

            DateTime dateTimeNow = DateTime.UtcNow;
            //DateTime dateTimeNow = Convert.ToDateTime("2016-3-30 10:28:32.934"); //����ʹ��
            DateTime dateTimeStart = Convert.ToDateTime("1970-01-01 00:00:00.000");
            TimeSpan timeSpan = new TimeSpan();
            timeSpan = dateTimeNow - dateTimeStart;
            long totalMilliseconds = Convert.ToInt64(timeSpan.TotalMilliseconds);
            string encodePwd = publicService.DESEncrypt(totalMilliseconds.ToString(), "q1w2e3r4");//����ʱ��UTCʱ��

            byte[] post = Encoding.UTF8.GetBytes("fltNo=" + encodeFltNo + "&datopChn=" + encodeDatopChn);
            String url = "http://" + encodeUsername + ":" + encodePwd + "@10.72.16.131/opm-web/passenger/passengerDetail.do";
            String PostHeaders = "Content-Type: application/x-www-form-urlencoded";
            webBrowser1.Navigate(url, null, post, PostHeaders);
            #endregion �����ÿ���Ϣ

        }
    }
}