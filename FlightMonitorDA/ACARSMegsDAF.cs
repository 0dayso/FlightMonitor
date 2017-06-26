using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ACARS���Ĵ������ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���  ��
    /// �������ڣ�2007-02-28
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class ACARSMegsDAF
    {
        public ACARSMegsDAF()
        { }

        #region �洢OUT��
        /// <summary>
        /// �洢OUT��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOUTMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����OUT��
                acarsMegsDA.InsertOUTMegs(acarsMegBM);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// �洢�������OUT��
        /// ��¼�������������ĺ�����Ϣ
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOUTMegs(ACARSMegsBM acarsMegBM, int iUnCertMegId)
        {
            int retVal = -1;
            int iFlightId = 0;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����OUT��
                iFlightId = acarsMegsDA.InsertOUTMegs(acarsMegBM);
                //���¸�����ȷ�����ĵ���Ϣ
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �洢OFF��
        /// <summary>
        /// �洢OFF��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOFFMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����OFF��
                acarsMegsDA.InsertOFFMegs(acarsMegBM);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// �洢OFF��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOFFMegs(ACARSMegsBM acarsMegBM, int iUnCertMegId)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int iFlightId = 0;

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����OFF��
                iFlightId = acarsMegsDA.InsertOFFMegs(acarsMegBM);
                //���¸�����ȷ�����ĵ���Ϣ
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �洢ON��
        /// <summary>
        /// �洢ON��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertONMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int iFlightId = 0;

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����OUT��
                acarsMegsDA.InsertONMegs(acarsMegBM);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// �洢ON��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertONMegs(ACARSMegsBM acarsMegBM, int iUnCertMegId)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int iFlightId = 0;

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����OUT��
                iFlightId = acarsMegsDA.InsertONMegs(acarsMegBM);
                //���¸�����ȷ�����ĵ���Ϣ
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �洢IN��
        /// <summary>
        /// �洢IN��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertINMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����IN��
                acarsMegsDA.InsertINMegs(acarsMegBM);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// �洢IN��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertINMegs(ACARSMegsBM acarsMegBM, int iUnCertMegId)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int iFlightId = 0;

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����IN��
                iFlightId = acarsMegsDA.InsertINMegs(acarsMegBM);
                //���¸�����ȷ�����ĵ���Ϣ
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �洢RTN��
        /// <summary>
        /// �洢RTN��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertRTNMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����RTN��
                retVal = acarsMegsDA.InsertRTNMegs(acarsMegBM);
                //�޸ĺ���
                acarsMegsDA.UpdateFlightRTNInfo(acarsMegBM, retVal);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }

        /// <summary>
        /// �洢RTN��
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertRTNMegs(ACARSMegsBM acarsMegBM, int iUnCertMegId)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //����RTN��
                retVal = acarsMegsDA.InsertRTNMegs(acarsMegBM);
                //�޸ĺ���
                acarsMegsDA.UpdateFlightRTNInfo(acarsMegBM, retVal);
                //�޸ĸ�����ȷ�����ĵ���Ϣ
                //ȷ��RTN���Ǵ��ĸ����෢����
                acarsMegsDA.UpdateUnCertMeg(retVal, iUnCertMegId);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �洢��ȷ���ı���
        /// <summary>
        /// �洢��ȷ���ı���
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertUnCertMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //��ʼ����
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //�洢��ȷ���ı���
                retVal = acarsMegsDA.InsertUnCertMegs(acarsMegBM);
                //�ύ����
                acarsMegsDA.Transaction.Commit();
            }
            catch (Exception ex)
            {
                //�ع�����
                acarsMegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ������һ�����ĵ�ʱ��
        /// <summary>
        /// ������һ�����ĵ�ʱ��
        /// ��OFF��OUT����ON��OFF����IN��ON����RTN��OUT
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string GetPrevTime(ACARSMegsBM acarsMegBM)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            string strPrevTime = "";

            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                strPrevTime = acarsMegsDA.GetPrevTime(acarsMegBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }

            return strPrevTime;
        }
        #endregion

        #region �洢ԭʼ����
        /// <summary>
        /// �洢ԭʼ����
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOrigMegs(ACARSMegsBM acarsMegBM)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int retVal = -1;
            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                retVal = acarsMegsDA.InsertOrigMegs(acarsMegBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region �����Ĳ���FOCϵͳ
        /// <summary>
        /// ��MVA������FOCϵͳ
        /// </summary>
        /// <param name="strMVA">MVA�����ַ���</param>
        /// <returns></returns>
        public int InsertMVAMegs(string strMVA)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            string strConn = ConfigManagerSF.GetConfigString(SysConstBM.DB_FleetWatch, 1);
            int retVal = acarsMegsDA.InsertMVAMegs(strMVA, strConn);
            return retVal;
        }
        #endregion

        #region ��FE���Ĳ������ݿ�
        public int InsertFEMeg(string strFE)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int retVal = -1;
            try
            {
                string strf = ConfigManagerSF.GetConfigString(SysConstBM.DB_FEMegs, 1);
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FEMegs, 1));
                retVal = acarsMegsDA.InsertFEMeg(strFE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return retVal;
        }
        #endregion

        #region ��ȡFE�������ڷ���
        public DataTable GetFEMegs(int iMaxNo)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            DataTable dt = new DataTable();
            try
            {
                //�������ݿ�
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FEMegs, 1));
                dt = acarsMegsDA.GetFEMegs(iMaxNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acarsMegsDA.ConnClose();
            }
            return dt;
        }
        #endregion
    }
}
