using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    public partial class fmCrewSignIn : Form
    {
        private string m_strQueryTime;
        private string m_strFlightNo;
        private string m_strDEPSTN;

        public fmCrewSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            InitializeComponent();
            this.m_strQueryTime = strQueryTime;
            this.m_strFlightNo = strFlightNo.Substring(3);
            this.m_strDEPSTN = strDEPSTN;
        }

        private void fmCrewSignIn_Load(object sender, EventArgs e)
        {
            //机组签到信息列表
            fpsCrewSignIn.ActiveSheet.Columns[0].DataField = "AP_NUMBER";
            //fpsCrewSignIn.ActiveSheet.Columns[1].DataField = "AF_BEGIN";
            //fpsCrewSignIn.ActiveSheet.Columns[2].DataField = "AF_END";
            fpsCrewSignIn.ActiveSheet.Columns[3].DataField = "AP_CODE";
            fpsCrewSignIn.ActiveSheet.Columns[4].DataField = "AF_SEG_CHINA";
            fpsCrewSignIn.ActiveSheet.Columns[5].DataField = "AF_CAPTAIN";
            fpsCrewSignIn.ActiveSheet.Columns[6].DataField = "AF_COPILOT1";
            fpsCrewSignIn.ActiveSheet.Columns[7].DataField = "AF_COPILOT2";
            fpsCrewSignIn.ActiveSheet.Columns[8].DataField = "AF_STUDENT";
            fpsCrewSignIn.ActiveSheet.Columns[9].DataField = "AF_CHECK1";
            fpsCrewSignIn.ActiveSheet.Columns[10].DataField = "AF_CHECK2";
            fpsCrewSignIn.ActiveSheet.Columns[11].DataField = "AF_CHECKMAN";
            //fpsCrewSignIn.ActiveSheet.Columns[12].DataField = "AF_QIANDAO";
            fpsCrewSignIn.ActiveSheet.Columns[13].DataField = "AF_CAPTAIN_FLAG";
            fpsCrewSignIn.ActiveSheet.Columns[14].DataField = "AF_COPILOT1_FLAG";
            fpsCrewSignIn.ActiveSheet.Columns[15].DataField = "AF_COPILOT2_FLAG";
            fpsCrewSignIn.ActiveSheet.Columns[16].DataField = "AF_STUDENT_FLAG";
            fpsCrewSignIn.ActiveSheet.Columns[17].DataField = "AF_CHECK1_FLAG";
            fpsCrewSignIn.ActiveSheet.Columns[18].DataField = "AF_CHECK2_FLAG";
            fpsCrewSignIn.ActiveSheet.Columns[19].DataField = "AF_CHECKMAN_FLAG";
            fpsCrewSignIn.ActiveSheet.Columns[20].DataField = "AF_REMARK";


            CrewSignInBF crewSignInBF = new CrewSignInBF();

            ReturnValueSF rvSF = crewSignInBF.GetCrewSignIn(m_strQueryTime, m_strFlightNo, m_strDEPSTN);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dtCrewSignIn = rvSF.Dt;

            if (dtCrewSignIn.Rows.Count == 0)
            {
                MessageBox.Show("非始发航班，没有签到记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //foreach (DataRow dataRow in dtCrewSignIn.Rows)
            //{
            //    dataRow["AF_BEGIN"] = DateTime.Parse(dataRow["AF_BEGIN"].ToString()).ToString("HH:mm");
            //    dataRow["AF_END"] = DateTime.Parse(dataRow["AF_END"].ToString()).ToString("HH:mm");
            //    dataRow["AF_QIANDAO"] = DateTime.Parse(dataRow["AF_QIANDAO"].ToString()).ToString("HH:mm");
            //}

            fpsCrewSignIn.DataSource = dtCrewSignIn;

            //由于有末尾加A的航班，因此可能查出两条记录，加入循环，处理两条记录
            for (int iLoop = 0; iLoop < dtCrewSignIn.Rows.Count; iLoop++)
            {
                fpsCrewSignIn.ActiveSheet.Cells[iLoop, 1].Text = DateTime.Parse(dtCrewSignIn.Rows[iLoop]["AF_BEGIN"].ToString()).ToString("HHmm");
                fpsCrewSignIn.ActiveSheet.Cells[iLoop, 2].Text = DateTime.Parse(dtCrewSignIn.Rows[iLoop]["AF_END"].ToString()).ToString("HHmm");
                fpsCrewSignIn.ActiveSheet.Cells[iLoop, 12].Text = DateTime.Parse(dtCrewSignIn.Rows[iLoop]["AF_QIANDAO"].ToString()).ToString("HHmm");

                int iCaptainFlag = Convert.ToInt32(dtCrewSignIn.Rows[iLoop]["AF_CAPTAIN_FLAG"].ToString());
                int iCopilot1Flag = Convert.ToInt32(dtCrewSignIn.Rows[iLoop]["AF_COPILOT1_FLAG"].ToString());
                int iCopilot2Flag = Convert.ToInt32(dtCrewSignIn.Rows[iLoop]["AF_COPILOT2_FLAG"].ToString());
                int iStudentFlag = Convert.ToInt32(dtCrewSignIn.Rows[iLoop]["AF_STUDENT_FLAG"].ToString());
                int iCheck1Flag = Convert.ToInt32(dtCrewSignIn.Rows[iLoop]["AF_CHECK1_FLAG"].ToString());
                int iCheck2Flag = Convert.ToInt32(dtCrewSignIn.Rows[iLoop]["AF_CHECK2_FLAG"].ToString());
                int iCheckManFlag = Convert.ToInt32(dtCrewSignIn.Rows[iLoop]["AF_CHECKMAN_FLAG"].ToString());
                //如果所有人员准时签到，则此行记录底色为浅绿色
                //cell值为0有两种情况，一是该位置为空，二是该位置的人员姓名在数据库中不存在
                //此处将cell值为0的情况一律视为该位置为空，如遇到该位置人员姓名在数据库中不存在
                //的情况，在后面进行处理
                if (iCaptainFlag == 2 && iCopilot1Flag == 2 && iCopilot2Flag == 2 && (iStudentFlag == 2 || iStudentFlag == 0) && (iCheck1Flag == 2 || iCheck1Flag == 0) && (iCheck2Flag == 2 || iCheck2Flag == 0) && (iCheckManFlag == 2 || iCheckManFlag == 0))
                {
                    fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor = Color.FromName("LightGreen");
                }
                //如果未到签到时间，则此行记录底色为浅蓝色
                else if (iCaptainFlag == 1 && iCopilot1Flag == 1 && iCopilot2Flag == 1 && (iStudentFlag == 1 || iStudentFlag == 0) && (iCheck1Flag == 1 || iCheck1Flag == 0) && (iCheck2Flag == 1 || iCheck2Flag == 0) && (iCheckManFlag == 1 || iCheckManFlag == 0))
                {
                    fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor = Color.FromName("LightSkyBlue");
                }
                //判断一行中是否有某人员姓名在数据库中不存在的情况，若有，则将该行的底色设为白色
                //若某人员姓名在数据库中不存在，即为标记单元格值为0，但姓名单元格有名字
                //另外只有所有人都准时签到时需要更改，因此加入这一条件进行判断
                for (int i = 13; i < 20; i++)
                {
                    int j = i - 8;
                    if (fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor == Color.FromName("LightGreen") && (int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 0 && (string)fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].Value != "")
                    {
                        fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor = Color.FromName("White");
                    }
                }
                //从显示标记的单元格开始处理
                //i为签到标记的单元格列值，j为签到人员姓名的单元格列值
                //此处标记签到的单元格从第13列开始，到20列结束
                for (int i = 13; i < 20; i++)
                {
                    //如果此人员准时签到，则字体为绿色
                    if ((int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 2)
                    {
                        //如果不是所有人员都准时签到，则分别对单元格的字体颜色赋值
                        if (fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor != Color.FromName("LightGreen"))
                        {
                            int j = i - 8;
                            fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].ForeColor = Color.FromName("LightGreen");
                        }
                    }
                    //如果此人员未准时签到，则字体为红色
                    else if ((int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 3)
                    {
                        int j = i - 8;
                        fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].ForeColor = Color.FromName("Red");
                    }
                    //如果此人员姓名在数据库不存在，则字体为红色，中间加一横线	
                    else if ((int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 0)
                    {
                        int j = i - 8;
                        fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].ForeColor = Color.FromName("Red");
                        fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].Font = new Font("宋体", 9, System.Drawing.FontStyle.Strikeout);
                    }
                }
            }
        }

        private void fpsCrewSignIn_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //如果点击的是表头，则不弹出任何窗体
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            //仅在双击显示飞行员姓名的单元格时，弹出显示签到记录的窗体
            if (e.Column >= 5 && e.Column <= 11)
            {
                fmCrewSignTime objfmCrewSignTime = new fmCrewSignTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_strDEPSTN, fpsCrewSignIn.ActiveSheet.Cells[e.Row, e.Column].Text.Trim());

                objfmCrewSignTime.ShowDialog();
            }
        }

       
    }
}