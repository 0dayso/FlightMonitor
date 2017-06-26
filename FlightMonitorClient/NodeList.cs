using System;
using System.Collections.Generic;
using System.Text;


namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// 节点集合
    /// </summary>
    class NodeList
    {
        private IList<Node> aNodes = new List<Node>();

        /// <summary>
        /// 开始时间最早的节点
        /// </summary>
        private Node oTheFirstNode;

        /// <summary>
        /// 结束时间最晚的节点
        /// </summary>
        private Node oTheLatestNode;

        public Node TheFirstNode
        {
            get
            {
                return this.oTheFirstNode;
            }
        }

        public Node TheLatestNode
        {
            get
            {
                return this.oTheLatestNode;
            }
        }

        public Node this[int iIndex]
        {
            get
            {
                if (iIndex > this.aNodes.Count - 1 || iIndex < 0)
                {
                    return null;
                }
                return this.aNodes[iIndex];
            }
        }

        /// <summary>
        /// 节点的总数目
        /// </summary>
        public int Count
        {
            get
            {
                return aNodes.Count;
            }
        }

        /// <summary>
        /// 是否需要重新构范围信息
        /// </summary>
        private bool bNeedRebuild = false;

        /// <summary>
        /// 是否需要重新构范围信息
        /// </summary>
        public bool NeedRebuild
        {
            get
            {
                return this.bNeedRebuild;
            }
            set
            {
                this.bNeedRebuild = value;
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="oNode"></param>
        public void add(Node oNode)
        {
            if (oNode != null)
            {
                this.aNodes.Add(oNode);
                this.bNeedRebuild = true;
            }
        }

        /// <summary>
        /// 删除索引处的节点
        /// </summary>
        /// <param name="iIndex"></param>
        public void removeAt(int iIndex)
        {
            if (iIndex >= 0 && iIndex < this.aNodes.Count)
            {
                this.aNodes.RemoveAt(iIndex);
                this.bNeedRebuild = true;
            }
        }

        /// <summary>
        /// 清除所有节点
        /// </summary>
        public void clear()
        {
            this.aNodes.Clear();
            this.bNeedRebuild = true;
        }

        /// <summary>
        /// 重建范围信息
        /// </summary>
        public void rebuild()
        {
            if (this.bNeedRebuild == true)
            {
                this.oTheFirstNode = null;
                this.oTheLatestNode = null;
                int iCount = this.Count;
                for (int i = 0; i < iCount; i++)
                {
                    Node oNode = this.aNodes[i];
                    oNode.Index = i;
                    if (this.oTheFirstNode == null)
                    {
                        this.oTheFirstNode = oNode;
                    }
                    if (this.oTheLatestNode == null)
                    {
                        this.oTheLatestNode = oNode;
                    }

                    if (oNode.StartMinute < this.oTheFirstNode.StartMinute)
                    {
                        this.oTheFirstNode = oNode;
                    }

                    if (oNode.EndMinute > this.oTheLatestNode.EndMinute)
                    {
                        this.oTheLatestNode = oNode;
                    }
                }
            }

            this.bNeedRebuild = false;
        }
    }
}

