using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorDA;
using System.Data;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// 数据项权限数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-23
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class DataItemPurviewBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataItemPurviewBF()
        {
        }

        /// <summary>
        /// 获取所有数据项
        /// </summary>
        /// <returns></returns>
        public ReturnValueSF GetDataItems()
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItems();
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
        /// 获取用户的数据项访问权限
        /// </summary>
        /// <param name="dataItemPurviewBM">访问权限实体对象</param>
        /// <returns></returns>
        public ReturnValueSF GetDataItemPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItemPurviewByUserId(accountBM);
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
        /// yanxian 获取参考点
        /// </summary>
        /// <param name="dataItemPurviewBM">访问权限实体对象</param>
        /// <returns></returns>
        public ReturnValueSF GetDataItemPointPurviewByUserId(FlightMonitorBM.AccountBM accountBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItemPointPurviewByUserId(accountBM);
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
        /// 判断数据项权限是否已经存在
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns>自定义返回值</returns>
        private ReturnValueSF ExistDataItemPurview(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();
            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                DataTable dtPurview = dataItemPurviewDAF.GetDataItemPurviewByDataItemNo(dataItemPurviewBM);

                if (dtPurview.Rows.Count == 0)
                {
                    rvSF.Result = 0;
                }
                else
                {
                    rvSF.Result = 1;
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }


         /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="dataItemPurview"></param>
        /// <returns></returns>
        public ReturnValueSF Insert(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();

            try
            {
                rvSF = ExistDataItemPurview(dataItemPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF.Result = dataItemPurviewDAF.Insert(dataItemPurviewBM);
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }

        /// <summary>
        /// 更新数据项权限
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdatePurview(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF = ExistDataItemPurview(dataItemPurviewBM);
                if (rvSF.Result == 0)
                {
                    rvSF = Insert(dataItemPurviewBM);
                }
                else if (rvSF.Result > 0)
                {
                    rvSF.Result = dataItemPurviewDAF.UpdatePurview(dataItemPurviewBM);
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 更新数据项的可见性
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateVisible(IList ilDataItemPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Result = dataItemPurviewDAF.UpdateVisible(ilDataItemPurviewBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 更新数据项的显示顺序
        /// </summary>
        /// <param name="dataItemPurviewBM"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateIndex(IList ilDataItemPurviewBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Result = dataItemPurviewDAF.UpdateIndex(ilDataItemPurviewBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 查询某用户某数据项的权限
        /// </summary>
        /// <param name="dataItemPurviewBM">数据项权限实体对象</param>
        /// <returns></returns>
        public ReturnValueSF GetDataItemPurviewByDataItemNo(FlightMonitorBM.DataItemPurviewBM dataItemPurviewBM)
        {
             //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetDataItemPurviewByDataItemNo(dataItemPurviewBM);
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
        /// 获取用户有权限的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetPurviewDataItem(FlightMonitorBM.AccountBM accountBM)
        {
              //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetPurviewDataItem(accountBM);
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
        /// 获取用户设置的可见的数据项
        /// </summary>
        /// <param name="accountBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM)
        {
              //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetVisibleDataItem(accountBM);
                rvSF.Result = 1;
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }
            return rvSF;
        }
        public ReturnValueSF GetVisibleDataItem(FlightMonitorBM.AccountBM accountBM, string strType)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            FlightMonitorDA.DataItemPurviewDAF dataItemPurviewDAF = new DataItemPurviewDAF();
            try
            {
                rvSF.Dt = dataItemPurviewDAF.GetVisibleDataItem(accountBM, strType);
                rvSF.Result = 1;
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
