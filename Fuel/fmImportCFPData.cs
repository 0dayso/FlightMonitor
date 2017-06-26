using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightMonitorBF;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace Fuel
{
    public partial class fmImportCFPData : Form
    {
        public fmImportCFPData()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string strFlightDate = dtFlightDate.Value.ToString("yyyy-MM-dd");

            //航班动态
            GuaranteeInforBF guaranteeInforBF = new GuaranteeInforBF();
            ReturnValueSF rvSF = guaranteeInforBF.GetLegsByFlightDate(strFlightDate, strFlightDate);


            DataTable dtGuaranteeInfor = new DataTable();
            if (rvSF.Result > 0)
            {
                dtGuaranteeInfor = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //计算机飞行计划
            ComputerPlanBF computerPlanBF = new ComputerPlanBF();
            rvSF = computerPlanBF.GetCFPByFlightDate(strFlightDate);

            

            DataTable dtCFP = new DataTable();
            if (rvSF.Result > 0)
            {
                dtCFP = rvSF.Dt;
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            
            IList ilMaintenGuaranteeInforBM = new ArrayList();
            foreach (DataRow rowLeg in dtGuaranteeInfor.Rows)
            {
                DataRow[] drCFP = dtCFP.Select("cndtdatop = '" + rowLeg["cncDATOP"].ToString() + "' AND cndtStdDate = '" + rowLeg["cncFlightDate"].ToString() + "' AND cnvcFlightNo = '" + rowLeg["cnvcFlightNo"].ToString() + "' AND cnvcDEPSTN = '" + rowLeg["cncDEPSTN"].ToString() + "' AND cnvcARRSTN = '" + rowLeg["cncARRSTN"].ToString() + "'");

                string strCFP = "";
                if (drCFP.Length > 0)
                {
                    strCFP = drCFP[0]["cntContent"].ToString().Replace("<br>", "\n").Replace("&nbsp;", " ");
                }

                if (strCFP.Length < 50)
                {

                    continue;
                }
                

                int iStartPos = strCFP.IndexOf("FOR ETD ");
                string strCFPTOFF = strCFP.Substring(iStartPos + 8, 4);

                int iHour = Convert.ToInt32(strCFPTOFF.Substring(0, 2));
                if (iHour + 8 > 23)
                {
                    iHour = iHour + 8 - 24;
                }
                else
                {
                    iHour = iHour + 8;
                }

                strCFPTOFF = iHour.ToString("00") + strCFPTOFF.Substring(2, 2);

                iStartPos = strCFP.IndexOf("FUEL TIME  DIST ARRIVE TAKEOFF LAND ZFW    AV PLD OPNLWT");
                iStartPos = strCFP.IndexOf("DST ", iStartPos);
                string strCFPTDWN = strCFP.Substring(iStartPos + 27, 4);

                iHour = Convert.ToInt32(strCFPTDWN.Substring(0, 2));
                if (iHour + 8 > 23)
                {
                    iHour = iHour + 8 - 24;
                }
                else
                {
                    iHour = iHour + 8;
                }
                strCFPTDWN = iHour.ToString("00") + strCFPTDWN.Substring(2, 2);


                
                MaintenGuaranteeInforBM maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                //维护信息
                maintenGuaranteeInforBM.DATOP = rowLeg["cncDATOP"].ToString();
                maintenGuaranteeInforBM.FLTID = rowLeg["cnvcFLTID"].ToString();
                maintenGuaranteeInforBM.LEGNO = rowLeg["cniLEGNO"].ToString();
                maintenGuaranteeInforBM.AC = rowLeg["cnvcAC"].ToString();
                maintenGuaranteeInforBM.FieldName = "cncCFPTOFF";
                maintenGuaranteeInforBM.FieldLength = 4;
                maintenGuaranteeInforBM.FieldType = 1;
                maintenGuaranteeInforBM.NewContent = strCFPTOFF;
                ilMaintenGuaranteeInforBM.Add(maintenGuaranteeInforBM);

                maintenGuaranteeInforBM = new MaintenGuaranteeInforBM();
                maintenGuaranteeInforBM.DATOP = rowLeg["cncDATOP"].ToString();
                maintenGuaranteeInforBM.FLTID = rowLeg["cnvcFLTID"].ToString();
                maintenGuaranteeInforBM.LEGNO = rowLeg["cniLEGNO"].ToString();
                maintenGuaranteeInforBM.AC = rowLeg["cnvcAC"].ToString();
                maintenGuaranteeInforBM.FieldName = "cncCFPTDWN";
                maintenGuaranteeInforBM.FieldLength = 4;
                maintenGuaranteeInforBM.FieldType = 1;
                maintenGuaranteeInforBM.NewContent = strCFPTDWN;
                ilMaintenGuaranteeInforBM.Add(maintenGuaranteeInforBM);
               
            }


            rvSF = guaranteeInforBF.UpdateGuaranteeInforList(ilMaintenGuaranteeInforBM);

            if (rvSF.Result > 0)
            {
                MessageBox.Show("导入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(rvSF.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}