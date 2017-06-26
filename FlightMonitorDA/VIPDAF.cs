using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// VIP���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-18
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class VIPDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public VIPDAF()
        {
        }

        /// <summary>
        /// ��FOC��ȡ����������VIP
        /// </summary>
        /// <param name="strStartDATOP">��ʼ���� ��ʽ2007-05-12</param>
        /// <returns>�������ݼ���VIP</returns>
        public DataSet GetVIPFromFoc(string strStartDATOP)
        {
            DataSet dsFocVIP = new DataSet();
            VIPDA vipDA = new VIPDA();
            try
            {
                dsFocVIP = vipDA.GetVIPFromFoc(strStartDATOP);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsFocVIP;
        }

         /// <summary>
        /// ����DATOP��ȡ����VIP
        /// </summary>
        /// <param name="strDATOP">����</param>
        /// <returns>�������з���������VIP�����ݱ�</returns>
        public DataTable GetVIPByDATOP(string strDATOP)
        {
            //���巵��ֵ
            DataTable dt = new DataTable();

            FlightMonitorDA.VIPDA vipDA = new VIPDA();
            try
            {
                vipDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = vipDA.GetVIPByDATOP(strDATOP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                vipDA.ConnClose();
            }

            return dt;
        }

        /// <summary>
        /// ����VIP������Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public int UpdateVipInfor(FlightMonitorBM.VIPBM vipBM)
        {
            //���巵��ֵ
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();
            try
            {
                //����������
                vipDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

                retVal = vipDA.UpdateVipInfor(vipBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                vipDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ����һ��VIP��Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public int InsertVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //���巵��ֵ
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();

            try
            {
                //�����ݿ�����
                vipDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

                retVal = vipDA.InsertVIP(vipBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                vipDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// ɾ��һ��VIP��Ϣ
        /// </summary>
        /// <param name="vipBM">VIPʵ�����</param>
        /// <returns></returns>
        public int DeleteVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //���巵��ֵ
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();

            try
            {
                //�����ݿ�����
                vipDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

                retVal = vipDA.DeleteVIP(vipBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                vipDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// �����ÿ������ͺ�����Ϣ��ȡVIP
        /// </summary>
        /// <param name="vipBM"></param>
        /// <returns></returns>
        public DataTable GetVIPByName(VIPBM vipBM)
        {
            //���巵��ֵ
            DataTable dt = new DataTable();

            FlightMonitorDA.VIPDA vipDA = new VIPDA();
            try
            {
                vipDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = vipDA.GetVIPByName(vipBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                vipDA.ConnClose();
            }

            return dt;
        }

        /// <summary>
        /// ��ȡ�����VIP��Ϣ
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public DataTable GetVIPByFlight(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            //���巵��ֵ
            DataTable dt = new DataTable();

            FlightMonitorDA.VIPDA vipDA = new VIPDA();
            try
            {
                vipDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dt = vipDA.GetVIPByFlight(changeLegsBM, iDeleteTag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                vipDA.ConnClose();
            }

            return dt;
        }

        /// <summary>
        /// �߼�ɾ�������һ������
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public int UpdateDeleteTag(VIPBM vipBM, int iDeleteTag)
        {
            //���巵��ֵ
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();

            try
            {
                //�����ݿ�����
                vipDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

                retVal = vipDA.UpdateDeleteTag(vipBM, iDeleteTag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                vipDA.ConnClose();
            }

            return retVal;
        }
    }
}
