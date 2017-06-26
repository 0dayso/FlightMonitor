using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.FlightMonitor.FlightMonitorDA;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    public class CommandInforBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommandInforBF()
        {
        }

        /// <summary>
        /// 获取某航站某类保障人员列表
        /// </summary>
        /// <param name="strCommderType"></param>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public ReturnValueSF GetCommanderByTypeAndStation(string strCommderType, string strStationCode)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            CommanderInforDAF commanderInforDAF = new CommanderInforDAF();

            try
            {
                rvSF.Dt = commanderInforDAF.GetCommanderByTypeAndStation(strCommderType, strStationCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

                /// <summary>
        /// 获取某航站保障人员列表
        /// </summary>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public ReturnValueSF GetCommanderByStation(string strStationCode)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            CommanderInforDAF commanderInforDAF = new CommanderInforDAF();

            try
            {
                rvSF.Dt = commanderInforDAF.GetCommanderByStation(strStationCode);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

          /// <summary>
        /// 添加一个航站保障人员
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertCommander(CommanderInforBM commanderInforBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            CommanderInforDAF commanderInforDAF = new CommanderInforDAF();

            try
            {
                rvSF.Result = commanderInforDAF.InsertCommander(commanderInforBM);                
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

         /// <summary>
        /// 删除一个航站保障人员
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF DeleteCommander(CommanderInforBM commanderInforBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            CommanderInforDAF commanderInforDAF = new CommanderInforDAF();

            try
            {
                rvSF.Result = commanderInforDAF.DeleteCommander(commanderInforBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }
    }
}
