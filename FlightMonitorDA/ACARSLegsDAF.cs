using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// ACARS航班动态数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-16
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ACARSLegsDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ACARSLegsDAF()
        {
        }

        /// <summary>
        /// 从文本文件读取航班动态信息
        /// </summary>
        /// <param name="strFullPath">文件路径</param>
        /// <returns>ACARS航班动态文本信息</returns>
        public string GetACARSLegsBMFromFile(string strFullPath)
        {
            //定义返回值
            string strACARSLegsInfo = "";
            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            try
            {
                strACARSLegsInfo = acarsLegsDA.GetACARSLegsBMFromFile(strFullPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strACARSLegsInfo;
        }

         /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSOnInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //定义返回值
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //打开数据库连接
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //更新ACARS航班动态
                acarsLegsDA.UpdateACARSOnInfo(acarsLegsBM, originalLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //记录ACARS报文
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                //提交事务
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSInInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //定义返回值
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //打开数据库连接
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //更新ACARS航班动态
                acarsLegsDA.UpdateACARSInInfo(acarsLegsBM, originalLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }


                //记录ACARS报文
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



                //提交事务
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                //回滚事务
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSOutInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //定义返回值
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //打开数据库连接
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //更新ACARS航班动态
                acarsLegsDA.UpdateACARSOutInfo(acarsLegsBM, originalLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //记录ACARS报文
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



                //提交事务
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 更新航班动态的ACARS信息
        /// </summary>
        /// <param name="acarsLegsBM">ACARS航班动态实体对象</param>
        /// <param name="originalLegsBM">原航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更实体对象列表</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateACARSOffInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM, FlightMonitorBM.ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //定义返回值
            int retVal = -1;

            ACARSLegsDA acarsLegsDA = new ACARSLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //打开数据库连接
                acarsLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                acarsLegsDA.Transaction = acarsLegsDA.SqlConn.BeginTransaction();

                //更新ACARS航班动态
                acarsLegsDA.UpdateACARSOffInfo(acarsLegsBM, originalLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = acarsLegsDA.SqlConn;
                changeRecordDA.Transaction = acarsLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //记录ACARS报文
                changeLegsDA.SqlConn = acarsLegsDA.SqlConn;
                changeLegsDA.Transaction = acarsLegsDA.Transaction;
                changeLegsDA.InsertACARSMessage(originalLegsBM, acarsLegsBM.MessageContent, acarsLegsBM.MessageTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));



                //提交事务
                acarsLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                acarsLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                acarsLegsDA.ConnClose();
            }

            return retVal;
        }
    }
}
