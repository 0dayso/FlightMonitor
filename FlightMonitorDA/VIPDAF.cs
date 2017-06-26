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
    /// VIP数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-18
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class VIPDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VIPDAF()
        {
        }

        /// <summary>
        /// 从FOC获取符合条件的VIP
        /// </summary>
        /// <param name="strStartDATOP">开始日期 格式2007-05-12</param>
        /// <returns>包含数据集的VIP</returns>
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
        /// 根据DATOP获取所有VIP
        /// </summary>
        /// <param name="strDATOP">日期</param>
        /// <returns>包含所有符合条件的VIP的数据表</returns>
        public DataTable GetVIPByDATOP(string strDATOP)
        {
            //定义返回值
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
        /// 更新VIP所有信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public int UpdateVipInfor(FlightMonitorBM.VIPBM vipBM)
        {
            //定义返回值
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();
            try
            {
                //打开数据连接
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
        /// 插入一条VIP信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public int InsertVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //定义返回值
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();

            try
            {
                //打开数据库连接
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
        /// 删除一条VIP信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public int DeleteVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //定义返回值
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();

            try
            {
                //打开数据库连接
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
        /// 根据旅客姓名和航班信息获取VIP
        /// </summary>
        /// <param name="vipBM"></param>
        /// <returns></returns>
        public DataTable GetVIPByName(VIPBM vipBM)
        {
            //定义返回值
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
        /// 获取航班的VIP信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public DataTable GetVIPByFlight(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            //定义返回值
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
        /// 逻辑删除或添加一个航班
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public int UpdateDeleteTag(VIPBM vipBM, int iDeleteTag)
        {
            //定义返回值
            int retVal = -1;

            FlightMonitorDA.VIPDA vipDA = new VIPDA();

            try
            {
                //打开数据库连接
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
