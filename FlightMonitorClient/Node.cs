using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// 项目节点
    /// </summary>
    class Node
    {

        private Pen oLinePen = new Pen(Brushes.Gray, 1);
        private Font oTextFont = new Font(SystemFonts.DefaultFont.FontFamily, 10, FontStyle.Bold);//new Font("宋体", 9.0f);
        private Brush oTextBrush = new SolidBrush(Color.Black);
        private Brush oBackgroundBrush = new SolidBrush(Color.LightGray);
        private Brush allBrush = new SolidBrush(Color.DarkGreen);
        private Brush allSBrush = new SolidBrush(Color.MediumVioletRed);

        private int iUpTop = 10;

        #region 属性
        /// <summary>
        /// 对应的gantt图
        /// </summary>
        private fmGanttView oGantt;

        /// <summary>
        /// 节点名称
        /// </summary>
        private string sName;

        /// <summary>
        /// 完成百分比，一个0-100之间的数字
        /// </summary>
        private int iCompletePrecent;

        /// <summary>
        /// 起始时间
        /// </summary>
        private DateTime dStartTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime dEndTime;

        /// <summary>
        /// 计划开始时间
        /// </summary>
        private DateTime sStartTime;

        /// <summary>
        /// 转化之后的计划起始分钟数
        /// </summary>
        private int iSStartMinute;

        /// <summary>
        /// 计划结束时间
        /// </summary>
        private DateTime sEndTime;

        /// <summary>
        /// 转化之后的计划结束分钟数
        /// </summary>
        private int iSEndMinute;

        /// <summary>
        /// 转换之后的起始分钟数
        /// </summary>
        private int iStartMinute;

        /// <summary>
        /// 转化之后的结束分钟数
        /// </summary>
        private int iEndMinute;

        /// <summary>
        /// 时间跨度的分钟数
        /// </summary>
        private int iMinutesSpan;

        public int SStartMinute
        {
            set { this.iSStartMinute = value; }
            get { return this.iSStartMinute; }
        }

        public int SEndMinute
        {
            set { this.iSEndMinute = value; }
            get { return this.iSEndMinute; }
        }

        private int iSMinutesSpan;

        public int SMinutesSpan
        {
            get
            {
                return this.iSMinutesSpan;
            }
        }

        /// <summary>
        /// 时间跨度的分钟数
        /// </summary>
        public int MinutesSpan
        {
            get
            {
                return iMinutesSpan;
            }
        }



        private int iIndex;
        /// <summary>
        /// 此节点在节点列表中的索引
        /// </summary>
        public int Index
        {
            get
            {
                return this.iIndex;
            }
            set
            {
                this.iIndex = value;
            }
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
            }
        }

        public int StartMinute
        {
            get { return iStartMinute; }
        }

        public int EndMinute
        {
            get { return iEndMinute; }
        }

        public DateTime StartTime
        {
            get
            {
                return dStartTime;
            }
            set
            {
                dStartTime = value;
                this.reCalcMinutesSpan();
            }
        }

        public DateTime SStartTime
        {
            get
            {
                return this.sStartTime;
            }
            set
            {
                this.sStartTime = value;
                this.reCalcMinutesSpan();
            }
        }

        public DateTime SEndTime
        {
            get
            {
                return this.sEndTime;
            }
            set
            {
                this.sEndTime = value;
                this.reCalcMinutesSpan();
            }
        }

        /// <summary>
        /// 提示信息标题
        /// </summary>
        public string ToolTipTitle
        {
            get
            {
                return this.Name;
            }
        }

        private string toolTipContent;

        /// <summary>
        /// 信息提示内容
        /// </summary>
        public string ToolTipConent
        {
            set
            {
                this.toolTipContent = value;
            }
            get
            {
                //return "完成百分比：" + this.iCompletePrecent + "%\n";
                return this.toolTipContent;
            }
        }

        #endregion

        /// <summary>
        /// 重新计算时间跨度分钟数，并设置重新计算范围
        /// </summary>
        private void reCalcMinutesSpan()
        {
            iStartMinute = fmGanttView.dateToMinutes(dStartTime);
            iEndMinute = fmGanttView.dateToMinutes(dEndTime);

            if (sStartTime != null && sEndTime != null)
            {
                iSStartMinute = fmGanttView.dateToMinutes(sStartTime);
                iSEndMinute = fmGanttView.dateToMinutes(sEndTime);
                iSMinutesSpan = iSEndMinute - iSStartMinute;
            }


            iMinutesSpan = iEndMinute - iStartMinute;

            //时间开始和结束发生改变，所以标记重新计算范围
            if (this.oGantt != null)
            {
                this.oGantt.NodeList.NeedRebuild = true;
            }
        }

        /// <summary>
        /// 结束时间一有变化得重新计算范围
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return this.dEndTime;
            }
            set
            {
                this.dEndTime = value;
                this.reCalcMinutesSpan();
            }
        }

        public int CompletePrecent
        {
            get
            {
                return iCompletePrecent;
            }
            set
            {
                //修正范围
                iCompletePrecent = Math.Max(0, Math.Min(100, value));
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// </summary>
        /// <param name="oGantt"></param>
        /// <param name="sName">节点名称</param>
        /// <param name="iCompletePrecent">节点完成进度</param>
        /// <param name="dStartTime">节点的开始时间</param>
        /// <param name="dEndTime">节点的结束时间</param>
        public Node(fmGanttView oGantt, string sName, int iCompletePrecent, DateTime dStartTime, DateTime dEndTime)
        {
            this.oGantt = oGantt;

            this.sName = sName;
            this.iCompletePrecent = iCompletePrecent;
            this.StartTime = dStartTime;
            this.EndTime = dEndTime;
            this.SStartTime = DateTime.MinValue;
            this.SEndTime = DateTime.MinValue;
        }

        public Node(fmGanttView oGantt, string sName, int iCompletePrecent, DateTime dStartTime, DateTime dEndTime, DateTime sDSTime, DateTime sDETime)
        {
            this.oGantt = oGantt;

            this.sName = sName;
            this.iCompletePrecent = iCompletePrecent;
            this.StartTime = dStartTime;
            this.EndTime = dEndTime;
            this.SStartTime = sDSTime;
            this.SEndTime = sDETime;
        }

        const int TRANSLATE_DELTA = 1000 * 1000 * 10 * 60;

        ///// <summary>
        ///// 时间跨度的分钟数
        ///// </summary>
        ///// <returns>此节点的时间跨度的分钟数</returns>
        //public int spanMinutes()
        //{
        //    return (int)((this.dEndTime.Ticks - this.dStartTime.Ticks) / Node.TRANSLATE_DELTA);
        //}

        /// <summary>
        /// 当前节点是否在制定的分钟范围之内
        /// </summary>
        /// <param name="iStartMinuts">起始分钟数</param>
        /// <param name="iMinutsSpan">分钟跨度</param>
        /// <returns></returns>
        public bool inMinutesRange(int iStartMinutes, int iMinutsSpan)
        {
            return (this.iStartMinute <= iStartMinutes + iMinutsSpan) || (this.iEndMinute < iStartMinutes);
        }

        /// <summary>
        /// 获取计划节点在视图区域中的矩形
        /// </summary>
        /// <param name="oGraphics"></param>
        /// <param name="iLine">此节点所在的行数</param>
        /// <param name="iPixelLeft">当前视图的x坐标相对于最早节点的像素值</param>
        /// <param name="iPixelTop">当前视图的y坐标相对于最早节点的像素值</param>
        /// <param name="iMinutesWidth">每分钟的宽度像素值</param>
        /// <param name="iNodeHeight">节点的高度像素值</param>
        /// <param name="iLineSpace">节点之间竖直方向上的间隔像素值</param>
        public Rectangle getRectangle(
            int iPixelLeft,
            int iPixelTop,
            int iMinutesWidth,
            int iNodeHeight,
            int iLineSpace
        )
        {
            //节点的整个矩形//x，y轴最小值计算
            return new Rectangle(
                (this.iStartMinute * iMinutesWidth) - iPixelLeft,
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop,
                this.MinutesSpan * iMinutesWidth,
                iNodeHeight
            );
        }

        /// <summary>
        /// 获取实际节点在视图区域中的矩形
        /// </summary>
        /// <param name="oGraphics"></param>
        /// <param name="iLine">此节点所在的行数</param>
        /// <param name="iPixelLeft">当前视图的x坐标相对于最早节点的像素值</param>
        /// <param name="iPixelTop">当前视图的y坐标相对于最早节点的像素值</param>
        /// <param name="iMinutesWidth">每分钟的宽度像素值</param>
        /// <param name="iNodeHeight">节点的高度像素值</param>
        /// <param name="iLineSpace">节点之间竖直方向上的间隔像素值</param>
        public Rectangle getSRectangle(
            int iPixelLeft,
            int iPixelTop,
            int iMinutesWidth,
            int iNodeHeight,
            int iLineSpace
        )
        {
            //节点的整个矩形//x，y轴最小值计算
            return new Rectangle(
                (this.iSStartMinute * iMinutesWidth) - iPixelLeft,
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop,
                this.iSMinutesSpan * iMinutesWidth,
                iNodeHeight
            );
        }

        /// <summary>
        /// 绘制节点
        /// </summary>
        /// <param name="oGraphics"></param>
        /// <param name="iLine">此节点所在的行数</param>
        /// <param name="iPixelLeft">当前视图的x坐标相对于最早节点的像素值</param>
        /// <param name="iPixelTop">当前视图的y坐标相对于最早节点的像素值</param>
        /// <param name="iMinutesWidth">每分钟的宽度像素值</param>
        /// <param name="iNodeHeight">节点的高度像素值</param>
        /// <param name="iLineSpace">节点之间竖直方向上的间隔像素值</param>
        public void draw(
            Graphics oGraphics,
            int iPixelLeft,
            int iPixelTop,
            int iMinutesWidth,
            int iNodeHeight,
            int iLineSpace
        )
        {


            //节点的整个矩形
            Rectangle oPaintRect = new Rectangle(
                (this.iStartMinute * iMinutesWidth) - iPixelLeft,
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop,
                this.MinutesSpan * iMinutesWidth,
                iNodeHeight
            );

            oGraphics.FillRectangle(allBrush, oPaintRect);


            if (this.sStartTime != DateTime.MinValue)
            {
                //实际时间的节点图
                Rectangle sPaintRect = new Rectangle(
                    (this.iSStartMinute * iMinutesWidth) - iPixelLeft,
                    (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop,
                    this.iSMinutesSpan * iMinutesWidth,
                    iNodeHeight
                );

                oGraphics.FillRectangle(allSBrush, sPaintRect);

                //实际完成时间的添加
                oGraphics.DrawString(this.sStartTime.ToString("HH:mm"),
                    new Font("宋体", 9.0f), allSBrush,
                    (this.iSStartMinute * iMinutesWidth) - iPixelLeft - (6 * iMinutesWidth),
                    (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop);
                oGraphics.DrawString(this.sEndTime.ToString("HH:mm"),
                    new Font("宋体", 9.0f), allSBrush,
                    (this.iSStartMinute * iMinutesWidth) - iPixelLeft + (this.iSMinutesSpan * iMinutesWidth),
                    (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop);
            }

            //计划完成时间的添加
            oGraphics.DrawString(StartTime.ToString("HH:mm"),
                new Font("宋体", 9.0f), new SolidBrush(Color.Green),
                (this.iStartMinute * iMinutesWidth) - iPixelLeft - (6 * iMinutesWidth),
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop);
            oGraphics.DrawString(this.EndTime.ToString("HH:mm"),
                new Font("宋体", 9.0f), new SolidBrush(Color.Green),
                (this.iStartMinute * iMinutesWidth) - iPixelLeft + (iMinutesSpan * iMinutesWidth),
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop);

        }
    }
}
