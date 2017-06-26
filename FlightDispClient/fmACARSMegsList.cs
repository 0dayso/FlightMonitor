using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    public partial class fmACARSMegsList : Form
    {
        private ChangeLegsBM _changeLegsBM;
        private DataTable _dtUp;
        private DataTable _dtDown;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="changeLegsBM">航班动态</param>
        public fmACARSMegsList(ChangeLegsBM changeLegsBM)
        {
            InitializeComponent();

            _changeLegsBM = changeLegsBM;
        }

        private void fmACARSMegsList_Load(object sender, EventArgs e)
        {
            DataTable dtSchema = new DataTable();
            dtSchema.Columns.Add("时间");
            dtSchema.Columns.Add("报文类型");
            dtSchema.Columns.Add("飞机号");
            dtSchema.Columns.Add("航班号");
            dtSchema.Columns.Add("起飞机场");
            dtSchema.Columns.Add("目的机场");
            dtSchema.Columns.Add("报文内容");
            DataTable dtDown = dtSchema.Clone();
            DataTable dtUp = dtSchema.Clone();

            ArrayList alMegType = new ArrayList();
            alMegType.Add("AOC Print");
            alMegType.Add("Crew Message");
            alMegType.Add("FreeText For Collins");
            alMegType.Add("WX Report");
            alMegType.Add("Position Report");

            ArrayList alMegContent = new ArrayList();
            alMegContent.Add("QU HAKDPHU  .BJSXCXA 230000  \r\nM14  FI HU7087/AN B-2676  \r\nDT BJS HKG 230000 M32A  \r\n-  POSITION  DMY 22JUL08,UTC 23:59,FLT HU7087,LAT N 21.156,LON E110.319,MCH  749,  CAS 2995,WD 14301,WS   11,FL 32100,TTG 0115,FOB 18440    ");
            alMegContent.Add("QU HAKDPHU  .BJSXCXA 230000  \r\nDEP  FI HU7147/AN B-5139/DA ---/OF ----/DS ---  \r\nDT BJS PEK 230000 M87A  \r\n-  OFF  DEP ZBAA,OFF 0000,DES ZUUU,ETA 0221,FOB 23520    ");
            alMegContent.Add("QU HAKDPHU  .BJSXCXA 230001  \r\nA80  FI HU7383/AN B-2908  \r\nDT BJS HAK 230001 M09A  \r\n-  OFF  DEP ZJHK,OFF 0001,DES ZGSZ,ETA 0056,FOB ----    ");
            alMegContent.Add("QU HAKDPHU  .BJSXCXA 230003  \r\nDEP  FI HU7137/AN B-5373/DA ---/OT ----/BF -----/FB ----/DS ---  \r\nDT BJS PEK 230003 M75A  \r\n-  OUT  DEP ZBAA,OUT 0002,DES ZLXY,FOB 20920,CLS ----    ");
            alMegContent.Add("QU HAKDPHU  .BJSXCXA 230005  \r\nDEP  FI HU7325/AN B-5182/DA ---/OF ----/DS ---  \r\nDT BJS TYN 230005 M43A  \r\n-  OFF  DEP ZBYN,OFF 0005,DES ZGSZ,ETA 0229,FOB 25440    ");

            Random rd = new Random(0);

            this.Text = "ACARS报文|" + _changeLegsBM.FlightNo + "|" + _changeLegsBM.LONG_REG;
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dtDown.NewRow();
                dr["时间"] = DateTime.Now.ToString();
                dr["报文类型"] = alMegType[rd.Next(5)].ToString();
                dr["飞机号"] = _changeLegsBM.LONG_REG;
                dr["航班号"] = _changeLegsBM.FlightNo;
                dr["起飞机场"] = _changeLegsBM.DEPFourCode;
                dr["目的机场"] = _changeLegsBM.ARRFourCode;
                dr["报文内容"] = alMegContent[rd.Next(5)].ToString();
                dtDown.Rows.Add(dr);
            }
            _dtDown = dtDown;
            fpsMegsList.DataSource = dtDown;
            fpsMegsList.DataMember = dtDown.TableName;

            shMegsList.Columns.Count = 6;
            shMegsList.ColumnHeader.Cells[0, 0].Text = "时间";
            shMegsList.Columns[0].DataField = "时间";
            shMegsList.ColumnHeader.Cells[0, 1].Text = "报文类型";
            shMegsList.Columns[1].DataField = "报文类型";
            shMegsList.ColumnHeader.Cells[0, 2].Text = "飞机号";
            shMegsList.Columns[2].DataField = "飞机号";
            shMegsList.ColumnHeader.Cells[0, 3].Text = "航班号";
            shMegsList.Columns[3].DataField = "航班号";
            shMegsList.ColumnHeader.Cells[0, 4].Text = "起飞机场";
            shMegsList.Columns[4].DataField = "起飞机场";
            shMegsList.ColumnHeader.Cells[0, 5].Text = "目的机场";
            shMegsList.Columns[5].DataField = "目的机场";

        }

        private void fpsMegsList_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            txtMegsContent.Text = _dtDown.Rows[e.Row]["报文内容"].ToString();
        }

        private void tsmiUpMegs_Click(object sender, EventArgs e)
        {
            fmSendACARSMegs obj = new fmSendACARSMegs(_changeLegsBM);
            if (obj.ShowDialog() == DialogResult.OK)
            { 
            }
        }
    }
}