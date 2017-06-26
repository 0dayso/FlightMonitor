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
        /// ���캯��
        /// </summary>
        public CommandInforBF()
        {
        }

        /// <summary>
        /// ��ȡĳ��վĳ�ౣ����Ա�б�
        /// </summary>
        /// <param name="strCommderType"></param>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public ReturnValueSF GetCommanderByTypeAndStation(string strCommderType, string strStationCode)
        {
            //�Զ��巵��ֵ
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
        /// ��ȡĳ��վ������Ա�б�
        /// </summary>
        /// <param name="strStationCode"></param>
        /// <returns></returns>
        public ReturnValueSF GetCommanderByStation(string strStationCode)
        {
            //�Զ��巵��ֵ
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
        /// ���һ����վ������Ա
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF InsertCommander(CommanderInforBM commanderInforBM)
        {
            //�Զ��巵��ֵ
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
        /// ɾ��һ����վ������Ա
        /// </summary>
        /// <param name="commanderInforBM"></param>
        /// <returns></returns>
        public ReturnValueSF DeleteCommander(CommanderInforBM commanderInforBM)
        {
            //�Զ��巵��ֵ
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
