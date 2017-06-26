using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;
using System.Drawing;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    /// <summary>
    /// ��װSpread Grid ����
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-28
    /// �� �� �ˣ�����
    /// �޸����ڣ�2008-06-18
    /// ��    ����
    public class SpreadGrid
    {
        private AccountBM m_accountBM;

        /// <summary>
        /// ���캯��
        /// </summary>
        public SpreadGrid(AccountBM accountBM)
        {
            this.m_accountBM = accountBM;
        }

        #region ������ͼ
        /// <summary>
        /// ������ͼ
        /// </summary>
        /// <param name="shGrid"></param>
        /// <param name="dataItems"></param>
        /// <param name="iChild"></param>
        public void SetView(FarPoint.Win.Spread.SheetView shGrid, DataTable dataItems, int iChild)
        {
            //�������
            DataTable dtDataItems = dataItems;
            shGrid.ColumnCount = 0;
            shGrid.ColumnCount = dtDataItems.Rows.Count;

            //ѡ�������
            int iIndex = 0;
            //ĳһ�������
            int iColumnGroupCount = 0;
            //ĳһ�������
            string strGroupTitle = "";
            //�����û�ѡ�����������ͼ
            foreach (DataRow rowItem in dtDataItems.Rows)
            {
                DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem, DataItemPurviewBM.DataType.PURVIEW);

                //���ö��뷽ʽ
                shGrid.Columns[iIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                shGrid.Columns[iIndex].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                shGrid.Columns[iIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                #region ���б�ͷ
                //���û��˫��ͷ
                if (dataItemPurviewBM.DataItemName.IndexOf("|") < 0)
                {
                    iColumnGroupCount = 0;
                    strGroupTitle = "";
                    shGrid.Models.ColumnHeaderSpan.Add(0, iIndex, 2, 1);

                    //��ͷ����
                    shGrid.ColumnHeader.Cells[0, iIndex].Text = dataItemPurviewBM.DataItemName;

                    //����Դ
                    if (iChild == 1)
                    {
                        if (dataItemPurviewBM.DataItemID.IndexOf("In") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(2);
                        }
                        else if (dataItemPurviewBM.DataItemID.IndexOf("Out") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(3);
                        }
                        else if (dataItemPurviewBM.DataItemID.IndexOf("Dsp") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(3);
                        }
                        else if (dataItemPurviewBM.DataItemID.IndexOf("Mon") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(3);
                        }
                    }
                    else
                    {
                        shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID;
                    }
                    
                    //���������е���ʾ��ʽ
                    ArrayList alSpecialColumn = new ArrayList();
                    alSpecialColumn.Add("IncnvcLONG_REG");
                    alSpecialColumn.Add("OutcnvcLONG_REG");
                    alSpecialColumn.Add("DspcnvcLONG_REG");
                    alSpecialColumn.Add("MoncnvcLONG_REG");
                    //���÷ɻ��ŵ�������
                    if (alSpecialColumn.Contains(dataItemPurviewBM.DataItemID))
                    {
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Blue;
                        shGrid.Columns[iIndex].ForeColor = Color.Blue;                        
                        shGrid.Columns[iIndex].Font = new Font("����", 9, FontStyle.Bold);
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("����", 9, FontStyle.Bold);                        
                    }
                    alSpecialColumn.Clear();

                    alSpecialColumn.Add("IncnvcFlightNo");
                    alSpecialColumn.Add("OutcnvcFlightNo");
                    alSpecialColumn.Add("DspcnvcFlightNo");
                    alSpecialColumn.Add("MoncnvcFlightNo");
                    //���ý��ۺ���ŵ�������
                    if (alSpecialColumn.Contains(dataItemPurviewBM.DataItemID))   
                    {
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Red;
                        shGrid.Columns[iIndex].ForeColor = Color.Red;
                        shGrid.Columns[iIndex].Font = new Font("����", 9, FontStyle.Bold);
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("����", 9, FontStyle.Bold);
                    }
                    alSpecialColumn.Clear();

                    //����ͣ��λ��������
                    if (dataItemPurviewBM.DataItemID == "IncnvcInGATE" || dataItemPurviewBM.DataItemID == "OutcnvcOutGate")      
                    {
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Blue;
                        shGrid.Columns[iIndex].ForeColor = Color.Blue;                        
                        shGrid.Columns[iIndex].Font = new Font("����", 9, FontStyle.Bold);
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("����", 9, FontStyle.Bold);                        
                    }
                    if (dataItemPurviewBM.DataItemID == "IncnvcInRemark" || dataItemPurviewBM.DataItemID == "OutcnvcOutRemark" || dataItemPurviewBM.DataItemID == "OutcniFocusTag")
                    {
                        shGrid.Columns[iIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        shGrid.Columns[iIndex].CellType = new FarPoint.Win.Spread.CellType.RichTextCellType();
                    }

                    shGrid.Columns[iIndex].Width = dataItemPurviewBM.ColumnWidth;
                    iIndex++;
                }
                #endregion

                #region ˫�б�ͷ
                //�����˫��ͷ
                else   
                {
                    string[] strTitle = dataItemPurviewBM.DataItemName.Split('|');
                    if (strTitle[0] != strGroupTitle)
                    {
                        //�����������
                        iColumnGroupCount = 1;
                        //�����
                        strGroupTitle = strTitle[0];   						
                        shGrid.Models.ColumnHeaderSpan.Add(0, iIndex, 1, 1);
                        //���������
                        shGrid.ColumnHeader.Cells[0, iIndex].Text = strGroupTitle;   
                        
                        //�������������ɫ
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Black;
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("Veranda", 8, FontStyle.Bold);
                    }
                    else
                    {
                        iColumnGroupCount += 1;
                        shGrid.Models.ColumnHeaderSpan.Remove(0, iIndex - iColumnGroupCount + 1);
                        shGrid.Models.ColumnHeaderSpan.Add(0, iIndex - iColumnGroupCount + 1, 1, iColumnGroupCount);
                    }
                    //�б���
                    shGrid.ColumnHeader.Cells[1, iIndex].Text = strTitle[1];
                    //������Դ
                    if (iChild == 1)
                    {
                        if (dataItemPurviewBM.DataItemID.IndexOf("In") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(2);
                        }
                        else if (dataItemPurviewBM.DataItemID.IndexOf("Out") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(3);
                        }
                        else if(dataItemPurviewBM.DataItemID.IndexOf("Dsp") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(3);
                        }
                        else if (dataItemPurviewBM.DataItemID.IndexOf("Mon") == 0)
                        {
                            shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID.Substring(3);
                        }
                    }
                    else
                    {
                        shGrid.Columns[iIndex].DataField = dataItemPurviewBM.DataItemID;
                    }
                    
                    //�п�
                    shGrid.Columns[iIndex].Width = dataItemPurviewBM.ColumnWidth;
                    //�е��������ɫ
                    shGrid.Columns[iIndex].ForeColor = Color.Black;                    
                    iIndex++;
                }
                #endregion
            }
        }
        #endregion
    }
}
