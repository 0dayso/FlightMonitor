using System;
using System.Collections.Generic;
using System.Text;


namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// �ڵ㼯��
    /// </summary>
    class NodeList
    {
        private IList<Node> aNodes = new List<Node>();

        /// <summary>
        /// ��ʼʱ������Ľڵ�
        /// </summary>
        private Node oTheFirstNode;

        /// <summary>
        /// ����ʱ������Ľڵ�
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
        /// �ڵ������Ŀ
        /// </summary>
        public int Count
        {
            get
            {
                return aNodes.Count;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ���¹���Χ��Ϣ
        /// </summary>
        private bool bNeedRebuild = false;

        /// <summary>
        /// �Ƿ���Ҫ���¹���Χ��Ϣ
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
        /// ��ӽڵ�
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
        /// ɾ���������Ľڵ�
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
        /// ������нڵ�
        /// </summary>
        public void clear()
        {
            this.aNodes.Clear();
            this.bNeedRebuild = true;
        }

        /// <summary>
        /// �ؽ���Χ��Ϣ
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

