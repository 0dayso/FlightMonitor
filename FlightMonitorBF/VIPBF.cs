using System;
using System.Collections.Generic;
using System.Text;
using AirSoft.FlightMonitor.FlightMonitorDA;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Data;

namespace AirSoft.FlightMonitor.FlightMonitorBF
{
    /// <summary>
    /// VIP业务外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-18
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class VIPBF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VIPBF()
        {
        }

        /// <summary>
        /// 从FOC获取符合条件的VIP
        /// </summary>
        /// <param name="strStartDATOP">开始日期 格式2007-05-12</param>
        /// <returns>包含数据集的VIP</returns>
        public ReturnValueSF GetVIPFromFoc(string strStartDATOP)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问外观类方法
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();
                rvSF.Ds = vipDAF.GetVIPFromFoc(strStartDATOP);
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
        /// 根据DATOP获取所有VIP
        /// </summary>
        /// <param name="strDATOP">日期</param>
        /// <returns>包含所有符合条件的VIP的数据表</returns>
        public ReturnValueSF GetVIPByDATOP(string strDATOP)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问外观类方法
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();
                rvSF.Dt = vipDAF.GetVIPByDATOP(strDATOP);
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
        /// 更新VIP所有信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public ReturnValueSF UpdateVipInfor(FlightMonitorBM.VIPBM vipBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层外观类方法
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.UpdateVipInfor(vipBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

         /// <summary>
        /// 插入一条VIP信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public ReturnValueSF InsertVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问外观类方法
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.InsertVIP(vipBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 删除一条VIP信息
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns></returns>
        public ReturnValueSF DeleteVIP(FlightMonitorBM.VIPBM vipBM)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问外观类方法
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.DeleteVIP(vipBM);
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = Public.SystemFramework.MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 根据旅客姓名和航班信息获取VIP
        /// </summary>
        /// <param name="vipBM"></param>
        /// <returns></returns>
        public ReturnValueSF GetVIPByName(VIPBM vipBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问外观类方法
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();
                rvSF.Dt = vipDAF.GetVIPByName(vipBM);
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
        /// 检查VIP是否存在
        /// </summary>
        /// <param name="vipBM">VIP实体对象</param>
        /// <returns>自定义类型</returns>
        public ReturnValueSF ValidVIP(VIPBM vipBM)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层数据访问外观类方法
                VIPDAF vipDAF = new VIPDAF();
                DataTable dtVIP = vipDAF.GetVIPByName(vipBM);

                if (dtVIP.Rows.Count > 0)
                {
                    //VIP用户已存在
                    rvSF.Result = 1;                    
                }
                else
                {
                    rvSF.Result = 0;                    
                }
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 获取航班的VIP信息
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public ReturnValueSF GetVIPByFlight(ChangeLegsBM changeLegsBM, int iDeleteTag)
        {
            //定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问层数据访问外观类方法
                VIPDAF vipDAF = new VIPDAF();
                rvSF.Dt = vipDAF.GetVIPByFlight(changeLegsBM, iDeleteTag);

                
                rvSF.Result = 1;
               
            }
            catch (Exception ex)
            {
                rvSF.Result = -1;
                rvSF.Message = MessageSF.Error(ex);
            }

            return rvSF;
        }

        /// <summary>
        /// 逻辑删除或添加一个航班
        /// </summary>
        /// <param name="changeLegsBM"></param>
        /// <param name="iDeleteTag"></param>
        /// <returns></returns>
        public ReturnValueSF UpdateDeleteTag(VIPBM vipBM, int iDeleteTag)
        {
            //自定义返回值
            ReturnValueSF rvSF = new ReturnValueSF();

            try
            {
                //调用数据访问外观类方法
                FlightMonitorDA.VIPDAF vipDAF = new VIPDAF();

                rvSF.Result = vipDAF.UpdateDeleteTag(vipBM, iDeleteTag);
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
