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
    /// ϯλx��Ϣ���ݷ��������
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-05-29
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class PositionInforDAF
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PositionInforDAF()
        {
        }

        /// <summary>
        /// ����ϯλ��Ż�ȡ���ڸ�ϯλ�����зɻ�����Ϣ
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public DataTable GetInforByPositionId(FlightMonitorBM.PositionNameBM positionNameBM)
        {
            //���巵��ֵ
            DataTable dtPositionInfor = new DataTable();

            PositionInforDA positionInforDA = new PositionInforDA();

            try
            {
                //�����ݿ�����
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
        /// ��ĳϯλ����ɻ�
        /// </summary>
        /// <param name="positionInforBM"></param>
        /// <returns></returns>
        public int InsertPositionInfors(FlightMonitorBM.PositionNameBM positionNameBM, IList ilPositionInforBM)
        {
            //���巵��ֵ
            int retVal = -1;


            PositionInforDA positionInforDA = new PositionInforDA();

            try
            {
                //�����ݿ�����
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
