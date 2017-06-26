using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AirSoft.FlightMonitor.FlightDispClient;

namespace AirSoft.FlightMonitor.FlightDispClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //签派放行系统主窗体
            fmMDIFlightDisp objfmMDIFlightDisp = new fmMDIFlightDisp();

            fmLogOn objfmLogOn = new fmLogOn();
            objfmLogOn.ShowDialog();
            if (objfmLogOn.IsLogin == true)
            {
                Application.Run(objfmMDIFlightDisp);
            }
        }
    }
}