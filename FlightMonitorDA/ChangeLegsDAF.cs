using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorBM;
using AirSoft.Public.SystemFramework;
using System.Collections;
using System.Data;


namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 航班变更数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-04
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class ChangeLegsDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChangeLegsDAF()
        {
        }

        /// <summary>
        /// 插入航班变更动态
        /// </summary>
        /// <param name="changeLegsBM">航班变更实体对象</param>
        /// <param name="changeRecordBM">航班操作实体对象</param>
        /// <returns>1：成功0：失败</returns>
        public int Insert(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            int retVal = -1;  //定义返回值
            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            //打开数据库连接访问
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
            changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();


            try
            {
                //插入一条航班动态变更
                changeLegsDA.Insert(changeLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;
                changeRecordDA.Insert(changeRecordBM);

                //事务提交
                changeLegsDA.Transaction.Commit();

                //返回值
                retVal = 1;
            }
            catch (Exception ex)
            {
                //事务回滚
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 逻辑删除一条航班动态
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <param name="changeRecordBM">航班操作实体</param>
        /// <returns>1：成功0：失败</returns>
        public int LogicDelete(FlightMonitorBM.ChangeLegsBM changeLegsBM, FlightMonitorBM.ChangeRecordBM changeRecordBM)
        {
            ///定义返回值
            int retVal = -1;

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            //打开数据库连接访问
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
            changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();

            try
            {
                //逻辑删除一条航班动态
                changeLegsDA.LogicDelete(changeLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;
                changeRecordDA.Insert(changeRecordBM);

                //事务提交
                changeLegsDA.Transaction.Commit();

                //返回值
                retVal = 1;
            }
            catch (Exception ex)
            {
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 以主键为条件更新航班信息
        /// </summary>
        /// <param name="oldChangeLegsBM">变更前航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更操作列表</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateAllInfoByPriKey(ChangeLegsBM changeLegsBM, IList ilChangeRecordBM)
        {
            //定义返回值
            int retVal = -1;

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();

            //打开数据库连接
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
            changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();

            try
            {
                //以主键为条件更新航班动态
                changeLegsDA.UpdateAllInfoByPriKey(changeLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    changeRecordDA.Insert(changeRecordBM);
                }

                //提交事务
                changeLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 以组合主键为条件更新航班信息
        /// </summary>
        /// <param name="changeLegsBM">变更前航班动态实体对象</param>
        /// <param name="ilChangeRecordBM">航班变更操作列表</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateAllInfoByComKey(ChangeLegsBM changeLegsBM,ChangeLegsBM originalLegsBM, IList ilChangeRecordBM)
        {
            //定义返回值
            int retVal = -1;

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            ChangeRecordDA changeRecordDA = new ChangeRecordDA();           

            try
            {
                //打开数据库连接
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                changeLegsDA.Transaction = changeLegsDA.SqlConn.BeginTransaction();

                //以主键为条件更新航班动态
                changeLegsDA.UpdateAllInfoByComKey(changeLegsBM, originalLegsBM);

                //插入航班操作记录
                changeRecordDA.SqlConn = changeLegsDA.SqlConn;
                changeRecordDA.Transaction = changeLegsDA.Transaction;

                IEnumerator ieChangeRecordBM = ilChangeRecordBM.GetEnumerator();
                while (ieChangeRecordBM.MoveNext())
                {
                    ChangeRecordBM changeRecordBM = (ChangeRecordBM)ieChangeRecordBM.Current;
                    if (changeRecordBM.ChangeReasonCode != "cnvcAC")
                    {
                        changeRecordDA.Insert(changeRecordBM);
                    }
                }

                //提交事务
                changeLegsDA.Transaction.Commit();

                retVal = 1;
            }
            catch (Exception ex)
            {
                changeLegsDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                changeRecordDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 以主键为条件查询一条记录
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <returns></returns>
        public DataTable GetFlightByKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            //打开数据库连接
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = changeLegsDA.GetFlightByKey(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }

        /// <summary>
        /// 以组合主键为条件查询一条记录
        /// </summary>
        /// <param name="changeLegsBM">航班变更动态实体</param>
        /// <returns></returns>
        public DataTable GetFlightByCombineKey(FlightMonitorBM.ChangeLegsBM changeLegsBM)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

           

            try
            {
                //打开数据库连接
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeLegsDA.GetFlightByCombineKey(changeLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }

         /// <summary>
        /// 根据acars起飞报信息获取航班
        /// </summary>
        /// <param name="acarsLegsBM">起飞报实体对象</param>
        /// <returns></returns>
        public DataTable GetFlightByDEPInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //打开数据库连接
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeLegsDA.GetFlightByDEPInfo(acarsLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }

        /// <summary>
        /// 根据acars落地报信息获取航班
        /// </summary>
        /// <param name="acarsLegsBM">落地报实体对象</param>
        /// <returns></returns>
        public DataTable GetFlightByARRInfo(FlightMonitorBM.ACARSLegsBM acarsLegsBM)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            try
            {
                //打开数据库连接
                changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dataTable = changeLegsDA.GetFlightByARRInfo(acarsLegsBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }

            return dataTable;
        }

        /// <summary>
        /// 读取航班变更文件，以DataSet的形式返回
        /// </summary>
        /// <param name="strFullPath">变更文件完整路径</param>
        /// <returns></returns>
        public DataSet GetChangeLegsFromFile(string strFullPath)
        {
            //定义返回值
            DataSet dataSet = new ScheduleLegsBM();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();
            try
            {
                dataSet = changeLegsDA.GetChangeLegsFromFile(strFullPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }


        #region added by LinYong

        #region 获取当天所有的航班动态 --added in 2009.10.26 ,获取 tbLegs 所有字段
        /// <summary>
        /// 获取当天所有的航班动态
        /// </summary>
        /// <param name="DateTimeBM"></param>
        /// <returns></returns>
        public DataTable GetAllLegsByDay(FlightMonitorBM.DateTimeBM dateTimeBM)
        {
            //定义返回值
            DataTable dataTable = new DataTable();

            ChangeLegsDA changeLegsDA = new ChangeLegsDA();

            //打开数据库连接
            changeLegsDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));

            try
            {
                dataTable = changeLegsDA.GetAllLegsByDay(dateTimeBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                changeLegsDA.ConnClose();
            }
            return dataTable;
        }
        #endregion

        #endregion

    }
}
