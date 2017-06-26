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
    /// ACARS报文处理数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：张  黎
    /// 创建日期：2007-02-28
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSMegsDAF
    {
        public ACARSMegsDAF()
        { }

        #region 存储OUT报
        /// <summary>
        /// 存储OUT报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOUTMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入OUT报
                acarsMegsDA.InsertOUTMegs(acarsMegBM);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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
        /// 存储修正后的OUT报
        /// 记录修正后报文所属的航班信息
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
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入OUT报
                iFlightId = acarsMegsDA.InsertOUTMegs(acarsMegBM);
                //更新该条不确定报文的信息
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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

        #region 存储OFF报
        /// <summary>
        /// 存储OFF报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOFFMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入OFF报
                acarsMegsDA.InsertOFFMegs(acarsMegBM);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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
        /// 存储OFF报
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
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入OFF报
                iFlightId = acarsMegsDA.InsertOFFMegs(acarsMegBM);
                //更新该条不确定报文的信息
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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

        #region 存储ON报
        /// <summary>
        /// 存储ON报
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
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入OUT报
                acarsMegsDA.InsertONMegs(acarsMegBM);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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
        /// 存储ON报
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
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入OUT报
                iFlightId = acarsMegsDA.InsertONMegs(acarsMegBM);
                //更新该条不确定报文的信息
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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

        #region 存储IN报
        /// <summary>
        /// 存储IN报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertINMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入IN报
                acarsMegsDA.InsertINMegs(acarsMegBM);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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
        /// 存储IN报
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
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入IN报
                iFlightId = acarsMegsDA.InsertINMegs(acarsMegBM);
                //更新该条不确定报文的信息
                acarsMegsDA.UpdateUnCertMeg(iFlightId, iUnCertMegId);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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

        #region 存储RTN报
        /// <summary>
        /// 存储RTN报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertRTNMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入RTN报
                retVal = acarsMegsDA.InsertRTNMegs(acarsMegBM);
                //修改航班
                acarsMegsDA.UpdateFlightRTNInfo(acarsMegBM, retVal);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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
        /// 存储RTN报
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertRTNMegs(ACARSMegsBM acarsMegBM, int iUnCertMegId)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //插入RTN报
                retVal = acarsMegsDA.InsertRTNMegs(acarsMegBM);
                //修改航班
                acarsMegsDA.UpdateFlightRTNInfo(acarsMegBM, retVal);
                //修改该条不确定报文的信息
                //确定RTN报是从哪个航班发出的
                acarsMegsDA.UpdateUnCertMeg(retVal, iUnCertMegId);
                //提交事务
                acarsMegsDA.Transaction.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
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

        #region 存储不确定的报文
        /// <summary>
        /// 存储不确定的报文
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertUnCertMegs(ACARSMegsBM acarsMegBM)
        {
            int retVal = -1;
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();

            try
            {
                //连接数据库
                acarsMegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_ACARSMegs, 1));
                //开始事务
                acarsMegsDA.Transaction = acarsMegsDA.SqlConn.BeginTransaction();
                //存储不确定的报文
                retVal = acarsMegsDA.InsertUnCertMegs(acarsMegBM);
                //提交事务
                acarsMegsDA.Transaction.Commit();
            }
            catch (Exception ex)
            {
                //回滚事务
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

        #region 查找上一个报文的时间
        /// <summary>
        /// 查找上一个报文的时间
        /// 对OFF是OUT，对ON是OFF，对IN是ON，对RTN是OUT
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public string GetPrevTime(ACARSMegsBM acarsMegBM)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            string strPrevTime = "";

            try
            {
                //连接数据库
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

        #region 存储原始报文
        /// <summary>
        /// 存储原始报文
        /// </summary>
        /// <param name="acarsMegBM"></param>
        /// <returns></returns>
        public int InsertOrigMegs(ACARSMegsBM acarsMegBM)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int retVal = -1;
            try
            {
                //连接数据库
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

        #region 将报文插入FOC系统
        /// <summary>
        /// 将MVA报插入FOC系统
        /// </summary>
        /// <param name="strMVA">MVA报文字符串</param>
        /// <returns></returns>
        public int InsertMVAMegs(string strMVA)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            string strConn = ConfigManagerSF.GetConfigString(SysConstBM.DB_FleetWatch, 1);
            int retVal = acarsMegsDA.InsertMVAMegs(strMVA, strConn);
            return retVal;
        }
        #endregion

        #region 将FE报文插入数据库
        public int InsertFEMeg(string strFE)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            int retVal = -1;
            try
            {
                string strf = ConfigManagerSF.GetConfigString(SysConstBM.DB_FEMegs, 1);
                //连接数据库
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

        #region 提取FE报文用于发送
        public DataTable GetFEMegs(int iMaxNo)
        {
            ACARSMegsDA acarsMegsDA = new ACARSMegsDA();
            DataTable dt = new DataTable();
            try
            {
                //连接数据库
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
