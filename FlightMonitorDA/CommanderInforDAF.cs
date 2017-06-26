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
        /// ���캯��
        /// </summary>
        public CommanderInforDAF()
        {
        }

         /// <summary>
        /// ��ȡĳ��վĳ�ౣ����Ա�б�
        /// </summary>
        /// <param name="strCommderType"></param>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public DataTable GetCommanderByTypeAndStation(string strCommderType, string strStationCode)
        {
            //���巵��ֵ
            DataTable dtCommander = new DataTable();
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //�����ݿ�����
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
        /// ��ȡĳ��վ������Ա�б�
        /// </summary>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public DataTable GetCommanderByStation(string strStationCode)
        {
            //���巵��ֵ
            DataTable dtCommander = new DataTable();
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //�����ݿ�����
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
        /// ���һ����վ������Ա
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public int InsertCommander(CommanderInforBM commanderInforBM)
        {
            //���巵��ֵ
            int iResult = -1;
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //�����ݿ�����
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
        /// ɾ��һ����վ������Ա
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public int DeleteCommander(CommanderInforBM commanderInforBM)
        {
            //���巵��ֵ
            int iResult = -1;
            CommanderInforDA commanderInforDA = new CommanderInforDA();

            try
            {
                //�����ݿ�����
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
