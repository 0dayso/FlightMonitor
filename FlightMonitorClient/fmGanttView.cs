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
        private Font oTextFont = new Font("宋体", 9.0f, FontStyle.Bold);// new Font(SystemFonts.DefaultFont.FontFamily, 10, FontStyle.Bold);//
        private Brush oTextBrush = new SolidBrush(Color.Black);
        private Brush oBackgroundBrush = new SolidBrush(Color.LightGray);

        private Brush fTextBrush = new SolidBrush(Color.Red); //一级颜色显示

        private Brush sTextBrush = new SolidBrush(Color.IndianRed); //二级颜色显示

        private Brush tTextBrush = new SolidBrush(Color.Black); //三级显示

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
        /// 构造函数
        /// </summary>
        public fmGanttView()
        {
            InitializeComponent();
        }


        #region 全局变量值
        /// <summary>
        /// 头部行高
        /// </summary>
        private int iHeaderLineHeight = 20;

        /// <summary>
        /// 头部行高
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
        /// 每一分钟的宽度
        /// </summary>
        private int iMinuteWidth = 10;

        /// <summary>
        /// 每一分钟的宽度
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
        /// 节点的高度
        /// </summary>
        private int iNodeHeight = 50;

        /// <summary>
        /// 节点的高度
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
        /// 节点的垂直为值
        /// </summary>
        private int iVerticalSpace = 15;

        /// <summary>
        /// 节点的垂直为值
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
        /// 记录水平滚动的值
        /// </summary>
        private int iVScroll = 0;

        /// <summary>
        /// 记录垂直滚动的值
        /// </summary>
        private int iHScroll = 0;

        /// <summary>
        /// 每次水平滚动所长生的分钟差值
        /// </summary>
        private int iMinutesPreScroll = 2;

        /// <summary>
        /// 每次垂直滚动滚动的行数
        /// </summary>
        private int iLinesPreScroll = 1;

        //序号列宽度
        private int iNumColumnWidth = 22;

        //出港航班计划起飞时间
        private DateTime dtOUTSTD = DateTime.MinValue;

        public DateTime DTOUTSTD
        {
            set { this.dtOUTSTD = value; }
            get { return this.dtOUTSTD; }
        }

        //出港航班计划起飞时间
        private DateTime dtINSTD = DateTime.MinValue;

        public DateTime DTINSTD
        {
            set { this.dtINSTD = value; }
            get { return this.dtINSTD; }
        }

        private DateTime startPointMinutes = DateTime.MinValue; 


        //每格代表两分钟
        /// <summary>
        /// 将DateTime的Ticks转换为分钟
        /// </summary>
        const int TRANSLATE_DELTA = 1000 * 1000 * 10 * 60;

        /// <summary>
        /// 将一个时间转换为分钟数
        /// </summary>
        /// <param name="dDate"></param>
        /// <returns></returns>
        public static int dateToMinutes(DateTime dDate)
        {
            return (int)(dDate.Ticks / fmGanttView.TRANSLATE_DELTA);
        }


        #endregion

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="sName">名称</param>
        /// <param name="iCompletePrecent">完成百分比，是个0-100之间的整数</param>
        /// <param name="dStartTime">开始时间</param>
        /// <param name="dEndTime">结束时间</param>
        public void add(string sName, int iCompletePrecent, DateTime dStartTime, DateTime dEndTime)
        {
            this.oNodeList.add(new Node(this, sName, iCompletePrecent, dStartTime, dEndTime));
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="sName">名称</param>
        /// <param name="iCompletePrecent">完成百分比，是个0-100之间的整数</param>
        /// <param name="dStartTime">开始时间</param>
        /// <param name="dEndTime">结束时间</param>
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
                    new Font("宋体", 9.0f), new SolidBrush(color),
                    (iSTD * iMinuteWidth) - dateToMinutes(this.startPointMinutes) * this.iMinuteWidth,
                    3 * this.iHeaderLineHeight);
            }
        }

        /// <summary>
        /// 清除所有节点
        /// </summary>
        public void remove()
        {
            this.NodeList.clear();
        }

        /// <summary>
        /// 绘制
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
        /// 绘制左边头部
        /// </summary>
        /// <param name="lGraphics"></param>
        private void drawLHeader(Graphics lGraphics)
        {
            //绘制左边头部
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
                "序号",
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
                "保障项目",
                oTextFont,
                oTextBrush,
                nameRect,
                oStringFormat
            );
        }

        /// <summary>
        /// 当前视图所能容纳的分钟数
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
        /// 当期视图所能容纳的行数
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
        /// 头部高度，根据头部绘制情况动态变化
        /// </summary>
        private int iHeaderHeight;

        /// <summary>
        /// 绘制头部
        /// </summary>
        private void drawHeader(Graphics oGraphics)
        {
            this.iHeaderHeight = this.iHeaderLineHeight;

            //确定开始时间，假如没有节点，就从当前时间开始绘制
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


            //要绘制的分钟数
            int iDrawMinutesSpan = this.MinutesSpan;//视图宽度除以每分钟代表的宽度

            dStartTime = dStartTime.AddMinutes(this.iMinutesPreScroll * this.iHScroll);

            this.startPointMinutes = dStartTime;

            StringFormat oStringFormat = new StringFormat();
            oStringFormat.Alignment = StringAlignment.Center;
            oStringFormat.LineAlignment = StringAlignment.Center;


            StringFormat mStringFormat = new StringFormat();
            mStringFormat.Alignment = StringAlignment.Near;
            mStringFormat.LineAlignment = StringAlignment.Center;

            //最小4像素，避免小于1产生异常
            int iFontSize = Math.Max(4, (int)(this.iHeaderLineHeight / 2));


            //日期绘制 
            //绘制当前的小时
            DateTime dCountDate = new DateTime(dStartTime.Ticks);
            int iHour = dStartTime.Hour + 1;
            
            int iMinutes = 60 * (24 - iHour) + (60 - dStartTime.Minute);

            Rectangle oDateRect = new Rectangle(
                0,
                0,
                iMinutes * this.iMinuteWidth,
                this.iHeaderLineHeight
            );

            //画背景
            oGraphics.FillRectangle(oBackgroundBrush, oDateRect);

            oGraphics.DrawRectangle(oLinePen, oDateRect);
            oGraphics.DrawString(
                dCountDate.ToString("yyyy-MM-dd"),
                oTextFont,
                oTextBrush,
                oDateRect,
                mStringFormat
            );

            //还需要绘制下一个日期
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
            //绘制当前的小时
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

            //还需要绘制下一个小时
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

            //只有当分钟的宽度大于2是绘制格子
            if (this.iMinuteWidth > 2)
            {

                this.iHeaderHeight += this.iHeaderLineHeight;
                //分钟绘制
                dCountDate = new DateTime(dStartTime.Ticks);
                int iMinutsY = 2 * this.iHeaderLineHeight;
                Rectangle oMinutsRect = new Rectangle(
                    0,
                    iMinutsY,
                    this.iMinuteWidth,
                    this.iHeaderLineHeight   /***界面修改***/
                );

                //绘制所有的分钟
                for (int i = 0; i <= iDrawMinutesSpan; i++)
                {
                    //绘制分钟矩形
                    oGraphics.DrawRectangle(oLinePen, oMinutsRect);
                    oMinutsRect.X += this.iMinuteWidth;
                }
            }


            //绘制分钟下面的样式         
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

            //还需要绘制下一个小时
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
        /// 绘制节点
        /// </summary>
        private void drawLNodes(Graphics lGraphics)
        {
            StringFormat ostringStringFormat = new StringFormat();
            ostringStringFormat.Alignment = StringAlignment.Near;
            ostringStringFormat.LineAlignment = StringAlignment.Center;

            StringFormat onumStringFormat = new StringFormat();
            onumStringFormat.Alignment = StringAlignment.Center;
            onumStringFormat.LineAlignment = StringAlignment.Center;

            //绘制当前视图范围内的节点
            //获取竖直范围的内的节点
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

                //绘制序号
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
        /// 绘制节点
        /// </summary>
        private void drawNodes(Graphics oGraphics)
        {


            for (int iIndex = 0; iIndex < this.lNodeList.Count; iIndex++)
            {
                Point pointL = new Point(0, 3 * this.iHeaderLineHeight + (this.iVerticalSpace + this.iNodeHeight) * (iIndex + 1));
                Point pointR = new Point(this.oPictureBox.Width, 3 * this.iHeaderLineHeight + (this.iVerticalSpace + this.iNodeHeight) * (iIndex + 1));
                oGraphics.DrawLine(new Pen(Color.LightSlateGray, 1), pointL, pointR);

            }


            //绘制当前视图范围内的节点
            //获取竖直范围的内的节点
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
        /// 视图显示区域中的节点
        /// </summary>
        private List<Node> oNodeInView = new List<Node>();
        private int iViewPixelLeft = 0;
        private int iViewPixelTop = 0;

        //左边视图节点
        private List<Node> lNodeList = new List<Node>();

        //获取所有在显示区域中的节点
        public void searchNodeInView()
        {
            oNodeInView.Clear();
            lNodeList.Clear();

            //重绘节点，只绘制当前显示视图范围内的节点
            if (this.oNodeList.Count > 0)
            {
                Node oFirstNode = this.oNodeList.TheFirstNode;
                Node oLastestNode = this.oNodeList.TheLatestNode;

                if (oFirstNode != null)
                {
                    //竖直范围，行数
                    int iDrawStartLine = this.iVScroll * this.iLinesPreScroll;
                    //绘制范围之内的节点
                    int iAllLines = this.oNodeList.Count;
                    //确定索引范围，以免产生空指针异常
                    if (iDrawStartLine < iAllLines && iDrawStartLine >= 0)
                    {
                        int iDrawLines = this.DrawLines + iDrawStartLine;
                        //水平范围，分钟为单位
                        int iStartMinute = oFirstNode.StartMinute;
                        int iEndMinute = oLastestNode.EndMinute;

                        int iDrawStartMinute = iStartMinute + (this.iMinutesPreScroll * this.iHScroll);
                        int iDrawMinutsSpan = this.MinutesSpan;


                        //按照最左节点的开始时间的坐标为0计算
                        this.iViewPixelLeft = iDrawStartMinute * this.iMinuteWidth;
                        this.iViewPixelTop = iDrawStartLine * (this.iNodeHeight + this.iVerticalSpace);


                        //头部高度
                        this.iViewPixelTop -= this.iHeaderHeight + this.iVerticalSpace;


                        //绘制当前视图范围内的节点
                        //获取竖直范围的内的节点
                        for (int iLine = iDrawStartLine; iLine < iDrawLines && iLine < iAllLines; iLine++)
                        {
                            Node oNode = this.oNodeList[iLine];

                            if (oNode != null && (oNode.inMinutesRange(iDrawStartMinute, iDrawMinutsSpan)))
                            {
                                oNodeInView.Add(oNode);
                            }
                        }

                        //左边列表节点名称显示
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
        /// 项目节点集合
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
        /// 处理水平滚动条事件
        /// </summary>
        /// <param name="iValue"></param>
        private void oHScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.EndScroll)
            {
                this.iHScroll += e.NewValue - e.OldValue;
                //值未发生改变，表示移动到了尽头
                if (e.NewValue == e.OldValue)
                {
                    //移动到了最小值
                    this.iHScroll += (e.NewValue == ((HScrollBar)sender).Minimum) ? -1 : 1;
                }
                this.draw();
            }


        }

        /// <summary>
        /// 处理竖直滚动条事件
        /// </summary>
        /// <param name="iValue"></param>  
        private void oVScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //值未发生改变，表示移动到了尽头
            if (e.Type == ScrollEventType.EndScroll)
            {
                this.iVScroll += e.NewValue - e.OldValue;
                if (e.NewValue == e.OldValue)
                {
                    //移动到了最小值   
                    iVScroll += (e.NewValue == ((VScrollBar)sender).Minimum) ? -1 : 1;
                }

                //垂直滚动条只需要处理向下滚动  
                this.iVScroll = Math.Max(0, this.iVScroll);

                //修正滚动条显示
                //bug无效
                //this.oVScrollBar.Value = Math.Max(this.oVScrollBar.Minimum, Math.Min(this.oVScrollBar.Maximum, this.iVScroll));
                //this.oVScrollBar.Refresh();

                this.draw();
            }

        }

        /// <summary>
        /// 重绘视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GanttView_Paint(object sender, PaintEventArgs e)
        {
            this.draw();
        }

        /// <summary>
        /// ToolTip提示信息
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
                    oNode.ToolTipConent = "计划开始时间:" + oNode.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" +
                        "计划结束时间:" + oNode.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n\r\n" +
                        "实际开始时间:" + oNode.SStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" +
                        "实际结束时间:" + oNode.SEndTime.ToString("yyyy-MM-dd HH:mm:ss");
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

            ////值未发生改变，表示移动到了尽头
            //if (e.NewValue == e.OldValue)
            //{
            //    //移动到了最小值
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