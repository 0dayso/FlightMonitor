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
            //����ǩ����Ϣ�б�
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
                MessageBox.Show(rvSF.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dtCrewSignIn = rvSF.Dt;

            if (dtCrewSignIn.Rows.Count == 0)
            {
                MessageBox.Show("��ʼ�����࣬û��ǩ����¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            //������ĩβ��A�ĺ��࣬��˿��ܲ��������¼������ѭ��������������¼
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
                //���������Ա׼ʱǩ��������м�¼��ɫΪǳ��ɫ
                //cellֵΪ0�����������һ�Ǹ�λ��Ϊ�գ����Ǹ�λ�õ���Ա���������ݿ��в�����
                //�˴���cellֵΪ0�����һ����Ϊ��λ��Ϊ�գ���������λ����Ա���������ݿ��в�����
                //��������ں�����д���
                if (iCaptainFlag == 2 && iCopilot1Flag == 2 && iCopilot2Flag == 2 && (iStudentFlag == 2 || iStudentFlag == 0) && (iCheck1Flag == 2 || iCheck1Flag == 0) && (iCheck2Flag == 2 || iCheck2Flag == 0) && (iCheckManFlag == 2 || iCheckManFlag == 0))
                {
                    fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor = Color.FromName("LightGreen");
                }
                //���δ��ǩ��ʱ�䣬����м�¼��ɫΪǳ��ɫ
                else if (iCaptainFlag == 1 && iCopilot1Flag == 1 && iCopilot2Flag == 1 && (iStudentFlag == 1 || iStudentFlag == 0) && (iCheck1Flag == 1 || iCheck1Flag == 0) && (iCheck2Flag == 1 || iCheck2Flag == 0) && (iCheckManFlag == 1 || iCheckManFlag == 0))
                {
                    fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor = Color.FromName("LightSkyBlue");
                }
                //�ж�һ�����Ƿ���ĳ��Ա���������ݿ��в����ڵ���������У��򽫸��еĵ�ɫ��Ϊ��ɫ
                //��ĳ��Ա���������ݿ��в����ڣ���Ϊ��ǵ�Ԫ��ֵΪ0����������Ԫ��������
                //����ֻ�������˶�׼ʱǩ��ʱ��Ҫ���ģ���˼�����һ���������ж�
                for (int i = 13; i < 20; i++)
                {
                    int j = i - 8;
                    if (fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor == Color.FromName("LightGreen") && (int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 0 && (string)fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].Value != "")
                    {
                        fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor = Color.FromName("White");
                    }
                }
                //����ʾ��ǵĵ�Ԫ��ʼ����
                //iΪǩ����ǵĵ�Ԫ����ֵ��jΪǩ����Ա�����ĵ�Ԫ����ֵ
                //�˴����ǩ���ĵ�Ԫ��ӵ�13�п�ʼ����20�н���
                for (int i = 13; i < 20; i++)
                {
                    //�������Ա׼ʱǩ����������Ϊ��ɫ
                    if ((int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 2)
                    {
                        //�������������Ա��׼ʱǩ������ֱ�Ե�Ԫ���������ɫ��ֵ
                        if (fpsCrewSignIn.ActiveSheet.Rows[iLoop].BackColor != Color.FromName("LightGreen"))
                        {
                            int j = i - 8;
                            fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].ForeColor = Color.FromName("LightGreen");
                        }
                    }
                    //�������Աδ׼ʱǩ����������Ϊ��ɫ
                    else if ((int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 3)
                    {
                        int j = i - 8;
                        fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].ForeColor = Color.FromName("Red");
                    }
                    //�������Ա���������ݿⲻ���ڣ�������Ϊ��ɫ���м��һ����	
                    else if ((int)fpsCrewSignIn.ActiveSheet.Cells[iLoop, i].Value == 0)
                    {
                        int j = i - 8;
                        fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].ForeColor = Color.FromName("Red");
                        fpsCrewSignIn.ActiveSheet.Cells[iLoop, j].Font = new Font("����", 9, System.Drawing.FontStyle.Strikeout);
                    }
                }
            }
        }

        private void fpsCrewSignIn_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //���������Ǳ�ͷ���򲻵����κδ���
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            //����˫����ʾ����Ա�����ĵ�Ԫ��ʱ��������ʾǩ����¼�Ĵ���
            if (e.Column >= 5 && e.Column <= 11)
            {
                fmCrewSignTime objfmCrewSignTime = new fmCrewSignTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_strDEPSTN, fpsCrewSignIn.ActiveSheet.Cells[e.Row, e.Column].Text.Trim());

                objfmCrewSignTime.ShowDialog();
            }
        }

       
    }
}