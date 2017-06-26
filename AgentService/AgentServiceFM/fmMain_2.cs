using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization; 

namespace AirSoft.FlightMonitor.AgentServiceFM
{
    /// <summary>
    /// ��QQ�������ܹ��������غ���ʾ
    /// </summary>
    public partial class fmMain_2 : Form
    {
        Timer checkDockTimer = new Timer();
        private AnchorStyles StopDock = AnchorStyles.None;

        private void StopRectTimer_Tick(object sender, EventArgs e)
        {
            //�������ڴ����ϣ������ͣ��λ����ʾ��������
            if (this.Bounds.Contains(Cursor.Position))
            {
                switch (this.StopDock)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, 0);
                        break;
                    case AnchorStyles.Bottom:
                        this.Location = new Point(this.Location.X, Screen.PrimaryScreen.Bounds.Height - this.Height);
                        break;
                    case AnchorStyles.Left:
                        this.Location = new Point(0, this.Location.Y);
                        break;
                    case AnchorStyles.Right:
                        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, this.Location.Y);
                        break;
                }
            }
            else  //�������뿪���壬�����ͣ��λ�����ش��壬�����������ִ����Ե�Ա����ѡ�д���
            {
                switch (this.StopDock)
                {
                    case AnchorStyles.Top:
                        this.Location = new Point(this.Location.X, (this.Height - 3) * (-1));
                        break;
                    case AnchorStyles.Bottom:
                        this.Location = new Point(this.Location.X, Screen.PrimaryScreen.Bounds.Height - 5);
                        break;
                    case AnchorStyles.Left:
                        this.Location = new Point((-1) * (this.Width - 3), this.Location.Y);
                        break;
                    case AnchorStyles.Right:
                        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 2, this.Location.Y);
                        break;
                }
            }
        }

        public fmMain_2()
        {
            InitializeComponent();
        }

        private void fmMain_2_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            checkDockTimer.Tick += new EventHandler(StopRectTimer_Tick);
            checkDockTimer.Interval = 100;
            checkDockTimer.Enabled = true;

        }

        /// <summary>
        /// ���Ĵ����λ��ʱ�����ݺ͸��������Ե�ľ��븳ֵͣ����λ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMain_2_LocationChanged(object sender, EventArgs e)
        {
            if (this.Top <= 0)
            {
                this.StopDock = AnchorStyles.Top;
            }
            else if (this.Bottom >= Screen.PrimaryScreen.Bounds.Height)
            {
                this.StopDock = AnchorStyles.Bottom;
            }
            else if (this.Left <= 0)
            {
                this.StopDock = AnchorStyles.Left;
            }
            else if (this.Left >= Screen.PrimaryScreen.Bounds.Width - this.Width)
            {
                this.StopDock = AnchorStyles.Right;
            }
            else
            {
                this.StopDock = AnchorStyles.None;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = "MMpyyyypdd";
            dtInfo.DateSeparator = "p";
            DateTime dt = Convert.ToDateTime("12p1999p15", dtInfo);
            textBox1.Text = dt.ToShortDateString();
            //textBox1.Text = dtInfo.ShortDatePattern + ";" + dtInfo.LongDatePattern;
            //Convert.ToDateTime("",
            //DateTimeFormatInfo..ShortDatePattern = "yyyyMMdd";
            //DateTime dt = Convert.ToDateTime("19991215", DateTimeFormatInfo.InvariantInfo);   



        }

        private void fmMain_2_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                checkDockTimer.Enabled = false;
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                checkDockTimer.Enabled = true;
            } 
        }
    }
}