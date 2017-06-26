using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// �û�������֤
    /// </summary>
    /// Copyright (c) 2003-2007 ���Ϻ��չɷ����޹�˾
    /// �� �� �ˣ���Ԫ��
    /// �������ڣ�2007-04-25
    /// �� �� �ˣ�
    /// �޸����ڣ�
    /// ��    ����
    public class Validator
    {
        /// <summary>
        /// ���TextBox�����Ƿ�Ϊ��
        /// </summary>
        /// <param name="strContent">�ж�����</param>
        /// <param name="strMsg">��ʾ����</param>
        /// <returns>���Ϊ���򷵻�false</returns>
        public static bool CheckNull(string strContent, string strMsg)
        {
            if (strContent.Trim().Length <= 0)
            {
                MessageBox.Show(strMsg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// �ж������ַ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="strNumber">������ַ���</param>
        /// <param name="strMsg">��ʾ����</param>
        /// <returns>false����Ϊ����</returns>
        public static bool IsNumber(string strNumber, string strMsg)
        {
            //���巵��ֵ
            bool blnReturn = false;
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            blnReturn = !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);

            if (!blnReturn)
            {
                MessageBox.Show(strMsg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return blnReturn;
        }

        /// <summary>
        /// ת�ƽ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
