using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AirSoft.Public.SystemFramework;
using AirSoft.FlightMonitor.FlightMonitorBM;
using System.Collections;

namespace AirSoft.FlightMonitor.FlightMonitorDA
{
    /// <summary>
    /// 席位x信息数据访问外观类
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-05-29
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class PositionInforDAF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PositionInforDAF()
        {
        }

        /// <summary>
        /// 根据席位编号获取属于该席位的所有飞机的信息
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public DataTable GetInforByPositionId(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            //定义返回值
            DataTable dtPositionInfor = new DataTable();

            PositionInforDA positionInforDA = new PositionInforDA();

            try
            {
                //打开数据库连接
                positionInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                dtPositionInfor = positionInforDA.GetInforByPositionId(positionNameBM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                positionInforDA.ConnClose();
            }

            return dtPositionInfor;
        }

         /// <summary>
        /// 给某席位分配飞机
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public int InsertPositionInfors(FlightMonitorBM.PositionNameBM positionNameBM, IList ilPositionInforBM)
        {
            //定义返回值
            int retVal = -1;


            PositionInforDA positionInforDA = new PositionInforDA();

            try
            {
                //打开数据库连接
                positionInforDA.GetConnOpen(ConfigManagerSF.GetConfigString(SysConstBM.DB_FlightMonitor, 1));
                positionInforDA.Transaction = positionInforDA.SqlConn.BeginTransaction();
                retVal = positionInforDA.DeleteInforByPositionId(positionNameBM);

                if (retVal >= 0)
                {
                    IEnumerator iePositionInforBM = ilPositionInforBM.GetEnumerator();
                    while (iePositionInforBM.MoveNext())
                    {
                        retVal = positionInforDA.InsertInfor((PositionInforBM)iePositionInforBM.Current);
                    }
                }

                positionInforDA.Transaction.Commit();
            }
            catch (Exception ex)
            {
                positionInforDA.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                positionInforDA.ConnClose();
            }

            return retVal;
        }        
    }
}
