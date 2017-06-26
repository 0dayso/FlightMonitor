using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AirSoft.FlightMonitor.FlightMonitorClient
{
    /// <summary>
    /// 用户输入验证
    /// </summary>
    /// Copyright (c) 2003-2007 海南航空股份有限公司
    /// 创 建 人：王元斌
    /// 创建日期：2007-04-25
    /// 修 改 人：
    /// 修改日期：
    /// 版    本：
    public class Validator
    {
        /// <summary>
        /// 检查TextBox输入是否为空
        /// </summary>
        /// <param name="strContent">判断内容</param>
        /// <param name="strMsg">提示内容</param>
        /// <returns>如果为空则返回false</returns>
        public static bool CheckNull(string strContent, string strMsg)
        {
            if (strContent.Trim().Length <= 0)
            {
                MessageBox.Show(strMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断输入字符串是否为数字
        /// </summary>
        /// <param name="strNumber">输入的字符串</param>
        /// <param name="strMsg">提示内容</param>
        /// <returns>false：不为数字</returns>
        public static bool IsNumber(string strNumber, string strMsg)
        {
            //定义返回值
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
                MessageBox.Show(strMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return blnReturn;
        }

        /// <summary>
        /// 转移焦点
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
