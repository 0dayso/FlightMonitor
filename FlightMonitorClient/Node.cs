using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// ��Ŀ�ڵ�
    /// </summary>
    class Node
    {

        private Pen oLinePen = new Pen(Brushes.Gray, 1);
        private Font oTextFont = new Font(SystemFonts.DefaultFont.FontFamily, 10, FontStyle.Bold);//new Font("����", 9.0f);
        private Brush oTextBrush = new SolidBrush(Color.Black);
        private Brush oBackgroundBrush = new SolidBrush(Color.LightGray);
        private Brush allBrush = new SolidBrush(Color.DarkGreen);
        private Brush allSBrush = new SolidBrush(Color.MediumVioletRed);

        private int iUpTop = 10;

        #region ����
        /// <summary>
        /// ��Ӧ��ganttͼ
        /// </summary>
        private fmGanttView oGantt;

        /// <summary>
        /// �ڵ�����
        /// </summary>
        private string sName;

        /// <summary>
        /// ��ɰٷֱȣ�һ��0-100֮�������
        /// </summary>
        private int iCompletePrecent;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime dStartTime;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime dEndTime;

        /// <summary>
        /// �ƻ���ʼʱ��
        /// </summary>
        private DateTime sStartTime;

        /// <summary>
        /// ת��֮��ļƻ���ʼ������
        /// </summary>
        private int iSStartMinute;

        /// <summary>
        /// �ƻ�����ʱ��
        /// </summary>
        private DateTime sEndTime;

        /// <summary>
        /// ת��֮��ļƻ�����������
        /// </summary>
        private int iSEndMinute;

        /// <summary>
        /// ת��֮�����ʼ������
        /// </summary>
        private int iStartMinute;

        /// <summary>
        /// ת��֮��Ľ���������
        /// </summary>
        private int iEndMinute;

        /// <summary>
        /// ʱ���ȵķ�����
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
        /// ʱ���ȵķ�����
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
        /// �˽ڵ��ڽڵ��б��е�����
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
        /// �ڵ�����
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
        /// ��ʾ��Ϣ����
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
        /// ��Ϣ��ʾ����
        /// </summary>
        public string ToolTipConent
        {
            set
            {
                this.toolTipContent = value;
            }
            get
            {
                //return "��ɰٷֱȣ�" + this.iCompletePrecent + "%\n";
                return this.toolTipContent;
            }
        }

        #endregion

        /// <summary>
        /// ���¼���ʱ���ȷ����������������¼��㷶Χ
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

            //ʱ�俪ʼ�ͽ��������ı䣬���Ա�����¼��㷶Χ
            if (this.oGantt != null)
            {
                this.oGantt.NodeList.NeedRebuild = true;
            }
        }

        /// <summary>
        /// ����ʱ��һ�б仯�����¼��㷶Χ
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
                //������Χ
                iCompletePrecent = Math.Max(0, Math.Min(100, value));
            }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// </summary>
        /// <param name="oGantt"></param>
        /// <param name="sName">�ڵ�����</param>
        /// <param name="iCompletePrecent">�ڵ���ɽ���</param>
        /// <param name="dStartTime">�ڵ�Ŀ�ʼʱ��</param>
        /// <param name="dEndTime">�ڵ�Ľ���ʱ��</param>
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
        ///// ʱ���ȵķ�����
        ///// </summary>
        ///// <returns>�˽ڵ��ʱ���ȵķ�����</returns>
        //public int spanMinutes()
        //{
        //    return (int)((this.dEndTime.Ticks - this.dStartTime.Ticks) / Node.TRANSLATE_DELTA);
        //}

        /// <summary>
        /// ��ǰ�ڵ��Ƿ����ƶ��ķ��ӷ�Χ֮��
        /// </summary>
        /// <param name="iStartMinuts">��ʼ������</param>
        /// <param name="iMinutsSpan">���ӿ��</param>
        /// <returns></returns>
        public bool inMinutesRange(int iStartMinutes, int iMinutsSpan)
        {
            return (this.iStartMinute <= iStartMinutes + iMinutsSpan) || (this.iEndMinute < iStartMinutes);
        }

        /// <summary>
        /// ��ȡ�ƻ��ڵ�����ͼ�����еľ���
        /// </summary>
        /// <param name="oGraphics"></param>
        /// <param name="iLine">�˽ڵ����ڵ�����</param>
        /// <param name="iPixelLeft">��ǰ��ͼ��x�������������ڵ������ֵ</param>
        /// <param name="iPixelTop">��ǰ��ͼ��y�������������ڵ������ֵ</param>
        /// <param name="iMinutesWidth">ÿ���ӵĿ������ֵ</param>
        /// <param name="iNodeHeight">�ڵ�ĸ߶�����ֵ</param>
        /// <param name="iLineSpace">�ڵ�֮����ֱ�����ϵļ������ֵ</param>
        public Rectangle getRectangle(
            int iPixelLeft,
            int iPixelTop,
            int iMinutesWidth,
            int iNodeHeight,
            int iLineSpace
        )
        {
            //�ڵ����������//x��y����Сֵ����
            return new Rectangle(
                (this.iStartMinute * iMinutesWidth) - iPixelLeft,
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop,
                this.MinutesSpan * iMinutesWidth,
                iNodeHeight
            );
        }

        /// <summary>
        /// ��ȡʵ�ʽڵ�����ͼ�����еľ���
        /// </summary>
        /// <param name="oGraphics"></param>
        /// <param name="iLine">�˽ڵ����ڵ�����</param>
        /// <param name="iPixelLeft">��ǰ��ͼ��x�������������ڵ������ֵ</param>
        /// <param name="iPixelTop">��ǰ��ͼ��y�������������ڵ������ֵ</param>
        /// <param name="iMinutesWidth">ÿ���ӵĿ������ֵ</param>
        /// <param name="iNodeHeight">�ڵ�ĸ߶�����ֵ</param>
        /// <param name="iLineSpace">�ڵ�֮����ֱ�����ϵļ������ֵ</param>
        public Rectangle getSRectangle(
            int iPixelLeft,
            int iPixelTop,
            int iMinutesWidth,
            int iNodeHeight,
            int iLineSpace
        )
        {
            //�ڵ����������//x��y����Сֵ����
            return new Rectangle(
                (this.iSStartMinute * iMinutesWidth) - iPixelLeft,
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop,
                this.iSMinutesSpan * iMinutesWidth,
                iNodeHeight
            );
        }

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        /// <param name="oGraphics"></param>
        /// <param name="iLine">�˽ڵ����ڵ�����</param>
        /// <param name="iPixelLeft">��ǰ��ͼ��x�������������ڵ������ֵ</param>
        /// <param name="iPixelTop">��ǰ��ͼ��y�������������ڵ������ֵ</param>
        /// <param name="iMinutesWidth">ÿ���ӵĿ������ֵ</param>
        /// <param name="iNodeHeight">�ڵ�ĸ߶�����ֵ</param>
        /// <param name="iLineSpace">�ڵ�֮����ֱ�����ϵļ������ֵ</param>
        public void draw(
            Graphics oGraphics,
            int iPixelLeft,
            int iPixelTop,
            int iMinutesWidth,
            int iNodeHeight,
            int iLineSpace
        )
        {


            //�ڵ����������
            Rectangle oPaintRect = new Rectangle(
                (this.iStartMinute * iMinutesWidth) - iPixelLeft,
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop,
                this.MinutesSpan * iMinutesWidth,
                iNodeHeight
            );

            oGraphics.FillRectangle(allBrush, oPaintRect);


            if (this.sStartTime != DateTime.MinValue)
            {
                //ʵ��ʱ��Ľڵ�ͼ
                Rectangle sPaintRect = new Rectangle(
                    (this.iSStartMinute * iMinutesWidth) - iPixelLeft,
                    (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop,
                    this.iSMinutesSpan * iMinutesWidth,
                    iNodeHeight
                );

                oGraphics.FillRectangle(allSBrush, sPaintRect);

                //ʵ�����ʱ������
                oGraphics.DrawString(this.sStartTime.ToString("HH:mm"),
                    new Font("����", 9.0f), allSBrush,
                    (this.iSStartMinute * iMinutesWidth) - iPixelLeft - (6 * iMinutesWidth),
                    (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop);
                oGraphics.DrawString(this.sEndTime.ToString("HH:mm"),
                    new Font("����", 9.0f), allSBrush,
                    (this.iSStartMinute * iMinutesWidth) - iPixelLeft + (this.iSMinutesSpan * iMinutesWidth),
                    (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop);
            }

            //�ƻ����ʱ������
            oGraphics.DrawString(StartTime.ToString("HH:mm"),
                new Font("����", 9.0f), new SolidBrush(Color.Green),
                (this.iStartMinute * iMinutesWidth) - iPixelLeft - (6 * iMinutesWidth),
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop);
            oGraphics.DrawString(this.EndTime.ToString("HH:mm"),
                new Font("����", 9.0f), new SolidBrush(Color.Green),
                (this.iStartMinute * iMinutesWidth) - iPixelLeft + (iMinutesSpan * iMinutesWidth),
                (this.iIndex * (iNodeHeight + iLineSpace)) - iPixelTop - iUpTop);

        }
    }
}
