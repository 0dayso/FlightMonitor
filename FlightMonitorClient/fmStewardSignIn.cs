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
    public partial class fmStewardSignIn : Form
    {
        private string m_strQueryTime;
        private string m_strFlightNo;
        private string m_strDEPSTN;

        public fmStewardSignIn(string strQueryTime, string strFlightNo, string strDEPSTN)
        {
            InitializeComponent();
            this.m_strQueryTime = strQueryTime;
            this.m_strFlightNo = strFlightNo.Substring(3);
            this.m_strDEPSTN = strDEPSTN;
        }

        private void fmStewardSignIn_Load(object sender, EventArgs e)
        {
            //乘务签到列表
            fpsStewardSignIn.ActiveSheet.Columns[0].DataField = "AP_NUMBER";
            //fpsStewardSignIn.ActiveSheet.Columns[1].DataField = "AT_BEGIN";
            //fpsStewardSignIn.ActiveSheet.Columns[2].DataField = "AT_END";
            fpsStewardSignIn.ActiveSheet.Columns[3].DataField = "AP_CODE";
            fpsStewardSignIn.ActiveSheet.Columns[4].DataField = "AT_SEG_CHINA";
            fpsStewardSignIn.ActiveSheet.Columns[5].DataField = "AT_STCAPTAIN1";
            fpsStewardSignIn.ActiveSheet.Columns[6].DataField = "AT_STCAPTAIN2";
            fpsStewardSignIn.ActiveSheet.Columns[7].DataField = "AT_STEWARD1";
            fpsStewardSignIn.ActiveSheet.Columns[8].DataField = "AT_STEWARD2";
            fpsStewardSignIn.ActiveSheet.Columns[9].DataField = "AT_STEWARD3";
            fpsStewardSignIn.ActiveSheet.Columns[10].DataField = "AT_STEWARD4";
            fpsStewardSignIn.ActiveSheet.Columns[11].DataField = "AT_STEWARD5";
            fpsStewardSignIn.ActiveSheet.Columns[12].DataField = "AT_STEWARD6";
            fpsStewardSignIn.ActiveSheet.Columns[13].DataField = "AT_STEWARD7";
            fpsStewardSignIn.ActiveSheet.Columns[14].DataField = "AT_STEWARD8";
            fpsStewardSignIn.ActiveSheet.Columns[15].DataField = "AT_STEWARD9";
            fpsStewardSignIn.ActiveSheet.Columns[16].DataField = "AT_STEWARD10";
            fpsStewardSignIn.ActiveSheet.Columns[17].DataField = "AT_SAFEMAN1";
            fpsStewardSignIn.ActiveSheet.Columns[18].DataField = "AT_SAFEMAN2";
            fpsStewardSignIn.ActiveSheet.Columns[19].DataField = "AT_CHECKMAN";
            //fpsStewardSignIn.ActiveSheet.Columns[20].DataField = "AF_QIANDAO";
            fpsStewardSignIn.ActiveSheet.Columns[21].DataField = "AT_STCAPTAIN1_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[22].DataField = "AT_STCAPTAIN2_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[23].DataField = "AT_STEWARD1_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[24].DataField = "AT_STEWARD2_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[25].DataField = "AT_STEWARD3_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[26].DataField = "AT_STEWARD4_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[27].DataField = "AT_STEWARD5_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[28].DataField = "AT_STEWARD6_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[29].DataField = "AT_STEWARD7_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[30].DataField = "AT_STEWARD8_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[31].DataField = "AT_STEWARD9_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[32].DataField = "AT_STEWARD10_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[33].DataField = "AT_SAFEMAN1_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[34].DataField = "AT_SAFEMAN2_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[35].DataField = "AT_CHECKMAN_FLAG";
            fpsStewardSignIn.ActiveSheet.Columns[36].DataField = "AT_REMARK";


            CrewSignInBF crewSignInBF = new CrewSignInBF();

            ReturnValueSF rvSF = crewSignInBF.GetStewardSignIn(m_strQueryTime, m_strFlightNo, m_strDEPSTN);

            if (rvSF.Result < 0)
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dtStewardSignIn = rvSF.Dt;

            if (dtStewardSignIn.Rows.Count == 0)
            {
                MessageBox.Show("非始发航班，没有签到记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            fpsStewardSignIn.DataSource = dtStewardSignIn;

            fpsStewardSignIn.ActiveSheet.Cells[0, 1].Text = DateTime.Parse(dtStewardSignIn.Rows[0]["AT_BEGIN"].ToString()).ToString("HHmm");
            fpsStewardSignIn.ActiveSheet.Cells[0, 2].Text = DateTime.Parse(dtStewardSignIn.Rows[0]["AT_END"].ToString()).ToString("HHmm");
            fpsStewardSignIn.ActiveSheet.Cells[0, 20].Text = DateTime.Parse(dtStewardSignIn.Rows[0]["AF_QIANDAO"].ToString()).ToString("HHmm");

            int stCaptain1Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 21].Value;
            int stCaptain2Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 22].Value;
            int steward1Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 23].Value;
            int steward2Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 24].Value;
            int steward3Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 25].Value;
            int steward4Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 26].Value;
            int steward5Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 27].Value;
            int steward6Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 28].Value;
            int steward7Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 29].Value;
            int steward8Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 30].Value;
            int steward9Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 31].Value;
            int steward10Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 32].Value;
            int safeMan1Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 33].Value;
            int safeMan2Flag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 34].Value;
            int checkManFlag = (int)fpsStewardSignIn.ActiveSheet.Cells[0, 35].Value;

            int i, j;
            //如果所有人员准时签到，则此行记录底色为浅绿色
            //cell值为0有两种情况，一是该位置为空，二是该位置的人员姓名在数据库中不存在
            //此处将cell值为0的情况一律视为该位置为空，如遇到该位置人员姓名在数据库中不存在
            //的情况，在后面进行处理
            if ((stCaptain1Flag == 2 || stCaptain1Flag == 0) && stCaptain2Flag == 2 && (steward1Flag == 2 || steward1Flag == 0) && (steward2Flag == 2 || steward2Flag == 0) && (steward3Flag == 2 || steward3Flag == 0) && (steward4Flag == 2 || steward4Flag == 0) && (steward5Flag == 2 || steward5Flag == 0) && (steward6Flag == 2 || steward6Flag == 0) && (steward7Flag == 2 || steward7Flag == 0) && (steward8Flag == 2 || steward8Flag == 0) && (steward9Flag == 2 || steward9Flag == 0) && (steward10Flag == 2 || steward10Flag == 0) && (safeMan1Flag == 2 || safeMan1Flag == 0) && (safeMan2Flag == 2 || safeMan2Flag == 0) && (checkManFlag == 2 || checkManFlag == 0))
            {
                fpsStewardSignIn.ActiveSheet.Rows[0].BackColor = Color.FromName("LightGreen");
            }
            //如果未到签到时间，则此行记录底色为浅蓝色
            else if ((stCaptain1Flag == 1 || stCaptain1Flag == 0) && stCaptain2Flag == 1 && (steward1Flag == 1 || steward1Flag == 0) && (steward2Flag == 1 || steward2Flag == 0) && (steward3Flag == 1 || steward3Flag == 0) && (steward4Flag == 1 || steward4Flag == 0) && (steward5Flag == 1 || steward5Flag == 0) && (steward6Flag == 1 || steward6Flag == 0) && (steward7Flag == 1 || steward7Flag == 0) && (steward8Flag == 1 || steward8Flag == 0) && (steward9Flag == 1 || steward9Flag == 0) && (steward10Flag == 1 || steward10Flag == 0) && (safeMan1Flag == 1 || safeMan1Flag == 0) && (safeMan2Flag == 1 || safeMan2Flag == 0) && (checkManFlag == 1 || checkManFlag == 0))
            {
                fpsStewardSignIn.ActiveSheet.Rows[0].BackColor = Color.FromName("LightSkyBlue");
            }

            //判断一行中是否有某人员姓名在数据库中不存在的情况，若有，则将该行的底色设为白色
            //若某人员姓名在数据库中不存在，即为标记单元格值为0，但姓名单元格有名字
            //另外只有所有人都准时签到时需要更改，因此加入这一条件进行判断
            for (i = 21; i < 36; i++)
            {
                j = i - 16;
                if (fpsStewardSignIn.ActiveSheet.Rows[0].BackColor == Color.FromName("LightGreen") && (int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 0 && (string)fpsStewardSignIn.ActiveSheet.Cells[0, j].Value != "")
                {
                    fpsStewardSignIn.ActiveSheet.Rows[0].BackColor = Color.FromName("White");
                }
            }
            //从显示标记的单元格开始处理
            //i为签到标记的单元格列值，j为签到人员姓名的单元格列值
            //此处标记签到的单元格从第21列开始，到35列结束
            for (i = 21; i < 36; i++)
            {
                //如果此人员准时签到，则字体为绿色
                if ((int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 2)
                {
                    //如果不是所有人员都准时签到，则分别对单元格的字体颜色赋值
                    if (fpsStewardSignIn.ActiveSheet.Rows[0].BackColor != Color.FromName("LightGreen"))
                    {
                        j = i - 16;
                        fpsStewardSignIn.ActiveSheet.Cells[0, j].ForeColor = Color.FromName("LightGreen");
                    }
                }
                //如果此人员未准时签到，则字体为红色
                else if ((int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 3)
                {
                    j = i - 16;
                    fpsStewardSignIn.ActiveSheet.Cells[0, j].ForeColor = Color.FromName("Red");
                }
                //如果此人员姓名在数据库不存在，则字体为红色，中间加一横线	
                else if ((int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 0)
                {
                    j = i - 16;
                    fpsStewardSignIn.ActiveSheet.Cells[0, j].ForeColor = Color.FromName("Red");
                    fpsStewardSignIn.ActiveSheet.Cells[0, j].Font = new Font("宋体", 9, System.Drawing.FontStyle.Strikeout);
                }
            }


        }

        private void fpsStewardSignIn_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //如果点击的是表头，则不弹出任何窗体
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            //仅在双击显示飞行员姓名的单元格时，弹出显示签到记录的窗体
            if (e.Column >= 5 && e.Column <= 19)
            {
                fmStewardSignTime objfmStewardSignTime = new fmStewardSignTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_strDEPSTN, fpsStewardSignIn.ActiveSheet.Cells[e.Row, e.Column].Text.Trim());

                objfmStewardSignTime.ShowDialog();
            }
        }
    }
}