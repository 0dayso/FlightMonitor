using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    public class CommanderInforDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommanderInforDAF()
        {
        }

         /// <summary>
        /// 获取某航站某类保障人员列表
        /// </summary>
        /// <param name="strCommderType"></param>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public DataTable GetCommanderByTypeAndStation(string strCommderType, string strStationCode)
        {
            //定义返回值
            DataTable dtCommander = new DataTable();
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //打开数据库联机
                commanderInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtCommander = commanderInforDA.GetCommanderByTypeAndStation(strCommderType, strStationCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commanderInforDA.ConnClose();
            }

            return dtCommander;
        }

        /// <summary>
        /// 获取某航站保障人员列表
        /// </summary>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public DataTable GetCommanderByStation(string strStationCode)
        {
            //定义返回值
            DataTable dtCommander = new DataTable();
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //打开数据库联机
                commanderInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtCommander = commanderInforDA.GetCommanderByStation(strStationCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commanderInforDA.ConnClose();
            }

            return dtCommander;
        }

        /// <summary>
        /// 添加一个航站保障人员
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public int InsertCommander(CommanderInforBM commanderInforBM)
        {
            //定义返回值
            int iResult = -1;
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //打开数据库联机
                commanderInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                iResult = commanderInforDA.InsertCommander(commanderInforBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commanderInforDA.ConnClose();
            }

            return iResult;
        }

         /// <summary>
        /// 删除一个航站保障人员
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public int DeleteCommander(CommanderInforBM commanderInforBM)
        {
            //定义返回值
            int iResult = -1;
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //打开数据库联机
                commanderInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                iResult = commanderInforDA.DeleteCommander(commanderInforBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                commanderInforDA.ConnClose();
            }

            return iResult;
        }
    }
}
