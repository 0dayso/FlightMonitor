using System;
using System.Collections.Generic;
using System.Text;

namespace AirSoft.FlightMonitor.FlightMonitorBM
{
    /// <summary>
    /// 标准流程实体类
    /// </summary>
    /// Author : yanxian
    /// Time : 2013-11-13
    public class StandardBM
    {
        private int m_StandardNo;
        private string m_Company;
        private int m_IView;
        private string m_FlightType;
        private string m_EndRefencePoint;
        private string m_BeginRefencePoint;
        private string m_EndTimeSpace;
        private string m_ItemName;
        private string m_BeginTimeSpace;
        private int m_ItemNo;
        private string m_Actype;
        private string m_FourCode;
        private string m_ThreeCode;
        private string m_FlightIOType;
        private string m_BeginFactPoint;
        private string m_EndFactPoint;
        private string m_CNBeginRefencePoint;
        private string m_CNEndRefencePoint;
        private string m_CNBeginFactPoint;
        private string m_CNEndFactPoint;
        private string m_CNCityName;
        private string m_action;
        private char m_IOFlight;
        private string m_ParentId;
        private char m_IsLeaf;

        public void Clear()
        {
            this.m_action = "";
            this.m_Actype = "";
            this.m_BeginFactPoint = "";
            this.m_BeginRefencePoint = "";
            this.m_BeginTimeSpace = "";
            this.m_CNBeginFactPoint = "";
            this.m_CNBeginRefencePoint = "";
            this.m_CNCityName = "";
            this.m_CNEndFactPoint = "";
            this.m_CNEndRefencePoint = "";
            this.m_Company = "";
            this.m_EndFactPoint = "";
            this.m_EndRefencePoint = "";
            this.m_EndTimeSpace = "";
            this.m_FlightIOType = "";
            this.m_FlightType = "";
            this.m_FourCode = "";
            this.m_ItemName = "";
            this.m_ItemNo = 0;
            this.m_IView = 0;
            this.m_StandardNo = 0;
            this.m_ThreeCode = "";
            this.m_IOFlight = ' ';
            this.m_IsLeaf = ' ';
            this.m_ParentId = "";

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public StandardBM()
        {
        }

        public int StandardNo
        {
            set { this.m_StandardNo = value; }
            get { return this.m_StandardNo; }
        }

        public string ThreeCode
        {
            set { this.m_ThreeCode = value; }
            get { return this.m_ThreeCode; }
        }

        public string FourCode
        {
            set { this.m_FourCode = value; }
            get { return this.m_FourCode; }
        }

        public string Actype
        {
            set { this.m_Actype = value; }
            get { return this.m_Actype; }
        }

        public int ItemNo
        {
            set { this.m_ItemNo = value; }
            get { return this.m_ItemNo; }
        }

        public string BeginTimeSpace
        {
            set { this.m_BeginTimeSpace = value; }
            get { return this.m_BeginTimeSpace; }
        }

        public string EndTimeSpace
        {
            set { this.m_EndTimeSpace = value; }
            get { return this.m_EndTimeSpace; }
        }

        public string BeginRefencePoint
        {
            set { this.m_BeginRefencePoint = value; }
            get { return this.m_BeginRefencePoint; }
        }

        public string EndRefencePoint
        {
            set { this.m_EndRefencePoint = value; }
            get { return this.m_EndRefencePoint; }
        }

        public string FlightType
        {
            set { this.m_FlightType = value; }
            get { return this.m_FlightType; }
        }

        public int IView
        {
            set { this.m_IView = value; }
            get { return this.m_IView; }
        }

        public string Company
        {
            set { this.m_Company = value; }
            get { return this.m_Company; }
        }

        public string ItemName
        {
            set { this.m_ItemName = value; }
            get { return this.m_ItemName; }
        }

        public string FlightIOType
        {
            set { this.m_FlightIOType = value; }
            get { return this.m_FlightIOType; }
        }

        public string BeginFactPoint
        {
            set { this.m_BeginFactPoint = value; }
            get { return this.m_BeginFactPoint; }
        }

        public string EndFactPoint
        {
            set { this.m_EndFactPoint = value; }
            get { return this.m_EndFactPoint; }
        }

        public string CNCityName
        {
            set { this.m_CNCityName = value; }
            get { return this.m_CNCityName; }
        }


        public string CNBeginRefencePoint
        {
            set { this.m_CNBeginRefencePoint = value; }
            get { return this.m_CNBeginRefencePoint; }
        }

        public string CNEndRefencePoint
        {
            set { this.m_CNEndRefencePoint = value; }
            get { return this.m_CNEndRefencePoint; }
        }

        public string CNBeginFactPoint
        {
            set { this.m_CNBeginFactPoint = value; }
            get { return this.m_CNBeginFactPoint; }
        }

        public string CNEndFactPoint
        {
            set { this.m_CNEndFactPoint = value; }
            get { return this.m_CNEndFactPoint; }
        }

        //操作动作的判断:主要是查找功能Iview操作
        public string Action
        {
            set { this.m_action = value; }
            get { return this.m_action; }
        }

        public char IOFlight
        {
            set { this.m_IOFlight = value; }
            get { return this.m_IOFlight; }
        }

        public string ParentId
        {
            set { this.m_ParentId = value; }
            get { return this.m_ParentId; }
        }

        public char IsLeaf
        {
            set { this.m_IsLeaf = value; }
            get { return this.m_IsLeaf; }
        }
    }
}
