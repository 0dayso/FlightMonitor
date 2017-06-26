using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    class fmGanttView : UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private HScrollBar oHScrollBar;
        private VScrollBar oVScrollBar;
        private ToolTip oToolTip;
        private PictureBox oPictureBox;

        private Color titleColor = Color.LightGray;
        private Color fontColor = Color.Black;

        private Color onelineColor = Color.White;
        private Color twolineColor = Color.WhiteSmoke;

        private Color oneHourColor = Color.LightGray;
        private Color twoHourColor = Color.LightGray;
        private PictureBox pictureBoxTreeView;
        private HScrollBar hScrollBarLeft;


        private Color headerColor = Color.Gray;

        private Pen oLinePen = new Pen(Brushes.Gray, 1);
        private Font oTextFont = new Font("����", 9.0f, FontStyle.Bold);// new Font(SystemFonts.DefaultFont.FontFamily, 10, FontStyle.Bold);//
        private Brush oTextBrush = new SolidBrush(Color.Black);
        private Brush oBackgroundBrush = new SolidBrush(Color.LightGray);

        private Brush fTextBrush = new SolidBrush(Color.Red); //һ����ɫ��ʾ

        private Brush sTextBrush = new SolidBrush(Color.IndianRed); //������ɫ��ʾ

        private Brush tTextBrush = new SolidBrush(Color.Black); //������ʾ

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.oHScrollBar = new System.Windows.Forms.HScrollBar();
            this.oVScrollBar = new System.Windows.Forms.VScrollBar();
            this.oPictureBox = new System.Windows.Forms.PictureBox();
            this.oToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxTreeView = new System.Windows.Forms.PictureBox();
            this.hScrollBarLeft = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.oPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTreeView)).BeginInit();
            this.SuspendLayout();
            // 
            // oHScrollBar
            // 
            this.oHScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oHScrollBar.LargeChange = 1;
            this.oHScrollBar.Location = new System.Drawing.Point(185, 346);
            this.oHScrollBar.Maximum = 10;
            this.oHScrollBar.Name = "oHScrollBar";
            this.oHScrollBar.Size = new System.Drawing.Size(598, 16);
            this.oHScrollBar.TabIndex = 0;
            this.oHScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.oHScrollBar_Scroll);
            // 
            // oVScrollBar
            // 
            this.oVScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oVScrollBar.LargeChange = 1;
            this.oVScrollBar.Location = new System.Drawing.Point(784, 0);
            this.oVScrollBar.Maximum = 10;
            this.oVScrollBar.Name = "oVScrollBar";
            this.oVScrollBar.Size = new System.Drawing.Size(16, 345);
            this.oVScrollBar.TabIndex = 1;
            this.oVScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.oVScrollBar_Scroll);
            // 
            // oPictureBox
            // 
            this.oPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oPictureBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.oPictureBox.Location = new System.Drawing.Point(185, 0);
            this.oPictureBox.Name = "oPictureBox";
            this.oPictureBox.Size = new System.Drawing.Size(598, 345);
            this.oPictureBox.TabIndex = 2;
            this.oPictureBox.TabStop = false;
            this.oPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.oPictureBox_MouseMove);
            this.oPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oPictureBox_MouseDown);
            this.oPictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.oPictureBox_MouseWheel);
            // 
            // pictureBoxTreeView
            // 
            this.pictureBoxTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxTreeView.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pictureBoxTreeView.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTreeView.Name = "pictureBoxTreeView";
            this.pictureBoxTreeView.Size = new System.Drawing.Size(185, 345);
            this.pictureBoxTreeView.TabIndex = 3;
            this.pictureBoxTreeView.TabStop = false;
            // 
            // hScrollBarLeft
            // 
            this.hScrollBarLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hScrollBarLeft.LargeChange = 1;
            this.hScrollBarLeft.Location = new System.Drawing.Point(0, 346);
            this.hScrollBarLeft.Maximum = 10;
            this.hScrollBarLeft.Name = "hScrollBarLeft";
            this.hScrollBarLeft.Size = new System.Drawing.Size(185, 16);
            this.hScrollBarLeft.TabIndex = 4;
            this.hScrollBarLeft.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarLeft_Scroll);
            // 
            // fmGanttView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Controls.Add(this.hScrollBarLeft);
            this.Controls.Add(this.pictureBoxTreeView);
            this.Controls.Add(this.oPictureBox);
            this.Controls.Add(this.oVScrollBar);
            this.Controls.Add(this.oHScrollBar);
            this.Name = "fmGanttView";
            this.Size = new System.Drawing.Size(801, 363);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GanttView_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.oPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTreeView)).EndInit();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public fmGanttView()
        {
            InitializeComponent();
        }


        #region ȫ�ֱ���ֵ
        /// <summary>
        /// ͷ���и�
        /// </summary>
        private int iHeaderLineHeight = 20;

        /// <summary>
        /// ͷ���и�
        /// </summary>
        public int HeaderLineHeight
        {
            get
            {
                return this.iHeaderLineHeight;
            }
            set
            {
                this.iHeaderLineHeight = value;
                //this.draw();
            }
        }


        /// <summary>
        /// ÿһ���ӵĿ��
        /// </summary>
        private int iMinuteWidth = 10;

        /// <summary>
        /// ÿһ���ӵĿ��
        /// </summary>
        public int MinuteWidth
        {
            get
            {
                return this.iMinuteWidth;
            }
            set
            {
                this.iMinuteWidth = value;
                //this.draw();
            }
        }

        /// <summary>
        /// �ڵ�ĸ߶�
        /// </summary>
        private int iNodeHeight = 50;

        /// <summary>
        /// �ڵ�ĸ߶�
        /// </summary>
        public int NodeHeight
        {
            get
            {
                return this.iNodeHeight;
            }
            set
            {
                this.iNodeHeight = value;
                //this.draw();
            }
        }

        /// <summary>
        /// �ڵ�Ĵ�ֱΪֵ
        /// </summary>
        private int iVerticalSpace = 15;

        /// <summary>
        /// �ڵ�Ĵ�ֱΪֵ
        /// </summary>
        public int VerticalSpace
        {
            get
            {
                return this.iVerticalSpace;
            }
            set
            {
                this.iVerticalSpace = value;
                //this.draw();
            }
        }

        /// <summary>
        /// ��¼ˮƽ������ֵ
        /// </summary>
        private int iVScroll = 0;

        /// <summary>
        /// ��¼��ֱ������ֵ
        /// </summary>
        private int iHScroll = 0;

        /// <summary>
        /// ÿ��ˮƽ�����������ķ��Ӳ�ֵ
        /// </summary>
        private int iMinutesPreScroll = 2;

        /// <summary>
        /// ÿ�δ�ֱ��������������
        /// </summary>
        private int iLinesPreScroll = 1;

        //����п��
        private int iNumColumnWidth = 22;

        //���ۺ���ƻ����ʱ��
        private DateTime dtOUTSTD = DateTime.MinValue;

        public DateTime DTOUTSTD
        {
            set { this.dtOUTSTD = value; }
            get { return this.dtOUTSTD; }
        }

        //���ۺ���ƻ����ʱ��
        private DateTime dtINSTD = DateTime.MinValue;

        public DateTime DTINSTD
        {
            set { this.dtINSTD = value; }
            get { return this.dtINSTD; }
        }

        private DateTime startPointMinutes = DateTime.MinValue; 


        //ÿ�����������
        /// <summary>
        /// ��DateTime��Ticksת��Ϊ����
        /// </summary>
        const int TRANSLATE_DELTA = 1000 * 1000 * 10 * 60;

        /// <summary>
        /// ��һ��ʱ��ת��Ϊ������
        /// </summary>
        /// <param name="dDate"></param>
        /// <returns></returns>
        public static int dateToMinutes(DateTime dDate)
        {
            return (int)(dDate.Ticks / fmGanttView.TRANSLATE_DELTA);
        }


        #endregion

        /// <summary>
        /// ��ӽڵ�
        /// </summary>
        /// <param name="sName">����</param>
        /// <param name="iCompletePrecent">��ɰٷֱȣ��Ǹ�0-100֮�������</param>
        /// <param name="dStartTime">��ʼʱ��</param>
        /// <param name="dEndTime">����ʱ��</param>
        public void add(string sName, int iCompletePrecent, DateTime dStartTime, DateTime dEndTime)
        {
            this.oNodeList.add(new Node(this, sName, iCompletePrecent, dStartTime, dEndTime));
        }

        /// <summary>
        /// ��ӽڵ�
        /// </summary>
        /// <param name="sName">����</param>
        /// <param name="iCompletePrecent">��ɰٷֱȣ��Ǹ�0-100֮�������</param>
        /// <param name="dStartTime">��ʼʱ��</param>
        /// <param name="dEndTime">����ʱ��</param>
        public void add(string sName, int iCompletePrecent, DateTime dStartTime, DateTime dEndTime, DateTime dSStartTime, DateTime dSEndTime)
        {
            this.oNodeList.add(new Node(this, sName, iCompletePrecent, dStartTime, dEndTime, dSStartTime, dSEndTime));
        }

        public void drawSTDTime(DateTime dtSTD, Graphics oGraphics, Color color)
        {
            if (dtSTD != DateTime.MinValue)
            {
                int iSTD = dateToMinutes(dtSTD);
                int iStartSTD = iSTD + (this.iMinutesPreScroll * this.iHScroll);

                if (this.oNodeList.TheFirstNode != null)
                {
                    iStartSTD = this.oNodeList.TheFirstNode.StartMinute + (this.iMinutesPreScroll * this.iHScroll);
                }

                int iCount = this.oNodeList.Count == 0 ? 1 : this.oNodeList.Count;

                Point pointUp = new Point(iSTD * this.iMinuteWidth - dateToMinutes(this.startPointMinutes) * this.iMinuteWidth, 3 * this.iHeaderLineHeight);
                Point pointDown = new Point(iSTD * this.iMinuteWidth - dateToMinutes(this.startPointMinutes) * this.iMinuteWidth, 3 * this.iHeaderLineHeight + iCount * (this.NodeHeight * 2 + this.iVerticalSpace));


                Pen stdPen = new Pen(color, 3);
                stdPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                oGraphics.DrawLine(stdPen, pointUp, pointDown);

                
                oGraphics.DrawString(dtSTD.ToString("HH:mm"),
                    new Font("����", 9.0f), new SolidBrush(color),
                    (iSTD * iMinuteWidth) - dateToMinutes(this.startPointMinutes) * this.iMinuteWidth,
                    3 * this.iHeaderLineHeight);
            }
        }

        /// <summary>
        /// ������нڵ�
        /// </summary>
        public void remove()
        {
            this.NodeList.clear();
        }

        /// <summary>
        /// ����
        /// </summary>
        public void draw()
        {
            this.searchNodeInView();

            try
            {
                this.oPictureBox.Image.Dispose();
                this.pictureBoxTreeView.Image.Dispose();
            }
            catch (Exception)
            {
            }

            if (this.oPictureBox.Width > 0 && this.oPictureBox.Height > 0)
            {
                this.NodeList.rebuild();

                this.oPictureBox.Image = new Bitmap(this.oPictureBox.Width, this.oPictureBox.Height);
                Graphics oGraphics = Graphics.FromImage(this.oPictureBox.Image);

                this.drawHeader(oGraphics);
                this.drawNodes(oGraphics);
                this.drawSTDTime(this.DTOUTSTD, oGraphics,Color.Red);
                this.drawSTDTime(this.DTINSTD, oGraphics, Color.Blue);
            }

            if (this.pictureBoxTreeView.Width > 0 && this.pictureBoxTreeView.Height > 0)
            {
                //this.NodeList.rebuild();

                this.pictureBoxTreeView.Image = new Bitmap(this.pictureBoxTreeView.Width, this.pictureBoxTreeView.Height);
                Graphics lGraphics = Graphics.FromImage(this.pictureBoxTreeView.Image);

                this.drawLHeader(lGraphics);
                this.drawLNodes(lGraphics);
            }
        }

        /// <summary>
        /// �������ͷ��
        /// </summary>
        /// <param name="lGraphics"></param>
        private void drawLHeader(Graphics lGraphics)
        {
            //�������ͷ��
            Rectangle leftRect = new Rectangle(
                this.pictureBoxTreeView.Location.X,
                this.pictureBoxTreeView.Location.Y,
                this.pictureBoxTreeView.Width,
                3 * this.iHeaderLineHeight
                );

            Pen headPen = new Pen(headerColor);
            lGraphics.DrawRectangle(headPen, leftRect);

            StringFormat oStringFormat = new StringFormat();
            oStringFormat.Alignment = StringAlignment.Center;
            oStringFormat.LineAlignment = StringAlignment.Center;
            Rectangle numRect = new Rectangle(
                this.pictureBoxTreeView.Location.X,
                this.pictureBoxTreeView.Location.Y,
                this.iNumColumnWidth,
                3 * this.iHeaderLineHeight
                );

            lGraphics.DrawRectangle(headPen, numRect);
            lGraphics.DrawString(
                "���",
                oTextFont,
                oTextBrush,
                numRect,
                oStringFormat
            );

            Rectangle nameRect = new Rectangle(
                this.iNumColumnWidth,
                this.pictureBoxTreeView.Location.Y,
                this.pictureBoxTreeView.Width - this.iNumColumnWidth,
                3 * this.iHeaderLineHeight
                );
            lGraphics.DrawString(
                "������Ŀ",
                oTextFont,
                oTextBrush,
                nameRect,
                oStringFormat
            );
        }

        /// <summary>
        /// ��ǰ��ͼ�������ɵķ�����
        /// </summary>
        public int MinutesSpan
        {
            get
            {
                if (this.oPictureBox.Image == null)
                {
                    return 0;
                }
                else
                {
                    return (int)(this.oPictureBox.Width / this.iMinuteWidth);

                }

            }
        }

        /// <summary>
        /// ������ͼ�������ɵ�����
        /// </summary>
        public int DrawLines
        {
            get
            {
                if (this.oPictureBox.Image == null)
                {
                    return 0;
                }
                else
                {
                    return (int)(this.oPictureBox.Height / (this.iNodeHeight + this.iVerticalSpace));

                }

            }
        }

        /// <summary>
        /// ͷ���߶ȣ�����ͷ�����������̬�仯
        /// </summary>
        private int iHeaderHeight;

        /// <summary>
        /// ����ͷ��
        /// </summary>
        private void drawHeader(Graphics oGraphics)
        {
            this.iHeaderHeight = this.iHeaderLineHeight;

            //ȷ����ʼʱ�䣬����û�нڵ㣬�ʹӵ�ǰʱ�俪ʼ����
            DateTime dStartTime = DateTime.Now;

            Node oFirstNode = this.NodeList.TheFirstNode;
            if (oFirstNode != null)
            {
                dStartTime = oFirstNode.StartTime;
            }
            else
            {
                if (this.dtINSTD != DateTime.MinValue && this.dtOUTSTD == DateTime.MinValue)
                {
                    dStartTime = this.DTINSTD;
                }

                if (this.dtOUTSTD != DateTime.MinValue && this.dtINSTD == DateTime.MinValue)
                {
                    dStartTime = this.dtOUTSTD;
                }
                if (this.dtINSTD != DateTime.MinValue && this.dtOUTSTD != DateTime.MinValue)
                {
                    dStartTime = dateToMinutes(this.dtINSTD) < dateToMinutes(this.dtOUTSTD) ? this.dtINSTD : this.dtOUTSTD;
                }
            }


            //Ҫ���Ƶķ�����
            int iDrawMinutesSpan = this.MinutesSpan;//��ͼ��ȳ���ÿ���Ӵ���Ŀ��

            dStartTime = dStartTime.AddMinutes(this.iMinutesPreScroll * this.iHScroll);

            this.startPointMinutes = dStartTime;

            StringFormat oStringFormat = new StringFormat();
            oStringFormat.Alignment = StringAlignment.Center;
            oStringFormat.LineAlignment = StringAlignment.Center;


            StringFormat mStringFormat = new StringFormat();
            mStringFormat.Alignment = StringAlignment.Near;
            mStringFormat.LineAlignment = StringAlignment.Center;

            //��С4���أ�����С��1�����쳣
            int iFontSize = Math.Max(4, (int)(this.iHeaderLineHeight / 2));


            //���ڻ��� 
            //���Ƶ�ǰ��Сʱ
            DateTime dCountDate = new DateTime(dStartTime.Ticks);
            int iHour = dStartTime.Hour + 1;
            
            int iMinutes = 60 * (24 - iHour) + (60 - dStartTime.Minute);

            Rectangle oDateRect = new Rectangle(
                0,
                0,
                iMinutes * this.iMinuteWidth,
                this.iHeaderLineHeight
            );

            //������
            oGraphics.FillRectangle(oBackgroundBrush, oDateRect);

            oGraphics.DrawRectangle(oLinePen, oDateRect);
            oGraphics.DrawString(
                dCountDate.ToString("yyyy-MM-dd"),
                oTextFont,
                oTextBrush,
                oDateRect,
                mStringFormat
            );

            //����Ҫ������һ������
            if (iMinutes < iDrawMinutesSpan)
            {
                oDateRect.Width = 24 * 60 * this.iMinuteWidth;
                do
                {
                    oDateRect.X = iMinutes * this.iMinuteWidth;
                    iMinutes += 60 * 24;
                    dCountDate = dCountDate.AddDays(1);
                    oGraphics.FillRectangle(oBackgroundBrush, oDateRect);
                    oGraphics.DrawRectangle(oLinePen, oDateRect);
                    oGraphics.DrawString(
                        dCountDate.ToString("yyyy-MM-dd"),
                        oTextFont,
                        oTextBrush,
                        oDateRect,
                        mStringFormat
                    );
                } while (iMinutes < iDrawMinutesSpan);
            }

            this.iHeaderHeight += this.iHeaderLineHeight;
            //���Ƶ�ǰ��Сʱ
            dCountDate = new DateTime(dStartTime.Ticks);
            iMinutes = 60 - dStartTime.Minute;
            Rectangle oHourRect = new Rectangle(
                0,
                this.iHeaderLineHeight,
                iMinutes * this.iMinuteWidth,
                this.iHeaderLineHeight
            );

            oGraphics.FillRectangle(new SolidBrush(titleColor), oHourRect);
            oGraphics.DrawRectangle(oLinePen, oHourRect);
            oGraphics.DrawString(
                dCountDate.Hour.ToString() + ":00",
                oTextFont,
                oTextBrush,
                oHourRect,
                oStringFormat
            );

            //����Ҫ������һ��Сʱ
            if (iMinutes < iDrawMinutesSpan)
            {
                oHourRect.Width = 60 * this.iMinuteWidth;
                do
                {
                    oHourRect.X = iMinutes * this.iMinuteWidth;
                    iMinutes += 60;
                    dCountDate = dCountDate.AddHours(1);
                    oGraphics.FillRectangle(new SolidBrush(titleColor), oHourRect);
                    oGraphics.DrawRectangle(oLinePen, oHourRect);
                    oGraphics.DrawString(
                        dCountDate.Hour.ToString() + ":00",
                        oTextFont,
                        oTextBrush,
                        oHourRect,
                        oStringFormat
                    );


                } while (iMinutes < iDrawMinutesSpan);
            }

            //ֻ�е����ӵĿ�ȴ���2�ǻ��Ƹ���
            if (this.iMinuteWidth > 2)
            {

                this.iHeaderHeight += this.iHeaderLineHeight;
                //���ӻ���
                dCountDate = new DateTime(dStartTime.Ticks);
                int iMinutsY = 2 * this.iHeaderLineHeight;
                Rectangle oMinutsRect = new Rectangle(
                    0,
                    iMinutsY,
                    this.iMinuteWidth,
                    this.iHeaderLineHeight   /***�����޸�***/
                );

                //�������еķ���
                for (int i = 0; i <= iDrawMinutesSpan; i++)
                {
                    //���Ʒ��Ӿ���
                    oGraphics.DrawRectangle(oLinePen, oMinutsRect);
                    oMinutsRect.X += this.iMinuteWidth;
                }
            }


            //���Ʒ����������ʽ         
            dCountDate = new DateTime(dStartTime.Ticks);
            iMinutes = 60 - dStartTime.Minute;
            Rectangle oPointHourRect = new Rectangle(
                0,
                3 * this.iHeaderLineHeight,
                iMinutes * this.iMinuteWidth,
                this.oPictureBox.Height - (3 * this.iHeaderLineHeight)
            );
            oGraphics.FillRectangle(new SolidBrush(twoHourColor), oPointHourRect);
            oGraphics.DrawRectangle(oLinePen, oPointHourRect);

            //����Ҫ������һ��Сʱ
            if (iMinutes < iDrawMinutesSpan)
            {
                oPointHourRect.Width = 60 * this.iMinuteWidth;
                int iColor = 0;
                do
                {

                    Color hourLineColor = (iColor % 2 == 0) ? oneHourColor : twoHourColor;
                    oPointHourRect.X = iMinutes * this.iMinuteWidth;
                    iMinutes += 60;
                    dCountDate = dCountDate.AddHours(1);
                    oGraphics.FillRectangle(new SolidBrush(hourLineColor), oPointHourRect);
                    oGraphics.DrawRectangle(oLinePen, oPointHourRect);
                    iColor++;
                } while (iMinutes < iDrawMinutesSpan);
            }

        }

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        private void drawLNodes(Graphics lGraphics)
        {
            StringFormat ostringStringFormat = new StringFormat();
            ostringStringFormat.Alignment = StringAlignment.Near;
            ostringStringFormat.LineAlignment = StringAlignment.Center;

            StringFormat onumStringFormat = new StringFormat();
            onumStringFormat.Alignment = StringAlignment.Center;
            onumStringFormat.LineAlignment = StringAlignment.Center;

            //���Ƶ�ǰ��ͼ��Χ�ڵĽڵ�
            //��ȡ��ֱ��Χ���ڵĽڵ�
            //int iCount = this.oNodeInView.Count;
            int iCount = this.lNodeList.Count;
            for (int iIndex = 0; iIndex < iCount; iIndex++)
            {
                Point pointL = new Point(0, 3 * this.iHeaderLineHeight +
                    (this.iVerticalSpace + this.iNodeHeight) * (iIndex + 1));

                Point pointR = new Point(this.pictureBoxTreeView.Width,
                    3 * this.iHeaderLineHeight +
                    (this.iVerticalSpace + this.iNodeHeight) * (iIndex + 1));

                lGraphics.DrawLine(new Pen(Color.LightSlateGray, 1), pointL, pointR);

                //�������
                Rectangle numColumnRect = new Rectangle(
                    0,
                    3 * this.iHeaderLineHeight + (this.iVerticalSpace + this.iNodeHeight) * iIndex,
                    this.iNumColumnWidth,
                    this.iVerticalSpace + this.iNodeHeight
                    );
                lGraphics.DrawRectangle(new Pen(Color.LightSlateGray, 1), numColumnRect);
                lGraphics.DrawString(
                    (this.iVScroll + iIndex + 1).ToString(),
                oTextFont,
                oTextBrush,
                numColumnRect,
                onumStringFormat
                );

                Rectangle nameColumnRect = new Rectangle(
                    this.iNumColumnWidth,
                    3 * this.iHeaderLineHeight + (this.iVerticalSpace + this.iNodeHeight) * iIndex,
                    this.pictureBoxTreeView.Width - this.iNumColumnWidth,
                    this.iVerticalSpace + this.iNodeHeight
                    );
                lGraphics.DrawRectangle(new Pen(Color.LightSlateGray, 1), numColumnRect);


                string strName = this.lNodeList[iIndex].Name.ToString();
                if (strName.Contains("|------"))
                {
                    tTextBrush = new SolidBrush(Color.Black);
                }
                else if (strName.Contains("|----"))
                {
                    tTextBrush = sTextBrush;
                }
                else
                {
                    tTextBrush = fTextBrush;
                }
                lGraphics.DrawString(
                    strName,
                    oTextFont,
                    tTextBrush,
                    nameColumnRect,
                    ostringStringFormat
                );
            }
        }

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        private void drawNodes(Graphics oGraphics)
        {


            for (int iIndex = 0; iIndex < this.lNodeList.Count; iIndex++)
            {
                Point pointL = new Point(0, 3 * this.iHeaderLineHeight + (this.iVerticalSpace + this.iNodeHeight) * (iIndex + 1));
                Point pointR = new Point(this.oPictureBox.Width, 3 * this.iHeaderLineHeight + (this.iVerticalSpace + this.iNodeHeight) * (iIndex + 1));
                oGraphics.DrawLine(new Pen(Color.LightSlateGray, 1), pointL, pointR);

            }


            //���Ƶ�ǰ��ͼ��Χ�ڵĽڵ�
            //��ȡ��ֱ��Χ���ڵĽڵ�
            int iCount = this.oNodeInView.Count;
            for (int iIndex = 0; iIndex < iCount; iIndex++)
            {
                oNodeInView[iIndex].draw(
                            oGraphics,
                            this.iViewPixelLeft,
                            this.iViewPixelTop,
                            this.iMinuteWidth,
                            this.iNodeHeight,
                            this.iVerticalSpace
                        );
            }
        }

        /// <summary>
        /// ��ͼ��ʾ�����еĽڵ�
        /// </summary>
        private List<Node> oNodeInView = new List<Node>();
        private int iViewPixelLeft = 0;
        private int iViewPixelTop = 0;

        //�����ͼ�ڵ�
        private List<Node> lNodeList = new List<Node>();

        //��ȡ��������ʾ�����еĽڵ�
        public void searchNodeInView()
        {
            oNodeInView.Clear();
            lNodeList.Clear();

            //�ػ�ڵ㣬ֻ���Ƶ�ǰ��ʾ��ͼ��Χ�ڵĽڵ�
            if (this.oNodeList.Count > 0)
            {
                Node oFirstNode = this.oNodeList.TheFirstNode;
                Node oLastestNode = this.oNodeList.TheLatestNode;

                if (oFirstNode != null)
                {
                    //��ֱ��Χ������
                    int iDrawStartLine = this.iVScroll * this.iLinesPreScroll;
                    //���Ʒ�Χ֮�ڵĽڵ�
                    int iAllLines = this.oNodeList.Count;
                    //ȷ��������Χ�����������ָ���쳣
                    if (iDrawStartLine < iAllLines && iDrawStartLine >= 0)
                    {
                        int iDrawLines = this.DrawLines + iDrawStartLine;
                        //ˮƽ��Χ������Ϊ��λ
                        int iStartMinute = oFirstNode.StartMinute;
                        int iEndMinute = oLastestNode.EndMinute;

                        int iDrawStartMinute = iStartMinute + (this.iMinutesPreScroll * this.iHScroll);
                        int iDrawMinutsSpan = this.MinutesSpan;


                        //��������ڵ�Ŀ�ʼʱ�������Ϊ0����
                        this.iViewPixelLeft = iDrawStartMinute * this.iMinuteWidth;
                        this.iViewPixelTop = iDrawStartLine * (this.iNodeHeight + this.iVerticalSpace);


                        //ͷ���߶�
                        this.iViewPixelTop -= this.iHeaderHeight + this.iVerticalSpace;


                        //���Ƶ�ǰ��ͼ��Χ�ڵĽڵ�
                        //��ȡ��ֱ��Χ���ڵĽڵ�
                        for (int iLine = iDrawStartLine; iLine < iDrawLines && iLine < iAllLines; iLine++)
                        {
                            Node oNode = this.oNodeList[iLine];

                            if (oNode != null && (oNode.inMinutesRange(iDrawStartMinute, iDrawMinutsSpan)))
                            {
                                oNodeInView.Add(oNode);
                            }
                        }

                        //����б�ڵ�������ʾ
                        for (int iLine = iDrawStartLine; iLine < this.oNodeList.Count; iLine++)
                        {
                            Node lNode = this.oNodeList[iLine];
                            this.lNodeList.Add(lNode);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// ��Ŀ�ڵ㼯��
        /// </summary>
        private NodeList oNodeList = new NodeList();

        public NodeList NodeList
        {
            get
            {
                return oNodeList;
            }
        }

        /// <summary>
        /// ����ˮƽ�������¼�
        /// </summary>
        /// <param name="iValue"></param>
        private void oHScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.EndScroll)
            {
                this.iHScroll += e.NewValue - e.OldValue;
                //ֵδ�����ı䣬��ʾ�ƶ����˾�ͷ
                if (e.NewValue == e.OldValue)
                {
                    //�ƶ�������Сֵ
                    this.iHScroll += (e.NewValue == ((HScrollBar)sender).Minimum) ? -1 : 1;
                }
                this.draw();
            }


        }

        /// <summary>
        /// ������ֱ�������¼�
        /// </summary>
        /// <param name="iValue"></param>  
        private void oVScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //ֵδ�����ı䣬��ʾ�ƶ����˾�ͷ
            if (e.Type == ScrollEventType.EndScroll)
            {
                this.iVScroll += e.NewValue - e.OldValue;
                if (e.NewValue == e.OldValue)
                {
                    //�ƶ�������Сֵ   
                    iVScroll += (e.NewValue == ((VScrollBar)sender).Minimum) ? -1 : 1;
                }

                //��ֱ������ֻ��Ҫ�������¹���  
                this.iVScroll = Math.Max(0, this.iVScroll);

                //������������ʾ
                //bug��Ч
                //this.oVScrollBar.Value = Math.Max(this.oVScrollBar.Minimum, Math.Min(this.oVScrollBar.Maximum, this.iVScroll));
                //this.oVScrollBar.Refresh();

                this.draw();
            }

        }

        /// <summary>
        /// �ػ���ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GanttView_Paint(object sender, PaintEventArgs e)
        {
            this.draw();
        }

        /// <summary>
        /// ToolTip��ʾ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            bool bFind = false;
            int iCount = this.oNodeInView.Count;
            for (int iIndex = 0; iIndex < iCount; iIndex++)
            {
                Node oNode = this.oNodeInView[iIndex];
                if (oNode.getRectangle(
                    this.iViewPixelLeft,
                    this.iViewPixelTop,
                    this.iMinuteWidth,
                    this.iNodeHeight,
                    this.iVerticalSpace
                    ).Contains(new Point(e.X, e.Y)) || oNode.getSRectangle(
                    this.iViewPixelLeft,
                    this.iViewPixelTop,
                    this.iMinuteWidth,
                    this.iNodeHeight,
                    this.iVerticalSpace
                    ).Contains(new Point(e.X, e.Y))
                )
                {
                    this.oToolTip.ToolTipTitle = oNode.ToolTipTitle.Replace("-","");
                    oNode.ToolTipConent = "�ƻ���ʼʱ��:" + oNode.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" +
                        "�ƻ�����ʱ��:" + oNode.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n\r\n" +
                        "ʵ�ʿ�ʼʱ��:" + oNode.SStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" +
                        "ʵ�ʽ���ʱ��:" + oNode.SEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                    this.oToolTip.Show(
                        oNode.ToolTipConent,
                        this.oPictureBox,
                        new Point(e.X, e.Y)
                    );
                    bFind = true;
                }
            }
            if (bFind == false)
            {
                this.oToolTip.Hide(this.oPictureBox);
            }
        }

        private void hScrollBarLeft_Scroll(object sender, ScrollEventArgs e)
        {
            //this.iHScroll += e.NewValue - e.OldValue;

            ////ֵδ�����ı䣬��ʾ�ƶ����˾�ͷ
            //if (e.NewValue == e.OldValue)
            //{
            //    //�ƶ�������Сֵ
            //    this.iHScroll += (e.NewValue == ((HScrollBar)sender).Minimum) ? -1 : 1;
            //}

            ////this.oHScrollBar.Value = Math.Max(this.oHScrollBar.Minimum, Math.Min(this.oHScrollBar.Maximum, this.iHScroll));
            ////this.oHScrollBar.Refresh();

            //this.draw();
        }

        private void oPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.oPictureBox.Focus();
        }

        private void oPictureBox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.iVScroll -= e.Delta / 120;
                this.iVScroll = Math.Max(0, this.iVScroll);
                this.draw();
            }
            else
            {
                this.iVScroll += (e.Delta / (-120));
                this.iVScroll = Math.Max(0, this.iVScroll);
                this.draw();
            }
        }

    }
}