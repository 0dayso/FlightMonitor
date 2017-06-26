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
    /// 封装Spread Grid 的类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-28
    /// 修 改 人：张黎
    /// 修改日期：2008-06-18
    /// 版    本：
    public class SpreadGrid
    {
        private AccountBM m_accountBM;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpreadGrid(AccountBM accountBM)
        {
            this.m_accountBM = accountBM;
        }

        #region 设置视图
        /// <summary>
        /// 设置视图
        /// </summary>
        /// <param name="shGrid"></param>
        /// <param name="dataItems"></param>
        /// <param name="iChild"></param>
        public void SetView(FarPoint.Win.Spread.SheetView shGrid, DataTable dataItems, int iChild)
        {
            //数据项表
            DataTable dtDataItems = dataItems;
            shGrid.ColumnCount = 0;
            shGrid.ColumnCount = dtDataItems.Rows.Count;

            //选择项序号
            int iIndex = 0;
            //某一组的列数
            int iColumnGroupCount = 0;
            //某一组的组名
            string strGroupTitle = "";
            //根据用户选择的列设置视图
            foreach (DataRow rowItem in dtDataItems.Rows)
            {
                DataItemPurviewBM dataItemPurviewBM = new DataItemPurviewBM(rowItem, DataItemPurviewBM.DataType.PURVIEW);

                //设置对齐方式
                shGrid.Columns[iIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                shGrid.Columns[iIndex].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                shGrid.Columns[iIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                #region 单行表头
                //如果没有双表头
                if (dataItemPurviewBM.DataItemName.IndexOf("|") < 0)
                {
                    iColumnGroupCount = 0;
                    strGroupTitle = "";
                    shGrid.Models.ColumnHeaderSpan.Add(0, iIndex, 2, 1);

                    //表头名称
                    shGrid.ColumnHeader.Cells[0, iIndex].Text = dataItemPurviewBM.DataItemName;

                    //数据源
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
                    
                    //设置特殊列的显示方式
                    ArrayList alSpecialColumn = new ArrayList();
                    alSpecialColumn.Add("IncnvcLONG_REG");
                    alSpecialColumn.Add("OutcnvcLONG_REG");
                    alSpecialColumn.Add("DspcnvcLONG_REG");
                    alSpecialColumn.Add("MoncnvcLONG_REG");
                    //设置飞机号的列属性
                    if (alSpecialColumn.Contains(dataItemPurviewBM.DataItemID))
                    {
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Blue;
                        shGrid.Columns[iIndex].ForeColor = Color.Blue;                        
                        shGrid.Columns[iIndex].Font = new Font("宋体", 9, FontStyle.Bold);
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("宋体", 9, FontStyle.Bold);                        
                    }
                    alSpecialColumn.Clear();

                    alSpecialColumn.Add("IncnvcFlightNo");
                    alSpecialColumn.Add("OutcnvcFlightNo");
                    alSpecialColumn.Add("DspcnvcFlightNo");
                    alSpecialColumn.Add("MoncnvcFlightNo");
                    //设置进港航班号的列属性
                    if (alSpecialColumn.Contains(dataItemPurviewBM.DataItemID))   
                    {
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Red;
                        shGrid.Columns[iIndex].ForeColor = Color.Red;
                        shGrid.Columns[iIndex].Font = new Font("宋体", 9, FontStyle.Bold);
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("宋体", 9, FontStyle.Bold);
                    }
                    alSpecialColumn.Clear();

                    //设置停机位的列属性
                    if (dataItemPurviewBM.DataItemID == "IncnvcInGATE" || dataItemPurviewBM.DataItemID == "OutcnvcOutGate")      
                    {
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Blue;
                        shGrid.Columns[iIndex].ForeColor = Color.Blue;                        
                        shGrid.Columns[iIndex].Font = new Font("宋体", 9, FontStyle.Bold);
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("宋体", 9, FontStyle.Bold);                        
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

                #region 双行表头
                //如果分双表头
                else   
                {
                    string[] strTitle = dataItemPurviewBM.DataItemName.Split('|');
                    if (strTitle[0] != strGroupTitle)
                    {
                        //组包含的列数
                        iColumnGroupCount = 1;
                        //组标题
                        strGroupTitle = strTitle[0];   						
                        shGrid.Models.ColumnHeaderSpan.Add(0, iIndex, 1, 1);
                        //设置组标题
                        shGrid.ColumnHeader.Cells[0, iIndex].Text = strGroupTitle;   
                        
                        //组标题的字体和颜色
                        shGrid.ColumnHeader.Cells[0, iIndex].ForeColor = Color.Black;
                        shGrid.ColumnHeader.Cells[0, iIndex].Font = new Font("Veranda", 8, FontStyle.Bold);
                    }
                    else
                    {
                        iColumnGroupCount += 1;
                        shGrid.Models.ColumnHeaderSpan.Remove(0, iIndex - iColumnGroupCount + 1);
                        shGrid.Models.ColumnHeaderSpan.Add(0, iIndex - iColumnGroupCount + 1, 1, iColumnGroupCount);
                    }
                    //列标题
                    shGrid.ColumnHeader.Cells[1, iIndex].Text = strTitle[1];
                    //列数据源
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
                    
                    //列宽
                    shGrid.Columns[iIndex].Width = dataItemPurviewBM.ColumnWidth;
                    //列的字体和颜色
                    shGrid.Columns[iIndex].ForeColor = Color.Black;                    
                    iIndex++;
                }
                #endregion
            }
        }
        #endregion
    }
}
