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
            //����ǩ���б�
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dtStewardSignIn = rvSF.Dt;

            if (dtStewardSignIn.Rows.Count == 0)
            {
                MessageBox.Show("��ʼ�����࣬û��ǩ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            //���������Ա׼ʱǩ��������м�¼��ɫΪǳ��ɫ
            //cellֵΪ0�����������һ�Ǹ�λ��Ϊ�գ����Ǹ�λ�õ���Ա���������ݿ��в�����
            //�˴���cellֵΪ0�����һ����Ϊ��λ��Ϊ�գ���������λ����Ա���������ݿ��в�����
            //��������ں�����д���
            if ((stCaptain1Flag == 2 || stCaptain1Flag == 0) && stCaptain2Flag == 2 && (steward1Flag == 2 || steward1Flag == 0) && (steward2Flag == 2 || steward2Flag == 0) && (steward3Flag == 2 || steward3Flag == 0) && (steward4Flag == 2 || steward4Flag == 0) && (steward5Flag == 2 || steward5Flag == 0) && (steward6Flag == 2 || steward6Flag == 0) && (steward7Flag == 2 || steward7Flag == 0) && (steward8Flag == 2 || steward8Flag == 0) && (steward9Flag == 2 || steward9Flag == 0) && (steward10Flag == 2 || steward10Flag == 0) && (safeMan1Flag == 2 || safeMan1Flag == 0) && (safeMan2Flag == 2 || safeMan2Flag == 0) && (checkManFlag == 2 || checkManFlag == 0))
            {
                fpsStewardSignIn.ActiveSheet.Rows[0].BackColor = Color.FromName("LightGreen");
            }
            //���δ��ǩ��ʱ�䣬����м�¼��ɫΪǳ��ɫ
            else if ((stCaptain1Flag == 1 || stCaptain1Flag == 0) && stCaptain2Flag == 1 && (steward1Flag == 1 || steward1Flag == 0) && (steward2Flag == 1 || steward2Flag == 0) && (steward3Flag == 1 || steward3Flag == 0) && (steward4Flag == 1 || steward4Flag == 0) && (steward5Flag == 1 || steward5Flag == 0) && (steward6Flag == 1 || steward6Flag == 0) && (steward7Flag == 1 || steward7Flag == 0) && (steward8Flag == 1 || steward8Flag == 0) && (steward9Flag == 1 || steward9Flag == 0) && (steward10Flag == 1 || steward10Flag == 0) && (safeMan1Flag == 1 || safeMan1Flag == 0) && (safeMan2Flag == 1 || safeMan2Flag == 0) && (checkManFlag == 1 || checkManFlag == 0))
            {
                fpsStewardSignIn.ActiveSheet.Rows[0].BackColor = Color.FromName("LightSkyBlue");
            }

            //�ж�һ�����Ƿ���ĳ��Ա���������ݿ��в����ڵ���������У��򽫸��еĵ�ɫ��Ϊ��ɫ
            //��ĳ��Ա���������ݿ��в����ڣ���Ϊ��ǵ�Ԫ��ֵΪ0����������Ԫ��������
            //����ֻ�������˶�׼ʱǩ��ʱ��Ҫ���ģ���˼�����һ���������ж�
            for (i = 21; i < 36; i++)
            {
                j = i - 16;
                if (fpsStewardSignIn.ActiveSheet.Rows[0].BackColor == Color.FromName("LightGreen") && (int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 0 && (string)fpsStewardSignIn.ActiveSheet.Cells[0, j].Value != "")
                {
                    fpsStewardSignIn.ActiveSheet.Rows[0].BackColor = Color.FromName("White");
                }
            }
            //����ʾ��ǵĵ�Ԫ��ʼ����
            //iΪǩ����ǵĵ�Ԫ����ֵ��jΪǩ����Ա�����ĵ�Ԫ����ֵ
            //�˴����ǩ���ĵ�Ԫ��ӵ�21�п�ʼ����35�н���
            for (i = 21; i < 36; i++)
            {
                //�������Ա׼ʱǩ����������Ϊ��ɫ
                if ((int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 2)
                {
                    //�������������Ա��׼ʱǩ������ֱ�Ե�Ԫ���������ɫ��ֵ
                    if (fpsStewardSignIn.ActiveSheet.Rows[0].BackColor != Color.FromName("LightGreen"))
                    {
                        j = i - 16;
                        fpsStewardSignIn.ActiveSheet.Cells[0, j].ForeColor = Color.FromName("LightGreen");
                    }
                }
                //�������Աδ׼ʱǩ����������Ϊ��ɫ
                else if ((int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 3)
                {
                    j = i - 16;
                    fpsStewardSignIn.ActiveSheet.Cells[0, j].ForeColor = Color.FromName("Red");
                }
                //�������Ա���������ݿⲻ���ڣ�������Ϊ��ɫ���м��һ����	
                else if ((int)fpsStewardSignIn.ActiveSheet.Cells[0, i].Value == 0)
                {
                    j = i - 16;
                    fpsStewardSignIn.ActiveSheet.Cells[0, j].ForeColor = Color.FromName("Red");
                    fpsStewardSignIn.ActiveSheet.Cells[0, j].Font = new Font("����", 9, System.Drawing.FontStyle.Strikeout);
                }
            }


        }

        private void fpsStewardSignIn_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //���������Ǳ�ͷ���򲻵����κδ���
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            //����˫����ʾ����Ա�����ĵ�Ԫ��ʱ��������ʾǩ����¼�Ĵ���
            if (e.Column >= 5 && e.Column <= 19)
            {
                fmStewardSignTime objfmStewardSignTime = new fmStewardSignTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_strDEPSTN, fpsStewardSignIn.ActiveSheet.Cells[e.Row, e.Column].Text.Trim());

                objfmStewardSignTime.ShowDialog();
            }
        }
    }
}