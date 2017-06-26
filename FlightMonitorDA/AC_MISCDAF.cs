using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 飞机信息数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class AC_MISCDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AC_MISCDAF()
        {
        }

         /// <summary>
        /// 获取某席位没有选择的飞机号
        /// </summary>
        /// <param name="positionBM"></param>
        /// <returns></returns>
        public DataTable GetAirCraftByPositionId(PositionNameBM positionBM)
        {
            //定义返回值
            DataTable dtAirCraft = new DataTable();
            AC_MISCDA ac_miscDA = new AC_MISCDA();

            try
            {
                ac_miscDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtAirCraft = ac_miscDA.GetAirCraftByPositionId(positionBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_miscDA.ConnClose();
            }

            return dtAirCraft;
        }

        /// <summary>
        /// 插入新飞机
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public int InsertACMISC(AC_MISCBM ac_MISCBM)
        {
            int retVal = -1;
            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                retVal = ac_MISCDA.InsertACMISC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 删除飞机
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public int DeleteACMISC(AC_MISCBM ac_MISCBM)
        {
            int retVal = -1;
            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                retVal = ac_MISCDA.DeleteACMISC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return retVal;
        }

        /// <summary>
        /// 根据短号获取一条记录
        /// </summary>
        /// <param name="ac_MISCBM"></param>
        /// <returns></returns>
        public DataTable GetACMISCByAC(AC_MISCBM ac_MISCBM)
        {
            DataTable dtACMISC = new DataTable();       // 定义返回值

            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                dtACMISC = ac_MISCDA.GetACMISCByAC(ac_MISCBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return dtACMISC;
        }

        /// <summary>
        /// 获取所有飞机列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetACMISC()
        {
            DataTable dtACMISC = new DataTable();       // 定义返回值

            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                dtACMISC = ac_MISCDA.GetACMISC();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return dtACMISC;
        }

        /// <summary>
        /// 获取所有飞机列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetACMISCGroupBy()
        {
            DataTable dtACMISC = new DataTable();       // 定义返回值

            AC_MISCDA ac_MISCDA = new AC_MISCDA();

            try
            {
                ac_MISCDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));  //打开数据库连接
                dtACMISC = ac_MISCDA.GetACMISCGroupBy();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ac_MISCDA.ConnClose();
            }

            return dtACMISC;
        }
    }
}
