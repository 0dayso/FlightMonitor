using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBR
{
    /// <summary>
    /// ACARS动态业务规则层
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSLegsBR
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ACARSLegsBR()
        {
        }

        public ChangeLegsBM GetOriginalLegsBM(ReturnValueSF rvSF, FlightMonitorBM.ACARSLegsBM acarsBM)
        {
            DataTable dtOriginalLegs = rvSF.Dt;

            //没有相应航班
            if (dtOriginalLegs.Rows.Count <= 0)
            {
                return null;
            }

            //只有一条相应航班
            if (dtOriginalLegs.Rows.Count == 1)
            {
                return new ChangeLegsBM(dtOriginalLegs.Rows[0]);
            }

            ChangeLegsBM changeLegsBM = new ChangeLegsBM(dtOriginalLegs.Rows[0]);
            //有多条相应航班
            double dDiffMinutes = 60;
            if (dtOriginalLegs.Rows.Count > 1)
            {
                if (acarsBM.MessageType == MsgType.OUT)
                {
                    foreach (DataRow rowItem in dtOriginalLegs.Rows)
                    {
                        ChangeLegsBM tempChangeLegsBM = new ChangeLegsBM(rowItem);
                        TimeSpan ts = DateTime.Parse(tempChangeLegsBM.ETD).Subtract(DateTime.Parse(acarsBM.PushTime));
                        if (Math.Round(ts.TotalMinutes) < dDiffMinutes)
                        {
                            changeLegsBM = tempChangeLegsBM;
                            break;
                        }
                    }
                }
                else if (acarsBM.MessageType == MsgType.OFF)
                {
                    foreach (DataRow rowItem in dtOriginalLegs.Rows)
                    {
                        ChangeLegsBM tempChangeLegsBM = new ChangeLegsBM(rowItem);
                        TimeSpan ts = DateTime.Parse(tempChangeLegsBM.TOFF).Subtract(DateTime.Parse(acarsBM.TOFF));
                        if (Math.Round(ts.TotalMinutes) < dDiffMinutes)
                        {
                            changeLegsBM = tempChangeLegsBM;
                            break;
                        }
                    }
                }
                else if (acarsBM.MessageType == MsgType.ON)
                {
                    foreach (DataRow rowItem in dtOriginalLegs.Rows)
                    {
                        ChangeLegsBM tempChangeLegsBM = new ChangeLegsBM(rowItem);
                        TimeSpan ts = DateTime.Parse(tempChangeLegsBM.ETA).Subtract(DateTime.Parse(acarsBM.TDWN));
                        if (Math.Round(ts.TotalMinutes) < dDiffMinutes)
                        {
                            changeLegsBM = tempChangeLegsBM;
                            break;
                        }
                    }
                }
                else if (acarsBM.MessageType == MsgType.IN)
                {
                    foreach (DataRow rowItem in dtOriginalLegs.Rows)
                    {
                        ChangeLegsBM tempChangeLegsBM = new ChangeLegsBM(rowItem);
                        TimeSpan ts = DateTime.Parse(tempChangeLegsBM.ATA).Subtract(DateTime.Parse(acarsBM.ATA));
                        if (Math.Round(ts.TotalMinutes) < dDiffMinutes)
                        {
                            changeLegsBM = tempChangeLegsBM;
                            break;
                        }
                    }
                }
            }

            return changeLegsBM;
        }
    }
}
